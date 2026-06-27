using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />.  
	/// Define um efeito de bitmap. Classes derivadas definem os efeitos que podem ser aplicados a um objeto <see cref="T:System.Windows.Media.Visual" />, como um <see cref="T:System.Windows.Controls.Button" /> ou <see cref="T:System.Windows.Controls.Image" />.</summary>
	// Token: 0x02000608 RID: 1544
	[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
	public abstract class BitmapEffect : Animatable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffect" />.</summary>
		// Token: 0x060046F9 RID: 18169 RVA: 0x00116CBC File Offset: 0x001160BC
		[SecuritySafeCritical]
		protected BitmapEffect()
		{
			SecurityHelper.DemandUIWindowPermission();
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new InvalidOperationException(SR.Get("RequiresSTA"));
			}
		}

		/// <summary>Quando substituído em uma classe derivada, atualiza os estados de propriedade das propriedades não gerenciadas do efeito.</summary>
		/// <param name="unmanagedEffect">O identificador para o efeito que contém as propriedades a serem atualizadas.</param>
		// Token: 0x060046FA RID: 18170
		[SecurityCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected abstract void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect);

		/// <summary>Quando substituído em uma classe derivada, cria um clone do efeito não gerenciado.</summary>
		/// <returns>Um identificador para o clone de efeito não gerenciado.</returns>
		// Token: 0x060046FB RID: 18171
		[SecurityCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected abstract SafeHandle CreateUnmanagedEffect();

		/// <summary>Define a propriedade especificada como o valor determinado.</summary>
		/// <param name="effect">O identificador para o efeito que contém a propriedade a ser alterada.</param>
		/// <param name="propertyName">O nome da propriedade a ser alterada.</param>
		/// <param name="value">O valor a ser usado para definir a propriedade.</param>
		// Token: 0x060046FC RID: 18172 RVA: 0x00116CF0 File Offset: 0x001160F0
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected static void SetValue(SafeHandle effect, string propertyName, object value)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		/// <summary>Cria um identificador para um objeto IMILBitmapEffect usado para inicializar um efeito personalizado.</summary>
		/// <returns>Um identificador para um objeto IMILBitmapEffect.</returns>
		// Token: 0x060046FD RID: 18173 RVA: 0x00116D04 File Offset: 0x00116104
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected static SafeHandle CreateBitmapEffectOuter()
		{
			SecurityHelper.DemandUIWindowPermission();
			return null;
		}

		/// <summary>Inicializa um identificador IMILBitmapEffect obtido de <see cref="M:System.Windows.Media.Effects.BitmapEffect.CreateBitmapEffectOuter" /> com o IMILBitmapEffectPrimitive determinado.</summary>
		/// <param name="outerObject">O wrapper IMILBitmapEffect externo a ser inicializado.</param>
		/// <param name="innerObject">O IMILBitmapEffectPrimitive interno.</param>
		// Token: 0x060046FE RID: 18174 RVA: 0x00116D18 File Offset: 0x00116118
		[SecurityCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecurityTreatAsSafe]
		protected static void InitializeBitmapEffect(SafeHandle outerObject, SafeHandle innerObject)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que resulta quando o efeito é aplicado ao <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> especificado.</summary>
		/// <param name="input">A entrada à qual aplicar o efeito.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com o efeito aplicado à entrada.</returns>
		// Token: 0x060046FF RID: 18175 RVA: 0x00116D2C File Offset: 0x0011612C
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapSource GetOutput(BitmapEffectInput input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Input == null)
			{
				throw new ArgumentException(SR.Get("Effect_No_InputSource"), "input");
			}
			if (input.Input == BitmapEffectInput.ContextInputSource)
			{
				throw new InvalidOperationException(SR.Get("Effect_No_ContextInputSource", null));
			}
			return input.Input.Clone();
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x00116D90 File Offset: 0x00116190
		internal virtual bool CanBeEmulatedUsingEffectPipeline()
		{
			return false;
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00116DA0 File Offset: 0x001161A0
		internal virtual Effect GetEmulatingEffect()
		{
			throw new NotImplementedException();
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.BitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004702 RID: 18178 RVA: 0x00116DB4 File Offset: 0x001161B4
		public new BitmapEffect Clone()
		{
			return (BitmapEffect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004703 RID: 18179 RVA: 0x00116DCC File Offset: 0x001161CC
		public new BitmapEffect CloneCurrentValue()
		{
			return (BitmapEffect)base.CloneCurrentValue();
		}
	}
}
