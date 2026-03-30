using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Interop;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002EC RID: 748
	internal class PointerStylusDevice : StylusDeviceBase
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0005D3E4 File Offset: 0x0005C7E4
		[SecurityCritical]
		internal PointerStylusDevice(PointerTabletDevice tabletDevice, UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO cursorInfo)
		{
			this._cursorInfo = cursorInfo;
			this._tabletDevice = tabletDevice;
			this._pointerLogic = StylusLogic.GetCurrentStylusLogicAs<PointerLogic>();
			if (tabletDevice.Type == TabletDeviceType.Touch)
			{
				this.TouchDevice = new PointerTouchDevice(this);
			}
			this._interactionEngine = new PointerInteractionEngine(this, null);
			this._interactionEngine.InteractionDetected += this.HandleInteraction;
			List<StylusButton> list = new List<StylusButton>();
			foreach (StylusPointProperty stylusPointProperty in this._tabletDevice.DeviceInfo.StylusPointProperties)
			{
				if (stylusPointProperty.IsButton)
				{
					StylusButton stylusButton = new StylusButton(StylusPointPropertyIds.GetStringRepresentation(stylusPointProperty.Id), stylusPointProperty.Id);
					stylusButton.SetOwner(this);
					list.Add(stylusButton);
				}
			}
			this._stylusButtons = new StylusButtonCollection(list);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0005D4FC File Offset: 0x0005C8FC
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this._interactionEngine.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0005D528 File Offset: 0x0005C928
		internal override IInputElement Target
		{
			get
			{
				return this.DirectlyOver;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0005D53C File Offset: 0x0005C93C
		internal override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				return this._inputSource.Value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0005D554 File Offset: 0x0005C954
		internal UnsafeNativeMethods.POINTER_INFO CurrentPointerInfo
		{
			get
			{
				return this._pointerData.Info;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0005D56C File Offset: 0x0005C96C
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0005D580 File Offset: 0x0005C980
		internal HwndPointerInputProvider CurrentPointerProvider { [SecurityCritical] get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0005D594 File Offset: 0x0005C994
		internal uint CursorId
		{
			get
			{
				return this._cursorInfo.cursorId;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0005D5AC File Offset: 0x0005C9AC
		internal bool IsNew
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_NEW);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005D5E0 File Offset: 0x0005C9E0
		internal bool IsInContact
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INCONTACT);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0005D614 File Offset: 0x0005CA14
		internal bool IsPrimary
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_PRIMARY);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0005D64C File Offset: 0x0005CA4C
		internal bool IsFirstButton
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_FIRSTBUTTON);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0005D680 File Offset: 0x0005CA80
		internal bool IsSecondButton
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_SECONDBUTTON);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0005D6B4 File Offset: 0x0005CAB4
		internal bool IsThirdButton
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_THIRDBUTTON);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005D6E8 File Offset: 0x0005CAE8
		internal bool IsFourthButton
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_FOURTHBUTTON);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005D720 File Offset: 0x0005CB20
		internal bool IsFifthButton
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_FIFTHBUTTON);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0005D758 File Offset: 0x0005CB58
		internal uint TimeStamp
		{
			get
			{
				PointerData pointerData = this._pointerData;
				if (pointerData == null)
				{
					return 0U;
				}
				return pointerData.Info.dwTime;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0005D77C File Offset: 0x0005CB7C
		internal bool IsDown
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_DOWN);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0005D7B4 File Offset: 0x0005CBB4
		internal bool IsUpdate
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_UPDATE);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0005D7EC File Offset: 0x0005CBEC
		internal bool IsUp
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_UP);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005D824 File Offset: 0x0005CC24
		internal bool HasCaptureChanged
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_CAPTURECHANGED);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0005D85C File Offset: 0x0005CC5C
		internal bool HasTransform
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_HASTRANSFORM);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005D894 File Offset: 0x0005CC94
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x0005D8A8 File Offset: 0x0005CCA8
		internal PointerTouchDevice TouchDevice { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0005D8BC File Offset: 0x0005CCBC
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x0005D8D0 File Offset: 0x0005CCD0
		internal override StylusPlugInCollection CurrentVerifiedTarget { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005D8E4 File Offset: 0x0005CCE4
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0005D908 File Offset: 0x0005CD08
		internal override StylusButtonCollection StylusButtons
		{
			get
			{
				return this._stylusButtons;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0005D91C File Offset: 0x0005CD1C
		internal override StylusPoint RawStylusPoint
		{
			get
			{
				return this._currentStylusPoints[this._currentStylusPoints.Count - 1];
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0005D944 File Offset: 0x0005CD44
		internal override bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0005D954 File Offset: 0x0005CD54
		internal override IInputElement DirectlyOver
		{
			get
			{
				return this._stylusOver;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0005D968 File Offset: 0x0005CD68
		internal override IInputElement Captured
		{
			get
			{
				return this._stylusCapture;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0005D97C File Offset: 0x0005CD7C
		internal override TabletDevice TabletDevice
		{
			get
			{
				return this._tabletDevice.TabletDevice;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0005D994 File Offset: 0x0005CD94
		internal PointerTabletDevice PointerTabletDevice
		{
			get
			{
				return this._tabletDevice;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x0005D9A8 File Offset: 0x0005CDA8
		internal override string Name
		{
			get
			{
				if (this._cursorInfo.cursor != UnsafeNativeMethods.POINTER_DEVICE_CURSOR_TYPE.POINTER_DEVICE_CURSOR_TYPE_ERASER)
				{
					return "Stylus";
				}
				return "Eraser";
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0005D9D0 File Offset: 0x0005CDD0
		internal override int Id
		{
			get
			{
				return (int)this.CursorId;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0005D9E4 File Offset: 0x0005CDE4
		internal override bool InAir
		{
			get
			{
				PointerData pointerData = this._pointerData;
				if (pointerData == null || !pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INCONTACT))
				{
					PointerData pointerData2 = this._pointerData;
					return pointerData2 != null && pointerData2.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INRANGE);
				}
				return false;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0005DA44 File Offset: 0x0005CE44
		internal override bool Inverted
		{
			get
			{
				if (this._tabletDevice.Type == TabletDeviceType.Stylus)
				{
					PointerData pointerData = this._pointerData;
					return pointerData != null && pointerData.PenInfo.penFlags.HasFlag(UnsafeNativeMethods.PEN_FLAGS.PEN_FLAG_INVERTED);
				}
				return false;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005DA88 File Offset: 0x0005CE88
		internal override bool InRange
		{
			get
			{
				PointerData pointerData = this._pointerData;
				return pointerData != null && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INRANGE);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0005DABC File Offset: 0x0005CEBC
		internal override int DoubleTapDeltaX
		{
			get
			{
				return (int)this.PointerTabletDevice.DoubleTapSize.Width;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0005DAE0 File Offset: 0x0005CEE0
		internal override int DoubleTapDeltaY
		{
			get
			{
				return (int)this.PointerTabletDevice.DoubleTapSize.Height;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0005DB04 File Offset: 0x0005CF04
		internal override int DoubleTapDeltaTime
		{
			[SecurityTreatAsSafe]
			get
			{
				return this.PointerTabletDevice.DoubleTapDeltaTime;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005DB1C File Offset: 0x0005CF1C
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x0005DB30 File Offset: 0x0005CF30
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005DB44 File Offset: 0x0005CF44
		internal override CaptureMode CapturedMode
		{
			get
			{
				return this._captureMode;
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005DB58 File Offset: 0x0005CF58
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
			UIElement uielement = element as UIElement;
			bool flag;
			if ((uielement != null && uielement.IsVisible) || (uielement != null && uielement.IsEnabled))
			{
				flag = true;
			}
			else
			{
				ContentElement contentElement = element as ContentElement;
				flag = (contentElement == null || !contentElement.IsEnabled || true);
			}
			if (flag)
			{
				this.ChangeStylusCapture(element, captureMode, tickCount);
			}
			return flag;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005DC24 File Offset: 0x0005D024
		internal override bool Capture(IInputElement element)
		{
			return this.Capture(element, CaptureMode.Element);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005DC3C File Offset: 0x0005D03C
		[SecurityCritical]
		internal override void Synchronize()
		{
			if (this.InRange && this._inputSource != null && this._inputSource.Value != null && this._inputSource.Value.CompositionTarget != null && !this._inputSource.Value.CompositionTarget.IsDisposed)
			{
				Point pointScreen = new Point((double)this._pointerData.Info.ptPixelLocationRaw.X, (double)this._pointerData.Info.ptPixelLocationRaw.Y);
				Point point = PointUtil.ScreenToClient(pointScreen, this._inputSource.Value);
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
					if (this._currentStylusPoints != null && this._currentStylusPoints.Count > 0 && StylusPointDescription.AreCompatible(this.PointerTabletDevice.StylusPointDescription, this._currentStylusPoints.Description))
					{
						int[] packetData = this._currentStylusPoints[this._currentStylusPoints.Count - 1].GetPacketData();
						Matrix tabletToScreen = this._tabletDevice.TabletToScreen;
						tabletToScreen.Invert();
						Point point2 = point * tabletToScreen;
						packetData[0] = (int)point2.X;
						packetData[1] = (int)point2.Y;
						RawStylusInputReport rawStylusInputReport = new RawStylusInputReport(InputMode.Foreground, tickCount, this._inputSource.Value, this.InAir ? RawStylusActions.InAirMove : RawStylusActions.Move, () => this.PointerTabletDevice.StylusPointDescription, this.TabletDevice.Id, this.Id, packetData);
						rawStylusInputReport.Synchronized = true;
						InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(base.StylusDevice, rawStylusInputReport);
						inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
						InputManager.Current.ProcessInput(inputReportEventArgs);
					}
				}
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005DE5C File Offset: 0x0005D25C
		internal override StylusPointCollection GetStylusPoints(IInputElement relativeTo)
		{
			base.VerifyAccess();
			if (this._currentStylusPoints == null)
			{
				return new StylusPointCollection(this._tabletDevice.StylusPointDescription);
			}
			return this._currentStylusPoints.Clone(StylusDevice.GetElementTransform(relativeTo), this._currentStylusPoints.Description);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005DEA4 File Offset: 0x0005D2A4
		internal override StylusPointCollection GetStylusPoints(IInputElement relativeTo, StylusPointDescription subsetToReformatTo)
		{
			if (subsetToReformatTo == null)
			{
				throw new ArgumentNullException("subsetToReformatTo");
			}
			if (this._currentStylusPoints == null)
			{
				return new StylusPointCollection(subsetToReformatTo);
			}
			return this._currentStylusPoints.Reformat(subsetToReformatTo, StylusDevice.GetElementTransform(relativeTo));
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005DEE0 File Offset: 0x0005D2E0
		[SecuritySafeCritical]
		internal override Point GetPosition(IInputElement relativeTo)
		{
			base.VerifyAccess();
			if (relativeTo != null && !InputElement.IsValid(relativeTo))
			{
				throw new InvalidOperationException();
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
			Point pointScreen = new Point((double)this._pointerData.Info.ptPixelLocationRaw.X, (double)this._pointerData.Info.ptPixelLocationRaw.Y);
			Point point = PointUtil.ScreenToClient(pointScreen, presentationSource);
			Point pt = PointUtil.ClientToRoot(point, presentationSource);
			return InputElement.TranslatePoint(pt, presentationSource.RootVisual, (DependencyObject)relativeTo);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0005DFB8 File Offset: 0x0005D3B8
		internal override Point GetMouseScreenPosition(MouseDevice mouseDevice)
		{
			return mouseDevice.GetScreenPositionFromSystem();
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005DFCC File Offset: 0x0005D3CC
		[SecuritySafeCritical]
		internal override MouseButtonState GetMouseButtonState(MouseButton mouseButton, MouseDevice mouseDevice)
		{
			return mouseDevice.GetButtonStateFromSystem(mouseButton);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0005DFE0 File Offset: 0x0005D3E0
		[SecurityCritical]
		internal void Update(HwndPointerInputProvider provider, PresentationSource inputSource, PointerData pointerData, RawStylusInputReport rsir)
		{
			this._lastEventTimeTicks = Environment.TickCount;
			this._inputSource = new SecurityCriticalDataClass<PresentationSource>(inputSource);
			this._pointerData = pointerData;
			this._currentStylusPoints = new StylusPointCollection(rsir.StylusPointDescription, rsir.GetRawPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
			bool? flag;
			if (rsir == null)
			{
				flag = null;
			}
			else
			{
				RawStylusInput rawStylusInput = rsir.RawStylusInput;
				flag = ((rawStylusInput != null) ? new bool?(rawStylusInput.StylusPointsModified) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				GeneralTransform inverse = rsir.RawStylusInput.Target.ViewToElement.Inverse;
				this._currentStylusPoints = rsir.RawStylusInput.GetStylusPoints(inverse);
			}
			this.CurrentPointerProvider = provider;
			if (this.PointerTabletDevice.Type == TabletDeviceType.Touch)
			{
				this.TouchDevice.ChangeActiveSource(this._inputSource.Value);
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0005E0C0 File Offset: 0x0005D4C0
		[SecurityCritical]
		internal void UpdateInteractions(RawStylusInputReport rsir)
		{
			this._interactionEngine.Update(rsir);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0005E0DC File Offset: 0x0005D4DC
		[SecurityCritical]
		private void HandleInteraction(object clientData, RawStylusSystemGestureInputReport originalReport)
		{
			RawStylusSystemGestureInputReport rawStylusSystemGestureInputReport = new RawStylusSystemGestureInputReport(InputMode.Foreground, Environment.TickCount, this.CriticalActiveSource, () => this.PointerTabletDevice.StylusPointDescription, this.TabletDevice.Id, this.Id, originalReport.SystemGesture, originalReport.GestureX, originalReport.GestureY, originalReport.ButtonState)
			{
				StylusDevice = base.StylusDevice
			};
			if (rawStylusSystemGestureInputReport.SystemGesture == SystemGesture.Flick)
			{
				StylusPoint stylusPoint = this._currentStylusPoints[this._currentStylusPoints.Count - 1];
				stylusPoint.X = (double)rawStylusSystemGestureInputReport.GestureX;
				stylusPoint.Y = (double)rawStylusSystemGestureInputReport.GestureY;
				this._currentStylusPoints = new StylusPointCollection(stylusPoint.Description, stylusPoint.GetPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
			}
			InputReportEventArgs input = new InputReportEventArgs(base.StylusDevice, rawStylusSystemGestureInputReport)
			{
				RoutedEvent = InputManager.PreviewInputReportEvent
			};
			InputManager.UnsecureCurrent.ProcessInput(input);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005E1C4 File Offset: 0x0005D5C4
		internal override StylusPlugInCollection GetCapturedPlugInCollection(ref bool elementHasCapture)
		{
			elementHasCapture = (this._stylusCapture != null);
			return this._stylusCapturePlugInCollection;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005E1E4 File Offset: 0x0005D5E4
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

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005E2D0 File Offset: 0x0005D6D0
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
			this._pointerLogic.UpdateOverProperty(this, this._stylusOver);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005E330 File Offset: 0x0005D730
		[SecuritySafeCritical]
		internal void ChangeStylusCapture(IInputElement stylusCapture, CaptureMode captureMode, int timestamp)
		{
			if (stylusCapture != this._stylusCapture)
			{
				IInputElement stylusCapture2 = this._stylusCapture;
				this._stylusCapture = stylusCapture;
				this._captureMode = captureMode;
				this._stylusCapturePlugInCollection = null;
				if (stylusCapture != null)
				{
					UIElement uielement = InputElement.GetContainingUIElement(stylusCapture as DependencyObject) as UIElement;
					if (uielement != null)
					{
						PresentationSource presentationSource = PresentationSource.CriticalFromVisual(uielement);
						PointerStylusPlugInManager pointerStylusPlugInManager;
						if (presentationSource != null && this._pointerLogic.PlugInManagers.TryGetValue(presentationSource, out pointerStylusPlugInManager))
						{
							this._stylusCapturePlugInCollection = pointerStylusPlugInManager.FindPlugInCollection(uielement);
						}
					}
				}
				this._pointerLogic.UpdateStylusCapture(this, stylusCapture2, this._stylusCapture, timestamp);
				if (stylusCapture2 != null)
				{
					StylusEventArgs stylusEventArgs = new StylusEventArgs(base.StylusDevice, timestamp);
					stylusEventArgs.RoutedEvent = Stylus.LostStylusCaptureEvent;
					stylusEventArgs.Source = stylusCapture2;
					InputManager.UnsecureCurrent.ProcessInput(stylusEventArgs);
				}
				if (this._stylusCapture != null)
				{
					StylusEventArgs stylusEventArgs2 = new StylusEventArgs(base.StylusDevice, timestamp);
					stylusEventArgs2.RoutedEvent = Stylus.GotStylusCaptureEvent;
					stylusEventArgs2.Source = this._stylusCapture;
					InputManager.UnsecureCurrent.ProcessInput(stylusEventArgs2);
				}
				if (this._pointerLogic.CurrentStylusDevice == this || this.InRange)
				{
					if (this._stylusCapture != null)
					{
						IInputElement stylusOver = this._stylusCapture;
						if (this.CapturedMode == CaptureMode.SubTree && this._inputSource != null && this._inputSource.Value != null)
						{
							Point position = this._pointerLogic.DeviceUnitsFromMeasureUnits(this.GetPosition(null));
							stylusOver = this.FindTarget(this._inputSource.Value, position);
						}
						this.ChangeStylusOver(stylusOver);
					}
					else if (this._inputSource != null && this._inputSource.Value != null)
					{
						Point point = this.GetPosition(null);
						point = this._pointerLogic.DeviceUnitsFromMeasureUnits(point);
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

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005E518 File Offset: 0x0005D918
		[SecurityCritical]
		internal override void UpdateEventStylusPoints(RawStylusInputReport report, bool resetIfNoOverride)
		{
			if (report.RawStylusInput != null && report.RawStylusInput.StylusPointsModified)
			{
				GeneralTransform inverse = report.RawStylusInput.Target.ViewToElement.Inverse;
				this._currentStylusPoints = report.RawStylusInput.GetStylusPoints(inverse);
				return;
			}
			if (resetIfNoOverride)
			{
				this._currentStylusPoints = new StylusPointCollection(report.StylusPointDescription, report.GetRawPacketData(), this.GetTabletToElementTransform(null), Matrix.Identity);
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005E58C File Offset: 0x0005D98C
		[SecuritySafeCritical]
		internal GeneralTransform GetTabletToElementTransform(IInputElement relativeTo)
		{
			GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
			Matrix transformToDevice = this._inputSource.Value.CompositionTarget.TransformToDevice;
			transformToDevice.Invert();
			generalTransformGroup.Children.Add(new MatrixTransform(this.PointerTabletDevice.TabletToScreen * transformToDevice));
			generalTransformGroup.Children.Add(StylusDevice.GetElementTransform(relativeTo));
			return generalTransformGroup;
		}

		// Token: 0x04000CF0 RID: 3312
		private int _tapCount = 1;

		// Token: 0x04000CF1 RID: 3313
		private StylusButtonCollection _stylusButtons;

		// Token: 0x04000CF2 RID: 3314
		private PointerInteractionEngine _interactionEngine;

		// Token: 0x04000CF3 RID: 3315
		private StylusPlugInCollection _stylusCapturePlugInCollection;

		// Token: 0x04000CF4 RID: 3316
		[SecurityCritical]
		private PointerLogic _pointerLogic;

		// Token: 0x04000CF5 RID: 3317
		private IInputElement _stylusCapture;

		// Token: 0x04000CF6 RID: 3318
		private CaptureMode _captureMode;

		// Token: 0x04000CF7 RID: 3319
		private IInputElement _stylusOver;

		// Token: 0x04000CF8 RID: 3320
		private Point _rawElementRelativePosition = new Point(0.0, 0.0);

		// Token: 0x04000CF9 RID: 3321
		private SecurityCriticalDataClass<PresentationSource> _inputSource;

		// Token: 0x04000CFA RID: 3322
		private int _lastEventTimeTicks;

		// Token: 0x04000CFB RID: 3323
		private PointerData _pointerData;

		// Token: 0x04000CFC RID: 3324
		private UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO _cursorInfo;

		// Token: 0x04000CFD RID: 3325
		private PointerTabletDevice _tabletDevice;

		// Token: 0x04000CFE RID: 3326
		private StylusPointCollection _currentStylusPoints;
	}
}
