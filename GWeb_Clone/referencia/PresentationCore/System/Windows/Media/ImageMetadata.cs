using System;

namespace System.Windows.Media
{
	/// <summary>Define uma classe base para todas as operações de metadados em APIs relacionadas a imagens. Esta é uma classe abstrata.</summary>
	// Token: 0x02000413 RID: 1043
	public abstract class ImageMetadata : Freezable
	{
		// Token: 0x06002A1C RID: 10780 RVA: 0x000A9160 File Offset: 0x000A8560
		internal ImageMetadata()
		{
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.ImageMetadata" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002A1D RID: 10781 RVA: 0x000A9174 File Offset: 0x000A8574
		public new ImageMetadata Clone()
		{
			return (ImageMetadata)base.Clone();
		}
	}
}
