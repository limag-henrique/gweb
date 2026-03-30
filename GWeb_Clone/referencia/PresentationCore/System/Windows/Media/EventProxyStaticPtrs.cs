using System;

namespace System.Windows.Media
{
	// Token: 0x0200038D RID: 909
	internal static class EventProxyStaticPtrs
	{
		// Token: 0x040010CD RID: 4301
		internal static EventProxyDescriptor.Dispose pfnDispose = new EventProxyDescriptor.Dispose(EventProxyDescriptor.StaticDispose);

		// Token: 0x040010CE RID: 4302
		internal static EventProxyDescriptor.RaiseEvent pfnRaiseEvent = new EventProxyDescriptor.RaiseEvent(EventProxyWrapper.RaiseEvent);
	}
}
