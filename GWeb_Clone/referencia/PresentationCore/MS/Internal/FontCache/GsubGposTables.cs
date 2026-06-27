using System;
using System.Security;
using MS.Internal.Shaping;

namespace MS.Internal.FontCache
{
	// Token: 0x02000775 RID: 1909
	internal sealed class GsubGposTables : IOpenTypeFont
	{
		// Token: 0x0600507E RID: 20606 RVA: 0x00142588 File Offset: 0x00141988
		[SecurityCritical]
		internal GsubGposTables(FontFaceLayoutInfo layout)
		{
			this._layout = layout;
			this._gsubTable = new FontTable(this._layout.Gsub());
			this._gposTable = new FontTable(this._layout.Gpos());
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x001425D0 File Offset: 0x001419D0
		public FontTable GetFontTable(OpenTypeTags TableTag)
		{
			if (TableTag == OpenTypeTags.GPOS)
			{
				return this._gposTable;
			}
			if (TableTag == OpenTypeTags.GSUB)
			{
				return this._gsubTable;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00142600 File Offset: 0x00141A00
		public LayoutOffset GetGlyphPointCoord(ushort Glyph, ushort PointIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00142614 File Offset: 0x00141A14
		[SecurityCritical]
		public byte[] GetTableCache(OpenTypeTags tableTag)
		{
			return this._layout.GetTableCache(tableTag);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x00142630 File Offset: 0x00141A30
		[SecurityCritical]
		public byte[] AllocateTableCache(OpenTypeTags tableTag, int size)
		{
			return this._layout.AllocateTableCache(tableTag, size);
		}

		// Token: 0x040024AA RID: 9386
		private FontTable _gsubTable;

		// Token: 0x040024AB RID: 9387
		private FontTable _gposTable;

		// Token: 0x040024AC RID: 9388
		private FontFaceLayoutInfo _layout;
	}
}
