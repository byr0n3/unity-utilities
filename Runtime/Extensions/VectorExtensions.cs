using System.Runtime.CompilerServices;
using UnityEngine;

namespace Byrone.Utilities.Extensions
{
	public static class Vector2Extensions
	{
		/// <summary>
		/// Returns if the vector is equal to <see cref="Vector2.zero"/>
		/// </summary>
		/// <param name="value">The vector</param>
		/// <returns>If the vector is equal to zero</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsZero(this Vector2 value) =>
			value.Equals(Vector2.zero);
	}

	public static class Vector3Extensions
	{
		/// <summary>
		/// Returns if the vector is equal to <see cref="Vector3.zero"/>
		/// </summary>
		/// <param name="value">The vector</param>
		/// <returns>If the vector is equal to zero</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsZero(this Vector3 value) =>
			value.Equals(Vector3.zero);
	}
}
