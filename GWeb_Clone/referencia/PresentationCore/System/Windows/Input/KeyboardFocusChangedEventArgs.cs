using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para os eventos roteados <see cref="E:System.Windows.UIElement.LostKeyboardFocus" /> e <see cref="E:System.Windows.UIElement.GotKeyboardFocus" />, bem como para eventos anexados e de versão prévia relacionados.</summary>
	// Token: 0x02000235 RID: 565
	public class KeyboardFocusChangedEventArgs : KeyboardEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" />.</summary>
		/// <param name="keyboard">O dispositivo de teclado lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="oldFocus">O elemento que tinha o foco anteriormente.</param>
		/// <param name="newFocus">O elemento que tem o foco agora.</param>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003BEC0 File Offset: 0x0003B2C0
		public KeyboardFocusChangedEventArgs(KeyboardDevice keyboard, int timestamp, IInputElement oldFocus, IInputElement newFocus) : base(keyboard, timestamp)
		{
			if (oldFocus != null && !InputElement.IsValid(oldFocus))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					oldFocus.GetType()
				}));
			}
			if (newFocus != null && !InputElement.IsValid(newFocus))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					newFocus.GetType()
				}));
			}
			this._oldFocus = oldFocus;
			this._newFocus = newFocus;
		}

		/// <summary>Obtém o elemento que tinha o foco anteriormente.</summary>
		/// <returns>O elemento focalizado anteriormente.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003BF3C File Offset: 0x0003B33C
		public IInputElement OldFocus
		{
			get
			{
				return this._oldFocus;
			}
		}

		/// <summary>Obtém o elemento para o qual o foco foi movido.</summary>
		/// <returns>O elemento com foco.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0003BF50 File Offset: 0x0003B350
		public IInputElement NewFocus
		{
			get
			{
				return this._newFocus;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003BF64 File Offset: 0x0003B364
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			KeyboardFocusChangedEventHandler keyboardFocusChangedEventHandler = (KeyboardFocusChangedEventHandler)genericHandler;
			keyboardFocusChangedEventHandler(genericTarget, this);
		}

		// Token: 0x04000898 RID: 2200
		private IInputElement _oldFocus;

		// Token: 0x04000899 RID: 2201
		private IInputElement _newFocus;
	}
}
