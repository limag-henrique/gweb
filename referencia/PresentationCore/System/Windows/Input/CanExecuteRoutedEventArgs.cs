using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para os eventos roteados <see cref="E:System.Windows.Input.CommandBinding.CanExecute" /> e <see cref="E:System.Windows.Input.CommandManager.PreviewCanExecute" />.</summary>
	// Token: 0x02000207 RID: 519
	public sealed class CanExecuteRoutedEventArgs : RoutedEventArgs
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x00033918 File Offset: 0x00032D18
		internal CanExecuteRoutedEventArgs(ICommand command, object parameter)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this._command = command;
			this._parameter = parameter;
		}

		/// <summary>Obtém o comando associado a esse evento.</summary>
		/// <returns>O comando. A menos que o comando é um comando personalizado, isso é geralmente um <see cref="T:System.Windows.Input.RoutedCommand" />. Nenhum valor padrão.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00033948 File Offset: 0x00032D48
		public ICommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Obtém os dados específicos do comando.</summary>
		/// <returns>Os dados de comando.  O valor padrão é <see langword="null" />.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003395C File Offset: 0x00032D5C
		public object Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		/// <summary>Obtém ou define um valor que indica se o <see cref="T:System.Windows.Input.RoutedCommand" /> associado a esse evento pode ser executado no destino de comando.</summary>
		/// <returns>
		///   <see langword="true" /> Se o evento pode ser executado no destino do comando; Caso contrário, <see langword="false" />.  O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00033970 File Offset: 0x00032D70
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x00033984 File Offset: 0x00032D84
		public bool CanExecute
		{
			get
			{
				return this._canExecute;
			}
			set
			{
				this._canExecute = value;
			}
		}

		/// <summary>Determina se os eventos roteados de entrada que invocaram o comando devem continuar roteando por meio da árvore de elementos.</summary>
		/// <returns>
		///   <see langword="true" /> Se o evento roteado deve continuar roteando por meio da árvore de elementos; Caso contrário, <see langword="false" />.   O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00033998 File Offset: 0x00032D98
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x000339AC File Offset: 0x00032DAC
		public bool ContinueRouting
		{
			get
			{
				return this._continueRouting;
			}
			set
			{
				this._continueRouting = value;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000339C0 File Offset: 0x00032DC0
		protected override void InvokeEventHandler(Delegate genericHandler, object target)
		{
			CanExecuteRoutedEventHandler canExecuteRoutedEventHandler = (CanExecuteRoutedEventHandler)genericHandler;
			canExecuteRoutedEventHandler(target as DependencyObject, this);
		}

		// Token: 0x04000814 RID: 2068
		private ICommand _command;

		// Token: 0x04000815 RID: 2069
		private object _parameter;

		// Token: 0x04000816 RID: 2070
		private bool _canExecute;

		// Token: 0x04000817 RID: 2071
		private bool _continueRouting;
	}
}
