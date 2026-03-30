using System;
using System.Security;
using System.Security.Permissions;

namespace System.Windows
{
	/// <summary>Fornece um mecanismo independente de formato para a transferência de dados.</summary>
	// Token: 0x020001C4 RID: 452
	public interface IDataObject
	{
		/// <summary>Recupera um objeto de dados em um formato especificado; o formato de dados é especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato no qual os dados serão recuperados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <returns>Um objeto de dados com os dados no formato especificado ou null, se os dados não estiverem disponíveis no formato especificado.</returns>
		// Token: 0x06000B8C RID: 2956
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		object GetData(string format);

		/// <summary>Recupera um objeto de dados em um formato especificado; o formato de dados é especificado por um objeto <see cref="T:System.Type" />.</summary>
		/// <param name="format">Um objeto <see cref="T:System.Type" /> que especifica o formato com o qual os dados serão recuperados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <returns>Um objeto de dados com os dados no formato especificado ou null, se os dados não estiverem disponíveis no formato especificado.</returns>
		// Token: 0x06000B8D RID: 2957
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		object GetData(Type format);

		/// <summary>Recupera um objeto de dados em um formato especificado, convertendo opcionalmente os dados no formato especificado.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato no qual os dados serão recuperados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> para tentar converter automaticamente os dados no formato especificado; <see langword="false" /> para nenhuma conversão de formato de dados.  
		/// Se esse parâmetro é <see langword="false" />, o método retorna dados no formato especificado, se disponíveis, ou <see langword="null" /> se os dados não estão disponíveis no formato especificado.</param>
		/// <returns>Um objeto de dados com os dados no formato especificado ou null, se os dados não estiverem disponíveis no formato especificado.</returns>
		// Token: 0x06000B8E RID: 2958
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		object GetData(string format, bool autoConvert);

		/// <summary>Verifica se os dados estão disponíveis em um formato especificado ou se podem ser convertidos para esse formato; o formato de dados é especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato em busca do qual verificar. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B8F RID: 2959
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		bool GetDataPresent(string format);

		/// <summary>Verifica se os dados estão disponíveis em um formato especificado ou se podem ser convertidos para esse formato. O formato de dados é especificado por um objeto <see cref="T:System.Type" />.</summary>
		/// <param name="format">Uma <see cref="T:System.Type" /> que especifica o formato em busca do qual verificar. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B90 RID: 2960
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		bool GetDataPresent(Type format);

		/// <summary>Verifica se os dados estão disponíveis em um formato especificado ou se podem ser convertidos para esse formato. Um sinalizador <see langword="Boolean" /> indica se é necessário verificar se os dados podem ser convertidos no formato especificado, se ele não está disponível nesse formato.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato em busca do qual verificar. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="autoConvert">
		///   <see langword="false" /> para verificar somente em busca do formato especificado; <see langword="true" /> para também verificar se os dados armazenados neste objeto de dados podem ou não ser convertidos no formato especificado.</param>
		/// <returns>
		///   <see langword="true" /> se os dados estiverem no formato especificado ou puderem ser convertidos nele; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B91 RID: 2961
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		bool GetDataPresent(string format, bool autoConvert);

		/// <summary>Retorna uma lista de todos os formatos em que os dados deste objeto de dados estão armazenados ou em que podem ser convertidos.</summary>
		/// <returns>Uma matriz de cadeias de caracteres, com cada cadeia de caracteres especificando o nome de um formato compatível com este objeto de dados.</returns>
		// Token: 0x06000B92 RID: 2962
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		string[] GetFormats();

		/// <summary>Retorna uma lista de todos os formatos em que os dados deste objeto de dados estão armazenados. Um sinalizador <see langword="Boolean" /> indica se deve-se ou não incluir também os formatos nos quais os dados podem ser automaticamente convertidos.</summary>
		/// <param name="autoConvert">
		///   <see langword="true" /> para recuperar todos os formatos nos quais os dados armazenados neste objeto de dados são armazenados ou nos quais podem ser convertidos; <see langword="false" /> para recuperar apenas os formatos nos quais os dados armazenados neste objeto de dados são armazenados (excluindo formatos nos quais os dados não são armazenados, mas nos quais podem ser convertidos automaticamente).</param>
		/// <returns>Uma matriz de cadeias de caracteres, com cada cadeia de caracteres especificando o nome de um formato compatível com este objeto de dados.</returns>
		// Token: 0x06000B93 RID: 2963
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		string[] GetFormats(bool autoConvert);

		/// <summary>Armazena os dados especificados neste objeto de dados, convertendo automaticamente o formato de dados do tipo de objeto de origem.</summary>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		// Token: 0x06000B94 RID: 2964
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		void SetData(object data);

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados. O formato de dados é especificado por uma cadeia de caracteres.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato no qual os dados serão armazenados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		// Token: 0x06000B95 RID: 2965
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		void SetData(string format, object data);

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados. O formato de dados é especificado por uma classe <see cref="T:System.Type" />.</summary>
		/// <param name="format">Um <see cref="T:System.Type" /> que especifica em qual formato armazenar os dados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		// Token: 0x06000B96 RID: 2966
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		void SetData(Type format, object data);

		/// <summary>Armazena os dados especificados neste objeto de dados, juntamente com um ou mais formatos de dados especificados. Essa sobrecarga inclui um sinalizador <see langword="Boolean" /> para indicar se os dados podem ou não ser convertidos para outro formato ao serem recuperados.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato no qual os dados serão armazenados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="data">Os dados a serem armazenados neste objeto de dados.</param>
		/// <param name="autoConvert">
		///   <see langword="true" /> para permitir que os dados sejam convertidos em outro formato ao serem recuperados; <see langword="false" /> para impedir que os dados sejam convertidos em outro formato ao serem recuperados.</param>
		// Token: 0x06000B97 RID: 2967
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Clipboard = UIPermissionClipboard.AllClipboard)]
		void SetData(string format, object data, bool autoConvert);
	}
}
