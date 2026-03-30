using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Windows.Media.Animation;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.Media.Animation.KeyTime" /> para e de outros tipos.</summary>
	// Token: 0x020001FC RID: 508
	public class KeyTimeConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <param name="typeDescriptorContext">Informações contextuais necessárias para conversão.</param>
		/// <param name="type">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este tipo puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00032084 File Offset: 0x00031484
		public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type type)
		{
			return type == typeof(string) || base.CanConvertFrom(typeDescriptorContext, type);
		}

		/// <summary>Determina se um tipo determinado pode ser convertido em uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <param name="typeDescriptorContext">Informações contextuais necessárias para conversão.</param>
		/// <param name="type">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este tipo puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D39 RID: 3385 RVA: 0x000320B0 File Offset: 0x000314B0
		public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type type)
		{
			return type == typeof(InstanceDescriptor) || type == typeof(string) || base.CanConvertTo(typeDescriptorContext, type);
		}

		/// <summary>Tenta converter um objeto fornecido em uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <param name="typeDescriptorContext">Informações de contexto necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O objeto sendo convertido em uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.KeyTime" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06000D3A RID: 3386 RVA: 0x000320EC File Offset: 0x000314EC
		public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(typeDescriptorContext, cultureInfo, value);
			}
			text = text.Trim();
			if (text == "Uniform")
			{
				return KeyTime.Uniform;
			}
			if (text == "Paced")
			{
				return KeyTime.Paced;
			}
			if (text[text.Length - 1] != KeyTimeConverter._percentCharacter[0])
			{
				TimeSpan timeSpan = (TimeSpan)TypeDescriptor.GetConverter(typeof(TimeSpan)).ConvertFrom(typeDescriptorContext, cultureInfo, text);
				return KeyTime.FromTimeSpan(timeSpan);
			}
			text = text.TrimEnd(KeyTimeConverter._percentCharacter);
			double num = (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFrom(typeDescriptorContext, cultureInfo, text);
			if (num == 0.0)
			{
				return KeyTime.FromPercent(0.0);
			}
			if (num == 100.0)
			{
				return KeyTime.FromPercent(1.0);
			}
			return KeyTime.FromPercent(num / 100.0);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" /> em outro tipo.</summary>
		/// <param name="typeDescriptorContext">Informações de contexto necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Animation.KeyTime" /> do qual converter.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>Um novo objeto baseado em <paramref name="value" />.</returns>
		// Token: 0x06000D3B RID: 3387 RVA: 0x00032208 File Offset: 0x00031608
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (value != null && value is KeyTime)
			{
				KeyTime keyTime = (KeyTime)value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					switch (keyTime.Type)
					{
					case KeyTimeType.Uniform:
					{
						MemberInfo member = typeof(KeyTime).GetProperty("Uniform");
						return new InstanceDescriptor(member, null);
					}
					case KeyTimeType.Percent:
					{
						MemberInfo member = typeof(KeyTime).GetMethod("FromPercent", new Type[]
						{
							typeof(double)
						});
						return new InstanceDescriptor(member, new object[]
						{
							keyTime.Percent
						});
					}
					case KeyTimeType.TimeSpan:
					{
						MemberInfo member = typeof(KeyTime).GetMethod("FromTimeSpan", new Type[]
						{
							typeof(TimeSpan)
						});
						return new InstanceDescriptor(member, new object[]
						{
							keyTime.TimeSpan
						});
					}
					case KeyTimeType.Paced:
					{
						MemberInfo member = typeof(KeyTime).GetProperty("Paced");
						return new InstanceDescriptor(member, null);
					}
					}
				}
				else if (destinationType == typeof(string))
				{
					switch (keyTime.Type)
					{
					case KeyTimeType.Uniform:
						return "Uniform";
					case KeyTimeType.Percent:
					{
						string str = (string)TypeDescriptor.GetConverter(typeof(double)).ConvertTo(typeDescriptorContext, cultureInfo, keyTime.Percent * 100.0, destinationType);
						return str + KeyTimeConverter._percentCharacter[0].ToString();
					}
					case KeyTimeType.TimeSpan:
						return TypeDescriptor.GetConverter(typeof(TimeSpan)).ConvertTo(typeDescriptorContext, cultureInfo, keyTime.TimeSpan, destinationType);
					case KeyTimeType.Paced:
						return "Paced";
					}
				}
			}
			return base.ConvertTo(typeDescriptorContext, cultureInfo, value, destinationType);
		}

		// Token: 0x040007FA RID: 2042
		private static char[] _percentCharacter = new char[]
		{
			'%'
		};
	}
}
