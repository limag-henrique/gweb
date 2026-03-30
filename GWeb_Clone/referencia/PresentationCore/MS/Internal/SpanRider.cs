using System;

namespace MS.Internal
{
	// Token: 0x02000696 RID: 1686
	internal struct SpanRider
	{
		// Token: 0x06004A0A RID: 18954 RVA: 0x0012011C File Offset: 0x0011F51C
		public SpanRider(SpanVector spans)
		{
			this = new SpanRider(spans, default(SpanPosition), 0);
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0012013C File Offset: 0x0011F53C
		public SpanRider(SpanVector spans, SpanPosition latestPosition)
		{
			this = new SpanRider(spans, latestPosition, latestPosition.CP);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00120158 File Offset: 0x0011F558
		public SpanRider(SpanVector spans, SpanPosition latestPosition, int cp)
		{
			this._spans = spans;
			this._spanPosition = default(SpanPosition);
			this._cp = 0;
			this._cch = 0;
			this.At(latestPosition, cp);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00120190 File Offset: 0x0011F590
		public bool At(int cp)
		{
			return this.At(this._spanPosition, cp);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x001201AC File Offset: 0x0011F5AC
		public bool At(SpanPosition latestPosition, int cp)
		{
			bool flag = this._spans.FindSpan(cp, latestPosition, out this._spanPosition);
			if (flag)
			{
				this._cch = this._spans[this._spanPosition.Index].length - (cp - this._spanPosition.CP);
				this._cp = cp;
			}
			else
			{
				this._cch = int.MaxValue;
				this._cp = this._spanPosition.CP;
			}
			return flag;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x00120228 File Offset: 0x0011F628
		public int CurrentSpanStart
		{
			get
			{
				return this._spanPosition.CP;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x00120240 File Offset: 0x0011F640
		public int Length
		{
			get
			{
				return this._cch;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004A11 RID: 18961 RVA: 0x00120254 File Offset: 0x0011F654
		public int CurrentPosition
		{
			get
			{
				return this._cp;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x00120268 File Offset: 0x0011F668
		public object CurrentElement
		{
			get
			{
				if (this._spanPosition.Index < this._spans.Count)
				{
					return this._spans[this._spanPosition.Index].element;
				}
				return this._spans.Default;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x001202B4 File Offset: 0x0011F6B4
		public int CurrentSpanIndex
		{
			get
			{
				return this._spanPosition.Index;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x001202CC File Offset: 0x0011F6CC
		public SpanPosition SpanPosition
		{
			get
			{
				return this._spanPosition;
			}
		}

		// Token: 0x04001EED RID: 7917
		private SpanVector _spans;

		// Token: 0x04001EEE RID: 7918
		private SpanPosition _spanPosition;

		// Token: 0x04001EEF RID: 7919
		private int _cp;

		// Token: 0x04001EF0 RID: 7920
		private int _cch;
	}
}
