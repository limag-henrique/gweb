using System;
using System.Security.Permissions;

namespace System.Windows.Interop
{
	/// <summary>Define o contrato para identificadores de janela Win32.</summary>
	// Token: 0x02000331 RID: 817
	public interface IWin32Window
	{
		/// <summary>Obtém o identificador da janela.</summary>
		/// <returns>O identificador da janela.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001BD9 RID: 7129
		IntPtr Handle { [UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)] get; }
	}
}
