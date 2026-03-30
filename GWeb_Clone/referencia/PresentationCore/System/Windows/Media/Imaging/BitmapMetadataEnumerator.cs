using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005DF RID: 1503
	internal struct BitmapMetadataEnumerator : IEnumerator<string>, IDisposable, IEnumerator
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x00109E68 File Offset: 0x00109268
		object IEnumerator.Current
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00109E7C File Offset: 0x0010927C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public bool MoveNext()
		{
			if (this._fStarted && this._current == null)
			{
				return false;
			}
			this._fStarted = true;
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			try
			{
				int hr = UnsafeNativeMethods.EnumString.Next(this._enumeratorHandle, 1, ref zero, ref num);
				if (HRESULT.IsWindowsCodecError(hr))
				{
					this._current = null;
					return false;
				}
				HRESULT.Check(hr);
				if (num == 0)
				{
					this._current = null;
					return false;
				}
				this._current = Marshal.PtrToStringUni(zero);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(zero);
					zero = IntPtr.Zero;
				}
			}
			return true;
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x00109F2C File Offset: 0x0010932C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void Reset()
		{
			HRESULT.Check(UnsafeNativeMethods.EnumString.Reset(this._enumeratorHandle));
			this._current = null;
			this._fStarted = false;
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x00109F58 File Offset: 0x00109358
		public string Current
		{
			get
			{
				if (this._current != null)
				{
					return this._current;
				}
				if (!this._fStarted)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_NotStarted"));
				}
				throw new InvalidOperationException(SR.Get("Enumerator_ReachedEnd"));
			}
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x00109F9C File Offset: 0x0010939C
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x00109FAC File Offset: 0x001093AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal BitmapMetadataEnumerator(SafeMILHandle metadataHandle)
		{
			HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryReader.GetEnumerator(metadataHandle, out this._enumeratorHandle));
			this._current = null;
			this._fStarted = false;
		}

		// Token: 0x040018DF RID: 6367
		[SecurityCritical]
		private SafeMILHandle _enumeratorHandle;

		// Token: 0x040018E0 RID: 6368
		private string _current;

		// Token: 0x040018E1 RID: 6369
		private bool _fStarted;
	}
}
