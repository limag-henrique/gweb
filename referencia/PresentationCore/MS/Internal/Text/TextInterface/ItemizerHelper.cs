using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000025 RID: 37
	internal class ItemizerHelper
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000B8A8 File Offset: 0x0000ACA8
		[return: MarshalAs(UnmanagedType.U1)]
		internal static bool IsExtendedCharacter(ushort ch)
		{
			return (((ch & 63488) == 55296) ? 1 : 0) != 0;
		}
	}
}
