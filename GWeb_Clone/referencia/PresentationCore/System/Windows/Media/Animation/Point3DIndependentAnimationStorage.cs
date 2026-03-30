using System;
using System.Security;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000529 RID: 1321
	internal class Point3DIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000EB780 File Offset: 0x000EAB80
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_POINT3DRESOURCE;
			}
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000EB790 File Offset: 0x000EAB90
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Point3D p = (Point3D)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_POINT3DRESOURCE milcmd_POINT3DRESOURCE;
			milcmd_POINT3DRESOURCE.Type = MILCMD.MilCmdPoint3DResource;
			milcmd_POINT3DRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_POINT3DRESOURCE.Value = CompositionResourceManager.Point3DToMilPoint3F(p);
			channel.SendCommand((byte*)(&milcmd_POINT3DRESOURCE), sizeof(DUCE.MILCMD_POINT3DRESOURCE));
		}
	}
}
