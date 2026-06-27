using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece dados para os eventos de <see cref="E:System.Windows.Media.Imaging.BitmapSource.DownloadProgress" /> e de <see cref="E:System.Windows.Media.Imaging.BitmapDecoder.DownloadProgress" /> .</summary>
	// Token: 0x020005EA RID: 1514
	public class DownloadProgressEventArgs : EventArgs
	{
		// Token: 0x06004571 RID: 17777 RVA: 0x0010EE8C File Offset: 0x0010E28C
		internal DownloadProgressEventArgs(int percentComplete)
		{
			this._percentComplete = percentComplete;
		}

		/// <summary>Obtém um valor que representa o progresso do download de um bitmap, expressado como um percentual.</summary>
		/// <returns>O progresso, expressado como uma porcentagem, para o qual um bitmap tiver sido baixado. O valor retornado será entre 1 e 100.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x0010EEA8 File Offset: 0x0010E2A8
		public int Progress
		{
			get
			{
				return this._percentComplete;
			}
		}

		// Token: 0x04001939 RID: 6457
		private int _percentComplete;
	}
}
