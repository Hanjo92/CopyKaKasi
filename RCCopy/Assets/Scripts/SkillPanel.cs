using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private SkillUI[] skillUIs;

	public void SetSkillUIs()
	{
		var list = GameController.Instance.PickRandomSkill(skillUIs.Length);
		for(int i = 0; i < skillUIs.Length; i++)
		{
			skillUIs[i].SetSkill(list[i]);
		}
	}
}
