using System;
using System.ComponentModel;
using System.Security;

namespace System.Windows.Input
{
	// Token: 0x02000296 RID: 662
	internal class RawUIStateInputReport : InputReport
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x00048358 File Offset: 0x00047758
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public RawUIStateInputReport(PresentationSource inputSource, InputMode mode, int timestamp, RawUIStateActions action, RawUIStateTargets targets) : base(inputSource, InputType.Keyboard, mode, timestamp)
		{
			if (!RawUIStateInputReport.IsValidRawUIStateAction(action))
			{
				throw new InvalidEnumArgumentException("action", (int)action, typeof(RawUIStateActions));
			}
			if (!RawUIStateInputReport.IsValidRawUIStateTargets(targets))
			{
				throw new InvalidEnumArgumentException("targets", (int)targets, typeof(RawUIStateTargets));
			}
			this._action = action;
			this._targets = targets;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000483C0 File Offset: 0x000477C0
		public RawUIStateActions Action
		{
			get
			{
				return this._action;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x000483D4 File Offset: 0x000477D4
		public RawUIStateTargets Targets
		{
			get
			{
				return this._targets;
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000483E8 File Offset: 0x000477E8
		internal static bool IsValidRawUIStateAction(RawUIStateActions action)
		{
			return action == RawUIStateActions.Set || action == RawUIStateActions.Clear || action == RawUIStateActions.Initialize;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00048404 File Offset: 0x00047804
		internal static bool IsValidRawUIStateTargets(RawUIStateTargets targets)
		{
			return (targets & (RawUIStateTargets.HideFocus | RawUIStateTargets.HideAccelerators | RawUIStateTargets.Active)) == targets;
		}

		// Token: 0x04000A81 RID: 2689
		private RawUIStateActions _action;

		// Token: 0x04000A82 RID: 2690
		private RawUIStateTargets _targets;
	}
}
