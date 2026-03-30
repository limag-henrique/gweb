using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Globalization.CultureInfo" /> de e para outros tipos de dados.</summary>
	// Token: 0x02000192 RID: 402
	public class CultureInfoIetfLanguageTagConverter : TypeConverter
	{
		/// <summary>Determina se <see cref="T:System.Windows.CultureInfoIetfLanguageTagConverter" /> pode converter de um determinado tipo.</summary>
		/// <param name="typeDescriptorContext">O valor <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</param>
		/// <param name="sourceType">O <see cref="T:System.Type" /> sendo consultado quanto a suporte para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <see cref="T:System.Windows.CultureInfoIetfLanguageTagConverter" /> puder converter, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000583 RID: 1411 RVA: 0x00019DF0 File Offset: 0x000191F0
		public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se <see cref="T:System.Windows.CultureInfoIetfLanguageTagConverter" /> pode converter para um determinado tipo.</summary>
		/// <param name="typeDescriptorContext">O valor <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</param>
		/// <param name="destinationType">O <see cref="T:System.Type" /> sendo consultado quanto a suporte para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <see cref="T:System.Windows.CultureInfoIetfLanguageTagConverter" /> puder converter, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000584 RID: 1412 RVA: 0x00019E10 File Offset: 0x00019210
		public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Converte o tipo de objeto determinado para um objeto <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="typeDescriptorContext">O valor <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</param>
		/// <param name="cultureInfo">O objeto <see cref="T:System.Globalization.CultureInfo" /> cujo valor é respeitado durante a conversão.</param>
		/// <param name="source">O <see cref="T:System.Type" /> que está sendo convertido.</param>
		/// <returns>Um objeto <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06000585 RID: 1413 RVA: 0x00019E44 File Offset: 0x00019244
		public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object source)
		{
			string text = source as string;
			if (text != null)
			{
				return CultureInfo.GetCultureInfoByIetfLanguageTag(text);
			}
			throw base.GetConvertFromException(source);
		}

		/// <summary>Converte um objeto <see cref="T:System.Globalization.CultureInfo" /> para um determinado tipo de objeto.</summary>
		/// <param name="typeDescriptorContext">O valor <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</param>
		/// <param name="cultureInfo">O objeto <see cref="T:System.Globalization.CultureInfo" /> cujo valor é respeitado durante a conversão.</param>
		/// <param name="value">O objeto que representa o <see cref="T:System.Globalization.CultureInfo" /> a ser convertido.</param>
		/// <param name="destinationType">O <see cref="T:System.Type" /> do objeto convertido retornado.</param>
		/// <returns>Um objeto CultureInfo convertido para um determinado tipo de objeto.</returns>
		// Token: 0x06000586 RID: 1414 RVA: 0x00019E6C File Offset: 0x0001926C
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			CultureInfo cultureInfo2 = value as CultureInfo;
			if (cultureInfo2 != null)
			{
				if (destinationType == typeof(string))
				{
					return cultureInfo2.IetfLanguageTag;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					MethodInfo method = typeof(CultureInfo).GetMethod("GetCultureInfo", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, new Type[]
					{
						typeof(string)
					}, null);
					return new InstanceDescriptor(method, new object[]
					{
						cultureInfo2.Name
					});
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}
	}
}
