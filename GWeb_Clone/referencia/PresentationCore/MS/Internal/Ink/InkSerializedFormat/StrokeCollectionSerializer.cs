using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007CA RID: 1994
	internal class StrokeCollectionSerializer
	{
		// Token: 0x06005427 RID: 21543 RVA: 0x001567B8 File Offset: 0x00155BB8
		static StrokeCollectionSerializer()
		{
			TransformDescriptor transformDescriptor = new TransformDescriptor();
			transformDescriptor.Transform[0] = 1.0;
			transformDescriptor.Tag = KnownTagCache.KnownTagIndex.TransformIsotropicScale;
			transformDescriptor.Size = 1U;
			StrokeCollectionSerializer.IdentityTransformDescriptor = transformDescriptor;
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x00156824 File Offset: 0x00155C24
		private StrokeCollectionSerializer()
		{
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00156854 File Offset: 0x00155C54
		internal StrokeCollectionSerializer(StrokeCollection coreStrokes)
		{
			this._coreStrokes = coreStrokes;
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0015688C File Offset: 0x00155C8C
		internal void DecodeISF(Stream inkData)
		{
			try
			{
				bool flag;
				bool flag2;
				uint num;
				this.ExamineStreamHeader(inkData, out flag, out flag2, out num);
				if (flag)
				{
					int num2 = StrokeCollectionSerializer.Base64HeaderBytes.Length;
					inkData.Position = (long)num2;
					List<char> list = new List<char>((int)inkData.Length);
					for (int num3 = inkData.ReadByte(); num3 != -1; num3 = inkData.ReadByte())
					{
						byte item = (byte)num3;
						list.Add((char)item);
					}
					if ((byte)list[list.Count - 1] == 0)
					{
						list.RemoveAt(list.Count - 1);
					}
					char[] array = list.ToArray();
					byte[] buffer = Convert.FromBase64CharArray(array, 0, array.Length);
					MemoryStream memoryStream = new MemoryStream(buffer);
					if (this.IsGIFData(memoryStream))
					{
						this.DecodeRawISF(SystemDrawingHelper.GetCommentFromGifStream(memoryStream));
					}
					else
					{
						this.DecodeRawISF(memoryStream);
					}
				}
				else if (flag2)
				{
					this.DecodeRawISF(SystemDrawingHelper.GetCommentFromGifStream(inkData));
				}
				else
				{
					this.DecodeRawISF(inkData);
				}
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
			catch (IndexOutOfRangeException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
			catch (NullReferenceException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
			catch (EndOfStreamException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
			catch (OverflowException)
			{
				throw new ArgumentException(SR.Get("IsfOperationFailed"), "stream");
			}
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00156A8C File Offset: 0x00155E8C
		internal uint LoadStrokeIds(Stream isfStream, uint cbSize)
		{
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num2;
			uint num = SerializationHelper.Decode(isfStream, out num2);
			if (num > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "isfStream");
			}
			uint num3 = cbSize - num;
			if (num2 == 0U)
			{
				return cbSize - num3;
			}
			num = num3;
			byte[] buffer = new byte[num];
			uint num4 = StrokeCollectionSerializer.ReliableRead(isfStream, buffer, num);
			if (num != num4)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Read different size from stream then expected"), "isfStream");
			}
			num3 -= num;
			if (num3 != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "isfStream");
			}
			return cbSize;
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x00156B18 File Offset: 0x00155F18
		private bool IsGIFData(Stream inkdata)
		{
			long position = inkdata.Position;
			bool result;
			try
			{
				result = ((byte)inkdata.ReadByte() == 71 && (byte)inkdata.ReadByte() == 73 && (byte)inkdata.ReadByte() == 70);
			}
			finally
			{
				inkdata.Position = position;
			}
			return result;
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00156B78 File Offset: 0x00155F78
		private void ExamineStreamHeader(Stream inkdata, out bool fBase64, out bool fGif, out uint cbData)
		{
			fGif = false;
			cbData = 0U;
			fBase64 = false;
			if (inkdata.Length >= 7L)
			{
				fBase64 = this.IsBase64Data(inkdata);
			}
			if (!fBase64 && inkdata.Length >= 3L)
			{
				fGif = this.IsGIFData(inkdata);
			}
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x00156BBC File Offset: 0x00155FBC
		private void DecodeRawISF(Stream inputStream)
		{
			uint num = 0U;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			uint num2 = 0U;
			uint num3 = uint.MaxValue;
			uint num4 = 0U;
			uint num5 = uint.MaxValue;
			uint num6 = 0U;
			uint num7 = uint.MaxValue;
			uint num8 = 0U;
			uint num9 = uint.MaxValue;
			GuidList guidList = new GuidList();
			int num10 = 0;
			StylusPointDescription stylusPointDescription = null;
			Matrix transform = Matrix.Identity;
			this._strokeDescriptorTable = new List<StrokeDescriptor>();
			this._drawingAttributesTable = new List<DrawingAttributes>();
			this._transformTable = new List<TransformDescriptor>();
			this._metricTable = new List<MetricBlock>();
			if (this._coreStrokes.Count != 0 || this._coreStrokes.ExtendedProperties.Count != 0)
			{
				throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("ISF decoder cannot operate on non-empty ink container"));
			}
			uint num12;
			uint num11 = SerializationHelper.Decode(inputStream, out num12);
			if (num12 != 0U)
			{
				throw new ArgumentException(SR.Get("InvalidStream"));
			}
			uint num13;
			num11 = SerializationHelper.Decode(inputStream, out num13);
			if (num13 == 0U)
			{
				return;
			}
			while (0U < num13)
			{
				num = 0U;
				num11 = SerializationHelper.Decode(inputStream, out num12);
				KnownTagCache.KnownTagIndex knownTagIndex = (KnownTagCache.KnownTagIndex)num12;
				if (num13 >= num11)
				{
					num13 -= num11;
					switch (knownTagIndex)
					{
					case KnownTagCache.KnownTagIndex.Unknown:
						num = this.DecodeInkSpaceRectangle(inputStream, num13);
						break;
					case KnownTagCache.KnownTagIndex.GuidTable:
					case KnownTagCache.KnownTagIndex.DrawingAttributesTable:
					case KnownTagCache.KnownTagIndex.DrawingAttributesBlock:
					case KnownTagCache.KnownTagIndex.StrokeDescriptorTable:
					case KnownTagCache.KnownTagIndex.StrokeDescriptorBlock:
					case KnownTagCache.KnownTagIndex.Stroke:
					case KnownTagCache.KnownTagIndex.CompressionHeader:
					case KnownTagCache.KnownTagIndex.TransformTable:
					case KnownTagCache.KnownTagIndex.MetricTable:
					case KnownTagCache.KnownTagIndex.MetricBlock:
					case KnownTagCache.KnownTagIndex.PersistenceFormat:
					case KnownTagCache.KnownTagIndex.HimetricSize:
					case KnownTagCache.KnownTagIndex.StrokeIds:
					case KnownTagCache.KnownTagIndex.ExtendedTransformTable:
						num11 = SerializationHelper.Decode(inputStream, out num);
						if (num13 < num11 + num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "inputStream");
						}
						num13 -= num11;
						switch (knownTagIndex)
						{
						case KnownTagCache.KnownTagIndex.GuidTable:
							num11 = guidList.Load(inputStream, num);
							break;
						case KnownTagCache.KnownTagIndex.DrawingAttributesTable:
							num11 = this.LoadDrawAttrsTable(inputStream, guidList, num);
							flag2 = true;
							break;
						case KnownTagCache.KnownTagIndex.DrawingAttributesBlock:
						{
							ExtendedPropertyCollection extendedPropertyCollection = new ExtendedPropertyCollection();
							extendedPropertyCollection.Add(KnownIds.DrawingFlags, DrawingFlags.Polyline);
							DrawingAttributes drawingAttributes = new DrawingAttributes(extendedPropertyCollection);
							num11 = DrawingAttributeSerializer.DecodeAsISF(inputStream, guidList, num, drawingAttributes);
							this._drawingAttributesTable.Add(drawingAttributes);
							flag2 = true;
							break;
						}
						case KnownTagCache.KnownTagIndex.StrokeDescriptorTable:
							num11 = this.DecodeStrokeDescriptorTable(inputStream, num);
							flag = true;
							break;
						case KnownTagCache.KnownTagIndex.StrokeDescriptorBlock:
							num11 = this.DecodeStrokeDescriptorBlock(inputStream, num);
							flag = true;
							break;
						case KnownTagCache.KnownTagIndex.Buttons:
						case KnownTagCache.KnownTagIndex.NoX:
						case KnownTagCache.KnownTagIndex.NoY:
						case KnownTagCache.KnownTagIndex.DrawingAttributesTableIndex:
						case KnownTagCache.KnownTagIndex.StrokePropertyList:
						case KnownTagCache.KnownTagIndex.PointProperty:
						case KnownTagCache.KnownTagIndex.StrokeDescriptorTableIndex:
							goto IL_515;
						case KnownTagCache.KnownTagIndex.Stroke:
						{
							StrokeDescriptor strokeDescriptor = null;
							if (flag)
							{
								if (num3 != num2 && (long)this._strokeDescriptorTable.Count <= (long)((ulong)num2))
								{
									throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
								}
								strokeDescriptor = this._strokeDescriptorTable[(int)num2];
							}
							if (num9 != num8)
							{
								if (flag4)
								{
									if ((long)this._transformTable.Count <= (long)((ulong)num8))
									{
										throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
									}
									transform = this.LoadTransform(this._transformTable[(int)num8]);
								}
								num9 = num8;
								transform.Scale(StrokeCollectionSerializer.HimetricToAvalonMultiplier, StrokeCollectionSerializer.HimetricToAvalonMultiplier);
							}
							MetricBlock block = null;
							if (flag3)
							{
								if (num7 != num6 && (long)this._metricTable.Count <= (long)((ulong)num6))
								{
									throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
								}
								block = this._metricTable[(int)num6];
							}
							DrawingAttributes drawingAttributes2 = null;
							if (flag2)
							{
								if (num5 != num4)
								{
									if ((long)this._drawingAttributesTable.Count <= (long)((ulong)num4))
									{
										throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
									}
									num5 = num4;
								}
								DrawingAttributes drawingAttributes3 = this._drawingAttributesTable[(int)num4];
								drawingAttributes2 = drawingAttributes3.Clone();
							}
							if (drawingAttributes2 == null)
							{
								drawingAttributes2 = new DrawingAttributes();
							}
							if (num7 != num6 || num3 != num2)
							{
								stylusPointDescription = this.BuildStylusPointDescription(strokeDescriptor, block, guidList);
								num3 = num2;
								num7 = num6;
							}
							Stroke stroke;
							num11 = StrokeSerializer.DecodeStroke(inputStream, num, guidList, strokeDescriptor, stylusPointDescription, drawingAttributes2, transform, out stroke);
							if (stroke != null)
							{
								this._coreStrokes.AddWithoutEvent(stroke);
								num10++;
							}
							break;
						}
						case KnownTagCache.KnownTagIndex.CompressionHeader:
							inputStream.Seek((long)((ulong)num), SeekOrigin.Current);
							num11 = num;
							break;
						case KnownTagCache.KnownTagIndex.TransformTable:
							num11 = this.DecodeTransformTable(inputStream, num, false);
							flag4 = true;
							break;
						default:
							switch (knownTagIndex)
							{
							case KnownTagCache.KnownTagIndex.MetricTable:
								num11 = this.DecodeMetricTable(inputStream, num);
								flag3 = true;
								break;
							case KnownTagCache.KnownTagIndex.MetricBlock:
							{
								MetricBlock item;
								num11 = this.DecodeMetricBlock(inputStream, num, out item);
								this._metricTable.Clear();
								this._metricTable.Add(item);
								flag3 = true;
								break;
							}
							case KnownTagCache.KnownTagIndex.MetricTableIndex:
							case KnownTagCache.KnownTagIndex.Mantissa:
								goto IL_515;
							case KnownTagCache.KnownTagIndex.PersistenceFormat:
							{
								uint num14;
								num11 = SerializationHelper.Decode(inputStream, out num14);
								if (num14 == 0U)
								{
									this.CurrentPersistenceFormat = PersistenceFormat.InkSerializedFormat;
								}
								else if (1U == num14)
								{
									this.CurrentPersistenceFormat = PersistenceFormat.Gif;
								}
								break;
							}
							case KnownTagCache.KnownTagIndex.HimetricSize:
							{
								int num15;
								num11 = SerializationHelper.SignDecode(inputStream, out num15);
								if (num11 > num13)
								{
									throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
								}
								this._himetricSize.X = (double)num15;
								num11 += SerializationHelper.SignDecode(inputStream, out num15);
								this._himetricSize.Y = (double)num15;
								break;
							}
							case KnownTagCache.KnownTagIndex.StrokeIds:
								num11 = this.LoadStrokeIds(inputStream, num);
								break;
							case KnownTagCache.KnownTagIndex.ExtendedTransformTable:
								if (!flag4)
								{
									throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
								}
								num11 = this.DecodeTransformTable(inputStream, num, true);
								break;
							default:
								goto IL_515;
							}
							break;
						}
						if (num11 != num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						break;
						IL_515:
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF tag logic"));
					case KnownTagCache.KnownTagIndex.Buttons:
					case KnownTagCache.KnownTagIndex.NoX:
					case KnownTagCache.KnownTagIndex.NoY:
					case KnownTagCache.KnownTagIndex.StrokePropertyList:
					case KnownTagCache.KnownTagIndex.PointProperty:
					case KnownTagCache.KnownTagIndex.TransformQuad:
					case KnownTagCache.KnownTagIndex.Mantissa:
						goto IL_5AF;
					case KnownTagCache.KnownTagIndex.DrawingAttributesTableIndex:
						num = SerializationHelper.Decode(inputStream, out num4);
						break;
					case KnownTagCache.KnownTagIndex.StrokeDescriptorTableIndex:
						num = SerializationHelper.Decode(inputStream, out num2);
						break;
					case KnownTagCache.KnownTagIndex.Transform:
					case KnownTagCache.KnownTagIndex.TransformIsotropicScale:
					case KnownTagCache.KnownTagIndex.TransformAnisotropicScale:
					case KnownTagCache.KnownTagIndex.TransformRotate:
					case KnownTagCache.KnownTagIndex.TransformTranslate:
					case KnownTagCache.KnownTagIndex.TransformScaleAndTranslate:
					{
						TransformDescriptor item2;
						num = this.DecodeTransformBlock(inputStream, knownTagIndex, num13, false, out item2);
						flag4 = true;
						this._transformTable.Clear();
						this._transformTable.Add(item2);
						break;
					}
					case KnownTagCache.KnownTagIndex.TransformTableIndex:
						num = SerializationHelper.Decode(inputStream, out num8);
						break;
					case KnownTagCache.KnownTagIndex.MetricTableIndex:
						num = SerializationHelper.Decode(inputStream, out num6);
						break;
					default:
						goto IL_5AF;
					}
					IL_66F:
					if (num > num13)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
					}
					num13 -= num;
					continue;
					IL_5AF:
					if (knownTagIndex >= (KnownTagCache.KnownTagIndex)KnownIdCache.CustomGuidBaseIndex || (knownTagIndex >= (KnownTagCache.KnownTagIndex)KnownTagCache.KnownTagCount && (ulong)knownTagIndex < (ulong)KnownTagCache.KnownTagCount + (ulong)((long)KnownIdCache.OriginalISFIdTable.Length)))
					{
						num = num13;
						Guid guid = guidList.FindGuid(knownTagIndex);
						if (guid == Guid.Empty)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Global Custom Attribute tag embedded in ISF stream does not match guid table"), "inkdata");
						}
						object value;
						num11 = ExtendedPropertySerializer.DecodeAsISF(inputStream, num, guidList, knownTagIndex, ref guid, out value);
						if (num11 > num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "inkdata");
						}
						this._coreStrokes.ExtendedProperties[guid] = value;
					}
					else
					{
						num11 = SerializationHelper.Decode(inputStream, out num);
						if (num13 < num11 + num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
						}
						inputStream.Seek((long)((ulong)(num + num11)), SeekOrigin.Current);
					}
					num = num11;
					goto IL_66F;
				}
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"));
			}
			if (num13 != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "inkdata");
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x00157270 File Offset: 0x00156670
		private uint LoadDrawAttrsTable(Stream strm, GuidList guidList, uint cbSize)
		{
			this._drawingAttributesTable.Clear();
			uint num = cbSize;
			uint num2 = 0U;
			while (num > 0U)
			{
				uint num3 = SerializationHelper.Decode(strm, out num2);
				if (cbSize < num3)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num3;
				if (num < num2)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				DrawingAttributes drawingAttributes = new DrawingAttributes();
				drawingAttributes.DrawingFlags = DrawingFlags.Polyline;
				num3 = DrawingAttributeSerializer.DecodeAsISF(strm, guidList, num2, drawingAttributes);
				if (cbSize < num2)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				this._drawingAttributesTable.Add(drawingAttributes);
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00157330 File Offset: 0x00156730
		private uint DecodeStrokeDescriptor(Stream strm, uint cbSize, out StrokeDescriptor descr)
		{
			descr = new StrokeDescriptor();
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num = cbSize;
			while (num > 0U)
			{
				uint num3;
				uint num2 = SerializationHelper.Decode(strm, out num3);
				KnownTagCache.KnownTagIndex knownTagIndex = (KnownTagCache.KnownTagIndex)num3;
				if (num2 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				descr.Template.Add(knownTagIndex);
				if (KnownTagCache.KnownTagIndex.Buttons == knownTagIndex && num > 0U)
				{
					uint num4;
					num2 = SerializationHelper.Decode(strm, out num4);
					if (num2 > num)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
					}
					num -= num2;
					descr.Template.Add((KnownTagCache.KnownTagIndex)num4);
					while (num > 0U)
					{
						if (num4 <= 0U)
						{
							break;
						}
						uint item;
						num2 = SerializationHelper.Decode(strm, out item);
						if (num2 > num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
						}
						num -= num2;
						num4 -= 1U;
						descr.Template.Add((KnownTagCache.KnownTagIndex)item);
					}
				}
				else if (KnownTagCache.KnownTagIndex.StrokePropertyList == knownTagIndex && num > 0U)
				{
					while (num > 0U)
					{
						uint item2;
						num2 = SerializationHelper.Decode(strm, out item2);
						if (num2 > num)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
						}
						num -= num2;
						descr.Template.Add((KnownTagCache.KnownTagIndex)item2);
					}
				}
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00157470 File Offset: 0x00156870
		private uint DecodeStrokeDescriptorBlock(Stream strm, uint cbSize)
		{
			this._strokeDescriptorTable.Clear();
			if (cbSize == 0U)
			{
				return 0U;
			}
			StrokeDescriptor item;
			uint num = this.DecodeStrokeDescriptor(strm, cbSize, out item);
			if (num != cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			this._strokeDescriptorTable.Add(item);
			return num;
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x001574C0 File Offset: 0x001568C0
		private uint DecodeStrokeDescriptorTable(Stream strm, uint cbSize)
		{
			this._strokeDescriptorTable.Clear();
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num = cbSize;
			while (num > 0U)
			{
				uint num3;
				uint num2 = SerializationHelper.Decode(strm, out num3);
				if (num2 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				if (num3 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				StrokeDescriptor item;
				num2 = this.DecodeStrokeDescriptor(strm, num3, out item);
				if (num2 != num3)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				this._strokeDescriptorTable.Add(item);
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00157574 File Offset: 0x00156974
		private uint DecodeMetricTable(Stream strm, uint cbSize)
		{
			this._metricTable.Clear();
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num = cbSize;
			while (num > 0U)
			{
				uint num3;
				uint num2 = SerializationHelper.Decode(strm, out num3);
				if (num2 + num3 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				MetricBlock item;
				num2 = this.DecodeMetricBlock(strm, num3, out item);
				if (num2 != num3)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				this._metricTable.Add(item);
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00157610 File Offset: 0x00156A10
		private uint DecodeMetricBlock(Stream strm, uint cbSize, out MetricBlock block)
		{
			block = new MetricBlock();
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num = cbSize;
			while (num > 0U)
			{
				uint tag;
				uint num2 = SerializationHelper.Decode(strm, out tag);
				if (num2 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				uint num3;
				num2 = SerializationHelper.Decode(strm, out num3);
				if (num2 + num3 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num2;
				MetricEntry metricEntry = new MetricEntry();
				metricEntry.Tag = (KnownTagCache.KnownTagIndex)tag;
				byte[] array = new byte[num3];
				uint num4 = StrokeCollectionSerializer.ReliableRead(strm, array, num3);
				num -= num4;
				if (num4 != num3)
				{
					break;
				}
				metricEntry.Data = array;
				block.AddMetricEntry(metricEntry);
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x001576DC File Offset: 0x00156ADC
		private uint DecodeTransformTable(Stream strm, uint cbSize, bool useDoubles)
		{
			if (!useDoubles)
			{
				this._transformTable.Clear();
			}
			if (cbSize == 0U)
			{
				return 0U;
			}
			uint num = cbSize;
			int num2 = 0;
			while (num > 0U)
			{
				uint num4;
				uint num3 = SerializationHelper.Decode(strm, out num4);
				KnownTagCache.KnownTagIndex tag = (KnownTagCache.KnownTagIndex)num4;
				if (num3 > num)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				num -= num3;
				TransformDescriptor transformDescriptor;
				num3 = this.DecodeTransformBlock(strm, tag, num, useDoubles, out transformDescriptor);
				num -= num3;
				if (useDoubles)
				{
					this._transformTable[num2] = transformDescriptor;
				}
				else
				{
					this._transformTable.Add(transformDescriptor);
				}
				num2++;
			}
			if (num != 0U)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return cbSize;
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00157780 File Offset: 0x00156B80
		internal static uint ReliableRead(Stream stream, byte[] buffer, uint requestedCount)
		{
			if (stream == null || buffer == null || (ulong)requestedCount > (ulong)((long)buffer.Length))
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid argument passed to ReliableRead"));
			}
			uint num;
			int num2;
			for (num = 0U; num < requestedCount; num += (uint)num2)
			{
				num2 = stream.Read(buffer, (int)num, (int)(requestedCount - num));
				if (num2 == 0)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x001577C8 File Offset: 0x00156BC8
		private uint DecodeTransformBlock(Stream strm, KnownTagCache.KnownTagIndex tag, uint cbSize, bool useDoubles, out TransformDescriptor xform)
		{
			xform = new TransformDescriptor();
			xform.Tag = tag;
			if (cbSize == 0U)
			{
				return 0U;
			}
			BinaryReader binaryReader = new BinaryReader(strm);
			uint num;
			if (KnownTagCache.KnownTagIndex.TransformRotate == tag)
			{
				uint num2;
				num = SerializationHelper.Decode(strm, out num2);
				if (num > cbSize)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				xform.Transform[0] = num2;
				xform.Size = 1U;
			}
			else
			{
				if (tag == KnownTagCache.KnownTagIndex.TransformIsotropicScale)
				{
					xform.Size = 1U;
				}
				else if (tag == KnownTagCache.KnownTagIndex.TransformAnisotropicScale || tag == KnownTagCache.KnownTagIndex.TransformTranslate)
				{
					xform.Size = 2U;
				}
				else if (tag == KnownTagCache.KnownTagIndex.TransformScaleAndTranslate)
				{
					xform.Size = 4U;
				}
				else
				{
					xform.Size = 6U;
				}
				if (useDoubles)
				{
					num = xform.Size * Native.SizeOfDouble;
				}
				else
				{
					num = xform.Size * Native.SizeOfFloat;
				}
				if (num > cbSize)
				{
					throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
				}
				int num3 = 0;
				while ((long)num3 < (long)((ulong)xform.Size))
				{
					if (useDoubles)
					{
						xform.Transform[num3] = binaryReader.ReadDouble();
					}
					else
					{
						xform.Transform[num3] = (double)binaryReader.ReadSingle();
					}
					num3++;
				}
			}
			return num;
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x001578F4 File Offset: 0x00156CF4
		private uint DecodeInkSpaceRectangle(Stream strm, uint cbSize)
		{
			uint num = 0U;
			int num3;
			uint num2 = SerializationHelper.SignDecode(strm, out num3);
			if (num2 > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			uint num4 = cbSize - num2;
			num += num2;
			this._inkSpaceRectangle.X = (double)num3;
			if (num > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num2 = SerializationHelper.SignDecode(strm, out num3);
			if (num2 > num4)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num4 -= num2;
			num += num2;
			this._inkSpaceRectangle.Y = (double)num3;
			if (num > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num2 = SerializationHelper.SignDecode(strm, out num3);
			if (num2 > num4)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num4 -= num2;
			num += num2;
			this._inkSpaceRectangle.Width = (double)num3 - this._inkSpaceRectangle.Left;
			if (num > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num2 = SerializationHelper.SignDecode(strm, out num3);
			if (num2 > num4)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			num4 -= num2;
			num += num2;
			this._inkSpaceRectangle.Height = (double)num3 - this._inkSpaceRectangle.Top;
			if (num > cbSize)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF data"), "strm");
			}
			return num;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00157A60 File Offset: 0x00156E60
		private Matrix LoadTransform(TransformDescriptor tdrd)
		{
			double m = 0.0;
			double num = 0.0;
			double m2 = 0.0;
			double num2 = 0.0;
			double offsetX = 0.0;
			double offsetY = 0.0;
			if (KnownTagCache.KnownTagIndex.TransformIsotropicScale == tdrd.Tag)
			{
				num2 = (m = tdrd.Transform[0]);
			}
			else if (KnownTagCache.KnownTagIndex.TransformRotate == tdrd.Tag)
			{
				double num3 = tdrd.Transform[0] / 100.0 * 0.017453292519943295;
				num2 = (m = Math.Cos(num3));
				num = Math.Sin(num3);
				if (num == 0.0 && num2 == 1.0)
				{
					m2 = 0.0;
				}
				else
				{
					m2 = -num2;
				}
			}
			else if (KnownTagCache.KnownTagIndex.TransformAnisotropicScale == tdrd.Tag)
			{
				m = tdrd.Transform[0];
				num2 = tdrd.Transform[1];
			}
			else if (KnownTagCache.KnownTagIndex.TransformTranslate == tdrd.Tag)
			{
				offsetX = tdrd.Transform[0];
				offsetY = tdrd.Transform[1];
			}
			else if (KnownTagCache.KnownTagIndex.TransformScaleAndTranslate == tdrd.Tag)
			{
				m = tdrd.Transform[0];
				num2 = tdrd.Transform[1];
				offsetX = tdrd.Transform[2];
				offsetY = tdrd.Transform[3];
			}
			else
			{
				m = tdrd.Transform[0];
				num = tdrd.Transform[1];
				m2 = tdrd.Transform[2];
				num2 = tdrd.Transform[3];
				offsetX = tdrd.Transform[4];
				offsetY = tdrd.Transform[5];
			}
			return new Matrix(m, num, m2, num2, offsetX, offsetY);
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00157BE8 File Offset: 0x00156FE8
		private StylusPointPropertyInfo GetStylusPointPropertyInfo(Guid guid, KnownTagCache.KnownTagIndex tag, MetricBlock block)
		{
			bool flag = false;
			int minimum = 0;
			int maximum = 0;
			StylusPointPropertyUnit unit = StylusPointPropertyUnit.None;
			float resolution = 1f;
			for (int i = 0; i < 11; i++)
			{
				if (MetricEntry.MetricEntry_Optional[i].Tag == tag)
				{
					minimum = MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Minimum;
					maximum = MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Maximum;
					resolution = MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Resolution;
					unit = MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Unit;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				minimum = int.MinValue;
				maximum = int.MaxValue;
				unit = StylusPointPropertyUnit.None;
				resolution = 1f;
			}
			if (block != null)
			{
				for (MetricEntry metricEntry = block.GetMetricEntryList(); metricEntry != null; metricEntry = metricEntry.Next)
				{
					if (metricEntry.Tag == tag)
					{
						uint num = 0U;
						using (MemoryStream memoryStream = new MemoryStream(metricEntry.Data))
						{
							int num2;
							num += SerializationHelper.SignDecode(memoryStream, out num2);
							if (num >= metricEntry.Size)
							{
								break;
							}
							minimum = num2;
							num += SerializationHelper.SignDecode(memoryStream, out num2);
							if (num >= metricEntry.Size)
							{
								break;
							}
							maximum = num2;
							uint num3;
							num += SerializationHelper.Decode(memoryStream, out num3);
							unit = (StylusPointPropertyUnit)num3;
							if (num >= metricEntry.Size)
							{
								break;
							}
							using (BinaryReader binaryReader = new BinaryReader(memoryStream))
							{
								resolution = binaryReader.ReadSingle();
								num += Native.SizeOfFloat;
								break;
							}
						}
					}
				}
			}
			return new StylusPointPropertyInfo(new StylusPointProperty(guid, StylusPointPropertyIds.IsKnownButton(guid)), minimum, maximum, unit, resolution);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00157DB8 File Offset: 0x001571B8
		private StylusPointDescription BuildStylusPointDescription(StrokeDescriptor strd, MetricBlock block, GuidList guidList)
		{
			int i = 0;
			int num = 0;
			Guid[] array = null;
			List<KnownTagCache.KnownTagIndex> list = null;
			if (strd != null)
			{
				list = new List<KnownTagCache.KnownTagIndex>();
				while (i < strd.Template.Count)
				{
					KnownTagCache.KnownTagIndex knownTagIndex = strd.Template[i];
					if (KnownTagCache.KnownTagIndex.Buttons == knownTagIndex)
					{
						i++;
						uint num2 = (uint)strd.Template[i];
						i++;
						array = new Guid[num2];
						for (uint num3 = 0U; num3 < num2; num3 += 1U)
						{
							Guid guid = guidList.FindGuid(strd.Template[i]);
							if (guid == Guid.Empty)
							{
								throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Button guid tag embedded in ISF stream does not match guid table"), "strd");
							}
							array[(int)num3] = guid;
							i++;
						}
					}
					else
					{
						if (KnownTagCache.KnownTagIndex.StrokePropertyList == knownTagIndex)
						{
							break;
						}
						if (KnownTagCache.KnownTagIndex.NoX == knownTagIndex || KnownTagCache.KnownTagIndex.NoY == knownTagIndex)
						{
							throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Invalid ISF with NoX or NoY specified"), "strd");
						}
						list.Add(strd.Template[i]);
						num++;
						i++;
					}
				}
			}
			List<StylusPointPropertyInfo> list2 = new List<StylusPointPropertyInfo>();
			list2.Add(this.GetStylusPointPropertyInfo(KnownIds.X, KnownIdCache.KnownGuidBaseIndex, block));
			list2.Add(this.GetStylusPointPropertyInfo(KnownIds.Y, KnownIdCache.KnownGuidBaseIndex + 1U, block));
			list2.Add(this.GetStylusPointPropertyInfo(KnownIds.NormalPressure, KnownIdCache.KnownGuidBaseIndex + 6U, block));
			int num4 = -1;
			if (list != null)
			{
				for (int j = 0; j < list.Count; j++)
				{
					Guid guid2 = guidList.FindGuid(list[j]);
					if (guid2 == Guid.Empty)
					{
						throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Packet Description Property tag embedded in ISF stream does not match guid table"), "strd");
					}
					if (num4 == -1 && guid2 == StylusPointPropertyIds.NormalPressure)
					{
						num4 = j + 2;
					}
					else
					{
						list2.Add(this.GetStylusPointPropertyInfo(guid2, list[j], block));
					}
				}
				if (array != null)
				{
					for (int k = 0; k < array.Length; k++)
					{
						StylusPointProperty stylusPointProperty = new StylusPointProperty(array[k], true);
						StylusPointPropertyInfo item = new StylusPointPropertyInfo(stylusPointProperty);
						list2.Add(item);
					}
				}
			}
			return new StylusPointDescription(list2, num4);
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00157FD0 File Offset: 0x001573D0
		internal void EncodeISF(Stream outputStream)
		{
			this._strokeLookupTable = new Dictionary<Stroke, StrokeCollectionSerializer.StrokeLookupEntry>(this._coreStrokes.Count);
			for (int i = 0; i < this._coreStrokes.Count; i++)
			{
				this._strokeLookupTable.Add(this._coreStrokes[i], new StrokeCollectionSerializer.StrokeLookupEntry());
			}
			this._strokeDescriptorTable = new List<StrokeDescriptor>(this._coreStrokes.Count);
			this._drawingAttributesTable = new List<DrawingAttributes>();
			this._metricTable = new List<MetricBlock>();
			this._transformTable = new List<TransformDescriptor>();
			using (MemoryStream memoryStream = new MemoryStream(this._coreStrokes.Count * 125))
			{
				GuidList guidList = this.BuildGuidList();
				uint num = 0U;
				uint num2 = 0U;
				byte compressionData = (this.CurrentCompressionMode == CompressionMode.NoCompression) ? AlgoModule.NoCompression : AlgoModule.DefaultCompression;
				foreach (Stroke stroke in this._coreStrokes)
				{
					this._strokeLookupTable[stroke].CompressionData = compressionData;
					int[][] isfreadyStrokeData;
					bool storePressure;
					stroke.StylusPoints.ToISFReadyArrays(out isfreadyStrokeData, out storePressure);
					this._strokeLookupTable[stroke].ISFReadyStrokeData = isfreadyStrokeData;
					this._strokeLookupTable[stroke].StorePressure = storePressure;
				}
				if (this._inkSpaceRectangle != default(Rect))
				{
					num2 = num;
					Rect inkSpaceRectangle = this._inkSpaceRectangle;
					num += SerializationHelper.Encode(memoryStream, 0U);
					int value = (int)inkSpaceRectangle.Left;
					num += SerializationHelper.SignEncode(memoryStream, value);
					value = (int)inkSpaceRectangle.Top;
					num += SerializationHelper.SignEncode(memoryStream, value);
					value = (int)inkSpaceRectangle.Right;
					num += SerializationHelper.SignEncode(memoryStream, value);
					value = (int)inkSpaceRectangle.Bottom;
					num += SerializationHelper.SignEncode(memoryStream, value);
					num2 = num - num2;
					if ((ulong)num != (ulong)memoryStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				if (this.CurrentPersistenceFormat != PersistenceFormat.InkSerializedFormat)
				{
					num2 = num;
					num += SerializationHelper.Encode(memoryStream, 28U);
					num += SerializationHelper.Encode(memoryStream, SerializationHelper.VarSize((uint)this.CurrentPersistenceFormat));
					num += SerializationHelper.Encode(memoryStream, (uint)this.CurrentPersistenceFormat);
					num2 = num - num2;
					if ((ulong)num != (ulong)memoryStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				num2 = num;
				num += guidList.Save(memoryStream);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				this.BuildTables(guidList);
				num2 = num;
				num += this.SerializeDrawingAttrsTable(memoryStream, guidList);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				num2 = num;
				num += this.SerializePacketDescrTable(memoryStream);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				num2 = num;
				num += this.SerializeMetricTable(memoryStream);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				num2 = num;
				num += this.SerializeTransformTable(memoryStream);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				if (this._coreStrokes.ExtendedProperties.Count > 0)
				{
					num2 = num;
					num += ExtendedPropertySerializer.EncodeAsISF(this._coreStrokes.ExtendedProperties, memoryStream, guidList, this.GetCompressionAlgorithm(), true);
					num2 = num - num2;
					if ((ulong)num != (ulong)memoryStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				num2 = num;
				num += StrokeCollectionSerializer.SaveStrokeIds(this._coreStrokes, memoryStream, false);
				num2 = num - num2;
				if ((ulong)num != (ulong)memoryStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
				this.StoreStrokeData(memoryStream, guidList, ref num, ref num2);
				long position = outputStream.Position;
				uint num3 = SerializationHelper.Encode(outputStream, 0U);
				num3 += SerializationHelper.Encode(outputStream, num);
				outputStream.Write(memoryStream.GetBuffer(), 0, (int)num);
				num3 += num;
				if ((ulong)num3 != (ulong)(outputStream.Position - position))
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0015840C File Offset: 0x0015780C
		private void StoreStrokeData(Stream localStream, GuidList guidList, ref uint cumulativeEncodedSize, ref uint localEncodedSize)
		{
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			int[] strokeIds = StrokeIdGenerator.GetStrokeIds(this._coreStrokes);
			for (int i = 0; i < this._coreStrokes.Count; i++)
			{
				Stroke stroke = this._coreStrokes[i];
				if (num != this._strokeLookupTable[stroke].DrawingAttributesTableIndex)
				{
					localEncodedSize = cumulativeEncodedSize;
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, 9U);
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, this._strokeLookupTable[stroke].DrawingAttributesTableIndex);
					num = this._strokeLookupTable[stroke].DrawingAttributesTableIndex;
					localEncodedSize = cumulativeEncodedSize - localEncodedSize;
					uint num5 = localEncodedSize;
					if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				if (num2 != this._strokeLookupTable[stroke].StrokeDescriptorTableIndex)
				{
					localEncodedSize = cumulativeEncodedSize;
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, 13U);
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, this._strokeLookupTable[stroke].StrokeDescriptorTableIndex);
					num2 = this._strokeLookupTable[stroke].StrokeDescriptorTableIndex;
					localEncodedSize = cumulativeEncodedSize - localEncodedSize;
					uint num6 = localEncodedSize;
					if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				if (num3 != this._strokeLookupTable[stroke].MetricDescriptorTableIndex)
				{
					localEncodedSize = cumulativeEncodedSize;
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, 26U);
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, this._strokeLookupTable[stroke].MetricDescriptorTableIndex);
					num3 = this._strokeLookupTable[stroke].MetricDescriptorTableIndex;
					localEncodedSize = cumulativeEncodedSize - localEncodedSize;
					uint num7 = localEncodedSize;
					if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				if (num4 != this._strokeLookupTable[stroke].TransformTableIndex)
				{
					localEncodedSize = cumulativeEncodedSize;
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, 23U);
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, this._strokeLookupTable[stroke].TransformTableIndex);
					num4 = this._strokeLookupTable[stroke].TransformTableIndex;
					localEncodedSize = cumulativeEncodedSize - localEncodedSize;
					uint num8 = localEncodedSize;
					if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				using (MemoryStream memoryStream = new MemoryStream(stroke.StylusPoints.Count * 5))
				{
					localEncodedSize = cumulativeEncodedSize;
					uint num9 = StrokeSerializer.EncodeStroke(stroke, memoryStream, this.GetCompressionAlgorithm(), guidList, this._strokeLookupTable[stroke]);
					if ((ulong)num9 != (ulong)memoryStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Encoded stroke size != reported size"));
					}
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, 10U);
					cumulativeEncodedSize += SerializationHelper.Encode(localStream, num9);
					localStream.Write(memoryStream.GetBuffer(), 0, (int)num9);
					cumulativeEncodedSize += num9;
					localEncodedSize = cumulativeEncodedSize - localEncodedSize;
					uint num10 = localEncodedSize;
					if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
					{
						throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
					}
				}
				if ((ulong)cumulativeEncodedSize != (ulong)localStream.Length)
				{
					throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage("Calculated ISF stream size != actual stream size"));
				}
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0015873C File Offset: 0x00157B3C
		internal static uint SaveStrokeIds(StrokeCollection strokes, Stream strm, bool forceSave)
		{
			if (strokes.Count == 0)
			{
				return 0U;
			}
			int[] strokeIds = StrokeIdGenerator.GetStrokeIds(strokes);
			bool flag = true;
			if (!forceSave)
			{
				for (int i = 0; i < strokeIds.Length; i++)
				{
					if (strokeIds[i] != i + 1)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return 0U;
				}
			}
			uint num = SerializationHelper.Encode(strm, 30U);
			byte defaultCompression = AlgoModule.DefaultCompression;
			byte[] array = Compressor.CompressPacketData(strokeIds, ref defaultCompression);
			if (array != null)
			{
				num += SerializationHelper.Encode(strm, (uint)((long)array.Length + (long)((ulong)SerializationHelper.VarSize((uint)strokes.Count))));
				num += SerializationHelper.Encode(strm, (uint)strokes.Count);
				strm.Write(array, 0, array.Length);
				num += (uint)array.Length;
			}
			else
			{
				byte noCompression = AlgoModule.NoCompression;
				uint value = (uint)((long)strokes.Count * (long)((ulong)Native.SizeOfInt) + 1L + (long)((ulong)SerializationHelper.VarSize((uint)strokes.Count)));
				num += SerializationHelper.Encode(strm, value);
				num += SerializationHelper.Encode(strm, (uint)strokes.Count);
				strm.WriteByte(noCompression);
				num += 1U;
				BinaryWriter binaryWriter = new BinaryWriter(strm);
				for (int j = 0; j < strokeIds.Length; j++)
				{
					binaryWriter.Write(strokeIds[j]);
					num += Native.SizeOfInt;
				}
			}
			return num;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0015885C File Offset: 0x00157C5C
		private bool IsBase64Data(Stream data)
		{
			long position = data.Position;
			bool result;
			try
			{
				byte[] base64HeaderBytes = StrokeCollectionSerializer.Base64HeaderBytes;
				if (data.Length < (long)base64HeaderBytes.Length)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < base64HeaderBytes.Length; i++)
					{
						if ((byte)data.ReadByte() != base64HeaderBytes[i])
						{
							return false;
						}
					}
					result = true;
				}
			}
			finally
			{
				data.Position = position;
			}
			return result;
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x001588D0 File Offset: 0x00157CD0
		private GuidList BuildGuidList()
		{
			GuidList guidList = new GuidList();
			ExtendedPropertyCollection extendedProperties = this._coreStrokes.ExtendedProperties;
			for (int i = 0; i < extendedProperties.Count; i++)
			{
				guidList.Add(extendedProperties[i].Id);
			}
			for (int j = 0; j < this._coreStrokes.Count; j++)
			{
				this.BuildStrokeGuidList(this._coreStrokes[j], guidList);
			}
			return guidList;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00158940 File Offset: 0x00157D40
		private void BuildStrokeGuidList(Stroke stroke, GuidList guidList)
		{
			int num;
			Guid[] unknownGuids = ExtendedPropertySerializer.GetUnknownGuids(stroke.DrawingAttributes.ExtendedProperties, out num);
			for (int i = 0; i < num; i++)
			{
				guidList.Add(unknownGuids[i]);
			}
			Guid[] stylusPointPropertyIds = stroke.StylusPoints.Description.GetStylusPointPropertyIds();
			for (int i = 0; i < stylusPointPropertyIds.Length; i++)
			{
				guidList.Add(stylusPointPropertyIds[i]);
			}
			if (stroke.ExtendedProperties.Count > 0)
			{
				for (int i = 0; i < stroke.ExtendedProperties.Count; i++)
				{
					guidList.Add(stroke.ExtendedProperties[i].Id);
				}
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x001589E8 File Offset: 0x00157DE8
		private byte GetCompressionAlgorithm()
		{
			if (this.CurrentCompressionMode == CompressionMode.Compressed)
			{
				return AlgoModule.DefaultCompression;
			}
			return AlgoModule.NoCompression;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00158A08 File Offset: 0x00157E08
		private uint SerializePacketDescrTable(Stream strm)
		{
			if (this._strokeDescriptorTable.Count == 0)
			{
				return 0U;
			}
			uint num = 0U;
			if (this._strokeDescriptorTable.Count == 1)
			{
				StrokeDescriptor strokeDescriptor = this._strokeDescriptorTable[0];
				if (strokeDescriptor.Template.Count == 0)
				{
					return 0U;
				}
				num += SerializationHelper.Encode(strm, 5U);
				num += this.EncodeStrokeDescriptor(strm, strokeDescriptor);
			}
			else
			{
				uint num2 = 0U;
				for (int i = 0; i < this._strokeDescriptorTable.Count; i++)
				{
					num2 += SerializationHelper.VarSize(this._strokeDescriptorTable[i].Size) + this._strokeDescriptorTable[i].Size;
				}
				num += SerializationHelper.Encode(strm, 4U);
				num += SerializationHelper.Encode(strm, num2);
				for (int i = 0; i < this._strokeDescriptorTable.Count; i++)
				{
					num += this.EncodeStrokeDescriptor(strm, this._strokeDescriptorTable[i]);
				}
			}
			return num;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00158AF0 File Offset: 0x00157EF0
		private uint SerializeMetricTable(Stream strm)
		{
			uint num = 0U;
			if (this._metricTable.Count == 0)
			{
				return 0U;
			}
			for (int i = 0; i < this._metricTable.Count; i++)
			{
				num += this._metricTable[i].Size;
			}
			uint num2 = 0U;
			if (1U == num)
			{
				return 0U;
			}
			if (1 == this._metricTable.Count)
			{
				num2 += SerializationHelper.Encode(strm, 25U);
			}
			else
			{
				num2 += SerializationHelper.Encode(strm, 24U);
				num2 += SerializationHelper.Encode(strm, num);
			}
			for (int j = 0; j < this._metricTable.Count; j++)
			{
				MetricBlock metricBlock = this._metricTable[j];
				num2 += metricBlock.Pack(strm);
			}
			return num2;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00158BA0 File Offset: 0x00157FA0
		private uint EncodeStrokeDescriptor(Stream strm, StrokeDescriptor strd)
		{
			uint num = 0U;
			num += SerializationHelper.Encode(strm, strd.Size);
			for (int i = 0; i < strd.Template.Count; i++)
			{
				num += SerializationHelper.Encode(strm, (uint)strd.Template[i]);
			}
			return num;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00158BEC File Offset: 0x00157FEC
		private uint SerializeTransformTable(Stream strm)
		{
			if (this._transformTable.Count == 1 && this._transformTable[0].Size == 0U)
			{
				return 0U;
			}
			uint num = 0U;
			uint num2 = 0U;
			for (int i = 0; i < this._transformTable.Count; i++)
			{
				TransformDescriptor transformDescriptor = this._transformTable[i];
				uint num3 = SerializationHelper.VarSize((uint)transformDescriptor.Tag);
				num += num3;
				num2 += num3;
				if (KnownTagCache.KnownTagIndex.TransformRotate == transformDescriptor.Tag)
				{
					num3 = SerializationHelper.VarSize((uint)(transformDescriptor.Transform[0] + 0.5));
					num += num3;
					num2 += num3;
				}
				else
				{
					num3 = transformDescriptor.Size * Native.SizeOfFloat;
					num += num3;
					num2 += num3 * 2U;
				}
			}
			uint num4 = 0U;
			if (this._transformTable.Count == 1)
			{
				TransformDescriptor xform = this._transformTable[0];
				num4 = this.EncodeTransformDescriptor(strm, xform, false);
			}
			else
			{
				num4 += SerializationHelper.Encode(strm, 15U);
				num4 += SerializationHelper.Encode(strm, num);
				for (int j = 0; j < this._transformTable.Count; j++)
				{
					num4 += this.EncodeTransformDescriptor(strm, this._transformTable[j], false);
				}
			}
			num4 += SerializationHelper.Encode(strm, 31U);
			num4 += SerializationHelper.Encode(strm, num2);
			for (int k = 0; k < this._transformTable.Count; k++)
			{
				num4 += this.EncodeTransformDescriptor(strm, this._transformTable[k], true);
			}
			return num4;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00158D60 File Offset: 0x00158160
		private uint EncodeTransformDescriptor(Stream strm, TransformDescriptor xform, bool useDoubles)
		{
			uint num = SerializationHelper.Encode(strm, (uint)xform.Tag);
			if (KnownTagCache.KnownTagIndex.TransformRotate == xform.Tag)
			{
				uint value = (uint)(xform.Transform[0] + 0.5);
				num += SerializationHelper.Encode(strm, value);
			}
			else
			{
				BinaryWriter binaryWriter = new BinaryWriter(strm);
				int num2 = 0;
				while ((long)num2 < (long)((ulong)xform.Size))
				{
					if (useDoubles)
					{
						binaryWriter.Write(xform.Transform[num2]);
						num += Native.SizeOfDouble;
					}
					else
					{
						binaryWriter.Write((float)xform.Transform[num2]);
						num += Native.SizeOfFloat;
					}
					num2++;
				}
			}
			return num;
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00158DF4 File Offset: 0x001581F4
		private uint SerializeDrawingAttrsTable(Stream stream, GuidList guidList)
		{
			uint num = 0U;
			uint num2 = 0U;
			if (1 == this._drawingAttributesTable.Count)
			{
				DrawingAttributes da = this._drawingAttributesTable[0];
				num += SerializationHelper.Encode(stream, 3U);
				using (MemoryStream memoryStream = new MemoryStream(16))
				{
					num2 = DrawingAttributeSerializer.EncodeAsISF(da, memoryStream, guidList, 0, true);
					num += SerializationHelper.Encode(stream, num2);
					uint num3 = Convert.ToUInt32(memoryStream.Position);
					num += num3;
					stream.Write(memoryStream.GetBuffer(), 0, Convert.ToInt32(num3));
					memoryStream.Dispose();
					return num;
				}
			}
			uint[] array = new uint[this._drawingAttributesTable.Count];
			MemoryStream[] array2 = new MemoryStream[this._drawingAttributesTable.Count];
			for (int i = 0; i < this._drawingAttributesTable.Count; i++)
			{
				DrawingAttributes da2 = this._drawingAttributesTable[i];
				array2[i] = new MemoryStream(16);
				array[i] = DrawingAttributeSerializer.EncodeAsISF(da2, array2[i], guidList, 0, true);
				num2 += SerializationHelper.VarSize(array[i]) + array[i];
			}
			num = SerializationHelper.Encode(stream, 2U);
			num += SerializationHelper.Encode(stream, num2);
			for (int j = 0; j < this._drawingAttributesTable.Count; j++)
			{
				DrawingAttributes drawingAttributes = this._drawingAttributesTable[j];
				num += SerializationHelper.Encode(stream, array[j]);
				uint num4 = Convert.ToUInt32(array2[j].Position);
				num += num4;
				stream.Write(array2[j].GetBuffer(), 0, Convert.ToInt32(num4));
				array2[j].Dispose();
			}
			return num;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00158F98 File Offset: 0x00158398
		private void BuildTables(GuidList guidList)
		{
			this._transformTable.Clear();
			this._strokeDescriptorTable.Clear();
			this._metricTable.Clear();
			this._drawingAttributesTable.Clear();
			for (int i = 0; i < this._coreStrokes.Count; i++)
			{
				Stroke stroke = this._coreStrokes[i];
				StrokeDescriptor strokeDescriptor;
				MetricBlock metricBlock;
				StrokeSerializer.BuildStrokeDescriptor(stroke, guidList, this._strokeLookupTable[stroke], out strokeDescriptor, out metricBlock);
				bool flag = false;
				for (int j = 0; j < this._strokeDescriptorTable.Count; j++)
				{
					if (strokeDescriptor.IsEqual(this._strokeDescriptorTable[j]))
					{
						flag = true;
						this._strokeLookupTable[stroke].StrokeDescriptorTableIndex = (uint)j;
						break;
					}
				}
				if (!flag)
				{
					this._strokeDescriptorTable.Add(strokeDescriptor);
					this._strokeLookupTable[stroke].StrokeDescriptorTableIndex = (uint)(this._strokeDescriptorTable.Count - 1);
				}
				flag = false;
				for (int k = 0; k < this._metricTable.Count; k++)
				{
					MetricBlock metricBlock2 = this._metricTable[k];
					SetType setType = SetType.SubSet;
					if (metricBlock2.CompareMetricBlock(metricBlock, ref setType))
					{
						if (setType == SetType.SuperSet)
						{
							this._metricTable[k] = metricBlock;
						}
						flag = true;
						this._strokeLookupTable[stroke].MetricDescriptorTableIndex = (uint)k;
						break;
					}
				}
				if (!flag)
				{
					this._metricTable.Add(metricBlock);
					this._strokeLookupTable[stroke].MetricDescriptorTableIndex = (uint)(this._metricTable.Count - 1);
				}
				flag = false;
				TransformDescriptor identityTransformDescriptor = StrokeCollectionSerializer.IdentityTransformDescriptor;
				for (int l = 0; l < this._transformTable.Count; l++)
				{
					if (identityTransformDescriptor.Compare(this._transformTable[l]))
					{
						flag = true;
						this._strokeLookupTable[stroke].TransformTableIndex = (uint)l;
						break;
					}
				}
				if (!flag)
				{
					this._transformTable.Add(identityTransformDescriptor);
					this._strokeLookupTable[stroke].TransformTableIndex = (uint)(this._transformTable.Count - 1);
				}
				flag = false;
				DrawingAttributes drawingAttributes = this._coreStrokes[i].DrawingAttributes;
				for (int m = 0; m < this._drawingAttributesTable.Count; m++)
				{
					if (drawingAttributes.Equals(this._drawingAttributesTable[m]))
					{
						flag = true;
						this._strokeLookupTable[stroke].DrawingAttributesTableIndex = (uint)m;
						break;
					}
				}
				if (!flag)
				{
					this._drawingAttributesTable.Add(drawingAttributes);
					this._strokeLookupTable[stroke].DrawingAttributesTableIndex = (uint)(this._drawingAttributesTable.Count - 1);
				}
			}
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00159220 File Offset: 0x00158620
		[Conditional("DEBUG_ISF")]
		private static void ISFDebugTrace(string message)
		{
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00159230 File Offset: 0x00158630
		internal static string ISFDebugMessage(string debugMessage)
		{
			return SR.Get("IsfOperationFailed");
		}

		// Token: 0x040025E2 RID: 9698
		internal static readonly double AvalonToHimetricMultiplier = 26.458333333333332;

		// Token: 0x040025E3 RID: 9699
		internal static readonly double HimetricToAvalonMultiplier = 0.037795275590551181;

		// Token: 0x040025E4 RID: 9700
		internal static readonly TransformDescriptor IdentityTransformDescriptor;

		// Token: 0x040025E5 RID: 9701
		internal PersistenceFormat CurrentPersistenceFormat;

		// Token: 0x040025E6 RID: 9702
		internal CompressionMode CurrentCompressionMode;

		// Token: 0x040025E7 RID: 9703
		internal List<int> StrokeIds;

		// Token: 0x040025E8 RID: 9704
		private static readonly byte[] Base64HeaderBytes = new byte[]
		{
			98,
			97,
			115,
			101,
			54,
			52,
			58
		};

		// Token: 0x040025E9 RID: 9705
		private StrokeCollection _coreStrokes;

		// Token: 0x040025EA RID: 9706
		private List<StrokeDescriptor> _strokeDescriptorTable;

		// Token: 0x040025EB RID: 9707
		private List<TransformDescriptor> _transformTable;

		// Token: 0x040025EC RID: 9708
		private List<DrawingAttributes> _drawingAttributesTable;

		// Token: 0x040025ED RID: 9709
		private List<MetricBlock> _metricTable;

		// Token: 0x040025EE RID: 9710
		private Vector _himetricSize = new Vector(0.0, 0.0);

		// Token: 0x040025EF RID: 9711
		private Rect _inkSpaceRectangle;

		// Token: 0x040025F0 RID: 9712
		private Dictionary<Stroke, StrokeCollectionSerializer.StrokeLookupEntry> _strokeLookupTable;

		// Token: 0x02000A0B RID: 2571
		internal class StrokeLookupEntry
		{
			// Token: 0x04002F55 RID: 12117
			internal uint MetricDescriptorTableIndex;

			// Token: 0x04002F56 RID: 12118
			internal uint StrokeDescriptorTableIndex;

			// Token: 0x04002F57 RID: 12119
			internal uint TransformTableIndex;

			// Token: 0x04002F58 RID: 12120
			internal uint DrawingAttributesTableIndex;

			// Token: 0x04002F59 RID: 12121
			internal byte CompressionData;

			// Token: 0x04002F5A RID: 12122
			internal int[][] ISFReadyStrokeData;

			// Token: 0x04002F5B RID: 12123
			internal bool StorePressure;
		}
	}
}
