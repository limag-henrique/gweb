using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Fornece métodos e campos auxiliares para iniciar operações do tipo “arrastar e soltar”, incluindo um método para iniciar uma operação desse tipo e recursos para adicionar e remover manipuladores de eventos relacionados a esse tipo de operação.</summary>
	// Token: 0x020001A6 RID: 422
	public static class DragDrop
	{
		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewQueryContinueDrag" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x0600063F RID: 1599 RVA: 0x0001CC54 File Offset: 0x0001C054
		public static void AddPreviewQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewQueryContinueDragEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewQueryContinueDrag" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000640 RID: 1600 RVA: 0x0001CC70 File Offset: 0x0001C070
		public static void RemovePreviewQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewQueryContinueDragEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.QueryContinueDrag" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000641 RID: 1601 RVA: 0x0001CC8C File Offset: 0x0001C08C
		public static void AddQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.QueryContinueDragEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.QueryContinueDrag" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000642 RID: 1602 RVA: 0x0001CCA8 File Offset: 0x0001C0A8
		public static void RemoveQueryContinueDragHandler(DependencyObject element, QueryContinueDragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.QueryContinueDragEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewGiveFeedback" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000643 RID: 1603 RVA: 0x0001CCC4 File Offset: 0x0001C0C4
		public static void AddPreviewGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewGiveFeedbackEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewGiveFeedback" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000644 RID: 1604 RVA: 0x0001CCE0 File Offset: 0x0001C0E0
		public static void RemovePreviewGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewGiveFeedbackEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.GiveFeedback" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000645 RID: 1605 RVA: 0x0001CCFC File Offset: 0x0001C0FC
		public static void AddGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.GiveFeedbackEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.GiveFeedback" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000646 RID: 1606 RVA: 0x0001CD18 File Offset: 0x0001C118
		public static void RemoveGiveFeedbackHandler(DependencyObject element, GiveFeedbackEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.GiveFeedbackEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragEnter" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000647 RID: 1607 RVA: 0x0001CD34 File Offset: 0x0001C134
		public static void AddPreviewDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewDragEnterEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragEnter" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000648 RID: 1608 RVA: 0x0001CD50 File Offset: 0x0001C150
		public static void RemovePreviewDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewDragEnterEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragEnter" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000649 RID: 1609 RVA: 0x0001CD6C File Offset: 0x0001C16C
		public static void AddDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.DragEnterEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragEnter" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x0600064A RID: 1610 RVA: 0x0001CD88 File Offset: 0x0001C188
		public static void RemoveDragEnterHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.DragEnterEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragOver" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x0600064B RID: 1611 RVA: 0x0001CDA4 File Offset: 0x0001C1A4
		public static void AddPreviewDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewDragOverEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragOver" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x0600064C RID: 1612 RVA: 0x0001CDC0 File Offset: 0x0001C1C0
		public static void RemovePreviewDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewDragOverEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragOver" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x0600064D RID: 1613 RVA: 0x0001CDDC File Offset: 0x0001C1DC
		public static void AddDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.DragOverEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragOver" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x0600064E RID: 1614 RVA: 0x0001CDF8 File Offset: 0x0001C1F8
		public static void RemoveDragOverHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.DragOverEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragLeave" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x0600064F RID: 1615 RVA: 0x0001CE14 File Offset: 0x0001C214
		public static void AddPreviewDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewDragLeaveEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDragLeave" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000650 RID: 1616 RVA: 0x0001CE30 File Offset: 0x0001C230
		public static void RemovePreviewDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewDragLeaveEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragLeave" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000651 RID: 1617 RVA: 0x0001CE4C File Offset: 0x0001C24C
		public static void AddDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.DragLeaveEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.DragLeave" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000652 RID: 1618 RVA: 0x0001CE68 File Offset: 0x0001C268
		public static void RemoveDragLeaveHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.DragLeaveEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDrop" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000653 RID: 1619 RVA: 0x0001CE84 File Offset: 0x0001C284
		public static void AddPreviewDropHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.PreviewDropEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.PreviewDrop" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000654 RID: 1620 RVA: 0x0001CEA0 File Offset: 0x0001C2A0
		public static void RemovePreviewDropHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.PreviewDropEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DragDrop.Drop" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x06000655 RID: 1621 RVA: 0x0001CEBC File Offset: 0x0001C2BC
		public static void AddDropHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.AddHandler(element, DragDrop.DropEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DragDrop.Drop" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x06000656 RID: 1622 RVA: 0x0001CED8 File Offset: 0x0001C2D8
		public static void RemoveDropHandler(DependencyObject element, DragEventHandler handler)
		{
			UIElement.RemoveHandler(element, DragDrop.DropEvent, handler);
		}

		/// <summary>Inicia uma operação do tipo "arrastar e soltar".</summary>
		/// <param name="dragSource">Uma referência para o objeto de dependência que é a origem dos dados sendo arrastados.</param>
		/// <param name="data">Um objeto de dados que contém os dados sendo arrastados.</param>
		/// <param name="allowedEffects">Um dos valores <see cref="T:System.Windows.DragDropEffects" /> que especificam os efeitos permitidos da operação do tipo "arrastar e soltar".</param>
		/// <returns>Um dos valores <see cref="T:System.Windows.DragDropEffects" /> que especificam o efeito final que foi executado durante a operação do tipo "arrastar e soltar".</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dragSource" /> ou <paramref name="data" /> é <see langword="null" />.</exception>
		// Token: 0x06000657 RID: 1623 RVA: 0x0001CEF4 File Offset: 0x0001C2F4
		[SecurityCritical]
		public static DragDropEffects DoDragDrop(DependencyObject dragSource, object data, DragDropEffects allowedEffects)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (dragSource == null)
			{
				throw new ArgumentNullException("dragSource");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RoutedEventArgs e = new RoutedEventArgs(DragDrop.DragDropStartedEvent, dragSource);
			if (dragSource is UIElement)
			{
				((UIElement)dragSource).RaiseEvent(e);
			}
			else if (dragSource is ContentElement)
			{
				((ContentElement)dragSource).RaiseEvent(e);
			}
			else
			{
				if (!(dragSource is UIElement3D))
				{
					throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "dragSource");
				}
				((UIElement3D)dragSource).RaiseEvent(e);
			}
			DataObject dataObject = data as DataObject;
			if (dataObject == null)
			{
				dataObject = new DataObject(data);
			}
			DragDropEffects result = DragDrop.OleDoDragDrop(dragSource, dataObject, allowedEffects);
			e = new RoutedEventArgs(DragDrop.DragDropCompletedEvent, dragSource);
			if (dragSource is UIElement)
			{
				((UIElement)dragSource).RaiseEvent(e);
			}
			else if (dragSource is ContentElement)
			{
				((ContentElement)dragSource).RaiseEvent(e);
			}
			else
			{
				if (!(dragSource is UIElement3D))
				{
					throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "dragSource");
				}
				((UIElement3D)dragSource).RaiseEvent(e);
			}
			return result;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001D004 File Offset: 0x0001C404
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void RegisterDropTarget(IntPtr windowHandle)
		{
			if (SecurityHelper.CheckUnmanagedCodePermission() && windowHandle != IntPtr.Zero)
			{
				OleDropTarget dropTarget = new OleDropTarget(windowHandle);
				OleServicesContext.CurrentOleServicesContext.OleRegisterDragDrop(new HandleRef(null, windowHandle), dropTarget);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001D040 File Offset: 0x0001C440
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void RevokeDropTarget(IntPtr windowHandle)
		{
			if (SecurityHelper.CheckUnmanagedCodePermission() && windowHandle != IntPtr.Zero)
			{
				OleServicesContext.CurrentOleServicesContext.OleRevokeDragDrop(new HandleRef(null, windowHandle));
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001D074 File Offset: 0x0001C474
		internal static bool IsValidDragDropEffects(DragDropEffects dragDropEffects)
		{
			int num = -2147483641;
			return (dragDropEffects & (DragDropEffects)(~(DragDropEffects)num)) == DragDropEffects.None;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001D090 File Offset: 0x0001C490
		internal static bool IsValidDragAction(DragAction dragAction)
		{
			return dragAction == DragAction.Continue || dragAction == DragAction.Drop || dragAction == DragAction.Cancel;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001D0AC File Offset: 0x0001C4AC
		internal static bool IsValidDragDropKeyStates(DragDropKeyStates dragDropKeyStates)
		{
			int num = 63;
			return (dragDropKeyStates & (DragDropKeyStates)(~(DragDropKeyStates)num)) == DragDropKeyStates.None;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001D0C8 File Offset: 0x0001C4C8
		[SecurityCritical]
		private static DragDropEffects OleDoDragDrop(DependencyObject dragSource, DataObject dataObject, DragDropEffects allowedEffects)
		{
			int[] array = new int[1];
			OleDragSource dropSource = new OleDragSource(dragSource);
			OleServicesContext.CurrentOleServicesContext.OleDoDragDrop(dataObject, dropSource, (int)allowedEffects, array);
			return (DragDropEffects)array[0];
		}

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewQueryContinueDrag" /></summary>
		// Token: 0x04000581 RID: 1409
		public static readonly RoutedEvent PreviewQueryContinueDragEvent = EventManager.RegisterRoutedEvent("PreviewQueryContinueDrag", RoutingStrategy.Tunnel, typeof(QueryContinueDragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.QueryContinueDrag" /></summary>
		// Token: 0x04000582 RID: 1410
		public static readonly RoutedEvent QueryContinueDragEvent = EventManager.RegisterRoutedEvent("QueryContinueDrag", RoutingStrategy.Bubble, typeof(QueryContinueDragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewGiveFeedback" /></summary>
		// Token: 0x04000583 RID: 1411
		public static readonly RoutedEvent PreviewGiveFeedbackEvent = EventManager.RegisterRoutedEvent("PreviewGiveFeedback", RoutingStrategy.Tunnel, typeof(GiveFeedbackEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.GiveFeedback" /></summary>
		// Token: 0x04000584 RID: 1412
		public static readonly RoutedEvent GiveFeedbackEvent = EventManager.RegisterRoutedEvent("GiveFeedback", RoutingStrategy.Bubble, typeof(GiveFeedbackEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragEnter" /></summary>
		// Token: 0x04000585 RID: 1413
		public static readonly RoutedEvent PreviewDragEnterEvent = EventManager.RegisterRoutedEvent("PreviewDragEnter", RoutingStrategy.Tunnel, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento <see cref="E:System.Windows.DragDrop.DragEnter" /> anexado.</summary>
		// Token: 0x04000586 RID: 1414
		public static readonly RoutedEvent DragEnterEvent = EventManager.RegisterRoutedEvent("DragEnter", RoutingStrategy.Bubble, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragOver" /></summary>
		// Token: 0x04000587 RID: 1415
		public static readonly RoutedEvent PreviewDragOverEvent = EventManager.RegisterRoutedEvent("PreviewDragOver", RoutingStrategy.Tunnel, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.DragOver" /></summary>
		// Token: 0x04000588 RID: 1416
		public static readonly RoutedEvent DragOverEvent = EventManager.RegisterRoutedEvent("DragOver", RoutingStrategy.Bubble, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragLeave" /></summary>
		// Token: 0x04000589 RID: 1417
		public static readonly RoutedEvent PreviewDragLeaveEvent = EventManager.RegisterRoutedEvent("PreviewDragLeave", RoutingStrategy.Tunnel, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.DragLeave" /></summary>
		// Token: 0x0400058A RID: 1418
		public static readonly RoutedEvent DragLeaveEvent = EventManager.RegisterRoutedEvent("DragLeave", RoutingStrategy.Bubble, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.PreviewDrop" /></summary>
		// Token: 0x0400058B RID: 1419
		public static readonly RoutedEvent PreviewDropEvent = EventManager.RegisterRoutedEvent("PreviewDrop", RoutingStrategy.Tunnel, typeof(DragEventHandler), typeof(DragDrop));

		/// <summary>Identifica o evento anexado <see cref="E:System.Windows.DragDrop.Drop" /></summary>
		// Token: 0x0400058C RID: 1420
		public static readonly RoutedEvent DropEvent = EventManager.RegisterRoutedEvent("Drop", RoutingStrategy.Bubble, typeof(DragEventHandler), typeof(DragDrop));

		// Token: 0x0400058D RID: 1421
		internal static readonly RoutedEvent DragDropStartedEvent = EventManager.RegisterRoutedEvent("DragDropStarted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DragDrop));

		// Token: 0x0400058E RID: 1422
		internal static readonly RoutedEvent DragDropCompletedEvent = EventManager.RegisterRoutedEvent("DragDropCompleted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DragDrop));
	}
}
