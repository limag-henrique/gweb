using System;

namespace System.Windows.Input
{
	// Token: 0x0200023C RID: 572
	internal interface IKeyboardInputProvider : IInputProvider
	{
		// Token: 0x06000FE6 RID: 4070
		bool AcquireFocus(bool checkOnly);
	}
}
