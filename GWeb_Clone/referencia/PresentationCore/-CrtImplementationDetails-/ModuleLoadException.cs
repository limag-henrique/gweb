using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00013254 File Offset: 0x00012654
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001323C File Offset: 0x0001263C
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00013228 File Offset: 0x00012628
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x0400048D RID: 1165
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
