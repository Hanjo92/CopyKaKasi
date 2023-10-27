using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
	public class FruitUIUnit : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI fruitName;
		[SerializeField] private Image sprite;

		public void Setup(int level)
		{
			fruitName.text = FruitName.GetName(level);
			sprite.color = FruitColor.GetColor(level);
		}
	}
}