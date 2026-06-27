using System;
using System.Windows;
using MS.Internal.FontFace;

namespace MS.Internal.FontCache
{
	// Token: 0x02000771 RID: 1905
	internal class CachedTypeface
	{
		// Token: 0x06005048 RID: 20552 RVA: 0x001412A8 File Offset: 0x001406A8
		internal CachedTypeface(FontStyle canonicalStyle, FontWeight canonicalWeight, FontStretch canonicalStretch, IFontFamily firstFontFamily, ITypefaceMetrics typefaceMetrics, bool nullFont)
		{
			this._canonicalStyle = canonicalStyle;
			this._canonicalWeight = canonicalWeight;
			this._canonicalStretch = canonicalStretch;
			Invariant.Assert(firstFontFamily != null && typefaceMetrics != null);
			this._firstFontFamily = firstFontFamily;
			this._typefaceMetrics = typefaceMetrics;
			this._nullFont = nullFont;
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06005049 RID: 20553 RVA: 0x001412FC File Offset: 0x001406FC
		internal FontStyle CanonicalStyle
		{
			get
			{
				return this._canonicalStyle;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x00141310 File Offset: 0x00140710
		internal FontWeight CanonicalWeight
		{
			get
			{
				return this._canonicalWeight;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x00141324 File Offset: 0x00140724
		internal FontStretch CanonicalStretch
		{
			get
			{
				return this._canonicalStretch;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x00141338 File Offset: 0x00140738
		internal IFontFamily FirstFontFamily
		{
			get
			{
				return this._firstFontFamily;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600504D RID: 20557 RVA: 0x0014134C File Offset: 0x0014074C
		internal ITypefaceMetrics TypefaceMetrics
		{
			get
			{
				return this._typefaceMetrics;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x00141360 File Offset: 0x00140760
		internal bool NullFont
		{
			get
			{
				return this._nullFont;
			}
		}

		// Token: 0x04002485 RID: 9349
		private FontStyle _canonicalStyle;

		// Token: 0x04002486 RID: 9350
		private FontWeight _canonicalWeight;

		// Token: 0x04002487 RID: 9351
		private FontStretch _canonicalStretch;

		// Token: 0x04002488 RID: 9352
		private IFontFamily _firstFontFamily;

		// Token: 0x04002489 RID: 9353
		private ITypefaceMetrics _typefaceMetrics;

		// Token: 0x0400248A RID: 9354
		private bool _nullFont;
	}
}
