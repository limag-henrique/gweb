using System;

namespace MS.Win32.Recognizer
{
	// Token: 0x02000654 RID: 1620
	internal struct PACKET_DESCRIPTION
	{
		// Token: 0x04001BDE RID: 7134
		public uint cbPacketSize;

		// Token: 0x04001BDF RID: 7135
		public uint cPacketProperties;

		// Token: 0x04001BE0 RID: 7136
		public IntPtr pPacketProperties;

		// Token: 0x04001BE1 RID: 7137
		public uint cButtons;

		// Token: 0x04001BE2 RID: 7138
		public IntPtr pguidButtons;
	}
}
