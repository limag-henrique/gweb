using System;
using System.Security;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200061C RID: 1564
	internal struct CompositionEngineLock : IDisposable
	{
		// Token: 0x060047F3 RID: 18419 RVA: 0x001199C0 File Offset: 0x00118DC0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static CompositionEngineLock Acquire()
		{
			UnsafeNativeMethods.MilCoreApi.EnterCompositionEngineLock();
			return default(CompositionEngineLock);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x001199DC File Offset: 0x00118DDC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			UnsafeNativeMethods.MilCoreApi.ExitCompositionEngineLock();
		}
	}
}
