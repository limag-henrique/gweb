using System;
using System.Security;

namespace MS.Internal.TtfDelta
{
	// Token: 0x02000003 RID: 3
	internal class ControlTableInit
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00006770 File Offset: 0x00005B70
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe static void Init()
		{
			if (!ControlTableInit._isInitialized)
			{
				lock (ControlTableInit._staticLock)
				{
					if (!ControlTableInit._isInitialized)
					{
						<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table = ref <Module>.??_C@_04NEODDMOL@head@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 8) = 54;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 16) = ref <Module>.MS.Internal.TtfDelta.HEAD_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 24) = ref <Module>.??_C@_04FMPHLIKP@hhea@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 32) = 36;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 40) = ref <Module>.MS.Internal.TtfDelta.HHEA_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 48) = ref <Module>.??_C@_04IDCHJBEM@vhea@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 56) = 36;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 64) = ref <Module>.MS.Internal.TtfDelta.VHEA_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 72) = ref <Module>.??_C@_04KODIGLGG@maxp@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 80) = 32;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 88) = ref <Module>.MS.Internal.TtfDelta.MAXP_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 96) = ref <Module>.??_C@_04LOKOGFID@post@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 104) = 32;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 112) = ref <Module>.MS.Internal.TtfDelta.POST_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 120) = ref <Module>.??_C@_04OEKHDCJK@OS?12@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 128) = 80;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 136) = ref <Module>.MS.Internal.TtfDelta.OS2_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 144) = ref <Module>.??_C@_04OEKHDCJK@OS?12@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 152) = 88;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 160) = ref <Module>.MS.Internal.TtfDelta.NEWOS2_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 168) = ref <Module>.??_C@_04OEKHDCJK@OS?12@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 176) = 98;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 184) = ref <Module>.MS.Internal.TtfDelta.VERSION2OS2_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 192) = ref <Module>.??_C@_04IDDCPPLH@hdmx@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 200) = 8;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 208) = ref <Module>.MS.Internal.TtfDelta.HDMX_CONTROL;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 216) = ref <Module>.??_C@_04LGOLHALL@LTSH@;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 224) = 4;
						*(ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 232) = ref <Module>.MS.Internal.TtfDelta.LTSH_CONTROL;
						ControlTableInit._isInitialized = true;
					}
				}
			}
		}

		// Token: 0x040002F3 RID: 755
		private static object _staticLock = new object();

		// Token: 0x040002F4 RID: 756
		private static bool _isInitialized = false;
	}
}
