using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x02000259 RID: 601
	internal class InputProcessorProfiles
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x000400D8 File Offset: 0x0003F4D8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal InputProcessorProfiles()
		{
			this._ipp.Value = null;
			this._cookie = -1;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00040100 File Offset: 0x0003F500
		[SecurityCritical]
		internal bool Initialize(object o)
		{
			this._ipp.Value = InputProcessorProfilesLoader.Load();
			if (this._ipp.Value == null)
			{
				return false;
			}
			this.AdviseNotifySink(o);
			return true;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00040134 File Offset: 0x0003F534
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Uninitialize()
		{
			this.UnadviseNotifySink();
			Marshal.ReleaseComObject(this._ipp.Value);
			this._ipp.Value = null;
		}

		// Token: 0x170002A9 RID: 681
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00040164 File Offset: 0x0003F564
		internal short CurrentInputLanguage
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				if (this._ipp.Value != null && this._ipp.Value.ChangeCurrentLanguage(value) != 0)
				{
					int keyboardLayoutList = SafeNativeMethods.GetKeyboardLayoutList(0, null);
					if (keyboardLayoutList > 1)
					{
						IntPtr[] array = new IntPtr[keyboardLayoutList];
						keyboardLayoutList = SafeNativeMethods.GetKeyboardLayoutList(keyboardLayoutList, array);
						int num = 0;
						while (num < array.Length && num < keyboardLayoutList)
						{
							if (value == (short)((int)array[num]))
							{
								SafeNativeMethods.ActivateKeyboardLayout(new HandleRef(this, array[num]), 0);
								return;
							}
							num++;
						}
					}
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000401E0 File Offset: 0x0003F5E0
		internal ArrayList InputLanguageList
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				IntPtr intPtr;
				int num;
				this._ipp.Value.GetLanguageList(out intPtr, out num);
				ArrayList arrayList = new ArrayList();
				int num2 = Marshal.SizeOf(typeof(short));
				for (int i = 0; i < num; i++)
				{
					short culture = (short)Marshal.PtrToStructure((IntPtr)((long)intPtr + (long)(num2 * i)), typeof(short));
					arrayList.Add(new CultureInfo((int)culture));
				}
				Marshal.FreeCoTaskMem(intPtr);
				return arrayList;
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00040260 File Offset: 0x0003F660
		[SecurityCritical]
		private void AdviseNotifySink(object o)
		{
			UnsafeNativeMethods.ITfSource tfSource = this._ipp.Value as UnsafeNativeMethods.ITfSource;
			Guid iid_ITfLanguageProfileNotifySink = UnsafeNativeMethods.IID_ITfLanguageProfileNotifySink;
			tfSource.AdviseSink(ref iid_ITfLanguageProfileNotifySink, o, out this._cookie);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00040294 File Offset: 0x0003F694
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void UnadviseNotifySink()
		{
			UnsafeNativeMethods.ITfSource tfSource = this._ipp.Value as UnsafeNativeMethods.ITfSource;
			tfSource.UnadviseSink(this._cookie);
			this._cookie = -1;
		}

		// Token: 0x04000930 RID: 2352
		[SecurityCritical]
		private SecurityCriticalDataForSet<UnsafeNativeMethods.ITfInputProcessorProfiles> _ipp;

		// Token: 0x04000931 RID: 2353
		private int _cookie;
	}
}
