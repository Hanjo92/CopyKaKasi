using TMPro;
using UnityEngine;
using Almond;

namespace RCCopy
{
	public class MonsterUI : MonoBehaviour, IPoolObj
	{
		[SerializeField] private string templateKey = "BasicMonsterUI";
		[SerializeField] private TextMeshPro nameText;
		[SerializeField] private SpriteRenderer hpSlider;

		private Vector2 sliderSize;

		public string TemplateKey => templateKey;
		private void Awake()
		{
			sliderSize = hpSlider.size;
		}

		public void Init(object[] param = null)
		{
			if(param == null)
			{
				Debug.LogWarning("pram is null");
				return;
			}

			if(param.Length < 2)
			{
				Debug.LogWarning("pram count not match");
				return;
			}

			var parents = (Transform)param[0];
			transform.SetParent(parents.transform, false);
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.localPosition = Vector3.zero;

			nameText.text = param[1].ToString();

			hpSlider.size = sliderSize;
			hpSlider.transform.localPosition = Vector3.zero;
		}
		public void Release()
		{
			transform.parent = null;
			SimplePool.Release(this);
		}

		public void UpdateSlider(float ratio)
		{
			var newSize = sliderSize;
			newSize.x *= ratio;
			hpSlider.transform.localPosition = Vector3.left * (sliderSize.x - newSize.x) * 0.5f;

			hpSlider.size = newSize;
		}
	}
}