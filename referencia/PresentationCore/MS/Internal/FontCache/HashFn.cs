using System;
using System.Security;
using MS.Internal.PresentationCore;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077A RID: 1914
	[FriendAccessAllowed]
	internal static class HashFn
	{
		// Token: 0x06005097 RID: 20631 RVA: 0x00142AB4 File Offset: 0x00141EB4
		internal static int HashMultiply(int hash)
		{
			return hash * 101;
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00142AC8 File Offset: 0x00141EC8
		internal static int HashScramble(int hash)
		{
			uint num = (uint)(314159269 * hash);
			return (int)(num % 1000000007U);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x00142AE8 File Offset: 0x00141EE8
		[SecurityCritical]
		internal unsafe static int HashMemory(void* pv, int numBytes, int hash)
		{
			byte* ptr = (byte*)pv;
			while (numBytes-- > 0)
			{
				hash = HashFn.HashMultiply(hash) + (int)(*ptr);
				ptr++;
			}
			return hash;
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x00142B14 File Offset: 0x00141F14
		internal static int HashString(string s, int hash)
		{
			foreach (char c in s)
			{
				hash = HashFn.HashMultiply(hash) + (int)c;
			}
			return hash;
		}

		// Token: 0x040024B9 RID: 9401
		private const int HASH_MULTIPLIER = 101;
	}
}
