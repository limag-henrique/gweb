using System;
using System.ComponentModel;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Descreve o grau em que uma fonte foi ampliada em comparação com sua taxa de proporção normal.</summary>
	// Token: 0x020001B6 RID: 438
	[Localizability(LocalizationCategory.None)]
	[TypeConverter(typeof(FontStretchConverter))]
	public struct FontStretch : IFormattable
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001F4A4 File Offset: 0x0001E8A4
		internal FontStretch(int stretch)
		{
			this._stretch = stretch - 5;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.FontStretch" /> que corresponde ao valor usStretchClass de OpenType.</summary>
		/// <param name="stretchValue">Um valor inteiro entre 1 e 9 que corresponde à definição usStretchValue na especificação OpenType.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001F4BC File Offset: 0x0001E8BC
		public static FontStretch FromOpenTypeStretch(int stretchValue)
		{
			if (stretchValue < 1 || stretchValue > 9)
			{
				throw new ArgumentOutOfRangeException("stretchValue", SR.Get("ParameterMustBeBetween", new object[]
				{
					1,
					9
				}));
			}
			return new FontStretch(stretchValue);
		}

		/// <summary>Retorna um valor que representa o usStretchClass de OpenType para este objeto <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <returns>Um valor inteiro entre 1 e 999 que corresponde à definição usStretchClass na especificação OpenType.</returns>
		// Token: 0x060006DB RID: 1755 RVA: 0x0001F508 File Offset: 0x0001E908
		public int ToOpenTypeStretch()
		{
			return this.RealStretch;
		}

		/// <summary>Compara duas instâncias de objetos <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <param name="left">O primeiro objeto <see cref="T:System.Windows.FontStretch" /> a ser comparado.</param>
		/// <param name="right">O segundo objeto <see cref="T:System.Windows.FontStretch" /> a ser comparado.</param>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa a relação entre as duas instâncias de <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x060006DC RID: 1756 RVA: 0x0001F51C File Offset: 0x0001E91C
		public static int Compare(FontStretch left, FontStretch right)
		{
			return left._stretch - right._stretch;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStretch" /> para determinar se uma é menor do que a outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> caso <paramref name="left" /> seja menor que <paramref name="right" />; do contrário, <see langword="false" />.</returns>
		// Token: 0x060006DD RID: 1757 RVA: 0x0001F538 File Offset: 0x0001E938
		public static bool operator <(FontStretch left, FontStretch right)
		{
			return FontStretch.Compare(left, right) < 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStretch" /> para determinar se uma é menor ou igual à outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="left" /> for menor ou igual a <paramref name="right" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006DE RID: 1758 RVA: 0x0001F550 File Offset: 0x0001E950
		public static bool operator <=(FontStretch left, FontStretch right)
		{
			return FontStretch.Compare(left, right) <= 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStretch" /> para determinar se uma é maior do que a outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> caso <paramref name="left" /> seja maior que <paramref name="right" />; do contrário, <see langword="false" />.</returns>
		// Token: 0x060006DF RID: 1759 RVA: 0x0001F56C File Offset: 0x0001E96C
		public static bool operator >(FontStretch left, FontStretch right)
		{
			return FontStretch.Compare(left, right) > 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStretch" /> para determinar se uma é maior ou igual à outra.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="left" /> for maior ou igual a <paramref name="right" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001F584 File Offset: 0x0001E984
		public static bool operator >=(FontStretch left, FontStretch right)
		{
			return FontStretch.Compare(left, right) >= 0;
		}

		/// <summary>Compara a igualdade de duas instâncias de <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> quando os objetos <see cref="T:System.Windows.FontStretch" /> especificados forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001F5A0 File Offset: 0x0001E9A0
		public static bool operator ==(FontStretch left, FontStretch right)
		{
			return FontStretch.Compare(left, right) == 0;
		}

		/// <summary>Avalia duas instâncias de <see cref="T:System.Windows.FontStretch" /> para determinar desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.FontStretch" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> se <paramref name="left" /> for igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001F5B8 File Offset: 0x0001E9B8
		public static bool operator !=(FontStretch left, FontStretch right)
		{
			return !(left == right);
		}

		/// <summary>Compara um objeto <see cref="T:System.Windows.FontStretch" /> ao objeto <see cref="T:System.Windows.FontStretch" /> atual.</summary>
		/// <param name="obj">A instância do objeto <see cref="T:System.Windows.FontStretch" /> a ser comparada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se duas instâncias são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001F5D0 File Offset: 0x0001E9D0
		public bool Equals(FontStretch obj)
		{
			return this == obj;
		}

		/// <summary>Compara um <see cref="T:System.Object" /> ao objeto <see cref="T:System.Windows.FontStretch" /> atual.</summary>
		/// <param name="obj">A instância do <see cref="T:System.Object" /> a ser comparada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se duas instâncias são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001F5EC File Offset: 0x0001E9EC
		public override bool Equals(object obj)
		{
			return obj is FontStretch && this == (FontStretch)obj;
		}

		/// <summary>Recupera o código hash para esse objeto.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash do objeto.</returns>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001F614 File Offset: 0x0001EA14
		public override int GetHashCode()
		{
			return this.RealStretch;
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.FontStretch" /> atual com base na cultura atual.</summary>
		/// <returns>Um valor <see cref="T:System.String" /> representando o objeto.</returns>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001F628 File Offset: 0x0001EA28
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
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001F640 File Offset: 0x0001EA40
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001F658 File Offset: 0x0001EA58
		private string ConvertToString(string format, IFormatProvider provider)
		{
			string result;
			if (!FontStretches.FontStretchToString(this.RealStretch, out result))
			{
				Invariant.Assert(false);
			}
			return result;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001F67C File Offset: 0x0001EA7C
		private int RealStretch
		{
			get
			{
				return this._stretch + 5;
			}
		}

		// Token: 0x040005AE RID: 1454
		private int _stretch;
	}
}
