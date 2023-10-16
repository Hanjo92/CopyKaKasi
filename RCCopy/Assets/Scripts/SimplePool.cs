using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolObj
{
	string TemplateKey {  get; }
	void Init(object[] param = null);
    void Release();
}

public class SimplePool
{
    public static Dictionary<string, Queue<MonoBehaviour>> poolDictionary = new Dictionary<string, Queue<MonoBehaviour>>();

    public static T Instantiate<T>(T template, object[] param = null) where T : MonoBehaviour, IPoolObj
	{
		var key = template.TemplateKey;
		if(string.IsNullOrEmpty(key))
		{
			key = template.gameObject.name.ToString();
		}
		var pooledObj = GetPooledObject<T>(key, param);
		if(pooledObj == null)
		{
			pooledObj = Object.Instantiate(template);
			pooledObj.Init(param);
		}

		return pooledObj;
	}

	public static T InstantiatePrimitive<T>(PrimitiveType primitiveType, string key, object[] param = null) where T : MonoBehaviour, IPoolObj
	{
		var primitiveName = primitiveType.ToString() + key;
		var pooledObj = GetPooledObject<T>(primitiveName, param);
		if(pooledObj != null)
			return pooledObj;

		var gameObject = GameObject.CreatePrimitive(primitiveType);
		gameObject.name = primitiveName;
		var t = gameObject.AddComponent<T>();
		t.Init(param);
		return t;
	}

	private static T GetPooledObject<T>(string key, object[] param) where T : MonoBehaviour, IPoolObj
	{
		var hasPool = poolDictionary.TryGetValue(key, out var pool);
		if(hasPool)
		{
			if(pool.Count > 0)
			{
				var obj = (T)pool.Dequeue();
				obj.gameObject.SetActive(true);
				obj.Init(param);
				return obj;
			}
		}
		if(hasPool == false)
		{
			poolDictionary.Add(key, new Queue<MonoBehaviour>());
		}
		return null;
	}

	public static void Release<T>(T obj, bool destroy = false) where T : MonoBehaviour, IPoolObj
	{
		var key = obj.TemplateKey;
		if(destroy)
		{
			GameObject.Destroy(obj.gameObject);
			return;
		}

		if(poolDictionary.TryGetValue(key, out var pool) == false)
		{
			Debug.LogWarning($"It is not pooled object :: {key}\nCreate Pool :: Key :{key}");
			pool = new Queue<MonoBehaviour>();
			poolDictionary.Add(key, pool);
		}
		pool.Enqueue(obj);
		obj.gameObject.SetActive(false);
	}

	public static void ReleaseOrDestroy<T>(T obj, bool destroy = false) where T : MonoBehaviour, IPoolObj
	{
		var key = obj.TemplateKey;
		if(destroy || poolDictionary.TryGetValue(key, out var pool) == false)
		{
			GameObject.Destroy(obj.gameObject);
			return;
		}

		pool.Enqueue(obj);
		obj.gameObject.SetActive(false);
	}

	public static void Clear(string key)
	{
		if( poolDictionary.TryGetValue(key, out var pool))
		{
			while(pool.Count > 0)
			{
				var obj = pool.Dequeue();
				GameObject.Destroy(obj.gameObject);
			}
			poolDictionary.Remove(key);
		}
	}
}
