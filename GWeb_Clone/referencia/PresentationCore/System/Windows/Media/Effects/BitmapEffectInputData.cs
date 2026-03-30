using System;

namespace System.Windows.Media.Effects
{
	// Token: 0x0200060B RID: 1547
	internal struct BitmapEffectInputData
	{
		// Token: 0x0600471D RID: 18205 RVA: 0x00117144 File Offset: 0x00116544
		public BitmapEffectInputData(BitmapEffect bitmapEffect, BitmapEffectInput bitmapEffectInput)
		{
			this.BitmapEffect = bitmapEffect;
			this.BitmapEffectInput = bitmapEffectInput;
		}

		// Token: 0x040019E6 RID: 6630
		public BitmapEffect BitmapEffect;

		// Token: 0x040019E7 RID: 6631
		public BitmapEffectInput BitmapEffectInput;
	}
}
