using System;

namespace System.Windows.Media.Media3D
{
	/// <summary>Classe base da qual todas as transformações 3D afins concretas – translações, rotações e transformações de escala – derivam.</summary>
	// Token: 0x02000451 RID: 1105
	public abstract class AffineTransform3D : Transform3D
	{
		// Token: 0x06002DCF RID: 11727 RVA: 0x000B76E4 File Offset: 0x000B6AE4
		internal AffineTransform3D()
		{
		}

		/// <summary>Obtém um valor que indica se a transformação é afim.</summary>
		/// <returns>True se a transformação é afim, false caso contrário.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B76F8 File Offset: 0x000B6AF8
		public override bool IsAffine
		{
			get
			{
				base.ReadPreamble();
				return true;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.AffineTransform3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B770C File Offset: 0x000B6B0C
		public new AffineTransform3D Clone()
		{
			return (AffineTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.AffineTransform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B7724 File Offset: 0x000B6B24
		public new AffineTransform3D CloneCurrentValue()
		{
			return (AffineTransform3D)base.CloneCurrentValue();
		}
	}
}
