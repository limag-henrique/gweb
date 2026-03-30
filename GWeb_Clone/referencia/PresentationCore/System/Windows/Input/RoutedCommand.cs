using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Define um comando que implementa o <see cref="T:System.Windows.Input.ICommand" /> e é encaminhado por meio da árvore de elementos.</summary>
	// Token: 0x02000221 RID: 545
	[ValueSerializer("System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public class RoutedCommand : ICommand
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedCommand" />.</summary>
		// Token: 0x06000EAE RID: 3758 RVA: 0x00037A18 File Offset: 0x00036E18
		public RoutedCommand()
		{
			this._name = string.Empty;
			this._ownerType = null;
			this._inputGestureCollection = null;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedCommand" /> com o tipo de proprietário e o nome especificados.</summary>
		/// <param name="name">Nome declarado para serialização.</param>
		/// <param name="ownerType">O tipo que está registrando o comando.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ownerType" /> é <see langword="null" />.</exception>
		// Token: 0x06000EAF RID: 3759 RVA: 0x00037A44 File Offset: 0x00036E44
		public RoutedCommand(string name, Type ownerType) : this(name, ownerType, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedCommand" /> com o nome, o tipo de proprietário e a coleção de gestos especificados.</summary>
		/// <param name="name">Nome declarado para serialização.</param>
		/// <param name="ownerType">O tipo que está registrando o comando.</param>
		/// <param name="inputGestures">Gestos de entrada padrão associados com este comando.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">o tamanho de <paramref name="name" /> é zero 
		///
		/// ou - 
		/// <paramref name="ownerType" /> é <see langword="null" />.</exception>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00037A5C File Offset: 0x00036E5C
		public RoutedCommand(string name, Type ownerType, InputGestureCollection inputGestures)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Get("StringEmpty"), "name");
			}
			if (ownerType == null)
			{
				throw new ArgumentNullException("ownerType");
			}
			this._name = name;
			this._ownerType = ownerType;
			this._inputGestureCollection = inputGestures;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00037AC4 File Offset: 0x00036EC4
		internal RoutedCommand(string name, Type ownerType, byte commandId) : this(name, ownerType, null)
		{
			this._commandId = commandId;
		}

		/// <summary>Para obter uma descrição desses membros, consulte <see cref="M:System.Windows.Input.ICommand.Execute(System.Object)" />.</summary>
		/// <param name="parameter">Dados usados pelo comando.  Se o comando não exigir que dados sejam passados, esse objeto poderá ser definido como <see langword="null" />.</param>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x00037AE4 File Offset: 0x00036EE4
		void ICommand.Execute(object parameter)
		{
			this.Execute(parameter, RoutedCommand.FilterInputElement(Keyboard.FocusedElement));
		}

		/// <summary>Para obter uma descrição desses membros, consulte <see cref="M:System.Windows.Input.ICommand.CanExecute(System.Object)" />.</summary>
		/// <param name="parameter">Dados usados pelo comando.  Se o comando não exigir que dados sejam passados, esse objeto poderá ser definido como <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> se esse comando puder ser executado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00037B04 File Offset: 0x00036F04
		[SecurityCritical]
		bool ICommand.CanExecute(object parameter)
		{
			bool flag;
			return this.CanExecuteImpl(parameter, RoutedCommand.FilterInputElement(Keyboard.FocusedElement), false, out flag);
		}

		/// <summary>Ocorre quando alterações na fonte de comando são detectadas pelo gerenciador de comandos. Geralmente, essas alterações afetam se o comando deve ser executado no destino de comando atual.</summary>
		// Token: 0x1400014D RID: 333
		// (add) Token: 0x06000EB4 RID: 3764 RVA: 0x00037B28 File Offset: 0x00036F28
		// (remove) Token: 0x06000EB5 RID: 3765 RVA: 0x00037B3C File Offset: 0x00036F3C
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		/// <summary>Executa o <see cref="T:System.Windows.Input.RoutedCommand" /> no destino de comando atual.</summary>
		/// <param name="parameter">Parâmetro definido pelo usuário a ser passado para o manipulador.</param>
		/// <param name="target">Elemento no qual começar a procurar por manipuladores de comandos.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="target" /> não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00037B50 File Offset: 0x00036F50
		[SecurityCritical]
		public void Execute(object parameter, IInputElement target)
		{
			if (target != null && !InputElement.IsValid(target))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					target.GetType()
				}));
			}
			if (target == null)
			{
				target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
			}
			this.ExecuteImpl(parameter, target, false);
		}

		/// <summary>Determina se este <see cref="T:System.Windows.Input.RoutedCommand" /> pode ser executado em seu estado atual.</summary>
		/// <param name="parameter">Um tipo de dados definido pelo usuário.</param>
		/// <param name="target">O destino do comando.</param>
		/// <returns>
		///   <see langword="true" /> se o comando puder ser executado no destino de comando atual; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="target" /> não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00037BA0 File Offset: 0x00036FA0
		[SecurityCritical]
		public bool CanExecute(object parameter, IInputElement target)
		{
			bool flag;
			return this.CriticalCanExecute(parameter, target, false, out flag);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00037BB8 File Offset: 0x00036FB8
		[SecurityCritical]
		internal bool CriticalCanExecute(object parameter, IInputElement target, bool trusted, out bool continueRouting)
		{
			if (target != null && !InputElement.IsValid(target))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					target.GetType()
				}));
			}
			if (target == null)
			{
				target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
			}
			return this.CanExecuteImpl(parameter, target, trusted, out continueRouting);
		}

		/// <summary>Obtém o nome do comando.</summary>
		/// <returns>O nome do comando.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00037C0C File Offset: 0x0003700C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Obtém o tipo que é registrado com o comando.</summary>
		/// <returns>O tipo de proprietário do comando.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00037C20 File Offset: 0x00037020
		public Type OwnerType
		{
			get
			{
				return this._ownerType;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00037C34 File Offset: 0x00037034
		internal byte CommandId
		{
			get
			{
				return this._commandId;
			}
		}

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.Input.InputGesture" /> associados a esse comando.</summary>
		/// <returns>Os gestos de entrada.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00037C48 File Offset: 0x00037048
		public InputGestureCollection InputGestures
		{
			get
			{
				if (this.InputGesturesInternal == null)
				{
					this._inputGestureCollection = new InputGestureCollection();
				}
				return this._inputGestureCollection;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00037C70 File Offset: 0x00037070
		internal InputGestureCollection InputGesturesInternal
		{
			get
			{
				if (this._inputGestureCollection == null && this.AreInputGesturesDelayLoaded)
				{
					this._inputGestureCollection = this.GetInputGestures();
					this.AreInputGesturesDelayLoaded = false;
				}
				return this._inputGestureCollection;
			}
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00037CA8 File Offset: 0x000370A8
		private InputGestureCollection GetInputGestures()
		{
			if (this.OwnerType == typeof(ApplicationCommands))
			{
				return ApplicationCommands.LoadDefaultGestureFromResource(this._commandId);
			}
			if (this.OwnerType == typeof(NavigationCommands))
			{
				return NavigationCommands.LoadDefaultGestureFromResource(this._commandId);
			}
			if (this.OwnerType == typeof(MediaCommands))
			{
				return MediaCommands.LoadDefaultGestureFromResource(this._commandId);
			}
			if (this.OwnerType == typeof(ComponentCommands))
			{
				return ComponentCommands.LoadDefaultGestureFromResource(this._commandId);
			}
			return new InputGestureCollection();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00037D48 File Offset: 0x00037148
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00037D5C File Offset: 0x0003715C
		internal bool IsBlockedByRM
		{
			[SecurityCritical]
			get
			{
				return this.ReadPrivateFlag(RoutedCommand.PrivateFlags.IsBlockedByRM);
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			set
			{
				this.WritePrivateFlag(RoutedCommand.PrivateFlags.IsBlockedByRM, value);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00037D74 File Offset: 0x00037174
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00037D88 File Offset: 0x00037188
		internal bool AreInputGesturesDelayLoaded
		{
			get
			{
				return this.ReadPrivateFlag(RoutedCommand.PrivateFlags.AreInputGesturesDelayLoaded);
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this.WritePrivateFlag(RoutedCommand.PrivateFlags.AreInputGesturesDelayLoaded, value);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00037DA0 File Offset: 0x000371A0
		private static IInputElement FilterInputElement(IInputElement elem)
		{
			if (elem != null && InputElement.IsValid(elem))
			{
				return elem;
			}
			return null;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00037DBC File Offset: 0x000371BC
		[SecurityCritical]
		private bool CanExecuteImpl(object parameter, IInputElement target, bool trusted, out bool continueRouting)
		{
			if (target != null && !this.IsBlockedByRM)
			{
				CanExecuteRoutedEventArgs canExecuteRoutedEventArgs = new CanExecuteRoutedEventArgs(this, parameter);
				canExecuteRoutedEventArgs.RoutedEvent = CommandManager.PreviewCanExecuteEvent;
				this.CriticalCanExecuteWrapper(parameter, target, trusted, canExecuteRoutedEventArgs);
				if (!canExecuteRoutedEventArgs.Handled)
				{
					canExecuteRoutedEventArgs.RoutedEvent = CommandManager.CanExecuteEvent;
					this.CriticalCanExecuteWrapper(parameter, target, trusted, canExecuteRoutedEventArgs);
				}
				continueRouting = canExecuteRoutedEventArgs.ContinueRouting;
				return canExecuteRoutedEventArgs.CanExecute;
			}
			continueRouting = false;
			return false;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00037E24 File Offset: 0x00037224
		[SecurityCritical]
		private void CriticalCanExecuteWrapper(object parameter, IInputElement target, bool trusted, CanExecuteRoutedEventArgs args)
		{
			DependencyObject dependencyObject = (DependencyObject)target;
			if (InputElement.IsUIElement(dependencyObject))
			{
				((UIElement)dependencyObject).RaiseEvent(args, trusted);
				return;
			}
			if (InputElement.IsContentElement(dependencyObject))
			{
				((ContentElement)dependencyObject).RaiseEvent(args, trusted);
				return;
			}
			if (InputElement.IsUIElement3D(dependencyObject))
			{
				((UIElement3D)dependencyObject).RaiseEvent(args, trusted);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00037E7C File Offset: 0x0003727C
		[SecurityCritical]
		internal bool ExecuteCore(object parameter, IInputElement target, bool userInitiated)
		{
			if (target == null)
			{
				target = RoutedCommand.FilterInputElement(Keyboard.FocusedElement);
			}
			return this.ExecuteImpl(parameter, target, userInitiated);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00037EA4 File Offset: 0x000372A4
		[SecurityCritical]
		private bool ExecuteImpl(object parameter, IInputElement target, bool userInitiated)
		{
			if (target != null && !this.IsBlockedByRM)
			{
				UIElement uielement = target as UIElement;
				ContentElement contentElement = null;
				UIElement3D uielement3D = null;
				ExecutedRoutedEventArgs executedRoutedEventArgs = new ExecutedRoutedEventArgs(this, parameter);
				executedRoutedEventArgs.RoutedEvent = CommandManager.PreviewExecutedEvent;
				if (uielement != null)
				{
					uielement.RaiseEvent(executedRoutedEventArgs, userInitiated);
				}
				else
				{
					contentElement = (target as ContentElement);
					if (contentElement != null)
					{
						contentElement.RaiseEvent(executedRoutedEventArgs, userInitiated);
					}
					else
					{
						uielement3D = (target as UIElement3D);
						if (uielement3D != null)
						{
							uielement3D.RaiseEvent(executedRoutedEventArgs, userInitiated);
						}
					}
				}
				if (!executedRoutedEventArgs.Handled)
				{
					executedRoutedEventArgs.RoutedEvent = CommandManager.ExecutedEvent;
					if (uielement != null)
					{
						uielement.RaiseEvent(executedRoutedEventArgs, userInitiated);
					}
					else if (contentElement != null)
					{
						contentElement.RaiseEvent(executedRoutedEventArgs, userInitiated);
					}
					else if (uielement3D != null)
					{
						uielement3D.RaiseEvent(executedRoutedEventArgs, userInitiated);
					}
				}
				return executedRoutedEventArgs.Handled;
			}
			return false;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00037F54 File Offset: 0x00037354
		[SecurityCritical]
		private void WritePrivateFlag(RoutedCommand.PrivateFlags bit, bool value)
		{
			if (value)
			{
				this._flags.Value = (this._flags.Value | bit);
				return;
			}
			this._flags.Value = (this._flags.Value & ~bit);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00037F90 File Offset: 0x00037390
		private bool ReadPrivateFlag(RoutedCommand.PrivateFlags bit)
		{
			return (this._flags.Value & bit) > (RoutedCommand.PrivateFlags)0;
		}

		// Token: 0x04000853 RID: 2131
		private string _name;

		// Token: 0x04000854 RID: 2132
		private SecurityCriticalDataForSet<RoutedCommand.PrivateFlags> _flags;

		// Token: 0x04000855 RID: 2133
		private Type _ownerType;

		// Token: 0x04000856 RID: 2134
		private InputGestureCollection _inputGestureCollection;

		// Token: 0x04000857 RID: 2135
		private byte _commandId;

		// Token: 0x02000804 RID: 2052
		private enum PrivateFlags : byte
		{
			// Token: 0x040026B1 RID: 9905
			IsBlockedByRM = 1,
			// Token: 0x040026B2 RID: 9906
			AreInputGesturesDelayLoaded
		}
	}
}
