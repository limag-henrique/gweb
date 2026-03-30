using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input.StylusPointer;
using System.Windows.Input.StylusWisp;
using System.Windows.Input.Tracing;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Win32;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002C7 RID: 711
	internal abstract class StylusLogic : DispatcherObject
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0004EDF4 File Offset: 0x0004E1F4
		internal static bool IsInstantiated
		{
			[SecuritySafeCritical]
			get
			{
				SecurityCriticalDataClass<StylusLogic> currentStylusLogic = StylusLogic._currentStylusLogic;
				return ((currentStylusLogic != null) ? currentStylusLogic.Value : null) != null;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0004EE18 File Offset: 0x0004E218
		internal static bool IsStylusAndTouchSupportEnabled
		{
			get
			{
				return !CoreAppContextSwitches.DisableStylusAndTouchSupport;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0004EE30 File Offset: 0x0004E230
		internal static bool IsPointerStackEnabled
		{
			[SecurityCritical]
			get
			{
				if (StylusLogic._isPointerStackEnabled == null)
				{
					StylusLogic._isPointerStackEnabled = new bool?(StylusLogic.IsStylusAndTouchSupportEnabled && (CoreAppContextSwitches.EnablePointerSupport || StylusLogic.IsPointerEnabledInRegistry) && OSVersionHelper.IsOsWindows10RS2OrGreater);
				}
				return StylusLogic._isPointerStackEnabled.Value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0004EE7C File Offset: 0x0004E27C
		internal static StylusLogic CurrentStylusLogic
		{
			[SecurityCritical]
			get
			{
				SecurityCriticalDataClass<StylusLogic> currentStylusLogic = StylusLogic._currentStylusLogic;
				if (((currentStylusLogic != null) ? currentStylusLogic.Value : null) == null)
				{
					StylusLogic.Initialize();
				}
				SecurityCriticalDataClass<StylusLogic> currentStylusLogic2 = StylusLogic._currentStylusLogic;
				if (currentStylusLogic2 == null)
				{
					return null;
				}
				return currentStylusLogic2.Value;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004EEB4 File Offset: 0x0004E2B4
		[SecurityCritical]
		internal static T GetCurrentStylusLogicAs<T>() where T : StylusLogic
		{
			return StylusLogic.CurrentStylusLogic as T;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0004EED0 File Offset: 0x0004E2D0
		[SecurityCritical]
		private static void Initialize()
		{
			if (StylusLogic.IsStylusAndTouchSupportEnabled)
			{
				if (StylusLogic.IsPointerStackEnabled)
				{
					StylusLogic._currentStylusLogic = new SecurityCriticalDataClass<StylusLogic>(new PointerLogic(InputManager.UnsecureCurrent));
					return;
				}
				StylusLogic._currentStylusLogic = new SecurityCriticalDataClass<StylusLogic>(new WispLogic(InputManager.UnsecureCurrent));
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004EF14 File Offset: 0x0004E314
		private static bool IsPointerEnabledInRegistry
		{
			[SecurityCritical]
			[RegistryPermission(SecurityAction.Assert, Read = "HKEY_CURRENT_USER\\Software\\Microsoft\\Avalon.Touch\\")]
			get
			{
				bool result = false;
				try
				{
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Avalon.Touch\\", RegistryKeyPermissionCheck.ReadSubTree);
					result = ((int)(((registryKey != null) ? registryKey.GetValue("EnablePointerSupport", 0) : null) ?? 0) == 1);
				}
				catch (Exception ex) when (ex is SecurityException || ex is IOException)
				{
				}
				return result;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0004EFA8 File Offset: 0x0004E3A8
		internal int StylusDoubleTapDelta
		{
			get
			{
				return this._stylusDoubleTapDelta;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0004EFBC File Offset: 0x0004E3BC
		internal int TouchDoubleTapDelta
		{
			get
			{
				return this._touchDoubleTapDelta;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004EFD0 File Offset: 0x0004E3D0
		internal int StylusDoubleTapDeltaTime
		{
			get
			{
				return this._stylusDoubleTapDeltaTime;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0004EFE4 File Offset: 0x0004E3E4
		internal int TouchDoubleTapDeltaTime
		{
			get
			{
				return this._touchDoubleTapDeltaTime;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004EFF8 File Offset: 0x0004E3F8
		[SecurityCritical]
		protected void ReadSystemConfig()
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_CURRENT_USER\\Software\\Microsoft\\Wisp\\").Assert();
			try
			{
				registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Wisp\\Software\\Microsoft\\Wisp\\Pen\\SysEventParameters");
				if (registryKey != null)
				{
					object value = registryKey.GetValue("DlbDist");
					this._stylusDoubleTapDelta = ((value == null) ? this._stylusDoubleTapDelta : ((int)value));
					value = registryKey.GetValue("DlbTime");
					this._stylusDoubleTapDeltaTime = ((value == null) ? this._stylusDoubleTapDeltaTime : ((int)value));
					value = registryKey.GetValue("Cancel");
					this._cancelDelta = ((value == null) ? this._cancelDelta : ((int)value));
				}
				registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Wisp\\Software\\Microsoft\\Wisp\\Touch");
				if (registryKey2 != null)
				{
					object value = registryKey2.GetValue("TouchModeN_DtapDist");
					this._touchDoubleTapDelta = ((value == null) ? this._touchDoubleTapDelta : StylusLogic.FitToCplCurve((double)this._touchDoubleTapDelta * 0.7, (double)this._touchDoubleTapDelta, (double)this._touchDoubleTapDelta * 1.3, (int)value));
					value = registryKey2.GetValue("TouchModeN_DtapTime");
					this._touchDoubleTapDeltaTime = ((value == null) ? this._touchDoubleTapDeltaTime : StylusLogic.FitToCplCurve((double)this._touchDoubleTapDeltaTime * 0.7, (double)this._touchDoubleTapDeltaTime, (double)this._touchDoubleTapDeltaTime * 1.3, (int)value));
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
		}

		// Token: 0x0600153D RID: 5437
		internal abstract void UpdateOverProperty(StylusDeviceBase stylusDevice, IInputElement newOver);

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600153E RID: 5438
		internal abstract StylusDeviceBase CurrentStylusDevice { get; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600153F RID: 5439
		internal abstract TabletDeviceCollection TabletDevices { get; }

		// Token: 0x06001540 RID: 5440
		internal abstract Point DeviceUnitsFromMeasureUnits(Point measurePoint);

		// Token: 0x06001541 RID: 5441
		internal abstract Point MeasureUnitsFromDeviceUnits(Point measurePoint);

		// Token: 0x06001542 RID: 5442
		internal abstract void UpdateStylusCapture(StylusDeviceBase stylusDevice, IInputElement oldStylusDeviceCapture, IInputElement newStylusDeviceCapture, int timestamp);

		// Token: 0x06001543 RID: 5443
		internal abstract void ReevaluateCapture(DependencyObject element, DependencyObject oldParent, bool isCoreParent);

		// Token: 0x06001544 RID: 5444
		internal abstract void ReevaluateStylusOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent);

		// Token: 0x06001545 RID: 5445
		protected abstract void OnTabletRemoved(uint wisptisIndex);

		// Token: 0x06001546 RID: 5446
		[SecurityCritical]
		[FriendAccessAllowed]
		internal abstract void HandleMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004F184 File Offset: 0x0004E584
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0004F198 File Offset: 0x0004E598
		protected StylusLogic.StylusLogicShutDownListener ShutdownListener { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0004F1AC File Offset: 0x0004E5AC
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x0004F1C0 File Offset: 0x0004E5C0
		public StylusTraceLogger.StylusStatistics Statistics { get; protected set; } = new StylusTraceLogger.StylusStatistics();

		// Token: 0x0600154B RID: 5451 RVA: 0x0004F1D4 File Offset: 0x0004E5D4
		private static int FitToCplCurve(double vMin, double vMid, double vMax, int value)
		{
			if (value < 0)
			{
				return (int)vMin;
			}
			if (value > 100)
			{
				return (int)vMax;
			}
			double num = (double)value / 100.0;
			double num2;
			if (num <= 0.5)
			{
				num2 = vMin + 2.0 * num * (vMid - vMin);
			}
			else
			{
				num2 = vMid + 2.0 * (num - 0.5) * (vMax - vMid);
			}
			return (int)num2;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004F248 File Offset: 0x0004E648
		[SecurityCritical]
		internal static bool IsPromotedMouseEvent(RawMouseInputReport mouseInputReport)
		{
			int num = NativeMethods.IntPtrToInt32(mouseInputReport.ExtraInformation);
			return ((long)num & (long)((ulong)-256)) == (long)((ulong)-11446528);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004F274 File Offset: 0x0004E674
		[SecurityCritical]
		internal static uint GetCursorIdFromMouseEvent(RawMouseInputReport mouseInputReport)
		{
			int num = NativeMethods.IntPtrToInt32(mouseInputReport.ExtraInformation);
			return (uint)(num & 127);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0004F294 File Offset: 0x0004E694
		[SecuritySafeCritical]
		internal static void CurrentStylusLogicReevaluateStylusOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			StylusLogic.CurrentStylusLogic.ReevaluateStylusOver(element, oldParent, isCoreParent);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0004F2B0 File Offset: 0x0004E6B0
		[SecuritySafeCritical]
		internal static void CurrentStylusLogicReevaluateCapture(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			StylusLogic.CurrentStylusLogic.ReevaluateCapture(element, oldParent, isCoreParent);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004F2CC File Offset: 0x0004E6CC
		internal static RoutedEvent GetMainEventFromPreviewEvent(RoutedEvent routedEvent)
		{
			if (routedEvent == Stylus.PreviewStylusDownEvent)
			{
				return Stylus.StylusDownEvent;
			}
			if (routedEvent == Stylus.PreviewStylusUpEvent)
			{
				return Stylus.StylusUpEvent;
			}
			if (routedEvent == Stylus.PreviewStylusMoveEvent)
			{
				return Stylus.StylusMoveEvent;
			}
			if (routedEvent == Stylus.PreviewStylusInAirMoveEvent)
			{
				return Stylus.StylusInAirMoveEvent;
			}
			if (routedEvent == Stylus.PreviewStylusInRangeEvent)
			{
				return Stylus.StylusInRangeEvent;
			}
			if (routedEvent == Stylus.PreviewStylusOutOfRangeEvent)
			{
				return Stylus.StylusOutOfRangeEvent;
			}
			if (routedEvent == Stylus.PreviewStylusSystemGestureEvent)
			{
				return Stylus.StylusSystemGestureEvent;
			}
			if (routedEvent == Stylus.PreviewStylusButtonDownEvent)
			{
				return Stylus.StylusButtonDownEvent;
			}
			if (routedEvent == Stylus.PreviewStylusButtonUpEvent)
			{
				return Stylus.StylusButtonUpEvent;
			}
			return null;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004F358 File Offset: 0x0004E758
		internal static RoutedEvent GetPreviewEventFromRawStylusActions(RawStylusActions actions)
		{
			RoutedEvent result = null;
			if (actions <= RawStylusActions.Move)
			{
				if (actions != RawStylusActions.Down)
				{
					if (actions != RawStylusActions.Up)
					{
						if (actions == RawStylusActions.Move)
						{
							result = Stylus.PreviewStylusMoveEvent;
						}
					}
					else
					{
						result = Stylus.PreviewStylusUpEvent;
					}
				}
				else
				{
					result = Stylus.PreviewStylusDownEvent;
				}
			}
			else if (actions <= RawStylusActions.InRange)
			{
				if (actions != RawStylusActions.InAirMove)
				{
					if (actions == RawStylusActions.InRange)
					{
						result = Stylus.PreviewStylusInRangeEvent;
					}
				}
				else
				{
					result = Stylus.PreviewStylusInAirMoveEvent;
				}
			}
			else if (actions != RawStylusActions.OutOfRange)
			{
				if (actions == RawStylusActions.SystemGesture)
				{
					result = Stylus.PreviewStylusSystemGestureEvent;
				}
			}
			else
			{
				result = Stylus.PreviewStylusOutOfRangeEvent;
			}
			return result;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0004F3D8 File Offset: 0x0004E7D8
		protected bool ValidateUIElementForCapture(UIElement element)
		{
			return element.IsEnabled && element.IsVisible && element.IsHitTestVisible;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0004F400 File Offset: 0x0004E800
		protected bool ValidateContentElementForCapture(ContentElement element)
		{
			return element.IsEnabled;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0004F414 File Offset: 0x0004E814
		protected bool ValidateUIElement3DForCapture(UIElement3D element)
		{
			return element.IsEnabled && element.IsVisible && element.IsHitTestVisible;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0004F43C File Offset: 0x0004E83C
		[SecuritySafeCritical]
		protected bool ValidateVisualForCapture(DependencyObject visual, StylusDeviceBase currentStylusDevice)
		{
			if (visual == null)
			{
				return false;
			}
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(visual);
			return presentationSource != null && (currentStylusDevice == null || currentStylusDevice.CriticalActiveSource == presentationSource || currentStylusDevice.Captured != null);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0004F474 File Offset: 0x0004E874
		internal static StylusLogic.FlickAction GetFlickAction(int flickData)
		{
			return (StylusLogic.FlickAction)(flickData & 31);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0004F488 File Offset: 0x0004E888
		protected static bool GetIsScrollUp(int flickData)
		{
			return NativeMethods.SignedHIWORD(flickData) == 0;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0004F4A0 File Offset: 0x0004E8A0
		internal bool HandleFlick(int flickData, IInputElement element)
		{
			bool result = false;
			switch (StylusLogic.GetFlickAction(flickData))
			{
			case StylusLogic.FlickAction.Scroll:
			{
				RoutedUICommand routedUICommand = StylusLogic.GetIsScrollUp(flickData) ? ComponentCommands.ScrollPageUp : ComponentCommands.ScrollPageDown;
				if (element != null)
				{
					if (routedUICommand.CanExecute(null, element))
					{
						this.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.FlickScrollingUsed;
						routedUICommand.Execute(null, element);
					}
					result = true;
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x04000B86 RID: 2950
		private const int FlickCommandMask = 31;

		// Token: 0x04000B87 RID: 2951
		private const string WispKeyAssert = "HKEY_CURRENT_USER\\Software\\Microsoft\\Wisp\\";

		// Token: 0x04000B88 RID: 2952
		private const string WispRootKey = "Software\\Microsoft\\Wisp\\";

		// Token: 0x04000B89 RID: 2953
		private const string WispPenSystemEventParametersKey = "Software\\Microsoft\\Wisp\\Software\\Microsoft\\Wisp\\Pen\\SysEventParameters";

		// Token: 0x04000B8A RID: 2954
		private const string WispTouchConfigKey = "Software\\Microsoft\\Wisp\\Software\\Microsoft\\Wisp\\Touch";

		// Token: 0x04000B8B RID: 2955
		private const string WispDoubleTapDistanceValue = "DlbDist";

		// Token: 0x04000B8C RID: 2956
		private const string WispDoubleTapTimeValue = "DlbTime";

		// Token: 0x04000B8D RID: 2957
		private const string WispCancelDeltaValue = "Cancel";

		// Token: 0x04000B8E RID: 2958
		private const string WispTouchDoubleTapDistanceValue = "TouchModeN_DtapDist";

		// Token: 0x04000B8F RID: 2959
		private const string WispTouchDoubleTapTimeValue = "TouchModeN_DtapTime";

		// Token: 0x04000B90 RID: 2960
		private const string WpfPointerKeyAssert = "HKEY_CURRENT_USER\\Software\\Microsoft\\Avalon.Touch\\";

		// Token: 0x04000B91 RID: 2961
		private const string WpfPointerKey = "Software\\Microsoft\\Avalon.Touch\\";

		// Token: 0x04000B92 RID: 2962
		private const string WpfPointerValue = "EnablePointerSupport";

		// Token: 0x04000B93 RID: 2963
		private const uint PromotedMouseEventTag = 4283520768U;

		// Token: 0x04000B94 RID: 2964
		private const uint PromotedMouseEventMask = 4294967040U;

		// Token: 0x04000B95 RID: 2965
		private const byte PromotedMouseEventCursorIdMask = 127;

		// Token: 0x04000B96 RID: 2966
		protected int _stylusDoubleTapDeltaTime = 800;

		// Token: 0x04000B97 RID: 2967
		protected int _stylusDoubleTapDelta = 15;

		// Token: 0x04000B98 RID: 2968
		protected int _cancelDelta = 10;

		// Token: 0x04000B99 RID: 2969
		protected int _touchDoubleTapDeltaTime = 300;

		// Token: 0x04000B9A RID: 2970
		protected int _touchDoubleTapDelta = 45;

		// Token: 0x04000B9B RID: 2971
		protected const double DoubleTapMinFactor = 0.7;

		// Token: 0x04000B9C RID: 2972
		protected const double DoubleTapMaxFactor = 1.3;

		// Token: 0x04000B9D RID: 2973
		private static bool? _isPointerStackEnabled;

		// Token: 0x04000B9E RID: 2974
		[ThreadStatic]
		private static SecurityCriticalDataClass<StylusLogic> _currentStylusLogic;

		// Token: 0x02000812 RID: 2066
		internal enum FlickAction
		{
			// Token: 0x04002756 RID: 10070
			GenericKey,
			// Token: 0x04002757 RID: 10071
			Scroll,
			// Token: 0x04002758 RID: 10072
			AppCommand,
			// Token: 0x04002759 RID: 10073
			CustomKey,
			// Token: 0x0400275A RID: 10074
			KeyModifier
		}

		// Token: 0x02000813 RID: 2067
		protected class StylusLogicShutDownListener : ShutDownListener
		{
			// Token: 0x0600560F RID: 22031 RVA: 0x00161F00 File Offset: 0x00161300
			[SecuritySafeCritical]
			public StylusLogicShutDownListener(StylusLogic target, ShutDownEvents events) : base(target, events)
			{
			}

			// Token: 0x06005610 RID: 22032 RVA: 0x00161F18 File Offset: 0x00161318
			internal override void OnShutDown(object target, object sender, EventArgs e)
			{
				StylusLogic stylusLogic = (StylusLogic)target;
				StylusTraceLogger.LogStatistics(stylusLogic.Statistics);
				StylusTraceLogger.LogShutdown();
			}
		}
	}
}
