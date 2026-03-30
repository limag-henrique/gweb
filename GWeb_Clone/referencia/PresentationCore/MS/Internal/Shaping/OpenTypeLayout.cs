using System;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006CF RID: 1743
	internal static class OpenTypeLayout
	{
		// Token: 0x06004B8E RID: 19342 RVA: 0x00127060 File Offset: 0x00126460
		[SecurityCritical]
		internal static TagInfoFlags FindScript(IOpenTypeFont Font, uint ScriptTag)
		{
			TagInfoFlags tagInfoFlags = TagInfoFlags.None;
			try
			{
				FontTable fontTable = Font.GetFontTable(OpenTypeTags.GSUB);
				if (fontTable.IsPresent)
				{
					GSUBHeader gsubheader = new GSUBHeader(0);
					if (!gsubheader.GetScriptList(fontTable).FindScript(fontTable, ScriptTag).IsNull)
					{
						tagInfoFlags |= TagInfoFlags.Substitution;
					}
				}
			}
			catch (FileFormatException)
			{
				return TagInfoFlags.None;
			}
			try
			{
				FontTable fontTable2 = Font.GetFontTable(OpenTypeTags.GPOS);
				if (fontTable2.IsPresent)
				{
					GPOSHeader gposheader = new GPOSHeader(0);
					if (!gposheader.GetScriptList(fontTable2).FindScript(fontTable2, ScriptTag).IsNull)
					{
						tagInfoFlags |= TagInfoFlags.Positioning;
					}
				}
			}
			catch (FileFormatException)
			{
				return TagInfoFlags.None;
			}
			return tagInfoFlags;
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00127134 File Offset: 0x00126534
		[SecurityCritical]
		internal static TagInfoFlags FindLangSys(IOpenTypeFont Font, uint ScriptTag, uint LangSysTag)
		{
			TagInfoFlags tagInfoFlags = TagInfoFlags.None;
			try
			{
				FontTable fontTable = Font.GetFontTable(OpenTypeTags.GSUB);
				if (fontTable.IsPresent)
				{
					GSUBHeader gsubheader = new GSUBHeader(0);
					ScriptTable scriptTable = gsubheader.GetScriptList(fontTable).FindScript(fontTable, ScriptTag);
					if (!scriptTable.IsNull && !scriptTable.FindLangSys(fontTable, LangSysTag).IsNull)
					{
						tagInfoFlags |= TagInfoFlags.Substitution;
					}
				}
			}
			catch (FileFormatException)
			{
				return TagInfoFlags.None;
			}
			try
			{
				FontTable fontTable2 = Font.GetFontTable(OpenTypeTags.GPOS);
				if (fontTable2.IsPresent)
				{
					GPOSHeader gposheader = new GPOSHeader(0);
					ScriptTable scriptTable2 = gposheader.GetScriptList(fontTable2).FindScript(fontTable2, ScriptTag);
					if (!scriptTable2.IsNull && !scriptTable2.FindLangSys(fontTable2, LangSysTag).IsNull)
					{
						tagInfoFlags |= TagInfoFlags.Positioning;
					}
				}
			}
			catch (FileFormatException)
			{
				return TagInfoFlags.None;
			}
			return tagInfoFlags;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00127230 File Offset: 0x00126630
		[SecurityCritical]
		internal static OpenTypeLayoutResult SubstituteGlyphs(IOpenTypeFont Font, OpenTypeLayoutWorkspace workspace, uint ScriptTag, uint LangSysTag, Feature[] FeatureSet, int featureCount, int featureSetOffset, int CharCount, UshortList Charmap, GlyphInfoList Glyphs)
		{
			try
			{
				FontTable fontTable = Font.GetFontTable(OpenTypeTags.GSUB);
				if (!fontTable.IsPresent)
				{
					return OpenTypeLayoutResult.ScriptNotFound;
				}
				GSUBHeader gsubheader = new GSUBHeader(0);
				ScriptTable scriptTable = gsubheader.GetScriptList(fontTable).FindScript(fontTable, ScriptTag);
				if (scriptTable.IsNull)
				{
					return OpenTypeLayoutResult.ScriptNotFound;
				}
				LangSysTable langSys = scriptTable.FindLangSys(fontTable, LangSysTag);
				if (langSys.IsNull)
				{
					return OpenTypeLayoutResult.LangSysNotFound;
				}
				FeatureList featureList = gsubheader.GetFeatureList(fontTable);
				LookupList lookupList = gsubheader.GetLookupList(fontTable);
				LayoutEngine.ApplyFeatures(Font, workspace, OpenTypeTags.GSUB, fontTable, default(LayoutMetrics), langSys, featureList, lookupList, FeatureSet, featureCount, featureSetOffset, CharCount, Charmap, Glyphs, null, null);
			}
			catch (FileFormatException)
			{
				return OpenTypeLayoutResult.BadFontTable;
			}
			return OpenTypeLayoutResult.Success;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00127300 File Offset: 0x00126700
		[SecurityCritical]
		internal unsafe static OpenTypeLayoutResult PositionGlyphs(IOpenTypeFont Font, OpenTypeLayoutWorkspace workspace, uint ScriptTag, uint LangSysTag, LayoutMetrics Metrics, Feature[] FeatureSet, int featureCount, int featureSetOffset, int CharCount, UshortList Charmap, GlyphInfoList Glyphs, int* Advances, LayoutOffset* Offsets)
		{
			try
			{
				FontTable fontTable = Font.GetFontTable(OpenTypeTags.GPOS);
				if (!fontTable.IsPresent)
				{
					return OpenTypeLayoutResult.ScriptNotFound;
				}
				GPOSHeader gposheader = new GPOSHeader(0);
				ScriptTable scriptTable = gposheader.GetScriptList(fontTable).FindScript(fontTable, ScriptTag);
				if (scriptTable.IsNull)
				{
					return OpenTypeLayoutResult.ScriptNotFound;
				}
				LangSysTable langSys = scriptTable.FindLangSys(fontTable, LangSysTag);
				if (langSys.IsNull)
				{
					return OpenTypeLayoutResult.LangSysNotFound;
				}
				FeatureList featureList = gposheader.GetFeatureList(fontTable);
				LookupList lookupList = gposheader.GetLookupList(fontTable);
				LayoutEngine.ApplyFeatures(Font, workspace, OpenTypeTags.GPOS, fontTable, Metrics, langSys, featureList, lookupList, FeatureSet, featureCount, featureSetOffset, CharCount, Charmap, Glyphs, Advances, Offsets);
			}
			catch (FileFormatException)
			{
				return OpenTypeLayoutResult.BadFontTable;
			}
			return OpenTypeLayoutResult.Success;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x001273C8 File Offset: 0x001267C8
		[SecurityCritical]
		internal static OpenTypeLayoutResult CreateLayoutCache(IOpenTypeFont font, int maxCacheSize)
		{
			OpenTypeLayoutCache.CreateCache(font, maxCacheSize);
			return OpenTypeLayoutResult.Success;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x001273E0 File Offset: 0x001267E0
		[SecurityCritical]
		internal static OpenTypeLayoutResult GetComplexLanguageList(IOpenTypeFont Font, uint[] featureList, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId, out WritingSystem[] complexLanguages)
		{
			OpenTypeLayoutResult result;
			try
			{
				WritingSystem[] array = null;
				WritingSystem[] array2 = null;
				int num = 0;
				int num2 = 0;
				FontTable fontTable = Font.GetFontTable(OpenTypeTags.GSUB);
				FontTable fontTable2 = Font.GetFontTable(OpenTypeTags.GPOS);
				if (fontTable.IsPresent)
				{
					LayoutEngine.GetComplexLanguageList(OpenTypeTags.GSUB, fontTable, featureList, glyphBits, minGlyphId, maxGlyphId, out array, out num);
				}
				if (fontTable2.IsPresent)
				{
					LayoutEngine.GetComplexLanguageList(OpenTypeTags.GPOS, fontTable2, featureList, glyphBits, minGlyphId, maxGlyphId, out array2, out num2);
				}
				if (array == null && array2 == null)
				{
					complexLanguages = null;
					result = OpenTypeLayoutResult.Success;
				}
				else
				{
					int num3 = 0;
					for (int i = 0; i < num2; i++)
					{
						bool flag = false;
						for (int j = 0; j < num; j++)
						{
							if (array[j].scriptTag == array2[i].scriptTag && array[j].langSysTag == array2[i].langSysTag)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (num3 < i)
							{
								array2[num3] = array2[i];
							}
							num3++;
						}
					}
					complexLanguages = new WritingSystem[num + num3];
					for (int i = 0; i < num; i++)
					{
						complexLanguages[i] = array[i];
					}
					for (int i = 0; i < num3; i++)
					{
						complexLanguages[num + i] = array2[i];
					}
					result = OpenTypeLayoutResult.Success;
				}
			}
			catch (FileFormatException)
			{
				complexLanguages = null;
				result = OpenTypeLayoutResult.BadFontTable;
			}
			return result;
		}
	}
}
