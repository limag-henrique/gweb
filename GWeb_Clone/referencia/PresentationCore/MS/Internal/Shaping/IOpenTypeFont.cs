using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006CB RID: 1739
	internal interface IOpenTypeFont
	{
		// Token: 0x06004B80 RID: 19328
		[SecurityCritical]
		FontTable GetFontTable(OpenTypeTags TableTag);

		// Token: 0x06004B81 RID: 19329
		LayoutOffset GetGlyphPointCoord(ushort Glyph, ushort PointIndex);

		// Token: 0x06004B82 RID: 19330
		byte[] GetTableCache(OpenTypeTags tableTag);

		// Token: 0x06004B83 RID: 19331
		byte[] AllocateTableCache(OpenTypeTags tableTag, int size);
	}
}
