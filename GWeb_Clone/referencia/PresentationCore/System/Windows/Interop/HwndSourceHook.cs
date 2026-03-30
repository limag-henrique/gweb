using System;

namespace System.Windows.Interop
{
	/// <summary>Representa o método que manipula as mensagens da janela Win32.</summary>
	/// <param name="hwnd">O identificador da janela.</param>
	/// <param name="msg">A ID da mensagem.</param>
	/// <param name="wParam">O valor de wParam da mensagem.</param>
	/// <param name="lParam">O valor de lParam da mensagem.</param>
	/// <param name="handled">Um valor que indica se a mensagem foi tratada. Defina o valor para <see langword="true" /> se a mensagem tiver sido tratada; caso contrário, <see langword="false" />.</param>
	/// <returns>O valor retornado apropriado depende da mensagem específica. Consulte os detalhes da documentação da mensagem para ver a mensagem Win32 sendo tratada.</returns>
	// Token: 0x0200032C RID: 812
	// (Invoke) Token: 0x06001BBC RID: 7100
	public delegate IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);
}
