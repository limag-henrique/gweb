using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Especifica as propriedades para um <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObject" />.</summary>
	// Token: 0x020005A0 RID: 1440
	public class TextEmbeddedObjectMetrics
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObjectMetrics" /> usando valores de largura e altura da linha de base especificados.</summary>
		/// <param name="width">Um <see cref="T:System.Double" /> que representa a largura <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObject" />.</param>
		/// <param name="height">Um <see cref="T:System.Double" /> que representa a altura <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObject" />.</param>
		/// <param name="baseline">Um <see cref="T:System.Double" /> que representa a linha de base <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObject" /> relativa à <paramref name="height" />.</param>
		// Token: 0x06004217 RID: 16919 RVA: 0x00102BBC File Offset: 0x00101FBC
		public TextEmbeddedObjectMetrics(double width, double height, double baseline)
		{
			this._width = width;
			this._height = height;
			this._baseline = baseline;
		}

		/// <summary>Obtém a largura do objeto de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a largura do objeto de texto.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x00102BE4 File Offset: 0x00101FE4
		public double Width
		{
			get
			{
				return this._width;
			}
		}

		/// <summary>Obtém a altura do objeto de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a altura do objeto de texto.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x00102BF8 File Offset: 0x00101FF8
		public double Height
		{
			get
			{
				return this._height;
			}
		}

		/// <summary>Obtém a linha de base do objeto de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a linha de base do objeto de texto em relação à sua altura.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x00102C0C File Offset: 0x0010200C
		public double Baseline
		{
			get
			{
				return this._baseline;
			}
		}

		// Token: 0x0400181D RID: 6173
		private double _width;

		// Token: 0x0400181E RID: 6174
		private double _height;

		// Token: 0x0400181F RID: 6175
		private double _baseline;
	}
}
