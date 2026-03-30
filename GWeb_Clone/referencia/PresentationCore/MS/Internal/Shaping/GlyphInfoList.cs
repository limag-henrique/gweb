using System;
using System.Diagnostics;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B6 RID: 1718
	internal class GlyphInfoList
	{
		// Token: 0x06004B18 RID: 19224 RVA: 0x00125018 File Offset: 0x00124418
		internal GlyphInfoList(int capacity, int leap, bool justify)
		{
			this._glyphs = new UshortList(capacity, leap);
			this._glyphFlags = new UshortList(capacity, leap);
			this._firstChars = new UshortList(capacity, leap);
			this._ligatureCounts = new UshortList(capacity, leap);
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x00125060 File Offset: 0x00124460
		public int Length
		{
			get
			{
				return this._glyphs.Length;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x00125078 File Offset: 0x00124478
		internal int Offset
		{
			get
			{
				return this._glyphs.Offset;
			}
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x00125090 File Offset: 0x00124490
		[Conditional("DEBUG")]
		internal void ValidateLength(int cch)
		{
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x001250A0 File Offset: 0x001244A0
		public void SetRange(int index, int length)
		{
			this._glyphs.SetRange(index, length);
			this._glyphFlags.SetRange(index, length);
			this._firstChars.SetRange(index, length);
			this._ligatureCounts.SetRange(index, length);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x001250E4 File Offset: 0x001244E4
		[SecurityCritical]
		public void SetLength(int length)
		{
			this._glyphs.Length = length;
			this._glyphFlags.Length = length;
			this._firstChars.Length = length;
			this._ligatureCounts.Length = length;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00125124 File Offset: 0x00124524
		public void Insert(int index, int Count)
		{
			this._glyphs.Insert(index, Count);
			this._glyphFlags.Insert(index, Count);
			this._firstChars.Insert(index, Count);
			this._ligatureCounts.Insert(index, Count);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00125168 File Offset: 0x00124568
		public void Remove(int index, int Count)
		{
			this._glyphs.Remove(index, Count);
			this._glyphFlags.Remove(index, Count);
			this._firstChars.Remove(index, Count);
			this._ligatureCounts.Remove(index, Count);
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x001251AC File Offset: 0x001245AC
		public UshortList Glyphs
		{
			get
			{
				return this._glyphs;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x001251C0 File Offset: 0x001245C0
		public UshortList GlyphFlags
		{
			get
			{
				return this._glyphFlags;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06004B22 RID: 19234 RVA: 0x001251D4 File Offset: 0x001245D4
		public UshortList FirstChars
		{
			get
			{
				return this._firstChars;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x001251E8 File Offset: 0x001245E8
		public UshortList LigatureCounts
		{
			get
			{
				return this._ligatureCounts;
			}
		}

		// Token: 0x04001FE9 RID: 8169
		private UshortList _glyphs;

		// Token: 0x04001FEA RID: 8170
		private UshortList _glyphFlags;

		// Token: 0x04001FEB RID: 8171
		private UshortList _firstChars;

		// Token: 0x04001FEC RID: 8172
		private UshortList _ligatureCounts;
	}
}
