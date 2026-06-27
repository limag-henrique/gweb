using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma fonte de luz que tem uma posição especificada no espaço e projeta sua luz em todos os trajetos.</summary>
	// Token: 0x02000471 RID: 1137
	public sealed class PointLight : PointLightBase
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.PointLight" /> na origem.</summary>
		// Token: 0x0600306B RID: 12395 RVA: 0x000C15A0 File Offset: 0x000C09A0
		public PointLight()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.PointLight" /> na posição especificada, usando a cor especificada.</summary>
		/// <param name="diffuseColor">A cor difusa.</param>
		/// <param name="position">A posição.</param>
		// Token: 0x0600306C RID: 12396 RVA: 0x000C15B4 File Offset: 0x000C09B4
		public PointLight(Color diffuseColor, Point3D position) : this()
		{
			base.Color = diffuseColor;
			base.Position = position;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.PointLight" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600306D RID: 12397 RVA: 0x000C15D8 File Offset: 0x000C09D8
		public new PointLight Clone()
		{
			return (PointLight)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.PointLight" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600306E RID: 12398 RVA: 0x000C15F0 File Offset: 0x000C09F0
		public new PointLight CloneCurrentValue()
		{
			return (PointLight)base.CloneCurrentValue();
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000C1608 File Offset: 0x000C0A08
		protected override Freezable CreateInstanceCore()
		{
			return new PointLight();
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000C161C File Offset: 0x000C0A1C
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
				DUCE.MILCMD_POINTLIGHT milcmd_POINTLIGHT;
				milcmd_POINTLIGHT.Type = MILCMD.MilCmdPointLight;
				milcmd_POINTLIGHT.Handle = this._duceResource.GetHandle(channel);
				milcmd_POINTLIGHT.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_POINTLIGHT.color = CompositionResourceManager.ColorToMilColorF(base.Color);
				}
				milcmd_POINTLIGHT.hColorAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_POINTLIGHT.position = CompositionResourceManager.Point3DToMilPoint3F(base.Position);
				}
				milcmd_POINTLIGHT.hPositionAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_POINTLIGHT.range = base.Range;
				}
				milcmd_POINTLIGHT.hRangeAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_POINTLIGHT.constantAttenuation = base.ConstantAttenuation;
				}
				milcmd_POINTLIGHT.hConstantAttenuationAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_POINTLIGHT.linearAttenuation = base.LinearAttenuation;
				}
				milcmd_POINTLIGHT.hLinearAttenuationAnimations = animationResourceHandle5;
				if (animationResourceHandle6.IsNull)
				{
					milcmd_POINTLIGHT.quadraticAttenuation = base.QuadraticAttenuation;
				}
				milcmd_POINTLIGHT.hQuadraticAttenuationAnimations = animationResourceHandle6;
				channel.SendCommand((byte*)(&milcmd_POINTLIGHT), sizeof(DUCE.MILCMD_POINTLIGHT));
			}
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000C17B0 File Offset: 0x000C0BB0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_POINTLIGHT))
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

		// Token: 0x06003072 RID: 12402 RVA: 0x000C17FC File Offset: 0x000C0BFC
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

		// Token: 0x06003073 RID: 12403 RVA: 0x000C1830 File Offset: 0x000C0C30
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000C184C File Offset: 0x000C0C4C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000C1864 File Offset: 0x000C0C64
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0400154C RID: 5452
		internal DUCE.MultiChannelResource _duceResource;
	}
}
