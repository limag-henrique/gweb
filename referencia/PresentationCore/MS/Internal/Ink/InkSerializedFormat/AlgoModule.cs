using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D7 RID: 2007
	internal class AlgoModule
	{
		// Token: 0x06005483 RID: 21635 RVA: 0x0015B88C File Offset: 0x0015AC8C
		internal AlgoModule()
		{
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0015B8A0 File Offset: 0x0015ACA0
		internal byte GetBestDefHuff(int[] input)
		{
			if (input.Length < 3)
			{
				return AlgoModule.NoCompression;
			}
			DeltaDelta deltaDelta = new DeltaDelta();
			int num = 0;
			int num2 = 0;
			deltaDelta.Transform(input[0], ref num, ref num2);
			deltaDelta.Transform(input[1], ref num, ref num2);
			double num3 = 0.0;
			uint num4 = 2U;
			while ((ulong)num4 < (ulong)((long)input.Length))
			{
				deltaDelta.Transform(input[(int)num4], ref num, ref num2);
				if (num2 == 0)
				{
					num3 += (double)num * (double)num;
				}
				num4 += 1U;
			}
			num3 *= 0.205625 / (num4 - 1.0);
			int num5 = AlgoModule.DefaultFirstSquareRoot.Length - 2;
			while (num5 > 1 && num3 <= AlgoModule.DefaultFirstSquareRoot[num5])
			{
				num5--;
			}
			return AlgoModule.IndexedHuffman | (byte)(num5 + 1);
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0015B95C File Offset: 0x0015AD5C
		internal byte[] CompressPacketData(int[] input, byte compression)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			List<byte> list = new List<byte>();
			list.Add(0);
			if (AlgoModule.DefaultCompression == (AlgoModule.DefaultCompression & compression))
			{
				compression = this.GetBestDefHuff(input);
			}
			if (AlgoModule.IndexedHuffman == (AlgoModule.DefaultCompression & compression))
			{
				DataXform dataXf = this.HuffModule.FindDtXf(compression);
				HuffCodec huffCodec = this.HuffModule.FindCodec(compression);
				huffCodec.Compress(dataXf, input, list);
				if (list.Count - 1 >> 2 > input.Length)
				{
					compression = AlgoModule.NoCompression;
					list.Clear();
					list.Add(0);
				}
			}
			if (AlgoModule.NoCompression == (AlgoModule.DefaultCompression & compression))
			{
				bool testDelDel = (compression & 32) > 0;
				compression = this.GorillaCodec.FindPacketAlgoByte(input, testDelDel);
				DeltaDelta deltaDelta = null;
				if ((compression & 32) != 0)
				{
					deltaDelta = this.DeltaDelta;
				}
				int startInputIndex = 0;
				if (deltaDelta != null)
				{
					int data = 0;
					int num = 0;
					deltaDelta.ResetState();
					deltaDelta.Transform(input[0], ref data, ref num);
					this.MultiByteCodec.SignEncode(data, list);
					deltaDelta.Transform(input[1], ref data, ref num);
					this.MultiByteCodec.SignEncode(data, list);
					startInputIndex = 2;
				}
				int bitCount = (int)(compression & 31);
				this.GorillaCodec.Compress(bitCount, input, startInputIndex, deltaDelta, list);
			}
			list[0] = compression;
			return list.ToArray();
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0015BA98 File Offset: 0x0015AE98
		internal uint DecompressPacketData(byte[] input, int[] outputBuffer)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 2)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Input buffer passed was shorter than expected"));
			}
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputBuffer.Length == 0)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("output buffer length was zero"));
			}
			byte b = input[0];
			uint num = 1U;
			int num2 = 1;
			int num3 = (int)(b & 192);
			if (num3 == 0)
			{
				int outputBufferIndex = 0;
				DeltaDelta deltaDelta = null;
				if ((b & 32) != 0)
				{
					deltaDelta = this.DeltaDelta;
				}
				int bitCount;
				if ((b & 31) == 0)
				{
					bitCount = 32;
				}
				else
				{
					bitCount = (int)(b & 31);
				}
				if (deltaDelta != null)
				{
					if (input.Length < 3)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Input buffer was too short (must be at least 3 bytes)"));
					}
					int xfData = 0;
					int extra = 0;
					deltaDelta.ResetState();
					uint num4 = this.MultiByteCodec.SignDecode(input, num2, ref xfData);
					num2 += (int)num4;
					num += num4;
					int num5 = deltaDelta.InverseTransform(xfData, extra);
					outputBuffer[outputBufferIndex++] = num5;
					num4 = this.MultiByteCodec.SignDecode(input, num2, ref xfData);
					num2 += (int)num4;
					num += num4;
					num5 = deltaDelta.InverseTransform(xfData, extra);
					outputBuffer[outputBufferIndex++] = num5;
				}
				return num + this.GorillaCodec.Uncompress(bitCount, input, num2, deltaDelta, outputBuffer, outputBufferIndex);
			}
			if (num3 == 128)
			{
				DataXform dtxf = this.HuffModule.FindDtXf(b);
				HuffCodec huffCodec = this.HuffModule.FindCodec(b);
				return num + huffCodec.Uncompress(dtxf, input, num2, outputBuffer);
			}
			throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid decompression algo byte"));
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0015BC14 File Offset: 0x0015B014
		internal byte[] CompressPropertyData(byte[] input, byte compression)
		{
			List<byte> list = new List<byte>(input.Length + 1);
			list.Add(0);
			if (AlgoModule.DefaultCompression == (AlgoModule.DefaultCompression & compression))
			{
				compression = this.GorillaCodec.FindPropAlgoByte(input);
			}
			if (AlgoModule.LempelZiv == (compression & AlgoModule.LempelZiv))
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid compression specified or computed by FindPropAlgoByte"));
			}
			int num = 0;
			int bitCount = 0;
			int num2 = 0;
			this.GorillaCodec.GetPropertyBitCount(compression, ref num, ref bitCount, ref num2);
			GorillaEncodingType encodingType = GorillaEncodingType.Byte;
			int num3 = input.Length;
			if (num == 4)
			{
				encodingType = GorillaEncodingType.Int;
				num3 >>= 2;
			}
			else if (num == 2)
			{
				encodingType = GorillaEncodingType.Short;
				num3 >>= 1;
			}
			BitStreamReader reader = new BitStreamReader(input);
			this.GorillaCodec.Compress(bitCount, reader, encodingType, num3, list);
			list[0] = compression;
			return list.ToArray();
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0015BCCC File Offset: 0x0015B0CC
		internal byte[] DecompressPropertyData(byte[] input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 2)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("input.Length must be at least 2"));
			}
			byte b = input[0];
			int num = 1;
			if (AlgoModule.LempelZiv != (b & AlgoModule.LempelZiv))
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				this.GorillaCodec.GetPropertyBitCount(b, ref num2, ref num3, ref num4);
				GorillaEncodingType encodingType = GorillaEncodingType.Byte;
				if (num2 == 4)
				{
					encodingType = GorillaEncodingType.Int;
				}
				else if (num2 == 2)
				{
					encodingType = GorillaEncodingType.Short;
				}
				int unitsToDecode = (input.Length - num << 3) / num3 - num4;
				BitStreamReader reader = new BitStreamReader(input, num);
				return this.GorillaCodec.Uncompress(num3, reader, encodingType, unitsToDecode);
			}
			if ((b & ~(AlgoModule.LempelZiv != 0)) != 0)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("bogus isf, we don't decompress property data with lz"));
			}
			return this.LZCodec.Uncompress(input, num);
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x0015BD90 File Offset: 0x0015B190
		private HuffModule HuffModule
		{
			get
			{
				if (this._huffModule == null)
				{
					this._huffModule = new HuffModule();
				}
				return this._huffModule;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x0015BDB8 File Offset: 0x0015B1B8
		private MultiByteCodec MultiByteCodec
		{
			get
			{
				if (this._multiByteCodec == null)
				{
					this._multiByteCodec = new MultiByteCodec();
				}
				return this._multiByteCodec;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x0015BDE0 File Offset: 0x0015B1E0
		private DeltaDelta DeltaDelta
		{
			get
			{
				if (this._deltaDelta == null)
				{
					this._deltaDelta = new DeltaDelta();
				}
				return this._deltaDelta;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x0015BE08 File Offset: 0x0015B208
		private GorillaCodec GorillaCodec
		{
			get
			{
				if (this._gorillaCodec == null)
				{
					this._gorillaCodec = new GorillaCodec();
				}
				return this._gorillaCodec;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x0015BE30 File Offset: 0x0015B230
		private LZCodec LZCodec
		{
			get
			{
				if (this._lzCodec == null)
				{
					this._lzCodec = new LZCodec();
				}
				return this._lzCodec;
			}
		}

		// Token: 0x04002617 RID: 9751
		private HuffModule _huffModule;

		// Token: 0x04002618 RID: 9752
		private MultiByteCodec _multiByteCodec;

		// Token: 0x04002619 RID: 9753
		private DeltaDelta _deltaDelta;

		// Token: 0x0400261A RID: 9754
		private GorillaCodec _gorillaCodec;

		// Token: 0x0400261B RID: 9755
		private LZCodec _lzCodec;

		// Token: 0x0400261C RID: 9756
		internal static readonly byte NoCompression = 0;

		// Token: 0x0400261D RID: 9757
		internal static readonly byte DefaultCompression = 192;

		// Token: 0x0400261E RID: 9758
		internal static readonly byte IndexedHuffman = 128;

		// Token: 0x0400261F RID: 9759
		internal static readonly byte LempelZiv = 128;

		// Token: 0x04002620 RID: 9760
		internal static readonly byte DefaultBAACount = 8;

		// Token: 0x04002621 RID: 9761
		internal static readonly byte MaxBAACount = 10;

		// Token: 0x04002622 RID: 9762
		private static readonly double[] DefaultFirstSquareRoot = new double[]
		{
			1.0,
			1.0,
			1.0,
			4.0,
			9.0,
			16.0,
			36.0,
			49.0
		};
	}
}
