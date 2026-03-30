using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Threading;
using Microsoft.Win32;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Fornece recursos para gerenciar eventos relacionados à entrada e às composições de texto.</summary>
	// Token: 0x020002DD RID: 733
	public sealed class TextCompositionManager : DispatcherObject
	{
		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInputStart" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x06001628 RID: 5672 RVA: 0x00052A84 File Offset: 0x00051E84
		public static void AddPreviewTextInputStartHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.PreviewTextInputStartEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInputStart" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x06001629 RID: 5673 RVA: 0x00052AAC File Offset: 0x00051EAC
		public static void RemovePreviewTextInputStartHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.PreviewTextInputStartEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInputStart" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x0600162A RID: 5674 RVA: 0x00052AD4 File Offset: 0x00051ED4
		public static void AddTextInputStartHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.TextInputStartEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInputStart" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x0600162B RID: 5675 RVA: 0x00052AFC File Offset: 0x00051EFC
		public static void RemoveTextInputStartHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.TextInputStartEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInputUpdate" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x0600162C RID: 5676 RVA: 0x00052B24 File Offset: 0x00051F24
		public static void AddPreviewTextInputUpdateHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.PreviewTextInputUpdateEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInputUpdate" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x0600162D RID: 5677 RVA: 0x00052B4C File Offset: 0x00051F4C
		public static void RemovePreviewTextInputUpdateHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.PreviewTextInputUpdateEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInputUpdate" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x0600162E RID: 5678 RVA: 0x00052B74 File Offset: 0x00051F74
		public static void AddTextInputUpdateHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.TextInputUpdateEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInputUpdate" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x0600162F RID: 5679 RVA: 0x00052B9C File Offset: 0x00051F9C
		public static void RemoveTextInputUpdateHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.TextInputUpdateEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x06001630 RID: 5680 RVA: 0x00052BC4 File Offset: 0x00051FC4
		public static void AddPreviewTextInputHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.PreviewTextInputEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x06001631 RID: 5681 RVA: 0x00052BEC File Offset: 0x00051FEC
		public static void RemovePreviewTextInputHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.PreviewTextInputEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" />.</summary>
		/// <param name="element">Um objeto de dependência ao qual o manipulador de eventos será adicionado.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser adicionado.</param>
		// Token: 0x06001632 RID: 5682 RVA: 0x00052C14 File Offset: 0x00052014
		public static void AddTextInputHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.AddHandler(element, TextCompositionManager.TextInputEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" />.</summary>
		/// <param name="element">Um objeto de dependência do qual o manipulador de eventos será removido.  O objeto de dependência deve ser um <see cref="T:System.Windows.UIElement" /> ou um <see cref="T:System.Windows.ContentElement" />.</param>
		/// <param name="handler">Um delegado que designa o manipulador a ser removido.</param>
		// Token: 0x06001633 RID: 5683 RVA: 0x00052C3C File Offset: 0x0005203C
		public static void RemoveTextInputHandler(DependencyObject element, TextCompositionEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			UIElement.RemoveHandler(element, TextCompositionManager.TextInputEvent, handler);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00052C64 File Offset: 0x00052064
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal TextCompositionManager(InputManager inputManager)
		{
			this._inputManager = inputManager;
			this._inputManager.PreProcessInput += this.PreProcessInput;
			this._inputManager.PostProcessInput += this.PostProcessInput;
		}

		/// <summary>Inicia uma composição de texto especificada.</summary>
		/// <param name="composition">Um objeto <see cref="T:System.Windows.Input.TextComposition" /> a ser iniciado.</param>
		/// <returns>
		///   <see langword="true" /> se a composição de texto for iniciada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001635 RID: 5685 RVA: 0x00052CAC File Offset: 0x000520AC
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static bool StartComposition(TextComposition composition)
		{
			return TextCompositionManager.UnsafeStartComposition(composition);
		}

		/// <summary>Atualiza uma composição de texto especificada.</summary>
		/// <param name="composition">Um objeto <see cref="T:System.Windows.Input.TextComposition" /> a ser atualizado.</param>
		/// <returns>
		///   <see langword="true" /> se a composição de texto for atualizada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001636 RID: 5686 RVA: 0x00052CC0 File Offset: 0x000520C0
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static bool UpdateComposition(TextComposition composition)
		{
			return TextCompositionManager.UnsafeUpdateComposition(composition);
		}

		/// <summary>Conclui uma composição de texto especificada.</summary>
		/// <param name="composition">Um objeto <see cref="T:System.Windows.Input.TextComposition" /> a ser concluído.</param>
		/// <returns>
		///   <see langword="true" /> se a composição de texto for concluída com êxito; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando a composição é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">Gerado quando não há um gerente de entrada associado à composição ou quando a composição de texto já está marcada como concluída.</exception>
		// Token: 0x06001637 RID: 5687 RVA: 0x00052CD4 File Offset: 0x000520D4
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static bool CompleteComposition(TextComposition composition)
		{
			return TextCompositionManager.UnsafeCompleteComposition(composition);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00052CE8 File Offset: 0x000520E8
		[SecurityCritical]
		private static bool UnsafeStartComposition(TextComposition composition)
		{
			if (composition == null)
			{
				throw new ArgumentNullException("composition");
			}
			if (composition._InputManager == null)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_NoInputManager", new object[]
				{
					"composition"
				}));
			}
			if (composition.Stage != TextCompositionStage.None)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_TextCompositionHasStarted", new object[]
				{
					"composition"
				}));
			}
			composition.Stage = TextCompositionStage.Started;
			TextCompositionEventArgs textCompositionEventArgs = new TextCompositionEventArgs(composition._InputDevice, composition);
			textCompositionEventArgs.RoutedEvent = TextCompositionManager.PreviewTextInputStartEvent;
			textCompositionEventArgs.Source = composition.Source;
			return composition._InputManager.ProcessInput(textCompositionEventArgs);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00052D88 File Offset: 0x00052188
		[SecurityCritical]
		private static bool UnsafeUpdateComposition(TextComposition composition)
		{
			if (composition == null)
			{
				throw new ArgumentNullException("composition");
			}
			if (composition._InputManager == null)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_NoInputManager", new object[]
				{
					"composition"
				}));
			}
			if (composition.Stage == TextCompositionStage.None)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_TextCompositionNotStarted", new object[]
				{
					"composition"
				}));
			}
			if (composition.Stage == TextCompositionStage.Done)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_TextCompositionHasDone", new object[]
				{
					"composition"
				}));
			}
			TextCompositionEventArgs textCompositionEventArgs = new TextCompositionEventArgs(composition._InputDevice, composition);
			textCompositionEventArgs.RoutedEvent = TextCompositionManager.PreviewTextInputUpdateEvent;
			textCompositionEventArgs.Source = composition.Source;
			return composition._InputManager.ProcessInput(textCompositionEventArgs);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00052E48 File Offset: 0x00052248
		[SecurityCritical]
		private static bool UnsafeCompleteComposition(TextComposition composition)
		{
			if (composition == null)
			{
				throw new ArgumentNullException("composition");
			}
			if (composition._InputManager == null)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_NoInputManager", new object[]
				{
					"composition"
				}));
			}
			if (composition.Stage == TextCompositionStage.None)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_TextCompositionNotStarted", new object[]
				{
					"composition"
				}));
			}
			if (composition.Stage == TextCompositionStage.Done)
			{
				throw new ArgumentException(SR.Get("TextCompositionManager_TextCompositionHasDone", new object[]
				{
					"composition"
				}));
			}
			composition.Stage = TextCompositionStage.Done;
			TextCompositionEventArgs textCompositionEventArgs = new TextCompositionEventArgs(composition._InputDevice, composition);
			textCompositionEventArgs.RoutedEvent = TextCompositionManager.PreviewTextInputEvent;
			textCompositionEventArgs.Source = composition.Source;
			return composition._InputManager.ProcessInput(textCompositionEventArgs);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00052F10 File Offset: 0x00052310
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static string GetCurrentOEMCPEncoding(int code)
		{
			SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			securityPermission.Assert();
			string result;
			try
			{
				int oemcp = UnsafeNativeMethods.GetOEMCP();
				result = TextCompositionManager.CharacterEncoding(oemcp, code);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00052F60 File Offset: 0x00052360
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static string CharacterEncoding(int cp, int code)
		{
			byte[] array = TextCompositionManager.ConvertCodeToByteArray(code);
			StringBuilder stringBuilder = new StringBuilder(4);
			int num = UnsafeNativeMethods.MultiByteToWideChar(cp, 5, array, array.Length, stringBuilder, 4);
			if (num == 0)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error);
			}
			stringBuilder.Length = num;
			return stringBuilder.ToString();
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00052FA8 File Offset: 0x000523A8
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyDownEvent)
			{
				KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
				if (!keyEventArgs.Handled)
				{
					if (!this._altNumpadEntryMode)
					{
						this.EnterAltNumpadEntryMode(keyEventArgs.RealKey);
					}
					else if (this.HandleAltNumpadEntry(keyEventArgs.RealKey, keyEventArgs.ScanCode, keyEventArgs.IsExtendedKey))
					{
						if (this._altNumpadcomposition == null)
						{
							this._altNumpadcomposition = new TextComposition(this._inputManager, (IInputElement)keyEventArgs.Source, "", TextCompositionAutoComplete.Off, keyEventArgs.Device);
							keyEventArgs.Handled = TextCompositionManager.UnsafeStartComposition(this._altNumpadcomposition);
						}
						else
						{
							this._altNumpadcomposition.ClearTexts();
							keyEventArgs.Handled = TextCompositionManager.UnsafeUpdateComposition(this._altNumpadcomposition);
						}
						e.Cancel();
					}
					else if (this._altNumpadcomposition != null)
					{
						this._altNumpadcomposition.ClearTexts();
						this._altNumpadcomposition.Complete();
						this.ClearAltnumpadComposition();
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyDownEvent)
			{
				KeyEventArgs keyEventArgs2 = (KeyEventArgs)e.StagingItem.Input;
				if (!keyEventArgs2.Handled && this._deadCharTextComposition != null && this._deadCharTextComposition.Stage == TextCompositionStage.Started)
				{
					keyEventArgs2.MarkDeadCharProcessed();
				}
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000530F8 File Offset: 0x000524F8
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyUpEvent)
			{
				KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
				if (!keyEventArgs.Handled)
				{
					if (keyEventArgs.RealKey == Key.LeftAlt || keyEventArgs.RealKey == Key.RightAlt)
					{
						ModifierKeys modifiers = keyEventArgs.KeyboardDevice.Modifiers;
						if ((modifiers & ModifierKeys.Alt) == ModifierKeys.None && this._altNumpadEntryMode)
						{
							this._altNumpadEntryMode = false;
							if (this._altNumpadEntry != 0)
							{
								this._altNumpadcomposition.ClearTexts();
								if (this._altNumpadConversionMode == AltNumpadConversionMode.OEMCodePage)
								{
									this._altNumpadcomposition.SetText(TextCompositionManager.GetCurrentOEMCPEncoding(this._altNumpadEntry));
								}
								else if (this._altNumpadConversionMode == AltNumpadConversionMode.DefaultCodePage || this._altNumpadConversionMode == AltNumpadConversionMode.HexDefaultCodePage)
								{
									this._altNumpadcomposition.SetText(TextCompositionManager.CharacterEncoding(InputLanguageManager.Current.CurrentInputLanguage.TextInfo.ANSICodePage, this._altNumpadEntry));
								}
								else if (this._altNumpadConversionMode == AltNumpadConversionMode.HexUnicode)
								{
									char[] value = new char[]
									{
										(char)this._altNumpadEntry
									};
									this._altNumpadcomposition.SetText(new string(value));
								}
							}
						}
					}
				}
				else
				{
					this._altNumpadEntryMode = false;
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.PreviewTextInputStartEvent)
			{
				TextCompositionEventArgs textCompositionEventArgs = (TextCompositionEventArgs)e.StagingItem.Input;
				if (!textCompositionEventArgs.Handled)
				{
					e.PushInput(new TextCompositionEventArgs(textCompositionEventArgs.Device, textCompositionEventArgs.TextComposition)
					{
						RoutedEvent = TextCompositionManager.TextInputStartEvent,
						Source = textCompositionEventArgs.TextComposition.Source
					}, e.StagingItem);
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.PreviewTextInputUpdateEvent)
			{
				TextCompositionEventArgs textCompositionEventArgs2 = (TextCompositionEventArgs)e.StagingItem.Input;
				if (!textCompositionEventArgs2.Handled)
				{
					e.PushInput(new TextCompositionEventArgs(textCompositionEventArgs2.Device, textCompositionEventArgs2.TextComposition)
					{
						RoutedEvent = TextCompositionManager.TextInputUpdateEvent,
						Source = textCompositionEventArgs2.TextComposition.Source
					}, e.StagingItem);
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.PreviewTextInputEvent)
			{
				TextCompositionEventArgs textCompositionEventArgs3 = (TextCompositionEventArgs)e.StagingItem.Input;
				if (!textCompositionEventArgs3.Handled)
				{
					e.PushInput(new TextCompositionEventArgs(textCompositionEventArgs3.Device, textCompositionEventArgs3.TextComposition)
					{
						RoutedEvent = TextCompositionManager.TextInputEvent,
						Source = textCompositionEventArgs3.TextComposition.Source
					}, e.StagingItem);
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.TextInputStartEvent)
			{
				TextCompositionEventArgs textCompositionEventArgs4 = (TextCompositionEventArgs)e.StagingItem.Input;
				if (!textCompositionEventArgs4.Handled && textCompositionEventArgs4.TextComposition.AutoComplete == TextCompositionAutoComplete.On)
				{
					textCompositionEventArgs4.Handled = TextCompositionManager.UnsafeCompleteComposition(textCompositionEventArgs4.TextComposition);
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.TextInputUpdateEvent)
			{
				TextCompositionEventArgs textCompositionEventArgs5 = (TextCompositionEventArgs)e.StagingItem.Input;
				if (!textCompositionEventArgs5.Handled && textCompositionEventArgs5.TextComposition == this._deadCharTextComposition && this._deadCharTextComposition.Composed)
				{
					DeadCharTextComposition deadCharTextComposition = this._deadCharTextComposition;
					this._deadCharTextComposition = null;
					textCompositionEventArgs5.Handled = TextCompositionManager.UnsafeCompleteComposition(deadCharTextComposition);
				}
			}
			InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
			if (inputReportEventArgs != null && inputReportEventArgs.Report.Type == InputType.Text && inputReportEventArgs.RoutedEvent == InputManager.InputReportEvent)
			{
				RawTextInputReport rawTextInputReport = (RawTextInputReport)inputReportEventArgs.Report;
				string text = new string(rawTextInputReport.CharacterCode, 1);
				bool flag = false;
				if (this._altNumpadcomposition != null)
				{
					if (text.Equals(this._altNumpadcomposition.Text))
					{
						flag = true;
					}
					else
					{
						this._altNumpadcomposition.ClearTexts();
					}
					this._altNumpadcomposition.Complete();
					this.ClearAltnumpadComposition();
				}
				if (!flag)
				{
					if (rawTextInputReport.IsDeadCharacter)
					{
						this._deadCharTextComposition = new DeadCharTextComposition(this._inputManager, null, text, TextCompositionAutoComplete.Off, InputManager.Current.PrimaryKeyboardDevice);
						if (rawTextInputReport.IsSystemCharacter)
						{
							this._deadCharTextComposition.MakeSystem();
						}
						else if (rawTextInputReport.IsControlCharacter)
						{
							this._deadCharTextComposition.MakeControl();
						}
						inputReportEventArgs.Handled = TextCompositionManager.UnsafeStartComposition(this._deadCharTextComposition);
						return;
					}
					if (this._deadCharTextComposition != null)
					{
						inputReportEventArgs.Handled = this.CompleteDeadCharComposition(text, rawTextInputReport.IsSystemCharacter, rawTextInputReport.IsControlCharacter);
						return;
					}
					TextComposition textComposition = new TextComposition(this._inputManager, (IInputElement)e.StagingItem.Input.Source, text, TextCompositionAutoComplete.On, InputManager.Current.PrimaryKeyboardDevice);
					if (rawTextInputReport.IsSystemCharacter)
					{
						textComposition.MakeSystem();
					}
					else if (rawTextInputReport.IsControlCharacter)
					{
						textComposition.MakeControl();
					}
					inputReportEventArgs.Handled = TextCompositionManager.UnsafeStartComposition(textComposition);
				}
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000535F8 File Offset: 0x000529F8
		[SecurityCritical]
		internal void CompleteDeadCharComposition()
		{
			this.CompleteDeadCharComposition(string.Empty, false, false);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00053614 File Offset: 0x00052A14
		[SecurityCritical]
		private bool CompleteDeadCharComposition(string inputText, bool isSystemCharacter, bool isControlCharacter)
		{
			if (this._deadCharTextComposition != null)
			{
				this._deadCharTextComposition.ClearTexts();
				this._deadCharTextComposition.SetText(inputText);
				this._deadCharTextComposition.Composed = true;
				if (isSystemCharacter)
				{
					this._deadCharTextComposition.MakeSystem();
				}
				else if (isControlCharacter)
				{
					this._deadCharTextComposition.MakeControl();
				}
				return TextCompositionManager.UnsafeUpdateComposition(this._deadCharTextComposition);
			}
			return false;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00053678 File Offset: 0x00052A78
		private bool EnterAltNumpadEntryMode(Key key)
		{
			bool result = false;
			if ((key == Key.LeftAlt || key == Key.RightAlt) && !this._altNumpadEntryMode)
			{
				this._altNumpadEntryMode = true;
				this._altNumpadEntry = 0;
				this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
				result = true;
			}
			return result;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000536B4 File Offset: 0x00052AB4
		private bool HandleAltNumpadEntry(Key key, int scanCode, bool isExtendedKey)
		{
			bool result = false;
			if (isExtendedKey)
			{
				return result;
			}
			if (!Keyboard.IsKeyDown(Key.LeftAlt) && !Keyboard.IsKeyDown(Key.RightAlt))
			{
				return false;
			}
			if (scanCode == 83)
			{
				if (TextCompositionManager.IsHexNumpadEnabled)
				{
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.HexDefaultCodePage;
					result = true;
				}
				else
				{
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
					result = false;
				}
			}
			else if (scanCode == 78)
			{
				if (TextCompositionManager.IsHexNumpadEnabled)
				{
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.HexUnicode;
					result = true;
				}
				else
				{
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
					result = false;
				}
			}
			else
			{
				int newEntry = this.GetNewEntry(key, scanCode);
				if (newEntry == -1)
				{
					this._altNumpadEntry = 0;
					this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
					result = false;
				}
				else
				{
					if (this._altNumpadEntry == 0 && newEntry == 0)
					{
						this._altNumpadConversionMode = AltNumpadConversionMode.DefaultCodePage;
					}
					if (this.HexConversionMode)
					{
						this._altNumpadEntry = this._altNumpadEntry * 16 + newEntry;
					}
					else
					{
						this._altNumpadEntry = this._altNumpadEntry * 10 + newEntry;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000537A4 File Offset: 0x00052BA4
		private int GetNewEntry(Key key, int scanCode)
		{
			if (this.HexConversionMode)
			{
				switch (key)
				{
				case Key.D0:
					return 0;
				case Key.D1:
					return 1;
				case Key.D2:
					return 2;
				case Key.D3:
					return 3;
				case Key.D4:
					return 4;
				case Key.D5:
					return 5;
				case Key.D6:
					return 6;
				case Key.D7:
					return 7;
				case Key.D8:
					return 8;
				case Key.D9:
					return 9;
				case Key.A:
					return 10;
				case Key.B:
					return 11;
				case Key.C:
					return 12;
				case Key.D:
					return 13;
				case Key.E:
					return 14;
				case Key.F:
					return 15;
				}
			}
			return TextCompositionManager.NumpadScanCode.DigitFromScanCode(scanCode);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00053834 File Offset: 0x00052C34
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static byte[] ConvertCodeToByteArray(int codeEntry)
		{
			byte[] result;
			if (codeEntry > 255)
			{
				result = new byte[]
				{
					(byte)(codeEntry >> 8),
					(byte)codeEntry
				};
			}
			else
			{
				result = new byte[]
				{
					(byte)codeEntry
				};
			}
			return result;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0005386C File Offset: 0x00052C6C
		private void ClearAltnumpadComposition()
		{
			this._altNumpadcomposition = null;
			this._altNumpadConversionMode = AltNumpadConversionMode.OEMCodePage;
			this._altNumpadEntry = 0;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00053890 File Offset: 0x00052C90
		private bool HexConversionMode
		{
			get
			{
				return this._altNumpadConversionMode == AltNumpadConversionMode.HexDefaultCodePage || this._altNumpadConversionMode == AltNumpadConversionMode.HexUnicode;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000538B4 File Offset: 0x00052CB4
		private static bool IsHexNumpadEnabled
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (!TextCompositionManager._isHexNumpadRegistryChecked)
				{
					RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_CURRENT_USER\\Control Panel\\Input Method");
					registryPermission.Assert();
					try
					{
						RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Input Method");
						if (registryKey != null)
						{
							object value = registryKey.GetValue("EnableHexNumpad");
							if (value is string && (string)value != "0")
							{
								TextCompositionManager._isHexNumpadEnabled = true;
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					TextCompositionManager._isHexNumpadRegistryChecked = true;
				}
				return TextCompositionManager._isHexNumpadEnabled;
			}
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInputStart" /> anexado.</summary>
		// Token: 0x04000C16 RID: 3094
		public static readonly RoutedEvent PreviewTextInputStartEvent = EventManager.RegisterRoutedEvent("PreviewTextInputStart", RoutingStrategy.Tunnel, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.TextInputStart" /> anexado.</summary>
		// Token: 0x04000C17 RID: 3095
		public static readonly RoutedEvent TextInputStartEvent = EventManager.RegisterRoutedEvent("TextInputStart", RoutingStrategy.Bubble, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInputUpdate" /> anexado.</summary>
		// Token: 0x04000C18 RID: 3096
		public static readonly RoutedEvent PreviewTextInputUpdateEvent = EventManager.RegisterRoutedEvent("PreviewTextInputUpdate", RoutingStrategy.Tunnel, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.TextInputUpdate" /> anexado.</summary>
		// Token: 0x04000C19 RID: 3097
		public static readonly RoutedEvent TextInputUpdateEvent = EventManager.RegisterRoutedEvent("TextInputUpdate", RoutingStrategy.Bubble, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" /> anexado.</summary>
		// Token: 0x04000C1A RID: 3098
		public static readonly RoutedEvent PreviewTextInputEvent = EventManager.RegisterRoutedEvent("PreviewTextInput", RoutingStrategy.Tunnel, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" /> anexado.</summary>
		// Token: 0x04000C1B RID: 3099
		public static readonly RoutedEvent TextInputEvent = EventManager.RegisterRoutedEvent("TextInput", RoutingStrategy.Bubble, typeof(TextCompositionEventHandler), typeof(TextCompositionManager));

		// Token: 0x04000C1C RID: 3100
		[SecurityCritical]
		private readonly InputManager _inputManager;

		// Token: 0x04000C1D RID: 3101
		private DeadCharTextComposition _deadCharTextComposition;

		// Token: 0x04000C1E RID: 3102
		private bool _altNumpadEntryMode;

		// Token: 0x04000C1F RID: 3103
		private int _altNumpadEntry;

		// Token: 0x04000C20 RID: 3104
		private AltNumpadConversionMode _altNumpadConversionMode;

		// Token: 0x04000C21 RID: 3105
		private TextComposition _altNumpadcomposition;

		// Token: 0x04000C22 RID: 3106
		private static bool _isHexNumpadRegistryChecked = false;

		// Token: 0x04000C23 RID: 3107
		private static bool _isHexNumpadEnabled = false;

		// Token: 0x04000C24 RID: 3108
		[SecuritySafeCritical]
		private const int EncodingBufferLen = 4;

		// Token: 0x0200081F RID: 2079
		internal static class NumpadScanCode
		{
			// Token: 0x06005634 RID: 22068 RVA: 0x00162540 File Offset: 0x00161940
			internal static int DigitFromScanCode(int scanCode)
			{
				switch (scanCode)
				{
				case 71:
					return 7;
				case 72:
					return 8;
				case 73:
					return 9;
				case 75:
					return 4;
				case 76:
					return 5;
				case 77:
					return 6;
				case 79:
					return 1;
				case 80:
					return 2;
				case 81:
					return 3;
				case 82:
					return 0;
				}
				return -1;
			}

			// Token: 0x04002772 RID: 10098
			internal const int NumpadDot = 83;

			// Token: 0x04002773 RID: 10099
			internal const int NumpadPlus = 78;

			// Token: 0x04002774 RID: 10100
			internal const int Numpad0 = 82;

			// Token: 0x04002775 RID: 10101
			internal const int Numpad1 = 79;

			// Token: 0x04002776 RID: 10102
			internal const int Numpad2 = 80;

			// Token: 0x04002777 RID: 10103
			internal const int Numpad3 = 81;

			// Token: 0x04002778 RID: 10104
			internal const int Numpad4 = 75;

			// Token: 0x04002779 RID: 10105
			internal const int Numpad5 = 76;

			// Token: 0x0400277A RID: 10106
			internal const int Numpad6 = 77;

			// Token: 0x0400277B RID: 10107
			internal const int Numpad7 = 71;

			// Token: 0x0400277C RID: 10108
			internal const int Numpad8 = 72;

			// Token: 0x0400277D RID: 10109
			internal const int Numpad9 = 73;
		}
	}
}
