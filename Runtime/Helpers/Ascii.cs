using System.Runtime.CompilerServices;

namespace Byrone.Utilities.Helpers
{
	internal static class Ascii
	{
		public const byte Zero = (byte)'0';
		public const byte Hyphen = (byte)'-';
		public const byte Dot = (byte)'.';

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ToByte(byte value) =>
			(byte)(Ascii.Zero + value);
	}
}
