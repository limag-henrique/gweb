using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Net.Cache;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Analisa um <see cref="T:System.Net.Cache.RequestCachePolicy" />.</summary>
	// Token: 0x02000438 RID: 1080
	public sealed class RequestCachePolicyConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se esse conversor pode converter um objeto do tipo especificado no tipo desse conversor, usando o contexto especificado.</summary>
		/// <param name="td">O contexto de formato.</param>
		/// <param name="t">O tipo do qual converter.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002C32 RID: 11314 RVA: 0x000B08C8 File Offset: 0x000AFCC8
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Retorna um valor que indica se esse conversor pode converter o objeto para o tipo especificado, usando o contexto especificado.</summary>
		/// <param name="typeDescriptorContext">O contexto de formato.</param>
		/// <param name="destinationType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002C33 RID: 11315 RVA: 0x000B08EC File Offset: 0x000AFCEC
		public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Converte o objeto especificado em um objeto <see cref="T:System.Net.Cache.RequestCachePolicy" />.</summary>
		/// <param name="td">O contexto de formato.</param>
		/// <param name="ci">A cultura atual.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <returns>Um objeto que representa o valor convertido.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é de um tipo válido.</exception>
		// Token: 0x06002C34 RID: 11316 RVA: 0x000B0920 File Offset: 0x000AFD20
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo ci, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(SR.Get("General_BadType", new object[]
				{
					"ConvertFrom"
				}), "value");
			}
			HttpRequestCacheLevel level = (HttpRequestCacheLevel)Enum.Parse(typeof(HttpRequestCacheLevel), text, true);
			return new HttpRequestCachePolicy(level);
		}

		/// <summary>Converte este objeto no tipo especificado.</summary>
		/// <param name="typeDescriptorContext">O contexto de formato.</param>
		/// <param name="cultureInfo">A cultura a ser usada para a conversão.</param>
		/// <param name="value">A política a converter.</param>
		/// <param name="destinationType">O tipo para o qual converter.</param>
		/// <returns>O objeto que é construído da conversão.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		// Token: 0x06002C35 RID: 11317 RVA: 0x000B0984 File Offset: 0x000AFD84
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			HttpRequestCachePolicy httpRequestCachePolicy = value as HttpRequestCachePolicy;
			if (httpRequestCachePolicy != null)
			{
				if (destinationType == typeof(string))
				{
					return httpRequestCachePolicy.Level.ToString();
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(HttpRequestCachePolicy).GetConstructor(new Type[]
					{
						typeof(HttpRequestCachePolicy)
					});
					return new InstanceDescriptor(constructor, new object[]
					{
						httpRequestCachePolicy.Level
					});
				}
			}
			RequestCachePolicy requestCachePolicy = value as RequestCachePolicy;
			if (requestCachePolicy != null)
			{
				if (destinationType == typeof(string))
				{
					return requestCachePolicy.Level.ToString();
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor2 = typeof(RequestCachePolicy).GetConstructor(new Type[]
					{
						typeof(RequestCachePolicy)
					});
					return new InstanceDescriptor(constructor2, new object[]
					{
						requestCachePolicy.Level
					});
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}
	}
}
