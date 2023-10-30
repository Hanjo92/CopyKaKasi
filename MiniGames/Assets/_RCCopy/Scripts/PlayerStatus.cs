using System.Collections.Generic;

namespace RCCopy
{
	public class PlayerStatus
	{
		private float currentHP;
		private Dictionary<SkillType, Skill> skillSet;
		public void Initialize()
		{
			currentHP = Player.Inst.CastleHP;
			if(skillSet == null)
			{
				skillSet = new Dictionary<SkillType, Skill>();
			}
			else
			{
				skillSet.Clear();
			}
		}
		public float GetCurrentHP() => currentHP;
		public void Attacked(float demage) => currentHP -= demage;
		public Dictionary<SkillType, Skill> GetSkillSet() => skillSet;
		public void AddSkill(SkillType skill)
		{
			if(skillSet.TryGetValue(skill, out var contained))
			{
				contained.AddStack();
			}
			else
			{
				skillSet.Add(skill, new Skill(SkillTBL.GetData(skill)));
			}
		}
	}
}