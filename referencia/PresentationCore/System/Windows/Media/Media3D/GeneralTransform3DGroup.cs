using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> que é uma composição das transformações em sua <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</summary>
	// Token: 0x0200045A RID: 1114
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	[ContentProperty("Children")]
	public sealed class GeneralTransform3DGroup : GeneralTransform3D
	{
		/// <summary>Tenta transformar o ponto 3D especificado.</summary>
		/// <param name="inPoint">O ponto 3D a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002E58 RID: 11864 RVA: 0x000B8E24 File Offset: 0x000B8224
		public override bool TryTransform(Point3D inPoint, out Point3D result)
		{
			result = inPoint;
			GeneralTransform3DCollection children = this.Children;
			if (children == null || children.Count == 0)
			{
				return false;
			}
			bool result2 = true;
			int i = 0;
			int count = children.Count;
			while (i < count)
			{
				if (!children._collection[i].TryTransform(inPoint, out result))
				{
					result2 = false;
					break;
				}
				inPoint = result;
				i++;
			}
			return result2;
		}

		/// <summary>Transforma a caixa delimitadora 3D especificada na menor caixa delimitadora 3D alinhada por eixo possível que contenha todos os pontos na caixa delimitadora original.</summary>
		/// <param name="rect">A caixa delimitadora 3D a ser transformada.</param>
		/// <returns>A caixa delimitadora transformada.</returns>
		// Token: 0x06002E59 RID: 11865 RVA: 0x000B8E84 File Offset: 0x000B8284
		public override Rect3D TransformBounds(Rect3D rect)
		{
			GeneralTransform3DCollection children = this.Children;
			if (children == null || children.Count == 0)
			{
				return rect;
			}
			Rect3D rect3D = rect;
			int i = 0;
			int count = children.Count;
			while (i < count)
			{
				rect3D = children._collection[i].TransformBounds(rect3D);
				i++;
			}
			return rect3D;
		}

		/// <summary>Obtém a transformação inversa deste <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />, se houver um inverso.</summary>
		/// <returns>A transformação inversa deste <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />, se houver um inverso; caso contrário, <see langword="null" />.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000B8ED0 File Offset: 0x000B82D0
		public override GeneralTransform3D Inverse
		{
			get
			{
				GeneralTransform3DCollection children = this.Children;
				if (children == null || children.Count == 0)
				{
					return null;
				}
				GeneralTransform3DGroup generalTransform3DGroup = new GeneralTransform3DGroup();
				for (int i = children.Count - 1; i >= 0; i--)
				{
					GeneralTransform3D inverse = children._collection[i].Inverse;
					if (inverse == null)
					{
						return null;
					}
					generalTransform3DGroup.Children.Add(inverse);
				}
				return generalTransform3DGroup;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000B8F30 File Offset: 0x000B8330
		internal override Transform3D AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				GeneralTransform3DCollection children = this.Children;
				if (children == null || children.Count == 0)
				{
					return null;
				}
				Matrix3D identity = Matrix3D.Identity;
				int i = 0;
				int count = children.Count;
				while (i < count)
				{
					Transform3D affineTransform = children._collection[i].AffineTransform;
					affineTransform.Append(ref identity);
					i++;
				}
				return new MatrixTransform3D(identity);
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06002E5C RID: 11868 RVA: 0x000B8F8C File Offset: 0x000B838C
		public new GeneralTransform3DGroup Clone()
		{
			return (GeneralTransform3DGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06002E5D RID: 11869 RVA: 0x000B8FA4 File Offset: 0x000B83A4
		public new GeneralTransform3DGroup CloneCurrentValue()
		{
			return (GeneralTransform3DGroup)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" /> que constitui esse <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" /> objetos que constituem esse <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DGroup" />. O padrão é uma coleção vazia.</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000B8FBC File Offset: 0x000B83BC
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000B8FDC File Offset: 0x000B83DC
		public GeneralTransform3DCollection Children
		{
			get
			{
				return (GeneralTransform3DCollection)base.GetValue(GeneralTransform3DGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(GeneralTransform3DGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000B8FF8 File Offset: 0x000B83F8
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform3DGroup();
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000B900C File Offset: 0x000B840C
		static GeneralTransform3DGroup()
		{
			Type typeFromHandle = typeof(GeneralTransform3DGroup);
			GeneralTransform3DGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(GeneralTransform3DCollection), typeFromHandle, new FreezableDefaultValueFactory(GeneralTransform3DCollection.Empty), null, null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.GeneralTransform3DGroup.Children" />.</summary>
		// Token: 0x040014F5 RID: 5365
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x040014F6 RID: 5366
		internal static GeneralTransform3DCollection s_Children = GeneralTransform3DCollection.Empty;
	}
}
