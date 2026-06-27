using System;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000005 RID: 5
	internal sealed class LocalizedErrorMsgs
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000ADCC File Offset: 0x0000A1CC
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000AE18 File Offset: 0x0000A218
		internal static string EnumeratorNotStarted
		{
			get
			{
				string localizedExceptionMsgEnumeratorNotStarted;
				lock (LocalizedErrorMsgs._staticLockForLocalizedExceptionMsgs)
				{
					localizedExceptionMsgEnumeratorNotStarted = LocalizedErrorMsgs._localizedExceptionMsgEnumeratorNotStarted;
				}
				return localizedExceptionMsgEnumeratorNotStarted;
			}
			set
			{
				lock (LocalizedErrorMsgs._staticLockForLocalizedExceptionMsgs)
				{
					LocalizedErrorMsgs._localizedExceptionMsgEnumeratorNotStarted = value;
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000AE60 File Offset: 0x0000A260
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000AEAC File Offset: 0x0000A2AC
		internal static string EnumeratorReachedEnd
		{
			get
			{
				string localizedExceptionMsgEnumeratorReachedEnd;
				lock (LocalizedErrorMsgs._staticLockForLocalizedExceptionMsgs)
				{
					localizedExceptionMsgEnumeratorReachedEnd = LocalizedErrorMsgs._localizedExceptionMsgEnumeratorReachedEnd;
				}
				return localizedExceptionMsgEnumeratorReachedEnd;
			}
			set
			{
				lock (LocalizedErrorMsgs._staticLockForLocalizedExceptionMsgs)
				{
					LocalizedErrorMsgs._localizedExceptionMsgEnumeratorReachedEnd = value;
				}
			}
		}

		// Token: 0x040002F5 RID: 757
		private static string _localizedExceptionMsgEnumeratorNotStarted;

		// Token: 0x040002F6 RID: 758
		private static string _localizedExceptionMsgEnumeratorReachedEnd;

		// Token: 0x040002F7 RID: 759
		private static object _staticLockForLocalizedExceptionMsgs = new object();
	}
}
