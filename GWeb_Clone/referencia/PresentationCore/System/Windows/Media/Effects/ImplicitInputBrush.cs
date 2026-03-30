using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Effects
{
	// Token: 0x02000615 RID: 1557
	internal sealed class ImplicitInputBrush : Brush
	{
		// Token: 0x060047BC RID: 18364 RVA: 0x00118D64 File Offset: 0x00118164
		public new ImplicitInputBrush Clone()
		{
			return (ImplicitInputBrush)base.Clone();
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x00118D7C File Offset: 0x0011817C
		public new ImplicitInputBrush CloneCurrentValue()
		{
			return (ImplicitInputBrush)base.CloneCurrentValue();
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x00118D94 File Offset: 0x00118194
		protected override Freezable CreateInstanceCore()
		{
			return new ImplicitInputBrush();
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x00118DA8 File Offset: 0x001181A8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hRelativeTransform;
				if (relativeTransform == null || relativeTransform == Transform.Identity)
				{
					hRelativeTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hRelativeTransform = ((DUCE.IResource)relativeTransform).GetHandle(channel);
				}
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.MILCMD_IMPLICITINPUTBRUSH milcmd_IMPLICITINPUTBRUSH;
				milcmd_IMPLICITINPUTBRUSH.Type = MILCMD.MilCmdImplicitInputBrush;
				milcmd_IMPLICITINPUTBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_IMPLICITINPUTBRUSH.Opacity = base.Opacity;
				}
				milcmd_IMPLICITINPUTBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_IMPLICITINPUTBRUSH.hTransform = hTransform;
				milcmd_IMPLICITINPUTBRUSH.hRelativeTransform = hRelativeTransform;
				channel.SendCommand((byte*)(&milcmd_IMPLICITINPUTBRUSH), sizeof(DUCE.MILCMD_IMPLICITINPUTBRUSH));
			}
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00118E80 File Offset: 0x00118280
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_IMPLICITINPUTBRUSH))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00118EE0 File Offset: 0x001182E0
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00118F24 File Offset: 0x00118324
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00118F40 File Offset: 0x00118340
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00118F58 File Offset: 0x00118358
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x04001A24 RID: 6692
		internal DUCE.MultiChannelResource _duceResource;
	}
}
