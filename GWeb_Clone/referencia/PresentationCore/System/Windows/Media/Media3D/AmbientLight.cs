using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Objeto de luz que aplica luz uniformemente aos objetos, independentemente de suas formas.</summary>
	// Token: 0x02000452 RID: 1106
	public sealed class AmbientLight : Light
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.AmbientLight" />.</summary>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B773C File Offset: 0x000B6B3C
		public AmbientLight() : this(Colors.White)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.AmbientLight" /> com a cor especificada.</summary>
		/// <param name="ambientColor">Cor da nova luz.</param>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B7754 File Offset: 0x000B6B54
		public AmbientLight(Color ambientColor)
		{
			base.Color = ambientColor;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.AmbientLight" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000B7770 File Offset: 0x000B6B70
		public new AmbientLight Clone()
		{
			return (AmbientLight)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.AmbientLight" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002DD6 RID: 11734 RVA: 0x000B7788 File Offset: 0x000B6B88
		public new AmbientLight CloneCurrentValue()
		{
			return (AmbientLight)base.CloneCurrentValue();
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B77A0 File Offset: 0x000B6BA0
		protected override Freezable CreateInstanceCore()
		{
			return new AmbientLight();
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B77B4 File Offset: 0x000B6BB4
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
				DUCE.MILCMD_AMBIENTLIGHT milcmd_AMBIENTLIGHT;
				milcmd_AMBIENTLIGHT.Type = MILCMD.MilCmdAmbientLight;
				milcmd_AMBIENTLIGHT.Handle = this._duceResource.GetHandle(channel);
				milcmd_AMBIENTLIGHT.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_AMBIENTLIGHT.color = CompositionResourceManager.ColorToMilColorF(base.Color);
				}
				milcmd_AMBIENTLIGHT.hColorAnimations = animationResourceHandle;
				channel.SendCommand((byte*)(&milcmd_AMBIENTLIGHT), sizeof(DUCE.MILCMD_AMBIENTLIGHT));
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B7864 File Offset: 0x000B6C64
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_AMBIENTLIGHT))
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

		// Token: 0x06002DDA RID: 11738 RVA: 0x000B78B0 File Offset: 0x000B6CB0
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

		// Token: 0x06002DDB RID: 11739 RVA: 0x000B78E4 File Offset: 0x000B6CE4
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000B7900 File Offset: 0x000B6D00
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000B7918 File Offset: 0x000B6D18
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000B7934 File Offset: 0x000B6D34
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x040014DB RID: 5339
		internal DUCE.MultiChannelResource _duceResource;
	}
}
