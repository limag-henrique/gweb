using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006CE RID: 1742
	internal class Feature
	{
		// Token: 0x06004B85 RID: 19333 RVA: 0x00126F90 File Offset: 0x00126390
		public Feature(ushort startIndex, ushort length, uint tag, uint parameter)
		{
			this._startIndex = startIndex;
			this._length = length;
			this._tag = tag;
			this._parameter = parameter;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x00126FC0 File Offset: 0x001263C0
		// (set) Token: 0x06004B87 RID: 19335 RVA: 0x00126FD4 File Offset: 0x001263D4
		public uint Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004B88 RID: 19336 RVA: 0x00126FE8 File Offset: 0x001263E8
		// (set) Token: 0x06004B89 RID: 19337 RVA: 0x00126FFC File Offset: 0x001263FC
		public uint Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x00127010 File Offset: 0x00126410
		// (set) Token: 0x06004B8B RID: 19339 RVA: 0x00127024 File Offset: 0x00126424
		public ushort StartIndex
		{
			get
			{
				return this._startIndex;
			}
			set
			{
				this._startIndex = value;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x00127038 File Offset: 0x00126438
		// (set) Token: 0x06004B8D RID: 19341 RVA: 0x0012704C File Offset: 0x0012644C
		public ushort Length
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}

		// Token: 0x04002094 RID: 8340
		private ushort _startIndex;

		// Token: 0x04002095 RID: 8341
		private ushort _length;

		// Token: 0x04002096 RID: 8342
		private uint _tag;

		// Token: 0x04002097 RID: 8343
		private uint _parameter;
	}
}
