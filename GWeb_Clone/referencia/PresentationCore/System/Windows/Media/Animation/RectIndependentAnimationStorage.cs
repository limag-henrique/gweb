using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000539 RID: 1337
	internal class RectIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x000EFC74 File Offset: 0x000EF074
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_RECTRESOURCE;
			}
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000EFC84 File Offset: 0x000EF084
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Rect value = (Rect)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_RECTRESOURCE milcmd_RECTRESOURCE;
			milcmd_RECTRESOURCE.Type = MILCMD.MilCmdRectResource;
			milcmd_RECTRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_RECTRESOURCE.Value = value;
			channel.SendCommand((byte*)(&milcmd_RECTRESOURCE), sizeof(DUCE.MILCMD_RECTRESOURCE));
		}
	}
}
