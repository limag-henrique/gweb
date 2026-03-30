using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Especifica os esquemas de compactação possíveis para imagens bitmap em formato TIFF.</summary>
	// Token: 0x020005FB RID: 1531
	public enum TiffCompressOption
	{
		/// <summary>O codificador <see cref="T:System.Windows.Media.Imaging.TiffBitmapEncoder" /> tenta salvar o bitmap com o melhor esquema de compactação possível.</summary>
		// Token: 0x04001974 RID: 6516
		Default,
		/// <summary>A imagem TIFF (formato TIFF) não foi compactada.</summary>
		// Token: 0x04001975 RID: 6517
		None,
		/// <summary>O esquema de compactação CCITT3 é usado.</summary>
		// Token: 0x04001976 RID: 6518
		Ccitt3,
		/// <summary>O esquema de compactação CCITT4 é usado.</summary>
		// Token: 0x04001977 RID: 6519
		Ccitt4,
		/// <summary>O esquema de compactação LZW é usado.</summary>
		// Token: 0x04001978 RID: 6520
		Lzw,
		/// <summary>O esquema de compactação RLE é usado.</summary>
		// Token: 0x04001979 RID: 6521
		Rle,
		/// <summary>O esquema de compactação ZIP é usado.</summary>
		// Token: 0x0400197A RID: 6522
		Zip
	}
}
