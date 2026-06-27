using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000782 RID: 1922
	internal class FontSourceCollection : IFontSourceCollection, IEnumerable<IFontSource>, IEnumerable
	{
		// Token: 0x060050DC RID: 20700 RVA: 0x00143D1C File Offset: 0x0014311C
		[SecurityCritical]
		public FontSourceCollection(Uri folderUri, bool isWindowsFonts)
		{
			this.Initialize(folderUri, isWindowsFonts, false);
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x00143D38 File Offset: 0x00143138
		[SecurityCritical]
		public FontSourceCollection(Uri folderUri, bool isWindowsFonts, bool tryGetCompositeFontsOnly)
		{
			this.Initialize(folderUri, isWindowsFonts, tryGetCompositeFontsOnly);
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x00143D54 File Offset: 0x00143154
		[SecurityCritical]
		private void Initialize(Uri folderUri, bool isWindowsFonts, bool tryGetCompositeFontsOnly)
		{
			this._uri = folderUri;
			this._isWindowsFonts = isWindowsFonts;
			this._tryGetCompositeFontsOnly = tryGetCompositeFontsOnly;
			bool isComposite = false;
			if (Util.IsSupportedFontExtension(Util.GetUriExtension(this._uri), out isComposite) || !Util.IsEnumerableFontUriScheme(this._uri))
			{
				this._fontSources = new List<IFontSource>(1);
				this._fontSources.Add(new FontSource(this._uri, false, isComposite));
				return;
			}
			this.InitializeDirectoryProperties();
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x00143DCC File Offset: 0x001431CC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void InitializeDirectoryProperties()
		{
			this._isFileSystemFolder = false;
			if (this._uri.IsFile)
			{
				if (this._isWindowsFonts)
				{
					if (this._uri == Util.WindowsFontsUriObject)
					{
						this._isFileSystemFolder = true;
						return;
					}
					this._isFileSystemFolder = false;
					return;
				}
				else
				{
					string localPath = this._uri.LocalPath;
					this._isFileSystemFolder = (localPath[localPath.Length - 1] == Path.DirectorySeparatorChar);
				}
			}
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00143E3C File Offset: 0x0014323C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void SetFontSources()
		{
			if (this._fontSources != null)
			{
				return;
			}
			lock (this)
			{
				List<IFontSource> list;
				if (this._uri.IsFile)
				{
					bool flag2 = false;
					ICollection<string> collection;
					if (this._isFileSystemFolder)
					{
						if (this._isWindowsFonts)
						{
							PermissionSet permissionSet = new PermissionSet(null);
							permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Util.WindowsFontsUriObject.LocalPath));
							permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts"));
							permissionSet.Assert();
							try
							{
								if (this._tryGetCompositeFontsOnly)
								{
									collection = Directory.GetFiles(this._uri.LocalPath, "*" + Util.CompositeFontExtension);
									flag2 = true;
									goto IL_1DD;
								}
								Dictionary<string, object> dictionary = new Dictionary<string, object>(512, StringComparer.OrdinalIgnoreCase);
								using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts"))
								{
									Invariant.Assert(registryKey != null);
									foreach (string name in registryKey.GetValueNames())
									{
										string text = registryKey.GetValue(name) as string;
										if (text != null)
										{
											if (Path.GetFileName(text) == text)
											{
												text = Path.Combine(Util.WindowsFontsLocalPath, text);
											}
											dictionary[text] = null;
										}
									}
								}
								foreach (string key in Directory.GetFiles(this._uri.LocalPath))
								{
									dictionary[key] = null;
								}
								collection = dictionary.Keys;
								goto IL_1DD;
							}
							finally
							{
								if (this._isWindowsFonts)
								{
									CodeAccessPermission.RevertAssert();
								}
							}
						}
						if (this._tryGetCompositeFontsOnly)
						{
							collection = Directory.GetFiles(this._uri.LocalPath, "*" + Util.CompositeFontExtension);
							flag2 = true;
						}
						else
						{
							collection = Directory.GetFiles(this._uri.LocalPath);
						}
					}
					else
					{
						collection = new string[]
						{
							this._uri.LocalPath
						};
					}
					IL_1DD:
					list = new List<IFontSource>(collection.Count);
					if (flag2)
					{
						using (IEnumerator<string> enumerator = collection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string uriString = enumerator.Current;
								list.Add(new FontSource(new Uri(uriString, UriKind.Absolute), this._isWindowsFonts, true));
							}
							goto IL_34F;
						}
					}
					using (IEnumerator<string> enumerator2 = collection.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text2 = enumerator2.Current;
							bool isComposite;
							if (Util.IsSupportedFontExtension(Path.GetExtension(text2), out isComposite))
							{
								list.Add(new FontSource(new Uri(text2, UriKind.Absolute), this._isWindowsFonts, isComposite));
							}
						}
						goto IL_34F;
					}
				}
				List<string> list2 = FontResourceCache.LookupFolder(this._uri);
				if (list2 == null)
				{
					list = new List<IFontSource>(0);
				}
				else
				{
					list = new List<IFontSource>(list2.Count);
					foreach (string text3 in list2)
					{
						if (string.IsNullOrEmpty(text3))
						{
							bool isComposite2 = Util.IsCompositeFont(Path.GetExtension(this._uri.AbsoluteUri));
							list.Add(new FontSource(this._uri, this._isWindowsFonts, isComposite2));
						}
						else
						{
							bool isComposite2 = Util.IsCompositeFont(Path.GetExtension(text3));
							list.Add(new FontSource(new Uri(this._uri, text3), this._isWindowsFonts, isComposite2));
						}
					}
				}
				IL_34F:
				this._fontSources = list;
			}
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x00144244 File Offset: 0x00143644
		IEnumerator<IFontSource> IEnumerable<IFontSource>.GetEnumerator()
		{
			this.SetFontSources();
			return this._fontSources.GetEnumerator();
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x00144264 File Offset: 0x00143664
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.SetFontSources();
			return this._fontSources.GetEnumerator();
		}

		// Token: 0x040024D7 RID: 9431
		[SecurityCritical]
		private Uri _uri;

		// Token: 0x040024D8 RID: 9432
		[SecurityCritical]
		private bool _isWindowsFonts;

		// Token: 0x040024D9 RID: 9433
		private bool _isFileSystemFolder;

		// Token: 0x040024DA RID: 9434
		private volatile IList<IFontSource> _fontSources;

		// Token: 0x040024DB RID: 9435
		private bool _tryGetCompositeFontsOnly;

		// Token: 0x040024DC RID: 9436
		private const string InstalledWindowsFontsRegistryKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";

		// Token: 0x040024DD RID: 9437
		private const string InstalledWindowsFontsRegistryKeyFullPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Fonts";
	}
}
