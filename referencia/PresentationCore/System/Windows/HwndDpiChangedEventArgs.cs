using System;
using System.ComponentModel;
using System.Security;
using MS.Win32;

namespace System.Windows
{
	/// <summary>Representa um tipo de HandledEventArgs que é relevante para um evento DpiChanged.</summary>
	// Token: 0x0200019D RID: 413
	public sealed class HwndDpiChangedEventArgs : HandledEventArgs
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x0001C7A8 File Offset: 0x0001BBA8
		[SecurityCritical]
		[Obsolete]
		internal HwndDpiChangedEventArgs(double oldDpiX, double oldDpiY, double newDpiX, double newDpiY, IntPtr lParam) : base(false)
		{
			this.OldDpi = new DpiScale(oldDpiX / 96.0, oldDpiY / 96.0);
			this.NewDpi = new DpiScale(newDpiX / 96.0, newDpiY / 96.0);
			NativeMethods.RECT rect = (NativeMethods.RECT)UnsafeNativeMethods.PtrToStructure(lParam, typeof(NativeMethods.RECT));
			this.SuggestedRect = new Rect((double)rect.left, (double)rect.top, (double)rect.Width, (double)rect.Height);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001C840 File Offset: 0x0001BC40
		internal HwndDpiChangedEventArgs(DpiScale oldDpi, DpiScale newDpi, Rect suggestedRect) : base(false)
		{
			this.OldDpi = oldDpi;
			this.NewDpi = newDpi;
			this.SuggestedRect = suggestedRect;
		}

		/// <summary>Obtém as informações de escala de DPI após uma alteração de DPI.</summary>
		/// <returns>Informações sobre a escala de DPI anterior.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001C86C File Offset: 0x0001BC6C
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001C880 File Offset: 0x0001BC80
		public DpiScale OldDpi { get; private set; }

		/// <summary>Obtém as informações de escala após uma alteração de DPI.</summary>
		/// <returns>As novas informações de escala DPI.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001C894 File Offset: 0x0001BC94
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0001C8A8 File Offset: 0x0001BCA8
		public DpiScale NewDpi { get; private set; }

		/// <summary>Fornece o tamanho e a posição da janela sugerida, dimensionada para o novo DPI.</summary>
		/// <returns>Um retângulo que representa o tamanho e posição da janela.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001C8BC File Offset: 0x0001BCBC
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001C8D0 File Offset: 0x0001BCD0
		public Rect SuggestedRect { get; private set; }
	}
}
