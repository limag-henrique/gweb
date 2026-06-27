using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Fornece dados para o evento de <see langword="PropertyDataChanged" /> .</summary>
	// Token: 0x02000346 RID: 838
	public class PropertyDataChangedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" />.</summary>
		/// <param name="propertyGuid">O <see cref="T:System.Guid" /> da propriedade personalizada que foi alterado.</param>
		/// <param name="newValue">O novo objeto de propriedade personalizada.</param>
		/// <param name="previousValue">O objeto de propriedade personalizada anterior.</param>
		// Token: 0x06001C57 RID: 7255 RVA: 0x000738F0 File Offset: 0x00072CF0
		public PropertyDataChangedEventArgs(Guid propertyGuid, object newValue, object previousValue)
		{
			if (newValue == null && previousValue == null)
			{
				throw new ArgumentException(SR.Get("CannotBothBeNull", new object[]
				{
					"newValue",
					"previousValue"
				}));
			}
			this._propertyGuid = propertyGuid;
			this._newValue = newValue;
			this._previousValue = previousValue;
		}

		/// <summary>Obtém o <see cref="T:System.Guid" /> da propriedade personalizada que foi alterado.</summary>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00073944 File Offset: 0x00072D44
		public Guid PropertyGuid
		{
			get
			{
				return this._propertyGuid;
			}
		}

		/// <summary>Obtém o novo objeto de propriedade personalizada.</summary>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00073958 File Offset: 0x00072D58
		public object NewValue
		{
			get
			{
				return this._newValue;
			}
		}

		/// <summary>Obtém o objeto de propriedade personalizada anterior.</summary>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0007396C File Offset: 0x00072D6C
		public object PreviousValue
		{
			get
			{
				return this._previousValue;
			}
		}

		// Token: 0x04000F7B RID: 3963
		private Guid _propertyGuid;

		// Token: 0x04000F7C RID: 3964
		private object _newValue;

		// Token: 0x04000F7D RID: 3965
		private object _previousValue;
	}
}
