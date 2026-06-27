using System;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Fornece acesso aos métodos estáticos que retornam os dispositivos tablet anexados ao sistema.</summary>
	// Token: 0x020002C8 RID: 712
	public static class Tablet
	{
		/// <summary>Obtém o <see cref="T:System.Windows.Input.TabletDevice" /> atual.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.TabletDevice" /> atual.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0004F560 File Offset: 0x0004E960
		public static TabletDevice CurrentTabletDevice
		{
			get
			{
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.TabletDevice;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.TabletDeviceCollection" /> associado ao sistema.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.TabletDeviceCollection" /> associado com o Tablet PC.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0004F580 File Offset: 0x0004E980
		public static TabletDeviceCollection TabletDevices
		{
			[SecurityCritical]
			get
			{
				StylusLogic currentStylusLogic = StylusLogic.CurrentStylusLogic;
				return ((currentStylusLogic != null) ? currentStylusLogic.TabletDevices : null) ?? TabletDeviceCollection.EmptyTabletDeviceCollection;
			}
		}
	}
}
