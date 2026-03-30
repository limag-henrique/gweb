using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.TextFormatting;

namespace MS.Internal
{
	// Token: 0x02000683 RID: 1667
	internal static class Classification
	{
		// Token: 0x06004972 RID: 18802
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern void MILGetClassificationTables(out Classification.RawClassificationTables ct);

		// Token: 0x06004973 RID: 18803 RVA: 0x0011E83C File Offset: 0x0011DC3C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		static Classification()
		{
			Classification.RawClassificationTables rawClassificationTables = default(Classification.RawClassificationTables);
			Classification.MILGetClassificationTables(out rawClassificationTables);
			Classification._unicodeClassTable = new SecurityCriticalData<IntPtr>(rawClassificationTables.UnicodeClasses);
			Classification._charAttributeTable = new SecurityCriticalData<IntPtr>(rawClassificationTables.CharacterAttributes);
			Classification._mirroredCharTable = new SecurityCriticalData<IntPtr>(rawClassificationTables.Mirroring);
			Classification._combiningMarksClassification = new SecurityCriticalData<Classification.CombiningMarksClassificationData>(rawClassificationTables.CombiningMarksClassification);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0011E898 File Offset: 0x0011DC98
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe static short GetUnicodeClassUTF16(char codepoint)
		{
			short** ptr = *(IntPtr*)Classification.UnicodeClassTable;
			Invariant.Assert(ptr >= 472L);
			short* ptr2 = *(IntPtr*)(ptr + (IntPtr)(codepoint >> 8) * (IntPtr)sizeof(short*) / (IntPtr)sizeof(short*));
			if (ptr2 >= 472L)
			{
				return ptr2[(IntPtr)(codepoint & 'ÿ')];
			}
			return ptr2;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0011E8E8 File Offset: 0x0011DCE8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe static short GetUnicodeClass(int unicodeScalar)
		{
			Invariant.Assert(unicodeScalar >= 0 && unicodeScalar <= 1114111);
			short** ptr = *(IntPtr*)(Classification.UnicodeClassTable + (IntPtr)((unicodeScalar >> 16 & 255) % 17) * (IntPtr)sizeof(short**) / (IntPtr)sizeof(short**));
			if (ptr < 472L)
			{
				return ptr;
			}
			short* ptr2 = *(IntPtr*)(ptr + (IntPtr)((unicodeScalar & 65535) >> 8) * (IntPtr)sizeof(short*) / (IntPtr)sizeof(short*));
			if (ptr2 < 472L)
			{
				return ptr2;
			}
			return ptr2[unicodeScalar & 255];
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x0011E968 File Offset: 0x0011DD68
		internal static int UnicodeScalar(CharacterBufferRange unicodeString, out int sizeofChar)
		{
			Invariant.Assert(unicodeString.CharacterBuffer != null && unicodeString.Length > 0);
			int num = (int)unicodeString[0];
			sizeofChar = 1;
			if (unicodeString.Length >= 2 && (num & 64512) == 55296 && (unicodeString[1] & 'ﰀ') == '\udc00')
			{
				num = ((num & 1023) << 10 | (int)(unicodeString[1] & 'Ͽ')) + 65536;
				sizeofChar++;
			}
			return num;
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x0011E9F0 File Offset: 0x0011DDF0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe static bool IsCombining(int unicodeScalar)
		{
			byte itemClass = Classification.CharAttributeTable[Classification.GetUnicodeClass(unicodeScalar)].ItemClass;
			return itemClass == 7 || itemClass == 8 || Classification.IsIVS(unicodeScalar);
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x0011EA28 File Offset: 0x0011DE28
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe static bool IsJoiner(int unicodeScalar)
		{
			byte itemClass = Classification.CharAttributeTable[Classification.GetUnicodeClass(unicodeScalar)].ItemClass;
			return itemClass == 10;
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0011EA54 File Offset: 0x0011DE54
		public static bool IsIVS(int unicodeScalar)
		{
			return unicodeScalar >= 917760 && unicodeScalar <= 917999;
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0011EA78 File Offset: 0x0011DE78
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe static int AdvanceUntilUTF16(CharacterBuffer charBuffer, int offsetToFirstChar, int stringLength, ushort mask, out ushort charFlags)
		{
			int i = offsetToFirstChar;
			int num = offsetToFirstChar + stringLength;
			charFlags = 0;
			while (i < num)
			{
				ushort flags = Classification.CharAttributeTable[Classification.GetUnicodeClassUTF16(charBuffer[i])].Flags;
				if ((flags & mask) != 0)
				{
					break;
				}
				charFlags |= flags;
				i++;
			}
			return i - offsetToFirstChar;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x0011EACC File Offset: 0x0011DECC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe static int AdvanceWhile(CharacterBufferRange unicodeString, ItemClass itemClass)
		{
			int i = 0;
			int length = unicodeString.Length;
			int num = 0;
			while (i < length)
			{
				int unicodeScalar = Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, i, length - i), out num);
				byte itemClass2 = Classification.CharAttributeTable[Classification.GetUnicodeClass(unicodeScalar)].ItemClass;
				if (itemClass2 != (byte)itemClass)
				{
					break;
				}
				i += num;
			}
			return i;
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x0011EB24 File Offset: 0x0011DF24
		private unsafe static short*** UnicodeClassTable
		{
			[SecurityCritical]
			get
			{
				return (short***)((void*)Classification._unicodeClassTable.Value);
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x0011EB44 File Offset: 0x0011DF44
		private unsafe static CharacterAttribute* CharAttributeTable
		{
			[SecurityCritical]
			get
			{
				return (CharacterAttribute*)((void*)Classification._charAttributeTable.Value);
			}
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x0011EB64 File Offset: 0x0011DF64
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe static CharacterAttribute CharAttributeOf(int charClass)
		{
			Invariant.Assert(charClass >= 0 && charClass < 472);
			return Classification.CharAttributeTable[charClass];
		}

		// Token: 0x04001CDF RID: 7391
		private static readonly SecurityCriticalData<IntPtr> _unicodeClassTable;

		// Token: 0x04001CE0 RID: 7392
		private static readonly SecurityCriticalData<IntPtr> _charAttributeTable;

		// Token: 0x04001CE1 RID: 7393
		private static readonly SecurityCriticalData<IntPtr> _mirroredCharTable;

		// Token: 0x04001CE2 RID: 7394
		private static readonly SecurityCriticalData<Classification.CombiningMarksClassificationData> _combiningMarksClassification;

		// Token: 0x020009A8 RID: 2472
		internal struct CombiningMarksClassificationData
		{
			// Token: 0x04002D8E RID: 11662
			internal IntPtr CombiningCharsIndexes;

			// Token: 0x04002D8F RID: 11663
			internal int CombiningCharsIndexesTableLength;

			// Token: 0x04002D90 RID: 11664
			internal int CombiningCharsIndexesTableSegmentLength;

			// Token: 0x04002D91 RID: 11665
			internal IntPtr CombiningMarkIndexes;

			// Token: 0x04002D92 RID: 11666
			internal int CombiningMarkIndexesTableLength;

			// Token: 0x04002D93 RID: 11667
			internal IntPtr CombinationChars;

			// Token: 0x04002D94 RID: 11668
			internal int CombinationCharsBaseCount;

			// Token: 0x04002D95 RID: 11669
			internal int CombinationCharsMarkCount;
		}

		// Token: 0x020009A9 RID: 2473
		internal struct RawClassificationTables
		{
			// Token: 0x04002D96 RID: 11670
			internal IntPtr UnicodeClasses;

			// Token: 0x04002D97 RID: 11671
			internal IntPtr CharacterAttributes;

			// Token: 0x04002D98 RID: 11672
			internal IntPtr Mirroring;

			// Token: 0x04002D99 RID: 11673
			internal Classification.CombiningMarksClassificationData CombiningMarksClassification;
		}
	}
}
