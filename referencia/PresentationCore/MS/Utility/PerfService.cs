using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace MS.Utility
{
	// Token: 0x02000643 RID: 1603
	[FriendAccessAllowed]
	internal static class PerfService
	{
		// Token: 0x06004820 RID: 18464 RVA: 0x0011A7EC File Offset: 0x00119BEC
		internal static long GetPerfElementID2(object element, string extraData)
		{
			return (long)PerfService.perfElementIds.GetValue(element, delegate(object key)
			{
				long nextPerfElementId = SafeNativeMethods.GetNextPerfElementId();
				if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Verbose))
				{
					Type type = key.GetType();
					Assembly assembly = type.Assembly;
					if (key != assembly)
					{
						EventTrace.EventProvider.TraceEvent(EventTrace.Event.PerfElementIDAssignment, EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Verbose, new object[]
						{
							nextPerfElementId,
							type.FullName,
							extraData,
							PerfService.GetPerfElementID2(assembly, assembly.FullName)
						});
					}
				}
				return nextPerfElementId;
			});
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0011A824 File Offset: 0x00119C24
		internal static long GetPerfElementID(object element)
		{
			return PerfService.GetPerfElementID2(element, string.Empty);
		}

		// Token: 0x04001BC8 RID: 7112
		private static ConditionalWeakTable<object, object> perfElementIds = new ConditionalWeakTable<object, object>();
	}
}
