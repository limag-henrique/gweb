using System;
using System.Windows.Media.Animation;

namespace System.Windows.Media.Media3D
{
	/// <summary>O objeto <see cref="T:System.Windows.Media.Media3D.Model3D" /> que representa a iluminação aplicada a uma cena 3D.</summary>
	// Token: 0x02000460 RID: 1120
	public abstract class Light : Model3D
	{
		// Token: 0x06002EAB RID: 11947 RVA: 0x000B9E98 File Offset: 0x000B9298
		internal Light()
		{
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000B9EAC File Offset: 0x000B92AC
		internal override void RayHitTestCore(RayHitTestParameters rayParams)
		{
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000B9EBC File Offset: 0x000B92BC
		internal override Rect3D CalculateSubgraphBoundsInnerSpace()
		{
			return Rect3D.Empty;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Light" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002EAE RID: 11950 RVA: 0x000B9ED0 File Offset: 0x000B92D0
		public new Light Clone()
		{
			return (Light)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Light" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002EAF RID: 11951 RVA: 0x000B9EE8 File Offset: 0x000B92E8
		public new Light CloneCurrentValue()
		{
			return (Light)base.CloneCurrentValue();
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B9F00 File Offset: 0x000B9300
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Light light = (Light)d;
			light.PropertyChanged(Light.ColorProperty);
		}

		/// <summary>Obtém ou define a cor da luz.</summary>
		/// <returns>Cor da luz.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000B9F20 File Offset: 0x000B9320
		// (set) Token: 0x06002EB2 RID: 11954 RVA: 0x000B9F40 File Offset: 0x000B9340
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(Light.ColorProperty);
			}
			set
			{
				base.SetValueInternal(Light.ColorProperty, value);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000B9F60 File Offset: 0x000B9360
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B9F70 File Offset: 0x000B9370
		static Light()
		{
			Type typeFromHandle = typeof(Light);
			Light.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.White, new PropertyChangedCallback(Light.ColorPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Light.Color" />.</summary>
		// Token: 0x04001509 RID: 5385
		public static readonly DependencyProperty ColorProperty;

		// Token: 0x0400150A RID: 5386
		internal static Color s_Color = Colors.White;
	}
}
