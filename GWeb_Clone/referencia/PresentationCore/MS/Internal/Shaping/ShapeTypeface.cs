using System;
using System.Windows.Media;
using MS.Internal.FontCache;
using MS.Internal.FontFace;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E0 RID: 1760
	internal class ShapeTypeface
	{
		// Token: 0x06004C0B RID: 19467 RVA: 0x00129C90 File Offset: 0x00129090
		internal ShapeTypeface(GlyphTypeface glyphTypeface, IDeviceFont deviceFont)
		{
			Invariant.Assert(glyphTypeface != null);
			this._glyphTypeface = glyphTypeface;
			this._deviceFont = deviceFont;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00129CBC File Offset: 0x001290BC
		public override int GetHashCode()
		{
			return HashFn.HashMultiply(this._glyphTypeface.GetHashCode()) + ((this._deviceFont == null) ? 0 : this._deviceFont.Name.GetHashCode());
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x00129CF8 File Offset: 0x001290F8
		public override bool Equals(object o)
		{
			ShapeTypeface shapeTypeface = o as ShapeTypeface;
			if (shapeTypeface == null)
			{
				return false;
			}
			if (this._deviceFont == null)
			{
				if (shapeTypeface._deviceFont != null)
				{
					return false;
				}
			}
			else if (shapeTypeface._deviceFont == null || shapeTypeface._deviceFont.Name != this._deviceFont.Name)
			{
				return false;
			}
			return this._glyphTypeface.Equals(shapeTypeface._glyphTypeface);
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x00129D5C File Offset: 0x0012915C
		internal IDeviceFont DeviceFont
		{
			get
			{
				return this._deviceFont;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x00129D70 File Offset: 0x00129170
		internal GlyphTypeface GlyphTypeface
		{
			get
			{
				return this._glyphTypeface;
			}
		}

		// Token: 0x040020FF RID: 8447
		private GlyphTypeface _glyphTypeface;

		// Token: 0x04002100 RID: 8448
		private IDeviceFont _deviceFont;
	}
}
