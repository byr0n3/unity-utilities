using System.Buffers;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Byrone.Utilities.Extensions;
using Byrone.Utilities.Helpers;

namespace Byrone.Utilities.Utilities
{
	[StructLayout(LayoutKind.Sequential)]
	public struct StringBuilder2 : System.IDisposable
	{
		private const int defaultSize = 50;

		private static readonly Encoding defaultEncoding = Encoding.UTF8;

		private readonly Encoding encoding;

		private int position;
		private byte[] buffer;

		public static StringBuilder2 Get() =>
			new(StringBuilder2.defaultSize);

		public StringBuilder2(int size) : this(size, StringBuilder2.defaultEncoding)
		{
		}

		public StringBuilder2(Encoding encoding) : this(StringBuilder2.defaultSize, encoding)
		{
		}

		public StringBuilder2(int size, Encoding encoding)
		{
			this.position = 0;
			this.encoding = encoding;
			this.buffer = ArrayPool<byte>.Shared.Rent(size);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear() =>
			this.position = 0;

		public void Append(System.ReadOnlySpan<char> span)
		{
			var size = this.encoding.IsSingleByte ? span.Length : this.encoding.GetByteCount(span.ToArray());

			if (size == 0)
			{
				return;
			}

			this.encoding.GetBytes(span, this.Slice(size));

			this.Advance(size);
		}

		public void Append(System.ReadOnlySpan<byte> span)
		{
			var size = span.Length;

			if (size == 0)
			{
				return;
			}

			ref var dst = ref this.GetByteRefUnsafe(size);
			ref var src = ref MemoryMarshal.GetReference(span);

			Unsafe.CopyBlockUnaligned(ref dst, ref src, (uint)size);

			this.Advance(size);
		}

		public void Append(int @int)
		{
			if (StringHelper.TryFormat(@int, this.Slice(), out var length))
			{
				this.Advance(length);
			}
		}

		public void Append(float value, byte scale = 6)
		{
			if (StringHelper.TryFormat(value, this.Slice(), scale, out var length))
			{
				this.Advance(length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char @char) =>
			this.Append((byte)@char);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(byte @byte) =>
			this.Write(@byte, sizeof(byte));

		public void AppendBase64(System.ReadOnlySpan<byte> src, int srcLength = 0)
		{
			if (srcLength == 0)
			{
				srcLength = src.Length;
			}

			var length = Base64.GetMaxEncodedToUtf8Length(srcLength);

			Base64.EncodeToUtf8(src, this.Slice(length), out _, out var written);

			this.Advance(written);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void Write<T>(T value, int size) where T : unmanaged
		{
			Unsafe.WriteUnaligned(ref this.GetByteRefUnsafe(sizeof(T)), value);

			this.Advance(size);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Advance(int size) =>
			this.position += size;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureCapacity(int capacity) =>
			ArrayPool<byte>.Shared.Resize(ref this.buffer, this.position, capacity, StringBuilder2.defaultSize);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private System.Span<byte> Slice(int size = 32) =>
			this.Slice(this.position, size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private System.Span<byte> Slice(int start, int size)
		{
			this.EnsureCapacity(size);
			return new System.Span<byte>(this.buffer, start, size);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref byte GetByteRefUnsafe(int size)
		{
			this.EnsureCapacity(size);
			return ref this.GetByteRefUnsafe();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ref byte GetByteRefUnsafe() =>
			ref Unsafe.Add(ref MemoryMarshal.GetReference(this.Slice(0, 1)), this.position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose() =>
			ArrayPool<byte>.Shared.Return(this.buffer);

		[System.Obsolete("Alloc")]
		public override string ToString() =>
			this.encoding.GetString(this.buffer, 0, this.position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public System.ReadOnlySpan<byte> AsReadOnlySpan() =>
			this.Slice(0, this.position);

		public readonly ref byte this[int index] =>
			ref this.buffer[index];
	}
}
