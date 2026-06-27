using System;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x0200024B RID: 587
	internal class InputLanguageProfileNotifySink : UnsafeNativeMethods.ITfLanguageProfileNotifySink
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x0003D6CC File Offset: 0x0003CACC
		internal InputLanguageProfileNotifySink(InputLanguageSource target)
		{
			this._target = target;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003D6E8 File Offset: 0x0003CAE8
		public void OnLanguageChange(short langid, out bool accept)
		{
			accept = this._target.OnLanguageChange(langid);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003D704 File Offset: 0x0003CB04
		public void OnLanguageChanged()
		{
			this._target.OnLanguageChanged();
		}

		// Token: 0x040008C4 RID: 2244
		private InputLanguageSource _target;
	}
}
