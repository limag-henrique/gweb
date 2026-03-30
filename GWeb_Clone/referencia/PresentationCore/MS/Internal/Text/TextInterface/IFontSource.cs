using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000015 RID: 21
	internal interface IFontSource
	{
		// Token: 0x060002CA RID: 714
		void TestFileOpenable();

		// Token: 0x060002CB RID: 715
		UnmanagedMemoryStream GetUnmanagedStream();

		// Token: 0x060002CC RID: 716
		DateTime GetLastWriteTimeUtc();

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002CD RID: 717
		Uri Uri { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002CE RID: 718
		bool IsComposite { [return: MarshalAs(UnmanagedType.U1)] get; }
	}
}
