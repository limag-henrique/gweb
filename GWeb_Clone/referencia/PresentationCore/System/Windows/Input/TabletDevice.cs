using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Input
{
	/// <summary>Representa o dispositivo digitalizador de um Tablet.</summary>
	// Token: 0x020002C9 RID: 713
	public sealed class TabletDevice : InputDevice
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0004F5A8 File Offset: 0x0004E9A8
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0004F5BC File Offset: 0x0004E9BC
		internal TabletDeviceBase TabletDeviceImpl { get; set; }

		// Token: 0x0600155E RID: 5470 RVA: 0x0004F5D0 File Offset: 0x0004E9D0
		internal TabletDevice(TabletDeviceBase impl)
		{
			if (impl == null)
			{
				throw new ArgumentNullException("impl");
			}
			this.TabletDeviceImpl = impl;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.IInputElement" /> que fornece processamento de entrada básico para o dispositivo de tablet.</summary>
		/// <returns>O <see cref="T:System.Windows.IInputElement" /> que fornece processamento de entrada básico para o dispositivo de tablet.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0004F5F8 File Offset: 0x0004E9F8
		public override IInputElement Target
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.Target;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.PresentationSource" /> que relata a entrada atual para o dispositivo de tablet.</summary>
		/// <returns>O <see cref="T:System.Windows.PresentationSource" /> que relata a entrada atual para o dispositivo de tablet.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0004F618 File Offset: 0x0004EA18
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.ActiveSource;
			}
		}

		/// <summary>Obtém o identificador exclusivo do dispositivo de tablet no sistema.</summary>
		/// <returns>O identificador exclusivo para o dispositivo de tablet no sistema.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0004F638 File Offset: 0x0004EA38
		public int Id
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.Id;
			}
		}

		/// <summary>Obtém o nome do dispositivo de tablet.</summary>
		/// <returns>O nome do dispositivo de tablet.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0004F658 File Offset: 0x0004EA58
		public string Name
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.Name;
			}
		}

		/// <summary>Obtém o identificador de produto do dispositivo de tablet.</summary>
		/// <returns>O identificador de produto para o dispositivo de tablet.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0004F678 File Offset: 0x0004EA78
		public string ProductId
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.ProductId;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.TabletHardwareCapabilities" /> do dispositivo de tablet.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.TabletHardwareCapabilities" /> para o dispositivo de tablet.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0004F698 File Offset: 0x0004EA98
		public TabletHardwareCapabilities TabletHardwareCapabilities
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.TabletHardwareCapabilities;
			}
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Input.StylusPointProperty" /> compatíveis com o <see cref="T:System.Windows.Input.TabletDevice" />.</summary>
		/// <returns>Uma coleção de <see cref="T:System.Windows.Input.StylusPointProperty" /> objetos que o <see cref="T:System.Windows.Input.TabletDevice" /> dá suporte.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0004F6B8 File Offset: 0x0004EAB8
		public ReadOnlyCollection<StylusPointProperty> SupportedStylusPointProperties
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.SupportedStylusPointProperties;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.TabletDeviceType" /> do dispositivo de tablet.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.TabletDeviceType" /> do dispositivo de tablet.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0004F6D8 File Offset: 0x0004EAD8
		public TabletDeviceType Type
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.Type;
			}
		}

		/// <summary>Retorna o nome do dispositivo de tablet.</summary>
		/// <returns>Um <see cref="T:System.String" /> que contém o nome do <see cref="T:System.Windows.Input.TabletDevice" />.</returns>
		// Token: 0x06001567 RID: 5479 RVA: 0x0004F6F8 File Offset: 0x0004EAF8
		public override string ToString()
		{
			return this.TabletDeviceImpl.ToString();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusDeviceCollection" /> associado ao dispositivo de tablet.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusDeviceCollection" /> associado com o dispositivo de tablet.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0004F710 File Offset: 0x0004EB10
		public StylusDeviceCollection StylusDevices
		{
			get
			{
				base.VerifyAccess();
				return this.TabletDeviceImpl.StylusDevices;
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0004F730 File Offset: 0x0004EB30
		internal T As<T>() where T : TabletDeviceBase
		{
			return this.TabletDeviceImpl as T;
		}
	}
}
