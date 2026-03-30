using System;

namespace System.Windows
{
	/// <summary>Argumentos para o <see cref="T:System.Windows.DataObject" />.<see cref="E:System.Windows.DataObject.Copying" /> .</summary>
	// Token: 0x02000197 RID: 407
	public sealed class DataObjectCopyingEventArgs : DataObjectEventArgs
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.DataObjectCopyingEventArgs" />.</summary>
		/// <param name="dataObject">Um <see cref="T:System.Windows.DataObject" /> que contém dados prontos para serem copiados.</param>
		/// <param name="isDragDrop">Um valor <see langword="Boolean" /> que indica se a cópia faz parte de uma operação do tipo “arrastar e soltar”. <see langword="true" /> para indica que a cópia faz parte de uma operação do tipo “arrastar e soltar”; caso contrário, <see langword="false" />. Se esse parâmetro for definido como <see langword="true" />, o evento <see cref="E:System.Windows.DataObject.Copying" /> é disparado quando arrastar for iniciado.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="dataObject" /> é null.</exception>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001C544 File Offset: 0x0001B944
		public DataObjectCopyingEventArgs(IDataObject dataObject, bool isDragDrop) : base(System.Windows.DataObject.CopyingEvent, isDragDrop)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			this._dataObject = dataObject;
		}

		/// <summary>Obtém o objeto de dados associado ao evento <see cref="E:System.Windows.DataObject.Copying" />.</summary>
		/// <returns>O objeto de dados associado a <see cref="E:System.Windows.DataObject.Copying" /> eventos.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001C574 File Offset: 0x0001B974
		public IDataObject DataObject
		{
			get
			{
				return this._dataObject;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001C588 File Offset: 0x0001B988
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			DataObjectCopyingEventHandler dataObjectCopyingEventHandler = (DataObjectCopyingEventHandler)genericHandler;
			dataObjectCopyingEventHandler(genericTarget, this);
		}

		// Token: 0x04000565 RID: 1381
		private IDataObject _dataObject;
	}
}
