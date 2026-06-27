using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Dimensiona um objeto no plano x-y-z tridimensional, começando de um ponto central definido. Os fatores de escala são definidos nos orientações x, y e z desse ponto central.</summary>
	// Token: 0x0200047D RID: 1149
	public sealed class ScaleTransform3D : AffineTransform3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" />.</summary>
		// Token: 0x0600316B RID: 12651 RVA: 0x000C59E8 File Offset: 0x000C4DE8
		public ScaleTransform3D()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" /> usando o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado.</summary>
		/// <param name="scale">O Vector3D ao longo do qual a transformação é escalonada.</param>
		// Token: 0x0600316C RID: 12652 RVA: 0x000C5A28 File Offset: 0x000C4E28
		public ScaleTransform3D(Vector3D scale)
		{
			this.ScaleX = scale.X;
			this.ScaleY = scale.Y;
			this.ScaleZ = scale.Z;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" /> usando os fatores de escala especificados.</summary>
		/// <param name="scaleX">O fator segundo o qual escalonar o valor X.</param>
		/// <param name="scaleY">O fator segundo o qual escalonar o valor Y.</param>
		/// <param name="scaleZ">O fator segundo o qual escalonar o valor Z.</param>
		// Token: 0x0600316D RID: 12653 RVA: 0x000C5A90 File Offset: 0x000C4E90
		public ScaleTransform3D(double scaleX, double scaleY, double scaleZ)
		{
			this.ScaleX = scaleX;
			this.ScaleY = scaleY;
			this.ScaleZ = scaleZ;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" /> usando o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificados.</summary>
		/// <param name="scale">O Vector3D ao longo do qual a transformação é escalonada.</param>
		/// <param name="center">O centro em torno do qual a transformação é escalonada.</param>
		// Token: 0x0600316E RID: 12654 RVA: 0x000C5AE8 File Offset: 0x000C4EE8
		public ScaleTransform3D(Vector3D scale, Point3D center)
		{
			this.ScaleX = scale.X;
			this.ScaleY = scale.Y;
			this.ScaleZ = scale.Z;
			this.CenterX = center.X;
			this.CenterY = center.Y;
			this.CenterZ = center.Z;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" /> usando as coordenadas de centro e fatores de escala especificados.</summary>
		/// <param name="scaleX">O fator segundo o qual escalonar o valor X.</param>
		/// <param name="scaleY">O fator segundo o qual escalonar o valor Y.</param>
		/// <param name="scaleZ">O fator segundo o qual escalonar o valor Z.</param>
		/// <param name="centerX">A coordenada X do ponto central do qual escalonar.</param>
		/// <param name="centerY">A coordenada Y do ponto central do qual escalonar.</param>
		/// <param name="centerZ">A coordenada Z do ponto central do qual escalonar.</param>
		// Token: 0x0600316F RID: 12655 RVA: 0x000C5B78 File Offset: 0x000C4F78
		public ScaleTransform3D(double scaleX, double scaleY, double scaleZ, double centerX, double centerY, double centerZ)
		{
			this.ScaleX = scaleX;
			this.ScaleY = scaleY;
			this.ScaleZ = scaleZ;
			this.CenterX = centerX;
			this.CenterY = centerY;
			this.CenterZ = centerZ;
		}

		/// <summary>Obtém uma representação de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> dessa transformação.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> representação dessa transformação.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000C5BE8 File Offset: 0x000C4FE8
		public override Matrix3D Value
		{
			get
			{
				base.ReadPreamble();
				Matrix3D result = default(Matrix3D);
				this.Append(ref result);
				return result;
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000C5C0C File Offset: 0x000C500C
		internal override void Append(ref Matrix3D matrix)
		{
			Vector3D scale = new Vector3D(this._cachedScaleXValue, this._cachedScaleYValue, this._cachedScaleZValue);
			if (this._cachedCenterXValue == 0.0 && this._cachedCenterYValue == 0.0 && this._cachedCenterZValue == 0.0)
			{
				matrix.Scale(scale);
				return;
			}
			matrix.ScaleAt(scale, new Point3D(this._cachedCenterXValue, this._cachedCenterYValue, this._cachedCenterZValue));
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003172 RID: 12658 RVA: 0x000C5C8C File Offset: 0x000C508C
		public new ScaleTransform3D Clone()
		{
			return (ScaleTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.ScaleTransform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003173 RID: 12659 RVA: 0x000C5CA4 File Offset: 0x000C50A4
		public new ScaleTransform3D CloneCurrentValue()
		{
			return (ScaleTransform3D)base.CloneCurrentValue();
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000C5CBC File Offset: 0x000C50BC
		private static void ScaleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedScaleXValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.ScaleXProperty);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000C5CF0 File Offset: 0x000C50F0
		private static void ScaleYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedScaleYValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.ScaleYProperty);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000C5D24 File Offset: 0x000C5124
		private static void ScaleZPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedScaleZValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.ScaleZProperty);
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000C5D58 File Offset: 0x000C5158
		private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedCenterXValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.CenterXProperty);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000C5D8C File Offset: 0x000C518C
		private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedCenterYValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.CenterYProperty);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000C5DC0 File Offset: 0x000C51C0
		private static void CenterZPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ScaleTransform3D scaleTransform3D = (ScaleTransform3D)d;
			scaleTransform3D._cachedCenterZValue = (double)e.NewValue;
			scaleTransform3D.PropertyChanged(ScaleTransform3D.CenterZProperty);
		}

		/// <summary>Obtém ou define o fator de escala na direção x.</summary>
		/// <returns>Fator de escala na direção x. O valor padrão é 1.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000C5DF4 File Offset: 0x000C51F4
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x000C5E10 File Offset: 0x000C5210
		public double ScaleX
		{
			get
			{
				base.ReadPreamble();
				return this._cachedScaleXValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.ScaleXProperty, value);
			}
		}

		/// <summary>Obtém ou define o fator de escala na direção y.</summary>
		/// <returns>Fator de escala na direção y. O valor padrão é 1.</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000C5E30 File Offset: 0x000C5230
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000C5E4C File Offset: 0x000C524C
		public double ScaleY
		{
			get
			{
				base.ReadPreamble();
				return this._cachedScaleYValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.ScaleYProperty, value);
			}
		}

		/// <summary>Obtém ou define o fator de escala na direção z.</summary>
		/// <returns>Fator de escala na direção z. O valor padrão é 1.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000C5E6C File Offset: 0x000C526C
		// (set) Token: 0x0600317F RID: 12671 RVA: 0x000C5E88 File Offset: 0x000C5288
		public double ScaleZ
		{
			get
			{
				base.ReadPreamble();
				return this._cachedScaleZValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.ScaleZProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada x do ponto central da transformação.</summary>
		/// <returns>A coordenada x do ponto central da transformação. O valor padrão é 0.</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000C5EA8 File Offset: 0x000C52A8
		// (set) Token: 0x06003181 RID: 12673 RVA: 0x000C5EC4 File Offset: 0x000C52C4
		public double CenterX
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterXValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.CenterXProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada z do ponto central da transformação.</summary>
		/// <returns>A coordenada y do ponto central da transformação. O valor padrão é 0.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000C5EE4 File Offset: 0x000C52E4
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x000C5F00 File Offset: 0x000C5300
		public double CenterY
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterYValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.CenterYProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada z do ponto central da transformação.</summary>
		/// <returns>A coordenada z do ponto central da transformação. O valor padrão é 0.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000C5F20 File Offset: 0x000C5320
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x000C5F3C File Offset: 0x000C533C
		public double CenterZ
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterZValue;
			}
			set
			{
				base.SetValueInternal(ScaleTransform3D.CenterZProperty, value);
			}
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000C5F5C File Offset: 0x000C535C
		protected override Freezable CreateInstanceCore()
		{
			return new ScaleTransform3D();
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000C5F70 File Offset: 0x000C5370
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(ScaleTransform3D.ScaleXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(ScaleTransform3D.ScaleYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(ScaleTransform3D.ScaleZProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(ScaleTransform3D.CenterXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle5 = base.GetAnimationResourceHandle(ScaleTransform3D.CenterYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle6 = base.GetAnimationResourceHandle(ScaleTransform3D.CenterZProperty, channel);
				DUCE.MILCMD_SCALETRANSFORM3D milcmd_SCALETRANSFORM3D;
				milcmd_SCALETRANSFORM3D.Type = MILCMD.MilCmdScaleTransform3D;
				milcmd_SCALETRANSFORM3D.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_SCALETRANSFORM3D.scaleX = this.ScaleX;
				}
				milcmd_SCALETRANSFORM3D.hScaleXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_SCALETRANSFORM3D.scaleY = this.ScaleY;
				}
				milcmd_SCALETRANSFORM3D.hScaleYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_SCALETRANSFORM3D.scaleZ = this.ScaleZ;
				}
				milcmd_SCALETRANSFORM3D.hScaleZAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_SCALETRANSFORM3D.centerX = this.CenterX;
				}
				milcmd_SCALETRANSFORM3D.hCenterXAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_SCALETRANSFORM3D.centerY = this.CenterY;
				}
				milcmd_SCALETRANSFORM3D.hCenterYAnimations = animationResourceHandle5;
				if (animationResourceHandle6.IsNull)
				{
					milcmd_SCALETRANSFORM3D.centerZ = this.CenterZ;
				}
				milcmd_SCALETRANSFORM3D.hCenterZAnimations = animationResourceHandle6;
				channel.SendCommand((byte*)(&milcmd_SCALETRANSFORM3D), sizeof(DUCE.MILCMD_SCALETRANSFORM3D));
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000C60CC File Offset: 0x000C54CC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SCALETRANSFORM3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000C6108 File Offset: 0x000C5508
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000C612C File Offset: 0x000C552C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000C6148 File Offset: 0x000C5548
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000C6160 File Offset: 0x000C5560
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000C617C File Offset: 0x000C557C
		static ScaleTransform3D()
		{
			Type typeFromHandle = typeof(ScaleTransform3D);
			ScaleTransform3D.ScaleXProperty = Animatable.RegisterProperty("ScaleX", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(ScaleTransform3D.ScaleXPropertyChanged), null, true, null);
			ScaleTransform3D.ScaleYProperty = Animatable.RegisterProperty("ScaleY", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(ScaleTransform3D.ScaleYPropertyChanged), null, true, null);
			ScaleTransform3D.ScaleZProperty = Animatable.RegisterProperty("ScaleZ", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(ScaleTransform3D.ScaleZPropertyChanged), null, true, null);
			ScaleTransform3D.CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(ScaleTransform3D.CenterXPropertyChanged), null, true, null);
			ScaleTransform3D.CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(ScaleTransform3D.CenterYPropertyChanged), null, true, null);
			ScaleTransform3D.CenterZProperty = Animatable.RegisterProperty("CenterZ", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(ScaleTransform3D.CenterZPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.ScaleX" />.</summary>
		// Token: 0x04001593 RID: 5523
		public static readonly DependencyProperty ScaleXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.ScaleY" />.</summary>
		// Token: 0x04001594 RID: 5524
		public static readonly DependencyProperty ScaleYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.ScaleZ" />.</summary>
		// Token: 0x04001595 RID: 5525
		public static readonly DependencyProperty ScaleZProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.CenterX" />.</summary>
		// Token: 0x04001596 RID: 5526
		public static readonly DependencyProperty CenterXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.CenterY" />.</summary>
		// Token: 0x04001597 RID: 5527
		public static readonly DependencyProperty CenterYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ScaleTransform3D.CenterZ" />.</summary>
		// Token: 0x04001598 RID: 5528
		public static readonly DependencyProperty CenterZProperty;

		// Token: 0x04001599 RID: 5529
		private double _cachedScaleXValue = 1.0;

		// Token: 0x0400159A RID: 5530
		private double _cachedScaleYValue = 1.0;

		// Token: 0x0400159B RID: 5531
		private double _cachedScaleZValue = 1.0;

		// Token: 0x0400159C RID: 5532
		private double _cachedCenterXValue;

		// Token: 0x0400159D RID: 5533
		private double _cachedCenterYValue;

		// Token: 0x0400159E RID: 5534
		private double _cachedCenterZValue;

		// Token: 0x0400159F RID: 5535
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040015A0 RID: 5536
		internal const double c_ScaleX = 1.0;

		// Token: 0x040015A1 RID: 5537
		internal const double c_ScaleY = 1.0;

		// Token: 0x040015A2 RID: 5538
		internal const double c_ScaleZ = 1.0;

		// Token: 0x040015A3 RID: 5539
		internal const double c_CenterX = 0.0;

		// Token: 0x040015A4 RID: 5540
		internal const double c_CenterY = 0.0;

		// Token: 0x040015A5 RID: 5541
		internal const double c_CenterZ = 0.0;
	}
}
