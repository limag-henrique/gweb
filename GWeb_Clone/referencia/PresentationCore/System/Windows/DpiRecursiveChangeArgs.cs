using System;

namespace System.Windows
{
	// Token: 0x020001A2 RID: 418
	internal class DpiRecursiveChangeArgs
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0001CA0C File Offset: 0x0001BE0C
		internal DpiRecursiveChangeArgs(DpiFlags dpiFlags, DpiScale oldDpiScale, DpiScale newDpiScale)
		{
			this.DpiScaleFlag1 = dpiFlags.DpiScaleFlag1;
			this.DpiScaleFlag2 = dpiFlags.DpiScaleFlag2;
			this.Index = dpiFlags.Index;
			this.OldDpiScale = oldDpiScale;
			this.NewDpiScale = newDpiScale;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001CA54 File Offset: 0x0001BE54
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001CA68 File Offset: 0x0001BE68
		internal bool DpiScaleFlag1 { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001CA7C File Offset: 0x0001BE7C
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001CA90 File Offset: 0x0001BE90
		internal bool DpiScaleFlag2 { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001CAA4 File Offset: 0x0001BEA4
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001CAB8 File Offset: 0x0001BEB8
		internal int Index { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001CACC File Offset: 0x0001BECC
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001CAE0 File Offset: 0x0001BEE0
		internal DpiScale OldDpiScale { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001CAF4 File Offset: 0x0001BEF4
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001CB08 File Offset: 0x0001BF08
		internal DpiScale NewDpiScale { get; set; }
	}
}
