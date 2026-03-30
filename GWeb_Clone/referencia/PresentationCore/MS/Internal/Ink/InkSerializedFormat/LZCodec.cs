using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007E0 RID: 2016
	internal class LZCodec
	{
		// Token: 0x060054B1 RID: 21681 RVA: 0x0015CD38 File Offset: 0x0015C138
		internal LZCodec()
		{
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0015CD5C File Offset: 0x0015C15C
		internal byte[] Uncompress(byte[] input, int inputIndex)
		{
			List<byte> list = new List<byte>();
			BitStreamWriter bitStreamWriter = new BitStreamWriter(list);
			BitStreamReader bitStreamReader = new BitStreamReader(input, inputIndex);
			this._maxMatchLength = LZCodec.FirstMaxMatchLength;
			for (int i = 0; i < LZCodec.RingBufferLength - this._maxMatchLength; i++)
			{
				this._ringBuffer[i] = 0;
			}
			this._flags = 0;
			this._currentRingBufferPosition = LZCodec.RingBufferLength - this._maxMatchLength;
			while (!bitStreamReader.EndOfStream)
			{
				byte b = bitStreamReader.ReadByte(8);
				if (((this._flags >>= 1) & 256) == 0)
				{
					this._flags = ((int)b | 65280);
					b = bitStreamReader.ReadByte(8);
				}
				if ((this._flags & 1) != 0)
				{
					bitStreamWriter.Write(b, 8);
					byte[] ringBuffer = this._ringBuffer;
					int currentRingBufferPosition = this._currentRingBufferPosition;
					this._currentRingBufferPosition = currentRingBufferPosition + 1;
					ringBuffer[currentRingBufferPosition] = b;
					this._currentRingBufferPosition &= LZCodec.RingBufferLength - 1;
				}
				else
				{
					byte b2 = bitStreamReader.ReadByte(8);
					int num = (int)b2;
					int num2 = (num & 240) << 4 | (int)b;
					num = (num & 15) + LZCodec.MaxLiteralLength;
					for (int i = 0; i <= num; i++)
					{
						b = this._ringBuffer[num2 + i & LZCodec.RingBufferLength - 1];
						bitStreamWriter.Write(b, 8);
						byte[] ringBuffer2 = this._ringBuffer;
						int currentRingBufferPosition = this._currentRingBufferPosition;
						this._currentRingBufferPosition = currentRingBufferPosition + 1;
						ringBuffer2[currentRingBufferPosition] = b;
						this._currentRingBufferPosition &= LZCodec.RingBufferLength - 1;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x04002633 RID: 9779
		private byte[] _ringBuffer = new byte[LZCodec.RingBufferLength];

		// Token: 0x04002634 RID: 9780
		private int _maxMatchLength;

		// Token: 0x04002635 RID: 9781
		private int _flags;

		// Token: 0x04002636 RID: 9782
		private int _currentRingBufferPosition;

		// Token: 0x04002637 RID: 9783
		private static readonly int FirstMaxMatchLength = 16;

		// Token: 0x04002638 RID: 9784
		private static readonly int RingBufferLength = 4069;

		// Token: 0x04002639 RID: 9785
		private static readonly int MaxLiteralLength = 2;
	}
}
