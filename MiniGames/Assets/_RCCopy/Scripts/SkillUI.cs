using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RCCopy
{
	public class SkillUI : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private TextMeshProUGUI text;
		private SkillType skillType;

		private void Awake()
		{
			button.onClick.AddListener(() => GameController.Instance.AddSkill(skillType));
		}

		public void SetSkill(SkillType _skillType)
		{
			skillType = _skillType;
			text.text = skillType.ToString();
		}
	}
}