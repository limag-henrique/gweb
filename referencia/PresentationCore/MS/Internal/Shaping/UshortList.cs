using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006EA RID: 1770
	internal class UshortList
	{
		// Token: 0x06004C59 RID: 19545 RVA: 0x0012B7C8 File Offset: 0x0012ABC8
		internal UshortList(int capacity, int leap)
		{
			Invariant.Assert(capacity >= 0 && leap >= 0, "Invalid parameter");
			this._storage = new UshortArray(capacity, leap);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0012B800 File Offset: 0x0012AC00
		internal UshortList(ushort[] array)
		{
			Invariant.Assert(array != null, "Invalid parameter");
			this._storage = new UshortArray(array);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0012B830 File Offset: 0x0012AC30
		internal UshortList(CheckedUShortPointer unsafeArray, int arrayLength)
		{
			this._storage = new UnsafeUshortArray(unsafeArray, arrayLength);
			this._length = arrayLength;
		}

		// Token: 0x17000FA6 RID: 4006
		public ushort this[int index]
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				Invariant.Assert(index >= 0 && index < this._length, "Index out of range");
				return this._storage[this._index + index];
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				Invariant.Assert(index >= 0 && index < this._length, "Index out of range");
				this._storage[this._index + index] = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x0012B8D0 File Offset: 0x0012ACD0
		// (set) Token: 0x06004C5F RID: 19551 RVA: 0x0012B8E4 File Offset: 0x0012ACE4
		public int Length
		{
			get
			{
				return this._length;
			}
			[SecurityCritical]
			set
			{
				this._length = value;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06004C60 RID: 19552 RVA: 0x0012B8F8 File Offset: 0x0012ACF8
		public int Offset
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0012B90C File Offset: 0x0012AD0C
		public void SetRange(int index, int length)
		{
			Invariant.Assert(length >= 0 && index + length <= this._storage.Length, "List out of storage");
			this._index = index;
			this._length = length;
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0012B94C File Offset: 0x0012AD4C
		public void Insert(int index, int count)
		{
			Invariant.Assert(index <= this._length && index >= 0, "Index out of range");
			Invariant.Assert(count > 0, "Invalid argument");
			this._storage.Insert(this._index + index, count, this._index + this._length);
			this._length += count;
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0012B9B4 File Offset: 0x0012ADB4
		public void Remove(int index, int count)
		{
			Invariant.Assert(index < this._length && index >= 0, "Index out of range");
			Invariant.Assert(count > 0 && index + count <= this._length, "Invalid argument");
			this._storage.Remove(this._index + index, count, this._index + this._length);
			this._length -= count;
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0012BA2C File Offset: 0x0012AE2C
		public ushort[] ToArray()
		{
			return this._storage.ToArray();
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0012BA44 File Offset: 0x0012AE44
		public ushort[] GetCopy()
		{
			return this._storage.GetSubsetCopy(this._index, this._length);
		}

		// Token: 0x04002132 RID: 8498
		private UshortBuffer _storage;

		// Token: 0x04002133 RID: 8499
		private int _index;

		// Token: 0x04002134 RID: 8500
		private int _length;
	}
}
