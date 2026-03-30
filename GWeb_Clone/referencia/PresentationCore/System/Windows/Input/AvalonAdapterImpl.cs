using System;

namespace System.Windows.Input
{
	// Token: 0x02000239 RID: 569
	internal class AvalonAdapterImpl : IAvalonAdapter
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0003C344 File Offset: 0x0003B744
		bool IAvalonAdapter.OnNoMoreTabStops(TraversalRequest request, ref bool ShouldCycle)
		{
			return false;
		}
	}
}
