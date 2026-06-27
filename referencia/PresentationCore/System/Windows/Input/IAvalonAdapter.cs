using System;

namespace System.Windows.Input
{
	// Token: 0x02000238 RID: 568
	internal interface IAvalonAdapter
	{
		// Token: 0x06000FDC RID: 4060
		bool OnNoMoreTabStops(TraversalRequest request, ref bool ShouldCycle);
	}
}
