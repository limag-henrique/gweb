using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005DC RID: 1500
	internal class BitmapInitialize : ISupportInitialize
	{
		// Token: 0x06004405 RID: 17413 RVA: 0x00108DF4 File Offset: 0x001081F4
		public void BeginInit()
		{
			if (this.IsInitAtLeastOnce)
			{
				throw new InvalidOperationException(SR.Get("Image_OnlyOneInit", null));
			}
			if (this.IsInInit)
			{
				throw new InvalidOperationException(SR.Get("Image_InInitialize", null));
			}
			this._inInit = true;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x00108E3C File Offset: 0x0010823C
		public void EndInit()
		{
			if (!this.IsInInit)
			{
				throw new InvalidOperationException(SR.Get("Image_EndInitWithoutBeginInit", null));
			}
			this._inInit = false;
			this._isInitialized = true;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00108E70 File Offset: 0x00108270
		public void SetPrologue()
		{
			if (!this.IsInInit)
			{
				throw new InvalidOperationException(SR.Get("Image_SetPropertyOutsideBeginEndInit", null));
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x00108E98 File Offset: 0x00108298
		public bool IsInInit
		{
			get
			{
				return this._inInit;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x00108EAC File Offset: 0x001082AC
		public bool IsInitAtLeastOnce
		{
			get
			{
				return this._isInitialized;
			}
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x00108EC0 File Offset: 0x001082C0
		public void EnsureInitializedComplete()
		{
			if (this.IsInInit)
			{
				throw new InvalidOperationException(SR.Get("Image_InitializationIncomplete", null));
			}
			if (!this.IsInitAtLeastOnce)
			{
				throw new InvalidOperationException(SR.Get("Image_NotInitialized", null));
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x00108F00 File Offset: 0x00108300
		public void Reset()
		{
			this._inInit = false;
			this._isInitialized = false;
		}

		// Token: 0x040018CA RID: 6346
		private bool _inInit;

		// Token: 0x040018CB RID: 6347
		private bool _isInitialized;
	}
}
