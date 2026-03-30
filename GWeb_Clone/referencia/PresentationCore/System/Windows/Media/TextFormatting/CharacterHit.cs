using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa informações sobre um caractere para o qual houve uma ocorrência em uma execução de glifo.</summary>
	// Token: 0x02000593 RID: 1427
	public struct CharacterHit : IEquatable<CharacterHit>
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" />.</summary>
		/// <param name="firstCharacterIndex">O índice do primeiro caractere do qual houve uma ocorrência.</param>
		/// <param name="trailingLength">No caso de uma borda à esquerda, esse valor é 0. No caso de uma borda à direita, esse valor é o número de pontos de código até a próxima posição de cursor válida.</param>
		// Token: 0x060041B8 RID: 16824 RVA: 0x00102214 File Offset: 0x00101614
		public CharacterHit(int firstCharacterIndex, int trailingLength)
		{
			this._firstCharacterIndex = firstCharacterIndex;
			this._trailingLength = trailingLength;
		}

		/// <summary>Obtém o índice do primeiro caractere para o qual houve uma ocorrência.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o índice.</returns>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x00102230 File Offset: 0x00101630
		public int FirstCharacterIndex
		{
			get
			{
				return this._firstCharacterIndex;
			}
		}

		/// <summary>Obtém o valor do comprimento à direita do caractere para o qual houve uma ocorrência.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o comprimento à direita.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x00102244 File Offset: 0x00101644
		public int TrailingLength
		{
			get
			{
				return this._trailingLength;
			}
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à igualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> quando os valores das propriedades <see cref="P:System.Windows.Media.TextFormatting.CharacterHit.FirstCharacterIndex" /> e <see cref="P:System.Windows.Media.TextFormatting.CharacterHit.TrailingLength" /> são iguais para os dois objetos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041BB RID: 16827 RVA: 0x00102258 File Offset: 0x00101658
		public static bool operator ==(CharacterHit left, CharacterHit right)
		{
			return left._firstCharacterIndex == right._firstCharacterIndex && left._trailingLength == right._trailingLength;
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> quando os valores das propriedades <see cref="P:System.Windows.Media.TextFormatting.CharacterHit.FirstCharacterIndex" /> e <see cref="P:System.Windows.Media.TextFormatting.CharacterHit.TrailingLength" /> são iguais para os dois objetos; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060041BC RID: 16828 RVA: 0x00102284 File Offset: 0x00101684
		public static bool operator !=(CharacterHit left, CharacterHit right)
		{
			return !(left == right);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="obj">O <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="obj" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041BD RID: 16829 RVA: 0x0010229C File Offset: 0x0010169C
		public bool Equals(CharacterHit obj)
		{
			return this == obj;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="obj">O objeto a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="obj" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />. Se <paramref name="obj" /> não for um objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x060041BE RID: 16830 RVA: 0x001022B8 File Offset: 0x001016B8
		public override bool Equals(object obj)
		{
			return obj is CharacterHit && this == (CharacterHit)obj;
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x060041BF RID: 16831 RVA: 0x001022E0 File Offset: 0x001016E0
		public override int GetHashCode()
		{
			return this._firstCharacterIndex.GetHashCode() ^ this._trailingLength.GetHashCode();
		}

		// Token: 0x04001802 RID: 6146
		private int _firstCharacterIndex;

		// Token: 0x04001803 RID: 6147
		private int _trailingLength;
	}
}
