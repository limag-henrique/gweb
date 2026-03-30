using System;
using System.Globalization;
using System.Text;
using System.Windows.Markup;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa a métrica usada para criar o layout de um caractere em uma fonte do dispositivo.</summary>
	// Token: 0x0200037C RID: 892
	public class CharacterMetrics
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CharacterMetrics" />.</summary>
		// Token: 0x06002030 RID: 8240 RVA: 0x000834C0 File Offset: 0x000828C0
		public CharacterMetrics()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CharacterMetrics" />, especificando a métrica como uma cadeia de caracteres.</summary>
		/// <param name="metrics">Um valor <see cref="T:System.String" /> separado por vírgula que representa a métrica para o caractere.</param>
		// Token: 0x06002031 RID: 8241 RVA: 0x000834D4 File Offset: 0x000828D4
		public CharacterMetrics(string metrics)
		{
			if (metrics == null)
			{
				throw new ArgumentNullException("metrics");
			}
			this.Metrics = metrics;
		}

		/// <summary>Obtém ou define uma cadeia de caracteres delimitada por vírgula que representa os valores de métrica.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.String" />.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000834FC File Offset: 0x000828FC
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x0008359C File Offset: 0x0008299C
		public string Metrics
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this._blackBoxWidth.ToString(TypeConverterHelper.InvariantEnglishUS));
				stringBuilder.Append(',');
				stringBuilder.Append(this._blackBoxHeight.ToString(TypeConverterHelper.InvariantEnglishUS));
				int num = 1;
				CharacterMetrics.AppendField(this._baseline, CharacterMetrics.FieldIndex.Baseline, ref num, stringBuilder);
				CharacterMetrics.AppendField(this._leftSideBearing, CharacterMetrics.FieldIndex.LeftSideBearing, ref num, stringBuilder);
				CharacterMetrics.AppendField(this._rightSideBearing, CharacterMetrics.FieldIndex.RightSideBearing, ref num, stringBuilder);
				CharacterMetrics.AppendField(this._topSideBearing, CharacterMetrics.FieldIndex.TopSideBearing, ref num, stringBuilder);
				CharacterMetrics.AppendField(this._bottomSideBearing, CharacterMetrics.FieldIndex.BottomSideBearing, ref num, stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				double[] array = CharacterMetrics.ParseMetrics(value);
				CompositeFontParser.VerifyNonNegativeMultiplierOfEm("BlackBoxWidth", ref array[0]);
				CompositeFontParser.VerifyNonNegativeMultiplierOfEm("BlackBoxHeight", ref array[1]);
				CompositeFontParser.VerifyMultiplierOfEm("Baseline", ref array[2]);
				CompositeFontParser.VerifyMultiplierOfEm("LeftSideBearing", ref array[3]);
				CompositeFontParser.VerifyMultiplierOfEm("RightSideBearing", ref array[4]);
				CompositeFontParser.VerifyMultiplierOfEm("TopSideBearing", ref array[5]);
				CompositeFontParser.VerifyMultiplierOfEm("BottomSideBearing", ref array[6]);
				double num = array[0] + array[3] + array[4];
				if (num < 0.0)
				{
					throw new ArgumentException(SR.Get("CharacterMetrics_NegativeHorizontalAdvance"));
				}
				double num2 = array[1] + array[5] + array[6];
				if (num2 < 0.0)
				{
					throw new ArgumentException(SR.Get("CharacterMetrics_NegativeVerticalAdvance"));
				}
				this._blackBoxWidth = array[0];
				this._blackBoxHeight = array[1];
				this._baseline = array[2];
				this._leftSideBearing = array[3];
				this._rightSideBearing = array[4];
				this._topSideBearing = array[5];
				this._bottomSideBearing = array[6];
			}
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000836B8 File Offset: 0x00082AB8
		private static void AppendField(double value, CharacterMetrics.FieldIndex fieldIndex, ref int lastIndex, StringBuilder s)
		{
			if (value != 0.0)
			{
				s.Append(',', fieldIndex - (CharacterMetrics.FieldIndex)lastIndex);
				lastIndex = (int)fieldIndex;
				s.Append(value.ToString(TypeConverterHelper.InvariantEnglishUS));
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000836F4 File Offset: 0x00082AF4
		private static double[] ParseMetrics(string s)
		{
			double[] array = new double[7];
			int num = 0;
			int num2 = 0;
			string text;
			for (;;)
			{
				if (num >= s.Length || s[num] != ' ')
				{
					int num3 = num;
					while (num3 < s.Length && s[num3] != ',')
					{
						num3++;
					}
					int num4 = num3;
					while (num4 > num && s[num4 - 1] == ' ')
					{
						num4--;
					}
					if (num4 > num)
					{
						text = s.Substring(num, num4 - num);
						if (!double.TryParse(text, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, TypeConverterHelper.InvariantEnglishUS, out array[num2]))
						{
							break;
						}
					}
					else if (num2 < 2)
					{
						goto Block_6;
					}
					num2++;
					if (num3 >= s.Length)
					{
						goto IL_E4;
					}
					if (num2 == 7)
					{
						goto Block_8;
					}
					num = num3 + 1;
				}
				else
				{
					num++;
				}
			}
			throw new ArgumentException(SR.Get("CannotConvertStringToType", new object[]
			{
				text,
				"double"
			}));
			Block_6:
			throw new ArgumentException(SR.Get("CharacterMetrics_MissingRequiredField"));
			Block_8:
			throw new ArgumentException(SR.Get("CharacterMetrics_TooManyFields"));
			IL_E4:
			if (num2 < 2)
			{
				throw new ArgumentException(SR.Get("CharacterMetrics_MissingRequiredField"));
			}
			return array;
		}

		/// <summary>Obtém a largura da caixa preta do caractere.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" /> que representa a largura da caixa preta.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x000837FC File Offset: 0x00082BFC
		public double BlackBoxWidth
		{
			get
			{
				return this._blackBoxWidth;
			}
		}

		/// <summary>Obtém a altura da caixa preta do caractere.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" /> que representa a altura da caixa preta.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00083810 File Offset: 0x00082C10
		public double BlackBoxHeight
		{
			get
			{
				return this._blackBoxHeight;
			}
		}

		/// <summary>Obtém o valor de linha de base do caractere.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" /> que representa a linha de base.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x00083824 File Offset: 0x00082C24
		public double Baseline
		{
			get
			{
				return this._baseline;
			}
		}

		/// <summary>Obtém o espaço em branco recomendado à esquerda da caixa preta.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x00083838 File Offset: 0x00082C38
		public double LeftSideBearing
		{
			get
			{
				return this._leftSideBearing;
			}
		}

		/// <summary>Obtém o espaço em branco recomendado à direita da caixa preta.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0008384C File Offset: 0x00082C4C
		public double RightSideBearing
		{
			get
			{
				return this._rightSideBearing;
			}
		}

		/// <summary>Obtém o espaço em branco recomendado acima da caixa preta.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00083860 File Offset: 0x00082C60
		public double TopSideBearing
		{
			get
			{
				return this._topSideBearing;
			}
		}

		/// <summary>Obtém o espaço em branco recomendado abaixo da caixa preta.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x00083874 File Offset: 0x00082C74
		public double BottomSideBearing
		{
			get
			{
				return this._bottomSideBearing;
			}
		}

		/// <summary>Determina se o objeto <see cref="T:System.Windows.Media.CharacterMetrics" /> especificado é igual ao objeto <see cref="T:System.Windows.Media.CharacterMetrics" /> atual.</summary>
		/// <param name="obj">O objeto <see cref="T:System.Windows.Media.CharacterMetrics" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.CharacterMetrics" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600203D RID: 8253 RVA: 0x00083888 File Offset: 0x00082C88
		public override bool Equals(object obj)
		{
			CharacterMetrics characterMetrics = obj as CharacterMetrics;
			return characterMetrics != null && characterMetrics._blackBoxWidth == this._blackBoxWidth && characterMetrics._blackBoxHeight == this._blackBoxHeight && characterMetrics._leftSideBearing == this._leftSideBearing && characterMetrics._rightSideBearing == this._rightSideBearing && characterMetrics._topSideBearing == this._topSideBearing && characterMetrics._bottomSideBearing == this._bottomSideBearing;
		}

		/// <summary>Cria um código hash dos valores de métrica do objeto <see cref="T:System.Windows.Media.CharacterMetrics" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Int32" />.</returns>
		// Token: 0x0600203E RID: 8254 RVA: 0x000838F8 File Offset: 0x00082CF8
		public override int GetHashCode()
		{
			int num = (int)(this._blackBoxWidth * 300.0);
			num = num * 101 + (int)(this._blackBoxHeight * 300.0);
			num = num * 101 + (int)(this._baseline * 300.0);
			num = num * 101 + (int)(this._leftSideBearing * 300.0);
			num = num * 101 + (int)(this._rightSideBearing * 300.0);
			num = num * 101 + (int)(this._topSideBearing * 300.0);
			return num * 101 + (int)(this._bottomSideBearing * 300.0);
		}

		// Token: 0x0400108A RID: 4234
		private double _blackBoxWidth;

		// Token: 0x0400108B RID: 4235
		private double _blackBoxHeight;

		// Token: 0x0400108C RID: 4236
		private double _baseline;

		// Token: 0x0400108D RID: 4237
		private double _leftSideBearing;

		// Token: 0x0400108E RID: 4238
		private double _rightSideBearing;

		// Token: 0x0400108F RID: 4239
		private double _topSideBearing;

		// Token: 0x04001090 RID: 4240
		private double _bottomSideBearing;

		// Token: 0x04001091 RID: 4241
		private const int NumFields = 7;

		// Token: 0x04001092 RID: 4242
		private const int NumRequiredFields = 2;

		// Token: 0x04001093 RID: 4243
		private const int HashMultiplier = 101;

		// Token: 0x02000864 RID: 2148
		private enum FieldIndex
		{
			// Token: 0x04002857 RID: 10327
			BlackBoxWidth,
			// Token: 0x04002858 RID: 10328
			BlackBoxHeight,
			// Token: 0x04002859 RID: 10329
			Baseline,
			// Token: 0x0400285A RID: 10330
			LeftSideBearing,
			// Token: 0x0400285B RID: 10331
			RightSideBearing,
			// Token: 0x0400285C RID: 10332
			TopSideBearing,
			// Token: 0x0400285D RID: 10333
			BottomSideBearing
		}
	}
}
