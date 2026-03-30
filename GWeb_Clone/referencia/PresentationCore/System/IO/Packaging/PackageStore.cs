using System;
using System.Collections.Specialized;
using System.Security;
using System.Windows.Navigation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.IO.Packaging
{
	/// <summary>Representa uma coleção de instâncias <see cref="T:System.IO.Packaging.Package" /> específicas de aplicativos usadas em combinação com <see cref="T:System.IO.Packaging.PackWebRequest" />.</summary>
	// Token: 0x02000187 RID: 391
	[SecurityCritical(SecurityCriticalScope.Everything)]
	public static class PackageStore
	{
		/// <summary>Retorna o <see cref="T:System.IO.Packaging.Package" /> com o URI especificado do repositório.</summary>
		/// <param name="uri">O URI (Uniform Resource Identifier) do pacote a retornar.</param>
		/// <returns>O pacote com um <paramref name="packageUri" /> especificado; ou <see langword="null" />, se um pacote com o <paramref name="packageUri" /> especificado não estiver no repositório.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="packageUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="packageUri" /> é um pacote inválido de URI.</exception>
		// Token: 0x060003C0 RID: 960 RVA: 0x000157C8 File Offset: 0x00014BC8
		public static Package GetPackage(Uri uri)
		{
			PackageStore.ValidatePackageUri(uri);
			object globalLock = PackageStore._globalLock;
			Package result;
			lock (globalLock)
			{
				Package package = null;
				if (PackageStore._packages != null && PackageStore._packages.Contains(uri))
				{
					package = (Package)PackageStore._packages[uri];
					PackageStore.DemandSecurityPermissionIfCustomPackage(package);
				}
				result = package;
			}
			return result;
		}

		/// <summary>Adiciona um <see cref="T:System.IO.Packaging.Package" /> ao repositório.</summary>
		/// <param name="uri">A tecla URI do <paramref name="package" /> a ser comparada em um <see cref="T:System.IO.Packaging.PackWebRequest" />.</param>
		/// <param name="package">O pacote a adicionar ao repositório.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="package" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="packageUri" /> é um pacote inválido de URI.</exception>
		/// <exception cref="T:System.InvalidOperationException">Um pacote com o <paramref name="packageUri" /> especificado já está no repositório.</exception>
		// Token: 0x060003C1 RID: 961 RVA: 0x00015844 File Offset: 0x00014C44
		public static void AddPackage(Uri uri, Package package)
		{
			PackageStore.DemandSecurityPermissionIfCustomPackage(package);
			PackageStore.ValidatePackageUri(uri);
			Uri firstPackUri = PackUriHelper.Create(uri);
			if (PackUriHelper.ComparePackUri(firstPackUri, BaseUriHelper.PackAppBaseUri) == 0 || PackUriHelper.ComparePackUri(firstPackUri, BaseUriHelper.SiteOfOriginBaseUri) == 0)
			{
				throw new ArgumentException(SR.Get("NotAllowedPackageUri"), "uri");
			}
			if (package == null)
			{
				throw new ArgumentNullException("package");
			}
			object globalLock = PackageStore._globalLock;
			lock (globalLock)
			{
				if (PackageStore._packages == null)
				{
					PackageStore._packages = new HybridDictionary(2);
				}
				if (PackageStore._packages.Contains(uri))
				{
					throw new InvalidOperationException(SR.Get("PackageAlreadyExists"));
				}
				PackageStore._packages.Add(uri, package);
			}
		}

		/// <summary>Remove o <see cref="T:System.IO.Packaging.Package" /> com o URI especificado do repositório.</summary>
		/// <param name="uri">O URI (Uniform Resource Identifier) do pacote a remover.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="packageUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="packageUri" /> é um pacote inválido de URI.</exception>
		// Token: 0x060003C2 RID: 962 RVA: 0x00015914 File Offset: 0x00014D14
		public static void RemovePackage(Uri uri)
		{
			PackageStore.ValidatePackageUri(uri);
			object globalLock = PackageStore._globalLock;
			lock (globalLock)
			{
				if (PackageStore._packages != null)
				{
					PackageStore.DemandSecurityPermissionIfCustomPackage((Package)PackageStore._packages[uri]);
					PackageStore._packages.Remove(uri);
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00015988 File Offset: 0x00014D88
		private static void ValidatePackageUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriMustBeAbsolute"), "uri");
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000159C8 File Offset: 0x00014DC8
		private static void DemandSecurityPermissionIfCustomPackage(Package package)
		{
			if (package != null && package.GetType() != typeof(ZipPackage))
			{
				SecurityHelper.DemandEnvironmentPermission();
			}
		}

		// Token: 0x040004BA RID: 1210
		private static HybridDictionary _packages;

		// Token: 0x040004BB RID: 1211
		private static object _globalLock = new object();
	}
}
