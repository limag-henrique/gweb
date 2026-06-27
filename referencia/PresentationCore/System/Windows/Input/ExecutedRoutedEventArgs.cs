using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para os eventos roteados <see cref="E:System.Windows.Input.CommandManager.Executed" /> e <see cref="E:System.Windows.Input.CommandManager.PreviewExecuted" />.</summary>
	// Token: 0x0200020F RID: 527
	public sealed class ExecutedRoutedEventArgs : RoutedEventArgs
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x000354D4 File Offset: 0x000348D4
		internal ExecutedRoutedEventArgs(ICommand command, object parameter)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this._command = command;
			this._parameter = parameter;
		}

		/// <summary>Obtém o comando que foi invocado.</summary>
		/// <returns>O comando associado ao evento.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00035504 File Offset: 0x00034904
		public ICommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Obtém o parâmetro de dados do comando.</summary>
		/// <returns>Os dados específicos ao comando. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00035518 File Offset: 0x00034918
		public object Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003552C File Offset: 0x0003492C
		protected override void InvokeEventHandler(Delegate genericHandler, object target)
		{
			ExecutedRoutedEventHandler executedRoutedEventHandler = (ExecutedRoutedEventHandler)genericHandler;
			executedRoutedEventHandler(target as DependencyObject, this);
		}

		// Token: 0x0400082A RID: 2090
		private ICommand _command;

		// Token: 0x0400082B RID: 2091
		private object _parameter;
	}
}
