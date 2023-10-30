using UnityEngine;

namespace Almond
{
	public static class UnityTime
	{
		public const float DefaultFixedTimeStep = 0.02f;
		public const float DefaultAllowedTimeStep = 0.3333333f;
		public const float DefaultParticleTimeStep = 0.03f;

		public static void SetAllTimeScale(float multiply)
		{
			SetTimeScale(multiply);
			SetFixedTimeScale(multiply);
			SetParticleTimeScale(multiply);
		}
		public static void SetTimeScale(float multiply)
		{
			Time.timeScale = multiply;
		}
		public static void SetFixedTimeScale(float multiply)
		{
			Time.fixedDeltaTime = DefaultFixedTimeStep * multiply;
		}
		public static void SetParticleTimeScale(float multiply)
		{
			Time.maximumParticleDeltaTime = DefaultParticleTimeStep * multiply;
		}
	}
}