using System;
using System.Security;
using MS.Internal;
using MS.Internal.Composition;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200038F RID: 911
	internal class FactoryMaker : IDisposable
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x00088A98 File Offset: 0x00087E98
		[SecurityCritical]
		internal FactoryMaker()
		{
			object obj = FactoryMaker.s_factoryMakerLock;
			lock (obj)
			{
				if (FactoryMaker.s_pFactory == IntPtr.Zero)
				{
					HRESULT.Check(UnsafeNativeMethods.MILFactory2.CreateFactory(out FactoryMaker.s_pFactory, MS.Internal.Composition.Version.MilSdkVersion));
				}
				FactoryMaker.s_cInstance++;
				this._fValidObject = true;
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00088B1C File Offset: 0x00087F1C
		~FactoryMaker()
		{
			this.Dispose(false);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00088B58 File Offset: 0x00087F58
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00088B6C File Offset: 0x00087F6C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected virtual void Dispose(bool fDisposing)
		{
			if (!this._disposed)
			{
				if (this._fValidObject)
				{
					object obj = FactoryMaker.s_factoryMakerLock;
					lock (obj)
					{
						FactoryMaker.s_cInstance--;
						this._fValidObject = false;
						if (FactoryMaker.s_cInstance == 0)
						{
							UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref FactoryMaker.s_pFactory);
							if (FactoryMaker.s_pImagingFactory != IntPtr.Zero)
							{
								UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref FactoryMaker.s_pImagingFactory);
							}
							FactoryMaker.s_pFactory = IntPtr.Zero;
							FactoryMaker.s_pImagingFactory = IntPtr.Zero;
						}
					}
				}
				this._disposed = true;
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00088C28 File Offset: 0x00088028
		internal IntPtr FactoryPtr
		{
			[SecurityCritical]
			get
			{
				return FactoryMaker.s_pFactory;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x00088C3C File Offset: 0x0008803C
		internal IntPtr ImagingFactoryPtr
		{
			[SecurityCritical]
			get
			{
				if (FactoryMaker.s_pImagingFactory == IntPtr.Zero)
				{
					object obj = FactoryMaker.s_factoryMakerLock;
					lock (obj)
					{
						HRESULT.Check(UnsafeNativeMethods.WICCodec.CreateImagingFactory(566U, out FactoryMaker.s_pImagingFactory));
					}
				}
				return FactoryMaker.s_pImagingFactory;
			}
		}

		// Token: 0x040010D0 RID: 4304
		private bool _disposed;

		// Token: 0x040010D1 RID: 4305
		[SecurityCritical]
		private static IntPtr s_pFactory;

		// Token: 0x040010D2 RID: 4306
		[SecurityCritical]
		private static IntPtr s_pImagingFactory;

		// Token: 0x040010D3 RID: 4307
		private static int s_cInstance = 0;

		// Token: 0x040010D4 RID: 4308
		private static object s_factoryMakerLock = new object();

		// Token: 0x040010D5 RID: 4309
		private bool _fValidObject;
	}
}
