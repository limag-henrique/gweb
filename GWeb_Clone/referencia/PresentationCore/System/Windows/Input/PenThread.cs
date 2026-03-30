using System;
using System.Security;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002D3 RID: 723
	internal sealed class PenThread
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x00050E70 File Offset: 0x00050270
		[SecurityCritical]
		internal PenThread()
		{
			this._penThreadWorker = new PenThreadWorker();
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00050E90 File Offset: 0x00050290
		internal void Dispose()
		{
			this.DisposeHelper();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00050EA4 File Offset: 0x000502A4
		~PenThread()
		{
			this.DisposeHelper();
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00050EDC File Offset: 0x000502DC
		[SecuritySafeCritical]
		private void DisposeHelper()
		{
			if (this._penThreadWorker != null)
			{
				this._penThreadWorker.Dispose();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00050F04 File Offset: 0x00050304
		[SecurityCritical]
		internal bool AddPenContext(PenContext penContext)
		{
			return this._penThreadWorker.WorkerAddPenContext(penContext);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00050F20 File Offset: 0x00050320
		[SecurityCritical]
		internal bool RemovePenContext(PenContext penContext)
		{
			return this._penThreadWorker.WorkerRemovePenContext(penContext);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00050F3C File Offset: 0x0005033C
		[SecurityCritical]
		internal TabletDeviceInfo[] WorkerGetTabletsInfo()
		{
			return this._penThreadWorker.WorkerGetTabletsInfo();
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00050F54 File Offset: 0x00050354
		[SecurityCritical]
		internal PenContextInfo WorkerCreateContext(IntPtr hwnd, IPimcTablet2 pimcTablet)
		{
			return this._penThreadWorker.WorkerCreateContext(hwnd, pimcTablet);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00050F70 File Offset: 0x00050370
		[SecurityCritical]
		internal bool WorkerAcquireTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
		{
			return this._penThreadWorker.WorkerAcquireTabletLocks(tablet, wispTabletKey);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00050F8C File Offset: 0x0005038C
		[SecurityCritical]
		internal bool WorkerReleaseTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
		{
			return this._penThreadWorker.WorkerReleaseTabletLocks(tablet, wispTabletKey);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00050FA8 File Offset: 0x000503A8
		[SecurityCritical]
		internal StylusDeviceInfo[] WorkerRefreshCursorInfo(IPimcTablet2 pimcTablet)
		{
			return this._penThreadWorker.WorkerRefreshCursorInfo(pimcTablet);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00050FC4 File Offset: 0x000503C4
		[SecurityCritical]
		internal TabletDeviceInfo WorkerGetTabletInfo(uint index)
		{
			return this._penThreadWorker.WorkerGetTabletInfo(index);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00050FE0 File Offset: 0x000503E0
		[SecurityCritical]
		internal TabletDeviceSizeInfo WorkerGetUpdatedSizes(IPimcTablet2 pimcTablet)
		{
			return this._penThreadWorker.WorkerGetUpdatedSizes(pimcTablet);
		}

		// Token: 0x04000BE0 RID: 3040
		private PenThreadWorker _penThreadWorker;
	}
}
