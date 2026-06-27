using System;
using System.Windows.Ink;
using System.Windows.Media;

namespace MS.Internal.Hashing.PresentationCore
{
	// Token: 0x020007E3 RID: 2019
	internal static class HashHelper
	{
		// Token: 0x060054C8 RID: 21704 RVA: 0x0015DC88 File Offset: 0x0015D088
		static HashHelper()
		{
			HashHelper.Initialize();
			Type[] types = new Type[]
			{
				typeof(CharacterMetrics),
				typeof(ExtendedProperty),
				typeof(FamilyTypeface),
				typeof(NumberSubstitution)
			};
			BaseHashHelper.RegisterTypes(typeof(HashHelper).Assembly, types);
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0015DCEC File Offset: 0x0015D0EC
		internal static bool HasReliableHashCode(object item)
		{
			return BaseHashHelper.HasReliableHashCode(item);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0015DD00 File Offset: 0x0015D100
		internal static void Initialize()
		{
		}
	}
}
