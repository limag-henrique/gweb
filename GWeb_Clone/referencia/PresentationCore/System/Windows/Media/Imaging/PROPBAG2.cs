using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005D5 RID: 1493
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct PROPBAG2
	{
		// Token: 0x06004398 RID: 17304 RVA: 0x00107154 File Offset: 0x00106554
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Init(string name)
		{
			this.pstrName = Marshal.StringToCoTaskMemUni(name);
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x00107170 File Offset: 0x00106570
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Clear()
		{
			Marshal.FreeCoTaskMem(this.pstrName);
			this.pstrName = IntPtr.Zero;
		}

		// Token: 0x040018A1 RID: 6305
		internal uint dwType;

		// Token: 0x040018A2 RID: 6306
		internal ushort vt;

		// Token: 0x040018A3 RID: 6307
		internal ushort cfType;

		// Token: 0x040018A4 RID: 6308
		internal IntPtr dwHint;

		// Token: 0x040018A5 RID: 6309
		[SecurityCritical]
		internal IntPtr pstrName;

		// Token: 0x040018A6 RID: 6310
		internal Guid clsid;
	}
}
