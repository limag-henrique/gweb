using System;

namespace System.Windows
{
	/// <summary>Fornece um conjunto de valores estáticos predefinidos <see cref="T:System.Windows.FontStyle" />.</summary>
	// Token: 0x020001B5 RID: 437
	public static class FontStyles
	{
		/// <summary>Especifica um <see cref="T:System.Windows.FontStyle" /> normal.</summary>
		/// <returns>Um valor que representa um normal <see cref="T:System.Windows.FontStyle" />.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001F408 File Offset: 0x0001E808
		public static FontStyle Normal
		{
			get
			{
				return new FontStyle(0);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStyle" /> oblíquo.</summary>
		/// <returns>Um valor que representa um Oblíquo <see cref="T:System.Windows.FontStyle" />.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001F41C File Offset: 0x0001E81C
		public static FontStyle Oblique
		{
			get
			{
				return new FontStyle(1);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStyle" /> itálico.</summary>
		/// <returns>Um valor que representa um <see cref="T:System.Windows.FontStyle" /> itálico.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001F430 File Offset: 0x0001E830
		public static FontStyle Italic
		{
			get
			{
				return new FontStyle(2);
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001F444 File Offset: 0x0001E844
		internal static bool FontStyleStringToKnownStyle(string s, IFormatProvider provider, ref FontStyle fontStyle)
		{
			if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
			{
				fontStyle = FontStyles.Normal;
				return true;
			}
			if (s.Equals("Italic", StringComparison.OrdinalIgnoreCase))
			{
				fontStyle = FontStyles.Italic;
				return true;
			}
			if (s.Equals("Oblique", StringComparison.OrdinalIgnoreCase))
			{
				fontStyle = FontStyles.Oblique;
				return true;
			}
			return false;
		}
	}
}
