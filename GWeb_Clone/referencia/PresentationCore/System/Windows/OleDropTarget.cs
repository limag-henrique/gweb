using System;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	// Token: 0x020001A8 RID: 424
	internal class OleDropTarget : DispatcherObject, UnsafeNativeMethods.IOleDropTarget
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0001D658 File Offset: 0x0001CA58
		public OleDropTarget(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this._windowHandle = handle;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D68C File Offset: 0x0001CA8C
		int UnsafeNativeMethods.IOleDropTarget.OleDragEnter(object data, int dragDropKeyStates, long point, ref int effects)
		{
			this._dataObject = this.GetDataObject(data);
			if (this._dataObject == null || !this.IsDataAvailable(this._dataObject))
			{
				effects = 0;
				return 1;
			}
			Point targetPoint;
			DependencyObject currentTarget = this.GetCurrentTarget(point, out targetPoint);
			this._lastTarget = currentTarget;
			if (currentTarget != null)
			{
				this.RaiseDragEvent(DragDrop.DragEnterEvent, dragDropKeyStates, ref effects, currentTarget, targetPoint);
			}
			else
			{
				effects = 0;
			}
			return 0;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D6F0 File Offset: 0x0001CAF0
		int UnsafeNativeMethods.IOleDropTarget.OleDragOver(int dragDropKeyStates, long point, ref int effects)
		{
			Invariant.Assert(this._dataObject != null);
			Point targetPoint;
			DependencyObject currentTarget = this.GetCurrentTarget(point, out targetPoint);
			if (currentTarget != null)
			{
				if (currentTarget != this._lastTarget)
				{
					try
					{
						if (this._lastTarget != null)
						{
							this.RaiseDragEvent(DragDrop.DragLeaveEvent, dragDropKeyStates, ref effects, this._lastTarget, targetPoint);
						}
						this.RaiseDragEvent(DragDrop.DragEnterEvent, dragDropKeyStates, ref effects, currentTarget, targetPoint);
						return 0;
					}
					finally
					{
						this._lastTarget = currentTarget;
					}
				}
				this.RaiseDragEvent(DragDrop.DragOverEvent, dragDropKeyStates, ref effects, currentTarget, targetPoint);
			}
			else
			{
				try
				{
					if (this._lastTarget != null)
					{
						this.RaiseDragEvent(DragDrop.DragLeaveEvent, dragDropKeyStates, ref effects, this._lastTarget, targetPoint);
					}
				}
				finally
				{
					this._lastTarget = currentTarget;
					effects = 0;
				}
			}
			return 0;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001D7C8 File Offset: 0x0001CBC8
		int UnsafeNativeMethods.IOleDropTarget.OleDragLeave()
		{
			if (this._lastTarget != null)
			{
				int num = 0;
				try
				{
					this.RaiseDragEvent(DragDrop.DragLeaveEvent, 0, ref num, this._lastTarget, new Point(0.0, 0.0));
				}
				finally
				{
					this._lastTarget = null;
					this._dataObject = null;
				}
			}
			return 0;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001D838 File Offset: 0x0001CC38
		int UnsafeNativeMethods.IOleDropTarget.OleDrop(object data, int dragDropKeyStates, long point, ref int effects)
		{
			IDataObject dataObject = this.GetDataObject(data);
			if (dataObject == null || !this.IsDataAvailable(dataObject))
			{
				effects = 0;
				return 1;
			}
			this._lastTarget = null;
			Point targetPoint;
			DependencyObject currentTarget = this.GetCurrentTarget(point, out targetPoint);
			if (currentTarget != null)
			{
				this.RaiseDragEvent(DragDrop.DropEvent, dragDropKeyStates, ref effects, currentTarget, targetPoint);
			}
			else
			{
				effects = 0;
			}
			return 0;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001D88C File Offset: 0x0001CC8C
		private void RaiseDragEvent(RoutedEvent dragEvent, int dragDropKeyStates, ref int effects, DependencyObject target, Point targetPoint)
		{
			Invariant.Assert(this._dataObject != null);
			Invariant.Assert(target != null);
			DragEventArgs dragEventArgs = new DragEventArgs(this._dataObject, (DragDropKeyStates)dragDropKeyStates, (DragDropEffects)effects, target, targetPoint);
			if (dragEvent == DragDrop.DragEnterEvent)
			{
				dragEventArgs.RoutedEvent = DragDrop.PreviewDragEnterEvent;
			}
			else if (dragEvent == DragDrop.DragOverEvent)
			{
				dragEventArgs.RoutedEvent = DragDrop.PreviewDragOverEvent;
			}
			else if (dragEvent == DragDrop.DragLeaveEvent)
			{
				dragEventArgs.RoutedEvent = DragDrop.PreviewDragLeaveEvent;
			}
			else if (dragEvent == DragDrop.DropEvent)
			{
				dragEventArgs.RoutedEvent = DragDrop.PreviewDropEvent;
			}
			if (target is UIElement)
			{
				((UIElement)target).RaiseEvent(dragEventArgs);
			}
			else if (target is ContentElement)
			{
				((ContentElement)target).RaiseEvent(dragEventArgs);
			}
			else
			{
				if (!(target is UIElement3D))
				{
					throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
				}
				((UIElement3D)target).RaiseEvent(dragEventArgs);
			}
			if (!dragEventArgs.Handled)
			{
				dragEventArgs.RoutedEvent = dragEvent;
				if (target is UIElement)
				{
					((UIElement)target).RaiseEvent(dragEventArgs);
				}
				else if (target is ContentElement)
				{
					((ContentElement)target).RaiseEvent(dragEventArgs);
				}
				else
				{
					if (!(target is UIElement3D))
					{
						throw new ArgumentException(SR.Get("ScopeMustBeUIElementOrContent"), "scope");
					}
					((UIElement3D)target).RaiseEvent(dragEventArgs);
				}
			}
			if (!dragEventArgs.Handled)
			{
				if (dragEvent == DragDrop.DragEnterEvent)
				{
					this.OnDefaultDragEnter(dragEventArgs);
				}
				else if (dragEvent == DragDrop.DragOverEvent)
				{
					this.OnDefaultDragOver(dragEventArgs);
				}
			}
			effects = (int)dragEventArgs.Effects;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001DA10 File Offset: 0x0001CE10
		private void OnDefaultDragEnter(DragEventArgs e)
		{
			if (e.Data == null)
			{
				e.Effects = DragDropEffects.None;
				return;
			}
			if ((e.AllowedEffects & DragDropEffects.Move) != DragDropEffects.None)
			{
				e.Effects = DragDropEffects.Move;
			}
			bool flag = (e.KeyStates & DragDropKeyStates.ControlKey) > DragDropKeyStates.None;
			if (flag)
			{
				e.Effects = DragDropEffects.Copy;
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001DA54 File Offset: 0x0001CE54
		private void OnDefaultDragOver(DragEventArgs e)
		{
			if (e.Data == null)
			{
				e.Effects = DragDropEffects.None;
				return;
			}
			if ((e.AllowedEffects & DragDropEffects.Move) != DragDropEffects.None)
			{
				e.Effects = DragDropEffects.Move;
			}
			bool flag = (e.KeyStates & DragDropKeyStates.ControlKey) > DragDropKeyStates.None;
			if (flag)
			{
				e.Effects = DragDropEffects.Copy;
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001DA98 File Offset: 0x0001CE98
		private Point GetClientPointFromScreenPoint(long dragPoint, PresentationSource source)
		{
			Point pointScreen = new Point((double)((int)(dragPoint & (long)((ulong)-1))), (double)((int)(dragPoint >> 32 & (long)((ulong)-1))));
			return PointUtil.ScreenToClient(pointScreen, source);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001DAC4 File Offset: 0x0001CEC4
		private DependencyObject GetCurrentTarget(long dragPoint, out Point targetPoint)
		{
			DependencyObject dependencyObject = null;
			HwndSource hwndSource = HwndSource.FromHwnd(this._windowHandle);
			targetPoint = this.GetClientPointFromScreenPoint(dragPoint, hwndSource);
			if (hwndSource != null)
			{
				dependencyObject = (MouseDevice.LocalHitTest(targetPoint, hwndSource) as DependencyObject);
				UIElement uielement = dependencyObject as UIElement;
				if (uielement != null)
				{
					if (uielement.AllowDrop)
					{
						dependencyObject = uielement;
					}
					else
					{
						dependencyObject = null;
					}
				}
				else
				{
					ContentElement contentElement = dependencyObject as ContentElement;
					if (contentElement != null)
					{
						if (contentElement.AllowDrop)
						{
							dependencyObject = contentElement;
						}
						else
						{
							dependencyObject = null;
						}
					}
					else
					{
						UIElement3D uielement3D = dependencyObject as UIElement3D;
						if (uielement3D != null)
						{
							if (uielement3D.AllowDrop)
							{
								dependencyObject = uielement3D;
							}
							else
							{
								dependencyObject = null;
							}
						}
					}
				}
				if (dependencyObject != null)
				{
					targetPoint = PointUtil.ClientToRoot(targetPoint, hwndSource);
					targetPoint = InputElement.TranslatePoint(targetPoint, hwndSource.RootVisual, dependencyObject);
				}
			}
			return dependencyObject;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001DB84 File Offset: 0x0001CF84
		private IDataObject GetDataObject(object data)
		{
			IDataObject result = null;
			if (data != null)
			{
				if (data is DataObject)
				{
					result = (DataObject)data;
				}
				else
				{
					result = new DataObject((IDataObject)data);
				}
			}
			return result;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001DBB4 File Offset: 0x0001CFB4
		private bool IsDataAvailable(IDataObject dataObject)
		{
			bool result = false;
			if (dataObject != null)
			{
				string[] formats = dataObject.GetFormats();
				for (int i = 0; i < formats.Length; i++)
				{
					if (dataObject.GetDataPresent(formats[i]))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x04000590 RID: 1424
		private IntPtr _windowHandle;

		// Token: 0x04000591 RID: 1425
		private IDataObject _dataObject;

		// Token: 0x04000592 RID: 1426
		private DependencyObject _lastTarget;
	}
}
