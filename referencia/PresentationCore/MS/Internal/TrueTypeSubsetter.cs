using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal
{
	// Token: 0x02000004 RID: 4
	internal static class TrueTypeSubsetter
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000112C4 File Offset: 0x000106C4
		[SecurityCritical]
		public unsafe static byte[] ComputeSubset(void* fontData, int fileSize, Uri sourceUri, int directoryOffset, ushort[] glyphArray)
		{
			byte* ptr = null;
			uint num = 0;
			uint num2 = 0;
			ref ushort pusKeepCharCodeList = ref glyphArray[0];
			short num3 = <Module>.MS.Internal.TtfDelta.CreateDeltaTTF((byte*)fontData, fileSize, &ptr, &num, &num2, 0, 0, 0, 0, 1, ref pusKeepCharCodeList, (ushort)glyphArray.Length, ldftn(MS.Internal.TtfDelta.Mem_ReAlloc), ldftn(MS.Internal.TtfDelta.Mem_Free), directoryOffset, null);
			byte[] array = null;
			try
			{
				if (num3 == 0)
				{
					array = new byte[num2];
					Marshal.Copy((IntPtr)((void*)ptr), array, 0, num2);
				}
			}
			finally
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			}
			if (num3 == 1007)
			{
				if (BaseAppContextSwitches.AllowFontReuseDuringFontSubsetting)
				{
					array = new byte[fileSize];
					Marshal.Copy((IntPtr)fontData, array, 0, fileSize);
					return array;
				}
			}
			else if (num3 == 0)
			{
				return array;
			}
			throw new FileFormatException(sourceUri);
		}
	}
}
