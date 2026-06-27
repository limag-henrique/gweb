using System;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000570 RID: 1392
	internal class IndependentlyAnimatedPropertyMetadata : UIPropertyMetadata
	{
		// Token: 0x0600407F RID: 16511 RVA: 0x000FCF20 File Offset: 0x000FC320
		internal IndependentlyAnimatedPropertyMetadata(object defaultValue) : base(defaultValue)
		{
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000FCF34 File Offset: 0x000FC334
		internal IndependentlyAnimatedPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(defaultValue, propertyChangedCallback, coerceValueCallback)
		{
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000FCF4C File Offset: 0x000FC34C
		internal override PropertyMetadata CreateInstance()
		{
			return new IndependentlyAnimatedPropertyMetadata(base.DefaultValue);
		}
	}
}
