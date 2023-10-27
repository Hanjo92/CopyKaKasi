using System;
using UnityEngine;
using UnityEngine.Video;

public static class Util
{
	public static Color HexToColor(string hexcoord)
	{
		if(hexcoord == null || hexcoord.Length < 6) return Color.white;

		var result = Color.white;
		for(int i = 0; i < 3; i++)
		{
			var hexString = hexcoord.Substring(2 * i, 2);
			var num = int.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
			result[i] = num / 255f;
		}

		return result;
	}
}