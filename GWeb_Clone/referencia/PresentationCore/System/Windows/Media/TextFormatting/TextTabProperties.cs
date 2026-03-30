using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Especifica as propriedades de tabulação definidas pelo usuário.</summary>
	// Token: 0x020005B9 RID: 1465
	public class TextTabProperties
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextTabProperties" /> especificando as propriedades da tabulação.</summary>
		/// <param name="alignment">Um valor enumerado de <see cref="T:System.Windows.Media.TextFormatting.TextTabAlignment" /> que representa o alinhamento do texto no local da tabulação.</param>
		/// <param name="location">Um valor <see cref="T:System.Double" /> que representa a localização da tabulação.</param>
		/// <param name="tabLeader">Um valor <see cref="T:System.Int32" /> que representa o preenchimento de tabulação.</param>
		/// <param name="aligningChar">Um valor <see cref="T:System.Int32" /> que representa o caractere específico no texto que está alinhado na localização da tabulação.</param>
		// Token: 0x060042FD RID: 17149 RVA: 0x00104254 File Offset: 0x00103654
		public TextTabProperties(TextTabAlignment alignment, double location, int tabLeader, int aligningChar)
		{
			this._alignment = alignment;
			this._location = location;
			this._tabLeader = tabLeader;
			this._aligningChar = aligningChar;
		}

		/// <summary>Obtém o estilo de alinhamento do texto no local da tabulação.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.TextFormatting.TextTabAlignment" /> que representa o alinhamento do texto no local da tabulação.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x00104284 File Offset: 0x00103684
		public TextTabAlignment Alignment
		{
			get
			{
				return this._alignment;
			}
		}

		/// <summary>Obtém o valor do índice do local da tabulação.</summary>
		/// <returns>Um valor <see cref="T:System.Double" /> que representa a localização da tabulação.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x00104298 File Offset: 0x00103698
		public double Location
		{
			get
			{
				return this._location;
			}
		}

		/// <summary>Obtém o índice do caractere que é usado para exibir o preenchimento de tabulação.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o local do líder de guia.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x001042AC File Offset: 0x001036AC
		public int TabLeader
		{
			get
			{
				return this._tabLeader;
			}
		}

		/// <summary>Obtém o índice do caractere específico em que o texto é alinhado no local especificado da tabulação.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> que representa o índice de valor.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x001042C0 File Offset: 0x001036C0
		public int AligningCharacter
		{
			get
			{
				return this._aligningChar;
			}
		}

		// Token: 0x04001847 RID: 6215
		private TextTabAlignment _alignment;

		// Token: 0x04001848 RID: 6216
		private double _location;

		// Token: 0x04001849 RID: 6217
		private int _tabLeader;

		// Token: 0x0400184A RID: 6218
		private int _aligningChar;
	}
}
