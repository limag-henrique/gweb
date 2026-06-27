using System;
using System.IO;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink
{
	// Token: 0x020007B4 RID: 1972
	internal class BitStreamReader
	{
		// Token: 0x0600530A RID: 21258 RVA: 0x0014BB00 File Offset: 0x0014AF00
		internal BitStreamReader(byte[] buffer)
		{
			this._byteArray = buffer;
			this._bufferLengthInBits = (uint)(buffer.Length * 8);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0014BB28 File Offset: 0x0014AF28
		internal BitStreamReader(byte[] buffer, int startIndex)
		{
			if (startIndex < 0 || startIndex >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			this._byteArray = buffer;
			this._byteArrayIndex = startIndex;
			this._bufferLengthInBits = (uint)((buffer.Length - startIndex) * 8);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0014BB6C File Offset: 0x0014AF6C
		internal BitStreamReader(byte[] buffer, uint bufferLengthInBits) : this(buffer)
		{
			if ((ulong)bufferLengthInBits > (ulong)((long)(buffer.Length * 8)))
			{
				throw new ArgumentOutOfRangeException("bufferLengthInBits", SR.Get("InvalidBufferLength"));
			}
			this._bufferLengthInBits = bufferLengthInBits;
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0014BBA8 File Offset: 0x0014AFA8
		internal long ReadUInt64(int countOfBits)
		{
			if (countOfBits > 64 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			long num = 0L;
			while (countOfBits > 0)
			{
				int num2 = 8;
				if (countOfBits < 8)
				{
					num2 = countOfBits;
				}
				num <<= num2;
				byte b = this.ReadByte(num2);
				num |= (long)((ulong)b);
				countOfBits -= num2;
			}
			return num;
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0014BC04 File Offset: 0x0014B004
		internal ushort ReadUInt16(int countOfBits)
		{
			if (countOfBits > 16 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			ushort num = 0;
			while (countOfBits > 0)
			{
				int num2 = 8;
				if (countOfBits < 8)
				{
					num2 = countOfBits;
				}
				num = (ushort)(num << num2);
				byte b = this.ReadByte(num2);
				num |= (ushort)b;
				countOfBits -= num2;
			}
			return num;
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0014BC60 File Offset: 0x0014B060
		internal uint ReadUInt16Reverse(int countOfBits)
		{
			if (countOfBits > 16 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			ushort num = 0;
			int num2 = 0;
			while (countOfBits > 0)
			{
				int num3 = 8;
				if (countOfBits < 8)
				{
					num3 = countOfBits;
				}
				ushort num4 = (ushort)this.ReadByte(num3);
				num4 = (ushort)(num4 << num2 * 8);
				num |= num4;
				num2++;
				countOfBits -= num3;
			}
			return (uint)num;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0014BCC4 File Offset: 0x0014B0C4
		internal uint ReadUInt32(int countOfBits)
		{
			if (countOfBits > 32 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			uint num = 0U;
			while (countOfBits > 0)
			{
				int num2 = 8;
				if (countOfBits < 8)
				{
					num2 = countOfBits;
				}
				num <<= num2;
				byte b = this.ReadByte(num2);
				num |= (uint)b;
				countOfBits -= num2;
			}
			return num;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0014BD20 File Offset: 0x0014B120
		internal uint ReadUInt32Reverse(int countOfBits)
		{
			if (countOfBits > 32 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			uint num = 0U;
			int num2 = 0;
			while (countOfBits > 0)
			{
				int num3 = 8;
				if (countOfBits < 8)
				{
					num3 = countOfBits;
				}
				uint num4 = (uint)this.ReadByte(num3);
				num4 <<= num2 * 8;
				num |= num4;
				num2++;
				countOfBits -= num3;
			}
			return num;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0014BD84 File Offset: 0x0014B184
		internal bool ReadBit()
		{
			byte b = this.ReadByte(1);
			return (b & 1) == 1;
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0014BDA0 File Offset: 0x0014B1A0
		internal byte ReadByte(int countOfBits)
		{
			if (this.EndOfStream)
			{
				throw new EndOfStreamException(SR.Get("EndOfStreamReached"));
			}
			if (countOfBits > 8 || countOfBits <= 0)
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsOutOfRange"));
			}
			if ((long)countOfBits > (long)((ulong)this._bufferLengthInBits))
			{
				throw new ArgumentOutOfRangeException("countOfBits", countOfBits, SR.Get("CountOfBitsGreatThanRemainingBits"));
			}
			this._bufferLengthInBits -= (uint)countOfBits;
			byte b;
			if (this._cbitsInPartialByte >= countOfBits)
			{
				int num = 8 - countOfBits;
				b = (byte)(this._partialByte >> num);
				this._partialByte = (byte)(this._partialByte << countOfBits);
				this._cbitsInPartialByte -= countOfBits;
			}
			else
			{
				byte b2 = this._byteArray[this._byteArrayIndex];
				this._byteArrayIndex++;
				int num2 = 8 - countOfBits;
				b = (byte)(this._partialByte >> num2);
				int num3 = Math.Abs(countOfBits - this._cbitsInPartialByte - 8);
				b |= (byte)(b2 >> num3);
				this._partialByte = (byte)(b2 << countOfBits - this._cbitsInPartialByte);
				this._cbitsInPartialByte = 8 - (countOfBits - this._cbitsInPartialByte);
			}
			return b;
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x0014BECC File Offset: 0x0014B2CC
		internal bool EndOfStream
		{
			get
			{
				return this._bufferLengthInBits == 0U;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x0014BEE4 File Offset: 0x0014B2E4
		internal int CurrentIndex
		{
			get
			{
				return this._byteArrayIndex - 1;
			}
		}

		// Token: 0x0400258B RID: 9611
		private byte[] _byteArray;

		// Token: 0x0400258C RID: 9612
		private uint _bufferLengthInBits;

		// Token: 0x0400258D RID: 9613
		private int _byteArrayIndex;

		// Token: 0x0400258E RID: 9614
		private byte _partialByte;

		// Token: 0x0400258F RID: 9615
		private int _cbitsInPartialByte;
	}
}
