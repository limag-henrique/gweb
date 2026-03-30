using System;

namespace System.Windows.Media
{
	/// <summary>Implementa um conjunto de objetos <see cref="T:System.Windows.Media.DashStyle" /> predefinidos.</summary>
	// Token: 0x0200037B RID: 891
	public static class DashStyles
	{
		/// <summary>Obtém um <see cref="T:System.Windows.Media.DashStyle" /> com uma propriedade <see cref="P:System.Windows.Media.DashStyle.Dashes" /> vazia.</summary>
		/// <returns>Uma sequência de traços com sem traços.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00083368 File Offset: 0x00082768
		public static DashStyle Solid
		{
			get
			{
				if (DashStyles._solid == null)
				{
					DashStyle dashStyle = new DashStyle();
					dashStyle.Freeze();
					DashStyles._solid = dashStyle;
				}
				return DashStyles._solid;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.DashStyle" /> com uma propriedade <see cref="P:System.Windows.Media.DashStyle.Dashes" /> igual a 2, 2.</summary>
		/// <returns>Uma sequência de traços de 2,2, que descreve uma sequência composta por um traço que é duas vezes, desde que a caneta <see cref="P:System.Windows.Media.Pen.Thickness" /> seguido por um espaço que é duas vezes, desde que o <see cref="P:System.Windows.Media.Pen.Thickness" />.</returns>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x00083394 File Offset: 0x00082794
		public static DashStyle Dash
		{
			get
			{
				if (DashStyles._dash == null)
				{
					DashStyle dashStyle = new DashStyle(new double[]
					{
						2.0,
						2.0
					}, 1.0);
					dashStyle.Freeze();
					DashStyles._dash = dashStyle;
				}
				return DashStyles._dash;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.DashStyle" /> com uma propriedade <see cref="P:System.Windows.Media.DashStyle.Dashes" /> igual a 0, 2.</summary>
		/// <returns>Uma sequência de traços de 0, 2.</returns>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000833E8 File Offset: 0x000827E8
		public static DashStyle Dot
		{
			get
			{
				if (DashStyles._dot == null)
				{
					DashStyle dashStyle = new DashStyle(new double[]
					{
						0.0,
						2.0
					}, 0.0);
					dashStyle.Freeze();
					DashStyles._dot = dashStyle;
				}
				return DashStyles._dot;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.DashStyle" /> com uma propriedade <see cref="P:System.Windows.Media.DashStyle.Dashes" /> igual a 2, 2, 0, 2.</summary>
		/// <returns>Uma sequência de traços de 2, 2, 0, 2.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x00083430 File Offset: 0x00082830
		public static DashStyle DashDot
		{
			get
			{
				if (DashStyles._dashDot == null)
				{
					DashStyle dashStyle = new DashStyle(new double[]
					{
						2.0,
						2.0,
						0.0,
						2.0
					}, 1.0);
					dashStyle.Freeze();
					DashStyles._dashDot = dashStyle;
				}
				return DashStyles._dashDot;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.DashStyle" /> com uma propriedade <see cref="P:System.Windows.Media.DashStyle.Dashes" /> igual a 2, 2, 0, 2, 0, 2.</summary>
		/// <returns>Uma sequência de traços de 2, 2, 0, 2, 0, 2.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x00083478 File Offset: 0x00082878
		public static DashStyle DashDotDot
		{
			get
			{
				if (DashStyles._dashDotDot == null)
				{
					DashStyle dashStyle = new DashStyle(new double[]
					{
						2.0,
						2.0,
						0.0,
						2.0,
						0.0,
						2.0
					}, 1.0);
					dashStyle.Freeze();
					DashStyles._dashDotDot = dashStyle;
				}
				return DashStyles._dashDotDot;
			}
		}

		// Token: 0x04001085 RID: 4229
		private static DashStyle _solid;

		// Token: 0x04001086 RID: 4230
		private static DashStyle _dash;

		// Token: 0x04001087 RID: 4231
		private static DashStyle _dot;

		// Token: 0x04001088 RID: 4232
		private static DashStyle _dashDot;

		// Token: 0x04001089 RID: 4233
		private static DashStyle _dashDotDot;
	}
}
