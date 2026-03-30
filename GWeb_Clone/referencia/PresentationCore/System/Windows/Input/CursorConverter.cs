using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Input.Cursor" /> de e em outros tipos.</summary>
	// Token: 0x02000231 RID: 561
	public class CursorConverter : TypeConverter
	{
		/// <summary>Determina se um objeto do tipo especificado pode ser convertido em uma instância do <see cref="T:System.Windows.Input.Cursor" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="sourceType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="sourceType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003B75C File Offset: 0x0003AB5C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se uma instância do <see cref="T:System.Windows.Input.Cursor" /> pode ser convertida no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003B780 File Offset: 0x0003AB80
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003B7A4 File Offset: 0x0003ABA4
		private PropertyInfo[] GetProperties()
		{
			return typeof(Cursors).GetProperties(BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Obtém uma coleção de valores de cursor padrão, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <returns>Uma coleção que contém um conjunto padrão de valores válidos.</returns>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003B7C4 File Offset: 0x0003ABC4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._standardValues == null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					object[] index = null;
					arrayList.Add(propertyInfo.GetValue(null, index));
				}
				this._standardValues = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this._standardValues;
		}

		/// <summary>Determina se este objeto dá suporte a um conjunto padrão de valores que podem ser escolhidos em uma lista, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <returns>Sempre retorna <see langword="true" />.</returns>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003B824 File Offset: 0x0003AC24
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Input.Cursor" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <returns>O objeto convertido ou <see langword="null" />, se <paramref name="value" /> for uma cadeia de caracteres vazia.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> não pode ser convertido</exception>
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003B834 File Offset: 0x0003AC34
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (!(text != string.Empty))
				{
					return null;
				}
				if (text.LastIndexOf(".", StringComparison.Ordinal) == -1)
				{
					switch ((CursorType)Enum.Parse(typeof(CursorType), text))
					{
					case CursorType.None:
						return Cursors.None;
					case CursorType.No:
						return Cursors.No;
					case CursorType.Arrow:
						return Cursors.Arrow;
					case CursorType.AppStarting:
						return Cursors.AppStarting;
					case CursorType.Cross:
						return Cursors.Cross;
					case CursorType.Help:
						return Cursors.Help;
					case CursorType.IBeam:
						return Cursors.IBeam;
					case CursorType.SizeAll:
						return Cursors.SizeAll;
					case CursorType.SizeNESW:
						return Cursors.SizeNESW;
					case CursorType.SizeNS:
						return Cursors.SizeNS;
					case CursorType.SizeNWSE:
						return Cursors.SizeNWSE;
					case CursorType.SizeWE:
						return Cursors.SizeWE;
					case CursorType.UpArrow:
						return Cursors.UpArrow;
					case CursorType.Wait:
						return Cursors.Wait;
					case CursorType.Hand:
						return Cursors.Hand;
					case CursorType.Pen:
						return Cursors.Pen;
					case CursorType.ScrollNS:
						return Cursors.ScrollNS;
					case CursorType.ScrollWE:
						return Cursors.ScrollWE;
					case CursorType.ScrollAll:
						return Cursors.ScrollAll;
					case CursorType.ScrollN:
						return Cursors.ScrollN;
					case CursorType.ScrollS:
						return Cursors.ScrollS;
					case CursorType.ScrollW:
						return Cursors.ScrollW;
					case CursorType.ScrollE:
						return Cursors.ScrollE;
					case CursorType.ScrollNW:
						return Cursors.ScrollNW;
					case CursorType.ScrollNE:
						return Cursors.ScrollNE;
					case CursorType.ScrollSW:
						return Cursors.ScrollSW;
					case CursorType.ScrollSE:
						return Cursors.ScrollSE;
					case CursorType.ArrowCD:
						return Cursors.ArrowCD;
					}
				}
				else if (text.EndsWith(".cur", StringComparison.OrdinalIgnoreCase) || text.EndsWith(".ani", StringComparison.OrdinalIgnoreCase))
				{
					UriHolder uriFromUriContext = TypeConverterHelper.GetUriFromUriContext(context, text);
					Uri resolvedUri = BindUriHelper.GetResolvedUri(uriFromUriContext.BaseUri, uriFromUriContext.OriginalUri);
					if (resolvedUri.IsAbsoluteUri && resolvedUri.IsFile)
					{
						return new Cursor(resolvedUri.LocalPath);
					}
					WebRequest request = WpfWebRequestHelper.CreateRequest(resolvedUri);
					WpfWebRequestHelper.ConfigCachePolicy(request, false);
					return new Cursor(WpfWebRequestHelper.GetResponseStream(request));
				}
			}
			throw base.GetConvertFromException(value);
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Input.Cursor" /> no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter o objeto.</param>
		/// <returns>O objeto convertido ou uma cadeia de caracteres vazia, se <paramref name="value" /> for <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="source" /> não pode ser convertido.</exception>
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003BA2C File Offset: 0x0003AE2C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				throw base.GetConvertToException(value, destinationType);
			}
			Cursor cursor = value as Cursor;
			if (cursor != null)
			{
				return cursor.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04000891 RID: 2193
		private TypeConverter.StandardValuesCollection _standardValues;
	}
}
