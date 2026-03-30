using System;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B9 RID: 1721
	internal static class LayoutEngine
	{
		// Token: 0x06004B27 RID: 19239 RVA: 0x001252D8 File Offset: 0x001246D8
		[SecurityCritical]
		public unsafe static void ApplyFeatures(IOpenTypeFont Font, OpenTypeLayoutWorkspace workspace, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, LangSysTable LangSys, FeatureList Features, LookupList Lookups, Feature[] FeatureSet, int featureCount, int featureSetOffset, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets)
		{
			LayoutEngine.UpdateGlyphFlags(Font, GlyphInfo, 0, GlyphInfo.Length, false, GlyphFlags.Unassigned);
			if (workspace == null)
			{
				workspace = new OpenTypeLayoutWorkspace();
			}
			ushort num = Lookups.LookupCount(Table);
			LayoutEngine.CompileFeatureSet(FeatureSet, featureCount, featureSetOffset, CharCount, Table, LangSys, Features, (int)num, workspace);
			OpenTypeLayoutCache.InitCache(Font, TableTag, GlyphInfo, workspace);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				if (workspace.IsAggregatedFlagSet((int)num2))
				{
					int num3 = 0;
					int startChar = 0;
					int i = 0;
					int num4 = 0;
					OpenTypeLayoutCache.FindNextLookup(workspace, GlyphInfo, num2, out num2, out i);
					if (num2 >= num)
					{
						break;
					}
					if (workspace.IsAggregatedFlagSet((int)num2))
					{
						LookupTable lookup = Lookups.Lookup(Table, num2);
						uint parameter = 0U;
						bool flag = LayoutEngine.IsLookupReversal(TableTag, lookup.LookupType());
						while (i < GlyphInfo.Length)
						{
							if (!OpenTypeLayoutCache.FindNextGlyphInLookup(workspace, num2, flag, ref i, ref num4))
							{
								i = num4;
							}
							if (i < num4)
							{
								int length = GlyphInfo.Length;
								int num5 = length - num4;
								int num6;
								bool flag2 = LayoutEngine.ApplyLookup(Font, TableTag, Table, Metrics, lookup, CharCount, Charmap, GlyphInfo, Advances, Offsets, i, num4, parameter, 0, out num6);
								if (flag2)
								{
									if (!flag)
									{
										OpenTypeLayoutCache.OnGlyphsChanged(workspace, GlyphInfo, length, i, num6);
										num4 = GlyphInfo.Length - num5;
										i = num6;
									}
									else
									{
										OpenTypeLayoutCache.OnGlyphsChanged(workspace, GlyphInfo, length, num6, num4);
										num4 = num6;
									}
								}
								else if (flag)
								{
									num4 = num6;
								}
								else
								{
									i = num6;
								}
							}
							else
							{
								LayoutEngine.GetNextEnabledGlyphRange(FeatureSet, featureCount, featureSetOffset, Table, workspace, LangSys, Features, num2, CharCount, Charmap, startChar, num4, GlyphInfo.Length, out num3, out startChar, out i, out num4, out parameter);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x00125450 File Offset: 0x00124850
		[SecurityCritical]
		internal unsafe static bool ApplyLookup(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, LookupTable Lookup, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			ushort num = Lookup.LookupType();
			ushort lookupFlags = Lookup.LookupFlags();
			ushort num2 = Lookup.SubTableCount();
			bool flag = false;
			NextGlyph = FirstGlyph + 1;
			if (!LayoutEngine.IsLookupReversal(TableTag, num))
			{
				FirstGlyph = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph, lookupFlags, 1);
			}
			else
			{
				AfterLastGlyph = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, AfterLastGlyph - 1, lookupFlags, -1) + 1;
			}
			if (FirstGlyph >= AfterLastGlyph)
			{
				return flag;
			}
			ushort num3 = num;
			ushort num4 = 0;
			while (!flag && num4 < num2)
			{
				num = num3;
				int offset = Lookup.SubtableOffset(Table, num4);
				if (TableTag != OpenTypeTags.GPOS)
				{
					if (TableTag == OpenTypeTags.GSUB)
					{
						if (num == 7)
						{
							ExtensionLookupTable extensionLookupTable = new ExtensionLookupTable(offset);
							num = extensionLookupTable.LookupType(Table);
							offset = extensionLookupTable.LookupSubtableOffset(Table);
						}
						switch (num)
						{
						case 1:
						{
							SingleSubstitutionSubtable singleSubstitutionSubtable = new SingleSubstitutionSubtable(offset);
							flag = singleSubstitutionSubtable.Apply(Table, GlyphInfo, FirstGlyph, out NextGlyph);
							break;
						}
						case 2:
						{
							MultipleSubstitutionSubtable multipleSubstitutionSubtable = new MultipleSubstitutionSubtable(offset);
							flag = multipleSubstitutionSubtable.Apply(Font, Table, CharCount, Charmap, GlyphInfo, lookupFlags, FirstGlyph, AfterLastGlyph, out NextGlyph);
							break;
						}
						case 3:
						{
							AlternateSubstitutionSubtable alternateSubstitutionSubtable = new AlternateSubstitutionSubtable(offset);
							flag = alternateSubstitutionSubtable.Apply(Table, GlyphInfo, Parameter, FirstGlyph, out NextGlyph);
							break;
						}
						case 4:
						{
							LigatureSubstitutionSubtable ligatureSubstitutionSubtable = new LigatureSubstitutionSubtable(offset);
							flag = ligatureSubstitutionSubtable.Apply(Font, Table, CharCount, Charmap, GlyphInfo, lookupFlags, FirstGlyph, AfterLastGlyph, out NextGlyph);
							break;
						}
						case 5:
						{
							ContextSubtable contextSubtable = new ContextSubtable(offset);
							flag = contextSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, lookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
							break;
						}
						case 6:
						{
							ChainingSubtable chainingSubtable = new ChainingSubtable(offset);
							flag = chainingSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, lookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
							break;
						}
						case 7:
							NextGlyph = FirstGlyph + 1;
							break;
						case 8:
						{
							ReverseChainingSubtable reverseChainingSubtable = new ReverseChainingSubtable(offset);
							flag = reverseChainingSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, lookupFlags, FirstGlyph, AfterLastGlyph, Parameter, out NextGlyph);
							break;
						}
						default:
							NextGlyph = FirstGlyph + 1;
							break;
						}
						if (flag)
						{
							if (!LayoutEngine.IsLookupReversal(TableTag, num))
							{
								LayoutEngine.UpdateGlyphFlags(Font, GlyphInfo, FirstGlyph, NextGlyph, true, GlyphFlags.Substituted);
							}
							else
							{
								LayoutEngine.UpdateGlyphFlags(Font, GlyphInfo, NextGlyph, AfterLastGlyph, true, GlyphFlags.Substituted);
							}
						}
					}
				}
				else
				{
					if (num == 9)
					{
						ExtensionLookupTable extensionLookupTable2 = new ExtensionLookupTable(offset);
						num = extensionLookupTable2.LookupType(Table);
						offset = extensionLookupTable2.LookupSubtableOffset(Table);
					}
					switch (num)
					{
					case 1:
					{
						SinglePositioningSubtable singlePositioningSubtable = new SinglePositioningSubtable(offset);
						flag = singlePositioningSubtable.Apply(Table, Metrics, GlyphInfo, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 2:
					{
						PairPositioningSubtable pairPositioningSubtable = new PairPositioningSubtable(offset);
						flag = pairPositioningSubtable.Apply(Font, Table, Metrics, GlyphInfo, lookupFlags, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 3:
					{
						CursivePositioningSubtable cursivePositioningSubtable = new CursivePositioningSubtable(offset);
						cursivePositioningSubtable.Apply(Font, Table, Metrics, GlyphInfo, lookupFlags, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 4:
					{
						MarkToBasePositioningSubtable markToBasePositioningSubtable = new MarkToBasePositioningSubtable(offset);
						flag = markToBasePositioningSubtable.Apply(Font, Table, Metrics, GlyphInfo, lookupFlags, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 5:
					{
						MarkToLigaturePositioningSubtable markToLigaturePositioningSubtable = new MarkToLigaturePositioningSubtable(offset);
						flag = markToLigaturePositioningSubtable.Apply(Font, Table, Metrics, GlyphInfo, lookupFlags, CharCount, Charmap, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 6:
					{
						MarkToMarkPositioningSubtable markToMarkPositioningSubtable = new MarkToMarkPositioningSubtable(offset);
						flag = markToMarkPositioningSubtable.Apply(Font, Table, Metrics, GlyphInfo, lookupFlags, Advances, Offsets, FirstGlyph, AfterLastGlyph, out NextGlyph);
						break;
					}
					case 7:
					{
						ContextSubtable contextSubtable2 = new ContextSubtable(offset);
						flag = contextSubtable2.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, lookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
						break;
					}
					case 8:
					{
						ChainingSubtable chainingSubtable2 = new ChainingSubtable(offset);
						flag = chainingSubtable2.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, lookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
						break;
					}
					case 9:
						NextGlyph = FirstGlyph + 1;
						break;
					default:
						NextGlyph = FirstGlyph + 1;
						break;
					}
					if (flag)
					{
						LayoutEngine.UpdateGlyphFlags(Font, GlyphInfo, FirstGlyph, NextGlyph, false, GlyphFlags.Positioned);
					}
				}
				num4 += 1;
			}
			return flag;
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x00125830 File Offset: 0x00124C30
		private static bool IsLookupReversal(OpenTypeTags TableTag, ushort LookupType)
		{
			return TableTag == OpenTypeTags.GSUB && LookupType == 8;
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x0012584C File Offset: 0x00124C4C
		[SecurityCritical]
		private static void CompileFeatureSet(Feature[] FeatureSet, int featureCount, int featureSetOffset, int charCount, FontTable Table, LangSysTable LangSys, FeatureList Features, int lookupCount, OpenTypeLayoutWorkspace workspace)
		{
			workspace.InitLookupUsageFlags(lookupCount, featureCount);
			FeatureTable featureTable = LangSys.RequiredFeature(Table, Features);
			if (!featureTable.IsNull)
			{
				int num = (int)featureTable.LookupCount(Table);
				ushort num2 = 0;
				while ((int)num2 < num)
				{
					workspace.SetRequiredFeatureFlag((int)featureTable.LookupIndex(Table, num2));
					num2 += 1;
				}
			}
			for (int i = 0; i < featureCount; i++)
			{
				Feature feature = FeatureSet[i];
				if (feature.Parameter != 0U && (int)feature.StartIndex < featureSetOffset + charCount && (int)(feature.StartIndex + feature.Length) > featureSetOffset)
				{
					FeatureTable featureTable2 = LangSys.FindFeature(Table, Features, feature.Tag);
					if (!featureTable2.IsNull)
					{
						int num3 = (int)featureTable2.LookupCount(Table);
						ushort num4 = 0;
						while ((int)num4 < num3)
						{
							workspace.SetFeatureFlag((int)featureTable2.LookupIndex(Table, num4), i);
							num4 += 1;
						}
					}
				}
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00125920 File Offset: 0x00124D20
		[SecurityCritical]
		private static void GetNextEnabledGlyphRange(Feature[] FeatureSet, int featureCount, int featureSetOffset, FontTable Table, OpenTypeLayoutWorkspace workspace, LangSysTable LangSys, FeatureList Features, ushort lookupIndex, int CharCount, UshortList Charmap, int StartChar, int StartGlyph, int GlyphRunLength, out int FirstChar, out int AfterLastChar, out int FirstGlyph, out int AfterLastGlyph, out uint Parameter)
		{
			FirstChar = int.MaxValue;
			AfterLastChar = int.MaxValue;
			FirstGlyph = StartGlyph;
			AfterLastGlyph = GlyphRunLength;
			Parameter = 0U;
			if (workspace.IsRequiredFeatureFlagSet((int)lookupIndex))
			{
				FirstChar = StartChar;
				AfterLastChar = CharCount;
				FirstGlyph = StartGlyph;
				AfterLastGlyph = GlyphRunLength;
				return;
			}
			for (int i = 0; i < featureCount; i++)
			{
				if (workspace.IsFeatureFlagSet((int)lookupIndex, i))
				{
					Feature feature = FeatureSet[i];
					int num = (int)feature.StartIndex - featureSetOffset;
					if (num < 0)
					{
						num = 0;
					}
					int num2 = (int)(feature.StartIndex + feature.Length) - featureSetOffset;
					if (num2 > CharCount)
					{
						num2 = CharCount;
					}
					if (num2 > StartChar && (num < FirstChar || (num == FirstChar && num2 >= AfterLastChar)))
					{
						FirstChar = num;
						AfterLastChar = num2;
						Parameter = feature.Parameter;
					}
				}
			}
			if (FirstChar == 2147483647)
			{
				FirstGlyph = GlyphRunLength;
				AfterLastGlyph = GlyphRunLength;
				return;
			}
			if (StartGlyph > (int)Charmap[FirstChar])
			{
				FirstGlyph = StartGlyph;
			}
			else
			{
				FirstGlyph = (int)Charmap[FirstChar];
			}
			if (AfterLastChar < CharCount)
			{
				AfterLastGlyph = (int)Charmap[AfterLastChar];
				return;
			}
			AfterLastGlyph = GlyphRunLength;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00125A28 File Offset: 0x00124E28
		[SecurityCritical]
		private static void UpdateGlyphFlags(IOpenTypeFont Font, GlyphInfoList GlyphInfo, int FirstGlyph, int AfterLastGlyph, bool DoAll, GlyphFlags FlagToSet)
		{
			ushort num = 7;
			FontTable fontTable = Font.GetFontTable(OpenTypeTags.GDEF);
			if (!fontTable.IsPresent)
			{
				for (int i = FirstGlyph; i < AfterLastGlyph; i++)
				{
					ushort num2 = (GlyphInfo.GlyphFlags[i] & ~num) | 0 | (ushort)FlagToSet;
				}
				return;
			}
			GDEFHeader gdefheader = new GDEFHeader(0);
			ClassDefTable glyphClassDef = gdefheader.GetGlyphClassDef(fontTable);
			for (int j = FirstGlyph; j < AfterLastGlyph; j++)
			{
				ushort num3 = GlyphInfo.GlyphFlags[j] | (ushort)FlagToSet;
				if ((num3 & num) == 7 || FlagToSet != GlyphFlags.Unassigned)
				{
					ushort glyph = GlyphInfo.Glyphs[j];
					num3 &= ~num;
					int @class = (int)glyphClassDef.GetClass(fontTable, glyph);
					GlyphInfo.GlyphFlags[j] = (num3 | ((@class == -1) ? 0 : ((ushort)@class)));
				}
			}
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00125AE8 File Offset: 0x00124EE8
		[SecurityCritical]
		internal static int GetNextGlyphInLookup(IOpenTypeFont Font, GlyphInfoList GlyphInfo, int FirstGlyph, ushort LookupFlags, int Direction)
		{
			FontTable fontTable = null;
			ClassDefTable classDefTable = ClassDefTable.InvalidClassDef;
			if (LookupFlags == 0)
			{
				return FirstGlyph;
			}
			if ((LookupFlags & 65280) != 0)
			{
				fontTable = Font.GetFontTable(OpenTypeTags.GDEF);
				if (fontTable.IsPresent)
				{
					classDefTable = new GDEFHeader(0).GetMarkAttachClassDef(fontTable);
				}
			}
			UshortList glyphFlags = GlyphInfo.GlyphFlags;
			ushort num = (ushort)((LookupFlags & 65280) >> 8);
			int length = GlyphInfo.Length;
			int num2 = FirstGlyph;
			while (num2 < length && num2 >= 0)
			{
				if (((LookupFlags & 2) == 0 || (glyphFlags[num2] & 7) != 1) && ((LookupFlags & 8) == 0 || (glyphFlags[num2] & 7) != 3) && ((LookupFlags & 4) == 0 || (glyphFlags[num2] & 7) != 2) && (num == 0 || (glyphFlags[num2] & 7) != 3 || classDefTable.IsInvalid || num == classDefTable.GetClass(fontTable, GlyphInfo.Glyphs[num2])))
				{
					return num2;
				}
				num2 += Direction;
			}
			return num2;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00125BC4 File Offset: 0x00124FC4
		[SecurityCritical]
		internal static void GetComplexLanguageList(OpenTypeTags tableTag, FontTable table, uint[] featureTagsList, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId, out WritingSystem[] complexLanguages, out int complexLanguageCount)
		{
			ScriptList scriptList = new ScriptList(0);
			FeatureList featureList = new FeatureList(0);
			LookupList lookupList = new LookupList(0);
			if (tableTag != OpenTypeTags.GPOS)
			{
				if (tableTag == OpenTypeTags.GSUB)
				{
					GSUBHeader gsubheader = new GSUBHeader(0);
					scriptList = gsubheader.GetScriptList(table);
					featureList = gsubheader.GetFeatureList(table);
					lookupList = gsubheader.GetLookupList(table);
				}
			}
			else
			{
				GPOSHeader gposheader = new GPOSHeader(0);
				scriptList = gposheader.GetScriptList(table);
				featureList = gposheader.GetFeatureList(table);
				lookupList = gposheader.GetLookupList(table);
			}
			int scriptCount = (int)scriptList.GetScriptCount(table);
			int num = (int)featureList.FeatureCount(table);
			int num2 = (int)lookupList.LookupCount(table);
			uint[] array = new uint[num2 + 31 >> 5];
			for (int i = 0; i < num2 + 31 >> 5; i++)
			{
				array[i] = 0U;
			}
			ushort num3 = 0;
			while ((int)num3 < num)
			{
				uint num4 = featureList.FeatureTag(table, num3);
				bool flag = false;
				for (int j = 0; j < featureTagsList.Length; j++)
				{
					if (featureTagsList[j] == num4)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					FeatureTable featureTable = featureList.FeatureTable(table, num3);
					ushort num5 = featureTable.LookupCount(table);
					for (ushort num6 = 0; num6 < num5; num6 += 1)
					{
						ushort num7 = featureTable.LookupIndex(table, num6);
						if ((int)num7 >= num2)
						{
							throw new FileFormatException();
						}
						array[num7 >> 5] |= 1U << (int)(num7 % 32);
					}
				}
				num3 += 1;
			}
			ushort num8 = 0;
			while ((int)num8 < num2)
			{
				if (((ulong)array[num8 >> 5] & (ulong)(1L << (int)(num8 % 32 & 31))) != 0UL)
				{
					LookupTable lookupTable = lookupList.Lookup(table, num8);
					ushort num9 = lookupTable.LookupType();
					ushort num10 = lookupTable.SubTableCount();
					bool flag2 = false;
					ushort num11 = num9;
					ushort num12 = 0;
					while (!flag2 && num12 < num10)
					{
						num9 = num11;
						int offset = lookupTable.SubtableOffset(table, num12);
						if (tableTag != OpenTypeTags.GPOS)
						{
							if (tableTag == OpenTypeTags.GSUB)
							{
								if (num9 == 7)
								{
									ExtensionLookupTable extensionLookupTable = new ExtensionLookupTable(offset);
									num9 = extensionLookupTable.LookupType(table);
									offset = extensionLookupTable.LookupSubtableOffset(table);
								}
								switch (num9)
								{
								case 1:
								{
									SingleSubstitutionSubtable singleSubstitutionSubtable = new SingleSubstitutionSubtable(offset);
									flag2 = singleSubstitutionSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 2:
								{
									MultipleSubstitutionSubtable multipleSubstitutionSubtable = new MultipleSubstitutionSubtable(offset);
									flag2 = multipleSubstitutionSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 3:
								{
									AlternateSubstitutionSubtable alternateSubstitutionSubtable = new AlternateSubstitutionSubtable(offset);
									flag2 = alternateSubstitutionSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 4:
								{
									LigatureSubstitutionSubtable ligatureSubstitutionSubtable = new LigatureSubstitutionSubtable(offset);
									flag2 = ligatureSubstitutionSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 5:
								{
									ContextSubtable contextSubtable = new ContextSubtable(offset);
									flag2 = contextSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 6:
								{
									ChainingSubtable chainingSubtable = new ChainingSubtable(offset);
									flag2 = chainingSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								case 7:
									break;
								case 8:
								{
									ReverseChainingSubtable reverseChainingSubtable = new ReverseChainingSubtable(offset);
									flag2 = reverseChainingSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
									break;
								}
								default:
									flag2 = true;
									break;
								}
							}
						}
						else
						{
							if (num9 == 9)
							{
								ExtensionLookupTable extensionLookupTable2 = new ExtensionLookupTable(offset);
								num9 = extensionLookupTable2.LookupType(table);
								offset = extensionLookupTable2.LookupSubtableOffset(table);
							}
							switch (num9)
							{
							case 1:
							{
								SinglePositioningSubtable singlePositioningSubtable = new SinglePositioningSubtable(offset);
								flag2 = singlePositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 2:
							{
								PairPositioningSubtable pairPositioningSubtable = new PairPositioningSubtable(offset);
								flag2 = pairPositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 3:
							{
								CursivePositioningSubtable cursivePositioningSubtable = new CursivePositioningSubtable(offset);
								flag2 = cursivePositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 4:
							{
								MarkToBasePositioningSubtable markToBasePositioningSubtable = new MarkToBasePositioningSubtable(offset);
								flag2 = markToBasePositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 5:
							{
								MarkToLigaturePositioningSubtable markToLigaturePositioningSubtable = new MarkToLigaturePositioningSubtable(offset);
								flag2 = markToLigaturePositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 6:
							{
								MarkToMarkPositioningSubtable markToMarkPositioningSubtable = new MarkToMarkPositioningSubtable(offset);
								flag2 = markToMarkPositioningSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 7:
							{
								ContextSubtable contextSubtable2 = new ContextSubtable(offset);
								flag2 = contextSubtable2.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 8:
							{
								ChainingSubtable chainingSubtable2 = new ChainingSubtable(offset);
								flag2 = chainingSubtable2.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
								break;
							}
							case 9:
								break;
							default:
								flag2 = true;
								break;
							}
						}
						num12 += 1;
					}
					if (!flag2)
					{
						array[num8 >> 5] &= ~(1U << (int)(num8 % 32));
					}
				}
				num8 += 1;
			}
			bool flag3 = false;
			for (int k = 0; k < num2 + 31 >> 5; k++)
			{
				if (array[k] != 0U)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				complexLanguages = null;
				complexLanguageCount = 0;
				return;
			}
			complexLanguages = new WritingSystem[10];
			complexLanguageCount = 0;
			ushort num13 = 0;
			while ((int)num13 < scriptCount)
			{
				ScriptTable scriptTable = scriptList.GetScriptTable(table, num13);
				uint scriptTag = scriptList.GetScriptTag(table, num13);
				ushort langSysCount = scriptTable.GetLangSysCount(table);
				if (scriptTable.IsDefaultLangSysExists(table))
				{
					LayoutEngine.AppendLangSys(scriptTag, 1684434036U, scriptTable.GetDefaultLangSysTable(table), featureList, table, featureTagsList, array, ref complexLanguages, ref complexLanguageCount);
				}
				for (ushort num14 = 0; num14 < langSysCount; num14 += 1)
				{
					uint langSysTag = scriptTable.GetLangSysTag(table, num14);
					LayoutEngine.AppendLangSys(scriptTag, langSysTag, scriptTable.GetLangSysTable(table, num14), featureList, table, featureTagsList, array, ref complexLanguages, ref complexLanguageCount);
				}
				num13 += 1;
			}
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001260E0 File Offset: 0x001254E0
		[SecurityCritical]
		private static void AppendLangSys(uint scriptTag, uint langSysTag, LangSysTable langSysTable, FeatureList featureList, FontTable table, uint[] featureTagsList, uint[] lookupBits, ref WritingSystem[] complexLanguages, ref int complexLanguageCount)
		{
			ushort num = langSysTable.FeatureCount(table);
			bool flag = false;
			ushort num2 = 0;
			while (!flag && num2 < num)
			{
				ushort featureIndex = langSysTable.GetFeatureIndex(table, num2);
				uint num3 = featureList.FeatureTag(table, featureIndex);
				bool flag2 = false;
				int num4 = 0;
				while (!flag && num4 < featureTagsList.Length)
				{
					if (featureTagsList[num4] == num3)
					{
						flag2 = true;
						break;
					}
					num4++;
				}
				if (flag2)
				{
					FeatureTable featureTable = featureList.FeatureTable(table, featureIndex);
					ushort num5 = featureTable.LookupCount(table);
					for (ushort num6 = 0; num6 < num5; num6 += 1)
					{
						ushort num7 = featureTable.LookupIndex(table, num6);
						if (((ulong)lookupBits[num7 >> 5] & (ulong)(1L << (int)(num7 % 32 & 31))) != 0UL)
						{
							flag = true;
							break;
						}
					}
				}
				num2 += 1;
			}
			if (flag)
			{
				if (complexLanguages.Length == complexLanguageCount)
				{
					WritingSystem[] array = new WritingSystem[complexLanguages.Length * 3 / 2];
					for (int i = 0; i < complexLanguages.Length; i++)
					{
						array[i] = complexLanguages[i];
					}
					complexLanguages = array;
				}
				complexLanguages[complexLanguageCount].scriptTag = scriptTag;
				complexLanguages[complexLanguageCount].langSysTag = langSysTag;
				complexLanguageCount++;
			}
		}

		// Token: 0x04001FFF RID: 8191
		public const ushort LookupFlagRightToLeft = 1;

		// Token: 0x04002000 RID: 8192
		public const ushort LookupFlagIgnoreBases = 2;

		// Token: 0x04002001 RID: 8193
		public const ushort LookupFlagIgnoreLigatures = 4;

		// Token: 0x04002002 RID: 8194
		public const ushort LookupFlagIgnoreMarks = 8;

		// Token: 0x04002003 RID: 8195
		public const ushort LookupFlagMarkAttachmentTypeMask = 65280;

		// Token: 0x04002004 RID: 8196
		public const ushort LookupFlagFindBase = 8;

		// Token: 0x04002005 RID: 8197
		public const int LookForward = 1;

		// Token: 0x04002006 RID: 8198
		public const int LookBackward = -1;
	}
}
