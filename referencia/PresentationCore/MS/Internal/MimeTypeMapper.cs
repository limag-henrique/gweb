using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using MS.Internal.PresentationCore;
using MS.Win32.Compile;

namespace MS.Internal
{
	// Token: 0x0200067B RID: 1659
	[FriendAccessAllowed]
	internal static class MimeTypeMapper
	{
		// Token: 0x06004936 RID: 18742 RVA: 0x0011DAF4 File Offset: 0x0011CEF4
		internal static ContentType GetMimeTypeFromUri(Uri uriSource)
		{
			ContentType contentType = ContentType.Empty;
			if (uriSource != null)
			{
				Uri uri = uriSource;
				if (!uri.IsAbsoluteUri)
				{
					uri = new Uri("http://foo/bar/");
					uri = new Uri(uri, uriSource);
				}
				string fileExtension = MimeTypeMapper.GetFileExtension(uri);
				object syncRoot = ((ICollection)MimeTypeMapper._fileExtensionToMimeType).SyncRoot;
				lock (syncRoot)
				{
					if (MimeTypeMapper._fileExtensionToMimeType.Count == 0)
					{
						MimeTypeMapper._fileExtensionToMimeType.Add("xaml", MimeTypeMapper.XamlMime);
						MimeTypeMapper._fileExtensionToMimeType.Add("baml", MimeTypeMapper.BamlMime);
						MimeTypeMapper._fileExtensionToMimeType.Add("jpg", MimeTypeMapper.JpgMime);
						MimeTypeMapper._fileExtensionToMimeType.Add("xbap", MimeTypeMapper.XbapMime);
					}
					if (!MimeTypeMapper._fileExtensionToMimeType.TryGetValue(fileExtension, out contentType))
					{
						contentType = MimeTypeMapper.GetMimeTypeFromUrlMon(uriSource);
						if (contentType != ContentType.Empty)
						{
							MimeTypeMapper._fileExtensionToMimeType.Add(fileExtension, contentType);
						}
					}
				}
			}
			return contentType;
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0011DC00 File Offset: 0x0011D000
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static ContentType GetMimeTypeFromUrlMon(Uri uriSource)
		{
			ContentType result = ContentType.Empty;
			string text;
			if (uriSource != null && UnsafeNativeMethods.FindMimeFromData(null, BindUriHelper.UriToString(uriSource), IntPtr.Zero, 0, null, 0, out text, 0) == 0 && text != null)
			{
				result = new ContentType(text);
			}
			return result;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0011DC44 File Offset: 0x0011D044
		private static string GetDocument(Uri uri)
		{
			string result;
			if (uri.IsFile)
			{
				result = uri.LocalPath;
			}
			else
			{
				result = uri.GetLeftPart(UriPartial.Path);
			}
			return result;
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0011DC6C File Offset: 0x0011D06C
		internal static string GetFileExtension(Uri uri)
		{
			string document = MimeTypeMapper.GetDocument(uri);
			string extension = Path.GetExtension(document);
			string result = string.Empty;
			if (!string.IsNullOrEmpty(extension))
			{
				result = extension.Substring(1).ToLower(CultureInfo.InvariantCulture);
			}
			return result;
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0011DCA8 File Offset: 0x0011D0A8
		internal static bool IsHTMLMime(ContentType contentType)
		{
			return MimeTypeMapper.HtmlMime.AreTypeAndSubTypeEqual(contentType) || MimeTypeMapper.HtmMime.AreTypeAndSubTypeEqual(contentType);
		}

		// Token: 0x04001CBC RID: 7356
		private static readonly Dictionary<string, ContentType> _fileExtensionToMimeType = new Dictionary<string, ContentType>(4);

		// Token: 0x04001CBD RID: 7357
		internal static readonly ContentType OctetMime = new ContentType("application/octet-stream");

		// Token: 0x04001CBE RID: 7358
		internal static readonly ContentType TextPlainMime = new ContentType("text/plain");

		// Token: 0x04001CBF RID: 7359
		internal const string XamlExtension = "xaml";

		// Token: 0x04001CC0 RID: 7360
		internal const string BamlExtension = "baml";

		// Token: 0x04001CC1 RID: 7361
		internal const string XbapExtension = "xbap";

		// Token: 0x04001CC2 RID: 7362
		internal const string JpgExtension = "jpg";

		// Token: 0x04001CC3 RID: 7363
		internal static readonly ContentType XamlMime = new ContentType("application/xaml+xml");

		// Token: 0x04001CC4 RID: 7364
		internal static readonly ContentType BamlMime = new ContentType("application/baml+xml");

		// Token: 0x04001CC5 RID: 7365
		internal static readonly ContentType JpgMime = new ContentType("image/jpg");

		// Token: 0x04001CC6 RID: 7366
		internal static readonly ContentType IconMime = new ContentType("image/x-icon");

		// Token: 0x04001CC7 RID: 7367
		internal static readonly ContentType FixedDocumentSequenceMime = new ContentType("application/vnd.ms-package.xps-fixeddocumentsequence+xml");

		// Token: 0x04001CC8 RID: 7368
		internal static readonly ContentType FixedDocumentMime = new ContentType("application/vnd.ms-package.xps-fixeddocument+xml");

		// Token: 0x04001CC9 RID: 7369
		internal static readonly ContentType FixedPageMime = new ContentType("application/vnd.ms-package.xps-fixedpage+xml");

		// Token: 0x04001CCA RID: 7370
		internal static readonly ContentType ResourceDictionaryMime = new ContentType("application/vnd.ms-package.xps-resourcedictionary+xml");

		// Token: 0x04001CCB RID: 7371
		internal static readonly ContentType HtmlMime = new ContentType("text/html");

		// Token: 0x04001CCC RID: 7372
		internal static readonly ContentType HtmMime = new ContentType("text/htm");

		// Token: 0x04001CCD RID: 7373
		internal static readonly ContentType XbapMime = new ContentType("application/x-ms-xbap");
	}
}
