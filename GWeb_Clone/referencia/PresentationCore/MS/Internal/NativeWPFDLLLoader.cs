using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using <CppImplementationDetails>;

namespace MS.Internal
{
	// Token: 0x0200015E RID: 350
	internal sealed class NativeWPFDLLLoader
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00012E30 File Offset: 0x00012230
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe static void LoadCommonDLLsAndDwrite()
		{
			$ArrayType$$$BY0BAE@G $ArrayType$$$BY0BAE@G;
			int num = <Module>.WPFUtils.GetWPFInstallPath((ushort*)(&$ArrayType$$$BY0BAE@G), 260UL);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			void* ptr = null;
			IntPtr hDWrite = new IntPtr((void*)<Module>.WPFUtils.LoadDWriteLibraryAndGetProcAddress(&ptr));
			NativeWPFDLLLoader.m_hDWrite = hDWrite;
			if (NativeWPFDLLLoader.m_hDWrite == IntPtr.Zero)
			{
				throw new DllNotFoundException("dwrite.dll", new Win32Exception());
			}
			if (ptr == null)
			{
				throw new InvalidOperationException();
			}
			NativeWPFDLLLoader.m_pfnDWriteCreateFactory = ptr;
			NativeWPFDLLLoader.m_hWpfGfx = NativeWPFDLLLoader.LoadNativeWPFDLL((ushort*)(&<Module>.??_C@_1CC@NDKAMMGP@?$AAw?$AAp?$AAf?$AAg?$AAf?$AAx?$AA_?$AAv?$AA0?$AA4?$AA0?$AA0?$AA?4?$AAd?$AAl@), (ushort*)(&$ArrayType$$$BY0BAE@G));
			NativeWPFDLLLoader.m_hPresentationNative = NativeWPFDLLLoader.LoadNativeWPFDLL((ushort*)(&<Module>.??_C@_1DK@CKKFELDO@?$AAP?$AAr?$AAe?$AAs?$AAe?$AAn?$AAt?$AAa?$AAt?$AAi?$AAo?$AAn?$AAN?$AAa?$AAt@), (ushort*)(&$ArrayType$$$BY0BAE@G));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012C14 File Offset: 0x00012014
		[SecuritySafeCritical]
		public unsafe static void UnloadCommonDLLs()
		{
			if (NativeWPFDLLLoader.m_hWpfGfx != IntPtr.Zero)
			{
				if (<Module>.FreeLibrary((HINSTANCE__*)NativeWPFDLLLoader.m_hWpfGfx.ToPointer()) == null)
				{
					uint lastWin32Error = Marshal.GetLastWin32Error();
					Marshal.ThrowExceptionForHR((lastWin32Error > 0) ? ((lastWin32Error & 65535) | -2147024896) : lastWin32Error);
				}
				NativeWPFDLLLoader.m_hWpfGfx = IntPtr.Zero;
			}
			if (NativeWPFDLLLoader.m_hPresentationNative != IntPtr.Zero)
			{
				if (<Module>.FreeLibrary((HINSTANCE__*)NativeWPFDLLLoader.m_hPresentationNative.ToPointer()) == null)
				{
					uint lastWin32Error2 = Marshal.GetLastWin32Error();
					Marshal.ThrowExceptionForHR((lastWin32Error2 > 0) ? ((lastWin32Error2 & 65535) | -2147024896) : lastWin32Error2);
				}
				NativeWPFDLLLoader.m_hPresentationNative = IntPtr.Zero;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00012EC0 File Offset: 0x000122C0
		[SecuritySafeCritical]
		public unsafe static void UnloadDWrite()
		{
			NativeWPFDLLLoader.ClearDWriteCreateFactoryFunctionPointer();
			if (NativeWPFDLLLoader.m_hDWrite != IntPtr.Zero)
			{
				if (<Module>.FreeLibrary((HINSTANCE__*)NativeWPFDLLLoader.m_hDWrite.ToPointer()) == null)
				{
					uint lastWin32Error = Marshal.GetLastWin32Error();
					Marshal.ThrowExceptionForHR((lastWin32Error > 0) ? ((lastWin32Error & 65535) | -2147024896) : lastWin32Error);
				}
				NativeWPFDLLLoader.m_hDWrite = IntPtr.Zero;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00012F20 File Offset: 0x00012320
		[SecuritySafeCritical]
		public unsafe static void LoadPresentationNative()
		{
			if (NativeWPFDLLLoader.m_hPresentationNative == IntPtr.Zero)
			{
				$ArrayType$$$BY0BAE@G $ArrayType$$$BY0BAE@G;
				int num = <Module>.WPFUtils.GetWPFInstallPath((ushort*)(&$ArrayType$$$BY0BAE@G), 260UL);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				NativeWPFDLLLoader.m_hPresentationNative = NativeWPFDLLLoader.LoadNativeWPFDLL((ushort*)(&<Module>.??_C@_1DK@CKKFELDO@?$AAP?$AAr?$AAe?$AAs?$AAe?$AAn?$AAt?$AAa?$AAt?$AAi?$AAo?$AAn?$AAN?$AAa?$AAt@), (ushort*)(&$ArrayType$$$BY0BAE@G));
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012CB8 File Offset: 0x000120B8
		[SecurityCritical]
		public unsafe static void* GetDWriteCreateFactoryFunctionPointer()
		{
			return NativeWPFDLLLoader.m_pfnDWriteCreateFactory;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012CCC File Offset: 0x000120CC
		[SecurityCritical]
		public static void ClearDWriteCreateFactoryFunctionPointer()
		{
			NativeWPFDLLLoader.m_pfnDWriteCreateFactory = null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012CE0 File Offset: 0x000120E0
		[SecurityCritical]
		private unsafe static IntPtr LoadNativeWPFDLL(ushort* relDllPath, ushort* baseDllPath)
		{
			$ArrayType$$$BY0BAE@G $ArrayType$$$BY0BAE@G;
			initblk(ref $ArrayType$$$BY0BAE@G, 0, 520L);
			if (<Module>.PathCombineW((ushort*)(&$ArrayType$$$BY0BAE@G), baseDllPath, relDllPath) == null)
			{
				throw new PathTooLongException();
			}
			IntPtr intPtr = new IntPtr(<Module>.LoadLibraryW((ushort*)(&$ArrayType$$$BY0BAE@G)));
			if (intPtr == IntPtr.Zero)
			{
				throw new DllNotFoundException(new string((char*)(&$ArrayType$$$BY0BAE@G)), new Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x04000486 RID: 1158
		private static IntPtr m_hWpfGfx = 0;

		// Token: 0x04000487 RID: 1159
		private static IntPtr m_hPresentationNative = 0;

		// Token: 0x04000488 RID: 1160
		private static IntPtr m_hDWrite = 0;

		// Token: 0x04000489 RID: 1161
		[SecurityCritical]
		private unsafe static void* m_pfnDWriteCreateFactory;
	}
}
