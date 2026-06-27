using System;

namespace System.Windows
{
	// Token: 0x020001DE RID: 478
	internal struct SourceItem
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00030640 File Offset: 0x0002FA40
		internal SourceItem(int startIndex, object source)
		{
			this._startIndex = startIndex;
			this._source = source;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0003065C File Offset: 0x0002FA5C
		internal int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00030670 File Offset: 0x0002FA70
		internal object Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00030684 File Offset: 0x0002FA84
		public override bool Equals(object o)
		{
			return this.Equals((SourceItem)o);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000306A0 File Offset: 0x0002FAA0
		public bool Equals(SourceItem sourceItem)
		{
			return sourceItem._startIndex == this._startIndex && sourceItem._source == this._source;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000306CC File Offset: 0x0002FACC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000306EC File Offset: 0x0002FAEC
		public static bool operator ==(SourceItem sourceItem1, SourceItem sourceItem2)
		{
			return sourceItem1.Equals(sourceItem2);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00030704 File Offset: 0x0002FB04
		public static bool operator !=(SourceItem sourceItem1, SourceItem sourceItem2)
		{
			return !sourceItem1.Equals(sourceItem2);
		}

		// Token: 0x04000757 RID: 1879
		private int _startIndex;

		// Token: 0x04000758 RID: 1880
		private object _source;
	}
}
