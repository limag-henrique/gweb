using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Representa um grupo de objetos do <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> que é usado para aplicar vários efeitos a um objeto visível.</summary>
	// Token: 0x02000609 RID: 1545
	[ContentProperty("Children")]
	public sealed class BitmapEffectGroup : BitmapEffect
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x00116DF8 File Offset: 0x001161F8
		[SecuritySafeCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00116E08 File Offset: 0x00116208
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecuritySafeCritical]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BitmapEffectGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004707 RID: 18183 RVA: 0x00116E18 File Offset: 0x00116218
		public new BitmapEffectGroup Clone()
		{
			return (BitmapEffectGroup)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BitmapEffectGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004708 RID: 18184 RVA: 0x00116E30 File Offset: 0x00116230
		public new BitmapEffectGroup CloneCurrentValue()
		{
			return (BitmapEffectGroup)base.CloneCurrentValue();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define os filhos do <see cref="T:System.Windows.Media.Effects.BitmapEffectGroup" />.</summary>
		/// <returns>Os filhos dos efeitos de grupo como um <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x00116E48 File Offset: 0x00116248
		// (set) Token: 0x0600470A RID: 18186 RVA: 0x00116E68 File Offset: 0x00116268
		public BitmapEffectCollection Children
		{
			get
			{
				return (BitmapEffectCollection)base.GetValue(BitmapEffectGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(BitmapEffectGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00116E84 File Offset: 0x00116284
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapEffectGroup();
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x00116E98 File Offset: 0x00116298
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00116EA8 File Offset: 0x001162A8
		static BitmapEffectGroup()
		{
			Type typeFromHandle = typeof(BitmapEffectGroup);
			BitmapEffectGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(BitmapEffectCollection), typeFromHandle, new FreezableDefaultValueFactory(BitmapEffectCollection.Empty), null, null, false, null);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BitmapEffectGroup.Children" />.</summary>
		// Token: 0x040019DD RID: 6621
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x040019DE RID: 6622
		internal static BitmapEffectCollection s_Children = BitmapEffectCollection.Empty;
	}
}
