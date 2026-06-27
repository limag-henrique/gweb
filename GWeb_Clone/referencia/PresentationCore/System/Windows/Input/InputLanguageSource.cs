using System;
using System.Collections;
using System.Globalization;
using System.Security;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x0200024C RID: 588
	internal sealed class InputLanguageSource : IInputLanguageSource, IDisposable
	{
		// Token: 0x06001052 RID: 4178 RVA: 0x0003D71C File Offset: 0x0003CB1C
		internal InputLanguageSource(InputLanguageManager inputlanguagemanager)
		{
			this._inputlanguagemanager = inputlanguagemanager;
			this._langid = (short)NativeMethods.IntPtrToInt32(SafeNativeMethods.GetKeyboardLayout(0));
			this._dispatcherThreadId = SafeNativeMethods.GetCurrentThreadId();
			this._inputlanguagemanager.RegisterInputLanguageSource(this);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003D760 File Offset: 0x0003CB60
		public void Dispose()
		{
			if (this._ipp != null)
			{
				this.Uninitialize();
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003D77C File Offset: 0x0003CB7C
		public void Initialize()
		{
			this.EnsureInputProcessorProfile();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003D790 File Offset: 0x0003CB90
		public void Uninitialize()
		{
			if (this._ipp != null)
			{
				this._ipp.Uninitialize();
				this._ipp = null;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003D7B8 File Offset: 0x0003CBB8
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0003D7D0 File Offset: 0x0003CBD0
		public CultureInfo CurrentInputLanguage
		{
			get
			{
				return new CultureInfo((int)this._CurrentInputLanguage);
			}
			set
			{
				this._CurrentInputLanguage = (short)value.LCID;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0003D7EC File Offset: 0x0003CBEC
		public IEnumerable InputLanguageList
		{
			get
			{
				this.EnsureInputProcessorProfile();
				if (this._ipp == null)
				{
					return new ArrayList
					{
						this.CurrentInputLanguage
					};
				}
				return this._ipp.InputLanguageList;
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003D828 File Offset: 0x0003CC28
		internal bool OnLanguageChange(short langid)
		{
			return this._langid == langid || InputLanguageManager.Current.Source != this || InputLanguageManager.Current.ReportInputLanguageChanging(new CultureInfo((int)langid), new CultureInfo((int)this._langid));
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003D868 File Offset: 0x0003CC68
		internal void OnLanguageChanged()
		{
			short currentInputLanguage = this._CurrentInputLanguage;
			if (this._langid != currentInputLanguage)
			{
				short langid = this._langid;
				this._langid = currentInputLanguage;
				if (InputLanguageManager.Current.Source == this)
				{
					InputLanguageManager.Current.ReportInputLanguageChanged(new CultureInfo((int)currentInputLanguage), new CultureInfo((int)langid));
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003D8B8 File Offset: 0x0003CCB8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void EnsureInputProcessorProfile()
		{
			if (this._ipp != null)
			{
				return;
			}
			if (SafeNativeMethods.GetKeyboardLayoutList(0, null) <= 1)
			{
				return;
			}
			InputLanguageProfileNotifySink o = new InputLanguageProfileNotifySink(this);
			this._ipp = new InputProcessorProfiles();
			if (!this._ipp.Initialize(o))
			{
				this._ipp = null;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0003D900 File Offset: 0x0003CD00
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x0003D920 File Offset: 0x0003CD20
		private short _CurrentInputLanguage
		{
			get
			{
				return (short)NativeMethods.IntPtrToInt32(SafeNativeMethods.GetKeyboardLayout(this._dispatcherThreadId));
			}
			set
			{
				this.EnsureInputProcessorProfile();
				if (this._ipp != null)
				{
					this._ipp.CurrentInputLanguage = value;
				}
			}
		}

		// Token: 0x040008C5 RID: 2245
		private short _langid;

		// Token: 0x040008C6 RID: 2246
		private int _dispatcherThreadId;

		// Token: 0x040008C7 RID: 2247
		private InputLanguageManager _inputlanguagemanager;

		// Token: 0x040008C8 RID: 2248
		private InputProcessorProfiles _ipp;
	}
}
