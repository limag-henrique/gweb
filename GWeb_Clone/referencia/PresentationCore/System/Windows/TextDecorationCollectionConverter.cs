using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.TextDecorationCollection" /> de outros tipos de dados.</summary>
	// Token: 0x020001F3 RID: 499
	public sealed class TextDecorationCollectionConverter : TypeConverter
	{
		/// <summary>Determina se uma instância de <see cref="T:System.Windows.TextDecorationCollection" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="false" /> sempre é retornado porque a <see cref="T:System.Windows.TextDecorationCollection" /> não pode ser convertida em outro tipo.</returns>
		// Token: 0x06000D18 RID: 3352 RVA: 0x000317D4 File Offset: 0x00030BD4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor);
		}

		/// <summary>Retorna um valor que indica se esse conversor pode converter um objeto do tipo especificado em uma instância de <see cref="T:System.Windows.TextDecorationCollection" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.TextDecorationCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D19 RID: 3353 RVA: 0x000317F8 File Offset: 0x00030BF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.TextDecorationCollection" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="input">O objeto sendo convertido.</param>
		/// <returns>A instância de <see cref="T:System.Windows.FontWeight" /> criada com base na <paramref name="input" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorre se <paramref name="input" /> é <see langword="null" /> ou não é um tipo válido para conversão.</exception>
		// Token: 0x06000D1A RID: 3354 RVA: 0x0003181C File Offset: 0x00030C1C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object input)
		{
			if (input == null)
			{
				throw base.GetConvertFromException(input);
			}
			string text = input as string;
			if (text == null)
			{
				throw new ArgumentException(SR.Get("General_BadType", new object[]
				{
					"ConvertFrom"
				}), "input");
			}
			return TextDecorationCollectionConverter.ConvertFromString(text);
		}

		/// <summary>Tenta converter uma cadeia de caracteres especificada em uma instância de <see cref="T:System.Windows.TextDecorationCollection" />.</summary>
		/// <param name="text">A <see cref="T:System.String" /> a ser convertida no objeto <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		/// <returns>A instância de <see cref="T:System.Windows.TextDecorationCollection" /> criada com base na <paramref name="text" /> convertida.</returns>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00031868 File Offset: 0x00030C68
		public static TextDecorationCollection ConvertFromString(string text)
		{
			if (text == null)
			{
				return null;
			}
			TextDecorationCollection textDecorationCollection = new TextDecorationCollection();
			byte b = 0;
			int num = TextDecorationCollectionConverter.AdvanceToNextNonWhiteSpace(text, 0);
			while (num >= 0 && num < text.Length)
			{
				if (TextDecorationCollectionConverter.Match("NONE", text, num))
				{
					num = TextDecorationCollectionConverter.AdvanceToNextNonWhiteSpace(text, num + "NONE".Length);
					if (textDecorationCollection.Count > 0 || num < text.Length)
					{
						num = -1;
					}
				}
				else
				{
					int num2 = 0;
					while (num2 < TextDecorationCollectionConverter.TextDecorationNames.Length && !TextDecorationCollectionConverter.Match(TextDecorationCollectionConverter.TextDecorationNames[num2], text, num))
					{
						num2++;
					}
					if (num2 < TextDecorationCollectionConverter.TextDecorationNames.Length)
					{
						if (((int)b & 1 << num2) > 0)
						{
							num = -1;
						}
						else
						{
							textDecorationCollection.Add(TextDecorationCollectionConverter.PredefinedTextDecorations[num2]);
							b |= (byte)(1 << num2);
							num = TextDecorationCollectionConverter.AdvanceToNextNameStart(text, num + TextDecorationCollectionConverter.TextDecorationNames[num2].Length);
						}
					}
					else
					{
						num = -1;
					}
				}
			}
			if (num < 0)
			{
				throw new ArgumentException(SR.Get("InvalidTextDecorationCollectionString", new object[]
				{
					text
				}));
			}
			return textDecorationCollection;
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.TextDecorationCollection" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">A instância de <see cref="T:System.Windows.TextDecorationCollection" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo para o qual esta instância de <see cref="T:System.Windows.TextDecorationCollection" /> é convertida.</param>
		/// <returns>
		///   <see langword="null" /> sempre é retornado porque <see cref="T:System.Windows.TextDecorationCollection" /> não pode ser convertida em qualquer outro tipo.</returns>
		// Token: 0x06000D1C RID: 3356 RVA: 0x00031964 File Offset: 0x00030D64
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is IEnumerable<TextDecoration>)
			{
				ConstructorInfo constructor = typeof(TextDecorationCollection).GetConstructor(new Type[]
				{
					typeof(IEnumerable<TextDecoration>)
				});
				return new InstanceDescriptor(constructor, new object[]
				{
					value
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000319CC File Offset: 0x00030DCC
		private static bool Match(string pattern, string input, int index)
		{
			int num = 0;
			while (num < pattern.Length && index + num < input.Length && pattern[num] == char.ToUpperInvariant(input[index + num]))
			{
				num++;
			}
			return num == pattern.Length;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00031A18 File Offset: 0x00030E18
		private static int AdvanceToNextNameStart(string input, int index)
		{
			int num = TextDecorationCollectionConverter.AdvanceToNextNonWhiteSpace(input, index);
			int num2;
			if (num >= input.Length)
			{
				num2 = input.Length;
			}
			else if (input[num] == ',')
			{
				num2 = TextDecorationCollectionConverter.AdvanceToNextNonWhiteSpace(input, num + 1);
				if (num2 >= input.Length)
				{
					num2 = -1;
				}
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00031A64 File Offset: 0x00030E64
		private static int AdvanceToNextNonWhiteSpace(string input, int index)
		{
			while (index < input.Length && char.IsWhiteSpace(input[index]))
			{
				index++;
			}
			if (index <= input.Length)
			{
				return index;
			}
			return input.Length;
		}

		// Token: 0x040007C6 RID: 1990
		private const string None = "NONE";

		// Token: 0x040007C7 RID: 1991
		private const char Separator = ',';

		// Token: 0x040007C8 RID: 1992
		private static readonly string[] TextDecorationNames = new string[]
		{
			"OVERLINE",
			"BASELINE",
			"UNDERLINE",
			"STRIKETHROUGH"
		};

		// Token: 0x040007C9 RID: 1993
		private static readonly TextDecorationCollection[] PredefinedTextDecorations = new TextDecorationCollection[]
		{
			TextDecorations.OverLine,
			TextDecorations.Baseline,
			TextDecorations.Underline,
			TextDecorations.Strikethrough
		};
	}
}
