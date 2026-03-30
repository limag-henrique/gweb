using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma rotação 3D de um ângulo especificado sobre um eixo especificado.</summary>
	// Token: 0x02000453 RID: 1107
	public sealed class AxisAngleRotation3D : Rotation3D
	{
		/// <summary>Cria uma instância de uma rotação 3D.</summary>
		// Token: 0x06002DDF RID: 11743 RVA: 0x000B7944 File Offset: 0x000B6D44
		public AxisAngleRotation3D()
		{
		}

		/// <summary>Cria uma instância de uma rotação 3D usando o eixo e o ângulo especificados.</summary>
		/// <param name="axis">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica o eixo em torno do qual girar.</param>
		/// <param name="angle">Duplo que especifica o ângulo de rotação, em graus.</param>
		// Token: 0x06002DE0 RID: 11744 RVA: 0x000B7964 File Offset: 0x000B6D64
		public AxisAngleRotation3D(Vector3D axis, double angle)
		{
			this.Axis = axis;
			this.Angle = angle;
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000B7990 File Offset: 0x000B6D90
		internal override Quaternion InternalQuaternion
		{
			get
			{
				if (this._cachedQuaternionValue == AxisAngleRotation3D.c_dirtyQuaternion)
				{
					Vector3D axis = this.Axis;
					if (axis.LengthSquared > 1.1754943508222875E-38)
					{
						this._cachedQuaternionValue = new Quaternion(axis, this.Angle);
					}
					else
					{
						this._cachedQuaternionValue = Quaternion.Identity;
					}
				}
				return this._cachedQuaternionValue;
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000B79F0 File Offset: 0x000B6DF0
		internal void AxisPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			this._cachedQuaternionValue = AxisAngleRotation3D.c_dirtyQuaternion;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000B7A08 File Offset: 0x000B6E08
		internal void AnglePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			this._cachedQuaternionValue = AxisAngleRotation3D.c_dirtyQuaternion;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.AxisAngleRotation3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000B7A20 File Offset: 0x000B6E20
		public new AxisAngleRotation3D Clone()
		{
			return (AxisAngleRotation3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.AxisAngleRotation3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000B7A38 File Offset: 0x000B6E38
		public new AxisAngleRotation3D CloneCurrentValue()
		{
			return (AxisAngleRotation3D)base.CloneCurrentValue();
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000B7A50 File Offset: 0x000B6E50
		private static void AxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AxisAngleRotation3D axisAngleRotation3D = (AxisAngleRotation3D)d;
			axisAngleRotation3D.AxisPropertyChangedHook(e);
			axisAngleRotation3D.PropertyChanged(AxisAngleRotation3D.AxisProperty);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B7A78 File Offset: 0x000B6E78
		private static void AnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AxisAngleRotation3D axisAngleRotation3D = (AxisAngleRotation3D)d;
			axisAngleRotation3D.AnglePropertyChangedHook(e);
			axisAngleRotation3D.PropertyChanged(AxisAngleRotation3D.AngleProperty);
		}

		/// <summary>Obtém ou define o eixo de uma rotação 3D.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica o eixo de rotação.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000B7AA0 File Offset: 0x000B6EA0
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x000B7AC0 File Offset: 0x000B6EC0
		public Vector3D Axis
		{
			get
			{
				return (Vector3D)base.GetValue(AxisAngleRotation3D.AxisProperty);
			}
			set
			{
				base.SetValueInternal(AxisAngleRotation3D.AxisProperty, value);
			}
		}

		/// <summary>Obtém ou define o ângulo de uma rotação 3D, em graus.</summary>
		/// <returns>Duplo que especifica o ângulo de uma rotação 3D, em graus.</returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000B7AE0 File Offset: 0x000B6EE0
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x000B7B00 File Offset: 0x000B6F00
		public double Angle
		{
			get
			{
				return (double)base.GetValue(AxisAngleRotation3D.AngleProperty);
			}
			set
			{
				base.SetValueInternal(AxisAngleRotation3D.AngleProperty, value);
			}
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000B7B20 File Offset: 0x000B6F20
		protected override Freezable CreateInstanceCore()
		{
			return new AxisAngleRotation3D();
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000B7B34 File Offset: 0x000B6F34
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(AxisAngleRotation3D.AxisProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(AxisAngleRotation3D.AngleProperty, channel);
				DUCE.MILCMD_AXISANGLEROTATION3D milcmd_AXISANGLEROTATION3D;
				milcmd_AXISANGLEROTATION3D.Type = MILCMD.MilCmdAxisAngleRotation3D;
				milcmd_AXISANGLEROTATION3D.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_AXISANGLEROTATION3D.axis = CompositionResourceManager.Vector3DToMilPoint3F(this.Axis);
				}
				milcmd_AXISANGLEROTATION3D.hAxisAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_AXISANGLEROTATION3D.angle = this.Angle;
				}
				milcmd_AXISANGLEROTATION3D.hAngleAnimations = animationResourceHandle2;
				channel.SendCommand((byte*)(&milcmd_AXISANGLEROTATION3D), sizeof(DUCE.MILCMD_AXISANGLEROTATION3D));
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B7BE4 File Offset: 0x000B6FE4
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_AXISANGLEROTATION3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B7C1C File Offset: 0x000B701C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B7C40 File Offset: 0x000B7040
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B7C5C File Offset: 0x000B705C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B7C74 File Offset: 0x000B7074
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000B7C90 File Offset: 0x000B7090
		static AxisAngleRotation3D()
		{
			Type typeFromHandle = typeof(AxisAngleRotation3D);
			AxisAngleRotation3D.AxisProperty = Animatable.RegisterProperty("Axis", typeof(Vector3D), typeFromHandle, new Vector3D(0.0, 1.0, 0.0), new PropertyChangedCallback(AxisAngleRotation3D.AxisPropertyChanged), null, true, null);
			AxisAngleRotation3D.AngleProperty = Animatable.RegisterProperty("Angle", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(AxisAngleRotation3D.AnglePropertyChanged), null, true, null);
		}

		// Token: 0x040014DC RID: 5340
		private Quaternion _cachedQuaternionValue = AxisAngleRotation3D.c_dirtyQuaternion;

		// Token: 0x040014DD RID: 5341
		internal static readonly Quaternion c_dirtyQuaternion = new Quaternion(2.7182818284590451, 3.1415926535897931, 8.539734222673566, 55.0);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.AxisAngleRotation3D.Axis" />.</summary>
		// Token: 0x040014DE RID: 5342
		public static readonly DependencyProperty AxisProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.AxisAngleRotation3D.Angle" />.</summary>
		// Token: 0x040014DF RID: 5343
		public static readonly DependencyProperty AngleProperty;

		// Token: 0x040014E0 RID: 5344
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040014E1 RID: 5345
		internal static Vector3D s_Axis = new Vector3D(0.0, 1.0, 0.0);

		// Token: 0x040014E2 RID: 5346
		internal const double c_Angle = 0.0;
	}
}
