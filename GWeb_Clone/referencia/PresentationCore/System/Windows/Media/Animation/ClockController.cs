using System;
using System.ComponentModel;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Controla interativamente um <see cref="T:System.Windows.Media.Animation.Clock" />.</summary>
	// Token: 0x020004A8 RID: 1192
	public sealed class ClockController : DispatcherObject
	{
		// Token: 0x06003575 RID: 13685 RVA: 0x000D5154 File Offset: 0x000D4554
		internal ClockController(Clock owner)
		{
			this._owner = owner;
		}

		/// <summary>Define o destino <see cref="P:System.Windows.Media.Animation.ClockController.Clock" /> para começar no próximo tique.</summary>
		// Token: 0x06003576 RID: 13686 RVA: 0x000D5170 File Offset: 0x000D4570
		public void Begin()
		{
			this._owner.InternalBegin();
		}

		/// <summary>Adianta a hora atual do <see cref="T:System.Windows.Media.Animation.Clock" /> de destino até o final do respectivo período ativo.</summary>
		// Token: 0x06003577 RID: 13687 RVA: 0x000D5188 File Offset: 0x000D4588
		public void SkipToFill()
		{
			this._owner.InternalSkipToFill();
		}

		/// <summary>Interrompe o andamento do destino <see cref="T:System.Windows.Media.Animation.Clock" />.</summary>
		// Token: 0x06003578 RID: 13688 RVA: 0x000D51A0 File Offset: 0x000D45A0
		public void Pause()
		{
			this._owner.InternalPause();
		}

		/// <summary>Permite a retomada do andamento de um <see cref="T:System.Windows.Media.Animation.Clock" /> colocado anteriormente em pausa.</summary>
		// Token: 0x06003579 RID: 13689 RVA: 0x000D51B8 File Offset: 0x000D45B8
		public void Resume()
		{
			this._owner.InternalResume();
		}

		/// <summary>Procura o <see cref="P:System.Windows.Media.Animation.ClockController.Clock" /> de destino pelo valor especificado quando o próximo tique ocorre. Se o relógio de destino for interrompido, realizar uma busca o ativará novamente.</summary>
		/// <param name="offset">O deslocamento de busca, medido na hora do relógio de destino. Esse deslocamento é relativo ao <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.BeginTime" /> ou <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.Duration" /> do relógio, dependendo do valor de <paramref name="origin" />.</param>
		/// <param name="origin">Um valor que indica se o deslocamento especificado em relação ao <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.BeginTime" /> ou <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.Duration" /> do relógio do destino.</param>
		// Token: 0x0600357A RID: 13690 RVA: 0x000D51D0 File Offset: 0x000D45D0
		public void Seek(TimeSpan offset, TimeSeekOrigin origin)
		{
			if (!TimeEnumHelper.IsValidTimeSeekOrigin(origin))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"TimeSeekOrigin"
				}));
			}
			if (origin == TimeSeekOrigin.Duration)
			{
				Duration resolvedDuration = this._owner.ResolvedDuration;
				if (!resolvedDuration.HasTimeSpan)
				{
					throw new InvalidOperationException(SR.Get("Timing_SeekDestinationIndefinite"));
				}
				offset += resolvedDuration.TimeSpan;
			}
			if (offset < TimeSpan.Zero)
			{
				throw new InvalidOperationException(SR.Get("Timing_SeekDestinationNegative"));
			}
			this._owner.InternalSeek(offset);
		}

		/// <summary>Procura o destino <see cref="T:System.Windows.Media.Animation.Clock" /> pelo valor especificado imediatamente. Se o relógio de destino for interrompido, realizar uma busca o ativará novamente.</summary>
		/// <param name="offset">O deslocamento de busca, medido na hora do relógio de destino. Esse deslocamento é relativo ao <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.BeginTime" /> ou <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.Duration" /> do relógio, dependendo do valor de <paramref name="origin" />.</param>
		/// <param name="origin">Um valor que indica se o deslocamento especificado em relação ao <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.BeginTime" /> ou <see cref="F:System.Windows.Media.Animation.TimeSeekOrigin.Duration" /> do relógio do destino.</param>
		// Token: 0x0600357B RID: 13691 RVA: 0x000D5264 File Offset: 0x000D4664
		public void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin)
		{
			if (!TimeEnumHelper.IsValidTimeSeekOrigin(origin))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"TimeSeekOrigin"
				}));
			}
			if (origin == TimeSeekOrigin.Duration)
			{
				Duration resolvedDuration = this._owner.ResolvedDuration;
				if (!resolvedDuration.HasTimeSpan)
				{
					throw new InvalidOperationException(SR.Get("Timing_SeekDestinationIndefinite"));
				}
				offset += resolvedDuration.TimeSpan;
			}
			if (offset < TimeSpan.Zero)
			{
				throw new InvalidOperationException(SR.Get("Timing_SeekDestinationNegative"));
			}
			this._owner.InternalSeekAlignedToLastTick(offset);
		}

		/// <summary>Interrompe o <see cref="T:System.Windows.Media.Animation.Clock" /> de destino.</summary>
		// Token: 0x0600357C RID: 13692 RVA: 0x000D52F8 File Offset: 0x000D46F8
		public void Stop()
		{
			this._owner.InternalStop();
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Clock" /> associado a este <see cref="T:System.Windows.Media.Animation.ClockController" /> das propriedades que ele anima. O relógio e seus relógios filho não afetarão mais essas propriedades.</summary>
		// Token: 0x0600357D RID: 13693 RVA: 0x000D5310 File Offset: 0x000D4710
		public void Remove()
		{
			this._owner.InternalRemove();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Animation.Clock" /> controlado por este <see cref="T:System.Windows.Media.Animation.ClockController" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Clock" /> controlado por este <see cref="T:System.Windows.Media.Animation.ClockController" />.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000D5328 File Offset: 0x000D4728
		public Clock Clock
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Obtém ou define a velocidade interativa do <see cref="T:System.Windows.Media.Animation.Clock" /> de destino.</summary>
		/// <returns>Um valor finito maior que zero que descreve a velocidade interativa do relógio de destino. Esse valor é multiplicado contra o valor da <see cref="P:System.Windows.Media.Animation.Timeline.SpeedRatio" /> do relógio do <see cref="T:System.Windows.Media.Animation.Timeline" />. Por exemplo, se a linha do tempo <see cref="P:System.Windows.Media.Animation.Timeline.SpeedRatio" /> é 0,5 e o <see cref="T:System.Windows.Media.Animation.ClockController" /> do objeto <see cref="P:System.Windows.Media.Animation.ClockController.SpeedRatio" /> é 3.0, a linha do tempo se move a velocidade normal de 1,5 vezes (0,5 * 3.0). O valor padrão é 1.0.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000D533C File Offset: 0x000D473C
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x000D5354 File Offset: 0x000D4754
		public double SpeedRatio
		{
			get
			{
				return this._owner.InternalGetSpeedRatio();
			}
			set
			{
				if (value < 0.0 || value > 1.7976931348623157E+308 || double.IsNaN(value))
				{
					throw new ArgumentException(SR.Get("Timing_InvalidArgFinitePositive"), "value");
				}
				this._owner.InternalSetSpeedRatio(value);
			}
		}

		// Token: 0x04001637 RID: 5687
		private Clock _owner;
	}
}
