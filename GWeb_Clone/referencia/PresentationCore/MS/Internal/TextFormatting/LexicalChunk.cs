using System;
using System.Windows.Media.TextFormatting;
using MS.Internal.Generic;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200070B RID: 1803
	internal struct LexicalChunk
	{
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x00133578 File Offset: 0x00132978
		internal TextLexicalBreaks Breaks
		{
			get
			{
				return this._breaks;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06004D9D RID: 19869 RVA: 0x0013358C File Offset: 0x0013298C
		internal bool IsNoBreak
		{
			get
			{
				return this._breaks == null;
			}
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x001335A4 File Offset: 0x001329A4
		internal LexicalChunk(TextLexicalBreaks breaks, SpanVector<int> ichVector)
		{
			Invariant.Assert(breaks != null);
			this._breaks = breaks;
			this._ichVector = ichVector;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x001335C8 File Offset: 0x001329C8
		internal int LSCPToCharacterIndex(int lsdcp)
		{
			if (this._ichVector.Count > 0)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < this._ichVector.Count; i++)
				{
					Span<int> span = this._ichVector[i];
					int value = span.Value;
					if (value > lsdcp)
					{
						return num - num2 + Math.Min(num2, lsdcp - num3);
					}
					num += span.Length;
					num2 = span.Length;
					num3 = value;
				}
				return num - num2 + Math.Min(num2, lsdcp - num3);
			}
			return lsdcp;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00133650 File Offset: 0x00132A50
		internal int CharacterIndexToLSCP(int ich)
		{
			if (this._ichVector.Count > 0)
			{
				SpanRider<int> spanRider = new SpanRider<int>(this._ichVector);
				spanRider.At(ich);
				return spanRider.CurrentValue + ich - spanRider.CurrentSpanStart;
			}
			return ich;
		}

		// Token: 0x040021C2 RID: 8642
		private TextLexicalBreaks _breaks;

		// Token: 0x040021C3 RID: 8643
		private SpanVector<int> _ichVector;
	}
}
