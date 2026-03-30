using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x020006FF RID: 1791
	internal static class DoubleWideChar
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x00131688 File Offset: 0x00130A88
		internal static int GetChar(CharacterBuffer charBuffer, int ichText, int cchText, int charNumber, out int wordCount)
		{
			if (charNumber < cchText - 1 && (charBuffer[ichText + charNumber] & 'ﰀ') == '\ud800' && (charBuffer[ichText + charNumber + 1] & 'ﰀ') == '\udc00')
			{
				wordCount = 2;
				return (int)((int)(charBuffer[ichText + charNumber] & 'Ͽ') << 10 | (charBuffer[ichText + charNumber + 1] & 'Ͽ')) + 65536;
			}
			wordCount = 1;
			return (int)charBuffer[ichText + charNumber];
		}
	}
}
