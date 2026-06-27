using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004BB RID: 1211
	internal class ColorIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000DA3F4 File Offset: 0x000D97F4
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_COLORRESOURCE;
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000DA404 File Offset: 0x000D9804
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Color c = (Color)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_COLORRESOURCE milcmd_COLORRESOURCE;
			milcmd_COLORRESOURCE.Type = MILCMD.MilCmdColorResource;
			milcmd_COLORRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_COLORRESOURCE.Value = CompositionResourceManager.ColorToMilColorF(c);
			channel.SendCommand((byte*)(&milcmd_COLORRESOURCE), sizeof(DUCE.MILCMD_COLORRESOURCE));
		}
	}
}
