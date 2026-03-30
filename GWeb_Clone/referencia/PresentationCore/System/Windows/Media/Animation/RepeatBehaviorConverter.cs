using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Windows.Media.Animation
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> de e para outros tipos de dados.</summary>
	// Token: 0x02000578 RID: 1400
	public sealed class RepeatBehaviorConverter : TypeConverter
	{
		/// <summary>Determina se a conversão de um tipo de dados especificado é possível.</summary>
		/// <param name="td">Informações de contexto necessárias para conversão.</param>
		/// <param name="t">O tipo a ser avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se há suporte para a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040D8 RID: 16600 RVA: 0x000FE310 File Offset: 0x000FD710
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se é possível realizar a conversão para um tipo especificado.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="destinationType">Tipo de destino sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se a conversão for possível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040D9 RID: 16601 RVA: 0x000FE334 File Offset: 0x000FD734
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Converte um valor de cadeia de caracteres fornecido em uma instância de <see cref="T:System.Windows.Media.Animation.RepeatBehaviorConverter" />.</summary>
		/// <param name="td">Informações de contexto necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">Objeto sendo avaliado para conversão.</param>
		/// <returns>Um novo objeto <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> baseado em <paramref name="value" />.</returns>
		// Token: 0x060040DA RID: 16602 RVA: 0x000FE368 File Offset: 0x000FD768
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo cultureInfo, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text == "Forever")
				{
					return RepeatBehavior.Forever;
				}
				if (text.Length > 0 && text[text.Length - 1] == RepeatBehaviorConverter._iterationCharacter[0])
				{
					string value2 = text.TrimEnd(RepeatBehaviorConverter._iterationCharacter);
					double count = (double)TypeDescriptor.GetConverter(typeof(double)).ConvertFrom(td, cultureInfo, value2);
					return new RepeatBehavior(count);
				}
			}
			TimeSpan duration = (TimeSpan)TypeDescriptor.GetConverter(typeof(TimeSpan)).ConvertFrom(td, cultureInfo, text);
			return new RepeatBehavior(duration);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> para um tipo de destino compatível.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">Objeto sendo avaliado para conversão.</param>
		/// <param name="destinationType">Tipo de destino sendo avaliado para conversão.</param>
		/// <returns>Os únicos tipos de destino compatíveis são <see cref="T:System.String" /> e <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />.</returns>
		// Token: 0x060040DB RID: 16603 RVA: 0x000FE420 File Offset: 0x000FD820
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (value is RepeatBehavior && destinationType != null)
			{
				RepeatBehavior repeatBehavior = (RepeatBehavior)value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					if (repeatBehavior == RepeatBehavior.Forever)
					{
						MemberInfo member = typeof(RepeatBehavior).GetProperty("Forever");
						return new InstanceDescriptor(member, null);
					}
					if (repeatBehavior.HasCount)
					{
						MemberInfo member = typeof(RepeatBehavior).GetConstructor(new Type[]
						{
							typeof(double)
						});
						return new InstanceDescriptor(member, new object[]
						{
							repeatBehavior.Count
						});
					}
					if (repeatBehavior.HasDuration)
					{
						MemberInfo member = typeof(RepeatBehavior).GetConstructor(new Type[]
						{
							typeof(TimeSpan)
						});
						return new InstanceDescriptor(member, new object[]
						{
							repeatBehavior.Duration
						});
					}
				}
				else if (destinationType == typeof(string))
				{
					return repeatBehavior.InternalToString(null, cultureInfo);
				}
			}
			return base.ConvertTo(context, cultureInfo, value, destinationType);
		}

		// Token: 0x040017B9 RID: 6073
		private static char[] _iterationCharacter = new char[]
		{
			'x'
		};
	}
}
