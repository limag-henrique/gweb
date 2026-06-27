using System;
using System.Globalization;

namespace System.Windows
{
	/// <summary>Fornece um conjunto de valores estáticos predefinidos <see cref="T:System.Windows.FontWeight" />.</summary>
	// Token: 0x020001BB RID: 443
	public static class FontWeights
	{
		/// <summary>Especifica uma espessura da fonte "Fina".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Fina".</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001FEA8 File Offset: 0x0001F2A8
		public static FontWeight Thin
		{
			get
			{
				return new FontWeight(100);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Extraleve".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Extra light".</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001FEBC File Offset: 0x0001F2BC
		public static FontWeight ExtraLight
		{
			get
			{
				return new FontWeight(200);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Ultraleve".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Ultraleve".</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001FED4 File Offset: 0x0001F2D4
		public static FontWeight UltraLight
		{
			get
			{
				return new FontWeight(200);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Leve".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Light".</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001FEEC File Offset: 0x0001F2EC
		public static FontWeight Light
		{
			get
			{
				return new FontWeight(300);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Normal".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Normal".</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001FF04 File Offset: 0x0001F304
		public static FontWeight Normal
		{
			get
			{
				return new FontWeight(400);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Regular".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Normal".</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001FF1C File Offset: 0x0001F31C
		public static FontWeight Regular
		{
			get
			{
				return new FontWeight(400);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Média".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Média".</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001FF34 File Offset: 0x0001F334
		public static FontWeight Medium
		{
			get
			{
				return new FontWeight(500);
			}
		}

		/// <summary>Especifica uma espessura da fonte "seminegrito".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "seminegrito".</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001FF4C File Offset: 0x0001F34C
		public static FontWeight DemiBold
		{
			get
			{
				return new FontWeight(600);
			}
		}

		/// <summary>Especifica uma espessura da fonte “Seminegrito”.</summary>
		/// <returns>Um valor que representa uma espessura da fonte “Seminegrito”.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001FF64 File Offset: 0x0001F364
		public static FontWeight SemiBold
		{
			get
			{
				return new FontWeight(600);
			}
		}

		/// <summary>Especifica uma espessura da fonte “Negrito”.</summary>
		/// <returns>Um valor que representa uma espessura da fonte “Negrito”.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001FF7C File Offset: 0x0001F37C
		public static FontWeight Bold
		{
			get
			{
				return new FontWeight(700);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Extranegrito".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Extra negrito".</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001FF94 File Offset: 0x0001F394
		public static FontWeight ExtraBold
		{
			get
			{
				return new FontWeight(800);
			}
		}

		/// <summary>Especifica uma espessura da fonte "UltraNegrito".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Ultranegrito".</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001FFAC File Offset: 0x0001F3AC
		public static FontWeight UltraBold
		{
			get
			{
				return new FontWeight(800);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Preta".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Preta".</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001FFC4 File Offset: 0x0001F3C4
		public static FontWeight Black
		{
			get
			{
				return new FontWeight(900);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Pesada".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Pesada".</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001FFDC File Offset: 0x0001F3DC
		public static FontWeight Heavy
		{
			get
			{
				return new FontWeight(900);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Extrapreta".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Extra preta".</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001FFF4 File Offset: 0x0001F3F4
		public static FontWeight ExtraBlack
		{
			get
			{
				return new FontWeight(950);
			}
		}

		/// <summary>Especifica uma espessura da fonte "Ultrapreta".</summary>
		/// <returns>Um valor que representa uma espessura da fonte "Ultrapreta".</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0002000C File Offset: 0x0001F40C
		public static FontWeight UltraBlack
		{
			get
			{
				return new FontWeight(950);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00020024 File Offset: 0x0001F424
		internal static bool FontWeightStringToKnownWeight(string s, IFormatProvider provider, ref FontWeight fontWeight)
		{
			switch (s.Length)
			{
			case 4:
				if (s.Equals("Bold", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Bold;
					return true;
				}
				if (s.Equals("Thin", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Thin;
					return true;
				}
				break;
			case 5:
				if (s.Equals("Black", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Black;
					return true;
				}
				if (s.Equals("Light", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Light;
					return true;
				}
				if (s.Equals("Heavy", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Heavy;
					return true;
				}
				break;
			case 6:
				if (s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Normal;
					return true;
				}
				if (s.Equals("Medium", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Medium;
					return true;
				}
				break;
			case 7:
				if (s.Equals("Regular", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.Regular;
					return true;
				}
				break;
			case 8:
				if (s.Equals("SemiBold", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.SemiBold;
					return true;
				}
				if (s.Equals("DemiBold", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.DemiBold;
					return true;
				}
				break;
			case 9:
				if (s.Equals("ExtraBold", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.ExtraBold;
					return true;
				}
				if (s.Equals("UltraBold", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.UltraBold;
					return true;
				}
				break;
			case 10:
				if (s.Equals("ExtraLight", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.ExtraLight;
					return true;
				}
				if (s.Equals("UltraLight", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.UltraLight;
					return true;
				}
				if (s.Equals("ExtraBlack", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.ExtraBlack;
					return true;
				}
				if (s.Equals("UltraBlack", StringComparison.OrdinalIgnoreCase))
				{
					fontWeight = FontWeights.UltraBlack;
					return true;
				}
				break;
			}
			int weightValue;
			if (int.TryParse(s, NumberStyles.Integer, provider, out weightValue))
			{
				fontWeight = FontWeight.FromOpenTypeWeight(weightValue);
				return true;
			}
			return false;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00020240 File Offset: 0x0001F640
		internal static bool FontWeightToString(int weight, out string convertedValue)
		{
			if (weight <= 500)
			{
				if (weight <= 200)
				{
					if (weight == 100)
					{
						convertedValue = "Thin";
						return true;
					}
					if (weight == 200)
					{
						convertedValue = "ExtraLight";
						return true;
					}
				}
				else
				{
					if (weight == 300)
					{
						convertedValue = "Light";
						return true;
					}
					if (weight == 400)
					{
						convertedValue = "Normal";
						return true;
					}
					if (weight == 500)
					{
						convertedValue = "Medium";
						return true;
					}
				}
			}
			else if (weight <= 700)
			{
				if (weight == 600)
				{
					convertedValue = "SemiBold";
					return true;
				}
				if (weight == 700)
				{
					convertedValue = "Bold";
					return true;
				}
			}
			else
			{
				if (weight == 800)
				{
					convertedValue = "ExtraBold";
					return true;
				}
				if (weight == 900)
				{
					convertedValue = "Black";
					return true;
				}
				if (weight == 950)
				{
					convertedValue = "ExtraBlack";
					return true;
				}
			}
			convertedValue = null;
			return false;
		}
	}
}
