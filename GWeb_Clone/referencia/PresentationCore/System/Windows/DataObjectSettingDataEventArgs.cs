using System;

namespace System.Windows
{
	/// <summary>Contém argumentos para o <see cref="T:System.Windows.DataObject" />.<see cref="E:System.Windows.DataObject.SettingData" /> .</summary>
	// Token: 0x0200019B RID: 411
	public sealed class DataObjectSettingDataEventArgs : DataObjectEventArgs
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.DataObjectSettingDataEventArgs" />.</summary>
		/// <param name="dataObject">O <see cref="T:System.Windows.DataObject" /> para o qual um novo formato de dados está sendo adicionado.</param>
		/// <param name="format">Uma cadeia de caracteres que especifica o novo formato de dados que está sendo adicionado ao objeto de dados que a acompanha. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="dataObject" /> ou <paramref name="format" /> for nulo.</exception>
		// Token: 0x06000604 RID: 1540 RVA: 0x0001C720 File Offset: 0x0001BB20
		public DataObjectSettingDataEventArgs(IDataObject dataObject, string format) : base(System.Windows.DataObject.SettingDataEvent, false)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			this._dataObject = dataObject;
			this._format = format;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.DataObject" /> associado ao evento <see cref="E:System.Windows.DataObject.SettingData" />.</summary>
		/// <returns>O <see cref="T:System.Windows.DataObject" /> associados a <see cref="E:System.Windows.DataObject.SettingData" /> eventos.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001C764 File Offset: 0x0001BB64
		public IDataObject DataObject
		{
			get
			{
				return this._dataObject;
			}
		}

		/// <summary>Obtém uma cadeia de caracteres que especifica o novo formato de dados que está sendo adicionado ao objeto de dados que a acompanha.</summary>
		/// <returns>Uma cadeia de caracteres que especifica o novo formato de dados que está sendo adicionado ao objeto de dados que a acompanha.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001C778 File Offset: 0x0001BB78
		public string Format
		{
			get
			{
				return this._format;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001C78C File Offset: 0x0001BB8C
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			DataObjectSettingDataEventHandler dataObjectSettingDataEventHandler = (DataObjectSettingDataEventHandler)genericHandler;
			dataObjectSettingDataEventHandler(genericTarget, this);
		}

		// Token: 0x04000569 RID: 1385
		private IDataObject _dataObject;

		// Token: 0x0400056A RID: 1386
		private string _format;
	}
}
