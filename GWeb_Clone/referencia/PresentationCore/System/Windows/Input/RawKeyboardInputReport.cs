using System;
using System.ComponentModel;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	// Token: 0x02000293 RID: 659
	internal class RawKeyboardInputReport : InputReport
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x000481BC File Offset: 0x000475BC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public RawKeyboardInputReport(PresentationSource inputSource, InputMode mode, int timestamp, RawKeyboardActions actions, int scanCode, bool isExtendedKey, bool isSystemKey, int virtualKey, IntPtr extraInformation) : base(inputSource, InputType.Keyboard, mode, timestamp)
		{
			if (!RawKeyboardInputReport.IsValidRawKeyboardActions(actions))
			{
				throw new InvalidEnumArgumentException("actions", (int)actions, typeof(RawKeyboardActions));
			}
			this._actions = actions;
			this._scanCode = scanCode;
			this._isExtendedKey = isExtendedKey;
			this._isSystemKey = isSystemKey;
			this._virtualKey = virtualKey;
			this._extraInformation = new SecurityCriticalData<IntPtr>(extraInformation);
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00048228 File Offset: 0x00047628
		public RawKeyboardActions Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0004823C File Offset: 0x0004763C
		public int ScanCode
		{
			get
			{
				return this._scanCode;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00048250 File Offset: 0x00047650
		public bool IsExtendedKey
		{
			get
			{
				return this._isExtendedKey;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00048264 File Offset: 0x00047664
		public bool IsSystemKey
		{
			get
			{
				return this._isSystemKey;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00048278 File Offset: 0x00047678
		public int VirtualKey
		{
			get
			{
				return this._virtualKey;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004828C File Offset: 0x0004768C
		public IntPtr ExtraInformation
		{
			[SecurityCritical]
			get
			{
				return this._extraInformation.Value;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000482A4 File Offset: 0x000476A4
		internal static bool IsValidRawKeyboardActions(RawKeyboardActions actions)
		{
			return ((RawKeyboardActions.AttributesChanged | RawKeyboardActions.Activate | RawKeyboardActions.Deactivate | RawKeyboardActions.KeyDown | RawKeyboardActions.KeyUp) & actions) == actions && ((RawKeyboardActions.KeyDown | RawKeyboardActions.KeyUp) & actions) != (RawKeyboardActions.KeyDown | RawKeyboardActions.KeyUp) && ((RawKeyboardActions.Deactivate & actions) != actions || RawKeyboardActions.Deactivate == actions);
		}

		// Token: 0x04000A73 RID: 2675
		private RawKeyboardActions _actions;

		// Token: 0x04000A74 RID: 2676
		private int _scanCode;

		// Token: 0x04000A75 RID: 2677
		private bool _isExtendedKey;

		// Token: 0x04000A76 RID: 2678
		private bool _isSystemKey;

		// Token: 0x04000A77 RID: 2679
		private int _virtualKey;

		// Token: 0x04000A78 RID: 2680
		private SecurityCriticalData<IntPtr> _extraInformation;
	}
}
