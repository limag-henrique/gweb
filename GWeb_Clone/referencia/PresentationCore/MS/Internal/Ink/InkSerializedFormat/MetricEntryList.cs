using System;
using System.Windows.Input;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D0 RID: 2000
	internal struct MetricEntryList
	{
		// Token: 0x0600544F RID: 21583 RVA: 0x00159FEC File Offset: 0x001593EC
		public MetricEntryList(KnownTagCache.KnownTagIndex tag, StylusPointPropertyInfo prop)
		{
			this.Tag = tag;
			this.PropertyMetrics = prop;
		}

		// Token: 0x04002602 RID: 9730
		public KnownTagCache.KnownTagIndex Tag;

		// Token: 0x04002603 RID: 9731
		public StylusPointPropertyInfo PropertyMetrics;
	}
}
