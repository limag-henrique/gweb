using System;

namespace MS.Internal
{
	// Token: 0x0200067C RID: 1660
	internal class Pair
	{
		// Token: 0x0600493C RID: 18748 RVA: 0x0011DDAC File Offset: 0x0011D1AC
		public Pair(object first, object second)
		{
			this._first = first;
			this._second = second;
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x0011DDD0 File Offset: 0x0011D1D0
		public object First
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x0011DDE4 File Offset: 0x0011D1E4
		public object Second
		{
			get
			{
				return this._second;
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x0011DDF8 File Offset: 0x0011D1F8
		public override int GetHashCode()
		{
			return ((this._first == null) ? 0 : this._first.GetHashCode()) ^ ((this._second == null) ? 0 : this._second.GetHashCode());
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x0011DE34 File Offset: 0x0011D234
		public override bool Equals(object o)
		{
			Pair pair = o as Pair;
			if (pair == null || !((this._first != null) ? this._first.Equals(pair._first) : (pair._first == null)))
			{
				return false;
			}
			if (this._second == null)
			{
				return pair._second == null;
			}
			return this._second.Equals(pair._second);
		}

		// Token: 0x04001CCE RID: 7374
		private object _first;

		// Token: 0x04001CCF RID: 7375
		private object _second;
	}
}
