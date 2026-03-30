using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Converters;
using System.Windows.Threading;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Define os objetos usados para pintar objetos gráficos. Classes que derivam de <see cref="T:System.Windows.Media.Brush" /> descrevem como a área é pintada.</summary>
	// Token: 0x02000368 RID: 872
	[TypeConverter(typeof(BrushConverter))]
	[ValueSerializer(typeof(BrushValueSerializer))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Brush : Animatable, IFormattable, DUCE.IResource
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x0007AA20 File Offset: 0x00079E20
		internal static Brush Parse(string value, ITypeDescriptorContext context)
		{
			IFreezeFreezables freezeFreezables = null;
			Brush brush;
			if (context != null)
			{
				freezeFreezables = (IFreezeFreezables)context.GetService(typeof(IFreezeFreezables));
				if (freezeFreezables != null && freezeFreezables.FreezeFreezables)
				{
					brush = (Brush)freezeFreezables.TryGetFreezable(value);
					if (brush != null)
					{
						return brush;
					}
				}
			}
			brush = Parsers.ParseBrush(value, TypeConverterHelper.InvariantEnglishUS, context);
			if (brush != null && freezeFreezables != null && freezeFreezables.FreezeFreezables)
			{
				freezeFreezables.TryFreeze(value, brush);
			}
			return brush;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0007AA8C File Offset: 0x00079E8C
		internal virtual bool CanSerializeToString()
		{
			return false;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Brush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0007AA9C File Offset: 0x00079E9C
		public new Brush Clone()
		{
			return (Brush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Brush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06001E03 RID: 7683 RVA: 0x0007AAB4 File Offset: 0x00079EB4
		public new Brush CloneCurrentValue()
		{
			return (Brush)base.CloneCurrentValue();
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0007AACC File Offset: 0x00079ECC
		private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Brush brush = (Brush)d;
			brush.PropertyChanged(Brush.OpacityProperty);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0007AAEC File Offset: 0x00079EEC
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Brush brush = (Brush)d;
			Transform resource = (Transform)e.OldValue;
			Transform resource2 = (Transform)e.NewValue;
			Dispatcher dispatcher = brush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = brush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						brush.ReleaseResource(resource, channel);
						brush.AddRefResource(resource2, channel);
					}
				}
			}
			brush.PropertyChanged(Brush.TransformProperty);
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0007ABB4 File Offset: 0x00079FB4
		private static void RelativeTransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Brush brush = (Brush)d;
			Transform resource = (Transform)e.OldValue;
			Transform resource2 = (Transform)e.NewValue;
			Dispatcher dispatcher = brush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = brush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						brush.ReleaseResource(resource, channel);
						brush.AddRefResource(resource2, channel);
					}
				}
			}
			brush.PropertyChanged(Brush.RelativeTransformProperty);
		}

		/// <summary>Obtém ou define o grau de opacidade de um <see cref="T:System.Windows.Media.Brush" />.</summary>
		/// <returns>O valor da propriedade <see cref="P:System.Windows.Media.Brush.Opacity" /> é expresso como um valor entre 0,0 e 1,0. O valor padrão é 1.0.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0007AC7C File Offset: 0x0007A07C
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0007AC9C File Offset: 0x0007A09C
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(Brush.OpacityProperty);
			}
			set
			{
				base.SetValueInternal(Brush.OpacityProperty, value);
			}
		}

		/// <summary>Obtém ou define a transformação que é aplicada ao pincel. Essa transformação é aplicada após a saída do pincel ter sido mapeada e posicionada.</summary>
		/// <returns>A transformação a ser aplicada ao pincel. O valor padrão é a transformação <see cref="P:System.Windows.Media.Transform.Identity" />.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0007ACBC File Offset: 0x0007A0BC
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0007ACDC File Offset: 0x0007A0DC
		public Transform Transform
		{
			get
			{
				return (Transform)base.GetValue(Brush.TransformProperty);
			}
			set
			{
				base.SetValueInternal(Brush.TransformProperty, value);
			}
		}

		/// <summary>Obtém ou define a transformação que é aplicada ao pincel, usando coordenadas relativas.</summary>
		/// <returns>A transformação que é aplicada ao pincel, usando coordenadas relativas.  O valor padrão é a transformação <see cref="P:System.Windows.Media.Transform.Identity" />.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0007ACF8 File Offset: 0x0007A0F8
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0007AD18 File Offset: 0x0007A118
		public Transform RelativeTransform
		{
			get
			{
				return (Transform)base.GetValue(Brush.RelativeTransformProperty);
			}
			set
			{
				base.SetValueInternal(Brush.RelativeTransformProperty, value);
			}
		}

		// Token: 0x06001E0D RID: 7693
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06001E0E RID: 7694 RVA: 0x0007AD34 File Offset: 0x0007A134
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06001E0F RID: 7695
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06001E10 RID: 7696 RVA: 0x0007AD7C File Offset: 0x0007A17C
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06001E11 RID: 7697
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06001E12 RID: 7698 RVA: 0x0007ADC4 File Offset: 0x0007A1C4
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06001E13 RID: 7699
		internal abstract int GetChannelCountCore();

		// Token: 0x06001E14 RID: 7700 RVA: 0x0007AE0C File Offset: 0x0007A20C
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06001E15 RID: 7701
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06001E16 RID: 7702 RVA: 0x0007AE20 File Offset: 0x0007A220
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Retorne a representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.Brush" />.</summary>
		/// <returns>Uma representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06001E17 RID: 7703 RVA: 0x0007AE34 File Offset: 0x0007A234
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação da cadeia de caracteres do objeto com base nas informações de formatação específicas da cultura especificadas.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura ou <see langword="null" /> para usar a formatação padrão da cultura atual.</param>
		/// <returns>Uma representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06001E18 RID: 7704 RVA: 0x0007AE50 File Offset: 0x0007A250
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
		// Token: 0x06001E19 RID: 7705 RVA: 0x0007AE6C File Offset: 0x0007A26C
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0007AE88 File Offset: 0x0007A288
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0007AE9C File Offset: 0x0007A29C
		static Brush()
		{
			Type typeFromHandle = typeof(Brush);
			Brush.OpacityProperty = Animatable.RegisterProperty("Opacity", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(Brush.OpacityPropertyChanged), null, true, null);
			Brush.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform), typeFromHandle, Transform.Identity, new PropertyChangedCallback(Brush.TransformPropertyChanged), null, false, null);
			Brush.RelativeTransformProperty = Animatable.RegisterProperty("RelativeTransform", typeof(Transform), typeFromHandle, Transform.Identity, new PropertyChangedCallback(Brush.RelativeTransformPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Brush.Opacity" />.</summary>
		// Token: 0x04001006 RID: 4102
		public static readonly DependencyProperty OpacityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Brush.Transform" />.</summary>
		// Token: 0x04001007 RID: 4103
		public static readonly DependencyProperty TransformProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Brush.RelativeTransform" />.</summary>
		// Token: 0x04001008 RID: 4104
		public static readonly DependencyProperty RelativeTransformProperty;

		// Token: 0x04001009 RID: 4105
		internal const double c_Opacity = 1.0;

		// Token: 0x0400100A RID: 4106
		internal static Transform s_Transform = Transform.Identity;

		// Token: 0x0400100B RID: 4107
		internal static Transform s_RelativeTransform = Transform.Identity;
	}
}
