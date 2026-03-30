using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma <see cref="T:System.Windows.Media.GeneralTransform" /> que é uma composição das transformações em sua <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
	// Token: 0x0200039C RID: 924
	[ContentProperty("Children")]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class GeneralTransformGroup : GeneralTransform
	{
		/// <summary>Tenta transformar o ponto especificado.</summary>
		/// <param name="inPoint">O ponto a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022D1 RID: 8913 RVA: 0x0008CC10 File Offset: 0x0008C010
		public override bool TryTransform(Point inPoint, out Point result)
		{
			result = inPoint;
			if (this.Children == null || this.Children.Count == 0)
			{
				return false;
			}
			bool result2 = true;
			for (int i = 0; i < this.Children.Count; i++)
			{
				if (!this.Children.Internal_GetItem(i).TryTransform(inPoint, out result))
				{
					result2 = false;
				}
				inPoint = result;
			}
			return result2;
		}

		/// <summary>Transforma a caixa delimitadora especificada na menor caixa delimitadora alinhada por eixo possível que contenha todos os pontos na caixa delimitadora original.</summary>
		/// <param name="rect">A caixa delimitadora a ser transformada.</param>
		/// <returns>A caixa delimitadora transformada, que é a menor caixa delimitadora alinhada por eixo possível que contém todos os pontos na caixa delimitadora original.</returns>
		// Token: 0x060022D2 RID: 8914 RVA: 0x0008CC78 File Offset: 0x0008C078
		public override Rect TransformBounds(Rect rect)
		{
			if (this.Children == null || this.Children.Count == 0)
			{
				return rect;
			}
			Rect rect2 = rect;
			for (int i = 0; i < this.Children.Count; i++)
			{
				rect2 = this.Children.Internal_GetItem(i).TransformBounds(rect2);
			}
			return rect2;
		}

		/// <summary>Obtém a transformação inversa deste <see cref="T:System.Windows.Media.GeneralTransformGroup" />, se houver um inverso.</summary>
		/// <returns>A transformação inversa deste <see cref="T:System.Windows.Media.GeneralTransformGroup" />, se houver um inverso; caso contrário, <see langword="null" />.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0008CCC8 File Offset: 0x0008C0C8
		public override GeneralTransform Inverse
		{
			get
			{
				base.ReadPreamble();
				if (this.Children == null || this.Children.Count == 0)
				{
					return null;
				}
				GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
				for (int i = this.Children.Count - 1; i >= 0; i--)
				{
					GeneralTransform inverse = this.Children.Internal_GetItem(i).Inverse;
					if (inverse == null)
					{
						return null;
					}
					generalTransformGroup.Children.Add(inverse);
				}
				return generalTransformGroup;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0008CD34 File Offset: 0x0008C134
		internal override Transform AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				if (this.Children == null || this.Children.Count == 0)
				{
					return null;
				}
				Matrix matrix = Matrix.Identity;
				foreach (GeneralTransform generalTransform in this.Children)
				{
					Transform affineTransform = generalTransform.AffineTransform;
					if (affineTransform != null)
					{
						matrix *= affineTransform.Value;
					}
				}
				return new MatrixTransform(matrix);
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeneralTransformGroup" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060022D5 RID: 8917 RVA: 0x0008CDC8 File Offset: 0x0008C1C8
		public new GeneralTransformGroup Clone()
		{
			return (GeneralTransformGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeneralTransformGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060022D6 RID: 8918 RVA: 0x0008CDE0 File Offset: 0x0008C1E0
		public new GeneralTransformGroup CloneCurrentValue()
		{
			return (GeneralTransformGroup)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.GeneralTransformGroup" /> que constitui esse <see cref="T:System.Windows.Media.GeneralTransformGroup" />.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.GeneralTransformGroup" /> objetos que constituem esse <see cref="T:System.Windows.Media.GeneralTransformGroup" />. O valor padrão é uma coleção vazia.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0008CDF8 File Offset: 0x0008C1F8
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0008CE18 File Offset: 0x0008C218
		public GeneralTransformCollection Children
		{
			get
			{
				return (GeneralTransformCollection)base.GetValue(GeneralTransformGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(GeneralTransformGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0008CE34 File Offset: 0x0008C234
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransformGroup();
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0008CE48 File Offset: 0x0008C248
		static GeneralTransformGroup()
		{
			Type typeFromHandle = typeof(GeneralTransformGroup);
			GeneralTransformGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(GeneralTransformCollection), typeFromHandle, new FreezableDefaultValueFactory(GeneralTransformCollection.Empty), null, null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeneralTransformGroup.Children" />.</summary>
		// Token: 0x04001118 RID: 4376
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x04001119 RID: 4377
		internal static GeneralTransformCollection s_Children = GeneralTransformCollection.Empty;
	}
}
