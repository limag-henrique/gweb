using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Contém os objetos <see cref="T:System.Windows.Input.StylusDevice" /> que representam os dispositivos de caneta de um Tablet PC.</summary>
	// Token: 0x020002B3 RID: 691
	public class StylusDeviceCollection : ReadOnlyCollection<StylusDevice>
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x0004B8CC File Offset: 0x0004ACCC
		internal StylusDeviceCollection(IEnumerable<StylusDeviceBase> styluses) : base(new List<StylusDevice>())
		{
			foreach (StylusDeviceBase stylusDeviceBase in styluses)
			{
				base.Items.Add(stylusDeviceBase.StylusDevice);
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004B938 File Offset: 0x0004AD38
		[SecurityCritical]
		internal void Dispose()
		{
			foreach (StylusDevice stylusDevice in base.Items)
			{
				stylusDevice.StylusDeviceImpl.Dispose();
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0004B998 File Offset: 0x0004AD98
		internal void AddStylusDevice(int index, StylusDeviceBase stylusDevice)
		{
			base.Items.Insert(index, stylusDevice.StylusDevice);
		}
	}
}
