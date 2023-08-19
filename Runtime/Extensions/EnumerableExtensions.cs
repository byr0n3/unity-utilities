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
		public static T Random<T>(this T[] array, ref Random rnd) =>
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
		/// <param name="array">The array instance</param>
		/// <param name="rnd">The random instance</param>
		/// <param name="length">Length of the array</param>
		/// <typeparam name="T">Type of the list elements</typeparam>
		public static void Shuffle<T>(this T[] array, ref Random rnd, int length = 0)
		{
			if (length == 0)
			{
				length = array.Length;
			}

			var n = length - 1;

			while (n > 1)
			{
				var k = rnd.NextInt(n);

				(array[k], array[n]) = (array[n], array[k]);

				n--;
			}
		}
	}
}
