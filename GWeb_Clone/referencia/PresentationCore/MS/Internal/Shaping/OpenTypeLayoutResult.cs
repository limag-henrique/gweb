using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D1 RID: 1745
	internal enum OpenTypeLayoutResult
	{
		// Token: 0x0400209B RID: 8347
		Success,
		// Token: 0x0400209C RID: 8348
		InvalidParameter,
		// Token: 0x0400209D RID: 8349
		TableNotFound,
		// Token: 0x0400209E RID: 8350
		ScriptNotFound,
		// Token: 0x0400209F RID: 8351
		LangSysNotFound,
		// Token: 0x040020A0 RID: 8352
		BadFontTable,
		// Token: 0x040020A1 RID: 8353
		UnderConstruction
	}
}
