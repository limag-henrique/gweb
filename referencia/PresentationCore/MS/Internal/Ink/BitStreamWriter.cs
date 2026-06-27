using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink
{
	// Token: 0x020007B5 RID: 1973
	internal class BitStreamWriter
	{
		// Token: 0x06005316 RID: 21270 RVA: 0x0014BEFC File Offset: 0x0014B2FC
		internal BitStreamWriter(List<byte> bufferToWriteTo)
		{
			if (bufferToWriteTo == null)
			{
				throw new ArgumentNullException("bufferToWriteTo");
			}
			this._targetBuffer = bufferToWriteTo;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0014BF24 File Offset: 0x0014B324
		internal void Write(uint bits, int countOfBits)
		{
			if (countOfBits <= 0 || countOfBits > 32)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			int i = countOfBits / 8;
			int num = countOfBits % 8;
			while (i >= 0)
			{
				byte bits2 = (byte)(bits >> i * 8);
				if (num > 0)
				{
					this.Write(bits2, num);
				}
				if (i > 0)
				{
					num = 8;
				}
				i--;
			}
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0014BF84 File Offset: 0x0014B384
		internal void WriteReverse(uint bits, int countOfBits)
		{
			if (countOfBits <= 0 || countOfBits > 32)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			int num = countOfBits / 8;
			int num2 = countOfBits % 8;
			if (num2 > 0)
			{
				num++;
			}
			for (int i = 0; i < num; i++)
			{
				byte bits2 = (byte)(bits >> i * 8);
				this.Write(bits2, 8);
			}
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0014BFE4 File Offset: 0x0014B3E4
		internal void Write(byte bits, int countOfBits)
		{
			if (countOfBits <= 0 || countOfBits > 8)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			if (this._remaining > 0)
			{
				byte b = this._targetBuffer[this._targetBuffer.Count - 1];
				if (countOfBits > this._remaining)
				{
					b |= (byte)(((int)bits & 255 >> 8 - countOfBits) >> countOfBits - this._remaining);
				}
				else
				{
					b |= (byte)(((int)bits & 255 >> 8 - countOfBits) << this._remaining - countOfBits);
				}
				this._targetBuffer[this._targetBuffer.Count - 1] = b;
			}
			if (countOfBits > this._remaining)
			{
				this._remaining = 8 - (countOfBits - this._remaining);
				byte b = (byte)(bits << this._remaining);
				this._targetBuffer.Add(b);
				return;
			}
			this._remaining -= countOfBits;
		}

		// Token: 0x04002590 RID: 9616
		private List<byte> _targetBuffer;

		// Token: 0x04002591 RID: 9617
		private int _remaining;
	}
}
