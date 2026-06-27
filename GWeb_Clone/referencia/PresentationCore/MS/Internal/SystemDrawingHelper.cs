using System;
using System.IO;
using System.Security;

namespace MS.Internal
{
	// Token: 0x02000697 RID: 1687
	internal static class SystemDrawingHelper
	{
		// Token: 0x06004A15 RID: 18965 RVA: 0x001202E0 File Offset: 0x0011F6E0
		internal static bool IsBitmap(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(false);
			return systemDrawingExtensionMethods != null && systemDrawingExtensionMethods.IsBitmap(data);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00120300 File Offset: 0x0011F700
		internal static bool IsImage(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(false);
			return systemDrawingExtensionMethods != null && systemDrawingExtensionMethods.IsImage(data);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00120320 File Offset: 0x0011F720
		internal static bool IsMetafile(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(false);
			return systemDrawingExtensionMethods != null && systemDrawingExtensionMethods.IsMetafile(data);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00120340 File Offset: 0x0011F740
		[SecurityCritical]
		internal static IntPtr GetHandleFromMetafile(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(false);
			if (systemDrawingExtensionMethods == null)
			{
				return IntPtr.Zero;
			}
			return systemDrawingExtensionMethods.GetHandleFromMetafile(data);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00120364 File Offset: 0x0011F764
		internal static object GetMetafileFromHemf(IntPtr hMetafile)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.GetMetafileFromHemf(hMetafile);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00120384 File Offset: 0x0011F784
		internal static object GetBitmap(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.GetBitmap(data);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x001203A4 File Offset: 0x0011F7A4
		[SecurityCritical]
		internal static IntPtr GetHBitmap(object data, out int width, out int height)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods != null)
			{
				return systemDrawingExtensionMethods.GetHBitmap(data, out width, out height);
			}
			width = (height = 0);
			return IntPtr.Zero;
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x001203D4 File Offset: 0x0011F7D4
		[SecurityCritical]
		internal static IntPtr GetHBitmapFromBitmap(object data)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(false);
			if (systemDrawingExtensionMethods == null)
			{
				return IntPtr.Zero;
			}
			return systemDrawingExtensionMethods.GetHBitmapFromBitmap(data);
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001203F8 File Offset: 0x0011F7F8
		[SecurityCritical]
		internal static IntPtr ConvertMetafileToHBitmap(IntPtr handle)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return IntPtr.Zero;
			}
			return systemDrawingExtensionMethods.ConvertMetafileToHBitmap(handle);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0012041C File Offset: 0x0011F81C
		internal static Stream GetCommentFromGifStream(Stream stream)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.GetCommentFromGifStream(stream);
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0012043C File Offset: 0x0011F83C
		internal static CodeAccessPermission NewSafePrintingPermission()
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.NewSafePrintingPermission();
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0012045C File Offset: 0x0011F85C
		internal static CodeAccessPermission NewDefaultPrintingPermission()
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.NewDefaultPrintingPermission();
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0012047C File Offset: 0x0011F87C
		internal static void SaveMetafileToImageStream(MemoryStream metafileStream, Stream imageStream)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods != null)
			{
				systemDrawingExtensionMethods.SaveMetafileToImageStream(metafileStream, imageStream);
			}
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0012049C File Offset: 0x0011F89C
		[SecurityCritical]
		internal static object GetBitmapFromBitmapSource(object source)
		{
			SystemDrawingExtensionMethods systemDrawingExtensionMethods = AssemblyHelper.ExtensionsForSystemDrawing(true);
			if (systemDrawingExtensionMethods == null)
			{
				return null;
			}
			return systemDrawingExtensionMethods.GetBitmapFromBitmapSource(source);
		}
	}
}
