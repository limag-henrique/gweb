using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E2 RID: 1762
	internal class ShaperBuffers
	{
		// Token: 0x06004C16 RID: 19478 RVA: 0x00129E84 File Offset: 0x00129284
		public ShaperBuffers(ushort charCount, ushort glyphCount)
		{
			this._glyphInfoList = new GlyphInfoList((int)((glyphCount > charCount) ? glyphCount : charCount), 16, false);
			this._charMap = new UshortList((int)charCount, 16);
			this._layoutWorkspace = new OpenTypeLayoutWorkspace();
			this._charMap.SetRange(0, (int)charCount);
			if (glyphCount > 0)
			{
				this._glyphInfoList.SetRange(0, (int)glyphCount);
			}
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x00129EE4 File Offset: 0x001292E4
		~ShaperBuffers()
		{
			this._glyphInfoList = null;
			this._charMap = null;
			this._layoutWorkspace = null;
			this._textFeatures = null;
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x00129F34 File Offset: 0x00129334
		public UshortList CharMap
		{
			get
			{
				return this._charMap;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x00129F48 File Offset: 0x00129348
		// (set) Token: 0x06004C1A RID: 19482 RVA: 0x00129F5C File Offset: 0x0012935C
		public GlyphInfoList GlyphInfoList
		{
			get
			{
				return this._glyphInfoList;
			}
			set
			{
				this._glyphInfoList = value;
			}
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x00129F70 File Offset: 0x00129370
		public bool Initialize(ushort charCount, ushort glyphCount)
		{
			if (charCount <= 0)
			{
				return false;
			}
			if (this._charMap.Length > 0)
			{
				this._charMap.Remove(0, this._charMap.Length);
			}
			this._charMap.Insert(0, (int)charCount);
			if (this._glyphInfoList.Length > 0)
			{
				this._glyphInfoList.Remove(0, this._glyphInfoList.Length);
			}
			if (glyphCount > 0)
			{
				this._glyphInfoList.Insert(0, (int)glyphCount);
			}
			return true;
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00129FEC File Offset: 0x001293EC
		public bool InitializeFeatureList(ushort size, ushort keep)
		{
			if (this._textFeatures == null)
			{
				this._textFeatures = new ShaperFeaturesList();
				if (this._textFeatures == null)
				{
					return false;
				}
				this._textFeatures.Initialize(size);
			}
			else
			{
				this._textFeatures.Resize(size, keep);
			}
			return true;
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x0012A034 File Offset: 0x00129434
		public OpenTypeLayoutWorkspace LayoutWorkspace
		{
			get
			{
				return this._layoutWorkspace;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x0012A048 File Offset: 0x00129448
		public ShaperFeaturesList TextFeatures
		{
			get
			{
				return this._textFeatures;
			}
		}

		// Token: 0x04002104 RID: 8452
		private UshortList _charMap;

		// Token: 0x04002105 RID: 8453
		private GlyphInfoList _glyphInfoList;

		// Token: 0x04002106 RID: 8454
		private OpenTypeLayoutWorkspace _layoutWorkspace;

		// Token: 0x04002107 RID: 8455
		private ShaperFeaturesList _textFeatures;
	}
}
