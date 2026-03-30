using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Move um objeto no plano tridimensional x-y-z.</summary>
	// Token: 0x02000483 RID: 1155
	public sealed class TranslateTransform3D : AffineTransform3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.TranslateTransform3D" />.</summary>
		// Token: 0x06003200 RID: 12800 RVA: 0x000C7A4C File Offset: 0x000C6E4C
		public TranslateTransform3D()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.TranslateTransform3D" /> usando o deslocamento especificado <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="offset">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> pelo qual deslocar o modelo.</param>
		// Token: 0x06003201 RID: 12801 RVA: 0x000C7A60 File Offset: 0x000C6E60
		public TranslateTransform3D(Vector3D offset)
		{
			this.OffsetX = offset.X;
			this.OffsetY = offset.Y;
			this.OffsetZ = offset.Z;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.TranslateTransform3D" /> usando o deslocamento especificado.</summary>
		/// <param name="offsetX">Duplo que especifica o valor de X do Vector3D que especifica o deslocamento de translação.</param>
		/// <param name="offsetY">Duplo que especifica o valor de Y do Vector3D que especifica o deslocamento de translação.</param>
		/// <param name="offsetZ">Duplo que especifica o valor de Z do Vector3D que especifica o deslocamento de translação.</param>
		// Token: 0x06003202 RID: 12802 RVA: 0x000C7A9C File Offset: 0x000C6E9C
		public TranslateTransform3D(double offsetX, double offsetY, double offsetZ)
		{
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.OffsetZ = offsetZ;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que representa o valor da translação.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que representa o valor da tradução.</returns>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000C7AC4 File Offset: 0x000C6EC4
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

		// Token: 0x06003204 RID: 12804 RVA: 0x000C7AE8 File Offset: 0x000C6EE8
		internal override void Append(ref Matrix3D matrix)
		{
			matrix.Translate(new Vector3D(this._cachedOffsetXValue, this._cachedOffsetYValue, this._cachedOffsetZValue));
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.TranslateTransform3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003205 RID: 12805 RVA: 0x000C7B14 File Offset: 0x000C6F14
		public new TranslateTransform3D Clone()
		{
			return (TranslateTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.TranslateTransform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003206 RID: 12806 RVA: 0x000C7B2C File Offset: 0x000C6F2C
		public new TranslateTransform3D CloneCurrentValue()
		{
			return (TranslateTransform3D)base.CloneCurrentValue();
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000C7B44 File Offset: 0x000C6F44
		private static void OffsetXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TranslateTransform3D translateTransform3D = (TranslateTransform3D)d;
			translateTransform3D._cachedOffsetXValue = (double)e.NewValue;
			translateTransform3D.PropertyChanged(TranslateTransform3D.OffsetXProperty);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000C7B78 File Offset: 0x000C6F78
		private static void OffsetYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TranslateTransform3D translateTransform3D = (TranslateTransform3D)d;
			translateTransform3D._cachedOffsetYValue = (double)e.NewValue;
			translateTransform3D.PropertyChanged(TranslateTransform3D.OffsetYProperty);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000C7BAC File Offset: 0x000C6FAC
		private static void OffsetZPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TranslateTransform3D translateTransform3D = (TranslateTransform3D)d;
			translateTransform3D._cachedOffsetZValue = (double)e.NewValue;
			translateTransform3D.PropertyChanged(TranslateTransform3D.OffsetZProperty);
		}

		/// <summary>Obtém ou define o valor do eixo X do deslocamento da translação.</summary>
		/// <returns>Duplo que representa o valor do eixo x do deslocamento da translação.</returns>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000C7BE0 File Offset: 0x000C6FE0
		// (set) Token: 0x0600320B RID: 12811 RVA: 0x000C7BFC File Offset: 0x000C6FFC
		public double OffsetX
		{
			get
			{
				base.ReadPreamble();
				return this._cachedOffsetXValue;
			}
			set
			{
				base.SetValueInternal(TranslateTransform3D.OffsetXProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor do eixo Y do deslocamento da translação.</summary>
		/// <returns>Duplo que representa o valor do eixo y do deslocamento da translação.</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000C7C1C File Offset: 0x000C701C
		// (set) Token: 0x0600320D RID: 12813 RVA: 0x000C7C38 File Offset: 0x000C7038
		public double OffsetY
		{
			get
			{
				base.ReadPreamble();
				return this._cachedOffsetYValue;
			}
			set
			{
				base.SetValueInternal(TranslateTransform3D.OffsetYProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor do eixo Z do deslocamento da translação.</summary>
		/// <returns>Duplo que representa o valor do eixo z do deslocamento da translação.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000C7C58 File Offset: 0x000C7058
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x000C7C74 File Offset: 0x000C7074
		public double OffsetZ
		{
			get
			{
				base.ReadPreamble();
				return this._cachedOffsetZValue;
			}
			set
			{
				base.SetValueInternal(TranslateTransform3D.OffsetZProperty, value);
			}
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000C7C94 File Offset: 0x000C7094
		protected override Freezable CreateInstanceCore()
		{
			return new TranslateTransform3D();
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000C7CA8 File Offset: 0x000C70A8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(TranslateTransform3D.OffsetXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(TranslateTransform3D.OffsetYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(TranslateTransform3D.OffsetZProperty, channel);
				DUCE.MILCMD_TRANSLATETRANSFORM3D milcmd_TRANSLATETRANSFORM3D;
				milcmd_TRANSLATETRANSFORM3D.Type = MILCMD.MilCmdTranslateTransform3D;
				milcmd_TRANSLATETRANSFORM3D.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_TRANSLATETRANSFORM3D.offsetX = this.OffsetX;
				}
				milcmd_TRANSLATETRANSFORM3D.hOffsetXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_TRANSLATETRANSFORM3D.offsetY = this.OffsetY;
				}
				milcmd_TRANSLATETRANSFORM3D.hOffsetYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_TRANSLATETRANSFORM3D.offsetZ = this.OffsetZ;
				}
				milcmd_TRANSLATETRANSFORM3D.hOffsetZAnimations = animationResourceHandle3;
				channel.SendCommand((byte*)(&milcmd_TRANSLATETRANSFORM3D), sizeof(DUCE.MILCMD_TRANSLATETRANSFORM3D));
			}
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000C7D80 File Offset: 0x000C7180
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_TRANSLATETRANSFORM3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000C7DBC File Offset: 0x000C71BC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000C7DE0 File Offset: 0x000C71E0
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000C7DFC File Offset: 0x000C71FC
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000C7E14 File Offset: 0x000C7214
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000C7E30 File Offset: 0x000C7230
		static TranslateTransform3D()
		{
			Type typeFromHandle = typeof(TranslateTransform3D);
			TranslateTransform3D.OffsetXProperty = Animatable.RegisterProperty("OffsetX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(TranslateTransform3D.OffsetXPropertyChanged), null, true, null);
			TranslateTransform3D.OffsetYProperty = Animatable.RegisterProperty("OffsetY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(TranslateTransform3D.OffsetYPropertyChanged), null, true, null);
			TranslateTransform3D.OffsetZProperty = Animatable.RegisterProperty("OffsetZ", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(TranslateTransform3D.OffsetZPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.TranslateTransform3D.OffsetX" />.</summary>
		// Token: 0x040015BC RID: 5564
		public static readonly DependencyProperty OffsetXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.TranslateTransform3D.OffsetY" />.</summary>
		// Token: 0x040015BD RID: 5565
		public static readonly DependencyProperty OffsetYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.TranslateTransform3D.OffsetZ" />.</summary>
		// Token: 0x040015BE RID: 5566
		public static readonly DependencyProperty OffsetZProperty;

		// Token: 0x040015BF RID: 5567
		private double _cachedOffsetXValue;

		// Token: 0x040015C0 RID: 5568
		private double _cachedOffsetYValue;

		// Token: 0x040015C1 RID: 5569
		private double _cachedOffsetZValue;

		// Token: 0x040015C2 RID: 5570
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040015C3 RID: 5571
		internal const double c_OffsetX = 0.0;

		// Token: 0x040015C4 RID: 5572
		internal const double c_OffsetY = 0.0;

		// Token: 0x040015C5 RID: 5573
		internal const double c_OffsetZ = 0.0;
	}
}
