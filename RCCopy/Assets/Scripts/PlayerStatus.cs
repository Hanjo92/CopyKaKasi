using System.Collections.Generic;

public class PlayerStatus
{
	private float currentHP;
	private Dictionary<SkillType, Skill> skillSet;
	public PlayerStatus()
	{
		currentHP = Player.Inst.CastleHP;
		skillSet = new Dictionary<SkillType, Skill>();
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