using System;
using System.Security;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002A5 RID: 677
	internal sealed class Win32KeyboardDevice : KeyboardDevice
	{
		// Token: 0x060013CF RID: 5071 RVA: 0x0004A01C File Offset: 0x0004941C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal Win32KeyboardDevice(InputManager inputManager) : base(inputManager)
		{
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004A030 File Offset: 0x00049430
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override KeyStates GetKeyStatesFromSystem(Key key)
		{
			KeyStates keyStates = KeyStates.None;
			bool flag = false;
			if (base.IsActive)
			{
				flag = true;
			}
			else if (SecurityHelper.AppDomainGrantedUnrestrictedUIPermission)
			{
				flag = true;
			}
			else if (key - Key.LeftShift <= 5)
			{
				flag = true;
			}
			if (flag)
			{
				int keyCode = KeyInterop.VirtualKeyFromKey(key);
				int keyState = (int)UnsafeNativeMethods.GetKeyState(keyCode);
				if ((keyState & 32768) == 32768)
				{
					keyStates |= KeyStates.Down;
				}
				if ((keyState & 1) == 1)
				{
					keyStates |= KeyStates.Toggled;
				}
			}
			return keyStates;
		}
	}
}
