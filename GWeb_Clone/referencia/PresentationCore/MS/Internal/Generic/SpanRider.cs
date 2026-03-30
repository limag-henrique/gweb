using System;

namespace MS.Internal.Generic
{
	// Token: 0x020006A9 RID: 1705
	internal struct SpanRider<T>
	{
		// Token: 0x06004AA2 RID: 19106 RVA: 0x0012323C File Offset: 0x0012263C
		internal SpanRider(SpanVector<T> vector)
		{
			this._defaultSpan = new Span<T>(vector.DefaultValue, int.MaxValue);
			this._vector = vector;
			this._current = 0;
			this._cp = 0;
			this._dcp = 0;
			this._cch = 0;
			this.At(0);
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0012328C File Offset: 0x0012268C
		internal bool At(int cp)
		{
			if (cp < this._dcp)
			{
				this._cp = (this._dcp = (this._current = (this._cch = 0)));
			}
			Span<T> span = default(Span<T>);
			while (this._current < this._vector.Count && this._dcp + (span = this._vector[this._current]).Length <= cp)
			{
				this._dcp += span.Length;
				this._current++;
			}
			if (this._current < this._vector.Count)
			{
				this._cch = this._vector[this._current].Length - cp + this._dcp;
				this._cp = cp;
				return true;
			}
			this._cch = this._defaultSpan.Length;
			this._cp = Math.Min(cp, this._dcp);
			return false;
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x0012338C File Offset: 0x0012278C
		internal int CurrentSpanStart
		{
			get
			{
				return this._dcp;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x001233A0 File Offset: 0x001227A0
		internal int Length
		{
			get
			{
				return this._cch;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x001233B4 File Offset: 0x001227B4
		internal int CurrentPosition
		{
			get
			{
				return this._cp;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x001233C8 File Offset: 0x001227C8
		internal T CurrentValue
		{
			get
			{
				if (this._current < this._vector.Count)
				{
					return this._vector[this._current].Value;
				}
				return this._defaultSpan.Value;
			}
		}

		// Token: 0x04001F96 RID: 8086
		private const int MaxCch = 2147483647;

		// Token: 0x04001F97 RID: 8087
		private SpanVector<T> _vector;

		// Token: 0x04001F98 RID: 8088
		private Span<T> _defaultSpan;

		// Token: 0x04001F99 RID: 8089
		private int _current;

		// Token: 0x04001F9A RID: 8090
		private int _cp;

		// Token: 0x04001F9B RID: 8091
		private int _dcp;

		// Token: 0x04001F9C RID: 8092
		private int _cch;
	}
}
