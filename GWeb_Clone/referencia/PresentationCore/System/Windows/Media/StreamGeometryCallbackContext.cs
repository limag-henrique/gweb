using System;

namespace System.Windows.Media
{
	// Token: 0x02000440 RID: 1088
	internal class StreamGeometryCallbackContext : ByteStreamGeometryContext
	{
		// Token: 0x06002C69 RID: 11369 RVA: 0x000B17E4 File Offset: 0x000B0BE4
		internal StreamGeometryCallbackContext(StreamGeometry owner)
		{
			this._owner = owner;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000B1800 File Offset: 0x000B0C00
		protected override void CloseCore(byte[] data)
		{
			this._owner.Close(data);
		}

		// Token: 0x0400144C RID: 5196
		private StreamGeometry _owner;
	}
}
