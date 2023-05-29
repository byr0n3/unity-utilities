using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Byrone.Utilities.Extensions
{
	public static class ArrayPoolExtensions
	{
		public static void Resize(this ArrayPool<byte> @this,
								  ref byte[] buffer,
								  int length,
								  int need,
								  int grow,
								  bool clear = false)
		{
			if (buffer.Length >= (length + need))
			{
				return;
			}

			if (need > grow)
			{
				grow = need;
			}

			var @new = @this.Rent(length + grow);

			var span = new System.Span<byte>(@new, 0, length + grow);
			var span2 = new System.ReadOnlySpan<byte>(buffer, 0, length);

			ref var dst = ref MemoryMarshal.GetReference(span);
			ref var src = ref MemoryMarshal.GetReference(span2);

			Unsafe.CopyBlockUnaligned(ref dst, ref src, (uint)length);

			@this.Return(buffer, clear);

			buffer = @new;
		}
	}
}
