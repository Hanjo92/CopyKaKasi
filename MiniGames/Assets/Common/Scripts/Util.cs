using UnityEngine;

namespace Almond
{
	public static class Util
	{
		public static Color HexToColor(string hexcoord)
		{
			if(hexcoord == null || hexcoord.Length < 6)
				return Color.white;

			var result = Color.white;
			for(int i = 0; i < 3; i++)
			{
				var hexString = hexcoord.Substring(2 * i, 2);
				var num = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
				result[i] = num / 255f;
			}

			return result;
		}

		public static void CalcLayoutPositions(float[] positions, int count, float interval)
		{
			if(positions == null || positions.Length != count)
				return;

			var position = -interval;
			position *= (count % 2 == 0) ? ((count / 2) - 0.5f) : (count / 2);

			for(int i = 0; i < count; i++)
			{
				positions[i] = position;
				position += interval;
			}
		}
	}
}