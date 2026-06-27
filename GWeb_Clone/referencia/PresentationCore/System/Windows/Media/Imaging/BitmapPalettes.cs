using System;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define várias paletas de cores usadas comumente por imagens de bitmap.</summary>
	// Token: 0x020005E1 RID: 1505
	public static class BitmapPalettes
	{
		/// <summary>Obtém um valor que representa uma paleta de cores em preto e branco. Essa paleta consiste em 2 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0010A4FC File Offset: 0x001098FC
		public static BitmapPalette BlackAndWhite
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedBW, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores em preto, branco e transparente. Essa paleta consiste em 3 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x0010A510 File Offset: 0x00109910
		public static BitmapPalette BlackAndWhiteTransparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedBW, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 8 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 16 cores essa paleta, que são o mesmo que a paleta do sistema.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x0010A524 File Offset: 0x00109924
		public static BitmapPalette Halftone8
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone8, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 8 cores primárias e 16 cores do sistema, com cores duplicadas removidas e 1 cor transparente adicional. Há um total de 17 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x0010A538 File Offset: 0x00109938
		public static BitmapPalette Halftone8Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone8, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 27 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 35 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x0010A54C File Offset: 0x0010994C
		public static BitmapPalette Halftone27
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone27, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 27 cores primárias e 16 cores do sistema, com cores duplicadas removidas e 1 cor transparente adicional. Há um total de 36 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x0010A560 File Offset: 0x00109960
		public static BitmapPalette Halftone27Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone27, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 64 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 72 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0010A574 File Offset: 0x00109974
		public static BitmapPalette Halftone64
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone64, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 64 cores primárias e 16 cores do sistema, com cores duplicadas removidas e 1 cor transparente adicional. Há um total de 73 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x0010A588 File Offset: 0x00109988
		public static BitmapPalette Halftone64Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone64, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 125 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 133 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x0010A59C File Offset: 0x0010999C
		public static BitmapPalette Halftone125
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone125, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 125 cores primárias, 16 cores do sistema e 1 cor transparente adicional. Cores duplicadas na paleta são removidas. Há um total de 134 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x0010A5B0 File Offset: 0x001099B0
		public static BitmapPalette Halftone125Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone125, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 216 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 224 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0010A5C4 File Offset: 0x001099C4
		public static BitmapPalette Halftone216
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone216, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 216 cores primárias, 16 cores do sistema e 1 cor transparente adicional. Cores duplicadas na paleta são removidas. Há um total de 225 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x0010A5D8 File Offset: 0x001099D8
		public static BitmapPalette Halftone216Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone216, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 252 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 256 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x0010A5EC File Offset: 0x001099EC
		public static BitmapPalette Halftone252
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone252, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 252 cores primárias, 16 cores do sistema e 1 cor transparente adicional. Cores duplicadas na paleta são removidas. Há um total de 256 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x0010A600 File Offset: 0x00109A00
		public static BitmapPalette Halftone252Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone252, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 256 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 256 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0010A614 File Offset: 0x00109A14
		public static BitmapPalette Halftone256
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone256, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 256 cores primárias, 16 cores do sistema e 1 cor transparente adicional que substitui a cor final na sequência. Cores duplicadas na paleta são removidas. Há um total de 256 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x0010A62C File Offset: 0x00109A2C
		public static BitmapPalette Halftone256Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone256, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 4 tons de cinza, variando de preto a cinza a branco. Essa paleta contém 4 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x0010A644 File Offset: 0x00109A44
		public static BitmapPalette Gray4
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray4, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 4 tons de cinza, variando de preto a cinza a branco com uma cor transparente adicional. Essa paleta contém 5 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x0010A65C File Offset: 0x00109A5C
		public static BitmapPalette Gray4Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray4, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 16 tons de cinza. Os intervalos da paleta de preto a cinza a branco. Essa paleta contém 16 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x0010A674 File Offset: 0x00109A74
		public static BitmapPalette Gray16
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray16, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 16 tons de cinza. Os intervalos da paleta de preto a cinza a branco com uma cor transparente adicional. Essa paleta contém 17 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x0010A68C File Offset: 0x00109A8C
		public static BitmapPalette Gray16Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray16, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 256 tons de cinza, variando de preto a cinza a branco. Essa paleta contém 256 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x0010A6A4 File Offset: 0x00109AA4
		public static BitmapPalette Gray256
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray256, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 256 tons de cinza, variando de preto a cinza a branco com uma cor transparente adicional. Essa paleta contém 257 cores no total.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x0010A6BC File Offset: 0x00109ABC
		public static BitmapPalette Gray256Transparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedGray256, true);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 216 cores primárias e 16 cores do sistema, com cores duplicadas removidas. Há um total de 224 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x0010A6D4 File Offset: 0x00109AD4
		public static BitmapPalette WebPalette
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone216, false);
			}
		}

		/// <summary>Obtém um valor que representa uma paleta de cores que contém 216 cores primárias e 16 cores do sistema, com cores duplicadas removidas e 1 cor transparente adicional. Há um total de 225 cores nessa paleta.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x0010A6E8 File Offset: 0x00109AE8
		public static BitmapPalette WebPaletteTransparent
		{
			get
			{
				return BitmapPalettes.FromMILPaletteType(WICPaletteType.WICPaletteTypeFixedHalftone216, true);
			}
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x0010A6FC File Offset: 0x00109AFC
		internal static BitmapPalette FromMILPaletteType(WICPaletteType type, bool hasAlpha)
		{
			BitmapPalette[] array;
			if (hasAlpha)
			{
				array = BitmapPalettes.transparentPalettes;
			}
			else
			{
				array = BitmapPalettes.opaquePalettes;
			}
			BitmapPalette bitmapPalette = array[(int)type];
			if (bitmapPalette == null)
			{
				BitmapPalette[] obj = array;
				lock (obj)
				{
					bitmapPalette = array[(int)type];
					if (bitmapPalette == null)
					{
						bitmapPalette = new BitmapPalette(type, hasAlpha);
						array[(int)type] = bitmapPalette;
					}
				}
			}
			return bitmapPalette;
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x0010A770 File Offset: 0x00109B70
		private static BitmapPalette[] transparentPalettes
		{
			get
			{
				if (BitmapPalettes.s_transparentPalettes == null)
				{
					BitmapPalettes.s_transparentPalettes = new BitmapPalette[64];
				}
				return BitmapPalettes.s_transparentPalettes;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x0010A798 File Offset: 0x00109B98
		private static BitmapPalette[] opaquePalettes
		{
			get
			{
				if (BitmapPalettes.s_opaquePalettes == null)
				{
					BitmapPalettes.s_opaquePalettes = new BitmapPalette[64];
				}
				return BitmapPalettes.s_opaquePalettes;
			}
		}

		// Token: 0x040018E4 RID: 6372
		private static BitmapPalette[] s_transparentPalettes;

		// Token: 0x040018E5 RID: 6373
		private static BitmapPalette[] s_opaquePalettes;

		// Token: 0x040018E6 RID: 6374
		private const int c_maxPalettes = 64;
	}
}
