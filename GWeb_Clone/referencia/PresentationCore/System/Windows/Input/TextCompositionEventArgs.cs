using System;

namespace System.Windows.Input
{
	/// <summary>Contém argumentos associados às alterações em um <see cref="T:System.Windows.Input.TextComposition" />.</summary>
	// Token: 0x020002DA RID: 730
	public class TextCompositionEventArgs : InputEventArgs
	{
		/// <summary>Inicializa uma nova instância do <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> classe, levando a especificada <see cref="T:System.Windows.Input.InputDevice" /> e <see cref="T:System.Windows.Input.TextComposition" /> como valores iniciais para a classe.</summary>
		/// <param name="inputDevice">O dispositivo de entrada associado a este evento.</param>
		/// <param name="composition">Um <see cref="T:System.Windows.Input.TextComposition" /> objeto associado ao evento.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando a composição é nula.</exception>
		// Token: 0x0600161E RID: 5662 RVA: 0x000529DC File Offset: 0x00051DDC
		public TextCompositionEventArgs(InputDevice inputDevice, TextComposition composition) : base(inputDevice, Environment.TickCount)
		{
			if (composition == null)
			{
				throw new ArgumentNullException("composition");
			}
			this._composition = composition;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.TextComposition" /> associado com um <see cref="T:System.Windows.Input.TextComposition" /> eventos.</summary>
		/// <returns>Um <see cref="T:System.Windows.Input.TextComposition" /> objeto que contém a composição de texto associada ao evento.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00052A0C File Offset: 0x00051E0C
		public TextComposition TextComposition
		{
			get
			{
				return this._composition;
			}
		}

		/// <summary>Obtém entrada texto associado a um <see cref="T:System.Windows.Input.TextComposition" /> eventos.</summary>
		/// <returns>Uma cadeia de caracteres que contém o texto de entrada associado ao evento.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00052A20 File Offset: 0x00051E20
		public string Text
		{
			get
			{
				return this._composition.Text;
			}
		}

		/// <summary>Obtém o texto do sistema associado a um <see cref="T:System.Windows.Input.TextComposition" /> eventos.</summary>
		/// <returns>Uma cadeia de caracteres que contém qualquer texto de sistema associado ao evento.</returns>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00052A38 File Offset: 0x00051E38
		public string SystemText
		{
			get
			{
				return this._composition.SystemText;
			}
		}

		/// <summary>Obtém controle texto associado a um <see cref="T:System.Windows.Input.TextComposition" /> eventos.</summary>
		/// <returns>Uma cadeia de caracteres que contém qualquer texto do controle associado ao evento.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00052A50 File Offset: 0x00051E50
		public string ControlText
		{
			get
			{
				return this._composition.ControlText;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001623 RID: 5667 RVA: 0x00052A68 File Offset: 0x00051E68
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			TextCompositionEventHandler textCompositionEventHandler = (TextCompositionEventHandler)genericHandler;
			textCompositionEventHandler(genericTarget, this);
		}

		// Token: 0x04000C10 RID: 3088
		private TextComposition _composition;
	}
}
