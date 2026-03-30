using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Descreve uma cadeia de caracteres.</summary>
	// Token: 0x02000594 RID: 1428
	public struct CharacterBufferRange : IEquatable<CharacterBufferRange>
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> de uma matriz de caracteres.</summary>
		/// <param name="characterArray">A matriz de caracteres.</param>
		/// <param name="offsetToFirstChar">O deslocamento de buffer de caracteres para o primeiro caractere.</param>
		/// <param name="characterLength">O número de caracteres em <paramref name="characterArray" /> a serem usados.</param>
		// Token: 0x060041C0 RID: 16832 RVA: 0x00102304 File Offset: 0x00101704
		public CharacterBufferRange(char[] characterArray, int offsetToFirstChar, int characterLength)
		{
			this = new CharacterBufferRange(new CharacterBufferReference(characterArray, offsetToFirstChar), characterLength);
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> de uma cadeia de caracteres.</summary>
		/// <param name="characterString">A cadeia de caracteres.</param>
		/// <param name="offsetToFirstChar">O deslocamento de buffer de caracteres para o primeiro caractere.</param>
		/// <param name="characterLength">O número de caracteres em <paramref name="characterString" /> a serem usados.</param>
		// Token: 0x060041C1 RID: 16833 RVA: 0x00102320 File Offset: 0x00101720
		public CharacterBufferRange(string characterString, int offsetToFirstChar, int characterLength)
		{
			this = new CharacterBufferRange(new CharacterBufferReference(characterString, offsetToFirstChar), characterLength);
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> de uma cadeia de caracteres não gerenciada.</summary>
		/// <param name="unsafeCharacterString">Uma referência de ponteiro não gerenciado para uma cadeia de caracteres.</param>
		/// <param name="characterLength">O número de caracteres em unsafecharacterString a usar.</param>
		// Token: 0x060041C2 RID: 16834 RVA: 0x0010233C File Offset: 0x0010173C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe CharacterBufferRange(char* unsafeCharacterString, int characterLength)
		{
			this = new CharacterBufferRange(new CharacterBufferReference(unsafeCharacterString, characterLength), characterLength);
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x00102358 File Offset: 0x00101758
		internal CharacterBufferRange(CharacterBufferReference characterBufferReference, int characterLength)
		{
			if (characterLength < 0)
			{
				throw new ArgumentOutOfRangeException("characterLength", SR.Get("ParameterCannotBeNegative"));
			}
			int num = (characterBufferReference.CharacterBuffer != null) ? (characterBufferReference.CharacterBuffer.Count - characterBufferReference.OffsetToFirstChar) : 0;
			if (characterLength > num)
			{
				throw new ArgumentOutOfRangeException("characterLength", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					num
				}));
			}
			this._charBufferRef = characterBufferReference;
			this._length = characterLength;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x001023D8 File Offset: 0x001017D8
		internal CharacterBufferRange(CharacterBufferRange characterBufferRange, int offsetToFirstChar, int characterLength)
		{
			this = new CharacterBufferRange(characterBufferRange.CharacterBuffer, characterBufferRange.OffsetToFirstChar + offsetToFirstChar, characterLength);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x001023FC File Offset: 0x001017FC
		internal CharacterBufferRange(string charString)
		{
			this = new CharacterBufferRange(new StringCharacterBuffer(charString), 0, charString.Length);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x0010241C File Offset: 0x0010181C
		internal CharacterBufferRange(CharacterBuffer charBuffer, int offsetToFirstChar, int characterLength)
		{
			this = new CharacterBufferRange(new CharacterBufferReference(charBuffer, offsetToFirstChar), characterLength);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x00102438 File Offset: 0x00101838
		internal CharacterBufferRange(TextRun textRun)
		{
			this._charBufferRef = textRun.CharacterBufferReference;
			this._length = textRun.Length;
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x060041C8 RID: 16840 RVA: 0x00102460 File Offset: 0x00101860
		public override int GetHashCode()
		{
			return this._charBufferRef.GetHashCode() ^ this._length;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual.</summary>
		/// <param name="obj">O <see cref="T:System.Object" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual; caso contrário, <see langword="false" />. Se <paramref name="o" /> não for um objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x060041C9 RID: 16841 RVA: 0x00102488 File Offset: 0x00101888
		public override bool Equals(object obj)
		{
			return obj is CharacterBufferRange && this.Equals((CharacterBufferRange)obj);
		}

		/// <summary>Determina se o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> é igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for igual ao objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> atual; caso contrário, <see langword="false" />. Se <paramref name="value" /> não for um objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x060041CA RID: 16842 RVA: 0x001024AC File Offset: 0x001018AC
		public bool Equals(CharacterBufferRange value)
		{
			return this._charBufferRef.Equals(value._charBufferRef) && this._length == value._length;
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> quanto à igualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> para mostrar que os objetos <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> especificados são iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060041CB RID: 16843 RVA: 0x001024DC File Offset: 0x001018DC
		public static bool operator ==(CharacterBufferRange left, CharacterBufferRange right)
		{
			return left.Equals(right);
		}

		/// <summary>Compare duas cadeias de caracteres de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> quanto à desigualdade.</summary>
		/// <param name="left">A primeira instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> a ser comparada.</param>
		/// <param name="right">A segunda instância de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="false" /> para mostrar que <paramref name="left" /> é igual a <paramref name="right" />; caso contrário, <see langword="true" />.</returns>
		// Token: 0x060041CC RID: 16844 RVA: 0x001024F4 File Offset: 0x001018F4
		public static bool operator !=(CharacterBufferRange left, CharacterBufferRange right)
		{
			return !(left == right);
		}

		/// <summary>Obtém uma referência ao buffer de caractere em uma cadeia de caracteres.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> valor que representa o buffer de caracteres de uma cadeia de caracteres.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060041CD RID: 16845 RVA: 0x0010250C File Offset: 0x0010190C
		public CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return this._charBufferRef;
			}
		}

		/// <summary>Obtém o número de caracteres no repositório de caracteres de origem do texto.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o número total de caracteres.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00102520 File Offset: 0x00101920
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém uma cadeia de caracteres vazia.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> do objeto cuja <see cref="P:System.Windows.Media.TextFormatting.CharacterBufferRange.Length" /> é igual a 0.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060041CF RID: 16847 RVA: 0x00102534 File Offset: 0x00101934
		public static CharacterBufferRange Empty
		{
			get
			{
				return default(CharacterBufferRange);
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0010254C File Offset: 0x0010194C
		internal bool IsEmpty
		{
			get
			{
				return this._charBufferRef.CharacterBuffer == null || this._length <= 0;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x00102574 File Offset: 0x00101974
		internal CharacterBuffer CharacterBuffer
		{
			get
			{
				return this._charBufferRef.CharacterBuffer;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x0010258C File Offset: 0x0010198C
		internal int OffsetToFirstChar
		{
			get
			{
				return this._charBufferRef.OffsetToFirstChar;
			}
		}

		// Token: 0x17000D49 RID: 3401
		internal char this[int index]
		{
			get
			{
				Invariant.Assert(index >= 0 && index < this._length);
				return this._charBufferRef.CharacterBuffer[this._charBufferRef.OffsetToFirstChar + index];
			}
		}

		// Token: 0x04001804 RID: 6148
		private CharacterBufferReference _charBufferRef;

		// Token: 0x04001805 RID: 6149
		private int _length;
	}
}
