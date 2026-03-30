using System;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Contém argumentos para o <see cref="T:System.Windows.DataObject" />.<see cref="E:System.Windows.DataObject.Pasting" /> .</summary>
	// Token: 0x02000199 RID: 409
	public sealed class DataObjectPastingEventArgs : DataObjectEventArgs
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.DataObjectPastingEventArgs" />.</summary>
		/// <param name="dataObject">Um <see cref="T:System.Windows.DataObject" /> que contém os dados a serem colados.</param>
		/// <param name="isDragDrop">Um valor <see langword="Boolean" /> que indica se colar faz parte de uma operação do tipo “arrastar e soltar”. <see langword="true" /> para indica que a colagem faz parte de uma operação do tipo “arrastar e soltar”; caso contrário, <see langword="false" />. Se esse parâmetro for definido como <see langword="true" />, um evento <see cref="E:System.Windows.DataObject.Pasting" /> será acionado ao soltar.</param>
		/// <param name="formatToApply">Uma cadeia de caracteres que especifica o formato de dados preferencial a ser usado para a operação colar. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="dataObject" /> ou <paramref name="formatToApply" /> for nulo.</exception>
		/// <exception cref="T:System.ArgumentException">Gerado quando <paramref name="formatToApply" /> especifica um formato de dados que não está presente no objeto de dados especificado por <paramref name="dataObject" />.</exception>
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001C5A4 File Offset: 0x0001B9A4
		public DataObjectPastingEventArgs(IDataObject dataObject, bool isDragDrop, string formatToApply) : base(System.Windows.DataObject.PastingEvent, isDragDrop)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			if (formatToApply == null)
			{
				throw new ArgumentNullException("formatToApply");
			}
			if (formatToApply == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			if (!dataObject.GetDataPresent(formatToApply))
			{
				throw new ArgumentException(SR.Get("DataObject_DataFormatNotPresentOnDataObject", new object[]
				{
					formatToApply
				}));
			}
			this._originalDataObject = dataObject;
			this._dataObject = dataObject;
			this._formatToApply = formatToApply;
		}

		/// <summary>Obtém uma cópia do objeto de dados original associado à operação colar.</summary>
		/// <returns>Uma cópia do objeto de dados original associado com a operação de colagem.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001C630 File Offset: 0x0001BA30
		public IDataObject SourceDataObject
		{
			get
			{
				return this._originalDataObject;
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.DataObject" /> sugerido a ser usado para a operação colar.</summary>
		/// <returns>No momento sugerido <see cref="T:System.Windows.DataObject" /> a ser usado para a operação de colagem.  
		/// Obter esse valor retorna atualmente sugerido <see cref="T:System.Windows.DataObject" /> para a operação Colar.  
		/// Definir esse valor especifica um novo sugerido <see cref="T:System.Windows.DataObject" /> a ser usado para a operação de colagem.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando é feita uma tentativa de definir essa propriedade como null.</exception>
		/// <exception cref="T:System.ArgumentException">Gerado quando é feita uma tentativa de definir esta propriedade para um objeto de dados que não contém nenhum formato de dados.</exception>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001C644 File Offset: 0x0001BA44
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0001C658 File Offset: 0x0001BA58
		public IDataObject DataObject
		{
			get
			{
				return this._dataObject;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				string[] formats = value.GetFormats(false);
				if (formats == null || formats.Length == 0)
				{
					throw new ArgumentException(SR.Get("DataObject_DataObjectMustHaveAtLeastOneFormat"));
				}
				this._dataObject = value;
				this._formatToApply = formats[0];
			}
		}

		/// <summary>Obtém ou define uma cadeia de caracteres que especifica o formato de dados sugerido a ser usado para a operação colar.</summary>
		/// <returns>Uma cadeia de caracteres especificando o formato de dados sugerido a ser usado para a operação de colagem.  
		/// Obter esse valor retorna o formato de dados atualmente sugerido a ser usado para a operação de colagem.  
		/// Se você definir esse valor, um novo formato de dados sugerido a ser usado para a operação de colagem.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando é feita uma tentativa de definir essa propriedade como null.</exception>
		/// <exception cref="T:System.ArgumentException">Gerado quando é feita uma tentativa de definir esta propriedade para um formato de dados que não está presente no objeto de dados referenciado pela propriedade <see cref="P:System.Windows.DataObjectPastingEventArgs.DataObject" />.</exception>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001C6A4 File Offset: 0x0001BAA4
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0001C6B8 File Offset: 0x0001BAB8
		public string FormatToApply
		{
			get
			{
				return this._formatToApply;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this._dataObject.GetDataPresent(value))
				{
					throw new ArgumentException(SR.Get("DataObject_DataFormatNotPresentOnDataObject", new object[]
					{
						value
					}));
				}
				this._formatToApply = value;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001C704 File Offset: 0x0001BB04
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			DataObjectPastingEventHandler dataObjectPastingEventHandler = (DataObjectPastingEventHandler)genericHandler;
			dataObjectPastingEventHandler(genericTarget, this);
		}

		// Token: 0x04000566 RID: 1382
		private IDataObject _originalDataObject;

		// Token: 0x04000567 RID: 1383
		private IDataObject _dataObject;

		// Token: 0x04000568 RID: 1384
		private string _formatToApply;
	}
}
