using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using MS.Internal;

namespace System.Windows.Interop
{
	/// <summary>Fornece uma classe auxiliar estática para interoperação do WPF/Win32 com um método, que é usado para obter um objeto <see cref="T:System.Windows.Input.Cursor" /> do WPF (Windows Presentation Foundation) com base em um identificador de cursor do Win32 fornecido.</summary>
	// Token: 0x02000320 RID: 800
	public static class CursorInteropHelper
	{
		/// <summary>Retorna um objeto Windows Presentation Foundation (WPF) <see cref="T:System.Windows.Input.Cursor" /> baseado em um identificador de cursor Win32 fornecido.</summary>
		/// <param name="cursorHandle">Referência de cursor a ser usada para a interoperação.</param>
		/// <returns>O objeto de cursor Windows Presentation Foundation (WPF) baseado no identificador de cursor Win32 fornecido.</returns>
		// Token: 0x06001A82 RID: 6786 RVA: 0x000681BC File Offset: 0x000675BC
		[SecurityCritical]
		public static Cursor Create(SafeHandle cursorHandle)
		{
			SecurityHelper.DemandUIWindowPermission();
			return CursorInteropHelper.CriticalCreate(cursorHandle);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000681D4 File Offset: 0x000675D4
		[SecurityCritical]
		internal static Cursor CriticalCreate(SafeHandle cursorHandle)
		{
			return new Cursor(cursorHandle);
		}
	}
}
