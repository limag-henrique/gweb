using System;

namespace MS.Internal
{
	// Token: 0x02000695 RID: 1685
	internal struct SpanPosition
	{
		// Token: 0x06004A07 RID: 18951 RVA: 0x001200D8 File Offset: 0x0011F4D8
		internal SpanPosition(int spanIndex, int spanCP)
		{
			this._spanIndex = spanIndex;
			this._spanCP = spanCP;
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x001200F4 File Offset: 0x0011F4F4
		internal int Index
		{
			get
			{
				return this._spanIndex;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x00120108 File Offset: 0x0011F508
		internal int CP
		{
			get
			{
				return this._spanCP;
			}
		}

		// Token: 0x04001EEB RID: 7915
		private int _spanIndex;

		// Token: 0x04001EEC RID: 7916
		private int _spanCP;
	}
}
