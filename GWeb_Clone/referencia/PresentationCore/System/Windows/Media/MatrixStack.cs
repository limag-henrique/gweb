using System;
using MS.Internal;

namespace System.Windows.Media
{
	// Token: 0x02000420 RID: 1056
	internal class MatrixStack
	{
		// Token: 0x06002AD0 RID: 10960 RVA: 0x000AB518 File Offset: 0x000AA918
		public MatrixStack()
		{
			this._items = new Matrix[MatrixStack.s_initialSize];
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000AB53C File Offset: 0x000AA93C
		private void EnsureCapacity()
		{
			if (this._size == this._items.Length)
			{
				Matrix[] array = new Matrix[MatrixStack.s_growFactor * this._size];
				Array.Copy(this._items, array, this._size);
				this._items = array;
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000AB584 File Offset: 0x000AA984
		public void Push(ref Matrix matrix, bool combine)
		{
			this.EnsureCapacity();
			if (combine && this._size > 0)
			{
				this._items[this._size] = matrix;
				MatrixUtil.MultiplyMatrix(ref this._items[this._size], ref this._items[this._size - 1]);
			}
			else
			{
				this._items[this._size] = matrix;
			}
			this._size++;
			this._highWaterMark = Math.Max(this._highWaterMark, this._size);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000AB624 File Offset: 0x000AAA24
		public void Push(Transform transform, bool combine)
		{
			this.EnsureCapacity();
			if (combine && this._size > 0)
			{
				transform.MultiplyValueByMatrix(ref this._items[this._size], ref this._items[this._size - 1]);
			}
			else
			{
				this._items[this._size] = transform.Value;
			}
			this._size++;
			this._highWaterMark = Math.Max(this._highWaterMark, this._size);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000AB6AC File Offset: 0x000AAAAC
		public void Push(Vector offset, bool combine)
		{
			this.EnsureCapacity();
			if (combine && this._size > 0)
			{
				this._items[this._size] = this._items[this._size - 1];
			}
			else
			{
				this._items[this._size] = Matrix.Identity;
			}
			MatrixUtil.PrependOffset(ref this._items[this._size], offset.X, offset.Y);
			this._size++;
			this._highWaterMark = Math.Max(this._highWaterMark, this._size);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000AB750 File Offset: 0x000AAB50
		public void Pop()
		{
			this._size--;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000AB76C File Offset: 0x000AAB6C
		public Matrix Peek()
		{
			return this._items[this._size - 1];
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000AB78C File Offset: 0x000AAB8C
		public bool IsEmpty
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000AB7A4 File Offset: 0x000AABA4
		public void Optimize()
		{
			if (this._observeCount == MatrixStack.s_trimCount)
			{
				int num = Math.Max(this._highWaterMark, MatrixStack.s_initialSize);
				if (num * MatrixStack.s_shrinkFactor <= this._items.Length)
				{
					this._items = new Matrix[num];
				}
				this._highWaterMark = 0;
				this._observeCount = 0;
				return;
			}
			this._observeCount++;
		}

		// Token: 0x04001397 RID: 5015
		private Matrix[] _items;

		// Token: 0x04001398 RID: 5016
		private int _size;

		// Token: 0x04001399 RID: 5017
		private static readonly int s_initialSize = 40;

		// Token: 0x0400139A RID: 5018
		private static readonly int s_growFactor = 2;

		// Token: 0x0400139B RID: 5019
		private static readonly int s_shrinkFactor = MatrixStack.s_growFactor + 1;

		// Token: 0x0400139C RID: 5020
		private int _highWaterMark;

		// Token: 0x0400139D RID: 5021
		private int _observeCount;

		// Token: 0x0400139E RID: 5022
		private static readonly int s_trimCount = 10;
	}
}
