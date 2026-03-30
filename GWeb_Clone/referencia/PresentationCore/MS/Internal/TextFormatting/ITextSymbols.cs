using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Shaping;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200070A RID: 1802
	internal interface ITextSymbols
	{
		// Token: 0x06004D9B RID: 19867
		IList<TextShapeableSymbols> GetTextShapeableSymbols(GlyphingCache glyphingCache, CharacterBufferReference characterBufferReference, int characterLength, bool rightToLeft, bool isRightToLeftParagraph, CultureInfo digitCulture, TextModifierScope textModifierScope, TextFormattingMode textFormattingMode, bool isSideways);
	}
}
