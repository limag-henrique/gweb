using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005D3 RID: 1491
	internal class QueueEntry
	{
		// Token: 0x0400188F RID: 6287
		internal List<WeakReference> decoders;

		// Token: 0x04001890 RID: 6288
		internal Uri inputUri;

		// Token: 0x04001891 RID: 6289
		internal Stream inputStream;

		// Token: 0x04001892 RID: 6290
		internal Stream outputStream;

		// Token: 0x04001893 RID: 6291
		internal string streamPath;

		// Token: 0x04001894 RID: 6292
		internal byte[] readBuffer;

		// Token: 0x04001895 RID: 6293
		internal long contentLength;

		// Token: 0x04001896 RID: 6294
		internal string contentType;

		// Token: 0x04001897 RID: 6295
		internal int lastPercent;

		// Token: 0x04001898 RID: 6296
		internal WebRequest webRequest;
	}
}
