using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.Duration" /> para e de outras representações de tipo.</summary>
	// Token: 0x020001AD RID: 429
	public class DurationConverter : TypeConverter
	{
		/// <summary>Determina se a conversão de determinado tipo em uma instância de <see cref="T:System.Windows.Duration" /> é possível.</summary>
		/// <param name="td">Informações de contexto usadas para conversão.</param>
		/// <param name="t">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="t" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000696 RID: 1686 RVA: 0x0001E1F8 File Offset: 0x0001D5F8
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se é possível realizar a conversão para um tipo especificado.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000697 RID: 1687 RVA: 0x0001E21C File Offset: 0x0001D61C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Converte um valor de cadeia de caracteres fornecido em uma instância de <see cref="T:System.Windows.Duration" />.</summary>
		/// <param name="td">Informações de contexto usadas para conversão.</param>
		/// <param name="cultureInfo">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O valor da cadeia de caracteres a ser convertida em uma instância de <see cref="T:System.Windows.Duration" />.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Duration" />.</returns>
		// Token: 0x06000698 RID: 1688 RVA: 0x0001E250 File Offset: 0x0001D650
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo cultureInfo, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text == "Automatic")
				{
					return Duration.Automatic;
				}
				if (text == "Forever")
				{
					return Duration.Forever;
				}
			}
			TimeSpan timeSpan = TimeSpan.Zero;
			if (DurationConverter._timeSpanConverter == null)
			{
				DurationConverter._timeSpanConverter = new TimeSpanConverter();
			}
			timeSpan = (TimeSpan)DurationConverter._timeSpanConverter.ConvertFrom(td, cultureInfo, value);
			return new Duration(timeSpan);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Duration" /> em outro tipo.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="cultureInfo">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">Valor de duração do qual converter.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>Uma nova instância do <paramref name="destinationType" />.</returns>
		// Token: 0x06000699 RID: 1689 RVA: 0x0001E2D4 File Offset: 0x0001D6D4
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (destinationType != null && value is Duration)
			{
				Duration t = (Duration)value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					MemberInfo member;
					if (t.HasTimeSpan)
					{
						member = typeof(Duration).GetConstructor(new Type[]
						{
							typeof(TimeSpan)
						});
						return new InstanceDescriptor(member, new object[]
						{
							t.TimeSpan
						});
					}
					if (t == Duration.Forever)
					{
						member = typeof(Duration).GetProperty("Forever");
						return new InstanceDescriptor(member, null);
					}
					member = typeof(Duration).GetProperty("Automatic");
					return new InstanceDescriptor(member, null);
				}
				else if (destinationType == typeof(string))
				{
					return t.ToString();
				}
			}
			return base.ConvertTo(context, cultureInfo, value, destinationType);
		}

		// Token: 0x040005A2 RID: 1442
		private static TimeSpanConverter _timeSpanConverter;
	}
}
