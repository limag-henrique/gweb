using System;

namespace System.Windows
{
	/// <summary>Fornece dados para o evento <see cref="E:System.Windows.Interop.HwndSource.AutoResized" /> gerado por <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
	// Token: 0x0200018A RID: 394
	public class AutoResizedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.AutoResizedEventArgs" />.</summary>
		/// <param name="size">O tamanho a ser relatado nos dados do evento.</param>
		// Token: 0x060003CB RID: 971 RVA: 0x00015A40 File Offset: 0x00014E40
		public AutoResizedEventArgs(Size size)
		{
			this._size = size;
		}

		/// <summary>Obtém o novo tamanho da janela após a operação de redimensionamento automático.</summary>
		/// <returns>Tamanho da janela depois de redimensionar.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00015A5C File Offset: 0x00014E5C
		public Size Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x040004BC RID: 1212
		private Size _size;
	}
}
