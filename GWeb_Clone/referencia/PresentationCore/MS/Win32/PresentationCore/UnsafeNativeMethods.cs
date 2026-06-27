using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Composition;
using System.Windows.Media.Imaging;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace MS.Win32.PresentationCore
{
	// Token: 0x02000649 RID: 1609
	internal static class UnsafeNativeMethods
	{
		// Token: 0x02000961 RID: 2401
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class MilCoreApi
		{
			// Token: 0x06005991 RID: 22929
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilCompositionEngine_EnterCompositionEngineLock")]
			internal static extern void EnterCompositionEngineLock();

			// Token: 0x06005992 RID: 22930
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilCompositionEngine_ExitCompositionEngineLock")]
			internal static extern void ExitCompositionEngineLock();

			// Token: 0x06005993 RID: 22931
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilCompositionEngine_EnterMediaSystemLock")]
			internal static extern void EnterMediaSystemLock();

			// Token: 0x06005994 RID: 22932
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilCompositionEngine_ExitMediaSystemLock")]
			internal static extern void ExitMediaSystemLock();

			// Token: 0x06005995 RID: 22933
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilVersionCheck(uint uiCallerMilSdkVersion);

			// Token: 0x06005996 RID: 22934
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern bool WgxConnection_ShouldForceSoftwareForGraphicsStreamClient();

			// Token: 0x06005997 RID: 22935
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int WgxConnection_Create(bool requestSynchronousTransport, out IntPtr ppConnection);

			// Token: 0x06005998 RID: 22936
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int WgxConnection_Disconnect(IntPtr pTranspManager);

			// Token: 0x06005999 RID: 22937
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MILCreateStreamFromStreamDescriptor(ref StreamDescriptor pSD, out IntPtr ppStream);

			// Token: 0x0600599A RID: 22938
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern void MilUtility_GetTileBrushMapping(D3DMATRIX* transform, D3DMATRIX* relativeTransform, Stretch stretch, AlignmentX alignmentX, AlignmentY alignmentY, BrushMappingMode viewPortUnits, BrushMappingMode viewBoxUnits, Rect* shapeFillBounds, Rect* contentBounds, ref Rect viewport, ref Rect viewbox, out D3DMATRIX contentToShape, out int brushIsEmpty);

			// Token: 0x0600599B RID: 22939
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilUtility_PathGeometryBounds(MIL_PEN_DATA* pPenData, double* pDashArray, MilMatrix3x2D* pWorldMatrix, FillRule fillRule, byte* pPathData, uint nSize, MilMatrix3x2D* pGeometryMatrix, double rTolerance, bool fRelative, bool fSkipHollows, MilRectD* pBounds);

			// Token: 0x0600599C RID: 22940
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilUtility_PathGeometryCombine(MilMatrix3x2D* pMatrix, MilMatrix3x2D* pMatrix1, FillRule fillRule1, byte* pPathData1, uint nSize1, MilMatrix3x2D* pMatrix2, FillRule fillRule2, byte* pPathData2, uint nSize2, double rTolerance, bool fRelative, Delegate addFigureCallback, GeometryCombineMode combineMode, out FillRule resultFillRule);

			// Token: 0x0600599D RID: 22941
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilUtility_PathGeometryWiden(MIL_PEN_DATA* pPenData, double* pDashArray, MilMatrix3x2D* pMatrix, FillRule fillRule, byte* pPathData, uint nSize, double rTolerance, bool fRelative, Delegate addFigureCallback, out FillRule widenedFillRule);

			// Token: 0x0600599E RID: 22942
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilUtility_PathGeometryOutline(MilMatrix3x2D* pMatrix, FillRule fillRule, byte* pPathData, uint nSize, double rTolerance, bool fRelative, Delegate addFigureCallback, out FillRule outlinedFillRule);

			// Token: 0x0600599F RID: 22943
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilUtility_PathGeometryFlatten(MilMatrix3x2D* pMatrix, FillRule fillRule, byte* pPathData, uint nSize, double rTolerance, bool fRelative, Delegate addFigureCallback, out FillRule resultFillRule);

			// Token: 0x060059A0 RID: 22944
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilGlyphCache_SetCreateGlyphBitmapsCallback(MulticastDelegate del);

			// Token: 0x060059A1 RID: 22945
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilGlyphCache_BeginCommandAtRenderTime(IntPtr pMilSlaveGlyphCacheTarget, byte* pbData, uint cbSize, uint cbExtra);

			// Token: 0x060059A2 RID: 22946
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilGlyphCache_AppendCommandDataAtRenderTime(IntPtr pMilSlaveGlyphCacheTarget, byte* pbData, uint cbSize);

			// Token: 0x060059A3 RID: 22947
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilGlyphCache_EndCommandAtRenderTime(IntPtr pMilSlaveGlyphCacheTarget);

			// Token: 0x060059A4 RID: 22948
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilGlyphRun_SetGeometryAtRenderTime(IntPtr pMilGlyphRunTarget, byte* pCmd, uint cbCmd);

			// Token: 0x060059A5 RID: 22949
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilGlyphRun_GetGlyphOutline(IntPtr pFontFace, ushort glyphIndex, bool sideways, double renderingEmSize, out byte* pPathGeometryData, out uint pSize, out FillRule pFillRule);

			// Token: 0x060059A6 RID: 22950
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilGlyphRun_ReleasePathGeometryData(byte* pPathGeometryData);

			// Token: 0x060059A7 RID: 22951
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilCreateReversePInvokeWrapper(IntPtr pFcn, out IntPtr reversePInvokeWrapper);

			// Token: 0x060059A8 RID: 22952
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern void MilReleasePInvokePtrBlocking(IntPtr reversePInvokeWrapper);

			// Token: 0x060059A9 RID: 22953
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern void RenderOptions_ForceSoftwareRenderingModeForProcess(bool fForce);

			// Token: 0x060059AA RID: 22954
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern bool RenderOptions_IsSoftwareRenderingForcedForProcess();

			// Token: 0x060059AB RID: 22955
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilResource_CreateCWICWrapperBitmap")]
			internal static extern int CreateCWICWrapperBitmap(BitmapSourceSafeMILHandle pIWICBitmapSource, out BitmapSourceSafeMILHandle pCWICWrapperBitmap);
		}

		// Token: 0x02000962 RID: 2402
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICComponentInfo
		{
			// Token: 0x060059AC RID: 22956
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentInfo_GetCLSID_Proxy")]
			internal static extern int GetCLSID(SafeMILHandle THIS_PTR, out Guid pclsid);

			// Token: 0x060059AD RID: 22957
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentInfo_GetAuthor_Proxy")]
			internal static extern int GetAuthor(SafeMILHandle THIS_PTR, uint cchAuthor, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzAuthor, out uint pcchActual);

			// Token: 0x060059AE RID: 22958
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentInfo_GetVersion_Proxy")]
			internal static extern int GetVersion(SafeMILHandle THIS_PTR, uint cchVersion, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzVersion, out uint pcchActual);

			// Token: 0x060059AF RID: 22959
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentInfo_GetSpecVersion_Proxy")]
			internal static extern int GetSpecVersion(SafeMILHandle THIS_PTR, uint cchSpecVersion, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzSpecVersion, out uint pcchActual);

			// Token: 0x060059B0 RID: 22960
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentInfo_GetFriendlyName_Proxy")]
			internal static extern int GetFriendlyName(SafeMILHandle THIS_PTR, uint cchFriendlyName, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzFriendlyName, out uint pcchActual);
		}

		// Token: 0x02000963 RID: 2403
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICBitmapCodecInfo
		{
			// Token: 0x060059B1 RID: 22961
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_GetContainerFormat_Proxy")]
			internal static extern int GetContainerFormat(SafeMILHandle THIS_PTR, out Guid pguidContainerFormat);

			// Token: 0x060059B2 RID: 22962
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_GetDeviceManufacturer_Proxy")]
			internal static extern int GetDeviceManufacturer(SafeMILHandle THIS_PTR, uint cchDeviceManufacturer, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzDeviceManufacturer, out uint pcchActual);

			// Token: 0x060059B3 RID: 22963
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_GetDeviceModels_Proxy")]
			internal static extern int GetDeviceModels(SafeMILHandle THIS_PTR, uint cchDeviceModels, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzDeviceModels, out uint pcchActual);

			// Token: 0x060059B4 RID: 22964
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_GetMimeTypes_Proxy")]
			internal static extern int GetMimeTypes(SafeMILHandle THIS_PTR, uint cchMimeTypes, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzMimeTypes, out uint pcchActual);

			// Token: 0x060059B5 RID: 22965
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_GetFileExtensions_Proxy")]
			internal static extern int GetFileExtensions(SafeMILHandle THIS_PTR, uint cchFileExtensions, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzFileExtensions, out uint pcchActual);

			// Token: 0x060059B6 RID: 22966
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_DoesSupportAnimation_Proxy")]
			internal static extern int DoesSupportAnimation(SafeMILHandle THIS_PTR, out bool pfSupportAnimation);

			// Token: 0x060059B7 RID: 22967
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_DoesSupportLossless_Proxy")]
			internal static extern int DoesSupportLossless(SafeMILHandle THIS_PTR, out bool pfSupportLossless);

			// Token: 0x060059B8 RID: 22968
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapCodecInfo_DoesSupportMultiframe_Proxy")]
			internal static extern int DoesSupportMultiframe(SafeMILHandle THIS_PTR, out bool pfSupportMultiframe);
		}

		// Token: 0x02000964 RID: 2404
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICMetadataQueryReader
		{
			// Token: 0x060059B9 RID: 22969
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryReader_GetContainerFormat_Proxy")]
			internal static extern int GetContainerFormat(SafeMILHandle THIS_PTR, out Guid pguidContainerFormat);

			// Token: 0x060059BA RID: 22970
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryReader_GetLocation_Proxy")]
			internal static extern int GetLocation(SafeMILHandle THIS_PTR, uint cchLocation, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzNamespace, out uint pcchActual);

			// Token: 0x060059BB RID: 22971
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryReader_GetMetadataByName_Proxy")]
			internal static extern int GetMetadataByName(SafeMILHandle THIS_PTR, [MarshalAs(UnmanagedType.LPWStr)] [Out] string wzName, ref PROPVARIANT propValue);

			// Token: 0x060059BC RID: 22972
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryReader_GetMetadataByName_Proxy")]
			internal static extern int ContainsMetadataByName(SafeMILHandle THIS_PTR, [MarshalAs(UnmanagedType.LPWStr)] [Out] string wzName, IntPtr propVar);

			// Token: 0x060059BD RID: 22973
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryReader_GetEnumerator_Proxy")]
			internal static extern int GetEnumerator(SafeMILHandle THIS_PTR, out SafeMILHandle enumString);
		}

		// Token: 0x02000965 RID: 2405
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICMetadataQueryWriter
		{
			// Token: 0x060059BE RID: 22974
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryWriter_SetMetadataByName_Proxy")]
			internal static extern int SetMetadataByName(SafeMILHandle THIS_PTR, [MarshalAs(UnmanagedType.LPWStr)] [Out] string wzName, ref PROPVARIANT propValue);

			// Token: 0x060059BF RID: 22975
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataQueryWriter_RemoveMetadataByName_Proxy")]
			internal static extern int RemoveMetadataByName(SafeMILHandle THIS_PTR, [MarshalAs(UnmanagedType.LPWStr)] [Out] string wzName);
		}

		// Token: 0x02000966 RID: 2406
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICFastMetadataEncoder
		{
			// Token: 0x060059C0 RID: 22976
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICFastMetadataEncoder_Commit_Proxy")]
			internal static extern int Commit(SafeMILHandle THIS_PTR);

			// Token: 0x060059C1 RID: 22977
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICFastMetadataEncoder_GetMetadataQueryWriter_Proxy")]
			internal static extern int GetMetadataQueryWriter(SafeMILHandle THIS_PTR, out SafeMILHandle ppIQueryWriter);
		}

		// Token: 0x02000967 RID: 2407
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class EnumString
		{
			// Token: 0x060059C2 RID: 22978
			[DllImport("WindowsCodecs.dll", EntryPoint = "IEnumString_Next_WIC_Proxy")]
			internal static extern int Next(SafeMILHandle THIS_PTR, int celt, ref IntPtr rgElt, ref int pceltFetched);

			// Token: 0x060059C3 RID: 22979
			[DllImport("WindowsCodecs.dll", EntryPoint = "IEnumString_Reset_WIC_Proxy")]
			internal static extern int Reset(SafeMILHandle THIS_PTR);
		}

		// Token: 0x02000968 RID: 2408
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class IPropertyBag2
		{
			// Token: 0x060059C4 RID: 22980
			[DllImport("WindowsCodecs.dll", EntryPoint = "IPropertyBag2_Write_Proxy")]
			internal static extern int Write(SafeMILHandle THIS_PTR, uint cProperties, ref PROPBAG2 propBag, ref PROPVARIANT propValue);
		}

		// Token: 0x02000969 RID: 2409
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICBitmapSource
		{
			// Token: 0x060059C5 RID: 22981
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapSource_GetSize_Proxy")]
			internal static extern int GetSize(SafeMILHandle THIS_PTR, out uint puiWidth, out uint puiHeight);

			// Token: 0x060059C6 RID: 22982
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapSource_GetPixelFormat_Proxy")]
			internal static extern int GetPixelFormat(SafeMILHandle THIS_PTR, out Guid pPixelFormatEnum);

			// Token: 0x060059C7 RID: 22983
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapSource_GetResolution_Proxy")]
			internal static extern int GetResolution(SafeMILHandle THIS_PTR, out double pDpiX, out double pDpiY);

			// Token: 0x060059C8 RID: 22984
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapSource_CopyPalette_Proxy")]
			internal static extern int CopyPalette(SafeMILHandle THIS_PTR, SafeMILHandle pIPalette);

			// Token: 0x060059C9 RID: 22985
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapSource_CopyPixels_Proxy")]
			internal static extern int CopyPixels(SafeMILHandle THIS_PTR, ref Int32Rect prc, uint cbStride, uint cbBufferSize, IntPtr pvPixels);
		}

		// Token: 0x0200096A RID: 2410
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICBitmapDecoder
		{
			// Token: 0x060059CA RID: 22986
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetDecoderInfo_Proxy")]
			internal static extern int GetDecoderInfo(SafeMILHandle THIS_PTR, out SafeMILHandle ppIDecoderInfo);

			// Token: 0x060059CB RID: 22987
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_CopyPalette_Proxy")]
			internal static extern int CopyPalette(SafeMILHandle THIS_PTR, SafeMILHandle pIPalette);

			// Token: 0x060059CC RID: 22988
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetPreview_Proxy")]
			internal static extern int GetPreview(SafeMILHandle THIS_PTR, out IntPtr ppIBitmapSource);

			// Token: 0x060059CD RID: 22989
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetColorContexts_Proxy")]
			internal static extern int GetColorContexts(SafeMILHandle THIS_PTR, uint count, IntPtr[] ppIColorContext, out uint pActualCount);

			// Token: 0x060059CE RID: 22990
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetThumbnail_Proxy")]
			internal static extern int GetThumbnail(SafeMILHandle THIS_PTR, out IntPtr ppIThumbnail);

			// Token: 0x060059CF RID: 22991
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetMetadataQueryReader_Proxy")]
			internal static extern int GetMetadataQueryReader(SafeMILHandle THIS_PTR, out IntPtr ppIQueryReader);

			// Token: 0x060059D0 RID: 22992
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetFrameCount_Proxy")]
			internal static extern int GetFrameCount(SafeMILHandle THIS_PTR, out uint pFrameCount);

			// Token: 0x060059D1 RID: 22993
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapDecoder_GetFrame_Proxy")]
			internal static extern int GetFrame(SafeMILHandle THIS_PTR, uint index, out IntPtr ppIFrameDecode);
		}

		// Token: 0x0200096B RID: 2411
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICBitmapFrameDecode
		{
			// Token: 0x060059D2 RID: 22994
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameDecode_GetThumbnail_Proxy")]
			internal static extern int GetThumbnail(SafeMILHandle THIS_PTR, out IntPtr ppIThumbnail);

			// Token: 0x060059D3 RID: 22995
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameDecode_GetMetadataQueryReader_Proxy")]
			internal static extern int GetMetadataQueryReader(SafeMILHandle THIS_PTR, out IntPtr ppIQueryReader);

			// Token: 0x060059D4 RID: 22996
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameDecode_GetColorContexts_Proxy")]
			internal static extern int GetColorContexts(SafeMILHandle THIS_PTR, uint count, IntPtr[] ppIColorContext, out uint pActualCount);
		}

		// Token: 0x0200096C RID: 2412
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class MILUnknown
		{
			// Token: 0x060059D5 RID: 22997
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILAddRef")]
			internal static extern uint AddRef(SafeMILHandle pIUnkown);

			// Token: 0x060059D6 RID: 22998
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILAddRef")]
			internal static extern uint AddRef(SafeReversePInvokeWrapper pIUnknown);

			// Token: 0x060059D7 RID: 22999
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILRelease")]
			internal static extern int Release(IntPtr pIUnkown);

			// Token: 0x060059D8 RID: 23000 RVA: 0x0016B0BC File Offset: 0x0016A4BC
			internal static void ReleaseInterface(ref IntPtr ptr)
			{
				if (ptr != IntPtr.Zero)
				{
					UnsafeNativeMethods.MILUnknown.Release(ptr);
					ptr = IntPtr.Zero;
				}
			}

			// Token: 0x060059D9 RID: 23001
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILQueryInterface")]
			internal static extern int QueryInterface(IntPtr pIUnknown, ref Guid guid, out IntPtr ppvObject);

			// Token: 0x060059DA RID: 23002
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILQueryInterface")]
			internal static extern int QueryInterface(SafeMILHandle pIUnknown, ref Guid guid, out IntPtr ppvObject);
		}

		// Token: 0x0200096D RID: 2413
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICStream
		{
			// Token: 0x060059DB RID: 23003
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICStream_InitializeFromIStream_Proxy")]
			internal static extern int InitializeFromIStream(IntPtr pIWICStream, IntPtr pIStream);

			// Token: 0x060059DC RID: 23004
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICStream_InitializeFromMemory_Proxy")]
			internal static extern int InitializeFromMemory(IntPtr pIWICStream, IntPtr pbBuffer, uint cbSize);
		}

		// Token: 0x0200096E RID: 2414
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WindowsCodecApi
		{
			// Token: 0x060059DD RID: 23005
			[DllImport("WindowsCodecs.dll", EntryPoint = "WICCreateBitmapFromSection")]
			internal static extern int CreateBitmapFromSection(uint width, uint height, ref Guid pixelFormatGuid, IntPtr hSection, uint stride, uint offset, out BitmapSourceSafeMILHandle ppIBitmap);
		}

		// Token: 0x0200096F RID: 2415
		internal static class WICBitmapFrameEncode
		{
			// Token: 0x060059DE RID: 23006
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle pIEncoderOptions);

			// Token: 0x060059DF RID: 23007
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_Commit_Proxy")]
			internal static extern int Commit(SafeMILHandle THIS_PTR);

			// Token: 0x060059E0 RID: 23008
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_SetSize_Proxy")]
			internal static extern int SetSize(SafeMILHandle THIS_PTR, int width, int height);

			// Token: 0x060059E1 RID: 23009
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_SetResolution_Proxy")]
			internal static extern int SetResolution(SafeMILHandle THIS_PTR, double dpiX, double dpiY);

			// Token: 0x060059E2 RID: 23010
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_WriteSource_Proxy")]
			internal static extern int WriteSource(SafeMILHandle THIS_PTR, SafeMILHandle pIBitmapSource, ref Int32Rect r);

			// Token: 0x060059E3 RID: 23011
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_SetThumbnail_Proxy")]
			internal static extern int SetThumbnail(SafeMILHandle THIS_PTR, SafeMILHandle pIThumbnail);

			// Token: 0x060059E4 RID: 23012
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_GetMetadataQueryWriter_Proxy")]
			internal static extern int GetMetadataQueryWriter(SafeMILHandle THIS_PTR, out SafeMILHandle ppIQueryWriter);

			// Token: 0x060059E5 RID: 23013
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFrameEncode_SetColorContexts_Proxy")]
			internal static extern int SetColorContexts(SafeMILHandle THIS_PTR, uint nIndex, IntPtr[] ppIColorContext);
		}

		// Token: 0x02000970 RID: 2416
		internal static class WICBitmapEncoder
		{
			// Token: 0x060059E6 RID: 23014
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, IntPtr pStream, WICBitmapEncodeCacheOption option);

			// Token: 0x060059E7 RID: 23015
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_GetEncoderInfo_Proxy")]
			internal static extern int GetEncoderInfo(SafeMILHandle THIS_PTR, out SafeMILHandle ppIEncoderInfo);

			// Token: 0x060059E8 RID: 23016
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_CreateNewFrame_Proxy")]
			internal static extern int CreateNewFrame(SafeMILHandle THIS_PTR, out SafeMILHandle ppIFramEncode, out SafeMILHandle ppIEncoderOptions);

			// Token: 0x060059E9 RID: 23017
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_SetThumbnail_Proxy")]
			internal static extern int SetThumbnail(SafeMILHandle THIS_PTR, SafeMILHandle pIThumbnail);

			// Token: 0x060059EA RID: 23018
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_SetPalette_Proxy")]
			internal static extern int SetPalette(SafeMILHandle THIS_PTR, SafeMILHandle pIPalette);

			// Token: 0x060059EB RID: 23019
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_GetMetadataQueryWriter_Proxy")]
			internal static extern int GetMetadataQueryWriter(SafeMILHandle THIS_PTR, out SafeMILHandle ppIQueryWriter);

			// Token: 0x060059EC RID: 23020
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapEncoder_Commit_Proxy")]
			internal static extern int Commit(SafeMILHandle THIS_PTR);
		}

		// Token: 0x02000971 RID: 2417
		internal static class WICPalette
		{
			// Token: 0x060059ED RID: 23021
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_InitializePredefined_Proxy")]
			internal static extern int InitializePredefined(SafeMILHandle THIS_PTR, WICPaletteType ePaletteType, bool fAddTransparentColor);

			// Token: 0x060059EE RID: 23022
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_InitializeCustom_Proxy")]
			internal static extern int InitializeCustom(SafeMILHandle THIS_PTR, IntPtr pColors, int colorCount);

			// Token: 0x060059EF RID: 23023
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_InitializeFromBitmap_Proxy")]
			internal static extern int InitializeFromBitmap(SafeMILHandle THIS_PTR, SafeMILHandle pISurface, int colorCount, bool fAddTransparentColor);

			// Token: 0x060059F0 RID: 23024
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_InitializeFromPalette_Proxy")]
			internal static extern int InitializeFromPalette(IntPtr THIS_PTR, SafeMILHandle pIWICPalette);

			// Token: 0x060059F1 RID: 23025
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_GetType_Proxy")]
			internal static extern int GetType(SafeMILHandle THIS_PTR, out WICPaletteType pePaletteType);

			// Token: 0x060059F2 RID: 23026
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_GetColorCount_Proxy")]
			internal static extern int GetColorCount(SafeMILHandle THIS_PTR, out int pColorCount);

			// Token: 0x060059F3 RID: 23027
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_GetColors_Proxy")]
			internal static extern int GetColors(SafeMILHandle THIS_PTR, int colorCount, IntPtr pColors, out int pcActualCount);

			// Token: 0x060059F4 RID: 23028
			[SecurityCritical]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPalette_HasAlpha_Proxy")]
			internal static extern int HasAlpha(SafeMILHandle THIS_PTR, out bool pfHasAlpha);
		}

		// Token: 0x02000972 RID: 2418
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICImagingFactory
		{
			// Token: 0x060059F5 RID: 23029
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateDecoderFromStream_Proxy")]
			internal static extern int CreateDecoderFromStream(IntPtr pICodecFactory, IntPtr pIStream, ref Guid guidVendor, uint metadataFlags, out IntPtr ppIDecode);

			// Token: 0x060059F6 RID: 23030
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateDecoderFromFileHandle_Proxy")]
			internal static extern int CreateDecoderFromFileHandle(IntPtr pICodecFactory, SafeFileHandle hFileHandle, ref Guid guidVendor, uint metadataFlags, out IntPtr ppIDecode);

			// Token: 0x060059F7 RID: 23031
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateComponentInfo_Proxy")]
			internal static extern int CreateComponentInfo(IntPtr pICodecFactory, ref Guid clsidComponent, out IntPtr ppIComponentInfo);

			// Token: 0x060059F8 RID: 23032
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreatePalette_Proxy")]
			internal static extern int CreatePalette(IntPtr pICodecFactory, out SafeMILHandle ppIPalette);

			// Token: 0x060059F9 RID: 23033
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateFormatConverter_Proxy")]
			internal static extern int CreateFormatConverter(IntPtr pICodecFactory, out BitmapSourceSafeMILHandle ppFormatConverter);

			// Token: 0x060059FA RID: 23034
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapScaler_Proxy")]
			internal static extern int CreateBitmapScaler(IntPtr pICodecFactory, out BitmapSourceSafeMILHandle ppBitmapScaler);

			// Token: 0x060059FB RID: 23035
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapClipper_Proxy")]
			internal static extern int CreateBitmapClipper(IntPtr pICodecFactory, out BitmapSourceSafeMILHandle ppBitmapClipper);

			// Token: 0x060059FC RID: 23036
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapFlipRotator_Proxy")]
			internal static extern int CreateBitmapFlipRotator(IntPtr pICodecFactory, out BitmapSourceSafeMILHandle ppBitmapFlipRotator);

			// Token: 0x060059FD RID: 23037
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateStream_Proxy")]
			internal static extern int CreateStream(IntPtr pICodecFactory, out IntPtr ppIStream);

			// Token: 0x060059FE RID: 23038
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateEncoder_Proxy")]
			internal static extern int CreateEncoder(IntPtr pICodecFactory, ref Guid guidContainerFormat, ref Guid guidVendor, out SafeMILHandle ppICodec);

			// Token: 0x060059FF RID: 23039
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapFromSource_Proxy")]
			internal static extern int CreateBitmapFromSource(IntPtr THIS_PTR, SafeMILHandle pIBitmapSource, WICBitmapCreateCacheOptions options, out BitmapSourceSafeMILHandle ppIBitmap);

			// Token: 0x06005A00 RID: 23040
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapFromMemory_Proxy")]
			internal static extern int CreateBitmapFromMemory(IntPtr THIS_PTR, uint width, uint height, ref Guid pixelFormatGuid, uint stride, uint cbBufferSize, IntPtr pvPixels, out BitmapSourceSafeMILHandle ppIBitmap);

			// Token: 0x06005A01 RID: 23041
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmap_Proxy")]
			internal static extern int CreateBitmap(IntPtr THIS_PTR, uint width, uint height, ref Guid pixelFormatGuid, WICBitmapCreateCacheOptions options, out BitmapSourceSafeMILHandle ppIBitmap);

			// Token: 0x06005A02 RID: 23042
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapFromHBITMAP_Proxy")]
			internal static extern int CreateBitmapFromHBITMAP(IntPtr THIS_PTR, IntPtr hBitmap, IntPtr hPalette, WICBitmapAlphaChannelOption options, out BitmapSourceSafeMILHandle ppIBitmap);

			// Token: 0x06005A03 RID: 23043
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateBitmapFromHICON_Proxy")]
			internal static extern int CreateBitmapFromHICON(IntPtr THIS_PTR, IntPtr hIcon, out BitmapSourceSafeMILHandle ppIBitmap);

			// Token: 0x06005A04 RID: 23044
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateFastMetadataEncoderFromDecoder_Proxy")]
			internal static extern int CreateFastMetadataEncoderFromDecoder(IntPtr THIS_PTR, SafeMILHandle pIDecoder, out SafeMILHandle ppIFME);

			// Token: 0x06005A05 RID: 23045
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateFastMetadataEncoderFromFrameDecode_Proxy")]
			internal static extern int CreateFastMetadataEncoderFromFrameDecode(IntPtr THIS_PTR, BitmapSourceSafeMILHandle pIFrameDecode, out SafeMILHandle ppIBitmap);

			// Token: 0x06005A06 RID: 23046
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateQueryWriter_Proxy")]
			internal static extern int CreateQueryWriter(IntPtr THIS_PTR, ref Guid metadataFormat, ref Guid guidVendor, out IntPtr queryWriter);

			// Token: 0x06005A07 RID: 23047
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICImagingFactory_CreateQueryWriterFromReader_Proxy")]
			internal static extern int CreateQueryWriterFromReader(IntPtr THIS_PTR, SafeMILHandle queryReader, ref Guid guidVendor, out IntPtr queryWriter);
		}

		// Token: 0x02000973 RID: 2419
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICComponentFactory
		{
			// Token: 0x06005A08 RID: 23048
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentFactory_CreateMetadataWriterFromReader_Proxy")]
			internal static extern int CreateMetadataWriterFromReader(IntPtr pICodecFactory, SafeMILHandle pIMetadataReader, ref Guid guidVendor, out IntPtr metadataWriter);

			// Token: 0x06005A09 RID: 23049
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICComponentFactory_CreateQueryWriterFromBlockWriter_Proxy")]
			internal static extern int CreateQueryWriterFromBlockWriter(IntPtr pICodecFactory, IntPtr pIBlockWriter, ref IntPtr ppIQueryWriter);
		}

		// Token: 0x02000974 RID: 2420
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICMetadataBlockReader
		{
			// Token: 0x06005A0A RID: 23050
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataBlockReader_GetCount_Proxy")]
			internal static extern int GetCount(IntPtr pIBlockReader, out uint count);

			// Token: 0x06005A0B RID: 23051
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICMetadataBlockReader_GetReaderByIndex_Proxy")]
			internal static extern int GetReaderByIndex(IntPtr pIBlockReader, uint index, out SafeMILHandle pIMetadataReader);
		}

		// Token: 0x02000975 RID: 2421
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICPixelFormatInfo
		{
			// Token: 0x06005A0C RID: 23052
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPixelFormatInfo_GetBitsPerPixel_Proxy")]
			internal static extern int GetBitsPerPixel(IntPtr pIPixelFormatInfo, out uint uiBitsPerPixel);

			// Token: 0x06005A0D RID: 23053
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPixelFormatInfo_GetChannelCount_Proxy")]
			internal static extern int GetChannelCount(IntPtr pIPixelFormatInfo, out uint uiChannelCount);

			// Token: 0x06005A0E RID: 23054
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICPixelFormatInfo_GetChannelMask_Proxy")]
			internal unsafe static extern int GetChannelMask(IntPtr pIPixelFormatInfo, uint uiChannelIndex, uint cbMaskBuffer, byte* pbMaskBuffer, out uint cbActual);
		}

		// Token: 0x02000976 RID: 2422
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICBitmapClipper
		{
			// Token: 0x06005A0F RID: 23055
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapClipper_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle source, ref Int32Rect prc);
		}

		// Token: 0x02000977 RID: 2423
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICBitmapFlipRotator
		{
			// Token: 0x06005A10 RID: 23056
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapFlipRotator_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle source, WICBitmapTransformOptions options);
		}

		// Token: 0x02000978 RID: 2424
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICBitmapScaler
		{
			// Token: 0x06005A11 RID: 23057
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapScaler_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle source, uint width, uint height, WICInterpolationMode mode);
		}

		// Token: 0x02000979 RID: 2425
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICFormatConverter
		{
			// Token: 0x06005A12 RID: 23058
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICFormatConverter_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle source, ref Guid dstFormat, DitherType dither, SafeMILHandle bitmapPalette, double alphaThreshold, WICPaletteType paletteTranslate);
		}

		// Token: 0x0200097A RID: 2426
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class IWICColorContext
		{
			// Token: 0x06005A13 RID: 23059
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICColorContext_InitializeFromMemory_Proxy")]
			internal static extern int InitializeFromMemory(SafeMILHandle THIS_PTR, byte[] pbBuffer, uint cbBufferSize);

			// Token: 0x06005A14 RID: 23060
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "IWICColorContext_GetProfileBytes_Proxy")]
			internal static extern int GetProfileBytes(SafeMILHandle THIS_PTR, uint cbBuffer, byte[] pbBuffer, out uint pcbActual);

			// Token: 0x06005A15 RID: 23061
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "IWICColorContext_GetType_Proxy")]
			internal static extern int GetType(SafeMILHandle THIS_PTR, out UnsafeNativeMethods.IWICColorContext.WICColorContextType pType);

			// Token: 0x06005A16 RID: 23062
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "IWICColorContext_GetExifColorSpace_Proxy")]
			internal static extern int GetExifColorSpace(SafeMILHandle THIS_PTR, out uint pValue);

			// Token: 0x02000A2E RID: 2606
			internal enum WICColorContextType : uint
			{
				// Token: 0x04002FBA RID: 12218
				WICColorContextUninitialized,
				// Token: 0x04002FBB RID: 12219
				WICColorContextProfile,
				// Token: 0x04002FBC RID: 12220
				WICColorContextExifColorSpace
			}
		}

		// Token: 0x0200097B RID: 2427
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICColorTransform
		{
			// Token: 0x06005A17 RID: 23063
			[DllImport("WindowsCodecsExt.dll", EntryPoint = "IWICColorTransform_Initialize_Proxy")]
			internal static extern int Initialize(SafeMILHandle THIS_PTR, SafeMILHandle source, SafeMILHandle pIContextSource, SafeMILHandle pIContextDest, ref Guid pixelFmtDest);
		}

		// Token: 0x0200097C RID: 2428
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICBitmap
		{
			// Token: 0x06005A18 RID: 23064
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmap_Lock_Proxy")]
			internal static extern int Lock(SafeMILHandle THIS_PTR, ref Int32Rect prcLock, LockFlags flags, out SafeMILHandle ppILock);

			// Token: 0x06005A19 RID: 23065
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmap_SetResolution_Proxy")]
			internal static extern int SetResolution(SafeMILHandle THIS_PTR, double dpiX, double dpiY);

			// Token: 0x06005A1A RID: 23066
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmap_SetPalette_Proxy")]
			internal static extern int SetPalette(SafeMILHandle THIS_PTR, SafeMILHandle pIPalette);
		}

		// Token: 0x0200097D RID: 2429
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class WICBitmapLock
		{
			// Token: 0x06005A1B RID: 23067
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapLock_GetStride_Proxy")]
			internal static extern int GetStride(SafeMILHandle pILock, ref uint pcbStride);

			// Token: 0x06005A1C RID: 23068
			[DllImport("WindowsCodecs.dll", EntryPoint = "IWICBitmapLock_GetDataPointer_STA_Proxy")]
			internal static extern int GetDataPointer(SafeMILHandle pILock, ref uint pcbBufferSize, ref IntPtr ppbData);
		}

		// Token: 0x0200097E RID: 2430
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class WICCodec
		{
			// Token: 0x06005A1D RID: 23069
			[DllImport("WindowsCodecs.dll", EntryPoint = "WICCreateImagingFactory_Proxy")]
			internal static extern int CreateImagingFactory(uint SDKVersion, out IntPtr ppICodecFactory);

			// Token: 0x06005A1E RID: 23070
			[DllImport("WindowsCodecs.dll")]
			internal static extern int WICConvertBitmapSource(ref Guid dstPixelFormatGuid, SafeMILHandle pISrc, out BitmapSourceSafeMILHandle ppIDst);

			// Token: 0x06005A1F RID: 23071
			[DllImport("WindowsCodecs.dll", EntryPoint = "WICSetEncoderFormat_Proxy")]
			internal static extern int WICSetEncoderFormat(SafeMILHandle pSourceIn, SafeMILHandle pIPalette, SafeMILHandle pIFrameEncode, out SafeMILHandle ppSourceOut);

			// Token: 0x06005A20 RID: 23072
			[DllImport("WindowsCodecs.dll")]
			internal static extern int WICMapGuidToShortName(ref Guid guid, uint cchName, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder wzName, ref uint pcchActual);

			// Token: 0x06005A21 RID: 23073
			[DllImport("WindowsCodecs.dll")]
			internal static extern int WICMapShortNameToGuid([MarshalAs(UnmanagedType.LPWStr)] [Out] string wzName, ref Guid guid);

			// Token: 0x06005A22 RID: 23074
			[DllImport("WindowsCodecsExt.dll", EntryPoint = "WICCreateColorTransform_Proxy")]
			internal static extern int CreateColorTransform(out BitmapSourceSafeMILHandle ppWICColorTransform);

			// Token: 0x06005A23 RID: 23075
			[DllImport("WindowsCodecs.dll", EntryPoint = "WICCreateColorContext_Proxy")]
			internal static extern int CreateColorContext(IntPtr pICodecFactory, out SafeMILHandle ppColorContext);

			// Token: 0x06005A24 RID: 23076
			[DllImport("ole32.dll")]
			internal static extern int CoInitialize(IntPtr reserved);

			// Token: 0x06005A25 RID: 23077
			[DllImport("ole32.dll")]
			internal static extern void CoUninitialize();

			// Token: 0x04002CAF RID: 11439
			internal const int WINCODEC_SDK_VERSION = 566;
		}

		// Token: 0x0200097F RID: 2431
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class Mscms
		{
			// Token: 0x06005A26 RID: 23078
			[DllImport("mscms.dll")]
			internal static extern ColorTransformHandle CreateMultiProfileTransform(IntPtr[] pahProfiles, uint nProfiles, uint[] padwIntent, uint nIntents, uint dwFlags, uint indexPreferredCMM);

			// Token: 0x06005A27 RID: 23079
			[DllImport("mscms.dll", SetLastError = true)]
			internal static extern bool DeleteColorTransform(IntPtr hColorTransform);

			// Token: 0x06005A28 RID: 23080
			[DllImport("mscms.dll")]
			internal static extern int TranslateColors(ColorTransformHandle hColorTransform, IntPtr paInputColors, uint nColors, uint ctInput, IntPtr paOutputColors, uint ctOutput);

			// Token: 0x06005A29 RID: 23081
			[DllImport("mscms.dll")]
			internal static extern SafeProfileHandle OpenColorProfile(ref UnsafeNativeMethods.PROFILE pProfile, uint dwDesiredAccess, uint dwShareMode, uint dwCreationMode);

			// Token: 0x06005A2A RID: 23082
			[DllImport("mscms.dll", SetLastError = true)]
			internal static extern bool CloseColorProfile(IntPtr phProfile);

			// Token: 0x06005A2B RID: 23083
			[DllImport("mscms.dll", SetLastError = true)]
			internal static extern bool GetColorProfileHeader(SafeProfileHandle phProfile, out UnsafeNativeMethods.PROFILEHEADER pHeader);

			// Token: 0x06005A2C RID: 23084
			[DllImport("mscms.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
			internal static extern int GetColorDirectory(IntPtr pMachineName, StringBuilder pBuffer, out uint pdwSize);

			// Token: 0x06005A2D RID: 23085
			[DllImport("mscms.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
			internal static extern int GetStandardColorSpaceProfile(IntPtr pMachineName, uint dwProfileID, StringBuilder pProfileName, out uint pdwSize);

			// Token: 0x06005A2E RID: 23086
			[DllImport("mscms.dll", SetLastError = true)]
			internal static extern bool GetColorProfileFromHandle(SafeProfileHandle hProfile, byte[] pBuffer, ref uint pdwSize);
		}

		// Token: 0x02000980 RID: 2432
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class MILFactory2
		{
			// Token: 0x06005A2F RID: 23087
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILCreateFactory")]
			internal static extern int CreateFactory(out IntPtr ppIFactory, uint SDKVersion);

			// Token: 0x06005A30 RID: 23088
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILFactoryCreateMediaPlayer")]
			internal static extern int CreateMediaPlayer(IntPtr THIS_PTR, SafeMILHandle pEventProxy, bool canOpenAllMedia, out SafeMediaHandle ppMedia);

			// Token: 0x06005A31 RID: 23089
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILFactoryCreateBitmapRenderTarget")]
			internal static extern int CreateBitmapRenderTarget(IntPtr THIS_PTR, uint width, uint height, PixelFormatEnum pixelFormatEnum, float dpiX, float dpiY, MILRTInitializationFlags dwFlags, out SafeMILHandle ppIRenderTargetBitmap);

			// Token: 0x06005A32 RID: 23090
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILFactoryCreateSWRenderTargetForBitmap")]
			internal static extern int CreateBitmapRenderTargetForBitmap(IntPtr THIS_PTR, BitmapSourceSafeMILHandle pIBitmap, out SafeMILHandle ppIRenderTargetBitmap);
		}

		// Token: 0x02000981 RID: 2433
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class InteropDeviceBitmap
		{
			// Token: 0x06005A33 RID: 23091
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "InteropDeviceBitmap_Create")]
			internal static extern int Create(IntPtr d3dResource, double dpiX, double dpiY, uint version, UnsafeNativeMethods.InteropDeviceBitmap.FrontBufferAvailableCallback pfnCallback, bool isSoftwareFallbackEnabled, out SafeMILHandle ppInteropDeviceBitmap, out uint pixelWidth, out uint pixelHeight);

			// Token: 0x06005A34 RID: 23092
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "InteropDeviceBitmap_Detach")]
			internal static extern void Detach(SafeMILHandle pInteropDeviceBitmap);

			// Token: 0x06005A35 RID: 23093
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "InteropDeviceBitmap_AddDirtyRect")]
			internal static extern int AddDirtyRect(int x, int y, int w, int h, SafeMILHandle pInteropDeviceBitmap);

			// Token: 0x06005A36 RID: 23094
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "InteropDeviceBitmap_GetAsSoftwareBitmap")]
			internal static extern int GetAsSoftwareBitmap(SafeMILHandle pInteropDeviceBitmap, out BitmapSourceSafeMILHandle pIWICBitmapSource);

			// Token: 0x02000A2F RID: 2607
			// (Invoke) Token: 0x06005C38 RID: 23608
			internal delegate void FrontBufferAvailableCallback(bool lost, uint version);
		}
	}
}
