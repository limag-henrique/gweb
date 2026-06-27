using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x020005A8 RID: 1448
	[FriendAccessAllowed]
	internal abstract class TextLexicalBreaks
	{
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004250 RID: 16976
		public abstract int Length { get; }

		// Token: 0x06004251 RID: 16977
		public abstract int GetNextBreak(int currentIndex);

		// Token: 0x06004252 RID: 16978
		public abstract int GetPreviousBreak(int currentIndex);
	}
}
