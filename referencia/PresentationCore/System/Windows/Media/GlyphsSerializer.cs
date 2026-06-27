using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000450 RID: 1104
	[FriendAccessAllowed]
	internal class GlyphsSerializer
	{
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B70DC File Offset: 0x000B64DC
		public GlyphsSerializer(GlyphRun glyphRun)
		{
			if (glyphRun == null)
			{
				throw new ArgumentNullException("glyphRun");
			}
			this._glyphTypeface = glyphRun.GlyphTypeface;
			this._milToEm = 100.0 / glyphRun.FontRenderingEmSize;
			this._sideways = glyphRun.IsSideways;
			this._characters = glyphRun.Characters;
			this._caretStops = glyphRun.CaretStops;
			this._clusters = glyphRun.ClusterMap;
			if (this._clusters != null)
			{
				this._glyphClusterInitialOffset = (int)this._clusters[0];
			}
			this._indices = glyphRun.GlyphIndices;
			this._advances = glyphRun.AdvanceWidths;
			this._offsets = glyphRun.GlyphOffsets;
			this._glyphStringBuider = new StringBuilder(10);
			this._indicesStringBuider = new StringBuilder(Math.Max((this._characters == null) ? 0 : this._characters.Count, this._indices.Count) * this._glyphStringBuider.Capacity);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000B71D8 File Offset: 0x000B65D8
		public void ComputeContentStrings(out string characters, out string indices, out string caretStops)
		{
			if (this._clusters != null)
			{
				int num = 0;
				int charClusterStart = 0;
				bool flag = true;
				int i;
				for (i = 0; i < this._clusters.Count; i++)
				{
					if (flag)
					{
						num = (int)this._clusters[i];
						charClusterStart = i;
						flag = false;
					}
					else if ((int)this._clusters[i] != num)
					{
						this.AddCluster(num - this._glyphClusterInitialOffset, (int)this._clusters[i] - this._glyphClusterInitialOffset, charClusterStart, i);
						num = (int)this._clusters[i];
						charClusterStart = i;
					}
				}
				this.AddCluster(num - this._glyphClusterInitialOffset, this._indices.Count, charClusterStart, i);
			}
			else
			{
				for (int j = 0; j < this._indices.Count; j++)
				{
					this.AddCluster(j, j + 1, j, j + 1);
				}
			}
			this.RemoveTrailingCharacters(this._indicesStringBuider, ';');
			indices = this._indicesStringBuider.ToString();
			if (this._characters == null || this._characters.Count == 0)
			{
				characters = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(this._characters.Count);
				foreach (char value in this._characters)
				{
					stringBuilder.Append(value);
				}
				characters = stringBuilder.ToString();
			}
			caretStops = this.CreateCaretStopsString();
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000B7358 File Offset: 0x000B6758
		private void RemoveTrailingCharacters(StringBuilder sb, char trailingCharacter)
		{
			int length = sb.Length;
			int num = length - 1;
			while (num >= 0 && sb[num] == trailingCharacter)
			{
				num--;
			}
			sb.Length = num + 1;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000B7390 File Offset: 0x000B6790
		private void AddGlyph(int glyph, int sourceCharacter)
		{
			ushort num = this._indices[glyph];
			ushort num2;
			if (sourceCharacter == -1 || !this._glyphTypeface.CharacterToGlyphMap.TryGetValue(sourceCharacter, out num2) || num != num2)
			{
				this._glyphStringBuider.Append(num.ToString(CultureInfo.InvariantCulture));
			}
			this._glyphStringBuider.Append(',');
			int num3 = (int)Math.Round(this._advances[glyph] * this._milToEm);
			double num4 = this._sideways ? this._glyphTypeface.AdvanceHeights[num] : this._glyphTypeface.AdvanceWidths[num];
			if (num3 != (int)Math.Round(num4 * 100.0))
			{
				this._glyphStringBuider.Append(num3.ToString(CultureInfo.InvariantCulture));
			}
			this._glyphStringBuider.Append(',');
			if (this._offsets != null)
			{
				int num5 = (int)Math.Round(this._offsets[glyph].X * this._milToEm);
				if (num5 != 0)
				{
					this._glyphStringBuider.Append(num5.ToString(CultureInfo.InvariantCulture));
				}
				this._glyphStringBuider.Append(',');
				num5 = (int)Math.Round(this._offsets[glyph].Y * this._milToEm);
				if (num5 != 0)
				{
					this._glyphStringBuider.Append(num5.ToString(CultureInfo.InvariantCulture));
				}
				this._glyphStringBuider.Append(',');
			}
			this.RemoveTrailingCharacters(this._glyphStringBuider, ',');
			this._glyphStringBuider.Append(';');
			this._indicesStringBuider.Append(this._glyphStringBuider.ToString());
			this._glyphStringBuider.Length = 0;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000B7554 File Offset: 0x000B6954
		private void AddCluster(int glyphClusterStart, int glyphClusterEnd, int charClusterStart, int charClusterEnd)
		{
			int num = charClusterEnd - charClusterStart;
			int num2 = glyphClusterEnd - glyphClusterStart;
			int sourceCharacter = -1;
			if (num2 != 1)
			{
				this._indicesStringBuider.AppendFormat(CultureInfo.InvariantCulture, "({0}:{1})", new object[]
				{
					num,
					num2
				});
			}
			else if (num != 1)
			{
				this._indicesStringBuider.AppendFormat(CultureInfo.InvariantCulture, "({0})", new object[]
				{
					num
				});
			}
			else if (this._characters != null && this._characters.Count != 0)
			{
				sourceCharacter = (int)this._characters[charClusterStart];
			}
			for (int i = glyphClusterStart; i < glyphClusterEnd; i++)
			{
				this.AddGlyph(i, sourceCharacter);
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000B7604 File Offset: 0x000B6A04
		private string CreateCaretStopsString()
		{
			if (this._caretStops == null)
			{
				return string.Empty;
			}
			int num = 0;
			int num2 = 0;
			for (int i = this._caretStops.Count - 1; i >= 0; i--)
			{
				if (!this._caretStops[i])
				{
					num = (i + 4) / 4;
					num2 = Math.Min(i | 3, this._caretStops.Count - 1);
					break;
				}
			}
			if (num == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			byte b = 8;
			byte b2 = 0;
			for (int j = 0; j <= num2; j++)
			{
				if (this._caretStops[j])
				{
					b2 |= b;
				}
				if (b != 1)
				{
					b = (byte)(b >> 1);
				}
				else
				{
					stringBuilder.AppendFormat("{0:x1}", b2);
					b2 = 0;
					b = 8;
				}
			}
			if (b != 8)
			{
				stringBuilder.AppendFormat("{0:x1}", b2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040014CC RID: 5324
		private GlyphTypeface _glyphTypeface;

		// Token: 0x040014CD RID: 5325
		private IList<char> _characters;

		// Token: 0x040014CE RID: 5326
		private double _milToEm;

		// Token: 0x040014CF RID: 5327
		private bool _sideways;

		// Token: 0x040014D0 RID: 5328
		private int _glyphClusterInitialOffset;

		// Token: 0x040014D1 RID: 5329
		private IList<ushort> _clusters;

		// Token: 0x040014D2 RID: 5330
		private IList<ushort> _indices;

		// Token: 0x040014D3 RID: 5331
		private IList<double> _advances;

		// Token: 0x040014D4 RID: 5332
		private IList<Point> _offsets;

		// Token: 0x040014D5 RID: 5333
		private IList<bool> _caretStops;

		// Token: 0x040014D6 RID: 5334
		private StringBuilder _indicesStringBuider;

		// Token: 0x040014D7 RID: 5335
		private StringBuilder _glyphStringBuider;

		// Token: 0x040014D8 RID: 5336
		private const char GlyphSubEntrySeparator = ',';

		// Token: 0x040014D9 RID: 5337
		private const char GlyphSeparator = ';';

		// Token: 0x040014DA RID: 5338
		private const double EmScaleFactor = 100.0;
	}
}
