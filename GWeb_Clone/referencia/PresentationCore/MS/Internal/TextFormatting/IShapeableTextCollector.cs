using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x020006FD RID: 1789
	internal interface IShapeableTextCollector
	{
		// Token: 0x06004D11 RID: 19729
		void Add(IList<TextShapeableSymbols> shapeableList, CharacterBufferRange characterBufferRange, TextRunProperties textRunProperties, ItemProps textItem, ShapeTypeface shapeTypeface, double emScale, bool nullShape, TextFormattingMode textFormattingMode);
	}
}
