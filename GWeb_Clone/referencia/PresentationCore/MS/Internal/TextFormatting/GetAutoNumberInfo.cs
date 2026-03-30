using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000719 RID: 1817
	// (Invoke) Token: 0x06004DD6 RID: 19926
	internal delegate LsErr GetAutoNumberInfo(IntPtr pols, ref LsKAlign alignment, ref LsChp lschp, ref IntPtr lsplsrun, ref ushort addedChar, ref LsChp lschpAddedChar, ref IntPtr lsplsrunAddedChar, ref int fWord95Model, ref int offset, ref int width);
}
