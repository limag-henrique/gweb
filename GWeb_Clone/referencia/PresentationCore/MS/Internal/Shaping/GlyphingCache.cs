using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B8 RID: 1720
	internal class GlyphingCache
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x001251FC File Offset: 0x001245FC
		internal GlyphingCache(int capacity)
		{
			this._sizeLimitedCache = new SizeLimitedCache<Typeface, TypefaceMap>(capacity);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x0012521C File Offset: 0x0012461C
		internal void GetShapeableText(Typeface typeface, CharacterBufferReference characterBufferReference, int stringLength, TextRunProperties textRunProperties, CultureInfo digitCulture, bool isRightToLeftParagraph, IList<TextShapeableSymbols> shapeableList, IShapeableTextCollector collector, TextFormattingMode textFormattingMode)
		{
			if (!typeface.Symbol)
			{
				this.Lookup(typeface).GetShapeableText(characterBufferReference, stringLength, textRunProperties, digitCulture, isRightToLeftParagraph, shapeableList, collector, textFormattingMode);
				return;
			}
			ShapeTypeface shapeTypeface = new ShapeTypeface(typeface.TryGetGlyphTypeface(), null);
			collector.Add(shapeableList, new CharacterBufferRange(characterBufferReference, stringLength), textRunProperties, new ItemProps(), shapeTypeface, 1.0, false, textFormattingMode);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00125280 File Offset: 0x00124680
		private TypefaceMap Lookup(Typeface key)
		{
			TypefaceMap typefaceMap = this._sizeLimitedCache.Get(key);
			if (typefaceMap == null)
			{
				typefaceMap = new TypefaceMap(key.FontFamily, key.FallbackFontFamily, key.CanonicalStyle, key.CanonicalWeight, key.CanonicalStretch, key.NullFont);
				this._sizeLimitedCache.Add(key, typefaceMap, false);
			}
			return typefaceMap;
		}

		// Token: 0x04001FFE RID: 8190
		private SizeLimitedCache<Typeface, TypefaceMap> _sizeLimitedCache;
	}
}
