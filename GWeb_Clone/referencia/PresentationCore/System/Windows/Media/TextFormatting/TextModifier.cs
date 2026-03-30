using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa um especializado sequência de texto que pode ser usado para modificar as propriedades de texto é executado dentro de seu escopo.</summary>
	// Token: 0x020005AE RID: 1454
	public abstract class TextModifier : TextRun
	{
		/// <summary>Obtém o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> para o <see cref="T:System.Windows.Media.TextFormatting.TextModifier" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x00103B4C File Offset: 0x00102F4C
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return default(CharacterBufferReference);
			}
		}

		/// <summary>Recupera o <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> para um sequência de texto.</summary>
		/// <param name="properties">O <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> para uma sequência de texto ou o valor retornado <see cref="M:System.Windows.Media.TextFormatting.TextModifier.ModifyProperties(System.Windows.Media.TextFormatting.TextRunProperties)" /> para um modificador de texto aninhadas.</param>
		/// <returns>O real <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado pelo <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />, sujeito à modificação adicional por <see cref="T:System.Windows.Media.TextFormatting.TextModifier" /> objetos em escopos externos.</returns>
		// Token: 0x06004288 RID: 17032
		public abstract TextRunProperties ModifyProperties(TextRunProperties properties);

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.TextFormatting.TextModifier" /> dá suporte ao <see cref="T:System.Windows.FlowDirection" /> para o escopo atual do texto.</summary>
		/// <returns>
		///   <see langword="true" /> Se <see cref="T:System.Windows.Media.TextFormatting.TextModifier" /> suporta <see cref="T:System.Windows.FlowDirection" /> para o escopo atual de texto; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06004289 RID: 17033
		public abstract bool HasDirectionalEmbedding { get; }

		/// <summary>Obtém o <see cref="T:System.Windows.FlowDirection" /> para o <see cref="T:System.Windows.Media.TextFormatting.TextModifier" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.FlowDirection" />.</returns>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600428A RID: 17034
		public abstract FlowDirection FlowDirection { get; }
	}
}
