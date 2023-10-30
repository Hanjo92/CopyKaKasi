using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Almond
{
#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(SerializedPropertyAttribute))]
	public class SerializedPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var fieldAttribute = (attribute as SerializedPropertyAttribute);
			var propertyName = fieldAttribute.TargetName;
			var target = property.serializedObject.targetObject;
			var type = target.GetType();
			var t = property.propertyType.ConvertType();
			if(t == null)
			{
				GUI.Label(position, $"Type not supported :: {property.propertyType}");
				return;
			}

			var targetProperty = type.GetProperty(propertyName);
			if(targetProperty != null)
			{
				if(targetProperty.PropertyType != t)
				{
					GUI.Label(position, $"Mismatched types M : {t.ToString()} / P:: {targetProperty.PropertyType.ToString()}");
					return;
				}

				SetValue(target, propertyName, property.propertyType, targetProperty, position);
			}
			else
			{
				GUI.Label(position, $"Is not property type. {propertyName}");
			}
		}
		#region EditorGUI Group
		private void SetValue(object target, string propertyName, SerializedPropertyType serializedPropertyType, PropertyInfo targetProperty, Rect position)
		{
			var originColor = GUI.backgroundColor;
			GUI.contentColor = Color.green;
			switch(serializedPropertyType)
			{
				case SerializedPropertyType.Integer:
				{
					int currentValue = (int)targetProperty.GetValue(target);
					var rangeAttribute = fieldInfo.GetAttribute<RangeAttribute>();
					int inspectorValue;
					if(rangeAttribute != null)
					{
						inspectorValue = EditorGUI.IntSlider(position, propertyName, currentValue, (int)rangeAttribute.min, (int)rangeAttribute.max);
					}
					else
					{
						inspectorValue = EditorGUI.IntField(position, propertyName, currentValue);
					}
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Boolean:
				{
					var currentValue = (bool)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Toggle(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Float:
				{
					var currentValue = (float)targetProperty.GetValue(target);
					var rangeAttribute = fieldInfo.GetAttribute<RangeAttribute>();
					float inspectorValue;
					if(rangeAttribute != null)
					{
						inspectorValue = EditorGUI.Slider(position, propertyName, currentValue, rangeAttribute.min, rangeAttribute.max);
					}
					else
					{
						inspectorValue = EditorGUI.FloatField(position, propertyName, currentValue);
					}

					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.String:
				{
					var currentValue = (string)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.TextField(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Color:
				{
					var currentValue = (Color)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.ColorField(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Vector2:
				{
					var currentValue = (Vector2)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Vector2Field(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Vector3:
				{
					var currentValue = (Vector3)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Vector3Field(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Vector4:
				{
					var currentValue = (Vector4)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Vector4Field(position, propertyName, currentValue);

					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Rect:
				{
					var currentValue = (Rect)targetProperty.GetValue(target);
					var view = new Vector4(currentValue.x, currentValue.y, currentValue.width, currentValue.height);
					var inspectorValue = EditorGUI.Vector4Field(position, propertyName, view);
					var toVector4 = new Rect(inspectorValue.x, inspectorValue.y, inspectorValue.z, inspectorValue.w);
					if(toVector4 != currentValue)
						targetProperty.SetValue(target, toVector4);
				}
				break;
				case SerializedPropertyType.Vector2Int:
				{
					var currentValue = (Vector2Int)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Vector2IntField(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
				case SerializedPropertyType.Vector3Int:
				{
					var currentValue = (Vector3Int)targetProperty.GetValue(target);
					var inspectorValue = EditorGUI.Vector3IntField(position, propertyName, currentValue);
					if(inspectorValue != currentValue)
						targetProperty.SetValue(target, inspectorValue);
				}
				break;
			};
			GUI.contentColor = originColor;
		}

		#endregion
	}
#endif

	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public class SerializedPropertyAttribute : PropertyAttribute
	{
		public string TargetName
		{
			get;
		}

		/// <summary>
		/// is bool property toggle button on inspector
		/// </summary>
		/// <param name="targetName"> must be a float type </param>
		public SerializedPropertyAttribute(string targetName)
		{
			TargetName = targetName;
		}
	}

	public static class SerializedPropertyExtention
	{
		public static Type ConvertType(this SerializedPropertyType serializedPropertyType)
		{
			return serializedPropertyType switch
			{
				SerializedPropertyType.Integer => typeof(int),
				SerializedPropertyType.Boolean => typeof(bool),
				SerializedPropertyType.Float => typeof(float),
				SerializedPropertyType.String => typeof(string),
				SerializedPropertyType.Color => typeof(Color),
				SerializedPropertyType.Vector2 => typeof(Vector2),
				SerializedPropertyType.Vector3 => typeof(Vector3),
				SerializedPropertyType.Vector4 => typeof(Vector4),
				SerializedPropertyType.Rect => typeof(Rect),
				SerializedPropertyType.Vector2Int => typeof(Vector2Int),
				SerializedPropertyType.Vector3Int => typeof(Vector3Int),
				_ => null
			};
		}
	}
}