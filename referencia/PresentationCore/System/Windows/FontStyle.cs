using System;
using System.ComponentModel;

namespace System.Windows
{
	/// <summary>Define uma estrutura que representa o estilo de um tipo de fonte como normal, itálico ou oblíquo.</summary>
	// Token: 0x020001B3 RID: 435
	[TypeConverter(typeof(FontStyleConverter))]
	[Localizability(LocalizationCategory.None)]
	public struct FontStyle : IFormattable
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0001F15C File Offset: 0x0001E55C
		internal FontStyle(int style)
		{
			this._style = style;
		}

		/// <summary>Compara a igualdade de duas instâncias de <see cref="T:System.Windows.FontStyle" />.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStyle" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStyle" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que os objetos <see cref="T:System.Windows.FontStyle" /> especificados são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006C7 RID: 1735 RVA: 0x0001F170 File Offset: 0x0001E570
		public static bool operator ==(FontStyle left, FontStyle right)
		{
			return left._style == right._style;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStyle" /> para determinar desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStyle" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStyle" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> para mostrar que <paramref name="left" /> é igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001F18C File Offset: 0x0001E58C
		public static bool operator !=(FontStyle left, FontStyle right)
		{
			return !(left == right);
		}

		/// <summary>Compara um <see cref="T:System.Windows.FontStyle" /> com a instância de <see cref="T:System.Windows.FontStyle" /> atual quanto à igualdade.</summary>
		/// <param name="obj">Uma instância de <see cref="T:System.Windows.FontStyle" /> a ser comparada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que as duas instâncias são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001F1A4 File Offset: 0x0001E5A4
		public bool Equals(FontStyle obj)
		{
			return this == obj;
		}

		/// <summary>Compara um <see cref="T:System.Object" /> com a instância de <see cref="T:System.Windows.FontStyle" /> atual quanto à igualdade.</summary>
		/// <param name="obj">Um valor <see cref="T:System.Object" /> que representa o <see cref="T:System.Windows.FontStyle" /> a ser comparado quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que as duas instâncias são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006CA RID: 1738 RVA: 0x0001F1C0 File Offset: 0x0001E5C0
		public override bool Equals(object obj)
		{
			return obj is FontStyle && this == (FontStyle)obj;
		}

		/// <summary>Recupera o código hash para esse objeto.</summary>
		/// <returns>Um código hash de 32 bits, que é um inteiro com sinal.</returns>
		// Token: 0x060006CB RID: 1739 RVA: 0x0001F1E8 File Offset: 0x0001E5E8
		public override int GetHashCode()
		{
			return this._style;
		}

		/// <summary>Cria um <see cref="T:System.String" /> que representa o objeto <see cref="T:System.Windows.FontStyle" /> atual e se baseia nas informações de propriedade <see cref="P:System.Globalization.CultureInfo.CurrentCulture" />.</summary>
		/// <returns>Um <see cref="T:System.String" /> que representa o valor do objeto <see cref="T:System.Windows.FontStyle" />, tais como "Normal", "Itálico" ou "Oblíquo".</returns>
		// Token: 0x060006CC RID: 1740 RVA: 0x0001F1FC File Offset: 0x0001E5FC
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
		// Token: 0x060006CD RID: 1741 RVA: 0x0001F214 File Offset: 0x0001E614
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001F22C File Offset: 0x0001E62C
		internal int GetStyleForInternalConstruction()
		{
			return this._style;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001F240 File Offset: 0x0001E640
		private string ConvertToString(string format, IFormatProvider provider)
		{
			if (this._style == 0)
			{
				return "Normal";
			}
			if (this._style == 1)
			{
				return "Oblique";
			}
			return "Italic";
		}

		// Token: 0x040005AD RID: 1453
		private int _style;
	}
}
