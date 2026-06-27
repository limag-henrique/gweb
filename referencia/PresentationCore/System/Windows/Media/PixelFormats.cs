using System;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa a coleção de formatos de pixel com suporte.</summary>
	// Token: 0x02000430 RID: 1072
	public static class PixelFormats
	{
		/// <summary>Obtém o formato de pixel mais adequado para a operação específica.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.PixelFormat" /> mais adequada para a operação específica.</returns>
		/// <exception cref="T:System.NotSupportedException">As propriedades <see cref="T:System.Windows.Media.PixelFormat" /> são acessadas.</exception>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000AFEE4 File Offset: 0x000AF2E4
		public static PixelFormat Default
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Default);
			}
		}

		/// <summary>Obtém o formato de pixel que especifica um bitmap da paleta com 2 cores.</summary>
		/// <returns>O formato de pixel que especifica um bitmap da paleta com 2 cores.</returns>
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000AFEF8 File Offset: 0x000AF2F8
		public static PixelFormat Indexed1
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Indexed1);
			}
		}

		/// <summary>Obtém o formato de pixel que especifica um bitmap da paleta com 4 cores.</summary>
		/// <returns>O formato de pixel que especifica um bitmap da paleta com 4 cores.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000AFF0C File Offset: 0x000AF30C
		public static PixelFormat Indexed2
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Indexed2);
			}
		}

		/// <summary>Obtém o formato de pixel que especifica um bitmap da paleta com 16 cores.</summary>
		/// <returns>O formato de pixel que especifica um bitmap da paleta com 16 cores.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000AFF20 File Offset: 0x000AF320
		public static PixelFormat Indexed4
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Indexed4);
			}
		}

		/// <summary>Obtém o formato de pixel que especifica um bitmap da paleta com 256 cores.</summary>
		/// <returns>O formato de pixel que especifica um bitmap da paleta com 256 cores.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000AFF34 File Offset: 0x000AF334
		public static PixelFormat Indexed8
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Indexed8);
			}
		}

		/// <summary>Obtém o formato de pixel preto e branco que exibe um bit de dados por pixel como preto ou branco.</summary>
		/// <returns>O formato de pixel preto e branco.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000AFF48 File Offset: 0x000AF348
		public static PixelFormat BlackWhite
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.BlackWhite);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray2" /> que exibe um canal de escala de cinza de 2 bits por pixel, permitindo 4 tons de cinza.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray2" />.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000AFF5C File Offset: 0x000AF35C
		public static PixelFormat Gray2
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Gray2);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray4" /> que exibe um canal de escala de cinza de 4 bits por pixel, permitindo 16 tons de cinza.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray4" />.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000AFF70 File Offset: 0x000AF370
		public static PixelFormat Gray4
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Gray4);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray8" /> que exibe um canal de escala de cinza de 8 bits por pixel, permitindo 256 tons de cinza.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray8" />.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000AFF84 File Offset: 0x000AF384
		public static PixelFormat Gray8
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Gray8);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr555" />. <see cref="P:System.Windows.Media.PixelFormats.Bgr555" /> é um formato sRGB com 16 BPP (bits por pixel). São alocados 5 BPP (bits por pixel) a cada canal de cor (azul, verde e vermelho).</summary>
		/// <returns>O <see cref="P:System.Windows.Media.PixelFormats.Bgr555" /> formato de pixel</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000AFF98 File Offset: 0x000AF398
		public static PixelFormat Bgr555
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgr555);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr565" />. <see cref="P:System.Windows.Media.PixelFormats.Bgr565" /> é um formato sRGB com 16 BPP (bits por pixel). São alocados 5, 6 e 5 BPP (bits por pixel), respectivamente, a cada canal de cor (azul, verde e vermelho).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr565" />.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000AFFAC File Offset: 0x000AF3AC
		public static PixelFormat Bgr565
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgr565);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb128Float" />. <see cref="P:System.Windows.Media.PixelFormats.Rgb128Float" /> é um formato ScRGB com 128 BPP (bits por pixel). São alocados 32 BPP a cada canal de cor. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb128Float" />.</returns>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000AFFC0 File Offset: 0x000AF3C0
		public static PixelFormat Rgb128Float
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Rgb128Float);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr24" />. <see cref="P:System.Windows.Media.PixelFormats.Bgr24" /> é um formato sRGB com 24 BPP (bits por pixel). São alocados 8 BPP (bits por pixel) a cada canal de cor (azul, verde e vermelho).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr24" />.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000AFFD4 File Offset: 0x000AF3D4
		public static PixelFormat Bgr24
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgr24);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb24" />. <see cref="P:System.Windows.Media.PixelFormats.Rgb24" /> é um formato sRGB com 24 BPP (bits por pixel). Cada canal de cor (vermelho, verde e azul) tem a alocação de 8 BPP (bits por pixel).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb24" />.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000AFFE8 File Offset: 0x000AF3E8
		public static PixelFormat Rgb24
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Rgb24);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr101010" />. <see cref="P:System.Windows.Media.PixelFormats.Bgr101010" /> é um formato sRGB com 32 BPP (bits por pixel). São alocados 10 BPP (bits por pixel) a cada canal de cor (azul, verde e vermelho).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr101010" />.</returns>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000AFFFC File Offset: 0x000AF3FC
		public static PixelFormat Bgr101010
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgr101010);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr32" />. <see cref="P:System.Windows.Media.PixelFormats.Bgr32" /> é um formato sRGB com 32 BPP (bits por pixel). São alocados 8 BPP (bits por pixel) a cada canal de cor (azul, verde e vermelho).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgr32" />.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000B0010 File Offset: 0x000AF410
		public static PixelFormat Bgr32
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgr32);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgra32" />. <see cref="P:System.Windows.Media.PixelFormats.Bgra32" /> é um formato sRGB com 32 BPP (bits por pixel). São alocados 8 BPP (bits por pixel) a cada canal (azul, verde, vermelho e alfa).</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Bgra32" />.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000B0024 File Offset: 0x000AF424
		public static PixelFormat Bgra32
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Bgra32);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Pbgra32" />. <see cref="P:System.Windows.Media.PixelFormats.Pbgra32" /> é um formato sRGB com 32 BPP (bits por pixel). São alocados 8 BPP (bits por pixel) a cada canal (azul, verde, vermelho e alfa). Cada canal de cor é pré-multiplicado pelo valor alfa.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Pbgra32" />.</returns>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000B0038 File Offset: 0x000AF438
		public static PixelFormat Pbgra32
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Pbgra32);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb48" />. <see cref="P:System.Windows.Media.PixelFormats.Rgb48" /> é um formato sRGB com 48 BPP (bits por pixel). São alocados 16 BPP (bits por pixel) a cada canal de cor (vermelho, verde e azul). Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgb48" />.</returns>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000B004C File Offset: 0x000AF44C
		public static PixelFormat Rgb48
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Rgb48);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgba64" />. <see cref="P:System.Windows.Media.PixelFormats.Rgba64" /> é um formato sRGB com 64 BPP (bits por pixel). São alocados 16 BPP (bits por pixel) a cada canal (vermelho, verde, azul e alfa). Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgba64" />.</returns>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000B0060 File Offset: 0x000AF460
		public static PixelFormat Rgba64
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Rgba64);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Prgba64" />. <see cref="P:System.Windows.Media.PixelFormats.Prgba64" /> é um formato sRGB com 64 BPP (bits por pixel). São alocados 32 BPP (bits por pixel) a cada canal (azul, verde, vermelho e alfa). Cada canal de cor é pré-multiplicado pelo valor alfa. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Prgba64" />.</returns>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000B0074 File Offset: 0x000AF474
		public static PixelFormat Prgba64
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Prgba64);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray16" /> que exibe um canal de escala de cinza de 16 bits por pixel, permitindo 65536 tons de cinza. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray16" />.</returns>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000B0088 File Offset: 0x000AF488
		public static PixelFormat Gray16
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Gray16);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray32Float" />. <see cref="P:System.Windows.Media.PixelFormats.Gray32Float" /> exibe um canal de escala de cinza de 32 BPP (bits por pixel), permitindo mais de 4 bilhões de tons de cinza. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Gray32Float" />.</returns>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000B009C File Offset: 0x000AF49C
		public static PixelFormat Gray32Float
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Gray32Float);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgba128Float" />. <see cref="P:System.Windows.Media.PixelFormats.Rgba128Float" /> é um formato ScRGB com 128 BPP (bits por pixel). São alocados 32 BPP (bits por pixel) a cada canal de cor. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Rgba128Float" />.</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000B00B0 File Offset: 0x000AF4B0
		public static PixelFormat Rgba128Float
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Rgba128Float);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Prgba128Float" />. <see cref="P:System.Windows.Media.PixelFormats.Prgba128Float" /> é um formato ScRGB com 128 BPP (bits por pixel). São alocados 32 BPP (bits por pixel) a cada canal (vermelho, verde, azul e alfa). Cada canal de cor é pré-multiplicado pelo valor alfa. Esse formato tem uma gama de 1.0.</summary>
		/// <returns>O formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Prgba128Float" />.</returns>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000B00C4 File Offset: 0x000AF4C4
		public static PixelFormat Prgba128Float
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Prgba128Float);
			}
		}

		/// <summary>Obtém o formato de pixel <see cref="P:System.Windows.Media.PixelFormats.Cmyk32" /> que exibe 32 BPP (bits por pixel) com cada canal de cor (ciano, magenta, amarelo e preto) tendo a alocação de 8 BPP (bits por pixel).</summary>
		/// <returns>O formato de pixel CMYK32.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000B00D8 File Offset: 0x000AF4D8
		public static PixelFormat Cmyk32
		{
			get
			{
				return new PixelFormat(PixelFormatEnum.Cmyk32);
			}
		}
	}
}
