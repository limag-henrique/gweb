using System;
using System.Collections.Generic;
using System.ComponentModel;
using MS.Internal;

namespace System.Windows.Media.Animation
{
	/// <summary>Define um segmento de tempo que pode conter objetos <see cref="T:System.Windows.Media.Animation.Timeline" /> filho. Essas linhas do tempo filho ficam ativas de acordo com suas respectivas propriedades <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />. Além disso, as linhas do tempo filho podem ser sobrepostas (executadas em paralelo).</summary>
	// Token: 0x02000525 RID: 1317
	public class ParallelTimeline : TimelineGroup
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ParallelTimeline" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003B9B RID: 15259 RVA: 0x000EA288 File Offset: 0x000E9688
		public new ParallelTimeline Clone()
		{
			return (ParallelTimeline)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.ParallelTimeline" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003B9C RID: 15260 RVA: 0x000EA2A0 File Offset: 0x000E96A0
		public new ParallelTimeline CloneCurrentValue()
		{
			return (ParallelTimeline)base.CloneCurrentValue();
		}

		/// <summary>Cria uma nova instância deste <see cref="T:System.Windows.Freezable" />.</summary>
		/// <returns>O novo <see cref="T:System.Windows.Freezable" />.</returns>
		// Token: 0x06003B9D RID: 15261 RVA: 0x000EA2B8 File Offset: 0x000E96B8
		protected override Freezable CreateInstanceCore()
		{
			return new ParallelTimeline();
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ParallelTimeline" />.</summary>
		// Token: 0x06003B9E RID: 15262 RVA: 0x000EA2CC File Offset: 0x000E96CC
		public ParallelTimeline()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> especificado.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003B9F RID: 15263 RVA: 0x000EA2E0 File Offset: 0x000E96E0
		public ParallelTimeline(TimeSpan? beginTime) : base(beginTime)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> e <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificados.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="duration">O <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000EA2F4 File Offset: 0x000E96F4
		public ParallelTimeline(TimeSpan? beginTime, Duration duration) : base(beginTime, duration)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />, <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> e <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> especificados.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="duration">O <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="repeatBehavior">O <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000EA30C File Offset: 0x000E970C
		public ParallelTimeline(TimeSpan? beginTime, Duration duration, RepeatBehavior repeatBehavior) : base(beginTime, duration, repeatBehavior)
		{
		}

		/// <summary>Retorna a duração natural (duração de uma única iteração) de um <see cref="T:System.Windows.Media.Animation.Clock" /> especificado.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> do qual retornar a duração natural.</param>
		/// <returns>A quantidade de <see cref="T:System.Windows.Duration" /> que representa a duração natural.</returns>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000EA324 File Offset: 0x000E9724
		protected override Duration GetNaturalDurationCore(Clock clock)
		{
			Duration duration = TimeSpan.Zero;
			ClockGroup clockGroup = clock as ClockGroup;
			if (clockGroup != null)
			{
				List<Clock> internalChildren = clockGroup.InternalChildren;
				if (internalChildren != null)
				{
					bool flag = false;
					for (int i = 0; i < internalChildren.Count; i++)
					{
						Duration endOfActivePeriod = internalChildren[i].EndOfActivePeriod;
						if (endOfActivePeriod == Duration.Forever)
						{
							return Duration.Forever;
						}
						if (endOfActivePeriod == Duration.Automatic)
						{
							flag = true;
						}
						else if (endOfActivePeriod > duration)
						{
							duration = endOfActivePeriod;
						}
					}
					if (flag)
					{
						return Duration.Automatic;
					}
				}
			}
			return duration;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000EA3B0 File Offset: 0x000E97B0
		private static bool ValidateSlipBehavior(object value)
		{
			return TimeEnumHelper.IsValidSlipBehavior((SlipBehavior)value);
		}

		/// <summary>Obtém ou define um valor que especifica o comportamento dessa linha do tempo quando um ou mais dos seus <see cref="T:System.Windows.Media.Animation.Timeline" /> filhos se desvia.</summary>
		/// <returns>Um valor que indica como essa linha do tempo se comportará quando um ou mais dos seus <see cref="T:System.Windows.Media.Animation.Timeline" /> filhos se desvia. O valor padrão é <see cref="F:System.Windows.Media.Animation.SlipBehavior.Grow" />.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000EA3C8 File Offset: 0x000E97C8
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000EA3E8 File Offset: 0x000E97E8
		[DefaultValue(SlipBehavior.Grow)]
		public SlipBehavior SlipBehavior
		{
			get
			{
				return (SlipBehavior)base.GetValue(ParallelTimeline.SlipBehaviorProperty);
			}
			set
			{
				base.SetValue(ParallelTimeline.SlipBehaviorProperty, value);
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000EA408 File Offset: 0x000E9808
		internal static void ParallelTimeline_PropertyChangedFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ParallelTimeline)d).PropertyChanged(e.Property);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ParallelTimeline.SlipBehavior" />.</summary>
		// Token: 0x040016F2 RID: 5874
		public static readonly DependencyProperty SlipBehaviorProperty = DependencyProperty.Register("SlipBehavior", typeof(SlipBehavior), typeof(ParallelTimeline), new PropertyMetadata(SlipBehavior.Grow, new PropertyChangedCallback(ParallelTimeline.ParallelTimeline_PropertyChangedFunction)), new ValidateValueCallback(ParallelTimeline.ValidateSlipBehavior));
	}
}
