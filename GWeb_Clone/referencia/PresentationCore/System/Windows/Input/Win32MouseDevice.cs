using System;
using System.Security;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002A4 RID: 676
	internal sealed class Win32MouseDevice : MouseDevice
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x00049FAC File Offset: 0x000493AC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal Win32MouseDevice(InputManager inputManager) : base(inputManager)
		{
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00049FC0 File Offset: 0x000493C0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override MouseButtonState GetButtonStateFromSystem(MouseButton mouseButton)
		{
			MouseButtonState result = MouseButtonState.Released;
			if (base.IsActive)
			{
				int keyCode = 0;
				switch (mouseButton)
				{
				case MouseButton.Left:
					keyCode = 1;
					break;
				case MouseButton.Middle:
					keyCode = 4;
					break;
				case MouseButton.Right:
					keyCode = 2;
					break;
				case MouseButton.XButton1:
					keyCode = 5;
					break;
				case MouseButton.XButton2:
					keyCode = 6;
					break;
				}
				result = ((((int)UnsafeNativeMethods.GetKeyState(keyCode) & 32768) != 0) ? MouseButtonState.Pressed : MouseButtonState.Released);
			}
			return result;
		}
	}
}
