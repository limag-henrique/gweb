using System;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002E2 RID: 738
	internal class TextServicesCompartmentEventSink : UnsafeNativeMethods.ITfCompartmentEventSink
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x000544EC File Offset: 0x000538EC
		internal TextServicesCompartmentEventSink(InputMethod inputmethod)
		{
			this._inputmethod = inputmethod;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00054508 File Offset: 0x00053908
		public void OnChange(ref Guid rguid)
		{
			this._inputmethod.OnChange(ref rguid);
		}

		// Token: 0x04000C31 RID: 3121
		private InputMethod _inputmethod;
	}
}
