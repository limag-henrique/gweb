using System;
using System.ComponentModel;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece serviços para formatação de texto e quebra de linhas de texto usando um cliente de layout de texto personalizado.</summary>
	// Token: 0x020005A4 RID: 1444
	public abstract class TextFormatter : IDisposable
	{
		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" /> com o modo de formatação especificado. Este é um método estático.</summary>
		/// <param name="textFormattingMode">O <see cref="T:System.Windows.Media.TextFormattingMode" /> que especifica o layout de texto para o <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</returns>
		// Token: 0x06004226 RID: 16934 RVA: 0x00102D64 File Offset: 0x00102164
		public static TextFormatter Create(TextFormattingMode textFormattingMode)
		{
			if (textFormattingMode < TextFormattingMode.Ideal || textFormattingMode > TextFormattingMode.Display)
			{
				throw new InvalidEnumArgumentException("textFormattingMode", (int)textFormattingMode, typeof(TextFormattingMode));
			}
			return new TextFormatterImp(textFormattingMode);
		}

		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />. Este é um método estático.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</returns>
		// Token: 0x06004227 RID: 16935 RVA: 0x00102D98 File Offset: 0x00102198
		public static TextFormatter Create()
		{
			return new TextFormatterImp();
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x00102DAC File Offset: 0x001021AC
		[FriendAccessAllowed]
		internal static TextFormatter CreateFromContext(TextFormatterContext soleContext)
		{
			return new TextFormatterImp(soleContext, TextFormattingMode.Ideal);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00102DC0 File Offset: 0x001021C0
		[FriendAccessAllowed]
		internal static TextFormatter CreateFromContext(TextFormatterContext soleContext, TextFormattingMode textFormattingMode)
		{
			return new TextFormatterImp(soleContext, textFormattingMode);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x00102DD4 File Offset: 0x001021D4
		[FriendAccessAllowed]
		internal static TextFormatter FromCurrentDispatcher()
		{
			return TextFormatter.FromCurrentDispatcher(TextFormattingMode.Ideal);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x00102DE8 File Offset: 0x001021E8
		[FriendAccessAllowed]
		internal static TextFormatter FromCurrentDispatcher(TextFormattingMode textFormattingMode)
		{
			Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
			if (currentDispatcher == null)
			{
				throw new ArgumentException(SR.Get("CurrentDispatcherNotFound"));
			}
			TextFormatter textFormatter;
			if (textFormattingMode == TextFormattingMode.Display)
			{
				textFormatter = (TextFormatterImp)currentDispatcher.Reserved4;
			}
			else
			{
				textFormatter = (TextFormatterImp)currentDispatcher.Reserved1;
			}
			if (textFormatter == null)
			{
				object staticLock = TextFormatter._staticLock;
				lock (staticLock)
				{
					if (textFormatter == null)
					{
						textFormatter = TextFormatter.Create(textFormattingMode);
						if (textFormattingMode == TextFormattingMode.Display)
						{
							currentDispatcher.Reserved4 = textFormatter;
						}
						else
						{
							currentDispatcher.Reserved1 = textFormatter;
						}
					}
				}
			}
			Invariant.Assert(textFormatter != null);
			return textFormatter;
		}

		/// <summary>Libera todos os recursos gerenciados e não gerenciados usados pelo objeto <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</summary>
		// Token: 0x0600422C RID: 16940 RVA: 0x00102E90 File Offset: 0x00102290
		public virtual void Dispose()
		{
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> que é usado para formatar e exibir o conteúdo do documento.</summary>
		/// <param name="textSource">Um valor <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa a origem do texto para a linha.</param>
		/// <param name="firstCharIndex">Um valor <see cref="T:System.Int32" /> que especifica o índice de caracteres do caractere inicial na linha.</param>
		/// <param name="paragraphWidth">Um valor <see cref="T:System.Double" /> que especifica a largura do parágrafo que a linha preenche.</param>
		/// <param name="paragraphProperties">Um valor <see cref="T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> que representa as propriedades do parágrafo, como direção do fluxo, alinhamento ou recuo.</param>
		/// <param name="previousLineBreak">Um valor <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" /> que especifica o estado de formatador de texto, em termos do lugar em que a linha no parágrafo anterior foi interrompida pelo processo de formatação de texto.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> que representa uma linha de texto que pode ser exibida.</returns>
		// Token: 0x0600422D RID: 16941
		public abstract TextLine FormatLine(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak);

		/// <summary>Cria um <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> que é usado para formatar e exibir o conteúdo do documento.</summary>
		/// <param name="textSource">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa a origem do texto para a linha.</param>
		/// <param name="firstCharIndex">Um valor <see cref="T:System.Int32" /> que especifica o índice de caracteres do caractere inicial na linha.</param>
		/// <param name="paragraphWidth">Um valor <see cref="T:System.Double" /> que especifica a largura do parágrafo que a linha preenche.</param>
		/// <param name="paragraphProperties">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> que representa as propriedades do parágrafo, como direção do fluxo, alinhamento ou recuo.</param>
		/// <param name="previousLineBreak">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" /> que especifica o estado de formatador de texto, em termos do lugar em que a linha no parágrafo anterior foi interrompida pelo processo de formatação de texto.</param>
		/// <param name="textRunCache">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextRunCache" /> que representa o mecanismo de cache para o layout do texto.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> que representa uma linha de texto que pode ser exibida.</returns>
		// Token: 0x0600422E RID: 16942
		public abstract TextLine FormatLine(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache);

		// Token: 0x0600422F RID: 16943
		[FriendAccessAllowed]
		internal abstract TextLine RecreateLine(TextSource textSource, int firstCharIndex, int lineLength, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache);

		// Token: 0x06004230 RID: 16944
		[FriendAccessAllowed]
		internal abstract TextParagraphCache CreateParagraphCache(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache);

		/// <summary>Retorna um valor que representa a menor e a maior largura de parágrafo possível que contém totalmente o conteúdo de texto especificado.</summary>
		/// <param name="textSource">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa a origem do texto para a linha.</param>
		/// <param name="firstCharIndex">Um valor <see cref="T:System.Int32" /> que especifica o índice de caracteres do caractere inicial na linha.</param>
		/// <param name="paragraphProperties">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> que representa as propriedades do parágrafo, como direção do fluxo, alinhamento ou recuo.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.MinMaxParagraphWidth" /> que representa a menor e a maior largura de parágrafo possível que contém totalmente o conteúdo de texto especificado.</returns>
		// Token: 0x06004231 RID: 16945
		public abstract MinMaxParagraphWidth FormatMinMaxParagraphWidth(TextSource textSource, int firstCharIndex, TextParagraphProperties paragraphProperties);

		/// <summary>Retorna um valor que representa a menor e a maior largura de parágrafo possível que contém totalmente o conteúdo de texto especificado.</summary>
		/// <param name="textSource">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa a origem do texto para a linha.</param>
		/// <param name="firstCharIndex">Um valor <see cref="T:System.Int32" /> que especifica o índice de caracteres do caractere inicial na linha.</param>
		/// <param name="paragraphProperties">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> que representa as propriedades do parágrafo, como direção do fluxo, alinhamento ou recuo.</param>
		/// <param name="textRunCache">Um objeto <see cref="T:System.Windows.Media.TextFormatting.TextRunCache" /> que representa o mecanismo de cache para o layout do texto.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.MinMaxParagraphWidth" /> que representa a menor e a maior largura de parágrafo possível que contém totalmente o conteúdo de texto especificado.</returns>
		// Token: 0x06004232 RID: 16946
		public abstract MinMaxParagraphWidth FormatMinMaxParagraphWidth(TextSource textSource, int firstCharIndex, TextParagraphProperties paragraphProperties, TextRunCache textRunCache);

		// Token: 0x04001823 RID: 6179
		private static object _staticLock = new object();
	}
}
