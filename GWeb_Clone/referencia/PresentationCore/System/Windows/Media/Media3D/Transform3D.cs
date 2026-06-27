using System;
using System.Windows.Media.Composition;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece uma classe pai para todas as transformações tridimensionais, incluindo as transformações de translação, rotação e escala.</summary>
	// Token: 0x02000481 RID: 1153
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Transform3D : GeneralTransform3D, DUCE.IResource
	{
		// Token: 0x060031D1 RID: 12753 RVA: 0x000C71C8 File Offset: 0x000C65C8
		internal Transform3D()
		{
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="point">O Point3D a ser transformado.</param>
		/// <returns>O Point3D transformado.</returns>
		// Token: 0x060031D2 RID: 12754 RVA: 0x000C71DC File Offset: 0x000C65DC
		public new Point3D Transform(Point3D point)
		{
			return base.Transform(point);
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado.</summary>
		/// <param name="vector">O Vector3D a ser transformado.</param>
		/// <returns>O Vector3D transformado.</returns>
		// Token: 0x060031D3 RID: 12755 RVA: 0x000C71F0 File Offset: 0x000C65F0
		public Vector3D Transform(Vector3D vector)
		{
			return this.Value.Transform(vector);
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Point4D" /> especificado.</summary>
		/// <param name="point">O Point4D a ser transformado.</param>
		/// <returns>O Point4D transformado.</returns>
		// Token: 0x060031D4 RID: 12756 RVA: 0x000C720C File Offset: 0x000C660C
		public Point4D Transform(Point4D point)
		{
			return this.Value.Transform(point);
		}

		/// <summary>Transforma a matriz especificada de objetos <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="points">Matriz de objetos Point3D a serem transformados.</param>
		// Token: 0x060031D5 RID: 12757 RVA: 0x000C7228 File Offset: 0x000C6628
		public void Transform(Point3D[] points)
		{
			this.Value.Transform(points);
		}

		/// <summary>Transforma a matriz especificada de objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vectors">Matriz de objetos Vector3D a serem transformados.</param>
		// Token: 0x060031D6 RID: 12758 RVA: 0x000C7244 File Offset: 0x000C6644
		public void Transform(Vector3D[] vectors)
		{
			this.Value.Transform(vectors);
		}

		/// <summary>Transforma a matriz especificada de objetos <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="points">Matriz de objetos Point4D a serem transformados.</param>
		// Token: 0x060031D7 RID: 12759 RVA: 0x000C7260 File Offset: 0x000C6660
		public void Transform(Point4D[] points)
		{
			this.Value.Transform(points);
		}

		/// <summary>Tenta transformar o ponto 3-D especificado e retorna um valor que indica se a transformação foi bem-sucedida.</summary>
		/// <param name="inPoint">O ponto 3D a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060031D8 RID: 12760 RVA: 0x000C727C File Offset: 0x000C667C
		public override bool TryTransform(Point3D inPoint, out Point3D result)
		{
			result = this.Value.Transform(inPoint);
			return true;
		}

		/// <summary>Transforma a caixa delimitadora 3-D especificada e retorna uma caixa delimitadora 3-D alinhada por eixo exatamente grande o suficiente para contê-la.</summary>
		/// <param name="rect">A caixa delimitadora 3D a ser transformada.</param>
		/// <returns>A menor caixa delimitadora 3D possível alinhada por eixo que contenha o <paramref name="rect" /> transformado.</returns>
		// Token: 0x060031D9 RID: 12761 RVA: 0x000C72A0 File Offset: 0x000C66A0
		public override Rect3D TransformBounds(Rect3D rect)
		{
			return M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref rect, this);
		}

		/// <summary>Obtém a transformação inversa desse objeto, se possível.</summary>
		/// <returns>Um inverso dessa instância, se possível; Caso contrário, <see langword="null" />.</returns>
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000C72B8 File Offset: 0x000C66B8
		public override GeneralTransform3D Inverse
		{
			get
			{
				base.ReadPreamble();
				Matrix3D value = this.Value;
				if (!value.HasInverse)
				{
					return null;
				}
				value.Invert();
				return new MatrixTransform3D(value);
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000C72EC File Offset: 0x000C66EC
		internal override Transform3D AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				return this;
			}
		}

		/// <summary>Obtém a transformação de identidade.</summary>
		/// <returns>Transformação de identidade.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000C72FC File Offset: 0x000C66FC
		public static Transform3D Identity
		{
			get
			{
				if (Transform3D.s_identity == null)
				{
					MatrixTransform3D matrixTransform3D = new MatrixTransform3D();
					matrixTransform3D.Freeze();
					Transform3D.s_identity = matrixTransform3D;
				}
				return Transform3D.s_identity;
			}
		}

		/// <summary>Obtém um valor que especifica se a matriz é afim.</summary>
		/// <returns>
		///   <see langword="true" /> Se a matriz é afim; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060031DD RID: 12765
		public abstract bool IsAffine { get; }

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que representa o valor da transformação atual.</summary>
		/// <returns>Matrix3D que representa o valor da transformação atual.</returns>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060031DE RID: 12766
		public abstract Matrix3D Value { get; }

		// Token: 0x060031DF RID: 12767
		internal abstract void Append(ref Matrix3D matrix);

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Transform3D" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x060031E0 RID: 12768 RVA: 0x000C7328 File Offset: 0x000C6728
		public new Transform3D Clone()
		{
			return (Transform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Transform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x060031E1 RID: 12769 RVA: 0x000C7340 File Offset: 0x000C6740
		public new Transform3D CloneCurrentValue()
		{
			return (Transform3D)base.CloneCurrentValue();
		}

		// Token: 0x060031E2 RID: 12770
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x060031E3 RID: 12771 RVA: 0x000C7358 File Offset: 0x000C6758
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x060031E4 RID: 12772
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x060031E5 RID: 12773 RVA: 0x000C73A0 File Offset: 0x000C67A0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x060031E6 RID: 12774
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x060031E7 RID: 12775 RVA: 0x000C73E8 File Offset: 0x000C67E8
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x060031E8 RID: 12776
		internal abstract int GetChannelCountCore();

		// Token: 0x060031E9 RID: 12777 RVA: 0x000C7430 File Offset: 0x000C6830
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x060031EA RID: 12778
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x060031EB RID: 12779 RVA: 0x000C7444 File Offset: 0x000C6844
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		// Token: 0x040015B8 RID: 5560
		private static Transform3D s_identity;
	}
}
