using System;

namespace System.Windows
{
	/// <summary>Armazena informações de DPI das quais um <see cref="T:System.Windows.Media.Visual" /> ou <see cref="T:System.Windows.UIElement" /> é renderizado.</summary>
	// Token: 0x020001A4 RID: 420
	public struct DpiScale
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.DpiScale" />.</summary>
		/// <param name="dpiScaleX">A escala de DPI no eixo X.</param>
		/// <param name="dpiScaleY">A escala de DPI no eixo Y.</param>
		// Token: 0x06000639 RID: 1593 RVA: 0x0001CBBC File Offset: 0x0001BFBC
		public DpiScale(double dpiScaleX, double dpiScaleY)
		{
			this._dpiScaleX = dpiScaleX;
			this._dpiScaleY = dpiScaleY;
		}

		/// <summary>Obtém a escala de DPI no eixo X.</summary>
		/// <returns>A escala de DPI para o eixo X.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001CBD8 File Offset: 0x0001BFD8
		public double DpiScaleX
		{
			get
			{
				return this._dpiScaleX;
			}
		}

		/// <summary>Obtém a escala de DPI no eixo Y.</summary>
		/// <returns>A escala de DPI para o eixo Y.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001CBEC File Offset: 0x0001BFEC
		public double DpiScaleY
		{
			get
			{
				return this._dpiScaleY;
			}
		}

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.DpiScale.PixelsPerDip" /> atual.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001CC00 File Offset: 0x0001C000
		public double PixelsPerDip
		{
			get
			{
				return this._dpiScaleY;
			}
		}

		/// <summary>Obtém o DPI ao longo do eixo X.</summary>
		/// <returns>O DPI ao longo do eixo X.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001CC14 File Offset: 0x0001C014
		public double PixelsPerInchX
		{
			get
			{
				return 96.0 * this._dpiScaleX;
			}
		}

		/// <summary>Obtém o DPI ao longo do eixo Y.</summary>
		/// <returns>O DPI ao longo do eixo Y.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001CC34 File Offset: 0x0001C034
		public double PixelsPerInchY
		{
			get
			{
				return 96.0 * this._dpiScaleY;
			}
		}

		// Token: 0x0400057B RID: 1403
		private readonly double _dpiScaleX;

		// Token: 0x0400057C RID: 1404
		private readonly double _dpiScaleY;
	}
}
