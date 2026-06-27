using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Windows.Ink;

namespace System.Windows
{
	/// <summary>Converte um <see cref="T:System.Windows.Ink.StrokeCollection" /> em uma cadeia de caracteres.</summary>
	// Token: 0x020001FA RID: 506
	public class StrokeCollectionConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.StrokeCollectionConverter" /> pode converter um objeto de um tipo especificado em um <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece o contexto de formato.</param>
		/// <param name="sourceType">O <see cref="T:System.Type" /> do qual converter.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.StrokeCollectionConverter" /> puder converter um objeto do tipo <paramref name="sourceType" /> em um <see cref="T:System.Windows.Ink.StrokeCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00031C44 File Offset: 0x00031044
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.StrokeCollectionConverter" /> pode converter um <see cref="T:System.Windows.Ink.StrokeCollection" /> no tipo especificado.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece o contexto de formato.</param>
		/// <param name="destinationType">O <see cref="T:System.Type" /> para o qual converter.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.StrokeCollectionConverter" /> puder converter um <see cref="T:System.Windows.Ink.StrokeCollection" /> no <paramref name="sourceType" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00031C70 File Offset: 0x00031070
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado como a cultura atual.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser convertido.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> convertido de <paramref name="value" />.</returns>
		// Token: 0x06000D30 RID: 3376 RVA: 0x00031C9C File Offset: 0x0003109C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text.Length != 0)
				{
					using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
					{
						return new StrokeCollection(memoryStream);
					}
				}
				return new StrokeCollection();
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte um <see cref="T:System.Windows.Ink.StrokeCollection" /> em uma cadeia de caracteres.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado como a cultura atual.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser convertido.</param>
		/// <param name="destinationType">O <see cref="T:System.Type" /> para o qual converter.</param>
		/// <returns>Um objeto que representa o <see cref="T:System.Windows.Ink.StrokeCollection" /> especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00031D10 File Offset: 0x00031110
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			StrokeCollection strokeCollection = value as StrokeCollection;
			if (strokeCollection != null)
			{
				if (destinationType == typeof(string))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						strokeCollection.Save(memoryStream, true);
						memoryStream.Position = 0L;
						return Convert.ToBase64String(memoryStream.ToArray());
					}
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(StrokeCollection).GetConstructor(new Type[]
					{
						typeof(Stream)
					});
					MemoryStream memoryStream2 = new MemoryStream();
					strokeCollection.Save(memoryStream2, true);
					memoryStream2.Position = 0L;
					return new InstanceDescriptor(constructor, new object[]
					{
						memoryStream2
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Retorna se este objeto é compatível com um conjunto padrão de valores que podem ser escolhidos em uma lista, usando o contexto especificado.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <returns>
		///   <see langword="false" /> em todos os casos.</returns>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00031E08 File Offset: 0x00031208
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
