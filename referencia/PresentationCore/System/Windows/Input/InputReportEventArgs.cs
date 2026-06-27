using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x0200025D RID: 605
	[FriendAccessAllowed]
	internal class InputReportEventArgs : InputEventArgs
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x00040500 File Offset: 0x0003F900
		public InputReportEventArgs(InputDevice inputDevice, InputReport report) : base(inputDevice, (report != null) ? report.Timestamp : -1)
		{
			if (report == null)
			{
				throw new ArgumentNullException("report");
			}
			this._report = report;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00040538 File Offset: 0x0003F938
		public InputReport Report
		{
			get
			{
				return this._report;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0004054C File Offset: 0x0003F94C
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			InputReportEventHandler inputReportEventHandler = (InputReportEventHandler)genericHandler;
			inputReportEventHandler(genericTarget, this);
		}

		// Token: 0x04000939 RID: 2361
		private InputReport _report;
	}
}
