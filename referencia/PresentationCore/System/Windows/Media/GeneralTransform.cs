using System;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Oferece suporte a transformação generalizada para objetos, como pontos e retângulos. Esta é uma classe abstrata.</summary>
	// Token: 0x0200039B RID: 923
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class GeneralTransform : Animatable, IFormattable
	{
		/// <summary>Quando substituída em uma classe derivada, tenta transformar o ponto especificado e retorna um valor que indica se a transformação foi bem-sucedida.</summary>
		/// <param name="inPoint">O ponto a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022C4 RID: 8900
		public abstract bool TryTransform(Point inPoint, out Point result);

		/// <summary>Transforma o ponto especificado e retorna o resultado.</summary>
		/// <param name="point">O ponto a ser transformado.</param>
		/// <returns>O resultado de transformar <paramref name="point" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transformação não foi bem-sucedida.</exception>
		// Token: 0x060022C5 RID: 8901 RVA: 0x0008CB14 File Offset: 0x0008BF14
		public Point Transform(Point point)
		{
			Point result;
			if (!this.TryTransform(point, out result))
			{
				throw new InvalidOperationException(SR.Get("GeneralTransform_TransformFailed", null));
			}
			return result;
		}

		/// <summary>Quando substituída em uma classe derivada, transforma a caixa delimitadora especificada e retorna uma caixa delimitadora alinhada por eixo que seja grande o suficiente, no tamanho exato para contê-la.</summary>
		/// <param name="rect">A caixa delimitadora a ser transformada.</param>
		/// <returns>A menor caixa delimitadora possível alinhada por eixo que contenha o <paramref name="rect" /> transformado.</returns>
		// Token: 0x060022C6 RID: 8902
		public abstract Rect TransformBounds(Rect rect);

		/// <summary>Obtém a transformação inversa deste <see cref="T:System.Windows.Media.GeneralTransform" />, se possível.</summary>
		/// <returns>Um inverso dessa instância, se possível; Caso contrário, <see langword="null" />.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060022C7 RID: 8903
		public abstract GeneralTransform Inverse { get; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0008CB40 File Offset: 0x0008BF40
		internal virtual Transform AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				return null;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeneralTransform" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060022C9 RID: 8905 RVA: 0x0008CB50 File Offset: 0x0008BF50
		public new GeneralTransform Clone()
		{
			return (GeneralTransform)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeneralTransform" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060022CA RID: 8906 RVA: 0x0008CB68 File Offset: 0x0008BF68
		public new GeneralTransform CloneCurrentValue()
		{
			return (GeneralTransform)base.CloneCurrentValue();
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.GeneralTransform" />.</summary>
		/// <returns>Uma representação de cadeia de caracteres dessa instância.</returns>
		// Token: 0x060022CB RID: 8907 RVA: 0x0008CB80 File Offset: 0x0008BF80
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa instância, com base no parâmetro <see cref="T:System.IFormatProvider" /> passado.</summary>
		/// <param name="provider">Instância <see cref="T:System.IFormatProvider" /> usada para processar essa instância.</param>
		/// <returns>Uma representação de cadeia de caracteres dessa instância, baseada em <paramref name="provider" />.</returns>
		// Token: 0x060022CC RID: 8908 RVA: 0x0008CB9C File Offset: 0x0008BF9C
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
		// Token: 0x060022CD RID: 8909 RVA: 0x0008CBB8 File Offset: 0x0008BFB8
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0008CBD4 File Offset: 0x0008BFD4
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}
	}
}
