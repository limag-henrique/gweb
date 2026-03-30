using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input.Tracing;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002EB RID: 747
	internal class PointerLogic : StylusLogic
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0005BE44 File Offset: 0x0005B244
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x0005BE58 File Offset: 0x0005B258
		internal Dictionary<PresentationSource, PointerStylusPlugInManager> PlugInManagers { [SecurityCritical] get; [SecurityCritical] private set; } = new Dictionary<PresentationSource, PointerStylusPlugInManager>();

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0005BE6C File Offset: 0x0005B26C
		internal bool InDragDrop
		{
			get
			{
				return this._inDragDrop;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0005BE80 File Offset: 0x0005B280
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x0005BE94 File Offset: 0x0005B294
		internal static bool IsEnabled { get; private set; } = true;

		// Token: 0x06001753 RID: 5971 RVA: 0x0005BEA8 File Offset: 0x0005B2A8
		[SecuritySafeCritical]
		internal PointerLogic(InputManager inputManager)
		{
			base.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.PointerStackEnabled;
			this._inputManager = new SecurityCriticalData<InputManager>(inputManager);
			this._inputManager.Value.PreProcessInput += this.PreProcessInput;
			this._inputManager.Value.PreNotifyInput += this.PreNotifyInput;
			this._inputManager.Value.PostProcessInput += this.PostProcessInput;
			this._overIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsEnabledChanged);
			this._overIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsVisibleChanged);
			this._overIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsHitTestVisibleChanged);
			this._reevaluateStylusOverDelegate = new DispatcherOperationCallback(this.ReevaluateStylusOverAsync);
			this._reevaluateStylusOverOperation = null;
			this._captureIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsEnabledChanged);
			this._captureIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsVisibleChanged);
			this._captureIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsHitTestVisibleChanged);
			this._reevaluateCaptureDelegate = new DispatcherOperationCallback(this.ReevaluateCaptureAsync);
			this._reevaluateCaptureOperation = null;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005C01C File Offset: 0x0005B41C
		[SecurityCritical]
		private void PreNotifyInput(object sender, NotifyInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Stylus)
				{
					RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
					PointerStylusDevice pointerStylusDevice = rawStylusInputReport.StylusDevice.As<PointerStylusDevice>();
					if (!this._inDragDrop && pointerStylusDevice.CurrentPointerProvider.IsWindowEnabled)
					{
						Point position = pointerStylusDevice.GetPosition(null);
						IInputElement newOver = pointerStylusDevice.FindTarget(pointerStylusDevice.CriticalActiveSource, position);
						this.SelectStylusDevice(pointerStylusDevice, newOver, true);
					}
					else
					{
						this.SelectStylusDevice(pointerStylusDevice, null, false);
					}
					this._inputManager.Value.MostRecentInputDevice = pointerStylusDevice.StylusDevice;
					PointerStylusPlugInManager managerForSource = this.GetManagerForSource(pointerStylusDevice.ActiveSource);
					if (managerForSource != null)
					{
						managerForSource.VerifyStylusPlugInCollectionTarget(rawStylusInputReport);
					}
				}
			}
			this.UpdateTapCount(e);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0005C100 File Offset: 0x0005B500
		[SecurityCritical]
		private void PreProcessMouseInput(PreProcessInputEventArgs e, InputReportEventArgs input)
		{
			RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)input.Report;
			bool flag = StylusLogic.IsPromotedMouseEvent(rawMouseInputReport);
			StylusDevice stylusDevice = input.Device as StylusDevice;
			PointerStylusDevice pointerStylusDevice;
			if (stylusDevice == null)
			{
				pointerStylusDevice = null;
			}
			else
			{
				StylusDeviceBase stylusDeviceImpl = stylusDevice.StylusDeviceImpl;
				pointerStylusDevice = ((stylusDeviceImpl != null) ? stylusDeviceImpl.As<PointerStylusDevice>() : null);
			}
			PointerStylusDevice pointerStylusDevice2 = pointerStylusDevice;
			if (flag)
			{
				StylusDeviceBase currentStylusDevice = this.CurrentStylusDevice;
				bool? flag2;
				if (currentStylusDevice == null)
				{
					flag2 = null;
				}
				else
				{
					PointerStylusDevice pointerStylusDevice3 = currentStylusDevice.As<PointerStylusDevice>();
					if (pointerStylusDevice3 == null)
					{
						flag2 = null;
					}
					else
					{
						PointerTouchDevice touchDevice = pointerStylusDevice3.TouchDevice;
						flag2 = ((touchDevice != null) ? new bool?(touchDevice.PromotingToOther) : null);
					}
				}
				bool? flag3 = flag2;
				if (!flag3.GetValueOrDefault())
				{
					StylusDeviceBase currentStylusDevice2 = this.CurrentStylusDevice;
					bool? flag4;
					if (currentStylusDevice2 == null)
					{
						flag4 = null;
					}
					else
					{
						PointerStylusDevice pointerStylusDevice4 = currentStylusDevice2.As<PointerStylusDevice>();
						if (pointerStylusDevice4 == null)
						{
							flag4 = null;
						}
						else
						{
							PointerTouchDevice touchDevice2 = pointerStylusDevice4.TouchDevice;
							flag4 = ((touchDevice2 != null) ? new bool?(touchDevice2.PromotingToManipulation) : null);
						}
					}
					flag3 = flag4;
					if (flag3.GetValueOrDefault())
					{
						if ((rawMouseInputReport.Actions & RawMouseActions.Activate) == RawMouseActions.Activate)
						{
							MouseDevice.PushActivateInputReport(e, input, rawMouseInputReport, true);
						}
						input.Handled = true;
						e.Cancel();
						return;
					}
				}
			}
			if (!flag && pointerStylusDevice2 == null)
			{
				RawMouseActions actions = rawMouseInputReport.Actions;
				if (actions != RawMouseActions.AbsoluteMove)
				{
					if (actions != RawMouseActions.CancelCapture)
					{
						return;
					}
					StylusDeviceBase currentStylusDevice3 = this.CurrentStylusDevice;
					if (currentStylusDevice3 != null && currentStylusDevice3.InRange)
					{
						RawMouseInputReport report = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, rawMouseInputReport.Actions, 0, 0, 0, IntPtr.Zero);
						InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(this.CurrentStylusDevice.StylusDevice, report);
						inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
						this._inputManager.Value.ProcessInput(inputReportEventArgs);
						e.Cancel();
					}
				}
				else
				{
					StylusDeviceBase currentStylusDevice4 = this.CurrentStylusDevice;
					if (currentStylusDevice4 != null && currentStylusDevice4.InRange)
					{
						e.Cancel();
						input.Handled = true;
						return;
					}
				}
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0005C2CC File Offset: 0x0005B6CC
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (inputReportEventArgs != null && !inputReportEventArgs.Handled)
				{
					if (this._inDragDrop != this._inputManager.Value.InDragDrop)
					{
						this._inDragDrop = this._inputManager.Value.InDragDrop;
					}
					if (inputReportEventArgs.Report.Type == InputType.Mouse)
					{
						this.PreProcessMouseInput(e, inputReportEventArgs);
						return;
					}
					if (inputReportEventArgs.Report.Type == InputType.Stylus)
					{
						RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
						SystemGesture? systemGesture = rawStylusInputReport.StylusDevice.TabletDevice.TabletDeviceImpl.GenerateStaticGesture(rawStylusInputReport);
						if (systemGesture != null)
						{
							this.GenerateGesture(rawStylusInputReport, systemGesture.Value);
						}
					}
				}
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0005C3A4 File Offset: 0x0005B7A4
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == Mouse.LostMouseCaptureEvent || e.StagingItem.Input.RoutedEvent == Mouse.GotMouseCaptureEvent)
			{
				foreach (object obj in ((IEnumerable)this.TabletDevices))
				{
					TabletDevice tabletDevice = (TabletDevice)obj;
					foreach (StylusDevice stylusDevice in tabletDevice.StylusDevices)
					{
						stylusDevice.Capture(Mouse.Captured, Mouse.CapturedMode);
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled)
				{
					InputType type = inputReportEventArgs.Report.Type;
					if (type == InputType.Stylus)
					{
						RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
						PointerStylusDevice pointerStylusDevice = rawStylusInputReport.StylusDevice.As<PointerStylusDevice>();
						if (!this._inDragDrop)
						{
							if (pointerStylusDevice.CurrentPointerProvider.IsWindowEnabled)
							{
								this.PromoteRawToPreview(rawStylusInputReport, e);
							}
							else if ((rawStylusInputReport.Actions & RawStylusActions.Up) != RawStylusActions.None && pointerStylusDevice != null)
							{
								PointerTouchDevice touchDevice = pointerStylusDevice.TouchDevice;
								if (touchDevice.IsActive)
								{
									touchDevice.OnDeactivate();
								}
							}
						}
						else if ((pointerStylusDevice == null || !pointerStylusDevice.IsPrimary) && (rawStylusInputReport.Actions & RawStylusActions.Up) != RawStylusActions.None)
						{
							PointerTouchDevice touchDevice2 = pointerStylusDevice.TouchDevice;
							if (touchDevice2.IsActive)
							{
								touchDevice2.OnDeactivate();
							}
						}
					}
				}
			}
			PointerStylusPlugInManager.InvokePlugInsForMouse(e);
			this.PromotePreviewToMain(e);
			this.PromoteMainToOther(e);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0005C57C File Offset: 0x0005B97C
		internal override StylusDeviceBase CurrentStylusDevice
		{
			get
			{
				return this._currentStylusDevice;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0005C590 File Offset: 0x0005B990
		internal override TabletDeviceCollection TabletDevices
		{
			get
			{
				if (!this._initialDeviceRefreshDone)
				{
					this._pointerDevices.Refresh();
					this._initialDeviceRefreshDone = true;
					StylusTraceLogger.LogStartup();
					base.ShutdownListener = new StylusLogic.StylusLogicShutDownListener(this, ShutDownEvents.DispatcherShutdown);
				}
				return this._pointerDevices;
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005C5D0 File Offset: 0x0005B9D0
		[SecuritySafeCritical]
		internal override Point DeviceUnitsFromMeasureUnits(Point measurePoint)
		{
			PointerStylusDevice currentStylusDevice = this._currentStylusDevice;
			Matrix? matrix;
			if (currentStylusDevice == null)
			{
				matrix = null;
			}
			else
			{
				PresentationSource activeSource = currentStylusDevice.ActiveSource;
				if (activeSource == null)
				{
					matrix = null;
				}
				else
				{
					CompositionTarget compositionTarget = activeSource.CompositionTarget;
					matrix = ((compositionTarget != null) ? new Matrix?(compositionTarget.TransformToDevice) : null);
				}
			}
			Point point = measurePoint * (matrix ?? Matrix.Identity);
			return new Point(Math.Round(point.X), Math.Round(point.Y));
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005C660 File Offset: 0x0005BA60
		[SecuritySafeCritical]
		internal override Point MeasureUnitsFromDeviceUnits(Point devicePoint)
		{
			PointerStylusDevice currentStylusDevice = this._currentStylusDevice;
			Matrix? matrix;
			if (currentStylusDevice == null)
			{
				matrix = null;
			}
			else
			{
				PresentationSource activeSource = currentStylusDevice.ActiveSource;
				if (activeSource == null)
				{
					matrix = null;
				}
				else
				{
					CompositionTarget compositionTarget = activeSource.CompositionTarget;
					matrix = ((compositionTarget != null) ? new Matrix?(compositionTarget.TransformFromDevice) : null);
				}
			}
			Point point = devicePoint * (matrix ?? Matrix.Identity);
			return new Point(Math.Round(point.X), Math.Round(point.Y));
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005C6F0 File Offset: 0x0005BAF0
		internal override void UpdateStylusCapture(StylusDeviceBase stylusDevice, IInputElement oldStylusDeviceCapture, IInputElement newStylusDeviceCapture, int timestamp)
		{
			if (newStylusDeviceCapture != this._stylusCapture)
			{
				IInputElement stylusCapture = this._stylusCapture;
				this._stylusCapture = newStylusDeviceCapture;
				if (stylusCapture != null)
				{
					DependencyObject dependencyObject = stylusCapture as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						UIElement uielement = dependencyObject as UIElement;
						uielement.IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
						uielement.IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
						uielement.IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
					}
					else
					{
						UIElement3D uielement3D = dependencyObject as UIElement3D;
						uielement3D.IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
						uielement3D.IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
						uielement3D.IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
					}
				}
				if (this._stylusCapture != null)
				{
					DependencyObject dependencyObject = this._stylusCapture as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						UIElement uielement2 = dependencyObject as UIElement;
						uielement2.IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
						uielement2.IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
						uielement2.IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
					}
					else
					{
						UIElement3D uielement3D2 = dependencyObject as UIElement3D;
						uielement3D2.IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
						uielement3D2.IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
						uielement3D2.IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
					}
				}
				UIElement.StylusCaptureWithinProperty.OnOriginValueChanged(stylusCapture as DependencyObject, this._stylusCapture as DependencyObject, ref this._stylusCaptureWithinTreeState);
				if (stylusCapture != null)
				{
					DependencyObject dependencyObject = stylusCapture as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusCapturedPropertyKey, false);
				}
				if (this._stylusCapture != null)
				{
					DependencyObject dependencyObject = this._stylusCapture as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusCapturedPropertyKey, true);
				}
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005C894 File Offset: 0x0005BC94
		internal override void UpdateOverProperty(StylusDeviceBase stylusDevice, IInputElement newOver)
		{
			if (stylusDevice == this._currentStylusDevice && newOver != this._stylusOver)
			{
				IInputElement stylusOver = this._stylusOver;
				this._stylusOver = newOver;
				if (stylusOver != null)
				{
					DependencyObject dependencyObject = stylusOver as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						UIElement uielement = dependencyObject as UIElement;
						uielement.IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
						uielement.IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
						uielement.IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
					}
					else
					{
						UIElement3D uielement3D = dependencyObject as UIElement3D;
						uielement3D.IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
						uielement3D.IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
						uielement3D.IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
					}
				}
				if (this._stylusOver != null)
				{
					DependencyObject dependencyObject = this._stylusOver as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						UIElement uielement2 = dependencyObject as UIElement;
						uielement2.IsEnabledChanged += this._overIsEnabledChangedEventHandler;
						uielement2.IsVisibleChanged += this._overIsVisibleChangedEventHandler;
						uielement2.IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
					}
					else
					{
						UIElement3D uielement3D2 = dependencyObject as UIElement3D;
						uielement3D2.IsEnabledChanged += this._overIsEnabledChangedEventHandler;
						uielement3D2.IsVisibleChanged += this._overIsVisibleChangedEventHandler;
						uielement3D2.IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
					}
				}
				UIElement.StylusOverProperty.OnOriginValueChanged(stylusOver as DependencyObject, this._stylusOver as DependencyObject, ref this._stylusOverTreeState);
				if (stylusOver != null)
				{
					DependencyObject dependencyObject = stylusOver as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusDirectlyOverPropertyKey, false);
				}
				if (this._stylusOver != null)
				{
					DependencyObject dependencyObject = this._stylusOver as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusDirectlyOverPropertyKey, true);
				}
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0005CA44 File Offset: 0x0005BE44
		private void OnOverIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0005CA5C File Offset: 0x0005BE5C
		private void OnOverIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005CA74 File Offset: 0x0005BE74
		private void OnOverIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005CA8C File Offset: 0x0005BE8C
		private void OnCaptureIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005CAA4 File Offset: 0x0005BEA4
		private void OnCaptureIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005CABC File Offset: 0x0005BEBC
		private void OnCaptureIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005CAD4 File Offset: 0x0005BED4
		internal override void ReevaluateCapture(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this._stylusCaptureWithinTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this._stylusCaptureWithinTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateCaptureOperation == null)
			{
				this._reevaluateCaptureOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateCaptureDelegate, null);
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005CB24 File Offset: 0x0005BF24
		private object ReevaluateCaptureAsync(object arg)
		{
			this._reevaluateCaptureOperation = null;
			if (this._stylusCapture == null)
			{
				return null;
			}
			DependencyObject o = this._stylusCapture as DependencyObject;
			bool flag;
			if (InputElement.IsUIElement(o))
			{
				flag = !base.ValidateUIElementForCapture((UIElement)this._stylusCapture);
			}
			else if (InputElement.IsContentElement(o))
			{
				flag = !base.ValidateContentElementForCapture((ContentElement)this._stylusCapture);
			}
			else
			{
				flag = !base.ValidateUIElement3DForCapture((UIElement3D)this._stylusCapture);
			}
			if (!flag)
			{
				DependencyObject containingVisual = InputElement.GetContainingVisual(o);
				flag = !base.ValidateVisualForCapture(containingVisual, this.CurrentStylusDevice);
			}
			if (flag)
			{
				Stylus.Capture(null);
			}
			if (this._stylusCaptureWithinTreeState != null && !this._stylusCaptureWithinTreeState.IsEmpty)
			{
				UIElement.StylusCaptureWithinProperty.OnOriginValueChanged(this._stylusCapture as DependencyObject, this._stylusCapture as DependencyObject, ref this._stylusCaptureWithinTreeState);
			}
			return null;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005CC04 File Offset: 0x0005C004
		internal override void ReevaluateStylusOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this._stylusOverTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this._stylusOverTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateStylusOverOperation == null)
			{
				this._reevaluateStylusOverOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateStylusOverDelegate, null);
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005CC54 File Offset: 0x0005C054
		private object ReevaluateStylusOverAsync(object arg)
		{
			this._reevaluateStylusOverOperation = null;
			if (this._stylusOverTreeState != null && !this._stylusOverTreeState.IsEmpty)
			{
				UIElement.StylusOverProperty.OnOriginValueChanged(this._stylusOver as DependencyObject, this._stylusOver as DependencyObject, ref this._stylusOverTreeState);
			}
			return null;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005CCA4 File Offset: 0x0005C0A4
		protected override void OnTabletRemoved(uint wisptisIndex)
		{
			PointerLogic.IsEnabled = false;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005CCB8 File Offset: 0x0005C0B8
		[FriendAccessAllowed]
		[SecurityCritical]
		internal override void HandleMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam)
		{
			if (msg <= WindowMessage.WM_DISPLAYCHANGE)
			{
				if (msg == WindowMessage.WM_WININICHANGE)
				{
					base.ReadSystemConfig();
					this._pointerDevices.Refresh();
					return;
				}
				if (msg != WindowMessage.WM_DISPLAYCHANGE)
				{
					return;
				}
				this._pointerDevices.Refresh();
				return;
			}
			else
			{
				if (msg == WindowMessage.WM_DEVICECHANGE)
				{
					this._pointerDevices.Refresh();
					return;
				}
				if (msg == WindowMessage.WM_TABLET_ADDED)
				{
					this._pointerDevices.Refresh();
					return;
				}
				if (msg != WindowMessage.WM_TABLET_DELETED)
				{
					return;
				}
				this._pointerDevices.Refresh();
				return;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005CD30 File Offset: 0x0005C130
		private bool IsTouchPromotionEvent(StylusEventArgs stylusEventArgs)
		{
			if (stylusEventArgs != null)
			{
				RoutedEvent routedEvent = stylusEventArgs.RoutedEvent;
				if (stylusEventArgs != null)
				{
					StylusDevice stylusDevice = stylusEventArgs.StylusDevice;
					TabletDeviceType? tabletDeviceType;
					if (stylusDevice == null)
					{
						tabletDeviceType = null;
					}
					else
					{
						TabletDevice tabletDevice = stylusDevice.TabletDevice;
						tabletDeviceType = ((tabletDevice != null) ? new TabletDeviceType?(tabletDevice.Type) : null);
					}
					TabletDeviceType? tabletDeviceType2 = tabletDeviceType;
					TabletDeviceType tabletDeviceType3 = TabletDeviceType.Touch;
					if (tabletDeviceType2.GetValueOrDefault() == tabletDeviceType3 & tabletDeviceType2 != null)
					{
						return routedEvent == Stylus.StylusMoveEvent || routedEvent == Stylus.StylusDownEvent || routedEvent == Stylus.StylusUpEvent;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0005CDB4 File Offset: 0x0005C1B4
		[SecurityCritical]
		private void PromoteRawToPreview(RawStylusInputReport report, ProcessInputEventArgs e)
		{
			RoutedEvent previewEventFromRawStylusActions = StylusLogic.GetPreviewEventFromRawStylusActions(report.Actions);
			if (previewEventFromRawStylusActions != null && report.StylusDevice != null)
			{
				StylusEventArgs stylusEventArgs;
				if (previewEventFromRawStylusActions != Stylus.PreviewStylusSystemGestureEvent)
				{
					if (previewEventFromRawStylusActions == Stylus.PreviewStylusDownEvent)
					{
						stylusEventArgs = new StylusDownEventArgs(report.StylusDevice, report.Timestamp);
					}
					else
					{
						stylusEventArgs = new StylusEventArgs(report.StylusDevice, report.Timestamp);
					}
				}
				else
				{
					RawStylusSystemGestureInputReport rawStylusSystemGestureInputReport = (RawStylusSystemGestureInputReport)report;
					stylusEventArgs = new StylusSystemGestureEventArgs(report.StylusDevice, report.Timestamp, rawStylusSystemGestureInputReport.SystemGesture, rawStylusSystemGestureInputReport.GestureX, rawStylusSystemGestureInputReport.GestureY, rawStylusSystemGestureInputReport.ButtonState);
				}
				stylusEventArgs.InputReport = report;
				stylusEventArgs.RoutedEvent = previewEventFromRawStylusActions;
				e.PushInput(stylusEventArgs, e.StagingItem);
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005CE64 File Offset: 0x0005C264
		[SecurityCritical]
		private void PromotePreviewToMain(ProcessInputEventArgs e)
		{
			if (!e.StagingItem.Input.Handled)
			{
				RoutedEvent mainEventFromPreviewEvent = StylusLogic.GetMainEventFromPreviewEvent(e.StagingItem.Input.RoutedEvent);
				if (mainEventFromPreviewEvent != null)
				{
					StylusEventArgs stylusEventArgs = (StylusEventArgs)e.StagingItem.Input;
					StylusDevice stylusDevice = stylusEventArgs.InputReport.StylusDevice;
					StylusEventArgs stylusEventArgs2;
					if (mainEventFromPreviewEvent == Stylus.StylusDownEvent || mainEventFromPreviewEvent == Stylus.PreviewStylusDownEvent)
					{
						StylusDownEventArgs stylusDownEventArgs = (StylusDownEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusDownEventArgs(stylusDevice, stylusEventArgs.Timestamp);
					}
					else if (mainEventFromPreviewEvent == Stylus.StylusButtonDownEvent || mainEventFromPreviewEvent == Stylus.StylusButtonUpEvent)
					{
						StylusButtonEventArgs stylusButtonEventArgs = (StylusButtonEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusButtonEventArgs(stylusDevice, stylusEventArgs.Timestamp, stylusButtonEventArgs.StylusButton);
					}
					else if (mainEventFromPreviewEvent != Stylus.StylusSystemGestureEvent)
					{
						stylusEventArgs2 = new StylusEventArgs(stylusDevice, stylusEventArgs.Timestamp);
					}
					else
					{
						StylusSystemGestureEventArgs stylusSystemGestureEventArgs = (StylusSystemGestureEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusSystemGestureEventArgs(stylusDevice, stylusSystemGestureEventArgs.Timestamp, stylusSystemGestureEventArgs.SystemGesture, stylusSystemGestureEventArgs.GestureX, stylusSystemGestureEventArgs.GestureY, stylusSystemGestureEventArgs.ButtonState);
					}
					stylusEventArgs2.InputReport = stylusEventArgs.InputReport;
					stylusEventArgs2.RoutedEvent = mainEventFromPreviewEvent;
					e.PushInput(stylusEventArgs2, e.StagingItem);
					return;
				}
			}
			else
			{
				StylusEventArgs stylusEventArgs3 = e.StagingItem.Input as StylusEventArgs;
				if (stylusEventArgs3 != null && stylusEventArgs3.RoutedEvent == Stylus.PreviewStylusUpEvent && stylusEventArgs3.StylusDeviceImpl.As<PointerStylusDevice>().TouchDevice.IsActive)
				{
					stylusEventArgs3.StylusDeviceImpl.As<PointerStylusDevice>().TouchDevice.OnDeactivate();
				}
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005CFD4 File Offset: 0x0005C3D4
		[SecurityCritical]
		private void PromoteMainToOther(ProcessInputEventArgs e)
		{
			StylusEventArgs stylusEventArgs = e.StagingItem.Input as StylusEventArgs;
			if (stylusEventArgs == null)
			{
				return;
			}
			PointerStylusDevice pointerStylusDevice = stylusEventArgs.StylusDeviceImpl.As<PointerStylusDevice>();
			PointerTouchDevice touchDevice = pointerStylusDevice.TouchDevice;
			if (this.IsTouchPromotionEvent(stylusEventArgs))
			{
				if (e.StagingItem.Input.Handled)
				{
					if (stylusEventArgs.RoutedEvent == Stylus.StylusUpEvent && touchDevice.IsActive)
					{
						touchDevice.OnDeactivate();
						return;
					}
				}
				else
				{
					this.PromoteMainToTouch(e, stylusEventArgs);
				}
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005D048 File Offset: 0x0005C448
		[SecurityCritical]
		private void PromoteMainToTouch(ProcessInputEventArgs e, StylusEventArgs stylusEventArgs)
		{
			PointerStylusDevice stylusDevice = stylusEventArgs.StylusDeviceImpl.As<PointerStylusDevice>();
			if (stylusEventArgs.RoutedEvent == Stylus.StylusMoveEvent)
			{
				this.PromoteMainMoveToTouch(stylusDevice, e.StagingItem);
				return;
			}
			if (stylusEventArgs.RoutedEvent == Stylus.StylusDownEvent)
			{
				this.PromoteMainDownToTouch(stylusDevice, e.StagingItem);
				return;
			}
			if (stylusEventArgs.RoutedEvent == Stylus.StylusUpEvent)
			{
				this.PromoteMainUpToTouch(stylusDevice, e.StagingItem);
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005D0B4 File Offset: 0x0005C4B4
		[SecurityCritical]
		private void PromoteMainDownToTouch(PointerStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			PointerTouchDevice touchDevice = stylusDevice.TouchDevice;
			if (touchDevice.IsActive)
			{
				touchDevice.OnDeactivate();
			}
			touchDevice.OnActivate();
			touchDevice.OnDown();
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005D0E4 File Offset: 0x0005C4E4
		[SecurityCritical]
		private void PromoteMainMoveToTouch(PointerStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			PointerTouchDevice touchDevice = stylusDevice.TouchDevice;
			if (touchDevice.IsActive)
			{
				touchDevice.OnMove();
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005D108 File Offset: 0x0005C508
		[SecurityCritical]
		private void PromoteMainUpToTouch(PointerStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			PointerTouchDevice touchDevice = stylusDevice.TouchDevice;
			if (touchDevice.IsActive)
			{
				touchDevice.OnUp();
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0005D12C File Offset: 0x0005C52C
		internal void SelectStylusDevice(PointerStylusDevice pointerStylusDevice, IInputElement newOver, bool updateOver)
		{
			bool flag = this._currentStylusDevice != pointerStylusDevice;
			PointerStylusDevice currentStylusDevice = this._currentStylusDevice;
			if (updateOver && pointerStylusDevice == null && flag && newOver == null)
			{
				this._currentStylusDevice.ChangeStylusOver(newOver);
			}
			this._currentStylusDevice = pointerStylusDevice;
			if (updateOver && pointerStylusDevice != null)
			{
				pointerStylusDevice.ChangeStylusOver(newOver);
				if (flag && currentStylusDevice != null && !currentStylusDevice.InRange)
				{
					currentStylusDevice.ChangeStylusOver(null);
				}
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005D194 File Offset: 0x0005C594
		[SecurityCritical]
		internal PointerStylusPlugInManager GetManagerForSource(PresentationSource source)
		{
			if (source == null)
			{
				return null;
			}
			PointerStylusPlugInManager result = null;
			this.PlugInManagers.TryGetValue(source, out result);
			return result;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005D1B8 File Offset: 0x0005C5B8
		[SecurityCritical]
		private void UpdateTapCount(NotifyInputEventArgs args)
		{
			if (args.StagingItem.Input.RoutedEvent != Stylus.PreviewStylusDownEvent)
			{
				if (args.StagingItem.Input.RoutedEvent == Stylus.PreviewStylusSystemGestureEvent)
				{
					StylusSystemGestureEventArgs stylusSystemGestureEventArgs = args.StagingItem.Input as StylusSystemGestureEventArgs;
					PointerStylusDevice pointerStylusDevice = stylusSystemGestureEventArgs.StylusDevice.As<PointerStylusDevice>();
					if (stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Drag || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.RightDrag)
					{
						pointerStylusDevice.TapCount = 1;
					}
				}
				return;
			}
			StylusEventArgs stylusEventArgs = args.StagingItem.Input as StylusDownEventArgs;
			PointerStylusDevice pointerStylusDevice2 = stylusEventArgs.StylusDevice.As<PointerStylusDevice>();
			Point position = pointerStylusDevice2.GetPosition(null);
			StylusButton stylusButtonByGuid = pointerStylusDevice2.StylusButtons.GetStylusButtonByGuid(StylusPointPropertyIds.BarrelButton);
			bool flag = ((stylusButtonByGuid != null) ? stylusButtonByGuid.StylusButtonState : StylusButtonState.Up) == StylusButtonState.Down;
			int num = Math.Abs(stylusEventArgs.Timestamp - this._lastTapTimeTicks);
			Point lastTapPoint = this.DeviceUnitsFromMeasureUnits(position);
			Size doubleTapSize = pointerStylusDevice2.PointerTabletDevice.DoubleTapSize;
			bool flag2 = Math.Abs(lastTapPoint.X - this._lastTapPoint.X) < doubleTapSize.Width && Math.Abs(lastTapPoint.Y - this._lastTapPoint.Y) < doubleTapSize.Height;
			if (num < pointerStylusDevice2.PointerTabletDevice.DoubleTapDeltaTime && flag2 && flag == this._lastTapBarrelDown)
			{
				PointerStylusDevice pointerStylusDevice3 = pointerStylusDevice2;
				int tapCount = pointerStylusDevice3.TapCount;
				pointerStylusDevice3.TapCount = tapCount + 1;
				return;
			}
			pointerStylusDevice2.TapCount = 1;
			this._lastTapPoint = lastTapPoint;
			this._lastTapTimeTicks = stylusEventArgs.Timestamp;
			this._lastTapBarrelDown = flag;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005D33C File Offset: 0x0005C73C
		[SecurityCritical]
		private void GenerateGesture(RawStylusInputReport rawStylusInputReport, SystemGesture gesture)
		{
			PointerStylusDevice stylusDevice = rawStylusInputReport.StylusDevice.As<PointerStylusDevice>();
			RawStylusSystemGestureInputReport report = new RawStylusSystemGestureInputReport(InputMode.Foreground, rawStylusInputReport.Timestamp, rawStylusInputReport.InputSource, () => stylusDevice.PointerTabletDevice.StylusPointDescription, rawStylusInputReport.TabletDeviceId, rawStylusInputReport.StylusDeviceId, gesture, 0, 0, 0)
			{
				StylusDevice = stylusDevice.StylusDevice
			};
			InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(stylusDevice.StylusDevice, report);
			inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
			this._inputManager.Value.ProcessInput(inputReportEventArgs);
		}

		// Token: 0x04000CD8 RID: 3288
		private bool _lastTapBarrelDown;

		// Token: 0x04000CD9 RID: 3289
		private Point _lastTapPoint = new Point(0.0, 0.0);

		// Token: 0x04000CDA RID: 3290
		private int _lastTapTimeTicks;

		// Token: 0x04000CDB RID: 3291
		private IInputElement _stylusCapture;

		// Token: 0x04000CDC RID: 3292
		private IInputElement _stylusOver;

		// Token: 0x04000CDD RID: 3293
		private DeferredElementTreeState _stylusOverTreeState = new DeferredElementTreeState();

		// Token: 0x04000CDE RID: 3294
		private DeferredElementTreeState _stylusCaptureWithinTreeState = new DeferredElementTreeState();

		// Token: 0x04000CDF RID: 3295
		private DependencyPropertyChangedEventHandler _overIsEnabledChangedEventHandler;

		// Token: 0x04000CE0 RID: 3296
		private DependencyPropertyChangedEventHandler _overIsVisibleChangedEventHandler;

		// Token: 0x04000CE1 RID: 3297
		private DependencyPropertyChangedEventHandler _overIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000CE2 RID: 3298
		private DispatcherOperationCallback _reevaluateStylusOverDelegate;

		// Token: 0x04000CE3 RID: 3299
		private DispatcherOperation _reevaluateStylusOverOperation;

		// Token: 0x04000CE4 RID: 3300
		private DependencyPropertyChangedEventHandler _captureIsEnabledChangedEventHandler;

		// Token: 0x04000CE5 RID: 3301
		private DependencyPropertyChangedEventHandler _captureIsVisibleChangedEventHandler;

		// Token: 0x04000CE6 RID: 3302
		private DependencyPropertyChangedEventHandler _captureIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000CE7 RID: 3303
		private DispatcherOperationCallback _reevaluateCaptureDelegate;

		// Token: 0x04000CE8 RID: 3304
		private DispatcherOperation _reevaluateCaptureOperation;

		// Token: 0x04000CE9 RID: 3305
		private bool _initialDeviceRefreshDone;

		// Token: 0x04000CEA RID: 3306
		private PointerTabletDeviceCollection _pointerDevices = new PointerTabletDeviceCollection();

		// Token: 0x04000CEB RID: 3307
		private PointerStylusDevice _currentStylusDevice;

		// Token: 0x04000CEC RID: 3308
		private SecurityCriticalData<InputManager> _inputManager;

		// Token: 0x04000CED RID: 3309
		private bool _inDragDrop;
	}
}
