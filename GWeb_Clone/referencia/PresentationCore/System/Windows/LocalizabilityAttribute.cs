using System;
using System.ComponentModel;

namespace System.Windows
{
	/// <summary>Especifica os atributos de localização para uma classe ou membro de classe de BAML (XAML binário).</summary>
	// Token: 0x020001C9 RID: 457
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizabilityAttribute : Attribute
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.LocalizabilityAttribute" /> com uma categoria de localização especificada.</summary>
		/// <param name="category">A categoria de localização.</param>
		// Token: 0x06000C31 RID: 3121 RVA: 0x0002EBB0 File Offset: 0x0002DFB0
		public LocalizabilityAttribute(LocalizationCategory category)
		{
			if (category < LocalizationCategory.None || category > LocalizationCategory.NeverLocalize)
			{
				throw new InvalidEnumArgumentException("category", (int)category, typeof(LocalizationCategory));
			}
			this._category = category;
			this._readability = Readability.Readable;
			this._modifiability = Modifiability.Modifiable;
		}

		/// <summary>Obtém a definição de categoria do valor de destino do atributo de localização.</summary>
		/// <returns>A configuração de categoria do atributo de localização.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002EBF8 File Offset: 0x0002DFF8
		public LocalizationCategory Category
		{
			get
			{
				return this._category;
			}
		}

		/// <summary>Obtém ou define a definição de legibilidade do valor de destino do atributo de localização.</summary>
		/// <returns>A definição de legibilidade do atributo de localização.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002EC0C File Offset: 0x0002E00C
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002EC20 File Offset: 0x0002E020
		public Readability Readability
		{
			get
			{
				return this._readability;
			}
			set
			{
				if (value != Readability.Unreadable && value != Readability.Readable && value != Readability.Inherit)
				{
					throw new InvalidEnumArgumentException("Readability", (int)value, typeof(Readability));
				}
				this._readability = value;
			}
		}

		/// <summary>Obtém ou define a definição de modificabilidade do valor de destino do atributo de localização.</summary>
		/// <returns>A definição de modificabilidade do atributo de localização.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002EC58 File Offset: 0x0002E058
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0002EC6C File Offset: 0x0002E06C
		public Modifiability Modifiability
		{
			get
			{
				return this._modifiability;
			}
			set
			{
				if (value != Modifiability.Unmodifiable && value != Modifiability.Modifiable && value != Modifiability.Inherit)
				{
					throw new InvalidEnumArgumentException("Modifiability", (int)value, typeof(Modifiability));
				}
				this._modifiability = value;
			}
		}

		// Token: 0x04000705 RID: 1797
		private LocalizationCategory _category;

		// Token: 0x04000706 RID: 1798
		private Readability _readability;

		// Token: 0x04000707 RID: 1799
		private Modifiability _modifiability;
	}
}
