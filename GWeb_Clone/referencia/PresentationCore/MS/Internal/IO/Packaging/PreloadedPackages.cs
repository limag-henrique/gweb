using System;
using System.Collections.Specialized;
using System.IO.Packaging;
using System.Security;
using MS.Internal.PresentationCore;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007B2 RID: 1970
	[FriendAccessAllowed]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal static class PreloadedPackages
	{
		// Token: 0x060052E8 RID: 21224 RVA: 0x0014B590 File Offset: 0x0014A990
		internal static Package GetPackage(Uri uri)
		{
			bool flag;
			return PreloadedPackages.GetPackage(uri, out flag);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0014B5A8 File Offset: 0x0014A9A8
		internal static Package GetPackage(Uri uri, out bool threadSafe)
		{
			PreloadedPackages.ValidateUriKey(uri);
			object globalLock = PreloadedPackages._globalLock;
			Package result;
			lock (globalLock)
			{
				Package package = null;
				threadSafe = false;
				if (PreloadedPackages._packagePairs != null)
				{
					PreloadedPackages.PackageThreadSafePair packageThreadSafePair = PreloadedPackages._packagePairs[uri] as PreloadedPackages.PackageThreadSafePair;
					if (packageThreadSafePair != null)
					{
						package = packageThreadSafePair.Package;
						threadSafe = packageThreadSafePair.ThreadSafe;
					}
				}
				result = package;
			}
			return result;
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0014B628 File Offset: 0x0014AA28
		internal static void AddPackage(Uri uri, Package package)
		{
			PreloadedPackages.AddPackage(uri, package, false);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0014B640 File Offset: 0x0014AA40
		internal static void AddPackage(Uri uri, Package package, bool threadSafe)
		{
			PreloadedPackages.ValidateUriKey(uri);
			object globalLock = PreloadedPackages._globalLock;
			lock (globalLock)
			{
				if (PreloadedPackages._packagePairs == null)
				{
					PreloadedPackages._packagePairs = new HybridDictionary(3);
				}
				PreloadedPackages._packagePairs.Add(uri, new PreloadedPackages.PackageThreadSafePair(package, threadSafe));
			}
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0014B6B0 File Offset: 0x0014AAB0
		internal static void RemovePackage(Uri uri)
		{
			PreloadedPackages.ValidateUriKey(uri);
			object globalLock = PreloadedPackages._globalLock;
			lock (globalLock)
			{
				if (PreloadedPackages._packagePairs != null)
				{
					PreloadedPackages._packagePairs.Remove(uri);
				}
			}
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0014B710 File Offset: 0x0014AB10
		internal static void Clear()
		{
			object globalLock = PreloadedPackages._globalLock;
			lock (globalLock)
			{
				PreloadedPackages._packagePairs = null;
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0014B75C File Offset: 0x0014AB5C
		private static void ValidateUriKey(Uri uri)
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

		// Token: 0x0400257B RID: 9595
		private static HybridDictionary _packagePairs;

		// Token: 0x0400257C RID: 9596
		private static object _globalLock = new object();

		// Token: 0x02000A00 RID: 2560
		private class PackageThreadSafePair
		{
			// Token: 0x06005BEE RID: 23534 RVA: 0x001714B4 File Offset: 0x001708B4
			internal PackageThreadSafePair(Package package, bool threadSafe)
			{
				Invariant.Assert(package != null);
				this._package = package;
				this._threadSafe = threadSafe;
			}

			// Token: 0x170012CE RID: 4814
			// (get) Token: 0x06005BEF RID: 23535 RVA: 0x001714E0 File Offset: 0x001708E0
			internal Package Package
			{
				get
				{
					return this._package;
				}
			}

			// Token: 0x170012CF RID: 4815
			// (get) Token: 0x06005BF0 RID: 23536 RVA: 0x001714F4 File Offset: 0x001708F4
			internal bool ThreadSafe
			{
				get
				{
					return this._threadSafe;
				}
			}

			// Token: 0x04002F23 RID: 12067
			private readonly Package _package;

			// Token: 0x04002F24 RID: 12068
			private readonly bool _threadSafe;
		}
	}
}
