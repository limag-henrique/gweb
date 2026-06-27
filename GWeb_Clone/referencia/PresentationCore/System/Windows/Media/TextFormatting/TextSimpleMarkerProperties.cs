using System;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece para uma implementação de propriedades de marcador de texto.</summary>
	// Token: 0x020005B6 RID: 1462
	public class TextSimpleMarkerProperties : TextMarkerProperties
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextSimpleMarkerProperties" />.</summary>
		/// <param name="style">Um valor enumerado de <see cref="T:System.Windows.TextMarkerStyle" />.</param>
		/// <param name="offset">Um <see cref="T:System.Double" /> que representa a distância do início da linha até o final do símbolo de marcador de texto.</param>
		/// <param name="autoNumberingIndex">Um <see cref="T:System.Int32" /> valor que representa o contador de numeração automática do marcador de texto de estilo de contador.</param>
		/// <param name="textParagraphProperties">Um <see cref="T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> valor que representa as propriedades compartilhadas por todos os caracteres de texto do marcador de texto.</param>
		// Token: 0x060042F1 RID: 17137 RVA: 0x001040FC File Offset: 0x001034FC
		public TextSimpleMarkerProperties(TextMarkerStyle style, double offset, int autoNumberingIndex, TextParagraphProperties textParagraphProperties)
		{
			if (textParagraphProperties == null)
			{
				throw new ArgumentNullException("textParagraphProperties");
			}
			this._offset = offset;
			if (style != TextMarkerStyle.None)
			{
				if (!TextMarkerSource.IsKnownSymbolMarkerStyle(style))
				{
					if (!TextMarkerSource.IsKnownIndexMarkerStyle(style))
					{
						throw new ArgumentException(SR.Get("Enum_Invalid", new object[]
						{
							typeof(TextMarkerStyle)
						}), "style");
					}
					if (autoNumberingIndex < 1)
					{
						throw new ArgumentOutOfRangeException("autoNumberingIndex", SR.Get("ParameterCannotBeLessThan", new object[]
						{
							1
						}));
					}
				}
				this._textSource = new TextMarkerSource(textParagraphProperties, style, autoNumberingIndex);
			}
		}

		/// <summary>Obtém a distância do início da linha até o final do símbolo de marcador de texto.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> objeto que representa o deslocamento do símbolo de marcador de texto.</returns>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x00104198 File Offset: 0x00103598
		public sealed override double Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Obtém a fonte do texto executa usada para o marcador de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> valor que representa o texto de execução usada para o marcador de texto.</returns>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060042F3 RID: 17139 RVA: 0x001041AC File Offset: 0x001035AC
		public sealed override TextSource TextSource
		{
			get
			{
				return this._textSource;
			}
		}

		// Token: 0x04001842 RID: 6210
		private double _offset;

		// Token: 0x04001843 RID: 6211
		private TextSource _textSource;
	}
}
