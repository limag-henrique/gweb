using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
	// Token: 0x020005C8 RID: 1480
	public class PathFigureCollectionValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004339 RID: 17209 RVA: 0x00104BA4 File Offset: 0x00103FA4
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.PathFigureCollection" />.</exception>
		// Token: 0x0600433A RID: 17210 RVA: 0x00104BB4 File Offset: 0x00103FB4
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is PathFigureCollection))
			{
				return false;
			}
			PathFigureCollection pathFigureCollection = (PathFigureCollection)value;
			return pathFigureCollection.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x0600433B RID: 17211 RVA: 0x00104BD8 File Offset: 0x00103FD8
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return PathFigureCollection.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> em um <see cref="T:System.String" /></summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.PathFigureCollection" /> fornecido.</returns>
		// Token: 0x0600433C RID: 17212 RVA: 0x00104BF8 File Offset: 0x00103FF8
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is PathFigureCollection))
			{
				return base.ConvertToString(value, context);
			}
			PathFigureCollection pathFigureCollection = (PathFigureCollection)value;
			if (!pathFigureCollection.CanSerializeToString())
			{
				return base.ConvertToString(value, context);
			}
			return pathFigureCollection.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
		}
	}
}
