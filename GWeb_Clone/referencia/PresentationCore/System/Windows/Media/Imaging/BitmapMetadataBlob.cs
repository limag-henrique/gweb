using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece um espaço reservado para itens de metadados que não podem ser convertidos de C# para um tipo de dados subjacente que mantém metadados. O blob é convertido em uma matriz de bytes para preservar o conteúdo.</summary>
	// Token: 0x020005DE RID: 1502
	public class BitmapMetadataBlob
	{
		/// <summary>Inicializa uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadataBlob" /> e converte os metadados que ela mantém em uma matriz de bytes para manter seu conteúdo.</summary>
		/// <param name="blob">Metadados de espaço reservado.</param>
		// Token: 0x06004440 RID: 17472 RVA: 0x00109E18 File Offset: 0x00109218
		public BitmapMetadataBlob(byte[] blob)
		{
			this._blob = blob;
		}

		/// <summary>Retorna uma matriz de bytes que representa o valor de um <see cref="T:System.Windows.Media.Imaging.BitmapMetadataBlob" />.</summary>
		/// <returns>Uma matriz de bytes.</returns>
		// Token: 0x06004441 RID: 17473 RVA: 0x00109E34 File Offset: 0x00109234
		public byte[] GetBlobValue()
		{
			return (byte[])this._blob.Clone();
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x00109E54 File Offset: 0x00109254
		internal byte[] InternalGetBlobValue()
		{
			return this._blob;
		}

		// Token: 0x040018DE RID: 6366
		private byte[] _blob;
	}
}
