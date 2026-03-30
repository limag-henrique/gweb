using System;

namespace System.Windows.Media
{
	/// <summary>Fornece dados de exceção de erro para eventos de mídia.</summary>
	// Token: 0x02000423 RID: 1059
	public sealed class ExceptionEventArgs : EventArgs
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x000ABCC4 File Offset: 0x000AB0C4
		internal ExceptionEventArgs(Exception errorException)
		{
			if (errorException == null)
			{
				throw new ArgumentNullException("errorException");
			}
			this._errorException = errorException;
		}

		/// <summary>Obtém a exceção que detalha a causa da falha.</summary>
		/// <returns>A exceção que fornece detalhes sobre a condição de erro.</returns>
		/// <exception cref="T:System.Security.SecurityException">A tentativa de acessar o arquivo de mídia ou imagem foi negada.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">O arquivo de mídia ou imagem não foi encontrado.</exception>
		/// <exception cref="T:System.IO.FileFormatException">Não há suporte para o formato de mídia ou imagem em um dos codecs instalados.  
		///
		/// ou - 
		/// O formato do arquivo não foi reconhecido.</exception>
		/// <exception cref="T:System.Windows.Media.InvalidWmpVersionException">Não há suporte para a versão detectada de Player de Mídia do Microsoft Windows.</exception>
		/// <exception cref="T:System.NotSupportedException">Não há suporte para a operação.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">Um código de erro COM é exibido.</exception>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000ABCEC File Offset: 0x000AB0EC
		public Exception ErrorException
		{
			get
			{
				return this._errorException;
			}
		}

		// Token: 0x040013A6 RID: 5030
		private Exception _errorException;
	}
}
