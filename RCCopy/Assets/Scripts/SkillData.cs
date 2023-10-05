public class SkillData
{
	public SkillType skillType;
	public float value;
}

public class Skill
{
	public SkillType Type => data.skillType;
	public float Value => data.value * skillStack;

	public Skill(SkillData skillData)
	{
		data = skillData;
		skillStack = 1;
	}

	private int skillStack;
	private SkillData data;
	public void AddStack(int stack = 1) => skillStack += stack;
}