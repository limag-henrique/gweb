using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077F RID: 1919
	internal class FontSourceFactory : IFontSourceFactory
	{
		// Token: 0x060050C7 RID: 20679 RVA: 0x0014381C File Offset: 0x00142C1C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public IFontSource Create(string uriString)
		{
			return new FontSource(new Uri(uriString), false);
		}
	}
}
