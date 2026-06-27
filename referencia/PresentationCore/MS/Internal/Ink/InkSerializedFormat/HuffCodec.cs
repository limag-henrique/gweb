using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007DE RID: 2014
	internal class HuffCodec
	{
		// Token: 0x060054A5 RID: 21669 RVA: 0x0015C8FC File Offset: 0x0015BCFC
		internal HuffCodec(uint defaultIndex)
		{
			HuffCodec.HuffBits huffBits = new HuffCodec.HuffBits();
			huffBits.InitBits(defaultIndex);
			this.InitHuffTable(huffBits);
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0015C934 File Offset: 0x0015BD34
		private void InitHuffTable(HuffCodec.HuffBits huffBits)
		{
			this._huffBits = huffBits;
			uint size = this._huffBits.GetSize();
			int num = 1;
			this._mins[0] = 0U;
			for (uint num2 = 1U; num2 < size; num2 += 1U)
			{
				this._mins[(int)num2] = (uint)num;
				num += 1 << (int)(this._huffBits.GetBitsAtIndex(num2) - 1);
			}
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0015C98C File Offset: 0x0015BD8C
		internal void Compress(DataXform dataXf, int[] input, List<byte> compressedData)
		{
			BitStreamWriter writer = new BitStreamWriter(compressedData);
			if (dataXf != null)
			{
				dataXf.ResetState();
				int data = 0;
				int extra = 0;
				uint num = 0U;
				while ((ulong)num < (ulong)((long)input.Length))
				{
					dataXf.Transform(input[(int)num], ref data, ref extra);
					this.Encode(data, extra, writer);
					num += 1U;
				}
				return;
			}
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)input.Length))
			{
				this.Encode(input[(int)num2], 0, writer);
				num2 += 1U;
			}
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0015C9F4 File Offset: 0x0015BDF4
		internal uint Uncompress(DataXform dtxf, byte[] input, int startIndex, int[] outputBuffer)
		{
			BitStreamReader bitStreamReader = new BitStreamReader(input, startIndex);
			int extra = 0;
			int num = 0;
			int num2 = 0;
			if (dtxf != null)
			{
				dtxf.ResetState();
				while (!bitStreamReader.EndOfStream)
				{
					this.Decode(ref num, ref extra, bitStreamReader);
					int num3 = dtxf.InverseTransform(num, extra);
					outputBuffer[num2++] = num3;
					if (num2 == outputBuffer.Length)
					{
						break;
					}
				}
			}
			else
			{
				while (!bitStreamReader.EndOfStream)
				{
					this.Decode(ref num, ref extra, bitStreamReader);
					outputBuffer[num2++] = num;
					if (num2 == outputBuffer.Length)
					{
						break;
					}
				}
			}
			return (uint)(bitStreamReader.CurrentIndex + 1 - startIndex);
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0015CA78 File Offset: 0x0015BE78
		internal byte Encode(int data, int extra, BitStreamWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (data == 0)
			{
				writer.Write(0, 1);
				return 1;
			}
			uint size = this._huffBits.GetSize();
			if (extra != 0)
			{
				byte b = (byte)(size + 1U);
				int bits = (1 << (int)b) - 2;
				writer.Write((uint)bits, (int)b);
				byte b2 = this.Encode(extra, 0, writer);
				byte b3 = this.Encode(data, 0, writer);
				return b + b2 + b3;
			}
			uint num = (uint)MathHelper.AbsNoThrow(data);
			byte b4 = 1;
			while ((uint)b4 < size && num >= this._mins[(int)b4])
			{
				b4 += 1;
			}
			uint bitsAtIndex = (uint)this._huffBits.GetBitsAtIndex((uint)(b4 - 1));
			int bits2 = (1 << (int)b4) - 2;
			writer.Write((uint)bits2, (int)b4);
			int num2 = (int)(bitsAtIndex - 1U);
			num = ((num - this._mins[(int)(b4 - 1)] & (1U << num2) - 1U) << 1 | ((data < 0) ? 1U : 0U));
			writer.Write(num, (int)bitsAtIndex);
			return (byte)((uint)b4 + bitsAtIndex);
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0015CB60 File Offset: 0x0015BF60
		internal void Decode(ref int data, ref int extra, BitStreamReader reader)
		{
			byte b = 0;
			while (reader.ReadBit())
			{
				b += 1;
			}
			extra = 0;
			if (b == 0)
			{
				data = 0;
				return;
			}
			if ((uint)b < this._huffBits.GetSize())
			{
				uint bitsAtIndex = (uint)this._huffBits.GetBitsAtIndex((uint)b);
				long num = reader.ReadUInt64((int)((byte)bitsAtIndex));
				bool flag = (num & 1L) != 0L;
				num = (num >> 1) + (long)((ulong)this._mins[(int)b]);
				data = (flag ? (-(int)num) : ((int)num));
				return;
			}
			if ((uint)b == this._huffBits.GetSize())
			{
				int num2 = 0;
				int num3 = 0;
				this.Decode(ref num2, ref num3, reader);
				extra = num2;
				int num4 = 0;
				this.Decode(ref num4, ref num3, reader);
				data = num4;
				return;
			}
			throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("invalid huffman encoded data"));
		}

		// Token: 0x0400262D RID: 9773
		private HuffCodec.HuffBits _huffBits;

		// Token: 0x0400262E RID: 9774
		private uint[] _mins = new uint[(int)HuffCodec.MaxBAASize];

		// Token: 0x0400262F RID: 9775
		private static readonly byte MaxBAASize = 10;

		// Token: 0x02000A0D RID: 2573
		private class HuffBits
		{
			// Token: 0x06005C0A RID: 23562 RVA: 0x00171DB0 File Offset: 0x001711B0
			internal HuffBits()
			{
				this._size = 2U;
				this._bits[0] = 0;
				this._bits[1] = 32;
				this._matchIndex = 0U;
				this._prefixCount = 1U;
			}

			// Token: 0x06005C0B RID: 23563 RVA: 0x00171DFC File Offset: 0x001711FC
			internal bool InitBits(uint defaultIndex)
			{
				if (defaultIndex < (uint)HuffCodec.HuffBits.DefaultBAACount && HuffCodec.HuffBits.DefaultBAASize[(int)defaultIndex] <= HuffCodec.HuffBits.MaxBAASize)
				{
					this._size = (uint)HuffCodec.HuffBits.DefaultBAASize[(int)defaultIndex];
					this._matchIndex = defaultIndex;
					this._prefixCount = this._size;
					this._bits = HuffCodec.HuffBits.DefaultBAAData[(int)defaultIndex];
					return true;
				}
				return false;
			}

			// Token: 0x06005C0C RID: 23564 RVA: 0x00171E50 File Offset: 0x00171250
			internal uint GetSize()
			{
				return this._size;
			}

			// Token: 0x06005C0D RID: 23565 RVA: 0x00171E64 File Offset: 0x00171264
			internal byte GetBitsAtIndex(uint index)
			{
				return this._bits[(int)index];
			}

			// Token: 0x04002F7E RID: 12158
			private byte[] _bits = new byte[(int)HuffCodec.HuffBits.MaxBAASize];

			// Token: 0x04002F7F RID: 12159
			private uint _size;

			// Token: 0x04002F80 RID: 12160
			private uint _matchIndex;

			// Token: 0x04002F81 RID: 12161
			private uint _prefixCount;

			// Token: 0x04002F82 RID: 12162
			private static readonly byte MaxBAASize = 10;

			// Token: 0x04002F83 RID: 12163
			private static readonly byte DefaultBAACount = 8;

			// Token: 0x04002F84 RID: 12164
			private static readonly byte[][] DefaultBAAData = new byte[][]
			{
				new byte[]
				{
					0,
					1,
					2,
					4,
					6,
					8,
					12,
					16,
					24,
					32
				},
				new byte[]
				{
					0,
					1,
					1,
					2,
					4,
					8,
					12,
					16,
					24,
					32
				},
				new byte[]
				{
					0,
					1,
					1,
					1,
					2,
					4,
					8,
					14,
					22,
					32
				},
				new byte[]
				{
					0,
					2,
					2,
					3,
					5,
					8,
					12,
					16,
					24,
					32
				},
				new byte[]
				{
					0,
					3,
					4,
					5,
					8,
					12,
					16,
					24,
					32,
					0
				},
				new byte[]
				{
					0,
					4,
					6,
					8,
					12,
					16,
					24,
					32,
					0,
					0
				},
				new byte[]
				{
					0,
					6,
					8,
					12,
					16,
					24,
					32,
					0,
					0,
					0
				},
				new byte[]
				{
					0,
					7,
					8,
					12,
					16,
					24,
					32,
					0,
					0,
					0
				}
			};

			// Token: 0x04002F85 RID: 12165
			private static readonly byte[] DefaultBAASize = new byte[]
			{
				10,
				10,
				10,
				10,
				9,
				8,
				7,
				7
			};
		}
	}
}
