using System;

namespace System.Windows
{
	/// <summary>Essa classe passa as informações necessárias para qualquer ouvinte do evento <see cref="F:System.Windows.Window.DpiChangedEvent" />, como quando uma janela é movida para um monitor com DPI diferente ou o DPI do monitor atual é alterado.</summary>
	// Token: 0x020001A1 RID: 417
	public sealed class DpiChangedEventArgs : RoutedEventArgs
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0001C998 File Offset: 0x0001BD98
		internal DpiChangedEventArgs(DpiScale oldDpi, DpiScale newDpi, RoutedEvent routedEvent, object source) : base(routedEvent, source)
		{
			this.OldDpi = oldDpi;
			this.NewDpi = newDpi;
		}

		/// <summary>Obtém as informações de escala de DPI antes de uma alteração de DPI.</summary>
		/// <returns>Informações sobre a escala de DPI anterior.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001C9BC File Offset: 0x0001BDBC
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001C9D0 File Offset: 0x0001BDD0
		public DpiScale OldDpi { get; private set; }

		/// <summary>Obtém as informações de escala após uma alteração de DPI.</summary>
		/// <returns>As novas informações de escala DPI.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001C9E4 File Offset: 0x0001BDE4
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001C9F8 File Offset: 0x0001BDF8
		public DpiScale NewDpi { get; private set; }
	}
}
