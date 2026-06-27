using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	/// <summary>Fornece métodos estáticos que facilitam a transferência de dados de e para a Área de Transferência do sistema.</summary>
	// Token: 0x0200018E RID: 398
	public static class Clipboard
	{
		/// <summary>Limpa todos os dados da área de transferência do sistema.</summary>
		// Token: 0x060003DA RID: 986 RVA: 0x00015DB8 File Offset: 0x000151B8
		[SecurityCritical]
		public static void Clear()
		{
			int num = 10;
			for (;;)
			{
				int num2 = OleServicesContext.CurrentOleServicesContext.OleSetClipboard(null);
				if (NativeMethods.Succeeded(num2))
				{
					break;
				}
				if (--num == 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				Thread.Sleep(100);
			}
		}

		/// <summary>Consultas a Área de Transferência quanto à presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />.</summary>
		/// <returns>
		///   <see langword="true" /> se a Área de Transferência contiver dados no formato <see cref="F:System.Windows.DataFormats.WaveAudio" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060003DB RID: 987 RVA: 0x00015DF4 File Offset: 0x000151F4
		public static bool ContainsAudio()
		{
			return Clipboard.ContainsDataInternal(DataFormats.WaveAudio);
		}

		/// <summary>Consulta a Área de Transferência quanto à presença de dados no formato de dados especificado.</summary>
		/// <param name="format">O formato dos dados a serem procurados. Consulte <see cref="T:System.Windows.DataFormats" /> para obter os formatos predefinidos.</param>
		/// <returns>
		///   <see langword="true" /> se os dados no formato especificado estiverem disponíveis na Área de Transferência; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x060003DC RID: 988 RVA: 0x00015E0C File Offset: 0x0001520C
		public static bool ContainsData(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return Clipboard.ContainsDataInternal(format);
		}

		/// <summary>Consultas a Área de Transferência quanto à presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.FileDrop" />.</summary>
		/// <returns>
		///   <see langword="true" /> se a Área de Transferência contiver dados no formato <see cref="F:System.Windows.DataFormats.FileDrop" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060003DD RID: 989 RVA: 0x00015E4C File Offset: 0x0001524C
		public static bool ContainsFileDropList()
		{
			return Clipboard.ContainsDataInternal(DataFormats.FileDrop);
		}

		/// <summary>Consultas a Área de Transferência quanto à presença de dados no formato de dados <see cref="F:System.Windows.DataFormats.Bitmap" />.</summary>
		/// <returns>
		///   <see langword="true" /> se a Área de Transferência contiver dados no formato <see cref="F:System.Windows.DataFormats.Bitmap" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060003DE RID: 990 RVA: 0x00015E64 File Offset: 0x00015264
		public static bool ContainsImage()
		{
			return Clipboard.ContainsDataInternal(DataFormats.Bitmap);
		}

		/// <summary>Consultas a Área de Transferência quanto à presença de dados no formato <see cref="F:System.Windows.DataFormats.UnicodeText" />.</summary>
		/// <returns>
		///   <see langword="true" /> se a Área de Transferência contiver dados no formato <see cref="F:System.Windows.DataFormats.UnicodeText" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060003DF RID: 991 RVA: 0x00015E7C File Offset: 0x0001527C
		public static bool ContainsText()
		{
			return Clipboard.ContainsDataInternal(DataFormats.UnicodeText);
		}

		/// <summary>Consultas a Área de Transferência quanto à presença de dados em um formato de dados de texto.</summary>
		/// <param name="format">Um membro da enumeração <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto a ser consultado.</param>
		/// <returns>
		///   <see langword="true" /> se a Área de Transferência contiver dados no formato de dados de texto especificado, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> não especifica um membro válido de <see cref="T:System.Windows.TextDataFormat" />.</exception>
		// Token: 0x060003E0 RID: 992 RVA: 0x00015E94 File Offset: 0x00015294
		public static bool ContainsText(TextDataFormat format)
		{
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			return Clipboard.ContainsDataInternal(DataFormats.ConvertToDataFormats(format));
		}

		/// <summary>Adiciona permanentemente os dados que estão no <see cref="T:System.Windows.Clipboard" /> para que eles estejam disponíveis depois que o aplicativo original dos dados for fechado.</summary>
		// Token: 0x060003E1 RID: 993 RVA: 0x00015ECC File Offset: 0x000152CC
		public static void Flush()
		{
			int num = 10;
			for (;;)
			{
				int hr = OleServicesContext.CurrentOleServicesContext.OleFlushClipboard();
				if (NativeMethods.Succeeded(hr))
				{
					break;
				}
				if (--num == 0)
				{
					SecurityHelper.ThrowExceptionForHR(hr);
				}
				Thread.Sleep(100);
			}
		}

		/// <summary>Retorna um fluxo de dados da Área de Transferência no formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />.</summary>
		/// <returns>Um fluxo que contém dados no formato <see cref="F:System.Windows.DataFormats.WaveAudio" /> ou <see langword="null" /> se a Área de Transferência não contiver dados neste formato.</returns>
		// Token: 0x060003E2 RID: 994 RVA: 0x00015F08 File Offset: 0x00015308
		public static Stream GetAudioStream()
		{
			return Clipboard.GetDataInternal(DataFormats.WaveAudio) as Stream;
		}

		/// <summary>Recupera dados em um formato especificado da Área de transferência.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato dos dados a serem recuperados. Para um conjunto de formatos de dados predefinidos, consulte a classe <see cref="T:System.Windows.DataFormats" />.</param>
		/// <returns>Um objeto que contém os dados no formato especificado ou <see langword="null" /> se os dados não estiverem disponíveis no formato especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x00015F24 File Offset: 0x00015324
		public static object GetData(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			return Clipboard.GetDataInternal(format);
		}

		/// <summary>Retorna uma coleção de cadeia de caracteres que contém uma lista de arquivos ignorados disponíveis na Área de Transferência.</summary>
		/// <returns>Uma coleção de cadeias de caracteres, na qual cada cadeia de caracteres especifica o nome de um arquivo na lista de arquivos ignorados na Área de Transferência ou <see langword="null" /> se os dados não estiverem disponíveis neste formato.</returns>
		// Token: 0x060003E4 RID: 996 RVA: 0x00015F64 File Offset: 0x00015364
		public static StringCollection GetFileDropList()
		{
			StringCollection stringCollection = new StringCollection();
			string[] array = Clipboard.GetDataInternal(DataFormats.FileDrop) as string[];
			if (array != null)
			{
				stringCollection.AddRange(array);
			}
			return stringCollection;
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> da Área de Transferência que contém dados no formato <see cref="F:System.Windows.DataFormats.Bitmap" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que contém dados no formato <see cref="F:System.Windows.DataFormats.Bitmap" /> ou <see langword="null" />, se os dados estiverem indisponíveis nesse formato.</returns>
		// Token: 0x060003E5 RID: 997 RVA: 0x00015F94 File Offset: 0x00015394
		public static BitmapSource GetImage()
		{
			return Clipboard.GetDataInternal(DataFormats.Bitmap) as BitmapSource;
		}

		/// <summary>Retorna uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> na Área de Transferência.</summary>
		/// <returns>Uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> ou uma cadeia de caracteres vazia se nenhum dado <see cref="F:System.Windows.DataFormats.UnicodeText" /> estiver disponível na Área de Transferência.</returns>
		// Token: 0x060003E6 RID: 998 RVA: 0x00015FB0 File Offset: 0x000153B0
		public static string GetText()
		{
			return Clipboard.GetText(TextDataFormat.UnicodeText);
		}

		/// <summary>Retorna uma cadeia de caracteres que contém os dados de texto na Área de Transferência.</summary>
		/// <param name="format">Um membro do <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto a ser recuperado.</param>
		/// <returns>Uma cadeia de caracteres que contém dados de texto no formato de dados especificado ou uma cadeia de caracteres vazia se nenhum dado de texto correspondente estiver disponível.</returns>
		// Token: 0x060003E7 RID: 999 RVA: 0x00015FC4 File Offset: 0x000153C4
		public static string GetText(TextDataFormat format)
		{
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			string text = (string)Clipboard.GetDataInternal(DataFormats.ConvertToDataFormats(format));
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		/// <summary>Armazena dados de áudio (formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />) na Área de Transferência.  Os dados de áudio são especificados como uma matriz de bytes.</summary>
		/// <param name="audioBytes">Uma matriz de bytes que contém dados de áudio a serem armazenados na Área de Transferência.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioBytes" /> é <see langword="null" />.</exception>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001600C File Offset: 0x0001540C
		public static void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			Clipboard.SetAudio(new MemoryStream(audioBytes));
		}

		/// <summary>Armazena dados de áudio (formato de dados <see cref="F:System.Windows.DataFormats.WaveAudio" />) na Área de Transferência.  Os dados de áudio são especificados como um fluxo.</summary>
		/// <param name="audioStream">Um fluxo que contém dados de áudio a serem armazenados na Área de Transferência.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioStream" /> é <see langword="null" />.</exception>
		// Token: 0x060003E9 RID: 1001 RVA: 0x00016034 File Offset: 0x00015434
		public static void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			Clipboard.SetDataInternal(DataFormats.WaveAudio, audioStream);
		}

		/// <summary>Armazena os dados especificados na Área de Transferência no formato especificado.</summary>
		/// <param name="format">Uma cadeia de caracteres que especifica o formato a ser usado para armazenar os dados. Consulte a classe <see cref="T:System.Windows.DataFormats" /> para obter um conjunto de formatos de dados predefinidos.</param>
		/// <param name="data">Um objeto que representa os dados a serem armazenados na Área de Transferência.</param>
		// Token: 0x060003EA RID: 1002 RVA: 0x0001605C File Offset: 0x0001545C
		public static void SetData(string format, object data)
		{
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
			Clipboard.SetDataInternal(format, data);
		}

		/// <summary>Armazena dados <see cref="F:System.Windows.DataFormats.FileDrop" /> na área de transferência.  A lista de arquivos ignorados é especificada como uma coleção de cadeia de caracteres.</summary>
		/// <param name="fileDropList">Uma coleção de cadeias de caracteres que contém a lista de arquivos ignorados a serem armazenados no objeto de dados.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileDropList" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileDropList" /> não contém nenhuma cadeia de caracteres ou o caminho completo para o arquivo especificado na lista não pode ser resolvido.</exception>
		// Token: 0x060003EB RID: 1003 RVA: 0x000160AC File Offset: 0x000154AC
		public static void SetFileDropList(StringCollection fileDropList)
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
				catch (ArgumentException)
				{
					throw new ArgumentException(SR.Get("DataObject_FileDropListHasInvalidFileDropPath", new object[]
					{
						fileDropList
					}));
				}
			}
			string[] array = new string[fileDropList.Count];
			fileDropList.CopyTo(array, 0);
			Clipboard.SetDataInternal(DataFormats.FileDrop, array);
		}

		/// <summary>Armazena dados <see cref="F:System.Windows.DataFormats.Bitmap" /> na área de transferência.  Os dados da imagem são especificados como um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
		/// <param name="image">Um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que contém os dados de imagem a armazenar na Área de Transferência.</param>
		// Token: 0x060003EC RID: 1004 RVA: 0x00016190 File Offset: 0x00015590
		public static void SetImage(BitmapSource image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			Clipboard.SetDataInternal(DataFormats.Bitmap, image);
		}

		/// <summary>Armazena dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> na área de transferência.</summary>
		/// <param name="text">Uma cadeia de caracteres que contém os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> a serem armazenados na área de transferência.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> é <see langword="null" />.</exception>
		// Token: 0x060003ED RID: 1005 RVA: 0x000161B8 File Offset: 0x000155B8
		public static void SetText(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			Clipboard.SetText(text, TextDataFormat.UnicodeText);
		}

		/// <summary>Armazena dados de texto na área de transferência em um formato de dados de texto especificado.  Os dados <see cref="F:System.Windows.DataFormats.UnicodeText" /> a serem armazenados são especificados como uma cadeia de caracteres.</summary>
		/// <param name="text">Uma cadeia de caracteres que contém os dados de texto a serem armazenados na área de transferência.</param>
		/// <param name="format">Um membro de <see cref="T:System.Windows.TextDataFormat" /> que especifica o formato de dados de texto específico a ser armazenado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="text" /> é <see langword="null" />.</exception>
		// Token: 0x060003EE RID: 1006 RVA: 0x000161DC File Offset: 0x000155DC
		public static void SetText(string text, TextDataFormat format)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (!DataFormats.IsValidTextDataFormat(format))
			{
				throw new InvalidEnumArgumentException("format", (int)format, typeof(TextDataFormat));
			}
			Clipboard.SetDataInternal(DataFormats.ConvertToDataFormats(format), text);
		}

		/// <summary>Retorna um objeto de dados que representa todo o conteúdo da Área de Transferência.</summary>
		/// <returns>Um objeto de dados que permite acessar todo o conteúdo da Área de Transferência do sistema ou <see langword="null" />, se não houver dados na Área de Transferência.</returns>
		// Token: 0x060003EF RID: 1007 RVA: 0x00016224 File Offset: 0x00015624
		[SecurityCritical]
		public static IDataObject GetDataObject()
		{
			SecurityHelper.DemandAllClipboardPermission();
			return Clipboard.GetDataObjectInternal();
		}

		/// <summary>Compara a um objeto de dados especificado ao conteúdo da Área de Transferência.</summary>
		/// <param name="data">Um objeto de dados a ser comparado com o conteúdo da Área de Transferência do sistema.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto de dados especificado corresponder ao que está na Área de Transferência do sistema, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Ocorreu um erro ao acessar a Área de Transferência. Os detalhes da exceção incluirão um <see langword="HResult" /> que identifica o erro específico, consulte <see cref="P:System.Runtime.InteropServices.ErrorWrapper.ErrorCode" />.</exception>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0001623C File Offset: 0x0001563C
		public static bool IsCurrent(IDataObject data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			bool result = false;
			if (data is IDataObject)
			{
				int num = 10;
				int num2;
				for (;;)
				{
					num2 = OleServicesContext.CurrentOleServicesContext.OleIsCurrentClipboard((IDataObject)data);
					if (NativeMethods.Succeeded(num2) || --num == 0)
					{
						break;
					}
					Thread.Sleep(100);
				}
				if (num2 == 0)
				{
					result = true;
				}
				else if (!NativeMethods.Succeeded(num2))
				{
					throw new ExternalException("OleIsCurrentClipboard()", num2);
				}
			}
			return result;
		}

		/// <summary>Insere um objeto de dados não persistentes especificado na área de transferência do sistema.</summary>
		/// <param name="data">Um objeto de dados (um objeto que implementa <see cref="T:System.Windows.IDataObject" />) para colocar na área de transferência do sistema.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Ocorreu um erro ao acessar a Área de Transferência. Os detalhes da exceção incluirão um <see langword="HResult" /> que identifica o erro específico, consulte <see cref="P:System.Runtime.InteropServices.ErrorWrapper.ErrorCode" />.</exception>
		// Token: 0x060003F1 RID: 1009 RVA: 0x000162AC File Offset: 0x000156AC
		[SecurityCritical]
		public static void SetDataObject(object data)
		{
			SecurityHelper.DemandAllClipboardPermission();
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Clipboard.SetDataObject(data, false);
		}

		/// <summary>Coloca um objeto de dados especificado na área de transferência do sistema e aceita um parâmetro booliano que indica se o objeto de dados deve ser deixado na área de transferência quando o aplicativo é encerrado.</summary>
		/// <param name="data">Um objeto de dados (um objeto que implementa <see cref="T:System.Windows.IDataObject" />) para colocar na área de transferência do sistema.</param>
		/// <param name="copy">
		///   <see langword="true" /> para deixar os dados na Área de Transferência do sistema quando o aplicativo é encerrado, <see langword="false" /> para limpar os dados da Área de Transferência do sistema quando o aplicativo é encerrado.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">Ocorreu um erro ao acessar a Área de Transferência.  Os detalhes da exceção incluirão um <see langword="HResult" /> que identifica o erro específico, consulte <see cref="P:System.Runtime.InteropServices.ErrorWrapper.ErrorCode" />.</exception>
		// Token: 0x060003F2 RID: 1010 RVA: 0x000162D4 File Offset: 0x000156D4
		[SecurityCritical]
		public static void SetDataObject(object data, bool copy)
		{
			SecurityHelper.DemandAllClipboardPermission();
			Clipboard.CriticalSetDataObject(data, copy);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000162F0 File Offset: 0x000156F0
		internal static bool UseLegacyDangerousClipboardDeserializationMode()
		{
			return !Clipboard.IsDeviceGuardEnabled && CoreAppContextSwitches.EnableLegacyDangerousClipboardDeserializationMode;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001630C File Offset: 0x0001570C
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static void CriticalSetDataObject(object data, bool copy)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			IDataObject dataObject;
			if (data is DataObject)
			{
				dataObject = (DataObject)data;
			}
			else if (data is IDataObject)
			{
				SecurityHelper.DemandUnmanagedCode();
				dataObject = (IDataObject)data;
			}
			else
			{
				dataObject = new DataObject(data);
			}
			int num = 10;
			for (;;)
			{
				int num2 = OleServicesContext.CurrentOleServicesContext.OleSetClipboard(dataObject);
				if (NativeMethods.Succeeded(num2))
				{
					break;
				}
				if (--num == 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				Thread.Sleep(100);
			}
			if (copy)
			{
				Thread.Sleep(10);
				Clipboard.Flush();
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016394 File Offset: 0x00015794
		[SecurityCritical]
		[FriendAccessAllowed]
		[SecurityTreatAsSafe]
		internal static bool IsClipboardPopulated()
		{
			bool result = false;
			new UIPermission(UIPermissionClipboard.AllClipboard).Assert();
			try
			{
				result = (Clipboard.GetDataObjectInternal() != null);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000163DC File Offset: 0x000157DC
		private static bool IsDeviceGuardEnabled
		{
			get
			{
				if (Clipboard._isDeviceGuardEnabled < 0)
				{
					return false;
				}
				if (Clipboard._isDeviceGuardEnabled > 0)
				{
					return true;
				}
				bool flag = Clipboard.IsDynamicCodePolicyEnabled();
				Clipboard._isDeviceGuardEnabled = (flag ? 1 : -1);
				return flag;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016410 File Offset: 0x00015810
		[SecuritySafeCritical]
		private static bool IsDynamicCodePolicyEnabled()
		{
			bool result = false;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = LoadLibraryHelper.SecureLoadLibraryEx("wldp.dll", IntPtr.Zero, UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32);
				if (intPtr != IntPtr.Zero)
				{
					IntPtr procAddressNoThrow = UnsafeNativeMethods.GetProcAddressNoThrow(new HandleRef(null, intPtr), "WldpIsDynamicCodePolicyEnabled");
					if (procAddressNoThrow != IntPtr.Zero)
					{
						int num = UnsafeNativeMethods.WldpIsDynamicCodePolicyEnabled(out result);
						if (num != 0)
						{
							result = false;
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.FreeLibrary(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000164C4 File Offset: 0x000158C4
		[SecurityCritical]
		private static bool IsDataObjectFromLessPriviligedApplicationDomain(IDataObject dataObjectToApply)
		{
			bool result = false;
			object obj = null;
			bool dataPresent = dataObjectToApply.GetDataPresent(DataFormats.ApplicationTrust, false);
			if (dataPresent)
			{
				obj = dataObjectToApply.GetData(DataFormats.ApplicationTrust, false);
			}
			if (obj != null)
			{
				string xml = obj.ToString();
				PermissionSet permissionSet;
				try
				{
					SecurityElement et = SecurityElement.FromString(xml);
					permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.FromXml(et);
				}
				catch (XmlSyntaxException)
				{
					return true;
				}
				PermissionSet permissionSet2 = SecurityHelper.ExtractAppDomainPermissionSetMinusSiteOfOrigin();
				if (!permissionSet2.IsSubsetOf(permissionSet))
				{
					return true;
				}
				return result;
			}
			return result;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016558 File Offset: 0x00015958
		[SecurityCritical]
		private static IDataObject GetDataObjectInternal()
		{
			int num = 10;
			IDataObject dataObject;
			for (;;)
			{
				dataObject = null;
				int num2 = OleServicesContext.CurrentOleServicesContext.OleGetClipboard(ref dataObject);
				if (NativeMethods.Succeeded(num2))
				{
					break;
				}
				if (--num == 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				Thread.Sleep(100);
			}
			IDataObject dataObject2;
			if (dataObject is IDataObject)
			{
				dataObject2 = (IDataObject)dataObject;
			}
			else if (dataObject != null)
			{
				dataObject2 = new DataObject(dataObject);
			}
			else
			{
				dataObject2 = null;
			}
			if (dataObject2 != null && Clipboard.IsDataObjectFromLessPriviligedApplicationDomain(dataObject2) && (dataObject2.GetDataPresent(DataFormats.Xaml, false) || dataObject2.GetDataPresent(DataFormats.ApplicationTrust, false)))
			{
				dataObject2 = new ConstrainedDataObject(dataObject2);
			}
			return dataObject2;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000165E4 File Offset: 0x000159E4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static bool ContainsDataInternal(string format)
		{
			SecurityHelper.DemandAllClipboardPermission();
			bool result = false;
			if (Clipboard.IsDataFormatAutoConvert(format))
			{
				string[] mappedFormats = DataObject.GetMappedFormats(format);
				for (int i = 0; i < mappedFormats.Length; i++)
				{
					if (SafeNativeMethods.IsClipboardFormatAvailable(DataFormats.GetDataFormat(mappedFormats[i]).Id))
					{
						result = true;
						break;
					}
				}
			}
			else
			{
				result = SafeNativeMethods.IsClipboardFormatAvailable(DataFormats.GetDataFormat(format).Id);
			}
			return result;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00016644 File Offset: 0x00015A44
		private static object GetDataInternal(string format)
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject != null)
			{
				bool autoConvert = Clipboard.IsDataFormatAutoConvert(format);
				return dataObject.GetData(format, autoConvert);
			}
			return null;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016674 File Offset: 0x00015A74
		private static void SetDataInternal(string format, object data)
		{
			bool autoConvert = Clipboard.IsDataFormatAutoConvert(format);
			IDataObject dataObject = new DataObject();
			dataObject.SetData(format, data, autoConvert);
			Clipboard.SetDataObject(dataObject, true);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000166A8 File Offset: 0x00015AA8
		private static bool IsDataFormatAutoConvert(string format)
		{
			return string.CompareOrdinal(format, DataFormats.FileDrop) == 0 || string.CompareOrdinal(format, DataFormats.Bitmap) == 0;
		}

		// Token: 0x040004C3 RID: 1219
		private const int OleRetryCount = 10;

		// Token: 0x040004C4 RID: 1220
		private const int OleRetryDelay = 100;

		// Token: 0x040004C5 RID: 1221
		private const int OleFlushDelay = 10;

		// Token: 0x040004C6 RID: 1222
		private static int _isDeviceGuardEnabled;
	}
}
