using System;
using System.ComponentModel;
using System.Text;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Descreve como um <see cref="T:System.Windows.Media.Animation.Timeline" /> repete sua duração simples.</summary>
	// Token: 0x02000577 RID: 1399
	[TypeConverter(typeof(RepeatBehaviorConverter))]
	public struct RepeatBehavior : IFormattable
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> com a contagem de iteração especificada.</summary>
		/// <param name="count">Um número maior ou igual a 0 que especifica o número de iterações a fazer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> avalia até o infinito, um valor que não é um número ou é negativo.</exception>
		// Token: 0x060040C7 RID: 16583 RVA: 0x000FDF90 File Offset: 0x000FD390
		public RepeatBehavior(double count)
		{
			if (double.IsInfinity(count) || DoubleUtil.IsNaN(count) || count < 0.0)
			{
				throw new ArgumentOutOfRangeException("count", SR.Get("Timing_RepeatBehaviorInvalidIterationCount", new object[]
				{
					count
				}));
			}
			this._repeatDuration = new TimeSpan(0L);
			this._iterationCount = count;
			this._type = RepeatBehavior.RepeatBehaviorType.IterationCount;
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> com a duração de repetição especificada.</summary>
		/// <param name="duration">O total de tempo pelo qual o <see cref="T:System.Windows.Media.Animation.Timeline" /> deve ser reproduzido (sua duração ativa).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O <paramref name="duration" /> é avaliado para um número negativo.</exception>
		// Token: 0x060040C8 RID: 16584 RVA: 0x000FDFF8 File Offset: 0x000FD3F8
		public RepeatBehavior(TimeSpan duration)
		{
			if (duration < new TimeSpan(0L))
			{
				throw new ArgumentOutOfRangeException("duration", SR.Get("Timing_RepeatBehaviorInvalidRepeatDuration", new object[]
				{
					duration
				}));
			}
			this._iterationCount = 0.0;
			this._repeatDuration = duration;
			this._type = RepeatBehavior.RepeatBehaviorType.RepeatDuration;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> que especifica um número infinito de repetições.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> que especifica um número infinito de repetições.</returns>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x000FE058 File Offset: 0x000FD458
		public static RepeatBehavior Forever
		{
			get
			{
				return new RepeatBehavior
				{
					_type = RepeatBehavior.RepeatBehaviorType.Forever
				};
			}
		}

		/// <summary>Obtém um valor que indica se o comportamento de repetição tem uma contagem de iteração especificada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o tipo especificado se refere a uma contagem de iteração; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x000FE078 File Offset: 0x000FD478
		public bool HasCount
		{
			get
			{
				return this._type == RepeatBehavior.RepeatBehaviorType.IterationCount;
			}
		}

		/// <summary>Obtém um valor que indica se o comportamento de repetição tem uma duração de repetição especificada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o tipo especificado se refere a uma duração de repetição; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x000FE090 File Offset: 0x000FD490
		public bool HasDuration
		{
			get
			{
				return this._type == RepeatBehavior.RepeatBehaviorType.RepeatDuration;
			}
		}

		/// <summary>Obtém o número de vezes que um <see cref="T:System.Windows.Media.Animation.Timeline" /> deve ser repetido.</summary>
		/// <returns>O valor numérico que representa o número de iterações para repetir.</returns>
		/// <exception cref="T:System.InvalidOperationException">Isso <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> descreve uma duração de repetição, não uma contagem de iteração.</exception>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x000FE0A8 File Offset: 0x000FD4A8
		public double Count
		{
			get
			{
				if (this._type != RepeatBehavior.RepeatBehaviorType.IterationCount)
				{
					throw new InvalidOperationException(SR.Get("Timing_RepeatBehaviorNotIterationCount", new object[]
					{
						this
					}));
				}
				return this._iterationCount;
			}
		}

		/// <summary>Obtém o tempo total pelo qual um <see cref="T:System.Windows.Media.Animation.Timeline" /> deve ser reproduzido.</summary>
		/// <returns>O comprimento total de tempo que deve ser executada por uma linha do tempo.</returns>
		/// <exception cref="T:System.InvalidOperationException">Isso <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> não descreve uma duração de repetição; descreve uma contagem de iteração.</exception>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x000FE0E8 File Offset: 0x000FD4E8
		public TimeSpan Duration
		{
			get
			{
				if (this._type != RepeatBehavior.RepeatBehaviorType.RepeatDuration)
				{
					throw new InvalidOperationException(SR.Get("Timing_RepeatBehaviorNotRepeatDuration", new object[]
					{
						this
					}));
				}
				return this._repeatDuration;
			}
		}

		/// <summary>Indica se essa instância é igual ao objeto especificado.</summary>
		/// <param name="value">O objeto a ser comparado com essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for um <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> que representa o mesmo comportamento de repetição que esta instância; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040CE RID: 16590 RVA: 0x000FE128 File Offset: 0x000FD528
		public override bool Equals(object value)
		{
			return value is RepeatBehavior && this.Equals((RepeatBehavior)value);
		}

		/// <summary>Retorna um valor que indica se essa instância é igual ao <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> especificado.</summary>
		/// <param name="repeatBehavior">Um valor com o qual comparar essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo e o comportamento de repetição de <paramref name="repeatBehavior" /> forem iguais aos dessa instância; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040CF RID: 16591 RVA: 0x000FE14C File Offset: 0x000FD54C
		public bool Equals(RepeatBehavior repeatBehavior)
		{
			if (this._type != repeatBehavior._type)
			{
				return false;
			}
			switch (this._type)
			{
			case RepeatBehavior.RepeatBehaviorType.IterationCount:
				return this._iterationCount == repeatBehavior._iterationCount;
			case RepeatBehavior.RepeatBehaviorType.RepeatDuration:
				return this._repeatDuration == repeatBehavior._repeatDuration;
			case RepeatBehavior.RepeatBehaviorType.Forever:
				return true;
			default:
				return false;
			}
		}

		/// <summary>Indica se as duas estruturas de <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> especificadas são iguais.</summary>
		/// <param name="repeatBehavior1">O primeiro valor a ser comparado.</param>
		/// <param name="repeatBehavior2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo e o comportamento de repetição de <paramref name="repeatBehavior1" /> forem iguais aos do <paramref name="repeatBehavior2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040D0 RID: 16592 RVA: 0x000FE1A8 File Offset: 0x000FD5A8
		public static bool Equals(RepeatBehavior repeatBehavior1, RepeatBehavior repeatBehavior2)
		{
			return repeatBehavior1.Equals(repeatBehavior2);
		}

		/// <summary>Retorna o código hash dessa instância.</summary>
		/// <returns>Um código de hash do inteiro assinado de 32 bits.</returns>
		// Token: 0x060040D1 RID: 16593 RVA: 0x000FE1C0 File Offset: 0x000FD5C0
		public override int GetHashCode()
		{
			switch (this._type)
			{
			case RepeatBehavior.RepeatBehaviorType.IterationCount:
				return this._iterationCount.GetHashCode();
			case RepeatBehavior.RepeatBehaviorType.RepeatDuration:
				return this._repeatDuration.GetHashCode();
			case RepeatBehavior.RepeatBehaviorType.Forever:
				return 2147483605;
			default:
				return base.GetHashCode();
			}
		}

		/// <summary>Retorna uma representação de cadeia de caracteres dessa instância <see cref="T:System.Windows.Media.Animation.RepeatBehavior" />.</summary>
		/// <returns>Uma representação de cadeia de caracteres dessa instância.</returns>
		// Token: 0x060040D2 RID: 16594 RVA: 0x000FE21C File Offset: 0x000FD61C
		public override string ToString()
		{
			return this.InternalToString(null, null);
		}

		/// <summary>Retorna uma representação de cadeia de caracteres desta instância <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> com o formato especificado.</summary>
		/// <param name="formatProvider">O formato usado para construir o valor retornado.</param>
		/// <returns>Uma representação de cadeia de caracteres dessa instância.</returns>
		// Token: 0x060040D3 RID: 16595 RVA: 0x000FE234 File Offset: 0x000FD634
		public string ToString(IFormatProvider formatProvider)
		{
			return this.InternalToString(null, formatProvider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="formatProvider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x060040D4 RID: 16596 RVA: 0x000FE24C File Offset: 0x000FD64C
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.InternalToString(format, formatProvider);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x000FE264 File Offset: 0x000FD664
		internal string InternalToString(string format, IFormatProvider formatProvider)
		{
			switch (this._type)
			{
			case RepeatBehavior.RepeatBehaviorType.IterationCount:
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat(formatProvider, "{0:" + format + "}x", new object[]
				{
					this._iterationCount
				});
				return stringBuilder.ToString();
			}
			case RepeatBehavior.RepeatBehaviorType.RepeatDuration:
				return this._repeatDuration.ToString();
			case RepeatBehavior.RepeatBehaviorType.Forever:
				return "Forever";
			default:
				return null;
			}
		}

		/// <summary>Indica se as duas instâncias de <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> especificadas são iguais.</summary>
		/// <param name="repeatBehavior1">O primeiro valor a ser comparado.</param>
		/// <param name="repeatBehavior2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo e o comportamento de repetição de <paramref name="repeatBehavior1" /> forem iguais aos do <paramref name="repeatBehavior2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040D6 RID: 16598 RVA: 0x000FE2E0 File Offset: 0x000FD6E0
		public static bool operator ==(RepeatBehavior repeatBehavior1, RepeatBehavior repeatBehavior2)
		{
			return repeatBehavior1.Equals(repeatBehavior2);
		}

		/// <summary>Indica se duas instâncias <see cref="T:System.Windows.Media.Animation.RepeatBehavior" /> não são iguais.</summary>
		/// <param name="repeatBehavior1">O primeiro valor a ser comparado.</param>
		/// <param name="repeatBehavior2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="repeatBehavior1" /> e <paramref name="repeatBehavior2" /> forem de tipos diferentes ou as propriedades de comportamento de repetição não forem iguais; caso contrário <see langword="false" />.</returns>
		// Token: 0x060040D7 RID: 16599 RVA: 0x000FE2F8 File Offset: 0x000FD6F8
		public static bool operator !=(RepeatBehavior repeatBehavior1, RepeatBehavior repeatBehavior2)
		{
			return !repeatBehavior1.Equals(repeatBehavior2);
		}

		// Token: 0x040017B6 RID: 6070
		private double _iterationCount;

		// Token: 0x040017B7 RID: 6071
		private TimeSpan _repeatDuration;

		// Token: 0x040017B8 RID: 6072
		private RepeatBehavior.RepeatBehaviorType _type;

		// Token: 0x020008CB RID: 2251
		private enum RepeatBehaviorType
		{
			// Token: 0x04002973 RID: 10611
			IterationCount,
			// Token: 0x04002974 RID: 10612
			RepeatDuration,
			// Token: 0x04002975 RID: 10613
			Forever
		}
	}
}
