using System;
using System.Collections;
using System.Windows;
using MS.Internal.PresentationCore;

namespace MS.Utility
{
	// Token: 0x02000644 RID: 1604
	[FriendAccessAllowed]
	internal class DTypeMap
	{
		// Token: 0x06004823 RID: 18467 RVA: 0x0011A854 File Offset: 0x00119C54
		public DTypeMap(int entryCount)
		{
			this._entryCount = entryCount;
			this._entries = new object[this._entryCount];
			this._activeDTypes = new ItemStructList<DependencyObjectType>(128);
		}

		// Token: 0x17000F0A RID: 3850
		public object this[DependencyObjectType dType]
		{
			get
			{
				if (dType.Id < this._entryCount)
				{
					return this._entries[dType.Id];
				}
				if (this._overFlow != null)
				{
					return this._overFlow[dType];
				}
				return null;
			}
			set
			{
				if (dType.Id < this._entryCount)
				{
					this._entries[dType.Id] = value;
				}
				else
				{
					if (this._overFlow == null)
					{
						this._overFlow = new Hashtable();
					}
					this._overFlow[dType] = value;
				}
				this._activeDTypes.Add(dType);
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x0011A928 File Offset: 0x00119D28
		public ItemStructList<DependencyObjectType> ActiveDTypes
		{
			get
			{
				return this._activeDTypes;
			}
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0011A93C File Offset: 0x00119D3C
		public void Clear()
		{
			for (int i = 0; i < this._entryCount; i++)
			{
				this._entries[i] = null;
			}
			for (int j = 0; j < this._activeDTypes.Count; j++)
			{
				this._activeDTypes.List[j] = null;
			}
			if (this._overFlow != null)
			{
				this._overFlow.Clear();
			}
		}

		// Token: 0x04001BC9 RID: 7113
		private int _entryCount;

		// Token: 0x04001BCA RID: 7114
		private object[] _entries;

		// Token: 0x04001BCB RID: 7115
		private Hashtable _overFlow;

		// Token: 0x04001BCC RID: 7116
		private ItemStructList<DependencyObjectType> _activeDTypes;
	}
}
