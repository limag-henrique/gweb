using System;

namespace System.Windows
{
	/// <summary>Fornece uma classe base abstrata para eventos associados à classe <see cref="T:System.Windows.DataObject" />.</summary>
	// Token: 0x02000196 RID: 406
	public abstract class DataObjectEventArgs : RoutedEventArgs
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0001C4BC File Offset: 0x0001B8BC
		internal DataObjectEventArgs(RoutedEvent routedEvent, bool isDragDrop)
		{
			if (routedEvent != DataObject.CopyingEvent && routedEvent != DataObject.PastingEvent && routedEvent != DataObject.SettingDataEvent)
			{
				throw new ArgumentOutOfRangeException("routedEvent");
			}
			base.RoutedEvent = routedEvent;
			this._isDragDrop = isDragDrop;
			this._commandCancelled = false;
		}

		/// <summary>Obtém um valor que indica se o evento associado faz parte de uma operação do tipo “arrastar e soltar”.</summary>
		/// <returns>
		///   <see langword="true" /> Se o evento associado faz parte de uma operação de arrastar e soltar; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001C508 File Offset: 0x0001B908
		public bool IsDragDrop
		{
			get
			{
				return this._isDragDrop;
			}
		}

		/// <summary>Obtém um valor que indica se o comando ou a operação associada foi cancelada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o comando tiver sido cancelado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001C51C File Offset: 0x0001B91C
		public bool CommandCancelled
		{
			get
			{
				return this._commandCancelled;
			}
		}

		/// <summary>Cancela a operação ou comando associado.</summary>
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001C530 File Offset: 0x0001B930
		public void CancelCommand()
		{
			this._commandCancelled = true;
		}

		// Token: 0x04000563 RID: 1379
		private bool _isDragDrop;

		// Token: 0x04000564 RID: 1380
		private bool _commandCancelled;
	}
}
