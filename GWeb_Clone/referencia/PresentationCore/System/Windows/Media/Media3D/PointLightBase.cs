using System;
using System.Windows.Media.Animation;

namespace System.Windows.Media.Media3D
{
	/// <summary>Classe base abstrata que representa um objeto de luz que tem uma posição no espaço e projeta sua luz em todos os trajetos.</summary>
	// Token: 0x02000472 RID: 1138
	public abstract class PointLightBase : Light
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x000C1880 File Offset: 0x000C0C80
		internal PointLightBase()
		{
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Media3D.PointLightBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003077 RID: 12407 RVA: 0x000C1894 File Offset: 0x000C0C94
		public new PointLightBase Clone()
		{
			return (PointLightBase)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.ByteAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003078 RID: 12408 RVA: 0x000C18AC File Offset: 0x000C0CAC
		public new PointLightBase CloneCurrentValue()
		{
			return (PointLightBase)base.CloneCurrentValue();
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000C18C4 File Offset: 0x000C0CC4
		private static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointLightBase pointLightBase = (PointLightBase)d;
			pointLightBase.PropertyChanged(PointLightBase.PositionProperty);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000C18E4 File Offset: 0x000C0CE4
		private static void RangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointLightBase pointLightBase = (PointLightBase)d;
			pointLightBase.PropertyChanged(PointLightBase.RangeProperty);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C1904 File Offset: 0x000C0D04
		private static void ConstantAttenuationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointLightBase pointLightBase = (PointLightBase)d;
			pointLightBase.PropertyChanged(PointLightBase.ConstantAttenuationProperty);
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000C1924 File Offset: 0x000C0D24
		private static void LinearAttenuationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointLightBase pointLightBase = (PointLightBase)d;
			pointLightBase.PropertyChanged(PointLightBase.LinearAttenuationProperty);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000C1944 File Offset: 0x000C0D44
		private static void QuadraticAttenuationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointLightBase pointLightBase = (PointLightBase)d;
			pointLightBase.PropertyChanged(PointLightBase.QuadraticAttenuationProperty);
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Point3D" /> que especifica a posição da luz no espaço de mundo.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Point3D" /> que especifica a posição da luz no espaço de mundo.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000C1964 File Offset: 0x000C0D64
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000C1984 File Offset: 0x000C0D84
		public Point3D Position
		{
			get
			{
				return (Point3D)base.GetValue(PointLightBase.PositionProperty);
			}
			set
			{
				base.SetValueInternal(PointLightBase.PositionProperty, value);
			}
		}

		/// <summary>Obtém ou define a distância após a qual a luz não terá efeito.</summary>
		/// <returns>Double que especifica a distância além do qual a luz não tem nenhum efeito.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000C19A4 File Offset: 0x000C0DA4
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000C19C4 File Offset: 0x000C0DC4
		public double Range
		{
			get
			{
				return (double)base.GetValue(PointLightBase.RangeProperty);
			}
			set
			{
				base.SetValueInternal(PointLightBase.RangeProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor constante pelo qual a intensidade da luz diminui com a distância.</summary>
		/// <returns>Duplo pelo qual a intensidade da luz diminui com a distância.</returns>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000C19E4 File Offset: 0x000C0DE4
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000C1A04 File Offset: 0x000C0E04
		public double ConstantAttenuation
		{
			get
			{
				return (double)base.GetValue(PointLightBase.ConstantAttenuationProperty);
			}
			set
			{
				base.SetValueInternal(PointLightBase.ConstantAttenuationProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica a diminuição linear da intensidade da luz com a distância.</summary>
		/// <returns>Double que especifica a diminuição linear da intensidade da luz com a distância.</returns>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000C1A24 File Offset: 0x000C0E24
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000C1A44 File Offset: 0x000C0E44
		public double LinearAttenuation
		{
			get
			{
				return (double)base.GetValue(PointLightBase.LinearAttenuationProperty);
			}
			set
			{
				base.SetValueInternal(PointLightBase.LinearAttenuationProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica a diminuição do efeito de luz com a distância, calculado por uma operação quadrática.</summary>
		/// <returns>Double que especifica a diminuição do efeito da luz com a distância, calculada por uma operação quadrática.</returns>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000C1A64 File Offset: 0x000C0E64
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x000C1A84 File Offset: 0x000C0E84
		public double QuadraticAttenuation
		{
			get
			{
				return (double)base.GetValue(PointLightBase.QuadraticAttenuationProperty);
			}
			set
			{
				base.SetValueInternal(PointLightBase.QuadraticAttenuationProperty, value);
			}
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000C1AA4 File Offset: 0x000C0EA4
		static PointLightBase()
		{
			Type typeFromHandle = typeof(PointLightBase);
			PointLightBase.PositionProperty = Animatable.RegisterProperty("Position", typeof(Point3D), typeFromHandle, default(Point3D), new PropertyChangedCallback(PointLightBase.PositionPropertyChanged), null, true, null);
			PointLightBase.RangeProperty = Animatable.RegisterProperty("Range", typeof(double), typeFromHandle, double.PositiveInfinity, new PropertyChangedCallback(PointLightBase.RangePropertyChanged), null, true, null);
			PointLightBase.ConstantAttenuationProperty = Animatable.RegisterProperty("ConstantAttenuation", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(PointLightBase.ConstantAttenuationPropertyChanged), null, true, null);
			PointLightBase.LinearAttenuationProperty = Animatable.RegisterProperty("LinearAttenuation", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(PointLightBase.LinearAttenuationPropertyChanged), null, true, null);
			PointLightBase.QuadraticAttenuationProperty = Animatable.RegisterProperty("QuadraticAttenuation", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(PointLightBase.QuadraticAttenuationPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PointLightBase.Position" />.</summary>
		// Token: 0x0400154D RID: 5453
		public static readonly DependencyProperty PositionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PointLightBase.Range" />.</summary>
		// Token: 0x0400154E RID: 5454
		public static readonly DependencyProperty RangeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PointLightBase.ConstantAttenuation" />.</summary>
		// Token: 0x0400154F RID: 5455
		public static readonly DependencyProperty ConstantAttenuationProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PointLightBase.LinearAttenuation" />.</summary>
		// Token: 0x04001550 RID: 5456
		public static readonly DependencyProperty LinearAttenuationProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PointLightBase.QuadraticAttenuation" />.</summary>
		// Token: 0x04001551 RID: 5457
		public static readonly DependencyProperty QuadraticAttenuationProperty;

		// Token: 0x04001552 RID: 5458
		internal static Point3D s_Position;

		// Token: 0x04001553 RID: 5459
		internal const double c_Range = double.PositiveInfinity;

		// Token: 0x04001554 RID: 5460
		internal const double c_ConstantAttenuation = 1.0;

		// Token: 0x04001555 RID: 5461
		internal const double c_LinearAttenuation = 0.0;

		// Token: 0x04001556 RID: 5462
		internal const double c_QuadraticAttenuation = 0.0;
	}
}
