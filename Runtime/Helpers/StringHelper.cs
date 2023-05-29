using System.Runtime.CompilerServices;

namespace Byrone.Utilities.Helpers
{
	internal static partial class StringHelper
	{
		static StringHelper()
		{
			StringHelper.BuildPowTable();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFormat(int value, System.Span<byte> span, out int length)
		{
			length = 0;

			if (span.Length == 0)
			{
				return false;
			}

			if (value < 0)
			{
				span[length++] = Ascii.Hyphen;
				value = -value;
			}

			if (!StringHelper.TryFormat((uint)value, span.Slice(length), out var i))
			{
				return false;
			}

			length += i;

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFormat(float value, System.Span<byte> span, byte scale, out int length)
		{
			length = 0;

			if (span.Length == 0)
			{
				return false;
			}

			if (float.IsNegative(value))
			{
				span[length++] = Ascii.Hyphen;
				value = -value;
			}

			var fractional = StringHelper.ModF(value, out var integral);

			if (!StringHelper.TryFormat((uint)integral, span.Slice(length), out var i))
			{
				return false;
			}

			length += i;

			if (fractional == 0f)
			{
				return true;
			}

			span[length++] = Ascii.Dot;

			var mult = StringHelper.Pow[scale];
			var rest = (1f + fractional) * mult;

			if (!StringHelper.TryFormatCore((uint)rest, span.Slice(length), 1, out var f))
			{
				return false;
			}

			length += f;

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFormat(uint value, System.Span<byte> span, out int length)
		{
			if (value >= 10)
			{
				return StringHelper.TryFormatCore(value, span, 0, out length);
			}

			span[0] = Ascii.ToByte((byte)value);
			length = 1;

			return true;
		}

		private static unsafe bool TryFormatCore(uint value, System.Span<byte> span, byte shift, out int length)
		{
			length = StringHelper.CountDigits(value) - shift;

			if (length > span.Length)
			{
				return false;
			}

			fixed (byte* dst = span)
			{
				var p = dst + length;

				do
				{
					value = StringHelper.DivRem(value, 10u, out var remainder);

					*--p = Ascii.ToByte((byte)remainder);
				} while (value != shift);
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint DivRem(uint left, uint right, out uint remainder)
		{
			var num = left / right;

			remainder = left - num * right;

			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ModF(float value, out int integral)
		{
			var truncate = (float)System.Math.Truncate(value);

			integral = (int)truncate;

			return value - truncate;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int CountDigits(uint value)
		{
			var digits = 1;

			// ReSharper disable once InvertIf
			if (value >= 100000)
			{
				value /= 100000;
				digits += 5;
			}

			return StringHelper.CountDigits(value, digits);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int CountDigits(uint value, int digits)
		{
			// ReSharper disable once InvertIf
			if (value >= 10)
			{
				// ReSharper disable once ConvertIfStatementToSwitchStatement
				if (value < 100)
				{
					digits += 1;
				}
				else if (value < 1000)
				{
					digits += 2;
				}
				else if (value < 10000)
				{
					digits += 3;
				}
				else
				{
					digits += 4;
				}
			}

			return digits;
		}
	}
}
