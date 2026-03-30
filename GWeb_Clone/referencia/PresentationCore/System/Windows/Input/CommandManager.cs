using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Windows.Threading;

namespace System.Windows.Input
{
	/// <summary>Fornece os métodos de utilitário relativos ao comando que registram objetos <see cref="T:System.Windows.Input.CommandBinding" /> e <see cref="T:System.Windows.Input.InputBinding" /> para proprietários de classe e comandos, adiciona e remove manipuladores de eventos de comando e fornece serviços para consultar o status de um comando.</summary>
	// Token: 0x0200020D RID: 525
	public sealed class CommandManager
	{
		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.CommandManager" /> detecta condições que podem alterar a capacidade de execução de um comando.</summary>
		// Token: 0x1400014A RID: 330
		// (add) Token: 0x06000DCF RID: 3535 RVA: 0x00034790 File Offset: 0x00033B90
		// (remove) Token: 0x06000DD0 RID: 3536 RVA: 0x000347A4 File Offset: 0x00033BA4
		public static event EventHandler RequerySuggested
		{
			add
			{
				CommandManager.RequerySuggestedEventManager.AddHandler(null, value);
			}
			remove
			{
				CommandManager.RequerySuggestedEventManager.RemoveHandler(null, value);
			}
		}

		/// <summary>Anexa o <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> especificado ao elemento especificado.</summary>
		/// <param name="element">O elemento ao qual anexar <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador can execute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x000347B8 File Offset: 0x00033BB8
		public static void AddPreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.AddHandler(CommandManager.PreviewExecutedEvent, handler);
		}

		/// <summary>Desanexa o <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> especificado do elemento especificado.</summary>
		/// <param name="element">O elemento do qual remover <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador executado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x000347F0 File Offset: 0x00033BF0
		public static void RemovePreviewExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.RemoveHandler(CommandManager.PreviewExecutedEvent, handler);
		}

		/// <summary>Anexa o <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> especificado ao elemento especificado.</summary>
		/// <param name="element">O elemento ao qual anexar <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador executado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00034828 File Offset: 0x00033C28
		public static void AddExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.AddHandler(CommandManager.ExecutedEvent, handler);
		}

		/// <summary>Desanexa o <see cref="T:System.Windows.Input.ExecutedRoutedEventHandler" /> especificado do elemento especificado.</summary>
		/// <param name="element">O elemento do qual remover <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador executado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00034860 File Offset: 0x00033C60
		public static void RemoveExecutedHandler(UIElement element, ExecutedRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.RemoveHandler(CommandManager.ExecutedEvent, handler);
		}

		/// <summary>Anexa o <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> especificado ao elemento especificado.</summary>
		/// <param name="element">O elemento ao qual anexar <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador can execute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x00034898 File Offset: 0x00033C98
		public static void AddPreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.AddHandler(CommandManager.PreviewCanExecuteEvent, handler);
		}

		/// <summary>Desanexa o <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> especificado do elemento especificado.</summary>
		/// <param name="element">O elemento do qual remover <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador can execute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x000348D0 File Offset: 0x00033CD0
		public static void RemovePreviewCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.RemoveHandler(CommandManager.PreviewCanExecuteEvent, handler);
		}

		/// <summary>Anexa o <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> especificado ao elemento especificado.</summary>
		/// <param name="element">O elemento ao qual anexar <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador can execute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00034908 File Offset: 0x00033D08
		public static void AddCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.AddHandler(CommandManager.CanExecuteEvent, handler);
		}

		/// <summary>Desanexa o <see cref="T:System.Windows.Input.CanExecuteRoutedEventHandler" /> especificado do elemento especificado.</summary>
		/// <param name="element">O elemento do qual remover <paramref name="handler" />.</param>
		/// <param name="handler">O manipulador can execute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00034940 File Offset: 0x00033D40
		public static void RemoveCanExecuteHandler(UIElement element, CanExecuteRoutedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			element.RemoveHandler(CommandManager.CanExecuteEvent, handler);
		}

		/// <summary>Registra o <see cref="T:System.Windows.Input.InputBinding" /> especificado com o tipo especificado.</summary>
		/// <param name="type">O tipo com o qual registrar <paramref name="inputBinding" />.</param>
		/// <param name="inputBinding">A associação de entrada a ser registrada.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> ou <paramref name="inputBinding" /> é <see langword="null" />.</exception>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00034978 File Offset: 0x00033D78
		public static void RegisterClassInputBinding(Type type, InputBinding inputBinding)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (inputBinding == null)
			{
				throw new ArgumentNullException("inputBinding");
			}
			object syncRoot = CommandManager._classInputBindings.SyncRoot;
			lock (syncRoot)
			{
				InputBindingCollection inputBindingCollection = CommandManager._classInputBindings[type] as InputBindingCollection;
				if (inputBindingCollection == null)
				{
					inputBindingCollection = new InputBindingCollection();
					CommandManager._classInputBindings[type] = inputBindingCollection;
				}
				inputBindingCollection.Add(inputBinding);
				if (!inputBinding.IsFrozen)
				{
					inputBinding.Freeze();
				}
			}
		}

		/// <summary>Registra um <see cref="T:System.Windows.Input.CommandBinding" /> com o tipo especificado.</summary>
		/// <param name="type">A classe com a qual registrar <paramref name="commandBinding" />.</param>
		/// <param name="commandBinding">A associação do comando a ser registrado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> ou <paramref name="commandBinding" /> é <see langword="null" />.</exception>
		// Token: 0x06000DDA RID: 3546 RVA: 0x00034A20 File Offset: 0x00033E20
		public static void RegisterClassCommandBinding(Type type, CommandBinding commandBinding)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (commandBinding == null)
			{
				throw new ArgumentNullException("commandBinding");
			}
			object syncRoot = CommandManager._classCommandBindings.SyncRoot;
			lock (syncRoot)
			{
				CommandBindingCollection commandBindingCollection = CommandManager._classCommandBindings[type] as CommandBindingCollection;
				if (commandBindingCollection == null)
				{
					commandBindingCollection = new CommandBindingCollection();
					CommandManager._classCommandBindings[type] = commandBindingCollection;
				}
				commandBindingCollection.Add(commandBinding);
			}
		}

		/// <summary>Força o <see cref="T:System.Windows.Input.CommandManager" /> a gerar o evento <see cref="E:System.Windows.Input.CommandManager.RequerySuggested" />.</summary>
		// Token: 0x06000DDB RID: 3547 RVA: 0x00034ABC File Offset: 0x00033EBC
		public static void InvalidateRequerySuggested()
		{
			CommandManager.Current.RaiseRequerySuggested();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00034AD4 File Offset: 0x00033ED4
		private static CommandManager Current
		{
			get
			{
				if (CommandManager._commandManager == null)
				{
					CommandManager._commandManager = new CommandManager();
				}
				return CommandManager._commandManager;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00034AF8 File Offset: 0x00033EF8
		[SecurityCritical]
		internal static void TranslateInput(IInputElement targetElement, InputEventArgs inputEventArgs)
		{
			if (targetElement == null || inputEventArgs == null)
			{
				return;
			}
			ICommand command = null;
			IInputElement inputElement = null;
			object parameter = null;
			DependencyObject o = targetElement as DependencyObject;
			bool flag = InputElement.IsUIElement(o);
			bool flag2 = !flag && InputElement.IsContentElement(o);
			bool flag3 = !flag && !flag2 && InputElement.IsUIElement3D(o);
			InputBindingCollection inputBindingCollection = null;
			if (flag)
			{
				inputBindingCollection = ((UIElement)targetElement).InputBindingsInternal;
			}
			else if (flag2)
			{
				inputBindingCollection = ((ContentElement)targetElement).InputBindingsInternal;
			}
			else if (flag3)
			{
				inputBindingCollection = ((UIElement3D)targetElement).InputBindingsInternal;
			}
			if (inputBindingCollection != null)
			{
				InputBinding inputBinding = inputBindingCollection.FindMatch(targetElement, inputEventArgs);
				if (inputBinding != null)
				{
					command = inputBinding.Command;
					inputElement = inputBinding.CommandTarget;
					parameter = inputBinding.CommandParameter;
				}
			}
			if (command == null)
			{
				object syncRoot = CommandManager._classInputBindings.SyncRoot;
				lock (syncRoot)
				{
					Type type = targetElement.GetType();
					while (type != null)
					{
						InputBindingCollection inputBindingCollection2 = CommandManager._classInputBindings[type] as InputBindingCollection;
						if (inputBindingCollection2 != null)
						{
							InputBinding inputBinding2 = inputBindingCollection2.FindMatch(targetElement, inputEventArgs);
							if (inputBinding2 != null)
							{
								command = inputBinding2.Command;
								inputElement = inputBinding2.CommandTarget;
								parameter = inputBinding2.CommandParameter;
								break;
							}
						}
						type = type.BaseType;
					}
				}
			}
			if (command == null)
			{
				CommandBindingCollection commandBindingCollection = null;
				if (flag)
				{
					commandBindingCollection = ((UIElement)targetElement).CommandBindingsInternal;
				}
				else if (flag2)
				{
					commandBindingCollection = ((ContentElement)targetElement).CommandBindingsInternal;
				}
				else if (flag3)
				{
					commandBindingCollection = ((UIElement3D)targetElement).CommandBindingsInternal;
				}
				if (commandBindingCollection != null)
				{
					command = commandBindingCollection.FindMatch(targetElement, inputEventArgs);
				}
			}
			if (command == null)
			{
				object syncRoot2 = CommandManager._classCommandBindings.SyncRoot;
				lock (syncRoot2)
				{
					Type type2 = targetElement.GetType();
					while (type2 != null)
					{
						CommandBindingCollection commandBindingCollection2 = CommandManager._classCommandBindings[type2] as CommandBindingCollection;
						if (commandBindingCollection2 != null)
						{
							command = commandBindingCollection2.FindMatch(targetElement, inputEventArgs);
							if (command != null)
							{
								break;
							}
						}
						type2 = type2.BaseType;
					}
				}
			}
			if (command != null && command != ApplicationCommands.NotACommand)
			{
				if (inputElement == null)
				{
					inputElement = targetElement;
				}
				bool flag6 = false;
				RoutedCommand routedCommand = command as RoutedCommand;
				if (routedCommand != null)
				{
					if (routedCommand.CriticalCanExecute(parameter, inputElement, inputEventArgs.UserInitiated, out flag6))
					{
						flag6 = false;
						CommandManager.ExecuteCommand(routedCommand, parameter, inputElement, inputEventArgs);
					}
				}
				else if (command.CanExecute(parameter))
				{
					command.Execute(parameter);
				}
				inputEventArgs.Handled = !flag6;
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00034D78 File Offset: 0x00034178
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static bool ExecuteCommand(RoutedCommand routedCommand, object parameter, IInputElement target, InputEventArgs inputEventArgs)
		{
			return routedCommand.ExecuteCore(parameter, target, inputEventArgs.UserInitiated);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00034D94 File Offset: 0x00034194
		internal static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (sender != null && e != null && e.Command != null)
			{
				CommandManager.FindCommandBinding(sender, e, e.Command, false);
				if (!e.Handled && e.RoutedEvent == CommandManager.CanExecuteEvent)
				{
					DependencyObject dependencyObject = sender as DependencyObject;
					if (dependencyObject != null && FocusManager.GetIsFocusScope(dependencyObject))
					{
						IInputElement parentScopeFocusedElement = CommandManager.GetParentScopeFocusedElement(dependencyObject);
						if (parentScopeFocusedElement != null)
						{
							CommandManager.TransferEvent(parentScopeFocusedElement, e);
						}
					}
				}
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00034DF8 File Offset: 0x000341F8
		private static bool CanExecuteCommandBinding(object sender, CanExecuteRoutedEventArgs e, CommandBinding commandBinding)
		{
			commandBinding.OnCanExecute(sender, e);
			return e.CanExecute || e.Handled;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00034E20 File Offset: 0x00034220
		internal static void OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (sender != null && e != null && e.Command != null)
			{
				CommandManager.FindCommandBinding(sender, e, e.Command, true);
				if (!e.Handled && e.RoutedEvent == CommandManager.ExecutedEvent)
				{
					DependencyObject dependencyObject = sender as DependencyObject;
					if (dependencyObject != null && FocusManager.GetIsFocusScope(dependencyObject))
					{
						IInputElement parentScopeFocusedElement = CommandManager.GetParentScopeFocusedElement(dependencyObject);
						if (parentScopeFocusedElement != null)
						{
							CommandManager.TransferEvent(parentScopeFocusedElement, e);
						}
					}
				}
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00034E84 File Offset: 0x00034284
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static bool ExecuteCommandBinding(object sender, ExecutedRoutedEventArgs e, CommandBinding commandBinding)
		{
			ISecureCommand secureCommand = e.Command as ISecureCommand;
			bool flag = e.UserInitiated && secureCommand != null && secureCommand.UserInitiatedPermission != null;
			if (flag)
			{
				secureCommand.UserInitiatedPermission.Assert();
			}
			try
			{
				commandBinding.OnExecuted(sender, e);
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return e.Handled;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00034EFC File Offset: 0x000342FC
		internal static void OnCommandDevice(object sender, CommandDeviceEventArgs e)
		{
			if (sender != null && e != null && e.Command != null)
			{
				CanExecuteRoutedEventArgs canExecuteRoutedEventArgs = new CanExecuteRoutedEventArgs(e.Command, null);
				canExecuteRoutedEventArgs.RoutedEvent = CommandManager.CanExecuteEvent;
				canExecuteRoutedEventArgs.Source = sender;
				CommandManager.OnCanExecute(sender, canExecuteRoutedEventArgs);
				if (canExecuteRoutedEventArgs.CanExecute)
				{
					ExecutedRoutedEventArgs executedRoutedEventArgs = new ExecutedRoutedEventArgs(e.Command, null);
					executedRoutedEventArgs.RoutedEvent = CommandManager.ExecutedEvent;
					executedRoutedEventArgs.Source = sender;
					CommandManager.OnExecuted(sender, executedRoutedEventArgs);
					if (executedRoutedEventArgs.Handled)
					{
						e.Handled = true;
					}
				}
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00034F7C File Offset: 0x0003437C
		private static void FindCommandBinding(object sender, RoutedEventArgs e, ICommand command, bool execute)
		{
			CommandBindingCollection commandBindingCollection = null;
			DependencyObject dependencyObject = sender as DependencyObject;
			if (InputElement.IsUIElement(dependencyObject))
			{
				commandBindingCollection = ((UIElement)dependencyObject).CommandBindingsInternal;
			}
			else if (InputElement.IsContentElement(dependencyObject))
			{
				commandBindingCollection = ((ContentElement)dependencyObject).CommandBindingsInternal;
			}
			else if (InputElement.IsUIElement3D(dependencyObject))
			{
				commandBindingCollection = ((UIElement3D)dependencyObject).CommandBindingsInternal;
			}
			if (commandBindingCollection != null)
			{
				CommandManager.FindCommandBinding(commandBindingCollection, sender, e, command, execute);
			}
			Tuple<Type, CommandBinding> tuple = null;
			List<Tuple<Type, CommandBinding>> list = null;
			object syncRoot = CommandManager._classCommandBindings.SyncRoot;
			lock (syncRoot)
			{
				Type type = sender.GetType();
				while (type != null)
				{
					CommandBindingCollection commandBindingCollection2 = CommandManager._classCommandBindings[type] as CommandBindingCollection;
					if (commandBindingCollection2 != null)
					{
						int num = 0;
						for (;;)
						{
							CommandBinding commandBinding = commandBindingCollection2.FindMatch(command, ref num);
							if (commandBinding == null)
							{
								break;
							}
							if (tuple == null)
							{
								tuple = new Tuple<Type, CommandBinding>(type, commandBinding);
							}
							else
							{
								if (list == null)
								{
									list = new List<Tuple<Type, CommandBinding>>();
									list.Add(tuple);
								}
								list.Add(new Tuple<Type, CommandBinding>(type, commandBinding));
							}
						}
					}
					type = type.BaseType;
				}
			}
			if (list != null)
			{
				ExecutedRoutedEventArgs e2 = execute ? ((ExecutedRoutedEventArgs)e) : null;
				CanExecuteRoutedEventArgs e3 = execute ? null : ((CanExecuteRoutedEventArgs)e);
				for (int i = 0; i < list.Count; i++)
				{
					if ((execute && CommandManager.ExecuteCommandBinding(sender, e2, list[i].Item2)) || (!execute && CommandManager.CanExecuteCommandBinding(sender, e3, list[i].Item2)))
					{
						Type item = list[i].Item1;
						while (++i < list.Count && list[i].Item1 == item)
						{
						}
						i--;
					}
				}
				return;
			}
			if (tuple != null)
			{
				if (execute)
				{
					CommandManager.ExecuteCommandBinding(sender, (ExecutedRoutedEventArgs)e, tuple.Item2);
					return;
				}
				CommandManager.CanExecuteCommandBinding(sender, (CanExecuteRoutedEventArgs)e, tuple.Item2);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003516C File Offset: 0x0003456C
		private static void FindCommandBinding(CommandBindingCollection commandBindings, object sender, RoutedEventArgs e, ICommand command, bool execute)
		{
			int num = 0;
			CommandBinding commandBinding;
			do
			{
				commandBinding = commandBindings.FindMatch(command, ref num);
			}
			while (commandBinding != null && (!execute || !CommandManager.ExecuteCommandBinding(sender, (ExecutedRoutedEventArgs)e, commandBinding)) && (execute || !CommandManager.CanExecuteCommandBinding(sender, (CanExecuteRoutedEventArgs)e, commandBinding)));
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000351B0 File Offset: 0x000345B0
		private static void TransferEvent(IInputElement newSource, CanExecuteRoutedEventArgs e)
		{
			RoutedCommand routedCommand = e.Command as RoutedCommand;
			if (routedCommand != null)
			{
				try
				{
					e.CanExecute = routedCommand.CanExecute(e.Parameter, newSource);
				}
				finally
				{
					e.Handled = true;
				}
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00035208 File Offset: 0x00034608
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static void TransferEvent(IInputElement newSource, ExecutedRoutedEventArgs e)
		{
			RoutedCommand routedCommand = e.Command as RoutedCommand;
			if (routedCommand != null)
			{
				try
				{
					routedCommand.ExecuteCore(e.Parameter, newSource, e.UserInitiated);
				}
				finally
				{
					e.Handled = true;
				}
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00035260 File Offset: 0x00034660
		private static IInputElement GetParentScopeFocusedElement(DependencyObject childScope)
		{
			DependencyObject parentScope = CommandManager.GetParentScope(childScope);
			if (parentScope != null)
			{
				IInputElement focusedElement = FocusManager.GetFocusedElement(parentScope);
				if (focusedElement != null && !CommandManager.ContainsElement(childScope, focusedElement as DependencyObject))
				{
					return focusedElement;
				}
			}
			return null;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00035294 File Offset: 0x00034694
		private static DependencyObject GetParentScope(DependencyObject childScope)
		{
			DependencyObject dependencyObject = null;
			UIElement uielement = childScope as UIElement;
			ContentElement contentElement = (uielement == null) ? (childScope as ContentElement) : null;
			UIElement3D uielement3D = (uielement == null && contentElement == null) ? (childScope as UIElement3D) : null;
			if (uielement != null)
			{
				dependencyObject = uielement.GetUIParent(true);
			}
			else if (contentElement != null)
			{
				dependencyObject = contentElement.GetUIParent(true);
			}
			else if (uielement3D != null)
			{
				dependencyObject = uielement3D.GetUIParent(true);
			}
			if (dependencyObject != null)
			{
				return FocusManager.GetFocusScope(dependencyObject);
			}
			return null;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000352F8 File Offset: 0x000346F8
		private static bool ContainsElement(DependencyObject scope, DependencyObject child)
		{
			if (child != null)
			{
				for (DependencyObject dependencyObject = FocusManager.GetFocusScope(child); dependencyObject != null; dependencyObject = CommandManager.GetParentScope(dependencyObject))
				{
					if (dependencyObject == scope)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00035324 File Offset: 0x00034724
		private CommandManager()
		{
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00035338 File Offset: 0x00034738
		private void RaiseRequerySuggested()
		{
			if (this._requerySuggestedOperation == null)
			{
				Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
				if (currentDispatcher != null && !currentDispatcher.HasShutdownStarted && !currentDispatcher.HasShutdownFinished)
				{
					this._requerySuggestedOperation = currentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(this.RaiseRequerySuggested), null);
				}
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00035380 File Offset: 0x00034780
		private object RaiseRequerySuggested(object obj)
		{
			this._requerySuggestedOperation = null;
			if (this.PrivateRequerySuggested != null)
			{
				this.PrivateRequerySuggested(null, EventArgs.Empty);
			}
			return null;
		}

		// Token: 0x1400014B RID: 331
		// (add) Token: 0x06000DEE RID: 3566 RVA: 0x000353B0 File Offset: 0x000347B0
		// (remove) Token: 0x06000DEF RID: 3567 RVA: 0x000353E8 File Offset: 0x000347E8
		private event EventHandler PrivateRequerySuggested;

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.CommandManager.PreviewExecuted" /> anexado.</summary>
		// Token: 0x04000821 RID: 2081
		public static readonly RoutedEvent PreviewExecutedEvent = EventManager.RegisterRoutedEvent("PreviewExecuted", RoutingStrategy.Tunnel, typeof(ExecutedRoutedEventHandler), typeof(CommandManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.CommandManager.Executed" /> anexado.</summary>
		// Token: 0x04000822 RID: 2082
		public static readonly RoutedEvent ExecutedEvent = EventManager.RegisterRoutedEvent("Executed", RoutingStrategy.Bubble, typeof(ExecutedRoutedEventHandler), typeof(CommandManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.CommandManager.PreviewCanExecute" /> anexado.</summary>
		// Token: 0x04000823 RID: 2083
		public static readonly RoutedEvent PreviewCanExecuteEvent = EventManager.RegisterRoutedEvent("PreviewCanExecute", RoutingStrategy.Tunnel, typeof(CanExecuteRoutedEventHandler), typeof(CommandManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.CommandManager.CanExecute" /> anexado.</summary>
		// Token: 0x04000824 RID: 2084
		public static readonly RoutedEvent CanExecuteEvent = EventManager.RegisterRoutedEvent("CanExecute", RoutingStrategy.Bubble, typeof(CanExecuteRoutedEventHandler), typeof(CommandManager));

		// Token: 0x04000825 RID: 2085
		[ThreadStatic]
		private static CommandManager _commandManager;

		// Token: 0x04000826 RID: 2086
		private static HybridDictionary _classCommandBindings = new HybridDictionary();

		// Token: 0x04000827 RID: 2087
		private static HybridDictionary _classInputBindings = new HybridDictionary();

		// Token: 0x04000828 RID: 2088
		private DispatcherOperation _requerySuggestedOperation;

		// Token: 0x02000803 RID: 2051
		private class RequerySuggestedEventManager : WeakEventManager
		{
			// Token: 0x060055FA RID: 22010 RVA: 0x00161CD0 File Offset: 0x001610D0
			private RequerySuggestedEventManager()
			{
			}

			// Token: 0x060055FB RID: 22011 RVA: 0x00161CE4 File Offset: 0x001610E4
			public static void AddHandler(CommandManager source, EventHandler handler)
			{
				if (handler == null)
				{
					return;
				}
				CommandManager.RequerySuggestedEventManager.CurrentManager.ProtectedAddHandler(source, handler);
			}

			// Token: 0x060055FC RID: 22012 RVA: 0x00161D04 File Offset: 0x00161104
			public static void RemoveHandler(CommandManager source, EventHandler handler)
			{
				if (handler == null)
				{
					return;
				}
				CommandManager.RequerySuggestedEventManager.CurrentManager.ProtectedRemoveHandler(source, handler);
			}

			// Token: 0x060055FD RID: 22013 RVA: 0x00161D24 File Offset: 0x00161124
			protected override WeakEventManager.ListenerList NewListenerList()
			{
				return new WeakEventManager.ListenerList();
			}

			// Token: 0x060055FE RID: 22014 RVA: 0x00161D38 File Offset: 0x00161138
			protected override void StartListening(object source)
			{
				CommandManager commandManager = CommandManager.Current;
				commandManager.PrivateRequerySuggested += this.OnRequerySuggested;
			}

			// Token: 0x060055FF RID: 22015 RVA: 0x00161D60 File Offset: 0x00161160
			protected override void StopListening(object source)
			{
				CommandManager commandManager = CommandManager.Current;
				commandManager.PrivateRequerySuggested -= this.OnRequerySuggested;
			}

			// Token: 0x170011A1 RID: 4513
			// (get) Token: 0x06005600 RID: 22016 RVA: 0x00161D88 File Offset: 0x00161188
			private static CommandManager.RequerySuggestedEventManager CurrentManager
			{
				get
				{
					Type typeFromHandle = typeof(CommandManager.RequerySuggestedEventManager);
					CommandManager.RequerySuggestedEventManager requerySuggestedEventManager = (CommandManager.RequerySuggestedEventManager)WeakEventManager.GetCurrentManager(typeFromHandle);
					if (requerySuggestedEventManager == null)
					{
						requerySuggestedEventManager = new CommandManager.RequerySuggestedEventManager();
						WeakEventManager.SetCurrentManager(typeFromHandle, requerySuggestedEventManager);
					}
					return requerySuggestedEventManager;
				}
			}

			// Token: 0x06005601 RID: 22017 RVA: 0x00161DC0 File Offset: 0x001611C0
			private void OnRequerySuggested(object sender, EventArgs args)
			{
				base.DeliverEvent(sender, args);
			}
		}
	}
}
