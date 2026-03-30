using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input.Tracing;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002EE RID: 750
	internal class PointerStylusPlugInManager
	{
		// Token: 0x060017BC RID: 6076 RVA: 0x0005E7B8 File Offset: 0x0005DBB8
		internal PointerStylusPlugInManager(PresentationSource source)
		{
			this._inputSource = new SecurityCriticalData<PresentationSource>(source);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005E7E4 File Offset: 0x0005DBE4
		internal void AddStylusPlugInCollection(StylusPlugInCollection pic)
		{
			this._plugInCollectionList.Insert(this.FindZOrderIndex(pic), pic);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0005E804 File Offset: 0x0005DC04
		internal void RemoveStylusPlugInCollection(StylusPlugInCollection pic)
		{
			this._plugInCollectionList.Remove(pic);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0005E820 File Offset: 0x0005DC20
		[SecuritySafeCritical]
		private Matrix GetTabletToViewTransform(TabletDevice tablet)
		{
			HwndSource hwndSource = this._inputSource.Value as HwndSource;
			Matrix? matrix;
			if (hwndSource == null)
			{
				matrix = null;
			}
			else
			{
				HwndTarget compositionTarget = hwndSource.CompositionTarget;
				matrix = ((compositionTarget != null) ? new Matrix?(compositionTarget.TransformToDevice) : null);
			}
			Matrix trans = matrix ?? Matrix.Identity;
			trans.Invert();
			return trans * tablet.TabletDeviceImpl.TabletToScreen;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005E8A0 File Offset: 0x0005DCA0
		internal int FindZOrderIndex(StylusPlugInCollection spicAdding)
		{
			DependencyObject dependencyObject = spicAdding.Element;
			int i;
			for (i = 0; i < this._plugInCollectionList.Count; i++)
			{
				DependencyObject dependencyObject2 = this._plugInCollectionList[i].Element;
				if (VisualTreeHelper.IsAncestorOf(dependencyObject, dependencyObject2))
				{
					for (i++; i < this._plugInCollectionList.Count; i++)
					{
						dependencyObject2 = this._plugInCollectionList[i].Element;
						if (!VisualTreeHelper.IsAncestorOf(dependencyObject, dependencyObject2))
						{
							break;
						}
					}
					return i;
				}
				DependencyObject dependencyObject3 = VisualTreeHelper.FindCommonAncestor(dependencyObject, dependencyObject2);
				if (dependencyObject3 != null)
				{
					if (dependencyObject2 == dependencyObject3)
					{
						return i;
					}
					while (VisualTreeHelper.GetParentInternal(dependencyObject) != dependencyObject3)
					{
						dependencyObject = VisualTreeHelper.GetParentInternal(dependencyObject);
					}
					while (VisualTreeHelper.GetParentInternal(dependencyObject2) != dependencyObject3)
					{
						dependencyObject2 = VisualTreeHelper.GetParentInternal(dependencyObject2);
					}
					int childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject3);
					for (int j = 0; j < childrenCount; j++)
					{
						DependencyObject child = VisualTreeHelper.GetChild(dependencyObject3, j);
						if (child == dependencyObject)
						{
							return i;
						}
						if (child == dependencyObject2)
						{
							break;
						}
					}
				}
			}
			return i;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005E984 File Offset: 0x0005DD84
		[SecurityCritical]
		internal StylusPlugInCollection TargetPlugInCollection(RawStylusInputReport inputReport)
		{
			PointerStylusDevice pointerStylusDevice = inputReport.StylusDevice.As<PointerStylusDevice>();
			bool flag = false;
			StylusPlugInCollection stylusPlugInCollection = pointerStylusDevice.GetCapturedPlugInCollection(ref flag);
			int inputArrayLengthPerPoint = inputReport.StylusPointDescription.GetInputArrayLengthPerPoint();
			if (flag && !this._plugInCollectionList.Contains(stylusPlugInCollection))
			{
				flag = false;
			}
			if (!flag && inputReport.Data != null && inputReport.Data.Length >= inputArrayLengthPerPoint)
			{
				int[] data = inputReport.Data;
				Point point = new Point((double)data[data.Length - inputArrayLengthPerPoint], (double)data[data.Length - inputArrayLengthPerPoint + 1]);
				point *= pointerStylusDevice.TabletDevice.TabletDeviceImpl.TabletToScreen;
				point.X = (double)((int)Math.Round(point.X));
				point.Y = (double)((int)Math.Round(point.Y));
				point *= inputReport.InputSource.CompositionTarget.TransformFromDevice;
				stylusPlugInCollection = this.HittestPlugInCollection(point);
			}
			return stylusPlugInCollection;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005EA70 File Offset: 0x0005DE70
		internal StylusPlugInCollection FindPlugInCollection(UIElement element)
		{
			foreach (StylusPlugInCollection stylusPlugInCollection in this._plugInCollectionList)
			{
				if (stylusPlugInCollection.Element == element || stylusPlugInCollection.Element.IsAncestorOf(element))
				{
					return stylusPlugInCollection;
				}
			}
			return null;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005EAE8 File Offset: 0x0005DEE8
		private StylusPlugInCollection HittestPlugInCollection(Point pt)
		{
			foreach (StylusPlugInCollection stylusPlugInCollection in this._plugInCollectionList)
			{
				if (stylusPlugInCollection.IsHit(pt))
				{
					return stylusPlugInCollection;
				}
			}
			return null;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005EB50 File Offset: 0x0005DF50
		[SecurityCritical]
		internal void VerifyStylusPlugInCollectionTarget(RawStylusInputReport rawStylusInputReport)
		{
			RawStylusActions actions = rawStylusInputReport.Actions;
			if (actions != RawStylusActions.Down && actions != RawStylusActions.Up && actions != RawStylusActions.Move)
			{
				return;
			}
			PointerLogic currentStylusLogicAs = StylusLogic.GetCurrentStylusLogicAs<PointerLogic>();
			RawStylusInput rawStylusInput = rawStylusInputReport.RawStylusInput;
			StylusPlugInCollection stylusPlugInCollection = null;
			StylusPlugInCollection stylusPlugInCollection2 = (rawStylusInput != null) ? rawStylusInput.Target : null;
			bool flag = false;
			UIElement uielement = InputElement.GetContainingUIElement(rawStylusInputReport.StylusDevice.DirectlyOver as DependencyObject) as UIElement;
			if (uielement != null)
			{
				stylusPlugInCollection = this.FindPlugInCollection(uielement);
			}
			using (Dispatcher.CurrentDispatcher.DisableProcessing())
			{
				if (stylusPlugInCollection2 != null && stylusPlugInCollection2 != stylusPlugInCollection && rawStylusInput != null)
				{
					foreach (RawStylusInputCustomData rawStylusInputCustomData in rawStylusInput.CustomDataList)
					{
						rawStylusInputCustomData.Owner.FireCustomData(rawStylusInputCustomData.Data, rawStylusInputReport.Actions, false);
					}
					flag = rawStylusInput.StylusPointsModified;
					rawStylusInputReport.RawStylusInput = null;
				}
				bool flag2 = false;
				if (stylusPlugInCollection != null && rawStylusInputReport.RawStylusInput == null)
				{
					GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
					generalTransformGroup.Children.Add(rawStylusInputReport.StylusDevice.As<PointerStylusDevice>().GetTabletToElementTransform(null));
					generalTransformGroup.Children.Add(stylusPlugInCollection.ViewToElement);
					generalTransformGroup.Freeze();
					RawStylusInput rawStylusInput2 = new RawStylusInput(rawStylusInputReport, generalTransformGroup, stylusPlugInCollection);
					rawStylusInputReport.RawStylusInput = rawStylusInput2;
					flag2 = true;
				}
				PointerStylusDevice pointerStylusDevice = rawStylusInputReport.StylusDevice.As<PointerStylusDevice>();
				StylusPlugInCollection currentVerifiedTarget = pointerStylusDevice.CurrentVerifiedTarget;
				if (stylusPlugInCollection != currentVerifiedTarget)
				{
					if (currentVerifiedTarget != null)
					{
						if (rawStylusInput == null)
						{
							GeneralTransformGroup generalTransformGroup2 = new GeneralTransformGroup();
							generalTransformGroup2.Children.Add(pointerStylusDevice.GetTabletToElementTransform(null));
							generalTransformGroup2.Children.Add(currentVerifiedTarget.ViewToElement);
							generalTransformGroup2.Freeze();
							rawStylusInput = new RawStylusInput(rawStylusInputReport, generalTransformGroup2, currentVerifiedTarget);
						}
						currentVerifiedTarget.FireEnterLeave(false, rawStylusInput, true);
					}
					if (stylusPlugInCollection != null)
					{
						stylusPlugInCollection.FireEnterLeave(true, rawStylusInputReport.RawStylusInput, true);
						currentStylusLogicAs.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
					}
					pointerStylusDevice.CurrentVerifiedTarget = stylusPlugInCollection;
				}
				if (flag2)
				{
					stylusPlugInCollection.FireRawStylusInput(rawStylusInputReport.RawStylusInput);
					flag = (flag || rawStylusInputReport.RawStylusInput.StylusPointsModified);
					currentStylusLogicAs.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
				}
				if (stylusPlugInCollection != null)
				{
					foreach (RawStylusInputCustomData rawStylusInputCustomData2 in rawStylusInputReport.RawStylusInput.CustomDataList)
					{
						rawStylusInputCustomData2.Owner.FireCustomData(rawStylusInputCustomData2.Data, rawStylusInputReport.Actions, true);
					}
				}
				if (flag)
				{
					rawStylusInputReport.StylusDevice.As<PointerStylusDevice>().UpdateEventStylusPoints(rawStylusInputReport, true);
				}
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0005EE20 File Offset: 0x0005E220
		[SecurityCritical]
		internal StylusPlugInCollection InvokeStylusPluginCollectionForMouse(RawStylusInputReport inputReport, IInputElement directlyOver, StylusPlugInCollection currentPlugInCollection)
		{
			StylusPlugInCollection stylusPlugInCollection = null;
			if (directlyOver != null)
			{
				UIElement uielement = InputElement.GetContainingUIElement(directlyOver as DependencyObject) as UIElement;
				if (uielement != null)
				{
					stylusPlugInCollection = this.FindPlugInCollection(uielement);
				}
			}
			if (currentPlugInCollection != null && currentPlugInCollection != stylusPlugInCollection)
			{
				RawStylusInput rawStylusInput = new RawStylusInput(inputReport, currentPlugInCollection.ViewToElement, currentPlugInCollection);
				currentPlugInCollection.FireEnterLeave(false, rawStylusInput, true);
				StylusLogic.CurrentStylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
			}
			if (stylusPlugInCollection != null)
			{
				RawStylusInput rawStylusInput2 = new RawStylusInput(inputReport, stylusPlugInCollection.ViewToElement, stylusPlugInCollection);
				inputReport.RawStylusInput = rawStylusInput2;
				if (stylusPlugInCollection != currentPlugInCollection)
				{
					stylusPlugInCollection.FireEnterLeave(true, rawStylusInput2, true);
				}
				stylusPlugInCollection.FireRawStylusInput(rawStylusInput2);
				StylusLogic.CurrentStylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
				foreach (RawStylusInputCustomData rawStylusInputCustomData in rawStylusInput2.CustomDataList)
				{
					rawStylusInputCustomData.Owner.FireCustomData(rawStylusInputCustomData.Data, inputReport.Actions, true);
				}
			}
			return stylusPlugInCollection;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005EF2C File Offset: 0x0005E32C
		[SecurityCritical]
		internal void InvokeStylusPluginCollection(RawStylusInputReport inputReport)
		{
			RawStylusActions actions = inputReport.Actions;
			if (actions == RawStylusActions.Down || actions == RawStylusActions.Up || actions == RawStylusActions.Move)
			{
				StylusPlugInCollection stylusPlugInCollection = this.TargetPlugInCollection(inputReport);
				PointerStylusDevice pointerStylusDevice = inputReport.StylusDevice.As<PointerStylusDevice>();
				StylusPlugInCollection currentVerifiedTarget = pointerStylusDevice.CurrentVerifiedTarget;
				if (currentVerifiedTarget != null && currentVerifiedTarget != stylusPlugInCollection)
				{
					GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
					generalTransformGroup.Children.Add(new MatrixTransform(this.GetTabletToViewTransform(pointerStylusDevice.TabletDevice)));
					generalTransformGroup.Children.Add(currentVerifiedTarget.ViewToElement);
					generalTransformGroup.Freeze();
					RawStylusInput rawStylusInput = new RawStylusInput(inputReport, generalTransformGroup, currentVerifiedTarget);
					currentVerifiedTarget.FireEnterLeave(false, rawStylusInput, false);
					StylusLogic.CurrentStylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
					pointerStylusDevice.CurrentVerifiedTarget = null;
				}
				if (stylusPlugInCollection != null)
				{
					GeneralTransformGroup generalTransformGroup2 = new GeneralTransformGroup();
					generalTransformGroup2.Children.Add(new MatrixTransform(this.GetTabletToViewTransform(pointerStylusDevice.TabletDevice)));
					generalTransformGroup2.Children.Add(stylusPlugInCollection.ViewToElement);
					generalTransformGroup2.Freeze();
					RawStylusInput rawStylusInput2 = new RawStylusInput(inputReport, generalTransformGroup2, stylusPlugInCollection);
					inputReport.RawStylusInput = rawStylusInput2;
					if (stylusPlugInCollection != currentVerifiedTarget)
					{
						pointerStylusDevice.CurrentVerifiedTarget = stylusPlugInCollection;
						stylusPlugInCollection.FireEnterLeave(true, rawStylusInput2, false);
					}
					stylusPlugInCollection.FireRawStylusInput(rawStylusInput2);
					StylusLogic.CurrentStylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
				}
				return;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005F070 File Offset: 0x0005E470
		[SecurityCritical]
		internal static void InvokePlugInsForMouse(ProcessInputEventArgs e)
		{
			if (!e.StagingItem.Input.Handled)
			{
				if (e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseDownEvent && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseUpEvent && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseMoveEvent && e.StagingItem.Input.RoutedEvent != InputManager.InputReportEvent)
				{
					return;
				}
				RawStylusActions actions = RawStylusActions.None;
				MouseDevice mouseDevice;
				bool flag;
				bool flag2;
				int timestamp;
				PresentationSource presentationSource;
				if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
				{
					StylusPlugInCollection activeMousePlugInCollection = PointerStylusPlugInManager._activeMousePlugInCollection;
					if (((activeMousePlugInCollection != null) ? activeMousePlugInCollection.Element : null) == null)
					{
						return;
					}
					InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
					if (inputReportEventArgs.Report.Type != InputType.Mouse)
					{
						return;
					}
					RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
					if ((rawMouseInputReport.Actions & RawMouseActions.Deactivate) != RawMouseActions.Deactivate)
					{
						return;
					}
					mouseDevice = InputManager.UnsecureCurrent.PrimaryMouseDevice;
					if (mouseDevice == null || mouseDevice.DirectlyOver != null)
					{
						return;
					}
					flag = (mouseDevice.LeftButton == MouseButtonState.Pressed);
					flag2 = (mouseDevice.RightButton == MouseButtonState.Pressed);
					timestamp = rawMouseInputReport.Timestamp;
					presentationSource = PresentationSource.CriticalFromVisual(PointerStylusPlugInManager._activeMousePlugInCollection.Element);
				}
				else
				{
					MouseEventArgs mouseEventArgs = e.StagingItem.Input as MouseEventArgs;
					mouseDevice = mouseEventArgs.MouseDevice;
					flag = (mouseDevice.LeftButton == MouseButtonState.Pressed);
					flag2 = (mouseDevice.RightButton == MouseButtonState.Pressed);
					if (mouseEventArgs.StylusDevice != null && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseUpEvent)
					{
						return;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseMoveEvent)
					{
						if (!flag)
						{
							return;
						}
						actions = RawStylusActions.Move;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseDownEvent)
					{
						MouseButtonEventArgs mouseButtonEventArgs = mouseEventArgs as MouseButtonEventArgs;
						if (mouseButtonEventArgs.ChangedButton != MouseButton.Left)
						{
							return;
						}
						actions = RawStylusActions.Down;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseUpEvent)
					{
						MouseButtonEventArgs mouseButtonEventArgs2 = mouseEventArgs as MouseButtonEventArgs;
						if (mouseButtonEventArgs2.ChangedButton != MouseButton.Left)
						{
							return;
						}
						actions = RawStylusActions.Up;
					}
					timestamp = mouseEventArgs.Timestamp;
					Visual visual = mouseDevice.DirectlyOver as Visual;
					if (visual == null)
					{
						return;
					}
					presentationSource = PresentationSource.CriticalFromVisual(visual);
				}
				if (presentationSource != null && presentationSource.CompositionTarget != null && !presentationSource.CompositionTarget.IsDisposed)
				{
					IInputElement directlyOver = mouseDevice.DirectlyOver;
					int num = (flag ? 1 : 0) | (flag2 ? 9 : 0);
					Point point = mouseDevice.GetPosition(presentationSource.RootVisual as IInputElement);
					point = presentationSource.CompositionTarget.TransformToDevice.Transform(point);
					int num2 = (flag ? 1 : 0) | (flag2 ? 3 : 0);
					int[] data = new int[]
					{
						(int)point.X,
						(int)point.Y,
						num,
						num2
					};
					RawStylusInputReport inputReport = new RawStylusInputReport(InputMode.Foreground, timestamp, presentationSource, actions, () => PointerStylusPlugInManager.MousePointDescription, 0, 0, data);
					PointerLogic currentStylusLogicAs = StylusLogic.GetCurrentStylusLogicAs<PointerLogic>();
					PointerStylusPlugInManager pointerStylusPlugInManager = (currentStylusLogicAs != null) ? currentStylusLogicAs.GetManagerForSource(presentationSource) : null;
					using (Dispatcher.CurrentDispatcher.DisableProcessing())
					{
						PointerStylusPlugInManager._activeMousePlugInCollection = ((pointerStylusPlugInManager != null) ? pointerStylusPlugInManager.InvokeStylusPluginCollectionForMouse(inputReport, directlyOver, PointerStylusPlugInManager._activeMousePlugInCollection) : null);
					}
				}
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0005F3C8 File Offset: 0x0005E7C8
		private static StylusPointDescription MousePointDescription
		{
			get
			{
				if (PointerStylusPlugInManager._mousePointDescription == null)
				{
					PointerStylusPlugInManager._mousePointDescription = new StylusPointDescription(new StylusPointPropertyInfo[]
					{
						StylusPointPropertyInfoDefaults.X,
						StylusPointPropertyInfoDefaults.Y,
						StylusPointPropertyInfoDefaults.NormalPressure,
						StylusPointPropertyInfoDefaults.PacketStatus,
						StylusPointPropertyInfoDefaults.TipButton,
						StylusPointPropertyInfoDefaults.BarrelButton
					}, -1);
				}
				return PointerStylusPlugInManager._mousePointDescription;
			}
		}

		// Token: 0x04000D03 RID: 3331
		private static StylusPointDescription _mousePointDescription;

		// Token: 0x04000D04 RID: 3332
		internal SecurityCriticalData<PresentationSource> _inputSource;

		// Token: 0x04000D05 RID: 3333
		private List<StylusPlugInCollection> _plugInCollectionList = new List<StylusPlugInCollection>();

		// Token: 0x04000D06 RID: 3334
		[ThreadStatic]
		private static StylusPlugInCollection _activeMousePlugInCollection;
	}
}
