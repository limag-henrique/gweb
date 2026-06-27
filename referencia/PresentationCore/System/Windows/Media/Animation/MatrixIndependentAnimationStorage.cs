using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200051F RID: 1311
	internal class MatrixIndependentAnimationStorage : IndependentAnimationStorage
	{
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000E80E4 File Offset: 0x000E74E4
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_MATRIXRESOURCE;
			}
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000E80F4 File Offset: 0x000E74F4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected unsafe override void UpdateResourceCore(DUCE.Channel channel)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			Matrix m = (Matrix)dependencyObject.GetValue(this._dependencyProperty);
			DUCE.MILCMD_MATRIXRESOURCE milcmd_MATRIXRESOURCE;
			milcmd_MATRIXRESOURCE.Type = MILCMD.MilCmdMatrixResource;
			milcmd_MATRIXRESOURCE.Handle = this._duceResource.GetHandle(channel);
			milcmd_MATRIXRESOURCE.Value = CompositionResourceManager.MatrixToMilMatrix3x2D(m);
			channel.SendCommand((byte*)(&milcmd_MATRIXRESOURCE), sizeof(DUCE.MILCMD_MATRIXRESOURCE));
		}
	}
}
