using System;
using System.IO;
using System.Runtime.InteropServices;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D3 RID: 2003
	internal static class SerializationHelper
	{
		// Token: 0x06005467 RID: 21607 RVA: 0x0015A9A4 File Offset: 0x00159DA4
		public static uint VarSize(uint Value)
		{
			if (Value < 128U)
			{
				return 1U;
			}
			if (Value < 16384U)
			{
				return 2U;
			}
			if (Value < 2097152U)
			{
				return 3U;
			}
			if (Value < 268435456U)
			{
				return 4U;
			}
			return 5U;
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x0015A9DC File Offset: 0x00159DDC
		public static uint Encode(Stream strm, uint Value)
		{
			ulong num = 0UL;
			while (Value >= 128U)
			{
				strm.WriteByte((byte)(128U | (Value & 127U)));
				Value >>= 7;
				num += 1UL;
			}
			strm.WriteByte((byte)Value);
			return (uint)(num + 1UL);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0015AA20 File Offset: 0x00159E20
		public static uint EncodeLarge(Stream strm, ulong ulValue)
		{
			uint num = 0U;
			while (ulValue >= 128UL)
			{
				strm.WriteByte((byte)(128UL | (ulValue & 127UL)));
				ulValue >>= 7;
				num += 1U;
			}
			strm.WriteByte((byte)ulValue);
			return num + 1U;
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0015AA64 File Offset: 0x00159E64
		public static uint SignEncode(Stream strm, int Value)
		{
			ulong num;
			if (-2147483648 == Value)
			{
				num = 4294967297UL;
			}
			else
			{
				num = (ulong)((long)Math.Abs(Value));
				num <<= 1;
				if (Value < 0)
				{
					num |= 1UL;
				}
			}
			return SerializationHelper.EncodeLarge(strm, num);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0015AAA4 File Offset: 0x00159EA4
		public static uint Decode(Stream strm, out uint dw)
		{
			int num = 0;
			uint num2 = 0U;
			dw = 0U;
			byte b;
			do
			{
				b = (byte)strm.ReadByte();
				num2 += 1U;
				dw += (uint)((uint)(b & 127) << num);
				num += 7;
			}
			while ((b & 128) > 0 && num < 29);
			return num2;
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0015AAE8 File Offset: 0x00159EE8
		public static uint DecodeLarge(Stream strm, out ulong ull)
		{
			int num = 0;
			uint num2 = 0U;
			ull = 0UL;
			byte b;
			do
			{
				b = (byte)strm.ReadByte();
				num2 += 1U;
				byte b2 = b & 127;
				long num3 = (long)((ulong)b2);
				ull |= (ulong)((ulong)num3 << num);
				num += 7;
			}
			while ((b & 128) > 0 && num < 57);
			return num2;
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0015AB38 File Offset: 0x00159F38
		public static uint SignDecode(Stream strm, out int i)
		{
			i = 0;
			ulong num = 0UL;
			uint num2 = SerializationHelper.DecodeLarge(strm, out num);
			if (num2 > 0U)
			{
				bool flag = false;
				if ((num & 1UL) > 0UL)
				{
					flag = true;
				}
				num >>= 1;
				long num3 = (long)num;
				i = (int)(flag ? (-(int)num3) : num3);
			}
			return num2;
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0015AB78 File Offset: 0x00159F78
		public static VarEnum ConvertToVarEnum(Type type, bool throwOnError)
		{
			if (typeof(char) == type)
			{
				return VarEnum.VT_I1;
			}
			if (typeof(char[]) == type)
			{
				return (VarEnum)8208;
			}
			if (typeof(byte) == type)
			{
				return VarEnum.VT_UI1;
			}
			if (typeof(byte[]) == type)
			{
				return (VarEnum)8209;
			}
			if (typeof(short) == type)
			{
				return VarEnum.VT_I2;
			}
			if (typeof(short[]) == type)
			{
				return (VarEnum)8194;
			}
			if (typeof(ushort) == type)
			{
				return VarEnum.VT_UI2;
			}
			if (typeof(ushort[]) == type)
			{
				return (VarEnum)8210;
			}
			if (typeof(int) == type)
			{
				return VarEnum.VT_I4;
			}
			if (typeof(int[]) == type)
			{
				return (VarEnum)8195;
			}
			if (typeof(uint) == type)
			{
				return VarEnum.VT_UI4;
			}
			if (typeof(uint[]) == type)
			{
				return (VarEnum)8211;
			}
			if (typeof(long) == type)
			{
				return VarEnum.VT_I8;
			}
			if (typeof(long[]) == type)
			{
				return (VarEnum)8212;
			}
			if (typeof(ulong) == type)
			{
				return VarEnum.VT_UI8;
			}
			if (typeof(ulong[]) == type)
			{
				return (VarEnum)8213;
			}
			if (typeof(float) == type)
			{
				return VarEnum.VT_R4;
			}
			if (typeof(float[]) == type)
			{
				return (VarEnum)8196;
			}
			if (typeof(double) == type)
			{
				return VarEnum.VT_R8;
			}
			if (typeof(double[]) == type)
			{
				return (VarEnum)8197;
			}
			if (typeof(DateTime) == type)
			{
				return VarEnum.VT_DATE;
			}
			if (typeof(DateTime[]) == type)
			{
				return (VarEnum)8199;
			}
			if (typeof(bool) == type)
			{
				return VarEnum.VT_BOOL;
			}
			if (typeof(bool[]) == type)
			{
				return (VarEnum)8203;
			}
			if (typeof(string) == type)
			{
				return VarEnum.VT_BSTR;
			}
			if (typeof(decimal) == type)
			{
				return VarEnum.VT_DECIMAL;
			}
			if (typeof(decimal[]) == type)
			{
				return (VarEnum)8206;
			}
			if (throwOnError)
			{
				throw new ArgumentException(SR.Get("InvalidDataTypeForExtendedProperty"));
			}
			return VarEnum.VT_UNKNOWN;
		}
	}
}
