using System;

namespace System.Windows
{
	/// <summary>Fornece um conjunto de decorações de texto predefinido estático.</summary>
	// Token: 0x020001F0 RID: 496
	public static class TextDecorations
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003080C File Offset: 0x0002FC0C
		static TextDecorations()
		{
			TextDecoration textDecoration = new TextDecoration();
			textDecoration.Location = TextDecorationLocation.Underline;
			TextDecorations.underline = new TextDecorationCollection();
			TextDecorations.underline.Add(textDecoration);
			TextDecorations.underline.Freeze();
			textDecoration = new TextDecoration();
			textDecoration.Location = TextDecorationLocation.Strikethrough;
			TextDecorations.strikethrough = new TextDecorationCollection();
			TextDecorations.strikethrough.Add(textDecoration);
			TextDecorations.strikethrough.Freeze();
			textDecoration = new TextDecoration();
			textDecoration.Location = TextDecorationLocation.OverLine;
			TextDecorations.overLine = new TextDecorationCollection();
			TextDecorations.overLine.Add(textDecoration);
			TextDecorations.overLine.Freeze();
			textDecoration = new TextDecoration();
			textDecoration.Location = TextDecorationLocation.Baseline;
			TextDecorations.baseline = new TextDecorationCollection();
			TextDecorations.baseline.Add(textDecoration);
			TextDecorations.baseline.Freeze();
		}

		/// <summary>Especifica um <see cref="T:System.Windows.TextDecoration" /> sublinhado.</summary>
		/// <returns>Um valor que representa um <see cref="T:System.Windows.TextDecoration" /> sublinhado.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x000308CC File Offset: 0x0002FCCC
		public static TextDecorationCollection Underline
		{
			get
			{
				return TextDecorations.underline;
			}
		}

		/// <summary>Especifica um tachado <see cref="T:System.Windows.TextDecoration" />.</summary>
		/// <returns>Um valor que representa um tachado <see cref="T:System.Windows.TextDecoration" />.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x000308E0 File Offset: 0x0002FCE0
		public static TextDecorationCollection Strikethrough
		{
			get
			{
				return TextDecorations.strikethrough;
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.TextDecoration" /> sobreposto.</summary>
		/// <returns>Um valor que representa um <see cref="T:System.Windows.TextDecoration" /> sobreposto.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000308F4 File Offset: 0x0002FCF4
		public static TextDecorationCollection OverLine
		{
			get
			{
				return TextDecorations.overLine;
			}
		}

		/// <summary>Especifica uma linha de base <see cref="T:System.Windows.TextDecoration" />.</summary>
		/// <returns>Um valor que representa uma linha de base <see cref="T:System.Windows.TextDecoration" />.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00030908 File Offset: 0x0002FD08
		public static TextDecorationCollection Baseline
		{
			get
			{
				return TextDecorations.baseline;
			}
		}

		// Token: 0x040007B6 RID: 1974
		private static readonly TextDecorationCollection underline;

		// Token: 0x040007B7 RID: 1975
		private static readonly TextDecorationCollection strikethrough;

		// Token: 0x040007B8 RID: 1976
		private static readonly TextDecorationCollection overLine;

		// Token: 0x040007B9 RID: 1977
		private static readonly TextDecorationCollection baseline;
	}
}
