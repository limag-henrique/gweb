using System;
using System.Windows.Media;

namespace System.Windows.Input
{
	// Token: 0x0200023B RID: 571
	internal interface IInputProvider
	{
		// Token: 0x06000FE4 RID: 4068
		bool ProvidesInputForRootVisual(Visual v);

		// Token: 0x06000FE5 RID: 4069
		void NotifyDeactivate();
	}
}
