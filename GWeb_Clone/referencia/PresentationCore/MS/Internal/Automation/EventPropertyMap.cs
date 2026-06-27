using System;
using System.Collections;
using System.Windows;

namespace MS.Internal.Automation
{
	// Token: 0x0200078D RID: 1933
	internal class EventPropertyMap
	{
		// Token: 0x06005138 RID: 20792 RVA: 0x00145590 File Offset: 0x00144990
		private EventPropertyMap()
		{
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x001455A4 File Offset: 0x001449A4
		internal static bool IsInterestingDP(DependencyProperty dp)
		{
			using (EventPropertyMap._propertyLock.ReadLock)
			{
				if (EventPropertyMap._propertyTable != null && EventPropertyMap._propertyTable.ContainsKey(dp))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00145600 File Offset: 0x00144A00
		internal static bool AddPropertyNotify(DependencyProperty[] properties)
		{
			if (properties == null)
			{
				return false;
			}
			bool result = false;
			using (EventPropertyMap._propertyLock.WriteLock)
			{
				if (EventPropertyMap._propertyTable == null)
				{
					EventPropertyMap._propertyTable = new Hashtable(20, 0.1f);
					result = true;
				}
				int count = EventPropertyMap._propertyTable.Count;
				foreach (DependencyProperty dependencyProperty in properties)
				{
					if (dependencyProperty != null)
					{
						int num = 0;
						if (EventPropertyMap._propertyTable.ContainsKey(dependencyProperty))
						{
							num = (int)EventPropertyMap._propertyTable[dependencyProperty];
						}
						num++;
						EventPropertyMap._propertyTable[dependencyProperty] = num;
					}
				}
			}
			return result;
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x001456C0 File Offset: 0x00144AC0
		internal static bool RemovePropertyNotify(DependencyProperty[] properties)
		{
			bool result = false;
			using (EventPropertyMap._propertyLock.WriteLock)
			{
				if (EventPropertyMap._propertyTable != null)
				{
					int count = EventPropertyMap._propertyTable.Count;
					foreach (DependencyProperty key in properties)
					{
						if (EventPropertyMap._propertyTable.ContainsKey(key))
						{
							int num = (int)EventPropertyMap._propertyTable[key];
							num--;
							if (num > 0)
							{
								EventPropertyMap._propertyTable[key] = num;
							}
							else
							{
								EventPropertyMap._propertyTable.Remove(key);
							}
						}
					}
					if (EventPropertyMap._propertyTable.Count == 0)
					{
						EventPropertyMap._propertyTable = null;
					}
				}
				result = (EventPropertyMap._propertyTable == null);
			}
			return result;
		}

		// Token: 0x040024ED RID: 9453
		private static ReaderWriterLockWrapper _propertyLock = new ReaderWriterLockWrapper();

		// Token: 0x040024EE RID: 9454
		private static Hashtable _propertyTable;
	}
}
