using System;
using System.Net;
using System.Security;
using MS.Internal;
using MS.Internal.IO.Packaging;
using MS.Internal.PresentationCore;

namespace System.IO.Packaging
{
	/// <summary>Representa a classe que é invocada quando uma instância de um URI de pacote <see cref="T:System.IO.Packaging.PackWebRequest" /> é criado.</summary>
	// Token: 0x02000184 RID: 388
	public sealed class PackWebRequestFactory : IWebRequestCreate
	{
		/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.  Use o método <see cref="T:System.IO.Packaging.PackUriHelper" /> fortemente tipado em vez disso.</summary>
		/// <param name="uri">O URI para criar a solicitação da Web.</param>
		/// <returns>A solicitação da Web URI "pack://".</returns>
		// Token: 0x06000389 RID: 905 RVA: 0x00014970 File Offset: 0x00013D70
		[SecurityTreatAsSafe]
		[SecurityCritical]
		WebRequest IWebRequestCreate.Create(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriMustBeAbsolute"), "uri");
			}
			if (string.Compare(uri.Scheme, PackUriHelper.UriSchemePack, StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(SR.Get("UriSchemeMismatch", new object[]
				{
					PackUriHelper.UriSchemePack
				}), "uri");
			}
			Uri uri2;
			Uri uri3;
			PackUriHelper.ValidateAndGetPackUriComponents(uri, out uri2, out uri3);
			if (uri3 != null)
			{
				bool cachedPackageIsThreadSafe;
				Package package = PreloadedPackages.GetPackage(uri2, out cachedPackageIsThreadSafe);
				bool respectCachePolicy = false;
				if (package == null)
				{
					cachedPackageIsThreadSafe = false;
					respectCachePolicy = true;
					package = PackageStore.GetPackage(uri2);
				}
				if (package != null)
				{
					return new PackWebRequest(uri, uri2, uri3, package, respectCachePolicy, cachedPackageIsThreadSafe);
				}
			}
			return new PackWebRequest(uri, uri2, uri3);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00014A2C File Offset: 0x00013E2C
		[FriendAccessAllowed]
		internal static WebRequest CreateWebRequest(Uri uri)
		{
			if (string.Compare(uri.Scheme, PackUriHelper.UriSchemePack, StringComparison.Ordinal) == 0)
			{
				return ((IWebRequestCreate)PackWebRequestFactory._factorySingleton).Create(uri);
			}
			return WpfWebRequestHelper.CreateRequest(uri);
		}

		// Token: 0x0400049D RID: 1181
		private static PackWebRequestFactory _factorySingleton = new PackWebRequestFactory();
	}
}
