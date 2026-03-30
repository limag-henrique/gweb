using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, representa um <see cref="T:System.Windows.Media.Animation.Timeline" /> que pode conter uma coleção de objetos filhos <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
	// Token: 0x0200055F RID: 1375
	[ContentProperty("Children")]
	public abstract class TimelineGroup : Timeline, IAddChild
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.TimelineGroup" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003F96 RID: 16278 RVA: 0x000F9744 File Offset: 0x000F8B44
		public new TimelineGroup Clone()
		{
			return (TimelineGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.TimelineGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003F97 RID: 16279 RVA: 0x000F975C File Offset: 0x000F8B5C
		public new TimelineGroup CloneCurrentValue()
		{
			return (TimelineGroup)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a coleção de objetos filhos <see cref="T:System.Windows.Media.Animation.Timeline" /> diretos do <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</summary>
		/// <returns>Filho <see cref="T:System.Windows.Media.Animation.Timeline" /> objetos do <see cref="T:System.Windows.Media.Animation.TimelineGroup" />. O valor padrão é nulo.</returns>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000F9774 File Offset: 0x000F8B74
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x000F9794 File Offset: 0x000F8B94
		public TimelineCollection Children
		{
			get
			{
				return (TimelineCollection)base.GetValue(TimelineGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(TimelineGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000F97B0 File Offset: 0x000F8BB0
		static TimelineGroup()
		{
			Type typeFromHandle = typeof(TimelineGroup);
			TimelineGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(TimelineCollection), typeFromHandle, new FreezableDefaultValueFactory(TimelineCollection.Empty), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> com propriedades padrão.</summary>
		// Token: 0x06003F9B RID: 16283 RVA: 0x000F97FC File Offset: 0x000F8BFC
		protected TimelineGroup()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> especificado.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003F9C RID: 16284 RVA: 0x000F9810 File Offset: 0x000F8C10
		protected TimelineGroup(TimeSpan? beginTime) : base(beginTime)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> e <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificados.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="duration">O <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003F9D RID: 16285 RVA: 0x000F9824 File Offset: 0x000F8C24
		protected TimelineGroup(TimeSpan? beginTime, Duration duration) : base(beginTime, duration)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> com o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" />, <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> e <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> especificados.</summary>
		/// <param name="beginTime">O <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="duration">O <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		/// <param name="repeatBehavior">O <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> para <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</param>
		// Token: 0x06003F9E RID: 16286 RVA: 0x000F983C File Offset: 0x000F8C3C
		protected TimelineGroup(TimeSpan? beginTime, Duration duration, RepeatBehavior repeatBehavior) : base(beginTime, duration, repeatBehavior)
		{
		}

		/// <summary>Cria um relógio de um tipo específico para essa linha do tempo.</summary>
		/// <returns>Um relógio para essa linha do tempo.</returns>
		// Token: 0x06003F9F RID: 16287 RVA: 0x000F9854 File Offset: 0x000F8C54
		protected internal override Clock AllocateClock()
		{
			return new ClockGroup(this);
		}

		/// <summary>Cria uma instância de um novo objeto <see cref="T:System.Windows.Media.Animation.ClockGroup" />, usando essa instância.</summary>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Animation.ClockGroup" />.</returns>
		// Token: 0x06003FA0 RID: 16288 RVA: 0x000F9868 File Offset: 0x000F8C68
		public new ClockGroup CreateClock()
		{
			return (ClockGroup)base.CreateClock();
		}

		/// <summary>Adiciona um objeto filho.</summary>
		/// <param name="child">O objeto filho a ser adicionado.</param>
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000F9880 File Offset: 0x000F8C80
		void IAddChild.AddChild(object child)
		{
			base.WritePreamble();
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			this.AddChild(child);
			base.WritePostscript();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Timeline" /> filho a este <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.TimelineGroup" />. Se esse objeto for um <see cref="T:System.Windows.Media.Animation.Timeline" />, ele será adicionado à coleção <see cref="P:System.Windows.Media.Animation.TimelineGroup.Children" />; caso contrário, uma exceção será gerada.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.Timeline" />.</exception>
		// Token: 0x06003FA2 RID: 16290 RVA: 0x000F98B0 File Offset: 0x000F8CB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			Timeline timeline = child as Timeline;
			if (timeline == null)
			{
				throw new ArgumentException(SR.Get("Timing_ChildMustBeTimeline"), "child");
			}
			this.Children.Add(timeline);
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003FA3 RID: 16291 RVA: 0x000F98E8 File Offset: 0x000F8CE8
		void IAddChild.AddText(string childText)
		{
			base.WritePreamble();
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
			base.WritePostscript();
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.Timeline" />.</param>
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000F9918 File Offset: 0x000F8D18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Timing_NoTextChildren"));
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.TimelineGroup.Children" />.</summary>
		// Token: 0x04001774 RID: 6004
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x04001775 RID: 6005
		internal static TimelineCollection s_Children = TimelineCollection.Empty;
	}
}
