using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x0200027E RID: 638
	internal sealed class MouseButtonUtilities
	{
		// Token: 0x060012B0 RID: 4784 RVA: 0x00045670 File Offset: 0x00044A70
		private MouseButtonUtilities()
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00045684 File Offset: 0x00044A84
		[FriendAccessAllowed]
		internal static void Validate(MouseButton button)
		{
			if (button > MouseButton.XButton2)
			{
				throw new InvalidEnumArgumentException("button", (int)button, typeof(MouseButton));
			}
		}
	}
}
