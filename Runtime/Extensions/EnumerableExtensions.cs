using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Byrone.Utilities.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Get a random element from the array
		/// </summary>
		/// <param name="array">The array instance</param>
		/// <param name="rnd">The random instance</param>
		/// <typeparam name="T">Type of the array elements</typeparam>
		/// <returns>Random element</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Random<T>(this T[] array, Random rnd) =>
			// @todo Optimize 
			array[rnd.NextInt(0, array.Length)];

		/// <summary>
		/// Shuffle the list contents
		/// </summary>
		/// <param name="list">The list instance</param>
		/// <param name="rnd">The random instance</param>
		/// <typeparam name="T">Type of the list elements</typeparam>
		public static void Shuffle<T>(this List<T> list, ref Random rnd)
		{
			var n = list.Count - 1;

			while (n > 1)
			{
				var k = rnd.NextInt(n);

				(list[k], list[n]) = (list[n], list[k]);

				n--;
			}
		}

		/// <summary>
		/// Shuffle the array contents
		/// </summary>
		/// <param name="list">The array instance</param>
		/// <param name="rnd">The random instance</param>
		/// <typeparam name="T">Type of the list elements</typeparam>
		public static void Shuffle<T>(this T[] list, ref Random rnd)
		{
			var n = list.Length - 1;

			while (n > 1)
			{
				var k = rnd.NextInt(n);

				(list[k], list[n]) = (list[n], list[k]);

				n--;
			}
		}
	}
}
