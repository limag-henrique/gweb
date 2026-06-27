using System;

namespace System.Windows.Input
{
	/// <summary>Fornece um serviço de nível de aplicativo que processa a entrada multitoque do sistema operacional e gera o evento <see cref="E:System.Windows.Input.Touch.FrameReported" />.</summary>
	// Token: 0x0200029A RID: 666
	public static class Touch
	{
		/// <summary>Ocorre quando uma mensagem de toque é enviada.</summary>
		// Token: 0x14000163 RID: 355
		// (add) Token: 0x0600135C RID: 4956 RVA: 0x000484EC File Offset: 0x000478EC
		// (remove) Token: 0x0600135D RID: 4957 RVA: 0x00048520 File Offset: 0x00047920
		public static event TouchFrameEventHandler FrameReported;

		// Token: 0x0600135E RID: 4958 RVA: 0x00048554 File Offset: 0x00047954
		internal static void ReportFrame()
		{
			if (Touch.FrameReported != null)
			{
				TouchFrameEventArgs e = new TouchFrameEventArgs(Environment.TickCount);
				Touch.FrameReported(null, e);
			}
		}

		// Token: 0x04000A8E RID: 2702
		internal static readonly RoutedEvent PreviewTouchDownEvent = EventManager.RegisterRoutedEvent("PreviewTouchDown", RoutingStrategy.Tunnel, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A8F RID: 2703
		internal static readonly RoutedEvent TouchDownEvent = EventManager.RegisterRoutedEvent("TouchDown", RoutingStrategy.Bubble, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A90 RID: 2704
		internal static readonly RoutedEvent PreviewTouchMoveEvent = EventManager.RegisterRoutedEvent("PreviewTouchMove", RoutingStrategy.Tunnel, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A91 RID: 2705
		internal static readonly RoutedEvent TouchMoveEvent = EventManager.RegisterRoutedEvent("TouchMove", RoutingStrategy.Bubble, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A92 RID: 2706
		internal static readonly RoutedEvent PreviewTouchUpEvent = EventManager.RegisterRoutedEvent("PreviewTouchUp", RoutingStrategy.Tunnel, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A93 RID: 2707
		internal static readonly RoutedEvent TouchUpEvent = EventManager.RegisterRoutedEvent("TouchUp", RoutingStrategy.Bubble, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A94 RID: 2708
		internal static readonly RoutedEvent GotTouchCaptureEvent = EventManager.RegisterRoutedEvent("GotTouchCapture", RoutingStrategy.Bubble, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A95 RID: 2709
		internal static readonly RoutedEvent LostTouchCaptureEvent = EventManager.RegisterRoutedEvent("LostTouchCapture", RoutingStrategy.Bubble, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A96 RID: 2710
		internal static readonly RoutedEvent TouchEnterEvent = EventManager.RegisterRoutedEvent("TouchEnter", RoutingStrategy.Direct, typeof(EventHandler<TouchEventArgs>), typeof(Touch));

		// Token: 0x04000A97 RID: 2711
		internal static readonly RoutedEvent TouchLeaveEvent = EventManager.RegisterRoutedEvent("TouchLeave", RoutingStrategy.Direct, typeof(EventHandler<TouchEventArgs>), typeof(Touch));
	}
}
