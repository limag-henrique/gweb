using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma transformação de rotação definida como um quatérnion.</summary>
	// Token: 0x02000477 RID: 1143
	public sealed class QuaternionRotation3D : Rotation3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.QuaternionRotation3D" />.</summary>
		// Token: 0x060030F0 RID: 12528 RVA: 0x000C3E80 File Offset: 0x000C3280
		public QuaternionRotation3D()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.QuaternionRotation3D" /> usando o <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado.</summary>
		/// <param name="quaternion">O Quaternion que especifica a rotação para a qual interpolar.</param>
		// Token: 0x060030F1 RID: 12529 RVA: 0x000C3EA0 File Offset: 0x000C32A0
		public QuaternionRotation3D(Quaternion quaternion)
		{
			this.Quaternion = quaternion;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000C3EC8 File Offset: 0x000C32C8
		internal override Quaternion InternalQuaternion
		{
			get
			{
				return this._cachedQuaternionValue;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.QuaternionRotation3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060030F3 RID: 12531 RVA: 0x000C3EDC File Offset: 0x000C32DC
		public new QuaternionRotation3D Clone()
		{
			return (QuaternionRotation3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.QuaternionRotation3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060030F4 RID: 12532 RVA: 0x000C3EF4 File Offset: 0x000C32F4
		public new QuaternionRotation3D CloneCurrentValue()
		{
			return (QuaternionRotation3D)base.CloneCurrentValue();
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000C3F0C File Offset: 0x000C330C
		private static void QuaternionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			QuaternionRotation3D quaternionRotation3D = (QuaternionRotation3D)d;
			quaternionRotation3D._cachedQuaternionValue = (Quaternion)e.NewValue;
			quaternionRotation3D.PropertyChanged(QuaternionRotation3D.QuaternionProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que define a rotação de destino.</summary>
		/// <returns>Quaternion que define a rotação de destino.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000C3F40 File Offset: 0x000C3340
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x000C3F5C File Offset: 0x000C335C
		public Quaternion Quaternion
		{
			get
			{
				base.ReadPreamble();
				return this._cachedQuaternionValue;
			}
			set
			{
				base.SetValueInternal(QuaternionRotation3D.QuaternionProperty, value);
			}
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000C3F7C File Offset: 0x000C337C
		protected override Freezable CreateInstanceCore()
		{
			return new QuaternionRotation3D();
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000C3F90 File Offset: 0x000C3390
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(QuaternionRotation3D.QuaternionProperty, channel);
				DUCE.MILCMD_QUATERNIONROTATION3D milcmd_QUATERNIONROTATION3D;
				milcmd_QUATERNIONROTATION3D.Type = MILCMD.MilCmdQuaternionRotation3D;
				milcmd_QUATERNIONROTATION3D.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_QUATERNIONROTATION3D.quaternion = CompositionResourceManager.QuaternionToMilQuaternionF(this.Quaternion);
				}
				milcmd_QUATERNIONROTATION3D.hQuaternionAnimations = animationResourceHandle;
				channel.SendCommand((byte*)(&milcmd_QUATERNIONROTATION3D), sizeof(DUCE.MILCMD_QUATERNIONROTATION3D));
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000C4014 File Offset: 0x000C3414
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_QUATERNIONROTATION3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000C404C File Offset: 0x000C344C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000C4070 File Offset: 0x000C3470
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000C408C File Offset: 0x000C348C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000C40A4 File Offset: 0x000C34A4
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000C40C0 File Offset: 0x000C34C0
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000C40D0 File Offset: 0x000C34D0
		static QuaternionRotation3D()
		{
			Type typeFromHandle = typeof(QuaternionRotation3D);
			QuaternionRotation3D.QuaternionProperty = Animatable.RegisterProperty("Quaternion", typeof(Quaternion), typeFromHandle, Quaternion.Identity, new PropertyChangedCallback(QuaternionRotation3D.QuaternionPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.QuaternionRotation3D.Quaternion" />.</summary>
		// Token: 0x0400156E RID: 5486
		public static readonly DependencyProperty QuaternionProperty;

		// Token: 0x0400156F RID: 5487
		private Quaternion _cachedQuaternionValue = Quaternion.Identity;

		// Token: 0x04001570 RID: 5488
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001571 RID: 5489
		internal static Quaternion s_Quaternion = Quaternion.Identity;
	}
}
