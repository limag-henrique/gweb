using System;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa um efeito de texto que pode ser aplicado a objetos de texto.</summary>
	// Token: 0x020003F3 RID: 1011
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class TextEffect : Animatable
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.TextEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060027C0 RID: 10176 RVA: 0x0009FF98 File Offset: 0x0009F398
		public new TextEffect Clone()
		{
			return (TextEffect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.TextEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060027C1 RID: 10177 RVA: 0x0009FFB0 File Offset: 0x0009F3B0
		public new TextEffect CloneCurrentValue()
		{
			return (TextEffect)base.CloneCurrentValue();
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0009FFC8 File Offset: 0x0009F3C8
		private static bool ValidatePositionStartValue(object value)
		{
			return TextEffect.OnPositionStartChanging((int)value);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0009FFE8 File Offset: 0x0009F3E8
		private static bool ValidatePositionCountValue(object value)
		{
			return TextEffect.OnPositionCountChanging((int)value);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Transform" /> aplicado ao <see cref="T:System.Windows.Media.TextEffect" />.</summary>
		/// <returns>O valor <see cref="T:System.Windows.Media.Transform" /> do <see cref="T:System.Windows.Media.TextEffect" />.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000A0008 File Offset: 0x0009F408
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x000A0028 File Offset: 0x0009F428
		public Transform Transform
		{
			get
			{
				return (Transform)base.GetValue(TextEffect.TransformProperty);
			}
			set
			{
				base.SetValueInternal(TextEffect.TransformProperty, value);
			}
		}

		/// <summary>Obtém ou define a área de recorte do <see cref="T:System.Windows.Media.TextEffect" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> que define a região de recorte.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000A0044 File Offset: 0x0009F444
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x000A0064 File Offset: 0x0009F464
		public Geometry Clip
		{
			get
			{
				return (Geometry)base.GetValue(TextEffect.ClipProperty);
			}
			set
			{
				base.SetValueInternal(TextEffect.ClipProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Brush" /> a ser aplicado ao conteúdo do <see cref="T:System.Windows.Media.TextEffect" />.</summary>
		/// <returns>O pincel usado para aplicar o <see cref="T:System.Windows.Media.TextEffect" />.</returns>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000A0080 File Offset: 0x0009F480
		// (set) Token: 0x060027C9 RID: 10185 RVA: 0x000A00A0 File Offset: 0x0009F4A0
		public Brush Foreground
		{
			get
			{
				return (Brush)base.GetValue(TextEffect.ForegroundProperty);
			}
			set
			{
				base.SetValueInternal(TextEffect.ForegroundProperty, value);
			}
		}

		/// <summary>Obtém ou define a posição inicial no texto ao qual o <see cref="T:System.Windows.Media.TextEffect" /> é aplicado.</summary>
		/// <returns>O <see cref="T:System.Int32" /> valor que representa a posição inicial no texto que o <see cref="T:System.Windows.Media.TextEffect" /> aplica-se a.</returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000A00BC File Offset: 0x0009F4BC
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x000A00DC File Offset: 0x0009F4DC
		public int PositionStart
		{
			get
			{
				return (int)base.GetValue(TextEffect.PositionStartProperty);
			}
			set
			{
				base.SetValueInternal(TextEffect.PositionStartProperty, value);
			}
		}

		/// <summary>Obtém ou define a posição no texto ao qual o <see cref="T:System.Windows.Media.TextEffect" /> é aplicado.</summary>
		/// <returns>O <see cref="T:System.Int32" /> valor que representa a posição no texto que o <see cref="T:System.Windows.Media.TextEffect" /> aplica-se a.</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000A00FC File Offset: 0x0009F4FC
		// (set) Token: 0x060027CD RID: 10189 RVA: 0x000A011C File Offset: 0x0009F51C
		public int PositionCount
		{
			get
			{
				return (int)base.GetValue(TextEffect.PositionCountProperty);
			}
			set
			{
				base.SetValueInternal(TextEffect.PositionCountProperty, value);
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000A013C File Offset: 0x0009F53C
		protected override Freezable CreateInstanceCore()
		{
			return new TextEffect();
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000A0150 File Offset: 0x0009F550
		static TextEffect()
		{
			Type typeFromHandle = typeof(TextEffect);
			TextEffect.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform), typeFromHandle, null, null, null, false, null);
			TextEffect.ClipProperty = Animatable.RegisterProperty("Clip", typeof(Geometry), typeFromHandle, null, null, null, false, null);
			TextEffect.ForegroundProperty = Animatable.RegisterProperty("Foreground", typeof(Brush), typeFromHandle, null, null, null, false, null);
			TextEffect.PositionStartProperty = Animatable.RegisterProperty("PositionStart", typeof(int), typeFromHandle, 0, null, new ValidateValueCallback(TextEffect.ValidatePositionStartValue), false, null);
			TextEffect.PositionCountProperty = Animatable.RegisterProperty("PositionCount", typeof(int), typeFromHandle, 0, null, new ValidateValueCallback(TextEffect.ValidatePositionCountValue), false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextEffect" /> especificando valores da propriedade da classe.</summary>
		/// <param name="transform">O <see cref="T:System.Windows.Media.Transform" /> que é aplicado ao <see cref="T:System.Windows.Media.TextEffect" />.</param>
		/// <param name="foreground">O <see cref="T:System.Windows.Media.Brush" /> a ser aplicado ao conteúdo do <see cref="T:System.Windows.Media.TextEffect" />.</param>
		/// <param name="clip">A área de recorte do <see cref="T:System.Windows.Media.TextEffect" />.</param>
		/// <param name="positionStart">A posição inicial no texto ao qual o <see cref="T:System.Windows.Media.TextEffect" /> é aplicado.</param>
		/// <param name="positionCount">O número de posições no texto ao qual o <see cref="T:System.Windows.Media.TextEffect" /> é aplicado.</param>
		// Token: 0x060027D0 RID: 10192 RVA: 0x000A0224 File Offset: 0x0009F624
		public TextEffect(Transform transform, Brush foreground, Geometry clip, int positionStart, int positionCount)
		{
			if (positionCount < 0)
			{
				throw new ArgumentOutOfRangeException("positionCount", SR.Get("ParameterCannotBeNegative"));
			}
			this.Transform = transform;
			this.Foreground = foreground;
			this.Clip = clip;
			this.PositionStart = positionStart;
			this.PositionCount = positionCount;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextEffect" />.</summary>
		// Token: 0x060027D1 RID: 10193 RVA: 0x000A0278 File Offset: 0x0009F678
		public TextEffect()
		{
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000A028C File Offset: 0x0009F68C
		private static bool OnPositionStartChanging(int value)
		{
			return value >= 0;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000A02A0 File Offset: 0x0009F6A0
		private static bool OnPositionCountChanging(int value)
		{
			return value >= 0;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TextEffect.Transform" />.</summary>
		// Token: 0x0400126C RID: 4716
		public static readonly DependencyProperty TransformProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TextEffect.Clip" />.</summary>
		// Token: 0x0400126D RID: 4717
		public static readonly DependencyProperty ClipProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TextEffect.Foreground" />.</summary>
		// Token: 0x0400126E RID: 4718
		public static readonly DependencyProperty ForegroundProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TextEffect.PositionStart" />.</summary>
		// Token: 0x0400126F RID: 4719
		public static readonly DependencyProperty PositionStartProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TextEffect.PositionCount" />.</summary>
		// Token: 0x04001270 RID: 4720
		public static readonly DependencyProperty PositionCountProperty;

		// Token: 0x04001271 RID: 4721
		internal const int c_PositionStart = 0;

		// Token: 0x04001272 RID: 4722
		internal const int c_PositionCount = 0;
	}
}
