using System;

namespace System.Windows.Ink
{
	// Token: 0x02000348 RID: 840
	internal class ExtendedPropertiesChangedEventArgs : EventArgs
	{
		// Token: 0x06001C5F RID: 7263 RVA: 0x00073980 File Offset: 0x00072D80
		internal ExtendedPropertiesChangedEventArgs(ExtendedProperty oldProperty, ExtendedProperty newProperty)
		{
			if (oldProperty == null && newProperty == null)
			{
				throw new ArgumentNullException("oldProperty");
			}
			this._oldProperty = oldProperty;
			this._newProperty = newProperty;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x000739C0 File Offset: 0x00072DC0
		internal ExtendedProperty OldProperty
		{
			get
			{
				return this._oldProperty;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000739D4 File Offset: 0x00072DD4
		internal ExtendedProperty NewProperty
		{
			get
			{
				return this._newProperty;
			}
		}

		// Token: 0x04000F7E RID: 3966
		private ExtendedProperty _oldProperty;

		// Token: 0x04000F7F RID: 3967
		private ExtendedProperty _newProperty;
	}
}
