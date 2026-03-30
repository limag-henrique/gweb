using System;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Aplica-se a <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> dada a propriedade de <see cref="P:System.Windows.UIElement.BitmapEffect" /> para uma região especificada do objeto visual.</summary>
	// Token: 0x0200060A RID: 1546
	public sealed class BitmapEffectInput : Animatable
	{
		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" />.</summary>
		// Token: 0x0600470E RID: 18190 RVA: 0x00116EF4 File Offset: 0x001162F4
		public BitmapEffectInput()
		{
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> como a origem para essa entrada.</summary>
		/// <param name="input">A origem de bitmap a ser usada por este objeto de entrada.</param>
		// Token: 0x0600470F RID: 18191 RVA: 0x00116F08 File Offset: 0x00116308
		public BitmapEffectInput(BitmapSource input)
		{
			this.Input = input;
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Determina se o <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.Input" /> deve ser serializado.</summary>
		/// <returns>
		///   <see langword="true" /> se a <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.Input" /> precisar ser serializado, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004710 RID: 18192 RVA: 0x00116F24 File Offset: 0x00116324
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeInput()
		{
			return this.Input != BitmapEffectInput.ContextInputSource;
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém um valor que representa o <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é derivado do contexto.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é derivado do contexto.</returns>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x00116F44 File Offset: 0x00116344
		public static BitmapSource ContextInputSource
		{
			get
			{
				if (BitmapEffectInput.s_defaultInputSource == null)
				{
					BitmapSource bitmapSource = new UnmanagedBitmapWrapper(true);
					bitmapSource.Freeze();
					BitmapEffectInput.s_defaultInputSource = bitmapSource;
				}
				return BitmapEffectInput.s_defaultInputSource;
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004712 RID: 18194 RVA: 0x00116F70 File Offset: 0x00116370
		public new BitmapEffectInput Clone()
		{
			return (BitmapEffectInput)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004713 RID: 18195 RVA: 0x00116F88 File Offset: 0x00116388
		public new BitmapEffectInput CloneCurrentValue()
		{
			return (BitmapEffectInput)base.CloneCurrentValue();
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00116FA0 File Offset: 0x001163A0
		private static void AreaToApplyEffectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapEffectInput bitmapEffectInput = (BitmapEffectInput)d;
			bitmapEffectInput.PropertyChanged(BitmapEffectInput.AreaToApplyEffectProperty);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define o <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é usado para a entrada do objeto.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é usado como entrada para o objeto. O valor padrão é nulo.</returns>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x00116FC0 File Offset: 0x001163C0
		// (set) Token: 0x06004716 RID: 18198 RVA: 0x00116FE0 File Offset: 0x001163E0
		public BitmapSource Input
		{
			get
			{
				return (BitmapSource)base.GetValue(BitmapEffectInput.InputProperty);
			}
			set
			{
				base.SetValueInternal(BitmapEffectInput.InputProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define o método com o qual o retângulo fornecido pelo <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.AreaToApplyEffect" /> deve ser interpretado.</summary>
		/// <returns>O método no qual interpretar o retângulo fornecido pelo <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.AreaToApplyEffectUnits" /> propriedade. O valor padrão é <see cref="F:System.Windows.Media.BrushMappingMode.RelativeToBoundingBox" />.</returns>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x00116FFC File Offset: 0x001163FC
		// (set) Token: 0x06004718 RID: 18200 RVA: 0x0011701C File Offset: 0x0011641C
		public BrushMappingMode AreaToApplyEffectUnits
		{
			get
			{
				return (BrushMappingMode)base.GetValue(BitmapEffectInput.AreaToApplyEffectUnitsProperty);
			}
			set
			{
				base.SetValueInternal(BitmapEffectInput.AreaToApplyEffectUnitsProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define uma região retangular no visual para a qual o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> é aplicado.</summary>
		/// <returns>A região retangular do visual para o qual o efeito é aplicado. O valor padrão é <see cref="P:System.Windows.Rect.Empty" />.</returns>
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x0011703C File Offset: 0x0011643C
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x0011705C File Offset: 0x0011645C
		public Rect AreaToApplyEffect
		{
			get
			{
				return (Rect)base.GetValue(BitmapEffectInput.AreaToApplyEffectProperty);
			}
			set
			{
				base.SetValueInternal(BitmapEffectInput.AreaToApplyEffectProperty, value);
			}
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x0011707C File Offset: 0x0011647C
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapEffectInput();
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00117090 File Offset: 0x00116490
		static BitmapEffectInput()
		{
			Type typeFromHandle = typeof(BitmapEffectInput);
			BitmapEffectInput.InputProperty = Animatable.RegisterProperty("Input", typeof(BitmapSource), typeFromHandle, BitmapEffectInput.ContextInputSource, null, null, false, null);
			BitmapEffectInput.AreaToApplyEffectUnitsProperty = Animatable.RegisterProperty("AreaToApplyEffectUnits", typeof(BrushMappingMode), typeFromHandle, BrushMappingMode.RelativeToBoundingBox, null, new ValidateValueCallback(ValidateEnums.IsBrushMappingModeValid), false, null);
			BitmapEffectInput.AreaToApplyEffectProperty = Animatable.RegisterProperty("AreaToApplyEffect", typeof(Rect), typeFromHandle, Rect.Empty, new PropertyChangedCallback(BitmapEffectInput.AreaToApplyEffectPropertyChanged), null, true, null);
		}

		// Token: 0x040019DF RID: 6623
		private static BitmapSource s_defaultInputSource;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.Input" />.</summary>
		// Token: 0x040019E0 RID: 6624
		public static readonly DependencyProperty InputProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.AreaToApplyEffectUnits" />.</summary>
		// Token: 0x040019E1 RID: 6625
		public static readonly DependencyProperty AreaToApplyEffectUnitsProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BitmapEffectInput.AreaToApplyEffect" />.</summary>
		// Token: 0x040019E2 RID: 6626
		public static readonly DependencyProperty AreaToApplyEffectProperty;

		// Token: 0x040019E3 RID: 6627
		internal static BitmapSource s_Input = BitmapEffectInput.ContextInputSource;

		// Token: 0x040019E4 RID: 6628
		internal const BrushMappingMode c_AreaToApplyEffectUnits = BrushMappingMode.RelativeToBoundingBox;

		// Token: 0x040019E5 RID: 6629
		internal static Rect s_AreaToApplyEffect = Rect.Empty;
	}
}
