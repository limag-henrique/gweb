using System;

namespace System.Windows.Input.StylusPlugIns
{
	// Token: 0x020002F8 RID: 760
	internal class RawStylusInputCustomData
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x000615D0 File Offset: 0x000609D0
		public RawStylusInputCustomData(StylusPlugIn owner, object data)
		{
			this._data = data;
			this._owner = owner;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x000615F4 File Offset: 0x000609F4
		public object Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00061608 File Offset: 0x00060A08
		public StylusPlugIn Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x04000D30 RID: 3376
		private StylusPlugIn _owner;

		// Token: 0x04000D31 RID: 3377
		private object _data;
	}
}
