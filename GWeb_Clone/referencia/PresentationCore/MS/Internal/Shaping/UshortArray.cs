using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006EC RID: 1772
	internal class UshortArray : UshortBuffer
	{
		// Token: 0x06004C6E RID: 19566 RVA: 0x0012BABC File Offset: 0x0012AEBC
		internal UshortArray(ushort[] array)
		{
			this._array = array;
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0012BAD8 File Offset: 0x0012AED8
		internal UshortArray(int capacity, int leap)
		{
			this._array = new ushort[capacity];
			this._leap = leap;
		}

		// Token: 0x17000FAB RID: 4011
		public override ushort this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x0012BB30 File Offset: 0x0012AF30
		public override int Length
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0012BB48 File Offset: 0x0012AF48
		public override ushort[] ToArray()
		{
			return this._array;
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0012BB5C File Offset: 0x0012AF5C
		public override ushort[] GetSubsetCopy(int index, int count)
		{
			ushort[] array = new ushort[count];
			Buffer.BlockCopy(this._array, index * 2, array, 0, ((index + count <= this._array.Length) ? count : this._array.Length) * 2);
			return array;
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0012BB9C File Offset: 0x0012AF9C
		public override void Insert(int index, int count, int length)
		{
			int num = length + count;
			if (num > this._array.Length)
			{
				Invariant.Assert(this._leap > 0, "Growing an ungrowable list!");
				int num2 = num - this._array.Length;
				int num3 = this._array.Length + ((num2 - 1) / this._leap + 1) * this._leap;
				ushort[] array = new ushort[num3];
				Buffer.BlockCopy(this._array, 0, array, 0, index * 2);
				if (index < length)
				{
					Buffer.BlockCopy(this._array, index * 2, array, (index + count) * 2, (length - index) * 2);
				}
				this._array = array;
				return;
			}
			if (index < length)
			{
				Buffer.BlockCopy(this._array, index * 2, this._array, (index + count) * 2, (length - index) * 2);
			}
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0012BC58 File Offset: 0x0012B058
		public override void Remove(int index, int count, int length)
		{
			Buffer.BlockCopy(this._array, (index + count) * 2, this._array, index * 2, (length - index - count) * 2);
		}

		// Token: 0x04002136 RID: 8502
		private ushort[] _array;
	}
}
