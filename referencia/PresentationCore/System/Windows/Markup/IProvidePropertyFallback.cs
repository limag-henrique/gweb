using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
	// Token: 0x02000200 RID: 512
	[TypeForwardedFrom("PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	internal interface IProvidePropertyFallback
	{
		// Token: 0x06000D42 RID: 3394
		bool CanProvidePropertyFallback(string property);

		// Token: 0x06000D43 RID: 3395
		object ProvidePropertyFallback(string property, Exception cause);
	}
}
