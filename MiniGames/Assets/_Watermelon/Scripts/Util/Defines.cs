using UnityEngine;

namespace Watermelon
{
	public static class Defines
	{
		public static Vector2[] Directions = {
			new Vector2(1, 1),
			new Vector2(1, -1),
			new Vector2(-1, -1),
			new Vector2(-1, 1),
		};

		public const float FruitDefaultSize = 0.3f;
		public const float FruitScaleStep = 0.3f;
		public const float DropHeight = 3;
		public static Vector2 DropRange = new Vector2(0.425f, 6.575f);

		public const string HighestScoreKey = "SavedHighScore";
		public const float ScaleTime = 0.1f;
	}

	public static class FruitColor
	{
		private static string[] Colors = {
			"FB3A22",
			"F86D4A",
			"A26AFF",
			"FFB602",
			"FE8B1B",
			"D80506",
			"FDEF7E",
			"FDCAC4",
			"F4E905",
			"8ACF18",
			"0E680C",
		};
		public static Color GetColor(int level) => Colors == null ? Color.white : Almond.Util.HexToColor(Colors[level % Colors.Length]);
	}
	public static class FruitName
	{
		private static string[] Names =	{
			"Cherry",
			"Strawberry",
			"Grape",
			"Mandarin",
			"Persimmon",
			"Apple",
			"Pear",
			"Peach",
			"Pineapple",
			"Melon",
			"Watermelon",
		};
		public static string GetName(int level) => Names == null ? "" : Names[level % Names.Length];
	}
}