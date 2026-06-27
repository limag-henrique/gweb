using System;
using System.Collections;

namespace MS.Internal
{
	// Token: 0x02000694 RID: 1684
	internal sealed class SpanEnumerator : IEnumerator
	{
		// Token: 0x06004A03 RID: 18947 RVA: 0x0012004C File Offset: 0x0011F44C
		internal SpanEnumerator(SpanVector spans)
		{
			this._spans = spans;
			this._current = -1;
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x00120070 File Offset: 0x0011F470
		public object Current
		{
			get
			{
				return this._spans[this._current];
			}
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x00120090 File Offset: 0x0011F490
		public bool MoveNext()
		{
			this._current++;
			return this._current < this._spans.Count;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x001200C4 File Offset: 0x0011F4C4
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04001EE9 RID: 7913
		private SpanVector _spans;

		// Token: 0x04001EEA RID: 7914
		private int _current;
	}
}
