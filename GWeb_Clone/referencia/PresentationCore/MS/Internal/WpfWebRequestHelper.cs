using System;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Net.Cache;
using System.Security;
using System.Security.Permissions;
using System.Windows.Navigation;
using MS.Internal.AppModel;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal
{
	// Token: 0x02000698 RID: 1688
	internal static class WpfWebRequestHelper
	{
		// Token: 0x06004A23 RID: 18979 RVA: 0x001204BC File Offset: 0x0011F8BC
		[SecurityTreatAsSafe]
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static WebRequest CreateRequest(Uri uri)
		{
			if (string.Compare(uri.Scheme, PackUriHelper.UriSchemePack, StringComparison.Ordinal) == 0)
			{
				return PackWebRequestFactory.CreateWebRequest(uri);
			}
			if (uri.IsFile)
			{
				uri = new Uri(uri.GetLeftPart(UriPartial.Path));
			}
			WebRequest webRequest = WebRequest.Create(uri);
			if (webRequest == null)
			{
				Uri uri2 = BaseUriHelper.PackAppBaseUri.MakeRelativeUri(uri);
				throw new WebException(uri2.ToString(), WebExceptionStatus.RequestCanceled);
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				if (string.IsNullOrEmpty(httpWebRequest.UserAgent))
				{
					httpWebRequest.UserAgent = WpfWebRequestHelper.DefaultUserAgent;
				}
				CookieHandler.HandleWebRequest(httpWebRequest);
				if (string.IsNullOrEmpty(httpWebRequest.Referer))
				{
					httpWebRequest.Referer = BindUriHelper.GetReferer(uri);
				}
				CustomCredentialPolicy.EnsureCustomCredentialPolicy();
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME").Assert();
				try
				{
					httpWebRequest.UseDefaultCredentials = true;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return webRequest;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x001205A0 File Offset: 0x0011F9A0
		[FriendAccessAllowed]
		internal static void ConfigCachePolicy(WebRequest request, bool isRefresh)
		{
			HttpWebRequest httpWebRequest = request as HttpWebRequest;
			if (httpWebRequest != null && (request.CachePolicy == null || request.CachePolicy.Level != RequestCacheLevel.Default))
			{
				if (isRefresh)
				{
					if (WpfWebRequestHelper._httpRequestCachePolicyRefresh == null)
					{
						WpfWebRequestHelper._httpRequestCachePolicyRefresh = new HttpRequestCachePolicy(HttpRequestCacheLevel.Refresh);
					}
					request.CachePolicy = WpfWebRequestHelper._httpRequestCachePolicyRefresh;
					return;
				}
				if (WpfWebRequestHelper._httpRequestCachePolicy == null)
				{
					WpfWebRequestHelper._httpRequestCachePolicy = new HttpRequestCachePolicy();
				}
				request.CachePolicy = WpfWebRequestHelper._httpRequestCachePolicy;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x0012060C File Offset: 0x0011FA0C
		// (set) Token: 0x06004A26 RID: 18982 RVA: 0x00120630 File Offset: 0x0011FA30
		internal static string DefaultUserAgent
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (WpfWebRequestHelper._defaultUserAgent == null)
				{
					WpfWebRequestHelper._defaultUserAgent = UnsafeNativeMethods.ObtainUserAgentString();
				}
				return WpfWebRequestHelper._defaultUserAgent;
			}
			set
			{
				WpfWebRequestHelper._defaultUserAgent = value;
			}
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00120644 File Offset: 0x0011FA44
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static void HandleWebResponse(WebResponse response)
		{
			CookieHandler.HandleWebResponse(response);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00120658 File Offset: 0x0011FA58
		[FriendAccessAllowed]
		internal static Stream CreateRequestAndGetResponseStream(Uri uri)
		{
			WebRequest request = WpfWebRequestHelper.CreateRequest(uri);
			return WpfWebRequestHelper.GetResponseStream(request);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00120674 File Offset: 0x0011FA74
		[FriendAccessAllowed]
		internal static Stream CreateRequestAndGetResponseStream(Uri uri, out ContentType contentType)
		{
			WebRequest request = WpfWebRequestHelper.CreateRequest(uri);
			return WpfWebRequestHelper.GetResponseStream(request, out contentType);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00120690 File Offset: 0x0011FA90
		[FriendAccessAllowed]
		internal static WebResponse CreateRequestAndGetResponse(Uri uri)
		{
			WebRequest request = WpfWebRequestHelper.CreateRequest(uri);
			return WpfWebRequestHelper.GetResponse(request);
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001206AC File Offset: 0x0011FAAC
		[FriendAccessAllowed]
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static WebResponse GetResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			if (response is HttpWebResponse && !(request is HttpWebRequest))
			{
				throw new ArgumentException();
			}
			if (response == null)
			{
				Uri uri = BaseUriHelper.PackAppBaseUri.MakeRelativeUri(request.RequestUri);
				throw new IOException(SR.Get("GetResponseFailed", new object[]
				{
					uri.ToString()
				}));
			}
			WpfWebRequestHelper.HandleWebResponse(response);
			return response;
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x00120710 File Offset: 0x0011FB10
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[FriendAccessAllowed]
		internal static WebResponse EndGetResponse(WebRequest request, IAsyncResult ar)
		{
			WebResponse webResponse = request.EndGetResponse(ar);
			if (webResponse is HttpWebResponse && !(request is HttpWebRequest))
			{
				throw new ArgumentException();
			}
			if (webResponse == null)
			{
				Uri uri = BaseUriHelper.PackAppBaseUri.MakeRelativeUri(request.RequestUri);
				throw new IOException(SR.Get("GetResponseFailed", new object[]
				{
					uri.ToString()
				}));
			}
			WpfWebRequestHelper.HandleWebResponse(webResponse);
			return webResponse;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x00120778 File Offset: 0x0011FB78
		[FriendAccessAllowed]
		internal static Stream GetResponseStream(WebRequest request)
		{
			WebResponse response = WpfWebRequestHelper.GetResponse(request);
			return response.GetResponseStream();
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00120794 File Offset: 0x0011FB94
		[FriendAccessAllowed]
		internal static Stream GetResponseStream(WebRequest request, out ContentType contentType)
		{
			WebResponse response = WpfWebRequestHelper.GetResponse(request);
			contentType = WpfWebRequestHelper.GetContentType(response);
			return response.GetResponseStream();
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x001207B8 File Offset: 0x0011FBB8
		[FriendAccessAllowed]
		internal static ContentType GetContentType(WebResponse response)
		{
			ContentType contentType = ContentType.Empty;
			if (!(response is FileWebResponse))
			{
				try
				{
					contentType = new ContentType(response.ContentType);
					if (MimeTypeMapper.OctetMime.AreTypeAndSubTypeEqual(contentType, true) || MimeTypeMapper.TextPlainMime.AreTypeAndSubTypeEqual(contentType, true))
					{
						string fileExtension = MimeTypeMapper.GetFileExtension(response.ResponseUri);
						if (string.Compare(fileExtension, "xaml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(fileExtension, "xbap", StringComparison.OrdinalIgnoreCase) == 0)
						{
							contentType = ContentType.Empty;
						}
					}
				}
				catch (NotImplementedException)
				{
				}
				catch (NotSupportedException)
				{
				}
			}
			if (contentType.TypeComponent == ContentType.Empty.TypeComponent && contentType.OriginalString == ContentType.Empty.OriginalString && contentType.SubTypeComponent == ContentType.Empty.SubTypeComponent)
			{
				contentType = MimeTypeMapper.GetMimeTypeFromUri(response.ResponseUri);
			}
			return contentType;
		}

		// Token: 0x04001EF1 RID: 7921
		private static HttpRequestCachePolicy _httpRequestCachePolicy;

		// Token: 0x04001EF2 RID: 7922
		private static HttpRequestCachePolicy _httpRequestCachePolicyRefresh;

		// Token: 0x04001EF3 RID: 7923
		private static string _defaultUserAgent;
	}
}
