using System;
using System.Windows;
using System.Windows.Automation;

namespace MS.Internal.Automation
{
	// Token: 0x02000786 RID: 1926
	internal class AutomationPropertyInfo
	{
		// Token: 0x060050EE RID: 20718 RVA: 0x0014432C File Offset: 0x0014372C
		internal AutomationPropertyInfo(AutomationProperty id, DependencyProperty dependencyProperty, DependencyProperty overrideDP)
		{
			this._id = id;
			this._dependencyProperty = dependencyProperty;
			this._overrideDP = overrideDP;
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060050EF RID: 20719 RVA: 0x00144354 File Offset: 0x00143754
		internal AutomationProperty ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060050F0 RID: 20720 RVA: 0x00144368 File Offset: 0x00143768
		internal DependencyProperty DependencyProperty
		{
			get
			{
				return this._dependencyProperty;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0014437C File Offset: 0x0014377C
		internal DependencyProperty OverrideDP
		{
			get
			{
				return this._overrideDP;
			}
		}

		// Token: 0x040024E1 RID: 9441
		private AutomationProperty _id;

		// Token: 0x040024E2 RID: 9442
		private DependencyProperty _dependencyProperty;

		// Token: 0x040024E3 RID: 9443
		private DependencyProperty _overrideDP;
	}
}
