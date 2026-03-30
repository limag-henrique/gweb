using System;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	// Token: 0x020001A7 RID: 423
	internal class OleDragSource : UnsafeNativeMethods.IOleDropSource
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0001D2FC File Offset: 0x0001C6FC
		public OleDragSource(DependencyObject dragSource)
		{
			this._dragSource = dragSource;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001D318 File Offset: 0x0001C718
		int UnsafeNativeMethods.IOleDropSource.OleQueryContinueDrag(int escapeKey, int grfkeyState)
		{
			bool escapePressed = false;
			if (escapeKey != 0)
			{
				escapePressed = true;
			}
			QueryContinueDragEventArgs queryContinueDragEventArgs = new QueryContinueDragEventArgs(escapePressed, (DragDropKeyStates)grfkeyState);
			this.RaiseQueryContinueDragEvent(queryContinueDragEventArgs);
			if (queryContinueDragEventArgs.Action == DragAction.Continue)
			{
				return 0;
			}
			if (queryContinueDragEventArgs.Action == DragAction.Drop)
			{
				return 262400;
			}
			if (queryContinueDragEventArgs.Action == DragAction.Cancel)
			{
				return 262401;
			}
			return 0;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001D364 File Offset: 0x0001C764
		int UnsafeNativeMethods.IOleDropSource.OleGiveFeedback(int effect)
		{
			GiveFeedbackEventArgs giveFeedbackEventArgs = new GiveFeedbackEventArgs((DragDropEffects)effect, false);
			this.RaiseGiveFeedbackEvent(giveFeedbackEventArgs);
			if (giveFeedbackEventArgs.UseDefaultCursors)
			{
				return 262402;
			}
			return 0;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001D390 File Offset: 0x0001C790
		private void RaiseQueryContinueDragEvent(QueryContinueDragEventArgs args)
		{
			args.RoutedEvent = DragDrop.PreviewQueryContinueDragEvent;
			if (this._dragSource is UIElement)
			{
				((UIElement)this._dragSource).RaiseEvent(args);
			}
			else if (this._dragSource is ContentElement)
			{
				((ContentElement)this._dragSource).RaiseEvent(args);
			}
			else
			{
				if (!(this._dragSource is UIElement3D))
				{
					throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
				}
				((UIElement3D)this._dragSource).RaiseEvent(args);
			}
			args.RoutedEvent = DragDrop.QueryContinueDragEvent;
			if (!args.Handled)
			{
				if (this._dragSource is UIElement)
				{
					((UIElement)this._dragSource).RaiseEvent(args);
				}
				else if (this._dragSource is ContentElement)
				{
					((ContentElement)this._dragSource).RaiseEvent(args);
				}
				else
				{
					if (!(this._dragSource is UIElement3D))
					{
						throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
					}
					((UIElement3D)this._dragSource).RaiseEvent(args);
				}
			}
			if (!args.Handled)
			{
				this.OnDefaultQueryContinueDrag(args);
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D4B8 File Offset: 0x0001C8B8
		private void RaiseGiveFeedbackEvent(GiveFeedbackEventArgs args)
		{
			args.RoutedEvent = DragDrop.PreviewGiveFeedbackEvent;
			if (this._dragSource is UIElement)
			{
				((UIElement)this._dragSource).RaiseEvent(args);
			}
			else if (this._dragSource is ContentElement)
			{
				((ContentElement)this._dragSource).RaiseEvent(args);
			}
			else
			{
				if (!(this._dragSource is UIElement3D))
				{
					throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
				}
				((UIElement3D)this._dragSource).RaiseEvent(args);
			}
			args.RoutedEvent = DragDrop.GiveFeedbackEvent;
			if (!args.Handled)
			{
				if (this._dragSource is UIElement)
				{
					((UIElement)this._dragSource).RaiseEvent(args);
				}
				else if (this._dragSource is ContentElement)
				{
					((ContentElement)this._dragSource).RaiseEvent(args);
				}
				else
				{
					if (!(this._dragSource is UIElement3D))
					{
						throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
					}
					((UIElement3D)this._dragSource).RaiseEvent(args);
				}
			}
			if (!args.Handled)
			{
				this.OnDefaultGiveFeedback(args);
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D5E0 File Offset: 0x0001C9E0
		private void OnDefaultQueryContinueDrag(QueryContinueDragEventArgs e)
		{
			int num = 0;
			if ((e.KeyStates & DragDropKeyStates.LeftMouseButton) == DragDropKeyStates.LeftMouseButton)
			{
				num++;
			}
			if ((e.KeyStates & DragDropKeyStates.MiddleMouseButton) == DragDropKeyStates.MiddleMouseButton)
			{
				num++;
			}
			if ((e.KeyStates & DragDropKeyStates.RightMouseButton) == DragDropKeyStates.RightMouseButton)
			{
				num++;
			}
			e.Action = DragAction.Continue;
			if (e.EscapePressed || num >= 2)
			{
				e.Action = DragAction.Cancel;
				return;
			}
			if (num == 0)
			{
				e.Action = DragAction.Drop;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D644 File Offset: 0x0001CA44
		private void OnDefaultGiveFeedback(GiveFeedbackEventArgs e)
		{
			e.UseDefaultCursors = true;
		}

		// Token: 0x0400058F RID: 1423
		private DependencyObject _dragSource;
	}
}
