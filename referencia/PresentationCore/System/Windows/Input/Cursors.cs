using System;

namespace System.Windows.Input
{
	/// <summary>Define um conjunto de cursores padrão.</summary>
	// Token: 0x0200022F RID: 559
	public static class Cursors
	{
		/// <summary>Obtém um cursor especial que é invisível.</summary>
		/// <returns>A nenhum cursor.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003B4DC File Offset: 0x0003A8DC
		public static Cursor None
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.None);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> que indica que uma região específica é inválida para determinada operação.</summary>
		/// <returns>O cursor não.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003B4F0 File Offset: 0x0003A8F0
		public static Cursor No
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.No);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de Seta.</summary>
		/// <returns>O cursor de seta.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003B504 File Offset: 0x0003A904
		public static Cursor Arrow
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Arrow);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> que aparece quando um aplicativo está sendo iniciado.</summary>
		/// <returns>O cursor AppStarting.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0003B518 File Offset: 0x0003A918
		public static Cursor AppStarting
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.AppStarting);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de cruz.</summary>
		/// <returns>Um cursor de cruz.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003B52C File Offset: 0x0003A92C
		public static Cursor Cross
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Cross);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de ajuda que é uma combinação de uma seta e um ponto de interrogação.</summary>
		/// <returns>O cursor de Ajuda.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0003B540 File Offset: 0x0003A940
		public static Cursor Help
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Help);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> em forma de I, usado para mostrar o local em que o cursor de texto é exibido ao clicar no mouse.</summary>
		/// <returns>O cursor IBeam.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003B554 File Offset: 0x0003A954
		public static Cursor IBeam
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.IBeam);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> com dimensionamento de quatro pontas, que consiste em quatro setas unidas que apontam para o norte, sul, leste e oeste.</summary>
		/// <returns>Um cursor de dimensionamento de quatro pontas.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003B568 File Offset: 0x0003A968
		public static Cursor SizeAll
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.SizeAll);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> com dimensionamento de duas pontas para o nordeste/sudoeste.</summary>
		/// <returns>Um cursor de dimensionamento de duas pontas do nordeste/sudoeste.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003B57C File Offset: 0x0003A97C
		public static Cursor SizeNESW
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.SizeNESW);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> com dimensionamento de duas pontas para o norte/sul.</summary>
		/// <returns>Um cursor de dimensionamento de duas pontas do Norte/Sul.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003B590 File Offset: 0x0003A990
		public static Cursor SizeNS
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.SizeNS);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> com dimensionamento de duas pontas para o noroeste/sudeste.</summary>
		/// <returns>Um cursor de dimensionamento do Noroeste/Sudoeste de duas pontas.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003B5A4 File Offset: 0x0003A9A4
		public static Cursor SizeNWSE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.SizeNWSE);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> com dimensionamento de duas pontas para o oeste/leste.</summary>
		/// <returns>Um cursor de dimensionamento de duas pontas do Oeste/Leste.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0003B5B8 File Offset: 0x0003A9B8
		public static Cursor SizeWE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.SizeWE);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de seta para cima, que normalmente é usado para identificar um ponto de inserção.</summary>
		/// <returns>Um cursor de seta para cima.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003B5CC File Offset: 0x0003A9CC
		public static Cursor UpArrow
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.UpArrow);
			}
		}

		/// <summary>Especifica um <see cref="T:System.Windows.Input.Cursor" /> de espera (ou ampulheta).</summary>
		/// <returns>Um cursor de espera.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0003B5E0 File Offset: 0x0003A9E0
		public static Cursor Wait
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Wait);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de mão.</summary>
		/// <returns>O cursor de mão.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003B5F4 File Offset: 0x0003A9F4
		public static Cursor Hand
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Hand);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de caneta.</summary>
		/// <returns>O cursor da caneta.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003B608 File Offset: 0x0003AA08
		public static Cursor Pen
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.Pen);
			}
		}

		/// <summary>Obtém o cursor de rolagem para o norte/sul.</summary>
		/// <returns>Um Role Norte/Sul <see cref="T:System.Windows.Input.Cursor" />.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003B61C File Offset: 0x0003AA1C
		public static Cursor ScrollNS
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollNS);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o leste/oeste.</summary>
		/// <returns>Um Oeste/Leste de cursor de rolagem.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003B630 File Offset: 0x0003AA30
		public static Cursor ScrollWE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollWE);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem completa.</summary>
		/// <returns>A rolagem todos os cursores.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003B644 File Offset: 0x0003AA44
		public static Cursor ScrollAll
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollAll);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o norte.</summary>
		/// <returns>Um cursor de Norte de rolagem.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0003B658 File Offset: 0x0003AA58
		public static Cursor ScrollN
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollN);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o sul.</summary>
		/// <returns>O cursor de rolagem do Sul.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003B66C File Offset: 0x0003AA6C
		public static Cursor ScrollS
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollS);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o oeste.</summary>
		/// <returns>O cursor do Oeste de rolagem.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003B680 File Offset: 0x0003AA80
		public static Cursor ScrollW
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollW);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o leste.</summary>
		/// <returns>Um cursor de Leste de rolagem.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0003B694 File Offset: 0x0003AA94
		public static Cursor ScrollE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollE);
			}
		}

		/// <summary>Obtém um cursor de rolagem para o noroeste.</summary>
		/// <returns>A rolagem Noroeste <see cref="T:System.Windows.Input.Cursor" />.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003B6A8 File Offset: 0x0003AAA8
		public static Cursor ScrollNW
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollNW);
			}
		}

		/// <summary>Obtém o cursor de rolagem para o nordeste.</summary>
		/// <returns>Uma rolagem nordeste <see cref="T:System.Windows.Input.Cursor" />.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0003B6BC File Offset: 0x0003AABC
		public static Cursor ScrollNE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollNE);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o sudoeste.</summary>
		/// <returns>O cursor de rolagem sudoeste.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003B6D0 File Offset: 0x0003AAD0
		public static Cursor ScrollSW
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollSW);
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Input.Cursor" /> de rolagem para o sul/leste.</summary>
		/// <returns>O Sul/Leste de cursor de rolagem.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003B6E4 File Offset: 0x0003AAE4
		public static Cursor ScrollSE
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ScrollSE);
			}
		}

		/// <summary>Obtém a seta com um <see cref="T:System.Windows.Input.Cursor" /> de CD.</summary>
		/// <returns>O cursor arrowCd.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003B6F8 File Offset: 0x0003AAF8
		public static Cursor ArrowCD
		{
			get
			{
				return Cursors.EnsureCursor(CursorType.ArrowCD);
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003B70C File Offset: 0x0003AB0C
		internal static Cursor EnsureCursor(CursorType cursorType)
		{
			if (Cursors._stockCursors[(int)cursorType] == null)
			{
				Cursors._stockCursors[(int)cursorType] = new Cursor(cursorType);
			}
			return Cursors._stockCursors[(int)cursorType];
		}

		// Token: 0x04000872 RID: 2162
		private static int _cursorTypeCount = 28;

		// Token: 0x04000873 RID: 2163
		private static Cursor[] _stockCursors = new Cursor[Cursors._cursorTypeCount];
	}
}
