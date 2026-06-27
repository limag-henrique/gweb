using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece a funcionalidade para modelos 3D.</summary>
	// Token: 0x02000469 RID: 1129
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Model3D : Animatable, IFormattable, DUCE.IResource
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x000BEAB8 File Offset: 0x000BDEB8
		internal Model3D()
		{
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que especifica a caixa delimitadora alinhada por eixo desse <see cref="T:System.Windows.Media.Media3D.Model3D" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> caixa delimitadora para o modelo.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000BEACC File Offset: 0x000BDECC
		public Rect3D Bounds
		{
			get
			{
				base.ReadPreamble();
				return this.CalculateSubgraphBoundsOuterSpace();
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000BEAE8 File Offset: 0x000BDEE8
		internal void RayHitTest(RayHitTestParameters rayParams)
		{
			Transform3D transform = this.Transform;
			rayParams.PushModelTransform(transform);
			this.RayHitTestCore(rayParams);
			rayParams.PopTransform(transform);
		}

		// Token: 0x06002F93 RID: 12179
		internal abstract void RayHitTestCore(RayHitTestParameters rayParams);

		// Token: 0x06002F94 RID: 12180 RVA: 0x000BEB14 File Offset: 0x000BDF14
		internal Rect3D CalculateSubgraphBoundsOuterSpace()
		{
			Rect3D rect3D = this.CalculateSubgraphBoundsInnerSpace();
			return M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref rect3D, this.Transform);
		}

		// Token: 0x06002F95 RID: 12181
		internal abstract Rect3D CalculateSubgraphBoundsInnerSpace();

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Model3D" /> e faz cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (embora não possam mais ser resolvidas), mas não copia animações nem seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F96 RID: 12182 RVA: 0x000BEB38 File Offset: 0x000BDF38
		public new Model3D Clone()
		{
			return (Model3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Model3D" /> e faz cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas; porém, seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F97 RID: 12183 RVA: 0x000BEB50 File Offset: 0x000BDF50
		public new Model3D CloneCurrentValue()
		{
			return (Model3D)base.CloneCurrentValue();
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000BEB68 File Offset: 0x000BDF68
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Model3D model3D = (Model3D)d;
			Transform3D resource = (Transform3D)e.OldValue;
			Transform3D resource2 = (Transform3D)e.NewValue;
			Dispatcher dispatcher = model3D.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = model3D;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						model3D.ReleaseResource(resource, channel);
						model3D.AddRefResource(resource2, channel);
					}
				}
			}
			model3D.PropertyChanged(Model3D.TransformProperty);
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Media.Media3D.Transform3D" /> definida no modelo.</summary>
		/// <returns>A <see cref="T:System.Windows.Media.Media3D.Transform3D" /> definida no modelo. O valor padrão é <see cref="T:System.Windows.Media.Media3D.MatrixTransform3D" />.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000BEC30 File Offset: 0x000BE030
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000BEC50 File Offset: 0x000BE050
		public Transform3D Transform
		{
			get
			{
				return (Transform3D)base.GetValue(Model3D.TransformProperty);
			}
			set
			{
				base.SetValueInternal(Model3D.TransformProperty, value);
			}
		}

		// Token: 0x06002F9B RID: 12187
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002F9C RID: 12188 RVA: 0x000BEC6C File Offset: 0x000BE06C
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002F9D RID: 12189
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002F9E RID: 12190 RVA: 0x000BECB4 File Offset: 0x000BE0B4
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002F9F RID: 12191
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000BECFC File Offset: 0x000BE0FC
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002FA1 RID: 12193
		internal abstract int GetChannelCountCore();

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000BED44 File Offset: 0x000BE144
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002FA3 RID: 12195
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000BED58 File Offset: 0x000BE158
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria uma representação de cadeia de caracteres de Model3D.</summary>
		/// <returns>Uma representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06002FA5 RID: 12197 RVA: 0x000BED6C File Offset: 0x000BE16C
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres de Model3D.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Uma representação de cadeia de caracteres de Model3D.</returns>
		// Token: 0x06002FA6 RID: 12198 RVA: 0x000BED88 File Offset: 0x000BE188
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
		// Token: 0x06002FA7 RID: 12199 RVA: 0x000BEDA4 File Offset: 0x000BE1A4
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000BEDC0 File Offset: 0x000BE1C0
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000BEDD4 File Offset: 0x000BE1D4
		static Model3D()
		{
			Type typeFromHandle = typeof(Model3D);
			Model3D.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform3D), typeFromHandle, Transform3D.Identity, new PropertyChangedCallback(Model3D.TransformPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Model3D.Transform" />.</summary>
		// Token: 0x04001535 RID: 5429
		public static readonly DependencyProperty TransformProperty;

		// Token: 0x04001536 RID: 5430
		internal static Transform3D s_Transform = Transform3D.Identity;
	}
}
