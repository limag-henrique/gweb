using System;
using System.Text;

namespace MS.Internal
{
	// Token: 0x02000676 RID: 1654
	internal class DecoderFallbackWithFailureFlag : DecoderFallback
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x0011D380 File Offset: 0x0011C780
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderFallbackWithFailureFlag.FallbackBuffer(this);
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004913 RID: 18707 RVA: 0x0011D394 File Offset: 0x0011C794
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0011D3A4 File Offset: 0x0011C7A4
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0011D3B8 File Offset: 0x0011C7B8
		public bool HasFailed
		{
			get
			{
				return this._hasFailed;
			}
			set
			{
				this._hasFailed = value;
			}
		}

		// Token: 0x04001CB2 RID: 7346
		private bool _hasFailed;

		// Token: 0x020009A3 RID: 2467
		private class FallbackBuffer : DecoderFallbackBuffer
		{
			// Token: 0x06005A42 RID: 23106 RVA: 0x0016B1FC File Offset: 0x0016A5FC
			public FallbackBuffer(DecoderFallbackWithFailureFlag parent)
			{
				this._parent = parent;
			}

			// Token: 0x06005A43 RID: 23107 RVA: 0x0016B218 File Offset: 0x0016A618
			public override bool Fallback(byte[] bytesUnknown, int index)
			{
				this._parent.HasFailed = true;
				return false;
			}

			// Token: 0x06005A44 RID: 23108 RVA: 0x0016B234 File Offset: 0x0016A634
			public override char GetNextChar()
			{
				return '\0';
			}

			// Token: 0x06005A45 RID: 23109 RVA: 0x0016B244 File Offset: 0x0016A644
			public override bool MovePrevious()
			{
				return false;
			}

			// Token: 0x1700126A RID: 4714
			// (get) Token: 0x06005A46 RID: 23110 RVA: 0x0016B254 File Offset: 0x0016A654
			public override int Remaining
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x04002D80 RID: 11648
			private DecoderFallbackWithFailureFlag _parent;
		}
	}
}
