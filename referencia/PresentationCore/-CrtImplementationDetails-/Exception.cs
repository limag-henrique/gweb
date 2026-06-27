using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00013210 File Offset: 0x00012610
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000131F8 File Offset: 0x000125F8
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000131E4 File Offset: 0x000125E4
		public Exception(string message) : base(message)
		{
		}
	}
}
