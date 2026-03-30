using System;
using System.Globalization;

namespace System.Windows
{
	/// <summary>Fornece um conjunto de valores estáticos predefinidos <see cref="T:System.Windows.FontStretch" />.</summary>
	// Token: 0x020001B8 RID: 440
	public static class FontStretches
	{
		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> ultracondensado.</summary>
		/// <returns>Um valor que representa um ultracondensado <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001F830 File Offset: 0x0001EC30
		public static FontStretch UltraCondensed
		{
			get
			{
				return new FontStretch(1);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> extracondensado.</summary>
		/// <returns>Um valor que representa um extracondensado <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001F844 File Offset: 0x0001EC44
		public static FontStretch ExtraCondensed
		{
			get
			{
				return new FontStretch(2);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> condensado.</summary>
		/// <returns>Um valor que representa um condensado <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001F858 File Offset: 0x0001EC58
		public static FontStretch Condensed
		{
			get
			{
				return new FontStretch(3);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> semicondensado.</summary>
		/// <returns>Um valor que representa um semicondensado <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001F86C File Offset: 0x0001EC6C
		public static FontStretch SemiCondensed
		{
			get
			{
				return new FontStretch(4);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> normal.</summary>
		/// <returns>Um valor que representa um normal <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001F880 File Offset: 0x0001EC80
		public static FontStretch Normal
		{
			get
			{
				return new FontStretch(5);
			}
		}

		/// <summary>Especifica um meio <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <returns>Um valor que representa uma mídia <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001F894 File Offset: 0x0001EC94
		public static FontStretch Medium
		{
			get
			{
				return new FontStretch(5);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> semiexpandido.</summary>
		/// <returns>Um valor que representa um semiexpandido <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001F8A8 File Offset: 0x0001ECA8
		public static FontStretch SemiExpanded
		{
			get
			{
				return new FontStretch(6);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> expandido.</summary>
		/// <returns>Um valor que representa uma expandida <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001F8BC File Offset: 0x0001ECBC
		public static FontStretch Expanded
		{
			get
			{
				return new FontStretch(7);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> extraexpandido.</summary>
		/// <returns>Um valor que representa um extraexpandido <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001F8D0 File Offset: 0x0001ECD0
		public static FontStretch ExtraExpanded
		{
			get
			{
				return new FontStretch(8);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.FontStretch" /> ultraexpandido.</summary>
		/// <returns>Um valor que representa um ultraexpandido <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001F8E4 File Offset: 0x0001ECE4
		public static FontStretch UltraExpanded
		{
			get
			{
				return new FontStretch(9);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001F8F8 File Offset: 0x0001ECF8
		internal static bool FontStretchStringToKnownStretch(string s, IFormatProvider provider, ref FontStretch fontStretch)
		{
			switch (s.Length)
			{
			case 6:
				if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.Normal;
					return true;
				}
				if (s.Equals("Medium", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.Medium;
					return true;
				}
				break;
			case 8:
				if (s.Equals("Expanded", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.Expanded;
					return true;
				}
				break;
			case 9:
				if (s.Equals("Condensed", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.Condensed;
					return true;
				}
				break;
			case 12:
				if (s.Equals("SemiExpanded", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.SemiExpanded;
					return true;
				}
				break;
			case 13:
				if (s.Equals("SemiCondensed", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.SemiCondensed;
					return true;
				}
				if (s.Equals("ExtraExpanded", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.ExtraExpanded;
					return true;
				}
				if (s.Equals("UltraExpanded", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.UltraExpanded;
					return true;
				}
				break;
			case 14:
				if (s.Equals("UltraCondensed", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.UltraCondensed;
					return true;
				}
				if (s.Equals("ExtraCondensed", StringComparison.OrdinalIgnoreCase))
				{
					fontStretch = FontStretches.ExtraCondensed;
					return true;
				}
				break;
			}
			int stretchValue;
			if (int.TryParse(s, NumberStyles.Integer, provider, out stretchValue))
			{
				fontStretch = FontStretch.FromOpenTypeStretch(stretchValue);
				return true;
			}
			return false;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001FA74 File Offset: 0x0001EE74
		internal static bool FontStretchToString(int stretch, out string convertedValue)
		{
			switch (stretch)
			{
			case 1:
				convertedValue = "UltraCondensed";
				return true;
			case 2:
				convertedValue = "ExtraCondensed";
				return true;
			case 3:
				convertedValue = "Condensed";
				return true;
			case 4:
				convertedValue = "SemiCondensed";
				return true;
			case 5:
				convertedValue = "Normal";
				return true;
			case 6:
				convertedValue = "SemiExpanded";
				return true;
			case 7:
				convertedValue = "Expanded";
				return true;
			case 8:
				convertedValue = "ExtraExpanded";
				return true;
			case 9:
				convertedValue = "UltraExpanded";
				return true;
			default:
				convertedValue = null;
				return false;
			}
		}
	}
}
