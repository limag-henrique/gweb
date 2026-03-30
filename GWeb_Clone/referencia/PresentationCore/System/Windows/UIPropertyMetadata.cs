using System;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Fornece metadados de propriedade para propriedades que não são de estrutura, mas que têm impacto de renderização/interface do usuário no nível de núcleo.</summary>
	// Token: 0x020001F6 RID: 502
	public class UIPropertyMetadata : PropertyMetadata
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" />.</summary>
		// Token: 0x06000D22 RID: 3362 RVA: 0x00031B18 File Offset: 0x00030F18
		public UIPropertyMetadata()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" /> com o valor padrão especificado da propriedade.</summary>
		/// <param name="defaultValue">O valor padrão da propriedade de dependência, geralmente fornecido como um valor de um tipo específico.</param>
		// Token: 0x06000D23 RID: 3363 RVA: 0x00031B2C File Offset: 0x00030F2C
		public UIPropertyMetadata(object defaultValue) : base(defaultValue)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" /> com o retorno de chamada PropertyChanged especificado.</summary>
		/// <param name="propertyChangedCallback">Referência a uma implementação do manipulador que será chamada pelo sistema de propriedades sempre que o valor efetivo da propriedade for alterado.</param>
		// Token: 0x06000D24 RID: 3364 RVA: 0x00031B40 File Offset: 0x00030F40
		public UIPropertyMetadata(PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" /> com o retorno de chamada PropertyChanged especificado.</summary>
		/// <param name="defaultValue">O valor padrão da propriedade de dependência, geralmente fornecido como um valor de um tipo específico.</param>
		/// <param name="propertyChangedCallback">Referência a uma implementação do manipulador que será chamada pelo sistema de propriedades sempre que o valor efetivo da propriedade for alterado.</param>
		// Token: 0x06000D25 RID: 3365 RVA: 0x00031B54 File Offset: 0x00030F54
		public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback) : base(defaultValue, propertyChangedCallback)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" /> com o valor padrão especificado e retornos de chamadas especificados.</summary>
		/// <param name="defaultValue">O valor padrão da propriedade de dependência, geralmente fornecido como um valor de um tipo específico.</param>
		/// <param name="propertyChangedCallback">Referência a uma implementação do manipulador que será chamada pelo sistema de propriedades sempre que o valor efetivo da propriedade for alterado.</param>
		/// <param name="coerceValueCallback">Uma referência a uma implementação do manipulador que será chamada sempre que o sistema de propriedades chamar <see cref="M:System.Windows.DependencyObject.CoerceValue(System.Windows.DependencyProperty)" /> nessa propriedade.</param>
		// Token: 0x06000D26 RID: 3366 RVA: 0x00031B6C File Offset: 0x00030F6C
		public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(defaultValue, propertyChangedCallback, coerceValueCallback)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIPropertyMetadata" /> com o valor padrão especificado e retornos de chamada, bem como um booliano usado para desabilitar animações na propriedade.</summary>
		/// <param name="defaultValue">O valor padrão da propriedade de dependência, geralmente fornecido como um valor de um tipo específico.</param>
		/// <param name="propertyChangedCallback">Referência a uma implementação do manipulador que será chamada pelo sistema de propriedades sempre que o valor efetivo da propriedade for alterado.</param>
		/// <param name="coerceValueCallback">Uma referência a uma implementação do manipulador que será chamada sempre que o sistema de propriedades chamar <see cref="M:System.Windows.DependencyObject.CoerceValue(System.Windows.DependencyProperty)" /> nessa propriedade.</param>
		/// <param name="isAnimationProhibited">Definido como <see langword="true" /> para impedir que o sistema de propriedades anime a propriedade à qual esses metadados foram aplicados. Essas propriedades gerarão exceções de tempo de execução se houver tentativas de animá-las. O padrão é <see langword="false" />.</param>
		// Token: 0x06000D27 RID: 3367 RVA: 0x00031B84 File Offset: 0x00030F84
		public UIPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback, bool isAnimationProhibited) : base(defaultValue, propertyChangedCallback, coerceValueCallback)
		{
			base.WriteFlag(PropertyMetadata.MetadataFlags.UI_IsAnimationProhibitedID, isAnimationProhibited);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00031BA4 File Offset: 0x00030FA4
		internal override PropertyMetadata CreateInstance()
		{
			return new UIPropertyMetadata();
		}

		/// <summary>Obtém ou define um valor que declara se as animações devem ser desabilitadas na propriedade de dependência em que a instância de metadados recipiente é aplicada.</summary>
		/// <returns>
		///   <see langword="true" /> indica que as animações não são permitidas; <see langword="false" /> indica que as animações são permitidas. O padrão é <see langword="false" /> (animações permitidas).</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00031BB8 File Offset: 0x00030FB8
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x00031BD0 File Offset: 0x00030FD0
		public bool IsAnimationProhibited
		{
			get
			{
				return base.ReadFlag(PropertyMetadata.MetadataFlags.UI_IsAnimationProhibitedID);
			}
			set
			{
				if (base.Sealed)
				{
					throw new InvalidOperationException(SR.Get("TypeMetadataCannotChangeAfterUse"));
				}
				base.WriteFlag(PropertyMetadata.MetadataFlags.UI_IsAnimationProhibitedID, value);
			}
		}
	}
}
