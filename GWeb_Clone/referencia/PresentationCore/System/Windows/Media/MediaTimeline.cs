using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece um <see cref="T:System.Windows.Media.Animation.Timeline" /> para conteúdo de mídia.</summary>
	// Token: 0x020003BF RID: 959
	public class MediaTimeline : Timeline, IUriContext
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.MediaTimeline" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002527 RID: 9511 RVA: 0x00094ACC File Offset: 0x00093ECC
		public new MediaTimeline Clone()
		{
			return (MediaTimeline)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.MediaTimeline" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002528 RID: 9512 RVA: 0x00094AE4 File Offset: 0x00093EE4
		public new MediaTimeline CloneCurrentValue()
		{
			return (MediaTimeline)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a origem de mídia associada à linha do tempo.</summary>
		/// <returns>A origem de mídia associada com a linha do tempo. O padrão é nulo.</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x00094AFC File Offset: 0x00093EFC
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x00094B1C File Offset: 0x00093F1C
		public Uri Source
		{
			get
			{
				return (Uri)base.GetValue(MediaTimeline.SourceProperty);
			}
			set
			{
				base.SetValueInternal(MediaTimeline.SourceProperty, value);
			}
		}

		/// <summary>Cria uma nova instância do MediaTimeline.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600252B RID: 9515 RVA: 0x00094B38 File Offset: 0x00093F38
		protected override Freezable CreateInstanceCore()
		{
			return new MediaTimeline();
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00094B4C File Offset: 0x00093F4C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00094B5C File Offset: 0x00093F5C
		static MediaTimeline()
		{
			Type typeFromHandle = typeof(MediaTimeline);
			MediaTimeline.SourceProperty = Animatable.RegisterProperty("Source", typeof(Uri), typeFromHandle, null, null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância de uma classe <see cref="T:System.Windows.Media.MediaTimeline" /> usando o URI fornecido como a origem de mídia.</summary>
		/// <param name="source">A origem de mídia para a linha do tempo.</param>
		// Token: 0x0600252E RID: 9518 RVA: 0x00094B94 File Offset: 0x00093F94
		public MediaTimeline(Uri source) : this()
		{
			this.Source = source;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00094BB0 File Offset: 0x00093FB0
		internal MediaTimeline(ITypeDescriptorContext context, Uri source) : this()
		{
			this._context = context;
			this.Source = source;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MediaTimeline" />.</summary>
		// Token: 0x06002530 RID: 9520 RVA: 0x00094BD4 File Offset: 0x00093FD4
		public MediaTimeline()
		{
		}

		/// <summary>Inicializa uma nova instância do <see cref="T:System.Windows.Media.MediaTimeline" /> que começa na hora especificada.</summary>
		/// <param name="beginTime">A hora na qual começar a linha do tempo.</param>
		// Token: 0x06002531 RID: 9521 RVA: 0x00094BE8 File Offset: 0x00093FE8
		public MediaTimeline(TimeSpan? beginTime) : this()
		{
			base.BeginTime = beginTime;
		}

		/// <summary>Inicializa uma nova instância do <see cref="T:System.Windows.Media.MediaTimeline" /> que começa na hora especificada e dura pelo tempo especificado.</summary>
		/// <param name="beginTime">O tempo para iniciar a reprodução de mídia.</param>
		/// <param name="duration">O período de tempo para reprodução de mídia.</param>
		// Token: 0x06002532 RID: 9522 RVA: 0x00094C04 File Offset: 0x00094004
		public MediaTimeline(TimeSpan? beginTime, Duration duration) : this()
		{
			base.BeginTime = beginTime;
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MediaTimeline" /> que começa no momento especificado no tempo especificado e tem o comportamento de repetição especificado.</summary>
		/// <param name="beginTime">O tempo para iniciar a reprodução de mídia.</param>
		/// <param name="duration">O período de tempo para reprodução de mídia.</param>
		/// <param name="repeatBehavior">O comportamento de repetição a ser usado quando a duração de reprodução tiver sido atingida.</param>
		// Token: 0x06002533 RID: 9523 RVA: 0x00094C28 File Offset: 0x00094028
		public MediaTimeline(TimeSpan? beginTime, Duration duration, RepeatBehavior repeatBehavior) : this()
		{
			base.BeginTime = beginTime;
			base.Duration = duration;
			base.RepeatBehavior = repeatBehavior;
		}

		/// <summary>Obtém ou define o URI base do contexto do aplicativo atual.</summary>
		/// <returns>O URI base do contexto do aplicativo.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x00094C50 File Offset: 0x00094050
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x00094C64 File Offset: 0x00094064
		Uri IUriContext.BaseUri
		{
			get
			{
				return this._baseUri;
			}
			set
			{
				this._baseUri = value;
			}
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.MediaClock" /> para essa linha do tempo.</summary>
		/// <returns>Um novo <see cref="T:System.Windows.Media.MediaClock" /> para essa linha do tempo.</returns>
		// Token: 0x06002536 RID: 9526 RVA: 0x00094C78 File Offset: 0x00094078
		protected internal override Clock AllocateClock()
		{
			if (this.Source == null)
			{
				throw new InvalidOperationException(SR.Get("Media_UriNotSpecified"));
			}
			return new MediaClock(this);
		}

		/// <summary>Torna esta instância do objeto de MediaTimeline não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> para verificar se a linha do tempo pode ser congelada; <see langword="false" /> para congelar a linha do tempo.</param>
		/// <returns>Se <paramref name="isChecking" /> for <see langword="true" />, esse método retorna <see langword="true" /> se este <see cref="T:System.Windows.Media.MediaTimeline" /> puder se tornar não modificável ou <see langword="false" />, se ele não puder se tornar não modificável. Se <paramref name="isChecking" /> for <see langword="false" />, esse método retornará <see langword="true" /> se o <see cref="T:System.Windows.Media.MediaTimeline" /> especificado agora não for modificável ou <see langword="false" /> se ele não puder se tornar não modificável, com o efeito colateral de ter feito a alteração real no status congelado para este objeto.</returns>
		// Token: 0x06002537 RID: 9527 RVA: 0x00094CAC File Offset: 0x000940AC
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = base.FreezeCore(isChecking);
			if (!flag)
			{
				return false;
			}
			if (isChecking)
			{
				flag &= !base.HasExpression(base.LookupEntry(MediaTimeline.SourceProperty.GlobalIndex), MediaTimeline.SourceProperty);
			}
			return flag;
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.MediaTimeline" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.MediaTimeline" /> a ser clonado.</param>
		// Token: 0x06002538 RID: 9528 RVA: 0x00094CEC File Offset: 0x000940EC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			MediaTimeline sourceTimeline = (MediaTimeline)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceTimeline);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.MediaTimeline" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.MediaTimeline" /> a ser clonado.</param>
		// Token: 0x06002539 RID: 9529 RVA: 0x00094D10 File Offset: 0x00094110
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			MediaTimeline sourceTimeline = (MediaTimeline)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceTimeline);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.MediaTimeline" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.MediaTimeline" /> a ser clonado e congelado.</param>
		// Token: 0x0600253A RID: 9530 RVA: 0x00094D34 File Offset: 0x00094134
		protected override void GetAsFrozenCore(Freezable source)
		{
			MediaTimeline sourceTimeline = (MediaTimeline)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceTimeline);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.MediaTimeline" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.MediaTimeline" /> a ser copiado e congelado.</param>
		// Token: 0x0600253B RID: 9531 RVA: 0x00094D58 File Offset: 0x00094158
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			MediaTimeline sourceTimeline = (MediaTimeline)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceTimeline);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00094D7C File Offset: 0x0009417C
		private void CopyCommon(MediaTimeline sourceTimeline)
		{
			this._context = sourceTimeline._context;
			this._baseUri = sourceTimeline._baseUri;
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.MediaClock" /> associado a <see cref="T:System.Windows.Media.MediaTimeline" />.</summary>
		/// <returns>O novo <see cref="T:System.Windows.Media.MediaClock" />.</returns>
		// Token: 0x0600253D RID: 9533 RVA: 0x00094DA4 File Offset: 0x000941A4
		public new MediaClock CreateClock()
		{
			return (MediaClock)base.CreateClock();
		}

		/// <summary>Recupera o <see cref="T:System.Windows.Duration" /> de um relógio especificado.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se <paramref name="clock" /> for um <see cref="T:System.Windows.Media.MediaClock" />, o valor <see cref="P:System.Windows.Media.MediaPlayer.NaturalDuration" /> do associado <see cref="T:System.Windows.Media.MediaPlayer" /> ao <paramref name="clock" /> ou <see cref="P:System.Windows.Duration.Automatic" />, se o <paramref name="clock" /> não for um <see cref="T:System.Windows.Media.MediaClock" />.</returns>
		// Token: 0x0600253E RID: 9534 RVA: 0x00094DBC File Offset: 0x000941BC
		protected override Duration GetNaturalDurationCore(Clock clock)
		{
			MediaClock mediaClock = (MediaClock)clock;
			if (mediaClock.Player == null)
			{
				return Duration.Automatic;
			}
			return mediaClock.Player.NaturalDuration;
		}

		/// <summary>Retorna a cadeia de caracteres que representa a origem de mídia.</summary>
		/// <returns>A cadeia de caracteres que representa a origem de mídia.</returns>
		// Token: 0x0600253F RID: 9535 RVA: 0x00094DEC File Offset: 0x000941EC
		public override string ToString()
		{
			if (null == this.Source)
			{
				throw new InvalidOperationException(SR.Get("Media_UriNotSpecified"));
			}
			return this.Source.ToString();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.MediaTimeline.Source" />.</summary>
		// Token: 0x0400118A RID: 4490
		public static readonly DependencyProperty SourceProperty;

		// Token: 0x0400118B RID: 4491
		internal static Uri s_Source;

		// Token: 0x0400118C RID: 4492
		internal const uint LastTimelineFlag = 1U;

		// Token: 0x0400118D RID: 4493
		internal ITypeDescriptorContext _context;

		// Token: 0x0400118E RID: 4494
		private Uri _baseUri;
	}
}
