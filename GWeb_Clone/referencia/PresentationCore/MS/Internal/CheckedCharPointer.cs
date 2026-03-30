using System;
using System.Security;
using MS.Internal.FontCache;

namespace MS.Internal
{
	// Token: 0x02000684 RID: 1668
	internal struct CheckedCharPointer
	{
		// Token: 0x0600497F RID: 18815 RVA: 0x0011EB9C File Offset: 0x0011DF9C
		[SecurityCritical]
		internal unsafe CheckedCharPointer(char* pointer, int length)
		{
			this._checkedPointer = new CheckedPointer((void*)pointer, length * 2);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0011EBB8 File Offset: 0x0011DFB8
		[SecurityCritical]
		internal unsafe char* Probe(int offset, int length)
		{
			return (char*)this._checkedPointer.Probe(offset * 2, length * 2);
		}

		// Token: 0x04001CE3 RID: 7395
		private CheckedPointer _checkedPointer;
	}
}
