using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Ink;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007C7 RID: 1991
	internal static class ExtendedPropertySerializer
	{
		// Token: 0x06005408 RID: 21512 RVA: 0x00154780 File Offset: 0x00153B80
		private static bool UsesEmbeddedTypeInformation(Guid propGuid)
		{
			for (int i = 0; i < KnownIdCache.OriginalISFIdTable.Length; i++)
			{
				if (propGuid.Equals(KnownIdCache.OriginalISFIdTable[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < KnownIdCache.TabletInternalIdTable.Length; j++)
			{
				if (propGuid.Equals(KnownIdCache.TabletInternalIdTable[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x001547E0 File Offset: 0x00153BE0
		internal static void EncodeToStream(ExtendedProperty attribute, Stream stream)
		{
			object value = attribute.Value;
			VarEnum type;
			if (attribute.Id == KnownIds.DrawingFlags)
			{
				type = VarEnum.VT_I4;
			}
			else if (attribute.Id == KnownIds.StylusTip)
			{
				type = VarEnum.VT_I4;
			}
			else if (ExtendedPropertySerializer.UsesEmbeddedTypeInformation(attribute.Id))
			{
				type = SerializationHelper.ConvertToVarEnum(attribute.Value.GetType(), true);
			}
			else
			{
				type = (VarEnum)8209;
			}
			ExtendedPropertySerializer.EncodeAttribute(attribute.Id, value, type, stream);
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00154858 File Offset: 0x00153C58
		internal static void EncodeAttribute(Guid guid, object value, VarEnum type, Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (ExtendedPropertySerializer.UsesEmbeddedTypeInformation(guid))
			{
				ushort value2 = (ushort)type;
				binaryWriter.Write(value2);
			}
			switch (type)
			{
			case VarEnum.VT_I2:
			{
				short value3 = (short)value;
				binaryWriter.Write(value3);
				return;
			}
			case VarEnum.VT_I4:
			{
				int value4 = (int)value;
				binaryWriter.Write(value4);
				return;
			}
			case VarEnum.VT_R4:
			{
				float value5 = (float)value;
				binaryWriter.Write(value5);
				return;
			}
			case VarEnum.VT_R8:
			{
				double value6 = (double)value;
				binaryWriter.Write(value6);
				return;
			}
			case VarEnum.VT_CY:
			case VarEnum.VT_DISPATCH:
			case VarEnum.VT_ERROR:
			case VarEnum.VT_VARIANT:
			case VarEnum.VT_UNKNOWN:
			case (VarEnum)15:
				break;
			case VarEnum.VT_DATE:
				binaryWriter.Write(((DateTime)value).ToOADate());
				return;
			case VarEnum.VT_BSTR:
			{
				string s = (string)value;
				binaryWriter.Write(Encoding.Unicode.GetBytes(s));
				return;
			}
			case VarEnum.VT_BOOL:
			{
				bool flag = (bool)value;
				if (flag)
				{
					binaryWriter.Write(byte.MaxValue);
					binaryWriter.Write(byte.MaxValue);
					return;
				}
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				return;
			}
			case VarEnum.VT_DECIMAL:
			{
				decimal value7 = (decimal)value;
				binaryWriter.Write(value7);
				return;
			}
			case VarEnum.VT_I1:
			{
				char ch = (char)value;
				binaryWriter.Write(ch);
				return;
			}
			case VarEnum.VT_UI1:
			{
				byte value8 = (byte)value;
				binaryWriter.Write(value8);
				return;
			}
			case VarEnum.VT_UI2:
			{
				ushort value9 = (ushort)value;
				binaryWriter.Write(value9);
				return;
			}
			case VarEnum.VT_UI4:
			{
				uint value10 = (uint)value;
				binaryWriter.Write(value10);
				return;
			}
			case VarEnum.VT_I8:
			{
				long value11 = (long)value;
				binaryWriter.Write(value11);
				return;
			}
			case VarEnum.VT_UI8:
			{
				ulong value12 = (ulong)value;
				binaryWriter.Write(value12);
				return;
			}
			default:
				switch (type)
				{
				case (VarEnum)8194:
				{
					short[] array = (short[])value;
					for (int i = 0; i < array.Length; i++)
					{
						binaryWriter.Write(array[i]);
					}
					return;
				}
				case (VarEnum)8195:
				{
					int[] array2 = (int[])value;
					for (int j = 0; j < array2.Length; j++)
					{
						binaryWriter.Write(array2[j]);
					}
					return;
				}
				case (VarEnum)8196:
				{
					float[] array3 = (float[])value;
					for (int k = 0; k < array3.Length; k++)
					{
						binaryWriter.Write(array3[k]);
					}
					return;
				}
				case (VarEnum)8197:
				{
					double[] array4 = (double[])value;
					for (int l = 0; l < array4.Length; l++)
					{
						binaryWriter.Write(array4[l]);
					}
					return;
				}
				case (VarEnum)8199:
				{
					DateTime[] array5 = (DateTime[])value;
					for (int m = 0; m < array5.Length; m++)
					{
						binaryWriter.Write(array5[m].ToOADate());
					}
					return;
				}
				case (VarEnum)8203:
				{
					bool[] array6 = (bool[])value;
					for (int n = 0; n < array6.Length; n++)
					{
						if (array6[n])
						{
							binaryWriter.Write(byte.MaxValue);
							binaryWriter.Write(byte.MaxValue);
						}
						else
						{
							binaryWriter.Write(0);
							binaryWriter.Write(0);
						}
					}
					return;
				}
				case (VarEnum)8206:
				{
					decimal[] array7 = (decimal[])value;
					for (int num = 0; num < array7.Length; num++)
					{
						binaryWriter.Write(array7[num]);
					}
					return;
				}
				case (VarEnum)8208:
				{
					char[] chars = (char[])value;
					binaryWriter.Write(chars);
					return;
				}
				case (VarEnum)8209:
				{
					byte[] buffer = (byte[])value;
					binaryWriter.Write(buffer);
					return;
				}
				case (VarEnum)8210:
				{
					ushort[] array8 = (ushort[])value;
					for (int num2 = 0; num2 < array8.Length; num2++)
					{
						binaryWriter.Write(array8[num2]);
					}
					return;
				}
				case (VarEnum)8211:
				{
					uint[] array9 = (uint[])value;
					for (int num3 = 0; num3 < array9.Length; num3++)
					{
						binaryWriter.Write(array9[num3]);
					}
					return;
				}
				case (VarEnum)8212:
				{
					long[] array10 = (long[])value;
					for (int num4 = 0; num4 < array10.Length; num4++)
					{
						binaryWriter.Write(array10[num4]);
					}
					return;
				}
				case (VarEnum)8213:
				{
					ulong[] array11 = (ulong[])value;
					for (int num5 = 0; num5 < array11.Length; num5++)
					{
						binaryWriter.Write(array11[num5]);
					}
					return;
				}
				}
				break;
			}
			throw new InvalidOperationException(SR.Get("InvalidEpInIsf"));
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00154C58 File Offset: 0x00154058
		internal static uint EncodeAsISF(Guid id, byte[] data, Stream strm, GuidList guidList, byte compressionAlgorithm, bool fTag)
		{
			uint num = 0U;
			uint num2 = GuidList.GetDataSizeIfKnownGuid(id);
			if (fTag)
			{
				uint value = (uint)guidList.FindTag(id, true);
				num += SerializationHelper.Encode(strm, value);
			}
			if (num2 == 0U)
			{
				num2 = (uint)data.Length;
				byte[] array = Compressor.CompressPropertyData(data, compressionAlgorithm);
				num += SerializationHelper.Encode(strm, (uint)(array.Length - 1));
				strm.Write(array, 0, array.Length);
				num += (uint)array.Length;
			}
			else
			{
				strm.Write(data, 0, data.Length);
				num += (uint)data.Length;
			}
			return num;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x00154CC8 File Offset: 0x001540C8
		internal static uint DecodeAsISF(Stream stream, uint cbSize, GuidList guidList, KnownTagCache.KnownTagIndex tag, ref Guid guid, out object data)
		{
			uint num = 0U;
			uint num2 = cbSize;
			if (cbSize == 0U)
			{
				throw new InvalidOperationException(SR.Get("EmptyDataToLoad"));
			}
			if (tag == KnownTagCache.KnownTagIndex.Unknown)
			{
				uint num4;
				uint num3 = SerializationHelper.Decode(stream, out num4);
				tag = (KnownTagCache.KnownTagIndex)num4;
				if (num3 > num2)
				{
					throw new ArgumentException(SR.Get("InvalidSizeSpecified"), "cbSize");
				}
				num2 -= num3;
				num += num3;
				guid = guidList.FindGuid(tag);
			}
			if (guid == Guid.Empty)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Custom Attribute tag embedded in ISF stream does not match guid table"), "tag");
			}
			uint dataSizeIfKnownGuid = GuidList.GetDataSizeIfKnownGuid(guid);
			if (dataSizeIfKnownGuid > num2)
			{
				throw new ArgumentException(SR.Get("InvalidSizeSpecified"), "cbSize");
			}
			if (dataSizeIfKnownGuid == 0U)
			{
				uint num3 = SerializationHelper.Decode(stream, out dataSizeIfKnownGuid);
				uint num5 = dataSizeIfKnownGuid + 1U;
				num += num3;
				num2 -= num3;
				if (num5 > num2)
				{
					throw new ArgumentException();
				}
				byte[] array = new byte[num5];
				uint num6 = (uint)stream.Read(array, 0, (int)num5);
				if (num5 != num6)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Read different size from stream then expected"), "cbSize");
				}
				num += num5;
				num2 -= num5;
				using (MemoryStream memoryStream = new MemoryStream(Compressor.DecompressPropertyData(array)))
				{
					data = ExtendedPropertySerializer.DecodeAttribute(guid, memoryStream);
					return num;
				}
			}
			byte[] buffer = new byte[dataSizeIfKnownGuid];
			uint num7 = (uint)stream.Read(buffer, 0, (int)dataSizeIfKnownGuid);
			if (dataSizeIfKnownGuid != num7)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Read different size from stream then expected"), "cbSize");
			}
			using (MemoryStream memoryStream2 = new MemoryStream(buffer))
			{
				data = ExtendedPropertySerializer.DecodeAttribute(guid, memoryStream2);
			}
			num2 -= dataSizeIfKnownGuid;
			num += dataSizeIfKnownGuid;
			return num;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00154E9C File Offset: 0x0015429C
		internal static object DecodeAttribute(Guid guid, Stream stream)
		{
			VarEnum varEnum;
			return ExtendedPropertySerializer.DecodeAttribute(guid, stream, out varEnum);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00154EB4 File Offset: 0x001542B4
		internal static object DecodeAttribute(Guid guid, Stream memStream, out VarEnum type)
		{
			using (BinaryReader binaryReader = new BinaryReader(memStream))
			{
				bool flag = ExtendedPropertySerializer.UsesEmbeddedTypeInformation(guid);
				if (flag)
				{
					type = (VarEnum)binaryReader.ReadUInt16();
				}
				else
				{
					type = (VarEnum)8209;
				}
				VarEnum varEnum = type;
				switch (varEnum)
				{
				case VarEnum.VT_I2:
					return binaryReader.ReadInt16();
				case VarEnum.VT_I4:
					return binaryReader.ReadInt32();
				case VarEnum.VT_R4:
					return binaryReader.ReadSingle();
				case VarEnum.VT_R8:
					return binaryReader.ReadDouble();
				case VarEnum.VT_CY:
				case VarEnum.VT_DISPATCH:
				case VarEnum.VT_ERROR:
				case VarEnum.VT_VARIANT:
				case VarEnum.VT_UNKNOWN:
				case (VarEnum)15:
					break;
				case VarEnum.VT_DATE:
					return DateTime.FromOADate(binaryReader.ReadDouble());
				case VarEnum.VT_BSTR:
				{
					byte[] bytes = binaryReader.ReadBytes((int)memStream.Length);
					return Encoding.Unicode.GetString(bytes);
				}
				case VarEnum.VT_BOOL:
					return binaryReader.ReadBoolean();
				case VarEnum.VT_DECIMAL:
					return binaryReader.ReadDecimal();
				case VarEnum.VT_I1:
					return binaryReader.ReadChar();
				case VarEnum.VT_UI1:
					return binaryReader.ReadByte();
				case VarEnum.VT_UI2:
					return binaryReader.ReadUInt16();
				case VarEnum.VT_UI4:
					return binaryReader.ReadUInt32();
				case VarEnum.VT_I8:
					return binaryReader.ReadInt64();
				case VarEnum.VT_UI8:
					return binaryReader.ReadUInt64();
				default:
					switch (varEnum)
					{
					case (VarEnum)8194:
					{
						int num = (int)(memStream.Length - 2L) / 2;
						short[] array = new short[num];
						for (int i = 0; i < num; i++)
						{
							array[i] = binaryReader.ReadInt16();
						}
						return array;
					}
					case (VarEnum)8195:
					{
						int num2 = (int)(memStream.Length - 2L) / 4;
						int[] array2 = new int[num2];
						for (int j = 0; j < num2; j++)
						{
							array2[j] = binaryReader.ReadInt32();
						}
						return array2;
					}
					case (VarEnum)8196:
					{
						int num3 = (int)(memStream.Length - 2L) / 4;
						float[] array3 = new float[num3];
						for (int k = 0; k < num3; k++)
						{
							array3[k] = binaryReader.ReadSingle();
						}
						return array3;
					}
					case (VarEnum)8197:
					{
						int num4 = (int)(memStream.Length - 2L) / 8;
						double[] array4 = new double[num4];
						for (int l = 0; l < num4; l++)
						{
							array4[l] = binaryReader.ReadDouble();
						}
						return array4;
					}
					case (VarEnum)8199:
					{
						int num5 = (int)(memStream.Length - 2L) / 8;
						DateTime[] array5 = new DateTime[num5];
						for (int m = 0; m < num5; m++)
						{
							array5[m] = DateTime.FromOADate(binaryReader.ReadDouble());
						}
						return array5;
					}
					case (VarEnum)8203:
					{
						int num6 = (int)(memStream.Length - 2L);
						bool[] array6 = new bool[num6];
						for (int n = 0; n < num6; n++)
						{
							array6[n] = binaryReader.ReadBoolean();
						}
						return array6;
					}
					case (VarEnum)8206:
					{
						int num7 = (int)((memStream.Length - 2L) / (long)((ulong)Native.SizeOfDecimal));
						decimal[] array7 = new decimal[num7];
						for (int num8 = 0; num8 < num7; num8++)
						{
							array7[num8] = binaryReader.ReadDecimal();
						}
						return array7;
					}
					case (VarEnum)8208:
						return binaryReader.ReadChars((int)(memStream.Length - 2L));
					case (VarEnum)8209:
					{
						int num9 = 2;
						if (!flag)
						{
							num9 = 0;
						}
						return binaryReader.ReadBytes((int)(memStream.Length - (long)num9));
					}
					case (VarEnum)8210:
					{
						int num10 = (int)(memStream.Length - 2L) / 2;
						ushort[] array8 = new ushort[num10];
						for (int num11 = 0; num11 < num10; num11++)
						{
							array8[num11] = binaryReader.ReadUInt16();
						}
						return array8;
					}
					case (VarEnum)8211:
					{
						int num12 = (int)(memStream.Length - 2L) / 4;
						uint[] array9 = new uint[num12];
						for (int num13 = 0; num13 < num12; num13++)
						{
							array9[num13] = binaryReader.ReadUInt32();
						}
						return array9;
					}
					case (VarEnum)8212:
					{
						int num14 = (int)(memStream.Length - 2L) / 8;
						long[] array10 = new long[num14];
						for (int num15 = 0; num15 < num14; num15++)
						{
							array10[num15] = binaryReader.ReadInt64();
						}
						return array10;
					}
					case (VarEnum)8213:
					{
						int num16 = (int)(memStream.Length - 2L) / 8;
						ulong[] array11 = new ulong[num16];
						for (int num17 = 0; num17 < num16; num17++)
						{
							array11[num17] = binaryReader.ReadUInt64();
						}
						return array11;
					}
					}
					break;
				}
				throw new InvalidOperationException(SR.Get("InvalidEpInIsf"));
			}
			object result;
			return result;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00155398 File Offset: 0x00154798
		internal static uint EncodeAsISF(ExtendedPropertyCollection attributes, Stream stream, GuidList guidList, byte compressionAlgorithm, bool fTag)
		{
			uint num = 0U;
			for (int i = 0; i < attributes.Count; i++)
			{
				ExtendedProperty extendedProperty = attributes[i];
				using (MemoryStream memoryStream = new MemoryStream(10))
				{
					ExtendedPropertySerializer.EncodeToStream(extendedProperty, memoryStream);
					byte[] data = memoryStream.ToArray();
					num += ExtendedPropertySerializer.EncodeAsISF(extendedProperty.Id, data, stream, guidList, compressionAlgorithm, fTag);
				}
			}
			return num;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00155418 File Offset: 0x00154818
		internal static Guid[] GetUnknownGuids(ExtendedPropertyCollection attributes, out int count)
		{
			Guid[] array = new Guid[attributes.Count];
			count = 0;
			for (int i = 0; i < attributes.Count; i++)
			{
				ExtendedProperty extendedProperty = attributes[i];
				if (GuidList.FindKnownTag(extendedProperty.Id) == KnownTagCache.KnownTagIndex.Unknown)
				{
					Guid[] array2 = array;
					int num = count;
					count = num + 1;
					array2[num] = extendedProperty.Id;
				}
			}
			return array;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00155470 File Offset: 0x00154870
		internal static void Validate(Guid id, object value)
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentException(SR.Get("InvalidGuid"));
			}
			if (id == KnownIds.Color)
			{
				if (!(value is Color))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(Color)
					}), "value");
				}
			}
			else if (id == KnownIds.CurveFittingError)
			{
				if (!(value.GetType() == typeof(int)))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(int)
					}), "value");
				}
			}
			else if (id == KnownIds.DrawingFlags)
			{
				if (value.GetType() != typeof(DrawingFlags))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(DrawingFlags)
					}), "value");
				}
			}
			else if (id == KnownIds.StylusTip)
			{
				Type type = value.GetType();
				bool flag = type == typeof(StylusTip);
				bool flag2 = type == typeof(int);
				if (!flag && !flag2)
				{
					throw new ArgumentException(SR.Get("InvalidValueType1", new object[]
					{
						typeof(StylusTip),
						typeof(int)
					}), "value");
				}
				if (!StylusTipHelper.IsDefined((StylusTip)value))
				{
					throw new ArgumentException(SR.Get("InvalidValueOfType", new object[]
					{
						value,
						typeof(StylusTip)
					}), "value");
				}
			}
			else if (id == KnownIds.StylusTipTransform)
			{
				Type type2 = value.GetType();
				if (type2 != typeof(string) && type2 != typeof(Matrix))
				{
					throw new ArgumentException(SR.Get("InvalidValueType1", new object[]
					{
						typeof(string),
						typeof(Matrix)
					}), "value");
				}
				if (type2 == typeof(Matrix))
				{
					Matrix matrix = (Matrix)value;
					if (!matrix.HasInverse)
					{
						throw new ArgumentException(SR.Get("MatrixNotInvertible"), "value");
					}
					if (MatrixHelper.ContainsNaN(matrix))
					{
						throw new ArgumentException(SR.Get("InvalidMatrixContainsNaN"), "value");
					}
					if (MatrixHelper.ContainsInfinity(matrix))
					{
						throw new ArgumentException(SR.Get("InvalidMatrixContainsInfinity"), "value");
					}
				}
			}
			else if (id == KnownIds.IsHighlighter)
			{
				if (value.GetType() != typeof(bool))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(bool)
					}), "value");
				}
			}
			else if (id == KnownIds.StylusHeight || id == KnownIds.StylusWidth)
			{
				if (value.GetType() != typeof(double))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(double)
					}), "value");
				}
				double num = (double)value;
				if (id == KnownIds.StylusHeight)
				{
					if (double.IsNaN(num) || num < DrawingAttributes.MinHeight || num > DrawingAttributes.MaxHeight)
					{
						throw new ArgumentOutOfRangeException("value", SR.Get("InvalidDrawingAttributesHeight"));
					}
				}
				else if (double.IsNaN(num) || num < DrawingAttributes.MinWidth || num > DrawingAttributes.MaxWidth)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("InvalidDrawingAttributesWidth"));
				}
			}
			else
			{
				if (!(id == KnownIds.Transparency))
				{
					if (!ExtendedPropertySerializer.UsesEmbeddedTypeInformation(id))
					{
						if (value.GetType() != typeof(byte[]))
						{
							throw new ArgumentException(SR.Get("InvalidValueType", new object[]
							{
								typeof(byte[])
							}), "value");
						}
					}
					else
					{
						VarEnum varEnum = SerializationHelper.ConvertToVarEnum(value.GetType(), true);
						if (varEnum <= VarEnum.VT_I1)
						{
							if (varEnum != VarEnum.VT_DATE && varEnum != VarEnum.VT_I1)
							{
								return;
							}
						}
						else if (varEnum != (VarEnum)8199 && varEnum != (VarEnum)8208)
						{
							return;
						}
						using (MemoryStream memoryStream = new MemoryStream(32))
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
							{
								try
								{
									if (varEnum <= VarEnum.VT_I1)
									{
										if (varEnum != VarEnum.VT_DATE)
										{
											if (varEnum == VarEnum.VT_I1)
											{
												binaryWriter.Write((char)value);
											}
										}
										else
										{
											binaryWriter.Write(((DateTime)value).ToOADate());
										}
									}
									else if (varEnum != (VarEnum)8199)
									{
										if (varEnum == (VarEnum)8208)
										{
											binaryWriter.Write((char[])value);
										}
									}
									else
									{
										DateTime[] array = (DateTime[])value;
										for (int i = 0; i < array.Length; i++)
										{
											binaryWriter.Write(array[i].ToOADate());
										}
									}
								}
								catch (ArgumentException innerException)
								{
									throw new ArgumentException(SR.Get("InvalidDataInISF"), innerException);
								}
								catch (OverflowException innerException2)
								{
									throw new ArgumentException(SR.Get("InvalidDataInISF"), innerException2);
								}
							}
						}
					}
					return;
				}
				if (value.GetType() != typeof(byte))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(byte)
					}), "value");
				}
				double num2 = (double)value;
				return;
			}
		}
	}
}
