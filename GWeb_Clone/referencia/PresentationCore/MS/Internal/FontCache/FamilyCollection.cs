using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000773 RID: 1907
	[FriendAccessAllowed]
	internal class FamilyCollection
	{
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x001415F0 File Offset: 0x001409F0
		internal static string SxSFontsLocation
		{
			[SecurityCritical]
			get
			{
				if (FamilyCollection._sxsFontsLocation == string.Empty)
				{
					object staticLock = FamilyCollection._staticLock;
					lock (staticLock)
					{
						if (FamilyCollection._sxsFontsLocation == string.Empty)
						{
							FamilyCollection._sxsFontsLocation = FamilyCollection.GetSystemSxSFontsLocation();
						}
					}
				}
				return FamilyCollection._sxsFontsLocation;
			}
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x00141668 File Offset: 0x00140A68
		[SecurityCritical]
		private static string GetSystemSxSFontsLocation()
		{
			RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Net Framework Setup\\NDP\\v4\\Client\\");
			registryPermission.Assert();
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Net Framework Setup\\NDP\\v4\\Client\\"))
				{
					Invariant.Assert(registryKey != null);
					string text = registryKey.GetValue("InstallPath") as string;
					FamilyCollection.CheckFrameworkInstallPath(text);
					result = Path.Combine(text, "WPF\\");
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0014170C File Offset: 0x00140B0C
		private static void CheckFrameworkInstallPath(string frameworkInstallPath)
		{
			if (frameworkInstallPath == null)
			{
				throw new ArgumentNullException("frameworkInstallPath", SR.Get("FamilyCollection_CannotFindCompositeFontsLocation"));
			}
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x00141734 File Offset: 0x00140B34
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static List<CompositeFontFamily> GetCompositeFontList(FontSourceCollection fontSourceCollection)
		{
			List<CompositeFontFamily> list = new List<CompositeFontFamily>();
			foreach (IFontSource fontSource in ((IEnumerable<IFontSource>)fontSourceCollection))
			{
				FontSource fontSource2 = (FontSource)fontSource;
				if (fontSource2.IsComposite)
				{
					CompositeFontInfo fontInfo = CompositeFontParser.LoadXml(fontSource2.GetStream());
					CompositeFontFamily item = new CompositeFontFamily(fontInfo);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x001417B4 File Offset: 0x00140BB4
		private bool UseSystemFonts
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._fontCollection == DWriteFactory.SystemFontCollection;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x001417D0 File Offset: 0x00140BD0
		private IList<CompositeFontFamily> UserCompositeFonts
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (this._userCompositeFonts == null)
				{
					this._userCompositeFonts = FamilyCollection.GetCompositeFontList(new FontSourceCollection(this._folderUri, false, true));
				}
				return this._userCompositeFonts;
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00141804 File Offset: 0x00140C04
		[SecurityCritical]
		private FamilyCollection(Uri folderUri, FontCollection fontCollection)
		{
			this._folderUri = folderUri;
			this._fontCollection = fontCollection;
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00141828 File Offset: 0x00140C28
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static FamilyCollection FromUri(Uri folderUri)
		{
			SecurityHelper.DemandUriReadPermission(folderUri);
			return new FamilyCollection(folderUri, DWriteFactory.GetFontCollectionFromFolder(folderUri));
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00141848 File Offset: 0x00140C48
		[SecurityCritical]
		internal static FamilyCollection FromWindowsFonts(Uri folderUri)
		{
			return new FamilyCollection(folderUri, DWriteFactory.SystemFontCollection);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00141884 File Offset: 0x00140C84
		[SecurityCritical]
		internal IFontFamily LookupFamily(string familyName, ref System.Windows.FontStyle fontStyle, ref System.Windows.FontWeight fontWeight, ref System.Windows.FontStretch fontStretch)
		{
			if (familyName == null || familyName.Length == 0)
			{
				return null;
			}
			familyName = familyName.Trim();
			if (this.UseSystemFonts)
			{
				CompositeFontFamily compositeFontFamily = FamilyCollection.SystemCompositeFonts.FindFamily(familyName);
				if (compositeFontFamily != null)
				{
					return compositeFontFamily;
				}
			}
			MS.Internal.Text.TextInterface.FontFamily fontFamily = this._fontCollection[familyName];
			if (fontFamily == null)
			{
				if (!this.UseSystemFonts)
				{
					CompositeFontFamily compositeFontFamily2 = this.LookUpUserCompositeFamily(familyName);
					if (compositeFontFamily2 != null)
					{
						return compositeFontFamily2;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				do
				{
					int num = familyName.LastIndexOf(' ');
					if (num < 0)
					{
						break;
					}
					stringBuilder.Insert(0, familyName.Substring(num));
					familyName = familyName.Substring(0, num);
					fontFamily = this._fontCollection[familyName];
				}
				while (fontFamily == null);
				if (fontFamily == null)
				{
					return null;
				}
				if (stringBuilder.Length > 0)
				{
					Font fontFromFamily = FamilyCollection.GetFontFromFamily(fontFamily, stringBuilder.ToString(1, stringBuilder.Length - 1));
					if (fontFromFamily != null)
					{
						fontStyle = new System.Windows.FontStyle((int)fontFromFamily.Style);
						fontWeight = new System.Windows.FontWeight((int)fontFromFamily.Weight);
						fontStretch = new System.Windows.FontStretch((int)fontFromFamily.Stretch);
					}
				}
			}
			if (this.UseSystemFonts && FamilyCollection.LegacyArabicFonts.UsePrivateFontCollectionForLegacyArabicFonts && FamilyCollection.LegacyArabicFonts.IsLegacyArabicFont(familyName))
			{
				fontFamily = FamilyCollection.LegacyArabicFonts.LegacyArabicFontCollection[familyName];
				if (fontFamily == null)
				{
					return FamilyCollection.SystemCompositeFonts.GetFallbackFontForArabicLegacyFonts();
				}
			}
			return new PhysicalFontFamily(fontFamily);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x001419B4 File Offset: 0x00140DB4
		private CompositeFontFamily LookUpUserCompositeFamily(string familyName)
		{
			if (this.UserCompositeFonts != null)
			{
				foreach (CompositeFontFamily compositeFontFamily in this.UserCompositeFonts)
				{
					foreach (KeyValuePair<XmlLanguage, string> keyValuePair in compositeFontFamily.FamilyNames)
					{
						if (string.Compare(keyValuePair.Value, familyName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return compositeFontFamily;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00141A68 File Offset: 0x00140E68
		[SecurityCritical]
		private static Font GetFontFromFamily(MS.Internal.Text.TextInterface.FontFamily fontFamily, string faceName)
		{
			faceName = faceName.ToUpper(CultureInfo.InvariantCulture);
			foreach (Font font in fontFamily)
			{
				foreach (KeyValuePair<CultureInfo, string> keyValuePair in font.FaceNames)
				{
					string a = keyValuePair.Value.ToUpper(CultureInfo.InvariantCulture);
					if (a == faceName)
					{
						return font;
					}
				}
			}
			Dictionary<string, Font> dictionary = new Dictionary<string, Font>();
			foreach (Font font2 in fontFamily)
			{
				foreach (KeyValuePair<CultureInfo, string> keyValuePair2 in font2.FaceNames)
				{
					string key = keyValuePair2.Value.ToUpper(CultureInfo.InvariantCulture);
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, font2);
					}
				}
			}
			Font result = null;
			for (int i = faceName.LastIndexOf(' '); i > 0; i = faceName.LastIndexOf(' '))
			{
				faceName = faceName.Substring(0, i);
				if (dictionary.TryGetValue(faceName, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00141C18 File Offset: 0x00141018
		private IEnumerable<MS.Internal.Text.TextInterface.FontFamily> GetPhysicalFontFamilies()
		{
			return new FamilyCollection.FamilyEnumerator(this._fontCollection);
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00141C38 File Offset: 0x00141038
		internal System.Windows.Media.FontFamily[] GetFontFamilies(Uri fontFamilyBaseUri, string fontFamilyLocationReference)
		{
			System.Windows.Media.FontFamily[] array = new System.Windows.Media.FontFamily[this.FamilyCount];
			int num = 0;
			foreach (MS.Internal.Text.TextInterface.FontFamily fontFamily in this.GetPhysicalFontFamilies())
			{
				string fontFamilyReference = Util.ConvertFamilyNameAndLocationToFontFamilyReference(fontFamily.OrdinalName, fontFamilyLocationReference);
				string familyName = Util.ConvertFontFamilyReferenceToFriendlyName(fontFamilyReference);
				array[num++] = new System.Windows.Media.FontFamily(fontFamilyBaseUri, familyName);
			}
			if (this.UseSystemFonts)
			{
				for (int i = 0; i < 4; i++)
				{
					System.Windows.Media.FontFamily fontFamily2 = this.CreateFontFamily(FamilyCollection.SystemCompositeFonts.GetCompositeFontFamilyAtIndex(i), fontFamilyBaseUri, fontFamilyLocationReference);
					if (fontFamily2 != null)
					{
						array[num++] = fontFamily2;
					}
				}
			}
			else
			{
				foreach (CompositeFontFamily compositeFontFamily in this.UserCompositeFonts)
				{
					System.Windows.Media.FontFamily fontFamily2 = this.CreateFontFamily(compositeFontFamily, fontFamilyBaseUri, fontFamilyLocationReference);
					if (fontFamily2 != null)
					{
						array[num++] = fontFamily2;
					}
				}
			}
			return array;
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x00141D54 File Offset: 0x00141154
		private System.Windows.Media.FontFamily CreateFontFamily(CompositeFontFamily compositeFontFamily, Uri fontFamilyBaseUri, string fontFamilyLocationReference)
		{
			IEnumerator<string> enumerator = ((IFontFamily)compositeFontFamily).Names.Values.GetEnumerator();
			if (enumerator.MoveNext())
			{
				string familyName = enumerator.Current;
				string fontFamilyReference = Util.ConvertFamilyNameAndLocationToFontFamilyReference(familyName, fontFamilyLocationReference);
				string familyName2 = Util.ConvertFontFamilyReferenceToFriendlyName(fontFamilyReference);
				return new System.Windows.Media.FontFamily(fontFamilyBaseUri, familyName2);
			}
			return null;
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x00141DA0 File Offset: 0x001411A0
		internal uint FamilyCount
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._fontCollection.FamilyCount + (this.UseSystemFonts ? 4U : checked((uint)this.UserCompositeFonts.Count));
			}
		}

		// Token: 0x0400248F RID: 9359
		private FontCollection _fontCollection;

		// Token: 0x04002490 RID: 9360
		private Uri _folderUri;

		// Token: 0x04002491 RID: 9361
		private List<CompositeFontFamily> _userCompositeFonts;

		// Token: 0x04002492 RID: 9362
		private const string _sxsFontsRelativeLocation = "WPF\\";

		// Token: 0x04002493 RID: 9363
		private static object _staticLock = new object();

		// Token: 0x04002494 RID: 9364
		[SecurityCritical]
		private static string _sxsFontsLocation = string.Empty;

		// Token: 0x020009EF RID: 2543
		private static class LegacyArabicFonts
		{
			// Token: 0x170012BF RID: 4799
			// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00170908 File Offset: 0x0016FD08
			internal static FontCollection LegacyArabicFontCollection
			{
				[SecurityTreatAsSafe]
				[SecurityCritical]
				get
				{
					if (FamilyCollection.LegacyArabicFonts._legacyArabicFontCollection == null)
					{
						object staticLock = FamilyCollection.LegacyArabicFonts._staticLock;
						lock (staticLock)
						{
							if (FamilyCollection.LegacyArabicFonts._legacyArabicFontCollection == null)
							{
								Uri uri = new Uri(FamilyCollection.SxSFontsLocation);
								SecurityHelper.CreateUriDiscoveryPermission(uri).Assert();
								try
								{
									FamilyCollection.LegacyArabicFonts._legacyArabicFontCollection = DWriteFactory.GetFontCollectionFromFolder(uri);
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
							}
						}
					}
					return FamilyCollection.LegacyArabicFonts._legacyArabicFontCollection;
				}
			}

			// Token: 0x06005BB1 RID: 23473 RVA: 0x001709A4 File Offset: 0x0016FDA4
			internal static bool IsLegacyArabicFont(string familyName)
			{
				for (int i = 0; i < FamilyCollection.LegacyArabicFonts._legacyArabicFonts.Length; i++)
				{
					if (string.Compare(familyName, FamilyCollection.LegacyArabicFonts._legacyArabicFonts[i], StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x170012C0 RID: 4800
			// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x001709D8 File Offset: 0x0016FDD8
			internal static bool UsePrivateFontCollectionForLegacyArabicFonts
			{
				get
				{
					if (!FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionIsInitialized)
					{
						object staticLock = FamilyCollection.LegacyArabicFonts._staticLock;
						lock (staticLock)
						{
							if (!FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionIsInitialized)
							{
								try
								{
									OperatingSystem osversion = Environment.OSVersion;
									FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionForLegacyArabicFonts = (osversion.Version.Major < 6 || (osversion.Version.Major == 6 && osversion.Version.Minor == 0));
								}
								catch (InvalidOperationException)
								{
									FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionForLegacyArabicFonts = true;
								}
								FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionIsInitialized = true;
							}
						}
					}
					return FamilyCollection.LegacyArabicFonts._usePrivateFontCollectionForLegacyArabicFonts;
				}
			}

			// Token: 0x04002EFB RID: 12027
			private static bool _usePrivateFontCollectionIsInitialized = false;

			// Token: 0x04002EFC RID: 12028
			private static object _staticLock = new object();

			// Token: 0x04002EFD RID: 12029
			private static bool _usePrivateFontCollectionForLegacyArabicFonts;

			// Token: 0x04002EFE RID: 12030
			private static readonly string[] _legacyArabicFonts = new string[]
			{
				"Traditional Arabic",
				"Andalus",
				"Simplified Arabic",
				"Simplified Arabic Fixed"
			};

			// Token: 0x04002EFF RID: 12031
			private static FontCollection _legacyArabicFontCollection;
		}

		// Token: 0x020009F0 RID: 2544
		private static class SystemCompositeFonts
		{
			// Token: 0x06005BB4 RID: 23476 RVA: 0x00170B0C File Offset: 0x0016FF0C
			internal static CompositeFontFamily GetFallbackFontForArabicLegacyFonts()
			{
				return FamilyCollection.SystemCompositeFonts.GetCompositeFontFamilyAtIndex(1);
			}

			// Token: 0x06005BB5 RID: 23477 RVA: 0x00170B20 File Offset: 0x0016FF20
			internal static CompositeFontFamily FindFamily(string familyName)
			{
				int indexOfFamily = FamilyCollection.SystemCompositeFonts.GetIndexOfFamily(familyName);
				if (indexOfFamily >= 0)
				{
					return FamilyCollection.SystemCompositeFonts.GetCompositeFontFamilyAtIndex(indexOfFamily);
				}
				return null;
			}

			// Token: 0x06005BB6 RID: 23478 RVA: 0x00170B40 File Offset: 0x0016FF40
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal static CompositeFontFamily GetCompositeFontFamilyAtIndex(int index)
			{
				if (FamilyCollection.SystemCompositeFonts._systemCompositeFonts[index] == null)
				{
					object systemCompositeFontsLock = FamilyCollection.SystemCompositeFonts._systemCompositeFontsLock;
					lock (systemCompositeFontsLock)
					{
						if (FamilyCollection.SystemCompositeFonts._systemCompositeFonts[index] == null)
						{
							FontSource fontSource = new FontSource(new Uri(FamilyCollection.SxSFontsLocation + FamilyCollection.SystemCompositeFonts._systemCompositeFontsFileNames[index] + Util.CompositeFontExtension, UriKind.Absolute), true, true);
							CompositeFontInfo fontInfo = CompositeFontParser.LoadXml(fontSource.GetStream());
							FamilyCollection.SystemCompositeFonts._systemCompositeFonts[index] = new CompositeFontFamily(fontInfo);
						}
					}
				}
				return FamilyCollection.SystemCompositeFonts._systemCompositeFonts[index];
			}

			// Token: 0x06005BB7 RID: 23479 RVA: 0x00170BDC File Offset: 0x0016FFDC
			private static int GetIndexOfFamily(string familyName)
			{
				for (int i = 0; i < FamilyCollection.SystemCompositeFonts._systemCompositeFontsNames.Length; i++)
				{
					if (string.Compare(FamilyCollection.SystemCompositeFonts._systemCompositeFontsNames[i], familyName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x04002F00 RID: 12032
			internal const int NumOfSystemCompositeFonts = 4;

			// Token: 0x04002F01 RID: 12033
			private static object _systemCompositeFontsLock = new object();

			// Token: 0x04002F02 RID: 12034
			private static readonly string[] _systemCompositeFontsNames = new string[]
			{
				"Global User Interface",
				"Global Monospace",
				"Global Sans Serif",
				"Global Serif"
			};

			// Token: 0x04002F03 RID: 12035
			private static readonly string[] _systemCompositeFontsFileNames = new string[]
			{
				"GlobalUserInterface",
				"GlobalMonospace",
				"GlobalSansSerif",
				"GlobalSerif"
			};

			// Token: 0x04002F04 RID: 12036
			private static CompositeFontFamily[] _systemCompositeFonts = new CompositeFontFamily[4];
		}

		// Token: 0x020009F1 RID: 2545
		private struct FamilyEnumerator : IEnumerator<MS.Internal.Text.TextInterface.FontFamily>, IDisposable, IEnumerator, IEnumerable<MS.Internal.Text.TextInterface.FontFamily>, IEnumerable
		{
			// Token: 0x06005BB8 RID: 23480 RVA: 0x00170C10 File Offset: 0x00170010
			internal FamilyEnumerator(FontCollection fontCollection)
			{
				this._fontCollection = fontCollection;
				this._currentFamily = 0U;
				this._firstEnumeration = true;
				this._familyCount = fontCollection.FamilyCount;
			}

			// Token: 0x06005BB9 RID: 23481 RVA: 0x00170C40 File Offset: 0x00170040
			public bool MoveNext()
			{
				if (this._firstEnumeration)
				{
					this._firstEnumeration = false;
				}
				else
				{
					this._currentFamily += 1U;
				}
				if (this._currentFamily >= this._familyCount)
				{
					this._currentFamily = this._familyCount;
					return false;
				}
				return true;
			}

			// Token: 0x170012C1 RID: 4801
			// (get) Token: 0x06005BBA RID: 23482 RVA: 0x00170C8C File Offset: 0x0017008C
			MS.Internal.Text.TextInterface.FontFamily IEnumerator<MS.Internal.Text.TextInterface.FontFamily>.Current
			{
				[SecurityCritical]
				[SecurityTreatAsSafe]
				get
				{
					if (this._currentFamily < 0U || this._currentFamily >= this._familyCount)
					{
						throw new InvalidOperationException();
					}
					return this._fontCollection[this._currentFamily];
				}
			}

			// Token: 0x170012C2 RID: 4802
			// (get) Token: 0x06005BBB RID: 23483 RVA: 0x00170CC8 File Offset: 0x001700C8
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator<MS.Internal.Text.TextInterface.FontFamily>)this).Current;
				}
			}

			// Token: 0x06005BBC RID: 23484 RVA: 0x00170CE8 File Offset: 0x001700E8
			public void Reset()
			{
				this._currentFamily = 0U;
				this._firstEnumeration = true;
			}

			// Token: 0x06005BBD RID: 23485 RVA: 0x00170D04 File Offset: 0x00170104
			public void Dispose()
			{
			}

			// Token: 0x06005BBE RID: 23486 RVA: 0x00170D14 File Offset: 0x00170114
			IEnumerator<MS.Internal.Text.TextInterface.FontFamily> IEnumerable<MS.Internal.Text.TextInterface.FontFamily>.GetEnumerator()
			{
				return this;
			}

			// Token: 0x06005BBF RID: 23487 RVA: 0x00170D2C File Offset: 0x0017012C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<MS.Internal.Text.TextInterface.FontFamily>)this).GetEnumerator();
			}

			// Token: 0x04002F05 RID: 12037
			private uint _familyCount;

			// Token: 0x04002F06 RID: 12038
			private FontCollection _fontCollection;

			// Token: 0x04002F07 RID: 12039
			private bool _firstEnumeration;

			// Token: 0x04002F08 RID: 12040
			private uint _currentFamily;
		}
	}
}
