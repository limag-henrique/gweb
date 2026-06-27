using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D6 RID: 2006
	internal class TransformDescriptor
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x0015B7AC File Offset: 0x0015ABAC
		// (set) Token: 0x0600547D RID: 21629 RVA: 0x0015B7C0 File Offset: 0x0015ABC0
		public KnownTagCache.KnownTagIndex Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x0015B7D4 File Offset: 0x0015ABD4
		// (set) Token: 0x0600547F RID: 21631 RVA: 0x0015B7E8 File Offset: 0x0015ABE8
		public uint Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06005480 RID: 21632 RVA: 0x0015B7FC File Offset: 0x0015ABFC
		public double[] Transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0015B810 File Offset: 0x0015AC10
		public bool Compare(TransformDescriptor that)
		{
			if (that.Tag != this.Tag)
			{
				return false;
			}
			if (that.Size == this._size)
			{
				int num = 0;
				while ((long)num < (long)((ulong)this._size))
				{
					if (!DoubleUtil.AreClose(that.Transform[num], this._transform[num]))
					{
						return false;
					}
					num++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x04002614 RID: 9748
		private double[] _transform = new double[6];

		// Token: 0x04002615 RID: 9749
		private uint _size;

		// Token: 0x04002616 RID: 9750
		private KnownTagCache.KnownTagIndex _tag;
	}
}
