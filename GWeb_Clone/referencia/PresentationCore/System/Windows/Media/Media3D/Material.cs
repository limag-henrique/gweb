using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Classe base abstrata de materiais.</summary>
	// Token: 0x02000461 RID: 1121
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Material : Animatable, IFormattable, DUCE.IResource
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B9FC8 File Offset: 0x000B93C8
		internal Material()
		{
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Material" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B9FDC File Offset: 0x000B93DC
		public new Material Clone()
		{
			return (Material)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Material" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002EB7 RID: 11959 RVA: 0x000B9FF4 File Offset: 0x000B93F4
		public new Material CloneCurrentValue()
		{
			return (Material)base.CloneCurrentValue();
		}

		// Token: 0x06002EB8 RID: 11960
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000BA00C File Offset: 0x000B940C
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002EBA RID: 11962
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002EBB RID: 11963 RVA: 0x000BA054 File Offset: 0x000B9454
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002EBC RID: 11964
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002EBD RID: 11965 RVA: 0x000BA09C File Offset: 0x000B949C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002EBE RID: 11966
		internal abstract int GetChannelCountCore();

		// Token: 0x06002EBF RID: 11967 RVA: 0x000BA0E4 File Offset: 0x000B94E4
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002EC0 RID: 11968
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000BA0F8 File Offset: 0x000B94F8
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria uma representação de cadeia de caracteres do objeto com base na cultura atual.</summary>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000BA10C File Offset: 0x000B950C
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres do Material.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000BA128 File Offset: 0x000B9528
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
		// Token: 0x06002EC4 RID: 11972 RVA: 0x000BA144 File Offset: 0x000B9544
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000BA160 File Offset: 0x000B9560
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}
	}
}
