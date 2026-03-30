using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Objeto de luz que projeta seu efeito ao longo de uma direção especificada por um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
	// Token: 0x02000457 RID: 1111
	public sealed class DirectionalLight : Light
	{
		/// <summary>Cria uma instância de uma luz que projeta seu efeito em uma direção especificada.</summary>
		// Token: 0x06002E29 RID: 11817 RVA: 0x000B865C File Offset: 0x000B7A5C
		public DirectionalLight()
		{
		}

		/// <summary>Cria uma instância de uma luz que projeta seu efeito ao longo de um Vector3D especificado com uma cor especificada.</summary>
		/// <param name="diffuseColor">Cor difusa da nova luz.</param>
		/// <param name="direction">Direção da nova luz.</param>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000B8670 File Offset: 0x000B7A70
		public DirectionalLight(Color diffuseColor, Vector3D direction)
		{
			base.Color = diffuseColor;
			this.Direction = direction;
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Media3D.DirectionalLight" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000B8694 File Offset: 0x000B7A94
		public new DirectionalLight Clone()
		{
			return (DirectionalLight)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.DirectionalLight" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002E2C RID: 11820 RVA: 0x000B86AC File Offset: 0x000B7AAC
		public new DirectionalLight CloneCurrentValue()
		{
			return (DirectionalLight)base.CloneCurrentValue();
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000B86C4 File Offset: 0x000B7AC4
		private static void DirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DirectionalLight directionalLight = (DirectionalLight)d;
			directionalLight.PropertyChanged(DirectionalLight.DirectionProperty);
		}

		/// <summary>Representa o vector ao longo do qual o efeito da luz será visto em modelos em uma cena 3D.</summary>
		/// <returns>Vector3D ao longo do qual os projetos de luz e que deve ter uma magnitude diferente de zero. O valor padrão é (0,0, -1).</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000B86E4 File Offset: 0x000B7AE4
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x000B8704 File Offset: 0x000B7B04
		public Vector3D Direction
		{
			get
			{
				return (Vector3D)base.GetValue(DirectionalLight.DirectionProperty);
			}
			set
			{
				base.SetValueInternal(DirectionalLight.DirectionProperty, value);
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B8724 File Offset: 0x000B7B24
		protected override Freezable CreateInstanceCore()
		{
			return new DirectionalLight();
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B8738 File Offset: 0x000B7B38
		[SecurityTreatAsSafe]
		[SecurityCritical]
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
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(DirectionalLight.DirectionProperty, channel);
				DUCE.MILCMD_DIRECTIONALLIGHT milcmd_DIRECTIONALLIGHT;
				milcmd_DIRECTIONALLIGHT.Type = MILCMD.MilCmdDirectionalLight;
				milcmd_DIRECTIONALLIGHT.Handle = this._duceResource.GetHandle(channel);
				milcmd_DIRECTIONALLIGHT.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_DIRECTIONALLIGHT.color = CompositionResourceManager.ColorToMilColorF(base.Color);
				}
				milcmd_DIRECTIONALLIGHT.hColorAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_DIRECTIONALLIGHT.direction = CompositionResourceManager.Vector3DToMilPoint3F(this.Direction);
				}
				milcmd_DIRECTIONALLIGHT.hDirectionAnimations = animationResourceHandle2;
				channel.SendCommand((byte*)(&milcmd_DIRECTIONALLIGHT), sizeof(DUCE.MILCMD_DIRECTIONALLIGHT));
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B8818 File Offset: 0x000B7C18
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DIRECTIONALLIGHT))
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

		// Token: 0x06002E33 RID: 11827 RVA: 0x000B8864 File Offset: 0x000B7C64
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

		// Token: 0x06002E34 RID: 11828 RVA: 0x000B8898 File Offset: 0x000B7C98
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000B88B4 File Offset: 0x000B7CB4
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000B88CC File Offset: 0x000B7CCC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000B88E8 File Offset: 0x000B7CE8
		static DirectionalLight()
		{
			Type typeFromHandle = typeof(DirectionalLight);
			DirectionalLight.DirectionProperty = Animatable.RegisterProperty("Direction", typeof(Vector3D), typeFromHandle, new Vector3D(0.0, 0.0, -1.0), new PropertyChangedCallback(DirectionalLight.DirectionPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.DirectionalLight.Direction" />.</summary>
		// Token: 0x040014ED RID: 5357
		public static readonly DependencyProperty DirectionProperty;

		// Token: 0x040014EE RID: 5358
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040014EF RID: 5359
		internal static Vector3D s_Direction = new Vector3D(0.0, 0.0, -1.0);
	}
}
