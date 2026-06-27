using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Descreve um buffer de caracteres para uma sequência de texto.</summary>
	// Token: 0x02000592 RID: 1426
	public struct CharacterBufferReference : IEquatable<CharacterBufferReference>
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> usando uma matriz de caracteres especificada.</summary>
		/// <param name="characterArray">A matriz <see cref="T:System.Char" />.</param>
		/// <param name="offsetToFirstChar">O deslocamento para o primeiro caractere a ser usado em <paramref name="characterArray" />.</param>
		// Token: 0x060041AD RID: 16813 RVA: 0x00102078 File Offset: 0x00101478
		public CharacterBufferReference(char[] characterArray, int offsetToFirstChar)
		{
			this = new CharacterBufferReference(new CharArrayCharacterBuffer(characterArray), offsetToFirstChar);
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> usando uma cadeia de caracteres especificada.</summary>
		/// <param name="characterString">O <see cref="T:System.String" /> que contém os caracteres de texto.</param>
		/// <param name="offsetToFirstChar">O deslocamento para o primeiro caractere a ser usado em <paramref name="characterString" />.</param>
		// Token: 0x060041AE RID: 16814 RVA: 0x00102094 File Offset: 0x00101494
		public CharacterBufferReference(string characterString, int offsetToFirstChar)
		{
			this = new CharacterBufferReference(new StringCharacterBuffer(characterString), offsetToFirstChar);
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> usando uma cadeia de caracteres desprotegidos especificada.</summary>
		/// <param name="unsafeCharacterString">Ponteiro para cadeia de caracteres.</param>
		/// <param name="characterLength">O comprimento de <paramref name="unsafeCharacterString" />.</param>
		// Token: 0x060041AF RID: 16815 RVA: 0x001020B0 File Offset: 0x001014B0
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe CharacterBufferReference(char* unsafeCharacterString, int characterLength)
		{
			this = new CharacterBufferReference(new UnsafeStringCharacterBuffer(unsafeCharacterString, characterLength), 0);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x001020CC File Offset: 0x001014CC
		internal CharacterBufferReference(CharacterBuffer charBuffer, int offsetToFirstChar)
		{
			if (offsetToFirstChar < 0)
			{
				throw new ArgumentOutOfRangeException("offsetToFirstChar", SR.Get("ParameterCannotBeNegative"));
			}
			int num = (charBuffer == null) ? 0 : Math.Max(0, charBuffer.Count - 1);
			if (offsetToFirstChar > num)
			{
				throw new ArgumentOutOfRangeException("offsetToFirstChar", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					num
				}));
			}
			this._charBuffer = charBuffer;
			this._offsetToFirstChar = offsetToFirstChar;
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x060041B1 RID: 16817 RVA: 0x00102140 File Offset: 0x00101540
		public override int GetHashCode()
		{
			if (this._charBuffer == null)
			{
				return 0;
			}
			return this._charBuffer.GetHashCode() ^ this._offsetToFirstChar;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="obj">O objeto a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="obj" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />. Se <paramref name="obj" /> não for um objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x060041B2 RID: 16818 RVA: 0x0010216C File Offset: 0x0010156C
		public override bool Equals(object obj)
		{
			return obj is CharacterBufferReference && this.Equals((CharacterBufferReference)obj);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041B3 RID: 16819 RVA: 0x00102190 File Offset: 0x00101590
		public bool Equals(CharacterBufferReference value)
		{
			return this._charBuffer == value._charBuffer && this._offsetToFirstChar == value._offsetToFirstChar;
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à igualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que os objetos <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> especificados são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041B4 RID: 16820 RVA: 0x001021BC File Offset: 0x001015BC
		public static bool operator ==(CharacterBufferReference left, CharacterBufferReference right)
		{
			return left.Equals(right);
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> quanto à desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> para mostrar que <paramref name="left" /> é igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060041B5 RID: 16821 RVA: 0x001021D4 File Offset: 0x001015D4
		public static bool operator !=(CharacterBufferReference left, CharacterBufferReference right)
		{
			return !(left == right);
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x001021EC File Offset: 0x001015EC
		internal CharacterBuffer CharacterBuffer
		{
			get
			{
				return this._charBuffer;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x00102200 File Offset: 0x00101600
		internal int OffsetToFirstChar
		{
			get
			{
				return this._offsetToFirstChar;
			}
		}

		// Token: 0x04001800 RID: 6144
		private CharacterBuffer _charBuffer;

		// Token: 0x04001801 RID: 6145
		private int _offsetToFirstChar;
	}
}
