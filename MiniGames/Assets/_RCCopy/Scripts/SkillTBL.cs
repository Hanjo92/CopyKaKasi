namespace RCCopy
{
	public static class SkillTBL
	{
		public static SkillData[] skillDatas =
		{
			new SkillData(){ skillType = SkillType.DamegeUp, value = 0.1f},
			new SkillData(){ skillType = SkillType.CoolTime, value = 0.1f},
			new SkillData(){ skillType = SkillType.BulletSpeed, value = 0.1f},
			new SkillData(){ skillType = SkillType.AddBullet, value = 1f},
			new SkillData(){ skillType = SkillType.Penetrate, value = 1f},
		};

		public static SkillData GetData(SkillType skillType) => (skillType == SkillType.Max) ? null : skillDatas[(int)skillType];
	}
}