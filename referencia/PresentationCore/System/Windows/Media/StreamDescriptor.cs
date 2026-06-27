using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Windows.Media
{
	// Token: 0x0200043D RID: 1085
	internal struct StreamDescriptor
	{
		// Token: 0x06002C45 RID: 11333 RVA: 0x000B0CE8 File Offset: 0x000B00E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void StaticDispose(ref StreamDescriptor pSD)
		{
			StreamAsIStream streamAsIStream = (StreamAsIStream)pSD.m_handle.Target;
			GCHandle handle = pSD.m_handle;
			handle.Free();
		}

		// Token: 0x04001429 RID: 5161
		internal StreamDescriptor.Dispose pfnDispose;

		// Token: 0x0400142A RID: 5162
		internal StreamDescriptor.Read pfnRead;

		// Token: 0x0400142B RID: 5163
		[SecurityCritical]
		internal StreamDescriptor.Seek pfnSeek;

		// Token: 0x0400142C RID: 5164
		internal StreamDescriptor.Stat pfnStat;

		// Token: 0x0400142D RID: 5165
		internal StreamDescriptor.Write pfnWrite;

		// Token: 0x0400142E RID: 5166
		[SecurityCritical]
		internal StreamDescriptor.CopyTo pfnCopyTo;

		// Token: 0x0400142F RID: 5167
		internal StreamDescriptor.SetSize pfnSetSize;

		// Token: 0x04001430 RID: 5168
		internal StreamDescriptor.Commit pfnCommit;

		// Token: 0x04001431 RID: 5169
		internal StreamDescriptor.Revert pfnRevert;

		// Token: 0x04001432 RID: 5170
		internal StreamDescriptor.LockRegion pfnLockRegion;

		// Token: 0x04001433 RID: 5171
		internal StreamDescriptor.UnlockRegion pfnUnlockRegion;

		// Token: 0x04001434 RID: 5172
		internal StreamDescriptor.Clone pfnClone;

		// Token: 0x04001435 RID: 5173
		internal StreamDescriptor.CanWrite pfnCanWrite;

		// Token: 0x04001436 RID: 5174
		internal StreamDescriptor.CanSeek pfnCanSeek;

		// Token: 0x04001437 RID: 5175
		internal GCHandle m_handle;

		// Token: 0x02000895 RID: 2197
		// (Invoke) Token: 0x06005823 RID: 22563
		internal delegate void Dispose(ref StreamDescriptor pSD);

		// Token: 0x02000896 RID: 2198
		// (Invoke) Token: 0x06005827 RID: 22567
		internal delegate int Read(ref StreamDescriptor pSD, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] buffer, uint cb, out uint cbRead);

		// Token: 0x02000897 RID: 2199
		// (Invoke) Token: 0x0600582B RID: 22571
		[SecurityCritical]
		internal unsafe delegate int Seek(ref StreamDescriptor pSD, long offset, uint origin, long* plibNewPostion);

		// Token: 0x02000898 RID: 2200
		// (Invoke) Token: 0x0600582F RID: 22575
		internal delegate int Stat(ref StreamDescriptor pSD, out System.Runtime.InteropServices.ComTypes.STATSTG statstg, uint grfStatFlag);

		// Token: 0x02000899 RID: 2201
		// (Invoke) Token: 0x06005833 RID: 22579
		internal delegate int Write(ref StreamDescriptor pSD, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, uint cb, out uint cbWritten);

		// Token: 0x0200089A RID: 2202
		// (Invoke) Token: 0x06005837 RID: 22583
		[SecurityCritical]
		internal delegate int CopyTo(ref StreamDescriptor pSD, IntPtr pstm, long cb, out long cbRead, out long cbWritten);

		// Token: 0x0200089B RID: 2203
		// (Invoke) Token: 0x0600583B RID: 22587
		internal delegate int SetSize(ref StreamDescriptor pSD, long value);

		// Token: 0x0200089C RID: 2204
		// (Invoke) Token: 0x0600583F RID: 22591
		internal delegate int Revert(ref StreamDescriptor pSD);

		// Token: 0x0200089D RID: 2205
		// (Invoke) Token: 0x06005843 RID: 22595
		internal delegate int Commit(ref StreamDescriptor pSD, uint grfCommitFlags);

		// Token: 0x0200089E RID: 2206
		// (Invoke) Token: 0x06005847 RID: 22599
		internal delegate int LockRegion(ref StreamDescriptor pSD, long libOffset, long cb, uint dwLockType);

		// Token: 0x0200089F RID: 2207
		// (Invoke) Token: 0x0600584B RID: 22603
		internal delegate int UnlockRegion(ref StreamDescriptor pSD, long libOffset, long cb, uint dwLockType);

		// Token: 0x020008A0 RID: 2208
		// (Invoke) Token: 0x0600584F RID: 22607
		internal delegate int Clone(ref StreamDescriptor pSD, out IntPtr stream);

		// Token: 0x020008A1 RID: 2209
		// (Invoke) Token: 0x06005853 RID: 22611
		internal delegate int CanWrite(ref StreamDescriptor pSD, out bool canWrite);

		// Token: 0x020008A2 RID: 2210
		// (Invoke) Token: 0x06005857 RID: 22615
		internal delegate int CanSeek(ref StreamDescriptor pSD, out bool canSeek);
	}
}
