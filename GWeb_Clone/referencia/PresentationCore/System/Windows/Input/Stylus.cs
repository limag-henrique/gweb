using System;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Fornece acesso a informações gerais sobre uma caneta eletrônica.</summary>
	// Token: 0x020002AB RID: 683
	public static class Stylus
	{
		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x060013F6 RID: 5110 RVA: 0x0004A7C0 File Offset: 0x00049BC0
		public static void AddPreviewStylusDownHandler(DependencyObject element, StylusDownEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x060013F7 RID: 5111 RVA: 0x0004A7DC File Offset: 0x00049BDC
		public static void RemovePreviewStylusDownHandler(DependencyObject element, StylusDownEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x060013F8 RID: 5112 RVA: 0x0004A7F8 File Offset: 0x00049BF8
		public static void AddStylusDownHandler(DependencyObject element, StylusDownEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x060013F9 RID: 5113 RVA: 0x0004A814 File Offset: 0x00049C14
		public static void RemoveStylusDownHandler(DependencyObject element, StylusDownEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x060013FA RID: 5114 RVA: 0x0004A830 File Offset: 0x00049C30
		public static void AddPreviewStylusUpHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x060013FB RID: 5115 RVA: 0x0004A84C File Offset: 0x00049C4C
		public static void RemovePreviewStylusUpHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x060013FC RID: 5116 RVA: 0x0004A868 File Offset: 0x00049C68
		public static void AddStylusUpHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x060013FD RID: 5117 RVA: 0x0004A884 File Offset: 0x00049C84
		public static void RemoveStylusUpHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x060013FE RID: 5118 RVA: 0x0004A8A0 File Offset: 0x00049CA0
		public static void AddPreviewStylusMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x060013FF RID: 5119 RVA: 0x0004A8BC File Offset: 0x00049CBC
		public static void RemovePreviewStylusMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001400 RID: 5120 RVA: 0x0004A8D8 File Offset: 0x00049CD8
		public static void AddStylusMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001401 RID: 5121 RVA: 0x0004A8F4 File Offset: 0x00049CF4
		public static void RemoveStylusMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInAirMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001402 RID: 5122 RVA: 0x0004A910 File Offset: 0x00049D10
		public static void AddPreviewStylusInAirMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusInAirMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInAirMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001403 RID: 5123 RVA: 0x0004A92C File Offset: 0x00049D2C
		public static void RemovePreviewStylusInAirMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusInAirMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInAirMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001404 RID: 5124 RVA: 0x0004A948 File Offset: 0x00049D48
		public static void AddStylusInAirMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusInAirMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInAirMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001405 RID: 5125 RVA: 0x0004A964 File Offset: 0x00049D64
		public static void RemoveStylusInAirMoveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusInAirMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusEnter" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001406 RID: 5126 RVA: 0x0004A980 File Offset: 0x00049D80
		public static void AddStylusEnterHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusEnterEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusEnter" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001407 RID: 5127 RVA: 0x0004A99C File Offset: 0x00049D9C
		public static void RemoveStylusEnterHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusEnterEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusLeave" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001408 RID: 5128 RVA: 0x0004A9B8 File Offset: 0x00049DB8
		public static void AddStylusLeaveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusLeaveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusLeave" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001409 RID: 5129 RVA: 0x0004A9D4 File Offset: 0x00049DD4
		public static void RemoveStylusLeaveHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusLeaveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600140A RID: 5130 RVA: 0x0004A9F0 File Offset: 0x00049DF0
		public static void AddPreviewStylusInRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusInRangeEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600140B RID: 5131 RVA: 0x0004AA0C File Offset: 0x00049E0C
		public static void RemovePreviewStylusInRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusInRangeEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600140C RID: 5132 RVA: 0x0004AA28 File Offset: 0x00049E28
		public static void AddStylusInRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusInRangeEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600140D RID: 5133 RVA: 0x0004AA44 File Offset: 0x00049E44
		public static void RemoveStylusInRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusInRangeEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusOutOfRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600140E RID: 5134 RVA: 0x0004AA60 File Offset: 0x00049E60
		public static void AddPreviewStylusOutOfRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusOutOfRangeEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusOutOfRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600140F RID: 5135 RVA: 0x0004AA7C File Offset: 0x00049E7C
		public static void RemovePreviewStylusOutOfRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusOutOfRangeEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusOutOfRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001410 RID: 5136 RVA: 0x0004AA98 File Offset: 0x00049E98
		public static void AddStylusOutOfRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusOutOfRangeEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusOutOfRange" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001411 RID: 5137 RVA: 0x0004AAB4 File Offset: 0x00049EB4
		public static void RemoveStylusOutOfRangeHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusOutOfRangeEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusSystemGesture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001412 RID: 5138 RVA: 0x0004AAD0 File Offset: 0x00049ED0
		public static void AddPreviewStylusSystemGestureHandler(DependencyObject element, StylusSystemGestureEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusSystemGestureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusSystemGesture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001413 RID: 5139 RVA: 0x0004AAEC File Offset: 0x00049EEC
		public static void RemovePreviewStylusSystemGestureHandler(DependencyObject element, StylusSystemGestureEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusSystemGestureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusSystemGesture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001414 RID: 5140 RVA: 0x0004AB08 File Offset: 0x00049F08
		public static void AddStylusSystemGestureHandler(DependencyObject element, StylusSystemGestureEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusSystemGestureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusSystemGesture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001415 RID: 5141 RVA: 0x0004AB24 File Offset: 0x00049F24
		public static void RemoveStylusSystemGestureHandler(DependencyObject element, StylusSystemGestureEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusSystemGestureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.GotStylusCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001416 RID: 5142 RVA: 0x0004AB40 File Offset: 0x00049F40
		public static void AddGotStylusCaptureHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.GotStylusCaptureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.GotStylusCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001417 RID: 5143 RVA: 0x0004AB5C File Offset: 0x00049F5C
		public static void RemoveGotStylusCaptureHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.GotStylusCaptureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.LostStylusCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001418 RID: 5144 RVA: 0x0004AB78 File Offset: 0x00049F78
		public static void AddLostStylusCaptureHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.LostStylusCaptureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.LostStylusCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001419 RID: 5145 RVA: 0x0004AB94 File Offset: 0x00049F94
		public static void RemoveLostStylusCaptureHandler(DependencyObject element, StylusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.LostStylusCaptureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600141A RID: 5146 RVA: 0x0004ABB0 File Offset: 0x00049FB0
		public static void AddStylusButtonDownHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusButtonDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600141B RID: 5147 RVA: 0x0004ABCC File Offset: 0x00049FCC
		public static void RemoveStylusButtonDownHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusButtonDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600141C RID: 5148 RVA: 0x0004ABE8 File Offset: 0x00049FE8
		public static void AddStylusButtonUpHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.StylusButtonUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600141D RID: 5149 RVA: 0x0004AC04 File Offset: 0x0004A004
		public static void RemoveStylusButtonUpHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.StylusButtonUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600141E RID: 5150 RVA: 0x0004AC20 File Offset: 0x0004A020
		public static void AddPreviewStylusButtonDownHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusButtonDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600141F RID: 5151 RVA: 0x0004AC3C File Offset: 0x0004A03C
		public static void RemovePreviewStylusButtonDownHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusButtonDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001420 RID: 5152 RVA: 0x0004AC58 File Offset: 0x0004A058
		public static void AddPreviewStylusButtonUpHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Stylus.PreviewStylusButtonUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001421 RID: 5153 RVA: 0x0004AC74 File Offset: 0x0004A074
		public static void RemovePreviewStylusButtonUpHandler(DependencyObject element, StylusButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Stylus.PreviewStylusButtonUpEvent, handler);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsPressAndHoldEnabled" /> no elemento especificado.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual será determinado se pressionar e manter pressionado está habilitado.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento especificado tiver pressionar e manter pressionado habilitado; caso contrário, <see langword="false" />;</returns>
		// Token: 0x06001422 RID: 5154 RVA: 0x0004AC90 File Offset: 0x0004A090
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetIsPressAndHoldEnabled(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			object value = element.GetValue(Stylus.IsPressAndHoldEnabledProperty);
			return value != null && (bool)value;
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsPressAndHoldEnabled" /> no elemento especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual habilitar pressionar e manter pressionado.</param>
		/// <param name="enabled">
		///   <see langword="true" /> para habilitar pressionar e manter pressionado; <see langword="false" /> para desabilitar pressionar e manter pressionado.</param>
		// Token: 0x06001423 RID: 5155 RVA: 0x0004ACC4 File Offset: 0x0004A0C4
		public static void SetIsPressAndHoldEnabled(DependencyObject element, bool enabled)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(Stylus.IsPressAndHoldEnabledProperty, enabled);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsFlicksEnabled" /> no elemento especificado.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual será determinado se movimentos estão habilitados.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento especificado tiver movimentos habilitados; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001424 RID: 5156 RVA: 0x0004ACEC File Offset: 0x0004A0EC
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetIsFlicksEnabled(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			object value = element.GetValue(Stylus.IsFlicksEnabledProperty);
			return value != null && (bool)value;
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsFlicksEnabled" /> no elemento especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual habilitar movimentos.</param>
		/// <param name="enabled">
		///   <see langword="true" /> para habilitar movimentos; <see langword="false" /> para desabilitar movimentos.</param>
		// Token: 0x06001425 RID: 5157 RVA: 0x0004AD20 File Offset: 0x0004A120
		public static void SetIsFlicksEnabled(DependencyObject element, bool enabled)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(Stylus.IsFlicksEnabledProperty, enabled);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsTapFeedbackEnabled" /> no elemento especificado.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual será determinado se os comentários de toque estão habilitados.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento especificado tiver os comentários de toque habilitados; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001426 RID: 5158 RVA: 0x0004AD48 File Offset: 0x0004A148
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetIsTapFeedbackEnabled(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			object value = element.GetValue(Stylus.IsTapFeedbackEnabledProperty);
			return value != null && (bool)value;
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsTapFeedbackEnabled" /> no elemento especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual habilitar os comentários de toque.</param>
		/// <param name="enabled">
		///   <see langword="true" /> para habilitar os comentários de toque; <see langword="false" /> para desabilitar os comentários de toque.</param>
		// Token: 0x06001427 RID: 5159 RVA: 0x0004AD7C File Offset: 0x0004A17C
		public static void SetIsTapFeedbackEnabled(DependencyObject element, bool enabled)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(Stylus.IsTapFeedbackEnabledProperty, enabled);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsTouchFeedbackEnabled" /> no elemento especificado.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual será determinado se os comentários de toque recebido estão habilitados.</param>
		/// <returns>
		///   <see langword="true" /> se os comentários de toque recebido estiver habilitado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001428 RID: 5160 RVA: 0x0004ADA4 File Offset: 0x0004A1A4
		public static bool GetIsTouchFeedbackEnabled(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			object value = element.GetValue(Stylus.IsTouchFeedbackEnabledProperty);
			return value != null && (bool)value;
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.Stylus.IsTouchFeedbackEnabled" /> no elemento especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> no qual habilitar os comentários de toque.</param>
		/// <param name="enabled">
		///   <see langword="true" /> para habilitar os comentários de toque recebido; <see langword="false" /> para desabilitar os comentários de toque recebidos.</param>
		// Token: 0x06001429 RID: 5161 RVA: 0x0004ADD8 File Offset: 0x0004A1D8
		public static void SetIsTouchFeedbackEnabled(DependencyObject element, bool enabled)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(Stylus.IsTouchFeedbackEnabledProperty, enabled);
		}

		/// <summary>Obtém o elemento que está logo abaixo da caneta.</summary>
		/// <returns>O <see cref="T:System.Windows.IInputElement" /> que está diretamente abaixo da caneta.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0004AE00 File Offset: 0x0004A200
		public static IInputElement DirectlyOver
		{
			get
			{
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.DirectlyOver;
			}
		}

		/// <summary>Obtém o elemento ao qual a caneta está associada.</summary>
		/// <returns>O <see cref="T:System.Windows.IInputElement" /> ao qual a caneta está associada.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0004AE20 File Offset: 0x0004A220
		public static IInputElement Captured
		{
			get
			{
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				return ((currentStylusDevice != null) ? currentStylusDevice.Captured : null) ?? Mouse.Captured;
			}
		}

		/// <summary>Captura a caneta para o elemento especificado.</summary>
		/// <param name="element">O elemento para o qual a caneta será capturada.</param>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada para <paramref name="element" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600142C RID: 5164 RVA: 0x0004AE48 File Offset: 0x0004A248
		public static bool Capture(IInputElement element)
		{
			return Stylus.Capture(element, CaptureMode.Element);
		}

		/// <summary>Captura a caneta para o elemento especificado.</summary>
		/// <param name="element">O elemento para o qual a caneta será capturada.</param>
		/// <param name="captureMode">Um dos valores de <see cref="T:System.Windows.Input.CaptureMode" />.</param>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada para <paramref name="element" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600142D RID: 5165 RVA: 0x0004AE5C File Offset: 0x0004A25C
		public static bool Capture(IInputElement element, CaptureMode captureMode)
		{
			return Mouse.Capture(element, captureMode);
		}

		/// <summary>Sincroniza o cursor e a interface do usuário.</summary>
		// Token: 0x0600142E RID: 5166 RVA: 0x0004AE70 File Offset: 0x0004A270
		public static void Synchronize()
		{
			StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
			if (currentStylusDevice == null)
			{
				return;
			}
			currentStylusDevice.Synchronize();
		}

		/// <summary>Obtém a caneta que representa a caneta que está sendo usada.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusDevice" /> que representa a caneta em uso no momento.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0004AE8C File Offset: 0x0004A28C
		public static StylusDevice CurrentStylusDevice
		{
			[SecurityCritical]
			get
			{
				StylusLogic currentStylusLogic = StylusLogic.CurrentStylusLogic;
				if (currentStylusLogic == null)
				{
					return null;
				}
				StylusDeviceBase currentStylusDevice = currentStylusLogic.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.StylusDevice;
			}
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusDown" /> anexado.</summary>
		// Token: 0x04000ADB RID: 2779
		public static readonly RoutedEvent PreviewStylusDownEvent = EventManager.RegisterRoutedEvent("PreviewStylusDown", RoutingStrategy.Tunnel, typeof(StylusDownEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusDown" /> anexado.</summary>
		// Token: 0x04000ADC RID: 2780
		public static readonly RoutedEvent StylusDownEvent = EventManager.RegisterRoutedEvent("StylusDown", RoutingStrategy.Bubble, typeof(StylusDownEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusUp" /> anexado.</summary>
		// Token: 0x04000ADD RID: 2781
		public static readonly RoutedEvent PreviewStylusUpEvent = EventManager.RegisterRoutedEvent("PreviewStylusUp", RoutingStrategy.Tunnel, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusUp" /> anexado.</summary>
		// Token: 0x04000ADE RID: 2782
		public static readonly RoutedEvent StylusUpEvent = EventManager.RegisterRoutedEvent("StylusUp", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusMove" /> anexado.</summary>
		// Token: 0x04000ADF RID: 2783
		public static readonly RoutedEvent PreviewStylusMoveEvent = EventManager.RegisterRoutedEvent("PreviewStylusMove", RoutingStrategy.Tunnel, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusMove" /> anexado.</summary>
		// Token: 0x04000AE0 RID: 2784
		public static readonly RoutedEvent StylusMoveEvent = EventManager.RegisterRoutedEvent("StylusMove", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusInAirMove" /> anexado.</summary>
		// Token: 0x04000AE1 RID: 2785
		public static readonly RoutedEvent PreviewStylusInAirMoveEvent = EventManager.RegisterRoutedEvent("PreviewStylusInAirMove", RoutingStrategy.Tunnel, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusInAirMove" /> anexado.</summary>
		// Token: 0x04000AE2 RID: 2786
		public static readonly RoutedEvent StylusInAirMoveEvent = EventManager.RegisterRoutedEvent("StylusInAirMove", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusEnter" /> anexado.</summary>
		// Token: 0x04000AE3 RID: 2787
		public static readonly RoutedEvent StylusEnterEvent = EventManager.RegisterRoutedEvent("StylusEnter", RoutingStrategy.Direct, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusLeave" /> anexado.</summary>
		// Token: 0x04000AE4 RID: 2788
		public static readonly RoutedEvent StylusLeaveEvent = EventManager.RegisterRoutedEvent("StylusLeave", RoutingStrategy.Direct, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusInRange" /> anexado.</summary>
		// Token: 0x04000AE5 RID: 2789
		public static readonly RoutedEvent PreviewStylusInRangeEvent = EventManager.RegisterRoutedEvent("PreviewStylusInRange", RoutingStrategy.Tunnel, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusInRange" /> anexado.</summary>
		// Token: 0x04000AE6 RID: 2790
		public static readonly RoutedEvent StylusInRangeEvent = EventManager.RegisterRoutedEvent("StylusInRange", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusOutOfRange" /> anexado.</summary>
		// Token: 0x04000AE7 RID: 2791
		public static readonly RoutedEvent PreviewStylusOutOfRangeEvent = EventManager.RegisterRoutedEvent("PreviewStylusOutOfRange", RoutingStrategy.Tunnel, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusOutOfRange" /> anexado.</summary>
		// Token: 0x04000AE8 RID: 2792
		public static readonly RoutedEvent StylusOutOfRangeEvent = EventManager.RegisterRoutedEvent("StylusOutOfRange", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusSystemGesture" /> anexado.</summary>
		// Token: 0x04000AE9 RID: 2793
		public static readonly RoutedEvent PreviewStylusSystemGestureEvent = EventManager.RegisterRoutedEvent("PreviewStylusSystemGesture", RoutingStrategy.Tunnel, typeof(StylusSystemGestureEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusSystemGesture" /> anexado.</summary>
		// Token: 0x04000AEA RID: 2794
		public static readonly RoutedEvent StylusSystemGestureEvent = EventManager.RegisterRoutedEvent("StylusSystemGesture", RoutingStrategy.Bubble, typeof(StylusSystemGestureEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.GotStylusCapture" /> anexado.</summary>
		// Token: 0x04000AEB RID: 2795
		public static readonly RoutedEvent GotStylusCaptureEvent = EventManager.RegisterRoutedEvent("GotStylusCapture", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.LostStylusCapture" /> anexado.</summary>
		// Token: 0x04000AEC RID: 2796
		public static readonly RoutedEvent LostStylusCaptureEvent = EventManager.RegisterRoutedEvent("LostStylusCapture", RoutingStrategy.Bubble, typeof(StylusEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusButtonDown" /> anexado.</summary>
		// Token: 0x04000AED RID: 2797
		public static readonly RoutedEvent StylusButtonDownEvent = EventManager.RegisterRoutedEvent("StylusButtonDown", RoutingStrategy.Bubble, typeof(StylusButtonEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.StylusButtonUp" /> anexado.</summary>
		// Token: 0x04000AEE RID: 2798
		public static readonly RoutedEvent StylusButtonUpEvent = EventManager.RegisterRoutedEvent("StylusButtonUp", RoutingStrategy.Bubble, typeof(StylusButtonEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonDown" /> anexado.</summary>
		// Token: 0x04000AEF RID: 2799
		public static readonly RoutedEvent PreviewStylusButtonDownEvent = EventManager.RegisterRoutedEvent("PreviewStylusButtonDown", RoutingStrategy.Tunnel, typeof(StylusButtonEventHandler), typeof(Stylus));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonUp" /> anexado.</summary>
		// Token: 0x04000AF0 RID: 2800
		public static readonly RoutedEvent PreviewStylusButtonUpEvent = EventManager.RegisterRoutedEvent("PreviewStylusButtonUp", RoutingStrategy.Tunnel, typeof(StylusButtonEventHandler), typeof(Stylus));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.Stylus.IsPressAndHoldEnabled" /> anexada.</summary>
		// Token: 0x04000AF1 RID: 2801
		public static readonly DependencyProperty IsPressAndHoldEnabledProperty = DependencyProperty.RegisterAttached("IsPressAndHoldEnabled", typeof(bool), typeof(Stylus), new PropertyMetadata(true));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.Stylus.IsFlicksEnabled" /> anexada.</summary>
		// Token: 0x04000AF2 RID: 2802
		public static readonly DependencyProperty IsFlicksEnabledProperty = DependencyProperty.RegisterAttached("IsFlicksEnabled", typeof(bool), typeof(Stylus), new PropertyMetadata(true));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.Stylus.IsTapFeedbackEnabled" /> anexada.</summary>
		// Token: 0x04000AF3 RID: 2803
		public static readonly DependencyProperty IsTapFeedbackEnabledProperty = DependencyProperty.RegisterAttached("IsTapFeedbackEnabled", typeof(bool), typeof(Stylus), new PropertyMetadata(true));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.Stylus.IsTouchFeedbackEnabled" /> anexada.</summary>
		// Token: 0x04000AF4 RID: 2804
		public static readonly DependencyProperty IsTouchFeedbackEnabledProperty = DependencyProperty.RegisterAttached("IsTouchFeedbackEnabled", typeof(bool), typeof(Stylus), new PropertyMetadata(true));
	}
}
