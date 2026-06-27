using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007DB RID: 2011
	internal class GorillaCodec
	{
		// Token: 0x0600549B RID: 21659 RVA: 0x0015C194 File Offset: 0x0015B594
		internal byte FindPacketAlgoByte(int[] input, bool testDelDel)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length == 0)
			{
				return 0;
			}
			testDelDel = (testDelDel && input.Length < 3);
			uint num = 1U;
			int num2 = 0;
			int num3 = 0;
			DeltaDelta deltaDelta = new DeltaDelta();
			int data4;
			int data3;
			int data2;
			int data = data2 = (data3 = (data4 = input[0]));
			if (testDelDel)
			{
				deltaDelta.Transform(input[0], ref num2, ref num3);
				deltaDelta.Transform(input[1], ref num2, ref num3);
				if (num3 != 0)
				{
					testDelDel = false;
				}
			}
			if (testDelDel)
			{
				deltaDelta.Transform(input[2], ref num2, ref num3);
				if (num3 != 0)
				{
					testDelDel = false;
				}
				else
				{
					data4 = (data3 = num2);
					GorillaCodec.UpdateMinMax(input[1], ref data, ref data2);
					GorillaCodec.UpdateMinMax(input[2], ref data, ref data2);
					num = 3U;
				}
			}
			uint num4 = num;
			while ((ulong)num4 < (ulong)((long)input.Length))
			{
				GorillaCodec.UpdateMinMax(input[(int)num4], ref data, ref data2);
				if (testDelDel)
				{
					deltaDelta.Transform(input[(int)num4], ref num2, ref num3);
					if (num3 != 0)
					{
						testDelDel = false;
					}
					else
					{
						GorillaCodec.UpdateMinMax(num2, ref data4, ref data3);
					}
				}
				num4 += 1U;
			}
			uint num5 = (uint)Math.Max(MathHelper.AbsNoThrow(data3), MathHelper.AbsNoThrow(data4));
			uint num6 = (uint)Math.Max(MathHelper.AbsNoThrow(data2), MathHelper.AbsNoThrow(data));
			if (testDelDel && num5 >> 1 < num6)
			{
				num6 = num5;
			}
			else
			{
				testDelDel = false;
			}
			int num7 = 0;
			while (num6 >> num7 != 0U && 31 > num7)
			{
				num7++;
			}
			num7++;
			return (byte)(num7 & 31) | (testDelDel ? 32 : 0);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x0015C2E4 File Offset: 0x0015B6E4
		internal byte FindPropAlgoByte(byte[] input)
		{
			if (input.Length == 0)
			{
				return 0;
			}
			int num = ((input.Length & 3) == 0) ? (input.Length >> 2) : 0;
			BitStreamReader bitStreamReader = null;
			if (num > 0)
			{
				bitStreamReader = new BitStreamReader(input);
			}
			int num2 = ((input.Length & 1) == 0) ? (input.Length >> 1) : 0;
			BitStreamReader bitStreamReader2 = null;
			if (num2 > 0)
			{
				bitStreamReader2 = new BitStreamReader(input);
			}
			int data = 0;
			int data2 = 0;
			ushort num3 = 0;
			byte b = input[0];
			uint num4 = 0U;
			while ((ulong)num4 < (ulong)((long)num))
			{
				b = Math.Max(input[(int)num4], b);
				num3 = Math.Max((ushort)bitStreamReader2.ReadUInt16Reverse(16), num3);
				GorillaCodec.UpdateMinMax((int)bitStreamReader.ReadUInt32Reverse(32), ref data, ref data2);
				num4 += 1U;
			}
			while ((ulong)num4 < (ulong)((long)num2))
			{
				b = Math.Max(input[(int)num4], b);
				num3 = Math.Max((ushort)bitStreamReader2.ReadUInt16Reverse(16), num3);
				num4 += 1U;
			}
			while ((ulong)num4 < (ulong)((long)input.Length))
			{
				b = Math.Max(input[(int)num4], b);
				num4 += 1U;
			}
			int num5 = 1;
			uint num6 = (uint)b;
			while (num6 >> num5 != 0U && (long)num5 < 8L)
			{
				num5++;
			}
			int num7 = ((~(num5 * input.Length) & 7) + 1 & 7) / num5;
			if (num2 > 0)
			{
				int num8 = 1;
				num6 = (uint)num3;
				while (num6 >> num8 != 0U && (long)num8 < 16L)
				{
					num8++;
				}
				if (num8 < num5 << 1)
				{
					num5 = num8;
					num7 = ((~(num5 * num2) & 7) + 1 & 7) / num5;
				}
				else
				{
					num2 = 0;
				}
			}
			if (num > 0)
			{
				int num9 = 0;
				num6 = (uint)Math.Max(MathHelper.AbsNoThrow(data2), MathHelper.AbsNoThrow(data));
				while (num6 >> num9 != 0U && 31 > num9)
				{
					num9++;
				}
				num9++;
				if (num9 < ((0 < num2) ? (num5 << 1) : (num5 << 2)))
				{
					num5 = num9;
					num7 = ((~(num5 * num) & 7) + 1 & 7) / num5;
					num2 = 0;
				}
				else
				{
					num = 0;
				}
			}
			byte b2 = (0 < num) ? 64 : ((0 < num2) ? 32 : 0);
			if (8 == num5 && num + num2 == 0)
			{
				b2 = 0;
			}
			else if (num5 > 7)
			{
				b2 |= (byte)(16 + num5);
			}
			else
			{
				b2 |= (byte)((int)GorillaCodec._gorIndexOffset[num5] + num7);
			}
			return b2;
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0015C4DC File Offset: 0x0015B8DC
		internal void GetPropertyBitCount(byte algorithmByte, ref int countPerItem, ref int bitCount, ref int padCount)
		{
			int num;
			if ((algorithmByte & 64) != 0)
			{
				countPerItem = 4;
				num = (int)(algorithmByte & 63);
			}
			else
			{
				countPerItem = (((algorithmByte & 32) != 0) ? 2 : 1);
				num = (int)(algorithmByte & 31);
			}
			bitCount = num - 16;
			padCount = 0;
			if (num < GorillaCodec._gorIndexMap.Length && num >= 0)
			{
				bitCount = (int)GorillaCodec._gorIndexMap[num].BitCount;
				padCount = (int)GorillaCodec._gorIndexMap[num].PadCount;
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0015C54C File Offset: 0x0015B94C
		internal void Compress(int bitCount, int[] input, int startInputIndex, DeltaDelta dtxf, List<byte> compressedData)
		{
			if (input == null || compressedData == null)
			{
				throw new ArgumentNullException(StrokeCollectionSerializer.ISFDebugMessage("input or compressed data was null in Compress"));
			}
			if (bitCount < 0)
			{
				throw new ArgumentOutOfRangeException("bitCount");
			}
			if (bitCount == 0)
			{
				bitCount = (int)((int)Native.SizeOfInt << 3);
			}
			BitStreamWriter bitStreamWriter = new BitStreamWriter(compressedData);
			if (dtxf != null)
			{
				int bits = 0;
				int num = 0;
				for (int i = startInputIndex; i < input.Length; i++)
				{
					dtxf.Transform(input[i], ref bits, ref num);
					if (num != 0)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Transform returned unexpected results"));
					}
					bitStreamWriter.Write((uint)bits, bitCount);
				}
				return;
			}
			for (int j = startInputIndex; j < input.Length; j++)
			{
				bitStreamWriter.Write((uint)input[j], bitCount);
			}
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0015C5F0 File Offset: 0x0015B9F0
		internal void Compress(int bitCount, BitStreamReader reader, GorillaEncodingType encodingType, int unitsToEncode, List<byte> compressedData)
		{
			if (reader == null || compressedData == null)
			{
				throw new ArgumentNullException(StrokeCollectionSerializer.ISFDebugMessage("reader or compressedData was null in compress"));
			}
			if (bitCount < 0)
			{
				throw new ArgumentOutOfRangeException("bitCount");
			}
			if (unitsToEncode < 0)
			{
				throw new ArgumentOutOfRangeException("unitsToEncode");
			}
			if (bitCount == 0)
			{
				switch (encodingType)
				{
				case GorillaEncodingType.Byte:
					bitCount = 8;
					break;
				case GorillaEncodingType.Short:
					bitCount = 16;
					break;
				case GorillaEncodingType.Int:
					bitCount = 32;
					break;
				default:
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("bogus GorillaEncodingType passed to compress"));
				}
			}
			BitStreamWriter bitStreamWriter = new BitStreamWriter(compressedData);
			while (!reader.EndOfStream && unitsToEncode > 0)
			{
				int dataFromReader = this.GetDataFromReader(reader, encodingType);
				bitStreamWriter.Write((uint)dataFromReader, bitCount);
				unitsToEncode--;
			}
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0015C69C File Offset: 0x0015BA9C
		private int GetDataFromReader(BitStreamReader reader, GorillaEncodingType type)
		{
			switch (type)
			{
			case GorillaEncodingType.Byte:
				return (int)reader.ReadByte(8);
			case GorillaEncodingType.Short:
				return (int)reader.ReadUInt16Reverse(16);
			case GorillaEncodingType.Int:
				return (int)reader.ReadUInt32Reverse(32);
			default:
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("bogus GorillaEncodingType passed to GetDataFromReader"));
			}
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0015C6E8 File Offset: 0x0015BAE8
		internal uint Uncompress(int bitCount, byte[] input, int inputIndex, DeltaDelta dtxf, int[] outputBuffer, int outputBufferIndex)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inputIndex >= input.Length)
			{
				throw new ArgumentOutOfRangeException("inputIndex");
			}
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputBufferIndex >= outputBuffer.Length)
			{
				throw new ArgumentOutOfRangeException("outputBufferIndex");
			}
			if (bitCount < 0)
			{
				throw new ArgumentOutOfRangeException("bitCount");
			}
			if (bitCount == 0)
			{
				bitCount = (int)((int)Native.SizeOfInt << 3);
			}
			uint num = uint.MaxValue << bitCount - 1;
			BitStreamReader bitStreamReader = new BitStreamReader(input, inputIndex);
			if (dtxf != null)
			{
				while (!bitStreamReader.EndOfStream)
				{
					uint num2 = bitStreamReader.ReadUInt32(bitCount);
					num2 = (((num2 & num) != 0U) ? (num | num2) : num2);
					int num3 = dtxf.InverseTransform((int)num2, 0);
					outputBuffer[outputBufferIndex++] = num3;
					if (outputBufferIndex == outputBuffer.Length)
					{
						break;
					}
				}
			}
			else
			{
				while (!bitStreamReader.EndOfStream)
				{
					uint num2 = bitStreamReader.ReadUInt32(bitCount);
					num2 = (((num2 & num) != 0U) ? (num | num2) : num2);
					outputBuffer[outputBufferIndex++] = (int)num2;
					if (outputBufferIndex == outputBuffer.Length)
					{
						break;
					}
				}
			}
			return (uint)(outputBuffer.Length * bitCount + 7 >> 3);
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0015C7E0 File Offset: 0x0015BBE0
		internal byte[] Uncompress(int bitCount, BitStreamReader reader, GorillaEncodingType encodingType, int unitsToDecode)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (bitCount < 0)
			{
				throw new ArgumentOutOfRangeException("bitCount");
			}
			if (unitsToDecode < 0)
			{
				throw new ArgumentOutOfRangeException("unitsToDecode");
			}
			int num;
			uint num2;
			switch (encodingType)
			{
			case GorillaEncodingType.Byte:
				if (bitCount == 0)
				{
					bitCount = 8;
				}
				num = 8;
				num2 = 0U;
				break;
			case GorillaEncodingType.Short:
				if (bitCount == 0)
				{
					bitCount = 16;
				}
				num = 16;
				num2 = 0U;
				break;
			case GorillaEncodingType.Int:
				if (bitCount == 0)
				{
					bitCount = 32;
				}
				num = 32;
				num2 = uint.MaxValue << bitCount - 1;
				break;
			default:
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("bogus GorillaEncodingType passed to Uncompress"));
			}
			List<byte> list = new List<byte>(num / 8 * unitsToDecode);
			BitStreamWriter bitStreamWriter = new BitStreamWriter(list);
			while (!reader.EndOfStream && unitsToDecode > 0)
			{
				uint num3 = reader.ReadUInt32(bitCount);
				num3 = (((num3 & num2) != 0U) ? (num2 | num3) : num3);
				bitStreamWriter.WriteReverse(num3, num);
				unitsToDecode--;
			}
			return list.ToArray();
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0015C8C0 File Offset: 0x0015BCC0
		private static void UpdateMinMax(int n, ref int max, ref int min)
		{
			if (n > max)
			{
				max = n;
				return;
			}
			if (n < min)
			{
				min = n;
			}
		}

		// Token: 0x04002625 RID: 9765
		private static GorillaAlgoByte[] _gorIndexMap = new GorillaAlgoByte[]
		{
			new GorillaAlgoByte(8U, 0U),
			new GorillaAlgoByte(1U, 0U),
			new GorillaAlgoByte(1U, 1U),
			new GorillaAlgoByte(1U, 2U),
			new GorillaAlgoByte(1U, 3U),
			new GorillaAlgoByte(1U, 4U),
			new GorillaAlgoByte(1U, 5U),
			new GorillaAlgoByte(1U, 6U),
			new GorillaAlgoByte(1U, 7U),
			new GorillaAlgoByte(2U, 0U),
			new GorillaAlgoByte(2U, 1U),
			new GorillaAlgoByte(2U, 2U),
			new GorillaAlgoByte(2U, 3U),
			new GorillaAlgoByte(3U, 0U),
			new GorillaAlgoByte(3U, 1U),
			new GorillaAlgoByte(3U, 2U),
			new GorillaAlgoByte(4U, 0U),
			new GorillaAlgoByte(4U, 1U),
			new GorillaAlgoByte(5U, 0U),
			new GorillaAlgoByte(5U, 1U),
			new GorillaAlgoByte(6U, 0U),
			new GorillaAlgoByte(6U, 1U),
			new GorillaAlgoByte(7U, 0U),
			new GorillaAlgoByte(7U, 1U)
		};

		// Token: 0x04002626 RID: 9766
		private static byte[] _gorIndexOffset = new byte[]
		{
			0,
			1,
			9,
			13,
			16,
			18,
			20,
			22
		};
	}
}
