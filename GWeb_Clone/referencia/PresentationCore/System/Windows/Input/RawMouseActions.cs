using System;

namespace System.Windows.Input
{
	// Token: 0x02000290 RID: 656
	[Flags]
	internal enum RawMouseActions
	{
		// Token: 0x04000A51 RID: 2641
		None = 0,
		// Token: 0x04000A52 RID: 2642
		AttributesChanged = 1,
		// Token: 0x04000A53 RID: 2643
		Activate = 2,
		// Token: 0x04000A54 RID: 2644
		Deactivate = 4,
		// Token: 0x04000A55 RID: 2645
		RelativeMove = 8,
		// Token: 0x04000A56 RID: 2646
		AbsoluteMove = 16,
		// Token: 0x04000A57 RID: 2647
		VirtualDesktopMove = 32,
		// Token: 0x04000A58 RID: 2648
		Button1Press = 64,
		// Token: 0x04000A59 RID: 2649
		Button1Release = 128,
		// Token: 0x04000A5A RID: 2650
		Button2Press = 256,
		// Token: 0x04000A5B RID: 2651
		Button2Release = 512,
		// Token: 0x04000A5C RID: 2652
		Button3Press = 1024,
		// Token: 0x04000A5D RID: 2653
		Button3Release = 2048,
		// Token: 0x04000A5E RID: 2654
		Button4Press = 4096,
		// Token: 0x04000A5F RID: 2655
		Button4Release = 8192,
		// Token: 0x04000A60 RID: 2656
		Button5Press = 16384,
		// Token: 0x04000A61 RID: 2657
		Button5Release = 32768,
		// Token: 0x04000A62 RID: 2658
		VerticalWheelRotate = 65536,
		// Token: 0x04000A63 RID: 2659
		HorizontalWheelRotate = 131072,
		// Token: 0x04000A64 RID: 2660
		QueryCursor = 262144,
		// Token: 0x04000A65 RID: 2661
		CancelCapture = 524288
	}
}
