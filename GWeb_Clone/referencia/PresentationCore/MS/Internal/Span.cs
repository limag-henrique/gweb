using System;

namespace MS.Internal
{
	// Token: 0x02000021 RID: 33
	internal sealed class Span
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000C214 File Offset: 0x0000B614
		public Span(object element, int length)
		{
			this.element = element;
			this.length = length;
		}

		// Token: 0x04000332 RID: 818
		public object element;

		// Token: 0x04000333 RID: 819
		public int length;
	}
}
