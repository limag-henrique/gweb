using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace MS.Internal.Text.TextInterface.Native
{
	// Token: 0x02000006 RID: 6
	internal sealed class Util
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00010468 File Offset: 0x0000F868
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static void ConvertHresultToException(int hr)
		{
			if (hr < 0)
			{
				if (hr == -2003283965)
				{
					throw new FileNotFoundException();
				}
				if (hr == -2003283964)
				{
					throw new UnauthorizedAccessException();
				}
				if (hr == -2003283968)
				{
					throw new FileFormatException();
				}
				Util.SanitizeAndThrowIfKnownException(hr);
				IntPtr errorInfo = new IntPtr(-1);
				Marshal.ThrowExceptionForHR(hr, errorInfo);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AEF4 File Offset: 0x0000A2F4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static ref char GetPtrToStringChars(string s)
		{
			return <Module>.CriticalPtrToStringChars(s);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AF08 File Offset: 0x0000A308
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe static _GUID ToGUID(Guid* guid)
		{
			ref byte byte& = ref guid.ToByteArray()[0];
			_GUID result;
			cpblk(ref result, ref byte&, 16);
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F37C File Offset: 0x0000E77C
		[SecurityCritical]
		private static void SanitizeAndThrowIfKnownException(int hr)
		{
			if (hr == -2146233079)
			{
				Exception exceptionForHR = Marshal.GetExceptionForHR(-2146233079);
				if (exceptionForHR is WebException)
				{
					if (Util.IsFullTrustCaller())
					{
						throw exceptionForHR;
					}
					throw new WebException();
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AF30 File Offset: 0x0000A330
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		private static bool IsFullTrustCaller()
		{
			try
			{
				if (Util._fullTrustPermissionSet == null)
				{
					Util._fullTrustPermissionSet = new PermissionSet(PermissionState.Unrestricted);
				}
				Util._fullTrustPermissionSet.Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x040002F8 RID: 760
		private static PermissionSet _fullTrustPermissionSet = null;
	}
}
