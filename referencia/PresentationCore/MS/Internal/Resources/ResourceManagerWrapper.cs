using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using MS.Internal.PresentationCore;

namespace MS.Internal.Resources
{
	// Token: 0x020006EE RID: 1774
	internal class ResourceManagerWrapper
	{
		// Token: 0x06004C7B RID: 19579 RVA: 0x0012BD38 File Offset: 0x0012B138
		internal ResourceManagerWrapper(Assembly assembly)
		{
			this._assembly = assembly;
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0012BD54 File Offset: 0x0012B154
		internal Stream GetStream(string name)
		{
			Stream stream = null;
			try
			{
				stream = this.ResourceManager.GetStream(name, CultureInfo.CurrentUICulture);
			}
			catch (SystemException ex)
			{
				if (!(ex is MissingManifestResourceException) && !(ex is MissingSatelliteAssemblyException))
				{
					throw;
				}
			}
			if (stream == null && this.ResourceSet != null)
			{
				try
				{
					stream = (this.ResourceSet.GetObject(name) as Stream);
				}
				catch (SystemException ex2)
				{
					if (!(ex2 is MissingManifestResourceException))
					{
						throw;
					}
				}
			}
			return stream;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x0012BDF8 File Offset: 0x0012B1F8
		// (set) Token: 0x06004C7E RID: 19582 RVA: 0x0012BE0C File Offset: 0x0012B20C
		internal Assembly Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
				this._resourceManager = null;
				this._resourceSet = null;
				this._resourceList = null;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x0012BE38 File Offset: 0x0012B238
		internal IList ResourceList
		{
			get
			{
				if (this._resourceList == null)
				{
					this._resourceList = new ArrayList();
					if (this.ResourceManager != null)
					{
						CultureInfo neutralResourcesLanguage = this.GetNeutralResourcesLanguage();
						ResourceSet resourceSet = this.ResourceManager.GetResourceSet(neutralResourcesLanguage, true, false);
						if (resourceSet != null)
						{
							this.AddResourceNameToList(resourceSet, ref this._resourceList);
							resourceSet.Close();
						}
					}
					if (this.ResourceSet != null)
					{
						this.AddResourceNameToList(this.ResourceSet, ref this._resourceList);
					}
				}
				return this._resourceList;
			}
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0012BEAC File Offset: 0x0012B2AC
		private CultureInfo GetNeutralResourcesLanguage()
		{
			CultureInfo result = CultureInfo.InvariantCulture;
			NeutralResourcesLanguageAttribute neutralResourcesLanguageAttribute = Attribute.GetCustomAttribute(this._assembly, typeof(NeutralResourcesLanguageAttribute)) as NeutralResourcesLanguageAttribute;
			if (neutralResourcesLanguageAttribute != null)
			{
				result = new CultureInfo(neutralResourcesLanguageAttribute.CultureName);
			}
			return result;
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0012BEEC File Offset: 0x0012B2EC
		private void AddResourceNameToList(ResourceSet rs, ref ArrayList resourceList)
		{
			IDictionaryEnumerator enumerator = rs.GetEnumerator();
			if (enumerator != null)
			{
				while (enumerator.MoveNext())
				{
					string value = enumerator.Key as string;
					resourceList.Add(value);
				}
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x0012BF24 File Offset: 0x0012B324
		private ResourceSet ResourceSet
		{
			get
			{
				if (this._resourceSet == null)
				{
					string baseName = SafeSecurityHelper.GetAssemblyPartialName(this._assembly) + ".unlocalizable.g";
					ResourceManager resourceManager = new ResourceManager(baseName, this._assembly);
					this._resourceSet = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
				}
				return this._resourceSet;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x0012BF78 File Offset: 0x0012B378
		private ResourceManager ResourceManager
		{
			get
			{
				if (this._resourceManager == null)
				{
					string baseName = SafeSecurityHelper.GetAssemblyPartialName(this._assembly) + ".g";
					this._resourceManager = new ResourceManager(baseName, this._assembly);
				}
				return this._resourceManager;
			}
		}

		// Token: 0x04002139 RID: 8505
		private ResourceManager _resourceManager;

		// Token: 0x0400213A RID: 8506
		private ResourceSet _resourceSet;

		// Token: 0x0400213B RID: 8507
		private Assembly _assembly;

		// Token: 0x0400213C RID: 8508
		private ArrayList _resourceList;

		// Token: 0x0400213D RID: 8509
		private const string LocalizableResourceNameSuffix = ".g";

		// Token: 0x0400213E RID: 8510
		private const string UnLocalizableResourceNameSuffix = ".unlocalizable.g";
	}
}
