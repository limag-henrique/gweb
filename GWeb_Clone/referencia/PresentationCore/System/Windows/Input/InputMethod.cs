using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Fornece recursos para gerenciar e interagir com a Estrutura de Serviços de Texto, que dá suporte para métodos de entrada de texto alternativos como fala e manuscrito.</summary>
	// Token: 0x02000252 RID: 594
	public class InputMethod : DispatcherObject
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x0003E978 File Offset: 0x0003DD78
		internal InputMethod()
		{
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" /> é definida.</param>
		/// <param name="value">O novo valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010A7 RID: 4263 RVA: 0x0003E98C File Offset: 0x0003DD8C
		public static void SetIsInputMethodEnabled(DependencyObject target, bool value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.IsInputMethodEnabledProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" />.</param>
		/// <returns>O valor atual do <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010A8 RID: 4264 RVA: 0x0003E9B4 File Offset: 0x0003DDB4
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetIsInputMethodEnabled(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (bool)target.GetValue(InputMethod.IsInputMethodEnabledProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" /> é definida.</param>
		/// <param name="value">O novo valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010A9 RID: 4265 RVA: 0x0003E9E0 File Offset: 0x0003DDE0
		public static void SetIsInputMethodSuspended(DependencyObject target, bool value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.IsInputMethodSuspendedProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" />.</param>
		/// <returns>O valor atual do <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AA RID: 4266 RVA: 0x0003EA08 File Offset: 0x0003DE08
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetIsInputMethodSuspended(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (bool)target.GetValue(InputMethod.IsInputMethodSuspendedProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" /> é definida.</param>
		/// <param name="value">Um membro da enumeração <see cref="T:System.Windows.Input.ImeConversionModeValues" /> que representa o novo valor para a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AB RID: 4267 RVA: 0x0003EA34 File Offset: 0x0003DE34
		public static void SetPreferredImeState(DependencyObject target, InputMethodState value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.PreferredImeStateProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" />.</param>
		/// <returns>Um membro da enumeração <see cref="T:System.Windows.Input.InputMethodState" /> especificando o atual <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AC RID: 4268 RVA: 0x0003EA60 File Offset: 0x0003DE60
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static InputMethodState GetPreferredImeState(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (InputMethodState)target.GetValue(InputMethod.PreferredImeStateProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" /> é definida.</param>
		/// <param name="value">Um membro da enumeração <see cref="T:System.Windows.Input.ImeConversionModeValues" /> que representa o novo valor para a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AD RID: 4269 RVA: 0x0003EA8C File Offset: 0x0003DE8C
		public static void SetPreferredImeConversionMode(DependencyObject target, ImeConversionModeValues value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.PreferredImeConversionModeProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" />.</param>
		/// <returns>Um membro da enumeração <see cref="T:System.Windows.Input.ImeConversionModeValues" /> especificando o atual <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AE RID: 4270 RVA: 0x0003EAB8 File Offset: 0x0003DEB8
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static ImeConversionModeValues GetPreferredImeConversionMode(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (ImeConversionModeValues)target.GetValue(InputMethod.PreferredImeConversionModeProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" /> é definida.</param>
		/// <param name="value">Um membro da enumeração <see cref="T:System.Windows.Input.ImeConversionModeValues" /> que representa o novo valor para a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010AF RID: 4271 RVA: 0x0003EAE4 File Offset: 0x0003DEE4
		public static void SetPreferredImeSentenceMode(DependencyObject target, ImeSentenceModeValues value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.PreferredImeSentenceModeProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" />.</param>
		/// <returns>Um membro da enumeração <see cref="T:System.Windows.Input.ImeSentenceModeValues" /> especificando o atual <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010B0 RID: 4272 RVA: 0x0003EB10 File Offset: 0x0003DF10
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static ImeSentenceModeValues GetPreferredImeSentenceMode(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (ImeSentenceModeValues)target.GetValue(InputMethod.PreferredImeSentenceModeProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.InputScope" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.InputScope" /> é definida.</param>
		/// <param name="value">Um objeto <see cref="T:System.Windows.Input.InputScope" /> que representa o novo valor para a propriedade anexada <see cref="P:System.Windows.Input.InputMethod.InputScope" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010B1 RID: 4273 RVA: 0x0003EB3C File Offset: 0x0003DF3C
		public static void SetInputScope(DependencyObject target, InputScope value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputMethod.InputScopeProperty, value);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputMethod.InputScope" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o escopo de entrada.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.Input.InputScope" /> que representa o escopo de entrada atual para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="target" /> é null.</exception>
		// Token: 0x060010B2 RID: 4274 RVA: 0x0003EB64 File Offset: 0x0003DF64
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static InputScope GetInputScope(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (InputScope)target.GetValue(InputMethod.InputScopeProperty);
		}

		/// <summary>Obtém uma referência a qualquer método de entrada ativo atualmente associado ao contexto atual.</summary>
		/// <returns>Uma referência a um <see cref="T:System.Windows.Input.InputMethod" /> objeto associado ao contexto atual, ou nulo se não houver nenhum método de entrada ativo.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003EB90 File Offset: 0x0003DF90
		public static InputMethod Current
		{
			get
			{
				InputMethod inputMethod = null;
				Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
				if (dispatcher != null)
				{
					inputMethod = (dispatcher.InputMethod as InputMethod);
					if (inputMethod == null)
					{
						inputMethod = new InputMethod();
						dispatcher.InputMethod = inputMethod;
					}
				}
				return inputMethod;
			}
		}

		/// <summary>Exibe o UI (interface do usuário) de configuração associado ao serviço de texto do teclado ativo no momento.</summary>
		// Token: 0x060010B4 RID: 4276 RVA: 0x0003EBCC File Offset: 0x0003DFCC
		public void ShowConfigureUI()
		{
			this.ShowConfigureUI(null);
		}

		/// <summary>Exibe o UI (interface do usuário) de configuração associado ao serviço de texto do teclado ativo no momento, usando um <see cref="T:System.Windows.UIElement" /> especificado como o elemento pai do Interface de Usuário de configuração.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> que será usado como o elemento pai para o Interface de Usuário de configuração.  Esse parâmetro pode ser <see langword="null" />.</param>
		// Token: 0x060010B5 RID: 4277 RVA: 0x0003EBE0 File Offset: 0x0003DFE0
		public void ShowConfigureUI(UIElement element)
		{
			this._ShowConfigureUI(element, true);
		}

		/// <summary>Exibe o UI (interface do usuário) de registro de palavras associado ao serviço de texto do teclado ativo no momento.</summary>
		// Token: 0x060010B6 RID: 4278 RVA: 0x0003EBF8 File Offset: 0x0003DFF8
		public void ShowRegisterWordUI()
		{
			this.ShowRegisterWordUI("");
		}

		/// <summary>Exibe o UI (interface do usuário) de registro de palavras associado ao serviço de texto do teclado ativo no momento.  Aceita uma cadeia de caracteres especificada como o valor padrão a ser registrado.</summary>
		/// <param name="registeredText">Uma cadeia de caracteres que especifica um valor a ser registrado.</param>
		// Token: 0x060010B7 RID: 4279 RVA: 0x0003EC10 File Offset: 0x0003E010
		public void ShowRegisterWordUI(string registeredText)
		{
			this.ShowRegisterWordUI(null, registeredText);
		}

		/// <summary>Exibe o UI (interface do usuário) de registro de palavras associado ao serviço de texto do teclado ativo no momento.  Aceita uma cadeia de caracteres especificada como o valor padrão a ser registrado, bem como um <see cref="T:System.Windows.UIElement" /> especificado como o elemento pai para o Interface de Usuário de configuração.</summary>
		/// <param name="element">Um <see cref="T:System.Windows.UIElement" /> que será usado como o elemento pai para o Interface de Usuário de registro de palavras.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="registeredText">Uma cadeia de caracteres que especifica um valor a ser registrado.</param>
		// Token: 0x060010B8 RID: 4280 RVA: 0x0003EC28 File Offset: 0x0003E028
		public void ShowRegisterWordUI(UIElement element, string registeredText)
		{
			this._ShowRegisterWordUI(element, true, registeredText);
		}

		/// <summary>Obtém ou define o estado atual para o editor do método de entrada associado a este método de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.InputMethodState" /> enumeração que especifica o estado do editor de método de entrada associado com esse método de entrada.  
		/// O valor padrão é <see cref="F:System.Windows.Input.InputMethodState.Off" />.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003EC40 File Offset: 0x0003E040
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0003ECC0 File Offset: 0x0003E0C0
		public InputMethodState ImeState
		{
			[SecurityCritical]
			get
			{
				if (!InputMethod.IsImm32ImeCurrent())
				{
					TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeState);
					if (compartment != null)
					{
						if (!compartment.BooleanValue)
						{
							return InputMethodState.Off;
						}
						return InputMethodState.On;
					}
				}
				else
				{
					IntPtr intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
					if (intPtr != IntPtr.Zero)
					{
						IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
						bool flag = UnsafeNativeMethods.ImmGetOpenStatus(new HandleRef(this, handle));
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
						if (!flag)
						{
							return InputMethodState.Off;
						}
						return InputMethodState.On;
					}
				}
				return InputMethodState.Off;
			}
			[SecurityCritical]
			set
			{
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeState);
				if (compartment != null && compartment.BooleanValue != (value == InputMethodState.On))
				{
					compartment.BooleanValue = (value == InputMethodState.On);
				}
				if (InputMethod._immEnabled)
				{
					IntPtr intPtr = IntPtr.Zero;
					intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
					if (intPtr != IntPtr.Zero)
					{
						IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
						bool flag = UnsafeNativeMethods.ImmGetOpenStatus(new HandleRef(this, handle));
						if (flag != (value == InputMethodState.On))
						{
							UnsafeNativeMethods.ImmSetOpenStatus(new HandleRef(this, handle), value == InputMethodState.On);
						}
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
					}
				}
			}
		}

		/// <summary>Obtém ou define o estado atual da entrada do microfone para esse método de entrada.</summary>
		/// <returns>Um membro da enumeração <see cref="T:System.Windows.Input.InputMethodState" /> que especifica o estado do método de entrada atual para a entrada do microfone.  
		/// O valor padrão é <see cref="F:System.Windows.Input.InputMethodState.Off" />.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003ED60 File Offset: 0x0003E160
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0003ED8C File Offset: 0x0003E18C
		public InputMethodState MicrophoneState
		{
			[SecurityCritical]
			get
			{
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.MicrophoneState);
				if (compartment == null)
				{
					return InputMethodState.Off;
				}
				if (!compartment.BooleanValue)
				{
					return InputMethodState.Off;
				}
				return InputMethodState.On;
			}
			[SecurityCritical]
			set
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.MicrophoneState);
				if (compartment != null && compartment.BooleanValue != (value == InputMethodState.On))
				{
					compartment.BooleanValue = (value == InputMethodState.On);
				}
			}
		}

		/// <summary>Obtém ou define o estado atual da entrada do manuscrito para esse método de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.InputMethodState" /> enumeração que especifica o estado atual do método de entrada para a entrada de manuscrito.  
		/// O valor padrão é <see cref="F:System.Windows.Input.InputMethodState.Off" />.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003EDC4 File Offset: 0x0003E1C4
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x0003EDF0 File Offset: 0x0003E1F0
		public InputMethodState HandwritingState
		{
			[SecurityCritical]
			get
			{
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.HandwritingState);
				if (compartment == null)
				{
					return InputMethodState.Off;
				}
				if (!compartment.BooleanValue)
				{
					return InputMethodState.Off;
				}
				return InputMethodState.On;
			}
			[SecurityCritical]
			set
			{
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.HandwritingState);
				if (compartment != null && compartment.BooleanValue != (value == InputMethodState.On))
				{
					compartment.BooleanValue = (value == InputMethodState.On);
				}
			}
		}

		/// <summary>Obtém ou define o modo de fala para esse método de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.SpeechMode" /> enumeração que especifica o modo de conversão de fala atual.  
		/// O valor padrão é <see cref="F:System.Windows.Input.SpeechMode.Indeterminate" />.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003EE24 File Offset: 0x0003E224
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0003EE58 File Offset: 0x0003E258
		public SpeechMode SpeechMode
		{
			[SecurityCritical]
			get
			{
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.SpeechMode);
				if (compartment != null)
				{
					int intValue = compartment.IntValue;
					if ((intValue & 1) != 0)
					{
						return SpeechMode.Dictation;
					}
					if ((intValue & 8) != 0)
					{
						return SpeechMode.Command;
					}
				}
				return SpeechMode.Indeterminate;
			}
			[SecurityCritical]
			set
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.SpeechMode);
				if (compartment != null)
				{
					int num = compartment.IntValue;
					if (value == SpeechMode.Dictation)
					{
						num &= -9;
						num |= 1;
						if (compartment.IntValue != num)
						{
							compartment.IntValue = num;
							return;
						}
					}
					else if (value == SpeechMode.Command)
					{
						num &= -2;
						num |= 8;
						if (compartment.IntValue != num)
						{
							compartment.IntValue = num;
						}
					}
				}
			}
		}

		/// <summary>Obtém ou define o modo de conversão atual para o editor do método de entrada associado a este método de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.ImeConversionModeValues" /> enumeração que especifica o modo de conversão.  
		/// O valor padrão é <see cref="F:System.Windows.Input.ImeConversionModeValues.Alphanumeric" />.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003EEBC File Offset: 0x0003E2BC
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0003F04C File Offset: 0x0003E44C
		public ImeConversionModeValues ImeConversionMode
		{
			[SecurityCritical]
			get
			{
				if (!InputMethod.IsImm32ImeCurrent())
				{
					TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeConversionModeValues);
					if (compartment != null)
					{
						UnsafeNativeMethods.ConversionModeFlags intValue = (UnsafeNativeMethods.ConversionModeFlags)compartment.IntValue;
						ImeConversionModeValues imeConversionModeValues = (ImeConversionModeValues)0;
						if ((intValue & (UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA)) == UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Alphanumeric;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Native;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Katakana;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.FullShape;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ROMAN) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Roman;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_CHARCODE) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.CharCode;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NOCONVERSION) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.NoConversion;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_EUDC) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Eudc;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_SYMBOL) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Symbol;
						}
						if ((intValue & UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FIXED) != UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							imeConversionModeValues |= ImeConversionModeValues.Fixed;
						}
						return imeConversionModeValues;
					}
				}
				else
				{
					IntPtr intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
					if (intPtr != IntPtr.Zero)
					{
						int num = 0;
						int num2 = 0;
						IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
						UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(this, handle), ref num, ref num2);
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
						ImeConversionModeValues imeConversionModeValues2 = (ImeConversionModeValues)0;
						if ((num & 3) == 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Alphanumeric;
						}
						if ((num & 1) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Native;
						}
						if ((num & 2) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Katakana;
						}
						if ((num & 8) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.FullShape;
						}
						if ((num & 16) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Roman;
						}
						if ((num & 32) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.CharCode;
						}
						if ((num & 256) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.NoConversion;
						}
						if ((num & 512) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Eudc;
						}
						if ((num & 1024) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Symbol;
						}
						if ((num & 2048) != 0)
						{
							imeConversionModeValues2 |= ImeConversionModeValues.Fixed;
						}
						return imeConversionModeValues2;
					}
				}
				return ImeConversionModeValues.Alphanumeric;
			}
			[SecurityCritical]
			set
			{
				if (!this.IsValidConversionMode(value))
				{
					throw new ArgumentException(SR.Get("InputMethod_InvalidConversionMode", new object[]
					{
						value
					}));
				}
				IntPtr intPtr = IntPtr.Zero;
				if (InputMethod._immEnabled)
				{
					intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
				}
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeConversionModeValues);
				if (compartment != null)
				{
					UnsafeNativeMethods.ConversionModeFlags conversionModeFlags;
					if (InputMethod._immEnabled)
					{
						conversionModeFlags = this.Imm32ConversionModeToTSFConversionMode(intPtr);
					}
					else
					{
						conversionModeFlags = (UnsafeNativeMethods.ConversionModeFlags)compartment.IntValue;
					}
					UnsafeNativeMethods.ConversionModeFlags conversionModeFlags2 = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC;
					if ((value & ImeConversionModeValues.Native) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE;
					}
					if ((value & ImeConversionModeValues.Katakana) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA;
					}
					if ((value & ImeConversionModeValues.FullShape) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE;
					}
					if ((value & ImeConversionModeValues.Roman) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ROMAN;
					}
					if ((value & ImeConversionModeValues.CharCode) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_CHARCODE;
					}
					if ((value & ImeConversionModeValues.NoConversion) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NOCONVERSION;
					}
					if ((value & ImeConversionModeValues.Eudc) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_EUDC;
					}
					if ((value & ImeConversionModeValues.Symbol) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_SYMBOL;
					}
					if ((value & ImeConversionModeValues.Fixed) != (ImeConversionModeValues)0)
					{
						conversionModeFlags2 |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FIXED;
					}
					if (conversionModeFlags != conversionModeFlags2)
					{
						UnsafeNativeMethods.ConversionModeFlags conversionModeFlags3 = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC;
						if (conversionModeFlags2 == (UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE))
						{
							conversionModeFlags3 = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA;
						}
						else if (conversionModeFlags2 == (UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA))
						{
							conversionModeFlags3 = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE;
						}
						else if (conversionModeFlags2 == UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE)
						{
							conversionModeFlags3 = (UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA);
						}
						else if (conversionModeFlags2 == UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC)
						{
							conversionModeFlags3 = (UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA | UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE);
						}
						else if (conversionModeFlags2 == UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE)
						{
							conversionModeFlags3 = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE;
						}
						conversionModeFlags2 |= conversionModeFlags;
						conversionModeFlags2 &= ~conversionModeFlags3;
						compartment.IntValue = (int)conversionModeFlags2;
					}
				}
				if (InputMethod._immEnabled && intPtr != IntPtr.Zero)
				{
					int num = 0;
					int sentence = 0;
					IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
					UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(this, handle), ref num, ref sentence);
					int num2 = 0;
					if ((value & ImeConversionModeValues.Native) != (ImeConversionModeValues)0)
					{
						num2 |= 1;
					}
					if ((value & ImeConversionModeValues.Katakana) != (ImeConversionModeValues)0)
					{
						num2 |= 2;
					}
					if ((value & ImeConversionModeValues.FullShape) != (ImeConversionModeValues)0)
					{
						num2 |= 8;
					}
					if ((value & ImeConversionModeValues.Roman) != (ImeConversionModeValues)0)
					{
						num2 |= 16;
					}
					if ((value & ImeConversionModeValues.CharCode) != (ImeConversionModeValues)0)
					{
						num2 |= 32;
					}
					if ((value & ImeConversionModeValues.NoConversion) != (ImeConversionModeValues)0)
					{
						num2 |= 256;
					}
					if ((value & ImeConversionModeValues.Eudc) != (ImeConversionModeValues)0)
					{
						num2 |= 512;
					}
					if ((value & ImeConversionModeValues.Symbol) != (ImeConversionModeValues)0)
					{
						num2 |= 1024;
					}
					if ((value & ImeConversionModeValues.Fixed) != (ImeConversionModeValues)0)
					{
						num2 |= 2048;
					}
					if (num != num2)
					{
						int num3 = 0;
						if (num2 == 9)
						{
							num3 = 2;
						}
						else if (num2 == 3)
						{
							num3 = 8;
						}
						else if (num2 == 8)
						{
							num3 = 3;
						}
						else if (num2 == 0)
						{
							num3 = 11;
						}
						else if (num2 == 1)
						{
							num3 = 8;
						}
						num2 |= num;
						num2 &= ~num3;
						UnsafeNativeMethods.ImmSetConversionStatus(new HandleRef(this, handle), num2, sentence);
					}
					UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
				}
			}
		}

		/// <summary>Obtém ou define o modo de sentença atual para o editor do método de entrada associado a este método de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.ImeSentenceModeValues" /> enumerações especificando o modo de sentença.  
		/// O valor padrão é <see cref="F:System.Windows.Input.ImeSentenceModeValues.None" />.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0003F28C File Offset: 0x0003E68C
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x0003F388 File Offset: 0x0003E788
		public ImeSentenceModeValues ImeSentenceMode
		{
			[SecurityCritical]
			get
			{
				if (!InputMethod.IsImm32ImeCurrent())
				{
					TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeSentenceModeValues);
					if (compartment != null)
					{
						UnsafeNativeMethods.SentenceModeFlags intValue = (UnsafeNativeMethods.SentenceModeFlags)compartment.IntValue;
						ImeSentenceModeValues imeSentenceModeValues = ImeSentenceModeValues.None;
						if (intValue == UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							return ImeSentenceModeValues.None;
						}
						if ((intValue & UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_PLAURALCLAUSE) != UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							imeSentenceModeValues |= ImeSentenceModeValues.PluralClause;
						}
						if ((intValue & UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_SINGLECONVERT) != UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							imeSentenceModeValues |= ImeSentenceModeValues.SingleConversion;
						}
						if ((intValue & UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_AUTOMATIC) != UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							imeSentenceModeValues |= ImeSentenceModeValues.Automatic;
						}
						if ((intValue & UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_PHRASEPREDICT) != UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							imeSentenceModeValues |= ImeSentenceModeValues.PhrasePrediction;
						}
						if ((intValue & UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_CONVERSATION) != UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE)
						{
							imeSentenceModeValues |= ImeSentenceModeValues.Conversation;
						}
						return imeSentenceModeValues;
					}
				}
				else
				{
					IntPtr intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
					if (intPtr != IntPtr.Zero)
					{
						ImeSentenceModeValues imeSentenceModeValues2 = ImeSentenceModeValues.None;
						int num = 0;
						int num2 = 0;
						IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
						UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(this, handle), ref num, ref num2);
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
						if (num2 == 0)
						{
							return ImeSentenceModeValues.None;
						}
						if ((num2 & 1) != 0)
						{
							imeSentenceModeValues2 |= ImeSentenceModeValues.PluralClause;
						}
						if ((num2 & 2) != 0)
						{
							imeSentenceModeValues2 |= ImeSentenceModeValues.SingleConversion;
						}
						if ((num2 & 4) != 0)
						{
							imeSentenceModeValues2 |= ImeSentenceModeValues.Automatic;
						}
						if ((num2 & 8) != 0)
						{
							imeSentenceModeValues2 |= ImeSentenceModeValues.PhrasePrediction;
						}
						if ((num2 & 16) != 0)
						{
							imeSentenceModeValues2 |= ImeSentenceModeValues.Conversation;
						}
						return imeSentenceModeValues2;
					}
				}
				return ImeSentenceModeValues.None;
			}
			[SecurityCritical]
			set
			{
				if (!this.IsValidSentenceMode(value))
				{
					throw new ArgumentException(SR.Get("InputMethod_InvalidSentenceMode", new object[]
					{
						value
					}));
				}
				TextServicesCompartment compartment = TextServicesCompartmentContext.Current.GetCompartment(InputMethodStateType.ImeSentenceModeValues);
				if (compartment != null)
				{
					UnsafeNativeMethods.SentenceModeFlags sentenceModeFlags = UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_NONE;
					if ((value & ImeSentenceModeValues.PluralClause) != ImeSentenceModeValues.None)
					{
						sentenceModeFlags |= UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_PLAURALCLAUSE;
					}
					if ((value & ImeSentenceModeValues.SingleConversion) != ImeSentenceModeValues.None)
					{
						sentenceModeFlags |= UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_SINGLECONVERT;
					}
					if ((value & ImeSentenceModeValues.Automatic) != ImeSentenceModeValues.None)
					{
						sentenceModeFlags |= UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_AUTOMATIC;
					}
					if ((value & ImeSentenceModeValues.PhrasePrediction) != ImeSentenceModeValues.None)
					{
						sentenceModeFlags |= UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_PHRASEPREDICT;
					}
					if ((value & ImeSentenceModeValues.Conversation) != ImeSentenceModeValues.None)
					{
						sentenceModeFlags |= UnsafeNativeMethods.SentenceModeFlags.TF_SENTENCEMODE_CONVERSATION;
					}
					if (compartment.IntValue != (int)sentenceModeFlags)
					{
						compartment.IntValue = (int)sentenceModeFlags;
					}
				}
				if (InputMethod._immEnabled)
				{
					IntPtr intPtr = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
					if (intPtr != IntPtr.Zero)
					{
						int conversion = 0;
						int num = 0;
						IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, intPtr));
						UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(this, handle), ref conversion, ref num);
						int num2 = 0;
						if ((value & ImeSentenceModeValues.PluralClause) != ImeSentenceModeValues.None)
						{
							num2 |= 1;
						}
						if ((value & ImeSentenceModeValues.SingleConversion) != ImeSentenceModeValues.None)
						{
							num2 |= 2;
						}
						if ((value & ImeSentenceModeValues.Automatic) != ImeSentenceModeValues.None)
						{
							num2 |= 4;
						}
						if ((value & ImeSentenceModeValues.PhrasePrediction) != ImeSentenceModeValues.None)
						{
							num2 |= 8;
						}
						if ((value & ImeSentenceModeValues.Conversation) != ImeSentenceModeValues.None)
						{
							num2 |= 16;
						}
						if (num != num2)
						{
							UnsafeNativeMethods.ImmSetConversionStatus(new HandleRef(this, handle), conversion, num2);
						}
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, intPtr), new HandleRef(this, handle));
					}
				}
			}
		}

		/// <summary>Obtém um valor que indica se esse método de entrada pode ou não exibir UI (interface do usuário) de configuração.</summary>
		/// <returns>
		///   <see langword="true" /> Se configuration Interface de Usuário pode ser exibido; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003F4B4 File Offset: 0x0003E8B4
		public bool CanShowConfigurationUI
		{
			get
			{
				return this._ShowConfigureUI(null, false);
			}
		}

		/// <summary>Obtém um valor que indica se esse método de entrada pode exibir UI (interface do usuário) de registro de palavras.</summary>
		/// <returns>
		///   <see langword="true" /> Se registro de palavras Interface de Usuário pode ser exibido; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0003F4CC File Offset: 0x0003E8CC
		public bool CanShowRegisterWordUI
		{
			get
			{
				return this._ShowRegisterWordUI(null, false, "");
			}
		}

		/// <summary>Ocorre quando o estado do método de entrada (representado pela propriedade <see cref="P:System.Windows.Input.InputMethod.ImeState" />) é alterado.</summary>
		// Token: 0x14000160 RID: 352
		// (add) Token: 0x060010C7 RID: 4295 RVA: 0x0003F4E8 File Offset: 0x0003E8E8
		// (remove) Token: 0x060010C8 RID: 4296 RVA: 0x0003F520 File Offset: 0x0003E920
		public event InputMethodStateChangedEventHandler StateChanged
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._StateChanged == null && TextServicesLoader.ServicesInstalled)
				{
					this.InitializeCompartmentEventSink();
				}
				this._StateChanged += value;
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._StateChanged -= value;
				if (this._StateChanged == null && TextServicesLoader.ServicesInstalled)
				{
					this.UninitializeCompartmentEventSink();
				}
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003F558 File Offset: 0x0003E958
		internal void GotKeyboardFocus(DependencyObject focus)
		{
			if (focus == null)
			{
				return;
			}
			object value = focus.GetValue(InputMethod.PreferredImeStateProperty);
			if (value != null && (InputMethodState)value != InputMethodState.DoNotCare)
			{
				this.ImeState = (InputMethodState)value;
			}
			value = focus.GetValue(InputMethod.PreferredImeConversionModeProperty);
			if (value != null && ((ImeConversionModeValues)value & ImeConversionModeValues.DoNotCare) == (ImeConversionModeValues)0)
			{
				this.ImeConversionMode = (ImeConversionModeValues)value;
			}
			value = focus.GetValue(InputMethod.PreferredImeSentenceModeProperty);
			if (value != null && ((ImeSentenceModeValues)value & ImeSentenceModeValues.DoNotCare) == ImeSentenceModeValues.None)
			{
				this.ImeSentenceMode = (ImeSentenceModeValues)value;
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003F5E0 File Offset: 0x0003E9E0
		internal void OnChange(ref Guid rguid)
		{
			if (this._StateChanged != null)
			{
				InputMethodStateType statetype = InputMethodEventTypeInfo.ToType(ref rguid);
				this._StateChanged(this, new InputMethodStateChangedEventArgs(statetype));
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003F610 File Offset: 0x0003EA10
		internal static bool IsImm32ImeCurrent()
		{
			if (!InputMethod._immEnabled)
			{
				return false;
			}
			IntPtr keyboardLayout = SafeNativeMethods.GetKeyboardLayout(0);
			return InputMethod.IsImm32Ime(keyboardLayout);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003F634 File Offset: 0x0003EA34
		internal static bool IsImm32Ime(IntPtr hkl)
		{
			return !(hkl == IntPtr.Zero) && ((long)NativeMethods.IntPtrToInt32(hkl) & (long)((ulong)-268435456)) == (long)((ulong)-536870912);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003F668 File Offset: 0x0003EA68
		private static void IsInputMethodEnabled_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			IInputElement inputElement = (IInputElement)d;
			if (inputElement == Keyboard.FocusedElement)
			{
				InputMethod.Current.EnableOrDisableInputMethod((bool)e.NewValue);
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003F69C File Offset: 0x0003EA9C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void EnableOrDisableInputMethod(bool bEnabled)
		{
			if (TextServicesLoader.ServicesInstalled && TextServicesContext.DispatcherCurrent != null)
			{
				if (bEnabled)
				{
					TextServicesContext.DispatcherCurrent.SetFocusOnDefaultTextStore();
				}
				else
				{
					TextServicesContext.DispatcherCurrent.SetFocusOnEmptyDim();
				}
			}
			if (InputMethod._immEnabled)
			{
				IntPtr handle = InputMethod.HwndFromInputElement(Keyboard.FocusedElement);
				if (bEnabled)
				{
					if (this.DefaultImc != IntPtr.Zero)
					{
						UnsafeNativeMethods.ImmAssociateContext(new HandleRef(this, handle), new HandleRef(this, InputMethod._defaultImc.Value));
						return;
					}
				}
				else
				{
					UnsafeNativeMethods.ImmAssociateContext(new HandleRef(this, handle), new HandleRef(this, IntPtr.Zero));
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0003F730 File Offset: 0x0003EB30
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0003F744 File Offset: 0x0003EB44
		internal TextServicesContext TextServicesContext
		{
			get
			{
				return this._textservicesContext;
			}
			set
			{
				this._textservicesContext = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0003F758 File Offset: 0x0003EB58
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0003F76C File Offset: 0x0003EB6C
		internal TextServicesCompartmentContext TextServicesCompartmentContext
		{
			get
			{
				return this._textservicesCompartmentContext;
			}
			set
			{
				this._textservicesCompartmentContext = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0003F780 File Offset: 0x0003EB80
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0003F794 File Offset: 0x0003EB94
		internal InputLanguageManager InputLanguageManager
		{
			get
			{
				return this._inputlanguagemanager;
			}
			set
			{
				this._inputlanguagemanager = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0003F7A8 File Offset: 0x0003EBA8
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0003F7BC File Offset: 0x0003EBBC
		internal DefaultTextStore DefaultTextStore
		{
			get
			{
				return this._defaulttextstore;
			}
			set
			{
				this._defaulttextstore = value;
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003F7D0 File Offset: 0x0003EBD0
		[SecurityCritical]
		private UnsafeNativeMethods.ConversionModeFlags Imm32ConversionModeToTSFConversionMode(IntPtr hwnd)
		{
			UnsafeNativeMethods.ConversionModeFlags conversionModeFlags = UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ALPHANUMERIC;
			if (hwnd != IntPtr.Zero)
			{
				int num = 0;
				int num2 = 0;
				IntPtr handle = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, hwnd));
				UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(this, handle), ref num, ref num2);
				UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, hwnd), new HandleRef(this, handle));
				if ((num & 1) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NATIVE;
				}
				if ((num & 2) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_KATAKANA;
				}
				if ((num & 8) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FULLSHAPE;
				}
				if ((num & 16) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_ROMAN;
				}
				if ((num & 32) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_CHARCODE;
				}
				if ((num & 256) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_NOCONVERSION;
				}
				if ((num & 512) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_EUDC;
				}
				if ((num & 1024) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_SYMBOL;
				}
				if ((num & 2048) != 0)
				{
					conversionModeFlags |= UnsafeNativeMethods.ConversionModeFlags.TF_CONVERSIONMODE_FIXED;
				}
			}
			return conversionModeFlags;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003F89C File Offset: 0x0003EC9C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void InitializeCompartmentEventSink()
		{
			for (int i = 0; i < InputMethodEventTypeInfo.InfoList.Length; i++)
			{
				InputMethodEventTypeInfo inputMethodEventTypeInfo = InputMethodEventTypeInfo.InfoList[i];
				TextServicesCompartment textServicesCompartment = null;
				if (inputMethodEventTypeInfo.Scope == CompartmentScope.Thread)
				{
					textServicesCompartment = TextServicesCompartmentContext.Current.GetThreadCompartment(inputMethodEventTypeInfo.Guid);
				}
				else if (inputMethodEventTypeInfo.Scope == CompartmentScope.Global)
				{
					textServicesCompartment = TextServicesCompartmentContext.Current.GetGlobalCompartment(inputMethodEventTypeInfo.Guid);
				}
				if (textServicesCompartment != null)
				{
					if (this._sink == null)
					{
						this._sink = new TextServicesCompartmentEventSink(this);
					}
					textServicesCompartment.AdviseNotifySink(this._sink);
				}
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003F920 File Offset: 0x0003ED20
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void UninitializeCompartmentEventSink()
		{
			for (int i = 0; i < InputMethodEventTypeInfo.InfoList.Length; i++)
			{
				InputMethodEventTypeInfo inputMethodEventTypeInfo = InputMethodEventTypeInfo.InfoList[i];
				TextServicesCompartment textServicesCompartment = null;
				if (inputMethodEventTypeInfo.Scope == CompartmentScope.Thread)
				{
					textServicesCompartment = TextServicesCompartmentContext.Current.GetThreadCompartment(inputMethodEventTypeInfo.Guid);
				}
				else if (inputMethodEventTypeInfo.Scope == CompartmentScope.Global)
				{
					textServicesCompartment = TextServicesCompartmentContext.Current.GetGlobalCompartment(inputMethodEventTypeInfo.Guid);
				}
				if (textServicesCompartment != null)
				{
					textServicesCompartment.UnadviseNotifySink();
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003F988 File Offset: 0x0003ED88
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool _ShowConfigureUI(UIElement element, bool fShow)
		{
			SecurityHelper.DemandUnrestrictedUIPermission();
			bool result = false;
			IntPtr keyboardLayout = SafeNativeMethods.GetKeyboardLayout(0);
			if (!InputMethod.IsImm32Ime(keyboardLayout))
			{
				UnsafeNativeMethods.TF_LANGUAGEPROFILE tf_LANGUAGEPROFILE;
				UnsafeNativeMethods.ITfFunctionProvider functionPrvForCurrentKeyboardTIP = this.GetFunctionPrvForCurrentKeyboardTIP(out tf_LANGUAGEPROFILE);
				if (functionPrvForCurrentKeyboardTIP != null)
				{
					Guid iid_ITfFnConfigure = UnsafeNativeMethods.IID_ITfFnConfigure;
					Guid guid_Null = UnsafeNativeMethods.Guid_Null;
					object obj;
					functionPrvForCurrentKeyboardTIP.GetFunction(ref guid_Null, ref iid_ITfFnConfigure, out obj);
					UnsafeNativeMethods.ITfFnConfigure tfFnConfigure = obj as UnsafeNativeMethods.ITfFnConfigure;
					if (tfFnConfigure != null)
					{
						result = true;
						if (fShow)
						{
							tfFnConfigure.Show(InputMethod.HwndFromInputElement(element), tf_LANGUAGEPROFILE.langid, ref tf_LANGUAGEPROFILE.guidProfile);
						}
						Marshal.ReleaseComObject(tfFnConfigure);
					}
					Marshal.ReleaseComObject(functionPrvForCurrentKeyboardTIP);
				}
			}
			else
			{
				result = true;
				if (fShow)
				{
					UnsafeNativeMethods.ImmConfigureIME(new HandleRef(this, keyboardLayout), new HandleRef(this, InputMethod.HwndFromInputElement(element)), 1, IntPtr.Zero);
				}
			}
			return result;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003FA38 File Offset: 0x0003EE38
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private bool _ShowRegisterWordUI(UIElement element, bool fShow, string strRegister)
		{
			SecurityHelper.DemandUnrestrictedUIPermission();
			bool result = false;
			IntPtr keyboardLayout = SafeNativeMethods.GetKeyboardLayout(0);
			if (!InputMethod.IsImm32Ime(keyboardLayout))
			{
				UnsafeNativeMethods.TF_LANGUAGEPROFILE tf_LANGUAGEPROFILE;
				UnsafeNativeMethods.ITfFunctionProvider functionPrvForCurrentKeyboardTIP = this.GetFunctionPrvForCurrentKeyboardTIP(out tf_LANGUAGEPROFILE);
				if (functionPrvForCurrentKeyboardTIP != null)
				{
					Guid iid_ITfFnConfigureRegisterWord = UnsafeNativeMethods.IID_ITfFnConfigureRegisterWord;
					Guid guid_Null = UnsafeNativeMethods.Guid_Null;
					object obj;
					functionPrvForCurrentKeyboardTIP.GetFunction(ref guid_Null, ref iid_ITfFnConfigureRegisterWord, out obj);
					UnsafeNativeMethods.ITfFnConfigureRegisterWord tfFnConfigureRegisterWord = obj as UnsafeNativeMethods.ITfFnConfigureRegisterWord;
					if (tfFnConfigureRegisterWord != null)
					{
						result = true;
						if (fShow)
						{
							tfFnConfigureRegisterWord.Show(InputMethod.HwndFromInputElement(element), tf_LANGUAGEPROFILE.langid, ref tf_LANGUAGEPROFILE.guidProfile, strRegister);
						}
						Marshal.ReleaseComObject(tfFnConfigureRegisterWord);
					}
					Marshal.ReleaseComObject(functionPrvForCurrentKeyboardTIP);
				}
			}
			else
			{
				result = true;
				if (fShow)
				{
					NativeMethods.REGISTERWORD registerword = default(NativeMethods.REGISTERWORD);
					registerword.lpReading = null;
					registerword.lpWord = strRegister;
					UnsafeNativeMethods.ImmConfigureIME(new HandleRef(this, keyboardLayout), new HandleRef(this, InputMethod.HwndFromInputElement(element)), 2, ref registerword);
				}
			}
			return result;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003FAFC File Offset: 0x0003EEFC
		[SecurityCritical]
		private static IntPtr HwndFromInputElement(IInputElement element)
		{
			IntPtr result = (IntPtr)0;
			if (element != null)
			{
				DependencyObject dependencyObject = element as DependencyObject;
				if (dependencyObject != null)
				{
					DependencyObject containingVisual = InputElement.GetContainingVisual(dependencyObject);
					if (containingVisual != null)
					{
						PresentationSource presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
						if (presentationSource != null)
						{
							IWin32Window win32Window = presentationSource as IWin32Window;
							if (win32Window != null)
							{
								new UIPermission(UIPermissionWindow.AllWindows).Assert();
								try
								{
									result = win32Window.Handle;
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003FB78 File Offset: 0x0003EF78
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private UnsafeNativeMethods.ITfFunctionProvider GetFunctionPrvForCurrentKeyboardTIP(out UnsafeNativeMethods.TF_LANGUAGEPROFILE tf_profile)
		{
			SecurityHelper.DemandUnmanagedCode();
			tf_profile = this.GetCurrentKeybordTipProfile();
			if (tf_profile.clsid.Equals(UnsafeNativeMethods.Guid_Null))
			{
				return null;
			}
			TextServicesContext dispatcherCurrent = TextServicesContext.DispatcherCurrent;
			UnsafeNativeMethods.ITfFunctionProvider result;
			dispatcherCurrent.ThreadManager.GetFunctionProvider(ref tf_profile.clsid, out result);
			return result;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003FBC8 File Offset: 0x0003EFC8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private UnsafeNativeMethods.TF_LANGUAGEPROFILE GetCurrentKeybordTipProfile()
		{
			UnsafeNativeMethods.ITfInputProcessorProfiles tfInputProcessorProfiles = InputProcessorProfilesLoader.Load();
			UnsafeNativeMethods.TF_LANGUAGEPROFILE result = default(UnsafeNativeMethods.TF_LANGUAGEPROFILE);
			if (tfInputProcessorProfiles != null)
			{
				CultureInfo currentInputLanguage = InputLanguageManager.Current.CurrentInputLanguage;
				UnsafeNativeMethods.IEnumTfLanguageProfiles enumTfLanguageProfiles;
				tfInputProcessorProfiles.EnumLanguageProfiles((short)currentInputLanguage.LCID, out enumTfLanguageProfiles);
				UnsafeNativeMethods.TF_LANGUAGEPROFILE[] array = new UnsafeNativeMethods.TF_LANGUAGEPROFILE[1];
				int num;
				while (enumTfLanguageProfiles.Next(1, array, out num) == 0)
				{
					if (array[0].fActive && array[0].catid.Equals(UnsafeNativeMethods.GUID_TFCAT_TIP_KEYBOARD))
					{
						result = array[0];
						break;
					}
				}
				Marshal.ReleaseComObject(enumTfLanguageProfiles);
			}
			return result;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003FC50 File Offset: 0x0003F050
		private bool IsValidConversionMode(ImeConversionModeValues mode)
		{
			int num = -2147482625;
			return (mode & (ImeConversionModeValues)(~(ImeConversionModeValues)num)) == (ImeConversionModeValues)0;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003FC6C File Offset: 0x0003F06C
		private bool IsValidSentenceMode(ImeSentenceModeValues mode)
		{
			int num = -2147483617;
			return (mode & (ImeSentenceModeValues)(~(ImeSentenceModeValues)num)) == ImeSentenceModeValues.None;
		}

		// Token: 0x14000161 RID: 353
		// (add) Token: 0x060010E1 RID: 4321 RVA: 0x0003FC88 File Offset: 0x0003F088
		// (remove) Token: 0x060010E2 RID: 4322 RVA: 0x0003FCC0 File Offset: 0x0003F0C0
		private event InputMethodStateChangedEventHandler _StateChanged;

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003FCF8 File Offset: 0x0003F0F8
		private IntPtr DefaultImc
		{
			[SecurityCritical]
			get
			{
				if (InputMethod._defaultImc == null)
				{
					SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
					securityPermission.Assert();
					try
					{
						IntPtr handle = UnsafeNativeMethods.ImmGetDefaultIMEWnd(new HandleRef(this, IntPtr.Zero));
						IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(this, handle));
						InputMethod._defaultImc = new SecurityCriticalDataClass<IntPtr>(intPtr);
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(this, handle), new HandleRef(this, intPtr));
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return InputMethod._defaultImc.Value;
			}
		}

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.IsInputMethodEnabled" /> anexada.</summary>
		// Token: 0x04000908 RID: 2312
		public static readonly DependencyProperty IsInputMethodEnabledProperty = DependencyProperty.RegisterAttached("IsInputMethodEnabled", typeof(bool), typeof(InputMethod), new PropertyMetadata(true, new PropertyChangedCallback(InputMethod.IsInputMethodEnabled_Changed)));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.IsInputMethodSuspended" /> anexada.</summary>
		// Token: 0x04000909 RID: 2313
		public static readonly DependencyProperty IsInputMethodSuspendedProperty = DependencyProperty.RegisterAttached("IsInputMethodSuspended", typeof(bool), typeof(InputMethod), new PropertyMetadata(false));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.PreferredImeState" /> anexada.</summary>
		// Token: 0x0400090A RID: 2314
		public static readonly DependencyProperty PreferredImeStateProperty = DependencyProperty.RegisterAttached("PreferredImeState", typeof(InputMethodState), typeof(InputMethod), new PropertyMetadata(InputMethodState.DoNotCare));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.PreferredImeConversionMode" /> anexada.</summary>
		// Token: 0x0400090B RID: 2315
		public static readonly DependencyProperty PreferredImeConversionModeProperty = DependencyProperty.RegisterAttached("PreferredImeConversionMode", typeof(ImeConversionModeValues), typeof(InputMethod), new PropertyMetadata(ImeConversionModeValues.DoNotCare));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.PreferredImeSentenceMode" /> anexada.</summary>
		// Token: 0x0400090C RID: 2316
		public static readonly DependencyProperty PreferredImeSentenceModeProperty = DependencyProperty.RegisterAttached("PreferredImeSentenceMode", typeof(ImeSentenceModeValues), typeof(InputMethod), new PropertyMetadata(ImeSentenceModeValues.DoNotCare));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputMethod.InputScope" /> anexada.</summary>
		// Token: 0x0400090D RID: 2317
		public static readonly DependencyProperty InputScopeProperty = DependencyProperty.RegisterAttached("InputScope", typeof(InputScope), typeof(InputMethod), new PropertyMetadata(null));

		// Token: 0x0400090F RID: 2319
		private TextServicesCompartmentEventSink _sink;

		// Token: 0x04000910 RID: 2320
		private TextServicesContext _textservicesContext;

		// Token: 0x04000911 RID: 2321
		private TextServicesCompartmentContext _textservicesCompartmentContext;

		// Token: 0x04000912 RID: 2322
		private InputLanguageManager _inputlanguagemanager;

		// Token: 0x04000913 RID: 2323
		private DefaultTextStore _defaulttextstore;

		// Token: 0x04000914 RID: 2324
		private static bool _immEnabled = SafeSystemMetrics.IsImmEnabled;

		// Token: 0x04000915 RID: 2325
		[ThreadStatic]
		private static SecurityCriticalDataClass<IntPtr> _defaultImc;
	}
}
