using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000547 RID: 1351
	internal class SizeIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000F48AC File Offset: 0x000F3CAC
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_SIZERESOURCE;
			}
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000F48BC File Offset: 0x000F3CBC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Size value = (Size)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_SIZERESOURCE milcmd_SIZERESOURCE;
			milcmd_SIZERESOURCE.Type = MILCMD.MilCmdSizeResource;
			milcmd_SIZERESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_SIZERESOURCE.Value = value;
			channel.SendCommand((byte*)(&milcmd_SIZERESOURCE), sizeof(DUCE.MILCMD_SIZERESOURCE));
		}
	}
}
