using System;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Descreve o local e a cor de um ponto de transição em um gradiente.</summary>
	// Token: 0x020003B4 RID: 948
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class GradientStop : Animatable, IFormattable
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GradientStop" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002419 RID: 9241 RVA: 0x00091588 File Offset: 0x00090988
		public new GradientStop Clone()
		{
			return (GradientStop)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GradientStop" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600241A RID: 9242 RVA: 0x000915A0 File Offset: 0x000909A0
		public new GradientStop CloneCurrentValue()
		{
			return (GradientStop)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a cor da parada de gradiente.</summary>
		/// <returns>A cor da parada de gradiente. O valor padrão é <see cref="P:System.Windows.Media.Colors.Transparent" />.</returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000915B8 File Offset: 0x000909B8
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x000915D8 File Offset: 0x000909D8
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(GradientStop.ColorProperty);
			}
			set
			{
				base.SetValueInternal(GradientStop.ColorProperty, value);
			}
		}

		/// <summary>Obtém o local da parada de gradiente dentro do vetor de gradiente.</summary>
		/// <returns>O local relativo dessa parada de gradiente ao longo do vetor de gradiente. O valor padrão é 0,0.</returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000915F8 File Offset: 0x000909F8
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x00091618 File Offset: 0x00090A18
		public double Offset
		{
			get
			{
				return (double)base.GetValue(GradientStop.OffsetProperty);
			}
			set
			{
				base.SetValueInternal(GradientStop.OffsetProperty, value);
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00091638 File Offset: 0x00090A38
		protected override Freezable CreateInstanceCore()
		{
			return new GradientStop();
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse objeto com base na cultura atual.</summary>
		/// <returns>Uma representação de cadeia de caracteres do objeto que contém seus valores <see cref="P:System.Windows.Media.GradientStop.Color" /> e <see cref="P:System.Windows.Media.GradientStop.Offset" />.</returns>
		// Token: 0x06002420 RID: 9248 RVA: 0x0009164C File Offset: 0x00090A4C
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação da cadeia de caracteres do objeto com base nas informações de formatação específicas da cultura especificadas.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura ou <see langword="null" /> para usar a cultura atual.</param>
		/// <returns>Uma representação de cadeia de caracteres do objeto que contém seus valores <see cref="P:System.Windows.Media.GradientStop.Color" /> e <see cref="P:System.Windows.Media.GradientStop.Offset" />.</returns>
		// Token: 0x06002421 RID: 9249 RVA: 0x00091668 File Offset: 0x00090A68
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002422 RID: 9250 RVA: 0x00091684 File Offset: 0x00090A84
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000916A0 File Offset: 0x00090AA0
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"{1:",
				format,
				"}{0}{2:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this.Color,
				this.Offset
			});
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientStop" />.</summary>
		// Token: 0x06002424 RID: 9252 RVA: 0x00091710 File Offset: 0x00090B10
		public GradientStop()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientStop" /> com a cor e o deslocamento especificados.</summary>
		/// <param name="color">O valor de cor da marca de gradiente.</param>
		/// <param name="offset">A localização no gradiente em que a marca de gradiente é colocada.</param>
		// Token: 0x06002425 RID: 9253 RVA: 0x00091724 File Offset: 0x00090B24
		public GradientStop(Color color, double offset)
		{
			this.Color = color;
			this.Offset = offset;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientStop.Color" />.</summary>
		// Token: 0x04001168 RID: 4456
		public static readonly DependencyProperty ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeof(GradientStop), Colors.Transparent, null, null, false, null);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientStop.Offset" />.</summary>
		// Token: 0x04001169 RID: 4457
		public static readonly DependencyProperty OffsetProperty = Animatable.RegisterProperty("Offset", typeof(double), typeof(GradientStop), 0.0, null, null, false, null);

		// Token: 0x0400116A RID: 4458
		internal static Color s_Color = Colors.Transparent;

		// Token: 0x0400116B RID: 4459
		internal const double c_Offset = 0.0;
	}
}
