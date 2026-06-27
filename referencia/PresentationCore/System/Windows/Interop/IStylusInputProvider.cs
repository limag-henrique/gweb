using System;
using System.Windows.Input;
using MS.Internal.Interop;

namespace System.Windows.Interop
{
	// Token: 0x02000330 RID: 816
	internal interface IStylusInputProvider : IInputProvider, IDisposable
	{
		// Token: 0x06001BD8 RID: 7128
		IntPtr FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled);
	}
}
