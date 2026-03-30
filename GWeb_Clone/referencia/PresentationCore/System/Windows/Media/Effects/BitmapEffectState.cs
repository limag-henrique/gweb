using System;

namespace System.Windows.Media.Effects
{
	// Token: 0x0200060C RID: 1548
	internal class BitmapEffectState
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x00117174 File Offset: 0x00116574
		// (set) Token: 0x06004720 RID: 18208 RVA: 0x00117188 File Offset: 0x00116588
		public BitmapEffect BitmapEffect
		{
			get
			{
				return this._bitmapEffect;
			}
			set
			{
				this._bitmapEffect = value;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x0011719C File Offset: 0x0011659C
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x001171B0 File Offset: 0x001165B0
		public BitmapEffectInput BitmapEffectInput
		{
			get
			{
				return this._bitmapEffectInput;
			}
			set
			{
				this._bitmapEffectInput = value;
			}
		}

		// Token: 0x040019E8 RID: 6632
		private BitmapEffect _bitmapEffect;

		// Token: 0x040019E9 RID: 6633
		private BitmapEffectInput _bitmapEffectInput;
	}
}
