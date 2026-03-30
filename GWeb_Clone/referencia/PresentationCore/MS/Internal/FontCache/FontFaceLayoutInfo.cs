using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Windows.Media;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000774 RID: 1908
	[FriendAccessAllowed]
	internal sealed class FontFaceLayoutInfo
	{
		// Token: 0x0600506D RID: 20589 RVA: 0x00141DD0 File Offset: 0x001411D0
		[SecurityCritical]
		internal FontFaceLayoutInfo(Font font)
		{
			this._fontTechnologyInitialized = false;
			this._typographyAvailabilitiesInitialized = false;
			this._gsubInitialized = false;
			this._gposInitialized = false;
			this._gdefInitialized = false;
			this._embeddingRightsInitialized = false;
			this._gsubCache = null;
			this._gposCache = null;
			this._gsub = null;
			this._gpos = null;
			this._gdef = null;
			this._font = font;
			this._cmap = new FontFaceLayoutInfo.IntMap(this._font);
			this._cmap.TryGetValue(32, out this._blankGlyphIndex);
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x00141E5C File Offset: 0x0014125C
		internal FontFaceLayoutInfo.IntMap CharacterMap
		{
			get
			{
				return this._cmap;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x00141E70 File Offset: 0x00141270
		internal ushort BlankGlyph
		{
			get
			{
				return this._blankGlyphIndex;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x00141E84 File Offset: 0x00141284
		internal ushort DesignEmHeight
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._font.Metrics.DesignUnitsPerEm;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x00141EA4 File Offset: 0x001412A4
		internal FontEmbeddingRight EmbeddingRights
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (!this._embeddingRightsInitialized)
				{
					FontEmbeddingRight embeddingRights = FontEmbeddingRight.RestrictedLicense;
					FontFace fontFace = this._font.GetFontFace();
					ushort num;
					bool flag;
					try
					{
						flag = fontFace.ReadFontEmbeddingRights(out num);
					}
					finally
					{
						fontFace.Release();
					}
					if (flag)
					{
						if ((num & 14) == 0)
						{
							int num2 = (int)(num & 768);
							if (num2 <= 256)
							{
								if (num2 != 0)
								{
									if (num2 == 256)
									{
										embeddingRights = FontEmbeddingRight.InstallableButNoSubsetting;
									}
								}
								else
								{
									embeddingRights = FontEmbeddingRight.Installable;
								}
							}
							else if (num2 != 512)
							{
								if (num2 == 768)
								{
									embeddingRights = FontEmbeddingRight.InstallableButNoSubsettingAndWithBitmapsOnly;
								}
							}
							else
							{
								embeddingRights = FontEmbeddingRight.InstallableButWithBitmapsOnly;
							}
						}
						else if ((num & 8) != 0)
						{
							int num3 = (int)(num & 768);
							if (num3 <= 256)
							{
								if (num3 != 0)
								{
									if (num3 == 256)
									{
										embeddingRights = FontEmbeddingRight.EditableButNoSubsetting;
									}
								}
								else
								{
									embeddingRights = FontEmbeddingRight.Editable;
								}
							}
							else if (num3 != 512)
							{
								if (num3 == 768)
								{
									embeddingRights = FontEmbeddingRight.EditableButNoSubsettingAndWithBitmapsOnly;
								}
							}
							else
							{
								embeddingRights = FontEmbeddingRight.EditableButWithBitmapsOnly;
							}
						}
						else if ((num & 4) != 0)
						{
							int num4 = (int)(num & 768);
							if (num4 <= 256)
							{
								if (num4 != 0)
								{
									if (num4 == 256)
									{
										embeddingRights = FontEmbeddingRight.PreviewAndPrintButNoSubsetting;
									}
								}
								else
								{
									embeddingRights = FontEmbeddingRight.PreviewAndPrint;
								}
							}
							else if (num4 != 512)
							{
								if (num4 == 768)
								{
									embeddingRights = FontEmbeddingRight.PreviewAndPrintButNoSubsettingAndWithBitmapsOnly;
								}
							}
							else
							{
								embeddingRights = FontEmbeddingRight.PreviewAndPrintButWithBitmapsOnly;
							}
						}
					}
					this._embeddingRights = embeddingRights;
					this._embeddingRightsInitialized = true;
				}
				return this._embeddingRights;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x00142000 File Offset: 0x00141400
		internal FontTechnology FontTechnology
		{
			get
			{
				if (!this._fontTechnologyInitialized)
				{
					this.ComputeFontTechnology();
					this._fontTechnologyInitialized = true;
				}
				return this._fontTechnology;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x00142028 File Offset: 0x00141428
		internal TypographyAvailabilities TypographyAvailabilities
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (!this._typographyAvailabilitiesInitialized)
				{
					this.ComputeTypographyAvailabilities();
					this._typographyAvailabilitiesInitialized = true;
				}
				return this._typographyAvailabilities;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x00142050 File Offset: 0x00141450
		internal ushort GlyphCount
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				FontFace fontFace = this._font.GetFontFace();
				ushort glyphCount;
				try
				{
					glyphCount = fontFace.GlyphCount;
				}
				finally
				{
					fontFace.Release();
				}
				return glyphCount;
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00142098 File Offset: 0x00141498
		[SecurityCritical]
		private byte[] GetFontTable(OpenTypeTableTag openTypeTableTag)
		{
			FontFace fontFace = this._font.GetFontFace();
			byte[] result;
			try
			{
				if (!fontFace.TryGetFontTable(openTypeTableTag, out result))
				{
					result = null;
				}
			}
			finally
			{
				fontFace.Release();
			}
			return result;
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x001420E4 File Offset: 0x001414E4
		[SecurityCritical]
		internal byte[] Gsub()
		{
			if (!this._gsubInitialized)
			{
				this._gsub = this.GetFontTable(OpenTypeTableTag.TTO_GSUB);
				this._gsubInitialized = true;
			}
			return this._gsub;
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00142118 File Offset: 0x00141518
		[SecurityCritical]
		internal byte[] Gpos()
		{
			if (!this._gposInitialized)
			{
				this._gpos = this.GetFontTable(OpenTypeTableTag.TTO_GPOS);
				this._gposInitialized = true;
			}
			return this._gpos;
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0014214C File Offset: 0x0014154C
		[SecurityCritical]
		internal byte[] Gdef()
		{
			if (!this._gdefInitialized)
			{
				this._gdef = this.GetFontTable(OpenTypeTableTag.TTO_GDEF);
				this._gdefInitialized = true;
			}
			return this._gdef;
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00142180 File Offset: 0x00141580
		[SecurityCritical]
		internal byte[] GetTableCache(OpenTypeTags tableTag)
		{
			if (tableTag != OpenTypeTags.GPOS)
			{
				if (tableTag != OpenTypeTags.GSUB)
				{
					throw new NotSupportedException();
				}
				if (this.Gsub() != null)
				{
					return this._gsubCache;
				}
			}
			else if (this.Gpos() != null)
			{
				return this._gposCache;
			}
			return null;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x001421C4 File Offset: 0x001415C4
		internal byte[] AllocateTableCache(OpenTypeTags tableTag, int size)
		{
			if (tableTag == OpenTypeTags.GPOS)
			{
				this._gposCache = new byte[size];
				return this._gposCache;
			}
			if (tableTag == OpenTypeTags.GSUB)
			{
				this._gsubCache = new byte[size];
				return this._gsubCache;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0014220C File Offset: 0x0014160C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void ComputeFontTechnology()
		{
			FontFace fontFace = this._font.GetFontFace();
			try
			{
				if (fontFace.Type == FontFaceType.TrueTypeCollection)
				{
					this._fontTechnology = FontTechnology.TrueTypeCollection;
				}
				else if (fontFace.Type == FontFaceType.CFF)
				{
					this._fontTechnology = FontTechnology.PostscriptOpenType;
				}
				else
				{
					this._fontTechnology = FontTechnology.TrueType;
				}
			}
			finally
			{
				fontFace.Release();
			}
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x00142274 File Offset: 0x00141674
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void ComputeTypographyAvailabilities()
		{
			int num = this.GlyphCount + 31 >> 5;
			uint[] uints = BufferCache.GetUInts(num);
			Array.Clear(uints, 0, num);
			ushort num2 = ushort.MaxValue;
			ushort num3 = 0;
			TypographyAvailabilities typographyAvailabilities = TypographyAvailabilities.None;
			GsubGposTables font = new GsubGposTables(this);
			for (int i = 0; i < FontFaceLayoutInfo.fastTextRanges.Length; i++)
			{
				uint[] fullRange = FontFaceLayoutInfo.fastTextRanges[i].GetFullRange();
				ushort[] ushorts = BufferCache.GetUShorts(fullRange.Length);
				fixed (uint* ptr = &fullRange[0])
				{
					uint* pKeys = ptr;
					fixed (ushort* ptr2 = &ushorts[0])
					{
						ushort* pIndices = ptr2;
						this.CharacterMap.TryGetValues(pKeys, checked((uint)fullRange.Length), pIndices);
					}
				}
				for (int j = 0; j < fullRange.Length; j++)
				{
					ushort num4 = ushorts[j];
					if (num4 != 0)
					{
						uints[num4 >> 5] |= 1U << (int)(num4 % 32);
						if (num4 > num3)
						{
							num3 = num4;
						}
						if (num4 < num2)
						{
							num2 = num4;
						}
					}
				}
				BufferCache.ReleaseUShorts(ushorts);
			}
			WritingSystem[] array;
			OpenTypeLayoutResult complexLanguageList = OpenTypeLayout.GetComplexLanguageList(font, FontFaceLayoutInfo.LoclFeature, uints, num2, num3, out array);
			if (complexLanguageList != OpenTypeLayoutResult.Success)
			{
				this._typographyAvailabilities = TypographyAvailabilities.None;
				return;
			}
			if (array != null)
			{
				TypographyAvailabilities typographyAvailabilities2 = TypographyAvailabilities.FastTextMajorLanguageLocalizedFormAvailable | TypographyAvailabilities.FastTextExtraLanguageLocalizedFormAvailable;
				int num5 = 0;
				while (num5 < array.Length && typographyAvailabilities != typographyAvailabilities2)
				{
					if (MajorLanguages.Contains((ScriptTags)array[num5].scriptTag, (LanguageTags)array[num5].langSysTag))
					{
						typographyAvailabilities |= TypographyAvailabilities.FastTextMajorLanguageLocalizedFormAvailable;
					}
					else
					{
						typographyAvailabilities |= TypographyAvailabilities.FastTextExtraLanguageLocalizedFormAvailable;
					}
					num5++;
				}
			}
			complexLanguageList = OpenTypeLayout.GetComplexLanguageList(font, FontFaceLayoutInfo.RequiredTypographyFeatures, uints, num2, num3, out array);
			if (complexLanguageList != OpenTypeLayoutResult.Success)
			{
				this._typographyAvailabilities = TypographyAvailabilities.None;
				return;
			}
			if (array != null)
			{
				typographyAvailabilities |= TypographyAvailabilities.FastTextTypographyAvailable;
			}
			for (int k = 0; k < num; k++)
			{
				uints[k] = uint.MaxValue;
			}
			complexLanguageList = OpenTypeLayout.GetComplexLanguageList(font, FontFaceLayoutInfo.RequiredFeatures, uints, num2, num3, out array);
			if (complexLanguageList != OpenTypeLayoutResult.Success)
			{
				this._typographyAvailabilities = TypographyAvailabilities.None;
				return;
			}
			if (array != null)
			{
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].scriptTag == 1751215721U)
					{
						typographyAvailabilities |= TypographyAvailabilities.IdeoTypographyAvailable;
					}
					else
					{
						typographyAvailabilities |= TypographyAvailabilities.Available;
					}
				}
			}
			if (typographyAvailabilities != TypographyAvailabilities.None)
			{
				typographyAvailabilities |= TypographyAvailabilities.Available;
			}
			this._typographyAvailabilities = typographyAvailabilities;
			BufferCache.ReleaseUInts(uints);
		}

		// Token: 0x04002495 RID: 9365
		private FontTechnology _fontTechnology;

		// Token: 0x04002496 RID: 9366
		private TypographyAvailabilities _typographyAvailabilities;

		// Token: 0x04002497 RID: 9367
		private FontEmbeddingRight _embeddingRights;

		// Token: 0x04002498 RID: 9368
		private bool _embeddingRightsInitialized;

		// Token: 0x04002499 RID: 9369
		private bool _gsubInitialized;

		// Token: 0x0400249A RID: 9370
		private bool _gposInitialized;

		// Token: 0x0400249B RID: 9371
		private bool _gdefInitialized;

		// Token: 0x0400249C RID: 9372
		private bool _fontTechnologyInitialized;

		// Token: 0x0400249D RID: 9373
		private bool _typographyAvailabilitiesInitialized;

		// Token: 0x0400249E RID: 9374
		private byte[] _gsubCache;

		// Token: 0x0400249F RID: 9375
		private byte[] _gposCache;

		// Token: 0x040024A0 RID: 9376
		private byte[] _gsub;

		// Token: 0x040024A1 RID: 9377
		private byte[] _gpos;

		// Token: 0x040024A2 RID: 9378
		private byte[] _gdef;

		// Token: 0x040024A3 RID: 9379
		private Font _font;

		// Token: 0x040024A4 RID: 9380
		private ushort _blankGlyphIndex;

		// Token: 0x040024A5 RID: 9381
		private FontFaceLayoutInfo.IntMap _cmap;

		// Token: 0x040024A6 RID: 9382
		private static readonly uint[] LoclFeature = new uint[]
		{
			1819239276U
		};

		// Token: 0x040024A7 RID: 9383
		private static readonly uint[] RequiredTypographyFeatures = new uint[]
		{
			1667460464U,
			1919707495U,
			1818847073U,
			1668049255U,
			1667329140U,
			1801810542U,
			1835102827U,
			1835756907U
		};

		// Token: 0x040024A8 RID: 9384
		private static readonly uint[] RequiredFeatures = new uint[]
		{
			1819239276U,
			1667460464U,
			1919707495U,
			1818847073U,
			1668049255U,
			1667329140U,
			1801810542U,
			1835102827U,
			1835756907U
		};

		// Token: 0x040024A9 RID: 9385
		private static readonly UnicodeRange[] fastTextRanges = new UnicodeRange[]
		{
			new UnicodeRange(32, 126),
			new UnicodeRange(161, 255),
			new UnicodeRange(256, 383),
			new UnicodeRange(384, 591),
			new UnicodeRange(7680, 7935),
			new UnicodeRange(12352, 12440),
			new UnicodeRange(12443, 12447),
			new UnicodeRange(12448, 12543)
		};

		// Token: 0x020009F2 RID: 2546
		internal sealed class IntMap : IDictionary<int, ushort>, ICollection<KeyValuePair<int, ushort>>, IEnumerable<KeyValuePair<int, ushort>>, IEnumerable
		{
			// Token: 0x06005BC0 RID: 23488 RVA: 0x00170D4C File Offset: 0x0017014C
			internal IntMap(Font font)
			{
				this._font = font;
				this._cmap = null;
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x00170D70 File Offset: 0x00170170
			private Dictionary<int, ushort> CMap
			{
				get
				{
					if (this._cmap == null)
					{
						lock (this)
						{
							if (this._cmap == null)
							{
								this._cmap = new Dictionary<int, ushort>();
								for (int i = 0; i <= 1114111; i++)
								{
									ushort value;
									if (this.TryGetValue(i, out value))
									{
										this._cmap.Add(i, value);
									}
								}
							}
						}
					}
					return this._cmap;
				}
			}

			// Token: 0x06005BC2 RID: 23490 RVA: 0x00170DFC File Offset: 0x001701FC
			public void Add(int key, ushort value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005BC3 RID: 23491 RVA: 0x00170E10 File Offset: 0x00170210
			[SecurityCritical]
			[SecurityTreatAsSafe]
			public bool ContainsKey(int key)
			{
				return this._font.HasCharacter(checked((uint)key));
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x00170E2C File Offset: 0x0017022C
			public ICollection<int> Keys
			{
				get
				{
					return this.CMap.Keys;
				}
			}

			// Token: 0x06005BC5 RID: 23493 RVA: 0x00170E44 File Offset: 0x00170244
			public bool Remove(int key)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005BC6 RID: 23494 RVA: 0x00170E58 File Offset: 0x00170258
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public unsafe bool TryGetValue(int key, out ushort value)
			{
				uint num = checked((uint)key);
				uint* pCodePoints = &num;
				FontFace fontFace = this._font.GetFontFace();
				ushort num2;
				try
				{
					fontFace.GetArrayOfGlyphIndices((uint*)pCodePoints, 1U, &num2);
				}
				finally
				{
					fontFace.Release();
				}
				value = num2;
				return value > 0;
			}

			// Token: 0x06005BC7 RID: 23495 RVA: 0x00170EB0 File Offset: 0x001702B0
			[SecurityCritical]
			internal unsafe void TryGetValues(uint* pKeys, uint characterCount, ushort* pIndices)
			{
				FontFace fontFace = this._font.GetFontFace();
				try
				{
					fontFace.GetArrayOfGlyphIndices((uint*)pKeys, characterCount, pIndices);
				}
				finally
				{
					fontFace.Release();
				}
			}

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x00170EF8 File Offset: 0x001702F8
			public ICollection<ushort> Values
			{
				get
				{
					return this.CMap.Values;
				}
			}

			// Token: 0x170012C6 RID: 4806
			ushort IDictionary<int, ushort>.this[int i]
			{
				get
				{
					ushort result;
					if (!this.TryGetValue(i, out result))
					{
						throw new KeyNotFoundException();
					}
					return result;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06005BCB RID: 23499 RVA: 0x00170F44 File Offset: 0x00170344
			public void Add(KeyValuePair<int, ushort> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005BCC RID: 23500 RVA: 0x00170F58 File Offset: 0x00170358
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005BCD RID: 23501 RVA: 0x00170F6C File Offset: 0x0017036C
			public bool Contains(KeyValuePair<int, ushort> item)
			{
				return this.ContainsKey(item.Key);
			}

			// Token: 0x06005BCE RID: 23502 RVA: 0x00170F88 File Offset: 0x00170388
			public void CopyTo(KeyValuePair<int, ushort>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Get("Collection_BadRank"));
				}
				if (arrayIndex < 0 || arrayIndex >= array.Length || arrayIndex + this.Count > array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				foreach (KeyValuePair<int, ushort> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x06005BCF RID: 23503 RVA: 0x0017102C File Offset: 0x0017042C
			public int Count
			{
				get
				{
					return this.CMap.Count;
				}
			}

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x00171044 File Offset: 0x00170444
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005BD1 RID: 23505 RVA: 0x00171054 File Offset: 0x00170454
			public bool Remove(KeyValuePair<int, ushort> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005BD2 RID: 23506 RVA: 0x00171068 File Offset: 0x00170468
			public IEnumerator<KeyValuePair<int, ushort>> GetEnumerator()
			{
				return this.CMap.GetEnumerator();
			}

			// Token: 0x06005BD3 RID: 23507 RVA: 0x00171088 File Offset: 0x00170488
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<KeyValuePair<int, ushort>>)this).GetEnumerator();
			}

			// Token: 0x04002F09 RID: 12041
			private Font _font;

			// Token: 0x04002F0A RID: 12042
			private Dictionary<int, ushort> _cmap;
		}
	}
}
