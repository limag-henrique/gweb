using System;
using System.ComponentModel;

namespace System.Windows.Input
{
	/// <summary>Define um <see cref="T:System.Windows.Input.ICommand" /> que é roteado através da árvore de elemento e contém uma propriedade de texto.</summary>
	// Token: 0x02000222 RID: 546
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public class RoutedUICommand : RoutedCommand
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedUICommand" />.</summary>
		// Token: 0x06000ECA RID: 3786 RVA: 0x00037FB0 File Offset: 0x000373B0
		public RoutedUICommand()
		{
			this._text = string.Empty;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedUICommand" /> usando o texto descritivo, o nome declarado e o tipo de proprietário especificados.</summary>
		/// <param name="text">Texto descritivo para o comando.</param>
		/// <param name="name">O nome declarado do comando para serialização.</param>
		/// <param name="ownerType">O tipo que está registrando o comando.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ownerType" /> é <see langword="null" />.</exception>
		// Token: 0x06000ECB RID: 3787 RVA: 0x00037FD0 File Offset: 0x000373D0
		public RoutedUICommand(string text, string name, Type ownerType) : this(text, name, ownerType, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.RoutedUICommand" /> usando o texto descritivo, o nome declarado, o tipo de proprietário e os gestos de entrada especificados.</summary>
		/// <param name="text">Texto descritivo para o comando.</param>
		/// <param name="name">O nome declarado do comando para serialização.</param>
		/// <param name="ownerType">O tipo que está registrando o comando.</param>
		/// <param name="inputGestures">Uma coleção de gestos a serem associados ao comando.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ownerType" /> é <see langword="null" />.</exception>
		// Token: 0x06000ECC RID: 3788 RVA: 0x00037FE8 File Offset: 0x000373E8
		public RoutedUICommand(string text, string name, Type ownerType, InputGestureCollection inputGestures) : base(name, ownerType, inputGestures)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this._text = text;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00038014 File Offset: 0x00037414
		internal RoutedUICommand(string name, Type ownerType, byte commandId) : base(name, ownerType, commandId)
		{
		}

		/// <summary>Obtém ou define o texto que descreve este comando.</summary>
		/// <returns>O texto que descreve o comando.  O padrão é uma cadeia de caracteres vazia.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003802C File Offset: 0x0003742C
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00038054 File Offset: 0x00037454
		public string Text
		{
			get
			{
				if (this._text == null)
				{
					this._text = this.GetText();
				}
				return this._text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._text = value;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00038078 File Offset: 0x00037478
		private string GetText()
		{
			if (base.OwnerType == typeof(ApplicationCommands))
			{
				return ApplicationCommands.GetUIText(base.CommandId);
			}
			if (base.OwnerType == typeof(NavigationCommands))
			{
				return NavigationCommands.GetUIText(base.CommandId);
			}
			if (base.OwnerType == typeof(MediaCommands))
			{
				return MediaCommands.GetUIText(base.CommandId);
			}
			if (base.OwnerType == typeof(ComponentCommands))
			{
				return ComponentCommands.GetUIText(base.CommandId);
			}
			return null;
		}

		// Token: 0x04000858 RID: 2136
		private string _text;
	}
}
