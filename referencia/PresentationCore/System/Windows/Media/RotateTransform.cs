using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Gira um objeto no sentido horário sobre um ponto especificado em um sistema de coordenadas x-y 2D.</summary>
	// Token: 0x020003EC RID: 1004
	public sealed class RotateTransform : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.RotateTransform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002735 RID: 10037 RVA: 0x0009E114 File Offset: 0x0009D514
		public new RotateTransform Clone()
		{
			return (RotateTransform)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.RotateTransform" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002736 RID: 10038 RVA: 0x0009E12C File Offset: 0x0009D52C
		public new RotateTransform CloneCurrentValue()
		{
			return (RotateTransform)base.CloneCurrentValue();
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0009E144 File Offset: 0x0009D544
		private static void AnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform rotateTransform = (RotateTransform)d;
			rotateTransform.PropertyChanged(RotateTransform.AngleProperty);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0009E164 File Offset: 0x0009D564
		private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform rotateTransform = (RotateTransform)d;
			rotateTransform.PropertyChanged(RotateTransform.CenterXProperty);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0009E184 File Offset: 0x0009D584
		private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform rotateTransform = (RotateTransform)d;
			rotateTransform.PropertyChanged(RotateTransform.CenterYProperty);
		}

		/// <summary>Obtém ou define o ângulo, em graus, da rotação no sentido horário.</summary>
		/// <returns>O ângulo, em graus, da rotação no sentido horário. O padrão é 0.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0009E1A4 File Offset: 0x0009D5A4
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0009E1C4 File Offset: 0x0009D5C4
		public double Angle
		{
			get
			{
				return (double)base.GetValue(RotateTransform.AngleProperty);
			}
			set
			{
				base.SetValueInternal(RotateTransform.AngleProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada X do ponto do centro de rotação.</summary>
		/// <returns>A coordenada X do centro de rotação. O padrão é 0.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0009E1E4 File Offset: 0x0009D5E4
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x0009E204 File Offset: 0x0009D604
		public double CenterX
		{
			get
			{
				return (double)base.GetValue(RotateTransform.CenterXProperty);
			}
			set
			{
				base.SetValueInternal(RotateTransform.CenterXProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada Y do ponto do centro de rotação.</summary>
		/// <returns>A coordenada y do centro da rotação. O padrão é 0.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0009E224 File Offset: 0x0009D624
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x0009E244 File Offset: 0x0009D644
		public double CenterY
		{
			get
			{
				return (double)base.GetValue(RotateTransform.CenterYProperty);
			}
			set
			{
				base.SetValueInternal(RotateTransform.CenterYProperty, value);
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0009E264 File Offset: 0x0009D664
		protected override Freezable CreateInstanceCore()
		{
			return new RotateTransform();
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0009E278 File Offset: 0x0009D678
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(RotateTransform.AngleProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(RotateTransform.CenterXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(RotateTransform.CenterYProperty, channel);
				DUCE.MILCMD_ROTATETRANSFORM milcmd_ROTATETRANSFORM;
				milcmd_ROTATETRANSFORM.Type = MILCMD.MilCmdRotateTransform;
				milcmd_ROTATETRANSFORM.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_ROTATETRANSFORM.Angle = this.Angle;
				}
				milcmd_ROTATETRANSFORM.hAngleAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_ROTATETRANSFORM.CenterX = this.CenterX;
				}
				milcmd_ROTATETRANSFORM.hCenterXAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_ROTATETRANSFORM.CenterY = this.CenterY;
				}
				milcmd_ROTATETRANSFORM.hCenterYAnimations = animationResourceHandle3;
				channel.SendCommand((byte*)(&milcmd_ROTATETRANSFORM), sizeof(DUCE.MILCMD_ROTATETRANSFORM));
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0009E350 File Offset: 0x0009D750
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_ROTATETRANSFORM))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0009E38C File Offset: 0x0009D78C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0009E3B0 File Offset: 0x0009D7B0
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0009E3CC File Offset: 0x0009D7CC
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0009E3E4 File Offset: 0x0009D7E4
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0009E400 File Offset: 0x0009D800
		static RotateTransform()
		{
			Type typeFromHandle = typeof(RotateTransform);
			RotateTransform.AngleProperty = Animatable.RegisterProperty("Angle", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform.AnglePropertyChanged), null, true, null);
			RotateTransform.CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform.CenterXPropertyChanged), null, true, null);
			RotateTransform.CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform.CenterYPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RotateTransform" />.</summary>
		// Token: 0x06002748 RID: 10056 RVA: 0x0009E4C0 File Offset: 0x0009D8C0
		public RotateTransform()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RotateTransform" /> que tem o ângulo especificado, em graus, da rotação no sentido horário. A rotação é centralizada na origem, (0,0).</summary>
		/// <param name="angle">O ângulo da rotação no sentido horário, em graus.</param>
		// Token: 0x06002749 RID: 10057 RVA: 0x0009E4D4 File Offset: 0x0009D8D4
		public RotateTransform(double angle)
		{
			this.Angle = angle;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RotateTransform" /> que tem o ângulo e o ponto central especificados.</summary>
		/// <param name="angle">O ângulo da rotação no sentido horário, em graus. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.RotateTransform.Angle" />.</param>
		/// <param name="centerX">A coordenada X do ponto central do <see cref="T:System.Windows.Media.RotateTransform" />. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.RotateTransform.CenterX" />.</param>
		/// <param name="centerY">A coordenada Y do ponto central do <see cref="T:System.Windows.Media.RotateTransform" />. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.RotateTransform.CenterY" />.</param>
		// Token: 0x0600274A RID: 10058 RVA: 0x0009E4F0 File Offset: 0x0009D8F0
		public RotateTransform(double angle, double centerX, double centerY) : this(angle)
		{
			this.CenterX = centerX;
			this.CenterY = centerY;
		}

		/// <summary>Obtém a transformação de rotação atual como um objeto <see cref="T:System.Windows.Media.Matrix" />.</summary>
		/// <returns>A transformação de rotação atual como um <see cref="T:System.Windows.Media.Matrix" />.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0009E514 File Offset: 0x0009D914
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				Matrix result = default(Matrix);
				result.RotateAt(this.Angle, this.CenterX, this.CenterY);
				return result;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x0009E54C File Offset: 0x0009D94C
		internal override bool IsIdentity
		{
			get
			{
				return this.Angle == 0.0 && base.CanFreeze;
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RotateTransform.Angle" />.</summary>
		// Token: 0x04001243 RID: 4675
		public static readonly DependencyProperty AngleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RotateTransform.CenterX" />.</summary>
		// Token: 0x04001244 RID: 4676
		public static readonly DependencyProperty CenterXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RotateTransform.CenterY" />.</summary>
		// Token: 0x04001245 RID: 4677
		public static readonly DependencyProperty CenterYProperty;

		// Token: 0x04001246 RID: 4678
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001247 RID: 4679
		internal const double c_Angle = 0.0;

		// Token: 0x04001248 RID: 4680
		internal const double c_CenterX = 0.0;

		// Token: 0x04001249 RID: 4681
		internal const double c_CenterY = 0.0;
	}
}
