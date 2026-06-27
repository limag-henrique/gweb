using System;
using System.Security;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000756 RID: 1878
	[FriendAccessAllowed]
	internal sealed class TextPenaltyModule : IDisposable
	{
		// Token: 0x06004EAF RID: 20143 RVA: 0x00138884 File Offset: 0x00137C84
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal TextPenaltyModule(SecurityCriticalDataForSet<IntPtr> ploc)
		{
			IntPtr value;
			LsErr lsErr = UnsafeNativeMethods.LoAcquirePenaltyModule(ploc.Value, out value);
			if (lsErr != LsErr.None)
			{
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("AcquirePenaltyModuleFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
			this._ploPenaltyModule.Value = value;
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x001388D4 File Offset: 0x00137CD4
		~TextPenaltyModule()
		{
			this.Dispose(false);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00138910 File Offset: 0x00137D10
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0013892C File Offset: 0x00137D2C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void Dispose(bool disposing)
		{
			if (this._ploPenaltyModule.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.LoDisposePenaltyModule(this._ploPenaltyModule.Value);
				this._ploPenaltyModule.Value = IntPtr.Zero;
				this._isDisposed = true;
				GC.KeepAlive(this);
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00138980 File Offset: 0x00137D80
		[SecurityCritical]
		internal IntPtr DangerousGetHandle()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(SR.Get("TextPenaltyModuleHasBeenDisposed"));
			}
			IntPtr result;
			LsErr lsErr = UnsafeNativeMethods.LoGetPenaltyModuleInternalHandle(this._ploPenaltyModule.Value, out result);
			if (lsErr != LsErr.None)
			{
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("GetPenaltyModuleHandleFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x040023C4 RID: 9156
		private SecurityCriticalDataForSet<IntPtr> _ploPenaltyModule;

		// Token: 0x040023C5 RID: 9157
		private bool _isDisposed;
	}
}
