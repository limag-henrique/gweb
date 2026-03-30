using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Referencia a densidade de uma face de tipos, em termos de luminosidade ou peso dos traços.</summary>
	// Token: 0x020001B9 RID: 441
	[TypeConverter(typeof(FontWeightConverter))]
	[Localizability(LocalizationCategory.None)]
	public struct FontWeight : IFormattable
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0001FB04 File Offset: 0x0001EF04
		internal FontWeight(int weight)
		{
			this._weight = weight - 400;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.FontWeight" /> que corresponde ao valor usWeightClass de OpenType.</summary>
		/// <param name="weightValue">Um valor inteiro entre 1 e 999 que corresponde à definição usWeightClass na especificação OpenType.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.FontWeight" />.</returns>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001FB20 File Offset: 0x0001EF20
		public static FontWeight FromOpenTypeWeight(int weightValue)
		{
			if (weightValue < 1 || weightValue > 999)
			{
				throw new ArgumentOutOfRangeException("weightValue", SR.Get("ParameterMustBeBetween", new object[]
				{
					1,
					999
				}));
			}
			return new FontWeight(weightValue);
		}

		/// <summary>Retorna um valor que representa o usWeightClass de OpenType para o objeto <see cref="T:System.Windows.FontWeight" />.</summary>
		/// <returns>Um valor inteiro entre 1 e 999 que corresponde à definição usWeightClass na especificação OpenType.</returns>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001FB70 File Offset: 0x0001EF70
		public int ToOpenTypeWeight()
		{
			return this.RealWeight;
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.FontWeight" />.</summary>
		/// <param name="left">O primeiro objeto <see cref="T:System.Windows.FontWeight" /> a ser comparado.</param>
		/// <param name="right">O segundo objeto <see cref="T:System.Windows.FontWeight" /> a ser comparado.</param>
		/// <returns>Um valor <see cref="T:System.Int32" /> que indica a relação entre as duas instâncias de <see cref="T:System.Windows.FontWeight" />. Quando o valor retornado é menor que zero, <paramref name="left" /> é menor que <paramref name="right" />. Quando esse valor é zero, ele indica que ambos os operandos são iguais. Quando o valor é maior que zero, isso indica que <paramref name="left" /> é maior do que <paramref name="right" />.</returns>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001FB84 File Offset: 0x0001EF84
		public static int Compare(FontWeight left, FontWeight right)
		{
			return left._weight - right._weight;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontWeight" /> para determinar se uma é menor do que a outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> caso <paramref name="left" /> seja menor que <paramref name="right" />; do contrário, <see langword="false" />.</returns>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001FBA0 File Offset: 0x0001EFA0
		public static bool operator <(FontWeight left, FontWeight right)
		{
			return FontWeight.Compare(left, right) < 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontWeight" /> para determinar se uma é menor ou igual à outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="left" /> for menor ou igual a <paramref name="right" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000700 RID: 1792 RVA: 0x0001FBB8 File Offset: 0x0001EFB8
		public static bool operator <=(FontWeight left, FontWeight right)
		{
			return FontWeight.Compare(left, right) <= 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontWeight" /> para determinar se uma é maior do que a outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> caso <paramref name="left" /> seja maior que <paramref name="right" />; do contrário, <see langword="false" />.</returns>
		// Token: 0x06000701 RID: 1793 RVA: 0x0001FBD4 File Offset: 0x0001EFD4
		public static bool operator >(FontWeight left, FontWeight right)
		{
			return FontWeight.Compare(left, right) > 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontWeight" /> para determinar se uma é maior ou igual à outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="left" /> for maior ou igual a <paramref name="right" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000702 RID: 1794 RVA: 0x0001FBEC File Offset: 0x0001EFEC
		public static bool operator >=(FontWeight left, FontWeight right)
		{
			return FontWeight.Compare(left, right) >= 0;
		}

		/// <summary>Compara a igualdade de duas instâncias de <see cref="T:System.Windows.FontWeight" />.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias de <see cref="T:System.Windows.FontWeight" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000703 RID: 1795 RVA: 0x0001FC08 File Offset: 0x0001F008
		public static bool operator ==(FontWeight left, FontWeight right)
		{
			return FontWeight.Compare(left, right) == 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontWeight" /> para determinar desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> se <paramref name="left" /> for igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x06000704 RID: 1796 RVA: 0x0001FC20 File Offset: 0x0001F020
		public static bool operator !=(FontWeight left, FontWeight right)
		{
			return !(left == right);
		}

		/// <summary>Determina se o objeto <see cref="T:System.Windows.FontWeight" /> atual é igual a um objeto <see cref="T:System.Windows.FontWeight" /> especificado.</summary>
		/// <param name="obj">A instância de <see cref="T:System.Windows.FontWeight" /> a ser comparada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000705 RID: 1797 RVA: 0x0001FC38 File Offset: 0x0001F038
		public bool Equals(FontWeight obj)
		{
			return this == obj;
		}

		/// <summary>Determina se o objeto <see cref="T:System.Windows.FontWeight" /> atual é igual a um objeto especificado.</summary>
		/// <param name="obj">O <see cref="T:System.Object" /> a ser comparado quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000706 RID: 1798 RVA: 0x0001FC54 File Offset: 0x0001F054
		public override bool Equals(object obj)
		{
			return obj is FontWeight && this == (FontWeight)obj;
		}

		/// <summary>Recupera o código hash para esse objeto.</summary>
		/// <returns>Um código hash de 32 bits, que é um inteiro com sinal.</returns>
		// Token: 0x06000707 RID: 1799 RVA: 0x0001FC7C File Offset: 0x0001F07C
		public override int GetHashCode()
		{
			return this.RealWeight;
		}

		/// <summary>Cria uma cadeia de caracteres de texto que representa valor do objeto <see cref="T:System.Windows.FontWeight" /> atual e se baseia nas informações de propriedade <see cref="P:System.Globalization.CultureInfo.CurrentCulture" />.</summary>
		/// <returns>Um <see cref="T:System.String" /> que representa o valor do objeto <see cref="T:System.Windows.FontWeight" />, tais como "Light", "Normal" ou "UltraBold".</returns>
		// Token: 0x06000708 RID: 1800 RVA: 0x0001FC90 File Offset: 0x0001F090
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">O <see cref="T:System.String" /> especificando o formato a ser usado.  
		///
		/// ou - 
		/// <see langword="null" /> para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O <see cref="T:System.IFormatProvider" /> a ser usado para formatar o valor.  
		///
		/// ou - 
		/// <see langword="null" /> para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>Uma <see cref="T:System.String" /> que contém o valor da instância atual no formato especificado.</returns>
		// Token: 0x06000709 RID: 1801 RVA: 0x0001FCA8 File Offset: 0x0001F0A8
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001FCC0 File Offset: 0x0001F0C0
		private string ConvertToString(string format, IFormatProvider provider)
		{
			string result;
			if (!FontWeights.FontWeightToString(this.RealWeight, out result))
			{
				return this.RealWeight.ToString(provider);
			}
			return result;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001FCF0 File Offset: 0x0001F0F0
		private int RealWeight
		{
			get
			{
				return this._weight + 400;
			}
		}

		// Token: 0x040005AF RID: 1455
		private int _weight;
	}
}
