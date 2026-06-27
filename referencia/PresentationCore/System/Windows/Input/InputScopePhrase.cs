using System;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Representa um padrão de texto de entrada sugerido.</summary>
	// Token: 0x02000261 RID: 609
	[ContentProperty("Name")]
	public class InputScopePhrase : IAddChild
	{
		/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
		// Token: 0x06001121 RID: 4385 RVA: 0x000407C4 File Offset: 0x0003FBC4
		public InputScopePhrase()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputScopePhrase" />, pegando uma cadeia de caracteres que especifica o <see cref="P:System.Windows.Input.InputScopePhrase.Name" /> da frase do escopo de entrada.</summary>
		/// <param name="name">Uma cadeia de caracteres especificando o valor inicial para a propriedade <see cref="P:System.Windows.Input.InputScopePhrase.Name" />.  Este valor pode não ser <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerado se <paramref name="name" /> for <see langword="null" />.</exception>
		// Token: 0x06001122 RID: 4386 RVA: 0x000407D8 File Offset: 0x0003FBD8
		public InputScopePhrase(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._phraseName = name;
		}

		/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
		/// <param name="value">Um objeto a ser adicionado como filho.</param>
		// Token: 0x06001123 RID: 4387 RVA: 0x00040800 File Offset: 0x0003FC00
		public void AddChild(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
		/// <param name="name">Uma cadeia de caracteres a ser adicionada.</param>
		// Token: 0x06001124 RID: 4388 RVA: 0x00040814 File Offset: 0x0003FC14
		public void AddText(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._phraseName = name;
		}

		/// <summary>Obtém ou define um nome descritivo associado com o padrão de entrada de texto para este <see cref="T:System.Windows.Input.InputScopePhrase" />.</summary>
		/// <returns>Uma cadeia de caracteres que contém o nome descritivo para este <see cref="T:System.Windows.Input.InputScopePhrase" />.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00040838 File Offset: 0x0003FC38
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0004084C File Offset: 0x0003FC4C
		public string Name
		{
			get
			{
				return this._phraseName;
			}
			set
			{
				this._phraseName = value;
			}
		}

		// Token: 0x0400093F RID: 2367
		private string _phraseName;
	}
}
