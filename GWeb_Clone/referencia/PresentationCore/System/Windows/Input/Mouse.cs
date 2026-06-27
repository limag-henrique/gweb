using System;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Representa o dispositivo de mouse para um thread específico.</summary>
	// Token: 0x0200027C RID: 636
	public static class Mouse
	{
		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600127F RID: 4735 RVA: 0x00044F08 File Offset: 0x00044308
		public static void AddPreviewMouseMoveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001280 RID: 4736 RVA: 0x00044F24 File Offset: 0x00044324
		public static void RemovePreviewMouseMoveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001281 RID: 4737 RVA: 0x00044F40 File Offset: 0x00044340
		public static void AddMouseMoveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseMoveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseMove" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001282 RID: 4738 RVA: 0x00044F5C File Offset: 0x0004435C
		public static void RemoveMouseMoveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseMoveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDownOutsideCapturedElement" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001283 RID: 4739 RVA: 0x00044F78 File Offset: 0x00044378
		public static void AddPreviewMouseDownOutsideCapturedElementHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseDownOutsideCapturedElementEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDownOutsideCapturedElement" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001284 RID: 4740 RVA: 0x00044F94 File Offset: 0x00044394
		public static void RemovePreviewMouseDownOutsideCapturedElementHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseDownOutsideCapturedElementEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUpOutsideCapturedElement" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001285 RID: 4741 RVA: 0x00044FB0 File Offset: 0x000443B0
		public static void AddPreviewMouseUpOutsideCapturedElementHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseUpOutsideCapturedElementEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUpOutsideCapturedElement" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001286 RID: 4742 RVA: 0x00044FCC File Offset: 0x000443CC
		public static void RemovePreviewMouseUpOutsideCapturedElementHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseUpOutsideCapturedElementEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001287 RID: 4743 RVA: 0x00044FE8 File Offset: 0x000443E8
		public static void AddPreviewMouseDownHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001288 RID: 4744 RVA: 0x00045004 File Offset: 0x00044404
		public static void RemovePreviewMouseDownHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001289 RID: 4745 RVA: 0x00045020 File Offset: 0x00044420
		public static void AddMouseDownHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128A RID: 4746 RVA: 0x0004503C File Offset: 0x0004443C
		public static void RemoveMouseDownHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128B RID: 4747 RVA: 0x00045058 File Offset: 0x00044458
		public static void AddPreviewMouseUpHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128C RID: 4748 RVA: 0x00045074 File Offset: 0x00044474
		public static void RemovePreviewMouseUpHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128D RID: 4749 RVA: 0x00045090 File Offset: 0x00044490
		public static void AddMouseUpHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128E RID: 4750 RVA: 0x000450AC File Offset: 0x000444AC
		public static void RemoveMouseUpHandler(DependencyObject element, MouseButtonEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseWheel" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600128F RID: 4751 RVA: 0x000450C8 File Offset: 0x000444C8
		public static void AddPreviewMouseWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.PreviewMouseWheelEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseWheel" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001290 RID: 4752 RVA: 0x000450E4 File Offset: 0x000444E4
		public static void RemovePreviewMouseWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.PreviewMouseWheelEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseWheel" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001291 RID: 4753 RVA: 0x00045100 File Offset: 0x00044500
		public static void AddMouseWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseWheelEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseWheel" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001292 RID: 4754 RVA: 0x0004511C File Offset: 0x0004451C
		public static void RemoveMouseWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseWheelEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseEnter" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001293 RID: 4755 RVA: 0x00045138 File Offset: 0x00044538
		public static void AddMouseEnterHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseEnterEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseEnter" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001294 RID: 4756 RVA: 0x00045154 File Offset: 0x00044554
		public static void RemoveMouseEnterHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseEnterEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.MouseLeave" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001295 RID: 4757 RVA: 0x00045170 File Offset: 0x00044570
		public static void AddMouseLeaveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.MouseLeaveEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.MouseLeave" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001296 RID: 4758 RVA: 0x0004518C File Offset: 0x0004458C
		public static void RemoveMouseLeaveHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.MouseLeaveEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.GotMouseCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001297 RID: 4759 RVA: 0x000451A8 File Offset: 0x000445A8
		public static void AddGotMouseCaptureHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.GotMouseCaptureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.GotMouseCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001298 RID: 4760 RVA: 0x000451C4 File Offset: 0x000445C4
		public static void RemoveGotMouseCaptureHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.GotMouseCaptureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.LostMouseCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x06001299 RID: 4761 RVA: 0x000451E0 File Offset: 0x000445E0
		public static void AddLostMouseCaptureHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.LostMouseCaptureEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.LostMouseCapture" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600129A RID: 4762 RVA: 0x000451FC File Offset: 0x000445FC
		public static void RemoveLostMouseCaptureHandler(DependencyObject element, MouseEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.LostMouseCaptureEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Mouse.QueryCursor" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600129B RID: 4763 RVA: 0x00045218 File Offset: 0x00044618
		public static void AddQueryCursorHandler(DependencyObject element, QueryCursorEventHandler handler)
		{
			UIElement.AddHandler(element, Mouse.QueryCursorEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Mouse.QueryCursor" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos.</param>
		// Token: 0x0600129C RID: 4764 RVA: 0x00045234 File Offset: 0x00044634
		public static void RemoveQueryCursorHandler(DependencyObject element, QueryCursorEventHandler handler)
		{
			UIElement.RemoveHandler(element, Mouse.QueryCursorEvent, handler);
		}

		/// <summary>Obtém o elemento sobre o qual o ponteiro do mouse está diretamente acima.</summary>
		/// <returns>O elemento sobre o qual o ponteiro do mouse está acima.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00045250 File Offset: 0x00044650
		public static IInputElement DirectlyOver
		{
			get
			{
				return Mouse.PrimaryDevice.DirectlyOver;
			}
		}

		/// <summary>Obtém o elemento que capturou o mouse.</summary>
		/// <returns>O elemento capturado pelo mouse.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00045268 File Offset: 0x00044668
		public static IInputElement Captured
		{
			get
			{
				return Mouse.PrimaryDevice.Captured;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00045280 File Offset: 0x00044680
		internal static CaptureMode CapturedMode
		{
			get
			{
				return Mouse.PrimaryDevice.CapturedMode;
			}
		}

		/// <summary>Captura a entrada de mouse no elemento especificado.</summary>
		/// <param name="element">O elemento do qual o mouse é capturado.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento conseguiu capturar o mouse, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060012A0 RID: 4768 RVA: 0x00045298 File Offset: 0x00044698
		public static bool Capture(IInputElement element)
		{
			return Mouse.PrimaryDevice.Capture(element);
		}

		/// <summary>Captura a entrada do mouse para o elemento especificado usando o <see cref="T:System.Windows.Input.CaptureMode" /> especificado.</summary>
		/// <param name="element">O elemento do qual o mouse é capturado.</param>
		/// <param name="captureMode">A política de captura a ser usada.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento conseguiu capturar o mouse, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060012A1 RID: 4769 RVA: 0x000452B0 File Offset: 0x000446B0
		public static bool Capture(IInputElement element, CaptureMode captureMode)
		{
			return Mouse.PrimaryDevice.Capture(element, captureMode);
		}

		/// <summary>Recupera até 64 coordenadas anteriores do ponteiro do mouse desde o último evento de movimentação do mouse.</summary>
		/// <param name="relativeTo">Os elementos com relação aos quais está o <paramref name="points" />.</param>
		/// <param name="points">Uma matriz de objetos.</param>
		/// <returns>O número de pontos retornado.</returns>
		// Token: 0x060012A2 RID: 4770 RVA: 0x000452CC File Offset: 0x000446CC
		[SecurityCritical]
		public static int GetIntermediatePoints(IInputElement relativeTo, Point[] points)
		{
			if (Mouse.PrimaryDevice.IsActive && relativeTo != null)
			{
				PresentationSource presentationSource = PresentationSource.FromDependencyObject(InputElement.GetContainingVisual(relativeTo as DependencyObject));
				if (presentationSource != null)
				{
					IMouseInputProvider mouseInputProvider = presentationSource.GetInputProvider(typeof(MouseDevice)) as IMouseInputProvider;
					if (mouseInputProvider != null)
					{
						return mouseInputProvider.GetIntermediatePoints(relativeTo, points);
					}
				}
			}
			return -1;
		}

		/// <summary>Obtém ou define o cursor para o aplicativo inteiro.</summary>
		/// <returns>O cursor de substituição ou <see langword="null" /> se o <see cref="P:System.Windows.Input.Mouse.OverrideCursor" /> não está definido.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00045320 File Offset: 0x00044720
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00045338 File Offset: 0x00044738
		public static Cursor OverrideCursor
		{
			get
			{
				return Mouse.PrimaryDevice.OverrideCursor;
			}
			set
			{
				Mouse.PrimaryDevice.OverrideCursor = value;
			}
		}

		/// <summary>Define o ponteiro do mouse para o <see cref="T:System.Windows.Input.Cursor" /> especificado.</summary>
		/// <param name="cursor">O cursor para o qual definir o ponteiro do mouse.</param>
		/// <returns>
		///   <see langword="true" />, se o cursor foi definido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060012A5 RID: 4773 RVA: 0x00045350 File Offset: 0x00044750
		public static bool SetCursor(Cursor cursor)
		{
			return Mouse.PrimaryDevice.SetCursor(cursor);
		}

		/// <summary>Obtém o estado do botão esquerdo do mouse.</summary>
		/// <returns>O estado do botão esquerdo do mouse.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00045368 File Offset: 0x00044768
		public static MouseButtonState LeftButton
		{
			get
			{
				return Mouse.PrimaryDevice.LeftButton;
			}
		}

		/// <summary>Obtém o estado do botão direito.</summary>
		/// <returns>O estado do botão direito do mouse.</returns>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00045380 File Offset: 0x00044780
		public static MouseButtonState RightButton
		{
			get
			{
				return Mouse.PrimaryDevice.RightButton;
			}
		}

		/// <summary>Obtém o estado do botão do meio do mouse.</summary>
		/// <returns>O estado do botão do meio do mouse.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00045398 File Offset: 0x00044798
		public static MouseButtonState MiddleButton
		{
			get
			{
				return Mouse.PrimaryDevice.MiddleButton;
			}
		}

		/// <summary>Obtém o estado do primeiro botão estendido.</summary>
		/// <returns>O estado do primeiro botão do mouse estendido.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000453B0 File Offset: 0x000447B0
		public static MouseButtonState XButton1
		{
			get
			{
				return Mouse.PrimaryDevice.XButton1;
			}
		}

		/// <summary>Obtém o estado do segundo botão estendido.</summary>
		/// <returns>O estado do segundo botão do mouse estendido.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x000453C8 File Offset: 0x000447C8
		public static MouseButtonState XButton2
		{
			get
			{
				return Mouse.PrimaryDevice.XButton2;
			}
		}

		/// <summary>Obtém a posição do mouse em relação a um elemento especificado.</summary>
		/// <param name="relativeTo">O espaço de coordenadas no qual a posição do mouse será calculada.</param>
		/// <returns>A posição do mouse em relação ao parâmetro <paramref name="relativeTo" />.</returns>
		// Token: 0x060012AB RID: 4779 RVA: 0x000453E0 File Offset: 0x000447E0
		public static Point GetPosition(IInputElement relativeTo)
		{
			return Mouse.PrimaryDevice.GetPosition(relativeTo);
		}

		/// <summary>Força o mouse a sincronizar novamente.</summary>
		// Token: 0x060012AC RID: 4780 RVA: 0x000453F8 File Offset: 0x000447F8
		public static void Synchronize()
		{
			Mouse.PrimaryDevice.Synchronize();
		}

		/// <summary>Força a atualização do cursor do mouse.</summary>
		// Token: 0x060012AD RID: 4781 RVA: 0x00045410 File Offset: 0x00044810
		public static void UpdateCursor()
		{
			Mouse.PrimaryDevice.UpdateCursor();
		}

		/// <summary>Obtém o dispositivo primário de mouse.</summary>
		/// <returns>O dispositivo.</returns>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00045428 File Offset: 0x00044828
		public static MouseDevice PrimaryDevice
		{
			[SecurityCritical]
			get
			{
				return InputManager.UnsecureCurrent.PrimaryMouseDevice;
			}
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseMove" /> anexado.</summary>
		// Token: 0x04000A08 RID: 2568
		public static readonly RoutedEvent PreviewMouseMoveEvent = EventManager.RegisterRoutedEvent("PreviewMouseMove", RoutingStrategy.Tunnel, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseMove" /> anexado.</summary>
		// Token: 0x04000A09 RID: 2569
		public static readonly RoutedEvent MouseMoveEvent = EventManager.RegisterRoutedEvent("MouseMove", RoutingStrategy.Bubble, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseDownOutsideCapturedElement" /> anexado.</summary>
		// Token: 0x04000A0A RID: 2570
		public static readonly RoutedEvent PreviewMouseDownOutsideCapturedElementEvent = EventManager.RegisterRoutedEvent("PreviewMouseDownOutsideCapturedElement", RoutingStrategy.Tunnel, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseUpOutsideCapturedElement" /> anexado.</summary>
		// Token: 0x04000A0B RID: 2571
		public static readonly RoutedEvent PreviewMouseUpOutsideCapturedElementEvent = EventManager.RegisterRoutedEvent("PreviewMouseUpOutsideCapturedElement", RoutingStrategy.Tunnel, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown" /> anexado.</summary>
		// Token: 0x04000A0C RID: 2572
		public static readonly RoutedEvent PreviewMouseDownEvent = EventManager.RegisterRoutedEvent("PreviewMouseDown", RoutingStrategy.Tunnel, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseDown" /> anexado.</summary>
		// Token: 0x04000A0D RID: 2573
		public static readonly RoutedEvent MouseDownEvent = EventManager.RegisterRoutedEvent("MouseDown", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseUp" /> anexado.</summary>
		// Token: 0x04000A0E RID: 2574
		public static readonly RoutedEvent PreviewMouseUpEvent = EventManager.RegisterRoutedEvent("PreviewMouseUp", RoutingStrategy.Tunnel, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseUp" /> anexado.</summary>
		// Token: 0x04000A0F RID: 2575
		public static readonly RoutedEvent MouseUpEvent = EventManager.RegisterRoutedEvent("MouseUp", RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.PreviewMouseWheel" /> anexado.</summary>
		// Token: 0x04000A10 RID: 2576
		public static readonly RoutedEvent PreviewMouseWheelEvent = EventManager.RegisterRoutedEvent("PreviewMouseWheel", RoutingStrategy.Tunnel, typeof(MouseWheelEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseWheel" /> anexado.</summary>
		// Token: 0x04000A11 RID: 2577
		public static readonly RoutedEvent MouseWheelEvent = EventManager.RegisterRoutedEvent("MouseWheel", RoutingStrategy.Bubble, typeof(MouseWheelEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseEnter" /> anexado.</summary>
		// Token: 0x04000A12 RID: 2578
		public static readonly RoutedEvent MouseEnterEvent = EventManager.RegisterRoutedEvent("MouseEnter", RoutingStrategy.Direct, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.MouseLeave" /> anexado.</summary>
		// Token: 0x04000A13 RID: 2579
		public static readonly RoutedEvent MouseLeaveEvent = EventManager.RegisterRoutedEvent("MouseLeave", RoutingStrategy.Direct, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.GotMouseCapture" /> anexado.</summary>
		// Token: 0x04000A14 RID: 2580
		public static readonly RoutedEvent GotMouseCaptureEvent = EventManager.RegisterRoutedEvent("GotMouseCapture", RoutingStrategy.Bubble, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.LostMouseCapture" /> anexado.</summary>
		// Token: 0x04000A15 RID: 2581
		public static readonly RoutedEvent LostMouseCaptureEvent = EventManager.RegisterRoutedEvent("LostMouseCapture", RoutingStrategy.Bubble, typeof(MouseEventHandler), typeof(Mouse));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Mouse.QueryCursor" /> anexado.</summary>
		// Token: 0x04000A16 RID: 2582
		public static readonly RoutedEvent QueryCursorEvent = EventManager.RegisterRoutedEvent("QueryCursor", RoutingStrategy.Bubble, typeof(QueryCursorEventHandler), typeof(Mouse));

		/// <summary>Representa o número de unidades que a roda do mouse gira para rolar uma linha.</summary>
		// Token: 0x04000A17 RID: 2583
		public const int MouseWheelDeltaForOneLine = 120;
	}
}
