using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000174 RID: 372
	internal class ModuleUninitializer : Stack
	{
		// Token: 0x06000375 RID: 885 RVA: 0x000133B8 File Offset: 0x000127B8
		[SecuritySafeCritical]
		internal void AddHandler(EventHandler handler)
		{
			RuntimeHelpers.PrepareDelegate(handler);
			this.Push(handler);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013720 File Offset: 0x00012B20
		[SecuritySafeCritical]
		private ModuleUninitializer()
		{
			EventHandler value = new EventHandler(this.SingletonDomainUnload);
			AppDomain.CurrentDomain.DomainUnload += value;
			AppDomain.CurrentDomain.ProcessExit += value;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000133D4 File Offset: 0x000127D4
		[PrePrepareMethod]
		[SecurityCritical]
		private void SingletonDomainUnload(object source, EventArgs arguments)
		{
			using (IEnumerator enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					((EventHandler)enumerator.Current)(source, arguments);
				}
			}
		}

		// Token: 0x04000490 RID: 1168
		private static object @lock = new object();

		// Token: 0x04000491 RID: 1169
		internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();
	}
}
