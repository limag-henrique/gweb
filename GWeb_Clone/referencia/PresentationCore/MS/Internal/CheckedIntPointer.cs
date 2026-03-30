using System;
using System.Security;
using MS.Internal.FontCache;

namespace MS.Internal
{
	// Token: 0x02000685 RID: 1669
	internal struct CheckedIntPointer
	{
		// Token: 0x06004981 RID: 18817 RVA: 0x0011EBD8 File Offset: 0x0011DFD8
		[SecurityCritical]
		internal unsafe CheckedIntPointer(int* pointer, int length)
		{
			this._checkedPointer = new CheckedPointer((void*)pointer, length * 4);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x0011EBF4 File Offset: 0x0011DFF4
		[SecurityCritical]
		internal unsafe int* Probe(int offset, int length)
		{
			return (int*)this._checkedPointer.Probe(offset * 4, length * 4);
		}

		// Token: 0x04001CE4 RID: 7396
		private CheckedPointer _checkedPointer;
	}
}
