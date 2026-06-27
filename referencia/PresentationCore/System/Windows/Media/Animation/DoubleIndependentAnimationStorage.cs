using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004DA RID: 1242
	internal class DoubleIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000DE1C0 File Offset: 0x000DD5C0
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_DOUBLERESOURCE;
			}
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000DE1D0 File Offset: 0x000DD5D0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			double value = (double)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_DOUBLERESOURCE milcmd_DOUBLERESOURCE;
			milcmd_DOUBLERESOURCE.Type = MILCMD.MilCmdDoubleResource;
			milcmd_DOUBLERESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_DOUBLERESOURCE.Value = value;
			channel.SendCommand((byte*)(&milcmd_DOUBLERESOURCE), sizeof(DUCE.MILCMD_DOUBLERESOURCE));
		}
	}
}
