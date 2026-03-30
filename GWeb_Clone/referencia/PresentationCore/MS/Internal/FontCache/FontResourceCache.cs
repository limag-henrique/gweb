using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Navigation;
using MS.Internal.Resources;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077E RID: 1918
	internal static class FontResourceCache
	{
		// Token: 0x060050C1 RID: 20673 RVA: 0x00143598 File Offset: 0x00142998
		private static void ConstructFontResourceCache(Assembly entryAssembly, Dictionary<string, List<string>> folderResourceMap)
		{
			Dictionary<string, string> contentFiles = ContentFileHelper.GetContentFiles(entryAssembly);
			if (contentFiles != null)
			{
				foreach (string resourceFullName in contentFiles.Keys)
				{
					FontResourceCache.AddResourceToFolderMap(folderResourceMap, resourceFullName);
				}
			}
			IList resourceList = new ResourceManagerWrapper(entryAssembly).ResourceList;
			if (resourceList != null)
			{
				foreach (object obj in resourceList)
				{
					string resourceFullName2 = (string)obj;
					FontResourceCache.AddResourceToFolderMap(folderResourceMap, resourceFullName2);
				}
			}
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x00143664 File Offset: 0x00142A64
		internal static List<string> LookupFolder(Uri uri)
		{
			bool flag = FontResourceCache.IsFolderUri(uri);
			if (flag)
			{
				uri = new Uri(uri, "X");
			}
			Assembly assembly;
			string text;
			BaseUriHelper.GetAssemblyAndPartNameFromPackAppUri(uri, out assembly, out text);
			if (assembly == null)
			{
				return null;
			}
			if (flag)
			{
				text = text.Substring(0, text.Length - "X".Length);
			}
			Dictionary<Assembly, Dictionary<string, List<string>>> assemblyCaches = FontResourceCache._assemblyCaches;
			Dictionary<string, List<string>> dictionary;
			lock (assemblyCaches)
			{
				if (!FontResourceCache._assemblyCaches.TryGetValue(assembly, out dictionary))
				{
					dictionary = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
					FontResourceCache.ConstructFontResourceCache(assembly, dictionary);
					FontResourceCache._assemblyCaches.Add(assembly, dictionary);
				}
			}
			List<string> result;
			dictionary.TryGetValue(text, out result);
			return result;
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00143730 File Offset: 0x00142B30
		private static bool IsFolderUri(Uri uri)
		{
			string components = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
			return components.Length == 0 || components[components.Length - 1] == '/';
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00143764 File Offset: 0x00142B64
		private static void AddResourceToFolderMap(Dictionary<string, List<string>> folderResourceMap, string resourceFullName)
		{
			int num = resourceFullName.LastIndexOf('/');
			string key;
			string text;
			if (num == -1)
			{
				key = string.Empty;
				text = resourceFullName;
			}
			else
			{
				key = resourceFullName.Substring(0, num + 1);
				text = resourceFullName.Substring(num + 1);
			}
			string extension = Path.GetExtension(text);
			bool flag;
			if (!Util.IsSupportedFontExtension(extension, out flag))
			{
				return;
			}
			if (!folderResourceMap.ContainsKey(key))
			{
				folderResourceMap[key] = new List<string>(1);
			}
			folderResourceMap[key].Add(text);
			folderResourceMap[resourceFullName] = new List<string>(1);
			folderResourceMap[resourceFullName].Add(string.Empty);
		}

		// Token: 0x040024CF RID: 9423
		private const string FakeFileName = "X";

		// Token: 0x040024D0 RID: 9424
		private static Dictionary<Assembly, Dictionary<string, List<string>>> _assemblyCaches = new Dictionary<Assembly, Dictionary<string, List<string>>>(1);
	}
}
