using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Objeto de luz que projeta seu efeito em uma área em forma de cone ao longo de uma direção especificada.</summary>
	// Token: 0x02000480 RID: 1152
	public sealed class SpotLight : PointLightBase
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.SpotLight" /> usando a cor, a posição, a direção e os ângulos de cone especificados.</summary>
		/// <param name="diffuseColor">Cor difusa do novo <see cref="T:System.Windows.Media.Media3D.SpotLight" />.</param>
		/// <param name="position">Posição do novo <see cref="T:System.Windows.Media.Media3D.SpotLight" />.</param>
		/// <param name="direction">Direção do novo <see cref="T:System.Windows.Media.Media3D.SpotLight" />.</param>
		/// <param name="outerConeAngle">Ângulo que define um cone fora do qual a luz não ilumina objetos presentes na cena.</param>
		/// <param name="innerConeAngle">Ângulo que define um cone dentro do qual a luz ilumina totalmente objetos presentes na cena.</param>
		// Token: 0x060031BC RID: 12732 RVA: 0x000C6C2C File Offset: 0x000C602C
		public SpotLight(Color diffuseColor, Point3D position, Vector3D direction, double outerConeAngle, double innerConeAngle) : this()
		{
			base.Color = diffuseColor;
			base.Position = position;
			this.Direction = direction;
			this.OuterConeAngle = outerConeAngle;
			this.InnerConeAngle = innerConeAngle;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.SpotLight" />.</summary>
		// Token: 0x060031BD RID: 12733 RVA: 0x000C6C64 File Offset: 0x000C6064
		public SpotLight()
		{
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.SpotLight" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060031BE RID: 12734 RVA: 0x000C6C78 File Offset: 0x000C6078
		public new SpotLight Clone()
		{
			return (SpotLight)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.SpotLight" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060031BF RID: 12735 RVA: 0x000C6C90 File Offset: 0x000C6090
		public new SpotLight CloneCurrentValue()
		{
			return (SpotLight)base.CloneCurrentValue();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000C6CA8 File Offset: 0x000C60A8
		private static void DirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SpotLight spotLight = (SpotLight)d;
			spotLight.PropertyChanged(SpotLight.DirectionProperty);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000C6CC8 File Offset: 0x000C60C8
		private static void OuterConeAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SpotLight spotLight = (SpotLight)d;
			spotLight.PropertyChanged(SpotLight.OuterConeAngleProperty);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000C6CE8 File Offset: 0x000C60E8
		private static void InnerConeAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SpotLight spotLight = (SpotLight)d;
			spotLight.PropertyChanged(SpotLight.InnerConeAngleProperty);
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a direção na qual o <see cref="T:System.Windows.Media.Media3D.SpotLight" /> projeta sua luz.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a direção da projeção do destaque. O valor padrão é 0,0, -1.</returns>
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000C6D08 File Offset: 0x000C6108
		// (set) Token: 0x060031C4 RID: 12740 RVA: 0x000C6D28 File Offset: 0x000C6128
		public Vector3D Direction
		{
			get
			{
				return (Vector3D)base.GetValue(SpotLight.DirectionProperty);
			}
			set
			{
				base.SetValueInternal(SpotLight.DirectionProperty, value);
			}
		}

		/// <summary>Obtém ou define um ângulo que especifica a proporção da projeção em forma de cone de um <see cref="T:System.Windows.Media.Media3D.SpotLight" /> fora da qual a luz não ilumina objetos presentes na cena.</summary>
		/// <returns>O ângulo em graus que especifica a proporção de um <see cref="T:System.Windows.Media.Media3D.SpotLight" />em forma de cone fora do qual a luz não ilumina objetos presentes na cena de projeção. O valor padrão é 90.</returns>
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000C6D48 File Offset: 0x000C6148
		// (set) Token: 0x060031C6 RID: 12742 RVA: 0x000C6D68 File Offset: 0x000C6168
		public double OuterConeAngle
		{
			get
			{
				return (double)base.GetValue(SpotLight.OuterConeAngleProperty);
			}
			set
			{
				base.SetValueInternal(SpotLight.OuterConeAngleProperty, value);
			}
		}

		/// <summary>Obtém ou define um ângulo que especifica a proporção da projeção em forma de cone de um <see cref="T:System.Windows.Media.Media3D.SpotLight" /> na qual a luz ilumina totalmente objetos presentes na cena.</summary>
		/// <returns>O ângulo em graus que especifica a proporção de um <see cref="T:System.Windows.Media.Media3D.SpotLight" />em forma de cone projeção no qual a luz ilumina totalmente objetos presentes na cena. O valor padrão é 180.</returns>
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000C6D88 File Offset: 0x000C6188
		// (set) Token: 0x060031C8 RID: 12744 RVA: 0x000C6DA8 File Offset: 0x000C61A8
		public double InnerConeAngle
		{
			get
			{
				return (double)base.GetValue(SpotLight.InnerConeAngleProperty);
			}
			set
			{
				base.SetValueInternal(SpotLight.InnerConeAngleProperty, value);
			}
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000C6DC8 File Offset: 0x000C61C8
		protected override Freezable CreateInstanceCore()
		{
			return new SpotLight();
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000C6DDC File Offset: 0x000C61DC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform3D transform = base.Transform;
				DUCE.ResourceHandle htransform;
				if (transform == null || transform == Transform3D.Identity)
				{
					htransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					htransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Light.ColorProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(PointLightBase.PositionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(PointLightBase.RangeProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(PointLightBase.ConstantAttenuationProperty, channel);
				DUCE.ResourceHandle animationResourceHandle5 = base.GetAnimationResourceHandle(PointLightBase.LinearAttenuationProperty, channel);
				DUCE.ResourceHandle animationResourceHandle6 = base.GetAnimationResourceHandle(PointLightBase.QuadraticAttenuationProperty, channel);
				DUCE.ResourceHandle animationResourceHandle7 = base.GetAnimationResourceHandle(SpotLight.DirectionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle8 = base.GetAnimationResourceHandle(SpotLight.OuterConeAngleProperty, channel);
				DUCE.ResourceHandle animationResourceHandle9 = base.GetAnimationResourceHandle(SpotLight.InnerConeAngleProperty, channel);
				DUCE.MILCMD_SPOTLIGHT milcmd_SPOTLIGHT;
				milcmd_SPOTLIGHT.Type = MILCMD.MilCmdSpotLight;
				milcmd_SPOTLIGHT.Handle = this._duceResource.GetHandle(channel);
				milcmd_SPOTLIGHT.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_SPOTLIGHT.color = CompositionResourceManager.ColorToMilColorF(base.Color);
				}
				milcmd_SPOTLIGHT.hColorAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_SPOTLIGHT.position = CompositionResourceManager.Point3DToMilPoint3F(base.Position);
				}
				milcmd_SPOTLIGHT.hPositionAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_SPOTLIGHT.range = base.Range;
				}
				milcmd_SPOTLIGHT.hRangeAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_SPOTLIGHT.constantAttenuation = base.ConstantAttenuation;
				}
				milcmd_SPOTLIGHT.hConstantAttenuationAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_SPOTLIGHT.linearAttenuation = base.LinearAttenuation;
				}
				milcmd_SPOTLIGHT.hLinearAttenuationAnimations = animationResourceHandle5;
				if (animationResourceHandle6.IsNull)
				{
					milcmd_SPOTLIGHT.quadraticAttenuation = base.QuadraticAttenuation;
				}
				milcmd_SPOTLIGHT.hQuadraticAttenuationAnimations = animationResourceHandle6;
				if (animationResourceHandle7.IsNull)
				{
					milcmd_SPOTLIGHT.direction = CompositionResourceManager.Vector3DToMilPoint3F(this.Direction);
				}
				milcmd_SPOTLIGHT.hDirectionAnimations = animationResourceHandle7;
				if (animationResourceHandle8.IsNull)
				{
					milcmd_SPOTLIGHT.outerConeAngle = this.OuterConeAngle;
				}
				milcmd_SPOTLIGHT.hOuterConeAngleAnimations = animationResourceHandle8;
				if (animationResourceHandle9.IsNull)
				{
					milcmd_SPOTLIGHT.innerConeAngle = this.InnerConeAngle;
				}
				milcmd_SPOTLIGHT.hInnerConeAngleAnimations = animationResourceHandle9;
				channel.SendCommand((byte*)(&milcmd_SPOTLIGHT), sizeof(DUCE.MILCMD_SPOTLIGHT));
			}
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000C6FFC File Offset: 0x000C63FC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SPOTLIGHT))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000C7048 File Offset: 0x000C6448
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000C707C File Offset: 0x000C647C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000C7098 File Offset: 0x000C6498
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000C70B0 File Offset: 0x000C64B0
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000C70CC File Offset: 0x000C64CC
		static SpotLight()
		{
			Type typeFromHandle = typeof(SpotLight);
			SpotLight.DirectionProperty = Animatable.RegisterProperty("Direction", typeof(Vector3D), typeFromHandle, new Vector3D(0.0, 0.0, -1.0), new PropertyChangedCallback(SpotLight.DirectionPropertyChanged), null, true, null);
			SpotLight.OuterConeAngleProperty = Animatable.RegisterProperty("OuterConeAngle", typeof(double), typeFromHandle, 90.0, new PropertyChangedCallback(SpotLight.OuterConeAnglePropertyChanged), null, true, null);
			SpotLight.InnerConeAngleProperty = Animatable.RegisterProperty("InnerConeAngle", typeof(double), typeFromHandle, 180.0, new PropertyChangedCallback(SpotLight.InnerConeAnglePropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpotLight.Direction" />.</summary>
		// Token: 0x040015B1 RID: 5553
		public static readonly DependencyProperty DirectionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpotLight.OuterConeAngle" />.</summary>
		// Token: 0x040015B2 RID: 5554
		public static readonly DependencyProperty OuterConeAngleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpotLight.InnerConeAngle" />.</summary>
		// Token: 0x040015B3 RID: 5555
		public static readonly DependencyProperty InnerConeAngleProperty;

		// Token: 0x040015B4 RID: 5556
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040015B5 RID: 5557
		internal static Vector3D s_Direction = new Vector3D(0.0, 0.0, -1.0);

		// Token: 0x040015B6 RID: 5558
		internal const double c_OuterConeAngle = 90.0;

		// Token: 0x040015B7 RID: 5559
		internal const double c_InnerConeAngle = 180.0;
	}
}
