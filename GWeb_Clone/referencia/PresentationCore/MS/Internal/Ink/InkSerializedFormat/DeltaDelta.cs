using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007DA RID: 2010
	internal class DeltaDelta : DataXform
	{
		// Token: 0x06005495 RID: 21653 RVA: 0x0015BEF0 File Offset: 0x0015B2F0
		internal DeltaDelta()
		{
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0015BF04 File Offset: 0x0015B304
		internal override void Transform(int data, ref int xfData, ref int extra)
		{
			long num = (long)data + this._d_i_2 - (this._d_i_1 << 1);
			this._d_i_2 = this._d_i_1;
			this._d_i_1 = (long)data;
			if (2147483647L >= MathHelper.AbsNoThrow(num))
			{
				extra = 0;
				xfData = (int)num;
				return;
			}
			long num2 = MathHelper.AbsNoThrow(num);
			extra = (int)(num2 >> 32);
			extra = (extra << 1 | ((num < 0L) ? 1 : 0));
			xfData = (int)((ulong)-1 & (ulong)num2);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0015BF74 File Offset: 0x0015B374
		internal override void ResetState()
		{
			this._d_i_1 = 0L;
			this._d_i_2 = 0L;
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0015BF94 File Offset: 0x0015B394
		internal override int InverseTransform(int xfData, int extra)
		{
			long num;
			if (extra != 0)
			{
				bool flag = (extra & 1) != 0;
				num = ((long)extra >> 1 << 32 | (long)((ulong)-1 & (ulong)((long)xfData)));
				num = (flag ? (-num) : num);
			}
			else
			{
				num = (long)xfData;
			}
			long num2 = num - this._d_i_2 + (this._d_i_1 << 1);
			this._d_i_2 = this._d_i_1;
			this._d_i_1 = num2;
			return (int)num2;
		}

		// Token: 0x04002623 RID: 9763
		private long _d_i_1;

		// Token: 0x04002624 RID: 9764
		private long _d_i_2;
	}
}
