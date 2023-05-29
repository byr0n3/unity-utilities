using System.Runtime.CompilerServices;

namespace Byrone.Utilities.Helpers
{
	internal static partial class StringHelper
	{
		private const byte powTable = 64;

		public static readonly ulong[] Pow = new ulong[StringHelper.powTable];

		private static void BuildPowTable()
		{
			for (byte i = 0; i < StringHelper.powTable; i++)
			{
				var value = StringHelper.Pow10(i);

				StringHelper.Pow[i] = value;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Pow10(int value)
		{
			var result = 1ul;

			result <<= value;

			while (value-- != 0)
			{
				result += result << 2;
			}

			return result;
		}
	}
}
