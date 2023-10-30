using UnityEngine;

namespace RCCopy
{
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
}