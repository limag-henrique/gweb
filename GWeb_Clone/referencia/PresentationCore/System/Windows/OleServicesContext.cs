using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	// Token: 0x020001CE RID: 462
	internal class OleServicesContext
	{
		// Token: 0x06000C3B RID: 3131 RVA: 0x0002EE08 File Offset: 0x0002E208
		private OleServicesContext()
		{
			this.SetDispatcherThread();
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0002EE24 File Offset: 0x0002E224
		internal static OleServicesContext CurrentOleServicesContext
		{
			get
			{
				OleServicesContext oleServicesContext = (OleServicesContext)Thread.GetData(OleServicesContext._threadDataSlot);
				if (oleServicesContext == null)
				{
					oleServicesContext = new OleServicesContext();
					Thread.SetData(OleServicesContext._threadDataSlot, oleServicesContext);
				}
				return oleServicesContext;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002EE58 File Offset: 0x0002E258
		[SecurityCritical]
		internal int OleSetClipboard(IDataObject dataObject)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.OleSetClipboard(dataObject);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002EE88 File Offset: 0x0002E288
		[SecurityCritical]
		internal int OleGetClipboard(ref IDataObject dataObject)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.OleGetClipboard(ref dataObject);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002EEB8 File Offset: 0x0002E2B8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal int OleFlushClipboard()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.OleFlushClipboard();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002EEE8 File Offset: 0x0002E2E8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal int OleIsCurrentClipboard(IDataObject dataObject)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.OleIsCurrentClipboard(dataObject);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002EF18 File Offset: 0x0002E318
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void OleDoDragDrop(IDataObject dataObject, UnsafeNativeMethods.IOleDropSource dropSource, int allowedEffects, int[] finalEffect)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			InputManager inputManager = (InputManager)Dispatcher.CurrentDispatcher.InputManager;
			if (inputManager != null)
			{
				inputManager.InDragDrop = true;
			}
			try
			{
				UnsafeNativeMethods.DoDragDrop(dataObject, dropSource, allowedEffects, finalEffect);
			}
			finally
			{
				if (inputManager != null)
				{
					inputManager.InDragDrop = false;
				}
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002EF90 File Offset: 0x0002E390
		[SecurityCritical]
		internal int OleRegisterDragDrop(HandleRef windowHandle, UnsafeNativeMethods.IOleDropTarget dropTarget)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.RegisterDragDrop(windowHandle, dropTarget);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002EFC0 File Offset: 0x0002E3C0
		[SecurityCritical]
		internal int OleRevokeDragDrop(HandleRef windowHandle)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			return UnsafeNativeMethods.RevokeDragDrop(windowHandle);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002EFF0 File Offset: 0x0002E3F0
		private void SetDispatcherThread()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			int num = this.OleInitialize();
			if (!NativeMethods.Succeeded(num))
			{
				throw new SystemException(SR.Get("OleServicesContext_oleInitializeFailure", new object[]
				{
					num
				}));
			}
			Dispatcher.CurrentDispatcher.ShutdownFinished += this.OnDispatcherShutdown;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002F060 File Offset: 0x0002E460
		private void OnDispatcherShutdown(object sender, EventArgs args)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.Get("OleServicesContext_ThreadMustBeSTA"));
			}
			this.OleUninitialize();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002F090 File Offset: 0x0002E490
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private int OleInitialize()
		{
			return UnsafeNativeMethods.OleInitialize();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002F0A4 File Offset: 0x0002E4A4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private int OleUninitialize()
		{
			return UnsafeNativeMethods.OleUninitialize();
		}

		// Token: 0x0400071F RID: 1823
		private static readonly LocalDataStoreSlot _threadDataSlot = Thread.AllocateDataSlot();
	}
}
