using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace System.Windows.Media
{
	// Token: 0x02000361 RID: 865
	internal static class TypeConverterHelper
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x00078B58 File Offset: 0x00077F58
		internal static UriHolder GetUriFromUriContext(ITypeDescriptorContext context, object inputString)
		{
			UriHolder uriHolder = default(UriHolder);
			if (inputString is string)
			{
				uriHolder.OriginalUri = new Uri((string)inputString, UriKind.RelativeOrAbsolute);
			}
			else
			{
				uriHolder.OriginalUri = (Uri)inputString;
			}
			if (!uriHolder.OriginalUri.IsAbsoluteUri && context != null)
			{
				IUriContext uriContext = (IUriContext)context.GetService(typeof(IUriContext));
				if (uriContext != null)
				{
					if (uriContext.BaseUri != null)
					{
						uriHolder.BaseUri = uriContext.BaseUri;
						if (!uriHolder.BaseUri.IsAbsoluteUri)
						{
							uriHolder.BaseUri = new Uri(BaseUriHelper.BaseUri, uriHolder.BaseUri);
						}
					}
					else
					{
						uriHolder.BaseUri = BaseUriHelper.BaseUri;
					}
				}
			}
			return uriHolder;
		}
	}
}
