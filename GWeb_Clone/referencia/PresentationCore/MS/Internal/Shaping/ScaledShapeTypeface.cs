using System;
using System.Windows.Media;
using MS.Internal.FontCache;
using MS.Internal.FontFace;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E1 RID: 1761
	internal class ScaledShapeTypeface
	{
		// Token: 0x06004C10 RID: 19472 RVA: 0x00129D84 File Offset: 0x00129184
		internal ScaledShapeTypeface(GlyphTypeface glyphTypeface, IDeviceFont deviceFont, double scaleInEm, bool nullShape)
		{
			this._shapeTypeface = new ShapeTypeface(glyphTypeface, deviceFont);
			this._scaleInEm = scaleInEm;
			this._nullShape = nullShape;
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x00129DB4 File Offset: 0x001291B4
		internal ShapeTypeface ShapeTypeface
		{
			get
			{
				return this._shapeTypeface;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x00129DC8 File Offset: 0x001291C8
		internal double ScaleInEm
		{
			get
			{
				return this._scaleInEm;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06004C13 RID: 19475 RVA: 0x00129DDC File Offset: 0x001291DC
		internal bool NullShape
		{
			get
			{
				return this._nullShape;
			}
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x00129DF0 File Offset: 0x001291F0
		public override int GetHashCode()
		{
			int hash = this._shapeTypeface.GetHashCode();
			hash = HashFn.HashMultiply(hash) + (this._nullShape ? 1 : 0);
			hash = HashFn.HashMultiply(hash) + this._scaleInEm.GetHashCode();
			return HashFn.HashScramble(hash);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x00129E38 File Offset: 0x00129238
		public override bool Equals(object o)
		{
			ScaledShapeTypeface scaledShapeTypeface = o as ScaledShapeTypeface;
			return scaledShapeTypeface != null && (this._shapeTypeface.Equals(scaledShapeTypeface._shapeTypeface) && this._scaleInEm == scaledShapeTypeface._scaleInEm) && this._nullShape == scaledShapeTypeface._nullShape;
		}

		// Token: 0x04002101 RID: 8449
		private ShapeTypeface _shapeTypeface;

		// Token: 0x04002102 RID: 8450
		private double _scaleInEm;

		// Token: 0x04002103 RID: 8451
		private bool _nullShape;
	}
}
