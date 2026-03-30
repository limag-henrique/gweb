using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Move um objeto no sistema de coordenadas 2-D x-y.</summary>
	// Token: 0x020003FC RID: 1020
	public sealed class TranslateTransform : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.TranslateTransform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x0600288B RID: 10379 RVA: 0x000A2A8C File Offset: 0x000A1E8C
		public new TranslateTransform Clone()
		{
			return (TranslateTransform)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.TranslateTransform" /> fazendo cópias em profundidade de seus valores. Este método não copia referências de recurso, associações de dados e animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x0600288C RID: 10380 RVA: 0x000A2AA4 File Offset: 0x000A1EA4
		public new TranslateTransform CloneCurrentValue()
		{
			return (TranslateTransform)base.CloneCurrentValue();
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000A2ABC File Offset: 0x000A1EBC
		private static void XPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TranslateTransform translateTransform = (TranslateTransform)d;
			translateTransform.PropertyChanged(TranslateTransform.XProperty);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000A2ADC File Offset: 0x000A1EDC
		private static void YPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TranslateTransform translateTransform = (TranslateTransform)d;
			translateTransform.PropertyChanged(TranslateTransform.YProperty);
		}

		/// <summary>Obtém ou define a distância a mover ao longo do eixo x.</summary>
		/// <returns>A distância a mover (translação) um objeto ao longo do eixo x. O valor padrão é 0.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000A2AFC File Offset: 0x000A1EFC
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x000A2B1C File Offset: 0x000A1F1C
		public double X
		{
			get
			{
				return (double)base.GetValue(TranslateTransform.XProperty);
			}
			set
			{
				base.SetValueInternal(TranslateTransform.XProperty, value);
			}
		}

		/// <summary>Obtém ou define a distância a mover (translação) um objeto ao longo do eixo y.</summary>
		/// <returns>A distância a mover (mover) um objeto ao longo do eixo y. O valor padrão é 0.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000A2B3C File Offset: 0x000A1F3C
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000A2B5C File Offset: 0x000A1F5C
		public double Y
		{
			get
			{
				return (double)base.GetValue(TranslateTransform.YProperty);
			}
			set
			{
				base.SetValueInternal(TranslateTransform.YProperty, value);
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000A2B7C File Offset: 0x000A1F7C
		protected override Freezable CreateInstanceCore()
		{
			return new TranslateTransform();
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000A2B90 File Offset: 0x000A1F90
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(TranslateTransform.XProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(TranslateTransform.YProperty, channel);
				DUCE.MILCMD_TRANSLATETRANSFORM milcmd_TRANSLATETRANSFORM;
				milcmd_TRANSLATETRANSFORM.Type = MILCMD.MilCmdTranslateTransform;
				milcmd_TRANSLATETRANSFORM.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_TRANSLATETRANSFORM.X = this.X;
				}
				milcmd_TRANSLATETRANSFORM.hXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_TRANSLATETRANSFORM.Y = this.Y;
				}
				milcmd_TRANSLATETRANSFORM.hYAnimations = animationResourceHandle2;
				channel.SendCommand((byte*)(&milcmd_TRANSLATETRANSFORM), sizeof(DUCE.MILCMD_TRANSLATETRANSFORM));
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000A2C3C File Offset: 0x000A203C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_TRANSLATETRANSFORM))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000A2C78 File Offset: 0x000A2078
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000A2C9C File Offset: 0x000A209C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000A2CB8 File Offset: 0x000A20B8
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000A2CD0 File Offset: 0x000A20D0
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000A2CEC File Offset: 0x000A20EC
		static TranslateTransform()
		{
			Type typeFromHandle = typeof(TranslateTransform);
			TranslateTransform.XProperty = Animatable.RegisterProperty("X", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(TranslateTransform.XPropertyChanged), null, true, null);
			TranslateTransform.YProperty = Animatable.RegisterProperty("Y", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(TranslateTransform.YPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TranslateTransform" />.</summary>
		// Token: 0x0600289B RID: 10395 RVA: 0x000A2D74 File Offset: 0x000A2174
		public TranslateTransform()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TranslateTransform" /> e especifica os deslocamentos na direção dos eixos x e y.</summary>
		/// <param name="offsetX">O deslocamento na direção do eixo x.</param>
		/// <param name="offsetY">O deslocamento na direção do eixo y.</param>
		// Token: 0x0600289C RID: 10396 RVA: 0x000A2D88 File Offset: 0x000A2188
		public TranslateTransform(double offsetX, double offsetY)
		{
			this.X = offsetX;
			this.Y = offsetY;
		}

		/// <summary>Obtém uma representação <see cref="T:System.Windows.Media.Matrix" /> desse <see cref="T:System.Windows.Media.TranslateTransform" />.</summary>
		/// <returns>Uma matriz que representa este <see cref="T:System.Windows.Media.TranslateTransform" />.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000A2DAC File Offset: 0x000A21AC
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				Matrix identity = Matrix.Identity;
				identity.Translate(this.X, this.Y);
				return identity;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x000A2DDC File Offset: 0x000A21DC
		internal override bool IsIdentity
		{
			get
			{
				return this.X == 0.0 && this.Y == 0.0 && base.CanFreeze;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000A2E14 File Offset: 0x000A2214
		internal override void TransformRect(ref Rect rect)
		{
			if (!rect.IsEmpty)
			{
				rect.Offset(this.X, this.Y);
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000A2E3C File Offset: 0x000A223C
		internal override void MultiplyValueByMatrix(ref Matrix result, ref Matrix matrixToMultiplyBy)
		{
			result = Matrix.Identity;
			result._offsetX = this.X;
			result._offsetY = this.Y;
			result._type = MatrixTypes.TRANSFORM_IS_TRANSLATION;
			MatrixUtil.MultiplyMatrix(ref result, ref matrixToMultiplyBy);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TranslateTransform.X" />.</summary>
		// Token: 0x0400129B RID: 4763
		public static readonly DependencyProperty XProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TranslateTransform.Y" />.</summary>
		// Token: 0x0400129C RID: 4764
		public static readonly DependencyProperty YProperty;

		// Token: 0x0400129D RID: 4765
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400129E RID: 4766
		internal const double c_X = 0.0;

		// Token: 0x0400129F RID: 4767
		internal const double c_Y = 0.0;
	}
}
