using System;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Automation.Peers
{
	/// <summary>Expõe tipos <see cref="T:System.Windows.Interop.HwndHost" /> à Automação de Interface do Usuário.</summary>
	// Token: 0x02000313 RID: 787
	public sealed class HostedWindowWrapper
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Automation.Peers.HostedWindowWrapper" />.</summary>
		/// <param name="hwnd">O ponteiro para o <see cref="T:System.Windows.Interop.HwndHost" /> que é associado a esse <see cref="T:System.Windows.Automation.Peers.HostedWindowWrapper" />.</param>
		// Token: 0x06001903 RID: 6403 RVA: 0x00063724 File Offset: 0x00062B24
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public HostedWindowWrapper(IntPtr hwnd)
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			this._hwnd = hwnd;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006374C File Offset: 0x00062B4C
		[SecurityCritical]
		private HostedWindowWrapper()
		{
			this._hwnd = IntPtr.Zero;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006376C File Offset: 0x00062B6C
		[SecurityCritical]
		internal static HostedWindowWrapper CreateInternal(IntPtr hwnd)
		{
			return new HostedWindowWrapper
			{
				_hwnd = hwnd
			};
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00063788 File Offset: 0x00062B88
		internal IntPtr Handle
		{
			[SecurityCritical]
			get
			{
				return this._hwnd;
			}
		}

		// Token: 0x04000DDD RID: 3549
		[SecurityCritical]
		private IntPtr _hwnd;
	}
}
