using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MS.Internal
{
	// Token: 0x02000687 RID: 1671
	internal static class CoreAppContextSwitches
	{
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x0011EC50 File Offset: 0x0011E050
		public static bool DoNotScaleForDpiChanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.DoNotScaleForDpiChanges", ref CoreAppContextSwitches._doNotScaleForDpiChanges);
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x0011EC6C File Offset: 0x0011E06C
		public static bool DisableStylusAndTouchSupport
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport", ref CoreAppContextSwitches._disableStylusAndTouchSupport);
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x0011EC88 File Offset: 0x0011E088
		public static bool EnablePointerSupport
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Input.Stylus.EnablePointerSupport", ref CoreAppContextSwitches._enablePointerSupport);
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x0011ECA4 File Offset: 0x0011E0A4
		public static bool OverrideExceptionWithNullReferenceException
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.ImageSourceConverter.OverrideExceptionWithNullReferenceException", ref CoreAppContextSwitches._overrideExceptionWithNullReferenceException);
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0011ECC0 File Offset: 0x0011E0C0
		public static bool DisableDiagnostics
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Diagnostics.DisableDiagnostics", ref CoreAppContextSwitches._disableDiagnostics);
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x0011ECDC File Offset: 0x0011E0DC
		public static bool AllowChangesDuringVisualTreeChanged
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Diagnostics.AllowChangesDuringVisualTreeChanged", ref CoreAppContextSwitches._allowChangesDuringVisualTreeChanged);
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x0011ECF8 File Offset: 0x0011E0F8
		public static bool DisableImplicitTouchKeyboardInvocation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Input.Stylus.DisableImplicitTouchKeyboardInvocation", ref CoreAppContextSwitches._disableImplicitTouchKeyboardInvocation);
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x0011ED14 File Offset: 0x0011E114
		public static bool UseNetFx47CompatibleAccessibilityFeatures
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AccessibilitySwitches.UseNetFx47CompatibleAccessibilityFeatures;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x0011ED28 File Offset: 0x0011E128
		public static bool UseNetFx471CompatibleAccessibilityFeatures
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AccessibilitySwitches.UseNetFx471CompatibleAccessibilityFeatures;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x0011ED3C File Offset: 0x0011E13C
		public static bool UseNetFx472CompatibleAccessibilityFeatures
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x0011ED50 File Offset: 0x0011E150
		public static bool ShouldRenderEvenWhenNoDisplayDevicesAreAvailable
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable", ref CoreAppContextSwitches._shouldRenderEvenWhenNoDisplayDevicesAreAvailable);
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x0011ED6C File Offset: 0x0011E16C
		public static bool ShouldNotRenderInNonInteractiveWindowStation
		{
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.ShouldNotRenderInNonInteractiveWindowStation", ref CoreAppContextSwitches._shouldNotRenderInNonInteractiveWindowStation);
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x0011ED88 File Offset: 0x0011E188
		public static bool DoNotUsePresentationDpiCapabilityTier2OrGreater
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.DoNotUsePresentationDpiCapabilityTier2OrGreater", ref CoreAppContextSwitches._doNotUsePresentationDpiCapabilityTier2OrGreater);
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x0011EDA4 File Offset: 0x0011E1A4
		public static bool DoNotUsePresentationDpiCapabilityTier3OrGreater
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.DoNotUsePresentationDpiCapabilityTier3OrGreater", ref CoreAppContextSwitches._doNotUsePresentationDpiCapabilityTier3OrGreater);
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x0011EDC0 File Offset: 0x0011E1C0
		public static bool AllowExternalProcessToBlockAccessToTemporaryFiles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.AllowExternalProcessToBlockAccessToTemporaryFiles", ref CoreAppContextSwitches._allowExternalProcessToBlockAccessToTemporaryFiles);
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x0011EDDC File Offset: 0x0011E1DC
		public static bool EnableLegacyDangerousClipboardDeserializationMode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.EnableLegacyDangerousClipboardDeserializationMode", ref CoreAppContextSwitches._enableLegacyDangerousClipboardDeserializationMode);
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x0011EDF8 File Offset: 0x0011E1F8
		public static bool HostVisualDisconnectsOnWrongThread
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.HostVisual.DisconnectsOnWrongThread", ref CoreAppContextSwitches._hostVisualDisconnectsOnWrongThread);
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x0011EE14 File Offset: 0x0011E214
		public static bool OptOutOfMoveToChromedWindowFix
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Interop.MouseInput.OptOutOfMoveToChromedWindowFix", ref CoreAppContextSwitches._OptOutOfMoveToChromedWindowFix);
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x0011EE30 File Offset: 0x0011E230
		public static bool DoNotOptOutOfMoveToChromedWindowFix
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Interop.MouseInput.DoNotOptOutOfMoveToChromedWindowFix", ref CoreAppContextSwitches._DoNotOptOutOfMoveToChromedWindowFix);
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x0011EE4C File Offset: 0x0011E24C
		public static bool DisableDirtyRectangles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (CoreAppContextSwitches.EnableDynamicDirtyRectangles)
				{
					bool result;
					AppContext.TryGetSwitch("Switch.System.Windows.Media.MediaContext.DisableDirtyRectangles", out result);
					return result;
				}
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.MediaContext.DisableDirtyRectangles", ref CoreAppContextSwitches._DisableDirtyRectangles);
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x0011EE80 File Offset: 0x0011E280
		public static bool EnableDynamicDirtyRectangles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Media.MediaContext.EnableDynamicDirtyRectangles", ref CoreAppContextSwitches._EnableDynamicDirtyRectangles);
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x0011EE9C File Offset: 0x0011E29C
		public static bool OptOutIgnoreWin32SetLastError
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Windows.Common.OptOutIgnoreWin32SetLastError", ref CoreAppContextSwitches._optOutIgnoreWin32SetLastError);
			}
		}

		// Token: 0x04001CE6 RID: 7398
		internal const string DoNotScaleForDpiChangesSwitchName = "Switch.System.Windows.DoNotScaleForDpiChanges";

		// Token: 0x04001CE7 RID: 7399
		private static int _doNotScaleForDpiChanges;

		// Token: 0x04001CE8 RID: 7400
		internal const string DisableStylusAndTouchSupportSwitchName = "Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport";

		// Token: 0x04001CE9 RID: 7401
		private static int _disableStylusAndTouchSupport;

		// Token: 0x04001CEA RID: 7402
		internal const string EnablePointerSupportSwitchName = "Switch.System.Windows.Input.Stylus.EnablePointerSupport";

		// Token: 0x04001CEB RID: 7403
		private static int _enablePointerSupport;

		// Token: 0x04001CEC RID: 7404
		internal const string OverrideExceptionWithNullReferenceExceptionName = "Switch.System.Windows.Media.ImageSourceConverter.OverrideExceptionWithNullReferenceException";

		// Token: 0x04001CED RID: 7405
		private static int _overrideExceptionWithNullReferenceException;

		// Token: 0x04001CEE RID: 7406
		internal const string DisableDiagnosticsSwitchName = "Switch.System.Windows.Diagnostics.DisableDiagnostics";

		// Token: 0x04001CEF RID: 7407
		private static int _disableDiagnostics;

		// Token: 0x04001CF0 RID: 7408
		internal const string AllowChangesDuringVisualTreeChangedSwitchName = "Switch.System.Windows.Diagnostics.AllowChangesDuringVisualTreeChanged";

		// Token: 0x04001CF1 RID: 7409
		private static int _allowChangesDuringVisualTreeChanged;

		// Token: 0x04001CF2 RID: 7410
		internal const string DisableImplicitTouchKeyboardInvocationSwitchName = "Switch.System.Windows.Input.Stylus.DisableImplicitTouchKeyboardInvocation";

		// Token: 0x04001CF3 RID: 7411
		private static int _disableImplicitTouchKeyboardInvocation;

		// Token: 0x04001CF4 RID: 7412
		internal const string ShouldRenderEvenWhenNoDisplayDevicesAreAvailableSwitchName = "Switch.System.Windows.Media.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable";

		// Token: 0x04001CF5 RID: 7413
		private static int _shouldRenderEvenWhenNoDisplayDevicesAreAvailable;

		// Token: 0x04001CF6 RID: 7414
		internal const string ShouldNotRenderInNonInteractiveWindowStationSwitchName = "Switch.System.Windows.Media.ShouldNotRenderInNonInteractiveWindowStation";

		// Token: 0x04001CF7 RID: 7415
		private static int _shouldNotRenderInNonInteractiveWindowStation;

		// Token: 0x04001CF8 RID: 7416
		internal const string DoNotUsePresentationDpiCapabilityTier2OrGreaterSwitchName = "Switch.System.Windows.DoNotUsePresentationDpiCapabilityTier2OrGreater";

		// Token: 0x04001CF9 RID: 7417
		private static int _doNotUsePresentationDpiCapabilityTier2OrGreater;

		// Token: 0x04001CFA RID: 7418
		internal const string DoNotUsePresentationDpiCapabilityTier3OrGreaterSwitchName = "Switch.System.Windows.DoNotUsePresentationDpiCapabilityTier3OrGreater";

		// Token: 0x04001CFB RID: 7419
		private static int _doNotUsePresentationDpiCapabilityTier3OrGreater;

		// Token: 0x04001CFC RID: 7420
		internal const string AllowExternalProcessToBlockAccessToTemporaryFilesSwitchName = "Switch.System.Windows.AllowExternalProcessToBlockAccessToTemporaryFiles";

		// Token: 0x04001CFD RID: 7421
		private static int _allowExternalProcessToBlockAccessToTemporaryFiles;

		// Token: 0x04001CFE RID: 7422
		internal const string EnableLegacyDangerousClipboardDeserializationModeSwitchName = "Switch.System.Windows.EnableLegacyDangerousClipboardDeserializationMode";

		// Token: 0x04001CFF RID: 7423
		private static int _enableLegacyDangerousClipboardDeserializationMode;

		// Token: 0x04001D00 RID: 7424
		internal const string HostVisualDisconnectsOnWrongThreadSwitchName = "Switch.System.Windows.Media.HostVisual.DisconnectsOnWrongThread";

		// Token: 0x04001D01 RID: 7425
		private static int _hostVisualDisconnectsOnWrongThread;

		// Token: 0x04001D02 RID: 7426
		internal const string OptOutOfMoveToChromedWindowFixSwitchName = "Switch.System.Windows.Interop.MouseInput.OptOutOfMoveToChromedWindowFix";

		// Token: 0x04001D03 RID: 7427
		private static int _OptOutOfMoveToChromedWindowFix;

		// Token: 0x04001D04 RID: 7428
		internal const string DoNotOptOutOfMoveToChromedWindowFixSwitchName = "Switch.System.Windows.Interop.MouseInput.DoNotOptOutOfMoveToChromedWindowFix";

		// Token: 0x04001D05 RID: 7429
		private static int _DoNotOptOutOfMoveToChromedWindowFix;

		// Token: 0x04001D06 RID: 7430
		internal const string DisableDirtyRectanglesSwitchName = "Switch.System.Windows.Media.MediaContext.DisableDirtyRectangles";

		// Token: 0x04001D07 RID: 7431
		private static int _DisableDirtyRectangles;

		// Token: 0x04001D08 RID: 7432
		internal const string EnableDynamicDirtyRectanglesSwitchName = "Switch.System.Windows.Media.MediaContext.EnableDynamicDirtyRectangles";

		// Token: 0x04001D09 RID: 7433
		private static int _EnableDynamicDirtyRectangles;

		// Token: 0x04001D0A RID: 7434
		internal const string OptOutIgnoreWin32SetLastErrorSwitchName = "Switch.System.Windows.Common.OptOutIgnoreWin32SetLastError";

		// Token: 0x04001D0B RID: 7435
		private static int _optOutIgnoreWin32SetLastError;
	}
}
