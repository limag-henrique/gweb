using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	/// <summary>Fornece uma implementação básica da interface <see cref="T:System.Windows.IDataObject" />, que define um mecanismo independente de formato para a transferência de dados.</summary>
	// Token: 0x02000195 RID: 405
	public sealed class DataObject : IDataObject, IDataObject
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataObject" />.</summary>
		// Token: 0x06000592 RID: 1426 RVA: 0x0001A5B8 File Offset: 0x000199B8
		[SecurityCritical]
		public DataObject()
		{
			SecurityHelper.DemandAllClipboardPermission();
			this._innerData = new DataObject.DataStore();
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataObject" /> que contém os dados especificados.</summary>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="data" /> faz referência a um objeto <see cref="T:System.Windows.DataObject" />.</exception>
		// Token: 0x06000593 RID: 1427 RVA: 0x0001A5DC File Offset: 0x000199DC
		[SecurityCritical]
		public DataObject(object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			IDataObject dataObject = data as IDataObject;
			if (dataObject != null)
			{
				this._innerData = dataObject;
				return;
			}
			IDataObject dataObject2 = data as IDataObject;
			if (dataObject2 != null)
			{
				this._innerData = new DataObject.OleConverter(dataObject2);
				return;
			}
			this._innerData = new DataObject.DataStore();
			this.SetData(data);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataObject" /> que contém os dados especificados e o formato associado; o formato é especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x06000594 RID: 1428 RVA: 0x0001A640 File Offset: 0x00019A40
		[SecurityCritical]
		public DataObject(string format, object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData = new DataObject.DataStore();
			this.SetData(format, data);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataObject" /> que contém os dados especificados e seu formato associado; o formato de dados é especificado por um objeto <see cref="T:System.Type" />.</summary>
		/// <param name="format">Um <see cref="T:System.Type" /> que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x06000595 RID: 1429 RVA: 0x0001A6A4 File Offset: 0x00019AA4
		[SecurityCritical]
		public DataObject(Type format, object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData = new DataObject.DataStore();
			this.SetData(format.FullName, data);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataObject" /> que contém os dados especificados e o formato associado; o formato é especificado por uma cadeia de caracteres. Essa sobrecarga inclui um sinalizador <see langword="Boolean" /> para indicar se os dados podem ou não ser convertidos para outro formato ao serem recuperados.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> para permitir que os dados sejam convertidos em outro formato ao serem recuperados; <see langword="false" /> para impedir que os dados sejam convertidos em outro formato ao serem recuperados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x06000596 RID: 1430 RVA: 0x0001A6F8 File Offset: 0x00019AF8
		[SecurityCritical]
		public DataObject(string format, object data, bool autoConvert)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData = new DataObject.DataStore();
			this.SetData(format, data, autoConvert);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A760 File Offset: 0x00019B60
		internal DataObject(IDataObject data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData = data;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A788 File Offset: 0x00019B88
		internal DataObject(IDataObject data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData = new DataObject.OleConverter(data);
		}

		/// <summary>Retorna um objeto de dados em um formato especificado, convertendo opcionalmente os dados no formato especificado.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> para tentar converter automaticamente os dados no formato especificado; <see langword="false" /> para nenhuma conversão de formato de dados.</param>
		/// <returns>Um objeto de dados com os dados no formato especificado ou <see langword="null" /> se os dados estiverem indisponíveis no formato especificado.  
		/// Se o parâmetro <paramref name="autoConvert" /> for <see langword="true" /> e os dados não puderem ser convertidos no formato especificado ou se a conversão automática estiver desabilitada (chamando <see cref="M:System.Windows.DataObject.SetData(System.String,System.Object,System.Boolean)" /> com o parâmetro <paramref name="autoConvert" /> definido como <see langword="false" />), esse método retornará <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é nulo.</exception>
		// Token: 0x06000599 RID: 1433 RVA: 0x0001A7B8 File Offset: 0x00019BB8
		public object GetData(string format, bool autoConvert)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return this._innerData.GetData(format, autoConvert);
		}

		/// <summary>Retorna dados em um formato especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <returns>Um objeto que contém os dados no formato especificado ou <see langword="null" /> se os dados não estiverem disponíveis no formato especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x0600059A RID: 1434 RVA: 0x0001A800 File Offset: 0x00019C00
		public object GetData(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return this.GetData(format, true);
		}

		/// <summary>Retorna um objeto de dados em um formato especificado por um objeto <see cref="T:System.Type" />.</summary>
		/// <param name="format">Um <see cref="T:System.Type" /> que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <returns>Um objeto de dados com os dados no formato especificado ou <see langword="null" /> se os dados estiverem indisponíveis no formato especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001A840 File Offset: 0x00019C40
		public object GetData(Type format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetData(format.FullName);
		}

		/// <summary>Determina se os dados estão disponíveis em um formato especificado por um objeto <see cref="T:System.Type" /> ou se eles podem ser convertidos nesse formato.</summary>
		/// <param name="format">Um <see cref="T:System.Type" /> que especifica o formato dos dados a ser verificado. F ou um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x0600059C RID: 1436 RVA: 0x0001A870 File Offset: 0x00019C70
		public bool GetDataPresent(Type format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetDataPresent(format.FullName);
		}

		/// <summary>Determina se os dados estão disponíveis em um formato especificado ou se podem ser convertidos nele. Um sinalizador <see langword="Boolean" /> indica se é necessário verificar se os dados podem ser convertidos no formato especificado, se ele não estiver disponível nesse formato.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados a ser verificado. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="autoConvert">
		///   <see langword="false" /> para verificar somente em busca do formato especificado; <see langword="true" /> para também verificar se os dados armazenados neste objeto de dados podem ser convertidos no formato especificado.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x0600059D RID: 1437 RVA: 0x0001A8A0 File Offset: 0x00019CA0
		public bool GetDataPresent(string format, bool autoConvert)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return this._innerData.GetDataPresent(format, autoConvert);
		}

		/// <summary>Determina se os dados estão disponíveis em um formato especificado por uma cadeia de caracteres ou se eles podem ser convertidos nesse formato.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x0600059E RID: 1438 RVA: 0x0001A8E8 File Offset: 0x00019CE8
		public bool GetDataPresent(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return this.GetDataPresent(format, true);
		}

		/// <summary>Retorna uma lista de formatos em que os dados neste objeto de dados estão armazenados. Um sinalizador <see langword="Boolean" /> indica se os formatos nos quais os dados podem ser automaticamente convertidos devem ser incluídos.</summary>
		/// <param name="autoConvert">
		///   <see langword="true" /> para recuperar todos os formatos em que os dados neste objeto de dados estão armazenados ou para os quais podem ser convertidos; <see langword="false" /> para recuperar apenas os formatos em que os dados neste objeto de dados são armazenados.</param>
		/// <returns>Uma matriz de cadeias de caracteres, com cada cadeia de caracteres especificando o nome de um formato compatível com este objeto de dados.</returns>
		// Token: 0x0600059F RID: 1439 RVA: 0x0001A928 File Offset: 0x00019D28
		public string[] GetFormats(bool autoConvert)
		{
			return this._innerData.GetFormats(autoConvert);
		}

		/// <summary>Retorna uma lista de formatos em que os dados neste objeto de dados estão armazenados ou nos quais eles podem ser convertidos.</summary>
		/// <returns>Uma matriz de cadeias de caracteres, com cada cadeia de caracteres especificando o nome de um formato compatível com este objeto de dados.</returns>
		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A944 File Offset: 0x00019D44
		public string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		/// <summary>Armazena os dados especificados neste objeto de dados, determinando automaticamente o formato de dados do tipo de objeto de origem.</summary>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> é <see langword="null" />.</exception>
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A958 File Offset: 0x00019D58
		[SecurityCritical]
		public void SetData(object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData.SetData(data);
		}

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados; o formato de dados é especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A984 File Offset: 0x00019D84
		[SecurityCritical]
		public void SetData(string format, object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData.SetData(format, data);
		}

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados; o formato de dados é especificado por um objeto <see cref="T:System.Type" />.</summary>
		/// <param name="format">Um objeto <see cref="T:System.Type" /> que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A9DC File Offset: 0x00019DDC
		[SecurityCritical]
		public void SetData(Type format, object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData.SetData(format, data);
		}

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados. Essa sobrecarga inclui um sinalizador <see langword="Boolean" /> para indicar se os dados podem ou não ser convertidos para outro formato ao serem recuperados.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <param name="data">Um objeto que representa os dados a serem armazenados neste objeto de dados.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> para permitir que os dados sejam convertidos em outro formato ao serem recuperados; <see langword="false" /> para impedir que os dados sejam convertidos em outro formato ao serem recuperados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> ou <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001AA20 File Offset: 0x00019E20
		[SecurityCritical]
		public void SetData(string format, object data, bool autoConvert)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			this.CriticalSetData(format, data, autoConvert);
		}

		/// <summary>Consulta a presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" /> em um objeto de dados.</summary>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados contém dados no formato de dados <see langword="false" />; caso contrário, <see cref="F:System.Windows.DataFormats.WaveAudio" />.</returns>
		// Token: 0x060005A5 RID: 1445 RVA: 0x0001AA68 File Offset: 0x00019E68
		public bool ContainsAudio()
		{
			return this.GetDataPresent(DataFormats.WaveAudio, false);
		}

		/// <summary>Consulta a presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.FileDrop" /> em um objeto de dados.</summary>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados contém dados no formato de dados <see langword="false" />; caso contrário, <see cref="F:System.Windows.DataFormats.FileDrop" />.</returns>
		// Token: 0x060005A6 RID: 1446 RVA: 0x0001AA84 File Offset: 0x00019E84
		public bool ContainsFileDropList()
		{
			return this.GetDataPresent(DataFormats.FileDrop, false);
		}

		/// <summary>Consulta a presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.Bitmap" /> em um objeto de dados.</summary>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados contém dados no formato de dados <see langword="false" />; caso contrário, <see cref="F:System.Windows.DataFormats.Bitmap" />.</returns>
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001AAA0 File Offset: 0x00019EA0
		public bool ContainsImage()
		{
			return this.GetDataPresent(DataFormats.Bitmap, false);
		}

		/// <summary>Consulta a presença de dados no formato <see cref="F:System.Windows.DataFormats.UnicodeText" /> em um objeto de dados.</summary>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados contém dados no formato de dados <see langword="false" />; caso contrário, <see cref="F:System.Windows.DataFormats.UnicodeText" />.</returns>
		// Token: 0x060005A8 RID: 1448 RVA: 0x0001AABC File Offset: 0x00019EBC
		public bool ContainsText()
		{
			return this.ContainsText(TextDataFormat.UnicodeText);
		}

		/// <summary>Consulta a presença de dados em um formato de dados de texto em um objeto de dados.</summary>
		/// <param name="format">Um membro da enumeração <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto a ser consultado.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados contém dados em um formato de dados de texto; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> não especifica um membro válido de <see cref="T:System.Windows.TextDataFormat" />.</exception>
		// Token: 0x060005A9 RID: 1449 RVA: 0x0001AAD0 File Offset: 0x00019ED0
		public bool ContainsText(TextDataFormat format)
		{
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			return this.GetDataPresent(DataFormats.ConvertToDataFormats(format), false);
		}

		/// <summary>Retorna um fluxo que contém dados no formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />.</summary>
		/// <returns>Um fluxo que contém dados no formato <see cref="F:System.Windows.DataFormats.WaveAudio" /> ou <see langword="null" />, se os dados estiverem indisponíveis nesse formato.</returns>
		// Token: 0x060005AA RID: 1450 RVA: 0x0001AB08 File Offset: 0x00019F08
		public Stream GetAudioStream()
		{
			return this.GetData(DataFormats.WaveAudio, false) as Stream;
		}

		/// <summary>Retorna uma coleção de cadeia de caracteres que contém uma lista de arquivos ignorados.</summary>
		/// <returns>Uma coleção de cadeias de caracteres, na qual cada cadeia de caracteres especifica o nome de um arquivo na lista de arquivos ignorados ou <see langword="null" /> se os dados não estiverem disponíveis neste formato.</returns>
		// Token: 0x060005AB RID: 1451 RVA: 0x0001AB28 File Offset: 0x00019F28
		public StringCollection GetFileDropList()
		{
			StringCollection stringCollection = new StringCollection();
			string[] array = this.GetData(DataFormats.FileDrop, true) as string[];
			if (array != null)
			{
				stringCollection.AddRange(array);
			}
			return stringCollection;
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que contém dados no formato <see cref="F:System.Windows.DataFormats.Bitmap" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que contém dados no formato <see cref="F:System.Windows.DataFormats.Bitmap" /> ou <see langword="null" />, se os dados estiverem indisponíveis nesse formato.</returns>
		// Token: 0x060005AC RID: 1452 RVA: 0x0001AB58 File Offset: 0x00019F58
		public BitmapSource GetImage()
		{
			return this.GetData(DataFormats.Bitmap, true) as BitmapSource;
		}

		/// <summary>Retorna uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> neste objeto de dados.</summary>
		/// <returns>Uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> ou uma cadeia de caracteres vazia se nenhum dado <see cref="F:System.Windows.DataFormats.UnicodeText" /> estiver disponível.</returns>
		// Token: 0x060005AD RID: 1453 RVA: 0x0001AB78 File Offset: 0x00019F78
		public string GetText()
		{
			return this.GetText(TextDataFormat.UnicodeText);
		}

		/// <summary>Retorna uma cadeia de caracteres que contém dados de texto do formato especificado neste objeto de dados.</summary>
		/// <param name="format">Um membro de <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto indicado a ser recuperado.</param>
		/// <returns>Uma cadeia de caracteres que contém dados de texto no formato de dados especificado ou uma cadeia de caracteres vazia se nenhum dado de texto correspondente estiver disponível.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> não especifica um membro válido de <see cref="T:System.Windows.TextDataFormat" />.</exception>
		// Token: 0x060005AE RID: 1454 RVA: 0x0001AB8C File Offset: 0x00019F8C
		public string GetText(TextDataFormat format)
		{
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			string text = (string)this.GetData(DataFormats.ConvertToDataFormats(format), false);
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Armazena dados de áudio (formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />) neste objeto de dados. Os dados de áudio são especificados como uma matriz de bytes.</summary>
		/// <param name="audioBytes">Uma matriz de bytes que contém dados de áudio para armazenar no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioBytes" /> é <see langword="null" />.</exception>
		// Token: 0x060005AF RID: 1455 RVA: 0x0001ABD4 File Offset: 0x00019FD4
		public void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			this.SetAudio(new MemoryStream(audioBytes));
		}

		/// <summary>Armazena dados de áudio (formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />) neste objeto de dados.  Os dados de áudio são especificados como um fluxo.</summary>
		/// <param name="audioStream">Um fluxo que contém dados de áudio a serem armazenados no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioStream" /> é <see langword="null" />.</exception>
		// Token: 0x060005B0 RID: 1456 RVA: 0x0001ABFC File Offset: 0x00019FFC
		public void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			this.SetData(DataFormats.WaveAudio, audioStream, false);
		}

		/// <summary>Armazena os dados <see cref="F:System.Windows.DataFormats.FileDrop" /> neste objeto de dados.  A lista de arquivos ignorados é especificada como uma coleção de cadeia de caracteres.</summary>
		/// <param name="fileDropList">Uma coleção de cadeias de caracteres que contém a lista de arquivos ignorados a serem armazenados no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileDropList" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileDropList" /> não contém nenhuma cadeia de caracteres ou o caminho completo para o arquivo especificado na lista não pode ser resolvido.</exception>
		// Token: 0x060005B1 RID: 1457 RVA: 0x0001AC24 File Offset: 0x0001A024
		public void SetFileDropList(StringCollection fileDropList)
		{
			if (fileDropList == null)
			{
				throw new ArgumentNullException("fileDropList");
			}
			if (fileDropList.Count == 0)
			{
				throw new ArgumentException(SR.Get("DataObject_FileDropListIsEmpty", new object[]
				{
					fileDropList
				}));
			}
			foreach (string path in fileDropList)
			{
				try
				{
					string fullPath = Path.GetFullPath(path);
				}
				catch (ArgumentException ex)
				{
					throw new ArgumentException(SR.Get("DataObject_FileDropListHasInvalidFileDropPath", new object[]
					{
						ex
					}));
				}
			}
			string[] array = new string[fileDropList.Count];
			fileDropList.CopyTo(array, 0);
			this.SetData(DataFormats.FileDrop, array, true);
		}

		/// <summary>Armazena os dados <see cref="F:System.Windows.DataFormats.Bitmap" /> neste objeto de dados.  Os dados da imagem são especificados como um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
		/// <param name="image">Um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que contém os dados de imagem a serem armazenados no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> é <see langword="null" />.</exception>
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001AD0C File Offset: 0x0001A10C
		public void SetImage(BitmapSource image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.SetData(DataFormats.Bitmap, image, true);
		}

		/// <summary>Armazena os dados <see cref="F:System.Windows.DataFormats.UnicodeText" />, especificados como uma cadeia de caracteres, neste objeto de dados.</summary>
		/// <param name="textData">Uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> a serem armazenados no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> é <see langword="null" />.</exception>
		// Token: 0x060005B3 RID: 1459 RVA: 0x0001AD34 File Offset: 0x0001A134
		public void SetText(string textData)
		{
			if (textData == null)
			{
				throw new ArgumentNullException("textData");
			}
			this.SetText(textData, TextDataFormat.UnicodeText);
		}

		/// <summary>Armazena os dados de texto neste objeto de dados. O formato dos dados de texto a serem armazenados é especificado com um membro de <see cref="T:System.Windows.TextDataFormat" />.</summary>
		/// <param name="textData">Uma cadeia de caracteres que contém os dados de texto a serem armazenados no objeto de dados.</param>
		/// <param name="format">Um membro de <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto a ser armazenado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> é <see langword="null" />.</exception>
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001AD58 File Offset: 0x0001A158
		public void SetText(string textData, TextDataFormat format)
		{
			if (textData == null)
			{
				throw new ArgumentNullException("textData");
			}
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			this.SetData(DataFormats.ConvertToDataFormats(format), textData, false);
		}

		/// <summary>Cria uma conexão entre um objeto de dados e um coletor de consultoria. Este método é chamado por um objeto compatível com um coletor de consultoria e o habilita para ser notificado sobre alterações nos dados do objeto.</summary>
		/// <param name="pFormatetc">Uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passada por referência, que define o formato, dispositivo de destino, aspecto e meio que serão usados para notificações futuras.</param>
		/// <param name="advf">Um dos valores <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> que especifica um grupo de sinalizadores para controlar a conexão de consultoria.</param>
		/// <param name="pAdvSink">Um ponteiro para a interface <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> no coletor de consultoria que receberá a notificação de alteração.</param>
		/// <param name="pdwConnection">Quando esse método retornar, conterá um ponteiro para um token DWORD que identifica esta conexão. É possível usar esse token posteriormente para excluir a conexão de consultoria, passando-o para <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. Se esse valor for zero, a conexão não terá sido estabelecida. Este parâmetro é passado não inicializado.</param>
		/// <returns>Este método é compatível com os valores retornados padrão E_INVALIDARG, E_UNEXPECTED e E_OUTOFMEMORY e também com os seguintes:</returns>
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001ADA0 File Offset: 0x0001A1A0
		[SecurityCritical]
		int IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink pAdvSink, out int pdwConnection)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this._innerData).OleDataObject.DAdvise(ref pFormatetc, advf, pAdvSink, out pdwConnection);
			}
			pdwConnection = 0;
			return -2147467263;
		}

		/// <summary>Destrói um conexão de notificação que tinha sido estabelecida anteriormente.</summary>
		/// <param name="dwConnection">Um token DWORD que especifica a conexão a ser removida. Use o valor retornado por <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> quando a conexão tiver sido estabelecida originalmente.</param>
		// Token: 0x060005B6 RID: 1462 RVA: 0x0001ADE0 File Offset: 0x0001A1E0
		[SecurityCritical]
		void IDataObject.DUnadvise(int dwConnection)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this._innerData).OleDataObject.DUnadvise(dwConnection);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147467263);
		}

		/// <summary>Cria um objeto que pode ser usado para enumerar as conexões de consultoria atuais.</summary>
		/// <param name="enumAdvise">Quando este método retorna, contém um <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> que recebe o ponteiro de interface para o novo objeto de enumerador. Se a implementação definir <paramref name="enumAdvise" /> como <see langword="null" />, não haverá conexões com coletores de consultoria neste momento. Este parâmetro é passado não inicializado.</param>
		/// <returns>Este método é compatível com os valores retornados padrão E_OUTOFMEMORY e também com os seguintes:  
		///   Valor  
		///
		///   Descrição  
		///
		///   S_OK  
		///
		///   A instância do objeto de enumerador foi criada com sucesso ou não há conexões.  
		///
		///   OLE_E_ADVISENOTSUPPORTED  
		///
		///   Este objeto não é compatível com notificações de consultoria.</returns>
		// Token: 0x060005B7 RID: 1463 RVA: 0x0001AE1C File Offset: 0x0001A21C
		[SecurityCritical]
		int IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this._innerData).OleDataObject.EnumDAdvise(out enumAdvise);
			}
			enumAdvise = null;
			return -2147221501;
		}

		/// <summary>Cria um objeto para enumerar as estruturas <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> de um objeto de dados. Essas estruturas são usadas em chamadas a <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> ou <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />.</summary>
		/// <param name="dwDirection">Um dos valores <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> que especifica a direção dos dados.</param>
		/// <returns>Este método é compatível com os valores retornados padrão E_INVALIDARG e E_OUTOFMEMORY e também com os seguintes: 
		///   Valor 
		///   Descrição 
		///   S_OK 
		///   O objeto de enumerador foi criado com êxito.  
		///   E_NOTIMPL 
		///   A direção especificada pelo parâmetro <paramref name="direction" /> não é compatível.  
		///   OLE_S_USEREG 
		///   Solicita que a OLE enumere os formatos do registro.</returns>
		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AE58 File Offset: 0x0001A258
		[SecurityCritical]
		IEnumFORMATETC IDataObject.EnumFormatEtc(DATADIR dwDirection)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this._innerData).OleDataObject.EnumFormatEtc(dwDirection);
			}
			if (dwDirection == DATADIR.DATADIR_GET)
			{
				return new DataObject.FormatEnumerator(this);
			}
			throw new ExternalException(SR.Get("DataObject_NotImplementedEnumFormatEtc", new object[]
			{
				dwDirection
			}), -2147467263);
		}

		/// <summary>Fornece uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> padrão logicamente equivalente a uma estrutura mais complexa. Use esse método para determinar se duas estruturas <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> diferentes retornariam os mesmos dados, acabando com a necessidade de renderização duplicada.</summary>
		/// <param name="pformatetcIn">Um ponteiro para uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passado por referência, que define o formato, o meio e o dispositivo de destino que o chamador gostaria de usar para recuperar dados em uma chamada subsequente, como <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. O membro <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> não é significativo nesse caso e deve ser ignorado.</param>
		/// <param name="pformatetcOut">Quando este método retorna, contém um ponteiro para uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> que contém as informações mais gerais possíveis para uma renderização específica, tornando-a canonicamente equivalente a formatetIn. O chamador deve alocar esta estrutura e o método <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> deve preencher os dados. Para recuperar dados em uma chamada subsequente, como <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, o chamador usa o valor fornecido de formatOut, a menos que o valor fornecido seja <see langword="null" />. Esse valor será <see langword="null" /> se o método retornar <see langword="DATA_S_SAMEFORMATETC" />. O membro <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> não é significativo nesse caso e deve ser ignorado. Este parâmetro é passado não inicializado.</param>
		/// <returns>Este método é compatível com os valores retornados padrão E_INVALIDARG, E_UNEXPECTED e E_OUTOFMEMORY e também com os seguintes: 
		///   Valor 
		///   Descrição 
		///   S_OK 
		///   A estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> retornada é diferente da que foi passada.  
		///   DATA_S_SAMEFORMATETC 
		///   As estruturas <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> são iguais e <see langword="null" /> é retornado no parâmetro <paramref name="formatOut" />.  
		///   DV_E_LINDEX 
		///   Há um valor inválido para <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; no momento, apenas -1 é compatível.  
		///   DV_E_FORMATETC 
		///   Há um valor inválido para o parâmetro <paramref name="pFormatetc" />.  
		///   OLE_E_NOTRUNNING 
		///   O aplicativo não está em execução.</returns>
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AEB8 File Offset: 0x0001A2B8
		[SecurityCritical]
		int IDataObject.GetCanonicalFormatEtc(ref FORMATETC pformatetcIn, out FORMATETC pformatetcOut)
		{
			pformatetcOut = default(FORMATETC);
			pformatetcOut = pformatetcIn;
			pformatetcOut.ptd = IntPtr.Zero;
			if (pformatetcIn.lindex != -1)
			{
				return -2147221400;
			}
			if (this._innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this._innerData).OleDataObject.GetCanonicalFormatEtc(ref pformatetcIn, out pformatetcOut);
			}
			return 262448;
		}

		/// <summary>Obtém dados de um objeto de dados de origem. O método <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, chamado por um consumidor de dados, renderiza os dados descritos na estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> especificada e os transfere por meio da estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> especificada. Em seguida, o chamador assume a responsabilidade por liberar a estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />.</summary>
		/// <param name="formatetc">Um ponteiro para uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passado por referência, que define o formato, meio e dispositivo de destino a serem usados ao passar os dados. É possível especificar mais de um meio usando o operador OR booliano, permitindo que o método escolha o melhor meio entre aqueles especificados.</param>
		/// <param name="medium">Quando esse método é retornado, contém um ponteiro para a estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> que indica o meio de armazenamento que contém os dados retornados por meio de seu membro <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> e a responsabilidade para liberar o meio por meio do valor de seu membro <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" />. Se <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> for <see langword="null" />, o receptor do meio será responsável pela liberação; caso contrário, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> apontará para a interface <see langword="IUnknown" /> no objeto apropriado para que seu método <see langword="Release" /> possa ser chamado. O meio deve ser alocado e preenchido por <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. Este parâmetro é passado não inicializado.</param>
		// Token: 0x060005BA RID: 1466 RVA: 0x0001AF1C File Offset: 0x0001A31C
		[SecurityCritical]
		void IDataObject.GetData(ref FORMATETC formatetc, out STGMEDIUM medium)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this._innerData).OleDataObject.GetData(ref formatetc, out medium);
				return;
			}
			int num = -2147221399;
			medium = default(STGMEDIUM);
			if (this.GetTymedUseable(formatetc.tymed))
			{
				if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
				{
					medium.tymed = TYMED.TYMED_HGLOBAL;
					medium.unionmember = DataObject.Win32GlobalAlloc(8258, (IntPtr)1);
					num = this.OleGetDataUnrestricted(ref formatetc, ref medium, false);
					if (NativeMethods.Failed(num))
					{
						DataObject.Win32GlobalFree(new HandleRef(this, medium.unionmember));
					}
				}
				else if ((formatetc.tymed & TYMED.TYMED_ISTREAM) != TYMED.TYMED_NULL)
				{
					if (SecurityHelper.CheckUnmanagedCodePermission())
					{
						medium.tymed = TYMED.TYMED_ISTREAM;
						IStream o = null;
						num = DataObject.Win32CreateStreamOnHGlobal(IntPtr.Zero, true, ref o);
						if (NativeMethods.Succeeded(num))
						{
							medium.unionmember = Marshal.GetComInterfaceForObject(o, typeof(IStream));
							Marshal.ReleaseComObject(o);
							num = this.OleGetDataUnrestricted(ref formatetc, ref medium, false);
							if (NativeMethods.Failed(num))
							{
								Marshal.Release(medium.unionmember);
							}
						}
					}
					else
					{
						num = -2147467259;
					}
				}
				else
				{
					medium.tymed = formatetc.tymed;
					num = this.OleGetDataUnrestricted(ref formatetc, ref medium, false);
				}
			}
			if (NativeMethods.Failed(num))
			{
				medium.unionmember = IntPtr.Zero;
				Marshal.ThrowExceptionForHR(num);
			}
		}

		/// <summary>Obtém dados de um objeto de dados de origem. Esse método, chamado por um consumidor de dados, é diferente do método <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> em que o chamador deve alocar e liberar o meio de armazenamento especificado.</summary>
		/// <param name="formatetc">Um ponteiro para uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passado por referência, que define o formato, meio e dispositivo de destino a serem usados ao passar os dados. Apenas um meio pode ser especificado em <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> e apenas os seguintes valores <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> são válidos: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" /> ou <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
		/// <param name="medium">Um <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passado por referência, que define o meio de armazenamento que contém os dados que estão sendo transferidos. O meio deve ser alocado pelo chamador e preenchido pelo <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. O chamador também deve liberar o meio. A implementação deste método sempre deve fornecer um valor de <see langword="null" /> para o membro <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> da estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> para a qual que este parâmetro aponta.</param>
		// Token: 0x060005BB RID: 1467 RVA: 0x0001B064 File Offset: 0x0001A464
		[SecurityCritical]
		void IDataObject.GetDataHere(ref FORMATETC formatetc, ref STGMEDIUM medium)
		{
			if (medium.tymed != TYMED.TYMED_ISTORAGE && medium.tymed != TYMED.TYMED_ISTREAM && medium.tymed != TYMED.TYMED_HGLOBAL && medium.tymed != TYMED.TYMED_FILE)
			{
				Marshal.ThrowExceptionForHR(-2147221399);
			}
			int num = this.OleGetDataUnrestricted(ref formatetc, ref medium, true);
			if (NativeMethods.Failed(num))
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		/// <summary>Determina se o objeto de dados é capaz de renderizar os dados descritos na estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />. Objetos que tentam uma operação de colar ou de soltar podem chamar este método antes de chamar <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> para obter uma indicação se a operação pode ser bem-sucedida.</summary>
		/// <param name="formatetc">Um ponteiro para uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passado por referência, que define o formato, o meio e o dispositivo de destino a serem usados para a consulta.</param>
		/// <returns>Este método é compatível com os valores retornados padrão E_INVALIDARG, E_UNEXPECTED e E_OUTOFMEMORY e também com os seguintes: 
		///   Valor 
		///   Descrição 
		///   S_OK 
		///   Uma chamada subsequente a <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> provavelmente seria bem-sucedida.  
		///   DV_E_LINDEX 
		///   Um valor inválido para <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; no momento, apenas -1 é compatível.  
		///   DV_E_FORMATETC 
		///   Um valor inválido para o parâmetro <paramref name="pFormatetc" />.  
		///   DV_E_TYMED 
		///   Um valor <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> inválido.  
		///   DV_E_DVASPECT 
		///   Um valor <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> inválido.  
		///   OLE_E_NOTRUNNING 
		///   O aplicativo não está em execução.</returns>
		// Token: 0x060005BC RID: 1468 RVA: 0x0001B0B8 File Offset: 0x0001A4B8
		[SecurityCritical]
		int IDataObject.QueryGetData(ref FORMATETC formatetc)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				return ((DataObject.OleConverter)this._innerData).OleDataObject.QueryGetData(ref formatetc);
			}
			if (formatetc.dwAspect != DVASPECT.DVASPECT_CONTENT)
			{
				return -2147221397;
			}
			if (!this.GetTymedUseable(formatetc.tymed))
			{
				return -2147221399;
			}
			if (formatetc.cfFormat == 0)
			{
				return 1;
			}
			if (!this.GetDataPresent(DataFormats.GetDataFormat((int)formatetc.cfFormat).Name))
			{
				return -2147221404;
			}
			return 0;
		}

		/// <summary>Transfere dados ao objeto que implementa este método. Este método é chamado por um objeto que contém uma fonte de dados.</summary>
		/// <param name="pFormatetcIn">Uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passada por referência, que define o formato usado pelo objeto de dados ao interpretar os dados contidos no meio de armazenamento.</param>
		/// <param name="pmedium">Uma estrutura <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passada por referência, que define o meio de armazenamento no qual os dados estão sendo analisados.</param>
		/// <param name="fRelease">
		///   <see langword="true" /> para especificar que o objeto de dados chamado, que implementa <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, tem o meio de armazenamento após o retorno da chamada. Isso significa que o objeto de dados deverá liberar o meio depois de ter sido usado por meio da chamada à função <see langword="ReleaseStgMedium" />. <see langword="false" /> para especificar que o chamador retém a propriedade do meio de armazenamento e o objeto de dados chamado usa o meio de armazenamento apenas pela duração da chamada.</param>
		// Token: 0x060005BD RID: 1469 RVA: 0x0001B138 File Offset: 0x0001A538
		[SecurityCritical]
		void IDataObject.SetData(ref FORMATETC pFormatetcIn, ref STGMEDIUM pmedium, bool fRelease)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (this._innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this._innerData).OleDataObject.SetData(ref pFormatetcIn, ref pmedium, fRelease);
				return;
			}
			Marshal.ThrowExceptionForHR(-2147467263);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DataObject.Copying" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x060005BE RID: 1470 RVA: 0x0001B17C File Offset: 0x0001A57C
		public static void AddCopyingHandler(DependencyObject element, DataObjectCopyingEventHandler handler)
		{
			UIElement.AddHandler(element, DataObject.CopyingEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DataObject.Copying" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x060005BF RID: 1471 RVA: 0x0001B198 File Offset: 0x0001A598
		public static void RemoveCopyingHandler(DependencyObject element, DataObjectCopyingEventHandler handler)
		{
			UIElement.RemoveHandler(element, DataObject.CopyingEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DataObject.Pasting" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x060005C0 RID: 1472 RVA: 0x0001B1B4 File Offset: 0x0001A5B4
		public static void AddPastingHandler(DependencyObject element, DataObjectPastingEventHandler handler)
		{
			UIElement.AddHandler(element, DataObject.PastingEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DataObject.Pasting" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x060005C1 RID: 1473 RVA: 0x0001B1D0 File Offset: 0x0001A5D0
		public static void RemovePastingHandler(DependencyObject element, DataObjectPastingEventHandler handler)
		{
			UIElement.RemoveHandler(element, DataObject.PastingEvent, handler);
		}

		/// <summary>Adiciona um manipulador de eventos <see cref="E:System.Windows.DataObject.SettingData" /> a um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) ao qual o manipulador de eventos será adicionado.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser adicionado.</param>
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001B1EC File Offset: 0x0001A5EC
		public static void AddSettingDataHandler(DependencyObject element, DataObjectSettingDataEventHandler handler)
		{
			UIElement.AddHandler(element, DataObject.SettingDataEvent, handler);
		}

		/// <summary>Remove um manipulador de eventos <see cref="E:System.Windows.DataObject.SettingData" /> de um objeto de dependência especificado.</summary>
		/// <param name="element">O objeto de dependência (um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />) do qual o manipulador de eventos será removido.</param>
		/// <param name="handler">Um delegado que faz referência ao método de manipulador a ser removido.</param>
		// Token: 0x060005C3 RID: 1475 RVA: 0x0001B208 File Offset: 0x0001A608
		public static void RemoveSettingDataHandler(DependencyObject element, DataObjectSettingDataEventHandler handler)
		{
			UIElement.RemoveHandler(element, DataObject.SettingDataEvent, handler);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001B224 File Offset: 0x0001A624
		[SecurityCritical]
		internal static IntPtr Win32GlobalAlloc(int flags, IntPtr bytes)
		{
			IntPtr intPtr = UnsafeNativeMethods.GlobalAlloc(flags, bytes);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return intPtr;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001B254 File Offset: 0x0001A654
		[SecurityCritical]
		private static int Win32CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, ref IStream istream)
		{
			int num = UnsafeNativeMethods.CreateStreamOnHGlobal(hGlobal, fDeleteOnRelease, ref istream);
			if (NativeMethods.Failed(num))
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return num;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001B27C File Offset: 0x0001A67C
		[SecurityCritical]
		internal static void Win32GlobalFree(HandleRef handle)
		{
			IntPtr value = UnsafeNativeMethods.GlobalFree(handle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (value != IntPtr.Zero)
			{
				throw new Win32Exception(lastWin32Error);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001B2AC File Offset: 0x0001A6AC
		[SecurityCritical]
		internal static IntPtr Win32GlobalReAlloc(HandleRef handle, IntPtr bytes, int flags)
		{
			IntPtr intPtr = UnsafeNativeMethods.GlobalReAlloc(handle, bytes, flags);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return intPtr;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001B2E0 File Offset: 0x0001A6E0
		[SecurityCritical]
		internal static IntPtr Win32GlobalLock(HandleRef handle)
		{
			IntPtr intPtr = UnsafeNativeMethods.GlobalLock(handle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return intPtr;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001B310 File Offset: 0x0001A710
		[SecurityCritical]
		internal static void Win32GlobalUnlock(HandleRef handle)
		{
			bool flag = UnsafeNativeMethods.GlobalUnlock(handle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 0)
			{
				throw new Win32Exception(lastWin32Error);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001B338 File Offset: 0x0001A738
		[SecurityCritical]
		internal static IntPtr Win32GlobalSize(HandleRef handle)
		{
			IntPtr intPtr = UnsafeNativeMethods.GlobalSize(handle);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return intPtr;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001B368 File Offset: 0x0001A768
		[SecurityCritical]
		internal static IntPtr Win32SelectObject(HandleRef handleDC, IntPtr handleObject)
		{
			IntPtr intPtr = UnsafeNativeMethods.SelectObject(handleDC, handleObject);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			return intPtr;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B394 File Offset: 0x0001A794
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void Win32DeleteObject(HandleRef handleDC)
		{
			SecurityHelper.DemandUnmanagedCode();
			UnsafeNativeMethods.DeleteObject(handleDC);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001B3AC File Offset: 0x0001A7AC
		[SecurityCritical]
		internal static IntPtr Win32GetDC(HandleRef handleDC)
		{
			return UnsafeNativeMethods.GetDC(handleDC);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001B3C4 File Offset: 0x0001A7C4
		internal static IntPtr Win32CreateCompatibleDC(HandleRef handleDC)
		{
			return UnsafeNativeMethods.CreateCompatibleDC(handleDC);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001B3DC File Offset: 0x0001A7DC
		[SecurityCritical]
		internal static IntPtr Win32CreateCompatibleBitmap(HandleRef handleDC, int width, int height)
		{
			return UnsafeNativeMethods.CreateCompatibleBitmap(handleDC, width, height);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001B3F4 File Offset: 0x0001A7F4
		internal static void Win32DeleteDC(HandleRef handleDC)
		{
			UnsafeNativeMethods.DeleteDC(handleDC);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001B408 File Offset: 0x0001A808
		[SecurityCritical]
		private static void Win32ReleaseDC(HandleRef handleHWND, HandleRef handleDC)
		{
			UnsafeNativeMethods.ReleaseDC(handleHWND, handleDC);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001B420 File Offset: 0x0001A820
		[SecurityCritical]
		internal static void Win32BitBlt(HandleRef handledestination, int width, int height, HandleRef handleSource, int operationCode)
		{
			if (!UnsafeNativeMethods.BitBlt(handledestination, 0, 0, width, height, handleSource, 0, 0, operationCode))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B448 File Offset: 0x0001A848
		[SecurityCritical]
		internal static int Win32WideCharToMultiByte(string wideString, int wideChars, byte[] bytes, int byteCount)
		{
			int num = UnsafeNativeMethods.WideCharToMultiByte(0, 0, wideString, wideChars, bytes, byteCount, IntPtr.Zero, IntPtr.Zero);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (num == 0)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return num;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B47C File Offset: 0x0001A87C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static string[] GetMappedFormats(string format)
		{
			if (format == null)
			{
				return null;
			}
			if (DataObject.IsFormatEqual(format, DataFormats.Text) || DataObject.IsFormatEqual(format, DataFormats.UnicodeText) || DataObject.IsFormatEqual(format, DataFormats.StringFormat))
			{
				string[] result;
				if (SecurityHelper.CheckUnmanagedCodePermission())
				{
					result = new string[]
					{
						DataFormats.Text,
						DataFormats.UnicodeText,
						DataFormats.StringFormat
					};
				}
				else
				{
					result = new string[]
					{
						DataFormats.Text,
						DataFormats.UnicodeText
					};
				}
				return result;
			}
			if (DataObject.IsFormatEqual(format, DataFormats.FileDrop) || DataObject.IsFormatEqual(format, DataFormats.FileName) || DataObject.IsFormatEqual(format, DataFormats.FileNameW))
			{
				return new string[]
				{
					DataFormats.FileDrop,
					DataFormats.FileNameW,
					DataFormats.FileName
				};
			}
			if (DataObject.IsFormatEqual(format, DataFormats.Bitmap) || DataObject.IsFormatEqual(format, "System.Windows.Media.Imaging.BitmapSource") || DataObject.IsFormatEqual(format, "System.Drawing.Bitmap"))
			{
				return new string[]
				{
					DataFormats.Bitmap,
					"System.Drawing.Bitmap",
					"System.Windows.Media.Imaging.BitmapSource"
				};
			}
			if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile) || DataObject.IsFormatEqual(format, "System.Drawing.Imaging.Metafile"))
			{
				return new string[]
				{
					DataFormats.EnhancedMetafile,
					"System.Drawing.Imaging.Metafile"
				};
			}
			return new string[]
			{
				format
			};
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001B5C0 File Offset: 0x0001A9C0
		[FriendAccessAllowed]
		[SecurityCritical]
		internal void CriticalSetData(string format, object data, bool autoConvert)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._innerData.SetData(format, data, autoConvert);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B5EC File Offset: 0x0001A9EC
		[SecurityCritical]
		private int OleGetDataUnrestricted(ref FORMATETC formatetc, ref STGMEDIUM medium, bool doNotReallocate)
		{
			if (this._innerData is DataObject.OleConverter)
			{
				((DataObject.OleConverter)this._innerData).OleDataObject.GetDataHere(ref formatetc, ref medium);
				return 0;
			}
			return this.GetDataIntoOleStructs(ref formatetc, ref medium, doNotReallocate);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001B628 File Offset: 0x0001AA28
		private static string[] GetDistinctStrings(string[] formats)
		{
			ArrayList arrayList = new ArrayList();
			foreach (string text in formats)
			{
				if (!arrayList.Contains(text))
				{
					arrayList.Add(text);
				}
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B674 File Offset: 0x0001AA74
		private bool GetTymedUseable(TYMED tymed)
		{
			for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
			{
				if ((tymed & DataObject.ALLOWED_TYMEDS[i]) != TYMED.TYMED_NULL)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001B6A4 File Offset: 0x0001AAA4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private IntPtr GetCompatibleBitmap(object data)
		{
			SecurityHelper.DemandUnmanagedCode();
			int width;
			int height;
			IntPtr hbitmap = SystemDrawingHelper.GetHBitmap(data, out width, out height);
			if (hbitmap == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr;
			try
			{
				IntPtr handle = DataObject.Win32GetDC(new HandleRef(this, IntPtr.Zero));
				IntPtr handle2 = DataObject.Win32CreateCompatibleDC(new HandleRef(this, handle));
				IntPtr handleObject = DataObject.Win32SelectObject(new HandleRef(this, handle2), hbitmap);
				IntPtr handle3 = DataObject.Win32CreateCompatibleDC(new HandleRef(this, handle));
				intPtr = DataObject.Win32CreateCompatibleBitmap(new HandleRef(this, handle), width, height);
				IntPtr handleObject2 = DataObject.Win32SelectObject(new HandleRef(this, handle3), intPtr);
				try
				{
					DataObject.Win32BitBlt(new HandleRef(this, handle3), width, height, new HandleRef(null, handle2), 13369376);
				}
				finally
				{
					DataObject.Win32SelectObject(new HandleRef(this, handle2), handleObject);
					DataObject.Win32SelectObject(new HandleRef(this, handle3), handleObject2);
					DataObject.Win32DeleteDC(new HandleRef(this, handle2));
					DataObject.Win32DeleteDC(new HandleRef(this, handle3));
					DataObject.Win32ReleaseDC(new HandleRef(this, IntPtr.Zero), new HandleRef(this, handle));
				}
			}
			finally
			{
				DataObject.Win32DeleteObject(new HandleRef(this, hbitmap));
			}
			return intPtr;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001B7E4 File Offset: 0x0001ABE4
		[SecurityCritical]
		private IntPtr GetEnhancedMetafileHandle(string format, object data)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile))
			{
				if (SystemDrawingHelper.IsMetafile(data))
				{
					intPtr = SystemDrawingHelper.GetHandleFromMetafile(data);
				}
				else if (data is MemoryStream)
				{
					MemoryStream memoryStream = data as MemoryStream;
					if (memoryStream != null)
					{
						byte[] buffer = memoryStream.GetBuffer();
						if (buffer != null && buffer.Length != 0)
						{
							intPtr = NativeMethods.SetEnhMetaFileBits((uint)buffer.Length, buffer);
							int lastWin32Error = Marshal.GetLastWin32Error();
							if (intPtr == IntPtr.Zero)
							{
								throw new Win32Exception(lastWin32Error);
							}
						}
					}
				}
			}
			return intPtr;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001B85C File Offset: 0x0001AC5C
		[SecurityCritical]
		private int GetDataIntoOleStructs(ref FORMATETC formatetc, ref STGMEDIUM medium, bool doNotReallocate)
		{
			int result = -2147221399;
			if (this.GetTymedUseable(formatetc.tymed) && this.GetTymedUseable(medium.tymed))
			{
				string name = DataFormats.GetDataFormat((int)formatetc.cfFormat).Name;
				result = -2147221404;
				if (this.GetDataPresent(name))
				{
					object data = this.GetData(name);
					result = -2147221399;
					if ((formatetc.tymed & TYMED.TYMED_HGLOBAL) != TYMED.TYMED_NULL)
					{
						result = this.GetDataIntoOleStructsByTypeMedimHGlobal(name, data, ref medium, doNotReallocate);
					}
					else if ((formatetc.tymed & TYMED.TYMED_GDI) != TYMED.TYMED_NULL)
					{
						result = this.GetDataIntoOleStructsByTypeMediumGDI(name, data, ref medium);
					}
					else if ((formatetc.tymed & TYMED.TYMED_ENHMF) != TYMED.TYMED_NULL)
					{
						result = this.GetDataIntoOleStructsByTypeMediumEnhancedMetaFile(name, data, ref medium);
					}
					else if ((formatetc.tymed & TYMED.TYMED_ISTREAM) != TYMED.TYMED_NULL)
					{
						result = this.GetDataIntoOleStructsByTypeMedimIStream(name, data, ref medium);
					}
				}
			}
			return result;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001B91C File Offset: 0x0001AD1C
		[SecurityCritical]
		private int GetDataIntoOleStructsByTypeMedimHGlobal(string format, object data, ref STGMEDIUM medium, bool doNotReallocate)
		{
			int num;
			if (data is Stream)
			{
				num = this.SaveStreamToHandle(medium.unionmember, (Stream)data, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.Html) || DataObject.IsFormatEqual(format, DataFormats.Xaml))
			{
				num = this.SaveStringToHandleAsUtf8(medium.unionmember, data.ToString(), doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.Text) || DataObject.IsFormatEqual(format, DataFormats.Rtf) || DataObject.IsFormatEqual(format, DataFormats.OemText) || DataObject.IsFormatEqual(format, DataFormats.CommaSeparatedValue))
			{
				num = this.SaveStringToHandle(medium.unionmember, data.ToString(), false, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.UnicodeText) || DataObject.IsFormatEqual(format, DataFormats.ApplicationTrust))
			{
				num = this.SaveStringToHandle(medium.unionmember, data.ToString(), true, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.FileDrop))
			{
				num = this.SaveFileListToHandle(medium.unionmember, (string[])data, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.FileName))
			{
				string[] array = (string[])data;
				num = this.SaveStringToHandle(medium.unionmember, array[0], false, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.FileNameW))
			{
				string[] array2 = (string[])data;
				num = this.SaveStringToHandle(medium.unionmember, array2[0], true, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.Dib) && SystemDrawingHelper.IsImage(data))
			{
				num = -2147221399;
			}
			else if (DataObject.IsFormatEqual(format, typeof(BitmapSource).FullName))
			{
				num = this.SaveSystemBitmapSourceToHandle(medium.unionmember, data, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, "System.Drawing.Bitmap"))
			{
				num = this.SaveSystemDrawingBitmapToHandle(medium.unionmember, data, doNotReallocate);
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile) || SystemDrawingHelper.IsMetafile(data))
			{
				num = -2147221399;
			}
			else if (DataObject.IsFormatEqual(format, DataFormats.Serializable) || data is ISerializable || (data != null && data.GetType().IsSerializable))
			{
				num = this.SaveObjectToHandle(medium.unionmember, data, doNotReallocate);
			}
			else
			{
				num = -2147221399;
			}
			if (num == 0)
			{
				medium.tymed = TYMED.TYMED_HGLOBAL;
			}
			return num;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001BB58 File Offset: 0x0001AF58
		[SecurityCritical]
		private int GetDataIntoOleStructsByTypeMedimIStream(string format, object data, ref STGMEDIUM medium)
		{
			IStream stream = (IStream)Marshal.GetObjectForIUnknown(medium.unionmember);
			if (stream == null)
			{
				return -2147024809;
			}
			int num = -2147467259;
			try
			{
				if (format == StrokeCollection.InkSerializedFormat)
				{
					Stream stream2 = data as Stream;
					if (stream2 != null)
					{
						IntPtr intPtr = (IntPtr)stream2.Length;
						byte[] array = new byte[NativeMethods.IntPtrToInt32(intPtr)];
						stream2.Position = 0L;
						stream2.Read(array, 0, NativeMethods.IntPtrToInt32(intPtr));
						stream.Write(array, NativeMethods.IntPtrToInt32(intPtr), IntPtr.Zero);
						num = 0;
					}
				}
			}
			finally
			{
				Marshal.ReleaseComObject(stream);
			}
			if (NativeMethods.Succeeded(num))
			{
				medium.tymed = TYMED.TYMED_ISTREAM;
			}
			return num;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001BC18 File Offset: 0x0001B018
		[SecurityCritical]
		private int GetDataIntoOleStructsByTypeMediumGDI(string format, object data, ref STGMEDIUM medium)
		{
			int result = -2147467259;
			if (DataObject.IsFormatEqual(format, DataFormats.Bitmap) && (SystemDrawingHelper.IsBitmap(data) || DataObject.IsDataSystemBitmapSource(data)))
			{
				IntPtr compatibleBitmap = this.GetCompatibleBitmap(data);
				if (compatibleBitmap != IntPtr.Zero)
				{
					medium.tymed = TYMED.TYMED_GDI;
					medium.unionmember = compatibleBitmap;
					result = 0;
				}
			}
			else
			{
				result = -2147221399;
			}
			return result;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001BC78 File Offset: 0x0001B078
		[SecurityCritical]
		private int GetDataIntoOleStructsByTypeMediumEnhancedMetaFile(string format, object data, ref STGMEDIUM medium)
		{
			int result = -2147467259;
			if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile))
			{
				IntPtr enhancedMetafileHandle = this.GetEnhancedMetafileHandle(format, data);
				if (enhancedMetafileHandle != IntPtr.Zero)
				{
					medium.tymed = TYMED.TYMED_ENHMF;
					medium.unionmember = enhancedMetafileHandle;
					result = 0;
				}
			}
			else
			{
				result = -2147221399;
			}
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001BCC8 File Offset: 0x0001B0C8
		[SecurityCritical]
		private int SaveObjectToHandle(IntPtr handle, object data, bool doNotReallocate)
		{
			Stream stream2;
			Stream stream = stream2 = new MemoryStream();
			int result;
			try
			{
				BinaryWriter binaryWriter2;
				BinaryWriter binaryWriter = binaryWriter2 = new BinaryWriter(stream);
				try
				{
					binaryWriter.Write(DataObject._serializedObjectID);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, data);
					result = this.SaveStreamToHandle(handle, stream, doNotReallocate);
				}
				finally
				{
					if (binaryWriter2 != null)
					{
						((IDisposable)binaryWriter2).Dispose();
					}
				}
			}
			finally
			{
				if (stream2 != null)
				{
					((IDisposable)stream2).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001BD5C File Offset: 0x0001B15C
		[SecurityCritical]
		private int SaveStreamToHandle(IntPtr handle, Stream stream, bool doNotReallocate)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			IntPtr intPtr = (IntPtr)stream.Length;
			int num = this.EnsureMemoryCapacity(ref handle, NativeMethods.IntPtrToInt32(intPtr), doNotReallocate);
			if (NativeMethods.Failed(num))
			{
				return num;
			}
			IntPtr destination = DataObject.Win32GlobalLock(new HandleRef(this, handle));
			try
			{
				byte[] array = new byte[NativeMethods.IntPtrToInt32(intPtr)];
				stream.Position = 0L;
				stream.Read(array, 0, NativeMethods.IntPtrToInt32(intPtr));
				Marshal.Copy(array, 0, destination, NativeMethods.IntPtrToInt32(intPtr));
			}
			finally
			{
				DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
			}
			return 0;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BE0C File Offset: 0x0001B20C
		[SecurityCritical]
		private int SaveSystemBitmapSourceToHandle(IntPtr handle, object data, bool doNotReallocate)
		{
			BitmapSource bitmapSource = null;
			if (DataObject.IsDataSystemBitmapSource(data))
			{
				bitmapSource = (BitmapSource)data;
			}
			else if (SystemDrawingHelper.IsBitmap(data))
			{
				IntPtr hbitmapFromBitmap = SystemDrawingHelper.GetHBitmapFromBitmap(data);
				bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmapFromBitmap, IntPtr.Zero, Int32Rect.Empty, null);
				DataObject.Win32DeleteObject(new HandleRef(this, hbitmapFromBitmap));
			}
			Invariant.Assert(bitmapSource != null);
			BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
			bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
			Stream stream = new MemoryStream();
			bitmapEncoder.Save(stream);
			return this.SaveStreamToHandle(handle, stream, doNotReallocate);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BE90 File Offset: 0x0001B290
		[SecurityCritical]
		private int SaveSystemDrawingBitmapToHandle(IntPtr handle, object data, bool doNotReallocate)
		{
			object bitmap = SystemDrawingHelper.GetBitmap(data);
			Invariant.Assert(bitmap != null);
			return this.SaveObjectToHandle(handle, bitmap, doNotReallocate);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001BEB8 File Offset: 0x0001B2B8
		[SecurityCritical]
		private int SaveFileListToHandle(IntPtr handle, string[] files, bool doNotReallocate)
		{
			if (files == null || files.Length < 1)
			{
				return 0;
			}
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			if (Marshal.SystemDefaultCharSize == 1)
			{
				Invariant.Assert(false, "Expected the system default char size to be 2 for Unicode systems.");
				return -2147024809;
			}
			IntPtr intPtr = IntPtr.Zero;
			int num = 20;
			int num2 = num;
			for (int i = 0; i < files.Length; i++)
			{
				num2 += (files[i].Length + 1) * 2;
			}
			num2 += 2;
			int num3 = this.EnsureMemoryCapacity(ref handle, num2, doNotReallocate);
			if (NativeMethods.Failed(num3))
			{
				return num3;
			}
			IntPtr intPtr2 = DataObject.Win32GlobalLock(new HandleRef(this, handle));
			try
			{
				intPtr = intPtr2;
				int[] array = new int[5];
				array[0] = num;
				int[] array2 = array;
				array2[4] = -1;
				Marshal.Copy(array2, 0, intPtr, array2.Length);
				intPtr = (IntPtr)((long)intPtr + (long)num);
				for (int j = 0; j < files.Length; j++)
				{
					UnsafeNativeMethods.CopyMemoryW(intPtr, files[j], files[j].Length * 2);
					intPtr = (IntPtr)((long)intPtr + (long)(files[j].Length * 2));
					Marshal.Copy(new char[1], 0, intPtr, 1);
					intPtr = (IntPtr)((long)intPtr + 2L);
				}
				Marshal.Copy(new char[1], 0, intPtr, 1);
			}
			finally
			{
				DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
			}
			return 0;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C014 File Offset: 0x0001B414
		[SecurityCritical]
		private int SaveStringToHandle(IntPtr handle, string str, bool unicode, bool doNotReallocate)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			if (unicode)
			{
				int minimumByteCount = str.Length * 2 + 2;
				int num = this.EnsureMemoryCapacity(ref handle, minimumByteCount, doNotReallocate);
				if (NativeMethods.Failed(num))
				{
					return num;
				}
				IntPtr intPtr = DataObject.Win32GlobalLock(new HandleRef(this, handle));
				try
				{
					char[] array = str.ToCharArray(0, str.Length);
					UnsafeNativeMethods.CopyMemoryW(intPtr, array, array.Length * 2);
					Marshal.Copy(new char[1], 0, (IntPtr)((long)intPtr + (long)array.Length * 2L), 1);
					return 0;
				}
				finally
				{
					DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
				}
			}
			int num2;
			if (str.Length > 0)
			{
				num2 = DataObject.Win32WideCharToMultiByte(str, str.Length, null, 0);
			}
			else
			{
				num2 = 0;
			}
			byte[] array2 = new byte[num2];
			if (num2 > 0)
			{
				DataObject.Win32WideCharToMultiByte(str, str.Length, array2, array2.Length);
			}
			int num3 = this.EnsureMemoryCapacity(ref handle, num2 + 1, doNotReallocate);
			if (NativeMethods.Failed(num3))
			{
				return num3;
			}
			IntPtr intPtr2 = DataObject.Win32GlobalLock(new HandleRef(this, handle));
			try
			{
				UnsafeNativeMethods.CopyMemory(intPtr2, array2, num2);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr2 + (long)num2), 1);
			}
			finally
			{
				DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
			}
			return 0;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C184 File Offset: 0x0001B584
		[SecurityCritical]
		private int SaveStringToHandleAsUtf8(IntPtr handle, string str, bool doNotReallocate)
		{
			if (handle == IntPtr.Zero)
			{
				return -2147024809;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			int byteCount = utf8Encoding.GetByteCount(str);
			byte[] bytes = utf8Encoding.GetBytes(str);
			int num = this.EnsureMemoryCapacity(ref handle, byteCount + 1, doNotReallocate);
			if (NativeMethods.Failed(num))
			{
				return num;
			}
			IntPtr intPtr = DataObject.Win32GlobalLock(new HandleRef(this, handle));
			try
			{
				UnsafeNativeMethods.CopyMemory(intPtr, bytes, byteCount);
				Marshal.Copy(new byte[1], 0, (IntPtr)((long)intPtr + (long)byteCount), 1);
			}
			finally
			{
				DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
			}
			return 0;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C230 File Offset: 0x0001B630
		private static bool IsDataSystemBitmapSource(object data)
		{
			return data is BitmapSource;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C248 File Offset: 0x0001B648
		private static bool IsFormatAndDataSerializable(string format, object data)
		{
			return DataObject.IsFormatNotSupportedInPartialTrust(format) && (DataObject.IsFormatEqual(format, DataFormats.Serializable) || data is ISerializable || (data != null && data.GetType().IsSerializable));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C288 File Offset: 0x0001B688
		private static bool IsFormatNotSupportedInPartialTrust(string format)
		{
			return !DataObject.IsFormatEqual(format, DataFormats.Text) && !DataObject.IsFormatEqual(format, DataFormats.OemText) && !DataObject.IsFormatEqual(format, DataFormats.UnicodeText) && !DataObject.IsFormatEqual(format, DataFormats.CommaSeparatedValue) && !DataObject.IsFormatEqual(format, DataFormats.Xaml) && !DataObject.IsFormatEqual(format, DataFormats.ApplicationTrust);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C2E8 File Offset: 0x0001B6E8
		private static bool IsFormatEqual(string format1, string format2)
		{
			return string.CompareOrdinal(format1, format2) == 0;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C300 File Offset: 0x0001B700
		[SecurityCritical]
		private int EnsureMemoryCapacity(ref IntPtr handle, int minimumByteCount, bool doNotReallocate)
		{
			int result = 0;
			if (doNotReallocate)
			{
				int num = NativeMethods.IntPtrToInt32(DataObject.Win32GlobalSize(new HandleRef(this, handle)));
				if (num < minimumByteCount)
				{
					handle = IntPtr.Zero;
					result = -2147286928;
				}
			}
			else
			{
				handle = DataObject.Win32GlobalReAlloc(new HandleRef(this, handle), (IntPtr)minimumByteCount, 8258);
				if (handle == IntPtr.Zero)
				{
					result = -2147024882;
				}
			}
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001C368 File Offset: 0x0001B768
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static object EnsureBitmapDataFromFormat(string format, bool autoConvert, object data)
		{
			object result = data;
			if (DataObject.IsDataSystemBitmapSource(data) && DataObject.IsFormatEqual(format, "System.Drawing.Bitmap"))
			{
				if (autoConvert)
				{
					SecurityHelper.DemandUnmanagedCode();
					result = SystemDrawingHelper.GetBitmap(data);
				}
				else
				{
					result = null;
				}
			}
			else if (SystemDrawingHelper.IsBitmap(data) && (DataObject.IsFormatEqual(format, DataFormats.Bitmap) || DataObject.IsFormatEqual(format, "System.Windows.Media.Imaging.BitmapSource")))
			{
				if (autoConvert)
				{
					IntPtr hbitmapFromBitmap = SystemDrawingHelper.GetHBitmapFromBitmap(data);
					result = Imaging.CreateBitmapSourceFromHBitmap(hbitmapFromBitmap, IntPtr.Zero, Int32Rect.Empty, null);
					DataObject.Win32DeleteObject(new HandleRef(null, hbitmapFromBitmap));
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001C3F0 File Offset: 0x0001B7F0
		// Note: this type is marked as 'beforefieldinit'.
		static DataObject()
		{
			TYMED[] array = new TYMED[5];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails><PresentationCoreCSharp_netmodule>.962C732E4EFE979C16B0229A7A7F6E81EC12A8FD55D25E4E7567E185D4A70D20).FieldHandle);
			DataObject.ALLOWED_TYMEDS = array;
			DataObject._serializedObjectID = new Guid(4255033238U, 15123, 17264, 166, 121, 86, 16, 107, 178, 136, 251).ToByteArray();
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.DataObject.Copying" /> anexado.</summary>
		// Token: 0x04000551 RID: 1361
		public static readonly RoutedEvent CopyingEvent = EventManager.RegisterRoutedEvent("Copying", RoutingStrategy.Bubble, typeof(DataObjectCopyingEventHandler), typeof(DataObject));

		/// <summary>Identifica o evento <see cref="E:System.Windows.DataObject.Pasting" /> anexado.</summary>
		// Token: 0x04000552 RID: 1362
		public static readonly RoutedEvent PastingEvent = EventManager.RegisterRoutedEvent("Pasting", RoutingStrategy.Bubble, typeof(DataObjectPastingEventHandler), typeof(DataObject));

		/// <summary>Identifica o evento <see cref="E:System.Windows.DataObject.SettingData" /> anexado.</summary>
		// Token: 0x04000553 RID: 1363
		public static readonly RoutedEvent SettingDataEvent = EventManager.RegisterRoutedEvent("SettingData", RoutingStrategy.Bubble, typeof(DataObjectSettingDataEventHandler), typeof(DataObject));

		// Token: 0x04000554 RID: 1364
		private const string SystemDrawingBitmapFormat = "System.Drawing.Bitmap";

		// Token: 0x04000555 RID: 1365
		private const string SystemBitmapSourceFormat = "System.Windows.Media.Imaging.BitmapSource";

		// Token: 0x04000556 RID: 1366
		private const string SystemDrawingImagingMetafileFormat = "System.Drawing.Imaging.Metafile";

		// Token: 0x04000557 RID: 1367
		private const int DV_E_FORMATETC = -2147221404;

		// Token: 0x04000558 RID: 1368
		private const int DV_E_LINDEX = -2147221400;

		// Token: 0x04000559 RID: 1369
		private const int DV_E_TYMED = -2147221399;

		// Token: 0x0400055A RID: 1370
		private const int DV_E_DVASPECT = -2147221397;

		// Token: 0x0400055B RID: 1371
		private const int OLE_E_NOTRUNNING = -2147221499;

		// Token: 0x0400055C RID: 1372
		private const int OLE_E_ADVISENOTSUPPORTED = -2147221501;

		// Token: 0x0400055D RID: 1373
		private const int DATA_S_SAMEFORMATETC = 262448;

		// Token: 0x0400055E RID: 1374
		private const int STG_E_MEDIUMFULL = -2147286928;

		// Token: 0x0400055F RID: 1375
		private const int FILEDROPBASESIZE = 20;

		// Token: 0x04000560 RID: 1376
		private static readonly TYMED[] ALLOWED_TYMEDS;

		// Token: 0x04000561 RID: 1377
		private IDataObject _innerData;

		// Token: 0x04000562 RID: 1378
		private static readonly byte[] _serializedObjectID;

		// Token: 0x020007F2 RID: 2034
		private class FormatEnumerator : IEnumFORMATETC
		{
			// Token: 0x06005584 RID: 21892 RVA: 0x0015FD40 File Offset: 0x0015F140
			internal FormatEnumerator(DataObject dataObject)
			{
				string[] formats = dataObject.GetFormats();
				this._formats = new FORMATETC[(formats == null) ? 0 : formats.Length];
				if (formats != null)
				{
					for (int i = 0; i < formats.Length; i++)
					{
						string text = formats[i];
						FORMATETC formatetc = default(FORMATETC);
						formatetc.cfFormat = (short)DataFormats.GetDataFormat(text).Id;
						formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
						formatetc.ptd = IntPtr.Zero;
						formatetc.lindex = -1;
						if (DataObject.IsFormatEqual(text, DataFormats.Bitmap))
						{
							formatetc.tymed = TYMED.TYMED_GDI;
						}
						else if (DataObject.IsFormatEqual(text, DataFormats.EnhancedMetafile))
						{
							formatetc.tymed = TYMED.TYMED_ENHMF;
						}
						else
						{
							formatetc.tymed = TYMED.TYMED_HGLOBAL;
						}
						this._formats[i] = formatetc;
					}
				}
			}

			// Token: 0x06005585 RID: 21893 RVA: 0x0015FE08 File Offset: 0x0015F208
			private FormatEnumerator(DataObject.FormatEnumerator formatEnumerator)
			{
				this._formats = formatEnumerator._formats;
				this._current = formatEnumerator._current;
			}

			// Token: 0x06005586 RID: 21894 RVA: 0x0015FE34 File Offset: 0x0015F234
			public int Next(int celt, FORMATETC[] rgelt, int[] pceltFetched)
			{
				int num = 0;
				if (rgelt == null)
				{
					return -2147024809;
				}
				int num2 = 0;
				while (num2 < celt && this._current < this._formats.Length)
				{
					rgelt[num2] = this._formats[this._current];
					this._current++;
					num++;
					num2++;
				}
				if (pceltFetched != null)
				{
					pceltFetched[0] = num;
				}
				if (num != celt)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06005587 RID: 21895 RVA: 0x0015FEA0 File Offset: 0x0015F2A0
			public int Skip(int celt)
			{
				this._current += Math.Min(celt, int.MaxValue - this._current);
				if (this._current >= this._formats.Length)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06005588 RID: 21896 RVA: 0x0015FEE0 File Offset: 0x0015F2E0
			public int Reset()
			{
				this._current = 0;
				return 0;
			}

			// Token: 0x06005589 RID: 21897 RVA: 0x0015FEF8 File Offset: 0x0015F2F8
			public void Clone(out IEnumFORMATETC ppenum)
			{
				ppenum = new DataObject.FormatEnumerator(this);
			}

			// Token: 0x04002685 RID: 9861
			private readonly FORMATETC[] _formats;

			// Token: 0x04002686 RID: 9862
			private int _current;
		}

		// Token: 0x020007F3 RID: 2035
		private class OleConverter : IDataObject
		{
			// Token: 0x0600558A RID: 21898 RVA: 0x0015FF10 File Offset: 0x0015F310
			public OleConverter(IDataObject data)
			{
				this._innerData = data;
			}

			// Token: 0x0600558B RID: 21899 RVA: 0x0015FF2C File Offset: 0x0015F32C
			public object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x0600558C RID: 21900 RVA: 0x0015FF44 File Offset: 0x0015F344
			public object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x0600558D RID: 21901 RVA: 0x0015FF60 File Offset: 0x0015F360
			public object GetData(string format, bool autoConvert)
			{
				return this.GetData(format, autoConvert, DVASPECT.DVASPECT_CONTENT, -1);
			}

			// Token: 0x0600558E RID: 21902 RVA: 0x0015FF78 File Offset: 0x0015F378
			public bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x0600558F RID: 21903 RVA: 0x0015FF90 File Offset: 0x0015F390
			public bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x06005590 RID: 21904 RVA: 0x0015FFAC File Offset: 0x0015F3AC
			public bool GetDataPresent(string format, bool autoConvert)
			{
				return this.GetDataPresent(format, autoConvert, DVASPECT.DVASPECT_CONTENT, -1);
			}

			// Token: 0x06005591 RID: 21905 RVA: 0x0015FFC4 File Offset: 0x0015F3C4
			public string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x06005592 RID: 21906 RVA: 0x0015FFD8 File Offset: 0x0015F3D8
			public void SetData(object data)
			{
				if (data is ISerializable)
				{
					this.SetData(DataFormats.Serializable, data);
					return;
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x06005593 RID: 21907 RVA: 0x00160008 File Offset: 0x0015F408
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public string[] GetFormats(bool autoConvert)
			{
				SecurityHelper.DemandAllClipboardPermission();
				ArrayList arrayList = new ArrayList();
				IEnumFORMATETC enumFORMATETC = this.EnumFormatEtcInner(DATADIR.DATADIR_GET);
				if (enumFORMATETC != null)
				{
					enumFORMATETC.Reset();
					FORMATETC[] array = new FORMATETC[1];
					int[] array2 = new int[]
					{
						1
					};
					while (array2[0] > 0)
					{
						array2[0] = 0;
						if (enumFORMATETC.Next(1, array, array2) == 0 && array2[0] > 0)
						{
							string name = DataFormats.GetDataFormat((int)array[0].cfFormat).Name;
							if (autoConvert)
							{
								string[] mappedFormats = DataObject.GetMappedFormats(name);
								for (int i = 0; i < mappedFormats.Length; i++)
								{
									arrayList.Add(mappedFormats[i]);
								}
							}
							else
							{
								arrayList.Add(name);
							}
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j].ptd != IntPtr.Zero)
								{
									Marshal.FreeCoTaskMem(array[j].ptd);
								}
							}
						}
					}
				}
				string[] array3 = new string[arrayList.Count];
				arrayList.CopyTo(array3, 0);
				return DataObject.GetDistinctStrings(array3);
			}

			// Token: 0x06005594 RID: 21908 RVA: 0x0016011C File Offset: 0x0015F51C
			public void SetData(string format, object data)
			{
				this.SetData(format, data, true);
			}

			// Token: 0x06005595 RID: 21909 RVA: 0x00160134 File Offset: 0x0015F534
			public void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x06005596 RID: 21910 RVA: 0x00160150 File Offset: 0x0015F550
			public void SetData(string format, object data, bool autoConvert)
			{
				this.SetData(format, data, true, DVASPECT.DVASPECT_CONTENT, 0);
			}

			// Token: 0x17001194 RID: 4500
			// (get) Token: 0x06005597 RID: 21911 RVA: 0x00160168 File Offset: 0x0015F568
			public IDataObject OleDataObject
			{
				[SecuritySafeCritical]
				get
				{
					SecurityHelper.DemandUnmanagedCode();
					return this._innerData;
				}
			}

			// Token: 0x06005598 RID: 21912 RVA: 0x00160180 File Offset: 0x0015F580
			[SecurityCritical]
			[SecurityTreatAsSafe]
			private object GetData(string format, bool autoConvert, DVASPECT aspect, int index)
			{
				SecurityHelper.DemandAllClipboardPermission();
				object obj = this.GetDataFromBoundOleDataObject(format, aspect, index);
				object obj2 = obj;
				if (autoConvert && (obj == null || obj is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!DataObject.IsFormatEqual(format, mappedFormats[i]))
							{
								obj = this.GetDataFromBoundOleDataObject(mappedFormats[i], aspect, index);
								if (obj != null && !(obj is MemoryStream))
								{
									if (DataObject.IsDataSystemBitmapSource(obj) || SystemDrawingHelper.IsBitmap(obj))
									{
										obj = DataObject.EnsureBitmapDataFromFormat(format, autoConvert, obj);
									}
									obj2 = null;
									break;
								}
							}
						}
					}
				}
				if (obj2 != null)
				{
					return obj2;
				}
				return obj;
			}

			// Token: 0x06005599 RID: 21913 RVA: 0x0016020C File Offset: 0x0015F60C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			private bool GetDataPresent(string format, bool autoConvert, DVASPECT aspect, int index)
			{
				SecurityHelper.DemandAllClipboardPermission();
				bool dataPresentInner = this.GetDataPresentInner(format, aspect, index);
				if (!dataPresentInner && autoConvert)
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!DataObject.IsFormatEqual(format, mappedFormats[i]))
							{
								dataPresentInner = this.GetDataPresentInner(mappedFormats[i], aspect, index);
								if (dataPresentInner)
								{
									break;
								}
							}
						}
					}
				}
				return dataPresentInner;
			}

			// Token: 0x0600559A RID: 21914 RVA: 0x00160268 File Offset: 0x0015F668
			private void SetData(string format, object data, bool autoConvert, DVASPECT aspect, int index)
			{
				throw new InvalidOperationException(SR.Get("DataObject_CannotSetDataOnAFozenOLEDataDbject"));
			}

			// Token: 0x0600559B RID: 21915 RVA: 0x00160284 File Offset: 0x0015F684
			[SecurityCritical]
			private object GetDataFromOleIStream(string format, DVASPECT aspect, int index)
			{
				FORMATETC formatetc = default(FORMATETC);
				formatetc.cfFormat = (short)DataFormats.GetDataFormat(format).Id;
				formatetc.dwAspect = aspect;
				formatetc.lindex = index;
				formatetc.tymed = TYMED.TYMED_ISTREAM;
				object result = null;
				if (this.QueryGetDataInner(ref formatetc) == 0)
				{
					STGMEDIUM stgmedium;
					this.GetDataInner(ref formatetc, out stgmedium);
					try
					{
						if (stgmedium.unionmember != IntPtr.Zero && stgmedium.tymed == TYMED.TYMED_ISTREAM)
						{
							UnsafeNativeMethods.IStream stream = (UnsafeNativeMethods.IStream)Marshal.GetObjectForIUnknown(stgmedium.unionmember);
							NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
							stream.Stat(statstg, 0);
							int num = (int)statstg.cbSize;
							IntPtr intPtr = DataObject.Win32GlobalAlloc(8258, (IntPtr)num);
							try
							{
								IntPtr buf = DataObject.Win32GlobalLock(new HandleRef(this, intPtr));
								try
								{
									stream.Seek(0L, 0);
									stream.Read(buf, num);
								}
								finally
								{
									DataObject.Win32GlobalUnlock(new HandleRef(this, intPtr));
								}
								result = this.GetDataFromHGLOBAL(format, intPtr);
							}
							finally
							{
								DataObject.Win32GlobalFree(new HandleRef(this, intPtr));
							}
						}
					}
					finally
					{
						UnsafeNativeMethods.ReleaseStgMedium(ref stgmedium);
					}
				}
				return result;
			}

			// Token: 0x0600559C RID: 21916 RVA: 0x001603DC File Offset: 0x0015F7DC
			[SecurityCritical]
			private object GetDataFromHGLOBAL(string format, IntPtr hglobal)
			{
				object result = null;
				if (hglobal != IntPtr.Zero)
				{
					if (DataObject.IsFormatEqual(format, DataFormats.Html) || DataObject.IsFormatEqual(format, DataFormats.Xaml))
					{
						result = this.ReadStringFromHandleUtf8(hglobal);
					}
					else if (DataObject.IsFormatEqual(format, DataFormats.Text) || DataObject.IsFormatEqual(format, DataFormats.Rtf) || DataObject.IsFormatEqual(format, DataFormats.OemText) || DataObject.IsFormatEqual(format, DataFormats.CommaSeparatedValue))
					{
						result = this.ReadStringFromHandle(hglobal, false);
					}
					else if (DataObject.IsFormatEqual(format, DataFormats.UnicodeText) || DataObject.IsFormatEqual(format, DataFormats.ApplicationTrust))
					{
						result = this.ReadStringFromHandle(hglobal, true);
					}
					else if (DataObject.IsFormatEqual(format, DataFormats.FileDrop))
					{
						SecurityHelper.DemandFilePathDiscoveryWriteRead();
						result = this.ReadFileListFromHandle(hglobal);
					}
					else if (DataObject.IsFormatEqual(format, DataFormats.FileName))
					{
						SecurityHelper.DemandFilePathDiscoveryWriteRead();
						result = new string[]
						{
							this.ReadStringFromHandle(hglobal, false)
						};
					}
					else if (DataObject.IsFormatEqual(format, DataFormats.FileNameW))
					{
						SecurityHelper.DemandFilePathDiscoveryWriteRead();
						result = new string[]
						{
							this.ReadStringFromHandle(hglobal, true)
						};
					}
					else if (DataObject.IsFormatEqual(format, typeof(BitmapSource).FullName))
					{
						result = this.ReadBitmapSourceFromHandle(hglobal);
					}
					else if (!Clipboard.UseLegacyDangerousClipboardDeserializationMode())
					{
						bool restrictDeserialization = DataObject.IsFormatEqual(format, DataFormats.StringFormat) || DataObject.IsFormatEqual(format, DataFormats.Dib) || DataObject.IsFormatEqual(format, DataFormats.Bitmap) || DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile) || DataObject.IsFormatEqual(format, DataFormats.MetafilePicture) || DataObject.IsFormatEqual(format, DataFormats.SymbolicLink) || DataObject.IsFormatEqual(format, DataFormats.Dif) || DataObject.IsFormatEqual(format, DataFormats.Tiff) || DataObject.IsFormatEqual(format, DataFormats.Palette) || DataObject.IsFormatEqual(format, DataFormats.PenData) || DataObject.IsFormatEqual(format, DataFormats.Riff) || DataObject.IsFormatEqual(format, DataFormats.WaveAudio) || DataObject.IsFormatEqual(format, DataFormats.Locale);
						result = this.ReadObjectFromHandle(hglobal, restrictDeserialization);
					}
					else
					{
						result = this.ReadObjectFromHandle(hglobal, false);
					}
				}
				return result;
			}

			// Token: 0x0600559D RID: 21917 RVA: 0x001605F8 File Offset: 0x0015F9F8
			[SecurityCritical]
			private object GetDataFromOleHGLOBAL(string format, DVASPECT aspect, int index)
			{
				FORMATETC formatetc = default(FORMATETC);
				formatetc.cfFormat = (short)DataFormats.GetDataFormat(format).Id;
				formatetc.dwAspect = aspect;
				formatetc.lindex = index;
				formatetc.tymed = TYMED.TYMED_HGLOBAL;
				object result = null;
				if (this.QueryGetDataInner(ref formatetc) == 0)
				{
					STGMEDIUM stgmedium;
					this.GetDataInner(ref formatetc, out stgmedium);
					try
					{
						if (stgmedium.unionmember != IntPtr.Zero && stgmedium.tymed == TYMED.TYMED_HGLOBAL)
						{
							result = this.GetDataFromHGLOBAL(format, stgmedium.unionmember);
						}
					}
					finally
					{
						UnsafeNativeMethods.ReleaseStgMedium(ref stgmedium);
					}
				}
				return result;
			}

			// Token: 0x0600559E RID: 21918 RVA: 0x001606A0 File Offset: 0x0015FAA0
			[SecurityCritical]
			private object GetDataFromOleOther(string format, DVASPECT aspect, int index)
			{
				FORMATETC formatetc = default(FORMATETC);
				TYMED tymed = TYMED.TYMED_NULL;
				if (DataObject.IsFormatEqual(format, DataFormats.Bitmap))
				{
					tymed = TYMED.TYMED_GDI;
				}
				else if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile))
				{
					tymed = TYMED.TYMED_ENHMF;
				}
				if (tymed == TYMED.TYMED_NULL)
				{
					return null;
				}
				formatetc.cfFormat = (short)DataFormats.GetDataFormat(format).Id;
				formatetc.dwAspect = aspect;
				formatetc.lindex = index;
				formatetc.tymed = tymed;
				object result = null;
				if (this.QueryGetDataInner(ref formatetc) == 0)
				{
					STGMEDIUM stgmedium;
					this.GetDataInner(ref formatetc, out stgmedium);
					try
					{
						if (stgmedium.unionmember != IntPtr.Zero)
						{
							if (DataObject.IsFormatEqual(format, DataFormats.Bitmap))
							{
								result = this.GetBitmapSourceFromHbitmap(stgmedium.unionmember);
							}
							else if (DataObject.IsFormatEqual(format, DataFormats.EnhancedMetafile))
							{
								result = SystemDrawingHelper.GetMetafileFromHemf(stgmedium.unionmember);
							}
						}
					}
					finally
					{
						UnsafeNativeMethods.ReleaseStgMedium(ref stgmedium);
					}
				}
				return result;
			}

			// Token: 0x0600559F RID: 21919 RVA: 0x00160790 File Offset: 0x0015FB90
			[SecurityCritical]
			private object GetDataFromBoundOleDataObject(string format, DVASPECT aspect, int index)
			{
				object obj = this.GetDataFromOleOther(format, aspect, index);
				if (obj == null)
				{
					obj = this.GetDataFromOleHGLOBAL(format, aspect, index);
				}
				if (obj == null)
				{
					obj = this.GetDataFromOleIStream(format, aspect, index);
				}
				return obj;
			}

			// Token: 0x060055A0 RID: 21920 RVA: 0x001607C4 File Offset: 0x0015FBC4
			[SecurityCritical]
			private Stream ReadByteStreamFromHandle(IntPtr handle, out bool isSerializedObject)
			{
				IntPtr source = DataObject.Win32GlobalLock(new HandleRef(this, handle));
				Stream result;
				try
				{
					int num = NativeMethods.IntPtrToInt32(DataObject.Win32GlobalSize(new HandleRef(this, handle)));
					byte[] array = new byte[num];
					Marshal.Copy(source, array, 0, num);
					int num2 = 0;
					if (num > DataObject._serializedObjectID.Length)
					{
						isSerializedObject = true;
						for (int i = 0; i < DataObject._serializedObjectID.Length; i++)
						{
							if (DataObject._serializedObjectID[i] != array[i])
							{
								isSerializedObject = false;
								break;
							}
						}
						if (isSerializedObject)
						{
							num2 = DataObject._serializedObjectID.Length;
						}
					}
					else
					{
						isSerializedObject = false;
					}
					result = new MemoryStream(array, num2, array.Length - num2);
				}
				finally
				{
					DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
				}
				return result;
			}

			// Token: 0x060055A1 RID: 21921 RVA: 0x00160880 File Offset: 0x0015FC80
			[SecurityCritical]
			private object ReadObjectFromHandle(IntPtr handle, bool restrictDeserialization)
			{
				object result = null;
				bool flag;
				Stream stream = this.ReadByteStreamFromHandle(handle, out flag);
				if (flag)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					if (restrictDeserialization)
					{
						binaryFormatter.Binder = new DataObject.OleConverter.TypeRestrictingSerializationBinder();
					}
					try
					{
						return binaryFormatter.Deserialize(stream);
					}
					catch (DataObject.OleConverter.RestrictedTypeDeserializationException)
					{
						return null;
					}
				}
				result = stream;
				return result;
			}

			// Token: 0x060055A2 RID: 21922 RVA: 0x001608E0 File Offset: 0x0015FCE0
			[SecurityCritical]
			private BitmapSource ReadBitmapSourceFromHandle(IntPtr handle)
			{
				BitmapSource result = null;
				bool flag;
				Stream stream = this.ReadByteStreamFromHandle(handle, out flag);
				if (stream != null)
				{
					result = BitmapFrame.Create(stream);
				}
				return result;
			}

			// Token: 0x060055A3 RID: 21923 RVA: 0x00160904 File Offset: 0x0015FD04
			[SecurityCritical]
			private string[] ReadFileListFromHandle(IntPtr hdrop)
			{
				string[] array = null;
				StringBuilder stringBuilder = new StringBuilder(260);
				int num = UnsafeNativeMethods.DragQueryFile(new HandleRef(this, hdrop), -1, null, 0);
				if (num > 0)
				{
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						if (UnsafeNativeMethods.DragQueryFile(new HandleRef(this, hdrop), i, stringBuilder, stringBuilder.Capacity) != 0)
						{
							array[i] = stringBuilder.ToString();
						}
					}
				}
				return array;
			}

			// Token: 0x060055A4 RID: 21924 RVA: 0x00160968 File Offset: 0x0015FD68
			[SecurityCritical]
			private unsafe string ReadStringFromHandle(IntPtr handle, bool unicode)
			{
				string result = null;
				IntPtr value = DataObject.Win32GlobalLock(new HandleRef(this, handle));
				try
				{
					if (unicode)
					{
						result = new string((char*)((void*)value));
					}
					else
					{
						result = new string((sbyte*)((void*)value));
					}
				}
				finally
				{
					DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
				}
				return result;
			}

			// Token: 0x060055A5 RID: 21925 RVA: 0x001609D0 File Offset: 0x0015FDD0
			[SecurityCritical]
			private string ReadStringFromHandleUtf8(IntPtr handle)
			{
				string result = null;
				int num = NativeMethods.IntPtrToInt32(DataObject.Win32GlobalSize(new HandleRef(this, handle)));
				IntPtr intPtr = DataObject.Win32GlobalLock(new HandleRef(this, handle));
				try
				{
					int i;
					for (i = 0; i < num; i++)
					{
						byte b = Marshal.ReadByte((IntPtr)((long)intPtr + (long)i));
						if (b == 0)
						{
							break;
						}
					}
					if (i > 0)
					{
						byte[] array = new byte[i];
						Marshal.Copy(intPtr, array, 0, i);
						UTF8Encoding utf8Encoding = new UTF8Encoding();
						result = utf8Encoding.GetString(array, 0, i);
					}
				}
				finally
				{
					DataObject.Win32GlobalUnlock(new HandleRef(this, handle));
				}
				return result;
			}

			// Token: 0x060055A6 RID: 21926 RVA: 0x00160A78 File Offset: 0x0015FE78
			[SecurityCritical]
			private bool GetDataPresentInner(string format, DVASPECT aspect, int index)
			{
				FORMATETC formatetc = default(FORMATETC);
				formatetc.cfFormat = (short)DataFormats.GetDataFormat(format).Id;
				formatetc.dwAspect = aspect;
				formatetc.lindex = index;
				for (int i = 0; i < DataObject.ALLOWED_TYMEDS.Length; i++)
				{
					formatetc.tymed |= DataObject.ALLOWED_TYMEDS[i];
				}
				int num = this.QueryGetDataInner(ref formatetc);
				return num == 0;
			}

			// Token: 0x060055A7 RID: 21927 RVA: 0x00160AE4 File Offset: 0x0015FEE4
			[SecurityCritical]
			private int QueryGetDataInner(ref FORMATETC formatetc)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				int result;
				try
				{
					result = this._innerData.QueryGetData(ref formatetc);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return result;
			}

			// Token: 0x060055A8 RID: 21928 RVA: 0x00160B30 File Offset: 0x0015FF30
			[SecurityCritical]
			private void GetDataInner(ref FORMATETC formatetc, out STGMEDIUM medium)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					this._innerData.GetData(ref formatetc, out medium);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x060055A9 RID: 21929 RVA: 0x00160B7C File Offset: 0x0015FF7C
			[SecurityCritical]
			private IEnumFORMATETC EnumFormatEtcInner(DATADIR dwDirection)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				IEnumFORMATETC result;
				try
				{
					result = this._innerData.EnumFormatEtc(dwDirection);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return result;
			}

			// Token: 0x060055AA RID: 21930 RVA: 0x00160BC8 File Offset: 0x0015FFC8
			[MethodImpl(MethodImplOptions.NoInlining)]
			private object GetBitmapSourceFromHbitmap(IntPtr hbitmap)
			{
				return Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, null);
			}

			// Token: 0x04002687 RID: 9863
			internal IDataObject _innerData;

			// Token: 0x02000A1D RID: 2589
			private class TypeRestrictingSerializationBinder : SerializationBinder
			{
				// Token: 0x06005C10 RID: 23568 RVA: 0x00171F74 File Offset: 0x00171374
				public override Type BindToType(string assemblyName, string typeName)
				{
					throw new DataObject.OleConverter.RestrictedTypeDeserializationException();
				}
			}

			// Token: 0x02000A1E RID: 2590
			private class RestrictedTypeDeserializationException : Exception
			{
			}
		}

		// Token: 0x020007F4 RID: 2036
		private class DataStore : IDataObject
		{
			// Token: 0x060055AC RID: 21932 RVA: 0x00160C08 File Offset: 0x00160008
			public object GetData(string format)
			{
				return this.GetData(format, true);
			}

			// Token: 0x060055AD RID: 21933 RVA: 0x00160C20 File Offset: 0x00160020
			public object GetData(Type format)
			{
				return this.GetData(format.FullName);
			}

			// Token: 0x060055AE RID: 21934 RVA: 0x00160C3C File Offset: 0x0016003C
			public object GetData(string format, bool autoConvert)
			{
				return this.GetData(format, autoConvert, DVASPECT.DVASPECT_CONTENT, -1);
			}

			// Token: 0x060055AF RID: 21935 RVA: 0x00160C54 File Offset: 0x00160054
			public bool GetDataPresent(string format)
			{
				return this.GetDataPresent(format, true);
			}

			// Token: 0x060055B0 RID: 21936 RVA: 0x00160C6C File Offset: 0x0016006C
			public bool GetDataPresent(Type format)
			{
				return this.GetDataPresent(format.FullName);
			}

			// Token: 0x060055B1 RID: 21937 RVA: 0x00160C88 File Offset: 0x00160088
			public bool GetDataPresent(string format, bool autoConvert)
			{
				return this.GetDataPresent(format, autoConvert, DVASPECT.DVASPECT_CONTENT, -1);
			}

			// Token: 0x060055B2 RID: 21938 RVA: 0x00160CA0 File Offset: 0x001600A0
			public string[] GetFormats()
			{
				return this.GetFormats(true);
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x00160CB4 File Offset: 0x001600B4
			public string[] GetFormats(bool autoConvert)
			{
				bool flag = false;
				string[] array = new string[this._data.Keys.Count];
				this._data.Keys.CopyTo(array, 0);
				if (autoConvert)
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i++)
					{
						DataObject.DataStore.DataStoreEntry[] array2 = (DataObject.DataStore.DataStoreEntry[])this._data[array[i]];
						bool flag2 = true;
						for (int j = 0; j < array2.Length; j++)
						{
							if (!array2[j].AutoConvert)
							{
								flag2 = false;
								break;
							}
						}
						if (flag2)
						{
							string[] mappedFormats = DataObject.GetMappedFormats(array[i]);
							for (int k = 0; k < mappedFormats.Length; k++)
							{
								bool flag3 = false;
								int num = 0;
								while (!flag3 && num < array2.Length)
								{
									if (DataObject.IsFormatAndDataSerializable(mappedFormats[k], array2[num].Data) && (flag || !SecurityHelper.CallerHasSerializationPermission()))
									{
										flag = true;
										flag3 = true;
									}
									num++;
								}
								if (!flag3)
								{
									arrayList.Add(mappedFormats[k]);
								}
							}
						}
						else
						{
							bool flag4 = flag;
							int num2 = 0;
							while (!flag4 && num2 < array2.Length)
							{
								if (DataObject.IsFormatAndDataSerializable(array[i], array2[num2].Data) && !SecurityHelper.CallerHasSerializationPermission())
								{
									flag = true;
									flag4 = true;
								}
								num2++;
							}
							if (!flag4)
							{
								arrayList.Add(array[i]);
							}
						}
					}
					string[] array3 = new string[arrayList.Count];
					arrayList.CopyTo(array3, 0);
					array = DataObject.GetDistinctStrings(array3);
				}
				return array;
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x00160E20 File Offset: 0x00160220
			public void SetData(object data)
			{
				if (data is ISerializable && !this._data.ContainsKey(DataFormats.Serializable))
				{
					this.SetData(DataFormats.Serializable, data);
				}
				this.SetData(data.GetType(), data);
			}

			// Token: 0x060055B5 RID: 21941 RVA: 0x00160E60 File Offset: 0x00160260
			public void SetData(string format, object data)
			{
				this.SetData(format, data, true);
			}

			// Token: 0x060055B6 RID: 21942 RVA: 0x00160E78 File Offset: 0x00160278
			public void SetData(Type format, object data)
			{
				this.SetData(format.FullName, data);
			}

			// Token: 0x060055B7 RID: 21943 RVA: 0x00160E94 File Offset: 0x00160294
			public void SetData(string format, object data, bool autoConvert)
			{
				if (DataObject.IsFormatEqual(format, DataFormats.Dib) && autoConvert && (SystemDrawingHelper.IsBitmap(data) || DataObject.IsDataSystemBitmapSource(data)))
				{
					format = DataFormats.Bitmap;
				}
				this.SetData(format, data, autoConvert, DVASPECT.DVASPECT_CONTENT, 0);
			}

			// Token: 0x060055B8 RID: 21944 RVA: 0x00160ED4 File Offset: 0x001602D4
			private object GetData(string format, bool autoConvert, DVASPECT aspect, int index)
			{
				DataObject.DataStore.DataStoreEntry dataStoreEntry = this.FindDataStoreEntry(format, aspect, index);
				object obj = this.GetDataFromDataStoreEntry(dataStoreEntry, format);
				object obj2 = obj;
				if (autoConvert && (dataStoreEntry == null || dataStoreEntry.AutoConvert) && (obj == null || obj is MemoryStream))
				{
					string[] mappedFormats = DataObject.GetMappedFormats(format);
					if (mappedFormats != null)
					{
						for (int i = 0; i < mappedFormats.Length; i++)
						{
							if (!DataObject.IsFormatEqual(format, mappedFormats[i]))
							{
								DataObject.DataStore.DataStoreEntry dataStoreEntry2 = this.FindDataStoreEntry(mappedFormats[i], aspect, index);
								obj = this.GetDataFromDataStoreEntry(dataStoreEntry2, mappedFormats[i]);
								if (obj != null && !(obj is MemoryStream))
								{
									if (DataObject.IsDataSystemBitmapSource(obj) || SystemDrawingHelper.IsBitmap(obj))
									{
										obj = DataObject.EnsureBitmapDataFromFormat(format, autoConvert, obj);
									}
									obj2 = null;
									break;
								}
							}
						}
					}
				}
				if (obj2 != null)
				{
					return obj2;
				}
				return obj;
			}

			// Token: 0x060055B9 RID: 21945 RVA: 0x00160F84 File Offset: 0x00160384
			private bool GetDataPresent(string format, bool autoConvert, DVASPECT aspect, int index)
			{
				if (autoConvert)
				{
					string[] formats = this.GetFormats(autoConvert);
					for (int i = 0; i < formats.Length; i++)
					{
						if (DataObject.IsFormatEqual(format, formats[i]))
						{
							return true;
						}
					}
					return false;
				}
				if (!this._data.ContainsKey(format))
				{
					return false;
				}
				DataObject.DataStore.DataStoreEntry[] array = (DataObject.DataStore.DataStoreEntry[])this._data[format];
				DataObject.DataStore.DataStoreEntry dataStoreEntry = null;
				DataObject.DataStore.DataStoreEntry dataStoreEntry2 = null;
				foreach (DataObject.DataStore.DataStoreEntry dataStoreEntry3 in array)
				{
					if (dataStoreEntry3.Aspect == aspect && (index == -1 || dataStoreEntry3.Index == index))
					{
						dataStoreEntry = dataStoreEntry3;
						break;
					}
					if (dataStoreEntry3.Aspect == DVASPECT.DVASPECT_CONTENT && dataStoreEntry3.Index == 0)
					{
						dataStoreEntry2 = dataStoreEntry3;
					}
				}
				if (dataStoreEntry == null && dataStoreEntry2 != null)
				{
					dataStoreEntry = dataStoreEntry2;
				}
				return dataStoreEntry != null;
			}

			// Token: 0x060055BA RID: 21946 RVA: 0x0016103C File Offset: 0x0016043C
			private void SetData(string format, object data, bool autoConvert, DVASPECT aspect, int index)
			{
				DataObject.DataStore.DataStoreEntry dataStoreEntry = new DataObject.DataStore.DataStoreEntry(data, autoConvert, aspect, index);
				DataObject.DataStore.DataStoreEntry[] array = (DataObject.DataStore.DataStoreEntry[])this._data[format];
				if (array == null)
				{
					array = (DataObject.DataStore.DataStoreEntry[])Array.CreateInstance(typeof(DataObject.DataStore.DataStoreEntry), 1);
				}
				else
				{
					DataObject.DataStore.DataStoreEntry[] array2 = (DataObject.DataStore.DataStoreEntry[])Array.CreateInstance(typeof(DataObject.DataStore.DataStoreEntry), array.Length + 1);
					array.CopyTo(array2, 1);
					array = array2;
				}
				array[0] = dataStoreEntry;
				this._data[format] = array;
			}

			// Token: 0x060055BB RID: 21947 RVA: 0x001610B8 File Offset: 0x001604B8
			private DataObject.DataStore.DataStoreEntry FindDataStoreEntry(string format, DVASPECT aspect, int index)
			{
				DataObject.DataStore.DataStoreEntry[] array = (DataObject.DataStore.DataStoreEntry[])this._data[format];
				DataObject.DataStore.DataStoreEntry dataStoreEntry = null;
				DataObject.DataStore.DataStoreEntry dataStoreEntry2 = null;
				if (array != null)
				{
					foreach (DataObject.DataStore.DataStoreEntry dataStoreEntry3 in array)
					{
						if (dataStoreEntry3.Aspect == aspect && (index == -1 || dataStoreEntry3.Index == index))
						{
							dataStoreEntry = dataStoreEntry3;
							break;
						}
						if (dataStoreEntry3.Aspect == DVASPECT.DVASPECT_CONTENT && dataStoreEntry3.Index == 0)
						{
							dataStoreEntry2 = dataStoreEntry3;
						}
					}
				}
				if (dataStoreEntry == null && dataStoreEntry2 != null)
				{
					dataStoreEntry = dataStoreEntry2;
				}
				return dataStoreEntry;
			}

			// Token: 0x060055BC RID: 21948 RVA: 0x0016112C File Offset: 0x0016052C
			private object GetDataFromDataStoreEntry(DataObject.DataStore.DataStoreEntry dataStoreEntry, string format)
			{
				object result = null;
				if (dataStoreEntry != null)
				{
					result = dataStoreEntry.Data;
				}
				return result;
			}

			// Token: 0x04002688 RID: 9864
			private Hashtable _data = new Hashtable();

			// Token: 0x02000A1F RID: 2591
			private class DataStoreEntry
			{
				// Token: 0x06005C12 RID: 23570 RVA: 0x00171F9C File Offset: 0x0017139C
				public DataStoreEntry(object data, bool autoConvert, DVASPECT aspect, int index)
				{
					this._data = data;
					this._autoConvert = autoConvert;
					this._aspect = aspect;
					this._index = index;
				}

				// Token: 0x170012D6 RID: 4822
				// (get) Token: 0x06005C13 RID: 23571 RVA: 0x00171FCC File Offset: 0x001713CC
				// (set) Token: 0x06005C14 RID: 23572 RVA: 0x00171FE0 File Offset: 0x001713E0
				public object Data
				{
					get
					{
						return this._data;
					}
					set
					{
						this._data = value;
					}
				}

				// Token: 0x170012D7 RID: 4823
				// (get) Token: 0x06005C15 RID: 23573 RVA: 0x00171FF4 File Offset: 0x001713F4
				public bool AutoConvert
				{
					get
					{
						return this._autoConvert;
					}
				}

				// Token: 0x170012D8 RID: 4824
				// (get) Token: 0x06005C16 RID: 23574 RVA: 0x00172008 File Offset: 0x00171408
				public DVASPECT Aspect
				{
					get
					{
						return this._aspect;
					}
				}

				// Token: 0x170012D9 RID: 4825
				// (get) Token: 0x06005C17 RID: 23575 RVA: 0x0017201C File Offset: 0x0017141C
				public int Index
				{
					get
					{
						return this._index;
					}
				}

				// Token: 0x04002F8D RID: 12173
				private object _data;

				// Token: 0x04002F8E RID: 12174
				private bool _autoConvert;

				// Token: 0x04002F8F RID: 12175
				private DVASPECT _aspect;

				// Token: 0x04002F90 RID: 12176
				private int _index;
			}
		}
	}
}
