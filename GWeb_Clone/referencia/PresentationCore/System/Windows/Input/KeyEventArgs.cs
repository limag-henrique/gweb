using System;
using System.ComponentModel;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para os eventos roteados <see cref="E:System.Windows.UIElement.KeyUp" /> e <see cref="E:System.Windows.UIElement.KeyDown" />, bem como eventos de Versão Prévia e anexados relacionados.</summary>
	// Token: 0x0200026C RID: 620
	public class KeyEventArgs : KeyboardEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyEventArgs" />.</summary>
		/// <param name="keyboard">O dispositivo de teclado lógico associado a este evento.</param>
		/// <param name="inputSource">A fonte de entrada.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="key">A chave referenciada pelo evento.</param>
		// Token: 0x06001185 RID: 4485 RVA: 0x00042244 File Offset: 0x00041644
		[SecurityCritical]
		public KeyEventArgs(KeyboardDevice keyboard, PresentationSource inputSource, int timestamp, Key key) : base(keyboard, timestamp)
		{
			if (inputSource == null)
			{
				throw new ArgumentNullException("inputSource");
			}
			if (!Keyboard.IsValidKey(key))
			{
				throw new InvalidEnumArgumentException("key", (int)key, typeof(Key));
			}
			this._inputSource = inputSource;
			this._realKey = key;
			this._isRepeat = false;
			this.MarkNormal();
		}

		/// <summary>Obtém a origem de entrada que forneceu essa entrada.</summary>
		/// <returns>A fonte de entrada.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x000422A4 File Offset: 0x000416A4
		public PresentationSource InputSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this.UnsafeInputSource;
			}
		}

		/// <summary>Obtém a tecla do teclado associada ao evento.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.Key" /> referenciado pelo evento.</returns>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000422BC File Offset: 0x000416BC
		public Key Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x000422D0 File Offset: 0x000416D0
		internal Key RealKey
		{
			get
			{
				return this._realKey;
			}
		}

		/// <summary>Obtém a tecla do teclado referenciada pelo evento, se a tecla tiver sido processada por um Input Method Editor (IME).</summary>
		/// <returns>O <see cref="T:System.Windows.Input.Key" /> referenciado pelo evento.</returns>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000422E4 File Offset: 0x000416E4
		public Key ImeProcessedKey
		{
			get
			{
				if (this._key != Key.ImeProcessed)
				{
					return Key.None;
				}
				return this._realKey;
			}
		}

		/// <summary>Obterá a chave de teclado referenciada pelo evento, se a chave for processada pelo sistema.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.Key" /> referenciado pelo evento.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00042308 File Offset: 0x00041708
		public Key SystemKey
		{
			get
			{
				if (this._key != Key.System)
				{
					return Key.None;
				}
				return this._realKey;
			}
		}

		/// <summary>Obtém a chave que faz parte da composição de tecla inativa para criar um único caractere combinado.</summary>
		/// <returns>A chave que faz parte da composição de tecla inativa para criar um único caractere combinado.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004232C File Offset: 0x0004172C
		public Key DeadCharProcessedKey
		{
			get
			{
				if (this._key != Key.DeadCharProcessed)
				{
					return Key.None;
				}
				return this._realKey;
			}
		}

		/// <summary>Obtém o estado da tecla do teclado associada a este evento.</summary>
		/// <returns>O estado da tecla.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00042350 File Offset: 0x00041750
		public KeyStates KeyStates
		{
			get
			{
				return base.KeyboardDevice.GetKeyStates(this._realKey);
			}
		}

		/// <summary>Obtém um valor que indica se a tecla do teclado referenciada pelo evento é uma tecla repetida.</summary>
		/// <returns>
		///   <see langword="true" /> se a tecla for repetida; caso contrário, <see langword="false" />.  Nenhum valor padrão.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00042370 File Offset: 0x00041770
		public bool IsRepeat
		{
			get
			{
				return this._isRepeat;
			}
		}

		/// <summary>Obtém um valor que indica se a tecla referenciada pelo evento está no estado pressionado.</summary>
		/// <returns>
		///   <see langword="true" /> se a tecla estiver pressionada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00042384 File Offset: 0x00041784
		public bool IsDown
		{
			get
			{
				return base.KeyboardDevice.IsKeyDown(this._realKey);
			}
		}

		/// <summary>Obtém um valor que indica se a tecla referenciada pelo evento está no estado ativo.</summary>
		/// <returns>
		///   <see langword="true" /> Se a chave for para cima; Caso contrário, <see langword="false" />.  Nenhum valor padrão.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000423A4 File Offset: 0x000417A4
		public bool IsUp
		{
			get
			{
				return base.KeyboardDevice.IsKeyUp(this._realKey);
			}
		}

		/// <summary>Obtém um valor que indica se a chave referenciada pelo evento está no estado alternado.</summary>
		/// <returns>
		///   <see langword="true" /> se a chave estiver alternada; caso contrário, <see langword="false" />.  Nenhum valor padrão.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000423C4 File Offset: 0x000417C4
		public bool IsToggled
		{
			get
			{
				return base.KeyboardDevice.IsKeyToggled(this._realKey);
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001191 RID: 4497 RVA: 0x000423E4 File Offset: 0x000417E4
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)genericHandler;
			keyEventHandler(genericTarget, this);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00042400 File Offset: 0x00041800
		internal void SetRepeat(bool newRepeatState)
		{
			this._isRepeat = newRepeatState;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00042414 File Offset: 0x00041814
		internal void MarkNormal()
		{
			this._key = this._realKey;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00042430 File Offset: 0x00041830
		internal void MarkSystem()
		{
			this._key = Key.System;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00042448 File Offset: 0x00041848
		internal void MarkImeProcessed()
		{
			this._key = Key.ImeProcessed;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00042460 File Offset: 0x00041860
		internal void MarkDeadCharProcessed()
		{
			this._key = Key.DeadCharProcessed;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00042478 File Offset: 0x00041878
		internal PresentationSource UnsafeInputSource
		{
			[SecurityCritical]
			get
			{
				return this._inputSource;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004248C File Offset: 0x0004188C
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x000424A0 File Offset: 0x000418A0
		internal int ScanCode
		{
			get
			{
				return this._scanCode;
			}
			set
			{
				this._scanCode = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x000424B4 File Offset: 0x000418B4
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x000424C8 File Offset: 0x000418C8
		internal bool IsExtendedKey
		{
			get
			{
				return this._isExtendedKey;
			}
			set
			{
				this._isExtendedKey = value;
			}
		}

		// Token: 0x04000999 RID: 2457
		private Key _realKey;

		// Token: 0x0400099A RID: 2458
		private Key _key;

		// Token: 0x0400099B RID: 2459
		[SecurityCritical]
		private PresentationSource _inputSource;

		// Token: 0x0400099C RID: 2460
		private bool _isRepeat;

		// Token: 0x0400099D RID: 2461
		private int _scanCode;

		// Token: 0x0400099E RID: 2462
		private bool _isExtendedKey;
	}
}
