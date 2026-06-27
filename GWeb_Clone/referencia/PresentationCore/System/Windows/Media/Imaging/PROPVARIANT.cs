using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005F8 RID: 1528
	[StructLayout(LayoutKind.Explicit, Pack = 1)]
	internal struct PROPVARIANT
	{
		// Token: 0x060045EA RID: 17898 RVA: 0x0011082C File Offset: 0x0010FC2C
		[SecurityCritical]
		private unsafe static void CopyBytes(byte* pbTo, int cbTo, byte* pbFrom, int cbFrom)
		{
			if (cbFrom > cbTo)
			{
				throw new InvalidOperationException(SR.Get("Image_InsufficientBufferSize"));
			}
			for (int i = 0; i < cbFrom; i++)
			{
				pbTo[i] = pbFrom[i];
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00110868 File Offset: 0x0010FC68
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void InitVector(Array array, Type type, VarEnum varEnum)
		{
			this.Init(array, type, varEnum | VarEnum.VT_VECTOR);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00110884 File Offset: 0x0010FC84
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe void Init(Array array, Type type, VarEnum vt)
		{
			this.varType = (ushort)vt;
			this.ca.cElems = 0U;
			this.ca.pElems = IntPtr.Zero;
			int length = array.Length;
			if (length > 0)
			{
				long num = (long)(Marshal.SizeOf(type) * length);
				IntPtr intPtr = IntPtr.Zero;
				GCHandle gchandle = default(GCHandle);
				try
				{
					intPtr = Marshal.AllocCoTaskMem((int)num);
					gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					PROPVARIANT.CopyBytes((byte*)((void*)intPtr), (int)num, (byte*)((void*)gchandle.AddrOfPinnedObject()), (int)num);
					this.ca.cElems = (uint)length;
					this.ca.pElems = intPtr;
					intPtr = IntPtr.Zero;
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00110968 File Offset: 0x0010FD68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Init(string[] value, bool fAscii)
		{
			this.varType = (fAscii ? 30 : 31);
			this.varType |= 4096;
			this.ca.cElems = 0U;
			this.ca.pElems = IntPtr.Zero;
			int num = value.Length;
			if (num > 0)
			{
				IntPtr intPtr = IntPtr.Zero;
				int num2 = 0;
				num2 = sizeof(IntPtr);
				long num3 = (long)(num2 * num);
				int i = 0;
				try
				{
					IntPtr val = IntPtr.Zero;
					intPtr = Marshal.AllocCoTaskMem((int)num3);
					for (i = 0; i < num; i++)
					{
						if (fAscii)
						{
							val = Marshal.StringToCoTaskMemAnsi(value[i]);
						}
						else
						{
							val = Marshal.StringToCoTaskMemUni(value[i]);
						}
						Marshal.WriteIntPtr(intPtr, i * num2, val);
					}
					this.ca.cElems = (uint)num;
					this.ca.pElems = intPtr;
					intPtr = IntPtr.Zero;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						for (int j = 0; j < i; j++)
						{
							IntPtr ptr = Marshal.ReadIntPtr(intPtr, j * num2);
							Marshal.FreeCoTaskMem(ptr);
						}
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00110A8C File Offset: 0x0010FE8C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Init(object value)
		{
			if (value == null)
			{
				this.varType = 0;
				return;
			}
			if (value is Array)
			{
				Type type = value.GetType();
				if (type == typeof(sbyte[]))
				{
					this.InitVector(value as Array, typeof(sbyte), VarEnum.VT_I1);
					return;
				}
				if (type == typeof(byte[]))
				{
					this.InitVector(value as Array, typeof(byte), VarEnum.VT_UI1);
					return;
				}
				if (value is char[])
				{
					this.varType = 30;
					this.pszVal = Marshal.StringToCoTaskMemAnsi(new string(value as char[]));
					return;
				}
				if (value is char[][])
				{
					char[][] array = value as char[][];
					string[] array2 = new string[array.GetLength(0)];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new string(array[i]);
					}
					this.Init(array2, true);
					return;
				}
				if (type == typeof(short[]))
				{
					this.InitVector(value as Array, typeof(short), VarEnum.VT_I2);
					return;
				}
				if (type == typeof(ushort[]))
				{
					this.InitVector(value as Array, typeof(ushort), VarEnum.VT_UI2);
					return;
				}
				if (type == typeof(int[]))
				{
					this.InitVector(value as Array, typeof(int), VarEnum.VT_I4);
					return;
				}
				if (type == typeof(uint[]))
				{
					this.InitVector(value as Array, typeof(uint), VarEnum.VT_UI4);
					return;
				}
				if (type == typeof(long[]))
				{
					this.InitVector(value as Array, typeof(long), VarEnum.VT_I8);
					return;
				}
				if (type == typeof(ulong[]))
				{
					this.InitVector(value as Array, typeof(ulong), VarEnum.VT_UI8);
					return;
				}
				if (value is float[])
				{
					this.InitVector(value as Array, typeof(float), VarEnum.VT_R4);
					return;
				}
				if (value is double[])
				{
					this.InitVector(value as Array, typeof(double), VarEnum.VT_R8);
					return;
				}
				if (value is Guid[])
				{
					this.InitVector(value as Array, typeof(Guid), VarEnum.VT_CLSID);
					return;
				}
				if (value is string[])
				{
					this.Init(value as string[], false);
					return;
				}
				if (value is bool[])
				{
					bool[] array3 = value as bool[];
					short[] array4 = new short[array3.Length];
					for (int j = 0; j < array3.Length; j++)
					{
						array4[j] = (array3[j] ? -1 : 0);
					}
					this.InitVector(array4, typeof(short), VarEnum.VT_BOOL);
					return;
				}
				throw new InvalidOperationException(SR.Get("Image_PropertyNotSupported"));
			}
			else
			{
				Type type2 = value.GetType();
				if (value is string)
				{
					this.varType = 31;
					this.pwszVal = Marshal.StringToCoTaskMemUni(value as string);
					return;
				}
				if (type2 == typeof(sbyte))
				{
					this.varType = 16;
					this.cVal = (sbyte)value;
					return;
				}
				if (type2 == typeof(byte))
				{
					this.varType = 17;
					this.bVal = (byte)value;
					return;
				}
				if (type2 == typeof(System.Runtime.InteropServices.ComTypes.FILETIME))
				{
					this.varType = 64;
					this.filetime = (System.Runtime.InteropServices.ComTypes.FILETIME)value;
					return;
				}
				if (value is char)
				{
					this.varType = 30;
					this.pszVal = Marshal.StringToCoTaskMemAnsi(new string(new char[]
					{
						(char)value
					}));
					return;
				}
				if (type2 == typeof(short))
				{
					this.varType = 2;
					this.iVal = (short)value;
					return;
				}
				if (type2 == typeof(ushort))
				{
					this.varType = 18;
					this.uiVal = (ushort)value;
					return;
				}
				if (type2 == typeof(int))
				{
					this.varType = 3;
					this.intVal = (int)value;
					return;
				}
				if (type2 == typeof(uint))
				{
					this.varType = 19;
					this.uintVal = (uint)value;
					return;
				}
				if (type2 == typeof(long))
				{
					this.varType = 20;
					this.lVal = (long)value;
					return;
				}
				if (type2 == typeof(ulong))
				{
					this.varType = 21;
					this.ulVal = (ulong)value;
					return;
				}
				if (value is float)
				{
					this.varType = 4;
					this.fltVal = (float)value;
					return;
				}
				if (value is double)
				{
					this.varType = 5;
					this.dblVal = (double)value;
					return;
				}
				if (value is Guid)
				{
					byte[] array5 = ((Guid)value).ToByteArray();
					this.varType = 72;
					this.pclsidVal = Marshal.AllocCoTaskMem(array5.Length);
					Marshal.Copy(array5, 0, this.pclsidVal, array5.Length);
					return;
				}
				if (value is bool)
				{
					this.varType = 11;
					this.boolVal = (((bool)value) ? -1 : 0);
					return;
				}
				if (value is BitmapMetadataBlob)
				{
					this.Init((value as BitmapMetadataBlob).InternalGetBlobValue(), typeof(byte), VarEnum.VT_BLOB);
					return;
				}
				if (!(value is BitmapMetadata))
				{
					throw new InvalidOperationException(SR.Get("Image_PropertyNotSupported"));
				}
				IntPtr zero = IntPtr.Zero;
				BitmapMetadata bitmapMetadata = value as BitmapMetadata;
				SafeMILHandle internalMetadataHandle = bitmapMetadata.InternalMetadataHandle;
				if (internalMetadataHandle == null || internalMetadataHandle.IsInvalid)
				{
					throw new NotImplementedException();
				}
				Guid iid_IWICMetadataQueryReader = MILGuidData.IID_IWICMetadataQueryReader;
				HRESULT.Check(UnsafeNativeMethods.MILUnknown.QueryInterface(internalMetadataHandle, ref iid_IWICMetadataQueryReader, out zero));
				this.varType = 13;
				this.punkVal = zero;
				return;
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00111034 File Offset: 0x00110434
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Clear()
		{
			VarEnum varEnum = (VarEnum)this.varType;
			if ((varEnum & VarEnum.VT_VECTOR) != VarEnum.VT_EMPTY || varEnum == VarEnum.VT_BLOB)
			{
				if (this.ca.pElems != IntPtr.Zero)
				{
					varEnum &= (VarEnum)(-4097);
					if (varEnum == VarEnum.VT_UNKNOWN)
					{
						IntPtr pElems = this.ca.pElems;
						int num = sizeof(IntPtr);
						for (uint num2 = 0U; num2 < this.ca.cElems; num2 += 1U)
						{
							UnsafeNativeMethods.MILUnknown.Release(Marshal.ReadIntPtr(pElems, (int)((ulong)num2 * (ulong)((long)num))));
						}
					}
					else if (varEnum == VarEnum.VT_LPWSTR || varEnum == VarEnum.VT_LPSTR)
					{
						IntPtr pElems2 = this.ca.pElems;
						int num3 = sizeof(IntPtr);
						for (uint num4 = 0U; num4 < this.ca.cElems; num4 += 1U)
						{
							Marshal.FreeCoTaskMem(Marshal.ReadIntPtr(pElems2, (int)((ulong)num4 * (ulong)((long)num3))));
						}
					}
					Marshal.FreeCoTaskMem(this.ca.pElems);
				}
			}
			else if (varEnum == VarEnum.VT_LPWSTR || varEnum == VarEnum.VT_LPSTR || varEnum == VarEnum.VT_CLSID)
			{
				Marshal.FreeCoTaskMem(this.pwszVal);
			}
			else if (varEnum == VarEnum.VT_UNKNOWN)
			{
				UnsafeNativeMethods.MILUnknown.Release(this.punkVal);
			}
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00111150 File Offset: 0x00110550
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal object ToObject(object syncObject)
		{
			VarEnum varEnum = (VarEnum)this.varType;
			if ((varEnum & VarEnum.VT_VECTOR) != VarEnum.VT_EMPTY)
			{
				VarEnum varEnum2 = varEnum & (VarEnum)(-4097);
				if (varEnum2 <= VarEnum.VT_LPSTR)
				{
					switch (varEnum2)
					{
					case VarEnum.VT_EMPTY:
						return null;
					case VarEnum.VT_NULL:
					case VarEnum.VT_CY:
					case VarEnum.VT_DATE:
					case VarEnum.VT_BSTR:
					case VarEnum.VT_DISPATCH:
					case VarEnum.VT_ERROR:
					case VarEnum.VT_VARIANT:
					case VarEnum.VT_UNKNOWN:
					case VarEnum.VT_DECIMAL:
					case (VarEnum)15:
						break;
					case VarEnum.VT_I2:
					{
						short[] array = new short[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array, 0, (int)this.ca.cElems);
						return array;
					}
					case VarEnum.VT_I4:
					{
						int[] array2 = new int[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array2, 0, (int)this.ca.cElems);
						return array2;
					}
					case VarEnum.VT_R4:
					{
						float[] array3 = new float[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array3, 0, (int)this.ca.cElems);
						return array3;
					}
					case VarEnum.VT_R8:
					{
						double[] array4 = new double[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array4, 0, (int)this.ca.cElems);
						return array4;
					}
					case VarEnum.VT_BOOL:
					{
						bool[] array5 = new bool[this.ca.cElems];
						int num = 0;
						while ((long)num < (long)((ulong)this.ca.cElems))
						{
							array5[num] = (Marshal.ReadInt16(this.ca.pElems, num * 2) != 0);
							num++;
						}
						return array5;
					}
					case VarEnum.VT_I1:
					{
						sbyte[] array6 = new sbyte[this.ca.cElems];
						int num2 = 0;
						while ((long)num2 < (long)((ulong)this.ca.cElems))
						{
							array6[num2] = (sbyte)Marshal.ReadByte(this.ca.pElems, num2);
							num2++;
						}
						return array6;
					}
					case VarEnum.VT_UI1:
					{
						byte[] array7 = new byte[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array7, 0, (int)this.ca.cElems);
						return array7;
					}
					case VarEnum.VT_UI2:
					{
						ushort[] array8 = new ushort[this.ca.cElems];
						int num3 = 0;
						while ((long)num3 < (long)((ulong)this.ca.cElems))
						{
							array8[num3] = (ushort)Marshal.ReadInt16(this.ca.pElems, num3 * 2);
							num3++;
						}
						return array8;
					}
					case VarEnum.VT_UI4:
					{
						uint[] array9 = new uint[this.ca.cElems];
						int num4 = 0;
						while ((long)num4 < (long)((ulong)this.ca.cElems))
						{
							array9[num4] = (uint)Marshal.ReadInt32(this.ca.pElems, num4 * 4);
							num4++;
						}
						return array9;
					}
					case VarEnum.VT_I8:
					{
						long[] array10 = new long[this.ca.cElems];
						Marshal.Copy(this.ca.pElems, array10, 0, (int)this.ca.cElems);
						return array10;
					}
					case VarEnum.VT_UI8:
					{
						ulong[] array11 = new ulong[this.ca.cElems];
						int num5 = 0;
						while ((long)num5 < (long)((ulong)this.ca.cElems))
						{
							array11[num5] = (ulong)Marshal.ReadInt64(this.ca.pElems, num5 * 8);
							num5++;
						}
						return array11;
					}
					default:
						if (varEnum2 == VarEnum.VT_LPSTR)
						{
							string[] array12 = new string[this.ca.cElems];
							int num6 = sizeof(IntPtr);
							int num7 = 0;
							while ((long)num7 < (long)((ulong)this.ca.cElems))
							{
								IntPtr ptr = Marshal.ReadIntPtr(this.ca.pElems, num7 * num6);
								array12[num7] = Marshal.PtrToStringAnsi(ptr);
								num7++;
							}
							return array12;
						}
						break;
					}
				}
				else
				{
					if (varEnum2 == VarEnum.VT_LPWSTR)
					{
						string[] array13 = new string[this.ca.cElems];
						int num8 = sizeof(IntPtr);
						int num9 = 0;
						while ((long)num9 < (long)((ulong)this.ca.cElems))
						{
							IntPtr ptr2 = Marshal.ReadIntPtr(this.ca.pElems, num9 * num8);
							array13[num9] = Marshal.PtrToStringUni(ptr2);
							num9++;
						}
						return array13;
					}
					if (varEnum2 == VarEnum.VT_CLSID)
					{
						Guid[] array14 = new Guid[this.ca.cElems];
						int num10 = 0;
						while ((long)num10 < (long)((ulong)this.ca.cElems))
						{
							byte[] array15 = new byte[16];
							Marshal.Copy(this.ca.pElems, array15, num10 * 16, 16);
							array14[num10] = new Guid(array15);
							num10++;
						}
						return array14;
					}
				}
			}
			else if (varEnum <= VarEnum.VT_LPWSTR)
			{
				switch (varEnum)
				{
				case VarEnum.VT_EMPTY:
					return null;
				case VarEnum.VT_NULL:
				case VarEnum.VT_CY:
				case VarEnum.VT_DATE:
				case VarEnum.VT_BSTR:
				case VarEnum.VT_DISPATCH:
				case VarEnum.VT_ERROR:
				case VarEnum.VT_VARIANT:
				case VarEnum.VT_DECIMAL:
				case (VarEnum)15:
					break;
				case VarEnum.VT_I2:
					return this.iVal;
				case VarEnum.VT_I4:
					return this.intVal;
				case VarEnum.VT_R4:
					return this.fltVal;
				case VarEnum.VT_R8:
					return this.dblVal;
				case VarEnum.VT_BOOL:
					return this.boolVal != 0;
				case VarEnum.VT_UNKNOWN:
				{
					IntPtr zero = IntPtr.Zero;
					Guid iid_IWICMetadataQueryWriter = MILGuidData.IID_IWICMetadataQueryWriter;
					Guid iid_IWICMetadataQueryReader = MILGuidData.IID_IWICMetadataQueryReader;
					try
					{
						if (UnsafeNativeMethods.MILUnknown.QueryInterface(this.punkVal, ref iid_IWICMetadataQueryWriter, out zero) == 0)
						{
							SafeMILHandle metadataHandle = new SafeMILHandle(zero);
							zero = IntPtr.Zero;
							return new BitmapMetadata(metadataHandle, false, false, syncObject);
						}
						int num11 = UnsafeNativeMethods.MILUnknown.QueryInterface(this.punkVal, ref iid_IWICMetadataQueryReader, out zero);
						if (num11 == 0)
						{
							SafeMILHandle metadataHandle2 = new SafeMILHandle(zero);
							zero = IntPtr.Zero;
							return new BitmapMetadata(metadataHandle2, true, false, syncObject);
						}
						HRESULT.Check(num11);
					}
					finally
					{
						if (zero != IntPtr.Zero)
						{
							UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
						}
					}
					break;
				}
				case VarEnum.VT_I1:
					return this.cVal;
				case VarEnum.VT_UI1:
					return this.bVal;
				case VarEnum.VT_UI2:
					return this.uiVal;
				case VarEnum.VT_UI4:
					return this.uintVal;
				case VarEnum.VT_I8:
					return this.lVal;
				case VarEnum.VT_UI8:
					return this.ulVal;
				default:
					if (varEnum == VarEnum.VT_LPSTR)
					{
						return Marshal.PtrToStringAnsi(this.pszVal);
					}
					if (varEnum == VarEnum.VT_LPWSTR)
					{
						return Marshal.PtrToStringUni(this.pwszVal);
					}
					break;
				}
			}
			else
			{
				if (varEnum == VarEnum.VT_FILETIME)
				{
					return this.filetime;
				}
				if (varEnum == VarEnum.VT_BLOB)
				{
					byte[] array16 = new byte[this.ca.cElems];
					Marshal.Copy(this.ca.pElems, array16, 0, (int)this.ca.cElems);
					return new BitmapMetadataBlob(array16);
				}
				if (varEnum == VarEnum.VT_CLSID)
				{
					byte[] array17 = new byte[16];
					Marshal.Copy(this.pclsidVal, array17, 0, 16);
					return new Guid(array17);
				}
			}
			throw new NotSupportedException(SR.Get("Image_PropertyNotSupported"));
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x00111824 File Offset: 0x00110C24
		internal bool RequiresSyncObject
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this.varType == 13;
			}
		}

		// Token: 0x0400195D RID: 6493
		[FieldOffset(0)]
		internal ushort varType;

		// Token: 0x0400195E RID: 6494
		[FieldOffset(2)]
		internal ushort wReserved1;

		// Token: 0x0400195F RID: 6495
		[FieldOffset(4)]
		internal ushort wReserved2;

		// Token: 0x04001960 RID: 6496
		[FieldOffset(6)]
		internal ushort wReserved3;

		// Token: 0x04001961 RID: 6497
		[FieldOffset(8)]
		internal byte bVal;

		// Token: 0x04001962 RID: 6498
		[FieldOffset(8)]
		internal sbyte cVal;

		// Token: 0x04001963 RID: 6499
		[FieldOffset(8)]
		internal ushort uiVal;

		// Token: 0x04001964 RID: 6500
		[FieldOffset(8)]
		internal short iVal;

		// Token: 0x04001965 RID: 6501
		[FieldOffset(8)]
		internal uint uintVal;

		// Token: 0x04001966 RID: 6502
		[FieldOffset(8)]
		internal int intVal;

		// Token: 0x04001967 RID: 6503
		[FieldOffset(8)]
		internal ulong ulVal;

		// Token: 0x04001968 RID: 6504
		[FieldOffset(8)]
		internal long lVal;

		// Token: 0x04001969 RID: 6505
		[FieldOffset(8)]
		internal float fltVal;

		// Token: 0x0400196A RID: 6506
		[FieldOffset(8)]
		internal double dblVal;

		// Token: 0x0400196B RID: 6507
		[FieldOffset(8)]
		internal short boolVal;

		// Token: 0x0400196C RID: 6508
		[FieldOffset(8)]
		internal IntPtr pclsidVal;

		// Token: 0x0400196D RID: 6509
		[FieldOffset(8)]
		internal IntPtr pszVal;

		// Token: 0x0400196E RID: 6510
		[FieldOffset(8)]
		internal IntPtr pwszVal;

		// Token: 0x0400196F RID: 6511
		[FieldOffset(8)]
		internal IntPtr punkVal;

		// Token: 0x04001970 RID: 6512
		[FieldOffset(8)]
		internal PROPARRAY ca;

		// Token: 0x04001971 RID: 6513
		[FieldOffset(8)]
		internal System.Runtime.InteropServices.ComTypes.FILETIME filetime;
	}
}
