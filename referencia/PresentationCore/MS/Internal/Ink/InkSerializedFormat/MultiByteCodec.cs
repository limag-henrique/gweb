using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007E1 RID: 2017
	internal class MultiByteCodec
	{
		// Token: 0x060054B4 RID: 21684 RVA: 0x0015CF08 File Offset: 0x0015C308
		internal MultiByteCodec()
		{
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0015CF1C File Offset: 0x0015C31C
		internal void Encode(uint data, List<byte> output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			while (data > 127U)
			{
				byte item = 128 | ((byte)data & 127);
				output.Add(item);
				data >>= 7;
			}
			byte item2 = (byte)(data & 127U);
			output.Add(item2);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0015CF64 File Offset: 0x0015C364
		internal void SignEncode(int data, List<byte> output)
		{
			uint data2;
			if (data < 0)
			{
				data2 = (uint)(-data << 1 | 1);
			}
			else
			{
				data2 = (uint)((uint)data << 1);
			}
			this.Encode(data2, output);
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0015CF8C File Offset: 0x0015C38C
		internal uint Decode(byte[] input, int inputIndex, ref uint data)
		{
			uint num = (uint)((input.Length - inputIndex > 5) ? 5 : (input.Length - inputIndex));
			uint num2 = 0U;
			data = 0U;
			while (num2 < num && input[(int)num2] > 127)
			{
				int num3 = (int)(num2 * 7U);
				data |= (uint)((uint)(input[(int)num2] & 127) << num3);
				num2 += 1U;
			}
			if (num2 < num)
			{
				int num4 = (int)(num2 * 7U);
				data |= (uint)((uint)(input[(int)num2] & 127) << num4);
				return num2 + 1U;
			}
			throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("invalid input in MultiByteCodec.Decode"));
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0015D004 File Offset: 0x0015C404
		internal uint SignDecode(byte[] input, int inputIndex, ref int data)
		{
			if (inputIndex >= input.Length)
			{
				throw new ArgumentOutOfRangeException("inputIndex");
			}
			uint num = 0U;
			uint result = this.Decode(input, inputIndex, ref num);
			data = (int)(((1U & num) != 0U) ? (-(int)(num >> 1)) : (num >> 1));
			return result;
		}
	}
}
