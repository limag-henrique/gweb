using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Navigation;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077B RID: 1915
	[FriendAccessAllowed]
	internal static class Util
	{
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x00142B48 File Offset: 0x00141F48
		internal static string CompositeFontExtension
		{
			get
			{
				return Util.SupportedExtensions[0];
			}
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x00142B5C File Offset: 0x00141F5C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static Util()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Windir");
			environmentPermission.Assert();
			string text;
			try
			{
				text = Environment.GetEnvironmentVariable("windir") + "\\Fonts\\";
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			Util._windowsFontsLocalPath = text.ToUpperInvariant();
			Util._windowsFontsUriObject = new Uri(Util._windowsFontsLocalPath, UriKind.Absolute);
			Util._windowsFontsUriString = Util._windowsFontsUriObject.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x00142C30 File Offset: 0x00142030
		internal static string WindowsFontsLocalPath
		{
			[SecurityCritical]
			get
			{
				return Util._windowsFontsLocalPath;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x00142C44 File Offset: 0x00142044
		internal static float PixelsPerDip
		{
			get
			{
				return (float)Util.Dpi / 96f;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x00142C60 File Offset: 0x00142060
		internal static int Dpi
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (!Util._dpiInitialized)
				{
					object dpiLock = Util._dpiLock;
					lock (dpiLock)
					{
						if (!Util._dpiInitialized)
						{
							HandleRef hWnd = new HandleRef(null, IntPtr.Zero);
							IntPtr dc = UnsafeNativeMethods.GetDC(hWnd);
							if (dc == IntPtr.Zero)
							{
								throw new Win32Exception();
							}
							try
							{
								Util._dpi = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
								Util._dpiInitialized = true;
							}
							finally
							{
								UnsafeNativeMethods.ReleaseDC(hWnd, new HandleRef(null, dc));
							}
						}
					}
				}
				return Util._dpi;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060050A0 RID: 20640 RVA: 0x00142D28 File Offset: 0x00142128
		internal static Uri WindowsFontsUriObject
		{
			[SecurityCritical]
			get
			{
				return Util._windowsFontsUriObject;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x00142D3C File Offset: 0x0014213C
		internal static string WindowsFontsUriString
		{
			[SecurityCritical]
			get
			{
				return Util._windowsFontsUriString;
			}
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x00142D50 File Offset: 0x00142150
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static bool IsReferenceToWindowsFonts(string s)
		{
			if (string.IsNullOrEmpty(s) || s[0] == '#')
			{
				return true;
			}
			int num = s.IndexOf('#');
			if (num < 0)
			{
				num = s.Length;
			}
			if (s.IndexOfAny(Util.InvalidFileNameChars, 0, num) >= 0)
			{
				return false;
			}
			for (int i = s.IndexOf('%', 0, num); i >= 0; i = s.IndexOf('%', i, num - i))
			{
				char value = Uri.HexUnescape(s, ref i);
				if (Array.IndexOf<char>(Util.InvalidFileNameChars, value) >= 0)
				{
					return false;
				}
			}
			if (s[0] == '.')
			{
				int j;
				for (j = 1; j < num; j++)
				{
					if (s[j] != '.')
					{
						break;
					}
				}
				while (j < num && char.IsWhiteSpace(s[j]))
				{
					j++;
				}
				if (j == num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x00142E10 File Offset: 0x00142210
		internal static bool IsSupportedSchemeForAbsoluteFontFamilyUri(Uri absoluteUri)
		{
			return absoluteUri.IsFile;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x00142E24 File Offset: 0x00142224
		internal static void SplitFontFaceIndex(Uri fontUri, out Uri fontSourceUri, out int faceIndex)
		{
			string components = fontUri.GetComponents(UriComponents.Fragment, UriFormat.SafeUnescaped);
			if (string.IsNullOrEmpty(components))
			{
				faceIndex = 0;
				fontSourceUri = fontUri;
				return;
			}
			if (!int.TryParse(components, NumberStyles.None, CultureInfo.InvariantCulture, out faceIndex))
			{
				throw new ArgumentException(SR.Get("FaceIndexMustBePositiveOrZero"), "fontUri");
			}
			fontSourceUri = new Uri(fontUri.GetComponents(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query, UriFormat.SafeUnescaped));
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x00142E80 File Offset: 0x00142280
		internal static Uri CombineUriWithFaceIndex(string fontUri, int faceIndex)
		{
			if (faceIndex == 0)
			{
				return new Uri(fontUri);
			}
			string components = new Uri(fontUri).GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
			string str = faceIndex.ToString(CultureInfo.InvariantCulture);
			return new Uri(components + "#" + str);
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x00142EC4 File Offset: 0x001422C4
		internal static bool IsSupportedFontExtension(string extension, out bool isComposite)
		{
			for (int i = 0; i < Util.SupportedExtensions.Length; i++)
			{
				string strB = Util.SupportedExtensions[i];
				if (string.Compare(extension, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					isComposite = (i == 0);
					return true;
				}
			}
			isComposite = false;
			return false;
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00142F04 File Offset: 0x00142304
		internal static bool IsCompositeFont(string extension)
		{
			return string.Compare(extension, Util.CompositeFontExtension, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00142F20 File Offset: 0x00142320
		internal static bool IsEnumerableFontUriScheme(Uri fontLocation)
		{
			bool result = false;
			if (fontLocation.IsAbsoluteUri)
			{
				Uri uri;
				if (fontLocation.IsFile)
				{
					result = true;
				}
				else if (fontLocation.Scheme == PackUriHelper.UriSchemePack && Uri.TryCreate(fontLocation, "X", out uri))
				{
					result = BaseUriHelper.IsPackApplicationUri(uri);
				}
			}
			return result;
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00142F6C File Offset: 0x0014236C
		internal static bool IsAppSpecificUri(Uri fontLocation)
		{
			return !fontLocation.IsAbsoluteUri || !fontLocation.IsFile || fontLocation.IsUnc;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00142F94 File Offset: 0x00142394
		internal static string GetUriExtension(Uri uri)
		{
			string components = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			return Path.GetExtension(components);
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x00142FB4 File Offset: 0x001423B4
		internal static string GetNormalizedFontFamilyReference(string friendlyName, int startIndex, int length)
		{
			if (friendlyName.IndexOf(',', startIndex, length) < 0)
			{
				return Util.NormalizeFontFamilyReference(friendlyName, startIndex, length);
			}
			return Util.NormalizeFontFamilyReference(friendlyName.Substring(startIndex, length).Replace(",,", ","));
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x00142FF4 File Offset: 0x001423F4
		private static string NormalizeFontFamilyReference(string fontFamilyReference)
		{
			return Util.NormalizeFontFamilyReference(fontFamilyReference, 0, fontFamilyReference.Length);
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x00143010 File Offset: 0x00142410
		private static string NormalizeFontFamilyReference(string fontFamilyReference, int startIndex, int length)
		{
			if (length == 0)
			{
				return "#";
			}
			int num = fontFamilyReference.IndexOf('#', startIndex, length);
			if (num < 0)
			{
				return "#" + fontFamilyReference.Substring(startIndex, length).ToUpperInvariant();
			}
			if (num + 1 == startIndex + length)
			{
				return "#";
			}
			if (num == startIndex)
			{
				return fontFamilyReference.Substring(startIndex, length).ToUpperInvariant();
			}
			string str = fontFamilyReference.Substring(startIndex, num - startIndex);
			string text = fontFamilyReference.Substring(num, startIndex + length - num);
			return str + text.ToUpperInvariant();
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x00143094 File Offset: 0x00142494
		internal static string ConvertFamilyNameAndLocationToFontFamilyReference(string familyName, string location)
		{
			string text = familyName.Replace("%", "%25").Replace("#", "%23");
			if (!string.IsNullOrEmpty(location))
			{
				text = location + "#" + text;
			}
			return text;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x001430D8 File Offset: 0x001424D8
		internal static string ConvertFontFamilyReferenceToFriendlyName(string fontFamilyReference)
		{
			return fontFamilyReference.Replace(",", ",,");
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x001430F8 File Offset: 0x001424F8
		internal static int CompareOrdinalIgnoreCase(string a, string b)
		{
			int length = a.Length;
			int length2 = b.Length;
			int num = Math.Min(length, length2);
			for (int i = 0; i < num; i++)
			{
				int num2 = Util.CompareOrdinalIgnoreCase(a[i], b[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return length - length2;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00143148 File Offset: 0x00142548
		private static int CompareOrdinalIgnoreCase(char a, char b)
		{
			char c = char.ToUpperInvariant(a);
			char c2 = char.ToUpperInvariant(b);
			return (int)(c - c2);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x00143168 File Offset: 0x00142568
		private static void ValidateFileNamePermissions(ref string fileName)
		{
			if (!SecurityHelper.CallerHasPathDiscoveryPermission(fileName))
			{
				fileName = Path.GetFileName(fileName);
			}
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00143188 File Offset: 0x00142588
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void ThrowWin32Exception(int errorCode, string fileName)
		{
			Util.ValidateFileNamePermissions(ref fileName);
			switch (errorCode)
			{
			case 2:
				throw new FileNotFoundException(SR.Get("FileNotFoundExceptionWithFileName", new object[]
				{
					fileName
				}), fileName);
			case 3:
				throw new DirectoryNotFoundException(SR.Get("DirectoryNotFoundExceptionWithFileName", new object[]
				{
					fileName
				}));
			case 4:
				break;
			case 5:
				throw new UnauthorizedAccessException(SR.Get("UnauthorizedAccessExceptionWithFileName", new object[]
				{
					fileName
				}));
			default:
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.Get("PathTooLongExceptionWithFileName", new object[]
					{
						fileName
					}));
				}
				break;
			}
			throw new IOException(SR.Get("IOExceptionWithFileName", new object[]
			{
				fileName
			}), NativeMethods.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00143248 File Offset: 0x00142648
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Exception ConvertInPageException(FontSource fontSource, SEHException e)
		{
			string text;
			if (fontSource.Uri.IsFile)
			{
				text = fontSource.Uri.LocalPath;
				Util.ValidateFileNamePermissions(ref text);
			}
			else
			{
				text = fontSource.GetUriString();
			}
			return new IOException(SR.Get("IOExceptionWithFileName", new object[]
			{
				text
			}), e);
		}

		// Token: 0x040024BA RID: 9402
		internal const int nullOffset = -1;

		// Token: 0x040024BB RID: 9403
		private static readonly string[] SupportedExtensions = new string[]
		{
			".COMPOSITEFONT",
			".OTF",
			".TTC",
			".TTF",
			".TTE"
		};

		// Token: 0x040024BC RID: 9404
		private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

		// Token: 0x040024BD RID: 9405
		internal const UriComponents UriWithoutFragment = UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query;

		// Token: 0x040024BE RID: 9406
		private const string WinDir = "windir";

		// Token: 0x040024BF RID: 9407
		private const string EmptyFontFamilyReference = "#";

		// Token: 0x040024C0 RID: 9408
		private const string EmptyCanonicalName = "";

		// Token: 0x040024C1 RID: 9409
		private static object _dpiLock = new object();

		// Token: 0x040024C2 RID: 9410
		private static int _dpi;

		// Token: 0x040024C3 RID: 9411
		private static bool _dpiInitialized = false;

		// Token: 0x040024C4 RID: 9412
		[SecurityCritical]
		private static readonly string _windowsFontsLocalPath;

		// Token: 0x040024C5 RID: 9413
		[SecurityCritical]
		private static readonly Uri _windowsFontsUriObject;

		// Token: 0x040024C6 RID: 9414
		[SecurityCritical]
		private static readonly string _windowsFontsUriString;
	}
}
