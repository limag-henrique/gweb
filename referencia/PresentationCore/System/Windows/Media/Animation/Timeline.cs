using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Define um segmento de tempo.</summary>
	// Token: 0x0200055D RID: 1373
	[RuntimeNameProperty("Name")]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Timeline : Animatable
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Timeline" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06003F2E RID: 16174 RVA: 0x000F82A0 File Offset: 0x000F76A0
		public new Timeline Clone()
		{
			return (Timeline)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.Timeline" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06003F2F RID: 16175 RVA: 0x000F82B8 File Offset: 0x000F76B8
		public new Timeline CloneCurrentValue()
		{
			return (Timeline)base.CloneCurrentValue();
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		// Token: 0x06003F30 RID: 16176 RVA: 0x000F82D0 File Offset: 0x000F76D0
		protected Timeline()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Timeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> especificado.</summary>
		/// <param name="beginTime">A hora em que esta <see cref="T:System.Windows.Media.Animation.Timeline" /> deve começar. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para obter mais informações.</param>
		// Token: 0x06003F31 RID: 16177 RVA: 0x000F82E4 File Offset: 0x000F76E4
		protected Timeline(TimeSpan? beginTime) : this()
		{
			this.BeginTime = beginTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Timeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> e <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificados.</summary>
		/// <param name="beginTime">A hora em que esta <see cref="T:System.Windows.Media.Animation.Timeline" /> deve começar. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para obter mais informações.</param>
		/// <param name="duration">O período de tempo em que essa linha do tempo é reproduzia, sem contar repetições. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003F32 RID: 16178 RVA: 0x000F8300 File Offset: 0x000F7700
		protected Timeline(TimeSpan? beginTime, Duration duration) : this()
		{
			this.BeginTime = beginTime;
			this.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Timeline" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />, <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> e <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> especificados.</summary>
		/// <param name="beginTime">A hora em que esta <see cref="T:System.Windows.Media.Animation.Timeline" /> deve começar. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para obter mais informações.</param>
		/// <param name="duration">O período de tempo em que essa linha do tempo é reproduzia, sem contar repetições. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="repeatBehavior">O comportamento de repetição dessa linha do tempo, como uma <see cref="P:System.Windows.Media.Animation.RepeatBehavior.Count" /> de iteração ou uma <see cref="P:System.Windows.Media.Animation.RepeatBehavior.Duration" /> de repetição. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> para obter mais informações.</param>
		// Token: 0x06003F33 RID: 16179 RVA: 0x000F8324 File Offset: 0x000F7724
		protected Timeline(TimeSpan? beginTime, Duration duration, RepeatBehavior repeatBehavior) : this()
		{
			this.BeginTime = beginTime;
			this.Duration = duration;
			this.RepeatBehavior = repeatBehavior;
		}

		/// <summary>Altera esta <see cref="T:System.Windows.Media.Animation.Timeline" /> para não modificável ou determina se ela pode ser alterada para não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura.  
		/// Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003F34 RID: 16180 RVA: 0x000F834C File Offset: 0x000F774C
		protected override bool FreezeCore(bool isChecking)
		{
			this.ValidateTimeline();
			return base.FreezeCore(isChecking);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Timeline" /> especificado.</summary>
		/// <param name="sourceFreezable">A instância <see cref="T:System.Windows.Media.Animation.Timeline" /> a ser clonada.</param>
		// Token: 0x06003F35 RID: 16181 RVA: 0x000F8368 File Offset: 0x000F7768
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Timeline sourceTimeline = (Timeline)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceTimeline);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Timeline" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Timeline" /> a ser copiado e congelado.</param>
		// Token: 0x06003F36 RID: 16182 RVA: 0x000F838C File Offset: 0x000F778C
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Timeline sourceTimeline = (Timeline)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceTimeline);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x000F83B0 File Offset: 0x000F77B0
		private static void Timeline_PropertyChangedFunction(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Timeline)d).PropertyChanged(e.Property);
		}

		/// <summary>Obtém ou define um valor que especifica o percentual do <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> da linha do tempo gasto que acelera a passagem de tempo de zero até sua taxa máxima.</summary>
		/// <returns>Um valor entre 0 e 1, inclusive, que especifica o percentual do <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> da linha do tempo gasto que acelera a passagem de tempo de zero até sua taxa máxima. Se a linha do tempo <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> propriedade também é definida, a soma dos <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> e <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> deve ser menor ou igual a 1. O valor padrão é 0.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> é menor que 0 ou maior que 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">A soma de <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> e <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> ultrapassa 1.</exception>
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x000F83D0 File Offset: 0x000F77D0
		// (set) Token: 0x06003F39 RID: 16185 RVA: 0x000F83F0 File Offset: 0x000F77F0
		public double AccelerationRatio
		{
			get
			{
				return (double)base.GetValue(Timeline.AccelerationRatioProperty);
			}
			set
			{
				base.SetValue(Timeline.AccelerationRatioProperty, value);
			}
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000F8410 File Offset: 0x000F7810
		private static bool ValidateAccelerationDecelerationRatio(object value)
		{
			double num = (double)value;
			if (num < 0.0 || num > 1.0 || double.IsNaN(num))
			{
				throw new ArgumentException(SR.Get("Timing_InvalidArgAccelAndDecel"), "value");
			}
			return true;
		}

		/// <summary>Obtém ou define um valor que indica se a linha do tempo é executada em ordem inversa após concluir uma iteração na ordem comum.</summary>
		/// <returns>
		///   <see langword="true" /> para que a linha do tempo seja executada em ordem inversa ao final de cada iteração; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000F845C File Offset: 0x000F785C
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x000F847C File Offset: 0x000F787C
		[DefaultValue(false)]
		public bool AutoReverse
		{
			get
			{
				return (bool)base.GetValue(Timeline.AutoReverseProperty);
			}
			set
			{
				base.SetValue(Timeline.AutoReverseProperty, value);
			}
		}

		/// <summary>Obtém ou define a hora em que esse <see cref="T:System.Windows.Media.Animation.Timeline" /> deve começar.</summary>
		/// <returns>A hora em que esse <see cref="T:System.Windows.Media.Animation.Timeline" /> deve começar, em relação ao seu pai <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />. Se essa linha do tempo é uma linha do tempo raiz, o tempo é em relação ao seu interativo (no momento em que a linha do tempo foi disparada) da hora de início. Esse valor pode ser positivo, negativo, ou <see langword="null" />; um <see langword="null" /> valor significa que a linha do tempo nunca é reproduzido. O valor padrão é zero.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000F8498 File Offset: 0x000F7898
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x000F84B8 File Offset: 0x000F78B8
		public TimeSpan? BeginTime
		{
			get
			{
				return (TimeSpan?)base.GetValue(Timeline.BeginTimeProperty);
			}
			set
			{
				base.SetValue(Timeline.BeginTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica o percentual do <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> da linha do tempo gasto que desacelera a passagem de tempo de sua taxa máxima até zero.</summary>
		/// <returns>Um valor entre 0 e 1, inclusive, que especifica o percentual do <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> da linha do tempo gasto que desacelera a passagem de tempo de sua taxa máxima até zero. Se a linha do tempo <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> propriedade também é definida, a soma dos <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> e <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> deve ser menor ou igual a 1. O valor padrão é 0.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> é menor que 0 ou maior que 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">A soma de <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" /> e <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" /> ultrapassa 1.</exception>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x000F84D8 File Offset: 0x000F78D8
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x000F84F8 File Offset: 0x000F78F8
		public double DecelerationRatio
		{
			get
			{
				return (double)base.GetValue(Timeline.DecelerationRatioProperty);
			}
			set
			{
				base.SetValue(Timeline.DecelerationRatioProperty, value);
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000F8518 File Offset: 0x000F7918
		private static bool ValidateDesiredFrameRate(object value)
		{
			int? num = (int?)value;
			return num == null || num.Value > 0;
		}

		/// <summary>Obtém a taxa de quadros desejada da <see cref="T:System.Windows.Media.Animation.Timeline" /> especificada.</summary>
		/// <param name="timeline">A linha do tempo da qual a taxa de quadros desejada deve ser recuperada.</param>
		/// <returns>A taxa de quadros desejada dessa linha do tempo. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000F8544 File Offset: 0x000F7944
		public static int? GetDesiredFrameRate(Timeline timeline)
		{
			if (timeline == null)
			{
				throw new ArgumentNullException("timeline");
			}
			return (int?)timeline.GetValue(Timeline.DesiredFrameRateProperty);
		}

		/// <summary>Define a taxa de quadros desejada do <see cref="T:System.Windows.Media.Animation.Timeline" /> especificado.</summary>
		/// <param name="timeline">O <see cref="T:System.Windows.Media.Animation.Timeline" /> ao qual <paramref name="desiredFrameRate" /> é atribuído.</param>
		/// <param name="desiredFrameRate">O número máximo de quadros que essa linha do tempo deve gerar por segundo ou <see langword="null" /> quando o sistema deve controlar o número de quadros.</param>
		// Token: 0x06003F43 RID: 16195 RVA: 0x000F8570 File Offset: 0x000F7970
		public static void SetDesiredFrameRate(Timeline timeline, int? desiredFrameRate)
		{
			if (timeline == null)
			{
				throw new ArgumentNullException("timeline");
			}
			timeline.SetValue(Timeline.DesiredFrameRateProperty, desiredFrameRate);
		}

		/// <summary>Obtém ou define o período para o qual essa linha do tempo é reproduzida, sem contar repetições.</summary>
		/// <returns>A duração simples da linha do tempo: a quantidade de tempo que essa linha do tempo leva para concluir uma única iteração de encaminhamento. O valor padrão é <see cref="P:System.Windows.Duration.Automatic" />.</returns>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000F859C File Offset: 0x000F799C
		// (set) Token: 0x06003F45 RID: 16197 RVA: 0x000F85BC File Offset: 0x000F79BC
		public Duration Duration
		{
			get
			{
				return (Duration)base.GetValue(Timeline.DurationProperty);
			}
			set
			{
				base.SetValue(Timeline.DurationProperty, value);
			}
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000F85DC File Offset: 0x000F79DC
		private static bool ValidateFillBehavior(object value)
		{
			return TimeEnumHelper.IsValidFillBehavior((FillBehavior)value);
		}

		/// <summary>Obtém ou define um valor que especifica como a <see cref="T:System.Windows.Media.Animation.Timeline" /> se comporta depois que atinge o final do seu período ativo.</summary>
		/// <returns>Um valor que especifica como a linha do tempo se comporta depois que atinge o final do seu período ativo, mas com seu pai dentro de seu período ativo ou de preenchimento. O valor padrão é <see cref="F:System.Windows.Media.Animation.FillBehavior.HoldEnd" />.</returns>
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000F85F4 File Offset: 0x000F79F4
		// (set) Token: 0x06003F48 RID: 16200 RVA: 0x000F8614 File Offset: 0x000F7A14
		public FillBehavior FillBehavior
		{
			get
			{
				return (FillBehavior)base.GetValue(Timeline.FillBehaviorProperty);
			}
			set
			{
				base.SetValue(Timeline.FillBehaviorProperty, value);
			}
		}

		/// <summary>Obtém ou define o nome deste <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		/// <returns>O nome dessa linha do tempo. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000F8634 File Offset: 0x000F7A34
		// (set) Token: 0x06003F4A RID: 16202 RVA: 0x000F8654 File Offset: 0x000F7A54
		[DefaultValue(null)]
		[MergableProperty(false)]
		public string Name
		{
			get
			{
				return (string)base.GetValue(Timeline.NameProperty);
			}
			set
			{
				base.SetValue(Timeline.NameProperty, value);
			}
		}

		/// <summary>Obtém ou define o comportamento de repetição desta linha do tempo.</summary>
		/// <returns>Uma iteração de <see cref="P:System.Windows.Media.Animation.RepeatBehavior.Count" /> que especifica quantas vezes a linha do tempo deve ser executada, um valor de <see cref="T:System.TimeSpan" /> que especifica o tamanho total do período de atividade dessa linha do tempo ou o valor especial <see cref="P:System.Windows.Media.Animation.RepeatBehavior.Forever" />, que especifica que a linha do tempo deve ser repetida indefinidamente. O valor padrão é uma <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> com um <see cref="P:System.Windows.Media.Animation.RepeatBehavior.Count" /> dos 1, que indica que a linha do tempo é executada uma vez.</returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000F8670 File Offset: 0x000F7A70
		// (set) Token: 0x06003F4C RID: 16204 RVA: 0x000F8690 File Offset: 0x000F7A90
		public RepeatBehavior RepeatBehavior
		{
			get
			{
				return (RepeatBehavior)base.GetValue(Timeline.RepeatBehaviorProperty);
			}
			set
			{
				base.SetValue(Timeline.RepeatBehaviorProperty, value);
			}
		}

		/// <summary>Obtém ou define a taxa, em relação ao pai, na qual o tempo progride para isso <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		/// <returns>Um valor finito maior que 0 que descreve a taxa na qual o tempo progride para esta linha do tempo, em relação à velocidade do pai da linha do tempo ou, se esta for uma linha do tempo raiz, a velocidade padrão da linha do tempo. O valor padrão é 1.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Media.Animation.Timeline.SpeedRatio" /> é menor que 0 ou não é um valor finito.</exception>
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x000F86B0 File Offset: 0x000F7AB0
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x000F86D0 File Offset: 0x000F7AD0
		[DefaultValue(1.0)]
		public double SpeedRatio
		{
			get
			{
				return (double)base.GetValue(Timeline.SpeedRatioProperty);
			}
			set
			{
				base.SetValue(Timeline.SpeedRatioProperty, value);
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000F86F0 File Offset: 0x000F7AF0
		private static bool ValidateSpeedRatio(object value)
		{
			double num = (double)value;
			if (num <= 0.0 || num > 1.7976931348623157E+308 || double.IsNaN(num))
			{
				throw new ArgumentException(SR.Get("Timing_InvalidArgFinitePositive"), "value");
			}
			return true;
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Animation.Clock" /> para esta <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		/// <returns>Um relógio para esta <see cref="T:System.Windows.Media.Animation.Timeline" />.</returns>
		// Token: 0x06003F50 RID: 16208 RVA: 0x000F873C File Offset: 0x000F7B3C
		protected internal virtual Clock AllocateClock()
		{
			return new Clock(this);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Animation.Clock" /> controlável desta <see cref="T:System.Windows.Media.Animation.Timeline" />. Se esta <see cref="T:System.Windows.Media.Animation.Timeline" /> tiver filhas, uma árvore de relógios será criada com esta <see cref="T:System.Windows.Media.Animation.Timeline" /> como a raiz.</summary>
		/// <returns>Um novo <see cref="T:System.Windows.Media.Animation.Clock" /> controlável construído nesta <see cref="T:System.Windows.Media.Animation.Timeline" />. Se esta <see cref="T:System.Windows.Media.Animation.Timeline" /> for um <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> contendo linhas do tempo filhas, uma árvore de objetos <see cref="T:System.Windows.Media.Animation.Clock" /> será criada com um <see cref="T:System.Windows.Media.Animation.Clock" /> controlável criado usando esta <see cref="T:System.Windows.Media.Animation.Timeline" /> como a raiz.</returns>
		// Token: 0x06003F51 RID: 16209 RVA: 0x000F8750 File Offset: 0x000F7B50
		public Clock CreateClock()
		{
			return this.CreateClock(true);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Animation.Clock" /> desta <see cref="T:System.Windows.Media.Animation.Timeline" /> e especifica se o novo <see cref="T:System.Windows.Media.Animation.Clock" /> é controlável. Se esta <see cref="T:System.Windows.Media.Animation.Timeline" /> tiver filhas, uma árvore de relógios será criada com esta <see cref="T:System.Windows.Media.Animation.Timeline" /> como a raiz.</summary>
		/// <param name="hasControllableRoot">
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Animation.Clock" /> raiz retornado precisar retornar um <see cref="T:System.Windows.Media.Animation.ClockController" /> de sua propriedade <see cref="P:System.Windows.Media.Animation.Clock.Controller" /> para que a árvore <see cref="T:System.Windows.Media.Animation.Clock" /> possa ser controlada interativamente, caso contrário, <see langword="false" />.</param>
		/// <returns>Um novo <see cref="T:System.Windows.Media.Animation.Clock" /> construído desta <see cref="T:System.Windows.Media.Animation.Timeline" />. Se esta <see cref="T:System.Windows.Media.Animation.Timeline" /> for um <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> contendo linhas do tempo filhas, uma árvore de objetos <see cref="T:System.Windows.Media.Animation.Clock" /> será criada com um <see cref="T:System.Windows.Media.Animation.Clock" /> controlável criado usando esta <see cref="T:System.Windows.Media.Animation.Timeline" /> como a raiz.</returns>
		// Token: 0x06003F52 RID: 16210 RVA: 0x000F8764 File Offset: 0x000F7B64
		public Clock CreateClock(bool hasControllableRoot)
		{
			return Clock.BuildClockTreeFromTimeline(this, hasControllableRoot);
		}

		/// <summary>Retorna o comprimento de uma única iteração desta <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> que foi criado para esta <see cref="T:System.Windows.Media.Animation.Timeline" />.</param>
		/// <returns>O comprimento de uma única iteração desta <see cref="T:System.Windows.Media.Animation.Timeline" /> ou <see cref="P:System.Windows.Duration.Automatic" /> se a duração natural for desconhecida.</returns>
		// Token: 0x06003F53 RID: 16211 RVA: 0x000F8778 File Offset: 0x000F7B78
		protected internal Duration GetNaturalDuration(Clock clock)
		{
			return this.GetNaturalDurationCore(clock);
		}

		/// <summary>Retorna o comprimento de uma única iteração desta <see cref="T:System.Windows.Media.Animation.Timeline" />. Esse método fornece a implementação para <see cref="M:System.Windows.Media.Animation.Timeline.GetNaturalDuration(System.Windows.Media.Animation.Clock)" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> que foi criado para esta <see cref="T:System.Windows.Media.Animation.Timeline" />.</param>
		/// <returns>O comprimento de uma única iteração desta <see cref="T:System.Windows.Media.Animation.Timeline" /> ou <see cref="P:System.Windows.Duration.Automatic" /> se a duração natural for desconhecida.</returns>
		// Token: 0x06003F54 RID: 16212 RVA: 0x000F878C File Offset: 0x000F7B8C
		protected virtual Duration GetNaturalDurationCore(Clock clock)
		{
			return Duration.Automatic;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000F87A0 File Offset: 0x000F7BA0
		private void ValidateTimeline()
		{
			if (this.AccelerationRatio + this.DecelerationRatio > 1.0)
			{
				throw new InvalidOperationException(SR.Get("Timing_AccelAndDecelGreaterThanOne"));
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Media.Animation.Clock.CurrentState" /> do <see cref="T:System.Windows.Media.Animation.Clock" /> da linha do tempo é atualizada.</summary>
		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x06003F56 RID: 16214 RVA: 0x000F87D8 File Offset: 0x000F7BD8
		// (remove) Token: 0x06003F57 RID: 16215 RVA: 0x000F87F4 File Offset: 0x000F7BF4
		public event EventHandler CurrentStateInvalidated
		{
			add
			{
				this.AddEventHandler(Timeline.CurrentStateInvalidatedKey, value);
			}
			remove
			{
				this.RemoveEventHandler(Timeline.CurrentStateInvalidatedKey, value);
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> do <see cref="T:System.Windows.Media.Animation.Clock" /> da linha do tempo é atualizada.</summary>
		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x06003F58 RID: 16216 RVA: 0x000F8810 File Offset: 0x000F7C10
		// (remove) Token: 0x06003F59 RID: 16217 RVA: 0x000F882C File Offset: 0x000F7C2C
		public event EventHandler CurrentTimeInvalidated
		{
			add
			{
				this.AddEventHandler(Timeline.CurrentTimeInvalidatedKey, value);
			}
			remove
			{
				this.RemoveEventHandler(Timeline.CurrentTimeInvalidatedKey, value);
			}
		}

		/// <summary>Ocorre quando há alteração na taxa em que o tempo do relógio da linha do tempo progride.</summary>
		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06003F5A RID: 16218 RVA: 0x000F8848 File Offset: 0x000F7C48
		// (remove) Token: 0x06003F5B RID: 16219 RVA: 0x000F8864 File Offset: 0x000F7C64
		public event EventHandler CurrentGlobalSpeedInvalidated
		{
			add
			{
				this.AddEventHandler(Timeline.CurrentGlobalSpeedInvalidatedKey, value);
			}
			remove
			{
				this.RemoveEventHandler(Timeline.CurrentGlobalSpeedInvalidatedKey, value);
			}
		}

		/// <summary>Ocorre quando essa linha do tempo concluiu a reprodução completamente: ela não inserirá seu período ativo.</summary>
		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x06003F5C RID: 16220 RVA: 0x000F8880 File Offset: 0x000F7C80
		// (remove) Token: 0x06003F5D RID: 16221 RVA: 0x000F889C File Offset: 0x000F7C9C
		public event EventHandler Completed
		{
			add
			{
				this.AddEventHandler(Timeline.CompletedKey, value);
			}
			remove
			{
				this.RemoveEventHandler(Timeline.CompletedKey, value);
			}
		}

		/// <summary>Ocorre quando o relógio criado para essa linha do tempo ou para uma de suas linhas do tempo pais é removido.</summary>
		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x06003F5E RID: 16222 RVA: 0x000F88B8 File Offset: 0x000F7CB8
		// (remove) Token: 0x06003F5F RID: 16223 RVA: 0x000F88D4 File Offset: 0x000F7CD4
		public event EventHandler RemoveRequested
		{
			add
			{
				this.AddEventHandler(Timeline.RemoveRequestedKey, value);
			}
			remove
			{
				this.RemoveEventHandler(Timeline.RemoveRequestedKey, value);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000F88F0 File Offset: 0x000F7CF0
		internal EventHandlersStore InternalEventHandlersStore
		{
			get
			{
				return Timeline.EventHandlersStoreField.GetValue(this);
			}
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000F8908 File Offset: 0x000F7D08
		internal void InternalOnFreezablePropertyChanged(Timeline originalTimeline, Timeline newTimeline)
		{
			base.OnFreezablePropertyChanged(originalTimeline, newTimeline);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000F8920 File Offset: 0x000F7D20
		internal bool InternalFreeze(bool isChecking)
		{
			return Freezable.Freeze(this, isChecking);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000F8934 File Offset: 0x000F7D34
		internal void InternalReadPreamble()
		{
			base.ReadPreamble();
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000F8948 File Offset: 0x000F7D48
		internal void InternalWritePostscript()
		{
			base.WritePostscript();
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000F895C File Offset: 0x000F7D5C
		private void AddEventHandler(EventPrivateKey key, Delegate handler)
		{
			base.WritePreamble();
			EventHandlersStore eventHandlersStore = Timeline.EventHandlersStoreField.GetValue(this);
			if (eventHandlersStore == null)
			{
				eventHandlersStore = new EventHandlersStore();
				Timeline.EventHandlersStoreField.SetValue(this, eventHandlersStore);
			}
			eventHandlersStore.Add(key, handler);
			base.WritePostscript();
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000F89A0 File Offset: 0x000F7DA0
		private void CopyCommon(Timeline sourceTimeline)
		{
			EventHandlersStore value = Timeline.EventHandlersStoreField.GetValue(sourceTimeline);
			if (value != null)
			{
				Timeline.EventHandlersStoreField.SetValue(this, new EventHandlersStore(value));
			}
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000F89D0 File Offset: 0x000F7DD0
		private void RemoveEventHandler(EventPrivateKey key, Delegate handler)
		{
			base.WritePreamble();
			EventHandlersStore value = Timeline.EventHandlersStoreField.GetValue(this);
			if (value != null)
			{
				value.Remove(key, handler);
				if (value.Count == 0)
				{
					Timeline.EventHandlersStoreField.ClearValue(this);
				}
				base.WritePostscript();
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.AccelerationRatio" />.</summary>
		// Token: 0x04001761 RID: 5985
		public static readonly DependencyProperty AccelerationRatioProperty = DependencyProperty.Register("AccelerationRatio", typeof(double), typeof(Timeline), new PropertyMetadata(0.0, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(Timeline.ValidateAccelerationDecelerationRatio));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.AutoReverse" />.</summary>
		// Token: 0x04001762 RID: 5986
		public static readonly DependencyProperty AutoReverseProperty = DependencyProperty.Register("AutoReverse", typeof(bool), typeof(Timeline), new PropertyMetadata(false, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />.</summary>
		// Token: 0x04001763 RID: 5987
		public static readonly DependencyProperty BeginTimeProperty = DependencyProperty.Register("BeginTime", typeof(TimeSpan?), typeof(Timeline), new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)));

		/// <summary>Identifica a propriedade de dependência de <see cref="P:System.Windows.Media.Animation.Timeline.DecelerationRatio" />.</summary>
		// Token: 0x04001764 RID: 5988
		public static readonly DependencyProperty DecelerationRatioProperty = DependencyProperty.Register("DecelerationRatio", typeof(double), typeof(Timeline), new PropertyMetadata(0.0, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(Timeline.ValidateAccelerationDecelerationRatio));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.DesiredFrameRate" /> anexada.</summary>
		// Token: 0x04001765 RID: 5989
		public static readonly DependencyProperty DesiredFrameRateProperty = DependencyProperty.RegisterAttached("DesiredFrameRate", typeof(int?), typeof(Timeline), new PropertyMetadata(null, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(Timeline.ValidateDesiredFrameRate));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.Duration" />.</summary>
		// Token: 0x04001766 RID: 5990
		public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(Timeline), new PropertyMetadata(Duration.Automatic, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.FillBehavior" />.</summary>
		// Token: 0x04001767 RID: 5991
		public static readonly DependencyProperty FillBehaviorProperty = DependencyProperty.Register("FillBehavior", typeof(FillBehavior), typeof(Timeline), new PropertyMetadata(FillBehavior.HoldEnd, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(Timeline.ValidateFillBehavior));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.Name" />.</summary>
		// Token: 0x04001768 RID: 5992
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Timeline), new PropertyMetadata(null, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(NameValidationHelper.NameValidationCallback));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" />.</summary>
		// Token: 0x04001769 RID: 5993
		public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(Timeline), new PropertyMetadata(new RepeatBehavior(1.0), new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)));

		/// <summary>Identifica a propriedade de dependência de <see cref="P:System.Windows.Media.Animation.Timeline.SpeedRatio" />.</summary>
		// Token: 0x0400176A RID: 5994
		public static readonly DependencyProperty SpeedRatioProperty = DependencyProperty.Register("SpeedRatio", typeof(double), typeof(Timeline), new PropertyMetadata(1.0, new PropertyChangedCallback(Timeline.Timeline_PropertyChangedFunction)), new ValidateValueCallback(Timeline.ValidateSpeedRatio));

		// Token: 0x0400176B RID: 5995
		internal static readonly UncommonField<EventHandlersStore> EventHandlersStoreField = new UncommonField<EventHandlersStore>();

		// Token: 0x0400176C RID: 5996
		internal static readonly EventPrivateKey CurrentGlobalSpeedInvalidatedKey = new EventPrivateKey();

		// Token: 0x0400176D RID: 5997
		internal static readonly EventPrivateKey CurrentStateInvalidatedKey = new EventPrivateKey();

		// Token: 0x0400176E RID: 5998
		internal static readonly EventPrivateKey CurrentTimeInvalidatedKey = new EventPrivateKey();

		// Token: 0x0400176F RID: 5999
		internal static readonly EventPrivateKey CompletedKey = new EventPrivateKey();

		// Token: 0x04001770 RID: 6000
		internal static readonly EventPrivateKey RemoveRequestedKey = new EventPrivateKey();
	}
}
