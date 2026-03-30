using System;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000027 RID: 39
	internal interface IClassification
	{
		// Token: 0x060002FF RID: 767
		void GetCharAttribute(int unicodeScalar, out bool isCombining, out bool needsCaretInfo, out bool isIndic, out bool isDigit, out bool isLatin, out bool isStrong);
	}
}
