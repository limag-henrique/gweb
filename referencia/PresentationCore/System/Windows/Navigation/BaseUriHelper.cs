using System;
using System.Globalization;
using System.IO.Packaging;
using System.Reflection;
using System.Security;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.AppModel;
using MS.Internal.IO.Packaging;
using MS.Internal.PresentationCore;

namespace System.Windows.Navigation
{
	/// <summary>Fornece um método para resolver URIs (Uniform Resource Identifiers) relativos em relação ao URI de base de um contêiner, como um <see cref="T:System.Windows.Controls.Frame" />.</summary>
	// Token: 0x02000204 RID: 516
	public static class BaseUriHelper
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x00032D34 File Offset: 0x00032134
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static BaseUriHelper()
		{
			BaseUriHelper._baseUri = new SecurityCriticalDataForSet<Uri>(BaseUriHelper._packAppBaseUri);
			PreloadedPackages.AddPackage(PackUriHelper.GetPackageUri(BaseUriHelper.SiteOfOriginBaseUri), new SiteOfOriginContainer(), true);
		}

		/// <summary>Obtém o valor do <see langword="BaseUri" /> para um <see cref="T:System.Windows.UIElement" /> especificado.</summary>
		/// <param name="element">O elemento do qual o valor da propriedade é lido.</param>
		/// <returns>O URI base de um determinado elemento.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.</exception>
		// Token: 0x06000D66 RID: 3430 RVA: 0x00032DB8 File Offset: 0x000321B8
		[SecurityCritical]
		public static Uri GetBaseUri(DependencyObject element)
		{
			Uri uri = BaseUriHelper.GetBaseUriCore(element);
			if (uri == null)
			{
				uri = BaseUriHelper.BaseUri;
			}
			else if (!uri.IsAbsoluteUri)
			{
				uri = new Uri(BaseUriHelper.BaseUri, uri);
			}
			return uri;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00032DF4 File Offset: 0x000321F4
		internal static Uri SiteOfOriginBaseUri
		{
			[FriendAccessAllowed]
			get
			{
				return BaseUriHelper._siteOfOriginBaseUri;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00032E08 File Offset: 0x00032208
		internal static Uri PackAppBaseUri
		{
			[FriendAccessAllowed]
			get
			{
				return BaseUriHelper._packAppBaseUri;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00032E1C File Offset: 0x0003221C
		internal static bool IsPackApplicationUri(Uri uri)
		{
			return uri.IsAbsoluteUri && SecurityHelper.AreStringTypesEqual(uri.Scheme, PackUriHelper.UriSchemePack) && SecurityHelper.AreStringTypesEqual(PackUriHelper.GetPackageUri(uri).GetComponents(UriComponents.AbsoluteUri, UriFormat.UriEscaped), "application:///");
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00032E60 File Offset: 0x00032260
		[FriendAccessAllowed]
		internal static void GetAssemblyAndPartNameFromPackAppUri(Uri uri, out Assembly assembly, out string partName)
		{
			Uri uri2 = new Uri(uri.AbsolutePath, UriKind.Relative);
			string text;
			string assemblyVersion;
			string assemblyKey;
			BaseUriHelper.GetAssemblyNameAndPart(uri2, out partName, out text, out assemblyVersion, out assemblyKey);
			if (string.IsNullOrEmpty(text))
			{
				assembly = BaseUriHelper.ResourceAssembly;
				return;
			}
			assembly = BaseUriHelper.GetLoadedAssembly(text, assemblyVersion, assemblyKey);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00032EA4 File Offset: 0x000322A4
		[FriendAccessAllowed]
		internal static Assembly GetLoadedAssembly(string assemblyName, string assemblyVersion, string assemblyKey)
		{
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			assemblyName2.CultureInfo = new CultureInfo(string.Empty);
			if (!string.IsNullOrEmpty(assemblyVersion))
			{
				assemblyName2.Version = new Version(assemblyVersion);
			}
			byte[] array = BaseUriHelper.ParseAssemblyKey(assemblyKey);
			if (array != null)
			{
				assemblyName2.SetPublicKeyToken(array);
			}
			Assembly assembly = SafeSecurityHelper.GetLoadedAssembly(assemblyName2);
			if (assembly == null)
			{
				assembly = Assembly.Load(assemblyName2);
			}
			return assembly;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00032F08 File Offset: 0x00032308
		[FriendAccessAllowed]
		internal static void GetAssemblyNameAndPart(Uri uri, out string partName, out string assemblyName, out string assemblyVersion, out string assemblyKey)
		{
			Invariant.Assert(uri != null && !uri.IsAbsoluteUri, "This method accepts relative uri only.");
			string text = uri.ToString();
			int num = 0;
			if (text[0] == '/')
			{
				num = 1;
			}
			partName = text.Substring(num);
			assemblyName = string.Empty;
			assemblyVersion = string.Empty;
			assemblyKey = string.Empty;
			int num2 = text.IndexOf('/', num);
			string text2 = string.Empty;
			bool flag = false;
			if (num2 > 0)
			{
				text2 = text.Substring(num, num2 - num);
				if (text2.EndsWith(";component", StringComparison.OrdinalIgnoreCase))
				{
					partName = text.Substring(num2 + 1);
					flag = true;
				}
			}
			if (flag)
			{
				string[] array = text2.Split(new char[]
				{
					';'
				});
				int num3 = array.Length;
				if (num3 > 4 || num3 < 2)
				{
					throw new UriFormatException(SR.Get("WrongFirstSegment"));
				}
				assemblyName = Uri.UnescapeDataString(array[0]);
				for (int i = 1; i < num3 - 1; i++)
				{
					if (array[i].StartsWith("v", StringComparison.OrdinalIgnoreCase))
					{
						if (!string.IsNullOrEmpty(assemblyVersion))
						{
							throw new UriFormatException(SR.Get("WrongFirstSegment"));
						}
						assemblyVersion = array[i].Substring(1);
					}
					else
					{
						if (!string.IsNullOrEmpty(assemblyKey))
						{
							throw new UriFormatException(SR.Get("WrongFirstSegment"));
						}
						assemblyKey = array[i];
					}
				}
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00033058 File Offset: 0x00032458
		[FriendAccessAllowed]
		internal static bool IsComponentEntryAssembly(string component)
		{
			if (component.EndsWith(";component", StringComparison.OrdinalIgnoreCase))
			{
				string[] array = component.Split(new char[]
				{
					';'
				});
				int num = array.Length;
				if (num >= 2 && num <= 4)
				{
					string strB = Uri.UnescapeDataString(array[0]);
					Assembly resourceAssembly = BaseUriHelper.ResourceAssembly;
					return resourceAssembly != null && string.Compare(SafeSecurityHelper.GetAssemblyPartialName(resourceAssembly), strB, StringComparison.OrdinalIgnoreCase) == 0;
				}
			}
			return false;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000330C0 File Offset: 0x000324C0
		[FriendAccessAllowed]
		internal static Uri GetResolvedUri(Uri baseUri, Uri orgUri)
		{
			return new Uri(baseUri, orgUri);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000330D4 File Offset: 0x000324D4
		[FriendAccessAllowed]
		internal static Uri MakeRelativeToSiteOfOriginIfPossible(Uri sUri)
		{
			if (Uri.Compare(sUri, BaseUriHelper.SiteOfOriginBaseUri, UriComponents.Scheme, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) == 0)
			{
				Uri uri;
				Uri uri2;
				PackUriHelper.ValidateAndGetPackUriComponents(sUri, out uri, out uri2);
				if (string.Compare(uri.GetComponents(UriComponents.AbsoluteUri, UriFormat.UriEscaped), "siteoforigin:///", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return new Uri(sUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)).MakeRelativeUri(sUri);
				}
			}
			return sUri;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00033128 File Offset: 0x00032528
		[FriendAccessAllowed]
		internal static Uri ConvertPackUriToAbsoluteExternallyVisibleUri(Uri packUri)
		{
			Invariant.Assert(packUri.IsAbsoluteUri && SecurityHelper.AreStringTypesEqual(packUri.Scheme, BaseUriHelper.PackAppBaseUri.Scheme));
			Uri uri = BaseUriHelper.MakeRelativeToSiteOfOriginIfPossible(packUri);
			if (!uri.IsAbsoluteUri)
			{
				return new Uri(SiteOfOriginContainer.SiteOfOrigin, uri);
			}
			throw new InvalidOperationException(SR.Get("CannotNavigateToApplicationResourcesInWebBrowser", new object[]
			{
				packUri
			}));
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00033190 File Offset: 0x00032590
		[FriendAccessAllowed]
		internal static Uri FixFileUri(Uri uri)
		{
			if (uri != null && uri.IsAbsoluteUri && SecurityHelper.AreStringTypesEqual(uri.Scheme, Uri.UriSchemeFile) && string.Compare(uri.OriginalString, 0, Uri.UriSchemeFile, 0, Uri.UriSchemeFile.Length, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return new Uri(uri.AbsoluteUri);
			}
			return uri;
		}

		/// <summary>Gets or sets the base uniform resource identifier (URI).</summary>
		/// <returns>The base uniform resource identifier (URI).</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000331EC File Offset: 0x000325EC
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00033204 File Offset: 0x00032604
		internal static Uri BaseUri
		{
			[FriendAccessAllowed]
			get
			{
				return BaseUriHelper._baseUri.Value;
			}
			[FriendAccessAllowed]
			[SecurityCritical]
			set
			{
				BaseUriHelper._baseUri.Value = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0003321C File Offset: 0x0003261C
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00033248 File Offset: 0x00032648
		internal static Assembly ResourceAssembly
		{
			get
			{
				if (BaseUriHelper._resourceAssembly == null)
				{
					BaseUriHelper._resourceAssembly = Assembly.GetEntryAssembly();
				}
				return BaseUriHelper._resourceAssembly;
			}
			[FriendAccessAllowed]
			set
			{
				BaseUriHelper._resourceAssembly = value;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003325C File Offset: 0x0003265C
		internal static Uri AppendAssemblyVersion(Uri uri, Assembly assemblyInfo)
		{
			Uri uri2 = null;
			Uri uri3 = null;
			AssemblyName assemblyName = new AssemblyName(assemblyInfo.FullName);
			Version version = assemblyName.Version;
			string value = (version != null) ? version.ToString() : null;
			if (uri != null && !string.IsNullOrEmpty(value))
			{
				if (uri.IsAbsoluteUri)
				{
					if (BaseUriHelper.IsPackApplicationUri(uri))
					{
						uri2 = new Uri(uri.AbsolutePath, UriKind.Relative);
						uri3 = new Uri(uri.GetLeftPart(UriPartial.Authority), UriKind.Absolute);
					}
				}
				else
				{
					uri2 = uri;
				}
				if (uri2 != null)
				{
					string value2;
					string text;
					string value3;
					string text2;
					BaseUriHelper.GetAssemblyNameAndPart(uri2, out value2, out text, out value3, out text2);
					bool flag = !string.IsNullOrEmpty(text2);
					if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(value3) && text.Equals(assemblyName.Name, StringComparison.Ordinal) && (!flag || BaseUriHelper.AssemblyMatchesKeyString(assemblyName, text2)))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append('/');
						stringBuilder.Append(text);
						stringBuilder.Append(';');
						stringBuilder.Append("v");
						stringBuilder.Append(value);
						if (flag)
						{
							stringBuilder.Append(';');
							stringBuilder.Append(text2);
						}
						stringBuilder.Append(";component");
						stringBuilder.Append('/');
						stringBuilder.Append(value2);
						string text3 = stringBuilder.ToString();
						if (uri3 != null)
						{
							return new Uri(uri3, text3);
						}
						return new Uri(text3, UriKind.Relative);
					}
				}
			}
			return null;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000333C8 File Offset: 0x000327C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Uri GetBaseUriCore(DependencyObject element)
		{
			Uri uri = null;
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			try
			{
				DependencyObject dependencyObject = element;
				while (dependencyObject != null)
				{
					uri = (dependencyObject.GetValue(BaseUriHelper.BaseUriProperty) as Uri);
					if (uri != null)
					{
						break;
					}
					IUriContext uriContext = dependencyObject as IUriContext;
					if (uriContext != null)
					{
						uri = uriContext.BaseUri;
						if (uri != null)
						{
							break;
						}
					}
					UIElement uielement = dependencyObject as UIElement;
					if (uielement != null)
					{
						dependencyObject = uielement.GetUIParent(true);
					}
					else
					{
						ContentElement contentElement = dependencyObject as ContentElement;
						if (contentElement != null)
						{
							dependencyObject = contentElement.Parent;
						}
						else
						{
							Visual visual = dependencyObject as Visual;
							if (visual == null)
							{
								break;
							}
							dependencyObject = VisualTreeHelper.GetParent(visual);
						}
					}
				}
			}
			finally
			{
				if (uri != null)
				{
					SecurityHelper.DemandUriDiscoveryPermission(uri);
				}
			}
			return uri;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00033498 File Offset: 0x00032898
		private static bool AssemblyMatchesKeyString(AssemblyName asmName, string assemblyKey)
		{
			byte[] curKeyToken = BaseUriHelper.ParseAssemblyKey(assemblyKey);
			byte[] publicKeyToken = asmName.GetPublicKeyToken();
			return SafeSecurityHelper.IsSameKeyToken(publicKeyToken, curKeyToken);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000334BC File Offset: 0x000328BC
		private static byte[] ParseAssemblyKey(string assemblyKey)
		{
			if (!string.IsNullOrEmpty(assemblyKey))
			{
				int num = assemblyKey.Length / 2;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					string s = assemblyKey.Substring(i * 2, 2);
					array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				}
				return array;
			}
			return null;
		}

		// Token: 0x04000806 RID: 2054
		private const string SOOBASE = "SiteOfOrigin://";

		// Token: 0x04000807 RID: 2055
		private static readonly Uri _siteOfOriginBaseUri = PackUriHelper.Create(new Uri("SiteOfOrigin://"));

		// Token: 0x04000808 RID: 2056
		private const string APPBASE = "application://";

		// Token: 0x04000809 RID: 2057
		private static readonly Uri _packAppBaseUri = PackUriHelper.Create(new Uri("application://"));

		// Token: 0x0400080A RID: 2058
		private static SecurityCriticalDataForSet<Uri> _baseUri;

		// Token: 0x0400080B RID: 2059
		private const string _packageApplicationBaseUriEscaped = "application:///";

		// Token: 0x0400080C RID: 2060
		private const string _packageSiteOfOriginBaseUriEscaped = "siteoforigin:///";

		/// <summary>Identifica a propriedade <see langword="BaseUri" /> anexada.</summary>
		// Token: 0x0400080D RID: 2061
		public static readonly DependencyProperty BaseUriProperty = DependencyProperty.RegisterAttached("BaseUri", typeof(Uri), typeof(BaseUriHelper), new PropertyMetadata(null));

		// Token: 0x0400080E RID: 2062
		private const string COMPONENT = ";component";

		// Token: 0x0400080F RID: 2063
		private const string VERSION = "v";

		// Token: 0x04000810 RID: 2064
		private const char COMPONENT_DELIMITER = ';';

		// Token: 0x04000811 RID: 2065
		private static Assembly _resourceAssembly;
	}
}
