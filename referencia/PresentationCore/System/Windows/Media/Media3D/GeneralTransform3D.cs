using System;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece suporte à transformação generalizada para objetos 3D.</summary>
	// Token: 0x02000459 RID: 1113
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class GeneralTransform3D : Animatable, IFormattable
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x000B8D38 File Offset: 0x000B8138
		internal GeneralTransform3D()
		{
		}

		/// <summary>Quando substituída em uma classe derivada, tenta transformar o ponto 3D especificado e retorna um valor que indica se a transformação foi bem-sucedida.</summary>
		/// <param name="inPoint">O ponto 3D a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002E4C RID: 11852
		public abstract bool TryTransform(Point3D inPoint, out Point3D result);

		/// <summary>Transforma o ponto 3D especificado e retorna o resultado.</summary>
		/// <param name="point">O ponto 3D a ser transformado.</param>
		/// <returns>O resultado de transformar <paramref name="point" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transformação não foi bem-sucedida.</exception>
		// Token: 0x06002E4D RID: 11853 RVA: 0x000B8D4C File Offset: 0x000B814C
		public Point3D Transform(Point3D point)
		{
			Point3D result;
			if (!this.TryTransform(point, out result))
			{
				throw new InvalidOperationException(SR.Get("GeneralTransform_TransformFailed", null));
			}
			return result;
		}

		/// <summary>Quando substituída em uma classe derivada, transforma a caixa delimitadora 3D especificada e retorna uma caixa delimitadora 3D alinhada por eixo que seja grande o suficiente, no tamanho exato para contê-la.</summary>
		/// <param name="rect">A caixa delimitadora 3D a ser transformada.</param>
		/// <returns>A menor caixa delimitadora 3D possível alinhada por eixo que contenha o <paramref name="rect" /> transformado.</returns>
		// Token: 0x06002E4E RID: 11854
		public abstract Rect3D TransformBounds(Rect3D rect);

		/// <summary>Obtém a transformação inversa deste <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />, se possível.</summary>
		/// <returns>Um inverso dessa instância, se possível; Caso contrário, <see langword="null" />.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002E4F RID: 11855
		public abstract GeneralTransform3D Inverse { get; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002E50 RID: 11856
		internal abstract Transform3D AffineTransform { [FriendAccessAllowed] get; }

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06002E51 RID: 11857 RVA: 0x000B8D78 File Offset: 0x000B8178
		public new GeneralTransform3D Clone()
		{
			return (GeneralTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06002E52 RID: 11858 RVA: 0x000B8D90 File Offset: 0x000B8190
		public new GeneralTransform3D CloneCurrentValue()
		{
			return (GeneralTransform3D)base.CloneCurrentValue();
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa instância.</summary>
		/// <returns>Uma representação de cadeia de caracteres dessa instância.</returns>
		// Token: 0x06002E53 RID: 11859 RVA: 0x000B8DA8 File Offset: 0x000B81A8
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa instância, com base no parâmetro <see cref="T:System.IFormatProvider" /> passado.</summary>
		/// <param name="provider">Instância <see cref="T:System.IFormatProvider" /> usada para processar essa instância.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002E54 RID: 11860 RVA: 0x000B8DC4 File Offset: 0x000B81C4
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002E55 RID: 11861 RVA: 0x000B8DE0 File Offset: 0x000B81E0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000B8DFC File Offset: 0x000B81FC
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}
	}
}
