using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa um tipo de objeto que tem largura, altura e <see cref="T:System.Windows.Media.ImageMetadata" /> como um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> e um <see cref="T:System.Windows.Media.DrawingImage" />. Esta é uma classe abstrata.</summary>
	// Token: 0x020003B8 RID: 952
	[ValueSerializer(typeof(ImageSourceValueSerializer))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	[TypeConverter(typeof(ImageSourceConverter))]
	public abstract class ImageSource : Animatable, IFormattable, DUCE.IResource
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.ImageSource" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600247D RID: 9341 RVA: 0x00092C38 File Offset: 0x00092038
		public new ImageSource Clone()
		{
			return (ImageSource)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.ImageSource" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600247E RID: 9342 RVA: 0x00092C50 File Offset: 0x00092050
		public new ImageSource CloneCurrentValue()
		{
			return (ImageSource)base.CloneCurrentValue();
		}

		// Token: 0x0600247F RID: 9343
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002480 RID: 9344 RVA: 0x00092C68 File Offset: 0x00092068
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002481 RID: 9345
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002482 RID: 9346 RVA: 0x00092CB0 File Offset: 0x000920B0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002483 RID: 9347
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002484 RID: 9348 RVA: 0x00092CF8 File Offset: 0x000920F8
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002485 RID: 9349
		internal abstract int GetChannelCountCore();

		// Token: 0x06002486 RID: 9350 RVA: 0x00092D40 File Offset: 0x00092140
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002487 RID: 9351
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002488 RID: 9352 RVA: 0x00092D54 File Offset: 0x00092154
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse objeto com base na cultura atual.</summary>
		/// <returns>Uma representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06002489 RID: 9353 RVA: 0x00092D68 File Offset: 0x00092168
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse objeto com base na <see cref="T:System.IFormatProvider" /> passada. Se o provedor for <see langword="null" />, o <see cref="P:System.Globalization.CultureInfo.CurrentCulture" /> será usado.</summary>
		/// <param name="provider">Um <see cref="T:System.IFormatProvider" /> que fornece informações de formatação específicas à cultura.</param>
		/// <returns>Uma representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x0600248A RID: 9354 RVA: 0x00092D84 File Offset: 0x00092184
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
		// Token: 0x0600248B RID: 9355 RVA: 0x00092DA0 File Offset: 0x000921A0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00092DBC File Offset: 0x000921BC
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00092DD0 File Offset: 0x000921D0
		internal ImageSource()
		{
		}

		/// <summary>Quando substituído em uma classe derivada, obtém a largura da imagem em unidades de medida (96º de polegada).</summary>
		/// <returns>A largura da imagem em unidades de medida (96 º de polegada).</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600248E RID: 9358
		public abstract double Width { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém a altura da imagem em unidades de medida (96º de polegada).</summary>
		/// <returns>A altura da imagem.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600248F RID: 9359
		public abstract double Height { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém o <see cref="T:System.Windows.Media.ImageMetadata" /> associado à imagem.</summary>
		/// <returns>Os metadados associados à imagem.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002490 RID: 9360
		public abstract ImageMetadata Metadata { get; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00092DE4 File Offset: 0x000921E4
		internal virtual Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00092E04 File Offset: 0x00092204
		internal virtual bool CanSerializeToString()
		{
			return false;
		}

		/// <summary>Converte pixels em DIPs de maneira consistente com MIL.</summary>
		/// <param name="dpi">Largura de bitmap em pontos por polegada.</param>
		/// <param name="pixels">Largura do bitmap em pixels.</param>
		/// <returns>O tamanho natural de bitmap em DIPs (Pixels Independentes de Dispositivo) (ou 1/96") do MIL.</returns>
		// Token: 0x06002493 RID: 9363 RVA: 0x00092E14 File Offset: 0x00092214
		protected static double PixelsToDIPs(double dpi, int pixels)
		{
			float num = (float)dpi;
			if (num < 0f || FloatUtil.IsCloseToDivideByZero(96f, num))
			{
				num = 96f;
			}
			return (double)((float)pixels * (96f / num));
		}
	}
}
