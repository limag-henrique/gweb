using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using MS.Internal.PresentationCore;
using MS.Win32;
using MS.Win32.Recognizer;

namespace MS.Internal.Ink.GestureRecognition
{
	// Token: 0x020007E2 RID: 2018
	internal sealed class NativeRecognizer : IDisposable
	{
		// Token: 0x060054BA RID: 21690 RVA: 0x0015D080 File Offset: 0x0015C480
		[SecurityCritical]
		private NativeRecognizer()
		{
			int hr = UnsafeNativeMethods.CreateContext(NativeRecognizer.RecognizerHandleSingleton, out this._hContext);
			if (HRESULT.Failed(hr))
			{
				throw new InvalidOperationException(SR.Get("UnspecifiedGestureConstructionException"));
			}
			this._hContext.AddReferenceOnRecognizer(NativeRecognizer.RecognizerHandleSingleton);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0015D0CC File Offset: 0x0015C4CC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static NativeRecognizer CreateInstance()
		{
			if (NativeRecognizer.RecognizerHandleSingleton != null)
			{
				return new NativeRecognizer();
			}
			return null;
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0015D0E8 File Offset: 0x0015C4E8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal ApplicationGesture[] SetEnabledGestures(IEnumerable<ApplicationGesture> applicationGestures)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("NativeRecognizer");
			}
			ApplicationGesture[] applicationGestureArrayAndVerify = NativeRecognizer.GetApplicationGestureArrayAndVerify(applicationGestures);
			int hr = this.SetEnabledGestures(this._hContext, applicationGestureArrayAndVerify);
			if (HRESULT.Failed(hr))
			{
				throw new InvalidOperationException(SR.Get("UnspecifiedSetEnabledGesturesException"));
			}
			return applicationGestureArrayAndVerify;
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0015D138 File Offset: 0x0015C538
		[SecurityCritical]
		internal GestureRecognitionResult[] Recognize(StrokeCollection strokes)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("NativeRecognizer");
			}
			if (strokes == null)
			{
				throw new ArgumentNullException("strokes");
			}
			if (strokes.Count > 2)
			{
				throw new ArgumentException(SR.Get("StrokeCollectionCountTooBig"), "strokes");
			}
			GestureRecognitionResult[] result = new GestureRecognitionResult[0];
			if (strokes.Count == 0)
			{
				return result;
			}
			int hr = 0;
			try
			{
				hr = UnsafeNativeMethods.ResetContext(this._hContext);
				if (HRESULT.Failed(hr))
				{
					return result;
				}
				hr = this.AddStrokes(this._hContext, strokes);
				if (HRESULT.Failed(hr))
				{
					return result;
				}
				bool flag;
				hr = UnsafeNativeMethods.Process(this._hContext, out flag);
				if (HRESULT.Succeeded(hr))
				{
					if (NativeRecognizer.s_GetAlternateListExists)
					{
						result = this.InvokeGetAlternateList();
					}
					else
					{
						result = this.InvokeGetLatticePtr();
					}
				}
			}
			finally
			{
				if (HRESULT.Failed(hr))
				{
					throw new InvalidOperationException(SR.Get("UnspecifiedGestureException"));
				}
			}
			return result;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0015D230 File Offset: 0x0015C630
		internal static ApplicationGesture[] GetApplicationGestureArrayAndVerify(IEnumerable<ApplicationGesture> applicationGestures)
		{
			if (applicationGestures == null)
			{
				throw new ArgumentNullException("applicationGestures");
			}
			uint num = 0U;
			ICollection<ApplicationGesture> collection = applicationGestures as ICollection<ApplicationGesture>;
			if (collection != null)
			{
				num = (uint)collection.Count;
			}
			else
			{
				foreach (ApplicationGesture applicationGesture in applicationGestures)
				{
					num += 1U;
				}
			}
			if (num == 0U)
			{
				throw new ArgumentException(SR.Get("ApplicationGestureArrayLengthIsZero"), "applicationGestures");
			}
			bool flag = false;
			List<ApplicationGesture> list = new List<ApplicationGesture>();
			foreach (ApplicationGesture applicationGesture2 in applicationGestures)
			{
				if (!ApplicationGestureHelper.IsDefined(applicationGesture2))
				{
					throw new ArgumentException(SR.Get("ApplicationGestureIsInvalid"), "applicationGestures");
				}
				if (applicationGesture2 == ApplicationGesture.AllGestures)
				{
					flag = true;
				}
				if (list.Contains(applicationGesture2))
				{
					throw new ArgumentException(SR.Get("DuplicateApplicationGestureFound"), "applicationGestures");
				}
				list.Add(applicationGesture2);
			}
			if (flag && list.Count != 1)
			{
				throw new ArgumentException(SR.Get("AllGesturesMustExistAlone"), "applicationGestures");
			}
			return list.ToArray();
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0015D37C File Offset: 0x0015C77C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			this._hContext.Dispose();
			this._disposed = true;
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0015D3A4 File Offset: 0x0015C7A4
		[SecurityCritical]
		private static bool LoadRecognizerDll()
		{
			string text = null;
			PermissionSet permissionSet = new PermissionSet(null);
			permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, AccessControlActions.View, "HKEY_LOCAL_MACHINE\\SOFTWARE\\MICROSOFT\\TPG\\SYSTEM RECOGNIZERS\\{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}"));
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\MICROSOFT\\TPG\\SYSTEM RECOGNIZERS\\{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}");
				if (registryKey != null)
				{
					try
					{
						text = (registryKey.GetValue("RECOGNIZER DLL") as string);
						if (text == null)
						{
							return false;
						}
						goto IL_73;
					}
					finally
					{
						registryKey.Close();
					}
				}
				return false;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			IL_73:
			if (text != null)
			{
				IntPtr intPtr = UnsafeNativeMethods.LoadLibrary(text);
				NativeRecognizer.s_GetAlternateListExists = false;
				if (intPtr != IntPtr.Zero)
				{
					NativeRecognizer.s_GetAlternateListExists = (UnsafeNativeMethods.GetProcAddressNoThrow(new HandleRef(null, intPtr), "GetAlternateList") != IntPtr.Zero);
				}
				return intPtr != IntPtr.Zero;
			}
			return false;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0015D4B0 File Offset: 0x0015C8B0
		[SecurityCritical]
		private int SetEnabledGestures(ContextSafeHandle recContext, ApplicationGesture[] enabledGestures)
		{
			uint num = (uint)enabledGestures.Length;
			CHARACTER_RANGE[] array = new CHARACTER_RANGE[num];
			if (num == 1U && enabledGestures[0] == ApplicationGesture.AllGestures)
			{
				array[0].cChars = 256;
				array[0].wcLow = 61440;
			}
			else
			{
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					array[num2].cChars = 1;
					array[num2].wcLow = (ushort)enabledGestures[num2];
					num2++;
				}
			}
			return UnsafeNativeMethods.SetEnabledUnicodeRanges(recContext, num, array);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0015D52C File Offset: 0x0015C92C
		[SecurityCritical]
		private int AddStrokes(ContextSafeHandle recContext, StrokeCollection strokes)
		{
			foreach (Stroke stroke in strokes)
			{
				PACKET_DESCRIPTION pd = default(PACKET_DESCRIPTION);
				IntPtr zero = IntPtr.Zero;
				try
				{
					int cbPackets;
					NativeMethods.XFORM xForm;
					this.GetPacketData(stroke, out pd, out cbPackets, out zero, out xForm);
					if (zero == IntPtr.Zero)
					{
						return -2147483640;
					}
					int num = UnsafeNativeMethods.AddStroke(recContext, ref pd, (uint)cbPackets, zero, xForm);
					if (HRESULT.Failed(num))
					{
						return num;
					}
				}
				finally
				{
					this.ReleaseResourcesinPacketDescription(pd, zero);
				}
			}
			return UnsafeNativeMethods.EndInkInput(recContext);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0015D5F4 File Offset: 0x0015C9F4
		[SecurityCritical]
		private unsafe void GetPacketData(Stroke stroke, out PACKET_DESCRIPTION packetDescription, out int countOfBytes, out IntPtr packets, out NativeMethods.XFORM xForm)
		{
			countOfBytes = 0;
			packets = IntPtr.Zero;
			packetDescription = default(PACKET_DESCRIPTION);
			Matrix identity = Matrix.Identity;
			xForm = new NativeMethods.XFORM((float)identity.M11, (float)identity.M12, (float)identity.M21, (float)identity.M22, (float)identity.OffsetX, (float)identity.OffsetY);
			StylusPointCollection stylusPointCollection = stroke.StylusPoints;
			if (stylusPointCollection.Count == 0)
			{
				return;
			}
			if (stylusPointCollection.Description.PropertyCount > StylusPointDescription.RequiredCountOfProperties)
			{
				StylusPointDescription subsetToReformatTo = new StylusPointDescription(new StylusPointPropertyInfo[]
				{
					new StylusPointPropertyInfo(StylusPointProperties.X),
					new StylusPointPropertyInfo(StylusPointProperties.Y),
					stylusPointCollection.Description.GetPropertyInfo(StylusPointProperties.NormalPressure)
				});
				stylusPointCollection = stylusPointCollection.Reformat(subsetToReformatTo);
			}
			if (stylusPointCollection.Count > 10000)
			{
				stylusPointCollection = stylusPointCollection.Clone(10000);
			}
			Guid[] array = new Guid[]
			{
				StylusPointPropertyIds.X,
				StylusPointPropertyIds.Y,
				StylusPointPropertyIds.NormalPressure
			};
			packetDescription.cbPacketSize = (uint)(array.Length * Marshal.SizeOf(typeof(int)));
			packetDescription.cPacketProperties = (uint)array.Length;
			StylusPointPropertyInfo[] array2 = new StylusPointPropertyInfo[StylusPointDescription.RequiredCountOfProperties];
			array2[StylusPointDescription.RequiredXIndex] = StylusPointPropertyInfoDefaults.X;
			array2[StylusPointDescription.RequiredYIndex] = StylusPointPropertyInfoDefaults.Y;
			array2[StylusPointDescription.RequiredPressureIndex] = stylusPointCollection.Description.GetPropertyInfo(StylusPointProperties.NormalPressure);
			PACKET_PROPERTY[] array3 = new PACKET_PROPERTY[packetDescription.cPacketProperties];
			int num = 0;
			while ((long)num < (long)((ulong)packetDescription.cPacketProperties))
			{
				array3[num].guid = array[num];
				StylusPointPropertyInfo stylusPointPropertyInfo = array2[num];
				array3[num].PropertyMetrics = new PROPERTY_METRICS
				{
					nLogicalMin = stylusPointPropertyInfo.Minimum,
					nLogicalMax = stylusPointPropertyInfo.Maximum,
					Units = (int)stylusPointPropertyInfo.Unit,
					fResolution = stylusPointPropertyInfo.Resolution
				};
				num++;
			}
			int cb = (int)((long)Marshal.SizeOf(typeof(PACKET_PROPERTY)) * (long)((ulong)packetDescription.cPacketProperties));
			packetDescription.pPacketProperties = Marshal.AllocCoTaskMem(cb);
			PACKET_PROPERTY* ptr = (PACKET_PROPERTY*)packetDescription.pPacketProperties.ToPointer();
			PACKET_PROPERTY* ptr2 = ptr;
			num = 0;
			while ((long)num < (long)((ulong)packetDescription.cPacketProperties))
			{
				Marshal.StructureToPtr(array3[num], new IntPtr((void*)ptr2), false);
				ptr2++;
				num++;
			}
			int[] array4 = stylusPointCollection.ToHiMetricArray();
			int num2 = array4.Length;
			if (num2 != 0)
			{
				countOfBytes = num2 * Marshal.SizeOf(typeof(int));
				packets = Marshal.AllocCoTaskMem(countOfBytes);
				Marshal.Copy(array4, 0, packets, num2);
			}
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0015D88C File Offset: 0x0015CC8C
		[SecurityCritical]
		private unsafe void ReleaseResourcesinPacketDescription(PACKET_DESCRIPTION pd, IntPtr packets)
		{
			if (pd.pPacketProperties != IntPtr.Zero)
			{
				PACKET_PROPERTY* ptr = (PACKET_PROPERTY*)pd.pPacketProperties.ToPointer();
				PACKET_PROPERTY* ptr2 = ptr;
				int num = 0;
				while ((long)num < (long)((ulong)pd.cPacketProperties))
				{
					Marshal.DestroyStructure(new IntPtr((void*)ptr2), typeof(PACKET_PROPERTY));
					ptr2++;
					num++;
				}
				Marshal.FreeCoTaskMem(pd.pPacketProperties);
				pd.pPacketProperties = IntPtr.Zero;
			}
			if (pd.pguidButtons != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(pd.pguidButtons);
				pd.pguidButtons = IntPtr.Zero;
			}
			if (packets != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(packets);
				packets = IntPtr.Zero;
			}
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0015D948 File Offset: 0x0015CD48
		[SecurityCritical]
		private GestureRecognitionResult[] InvokeGetAlternateList()
		{
			GestureRecognitionResult[] result = new GestureRecognitionResult[0];
			RECO_RANGE reco_RANGE;
			reco_RANGE.iwcBegin = 0U;
			reco_RANGE.cCount = 1U;
			uint num = 10U;
			IntPtr[] array = new IntPtr[10];
			try
			{
				int alternateList = UnsafeNativeMethods.GetAlternateList(this._hContext, ref reco_RANGE, ref num, array, ALT_BREAKS.ALT_BREAKS_SAME);
				if (HRESULT.Succeeded(alternateList) && num != 0U)
				{
					List<GestureRecognitionResult> list = new List<GestureRecognitionResult>();
					int num2 = 0;
					while ((long)num2 < (long)((ulong)num))
					{
						uint num3 = 1U;
						StringBuilder stringBuilder = new StringBuilder(1);
						RecognitionConfidence confidence;
						if (!HRESULT.Failed(UnsafeNativeMethods.GetString(array[num2], out reco_RANGE, ref num3, stringBuilder)) && !HRESULT.Failed(UnsafeNativeMethods.GetConfidenceLevel(array[num2], out reco_RANGE, out confidence)))
						{
							ApplicationGesture applicationGesture = (ApplicationGesture)stringBuilder[0];
							if (ApplicationGestureHelper.IsDefined(applicationGesture))
							{
								list.Add(new GestureRecognitionResult(confidence, applicationGesture));
							}
						}
						num2++;
					}
					result = list.ToArray();
				}
			}
			finally
			{
				int num4 = 0;
				while ((long)num4 < (long)((ulong)num))
				{
					if (array[num4] != IntPtr.Zero)
					{
						UnsafeNativeMethods.DestroyAlternate(array[num4]);
						array[num4] = IntPtr.Zero;
					}
					num4++;
				}
			}
			return result;
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0015DA64 File Offset: 0x0015CE64
		[SecurityCritical]
		private unsafe GestureRecognitionResult[] InvokeGetLatticePtr()
		{
			GestureRecognitionResult[] result = new GestureRecognitionResult[0];
			IntPtr zero = IntPtr.Zero;
			if (HRESULT.Succeeded(UnsafeNativeMethods.GetLatticePtr(this._hContext, ref zero)))
			{
				RECO_LATTICE* ptr = (RECO_LATTICE*)((void*)zero);
				uint ulBestResultColumnCount = ptr->ulBestResultColumnCount;
				if (ulBestResultColumnCount > 0U && ptr->pLatticeColumns != IntPtr.Zero)
				{
					List<GestureRecognitionResult> list = new List<GestureRecognitionResult>();
					RECO_LATTICE_COLUMN* ptr2 = (RECO_LATTICE_COLUMN*)((void*)ptr->pLatticeColumns);
					ulong* ptr3 = (ulong*)((void*)ptr->pulBestResultColumns);
					for (uint num = 0U; num < ulBestResultColumnCount; num += 1U)
					{
						ulong num2 = ptr3[(ulong)num * 8UL / 8UL];
						RECO_LATTICE_COLUMN reco_LATTICE_COLUMN = ptr2[num2 * (ulong)((long)sizeof(RECO_LATTICE_COLUMN)) / (ulong)sizeof(RECO_LATTICE_COLUMN)];
						int num3 = 0;
						while ((long)num3 < (long)((ulong)reco_LATTICE_COLUMN.cLatticeElements))
						{
							RECO_LATTICE_ELEMENT reco_LATTICE_ELEMENT = *(RECO_LATTICE_ELEMENT*)((byte*)((void*)reco_LATTICE_COLUMN.pLatticeElements) + (IntPtr)num3 * (IntPtr)sizeof(RECO_LATTICE_ELEMENT));
							if (reco_LATTICE_ELEMENT.type == 1)
							{
								RecognitionConfidence confidence = RecognitionConfidence.Poor;
								RECO_LATTICE_PROPERTIES epProp = reco_LATTICE_ELEMENT.epProp;
								uint cProperties = epProp.cProperties;
								RECO_LATTICE_PROPERTY** ptr4 = (RECO_LATTICE_PROPERTY**)((void*)epProp.apProps);
								int num4 = 0;
								while ((long)num4 < (long)((ulong)cProperties))
								{
									RECO_LATTICE_PROPERTY* ptr5 = *(IntPtr*)(ptr4 + (IntPtr)num4 * (IntPtr)sizeof(RECO_LATTICE_PROPERTY*) / (IntPtr)sizeof(RECO_LATTICE_PROPERTY*));
									if (ptr5->guidProperty == NativeRecognizer.GUID_CONFIDENCELEVEL)
									{
										RecognitionConfidence recognitionConfidence = (RecognitionConfidence)(*(uint*)((void*)ptr5->pPropertyValue));
										if (recognitionConfidence >= RecognitionConfidence.Strong && recognitionConfidence <= RecognitionConfidence.Poor)
										{
											confidence = recognitionConfidence;
											break;
										}
										break;
									}
									else
									{
										num4++;
									}
								}
								ApplicationGesture applicationGesture = (ApplicationGesture)((ushort)((int)reco_LATTICE_ELEMENT.pData));
								if (ApplicationGestureHelper.IsDefined(applicationGesture))
								{
									list.Add(new GestureRecognitionResult(confidence, applicationGesture));
								}
							}
							num3++;
						}
					}
					result = list.ToArray();
				}
			}
			return result;
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0015DC04 File Offset: 0x0015D004
		private static RecognizerSafeHandle RecognizerHandleSingleton
		{
			[SecurityCritical]
			get
			{
				if (NativeRecognizer.s_isSupported && NativeRecognizer.s_hRec == null)
				{
					object syncRoot = NativeRecognizer._syncRoot;
					lock (syncRoot)
					{
						if (NativeRecognizer.s_isSupported && NativeRecognizer.s_hRec == null && HRESULT.Failed(UnsafeNativeMethods.CreateRecognizer(ref NativeRecognizer.s_Gesture, out NativeRecognizer.s_hRec)))
						{
							NativeRecognizer.s_hRec = null;
						}
					}
				}
				return NativeRecognizer.s_hRec;
			}
		}

		// Token: 0x0400263A RID: 9786
		private const string GestureRecognizerPath = "SOFTWARE\\MICROSOFT\\TPG\\SYSTEM RECOGNIZERS\\{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}";

		// Token: 0x0400263B RID: 9787
		private const string GestureRecognizerFullPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\MICROSOFT\\TPG\\SYSTEM RECOGNIZERS\\{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}";

		// Token: 0x0400263C RID: 9788
		private const string GestureRecognizerValueName = "RECOGNIZER DLL";

		// Token: 0x0400263D RID: 9789
		private const string GestureRecognizerGuid = "{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}";

		// Token: 0x0400263E RID: 9790
		private const ushort MAX_GESTURE_COUNT = 256;

		// Token: 0x0400263F RID: 9791
		private const ushort GESTURE_NULL = 61440;

		// Token: 0x04002640 RID: 9792
		private const ushort IRAS_DefaultCount = 10;

		// Token: 0x04002641 RID: 9793
		private const ushort MaxStylusPoints = 10000;

		// Token: 0x04002642 RID: 9794
		private static readonly Guid GUID_CONFIDENCELEVEL = new Guid("{7DFE11A7-FB5D-4958-8765-154ADF0D833F}");

		// Token: 0x04002643 RID: 9795
		private bool _disposed;

		// Token: 0x04002644 RID: 9796
		[SecurityCritical]
		private ContextSafeHandle _hContext;

		// Token: 0x04002645 RID: 9797
		private static object _syncRoot = new object();

		// Token: 0x04002646 RID: 9798
		[SecurityCritical]
		private static RecognizerSafeHandle s_hRec;

		// Token: 0x04002647 RID: 9799
		private static Guid s_Gesture = new Guid("{BED9A940-7D48-48E3-9A68-F4887A5A1B2E}");

		// Token: 0x04002648 RID: 9800
		private static readonly bool s_isSupported = NativeRecognizer.LoadRecognizerDll();

		// Token: 0x04002649 RID: 9801
		private static bool s_GetAlternateListExists;
	}
}
