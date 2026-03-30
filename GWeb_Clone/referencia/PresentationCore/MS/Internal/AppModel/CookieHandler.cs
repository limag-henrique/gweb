using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.AppModel
{
	// Token: 0x020007A5 RID: 1957
	internal static class CookieHandler
	{
		// Token: 0x06005243 RID: 21059 RVA: 0x00148010 File Offset: 0x00147410
		internal static void HandleWebRequest(WebRequest request)
		{
			HttpWebRequest httpWebRequest = request as HttpWebRequest;
			if (httpWebRequest != null)
			{
				try
				{
					string cookie = CookieHandler.GetCookie(httpWebRequest.RequestUri, false);
					if (!string.IsNullOrEmpty(cookie))
					{
						if (httpWebRequest.CookieContainer == null)
						{
							httpWebRequest.CookieContainer = new CookieContainer();
						}
						httpWebRequest.CookieContainer.SetCookies(httpWebRequest.RequestUri, cookie.Replace(';', ','));
					}
				}
				catch (Exception ex)
				{
					if (CriticalExceptions.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x00148098 File Offset: 0x00147498
		[SecurityCritical]
		internal static void HandleWebResponse(WebResponse response)
		{
			HttpWebResponse httpWebResponse = response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				WebHeaderCollection headers = httpWebResponse.Headers;
				for (int i = headers.Count - 1; i >= 0; i--)
				{
					if (string.Compare(headers.Keys[i], "Set-Cookie", StringComparison.OrdinalIgnoreCase) == 0)
					{
						string p3pHeader = httpWebResponse.Headers["P3P"];
						foreach (string cookieData in headers.GetValues(i))
						{
							try
							{
								CookieHandler.SetCookieUnsafe(httpWebResponse.ResponseUri, cookieData, p3pHeader);
							}
							catch (Exception ex)
							{
								if (CriticalExceptions.IsCriticalException(ex))
								{
									throw;
								}
							}
						}
						return;
					}
				}
			}
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x00148154 File Offset: 0x00147554
		[FriendAccessAllowed]
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static string GetCookie(Uri uri, bool throwIfNoCookie)
		{
			SecurityHelper.DemandWebPermission(uri);
			uint num = 0U;
			string url = BindUriHelper.UriToString(uri);
			if (UnsafeNativeMethods.InternetGetCookieEx(url, null, null, ref num, 0U, IntPtr.Zero))
			{
				num += 1U;
				StringBuilder stringBuilder = new StringBuilder((int)num);
				if (UnsafeNativeMethods.InternetGetCookieEx(url, null, stringBuilder, ref num, 0U, IntPtr.Zero))
				{
					return stringBuilder.ToString();
				}
			}
			if (!throwIfNoCookie && Marshal.GetLastWin32Error() == 259)
			{
				return null;
			}
			throw new Win32Exception();
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x001481BC File Offset: 0x001475BC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static bool SetCookie(Uri uri, string cookieData)
		{
			SecurityHelper.DemandWebPermission(uri);
			return CookieHandler.SetCookieUnsafe(uri, cookieData, null);
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x001481D8 File Offset: 0x001475D8
		[SecurityCritical]
		private static bool SetCookieUnsafe(Uri uri, string cookieData, string p3pHeader)
		{
			string url = BindUriHelper.UriToString(uri);
			uint num = UnsafeNativeMethods.InternetSetCookieEx(url, null, cookieData, 64U, p3pHeader);
			if (num == 0U)
			{
				throw new Win32Exception();
			}
			return num != 5U;
		}
	}
}
