using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D5 RID: 2005
	internal static class StrokeSerializer
	{
		// Token: 0x06005474 RID: 21620 RVA: 0x0015AEAC File Offset: 0x0015A2AC
		internal static uint DecodeStroke(Stream stream, uint size, GuidList guidList, StrokeDescriptor strokeDescriptor, StylusPointDescription stylusPointDescription, DrawingAttributes drawingAttributes, Matrix transform, out Stroke stroke)
		{
			StylusPointCollection stylusPoints;
			ExtendedPropertyCollection extendedProperties;
			uint num = StrokeSerializer.DecodeISFIntoStroke(stream, size, guidList, strokeDescriptor, stylusPointDescription, transform, out stylusPoints, out extendedProperties);
			if (num != size)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage(string.Concat(new string[]
				{
					"Stroke size (",
					num.ToString(CultureInfo.InvariantCulture),
					") != expected (",
					size.ToString(CultureInfo.InvariantCulture),
					")"
				})));
			}
			stroke = new Stroke(stylusPoints, drawingAttributes, extendedProperties);
			return num;
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0015AF28 File Offset: 0x0015A328
		private static uint DecodeISFIntoStroke(Stream stream, uint totalBytesInStrokeBlockOfIsfStream, GuidList guidList, StrokeDescriptor strokeDescriptor, StylusPointDescription stylusPointDescription, Matrix transform, out StylusPointCollection stylusPoints, out ExtendedPropertyCollection extendedProperties)
		{
			stylusPoints = null;
			extendedProperties = null;
			if (totalBytesInStrokeBlockOfIsfStream == 0U)
			{
				return 0U;
			}
			uint num = StrokeSerializer.LoadPackets(stream, totalBytesInStrokeBlockOfIsfStream, stylusPointDescription, transform, out stylusPoints);
			if (num > totalBytesInStrokeBlockOfIsfStream)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Packet buffer overflowed the ISF stream"));
			}
			uint num2 = totalBytesInStrokeBlockOfIsfStream - num;
			if (num2 == 0U)
			{
				return num;
			}
			for (int i = 1; i < strokeDescriptor.Template.Count; i++)
			{
				if (num2 <= 0U)
				{
					break;
				}
				KnownTagCache.KnownTagIndex knownTagIndex = strokeDescriptor.Template[i - 1];
				if (knownTagIndex != KnownTagCache.KnownTagIndex.Buttons)
				{
					if (knownTagIndex != KnownTagCache.KnownTagIndex.StrokePropertyList)
					{
						Trace.WriteLine("Ignoring unhandled stroke tag in ISF stroke descriptor");
					}
					else
					{
						while (i < strokeDescriptor.Template.Count)
						{
							if (num2 <= 0U)
							{
								break;
							}
							knownTagIndex = strokeDescriptor.Template[i];
							Guid guid = guidList.FindGuid(knownTagIndex);
							if (guid == Guid.Empty)
							{
								throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Stroke Custom Attribute tag embedded in ISF stream does not match guid table"));
							}
							object value;
							num = ExtendedPropertySerializer.DecodeAsISF(stream, num2, guidList, knownTagIndex, ref guid, out value);
							if (extendedProperties == null)
							{
								extendedProperties = new ExtendedPropertyCollection();
							}
							extendedProperties[guid] = value;
							if (num > num2)
							{
								throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
							}
							num2 -= num;
							i++;
						}
					}
				}
				else
				{
					i = (int)((uint)i + (strokeDescriptor.Template[i] + 1U));
				}
			}
			while (num2 > 0U)
			{
				uint num3;
				num = SerializationHelper.Decode(stream, out num3);
				KnownTagCache.KnownTagIndex knownTagIndex2 = (KnownTagCache.KnownTagIndex)num3;
				if (num > num2)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
				}
				num2 -= num;
				if (knownTagIndex2 == KnownTagCache.KnownTagIndex.PointProperty)
				{
					uint num4;
					num = SerializationHelper.Decode(stream, out num4);
					if (num > num2)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
					}
					uint num6;
					for (num2 -= num; num2 > 0U; num2 -= num6)
					{
						num = SerializationHelper.Decode(stream, out num3);
						if (num > num2)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						num2 -= num;
						uint num5;
						num = SerializationHelper.Decode(stream, out num5);
						if (num > num2)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						num2 -= num;
						num = SerializationHelper.Decode(stream, out num6);
						if (num > num2)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						num2 -= num;
						num6 += 1U;
						if (num6 > num2)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						byte[] array = new byte[num6];
						uint num7 = StrokeCollectionSerializer.ReliableRead(stream, array, num6);
						if (num6 != num7)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Read different size from stream then expected"));
						}
						byte[] array2 = Compressor.DecompressPropertyData(array);
					}
				}
				else
				{
					Guid guid2 = guidList.FindGuid(knownTagIndex2);
					if (guid2 == Guid.Empty)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Stroke Custom Attribute tag embedded in ISF stream does not match guid table"));
					}
					object value2;
					num = ExtendedPropertySerializer.DecodeAsISF(stream, num2, guidList, knownTagIndex2, ref guid2, out value2);
					if (extendedProperties == null)
					{
						extendedProperties = new ExtendedPropertyCollection();
					}
					extendedProperties[guid2] = value2;
					if (num > num2)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("ExtendedProperty decoded totalBytesInStrokeBlockOfIsfStream exceeded ISF stream totalBytesInStrokeBlockOfIsfStream"));
					}
					num2 -= num;
				}
			}
			if (num2 != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
			}
			return totalBytesInStrokeBlockOfIsfStream;
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0015B20C File Offset: 0x0015A60C
		private static uint LoadPackets(Stream inputStream, uint totalBytesInStrokeBlockOfIsfStream, StylusPointDescription stylusPointDescription, Matrix transform, out StylusPointCollection stylusPoints)
		{
			stylusPoints = null;
			if (totalBytesInStrokeBlockOfIsfStream == 0U)
			{
				return 0U;
			}
			uint num2;
			uint num = SerializationHelper.Decode(inputStream, out num2);
			if (totalBytesInStrokeBlockOfIsfStream < num)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
			}
			uint num3 = totalBytesInStrokeBlockOfIsfStream - num;
			if (num3 == 0U)
			{
				return num;
			}
			int inputArrayLengthPerPoint = stylusPointDescription.GetInputArrayLengthPerPoint();
			int buttonCount = stylusPointDescription.ButtonCount;
			int num4 = (buttonCount > 0) ? 1 : 0;
			int num5 = inputArrayLengthPerPoint - num4;
			int[] array = new int[(ulong)num2 * (ulong)((long)inputArrayLengthPerPoint)];
			int[] array2 = new int[num2];
			byte[] array3 = new byte[num3];
			uint num6 = StrokeCollectionSerializer.ReliableRead(inputStream, array3, num3);
			if (num6 != num3)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
			}
			int originalPressureIndex = stylusPointDescription.OriginalPressureIndex;
			int num7 = 0;
			while (num7 < num5 && num3 > 0U)
			{
				num = num3;
				Compressor.DecompressPacketData(array3, ref num, array2);
				if (num > num3)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
				}
				int num8 = num7;
				if (num8 > 1 && originalPressureIndex != -1 && originalPressureIndex != StylusPointDescription.RequiredPressureIndex)
				{
					if (num8 == originalPressureIndex)
					{
						num8 = 2;
					}
					else if (num8 < originalPressureIndex)
					{
						num8++;
					}
				}
				num3 -= num;
				int num9 = 0;
				int num10 = 0;
				while ((long)num9 < (long)((ulong)num2))
				{
					array[num10 + num8] = array2[num9];
					num9++;
					num10 += inputArrayLengthPerPoint;
				}
				for (uint num11 = 0U; num11 < num3; num11 += 1U)
				{
					array3[(int)num11] = array3[(int)(checked((IntPtr)(unchecked((ulong)num11 + (ulong)((long)num)))))];
				}
				num7++;
			}
			if (num3 != 0U && buttonCount > 0)
			{
				int num12 = buttonCount / 8;
				int num13 = buttonCount % 8;
				num = (uint)(((long)buttonCount * (long)((ulong)num2) + 7L) / 8L);
				if (num > num3)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Buffer range is smaller than expected expected size"));
				}
				num3 -= num;
				int num14 = (buttonCount + 7) / 8;
				byte[] array4 = new byte[(ulong)num2 * (ulong)((long)num14)];
				BitStreamReader bitStreamReader = new BitStreamReader(array3, (uint)(buttonCount * (int)num2));
				int num15 = 0;
				while (!bitStreamReader.EndOfStream)
				{
					for (int i = 0; i < num12; i++)
					{
						array4[num15++] = bitStreamReader.ReadByte(8);
					}
					if (num13 > 0)
					{
						array4[num15++] = bitStreamReader.ReadByte(num13);
					}
				}
				if (num15 != array4.Length)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Button data length not equal to expected length"));
				}
				StrokeSerializer.FillButtonData((int)num2, buttonCount, num5, array, array4);
			}
			stylusPoints = new StylusPointCollection(stylusPointDescription, array, null, transform);
			if (num3 != 0U)
			{
				inputStream.Seek((long)(0UL - (ulong)num3), SeekOrigin.Current);
			}
			return totalBytesInStrokeBlockOfIsfStream - num3;
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0015B454 File Offset: 0x0015A854
		private static void FillButtonData(int pointCount, int buttonCount, int buttonIndex, int[] packets, byte[] buttonData)
		{
			int num = buttonIndex + 1;
			int num2 = buttonIndex;
			if (buttonData != null)
			{
				int num3 = (buttonCount + 7) / 8;
				int num4 = (int)((long)num3 / (long)((ulong)Native.SizeOfInt));
				int num5 = (int)((long)num3 % (long)((ulong)Native.SizeOfInt));
				int i = 0;
				while (i < num4)
				{
					int num6 = (int)((long)i * (long)((ulong)Native.SizeOfInt));
					int num7 = num2;
					int j = 0;
					while (j < pointCount)
					{
						packets[num7] = ((int)buttonData[num6] << 24 | (int)buttonData[num6 + 1] << 16 | (int)buttonData[num6 + 2] << 8 | (int)buttonData[num6 + 3]);
						j++;
						num7 += num;
						num6 += num3;
					}
					i++;
					num2++;
				}
				if (0 < num5)
				{
					int num6 = (int)((long)num4 * (long)((ulong)Native.SizeOfInt));
					int num7 = num2;
					int k = 0;
					while (k < pointCount)
					{
						uint num8 = (uint)buttonData[num6];
						for (int l = 1; l < num5; l++)
						{
							num8 = (num8 << 8 | (uint)buttonData[num6 + l]);
						}
						packets[num7] = (int)num8;
						k++;
						num7 += num;
						num6 += num3;
					}
				}
			}
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0015B548 File Offset: 0x0015A948
		internal static uint EncodeStroke(Stroke stroke, Stream stream, byte compressionAlgorithm, GuidList guidList, StrokeCollectionSerializer.StrokeLookupEntry strokeLookupEntry)
		{
			uint num = StrokeSerializer.SavePackets(stroke, stream, strokeLookupEntry);
			if (stroke.ExtendedProperties.Count > 0)
			{
				num += ExtendedPropertySerializer.EncodeAsISF(stroke.ExtendedProperties, stream, guidList, compressionAlgorithm, false);
			}
			return num;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x0015B580 File Offset: 0x0015A980
		internal static void BuildStrokeDescriptor(Stroke stroke, GuidList guidList, StrokeCollectionSerializer.StrokeLookupEntry strokeLookupEntry, out StrokeDescriptor strokeDescriptor, out MetricBlock metricBlock)
		{
			metricBlock = new MetricBlock();
			strokeDescriptor = new StrokeDescriptor();
			StylusPointDescription description = stroke.StylusPoints.Description;
			KnownTagCache.KnownTagIndex knownTagIndex = guidList.FindTag(KnownIds.X, true);
			MetricEntryType metricEntryType = metricBlock.AddMetricEntry(description.GetPropertyInfo(StylusPointProperties.X), knownTagIndex);
			knownTagIndex = guidList.FindTag(KnownIds.Y, true);
			metricEntryType = metricBlock.AddMetricEntry(description.GetPropertyInfo(StylusPointProperties.Y), knownTagIndex);
			ReadOnlyCollection<StylusPointPropertyInfo> stylusPointProperties = description.GetStylusPointProperties();
			for (int i = 2; i < stylusPointProperties.Count; i++)
			{
				if (i != StylusPointDescription.RequiredPressureIndex || strokeLookupEntry.StorePressure)
				{
					StylusPointPropertyInfo stylusPointPropertyInfo = stylusPointProperties[i];
					if (stylusPointPropertyInfo.IsButton)
					{
						break;
					}
					knownTagIndex = guidList.FindTag(stylusPointPropertyInfo.Id, true);
					strokeDescriptor.Template.Add(knownTagIndex);
					strokeDescriptor.Size += SerializationHelper.VarSize((uint)knownTagIndex);
					metricEntryType = metricBlock.AddMetricEntry(stylusPointPropertyInfo, knownTagIndex);
				}
			}
			if (stroke.ExtendedProperties.Count > 0)
			{
				strokeDescriptor.Template.Add(KnownTagCache.KnownTagIndex.StrokePropertyList);
				strokeDescriptor.Size += SerializationHelper.VarSize(11U);
				for (int j = 0; j < stroke.ExtendedProperties.Count; j++)
				{
					knownTagIndex = guidList.FindTag(stroke.ExtendedProperties[j].Id, false);
					strokeDescriptor.Template.Add(knownTagIndex);
					strokeDescriptor.Size += SerializationHelper.VarSize((uint)knownTagIndex);
				}
			}
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0015B6EC File Offset: 0x0015AAEC
		private static uint SavePackets(Stroke stroke, Stream stream, StrokeCollectionSerializer.StrokeLookupEntry strokeLookupEntry)
		{
			uint count = (uint)stroke.StylusPoints.Count;
			uint num = (stream != null) ? SerializationHelper.Encode(stream, count) : SerializationHelper.VarSize(count);
			int[][] isfreadyStrokeData = strokeLookupEntry.ISFReadyStrokeData;
			ReadOnlyCollection<StylusPointPropertyInfo> stylusPointProperties = stroke.StylusPoints.Description.GetStylusPointProperties();
			for (int i = 0; i < stylusPointProperties.Count; i++)
			{
				StylusPointPropertyInfo stylusPointPropertyInfo = stylusPointProperties[i];
				if (i != 2 || strokeLookupEntry.StorePressure)
				{
					if (stylusPointPropertyInfo.IsButton)
					{
						break;
					}
					byte compressionData = strokeLookupEntry.CompressionData;
					num += StrokeSerializer.SavePacketPropertyData(isfreadyStrokeData[i], stream, stylusPointPropertyInfo.Id, ref compressionData);
				}
			}
			return num;
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0015B780 File Offset: 0x0015AB80
		private static uint SavePacketPropertyData(int[] packetdata, Stream stream, Guid guid, ref byte algo)
		{
			if (packetdata.Length == 0)
			{
				return 0U;
			}
			byte[] array = Compressor.CompressPacketData(packetdata, ref algo);
			stream.Write(array, 0, array.Length);
			return (uint)array.Length;
		}
	}
}
