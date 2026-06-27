using System;
using System.Security;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000562 RID: 1378
	internal class Vector3DIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x000FA14C File Offset: 0x000F954C
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_VECTOR3DRESOURCE;
			}
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000FA15C File Offset: 0x000F955C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Vector3D v = (Vector3D)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_VECTOR3DRESOURCE milcmd_VECTOR3DRESOURCE;
			milcmd_VECTOR3DRESOURCE.Type = MILCMD.MilCmdVector3DResource;
			milcmd_VECTOR3DRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_VECTOR3DRESOURCE.Value = CompositionResourceManager.Vector3DToMilPoint3F(v);
			channel.SendCommand((byte*)(&milcmd_VECTOR3DRESOURCE), sizeof(DUCE.MILCMD_VECTOR3DRESOURCE));
		}
	}
}
