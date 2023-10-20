using UnityEngine;
using UnityEngine.UIElements;

public static class Util
{
	public static void CalcLayoutPositions(float[] positions, int count, float interval)
	{
		if(positions == null || positions.Length != count) return;

		var position = -interval;
		position *= (count % 2 == 0) ? ((count / 2) - 0.5f) : (count / 2);

		for(int i = 0; i < count; i++)
		{
			positions[i] = position;
			position += interval;
		}
	}

}