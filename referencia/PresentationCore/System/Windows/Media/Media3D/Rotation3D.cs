using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Especifica a rotação 3D a ser usada em uma transformação.</summary>
	// Token: 0x02000467 RID: 1127
	public abstract class Rotation3D : Animatable, IFormattable, DUCE.IResource
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x000BDBFC File Offset: 0x000BCFFC
		static Rotation3D()
		{
			Rotation3D.s_identity.Freeze();
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000BDC20 File Offset: 0x000BD020
		internal Rotation3D()
		{
		}

		/// <summary>Identidade de singleton <see cref="T:System.Windows.Media.Media3D.Rotation3D" />.</summary>
		/// <returns>A identidade <see cref="T:System.Windows.Media.Media3D.Rotation3D" />.</returns>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000BDC34 File Offset: 0x000BD034
		public static Rotation3D Identity
		{
			get
			{
				return Rotation3D.s_identity;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002F5E RID: 12126
		internal abstract Quaternion InternalQuaternion { get; }

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Rotation3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002F5F RID: 12127 RVA: 0x000BDC48 File Offset: 0x000BD048
		public new Rotation3D Clone()
		{
			return (Rotation3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Rotation3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F60 RID: 12128 RVA: 0x000BDC60 File Offset: 0x000BD060
		public new Rotation3D CloneCurrentValue()
		{
			return (Rotation3D)base.CloneCurrentValue();
		}

		// Token: 0x06002F61 RID: 12129
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002F62 RID: 12130 RVA: 0x000BDC78 File Offset: 0x000BD078
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002F63 RID: 12131
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002F64 RID: 12132 RVA: 0x000BDCC0 File Offset: 0x000BD0C0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002F65 RID: 12133
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002F66 RID: 12134 RVA: 0x000BDD08 File Offset: 0x000BD108
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002F67 RID: 12135
		internal abstract int GetChannelCountCore();

		// Token: 0x06002F68 RID: 12136 RVA: 0x000BDD50 File Offset: 0x000BD150
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002F69 RID: 12137
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002F6A RID: 12138 RVA: 0x000BDD64 File Offset: 0x000BD164
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Retorna uma cadeia de caracteres que representa o objeto atual.</summary>
		/// <returns>Uma cadeia de caracteres que representa o objeto atual.</returns>
		// Token: 0x06002F6B RID: 12139 RVA: 0x000BDD78 File Offset: 0x000BD178
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Retorna um <see cref="T:System.String" /> usando um formato especificado que representa o <see cref="T:System.Object" /> atual.</summary>
		/// <param name="provider">Formata o valor da instância atual usando o formato especificado.</param>
		/// <returns>Texto como uma série de caracteres Unicode.</returns>
		// Token: 0x06002F6C RID: 12140 RVA: 0x000BDD94 File Offset: 0x000BD194
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
		// Token: 0x06002F6D RID: 12141 RVA: 0x000BDDB0 File Offset: 0x000BD1B0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000BDDCC File Offset: 0x000BD1CC
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		// Token: 0x0400152A RID: 5418
		private static readonly Rotation3D s_identity = new QuaternionRotation3D();
	}
}
