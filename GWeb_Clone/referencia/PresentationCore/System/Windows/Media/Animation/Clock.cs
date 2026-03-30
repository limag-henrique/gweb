using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Mantém o estado do intervalo de tempo de execução para um <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
	// Token: 0x020004A7 RID: 1191
	public class Clock : DispatcherObject
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Clock" /> usando o <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> especificado como um modelo. O novo objeto <see cref="T:System.Windows.Media.Animation.Clock" /> não tem filhos.</summary>
		/// <param name="timeline">O <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> do qual esse relógio deve ser construído. Relógios não serão criados para nenhum objeto <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> filho, se algum existir.</param>
		// Token: 0x060034C8 RID: 13512 RVA: 0x000D18FC File Offset: 0x000D0CFC
		protected internal Clock(Timeline timeline)
		{
			this._timeline = (Timeline)timeline.GetCurrentValueAsFrozen();
			this._eventHandlersStore = timeline.InternalEventHandlersStore;
			this.SetFlag(Clock.ClockFlags.NeedsTicksWhenActive, this._eventHandlersStore != null);
			this._beginTime = this._timeline.BeginTime;
			this._resolvedDuration = this._timeline.Duration;
			if (this._resolvedDuration == Duration.Automatic)
			{
				this._resolvedDuration = Duration.Forever;
			}
			else
			{
				this.HasResolvedDuration = true;
			}
			this._currentDuration = this._resolvedDuration;
			this._appliedSpeedRatio = this._timeline.SpeedRatio;
			this._currentClockState = ClockState.Stopped;
			if (this._beginTime != null)
			{
				this._nextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000D19CC File Offset: 0x000D0DCC
		internal bool CanGrow
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CanGrow);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000D19E4 File Offset: 0x000D0DE4
		internal bool CanSlip
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CanSlip);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.ClockController" /> que pode ser usado para iniciar, pausar, retomar, pesquisar, ignorar, parar ou remover este <see cref="T:System.Windows.Media.Animation.Clock" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.ClockController" /> quando se trata de um relógio raiz; caso contrário, <see langword="null" />.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000D19FC File Offset: 0x000D0DFC
		public ClockController Controller
		{
			get
			{
				if (this.IsRoot && this.HasControllableRoot)
				{
					return new ClockController(this);
				}
				return null;
			}
		}

		/// <summary>Obtém a iteração atual deste relógio.</summary>
		/// <returns>A iteração atual do relógio dentro de seu período ativo atual ou <see langword="null" /> se este relógio for interrompido.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000D1A24 File Offset: 0x000D0E24
		public int? CurrentIteration
		{
			get
			{
				return this._currentIteration;
			}
		}

		/// <summary>Obtém a taxa em que o horário do relógio está atualmente em andamento se comparada à hora do mundo real.</summary>
		/// <returns>A taxa em que a hora do relógio é atual está em andamento, em comparação ao tempo do mundo real. Se o relógio for interrompido, essa propriedade retornará <see langword="null" />.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000D1A38 File Offset: 0x000D0E38
		public double? CurrentGlobalSpeed
		{
			get
			{
				return this._currentGlobalSpeed;
			}
		}

		/// <summary>Obtém o andamento atual deste <see cref="T:System.Windows.Media.Animation.Clock" /> dentro de sua iteração atual.</summary>
		/// <returns>
		///   <see langword="null" /> Se for esse relógio <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />, ou 0,0 se esse relógio estiver ativo e seu <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> tem um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> de <see cref="P:System.Windows.Duration.Forever" />; caso contrário, um valor entre 0,0 e 1,0 que indica o andamento atual deste relógio dentro de sua iteração atual. Um valor 0,0 não indica nenhum progresso e um valor 1,0 indica que o relógio está no final da sua iteração atual.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000D1A4C File Offset: 0x000D0E4C
		public double? CurrentProgress
		{
			get
			{
				return this._currentProgress;
			}
		}

		/// <summary>Obtém um valor que indica se o relógio é atualmente <see cref="F:System.Windows.Media.Animation.ClockState.Active" />, <see cref="F:System.Windows.Media.Animation.ClockState.Filling" /> ou <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />.</summary>
		/// <returns>O estado atual do relógio: <see cref="F:System.Windows.Media.Animation.ClockState.Active" />, <see cref="F:System.Windows.Media.Animation.ClockState.Filling" />, ou <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000D1A60 File Offset: 0x000D0E60
		public ClockState CurrentState
		{
			get
			{
				return this._currentClockState;
			}
		}

		/// <summary>Obtém a hora atual deste relógio dentro de sua iteração atual.</summary>
		/// <returns>
		///   <see langword="null" /> Se este relógio for <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />; caso contrário, a hora atual deste relógio.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000D1A74 File Offset: 0x000D0E74
		public TimeSpan? CurrentTime
		{
			get
			{
				return this._currentTime;
			}
		}

		/// <summary>Obtém um valor que indica se o controle <see cref="T:System.Windows.Media.Animation.Clock" /> faz parte de uma árvore de relógios controlável.</summary>
		/// <returns>
		///   <see langword="true" /> Se este relógio pertence a uma árvore de relógio com um relógio raiz controlável ou se este relógio em si for uma raiz controlável; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000D1A88 File Offset: 0x000D0E88
		public bool HasControllableRoot
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasControllableRoot);
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Media.Animation.Clock" /> ou qualquer um de seus pais, está em pausa.</summary>
		/// <returns>
		///   <see langword="true" /> Se este <see cref="T:System.Windows.Media.Animation.Clock" /> ou qualquer um de seus pais está em pausa; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000D1AA0 File Offset: 0x000D0EA0
		public bool IsPaused
		{
			get
			{
				return this.IsInteractivelyPaused;
			}
		}

		/// <summary>Obtém a duração normal do <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> deste relógio.</summary>
		/// <returns>A duração natural deste relógio, conforme determinado pela sua <see cref="P:System.Windows.Media.Animation.Clock.Timeline" />.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000D1AB4 File Offset: 0x000D0EB4
		public Duration NaturalDuration
		{
			get
			{
				return this._timeline.GetNaturalDuration(this);
			}
		}

		/// <summary>Obtém o relógio que é o pai deste relógio.</summary>
		/// <returns>O pai deste relógio ou <see langword="null" /> se este relógio for uma raiz.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000D1AD0 File Offset: 0x000D0ED0
		public Clock Parent
		{
			get
			{
				if (this.IsRoot)
				{
					return null;
				}
				return this._parent;
			}
		}

		/// <summary>Obtém o <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> do qual este <see cref="T:System.Windows.Media.Animation.Clock" /> foi criado.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Animation.Clock.Timeline" /> do qual este <see cref="T:System.Windows.Media.Animation.Clock" /> foi criado.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000D1AF0 File Offset: 0x000D0EF0
		public Timeline Timeline
		{
			get
			{
				return this._timeline;
			}
		}

		/// <summary>Ocorre quando a reprodução deste relógio foi completamente concluída.</summary>
		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x060034D6 RID: 13526 RVA: 0x000D1B04 File Offset: 0x000D0F04
		// (remove) Token: 0x060034D7 RID: 13527 RVA: 0x000D1B20 File Offset: 0x000D0F20
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

		/// <summary>Ocorre quando a velocidade do relógio é atualizada.</summary>
		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x060034D8 RID: 13528 RVA: 0x000D1B3C File Offset: 0x000D0F3C
		// (remove) Token: 0x060034D9 RID: 13529 RVA: 0x000D1B58 File Offset: 0x000D0F58
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

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Media.Animation.Clock.CurrentState" /> do relógio é atualizada.</summary>
		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x060034DA RID: 13530 RVA: 0x000D1B74 File Offset: 0x000D0F74
		// (remove) Token: 0x060034DB RID: 13531 RVA: 0x000D1B90 File Offset: 0x000D0F90
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

		/// <summary>Ocorre quando o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> deste relógio torna-se inválido.</summary>
		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x060034DC RID: 13532 RVA: 0x000D1BAC File Offset: 0x000D0FAC
		// (remove) Token: 0x060034DD RID: 13533 RVA: 0x000D1BC8 File Offset: 0x000D0FC8
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

		/// <summary>Ocorre quando o método <see cref="M:System.Windows.Media.Animation.ClockController.Remove" /> é chamado neste <see cref="T:System.Windows.Media.Animation.Clock" /> ou em um de seus relógios pai.</summary>
		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x060034DE RID: 13534 RVA: 0x000D1BE4 File Offset: 0x000D0FE4
		// (remove) Token: 0x060034DF RID: 13535 RVA: 0x000D1C00 File Offset: 0x000D1000
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

		/// <summary>Quando implementado em uma classe derivada, será invocado sempre que um relógio se repete, ignora ou busca.</summary>
		// Token: 0x060034E0 RID: 13536 RVA: 0x000D1C1C File Offset: 0x000D101C
		protected virtual void DiscontinuousTimeMovement()
		{
		}

		/// <summary>Retorna se o <see cref="T:System.Windows.Media.Animation.Clock" /> tem ou não sua própria fonte de tempo externa, a qual pode exigir a sincronização com o sistema de tempo.</summary>
		/// <returns>Retorna <see langword="true" /> se o <see cref="T:System.Windows.Media.Animation.Clock" /> tem sua própria fonte externa para o tempo, o que pode exigir a sincronização com o sistema de tempo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060034E1 RID: 13537 RVA: 0x000D1C2C File Offset: 0x000D102C
		protected virtual bool GetCanSlip()
		{
			return false;
		}

		/// <summary>Obtém a hora atual deste relógio dentro de sua iteração atual.</summary>
		/// <returns>A hora atual deste relógio se ele está ativo ou preenchendo; caso contrário, <see cref="F:System.TimeSpan.Zero" />.</returns>
		// Token: 0x060034E2 RID: 13538 RVA: 0x000D1C3C File Offset: 0x000D103C
		protected virtual TimeSpan GetCurrentTimeCore()
		{
			if (this._currentTime == null)
			{
				return TimeSpan.Zero;
			}
			return this._currentTime.Value;
		}

		/// <summary>Quando implementado em uma classe derivada, será invocado sempre que um relógio começar, ignorar, pausar, retomar ou então quando o relógio <see cref="P:System.Windows.Media.Animation.ClockController.SpeedRatio" /> for modificado.</summary>
		// Token: 0x060034E3 RID: 13539 RVA: 0x000D1C68 File Offset: 0x000D1068
		protected virtual void SpeedChanged()
		{
		}

		/// <summary>Quando implementado em uma classe derivada, será invocado sempre que um relógio é interrompido usando o método <see cref="M:System.Windows.Media.Animation.ClockController.Stop" />.</summary>
		// Token: 0x060034E4 RID: 13540 RVA: 0x000D1C78 File Offset: 0x000D1078
		protected virtual void Stopped()
		{
		}

		/// <summary>Obtém a hora global atual, conforme estabelecido pelo sistema de tempo WPF.</summary>
		/// <returns>A hora global atual para o WPF sistema de temporização.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000D1C88 File Offset: 0x000D1088
		protected TimeSpan CurrentGlobalTime
		{
			get
			{
				if (this._timeManager == null)
				{
					return TimeSpan.Zero;
				}
				if (this.IsTimeManager)
				{
					return this._timeManager.InternalCurrentGlobalTime;
				}
				Clock clock = this;
				while (!clock.IsRoot)
				{
					clock = clock._parent;
				}
				if (clock.HasDesiredFrameRate)
				{
					return clock._rootData.CurrentAdjustedGlobalTime;
				}
				return this._timeManager.InternalCurrentGlobalTime;
			}
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000D1CEC File Offset: 0x000D10EC
		internal virtual void AddNullPointToCurrentIntervals()
		{
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000D1CFC File Offset: 0x000D10FC
		internal static Clock AllocateClock(Timeline timeline, bool hasControllableRoot)
		{
			Clock clock = timeline.AllocateClock();
			ClockGroup clockGroup = clock as ClockGroup;
			if (clock._parent != null || (clockGroup != null && clockGroup.InternalChildren != null))
			{
				throw new InvalidOperationException(SR.Get("Timing_CreateClockMustReturnNewClock", new object[]
				{
					timeline.GetType().Name
				}));
			}
			clock.SetFlag(Clock.ClockFlags.HasControllableRoot, hasControllableRoot);
			return clock;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000D1D5C File Offset: 0x000D115C
		internal virtual void BuildClockSubTreeFromTimeline(Timeline timeline, bool hasControllableRoot)
		{
			this.SetFlag(Clock.ClockFlags.CanSlip, this.GetCanSlip());
			if (this.CanSlip && (this.IsRoot || this._timeline.BeginTime != null))
			{
				this.ResolveDuration();
				if (!this._resolvedDuration.HasTimeSpan || this._resolvedDuration.TimeSpan > TimeSpan.Zero)
				{
					if (this._timeline.AutoReverse || this._timeline.AccelerationRatio > 0.0 || this._timeline.DecelerationRatio > 0.0)
					{
						throw new NotSupportedException(SR.Get("Timing_CanSlipOnlyOnSimpleTimelines"));
					}
					this._syncData = new Clock.SyncData(this);
					this.HasDescendantsWithUnresolvedDuration = !this.HasResolvedDuration;
					for (Clock parent = this._parent; parent != null; parent = parent._parent)
					{
						if (parent._timeline.AutoReverse || parent._timeline.AccelerationRatio > 0.0 || parent._timeline.DecelerationRatio > 0.0)
						{
							throw new InvalidOperationException(SR.Get("Timing_SlipBehavior_SyncOnlyWithSimpleParents"));
						}
						parent.SetFlag(Clock.ClockFlags.CanGrow, true);
						if (!this.HasResolvedDuration)
						{
							parent.HasDescendantsWithUnresolvedDuration = true;
						}
						parent._currentIterationBeginTime = parent._beginTime;
					}
				}
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000D1EBC File Offset: 0x000D12BC
		internal static Clock BuildClockTreeFromTimeline(Timeline rootTimeline, bool hasControllableRoot)
		{
			Clock clock = Clock.AllocateClock(rootTimeline, hasControllableRoot);
			clock.IsRoot = true;
			clock._rootData = new Clock.RootData();
			clock.BuildClockSubTreeFromTimeline(clock.Timeline, hasControllableRoot);
			clock.AddToTimeManager();
			return clock;
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000D1EF8 File Offset: 0x000D12F8
		internal virtual void ClearCurrentIntervalsToNull()
		{
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000D1F08 File Offset: 0x000D1308
		internal void ClipNextTickByParent()
		{
			if (!this.IsTimeManager && !this._parent.IsTimeManager && (this.InternalNextTickNeededTime == null || (this._parent.InternalNextTickNeededTime != null && this._parent.InternalNextTickNeededTime.Value < this.InternalNextTickNeededTime.Value)))
			{
				this.InternalNextTickNeededTime = this._parent.InternalNextTickNeededTime;
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000D1F88 File Offset: 0x000D1388
		internal virtual void ComputeCurrentIntervals(TimeIntervalCollection parentIntervalCollection, TimeSpan beginTime, TimeSpan? endTime, Duration fillDuration, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000D1F98 File Offset: 0x000D1398
		internal virtual void ComputeCurrentFillInterval(TimeIntervalCollection parentIntervalCollection, TimeSpan beginTime, TimeSpan endTime, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000D1FA8 File Offset: 0x000D13A8
		internal void ComputeLocalState()
		{
			ClockState currentClockState = this._currentClockState;
			TimeSpan? currentTime = this._currentTime;
			double? currentGlobalSpeed = this._currentGlobalSpeed;
			double? currentProgress = this._currentProgress;
			int? currentIteration = this._currentIteration;
			this.PauseStateChangedDuringTick = false;
			this.ComputeLocalStateHelper(true, false);
			if (currentClockState != this._currentClockState)
			{
				this.RaiseCurrentStateInvalidated();
				this.RaiseCurrentGlobalSpeedInvalidated();
				this.RaiseCurrentTimeInvalidated();
			}
			double? currentGlobalSpeed2 = this._currentGlobalSpeed;
			double? num = currentGlobalSpeed;
			if (!(currentGlobalSpeed2.GetValueOrDefault() == num.GetValueOrDefault() & currentGlobalSpeed2 != null == (num != null)))
			{
				this.RaiseCurrentGlobalSpeedInvalidated();
			}
			if (this.HasDiscontinuousTimeMovementOccured)
			{
				this.DiscontinuousTimeMovement();
				this.HasDiscontinuousTimeMovementOccured = false;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000D204C File Offset: 0x000D144C
		internal virtual Duration CurrentDuration
		{
			get
			{
				return Duration.Automatic;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000D2060 File Offset: 0x000D1460
		internal void InternalBegin()
		{
			this.InternalSeek(TimeSpan.Zero);
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000D2078 File Offset: 0x000D1478
		internal double InternalGetSpeedRatio()
		{
			return this._rootData.InteractiveSpeedRatio;
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000D2090 File Offset: 0x000D1490
		internal void InternalPause()
		{
			if (this.PendingInteractiveResume)
			{
				this.PendingInteractiveResume = false;
			}
			else if (!this.IsInteractivelyPaused)
			{
				this.PendingInteractivePause = true;
			}
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000D20C4 File Offset: 0x000D14C4
		internal void InternalRemove()
		{
			this.PendingInteractiveRemove = true;
			this.InternalStop();
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000D20E0 File Offset: 0x000D14E0
		internal void InternalResume()
		{
			if (this.PendingInteractivePause)
			{
				this.PendingInteractivePause = false;
			}
			else if (this.IsInteractivelyPaused)
			{
				this.PendingInteractiveResume = true;
			}
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000D2114 File Offset: 0x000D1514
		internal void InternalSeek(TimeSpan destination)
		{
			this.IsInteractivelyStopped = false;
			this.PendingInteractiveStop = false;
			this.ResetNodesWithSlip();
			this._rootData.PendingSeekDestination = new TimeSpan?(destination);
			this.RootBeginPending = false;
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000D2154 File Offset: 0x000D1554
		internal void InternalSeekAlignedToLastTick(TimeSpan destination)
		{
			if (this._timeManager == null || this.HasDescendantsWithUnresolvedDuration)
			{
				return;
			}
			this._beginTime = new TimeSpan?(this.CurrentGlobalTime - this.DivideTimeSpan(destination, this._appliedSpeedRatio));
			if (this.CanGrow)
			{
				this._currentIteration = null;
				this._currentIterationBeginTime = this._beginTime;
				this.ResetSlipOnSubtree();
				this.UpdateSyncBeginTime();
			}
			this.IsInteractivelyStopped = false;
			this.PendingInteractiveStop = false;
			this.RootBeginPending = false;
			this.ResetNodesWithSlip();
			this._timeManager.InternalCurrentIntervals = TimeIntervalCollection.Empty;
			PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock = prefixSubtreeEnumerator.Current;
				clock.ComputeLocalStateHelper(false, true);
				if (this.HasDiscontinuousTimeMovementOccured)
				{
					this.DiscontinuousTimeMovement();
					this.HasDiscontinuousTimeMovementOccured = false;
				}
				prefixSubtreeEnumerator.Current.ClipNextTickByParent();
				prefixSubtreeEnumerator.Current.NeedsPostfixTraversal = (prefixSubtreeEnumerator.Current is ClockGroup);
			}
			this._parent.ComputeTreeStateRoot();
			prefixSubtreeEnumerator.Reset();
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock2 = prefixSubtreeEnumerator.Current;
				clock2.CurrentTimeInvalidatedEventRaised = true;
				prefixSubtreeEnumerator.Current.CurrentStateInvalidatedEventRaised = true;
				prefixSubtreeEnumerator.Current.CurrentGlobalSpeedInvalidatedEventRaised = true;
				prefixSubtreeEnumerator.Current.RaiseAccumulatedEvents();
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000D229C File Offset: 0x000D169C
		internal void InternalSetSpeedRatio(double ratio)
		{
			this._rootData.PendingSpeedRatio = new double?(ratio);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000D22BC File Offset: 0x000D16BC
		internal void InternalSkipToFill()
		{
			TimeSpan? timeSpan = this.ComputeEffectiveDuration();
			if (timeSpan == null)
			{
				throw new InvalidOperationException(SR.Get("Timing_SkipToFillDestinationIndefinite"));
			}
			this.IsInteractivelyStopped = false;
			this.PendingInteractiveStop = false;
			this.ResetNodesWithSlip();
			this.RootBeginPending = false;
			this._rootData.PendingSeekDestination = new TimeSpan?(timeSpan.Value);
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000D2324 File Offset: 0x000D1724
		internal void InternalStop()
		{
			this.PendingInteractiveStop = true;
			this._rootData.PendingSeekDestination = null;
			this.RootBeginPending = false;
			this.ResetNodesWithSlip();
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000D2360 File Offset: 0x000D1760
		internal void RaiseAccumulatedEvents()
		{
			try
			{
				if (this.CurrentTimeInvalidatedEventRaised)
				{
					this.FireCurrentTimeInvalidatedEvent();
				}
				if (this.CurrentGlobalSpeedInvalidatedEventRaised)
				{
					this.FireCurrentGlobalSpeedInvalidatedEvent();
					this.SpeedChanged();
				}
				if (this.CurrentStateInvalidatedEventRaised)
				{
					this.FireCurrentStateInvalidatedEvent();
					if (!this.CurrentGlobalSpeedInvalidatedEventRaised)
					{
						this.DiscontinuousTimeMovement();
					}
				}
				if (this.CompletedEventRaised)
				{
					this.FireCompletedEvent();
				}
				if (this.RemoveRequestedEventRaised)
				{
					this.FireRemoveRequestedEvent();
				}
			}
			finally
			{
				this.CurrentTimeInvalidatedEventRaised = false;
				this.CurrentGlobalSpeedInvalidatedEventRaised = false;
				this.CurrentStateInvalidatedEventRaised = false;
				this.CompletedEventRaised = false;
				this.RemoveRequestedEventRaised = false;
				this.IsInEventQueue = false;
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000D2410 File Offset: 0x000D1810
		internal void RaiseCompleted()
		{
			this.CompletedEventRaised = true;
			if (!this.IsInEventQueue)
			{
				this._timeManager.AddToEventQueue(this);
				this.IsInEventQueue = true;
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000D2440 File Offset: 0x000D1840
		internal void RaiseCurrentGlobalSpeedInvalidated()
		{
			this.CurrentGlobalSpeedInvalidatedEventRaised = true;
			if (!this.IsInEventQueue)
			{
				this._timeManager.AddToEventQueue(this);
				this.IsInEventQueue = true;
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000D2470 File Offset: 0x000D1870
		internal void RaiseCurrentStateInvalidated()
		{
			if (this._currentClockState == ClockState.Stopped)
			{
				this.Stopped();
			}
			this.CurrentStateInvalidatedEventRaised = true;
			if (!this.IsInEventQueue)
			{
				this._timeManager.AddToEventQueue(this);
				this.IsInEventQueue = true;
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000D24B0 File Offset: 0x000D18B0
		internal void RaiseCurrentTimeInvalidated()
		{
			this.CurrentTimeInvalidatedEventRaised = true;
			if (!this.IsInEventQueue)
			{
				this._timeManager.AddToEventQueue(this);
				this.IsInEventQueue = true;
			}
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000D24E0 File Offset: 0x000D18E0
		internal void RaiseRemoveRequested()
		{
			this.RemoveRequestedEventRaised = true;
			if (!this.IsInEventQueue)
			{
				this._timeManager.AddToEventQueue(this);
				this.IsInEventQueue = true;
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000D2510 File Offset: 0x000D1910
		internal void ResetCachedStateToStopped()
		{
			this._currentGlobalSpeed = null;
			this._currentIteration = null;
			this.IsBackwardsProgressingGlobal = false;
			this._currentProgress = null;
			this._currentTime = null;
			this._currentClockState = ClockState.Stopped;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000D255C File Offset: 0x000D195C
		internal virtual void ResetNodesWithSlip()
		{
			if (this._syncData != null)
			{
				this._syncData.IsInSyncPeriod = false;
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000D2580 File Offset: 0x000D1980
		internal virtual void UpdateDescendantsWithUnresolvedDuration()
		{
			if (this.HasResolvedDuration)
			{
				this.HasDescendantsWithUnresolvedDuration = false;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000D259C File Offset: 0x000D199C
		internal int Depth
		{
			get
			{
				return this._depth;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000D25B0 File Offset: 0x000D19B0
		internal Duration EndOfActivePeriod
		{
			get
			{
				if (!this.HasResolvedDuration)
				{
					return Duration.Automatic;
				}
				TimeSpan? timeSpan;
				this.ComputeExpirationTime(out timeSpan);
				if (timeSpan != null)
				{
					return timeSpan.Value;
				}
				return Duration.Forever;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000D25F0 File Offset: 0x000D19F0
		internal virtual Clock FirstChild
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000D2600 File Offset: 0x000D1A00
		// (set) Token: 0x06003507 RID: 13575 RVA: 0x000D2614 File Offset: 0x000D1A14
		internal ClockState InternalCurrentClockState
		{
			get
			{
				return this._currentClockState;
			}
			set
			{
				this._currentClockState = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000D2628 File Offset: 0x000D1A28
		// (set) Token: 0x06003509 RID: 13577 RVA: 0x000D263C File Offset: 0x000D1A3C
		internal double? InternalCurrentGlobalSpeed
		{
			get
			{
				return this._currentGlobalSpeed;
			}
			set
			{
				this._currentGlobalSpeed = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000D2650 File Offset: 0x000D1A50
		// (set) Token: 0x0600350B RID: 13579 RVA: 0x000D2664 File Offset: 0x000D1A64
		internal int? InternalCurrentIteration
		{
			get
			{
				return this._currentIteration;
			}
			set
			{
				this._currentIteration = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000D2678 File Offset: 0x000D1A78
		// (set) Token: 0x0600350D RID: 13581 RVA: 0x000D268C File Offset: 0x000D1A8C
		internal double? InternalCurrentProgress
		{
			get
			{
				return this._currentProgress;
			}
			set
			{
				this._currentProgress = value;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000D26A0 File Offset: 0x000D1AA0
		// (set) Token: 0x0600350F RID: 13583 RVA: 0x000D26B4 File Offset: 0x000D1AB4
		internal TimeSpan? InternalNextTickNeededTime
		{
			get
			{
				return this._nextTickNeededTime;
			}
			set
			{
				this._nextTickNeededTime = value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000D26C8 File Offset: 0x000D1AC8
		internal ClockGroup InternalParent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000D26DC File Offset: 0x000D1ADC
		internal Duration ResolvedDuration
		{
			get
			{
				this.ResolveDuration();
				return this._resolvedDuration;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000D26F8 File Offset: 0x000D1AF8
		internal Clock NextSibling
		{
			get
			{
				List<Clock> internalChildren = this._parent.InternalChildren;
				if (this._childIndex == internalChildren.Count - 1)
				{
					return null;
				}
				return internalChildren[this._childIndex + 1];
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000D2734 File Offset: 0x000D1B34
		internal WeakReference WeakReference
		{
			get
			{
				WeakReference weakReference = this._weakReference;
				if (weakReference == null)
				{
					weakReference = new WeakReference(this);
					this._weakReference = weakReference;
				}
				return weakReference;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000D275C File Offset: 0x000D1B5C
		internal int? DesiredFrameRate
		{
			get
			{
				int? result = null;
				if (this.HasDesiredFrameRate)
				{
					result = new int?(this._rootData.DesiredFrameRate);
				}
				return result;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000D278C File Offset: 0x000D1B8C
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000D27A4 File Offset: 0x000D1BA4
		internal bool CompletedEventRaised
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CompletedEventRaised);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.CompletedEventRaised, value);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000D27C0 File Offset: 0x000D1BC0
		// (set) Token: 0x06003518 RID: 13592 RVA: 0x000D27D8 File Offset: 0x000D1BD8
		internal bool CurrentGlobalSpeedInvalidatedEventRaised
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CurrentGlobalSpeedInvalidatedEventRaised);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.CurrentGlobalSpeedInvalidatedEventRaised, value);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000D27F4 File Offset: 0x000D1BF4
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000D280C File Offset: 0x000D1C0C
		internal bool CurrentStateInvalidatedEventRaised
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CurrentStateInvalidatedEventRaised);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.CurrentStateInvalidatedEventRaised, value);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000D2828 File Offset: 0x000D1C28
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x000D2840 File Offset: 0x000D1C40
		internal bool CurrentTimeInvalidatedEventRaised
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.CurrentTimeInvalidatedEventRaised);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.CurrentTimeInvalidatedEventRaised, value);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000D285C File Offset: 0x000D1C5C
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x000D2874 File Offset: 0x000D1C74
		private bool HasDesiredFrameRate
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasDesiredFrameRate);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.HasDesiredFrameRate, value);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000D2890 File Offset: 0x000D1C90
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000D28A8 File Offset: 0x000D1CA8
		internal bool HasResolvedDuration
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasResolvedDuration);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.HasResolvedDuration, value);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000D28C4 File Offset: 0x000D1CC4
		// (set) Token: 0x06003522 RID: 13602 RVA: 0x000D28D8 File Offset: 0x000D1CD8
		internal bool IsBackwardsProgressingGlobal
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsBackwardsProgressingGlobal);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsBackwardsProgressingGlobal, value);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000D28F0 File Offset: 0x000D1CF0
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x000D2908 File Offset: 0x000D1D08
		internal bool IsInEventQueue
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsInEventQueue);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsInEventQueue, value);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000D2924 File Offset: 0x000D1D24
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x000D2938 File Offset: 0x000D1D38
		internal bool IsInteractivelyPaused
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsInteractivelyPaused);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsInteractivelyPaused, value);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000D2950 File Offset: 0x000D1D50
		// (set) Token: 0x06003528 RID: 13608 RVA: 0x000D2968 File Offset: 0x000D1D68
		internal bool IsInteractivelyStopped
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsInteractivelyStopped);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsInteractivelyStopped, value);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000D2980 File Offset: 0x000D1D80
		// (set) Token: 0x0600352A RID: 13610 RVA: 0x000D2994 File Offset: 0x000D1D94
		internal bool IsRoot
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsRoot);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsRoot, value);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000D29AC File Offset: 0x000D1DAC
		// (set) Token: 0x0600352C RID: 13612 RVA: 0x000D29C0 File Offset: 0x000D1DC0
		internal bool IsTimeManager
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.IsTimeManager);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.IsTimeManager, value);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000D29D8 File Offset: 0x000D1DD8
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x000D29F0 File Offset: 0x000D1DF0
		internal bool NeedsPostfixTraversal
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.NeedsPostfixTraversal);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.NeedsPostfixTraversal, value);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000D2A0C File Offset: 0x000D1E0C
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000D2A24 File Offset: 0x000D1E24
		internal virtual bool NeedsTicksWhenActive
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.NeedsTicksWhenActive);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.NeedsTicksWhenActive, value);
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000D2A40 File Offset: 0x000D1E40
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000D2A58 File Offset: 0x000D1E58
		internal bool PauseStateChangedDuringTick
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.PauseStateChangedDuringTick);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.PauseStateChangedDuringTick, value);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000D2A74 File Offset: 0x000D1E74
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000D2A8C File Offset: 0x000D1E8C
		internal bool PendingInteractivePause
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.PendingInteractivePause);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.PendingInteractivePause, value);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000D2AA4 File Offset: 0x000D1EA4
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000D2ABC File Offset: 0x000D1EBC
		internal bool PendingInteractiveRemove
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.PendingInteractiveRemove);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.PendingInteractiveRemove, value);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000D2AD8 File Offset: 0x000D1ED8
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000D2AF0 File Offset: 0x000D1EF0
		internal bool PendingInteractiveResume
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.PendingInteractiveResume);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.PendingInteractiveResume, value);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000D2B08 File Offset: 0x000D1F08
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x000D2B20 File Offset: 0x000D1F20
		internal bool PendingInteractiveStop
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.PendingInteractiveStop);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.PendingInteractiveStop, value);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000D2B3C File Offset: 0x000D1F3C
		// (set) Token: 0x0600353C RID: 13628 RVA: 0x000D2B54 File Offset: 0x000D1F54
		internal bool RemoveRequestedEventRaised
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.RemoveRequestedEventRaised);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.RemoveRequestedEventRaised, value);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000D2B70 File Offset: 0x000D1F70
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x000D2B88 File Offset: 0x000D1F88
		private bool HasDiscontinuousTimeMovementOccured
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasDiscontinuousTimeMovementOccured);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.HasDiscontinuousTimeMovementOccured, value);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000D2BA4 File Offset: 0x000D1FA4
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000D2BBC File Offset: 0x000D1FBC
		internal bool HasDescendantsWithUnresolvedDuration
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasDescendantsWithUnresolvedDuration);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.HasDescendantsWithUnresolvedDuration, value);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000D2BD8 File Offset: 0x000D1FD8
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000D2BF0 File Offset: 0x000D1FF0
		private bool HasSeekOccuredAfterLastTick
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.HasSeekOccuredAfterLastTick);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.HasSeekOccuredAfterLastTick, value);
			}
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000D2C0C File Offset: 0x000D200C
		private void AdjustBeginTime()
		{
			if (this._rootData.PendingSeekDestination != null && !this.HasDescendantsWithUnresolvedDuration)
			{
				this._beginTime = new TimeSpan?(this.CurrentGlobalTime - this.DivideTimeSpan(this._rootData.PendingSeekDestination.Value, this._appliedSpeedRatio));
				if (this.CanGrow)
				{
					this._currentIterationBeginTime = this._beginTime;
					this._currentIteration = null;
					this.ResetSlipOnSubtree();
				}
				this.UpdateSyncBeginTime();
				this._rootData.PendingSeekDestination = null;
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					Clock clock = prefixSubtreeEnumerator.Current;
					clock.RaiseCurrentStateInvalidated();
					prefixSubtreeEnumerator.Current.RaiseCurrentTimeInvalidated();
					prefixSubtreeEnumerator.Current.RaiseCurrentGlobalSpeedInvalidated();
				}
			}
			else if (this.RootBeginPending)
			{
				this._beginTime = this.CurrentGlobalTime + this._timeline.BeginTime;
				if (this.CanGrow)
				{
					this._currentIterationBeginTime = this._beginTime;
				}
				this.UpdateSyncBeginTime();
				this.RootBeginPending = false;
			}
			else if ((this.IsInteractivelyPaused || this._rootData.InteractiveSpeedRatio == 0.0) && (this._syncData == null || !this._syncData.IsInSyncPeriod) && this._beginTime != null)
			{
				this._beginTime += this._timeManager.LastTickDelta;
				this.UpdateSyncBeginTime();
				if (this._currentIterationBeginTime != null)
				{
					this._currentIterationBeginTime += this._timeManager.LastTickDelta;
				}
			}
			if (this._rootData.PendingSpeedRatio != null)
			{
				double num = this._rootData.PendingSpeedRatio.Value * this._timeline.SpeedRatio;
				if (num == 0.0)
				{
					num = 1.0;
				}
				TimeSpan currentGlobalTime = this.CurrentGlobalTime;
				if (this._currentIterationBeginTime != null)
				{
					this._currentIterationBeginTime = new TimeSpan?(currentGlobalTime - Clock.MultiplyTimeSpan(currentGlobalTime - this._currentIterationBeginTime.Value, this._appliedSpeedRatio / num));
				}
				else
				{
					this._beginTime = new TimeSpan?(currentGlobalTime - Clock.MultiplyTimeSpan(currentGlobalTime - this._beginTime.Value, this._appliedSpeedRatio / num));
				}
				this.RaiseCurrentGlobalSpeedInvalidated();
				this._appliedSpeedRatio = num;
				this._rootData.InteractiveSpeedRatio = this._rootData.PendingSpeedRatio.Value;
				this._rootData.PendingSpeedRatio = null;
				this.UpdateSyncBeginTime();
			}
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000D2F44 File Offset: 0x000D2344
		internal void ApplyDesiredFrameRateToGlobalTime()
		{
			if (this.HasDesiredFrameRate)
			{
				this._rootData.LastAdjustedGlobalTime = this._rootData.CurrentAdjustedGlobalTime;
				this._rootData.CurrentAdjustedGlobalTime = this.GetCurrentDesiredFrameTime(this._timeManager.InternalCurrentGlobalTime);
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000D2F8C File Offset: 0x000D238C
		internal void ApplyDesiredFrameRateToNextTick()
		{
			if (this.HasDesiredFrameRate && this.InternalNextTickNeededTime != null)
			{
				TimeSpan? internalNextTickNeededTime = this.InternalNextTickNeededTime;
				TimeSpan zero = TimeSpan.Zero;
				TimeSpan time = (internalNextTickNeededTime != null && (internalNextTickNeededTime == null || internalNextTickNeededTime.GetValueOrDefault() == zero)) ? this._rootData.CurrentAdjustedGlobalTime : this.InternalNextTickNeededTime.Value;
				this.InternalNextTickNeededTime = new TimeSpan?(this.GetNextDesiredFrameTime(time));
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000D3014 File Offset: 0x000D2414
		private bool ComputeCurrentIteration(TimeSpan parentTime, double parentSpeed, TimeSpan? expirationTime, out TimeSpan localProgress)
		{
			RepeatBehavior repeatBehavior = this._timeline.RepeatBehavior;
			TimeSpan t = (this._currentIterationBeginTime != null) ? this._currentIterationBeginTime.Value : this._beginTime.Value;
			TimeSpan timeSpan = Clock.MultiplyTimeSpan(parentTime - t, this._appliedSpeedRatio);
			this.IsBackwardsProgressingGlobal = this._parent.IsBackwardsProgressingGlobal;
			if (this._currentDuration.HasTimeSpan)
			{
				if (this._currentDuration.TimeSpan == TimeSpan.Zero)
				{
					localProgress = TimeSpan.Zero;
					this._currentTime = new TimeSpan?(TimeSpan.Zero);
					double num;
					if (repeatBehavior.HasCount)
					{
						double count = repeatBehavior.Count;
						if (count <= 1.0)
						{
							num = count;
							this._currentIteration = new int?(1);
						}
						else
						{
							double num2 = (double)((int)count);
							if (count == num2)
							{
								num = 1.0;
								this._currentIteration = new int?((int)count);
							}
							else
							{
								num = count - num2;
								this._currentIteration = new int?((int)(count + 1.0));
							}
						}
					}
					else
					{
						this._currentIteration = new int?(1);
						num = 1.0;
					}
					if (this._timeline.AutoReverse)
					{
						if (num == 1.0)
						{
							num = 0.0;
						}
						else if (num < 0.5)
						{
							num *= 2.0;
						}
						else
						{
							num = 1.0 - (num - 0.5) * 2.0;
						}
					}
					this._currentProgress = new double?(num);
					return true;
				}
				if (this._currentClockState == ClockState.Filling && repeatBehavior.HasCount && this._currentIterationBeginTime == null)
				{
					double num3 = repeatBehavior.Count;
					if (this._timeline.AutoReverse)
					{
						num3 *= 2.0;
					}
					TimeSpan timeSpan2 = Clock.MultiplyTimeSpan(this._resolvedDuration.TimeSpan, num3);
					timeSpan = timeSpan2;
				}
				int num4;
				if (this._currentIterationBeginTime != null)
				{
					this.ComputeCurrentIterationWithGrow(parentTime, expirationTime, out localProgress, out num4);
				}
				else
				{
					localProgress = TimeSpan.FromTicks(timeSpan.Ticks % this._currentDuration.TimeSpan.Ticks);
					num4 = (int)(timeSpan.Ticks / this._resolvedDuration.TimeSpan.Ticks);
				}
				if (localProgress == TimeSpan.Zero && num4 > 0 && (this._currentClockState == ClockState.Filling || this._parent.IsBackwardsProgressingGlobal))
				{
					localProgress = this._currentDuration.TimeSpan;
					num4--;
				}
				if (this._timeline.AutoReverse)
				{
					if ((num4 & 1) == 1)
					{
						if (localProgress == TimeSpan.Zero)
						{
							this.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
						}
						localProgress = this._currentDuration.TimeSpan - localProgress;
						this.IsBackwardsProgressingGlobal = !this.IsBackwardsProgressingGlobal;
						parentSpeed = -parentSpeed;
					}
					num4 /= 2;
				}
				this._currentIteration = new int?(1 + num4);
				if (this._currentClockState == ClockState.Active && parentSpeed != 0.0 && !this.NeedsTicksWhenActive)
				{
					TimeSpan t2;
					if (localProgress == TimeSpan.Zero)
					{
						t2 = this.DivideTimeSpan(this._currentDuration.TimeSpan, Math.Abs(parentSpeed));
					}
					else if (parentSpeed > 0.0)
					{
						TimeSpan t3 = Clock.MultiplyTimeSpan(this._currentDuration.TimeSpan, 1.0 - this._timeline.DecelerationRatio);
						t2 = this.DivideTimeSpan(t3 - localProgress, parentSpeed);
					}
					else
					{
						TimeSpan t4 = Clock.MultiplyTimeSpan(this._currentDuration.TimeSpan, this._timeline.AccelerationRatio);
						t2 = this.DivideTimeSpan(t4 - localProgress, parentSpeed);
					}
					TimeSpan timeSpan3 = this.CurrentGlobalTime + t2;
					if (this.InternalNextTickNeededTime == null || timeSpan3 < this.InternalNextTickNeededTime.Value)
					{
						this.InternalNextTickNeededTime = new TimeSpan?(timeSpan3);
					}
				}
			}
			else
			{
				localProgress = timeSpan;
				this._currentIteration = new int?(1);
			}
			return false;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000D345C File Offset: 0x000D285C
		private void ComputeCurrentIterationWithGrow(TimeSpan parentTime, TimeSpan? expirationTime, out TimeSpan localProgress, out int newIteration)
		{
			TimeSpan timeSpan = Clock.MultiplyTimeSpan(parentTime - this._currentIterationBeginTime.Value, this._appliedSpeedRatio);
			int num;
			if (timeSpan < this._currentDuration.TimeSpan)
			{
				localProgress = timeSpan;
				num = 0;
			}
			else
			{
				long ticks = (timeSpan - this._currentDuration.TimeSpan).Ticks;
				localProgress = TimeSpan.FromTicks(ticks % this._resolvedDuration.TimeSpan.Ticks);
				num = 1 + (int)(ticks / this._resolvedDuration.TimeSpan.Ticks);
				this._currentIterationBeginTime += this._currentDuration.TimeSpan + Clock.MultiplyTimeSpan(this._resolvedDuration.TimeSpan, (double)(num - 1));
				if (this._currentClockState == ClockState.Filling && expirationTime != null && this._currentIterationBeginTime >= expirationTime)
				{
					if (num > 1)
					{
						this._currentIterationBeginTime -= this._resolvedDuration.TimeSpan;
					}
					else
					{
						this._currentIterationBeginTime -= this._currentDuration.TimeSpan;
					}
				}
				else
				{
					this.ResetSlipOnSubtree();
				}
			}
			newIteration = ((this._currentIteration != null) ? (num + (this._currentIteration.Value - 1)) : num);
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000D3654 File Offset: 0x000D2A54
		private bool ComputeCurrentState(TimeSpan? expirationTime, ref TimeSpan parentTime, double parentSpeed, bool isInTick)
		{
			FillBehavior fillBehavior = this._timeline.FillBehavior;
			if (parentTime < this._beginTime)
			{
				this.ResetCachedStateToStopped();
				return true;
			}
			if (expirationTime != null && parentTime >= expirationTime)
			{
				this.RaiseCompletedForRoot(isInTick);
				if (fillBehavior != FillBehavior.HoldEnd)
				{
					this.ResetCachedStateToStopped();
					return true;
				}
				this.ResetCachedStateToFilling();
				parentTime = expirationTime.Value;
			}
			else
			{
				this._currentClockState = ClockState.Active;
			}
			if (parentSpeed != 0.0 && this._currentClockState == ClockState.Active && this.NeedsTicksWhenActive)
			{
				this.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
			}
			return false;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000D3728 File Offset: 0x000D2B28
		private bool ComputeCurrentSpeed(double localSpeed)
		{
			if (this.IsInteractivelyPaused)
			{
				this._currentGlobalSpeed = new double?(0.0);
			}
			else
			{
				localSpeed *= this._appliedSpeedRatio;
				if (this.IsBackwardsProgressingGlobal)
				{
					localSpeed = -localSpeed;
				}
				this._currentGlobalSpeed = localSpeed * this._parent._currentGlobalSpeed;
			}
			return false;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000D37A0 File Offset: 0x000D2BA0
		private bool ComputeCurrentTime(TimeSpan localProgress, out double localSpeed)
		{
			if (this._currentDuration.HasTimeSpan)
			{
				double accelerationRatio = this._timeline.AccelerationRatio;
				double decelerationRatio = this._timeline.DecelerationRatio;
				double num = accelerationRatio + decelerationRatio;
				double num2 = (double)this._currentDuration.TimeSpan.Ticks;
				double num3 = (double)localProgress.Ticks / num2;
				if (num == 0.0)
				{
					localSpeed = 1.0;
					this._currentTime = new TimeSpan?(localProgress);
				}
				else
				{
					double num4 = 2.0 / (2.0 - num);
					if (num3 < accelerationRatio)
					{
						localSpeed = num4 * num3 / accelerationRatio;
						num3 = num4 * num3 * num3 / (2.0 * accelerationRatio);
						if (this._currentClockState == ClockState.Active && this._parent._currentClockState == ClockState.Active)
						{
							this.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
						}
					}
					else if (num3 <= 1.0 - decelerationRatio)
					{
						localSpeed = num4;
						num3 = num4 * (num3 - accelerationRatio / 2.0);
					}
					else
					{
						double num5 = 1.0 - num3;
						localSpeed = num4 * num5 / decelerationRatio;
						num3 = 1.0 - num4 * num5 * num5 / (2.0 * decelerationRatio);
						if (this._currentClockState == ClockState.Active && this._parent._currentClockState == ClockState.Active)
						{
							this.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
						}
					}
					this._currentTime = new TimeSpan?(TimeSpan.FromTicks((long)(num3 * num2 + 0.5)));
				}
				this._currentProgress = new double?(num3);
			}
			else
			{
				this._currentTime = new TimeSpan?(localProgress);
				this._currentProgress = new double?(0.0);
				localSpeed = 1.0;
			}
			return this._currentClockState > ClockState.Active;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000D3968 File Offset: 0x000D2D68
		private void ResolveDuration()
		{
			if (!this.HasResolvedDuration)
			{
				Duration naturalDuration = this.NaturalDuration;
				if (naturalDuration != Duration.Automatic)
				{
					this._resolvedDuration = naturalDuration;
					this._currentDuration = naturalDuration;
					this.HasResolvedDuration = true;
				}
			}
			if (this.CanGrow)
			{
				this._currentDuration = this.CurrentDuration;
				if (this._currentDuration == Duration.Automatic)
				{
					this._currentDuration = Duration.Forever;
				}
			}
			if (this.HasDescendantsWithUnresolvedDuration)
			{
				this.UpdateDescendantsWithUnresolvedDuration();
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000D39E8 File Offset: 0x000D2DE8
		private TimeSpan? ComputeEffectiveDuration()
		{
			this.ResolveDuration();
			RepeatBehavior repeatBehavior = this._timeline.RepeatBehavior;
			TimeSpan? result;
			if (this._currentDuration.HasTimeSpan && this._currentDuration.TimeSpan == TimeSpan.Zero)
			{
				result = new TimeSpan?(TimeSpan.Zero);
			}
			else if (repeatBehavior.HasCount)
			{
				if (repeatBehavior.Count == 0.0)
				{
					result = new TimeSpan?(TimeSpan.Zero);
				}
				else if (this._currentDuration == Duration.Forever)
				{
					result = null;
				}
				else if (!this.CanGrow)
				{
					double num = repeatBehavior.Count / this._appliedSpeedRatio;
					if (this._timeline.AutoReverse)
					{
						num *= 2.0;
					}
					result = new TimeSpan?(Clock.MultiplyTimeSpan(this._currentDuration.TimeSpan, num));
				}
				else
				{
					TimeSpan t = TimeSpan.Zero;
					double num2 = repeatBehavior.Count;
					if (this.CanGrow && this._currentIterationBeginTime != null && this._currentIteration != null)
					{
						num2 -= (double)(this._currentIteration.Value - 1);
						t = this._currentIterationBeginTime.Value - this._beginTime.Value;
					}
					double num3;
					if (num2 <= 1.0)
					{
						num3 = (double)this._currentDuration.TimeSpan.Ticks * num2;
					}
					else
					{
						num3 = (double)this._currentDuration.TimeSpan.Ticks + (double)this._resolvedDuration.TimeSpan.Ticks * (num2 - 1.0);
					}
					if (this._timeline.AutoReverse)
					{
						num3 *= 2.0;
					}
					result = new TimeSpan?(TimeSpan.FromTicks((long)(num3 / this._appliedSpeedRatio + 0.5)) + t);
				}
			}
			else if (repeatBehavior.HasDuration)
			{
				result = new TimeSpan?(repeatBehavior.Duration);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000D3BF8 File Offset: 0x000D2FF8
		private void ComputeEvents(TimeSpan? expirationTime, TimeIntervalCollection parentIntervalCollection)
		{
			this.ClearCurrentIntervalsToNull();
			if (this._beginTime != null && !(this.IsInteractivelyPaused ^ this.PauseStateChangedDuringTick))
			{
				Duration duration;
				if (expirationTime != null)
				{
					duration = Duration.Forever;
				}
				else
				{
					duration = TimeSpan.Zero;
				}
				if (expirationTime == null || expirationTime >= this._beginTime)
				{
					TimeIntervalCollection timeIntervalCollection;
					if (expirationTime != null)
					{
						if (expirationTime == this._beginTime)
						{
							timeIntervalCollection = TimeIntervalCollection.Empty;
						}
						else
						{
							timeIntervalCollection = TimeIntervalCollection.CreateClosedOpenInterval(this._beginTime.Value, expirationTime.Value);
						}
					}
					else
					{
						timeIntervalCollection = TimeIntervalCollection.CreateInfiniteClosedInterval(this._beginTime.Value);
					}
					if (parentIntervalCollection.Intersects(timeIntervalCollection))
					{
						this.ComputeIntervalsWithParentIntersection(parentIntervalCollection, timeIntervalCollection, expirationTime, duration);
					}
					else if (duration != TimeSpan.Zero && this._timeline.FillBehavior == FillBehavior.HoldEnd)
					{
						this.ComputeIntervalsWithHoldEnd(parentIntervalCollection, expirationTime);
					}
				}
			}
			if (this.PendingInteractiveRemove)
			{
				this.RaiseRemoveRequestedForRoot();
				this.RaiseCompletedForRoot(true);
				this.PendingInteractiveRemove = false;
			}
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000D3D5C File Offset: 0x000D315C
		private bool ComputeExpirationTime(out TimeSpan? expirationTime)
		{
			if (this._beginTime == null)
			{
				expirationTime = null;
				return true;
			}
			TimeSpan? timeSpan = this.ComputeEffectiveDuration();
			if (timeSpan != null)
			{
				expirationTime = this._beginTime + timeSpan;
				if (this._syncData != null && this._syncData.IsInSyncPeriod && !this._syncData.SyncClockHasReachedEffectiveDuration)
				{
					expirationTime += TimeSpan.FromMilliseconds(50.0);
				}
			}
			else
			{
				expirationTime = null;
			}
			return false;
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000D3E48 File Offset: 0x000D3248
		private bool ComputeInteractiveValues()
		{
			bool result = false;
			if (this.PendingInteractiveStop)
			{
				this.PendingInteractiveStop = false;
				this.IsInteractivelyStopped = true;
				this._beginTime = null;
				this._currentIterationBeginTime = null;
				if (this.CanGrow)
				{
					this.ResetSlipOnSubtree();
				}
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					Clock clock = prefixSubtreeEnumerator.Current;
					if (clock._currentClockState != ClockState.Stopped)
					{
						clock.ResetCachedStateToStopped();
						clock.RaiseCurrentStateInvalidated();
						clock.RaiseCurrentTimeInvalidated();
						clock.RaiseCurrentGlobalSpeedInvalidated();
					}
					else
					{
						prefixSubtreeEnumerator.SkipSubtree();
					}
				}
			}
			if (this.IsInteractivelyStopped)
			{
				this.ResetCachedStateToStopped();
				this.InternalNextTickNeededTime = null;
				result = true;
			}
			else
			{
				this.AdjustBeginTime();
			}
			if (this.PendingInteractivePause)
			{
				this.PendingInteractivePause = false;
				this.RaiseCurrentGlobalSpeedInvalidated();
				PrefixSubtreeEnumerator prefixSubtreeEnumerator2 = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator2.MoveNext())
				{
					Clock clock2 = prefixSubtreeEnumerator2.Current;
					clock2.IsInteractivelyPaused = true;
					prefixSubtreeEnumerator2.Current.PauseStateChangedDuringTick = true;
				}
			}
			if (this.PendingInteractiveResume)
			{
				this.PendingInteractiveResume = false;
				if (this._currentClockState != ClockState.Filling)
				{
					this.RaiseCurrentGlobalSpeedInvalidated();
				}
				PrefixSubtreeEnumerator prefixSubtreeEnumerator3 = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator3.MoveNext())
				{
					Clock clock3 = prefixSubtreeEnumerator3.Current;
					clock3.IsInteractivelyPaused = false;
					prefixSubtreeEnumerator3.Current.PauseStateChangedDuringTick = true;
				}
			}
			return result;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000D3F94 File Offset: 0x000D3394
		private void ComputeIntervalsWithHoldEnd(TimeIntervalCollection parentIntervalCollection, TimeSpan? endOfActivePeriod)
		{
			TimeIntervalCollection other = TimeIntervalCollection.CreateInfiniteClosedInterval(endOfActivePeriod.Value);
			if (parentIntervalCollection.Intersects(other))
			{
				TimeSpan beginTime = (this._currentIterationBeginTime != null) ? this._currentIterationBeginTime.Value : this._beginTime.Value;
				this.ComputeCurrentFillInterval(parentIntervalCollection, beginTime, endOfActivePeriod.Value, this._currentDuration, this._appliedSpeedRatio, this._timeline.AccelerationRatio, this._timeline.DecelerationRatio, this._timeline.AutoReverse);
				if (parentIntervalCollection.IntersectsInverseOf(other))
				{
					this.RaiseCurrentStateInvalidated();
					this.RaiseCurrentTimeInvalidated();
					this.RaiseCurrentGlobalSpeedInvalidated();
					this.AddNullPointToCurrentIntervals();
				}
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000D4040 File Offset: 0x000D3440
		private void ComputeIntervalsWithParentIntersection(TimeIntervalCollection parentIntervalCollection, TimeIntervalCollection activePeriod, TimeSpan? endOfActivePeriod, Duration postFillDuration)
		{
			TimeSpan beginTime = (this._currentIterationBeginTime != null) ? this._currentIterationBeginTime.Value : this._beginTime.Value;
			this.RaiseCurrentTimeInvalidated();
			if (parentIntervalCollection.IntersectsInverseOf(activePeriod))
			{
				this.RaiseCurrentStateInvalidated();
				this.RaiseCurrentGlobalSpeedInvalidated();
			}
			else if (parentIntervalCollection.IntersectsPeriodicCollection(beginTime, this._currentDuration, this._appliedSpeedRatio, this._timeline.AccelerationRatio, this._timeline.DecelerationRatio, this._timeline.AutoReverse))
			{
				this.RaiseCurrentGlobalSpeedInvalidated();
			}
			else if (parentIntervalCollection.IntersectsMultiplePeriods(beginTime, this._currentDuration, this._appliedSpeedRatio))
			{
				this.HasDiscontinuousTimeMovementOccured = true;
				if (this._syncData != null)
				{
					this._syncData.SyncClockDiscontinuousEvent = true;
				}
			}
			this.ComputeCurrentIntervals(parentIntervalCollection, beginTime, endOfActivePeriod, postFillDuration, this._currentDuration, this._appliedSpeedRatio, this._timeline.AccelerationRatio, this._timeline.DecelerationRatio, this._timeline.AutoReverse);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000D413C File Offset: 0x000D353C
		private void ComputeLocalStateHelper(bool performTickOperations, bool seekedAlignedToLastTick)
		{
			bool flag = false;
			TimeSpan? timeSpan;
			double? num;
			TimeIntervalCollection parentIntervalCollection;
			if (this.ComputeParentParameters(out timeSpan, out num, out parentIntervalCollection, seekedAlignedToLastTick))
			{
				flag = true;
			}
			if (this._syncData != null && this._syncData.IsInSyncPeriod && this._parent.CurrentState != ClockState.Stopped)
			{
				this.ComputeSyncSlip(ref parentIntervalCollection, timeSpan.Value, num.Value);
			}
			this.ResolveDuration();
			if (performTickOperations && this.IsRoot && this.ComputeInteractiveValues())
			{
				flag = true;
			}
			if (this._syncData != null && !this._syncData.IsInSyncPeriod && this._parent.CurrentState != ClockState.Stopped && (!parentIntervalCollection.IsEmptyOfRealPoints || this.HasSeekOccuredAfterLastTick))
			{
				this.ComputeSyncEnter(ref parentIntervalCollection, timeSpan.Value);
			}
			TimeSpan? expirationTime;
			if (this.ComputeExpirationTime(out expirationTime))
			{
				flag = true;
			}
			if (performTickOperations)
			{
				this.ComputeEvents(expirationTime, parentIntervalCollection);
			}
			if (flag)
			{
				return;
			}
			TimeSpan value = timeSpan.Value;
			if (this.ComputeNextTickNeededTime(expirationTime, value, num.Value))
			{
				return;
			}
			if (this.ComputeCurrentState(expirationTime, ref value, num.Value, performTickOperations))
			{
				return;
			}
			TimeSpan localProgress;
			if (this.ComputeCurrentIteration(value, num.Value, expirationTime, out localProgress))
			{
				return;
			}
			double localSpeed;
			if (this.ComputeCurrentTime(localProgress, out localSpeed))
			{
				return;
			}
			this.ComputeCurrentSpeed(localSpeed);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000D4268 File Offset: 0x000D3668
		private bool ComputeNextTickNeededTime(TimeSpan? expirationTime, TimeSpan parentTime, double parentSpeed)
		{
			if (parentSpeed == 0.0)
			{
				this.InternalNextTickNeededTime = (this.IsInteractivelyPaused ? new TimeSpan?(TimeSpan.Zero) : null);
			}
			else
			{
				double factor = 1.0 / parentSpeed;
				TimeSpan? timeSpan = null;
				TimeSpan timeSpan2 = Clock.MultiplyTimeSpan(this._beginTime.Value - parentTime, factor);
				if (timeSpan2 >= TimeSpan.Zero)
				{
					timeSpan = new TimeSpan?(timeSpan2);
				}
				if (expirationTime != null)
				{
					TimeSpan timeSpan3 = Clock.MultiplyTimeSpan(expirationTime.Value - parentTime, factor);
					if (timeSpan3 >= TimeSpan.Zero && (timeSpan == null || timeSpan3 < timeSpan.Value))
					{
						timeSpan = new TimeSpan?(timeSpan3);
					}
				}
				if (timeSpan != null)
				{
					this.InternalNextTickNeededTime = this.CurrentGlobalTime + timeSpan;
				}
				else
				{
					this.InternalNextTickNeededTime = null;
				}
			}
			return false;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000D4388 File Offset: 0x000D3788
		private bool ComputeParentParameters(out TimeSpan? parentTime, out double? parentSpeed, out TimeIntervalCollection parentIntervalCollection, bool seekedAlignedToLastTick)
		{
			if (this.IsRoot)
			{
				this.HasSeekOccuredAfterLastTick = (seekedAlignedToLastTick || this._rootData.PendingSeekDestination != null);
				if (this._timeManager == null || this._timeManager.InternalIsStopped)
				{
					this.ResetCachedStateToStopped();
					parentTime = null;
					parentSpeed = null;
					this.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
					parentIntervalCollection = TimeIntervalCollection.Empty;
					return true;
				}
				parentSpeed = new double?(1.0);
				parentIntervalCollection = this._timeManager.InternalCurrentIntervals;
				if (this.HasDesiredFrameRate)
				{
					parentTime = new TimeSpan?(this._rootData.CurrentAdjustedGlobalTime);
					if (!parentIntervalCollection.IsEmptyOfRealPoints)
					{
						parentIntervalCollection = parentIntervalCollection.SetBeginningOfConnectedInterval(this._rootData.LastAdjustedGlobalTime);
					}
				}
				else
				{
					parentTime = new TimeSpan?(this._timeManager.InternalCurrentGlobalTime);
				}
				return false;
			}
			else
			{
				this.HasSeekOccuredAfterLastTick = (seekedAlignedToLastTick || this._parent.HasSeekOccuredAfterLastTick);
				parentTime = this._parent._currentTime;
				parentSpeed = this._parent._currentGlobalSpeed;
				parentIntervalCollection = this._parent.CurrentIntervals;
				if (this._parent._currentClockState != ClockState.Stopped)
				{
					return false;
				}
				if (this._currentClockState != ClockState.Stopped)
				{
					this.RaiseCurrentStateInvalidated();
					this.RaiseCurrentGlobalSpeedInvalidated();
					this.RaiseCurrentTimeInvalidated();
				}
				this.ResetCachedStateToStopped();
				this.InternalNextTickNeededTime = null;
				return true;
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000D4510 File Offset: 0x000D3910
		private void ComputeSyncEnter(ref TimeIntervalCollection parentIntervalCollection, TimeSpan currentParentTimePT)
		{
			if (this._beginTime != null && currentParentTimePT >= this._beginTime.Value)
			{
				TimeSpan t = (this._currentIterationBeginTime != null) ? this._currentIterationBeginTime.Value : this._beginTime.Value;
				TimeSpan timeSpan = currentParentTimePT - t;
				TimeSpan timeSpan2 = Clock.MultiplyTimeSpan(timeSpan, this._appliedSpeedRatio);
				if (this._syncData.SyncClock == this || timeSpan2 >= this._syncData.SyncClockBeginTime)
				{
					if (this.HasSeekOccuredAfterLastTick)
					{
						TimeSpan? timeSpan3;
						this.ComputeExpirationTime(out timeSpan3);
						if (timeSpan3 == null || currentParentTimePT < timeSpan3.Value)
						{
							TimeSpan timeSpan4 = (this._syncData.SyncClock == this) ? timeSpan2 : Clock.MultiplyTimeSpan(timeSpan2 - this._syncData.SyncClockBeginTime, this._syncData.SyncClockSpeedRatio);
							TimeSpan? syncClockEffectiveDuration = this._syncData.SyncClockEffectiveDuration;
							if (this._syncData.SyncClock == this || syncClockEffectiveDuration == null || timeSpan4 < syncClockEffectiveDuration)
							{
								Duration syncClockResolvedDuration = this._syncData.SyncClockResolvedDuration;
								if (syncClockResolvedDuration.HasTimeSpan)
								{
									this._syncData.PreviousSyncClockTime = TimeSpan.FromTicks(timeSpan4.Ticks % syncClockResolvedDuration.TimeSpan.Ticks);
									this._syncData.PreviousRepeatTime = timeSpan4 - this._syncData.PreviousSyncClockTime;
								}
								else
								{
									if (!(syncClockResolvedDuration == Duration.Forever))
									{
										throw new InvalidOperationException(SR.Get("Timing_SeekDestinationAmbiguousDueToSlip"));
									}
									this._syncData.PreviousSyncClockTime = timeSpan4;
									this._syncData.PreviousRepeatTime = TimeSpan.Zero;
								}
								this._syncData.IsInSyncPeriod = true;
								return;
							}
						}
					}
					else
					{
						TimeSpan? timeSpan5 = (this._syncData.SyncClock == this) ? new TimeSpan?(parentIntervalCollection.FirstNodeTime) : this._currentTime;
						if (timeSpan5 == null || this._syncData.SyncClockDiscontinuousEvent || timeSpan5.Value <= this._syncData.SyncClockBeginTime)
						{
							TimeSpan timeSpan6 = timeSpan;
							if (this._syncData.SyncClock != this)
							{
								timeSpan6 -= this.DivideTimeSpan(this._syncData.SyncClockBeginTime, this._appliedSpeedRatio);
							}
							if (this._currentIterationBeginTime != null)
							{
								this._currentIterationBeginTime += timeSpan6;
							}
							else
							{
								this._beginTime += timeSpan6;
							}
							this.UpdateSyncBeginTime();
							parentIntervalCollection = parentIntervalCollection.SlipBeginningOfConnectedInterval(timeSpan6);
							this._syncData.IsInSyncPeriod = true;
							this._syncData.PreviousSyncClockTime = TimeSpan.Zero;
							this._syncData.PreviousRepeatTime = TimeSpan.Zero;
							this._syncData.SyncClockDiscontinuousEvent = false;
						}
					}
				}
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000D4840 File Offset: 0x000D3C40
		private void ComputeSyncSlip(ref TimeIntervalCollection parentIntervalCollection, TimeSpan currentParentTimePT, double currentParentSpeed)
		{
			TimeSpan t = parentIntervalCollection.IsEmptyOfRealPoints ? currentParentTimePT : parentIntervalCollection.FirstNodeTime;
			TimeSpan timeSpan = currentParentTimePT - t;
			TimeSpan timeSpan2 = Clock.MultiplyTimeSpan(timeSpan, this._appliedSpeedRatio);
			TimeSpan currentTimeCore = this._syncData.SyncClock.GetCurrentTimeCore();
			TimeSpan timeSpan3 = currentTimeCore - this._syncData.PreviousSyncClockTime;
			if (timeSpan3 > TimeSpan.Zero)
			{
				TimeSpan? syncClockEffectiveDuration = this._syncData.SyncClockEffectiveDuration;
				Duration syncClockResolvedDuration = this._syncData.SyncClockResolvedDuration;
				if (syncClockEffectiveDuration != null && this._syncData.PreviousRepeatTime + currentTimeCore >= syncClockEffectiveDuration.Value)
				{
					this._syncData.IsInSyncPeriod = false;
					this._syncData.PreviousRepeatTime = TimeSpan.Zero;
					this._syncData.SyncClockDiscontinuousEvent = false;
				}
				else if (syncClockResolvedDuration.HasTimeSpan && currentTimeCore >= syncClockResolvedDuration.TimeSpan)
				{
					this._syncData.PreviousSyncClockTime = TimeSpan.Zero;
					this._syncData.PreviousRepeatTime += syncClockResolvedDuration.TimeSpan;
				}
				else
				{
					this._syncData.PreviousSyncClockTime = currentTimeCore;
				}
			}
			else
			{
				timeSpan3 = TimeSpan.Zero;
			}
			TimeSpan timeSpan4 = (this._syncData.SyncClock == this) ? timeSpan3 : this.DivideTimeSpan(timeSpan3, this._syncData.SyncClockSpeedRatio);
			TimeSpan timeSpan5 = timeSpan - this.DivideTimeSpan(timeSpan4, this._appliedSpeedRatio);
			if (this._currentIterationBeginTime != null)
			{
				this._currentIterationBeginTime += timeSpan5;
			}
			else
			{
				this._beginTime += timeSpan5;
			}
			this.UpdateSyncBeginTime();
			parentIntervalCollection = parentIntervalCollection.SlipBeginningOfConnectedInterval(timeSpan5);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000D4A40 File Offset: 0x000D3E40
		private void ResetSlipOnSubtree()
		{
			PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, false);
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock = prefixSubtreeEnumerator.Current;
				if (clock._syncData != null)
				{
					clock._syncData.IsInSyncPeriod = false;
					clock._syncData.SyncClockDiscontinuousEvent = true;
				}
				if (clock.CanSlip)
				{
					clock._beginTime = clock._timeline.BeginTime;
					clock._currentIteration = null;
					clock.UpdateSyncBeginTime();
					clock.HasDiscontinuousTimeMovementOccured = true;
				}
				else if (clock.CanGrow)
				{
					clock._currentIterationBeginTime = clock._beginTime;
					clock._currentDuration = clock._resolvedDuration;
				}
				else
				{
					prefixSubtreeEnumerator.SkipSubtree();
				}
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000D4AEC File Offset: 0x000D3EEC
		private void AddEventHandler(EventPrivateKey key, Delegate handler)
		{
			if (this._eventHandlersStore == null)
			{
				this._eventHandlersStore = new EventHandlersStore();
			}
			this._eventHandlersStore.Add(key, handler);
			this.VerifyNeedsTicksWhenActive();
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000D4B20 File Offset: 0x000D3F20
		private void FireCompletedEvent()
		{
			this.FireEvent(Timeline.CompletedKey);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000D4B38 File Offset: 0x000D3F38
		private void FireCurrentGlobalSpeedInvalidatedEvent()
		{
			this.FireEvent(Timeline.CurrentGlobalSpeedInvalidatedKey);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000D4B50 File Offset: 0x000D3F50
		private void FireCurrentStateInvalidatedEvent()
		{
			this.FireEvent(Timeline.CurrentStateInvalidatedKey);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000D4B68 File Offset: 0x000D3F68
		private void FireCurrentTimeInvalidatedEvent()
		{
			this.FireEvent(Timeline.CurrentTimeInvalidatedKey);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000D4B80 File Offset: 0x000D3F80
		private void FireEvent(EventPrivateKey key)
		{
			if (this._eventHandlersStore != null)
			{
				EventHandler eventHandler = (EventHandler)this._eventHandlersStore.Get(key);
				if (eventHandler != null)
				{
					eventHandler(this, null);
				}
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000D4BB4 File Offset: 0x000D3FB4
		private void FireRemoveRequestedEvent()
		{
			this.FireEvent(Timeline.RemoveRequestedKey);
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000D4BCC File Offset: 0x000D3FCC
		private TimeSpan GetCurrentDesiredFrameTime(TimeSpan time)
		{
			return this.GetDesiredFrameTime(time, 0);
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000D4BE4 File Offset: 0x000D3FE4
		private TimeSpan GetDesiredFrameTime(TimeSpan time, int frameOffset)
		{
			long num = (long)this._rootData.DesiredFrameRate;
			long num2 = time.Ticks * num / Clock.s_TimeSpanTicksPerSecond + (long)frameOffset;
			long value = num2 * Clock.s_TimeSpanTicksPerSecond / num;
			return TimeSpan.FromTicks(value);
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000D4C24 File Offset: 0x000D4024
		private TimeSpan GetNextDesiredFrameTime(TimeSpan time)
		{
			return this.GetDesiredFrameTime(time, 1);
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000D4C3C File Offset: 0x000D403C
		private void RemoveEventHandler(EventPrivateKey key, Delegate handler)
		{
			if (this._eventHandlersStore != null)
			{
				this._eventHandlersStore.Remove(key, handler);
				if (this._eventHandlersStore.Count == 0)
				{
					this._eventHandlersStore = null;
				}
			}
			this.UpdateNeedsTicksWhenActive();
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000D4C78 File Offset: 0x000D4078
		private void AddToTimeManager()
		{
			TimeManager timeManager = MediaContext.From(base.Dispatcher).TimeManager;
			if (timeManager == null)
			{
				return;
			}
			this._parent = timeManager.TimeManagerClock;
			this.SetTimeManager(this._parent._timeManager);
			int? desiredFrameRate = Timeline.GetDesiredFrameRate(this._timeline);
			if (desiredFrameRate != null)
			{
				this.HasDesiredFrameRate = true;
				this._rootData.DesiredFrameRate = desiredFrameRate.Value;
			}
			this._parent.InternalRootChildren.Add(this.WeakReference);
			this._subtreeFinalizer = new Clock.SubtreeFinalizer(this._timeManager);
			PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock = prefixSubtreeEnumerator.Current;
				clock._depth = clock._parent._depth + 1;
			}
			if (this.IsInTimingTree)
			{
				this._timeManager.SetDirty();
			}
			TimeIntervalCollection internalCurrentIntervals = TimeIntervalCollection.CreatePoint(this._timeManager.InternalCurrentGlobalTime);
			internalCurrentIntervals.AddNullPoint();
			this._timeManager.InternalCurrentIntervals = internalCurrentIntervals;
			this._beginTime = null;
			this._currentIterationBeginTime = null;
			prefixSubtreeEnumerator.Reset();
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock2 = prefixSubtreeEnumerator.Current;
				clock2.ComputeLocalState();
				prefixSubtreeEnumerator.Current.ClipNextTickByParent();
				prefixSubtreeEnumerator.Current.NeedsPostfixTraversal = (prefixSubtreeEnumerator.Current is ClockGroup);
			}
			this._parent.ComputeTreeStateRoot();
			if (this._timeline.BeginTime != null)
			{
				this.RootBeginPending = true;
			}
			this.NotifyNewEarliestFutureActivity();
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000D4E00 File Offset: 0x000D4200
		private TimeSpan DivideTimeSpan(TimeSpan timeSpan, double factor)
		{
			return TimeSpan.FromTicks((long)((double)timeSpan.Ticks / factor + 0.5));
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000D4E28 File Offset: 0x000D4228
		private bool GetFlag(Clock.ClockFlags flagMask)
		{
			return (this._flags & flagMask) == flagMask;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000D4E40 File Offset: 0x000D4240
		private static TimeSpan MultiplyTimeSpan(TimeSpan timeSpan, double factor)
		{
			return TimeSpan.FromTicks((long)(factor * (double)timeSpan.Ticks + 0.5));
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000D4E68 File Offset: 0x000D4268
		private void NotifyNewEarliestFutureActivity()
		{
			PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock = prefixSubtreeEnumerator.Current;
				clock.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
			}
			for (Clock parent = this._parent; parent != null; parent = parent._parent)
			{
				TimeSpan? internalNextTickNeededTime = parent.InternalNextTickNeededTime;
				TimeSpan zero = TimeSpan.Zero;
				if (internalNextTickNeededTime != null && (internalNextTickNeededTime == null || !(internalNextTickNeededTime.GetValueOrDefault() != zero)))
				{
					break;
				}
				parent.InternalNextTickNeededTime = new TimeSpan?(TimeSpan.Zero);
				if (parent.IsTimeManager)
				{
					this._timeManager.NotifyNewEarliestFutureActivity();
					break;
				}
			}
			if (this._timeManager != null)
			{
				this._timeManager.SetDirty();
			}
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000D4F20 File Offset: 0x000D4320
		private void ResetCachedStateToFilling()
		{
			this._currentGlobalSpeed = new double?(0.0);
			this.IsBackwardsProgressingGlobal = false;
			this._currentClockState = ClockState.Filling;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000D4F50 File Offset: 0x000D4350
		private void RaiseCompletedForRoot(bool isInTick)
		{
			if (this.IsRoot && (this.CurrentStateInvalidatedEventRaised || !isInTick))
			{
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					Clock clock = prefixSubtreeEnumerator.Current;
					clock.RaiseCompleted();
				}
			}
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000D4F90 File Offset: 0x000D4390
		private void RaiseRemoveRequestedForRoot()
		{
			PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
			while (prefixSubtreeEnumerator.MoveNext())
			{
				Clock clock = prefixSubtreeEnumerator.Current;
				clock.RaiseRemoveRequested();
			}
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000D4FC0 File Offset: 0x000D43C0
		private void SetFlag(Clock.ClockFlags flagMask, bool value)
		{
			if (value)
			{
				this._flags |= flagMask;
				return;
			}
			this._flags &= ~flagMask;
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000D4FF0 File Offset: 0x000D43F0
		private void SetTimeManager(TimeManager timeManager)
		{
			if (this._timeManager != timeManager)
			{
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(this, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					Clock clock = prefixSubtreeEnumerator.Current;
					clock._timeManager = timeManager;
				}
				if (timeManager != null)
				{
					prefixSubtreeEnumerator.Reset();
					while (prefixSubtreeEnumerator.MoveNext())
					{
						Clock clock2 = prefixSubtreeEnumerator.Current;
					}
				}
			}
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000D5044 File Offset: 0x000D4444
		private void UpdateNeedsTicksWhenActive()
		{
			if (this._eventHandlersStore == null)
			{
				this.NeedsTicksWhenActive = false;
				return;
			}
			this.NeedsTicksWhenActive = true;
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000D5068 File Offset: 0x000D4468
		private void UpdateSyncBeginTime()
		{
			if (this._syncData != null)
			{
				this._syncData.UpdateClockBeginTime();
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000D5088 File Offset: 0x000D4488
		private void VerifyNeedsTicksWhenActive()
		{
			if (!this.NeedsTicksWhenActive)
			{
				this.NeedsTicksWhenActive = true;
				this.NotifyNewEarliestFutureActivity();
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003570 RID: 13680 RVA: 0x000D50AC File Offset: 0x000D44AC
		private bool IsInTimingTree
		{
			get
			{
				return this._timeManager != null && this._timeManager.State > TimeState.Stopped;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000D50D4 File Offset: 0x000D44D4
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000D50EC File Offset: 0x000D44EC
		private bool RootBeginPending
		{
			get
			{
				return this.GetFlag(Clock.ClockFlags.RootBeginPending);
			}
			set
			{
				this.SetFlag(Clock.ClockFlags.RootBeginPending, value);
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000D5108 File Offset: 0x000D4508
		[Conditional("DEBUG")]
		private void Debug_VerifyOffsetFromBegin(long inputTime, long optimizedInputTime)
		{
			long num = (long)Math.Max(this._appliedSpeedRatio, 1.0);
		}

		// Token: 0x04001620 RID: 5664
		private Clock.ClockFlags _flags;

		// Token: 0x04001621 RID: 5665
		private int? _currentIteration;

		// Token: 0x04001622 RID: 5666
		private double? _currentProgress;

		// Token: 0x04001623 RID: 5667
		private double? _currentGlobalSpeed;

		// Token: 0x04001624 RID: 5668
		private TimeSpan? _currentTime;

		// Token: 0x04001625 RID: 5669
		private ClockState _currentClockState;

		// Token: 0x04001626 RID: 5670
		private Clock.RootData _rootData;

		// Token: 0x04001627 RID: 5671
		internal Clock.SyncData _syncData;

		// Token: 0x04001628 RID: 5672
		internal TimeSpan? _beginTime;

		// Token: 0x04001629 RID: 5673
		private TimeSpan? _currentIterationBeginTime;

		// Token: 0x0400162A RID: 5674
		internal TimeSpan? _nextTickNeededTime;

		// Token: 0x0400162B RID: 5675
		private WeakReference _weakReference;

		// Token: 0x0400162C RID: 5676
		private Clock.SubtreeFinalizer _subtreeFinalizer;

		// Token: 0x0400162D RID: 5677
		private EventHandlersStore _eventHandlersStore;

		// Token: 0x0400162E RID: 5678
		internal Duration _resolvedDuration;

		// Token: 0x0400162F RID: 5679
		internal Duration _currentDuration;

		// Token: 0x04001630 RID: 5680
		private double _appliedSpeedRatio;

		// Token: 0x04001631 RID: 5681
		internal Timeline _timeline;

		// Token: 0x04001632 RID: 5682
		internal TimeManager _timeManager;

		// Token: 0x04001633 RID: 5683
		internal ClockGroup _parent;

		// Token: 0x04001634 RID: 5684
		internal int _childIndex;

		// Token: 0x04001635 RID: 5685
		internal int _depth;

		// Token: 0x04001636 RID: 5686
		private static long s_TimeSpanTicksPerSecond = TimeSpan.FromSeconds(1.0).Ticks;

		// Token: 0x020008B1 RID: 2225
		[Flags]
		private enum ClockFlags : uint
		{
			// Token: 0x04002919 RID: 10521
			IsTimeManager = 1U,
			// Token: 0x0400291A RID: 10522
			IsRoot = 2U,
			// Token: 0x0400291B RID: 10523
			IsBackwardsProgressingGlobal = 4U,
			// Token: 0x0400291C RID: 10524
			IsInteractivelyPaused = 8U,
			// Token: 0x0400291D RID: 10525
			IsInteractivelyStopped = 16U,
			// Token: 0x0400291E RID: 10526
			PendingInteractivePause = 32U,
			// Token: 0x0400291F RID: 10527
			PendingInteractiveResume = 64U,
			// Token: 0x04002920 RID: 10528
			PendingInteractiveStop = 128U,
			// Token: 0x04002921 RID: 10529
			PendingInteractiveRemove = 256U,
			// Token: 0x04002922 RID: 10530
			CanGrow = 512U,
			// Token: 0x04002923 RID: 10531
			CanSlip = 1024U,
			// Token: 0x04002924 RID: 10532
			CurrentStateInvalidatedEventRaised = 2048U,
			// Token: 0x04002925 RID: 10533
			CurrentTimeInvalidatedEventRaised = 4096U,
			// Token: 0x04002926 RID: 10534
			CurrentGlobalSpeedInvalidatedEventRaised = 8192U,
			// Token: 0x04002927 RID: 10535
			CompletedEventRaised = 16384U,
			// Token: 0x04002928 RID: 10536
			RemoveRequestedEventRaised = 32768U,
			// Token: 0x04002929 RID: 10537
			IsInEventQueue = 65536U,
			// Token: 0x0400292A RID: 10538
			NeedsTicksWhenActive = 131072U,
			// Token: 0x0400292B RID: 10539
			NeedsPostfixTraversal = 262144U,
			// Token: 0x0400292C RID: 10540
			PauseStateChangedDuringTick = 524288U,
			// Token: 0x0400292D RID: 10541
			RootBeginPending = 1048576U,
			// Token: 0x0400292E RID: 10542
			HasControllableRoot = 2097152U,
			// Token: 0x0400292F RID: 10543
			HasResolvedDuration = 4194304U,
			// Token: 0x04002930 RID: 10544
			HasDesiredFrameRate = 8388608U,
			// Token: 0x04002931 RID: 10545
			HasDiscontinuousTimeMovementOccured = 16777216U,
			// Token: 0x04002932 RID: 10546
			HasDescendantsWithUnresolvedDuration = 33554432U,
			// Token: 0x04002933 RID: 10547
			HasSeekOccuredAfterLastTick = 67108864U
		}

		// Token: 0x020008B2 RID: 2226
		private class SubtreeFinalizer
		{
			// Token: 0x0600589D RID: 22685 RVA: 0x0016836C File Offset: 0x0016776C
			internal SubtreeFinalizer(TimeManager timeManager)
			{
				this._timeManager = timeManager;
			}

			// Token: 0x0600589E RID: 22686 RVA: 0x00168388 File Offset: 0x00167788
			~SubtreeFinalizer()
			{
				this._timeManager.ScheduleClockCleanup();
			}

			// Token: 0x04002934 RID: 10548
			private TimeManager _timeManager;
		}

		// Token: 0x020008B3 RID: 2227
		internal class SyncData
		{
			// Token: 0x0600589F RID: 22687 RVA: 0x001683C8 File Offset: 0x001677C8
			internal SyncData(Clock syncClock)
			{
				this._syncClock = syncClock;
				this.UpdateClockBeginTime();
			}

			// Token: 0x060058A0 RID: 22688 RVA: 0x001683F4 File Offset: 0x001677F4
			internal void UpdateClockBeginTime()
			{
				this._syncClockBeginTime = this._syncClock._beginTime;
				this._syncClockSpeedRatio = this._syncClock._appliedSpeedRatio;
				this._syncClockResolvedDuration = this.SyncClockResolvedDuration;
			}

			// Token: 0x17001246 RID: 4678
			// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00168430 File Offset: 0x00167830
			internal Clock SyncClock
			{
				get
				{
					return this._syncClock;
				}
			}

			// Token: 0x17001247 RID: 4679
			// (get) Token: 0x060058A2 RID: 22690 RVA: 0x00168444 File Offset: 0x00167844
			internal Duration SyncClockResolvedDuration
			{
				get
				{
					if (!this._syncClockResolvedDuration.HasTimeSpan)
					{
						this._syncClockEffectiveDuration = this._syncClock.ComputeEffectiveDuration();
						this._syncClockResolvedDuration = this._syncClock._resolvedDuration;
					}
					return this._syncClockResolvedDuration;
				}
			}

			// Token: 0x17001248 RID: 4680
			// (get) Token: 0x060058A3 RID: 22691 RVA: 0x00168488 File Offset: 0x00167888
			internal bool SyncClockHasReachedEffectiveDuration
			{
				get
				{
					return this._syncClockEffectiveDuration != null && this._previousRepeatTime + this._syncClock.GetCurrentTimeCore() >= this._syncClockEffectiveDuration.Value;
				}
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x060058A4 RID: 22692 RVA: 0x001684CC File Offset: 0x001678CC
			internal TimeSpan? SyncClockEffectiveDuration
			{
				get
				{
					return this._syncClockEffectiveDuration;
				}
			}

			// Token: 0x1700124A RID: 4682
			// (get) Token: 0x060058A5 RID: 22693 RVA: 0x001684E0 File Offset: 0x001678E0
			internal double SyncClockSpeedRatio
			{
				get
				{
					return this._syncClockSpeedRatio;
				}
			}

			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x060058A6 RID: 22694 RVA: 0x001684F4 File Offset: 0x001678F4
			// (set) Token: 0x060058A7 RID: 22695 RVA: 0x00168508 File Offset: 0x00167908
			internal bool IsInSyncPeriod
			{
				get
				{
					return this._isInSyncPeriod;
				}
				set
				{
					this._isInSyncPeriod = value;
				}
			}

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x060058A8 RID: 22696 RVA: 0x0016851C File Offset: 0x0016791C
			// (set) Token: 0x060058A9 RID: 22697 RVA: 0x00168530 File Offset: 0x00167930
			internal bool SyncClockDiscontinuousEvent
			{
				get
				{
					return this._syncClockDiscontinuousEvent;
				}
				set
				{
					this._syncClockDiscontinuousEvent = value;
				}
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x060058AA RID: 22698 RVA: 0x00168544 File Offset: 0x00167944
			// (set) Token: 0x060058AB RID: 22699 RVA: 0x00168558 File Offset: 0x00167958
			internal TimeSpan PreviousSyncClockTime
			{
				get
				{
					return this._previousSyncClockTime;
				}
				set
				{
					this._previousSyncClockTime = value;
				}
			}

			// Token: 0x1700124E RID: 4686
			// (get) Token: 0x060058AC RID: 22700 RVA: 0x0016856C File Offset: 0x0016796C
			// (set) Token: 0x060058AD RID: 22701 RVA: 0x00168580 File Offset: 0x00167980
			internal TimeSpan PreviousRepeatTime
			{
				get
				{
					return this._previousRepeatTime;
				}
				set
				{
					this._previousRepeatTime = value;
				}
			}

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x060058AE RID: 22702 RVA: 0x00168594 File Offset: 0x00167994
			internal TimeSpan SyncClockBeginTime
			{
				get
				{
					return this._syncClockBeginTime.Value;
				}
			}

			// Token: 0x04002935 RID: 10549
			private Clock _syncClock;

			// Token: 0x04002936 RID: 10550
			private double _syncClockSpeedRatio;

			// Token: 0x04002937 RID: 10551
			private bool _isInSyncPeriod;

			// Token: 0x04002938 RID: 10552
			private bool _syncClockDiscontinuousEvent;

			// Token: 0x04002939 RID: 10553
			private Duration _syncClockResolvedDuration = Duration.Automatic;

			// Token: 0x0400293A RID: 10554
			private TimeSpan? _syncClockEffectiveDuration;

			// Token: 0x0400293B RID: 10555
			private TimeSpan? _syncClockBeginTime;

			// Token: 0x0400293C RID: 10556
			private TimeSpan _previousSyncClockTime;

			// Token: 0x0400293D RID: 10557
			private TimeSpan _previousRepeatTime;
		}

		// Token: 0x020008B4 RID: 2228
		internal class RootData
		{
			// Token: 0x060058AF RID: 22703 RVA: 0x001685AC File Offset: 0x001679AC
			internal RootData()
			{
			}

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x060058B0 RID: 22704 RVA: 0x001685D0 File Offset: 0x001679D0
			// (set) Token: 0x060058B1 RID: 22705 RVA: 0x001685E4 File Offset: 0x001679E4
			internal TimeSpan CurrentAdjustedGlobalTime
			{
				get
				{
					return this._currentAdjustedGlobalTime;
				}
				set
				{
					this._currentAdjustedGlobalTime = value;
				}
			}

			// Token: 0x17001251 RID: 4689
			// (get) Token: 0x060058B2 RID: 22706 RVA: 0x001685F8 File Offset: 0x001679F8
			// (set) Token: 0x060058B3 RID: 22707 RVA: 0x0016860C File Offset: 0x00167A0C
			internal int DesiredFrameRate
			{
				get
				{
					return this._desiredFrameRate;
				}
				set
				{
					this._desiredFrameRate = value;
				}
			}

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x060058B4 RID: 22708 RVA: 0x00168620 File Offset: 0x00167A20
			// (set) Token: 0x060058B5 RID: 22709 RVA: 0x00168634 File Offset: 0x00167A34
			internal double InteractiveSpeedRatio
			{
				get
				{
					return this._interactiveSpeedRatio;
				}
				set
				{
					this._interactiveSpeedRatio = value;
				}
			}

			// Token: 0x17001253 RID: 4691
			// (get) Token: 0x060058B6 RID: 22710 RVA: 0x00168648 File Offset: 0x00167A48
			// (set) Token: 0x060058B7 RID: 22711 RVA: 0x0016865C File Offset: 0x00167A5C
			internal TimeSpan LastAdjustedGlobalTime
			{
				get
				{
					return this._lastAdjustedGlobalTime;
				}
				set
				{
					this._lastAdjustedGlobalTime = value;
				}
			}

			// Token: 0x17001254 RID: 4692
			// (get) Token: 0x060058B8 RID: 22712 RVA: 0x00168670 File Offset: 0x00167A70
			// (set) Token: 0x060058B9 RID: 22713 RVA: 0x00168684 File Offset: 0x00167A84
			internal TimeSpan? PendingSeekDestination
			{
				get
				{
					return this._pendingSeekDestination;
				}
				set
				{
					this._pendingSeekDestination = value;
				}
			}

			// Token: 0x17001255 RID: 4693
			// (get) Token: 0x060058BA RID: 22714 RVA: 0x00168698 File Offset: 0x00167A98
			// (set) Token: 0x060058BB RID: 22715 RVA: 0x001686AC File Offset: 0x00167AAC
			internal double? PendingSpeedRatio
			{
				get
				{
					return this._pendingSpeedRatio;
				}
				set
				{
					this._pendingSpeedRatio = value;
				}
			}

			// Token: 0x0400293E RID: 10558
			private int _desiredFrameRate;

			// Token: 0x0400293F RID: 10559
			private double _interactiveSpeedRatio = 1.0;

			// Token: 0x04002940 RID: 10560
			private double? _pendingSpeedRatio;

			// Token: 0x04002941 RID: 10561
			private TimeSpan _currentAdjustedGlobalTime;

			// Token: 0x04002942 RID: 10562
			private TimeSpan _lastAdjustedGlobalTime;

			// Token: 0x04002943 RID: 10563
			private TimeSpan? _pendingSeekDestination;
		}
	}
}
