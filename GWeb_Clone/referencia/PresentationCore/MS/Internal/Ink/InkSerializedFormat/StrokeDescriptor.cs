using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D4 RID: 2004
	internal class StrokeDescriptor
	{
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0015ADF4 File Offset: 0x0015A1F4
		// (set) Token: 0x06005470 RID: 21616 RVA: 0x0015AE08 File Offset: 0x0015A208
		public uint Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x0015AE1C File Offset: 0x0015A21C
		public List<KnownTagCache.KnownTagIndex> Template
		{
			get
			{
				return this._strokeDescriptor;
			}
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0015AE50 File Offset: 0x0015A250
		public bool IsEqual(StrokeDescriptor strd)
		{
			if (this._strokeDescriptor.Count != strd.Template.Count)
			{
				return false;
			}
			for (int i = 0; i < this._strokeDescriptor.Count; i++)
			{
				if (this._strokeDescriptor[i] != strd.Template[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002612 RID: 9746
		private List<KnownTagCache.KnownTagIndex> _strokeDescriptor = new List<KnownTagCache.KnownTagIndex>();

		// Token: 0x04002613 RID: 9747
		private uint _Size;
	}
}
