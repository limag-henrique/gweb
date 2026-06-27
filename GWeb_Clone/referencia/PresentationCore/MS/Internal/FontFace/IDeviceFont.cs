using System;
using System.Security;

namespace MS.Internal.FontFace
{
	// Token: 0x02000769 RID: 1897
	internal interface IDeviceFont
	{
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06005004 RID: 20484
		string Name { get; }

		// Token: 0x06005005 RID: 20485
		bool ContainsCharacter(int unicodeScalar);

		// Token: 0x06005006 RID: 20486
		[SecurityCritical]
		unsafe void GetAdvanceWidths(char* characterString, int characterLength, double emSize, int* pAdvances);
	}
}
