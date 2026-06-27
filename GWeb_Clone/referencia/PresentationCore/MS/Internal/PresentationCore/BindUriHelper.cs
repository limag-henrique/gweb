using System;
using System.Security;
using System.Text;
using System.Windows.Navigation;
using MS.Internal.AppModel;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007EA RID: 2026
	internal static class BindUriHelper
	{
		// Token: 0x060054E5 RID: 21733 RVA: 0x0015E468 File Offset: 0x0015D868
		internal static string UriToString(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return new StringBuilder(uri.GetComponents(uri.IsAbsoluteUri ? UriComponents.AbsoluteUri : UriComponents.SerializationInfoString, UriFormat.SafeUnescaped), 2083).ToString();
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0015E4B0 File Offset: 0x0015D8B0
		// (set) Token: 0x060054E7 RID: 21735 RVA: 0x0015E4C4 File Offset: 0x0015D8C4
		internal static Uri BaseUri
		{
			get
			{
				return BaseUriHelper.BaseUri;
			}
			[SecurityCritical]
			set
			{
				BaseUriHelper.BaseUri = BaseUriHelper.FixFileUri(value);
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0015E4DC File Offset: 0x0015D8DC
		internal static bool DoSchemeAndHostMatch(Uri first, Uri second)
		{
			return SecurityHelper.AreStringTypesEqual(first.Scheme, second.Scheme) && first.Host.Equals(second.Host);
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0015E510 File Offset: 0x0015D910
		internal static Uri GetResolvedUri(Uri baseUri, Uri orgUri)
		{
			Uri result;
			if (orgUri == null)
			{
				result = null;
			}
			else if (!orgUri.IsAbsoluteUri)
			{
				Uri baseUri2 = (baseUri == null) ? BindUriHelper.BaseUri : baseUri;
				result = new Uri(baseUri2, orgUri);
			}
			else
			{
				result = BaseUriHelper.FixFileUri(orgUri);
			}
			return result;
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x0015E558 File Offset: 0x0015D958
		internal static string GetReferer(Uri destinationUri)
		{
			string result = null;
			Uri browserSource = SiteOfOriginContainer.BrowserSource;
			if (browserSource != null)
			{
				SecurityZone securityZone = CustomCredentialPolicy.MapUrlToZone(browserSource);
				SecurityZone securityZone2 = CustomCredentialPolicy.MapUrlToZone(destinationUri);
				if (securityZone == securityZone2 && SecurityHelper.AreStringTypesEqual(browserSource.Scheme, destinationUri.Scheme))
				{
					result = browserSource.GetComponents(UriComponents.AbsoluteUri, UriFormat.UriEscaped);
				}
			}
			return result;
		}

		// Token: 0x0400265C RID: 9820
		private const int MAX_PATH_LENGTH = 2048;

		// Token: 0x0400265D RID: 9821
		private const int MAX_SCHEME_LENGTH = 32;

		// Token: 0x0400265E RID: 9822
		public const int MAX_URL_LENGTH = 2083;
	}
}
