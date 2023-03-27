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
		/// <typeparam name="T">Type of the array elements</typeparam>
		/// <returns>Random element</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Random<T>(this T[] array, Random rnd = new Random()) =>
			// @todo Optimize 
			array[rnd.NextInt(0, array.Length)];

		public static void Shuffle<T>(this List<T> list, Random rnd = new Random())
		{
			var n = list.Count - 1;

			while (n > 1)
			{
				var k = rnd.NextInt(n);

				(list[k], list[n]) = (list[n], list[k]);

				n--;
			}
		}
	}
}
