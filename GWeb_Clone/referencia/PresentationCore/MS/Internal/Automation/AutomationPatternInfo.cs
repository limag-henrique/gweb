using System;
using System.Windows.Automation;

namespace MS.Internal.Automation
{
	// Token: 0x02000788 RID: 1928
	internal class AutomationPatternInfo
	{
		// Token: 0x060050F6 RID: 20726 RVA: 0x00144390 File Offset: 0x00143790
		internal AutomationPatternInfo(AutomationPattern id, WrapObject wcpWrapper)
		{
			this._id = id;
			this._wcpWrapper = wcpWrapper;
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x001443B4 File Offset: 0x001437B4
		internal AutomationPattern ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x001443C8 File Offset: 0x001437C8
		internal WrapObject WcpWrapper
		{
			get
			{
				return this._wcpWrapper;
			}
		}

		// Token: 0x040024E4 RID: 9444
		private AutomationPattern _id;

		// Token: 0x040024E5 RID: 9445
		private WrapObject _wcpWrapper;
	}
}
