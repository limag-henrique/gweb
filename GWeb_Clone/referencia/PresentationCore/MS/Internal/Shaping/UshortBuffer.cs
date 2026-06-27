using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006EB RID: 1771
	internal abstract class UshortBuffer
	{
		// Token: 0x17000FA9 RID: 4009
		public abstract ushort this[int index]
		{
			get;
			set;
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06004C68 RID: 19560
		public abstract int Length { get; }

		// Token: 0x06004C69 RID: 19561 RVA: 0x0012BA68 File Offset: 0x0012AE68
		public virtual ushort[] ToArray()
		{
			return null;
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0012BA78 File Offset: 0x0012AE78
		public virtual ushort[] GetSubsetCopy(int index, int count)
		{
			return null;
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0012BA88 File Offset: 0x0012AE88
		public virtual void Insert(int index, int count, int length)
		{
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0012BA98 File Offset: 0x0012AE98
		public virtual void Remove(int index, int count, int length)
		{
		}

		// Token: 0x04002135 RID: 8501
		protected int _leap;
	}
}
