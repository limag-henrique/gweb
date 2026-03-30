using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Representa uma inclinação 2D.</summary>
	// Token: 0x020003EE RID: 1006
	public sealed class SkewTransform : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.SkewTransform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x0600276A RID: 10090 RVA: 0x0009EBD8 File Offset: 0x0009DFD8
		public new SkewTransform Clone()
		{
			return (SkewTransform)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.SkewTransform" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x0600276B RID: 10091 RVA: 0x0009EBF0 File Offset: 0x0009DFF0
		public new SkewTransform CloneCurrentValue()
		{
			return (SkewTransform)base.CloneCurrentValue();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0009EC08 File Offset: 0x0009E008
		private static void AngleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SkewTransform skewTransform = (SkewTransform)d;
			skewTransform.PropertyChanged(SkewTransform.AngleXProperty);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0009EC28 File Offset: 0x0009E028
		private static void AngleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SkewTransform skewTransform = (SkewTransform)d;
			skewTransform.PropertyChanged(SkewTransform.AngleYProperty);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0009EC48 File Offset: 0x0009E048
		private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SkewTransform skewTransform = (SkewTransform)d;
			skewTransform.PropertyChanged(SkewTransform.CenterXProperty);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0009EC68 File Offset: 0x0009E068
		private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SkewTransform skewTransform = (SkewTransform)d;
			skewTransform.PropertyChanged(SkewTransform.CenterYProperty);
		}

		/// <summary>Obtém ou define o ângulo de inclinação do eixo X, que é medido em graus, no sentido anti-horário partindo do eixo Y.</summary>
		/// <returns>O ângulo de inclinação, que é medido em graus no sentido anti-horário partindo do eixo y. O padrão é 0.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0009EC88 File Offset: 0x0009E088
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x0009ECA8 File Offset: 0x0009E0A8
		public double AngleX
		{
			get
			{
				return (double)base.GetValue(SkewTransform.AngleXProperty);
			}
			set
			{
				base.SetValueInternal(SkewTransform.AngleXProperty, value);
			}
		}

		/// <summary>Obtém ou define o ângulo de inclinação do eixo Y, que é medido em graus, no sentido anti-horário partindo do eixo X.</summary>
		/// <returns>O ângulo de inclinação, que é medido em graus no sentido anti-horário partindo do eixo x. O padrão é 0.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x0009ECC8 File Offset: 0x0009E0C8
		// (set) Token: 0x06002773 RID: 10099 RVA: 0x0009ECE8 File Offset: 0x0009E0E8
		public double AngleY
		{
			get
			{
				return (double)base.GetValue(SkewTransform.AngleYProperty);
			}
			set
			{
				base.SetValueInternal(SkewTransform.AngleYProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada X do centro da transformação.</summary>
		/// <returns>A coordenada X do centro de transformação. O padrão é 0.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x0009ED08 File Offset: 0x0009E108
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x0009ED28 File Offset: 0x0009E128
		public double CenterX
		{
			get
			{
				return (double)base.GetValue(SkewTransform.CenterXProperty);
			}
			set
			{
				base.SetValueInternal(SkewTransform.CenterXProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada Y do centro da transformação.</summary>
		/// <returns>A coordenada Y do centro de transformação. O padrão é 0.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x0009ED48 File Offset: 0x0009E148
		// (set) Token: 0x06002777 RID: 10103 RVA: 0x0009ED68 File Offset: 0x0009E168
		public double CenterY
		{
			get
			{
				return (double)base.GetValue(SkewTransform.CenterYProperty);
			}
			set
			{
				base.SetValueInternal(SkewTransform.CenterYProperty, value);
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0009ED88 File Offset: 0x0009E188
		protected override Freezable CreateInstanceCore()
		{
			return new SkewTransform();
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0009ED9C File Offset: 0x0009E19C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(SkewTransform.AngleXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(SkewTransform.AngleYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(SkewTransform.CenterXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(SkewTransform.CenterYProperty, channel);
				DUCE.MILCMD_SKEWTRANSFORM milcmd_SKEWTRANSFORM;
				milcmd_SKEWTRANSFORM.Type = MILCMD.MilCmdSkewTransform;
				milcmd_SKEWTRANSFORM.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_SKEWTRANSFORM.AngleX = this.AngleX;
				}
				milcmd_SKEWTRANSFORM.hAngleXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_SKEWTRANSFORM.AngleY = this.AngleY;
				}
				milcmd_SKEWTRANSFORM.hAngleYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_SKEWTRANSFORM.CenterX = this.CenterX;
				}
				milcmd_SKEWTRANSFORM.hCenterXAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_SKEWTRANSFORM.CenterY = this.CenterY;
				}
				milcmd_SKEWTRANSFORM.hCenterYAnimations = animationResourceHandle4;
				channel.SendCommand((byte*)(&milcmd_SKEWTRANSFORM), sizeof(DUCE.MILCMD_SKEWTRANSFORM));
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0009EE9C File Offset: 0x0009E29C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SKEWTRANSFORM))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0009EED8 File Offset: 0x0009E2D8
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0009EEFC File Offset: 0x0009E2FC
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0009EF18 File Offset: 0x0009E318
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0009EF30 File Offset: 0x0009E330
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0009EF4C File Offset: 0x0009E34C
		static SkewTransform()
		{
			Type typeFromHandle = typeof(SkewTransform);
			SkewTransform.AngleXProperty = Animatable.RegisterProperty("AngleX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(SkewTransform.AngleXPropertyChanged), null, true, null);
			SkewTransform.AngleYProperty = Animatable.RegisterProperty("AngleY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(SkewTransform.AngleYPropertyChanged), null, true, null);
			SkewTransform.CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(SkewTransform.CenterXPropertyChanged), null, true, null);
			SkewTransform.CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(SkewTransform.CenterYPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.SkewTransform" />.</summary>
		// Token: 0x06002780 RID: 10112 RVA: 0x0009F040 File Offset: 0x0009E440
		public SkewTransform()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.SkewTransform" /> que tem os ângulos dos eixos x e y especificados e é centralizada na origem.</summary>
		/// <param name="angleX">O ângulo de inclinação do eixo X, que é medido em graus, no sentido anti-horário partindo do eixo Y. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.AngleX" />.</param>
		/// <param name="angleY">O ângulo de inclinação do eixo Y, que é medido em graus, no sentido anti-horário partindo do eixo X. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.AngleY" />.</param>
		// Token: 0x06002781 RID: 10113 RVA: 0x0009F054 File Offset: 0x0009E454
		public SkewTransform(double angleX, double angleY)
		{
			this.AngleX = angleX;
			this.AngleY = angleY;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.SkewTransform" /> que tem o centro e os ângulos dos eixos x e y especificados.</summary>
		/// <param name="angleX">O ângulo de inclinação do eixo X, que é medido em graus, no sentido anti-horário partindo do eixo Y. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.AngleX" />.</param>
		/// <param name="angleY">O ângulo de inclinação do eixo Y, que é medido em graus, no sentido anti-horário partindo do eixo X. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.AngleY" />.</param>
		/// <param name="centerX">A coordenada X do centro de transformação. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.CenterX" />.</param>
		/// <param name="centerY">A coordenada Y do centro de transformação. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.SkewTransform.CenterY" />.</param>
		// Token: 0x06002782 RID: 10114 RVA: 0x0009F078 File Offset: 0x0009E478
		public SkewTransform(double angleX, double angleY, double centerX, double centerY) : this(angleX, angleY)
		{
			this.CenterX = centerX;
			this.CenterY = centerY;
		}

		/// <summary>Obtém o valor atual da transformação como um <see cref="T:System.Windows.Media.Matrix" />.</summary>
		/// <returns>O valor atual de transformação de inclinação como um <see cref="T:System.Windows.Media.Matrix" />.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x0009F09C File Offset: 0x0009E49C
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				Matrix result = default(Matrix);
				double angleX = this.AngleX;
				double angleY = this.AngleY;
				double centerX = this.CenterX;
				double centerY = this.CenterY;
				bool flag = centerX != 0.0 || centerY != 0.0;
				if (flag)
				{
					result.Translate(-centerX, -centerY);
				}
				result.Skew(angleX, angleY);
				if (flag)
				{
					result.Translate(centerX, centerY);
				}
				return result;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x0009F11C File Offset: 0x0009E51C
		internal override bool IsIdentity
		{
			get
			{
				return this.AngleX == 0.0 && this.AngleY == 0.0 && base.CanFreeze;
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.SkewTransform.AngleX" />.</summary>
		// Token: 0x04001253 RID: 4691
		public static readonly DependencyProperty AngleXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.SkewTransform.AngleY" />.</summary>
		// Token: 0x04001254 RID: 4692
		public static readonly DependencyProperty AngleYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.SkewTransform.CenterX" />.</summary>
		// Token: 0x04001255 RID: 4693
		public static readonly DependencyProperty CenterXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.SkewTransform.CenterY" />.</summary>
		// Token: 0x04001256 RID: 4694
		public static readonly DependencyProperty CenterYProperty;

		// Token: 0x04001257 RID: 4695
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001258 RID: 4696
		internal const double c_AngleX = 0.0;

		// Token: 0x04001259 RID: 4697
		internal const double c_AngleY = 0.0;

		// Token: 0x0400125A RID: 4698
		internal const double c_CenterX = 0.0;

		// Token: 0x0400125B RID: 4699
		internal const double c_CenterY = 0.0;
	}
}
