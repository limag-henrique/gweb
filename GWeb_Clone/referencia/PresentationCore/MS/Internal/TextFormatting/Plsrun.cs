using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000728 RID: 1832
	internal enum Plsrun : uint
	{
		// Token: 0x040021C5 RID: 8645
		CloseAnchor,
		// Token: 0x040021C6 RID: 8646
		Reverse,
		// Token: 0x040021C7 RID: 8647
		FakeLineBreak,
		// Token: 0x040021C8 RID: 8648
		FormatAnchor,
		// Token: 0x040021C9 RID: 8649
		Hidden,
		// Token: 0x040021CA RID: 8650
		Text,
		// Token: 0x040021CB RID: 8651
		InlineObject,
		// Token: 0x040021CC RID: 8652
		LineBreak,
		// Token: 0x040021CD RID: 8653
		ParaBreak,
		// Token: 0x040021CE RID: 8654
		Undefined = 2147483648U,
		// Token: 0x040021CF RID: 8655
		IsMarker = 1073741824U,
		// Token: 0x040021D0 RID: 8656
		UseNewCharacterBuffer = 536870912U,
		// Token: 0x040021D1 RID: 8657
		IsSymbol = 268435456U,
		// Token: 0x040021D2 RID: 8658
		UnmaskAll = 268435455U
	}
}
