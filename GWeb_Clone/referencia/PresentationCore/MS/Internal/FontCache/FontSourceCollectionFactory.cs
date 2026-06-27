using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000781 RID: 1921
	internal class FontSourceCollectionFactory : IFontSourceCollectionFactory
	{
		// Token: 0x060050DB RID: 20699 RVA: 0x00143D00 File Offset: 0x00143100
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public IFontSourceCollection Create(string uriString)
		{
			return new FontSourceCollection(new Uri(uriString), false);
		}
	}
}
