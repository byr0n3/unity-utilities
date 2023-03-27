using System.Runtime.CompilerServices;
using UnityEngine;

namespace Byrone.Utilities.Extensions
{
	public static class UnityExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Contains(this LayerMask mask, int layer) =>
			mask == (mask | (1 << layer));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InLayerMask(this GameObject obj, LayerMask mask) =>
			mask.Contains(obj.layer);
	}
}
