using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Especifica se uma imagem no formato PNG (Portable Network Graphics) é entrelaçada durante a codificação.</summary>
	// Token: 0x020005F5 RID: 1525
	public enum PngInterlaceOption
	{
		/// <summary>O <see cref="T:System.Windows.Media.Imaging.PngBitmapEncoder" /> determina se a imagem deve ser entrelaçada.</summary>
		// Token: 0x04001955 RID: 6485
		Default,
		/// <summary>A imagem de bitmap resultante é entrelaçada.</summary>
		// Token: 0x04001956 RID: 6486
		On,
		/// <summary>A imagem de bitmap resultante não é entrelaçada.</summary>
		// Token: 0x04001957 RID: 6487
		Off
	}
}
