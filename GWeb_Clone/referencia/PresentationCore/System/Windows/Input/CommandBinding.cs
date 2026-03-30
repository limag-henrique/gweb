using System;

namespace System.Windows.Input
{
	/// <summary>Associa um <see cref="T:System.Windows.Input.RoutedCommand" /> aos manipuladores de eventos que implementam o comando.</summary>
	// Token: 0x0200020B RID: 523
	public class CommandBinding
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBinding" />.</summary>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00033F38 File Offset: 0x00033338
		public CommandBinding()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBinding" /> usando o <see cref="T:System.Windows.Input.ICommand" /> especificado.</summary>
		/// <param name="command">O comando no qual o novo <see cref="T:System.Windows.Input.RoutedCommand" /> é baseado.</param>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00033F4C File Offset: 0x0003334C
		public CommandBinding(ICommand command) : this(command, null, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBinding" /> usando o <see cref="T:System.Windows.Input.ICommand" /> e o manipulador de eventos <see cref="E:System.Windows.Input.CommandBinding.Executed" /> especificados.</summary>
		/// <param name="command">O comando no qual o novo <see cref="T:System.Windows.Input.RoutedCommand" /> é baseado.</param>
		/// <param name="executed">O manipulador para o evento <see cref="E:System.Windows.Input.CommandBinding.Executed" /> no novo <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00033F64 File Offset: 0x00033364
		public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed) : this(command, executed, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBinding" /> usando o <see cref="T:System.Windows.Input.ICommand" /> especificado e os manipuladores de eventos <see cref="E:System.Windows.Input.CommandBinding.Executed" /> e <see cref="E:System.Windows.Input.CommandBinding.CanExecute" /> especificados.</summary>
		/// <param name="command">O comando no qual o novo <see cref="T:System.Windows.Input.RoutedCommand" /> é baseado.</param>
		/// <param name="executed">O manipulador para o evento <see cref="E:System.Windows.Input.CommandBinding.Executed" /> no novo <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
		/// <param name="canExecute">O manipulador para o evento <see cref="E:System.Windows.Input.CommandBinding.CanExecute" /> no novo <see cref="T:System.Windows.Input.RoutedCommand" />.</param>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00033F7C File Offset: 0x0003337C
		public CommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this._command = command;
			if (executed != null)
			{
				this.Executed += executed;
			}
			if (canExecute != null)
			{
				this.CanExecute += canExecute;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.ICommand" /> associado a esse <see cref="T:System.Windows.Input.CommandBinding" />.</summary>
		/// <returns>O comando associado a essa associação.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00033FB8 File Offset: 0x000333B8
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00033FCC File Offset: 0x000333CC
		[Localizability(LocalizationCategory.NeverLocalize)]
		public ICommand Command
		{
			get
			{
				return this._command;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._command = value;
			}
		}

		/// <summary>Ocorre quando o comando associado a este <see cref="T:System.Windows.Input.CommandBinding" /> é executado.</summary>
		// Token: 0x14000146 RID: 326
		// (add) Token: 0x06000DA7 RID: 3495 RVA: 0x00033FF0 File Offset: 0x000333F0
		// (remove) Token: 0x06000DA8 RID: 3496 RVA: 0x00034028 File Offset: 0x00033428
		public event ExecutedRoutedEventHandler PreviewExecuted;

		/// <summary>Ocorre quando o comando associado a este <see cref="T:System.Windows.Input.CommandBinding" /> é executado.</summary>
		// Token: 0x14000147 RID: 327
		// (add) Token: 0x06000DA9 RID: 3497 RVA: 0x00034060 File Offset: 0x00033460
		// (remove) Token: 0x06000DAA RID: 3498 RVA: 0x00034098 File Offset: 0x00033498
		public event ExecutedRoutedEventHandler Executed;

		/// <summary>Ocorre quando o comando associado a essa <see cref="T:System.Windows.Input.CommandBinding" /> inicia uma verificação para determinar se o comando pode ser executado no destino do comando atual.</summary>
		// Token: 0x14000148 RID: 328
		// (add) Token: 0x06000DAB RID: 3499 RVA: 0x000340D0 File Offset: 0x000334D0
		// (remove) Token: 0x06000DAC RID: 3500 RVA: 0x00034108 File Offset: 0x00033508
		public event CanExecuteRoutedEventHandler PreviewCanExecute;

		/// <summary>Ocorre quando o comando associado a essa <see cref="T:System.Windows.Input.CommandBinding" /> inicia uma verificação para determinar se o comando pode ser executado no destino do comando.</summary>
		// Token: 0x14000149 RID: 329
		// (add) Token: 0x06000DAD RID: 3501 RVA: 0x00034140 File Offset: 0x00033540
		// (remove) Token: 0x06000DAE RID: 3502 RVA: 0x00034178 File Offset: 0x00033578
		public event CanExecuteRoutedEventHandler CanExecute;

		// Token: 0x06000DAF RID: 3503 RVA: 0x000341B0 File Offset: 0x000335B0
		internal void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (!e.Handled)
			{
				if (e.RoutedEvent == CommandManager.CanExecuteEvent)
				{
					if (this.CanExecute != null)
					{
						this.CanExecute(sender, e);
						if (e.CanExecute)
						{
							e.Handled = true;
							return;
						}
					}
					else if (!e.CanExecute && this.Executed != null)
					{
						e.CanExecute = true;
						e.Handled = true;
						return;
					}
				}
				else if (this.PreviewCanExecute != null)
				{
					this.PreviewCanExecute(sender, e);
					if (e.CanExecute)
					{
						e.Handled = true;
					}
				}
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00034240 File Offset: 0x00033640
		private bool CheckCanExecute(object sender, ExecutedRoutedEventArgs e)
		{
			CanExecuteRoutedEventArgs canExecuteRoutedEventArgs = new CanExecuteRoutedEventArgs(e.Command, e.Parameter);
			canExecuteRoutedEventArgs.RoutedEvent = CommandManager.CanExecuteEvent;
			canExecuteRoutedEventArgs.Source = e.OriginalSource;
			canExecuteRoutedEventArgs.OverrideSource(e.Source);
			this.OnCanExecute(sender, canExecuteRoutedEventArgs);
			return canExecuteRoutedEventArgs.CanExecute;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00034290 File Offset: 0x00033690
		internal void OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (!e.Handled)
			{
				if (e.RoutedEvent == CommandManager.ExecutedEvent)
				{
					if (this.Executed != null && this.CheckCanExecute(sender, e))
					{
						this.Executed(sender, e);
						e.Handled = true;
						return;
					}
				}
				else if (this.PreviewExecuted != null && this.CheckCanExecute(sender, e))
				{
					this.PreviewExecuted(sender, e);
					e.Handled = true;
				}
			}
		}

		// Token: 0x0400081F RID: 2079
		private ICommand _command;
	}
}
