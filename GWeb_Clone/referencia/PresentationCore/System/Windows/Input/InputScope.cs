using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Input
{
	/// <summary>Representa informações relacionadas ao escopo de dados fornecido por um método de entrada.</summary>
	// Token: 0x0200025F RID: 607
	[TypeConverter("System.Windows.Input.InputScopeConverter, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public class InputScope
	{
		/// <summary>Obtém ou define o nome do escopo de entrada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.Input.InputScopeName" /> enumeração que especifica um nome para esse escopo de entrada.  
		/// O valor padrão é <see cref="F:System.Windows.Input.InputScopeNameValue.Default" />.</returns>
		/// <exception cref="T:System.ArgumentException">Gerado quando é feita uma tentativa de definir essa propriedade como qualquer valor diferente de um membro válido da enumeração <see cref="T:System.Windows.Input.InputScopeName" />.</exception>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00040568 File Offset: 0x0003F968
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public IList Names
		{
			get
			{
				return (IList)this._scopeNames;
			}
		}

		/// <summary>Obtém ou define uma cadeia de caracteres que especifica qualquer marcação SRGS (Especificação de Gramática de Reconhecimento de Fala) a ser usada como um padrão de entrada sugerido pelos processadores de entrada.</summary>
		/// <returns>Uma cadeia de caracteres que especifica qualquer marcação SRGS a ser usado como um padrão de entrada sugerido pelos processadores de entrada.  
		/// Esta propriedade não tem valor padrão.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando é feita uma tentativa de definir essa propriedade como null.</exception>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00040580 File Offset: 0x0003F980
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00040594 File Offset: 0x0003F994
		[DefaultValue(null)]
		public string SrgsMarkup
		{
			get
			{
				return this._srgsMarkup;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._srgsMarkup = value;
			}
		}

		/// <summary>Obtém ou define uma expressão regular a ser usada como um padrão de entrada de texto sugerido pelos processadores de entrada.</summary>
		/// <returns>Uma cadeia de caracteres que define uma expressão regular a ser usado como um padrão de entrada de texto sugerido pelos processadores de entrada.  
		/// Esta propriedade não tem valor padrão.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando é feita uma tentativa de definir essa propriedade como null.</exception>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x000405B8 File Offset: 0x0003F9B8
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x000405CC File Offset: 0x0003F9CC
		[DefaultValue(null)]
		public string RegularExpression
		{
			get
			{
				return this._regexString;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._regexString = value;
			}
		}

		/// <summary>Obtém uma coleção de frases a serem usadas como padrões de entrada sugeridos pelos processadores de entrada.</summary>
		/// <returns>Um objeto que contém uma coleção de frases a serem usadas como padrões de entrada sugeridos pelos processadores de entrada.  Esse objeto implementa a <see cref="T:System.Collections.IList" /> interface.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000405F0 File Offset: 0x0003F9F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public IList PhraseList
		{
			get
			{
				return (IList)this._phraseList;
			}
		}

		// Token: 0x0400093A RID: 2362
		private IList<InputScopeName> _scopeNames = new List<InputScopeName>();

		// Token: 0x0400093B RID: 2363
		private IList<InputScopePhrase> _phraseList = new List<InputScopePhrase>();

		// Token: 0x0400093C RID: 2364
		private string _regexString;

		// Token: 0x0400093D RID: 2365
		private string _srgsMarkup;
	}
}
