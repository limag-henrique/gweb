using System;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x020005A9 RID: 1449
	[FriendAccessAllowed]
	internal abstract class TextLexicalService
	{
		// Token: 0x06004254 RID: 16980
		public abstract bool IsCultureSupported(CultureInfo culture);

		// Token: 0x06004255 RID: 16981
		public abstract TextLexicalBreaks AnalyzeText(char[] characterSource, int length, CultureInfo textCulture);
	}
}
