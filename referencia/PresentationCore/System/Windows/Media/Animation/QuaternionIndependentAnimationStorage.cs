using System;
using System.Security;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000534 RID: 1332
	internal class QuaternionIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x000EEC9C File Offset: 0x000EE09C
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_QUATERNIONRESOURCE;
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000EECAC File Offset: 0x000EE0AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Quaternion q = (Quaternion)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_QUATERNIONRESOURCE milcmd_QUATERNIONRESOURCE;
			milcmd_QUATERNIONRESOURCE.Type = MILCMD.MilCmdQuaternionResource;
			milcmd_QUATERNIONRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_QUATERNIONRESOURCE.Value = CompositionResourceManager.QuaternionToMilQuaternionF(q);
			channel.SendCommand((byte*)(&milcmd_QUATERNIONRESOURCE), sizeof(DUCE.MILCMD_QUATERNIONRESOURCE));
		}
	}
}
