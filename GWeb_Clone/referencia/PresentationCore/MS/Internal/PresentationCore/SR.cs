using System;
using System.Globalization;
using System.Resources;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007EC RID: 2028
	internal static class SR
	{
		// Token: 0x06005565 RID: 21861 RVA: 0x0015F618 File Offset: 0x0015EA18
		internal static string Get(string id)
		{
			string @string = SR._resourceManager.GetString(id);
			if (@string == null)
			{
				@string = SR._resourceManager.GetString("Unavailable");
			}
			return @string;
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x0015F648 File Offset: 0x0015EA48
		internal static string Get(string id, params object[] args)
		{
			string text = SR._resourceManager.GetString(id);
			if (text == null)
			{
				text = SR._resourceManager.GetString("Unavailable");
			}
			else if (args != null && args.Length != 0)
			{
				text = string.Format(CultureInfo.CurrentCulture, text, args);
			}
			return text;
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06005567 RID: 21863 RVA: 0x0015F68C File Offset: 0x0015EA8C
		internal static ResourceManager ResourceManager
		{
			get
			{
				return SR._resourceManager;
			}
		}

		// Token: 0x0400265F RID: 9823
		private static ResourceManager _resourceManager = new ResourceManager("ExceptionStringTable", typeof(SR).Assembly);
	}
}
