using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa a menor e a maior larguras de parágrafo possíveis que contêm totalmente o conteúdo de texto especificado.</summary>
	// Token: 0x02000597 RID: 1431
	public struct MinMaxParagraphWidth : IEquatable<MinMaxParagraphWidth>
	{
		// Token: 0x060041DB RID: 16859 RVA: 0x00102694 File Offset: 0x00101A94
		internal MinMaxParagraphWidth(double minWidth, double maxWidth)
		{
			this._minWidth = minWidth;
			this._maxWidth = maxWidth;
		}

		/// <summary>Obtém a menor largura de parágrafo possível que pode conter completamente o conteúdo do texto especificado.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a menor largura de parágrafo possíveis.</returns>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x001026B0 File Offset: 0x00101AB0
		public double MinWidth
		{
			get
			{
				return this._minWidth;
			}
		}

		/// <summary>Obtém a maior largura de parágrafo possível que pode conter completamente o conteúdo do texto especificado.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a maior largura de parágrafo possíveis.</returns>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x001026C4 File Offset: 0x00101AC4
		public double MaxWidth
		{
			get
			{
				return this._maxWidth;
			}
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x060041DE RID: 16862 RVA: 0x001026D8 File Offset: 0x00101AD8
		public override int GetHashCode()
		{
			return this._minWidth.GetHashCode() ^ this._maxWidth.GetHashCode();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041DF RID: 16863 RVA: 0x001026FC File Offset: 0x00101AFC
		public bool Equals(MinMaxParagraphWidth value)
		{
			return this == value;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="obj">O <see cref="T:System.Object" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="obj" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />. Se <paramref name="obj" /> não for um objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x060041E0 RID: 16864 RVA: 0x00102718 File Offset: 0x00101B18
		public override bool Equals(object obj)
		{
			return obj is MinMaxParagraphWidth && this == (MinMaxParagraphWidth)obj;
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à igualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que os objetos <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> especificados são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041E1 RID: 16865 RVA: 0x00102740 File Offset: 0x00101B40
		public static bool operator ==(MinMaxParagraphWidth left, MinMaxParagraphWidth right)
		{
			return left._minWidth == right._minWidth && left._maxWidth == right._maxWidth;
		}

		/// <summary>Compare dois objetos <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> para mostrar que <paramref name="left" /> é igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060041E2 RID: 16866 RVA: 0x0010276C File Offset: 0x00101B6C
		public static bool operator !=(MinMaxParagraphWidth left, MinMaxParagraphWidth right)
		{
			return !(left == right);
		}

		// Token: 0x0400180B RID: 6155
		private double _minWidth;

		// Token: 0x0400180C RID: 6156
		private double _maxWidth;
	}
}
