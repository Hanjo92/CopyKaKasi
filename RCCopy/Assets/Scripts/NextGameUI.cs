using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI description;
	
	public void SetTexts(bool isClear)
	{
		if(title)
		{
			title.text = isClear ? "Clear!" : "Defeat";
		}
		if(description)
		{
			description.text = isClear ? "You've defeated all enemies!\n:D" : "Your castle is destroyed!\n:(";
		}
	}
}
