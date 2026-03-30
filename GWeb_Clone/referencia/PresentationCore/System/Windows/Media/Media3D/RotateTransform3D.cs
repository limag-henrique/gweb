using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Media3D
{
	/// <summary>Especifica uma transformação de rotação.</summary>
	// Token: 0x0200047C RID: 1148
	public sealed class RotateTransform3D : AffineTransform3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" />.</summary>
		// Token: 0x0600314F RID: 12623 RVA: 0x000C5344 File Offset: 0x000C4744
		public RotateTransform3D()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" /> com a rotação especificada.</summary>
		/// <param name="rotation">O Rotation3D que especifica a rotação.</param>
		// Token: 0x06003150 RID: 12624 RVA: 0x000C5364 File Offset: 0x000C4764
		public RotateTransform3D(Rotation3D rotation)
		{
			this.Rotation = rotation;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" /> com o centro e a rotação especificados.</summary>
		/// <param name="rotation">O Rotation3D que especifica a rotação.</param>
		/// <param name="center">O centro da rotação da transformação.</param>
		// Token: 0x06003151 RID: 12625 RVA: 0x000C538C File Offset: 0x000C478C
		public RotateTransform3D(Rotation3D rotation, Point3D center)
		{
			this.Rotation = rotation;
			this.CenterX = center.X;
			this.CenterY = center.Y;
			this.CenterZ = center.Z;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" /> usando as coordenadas de rotação e de ponto central especificadas.</summary>
		/// <param name="rotation">O Rotation3D que especifica a rotação.</param>
		/// <param name="centerX">Double que especifica o valor de X sobre o qual girar.</param>
		/// <param name="centerY">Double que especifica o valor de Y sobre o qual girar.</param>
		/// <param name="centerZ">Double que especifica o valor de Z sobre o qual girar.</param>
		// Token: 0x06003152 RID: 12626 RVA: 0x000C53D8 File Offset: 0x000C47D8
		public RotateTransform3D(Rotation3D rotation, double centerX, double centerY, double centerZ)
		{
			this.Rotation = rotation;
			this.CenterX = centerX;
			this.CenterY = centerY;
			this.CenterZ = centerZ;
		}

		/// <summary>Recupera um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que representa a rotação.</summary>
		/// <returns>Matrix3D que represente a rotação.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000C5414 File Offset: 0x000C4814
		public override Matrix3D Value
		{
			get
			{
				base.ReadPreamble();
				Rotation3D cachedRotationValue = this._cachedRotationValue;
				if (cachedRotationValue == null)
				{
					return Matrix3D.Identity;
				}
				Quaternion internalQuaternion = cachedRotationValue.InternalQuaternion;
				Point3D point3D = new Point3D(this._cachedCenterXValue, this._cachedCenterYValue, this._cachedCenterZValue);
				return Matrix3D.CreateRotationMatrix(ref internalQuaternion, ref point3D);
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000C5460 File Offset: 0x000C4860
		internal override void Append(ref Matrix3D matrix)
		{
			matrix *= this.Value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003155 RID: 12629 RVA: 0x000C5484 File Offset: 0x000C4884
		public new RotateTransform3D Clone()
		{
			return (RotateTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.RotateTransform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003156 RID: 12630 RVA: 0x000C549C File Offset: 0x000C489C
		public new RotateTransform3D CloneCurrentValue()
		{
			return (RotateTransform3D)base.CloneCurrentValue();
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000C54B4 File Offset: 0x000C48B4
		private static void CenterXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform3D rotateTransform3D = (RotateTransform3D)d;
			rotateTransform3D._cachedCenterXValue = (double)e.NewValue;
			rotateTransform3D.PropertyChanged(RotateTransform3D.CenterXProperty);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000C54E8 File Offset: 0x000C48E8
		private static void CenterYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform3D rotateTransform3D = (RotateTransform3D)d;
			rotateTransform3D._cachedCenterYValue = (double)e.NewValue;
			rotateTransform3D.PropertyChanged(RotateTransform3D.CenterYProperty);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000C551C File Offset: 0x000C491C
		private static void CenterZPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RotateTransform3D rotateTransform3D = (RotateTransform3D)d;
			rotateTransform3D._cachedCenterZValue = (double)e.NewValue;
			rotateTransform3D.PropertyChanged(RotateTransform3D.CenterZProperty);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000C5550 File Offset: 0x000C4950
		private static void RotationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			RotateTransform3D rotateTransform3D = (RotateTransform3D)d;
			rotateTransform3D._cachedRotationValue = (Rotation3D)e.NewValue;
			Rotation3D resource = (Rotation3D)e.OldValue;
			Rotation3D resource2 = (Rotation3D)e.NewValue;
			Dispatcher dispatcher = rotateTransform3D.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = rotateTransform3D;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						rotateTransform3D.ReleaseResource(resource, channel);
						rotateTransform3D.AddRefResource(resource2, channel);
					}
				}
			}
			rotateTransform3D.PropertyChanged(RotateTransform3D.RotationProperty);
		}

		/// <summary>Obtém ou define a coordenada X do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre o qual girar.</summary>
		/// <returns>Valor duplo que representa a coordenada X do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre qual girar.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000C5628 File Offset: 0x000C4A28
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x000C5644 File Offset: 0x000C4A44
		public double CenterX
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterXValue;
			}
			set
			{
				base.SetValueInternal(RotateTransform3D.CenterXProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada Y do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre o qual girar.</summary>
		/// <returns>Valor duplo que representa a coordenada Y do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre qual girar.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000C5664 File Offset: 0x000C4A64
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000C5680 File Offset: 0x000C4A80
		public double CenterY
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterYValue;
			}
			set
			{
				base.SetValueInternal(RotateTransform3D.CenterYProperty, value);
			}
		}

		/// <summary>Obtém ou define a coordenada Z do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre o qual girar.</summary>
		/// <returns>Duplo que representa a coordenada Z do <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre qual girar.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000C56A0 File Offset: 0x000C4AA0
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x000C56BC File Offset: 0x000C4ABC
		public double CenterZ
		{
			get
			{
				base.ReadPreamble();
				return this._cachedCenterZValue;
			}
			set
			{
				base.SetValueInternal(RotateTransform3D.CenterZProperty, value);
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> que especifica a rotação.</summary>
		/// <returns>Rotation3D que especifica um ângulo de rotação sobre um eixo.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000C56DC File Offset: 0x000C4ADC
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x000C56F8 File Offset: 0x000C4AF8
		public Rotation3D Rotation
		{
			get
			{
				base.ReadPreamble();
				return this._cachedRotationValue;
			}
			set
			{
				base.SetValueInternal(RotateTransform3D.RotationProperty, value);
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000C5714 File Offset: 0x000C4B14
		protected override Freezable CreateInstanceCore()
		{
			return new RotateTransform3D();
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000C5728 File Offset: 0x000C4B28
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Rotation3D rotation = this.Rotation;
				DUCE.ResourceHandle hrotation = (rotation != null) ? ((DUCE.IResource)rotation).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(RotateTransform3D.CenterXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(RotateTransform3D.CenterYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(RotateTransform3D.CenterZProperty, channel);
				DUCE.MILCMD_ROTATETRANSFORM3D milcmd_ROTATETRANSFORM3D;
				milcmd_ROTATETRANSFORM3D.Type = MILCMD.MilCmdRotateTransform3D;
				milcmd_ROTATETRANSFORM3D.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_ROTATETRANSFORM3D.centerX = this.CenterX;
				}
				milcmd_ROTATETRANSFORM3D.hCenterXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_ROTATETRANSFORM3D.centerY = this.CenterY;
				}
				milcmd_ROTATETRANSFORM3D.hCenterYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_ROTATETRANSFORM3D.centerZ = this.CenterZ;
				}
				milcmd_ROTATETRANSFORM3D.hCenterZAnimations = animationResourceHandle3;
				milcmd_ROTATETRANSFORM3D.hrotation = hrotation;
				channel.SendCommand((byte*)(&milcmd_ROTATETRANSFORM3D), sizeof(DUCE.MILCMD_ROTATETRANSFORM3D));
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000C5820 File Offset: 0x000C4C20
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_ROTATETRANSFORM3D))
			{
				Rotation3D rotation = this.Rotation;
				if (rotation != null)
				{
					((DUCE.IResource)rotation).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000C586C File Offset: 0x000C4C6C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Rotation3D rotation = this.Rotation;
				if (rotation != null)
				{
					((DUCE.IResource)rotation).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000C58A0 File Offset: 0x000C4CA0
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000C58BC File Offset: 0x000C4CBC
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000C58D4 File Offset: 0x000C4CD4
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000C58F0 File Offset: 0x000C4CF0
		static RotateTransform3D()
		{
			Type typeFromHandle = typeof(RotateTransform3D);
			RotateTransform3D.CenterXProperty = Animatable.RegisterProperty("CenterX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform3D.CenterXPropertyChanged), null, true, null);
			RotateTransform3D.CenterYProperty = Animatable.RegisterProperty("CenterY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform3D.CenterYPropertyChanged), null, true, null);
			RotateTransform3D.CenterZProperty = Animatable.RegisterProperty("CenterZ", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RotateTransform3D.CenterZPropertyChanged), null, true, null);
			RotateTransform3D.RotationProperty = Animatable.RegisterProperty("Rotation", typeof(Rotation3D), typeFromHandle, Rotation3D.Identity, new PropertyChangedCallback(RotateTransform3D.RotationPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.RotateTransform3D.CenterX" />.</summary>
		// Token: 0x04001586 RID: 5510
		public static readonly DependencyProperty CenterXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.RotateTransform3D.CenterY" />.</summary>
		// Token: 0x04001587 RID: 5511
		public static readonly DependencyProperty CenterYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.RotateTransform3D.CenterZ" />.</summary>
		// Token: 0x04001588 RID: 5512
		public static readonly DependencyProperty CenterZProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.RotateTransform3D.Rotation" />.</summary>
		// Token: 0x04001589 RID: 5513
		public static readonly DependencyProperty RotationProperty;

		// Token: 0x0400158A RID: 5514
		private double _cachedCenterXValue;

		// Token: 0x0400158B RID: 5515
		private double _cachedCenterYValue;

		// Token: 0x0400158C RID: 5516
		private double _cachedCenterZValue;

		// Token: 0x0400158D RID: 5517
		private Rotation3D _cachedRotationValue = Rotation3D.Identity;

		// Token: 0x0400158E RID: 5518
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400158F RID: 5519
		internal const double c_CenterX = 0.0;

		// Token: 0x04001590 RID: 5520
		internal const double c_CenterY = 0.0;

		// Token: 0x04001591 RID: 5521
		internal const double c_CenterZ = 0.0;

		// Token: 0x04001592 RID: 5522
		internal static Rotation3D s_Rotation = Rotation3D.Identity;
	}
}
