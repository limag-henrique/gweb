using System;
using System.ComponentModel;
using System.Globalization;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Interop;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input.StylusWisp
{
	// Token: 0x020002E4 RID: 740
	internal class WispStylusDevice : StylusDeviceBase
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x000583BC File Offset: 0x000577BC
		[SecuritySafeCritical]
		internal WispStylusDevice(WispTabletDevice tabletDevice, string sName, int id, bool fInverted, StylusButtonCollection stylusButtonCollection)
		{
			this._tabletDevice = tabletDevice;
			this._sName = sName;
			this._id = id;
			this._fInverted = fInverted;
			this._fInRange = false;
			this._stylusButtonCollection = stylusButtonCollection;
			if (this._stylusButtonCollection != null)
			{
				foreach (StylusButton stylusButton in this._stylusButtonCollection)
				{
					stylusButton.SetOwner(this);
				}
			}
			this._stylusLogic = StylusLogic.GetCurrentStylusLogicAs<WispLogic>();
			this._stylusLogic.RegisterStylusDeviceCore(base.StylusDevice);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0005851C File Offset: 0x0005791C
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this._stylusLogic.UnregisterStylusDeviceCore(base.StylusDevice);
					WispStylusTouchDevice touchDevice = this._touchDevice;
					if (touchDevice != null && touchDevice.IsActive)
					{
						this._touchDevice.OnDeactivate();
					}
					this._inputSource = null;
					this._stylusCapture = null;
					this._stylusOver = null;
					this._nonVerifiedTarget = null;
					this._verifiedTarget = null;
					this._rtiCaptureChanged = null;
					this._stylusCapturePlugInCollection = null;
					this._fBlockMouseMoveChanges = false;
					this._tabletDevice = null;
					this._stylusLogic = null;
					this._fInRange = false;
					this._touchDevice = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000585C8 File Offset: 0x000579C8
		internal override IInputElement Target
		{
			get
			{
				base.VerifyAccess();
				return this._stylusOver;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000585E4 File Offset: 0x000579E4
		internal override bool IsValid
		{
			get
			{
				return this._tabletDevice != null;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000585FC File Offset: 0x000579FC
		internal override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				if (this._inputSource != null)
				{
					return this._inputSource.Value;
				}
				return null;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x00058620 File Offset: 0x00057A20
		internal override PresentationSource CriticalActiveSource
		{
			[SecurityCritical]
			get
			{
				if (this._inputSource != null)
				{
					return this._inputSource.Value;
				}
				return null;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00058644 File Offset: 0x00057A44
		internal PenContext ActivePenContext
		{
			[SecurityCritical]
			get
			{
				if (this._activePenContext != null)
				{
					return this._activePenContext.Value;
				}
				return null;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00058668 File Offset: 0x00057A68
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0005867C File Offset: 0x00057A7C
		internal StylusPlugInCollection CurrentNonVerifiedTarget
		{
			get
			{
				return this._nonVerifiedTarget;
			}
			set
			{
				this._nonVerifiedTarget = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00058690 File Offset: 0x00057A90
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x000586A4 File Offset: 0x00057AA4
		internal override StylusPlugInCollection CurrentVerifiedTarget
		{
			get
			{
				return this._verifiedTarget;
			}
			set
			{
				this._verifiedTarget = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000586B8 File Offset: 0x00057AB8
		internal override IInputElement DirectlyOver
		{
			get
			{
				base.VerifyAccess();
				return this._stylusOver;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x000586D4 File Offset: 0x00057AD4
		internal override IInputElement Captured
		{
			get
			{
				base.VerifyAccess();
				return this._stylusCapture;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000586F0 File Offset: 0x00057AF0
		internal override CaptureMode CapturedMode
		{
			get
			{
				return this._captureMode;
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00058704 File Offset: 0x00057B04
		internal override bool Capture(IInputElement element, CaptureMode captureMode)
		{
			int tickCount = Environment.TickCount;
			base.VerifyAccess();
			if (captureMode != CaptureMode.None && captureMode != CaptureMode.Element && captureMode != CaptureMode.SubTree)
			{
				throw new InvalidEnumArgumentException("captureMode", (int)captureMode, typeof(CaptureMode));
			}
			if (element == null)
			{
				captureMode = CaptureMode.None;
			}
			if (captureMode == CaptureMode.None)
			{
				element = null;
			}
			DependencyObject dependencyObject = element as DependencyObject;
			if (dependencyObject != null && !InputElement.IsValid(element))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					dependencyObject.GetType()
				}));
			}
			if (dependencyObject != null)
			{
				dependencyObject.VerifyAccess();
			}
			bool flag = false;
			UIElement uielement = element as UIElement;
			if (uielement != null)
			{
				if (uielement.IsVisible || uielement.IsEnabled)
				{
					flag = true;
				}
			}
			else
			{
				ContentElement contentElement = element as ContentElement;
				if (contentElement != null)
				{
					if (contentElement.IsEnabled)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.ChangeStylusCapture(element, captureMode, tickCount);
			}
			return flag;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000587CC File Offset: 0x00057BCC
		internal override bool Capture(IInputElement element)
		{
			return this.Capture(element, CaptureMode.Element);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000587E4 File Offset: 0x00057BE4
		internal override StylusPlugInCollection GetCapturedPlugInCollection(ref bool elementHasCapture)
		{
			object rtiCaptureChanged = this._rtiCaptureChanged;
			StylusPlugInCollection stylusCapturePlugInCollection;
			lock (rtiCaptureChanged)
			{
				elementHasCapture = (this._stylusCapture != null);
				stylusCapturePlugInCollection = this._stylusCapturePlugInCollection;
			}
			return stylusCapturePlugInCollection;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00058840 File Offset: 0x00057C40
		[SecurityCritical]
		internal override void Synchronize()
		{
			if (this.InRange && this._inputSource != null && this._inputSource.Value != null && this._inputSource.Value.CompositionTarget != null && !this._inputSource.Value.CompositionTarget.IsDisposed)
			{
				Point point = PointUtil.ScreenToClient(this._lastScreenLocation, this._inputSource.Value);
				IInputElement inputElement = StylusDevice.GlobalHitTest(this._inputSource.Value, point);
				bool flag = false;
				if (this._stylusOver == inputElement)
				{
					Point position = this.GetPosition(inputElement);
					flag = (!DoubleUtil.AreClose(position.X, this._rawElementRelativePosition.X) || !DoubleUtil.AreClose(position.Y, this._rawElementRelativePosition.Y));
				}
				if (flag || this._stylusOver != inputElement)
				{
					int tickCount = Environment.TickCount;
					PenContext stylusPenContextForHwnd = this._stylusLogic.GetStylusPenContextForHwnd(this._inputSource.Value, this.TabletDevice.Id);
					if (this._eventStylusPoints != null && this._eventStylusPoints.Count > 0 && StylusPointDescription.AreCompatible(stylusPenContextForHwnd.StylusPointDescription, this._eventStylusPoints.Description))
					{
						int[] packetData = this._eventStylusPoints[this._eventStylusPoints.Count - 1].GetPacketData();
						Matrix tabletToScreen = this._tabletDevice.TabletToScreen;
						tabletToScreen.Invert();
						Point point2 = point * tabletToScreen;
						packetData[0] = (int)point2.X;
						packetData[1] = (int)point2.Y;
						RawStylusInputReport rawStylusInputReport = new RawStylusInputReport(InputMode.Foreground, tickCount, this._inputSource.Value, stylusPenContextForHwnd, this.InAir ? RawStylusActions.InAirMove : RawStylusActions.Move, this.TabletDevice.Id, this.Id, packetData);
						rawStylusInputReport.Synchronized = true;
						InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(base.StylusDevice, rawStylusInputReport);
						inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
						this._stylusLogic.InputManagerProcessInputEventArgs(inputReportEventArgs);
					}
				}
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00058A44 File Offset: 0x00057E44
		[SecuritySafeCritical]
		internal void ChangeStylusCapture(IInputElement stylusCapture, CaptureMode captureMode, int timestamp)
		{
			if (stylusCapture != this._stylusCapture)
			{
				IInputElement stylusCapture2 = this._stylusCapture;
				using (base.Dispatcher.DisableProcessing())
				{
					object rtiCaptureChanged = this._rtiCaptureChanged;
					lock (rtiCaptureChanged)
					{
						this._stylusCapture = stylusCapture;
						this._captureMode = captureMode;
						this._stylusCapturePlugInCollection = null;
						if (stylusCapture != null)
						{
							UIElement uielement = InputElement.GetContainingUIElement(stylusCapture as DependencyObject) as UIElement;
							if (uielement != null)
							{
								PresentationSource presentationSource = PresentationSource.CriticalFromVisual(uielement);
								if (presentationSource != null)
								{
									PenContexts penContextsFromHwnd = this._stylusLogic.GetPenContextsFromHwnd(presentationSource);
									this._stylusCapturePlugInCollection = penContextsFromHwnd.FindPlugInCollection(uielement);
								}
							}
						}
					}
				}
				this._stylusLogic.UpdateStylusCapture(this, stylusCapture2, this._stylusCapture, timestamp);
				if (stylusCapture2 != null)
				{
					StylusEventArgs stylusEventArgs = new StylusEventArgs(base.StylusDevice, timestamp);
					stylusEventArgs.RoutedEvent = Stylus.LostStylusCaptureEvent;
					stylusEventArgs.Source = stylusCapture2;
					this._stylusLogic.InputManagerProcessInputEventArgs(stylusEventArgs);
				}
				if (this._stylusCapture != null)
				{
					StylusEventArgs stylusEventArgs2 = new StylusEventArgs(base.StylusDevice, timestamp);
					stylusEventArgs2.RoutedEvent = Stylus.GotStylusCaptureEvent;
					stylusEventArgs2.Source = this._stylusCapture;
					this._stylusLogic.InputManagerProcessInputEventArgs(stylusEventArgs2);
				}
				if (this._stylusLogic.CurrentStylusDevice == this || this.InRange)
				{
					if (this._stylusCapture != null)
					{
						IInputElement stylusOver = this._stylusCapture;
						if (this.CapturedMode == CaptureMode.SubTree && this._inputSource != null && this._inputSource.Value != null)
						{
							Point position = this._stylusLogic.DeviceUnitsFromMeasureUnits(this.GetPosition(null));
							stylusOver = this.FindTarget(this._inputSource.Value, position);
						}
						this.ChangeStylusOver(stylusOver);
					}
					else if (this._inputSource != null && this._inputSource.Value != null)
					{
						Point point = this.GetPosition(null);
						point = this._stylusLogic.DeviceUnitsFromMeasureUnits(point);
						IInputElement stylusOver2 = StylusDevice.GlobalHitTest(this._inputSource.Value, point);
						this.ChangeStylusOver(stylusOver2);
					}
				}
				if (Mouse.Captured != this._stylusCapture || Mouse.CapturedMode != this._captureMode)
				{
					Mouse.Capture(this._stylusCapture, this._captureMode);
				}
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00058C98 File Offset: 0x00058098
		[SecuritySafeCritical]
		internal void ChangeStylusOver(IInputElement stylusOver)
		{
			if (this._stylusOver != stylusOver)
			{
				this._stylusOver = stylusOver;
				this._rawElementRelativePosition = this.GetPosition(this._stylusOver);
			}
			else if (this.InRange)
			{
				this._rawElementRelativePosition = this.GetPosition(this._stylusOver);
			}
			this._stylusLogic.UpdateOverProperty(this, this._stylusOver);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00058CF8 File Offset: 0x000580F8
		[SecurityCritical]
		internal IInputElement FindTarget(PresentationSource inputSource, Point position)
		{
			IInputElement inputElement = null;
			switch (this._captureMode)
			{
			case CaptureMode.None:
				inputElement = StylusDevice.GlobalHitTest(inputSource, position);
				if (!InputElement.IsValid(inputElement))
				{
					inputElement = InputElement.GetContainingInputElement(inputElement as DependencyObject);
				}
				break;
			case CaptureMode.Element:
				inputElement = this._stylusCapture;
				break;
			case CaptureMode.SubTree:
			{
				IInputElement containingInputElement = InputElement.GetContainingInputElement(this._stylusCapture as DependencyObject);
				if (containingInputElement != null && inputSource != null)
				{
					inputElement = StylusDevice.GlobalHitTest(inputSource, position);
				}
				if (inputElement != null && !InputElement.IsValid(inputElement))
				{
					inputElement = InputElement.GetContainingInputElement(inputElement as DependencyObject);
				}
				if (inputElement != null)
				{
					IInputElement inputElement2 = inputElement;
					while (inputElement2 != null && inputElement2 != containingInputElement)
					{
						UIElement uielement = inputElement2 as UIElement;
						if (uielement != null)
						{
							inputElement2 = InputElement.GetContainingInputElement(uielement.GetUIParent(true));
						}
						else
						{
							ContentElement contentElement = inputElement2 as ContentElement;
							inputElement2 = InputElement.GetContainingInputElement(contentElement.GetUIParent(true));
						}
					}
					if (inputElement2 != containingInputElement)
					{
						inputElement = this._stylusCapture;
					}
				}
				else
				{
					inputElement = this._stylusCapture;
				}
				break;
			}
			}
			return inputElement;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00058DE4 File Offset: 0x000581E4
		internal override TabletDevice TabletDevice
		{
			get
			{
				return this._tabletDevice.TabletDevice;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00058DFC File Offset: 0x000581FC
		internal override string Name
		{
			get
			{
				base.VerifyAccess();
				return this._sName;
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00058E18 File Offset: 0x00058218
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				base.ToString(),
				this.Name
			});
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00058E4C File Offset: 0x0005824C
		internal override int Id
		{
			get
			{
				base.VerifyAccess();
				return this._id;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00058E68 File Offset: 0x00058268
		internal override StylusPointCollection GetStylusPoints(IInputElement relativeTo)
		{
			base.VerifyAccess();
			if (this._eventStylusPoints == null)
			{
				return new StylusPointCollection(this._tabletDevice.StylusPointDescription);
			}
			return this._eventStylusPoints.Clone(StylusDevice.GetElementTransform(relativeTo), this._eventStylusPoints.Description);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00058EB0 File Offset: 0x000582B0
		internal override StylusPointCollection GetStylusPoints(IInputElement relativeTo, StylusPointDescription subsetToReformatTo)
		{
			if (subsetToReformatTo == null)
			{
				throw new ArgumentNullException("subsetToReformatTo");
			}
			if (this._eventStylusPoints == null)
			{
				return new StylusPointCollection(subsetToReformatTo);
			}
			return this._eventStylusPoints.Reformat(subsetToReformatTo, StylusDevice.GetElementTransform(relativeTo));
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00058EEC File Offset: 0x000582EC
		internal override StylusButtonCollection StylusButtons
		{
			get
			{
				base.VerifyAccess();
				return this._stylusButtonCollection;
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00058F08 File Offset: 0x00058308
		[SecuritySafeCritical]
		internal override Point GetPosition(IInputElement relativeTo)
		{
			base.VerifyAccess();
			if (relativeTo != null && !InputElement.IsValid(relativeTo))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					relativeTo.GetType()
				}));
			}
			PresentationSource presentationSource = null;
			if (relativeTo != null)
			{
				DependencyObject o = relativeTo as DependencyObject;
				DependencyObject containingVisual = InputElement.GetContainingVisual(o);
				if (containingVisual != null)
				{
					presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
				}
			}
			else if (this._inputSource != null)
			{
				presentationSource = this._inputSource.Value;
			}
			if (presentationSource == null || presentationSource.RootVisual == null)
			{
				return new Point(0.0, 0.0);
			}
			Point point = PointUtil.ScreenToClient(this._lastScreenLocation, presentationSource);
			Point pt = PointUtil.ClientToRoot(point, presentationSource);
			return InputElement.TranslatePoint(pt, presentationSource.RootVisual, (DependencyObject)relativeTo);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00058FC8 File Offset: 0x000583C8
		internal Point GetRawPosition(IInputElement relativeTo)
		{
			GeneralTransform elementTransform = StylusDevice.GetElementTransform(relativeTo);
			Point result;
			elementTransform.TryTransform((Point)this._rawPosition, out result);
			return result;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00058FF4 File Offset: 0x000583F4
		internal override StylusPoint RawStylusPoint
		{
			get
			{
				return this._rawPosition;
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00059008 File Offset: 0x00058408
		[SecuritySafeCritical]
		internal override MouseButtonState GetMouseButtonState(MouseButton mouseButton, MouseDevice mouseDevice)
		{
			if (mouseButton == MouseButton.Left)
			{
				return this._stylusLogic.GetMouseLeftOrRightButtonState(true);
			}
			if (mouseButton == MouseButton.Right)
			{
				return this._stylusLogic.GetMouseLeftOrRightButtonState(false);
			}
			return mouseDevice.GetButtonStateFromSystem(mouseButton);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00059040 File Offset: 0x00058440
		internal override Point GetMouseScreenPosition(MouseDevice mouseDevice)
		{
			if (mouseDevice == null)
			{
				return this._lastMouseScreenLocation;
			}
			return mouseDevice.GetScreenPositionFromSystem();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00059060 File Offset: 0x00058460
		[SecurityCritical]
		private GeneralTransform GetTabletToElementTransform(IInputElement relativeTo)
		{
			return new GeneralTransformGroup
			{
				Children = 
				{
					new MatrixTransform(this._stylusLogic.GetTabletToViewTransform(this._tabletDevice.TabletDevice)),
					StylusDevice.GetElementTransform(relativeTo)
				}
			};
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x000590AC File Offset: 0x000584AC
		internal override bool InAir
		{
			get
			{
				base.VerifyAccess();
				return this._fInAir;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x000590C8 File Offset: 0x000584C8
		internal override bool Inverted
		{
			get
			{
				base.VerifyAccess();
				return this._fInverted;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000590E4 File Offset: 0x000584E4
		internal override bool InRange
		{
			get
			{
				base.VerifyAccess();
				return this._fInRange;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00059100 File Offset: 0x00058500
		[SecurityCritical]
		internal override void UpdateEventStylusPoints(RawStylusInputReport report, bool resetIfNoOverride)
		{
			if (report.RawStylusInput != null && report.RawStylusInput.StylusPointsModified)
			{
				GeneralTransform inverse = report.RawStylusInput.Target.ViewToElement.Inverse;
				this._eventStylusPoints = report.RawStylusInput.GetStylusPoints(inverse);
				return;
			}
			if (resetIfNoOverride)
			{
				this._eventStylusPoints = new StylusPointCollection(report.StylusPointDescription, report.GetRawPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00059174 File Offset: 0x00058574
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x00059188 File Offset: 0x00058588
		internal override int TapCount
		{
			get
			{
				return this._tapCount;
			}
			set
			{
				this._tapCount = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005919C File Offset: 0x0005859C
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x000591B0 File Offset: 0x000585B0
		internal int LastTapTime
		{
			get
			{
				return this._lastTapTime;
			}
			set
			{
				this._lastTapTime = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000591C4 File Offset: 0x000585C4
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x000591D8 File Offset: 0x000585D8
		internal Point LastTapPoint
		{
			get
			{
				return this._lastTapXY;
			}
			set
			{
				this._lastTapXY = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x000591EC File Offset: 0x000585EC
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x00059200 File Offset: 0x00058600
		internal bool LastTapBarrelDown
		{
			get
			{
				return this._lastTapBarrelDown;
			}
			set
			{
				this._lastTapBarrelDown = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00059214 File Offset: 0x00058614
		internal override int DoubleTapDeltaX
		{
			get
			{
				return (int)this._tabletDevice.DoubleTapSize.Width;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00059238 File Offset: 0x00058638
		internal override int DoubleTapDeltaY
		{
			get
			{
				return (int)this._tabletDevice.DoubleTapSize.Height;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0005925C File Offset: 0x0005865C
		internal override int DoubleTapDeltaTime
		{
			[SecuritySafeCritical]
			get
			{
				return this._stylusLogic.DoubleTapDeltaTime;
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00059274 File Offset: 0x00058674
		[SecurityCritical]
		internal void UpdateState(RawStylusInputReport report)
		{
			this._eventStylusPoints = new StylusPointCollection(report.StylusPointDescription, report.GetRawPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
			PresentationSource presentationSource = this.DetermineValidSource(report.InputSource, this._eventStylusPoints, report.PenContext.Contexts);
			if (presentationSource != null && presentationSource != report.InputSource)
			{
				Point measurePoint = PointUtil.ClientToScreen(new Point(0.0, 0.0), presentationSource);
				measurePoint = this._stylusLogic.MeasureUnitsFromDeviceUnits(measurePoint);
				Point point = this._stylusLogic.MeasureUnitsFromDeviceUnits(report.PenContext.Contexts.DestroyedLocation);
				MatrixTransform transform = new MatrixTransform(new Matrix(1.0, 0.0, 0.0, 1.0, point.X - measurePoint.X, point.Y - measurePoint.Y));
				this._eventStylusPoints = this._eventStylusPoints.Reformat(report.StylusPointDescription, transform);
			}
			this._rawPosition = this._eventStylusPoints[this._eventStylusPoints.Count - 1];
			this._inputSource = new SecurityCriticalDataClass<PresentationSource>(presentationSource);
			if (presentationSource != null)
			{
				Point pointClient = this._stylusLogic.DeviceUnitsFromMeasureUnits((Point)this._rawPosition);
				this._lastScreenLocation = PointUtil.ClientToScreen(pointClient, presentationSource);
			}
			if (!this._fBlockMouseMoveChanges)
			{
				this._lastMouseScreenLocation = this._lastScreenLocation;
			}
			if ((report.Actions & RawStylusActions.Down) != RawStylusActions.None || (report.Actions & RawStylusActions.Move) != RawStylusActions.None)
			{
				this._fInAir = false;
				if ((report.Actions & RawStylusActions.Down) != RawStylusActions.None)
				{
					this._needToSendMouseDown = true;
					this._fGestureWasFired = false;
					this._fDetectedDrag = false;
					this._seenHoldEnterGesture = false;
					this._tabletDevice.UpdateSizeDeltas(report.StylusPointDescription, this._stylusLogic);
				}
				else if (presentationSource != null && this._fBlockMouseMoveChanges && this._seenDoubleTapGesture && !this._fGestureWasFired && !this._fDetectedDrag)
				{
					Size cancelSize = this._tabletDevice.CancelSize;
					Point point2 = (Point)this._eventStylusPoints[0];
					point2 = this._stylusLogic.DeviceUnitsFromMeasureUnits(point2);
					point2 = PointUtil.ClientToScreen(point2, presentationSource);
					if (Math.Abs(this._lastMouseScreenLocation.X - point2.X) > cancelSize.Width || Math.Abs(this._lastMouseScreenLocation.Y - point2.Y) > cancelSize.Height)
					{
						this._fDetectedDrag = true;
					}
				}
			}
			this.UpdateEventStylusPoints(report, false);
			if ((report.Actions & RawStylusActions.Up) != RawStylusActions.None || (report.Actions & RawStylusActions.InAirMove) != RawStylusActions.None)
			{
				this._fInAir = true;
				if ((report.Actions & RawStylusActions.Up) != RawStylusActions.None)
				{
					this._sawMouseButton1Down = false;
				}
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00059530 File Offset: 0x00058930
		[SecurityCritical]
		private PresentationSource DetermineValidSource(PresentationSource inputSource, StylusPointCollection stylusPoints, PenContexts penContextsOfPoints)
		{
			HwndSource hwndSource = (HwndSource)inputSource;
			if (inputSource.CompositionTarget == null || inputSource.CompositionTarget.IsDisposed || hwndSource == null || hwndSource.IsHandleNull)
			{
				PresentationSource presentationSource = null;
				if (this._stylusCapture != null)
				{
					DependencyObject containingVisual = InputElement.GetContainingVisual(this._stylusCapture as DependencyObject);
					PresentationSource presentationSource2 = PresentationSource.CriticalFromVisual(containingVisual);
					if (presentationSource2 != null && presentationSource2.CompositionTarget != null && !presentationSource2.CompositionTarget.IsDisposed)
					{
						presentationSource = presentationSource2;
					}
				}
				if (presentationSource == null && stylusPoints != null)
				{
					Point point;
					if (penContextsOfPoints != null)
					{
						point = this._stylusLogic.DeviceUnitsFromMeasureUnits((Point)stylusPoints[0]);
						point.Offset(penContextsOfPoints.DestroyedLocation.X, penContextsOfPoints.DestroyedLocation.Y);
					}
					else
					{
						point = this._lastMouseScreenLocation;
					}
					IntPtr intPtr = UnsafeNativeMethods.WindowFromPoint((int)point.X, (int)point.Y);
					if (intPtr != IntPtr.Zero)
					{
						HwndSource hwndSource2 = HwndSource.CriticalFromHwnd(intPtr);
						if (hwndSource2 != null && hwndSource2.Dispatcher == base.Dispatcher)
						{
							presentationSource = hwndSource2;
						}
					}
				}
				return presentationSource;
			}
			return inputSource;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00059640 File Offset: 0x00058A40
		[SecurityCritical]
		internal void UpdateInRange(bool inRange, PenContext penContext)
		{
			this._fInRange = inRange;
			if (inRange)
			{
				this._activePenContext = new SecurityCriticalDataClass<PenContext>(penContext);
				return;
			}
			this._activePenContext = null;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005966C File Offset: 0x00058A6C
		[SecurityCritical]
		internal void UpdateStateForSystemGesture(RawStylusSystemGestureInputReport report)
		{
			this.UpdateStateForSystemGesture(report.SystemGesture, report);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00059688 File Offset: 0x00058A88
		[SecurityCritical]
		private void UpdateStateForSystemGesture(SystemGesture gesture, RawStylusSystemGestureInputReport report)
		{
			switch (gesture)
			{
			case SystemGesture.Tap:
			case SystemGesture.Drag:
				this._fLeftButtonDownTrigger = true;
				this._fGestureWasFired = true;
				return;
			case (SystemGesture)17:
				break;
			case SystemGesture.RightTap:
			case SystemGesture.RightDrag:
				this._fLeftButtonDownTrigger = false;
				this._fGestureWasFired = true;
				return;
			case SystemGesture.HoldEnter:
				this._seenHoldEnterGesture = true;
				return;
			default:
				if (gesture != SystemGesture.Flick)
				{
					return;
				}
				this._fGestureWasFired = true;
				if (report != null && report.InputSource != null && this._eventStylusPoints != null && this._eventStylusPoints.Count > 0)
				{
					StylusPoint stylusPoint = this._eventStylusPoints[this._eventStylusPoints.Count - 1];
					stylusPoint.X = (double)report.GestureX;
					stylusPoint.Y = (double)report.GestureY;
					this._eventStylusPoints = new StylusPointCollection(stylusPoint.Description, stylusPoint.GetPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
					PresentationSource presentationSource = this.DetermineValidSource(report.InputSource, this._eventStylusPoints, report.PenContext.Contexts);
					if (presentationSource != null)
					{
						if (presentationSource != report.InputSource)
						{
							Point measurePoint = PointUtil.ClientToScreen(new Point(0.0, 0.0), presentationSource);
							measurePoint = this._stylusLogic.MeasureUnitsFromDeviceUnits(measurePoint);
							Point point = this._stylusLogic.MeasureUnitsFromDeviceUnits(report.PenContext.Contexts.DestroyedLocation);
							MatrixTransform transform = new MatrixTransform(new Matrix(1.0, 0.0, 0.0, 1.0, point.X - measurePoint.X, point.Y - measurePoint.Y));
							this._eventStylusPoints = this._eventStylusPoints.Reformat(report.StylusPointDescription, transform);
						}
						this._rawPosition = this._eventStylusPoints[this._eventStylusPoints.Count - 1];
						this._inputSource = new SecurityCriticalDataClass<PresentationSource>(presentationSource);
						Point pointClient = this._stylusLogic.DeviceUnitsFromMeasureUnits((Point)this._rawPosition);
						this._lastScreenLocation = PointUtil.ClientToScreen(pointClient, presentationSource);
					}
				}
				break;
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0005989C File Offset: 0x00058C9C
		[SecurityCritical]
		internal void PlayBackCachedDownInputReport(int timestamp)
		{
			if (this._needToSendMouseDown)
			{
				PresentationSource mousePresentationSource = this.GetMousePresentationSource();
				if (mousePresentationSource != null)
				{
					Point point = PointUtil.ScreenToClient(this._lastMouseScreenLocation, mousePresentationSource);
					this._needToSendMouseDown = false;
					this._promotedMouseState = MouseButtonState.Pressed;
					RawMouseActions rawMouseActions = this._fLeftButtonDownTrigger ? RawMouseActions.Button1Press : RawMouseActions.Button2Press;
					if (this._stylusLogic.UpdateMouseButtonState(rawMouseActions))
					{
						InputManager inputManager = (InputManager)base.Dispatcher.InputManager;
						if (inputManager != null && inputManager.PrimaryMouseDevice.CriticalActiveSource != mousePresentationSource)
						{
							rawMouseActions |= RawMouseActions.Activate;
						}
						RawMouseInputReport report = new RawMouseInputReport(InputMode.Foreground, timestamp, mousePresentationSource, rawMouseActions, (int)point.X, (int)point.Y, 0, IntPtr.Zero);
						InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(base.StylusDevice, report);
						inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
						this._stylusLogic.InputManagerProcessInputEventArgs(inputReportEventArgs);
					}
				}
				this._needToSendMouseDown = false;
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00059970 File Offset: 0x00058D70
		[SecurityCritical]
		internal PresentationSource GetMousePresentationSource()
		{
			InputManager inputManager = (InputManager)base.Dispatcher.InputManager;
			PresentationSource result = null;
			if (inputManager != null)
			{
				IInputElement captured = inputManager.PrimaryMouseDevice.Captured;
				if (captured != null)
				{
					DependencyObject containingVisual = InputElement.GetContainingVisual((DependencyObject)captured);
					if (containingVisual != null)
					{
						result = PresentationSource.CriticalFromVisual(containingVisual);
					}
				}
				else if (this._stylusOver != null)
				{
					result = ((this._inputSource != null && this._inputSource.Value != null) ? this.DetermineValidSource(this._inputSource.Value, this._eventStylusPoints, null) : null);
				}
			}
			return result;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000599F4 File Offset: 0x00058DF4
		[SecurityCritical]
		internal RawMouseActions GetMouseActionsFromStylusEventAndPlaybackCachedDown(RoutedEvent stylusEvent, StylusEventArgs stylusArgs)
		{
			if (stylusEvent == Stylus.StylusSystemGestureEvent)
			{
				StylusSystemGestureEventArgs stylusSystemGestureEventArgs = (StylusSystemGestureEventArgs)stylusArgs;
				if (stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Tap || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.RightTap || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Drag || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.RightDrag || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Flick)
				{
					this.UpdateStateForSystemGesture(stylusSystemGestureEventArgs.SystemGesture, null);
					if (stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Drag || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.RightDrag || stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Flick)
					{
						this._fBlockMouseMoveChanges = false;
						this.TapCount = 1;
						if (stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Flick)
						{
							this._needToSendMouseDown = false;
						}
						else
						{
							this.PlayBackCachedDownInputReport(stylusSystemGestureEventArgs.Timestamp);
						}
					}
					else
					{
						this.PlayBackCachedDownInputReport(stylusSystemGestureEventArgs.Timestamp);
					}
				}
			}
			else
			{
				if (stylusEvent == Stylus.StylusInAirMoveEvent)
				{
					return RawMouseActions.AbsoluteMove;
				}
				if (stylusEvent == Stylus.StylusDownEvent)
				{
					this._fLeftButtonDownTrigger = true;
					this._fBlockMouseMoveChanges = true;
					if (this._seenDoubleTapGesture || this._sawMouseButton1Down)
					{
						this.PlayBackCachedDownInputReport(stylusArgs.Timestamp);
					}
				}
				else if (stylusEvent == Stylus.StylusMoveEvent)
				{
					if (!this._fBlockMouseMoveChanges)
					{
						return RawMouseActions.AbsoluteMove;
					}
				}
				else if (stylusEvent == Stylus.StylusUpEvent)
				{
					MouseButtonState promotedMouseState = this._promotedMouseState;
					this.ResetStateForStylusUp();
					if (promotedMouseState == MouseButtonState.Pressed)
					{
						RawMouseActions rawMouseActions = this._fLeftButtonDownTrigger ? RawMouseActions.Button1Release : RawMouseActions.Button2Release;
						if (this._stylusLogic.UpdateMouseButtonState(rawMouseActions))
						{
							return rawMouseActions;
						}
					}
				}
			}
			return RawMouseActions.None;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00059B4C File Offset: 0x00058F4C
		internal void ResetStateForStylusUp()
		{
			this._fBlockMouseMoveChanges = false;
			this._seenDoubleTapGesture = false;
			this._sawMouseButton1Down = false;
			if (this._promotedMouseState == MouseButtonState.Pressed)
			{
				this._promotedMouseState = MouseButtonState.Released;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00059B80 File Offset: 0x00058F80
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x00059B94 File Offset: 0x00058F94
		internal Point LastMouseScreenPoint
		{
			get
			{
				return this._lastMouseScreenLocation;
			}
			set
			{
				this._lastMouseScreenLocation = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00059BA8 File Offset: 0x00058FA8
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x00059BBC File Offset: 0x00058FBC
		internal bool SeenDoubleTapGesture
		{
			get
			{
				return this._seenDoubleTapGesture;
			}
			set
			{
				this._seenDoubleTapGesture = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00059BD0 File Offset: 0x00058FD0
		internal bool SeenHoldEnterGesture
		{
			get
			{
				return this._seenHoldEnterGesture;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00059BE4 File Offset: 0x00058FE4
		internal bool GestureWasFired
		{
			get
			{
				return this._fGestureWasFired;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00059BF8 File Offset: 0x00058FF8
		internal bool SentMouseDown
		{
			get
			{
				return this._promotedMouseState == MouseButtonState.Pressed;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00059C10 File Offset: 0x00059010
		internal bool DetectedDrag
		{
			get
			{
				return this._fDetectedDrag;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00059C24 File Offset: 0x00059024
		internal bool LeftIsActiveMouseButton
		{
			get
			{
				return this._fLeftButtonDownTrigger;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00059C38 File Offset: 0x00059038
		internal void SetSawMouseButton1Down(bool sawMouseButton1Down)
		{
			this._sawMouseButton1Down = sawMouseButton1Down;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x00059C4C File Offset: 0x0005904C
		// (set) Token: 0x0600170B RID: 5899 RVA: 0x00059C60 File Offset: 0x00059060
		internal bool IgnoreStroke
		{
			get
			{
				return this._ignoreStroke;
			}
			set
			{
				this._ignoreStroke = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00059C74 File Offset: 0x00059074
		internal WispStylusTouchDevice TouchDevice
		{
			get
			{
				if (this._touchDevice == null)
				{
					this._touchDevice = new WispStylusTouchDevice(this);
				}
				return this._touchDevice;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00059C9C File Offset: 0x0005909C
		[SecuritySafeCritical]
		internal void UpdateTouchActiveSource()
		{
			if (this._touchDevice != null)
			{
				PresentationSource criticalActiveSource = this.CriticalActiveSource;
				if (criticalActiveSource != null)
				{
					this._touchDevice.ChangeActiveSource(criticalActiveSource);
				}
			}
		}

		// Token: 0x04000C66 RID: 3174
		private WispTabletDevice _tabletDevice;

		// Token: 0x04000C67 RID: 3175
		private string _sName;

		// Token: 0x04000C68 RID: 3176
		private int _id;

		// Token: 0x04000C69 RID: 3177
		private bool _fInverted;

		// Token: 0x04000C6A RID: 3178
		private bool _fInRange;

		// Token: 0x04000C6B RID: 3179
		private StylusButtonCollection _stylusButtonCollection;

		// Token: 0x04000C6C RID: 3180
		private IInputElement _stylusOver;

		// Token: 0x04000C6D RID: 3181
		private IInputElement _stylusCapture;

		// Token: 0x04000C6E RID: 3182
		private CaptureMode _captureMode;

		// Token: 0x04000C6F RID: 3183
		private StylusPoint _rawPosition = new StylusPoint(0.0, 0.0);

		// Token: 0x04000C70 RID: 3184
		private Point _rawElementRelativePosition = new Point(0.0, 0.0);

		// Token: 0x04000C71 RID: 3185
		private StylusPointCollection _eventStylusPoints;

		// Token: 0x04000C72 RID: 3186
		private SecurityCriticalDataClass<PresentationSource> _inputSource;

		// Token: 0x04000C73 RID: 3187
		private SecurityCriticalDataClass<PenContext> _activePenContext;

		// Token: 0x04000C74 RID: 3188
		private bool _needToSendMouseDown;

		// Token: 0x04000C75 RID: 3189
		private Point _lastMouseScreenLocation = new Point(0.0, 0.0);

		// Token: 0x04000C76 RID: 3190
		private Point _lastScreenLocation = new Point(0.0, 0.0);

		// Token: 0x04000C77 RID: 3191
		private bool _fInAir = true;

		// Token: 0x04000C78 RID: 3192
		private bool _fLeftButtonDownTrigger = true;

		// Token: 0x04000C79 RID: 3193
		private bool _fGestureWasFired = true;

		// Token: 0x04000C7A RID: 3194
		private bool _fBlockMouseMoveChanges;

		// Token: 0x04000C7B RID: 3195
		private bool _fDetectedDrag;

		// Token: 0x04000C7C RID: 3196
		private MouseButtonState _promotedMouseState;

		// Token: 0x04000C7D RID: 3197
		private StylusPlugInCollection _nonVerifiedTarget;

		// Token: 0x04000C7E RID: 3198
		private StylusPlugInCollection _verifiedTarget;

		// Token: 0x04000C7F RID: 3199
		private object _rtiCaptureChanged = new object();

		// Token: 0x04000C80 RID: 3200
		private StylusPlugInCollection _stylusCapturePlugInCollection;

		// Token: 0x04000C81 RID: 3201
		private Point _lastTapXY = new Point(0.0, 0.0);

		// Token: 0x04000C82 RID: 3202
		private int _tapCount;

		// Token: 0x04000C83 RID: 3203
		private int _lastTapTime;

		// Token: 0x04000C84 RID: 3204
		private bool _lastTapBarrelDown;

		// Token: 0x04000C85 RID: 3205
		private bool _seenDoubleTapGesture;

		// Token: 0x04000C86 RID: 3206
		private bool _seenHoldEnterGesture;

		// Token: 0x04000C87 RID: 3207
		private bool _sawMouseButton1Down;

		// Token: 0x04000C88 RID: 3208
		private bool _ignoreStroke;

		// Token: 0x04000C89 RID: 3209
		[SecurityCritical]
		private WispLogic _stylusLogic;

		// Token: 0x04000C8A RID: 3210
		private WispStylusTouchDevice _touchDevice;
	}
}
