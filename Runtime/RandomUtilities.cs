using UnityEngine;

namespace Byrone.Utilities
{
	public static class RandomUtilities
	{
		/// <summary>
		/// Returns a random integer between the min and max
		/// </summary>
		/// <param name="min">Minimum (inclusive)</param>
		/// <param name="max">Maximum (inclusive)</param>
		/// <returns>Random value between <see cref="min"/> and <see cref="max"/></returns>
		public static int Range(int min, int max) =>
			// @todo Optimize
			Random.Range(min, max + 1);

		/// <summary>
		/// Returns a random float between the min and max
		/// </summary>
		/// <param name="min">Minimum (inclusive)</param>
		/// <param name="max">Maximum (inclusive)</param>
		/// <returns>Random value between <see cref="min"/> and <see cref="max"/></returns>
		public static float Range(float min, float max) =>
			// @todo Optimize 
			Random.Range(min, max);

		/// <summary>
		/// Returns a random bool
		/// </summary>
		public static bool CoinFlip() =>
			Random.Range(0, 2) is 1;
	}
}
