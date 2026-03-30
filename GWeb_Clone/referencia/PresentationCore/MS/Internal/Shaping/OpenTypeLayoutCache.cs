using System;
using System.Collections;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D3 RID: 1747
	internal static class OpenTypeLayoutCache
	{
		// Token: 0x06004BA1 RID: 19361 RVA: 0x001277C4 File Offset: 0x00126BC4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public static void InitCache(IOpenTypeFont font, OpenTypeTags tableTag, GlyphInfoList glyphInfo, OpenTypeLayoutWorkspace workspace)
		{
			byte[] tableCache = font.GetTableCache(tableTag);
			if (tableCache == null)
			{
				workspace.TableCacheData = null;
				return;
			}
			workspace.TableCacheData = tableCache;
			workspace.AllocateCachePointers(glyphInfo.Length);
			OpenTypeLayoutCache.RenewPointers(glyphInfo, workspace, 0, glyphInfo.Length);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00127808 File Offset: 0x00126C08
		[SecurityCritical]
		public static void OnGlyphsChanged(OpenTypeLayoutWorkspace workspace, GlyphInfoList glyphInfo, int oldLength, int firstGlyphChanged, int afterLastGlyphChanged)
		{
			if (workspace.TableCacheData == null)
			{
				return;
			}
			workspace.UpdateCachePointers(oldLength, glyphInfo.Length, firstGlyphChanged, afterLastGlyphChanged);
			OpenTypeLayoutCache.RenewPointers(glyphInfo, workspace, firstGlyphChanged, afterLastGlyphChanged);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00127838 File Offset: 0x00126C38
		[SecurityCritical]
		private unsafe static ushort GetCacheLookupCount(OpenTypeLayoutWorkspace workspace)
		{
			if (workspace.TableCacheData == null)
			{
				return 0;
			}
			fixed (byte* ptr = &workspace.TableCacheData[0])
			{
				byte* ptr2 = ptr;
				ushort* ptr3 = (ushort*)ptr2;
				return ptr3[2];
			}
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00127868 File Offset: 0x00126C68
		[SecurityCritical]
		public static void FindNextLookup(OpenTypeLayoutWorkspace workspace, GlyphInfoList glyphInfo, ushort firstLookupIndex, out ushort lookupIndex, out int firstGlyph)
		{
			if (firstLookupIndex >= OpenTypeLayoutCache.GetCacheLookupCount(workspace))
			{
				lookupIndex = firstLookupIndex;
				firstGlyph = 0;
				return;
			}
			ushort[] cachePointers = workspace.CachePointers;
			int length = glyphInfo.Length;
			lookupIndex = ushort.MaxValue;
			firstGlyph = 0;
			for (int i = 0; i < length; i++)
			{
				while (cachePointers[i] < firstLookupIndex)
				{
					ushort[] array = cachePointers;
					int num = i;
					array[num] += 1;
				}
				if (cachePointers[i] < lookupIndex)
				{
					lookupIndex = cachePointers[i];
					firstGlyph = i;
				}
			}
			if (lookupIndex == 65535)
			{
				lookupIndex = OpenTypeLayoutCache.GetCacheLookupCount(workspace);
				firstGlyph = 0;
			}
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x001278E4 File Offset: 0x00126CE4
		[SecurityCritical]
		public static bool FindNextGlyphInLookup(OpenTypeLayoutWorkspace workspace, ushort lookupIndex, bool isLookupReversal, ref int firstGlyph, ref int afterLastGlyph)
		{
			if (lookupIndex >= OpenTypeLayoutCache.GetCacheLookupCount(workspace))
			{
				return true;
			}
			ushort[] cachePointers = workspace.CachePointers;
			if (!isLookupReversal)
			{
				for (int i = firstGlyph; i < afterLastGlyph; i++)
				{
					if (cachePointers[i] == lookupIndex)
					{
						firstGlyph = i;
						return true;
					}
				}
				return false;
			}
			for (int j = afterLastGlyph - 1; j >= firstGlyph; j--)
			{
				if (cachePointers[j] == lookupIndex)
				{
					afterLastGlyph = j + 1;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00127944 File Offset: 0x00126D44
		[SecurityCritical]
		private unsafe static void RenewPointers(GlyphInfoList glyphInfo, OpenTypeLayoutWorkspace workspace, int firstGlyph, int afterLastGlyph)
		{
			fixed (byte* ptr = &workspace.TableCacheData[0])
			{
				byte* ptr2 = ptr;
				if (ptr2 == null)
				{
					return;
				}
				ushort[] cachePointers = workspace.CachePointers;
				for (int i = firstGlyph; i < afterLastGlyph; i++)
				{
					ushort num = glyphInfo.Glyphs[i];
					int num2 = 2;
					int num3 = (int)(*(ushort*)(ptr2 + (IntPtr)3 * 2));
					ushort* ptr3 = (ushort*)(ptr2 + (IntPtr)4 * 2);
					int j = 0;
					int num4 = num3;
					while (j < num4)
					{
						int num5 = j + num4 >> 1;
						ushort num6 = ptr3[num5 * 2];
						if (num < num6)
						{
							num4 = num5;
						}
						else
						{
							if (num <= num6)
							{
								num2 = (int)ptr3[num5 * 2 + 1];
								break;
							}
							j = num5 + 1;
						}
					}
					cachePointers[i] = *(ushort*)(ptr2 + num2);
				}
			}
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x001279F8 File Offset: 0x00126DF8
		[SecurityCritical]
		internal static void CreateCache(IOpenTypeFont font, int maxCacheSize)
		{
			if (maxCacheSize > 65535)
			{
				maxCacheSize = 65535;
			}
			int num = 0;
			int num2;
			OpenTypeLayoutCache.CreateTableCache(font, OpenTypeTags.GSUB, maxCacheSize - num, out num2);
			num += num2;
			OpenTypeLayoutCache.CreateTableCache(font, OpenTypeTags.GPOS, maxCacheSize - num, out num2);
			num += num2;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x00127A40 File Offset: 0x00126E40
		[SecurityCritical]
		private static void CreateTableCache(IOpenTypeFont font, OpenTypeTags tableTag, int maxCacheSize, out int tableCacheSize)
		{
			tableCacheSize = 0;
			int num = 0;
			int recordCount = 0;
			int glyphCount = 0;
			int lastLookupAdded = -1;
			OpenTypeLayoutCache.GlyphLookupRecord[] records = null;
			try
			{
				OpenTypeLayoutCache.ComputeTableCache(font, tableTag, maxCacheSize, ref num, ref records, ref recordCount, ref glyphCount, ref lastLookupAdded);
			}
			catch (FileFormatException)
			{
				num = 0;
			}
			if (num > 0)
			{
				tableCacheSize = OpenTypeLayoutCache.FillTableCache(font, tableTag, num, records, recordCount, glyphCount, lastLookupAdded);
			}
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x00127AA4 File Offset: 0x00126EA4
		[SecurityCritical]
		private static void ComputeTableCache(IOpenTypeFont font, OpenTypeTags tableTag, int maxCacheSize, ref int cacheSize, ref OpenTypeLayoutCache.GlyphLookupRecord[] records, ref int recordCount, ref int glyphCount, ref int lastLookupAdded)
		{
			FontTable fontTable = font.GetFontTable(tableTag);
			if (!fontTable.IsPresent)
			{
				return;
			}
			FeatureList featureList;
			LookupList lookupList;
			if (tableTag != OpenTypeTags.GPOS)
			{
				if (tableTag == OpenTypeTags.GSUB)
				{
					GSUBHeader gsubheader = default(GSUBHeader);
					featureList = gsubheader.GetFeatureList(fontTable);
					lookupList = gsubheader.GetLookupList(fontTable);
				}
				else
				{
					featureList = new FeatureList(0);
					lookupList = new LookupList(0);
				}
			}
			else
			{
				GPOSHeader gposheader = default(GPOSHeader);
				featureList = gposheader.GetFeatureList(fontTable);
				lookupList = gposheader.GetLookupList(fontTable);
			}
			int num = maxCacheSize / 4;
			records = new OpenTypeLayoutCache.GlyphLookupRecord[num];
			int num2 = (int)lookupList.LookupCount(fontTable);
			int num3 = 0;
			BitArray bitArray = new BitArray(num2);
			for (ushort num4 = 0; num4 < featureList.FeatureCount(fontTable); num4 += 1)
			{
				FeatureTable featureTable = featureList.FeatureTable(fontTable, num4);
				for (ushort num5 = 0; num5 < featureTable.LookupCount(fontTable); num5 += 1)
				{
					ushort num6 = featureTable.LookupIndex(fontTable, num5);
					if ((int)num6 < num2)
					{
						bitArray[(int)num6] = true;
					}
				}
			}
			ushort num7 = 0;
			while ((int)num7 < num2)
			{
				if (bitArray[(int)num7])
				{
					int num8 = recordCount;
					int num9 = -1;
					bool flag = false;
					LookupTable lookupTable = lookupList.Lookup(fontTable, num7);
					ushort lookupType = lookupTable.LookupType();
					ushort num10 = lookupTable.SubTableCount();
					for (ushort num11 = 0; num11 < num10; num11 += 1)
					{
						int subtableOffset = lookupTable.SubtableOffset(fontTable, num11);
						CoverageTable subtablePrincipalCoverage = OpenTypeLayoutCache.GetSubtablePrincipalCoverage(fontTable, tableTag, lookupType, subtableOffset);
						if (!subtablePrincipalCoverage.IsInvalid)
						{
							flag = !OpenTypeLayoutCache.AppendCoverageGlyphRecords(fontTable, num7, subtablePrincipalCoverage, records, ref recordCount, ref num9);
							if (flag)
							{
								break;
							}
						}
					}
					if (flag)
					{
						break;
					}
					lastLookupAdded = (int)num7;
					num3 = recordCount;
				}
				num7 += 1;
			}
			recordCount = num3;
			if (lastLookupAdded == -1)
			{
				return;
			}
			Array.Sort<OpenTypeLayoutCache.GlyphLookupRecord>(records, 0, recordCount);
			cacheSize = -1;
			glyphCount = -1;
			while (recordCount > 0)
			{
				OpenTypeLayoutCache.CalculateCacheSize(records, recordCount, out cacheSize, out glyphCount);
				if (cacheSize <= maxCacheSize)
				{
					break;
				}
				int num12 = -1;
				for (int i = 0; i < recordCount; i++)
				{
					int lookup = (int)records[i].Lookup;
					if (num12 < lookup)
					{
						num12 = lookup;
					}
				}
				int num13 = 0;
				for (int j = 0; j < recordCount; j++)
				{
					if ((int)records[j].Lookup != num12 && num13 != j)
					{
						records[num13] = records[j];
						num13++;
					}
				}
				recordCount = num13;
				lastLookupAdded = num12 - 1;
			}
			int num14 = recordCount;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x00127CE8 File Offset: 0x001270E8
		[SecurityCritical]
		private unsafe static int FillTableCache(IOpenTypeFont font, OpenTypeTags tableTag, int cacheSize, OpenTypeLayoutCache.GlyphLookupRecord[] records, int recordCount, int glyphCount, int lastLookupAdded)
		{
			byte[] array = font.AllocateTableCache(tableTag, cacheSize);
			if (array == null)
			{
				return 0;
			}
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				ushort* ptr3 = (ushort*)ptr2;
				*ptr3 = (ushort)cacheSize;
				ptr3[1] = ushort.MaxValue;
				ptr3[2] = (ushort)(lastLookupAdded + 1);
				ptr3[3] = (ushort)glyphCount;
				ushort* ptr4 = ptr3 + 4;
				ushort* ptr5 = ptr4 + glyphCount * 2;
				ushort* ptr6 = null;
				int glyphListIndex = -1;
				int num = 0;
				int num2 = 0;
				int num3 = 1;
				ushort glyph = records[0].Glyph;
				for (int i = 1; i < recordCount; i++)
				{
					if (records[i].Glyph != glyph)
					{
						if (num != num3 || !OpenTypeLayoutCache.CompareGlyphRecordLists(records, recordCount, glyphListIndex, num2))
						{
							ptr6 = ptr5;
							for (int j = num2; j < i; j++)
							{
								*ptr5 = records[j].Lookup;
								ptr5++;
							}
							*ptr5 = ushort.MaxValue;
							ptr5++;
						}
						*ptr4 = glyph;
						ptr4++;
						*ptr4 = (ushort)((long)(ptr6 - ptr3) * 2L);
						ptr4++;
						glyphListIndex = num2;
						num = num3;
						glyph = records[i].Glyph;
						num2 = i;
						num3 = 1;
					}
				}
				if (num != num3 || !OpenTypeLayoutCache.CompareGlyphRecordLists(records, recordCount, glyphListIndex, num2))
				{
					ptr6 = ptr5;
					for (int k = num2; k < recordCount; k++)
					{
						*ptr5 = records[k].Lookup;
						ptr5++;
					}
					*ptr5 = ushort.MaxValue;
					ptr5++;
				}
				*ptr4 = glyph;
				ptr4++;
				*ptr4 = (ushort)((long)(ptr6 - ptr3) * 2L);
				ptr4++;
			}
			return cacheSize;
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x00127E58 File Offset: 0x00127258
		private static void CalculateCacheSize(OpenTypeLayoutCache.GlyphLookupRecord[] records, int recordCount, out int cacheSize, out int glyphCount)
		{
			glyphCount = 1;
			int num = 0;
			int num2 = 0;
			int glyphListIndex = -1;
			int num3 = 0;
			int num4 = 0;
			int num5 = 1;
			ushort glyph = records[0].Glyph;
			for (int i = 1; i < recordCount; i++)
			{
				if (records[i].Glyph != glyph)
				{
					glyphCount++;
					if (num3 != num5 || !OpenTypeLayoutCache.CompareGlyphRecordLists(records, recordCount, glyphListIndex, num4))
					{
						num++;
						num2 += num5;
					}
					glyphListIndex = num4;
					num3 = num5;
					glyph = records[i].Glyph;
					num4 = i;
					num5 = 1;
				}
				else
				{
					num5++;
				}
			}
			if (num3 != num5 || !OpenTypeLayoutCache.CompareGlyphRecordLists(records, recordCount, glyphListIndex, num4))
			{
				num++;
				num2 += num5;
			}
			cacheSize = 2 * (4 + glyphCount * 2 + num2 + num);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00127F00 File Offset: 0x00127300
		private static bool CompareGlyphRecordLists(OpenTypeLayoutCache.GlyphLookupRecord[] records, int recordCount, int glyphListIndex1, int glyphListIndex2)
		{
			ushort glyph = records[glyphListIndex1].Glyph;
			ushort glyph2 = records[glyphListIndex2].Glyph;
			for (;;)
			{
				ushort num;
				ushort num2;
				if (glyphListIndex1 != recordCount)
				{
					num = records[glyphListIndex1].Glyph;
					num2 = records[glyphListIndex1].Lookup;
				}
				else
				{
					num = ushort.MaxValue;
					num2 = ushort.MaxValue;
				}
				ushort num3;
				ushort num4;
				if (glyphListIndex2 != recordCount)
				{
					num3 = records[glyphListIndex2].Glyph;
					num4 = records[glyphListIndex2].Lookup;
				}
				else
				{
					num3 = ushort.MaxValue;
					num4 = ushort.MaxValue;
				}
				if (num != glyph && num3 != glyph2)
				{
					break;
				}
				if (num != glyph || num3 != glyph2)
				{
					return false;
				}
				if (num2 != num4)
				{
					return false;
				}
				glyphListIndex1++;
				glyphListIndex2++;
			}
			return true;
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00127F94 File Offset: 0x00127394
		[SecurityCritical]
		private static CoverageTable GetSubtablePrincipalCoverage(FontTable table, OpenTypeTags tableTag, ushort lookupType, int subtableOffset)
		{
			CoverageTable invalidCoverage = CoverageTable.InvalidCoverage;
			if (tableTag != OpenTypeTags.GPOS)
			{
				if (tableTag == OpenTypeTags.GSUB)
				{
					if (lookupType == 7)
					{
						ExtensionLookupTable extensionLookupTable = new ExtensionLookupTable(subtableOffset);
						lookupType = extensionLookupTable.LookupType(table);
						subtableOffset = extensionLookupTable.LookupSubtableOffset(table);
					}
					switch (lookupType)
					{
					case 1:
					{
						SingleSubstitutionSubtable singleSubstitutionSubtable = new SingleSubstitutionSubtable(subtableOffset);
						return singleSubstitutionSubtable.GetPrimaryCoverage(table);
					}
					case 2:
					{
						MultipleSubstitutionSubtable multipleSubstitutionSubtable = new MultipleSubstitutionSubtable(subtableOffset);
						return multipleSubstitutionSubtable.GetPrimaryCoverage(table);
					}
					case 3:
					{
						AlternateSubstitutionSubtable alternateSubstitutionSubtable = new AlternateSubstitutionSubtable(subtableOffset);
						return alternateSubstitutionSubtable.GetPrimaryCoverage(table);
					}
					case 4:
					{
						LigatureSubstitutionSubtable ligatureSubstitutionSubtable = new LigatureSubstitutionSubtable(subtableOffset);
						return ligatureSubstitutionSubtable.GetPrimaryCoverage(table);
					}
					case 5:
					{
						ContextSubtable contextSubtable = new ContextSubtable(subtableOffset);
						return contextSubtable.GetPrimaryCoverage(table);
					}
					case 6:
					{
						ChainingSubtable chainingSubtable = new ChainingSubtable(subtableOffset);
						return chainingSubtable.GetPrimaryCoverage(table);
					}
					case 8:
					{
						ReverseChainingSubtable reverseChainingSubtable = new ReverseChainingSubtable(subtableOffset);
						return reverseChainingSubtable.GetPrimaryCoverage(table);
					}
					}
				}
			}
			else
			{
				if (lookupType == 9)
				{
					ExtensionLookupTable extensionLookupTable2 = new ExtensionLookupTable(subtableOffset);
					lookupType = extensionLookupTable2.LookupType(table);
					subtableOffset = extensionLookupTable2.LookupSubtableOffset(table);
				}
				switch (lookupType)
				{
				case 1:
				{
					SinglePositioningSubtable singlePositioningSubtable = new SinglePositioningSubtable(subtableOffset);
					return singlePositioningSubtable.GetPrimaryCoverage(table);
				}
				case 2:
				{
					PairPositioningSubtable pairPositioningSubtable = new PairPositioningSubtable(subtableOffset);
					return pairPositioningSubtable.GetPrimaryCoverage(table);
				}
				case 3:
				{
					CursivePositioningSubtable cursivePositioningSubtable = new CursivePositioningSubtable(subtableOffset);
					return cursivePositioningSubtable.GetPrimaryCoverage(table);
				}
				case 4:
				{
					MarkToBasePositioningSubtable markToBasePositioningSubtable = new MarkToBasePositioningSubtable(subtableOffset);
					return markToBasePositioningSubtable.GetPrimaryCoverage(table);
				}
				case 5:
				{
					MarkToLigaturePositioningSubtable markToLigaturePositioningSubtable = new MarkToLigaturePositioningSubtable(subtableOffset);
					return markToLigaturePositioningSubtable.GetPrimaryCoverage(table);
				}
				case 6:
				{
					MarkToMarkPositioningSubtable markToMarkPositioningSubtable = new MarkToMarkPositioningSubtable(subtableOffset);
					return markToMarkPositioningSubtable.GetPrimaryCoverage(table);
				}
				case 7:
				{
					ContextSubtable contextSubtable2 = new ContextSubtable(subtableOffset);
					return contextSubtable2.GetPrimaryCoverage(table);
				}
				case 8:
				{
					ChainingSubtable chainingSubtable2 = new ChainingSubtable(subtableOffset);
					return chainingSubtable2.GetPrimaryCoverage(table);
				}
				}
			}
			return CoverageTable.InvalidCoverage;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00128164 File Offset: 0x00127564
		[SecurityCritical]
		private static bool AppendCoverageGlyphRecords(FontTable table, ushort lookupIndex, CoverageTable coverage, OpenTypeLayoutCache.GlyphLookupRecord[] records, ref int recordCount, ref int maxLookupGlyph)
		{
			ushort num = coverage.Format(table);
			if (num != 1)
			{
				if (num == 2)
				{
					ushort num2 = coverage.Format2RangeCount(table);
					for (ushort num3 = 0; num3 < num2; num3 += 1)
					{
						ushort num4 = coverage.Format2RangeStartGlyph(table, num3);
						ushort num5 = coverage.Format2RangeEndGlyph(table, num3);
						for (int i = (int)num4; i <= (int)num5; i++)
						{
							if (!OpenTypeLayoutCache.AppendGlyphRecord((ushort)i, lookupIndex, records, ref recordCount, ref maxLookupGlyph))
							{
								return false;
							}
						}
					}
				}
			}
			else
			{
				ushort num6 = coverage.Format1GlyphCount(table);
				for (ushort num7 = 0; num7 < num6; num7 += 1)
				{
					ushort glyph = coverage.Format1Glyph(table, num7);
					if (!OpenTypeLayoutCache.AppendGlyphRecord(glyph, lookupIndex, records, ref recordCount, ref maxLookupGlyph))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0012820C File Offset: 0x0012760C
		private static bool AppendGlyphRecord(ushort glyph, ushort lookupIndex, OpenTypeLayoutCache.GlyphLookupRecord[] records, ref int recordCount, ref int maxLookupGlyph)
		{
			if ((int)glyph == maxLookupGlyph)
			{
				return true;
			}
			if ((int)glyph > maxLookupGlyph)
			{
				maxLookupGlyph = (int)glyph;
			}
			else
			{
				int num = recordCount - 1;
				while (num >= 0 && records[num].Lookup == lookupIndex)
				{
					if (records[num].Glyph == glyph)
					{
						return true;
					}
					num--;
				}
			}
			if (recordCount == records.Length)
			{
				return false;
			}
			records[recordCount] = new OpenTypeLayoutCache.GlyphLookupRecord(glyph, lookupIndex);
			recordCount++;
			return true;
		}

		// Token: 0x020009BF RID: 2495
		private class GlyphLookupRecord : IComparable<OpenTypeLayoutCache.GlyphLookupRecord>
		{
			// Token: 0x06005AD0 RID: 23248 RVA: 0x0016C8A8 File Offset: 0x0016BCA8
			public GlyphLookupRecord(ushort glyph, ushort lookup)
			{
				this._glyph = glyph;
				this._lookup = lookup;
			}

			// Token: 0x17001284 RID: 4740
			// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0016C8CC File Offset: 0x0016BCCC
			public ushort Glyph
			{
				get
				{
					return this._glyph;
				}
			}

			// Token: 0x17001285 RID: 4741
			// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x0016C8E0 File Offset: 0x0016BCE0
			public ushort Lookup
			{
				get
				{
					return this._lookup;
				}
			}

			// Token: 0x06005AD3 RID: 23251 RVA: 0x0016C8F4 File Offset: 0x0016BCF4
			public int CompareTo(OpenTypeLayoutCache.GlyphLookupRecord value)
			{
				if (this._glyph < value._glyph)
				{
					return -1;
				}
				if (this._glyph > value._glyph)
				{
					return 1;
				}
				if (this._lookup < value._lookup)
				{
					return -1;
				}
				if (this._lookup > value._lookup)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06005AD4 RID: 23252 RVA: 0x0016C944 File Offset: 0x0016BD44
			public bool Equals(OpenTypeLayoutCache.GlyphLookupRecord value)
			{
				return this._glyph == value._glyph && this._lookup == value._lookup;
			}

			// Token: 0x06005AD5 RID: 23253 RVA: 0x0016C970 File Offset: 0x0016BD70
			public static bool operator ==(OpenTypeLayoutCache.GlyphLookupRecord value1, OpenTypeLayoutCache.GlyphLookupRecord value2)
			{
				return value1.Equals(value2);
			}

			// Token: 0x06005AD6 RID: 23254 RVA: 0x0016C984 File Offset: 0x0016BD84
			public static bool operator !=(OpenTypeLayoutCache.GlyphLookupRecord value1, OpenTypeLayoutCache.GlyphLookupRecord value2)
			{
				return !value1.Equals(value2);
			}

			// Token: 0x06005AD7 RID: 23255 RVA: 0x0016C99C File Offset: 0x0016BD9C
			public override bool Equals(object value)
			{
				return this.Equals((OpenTypeLayoutCache.GlyphLookupRecord)value);
			}

			// Token: 0x06005AD8 RID: 23256 RVA: 0x0016C9B8 File Offset: 0x0016BDB8
			public override int GetHashCode()
			{
				return (int)this._glyph << (int)(16 + this._lookup);
			}

			// Token: 0x04002DCF RID: 11727
			private ushort _glyph;

			// Token: 0x04002DD0 RID: 11728
			private ushort _lookup;
		}
	}
}
