using System;
using System.Security;

namespace System.Windows.Input
{
	// Token: 0x02000232 RID: 562
	internal sealed class DeadCharTextComposition : TextComposition
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x0003BA98 File Offset: 0x0003AE98
		[SecurityCritical]
		internal DeadCharTextComposition(InputManager inputManager, IInputElement source, string text, TextCompositionAutoComplete autoComplete, InputDevice inputDevice) : base(inputManager, source, text, autoComplete, inputDevice)
		{
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0003BAB4 File Offset: 0x0003AEB4
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x0003BAC8 File Offset: 0x0003AEC8
		internal bool Composed
		{
			get
			{
				return this._composed;
			}
			set
			{
				this._composed = value;
			}
		}

		// Token: 0x04000892 RID: 2194
		private bool _composed;
	}
}
