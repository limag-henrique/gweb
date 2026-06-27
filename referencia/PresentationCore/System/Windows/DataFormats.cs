using System;
using System.Collections;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Windows.Ink;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows
{
	/// <summary>Fornece um conjunto de nomes de formatos de dados predefinidos que podem ser usados para identificar os formatos de dados disponíveis nas operações de área de transferência ou do tipo "arrastar e soltar".</summary>
	// Token: 0x02000193 RID: 403
	public static class DataFormats
	{
		/// <summary>Retorna um objeto <see cref="T:System.Windows.DataFormat" /> que define um nome e ID numérica para o formato de dados especificado. O formato de dados desejado é especificado pela ID numérica.</summary>
		/// <param name="id">A ID numérica do formato de dados para a qual solicitar um objeto <see cref="T:System.Windows.DataFormat" />.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.DataFormat" /> que contém a ID numérica e o nome do formato de dados solicitado.</returns>
		// Token: 0x06000588 RID: 1416 RVA: 0x00019F28 File Offset: 0x00019328
		public static DataFormat GetDataFormat(int id)
		{
			return DataFormats.InternalGetDataFormat(id);
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.DataFormat" /> que define um nome e ID numérica para o formato de dados especificado. O formato de dados desejado é especificado pelo nome (uma cadeia de caracteres).</summary>
		/// <param name="format">o nome do formato de dados para o qual solicitar um objeto <see cref="T:System.Windows.DataFormat" />.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.DataFormat" /> que contém a ID numérica e o nome do formato de dados solicitado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> é <see langword="null" />.</exception>
		// Token: 0x06000589 RID: 1417 RVA: 0x00019F3C File Offset: 0x0001933C
		[SecurityCritical]
		public static DataFormat GetDataFormat(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			DataFormats.EnsurePredefined();
			object formatListlock = DataFormats._formatListlock;
			DataFormat result;
			lock (formatListlock)
			{
				for (int i = 0; i < DataFormats._formatList.Count; i++)
				{
					DataFormat dataFormat = (DataFormat)DataFormats._formatList[i];
					if (dataFormat.Name.Equals(format))
					{
						return dataFormat;
					}
				}
				for (int j = 0; j < DataFormats._formatList.Count; j++)
				{
					DataFormat dataFormat2 = (DataFormat)DataFormats._formatList[j];
					if (string.Compare(dataFormat2.Name, format, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return dataFormat2;
					}
				}
				SecurityHelper.DemandUnmanagedCode();
				int num = UnsafeNativeMethods.RegisterClipboardFormat(format);
				if (num == 0)
				{
					throw new Win32Exception();
				}
				int index = DataFormats._formatList.Add(new DataFormat(format, num));
				result = (DataFormat)DataFormats._formatList[index];
			}
			return result;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001A06C File Offset: 0x0001946C
		internal static string ConvertToDataFormats(TextDataFormat textDataformat)
		{
			string result = DataFormats.UnicodeText;
			switch (textDataformat)
			{
			case TextDataFormat.Text:
				result = DataFormats.Text;
				break;
			case TextDataFormat.UnicodeText:
				result = DataFormats.UnicodeText;
				break;
			case TextDataFormat.Rtf:
				result = DataFormats.Rtf;
				break;
			case TextDataFormat.Html:
				result = DataFormats.Html;
				break;
			case TextDataFormat.CommaSeparatedValue:
				result = DataFormats.CommaSeparatedValue;
				break;
			case TextDataFormat.Xaml:
				result = DataFormats.Xaml;
				break;
			}
			return result;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001A0D0 File Offset: 0x000194D0
		internal static bool IsValidTextDataFormat(TextDataFormat textDataFormat)
		{
			return textDataFormat == TextDataFormat.Text || textDataFormat == TextDataFormat.UnicodeText || textDataFormat == TextDataFormat.Rtf || textDataFormat == TextDataFormat.Html || textDataFormat == TextDataFormat.CommaSeparatedValue || textDataFormat == TextDataFormat.Xaml;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001A0F8 File Offset: 0x000194F8
		[SecuritySafeCritical]
		private static DataFormat InternalGetDataFormat(int id)
		{
			DataFormats.EnsurePredefined();
			object formatListlock = DataFormats._formatListlock;
			DataFormat result;
			lock (formatListlock)
			{
				for (int i = 0; i < DataFormats._formatList.Count; i++)
				{
					DataFormat dataFormat = (DataFormat)DataFormats._formatList[i];
					if ((dataFormat.Id & 65535) == (id & 65535))
					{
						return dataFormat;
					}
				}
				StringBuilder stringBuilder = new StringBuilder(260);
				if (UnsafeNativeMethods.GetClipboardFormatName(id, stringBuilder, stringBuilder.Capacity) == 0)
				{
					stringBuilder.Length = 0;
					stringBuilder.Append("Format").Append(id);
				}
				int index = DataFormats._formatList.Add(new DataFormat(stringBuilder.ToString(), id));
				result = (DataFormat)DataFormats._formatList[index];
			}
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001A1E8 File Offset: 0x000195E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void EnsurePredefined()
		{
			object formatListlock = DataFormats._formatListlock;
			lock (formatListlock)
			{
				if (DataFormats._formatList == null)
				{
					DataFormats._formatList = new ArrayList(19);
					DataFormats._formatList.Add(new DataFormat(DataFormats.UnicodeText, 13));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Text, 1));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Bitmap, 2));
					DataFormats._formatList.Add(new DataFormat(DataFormats.MetafilePicture, 3));
					DataFormats._formatList.Add(new DataFormat(DataFormats.EnhancedMetafile, 14));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Dif, 5));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Tiff, 6));
					DataFormats._formatList.Add(new DataFormat(DataFormats.OemText, 7));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Dib, 8));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Palette, 9));
					DataFormats._formatList.Add(new DataFormat(DataFormats.PenData, 10));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Riff, 11));
					DataFormats._formatList.Add(new DataFormat(DataFormats.WaveAudio, 12));
					DataFormats._formatList.Add(new DataFormat(DataFormats.SymbolicLink, 4));
					DataFormats._formatList.Add(new DataFormat(DataFormats.FileDrop, 15));
					DataFormats._formatList.Add(new DataFormat(DataFormats.Locale, 16));
					int num = UnsafeNativeMethods.RegisterClipboardFormat(DataFormats.Xaml);
					if (num != 0)
					{
						DataFormats._formatList.Add(new DataFormat(DataFormats.Xaml, num));
					}
					int num2 = UnsafeNativeMethods.RegisterClipboardFormat(DataFormats.ApplicationTrust);
					if (num2 != 0)
					{
						DataFormats._formatList.Add(new DataFormat(DataFormats.ApplicationTrust, num2));
					}
					int num3 = UnsafeNativeMethods.RegisterClipboardFormat(StrokeCollection.InkSerializedFormat);
					if (num3 != 0)
					{
						DataFormats._formatList.Add(new DataFormat(StrokeCollection.InkSerializedFormat, num3));
					}
				}
			}
		}

		/// <summary>Especifica o formato de dados de texto ANSI.</summary>
		// Token: 0x04000533 RID: 1331
		public static readonly string Text = "Text";

		/// <summary>Especifica o formato de dados de texto Unicode.</summary>
		// Token: 0x04000534 RID: 1332
		public static readonly string UnicodeText = "UnicodeText";

		/// <summary>Especifica o formato de dados de DIB.</summary>
		// Token: 0x04000535 RID: 1333
		public static readonly string Dib = "DeviceIndependentBitmap";

		/// <summary>Especifica um formato de dados de bitmap Microsoft Windows.</summary>
		// Token: 0x04000536 RID: 1334
		public static readonly string Bitmap = "Bitmap";

		/// <summary>Especifica o formato de metarquivo avançado Windows.</summary>
		// Token: 0x04000537 RID: 1335
		public static readonly string EnhancedMetafile = "EnhancedMetafile";

		/// <summary>Especifica o formato de dados da imagem do metarquivo Windows.</summary>
		// Token: 0x04000538 RID: 1336
		public static readonly string MetafilePicture = "MetaFilePict";

		/// <summary>Especifica o formato de dados do link simbólico Windows.</summary>
		// Token: 0x04000539 RID: 1337
		public static readonly string SymbolicLink = "SymbolicLink";

		/// <summary>Especifica o formato de dados DIF do Windows.</summary>
		// Token: 0x0400053A RID: 1338
		public static readonly string Dif = "DataInterchangeFormat";

		/// <summary>Especifica o formato de dados de TIFF (formato TIFF).</summary>
		// Token: 0x0400053B RID: 1339
		public static readonly string Tiff = "TaggedImageFileFormat";

		/// <summary>Especifica o formato de dados de texto do OEM Windows padrão.</summary>
		// Token: 0x0400053C RID: 1340
		public static readonly string OemText = "OEMText";

		/// <summary>Especifica o formato de dados da paleta Windows.</summary>
		// Token: 0x0400053D RID: 1341
		public static readonly string Palette = "Palette";

		/// <summary>Especifica o formato de dados da caneta Windows.</summary>
		// Token: 0x0400053E RID: 1342
		public static readonly string PenData = "PenData";

		/// <summary>Especifica o formato de dados de áudio RIFF (Resource Interchange File Format).</summary>
		// Token: 0x0400053F RID: 1343
		public static readonly string Riff = "RiffAudio";

		/// <summary>Especifica o formato de dados de áudio wave.</summary>
		// Token: 0x04000540 RID: 1344
		public static readonly string WaveAudio = "WaveAudio";

		/// <summary>Especifica o formato para soltar o arquivo Windows.</summary>
		// Token: 0x04000541 RID: 1345
		public static readonly string FileDrop = "FileDrop";

		/// <summary>Especifica o formato de dados de localidade (cultura) Windows.</summary>
		// Token: 0x04000542 RID: 1346
		public static readonly string Locale = "Locale";

		/// <summary>Especifica o formato de dados de HTML.</summary>
		// Token: 0x04000543 RID: 1347
		public static readonly string Html = "HTML Format";

		/// <summary>Especifica o formato de dados de RTF (Formato Rich Text).</summary>
		// Token: 0x04000544 RID: 1348
		public static readonly string Rtf = "Rich Text Format";

		/// <summary>Especifica um formato de dados CSV (valores separados por vírgula).</summary>
		// Token: 0x04000545 RID: 1349
		public static readonly string CommaSeparatedValue = "CSV";

		/// <summary>Especifica o formato de dados da classe da cadeia de caracteres CLR (Common Language Runtime).</summary>
		// Token: 0x04000546 RID: 1350
		public static readonly string StringFormat = typeof(string).FullName;

		/// <summary>Especifica um formato de dados que encapsula qualquer tipo de objetos de dados serializáveis.</summary>
		// Token: 0x04000547 RID: 1351
		public static readonly string Serializable = "PersistentObject";

		/// <summary>Especifica o formato de dados de XAML (linguagem XAML).</summary>
		// Token: 0x04000548 RID: 1352
		public static readonly string Xaml = "Xaml";

		/// <summary>Especifica o formato de dados de pacote XAML (linguagem XAML).</summary>
		// Token: 0x04000549 RID: 1353
		public static readonly string XamlPackage = "XamlPackage";

		// Token: 0x0400054A RID: 1354
		internal static readonly string ApplicationTrust = "ApplicationTrust";

		// Token: 0x0400054B RID: 1355
		internal static readonly string FileName = "FileName";

		// Token: 0x0400054C RID: 1356
		internal static readonly string FileNameW = "FileNameW";

		// Token: 0x0400054D RID: 1357
		private static ArrayList _formatList;

		// Token: 0x0400054E RID: 1358
		private static object _formatListlock = new object();
	}
}
