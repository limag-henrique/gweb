using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa uma composição relacionada à entrada de texto que inclui o texto de composição em si, o texto do controle ou do sistema relacionado e um estado de conclusão para a composição.</summary>
	// Token: 0x020002D9 RID: 729
	public class TextComposition : DispatcherObject
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.TextComposition" />, considerando um <see cref="T:System.Windows.Input.InputManager" /> especificado, um elemento de origem e um texto de composição como valores iniciais para a nova instância.</summary>
		/// <param name="inputManager">Um gerente de entrada a ser associado com essa composição de texto.</param>
		/// <param name="source">Um elemento de origem para essa composição de texto.  O objeto subjacente que elemento de origem deve implementar na interface <see cref="T:System.Windows.IInputElement" />.</param>
		/// <param name="resultText">Uma cadeia de caracteres que contém o texto inicial para a composição.  Esse parâmetro se tornará o valor da propriedade <see cref="P:System.Windows.Input.TextComposition.Text" /> na nova instância da classe.</param>
		// Token: 0x06001605 RID: 5637 RVA: 0x000526B0 File Offset: 0x00051AB0
		public TextComposition(InputManager inputManager, IInputElement source, string resultText) : this(inputManager, source, resultText, TextCompositionAutoComplete.On)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.TextComposition" />, considerando um <see cref="T:System.Windows.Input.InputManager" /> especificado, um elemento de origem, um texto de composição e uma configuração <see cref="T:System.Windows.Input.TextCompositionAutoComplete" /> como valores iniciais para a nova instância.</summary>
		/// <param name="inputManager">Um gerente de entrada a ser associado com essa composição de texto.</param>
		/// <param name="source">Um elemento de origem para essa composição de texto.  O objeto subjacente que elemento de origem deve implementar na interface <see cref="T:System.Windows.IInputElement" />.</param>
		/// <param name="resultText">Uma cadeia de caracteres que contém o texto inicial para a composição.  Esse parâmetro se tornará o valor da propriedade <see cref="P:System.Windows.Input.TextComposition.Text" /> na nova instância da classe.</param>
		/// <param name="autoComplete">Um membro das enumerações <see cref="T:System.Windows.Input.TextCompositionAutoComplete" /> que especifica o comportamento de preenchimento automático desejado para essa composição de texto.</param>
		// Token: 0x06001606 RID: 5638 RVA: 0x000526C8 File Offset: 0x00051AC8
		[SecurityCritical]
		public TextComposition(InputManager inputManager, IInputElement source, string resultText, TextCompositionAutoComplete autoComplete) : this(inputManager, source, resultText, autoComplete, InputManager.Current.PrimaryKeyboardDevice)
		{
			if (autoComplete != TextCompositionAutoComplete.Off && autoComplete != TextCompositionAutoComplete.On)
			{
				throw new InvalidEnumArgumentException("autoComplete", (int)autoComplete, typeof(TextCompositionAutoComplete));
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0005270C File Offset: 0x00051B0C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal TextComposition(InputManager inputManager, IInputElement source, string resultText, TextCompositionAutoComplete autoComplete, InputDevice inputDevice)
		{
			this._inputManager = inputManager;
			this._inputDevice = inputDevice;
			if (resultText == null)
			{
				throw new ArgumentException(SR.Get("TextComposition_NullResultText"));
			}
			this._resultText = resultText;
			this._compositionText = "";
			this._systemText = "";
			this._systemCompositionText = "";
			this._controlText = "";
			this._autoComplete = autoComplete;
			this._stage = TextCompositionStage.None;
			this._source = source;
		}

		/// <summary>Conclui essa composição de texto.</summary>
		// Token: 0x06001608 RID: 5640 RVA: 0x0005278C File Offset: 0x00051B8C
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Complete()
		{
			TextCompositionManager.CompleteComposition(this);
		}

		/// <summary>Obtém ou define o texto atual para essa composição de texto.</summary>
		/// <returns>Uma cadeia de caracteres que contém o texto atual para essa composição de texto.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x000527A0 File Offset: 0x00051BA0
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x000527B4 File Offset: 0x00051BB4
		[CLSCompliant(false)]
		public string Text
		{
			get
			{
				return this._resultText;
			}
			protected set
			{
				this._resultText = value;
			}
		}

		/// <summary>Obtém ou define o texto de composição para essa composição de texto.</summary>
		/// <returns>Uma cadeia de caracteres que contém o texto de composição para essa composição de texto.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x000527C8 File Offset: 0x00051BC8
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x000527DC File Offset: 0x00051BDC
		[CLSCompliant(false)]
		public string CompositionText
		{
			get
			{
				return this._compositionText;
			}
			protected set
			{
				this._compositionText = value;
			}
		}

		/// <summary>Obtém ou define o texto do sistema para essa composição de texto.</summary>
		/// <returns>Uma cadeia de caracteres que contém o texto do sistema para essa composição de texto.</returns>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000527F0 File Offset: 0x00051BF0
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00052804 File Offset: 0x00051C04
		[CLSCompliant(false)]
		public string SystemText
		{
			get
			{
				return this._systemText;
			}
			protected set
			{
				this._systemText = value;
			}
		}

		/// <summary>Obtém ou define qualquer texto de controle associado com essa composição de texto.</summary>
		/// <returns>Uma cadeia de caracteres que contém qualquer texto de controle associado a essa composição de texto.</returns>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00052818 File Offset: 0x00051C18
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x0005282C File Offset: 0x00051C2C
		[CLSCompliant(false)]
		public string ControlText
		{
			get
			{
				return this._controlText;
			}
			protected set
			{
				this._controlText = value;
			}
		}

		/// <summary>Obtém ou define o texto de composição do sistema para essa composição de texto.</summary>
		/// <returns>Uma cadeia de caracteres que contém o texto de composição do sistema para essa composição de texto.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00052840 File Offset: 0x00051C40
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00052854 File Offset: 0x00051C54
		[CLSCompliant(false)]
		public string SystemCompositionText
		{
			get
			{
				return this._systemCompositionText;
			}
			protected set
			{
				this._systemCompositionText = value;
			}
		}

		/// <summary>Obtém a configuração de preenchimento automático para essa composição de texto.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.TextCompositionAutoComplete" /> enumerações especificando o comportamento de preenchimento automático atual para essa composição de texto.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00052868 File Offset: 0x00051C68
		public TextCompositionAutoComplete AutoComplete
		{
			get
			{
				return this._autoComplete;
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0005287C File Offset: 0x00051C7C
		internal void SetText(string resultText)
		{
			this._resultText = resultText;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00052890 File Offset: 0x00051C90
		internal void SetCompositionText(string compositionText)
		{
			this._compositionText = compositionText;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000528A4 File Offset: 0x00051CA4
		internal void MakeSystem()
		{
			this._systemText = this._resultText;
			this._systemCompositionText = this._compositionText;
			this._resultText = "";
			this._compositionText = "";
			this._controlText = "";
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000528EC File Offset: 0x00051CEC
		internal void MakeControl()
		{
			this._controlText = this._resultText;
			this._resultText = "";
			this._systemText = "";
			this._compositionText = "";
			this._systemCompositionText = "";
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00052934 File Offset: 0x00051D34
		internal void ClearTexts()
		{
			this._resultText = "";
			this._compositionText = "";
			this._systemText = "";
			this._systemCompositionText = "";
			this._controlText = "";
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00052978 File Offset: 0x00051D78
		internal IInputElement Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0005298C File Offset: 0x00051D8C
		internal InputDevice _InputDevice
		{
			get
			{
				return this._inputDevice;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x000529A0 File Offset: 0x00051DA0
		internal InputManager _InputManager
		{
			[SecurityCritical]
			get
			{
				return this._inputManager;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000529B4 File Offset: 0x00051DB4
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x000529C8 File Offset: 0x00051DC8
		internal TextCompositionStage Stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		// Token: 0x04000C06 RID: 3078
		[SecurityCritical]
		private readonly InputManager _inputManager;

		// Token: 0x04000C07 RID: 3079
		private readonly InputDevice _inputDevice;

		// Token: 0x04000C08 RID: 3080
		private string _resultText;

		// Token: 0x04000C09 RID: 3081
		private string _compositionText;

		// Token: 0x04000C0A RID: 3082
		private string _systemText;

		// Token: 0x04000C0B RID: 3083
		private string _controlText;

		// Token: 0x04000C0C RID: 3084
		private string _systemCompositionText;

		// Token: 0x04000C0D RID: 3085
		private readonly TextCompositionAutoComplete _autoComplete;

		// Token: 0x04000C0E RID: 3086
		private TextCompositionStage _stage;

		// Token: 0x04000C0F RID: 3087
		private IInputElement _source;
	}
}
