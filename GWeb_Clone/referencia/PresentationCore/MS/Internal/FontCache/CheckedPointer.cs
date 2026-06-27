using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.PresentationCore;

namespace MS.Internal.FontCache
{
	// Token: 0x02000779 RID: 1913
	[FriendAccessAllowed]
	internal struct CheckedPointer
	{
		// Token: 0x06005088 RID: 20616 RVA: 0x0014280C File Offset: 0x00141C0C
		[SecurityCritical]
		internal unsafe CheckedPointer(void* pointer, int size)
		{
			this._pointer = pointer;
			this._size = size;
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00142828 File Offset: 0x00141C28
		[SecurityCritical]
		internal unsafe CheckedPointer(UnmanagedMemoryStream stream)
		{
			this._pointer = (void*)stream.PositionPointer;
			long length = stream.Length;
			if (length < 0L || length > 2147483647L)
			{
				throw new ArgumentOutOfRangeException();
			}
			this._size = (int)length;
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x00142864 File Offset: 0x00141C64
		internal bool IsNull
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._pointer == null;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x0014287C File Offset: 0x00141C7C
		internal int Size
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x00142890 File Offset: 0x00141C90
		[SecurityCritical]
		internal byte[] ToArray()
		{
			byte[] array = new byte[this._size];
			if (this._pointer == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			Marshal.Copy((IntPtr)this._pointer, array, 0, this.Size);
			return array;
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x001428D4 File Offset: 0x00141CD4
		[SecurityCritical]
		internal unsafe void CopyTo(CheckedPointer dest)
		{
			if (this._pointer == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			byte* pointer = (byte*)this._pointer;
			byte* ptr = (byte*)dest.Probe(0, this._size);
			for (int i = 0; i < this._size; i++)
			{
				ptr[i] = pointer[i];
			}
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x00142920 File Offset: 0x00141D20
		[SecurityCritical]
		internal unsafe int OffsetOf(void* pointer)
		{
			long num = (long)((byte*)pointer - (byte*)this._pointer);
			if (num < 0L || num > (long)this._size || this._pointer == null || pointer == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (int)num;
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00142960 File Offset: 0x00141D60
		[SecurityCritical]
		internal int OffsetOf(CheckedPointer pointer)
		{
			return this.OffsetOf(pointer._pointer);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0014297C File Offset: 0x00141D7C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe static CheckedPointer operator +(CheckedPointer rhs, int offset)
		{
			if (offset < 0 || offset > rhs._size || rhs._pointer == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			rhs._pointer = (void*)((byte*)rhs._pointer + offset);
			rhs._size -= offset;
			return rhs;
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x001429C4 File Offset: 0x00141DC4
		[SecurityCritical]
		internal unsafe void* Probe(int offset, int length)
		{
			if (this._pointer == null || offset < 0 || offset > this._size || offset + length > this._size || offset + length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (void*)((byte*)this._pointer + offset);
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x00142A08 File Offset: 0x00141E08
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe CheckedPointer CheckedProbe(int offset, int length)
		{
			if (this._pointer == null || offset < 0 || offset > this._size || offset + length > this._size || offset + length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new CheckedPointer((void*)((byte*)this._pointer + offset), length);
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00142A54 File Offset: 0x00141E54
		[SecurityCritical]
		internal void SetSize(int newSize)
		{
			this._size = newSize;
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00142A68 File Offset: 0x00141E68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal bool PointerEquals(CheckedPointer pointer)
		{
			return this._pointer == pointer._pointer;
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00142A84 File Offset: 0x00141E84
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void WriteBool(bool value)
		{
			*(byte*)this.Probe(0, 1) = (value ? 1 : 0);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x00142A9C File Offset: 0x00141E9C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe bool ReadBool()
		{
			return *(byte*)this.Probe(0, 1) != 0;
		}

		// Token: 0x040024B7 RID: 9399
		[SecurityCritical]
		private unsafe void* _pointer;

		// Token: 0x040024B8 RID: 9400
		[SecurityCritical]
		private int _size;
	}
}
