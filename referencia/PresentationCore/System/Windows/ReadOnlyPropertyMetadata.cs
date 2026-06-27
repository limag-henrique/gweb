using System;

namespace System.Windows
{
	// Token: 0x020001F7 RID: 503
	internal class ReadOnlyPropertyMetadata : PropertyMetadata
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00031C00 File Offset: 0x00031000
		public ReadOnlyPropertyMetadata(object defaultValue, GetReadOnlyValueCallback getValueCallback, PropertyChangedCallback propertyChangedCallback) : base(defaultValue, propertyChangedCallback)
		{
			this._getValueCallback = getValueCallback;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00031C1C File Offset: 0x0003101C
		internal override GetReadOnlyValueCallback GetReadOnlyValueCallback
		{
			get
			{
				return this._getValueCallback;
			}
		}

		// Token: 0x040007F0 RID: 2032
		private GetReadOnlyValueCallback _getValueCallback;
	}
}
