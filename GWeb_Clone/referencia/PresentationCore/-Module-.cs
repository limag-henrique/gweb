using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Media;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using MS.Internal;
using MS.Internal.TtfDelta;
using vc_attributes;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x000030A4 File Offset: 0x000024A4
	[SecurityCritical]
	internal static ref char CriticalPtrToStringChars(string s)
	{
		ref byte ptr = s;
		if (ref ptr != null)
		{
			ptr = (ulong)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		return ref ptr;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000C3F0 File Offset: 0x0000B7F0
	[SecurityCritical]
	internal static short ExitCleanup(short errCode)
	{
		<Module>.MS.Internal.TtfDelta.Mem_End();
		return errCode;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000C404 File Offset: 0x0000B804
	[SecurityCritical]
	internal unsafe static short CopyOffsetDirectoryTables(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usFormat, uint* pulNewOutOffset)
	{
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04NBGNOCAJ@dttf@));
		ushort num3;
		short num2;
		if (num != null)
		{
			DTTF_HEADER dttf_HEADER;
			num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)(&dttf_HEADER), 18, (byte*)(&<Module>.MS.Internal.TtfDelta.DTTF_HEADER_CONTROL), num, &num3);
			if (num2 != 0)
			{
				return num2;
			}
			if (*(ref dttf_HEADER + 12) != 3)
			{
				return 1013;
			}
		}
		uint num4 = *(int*)(pInputBufferInfo + 12L / (long)sizeof(CONST_TTFACC_FILEBUFFERINFO));
		OFFSET_TABLE offset_TABLE;
		num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num4, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		ushort num5 = *(ref offset_TABLE + 4);
		num4 += num3;
		int num6 = (num == 0) ? 1 : 0;
		DIRECTORY* ptr = (DIRECTORY*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)((int)(*(ref offset_TABLE + 4)) + num6) * 16L));
		if (ptr == null)
		{
			return 1005;
		}
		ushort num7 = 0;
		ushort num8 = 0;
		if (0 < num5)
		{
			for (;;)
			{
				DIRECTORY directory;
				num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num4, &num3);
				num4 += num3;
				if (num2 == 0)
				{
					if (usFormat != 2)
					{
						goto IL_194;
					}
					if (directory <= 1751412088)
					{
						if (directory == 1751412088)
						{
							goto IL_194;
						}
						if (directory <= 1651273571)
						{
							if (directory == 1651273571 || directory == 1161970772 || directory == 1161972803 || directory == 1280594760 || directory == 1650745716)
							{
								goto IL_194;
							}
						}
						else if (directory == 1668112752 || directory == 1685353574 || directory == 1735162214)
						{
							goto IL_194;
						}
					}
					else if (directory <= 1819239265)
					{
						if (directory == 1819239265 || directory == 1751474532 || directory == 1751672161 || directory == 1752003704)
						{
							goto IL_194;
						}
					}
					else if (directory == 1835104368 || directory == 1986553185 || directory == 1986884728)
					{
						goto IL_194;
					}
					IL_1B4:
					num8 += 1;
					if (num8 >= num5)
					{
						break;
					}
					continue;
					IL_194:
					DIRECTORY* ptr2 = num7 * 16L + ptr / sizeof(DIRECTORY);
					*(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)) = 0;
					*(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)) = 0;
					*(int*)ptr2 = directory;
					num7 += 1;
					goto IL_1B4;
				}
				goto IL_1C5;
			}
			goto IL_1CE;
			IL_1C5:
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return num2;
		}
		IL_1CE:
		if ((num == null && usFormat == 1) || usFormat == 2)
		{
			DIRECTORY* ptr2 = num7 * 16L + ptr / sizeof(DIRECTORY);
			*(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)) = 0;
			*(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)) = 0;
			*(int*)ptr2 = 1685353574;
			num7 += 1;
			<Module>.MS.Internal.TtfDelta.SortByTag(ptr, num7);
		}
		*(ref offset_TABLE + 4) = (short)num7;
		num4 = *(int*)(pOutputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO));
		ushort num9;
		num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num4, &num9);
		num4 += num9;
		if (num2 == 0)
		{
			uint num10;
			num2 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, (byte*)ptr, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num4, &num10, num7, 16);
			if (num2 == 0)
			{
				*(int*)pulNewOutOffset = num10 + num4;
			}
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num2;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000FE38 File Offset: 0x0000F238
	[SecurityCritical]
	internal unsafe static short CopyForgottenTables(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint* pulNewOutOffset)
	{
		uint num = *(int*)(pOutputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO));
		OFFSET_TABLE offset_TABLE;
		ushort num2;
		if (<Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num, &num2) != 0)
		{
			return 1005;
		}
		num += num2;
		ushort num3 = *(ref offset_TABLE + 4);
		DIRECTORY* ptr = (DIRECTORY*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref offset_TABLE + 4)) * 16UL);
		if (ptr == null)
		{
			return 1005;
		}
		uint num5;
		short num4 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pOutputBufferInfo, (byte*)ptr, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num5, num3, 16);
		if (num4 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return num4;
		}
		<Module>.MS.Internal.TtfDelta.SortByOffset(ptr, num3);
		ushort num6 = 0;
		if (0 < num3)
		{
			do
			{
				DIRECTORY* ptr2 = num6 * 16L + ptr / sizeof(DIRECTORY);
				if (*(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)) != 0 || *(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)) != 0)
				{
					break;
				}
				uint num7 = (uint)(*(int*)ptr2);
				if (num7 != 16843009U)
				{
					$ArrayType$$$BY04D $ArrayType$$$BY04D;
					<Module>.MS.Internal.TtfDelta.ConvertLongTagToString(num7, (sbyte*)(&$ArrayType$$$BY04D));
					num4 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&$ArrayType$$$BY04D), pulNewOutOffset);
					if (num4 != 0)
					{
						break;
					}
				}
				num6 += 1;
			}
			while (num6 < num3);
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num4;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000030C0 File Offset: 0x000024C0
	[SecurityCritical]
	internal unsafe static void FillGlyphIndexArray(byte* puchKeepGlyphList, ushort usGlyphListCount, ushort* pusGlyphIndexArray, ushort usDttfGlyphIndexCount)
	{
		ushort num = 0;
		ushort num2 = 0;
		if (0 < usGlyphListCount)
		{
			while (num < usDttfGlyphIndexCount)
			{
				if (num2[puchKeepGlyphList] != 0)
				{
					(num * 2L)[pusGlyphIndexArray / 2] = num2;
					num += 1;
				}
				num2 += 1;
				if (num2 >= usGlyphListCount)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000FF0C File Offset: 0x0000F30C
	[SecurityCritical]
	internal unsafe static short CompactMaxpLocaTable(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort usDttfGlyphIndexCount)
	{
		if (usDttfGlyphIndexCount == 0)
		{
			return 0;
		}
		HEAD head;
		if (<Module>.MS.Internal.TtfDelta.GetHead(pOutputBufferInfo, &head) == null)
		{
			return 1032;
		}
		uint* ptr = (uint*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(usGlyphListCount + 1) * 4L));
		if (ptr == null)
		{
			return 1005;
		}
		uint num = <Module>.MS.Internal.TtfDelta.GetLoca(pOutputBufferInfo, ptr, usGlyphListCount + 1);
		if (num == null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return 1035;
		}
		uint ulNewLength;
		if (*(ref head + 50) == 0)
		{
			ushort num2 = 0;
			ushort num3 = 0;
			while (num3 <= usDttfGlyphIndexCount)
			{
				if (num3 == usDttfGlyphIndexCount || (num2 < usGlyphListCount && num2[puchKeepGlyphList] != 0))
				{
					ushort usValue = (ushort)((uint)(num2 * 4L)[ptr / 4] >> 1);
					if (<Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, usValue, (uint)((ulong)num3 * 2UL + num)) != 0)
					{
						break;
					}
					num3 += 1;
				}
				num2 += 1;
				if (num2 > usGlyphListCount)
				{
					break;
				}
			}
			ulNewLength = (uint)((ulong)(usDttfGlyphIndexCount + 1) * 2UL);
		}
		else
		{
			ushort num2 = 0;
			ushort num3 = 0;
			while (num3 <= usDttfGlyphIndexCount)
			{
				if (num3 == usDttfGlyphIndexCount || (num2 < usGlyphListCount && num2[puchKeepGlyphList] != 0))
				{
					if (<Module>.MS.Internal.TtfDelta.WriteLong(pOutputBufferInfo, (num2 * 4L)[ptr / 4], (uint)((ulong)num3 * 4UL + num)) != 0)
					{
						break;
					}
					num3 += 1;
				}
				num2 += 1;
				if (num2 > usGlyphListCount)
				{
					break;
				}
			}
			ulNewLength = (uint)((ulong)(usDttfGlyphIndexCount + 1) * 4UL);
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		short num4 = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@), ulNewLength);
		if (num4 != 0)
		{
			return num4;
		}
		MAXP maxp;
		uint num5 = <Module>.MS.Internal.TtfDelta.GetMaxp(pOutputBufferInfo, &maxp);
		if (num5 == null)
		{
			return 1036;
		}
		*(ref maxp + 4) = (short)usDttfGlyphIndexCount;
		ushort num6;
		return <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&maxp), 32, (byte*)(&<Module>.MS.Internal.TtfDelta.MAXP_CONTROL), num5, &num6);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000C664 File Offset: 0x0000BA64
	[SecurityCritical]
	internal unsafe static short UpdatePrivateTable(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint* pulNewOutOffset, ushort* pusGlyphIndexArray, ushort usDttfGlyphIndexCount, ushort usNumGlyphs, ushort usFormat, uint ulCheckSum)
	{
		if (usFormat != 1 && usFormat != 2)
		{
			return 0;
		}
		DIRECTORY directory;
		uint num = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04NBGNOCAJ@dttf@), &directory);
		short num2 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, *(int*)pulNewOutOffset, ref directory + 8);
		if (num2 != 0)
		{
			return num2;
		}
		*(ref directory + 12) = (int)((uint)((ulong)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.DTTF_HEADER_CONTROL)) + usDttfGlyphIndexCount * 2UL));
		if (num == null)
		{
			return 1000;
		}
		ushort num3;
		num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		DTTF_HEADER dttf_HEADER = 65536;
		*(ref dttf_HEADER + 4) = ulCheckSum;
		*(ref dttf_HEADER + 8) = usNumGlyphs;
		*(ref dttf_HEADER + 10) = (short)(*(ushort*)((usDttfGlyphIndexCount - 1) * 2L + pusGlyphIndexArray / 2));
		*(ref dttf_HEADER + 12) = usFormat;
		*(ref dttf_HEADER + 14) = 0;
		*(ref dttf_HEADER + 16) = usDttfGlyphIndexCount;
		num = *(ref directory + 8);
		num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&dttf_HEADER), 18, (byte*)(&<Module>.MS.Internal.TtfDelta.DTTF_HEADER_CONTROL), *(ref directory + 8), &num3);
		if (num2 != 0)
		{
			return num2;
		}
		num += num3;
		ushort num4 = 0;
		if (0 < usDttfGlyphIndexCount)
		{
			do
			{
				num2 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, (num4 * 2L)[pusGlyphIndexArray / 2], num);
				if (num2 != 0)
				{
					return num2;
				}
				num = (uint)(num + 2UL);
				num4 += 1;
			}
			while (num4 < usDttfGlyphIndexCount);
		}
		num2 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, num, &num);
		if (num2 != 0)
		{
			return num2;
		}
		*(int*)pulNewOutOffset = num;
		return 0;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000C77C File Offset: 0x0000BB7C
	[SecurityCritical]
	internal unsafe static void CalcOutputBufferSize(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usGlyphListCount, ushort usGlyphKeepCount, ushort usFormat, uint ulSrcBufferSize, uint* pulOutputBufferLength)
	{
		uint num = 0;
		int num2 = (usGlyphListCount - usGlyphKeepCount) * 100 / usGlyphListCount - 10;
		num2 = ((num2 < 0) ? 0 : num2);
		int num3 = 100 - num2;
		uint num4 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DBJHPCCB@EBDT@));
		uint num5 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@));
		if (num4 != null && num4 == num5)
		{
			num5 = ((<Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DBJHPCCB@EBDT@)) != <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@))) ? 0 : num5);
		}
		uint num6 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LLIHEPK@glyf@)) + num5 + num4;
		if (usFormat == 2 || usFormat == 1)
		{
			num = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LGOLHALL@LTSH@));
			num += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04ONMNCIMC@hmtx@));
			num += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DCBNABCB@vmtx@));
			num += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDDCPPLH@hdmx@));
			num += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@));
		}
		num += num6;
		if (usFormat == 2)
		{
			uint num7 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04NEODDMOL@head@));
			num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04KODIGLGG@maxp@));
			num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04FMPHLIKP@hhea@));
			num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDCHJBEM@vhea@));
			num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@));
			if (num4 > 0)
			{
				num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DKAHCHAP@EBLC@));
			}
			if (num5 > 0)
			{
				num7 += <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04KGIENAAN@bloc@));
			}
			*(int*)pulOutputBufferLength = num3 * num / 100 + num7;
		}
		else
		{
			*(int*)pulOutputBufferLength = ulSrcBufferSize - num2 * num / 100;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00011110 File Offset: 0x00010510
	[SecurityCritical]
	internal unsafe static short CreateDeltaTTF(byte* puchSrcBuffer, uint ulSrcBufferSize, byte** ppuchDestBuffer, uint* pulDestBufferSize, uint* pulBytesWritten, ushort usFormat, ushort usLanguage, ushort usPlatform, ushort usEncoding, ushort usListType, ushort* pusKeepCharCodeList, ushort usListCount, method lpfnReAllocate, method lpfnFree, uint ulOffsetTableOffset, void* lpvReserved)
	{
		ushort num = 0;
		$ArrayType$$$BY03K $ArrayType$$$BY03K = 100;
		*(ref $ArrayType$$$BY03K + 4) = 114;
		*(ref $ArrayType$$$BY03K + 8) = 77;
		*(ref $ArrayType$$$BY03K + 12) = 34;
		uint* ptr = null;
		ushort num2 = 0;
		short num4;
		if (pusKeepCharCodeList != null)
		{
			if (usListType == 1)
			{
				CONST_TTFACC_FILEBUFFERINFO const_TTFACC_FILEBUFFERINFO = puchSrcBuffer;
				*(ref const_TTFACC_FILEBUFFERINFO + 8) = ulSrcBufferSize;
				*(ref const_TTFACC_FILEBUFFERINFO + 12) = ulOffsetTableOffset;
				*(ref const_TTFACC_FILEBUFFERINFO + 16) = 0L;
				ushort num3 = <Module>.MS.Internal.TtfDelta.GetNumGlyphs((TTFACC_FILEBUFFERINFO*)(&const_TTFACC_FILEBUFFERINFO));
				if (num3 == 0)
				{
					return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(1009);
				}
				byte* ptr2 = (byte*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num3);
				if (ptr2 == null)
				{
					return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(1005);
				}
				ushort num5;
				num4 = <Module>.MS.Internal.TtfDelta.MakeKeepGlyphList((TTFACC_FILEBUFFERINFO*)(&const_TTFACC_FILEBUFFERINFO), 0, 3, 1, (uint*)(&$ArrayType$$$BY03K), 4, ptr2, num3, &num5, &num, 0);
				if (num4 != 0)
				{
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
					return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(num4);
				}
				num2 = num + usListCount;
				ptr = (uint*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num2 * 4UL);
				if (ptr == null)
				{
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
					return 1005;
				}
				ushort num6 = 0;
				if (0 < usListCount)
				{
					do
					{
						long num7 = (long)((ulong)num6);
						*(int*)(num7 * 4L / (long)sizeof(uint) + ptr) = (int)(*(ushort*)(num7 * 2L / (long)sizeof(ushort) + pusKeepCharCodeList));
						num6 += 1;
					}
					while (num6 < usListCount);
				}
				ushort num8 = 0;
				ushort num9 = usListCount;
				while (num9 < num2)
				{
					if (num8[ptr2] != 0)
					{
						(num9 * 4L)[ptr / 4] = (int)num8;
						num9 += 1;
					}
					num8 += 1;
					if (num8 > num5)
					{
						break;
					}
				}
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
			}
			else
			{
				ptr = (uint*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((usListCount + 4) * 4L));
				if (ptr == null)
				{
					return 1005;
				}
				if (<Module>.MS.Internal.TtfDelta.UTF16toUCS4((ushort*)pusKeepCharCodeList, usListCount, ptr, usListCount, &num2) != 0)
				{
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
					return 1005;
				}
				ushort num10 = 0;
				do
				{
					*(int*)((long)(num10 + num2) * 4L + ptr / 4) = *((ulong)num10 * 4UL + ref $ArrayType$$$BY03K);
					num10 += 1;
				}
				while (num10 < 4);
				num2 += 4;
			}
		}
		num4 = <Module>.MS.Internal.TtfDelta.CreateDeltaTTFEx(puchSrcBuffer, ulSrcBufferSize, ppuchDestBuffer, pulDestBufferSize, pulBytesWritten, usFormat, usLanguage, usPlatform, usEncoding, usListType, (uint*)ptr, num2, lpfnReAllocate, lpfnFree, ulOffsetTableOffset, lpvReserved);
		if (ptr != null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		}
		return num4;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00010048 File Offset: 0x0000F448
	[SecurityCritical]
	internal unsafe static short CreateDeltaTTFEx(byte* puchSrcBuffer, uint ulSrcBufferSize, byte** ppuchDestBuffer, uint* pulDestBufferSize, uint* pulBytesWritten, ushort usFormat, ushort usLanguage, ushort usPlatform, ushort usEncoding, ushort usListType, uint* pulKeepCharCodeList, ushort usListCount, method lpfnReAllocate, method lpfnFree, uint ulOffsetTableOffset, void* lpvReserved)
	{
		ushort num = 0;
		ushort num2 = 0;
		ushort maxValue = ushort.MaxValue;
		ushort usMaxChr = 0;
		uint ulCheckSum = 0;
		int num3 = 1;
		uint num4 = 0;
		if (puchSrcBuffer == null)
		{
			return 1100;
		}
		if (ulSrcBufferSize == null)
		{
			return 1101;
		}
		if (ppuchDestBuffer == null)
		{
			return 1102;
		}
		if (pulDestBufferSize == null)
		{
			return 1103;
		}
		if (pulBytesWritten == null)
		{
			return 1104;
		}
		if (usFormat > 2)
		{
			return 1105;
		}
		if (<Module>.MS.Internal.TtfDelta.Mem_Init() != 0)
		{
			return 1005;
		}
		CONST_TTFACC_FILEBUFFERINFO const_TTFACC_FILEBUFFERINFO = puchSrcBuffer;
		*(ref const_TTFACC_FILEBUFFERINFO + 8) = ulSrcBufferSize;
		*(ref const_TTFACC_FILEBUFFERINFO + 12) = ulOffsetTableOffset;
		*(ref const_TTFACC_FILEBUFFERINFO + 16) = 0L;
		*(int*)pulBytesWritten = 0;
		ushort num5 = <Module>.MS.Internal.TtfDelta.GetNumGlyphs((TTFACC_FILEBUFFERINFO*)(&const_TTFACC_FILEBUFFERINFO));
		if (num5 == 0)
		{
			return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(1009);
		}
		byte* ptr = (byte*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num5);
		if (ptr == null)
		{
			return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(1005);
		}
		ushort usMaxGlyphIndexUsed;
		short num6 = <Module>.MS.Internal.TtfDelta.MakeKeepGlyphList((TTFACC_FILEBUFFERINFO*)(&const_TTFACC_FILEBUFFERINFO), usListType, usPlatform, usEncoding, pulKeepCharCodeList, usListCount, ptr, num5, &usMaxGlyphIndexUsed, &num2, 1);
		if (num6 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(num6);
		}
		if (*(long*)ppuchDestBuffer == 0L || *(int*)pulDestBufferSize == 0)
		{
			<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.CalcOutputBufferSize(&const_TTFACC_FILEBUFFERINFO, num5, num2, usFormat, ulSrcBufferSize, pulDestBufferSize);
			void* ptr2 = calli(System.Void*(System.Void*,System.UInt64), 0L, (ulong)(*(int*)pulDestBufferSize), lpfnReAllocate);
			*(long*)ppuchDestBuffer = ptr2;
			if (ptr2 == null)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(1005);
			}
		}
		TTFACC_FILEBUFFERINFO ttfacc_FILEBUFFERINFO = *(long*)ppuchDestBuffer;
		*(ref ttfacc_FILEBUFFERINFO + 8) = *(int*)pulDestBufferSize;
		*(ref ttfacc_FILEBUFFERINFO + 12) = 0;
		*(ref ttfacc_FILEBUFFERINFO + 16) = lpfnReAllocate;
		if (BaseAppContextSwitches.AllowFontReuseDuringFontSubsetting)
		{
			*(long*)ppuchDestBuffer = 0L;
		}
		if (usFormat == 1 || usFormat == 2)
		{
			num = num2;
		}
		num6 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.CopyOffsetDirectoryTables(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, usFormat, &num4);
		if (num6 == 0)
		{
			num6 = <Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04NEODDMOL@head@), &num4);
			if (num6 == 0)
			{
				num6 = <Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04FMPHLIKP@hhea@), &num4);
				if (num6 == 0)
				{
					num6 = <Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04KODIGLGG@maxp@), &num4);
					if (num6 == 0)
					{
						if (usFormat != 2)
						{
							<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04OEKHDCJK@OS?12@), &num4);
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModXmtxXhea(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, num, usMaxGlyphIndexUsed, 1, &num4);
						if (num6 != 0)
						{
							if (num6 != 1007)
							{
								goto IL_357;
							}
							num3 = 0;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModLTSH(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, num, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModVDMX(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, usFormat, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						if (num3 == 1)
						{
							num6 = <Module>.MS.Internal.TtfDelta.ModHdmx(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, num, &num4);
							if (num6 != 0)
							{
								goto IL_3D1;
							}
						}
						else
						{
							<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04IDDCPPLH@hdmx@), &num4);
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModCmap(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, ptr, num5, &maxValue, &usMaxChr, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						if (usFormat != 2)
						{
							<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04NJFLOCNM@fpgm@), &num4);
							<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04GABKPAAH@prep@), &num4);
							<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04GPGHBOBB@cvt?5@), &num4);
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModGlyfLocaAndHead(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, ptr, num5, &ulCheckSum, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModMaxP(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModOS2(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, maxValue, usMaxChr, usFormat, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModKern(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, usFormat, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModName(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, usLanguage, usFormat, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						num6 = <Module>.MS.Internal.TtfDelta.ModPost(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, usFormat, &num4);
						if (num6 != 0)
						{
							goto IL_3D1;
						}
						<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04OINNJMCG@gasp@), &num4);
						<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04CPDINMAO@PCLT@), &num4);
						<Module>.MS.Internal.TtfDelta.CopyTableOver(&ttfacc_FILEBUFFERINFO, &const_TTFACC_FILEBUFFERINFO, (sbyte*)(&<Module>.??_C@_04IDCHJBEM@vhea@), &num4);
						num6 = <Module>.MS.Internal.TtfDelta.ModXmtxXhea(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, num, usMaxGlyphIndexUsed, 0, &num4);
						if (num6 == 0 || num6 == 1007)
						{
							num6 = <Module>.MS.Internal.TtfDelta.ModSbit(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, (byte*)ptr, num5, &num4);
						}
						IL_357:
						if (num6 == 0)
						{
							if (num != 0)
							{
								num6 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.CompactMaxpLocaTable(&ttfacc_FILEBUFFERINFO, ptr, num5, num);
								if (num6 != 0)
								{
									goto IL_3D1;
								}
								ushort* ptr3 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num * 2UL);
								if (ptr3 == null)
								{
									num6 = 1005;
									goto IL_3D1;
								}
								<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FillGlyphIndexArray((byte*)ptr, num5, ptr3, num);
								num6 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdatePrivateTable(&ttfacc_FILEBUFFERINFO, &num4, (ushort*)ptr3, num, num5, usFormat, ulCheckSum);
								<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr3);
								if (num6 != 0)
								{
									goto IL_3D1;
								}
							}
							num6 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.CopyForgottenTables(&const_TTFACC_FILEBUFFERINFO, &ttfacc_FILEBUFFERINFO, &num4);
							if (num6 == 0)
							{
								num6 = <Module>.MS.Internal.TtfDelta.CompressTables(&ttfacc_FILEBUFFERINFO, &num4);
								if (num6 == 0)
								{
									<Module>.MS.Internal.TtfDelta.SetFileChecksum(&ttfacc_FILEBUFFERINFO, num4);
								}
							}
						}
					}
				}
			}
		}
		IL_3D1:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		if (num6 == 0)
		{
			if (num4 <= ulSrcBufferSize)
			{
				*(long*)ppuchDestBuffer = ttfacc_FILEBUFFERINFO;
				*(int*)pulDestBufferSize = *(ref ttfacc_FILEBUFFERINFO + 8);
				*(int*)pulBytesWritten = num4;
				goto IL_40C;
			}
			num6 = 1007;
		}
		if (*(long*)ppuchDestBuffer == 0L && lpfnFree != null)
		{
			calli(System.Void(System.Void*), ttfacc_FILEBUFFERINFO, lpfnFree);
		}
		IL_40C:
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ExitCleanup(num6);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000030F8 File Offset: 0x000024F8
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void* Mem_Alloc(ulong size)
	{
		return <Module>.calloc(1UL, size);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00003110 File Offset: 0x00002510
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void Real_Mem_Free(void* pv)
	{
		<Module>.free(pv);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00003124 File Offset: 0x00002524
	[SecurityCritical]
	internal unsafe static void Mem_Free(void* pv)
	{
		if (pv != null)
		{
			<Module>.MS.Internal.TtfDelta.Real_Mem_Free(pv);
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000313C File Offset: 0x0000253C
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void* Mem_ReAlloc(void* @base, ulong newSize)
	{
		return <Module>.realloc(@base, newSize);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003150 File Offset: 0x00002550
	[SecurityCritical]
	internal static short Mem_Init()
	{
		return 0;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00003160 File Offset: 0x00002560
	[SecurityCritical]
	internal static void Mem_End()
	{
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00005F7C File Offset: 0x0000537C
	[SecurityCritical]
	internal unsafe static short CheckInOffset(TTFACC_FILEBUFFERINFO* a, uint b, uint c)
	{
		if (*(long*)a == 0L)
		{
			return 1001;
		}
		uint num = (uint)(*(int*)(a + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO)));
		if (b <= num)
		{
			uint num2 = b + c;
			if (num2 <= num && num2 >= b)
			{
				return 0;
			}
		}
		return 1001;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00005FB0 File Offset: 0x000053B0
	[SecurityCritical]
	internal unsafe static short CheckOutOffset(TTFACC_FILEBUFFERINFO* a, uint b, uint c)
	{
		ulong num = (ulong)(*(long*)a);
		if (num == 0UL)
		{
			return 1002;
		}
		uint num2 = b + c;
		if (num2 < b)
		{
			return 1002;
		}
		uint num3 = (uint)(*(int*)(a + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO)));
		if (num2 > num3)
		{
			ulong num4 = (ulong)(*(long*)(a + 16L / (long)sizeof(TTFACC_FILEBUFFERINFO)));
			if (num4 == 0UL)
			{
				return 1002;
			}
			uint num5 = num3 * 11U / 10U;
			*(int*)(a + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO)) = ((num5 > num2) ? num5 : num2);
			long num6 = calli(System.Void*(System.Void*,System.UInt64), num, (ulong)(*(int*)(a + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO))), num4);
			*(long*)a = num6;
			if (num6 == 0L)
			{
				*(int*)(a + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO)) = 0;
				return 1005;
			}
		}
		return 0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00006030 File Offset: 0x00005430
	[SecurityCritical]
	internal unsafe static short ReadByte(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchBuffer, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulOffset, 1);
		if (num != 0)
		{
			return num;
		}
		*puchBuffer = *(ulOffset + (ulong)(*(long*)pInputBufferInfo));
		return 0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00006054 File Offset: 0x00005454
	[SecurityCritical]
	internal unsafe static short ReadWord(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort* pusBuffer, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulOffset, 2);
		if (num != 0)
		{
			return num;
		}
		long num2 = (long)(ulOffset + (ulong)(*(long*)pInputBufferInfo));
		*pusBuffer = (ushort)((int)(*num2) << 8 | (int)(*(num2 + 1L)));
		return 0;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00006088 File Offset: 0x00005488
	[SecurityCritical]
	internal unsafe static short ReadLong(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint* pulBuffer, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulOffset, 4);
		if (num != 0)
		{
			return num;
		}
		long num2 = (long)(ulOffset + (ulong)(*(long*)pInputBufferInfo));
		*(int*)pulBuffer = ((((int)(*(num2 + 1L)) | (int)(*num2) << 8) << 8 | (int)(*(num2 + 2L))) << 8 | (int)(*(num2 + 3L)));
		return 0;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000060CC File Offset: 0x000054CC
	[SecurityCritical]
	internal unsafe static short ReadBytes(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchBuffer, uint ulOffset, uint Count)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulOffset, Count);
		if (num != 0)
		{
			return num;
		}
		cpblk(puchBuffer, ulOffset + (ulong)(*(long*)pInputBufferInfo), Count);
		return 0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000060F8 File Offset: 0x000054F8
	[SecurityCritical]
	internal unsafe static short WriteByte(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte uchValue, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, ulOffset, 1);
		if (num != 0)
		{
			return num;
		}
		*(ulOffset + (ulong)(*(long*)pOutputBufferInfo)) = uchValue;
		return 0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000611C File Offset: 0x0000551C
	[SecurityCritical]
	internal unsafe static short WriteWord(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usValue, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, ulOffset, 2);
		if (num != 0)
		{
			return num;
		}
		*(ulOffset + (ulong)(*(long*)pOutputBufferInfo)) = (short)((int)usValue << 8 | (int)(*(ref usValue + 1)));
		return 0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000614C File Offset: 0x0000554C
	[SecurityCritical]
	internal unsafe static short WriteLong(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulValue, uint ulOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, ulOffset, 4);
		if (num != 0)
		{
			return num;
		}
		*(ulOffset + (ulong)(*(long*)pOutputBufferInfo)) = (((ulValue << 8 | *(ref ulValue + 1)) << 8 | *(ref ulValue + 2)) << 8 | *(ref ulValue + 3));
		return 0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000618C File Offset: 0x0000558C
	[SecurityCritical]
	internal unsafe static short WriteBytes(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchBuffer, uint ulOffset, uint Count)
	{
		short num = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, ulOffset, Count);
		if (num != 0)
		{
			return num;
		}
		cpblk(ulOffset + (ulong)(*(long*)pOutputBufferInfo), puchBuffer, Count);
		return 0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000061B8 File Offset: 0x000055B8
	[SecurityCritical]
	internal unsafe static short ReadGeneric(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchBuffer, ushort usBufferSize, byte* puchControl, uint ulOffset, ushort* pusBytesRead)
	{
		uint num = ulOffset;
		ushort num2 = 0;
		ushort num3 = (ushort)(*puchControl);
		ushort num4 = 1;
		if (1 <= num3)
		{
			short num7;
			for (;;)
			{
				byte b = num4[puchControl];
				int num5 = (int)(b & 7);
				if (num5 != 1)
				{
					if (num5 != 2)
					{
						if (num5 != 4)
						{
							return 1003;
						}
						ulong num6 = (ulong)num2 + 4UL;
						if (num6 > (ulong)usBufferSize)
						{
							return 1003;
						}
						byte* ptr = num2 + puchBuffer;
						uint* ptr2 = (uint*)ptr;
						if ((b & 16) != 0)
						{
							*(int*)ptr2 = 0;
						}
						else
						{
							if ((b & 32) != 0)
							{
								num7 = <Module>.MS.Internal.TtfDelta.ReadBytes(pInputBufferInfo, ptr, num, 4);
								if (num7 != 0)
								{
									break;
								}
							}
							else
							{
								num7 = <Module>.MS.Internal.TtfDelta.ReadLong(pInputBufferInfo, ptr2, num);
								if (num7 != 0)
								{
									return num7;
								}
							}
							num = (uint)(num + 4UL);
						}
						num2 = (ushort)((uint)num6);
					}
					else
					{
						ulong num8 = (ulong)num2 + 2UL;
						if (num8 > (ulong)usBufferSize)
						{
							return 1003;
						}
						byte* ptr3 = num2 + puchBuffer;
						ushort* ptr4 = (ushort*)ptr3;
						if ((b & 16) != 0)
						{
							*ptr4 = 0;
						}
						else
						{
							if ((b & 32) != 0)
							{
								num7 = <Module>.MS.Internal.TtfDelta.ReadBytes(pInputBufferInfo, ptr3, num, 2);
								if (num7 != 0)
								{
									return num7;
								}
							}
							else
							{
								num7 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, ptr4, num);
								if (num7 != 0)
								{
									return num7;
								}
							}
							num = (uint)(num + 2UL);
						}
						num2 = (ushort)((uint)num8);
					}
				}
				else
				{
					ulong num9 = (ulong)num2 + 1UL;
					if (num9 > (ulong)usBufferSize)
					{
						return 1003;
					}
					if ((b & 16) != 0)
					{
						num2[puchBuffer] = 0;
					}
					else
					{
						num7 = <Module>.MS.Internal.TtfDelta.ReadByte(pInputBufferInfo, num2 + puchBuffer, num);
						if (num7 != 0)
						{
							return num7;
						}
						num = (uint)(num + 1UL);
					}
					num2 = (ushort)((uint)num9);
				}
				num4 += 1;
				if (num4 > num3)
				{
					goto IL_15E;
				}
			}
			return num7;
		}
		IL_15E:
		if (num2 < usBufferSize)
		{
			return 1003;
		}
		*pusBytesRead = num - ulOffset;
		return 0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00006338 File Offset: 0x00005738
	[SecurityCritical]
	internal unsafe static short ReadGenericRepeat(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchBuffer, byte* puchControl, uint ulOffset, uint* pulBytesRead, ushort usItemCount, ushort usItemSize)
	{
		ushort num = 0;
		if (0 < usItemCount)
		{
			do
			{
				ushort num3;
				short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, puchBuffer, usItemSize, puchControl, ulOffset, &num3);
				if (num2 != 0)
				{
					return num2;
				}
				ulOffset += num3;
				puchBuffer = usItemSize + puchBuffer;
				num += 1;
			}
			while (num < usItemCount);
		}
		*(int*)pulBytesRead = (int)(usItemCount * usItemSize);
		return 0;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00006380 File Offset: 0x00005780
	[SecurityCritical]
	internal unsafe static short WriteGeneric(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchBuffer, ushort usBufferSize, byte* puchControl, uint ulOffset, ushort* pusBytesWritten)
	{
		uint num = ulOffset;
		ushort num2 = 0;
		ushort num3 = (ushort)(*puchControl);
		ushort num4 = 1;
		if (1 <= num3)
		{
			short num6;
			for (;;)
			{
				byte b = num4[puchControl];
				int num5 = (int)(b & 7);
				if (num5 != 1)
				{
					if (num5 != 2)
					{
						if (num5 != 4)
						{
							return 1004;
						}
						if ((b & 16) == 0)
						{
							if ((ulong)num2 + 4UL > (ulong)usBufferSize)
							{
								return 1004;
							}
							if ((b & 32) != 0)
							{
								num6 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, num2 + puchBuffer, num, 4);
								if (num6 != 0)
								{
									break;
								}
							}
							else
							{
								num6 = <Module>.MS.Internal.TtfDelta.WriteLong(pOutputBufferInfo, num2[puchBuffer / 4], num);
								if (num6 != 0)
								{
									return num6;
								}
							}
							num = (uint)(num + 4UL);
						}
						num2 = (ushort)((uint)((ulong)num2 + 4UL));
					}
					else
					{
						if ((b & 16) == 0)
						{
							if ((ulong)num2 + 2UL > (ulong)usBufferSize)
							{
								return 1004;
							}
							if ((b & 32) != 0)
							{
								short num7 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, num2 + puchBuffer, num, 2);
								if (num7 != 0)
								{
									return num7;
								}
							}
							else
							{
								short num7 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, num2[puchBuffer / 2], num);
								if (num7 != 0)
								{
									return num7;
								}
							}
							num = (uint)(num + 2UL);
						}
						num2 = (ushort)((uint)((ulong)num2 + 2UL));
					}
				}
				else
				{
					if ((b & 16) == 0)
					{
						if ((ulong)num2 + 1UL > (ulong)usBufferSize)
						{
							return 1004;
						}
						short num8 = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, num2[puchBuffer], num);
						if (num8 != 0)
						{
							return num8;
						}
						num = (uint)(num + 1UL);
					}
					num2 = (ushort)((uint)((ulong)num2 + 1UL));
				}
				num4 += 1;
				if (num4 > num3)
				{
					goto IL_14A;
				}
			}
			return num6;
		}
		IL_14A:
		if (num2 < usBufferSize)
		{
			return 1004;
		}
		uint num9 = num - ulOffset;
		ushort num10 = num9;
		*pusBytesWritten = num10;
		return (num9 != num10) ? 1006 : 0;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00006500 File Offset: 0x00005900
	[SecurityCritical]
	internal unsafe static short WriteGenericRepeat(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchBuffer, byte* puchControl, uint ulOffset, uint* pulBytesWritten, ushort usItemCount, ushort usItemSize)
	{
		ushort num = 0;
		if (0 < usItemCount)
		{
			do
			{
				ushort num3;
				short num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, puchBuffer, usItemSize, puchControl, ulOffset, &num3);
				if (num2 != 0)
				{
					return num2;
				}
				ulOffset += num3;
				puchBuffer = usItemSize + puchBuffer;
				num += 1;
			}
			while (num < usItemCount);
		}
		*(int*)pulBytesWritten = (int)(usItemCount * usItemSize);
		return 0;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00006548 File Offset: 0x00005948
	[SecurityCritical]
	internal unsafe static ushort GetGenericSize(byte* puchControl)
	{
		ushort num = 0;
		ushort num2 = (ushort)(*puchControl);
		ushort num3 = 1;
		if (1 <= num2)
		{
			for (;;)
			{
				byte b = num3[puchControl];
				int num4 = (int)(b & 7);
				if (num4 != 1)
				{
					if (num4 != 2)
					{
						if (num4 != 4)
						{
							break;
						}
						if ((b & 16) == 0)
						{
							num = (ushort)((uint)((ulong)num + 4UL));
						}
					}
					else if ((b & 16) == 0)
					{
						num = (ushort)((uint)((ulong)num + 2UL));
					}
				}
				else if ((b & 16) == 0)
				{
					num = (ushort)((uint)((ulong)num + 1UL));
				}
				num3 += 1;
				if (num3 > num2)
				{
					return num;
				}
			}
			return 0;
		}
		return num;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000065B8 File Offset: 0x000059B8
	[SecurityCritical]
	internal unsafe static short CalcChecksum(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, uint ulLength, uint* pulChecksum)
	{
		*(int*)pulChecksum = 0;
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulOffset, ulLength);
		if (num != 0)
		{
			return num;
		}
		uint num2 = (ulLength & -4) + ulOffset;
		if (ulOffset < num2)
		{
			do
			{
				long num3 = (long)(ulOffset + (ulong)(*(long*)pInputBufferInfo));
				*(int*)pulChecksum = *(int*)pulChecksum + ((((int)(*(num3 + 1L)) | (int)(*num3) << 8) << 8 | (int)(*(num3 + 2L))) << 8 | (int)(*(num3 + 3L)));
				ulOffset = (uint)(ulOffset + 4UL);
			}
			while (ulOffset < num2);
		}
		uint num4 = ulLength & 3;
		if (num4 != 0U)
		{
			uint num5 = 0;
			uint num6 = 0;
			if (0U < num4)
			{
				long num7 = (long)(ulOffset + (ulong)(*(long*)pInputBufferInfo));
				uint num8 = num4;
				num6 = num8;
				do
				{
					num5 = num5 * 256 + *num7;
					num7 += 1L;
					num8 += uint.MaxValue;
				}
				while (num8 > 0U);
			}
			*(int*)pulChecksum = *(int*)pulChecksum + (num5 << (4 - num6) * 8);
		}
		return 0;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00006660 File Offset: 0x00005A60
	[SecurityCritical]
	internal unsafe static ushort CalcFileChecksum(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulLength, uint* pulChecksum)
	{
		return (ushort)<Module>.MS.Internal.TtfDelta.CalcChecksum(pInputBufferInfo, 0, ulLength, pulChecksum);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00006678 File Offset: 0x00005A78
	[SecurityCritical]
	internal unsafe static ushort UTF16toUCS4(ushort* pUTF16, ushort usCountUTF16, uint* pUCS4, ushort usCountUCS4, ushort* pusChars)
	{
		*pusChars = 0;
		ushort num = 0;
		if (0 < usCountUTF16)
		{
			for (;;)
			{
				ushort num2 = (num * 2L)[pUTF16 / 2];
				num += 1;
				if (num >= usCountUTF16 || num2 + 10240 > 1023)
				{
					uint num3 = num2;
					goto IL_67;
				}
				ushort num4 = (num * 2L)[pUTF16 / 2];
				num += 1;
				if (num4 + 9216 <= 1023)
				{
					uint num3 = ((int)num2 << 10) + (int)num4 - 56613888;
					goto IL_67;
				}
				num += ushort.MaxValue;
				IL_7F:
				if (num >= usCountUTF16)
				{
					break;
				}
				continue;
				IL_67:
				ushort num5 = *pusChars;
				if (num5 < usCountUCS4)
				{
					uint num3;
					(num5 * 4L)[pUCS4 / 4] = num3;
				}
				*pusChars += 1;
				goto IL_7F;
			}
			if (*pusChars > usCountUCS4)
			{
				return 1005;
			}
		}
		return 0;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00006718 File Offset: 0x00005B18
	[SecurityCritical]
	internal unsafe static void InitFileBufferInfo(TTFACC_FILEBUFFERINFO* pBufferInfo, byte* puchBuffer, uint ulBufferSize, method lpfnReAlloc)
	{
		*(long*)pBufferInfo = puchBuffer;
		*(int*)(pBufferInfo + 8L / (long)sizeof(TTFACC_FILEBUFFERINFO)) = ulBufferSize;
		*(int*)(pBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO)) = 0;
		*(long*)(pBufferInfo + 16L / (long)sizeof(TTFACC_FILEBUFFERINFO)) = lpfnReAlloc;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00006990 File Offset: 0x00005D90
	[SecurityCritical]
	internal unsafe static void ConvertLongTagToString(uint ulTag, sbyte* szTag)
	{
		uint num = ((ulTag << 8 | *(ref ulTag + 1)) << 8 | *(ref ulTag + 2)) << 8 | *(ref ulTag + 3);
		cpblk(szTag, ref num, 4);
		*(byte*)(szTag + 4L / (long)sizeof(sbyte)) = 0;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000069C8 File Offset: 0x00005DC8
	[SecurityCritical]
	internal unsafe static uint TTDirectoryEntryOffset(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szTagName)
	{
		uint num = *(int*)(pInputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO));
		OFFSET_TABLE offset_TABLE;
		ushort num2;
		if (<Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num, &num2) != 0)
		{
			return -1;
		}
		num += num2;
		ushort num3 = 0;
		if (0 < *(ref offset_TABLE + 4))
		{
			DIRECTORY directory;
			while (<Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_NO_XLATE_CONTROL), num, &num2) == 0)
			{
				if (*(int*)szTagName == directory)
				{
					return num;
				}
				num += num2;
				num3 += 1;
				if (num3 >= *(ref offset_TABLE + 4))
				{
					return 0;
				}
			}
			return -1;
		}
		return 0;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00006A34 File Offset: 0x00005E34
	[SecurityCritical]
	internal unsafe static uint GetTTDirectory(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szTagName, DIRECTORY* pDirectory)
	{
		uint num = <Module>.MS.Internal.TtfDelta.TTDirectoryEntryOffset(pInputBufferInfo, szTagName);
		if (num != null && num != -1)
		{
			ushort num2;
			return (<Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pDirectory, 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num2) != 0) ? 0 : num;
		}
		return 0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00006A6C File Offset: 0x00005E6C
	[SecurityCritical]
	internal unsafe static uint TTTableLength(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szTagName)
	{
		DIRECTORY directory;
		return (<Module>.MS.Internal.TtfDelta.GetTTDirectory(pInputBufferInfo, szTagName, &directory) != 0) ? (*(ref directory + 12)) : 0;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00006A90 File Offset: 0x00005E90
	[SecurityCritical]
	internal unsafe static uint TTTableOffset(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szTagName)
	{
		DIRECTORY directory;
		return (<Module>.MS.Internal.TtfDelta.GetTTDirectory(pInputBufferInfo, szTagName, &directory) != 0) ? (*(ref directory + 8)) : 0;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000C8D4 File Offset: 0x0000BCD4
	[SecurityCritical]
	internal unsafe static short UpdateDirEntry(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szDirTag, uint ulNewLength)
	{
		DIRECTORY directory;
		uint num = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pInputBufferInfo, szDirTag, &directory);
		if (num == null)
		{
			return 0;
		}
		*(ref directory + 12) = ulNewLength;
		short num2 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordGap(pInputBufferInfo, *(ref directory + 8), ulNewLength, null);
		if (num2 != 0)
		{
			return num2;
		}
		num2 = <Module>.MS.Internal.TtfDelta.CalcChecksum(pInputBufferInfo, *(ref directory + 8), *(ref directory + 12), ref directory + 4);
		if (num2 != 0)
		{
			return num2;
		}
		ushort num3;
		return <Module>.MS.Internal.TtfDelta.WriteGeneric(pInputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num3);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000C93C File Offset: 0x0000BD3C
	[SecurityCritical]
	internal unsafe static short UpdateDirEntryAll(TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* szDirTag, uint ulNewLength, uint ulNewOffset)
	{
		DIRECTORY directory;
		uint ulOffset = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pInputBufferInfo, szDirTag, &directory);
		*(ref directory + 12) = ulNewLength;
		short num = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pInputBufferInfo, ulNewOffset, ref directory + 8);
		if (num != 0)
		{
			return num;
		}
		num = <Module>.MS.Internal.TtfDelta.CalcChecksum(pInputBufferInfo, *(ref directory + 8), *(ref directory + 12), ref directory + 4);
		if (num != 0)
		{
			return num;
		}
		ushort num2;
		return <Module>.MS.Internal.TtfDelta.WriteGeneric(pInputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), ulOffset, &num2);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00006AB4 File Offset: 0x00005EB4
	[SecurityCritical]
	internal unsafe static uint GetGeneric(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchBuffer, ushort usTagIndex)
	{
		if (usTagIndex >= 10)
		{
			return 0;
		}
		long num = (long)((ulong)usTagIndex * 24UL);
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, *(num + ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table));
		if (num2 == null)
		{
			return 0;
		}
		ushort num3;
		return (<Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, puchBuffer, *(num + (ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 8)), *(num + (ref <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Control_Table + 16)), num2, &num3) != 0) ? 0 : num2;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00006B0C File Offset: 0x00005F0C
	[SecurityCritical]
	internal unsafe static uint GetHHea(TTFACC_FILEBUFFERINFO* pInputBufferInfo, HHEA* pHorizHead)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pHorizHead, 1);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00006B24 File Offset: 0x00005F24
	[SecurityCritical]
	internal unsafe static uint GetVHea(TTFACC_FILEBUFFERINFO* pInputBufferInfo, VHEA* pVertHead)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pVertHead, 2);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00006B3C File Offset: 0x00005F3C
	[SecurityCritical]
	internal unsafe static uint GetHead(TTFACC_FILEBUFFERINFO* pInputBufferInfo, HEAD* pHead)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pHead, 0);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00006B54 File Offset: 0x00005F54
	[SecurityCritical]
	internal unsafe static uint GetOS2(TTFACC_FILEBUFFERINFO* pInputBufferInfo, OS2* pOs2)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pOs2, 5);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00006B6C File Offset: 0x00005F6C
	[SecurityCritical]
	internal unsafe static uint GetNEWOS2(TTFACC_FILEBUFFERINFO* pInputBufferInfo, NEWOS2* pNewOs2)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pNewOs2, 6);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00006B84 File Offset: 0x00005F84
	[SecurityCritical]
	internal unsafe static uint GetSmartOS2(TTFACC_FILEBUFFERINFO* pInputBufferInfo, NEWOS2* pOs2, int* pbNewOS2)
	{
		uint result = 0;
		uint num = <Module>.MS.Internal.TtfDelta.TTTableLength(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04OEKHDCJK@OS?12@));
		if (num > 0)
		{
			if (num == <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.OS2_CONTROL)))
			{
				*pbNewOS2 = 0;
				result = <Module>.MS.Internal.TtfDelta.GetOS2(pInputBufferInfo, (OS2*)pOs2);
			}
			else if (num >= <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.NEWOS2_CONTROL)))
			{
				*pbNewOS2 = 1;
				result = <Module>.MS.Internal.TtfDelta.GetNEWOS2(pInputBufferInfo, pOs2);
			}
		}
		return result;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00006BD8 File Offset: 0x00005FD8
	[SecurityCritical]
	internal unsafe static uint GetMaxp(TTFACC_FILEBUFFERINFO* pInputBufferInfo, MAXP* pMaxp)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)pMaxp, 3);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00006BF0 File Offset: 0x00005FF0
	[SecurityCritical]
	internal unsafe static uint GetPost(TTFACC_FILEBUFFERINFO* pInputBufferInfo, POST* Post)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)Post, 4);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00006C08 File Offset: 0x00006008
	[SecurityCritical]
	internal unsafe static uint GetHdmx(TTFACC_FILEBUFFERINFO* pInputBufferInfo, HDMX* Hdmx)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)Hdmx, 8);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00006C20 File Offset: 0x00006020
	[SecurityCritical]
	internal unsafe static uint GetLTSH(TTFACC_FILEBUFFERINFO* pInputBufferInfo, LTSH* Ltsh)
	{
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGeneric(pInputBufferInfo, (byte*)Ltsh, 9);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00006C38 File Offset: 0x00006038
	[SecurityCritical]
	internal unsafe static ushort GetNumGlyphs(TTFACC_FILEBUFFERINFO* pInputBufferInfo)
	{
		MAXP maxp;
		initblk(ref maxp, 0, 32L);
		return (<Module>.MS.Internal.TtfDelta.GetMaxp(pInputBufferInfo, &maxp) == 0) ? 0 : (*(ref maxp + 4));
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00006C64 File Offset: 0x00006064
	[SecurityCritical]
	internal unsafe static void SetFileChecksum(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulLength)
	{
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04NEODDMOL@head@));
		HEAD head;
		ushort num2;
		if (num != null && <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&head), 54, (byte*)(&<Module>.MS.Internal.TtfDelta.HEAD_CONTROL), num, &num2) == 0)
		{
			*(ref head + 8) = 0;
			uint num3;
			if (<Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&head), 54, (byte*)(&<Module>.MS.Internal.TtfDelta.HEAD_CONTROL), num, &num2) == 0 && <Module>.MS.Internal.TtfDelta.CalcFileChecksum(pOutputBufferInfo, ulLength, &num3) == 0)
			{
				*(ref head + 8) = -1313820742 - num3;
				<Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&head), 54, (byte*)(&<Module>.MS.Internal.TtfDelta.HEAD_CONTROL), num, &num2);
			}
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00006CD8 File Offset: 0x000060D8
	[SecurityCritical]
	internal unsafe static short CopyBlock(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulTarget, uint ulSource, uint ulSize)
	{
		if (ulTarget == ulSource || ulSize == null)
		{
			return 0;
		}
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset(pInputBufferInfo, ulSource, ulSize);
		if (num != 0)
		{
			return num;
		}
		short num2 = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pInputBufferInfo, ulTarget, ulSize);
		if (num2 != 0)
		{
			return num2;
		}
		long num3 = *(long*)pInputBufferInfo;
		<Module>.memmove((void*)((byte*)ulTarget + num3), ulSource + num3 / (long)sizeof(void), ulSize);
		return 0;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00006D20 File Offset: 0x00006120
	[SecurityCritical]
	internal unsafe static short CopyBlockOver(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulTarget, uint ulSource, uint ulSize)
	{
		if (ulTarget + (ulong)(*(long*)pOutputBufferInfo) == ulSource + (ulong)(*(long*)pInputBufferInfo) || ulSize == null)
		{
			return 0;
		}
		short num = <Module>.MS.Internal.TtfDelta.CheckInOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, ulSource, ulSize);
		if (num != 0)
		{
			return num;
		}
		short num2 = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, ulTarget, ulSize);
		if (num2 != 0)
		{
			return num2;
		}
		<Module>.memmove((void*)((byte*)ulTarget + *(long*)pOutputBufferInfo), ulSource + *(long*)pInputBufferInfo / (long)sizeof(void), ulSize);
		return 0;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000C99C File Offset: 0x0000BD9C
	[SecurityCritical]
	internal unsafe static short CopyTableOver(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, sbyte* Tag, uint* pulNewOutOffset)
	{
		DIRECTORY directory;
		uint num = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pOutputBufferInfo, Tag, &directory);
		if (num == null)
		{
			return 1006;
		}
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, Tag);
		uint num3 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, Tag);
		if (num2 == null)
		{
			return 1006;
		}
		uint num5;
		short num4 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, *(int*)pulNewOutOffset, &num5);
		if (num4 != 0)
		{
			return num4;
		}
		*(ref directory + 8) = num5;
		*(ref directory + 12) = num3;
		if (num3 > 0)
		{
			num4 = <Module>.MS.Internal.TtfDelta.CheckInOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, num2, num3);
			if (num4 != 0)
			{
				return num4;
			}
			num4 = <Module>.MS.Internal.TtfDelta.CheckOutOffset(pOutputBufferInfo, num5, num3);
			if (num4 != 0)
			{
				return num4;
			}
			num4 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, num5 + *(long*)pOutputBufferInfo, num2, num3);
			if (num4 != 0)
			{
				return num4;
			}
		}
		ushort num6;
		num4 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num6);
		if (num4 == 0)
		{
			*(int*)pulNewOutOffset = num5 + num3;
		}
		return num4;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00006D74 File Offset: 0x00006174
	[SecurityCritical]
	internal static uint RoundToLongWord(uint ulLength)
	{
		return ulLength + 3 & -4;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00006D88 File Offset: 0x00006188
	[SecurityCritical]
	internal unsafe static ushort ZeroLongWordGap(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, uint ulUnalignedLength, uint* pulNewOffset)
	{
		if (pulNewOffset != null)
		{
			*(int*)pulNewOffset = <Module>.MS.Internal.TtfDelta.RoundToLongWord(ulUnalignedLength) + ulOffset;
		}
		uint num = <Module>.MS.Internal.TtfDelta.RoundToLongWord(ulUnalignedLength) - ulUnalignedLength;
		ushort num2 = 0;
		if (0 < num)
		{
			do
			{
				ushort num3 = (ushort)<Module>.MS.Internal.TtfDelta.WriteByte(pInputBufferInfo, 0, num2 + ulOffset + ulUnalignedLength);
				if (num3 != 0)
				{
					return num3;
				}
				num2 += 1;
			}
			while (num2 < num);
			return 0;
		}
		return 0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00006DD0 File Offset: 0x000061D0
	[SecurityCritical]
	internal unsafe static ushort ZeroLongWordAlign(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, uint* pulNewOffset)
	{
		uint num = <Module>.MS.Internal.TtfDelta.RoundToLongWord(ulOffset);
		*(int*)pulNewOffset = (int)num;
		uint num2 = num - ulOffset;
		ushort num3 = 0;
		if (0 < num2)
		{
			do
			{
				ushort num4 = (ushort)<Module>.MS.Internal.TtfDelta.WriteByte(pInputBufferInfo, 0, num3 + ulOffset);
				if (num4 != 0)
				{
					return num4;
				}
				num3 += 1;
			}
			while (num3 < num2);
			return 0;
		}
		return 0;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00006E10 File Offset: 0x00006210
	[SecurityCritical]
	internal unsafe static int AscendingTagCompare(void* arg1, void* arg2)
	{
		uint num = (uint)(*(int*)arg1);
		uint num2 = (uint)(*(int*)arg2);
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = -1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00006E30 File Offset: 0x00006230
	[SecurityCritical]
	internal unsafe static int AscendingOffsetCompare(void* arg1, void* arg2)
	{
		uint num = (uint)(*(int*)(arg1 + 8L / (long)sizeof(void)));
		uint num2 = (uint)(*(int*)(arg2 + 8L / (long)sizeof(void)));
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = -1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00006E58 File Offset: 0x00006258
	[SecurityCritical]
	internal unsafe static void SortByTag(DIRECTORY* aDirectory, ushort usnDirs)
	{
		if (aDirectory != null && usnDirs != 0)
		{
			<Module>.qsort((void*)aDirectory, (ulong)usnDirs, 16UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingTagCompare));
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00006E7C File Offset: 0x0000627C
	[SecurityCritical]
	internal unsafe static void SortByOffset(DIRECTORY* aDirectory, ushort usnDirs)
	{
		if (aDirectory != null && usnDirs != 0)
		{
			<Module>.qsort((void*)aDirectory, (ulong)usnDirs, 16UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingOffsetCompare));
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00006EA0 File Offset: 0x000062A0
	[SecurityCritical]
	internal unsafe static void MarkTableForDeletion(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, sbyte* szDirTag)
	{
		DIRECTORY directory;
		uint num = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pOutputBufferInfo, szDirTag, &directory);
		if (num != null)
		{
			directory = 16843009;
			ushort num2;
			<Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num2);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00006ED8 File Offset: 0x000062D8
	[SecurityCritical]
	internal unsafe static uint FindCmapSubtable(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usDesiredPlatform, ushort usDesiredEncodingID, ushort* pusFoundEncoding)
	{
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@));
		if (num == null)
		{
			return 0;
		}
		CMAP_HEADER cmap_HEADER;
		ushort num2;
		if (<Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&cmap_HEADER), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_HEADER_CONTROL), num, &num2) != 0)
		{
			return 0;
		}
		int num3 = 0;
		uint num4 = 0;
		uint num5 = num2 + num;
		short num6 = (short)(*(ref cmap_HEADER + 2));
		if (usDesiredPlatform == 3 && usDesiredEncodingID == 65535)
		{
			short num7 = 0;
			if (0 < num6)
			{
				CMAP_TABLELOC cmap_TABLELOC;
				while (<Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&cmap_TABLELOC), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_TABLELOC_CONTROL), num5, &num2) == 0)
				{
					if (cmap_TABLELOC == 3)
					{
						if (*(ref cmap_TABLELOC + 2) == 10)
						{
							num4 = *(ref cmap_TABLELOC + 4);
							*pusFoundEncoding = 10;
							num3 = 1;
						}
						else if (*(ref cmap_TABLELOC + 2) == 1)
						{
							if (num3 == 0 || *pusFoundEncoding != 10)
							{
								num4 = *(ref cmap_TABLELOC + 4);
								*pusFoundEncoding = 1;
								num3 = 1;
							}
						}
						else if (*(ref cmap_TABLELOC + 2) == 0 && num3 == 0)
						{
							num4 = *(ref cmap_TABLELOC + 4);
							*pusFoundEncoding = 0;
							num3 = 1;
						}
					}
					num7 += 1;
					num5 += num2;
					if (num7 >= num6)
					{
						goto IL_11C;
					}
				}
				return 0;
			}
			return 0;
		}
		else
		{
			short num7 = 0;
			if (0 < num6)
			{
				while (num3 == 0)
				{
					CMAP_TABLELOC cmap_TABLELOC;
					if (<Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&cmap_TABLELOC), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_TABLELOC_CONTROL), num5, &num2) != 0)
					{
						return 0;
					}
					if (cmap_TABLELOC == usDesiredPlatform && (*(ref cmap_TABLELOC + 2) == usDesiredEncodingID || usDesiredEncodingID == 65535))
					{
						num4 = *(ref cmap_TABLELOC + 4);
						num3 = 1;
						*pusFoundEncoding = *(ref cmap_TABLELOC + 2);
					}
					num7 += 1;
					num5 += num2;
					if (num7 >= num6)
					{
						goto IL_11C;
					}
				}
				goto IL_123;
			}
			return 0;
		}
		IL_11C:
		if (num3 == 0)
		{
			return 0;
		}
		IL_123:
		return num4 + num;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00007010 File Offset: 0x00006410
	[SecurityCritical]
	internal unsafe static ushort GuessNumCmapGlyphIds(ushort usnSegments, FORMAT4_SEGMENTS* Format4Segments)
	{
		ushort num = 0;
		ushort num2 = 0;
		if (0 < usnSegments)
		{
			do
			{
				FORMAT4_SEGMENTS* ptr = num2 * 8L + Format4Segments / sizeof(FORMAT4_SEGMENTS);
				ushort num3 = *(ushort*)(ptr + 6L / (long)sizeof(FORMAT4_SEGMENTS));
				if (num3 != 0)
				{
					ushort num4 = *(ushort*)(ptr + 2L / (long)sizeof(FORMAT4_SEGMENTS));
					ushort num5 = num4;
					ushort num6 = *(ushort*)ptr;
					ushort num7 = num6;
					if (num5 <= num7)
					{
						while (num6 != 65535)
						{
							int num8 = ((uint)num3 >> 1) - (uint)num4 - (uint)usnSegments + (uint)num5 + (uint)num2 + 1U;
							ushort num9 = (num > num8) ? num : num8;
							num = num9;
							num5 += 1;
							if (num5 > num7)
							{
								break;
							}
						}
					}
				}
				num2 += 1;
			}
			while (num2 < usnSegments);
		}
		return num;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000708C File Offset: 0x0000648C
	[SecurityCritical]
	internal unsafe static uint GetLoca(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint* pulLoca, uint ulAllocedCount)
	{
		HEAD head;
		if (<Module>.MS.Internal.TtfDelta.GetHead(pInputBufferInfo, &head) == null)
		{
			return 0;
		}
		ushort num = (ushort)(*(ref head + 50));
		uint num2 = <Module>.MS.Internal.TtfDelta.GetNumGlyphs(pInputBufferInfo);
		if (ulAllocedCount < num2 + 1)
		{
			return 0;
		}
		uint num3 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@));
		if (num3 == null)
		{
			return 0;
		}
		if (num == 0)
		{
			uint num4 = 0;
			ulong num5 = num3;
			uint* ptr = pulLoca;
			ushort num6;
			while (<Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num6, (uint)(num4 * 2UL + num5)) == 0)
			{
				*(int*)ptr = (int)(num6 * 2);
				num4++;
				ptr += 4L / (long)sizeof(uint);
				if (num4 > num2)
				{
					return num3;
				}
			}
			return 0;
		}
		uint num7;
		if (<Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)pulLoca, (byte*)(&<Module>.MS.Internal.TtfDelta.LONG_CONTROL), num3, &num7, num2 + 1, 4) != 0)
		{
			return 0;
		}
		return num3;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00007120 File Offset: 0x00006520
	[SecurityCritical]
	internal unsafe static int CompareSegments(void* elem1, void* elem2)
	{
		if (*(ushort*)elem1 <= *(ushort*)elem2 && *(ushort*)(elem1 + 2L / (long)sizeof(void)) >= *(ushort*)(elem2 + 2L / (long)sizeof(void)))
		{
			return 0;
		}
		int result;
		int num = result = -1;
		if (*(ushort*)(elem1 + 2L / (long)sizeof(void)) >= *(ushort*)(elem2 + 2L / (long)sizeof(void)))
		{
			result = -num;
		}
		return result;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00007150 File Offset: 0x00006550
	[SecurityCritical]
	internal unsafe static ushort GetGlyphIdx(ushort usCharCode, FORMAT4_SEGMENTS* Format4Segments, ushort usnSegments, ushort* GlyphId, ushort usnGlyphs)
	{
		FORMAT4_SEGMENTS format4_SEGMENTS;
		*(ref format4_SEGMENTS + 2) = (short)usCharCode;
		format4_SEGMENTS = usCharCode;
		FORMAT4_SEGMENTS* ptr = (FORMAT4_SEGMENTS*)<Module>.bsearch((void*)(&format4_SEGMENTS), (void*)Format4Segments, (ulong)usnSegments, 8UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.CompareSegments));
		if (ptr == null)
		{
			return ushort.MaxValue;
		}
		ushort num = *(ushort*)(ptr + 6L / (long)sizeof(FORMAT4_SEGMENTS));
		ushort num2;
		if (num == 0)
		{
			num2 = (ushort)(*(short*)(ptr + 4L / (long)sizeof(FORMAT4_SEGMENTS)) + (short)usCharCode);
		}
		else
		{
			int num3 = (int)(ptr - (ulong)usnSegments * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) - Format4Segments >> 3) - (int)(*(ushort*)(ptr + 2L / (long)sizeof(FORMAT4_SEGMENTS))) + (int)((uint)num >> 1) + (int)usCharCode;
			if (num3 >= usnGlyphs)
			{
				return ushort.MaxValue;
			}
			num2 = (num3 * 2L)[GlyphId / 2];
			if (num2 != 0)
			{
				num2 = (ushort)(*(short*)(ptr + 4L / (long)sizeof(FORMAT4_SEGMENTS)) + (short)num2);
			}
		}
		return num2;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000071D0 File Offset: 0x000065D0
	[SecurityCritical]
	internal unsafe static uint GetGlyphIdx12(uint ulCharCode, FORMAT12_GROUPS* pFormat12Groups, uint ulnGroups)
	{
		uint result = -1;
		if (0 < ulnGroups)
		{
			FORMAT12_GROUPS* ptr = pFormat12Groups;
			uint num = ulnGroups;
			do
			{
				uint num2 = (uint)(*(int*)ptr);
				if (num2 <= ulCharCode && *(int*)(ptr + 8L / (long)sizeof(FORMAT12_GROUPS) - 4L / (long)sizeof(FORMAT12_GROUPS)) >= ulCharCode)
				{
					result = *(int*)(ptr + 8L / (long)sizeof(FORMAT12_GROUPS)) - (int)num2 + ulCharCode;
				}
				ptr += 12L / (long)sizeof(FORMAT12_GROUPS);
				num += -1;
			}
			while (num > 0);
		}
		return result;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeCmapFormat4Ids(ushort* GlyphId)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)GlyphId);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeCmapFormat4Segs(FORMAT4_SEGMENTS* Format4Segments)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)Format4Segments);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00007228 File Offset: 0x00006628
	[SecurityCritical]
	internal unsafe static void FreeCmapFormat4(FORMAT4_SEGMENTS* Format4Segments, ushort* GlyphId)
	{
		<Module>.MS.Internal.TtfDelta.FreeCmapFormat4Segs(Format4Segments);
		<Module>.MS.Internal.TtfDelta.FreeCmapFormat4Ids(GlyphId);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00007244 File Offset: 0x00006644
	[SecurityCritical]
	internal unsafe static short ReadAllocCmapFormat4Ids(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usSegCount, FORMAT4_SEGMENTS* Format4Segments, ushort** ppGlyphId, ushort* pusnIds, uint ulOffset, uint* pulBytesRead)
	{
		*(long*)ppGlyphId = 0L;
		ushort num = 0;
		if (0 < usSegCount)
		{
			do
			{
				FORMAT4_SEGMENTS* ptr = num * 8L + Format4Segments / sizeof(FORMAT4_SEGMENTS);
				if (*(ushort*)ptr < *(ushort*)(ptr + 2L / (long)sizeof(FORMAT4_SEGMENTS)))
				{
					return 1060;
				}
				num += 1;
			}
			while (num < usSegCount);
		}
		ushort num2 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GuessNumCmapGlyphIds(usSegCount, Format4Segments);
		*pusnIds = num2;
		if (num2 == 0)
		{
			return 0;
		}
		void* ptr2 = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num2 * 2UL);
		*(long*)ppGlyphId = ptr2;
		if (ptr2 == null)
		{
			return 1005;
		}
		short num3 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr2, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), ulOffset, pulBytesRead, *pusnIds, 2);
		if (num3 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)ppGlyphId);
			*(long*)ppGlyphId = 0L;
			return num3;
		}
		return 0;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000072D0 File Offset: 0x000066D0
	[SecurityCritical]
	internal unsafe static short ReadAllocCmapFormat4Segs(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usSegCount, FORMAT4_SEGMENTS** Format4Segments, uint ulOffset, uint* pulBytesRead)
	{
		void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(usSegCount * 8)));
		*(long*)Format4Segments = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		if (usSegCount * 8 + ulOffset + 2 < ulOffset)
		{
			return 1001;
		}
		ushort num = 0;
		short num2;
		if (0 < usSegCount)
		{
			do
			{
				num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, num * 8L + *(long*)Format4Segments / 2L, num * 2 + ulOffset);
				if (num2 != 0)
				{
					goto IL_50;
				}
				num += 1;
			}
			while (num < usSegCount);
			goto IL_5D;
			IL_50:
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)Format4Segments);
			*(long*)Format4Segments = 0L;
			return num2;
		}
		IL_5D:
		uint num3 = (uint)(usSegCount * 2);
		uint num4 = num3 + ulOffset;
		ushort num5;
		num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num5, num4);
		if (num2 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)Format4Segments);
			*(long*)Format4Segments = 0L;
			return num2;
		}
		num4 += 2;
		num = 0;
		if (0 < usSegCount)
		{
			do
			{
				num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, *(long*)Format4Segments / 2L + num * 8L + 2L / 2L, num * 2 + num4);
				if (num2 != 0)
				{
					goto IL_AD;
				}
				num += 1;
			}
			while (num < usSegCount);
			goto IL_BA;
			IL_AD:
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)Format4Segments);
			*(long*)Format4Segments = 0L;
			return num2;
		}
		IL_BA:
		num4 += num3;
		num = 0;
		if (0 < usSegCount)
		{
			do
			{
				num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, *(long*)Format4Segments / 2L + num * 8L + 4L / 2L, num * 2 + num4);
				if (num2 != 0)
				{
					goto IL_E8;
				}
				num += 1;
			}
			while (num < usSegCount);
			goto IL_F5;
			IL_E8:
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)Format4Segments);
			*(long*)Format4Segments = 0L;
			return num2;
		}
		IL_F5:
		num4 += num3;
		num = 0;
		if (0 < usSegCount)
		{
			do
			{
				num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, *(long*)Format4Segments / 2L + num * 8L + 6L / 2L, num * 2 + num4);
				if (num2 != 0)
				{
					goto IL_123;
				}
				num += 1;
			}
			while (num < usSegCount);
			goto IL_130;
			IL_123:
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)Format4Segments);
			*(long*)Format4Segments = 0L;
			return num2;
		}
		IL_130:
		*(int*)pulBytesRead = num3 - ulOffset + num4;
		return 0;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00007418 File Offset: 0x00006818
	[SecurityCritical]
	internal unsafe static short ReadCmapLength(TTFACC_FILEBUFFERINFO* pInputBufferInfo, CMAP_SUBHEADER_GEN* pCmapSubHeader, uint ulStartOffset, ushort* pusBytesRead)
	{
		short num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, (ushort*)pCmapSubHeader, ulStartOffset);
		if (num != 0)
		{
			return num;
		}
		uint num2 = (uint)(ulStartOffset + 2UL);
		switch (*(ushort*)pCmapSubHeader)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		{
			ushort num3;
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num3, num2);
			if (num != 0)
			{
				return num;
			}
			*(int*)(pCmapSubHeader + 4L / (long)sizeof(CMAP_SUBHEADER_GEN)) = (int)num3;
			num2 = (uint)((ulong)((uint)(num2 + 2UL)) + 2UL);
			goto IL_B6;
		}
		case 14:
			num = <Module>.MS.Internal.TtfDelta.ReadLong(pInputBufferInfo, (uint*)(pCmapSubHeader + 4L / (long)sizeof(CMAP_SUBHEADER_GEN)), num2);
			if (num != 0)
			{
				return num;
			}
			num2 = (uint)(num2 + 4UL);
			goto IL_B6;
		}
		num2 = (uint)(num2 + 2UL);
		num = <Module>.MS.Internal.TtfDelta.ReadLong(pInputBufferInfo, (uint*)(pCmapSubHeader + 4L / (long)sizeof(CMAP_SUBHEADER_GEN)), num2);
		if (num != 0)
		{
			return num;
		}
		num2 = (uint)(num2 + 4UL);
		IL_B6:
		if (pusBytesRead != null)
		{
			*pusBytesRead = num2 - ulStartOffset;
		}
		return 0;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000074E4 File Offset: 0x000068E4
	[SecurityCritical]
	internal unsafe static short ReadAllocCmapFormat4(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usPlatform, ushort usEncoding, ushort* pusFoundEncoding, CMAP_FORMAT4* pCmapFormat4, FORMAT4_SEGMENTS** ppFormat4Segments, ushort** ppGlyphId, ushort* pusnIds)
	{
		*(long*)ppFormat4Segments = 0L;
		*(long*)ppGlyphId = 0L;
		*pusnIds = 0;
		uint num = <Module>.MS.Internal.TtfDelta.FindCmapSubtable(pInputBufferInfo, usPlatform, usEncoding, pusFoundEncoding);
		if (num == null)
		{
			return 1006;
		}
		CMAP_SUBHEADER_GEN cmap_SUBHEADER_GEN;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadCmapLength(pInputBufferInfo, &cmap_SUBHEADER_GEN, num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		if (cmap_SUBHEADER_GEN != 4)
		{
			return 1006;
		}
		num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pCmapFormat4, 14, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT4_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		ushort usSegCount = (ushort)((uint)(*(ushort*)(pCmapFormat4 + 6L / (long)sizeof(CMAP_FORMAT4))) >> 1);
		num += num3;
		uint num4;
		num2 = <Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat4Segs(pInputBufferInfo, usSegCount, ppFormat4Segments, num, &num4);
		if (num2 != 0)
		{
			return num2;
		}
		if (num4 == null)
		{
			return 0;
		}
		num += num4;
		num2 = <Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat4Ids(pInputBufferInfo, usSegCount, *(long*)ppFormat4Segments, ppGlyphId, pusnIds, num, &num4);
		if (num2 != 0)
		{
			<Module>.MS.Internal.TtfDelta.FreeCmapFormat4(*(long*)ppFormat4Segments, *(long*)ppGlyphId);
			*(long*)ppFormat4Segments = 0L;
			*(long*)ppGlyphId = 0L;
			*pusnIds = 0;
			return num2;
		}
		return 0;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeCmapFormat6(ushort* glyphIndexArray)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)glyphIndexArray);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000075A4 File Offset: 0x000069A4
	[SecurityCritical]
	internal unsafe static short ReadAllocCmapFormat6(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usPlatform, ushort usEncoding, ushort* pusFoundEncoding, CMAP_FORMAT6* pCmap, ushort** glyphIndexArray)
	{
		uint num = <Module>.MS.Internal.TtfDelta.FindCmapSubtable(pInputBufferInfo, usPlatform, usEncoding, pusFoundEncoding);
		if (num == null)
		{
			return 1006;
		}
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pCmap, 10, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT6_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		if (*(ushort*)pCmap != 6)
		{
			return 1006;
		}
		void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ushort*)(pCmap + 8L / (long)sizeof(CMAP_FORMAT6))) * 2UL);
		*(long*)glyphIndexArray = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		uint num4;
		num2 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num, &num4, *(ushort*)(pCmap + 8L / (long)sizeof(CMAP_FORMAT6)), 2);
		if (num2 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)glyphIndexArray);
			*(long*)glyphIndexArray = 0L;
			return num2;
		}
		return 0;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00007634 File Offset: 0x00006A34
	[SecurityCritical]
	internal unsafe static short ReadCmapFormat0(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usPlatform, ushort usEncoding, ushort* pusFoundEncoding, CMAP_FORMAT0* pCmap)
	{
		uint num = <Module>.MS.Internal.TtfDelta.FindCmapSubtable(pInputBufferInfo, usPlatform, usEncoding, pusFoundEncoding);
		if (num == null)
		{
			return 1006;
		}
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pCmap, 6, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT0_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		if (*(ushort*)pCmap != 0)
		{
			return 1006;
		}
		uint num4;
		return <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)(pCmap + 6L / (long)sizeof(CMAP_FORMAT0)), (byte*)(&<Module>.MS.Internal.TtfDelta.BYTE_CONTROL), num3 + num, &num4, 256, 1);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00007694 File Offset: 0x00006A94
	[SecurityCritical]
	internal unsafe static short ReadAllocCmapFormat12(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulSubOffset, CMAP_FORMAT12* pCmapFormat12, FORMAT12_GROUPS** ppFormat12Groups)
	{
		*(long*)ppFormat12Groups = 0L;
		ushort num2;
		short num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pCmapFormat12, 16, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT12_CONTROL), ulSubOffset, &num2);
		if (num != 0)
		{
			return num;
		}
		uint num3 = num2 + ulSubOffset;
		uint num4 = *(int*)(pCmapFormat12 + 12L / (long)sizeof(CMAP_FORMAT12));
		if (num4 > 357913941)
		{
			return 1005;
		}
		void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc(num4 * 12);
		*(long*)ppFormat12Groups = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		uint num5 = 0;
		if (0 < num4)
		{
			do
			{
				num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, num5 * 12L + *(long*)ppFormat12Groups, 12, (byte*)(&<Module>.MS.Internal.TtfDelta.FORMAT12_GROUPS_CONTROL), num3, &num2);
				if (num != 0)
				{
					goto IL_81;
				}
				num3 += num2;
				num5++;
			}
			while (num5 < num4);
			return 0;
			IL_81:
			<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)ppFormat12Groups);
			*(long*)ppFormat12Groups = 0L;
			return num;
		}
		return 0;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeCmapFormat12Groups(FORMAT12_GROUPS* pFormat12Groups)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)pFormat12Groups);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00007730 File Offset: 0x00006B30
	[SecurityCritical]
	internal unsafe static short GetGlyphHeader(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort GlyfIdx, ushort usIdxToLocFmt, uint ulLocaOffset, uint ulGlyfOffset, GLYF_HEADER* pGlyfHeader, uint* pulOffset, ushort* pusLength)
	{
		uint num4;
		uint num5;
		if (usIdxToLocFmt == 0)
		{
			ulLocaOffset = (uint)((ulong)GlyfIdx * 2UL + ulLocaOffset);
			ushort num2;
			short num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num2, ulLocaOffset);
			if (num != 0)
			{
				return num;
			}
			ushort num3;
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num3, (uint)(ulLocaOffset + 2UL));
			if (num != 0)
			{
				return num;
			}
			num4 = num2 * 2;
			num5 = num3 * 2;
		}
		else
		{
			ulLocaOffset = (uint)((ulong)GlyfIdx * 4UL + ulLocaOffset);
			short num = <Module>.MS.Internal.TtfDelta.ReadLong(pInputBufferInfo, &num4, ulLocaOffset);
			if (num != 0)
			{
				return num;
			}
			num = <Module>.MS.Internal.TtfDelta.ReadLong(pInputBufferInfo, &num5, (uint)(ulLocaOffset + 4UL));
			if (num != 0)
			{
				return num;
			}
		}
		uint num6 = num5 - num4;
		*pusLength = num6;
		if (num6 == null)
		{
			initblk(pGlyfHeader, 0, 10L);
			*(int*)pulOffset = ulGlyfOffset;
			return 0;
		}
		uint num7 = num4 + ulGlyfOffset;
		*(int*)pulOffset = num7;
		ushort num8;
		return <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)pGlyfHeader, 10, (byte*)(&<Module>.MS.Internal.TtfDelta.GLYF_HEADER_CONTROL), num7, &num8);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000077E4 File Offset: 0x00006BE4
	[SecurityCritical]
	internal unsafe static short GetComponentGlyphList(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usCompositeGlyphIdx, ushort* pusnGlyphs, ushort* ausGlyphIdxs, ushort cMaxGlyphs, ushort* pusnComponentDepth, ushort usLevelValue, ushort usIdxToLocFmt, uint ulLocaOffset, uint ulGlyfOffset)
	{
		*pusnGlyphs = 0;
		GLYF_HEADER glyf_HEADER;
		uint num2;
		ushort num3;
		short num = <Module>.MS.Internal.TtfDelta.GetGlyphHeader(pInputBufferInfo, usCompositeGlyphIdx, usIdxToLocFmt, ulLocaOffset, ulGlyfOffset, &glyf_HEADER, &num2, &num3);
		if (num != 0)
		{
			return num;
		}
		if (*pusnComponentDepth < usLevelValue)
		{
			*pusnComponentDepth = usLevelValue;
		}
		if (glyf_HEADER >= 0)
		{
			return 0;
		}
		uint num4 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.GLYF_HEADER_CONTROL)) + num2;
		while (*pusnGlyphs < cMaxGlyphs)
		{
			ushort num5;
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num5, num4);
			if (num != 0)
			{
				return num;
			}
			num4 = (uint)(num4 + 2UL);
			ushort num6;
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num6, num4);
			if (num != 0)
			{
				return num;
			}
			num4 = (uint)(num4 + 2UL);
			(*pusnGlyphs * 2L)[ausGlyphIdxs / 2] = num6;
			ushort num7 = *pusnGlyphs + 1;
			*pusnGlyphs = num7;
			if ((num5 & 1) != 0)
			{
				num4 = (uint)(num4 + 4UL);
			}
			else
			{
				num4 = (uint)(num4 + 2UL);
			}
			if ((num5 & 8) != 0)
			{
				num4 = (uint)(num4 + 2UL);
			}
			else if ((num5 & 64) != 0)
			{
				num4 = (uint)(num4 + 4UL);
			}
			else if ((num5 & 128) != 0)
			{
				num4 = (uint)(num4 + 8UL);
			}
			ushort num8;
			num = <Module>.MS.Internal.TtfDelta.GetComponentGlyphList(pInputBufferInfo, num6, &num8, num7 * 2L + ausGlyphIdxs / 2, cMaxGlyphs - num7, pusnComponentDepth, usLevelValue + 1, usIdxToLocFmt, ulLocaOffset, ulGlyfOffset);
			if (num != 0)
			{
				return num;
			}
			if (num8 > 0)
			{
				*pusnGlyphs += num8;
			}
			if ((num5 & 32) == 0)
			{
				return 0;
			}
		}
		return 1066;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000790C File Offset: 0x00006D0C
	[SecurityCritical]
	internal unsafe static int AscendingCodeCompare(void* arg1, void* arg2)
	{
		ushort num = *(ushort*)arg1;
		ushort num2 = *(ushort*)arg2;
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = -1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000792C File Offset: 0x00006D2C
	[SecurityCritical]
	internal unsafe static void SortCodeList(Char_Glyph_Map_List* pCharGlyphMapList, ushort* pusnCharMapListLength)
	{
		if (pCharGlyphMapList != null)
		{
			ushort num = *pusnCharMapListLength;
			if (num != 0)
			{
				<Module>.qsort((void*)pCharGlyphMapList, (ulong)num, 4UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingCodeCompare));
				ushort num2 = 0;
				ushort num3 = 1;
				if (1 < *pusnCharMapListLength)
				{
					do
					{
						Char_Glyph_Map_List* ptr = num3 * 4L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List);
						if ((num2 * 4L)[pCharGlyphMapList / 2] != *(ushort*)ptr)
						{
							int num4 = (int)(num2 + 1);
							if ((int)num3 > num4)
							{
								cpblk((long)num4 * 4L / (long)sizeof(Char_Glyph_Map_List) + pCharGlyphMapList, ptr, 4);
							}
							num2 += 1;
						}
						num3 += 1;
					}
					while (num3 < *pusnCharMapListLength);
				}
				*pusnCharMapListLength = num2 + 1;
			}
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00006E10 File Offset: 0x00006210
	[SecurityCritical]
	internal unsafe static int AscendingCodeCompareEx(void* arg1, void* arg2)
	{
		uint num = (uint)(*(int*)arg1);
		uint num2 = (uint)(*(int*)arg2);
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = -1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000799C File Offset: 0x00006D9C
	[SecurityCritical]
	internal unsafe static void SortCodeListEx(Char_Glyph_Map_List_Ex* pCharGlyphMapList, uint* pulnCharMapListLength)
	{
		if (pCharGlyphMapList != null)
		{
			uint num = (uint)(*(int*)pulnCharMapListLength);
			if (num != 0U)
			{
				<Module>.qsort((void*)pCharGlyphMapList, (ulong)num, 8UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingCodeCompareEx));
				uint num2 = 0;
				uint num3 = 1;
				if (1 < *(int*)pulnCharMapListLength)
				{
					uint num4 = 1U;
					do
					{
						Char_Glyph_Map_List_Ex* ptr = num3 * 8L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List_Ex);
						if ((num2 * 8L)[pCharGlyphMapList / 4] != *(int*)ptr)
						{
							if (num3 > num4)
							{
								cpblk((ulong)num4 * 8UL / (ulong)sizeof(Char_Glyph_Map_List_Ex) + pCharGlyphMapList, ptr, 8);
							}
							num2++;
							num4 += 1U;
						}
						num3++;
					}
					while (num3 < *(int*)pulnCharMapListLength);
				}
				*(int*)pulnCharMapListLength = num2 + 1;
			}
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeFormat4CharCodes(Char_Glyph_Map_List* pusCharCodeList)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)pusCharCodeList);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00007A0C File Offset: 0x00006E0C
	[SecurityCritical]
	internal unsafe static short ReadAllocFormat4CharGlyphMapList(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usPlatform, ushort usEncoding, byte* puchKeepGlyphList, ushort usGlyphCount, Char_Glyph_Map_List** ppCharGlyphMapList, ushort* pusnCharGlyphMapListCount)
	{
		*(long*)ppCharGlyphMapList = 0L;
		*pusnCharGlyphMapListCount = 0;
		ushort num2;
		CMAP_FORMAT4 cmap_FORMAT;
		FORMAT4_SEGMENTS* ptr;
		ushort* ptr2;
		ushort num3;
		short num = <Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat4(pInputBufferInfo, usPlatform, usEncoding, &num2, &cmap_FORMAT, &ptr, &ptr2, &num3);
		if (num != 0)
		{
			return num;
		}
		ushort num4 = (ushort)((uint)(*(ref cmap_FORMAT + 6)) >> 1);
		ushort num5 = 0;
		ushort num6 = 0;
		if (0 < num4)
		{
			do
			{
				FORMAT4_SEGMENTS* ptr3 = num6 * 8L + ptr / sizeof(FORMAT4_SEGMENTS);
				ushort num7 = *(ushort*)ptr3;
				if (num7 != 65535)
				{
					ushort num8 = *(ushort*)(ptr3 + 2L / (long)sizeof(FORMAT4_SEGMENTS));
					if (num7 >= num8)
					{
						num5 = num7 - num8 + num5 + 1;
					}
				}
				num6 += 1;
			}
			while (num6 < num4);
		}
		void* ptr4 = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num5 * 4UL);
		*(long*)ppCharGlyphMapList = ptr4;
		if (ptr4 == null)
		{
			<Module>.MS.Internal.TtfDelta.FreeCmapFormat4(ptr, ptr2);
			return 1005;
		}
		*pusnCharGlyphMapListCount = num5;
		ushort num9 = 0;
		num6 = 0;
		if (0 < num4)
		{
			do
			{
				long num10 = (long)((ulong)num6 * 8UL);
				FORMAT4_SEGMENTS* ptr5 = num10 / (long)sizeof(FORMAT4_SEGMENTS) + ptr;
				ushort num11 = *(ushort*)ptr5;
				if (num11 != 65535)
				{
					ushort num12 = *(ushort*)(ptr5 + 2L / (long)sizeof(FORMAT4_SEGMENTS));
					if (num11 >= num12)
					{
						ushort num13 = num12;
						if (num13 <= num11)
						{
							for (;;)
							{
								ushort num14 = *(ushort*)(ptr5 + 6L / (long)sizeof(FORMAT4_SEGMENTS));
								ushort num15;
								if (num14 == 0)
								{
									num15 = (ushort)(*(short*)(ptr5 + 4L / (long)sizeof(FORMAT4_SEGMENTS)) + (short)num13);
									goto IL_125;
								}
								int num16 = ((uint)num14 >> 1) - (uint)(*(ushort*)(ptr5 + 2L / (long)sizeof(FORMAT4_SEGMENTS))) - (uint)num4 + (uint)num13 + (uint)num6;
								if (num16 < num3)
								{
									num15 = (num16 * 2L)[ptr2 / 2];
									if (num15 != 0)
									{
										num15 = (ushort)(*(short*)(ptr5 + 4L / (long)sizeof(FORMAT4_SEGMENTS)) + (short)num15);
										goto IL_125;
									}
								}
								IL_15D:
								num13 += 1;
								ptr5 = num10 / (long)sizeof(FORMAT4_SEGMENTS) + ptr;
								if (num13 > *(ushort*)ptr5)
								{
									break;
								}
								continue;
								IL_125:
								if (num15 != 0 && num15 != 65535 && num15 < usGlyphCount && num15[puchKeepGlyphList] != 0)
								{
									long num17 = (long)((ulong)num9 * 4UL);
									*(num17 + *(long*)ppCharGlyphMapList) = (short)num13;
									*(*(long*)ppCharGlyphMapList + num17 + 2L) = (short)num15;
									num9 += 1;
									goto IL_15D;
								}
								goto IL_15D;
							}
						}
					}
				}
				num6 += 1;
			}
			while (num6 < num4);
		}
		*pusnCharGlyphMapListCount = num9;
		<Module>.MS.Internal.TtfDelta.FreeCmapFormat4(ptr, ptr2);
		<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.SortCodeList(*(long*)ppCharGlyphMapList, pusnCharGlyphMapListCount);
		return 0;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00007214 File Offset: 0x00006614
	[SecurityCritical]
	internal unsafe static void FreeFormat12CharCodes(Char_Glyph_Map_List_Ex* pulCharCodeList)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)pulCharCodeList);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00007BB0 File Offset: 0x00006FB0
	[SecurityCritical]
	internal unsafe static short ReadAllocFormat12CharGlyphMapList(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, byte* puchKeepGlyphList, ushort usGlyphCount, Char_Glyph_Map_List_Ex** ppCharGlyphMapList, uint* pulnCharGlyphMapListCount)
	{
		*(long*)ppCharGlyphMapList = 0L;
		*(int*)pulnCharGlyphMapListCount = 0;
		CMAP_FORMAT12 cmap_FORMAT;
		FORMAT12_GROUPS* ptr;
		short num = <Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat12(pInputBufferInfo, ulOffset, &cmap_FORMAT, &ptr);
		if (num != 0)
		{
			return num;
		}
		uint num2 = *(ref cmap_FORMAT + 12);
		uint num3 = 0;
		if (0 < *(ref cmap_FORMAT + 12))
		{
			FORMAT12_GROUPS* ptr2 = ptr;
			uint num4 = (uint)(*(ref cmap_FORMAT + 12));
			do
			{
				uint num5 = (uint)(*(int*)(ptr2 + 4L / (long)sizeof(FORMAT12_GROUPS)));
				uint num6 = (uint)(*(int*)ptr2);
				if (num5 >= num6)
				{
					num3 += num5 - num6 + 1U;
				}
				ptr2 += 12L / (long)sizeof(FORMAT12_GROUPS);
				num4 += uint.MaxValue;
			}
			while (num4 > 0U);
		}
		void* ptr3 = <Module>.MS.Internal.TtfDelta.Mem_Alloc(num3 * 8UL);
		*(long*)ppCharGlyphMapList = ptr3;
		if (ptr3 == null)
		{
			<Module>.MS.Internal.TtfDelta.FreeCmapFormat12Groups(ptr);
			return 1005;
		}
		*(int*)pulnCharGlyphMapListCount = num3;
		uint num7 = 0;
		if (0 < num2)
		{
			long num8 = 0L;
			uint num9 = num2;
			do
			{
				FORMAT12_GROUPS* ptr4 = num8 / (long)sizeof(FORMAT12_GROUPS) + ptr;
				uint num10 = (uint)(*(int*)(ptr4 + 4L / (long)sizeof(FORMAT12_GROUPS)));
				uint num11 = (uint)(*(int*)ptr4);
				if (num10 >= num11)
				{
					uint num12 = num11;
					if (num12 <= num10)
					{
						do
						{
							uint num13 = *(int*)(ptr4 + 8L / (long)sizeof(FORMAT12_GROUPS)) - *(int*)ptr4 + num12;
							ushort num14 = num13;
							if (num14 != 0 && num13 < usGlyphCount && num14[puchKeepGlyphList] != 0)
							{
								long num15 = (long)(num7 * 8UL);
								*(*(long*)ppCharGlyphMapList + num15) = num12;
								*(num15 + *(long*)ppCharGlyphMapList + 4L) = num13;
								num7++;
							}
							num12++;
							ptr4 = num8 / (long)sizeof(FORMAT12_GROUPS) + ptr;
						}
						while (num12 <= *(int*)(ptr4 + 4L / (long)sizeof(FORMAT12_GROUPS)));
					}
				}
				num8 += 12L;
				num9 += -1;
			}
			while (num9 > 0);
		}
		*(int*)pulnCharGlyphMapListCount = num7;
		<Module>.MS.Internal.TtfDelta.FreeCmapFormat12Groups(ptr);
		<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.SortCodeListEx(*(long*)ppCharGlyphMapList, pulnCharGlyphMapListCount);
		return 0;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00007D00 File Offset: 0x00007100
	[SecurityCritical]
	internal unsafe static uint Format4CmapLength(ushort usnSegments, ushort usnGlyphIdxs)
	{
		int num = (int)(<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.FORMAT4_SEGMENTS_CONTROL)) * usnSegments);
		return (uint)((long)((int)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT4_CONTROL)) + num) + (long)(((ulong)usnGlyphIdxs + 1UL) * 2UL));
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000CA44 File Offset: 0x0000BE44
	[SecurityCritical]
	internal unsafe static void ComputeFormat4CmapData(CMAP_FORMAT4* pCmapFormat4, FORMAT4_SEGMENTS* NewFormat4Segments, ushort* pusnSegment, ushort* NewFormat4GlyphIdArray, ushort* psnFormat4GlyphIdArray, Char_Glyph_Map_List* pCharGlyphMapList, ushort usnCharGlyphMapListCount)
	{
		ushort num = 0;
		*pusnSegment = 0;
		*psnFormat4GlyphIdArray = 0;
		if (0 < usnCharGlyphMapListCount)
		{
			int num2 = (int)(usnCharGlyphMapListCount - 1);
			do
			{
				ushort num3 = num;
				if ((int)num < num2)
				{
					while ((num * 4L)[pCharGlyphMapList / 2] + 1 == *(ushort*)((long)(num + 1) * 4L + pCharGlyphMapList / 2))
					{
						num += 1;
						if ((int)num >= num2)
						{
							break;
						}
					}
				}
				ushort num4 = num;
				num += 1;
				int num5 = 1;
				ushort num6 = num3;
				if (num3 < num4)
				{
					while (num5 != 0)
					{
						num5 = ((*(ushort*)(pCharGlyphMapList + (ulong)num6 * 4UL / (ulong)sizeof(Char_Glyph_Map_List) + 2L / (long)sizeof(Char_Glyph_Map_List)) + 1 != *(ushort*)(pCharGlyphMapList + (long)(num6 + 1) * 4L / (long)sizeof(Char_Glyph_Map_List) + 2L / (long)sizeof(Char_Glyph_Map_List))) ? 0 : num5);
						num6 += 1;
						if (num6 >= num4)
						{
							break;
						}
					}
				}
				Char_Glyph_Map_List* ptr = num3 * 4L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List);
				*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 2L / (long)sizeof(FORMAT4_SEGMENTS)) = (short)(*(ushort*)ptr);
				(*pusnSegment * 8L)[NewFormat4Segments / 2] = (short)(num4 * 4L)[pCharGlyphMapList / 2];
				if (num5 != 0)
				{
					*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 4L / (long)sizeof(FORMAT4_SEGMENTS)) = (short)(*(ushort*)(ptr + 2L / (long)sizeof(Char_Glyph_Map_List)) - *(ushort*)ptr);
					*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 6L / (long)sizeof(FORMAT4_SEGMENTS)) = 0;
				}
				else
				{
					*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 4L / (long)sizeof(FORMAT4_SEGMENTS)) = 0;
					*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 6L / (long)sizeof(FORMAT4_SEGMENTS)) = 1;
				}
				*pusnSegment += 1;
			}
			while (num < usnCharGlyphMapListCount);
		}
		ushort num7 = 0;
		ushort num8 = 0;
		if (0 < *pusnSegment)
		{
			do
			{
				FORMAT4_SEGMENTS* ptr2 = num8 * 8L + NewFormat4Segments / sizeof(FORMAT4_SEGMENTS);
				if (*(ushort*)(ptr2 + 6L / (long)sizeof(FORMAT4_SEGMENTS)) == 0)
				{
					num7 = num7 - *(ushort*)(ptr2 + 2L / (long)sizeof(FORMAT4_SEGMENTS)) + *(ushort*)ptr2 + 1;
					*(short*)(ptr2 + 6L / (long)sizeof(FORMAT4_SEGMENTS)) = 0;
				}
				else
				{
					*(short*)(ptr2 + 6L / (long)sizeof(FORMAT4_SEGMENTS)) = (short)((*pusnSegment - num8 + *psnFormat4GlyphIdArray + 1) * 2);
					ushort num6 = *(ushort*)(ptr2 + 2L / (long)sizeof(FORMAT4_SEGMENTS));
					if (num6 <= *(ushort*)ptr2)
					{
						do
						{
							(*psnFormat4GlyphIdArray * 2L)[NewFormat4GlyphIdArray / 2] = *(ushort*)(pCharGlyphMapList + (ulong)num7 * 4UL / (ulong)sizeof(Char_Glyph_Map_List) + 2L / (long)sizeof(Char_Glyph_Map_List));
							*psnFormat4GlyphIdArray += 1;
							num7 += 1;
							num6 += 1;
						}
						while (num6 <= *(ushort*)ptr2);
					}
				}
				num8 += 1;
			}
			while (num8 < *pusnSegment);
		}
		*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 6L / (long)sizeof(FORMAT4_SEGMENTS)) = 0;
		*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 4L / (long)sizeof(FORMAT4_SEGMENTS)) = 1;
		(*pusnSegment * 8L)[NewFormat4Segments / 2] = (short)65535;
		*(short*)(NewFormat4Segments + (ulong)(*pusnSegment) * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 2L / (long)sizeof(FORMAT4_SEGMENTS)) = (short)65535;
		*pusnSegment += 1;
		*(short*)pCmapFormat4 = 4;
		*(short*)(pCmapFormat4 + 4L / (long)sizeof(CMAP_FORMAT4)) = 0;
		*(short*)(pCmapFormat4 + 2L / (long)sizeof(CMAP_FORMAT4)) = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.Format4CmapLength(*pusnSegment, *psnFormat4GlyphIdArray);
		*(short*)(pCmapFormat4 + 6L / (long)sizeof(CMAP_FORMAT4)) = (short)(*pusnSegment * 2);
		ushort num9 = (ushort)(1 << (int)(<Module>.MS.Internal.TtfDelta.log2(*pusnSegment) + 1));
		*(short*)(pCmapFormat4 + 8L / (long)sizeof(CMAP_FORMAT4)) = (short)num9;
		*(short*)(pCmapFormat4 + 10L / (long)sizeof(CMAP_FORMAT4)) = (short)<Module>.MS.Internal.TtfDelta.log2((ushort)((uint)num9 >> 1));
		*(short*)(pCmapFormat4 + 12L / (long)sizeof(CMAP_FORMAT4)) = (short)(*pusnSegment * 2 - *(ushort*)(pCmapFormat4 + 8L / (long)sizeof(CMAP_FORMAT4)));
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00007D34 File Offset: 0x00007134
	[SecurityCritical]
	internal unsafe static short WriteOutFormat4CmapData(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, CMAP_FORMAT4* pCmapFormat4, FORMAT4_SEGMENTS* NewFormat4Segments, ushort* NewFormat4GlyphIdArray, ushort usnSegment, ushort snFormat4GlyphIdArray, uint ulNewOffset, uint* pulBytesWritten)
	{
		ushort num2;
		short num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)pCmapFormat4, 14, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT4_CONTROL), ulNewOffset, &num2);
		if (num != 0)
		{
			return num;
		}
		uint num3 = num2 + ulNewOffset;
		ushort num4 = 0;
		if (0 < usnSegment)
		{
			ulong num5 = num3;
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, (num4 * 8L)[NewFormat4Segments / 2], (uint)((ulong)num4 * 2UL + num5));
				if (num != 0)
				{
					return num;
				}
				num4 += 1;
			}
			while (num4 < usnSegment);
		}
		ulong num6 = (ulong)usnSegment * 2UL;
		num3 = (uint)(num3 + num6);
		num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, 0, num3);
		if (num != 0)
		{
			return num;
		}
		num3 = (uint)(num3 + 2UL);
		num4 = 0;
		if (0 < usnSegment)
		{
			ulong num7 = num3;
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, *(ushort*)(NewFormat4Segments + (ulong)num4 * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 2L / (long)sizeof(FORMAT4_SEGMENTS)), (uint)((ulong)num4 * 2UL + num7));
				if (num != 0)
				{
					return num;
				}
				num4 += 1;
			}
			while (num4 < usnSegment);
		}
		num3 = (uint)(num3 + num6);
		num4 = 0;
		if (0 < usnSegment)
		{
			ulong num8 = num3;
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, (ushort)(*(short*)(NewFormat4Segments + (ulong)num4 * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 4L / (long)sizeof(FORMAT4_SEGMENTS))), (uint)((ulong)num4 * 2UL + num8));
				if (num != 0)
				{
					return num;
				}
				num4 += 1;
			}
			while (num4 < usnSegment);
		}
		num3 = (uint)(num3 + num6);
		num4 = 0;
		if (0 < usnSegment)
		{
			ulong num9 = num3;
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, *(ushort*)(NewFormat4Segments + (ulong)num4 * 8UL / (ulong)sizeof(FORMAT4_SEGMENTS) + 6L / (long)sizeof(FORMAT4_SEGMENTS)), (uint)((ulong)num4 * 2UL + num9));
				if (num != 0)
				{
					return num;
				}
				num4 += 1;
			}
			while (num4 < usnSegment);
		}
		num3 = (uint)(num3 + num6);
		num4 = 0;
		if (0 < snFormat4GlyphIdArray)
		{
			ulong num10 = num3;
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, (num4 * 2L)[NewFormat4GlyphIdArray / 2], (uint)((ulong)num4 * 2UL + num10));
				if (num != 0)
				{
					return num;
				}
				num4 += 1;
			}
			while (num4 < snFormat4GlyphIdArray);
		}
		*(int*)pulBytesWritten = (uint)((ulong)snFormat4GlyphIdArray * 2UL + num3) - ulNewOffset;
		return 0;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00007EB0 File Offset: 0x000072B0
	[SecurityCritical]
	internal unsafe static void ComputeFormat12CmapData(CMAP_FORMAT12* pCmapFormat12, FORMAT12_GROUPS* NewFormat12Groups, uint* pulnGroups, Char_Glyph_Map_List_Ex* pCharGlyphMapList, uint ulnCharGlyphMapListCount)
	{
		uint num = 0;
		*(int*)pulnGroups = 0;
		if (0 < ulnCharGlyphMapListCount)
		{
			do
			{
				uint num2 = num;
				if (num < ulnCharGlyphMapListCount - 1)
				{
					do
					{
						Char_Glyph_Map_List_Ex* ptr = num * 8L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List_Ex);
						Char_Glyph_Map_List_Ex* ptr2 = (num + 1) * 8L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List_Ex);
						if (*(int*)ptr + 1 != *(int*)ptr2 || *(int*)(ptr + 4L / (long)sizeof(Char_Glyph_Map_List_Ex)) + 1 != *(int*)(ptr2 + 4L / (long)sizeof(Char_Glyph_Map_List_Ex)))
						{
							break;
						}
						num++;
					}
					while (num < ulnCharGlyphMapListCount - 1);
				}
				uint num3 = num;
				num++;
				Char_Glyph_Map_List_Ex* ptr3 = num2 * 8L + pCharGlyphMapList / sizeof(Char_Glyph_Map_List_Ex);
				(*(int*)pulnGroups * 12L)[NewFormat12Groups / 4] = *(int*)ptr3;
				*(int*)(NewFormat12Groups + (ulong)(*(int*)pulnGroups) * 12UL / (ulong)sizeof(FORMAT12_GROUPS) + 4L / (long)sizeof(FORMAT12_GROUPS)) = (num3 * 8L)[pCharGlyphMapList / 4];
				*(int*)(NewFormat12Groups + (ulong)(*(int*)pulnGroups) * 12UL / (ulong)sizeof(FORMAT12_GROUPS) + 8L / (long)sizeof(FORMAT12_GROUPS)) = *(int*)(ptr3 + 4L / (long)sizeof(Char_Glyph_Map_List_Ex));
				*(int*)pulnGroups = *(int*)pulnGroups + 1;
			}
			while (num < ulnCharGlyphMapListCount);
		}
		*(short*)pCmapFormat12 = 12;
		*(short*)(pCmapFormat12 + 2L / (long)sizeof(CMAP_FORMAT12)) = 0;
		uint num4 = (uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.FORMAT12_GROUPS_CONTROL));
		uint num5 = (uint)(*(int*)pulnGroups * (int)num4);
		*(int*)(pCmapFormat12 + 4L / (long)sizeof(CMAP_FORMAT12)) = (int)((uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT12_CONTROL)) + num5);
		*(int*)(pCmapFormat12 + 12L / (long)sizeof(CMAP_FORMAT12)) = *(int*)pulnGroups;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00007F90 File Offset: 0x00007390
	[SecurityCritical]
	internal unsafe static short WriteOutFormat12CmapData(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, CMAP_FORMAT12* pCmapFormat12, FORMAT12_GROUPS* NewFormat12Groups, uint ulnGroups, uint ulNewOffset, uint* pulBytesWritten)
	{
		ushort num2;
		short num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)pCmapFormat12, 16, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT12_CONTROL), ulNewOffset, &num2);
		if (num != 0)
		{
			return num;
		}
		uint num3 = num2 + ulNewOffset;
		uint num4 = 0;
		if (0 < ulnGroups)
		{
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, num4 * 12L + NewFormat12Groups, 12, (byte*)(&<Module>.MS.Internal.TtfDelta.FORMAT12_GROUPS_CONTROL), num3, &num2);
				if (num != 0)
				{
					return num;
				}
				num3 += num2;
				num4++;
			}
			while (num4 < ulnGroups);
		}
		*(int*)pulBytesWritten = num3 - ulNewOffset;
		return 0;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000CC6C File Offset: 0x0000C06C
	[SecurityCritical]
	internal unsafe static short ReadAllocNameRecords(TTFACC_FILEBUFFERINFO* pInputBufferInfo, namerecord** ppNameRecordArray, ushort* pNameRecordCount, method lfpnAllocate, method lfpnFree)
	{
		*(long*)ppNameRecordArray = 0L;
		*pNameRecordCount = 0;
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@));
		if (num == null)
		{
			return 1037;
		}
		if (<Module>.MS.Internal.TtfDelta.TTTableLength(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@)) == null)
		{
			return 1067;
		}
		NAME_HEADER name_HEADER;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&name_HEADER), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.NAME_HEADER_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		uint num4 = num3 + num;
		void* ptr = calli(System.Void*(System.UInt64), (ulong)(*(ref name_HEADER + 2)) * 40UL, lfpnAllocate);
		*(long*)ppNameRecordArray = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		*pNameRecordCount = *(ref name_HEADER + 2);
		ushort num5 = 0;
		if (0 < *(ref name_HEADER + 2))
		{
			for (;;)
			{
				long num6 = (long)((ulong)num5 * 40UL);
				num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, *(long*)ppNameRecordArray + num6, 12, (byte*)(&<Module>.MS.Internal.TtfDelta.NAME_RECORD_CONTROL), num4, &num3);
				if (num2 != 0)
				{
					goto IL_131;
				}
				num4 += num3;
				ushort num7 = *(num6 + *(long*)ppNameRecordArray + 8L);
				if (num7 != 0)
				{
					*(num6 + *(long*)ppNameRecordArray + 16L) = calli(System.Void*(System.UInt64), (ulong)num7, lfpnAllocate);
					long num8 = *(long*)ppNameRecordArray + num6;
					ulong num9 = (ulong)(*(num8 + 16L));
					if (num9 == 0UL)
					{
						break;
					}
					num2 = <Module>.MS.Internal.TtfDelta.ReadBytes(pInputBufferInfo, num9, *(num8 + 10L) + *(ref name_HEADER + 4) + num, *(num8 + 8L));
					if (num2 != 0)
					{
						goto IL_131;
					}
					*(num6 + *(long*)ppNameRecordArray + 24L) = 0L;
					*(num6 + *(long*)ppNameRecordArray + 12L) = 0;
					*(num6 + *(long*)ppNameRecordArray + 32L) = 0;
				}
				num5 += 1;
				if (num5 >= *pNameRecordCount)
				{
					return num2;
				}
			}
			num2 = 1005;
			IL_131:
			<Module>.MS.Internal.TtfDelta.FreeNameRecords(*(long*)ppNameRecordArray, *pNameRecordCount, lfpnFree);
			*(long*)ppNameRecordArray = 0L;
			*pNameRecordCount = 0;
		}
		return num2;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00007FF4 File Offset: 0x000073F4
	[SecurityCritical]
	internal unsafe static int DescendingStringLengthCompare(void* arg1, void* arg2)
	{
		ushort num = *(ushort*)(arg1 + 2L / (long)sizeof(void));
		ushort num2 = *(ushort*)(arg2 + 2L / (long)sizeof(void));
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = 1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000790C File Offset: 0x00006D0C
	[SecurityCritical]
	internal unsafe static int AscendingRecordIndexCompare(void* arg1, void* arg2)
	{
		ushort num = *(ushort*)arg1;
		ushort num2 = *(ushort*)arg2;
		if (num == num2)
		{
			return 0;
		}
		int result;
		int num3 = result = -1;
		if (num >= num2)
		{
			result = -num3;
		}
		return result;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000801C File Offset: 0x0000741C
	[SecurityCritical]
	internal unsafe static void SortNameRecordsByStringLength(namerecordstrings* pNameRecordStrings, ushort NameRecordCount)
	{
		if (pNameRecordStrings != null && NameRecordCount != 0)
		{
			<Module>.qsort((void*)pNameRecordStrings, (ulong)NameRecordCount, 8UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.DescendingStringLengthCompare));
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00008040 File Offset: 0x00007440
	[SecurityCritical]
	internal unsafe static void SortNameRecordsByNameRecordIndex(namerecordstrings* pNameRecordStrings, ushort NameRecordCount)
	{
		if (pNameRecordStrings != null && NameRecordCount != 0)
		{
			<Module>.qsort((void*)pNameRecordStrings, (ulong)NameRecordCount, 8UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingRecordIndexCompare));
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00008064 File Offset: 0x00007464
	[SecurityCritical]
	internal unsafe static int AscendingNameRecordCompare(void* arg1, void* arg2)
	{
		ushort num = *(ushort*)arg1;
		ushort num2 = *(ushort*)arg2;
		if (num != num2)
		{
			int result;
			int num3 = result = -1;
			if (num >= num2)
			{
				result = -num3;
			}
			return result;
		}
		ushort num4 = *(ushort*)(arg1 + 2L / (long)sizeof(void));
		ushort num5 = *(ushort*)(arg2 + 2L / (long)sizeof(void));
		if (num4 != num5)
		{
			int result2;
			int num6 = result2 = -1;
			if (num4 >= num5)
			{
				result2 = -num6;
			}
			return result2;
		}
		ushort num7 = *(ushort*)(arg1 + 4L / (long)sizeof(void));
		ushort num8 = *(ushort*)(arg2 + 4L / (long)sizeof(void));
		if (num7 != num8)
		{
			int result3;
			int num9 = result3 = -1;
			if (num7 >= num8)
			{
				result3 = -num9;
			}
			return result3;
		}
		ushort num10 = *(ushort*)(arg1 + 6L / (long)sizeof(void));
		ushort num11 = *(ushort*)(arg2 + 6L / (long)sizeof(void));
		if (num10 == num11)
		{
			return 0;
		}
		int result4;
		int num12 = result4 = -1;
		if (num10 >= num11)
		{
			result4 = -num12;
		}
		return result4;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000080D4 File Offset: 0x000074D4
	[SecurityCritical]
	internal unsafe static short WriteNameRecords(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, namerecord* pNameRecordArray, ushort NameRecordCount, int bDeleteStrings, int bOptimize, uint* pulBytesWritten)
	{
		*(int*)pulBytesWritten = 0;
		if (pNameRecordArray == null || NameRecordCount == 0)
		{
			return 1000;
		}
		ulong num = (ulong)NameRecordCount;
		<Module>.qsort((void*)pNameRecordArray, num, 40UL, ldftn(MS.Internal.TtfDelta.?A0x67cd8d4d.AscendingNameRecordCompare));
		NAME_HEADER name_HEADER = 0;
		uint num2 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.NAME_HEADER_CONTROL));
		namerecordstrings* ptr = (namerecordstrings*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num * 8UL);
		if (ptr == null)
		{
			return 1005;
		}
		ushort num3 = 0;
		ushort num4 = 0;
		if (0 < NameRecordCount)
		{
			do
			{
				if (bDeleteStrings == 0 || *(int*)(pNameRecordArray + (ulong)num4 * 40UL / (ulong)sizeof(namerecord) + 32L / (long)sizeof(namerecord)) == 0)
				{
					namerecordstrings* ptr2 = num3 * 8L + ptr / sizeof(namerecordstrings);
					*(short*)ptr2 = (short)num4;
					namerecord* ptr3 = num4 * 40L + pNameRecordArray / sizeof(namerecord);
					*(short*)(ptr2 + 2L / (long)sizeof(namerecordstrings)) = (short)(*(ushort*)(ptr3 + 8L / (long)sizeof(namerecord)));
					*(short*)(ptr2 + 4L / (long)sizeof(namerecordstrings)) = (short)num4;
					*(short*)(ptr2 + 6L / (long)sizeof(namerecordstrings)) = 0;
					*(short*)(ptr3 + 10L / (long)sizeof(namerecord)) = 0;
					num3 += 1;
				}
				num4 += 1;
			}
			while (num4 < NameRecordCount);
		}
		ushort num5 = 0;
		int num6 = (int)(<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.NAME_RECORD_CONTROL)) * num3);
		*(ref name_HEADER + 4) = (short)((int)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.NAME_HEADER_CONTROL)) + num6);
		if (bOptimize != 0)
		{
			<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.SortNameRecordsByStringLength(ptr, num3);
			num4 = 1;
			if (1 < num3)
			{
				for (;;)
				{
					namerecordstrings* ptr4 = num4 * 8L + ptr / sizeof(namerecordstrings);
					namerecord* ptr5 = *(ushort*)ptr4 * 40L + pNameRecordArray / sizeof(namerecord);
					sbyte* ptr6 = *(long*)(ptr5 + 24L / (long)sizeof(namerecord));
					if (ptr6 == null)
					{
						ptr6 = *(long*)(ptr5 + 16L / (long)sizeof(namerecord));
					}
					int num7 = (int)(num4 - 1);
					namerecordstrings* ptr7 = (long)num7 * 8L + ptr / sizeof(namerecordstrings);
					namerecord* ptr8 = *(ushort*)ptr7 * 40L + pNameRecordArray / sizeof(namerecord);
					sbyte* ptr9 = *(long*)(ptr8 + 24L / (long)sizeof(namerecord));
					if (ptr9 == null)
					{
						ptr9 = *(long*)(ptr8 + 16L / (long)sizeof(namerecord));
					}
					ushort num8 = *(ushort*)(ptr4 + 2L / (long)sizeof(namerecordstrings));
					if (num8 != *(ushort*)(ptr7 + 2L / (long)sizeof(namerecordstrings)))
					{
						goto IL_1C1;
					}
					ulong num9 = (ulong)num8;
					sbyte* ptr10 = ptr9;
					if (num9 != 0UL)
					{
						byte b = *(byte*)ptr6;
						byte b2 = *(byte*)ptr9;
						if (b >= b2)
						{
							long num10 = (long)(ptr6 - ptr9);
							while (b <= b2)
							{
								if (num9 == 1UL)
								{
									goto IL_1A4;
								}
								num9 -= 1UL;
								ptr10 += 1L / (long)sizeof(sbyte);
								b = *(byte*)(num10 / (long)sizeof(sbyte) + ptr10);
								b2 = *(byte*)ptr10;
								if (b < b2)
								{
									break;
								}
							}
							goto IL_1C1;
						}
						goto IL_1C1;
					}
					IL_1A4:
					*(short*)(ptr4 + 4L / (long)sizeof(namerecordstrings)) = (short)(*(ushort*)(ptr7 + 4L / (long)sizeof(namerecordstrings)));
					*(short*)(ptr4 + 6L / (long)sizeof(namerecordstrings)) = (short)(*(ushort*)(ptr7 + 6L / (long)sizeof(namerecordstrings)));
					IL_29C:
					num4 += 1;
					if (num4 >= num3)
					{
						break;
					}
					continue;
					IL_1C1:
					ushort num11 = 0;
					if (0 >= num7)
					{
						goto IL_29C;
					}
					ulong num12 = (ulong)num8;
					for (;;)
					{
						namerecordstrings* ptr11 = num11 * 8L + ptr / sizeof(namerecordstrings);
						ushort num13 = *(ushort*)(ptr11 + 4L / (long)sizeof(namerecordstrings));
						namerecord* ptr12 = num13 * 40L + pNameRecordArray / sizeof(namerecord);
						ptr9 = *(long*)(ptr12 + 24L / (long)sizeof(namerecord));
						if (ptr9 == null)
						{
							ptr9 = *(long*)(ptr12 + 16L / (long)sizeof(namerecord));
						}
						ushort num14 = *(ushort*)(ptr11 + 2L / (long)sizeof(namerecordstrings)) - num8;
						ushort num15 = 0;
						do
						{
							ulong num16 = num12;
							sbyte* ptr13 = num15 + ptr9 / sizeof(sbyte);
							sbyte* ptr14 = ptr13;
							if (num16 == 0UL)
							{
								goto IL_277;
							}
							byte b3 = *(byte*)ptr6;
							byte b4 = *(byte*)ptr14;
							if (b3 >= b4)
							{
								long num17 = (long)(ptr6 - ptr13);
								while (b3 <= b4)
								{
									if (num16 == 1UL)
									{
										goto IL_277;
									}
									num16 -= 1UL;
									ptr14 += 1L / (long)sizeof(sbyte);
									b3 = *(byte*)(num17 / (long)sizeof(sbyte) + ptr14);
									b4 = *(byte*)ptr14;
									if (b3 < b4)
									{
										break;
									}
								}
							}
							num15 += 1;
						}
						while (num15 <= num14);
						IL_28D:
						num11 += 1;
						if ((int)num11 >= num7)
						{
							goto IL_29C;
						}
						continue;
						goto IL_28D;
						IL_277:
						*(short*)(ptr4 + 4L / (long)sizeof(namerecordstrings)) = (short)num13;
						*(short*)(ptr4 + 6L / (long)sizeof(namerecordstrings)) = (short)num15;
						if (num15 > num14)
						{
							goto IL_28D;
						}
						goto IL_29C;
					}
				}
			}
			<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.SortNameRecordsByNameRecordIndex(ptr, num3);
		}
		ushort num18 = 0;
		short num21;
		ushort num22;
		if (0 < num3)
		{
			do
			{
				namerecordstrings* ptr15 = num18 * 8L + ptr / sizeof(namerecordstrings);
				ushort num19 = *(ushort*)ptr15;
				ushort num20 = *(ushort*)(ptr15 + 4L / (long)sizeof(namerecordstrings));
				namerecord* ptr16 = num19 * 40L + pNameRecordArray / sizeof(namerecord);
				if (*(ushort*)(ptr16 + 12L / (long)sizeof(namerecord)) == 0)
				{
					if (num19 != num20)
					{
						namerecord* ptr17 = num20 * 40L + pNameRecordArray / sizeof(namerecord);
						if (*(ushort*)(ptr17 + 12L / (long)sizeof(namerecord)) == 0)
						{
							*(short*)(ptr17 + 10L / (long)sizeof(namerecord)) = (short)num5;
							*(short*)(ptr17 + 12L / (long)sizeof(namerecord)) = 1;
							sbyte* ptr6 = *(long*)(ptr17 + 24L / (long)sizeof(namerecord));
							if (ptr6 == null)
							{
								ptr6 = *(long*)(ptr17 + 16L / (long)sizeof(namerecord));
							}
							num21 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, (byte*)ptr6, *(ref name_HEADER + 4) + num5, *(ushort*)(ptr17 + 8L / (long)sizeof(namerecord)));
							if (num21 != 0)
							{
								goto IL_402;
							}
							num5 = *(ushort*)(ptr17 + 8L / (long)sizeof(namerecord)) + num5;
						}
						*(short*)(ptr16 + 10L / (long)sizeof(namerecord)) = (short)(*(ushort*)(ptr15 + 6L / (long)sizeof(namerecordstrings)) + *(ushort*)(ptr17 + 10L / (long)sizeof(namerecord)));
					}
					else
					{
						*(short*)(ptr16 + 10L / (long)sizeof(namerecord)) = (short)(*(ushort*)(ptr15 + 6L / (long)sizeof(namerecordstrings)) + num5);
						sbyte* ptr6 = *(long*)(ptr16 + 24L / (long)sizeof(namerecord));
						if (ptr6 == null)
						{
							ptr6 = *(long*)(ptr16 + 16L / (long)sizeof(namerecord));
						}
						num21 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, (byte*)ptr6, *(ref name_HEADER + 4) + num5, *(ushort*)(ptr16 + 8L / (long)sizeof(namerecord)));
						if (num21 != 0)
						{
							goto IL_402;
						}
						num5 = *(ushort*)(ptr16 + 8L / (long)sizeof(namerecord)) + num5;
					}
					*(short*)(ptr16 + 12L / (long)sizeof(namerecord)) = 1;
				}
				num21 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)ptr16, 12, (byte*)(&<Module>.MS.Internal.TtfDelta.NAME_RECORD_CONTROL), num2, &num22);
				if (num21 != 0)
				{
					goto IL_402;
				}
				num2 += num22;
				num18 += 1;
			}
			while (num18 < num3);
		}
		*(ref name_HEADER + 2) = (short)num3;
		*(int*)pulBytesWritten = (int)(*(ref name_HEADER + 4) + num5);
		num21 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&name_HEADER), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.NAME_HEADER_CONTROL), 0, &num22);
		IL_402:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num21;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000084F4 File Offset: 0x000078F4
	[SecurityCritical]
	internal unsafe static void FreeNameRecords(namerecord* pNameRecordArray, ushort NameRecordCount, method lfpnFree)
	{
		if (pNameRecordArray != null)
		{
			ushort num = 0;
			if (0 < NameRecordCount)
			{
				do
				{
					namerecord* ptr = num * 40L + pNameRecordArray / sizeof(namerecord);
					ulong num2 = (ulong)(*(long*)(ptr + 16L / (long)sizeof(namerecord)));
					if (num2 != 0UL)
					{
						calli(System.Void(System.Void*), num2, lfpnFree);
					}
					ulong num3 = (ulong)(*(long*)(ptr + 24L / (long)sizeof(namerecord)));
					if (num3 != 0UL)
					{
						calli(System.Void(System.Void*), num3, lfpnFree);
					}
					num += 1;
				}
				while (num < NameRecordCount);
			}
			calli(System.Void(System.Void*), pNameRecordArray, lfpnFree);
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000CDC0 File Offset: 0x0000C1C0
	[SecurityCritical]
	internal unsafe static short CompressTables(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint* pulBytesWritten)
	{
		uint num = *(int*)(pOutputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO));
		OFFSET_TABLE offset_TABLE;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return 1005;
		}
		ushort num4 = *(ref offset_TABLE + 4);
		num += num3;
		DIRECTORY* ptr = (DIRECTORY*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref offset_TABLE + 4)) * 16UL);
		if (ptr == null)
		{
			return 1005;
		}
		ushort num5 = 0;
		ushort num6 = 0;
		if (0 < num4)
		{
			do
			{
				DIRECTORY directory;
				num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num3);
				if (num2 != 0)
				{
					goto IL_A9;
				}
				num += num3;
				if (directory != 16843009 && *(ref directory + 12) != 0 && *(ref directory + 8) != 0)
				{
					cpblk((ulong)num5 * 16UL / (ulong)sizeof(DIRECTORY) + ptr, ref directory, 16);
					num5 += 1;
				}
				num6 += 1;
			}
			while (num6 < num4);
			goto IL_B1;
			IL_A9:
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return num2;
		}
		IL_B1:
		<Module>.MS.Internal.TtfDelta.SortByOffset(ptr, num5);
		uint num7 = (uint)(<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL)) * num5);
		num = (uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL)) + num7 + (uint)(*(int*)(pOutputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO)));
		num2 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, num, &num);
		if (num2 != 0)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return num2;
		}
		ushort num8 = 0;
		ushort num9 = 0;
		if (0 < num5)
		{
			do
			{
				if (num8 == 0)
				{
					DIRECTORY* ptr2 = num9 * 16L + ptr / sizeof(DIRECTORY);
					num2 = <Module>.MS.Internal.TtfDelta.CopyBlock(pOutputBufferInfo, num, *(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)), *(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)));
					if (num2 != 0)
					{
						goto IL_26A;
					}
					int num10 = (int)(num9 + 1);
					if (num10 < (int)num5)
					{
						DIRECTORY* ptr3 = (long)num10 * 16L + ptr / sizeof(DIRECTORY);
						if (*(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)) == *(int*)(ptr3 + 8L / (long)sizeof(DIRECTORY)))
						{
							uint num11 = (uint)(*(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)));
							if (num11 != 0U)
							{
								num8 = 1;
								*(int*)(ptr3 + 8L / (long)sizeof(DIRECTORY)) = num;
								*(int*)(ptr3 + 12L / (long)sizeof(DIRECTORY)) = (int)num11;
							}
						}
					}
					*(int*)(ptr2 + 8L / (long)sizeof(DIRECTORY)) = num;
					num2 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordGap(pOutputBufferInfo, num, *(int*)(ptr2 + 12L / (long)sizeof(DIRECTORY)), &num);
					if (num2 != 0)
					{
						goto IL_26A;
					}
				}
				else
				{
					num8 = 0;
				}
				DIRECTORY* ptr4 = num9 * 16L + ptr / sizeof(DIRECTORY);
				num2 = <Module>.MS.Internal.TtfDelta.CalcChecksum(pOutputBufferInfo, *(int*)(ptr4 + 8L / (long)sizeof(DIRECTORY)), *(int*)(ptr4 + 12L / (long)sizeof(DIRECTORY)), (uint*)(ptr4 + 4L / (long)sizeof(DIRECTORY)));
				if (num2 != 0)
				{
					goto IL_26A;
				}
				num9 += 1;
			}
			while (num9 < num5);
		}
		uint num12 = num;
		<Module>.MS.Internal.TtfDelta.SortByTag(ptr, num5);
		*(ref offset_TABLE + 4) = (short)num5;
		*(ref offset_TABLE + 6) = (short)(1 << (int)<Module>.MS.Internal.TtfDelta.log2(num5) << 4);
		*(ref offset_TABLE + 8) = (short)<Module>.MS.Internal.TtfDelta.log2((ushort)(1 << (int)<Module>.MS.Internal.TtfDelta.log2(num5)));
		*(ref offset_TABLE + 10) = (short)(((int)num5 - (1 << (int)<Module>.MS.Internal.TtfDelta.log2(num5))) * 16);
		num = *(int*)(pOutputBufferInfo + 12L / (long)sizeof(TTFACC_FILEBUFFERINFO));
		ushort num13;
		num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&offset_TABLE), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.OFFSET_TABLE_CONTROL), num, &num13);
		if (num2 == 0)
		{
			num += num13;
			num6 = 0;
			if (0 < num5)
			{
				do
				{
					num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, num6 * 16L + ptr, 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num, &num13);
					if (num2 != 0)
					{
						goto IL_26A;
					}
					num += num13;
					num6 += 1;
				}
				while (num6 < num5);
			}
			*(int*)pulBytesWritten = num12;
		}
		IL_26A:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num2;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00008544 File Offset: 0x00007944
	[SecurityCritical]
	internal unsafe static short InitCmapOffsetArray(cmapoffsetrecordkeeper* pKeeper, ushort usRecordCount)
	{
		void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)usRecordCount * 8UL);
		*(long*)pKeeper = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		*(short*)(pKeeper + 8L / (long)sizeof(cmapoffsetrecordkeeper)) = (short)usRecordCount;
		*(short*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper)) = 0;
		return 0;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00008578 File Offset: 0x00007978
	[SecurityCritical]
	internal unsafe static void FreeCmapOffsetArray(cmapoffsetrecordkeeper* pKeeper)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)pKeeper);
		*(long*)pKeeper = 0L;
		*(short*)(pKeeper + 8L / (long)sizeof(cmapoffsetrecordkeeper)) = 0;
		*(short*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper)) = 0;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000085A0 File Offset: 0x000079A0
	[SecurityCritical]
	internal unsafe static short RecordCmapOffset(cmapoffsetrecordkeeper* pKeeper, uint ulOldCmapOffset, uint ulNewCmapOffset)
	{
		ushort num = *(ushort*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper));
		if (num >= *(ushort*)(pKeeper + 8L / (long)sizeof(cmapoffsetrecordkeeper)))
		{
			return 1060;
		}
		*((ulong)num * 8UL + (ulong)(*(long*)pKeeper)) = ulOldCmapOffset;
		*(*(long*)pKeeper + (long)((ulong)(*(ushort*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper))) * 8UL) + 4L) = ulNewCmapOffset;
		*(short*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper)) = (short)(*(ushort*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper)) + 1);
		return 0;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000085F0 File Offset: 0x000079F0
	[SecurityCritical]
	internal unsafe static uint LookupCmapOffset(cmapoffsetrecordkeeper* pKeeper, uint ulOldCmapOffset)
	{
		ushort num = 0;
		ushort num2 = *(ushort*)(pKeeper + 10L / (long)sizeof(cmapoffsetrecordkeeper));
		if (0 < num2)
		{
			long num3 = *(long*)pKeeper;
			while (ulOldCmapOffset != *((ulong)num * 8UL + (ulong)num3))
			{
				num += 1;
				if (num >= num2)
				{
					return 0;
				}
			}
			return *(num3 + (long)((ulong)num * 8UL) + 4L);
		}
		return 0;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00008630 File Offset: 0x00007A30
	[SecurityCritical]
	internal unsafe static void SortCmapSubByOffset(CMAP_TABLELOC* pCmapTableLoc, ushort usSubTableCount, IndexOffset* pIndexArray)
	{
		ushort num = 0;
		if (0 < usSubTableCount)
		{
			do
			{
				ushort num2 = 0;
				if (0 < num)
				{
					uint num3 = (uint)(*(int*)(pCmapTableLoc + (ulong)num * 8UL / (ulong)sizeof(CMAP_TABLELOC) + 4L / (long)sizeof(CMAP_TABLELOC)));
					while (num3 >= (uint)(*(int*)(pCmapTableLoc + (ulong)(num2 * 8L)[pIndexArray / 2] * 8UL / (ulong)sizeof(CMAP_TABLELOC) + 4L / (long)sizeof(CMAP_TABLELOC))))
					{
						num2 += 1;
						if (num2 >= num)
						{
							goto IL_5B;
						}
					}
					ushort num4 = num;
					if (num > num2)
					{
						do
						{
							(num4 * 8L)[pIndexArray / 2] = (short)(*(ushort*)((long)(num4 - 1) * 8L + pIndexArray / 2));
							num4 += ushort.MaxValue;
						}
						while (num4 > num2);
					}
				}
				IL_5B:
				(num2 * 8L)[pIndexArray / 2] = (short)num;
				num += 1;
			}
			while (num < usSubTableCount);
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000D040 File Offset: 0x0000C440
	[SecurityCritical]
	internal unsafe static short CompressCmapSubTables(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, CMAP_TABLELOC* pCmapTableLoc, ushort usSubTableCount, uint ulCmapOffset, uint ulSubTableOffset, uint ulCmapOldLength, uint* pulCmapNewLength)
	{
		short num = 0;
		uint num2 = 0;
		IndexOffset* ptr = (IndexOffset*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)usSubTableCount * 8UL);
		if (ptr == null)
		{
			return 1005;
		}
		<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.SortCmapSubByOffset(pCmapTableLoc, usSubTableCount, ptr);
		uint num3 = ulSubTableOffset;
		uint num4 = 0;
		ushort num5 = 0;
		ushort num7;
		if (0 < usSubTableCount)
		{
			do
			{
				IndexOffset* ptr2 = num5 * 8L + ptr / sizeof(IndexOffset);
				ushort num6 = *(ushort*)ptr2;
				if (num5 > 0 && *(int*)(pCmapTableLoc + (ulong)num6 * 8UL / (ulong)sizeof(CMAP_TABLELOC) + 4L / (long)sizeof(CMAP_TABLELOC)) == num4)
				{
					*(int*)(ptr2 + 4L / (long)sizeof(IndexOffset)) = *(int*)(ptr + (long)(num5 - 1) * 8L / (long)sizeof(IndexOffset) + 4L / (long)sizeof(IndexOffset));
				}
				else
				{
					CMAP_TABLELOC* ptr3 = pCmapTableLoc + (ulong)num6 * 8UL / (ulong)sizeof(CMAP_TABLELOC) + 4L / (long)sizeof(CMAP_TABLELOC);
					CMAP_SUBHEADER_GEN cmap_SUBHEADER_GEN;
					num = <Module>.MS.Internal.TtfDelta.ReadCmapLength(pOutputBufferInfo, &cmap_SUBHEADER_GEN, *(int*)ptr3 + ulCmapOffset, &num7);
					if (num != 0)
					{
						goto IL_180;
					}
					uint num8 = num3;
					num3 = (num3 + 1 & -2);
					ushort num9 = num3 - num8;
					if ((int)num9 + *(ref cmap_SUBHEADER_GEN + 4) + num2 > ulCmapOldLength)
					{
						goto IL_110;
					}
					*(int*)(ptr2 + 4L / (long)sizeof(IndexOffset)) = num3 - ulCmapOffset;
					uint num10 = (uint)(*(int*)ptr3);
					num4 = num10;
					num = <Module>.MS.Internal.TtfDelta.CopyBlock(pOutputBufferInfo, num3, num10 + ulCmapOffset, *(ref cmap_SUBHEADER_GEN + 4));
					if (num != 0)
					{
						goto IL_180;
					}
					ushort num11 = 0;
					if (0 < num9)
					{
						do
						{
							<Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, 0, num11 + num8);
							num11 += 1;
						}
						while (num11 < num9);
					}
					num3 += *(ref cmap_SUBHEADER_GEN + 4);
					num2 = num3 - ulCmapOffset;
				}
				num5 += 1;
			}
			while (num5 < usSubTableCount);
			goto IL_118;
			IL_110:
			num = 1007;
			goto IL_180;
		}
		IL_118:
		num5 = 0;
		if (0 < usSubTableCount)
		{
			do
			{
				IndexOffset* ptr4 = num5 * 8L + ptr / sizeof(IndexOffset);
				*(int*)(pCmapTableLoc + (ulong)(*(ushort*)ptr4) * 8UL / (ulong)sizeof(CMAP_TABLELOC) + 4L / (long)sizeof(CMAP_TABLELOC)) = *(int*)(ptr4 + 4L / (long)sizeof(IndexOffset));
				num5 += 1;
			}
			while (num5 < usSubTableCount);
		}
		uint num12 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_HEADER_CONTROL)) + ulCmapOffset;
		num5 = 0;
		if (0 < usSubTableCount)
		{
			do
			{
				num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, num5 * 8L + pCmapTableLoc, 8, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_TABLELOC_CONTROL), num12, &num7);
				if (num != 0)
				{
					break;
				}
				num12 += num7;
				num5 += 1;
			}
			while (num5 < usSubTableCount);
		}
		IL_180:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		if (num == 0)
		{
			num = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@), num2);
		}
		*(int*)pulCmapNewLength = num2;
		return num;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000086AC File Offset: 0x00007AAC
	[SecurityCritical]
	internal unsafe static ushort GetCmapSubtableCount(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulCmapOffset)
	{
		CMAP_HEADER cmap_HEADER;
		ushort num;
		return (<Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&cmap_HEADER), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_HEADER_CONTROL), ulCmapOffset, &num) != 0) ? 0 : (*(ref cmap_HEADER + 2));
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000086D8 File Offset: 0x00007AD8
	[SecurityCritical]
	internal unsafe static short ModMacStandardCmap(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulOffset, byte* puchKeepGlyphList, ushort usGlyphCount)
	{
		ushort num = 0;
		do
		{
			byte b;
			short num2 = <Module>.MS.Internal.TtfDelta.ReadByte(pOutputBufferInfo, &b, ulOffset);
			if (num2 != 0)
			{
				return num2;
			}
			if ((ushort)b >= usGlyphCount || b[puchKeepGlyphList] == 0)
			{
				num2 = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, 0, ulOffset);
				if (num2 != 0)
				{
					return num2;
				}
			}
			ulOffset = (uint)(ulOffset + 1UL);
			num += 1;
		}
		while (num < 256);
		return 0;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00008728 File Offset: 0x00007B28
	[SecurityCritical]
	internal unsafe static short ModMacTrimmedCmap(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulOffset, byte* puchKeepGlyphList, ushort usGlyphCount)
	{
		ushort num = ushort.MaxValue;
		ushort num2 = 0;
		CMAP_FORMAT6 cmap_FORMAT;
		ushort num4;
		short num3 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&cmap_FORMAT), 10, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT6_CONTROL), ulOffset, &num4);
		if (num3 != 0)
		{
			return num3;
		}
		uint num5 = num4 + ulOffset;
		uint num6 = num5;
		ushort num7 = 0;
		if (0 < *(ref cmap_FORMAT + 8))
		{
			ushort num8;
			do
			{
				num3 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, &num8, num6);
				if (num3 != 0)
				{
					return num3;
				}
				if (num8 < usGlyphCount && num8[puchKeepGlyphList] != 0)
				{
					if (num == 65535)
					{
						num = *(ref cmap_FORMAT + 6) + num7;
					}
					num2 = *(ref cmap_FORMAT + 6) + num7;
				}
				num6 = (uint)(num6 + 2UL);
				num7 += 1;
			}
			while (num7 < *(ref cmap_FORMAT + 8));
			if (num == 65535)
			{
				goto IL_108;
			}
			*(ref cmap_FORMAT + 6) = (short)num;
			*(ref cmap_FORMAT + 8) = (short)(num2 - num + 1);
			num6 = (uint)((ulong)num * 2UL + num5);
			num7 = num;
			if (num <= num2)
			{
				for (;;)
				{
					num3 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, &num8, num6);
					if (num3 != 0)
					{
						return num3;
					}
					if (num8 < usGlyphCount && num8[puchKeepGlyphList] != 0)
					{
						num3 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, num8, num5);
						if (num3 != 0)
						{
							break;
						}
					}
					else
					{
						num3 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, 0, num5);
						if (num3 != 0)
						{
							return num3;
						}
					}
					num6 = (uint)(num6 + 2UL);
					num5 = (uint)(num5 + 2UL);
					num7 += 1;
					if (num7 > num2)
					{
						goto IL_114;
					}
				}
				return num3;
			}
			goto IL_114;
		}
		IL_108:
		*(ref cmap_FORMAT + 6) = 0;
		*(ref cmap_FORMAT + 8) = 0;
		IL_114:
		*(ref cmap_FORMAT + 2) = num5 - ulOffset;
		ushort num9;
		return <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&cmap_FORMAT), 10, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_FORMAT6_CONTROL), ulOffset, &num9);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000D1EC File Offset: 0x0000C5EC
	[SecurityCritical]
	internal unsafe static short ModCmap(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphCount, ushort* pOS2MinChr, ushort* pOS2MaxChr, uint* pulNewOutOffset)
	{
		FORMAT4_SEGMENTS* ptr = null;
		ushort* ptr2 = null;
		Char_Glyph_Map_List* ptr3 = null;
		ushort num = 0;
		Char_Glyph_Map_List_Ex* ptr4 = null;
		uint num2 = 0;
		short num3 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@), pulNewOutOffset);
		if (num3 != 0)
		{
			return num3;
		}
		uint num4 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@));
		uint num5 = <Module>.MS.Internal.TtfDelta.TTTableLength(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@));
		*pOS2MinChr = 0;
		*pOS2MaxChr = 0;
		if (num4 == null || num5 == null)
		{
			return 1060;
		}
		ushort num6 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetCmapSubtableCount(pOutputBufferInfo, num4);
		CMAP_TABLELOC* ptr5 = (CMAP_TABLELOC*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(num6 * 8)));
		if (ptr5 == null)
		{
			return 1005;
		}
		uint num7 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_HEADER_CONTROL)) + num4;
		cmapoffsetrecordkeeper cmapoffsetrecordkeeper;
		if (<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.InitCmapOffsetArray(&cmapoffsetrecordkeeper, num6) != 0)
		{
			return 1005;
		}
		ushort num8 = 0;
		if (0 < num6)
		{
			for (;;)
			{
				CMAP_TABLELOC* ptr6 = num8 * 8L + ptr5 / sizeof(CMAP_TABLELOC);
				ushort num9;
				num3 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)ptr6, 8, (byte*)(&<Module>.MS.Internal.TtfDelta.CMAP_TABLELOC_CONTROL), num7, &num9);
				if (num3 != 0)
				{
					goto IL_2D0;
				}
				num7 += num9;
				uint num10 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.LookupCmapOffset(&cmapoffsetrecordkeeper, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)));
				if (num10 != null)
				{
					*(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) = num10;
				}
				else
				{
					CMAP_SUBHEADER_GEN cmap_SUBHEADER_GEN;
					num3 = <Module>.MS.Internal.TtfDelta.ReadCmapLength(pOutputBufferInfo, &cmap_SUBHEADER_GEN, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, &num9);
					if (num3 != 0)
					{
						goto IL_2D0;
					}
					if (cmap_SUBHEADER_GEN == null)
					{
						num3 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ModMacStandardCmap(pOutputBufferInfo, (int)num9 + *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, puchKeepGlyphList, usGlyphCount);
						if (num3 != 0)
						{
							goto IL_2D0;
						}
					}
					else if (cmap_SUBHEADER_GEN == 6)
					{
						num3 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ModMacTrimmedCmap(pOutputBufferInfo, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, puchKeepGlyphList, usGlyphCount);
						if (num3 != 0)
						{
							goto IL_2D0;
						}
					}
					else if (cmap_SUBHEADER_GEN == 4)
					{
						num3 = <Module>.MS.Internal.TtfDelta.ReadAllocFormat4CharGlyphMapList(pOutputBufferInfo, *(ushort*)ptr6, *(ushort*)(ptr6 + 2L / (long)sizeof(CMAP_TABLELOC)), puchKeepGlyphList, usGlyphCount, &ptr3, &num);
						if (num3 != 0)
						{
							goto IL_2D0;
						}
						ptr = (FORMAT4_SEGMENTS*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)((num + 1) * 8)));
						ptr2 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num * 2UL);
						if (ptr == null || ptr2 == null)
						{
							break;
						}
						CMAP_FORMAT4 cmap_FORMAT;
						ushort usnSegment;
						ushort snFormat4GlyphIdArray;
						<Module>.MS.Internal.TtfDelta.ComputeFormat4CmapData(&cmap_FORMAT, ptr, &usnSegment, ptr2, &snFormat4GlyphIdArray, ptr3, num);
						if ((int)(*(ref cmap_FORMAT + 2)) <= *(ref cmap_SUBHEADER_GEN + 4))
						{
							if (*(ushort*)ptr6 == 3)
							{
								*pOS2MinChr = *(ushort*)ptr3;
								*pOS2MaxChr = *(ushort*)((long)(num - 1) * 4L + ptr3 / 2);
							}
							uint num11;
							num3 = <Module>.MS.Internal.TtfDelta.WriteOutFormat4CmapData(pOutputBufferInfo, &cmap_FORMAT, ptr, ptr2, usnSegment, snFormat4GlyphIdArray, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, &num11);
						}
						<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
						<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
						<Module>.MS.Internal.TtfDelta.FreeFormat4CharCodes(ptr3);
						ptr = null;
						ptr2 = null;
						ptr3 = null;
					}
					else if (cmap_SUBHEADER_GEN == 12)
					{
						uint ulnGroups = 0;
						num3 = <Module>.MS.Internal.TtfDelta.ReadAllocFormat12CharGlyphMapList(pOutputBufferInfo, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, puchKeepGlyphList, usGlyphCount, &ptr4, &num2);
						if (num3 != 0)
						{
							goto IL_2D0;
						}
						FORMAT12_GROUPS* ptr7 = (FORMAT12_GROUPS*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num2 * 12);
						if (ptr7 == null)
						{
							goto IL_2C5;
						}
						CMAP_FORMAT12 cmap_FORMAT2;
						<Module>.MS.Internal.TtfDelta.ComputeFormat12CmapData(&cmap_FORMAT2, ptr7, &ulnGroups, ptr4, num2);
						if (*(ref cmap_FORMAT2 + 4) <= *(ref cmap_SUBHEADER_GEN + 4))
						{
							if (*(ushort*)ptr6 == 3)
							{
								*pOS2MinChr = (ushort)(*(int*)ptr4);
								*pOS2MaxChr = (ushort)((num2 - 1) * 8L)[ptr4 / 4];
							}
							uint num11;
							num3 = <Module>.MS.Internal.TtfDelta.WriteOutFormat12CmapData(pOutputBufferInfo, &cmap_FORMAT2, ptr7, ulnGroups, *(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)) + num4, &num11);
						}
						<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr7);
						<Module>.MS.Internal.TtfDelta.FreeFormat12CharCodes(ptr4);
						ptr4 = null;
					}
					uint num12 = (uint)(*(int*)(ptr6 + 4L / (long)sizeof(CMAP_TABLELOC)));
					uint num13 = num12;
					<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.RecordCmapOffset(&cmapoffsetrecordkeeper, num13, num13);
				}
				num8 += 1;
				if (num8 >= num6)
				{
					goto IL_2CD;
				}
			}
			num3 = 1005;
			goto IL_2D0;
			IL_2C5:
			num3 = 1005;
			goto IL_2D0;
			IL_2CD:
			if (num3 == 0)
			{
				goto IL_301;
			}
			IL_2D0:
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
			<Module>.MS.Internal.TtfDelta.FreeFormat4CharCodes(ptr3);
			if (num3 == 1007)
			{
				*(int*)pulNewOutOffset = num4;
				num3 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04EICJPCEA@cmap@), pulNewOutOffset);
				goto IL_31A;
			}
			goto IL_31A;
		}
		IL_301:
		uint num14;
		num3 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.CompressCmapSubTables(pOutputBufferInfo, ptr5, num6, num4, num7, num5, &num14);
		*(int*)pulNewOutOffset = num14 + num4;
		IL_31A:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
		<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FreeCmapOffsetArray(&cmapoffsetrecordkeeper);
		return num3;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000D528 File Offset: 0x0000C928
	[SecurityCritical]
	internal unsafe static short ModGlyfLocaAndHead(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphCount, uint* pCheckSumAdjustment, uint* pulNewOutOffset)
	{
		short num = 0;
		int num2 = (int)(usGlyphCount + 1);
		uint* ptr = (uint*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)num2 * 4L));
		if (ptr == null)
		{
			return 1005;
		}
		uint num3 = (uint)(usGlyphCount + 1);
		if (<Module>.MS.Internal.TtfDelta.GetLoca((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, ptr, num3) == null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return 1065;
		}
		HEAD head;
		uint num4 = <Module>.MS.Internal.TtfDelta.GetHead(pOutputBufferInfo, &head);
		if (num4 == null)
		{
			num = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04NEODDMOL@head@), pulNewOutOffset);
			if (num != 0)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return num;
			}
			num4 = <Module>.MS.Internal.TtfDelta.GetHead(pOutputBufferInfo, &head);
		}
		uint num5 = 0;
		uint num6 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LLIHEPK@glyf@));
		if (num6 == null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return 1031;
		}
		DIRECTORY directory;
		uint num7 = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04LLIHEPK@glyf@), &directory);
		if (num7 == null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return 1031;
		}
		if (*(ref directory + 8) == 0)
		{
			num = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, *(int*)pulNewOutOffset, pulNewOutOffset);
			if (num != 0)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return num;
			}
			*(ref directory + 8) = *(int*)pulNewOutOffset;
		}
		uint num8 = *(ref directory + 8);
		ushort num9 = 0;
		if (0 < usGlyphCount)
		{
			do
			{
				uint num10 = 0;
				long num11 = (long)((ulong)num9);
				if (num11[puchKeepGlyphList] != 0)
				{
					uint num12 = (uint)(*(int*)(num11 * 4L / (long)sizeof(uint) + ptr));
					uint num13 = (uint)(*(int*)((long)(num9 + 1) * 4L + ptr / 4));
					if (num12 < num13)
					{
						num10 = num13 - num12;
						if (num10 != null)
						{
							num = <Module>.MS.Internal.TtfDelta.CopyBlockOver(pOutputBufferInfo, pInputBufferInfo, num8 + num5, num12 + num6, num10);
							if (num != 0)
							{
								goto IL_189;
							}
						}
					}
				}
				*(int*)(num11 * 4L / (long)sizeof(uint) + ptr) = num5;
				num5 += num10;
				if ((num5 & 1) != null)
				{
					num = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, 0, num8 + num5);
					if (num != 0)
					{
						goto IL_189;
					}
					num5++;
				}
				num9 += 1;
			}
			while (num9 < usGlyphCount);
			if (num != 0)
			{
				goto IL_189;
			}
		}
		*(int*)pulNewOutOffset = *(int*)pulNewOutOffset + num5;
		(usGlyphCount * 4L)[ptr / 4] = num5;
		*(ref directory + 12) = num5;
		ushort num14;
		num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&directory), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num7, &num14);
		if (num == 0)
		{
			DIRECTORY directory2;
			uint num15 = <Module>.MS.Internal.TtfDelta.GetTTDirectory(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@), &directory2);
			if (num15 == null)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return 1035;
			}
			num = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, *(int*)pulNewOutOffset, pulNewOutOffset);
			if (num != 0)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return num;
			}
			*(ref directory2 + 8) = *(int*)pulNewOutOffset;
			ushort num16;
			if (num5 <= 131068)
			{
				num16 = 0;
				num9 = 0;
				ulong num17 = (ulong)(*(ref directory2 + 8));
				do
				{
					ushort usValue = (ushort)((uint)(num9 * 4L)[ptr / 4] >> 1);
					num = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, usValue, (uint)((ulong)num9 * 2UL + num17));
					if (num != 0)
					{
						break;
					}
					num9 += 1;
				}
				while (num9 <= usGlyphCount);
				num5 = (uint)((ulong)num3 * 2UL);
			}
			else
			{
				num16 = 1;
				uint num18;
				num = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, (byte*)ptr, (byte*)(&<Module>.MS.Internal.TtfDelta.LONG_CONTROL), *(ref directory2 + 8), &num18, (ushort)num2, 4);
				num5 = num18;
			}
			if (num == 0)
			{
				*(ref directory2 + 12) = num5;
				*(int*)pulNewOutOffset = *(int*)pulNewOutOffset + num5;
				num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&directory2), 16, (byte*)(&<Module>.MS.Internal.TtfDelta.DIRECTORY_CONTROL), num15, &num14);
				if (num == 0)
				{
					*(int*)pCheckSumAdjustment = *(ref head + 8);
					*(ref head + 8) = 0;
					*(ref head + 50) = (short)num16;
					num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&head), 54, (byte*)(&<Module>.MS.Internal.TtfDelta.HEAD_CONTROL), num4, &num14);
				}
			}
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return num;
		}
		IL_189:
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00008868 File Offset: 0x00007C68
	[SecurityCritical]
	internal unsafe static int UIntAdd(uint uAugend, uint uAddend, uint* puResult)
	{
		uint num = uAugend + uAddend;
		int result;
		if (num >= uAugend)
		{
			*puResult = num;
			result = 0;
		}
		else
		{
			*puResult = uint.MaxValue;
			result = -2147024362;
		}
		return result;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000888C File Offset: 0x00007C8C
	[SecurityCritical]
	internal unsafe static int ULongLongToULong(ulong ullOperand, uint* pulResult)
	{
		int result;
		if (ullOperand <= 4294967295UL)
		{
			*(int*)pulResult = (int)((uint)ullOperand);
			result = 0;
		}
		else
		{
			*(int*)pulResult = -1;
			result = -2147024362;
		}
		return result;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000088B4 File Offset: 0x00007CB4
	[SecurityCritical]
	internal unsafe static int ULongMult(uint ulMultiplicand, uint ulMultiplier, uint* pulResult)
	{
		return <Module>.MS.Internal.TtfDelta.ULongLongToULong(ulMultiplicand * ulMultiplier, pulResult);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000088CC File Offset: 0x00007CCC
	[SecurityCritical]
	internal unsafe static int ULongSub(uint ulMinuend, uint ulSubtrahend, uint* pulResult)
	{
		int result;
		if (ulMinuend >= ulSubtrahend)
		{
			*(int*)pulResult = ulMinuend - ulSubtrahend;
			result = 0;
		}
		else
		{
			*(int*)pulResult = -1;
			result = -2147024362;
		}
		return result;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000088F0 File Offset: 0x00007CF0
	[SecurityCritical]
	internal unsafe static short RecordGlyphOffset(glyphoffsetrecordkeeper* pKeeper, uint ulOldOffset, ImageDataBlock* pImageDataBlock)
	{
		uint num = (uint)(*(int*)(pKeeper + 8L / (long)sizeof(glyphoffsetrecordkeeper)));
		if (*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper)) >= (int)num)
		{
			void* ptr = <Module>.MS.Internal.TtfDelta.Mem_ReAlloc(*(long*)pKeeper, (ulong)(num + 100U) * 16UL);
			*(long*)pKeeper = ptr;
			if (ptr == null)
			{
				return 1005;
			}
			initblk((ulong)(*(int*)(pKeeper + 8L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL + (byte*)ptr, 0, 1600L);
			*(int*)(pKeeper + 8L / (long)sizeof(glyphoffsetrecordkeeper)) = *(int*)(pKeeper + 8L / (long)sizeof(glyphoffsetrecordkeeper)) + 100;
		}
		*((ulong)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL + (ulong)(*(long*)pKeeper)) = ulOldOffset;
		*(*(long*)pKeeper + (long)((ulong)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL) + 4L) = *(int*)pImageDataBlock;
		*(*(long*)pKeeper + (long)((ulong)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL) + 8L) = (short)(*(ushort*)(pImageDataBlock + 4L / (long)sizeof(ImageDataBlock)));
		*(*(long*)pKeeper + (long)((ulong)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL) + 10L) = (short)(*(ushort*)(pImageDataBlock + 6L / (long)sizeof(ImageDataBlock)));
		*(*(long*)pKeeper + (long)((ulong)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper))) * 16UL) + 12L) = (short)(*(ushort*)(pImageDataBlock + 8L / (long)sizeof(ImageDataBlock)));
		*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper)) = *(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper)) + 1;
		return 0;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000089C8 File Offset: 0x00007DC8
	[SecurityCritical]
	internal unsafe static uint LookupGlyphOffset(glyphoffsetrecordkeeper* pKeeper, uint ulOldOffset, ImageDataBlock* pImageDataBlock)
	{
		uint num = 0;
		uint num2 = (uint)(*(int*)(pKeeper + 12L / (long)sizeof(glyphoffsetrecordkeeper)));
		if (0U < num2)
		{
			long num3 = *(long*)pKeeper;
			while (ulOldOffset != *(num * 16UL + (ulong)num3))
			{
				num++;
				if (num >= num2)
				{
					return 0;
				}
			}
			long num4 = (long)(num * 16UL);
			*(int*)pImageDataBlock = *(num4 + num3 + 4L);
			*(short*)(pImageDataBlock + 4L / (long)sizeof(ImageDataBlock)) = (short)(*(num4 + *(long*)pKeeper + 8L));
			*(short*)(pImageDataBlock + 6L / (long)sizeof(ImageDataBlock)) = (short)(*(num4 + *(long*)pKeeper + 10L));
			*(short*)(pImageDataBlock + 8L / (long)sizeof(ImageDataBlock)) = (short)(*(num4 + *(long*)pKeeper + 12L));
			return 1;
		}
		return 0;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00008A38 File Offset: 0x00007E38
	[SecurityCritical]
	internal unsafe static short FixSbitSubTables(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulOffset, ushort usOldFirstGlyphIndex, ushort usOldLastGlyphIndex, ushort* pusNewFirstGlyphIndex, ushort* pusNewLastGlyphIndex, byte** ppuchIndexSubTable, uint* pulIndexSubTableSize, uint* pulTableSize, uint ulCurrAdditionalOffset, uint ulInitialOffset, byte* puchKeepGlyphList, ushort usGlyphListCount, uint ulImageDataOffset, uint* pulEBDTBytesWritten, byte* puchEBDTDestPtr, uint ulEBDTSrcOffset, glyphoffsetrecordkeeper* pKeeper)
	{
		int num = 1;
		INDEXSUBHEADER indexsubheader;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubheader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBHEADER_CONTROL), ulOffset, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		ushort num4 = indexsubheader;
		uint num5 = *(ref indexsubheader + 4);
		uint num6 = ulImageDataOffset;
		*(int*)pulEBDTBytesWritten = 0;
		*(int*)pulTableSize = 0;
		ImageDataBlock imageDataBlock;
		if (<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.LookupGlyphOffset(pKeeper, *(ref indexsubheader + 4), &imageDataBlock) == null)
		{
			imageDataBlock = ulImageDataOffset;
			*(ref imageDataBlock + 8) = indexsubheader;
			*(ref imageDataBlock + 6) = (short)(*(ref indexsubheader + 2));
			*(ref imageDataBlock + 4) = (short)usOldFirstGlyphIndex;
			num2 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.RecordGlyphOffset(pKeeper, num5, &imageDataBlock);
			if (num2 != 0)
			{
				return num2;
			}
		}
		else
		{
			if (*(ref imageDataBlock + 6) != *(ref indexsubheader + 2))
			{
				return 0;
			}
			num = 0;
			num6 = imageDataBlock;
		}
		uint num7 = ulCurrAdditionalOffset - ulInitialOffset;
		ushort num8 = usOldLastGlyphIndex - usOldFirstGlyphIndex + 1;
		int num9 = (int)num4;
		long num10 = (long)num;
		uint num12;
		uint num13;
		if (num9 != 1)
		{
			if (num9 != 2)
			{
				if (num9 != 3)
				{
					if (num9 != 4)
					{
						if (num9 != 5)
						{
							return 0;
						}
						ushort num11 = 0;
						INDEXSUBTABLE5 indexsubtable;
						num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtable), 24, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE5_CONTROL), ulOffset, &num3);
						if (num2 != 0)
						{
							return num2;
						}
						ulOffset += num3;
						*(ref indexsubtable + 4) = num6;
						num12 = 24;
						num13 = 0;
						uint num14 = 0;
						uint num15 = *(ref indexsubtable + 8);
						num8 = (ushort)(*(ref indexsubtable + 20));
						ushort num16 = 0;
						ushort num17 = 0;
						uint num18 = 0;
						if (0 < num8)
						{
							while (num17 < usOldLastGlyphIndex)
							{
								num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, &num17, ulOffset);
								if (num2 != 0)
								{
									return num2;
								}
								ulOffset = (uint)(ulOffset + 2UL);
								if (num17 < usGlyphListCount && num17[puchKeepGlyphList] != 0)
								{
									num11 = ((num11 == 0) ? num17 : num11);
									if (num12 + num7 + 2UL > (ulong)(*(int*)pulIndexSubTableSize))
									{
										return 1086;
									}
									cpblk(num12 + num7 + (ulong)(*(long*)ppuchIndexSubTable), ref num17, 2);
									num12 = (uint)(num12 + 2UL);
									if (num10 != 0L)
									{
										num2 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, num13 + (puchEBDTDestPtr + (ulong)(*(ref indexsubtable + 4))), num14 + num5 + ulEBDTSrcOffset, num15);
										if (num2 != 0)
										{
											return num2;
										}
									}
									num18++;
									num13 += num15;
									*pusNewLastGlyphIndex = num17;
								}
								num14 += num15;
								num16 += 1;
								if (num16 >= num8)
								{
									break;
								}
							}
							if (num18 != null)
							{
								*(ref indexsubtable + 20) = num18;
								cpblk(num7 + (ulong)(*(long*)ppuchIndexSubTable), ref indexsubtable, 24);
								*pusNewFirstGlyphIndex = num11;
								goto IL_86C;
							}
						}
						return 0;
					}
					else
					{
						ushort num19 = 0;
						INDEXSUBTABLE4 indexsubtable2;
						num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtable2), 12, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE4_CONTROL), ulOffset, &num3);
						if (num2 != 0)
						{
							return num2;
						}
						ulOffset += num3;
						*(ref indexsubtable2 + 4) = num6;
						num12 = 12;
						ushort num20 = 0;
						num8 = (ushort)(*(ref indexsubtable2 + 8));
						CODEOFFSETPAIR codeoffsetpair;
						num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&codeoffsetpair), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.CODEOFFSETPAIR_CONTROL), ulOffset, &num3);
						if (num2 != 0)
						{
							return num2;
						}
						ulOffset += num3;
						ushort num17 = codeoffsetpair;
						ushort num21 = *(ref codeoffsetpair + 2);
						ushort num16 = 0;
						uint num22 = 0;
						if (0 < num8)
						{
							while (num17 <= usOldLastGlyphIndex)
							{
								num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&codeoffsetpair), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.CODEOFFSETPAIR_CONTROL), ulOffset, &num3);
								if (num2 != 0)
								{
									return num2;
								}
								ulOffset += num3;
								ushort num23 = codeoffsetpair;
								ushort num24 = *(ref codeoffsetpair + 2);
								if (num17 < usGlyphListCount && num17[puchKeepGlyphList] != 0)
								{
									num19 = ((num19 == 0) ? num17 : num19);
									if (num12 + num7 + 8 > *(int*)pulIndexSubTableSize)
									{
										return 1086;
									}
									long num25 = (long)num7;
									cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num17, 2);
									num12 = (uint)(num12 + 2UL);
									cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num20, 2);
									num12 = (uint)(num12 + 2UL);
									ushort num26 = num24 - num21;
									if (num10 != 0L)
									{
										num2 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, *(ref indexsubtable2 + 4) + (puchEBDTDestPtr + (ulong)num20), num21 + num5 + ulEBDTSrcOffset, num26);
										if (num2 != 0)
										{
											return num2;
										}
									}
									num20 = num26 + num20;
									num22++;
									*pusNewLastGlyphIndex = num17;
								}
								num21 = num24;
								num17 = num23;
								num16 += 1;
								if (num16 >= num8)
								{
									break;
								}
							}
							if (num22 != null)
							{
								codeoffsetpair = 0;
								*(ref codeoffsetpair + 2) = (short)num20;
								long num25 = (long)num7;
								cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref codeoffsetpair, 4);
								num12 += 4;
								*(ref indexsubtable2 + 8) = num22;
								cpblk(*(long*)ppuchIndexSubTable + num25, ref indexsubtable2, 12);
								*pusNewFirstGlyphIndex = num19;
								num13 = num20;
								goto IL_86C;
							}
						}
						return 0;
					}
				}
				else
				{
					INDEXSUBTABLE3 indexsubtable3;
					num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtable3), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE3_CONTROL), ulOffset, &num3);
					if (num2 != 0)
					{
						return num2;
					}
					ulOffset += num3;
					*(ref indexsubtable3 + 4) = num6;
					if (num7 + 8 > *(int*)pulIndexSubTableSize)
					{
						return 1086;
					}
					long num25 = (long)num7;
					cpblk(*(long*)ppuchIndexSubTable + num25, ref indexsubtable3, 8);
					num12 = 8;
					ushort num20 = 0;
					ushort num21;
					num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, &num21, ulOffset);
					if (num2 != 0)
					{
						return num2;
					}
					ulOffset = (uint)(ulOffset + 2UL);
					ushort num16 = 0;
					if (0 < num8)
					{
						for (;;)
						{
							ushort num24;
							num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, &num24, ulOffset);
							if (num2 != 0)
							{
								return num2;
							}
							ulOffset = (uint)(ulOffset + 2UL);
							ushort num17 = num16 + usOldFirstGlyphIndex;
							if (num17 > *pusNewLastGlyphIndex)
							{
								goto IL_4B3;
							}
							if (num17 >= *pusNewFirstGlyphIndex)
							{
								if (num12 + num7 + 4UL > (ulong)(*(int*)pulIndexSubTableSize))
								{
									return 1086;
								}
								cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num20, 2);
								num12 = (uint)(num12 + 2UL);
								if (num17 < usGlyphListCount && num17[puchKeepGlyphList] != 0)
								{
									ushort num27 = num24 - num21;
									if (num10 != 0L)
									{
										num2 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, *(ref indexsubtable3 + 4) + (puchEBDTDestPtr + (ulong)num20), num21 + num5 + ulEBDTSrcOffset, num27);
										if (num2 != 0)
										{
											break;
										}
									}
									num20 = num27 + num20;
								}
							}
							num21 = num24;
							num16 += 1;
							if (num16 >= num8)
							{
								goto IL_4B3;
							}
						}
						return num2;
						IL_4B3:
						if (num20 != 0)
						{
							cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num20, 2);
							num12 = (uint)(num12 + 2UL);
							num13 = num20;
							goto IL_86C;
						}
						return 0;
					}
					return 0;
				}
			}
			else
			{
				ushort* ptr = null;
				INDEXSUBTABLE2 indexsubtable4;
				num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtable4), 20, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE2_CONTROL), ulOffset, &num3);
				if (num2 != 0)
				{
					return num2;
				}
				*(ref indexsubtable4 + 4) = num6;
				num13 = 0;
				uint num14 = 0;
				uint num28 = *(ref indexsubtable4 + 8);
				INDEXSUBTABLE5 indexsubtable5;
				cpblk(ref indexsubtable5, ref indexsubtable4, 20);
				indexsubtable5 = 5;
				*(ref indexsubtable5 + 20) = 0;
				ushort num29 = *pusNewFirstGlyphIndex;
				ushort num30 = num29;
				ushort num31 = *pusNewLastGlyphIndex;
				if (num30 <= num31)
				{
					do
					{
						if (num30 < usGlyphListCount && num30[puchKeepGlyphList] != 0)
						{
							*(ref indexsubtable5 + 20) = *(ref indexsubtable5 + 20) + 1;
						}
						num30 += 1;
					}
					while (num30 <= num31);
					if (*(ref indexsubtable5 + 20) != 0)
					{
						if (*(ref indexsubtable5 + 20) != (int)(num31 - num29 + 1))
						{
							ulong num32 = (ulong)(*(ref indexsubtable5 + 20));
							ptr = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num32 * 2UL);
							uint num33 = (uint)((num32 + 2UL) * 2UL + (ulong)(*(int*)pulIndexSubTableSize));
							*(int*)pulIndexSubTableSize = (int)num33;
							void* ptr2 = <Module>.MS.Internal.TtfDelta.Mem_ReAlloc(*(long*)ppuchIndexSubTable, (ulong)num33);
							*(long*)ppuchIndexSubTable = ptr2;
							if (ptr == null || ptr2 == null)
							{
								<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
								return 1005;
							}
						}
						*(ref indexsubtable5 + 20) = 0;
						ushort num16 = usOldFirstGlyphIndex;
						if (usOldFirstGlyphIndex <= usOldLastGlyphIndex)
						{
							for (;;)
							{
								if (num16 < usGlyphListCount && num16[puchKeepGlyphList] != 0)
								{
									if (ptr != null)
									{
										(*(ref indexsubtable5 + 20) * 2L)[ptr / 2] = num16;
										*(ref indexsubtable5 + 20) = *(ref indexsubtable5 + 20) + 1;
									}
									if (num10 != 0L)
									{
										num2 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, num13 + (puchEBDTDestPtr + (ulong)(*(ref indexsubtable4 + 4))), num14 + num5 + ulEBDTSrcOffset, num28);
										if (num2 != 0)
										{
											break;
										}
									}
									num13 += num28;
								}
								num14 += num28;
								num16 += 1;
								if (num16 > usOldLastGlyphIndex)
								{
									goto IL_650;
								}
							}
							<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
							return num2;
							IL_650:
							if (num13 != null)
							{
								if (ptr != null)
								{
									ulong num34 = (ulong)(*(ref indexsubtable5 + 20));
									num12 = (uint)((num34 + 12UL) * 2UL);
									if (num12 + num7 > *(int*)pulIndexSubTableSize)
									{
										<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
										return 1086;
									}
									long num25 = (long)num7;
									cpblk(*(long*)ppuchIndexSubTable + num25, ref indexsubtable5, 24);
									cpblk(num25 + *(long*)ppuchIndexSubTable + 24L, ptr, num34 * 2UL);
									<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
									goto IL_86C;
								}
								else
								{
									num12 = 20;
									if (num7 + 20 > *(int*)pulIndexSubTableSize)
									{
										return 1086;
									}
									cpblk(num7 + (ulong)(*(long*)ppuchIndexSubTable), ref indexsubtable4, 20L);
									goto IL_86C;
								}
							}
						}
						<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
						return 0;
					}
				}
				return 0;
			}
		}
		else
		{
			INDEXSUBTABLE1 indexsubtable6;
			num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtable6), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE1_CONTROL), ulOffset, &num3);
			if (num2 != 0)
			{
				return num2;
			}
			ulOffset += num3;
			*(ref indexsubtable6 + 4) = num6;
			uint num35;
			if (<Module>.MS.Internal.TtfDelta.UIntAdd(num7, 8U, (uint*)(&num35)) < 0 || num35 > *(int*)pulIndexSubTableSize)
			{
				return 1086;
			}
			long num25 = (long)num7;
			cpblk(*(long*)ppuchIndexSubTable + num25, ref indexsubtable6, 8);
			num12 = 8;
			num13 = 0;
			uint num14;
			num2 = <Module>.MS.Internal.TtfDelta.ReadLong(pOutputBufferInfo, &num14, ulOffset);
			if (num2 != 0)
			{
				return num2;
			}
			ulOffset = (uint)(ulOffset + 4UL);
			ushort num16 = 0;
			if (0 < num8)
			{
				for (;;)
				{
					uint num36;
					num2 = <Module>.MS.Internal.TtfDelta.ReadLong(pOutputBufferInfo, &num36, ulOffset);
					if (num2 != 0)
					{
						return num2;
					}
					ulOffset = (uint)(ulOffset + 4UL);
					ushort num17 = num16 + usOldFirstGlyphIndex;
					if (num17 > *pusNewLastGlyphIndex)
					{
						goto IL_83C;
					}
					if (num17 >= *pusNewFirstGlyphIndex)
					{
						if (<Module>.MS.Internal.TtfDelta.UIntAdd(num7, num12, (uint*)(&num35)) < 0 || <Module>.MS.Internal.TtfDelta.UIntAdd(num35, 8U, (uint*)(&num35)) < 0 || num35 > *(int*)pulIndexSubTableSize)
						{
							return 1086;
						}
						cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num13, 4);
						num12 = (uint)(num12 + 4UL);
						if (num17 < usGlyphListCount && num17[puchKeepGlyphList] != 0)
						{
							if (num36 < num14)
							{
								return 1086;
							}
							uint num37 = num36 - num14;
							if (num10 != 0L)
							{
								num2 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, *(ref indexsubtable6 + 4) + (puchEBDTDestPtr + num13), num14 + num5 + ulEBDTSrcOffset, num37);
								if (num2 != 0)
								{
									break;
								}
							}
							num13 += num37;
						}
					}
					num14 = num36;
					num16 += 1;
					if (num16 >= num8)
					{
						goto IL_83C;
					}
				}
				return num2;
				IL_83C:
				if (num13 == null)
				{
					return 0;
				}
				if (num12 + num7 + 4UL > (ulong)(*(int*)pulIndexSubTableSize))
				{
					return 1086;
				}
				cpblk(num12 + (ulong)(*(long*)ppuchIndexSubTable) + (ulong)num25, ref num13, 4);
				num12 = (uint)(num12 + 4UL);
				goto IL_86C;
			}
			return 0;
		}
		IL_86C:
		num12 = <Module>.MS.Internal.TtfDelta.RoundToLongWord(num12);
		if (num != 0)
		{
			*(int*)pulEBDTBytesWritten = num13;
		}
		*(int*)pulTableSize = num12;
		return 0;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000092D0 File Offset: 0x000086D0
	[SecurityCritical]
	internal unsafe static uint FixSbitSubTableFormat1(ushort usFirstIndex, ushort* pusLastIndex, byte* puchIndexSubTable, ushort usImageFormat, uint ulCurrAdditionalOffset, uint ulInitialOffset, uint* pulSourceOffset, uint* pulNewImageDataOffset)
	{
		ushort num = 0;
		ulong num2 = ulCurrAdditionalOffset - ulInitialOffset;
		INDEXSUBTABLE3 indexsubtable;
		*(ref indexsubtable + 2) = (short)usImageFormat;
		indexsubtable = 3;
		*(ref indexsubtable + 4) = *(int*)pulNewImageDataOffset;
		long num3 = (long)num2;
		cpblk(num3 + puchIndexSubTable, ref indexsubtable, 8);
		uint num4 = 8;
		uint num5 = (uint)(*(int*)pulSourceOffset);
		uint num6 = (uint)num5[puchIndexSubTable / 4];
		uint num7 = num6;
		uint num8 = num6 - num7;
		*(int*)pulSourceOffset = (int)((uint)((ulong)num5 + 4UL));
		ushort num9 = usFirstIndex;
		if (usFirstIndex <= *pusLastIndex)
		{
			do
			{
				num = num8;
				num5 = (uint)(*(int*)pulSourceOffset);
				num8 = num5[puchIndexSubTable / 4] - num7;
				if (num8 > 65535)
				{
					break;
				}
				*(int*)pulSourceOffset = (int)((uint)((ulong)num5 + 4UL));
				cpblk(num4 + (ulong)num3 + puchIndexSubTable, ref num, 2);
				num4 = (uint)(num4 + 2UL);
				num9 += 1;
			}
			while (num9 <= *pusLastIndex);
		}
		if (num9 > *pusLastIndex)
		{
			num = num8;
		}
		else if (num9 - usFirstIndex < 4)
		{
			return 0;
		}
		*(int*)pulNewImageDataOffset = *(int*)pulNewImageDataOffset + (int)num;
		if (num9 > *pusLastIndex)
		{
			cpblk(num4 + (ulong)num3 + puchIndexSubTable, ref num, 2);
			num4 = (uint)(num4 + 2UL);
		}
		if ((num4 & 3) != null)
		{
			num4 = (uint)(num4 + 2UL);
		}
		*pusLastIndex = num9 - 1;
		return num4;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000093B8 File Offset: 0x000087B8
	[SecurityCritical]
	internal unsafe static short FixSbitSubTableArray(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint ulOffset, SubTablePointers* pSubTablePointers, byte* puchKeepGlyphList, ushort usGlyphListCount, uint* pulNewImageDataOffset, byte* puchEBDTDestPtr, uint ulEBDTSrcOffset, glyphoffsetrecordkeeper* pKeeper, uint ulEBLCEndOffset)
	{
		uint num = 0;
		uint num2 = ulOffset;
		uint num3 = (uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLEARRAY_CONTROL));
		uint num4 = (uint)(*(int*)pSubTablePointers);
		uint num5 = num4 * num3;
		uint num6 = num4;
		uint num7 = num6;
		uint num8 = num5;
		uint num9 = 0;
		if (0 < num6)
		{
			for (;;)
			{
				INDEXSUBTABLEARRAY indexsubtablearray;
				ushort num11;
				short num10 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&indexsubtablearray), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLEARRAY_CONTROL), ulOffset, &num11);
				if (num10 != 0)
				{
					return num10;
				}
				ulOffset += num11;
				uint num12 = *(ref indexsubtablearray + 4) + num2;
				if (num12 >= ulEBLCEndOffset)
				{
					return 1086;
				}
				ushort num13 = indexsubtablearray;
				ushort num14 = *(ref indexsubtablearray + 2);
				if (indexsubtablearray < usGlyphListCount)
				{
					if (*(ref indexsubtablearray + 2) >= usGlyphListCount)
					{
						num14 = usGlyphListCount - 1;
					}
					if (indexsubtablearray[puchKeepGlyphList] == 0)
					{
						while (num13 < num14)
						{
							num13 += 1;
							if (num13[puchKeepGlyphList] != 0)
							{
								break;
							}
						}
					}
					if (num14[puchKeepGlyphList] == 0)
					{
						while (num14 > num13)
						{
							num14 += ushort.MaxValue;
							if (num14[puchKeepGlyphList] != 0)
							{
								break;
							}
						}
					}
					if (num13 != num14 || num14[puchKeepGlyphList] != 0)
					{
						uint num15 = (uint)(*(int*)pulNewImageDataOffset);
						uint num16 = num15;
						SubTablePointers* ptr = pSubTablePointers + 24L / (long)sizeof(SubTablePointers);
						uint num17;
						uint num18;
						num10 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FixSbitSubTables(pInputBufferInfo, pOutputBufferInfo, num12, indexsubtablearray, *(ref indexsubtablearray + 2), &num13, &num14, (byte**)ptr, (uint*)(pSubTablePointers + 16L / (long)sizeof(SubTablePointers)), &num17, num8, num5, puchKeepGlyphList, usGlyphListCount, num15, &num18, puchEBDTDestPtr, ulEBDTSrcOffset, pKeeper);
						if (num10 != 0)
						{
							return num10;
						}
						if (num17 > 0)
						{
							*(int*)pulNewImageDataOffset = *(int*)pulNewImageDataOffset + num18;
							long num19 = (long)num5;
							if (*(num8 - (ulong)num19 + (ulong)(*(long*)ptr)) != 1)
							{
								indexsubtablearray = num13;
								*(ref indexsubtablearray + 2) = (short)num14;
								*(ref indexsubtablearray + 4) = num8;
								cpblk(num * 8UL + (ulong)(*(long*)(pSubTablePointers + 8L / (long)sizeof(SubTablePointers))), ref indexsubtablearray, 8);
								num8 += num17;
								num++;
							}
							else
							{
								uint num20 = num8 - num5 + 8;
								ushort num21 = num13;
								ushort num22 = num14;
								for (;;)
								{
									long num23 = *(long*)ptr;
									ushort usFirstIndex = num13;
									long num24 = num23;
									num17 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FixSbitSubTableFormat1(usFirstIndex, &num14, num24, *(num24 + (long)(num8 - (ulong)num19) + 2L), num8, num5, &num20, &num16);
									if (num17 == null)
									{
										return 1000;
									}
									indexsubtablearray = num21;
									*(ref indexsubtablearray + 2) = (short)num22;
									*(ref indexsubtablearray + 4) = num8;
									cpblk(num * 8UL + (ulong)(*(long*)(pSubTablePointers + 8L / (long)sizeof(SubTablePointers))), ref indexsubtablearray, 8);
									num8 += num17;
									num++;
									num6++;
									if (num14 == num22)
									{
										break;
									}
									uint num25;
									if (<Module>.MS.Internal.TtfDelta.ULongMult(num6, 8, &num25) < 0)
									{
										return 1005;
									}
									void* ptr2 = <Module>.MS.Internal.TtfDelta.Mem_ReAlloc(*(long*)(pSubTablePointers + 8L / (long)sizeof(SubTablePointers)), num25);
									*(long*)(pSubTablePointers + 8L / (long)sizeof(SubTablePointers)) = ptr2;
									if (ptr2 == null)
									{
										return 1005;
									}
									num13 = num14;
									num14 = num22;
								}
								if (num16 != *(int*)pulNewImageDataOffset)
								{
									return 1000;
								}
							}
						}
					}
				}
				num9++;
				if (num9 >= num7)
				{
					goto IL_245;
				}
			}
			return 1005;
		}
		IL_245:
		int num26 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLEARRAY_CONTROL)) * (num - num7);
		if (0 < num)
		{
			SubTablePointers* ptr3 = pSubTablePointers + 8L / (long)sizeof(SubTablePointers);
			long num27 = 0L;
			uint num28 = num;
			do
			{
				*(*(long*)ptr3 + num27 + 4L) += num26;
				num27 += 8L;
				num28 += -1;
			}
			while (num28 > 0);
		}
		*(int*)(pSubTablePointers + 36L / (long)sizeof(SubTablePointers)) = num8;
		*(int*)(pSubTablePointers + 40L / (long)sizeof(SubTablePointers)) = num;
		*(int*)pSubTablePointers = num;
		return 0;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00009660 File Offset: 0x00008A60
	[SecurityCritical]
	internal unsafe static short WriteIndexSubTables(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, INDEXSUBTABLEARRAY* pIndexSubTableArray, byte* puchIndexSubTables, ushort usnIndexSubTables, uint ulOffset, uint ulIndexSubTableArrayLength, uint* pulBytesWritten)
	{
		uint num = ulOffset;
		ushort num2 = 0;
		if (0 < usnIndexSubTables)
		{
			long num3 = (long)ulIndexSubTableArrayLength;
			short num7;
			for (;;)
			{
				INDEXSUBTABLEARRAY* ptr = num2 * 8L + pIndexSubTableArray / sizeof(INDEXSUBTABLEARRAY);
				byte* ptr2 = *(int*)(ptr + 4L / (long)sizeof(INDEXSUBTABLEARRAY)) - num3 + puchIndexSubTables;
				*(int*)(ptr + 4L / (long)sizeof(INDEXSUBTABLEARRAY)) = ulOffset - num + ulIndexSubTableArrayLength;
				int num4 = (int)(*(ushort*)ptr2);
				if (num4 != 1)
				{
					if (num4 != 2)
					{
						if (num4 != 3)
						{
							if (num4 != 4)
							{
								if (num4 != 5)
								{
									return 1086;
								}
								ushort num6;
								short num5 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, ptr2, 24, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE5_CONTROL), ulOffset, &num6);
								if (num5 != 0)
								{
									return num5;
								}
								ulOffset += num6;
								uint num8;
								num7 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, ptr2 + 24L, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), ulOffset, &num8, (ushort)(*(int*)(ptr2 + 20L)), 2);
								if (num7 != 0)
								{
									break;
								}
								ulOffset += num8;
							}
							else
							{
								ushort num6;
								short num9 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, ptr2, 12, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE4_CONTROL), ulOffset, &num6);
								if (num9 != 0)
								{
									return num9;
								}
								ulOffset += num6;
								uint num8;
								num7 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, ptr2 + 12L, (byte*)(&<Module>.MS.Internal.TtfDelta.CODEOFFSETPAIR_CONTROL), ulOffset, &num8, (ushort)(*(int*)(ptr2 + 8L) + 1), 4);
								if (num7 != 0)
								{
									return num7;
								}
								ulOffset += num8;
							}
						}
						else
						{
							ushort usItemCount = *(ushort*)(ptr + 2L / (long)sizeof(INDEXSUBTABLEARRAY)) - *(ushort*)ptr + 2;
							ushort num6;
							short num10 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, ptr2, 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE3_CONTROL), ulOffset, &num6);
							if (num10 != 0)
							{
								return num10;
							}
							ulOffset += num6;
							uint num8;
							num7 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, ptr2 + 8L, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), ulOffset, &num8, usItemCount, 2);
							if (num7 != 0)
							{
								return num7;
							}
							ulOffset += num8;
						}
					}
					else
					{
						ushort num6;
						num7 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, ptr2, 20, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE2_CONTROL), ulOffset, &num6);
						if (num7 != 0)
						{
							return num7;
						}
						ulOffset += num6;
					}
				}
				else
				{
					ushort usItemCount = *(ushort*)(ptr + 2L / (long)sizeof(INDEXSUBTABLEARRAY)) - *(ushort*)ptr + 2;
					ushort num6;
					short num11 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, ptr2, 8, (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLE1_CONTROL), ulOffset, &num6);
					if (num11 != 0)
					{
						return num11;
					}
					ulOffset += num6;
					uint num8;
					num7 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, ptr2 + 8L, (byte*)(&<Module>.MS.Internal.TtfDelta.LONG_CONTROL), ulOffset, &num8, usItemCount, 4);
					if (num7 != 0)
					{
						return num7;
					}
					ulOffset += num8;
				}
				short num12 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, ulOffset, &ulOffset);
				if (num12 != 0)
				{
					return num12;
				}
				num2 += 1;
				if (num2 >= usnIndexSubTables)
				{
					goto IL_1FC;
				}
			}
			return num7;
		}
		IL_1FC:
		*(int*)pulBytesWritten = ulOffset - num;
		return 0;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00009874 File Offset: 0x00008C74
	[SecurityCritical]
	internal unsafe static void Cleanup_SubTablePointers(SubTablePointers* pSubTablePointers, uint ulNumSizes)
	{
		if (pSubTablePointers != null)
		{
			ushort num = 0;
			if (0 < ulNumSizes)
			{
				do
				{
					SubTablePointers* ptr = num * 80L + pSubTablePointers / sizeof(SubTablePointers);
					ulong num2 = (ulong)(*(long*)(ptr + 24L / (long)sizeof(SubTablePointers)));
					if (num2 != 0UL)
					{
						<Module>.MS.Internal.TtfDelta.Mem_Free(num2);
					}
					ulong num3 = (ulong)(*(long*)(ptr + 8L / (long)sizeof(SubTablePointers)));
					if (num3 != 0UL)
					{
						<Module>.MS.Internal.TtfDelta.Mem_Free(num3);
					}
					num += 1;
				}
				while (num < ulNumSizes);
			}
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)pSubTablePointers);
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000D7C8 File Offset: 0x0000CBC8
	[SecurityCritical]
	internal unsafe static short ModSbit(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, uint* pulNewOutOffset)
	{
		uint num = 0;
		uint num2 = 0;
		uint num3 = 0;
		uint num4 = 0;
		uint num5 = 0;
		uint ulNewLength = 0;
		uint num6 = 0;
		SubTablePointers* ptr = null;
		byte* ptr2 = null;
		short num7 = 0;
		glyphoffsetrecordkeeper pv = 0L;
		ushort num8 = 0;
		uint num17;
		for (;;)
		{
			sbyte* ptr3;
			sbyte* ptr4;
			if (num8 == 0)
			{
				sbyte* szDirTag = (sbyte*)(&<Module>.??_C@_04CNHNMDEC@EBSC@);
				ptr3 = (sbyte*)(&<Module>.??_C@_04DBJHPCCB@EBDT@);
				ptr4 = (sbyte*)(&<Module>.??_C@_04DKAHCHAP@EBLC@);
				num5 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DBJHPCCB@EBDT@));
				num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DKAHCHAP@EBLC@));
				if (num2 != null && num5 != null)
				{
					goto IL_122;
				}
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04DKAHCHAP@EBLC@));
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04DBJHPCCB@EBDT@));
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04CNHNMDEC@EBSC@));
			}
			else
			{
				<Module>.MS.Internal.TtfDelta.Cleanup_SubTablePointers(ptr, num6);
				<Module>.MS.Internal.TtfDelta.Mem_Free(pv);
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
				num6 = 0;
				ptr = null;
				pv = 0L;
				ptr2 = null;
				ptr3 = (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@);
				sbyte* szDirTag = (sbyte*)(&<Module>.??_C@_04JFNCAMCC@bsca@);
				ptr4 = (sbyte*)(&<Module>.??_C@_04KGIENAAN@bloc@);
				uint num9 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@));
				uint num10 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04KGIENAAN@bloc@));
				if (num10 == null || num9 == null || ((num10 == num2 || num9 == num5) && (num == null || num4 == null)))
				{
					goto IL_5DA;
				}
				if (num10 != num2 && num9 != num5)
				{
					num5 = num9;
					num2 = num10;
					goto IL_122;
				}
				goto IL_5B9;
			}
			IL_559:
			num8 += 1;
			if (num8 < 2)
			{
				continue;
			}
			goto IL_5FB;
			IL_122:
			num7 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)ptr4, pulNewOutOffset);
			if (num7 != 0)
			{
				goto IL_5FB;
			}
			num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)ptr4);
			uint ulEBLCEndOffset = <Module>.MS.Internal.TtfDelta.TTTableLength(pOutputBufferInfo, (sbyte*)ptr4) + num;
			uint num11 = 0;
			pv = 0L;
			*(ref pv + 8) = 0;
			*(ref pv + 12) = 0;
			ptr2 = (byte*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(<Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)ptr3));
			if (ptr2 == null)
			{
				goto IL_5B1;
			}
			ushort num12;
			num7 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, ptr2, 4, (byte*)(&<Module>.MS.Internal.TtfDelta.EBDTHEADERNOXLATENOPAD_CONTROL), num5, &num12);
			if (num7 != 0)
			{
				goto IL_5FB;
			}
			uint num13 = num12;
			EBLCHEADER eblcheader;
			num7 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&eblcheader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.EBLCHEADER_CONTROL), num, &num12);
			if (num7 != 0)
			{
				goto IL_5FB;
			}
			uint num14 = num12 + num;
			num6 = *(ref eblcheader + 4);
			ushort num15 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLEARRAY_CONTROL));
			uint num16;
			if (<Module>.MS.Internal.TtfDelta.ULongMult(num6, 80, &num16) < 0)
			{
				goto IL_5A9;
			}
			ptr = (SubTablePointers*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num16);
			if (ptr != null)
			{
				num17 = 0;
				if (0 < num6)
				{
					SubTablePointers* ptr5 = ptr + 36L / (long)sizeof(SubTablePointers);
					do
					{
						SubTablePointers* ptr6 = num17 * 80L + ptr / sizeof(SubTablePointers);
						num7 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(ptr6 + 32L / (long)sizeof(SubTablePointers)), 48, (byte*)(&<Module>.MS.Internal.TtfDelta.BITMAPSIZETABLE_CONTROL), num14, &num12);
						if (num7 != 0)
						{
							goto IL_5FB;
						}
						num14 += num12;
						ushort num18 = *(ushort*)(ptr5 + 36L / (long)sizeof(SubTablePointers));
						ushort num19 = *(ushort*)(ptr5 + 38L / (long)sizeof(SubTablePointers));
						if (num18 >= usGlyphListCount)
						{
							*(short*)(ptr5 + 36L / (long)sizeof(SubTablePointers)) = 0;
							*(short*)(ptr5 + 38L / (long)sizeof(SubTablePointers)) = 0;
							*(long*)(ptr5 - 12L / (long)sizeof(SubTablePointers)) = 0L;
							*(long*)(ptr5 - 28L / (long)sizeof(SubTablePointers)) = 0L;
						}
						else
						{
							if (num19 >= usGlyphListCount)
							{
								num19 = usGlyphListCount - 1;
							}
							if (num18[puchKeepGlyphList] == 0)
							{
								while (num18 < num19)
								{
									num18 += 1;
									if (num18[puchKeepGlyphList] != 0)
									{
										break;
									}
								}
							}
							if (num19[puchKeepGlyphList] == 0)
							{
								while (num19 > num18)
								{
									num19 += ushort.MaxValue;
									if (num19[puchKeepGlyphList] != 0)
									{
										break;
									}
								}
							}
							if (num18 == num19 && num19[puchKeepGlyphList] == 0)
							{
								*(short*)(ptr5 + 36L / (long)sizeof(SubTablePointers)) = 0;
								*(short*)(ptr5 + 38L / (long)sizeof(SubTablePointers)) = 0;
								*(long*)(ptr5 - 12L / (long)sizeof(SubTablePointers)) = 0L;
								*(long*)(ptr5 - 28L / (long)sizeof(SubTablePointers)) = 0L;
							}
							else
							{
								*(short*)(ptr5 + 36L / (long)sizeof(SubTablePointers)) = (short)num18;
								*(short*)(ptr5 + 38L / (long)sizeof(SubTablePointers)) = (short)num19;
								uint num20 = *(int*)(ptr5 + 4L / (long)sizeof(SubTablePointers));
								uint ulMultiplicand = *(int*)ptr5;
								uint num21 = *(int*)(ptr5 - 4L / (long)sizeof(SubTablePointers));
								uint num22;
								uint num23;
								if (<Module>.MS.Internal.TtfDelta.ULongMult(num20, num15, &num22) < 0 || <Module>.MS.Internal.TtfDelta.ULongMult(ulMultiplicand, 2, &num23) < 0 || <Module>.MS.Internal.TtfDelta.ULongSub(num23, num22, &num23) < 0 || <Module>.MS.Internal.TtfDelta.ULongMult(num23, 2, &num23) < 0)
								{
									goto IL_599;
								}
								*(int*)(ptr5 - 20L / (long)sizeof(SubTablePointers)) = num23;
								void* ptr7 = <Module>.MS.Internal.TtfDelta.Mem_Alloc(num23);
								*(long*)(ptr5 - 12L / (long)sizeof(SubTablePointers)) = ptr7;
								if (ptr7 == null)
								{
									goto IL_591;
								}
								*(int*)(ptr5 - 36L / (long)sizeof(SubTablePointers)) = num20;
								void* ptr8 = <Module>.MS.Internal.TtfDelta.Mem_Alloc(num22);
								*(long*)(ptr5 - 28L / (long)sizeof(SubTablePointers)) = ptr8;
								if (ptr8 == null)
								{
									goto IL_56C;
								}
								if (<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FixSbitSubTableArray(pInputBufferInfo, pOutputBufferInfo, num21 + num, ptr6, puchKeepGlyphList, usGlyphListCount, &num13, ptr2, num5, &pv, ulEBLCEndOffset) == 0 && *(int*)(ptr5 - 36L / (long)sizeof(SubTablePointers)) != 0)
								{
									num11++;
								}
								else
								{
									*(short*)(ptr5 + 36L / (long)sizeof(SubTablePointers)) = 0;
									*(short*)(ptr5 + 38L / (long)sizeof(SubTablePointers)) = 0;
								}
							}
						}
						num17++;
						ptr5 += 80L / (long)sizeof(SubTablePointers);
					}
					while (num17 < num6);
					if (num11 != null)
					{
						*(ref eblcheader + 4) = num11;
						ushort num24;
						num7 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&eblcheader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.EBLCHEADER_CONTROL), num, &num24);
						if (num7 != 0)
						{
							goto IL_5FB;
						}
						num14 = num24 + num;
						ushort num25 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.BITMAPSIZETABLE_CONTROL));
						num3 = num25 * num11 + num24;
						num17 = 0;
						SubTablePointers* ptr9 = ptr + 32L / (long)sizeof(SubTablePointers);
						do
						{
							if (*(ushort*)(ptr9 + 42L / (long)sizeof(SubTablePointers)) != 0)
							{
								uint num26 = num3;
								*(int*)ptr9 = num3;
								uint num20 = *(int*)(ptr9 + 8L / (long)sizeof(SubTablePointers));
								uint num22 = num15 * num20;
								uint num27;
								num7 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.WriteIndexSubTables(pOutputBufferInfo, *(long*)(ptr9 - 24L / (long)sizeof(SubTablePointers)), *(long*)(ptr9 - 8L / (long)sizeof(SubTablePointers)), (ushort)(*(int*)(ptr9 - 32L / (long)sizeof(SubTablePointers))), num22 + num3 + num, num22, &num27);
								if (num7 != 0)
								{
									goto IL_5FB;
								}
								num3 += num27;
								*(int*)(ptr9 + 4L / (long)sizeof(SubTablePointers)) = num27;
								num7 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, *(long*)(ptr9 - 24L / (long)sizeof(SubTablePointers)), (byte*)(&<Module>.MS.Internal.TtfDelta.INDEXSUBTABLEARRAY_CONTROL), num26 + num, &num27, num20, 8);
								if (num7 != 0)
								{
									goto IL_5FB;
								}
								num3 += num27;
								*(int*)(ptr9 + 4L / (long)sizeof(SubTablePointers)) = *(int*)(ptr9 + 4L / (long)sizeof(SubTablePointers)) + num27;
								num7 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(ptr + num17 * 80UL / (ulong)sizeof(SubTablePointers) + 32L / (long)sizeof(SubTablePointers)), 48, (byte*)(&<Module>.MS.Internal.TtfDelta.BITMAPSIZETABLE_CONTROL), num14, &num24);
								if (num7 != 0)
								{
									goto IL_5FB;
								}
								num14 += num24;
							}
							num17++;
							ptr9 += 80L / (long)sizeof(SubTablePointers);
						}
						while (num17 < num6);
						*(int*)pulNewOutOffset = num3 + num;
						num7 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, num3 + num, &num4);
						if (num7 != 0)
						{
							goto IL_5FB;
						}
						num7 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, ptr2, num4, num13);
						if (num7 != 0)
						{
							goto IL_559;
						}
						ulNewLength = num13;
						*(int*)pulNewOutOffset = num13 + num4;
						num7 = <Module>.MS.Internal.TtfDelta.UpdateDirEntryAll(pOutputBufferInfo, (sbyte*)ptr3, num13, num4);
						if (num7 == 0)
						{
							num7 = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)ptr4, num3);
							goto IL_559;
						}
						goto IL_559;
					}
				}
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)ptr4);
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)ptr3);
				sbyte* szDirTag;
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)szDirTag);
				num = 0;
				num4 = 0;
				goto IL_559;
			}
			goto IL_5A1;
		}
		IL_56C:
		SubTablePointers* ptr10 = ptr + num17 * 80UL / (ulong)sizeof(SubTablePointers) + 24L / (long)sizeof(SubTablePointers);
		<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)ptr10);
		*(long*)ptr10 = 0L;
		num7 = 1005;
		goto IL_5FB;
		IL_591:
		num7 = 1005;
		goto IL_5FB;
		IL_599:
		num7 = 1005;
		goto IL_5FB;
		IL_5A1:
		num7 = 1005;
		goto IL_5FB;
		IL_5A9:
		num7 = 1006;
		goto IL_5FB;
		IL_5B1:
		num7 = 1005;
		goto IL_5FB;
		IL_5B9:
		<Module>.MS.Internal.TtfDelta.UpdateDirEntryAll(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04KGIENAAN@bloc@), num3, num);
		<Module>.MS.Internal.TtfDelta.UpdateDirEntryAll(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@), ulNewLength, num4);
		goto IL_5FB;
		IL_5DA:
		<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04KGIENAAN@bloc@));
		<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04GMCNFBHO@bdat@));
		<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04JFNCAMCC@bsca@));
		IL_5FB:
		<Module>.MS.Internal.TtfDelta.Cleanup_SubTablePointers(ptr, num6);
		<Module>.MS.Internal.TtfDelta.Mem_Free(pv);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
		return num7;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000DDEC File Offset: 0x0000D1EC
	[SecurityCritical]
	internal unsafe static short ModXmtxXhea(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort usDttfGlyphIndexCount, ushort usMaxGlyphIndexUsed, int isHmtx, uint* pulNewOutOffset)
	{
		sbyte* ptr;
		XHEA xhea;
		uint num;
		uint num2;
		short num3;
		if (isHmtx != 0)
		{
			ptr = (sbyte*)(&<Module>.??_C@_04ONMNCIMC@hmtx@);
			num = <Module>.MS.Internal.TtfDelta.GetHHea(pOutputBufferInfo, (HHEA*)(&xhea));
			if (num == null)
			{
				if (<Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04FMPHLIKP@hhea@), pulNewOutOffset) != 0)
				{
					return 1063;
				}
				num = <Module>.MS.Internal.TtfDelta.GetHHea(pOutputBufferInfo, (HHEA*)(&xhea));
				if (num == null)
				{
					return 1033;
				}
			}
		}
		else
		{
			ptr = (sbyte*)(&<Module>.??_C@_04DCBNABCB@vmtx@);
			num = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDCHJBEM@vhea@));
			num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DCBNABCB@vmtx@));
			if (num != null && num2 == null)
			{
				<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDCHJBEM@vhea@));
				return 0;
			}
			num = <Module>.MS.Internal.TtfDelta.GetVHea(pOutputBufferInfo, (VHEA*)(&xhea));
			if (num == null)
			{
				num3 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDCHJBEM@vhea@), pulNewOutOffset);
				if (num3 != 0)
				{
					return (num3 == 1006) ? 0 : num3;
				}
				num = <Module>.MS.Internal.TtfDelta.GetVHea(pOutputBufferInfo, (VHEA*)(&xhea));
				if (num == null)
				{
					return 1040;
				}
			}
		}
		num3 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, ptr, pulNewOutOffset);
		if (num3 != 0)
		{
			return num3;
		}
		num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, ptr);
		if (*(ref xhea + 34) == 0 || *(ref xhea + 34) > usGlyphListCount)
		{
			return 1072;
		}
		if (num2 == null)
		{
			return 1043;
		}
		uint num4 = num2;
		LONGXMETRIC longxmetric;
		*(ref longxmetric + 2) = 0;
		longxmetric = 0;
		ushort num5 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.LONGXMETRIC_CONTROL));
		ushort num7;
		ushort num9;
		uint num11;
		if (usDttfGlyphIndexCount == null)
		{
			if (*(ref xhea + 34) != usGlyphListCount && usMaxGlyphIndexUsed + 2 > *(ref xhea + 34))
			{
				return 1007;
			}
			int num6 = usMaxGlyphIndexUsed + 2;
			num7 = ((usGlyphListCount >= num6) ? num6 : usGlyphListCount);
			ushort num8 = 0;
			if (0 < num7)
			{
				for (;;)
				{
					if (num8[puchKeepGlyphList] == 0)
					{
						num3 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&longxmetric), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.LONGXMETRIC_CONTROL), num4, &num9);
						if (num3 != 0)
						{
							break;
						}
					}
					num4 += num5;
					num8 += 1;
					if (num8 >= num7)
					{
						goto IL_181;
					}
				}
				return num3;
			}
			IL_181:
			ushort num10 = num7;
			if (num7 < usGlyphListCount)
			{
				do
				{
					num3 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, 0, num4);
					if (num3 != 0)
					{
						return num3;
					}
					num4 = (uint)(num4 + 2UL);
					num10 += 1;
				}
				while (num10 < usGlyphListCount);
			}
			num11 = num4 - num2;
		}
		else
		{
			num4 = num2;
			LONGXMETRIC* ptr2 = (LONGXMETRIC*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(usDttfGlyphIndexCount * 4UL);
			if (ptr2 == null)
			{
				return 1005;
			}
			num7 = 0;
			ushort num10 = 0;
			ushort num12 = 0;
			LONGXMETRIC longxmetric2;
			if (0 < *(ref xhea + 34))
			{
				while (num12 < usDttfGlyphIndexCount)
				{
					if (num10[puchKeepGlyphList] != 0)
					{
						ushort num13;
						num3 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&longxmetric2), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.LONGXMETRIC_CONTROL), num4, &num13);
						if (num3 != 0)
						{
							goto IL_258;
						}
						cpblk((ulong)num12 * 4UL / (ulong)sizeof(LONGXMETRIC) + ptr2, ref longxmetric2, 4);
						num12 += 1;
						num7 += 1;
					}
					else if (num10 == *(ref xhea + 34) - 1)
					{
						ushort num13;
						num3 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&longxmetric2), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.LONGXMETRIC_CONTROL), num4, &num13);
						if (num3 != 0)
						{
							goto IL_258;
						}
						num7 += 1;
					}
					num4 += num5;
					num10 += 1;
					if (num10 < *(ref xhea + 34))
					{
						continue;
					}
					break;
					IL_258:
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
					return num3;
				}
			}
			if (num10 < usGlyphListCount)
			{
				while (num12 < usDttfGlyphIndexCount)
				{
					if (num10[puchKeepGlyphList] != 0)
					{
						num3 = <Module>.MS.Internal.TtfDelta.ReadWord(pOutputBufferInfo, ref longxmetric2 + 2, num4);
						if (num3 != 0)
						{
							<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
							return num3;
						}
						cpblk((ulong)num12 * 4UL / (ulong)sizeof(LONGXMETRIC) + ptr2, ref longxmetric2, 4);
						num12 += 1;
					}
					num4 = (uint)(num4 + 2UL);
					num10 += 1;
					if (num10 >= usGlyphListCount)
					{
						break;
					}
				}
			}
			if (num12 != usDttfGlyphIndexCount)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
				return 1000;
			}
			num3 = <Module>.MS.Internal.TtfDelta.WriteGenericRepeat(pOutputBufferInfo, (byte*)ptr2, (byte*)(&<Module>.MS.Internal.TtfDelta.LONGXMETRIC_CONTROL), num2, &num11, num7, 4);
			if (num3 == 0)
			{
				num4 = num11 + num2;
				num10 = num7;
				if (num7 < usDttfGlyphIndexCount)
				{
					do
					{
						num3 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, (ushort)(*(short*)(ptr2 + (ulong)num10 * 4UL / (ulong)sizeof(LONGXMETRIC) + 2L / (long)sizeof(LONGXMETRIC))), num4);
						if (num3 != 0)
						{
							break;
						}
						num4 = (uint)(num4 + 2UL);
						num10 += 1;
					}
					while (num10 < usDttfGlyphIndexCount);
				}
			}
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
			if (num3 != 0)
			{
				return num3;
			}
			num11 = num4 - num2;
		}
		num3 = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, ptr, num11);
		if (num3 == 0 && num7 != *(ref xhea + 34))
		{
			*(ref xhea + 34) = (short)num7;
			num3 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&xhea), 36, (byte*)(&<Module>.MS.Internal.TtfDelta.XHEA_CONTROL), num, &num9);
			if (num3 != 0)
			{
				return num3;
			}
		}
		*(int*)pulNewOutOffset = num4;
		return num3;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x0000E160 File Offset: 0x0000D560
	[SecurityCritical]
	internal unsafe static short ModMaxP(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, uint* pulNewOutOffset)
	{
		MAXP maxp;
		uint num = <Module>.MS.Internal.TtfDelta.GetMaxp(pOutputBufferInfo, &maxp);
		if (num == null)
		{
			if (<Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04KODIGLGG@maxp@), pulNewOutOffset) != 0)
			{
				return 1036;
			}
			num = <Module>.MS.Internal.TtfDelta.GetMaxp(pOutputBufferInfo, &maxp);
			if (num == null)
			{
				return 1000;
			}
		}
		int num2;
		if (3 > *(ref maxp + 28))
		{
			num2 = 3;
		}
		else
		{
			num2 = (int)(*(ref maxp + 28));
		}
		ushort num3 = (ushort)((int)((3 <= *(ref maxp + 30)) ? (*(ref maxp + 30)) : 3) * num2);
		ushort* ptr = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num3 * 2UL);
		if (ptr == null)
		{
			return 1005;
		}
		short num4 = <Module>.MS.Internal.TtfDelta.ComputeMaxPStats(pOutputBufferInfo, ref maxp + 8, ref maxp + 6, ref maxp + 12, ref maxp + 10, ref maxp + 26, ref maxp + 28, ref maxp + 30, ptr, num3);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		if (num4 == 0)
		{
			ushort num5;
			num4 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&maxp), 32, (byte*)(&<Module>.MS.Internal.TtfDelta.MAXP_CONTROL), num, &num5);
		}
		return num4;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000E228 File Offset: 0x0000D628
	[SecurityCritical]
	internal unsafe static short ModOS2(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usMinChr, ushort usMaxChr, ushort usFormat, uint* pulNewOutOffset)
	{
		int num = 0;
		short num2 = 0;
		if (usFormat == 2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04OEKHDCJK@OS?12@));
			return 0;
		}
		NEWOS2 newos;
		uint num3 = <Module>.MS.Internal.TtfDelta.GetSmartOS2(pOutputBufferInfo, &newos, &num);
		if (num3 == null)
		{
			num2 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04OEKHDCJK@OS?12@), pulNewOutOffset);
			if (num2 != 0)
			{
				return (num2 == 1006) ? 0 : num2;
			}
			num3 = <Module>.MS.Internal.TtfDelta.GetSmartOS2(pOutputBufferInfo, &newos, &num);
			if (num3 == null)
			{
				return 1000;
			}
		}
		if (usMinChr != 0 || usMaxChr != 0)
		{
			*(ref newos + 66) = (short)((*(ref newos + 66) < 61440) ? usMinChr : (*(ref newos + 66)));
			*(ref newos + 68) = (short)usMaxChr;
			if (num != 0)
			{
				ushort num4;
				num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&newos), 88, (byte*)(&<Module>.MS.Internal.TtfDelta.NEWOS2_CONTROL), num3, &num4);
				if (num2 != 0)
				{
					return num2;
				}
			}
			else
			{
				ushort num4;
				num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&newos), 80, (byte*)(&<Module>.MS.Internal.TtfDelta.OS2_CONTROL), num3, &num4);
				if (num2 != 0)
				{
					return num2;
				}
			}
		}
		return num2;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000E2EC File Offset: 0x0000D6EC
	[SecurityCritical]
	internal unsafe static short ModPost(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usFormat, uint* pulNewOutOffset)
	{
		if (usFormat == 2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04LOKOGFID@post@));
			return 0;
		}
		short num = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LOKOGFID@post@), pulNewOutOffset);
		if (num != 0)
		{
			return (num == 1006) ? 0 : num;
		}
		POST post;
		uint num2 = <Module>.MS.Internal.TtfDelta.GetPost(pOutputBufferInfo, &post);
		if (num2 == null)
		{
			return 1000;
		}
		if (post != 196608)
		{
			post = 196608;
			ushort num3;
			num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&post), 32, (byte*)(&<Module>.MS.Internal.TtfDelta.POST_CONTROL), num2, &num3);
			if (num != 0)
			{
				return num;
			}
			num = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04LOKOGFID@post@), num3);
			*(int*)pulNewOutOffset = num3 + num2;
		}
		return num;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000E378 File Offset: 0x0000D778
	[SecurityCritical]
	internal unsafe static short ModName(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usLanguage, ushort usFormat, uint* pulNewOutOffset)
	{
		ushort num = 0;
		ushort num2 = 0;
		uint num3 = 0;
		if (usFormat == 2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@));
			return 0;
		}
		short num4 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@), pulNewOutOffset);
		if (num4 != 0)
		{
			return num4;
		}
		uint num5 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@));
		uint ulBufferSize = <Module>.MS.Internal.TtfDelta.TTTableLength(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@));
		namerecord* ptr;
		ushort num6;
		num4 = <Module>.MS.Internal.TtfDelta.ReadAllocNameRecords(pOutputBufferInfo, &ptr, &num6, ldftn(MS.Internal.TtfDelta.Mem_Alloc), ldftn(MS.Internal.TtfDelta.Mem_Free));
		if (num4 != 0)
		{
			return num4;
		}
		if (usLanguage != null)
		{
			ushort num7 = 0;
			if (0 < num6)
			{
				do
				{
					namerecord* ptr2 = num7 * 40L + ptr / sizeof(namerecord);
					if (*(ushort*)ptr2 == 3)
					{
						if (*(ushort*)(ptr2 + 4L / (long)sizeof(namerecord)) == usLanguage)
						{
							num = 1;
						}
						else
						{
							*(int*)(ptr2 + 32L / (long)sizeof(namerecord)) = 1;
							num2 = 1;
						}
					}
					num7 += 1;
				}
				while (num7 < num6);
				if (num2 != 0)
				{
					num2 = ((num == 0) ? 0 : num2);
				}
			}
		}
		TTFACC_FILEBUFFERINFO ttfacc_FILEBUFFERINFO;
		<Module>.MS.Internal.TtfDelta.InitFileBufferInfo(&ttfacc_FILEBUFFERINFO, num5 + *(long*)pOutputBufferInfo, ulBufferSize, 0L);
		num4 = <Module>.MS.Internal.TtfDelta.WriteNameRecords(&ttfacc_FILEBUFFERINFO, ptr, num6, (int)num2, 1, &num3);
		<Module>.MS.Internal.TtfDelta.FreeNameRecords(ptr, num6, ldftn(MS.Internal.TtfDelta.Mem_Free));
		if (num4 == 0)
		{
			*(int*)pulNewOutOffset = num5 + num3;
			<Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@), num3);
		}
		else
		{
			*(int*)pulNewOutOffset = num5;
			num4 = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04MEMAJGDJ@name@), pulNewOutOffset);
		}
		return num4;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000E490 File Offset: 0x0000D890
	[SecurityCritical]
	internal unsafe static short AdjustKernFormat0(TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, KERN_SUB_HEADER KernSubHeader, uint ulOffset, ushort usSubHeaderSize, ushort* pusNewLength)
	{
		uint num = usSubHeaderSize + ulOffset;
		KERN_FORMAT_0 kern_FORMAT_;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&kern_FORMAT_), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_FORMAT_0_CONTROL), num, &num3);
		if (num2 != 0)
		{
			return num2;
		}
		num += num3;
		uint num4 = num;
		ushort num5 = 0;
		ushort num6 = 0;
		if (0 < kern_FORMAT_)
		{
			ushort num7;
			for (;;)
			{
				KERN_PAIR kern_PAIR;
				num2 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&kern_PAIR), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_PAIR_CONTROL), num, &num3);
				if (num2 != 0)
				{
					return num2;
				}
				if (kern_PAIR < usGlyphListCount && kern_PAIR[puchKeepGlyphList] != 0 && *(ref kern_PAIR + 2) < usGlyphListCount && (*(ref kern_PAIR + 2))[puchKeepGlyphList] != 0)
				{
					num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&kern_PAIR), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_PAIR_CONTROL), num4, &num7);
					if (num2 != 0)
					{
						break;
					}
					num4 += num7;
					num5 += 1;
				}
				num += num3;
				num6 += 1;
				if (num6 >= kern_FORMAT_)
				{
					goto IL_A0;
				}
			}
			return num2;
			IL_A0:
			if (num5 <= 0)
			{
				goto IL_135;
			}
			uint num8 = num4 - ulOffset;
			*pusNewLength = num8;
			*(ref KernSubHeader + 2) = num8;
			num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&KernSubHeader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_SUB_HEADER_CONTROL), ulOffset, &num7);
			if (num2 != 0)
			{
				return num2;
			}
			int num9 = 1 << (int)<Module>.MS.Internal.TtfDelta.log2(num5);
			ushort num10 = (ushort)((int)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.KERN_PAIR_CONTROL)) * num9);
			ushort num11 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.KERN_PAIR_CONTROL)) * num5 - num10;
			kern_FORMAT_ = num5;
			*(ref kern_FORMAT_ + 2) = (short)num10;
			*(ref kern_FORMAT_ + 4) = (short)<Module>.MS.Internal.TtfDelta.log2(num5);
			*(ref kern_FORMAT_ + 6) = (short)num11;
			num2 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&kern_FORMAT_), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_FORMAT_0_CONTROL), num7 + ulOffset, &num7);
			if (num2 != 0)
			{
				return num2;
			}
			return 0;
		}
		IL_135:
		*pusNewLength = 0;
		return 0;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000E5D8 File Offset: 0x0000D9D8
	[SecurityCritical]
	internal unsafe static short ModKern(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort usFormat, uint* pulNewOutOffset)
	{
		if (usFormat == 2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04HHMMLDJI@kern@));
			return 0;
		}
		short num = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04HHMMLDJI@kern@), pulNewOutOffset);
		if (num != 0)
		{
			return (num == 1006) ? 0 : num;
		}
		if (usFormat == 1)
		{
			return 0;
		}
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04HHMMLDJI@kern@));
		if (num2 == null)
		{
			return 1000;
		}
		KERN_HEADER kern_HEADER;
		ushort num3;
		num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&kern_HEADER), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_HEADER_CONTROL), num2, &num3);
		if (num != 0)
		{
			return num;
		}
		uint num4 = num3 + num2;
		uint num5 = num4;
		ushort num6 = 0;
		if (0 < *(ref kern_HEADER + 2))
		{
			for (;;)
			{
				KERN_SUB_HEADER kern_SUB_HEADER;
				num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&kern_SUB_HEADER), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.KERN_SUB_HEADER_CONTROL), num4, &num3);
				if (num != 0)
				{
					return num;
				}
				num = <Module>.MS.Internal.TtfDelta.CopyBlock(pOutputBufferInfo, num5, num4, *(ref kern_SUB_HEADER + 2));
				if (num != 0)
				{
					return num;
				}
				num4 += *(ref kern_SUB_HEADER + 2);
				if (kern_SUB_HEADER == null)
				{
					ushort num7;
					num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.AdjustKernFormat0(pOutputBufferInfo, puchKeepGlyphList, usGlyphListCount, kern_SUB_HEADER, num5, num3, &num7);
					if (num != 0)
					{
						break;
					}
					num5 += num7;
				}
				else
				{
					num5 += *(ref kern_SUB_HEADER + 2);
				}
				num6 += 1;
				if (num6 >= *(ref kern_HEADER + 2))
				{
					goto IL_E2;
				}
			}
			return num;
		}
		IL_E2:
		if (num5 == <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.KERN_HEADER_CONTROL)) + num2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04HHMMLDJI@kern@));
		}
		else
		{
			num = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04HHMMLDJI@kern@), num5 - num2);
		}
		*(int*)pulNewOutOffset = num5;
		return num;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000E6F8 File Offset: 0x0000DAF8
	[SecurityCritical]
	internal unsafe static short ModHdmx(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort usDttfGlyphIndexCount, uint* pulNewOutOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDDCPPLH@hdmx@), pulNewOutOffset);
		if (num != 0)
		{
			return (num == 1006) ? 0 : num;
		}
		HDMX hdmx;
		uint num2 = <Module>.MS.Internal.TtfDelta.GetHdmx(pOutputBufferInfo, &hdmx);
		if (num2 == null)
		{
			return 1000;
		}
		uint num3 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_CONTROL)) + num2;
		if (usDttfGlyphIndexCount != null)
		{
			uint num4 = num3;
			uint num5 = num3;
			uint num6 = <Module>.MS.Internal.TtfDelta.RoundToLongWord((uint)((ulong)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_DEVICE_REC_CONTROL)) + usDttfGlyphIndexCount));
			ushort num7 = 0;
			ushort num13;
			if (0 < *(ref hdmx + 2))
			{
				for (;;)
				{
					uint num8 = num5;
					uint num9 = num4;
					HDMX_DEVICE_REC hdmx_DEVICE_REC;
					ushort num10;
					num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&hdmx_DEVICE_REC), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_DEVICE_REC_CONTROL), num5, &num10);
					if (num != 0)
					{
						return num;
					}
					num5 += num10;
					num4 += num10;
					byte b = 0;
					ushort num11 = 0;
					ushort num12 = 0;
					if (0 < usGlyphListCount)
					{
						while (num12 < usDttfGlyphIndexCount)
						{
							if (num11[puchKeepGlyphList] != 0)
							{
								byte b2;
								num = <Module>.MS.Internal.TtfDelta.ReadByte(pOutputBufferInfo, &b2, num5);
								if (num != 0)
								{
									return num;
								}
								byte b3 = (b > b2) ? b : b2;
								b = b3;
								num = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, b2, num4);
								if (num != 0)
								{
									return num;
								}
								num4 = (uint)(num4 + 1UL);
								num12 += 1;
							}
							num5 = (uint)(num5 + 1UL);
							num11 += 1;
							if (num11 >= usGlyphListCount)
							{
								break;
							}
						}
					}
					num = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, num4, &num4);
					if (num != 0)
					{
						return num;
					}
					*(ref hdmx_DEVICE_REC + 1) = b;
					num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&hdmx_DEVICE_REC), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_DEVICE_REC_CONTROL), num9, &num13);
					if (num != 0)
					{
						return num;
					}
					num5 = *(ref hdmx + 4) + num8;
					num4 = num9 + num6;
					num7 += 1;
					if (num7 >= (ushort)(*(ref hdmx + 2)))
					{
						goto IL_15A;
					}
				}
				return num;
			}
			IL_15A:
			*(ref hdmx + 4) = num6;
			num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&hdmx), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_CONTROL), num2, &num13);
			if (num != 0)
			{
				return num;
			}
			num = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04IDDCPPLH@hdmx@), num4 - num2);
			if (num != 0)
			{
				return num;
			}
			*(int*)pulNewOutOffset = num4;
		}
		else
		{
			ushort num7 = 0;
			if (0 < *(ref hdmx + 2))
			{
				for (;;)
				{
					uint num14 = num3;
					HDMX_DEVICE_REC hdmx_DEVICE_REC;
					ushort num10;
					num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pOutputBufferInfo, (byte*)(&hdmx_DEVICE_REC), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_DEVICE_REC_CONTROL), num3, &num10);
					if (num != 0)
					{
						return num;
					}
					num3 += num10;
					byte b4 = 0;
					ushort num15 = 0;
					if (0 < usGlyphListCount)
					{
						do
						{
							if (num15[puchKeepGlyphList] != 0)
							{
								byte b2;
								num = <Module>.MS.Internal.TtfDelta.ReadByte(pOutputBufferInfo, &b2, num3);
								if (num != 0)
								{
									return num;
								}
								byte b5 = (b4 > b2) ? b4 : b2;
								b4 = b5;
							}
							else
							{
								num = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, 0, num3);
								if (num != 0)
								{
									return num;
								}
							}
							num3 = (uint)(num3 + 1UL);
							num15 += 1;
						}
						while (num15 < usGlyphListCount);
					}
					if (*(ref hdmx_DEVICE_REC + 1) != b4)
					{
						*(ref hdmx_DEVICE_REC + 1) = b4;
						ushort num13;
						num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&hdmx_DEVICE_REC), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.HDMX_DEVICE_REC_CONTROL), num14, &num13);
						if (num != 0)
						{
							return num;
						}
					}
					num3 = num14 + *(ref hdmx + 4);
					num7 += 1;
					if (num7 >= (ushort)(*(ref hdmx + 2)))
					{
						goto IL_261;
					}
				}
				return num;
			}
			IL_261:
			*(int*)pulNewOutOffset = num3;
		}
		return 0;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000E96C File Offset: 0x0000DD6C
	[SecurityCritical]
	internal unsafe static short ModLTSH(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort usDttfGlyphIndexCount, uint* pulNewOutOffset)
	{
		short num = <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LGOLHALL@LTSH@), pulNewOutOffset);
		if (num != 0)
		{
			return (num == 1006) ? 0 : num;
		}
		LTSH ltsh;
		uint num2 = <Module>.MS.Internal.TtfDelta.GetLTSH(pOutputBufferInfo, &ltsh);
		if (num2 == null)
		{
			return 1000;
		}
		uint num3;
		if (usDttfGlyphIndexCount != null)
		{
			num3 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.LTSH_CONTROL)) + num2;
			uint num4 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.LTSH_CONTROL)) + num2;
			ushort num5 = (*(ref ltsh + 2) < usGlyphListCount) ? (*(ref ltsh + 2)) : usGlyphListCount;
			ushort num6 = 0;
			ushort num7 = 0;
			if (0 < num5)
			{
				while (num7 < usDttfGlyphIndexCount)
				{
					if (num6[puchKeepGlyphList] != 0)
					{
						byte uchValue;
						num = <Module>.MS.Internal.TtfDelta.ReadByte(pOutputBufferInfo, &uchValue, num4);
						if (num != 0)
						{
							return num;
						}
						num = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, uchValue, num3);
						if (num != 0)
						{
							return num;
						}
						num3 = (uint)(num3 + 1UL);
						num7 += 1;
					}
					num4 = (uint)(num4 + 1UL);
					num6 += 1;
					if (num6 >= num5)
					{
						break;
					}
				}
			}
			*(ref ltsh + 2) = usDttfGlyphIndexCount;
			ushort num8;
			num = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&ltsh), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.LTSH_CONTROL), num2, &num8);
			if (num != 0)
			{
				return num;
			}
			num = <Module>.MS.Internal.TtfDelta.UpdateDirEntry(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04LGOLHALL@LTSH@), num3 - num2);
			if (num != 0)
			{
				return num;
			}
		}
		else
		{
			num3 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.LTSH_CONTROL)) + num2;
			ushort num9 = (*(ref ltsh + 2) < usGlyphListCount) ? (*(ref ltsh + 2)) : usGlyphListCount;
			ushort num6 = 0;
			if (0 < num9)
			{
				for (;;)
				{
					if (num6[puchKeepGlyphList] == 0)
					{
						num = <Module>.MS.Internal.TtfDelta.WriteByte(pOutputBufferInfo, 0, num3);
						if (num != 0)
						{
							break;
						}
					}
					num3 = (uint)(num3 + 1UL);
					num6 += 1;
					if (num6 >= num9)
					{
						goto IL_13B;
					}
				}
				return num;
			}
		}
		IL_13B:
		*(int*)pulNewOutOffset = num3;
		return 0;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000098C0 File Offset: 0x00008CC0
	[SecurityCritical]
	internal static ushort GCD(ushort u, ushort v)
	{
		if (v == 0)
		{
			return u;
		}
		return <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GCD(v, u % v);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000098DC File Offset: 0x00008CDC
	[SecurityCritical]
	internal unsafe static void ReduceRatio(ushort* px, ushort* py)
	{
		ushort num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GCD(*px, *py);
		if (num > 0)
		{
			*px /= num;
			*py /= num;
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00009904 File Offset: 0x00008D04
	[SecurityCritical]
	internal unsafe static short InitGroupOffsetArray(groupoffsetrecordkeeper* pKeeper, ushort usRecordCount)
	{
		void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)usRecordCount * 4UL);
		*(long*)pKeeper = ptr;
		if (ptr == null)
		{
			return 1005;
		}
		*(short*)(pKeeper + 8L / (long)sizeof(groupoffsetrecordkeeper)) = (short)usRecordCount;
		*(short*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper)) = 0;
		return 0;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00008578 File Offset: 0x00007978
	[SecurityCritical]
	internal unsafe static void FreeGroupOffsetArray(groupoffsetrecordkeeper* pKeeper)
	{
		<Module>.MS.Internal.TtfDelta.Mem_Free(*(long*)pKeeper);
		*(long*)pKeeper = 0L;
		*(short*)(pKeeper + 8L / (long)sizeof(groupoffsetrecordkeeper)) = 0;
		*(short*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper)) = 0;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00009938 File Offset: 0x00008D38
	[SecurityCritical]
	internal unsafe static short RecordGroupOffset(groupoffsetrecordkeeper* pKeeper, ushort usOldGroupOffset, ushort usNewGroupOffset)
	{
		ushort num = *(ushort*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper));
		if (num >= *(ushort*)(pKeeper + 8L / (long)sizeof(groupoffsetrecordkeeper)))
		{
			return 1006;
		}
		*((ulong)num * 4UL + (ulong)(*(long*)pKeeper)) = (short)usOldGroupOffset;
		*(*(long*)pKeeper + (long)((ulong)(*(ushort*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper))) * 4UL) + 2L) = (short)usNewGroupOffset;
		*(short*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper)) = (short)(*(ushort*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper)) + 1);
		return 0;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00009988 File Offset: 0x00008D88
	[SecurityCritical]
	internal unsafe static ushort LookupGroupOffset(groupoffsetrecordkeeper* pKeeper, ushort usOldGroupOffset)
	{
		ushort num = 0;
		ushort num2 = *(ushort*)(pKeeper + 10L / (long)sizeof(groupoffsetrecordkeeper));
		if (0 < num2)
		{
			long num3 = *(long*)pKeeper;
			while (usOldGroupOffset != *((ulong)num * 4UL + (ulong)num3))
			{
				num += 1;
				if (num >= num2)
				{
					return 0;
				}
			}
			return *(num3 + (long)((ulong)num * 4UL) + 2L);
		}
		return 0;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000EABC File Offset: 0x0000DEBC
	[SecurityCritical]
	internal unsafe static short ModVDMX(CONST_TTFACC_FILEBUFFERINFO* pInputBufferInfo, TTFACC_FILEBUFFERINFO* pOutputBufferInfo, ushort usFormat, uint* pulNewOutOffset)
	{
		sbyte* ptr = null;
		byte* ptr2 = null;
		ushort num = 0;
		ushort num2 = 0;
		if (usFormat == 2)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@));
			return 0;
		}
		uint num3 = <Module>.MS.Internal.TtfDelta.TTTableOffset((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@));
		if (num3 == null)
		{
			return 0;
		}
		uint num4 = <Module>.MS.Internal.TtfDelta.TTTableLength((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@));
		if (num4 == null)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@));
			return 0;
		}
		uint num6;
		short num5 = (short)<Module>.MS.Internal.TtfDelta.ZeroLongWordAlign(pOutputBufferInfo, *(int*)pulNewOutOffset, &num6);
		if (num5 != 0)
		{
			return num5;
		}
		VDMX vdmx;
		ushort num7;
		num5 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)(&vdmx), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.VDMX_CONTROL), num3, &num7);
		if (num5 != 0)
		{
			return num5;
		}
		if (*(ref vdmx + 4) == 0)
		{
			<Module>.MS.Internal.TtfDelta.MarkTableForDeletion(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@));
			return 0;
		}
		uint num8 = num7 + num3;
		uint num9 = (uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.VDMXRATIO_CONTROL));
		ulong num10 = (uint)(*(ref vdmx + 4)) * num9 + num8;
		ulong num11 = (ulong)(*(ref vdmx + 4));
		ulong num12 = num10;
		uint num13 = (uint)(num11 * 2UL + num12);
		groupoffsetrecordkeeper groupoffsetrecordkeeper;
		initblk(ref groupoffsetrecordkeeper, 0, 16L);
		VDMXRatio* ptr3 = (VDMXRatio*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num11 * 4UL);
		if (ptr3 == null)
		{
			num5 = 1005;
		}
		else
		{
			ptr = (sbyte*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref vdmx + 4)));
			if (ptr == null)
			{
				num5 = 1005;
			}
			else
			{
				uint num14;
				num5 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)ptr3, (byte*)(&<Module>.MS.Internal.TtfDelta.VDMXRATIO_CONTROL), num8, &num14, *(ref vdmx + 4), 4);
				if (num5 == 0)
				{
					short num15 = 0;
					ushort num16 = 0;
					if (0 < *(ref vdmx + 4))
					{
						for (;;)
						{
							long num17 = (long)((ulong)num16);
							num17[ptr] = 1;
							VDMXRatio* ptr4 = num17 * 4L / (long)sizeof(VDMXRatio) + ptr3;
							ushort num18 = (ushort)(*(byte*)(ptr4 + 1L / (long)sizeof(VDMXRatio)));
							ushort num19 = (ushort)(*(byte*)(ptr4 + 2L / (long)sizeof(VDMXRatio)));
							<Module>.MS.Internal.TtfDelta.ReduceRatio(&num18, &num19);
							if (num18 == num19)
							{
								if (*(byte*)(ptr4 + 1L / (long)sizeof(VDMXRatio)) == 0)
								{
									break;
								}
								if (num15 != 0)
								{
									num17[ptr] = 0;
								}
								else
								{
									num15 = 1;
									num2 += 1;
								}
							}
							else if (num18 == 4 && num19 == 3)
							{
								num17[ptr] = 0;
							}
							else
							{
								num2 += 1;
							}
							num16 += 1;
							if (num16 >= *(ref vdmx + 4))
							{
								goto IL_1A9;
							}
						}
						if (num15 == 0)
						{
							num2 += 1;
						}
						IL_1A9:
						if (num2 != 0 && num2 != *(ref vdmx + 4))
						{
							uint num20 = num7 + num6;
							ulong num21 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.VDMXRATIO_CONTROL)) * num2 + num20;
							ushort num22 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.VDMXRATIO_CONTROL));
							ulong num23 = num21;
							uint num24 = (uint)((ulong)num2 * 2UL + num23) - num6;
							num5 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.InitGroupOffsetArray(&groupoffsetrecordkeeper, num2);
							if (num5 != 0)
							{
								goto IL_3C9;
							}
							uint num25 = num4 - num13 + num3;
							ptr2 = (byte*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(num25);
							if (ptr2 == null)
							{
								num5 = 1005;
								goto IL_3C9;
							}
							ushort num26 = 0;
							ushort num27 = 0;
							ushort num29;
							if (0 < *(ref vdmx + 4))
							{
								while (num26 < num2)
								{
									long num28 = (long)((ulong)num27);
									if (num28[ptr] == 1)
									{
										num5 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(num28 * 4L / (long)sizeof(VDMXRatio) + ptr3), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.VDMXRATIO_CONTROL), num26 * num22 + num20, &num29);
										if (num5 != 0)
										{
											goto IL_3C9;
										}
										ushort num30;
										num5 = <Module>.MS.Internal.TtfDelta.ReadWord((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, &num30, (uint)((ulong)num27 * 2UL + num12));
										if (num5 != 0)
										{
											goto IL_3C9;
										}
										ushort num31 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.LookupGroupOffset(&groupoffsetrecordkeeper, num30);
										if (num31 == 0)
										{
											if (num24 > 65535)
											{
												num5 = 1088;
												goto IL_3C9;
											}
											ushort num32 = num24;
											num5 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.RecordGroupOffset(&groupoffsetrecordkeeper, num30, num32);
											if (num5 != 0)
											{
												goto IL_3C9;
											}
											num31 = num32;
											VDMXGroup vdmxgroup;
											num5 = <Module>.MS.Internal.TtfDelta.ReadGeneric((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, (byte*)(&vdmxgroup), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.VDMXGROUP_CONTROL), num30 + num3, &num7);
											if (num5 != 0)
											{
												goto IL_3C9;
											}
											uint num33 = (uint)<Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.VDMXVTABLE_CONTROL));
											uint num34 = vdmxgroup * num33 + num7;
											if (num34 > num25)
											{
												num5 = 1088;
												goto IL_3C9;
											}
											num5 = <Module>.MS.Internal.TtfDelta.ReadBytes((TTFACC_FILEBUFFERINFO*)pInputBufferInfo, ptr2, num30 + num3, num34);
											if (num5 != 0)
											{
												goto IL_3C9;
											}
											num5 = <Module>.MS.Internal.TtfDelta.WriteBytes(pOutputBufferInfo, ptr2, num32 + num6, num34);
											if (num5 != 0)
											{
												goto IL_3C9;
											}
											num += 1;
											num24 = num32 + num34;
										}
										num5 = <Module>.MS.Internal.TtfDelta.WriteWord(pOutputBufferInfo, num31, (uint)((ulong)num26 * 2UL + num23));
										if (num5 != 0)
										{
											goto IL_3C9;
										}
										num26 += 1;
									}
									num27 += 1;
									if (num27 >= *(ref vdmx + 4))
									{
										break;
									}
								}
							}
							*(ref vdmx + 4) = (short)num2;
							*(ref vdmx + 2) = (short)num;
							num5 = <Module>.MS.Internal.TtfDelta.WriteGeneric(pOutputBufferInfo, (byte*)(&vdmx), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.VDMX_CONTROL), num6, &num29);
							if (num5 == 0)
							{
								num5 = <Module>.MS.Internal.TtfDelta.UpdateDirEntryAll(pOutputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@), num24, num6);
								*(int*)pulNewOutOffset = num6 + num24;
								goto IL_3C9;
							}
							goto IL_3C9;
						}
					}
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr3);
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
					return <Module>.MS.Internal.TtfDelta.CopyTableOver(pOutputBufferInfo, pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04JANIDECM@VDMX@), pulNewOutOffset);
				}
			}
		}
		IL_3C9:
		<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.FreeGroupOffsetArray(&groupoffsetrecordkeeper);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr3);
		return num5;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000099C8 File Offset: 0x00008DC8
	[SecurityCritical]
	internal unsafe static short UnicodeToSymbols(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint* pulKeepCharCodeList, ushort usCharListCount, uint** ppulKeepSymbolCodeList)
	{
		*(long*)ppulKeepSymbolCodeList = 0L;
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04OEKHDCJK@OS?12@));
		if (num == null)
		{
			return 1039;
		}
		ushort num2;
		if (<Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num2, num + 64) == 0)
		{
			if (num2 >= 61440)
			{
				void* ptr = <Module>.MS.Internal.TtfDelta.Mem_Alloc(usCharListCount * 4UL);
				*(long*)ppulKeepSymbolCodeList = ptr;
				if (ptr == null)
				{
					return 1005;
				}
				ushort num3 = num2 & 65280;
				ushort num4 = 0;
				if (0 < usCharListCount)
				{
					do
					{
						long num5 = (long)((ulong)num4 * 4UL);
						uint num6 = (uint)(*(int*)(num5 / (long)sizeof(uint) + pulKeepCharCodeList));
						if (num6 <= 255U)
						{
							*(num5 + *(long*)ppulKeepSymbolCodeList) = (int)((uint)num3 + num6);
						}
						else
						{
							*(num5 + *(long*)ppulKeepSymbolCodeList) = (int)num6;
						}
						num4 += 1;
					}
					while (num4 < usCharListCount);
				}
			}
			return 0;
		}
		return 1001;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00009A68 File Offset: 0x00008E68
	[SecurityCritical]
	internal unsafe static short EnsureNonEmptyGlyfTable(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* puchKeepGlyphList, ushort usGlyphCount)
	{
		uint* ptr = (uint*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(usGlyphCount + 1) * 4L));
		if (ptr == null)
		{
			return 1005;
		}
		if (<Module>.MS.Internal.TtfDelta.GetLoca(pInputBufferInfo, ptr, usGlyphCount + 1) == null)
		{
			<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
			return 1065;
		}
		ushort num = ushort.MaxValue;
		ushort num2 = 0;
		if (0 < usGlyphCount)
		{
			do
			{
				long num3 = (long)((ulong)num2);
				if (*(int*)(num3 * 4L / (long)sizeof(uint) + ptr) < *(int*)((long)(num2 + 1) * 4L + ptr / 4))
				{
					if (num3[puchKeepGlyphList] != 0)
					{
						break;
					}
					num = ((num == ushort.MaxValue) ? num2 : num);
				}
				num2 += 1;
			}
			while (num2 < usGlyphCount);
		}
		if (num2 == usGlyphCount)
		{
			if (num == 65535)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return 1061;
			}
			num[puchKeepGlyphList] = 1;
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return 0;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000EEB0 File Offset: 0x0000E2B0
	[SecurityCritical]
	internal unsafe static short MakeKeepGlyphList(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usListType, ushort usPlatform, ushort usEncoding, uint* pulKeepCharCodeList, ushort usCharListCount, byte* puchKeepGlyphList, ushort usGlyphListCount, ushort* pusMaxGlyphIndexUsed, ushort* pusGlyphKeepCount, int bAddRelatedGlyphs)
	{
		FORMAT4_SEGMENTS* format4Segments = null;
		FORMAT12_GROUPS* pFormat12Groups = null;
		ushort* glyphId = null;
		ushort num = 0;
		short num2 = 0;
		short num3 = 0;
		HEAD head;
		if (<Module>.MS.Internal.TtfDelta.GetHead(pInputBufferInfo, &head) == null)
		{
			return 1032;
		}
		ushort usIdxToLocFmt = (ushort)(*(ref head + 50));
		MAXP maxp;
		if (<Module>.MS.Internal.TtfDelta.GetMaxp(pInputBufferInfo, &maxp) == null)
		{
			return 1036;
		}
		uint num4 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@));
		if (num4 == null)
		{
			return 1035;
		}
		uint num5 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LLIHEPK@glyf@));
		if (num5 == null)
		{
			return 1031;
		}
		ushort num6 = *(ref maxp + 30) * *(ref maxp + 28);
		ushort* ptr = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num6 * 2UL);
		if (ptr == null)
		{
			return 1005;
		}
		if (bAddRelatedGlyphs != 0)
		{
			if (usGlyphListCount > 0)
			{
				*puchKeepGlyphList = 1;
			}
			if (usGlyphListCount > 1)
			{
				puchKeepGlyphList[1L] = 1;
			}
			if (usGlyphListCount > 2)
			{
				puchKeepGlyphList[2L] = 1;
			}
		}
		if (usListType == 1)
		{
			ushort num7 = 0;
			if (0 < usCharListCount)
			{
				do
				{
					uint num8 = (uint)(num7 * 4L)[pulKeepCharCodeList / 4];
					if (num8 < usGlyphListCount)
					{
						num8[puchKeepGlyphList] = 1;
					}
					num7 += 1;
				}
				while (num7 < usCharListCount);
			}
		}
		else
		{
			uint* ptr2 = null;
			if (<Module>.MS.Internal.TtfDelta.UnicodeToSymbols(pInputBufferInfo, pulKeepCharCodeList, usCharListCount, &ptr2) == 0)
			{
				uint* ptr3 = (uint*)((ptr2 != null) ? ptr2 : ((uint*)pulKeepCharCodeList));
				ushort num10;
				uint num9 = <Module>.MS.Internal.TtfDelta.FindCmapSubtable(pInputBufferInfo, usPlatform, usEncoding, &num10);
				CMAP_SUBHEADER_GEN cmap_SUBHEADER_GEN;
				ushort num11;
				if (num9 > 0 && <Module>.MS.Internal.TtfDelta.ReadCmapLength(pInputBufferInfo, &cmap_SUBHEADER_GEN, num9, &num11) == 0)
				{
					int num12 = cmap_SUBHEADER_GEN;
					CMAP_FORMAT0 cmap_FORMAT4;
					if (num12 != 0)
					{
						CMAP_FORMAT4 cmap_FORMAT3;
						ushort usnGlyphs;
						if (num12 != 4)
						{
							CMAP_FORMAT6 cmap_FORMAT2;
							ushort* ptr5;
							if (num12 != 6)
							{
								CMAP_FORMAT12 cmap_FORMAT;
								if (num12 == 12 && <Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat12(pInputBufferInfo, num9, &cmap_FORMAT, &pFormat12Groups) == 0)
								{
									ushort num13 = 0;
									if (0 < usCharListCount)
									{
										do
										{
											uint* ptr4 = num13 * 4L + ptr3 / sizeof(uint);
											uint num14 = <Module>.MS.Internal.TtfDelta.GetGlyphIdx12(*(int*)ptr4, pFormat12Groups, *(ref cmap_FORMAT + 12));
											if (num14 != null && num14 != -1 && num14 < usGlyphListCount)
											{
												num14[puchKeepGlyphList] = 1;
												uint num15 = (uint)(*(int*)ptr4);
												if (num15 == 183U)
												{
													num2 = 1;
												}
												else
												{
													num3 = ((num15 == 8729U) ? 1 : num3);
												}
											}
											num13 += 1;
										}
										while (num13 < usCharListCount);
									}
									if (usPlatform == 3 && num10 == 1 && num2 != 0 && num3 == 0)
									{
										uint num16 = <Module>.MS.Internal.TtfDelta.GetGlyphIdx12(8729, pFormat12Groups, *(ref cmap_FORMAT + 12));
										if (num16 != null && num16 != -1 && num16 < usGlyphListCount)
										{
											num16[puchKeepGlyphList] = 1;
										}
									}
									<Module>.MS.Internal.TtfDelta.FreeCmapFormat12Groups(pFormat12Groups);
								}
							}
							else if (<Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat6(pInputBufferInfo, usPlatform, usEncoding, &num10, &cmap_FORMAT2, &ptr5) == 0)
							{
								ushort num17 = 0;
								if (0 < usCharListCount)
								{
									do
									{
										ushort num18 = (ushort)(num17 * 4L)[ptr3 / 4];
										if (num18 >= *(ref cmap_FORMAT2 + 6) && num18 < *(ref cmap_FORMAT2 + 8) + *(ref cmap_FORMAT2 + 6))
										{
											ushort num19 = ((long)(num18 - *(ref cmap_FORMAT2 + 6)) * 2L)[ptr5 / 2];
											if (num19 < usGlyphListCount)
											{
												num19[puchKeepGlyphList] = 1;
											}
										}
										num17 += 1;
									}
									while (num17 < usCharListCount);
								}
								<Module>.MS.Internal.TtfDelta.FreeCmapFormat6(ptr5);
							}
						}
						else if (<Module>.MS.Internal.TtfDelta.ReadAllocCmapFormat4(pInputBufferInfo, usPlatform, usEncoding, &num10, &cmap_FORMAT3, &format4Segments, &glyphId, &usnGlyphs) == 0)
						{
							ushort num20 = 0;
							if (0 < usCharListCount)
							{
								do
								{
									uint* ptr6 = num20 * 4L + ptr3 / sizeof(uint);
									ushort num21 = <Module>.MS.Internal.TtfDelta.GetGlyphIdx((ushort)(*(int*)ptr6), format4Segments, (ushort)((uint)(*(ref cmap_FORMAT3 + 6)) >> 1), glyphId, usnGlyphs);
									if (num21 != 0 && num21 != 65535 && num21 < usGlyphListCount)
									{
										num21[puchKeepGlyphList] = 1;
										ushort num22 = (ushort)(*(int*)ptr6);
										num2 = ((num22 == 183) ? 1 : num2);
										num3 = ((num22 == 8729) ? 1 : num3);
									}
									num20 += 1;
								}
								while (num20 < usCharListCount);
							}
							if (usPlatform == 3 && num10 == 1 && num2 != 0 && num3 == 0)
							{
								ushort num23 = <Module>.MS.Internal.TtfDelta.GetGlyphIdx(8729, format4Segments, (ushort)((uint)(*(ref cmap_FORMAT3 + 6)) >> 1), glyphId, usnGlyphs);
								if (num23 != 0 && num23 != 65535 && num23 < usGlyphListCount)
								{
									num23[puchKeepGlyphList] = 1;
								}
							}
							<Module>.MS.Internal.TtfDelta.FreeCmapFormat4(format4Segments, glyphId);
						}
					}
					else if (<Module>.MS.Internal.TtfDelta.ReadCmapFormat0(pInputBufferInfo, usPlatform, usEncoding, &num10, &cmap_FORMAT4) == 0)
					{
						ushort num24 = 0;
						if (0 < usCharListCount)
						{
							do
							{
								ushort num25 = (ushort)(num24 * 4L)[ptr3 / 4];
								if (num25 < 256)
								{
									ushort num26 = (ushort)(*((ulong)num25 + (ref cmap_FORMAT4 + 6)));
									if (num26 < usGlyphListCount)
									{
										num26[puchKeepGlyphList] = 1;
									}
								}
								num24 += 1;
							}
							while (num24 < usCharListCount);
						}
					}
				}
				if (ptr2 != null)
				{
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
				}
			}
		}
		short num27 = <Module>.MS.Internal.TtfDelta.EnsureNonEmptyGlyfTable(pInputBufferInfo, puchKeepGlyphList, usGlyphListCount);
		*pusGlyphKeepCount = 0;
		*pusMaxGlyphIndexUsed = 0;
		ushort num28 = 1;
		if (num27 == 0)
		{
			do
			{
				ushort num29 = 0;
				ushort num30 = 0;
				ushort num31 = 0;
				if (0 < usGlyphListCount)
				{
					do
					{
						if ((ushort)num31[puchKeepGlyphList] == num28)
						{
							num30 = num31;
							num29 += 1;
							ushort num32;
							<Module>.MS.Internal.TtfDelta.GetComponentGlyphList(pInputBufferInfo, num31, &num32, ptr, num6, &num, 0, usIdxToLocFmt, num4, num5);
							ushort num33 = 0;
							if (0 < num32)
							{
								do
								{
									ushort num34 = (num33 * 2L)[ptr / 2];
									if (num34 < usGlyphListCount)
									{
										byte* ptr7 = num34 + puchKeepGlyphList;
										if (*ptr7 == 0)
										{
											*ptr7 = (byte)(num28 + 1);
										}
									}
									num33 += 1;
								}
								while (num33 < num32);
							}
						}
						num31 += 1;
					}
					while (num31 < usGlyphListCount);
				}
				*pusGlyphKeepCount += num29;
				ushort num35 = *pusMaxGlyphIndexUsed;
				ushort num36 = (num30 > num35) ? num30 : num35;
				*pusMaxGlyphIndexUsed = num36;
				if (num29 == 0)
				{
					break;
				}
				if (bAddRelatedGlyphs != 0)
				{
					num27 = <Module>.MS.Internal.TtfDelta.TTOAutoMap(pInputBufferInfo, puchKeepGlyphList, usGlyphListCount, num28);
					if (num27 != 0)
					{
						break;
					}
					num27 = <Module>.MS.Internal.TtfDelta.MortAutoMap(pInputBufferInfo, puchKeepGlyphList, usGlyphListCount, num28);
					if (num27 != 0)
					{
						break;
					}
				}
				num28 += 1;
			}
			while (num27 == 0);
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		return num27;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00009B08 File Offset: 0x00008F08
	[SecurityCritical]
	internal unsafe static short GetGlyphStats(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usGlyphIdx, short* psnContours, ushort* pusnPoints, ushort* pusnInstructions, ushort indexToLocFormat, uint ulLocaOffset, uint ulGlyfOffset, int* bStatus)
	{
		*psnContours = 0;
		*pusnPoints = 0;
		*pusnInstructions = 0;
		*bStatus = 0;
		GLYF_HEADER glyf_HEADER;
		uint num2;
		ushort num3;
		short num = <Module>.MS.Internal.TtfDelta.GetGlyphHeader(pInputBufferInfo, usGlyphIdx, indexToLocFormat, ulLocaOffset, ulGlyfOffset, &glyf_HEADER, &num2, &num3);
		if (num != 0)
		{
			return num;
		}
		if (num3 == 0)
		{
			return 0;
		}
		*psnContours = glyf_HEADER;
		if (glyf_HEADER > 0)
		{
			ulong num4 = <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.GLYF_HEADER_CONTROL)) + num2;
			uint num5 = (uint)((long)(*psnContours - 1) * 2L + (long)num4);
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, pusnPoints, num5);
			if (num != 0)
			{
				return num;
			}
			*pusnPoints += 1;
			num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, pusnInstructions, (uint)(num5 + 2UL));
			if (num != 0)
			{
				return num;
			}
		}
		*bStatus = 1;
		return 0;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00009B94 File Offset: 0x00008F94
	[SecurityCritical]
	internal unsafe static short GetCompositeGlyphStats(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort usGlyphIdx, short* psnContours, ushort* pusnPoints, ushort* pusnInstructions, ushort* pusnComponentElements, ushort* pusnComponentDepth, ushort indexToLocFormat, uint ulLocaOffset, uint ulGlyfOffset, ushort* pausComponents, ushort usnMaxComponents)
	{
		ushort num = 0;
		ushort num2 = 0;
		ushort num3 = 0;
		ushort num4;
		<Module>.MS.Internal.TtfDelta.GetComponentGlyphList(pInputBufferInfo, usGlyphIdx, &num4, pausComponents, usnMaxComponents, pusnComponentDepth, 0, indexToLocFormat, ulLocaOffset, ulGlyfOffset);
		ushort num5 = *pusnComponentElements;
		ushort num6 = (num5 > num4) ? num5 : num4;
		*pusnComponentElements = num6;
		ushort num7 = 0;
		if (0 < num4)
		{
			do
			{
				short num9;
				ushort num10;
				ushort num11;
				int num12;
				short num8 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGlyphStats(pInputBufferInfo, (num7 * 2L)[pausComponents / 2], &num9, &num10, &num11, indexToLocFormat, ulLocaOffset, ulGlyfOffset, &num12);
				if (num8 != 0)
				{
					return num8;
				}
				if (num12 == 1 && num9 > 0)
				{
					num = (ushort)(num9 + (short)num);
					num2 = num10 + num2;
					ushort num13 = (num3 > num11) ? num3 : num11;
					num3 = num13;
				}
				num7 += 1;
			}
			while (num7 < num4);
		}
		*psnContours = (short)num;
		*pusnPoints = num2;
		*pusnInstructions = num3;
		return 0;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00009C40 File Offset: 0x00009040
	[SecurityCritical]
	internal unsafe static short ComputeMaxPStats(TTFACC_FILEBUFFERINFO* pInputBufferInfo, ushort* pusMaxContours, ushort* pusMaxPoints, ushort* pusMaxCompositeContours, ushort* pusMaxCompositePoints, ushort* pusMaxInstructions, ushort* pusMaxComponentElements, ushort* pusMaxComponentDepth, ushort* pausComponents, ushort usnMaxComponents)
	{
		*pusMaxContours = 0;
		*pusMaxPoints = 0;
		*pusMaxInstructions = 0;
		*pusMaxCompositeContours = 0;
		*pusMaxCompositePoints = 0;
		*pusMaxComponentElements = 0;
		*pusMaxComponentDepth = 0;
		ushort num = <Module>.MS.Internal.TtfDelta.GetNumGlyphs(pInputBufferInfo);
		if (num == 0)
		{
			return 1009;
		}
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DACNFKGE@loca@));
		if (num2 == null)
		{
			return 1035;
		}
		uint num3 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04LLIHEPK@glyf@));
		if (num3 == null)
		{
			return 1031;
		}
		HEAD head;
		if (<Module>.MS.Internal.TtfDelta.GetHead(pInputBufferInfo, &head) == null)
		{
			return 1032;
		}
		ushort num4 = 0;
		ushort num15;
		if (0 < num)
		{
			for (;;)
			{
				short num6;
				ushort num7;
				ushort num8;
				int num9;
				short num5 = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetGlyphStats(pInputBufferInfo, num4, &num6, &num7, &num8, (ushort)(*(ref head + 50)), num2, num3, &num9);
				if (num5 != 0)
				{
					return num5;
				}
				if (num9 != 0)
				{
					if (num6 >= 0)
					{
						ushort num10 = (ushort)num6;
						ushort num11 = *pusMaxContours;
						ushort num12 = (num11 > num10) ? num11 : num10;
						*pusMaxContours = num12;
						ushort num13 = *pusMaxPoints;
						ushort num14 = (num13 > num7) ? num13 : num7;
						*pusMaxPoints = num14;
						num15 = *pusMaxInstructions;
						ushort num16 = (num15 > num8) ? num15 : num8;
						*pusMaxInstructions = num16;
					}
					else
					{
						if (num6 != -1)
						{
							break;
						}
						num8 = 0;
						ushort num17 = 0;
						ushort num18 = 0;
						<Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.GetCompositeGlyphStats(pInputBufferInfo, num4, &num6, &num7, &num8, &num18, &num17, (ushort)(*(ref head + 50)), num2, num3, pausComponents, usnMaxComponents);
						ushort num19 = (ushort)num6;
						ushort num20 = *pusMaxCompositeContours;
						ushort num21 = (num20 > num19) ? num20 : num19;
						*pusMaxCompositeContours = num21;
						ushort num22 = *pusMaxCompositePoints;
						ushort num23 = (num22 > num7) ? num22 : num7;
						*pusMaxCompositePoints = num23;
						num15 = *pusMaxInstructions;
						ushort num24 = (num15 > num8) ? num15 : num8;
						*pusMaxInstructions = num24;
						ushort num25 = *pusMaxComponentElements;
						ushort num26 = (num25 > num18) ? num25 : num18;
						*pusMaxComponentElements = num26;
						ushort num27 = *pusMaxComponentDepth;
						ushort num28 = (num27 > num17) ? num27 : num17;
						*pusMaxComponentDepth = num28;
					}
				}
				num4 += 1;
				if (num4 >= num)
				{
					goto IL_1A9;
				}
			}
			return 1061;
		}
		IL_1A9:
		ushort num29 = <Module>.MS.Internal.TtfDelta.TTTableLength(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04GABKPAAH@prep@));
		ushort num30 = <Module>.MS.Internal.TtfDelta.TTTableLength(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04NJFLOCNM@fpgm@));
		ushort num31 = (num29 > num30) ? num29 : num30;
		num15 = *pusMaxInstructions;
		ushort num33;
		if (num31 > num15)
		{
			ushort num32 = (num29 > num30) ? num29 : num30;
			num33 = num32;
		}
		else
		{
			num33 = num15;
		}
		*pusMaxInstructions = num33;
		return 0;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00009E40 File Offset: 0x00009240
	[SecurityCritical]
	internal unsafe static short MortAutoMap(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* pabKeepGlyphs, ushort usnGlyphs, ushort fKeepFlag)
	{
		uint num = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04CHBMFMIH@mort@));
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableLength(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04CHBMFMIH@mort@));
		uint num3 = num2 + num;
		if (num == null || num2 == null)
		{
			return 0;
		}
		num += <Module>.MS.Internal.TtfDelta.GetGenericSize((byte*)(&<Module>.MS.Internal.TtfDelta.MORTHEADER_CONTROL));
		MORTBINSRCHHEADER mortbinsrchheader;
		ushort num5;
		short num4 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&mortbinsrchheader), 10, (byte*)(&<Module>.MS.Internal.TtfDelta.MORTBINSRCHHEADER_CONTROL), num, &num5);
		if (num4 != 0)
		{
			return num4;
		}
		num += num5;
		ushort num6 = *(ref mortbinsrchheader + 2);
		if (*(ref mortbinsrchheader + 2) > 0)
		{
			while (num < num3)
			{
				MORTLOOKUPSINGLE mortlookupsingle;
				num4 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&mortlookupsingle), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.MORTLOOKUPSINGLE_CONTROL), num, &num5);
				if (num4 != 0)
				{
					return num4;
				}
				num += num5;
				if (mortlookupsingle < usnGlyphs && (ushort)mortlookupsingle[pabKeepGlyphs] == fKeepFlag && *(ref mortlookupsingle + 2) < usnGlyphs)
				{
					byte* ptr = *(ref mortlookupsingle + 2) + pabKeepGlyphs;
					if (*ptr == 0)
					{
						*ptr = (byte)(fKeepFlag + 1);
					}
				}
				num6 += ushort.MaxValue;
				if (num6 <= 0)
				{
					break;
				}
			}
		}
		return 0;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00009F10 File Offset: 0x00009310
	[SecurityCritical]
	internal unsafe static short UpdateKeepWithCoverage(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* pabKeepGlyphs, ushort usnGlyphs, ushort fKeepFlag, uint ulBaseOffset, uint ulCoverageOffset, ushort* pArray, ushort usLookupType, ushort usSubstFormat)
	{
		ushort* ptr = null;
		GSUBRANGERECORD* ptr2 = null;
		if (ulCoverageOffset == null || pArray == null)
		{
			return 0;
		}
		uint num = ulBaseOffset + ulCoverageOffset;
		ushort num3;
		short num2 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num3, num);
		if (num2 != 0)
		{
			return num2;
		}
		int num4 = (int)num3;
		ushort num6;
		ushort num7;
		uint num9;
		short num8;
		if (num4 != 1)
		{
			if (num4 != 2)
			{
				return 1080;
			}
			GSUBCOVERAGEFORMAT2 gsubcoverageformat;
			short num5 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubcoverageformat), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBCOVERAGEFORMAT2_CONTROL), num, &num6);
			if (num5 != 0)
			{
				return num5;
			}
			num += num6;
			num7 = *(ref gsubcoverageformat + 2);
			ptr2 = (GSUBRANGERECORD*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(*(ref gsubcoverageformat + 2) * 8)));
			if (ptr2 == null)
			{
				return 1005;
			}
			num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr2, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBRANGERECORD_CONTROL), num, &num9, num7, 8);
			if (num8 != 0)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
				return num8;
			}
		}
		else
		{
			GSUBCOVERAGEFORMAT1 gsubcoverageformat2;
			short num10 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubcoverageformat2), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBCOVERAGEFORMAT1_CONTROL), num, &num6);
			if (num10 != 0)
			{
				return num10;
			}
			num += num6;
			num7 = *(ref gsubcoverageformat2 + 2);
			ptr = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsubcoverageformat2 + 2)) * 2UL);
			if (ptr == null)
			{
				return 1005;
			}
			num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num, &num9, num7, 2);
			if (num8 != 0)
			{
				<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
				return num8;
			}
		}
		ushort num11 = 0;
		ushort num12 = 0;
		ushort num13 = 0;
		if (0 < num7)
		{
			while (num8 == 0)
			{
				ushort num14;
				if (num3 == 1)
				{
					num14 = (num11 * 2L)[ptr / 2];
					num11 += 1;
				}
				else
				{
					GSUBRANGERECORD* ptr3 = num11 * 8L + ptr2 / sizeof(GSUBRANGERECORD);
					num14 = num12 + *(ushort*)ptr3;
					if (num14 < *(ushort*)(ptr3 + 2L / (long)sizeof(GSUBRANGERECORD)))
					{
						num12 += 1;
					}
					else
					{
						num11 += 1;
						num12 = 0;
					}
				}
				if (num14 < usnGlyphs && (ushort)num14[pabKeepGlyphs] == fKeepFlag)
				{
					if (usLookupType != 1)
					{
						if (usLookupType != 2)
						{
							if (usLookupType != 3)
							{
								if (usLookupType == 4)
								{
									ushort* ptr4 = num13 * 2L + pArray / 2;
									ushort num15 = *ptr4;
									if (num15 != 0)
									{
										num = num15 + ulBaseOffset;
										ushort num16;
										num8 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num16, num);
										if (num8 == 0)
										{
											ushort* ptr5 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num16 * 2UL);
											if (ptr5 == null)
											{
												num8 = 1005;
											}
											else
											{
												num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr5, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), (uint)(num + 2UL), &num9, num16, 2);
												if (num8 == 0)
												{
													ushort num17 = 0;
													if (0 < num16)
													{
														ushort* ptr6;
														for (;;)
														{
															ushort num18 = (num17 * 2L)[ptr5 / 2];
															if (num18 != 0)
															{
																num = num18 + *ptr4 + ulBaseOffset;
																GSUBLIGATURE gsubligature;
																num8 = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubligature), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBLIGATURE_CONTROL), num, &num6);
																if (num8 != 0)
																{
																	goto IL_4D0;
																}
																num += num6;
																ushort num19 = *(ref gsubligature + 2);
																if (gsubligature < usnGlyphs)
																{
																	long num20 = (long)gsubligature;
																	long num21 = num20;
																	if (num20[pabKeepGlyphs] == 0)
																	{
																		ptr6 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(*(ref gsubligature + 2) - 1) * 2L));
																		if (ptr6 == null)
																		{
																			goto IL_4B7;
																		}
																		int num22 = (int)(num19 - 1);
																		num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr6, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num, &num9, (ushort)num22, 2);
																		if (num8 != 0)
																		{
																			break;
																		}
																		ushort num23 = 0;
																		if (0 < num22)
																		{
																			do
																			{
																				ushort num24 = (num23 * 2L)[ptr6 / 2];
																				if (num24 >= usnGlyphs || num24[pabKeepGlyphs] == 0)
																				{
																					break;
																				}
																				num23 += 1;
																			}
																			while ((int)num23 < num22);
																		}
																		if ((int)num23 == num22 && num21[pabKeepGlyphs] == 0)
																		{
																			num21[pabKeepGlyphs] = (byte)(fKeepFlag + 1);
																		}
																		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr6);
																	}
																}
															}
															num17 += 1;
															if (num17 >= num16)
															{
																goto IL_2EF;
															}
														}
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr6);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
														return num8;
														IL_4B7:
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
														return 1005;
														IL_4D0:
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
														<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
														return num8;
													}
												}
												IL_2EF:
												<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
											}
										}
									}
								}
							}
							else
							{
								ushort num25 = (num13 * 2L)[pArray / 2];
								if (num25 != 0)
								{
									num = num25 + ulBaseOffset;
									ushort num26;
									num8 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num26, num);
									if (num8 == 0)
									{
										ushort* ptr7 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num26 * 2UL);
										if (ptr7 == null)
										{
											num8 = 1005;
										}
										else
										{
											num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr7, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), (uint)(num + 2UL), &num9, num26, 2);
											if (num8 == 0)
											{
												ushort num27 = 0;
												if (0 < num26)
												{
													do
													{
														ushort num28 = (num27 * 2L)[ptr7 / 2];
														if (num28 < usnGlyphs)
														{
															byte* ptr8 = num28 + pabKeepGlyphs;
															if (*ptr8 == 0)
															{
																*ptr8 = (byte)(fKeepFlag + 1);
															}
														}
														num27 += 1;
													}
													while (num27 < num26);
												}
											}
											<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr7);
										}
									}
								}
							}
						}
						else
						{
							ushort num29 = (num13 * 2L)[pArray / 2];
							if (num29 != 0)
							{
								num = num29 + ulBaseOffset;
								ushort num30;
								num8 = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num30, num);
								if (num8 == 0)
								{
									ushort* ptr9 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)num30 * 2UL);
									if (ptr9 == null)
									{
										num8 = 1005;
									}
									else
									{
										num8 = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr9, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), (uint)(num + 2UL), &num9, num30, 2);
										if (num8 == 0)
										{
											ushort num31 = 0;
											if (0 < num30)
											{
												do
												{
													ushort num32 = (num31 * 2L)[ptr9 / 2];
													if (num32 < usnGlyphs)
													{
														byte* ptr10 = num32 + pabKeepGlyphs;
														if (*ptr10 == 0)
														{
															*ptr10 = (byte)(fKeepFlag + 1);
														}
													}
													num31 += 1;
												}
												while (num31 < num30);
											}
										}
										<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr9);
									}
								}
							}
						}
					}
					else if (usSubstFormat == 1)
					{
						int num33 = (int)((short)(*pArray) + (short)num14);
						if (num33 < (int)usnGlyphs)
						{
							byte* ptr11 = num33 + pabKeepGlyphs;
							if (*ptr11 == 0)
							{
								*ptr11 = (byte)(fKeepFlag + 1);
							}
						}
					}
					else
					{
						ushort num34 = (num13 * 2L)[pArray / 2];
						if (num34 < usnGlyphs)
						{
							byte* ptr12 = num34 + pabKeepGlyphs;
							if (*ptr12 == 0)
							{
								*ptr12 = (byte)(fKeepFlag + 1);
							}
						}
					}
				}
				num13 += 1;
				if (num11 >= num7)
				{
					break;
				}
			}
		}
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr2);
		return num8;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000A414 File Offset: 0x00009814
	[SecurityCritical]
	internal unsafe static short ProcessBaseCoord(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, byte* pabKeepGlyphs, ushort usnGlyphs, ushort fKeepFlag)
	{
		ushort num2;
		short num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num2, ulOffset);
		if (num != 0)
		{
			return num;
		}
		if (num2 != 2)
		{
			return 0;
		}
		BASECOORDFORMAT2 basecoordformat;
		ushort num3;
		num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basecoordformat), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASECOORDFORMAT2_CONTROL), ulOffset, &num3);
		if (num != 0)
		{
			return num;
		}
		if (*(ref basecoordformat + 4) < usnGlyphs)
		{
			byte* ptr = *(ref basecoordformat + 4) + pabKeepGlyphs;
			if (*ptr == 0)
			{
				*ptr = (byte)(fKeepFlag + 1);
			}
		}
		return 0;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000A46C File Offset: 0x0000986C
	[SecurityCritical]
	internal unsafe static short ProcessMinMax(TTFACC_FILEBUFFERINFO* pInputBufferInfo, uint ulOffset, byte* pabKeepGlyphs, ushort usnGlyphs, ushort fKeepFlag)
	{
		BASEMINMAX baseminmax;
		ushort num2;
		short num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&baseminmax), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASEMINMAX_CONTROL), ulOffset, &num2);
		if (num != 0)
		{
			return num;
		}
		if (baseminmax != null)
		{
			num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessBaseCoord(pInputBufferInfo, baseminmax + ulOffset, pabKeepGlyphs, usnGlyphs, fKeepFlag);
			if (num != 0)
			{
				return num;
			}
		}
		if (*(ref baseminmax + 2) != 0)
		{
			num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessBaseCoord(pInputBufferInfo, *(ref baseminmax + 2) + ulOffset, pabKeepGlyphs, usnGlyphs, fKeepFlag);
			if (num != 0)
			{
				return num;
			}
		}
		uint num3 = num2 + ulOffset;
		ushort num4 = 0;
		if (0 < *(ref baseminmax + 4))
		{
			for (;;)
			{
				BASEFEATMINMAXRECORD basefeatminmaxrecord;
				num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basefeatminmaxrecord), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASEFEATMINMAXRECORD_CONTROL), num3, &num2);
				if (num != 0)
				{
					return num;
				}
				num3 += num2;
				if (*(ref basefeatminmaxrecord + 4) != 0)
				{
					num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessBaseCoord(pInputBufferInfo, *(ref basefeatminmaxrecord + 4) + ulOffset, pabKeepGlyphs, usnGlyphs, fKeepFlag);
					if (num != 0)
					{
						break;
					}
				}
				if (*(ref basefeatminmaxrecord + 6) != 0)
				{
					num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessBaseCoord(pInputBufferInfo, *(ref basefeatminmaxrecord + 6) + ulOffset, pabKeepGlyphs, usnGlyphs, fKeepFlag);
					if (num != 0)
					{
						return num;
					}
				}
				num4 += 1;
				if (num4 >= *(ref baseminmax + 4))
				{
					return 0;
				}
			}
			return num;
		}
		return 0;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000A53C File Offset: 0x0000993C
	[SecurityCritical]
	internal unsafe static short TTOAutoMap(TTFACC_FILEBUFFERINFO* pInputBufferInfo, byte* pabKeepGlyphs, ushort usnGlyphs, ushort fKeepFlag)
	{
		short num = 0;
		uint num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04KCIOONCL@GSUB@));
		ushort num3;
		uint num6;
		if (num2 != null)
		{
			GSUBHEADER gsubheader;
			num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubheader), 10, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBHEADER_CONTROL), num2, &num3);
			if (num != 0)
			{
				return num;
			}
			if (*(ref gsubheader + 8) != 0)
			{
				GSUBLOOKUPLIST gsublookuplist;
				num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsublookuplist), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBLOOKUPLIST_CONTROL), *(ref gsubheader + 8) + num2, &num3);
				if (num != 0)
				{
					return num;
				}
				ushort num4 = gsublookuplist;
				if (gsublookuplist != null)
				{
					uint num5 = *(ref gsubheader + 8) + num2;
					GSUBLOOKUPLIST* ptr = (GSUBLOOKUPLIST*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((gsublookuplist + 1UL) * 2UL);
					if (ptr == null)
					{
						num = 1005;
					}
					else
					{
						num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)ptr, 2, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBLOOKUPLIST_CONTROL), num5, &num3);
						if (num == 0)
						{
							num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)(ptr + 2L / (long)sizeof(GSUBLOOKUPLIST)), (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, num4, 2);
							if (num == 0)
							{
								ushort num7 = 0;
								if (0 < num4)
								{
									for (;;)
									{
										GSUBLOOKUPLIST* ptr2 = (num7 + 1UL / (ulong)sizeof(GSUBLOOKUPLIST)) * 2L + ptr / sizeof(GSUBLOOKUPLIST);
										ushort num8 = *(ushort*)ptr2;
										if (num8 != 0)
										{
											num5 = num8 + *(ref gsubheader + 8) + num2;
											GSUBLOOKUP gsublookup;
											num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsublookup), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBLOOKUP_CONTROL), num5, &num3);
											if (num != 0)
											{
												goto IL_423;
											}
											if (gsublookup != 5)
											{
												ushort num9 = *(ref gsublookup + 4);
												ushort* ptr3 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsublookup + 4)) * 2UL);
												if (ptr3 == null)
												{
													break;
												}
												num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr3, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, num9, 2);
												if (num != 0)
												{
													goto IL_423;
												}
												ushort num10 = 0;
												if (0 < num9)
												{
													for (;;)
													{
														ushort num11 = (num10 * 2L)[ptr3 / 2];
														if (num11 != 0)
														{
															num5 = *(ushort*)ptr2 + num11 + *(ref gsubheader + 8) + num2;
															ushort num12;
															num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num12, num5);
															if (num != 0)
															{
																goto IL_402;
															}
															int num13 = gsublookup;
															if (num13 != 1)
															{
																if (num13 != 2)
																{
																	if (num13 != 3)
																	{
																		if (num13 != 4 || num12 != 1)
																		{
																			goto IL_3CB;
																		}
																		GSUBLIGATURESUBSTFORMAT1 gsubligaturesubstformat;
																		num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubligaturesubstformat), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBLIGATURESUBSTFORMAT1_CONTROL), num5, &num3);
																		if (num != 0)
																		{
																			goto IL_402;
																		}
																		ushort usItemCount = *(ref gsubligaturesubstformat + 4);
																		ushort* ptr4 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsubligaturesubstformat + 4)) * 2UL);
																		if (ptr4 == null)
																		{
																			break;
																		}
																		num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr4, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, usItemCount, 2);
																		if (num == 0)
																		{
																			num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdateKeepWithCoverage(pInputBufferInfo, pabKeepGlyphs, usnGlyphs, fKeepFlag, num5, *(ref gsubligaturesubstformat + 2), ptr4, gsublookup, num12);
																		}
																		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr4);
																	}
																	else
																	{
																		if (num12 != 1)
																		{
																			goto IL_3CB;
																		}
																		GSUBALTERNATESUBSTFORMAT1 gsubalternatesubstformat;
																		num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubalternatesubstformat), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBALTERNATESUBSTFORMAT1_CONTROL), num5, &num3);
																		if (num != 0)
																		{
																			goto IL_402;
																		}
																		ushort usItemCount2 = *(ref gsubalternatesubstformat + 4);
																		ushort* ptr5 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsubalternatesubstformat + 4)) * 2UL);
																		if (ptr5 == null)
																		{
																			goto IL_3E4;
																		}
																		num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr5, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, usItemCount2, 2);
																		if (num == 0)
																		{
																			num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdateKeepWithCoverage(pInputBufferInfo, pabKeepGlyphs, usnGlyphs, fKeepFlag, num5, *(ref gsubalternatesubstformat + 2), ptr5, gsublookup, num12);
																		}
																		<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr5);
																	}
																}
																else
																{
																	if (num12 != 1)
																	{
																		goto IL_3CB;
																	}
																	GSUBMULTIPLESUBSTFORMAT1 gsubmultiplesubstformat;
																	num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubmultiplesubstformat), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBMULTIPLESUBSTFORMAT1_CONTROL), num5, &num3);
																	if (num != 0)
																	{
																		goto IL_402;
																	}
																	ushort usItemCount3 = *(ref gsubmultiplesubstformat + 4);
																	ushort* ptr6 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsubmultiplesubstformat + 4)) * 2UL);
																	if (ptr6 == null)
																	{
																		goto IL_3EC;
																	}
																	num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr6, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, usItemCount3, 2);
																	if (num == 0)
																	{
																		num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdateKeepWithCoverage(pInputBufferInfo, pabKeepGlyphs, usnGlyphs, fKeepFlag, num5, *(ref gsubmultiplesubstformat + 2), ptr6, gsublookup, num12);
																	}
																	<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr6);
																}
															}
															else
															{
																int num14 = (int)num12;
																if (num14 != 1)
																{
																	if (num14 != 2)
																	{
																		goto IL_3FC;
																	}
																	GSUBSINGLESUBSTFORMAT2 gsubsinglesubstformat;
																	num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubsinglesubstformat), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBSINGLESUBSTFORMAT2_CONTROL), num5, &num3);
																	if (num != 0)
																	{
																		goto IL_402;
																	}
																	ushort usItemCount4 = *(ref gsubsinglesubstformat + 4);
																	ushort* ptr7 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)(*(ref gsubsinglesubstformat + 4)) * 2UL);
																	if (ptr7 == null)
																	{
																		goto IL_3F4;
																	}
																	num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr7, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num5, &num6, usItemCount4, 2);
																	if (num == 0)
																	{
																		num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdateKeepWithCoverage(pInputBufferInfo, pabKeepGlyphs, usnGlyphs, fKeepFlag, num5, *(ref gsubsinglesubstformat + 2), ptr7, gsublookup, num12);
																	}
																	<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr7);
																}
																else
																{
																	GSUBSINGLESUBSTFORMAT1 gsubsinglesubstformat2;
																	num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&gsubsinglesubstformat2), 6, (byte*)(&<Module>.MS.Internal.TtfDelta.GSUBSINGLESUBSTFORMAT1_CONTROL), num5, &num3);
																	if (num != 0)
																	{
																		goto IL_402;
																	}
																	num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.UpdateKeepWithCoverage(pInputBufferInfo, pabKeepGlyphs, usnGlyphs, fKeepFlag, num5, *(ref gsubsinglesubstformat2 + 2), ref gsubsinglesubstformat2 + 4, gsublookup, num12);
																}
															}
															if (num != 0)
															{
																goto IL_402;
															}
														}
														IL_3CB:
														num10 += 1;
														if (num10 >= num9)
														{
															goto IL_402;
														}
													}
													num = 1005;
													goto IL_402;
													IL_3E4:
													num = 1005;
													goto IL_402;
													IL_3EC:
													num = 1005;
													goto IL_402;
													IL_3F4:
													num = 1005;
													goto IL_402;
													IL_3FC:
													num = 1081;
												}
												IL_402:
												<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr3);
												if (num != 0)
												{
													goto IL_423;
												}
											}
										}
										num7 += 1;
										if (num7 >= num4)
										{
											goto IL_423;
										}
									}
									num = 1005;
								}
							}
						}
					}
					IL_423:
					<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr);
					if (num != 0)
					{
						return num;
					}
				}
			}
		}
		num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04DPLAIGKJ@JSTF@));
		if (num2 != null)
		{
			JSTFHEADER jstfheader;
			num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&jstfheader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.JSTFHEADER_CONTROL), num2, &num3);
			if (num == 0)
			{
				JSTFSCRIPTRECORD* ptr8 = (JSTFSCRIPTRECORD*)<Module>.MS.Internal.TtfDelta.Mem_Alloc((ulong)((long)(*(ref jstfheader + 4) * 8)));
				if (ptr8 == null)
				{
					num = 1005;
				}
				else
				{
					num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr8, (byte*)(&<Module>.MS.Internal.TtfDelta.JSTFSCRIPTRECORD_CONTROL), num3 + num2, &num6, *(ref jstfheader + 4), 8);
					if (num == 0)
					{
						ushort num15 = 0;
						if (0 < *(ref jstfheader + 4))
						{
							for (;;)
							{
								JSTFSCRIPTRECORD* ptr9 = ptr8 + (ulong)num15 * 8UL / (ulong)sizeof(JSTFSCRIPTRECORD) + 4L / (long)sizeof(JSTFSCRIPTRECORD);
								ushort num16 = *(ushort*)ptr9;
								if (num16 != 0)
								{
									uint ulOffset = num16 + num2;
									JSTFSCRIPT jstfscript;
									num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&jstfscript), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.JSTFSCRIPT_CONTROL), ulOffset, &num3);
									if (num != 0)
									{
										goto IL_593;
									}
									if (jstfscript != null)
									{
										uint num17 = *(ushort*)ptr9 + jstfscript + num2;
										JSTFEXTENDERGLYPH jstfextenderglyph;
										num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&jstfextenderglyph), 2, (byte*)(&<Module>.MS.Internal.TtfDelta.JSTFEXTENDERGLYPH_CONTROL), num17, &num3);
										if (num != 0)
										{
											goto IL_593;
										}
										ushort* ptr10 = (ushort*)<Module>.MS.Internal.TtfDelta.Mem_Alloc(jstfextenderglyph * 2UL);
										if (ptr10 == null)
										{
											break;
										}
										num = <Module>.MS.Internal.TtfDelta.ReadGenericRepeat(pInputBufferInfo, (byte*)ptr10, (byte*)(&<Module>.MS.Internal.TtfDelta.WORD_CONTROL), num3 + num17, &num6, jstfextenderglyph, 2);
										if (num == 0)
										{
											ushort num18 = 0;
											if (0 < jstfextenderglyph)
											{
												do
												{
													ushort num19 = (num18 * 2L)[ptr10 / 2];
													if (num19 < usnGlyphs)
													{
														byte* ptr11 = num19 + pabKeepGlyphs;
														if (*ptr11 == 0)
														{
															*ptr11 = (byte)(fKeepFlag + 1);
														}
													}
													num18 += 1;
												}
												while (num18 < jstfextenderglyph);
											}
										}
										<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr10);
										if (num != 0)
										{
											goto IL_593;
										}
									}
								}
								num15 += 1;
								if (num15 >= *(ref jstfheader + 4))
								{
									goto IL_593;
								}
							}
							num = 1005;
						}
						IL_593:
						<Module>.MS.Internal.TtfDelta.Mem_Free((void*)ptr8);
						if (num == 0)
						{
							goto IL_59F;
						}
					}
				}
			}
			return num;
		}
		IL_59F:
		num2 = <Module>.MS.Internal.TtfDelta.TTTableOffset(pInputBufferInfo, (sbyte*)(&<Module>.??_C@_04NLLCBHDK@BASE@));
		if (num2 != null)
		{
			BASEHEADER baseheader;
			num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&baseheader), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASEHEADER_CONTROL), num2, &num3);
			if (num == 0)
			{
				ushort num20 = *(ref baseheader + 4);
				ushort num21 = 0;
				do
				{
					if (num20 != 0)
					{
						BASEAXIS baseaxis;
						num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&baseaxis), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.BASEAXIS_CONTROL), num20 + num2, &num3);
						if (num != 0)
						{
							break;
						}
						if (*(ref baseaxis + 2) != 0)
						{
							uint num22 = *(ref baseaxis + 2) + num20 + num2;
							BASESCRIPTLIST basescriptlist;
							num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basescriptlist), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.BASESCRIPTLIST_CONTROL), num22, &num3);
							if (num != 0)
							{
								break;
							}
							uint num23 = num3 + num22;
							ushort num24 = 0;
							if (0 < basescriptlist)
							{
								do
								{
									BASESCRIPTRECORD basescriptrecord;
									num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basescriptrecord), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASESCRIPTRECORD_CONTROL), num23, &num3);
									if (num != 0)
									{
										return num;
									}
									num23 += num3;
									if (*(ref basescriptrecord + 4) != 0)
									{
										BASESCRIPT basescript;
										num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basescript), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASESCRIPT_CONTROL), *(ref basescriptrecord + 4) + num22, &num3);
										if (num != 0)
										{
											return num;
										}
										uint num25 = *(ref basescriptrecord + 4) + num3 + num22;
										if (basescript != null)
										{
											uint num26 = basescript + *(ref basescriptrecord + 4) + num22;
											BASEVALUES basevalues;
											num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&basevalues), 4, (byte*)(&<Module>.MS.Internal.TtfDelta.BASEVALUES_CONTROL), num26, &num3);
											if (num != 0)
											{
												return num;
											}
											num26 += num3;
											ushort num27 = 0;
											if (0 < *(ref basevalues + 2))
											{
												do
												{
													ushort num28;
													num = <Module>.MS.Internal.TtfDelta.ReadWord(pInputBufferInfo, &num28, num26);
													if (num != 0)
													{
														return num;
													}
													num26 = (uint)(num26 + 2UL);
													num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessBaseCoord(pInputBufferInfo, num28 + basescript + *(ref basescriptrecord + 4) + num22, pabKeepGlyphs, usnGlyphs, fKeepFlag);
													if (num != 0)
													{
														return num;
													}
													num27 += 1;
												}
												while (num27 < *(ref basevalues + 2));
											}
										}
										if (*(ref basescript + 2) != 0)
										{
											uint ulOffset2 = *(ref basescript + 2) + *(ref basescriptrecord + 4) + num22;
											num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessMinMax(pInputBufferInfo, ulOffset2, pabKeepGlyphs, usnGlyphs, fKeepFlag);
											if (num != 0)
											{
												return num;
											}
										}
										ushort num29 = 0;
										if (0 < *(ref basescript + 4))
										{
											do
											{
												BASELANGSYSRECORD baselangsysrecord;
												num = <Module>.MS.Internal.TtfDelta.ReadGeneric(pInputBufferInfo, (byte*)(&baselangsysrecord), 8, (byte*)(&<Module>.MS.Internal.TtfDelta.BASELANGSYSRECORD_CONTROL), num25, &num3);
												if (num != 0)
												{
													return num;
												}
												num25 += num3;
												if (*(ref baselangsysrecord + 4) != 0)
												{
													num = <Module>.MS.Internal.TtfDelta.?A0x67cd8d4d.ProcessMinMax(pInputBufferInfo, *(ref baselangsysrecord + 4) + *(ref basescriptrecord + 4) + num22, pabKeepGlyphs, usnGlyphs, fKeepFlag);
													if (num != 0)
													{
														return num;
													}
												}
												num29 += 1;
											}
											while (num29 < *(ref basescript + 4));
										}
										if (num != 0)
										{
											return num;
										}
									}
									num24 += 1;
								}
								while (num24 < basescriptlist);
								if (num != 0)
								{
									break;
								}
							}
						}
					}
					num21 += 1;
					num20 = *(ref baseheader + 6);
				}
				while (num21 < 2);
			}
		}
		return num;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x0000AD08 File Offset: 0x0000A108
	[SecurityCritical]
	internal static ushort log2(ushort arg)
	{
		if (arg < 2)
		{
			return 0;
		}
		if (arg < 4)
		{
			return 1;
		}
		if (arg < 8)
		{
			return 2;
		}
		if (arg < 16)
		{
			return 3;
		}
		if (arg < 32)
		{
			return 4;
		}
		if (arg < 64)
		{
			return 5;
		}
		if (arg < 128)
		{
			return 6;
		}
		if (arg < 256)
		{
			return 7;
		}
		if (arg < 512)
		{
			return 8;
		}
		if (arg < 1024)
		{
			return 9;
		}
		if (arg < 2048)
		{
			return 10;
		}
		if (arg < 4096)
		{
			return 11;
		}
		if (arg < 8192)
		{
			return 12;
		}
		if (arg < 16384)
		{
			return 13;
		}
		return (arg < 32768) ? 14 : 15;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00005F58 File Offset: 0x00005358
	internal static int HRESULT_FROM_WIN32(uint x)
	{
		return (x > 0) ? ((x & 65535) | -2147024896) : x;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00012C00 File Offset: 0x00012000
	[SecurityCritical]
	internal static int atexit(method _Function)
	{
		return <Module>._atexit_m_appdomain(_Function);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00001790 File Offset: 0x00000B90
	internal static void ??__E?A0x5bf621e4@SA_Yes@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_Yes = (YesNoMaybe)268370176;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000017A8 File Offset: 0x00000BA8
	internal static void ??__E?A0x5bf621e4@SA_No@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_No = (YesNoMaybe)268369921;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000017C0 File Offset: 0x00000BC0
	internal static void ??__E?A0x5bf621e4@SA_Maybe@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_Maybe = (YesNoMaybe)268369936;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000017D8 File Offset: 0x00000BD8
	internal static void ??__E?A0x5bf621e4@SA_NoAccess@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_NoAccess = (AccessType)0;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000017EC File Offset: 0x00000BEC
	internal static void ??__E?A0x5bf621e4@SA_Read@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_Read = (AccessType)1;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00001800 File Offset: 0x00000C00
	internal static void ??__E?A0x5bf621e4@SA_Write@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_Write = (AccessType)2;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00001814 File Offset: 0x00000C14
	internal static void ??__E?A0x5bf621e4@SA_ReadWrite@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.SA_ReadWrite = (AccessType)3;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00012F68 File Offset: 0x00012368
	[SecuritySafeCritical]
	internal unsafe static CModuleInitialize* {ctor}(CModuleInitialize* A_0, method cleaningUpFunc)
	{
		<Module>.CModuleInitialize.IsProcessDpiAware(A_0);
		NativeWPFDLLLoader.LoadCommonDLLsAndDwrite();
		GlobalInit.Init();
		ControlTableInit.Init();
		<Module>.atexit(cleaningUpFunc);
		return A_0;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00012D6C File Offset: 0x0001216C
	[SecuritySafeCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void UnInitialize(CModuleInitialize* A_0)
	{
		NativeWPFDLLLoader.UnloadCommonDLLs();
		NativeWPFDLLLoader.ClearDWriteCreateFactoryFunctionPointer();
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00012D84 File Offset: 0x00012184
	[SecurityCritical]
	internal unsafe static void* GetDWriteCreateFactoryFunctionPointer(CModuleInitialize* A_0)
	{
		return NativeWPFDLLLoader.GetDWriteCreateFactoryFunctionPointer();
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00012D98 File Offset: 0x00012198
	[SecuritySafeCritical]
	internal unsafe static void IsProcessDpiAware(CModuleInitialize* A_0)
	{
		if (Environment.OSVersion.Version.Major >= 6)
		{
			Type typeFromHandle = typeof(DisableDpiAwarenessAttribute);
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (!(entryAssembly != null) || !Attribute.IsDefined(entryAssembly, typeFromHandle))
			{
				<Module>.SetProcessDPIAware_Internal();
			}
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00012F94 File Offset: 0x00012394
	[SecuritySafeCritical]
	internal unsafe static long InitCmiStartupRunner()
	{
		CModuleInitialize* ptr = (CModuleInitialize*)<Module>.@new(1UL);
		CModuleInitialize* value;
		try
		{
			if (ptr != null)
			{
				value = <Module>.CModuleInitialize.{ctor}(ptr, ldftn(CleanUp));
			}
			else
			{
				value = null;
			}
		}
		catch
		{
			<Module>.delete((void*)ptr);
			throw;
		}
		IntPtr intPtr = new IntPtr((void*)value);
		<Module>.?A0x5bf621e4.cmiStartupRunner = intPtr;
		return <Module>.?A0x5bf621e4.cmiStartupRunner.ToInt64();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00001828 File Offset: 0x00000C28
	internal static void ??__E?A0x5bf621e4@unused@@YMXXZ()
	{
		<Module>.?A0x5bf621e4.unused = <Module>.?A0x5bf621e4.InitCmiStartupRunner();
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00012DE0 File Offset: 0x000121E0
	[SecuritySafeCritical]
	internal unsafe static void CleanUp()
	{
		CModuleInitialize* ptr = (CModuleInitialize*)<Module>.?A0x5bf621e4.cmiStartupRunner.ToPointer();
		<Module>.CModuleInitialize.UnInitialize(ptr);
		<Module>.delete((void*)ptr);
		IntPtr intPtr = new IntPtr(0);
		<Module>.?A0x5bf621e4.cmiStartupRunner = intPtr;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00012E14 File Offset: 0x00012214
	[SecurityCritical]
	internal unsafe static void* GetDWriteCreateFactoryFunctionPointer()
	{
		return <Module>.CModuleInitialize.GetDWriteCreateFactoryFunctionPointer(<Module>.?A0x5bf621e4.cmiStartupRunner.ToPointer());
	}

	// Token: 0x060000B1 RID: 177
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("user32.dll", EntryPoint = "SetProcessDPIAware")]
	public static extern int SetProcessDPIAware_Internal();

	// Token: 0x060000B2 RID: 178 RVA: 0x00001840 File Offset: 0x00000C40
	internal static void ??__E?A0x2d61e9b1@SA_Yes@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_Yes = (YesNoMaybe)268370176;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00001858 File Offset: 0x00000C58
	internal static void ??__E?A0x2d61e9b1@SA_No@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_No = (YesNoMaybe)268369921;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00001870 File Offset: 0x00000C70
	internal static void ??__E?A0x2d61e9b1@SA_Maybe@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_Maybe = (YesNoMaybe)268369936;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00001888 File Offset: 0x00000C88
	internal static void ??__E?A0x2d61e9b1@SA_NoAccess@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_NoAccess = (AccessType)0;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000189C File Offset: 0x00000C9C
	internal static void ??__E?A0x2d61e9b1@SA_Read@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_Read = (AccessType)1;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000018B0 File Offset: 0x00000CB0
	internal static void ??__E?A0x2d61e9b1@SA_Write@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_Write = (AccessType)2;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x000018C4 File Offset: 0x00000CC4
	internal static void ??__E?A0x2d61e9b1@SA_ReadWrite@@YMXXZ()
	{
		<Module>.?A0x2d61e9b1.SA_ReadWrite = (AccessType)3;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00012FFC File Offset: 0x000123FC
	internal static ref char PtrToStringChars(string s)
	{
		ref byte ptr = s;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		return ref ptr;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00013018 File Offset: 0x00012418
	[SecurityCritical]
	internal unsafe static int ReadRegistryString(HKEY__* rootKey, ushort* keyName, ushort* valueName, ushort* value, ulong cchMax)
	{
		HKEY__* ptr = null;
		int num = <Module>.RegOpenKeyExW(rootKey, keyName, 0, 131097, &ptr);
		if (num == null)
		{
			if (cchMax > 2147483647UL)
			{
				num = 87;
			}
			else
			{
				uint num2 = (uint)((ulong)((uint)cchMax) * 2UL);
				uint num3;
				num = <Module>.RegQueryValueExW(ptr, valueName, null, &num3, (byte*)value, &num2);
				if (num == null)
				{
					num = ((num3 != 1) ? 1630 : num);
				}
				<Module>.RegCloseKey(ptr);
			}
		}
		return num;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0001307C File Offset: 0x0001247C
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int GetWPFInstallPath(ushort* pszPath, ulong cchMaxPath)
	{
		int num = 0;
		if (cchMaxPath < 260UL)
		{
			return -2147024882;
		}
		$ArrayType$$$BY0BAE@G $ArrayType$$$BY0BAE@G;
		if (<Module>.GetEnvironmentVariableW((ushort*)(&<Module>.??_C@_1CA@MAEHCNED@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAV?$AAe?$AAr?$AAs?$AAi?$AAo?$AAn@), (ushort*)(&$ArrayType$$$BY0BAE@G), 260) > 0)
		{
			uint num2 = (uint)cchMaxPath;
			if (<Module>.GetEnvironmentVariableW((ushort*)(&<Module>.??_C@_1CI@CAKAMLOG@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl@), pszPath, num2) <= 0)
			{
				int num3 = <Module>.WPFUtils.ReadRegistryString(-2147483646L, (ushort*)(&<Module>.??_C@_1EC@CFPPBACA@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@), (ushort*)(&<Module>.??_C@_1BI@LHIOEDHG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAR?$AAo?$AAo?$AAt@), pszPath, (ulong)num2);
				if (num3 != null)
				{
					num = <Module>.HRESULT_FROM_WIN32(num3);
					if (num < 0)
					{
						return num;
					}
				}
			}
			if (<Module>.PathAppendW(pszPath, (ushort*)(&$ArrayType$$$BY0BAE@G)) == null)
			{
				return -2147024882;
			}
		}
		else
		{
			if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
			{
				string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
				ulong num4 = (ulong)((long)runtimeDirectory.Length);
				if (num4 < cchMaxPath)
				{
					ref ushort uint16_u0020modopt(IsConst)& = ref <Module>.PtrToStringChars(runtimeDirectory);
					<Module>.wcsncpy(pszPath, ref uint16_u0020modopt(IsConst)&, num4);
					(num4 * 2UL)[pszPath / 2] = 0;
				}
				else
				{
					num = -2147024882;
				}
				if (num4 != 0UL)
				{
					goto IL_E3;
				}
			}
			int num5 = <Module>.WPFUtils.ReadRegistryString(-2147483646L, (ushort*)(&<Module>.??_C@_1GK@HPFJOIOL@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@), (ushort*)(&<Module>.??_C@_1BI@NFCPJLAG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAP?$AAa?$AAt?$AAh@), pszPath, cchMaxPath);
			if (num5 != null)
			{
				num = <Module>.HRESULT_FROM_WIN32(num5);
			}
		}
		IL_E3:
		if (num >= 0)
		{
			num = ((<Module>.PathAppendW(pszPath, (ushort*)(&<Module>.??_C@_17HHMPNFFP@?$AAW?$AAP?$AAF@)) == 0) ? -2147024882 : num);
		}
		return num;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00013188 File Offset: 0x00012588
	[SecurityCritical]
	internal unsafe static HINSTANCE__* LoadDWriteLibraryAndGetProcAddress(void** pfncptrDWriteCreateFactory)
	{
		HINSTANCE__* ptr = null;
		HINSTANCE__* moduleHandleW = <Module>.GetModuleHandleW((ushort*)(&<Module>.??_C@_1BK@MGMFAEKH@?$AAk?$AAe?$AAr?$AAn?$AAe?$AAl?$AA3?$AA2?$AA?4?$AAd?$AAl?$AAl@));
		if (moduleHandleW != null)
		{
			if (<Module>.GetProcAddress(moduleHandleW, (sbyte*)(&<Module>.??_C@_0BA@NMFGOKJN@AddDllDirectory@)) != null)
			{
				ptr = <Module>.LoadLibraryExW((ushort*)(&<Module>.??_C@_1BG@HGHNANM@?$AAd?$AAw?$AAr?$AAi?$AAt?$AAe?$AA?4?$AAd?$AAl?$AAl@), null, 2048);
			}
			else
			{
				ptr = <Module>.LoadLibraryW((ushort*)(&<Module>.??_C@_1BG@HGHNANM@?$AAd?$AAw?$AAr?$AAi?$AAt?$AAe?$AA?4?$AAd?$AAl?$AAl@));
			}
			if (ptr != null)
			{
				*(long*)pfncptrDWriteCreateFactory = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.??_C@_0BE@JOALNNKC@DWriteCreateFactory@));
			}
		}
		return ptr;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000018D8 File Offset: 0x00000CD8
	internal static void ??__E?A0x9b4a1587@_HUGE@@YMXXZ()
	{
		<Module>.?A0x9b4a1587._HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000018F4 File Offset: 0x00000CF4
	internal static void ??__E?A0x9b4a1587@HUGE@@YMXXZ()
	{
		<Module>.?A0x9b4a1587.HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00013704 File Offset: 0x00012B04
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000133A0 File Offset: 0x000127A0
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00013434 File Offset: 0x00012834
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00001910 File Offset: 0x00000D10
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00001924 File Offset: 0x00000D24
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00001938 File Offset: 0x00000D38
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000194C File Offset: 0x00000D4C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00001960 File Offset: 0x00000D60
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00001974 File Offset: 0x00000D74
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00001988 File Offset: 0x00000D88
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00013758 File Offset: 0x00012B58
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0001344C File Offset: 0x0001284C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.LanguageSupport.IsDllNameCppSupportLike(LanguageSupport* A_0, string dllName, string prefix)
	{
		if (!dllName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		int length = dllName.Length;
		int length2 = prefix.Length;
		int num = length2;
		if (length2 < length)
		{
			while (dllName[num] >= '0' && dllName[num] <= '9')
			{
				num++;
				if (num >= length)
				{
					break;
				}
			}
			if (num != length2)
			{
				if (num < length && (dllName[num] == 'd' || dllName[num] == 'D'))
				{
					num++;
				}
				int num2;
				if (num + 4 == length && dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 1;
				}
				else
				{
					num2 = 0;
				}
				return (byte)num2 != 0;
			}
		}
		return false;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000134D8 File Offset: 0x000128D8
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.LanguageSupport.IsRuntimeDllName(LanguageSupport* A_0, string dllName)
	{
		if (dllName.Equals("ucrtbased.dll", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (dllName.StartsWith("api-ms-win-crt-", StringComparison.OrdinalIgnoreCase) && dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		int num;
		if (!<Module>.<CrtImplementationDetails>.LanguageSupport.IsDllNameCppSupportLike(A_0, dllName, "msvcp") && !<Module>.<CrtImplementationDetails>.LanguageSupport.IsDllNameCppSupportLike(A_0, dllName, "vcruntime") && !<Module>.<CrtImplementationDetails>.LanguageSupport.IsDllNameCppSupportLike(A_0, dllName, "concrt"))
		{
			num = 0;
		}
		else
		{
			num = 1;
		}
		return (byte)num != 0;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00013548 File Offset: 0x00012948
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.ForceLoadRuntimeApis(LanguageSupport* A_0)
	{
		using (IEnumerator<Module> enumerator = Assembly.GetExecutingAssembly().Modules.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				foreach (MethodInfo methodInfo in enumerator.Current.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributes = methodInfo.GetCustomAttributes(typeof(DllImportAttribute), false);
					if (customAttributes != null && customAttributes.Length == 1)
					{
						DllImportAttribute dllImportAttribute = (DllImportAttribute)customAttributes[0];
						if (<Module>.<CrtImplementationDetails>.LanguageSupport.IsRuntimeDllName(A_0, dllImportAttribute.Value))
						{
							try
							{
								Marshal.Prelink(methodInfo);
							}
							catch (<CrtImplementationDetails>.Exception)
							{
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0001378C File Offset: 0x00012B8C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.ForceLoadRuntimeApis(A_0);
		<Module>.__scrt_initialize_stdio_msvcrt_compatibility_mode();
		<Module>.__scrt_initialize_default_local_stdio_options();
		<Module>.__scrt_initialize_printf_standard_rounding_mode();
		<Module>.__scrt_initialize_iso_stdio_wide_specifier_mode();
		<Module>.__scrt_initialize_legacy_stdio_wide_specifier_mode();
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.__xc_ma_a), (method*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000137E8 File Offset: 0x00012BE8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00013814 File Offset: 0x00012C14
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00013614 File Offset: 0x00012A14
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00013628 File Offset: 0x00012A28
	[PrePrepareMethod]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00013848 File Offset: 0x00012C48
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
		}
		catch (System.Exception nestedException)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, nestedException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000138A4 File Offset: 0x00012CA4
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000138BC File Offset: 0x00012CBC
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000138D0 File Offset: 0x00012CD0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception innerException)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00013954 File Offset: 0x00012D54
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00013650 File Offset: 0x00012A50
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00013674 File Offset: 0x00012A74
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0001369C File Offset: 0x00012A9C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000136C4 File Offset: 0x00012AC4
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000199C File Offset: 0x00000D9C
	internal static void ??__E?A0x618b78b6@_HUGE@@YMXXZ()
	{
		<Module>.?A0x618b78b6._HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x000019B8 File Offset: 0x00000DB8
	internal static void ??__E?A0x618b78b6@HUGE@@YMXXZ()
	{
		<Module>.?A0x618b78b6.HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x000139C8 File Offset: 0x00012DC8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00013DE8 File Offset: 0x000131E8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x060000DF RID: 223 RVA: 0x000139F8 File Offset: 0x00012DF8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00013A48 File Offset: 0x00012E48
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00013A6C File Offset: 0x00012E6C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00013A94 File Offset: 0x00012E94
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return ((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) != 0;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00013E04 File Offset: 0x00013204
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (!<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized())
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00013AB0 File Offset: 0x00012EB0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00013AD8 File Offset: 0x00012ED8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00013AF0 File Offset: 0x00012EF0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00013B08 File Offset: 0x00012F08
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized())
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			result = true;
		}
		return result;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00013B28 File Offset: 0x00012F28
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized())
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			result = true;
		}
		return result;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00013E34 File Offset: 0x00013234
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00013B48 File Offset: 0x00012F48
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00013B5C File Offset: 0x00012F5C
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method system.Void_u0020() = 0L;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0x618b78b6.__global_lock())
		{
			try
			{
				method* ptr = (method*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
				method* ptr2 = (method*)<Module>.DecodePointer(*(long*)__ponexitend_e);
				long num = (long)(ptr2 - ptr);
				if (*__pexit_list_size - 1UL < (ulong)num >> 3)
				{
					try
					{
						ulong num2 = *__pexit_list_size * 8UL;
						ulong num3 = (num2 < 4096UL) ? num2 : 4096UL;
						IntPtr cb = new IntPtr((int)(num2 + num3));
						IntPtr pv = new IntPtr((void*)ptr);
						IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
						ptr2 = (method*)((byte*)intPtr.ToPointer() + num);
						ptr = (method*)intPtr.ToPointer();
						ulong num4 = *__pexit_list_size;
						ulong num5 = (512UL < num4) ? 512UL : num4;
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr pv2 = new IntPtr((void*)ptr);
						IntPtr intPtr2 = Marshal.ReAllocHGlobal(pv2, cb2);
						ptr2 = (intPtr2.ToPointer() - ptr) / (IntPtr)sizeof(method) + ptr2;
						ptr = (method*)intPtr2.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(long*)ptr2 = func;
				ptr2 += 8L / (long)sizeof(method);
				system.Void_u0020() = func;
				*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
				*(long*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0x618b78b6.__global_unlock();
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00013E4C File Offset: 0x0001324C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0x618b78b6.__alloc_global_lock())
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
			<Module>.__scrt_initialize_type_info();
			<Module>._atexit_m_appdomain(ldftn(__scrt_uninitialize_type_info));
		}
		return 1;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00013CD4 File Offset: 0x000130D4
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(method);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						method system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0x618b78b6.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00013DC0 File Offset: 0x000131C0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x060000EF RID: 239
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x060000F0 RID: 240
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x060000F1 RID: 241 RVA: 0x000019D4 File Offset: 0x00000DD4
	internal static void ??__E?A0x693b1921@_HUGE@@YMXXZ()
	{
		<Module>.?A0x693b1921._HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000019F0 File Offset: 0x00000DF0
	internal static void ??__E?A0x693b1921@HUGE@@YMXXZ()
	{
		<Module>.?A0x693b1921.HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00013EA4 File Offset: 0x000132A4
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00013EF4 File Offset: 0x000132F4
	[SecurityCritical]
	[DebuggerStepThrough]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00013EC8 File Offset: 0x000132C8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00013F24 File Offset: 0x00013324
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00001A0C File Offset: 0x00000E0C
	internal static void ??__E?A0x7bdd3987@_HUGE@@YMXXZ()
	{
		<Module>.?A0x7bdd3987._HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00001A28 File Offset: 0x00000E28
	internal static void ??__E?A0x7bdd3987@HUGE@@YMXXZ()
	{
		<Module>.?A0x7bdd3987.HUGE = double.PositiveInfinity;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00001A44 File Offset: 0x00000E44
	internal static void ??__E?A0x7bdd3987@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00001A58 File Offset: 0x00000E58
	internal static void ??__E?A0x7bdd3987@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00001A6C File Offset: 0x00000E6C
	internal static void ??__E?A0x7bdd3987@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00001A80 File Offset: 0x00000E80
	internal static void ??__E?A0x7bdd3987@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00001A9C File Offset: 0x00000E9C
	internal static void ??__E?A0x7bdd3987@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987._FNV_prime = 1099511628211UL;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00013F68 File Offset: 0x00013368
	[SecuritySafeCritical]
	internal unsafe static void swap(sbyte* a, sbyte* b, ulong width)
	{
		if (a != b && width != 0UL)
		{
			long num = (long)(a - b);
			do
			{
				width -= 1UL;
				sbyte b2 = *(sbyte*)(num / (long)sizeof(sbyte) + b);
				*(byte*)(num / (long)sizeof(sbyte) + b) = (byte)(*(sbyte*)b);
				*(byte*)b = b2;
				b += 1L / (long)sizeof(sbyte);
			}
			while (width != 0UL);
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00013FA0 File Offset: 0x000133A0
	[SecuritySafeCritical]
	internal unsafe static void shortsort(sbyte* lo, sbyte* hi, ulong width, method comp)
	{
		if (hi != lo)
		{
			long num = lo + width / sizeof(sbyte);
			do
			{
				sbyte* ptr = lo;
				sbyte* ptr2 = num;
				if (ptr2 == hi)
				{
					do
					{
						ptr = ((calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr2, ptr, comp) > 0) ? ptr2 : ptr);
						ptr2 = width / sizeof(sbyte) + ptr2;
					}
					while (ptr2 == hi);
				}
				<Module>.?A0x7bdd3987.swap(ptr, hi, width);
				hi -= width / sizeof(sbyte);
			}
			while (hi != lo);
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00013FE8 File Offset: 0x000133E8
	[SecuritySafeCritical]
	internal unsafe static void qsort(void* @base, ulong num, ulong width, method comp)
	{
		if (@base == null && num != 0UL)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
		}
		else if (width > 0UL)
		{
			if (comp != null)
			{
				if (num >= 2UL)
				{
					sbyte* ptr = (sbyte*)@base;
					sbyte* ptr2 = (sbyte*)((num - 1UL) * width + (byte*)@base);
					long num2 = 0L;
					for (;;)
					{
						ulong num3 = (ulong)(ptr2 - ptr) / width + 1UL;
						$ArrayType$$$BY0DO@PEAD $ArrayType$$$BY0DO@PEAD;
						$ArrayType$$$BY0DO@PEAD $ArrayType$$$BY0DO@PEAD2;
						if (num3 <= 8L)
						{
							<Module>.?A0x7bdd3987.shortsort(ptr, ptr2, width, comp);
						}
						else
						{
							sbyte* ptr3 = (num3 >> 1) * width / sizeof(sbyte) + ptr;
							if (calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr, ptr3, comp) > 0)
							{
								<Module>.?A0x7bdd3987.swap(ptr, ptr3, width);
							}
							if (calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr, ptr2, comp) > 0)
							{
								<Module>.?A0x7bdd3987.swap(ptr, ptr2, width);
							}
							if (calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr3, ptr2, comp) > 0)
							{
								<Module>.?A0x7bdd3987.swap(ptr3, ptr2, width);
							}
							sbyte* ptr4 = ptr;
							sbyte* ptr5 = ptr2;
							for (;;)
							{
								if (ptr3 != ptr4)
								{
									do
									{
										ptr4 = width / (ulong)sizeof(sbyte) + ptr4;
										if (ptr4 >= ptr3)
										{
											goto IL_C4;
										}
									}
									while (calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr4, ptr3, comp) <= 0);
									if (ptr3 != ptr4)
									{
										goto IL_D8;
									}
								}
								do
								{
									IL_C4:
									ptr4 = width / (ulong)sizeof(sbyte) + ptr4;
								}
								while (ptr4 == ptr2 && calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr4, ptr3, comp) <= 0);
								do
								{
									IL_D8:
									ptr5 -= width / (ulong)sizeof(sbyte);
								}
								while (ptr5 != ptr3 && calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr5, ptr3, comp) > 0);
								if (ptr5 < ptr4)
								{
									break;
								}
								<Module>.?A0x7bdd3987.swap(ptr4, ptr5, width);
								if (ptr3 == ptr5)
								{
									ptr3 = ptr4;
								}
							}
							ptr5 = width / (ulong)sizeof(sbyte) + ptr5;
							if (ptr3 < ptr5)
							{
								do
								{
									ptr5 -= width / (ulong)sizeof(sbyte);
									if (ptr5 == ptr3)
									{
										goto IL_11D;
									}
								}
								while (!calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr5, ptr3, comp));
								if (ptr3 < ptr5)
								{
									goto IL_12F;
								}
							}
							do
							{
								IL_11D:
								ptr5 -= width / (ulong)sizeof(sbyte);
							}
							while (ptr5 != ptr && !calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), ptr5, ptr3, comp));
							IL_12F:
							if (ptr5 - ptr >= ptr2 - ptr4)
							{
								if (ptr < ptr5)
								{
									long num4 = num2 * 8L;
									*(num4 + ref $ArrayType$$$BY0DO@PEAD) = ptr;
									*(num4 + ref $ArrayType$$$BY0DO@PEAD2) = ptr5;
									num2 += 1L;
								}
								if (ptr4 < ptr2)
								{
									ptr = ptr4;
									continue;
								}
							}
							else
							{
								if (ptr4 < ptr2)
								{
									long num5 = num2 * 8L;
									*(num5 + ref $ArrayType$$$BY0DO@PEAD) = ptr4;
									*(num5 + ref $ArrayType$$$BY0DO@PEAD2) = ptr2;
									num2 += 1L;
								}
								if (ptr < ptr5)
								{
									ptr2 = ptr5;
									continue;
								}
							}
						}
						num2 -= 1L;
						if (num2 < 0L)
						{
							break;
						}
						long num6 = num2 * 8L;
						ptr = *(num6 + ref $ArrayType$$$BY0DO@PEAD);
						ptr2 = *(num6 + ref $ArrayType$$$BY0DO@PEAD2);
					}
				}
			}
			else
			{
				*<Module>._errno() = 22;
				<Module>._invalid_parameter_noinfo();
			}
		}
		else
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00001AB8 File Offset: 0x00000EB8
	internal static void ??__E?A0x7bdd3987@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_trivial_v@M = true;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00001ACC File Offset: 0x00000ECC
	internal static void ??__E?A0x7bdd3987@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_trivial_v@N = true;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00001AE0 File Offset: 0x00000EE0
	internal static void ??__E?A0x7bdd3987@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_trivial_v@O = true;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00001AF4 File Offset: 0x00000EF4
	internal static void ??__E?A0x7bdd3987@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x7bdd3987.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00001B08 File Offset: 0x00000F08
	internal static void ??__E?A0xe51b59a8@_HUGE@@YMXXZ()
	{
		<Module>.?A0xe51b59a8._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00001B24 File Offset: 0x00000F24
	internal static void ??__E?A0xe51b59a8@HUGE@@YMXXZ()
	{
		<Module>.?A0xe51b59a8.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00001B40 File Offset: 0x00000F40
	internal static void ??__E?A0xe51b59a8@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00001B54 File Offset: 0x00000F54
	internal static void ??__E?A0xe51b59a8@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00001B68 File Offset: 0x00000F68
	internal static void ??__E?A0xe51b59a8@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00001B7C File Offset: 0x00000F7C
	internal static void ??__E?A0xe51b59a8@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00001B98 File Offset: 0x00000F98
	internal static void ??__E?A0xe51b59a8@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x000141CC File Offset: 0x000135CC
	[SecuritySafeCritical]
	internal unsafe static void* bsearch(void* key, void* @base, ulong num, ulong width, method compare)
	{
		if (@base == null && num != 0UL)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return null;
		}
		if (width <= 0UL)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return null;
		}
		if (compare != null)
		{
			sbyte* ptr = (sbyte*)@base;
			sbyte* ptr2 = (sbyte*)((num - 1UL) * width / (ulong)sizeof(void) + @base);
			if (@base == (void*)ptr2)
			{
				do
				{
					ulong num2 = num >> 1;
					if (num2 == null)
					{
						goto IL_87;
					}
					ulong num3 = num & 1UL;
					sbyte* ptr3 = ((num3 == 0UL) ? (num2 - 1L) : num2) * width / sizeof(sbyte) + ptr;
					int num4 = calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), key, ptr3, compare);
					if (num4 == null)
					{
						return ptr3;
					}
					if (num4 < 0)
					{
						ptr2 = ptr3 - width;
						num = ((num3 == 0UL) ? (num2 - 1L) : num2);
					}
					else
					{
						ptr = ptr3 + width;
						num = num2;
					}
				}
				while (ptr == ptr2);
				goto IL_9C;
				IL_87:
				if (num != 0UL)
				{
					return (void*)((calli(System.Int32(System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), key, ptr, compare) != 0) ? null : ptr);
				}
			}
			IL_9C:
			return null;
		}
		*<Module>._errno() = 22;
		<Module>._invalid_parameter_noinfo();
		return null;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00001BB4 File Offset: 0x00000FB4
	internal static void ??__E?A0xe51b59a8@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_trivial_v@M = true;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00001BC8 File Offset: 0x00000FC8
	internal static void ??__E?A0xe51b59a8@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_trivial_v@N = true;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00001BDC File Offset: 0x00000FDC
	internal static void ??__E?A0xe51b59a8@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_trivial_v@O = true;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00001BF0 File Offset: 0x00000FF0
	internal static void ??__E?A0xe51b59a8@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xe51b59a8.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00001C04 File Offset: 0x00001004
	internal static void ??__E?A0xace26467@_HUGE@@YMXXZ()
	{
		<Module>.?A0xace26467._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00001C20 File Offset: 0x00001020
	internal static void ??__E?A0xace26467@HUGE@@YMXXZ()
	{
		<Module>.?A0xace26467.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00001C3C File Offset: 0x0000103C
	internal static void ??__E?A0xace26467@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00001C50 File Offset: 0x00001050
	internal static void ??__E?A0xace26467@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00001C64 File Offset: 0x00001064
	internal static void ??__E?A0xace26467@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00001C78 File Offset: 0x00001078
	internal static void ??__E?A0xace26467@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00001C94 File Offset: 0x00001094
	internal static void ??__E?A0xace26467@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00014298 File Offset: 0x00013698
	[SecurityCritical]
	internal unsafe static void delete(void* block)
	{
		<Module>.free(block);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00001CB0 File Offset: 0x000010B0
	internal static void ??__E?A0xace26467@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_trivial_v@M = true;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00001CC4 File Offset: 0x000010C4
	internal static void ??__E?A0xace26467@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_trivial_v@N = true;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00001CD8 File Offset: 0x000010D8
	internal static void ??__E?A0xace26467@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_trivial_v@O = true;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00001CEC File Offset: 0x000010EC
	internal static void ??__E?A0xace26467@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xace26467.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00001D00 File Offset: 0x00001100
	internal static void ??__E?A0x73beb3f9@_HUGE@@YMXXZ()
	{
		<Module>.?A0x73beb3f9._HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00001D1C File Offset: 0x0000111C
	internal static void ??__E?A0x73beb3f9@HUGE@@YMXXZ()
	{
		<Module>.?A0x73beb3f9.HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00001D38 File Offset: 0x00001138
	internal static void ??__E?A0x73beb3f9@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00001D4C File Offset: 0x0000114C
	internal static void ??__E?A0x73beb3f9@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00001D60 File Offset: 0x00001160
	internal static void ??__E?A0x73beb3f9@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00001D74 File Offset: 0x00001174
	internal static void ??__E?A0x73beb3f9@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00001D90 File Offset: 0x00001190
	internal static void ??__E?A0x73beb3f9@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000142AC File Offset: 0x000136AC
	[SecurityCritical]
	internal unsafe static void* @new(ulong size)
	{
		void* ptr = <Module>.malloc(size);
		if (ptr == null)
		{
			while (<Module>._callnewh(size) != null)
			{
				ptr = <Module>.malloc(size);
				if (ptr != null)
				{
					return ptr;
				}
			}
			if (size == 18446744073709551615UL)
			{
				<Module>.__scrt_throw_std_bad_array_new_length();
			}
			<Module>.__scrt_throw_std_bad_alloc();
		}
		return ptr;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00001DAC File Offset: 0x000011AC
	internal static void ??__E?A0x73beb3f9@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_trivial_v@M = true;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00001DC0 File Offset: 0x000011C0
	internal static void ??__E?A0x73beb3f9@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_trivial_v@N = true;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00001DD4 File Offset: 0x000011D4
	internal static void ??__E?A0x73beb3f9@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_trivial_v@O = true;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00001DE8 File Offset: 0x000011E8
	internal static void ??__E?A0x73beb3f9@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x73beb3f9.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00001DFC File Offset: 0x000011FC
	internal static void ??__E?A0x5a20a2cc@_HUGE@@YMXXZ()
	{
		<Module>.?A0x5a20a2cc._HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00001E18 File Offset: 0x00001218
	internal static void ??__E?A0x5a20a2cc@HUGE@@YMXXZ()
	{
		<Module>.?A0x5a20a2cc.HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00001E34 File Offset: 0x00001234
	internal static void ??__E?A0x5a20a2cc@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00001E48 File Offset: 0x00001248
	internal static void ??__E?A0x5a20a2cc@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00001E5C File Offset: 0x0000125C
	internal static void ??__E?A0x5a20a2cc@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00001E70 File Offset: 0x00001270
	internal static void ??__E?A0x5a20a2cc@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00001E8C File Offset: 0x0000128C
	internal static void ??__E?A0x5a20a2cc@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void __scrt_initialize_stdio_msvcrt_compatibility_mode()
	{
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00001EA8 File Offset: 0x000012A8
	internal static void ??__E?A0x5a20a2cc@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_trivial_v@M = true;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00001EBC File Offset: 0x000012BC
	internal static void ??__E?A0x5a20a2cc@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_trivial_v@N = true;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00001ED0 File Offset: 0x000012D0
	internal static void ??__E?A0x5a20a2cc@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_trivial_v@O = true;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00001EE4 File Offset: 0x000012E4
	internal static void ??__E?A0x5a20a2cc@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x5a20a2cc.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00001EF8 File Offset: 0x000012F8
	internal static void ??__E?A0x03600527@_HUGE@@YMXXZ()
	{
		<Module>.?A0x03600527._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00001F14 File Offset: 0x00001314
	internal static void ??__E?A0x03600527@HUGE@@YMXXZ()
	{
		<Module>.?A0x03600527.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00001F30 File Offset: 0x00001330
	internal static void ??__E?A0x03600527@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00001F44 File Offset: 0x00001344
	internal static void ??__E?A0x03600527@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00001F58 File Offset: 0x00001358
	internal static void ??__E?A0x03600527@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00001F6C File Offset: 0x0000136C
	internal static void ??__E?A0x03600527@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00001F88 File Offset: 0x00001388
	internal static void ??__E?A0x03600527@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000142F0 File Offset: 0x000136F0
	[SecurityCritical]
	internal unsafe static ulong* __local_stdio_printf_options()
	{
		return &<Module>.?_OptionsStorage@?1??__local_stdio_printf_options@@YMPEA_KXZ@$$Q4_KA;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00014304 File Offset: 0x00013704
	[SecurityCritical]
	internal unsafe static ulong* __local_stdio_scanf_options()
	{
		return &<Module>.?_OptionsStorage@?1??__local_stdio_scanf_options@@YMPEA_KXZ@$$Q4_KA;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00014318 File Offset: 0x00013718
	[SecurityCritical]
	internal unsafe static void __scrt_initialize_default_local_stdio_options()
	{
		*<Module>.__local_stdio_printf_options() |= 36UL;
		*<Module>.__local_stdio_scanf_options() |= 2UL;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00001FA4 File Offset: 0x000013A4
	internal static void ??__E?A0x03600527@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_trivial_v@M = true;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00001FB8 File Offset: 0x000013B8
	internal static void ??__E?A0x03600527@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_trivial_v@N = true;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00001FCC File Offset: 0x000013CC
	internal static void ??__E?A0x03600527@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_trivial_v@O = true;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00001FE0 File Offset: 0x000013E0
	internal static void ??__E?A0x03600527@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x03600527.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00001FF4 File Offset: 0x000013F4
	internal static void ??__E?A0xb9e66ada@_HUGE@@YMXXZ()
	{
		<Module>.?A0xb9e66ada._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00002010 File Offset: 0x00001410
	internal static void ??__E?A0xb9e66ada@HUGE@@YMXXZ()
	{
		<Module>.?A0xb9e66ada.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000202C File Offset: 0x0000142C
	internal static void ??__E?A0xb9e66ada@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00002040 File Offset: 0x00001440
	internal static void ??__E?A0xb9e66ada@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00002054 File Offset: 0x00001454
	internal static void ??__E?A0xb9e66ada@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00002068 File Offset: 0x00001468
	internal static void ??__E?A0xb9e66ada@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00002084 File Offset: 0x00001484
	internal static void ??__E?A0xb9e66ada@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void __scrt_initialize_printf_standard_rounding_mode()
	{
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000020A0 File Offset: 0x000014A0
	internal static void ??__E?A0xb9e66ada@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_trivial_v@M = true;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000020B4 File Offset: 0x000014B4
	internal static void ??__E?A0xb9e66ada@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_trivial_v@N = true;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000020C8 File Offset: 0x000014C8
	internal static void ??__E?A0xb9e66ada@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_trivial_v@O = true;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x000020DC File Offset: 0x000014DC
	internal static void ??__E?A0xb9e66ada@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xb9e66ada.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000020F0 File Offset: 0x000014F0
	internal static void ??__E?A0x572a2b92@_HUGE@@YMXXZ()
	{
		<Module>.?A0x572a2b92._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000210C File Offset: 0x0000150C
	internal static void ??__E?A0x572a2b92@HUGE@@YMXXZ()
	{
		<Module>.?A0x572a2b92.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00002128 File Offset: 0x00001528
	internal static void ??__E?A0x572a2b92@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000213C File Offset: 0x0000153C
	internal static void ??__E?A0x572a2b92@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00002150 File Offset: 0x00001550
	internal static void ??__E?A0x572a2b92@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00002164 File Offset: 0x00001564
	internal static void ??__E?A0x572a2b92@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00002180 File Offset: 0x00001580
	internal static void ??__E?A0x572a2b92@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void __scrt_initialize_iso_stdio_wide_specifier_mode()
	{
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000219C File Offset: 0x0000159C
	internal static void ??__E?A0x572a2b92@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_trivial_v@M = true;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000021B0 File Offset: 0x000015B0
	internal static void ??__E?A0x572a2b92@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_trivial_v@N = true;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000021C4 File Offset: 0x000015C4
	internal static void ??__E?A0x572a2b92@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_trivial_v@O = true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000021D8 File Offset: 0x000015D8
	internal static void ??__E?A0x572a2b92@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x572a2b92.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000021EC File Offset: 0x000015EC
	internal static void ??__E?A0xd1de7625@_HUGE@@YMXXZ()
	{
		<Module>.?A0xd1de7625._HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00002208 File Offset: 0x00001608
	internal static void ??__E?A0xd1de7625@HUGE@@YMXXZ()
	{
		<Module>.?A0xd1de7625.HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00002224 File Offset: 0x00001624
	internal static void ??__E?A0xd1de7625@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00002238 File Offset: 0x00001638
	internal static void ??__E?A0xd1de7625@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000224C File Offset: 0x0000164C
	internal static void ??__E?A0xd1de7625@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00002260 File Offset: 0x00001660
	internal static void ??__E?A0xd1de7625@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000227C File Offset: 0x0000167C
	internal static void ??__E?A0xd1de7625@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void __scrt_initialize_legacy_stdio_wide_specifier_mode()
	{
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00002298 File Offset: 0x00001698
	internal static void ??__E?A0xd1de7625@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_trivial_v@M = true;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000022AC File Offset: 0x000016AC
	internal static void ??__E?A0xd1de7625@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_trivial_v@N = true;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000022C0 File Offset: 0x000016C0
	internal static void ??__E?A0xd1de7625@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_trivial_v@O = true;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000022D4 File Offset: 0x000016D4
	internal static void ??__E?A0xd1de7625@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xd1de7625.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000022E8 File Offset: 0x000016E8
	internal static void ??__E?A0xa788fbba@_HUGE@@YMXXZ()
	{
		<Module>.?A0xa788fbba._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00002304 File Offset: 0x00001704
	internal static void ??__E?A0xa788fbba@HUGE@@YMXXZ()
	{
		<Module>.?A0xa788fbba.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00002320 File Offset: 0x00001720
	internal static void ??__E?A0xa788fbba@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00002334 File Offset: 0x00001734
	internal static void ??__E?A0xa788fbba@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00002348 File Offset: 0x00001748
	internal static void ??__E?A0xa788fbba@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000235C File Offset: 0x0000175C
	internal static void ??__E?A0xa788fbba@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00002378 File Offset: 0x00001778
	internal static void ??__E?A0xa788fbba@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00002394 File Offset: 0x00001794
	internal static void ??__E?A0xa788fbba@_Small_object_num_ptrs@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba._Small_object_num_ptrs = 8;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0001433C File Offset: 0x0001373C
	[SecuritySafeCritical]
	internal unsafe static void __scrt_initialize_type_info()
	{
		try
		{
			IntPtr intPtr = new IntPtr(<Module>.calloc(1UL, 16UL));
			<Module>.__type_info_root_node = intPtr;
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			<Module>.InitializeSListHead((_SLIST_HEADER*)<Module>.__type_info_root_node.ToPointer());
		}
		catch (SecurityException)
		{
			<Module>.__type_info_root_node = IntPtr.Zero;
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000143B0 File Offset: 0x000137B0
	[SecuritySafeCritical]
	internal unsafe static void __scrt_uninitialize_type_info()
	{
		if (<Module>.__type_info_root_node != IntPtr.Zero)
		{
			<Module>.__std_type_info_destroy_list((__type_info_node*)<Module>.__type_info_root_node.ToPointer());
			<Module>.free(<Module>.__type_info_root_node.ToPointer());
			<Module>.__type_info_root_node = IntPtr.Zero;
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x000023A8 File Offset: 0x000017A8
	internal static void ??__E?A0xa788fbba@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_trivial_v@M = true;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x000023BC File Offset: 0x000017BC
	internal static void ??__E?A0xa788fbba@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_trivial_v@N = true;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000023D0 File Offset: 0x000017D0
	internal static void ??__E?A0xa788fbba@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_trivial_v@O = true;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000023E4 File Offset: 0x000017E4
	internal static void ??__E?A0xa788fbba@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xa788fbba.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000023F8 File Offset: 0x000017F8
	internal static void ??__E?A0x0367cfd4@_HUGE@@YMXXZ()
	{
		<Module>.?A0x0367cfd4._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00002414 File Offset: 0x00001814
	internal static void ??__E?A0x0367cfd4@HUGE@@YMXXZ()
	{
		<Module>.?A0x0367cfd4.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00002430 File Offset: 0x00001830
	internal static void ??__E?A0x0367cfd4@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00002444 File Offset: 0x00001844
	internal static void ??__E?A0x0367cfd4@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00002458 File Offset: 0x00001858
	internal static void ??__E?A0x0367cfd4@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000246C File Offset: 0x0000186C
	internal static void ??__E?A0x0367cfd4@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00002488 File Offset: 0x00001888
	internal static void ??__E?A0x0367cfd4@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x000024A4 File Offset: 0x000018A4
	internal static void ??__E?A0x0367cfd4@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_trivial_v@M = true;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000024B8 File Offset: 0x000018B8
	internal static void ??__E?A0x0367cfd4@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_trivial_v@N = true;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000024CC File Offset: 0x000018CC
	internal static void ??__E?A0x0367cfd4@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_trivial_v@O = true;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000024E0 File Offset: 0x000018E0
	internal static void ??__E?A0x0367cfd4@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x0367cfd4.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x000024F4 File Offset: 0x000018F4
	internal static void ??__E?A0xbdb0e99a@_HUGE@@YMXXZ()
	{
		<Module>.?A0xbdb0e99a._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00002510 File Offset: 0x00001910
	internal static void ??__E?A0xbdb0e99a@HUGE@@YMXXZ()
	{
		<Module>.?A0xbdb0e99a.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000252C File Offset: 0x0000192C
	internal static void ??__E?A0xbdb0e99a@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00002540 File Offset: 0x00001940
	internal static void ??__E?A0xbdb0e99a@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00002554 File Offset: 0x00001954
	internal static void ??__E?A0xbdb0e99a@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00002568 File Offset: 0x00001968
	internal static void ??__E?A0xbdb0e99a@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00002584 File Offset: 0x00001984
	internal static void ??__E?A0xbdb0e99a@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000025A0 File Offset: 0x000019A0
	internal static void ??__E?A0xbdb0e99a@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_trivial_v@M = true;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000025B4 File Offset: 0x000019B4
	internal static void ??__E?A0xbdb0e99a@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_trivial_v@N = true;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000025C8 File Offset: 0x000019C8
	internal static void ??__E?A0xbdb0e99a@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_trivial_v@O = true;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x000025DC File Offset: 0x000019DC
	internal static void ??__E?A0xbdb0e99a@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xbdb0e99a.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x000025F0 File Offset: 0x000019F0
	internal static void ??__E?A0x31d8a37c@_HUGE@@YMXXZ()
	{
		<Module>.?A0x31d8a37c._HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000260C File Offset: 0x00001A0C
	internal static void ??__E?A0x31d8a37c@HUGE@@YMXXZ()
	{
		<Module>.?A0x31d8a37c.HUGE = double.PositiveInfinity;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00002628 File Offset: 0x00001A28
	internal static void ??__E?A0x31d8a37c@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000263C File Offset: 0x00001A3C
	internal static void ??__E?A0x31d8a37c@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00002650 File Offset: 0x00001A50
	internal static void ??__E?A0x31d8a37c@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00002664 File Offset: 0x00001A64
	internal static void ??__E?A0x31d8a37c@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00002680 File Offset: 0x00001A80
	internal static void ??__E?A0x31d8a37c@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c._FNV_prime = 1099511628211UL;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000269C File Offset: 0x00001A9C
	internal static void ??__E?A0x31d8a37c@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_trivial_v@M = true;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000026B0 File Offset: 0x00001AB0
	internal static void ??__E?A0x31d8a37c@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_trivial_v@N = true;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x000026C4 File Offset: 0x00001AC4
	internal static void ??__E?A0x31d8a37c@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_trivial_v@O = true;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000026D8 File Offset: 0x00001AD8
	internal static void ??__E?A0x31d8a37c@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x31d8a37c.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000026EC File Offset: 0x00001AEC
	internal static void ??__E?A0x0d156348@_HUGE@@YMXXZ()
	{
		<Module>.?A0x0d156348._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00002708 File Offset: 0x00001B08
	internal static void ??__E?A0x0d156348@HUGE@@YMXXZ()
	{
		<Module>.?A0x0d156348.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00002724 File Offset: 0x00001B24
	internal static void ??__E?A0x0d156348@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00002738 File Offset: 0x00001B38
	internal static void ??__E?A0x0d156348@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000274C File Offset: 0x00001B4C
	internal static void ??__E?A0x0d156348@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00002760 File Offset: 0x00001B60
	internal static void ??__E?A0x0d156348@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000277C File Offset: 0x00001B7C
	internal static void ??__E?A0x0d156348@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00002798 File Offset: 0x00001B98
	internal static void ??__E?A0x0d156348@_Small_object_num_ptrs@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._Small_object_num_ptrs = 8;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void ??__E?A0x0d156348@piecewise_construct@std@@YMXXZ()
	{
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00002E3C File Offset: 0x0000223C
	internal static void ??__E?A0x0d156348@allocator_arg@std@@YMXXZ()
	{
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x000027AC File Offset: 0x00001BAC
	internal static void ??__E?A0x0d156348@_Big_allocation_threshold@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._Big_allocation_threshold = 4096UL;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000027C4 File Offset: 0x00001BC4
	internal static void ??__E?A0x0d156348@_Big_allocation_alignment@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._Big_allocation_alignment = 32UL;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000027DC File Offset: 0x00001BDC
	internal static void ??__E?A0x0d156348@_Non_user_size@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._Non_user_size = 39UL;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000027F4 File Offset: 0x00001BF4
	internal static void ??__E?A0x0d156348@_Big_allocation_sentinel@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348._Big_allocation_sentinel = 18085043209519168250UL;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00002810 File Offset: 0x00001C10
	internal static void ??__E?A0x0d156348@?$is_const_v@D@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_const_v@D = false;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00002824 File Offset: 0x00001C24
	internal static void ??__E?A0x0d156348@?$is_same_v@_K_K@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@_K_K = true;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00002838 File Offset: 0x00001C38
	internal static void ??__E?A0x0d156348@?$is_same_v@_J_J@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@_J_J = true;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000284C File Offset: 0x00001C4C
	internal static void ??__E?A0x0d156348@?$is_same_v@PEADPEAD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEADPEAD = true;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00002860 File Offset: 0x00001C60
	internal static void ??__E?A0x0d156348@?$is_same_v@PEBDPEBD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEBDPEBD = true;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00002874 File Offset: 0x00001C74
	internal static void ??__E?A0x0d156348@?$_Is_simple_alloc_v@V?$allocator@D@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_Is_simple_alloc_v@V?$allocator@D@std@@ = true;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00002888 File Offset: 0x00001C88
	internal static void ??__E?A0x0d156348@?$is_same_v@DD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@DD = true;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000289C File Offset: 0x00001C9C
	internal static void ??__E?A0x0d156348@?$is_array_v@D@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@D = false;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000028B0 File Offset: 0x00001CB0
	internal static void ??__E?A0x0d156348@?$is_trivial_v@D@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@D = true;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x000028C4 File Offset: 0x00001CC4
	internal static void ??__E?A0x0d156348@?$is_standard_layout_v@D@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_standard_layout_v@D = true;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000028D8 File Offset: 0x00001CD8
	internal static void ??__E?$_Is_specialization_v@U?$char_traits@D@std@@Uchar_traits@2@@std@@YMXXZ()
	{
		<Module>.std.?$_Is_specialization_v@U?$char_traits@D@std@@Uchar_traits@2@ = true;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000028EC File Offset: 0x00001CEC
	internal static void ??__E?A0x0d156348@?$is_trivial_v@PEAD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@PEAD = true;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00002900 File Offset: 0x00001D00
	internal static void ??__E?A0x0d156348@?$is_empty_v@U_Container_base0@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_empty_v@U_Container_base0@std@@ = true;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00002914 File Offset: 0x00001D14
	internal static void ??__E?A0x0d156348@?$_Size_after_ebco_v@U_Container_base0@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_Size_after_ebco_v@U_Container_base0@std@@ = 0UL;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00002928 File Offset: 0x00001D28
	internal static void ??__E?A0x0d156348@?$is_empty_v@V?$allocator@D@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_empty_v@V?$allocator@D@std@@ = true;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000293C File Offset: 0x00001D3C
	internal static void ??__E?A0x0d156348@?$is_final_v@V?$allocator@D@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_final_v@V?$allocator@D@std@@ = false;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00002950 File Offset: 0x00001D50
	internal static void ??__E?A0x0d156348@?$is_const_v@_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_const_v@_W = false;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00002964 File Offset: 0x00001D64
	internal static void ??__E?A0x0d156348@?$is_same_v@PEA_WPEA_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEA_WPEA_W = true;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00002978 File Offset: 0x00001D78
	internal static void ??__E?A0x0d156348@?$is_same_v@PEB_WPEB_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEB_WPEB_W = true;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000298C File Offset: 0x00001D8C
	internal static void ??__E?A0x0d156348@?$_Is_simple_alloc_v@V?$allocator@_W@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_Is_simple_alloc_v@V?$allocator@_W@std@@ = true;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000029A0 File Offset: 0x00001DA0
	internal static void ??__E?A0x0d156348@?$is_same_v@_W_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@_W_W = true;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000029B4 File Offset: 0x00001DB4
	internal static void ??__E?A0x0d156348@?$is_array_v@_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@_W = false;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x000029C8 File Offset: 0x00001DC8
	internal static void ??__E?A0x0d156348@?$is_trivial_v@_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@_W = true;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x000029DC File Offset: 0x00001DDC
	internal static void ??__E?A0x0d156348@?$is_standard_layout_v@_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_standard_layout_v@_W = true;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x000029F0 File Offset: 0x00001DF0
	internal static void ??__E?$_Is_specialization_v@U?$char_traits@_W@std@@Uchar_traits@2@@std@@YMXXZ()
	{
		<Module>.std.?$_Is_specialization_v@U?$char_traits@_W@std@@Uchar_traits@2@ = true;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00002A04 File Offset: 0x00001E04
	internal static void ??__E?A0x0d156348@?$is_trivial_v@PEA_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@PEA_W = true;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00002A18 File Offset: 0x00001E18
	internal static void ??__E?A0x0d156348@?$is_empty_v@V?$allocator@_W@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_empty_v@V?$allocator@_W@std@@ = true;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00002A2C File Offset: 0x00001E2C
	internal static void ??__E?A0x0d156348@?$is_final_v@V?$allocator@_W@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_final_v@V?$allocator@_W@std@@ = false;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00002A40 File Offset: 0x00001E40
	internal static void ??__E?A0x0d156348@?$is_const_v@_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_const_v@_S = false;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00002A54 File Offset: 0x00001E54
	internal static void ??__E?A0x0d156348@?$is_same_v@PEA_SPEA_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEA_SPEA_S = true;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00002A68 File Offset: 0x00001E68
	internal static void ??__E?A0x0d156348@?$is_same_v@PEB_SPEB_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEB_SPEB_S = true;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00002A7C File Offset: 0x00001E7C
	internal static void ??__E?A0x0d156348@?$_Is_simple_alloc_v@V?$allocator@_S@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_Is_simple_alloc_v@V?$allocator@_S@std@@ = true;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00002A90 File Offset: 0x00001E90
	internal static void ??__E?A0x0d156348@?$is_same_v@_S_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@_S_S = true;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00002AA4 File Offset: 0x00001EA4
	internal static void ??__E?A0x0d156348@?$is_array_v@_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@_S = false;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00002AB8 File Offset: 0x00001EB8
	internal static void ??__E?A0x0d156348@?$is_trivial_v@_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@_S = true;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00002ACC File Offset: 0x00001ECC
	internal static void ??__E?A0x0d156348@?$is_standard_layout_v@_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_standard_layout_v@_S = true;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00002AE0 File Offset: 0x00001EE0
	internal static void ??__E?$_Is_specialization_v@U?$char_traits@_S@std@@Uchar_traits@2@@std@@YMXXZ()
	{
		<Module>.std.?$_Is_specialization_v@U?$char_traits@_S@std@@Uchar_traits@2@ = true;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00002AF4 File Offset: 0x00001EF4
	internal static void ??__E?A0x0d156348@?$is_trivial_v@PEA_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@PEA_S = true;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00002B08 File Offset: 0x00001F08
	internal static void ??__E?A0x0d156348@?$is_empty_v@V?$allocator@_S@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_empty_v@V?$allocator@_S@std@@ = true;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00002B1C File Offset: 0x00001F1C
	internal static void ??__E?A0x0d156348@?$is_final_v@V?$allocator@_S@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_final_v@V?$allocator@_S@std@@ = false;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00002B30 File Offset: 0x00001F30
	internal static void ??__E?A0x0d156348@?$is_const_v@_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_const_v@_U = false;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00002B44 File Offset: 0x00001F44
	internal static void ??__E?A0x0d156348@?$is_same_v@PEA_UPEA_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEA_UPEA_U = true;
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00002B58 File Offset: 0x00001F58
	internal static void ??__E?A0x0d156348@?$is_same_v@PEB_UPEB_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@PEB_UPEB_U = true;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00002B6C File Offset: 0x00001F6C
	internal static void ??__E?A0x0d156348@?$_Is_simple_alloc_v@V?$allocator@_U@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_Is_simple_alloc_v@V?$allocator@_U@std@@ = true;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00002B80 File Offset: 0x00001F80
	internal static void ??__E?A0x0d156348@?$is_same_v@_U_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_same_v@_U_U = true;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00002B94 File Offset: 0x00001F94
	internal static void ??__E?A0x0d156348@?$is_array_v@_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@_U = false;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00002BA8 File Offset: 0x00001FA8
	internal static void ??__E?A0x0d156348@?$is_trivial_v@_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@_U = true;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00002BBC File Offset: 0x00001FBC
	internal static void ??__E?A0x0d156348@?$is_standard_layout_v@_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_standard_layout_v@_U = true;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00002BD0 File Offset: 0x00001FD0
	internal static void ??__E?$_Is_specialization_v@U?$char_traits@_U@std@@Uchar_traits@2@@std@@YMXXZ()
	{
		<Module>.std.?$_Is_specialization_v@U?$char_traits@_U@std@@Uchar_traits@2@ = true;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00002BE4 File Offset: 0x00001FE4
	internal static void ??__E?A0x0d156348@?$is_trivial_v@PEA_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@PEA_U = true;
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00002BF8 File Offset: 0x00001FF8
	internal static void ??__E?A0x0d156348@?$is_empty_v@V?$allocator@_U@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_empty_v@V?$allocator@_U@std@@ = true;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00002C0C File Offset: 0x0000200C
	internal static void ??__E?A0x0d156348@?$is_final_v@V?$allocator@_U@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_final_v@V?$allocator@_U@std@@ = false;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00002C20 File Offset: 0x00002020
	internal static void ??__E?A0x0d156348@?$is_array_v@$$CBU_EXCEPTION_RECORD@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@$$CBU_EXCEPTION_RECORD@@ = false;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00002C34 File Offset: 0x00002034
	internal static void ??__E?A0x0d156348@?$disjunction_v@U?$is_array@$$CBU_EXCEPTION_RECORD@@@std@@U?$is_void@$$CBU_EXCEPTION_RECORD@@@2@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$disjunction_v@U?$is_array@$$CBU_EXCEPTION_RECORD@@@std@@U?$is_void@$$CBU_EXCEPTION_RECORD@@@2@ = false;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00002C48 File Offset: 0x00002048
	internal static void ??__E?A0x0d156348@?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_U@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_U@std@@@std@@$$V@2@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_U@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_U@std@@@std@@$$V@2@ = true;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00002C5C File Offset: 0x0000205C
	internal static void ??__E?A0x0d156348@?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_S@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_S@std@@@std@@$$V@2@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_S@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_S@std@@@std@@$$V@2@ = true;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00002C70 File Offset: 0x00002070
	internal static void ??__E?A0x0d156348@?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_W@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_W@std@@@std@@$$V@2@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_W@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_W@std@@@std@@$$V@2@ = true;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00002C84 File Offset: 0x00002084
	internal static void ??__E?A0x0d156348@?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@D@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@D@std@@@std@@$$V@2@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@D@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@D@std@@@std@@$$V@2@ = true;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00002C98 File Offset: 0x00002098
	internal static void ??__E?A0x0d156348@?$is_nothrow_move_constructible_v@PEBU_EXCEPTION_RECORD@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_move_constructible_v@PEBU_EXCEPTION_RECORD@@ = true;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00002CAC File Offset: 0x000020AC
	internal static void ??__E?A0x0d156348@?$is_nothrow_move_assignable_v@PEBU_EXCEPTION_RECORD@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_move_assignable_v@PEBU_EXCEPTION_RECORD@@ = true;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00002CC0 File Offset: 0x000020C0
	internal static void ??__E?A0x0d156348@?$is_nothrow_move_constructible_v@PEAV_Ref_count_base@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_move_constructible_v@PEAV_Ref_count_base@std@@ = true;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00002CD4 File Offset: 0x000020D4
	internal static void ??__E?A0x0d156348@?$is_nothrow_move_assignable_v@PEAV_Ref_count_base@std@@@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_move_assignable_v@PEAV_Ref_count_base@std@@ = true;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00002E4C File Offset: 0x0000224C
	internal static void ??__E?A0x0d156348@?$_New_alignof@_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_New_alignof@_U = 16UL;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00002E64 File Offset: 0x00002264
	internal static void ??__E?A0x0d156348@?$_New_alignof@_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_New_alignof@_S = 16UL;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00002E7C File Offset: 0x0000227C
	internal static void ??__E?A0x0d156348@?$_New_alignof@_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_New_alignof@_W = 16UL;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00002E94 File Offset: 0x00002294
	internal static void ??__E?A0x0d156348@?$_New_alignof@D@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$_New_alignof@D = 16UL;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00002CE8 File Offset: 0x000020E8
	internal static void ??__E?A0x0d156348@?$is_integral_v@$$CBJ@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_integral_v@$$CBJ = true;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00002CFC File Offset: 0x000020FC
	internal static void ??__E?A0x0d156348@?$is_array_v@PEA_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@PEA_U = false;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00002D10 File Offset: 0x00002110
	internal static void ??__E?A0x0d156348@?$is_nothrow_constructible_v@PEA_UAEBQEA_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_constructible_v@PEA_UAEBQEA_U = true;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00002D24 File Offset: 0x00002124
	internal static void ??__E?A0x0d156348@?$is_array_v@PEA_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@PEA_S = false;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00002D38 File Offset: 0x00002138
	internal static void ??__E?A0x0d156348@?$is_nothrow_constructible_v@PEA_SAEBQEA_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_constructible_v@PEA_SAEBQEA_S = true;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00002D4C File Offset: 0x0000214C
	internal static void ??__E?A0x0d156348@?$is_array_v@PEA_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@PEA_W = false;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00002D60 File Offset: 0x00002160
	internal static void ??__E?A0x0d156348@?$is_nothrow_constructible_v@PEA_WAEBQEA_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_constructible_v@PEA_WAEBQEA_W = true;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00002D74 File Offset: 0x00002174
	internal static void ??__E?A0x0d156348@?$is_array_v@PEAD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_array_v@PEAD = false;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00002D88 File Offset: 0x00002188
	internal static void ??__E?A0x0d156348@?$is_nothrow_constructible_v@PEADAEBQEAD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_nothrow_constructible_v@PEADAEBQEAD = true;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00002D9C File Offset: 0x0000219C
	internal static void ??__E?A0x0d156348@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@M = true;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00002DB0 File Offset: 0x000021B0
	internal static void ??__E?A0x0d156348@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@N = true;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00002DC4 File Offset: 0x000021C4
	internal static void ??__E?A0x0d156348@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@O = true;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00002DD8 File Offset: 0x000021D8
	internal static void ??__E?A0x0d156348@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00002DEC File Offset: 0x000021EC
	internal static void ??__E?A0x0d156348@?$is_pointer_v@PEAPEA_U@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_pointer_v@PEAPEA_U = true;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00002E00 File Offset: 0x00002200
	internal static void ??__E?A0x0d156348@?$is_pointer_v@PEAPEA_S@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_pointer_v@PEAPEA_S = true;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00002E14 File Offset: 0x00002214
	internal static void ??__E?A0x0d156348@?$is_pointer_v@PEAPEA_W@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_pointer_v@PEAPEA_W = true;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00002E28 File Offset: 0x00002228
	internal static void ??__E?A0x0d156348@?$is_pointer_v@PEAPEAD@std@@YMXXZ()
	{
		<Module>.std.?A0x0d156348.?$is_pointer_v@PEAPEAD = true;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00002EAC File Offset: 0x000022AC
	internal static void ??__E?A0xc7023181@_HUGE@@YMXXZ()
	{
		<Module>.?A0xc7023181._HUGE = double.PositiveInfinity;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00002EC8 File Offset: 0x000022C8
	internal static void ??__E?A0xc7023181@HUGE@@YMXXZ()
	{
		<Module>.?A0xc7023181.HUGE = double.PositiveInfinity;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00002EE4 File Offset: 0x000022E4
	internal static void ??__E?A0xc7023181@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00002EF8 File Offset: 0x000022F8
	internal static void ??__E?A0xc7023181@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00002F0C File Offset: 0x0000230C
	internal static void ??__E?A0xc7023181@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00002F20 File Offset: 0x00002320
	internal static void ??__E?A0xc7023181@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00002F3C File Offset: 0x0000233C
	internal static void ??__E?A0xc7023181@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181._FNV_prime = 1099511628211UL;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x000143F8 File Offset: 0x000137F8
	[SecurityCritical]
	internal static void __scrt_throw_std_bad_alloc()
	{
		<Module>.TerminateProcess(<Module>.GetCurrentProcess(), 3U);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000143F8 File Offset: 0x000137F8
	[SecurityCritical]
	internal static void __scrt_throw_std_bad_array_new_length()
	{
		<Module>.TerminateProcess(<Module>.GetCurrentProcess(), 3U);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00002F58 File Offset: 0x00002358
	internal static void ??__E?A0xc7023181@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_trivial_v@M = true;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00002F6C File Offset: 0x0000236C
	internal static void ??__E?A0xc7023181@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_trivial_v@N = true;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00002F80 File Offset: 0x00002380
	internal static void ??__E?A0xc7023181@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_trivial_v@O = true;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00002F94 File Offset: 0x00002394
	internal static void ??__E?A0xc7023181@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0xc7023181.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00002FA8 File Offset: 0x000023A8
	internal static void ??__E?A0x60e3390d@_HUGE@@YMXXZ()
	{
		<Module>.?A0x60e3390d._HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00002FC4 File Offset: 0x000023C4
	internal static void ??__E?A0x60e3390d@HUGE@@YMXXZ()
	{
		<Module>.?A0x60e3390d.HUGE = double.PositiveInfinity;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00002FE0 File Offset: 0x000023E0
	internal static void ??__E?A0x60e3390d@?$is_same_v@H$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_same_v@H$$QEAH = false;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00002FF4 File Offset: 0x000023F4
	internal static void ??__E?A0x60e3390d@?$is_same_v@AEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_same_v@AEAH$$QEAH = false;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00003008 File Offset: 0x00002408
	internal static void ??__E?A0x60e3390d@?$is_same_v@$$QEAH$$QEAH@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_same_v@$$QEAH$$QEAH = true;
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000301C File Offset: 0x0000241C
	internal static void ??__E?A0x60e3390d@_FNV_offset_basis@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d._FNV_offset_basis = 14695981039346656037UL;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00003038 File Offset: 0x00002438
	internal static void ??__E?A0x60e3390d@_FNV_prime@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d._FNV_prime = 1099511628211UL;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00003054 File Offset: 0x00002454
	internal static void ??__E?A0x60e3390d@?$is_trivial_v@M@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_trivial_v@M = true;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00003068 File Offset: 0x00002468
	internal static void ??__E?A0x60e3390d@?$is_trivial_v@N@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_trivial_v@N = true;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000307C File Offset: 0x0000247C
	internal static void ??__E?A0x60e3390d@?$is_trivial_v@O@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_trivial_v@O = true;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00003090 File Offset: 0x00002490
	internal static void ??__E?A0x60e3390d@?$is_trivial_v@PEAX@std@@YMXXZ()
	{
		<Module>.std.?A0x60e3390d.?$is_trivial_v@PEAX = true;
	}

	// Token: 0x0600020F RID: 527
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int CloseHandle(void*);

	// Token: 0x06000210 RID: 528
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int GetFileInformationByHandle(void*, _BY_HANDLE_FILE_INFORMATION*);

	// Token: 0x06000211 RID: 529
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* CreateFileW(ushort*, uint, uint, _SECURITY_ATTRIBUTES*, uint, uint, void*);

	// Token: 0x06000212 RID: 530
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("VCRUNTIME140_CLR0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* memmove(void*, void*, ulong);

	// Token: 0x06000213 RID: 531
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* realloc(void*, ulong);

	// Token: 0x06000214 RID: 532
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void free(void*);

	// Token: 0x06000215 RID: 533
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* calloc(ulong, ulong);

	// Token: 0x06000216 RID: 534
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ADVAPI32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int RegCloseKey(HKEY__*);

	// Token: 0x06000217 RID: 535
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ADVAPI32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int RegOpenKeyExW(HKEY__*, ushort*, uint, uint, HKEY__**);

	// Token: 0x06000218 RID: 536
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int FreeLibrary(HINSTANCE__*);

	// Token: 0x06000219 RID: 537
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern HINSTANCE__* LoadLibraryW(ushort*);

	// Token: 0x0600021A RID: 538
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("SHLWAPI.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern ushort* PathCombineW(ushort*, ushort*, ushort*);

	// Token: 0x0600021B RID: 539
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("SHLWAPI.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int PathAppendW(ushort*, ushort*);

	// Token: 0x0600021C RID: 540
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern ushort* wcsncpy(ushort*, ushort*, ulong);

	// Token: 0x0600021D RID: 541
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern uint GetEnvironmentVariableW(ushort*, ushort*, uint);

	// Token: 0x0600021E RID: 542
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ADVAPI32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int RegQueryValueExW(HKEY__*, ushort*, uint*, uint*, byte*, uint*);

	// Token: 0x0600021F RID: 543
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern HINSTANCE__* LoadLibraryExW(ushort*, void*, uint);

	// Token: 0x06000220 RID: 544
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern HINSTANCE__* GetModuleHandleW(ushort*);

	// Token: 0x06000221 RID: 545
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern method GetProcAddress(HINSTANCE__*, sbyte*);

	// Token: 0x06000222 RID: 546
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("VCRUNTIME140_CLR0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x06000223 RID: 547
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int* _errno();

	// Token: 0x06000224 RID: 548
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal static extern void _invalid_parameter_noinfo();

	// Token: 0x06000225 RID: 549
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* malloc(ulong);

	// Token: 0x06000226 RID: 550
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("ucrtbase_clr0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal static extern int _callnewh(ulong);

	// Token: 0x06000227 RID: 551
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void InitializeSListHead(_SLIST_HEADER*);

	// Token: 0x06000228 RID: 552
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("VCRUNTIME140_CLR0400.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void __std_type_info_destroy_list(__type_info_node*);

	// Token: 0x06000229 RID: 553
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern int TerminateProcess(void*, uint);

	// Token: 0x0600022A RID: 554
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	internal unsafe static extern void* GetCurrentProcess();

	// Token: 0x04000001 RID: 1 RVA: 0x001739C8 File Offset: 0x001725C8
	internal static ??_C@_04NBGNOCAJ@dttf@$$BY0A@$$CBD ??_C@_04NBGNOCAJ@dttf@;

	// Token: 0x04000002 RID: 2 RVA: 0x00173988 File Offset: 0x00172588
	internal static ??_C@_04DACNFKGE@loca@$$BY0A@$$CBD ??_C@_04DACNFKGE@loca@;

	// Token: 0x04000003 RID: 3 RVA: 0x001739D0 File Offset: 0x001725D0
	internal static ??_C@_04DBJHPCCB@EBDT@$$BY0A@$$CBD ??_C@_04DBJHPCCB@EBDT@;

	// Token: 0x04000004 RID: 4 RVA: 0x001739D8 File Offset: 0x001725D8
	internal static ??_C@_04GMCNFBHO@bdat@$$BY0A@$$CBD ??_C@_04GMCNFBHO@bdat@;

	// Token: 0x04000005 RID: 5 RVA: 0x00173990 File Offset: 0x00172590
	internal static ??_C@_04LLIHEPK@glyf@$$BY0A@$$CBD ??_C@_04LLIHEPK@glyf@;

	// Token: 0x04000006 RID: 6 RVA: 0x00173978 File Offset: 0x00172578
	internal static ??_C@_04LGOLHALL@LTSH@$$BY0A@$$CBD ??_C@_04LGOLHALL@LTSH@;

	// Token: 0x04000007 RID: 7 RVA: 0x001739E0 File Offset: 0x001725E0
	internal static ??_C@_04ONMNCIMC@hmtx@$$BY0A@$$CBD ??_C@_04ONMNCIMC@hmtx@;

	// Token: 0x04000008 RID: 8 RVA: 0x001739E8 File Offset: 0x001725E8
	internal static ??_C@_04DCBNABCB@vmtx@$$BY0A@$$CBD ??_C@_04DCBNABCB@vmtx@;

	// Token: 0x04000009 RID: 9 RVA: 0x00173970 File Offset: 0x00172570
	internal static ??_C@_04IDDCPPLH@hdmx@$$BY0A@$$CBD ??_C@_04IDDCPPLH@hdmx@;

	// Token: 0x0400000A RID: 10 RVA: 0x00173940 File Offset: 0x00172540
	internal static ??_C@_04NEODDMOL@head@$$BY0A@$$CBD ??_C@_04NEODDMOL@head@;

	// Token: 0x0400000B RID: 11 RVA: 0x00173958 File Offset: 0x00172558
	internal static ??_C@_04KODIGLGG@maxp@$$BY0A@$$CBD ??_C@_04KODIGLGG@maxp@;

	// Token: 0x0400000C RID: 12 RVA: 0x00173948 File Offset: 0x00172548
	internal static ??_C@_04FMPHLIKP@hhea@$$BY0A@$$CBD ??_C@_04FMPHLIKP@hhea@;

	// Token: 0x0400000D RID: 13 RVA: 0x00173950 File Offset: 0x00172550
	internal static ??_C@_04IDCHJBEM@vhea@$$BY0A@$$CBD ??_C@_04IDCHJBEM@vhea@;

	// Token: 0x0400000E RID: 14 RVA: 0x00173980 File Offset: 0x00172580
	internal static ??_C@_04EICJPCEA@cmap@$$BY0A@$$CBD ??_C@_04EICJPCEA@cmap@;

	// Token: 0x0400000F RID: 15 RVA: 0x001739F0 File Offset: 0x001725F0
	internal static ??_C@_04DKAHCHAP@EBLC@$$BY0A@$$CBD ??_C@_04DKAHCHAP@EBLC@;

	// Token: 0x04000010 RID: 16 RVA: 0x001739F8 File Offset: 0x001725F8
	internal static ??_C@_04KGIENAAN@bloc@$$BY0A@$$CBD ??_C@_04KGIENAAN@bloc@;

	// Token: 0x04000011 RID: 17 RVA: 0x00173968 File Offset: 0x00172568
	internal static ??_C@_04OEKHDCJK@OS?12@$$BY0A@$$CBD ??_C@_04OEKHDCJK@OS?12@;

	// Token: 0x04000012 RID: 18 RVA: 0x001739A0 File Offset: 0x001725A0
	internal static ??_C@_04NJFLOCNM@fpgm@$$BY0A@$$CBD ??_C@_04NJFLOCNM@fpgm@;

	// Token: 0x04000013 RID: 19 RVA: 0x00173998 File Offset: 0x00172598
	internal static ??_C@_04GABKPAAH@prep@$$BY0A@$$CBD ??_C@_04GABKPAAH@prep@;

	// Token: 0x04000014 RID: 20 RVA: 0x00173A28 File Offset: 0x00172628
	internal static ??_C@_04GPGHBOBB@cvt?5@$$BY0A@$$CBD ??_C@_04GPGHBOBB@cvt?5@;

	// Token: 0x04000015 RID: 21 RVA: 0x00173A30 File Offset: 0x00172630
	internal static ??_C@_04OINNJMCG@gasp@$$BY0A@$$CBD ??_C@_04OINNJMCG@gasp@;

	// Token: 0x04000016 RID: 22 RVA: 0x00173A38 File Offset: 0x00172638
	internal static ??_C@_04CPDINMAO@PCLT@$$BY0A@$$CBD ??_C@_04CPDINMAO@PCLT@;

	// Token: 0x04000017 RID: 23 RVA: 0x00173960 File Offset: 0x00172560
	internal static ??_C@_04LOKOGFID@post@$$BY0A@$$CBD ??_C@_04LOKOGFID@post@;

	// Token: 0x04000018 RID: 24 RVA: 0x00173A00 File Offset: 0x00172600
	internal static ??_C@_04MEMAJGDJ@name@$$BY0A@$$CBD ??_C@_04MEMAJGDJ@name@;

	// Token: 0x04000019 RID: 25 RVA: 0x00173A08 File Offset: 0x00172608
	internal static ??_C@_04CNHNMDEC@EBSC@$$BY0A@$$CBD ??_C@_04CNHNMDEC@EBSC@;

	// Token: 0x0400001A RID: 26 RVA: 0x00173A10 File Offset: 0x00172610
	internal static ??_C@_04JFNCAMCC@bsca@$$BY0A@$$CBD ??_C@_04JFNCAMCC@bsca@;

	// Token: 0x0400001B RID: 27 RVA: 0x00173A18 File Offset: 0x00172618
	internal static ??_C@_04HHMMLDJI@kern@$$BY0A@$$CBD ??_C@_04HHMMLDJI@kern@;

	// Token: 0x0400001C RID: 28 RVA: 0x00173A20 File Offset: 0x00172620
	internal static ??_C@_04JANIDECM@VDMX@$$BY0A@$$CBD ??_C@_04JANIDECM@VDMX@;

	// Token: 0x0400001D RID: 29 RVA: 0x001739A8 File Offset: 0x001725A8
	internal static ??_C@_04CHBMFMIH@mort@$$BY0A@$$CBD ??_C@_04CHBMFMIH@mort@;

	// Token: 0x0400001E RID: 30 RVA: 0x001739B0 File Offset: 0x001725B0
	internal static ??_C@_04KCIOONCL@GSUB@$$BY0A@$$CBD ??_C@_04KCIOONCL@GSUB@;

	// Token: 0x0400001F RID: 31 RVA: 0x001739B8 File Offset: 0x001725B8
	internal static ??_C@_04DPLAIGKJ@JSTF@$$BY0A@$$CBD ??_C@_04DPLAIGKJ@JSTF@;

	// Token: 0x04000020 RID: 32 RVA: 0x001739C0 File Offset: 0x001725C0
	internal static ??_C@_04NLLCBHDK@BASE@$$BY0A@$$CBD ??_C@_04NLLCBHDK@BASE@;

	// Token: 0x04000021 RID: 33
	[FixedAddressValueType]
	internal static $ArrayType$$$BY09UCONTROL_TABLE@TtfDelta@Internal@MS@@ Control_Table;

	// Token: 0x04000022 RID: 34
	[FixedAddressValueType]
	internal static MORTHEADER_CONTROL$$BY0A@E MORTHEADER_CONTROL;

	// Token: 0x04000023 RID: 35
	[FixedAddressValueType]
	internal static MORTLOOKUPSINGLE_CONTROL$$BY0A@E MORTLOOKUPSINGLE_CONTROL;

	// Token: 0x04000024 RID: 36
	[FixedAddressValueType]
	internal static MORTBINSRCHHEADER_CONTROL$$BY0A@E MORTBINSRCHHEADER_CONTROL;

	// Token: 0x04000025 RID: 37
	[FixedAddressValueType]
	internal static BASECOORDFORMAT2_CONTROL$$BY0A@E BASECOORDFORMAT2_CONTROL;

	// Token: 0x04000026 RID: 38
	[FixedAddressValueType]
	internal static BASEMINMAX_CONTROL$$BY0A@E BASEMINMAX_CONTROL;

	// Token: 0x04000027 RID: 39
	[FixedAddressValueType]
	internal static BASEFEATMINMAXRECORD_CONTROL$$BY0A@E BASEFEATMINMAXRECORD_CONTROL;

	// Token: 0x04000028 RID: 40
	[FixedAddressValueType]
	internal static BASEVALUES_CONTROL$$BY0A@E BASEVALUES_CONTROL;

	// Token: 0x04000029 RID: 41
	[FixedAddressValueType]
	internal static BASESCRIPT_CONTROL$$BY0A@E BASESCRIPT_CONTROL;

	// Token: 0x0400002A RID: 42
	[FixedAddressValueType]
	internal static BASELANGSYSRECORD_CONTROL$$BY0A@E BASELANGSYSRECORD_CONTROL;

	// Token: 0x0400002B RID: 43
	[FixedAddressValueType]
	internal static BASESCRIPTLIST_CONTROL$$BY0A@E BASESCRIPTLIST_CONTROL;

	// Token: 0x0400002C RID: 44
	[FixedAddressValueType]
	internal static BASESCRIPTRECORD_CONTROL$$BY0A@E BASESCRIPTRECORD_CONTROL;

	// Token: 0x0400002D RID: 45
	[FixedAddressValueType]
	internal static BASEAXIS_CONTROL$$BY0A@E BASEAXIS_CONTROL;

	// Token: 0x0400002E RID: 46
	[FixedAddressValueType]
	internal static BASEHEADER_CONTROL$$BY0A@E BASEHEADER_CONTROL;

	// Token: 0x0400002F RID: 47
	[FixedAddressValueType]
	internal static JSTFEXTENDERGLYPH_CONTROL$$BY0A@E JSTFEXTENDERGLYPH_CONTROL;

	// Token: 0x04000030 RID: 48
	[FixedAddressValueType]
	internal static JSTFSCRIPT_CONTROL$$BY0A@E JSTFSCRIPT_CONTROL;

	// Token: 0x04000031 RID: 49
	[FixedAddressValueType]
	internal static JSTFLANGSYSRECORD_CONTROL$$BY0A@E JSTFLANGSYSRECORD_CONTROL;

	// Token: 0x04000032 RID: 50
	[FixedAddressValueType]
	internal static JSTFHEADER_CONTROL$$BY0A@E JSTFHEADER_CONTROL;

	// Token: 0x04000033 RID: 51
	[FixedAddressValueType]
	internal static JSTFSCRIPTRECORD_CONTROL$$BY0A@E JSTFSCRIPTRECORD_CONTROL;

	// Token: 0x04000034 RID: 52
	[FixedAddressValueType]
	internal static GSUBCONTEXTSUBSTFORMAT3_CONTROL$$BY0A@E GSUBCONTEXTSUBSTFORMAT3_CONTROL;

	// Token: 0x04000035 RID: 53
	[FixedAddressValueType]
	internal static GSUBCONTEXTSUBSTFORMAT2_CONTROL$$BY0A@E GSUBCONTEXTSUBSTFORMAT2_CONTROL;

	// Token: 0x04000036 RID: 54
	[FixedAddressValueType]
	internal static GSUBSUBCLASSSET_CONTROL$$BY0A@E GSUBSUBCLASSSET_CONTROL;

	// Token: 0x04000037 RID: 55
	[FixedAddressValueType]
	internal static GSUBSUBCLASSRULE_CONTROL$$BY0A@E GSUBSUBCLASSRULE_CONTROL;

	// Token: 0x04000038 RID: 56
	[FixedAddressValueType]
	internal static GSUBCONTEXTSUBSTFORMAT1_CONTROL$$BY0A@E GSUBCONTEXTSUBSTFORMAT1_CONTROL;

	// Token: 0x04000039 RID: 57
	[FixedAddressValueType]
	internal static GSUBSUBRULESET_CONTROL$$BY0A@E GSUBSUBRULESET_CONTROL;

	// Token: 0x0400003A RID: 58
	[FixedAddressValueType]
	internal static GSUBSUBRULE_CONTROL$$BY0A@E GSUBSUBRULE_CONTROL;

	// Token: 0x0400003B RID: 59
	[FixedAddressValueType]
	internal static GSUBSUBSTLOOKUPRECORD_CONTROL$$BY0A@E GSUBSUBSTLOOKUPRECORD_CONTROL;

	// Token: 0x0400003C RID: 60
	[FixedAddressValueType]
	internal static GSUBLIGATURESUBSTFORMAT1_CONTROL$$BY0A@E GSUBLIGATURESUBSTFORMAT1_CONTROL;

	// Token: 0x0400003D RID: 61
	[FixedAddressValueType]
	internal static GSUBLIGATURESET_CONTROL$$BY0A@E GSUBLIGATURESET_CONTROL;

	// Token: 0x0400003E RID: 62
	[FixedAddressValueType]
	internal static GSUBLIGATURE_CONTROL$$BY0A@E GSUBLIGATURE_CONTROL;

	// Token: 0x0400003F RID: 63
	[FixedAddressValueType]
	internal static GSUBALTERNATESUBSTFORMAT1_CONTROL$$BY0A@E GSUBALTERNATESUBSTFORMAT1_CONTROL;

	// Token: 0x04000040 RID: 64
	[FixedAddressValueType]
	internal static GSUBALTERNATESET_CONTROL$$BY0A@E GSUBALTERNATESET_CONTROL;

	// Token: 0x04000041 RID: 65
	[FixedAddressValueType]
	internal static GSUBMULTIPLESUBSTFORMAT1_CONTROL$$BY0A@E GSUBMULTIPLESUBSTFORMAT1_CONTROL;

	// Token: 0x04000042 RID: 66
	[FixedAddressValueType]
	internal static GSUBSEQUENCE_CONTROL$$BY0A@E GSUBSEQUENCE_CONTROL;

	// Token: 0x04000043 RID: 67
	[FixedAddressValueType]
	internal static GSUBSINGLESUBSTFORMAT2_CONTROL$$BY0A@E GSUBSINGLESUBSTFORMAT2_CONTROL;

	// Token: 0x04000044 RID: 68
	[FixedAddressValueType]
	internal static GSUBSINGLESUBSTFORMAT1_CONTROL$$BY0A@E GSUBSINGLESUBSTFORMAT1_CONTROL;

	// Token: 0x04000045 RID: 69
	[FixedAddressValueType]
	internal static GSUBHEADER_CONTROL$$BY0A@E GSUBHEADER_CONTROL;

	// Token: 0x04000046 RID: 70
	[FixedAddressValueType]
	internal static GSUBCOVERAGEFORMAT2_CONTROL$$BY0A@E GSUBCOVERAGEFORMAT2_CONTROL;

	// Token: 0x04000047 RID: 71
	[FixedAddressValueType]
	internal static GSUBRANGERECORD_CONTROL$$BY0A@E GSUBRANGERECORD_CONTROL;

	// Token: 0x04000048 RID: 72
	[FixedAddressValueType]
	internal static GSUBCOVERAGEFORMAT1_CONTROL$$BY0A@E GSUBCOVERAGEFORMAT1_CONTROL;

	// Token: 0x04000049 RID: 73
	[FixedAddressValueType]
	internal static GSUBLOOKUPLIST_CONTROL$$BY0A@E GSUBLOOKUPLIST_CONTROL;

	// Token: 0x0400004A RID: 74
	[FixedAddressValueType]
	internal static GSUBLOOKUP_CONTROL$$BY0A@E GSUBLOOKUP_CONTROL;

	// Token: 0x0400004B RID: 75
	[FixedAddressValueType]
	internal static GSUBFEATURELIST_CONTROL$$BY0A@E GSUBFEATURELIST_CONTROL;

	// Token: 0x0400004C RID: 76
	[FixedAddressValueType]
	internal static GSUBFEATURERECORD_CONTROL$$BY0A@E GSUBFEATURERECORD_CONTROL;

	// Token: 0x0400004D RID: 77
	[FixedAddressValueType]
	internal static GSUBFEATURE_CONTROL$$BY0A@E GSUBFEATURE_CONTROL;

	// Token: 0x0400004E RID: 78
	[FixedAddressValueType]
	internal static EBDTFORMAT9_CONTROL$$BY0A@E EBDTFORMAT9_CONTROL;

	// Token: 0x0400004F RID: 79
	[FixedAddressValueType]
	internal static EBDTFORMAT8SIZE_CONTROL$$BY0A@E EBDTFORMAT8SIZE_CONTROL;

	// Token: 0x04000050 RID: 80
	[FixedAddressValueType]
	internal static EBDTCOMPONENT_CONTROL$$BY0A@E EBDTCOMPONENT_CONTROL;

	// Token: 0x04000051 RID: 81
	[FixedAddressValueType]
	internal static EBDTHEADERNOXLATENOPAD_CONTROL$$BY0A@E EBDTHEADERNOXLATENOPAD_CONTROL;

	// Token: 0x04000052 RID: 82
	[FixedAddressValueType]
	internal static EBDTHEADER_CONTROL$$BY0A@E EBDTHEADER_CONTROL;

	// Token: 0x04000053 RID: 83
	[FixedAddressValueType]
	internal static INDEXSUBTABLE5_CONTROL$$BY0A@E INDEXSUBTABLE5_CONTROL;

	// Token: 0x04000054 RID: 84
	[FixedAddressValueType]
	internal static INDEXSUBTABLE4_CONTROL$$BY0A@E INDEXSUBTABLE4_CONTROL;

	// Token: 0x04000055 RID: 85
	[FixedAddressValueType]
	internal static CODEOFFSETPAIR_CONTROL$$BY0A@E CODEOFFSETPAIR_CONTROL;

	// Token: 0x04000056 RID: 86
	[FixedAddressValueType]
	internal static INDEXSUBTABLE3_CONTROL$$BY0A@E INDEXSUBTABLE3_CONTROL;

	// Token: 0x04000057 RID: 87
	[FixedAddressValueType]
	internal static INDEXSUBTABLE2_CONTROL$$BY0A@E INDEXSUBTABLE2_CONTROL;

	// Token: 0x04000058 RID: 88
	[FixedAddressValueType]
	internal static INDEXSUBTABLE1_CONTROL$$BY0A@E INDEXSUBTABLE1_CONTROL;

	// Token: 0x04000059 RID: 89
	[FixedAddressValueType]
	internal static INDEXSUBHEADER_CONTROL$$BY0A@E INDEXSUBHEADER_CONTROL;

	// Token: 0x0400005A RID: 90
	[FixedAddressValueType]
	internal static INDEXSUBTABLEARRAY_CONTROL$$BY0A@E INDEXSUBTABLEARRAY_CONTROL;

	// Token: 0x0400005B RID: 91
	[FixedAddressValueType]
	internal static SMALLGLYPHMETRICS_CONTROL$$BY0A@E SMALLGLYPHMETRICS_CONTROL;

	// Token: 0x0400005C RID: 92
	[FixedAddressValueType]
	internal static BIGGLYPHMETRICS_CONTROL$$BY0A@E BIGGLYPHMETRICS_CONTROL;

	// Token: 0x0400005D RID: 93
	[FixedAddressValueType]
	internal static BITMAPSIZETABLE_CONTROL$$BY0A@E BITMAPSIZETABLE_CONTROL;

	// Token: 0x0400005E RID: 94
	[FixedAddressValueType]
	internal static SBITLINEMETRICS_CONTROL$$BY0A@E SBITLINEMETRICS_CONTROL;

	// Token: 0x0400005F RID: 95
	[FixedAddressValueType]
	internal static EBLCHEADER_CONTROL$$BY0A@E EBLCHEADER_CONTROL;

	// Token: 0x04000060 RID: 96
	[FixedAddressValueType]
	internal static VERSION2OS2_CONTROL$$BY0A@E VERSION2OS2_CONTROL;

	// Token: 0x04000061 RID: 97
	[FixedAddressValueType]
	internal static NEWOS2_CONTROL$$BY0A@E NEWOS2_CONTROL;

	// Token: 0x04000062 RID: 98
	[FixedAddressValueType]
	internal static OS2_CONTROL$$BY0A@E OS2_CONTROL;

	// Token: 0x04000063 RID: 99
	[FixedAddressValueType]
	internal static OS2_PANOSE_CONTROL$$BY0A@E OS2_PANOSE_CONTROL;

	// Token: 0x04000064 RID: 100
	[FixedAddressValueType]
	internal static SEARCH_PAIRS_CONTROL$$BY0A@E SEARCH_PAIRS_CONTROL;

	// Token: 0x04000065 RID: 101
	[FixedAddressValueType]
	internal static KERN_PAIR_CONTROL$$BY0A@E KERN_PAIR_CONTROL;

	// Token: 0x04000066 RID: 102
	[FixedAddressValueType]
	internal static KERN_FORMAT_0_CONTROL$$BY0A@E KERN_FORMAT_0_CONTROL;

	// Token: 0x04000067 RID: 103
	[FixedAddressValueType]
	internal static KERN_SUB_HEADER_CONTROL$$BY0A@E KERN_SUB_HEADER_CONTROL;

	// Token: 0x04000068 RID: 104
	[FixedAddressValueType]
	internal static KERN_HEADER_CONTROL$$BY0A@E KERN_HEADER_CONTROL;

	// Token: 0x04000069 RID: 105
	[FixedAddressValueType]
	internal static VDMX_CONTROL$$BY0A@E VDMX_CONTROL;

	// Token: 0x0400006A RID: 106
	[FixedAddressValueType]
	internal static VDMXRATIO_CONTROL$$BY0A@E VDMXRATIO_CONTROL;

	// Token: 0x0400006B RID: 107
	[FixedAddressValueType]
	internal static VDMXGROUP_CONTROL$$BY0A@E VDMXGROUP_CONTROL;

	// Token: 0x0400006C RID: 108
	[FixedAddressValueType]
	internal static VDMXVTABLE_CONTROL$$BY0A@E VDMXVTABLE_CONTROL;

	// Token: 0x0400006D RID: 109
	[FixedAddressValueType]
	internal static HDMX_CONTROL$$BY0A@E HDMX_CONTROL;

	// Token: 0x0400006E RID: 110
	[FixedAddressValueType]
	internal static HDMX_DEVICE_REC_CONTROL$$BY0A@E HDMX_DEVICE_REC_CONTROL;

	// Token: 0x0400006F RID: 111
	[FixedAddressValueType]
	internal static NAME_HEADER_CONTROL$$BY0A@E NAME_HEADER_CONTROL;

	// Token: 0x04000070 RID: 112
	[FixedAddressValueType]
	internal static NAME_RECORD_CONTROL$$BY0A@E NAME_RECORD_CONTROL;

	// Token: 0x04000071 RID: 113
	[FixedAddressValueType]
	internal static LTSH_CONTROL$$BY0A@E LTSH_CONTROL;

	// Token: 0x04000072 RID: 114
	[FixedAddressValueType]
	internal static TSB_CONTROL$$BY0A@E TSB_CONTROL;

	// Token: 0x04000073 RID: 115
	[FixedAddressValueType]
	internal static XSB_CONTROL$$BY0A@E XSB_CONTROL;

	// Token: 0x04000074 RID: 116
	[FixedAddressValueType]
	internal static LONGXMETRIC_CONTROL$$BY0A@E LONGXMETRIC_CONTROL;

	// Token: 0x04000075 RID: 117
	[FixedAddressValueType]
	internal static XHEA_CONTROL$$BY0A@E XHEA_CONTROL;

	// Token: 0x04000076 RID: 118
	[FixedAddressValueType]
	internal static LONGVERMETRIC_CONTROL$$BY0A@E LONGVERMETRIC_CONTROL;

	// Token: 0x04000077 RID: 119
	[FixedAddressValueType]
	internal static VHEA_CONTROL$$BY0A@E VHEA_CONTROL;

	// Token: 0x04000078 RID: 120
	[FixedAddressValueType]
	internal static LSB_CONTROL$$BY0A@E LSB_CONTROL;

	// Token: 0x04000079 RID: 121
	[FixedAddressValueType]
	internal static LONGHORMETRIC_CONTROL$$BY0A@E LONGHORMETRIC_CONTROL;

	// Token: 0x0400007A RID: 122
	[FixedAddressValueType]
	internal static HHEA_CONTROL$$BY0A@E HHEA_CONTROL;

	// Token: 0x0400007B RID: 123
	[FixedAddressValueType]
	internal static HEAD_CONTROL$$BY0A@E HEAD_CONTROL;

	// Token: 0x0400007C RID: 124
	[FixedAddressValueType]
	internal static COMPOSITE_GLYPH_CONTROL$$BY0A@E COMPOSITE_GLYPH_CONTROL;

	// Token: 0x0400007D RID: 125
	[FixedAddressValueType]
	internal static SIMPLE_GLYPH_CONTROL$$BY0A@E SIMPLE_GLYPH_CONTROL;

	// Token: 0x0400007E RID: 126
	[FixedAddressValueType]
	internal static GLYF_HEADER_CONTROL$$BY0A@E GLYF_HEADER_CONTROL;

	// Token: 0x0400007F RID: 127
	[FixedAddressValueType]
	internal static POST_CONTROL$$BY0A@E POST_CONTROL;

	// Token: 0x04000080 RID: 128
	[FixedAddressValueType]
	internal static FORMAT12_GROUPS_CONTROL$$BY0A@E FORMAT12_GROUPS_CONTROL;

	// Token: 0x04000081 RID: 129
	[FixedAddressValueType]
	internal static CMAP_FORMAT12_CONTROL$$BY0A@E CMAP_FORMAT12_CONTROL;

	// Token: 0x04000082 RID: 130
	[FixedAddressValueType]
	internal static FORMAT4_SEGMENTS_CONTROL$$BY0A@E FORMAT4_SEGMENTS_CONTROL;

	// Token: 0x04000083 RID: 131
	[FixedAddressValueType]
	internal static CMAP_FORMAT4_CONTROL$$BY0A@E CMAP_FORMAT4_CONTROL;

	// Token: 0x04000084 RID: 132
	[FixedAddressValueType]
	internal static CMAP_FORMAT6_CONTROL$$BY0A@E CMAP_FORMAT6_CONTROL;

	// Token: 0x04000085 RID: 133
	[FixedAddressValueType]
	internal static CMAP_FORMAT0_CONTROL$$BY0A@E CMAP_FORMAT0_CONTROL;

	// Token: 0x04000086 RID: 134
	[FixedAddressValueType]
	internal static CMAP_SUBHEADER_CONTROL$$BY0A@E CMAP_SUBHEADER_CONTROL;

	// Token: 0x04000087 RID: 135
	[FixedAddressValueType]
	internal static CMAP_TABLELOC_CONTROL$$BY0A@E CMAP_TABLELOC_CONTROL;

	// Token: 0x04000088 RID: 136
	[FixedAddressValueType]
	internal static CMAP_HEADER_CONTROL$$BY0A@E CMAP_HEADER_CONTROL;

	// Token: 0x04000089 RID: 137
	[FixedAddressValueType]
	internal static DIRECTORY_NO_XLATE_CONTROL$$BY0A@E DIRECTORY_NO_XLATE_CONTROL;

	// Token: 0x0400008A RID: 138
	[FixedAddressValueType]
	internal static WORD_CONTROL$$BY0A@E WORD_CONTROL;

	// Token: 0x0400008B RID: 139
	[FixedAddressValueType]
	internal static BYTE_CONTROL$$BY0A@E BYTE_CONTROL;

	// Token: 0x0400008C RID: 140
	[FixedAddressValueType]
	internal static MAXP_CONTROL$$BY0A@E MAXP_CONTROL;

	// Token: 0x0400008D RID: 141
	[FixedAddressValueType]
	internal static DIRECTORY_CONTROL$$BY0A@E DIRECTORY_CONTROL;

	// Token: 0x0400008E RID: 142
	[FixedAddressValueType]
	internal static OFFSET_TABLE_CONTROL$$BY0A@E OFFSET_TABLE_CONTROL;

	// Token: 0x0400008F RID: 143
	[FixedAddressValueType]
	internal static DTTF_HEADER_CONTROL$$BY0A@E DTTF_HEADER_CONTROL;

	// Token: 0x04000090 RID: 144
	[FixedAddressValueType]
	internal static LONG_CONTROL$$BY0A@E LONG_CONTROL;

	// Token: 0x04000091 RID: 145
	[FixedAddressValueType]
	internal static TTC_HEADER_CONTROL$$BY0A@E TTC_HEADER_CONTROL;

	// Token: 0x04000092 RID: 146
	[FixedAddressValueType]
	internal static int __@@_PchSym_@00@UyrmzirvhUrmgvinvwrzgvUdkuUkivhvmgzgrlmxlivxkkOmzgrevkilqPFACEEJAFIUlyqiUznwGEUkivxlnkOlyq@PresentationCoreManagedCpp;

	// Token: 0x04000093 RID: 147 RVA: 0x00173A40 File Offset: 0x00172640
	internal static ??_C@_1CC@NDKAMMGP@?$AAw?$AAp?$AAf?$AAg?$AAf?$AAx?$AA_?$AAv?$AA0?$AA4?$AA0?$AA0?$AA?4?$AAd?$AAl@$$BY0A@$$CBG ??_C@_1CC@NDKAMMGP@?$AAw?$AAp?$AAf?$AAg?$AAf?$AAx?$AA_?$AAv?$AA0?$AA4?$AA0?$AA0?$AA?4?$AAd?$AAl@;

	// Token: 0x04000094 RID: 148 RVA: 0x00173A68 File Offset: 0x00172668
	internal static ??_C@_1DK@CKKFELDO@?$AAP?$AAr?$AAe?$AAs?$AAe?$AAn?$AAt?$AAa?$AAt?$AAi?$AAo?$AAn?$AAN?$AAa?$AAt@$$BY0A@$$CBG ??_C@_1DK@CKKFELDO@?$AAP?$AAr?$AAe?$AAs?$AAe?$AAn?$AAt?$AAa?$AAt?$AAi?$AAo?$AAn?$AAN?$AAa?$AAt@;

	// Token: 0x04000095 RID: 149
	[FixedAddressValueType]
	internal static long unused;

	// Token: 0x04000096 RID: 150 RVA: 0x001738E8 File Offset: 0x001724E8
	internal static method unused$initializer$;

	// Token: 0x04000097 RID: 151
	[FixedAddressValueType]
	internal static AccessType SA_ReadWrite;

	// Token: 0x04000098 RID: 152
	[FixedAddressValueType]
	internal static YesNoMaybe SA_No;

	// Token: 0x04000099 RID: 153
	[FixedAddressValueType]
	internal static YesNoMaybe SA_Maybe;

	// Token: 0x0400009A RID: 154 RVA: 0x00173018 File Offset: 0x00171C18
	internal static method SA_Maybe$initializer$;

	// Token: 0x0400009B RID: 155
	[FixedAddressValueType]
	internal static AccessType SA_Read;

	// Token: 0x0400009C RID: 156 RVA: 0x00173028 File Offset: 0x00171C28
	internal static method SA_Read$initializer$;

	// Token: 0x0400009D RID: 157 RVA: 0x00173038 File Offset: 0x00171C38
	internal static method SA_ReadWrite$initializer$;

	// Token: 0x0400009E RID: 158
	[FixedAddressValueType]
	internal static AccessType SA_NoAccess;

	// Token: 0x0400009F RID: 159
	[FixedAddressValueType]
	internal static YesNoMaybe SA_Yes;

	// Token: 0x040000A0 RID: 160 RVA: 0x00173008 File Offset: 0x00171C08
	internal static method SA_Yes$initializer$;

	// Token: 0x040000A1 RID: 161
	[FixedAddressValueType]
	internal static AccessType SA_Write;

	// Token: 0x040000A2 RID: 162 RVA: 0x00173030 File Offset: 0x00171C30
	internal static method SA_Write$initializer$;

	// Token: 0x040000A3 RID: 163
	[FixedAddressValueType]
	internal static IntPtr cmiStartupRunner;

	// Token: 0x040000A4 RID: 164 RVA: 0x00173020 File Offset: 0x00171C20
	internal static method SA_NoAccess$initializer$;

	// Token: 0x040000A5 RID: 165 RVA: 0x00173010 File Offset: 0x00171C10
	internal static method SA_No$initializer$;

	// Token: 0x040000A6 RID: 166 RVA: 0x00173AA8 File Offset: 0x001726A8
	internal static ??_C@_1CA@MAEHCNED@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAV?$AAe?$AAr?$AAs?$AAi?$AAo?$AAn@$$BY0A@$$CBG ??_C@_1CA@MAEHCNED@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAV?$AAe?$AAr?$AAs?$AAi?$AAo?$AAn@;

	// Token: 0x040000A7 RID: 167 RVA: 0x00173AC8 File Offset: 0x001726C8
	internal static ??_C@_1CI@CAKAMLOG@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl@$$BY0A@$$CBG ??_C@_1CI@CAKAMLOG@?$AAC?$AAO?$AAM?$AAP?$AAL?$AAU?$AAS?$AA_?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl@;

	// Token: 0x040000A8 RID: 168 RVA: 0x00173B38 File Offset: 0x00172738
	internal static ??_C@_1BI@LHIOEDHG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAR?$AAo?$AAo?$AAt@$$BY0A@$$CBG ??_C@_1BI@LHIOEDHG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAR?$AAo?$AAo?$AAt@;

	// Token: 0x040000A9 RID: 169 RVA: 0x00173AF0 File Offset: 0x001726F0
	internal static ??_C@_1EC@CFPPBACA@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@$$BY0A@$$CBG ??_C@_1EC@CFPPBACA@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@;

	// Token: 0x040000AA RID: 170 RVA: 0x00173BC0 File Offset: 0x001727C0
	internal static ??_C@_1BI@NFCPJLAG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAP?$AAa?$AAt?$AAh@$$BY0A@$$CBG ??_C@_1BI@NFCPJLAG@?$AAI?$AAn?$AAs?$AAt?$AAa?$AAl?$AAl?$AAP?$AAa?$AAt?$AAh@;

	// Token: 0x040000AB RID: 171 RVA: 0x00173B50 File Offset: 0x00172750
	internal static ??_C@_1GK@HPFJOIOL@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@$$BY0A@$$CBG ??_C@_1GK@HPFJOIOL@?$AAS?$AAo?$AAf?$AAt?$AAw?$AAa?$AAr?$AAe?$AA?2?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs@;

	// Token: 0x040000AC RID: 172 RVA: 0x00173BD8 File Offset: 0x001727D8
	internal static ??_C@_17HHMPNFFP@?$AAW?$AAP?$AAF@$$BY0A@$$CBG ??_C@_17HHMPNFFP@?$AAW?$AAP?$AAF@;

	// Token: 0x040000AD RID: 173
	[FixedAddressValueType]
	internal static AccessType SA_ReadWrite;

	// Token: 0x040000AE RID: 174
	[FixedAddressValueType]
	internal static YesNoMaybe SA_No;

	// Token: 0x040000AF RID: 175
	[FixedAddressValueType]
	internal static YesNoMaybe SA_Maybe;

	// Token: 0x040000B0 RID: 176 RVA: 0x00173050 File Offset: 0x00171C50
	internal static method SA_Maybe$initializer$;

	// Token: 0x040000B1 RID: 177
	[FixedAddressValueType]
	internal static AccessType SA_Read;

	// Token: 0x040000B2 RID: 178 RVA: 0x00173060 File Offset: 0x00171C60
	internal static method SA_Read$initializer$;

	// Token: 0x040000B3 RID: 179 RVA: 0x00173070 File Offset: 0x00171C70
	internal static method SA_ReadWrite$initializer$;

	// Token: 0x040000B4 RID: 180
	[FixedAddressValueType]
	internal static AccessType SA_NoAccess;

	// Token: 0x040000B5 RID: 181
	[FixedAddressValueType]
	internal static YesNoMaybe SA_Yes;

	// Token: 0x040000B6 RID: 182 RVA: 0x00173040 File Offset: 0x00171C40
	internal static method SA_Yes$initializer$;

	// Token: 0x040000B7 RID: 183
	[FixedAddressValueType]
	internal static AccessType SA_Write;

	// Token: 0x040000B8 RID: 184 RVA: 0x00173068 File Offset: 0x00171C68
	internal static method SA_Write$initializer$;

	// Token: 0x040000B9 RID: 185 RVA: 0x00173058 File Offset: 0x00171C58
	internal static method SA_NoAccess$initializer$;

	// Token: 0x040000BA RID: 186 RVA: 0x00173048 File Offset: 0x00171C48
	internal static method SA_No$initializer$;

	// Token: 0x040000BB RID: 187 RVA: 0x00173BE0 File Offset: 0x001727E0
	internal static ??_C@_1BK@MGMFAEKH@?$AAk?$AAe?$AAr?$AAn?$AAe?$AAl?$AA3?$AA2?$AA?4?$AAd?$AAl?$AAl@$$BY0A@$$CBG ??_C@_1BK@MGMFAEKH@?$AAk?$AAe?$AAr?$AAn?$AAe?$AAl?$AA3?$AA2?$AA?4?$AAd?$AAl?$AAl@;

	// Token: 0x040000BC RID: 188 RVA: 0x00173C00 File Offset: 0x00172800
	internal static ??_C@_0BA@NMFGOKJN@AddDllDirectory@$$BY0A@$$CBD ??_C@_0BA@NMFGOKJN@AddDllDirectory@;

	// Token: 0x040000BD RID: 189 RVA: 0x00173C10 File Offset: 0x00172810
	internal static ??_C@_1BG@HGHNANM@?$AAd?$AAw?$AAr?$AAi?$AAt?$AAe?$AA?4?$AAd?$AAl?$AAl@$$BY0A@$$CBG ??_C@_1BG@HGHNANM@?$AAd?$AAw?$AAr?$AAi?$AAt?$AAe?$AA?4?$AAd?$AAl?$AAl@;

	// Token: 0x040000BE RID: 190 RVA: 0x00173C28 File Offset: 0x00172828
	internal static ??_C@_0BE@JOALNNKC@DWriteCreateFactory@$$BY0A@$$CBD ??_C@_0BE@JOALNNKC@DWriteCreateFactory@;

	// Token: 0x040000BF RID: 191
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000C0 RID: 192 RVA: 0x00173090 File Offset: 0x00171C90
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C1 RID: 193
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000C2 RID: 194 RVA: 0x001730A8 File Offset: 0x00171CA8
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C3 RID: 195
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000C4 RID: 196
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000C5 RID: 197
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x040000C6 RID: 198
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000C7 RID: 199
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x040000C8 RID: 200
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040000C9 RID: 201 RVA: 0x00173078 File Offset: 0x00171C78
	internal static method _HUGE$initializer$;

	// Token: 0x040000CA RID: 202 RVA: 0x00173930 File Offset: 0x00172530
	internal static __xi_vt_z$$BY0A@Q6MPEBXXZ __xi_vt_z;

	// Token: 0x040000CB RID: 203 RVA: 0x001730B0 File Offset: 0x00171CB0
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CC RID: 204 RVA: 0x00173000 File Offset: 0x00171C00
	internal static __xc_ma_a$$BY0A@Q6MPEBXXZ __xc_ma_a;

	// Token: 0x040000CD RID: 205 RVA: 0x00173920 File Offset: 0x00172520
	internal static __xc_ma_z$$BY0A@Q6MPEBXXZ __xc_ma_z;

	// Token: 0x040000CE RID: 206 RVA: 0x001730B8 File Offset: 0x00171CB8
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CF RID: 207 RVA: 0x00173928 File Offset: 0x00172528
	internal static __xi_vt_a$$BY0A@Q6MPEBXXZ __xi_vt_a;

	// Token: 0x040000D0 RID: 208
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040000D1 RID: 209 RVA: 0x00173088 File Offset: 0x00171C88
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000D2 RID: 210 RVA: 0x001730A0 File Offset: 0x00171CA0
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000D3 RID: 211 RVA: 0x00173080 File Offset: 0x00171C80
	internal static method HUGE$initializer$;

	// Token: 0x040000D4 RID: 212 RVA: 0x00173098 File Offset: 0x00171C98
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000D5 RID: 213
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x040000D6 RID: 214
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x040000D7 RID: 215
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x040000D8 RID: 216
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040000D9 RID: 217 RVA: 0x001730C0 File Offset: 0x00171CC0
	internal static method _HUGE$initializer$;

	// Token: 0x040000DA RID: 218
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040000DB RID: 219
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x040000DC RID: 220
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x040000DD RID: 221 RVA: 0x001730C8 File Offset: 0x00171CC8
	internal static method HUGE$initializer$;

	// Token: 0x040000DE RID: 222
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040000DF RID: 223 RVA: 0x001730D0 File Offset: 0x00171CD0
	internal static method _HUGE$initializer$;

	// Token: 0x040000E0 RID: 224
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040000E1 RID: 225 RVA: 0x001730D8 File Offset: 0x00171CD8
	internal static method HUGE$initializer$;

	// Token: 0x040000E2 RID: 226
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040000E3 RID: 227 RVA: 0x00173118 File Offset: 0x00171D18
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040000E4 RID: 228
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040000E5 RID: 229 RVA: 0x00173108 File Offset: 0x00171D08
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040000E6 RID: 230
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040000E7 RID: 231 RVA: 0x00173128 File Offset: 0x00171D28
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040000E8 RID: 232
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040000E9 RID: 233 RVA: 0x00173100 File Offset: 0x00171D00
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040000EA RID: 234
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040000EB RID: 235 RVA: 0x001730E0 File Offset: 0x00171CE0
	internal static method _HUGE$initializer$;

	// Token: 0x040000EC RID: 236
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040000ED RID: 237 RVA: 0x00173110 File Offset: 0x00171D10
	internal static method _FNV_prime$initializer$;

	// Token: 0x040000EE RID: 238
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040000EF RID: 239 RVA: 0x001730F8 File Offset: 0x00171CF8
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040000F0 RID: 240
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040000F1 RID: 241
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040000F2 RID: 242
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040000F3 RID: 243
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040000F4 RID: 244 RVA: 0x001730F0 File Offset: 0x00171CF0
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040000F5 RID: 245 RVA: 0x00173130 File Offset: 0x00171D30
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040000F6 RID: 246 RVA: 0x001730E8 File Offset: 0x00171CE8
	internal static method HUGE$initializer$;

	// Token: 0x040000F7 RID: 247 RVA: 0x00173120 File Offset: 0x00171D20
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040000F8 RID: 248
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040000F9 RID: 249 RVA: 0x00173170 File Offset: 0x00171D70
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040000FA RID: 250
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040000FB RID: 251 RVA: 0x00173160 File Offset: 0x00171D60
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040000FC RID: 252
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040000FD RID: 253 RVA: 0x00173180 File Offset: 0x00171D80
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040000FE RID: 254
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040000FF RID: 255 RVA: 0x00173158 File Offset: 0x00171D58
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000100 RID: 256
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x04000101 RID: 257 RVA: 0x00173138 File Offset: 0x00171D38
	internal static method _HUGE$initializer$;

	// Token: 0x04000102 RID: 258
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000103 RID: 259 RVA: 0x00173168 File Offset: 0x00171D68
	internal static method _FNV_prime$initializer$;

	// Token: 0x04000104 RID: 260
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x04000105 RID: 261 RVA: 0x00173150 File Offset: 0x00171D50
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x04000106 RID: 262
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x04000107 RID: 263
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x04000108 RID: 264
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000109 RID: 265
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x0400010A RID: 266 RVA: 0x00173148 File Offset: 0x00171D48
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x0400010B RID: 267 RVA: 0x00173188 File Offset: 0x00171D88
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x0400010C RID: 268 RVA: 0x00173140 File Offset: 0x00171D40
	internal static method HUGE$initializer$;

	// Token: 0x0400010D RID: 269 RVA: 0x00173178 File Offset: 0x00171D78
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x0400010E RID: 270
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x0400010F RID: 271 RVA: 0x001731C8 File Offset: 0x00171DC8
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x04000110 RID: 272
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000111 RID: 273 RVA: 0x001731B8 File Offset: 0x00171DB8
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000112 RID: 274
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000113 RID: 275 RVA: 0x001731D8 File Offset: 0x00171DD8
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x04000114 RID: 276
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x04000115 RID: 277 RVA: 0x001731B0 File Offset: 0x00171DB0
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000116 RID: 278
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x04000117 RID: 279 RVA: 0x00173190 File Offset: 0x00171D90
	internal static method _HUGE$initializer$;

	// Token: 0x04000118 RID: 280
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000119 RID: 281 RVA: 0x001731C0 File Offset: 0x00171DC0
	internal static method _FNV_prime$initializer$;

	// Token: 0x0400011A RID: 282
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x0400011B RID: 283 RVA: 0x001731A8 File Offset: 0x00171DA8
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x0400011C RID: 284
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x0400011D RID: 285
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x0400011E RID: 286
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x0400011F RID: 287
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000120 RID: 288 RVA: 0x001731A0 File Offset: 0x00171DA0
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x04000121 RID: 289 RVA: 0x001731E0 File Offset: 0x00171DE0
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x04000122 RID: 290 RVA: 0x00173198 File Offset: 0x00171D98
	internal static method HUGE$initializer$;

	// Token: 0x04000123 RID: 291 RVA: 0x001731D0 File Offset: 0x00171DD0
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x04000124 RID: 292
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x04000125 RID: 293 RVA: 0x00173220 File Offset: 0x00171E20
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x04000126 RID: 294
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000127 RID: 295 RVA: 0x00173210 File Offset: 0x00171E10
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000128 RID: 296
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000129 RID: 297 RVA: 0x00173230 File Offset: 0x00171E30
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x0400012A RID: 298
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x0400012B RID: 299 RVA: 0x00173208 File Offset: 0x00171E08
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x0400012C RID: 300
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x0400012D RID: 301 RVA: 0x001731E8 File Offset: 0x00171DE8
	internal static method _HUGE$initializer$;

	// Token: 0x0400012E RID: 302
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x0400012F RID: 303 RVA: 0x00173218 File Offset: 0x00171E18
	internal static method _FNV_prime$initializer$;

	// Token: 0x04000130 RID: 304
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x04000131 RID: 305 RVA: 0x00173200 File Offset: 0x00171E00
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x04000132 RID: 306
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x04000133 RID: 307
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x04000134 RID: 308
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000135 RID: 309
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000136 RID: 310 RVA: 0x001731F8 File Offset: 0x00171DF8
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x04000137 RID: 311 RVA: 0x00173238 File Offset: 0x00171E38
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x04000138 RID: 312 RVA: 0x001731F0 File Offset: 0x00171DF0
	internal static method HUGE$initializer$;

	// Token: 0x04000139 RID: 313 RVA: 0x00173228 File Offset: 0x00171E28
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x0400013A RID: 314
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x0400013B RID: 315 RVA: 0x00173278 File Offset: 0x00171E78
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x0400013C RID: 316
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x0400013D RID: 317 RVA: 0x00173268 File Offset: 0x00171E68
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x0400013E RID: 318
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x0400013F RID: 319 RVA: 0x00173288 File Offset: 0x00171E88
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x04000140 RID: 320
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x04000141 RID: 321 RVA: 0x00173260 File Offset: 0x00171E60
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000142 RID: 322
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x04000143 RID: 323 RVA: 0x00173240 File Offset: 0x00171E40
	internal static method _HUGE$initializer$;

	// Token: 0x04000144 RID: 324
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000145 RID: 325 RVA: 0x00173270 File Offset: 0x00171E70
	internal static method _FNV_prime$initializer$;

	// Token: 0x04000146 RID: 326
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x04000147 RID: 327 RVA: 0x00173258 File Offset: 0x00171E58
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x04000148 RID: 328
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x04000149 RID: 329
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x0400014A RID: 330
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x0400014B RID: 331
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x0400014C RID: 332 RVA: 0x00173250 File Offset: 0x00171E50
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x0400014D RID: 333 RVA: 0x00173290 File Offset: 0x00171E90
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x0400014E RID: 334 RVA: 0x00173248 File Offset: 0x00171E48
	internal static method HUGE$initializer$;

	// Token: 0x0400014F RID: 335 RVA: 0x00173280 File Offset: 0x00171E80
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x04000150 RID: 336
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x04000151 RID: 337 RVA: 0x001732D0 File Offset: 0x00171ED0
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x04000152 RID: 338
	[FixedAddressValueType]
	internal static ulong ?_OptionsStorage@?1??__local_stdio_scanf_options@@YMPEA_KXZ@$$Q4_KA;

	// Token: 0x04000153 RID: 339
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000154 RID: 340 RVA: 0x001732C0 File Offset: 0x00171EC0
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000155 RID: 341
	[FixedAddressValueType]
	internal static ulong ?_OptionsStorage@?1??__local_stdio_printf_options@@YMPEA_KXZ@$$Q4_KA;

	// Token: 0x04000156 RID: 342
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000157 RID: 343 RVA: 0x001732E0 File Offset: 0x00171EE0
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x04000158 RID: 344
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x04000159 RID: 345 RVA: 0x001732B8 File Offset: 0x00171EB8
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x0400015A RID: 346
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x0400015B RID: 347 RVA: 0x00173298 File Offset: 0x00171E98
	internal static method _HUGE$initializer$;

	// Token: 0x0400015C RID: 348
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x0400015D RID: 349 RVA: 0x001732C8 File Offset: 0x00171EC8
	internal static method _FNV_prime$initializer$;

	// Token: 0x0400015E RID: 350
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x0400015F RID: 351 RVA: 0x001732B0 File Offset: 0x00171EB0
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x04000160 RID: 352
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x04000161 RID: 353
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x04000162 RID: 354
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000163 RID: 355
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000164 RID: 356 RVA: 0x001732A8 File Offset: 0x00171EA8
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x04000165 RID: 357 RVA: 0x001732E8 File Offset: 0x00171EE8
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x04000166 RID: 358 RVA: 0x001732A0 File Offset: 0x00171EA0
	internal static method HUGE$initializer$;

	// Token: 0x04000167 RID: 359 RVA: 0x001732D8 File Offset: 0x00171ED8
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x04000168 RID: 360
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x04000169 RID: 361 RVA: 0x00173328 File Offset: 0x00171F28
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x0400016A RID: 362
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x0400016B RID: 363 RVA: 0x00173318 File Offset: 0x00171F18
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x0400016C RID: 364
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x0400016D RID: 365 RVA: 0x00173338 File Offset: 0x00171F38
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x0400016E RID: 366
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x0400016F RID: 367 RVA: 0x00173310 File Offset: 0x00171F10
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000170 RID: 368
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x04000171 RID: 369 RVA: 0x001732F0 File Offset: 0x00171EF0
	internal static method _HUGE$initializer$;

	// Token: 0x04000172 RID: 370
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000173 RID: 371 RVA: 0x00173320 File Offset: 0x00171F20
	internal static method _FNV_prime$initializer$;

	// Token: 0x04000174 RID: 372
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x04000175 RID: 373 RVA: 0x00173308 File Offset: 0x00171F08
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x04000176 RID: 374
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x04000177 RID: 375
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x04000178 RID: 376
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000179 RID: 377
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x0400017A RID: 378 RVA: 0x00173300 File Offset: 0x00171F00
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x0400017B RID: 379 RVA: 0x00173340 File Offset: 0x00171F40
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x0400017C RID: 380 RVA: 0x001732F8 File Offset: 0x00171EF8
	internal static method HUGE$initializer$;

	// Token: 0x0400017D RID: 381 RVA: 0x00173330 File Offset: 0x00171F30
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x0400017E RID: 382
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x0400017F RID: 383 RVA: 0x00173380 File Offset: 0x00171F80
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x04000180 RID: 384
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000181 RID: 385 RVA: 0x00173370 File Offset: 0x00171F70
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000182 RID: 386
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000183 RID: 387 RVA: 0x00173390 File Offset: 0x00171F90
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x04000184 RID: 388
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x04000185 RID: 389 RVA: 0x00173368 File Offset: 0x00171F68
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000186 RID: 390
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x04000187 RID: 391 RVA: 0x00173348 File Offset: 0x00171F48
	internal static method _HUGE$initializer$;

	// Token: 0x04000188 RID: 392
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000189 RID: 393 RVA: 0x00173378 File Offset: 0x00171F78
	internal static method _FNV_prime$initializer$;

	// Token: 0x0400018A RID: 394
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x0400018B RID: 395 RVA: 0x00173360 File Offset: 0x00171F60
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x0400018C RID: 396
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x0400018D RID: 397
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x0400018E RID: 398
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x0400018F RID: 399
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000190 RID: 400 RVA: 0x00173358 File Offset: 0x00171F58
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x04000191 RID: 401 RVA: 0x00173398 File Offset: 0x00171F98
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x04000192 RID: 402 RVA: 0x00173350 File Offset: 0x00171F50
	internal static method HUGE$initializer$;

	// Token: 0x04000193 RID: 403 RVA: 0x00173388 File Offset: 0x00171F88
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x04000194 RID: 404
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x04000195 RID: 405 RVA: 0x001733D8 File Offset: 0x00171FD8
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x04000196 RID: 406
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000197 RID: 407 RVA: 0x001733C8 File Offset: 0x00171FC8
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000198 RID: 408
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000199 RID: 409 RVA: 0x001733E8 File Offset: 0x00171FE8
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x0400019A RID: 410
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x0400019B RID: 411 RVA: 0x001733C0 File Offset: 0x00171FC0
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x0400019C RID: 412
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x0400019D RID: 413 RVA: 0x001733A0 File Offset: 0x00171FA0
	internal static method _HUGE$initializer$;

	// Token: 0x0400019E RID: 414
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x0400019F RID: 415 RVA: 0x001733D0 File Offset: 0x00171FD0
	internal static method _FNV_prime$initializer$;

	// Token: 0x040001A0 RID: 416
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040001A1 RID: 417 RVA: 0x001733B8 File Offset: 0x00171FB8
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040001A2 RID: 418
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040001A3 RID: 419
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040001A4 RID: 420
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040001A5 RID: 421
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040001A6 RID: 422 RVA: 0x001733B0 File Offset: 0x00171FB0
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040001A7 RID: 423 RVA: 0x001733F0 File Offset: 0x00171FF0
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040001A8 RID: 424 RVA: 0x001733A8 File Offset: 0x00171FA8
	internal static method HUGE$initializer$;

	// Token: 0x040001A9 RID: 425 RVA: 0x001733E0 File Offset: 0x00171FE0
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040001AA RID: 426
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040001AB RID: 427 RVA: 0x00173438 File Offset: 0x00172038
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040001AC RID: 428
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040001AD RID: 429 RVA: 0x00173420 File Offset: 0x00172020
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040001AE RID: 430
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040001AF RID: 431 RVA: 0x00173448 File Offset: 0x00172048
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040001B0 RID: 432
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040001B1 RID: 433 RVA: 0x00173418 File Offset: 0x00172018
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040001B2 RID: 434
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040001B3 RID: 435 RVA: 0x001733F8 File Offset: 0x00171FF8
	internal static method _HUGE$initializer$;

	// Token: 0x040001B4 RID: 436
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040001B5 RID: 437 RVA: 0x00173428 File Offset: 0x00172028
	internal static method _FNV_prime$initializer$;

	// Token: 0x040001B6 RID: 438
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040001B7 RID: 439 RVA: 0x00173410 File Offset: 0x00172010
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040001B8 RID: 440
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040001B9 RID: 441
	[FixedAddressValueType]
	internal static int _Small_object_num_ptrs;

	// Token: 0x040001BA RID: 442
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040001BB RID: 443
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040001BC RID: 444
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040001BD RID: 445 RVA: 0x00173408 File Offset: 0x00172008
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040001BE RID: 446 RVA: 0x00173450 File Offset: 0x00172050
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040001BF RID: 447
	[FixedAddressValueType]
	internal static IntPtr __type_info_root_node;

	// Token: 0x040001C0 RID: 448 RVA: 0x00173430 File Offset: 0x00172030
	internal static method _Small_object_num_ptrs$initializer$;

	// Token: 0x040001C1 RID: 449 RVA: 0x00173400 File Offset: 0x00172000
	internal static method HUGE$initializer$;

	// Token: 0x040001C2 RID: 450 RVA: 0x00173440 File Offset: 0x00172040
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040001C3 RID: 451
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040001C4 RID: 452 RVA: 0x00173490 File Offset: 0x00172090
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040001C5 RID: 453
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040001C6 RID: 454 RVA: 0x00173480 File Offset: 0x00172080
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040001C7 RID: 455
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040001C8 RID: 456 RVA: 0x001734A0 File Offset: 0x001720A0
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040001C9 RID: 457
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040001CA RID: 458 RVA: 0x00173478 File Offset: 0x00172078
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040001CB RID: 459
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040001CC RID: 460 RVA: 0x00173458 File Offset: 0x00172058
	internal static method _HUGE$initializer$;

	// Token: 0x040001CD RID: 461
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040001CE RID: 462 RVA: 0x00173488 File Offset: 0x00172088
	internal static method _FNV_prime$initializer$;

	// Token: 0x040001CF RID: 463
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040001D0 RID: 464 RVA: 0x00173470 File Offset: 0x00172070
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040001D1 RID: 465
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040001D2 RID: 466
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040001D3 RID: 467
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040001D4 RID: 468
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040001D5 RID: 469 RVA: 0x00173468 File Offset: 0x00172068
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040001D6 RID: 470 RVA: 0x001734A8 File Offset: 0x001720A8
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040001D7 RID: 471 RVA: 0x00173460 File Offset: 0x00172060
	internal static method HUGE$initializer$;

	// Token: 0x040001D8 RID: 472 RVA: 0x00173498 File Offset: 0x00172098
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040001D9 RID: 473
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040001DA RID: 474 RVA: 0x001734E8 File Offset: 0x001720E8
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040001DB RID: 475
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040001DC RID: 476 RVA: 0x001734D8 File Offset: 0x001720D8
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040001DD RID: 477
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040001DE RID: 478 RVA: 0x001734F8 File Offset: 0x001720F8
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040001DF RID: 479
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040001E0 RID: 480 RVA: 0x001734D0 File Offset: 0x001720D0
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040001E1 RID: 481
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040001E2 RID: 482 RVA: 0x001734B0 File Offset: 0x001720B0
	internal static method _HUGE$initializer$;

	// Token: 0x040001E3 RID: 483
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040001E4 RID: 484 RVA: 0x001734E0 File Offset: 0x001720E0
	internal static method _FNV_prime$initializer$;

	// Token: 0x040001E5 RID: 485
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040001E6 RID: 486 RVA: 0x001734C8 File Offset: 0x001720C8
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040001E7 RID: 487
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040001E8 RID: 488
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040001E9 RID: 489
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040001EA RID: 490
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040001EB RID: 491 RVA: 0x001734C0 File Offset: 0x001720C0
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040001EC RID: 492 RVA: 0x00173500 File Offset: 0x00172100
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040001ED RID: 493 RVA: 0x001734B8 File Offset: 0x001720B8
	internal static method HUGE$initializer$;

	// Token: 0x040001EE RID: 494 RVA: 0x001734F0 File Offset: 0x001720F0
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040001EF RID: 495
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040001F0 RID: 496 RVA: 0x00173540 File Offset: 0x00172140
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040001F1 RID: 497
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040001F2 RID: 498 RVA: 0x00173530 File Offset: 0x00172130
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040001F3 RID: 499
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040001F4 RID: 500 RVA: 0x00173550 File Offset: 0x00172150
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040001F5 RID: 501
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040001F6 RID: 502 RVA: 0x00173528 File Offset: 0x00172128
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040001F7 RID: 503
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040001F8 RID: 504 RVA: 0x00173508 File Offset: 0x00172108
	internal static method _HUGE$initializer$;

	// Token: 0x040001F9 RID: 505
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040001FA RID: 506 RVA: 0x00173538 File Offset: 0x00172138
	internal static method _FNV_prime$initializer$;

	// Token: 0x040001FB RID: 507
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040001FC RID: 508 RVA: 0x00173520 File Offset: 0x00172120
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040001FD RID: 509
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040001FE RID: 510
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040001FF RID: 511
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000200 RID: 512
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000201 RID: 513 RVA: 0x00173518 File Offset: 0x00172118
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x04000202 RID: 514 RVA: 0x00173558 File Offset: 0x00172158
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x04000203 RID: 515 RVA: 0x00173510 File Offset: 0x00172110
	internal static method HUGE$initializer$;

	// Token: 0x04000204 RID: 516 RVA: 0x00173548 File Offset: 0x00172148
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x04000205 RID: 517
	[FixedAddressValueType]
	internal static ulong _Non_user_size;

	// Token: 0x04000206 RID: 518 RVA: 0x001735B0 File Offset: 0x001721B0
	internal static method _Non_user_size$initializer$;

	// Token: 0x04000207 RID: 519
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_move_constructible_v@PEBU_EXCEPTION_RECORD@@;

	// Token: 0x04000208 RID: 520 RVA: 0x00173790 File Offset: 0x00172390
	internal static method ?$is_nothrow_move_constructible_v@PEBU_EXCEPTION_RECORD@@$initializer$;

	// Token: 0x04000209 RID: 521
	[FixedAddressValueType]
	internal static ulong ?$_Size_after_ebco_v@U_Container_base0@std@@;

	// Token: 0x0400020A RID: 522 RVA: 0x00173628 File Offset: 0x00172228
	internal static method ?$_Size_after_ebco_v@U_Container_base0@std@@$initializer$;

	// Token: 0x0400020B RID: 523
	[FixedAddressValueType]
	internal static bool ?$is_empty_v@U_Container_base0@std@@;

	// Token: 0x0400020C RID: 524 RVA: 0x00173620 File Offset: 0x00172220
	internal static method ?$is_empty_v@U_Container_base0@std@@$initializer$;

	// Token: 0x0400020D RID: 525
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x0400020E RID: 526 RVA: 0x001737F8 File Offset: 0x001723F8
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x0400020F RID: 527
	[FixedAddressValueType]
	internal static bool ?$_Is_specialization_v@U?$char_traits@D@std@@Uchar_traits@2@;

	// Token: 0x04000210 RID: 528 RVA: 0x00173610 File Offset: 0x00172210
	internal static method ?$_Is_specialization_v@U?$char_traits@D@std@@Uchar_traits@2@$initializer$;

	// Token: 0x04000211 RID: 529
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x04000212 RID: 530 RVA: 0x00173588 File Offset: 0x00172188
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x04000213 RID: 531
	[FixedAddressValueType]
	internal static bool ?$_Is_simple_alloc_v@V?$allocator@_S@std@@;

	// Token: 0x04000214 RID: 532 RVA: 0x001736B8 File Offset: 0x001722B8
	internal static method ?$_Is_simple_alloc_v@V?$allocator@_S@std@@$initializer$;

	// Token: 0x04000215 RID: 533
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEA_W;

	// Token: 0x04000216 RID: 534 RVA: 0x00173688 File Offset: 0x00172288
	internal static method ?$is_trivial_v@PEA_W$initializer$;

	// Token: 0x04000217 RID: 535
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@_U;

	// Token: 0x04000218 RID: 536 RVA: 0x00173730 File Offset: 0x00172330
	internal static method ?$is_trivial_v@_U$initializer$;

	// Token: 0x04000219 RID: 537
	[FixedAddressValueType]
	internal static bool ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_U@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_U@std@@@std@@$$V@2@;

	// Token: 0x0400021A RID: 538 RVA: 0x00173770 File Offset: 0x00172370
	internal static method ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_U@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_U@std@@@std@@$$V@2@$initializer$;

	// Token: 0x0400021B RID: 539
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_move_constructible_v@PEAV_Ref_count_base@std@@;

	// Token: 0x0400021C RID: 540 RVA: 0x001737A0 File Offset: 0x001723A0
	internal static method ?$is_nothrow_move_constructible_v@PEAV_Ref_count_base@std@@$initializer$;

	// Token: 0x0400021D RID: 541
	[FixedAddressValueType]
	internal static ulong ?$_New_alignof@_W;

	// Token: 0x0400021E RID: 542 RVA: 0x00173910 File Offset: 0x00172510
	internal static method ?$_New_alignof@_W$initializer$;

	// Token: 0x0400021F RID: 543
	[FixedAddressValueType]
	internal static bool ?$is_standard_layout_v@_W;

	// Token: 0x04000220 RID: 544 RVA: 0x00173678 File Offset: 0x00172278
	internal static method ?$is_standard_layout_v@_W$initializer$;

	// Token: 0x04000221 RID: 545 RVA: 0x001738F8 File Offset: 0x001724F8
	internal static method allocator_arg$initializer$;

	// Token: 0x04000222 RID: 546
	[FixedAddressValueType]
	internal static bool ?$is_same_v@_J_J;

	// Token: 0x04000223 RID: 547 RVA: 0x001735D0 File Offset: 0x001721D0
	internal static method ?$is_same_v@_J_J$initializer$;

	// Token: 0x04000224 RID: 548
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x04000225 RID: 549 RVA: 0x00173808 File Offset: 0x00172408
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x04000226 RID: 550
	[FixedAddressValueType]
	internal static bool ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_S@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_S@std@@@std@@$$V@2@;

	// Token: 0x04000227 RID: 551 RVA: 0x00173778 File Offset: 0x00172378
	internal static method ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_S@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_S@std@@@std@@$$V@2@$initializer$;

	// Token: 0x04000228 RID: 552
	[FixedAddressValueType]
	internal static bool ?$is_array_v@PEAD;

	// Token: 0x04000229 RID: 553 RVA: 0x001737E8 File Offset: 0x001723E8
	internal static method ?$is_array_v@PEAD$initializer$;

	// Token: 0x0400022A RID: 554
	[FixedAddressValueType]
	internal static bool ?$is_final_v@V?$allocator@_U@std@@;

	// Token: 0x0400022B RID: 555 RVA: 0x00173758 File Offset: 0x00172358
	internal static method ?$is_final_v@V?$allocator@_U@std@@$initializer$;

	// Token: 0x0400022C RID: 556
	[FixedAddressValueType]
	internal static bool ?$is_same_v@_K_K;

	// Token: 0x0400022D RID: 557 RVA: 0x001735C8 File Offset: 0x001721C8
	internal static method ?$is_same_v@_K_K$initializer$;

	// Token: 0x0400022E RID: 558
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEA_SPEA_S;

	// Token: 0x0400022F RID: 559 RVA: 0x001736A8 File Offset: 0x001722A8
	internal static method ?$is_same_v@PEA_SPEA_S$initializer$;

	// Token: 0x04000230 RID: 560
	[FixedAddressValueType]
	internal static bool ?$is_empty_v@V?$allocator@_S@std@@;

	// Token: 0x04000231 RID: 561 RVA: 0x001736F0 File Offset: 0x001722F0
	internal static method ?$is_empty_v@V?$allocator@_S@std@@$initializer$;

	// Token: 0x04000232 RID: 562
	[FixedAddressValueType]
	internal static bool ?$is_array_v@D;

	// Token: 0x04000233 RID: 563 RVA: 0x001735F8 File Offset: 0x001721F8
	internal static method ?$is_array_v@D$initializer$;

	// Token: 0x04000234 RID: 564
	[FixedAddressValueType]
	internal static ulong ?$_New_alignof@_U;

	// Token: 0x04000235 RID: 565 RVA: 0x00173900 File Offset: 0x00172500
	internal static method ?$_New_alignof@_U$initializer$;

	// Token: 0x04000236 RID: 566
	[FixedAddressValueType]
	internal static bool ?$is_const_v@_W;

	// Token: 0x04000237 RID: 567 RVA: 0x00173640 File Offset: 0x00172240
	internal static method ?$is_const_v@_W$initializer$;

	// Token: 0x04000238 RID: 568
	[FixedAddressValueType]
	internal static bool ?$is_empty_v@V?$allocator@_W@std@@;

	// Token: 0x04000239 RID: 569 RVA: 0x00173690 File Offset: 0x00172290
	internal static method ?$is_empty_v@V?$allocator@_W@std@@$initializer$;

	// Token: 0x0400023A RID: 570
	[FixedAddressValueType]
	internal static bool ?$_Is_specialization_v@U?$char_traits@_W@std@@Uchar_traits@2@;

	// Token: 0x0400023B RID: 571 RVA: 0x00173680 File Offset: 0x00172280
	internal static method ?$_Is_specialization_v@U?$char_traits@_W@std@@Uchar_traits@2@$initializer$;

	// Token: 0x0400023C RID: 572
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEB_SPEB_S;

	// Token: 0x0400023D RID: 573 RVA: 0x001736B0 File Offset: 0x001722B0
	internal static method ?$is_same_v@PEB_SPEB_S$initializer$;

	// Token: 0x0400023E RID: 574
	[FixedAddressValueType]
	internal static bool ?$is_const_v@_S;

	// Token: 0x0400023F RID: 575 RVA: 0x001736A0 File Offset: 0x001722A0
	internal static method ?$is_const_v@_S$initializer$;

	// Token: 0x04000240 RID: 576
	[FixedAddressValueType]
	internal static bool ?$is_final_v@V?$allocator@_S@std@@;

	// Token: 0x04000241 RID: 577 RVA: 0x001736F8 File Offset: 0x001722F8
	internal static method ?$is_final_v@V?$allocator@_S@std@@$initializer$;

	// Token: 0x04000242 RID: 578
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_constructible_v@PEA_WAEBQEA_W;

	// Token: 0x04000243 RID: 579 RVA: 0x001737E0 File Offset: 0x001723E0
	internal static method ?$is_nothrow_constructible_v@PEA_WAEBQEA_W$initializer$;

	// Token: 0x04000244 RID: 580
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x04000245 RID: 581 RVA: 0x00173580 File Offset: 0x00172180
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x04000246 RID: 582
	[FixedAddressValueType]
	internal static bool ?$is_empty_v@V?$allocator@_U@std@@;

	// Token: 0x04000247 RID: 583 RVA: 0x00173750 File Offset: 0x00172350
	internal static method ?$is_empty_v@V?$allocator@_U@std@@$initializer$;

	// Token: 0x04000248 RID: 584
	[FixedAddressValueType]
	internal static bool ?$is_final_v@V?$allocator@D@std@@;

	// Token: 0x04000249 RID: 585 RVA: 0x00173638 File Offset: 0x00172238
	internal static method ?$is_final_v@V?$allocator@D@std@@$initializer$;

	// Token: 0x0400024A RID: 586
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x0400024B RID: 587 RVA: 0x00173560 File Offset: 0x00172160
	internal static method _HUGE$initializer$;

	// Token: 0x0400024C RID: 588
	[FixedAddressValueType]
	internal static bool ?$is_empty_v@V?$allocator@D@std@@;

	// Token: 0x0400024D RID: 589 RVA: 0x00173630 File Offset: 0x00172230
	internal static method ?$is_empty_v@V?$allocator@D@std@@$initializer$;

	// Token: 0x0400024E RID: 590
	[FixedAddressValueType]
	internal static ulong _Big_allocation_threshold;

	// Token: 0x0400024F RID: 591 RVA: 0x001735A0 File Offset: 0x001721A0
	internal static method _Big_allocation_threshold$initializer$;

	// Token: 0x04000250 RID: 592
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEA_UPEA_U;

	// Token: 0x04000251 RID: 593 RVA: 0x00173708 File Offset: 0x00172308
	internal static method ?$is_same_v@PEA_UPEA_U$initializer$;

	// Token: 0x04000252 RID: 594
	[FixedAddressValueType]
	internal static bool ?$is_array_v@PEA_W;

	// Token: 0x04000253 RID: 595 RVA: 0x001737D8 File Offset: 0x001723D8
	internal static method ?$is_array_v@PEA_W$initializer$;

	// Token: 0x04000254 RID: 596
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@_S;

	// Token: 0x04000255 RID: 597 RVA: 0x001736D0 File Offset: 0x001722D0
	internal static method ?$is_trivial_v@_S$initializer$;

	// Token: 0x04000256 RID: 598
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_constructible_v@PEA_SAEBQEA_S;

	// Token: 0x04000257 RID: 599 RVA: 0x001737D0 File Offset: 0x001723D0
	internal static method ?$is_nothrow_constructible_v@PEA_SAEBQEA_S$initializer$;

	// Token: 0x04000258 RID: 600
	[FixedAddressValueType]
	internal static bool ?$is_const_v@_U;

	// Token: 0x04000259 RID: 601 RVA: 0x00173700 File Offset: 0x00172300
	internal static method ?$is_const_v@_U$initializer$;

	// Token: 0x0400025A RID: 602
	[FixedAddressValueType]
	internal static bool ?$is_same_v@_W_W;

	// Token: 0x0400025B RID: 603 RVA: 0x00173660 File Offset: 0x00172260
	internal static method ?$is_same_v@_W_W$initializer$;

	// Token: 0x0400025C RID: 604
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEBDPEBD;

	// Token: 0x0400025D RID: 605 RVA: 0x001735E0 File Offset: 0x001721E0
	internal static method ?$is_same_v@PEBDPEBD$initializer$;

	// Token: 0x0400025E RID: 606
	[FixedAddressValueType]
	internal static bool ?$is_const_v@D;

	// Token: 0x0400025F RID: 607 RVA: 0x001735C0 File Offset: 0x001721C0
	internal static method ?$is_const_v@D$initializer$;

	// Token: 0x04000260 RID: 608
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x04000261 RID: 609 RVA: 0x00173590 File Offset: 0x00172190
	internal static method _FNV_prime$initializer$;

	// Token: 0x04000262 RID: 610
	[FixedAddressValueType]
	internal static bool ?$is_array_v@_S;

	// Token: 0x04000263 RID: 611 RVA: 0x001736C8 File Offset: 0x001722C8
	internal static method ?$is_array_v@_S$initializer$;

	// Token: 0x04000264 RID: 612
	[FixedAddressValueType]
	internal static bool ?$disjunction_v@U?$is_array@$$CBU_EXCEPTION_RECORD@@@std@@U?$is_void@$$CBU_EXCEPTION_RECORD@@@2@;

	// Token: 0x04000265 RID: 613 RVA: 0x00173768 File Offset: 0x00172368
	internal static method ?$disjunction_v@U?$is_array@$$CBU_EXCEPTION_RECORD@@@std@@U?$is_void@$$CBU_EXCEPTION_RECORD@@@2@$initializer$;

	// Token: 0x04000266 RID: 614
	[FixedAddressValueType]
	internal static bool ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@D@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@D@std@@@std@@$$V@2@;

	// Token: 0x04000267 RID: 615 RVA: 0x00173788 File Offset: 0x00172388
	internal static method ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@D@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@D@std@@@std@@$$V@2@$initializer$;

	// Token: 0x04000268 RID: 616
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x04000269 RID: 617 RVA: 0x00173578 File Offset: 0x00172178
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x0400026A RID: 618
	[FixedAddressValueType]
	internal static bool ?$is_standard_layout_v@_S;

	// Token: 0x0400026B RID: 619 RVA: 0x001736D8 File Offset: 0x001722D8
	internal static method ?$is_standard_layout_v@_S$initializer$;

	// Token: 0x0400026C RID: 620
	[FixedAddressValueType]
	internal static ulong _Big_allocation_sentinel;

	// Token: 0x0400026D RID: 621 RVA: 0x001735B8 File Offset: 0x001721B8
	internal static method _Big_allocation_sentinel$initializer$;

	// Token: 0x0400026E RID: 622
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEADPEAD;

	// Token: 0x0400026F RID: 623 RVA: 0x001735D8 File Offset: 0x001721D8
	internal static method ?$is_same_v@PEADPEAD$initializer$;

	// Token: 0x04000270 RID: 624
	[FixedAddressValueType]
	internal static bool ?$_Is_simple_alloc_v@V?$allocator@_W@std@@;

	// Token: 0x04000271 RID: 625 RVA: 0x00173658 File Offset: 0x00172258
	internal static method ?$_Is_simple_alloc_v@V?$allocator@_W@std@@$initializer$;

	// Token: 0x04000272 RID: 626
	[FixedAddressValueType]
	internal static bool ?$is_pointer_v@PEAPEAD;

	// Token: 0x04000273 RID: 627 RVA: 0x00173830 File Offset: 0x00172430
	internal static method ?$is_pointer_v@PEAPEAD$initializer$;

	// Token: 0x04000274 RID: 628
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@D;

	// Token: 0x04000275 RID: 629 RVA: 0x00173600 File Offset: 0x00172200
	internal static method ?$is_trivial_v@D$initializer$;

	// Token: 0x04000276 RID: 630
	[FixedAddressValueType]
	internal static bool ?$is_pointer_v@PEAPEA_W;

	// Token: 0x04000277 RID: 631 RVA: 0x00173828 File Offset: 0x00172428
	internal static method ?$is_pointer_v@PEAPEA_W$initializer$;

	// Token: 0x04000278 RID: 632 RVA: 0x001738F0 File Offset: 0x001724F0
	internal static method piecewise_construct$initializer$;

	// Token: 0x04000279 RID: 633
	[FixedAddressValueType]
	internal static bool ?$is_array_v@_W;

	// Token: 0x0400027A RID: 634 RVA: 0x00173668 File Offset: 0x00172268
	internal static method ?$is_array_v@_W$initializer$;

	// Token: 0x0400027B RID: 635
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x0400027C RID: 636
	[FixedAddressValueType]
	internal static bool ?$is_array_v@PEA_U;

	// Token: 0x0400027D RID: 637 RVA: 0x001737B8 File Offset: 0x001723B8
	internal static method ?$is_array_v@PEA_U$initializer$;

	// Token: 0x0400027E RID: 638
	[FixedAddressValueType]
	internal static bool ?$is_final_v@V?$allocator@_W@std@@;

	// Token: 0x0400027F RID: 639 RVA: 0x00173698 File Offset: 0x00172298
	internal static method ?$is_final_v@V?$allocator@_W@std@@$initializer$;

	// Token: 0x04000280 RID: 640
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_move_assignable_v@PEBU_EXCEPTION_RECORD@@;

	// Token: 0x04000281 RID: 641
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_move_assignable_v@PEAV_Ref_count_base@std@@;

	// Token: 0x04000282 RID: 642
	[FixedAddressValueType]
	internal static int _Small_object_num_ptrs;

	// Token: 0x04000283 RID: 643
	[FixedAddressValueType]
	internal static bool ?$_Is_simple_alloc_v@V?$allocator@D@std@@;

	// Token: 0x04000284 RID: 644
	[FixedAddressValueType]
	internal static bool ?$_Is_simple_alloc_v@V?$allocator@_U@std@@;

	// Token: 0x04000285 RID: 645
	[FixedAddressValueType]
	internal static bool ?$is_standard_layout_v@_U;

	// Token: 0x04000286 RID: 646
	[FixedAddressValueType]
	internal static bool ?$is_standard_layout_v@D;

	// Token: 0x04000287 RID: 647
	[FixedAddressValueType]
	internal static bool ?$is_pointer_v@PEAPEA_U;

	// Token: 0x04000288 RID: 648
	[FixedAddressValueType]
	internal static bool ?$is_pointer_v@PEAPEA_S;

	// Token: 0x04000289 RID: 649
	[FixedAddressValueType]
	internal static bool ?$is_integral_v@$$CBJ;

	// Token: 0x0400028A RID: 650
	[FixedAddressValueType]
	internal static bool ?$_Is_specialization_v@U?$char_traits@_S@std@@Uchar_traits@2@;

	// Token: 0x0400028B RID: 651
	[FixedAddressValueType]
	internal static bool ?$_Is_specialization_v@U?$char_traits@_U@std@@Uchar_traits@2@;

	// Token: 0x0400028C RID: 652
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x0400028D RID: 653
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@_W;

	// Token: 0x0400028E RID: 654
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAD;

	// Token: 0x0400028F RID: 655
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEA_S;

	// Token: 0x04000290 RID: 656
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEA_U;

	// Token: 0x04000291 RID: 657
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x04000292 RID: 658
	[FixedAddressValueType]
	internal static ulong _Big_allocation_alignment;

	// Token: 0x04000293 RID: 659
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x04000294 RID: 660
	[FixedAddressValueType]
	internal static bool ?$is_same_v@DD;

	// Token: 0x04000295 RID: 661
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEA_WPEA_W;

	// Token: 0x04000296 RID: 662
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEB_WPEB_W;

	// Token: 0x04000297 RID: 663
	[FixedAddressValueType]
	internal static bool ?$is_same_v@_S_S;

	// Token: 0x04000298 RID: 664
	[FixedAddressValueType]
	internal static bool ?$is_same_v@PEB_UPEB_U;

	// Token: 0x04000299 RID: 665
	[FixedAddressValueType]
	internal static bool ?$is_same_v@_U_U;

	// Token: 0x0400029A RID: 666
	[FixedAddressValueType]
	internal static bool ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_W@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_W@std@@@std@@$$V@2@;

	// Token: 0x0400029B RID: 667
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_constructible_v@PEA_UAEBQEA_U;

	// Token: 0x0400029C RID: 668
	[FixedAddressValueType]
	internal static bool ?$is_nothrow_constructible_v@PEADAEBQEAD;

	// Token: 0x0400029D RID: 669
	[FixedAddressValueType]
	internal static ulong ?$_New_alignof@D;

	// Token: 0x0400029E RID: 670
	[FixedAddressValueType]
	internal static ulong ?$_New_alignof@_S;

	// Token: 0x0400029F RID: 671
	[FixedAddressValueType]
	internal static bool ?$is_array_v@_U;

	// Token: 0x040002A0 RID: 672
	[FixedAddressValueType]
	internal static bool ?$is_array_v@PEA_S;

	// Token: 0x040002A1 RID: 673
	[FixedAddressValueType]
	internal static bool ?$is_array_v@$$CBU_EXCEPTION_RECORD@@;

	// Token: 0x040002A2 RID: 674 RVA: 0x001737B0 File Offset: 0x001723B0
	internal static method ?$is_integral_v@$$CBJ$initializer$;

	// Token: 0x040002A3 RID: 675 RVA: 0x00173820 File Offset: 0x00172420
	internal static method ?$is_pointer_v@PEAPEA_S$initializer$;

	// Token: 0x040002A4 RID: 676 RVA: 0x00173780 File Offset: 0x00172380
	internal static method ?$conjunction_v@U?$is_nothrow_default_constructible@V?$allocator@_W@std@@@std@@U?$is_nothrow_constructible@V?$_String_val@U?$_Simple_types@_W@std@@@std@@$$V@2@$initializer$;

	// Token: 0x040002A5 RID: 677 RVA: 0x00173748 File Offset: 0x00172348
	internal static method ?$is_trivial_v@PEA_U$initializer$;

	// Token: 0x040002A6 RID: 678 RVA: 0x00173718 File Offset: 0x00172318
	internal static method ?$_Is_simple_alloc_v@V?$allocator@_U@std@@$initializer$;

	// Token: 0x040002A7 RID: 679 RVA: 0x00173738 File Offset: 0x00172338
	internal static method ?$is_standard_layout_v@_U$initializer$;

	// Token: 0x040002A8 RID: 680 RVA: 0x00173720 File Offset: 0x00172320
	internal static method ?$is_same_v@_U_U$initializer$;

	// Token: 0x040002A9 RID: 681 RVA: 0x001736C0 File Offset: 0x001722C0
	internal static method ?$is_same_v@_S_S$initializer$;

	// Token: 0x040002AA RID: 682 RVA: 0x00173710 File Offset: 0x00172310
	internal static method ?$is_same_v@PEB_UPEB_U$initializer$;

	// Token: 0x040002AB RID: 683 RVA: 0x00173818 File Offset: 0x00172418
	internal static method ?$is_pointer_v@PEAPEA_U$initializer$;

	// Token: 0x040002AC RID: 684 RVA: 0x001737C0 File Offset: 0x001723C0
	internal static method ?$is_nothrow_constructible_v@PEA_UAEBQEA_U$initializer$;

	// Token: 0x040002AD RID: 685 RVA: 0x00173570 File Offset: 0x00172170
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040002AE RID: 686 RVA: 0x00173908 File Offset: 0x00172508
	internal static method ?$_New_alignof@_S$initializer$;

	// Token: 0x040002AF RID: 687 RVA: 0x001737A8 File Offset: 0x001723A8
	internal static method ?$is_nothrow_move_assignable_v@PEAV_Ref_count_base@std@@$initializer$;

	// Token: 0x040002B0 RID: 688 RVA: 0x00173760 File Offset: 0x00172360
	internal static method ?$is_array_v@$$CBU_EXCEPTION_RECORD@@$initializer$;

	// Token: 0x040002B1 RID: 689 RVA: 0x001736E0 File Offset: 0x001722E0
	internal static method ?$_Is_specialization_v@U?$char_traits@_S@std@@Uchar_traits@2@$initializer$;

	// Token: 0x040002B2 RID: 690 RVA: 0x00173618 File Offset: 0x00172218
	internal static method ?$is_trivial_v@PEAD$initializer$;

	// Token: 0x040002B3 RID: 691 RVA: 0x001737F0 File Offset: 0x001723F0
	internal static method ?$is_nothrow_constructible_v@PEADAEBQEAD$initializer$;

	// Token: 0x040002B4 RID: 692 RVA: 0x00173810 File Offset: 0x00172410
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040002B5 RID: 693 RVA: 0x00173608 File Offset: 0x00172208
	internal static method ?$is_standard_layout_v@D$initializer$;

	// Token: 0x040002B6 RID: 694 RVA: 0x00173740 File Offset: 0x00172340
	internal static method ?$_Is_specialization_v@U?$char_traits@_U@std@@Uchar_traits@2@$initializer$;

	// Token: 0x040002B7 RID: 695 RVA: 0x00173650 File Offset: 0x00172250
	internal static method ?$is_same_v@PEB_WPEB_W$initializer$;

	// Token: 0x040002B8 RID: 696 RVA: 0x001735F0 File Offset: 0x001721F0
	internal static method ?$is_same_v@DD$initializer$;

	// Token: 0x040002B9 RID: 697 RVA: 0x00173728 File Offset: 0x00172328
	internal static method ?$is_array_v@_U$initializer$;

	// Token: 0x040002BA RID: 698 RVA: 0x00173648 File Offset: 0x00172248
	internal static method ?$is_same_v@PEA_WPEA_W$initializer$;

	// Token: 0x040002BB RID: 699 RVA: 0x001735E8 File Offset: 0x001721E8
	internal static method ?$_Is_simple_alloc_v@V?$allocator@D@std@@$initializer$;

	// Token: 0x040002BC RID: 700 RVA: 0x00173598 File Offset: 0x00172198
	internal static method _Small_object_num_ptrs$initializer$;

	// Token: 0x040002BD RID: 701 RVA: 0x00173568 File Offset: 0x00172168
	internal static method HUGE$initializer$;

	// Token: 0x040002BE RID: 702 RVA: 0x00173670 File Offset: 0x00172270
	internal static method ?$is_trivial_v@_W$initializer$;

	// Token: 0x040002BF RID: 703 RVA: 0x00173918 File Offset: 0x00172518
	internal static method ?$_New_alignof@D$initializer$;

	// Token: 0x040002C0 RID: 704 RVA: 0x00173800 File Offset: 0x00172400
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040002C1 RID: 705 RVA: 0x001736E8 File Offset: 0x001722E8
	internal static method ?$is_trivial_v@PEA_S$initializer$;

	// Token: 0x040002C2 RID: 706 RVA: 0x001737C8 File Offset: 0x001723C8
	internal static method ?$is_array_v@PEA_S$initializer$;

	// Token: 0x040002C3 RID: 707 RVA: 0x001735A8 File Offset: 0x001721A8
	internal static method _Big_allocation_alignment$initializer$;

	// Token: 0x040002C4 RID: 708 RVA: 0x00173798 File Offset: 0x00172398
	internal static method ?$is_nothrow_move_assignable_v@PEBU_EXCEPTION_RECORD@@$initializer$;

	// Token: 0x040002C5 RID: 709
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040002C6 RID: 710 RVA: 0x00173870 File Offset: 0x00172470
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040002C7 RID: 711
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040002C8 RID: 712 RVA: 0x00173860 File Offset: 0x00172460
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040002C9 RID: 713
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040002CA RID: 714 RVA: 0x00173880 File Offset: 0x00172480
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040002CB RID: 715
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040002CC RID: 716 RVA: 0x00173858 File Offset: 0x00172458
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040002CD RID: 717
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040002CE RID: 718 RVA: 0x00173838 File Offset: 0x00172438
	internal static method _HUGE$initializer$;

	// Token: 0x040002CF RID: 719
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040002D0 RID: 720 RVA: 0x00173868 File Offset: 0x00172468
	internal static method _FNV_prime$initializer$;

	// Token: 0x040002D1 RID: 721
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040002D2 RID: 722 RVA: 0x00173850 File Offset: 0x00172450
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040002D3 RID: 723
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040002D4 RID: 724
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040002D5 RID: 725
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040002D6 RID: 726
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040002D7 RID: 727 RVA: 0x00173848 File Offset: 0x00172448
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040002D8 RID: 728 RVA: 0x00173888 File Offset: 0x00172488
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040002D9 RID: 729 RVA: 0x00173840 File Offset: 0x00172440
	internal static method HUGE$initializer$;

	// Token: 0x040002DA RID: 730 RVA: 0x00173878 File Offset: 0x00172478
	internal static method ?$is_trivial_v@N$initializer$;

	// Token: 0x040002DB RID: 731
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@M;

	// Token: 0x040002DC RID: 732 RVA: 0x001738C8 File Offset: 0x001724C8
	internal static method ?$is_trivial_v@M$initializer$;

	// Token: 0x040002DD RID: 733
	[FixedAddressValueType]
	internal static ulong _FNV_offset_basis;

	// Token: 0x040002DE RID: 734 RVA: 0x001738B8 File Offset: 0x001724B8
	internal static method _FNV_offset_basis$initializer$;

	// Token: 0x040002DF RID: 735
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@O;

	// Token: 0x040002E0 RID: 736 RVA: 0x001738D8 File Offset: 0x001724D8
	internal static method ?$is_trivial_v@O$initializer$;

	// Token: 0x040002E1 RID: 737
	[FixedAddressValueType]
	internal static bool ?$is_same_v@$$QEAH$$QEAH;

	// Token: 0x040002E2 RID: 738 RVA: 0x001738B0 File Offset: 0x001724B0
	internal static method ?$is_same_v@$$QEAH$$QEAH$initializer$;

	// Token: 0x040002E3 RID: 739
	[FixedAddressValueType]
	internal static double _HUGE;

	// Token: 0x040002E4 RID: 740 RVA: 0x00173890 File Offset: 0x00172490
	internal static method _HUGE$initializer$;

	// Token: 0x040002E5 RID: 741
	[FixedAddressValueType]
	internal static ulong _FNV_prime;

	// Token: 0x040002E6 RID: 742 RVA: 0x001738C0 File Offset: 0x001724C0
	internal static method _FNV_prime$initializer$;

	// Token: 0x040002E7 RID: 743
	[FixedAddressValueType]
	internal static bool ?$is_same_v@AEAH$$QEAH;

	// Token: 0x040002E8 RID: 744 RVA: 0x001738A8 File Offset: 0x001724A8
	internal static method ?$is_same_v@AEAH$$QEAH$initializer$;

	// Token: 0x040002E9 RID: 745
	[FixedAddressValueType]
	internal static double HUGE;

	// Token: 0x040002EA RID: 746
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@N;

	// Token: 0x040002EB RID: 747
	[FixedAddressValueType]
	internal static bool ?$is_trivial_v@PEAX;

	// Token: 0x040002EC RID: 748
	[FixedAddressValueType]
	internal static bool ?$is_same_v@H$$QEAH;

	// Token: 0x040002ED RID: 749 RVA: 0x001738A0 File Offset: 0x001724A0
	internal static method ?$is_same_v@H$$QEAH$initializer$;

	// Token: 0x040002EE RID: 750 RVA: 0x001738E0 File Offset: 0x001724E0
	internal static method ?$is_trivial_v@PEAX$initializer$;

	// Token: 0x040002EF RID: 751 RVA: 0x00173898 File Offset: 0x00172498
	internal static method HUGE$initializer$;

	// Token: 0x040002F0 RID: 752 RVA: 0x001738D0 File Offset: 0x001724D0
	internal static method ?$is_trivial_v@N$initializer$;
}
