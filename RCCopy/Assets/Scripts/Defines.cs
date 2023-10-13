using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class Defines
{
	public static int MonsterLayer => LayerMask.NameToLayer("Monster");

	public const float BaseCastleHP = 1500;
	public const float IncreaseHPAmount = 500;
	public const int IncreaseLevelCount = 5;

	public const float CastlePosition = 15f;

	public const float DefaultBulletDemage = 5f;
	public const float DefaultBulletSpeed = 20f;

	public const float MinimumCooltime = 0.05f;

	public static string PrefabPath(this string name) => $"Prefabs/{name}";
}

#region Enums
public enum SkillType
{
	DamegeUp,
	CoolTime,
	BulletSpeed,
	AddBullet,
	Penetrate,
	Max
}
#endregion