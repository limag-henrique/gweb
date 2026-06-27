using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Markup
{
	// Token: 0x02000201 RID: 513
	internal static class XamlCoreSerializerUtil
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x00032450 File Offset: 0x00031850
		static XamlCoreSerializerUtil()
		{
			XamlCoreSerializerUtil.ThrowIfIAddChildInternal("not IAddChildInternal");
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00032468 File Offset: 0x00031868
		internal static void ThrowIfIAddChildInternal(object o)
		{
			if (o is IAddChildInternal)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00032484 File Offset: 0x00031884
		internal static void ThrowIfNonWhiteSpaceInAddText(string s)
		{
			if (s != null)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (!char.IsWhiteSpace(s[i]))
					{
						throw new ArgumentException(SR.Get("NonWhiteSpaceInAddText", new object[]
						{
							s
						}));
					}
				}
			}
		}
	}
}
