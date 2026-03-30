using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Descreve uma cor em termos de canais alfa, vermelho, verde e azul.</summary>
	// Token: 0x0200036E RID: 878
	[TypeConverter(typeof(ColorConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public struct Color : IFormattable, IEquatable<Color>
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x0007E800 File Offset: 0x0007DC00
		private static Color FromProfile(Uri profileUri)
		{
			Color color = default(Color);
			color.context = new ColorContext(profileUri);
			color.scRgbColor.a = 1f;
			color.scRgbColor.r = 0f;
			color.scRgbColor.g = 0f;
			color.scRgbColor.b = 0f;
			color.sRgbColor.a = byte.MaxValue;
			color.sRgbColor.r = 0;
			color.sRgbColor.g = 0;
			color.sRgbColor.b = 0;
			if (color.context != null)
			{
				color.nativeColorValue = new float[color.context.NumChannels];
				for (int i = 0; i < color.nativeColorValue.GetLength(0); i++)
				{
					color.nativeColorValue[i] = 0f;
				}
			}
			color.isFromScRgb = false;
			return color;
		}

		/// <summary>Cria uma nova estrutura <see cref="T:System.Windows.Media.Color" /> usando o canal alfa, os valores de canal de cor e o perfil de cor especificados.</summary>
		/// <param name="a">O canal alfa da nova cor, um valor entre 0 e 1.</param>
		/// <param name="values">Uma coleção de valores que especificam os canais de cor da nova cor. Esses valores são mapeados para o <paramref name="profileUri" />.</param>
		/// <param name="profileUri">O perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) da nova cor.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Color" /> com os valores especificados.</returns>
		// Token: 0x06001F40 RID: 8000 RVA: 0x0007E8F0 File Offset: 0x0007DCF0
		public static Color FromAValues(float a, float[] values, Uri profileUri)
		{
			Color color = Color.FromProfile(profileUri);
			if (values == null)
			{
				throw new ArgumentException(SR.Get("Color_DimensionMismatch", null));
			}
			if (values.GetLength(0) != color.nativeColorValue.GetLength(0))
			{
				throw new ArgumentException(SR.Get("Color_DimensionMismatch", null));
			}
			for (int i = 0; i < values.GetLength(0); i++)
			{
				color.nativeColorValue[i] = values[i];
			}
			color.ComputeScRgbValues();
			color.scRgbColor.a = a;
			if (a < 0f)
			{
				a = 0f;
			}
			else if (a > 1f)
			{
				a = 1f;
			}
			color.sRgbColor.a = (byte)(a * 255f + 0.5f);
			color.sRgbColor.r = Color.ScRgbTosRgb(color.scRgbColor.r);
			color.sRgbColor.g = Color.ScRgbTosRgb(color.scRgbColor.g);
			color.sRgbColor.b = Color.ScRgbTosRgb(color.scRgbColor.b);
			return color;
		}

		/// <summary>Cria uma nova estrutura <see cref="T:System.Windows.Media.Color" /> usando os valores de canal de cor e o perfil de cor especificados.</summary>
		/// <param name="values">Uma coleção de valores que especificam os canais de cor da nova cor. Esses valores são mapeados para o <paramref name="profileUri" />.</param>
		/// <param name="profileUri">O perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) da nova cor.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Color" /> com os valores especificados e um valor de canal alfa de 1.</returns>
		// Token: 0x06001F41 RID: 8001 RVA: 0x0007EA00 File Offset: 0x0007DE00
		public static Color FromValues(float[] values, Uri profileUri)
		{
			return Color.FromAValues(1f, values, profileUri);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0007EA1C File Offset: 0x0007DE1C
		internal static Color FromUInt32(uint argb)
		{
			Color color = default(Color);
			color.sRgbColor.a = (byte)((argb & 4278190080U) >> 24);
			color.sRgbColor.r = (byte)((argb & 16711680U) >> 16);
			color.sRgbColor.g = (byte)((argb & 65280U) >> 8);
			color.sRgbColor.b = (byte)(argb & 255U);
			color.scRgbColor.a = (float)color.sRgbColor.a / 255f;
			color.scRgbColor.r = Color.sRgbToScRgb(color.sRgbColor.r);
			color.scRgbColor.g = Color.sRgbToScRgb(color.sRgbColor.g);
			color.scRgbColor.b = Color.sRgbToScRgb(color.sRgbColor.b);
			color.context = null;
			color.isFromScRgb = false;
			return color;
		}

		/// <summary>Cria uma nova estrutura <see cref="T:System.Windows.Media.Color" /> usando os valores de canal alfa e valores de canal de cor <see langword="ScRGB" /> especificados.</summary>
		/// <param name="a">O canal alfa de <see langword="ScRGB" />, <see cref="P:System.Windows.Media.Color.ScA" />, da nova cor.</param>
		/// <param name="r">O canal vermelho <see langword="ScRGB" />, <see cref="P:System.Windows.Media.Color.ScR" />, da nova cor.</param>
		/// <param name="g">O canal verde <see langword="ScRGB" />, <see cref="P:System.Windows.Media.Color.ScG" />, da nova cor.</param>
		/// <param name="b">O canal azul <see langword="ScRGB" />, <see cref="P:System.Windows.Media.Color.ScB" />, da nova cor.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Color" /> com os valores especificados.</returns>
		// Token: 0x06001F43 RID: 8003 RVA: 0x0007EB0C File Offset: 0x0007DF0C
		public static Color FromScRgb(float a, float r, float g, float b)
		{
			Color color = default(Color);
			color.scRgbColor.r = r;
			color.scRgbColor.g = g;
			color.scRgbColor.b = b;
			color.scRgbColor.a = a;
			if (a < 0f)
			{
				a = 0f;
			}
			else if (a > 1f)
			{
				a = 1f;
			}
			color.sRgbColor.a = (byte)(a * 255f + 0.5f);
			color.sRgbColor.r = Color.ScRgbTosRgb(color.scRgbColor.r);
			color.sRgbColor.g = Color.ScRgbTosRgb(color.scRgbColor.g);
			color.sRgbColor.b = Color.ScRgbTosRgb(color.scRgbColor.b);
			color.context = null;
			color.isFromScRgb = true;
			return color;
		}

		/// <summary>Cria uma nova estrutura <see cref="T:System.Windows.Media.Color" /> usando os valores de canal alfa e valores de canal de cor <see langword="sRGB" /> especificados.</summary>
		/// <param name="a">O canal alfa, <see cref="P:System.Windows.Media.Color.A" />, da nova cor.</param>
		/// <param name="r">O canal vermelho, <see cref="P:System.Windows.Media.Color.R" />, da nova cor.</param>
		/// <param name="g">O canal verde, <see cref="P:System.Windows.Media.Color.G" />, da nova cor.</param>
		/// <param name="b">O canal azul, <see cref="P:System.Windows.Media.Color.B" />, da nova cor.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Color" /> com os valores especificados.</returns>
		// Token: 0x06001F44 RID: 8004 RVA: 0x0007EBF4 File Offset: 0x0007DFF4
		public static Color FromArgb(byte a, byte r, byte g, byte b)
		{
			Color color = default(Color);
			color.scRgbColor.a = (float)a / 255f;
			color.scRgbColor.r = Color.sRgbToScRgb(r);
			color.scRgbColor.g = Color.sRgbToScRgb(g);
			color.scRgbColor.b = Color.sRgbToScRgb(b);
			color.context = null;
			color.sRgbColor.a = a;
			color.sRgbColor.r = Color.ScRgbTosRgb(color.scRgbColor.r);
			color.sRgbColor.g = Color.ScRgbTosRgb(color.scRgbColor.g);
			color.sRgbColor.b = Color.ScRgbTosRgb(color.scRgbColor.b);
			color.isFromScRgb = false;
			return color;
		}

		/// <summary>Cria uma nova estrutura <see cref="T:System.Windows.Media.Color" /> usando os valores de canal de cor <see langword="sRGB" /> especificados.</summary>
		/// <param name="r">O canal vermelho <see langword="sRGB" />, <see cref="P:System.Windows.Media.Color.R" />, da nova cor.</param>
		/// <param name="g">O canal verde <see langword="sRGB" />, <see cref="P:System.Windows.Media.Color.G" />, da nova cor.</param>
		/// <param name="b">O canal azul <see langword="sRGB" />, <see cref="P:System.Windows.Media.Color.B" />, da nova cor.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Color" /> com os valores especificados e um valor de canal alfa de 255.</returns>
		// Token: 0x06001F45 RID: 8005 RVA: 0x0007ECC8 File Offset: 0x0007E0C8
		public static Color FromRgb(byte r, byte g, byte b)
		{
			return Color.FromArgb(byte.MaxValue, r, g, b);
		}

		/// <summary>Obtém o código hash desta estrutura <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <returns>Um código hash desta estrutura <see cref="T:System.Windows.Media.Color" />.</returns>
		// Token: 0x06001F46 RID: 8006 RVA: 0x0007ECE4 File Offset: 0x0007E0E4
		public override int GetHashCode()
		{
			return this.scRgbColor.GetHashCode();
		}

		/// <summary>Cria uma representação de cadeia de caracteres da cor usando os canais <see langword="sRGB" />.</summary>
		/// <returns>A representação de cadeia de caracteres da cor. A implementação padrão representa os valores <see cref="T:System.Byte" /> em formato hexadecimal, prefixos com o caractere # e começa com o canal alfa. Por exemplo, o valor <see cref="M:System.Windows.Media.Color.ToString" /> para <see cref="P:System.Windows.Media.Colors.AliceBlue" /> é FFF0F8FF #.</returns>
		// Token: 0x06001F47 RID: 8007 RVA: 0x0007ED04 File Offset: 0x0007E104
		public override string ToString()
		{
			string format = this.isFromScRgb ? "R" : null;
			return this.ConvertToString(format, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres da cor usando os canais <see langword="sRGB" /> e o provedor de formato especificado.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>A representação de cadeia de caracteres da cor.</returns>
		// Token: 0x06001F48 RID: 8008 RVA: 0x0007ED2C File Offset: 0x0007E12C
		public string ToString(IFormatProvider provider)
		{
			string format = this.isFromScRgb ? "R" : null;
			return this.ConvertToString(format, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06001F49 RID: 8009 RVA: 0x0007ED54 File Offset: 0x0007E154
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0007ED6C File Offset: 0x0007E16C
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.context == null)
			{
				if (format == null)
				{
					stringBuilder.AppendFormat(provider, "#{0:X2}", new object[]
					{
						this.sRgbColor.a
					});
					stringBuilder.AppendFormat(provider, "{0:X2}", new object[]
					{
						this.sRgbColor.r
					});
					stringBuilder.AppendFormat(provider, "{0:X2}", new object[]
					{
						this.sRgbColor.g
					});
					stringBuilder.AppendFormat(provider, "{0:X2}", new object[]
					{
						this.sRgbColor.b
					});
				}
				else
				{
					char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
					stringBuilder.AppendFormat(provider, string.Concat(new string[]
					{
						"sc#{1:",
						format,
						"}{0} {2:",
						format,
						"}{0} {3:",
						format,
						"}{0} {4:",
						format,
						"}"
					}), new object[]
					{
						numericListSeparator,
						this.scRgbColor.a,
						this.scRgbColor.r,
						this.scRgbColor.g,
						this.scRgbColor.b
					});
				}
			}
			else
			{
				char numericListSeparator2 = TokenizerHelper.GetNumericListSeparator(provider);
				format = "R";
				Uri uri = new Uri(this.context.ProfileUri.GetComponents(UriComponents.SerializationInfoString, UriFormat.SafeUnescaped), this.context.ProfileUri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
				string components = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
				stringBuilder.AppendFormat(provider, "{0}{1} ", new object[]
				{
					"ContextColor ",
					components
				});
				stringBuilder.AppendFormat(provider, "{1:" + format + "}{0}", new object[]
				{
					numericListSeparator2,
					this.scRgbColor.a
				});
				for (int i = 0; i < this.nativeColorValue.GetLength(0); i++)
				{
					stringBuilder.AppendFormat(provider, "{0:" + format + "}", new object[]
					{
						this.nativeColorValue[i]
					});
					if (i < this.nativeColorValue.GetLength(0) - 1)
					{
						stringBuilder.AppendFormat(provider, "{0}", new object[]
						{
							numericListSeparator2
						});
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Compara a igualdade difusa de duas estruturas <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="color1">A primeira cor a ser comparada.</param>
		/// <param name="color2">A segunda cor a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="color1" /> e <paramref name="color2" /> são quase idênticas; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F4B RID: 8011 RVA: 0x0007F008 File Offset: 0x0007E408
		public static bool AreClose(Color color1, Color color2)
		{
			return color1.IsClose(color2);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0007F020 File Offset: 0x0007E420
		private bool IsClose(Color color)
		{
			bool flag = true;
			if (this.context == null || color.nativeColorValue == null)
			{
				flag = (flag && FloatUtil.AreClose(this.scRgbColor.r, color.scRgbColor.r));
				flag = (flag && FloatUtil.AreClose(this.scRgbColor.g, color.scRgbColor.g));
				flag = (flag && FloatUtil.AreClose(this.scRgbColor.b, color.scRgbColor.b));
			}
			else
			{
				for (int i = 0; i < color.nativeColorValue.GetLength(0); i++)
				{
					flag = (flag && FloatUtil.AreClose(this.nativeColorValue[i], color.nativeColorValue[i]));
				}
			}
			return flag && FloatUtil.AreClose(this.scRgbColor.a, color.scRgbColor.a);
		}

		/// <summary>Define os canais <see langword="ScRGB" /> da cor para dentro do intervalo de gama de 0 a 1, se estão fora dele.</summary>
		// Token: 0x06001F4D RID: 8013 RVA: 0x0007F100 File Offset: 0x0007E500
		public void Clamp()
		{
			this.scRgbColor.r = ((this.scRgbColor.r < 0f) ? 0f : ((this.scRgbColor.r > 1f) ? 1f : this.scRgbColor.r));
			this.scRgbColor.g = ((this.scRgbColor.g < 0f) ? 0f : ((this.scRgbColor.g > 1f) ? 1f : this.scRgbColor.g));
			this.scRgbColor.b = ((this.scRgbColor.b < 0f) ? 0f : ((this.scRgbColor.b > 1f) ? 1f : this.scRgbColor.b));
			this.scRgbColor.a = ((this.scRgbColor.a < 0f) ? 0f : ((this.scRgbColor.a > 1f) ? 1f : this.scRgbColor.a));
			this.sRgbColor.a = (byte)(this.scRgbColor.a * 255f);
			this.sRgbColor.r = Color.ScRgbTosRgb(this.scRgbColor.r);
			this.sRgbColor.g = Color.ScRgbTosRgb(this.scRgbColor.g);
			this.sRgbColor.b = Color.ScRgbTosRgb(this.scRgbColor.b);
		}

		/// <summary>Obtém os valores de canal de cor da cor.</summary>
		/// <returns>Uma matriz de valores de canal de cor.</returns>
		// Token: 0x06001F4E RID: 8014 RVA: 0x0007F29C File Offset: 0x0007E69C
		public float[] GetNativeColorValues()
		{
			if (this.context != null)
			{
				return (float[])this.nativeColorValue.Clone();
			}
			throw new InvalidOperationException(SR.Get("Color_NullColorContext", null));
		}

		/// <summary>Adiciona duas estruturas <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="color1">A primeira estrutura <see cref="T:System.Windows.Media.Color" /> a ser adicionada.</param>
		/// <param name="color2">A segunda estrutura <see cref="T:System.Windows.Media.Color" /> a ser adicionada.</param>
		/// <returns>Uma nova estrutura <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de adição.</returns>
		// Token: 0x06001F4F RID: 8015 RVA: 0x0007F2D8 File Offset: 0x0007E6D8
		public static Color operator +(Color color1, Color color2)
		{
			if (color1.context == null && color2.context == null)
			{
				return Color.FromScRgb(color1.scRgbColor.a + color2.scRgbColor.a, color1.scRgbColor.r + color2.scRgbColor.r, color1.scRgbColor.g + color2.scRgbColor.g, color1.scRgbColor.b + color2.scRgbColor.b);
			}
			if (color1.context == color2.context)
			{
				Color color3 = default(Color);
				color3.context = color1.context;
				color3.nativeColorValue = new float[color3.context.NumChannels];
				for (int i = 0; i < color3.nativeColorValue.GetLength(0); i++)
				{
					color3.nativeColorValue[i] = color1.nativeColorValue[i] + color2.nativeColorValue[i];
				}
				Color color4 = Color.FromRgb(0, 0, 0);
				color4.context = new ColorContext(PixelFormats.Bgra32);
				ColorTransform colorTransform = new ColorTransform(color3.context, color4.context);
				float[] array = new float[3];
				colorTransform.Translate(color3.nativeColorValue, array);
				if (array[0] < 0f)
				{
					color3.sRgbColor.r = 0;
				}
				else if (array[0] > 1f)
				{
					color3.sRgbColor.r = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.r = (byte)(array[0] * 255f + 0.5f);
				}
				if (array[1] < 0f)
				{
					color3.sRgbColor.g = 0;
				}
				else if (array[1] > 1f)
				{
					color3.sRgbColor.g = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.g = (byte)(array[1] * 255f + 0.5f);
				}
				if (array[2] < 0f)
				{
					color3.sRgbColor.b = 0;
				}
				else if (array[2] > 1f)
				{
					color3.sRgbColor.b = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.b = (byte)(array[2] * 255f + 0.5f);
				}
				color3.scRgbColor.r = Color.sRgbToScRgb(color3.sRgbColor.r);
				color3.scRgbColor.g = Color.sRgbToScRgb(color3.sRgbColor.g);
				color3.scRgbColor.b = Color.sRgbToScRgb(color3.sRgbColor.b);
				color3.scRgbColor.a = color1.scRgbColor.a + color2.scRgbColor.a;
				if (color3.scRgbColor.a < 0f)
				{
					color3.scRgbColor.a = 0f;
					color3.sRgbColor.a = 0;
				}
				else if (color3.scRgbColor.a > 1f)
				{
					color3.scRgbColor.a = 1f;
					color3.sRgbColor.a = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.a = (byte)(color3.scRgbColor.a * 255f + 0.5f);
				}
				return color3;
			}
			throw new ArgumentException(SR.Get("Color_ColorContextTypeMismatch", null));
		}

		/// <summary>Adiciona duas estruturas <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="color1">A primeira estrutura <see cref="T:System.Windows.Media.Color" /> a ser adicionada.</param>
		/// <param name="color2">A segunda estrutura <see cref="T:System.Windows.Media.Color" /> a ser adicionada.</param>
		/// <returns>Uma nova estrutura <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de adição.</returns>
		// Token: 0x06001F50 RID: 8016 RVA: 0x0007F62C File Offset: 0x0007EA2C
		public static Color Add(Color color1, Color color2)
		{
			return color1 + color2;
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Color" /> de uma estrutura <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="color1">A estrutura <see cref="T:System.Windows.Media.Color" /> da qual subtrair.</param>
		/// <param name="color2">A estrutura <see cref="T:System.Windows.Media.Color" /> para subtrair de <paramref name="color1" />.</param>
		/// <returns>Uma nova estrutura de <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de subtração.</returns>
		// Token: 0x06001F51 RID: 8017 RVA: 0x0007F640 File Offset: 0x0007EA40
		public static Color operator -(Color color1, Color color2)
		{
			if (color1.context == null && color2.context == null)
			{
				return Color.FromScRgb(color1.scRgbColor.a - color2.scRgbColor.a, color1.scRgbColor.r - color2.scRgbColor.r, color1.scRgbColor.g - color2.scRgbColor.g, color1.scRgbColor.b - color2.scRgbColor.b);
			}
			if (color1.context == null || color2.context == null)
			{
				throw new ArgumentException(SR.Get("Color_ColorContextTypeMismatch", null));
			}
			if (color1.context == color2.context)
			{
				Color color3 = default(Color);
				color3.context = color1.context;
				color3.nativeColorValue = new float[color3.context.NumChannels];
				for (int i = 0; i < color3.nativeColorValue.GetLength(0); i++)
				{
					color3.nativeColorValue[i] = color1.nativeColorValue[i] - color2.nativeColorValue[i];
				}
				Color color4 = Color.FromRgb(0, 0, 0);
				color4.context = new ColorContext(PixelFormats.Bgra32);
				ColorTransform colorTransform = new ColorTransform(color3.context, color4.context);
				float[] array = new float[3];
				colorTransform.Translate(color3.nativeColorValue, array);
				if (array[0] < 0f)
				{
					color3.sRgbColor.r = 0;
				}
				else if (array[0] > 1f)
				{
					color3.sRgbColor.r = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.r = (byte)(array[0] * 255f + 0.5f);
				}
				if (array[1] < 0f)
				{
					color3.sRgbColor.g = 0;
				}
				else if (array[1] > 1f)
				{
					color3.sRgbColor.g = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.g = (byte)(array[1] * 255f + 0.5f);
				}
				if (array[2] < 0f)
				{
					color3.sRgbColor.b = 0;
				}
				else if (array[2] > 1f)
				{
					color3.sRgbColor.b = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.b = (byte)(array[2] * 255f + 0.5f);
				}
				color3.scRgbColor.r = Color.sRgbToScRgb(color3.sRgbColor.r);
				color3.scRgbColor.g = Color.sRgbToScRgb(color3.sRgbColor.g);
				color3.scRgbColor.b = Color.sRgbToScRgb(color3.sRgbColor.b);
				color3.scRgbColor.a = color1.scRgbColor.a - color2.scRgbColor.a;
				if (color3.scRgbColor.a < 0f)
				{
					color3.scRgbColor.a = 0f;
					color3.sRgbColor.a = 0;
				}
				else if (color3.scRgbColor.a > 1f)
				{
					color3.scRgbColor.a = 1f;
					color3.sRgbColor.a = byte.MaxValue;
				}
				else
				{
					color3.sRgbColor.a = (byte)(color3.scRgbColor.a * 255f + 0.5f);
				}
				return color3;
			}
			throw new ArgumentException(SR.Get("Color_ColorContextTypeMismatch", null));
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Color" /> de uma estrutura <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="color1">A estrutura <see cref="T:System.Windows.Media.Color" /> da qual subtrair.</param>
		/// <param name="color2">A estrutura <see cref="T:System.Windows.Media.Color" /> para subtrair de <paramref name="color1" />.</param>
		/// <returns>Uma nova estrutura de <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de subtração.</returns>
		// Token: 0x06001F52 RID: 8018 RVA: 0x0007F9C0 File Offset: 0x0007EDC0
		public static Color Subtract(Color color1, Color color2)
		{
			return color1 - color2;
		}

		/// <summary>Multiplica os canais alfa, vermelho, azul e verde da estrutura <see cref="T:System.Windows.Media.Color" /> especificada pelo valor especificado.</summary>
		/// <param name="color">O <see cref="T:System.Windows.Media.Color" /> a ser multiplicado.</param>
		/// <param name="coefficient">O valor pelo qual multiplicar.</param>
		/// <returns>Uma nova estrutura <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de multiplicação.</returns>
		// Token: 0x06001F53 RID: 8019 RVA: 0x0007F9D4 File Offset: 0x0007EDD4
		public static Color operator *(Color color, float coefficient)
		{
			Color color2 = Color.FromScRgb(color.scRgbColor.a * coefficient, color.scRgbColor.r * coefficient, color.scRgbColor.g * coefficient, color.scRgbColor.b * coefficient);
			if (color.context == null)
			{
				return color2;
			}
			color2.context = color.context;
			color2.ComputeNativeValues(color2.context.NumChannels);
			return color2;
		}

		/// <summary>Multiplica os canais alfa, vermelho, azul e verde da estrutura <see cref="T:System.Windows.Media.Color" /> especificada pelo valor especificado.</summary>
		/// <param name="color">O <see cref="T:System.Windows.Media.Color" /> a ser multiplicado.</param>
		/// <param name="coefficient">O valor pelo qual multiplicar.</param>
		/// <returns>Uma nova estrutura <see cref="T:System.Windows.Media.Color" /> cujos valores de cor são os resultados da operação de multiplicação.</returns>
		// Token: 0x06001F54 RID: 8020 RVA: 0x0007FA4C File Offset: 0x0007EE4C
		public static Color Multiply(Color color, float coefficient)
		{
			return color * coefficient;
		}

		/// <summary>Testa se duas estruturas <see cref="T:System.Windows.Media.Color" /> são idênticas.</summary>
		/// <param name="color1">A primeira estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <param name="color2">A segunda estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="color1" /> e <paramref name="color2" /> são exatamente idênticas; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F55 RID: 8021 RVA: 0x0007FA60 File Offset: 0x0007EE60
		public static bool Equals(Color color1, Color color2)
		{
			return color1 == color2;
		}

		/// <summary>Testa se a estrutura <see cref="T:System.Windows.Media.Color" /> especificada é idêntica a esta cor.</summary>
		/// <param name="color">A estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada à estrutura <see cref="T:System.Windows.Media.Color" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se a estrutura <see cref="T:System.Windows.Media.Color" /> especificada e é idêntica à estrutura <see cref="T:System.Windows.Media.Color" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F56 RID: 8022 RVA: 0x0007FA74 File Offset: 0x0007EE74
		public bool Equals(Color color)
		{
			return this == color;
		}

		/// <summary>Testa se o objeto especificado é uma estrutura <see cref="T:System.Windows.Media.Color" /> e é equivalente a esta cor.</summary>
		/// <param name="o">O objeto a ser comparado a essa estrutura <see cref="T:System.Windows.Media.Color" />.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto especificado é uma estrutura <see cref="T:System.Windows.Media.Color" /> e é idêntico à estrutura <see cref="T:System.Windows.Media.Color" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F57 RID: 8023 RVA: 0x0007FA90 File Offset: 0x0007EE90
		public override bool Equals(object o)
		{
			if (o is Color)
			{
				Color color = (Color)o;
				return this == color;
			}
			return false;
		}

		/// <summary>Testa se duas estruturas <see cref="T:System.Windows.Media.Color" /> são idênticas.</summary>
		/// <param name="color1">A primeira estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <param name="color2">A segunda estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="color1" /> e <paramref name="color2" /> são exatamente idênticas; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F58 RID: 8024 RVA: 0x0007FABC File Offset: 0x0007EEBC
		public static bool operator ==(Color color1, Color color2)
		{
			if (color1.context == null && color2.context == null)
			{
				return color1.scRgbColor.r == color2.scRgbColor.r && color1.scRgbColor.g == color2.scRgbColor.g && color1.scRgbColor.b == color2.scRgbColor.b && color1.scRgbColor.a == color2.scRgbColor.a;
			}
			if (color1.context == null || color2.context == null)
			{
				return false;
			}
			if (color1.context.ColorSpaceFamily != color2.context.ColorSpaceFamily)
			{
				return false;
			}
			if (color1.nativeColorValue == null && color2.nativeColorValue == null)
			{
				return true;
			}
			if (color1.nativeColorValue == null || color2.nativeColorValue == null)
			{
				return false;
			}
			if (color1.nativeColorValue.GetLength(0) != color2.nativeColorValue.GetLength(0))
			{
				return false;
			}
			for (int i = 0; i < color1.nativeColorValue.GetLength(0); i++)
			{
				if (color1.nativeColorValue[i] != color2.nativeColorValue[i])
				{
					return false;
				}
			}
			return color1.scRgbColor.a == color2.scRgbColor.a;
		}

		/// <summary>Testa se duas estruturas <see cref="T:System.Windows.Media.Color" /> não são idênticas.</summary>
		/// <param name="color1">A primeira estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <param name="color2">A segunda estrutura <see cref="T:System.Windows.Media.Color" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="color1" /> e <paramref name="color2" /> não forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F59 RID: 8025 RVA: 0x0007FC14 File Offset: 0x0007F014
		public static bool operator !=(Color color1, Color color2)
		{
			return !(color1 == color2);
		}

		/// <summary>Obtém o perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) da cor.</summary>
		/// <returns>O perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) da cor.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0007FC2C File Offset: 0x0007F02C
		public ColorContext ColorContext
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Obtém ou define o valor de canal alfa <see langword="sRGB" /> da cor.</summary>
		/// <returns>O valor de canal alfa <see langword="sRGB" /> da cor, um valor entre 0 e 255.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0007FC40 File Offset: 0x0007F040
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0007FC58 File Offset: 0x0007F058
		public byte A
		{
			get
			{
				return this.sRgbColor.a;
			}
			set
			{
				this.scRgbColor.a = (float)value / 255f;
				this.sRgbColor.a = value;
			}
		}

		/// <summary>Obtém ou define o valor de canal vermelho <see langword="sRGB" /> da cor.</summary>
		/// <returns>O <see langword="sRGB" /> valor de canal vermelho do <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 255.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0007FC84 File Offset: 0x0007F084
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0007FC9C File Offset: 0x0007F09C
		public byte R
		{
			get
			{
				return this.sRgbColor.r;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.r = Color.sRgbToScRgb(value);
					this.sRgbColor.r = value;
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		/// <summary>Obtém ou define o valor de canal verde <see langword="sRGB" /> da cor.</summary>
		/// <returns>O <see langword="sRGB" /> valor de canal de verde a <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 255.</returns>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0007FD04 File Offset: 0x0007F104
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0007FD1C File Offset: 0x0007F11C
		public byte G
		{
			get
			{
				return this.sRgbColor.g;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.g = Color.sRgbToScRgb(value);
					this.sRgbColor.g = value;
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		/// <summary>Obtém ou define o valor de canal azul <see langword="sRGB" /> da cor.</summary>
		/// <returns>O <see langword="sRGB" /> valor de canal de azul a <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 255.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0007FD84 File Offset: 0x0007F184
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x0007FD9C File Offset: 0x0007F19C
		public byte B
		{
			get
			{
				return this.sRgbColor.b;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.b = Color.sRgbToScRgb(value);
					this.sRgbColor.b = value;
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		/// <summary>Obtém ou define o valor de canal alfa <see langword="ScRGB" /> da cor.</summary>
		/// <returns>O valor de canal alfa <see langword="ScRGB" /> da estrutura <see cref="T:System.Windows.Media.Color" />, um valor entre 0 e 1.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0007FE04 File Offset: 0x0007F204
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0007FE1C File Offset: 0x0007F21C
		public float ScA
		{
			get
			{
				return this.scRgbColor.a;
			}
			set
			{
				this.scRgbColor.a = value;
				if (value < 0f)
				{
					this.sRgbColor.a = 0;
					return;
				}
				if (value > 1f)
				{
					this.sRgbColor.a = byte.MaxValue;
					return;
				}
				this.sRgbColor.a = (byte)(value * 255f);
			}
		}

		/// <summary>Obtém ou define o valor de canal vermelho <see langword="ScRGB" /> da cor.</summary>
		/// <returns>O <see langword="ScRGB" /> valor de canal vermelho do <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 1.</returns>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0007FE78 File Offset: 0x0007F278
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x0007FE90 File Offset: 0x0007F290
		public float ScR
		{
			get
			{
				return this.scRgbColor.r;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.r = value;
					this.sRgbColor.r = Color.ScRgbTosRgb(value);
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		/// <summary>Obtém ou define o valor de canal verde <see langword="ScRGB" /> da cor.</summary>
		/// <returns>O <see langword="ScRGB" /> valor de canal de verde a <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 1.</returns>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0007FEF8 File Offset: 0x0007F2F8
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0007FF10 File Offset: 0x0007F310
		public float ScG
		{
			get
			{
				return this.scRgbColor.g;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.g = value;
					this.sRgbColor.g = Color.ScRgbTosRgb(value);
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		/// <summary>Obtém ou define o valor de canal azul ScRGB da cor.</summary>
		/// <returns>O valor de canal vermelho ScRGB do <see cref="T:System.Windows.Media.Color" /> estruturar um valor entre 0 e 1.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0007FF78 File Offset: 0x0007F378
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0007FF90 File Offset: 0x0007F390
		public float ScB
		{
			get
			{
				return this.scRgbColor.b;
			}
			set
			{
				if (this.context == null || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.Srgb || this.context.ColorSpaceFamily == ColorContext.StandardColorSpace.ScRgb)
				{
					this.scRgbColor.b = value;
					this.sRgbColor.b = Color.ScRgbTosRgb(value);
					return;
				}
				throw new InvalidOperationException(SR.Get("Color_ColorContextNotsRGB_or_scRGB", null));
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0007FFF8 File Offset: 0x0007F3F8
		private static float sRgbToScRgb(byte bval)
		{
			float num = (float)bval / 255f;
			if ((double)num <= 0.0)
			{
				return 0f;
			}
			if ((double)num <= 0.04045)
			{
				return num / 12.92f;
			}
			if (num < 1f)
			{
				return (float)Math.Pow(((double)num + 0.055) / 1.055, 2.4);
			}
			return 1f;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0008006C File Offset: 0x0007F46C
		private static byte ScRgbTosRgb(float val)
		{
			if ((double)val <= 0.0)
			{
				return 0;
			}
			if ((double)val <= 0.0031308)
			{
				return (byte)(255f * val * 12.92f + 0.5f);
			}
			if ((double)val < 1.0)
			{
				return (byte)(255f * (1.055f * (float)Math.Pow((double)val, 0.41666666666666669) - 0.055f) + 0.5f);
			}
			return byte.MaxValue;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000800E8 File Offset: 0x0007F4E8
		private void ComputeScRgbValues()
		{
			if (this.context != null)
			{
				Color color = Color.FromRgb(0, 0, 0);
				color.context = new ColorContext(PixelFormats.Bgra32);
				ColorTransform colorTransform = new ColorTransform(this.context, color.context);
				float[] array = new float[3];
				colorTransform.Translate(this.nativeColorValue, array);
				this.scRgbColor.r = Color.sRgbToScRgb((byte)(255f * array[0] + 0.5f));
				this.scRgbColor.g = Color.sRgbToScRgb((byte)(255f * array[1] + 0.5f));
				this.scRgbColor.b = Color.sRgbToScRgb((byte)(255f * array[2] + 0.5f));
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000801A8 File Offset: 0x0007F5A8
		private void ComputeNativeValues(int numChannels)
		{
			this.nativeColorValue = new float[numChannels];
			if (this.nativeColorValue.GetLength(0) > 0)
			{
				float[] srcValue = new float[]
				{
					(float)this.sRgbColor.r / 255f,
					(float)this.sRgbColor.g / 255f,
					(float)this.sRgbColor.b / 255f
				};
				ColorTransform colorTransform = new ColorTransform(this.context, new ColorContext(PixelFormats.Bgra32));
				colorTransform.Translate(srcValue, this.nativeColorValue);
			}
		}

		// Token: 0x0400104F RID: 4175
		[MarshalAs(UnmanagedType.Interface)]
		private ColorContext context;

		// Token: 0x04001050 RID: 4176
		private Color.MILColorF scRgbColor;

		// Token: 0x04001051 RID: 4177
		private Color.MILColor sRgbColor;

		// Token: 0x04001052 RID: 4178
		private float[] nativeColorValue;

		// Token: 0x04001053 RID: 4179
		private bool isFromScRgb;

		// Token: 0x04001054 RID: 4180
		private const string c_scRgbFormat = "R";

		// Token: 0x0200085A RID: 2138
		private struct MILColorF
		{
			// Token: 0x0400282A RID: 10282
			public float a;

			// Token: 0x0400282B RID: 10283
			public float r;

			// Token: 0x0400282C RID: 10284
			public float g;

			// Token: 0x0400282D RID: 10285
			public float b;
		}

		// Token: 0x0200085B RID: 2139
		private struct MILColor
		{
			// Token: 0x0400282E RID: 10286
			public byte a;

			// Token: 0x0400282F RID: 10287
			public byte r;

			// Token: 0x04002830 RID: 10288
			public byte g;

			// Token: 0x04002831 RID: 10289
			public byte b;
		}
	}
}
