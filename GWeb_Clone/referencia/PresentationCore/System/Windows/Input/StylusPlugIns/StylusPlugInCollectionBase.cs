using System;
using System.Security;
using System.Windows.Input.StylusPointer;
using System.Windows.Input.StylusWisp;

namespace System.Windows.Input.StylusPlugIns
{
	// Token: 0x020002FC RID: 764
	internal abstract class StylusPlugInCollectionBase
	{
		// Token: 0x06001876 RID: 6262 RVA: 0x00062308 File Offset: 0x00061708
		[SecuritySafeCritical]
		internal static StylusPlugInCollectionBase Create(StylusPlugInCollection wrapper)
		{
			StylusPlugInCollectionBase stylusPlugInCollectionBase;
			if (StylusLogic.IsPointerStackEnabled)
			{
				stylusPlugInCollectionBase = new PointerStylusPlugInCollection();
			}
			else
			{
				stylusPlugInCollectionBase = new WispStylusPlugInCollection();
			}
			stylusPlugInCollectionBase.Wrapper = wrapper;
			return stylusPlugInCollectionBase;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00062334 File Offset: 0x00061734
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00062348 File Offset: 0x00061748
		internal StylusPlugInCollection Wrapper { get; private set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001879 RID: 6265
		internal abstract bool IsActiveForInput { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600187A RID: 6266
		internal abstract object SyncRoot { get; }

		// Token: 0x0600187B RID: 6267
		internal abstract void UpdateState(UIElement element);

		// Token: 0x0600187C RID: 6268
		internal abstract void Unhook();
	}
}
