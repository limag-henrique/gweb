using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using MS.Internal.PresentationCore;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa uma coleção de glifos de caractere de tipos faces de tipo distintas.</summary>
	// Token: 0x0200059B RID: 1435
	public class TextCharacters : TextRun, ITextSymbols, IShapeableTextCollector
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextCharacters" /> usando uma matriz de caracteres especificada.</summary>
		/// <param name="characterArray">A matriz <see cref="T:System.Char" />.</param>
		/// <param name="offsetToFirstChar">O deslocamento para o primeiro caractere a ser usado em <paramref name="characterArray" />.</param>
		/// <param name="length">O comprimento de caracteres a serem usados em <paramref name="characterArray" />.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres em <paramref name="characterArray" />.</param>
		// Token: 0x060041FE RID: 16894 RVA: 0x001028AC File Offset: 0x00101CAC
		public TextCharacters(char[] characterArray, int offsetToFirstChar, int length, TextRunProperties textRunProperties) : this(new CharacterBufferReference(characterArray, offsetToFirstChar), length, textRunProperties)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextCharacters" /> usando uma cadeia de caracteres especificada.</summary>
		/// <param name="characterString">O <see cref="T:System.String" /> que contém os caracteres de texto.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres em <paramref name="characterString" />.</param>
		// Token: 0x060041FF RID: 16895 RVA: 0x001028CC File Offset: 0x00101CCC
		public TextCharacters(string characterString, TextRunProperties textRunProperties) : this(characterString, 0, (characterString == null) ? 0 : characterString.Length, textRunProperties)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextCharacters" /> usando uma subcadeia de caracteres especificada.</summary>
		/// <param name="characterString">O <see cref="T:System.String" /> que contém os caracteres de texto.</param>
		/// <param name="offsetToFirstChar">O deslocamento para o primeiro caractere a ser usado em <paramref name="characterString" />.</param>
		/// <param name="length">O comprimento de caracteres a serem usados em <paramref name="characterString" />.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres em <paramref name="characterString" />.</param>
		// Token: 0x06004200 RID: 16896 RVA: 0x001028F0 File Offset: 0x00101CF0
		public TextCharacters(string characterString, int offsetToFirstChar, int length, TextRunProperties textRunProperties) : this(new CharacterBufferReference(characterString, offsetToFirstChar), length, textRunProperties)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextCharacters" /> usando uma cadeia de caracteres desprotegidos especificada.</summary>
		/// <param name="unsafeCharacterString">Ponteiro para cadeia de caracteres.</param>
		/// <param name="length">O comprimento de caracteres a serem usados em <paramref name="unsafeCharacterString" />.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres em <paramref name="unsafeCharacterString" />.</param>
		// Token: 0x06004201 RID: 16897 RVA: 0x00102910 File Offset: 0x00101D10
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe TextCharacters(char* unsafeCharacterString, int length, TextRunProperties textRunProperties) : this(new CharacterBufferReference(unsafeCharacterString, length), length, textRunProperties)
		{
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x0010292C File Offset: 0x00101D2C
		private TextCharacters(CharacterBufferReference characterBufferReference, int length, TextRunProperties textRunProperties)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (textRunProperties == null)
			{
				throw new ArgumentNullException("textRunProperties");
			}
			if (textRunProperties.Typeface == null)
			{
				throw new ArgumentNullException("textRunProperties.Typeface");
			}
			if (textRunProperties.CultureInfo == null)
			{
				throw new ArgumentNullException("textRunProperties.CultureInfo");
			}
			if (textRunProperties.FontRenderingEmSize <= 0.0)
			{
				throw new ArgumentOutOfRangeException("textRunProperties.FontRenderingEmSize", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			this._characterBufferReference = characterBufferReference;
			this._length = length;
			this._textRunProperties = textRunProperties;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> para o <see cref="T:System.Windows.Media.TextFormatting.TextCharacters" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> que representa os caracteres de texto.</returns>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x001029C8 File Offset: 0x00101DC8
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return this._characterBufferReference;
			}
		}

		/// <summary>Obtém o comprimento dos caracteres de texto.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> objeto que representa o comprimento dos caracteres de texto.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x001029DC File Offset: 0x00101DDC
		public sealed override int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o conjunto de propriedades compartilhadas por todos os caracteres de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> valor que representa as propriedades compartilhadas por todos os caracteres de texto.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x001029F0 File Offset: 0x00101DF0
		public sealed override TextRunProperties Properties
		{
			get
			{
				return this._textRunProperties;
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x00102A04 File Offset: 0x00101E04
		IList<TextShapeableSymbols> ITextSymbols.GetTextShapeableSymbols(GlyphingCache glyphingCache, CharacterBufferReference characterBufferReference, int length, bool rightToLeft, bool isRightToLeftParagraph, CultureInfo digitCulture, TextModifierScope textModifierScope, TextFormattingMode textFormattingMode, bool isSideways)
		{
			if (characterBufferReference.CharacterBuffer == null)
			{
				throw new ArgumentNullException("characterBufferReference.CharacterBuffer");
			}
			int num = characterBufferReference.OffsetToFirstChar - this._characterBufferReference.OffsetToFirstChar;
			if (length < 0 || num + length > this._length)
			{
				length = this._length - num;
			}
			TextRunProperties textRunProperties = this._textRunProperties;
			if (textModifierScope != null)
			{
				textRunProperties = textModifierScope.ModifyProperties(textRunProperties);
			}
			int num2;
			if (!rightToLeft && textRunProperties.Typeface.CheckFastPathNominalGlyphs(new CharacterBufferRange(characterBufferReference, length), textRunProperties.FontRenderingEmSize, (float)textRunProperties.PixelsPerDip, 1.0, 1.7976931348623157E+308, true, digitCulture != null, CultureMapper.GetSpecificCulture(textRunProperties.CultureInfo), textFormattingMode, isSideways, false, out num2) && length == num2)
			{
				return new TextShapeableCharacters[]
				{
					new TextShapeableCharacters(new CharacterBufferRange(characterBufferReference, num2), textRunProperties, textRunProperties.FontRenderingEmSize, new ItemProps(), null, false, textFormattingMode, isSideways)
				};
			}
			IList<TextShapeableSymbols> list = new List<TextShapeableSymbols>(2);
			glyphingCache.GetShapeableText(textRunProperties.Typeface, characterBufferReference, length, textRunProperties, digitCulture, isRightToLeftParagraph, list, this, textFormattingMode);
			return list;
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x00102B04 File Offset: 0x00101F04
		void IShapeableTextCollector.Add(IList<TextShapeableSymbols> shapeables, CharacterBufferRange characterBufferRange, TextRunProperties textRunProperties, ItemProps textItem, ShapeTypeface shapeTypeface, double emScale, bool nullShape, TextFormattingMode textFormattingMode)
		{
			shapeables.Add(new TextShapeableCharacters(characterBufferRange, textRunProperties, textRunProperties.FontRenderingEmSize * emScale, textItem, shapeTypeface, nullShape, textFormattingMode, false));
		}

		// Token: 0x04001814 RID: 6164
		private CharacterBufferReference _characterBufferReference;

		// Token: 0x04001815 RID: 6165
		private int _length;

		// Token: 0x04001816 RID: 6166
		private TextRunProperties _textRunProperties;
	}
}
