using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Ink;
using System.Windows.Media;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007C8 RID: 1992
	internal static class DrawingAttributeSerializer
	{
		// Token: 0x06005412 RID: 21522 RVA: 0x00155A48 File Offset: 0x00154E48
		internal static uint DecodeAsISF(Stream stream, GuidList guidList, uint maximumStreamSize, DrawingAttributes da)
		{
			DrawingAttributeSerializer.PenTip penTip = DrawingAttributeSerializer.PenTip.Circle;
			double num = DrawingAttributeSerializer.V1PenWidthWhenWidthIsMissing;
			double num2 = DrawingAttributeSerializer.V1PenHeightWhenHeightIsMissing;
			uint num3 = DrawingAttributeSerializer.RasterOperationDefaultV1;
			int num4 = DrawingAttributeSerializer.TransparencyDefaultV1;
			bool flag = false;
			bool flag2 = false;
			uint result = maximumStreamSize;
			while (maximumStreamSize > 0U)
			{
				uint num6;
				uint num5 = SerializationHelper.Decode(stream, out num6);
				KnownTagCache.KnownTagIndex tag = (KnownTagCache.KnownTagIndex)num6;
				if (maximumStreamSize < num5)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("ISF size is larger than maximum stream size"));
				}
				maximumStreamSize -= num5;
				Guid guid = guidList.FindGuid(tag);
				if (guid == Guid.Empty)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Drawing Attribute tag embedded in ISF stream does not match guid table"));
				}
				uint num7 = 0U;
				if (KnownIds.PenTip == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					penTip = (DrawingAttributeSerializer.PenTip)num7;
					if (!DrawingAttributeSerializer.PenTipHelper.IsDefined(penTip))
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid PenTip value found in ISF stream"));
					}
					maximumStreamSize -= num5;
				}
				else if (KnownIds.PenStyle == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					maximumStreamSize -= num5;
				}
				else if (KnownIds.DrawingFlags == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					DrawingFlags drawingFlags = (DrawingFlags)num7;
					da.DrawingFlags = drawingFlags;
					maximumStreamSize -= num5;
				}
				else if (KnownIds.RasterOperation == guid)
				{
					uint dataSizeIfKnownGuid = GuidList.GetDataSizeIfKnownGuid(KnownIds.RasterOperation);
					if (dataSizeIfKnownGuid == 0U)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("ROP data size was not found"));
					}
					byte[] array = new byte[dataSizeIfKnownGuid];
					stream.Read(array, 0, (int)dataSizeIfKnownGuid);
					if (array != null && array.Length != 0)
					{
						num3 = Convert.ToUInt32(array[0]);
					}
					maximumStreamSize -= dataSizeIfKnownGuid;
				}
				else if (KnownIds.CurveFittingError == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					da.FittingError = (int)num7;
					maximumStreamSize -= num5;
				}
				else if (KnownIds.StylusHeight == guid || KnownIds.StylusWidth == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					double num8 = num7;
					maximumStreamSize -= num5;
					if (maximumStreamSize > 0U)
					{
						num5 = SerializationHelper.Decode(stream, out num7);
						maximumStreamSize -= num5;
						if (27U == num7)
						{
							uint num9;
							num5 = SerializationHelper.Decode(stream, out num9);
							maximumStreamSize -= num5;
							num9 += 1U;
							if (num9 > maximumStreamSize)
							{
								throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("ISF size if greater then maximum stream size"));
							}
							byte[] array2 = new byte[num9];
							uint num10 = (uint)stream.Read(array2, 0, (int)num9);
							if (num9 != num10)
							{
								throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Read different size from stream then expected"));
							}
							byte[] buffer = Compressor.DecompressPropertyData(array2);
							using (MemoryStream memoryStream = new MemoryStream(buffer))
							{
								using (BinaryReader binaryReader = new BinaryReader(memoryStream))
								{
									short num11 = binaryReader.ReadInt16();
									num8 += (double)((float)num11 / DrawingAttributes.StylusPrecision);
									maximumStreamSize -= num9;
									goto IL_294;
								}
							}
						}
						stream.Seek((long)(-(long)((ulong)num5)), SeekOrigin.Current);
						maximumStreamSize += num5;
					}
					IL_294:
					if (KnownIds.StylusWidth == guid)
					{
						flag = true;
						num = num8;
					}
					else
					{
						flag2 = true;
						num2 = num8;
					}
				}
				else if (KnownIds.Transparency == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					num4 = (int)num7;
					maximumStreamSize -= num5;
				}
				else if (KnownIds.Color == guid)
				{
					num5 = SerializationHelper.Decode(stream, out num7);
					Color color = Color.FromRgb((byte)(num7 & 255U), (byte)((num7 & 65280U) >> 8), (byte)((num7 & 16711680U) >> 16));
					da.Color = color;
					maximumStreamSize -= num5;
				}
				else
				{
					if (KnownIds.StylusTipTransform == guid)
					{
						try
						{
							object obj;
							num5 = ExtendedPropertySerializer.DecodeAsISF(stream, maximumStreamSize, guidList, tag, ref guid, out obj);
							Matrix stylusTipTransform = Matrix.Parse((string)obj);
							da.StylusTipTransform = stylusTipTransform;
							continue;
						}
						catch (InvalidOperationException)
						{
							continue;
						}
						finally
						{
							maximumStreamSize -= num5;
						}
					}
					object propertyData;
					num5 = ExtendedPropertySerializer.DecodeAsISF(stream, maximumStreamSize, guidList, tag, ref guid, out propertyData);
					maximumStreamSize -= num5;
					da.AddPropertyData(guid, propertyData);
				}
			}
			if (maximumStreamSize != 0U)
			{
				throw new ArgumentException();
			}
			if (penTip == DrawingAttributeSerializer.PenTip.Circle)
			{
				if (da.StylusTip != StylusTip.Ellipse)
				{
					da.StylusTip = StylusTip.Ellipse;
				}
			}
			else if (da.StylusTip == StylusTip.Ellipse)
			{
				da.StylusTip = StylusTip.Rectangle;
			}
			if (da.StylusTip == StylusTip.Ellipse && flag && !flag2)
			{
				num2 = num;
				da.HeightChangedForCompatabity = true;
			}
			num2 *= StrokeCollectionSerializer.HimetricToAvalonMultiplier;
			num *= StrokeCollectionSerializer.HimetricToAvalonMultiplier;
			double heightOrWidth = DoubleUtil.IsZero(num2) ? ((double)DrawingAttributes.GetDefaultDrawingAttributeValue(KnownIds.StylusHeight)) : num2;
			double heightOrWidth2 = DoubleUtil.IsZero(num) ? ((double)DrawingAttributes.GetDefaultDrawingAttributeValue(KnownIds.StylusWidth)) : num;
			da.Height = DrawingAttributeSerializer.GetCappedHeightOrWidth(heightOrWidth);
			da.Width = DrawingAttributeSerializer.GetCappedHeightOrWidth(heightOrWidth2);
			da.RasterOperation = num3;
			if (num3 == DrawingAttributeSerializer.RasterOperationDefaultV1)
			{
				if (da.ContainsPropertyData(KnownIds.IsHighlighter))
				{
					da.RemovePropertyData(KnownIds.IsHighlighter);
				}
			}
			else if (num3 == DrawingAttributeSerializer.RasterOperationMaskPen)
			{
				da.IsHighlighter = true;
			}
			if (num4 > DrawingAttributeSerializer.TransparencyDefaultV1)
			{
				int value = MathHelper.AbsNoThrow(num4 - 255);
				Color color2 = da.Color;
				color2.A = Convert.ToByte(value);
				da.Color = color2;
			}
			return result;
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00155F6C File Offset: 0x0015536C
		internal static double GetCappedHeightOrWidth(double heightOrWidth)
		{
			if (heightOrWidth > DrawingAttributes.MaxHeight)
			{
				return DrawingAttributes.MaxHeight;
			}
			if (heightOrWidth < DrawingAttributes.MinHeight)
			{
				return DrawingAttributes.MinHeight;
			}
			return heightOrWidth;
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x00155F98 File Offset: 0x00155398
		internal static uint EncodeAsISF(DrawingAttributes da, Stream stream, GuidList guidList, byte compressionAlgorithm, bool fTag)
		{
			uint result = 0U;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			DrawingAttributeSerializer.PersistDrawingFlags(da, stream, guidList, ref result, ref binaryWriter);
			DrawingAttributeSerializer.PersistColorAndTransparency(da, stream, guidList, ref result, ref binaryWriter);
			DrawingAttributeSerializer.PersistRasterOperation(da, stream, guidList, ref result, ref binaryWriter);
			DrawingAttributeSerializer.PersistWidthHeight(da, stream, guidList, ref result, ref binaryWriter);
			DrawingAttributeSerializer.PersistStylusTip(da, stream, guidList, ref result, ref binaryWriter);
			DrawingAttributeSerializer.PersistExtendedProperties(da, stream, guidList, ref result, ref binaryWriter, compressionAlgorithm, fTag);
			return result;
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x00155FFC File Offset: 0x001553FC
		private static void PersistDrawingFlags(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw)
		{
			cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.DrawingFlags, true));
			cbData += SerializationHelper.Encode(stream, (uint)da.DrawingFlags);
			if (da.ContainsPropertyData(KnownIds.CurveFittingError))
			{
				cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.CurveFittingError, true));
				cbData += SerializationHelper.Encode(stream, (uint)((int)da.GetPropertyData(KnownIds.CurveFittingError)));
			}
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x00156070 File Offset: 0x00155470
		private static void PersistColorAndTransparency(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw)
		{
			if (da.ContainsPropertyData(KnownIds.Color))
			{
				Color color = da.Color;
				uint r = (uint)color.R;
				uint g = (uint)color.G;
				uint b = (uint)color.B;
				uint value = r + (g << 8) + (b << 16);
				cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.Color, true));
				cbData += SerializationHelper.Encode(stream, value);
			}
			byte a = da.Color.A;
			if (a != 255)
			{
				int value2 = MathHelper.AbsNoThrow((int)(a - byte.MaxValue));
				cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.Transparency, true));
				cbData += SerializationHelper.Encode(stream, Convert.ToUInt32(value2));
			}
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0015612C File Offset: 0x0015552C
		private static void PersistRasterOperation(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw)
		{
			if (da.RasterOperation != DrawingAttributeSerializer.RasterOperationDefaultV1)
			{
				uint dataSizeIfKnownGuid = GuidList.GetDataSizeIfKnownGuid(KnownIds.RasterOperation);
				if (dataSizeIfKnownGuid == 0U)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("ROP data size was not found"));
				}
				cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.RasterOperation, true));
				long position = stream.Position;
				bw.Write(da.RasterOperation);
				if ((uint)(stream.Position - position) != dataSizeIfKnownGuid)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("ROP data was incorrectly serialized"));
				}
				cbData += dataSizeIfKnownGuid;
			}
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x001561B4 File Offset: 0x001555B4
		private static void PersistExtendedProperties(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw, byte compressionAlgorithm, bool fTag)
		{
			ExtendedPropertyCollection extendedPropertyCollection = da.CopyPropertyData();
			for (int i = extendedPropertyCollection.Count - 1; i >= 0; i--)
			{
				if (extendedPropertyCollection[i].Id == KnownIds.StylusTipTransform)
				{
					string value = ((Matrix)extendedPropertyCollection[i].Value).ToString(CultureInfo.InvariantCulture);
					extendedPropertyCollection[i].Value = value;
				}
				else if (DrawingAttributes.RemoveIdFromExtendedProperties(extendedPropertyCollection[i].Id))
				{
					extendedPropertyCollection.Remove(extendedPropertyCollection[i].Id);
				}
			}
			cbData += ExtendedPropertySerializer.EncodeAsISF(extendedPropertyCollection, stream, guidList, compressionAlgorithm, fTag);
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00156258 File Offset: 0x00155658
		private static void PersistStylusTip(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw)
		{
			if (da.ContainsPropertyData(KnownIds.StylusTip))
			{
				cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(KnownIds.PenTip, true));
				cbData += SerializationHelper.Encode(stream, 1U);
				using (MemoryStream memoryStream = new MemoryStream(6))
				{
					int num = Convert.ToInt32(da.StylusTip, CultureInfo.InvariantCulture);
					VarEnum type = SerializationHelper.ConvertToVarEnum(DrawingAttributeSerializer.PersistenceTypes.StylusTip, true);
					ExtendedPropertySerializer.EncodeAttribute(KnownIds.StylusTip, num, type, memoryStream);
					cbData += ExtendedPropertySerializer.EncodeAsISF(KnownIds.StylusTip, memoryStream.ToArray(), stream, guidList, 0, true);
				}
			}
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00156314 File Offset: 0x00155714
		private static void PersistWidthHeight(DrawingAttributes da, Stream stream, GuidList guidList, ref uint cbData, ref BinaryWriter bw)
		{
			double width = da.Width;
			double height = da.Height;
			for (int i = 0; i < 2; i++)
			{
				Guid guid = (i == 0) ? KnownIds.StylusWidth : KnownIds.StylusHeight;
				double num = (i == 0) ? width : height;
				num *= StrokeCollectionSerializer.AvalonToHimetricMultiplier;
				double value = (i == 0) ? DrawingAttributeSerializer.V1PenWidthWhenWidthIsMissing : DrawingAttributeSerializer.V1PenHeightWhenHeightIsMissing;
				bool flag = DoubleUtil.AreClose(num, value);
				if (width == height && da.StylusTip == StylusTip.Ellipse && guid == KnownIds.StylusHeight && da.HeightChangedForCompatabity)
				{
					flag = true;
				}
				if (!flag)
				{
					uint num2 = (uint)(num + 0.5);
					cbData += SerializationHelper.Encode(stream, (uint)guidList.FindTag(guid, true));
					cbData += SerializationHelper.Encode(stream, num2);
					short num3 = (num > num2) ? ((short)((double)DrawingAttributes.StylusPrecision * (num - num2) + 0.5)) : ((short)((double)DrawingAttributes.StylusPrecision * (num - num2) - 0.5));
					if (num3 != 0)
					{
						uint sizeOfUShort = Native.SizeOfUShort;
						cbData += SerializationHelper.Encode(stream, 27U);
						cbData += SerializationHelper.Encode(stream, sizeOfUShort);
						bw.Write(0);
						bw.Write(num3);
						cbData += sizeOfUShort + 1U;
					}
				}
			}
		}

		// Token: 0x040025DC RID: 9692
		private static readonly double V1PenWidthWhenWidthIsMissing = 25.0;

		// Token: 0x040025DD RID: 9693
		private static readonly double V1PenHeightWhenHeightIsMissing = 25.0;

		// Token: 0x040025DE RID: 9694
		private static readonly int TransparencyDefaultV1 = 0;

		// Token: 0x040025DF RID: 9695
		internal static readonly uint RasterOperationMaskPen = 9U;

		// Token: 0x040025E0 RID: 9696
		internal static readonly uint RasterOperationDefaultV1 = 13U;

		// Token: 0x02000A07 RID: 2567
		private enum PenTip
		{
			// Token: 0x04002F4B RID: 12107
			Circle,
			// Token: 0x04002F4C RID: 12108
			Rectangle,
			// Token: 0x04002F4D RID: 12109
			Default = 0
		}

		// Token: 0x02000A08 RID: 2568
		private static class PenTipHelper
		{
			// Token: 0x06005C07 RID: 23559 RVA: 0x00171D48 File Offset: 0x00171148
			internal static bool IsDefined(DrawingAttributeSerializer.PenTip penTip)
			{
				return penTip >= DrawingAttributeSerializer.PenTip.Circle && penTip <= DrawingAttributeSerializer.PenTip.Rectangle;
			}
		}

		// Token: 0x02000A09 RID: 2569
		private enum PenStyle
		{
			// Token: 0x04002F4F RID: 12111
			Cosmetic,
			// Token: 0x04002F50 RID: 12112
			Geometric = 65536,
			// Token: 0x04002F51 RID: 12113
			Default = 65536
		}

		// Token: 0x02000A0A RID: 2570
		internal static class PersistenceTypes
		{
			// Token: 0x04002F52 RID: 12114
			public static readonly Type StylusTip = typeof(int);

			// Token: 0x04002F53 RID: 12115
			public static readonly Type IsHollow = typeof(bool);

			// Token: 0x04002F54 RID: 12116
			public static readonly Type StylusTipTransform = typeof(string);
		}
	}
}
