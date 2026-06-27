using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Composition;
using System.Windows.Media.Converters;
using System.Windows.Media.TextFormatting;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace System.Windows.Media
{
	/// <summary>Representa uma sequência de glifos de uma única face de uma única fonte em um tamanho único, com um único estilo de renderização.</summary>
	// Token: 0x02000406 RID: 1030
	public class GlyphRun : DUCE.IResource, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		// Token: 0x06002928 RID: 10536 RVA: 0x000A4CE0 File Offset: 0x000A40E0
		[Obsolete("Use the PixelsPerDip override", false)]
		public GlyphRun()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <param name="pixelsPerDip">O valor de Pixels por Pixel Independente de Densidade, que é o equivalente do fator de escala. Por exemplo, se o DPI da tela for 120 (ou 1,25 porque 120/96 = 1,25), será desenhado 1,25 pixel por pixel independente de densidade. DIP é a unidade de medida usada pelo WPF para ser independente da resolução do dispositivo e DPIs.</param>
		// Token: 0x06002929 RID: 10537 RVA: 0x000A4D00 File Offset: 0x000A4100
		public GlyphRun(float pixelsPerDip)
		{
			this._pixelsPerDip = pixelsPerDip;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRun" /> especificando propriedades da classe.</summary>
		/// <param name="glyphTypeface">Um valor do tipo <see cref="T:System.Windows.Media.GlyphTypeface" />.</param>
		/// <param name="bidiLevel">Um valor do tipo <see cref="T:System.Int32" />.</param>
		/// <param name="isSideways">Um valor do tipo <see cref="T:System.Boolean" />.</param>
		/// <param name="renderingEmSize">Um valor do tipo <see cref="T:System.Double" />.</param>
		/// <param name="pixelsPerDip">Um valor do tipo <see cref="T:System.Double" />.</param>
		/// <param name="glyphIndices">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="baselineOrigin">Um valor do tipo <see cref="T:System.Windows.Point" />.</param>
		/// <param name="advanceWidths">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="glyphOffsets">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="characters">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="deviceFontName">Um valor do tipo <see cref="T:System.String" />.</param>
		/// <param name="clusterMap">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="caretStops">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="language">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		// Token: 0x0600292A RID: 10538 RVA: 0x000A4D28 File Offset: 0x000A4128
		[CLSCompliant(false)]
		public GlyphRun(GlyphTypeface glyphTypeface, int bidiLevel, bool isSideways, double renderingEmSize, float pixelsPerDip, IList<ushort> glyphIndices, Point baselineOrigin, IList<double> advanceWidths, IList<Point> glyphOffsets, IList<char> characters, string deviceFontName, IList<ushort> clusterMap, IList<bool> caretStops, XmlLanguage language)
		{
			this.Initialize(glyphTypeface, bidiLevel, isSideways, renderingEmSize, pixelsPerDip, glyphIndices, baselineOrigin, advanceWidths, glyphOffsets, characters, deviceFontName, clusterMap, caretStops, language, TextFormattingMode.Ideal);
			this._flags |= GlyphRun.GlyphRunFlags.CacheInkBounds;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRun" /> especificando propriedades da classe.</summary>
		/// <param name="glyphTypeface">Um valor do tipo <see cref="T:System.Windows.Media.GlyphTypeface" />.</param>
		/// <param name="bidiLevel">Um valor do tipo <see cref="T:System.Int32" />.</param>
		/// <param name="isSideways">Um valor do tipo <see cref="T:System.Boolean" />.</param>
		/// <param name="renderingEmSize">Um valor do tipo <see cref="T:System.Double" />.</param>
		/// <param name="glyphIndices">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="baselineOrigin">Um valor do tipo <see cref="T:System.Windows.Point" />.</param>
		/// <param name="advanceWidths">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="glyphOffsets">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="characters">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="deviceFontName">Um valor do tipo <see cref="T:System.String" />.</param>
		/// <param name="clusterMap">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="caretStops">Uma matriz do tipo <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <param name="language">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		// Token: 0x0600292B RID: 10539 RVA: 0x000A4D78 File Offset: 0x000A4178
		[CLSCompliant(false)]
		[Obsolete("Use the PixelsPerDip override", false)]
		public GlyphRun(GlyphTypeface glyphTypeface, int bidiLevel, bool isSideways, double renderingEmSize, IList<ushort> glyphIndices, Point baselineOrigin, IList<double> advanceWidths, IList<Point> glyphOffsets, IList<char> characters, string deviceFontName, IList<ushort> clusterMap, IList<bool> caretStops, XmlLanguage language)
		{
			this.Initialize(glyphTypeface, bidiLevel, isSideways, renderingEmSize, Util.PixelsPerDip, glyphIndices, baselineOrigin, advanceWidths, glyphOffsets, characters, deviceFontName, clusterMap, caretStops, language, TextFormattingMode.Ideal);
			this._flags |= GlyphRun.GlyphRunFlags.CacheInkBounds;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000A4DC8 File Offset: 0x000A41C8
		internal static GlyphRun TryCreate(GlyphTypeface glyphTypeface, int bidiLevel, bool isSideways, double renderingEmSize, float pixelsPerDip, IList<ushort> glyphIndices, Point baselineOrigin, IList<double> advanceWidths, IList<Point> glyphOffsets, IList<char> characters, string deviceFontName, IList<ushort> clusterMap, IList<bool> caretStops, XmlLanguage language, TextFormattingMode textLayout)
		{
			GlyphRun glyphRun = new GlyphRun(pixelsPerDip);
			glyphRun.Initialize(glyphTypeface, bidiLevel, isSideways, renderingEmSize, pixelsPerDip, glyphIndices, baselineOrigin, advanceWidths, glyphOffsets, characters, deviceFontName, clusterMap, caretStops, language, textLayout);
			glyphRun._flags |= GlyphRun.GlyphRunFlags.CacheInkBounds;
			if (glyphRun.IsInitialized)
			{
				return glyphRun;
			}
			return null;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000A4E18 File Offset: 0x000A4218
		private void Initialize(GlyphTypeface glyphTypeface, int bidiLevel, bool isSideways, double renderingEmSize, float pixelsPerDip, IList<ushort> glyphIndices, Point baselineOrigin, IList<double> advanceWidths, IList<Point> glyphOffsets, IList<char> characters, string deviceFontName, IList<ushort> clusterMap, IList<bool> caretStops, XmlLanguage language, TextFormattingMode textFormattingMode)
		{
			if (glyphTypeface != null && glyphIndices != null && advanceWidths != null && renderingEmSize >= 0.0 && glyphIndices.Count > 0 && glyphIndices.Count <= 65535 && advanceWidths.Count == glyphIndices.Count && (glyphOffsets == null || (glyphOffsets != null && glyphOffsets.Count != 0 && glyphOffsets.Count == glyphIndices.Count)))
			{
				this._textFormattingMode = textFormattingMode;
				this._glyphIndices = glyphIndices;
				this._characters = characters;
				this._clusterMap = clusterMap;
				this._baselineOrigin = baselineOrigin;
				this._renderingEmSize = renderingEmSize;
				this._advanceWidths = advanceWidths;
				this._glyphOffsets = glyphOffsets;
				this._glyphTypeface = glyphTypeface;
				this._flags = (isSideways ? GlyphRun.GlyphRunFlags.IsSideways : GlyphRun.GlyphRunFlags.None);
				this._bidiLevel = bidiLevel;
				this._caretStops = caretStops;
				this._language = language;
				this._deviceFontName = deviceFontName;
				this._pixelsPerDip = pixelsPerDip;
				if (characters != null && characters.Count != 0)
				{
					if (clusterMap != null && clusterMap.Count != 0)
					{
						if (clusterMap.Count != characters.Count)
						{
							throw new ArgumentException(SR.Get("CollectionNumberOfElementsShouldBeEqualTo", new object[]
							{
								characters.Count
							}), "clusterMap");
						}
						if (clusterMap[0] != 0)
						{
							throw new ArgumentException(SR.Get("ClusterMapFirstEntryMustBeZero"), "clusterMap");
						}
						int glyphCount = this.GlyphCount;
						int count = clusterMap.Count;
						ushort num = clusterMap[0];
						for (int i = 1; i < count; i++)
						{
							ushort num2 = clusterMap[i];
							if (num2 >= num && (int)num2 < glyphCount)
							{
								num = num2;
							}
							else
							{
								if (clusterMap[i] < clusterMap[i - 1])
								{
									throw new ArgumentException(SR.Get("ClusterMapEntriesShouldNotDecrease"), "clusterMap");
								}
								if ((int)clusterMap[i] >= this.GlyphCount)
								{
									throw new ArgumentException(SR.Get("ClusterMapEntryShouldPointWithinGlyphIndices"), "clusterMap");
								}
							}
						}
					}
					else if (this.GlyphCount != characters.Count)
					{
						throw new ArgumentException(SR.Get("CollectionNumberOfElementsShouldBeEqualTo", new object[]
						{
							this.GlyphCount
						}), "clusterMap");
					}
				}
				if (caretStops != null && caretStops.Count != 0 && caretStops.Count != this.CodepointCount + 1)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsShouldBeEqualTo", new object[]
					{
						this.CodepointCount + 1
					}), "caretStops");
				}
				if (isSideways && (bidiLevel & 1) != 0)
				{
					throw new ArgumentException(SR.Get("SidewaysRTLTextIsNotSupported"));
				}
			}
			else
			{
				if (DoubleUtil.IsNaN(renderingEmSize))
				{
					throw new ArgumentOutOfRangeException("renderingEmSize", SR.Get("ParameterValueCannotBeNaN"));
				}
				if (renderingEmSize < 0.0)
				{
					throw new ArgumentOutOfRangeException("renderingEmSize", SR.Get("ParameterValueCannotBeNegative"));
				}
				if (glyphTypeface == null)
				{
					throw new ArgumentNullException("glyphTypeface");
				}
				if (glyphIndices == null)
				{
					throw new ArgumentNullException("glyphIndices");
				}
				if (glyphIndices.Count <= 0)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeGreaterThanZero"), "glyphIndices");
				}
				if (glyphIndices.Count > 65535)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeLessOrEqualTo", new object[]
					{
						65535
					}), "glyphIndices");
				}
				if (advanceWidths == null)
				{
					throw new ArgumentNullException("advanceWidths");
				}
				if (advanceWidths.Count != glyphIndices.Count)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsShouldBeEqualTo", new object[]
					{
						glyphIndices.Count
					}), "advanceWidths");
				}
				if (glyphOffsets != null && glyphOffsets.Count != 0 && glyphOffsets.Count != glyphIndices.Count)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsShouldBeEqualTo", new object[]
					{
						glyphIndices.Count
					}), "glyphOffsets");
				}
				Invariant.Assert(false);
			}
			this.IsInitialized = true;
		}

		/// <summary>Recupera o deslocamento da borda esquerda do <see cref="T:System.Windows.Media.GlyphRun" /> para a borda à esquerda ou à direita de uma parada de circunflexo que contém a ocorrência de caractere especificada.</summary>
		/// <param name="characterHit">O <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> a ser usado para calcular o deslocamento.</param>
		/// <returns>Um <see cref="T:System.Double" /> que representa o deslocamento da borda à esquerda do <see cref="T:System.Windows.Media.GlyphRun" /> para a borda à esquerda ou à direita de uma parada de circunflexo que contém a ocorrência de caractere.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A ocorrência de caractere está fora do intervalo especificado pela cadeia de caracteres Unicode <see cref="T:System.Windows.Media.GlyphRun" />.</exception>
		// Token: 0x0600292E RID: 10542 RVA: 0x000A5220 File Offset: 0x000A4620
		public double GetDistanceFromCaretCharacterHit(CharacterHit characterHit)
		{
			this.CheckInitialized();
			IList<bool> list2;
			if (this.CaretStops == null || this.CaretStops.Count == 0)
			{
				IList<bool> list = new GlyphRun.DefaultCaretStopList(this.CodepointCount);
				list2 = list;
			}
			else
			{
				list2 = this.CaretStops;
			}
			IList<bool> caretStops = list2;
			if (characterHit.FirstCharacterIndex < 0 || characterHit.FirstCharacterIndex > this.CodepointCount)
			{
				throw new ArgumentOutOfRangeException("characterHit");
			}
			int num;
			int num2;
			this.FindNearestCaretStop(characterHit.FirstCharacterIndex, caretStops, out num, out num2);
			if (num == -1)
			{
				return 0.0;
			}
			if (num2 == -1 && characterHit.TrailingLength != 0)
			{
				return 0.0;
			}
			int num3 = (characterHit.TrailingLength == 0) ? num : (num + num2);
			double num4 = 0.0;
			IList<ushort> list3 = this.ClusterMap;
			if (list3 == null)
			{
				list3 = new GlyphRun.DefaultClusterMap(this.CodepointCount);
			}
			int num5 = 0;
			int num6 = num5;
			IList<double> advanceWidths = this.AdvanceWidths;
			double num7;
			for (;;)
			{
				num6++;
				if (num6 >= list3.Count || list3[num6] != list3[num5])
				{
					num7 = 0.0;
					int num8;
					if (num6 >= list3.Count)
					{
						num8 = advanceWidths.Count;
					}
					else
					{
						num8 = (int)list3[num6];
					}
					for (int i = (int)list3[num5]; i < num8; i++)
					{
						num7 += advanceWidths[i];
					}
					if (num3 < num6 || num6 >= list3.Count)
					{
						break;
					}
					num4 += num7;
					num5 = num6;
				}
			}
			num7 *= (double)(num3 - num5) / (double)(num6 - num5);
			return num4 + num7;
		}

		/// <summary>Recupera o valor <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> que representa a ocorrência do caractere do circunflexo do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <param name="distance">Deslocamento a ser usado para calcular a ocorrência do circunflexo.</param>
		/// <param name="isInside">Determina se a ocorrência do caractere está dentro do <see cref="T:System.Windows.Media.GlyphRun" />.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> que representa a ocorrências de caractere mais próxima do valor <paramref name="distance" />. O parâmetro de saída <paramref name="isInside" /> retorna <see langword="true" /> se a ocorrência do caractere está dentro do <see cref="T:System.Windows.Media.GlyphRun" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600292F RID: 10543 RVA: 0x000A539C File Offset: 0x000A479C
		public CharacterHit GetCaretCharacterHitFromDistance(double distance, out bool isInside)
		{
			this.CheckInitialized();
			IList<double> advanceWidths = this.AdvanceWidths;
			IList<bool> list2;
			if (this.CaretStops == null || this.CaretStops.Count == 0)
			{
				IList<bool> list = new GlyphRun.DefaultCaretStopList(this.CodepointCount);
				list2 = list;
			}
			else
			{
				list2 = this.CaretStops;
			}
			IList<bool> list3 = list2;
			IList<ushort> list4 = this.ClusterMap;
			if (list4 == null)
			{
				list4 = new GlyphRun.DefaultClusterMap(this.CodepointCount);
			}
			int num = -1;
			double num2 = 0.0;
			int num3 = -1;
			double num4 = 0.0;
			int num5 = 0;
			for (int i = 1; i < list3.Count; i++)
			{
				if (i >= list4.Count || list4[i] != list4[num5])
				{
					ushort num6 = (i < list4.Count) ? list4[i] : ((ushort)advanceWidths.Count);
					double num7 = 0.0;
					for (int j = (int)list4[num5]; j < (int)num6; j++)
					{
						num7 += advanceWidths[j];
					}
					num7 /= (double)(i - num5);
					for (int k = num5; k < i; k++)
					{
						if (list3[k])
						{
							if (num4 > distance)
							{
								num3 = k;
								goto IL_152;
							}
							num = k;
							num2 = num4;
						}
						num4 += num7;
					}
					num5 = i;
					goto IL_123;
					IL_152:
					if (num == -1 && num3 == -1)
					{
						isInside = false;
						if (list3[list3.Count - 1])
						{
							return new CharacterHit(list3.Count - 1, 0);
						}
						return new CharacterHit(0, 0);
					}
					else
					{
						if (num == -1)
						{
							isInside = false;
							return new CharacterHit(num3, 0);
						}
						if (num3 == -1)
						{
							isInside = false;
							return new CharacterHit(num, list3.Count - 1 - num);
						}
						isInside = true;
						if (distance <= (num2 + num4) / 2.0)
						{
							return new CharacterHit(num, 0);
						}
						return new CharacterHit(num, num3 - num);
					}
				}
				IL_123:;
			}
			if (list3[list3.Count - 1] && num4 > distance)
			{
				num3 = list3.Count - 1;
				goto IL_152;
			}
			goto IL_152;
		}

		/// <summary>Recupera a próxima ocorrência de circunflexo válida na direção lógica no <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <param name="characterHit">O <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> a ser usado para calcular o próximo valor de ocorrência.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> que representa a próxima ocorrência de circunflexo válida na direção lógica. Se o valor retornado é igual a <paramref name="characterHit" />, nenhuma navegação adicional é possível no <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x06002930 RID: 10544 RVA: 0x000A5580 File Offset: 0x000A4980
		public CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit)
		{
			this.CheckInitialized();
			IList<bool> list2;
			if (this.CaretStops == null || this.CaretStops.Count == 0)
			{
				IList<bool> list = new GlyphRun.DefaultCaretStopList(this.CodepointCount);
				list2 = list;
			}
			else
			{
				list2 = this.CaretStops;
			}
			IList<bool> caretStops = list2;
			if (characterHit.FirstCharacterIndex < 0 || characterHit.FirstCharacterIndex > this.CodepointCount)
			{
				throw new ArgumentOutOfRangeException("characterHit");
			}
			int num;
			int num2;
			this.FindNearestCaretStop(characterHit.FirstCharacterIndex, caretStops, out num, out num2);
			if (num == -1 || num2 == -1)
			{
				return characterHit;
			}
			if (characterHit.TrailingLength == 0)
			{
				return new CharacterHit(num, num2);
			}
			int firstCharacterIndex;
			int num3;
			this.FindNearestCaretStop(num + num2, caretStops, out firstCharacterIndex, out num3);
			if (num3 == -1)
			{
				return characterHit;
			}
			return new CharacterHit(firstCharacterIndex, num3);
		}

		/// <summary>Recupera a ocorrência de circunflexo válida anterior na direção lógica no <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <param name="characterHit">O <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> a ser usado para calcular o valor de ocorrência anterior.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> que representa a ocorrência de circunflexo válida anterior na direção lógica. Se o valor retornado é igual a <paramref name="characterHit" />, nenhuma navegação adicional é possível no <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x06002931 RID: 10545 RVA: 0x000A5630 File Offset: 0x000A4A30
		public CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit)
		{
			this.CheckInitialized();
			IList<bool> list2;
			if (this.CaretStops == null || this.CaretStops.Count == 0)
			{
				IList<bool> list = new GlyphRun.DefaultCaretStopList(this.CodepointCount);
				list2 = list;
			}
			else
			{
				list2 = this.CaretStops;
			}
			IList<bool> caretStops = list2;
			if (characterHit.FirstCharacterIndex < 0 || characterHit.FirstCharacterIndex > this.CodepointCount)
			{
				throw new ArgumentOutOfRangeException("characterHit");
			}
			int num;
			int num2;
			this.FindNearestCaretStop(characterHit.FirstCharacterIndex, caretStops, out num, out num2);
			if (num == -1)
			{
				return characterHit;
			}
			if (characterHit.TrailingLength != 0)
			{
				return new CharacterHit(num, 0);
			}
			int num3;
			this.FindNearestCaretStop(num - 1, caretStops, out num3, out num2);
			if (num3 == -1 || num3 == num)
			{
				return characterHit;
			}
			return new CharacterHit(num3, 0);
		}

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.Media.GlyphRun.PixelsPerDip" /> atual.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x000A56DC File Offset: 0x000A4ADC
		// (set) Token: 0x06002933 RID: 10547 RVA: 0x000A56F8 File Offset: 0x000A4AF8
		public float PixelsPerDip
		{
			get
			{
				this.CheckInitialized();
				return this._pixelsPerDip;
			}
			set
			{
				this.CheckInitializing();
				this._pixelsPerDip = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x000A5714 File Offset: 0x000A4B14
		private double AdvanceWidth
		{
			get
			{
				double num = 0.0;
				if (this._advanceWidths != null)
				{
					foreach (double num2 in this._advanceWidths)
					{
						num += num2;
					}
				}
				return num;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x000A5780 File Offset: 0x000A4B80
		private double Ascent
		{
			get
			{
				if (this.IsSideways)
				{
					return this._renderingEmSize * this._glyphTypeface.Height / 2.0;
				}
				return this._glyphTypeface.Baseline * this._renderingEmSize;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000A57C4 File Offset: 0x000A4BC4
		private double Height
		{
			get
			{
				return this._glyphTypeface.Height * this._renderingEmSize;
			}
		}

		/// <summary>Obtém ou define a origem de linha de base do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Point" /> valor que representa a origem de linha de base.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000A57E4 File Offset: 0x000A4BE4
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x000A5800 File Offset: 0x000A4C00
		public Point BaselineOrigin
		{
			get
			{
				this.CheckInitialized();
				return this._baselineOrigin;
			}
			set
			{
				this.CheckInitializing();
				this._baselineOrigin = value;
			}
		}

		/// <summary>Obtém ou define o tamanho em usado para renderizar a <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa o tamanho em usado para renderização.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000A581C File Offset: 0x000A4C1C
		// (set) Token: 0x0600293A RID: 10554 RVA: 0x000A5838 File Offset: 0x000A4C38
		public double FontRenderingEmSize
		{
			get
			{
				this.CheckInitialized();
				return this._renderingEmSize;
			}
			set
			{
				this.CheckInitializing();
				this._renderingEmSize = value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.GlyphTypeface" /> do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.GlyphTypeface" /> para o <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000A5854 File Offset: 0x000A4C54
		// (set) Token: 0x0600293C RID: 10556 RVA: 0x000A5870 File Offset: 0x000A4C70
		public GlyphTypeface GlyphTypeface
		{
			get
			{
				this.CheckInitialized();
				return this._glyphTypeface;
			}
			set
			{
				this.CheckInitializing();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._glyphTypeface = value;
			}
		}

		/// <summary>Obtém ou define o nível de aninhamento bidirecional do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o nível de aninhamento bidirecional.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x000A5898 File Offset: 0x000A4C98
		// (set) Token: 0x0600293E RID: 10558 RVA: 0x000A58B4 File Offset: 0x000A4CB4
		public int BidiLevel
		{
			get
			{
				this.CheckInitialized();
				return this._bidiLevel;
			}
			set
			{
				this.CheckInitializing();
				this._bidiLevel = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000A58D0 File Offset: 0x000A4CD0
		private bool IsLeftToRight
		{
			get
			{
				return (this._bidiLevel & 1) == 0;
			}
		}

		/// <summary>Obtém ou define um valor que indica se a rotação de glifos deve ser realizada.</summary>
		/// <returns>
		///   <see langword="true" /> Se os glifos são girados; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000A58E8 File Offset: 0x000A4CE8
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x000A5908 File Offset: 0x000A4D08
		public bool IsSideways
		{
			get
			{
				this.CheckInitialized();
				return (this._flags & GlyphRun.GlyphRunFlags.IsSideways) > GlyphRun.GlyphRunFlags.None;
			}
			set
			{
				this.CheckInitializing();
				if (value)
				{
					this._flags |= GlyphRun.GlyphRunFlags.IsSideways;
					return;
				}
				this._flags &= ~GlyphRun.GlyphRunFlags.IsSideways;
			}
		}

		/// <summary>Obtém ou define a lista de valores <see cref="T:System.Boolean" /> que determinam se há paradas de circunflexo para cada ponto de código UTF16 no Unicode representando o <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Uma lista de <see cref="T:System.Boolean" /> valores que representam se há paradas de circunflexo.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000A5940 File Offset: 0x000A4D40
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x000A595C File Offset: 0x000A4D5C
		[TypeConverter(typeof(BoolIListConverter))]
		[CLSCompliant(false)]
		public IList<bool> CaretStops
		{
			get
			{
				this.CheckInitialized();
				return this._caretStops;
			}
			set
			{
				this.CheckInitializing();
				this._caretStops = value;
			}
		}

		/// <summary>Obtém um valor que indica se há quaisquer ocorrências de circunflexo válidas dentro do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se não houver ocorrências de circunflexo válidas; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000A5978 File Offset: 0x000A4D78
		public bool IsHitTestable
		{
			get
			{
				this.CheckInitialized();
				if (this.CaretStops == null || this.CaretStops.Count == 0)
				{
					return true;
				}
				foreach (bool flag in this.CaretStops)
				{
					if (flag)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Obtém ou define a lista de valores <see cref="T:System.UInt16" /> que mapeiam caracteres no <see cref="T:System.Windows.Media.GlyphRun" /> para índices de glifo.</summary>
		/// <returns>Uma lista de <see cref="T:System.UInt16" /> valores que representam os índices de glifo mapeada.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000A59F4 File Offset: 0x000A4DF4
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x000A5A10 File Offset: 0x000A4E10
		[CLSCompliant(false)]
		[TypeConverter(typeof(UShortIListConverter))]
		public IList<ushort> ClusterMap
		{
			get
			{
				this.CheckInitialized();
				return this._clusterMap;
			}
			set
			{
				this.CheckInitializing();
				this._clusterMap = value;
			}
		}

		/// <summary>Obtém ou define a lista de pontos de código UTF16 que representam o conteúdo Unicode do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Uma lista de <see cref="T:System.Char" /> valores que representam o conteúdo Unicode.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000A5A2C File Offset: 0x000A4E2C
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x000A5A48 File Offset: 0x000A4E48
		[CLSCompliant(false)]
		[TypeConverter(typeof(CharIListConverter))]
		public IList<char> Characters
		{
			get
			{
				this.CheckInitialized();
				return this._characters;
			}
			set
			{
				this.CheckInitializing();
				this._characters = value;
			}
		}

		/// <summary>Obtém ou define uma matriz de valores <see cref="T:System.UInt16" /> que representam os índices de glifo na fonte física de renderização.</summary>
		/// <returns>Uma lista de <see cref="T:System.UInt16" /> valores que representam os índices de glifo.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000A5A64 File Offset: 0x000A4E64
		// (set) Token: 0x0600294A RID: 10570 RVA: 0x000A5A80 File Offset: 0x000A4E80
		[TypeConverter(typeof(UShortIListConverter))]
		[CLSCompliant(false)]
		public IList<ushort> GlyphIndices
		{
			get
			{
				this.CheckInitialized();
				return this._glyphIndices;
			}
			set
			{
				this.CheckInitializing();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Count <= 0)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeGreaterThanZero"), "value");
				}
				this._glyphIndices = value;
			}
		}

		/// <summary>Obtém ou define a lista de valores <see cref="T:System.Double" /> que representam as larguras de avanço correspondentes aos índices de glifo.</summary>
		/// <returns>Uma lista de <see cref="T:System.Double" /> valores que representam as larguras de avanço.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000A5AC8 File Offset: 0x000A4EC8
		// (set) Token: 0x0600294C RID: 10572 RVA: 0x000A5AE4 File Offset: 0x000A4EE4
		[CLSCompliant(false)]
		[TypeConverter(typeof(DoubleIListConverter))]
		public IList<double> AdvanceWidths
		{
			get
			{
				this.CheckInitialized();
				return this._advanceWidths;
			}
			set
			{
				this.CheckInitializing();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Count <= 0)
				{
					throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeGreaterThanZero"), "value");
				}
				this._advanceWidths = value;
			}
		}

		/// <summary>Obtém ou define uma matriz de valores <see cref="T:System.Windows.Point" /> que representam os deslocamentos de glifos no <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Uma lista de <see cref="T:System.Windows.Point" /> valores que representam os deslocamentos de glifo.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000A5B2C File Offset: 0x000A4F2C
		// (set) Token: 0x0600294E RID: 10574 RVA: 0x000A5B48 File Offset: 0x000A4F48
		[CLSCompliant(false)]
		[TypeConverter(typeof(PointIListConverter))]
		public IList<Point> GlyphOffsets
		{
			get
			{
				this.CheckInitialized();
				return this._glyphOffsets;
			}
			set
			{
				this.CheckInitializing();
				this._glyphOffsets = value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Markup.XmlLanguage" /> do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Markup.XmlLanguage" /> para o <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000A5B64 File Offset: 0x000A4F64
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x000A5B80 File Offset: 0x000A4F80
		public XmlLanguage Language
		{
			get
			{
				this.CheckInitialized();
				return this._language;
			}
			set
			{
				this.CheckInitializing();
				this._language = value;
			}
		}

		/// <summary>Obtém ou define a fonte do dispositivo específica para a qual o <see cref="T:System.Windows.Media.GlyphRun" /> foi otimizado.</summary>
		/// <returns>Um <see cref="T:System.String" /> valor que representa a fonte do dispositivo.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000A5B9C File Offset: 0x000A4F9C
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x000A5BB8 File Offset: 0x000A4FB8
		public string DeviceFontName
		{
			get
			{
				this.CheckInitialized();
				return this._deviceFontName;
			}
			set
			{
				this.CheckInitializing();
				this._deviceFontName = value;
			}
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000A5BD4 File Offset: 0x000A4FD4
		internal Point GetGlyphOffset(int i)
		{
			if (this._glyphOffsets == null || this._glyphOffsets.Count == 0)
			{
				return new Point(0.0, 0.0);
			}
			return this._glyphOffsets[i];
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x000A5C1C File Offset: 0x000A501C
		internal int GlyphCount
		{
			get
			{
				return this._glyphIndices.Count;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000A5C34 File Offset: 0x000A5034
		internal int CodepointCount
		{
			get
			{
				if (this._characters != null && this._characters.Count != 0)
				{
					return this._characters.Count;
				}
				if (this._clusterMap != null && this._clusterMap.Count != 0)
				{
					return this._clusterMap.Count;
				}
				return this._glyphIndices.Count;
			}
		}

		/// <summary>Recupera a caixa delimitadora de tinta do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Obtém um <see cref="T:System.Windows.Rect" /> que representa a caixa delimitadora do <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x06002956 RID: 10582 RVA: 0x000A5C90 File Offset: 0x000A5090
		public Rect ComputeInkBoundingBox()
		{
			this.CheckInitialized();
			if ((this._flags & GlyphRun.GlyphRunFlags.CacheInkBounds) != GlyphRun.GlyphRunFlags.None && this._inkBoundingBox != null)
			{
				return (Rect)this._inkBoundingBox;
			}
			int count = this._glyphIndices.Count;
			ushort[] ushorts = BufferCache.GetUShorts(count);
			this._glyphIndices.CopyTo(ushorts, 0);
			GlyphMetrics[] glyphMetrics = BufferCache.GetGlyphMetrics(count);
			this._glyphTypeface.GetGlyphMetrics(ushorts, count, this._renderingEmSize, this._pixelsPerDip, this._textFormattingMode, this.IsSideways, glyphMetrics);
			BufferCache.ReleaseUShorts(ushorts);
			Rect rect;
			if (this.IsLeftToRight && !this.IsSideways)
			{
				rect = this.ComputeInkBoundingBoxLtoR(glyphMetrics);
			}
			else
			{
				double num = 0.0;
				double num2 = double.PositiveInfinity;
				double num3 = double.PositiveInfinity;
				double num4 = double.NegativeInfinity;
				double num5 = double.NegativeInfinity;
				double designToEm = this._renderingEmSize / (double)this._glyphTypeface.DesignEmHeight;
				for (int i = 0; i < this.GlyphCount; i++)
				{
					GlyphRun.EmGlyphMetrics emGlyphMetrics = new GlyphRun.EmGlyphMetrics(glyphMetrics[i], designToEm, (double)this._pixelsPerDip, this._textFormattingMode);
					if (TextFormattingMode.Display == this._textFormattingMode)
					{
						emGlyphMetrics.AdvanceHeight = this.AdjustAdvanceForDisplayLayout(emGlyphMetrics.AdvanceHeight, emGlyphMetrics.TopSideBearing, emGlyphMetrics.BottomSideBearing);
						emGlyphMetrics.AdvanceWidth = this.AdjustAdvanceForDisplayLayout(emGlyphMetrics.AdvanceWidth, emGlyphMetrics.LeftSideBearing, emGlyphMetrics.RightSideBearing);
					}
					Point glyphOffset = this.GetGlyphOffset(i);
					double num6;
					if (this.IsLeftToRight)
					{
						num6 = num + glyphOffset.X;
					}
					else
					{
						num6 = -num - (emGlyphMetrics.AdvanceWidth + glyphOffset.X);
					}
					num += this._advanceWidths[i];
					double num7 = -glyphOffset.Y;
					double num8;
					double num9;
					double num10;
					double num11;
					if (this.IsSideways)
					{
						num7 += emGlyphMetrics.AdvanceWidth / 2.0;
						num8 = num7 - emGlyphMetrics.LeftSideBearing;
						num9 = num7 - emGlyphMetrics.AdvanceWidth + emGlyphMetrics.RightSideBearing;
						num10 = num6 + emGlyphMetrics.TopSideBearing;
						num11 = num10 + emGlyphMetrics.AdvanceHeight - emGlyphMetrics.TopSideBearing - emGlyphMetrics.BottomSideBearing;
					}
					else
					{
						num10 = num6 + emGlyphMetrics.LeftSideBearing;
						num11 = num6 + emGlyphMetrics.AdvanceWidth - emGlyphMetrics.RightSideBearing;
						num8 = num7 + emGlyphMetrics.Baseline;
						num9 = num8 - emGlyphMetrics.AdvanceHeight + emGlyphMetrics.TopSideBearing + emGlyphMetrics.BottomSideBearing;
					}
					if (num10 + 1E-07 < num11 && num9 + 1E-07 < num8)
					{
						if (num2 > num10)
						{
							num2 = num10;
						}
						if (num3 > num9)
						{
							num3 = num9;
						}
						if (num4 < num11)
						{
							num4 = num11;
						}
						if (num5 < num8)
						{
							num5 = num8;
						}
					}
				}
				if (num2 > num4)
				{
					rect = Rect.Empty;
				}
				else
				{
					rect = new Rect(num2, num3, num4 - num2, num5 - num3);
				}
			}
			BufferCache.ReleaseGlyphMetrics(glyphMetrics);
			if (CoreCompatibilityPreferences.GetIncludeAllInkInBoundingBox())
			{
				if (!rect.IsEmpty)
				{
					double num12 = Math.Min(this._renderingEmSize / 7.0, 1.0);
					rect.Inflate(num12, num12);
				}
			}
			else if (TextFormattingMode.Display == this._textFormattingMode && !rect.IsEmpty)
			{
				rect.Inflate(1.0, 1.0);
			}
			if ((this._flags & GlyphRun.GlyphRunFlags.CacheInkBounds) != GlyphRun.GlyphRunFlags.None)
			{
				this._inkBoundingBox = rect;
			}
			return rect;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000A5FE4 File Offset: 0x000A53E4
		private double AdjustAdvanceForDisplayLayout(double advance, double oneSideBearing, double otherSideBearing)
		{
			return Math.Max(advance, oneSideBearing + otherSideBearing + 1.0);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000A6004 File Offset: 0x000A5404
		private Rect ComputeInkBoundingBoxLtoR(GlyphMetrics[] glyphMetrics)
		{
			double num = double.PositiveInfinity;
			double num2 = double.PositiveInfinity;
			double num3 = double.NegativeInfinity;
			double num4 = double.NegativeInfinity;
			double num5 = 0.0;
			double designToEm = this._renderingEmSize / (double)this._glyphTypeface.DesignEmHeight;
			int glyphCount = this.GlyphCount;
			for (int i = 0; i < glyphCount; i++)
			{
				GlyphRun.EmGlyphMetrics emGlyphMetrics = new GlyphRun.EmGlyphMetrics(glyphMetrics[i], designToEm, (double)this._pixelsPerDip, this._textFormattingMode);
				if (TextFormattingMode.Display == this._textFormattingMode)
				{
					emGlyphMetrics.AdvanceHeight = this.AdjustAdvanceForDisplayLayout(emGlyphMetrics.AdvanceHeight, emGlyphMetrics.TopSideBearing, emGlyphMetrics.BottomSideBearing);
					emGlyphMetrics.AdvanceWidth = this.AdjustAdvanceForDisplayLayout(emGlyphMetrics.AdvanceWidth, emGlyphMetrics.LeftSideBearing, emGlyphMetrics.RightSideBearing);
				}
				if (this.GlyphOffsets != null)
				{
					Point glyphOffset = this.GetGlyphOffset(i);
					double num6 = num5 + glyphOffset.X;
					num5 += this._advanceWidths[i];
					double num7 = -glyphOffset.Y;
					double num8 = num6 + emGlyphMetrics.LeftSideBearing;
					double num9 = num6 + emGlyphMetrics.AdvanceWidth - emGlyphMetrics.RightSideBearing;
					double num10 = num7 + emGlyphMetrics.Baseline;
					double num11 = num10 - emGlyphMetrics.AdvanceHeight + emGlyphMetrics.TopSideBearing + emGlyphMetrics.BottomSideBearing;
					if (num8 + 1E-07 < num9 && num11 + 1E-07 < num10)
					{
						if (num > num8)
						{
							num = num8;
						}
						if (num2 > num11)
						{
							num2 = num11;
						}
						if (num3 < num9)
						{
							num3 = num9;
						}
						if (num4 < num10)
						{
							num4 = num10;
						}
					}
				}
				else
				{
					double num12 = num5 + emGlyphMetrics.LeftSideBearing;
					double num13 = num5 + emGlyphMetrics.AdvanceWidth - emGlyphMetrics.RightSideBearing;
					double num14 = emGlyphMetrics.Baseline - emGlyphMetrics.AdvanceHeight + emGlyphMetrics.TopSideBearing + emGlyphMetrics.BottomSideBearing;
					num5 += this._advanceWidths[i];
					if (num12 + 1E-07 < num13 && num14 + 1E-07 < emGlyphMetrics.Baseline)
					{
						if (num > num12)
						{
							num = num12;
						}
						if (num2 > num14)
						{
							num2 = num14;
						}
						if (num3 < num13)
						{
							num3 = num13;
						}
						if (num4 < emGlyphMetrics.Baseline)
						{
							num4 = emGlyphMetrics.Baseline;
						}
					}
				}
			}
			if (num > num3)
			{
				return Rect.Empty;
			}
			return new Rect(num, num2, num3 - num, num4 - num2);
		}

		/// <summary>Recupera o <see cref="T:System.Windows.Media.Geometry" /> para o <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> correspondente ao <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x06002959 RID: 10585 RVA: 0x000A625C File Offset: 0x000A565C
		public Geometry BuildGeometry()
		{
			this.CheckInitialized();
			GeometryGroup geometryGroup = null;
			double num = 0.0;
			for (int i = 0; i < this.GlyphCount; i++)
			{
				ushort num2 = this._glyphIndices[i];
				double num3;
				if (this.IsLeftToRight)
				{
					num3 = num;
					num3 += this.GetGlyphOffset(i).X;
				}
				else
				{
					double num4 = TextFormatterImp.RoundDip(this._glyphTypeface.GetAdvanceWidth(num2, this._pixelsPerDip, this._textFormattingMode, this.IsSideways) * this._renderingEmSize, (double)this._pixelsPerDip, this._textFormattingMode);
					num3 = -num;
					num3 -= num4 + this.GetGlyphOffset(i).X;
				}
				num += this._advanceWidths[i];
				double num5 = -this.GetGlyphOffset(i).Y;
				Geometry geometry = this._glyphTypeface.ComputeGlyphOutline(num2, this.IsSideways, this._renderingEmSize);
				if (!geometry.IsEmpty())
				{
					geometry.Transform = new TranslateTransform(num3 + this._baselineOrigin.X, num5 + this._baselineOrigin.Y);
					if (geometryGroup == null)
					{
						geometryGroup = new GeometryGroup();
						geometryGroup.FillRule = FillRule.Nonzero;
					}
					geometryGroup.Children.Add(geometry.GetOutlinedPathGeometry(GlyphRun.RelativeFlatteningTolerance, ToleranceType.Relative));
				}
			}
			if (geometryGroup == null || geometryGroup.IsEmpty())
			{
				return Geometry.Empty;
			}
			return geometryGroup;
		}

		/// <summary>Recupera a caixa de alinhamento para o <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que representa a caixa de alinhamento do <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x0600295A RID: 10586 RVA: 0x000A63BC File Offset: 0x000A57BC
		public Rect ComputeAlignmentBox()
		{
			this.CheckInitialized();
			double num = this.AdvanceWidth;
			bool flag = this.IsLeftToRight;
			if (num < 0.0)
			{
				flag = !flag;
				num = -num;
			}
			if (flag)
			{
				return new Rect(0.0, -this.Ascent, num, this.Height);
			}
			return new Rect(-num, -this.Ascent, num, this.Height);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000A6428 File Offset: 0x000A5828
		internal void EmitBackground(DrawingContext dc, Brush backgroundBrush)
		{
			double advanceWidth;
			if (backgroundBrush != null && (advanceWidth = this.AdvanceWidth) > 0.0)
			{
				Rect rectangle;
				if (this.IsLeftToRight)
				{
					rectangle = new Rect(this._baselineOrigin.X, this._baselineOrigin.Y - this.Ascent, advanceWidth, this.Height);
				}
				else
				{
					rectangle = new Rect(this._baselineOrigin.X - advanceWidth, this._baselineOrigin.Y - this.Ascent, advanceWidth, this.Height);
				}
				dc.DrawRectangle(backgroundBrush, null, rectangle);
			}
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000A64BC File Offset: 0x000A58BC
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			this.CheckInitialized();
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._mcr.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GLYPHRUN))
				{
					this.CreateOnChannel(channel);
				}
				handle = this._mcr.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000A6528 File Offset: 0x000A5928
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			this.CheckInitialized();
			using (CompositionEngineLock.Acquire())
			{
				this._mcr.ReleaseOnChannel(channel);
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000A657C File Offset: 0x000A597C
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000A6590 File Offset: 0x000A5990
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000A65A4 File Offset: 0x000A59A4
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			this.CheckInitialized();
			return this._mcr.GetHandle(channel);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000A65C4 File Offset: 0x000A59C4
		int DUCE.IResource.GetChannelCount()
		{
			return this._mcr.GetChannelCount();
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000A65DC File Offset: 0x000A59DC
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._mcr.GetChannel(index);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000A65F8 File Offset: 0x000A59F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void CreateOnChannel(DUCE.Channel channel)
		{
			int glyphCount = this.GlyphCount;
			Rect managedBounds = this.ComputeInkBoundingBox();
			if (!managedBounds.IsEmpty)
			{
				managedBounds.Offset((Vector)this.BaselineOrigin);
			}
			DUCE.MILCMD_GLYPHRUN_CREATE milcmd_GLYPHRUN_CREATE;
			milcmd_GLYPHRUN_CREATE.Type = MILCMD.MilCmdGlyphRunCreate;
			milcmd_GLYPHRUN_CREATE.Handle = this._mcr.GetHandle(channel);
			milcmd_GLYPHRUN_CREATE.GlyphRunFlags = this.ComposeFlags();
			milcmd_GLYPHRUN_CREATE.Origin.X = (float)this._baselineOrigin.X;
			milcmd_GLYPHRUN_CREATE.Origin.Y = (float)this._baselineOrigin.Y;
			milcmd_GLYPHRUN_CREATE.MuSize = (float)this._renderingEmSize;
			milcmd_GLYPHRUN_CREATE.ManagedBounds = managedBounds;
			checked
			{
				milcmd_GLYPHRUN_CREATE.GlyphCount = (ushort)glyphCount;
				milcmd_GLYPHRUN_CREATE.BidiLevel = (ushort)this._bidiLevel;
				milcmd_GLYPHRUN_CREATE.pIDWriteFont = (ulong)((long)this._glyphTypeface.GetDWriteFontAddRef);
			}
			milcmd_GLYPHRUN_CREATE.DWriteTextMeasuringMethod = (ushort)DWriteTypeConverter.Convert(this._textFormattingMode);
			int num = glyphCount * 2;
			num += glyphCount * 4;
			if (this._glyphOffsets != null && this._glyphOffsets.Count != 0)
			{
				num += glyphCount * 8;
			}
			channel.BeginCommand((byte*)(&milcmd_GLYPHRUN_CREATE), sizeof(DUCE.MILCMD_GLYPHRUN_CREATE), num);
			if (glyphCount <= 512)
			{
				ushort* ptr = stackalloc ushort[checked(unchecked((UIntPtr)glyphCount) * 2)];
				for (int i = 0; i < glyphCount; i++)
				{
					ptr[i] = this._glyphIndices[i];
				}
				channel.AppendCommandData((byte*)ptr, glyphCount * 2);
			}
			else
			{
				for (int j = 0; j < glyphCount; j++)
				{
					ushort num2 = this._glyphIndices[j];
					channel.AppendCommandData((byte*)(&num2), 2);
				}
			}
			if (glyphCount <= 256)
			{
				float* ptr2 = stackalloc float[checked(unchecked((UIntPtr)glyphCount) * 4)];
				for (int k = 0; k < glyphCount; k++)
				{
					ptr2[k] = (float)this._advanceWidths[k];
				}
				channel.AppendCommandData((byte*)ptr2, glyphCount * 4);
			}
			else
			{
				for (int l = 0; l < glyphCount; l++)
				{
					float num3 = (float)this._advanceWidths[l];
					channel.AppendCommandData((byte*)(&num3), 4);
				}
			}
			if (this._glyphOffsets != null && this._glyphOffsets.Count != 0)
			{
				if (glyphCount <= 128)
				{
					float* ptr3 = stackalloc float[checked(unchecked((UIntPtr)(2 * glyphCount)) * 4)];
					for (int m = 0; m < glyphCount; m++)
					{
						ptr3[2 * m] = (float)this._glyphOffsets[m].X;
						ptr3[2 * m + 1] = (float)this._glyphOffsets[m].Y;
					}
					channel.AppendCommandData((byte*)ptr3, 2 * glyphCount * 4);
				}
				else
				{
					for (int n = 0; n < glyphCount; n++)
					{
						float num4 = (float)this._glyphOffsets[n].X;
						float num5 = (float)this._glyphOffsets[n].Y;
						channel.AppendCommandData((byte*)(&num4), 4);
						channel.AppendCommandData((byte*)(&num5), 4);
					}
				}
			}
			channel.EndCommand();
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000A68D0 File Offset: 0x000A5CD0
		private ushort ComposeFlags()
		{
			ushort num = 0;
			if (this.IsSideways)
			{
				num |= 1;
			}
			if (this._glyphOffsets != null && this._glyphOffsets.Count != 0)
			{
				num |= 16;
			}
			return num;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000A6908 File Offset: 0x000A5D08
		private void FindNearestCaretStop(int characterIndex, IList<bool> caretStops, out int caretStopIndex, out int codePointsUntilNextStop)
		{
			caretStopIndex = -1;
			codePointsUntilNextStop = -1;
			if (characterIndex < 0 || characterIndex >= caretStops.Count)
			{
				return;
			}
			for (int i = characterIndex; i >= 0; i--)
			{
				if (caretStops[i])
				{
					caretStopIndex = i;
					break;
				}
			}
			if (caretStopIndex == -1)
			{
				for (int j = characterIndex + 1; j < caretStops.Count; j++)
				{
					if (caretStops[j])
					{
						caretStopIndex = j;
						break;
					}
				}
			}
			if (caretStopIndex == -1)
			{
				return;
			}
			for (int k = caretStopIndex + 1; k < caretStops.Count; k++)
			{
				if (caretStops[k])
				{
					codePointsUntilNextStop = k - caretStopIndex;
					return;
				}
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.ComponentModel.ISupportInitialize.BeginInit" />.</summary>
		// Token: 0x06002966 RID: 10598 RVA: 0x000A6998 File Offset: 0x000A5D98
		void ISupportInitialize.BeginInit()
		{
			if (this.IsInitialized)
			{
				throw new InvalidOperationException(SR.Get("OnlyOneInitialization"));
			}
			if (this.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("InInitialization"));
			}
			this.IsInitializing = true;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.ComponentModel.ISupportInitialize.EndInit" />.</summary>
		// Token: 0x06002967 RID: 10599 RVA: 0x000A69DC File Offset: 0x000A5DDC
		void ISupportInitialize.EndInit()
		{
			if (!this.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("NotInInitialization"));
			}
			GlyphTypeface glyphTypeface = this._glyphTypeface;
			int bidiLevel = this._bidiLevel;
			bool isSideways = (this._flags & GlyphRun.GlyphRunFlags.IsSideways) > GlyphRun.GlyphRunFlags.None;
			double renderingEmSize = this._renderingEmSize;
			float pixelsPerDip = this._pixelsPerDip;
			IList<ushort> glyphIndices = this._glyphIndices;
			Point baselineOrigin = this._baselineOrigin;
			IList<double> advanceWidths;
			if (this._advanceWidths != null)
			{
				if (this._textFormattingMode == TextFormattingMode.Ideal)
				{
					IList<double> list = new ThousandthOfEmRealDoubles(this._renderingEmSize, this._advanceWidths);
					advanceWidths = list;
				}
				else
				{
					IList<double> list = new List<double>();
					advanceWidths = list;
				}
			}
			else
			{
				advanceWidths = null;
			}
			IList<Point> glyphOffsets;
			if (this._glyphOffsets != null)
			{
				if (this._textFormattingMode == TextFormattingMode.Ideal)
				{
					IList<Point> list2 = new ThousandthOfEmRealPoints(this._renderingEmSize, this._glyphOffsets);
					glyphOffsets = list2;
				}
				else
				{
					IList<Point> list2 = new List<Point>();
					glyphOffsets = list2;
				}
			}
			else
			{
				glyphOffsets = null;
			}
			this.Initialize(glyphTypeface, bidiLevel, isSideways, renderingEmSize, pixelsPerDip, glyphIndices, baselineOrigin, advanceWidths, glyphOffsets, this._characters, this._deviceFontName, this._clusterMap, this._caretStops, this._language, TextFormattingMode.Ideal);
			this.IsInitializing = false;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000A6ABC File Offset: 0x000A5EBC
		private void CheckInitialized()
		{
			if (!this.IsInitialized)
			{
				throw new InvalidOperationException(SR.Get("InitializationIncomplete"));
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000A6AE4 File Offset: 0x000A5EE4
		private void CheckInitializing()
		{
			if (!this.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("NotInInitialization"));
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000A6B0C File Offset: 0x000A5F0C
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x000A6B28 File Offset: 0x000A5F28
		private bool IsInitializing
		{
			get
			{
				return (this._flags & GlyphRun.GlyphRunFlags.IsInitializing) > GlyphRun.GlyphRunFlags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= GlyphRun.GlyphRunFlags.IsInitializing;
					return;
				}
				this._flags &= ~GlyphRun.GlyphRunFlags.IsInitializing;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000A6B5C File Offset: 0x000A5F5C
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x000A6B74 File Offset: 0x000A5F74
		private bool IsInitialized
		{
			get
			{
				return (this._flags & GlyphRun.GlyphRunFlags.IsInitialized) > GlyphRun.GlyphRunFlags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= GlyphRun.GlyphRunFlags.IsInitialized;
					return;
				}
				this._flags &= ~GlyphRun.GlyphRunFlags.IsInitialized;
			}
		}

		// Token: 0x040012C7 RID: 4807
		private DUCE.MultiChannelResource _mcr;

		// Token: 0x040012C8 RID: 4808
		private Point _baselineOrigin;

		// Token: 0x040012C9 RID: 4809
		private GlyphRun.GlyphRunFlags _flags;

		// Token: 0x040012CA RID: 4810
		private double _renderingEmSize;

		// Token: 0x040012CB RID: 4811
		private IList<ushort> _glyphIndices;

		// Token: 0x040012CC RID: 4812
		private IList<double> _advanceWidths;

		// Token: 0x040012CD RID: 4813
		private IList<Point> _glyphOffsets;

		// Token: 0x040012CE RID: 4814
		private int _bidiLevel;

		// Token: 0x040012CF RID: 4815
		private GlyphTypeface _glyphTypeface;

		// Token: 0x040012D0 RID: 4816
		private IList<char> _characters;

		// Token: 0x040012D1 RID: 4817
		private IList<ushort> _clusterMap;

		// Token: 0x040012D2 RID: 4818
		private IList<bool> _caretStops;

		// Token: 0x040012D3 RID: 4819
		private XmlLanguage _language;

		// Token: 0x040012D4 RID: 4820
		private string _deviceFontName;

		// Token: 0x040012D5 RID: 4821
		private object _inkBoundingBox;

		// Token: 0x040012D6 RID: 4822
		private TextFormattingMode _textFormattingMode;

		// Token: 0x040012D7 RID: 4823
		private float _pixelsPerDip = Util.PixelsPerDip;

		// Token: 0x040012D8 RID: 4824
		private const double Sin20 = 0.34202014332566871;

		// Token: 0x040012D9 RID: 4825
		private const double InkMetricsEpsilon = 1E-07;

		// Token: 0x040012DA RID: 4826
		private const double DefaultFontHintingSize = 12.0;

		// Token: 0x040012DB RID: 4827
		internal static double RelativeFlatteningTolerance = 0.01;

		// Token: 0x040012DC RID: 4828
		internal const int MaxGlyphCount = 65535;

		// Token: 0x040012DD RID: 4829
		internal const int MaxStackAlloc = 1024;

		// Token: 0x02000884 RID: 2180
		private struct EmGlyphMetrics
		{
			// Token: 0x060057CF RID: 22479 RVA: 0x00166D44 File Offset: 0x00166144
			internal EmGlyphMetrics(GlyphMetrics glyphMetrics, double designToEm, double pixelsPerDip, TextFormattingMode textFormattingMode)
			{
				if (TextFormattingMode.Display == textFormattingMode)
				{
					this.AdvanceWidth = TextFormatterImp.RoundDipForDisplayMode(designToEm * glyphMetrics.AdvanceWidth, pixelsPerDip);
					this.AdvanceHeight = TextFormatterImp.RoundDipForDisplayMode(designToEm * glyphMetrics.AdvanceHeight, pixelsPerDip);
					this.LeftSideBearing = TextFormatterImp.RoundDipForDisplayMode(designToEm * (double)glyphMetrics.LeftSideBearing, pixelsPerDip);
					this.RightSideBearing = TextFormatterImp.RoundDipForDisplayMode(designToEm * (double)glyphMetrics.RightSideBearing, pixelsPerDip);
					this.TopSideBearing = TextFormatterImp.RoundDipForDisplayMode(designToEm * (double)glyphMetrics.TopSideBearing, pixelsPerDip);
					this.BottomSideBearing = TextFormatterImp.RoundDipForDisplayMode(designToEm * (double)glyphMetrics.BottomSideBearing, pixelsPerDip);
					this.Baseline = TextFormatterImp.RoundDipForDisplayMode(designToEm * GlyphTypeface.BaselineHelper(glyphMetrics), pixelsPerDip);
					return;
				}
				this.AdvanceWidth = designToEm * glyphMetrics.AdvanceWidth;
				this.AdvanceHeight = designToEm * glyphMetrics.AdvanceHeight;
				this.LeftSideBearing = designToEm * (double)glyphMetrics.LeftSideBearing;
				this.RightSideBearing = designToEm * (double)glyphMetrics.RightSideBearing;
				this.TopSideBearing = designToEm * (double)glyphMetrics.TopSideBearing;
				this.BottomSideBearing = designToEm * (double)glyphMetrics.BottomSideBearing;
				this.Baseline = designToEm * GlyphTypeface.BaselineHelper(glyphMetrics);
			}

			// Token: 0x040028BE RID: 10430
			internal double LeftSideBearing;

			// Token: 0x040028BF RID: 10431
			internal double AdvanceWidth;

			// Token: 0x040028C0 RID: 10432
			internal double RightSideBearing;

			// Token: 0x040028C1 RID: 10433
			internal double TopSideBearing;

			// Token: 0x040028C2 RID: 10434
			internal double AdvanceHeight;

			// Token: 0x040028C3 RID: 10435
			internal double BottomSideBearing;

			// Token: 0x040028C4 RID: 10436
			internal double Baseline;
		}

		// Token: 0x02000885 RID: 2181
		internal struct Scale
		{
			// Token: 0x060057D0 RID: 22480 RVA: 0x00166E58 File Offset: 0x00166258
			internal Scale(ref Matrix matrix)
			{
				double m = matrix.M11;
				double m2 = matrix.M12;
				double m3 = matrix.M21;
				double m4 = matrix.M22;
				this._baseVectorX = Math.Sqrt(m * m + m2 * m2);
				if (DoubleUtil.IsNaN(this._baseVectorX))
				{
					this._baseVectorX = 0.0;
				}
				this._baseVectorY = ((this._baseVectorX == 0.0) ? 0.0 : (Math.Abs(m * m4 - m2 * m3) / this._baseVectorX));
				if (DoubleUtil.IsNaN(this._baseVectorY))
				{
					this._baseVectorY = 0.0;
				}
			}

			// Token: 0x17001222 RID: 4642
			// (get) Token: 0x060057D1 RID: 22481 RVA: 0x00166F00 File Offset: 0x00166300
			internal bool IsValid
			{
				get
				{
					return this._baseVectorX != 0.0 && this._baseVectorY != 0.0;
				}
			}

			// Token: 0x060057D2 RID: 22482 RVA: 0x00166F34 File Offset: 0x00166334
			internal bool IsSame(ref GlyphRun.Scale scale)
			{
				return this._baseVectorX * 0.999999999 <= scale._baseVectorX && this._baseVectorX * 1.000000001 >= scale._baseVectorX && this._baseVectorY * 0.999999999 <= scale._baseVectorY && this._baseVectorY * 1.000000001 >= scale._baseVectorY;
			}

			// Token: 0x040028C5 RID: 10437
			internal double _baseVectorX;

			// Token: 0x040028C6 RID: 10438
			internal double _baseVectorY;
		}

		// Token: 0x02000886 RID: 2182
		private class DefaultCaretStopList : IList<bool>, ICollection<bool>, IEnumerable<bool>, IEnumerable
		{
			// Token: 0x060057D3 RID: 22483 RVA: 0x00166FA8 File Offset: 0x001663A8
			public DefaultCaretStopList(int codePointCount)
			{
				this._count = codePointCount + 1;
			}

			// Token: 0x060057D4 RID: 22484 RVA: 0x00166FC4 File Offset: 0x001663C4
			public int IndexOf(bool item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057D5 RID: 22485 RVA: 0x00166FD8 File Offset: 0x001663D8
			public void Insert(int index, bool item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001223 RID: 4643
			public bool this[int index]
			{
				get
				{
					return true;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x060057D8 RID: 22488 RVA: 0x00167010 File Offset: 0x00166410
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057D9 RID: 22489 RVA: 0x00167024 File Offset: 0x00166424
			public void Add(bool item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057DA RID: 22490 RVA: 0x00167038 File Offset: 0x00166438
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057DB RID: 22491 RVA: 0x0016704C File Offset: 0x0016644C
			public bool Contains(bool item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057DC RID: 22492 RVA: 0x00167060 File Offset: 0x00166460
			public void CopyTo(bool[] array, int arrayIndex)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001224 RID: 4644
			// (get) Token: 0x060057DD RID: 22493 RVA: 0x00167074 File Offset: 0x00166474
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001225 RID: 4645
			// (get) Token: 0x060057DE RID: 22494 RVA: 0x00167088 File Offset: 0x00166488
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060057DF RID: 22495 RVA: 0x00167098 File Offset: 0x00166498
			public bool Remove(bool item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057E0 RID: 22496 RVA: 0x001670AC File Offset: 0x001664AC
			IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057E1 RID: 22497 RVA: 0x001670C0 File Offset: 0x001664C0
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040028C7 RID: 10439
			private int _count;
		}

		// Token: 0x02000887 RID: 2183
		private class DefaultClusterMap : IList<ushort>, ICollection<ushort>, IEnumerable<ushort>, IEnumerable
		{
			// Token: 0x060057E2 RID: 22498 RVA: 0x001670D4 File Offset: 0x001664D4
			public DefaultClusterMap(int count)
			{
				this._count = count;
			}

			// Token: 0x060057E3 RID: 22499 RVA: 0x001670F0 File Offset: 0x001664F0
			public int IndexOf(ushort item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057E4 RID: 22500 RVA: 0x00167104 File Offset: 0x00166504
			public void Insert(int index, ushort item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001226 RID: 4646
			public ushort this[int index]
			{
				get
				{
					return (ushort)index;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x060057E7 RID: 22503 RVA: 0x0016713C File Offset: 0x0016653C
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057E8 RID: 22504 RVA: 0x00167150 File Offset: 0x00166550
			public void Add(ushort item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057E9 RID: 22505 RVA: 0x00167164 File Offset: 0x00166564
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057EA RID: 22506 RVA: 0x00167178 File Offset: 0x00166578
			public bool Contains(ushort item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057EB RID: 22507 RVA: 0x0016718C File Offset: 0x0016658C
			public void CopyTo(ushort[] array, int arrayIndex)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001227 RID: 4647
			// (get) Token: 0x060057EC RID: 22508 RVA: 0x001671A0 File Offset: 0x001665A0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17001228 RID: 4648
			// (get) Token: 0x060057ED RID: 22509 RVA: 0x001671B4 File Offset: 0x001665B4
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060057EE RID: 22510 RVA: 0x001671C4 File Offset: 0x001665C4
			public bool Remove(ushort item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057EF RID: 22511 RVA: 0x001671D8 File Offset: 0x001665D8
			IEnumerator<ushort> IEnumerable<ushort>.GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057F0 RID: 22512 RVA: 0x001671EC File Offset: 0x001665EC
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040028C8 RID: 10440
			private int _count;
		}

		// Token: 0x02000888 RID: 2184
		[Flags]
		private enum GlyphRunFlags : byte
		{
			// Token: 0x040028CA RID: 10442
			None = 0,
			// Token: 0x040028CB RID: 10443
			IsSideways = 1,
			// Token: 0x040028CC RID: 10444
			IsInitialized = 8,
			// Token: 0x040028CD RID: 10445
			IsInitializing = 16,
			// Token: 0x040028CE RID: 10446
			CacheInkBounds = 32
		}
	}
}
