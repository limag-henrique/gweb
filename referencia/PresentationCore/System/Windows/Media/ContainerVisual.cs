using System;
using System.ComponentModel;
using System.Windows.Media.Effects;

namespace System.Windows.Media
{
	/// <summary>Gerencia uma coleção de objetos <see cref="T:System.Windows.Media.Visual" />.</summary>
	// Token: 0x02000378 RID: 888
	public class ContainerVisual : Visual
	{
		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00082624 File Offset: 0x00081A24
		public ContainerVisual()
		{
			this._children = new VisualCollection(this);
		}

		/// <summary>Obtém a coleção filho do <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.VisualCollection" /> que contém os filhos do <see cref="T:System.Windows.Media.ContainerVisual" />.</returns>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00082644 File Offset: 0x00081A44
		public VisualCollection Children
		{
			get
			{
				base.VerifyAPIReadOnly();
				return this._children;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Visual" /> pai para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O pai do visual.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00082660 File Offset: 0x00081A60
		public DependencyObject Parent
		{
			get
			{
				return base.VisualParent;
			}
		}

		/// <summary>Obtém ou define a área de recorte do <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> que define a região de recorte.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00082674 File Offset: 0x00081A74
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00082688 File Offset: 0x00081A88
		public Geometry Clip
		{
			get
			{
				return base.VisualClip;
			}
			set
			{
				base.VisualClip = value;
			}
		}

		/// <summary>Obtém ou define a opacidade do <see cref="T:System.Windows.Media.ContainerVisual" />, com base em 0 = transparente, 1 = opaco.</summary>
		/// <returns>Um valor entre 0 e 1 que especifica um intervalo de totalmente transparente a completamente opaco. Um valor de 0 indica que o <see cref="T:System.Windows.Media.ContainerVisual" /> é completamente transparente, enquanto um valor de 1 indica que o <see cref="T:System.Windows.Media.ContainerVisual" /> é completamente opaco. Um valor de 0,5 indica 50 por cento opaco, um valor de 0.725 indica 72.5% opaco e assim por diante. Valores menores que 0 são tratados como 0, enquanto valores maiores que 1 são tratados como 1.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0008269C File Offset: 0x00081A9C
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x000826B0 File Offset: 0x00081AB0
		public double Opacity
		{
			get
			{
				return base.VisualOpacity;
			}
			set
			{
				base.VisualOpacity = value;
			}
		}

		/// <summary>Obtém ou define um pincel que especifica uma máscara de opacidade possível para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.Brush" /> que representa o valor da máscara de opacidade do <see cref="T:System.Windows.Media.ContainerVisual" />.</returns>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x000826C4 File Offset: 0x00081AC4
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x000826D8 File Offset: 0x00081AD8
		public Brush OpacityMask
		{
			get
			{
				return base.VisualOpacityMask;
			}
			set
			{
				base.VisualOpacityMask = value;
			}
		}

		/// <summary>Obtém ou define uma representação armazenada em cache do <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.CacheMode" /> que contém uma representação armazenada em cache do <see cref="T:System.Windows.Media.ContainerVisual" />.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000826EC File Offset: 0x00081AEC
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00082700 File Offset: 0x00081B00
		public CacheMode CacheMode
		{
			get
			{
				return base.VisualCacheMode;
			}
			set
			{
				base.VisualCacheMode = value;
			}
		}

		/// <summary>Obtém ou define um valor <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O efeito de bitmap para este objeto visual.</returns>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00082714 File Offset: 0x00081B14
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00082728 File Offset: 0x00081B28
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffect BitmapEffect
		{
			get
			{
				return base.VisualBitmapEffect;
			}
			set
			{
				base.VisualBitmapEffect = value;
			}
		}

		/// <summary>Obtém ou define um valor <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O valor de entrada do efeito de bitmap para este objeto visual.</returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0008273C File Offset: 0x00081B3C
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00082750 File Offset: 0x00081B50
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffectInput BitmapEffectInput
		{
			get
			{
				return base.VisualBitmapEffectInput;
			}
			set
			{
				base.VisualBitmapEffectInput = value;
			}
		}

		/// <summary>Obtém ou define o efeito de bitmap a ser aplicado ao <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.Effect" /> que representa o efeito de bitmap.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00082764 File Offset: 0x00081B64
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00082778 File Offset: 0x00081B78
		public Effect Effect
		{
			get
			{
				return base.VisualEffect;
			}
			set
			{
				base.VisualEffect = value;
			}
		}

		/// <summary>Obtém ou define a orientação de X (horizontal) para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>A orientação horizontal.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0008278C File Offset: 0x00081B8C
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x000827A0 File Offset: 0x00081BA0
		[DefaultValue(null)]
		public DoubleCollection XSnappingGuidelines
		{
			get
			{
				return base.VisualXSnappingGuidelines;
			}
			set
			{
				base.VisualXSnappingGuidelines = value;
			}
		}

		/// <summary>Obtém ou define a orientação de Y (vertical) para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>A orientação vertical.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000827B4 File Offset: 0x00081BB4
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000827C8 File Offset: 0x00081BC8
		[DefaultValue(null)]
		public DoubleCollection YSnappingGuidelines
		{
			get
			{
				return base.VisualYSnappingGuidelines;
			}
			set
			{
				base.VisualYSnappingGuidelines = value;
			}
		}

		/// <summary>Retorna o primeiro objeto visual de um teste de clique especificando um <see cref="T:System.Windows.Point" />.</summary>
		/// <param name="point">O valor de ponto a ter o teste de clique feito.</param>
		/// <returns>O resultado do teste de clique do visual retornado como um tipo <see cref="T:System.Windows.Media.HitTestResult" />.</returns>
		// Token: 0x06001FEB RID: 8171 RVA: 0x000827DC File Offset: 0x00081BDC
		public new HitTestResult HitTest(Point point)
		{
			return base.HitTest(point);
		}

		/// <summary>Inicia um teste de clique no <see cref="T:System.Windows.Media.ContainerVisual" /> usando os objetos <see cref="T:System.Windows.Media.HitTestFilterCallback" /> e <see cref="T:System.Windows.Media.HitTestResultCallback" />.</summary>
		/// <param name="filterCallback">O delegado que permite que você ignore as partes da árvore visual que você não está interessado em processar nos seus resultados de teste de clique.</param>
		/// <param name="resultCallback">O delegado usado para controlar o retorno de informações de teste de clique.</param>
		/// <param name="hitTestParameters">Define o conjunto de parâmetros para um teste de clique.</param>
		// Token: 0x06001FEC RID: 8172 RVA: 0x000827F0 File Offset: 0x00081BF0
		public new void HitTest(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
		{
			base.HitTest(filterCallback, resultCallback, hitTestParameters);
		}

		/// <summary>Obtém a caixa delimitadora do conteúdo do <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00082808 File Offset: 0x00081C08
		public Rect ContentBounds
		{
			get
			{
				return base.VisualContentBounds;
			}
		}

		/// <summary>Obtém ou define a transformação aplicada ao <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O valor de transformação.</returns>
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0008281C File Offset: 0x00081C1C
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00082830 File Offset: 0x00081C30
		public Transform Transform
		{
			get
			{
				return base.VisualTransform;
			}
			set
			{
				base.VisualTransform = value;
			}
		}

		/// <summary>Obtém ou define o valor de deslocamento do <see cref="T:System.Windows.Media.ContainerVisual" /> do seu ponto de referência.</summary>
		/// <returns>Um <see cref="T:System.Windows.Vector" /> que representa o valor de deslocamento do <see cref="T:System.Windows.Media.ContainerVisual" />.</returns>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00082844 File Offset: 0x00081C44
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x00082858 File Offset: 0x00081C58
		public Vector Offset
		{
			get
			{
				return base.VisualOffset;
			}
			set
			{
				base.VisualOffset = value;
			}
		}

		/// <summary>Obtém a união de todas as caixas delimitadoras de conteúdo para todos os descendentes do <see cref="T:System.Windows.Media.ContainerVisual" />, mas não incluindo o conteúdo do <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora de combinação.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0008286C File Offset: 0x00081C6C
		public Rect DescendantBounds
		{
			get
			{
				return base.VisualDescendantBounds;
			}
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Media.Visual" /> filho especificado para o <see cref="T:System.Windows.Media.ContainerVisual" /> pai.</summary>
		/// <param name="index">Um inteiro com sinal de 32 bits que representa o valor do índice do <see cref="T:System.Windows.Media.Visual" /> filho. O valor de <paramref name="index" /> deve estar entre 0 e <see cref="P:System.Windows.Media.ContainerVisual.VisualChildrenCount" /> -1.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Visual" /> filho.</returns>
		// Token: 0x06001FF3 RID: 8179 RVA: 0x00082880 File Offset: 0x00081C80
		protected sealed override Visual GetVisualChild(int index)
		{
			return this._children[index];
		}

		/// <summary>Obtém o número de filhos para o <see cref="T:System.Windows.Media.ContainerVisual" />.</summary>
		/// <returns>O número de filhos na <see cref="T:System.Windows.Media.VisualCollection" /> do <see cref="T:System.Windows.Media.ContainerVisual" />.</returns>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0008289C File Offset: 0x00081C9C
		protected sealed override int VisualChildrenCount
		{
			get
			{
				return this._children.Count;
			}
		}

		// Token: 0x0400107E RID: 4222
		private readonly VisualCollection _children;
	}
}
