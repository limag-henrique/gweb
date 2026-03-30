using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace MS.Internal.Resources
{
	// Token: 0x020006EF RID: 1775
	internal static class ContentFileHelper
	{
		// Token: 0x06004C84 RID: 19588 RVA: 0x0012BFBC File Offset: 0x0012B3BC
		internal static bool IsContentFile(string partName)
		{
			if (ContentFileHelper._contentFiles == null)
			{
				ContentFileHelper._contentFiles = ContentFileHelper.GetContentFiles(BaseUriHelper.ResourceAssembly);
			}
			return ContentFileHelper._contentFiles != null && ContentFileHelper._contentFiles.Count > 0 && ContentFileHelper._contentFiles.ContainsKey(partName);
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0012C004 File Offset: 0x0012B404
		internal static Dictionary<string, string> GetContentFiles(Assembly asm)
		{
			Dictionary<string, string> dictionary = null;
			if (asm == null)
			{
				asm = BaseUriHelper.ResourceAssembly;
				if (asm == null)
				{
					return new Dictionary<string, string>();
				}
			}
			Attribute[] customAttributes = Attribute.GetCustomAttributes(asm, typeof(AssemblyAssociatedContentFileAttribute));
			if (customAttributes != null && customAttributes.Length != 0)
			{
				dictionary = new Dictionary<string, string>(customAttributes.Length, StringComparer.OrdinalIgnoreCase);
				foreach (AssemblyAssociatedContentFileAttribute assemblyAssociatedContentFileAttribute in customAttributes)
				{
					dictionary.Add(assemblyAssociatedContentFileAttribute.RelativeContentFilePath, null);
				}
			}
			return dictionary;
		}

		// Token: 0x0400213F RID: 8511
		private static Dictionary<string, string> _contentFiles;
	}
}
