using System;

namespace System.Windows
{
	// Token: 0x020001A3 RID: 419
	internal class DpiFlags
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0001CB1C File Offset: 0x0001BF1C
		internal DpiFlags(bool dpiScaleFlag1, bool dpiScaleFlag2, int index)
		{
			this.DpiScaleFlag1 = dpiScaleFlag1;
			this.DpiScaleFlag2 = dpiScaleFlag2;
			this.Index = index;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001CB44 File Offset: 0x0001BF44
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0001CB58 File Offset: 0x0001BF58
		internal bool DpiScaleFlag1 { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001CB6C File Offset: 0x0001BF6C
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0001CB80 File Offset: 0x0001BF80
		internal bool DpiScaleFlag2 { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001CB94 File Offset: 0x0001BF94
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001CBA8 File Offset: 0x0001BFA8
		internal int Index { get; set; }
	}
}
