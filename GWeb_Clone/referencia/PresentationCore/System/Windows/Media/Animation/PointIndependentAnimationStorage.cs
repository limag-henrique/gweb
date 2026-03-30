using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200052E RID: 1326
	internal class PointIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000EC758 File Offset: 0x000EBB58
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_POINTRESOURCE;
			}
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000EC768 File Offset: 0x000EBB68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Point value = (Point)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_POINTRESOURCE milcmd_POINTRESOURCE;
			milcmd_POINTRESOURCE.Type = MILCMD.MilCmdPointResource;
			milcmd_POINTRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_POINTRESOURCE.Value = value;
			channel.SendCommand((byte*)(&milcmd_POINTRESOURCE), sizeof(DUCE.MILCMD_POINTRESOURCE));
		}
	}
}
