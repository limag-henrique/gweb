using System;
using System.Security;
using MS.Internal.FontCache;

namespace MS.Internal
{
	// Token: 0x02000686 RID: 1670
	internal struct CheckedUShortPointer
	{
		// Token: 0x06004983 RID: 18819 RVA: 0x0011EC14 File Offset: 0x0011E014
		[SecurityCritical]
		internal unsafe CheckedUShortPointer(ushort* pointer, int length)
		{
			this._checkedPointer = new CheckedPointer((void*)pointer, length * 2);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x0011EC30 File Offset: 0x0011E030
		[SecurityCritical]
		internal unsafe ushort* Probe(int offset, int length)
		{
			return (ushort*)this._checkedPointer.Probe(offset * 2, length * 2);
		}

		// Token: 0x04001CE5 RID: 7397
		private CheckedPointer _checkedPointer;
	}
}
