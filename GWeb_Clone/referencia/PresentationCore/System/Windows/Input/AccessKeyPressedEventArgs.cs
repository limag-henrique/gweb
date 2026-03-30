using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento roteado <see cref="T:System.Windows.Input.AccessKeyManager" />.</summary>
	// Token: 0x0200022A RID: 554
	public class AccessKeyPressedEventArgs : RoutedEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.AccessKeyEventArgs" />.</summary>
		// Token: 0x06000F63 RID: 3939 RVA: 0x0003ADB4 File Offset: 0x0003A1B4
		public AccessKeyPressedEventArgs()
		{
			base.RoutedEvent = AccessKeyManager.AccessKeyPressedEvent;
			this._key = null;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.AccessKeyPressedEventArgs" /> com a chave de acesso especificada.</summary>
		/// <param name="key">A chave de acesso.</param>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003ADDC File Offset: 0x0003A1DC
		public AccessKeyPressedEventArgs(string key) : this()
		{
			this._key = key;
		}

		/// <summary>Obtém o escopo do elemento que disparou este evento.</summary>
		/// <returns>Escopo do elemento.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003ADF8 File Offset: 0x0003A1F8
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0003AE0C File Offset: 0x0003A20C
		public object Scope
		{
			get
			{
				return this._scope;
			}
			set
			{
				this._scope = value;
			}
		}

		/// <summary>Obtém ou define o destino para o evento.</summary>
		/// <returns>O elemento que gerou esse evento.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003AE20 File Offset: 0x0003A220
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0003AE34 File Offset: 0x0003A234
		public UIElement Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		/// <summary>Obtém uma representação de cadeia de caracteres da chave de acesso que foi pressionada</summary>
		/// <returns>A chave de acesso.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0003AE48 File Offset: 0x0003A248
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado em uma forma específica de tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06000F6A RID: 3946 RVA: 0x0003AE5C File Offset: 0x0003A25C
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			AccessKeyPressedEventHandler accessKeyPressedEventHandler = (AccessKeyPressedEventHandler)genericHandler;
			accessKeyPressedEventHandler(genericTarget, this);
		}

		// Token: 0x04000862 RID: 2146
		private object _scope;

		// Token: 0x04000863 RID: 2147
		private UIElement _target;

		// Token: 0x04000864 RID: 2148
		private string _key;
	}
}
