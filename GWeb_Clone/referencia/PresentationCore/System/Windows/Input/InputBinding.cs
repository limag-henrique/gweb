using System;
using System.ComponentModel;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	/// <summary>Representa uma associação entre um <see cref="T:System.Windows.Input.InputGesture" /> e um comando. O comando é potencialmente um <see cref="T:System.Windows.Input.RoutedCommand" />.</summary>
	// Token: 0x02000211 RID: 529
	public class InputBinding : Freezable, ICommandSource
	{
		/// <summary>Fornece a inicialização de base para classes derivadas de <see cref="T:System.Windows.Input.InputBinding" />.</summary>
		// Token: 0x06000DFC RID: 3580 RVA: 0x00035550 File Offset: 0x00034950
		protected InputBinding()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputBinding" /> com o comando e o gesto de entrada especificados.</summary>
		/// <param name="command">O comando a ser associado a <paramref name="gesture" />.</param>
		/// <param name="gesture">O gesto de entrada a associar ao <paramref name="command" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="command" /> ou <paramref name="gesture" /> é <see langword="null" />.</exception>
		// Token: 0x06000DFD RID: 3581 RVA: 0x00035564 File Offset: 0x00034964
		[SecurityCritical]
		public InputBinding(ICommand command, InputGesture gesture)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (gesture == null)
			{
				throw new ArgumentNullException("gesture");
			}
			this.CheckSecureCommand(command, gesture);
			this.Command = command;
			this._gesture = gesture;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.ICommand" /> associado a esta associação de entrada.</summary>
		/// <returns>O comando associado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O valor <see cref="P:System.Windows.Input.InputBinding.Command" /> é <see langword="null" />.</exception>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000355AC File Offset: 0x000349AC
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x000355CC File Offset: 0x000349CC
		[Localizability(LocalizationCategory.NeverLocalize)]
		[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
		public ICommand Command
		{
			get
			{
				return (ICommand)base.GetValue(InputBinding.CommandProperty);
			}
			set
			{
				base.SetValue(InputBinding.CommandProperty, value);
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000355E8 File Offset: 0x000349E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			InputBinding inputBinding = (InputBinding)d;
			inputBinding.CheckSecureCommand((ICommand)e.NewValue, inputBinding.Gesture);
		}

		/// <summary>Obtém ou define os dados específicos do comando para um determinado comando.</summary>
		/// <returns>Os dados específicos do comando. O padrão é <see langword="null" />.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00035614 File Offset: 0x00034A14
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0003562C File Offset: 0x00034A2C
		public object CommandParameter
		{
			get
			{
				return base.GetValue(InputBinding.CommandParameterProperty);
			}
			set
			{
				base.SetValue(InputBinding.CommandParameterProperty, value);
			}
		}

		/// <summary>Obtém ou define o elemento de destino do comando.</summary>
		/// <returns>O destino do comando. O padrão é <see langword="null" />.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00035648 File Offset: 0x00034A48
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00035668 File Offset: 0x00034A68
		public IInputElement CommandTarget
		{
			get
			{
				return (IInputElement)base.GetValue(InputBinding.CommandTargetProperty);
			}
			set
			{
				base.SetValue(InputBinding.CommandTargetProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.InputGesture" /> associado a esta associação de entrada.</summary>
		/// <returns>O gesto associado. O padrão é <see langword="null" />.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00035684 File Offset: 0x00034A84
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x000356A0 File Offset: 0x00034AA0
		public virtual InputGesture Gesture
		{
			get
			{
				base.ReadPreamble();
				return this._gesture;
			}
			[SecurityCritical]
			set
			{
				base.WritePreamble();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				object dataLock = InputBinding._dataLock;
				lock (dataLock)
				{
					this.CheckSecureCommand(this.Command, value);
					this._gesture = value;
				}
				base.WritePostscript();
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00035714 File Offset: 0x00034B14
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CheckSecureCommand(ICommand command, InputGesture gesture)
		{
			ISecureCommand secureCommand = command as ISecureCommand;
			if (secureCommand != null && command != ApplicationCommands.Cut && command != ApplicationCommands.Copy)
			{
				secureCommand.UserInitiatedPermission.Demand();
			}
		}

		/// <summary>Cria uma instância de um <see cref="T:System.Windows.Input.InputBinding" />.</summary>
		/// <returns>O novo objeto.</returns>
		// Token: 0x06000E08 RID: 3592 RVA: 0x00035748 File Offset: 0x00034B48
		protected override Freezable CreateInstanceCore()
		{
			return new InputBinding();
		}

		/// <summary>Copia os valores base (não animados) das propriedades do objeto especificado.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E09 RID: 3593 RVA: 0x0003575C File Offset: 0x00034B5C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			base.CloneCore(sourceFreezable);
			this._gesture = ((InputBinding)sourceFreezable).Gesture;
		}

		/// <summary>Copia os valores atuais das propriedades do objeto especificado.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E0A RID: 3594 RVA: 0x00035784 File Offset: 0x00034B84
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			base.CloneCurrentValueCore(sourceFreezable);
			this._gesture = ((InputBinding)sourceFreezable).Gesture;
		}

		/// <summary>Torna a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E0B RID: 3595 RVA: 0x000357AC File Offset: 0x00034BAC
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetAsFrozenCore(sourceFreezable);
			this._gesture = ((InputBinding)sourceFreezable).Gesture;
		}

		/// <summary>Torna a instância atual um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E0C RID: 3596 RVA: 0x000357D4 File Offset: 0x00034BD4
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this._gesture = ((InputBinding)sourceFreezable).Gesture;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000357FC File Offset: 0x00034BFC
		internal override DependencyObject InheritanceContext
		{
			get
			{
				return this._inheritanceContext;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00035810 File Offset: 0x00034C10
		internal override void AddInheritanceContext(DependencyObject context, DependencyProperty property)
		{
			InheritanceContextHelper.AddInheritanceContext(context, this, ref this._hasMultipleInheritanceContexts, ref this._inheritanceContext);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00035830 File Offset: 0x00034C30
		internal override void RemoveInheritanceContext(DependencyObject context, DependencyProperty property)
		{
			InheritanceContextHelper.RemoveInheritanceContext(context, this, ref this._hasMultipleInheritanceContexts, ref this._inheritanceContext);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00035850 File Offset: 0x00034C50
		internal override bool HasMultipleInheritanceContexts
		{
			get
			{
				return this._hasMultipleInheritanceContexts;
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.InputBinding.Command" />.</summary>
		// Token: 0x0400082C RID: 2092
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InputBinding), new UIPropertyMetadata(null, new PropertyChangedCallback(InputBinding.OnCommandPropertyChanged)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.InputBinding.CommandParameter" />.</summary>
		// Token: 0x0400082D RID: 2093
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InputBinding));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.InputBinding.CommandTarget" />.</summary>
		// Token: 0x0400082E RID: 2094
		public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(InputBinding));

		// Token: 0x0400082F RID: 2095
		private InputGesture _gesture;

		// Token: 0x04000830 RID: 2096
		internal static object _dataLock = new object();

		// Token: 0x04000831 RID: 2097
		private DependencyObject _inheritanceContext;

		// Token: 0x04000832 RID: 2098
		private bool _hasMultipleInheritanceContexts;
	}
}
