using System;

namespace System.Windows.Media
{
	/// <summary>Uma enumeração que especifica uma dica para o mecanismo de renderização de que o texto pode ser renderizado com ClearType.</summary>
	// Token: 0x020003A9 RID: 937
	public enum ClearTypeHint
	{
		/// <summary>O mecanismo de renderização usa ClearType quando possível. Se a opacidade for introduzida, ClearType será desabilitado nessa subárvore.</summary>
		// Token: 0x0400113E RID: 4414
		Auto,
		/// <summary>O mecanismo de renderização reabilita ClearType para a subárvore atual. Quando a opacidade é introduzida nessa subárvore, ClearType é desabilitado.</summary>
		// Token: 0x0400113F RID: 4415
		Enabled
	}
}
