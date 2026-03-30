using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Dimensiona um objeto no sistema de coordenadas x-y 2D.</summary>
	// Token: 0x020003ED RID: 1005
	public sealed class ScaleTransform : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.ScaleTransform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x0600274D RID: 10061 RVA: 0x0009E574 File Offset: 0x0009D974
		public new ScaleTransform Clone()
		{
			return (ScaleTransform)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.ScaleTransform" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x0600274E RID: 10062 RVA: 0x0009E58C File Offset: 0x0009D98C
		public new ScaleTransform CloneCurrentValue()
		{
			return (ScaleTransform)base.CloneCurrentValue();
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0009E5A4 File Offset: 0x0009D9A4
		private static void ScaleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform scaleTransform = (ScaleTransform)d;
			scaleTransform.PropertyChanged(ScaleTransform.ScaleXProperty);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0009E5C4 File Offset: 0x0009D9C4
		private static void ScaleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform scaleTransform = (ScaleTransform)d;
			scaleTransform.PropertyChanged(ScaleTransform.ScaleYProperty);
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0009E5E4 File Offset: 0x0009D9E4
		private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform scaleTransform = (ScaleTransform)d;
			scaleTransform.PropertyChanged(ScaleTransform.CenterXProperty);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0009E604 File Offset: 0x0009DA04
		private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform scaleTransform = (ScaleTransform)d;
			scaleTransform.PropertyChanged(ScaleTransform.CenterYProperty);
		}

		/// <summary>Obtém ou define o fator de escala do eixo x.</summary>
		/// <returns>O fator de escala ao longo do eixo x. O padrão é 1.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0009E624 File Offset: 0x0009DA24
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x0009E644 File Offset: 0x0009DA44
		public double ScaleX
		{
			get
			{
				return (double)base.GetValue(ScaleTransform.ScaleXProperty);
			}
			set
			{
				base.SetValueInternal(ScaleTransform.ScaleXProperty, value);
			}
		}

		/// <summary>Obtém ou define o fator de escala do eixo y.</summary>
		/// <returns>O fator de escala no eixo y. O padrão é 1.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x0009E664 File Offset: 0x0009DA64
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x0009E684 File Offset: 0x0009DA84
		public double ScaleY
		{
			get
			{
				return (double)base.GetValue(ScaleTransform.ScaleYProperty);
			}
			set
			{
				base.SetValueInternal(ScaleTransform.ScaleYProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada X do ponto central deste <see cref="T:System.Windows.Media.ScaleTransform" />.</summary>
		/// <returns>A coordenada X do ponto central deste <see cref="T:System.Windows.Media.ScaleTransform" />. O padrão é 0.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0009E6A4 File Offset: 0x0009DAA4
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x0009E6C4 File Offset: 0x0009DAC4
		public double CenterX
		{
			get
			{
				return (double)base.GetValue(ScaleTransform.CenterXProperty);
			}
			set
			{
				base.SetValueInternal(ScaleTransform.CenterXProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada Y do ponto central deste <see cref="T:System.Windows.Media.ScaleTransform" />.</summary>
		/// <returns>A coordenada y do ponto central deste <see cref="T:System.Windows.Media.ScaleTransform" />. O padrão é 0.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0009E6E4 File Offset: 0x0009DAE4
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x0009E704 File Offset: 0x0009DB04
		public double CenterY
		{
			get
			{
				return (double)base.GetValue(ScaleTransform.CenterYProperty);
			}
			set
			{
				base.SetValueInternal(ScaleTransform.CenterYProperty, value);
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0009E724 File Offset: 0x0009DB24
		protected override Freezable CreateInstanceCore()
		{
			return new ScaleTransform();
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0009E738 File Offset: 0x0009DB38
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(ScaleTransform.ScaleXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(ScaleTransform.ScaleYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(ScaleTransform.CenterXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(ScaleTransform.CenterYProperty, channel);
				DUCE.MILCMD_SCALETRANSFORM milcmd_SCALETRANSFORM;
				milcmd_SCALETRANSFORM.Type = MILCMD.MilCmdScaleTransform;
				milcmd_SCALETRANSFORM.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_SCALETRANSFORM.ScaleX = this.ScaleX;
				}
				milcmd_SCALETRANSFORM.hScaleXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_SCALETRANSFORM.ScaleY = this.ScaleY;
				}
				milcmd_SCALETRANSFORM.hScaleYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_SCALETRANSFORM.CenterX = this.CenterX;
				}
				milcmd_SCALETRANSFORM.hCenterXAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_SCALETRANSFORM.CenterY = this.CenterY;
				}
				milcmd_SCALETRANSFORM.hCenterYAnimations = animationResourceHandle4;
				channel.SendCommand((byte*)(&milcmd_SCALETRANSFORM), sizeof(DUCE.MILCMD_SCALETRANSFORM));
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0009E838 File Offset: 0x0009DC38
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SCALETRANSFORM))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x0009E874 File Offset: 0x0009DC74
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0009E898 File Offset: 0x0009DC98
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0009E8B4 File Offset: 0x0009DCB4
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0009E8CC File Offset: 0x0009DCCC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0009E8E8 File Offset: 0x0009DCE8
		static ScaleTransform()
		{
			Type typeFromHandle = typeof(ScaleTransform);
			ScaleTransform.ScaleXProperty = Animatable.RegisterProperty("ScaleX", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(ScaleTransform.ScaleXPropertyChanged), null, true, null);
			ScaleTransform.ScaleYProperty = Animatable.RegisterProperty("ScaleY", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(ScaleTransform.ScaleYPropertyChanged), null, true, null);
			ScaleTransform.CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(ScaleTransform.CenterXPropertyChanged), null, true, null);
			ScaleTransform.CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(ScaleTransform.CenterYPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ScaleTransform" />.</summary>
		// Token: 0x06002763 RID: 10083 RVA: 0x0009E9DC File Offset: 0x0009DDDC
		public ScaleTransform()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ScaleTransform" /> com os fatores de escala de x e y especificados. A operação de dimensionamento é centralizada em (0,0).</summary>
		/// <param name="scaleX">O fator de escala do eixo x.</param>
		/// <param name="scaleY">O fator de escala do eixo y.</param>
		// Token: 0x06002764 RID: 10084 RVA: 0x0009E9F0 File Offset: 0x0009DDF0
		public ScaleTransform(double scaleX, double scaleY)
		{
			this.ScaleX = scaleX;
			this.ScaleY = scaleY;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ScaleTransform" /> que tem os fatores de escala e o ponto central especificados.</summary>
		/// <param name="scaleX">O fator de escala do eixo x. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.ScaleTransform.ScaleX" />.</param>
		/// <param name="scaleY">O fator de escala do eixo y. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.ScaleTransform.ScaleY" />.</param>
		/// <param name="centerX">A coordenada X do centro desse <see cref="T:System.Windows.Media.ScaleTransform" />. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.ScaleTransform.CenterX" />.</param>
		/// <param name="centerY">A coordenada Y do centro desse <see cref="T:System.Windows.Media.ScaleTransform" />. Para obter mais informações, consulte a propriedade <see cref="P:System.Windows.Media.ScaleTransform.CenterY" />.</param>
		// Token: 0x06002765 RID: 10085 RVA: 0x0009EA14 File Offset: 0x0009DE14
		public ScaleTransform(double scaleX, double scaleY, double centerX, double centerY) : this(scaleX, scaleY)
		{
			this.CenterX = centerX;
			this.CenterY = centerY;
		}

		/// <summary>Obtém a transformação de colocação em escala atual como um objeto <see cref="T:System.Windows.Media.Matrix" />.</summary>
		/// <returns>A transformação de colocação em escala atual retornada como um <see cref="T:System.Windows.Media.Matrix" /> objeto.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x0009EA38 File Offset: 0x0009DE38
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				Matrix result = default(Matrix);
				result.ScaleAt(this.ScaleX, this.ScaleY, this.CenterX, this.CenterY);
				return result;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x0009EA74 File Offset: 0x0009DE74
		internal override bool IsIdentity
		{
			get
			{
				return this.ScaleX == 1.0 && this.ScaleY == 1.0 && base.CanFreeze;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0009EAAC File Offset: 0x0009DEAC
		internal override void TransformRect(ref Rect rect)
		{
			if (rect.IsEmpty)
			{
				return;
			}
			double scaleX = this.ScaleX;
			double scaleY = this.ScaleY;
			double centerX = this.CenterX;
			double centerY = this.CenterY;
			bool flag = centerX != 0.0 || centerY != 0.0;
			if (flag)
			{
				rect.X -= centerX;
				rect.Y -= centerY;
			}
			rect.Scale(scaleX, scaleY);
			if (flag)
			{
				rect.X += centerX;
				rect.Y += centerY;
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0009EB48 File Offset: 0x0009DF48
		internal override void MultiplyValueByMatrix(ref Matrix result, ref Matrix matrixToMultiplyBy)
		{
			result = Matrix.Identity;
			result._m11 = this.ScaleX;
			result._m22 = this.ScaleY;
			double centerX = this.CenterX;
			double centerY = this.CenterY;
			result._type = MatrixTypes.TRANSFORM_IS_SCALING;
			if (centerX != 0.0 || centerY != 0.0)
			{
				result._offsetX = centerX - centerX * result._m11;
				result._offsetY = centerY - centerY * result._m22;
				result._type |= MatrixTypes.TRANSFORM_IS_TRANSLATION;
			}
			MatrixUtil.MultiplyMatrix(ref result, ref matrixToMultiplyBy);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ScaleTransform.ScaleX" />.</summary>
		// Token: 0x0400124A RID: 4682
		public static readonly DependencyProperty ScaleXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ScaleTransform.ScaleY" />.</summary>
		// Token: 0x0400124B RID: 4683
		public static readonly DependencyProperty ScaleYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ScaleTransform.CenterX" />.</summary>
		// Token: 0x0400124C RID: 4684
		public static readonly DependencyProperty CenterXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ScaleTransform.CenterY" />.</summary>
		// Token: 0x0400124D RID: 4685
		public static readonly DependencyProperty CenterYProperty;

		// Token: 0x0400124E RID: 4686
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400124F RID: 4687
		internal const double c_ScaleX = 1.0;

		// Token: 0x04001250 RID: 4688
		internal const double c_ScaleY = 1.0;

		// Token: 0x04001251 RID: 4689
		internal const double c_CenterX = 0.0;

		// Token: 0x04001252 RID: 4690
		internal const double c_CenterY = 0.0;
	}
}
