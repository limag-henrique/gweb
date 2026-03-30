using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Define um segmento de tempo em que os valores de saída são produzidos. Esses valores são usados para animar uma propriedade de destino.</summary>
	// Token: 0x020004A5 RID: 1189
	public abstract class AnimationTimeline : Timeline
	{
		// Token: 0x060034BF RID: 13503 RVA: 0x000D17D0 File Offset: 0x000D0BD0
		private static void AnimationTimeline_PropertyChangedFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((AnimationTimeline)d).PropertyChanged(e.Property);
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060034C0 RID: 13504 RVA: 0x000D17F0 File Offset: 0x000D0BF0
		public new AnimationTimeline Clone()
		{
			return (AnimationTimeline)base.Clone();
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Animation.Clock" /> para esta <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</summary>
		/// <returns>Um relógio para esta <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</returns>
		// Token: 0x060034C1 RID: 13505 RVA: 0x000D1808 File Offset: 0x000D0C08
		protected internal override Clock AllocateClock()
		{
			return new AnimationClock(this);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Animation.AnimationClock" /> com base neste <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</summary>
		/// <returns>Um novo relógio, criado com base neste <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</returns>
		// Token: 0x060034C2 RID: 13506 RVA: 0x000D181C File Offset: 0x000D0C1C
		public new AnimationClock CreateClock()
		{
			return (AnimationClock)base.CreateClock();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial. Se esta animação for a primeira em uma cadeia de composição, ela será o valor base da propriedade que estiver sendo animada; caso contrário, será o valor retornado pela animação anterior na cadeia.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tem seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x060034C3 RID: 13507 RVA: 0x000D1834 File Offset: 0x000D0C34
		public virtual object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			base.ReadPreamble();
			return defaultDestinationValue;
		}

		/// <summary>Retorna o comprimento de uma única iteração deste <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</summary>
		/// <param name="clock">O relógio que foi criado para este <see cref="T:System.Windows.Media.Animation.AnimationTimeline" />.</param>
		/// <returns>A duração natural da animação. Esse método sempre retorna um <see cref="T:System.Windows.Duration" /> de 1 segundo.</returns>
		// Token: 0x060034C4 RID: 13508 RVA: 0x000D1848 File Offset: 0x000D0C48
		protected override Duration GetNaturalDurationCore(Clock clock)
		{
			return new TimeSpan(0, 0, 1);
		}

		/// <summary>Quando substituído em uma classe derivada, obtém o <see cref="T:System.Type" /> da propriedade que pode ser animada.</summary>
		/// <returns>O tipo de propriedade que pode ser animado por essa animação.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060034C5 RID: 13509
		public abstract Type TargetPropertyType { get; }

		/// <summary>Obtém um valor que indica se a animação usa o parâmetro defaultDestinationValue do método <see cref="M:System.Windows.Media.Animation.AnimationTimeline.GetCurrentValue(System.Object,System.Object,System.Windows.Media.Animation.AnimationClock)" /> como seu valor de destino.</summary>
		/// <returns>
		///   <see langword="true" /> Se o defaultDesintationValue parâmetro do <see cref="M:System.Windows.Media.Animation.AnimationTimeline.GetCurrentValue(System.Object,System.Object,System.Windows.Media.Animation.AnimationClock)" /> método é o valor dessa animação quando atinge o final de sua duração simples (seu relógio tem uma <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> de 1); caso contrário, <see langword="false" />. A implementação padrão sempre retorna <see langword="false" />.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000D1864 File Offset: 0x000D0C64
		public virtual bool IsDestinationDefault
		{
			get
			{
				base.ReadPreamble();
				return false;
			}
		}

		/// <summary>Identifica a propriedade de dependência IsAdditive.</summary>
		// Token: 0x04001617 RID: 5655
		public static readonly DependencyProperty IsAdditiveProperty = DependencyProperty.Register("IsAdditive", typeof(bool), typeof(AnimationTimeline), new PropertyMetadata(false, new PropertyChangedCallback(AnimationTimeline.AnimationTimeline_PropertyChangedFunction)));

		/// <summary>Identifica a propriedade de dependência IsCumulative.</summary>
		// Token: 0x04001618 RID: 5656
		public static readonly DependencyProperty IsCumulativeProperty = DependencyProperty.Register("IsCumulative", typeof(bool), typeof(AnimationTimeline), new PropertyMetadata(false, new PropertyChangedCallback(AnimationTimeline.AnimationTimeline_PropertyChangedFunction)));
	}
}
