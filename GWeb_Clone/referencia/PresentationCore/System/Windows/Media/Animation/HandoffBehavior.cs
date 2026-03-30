using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Especifica como novas animações interagem com as existentes já aplicadas a uma propriedade.</summary>
	// Token: 0x02000569 RID: 1385
	public enum HandoffBehavior
	{
		/// <summary>Novas animações substituem todas as animações existentes nas propriedades às quais elas são aplicadas.</summary>
		// Token: 0x0400178F RID: 6031
		SnapshotAndReplace,
		/// <summary>Novas animações são combinadas com animações existentes acrescentando novas animações no final da cadeia de composição.</summary>
		// Token: 0x04001790 RID: 6032
		Compose
	}
}
