using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

namespace Watermelon
{
	public class GuideUI : MonoBehaviour
	{
		[SerializeField] private FruitUIUnit[] units;
		[SerializeField] private Image[] fruits;

		private bool isPlayed = false;

		private void Awake()
		{
			foreach (var unit in units)
			{
				unit.gameObject.SetActive(false);
			}
			foreach(var fruit in fruits)
			{
				fruit.gameObject.SetActive(false);
			}
		}

		public async UniTask SetupUI()
		{
			if(isPlayed) return;

			for(int i = 0; i < 11; i++)
			{
				units[i].Setup(i);
				units[i].transform.DOScale(Vector3.zero, 0);
				fruits[i].transform.DOScale(Vector3.zero, 0);

				units[i].gameObject.SetActive(true);
				units[i].transform.DOScale(Vector3.one, Defines.ScaleTime);
				fruits[i].gameObject.SetActive(true);
				fruits[i].color = FruitColor.GetColor(i);
				fruits[i].transform.DOScale(Vector3.one * 0.6f, Defines.ScaleTime);
				await UniTask.Delay(TimeSpan.FromSeconds(Defines.ScaleTime));
			}

			isPlayed = true;
		}
	}
}