using System;
using System.Security;

namespace System.Windows.Media
{
	// Token: 0x0200043E RID: 1086
	internal static class StaticPtrs
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000B0D14 File Offset: 0x000B0114
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static StaticPtrs()
		{
			StaticPtrs.pfnCommit = new StreamDescriptor.Commit(StreamAsIStream.Commit);
			StaticPtrs.pfnCopyTo = new StreamDescriptor.CopyTo(StreamAsIStream.CopyTo);
			StaticPtrs.pfnLockRegion = new StreamDescriptor.LockRegion(StreamAsIStream.LockRegion);
			StaticPtrs.pfnRead = new StreamDescriptor.Read(StreamAsIStream.Read);
			StaticPtrs.pfnRevert = new StreamDescriptor.Revert(StreamAsIStream.Revert);
			StaticPtrs.pfnSeek = new StreamDescriptor.Seek(StreamAsIStream.Seek);
			StaticPtrs.pfnSetSize = new StreamDescriptor.SetSize(StreamAsIStream.SetSize);
			StaticPtrs.pfnStat = new StreamDescriptor.Stat(StreamAsIStream.Stat);
			StaticPtrs.pfnUnlockRegion = new StreamDescriptor.UnlockRegion(StreamAsIStream.UnlockRegion);
			StaticPtrs.pfnWrite = new StreamDescriptor.Write(StreamAsIStream.Write);
			StaticPtrs.pfnCanWrite = new StreamDescriptor.CanWrite(StreamAsIStream.CanWrite);
			StaticPtrs.pfnCanSeek = new StreamDescriptor.CanSeek(StreamAsIStream.CanSeek);
		}

		// Token: 0x04001438 RID: 5176
		internal static StreamDescriptor.Dispose pfnDispose = new StreamDescriptor.Dispose(StreamDescriptor.StaticDispose);

		// Token: 0x04001439 RID: 5177
		internal static StreamDescriptor.Read pfnRead;

		// Token: 0x0400143A RID: 5178
		[SecurityCritical]
		internal static StreamDescriptor.Seek pfnSeek;

		// Token: 0x0400143B RID: 5179
		internal static StreamDescriptor.Stat pfnStat;

		// Token: 0x0400143C RID: 5180
		internal static StreamDescriptor.Write pfnWrite;

		// Token: 0x0400143D RID: 5181
		[SecurityCritical]
		internal static StreamDescriptor.CopyTo pfnCopyTo;

		// Token: 0x0400143E RID: 5182
		internal static StreamDescriptor.SetSize pfnSetSize;

		// Token: 0x0400143F RID: 5183
		internal static StreamDescriptor.Commit pfnCommit;

		// Token: 0x04001440 RID: 5184
		internal static StreamDescriptor.Revert pfnRevert;

		// Token: 0x04001441 RID: 5185
		internal static StreamDescriptor.LockRegion pfnLockRegion;

		// Token: 0x04001442 RID: 5186
		internal static StreamDescriptor.UnlockRegion pfnUnlockRegion;

		// Token: 0x04001443 RID: 5187
		internal static StreamDescriptor.Clone pfnClone = new StreamDescriptor.Clone(StreamAsIStream.Clone);

		// Token: 0x04001444 RID: 5188
		internal static StreamDescriptor.CanWrite pfnCanWrite;

		// Token: 0x04001445 RID: 5189
		internal static StreamDescriptor.CanSeek pfnCanSeek;
	}
}
