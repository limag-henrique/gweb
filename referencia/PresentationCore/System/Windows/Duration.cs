using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Representa a duração do tempo em que um <see cref="T:System.Windows.Media.Animation.Timeline" /> está ativo.</summary>
	// Token: 0x020001AC RID: 428
	[TypeConverter(typeof(DurationConverter))]
	public struct Duration
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Duration" /> com o valor <see cref="T:System.TimeSpan" /> fornecido.</summary>
		/// <param name="timeSpan">Representa o intervalo de tempo inicial desta duração.</param>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="timeSpan" /> é inicializado com um valor negativo.</exception>
		// Token: 0x0600067E RID: 1662 RVA: 0x0001DD3C File Offset: 0x0001D13C
		public Duration(TimeSpan timeSpan)
		{
			if (timeSpan < TimeSpan.Zero)
			{
				throw new ArgumentException(SR.Get("Timing_InvalidArgNonNegative"), "timeSpan");
			}
			this._durationType = Duration.DurationType.TimeSpan;
			this._timeSpan = timeSpan;
		}

		/// <summary>Cria implicitamente um <see cref="T:System.Windows.Duration" /> de determinado <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="timeSpan">
		///   <see cref="T:System.TimeSpan" /> do qual uma instância de <see cref="T:System.Windows.Duration" /> é criado implicitamente.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Duration" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <see cref="T:System.TimeSpan" /> é negativo.</exception>
		// Token: 0x0600067F RID: 1663 RVA: 0x0001DD7C File Offset: 0x0001D17C
		public static implicit operator Duration(TimeSpan timeSpan)
		{
			if (timeSpan < TimeSpan.Zero)
			{
				throw new ArgumentException(SR.Get("Timing_InvalidArgNonNegative"), "timeSpan");
			}
			return new Duration(timeSpan);
		}

		/// <summary>Adiciona duas instâncias do <see cref="T:System.Windows.Duration" /> juntas.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser adicionada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser adicionada.</param>
		/// <returns>Se ambas as instâncias de <see cref="T:System.Windows.Duration" /> têm valores <see cref="T:System.TimeSpan" />, esse método retorna a soma desses dois valores. Se algum um desses valores for definido como <see cref="P:System.Windows.Duration.Automatic" />, o método retornará <see cref="P:System.Windows.Duration.Automatic" />. Se algum um desses valores for definido como <see cref="P:System.Windows.Duration.Forever" />, o método retornará <see cref="P:System.Windows.Duration.Forever" />.  
		/// Se um dentre <paramref name="t1" /> ou <paramref name="t2" /> não tiver nenhum valor, esse método retornará <see langword="null" />.</returns>
		// Token: 0x06000680 RID: 1664 RVA: 0x0001DDB4 File Offset: 0x0001D1B4
		public static Duration operator +(Duration t1, Duration t2)
		{
			if (t1.HasTimeSpan && t2.HasTimeSpan)
			{
				return new Duration(t1._timeSpan + t2._timeSpan);
			}
			if (t1._durationType != Duration.DurationType.Automatic && t2._durationType != Duration.DurationType.Automatic)
			{
				return Duration.Forever;
			}
			return Duration.Automatic;
		}

		/// <summary>Subtrai o valor de uma instância de <see cref="T:System.Windows.Duration" /> de outro.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" />.</param>
		/// <param name="t2">A instância de <see cref="T:System.Windows.Duration" /> a ser subtraída.</param>
		/// <returns>Se ambas as instâncias de <see cref="T:System.Windows.Duration" /> tiverem valores, uma instância de <see cref="T:System.Windows.Duration" /> que representa o valor de <paramref name="t1" /> menos <paramref name="t2" />. Se <paramref name="t1" /> tiver um valor de <see cref="P:System.Windows.Duration.Forever" /> e <paramref name="t2" /> tiver um valor de <see cref="P:System.Windows.Duration.TimeSpan" />, esse método retornará <see cref="P:System.Windows.Duration.Forever" />. Caso contrário, esse método retorna <see langword="null" />.</returns>
		// Token: 0x06000681 RID: 1665 RVA: 0x0001DE08 File Offset: 0x0001D208
		public static Duration operator -(Duration t1, Duration t2)
		{
			if (t1.HasTimeSpan && t2.HasTimeSpan)
			{
				return new Duration(t1._timeSpan - t2._timeSpan);
			}
			if (t1._durationType == Duration.DurationType.Forever && t2.HasTimeSpan)
			{
				return Duration.Forever;
			}
			return Duration.Automatic;
		}

		/// <summary>Determina se duas instâncias de <see cref="T:System.Windows.Duration" /> são iguais.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se ambas as instâncias de <see cref="T:System.Windows.Duration" /> tiverem valores e forem iguais ou se ambas as instâncias de <see cref="T:System.Windows.Duration" /> são <see langword="null" />. Do contrário, esse método retorna <see langword="false" />.</returns>
		// Token: 0x06000682 RID: 1666 RVA: 0x0001DE5C File Offset: 0x0001D25C
		public static bool operator ==(Duration t1, Duration t2)
		{
			return t1.Equals(t2);
		}

		/// <summary>Determina se duas instâncias de <see cref="T:System.Windows.Duration" /> não são iguais.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se exatamente um dos <paramref name="t1" /> ou <paramref name="t2" /> representar um valor ou se ambos representam valores diferentes; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000683 RID: 1667 RVA: 0x0001DE74 File Offset: 0x0001D274
		public static bool operator !=(Duration t1, Duration t2)
		{
			return !t1.Equals(t2);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Duration" /> é maior que outra.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se ambos <paramref name="t1" /> e <paramref name="t2" /> tiverem valores e <paramref name="t1" /> for maior que <paramref name="t2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000684 RID: 1668 RVA: 0x0001DE8C File Offset: 0x0001D28C
		public static bool operator >(Duration t1, Duration t2)
		{
			if (t1.HasTimeSpan && t2.HasTimeSpan)
			{
				return t1._timeSpan > t2._timeSpan;
			}
			return (!t1.HasTimeSpan || t2._durationType != Duration.DurationType.Forever) && (t1._durationType == Duration.DurationType.Forever && t2.HasTimeSpan);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Duration" /> é maior ou igual a outra.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se ambos <paramref name="t1" /> e <paramref name="t2" /> tiverem valores e <paramref name="t1" /> for maior ou igual a <paramref name="t2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000685 RID: 1669 RVA: 0x0001DEE8 File Offset: 0x0001D2E8
		public static bool operator >=(Duration t1, Duration t2)
		{
			return (t1._durationType == Duration.DurationType.Automatic && t2._durationType == Duration.DurationType.Automatic) || (t1._durationType != Duration.DurationType.Automatic && t2._durationType != Duration.DurationType.Automatic && !(t1 < t2));
		}

		/// <summary>Determina se o valor de uma instância de <see cref="T:System.Windows.Duration" /> é menor que o valor de outra.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se ambos <paramref name="t1" /> e <paramref name="t2" /> tiverem valores e <paramref name="t1" /> for menor que <paramref name="t2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000686 RID: 1670 RVA: 0x0001DF24 File Offset: 0x0001D324
		public static bool operator <(Duration t1, Duration t2)
		{
			if (t1.HasTimeSpan && t2.HasTimeSpan)
			{
				return t1._timeSpan < t2._timeSpan;
			}
			if (t1.HasTimeSpan && t2._durationType == Duration.DurationType.Forever)
			{
				return true;
			}
			if (t1._durationType == Duration.DurationType.Forever)
			{
				bool hasTimeSpan = t2.HasTimeSpan;
				return false;
			}
			return false;
		}

		/// <summary>Determina se o valor de uma instância de <see cref="T:System.Windows.Duration" /> é menor ou igual ao valor de outra.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se ambos <paramref name="t1" /> e <paramref name="t2" /> tiverem valores e <paramref name="t1" /> for menor ou igual a <paramref name="t2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000687 RID: 1671 RVA: 0x0001DF80 File Offset: 0x0001D380
		public static bool operator <=(Duration t1, Duration t2)
		{
			return (t1._durationType == Duration.DurationType.Automatic && t2._durationType == Duration.DurationType.Automatic) || (t1._durationType != Duration.DurationType.Automatic && t2._durationType != Duration.DurationType.Automatic && !(t1 > t2));
		}

		/// <summary>Compara um valor <see cref="T:System.Windows.Duration" /> com outro.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>Se <paramref name="t1" /> for menor do que <paramref name="t2" />, um valor negativo que representa a diferença. Se <paramref name="t1" /> for igual a <paramref name="t2" />, zero. Se <paramref name="t1" /> for maior do que <paramref name="t2" />, um valor positivo que representa a diferença.</returns>
		// Token: 0x06000688 RID: 1672 RVA: 0x0001DFBC File Offset: 0x0001D3BC
		public static int Compare(Duration t1, Duration t2)
		{
			if (t1._durationType == Duration.DurationType.Automatic)
			{
				if (t2._durationType == Duration.DurationType.Automatic)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (t2._durationType == Duration.DurationType.Automatic)
				{
					return 1;
				}
				if (t1 < t2)
				{
					return -1;
				}
				if (t1 > t2)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Retorna a instância especificada do <see cref="T:System.Windows.Duration" />.</summary>
		/// <param name="duration">A instância de <see cref="T:System.Windows.Duration" /> a ser obtido.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Duration" />.</returns>
		// Token: 0x06000689 RID: 1673 RVA: 0x0001E000 File Offset: 0x0001D400
		public static Duration Plus(Duration duration)
		{
			return duration;
		}

		/// <summary>Retorna a instância especificada do <see cref="T:System.Windows.Duration" />.</summary>
		/// <param name="duration">A instância de <see cref="T:System.Windows.Duration" /> a ser obtido.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Duration" />.</returns>
		// Token: 0x0600068A RID: 1674 RVA: 0x0001E010 File Offset: 0x0001D410
		public static Duration operator +(Duration duration)
		{
			return duration;
		}

		/// <summary>Obtém um valor que indica se esse <see cref="T:System.Windows.Duration" /> representa um valor <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>True se esta duração é um <see cref="T:System.TimeSpan" /> valor; caso contrário, false.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001E020 File Offset: 0x0001D420
		public bool HasTimeSpan
		{
			get
			{
				return this._durationType == Duration.DurationType.TimeSpan;
			}
		}

		/// <summary>Obtém um valor <see cref="T:System.Windows.Duration" /> que é determinado automaticamente.</summary>
		/// <returns>Um <see cref="T:System.Windows.Duration" /> inicializada com um valor automático.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001E038 File Offset: 0x0001D438
		public static Duration Automatic
		{
			get
			{
				return new Duration
				{
					_durationType = Duration.DurationType.Automatic
				};
			}
		}

		/// <summary>Obtém um valor <see cref="T:System.Windows.Duration" /> que representa um intervalo infinito.</summary>
		/// <returns>Um <see cref="T:System.Windows.Duration" /> inicializado com um valor para sempre.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001E058 File Offset: 0x0001D458
		public static Duration Forever
		{
			get
			{
				return new Duration
				{
					_durationType = Duration.DurationType.Forever
				};
			}
		}

		/// <summary>Obtém o valor <see cref="T:System.TimeSpan" /> que esse <see cref="T:System.Windows.Duration" /> representa.</summary>
		/// <returns>O valor <see cref="T:System.TimeSpan" /> que esse <see cref="T:System.Windows.Duration" /> representa.</returns>
		/// <exception cref="T:System.InvalidOperationException">Ocorrerá se <see cref="T:System.Windows.Duration" /> for <see langword="null" />.</exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001E078 File Offset: 0x0001D478
		public TimeSpan TimeSpan
		{
			get
			{
				if (this.HasTimeSpan)
				{
					return this._timeSpan;
				}
				throw new InvalidOperationException(SR.Get("Timing_NotTimeSpan", new object[]
				{
					this
				}));
			}
		}

		/// <summary>Adiciona o valor da instância especificada de <see cref="T:System.Windows.Duration" /> ao valor da instância atual.</summary>
		/// <param name="duration">Uma instância de <see cref="T:System.Windows.Duration" /> que representa o valor da instância atual mais a <paramref name="duration" />.</param>
		/// <returns>Se ambas as instâncias de <see cref="T:System.Windows.Duration" /> tiverem valores, uma instância de <see cref="T:System.Windows.Duration" /> que representa os valores combinados. Caso contrário, esse método retorna <see langword="null" />.</returns>
		// Token: 0x0600068F RID: 1679 RVA: 0x0001E0B8 File Offset: 0x0001D4B8
		public Duration Add(Duration duration)
		{
			return this + duration;
		}

		/// <summary>Determina se um objeto especificado é igual a uma instância de <see cref="T:System.Windows.Duration" />.</summary>
		/// <param name="value">Objeto a ser verificado quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se o valor for igual à instância atual de Duração; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000690 RID: 1680 RVA: 0x0001E0D4 File Offset: 0x0001D4D4
		public override bool Equals(object value)
		{
			return value != null && value is Duration && this.Equals((Duration)value);
		}

		/// <summary>Determina se um <see cref="T:System.Windows.Duration" /> especificado é igual a esta instância de <see cref="T:System.Windows.Duration" />.</summary>
		/// <param name="duration">Instância de <see cref="T:System.Windows.Duration" /> a ser verificada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="duration" /> for igual à instância atual de <see cref="T:System.Windows.Duration" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000691 RID: 1681 RVA: 0x0001E0FC File Offset: 0x0001D4FC
		public bool Equals(Duration duration)
		{
			if (this.HasTimeSpan)
			{
				return duration.HasTimeSpan && this._timeSpan == duration._timeSpan;
			}
			return this._durationType == duration._durationType;
		}

		/// <summary>Determina se duas instâncias de <see cref="T:System.Windows.Duration" /> são iguais.</summary>
		/// <param name="t1">A primeira instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <param name="t2">A segunda instância de <see cref="T:System.Windows.Duration" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="t1" /> for igual a <paramref name="t2" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000692 RID: 1682 RVA: 0x0001E13C File Offset: 0x0001D53C
		public static bool Equals(Duration t1, Duration t2)
		{
			return t1.Equals(t2);
		}

		/// <summary>Obtém um código hash para essa instância.</summary>
		/// <returns>Um código hash do inteiro com sinal de 32 bits.</returns>
		// Token: 0x06000693 RID: 1683 RVA: 0x0001E154 File Offset: 0x0001D554
		public override int GetHashCode()
		{
			if (this.HasTimeSpan)
			{
				return this._timeSpan.GetHashCode();
			}
			return this._durationType.GetHashCode() + 17;
		}

		/// <summary>Subtrai o valor da instância especificada de <see cref="T:System.Windows.Duration" /> dessa instância.</summary>
		/// <param name="duration">A instância de <see cref="T:System.Windows.Duration" /> a ser subtraída da instância atual.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Duration" /> cujo valor é o resultado dessa instância menos o valor de <paramref name="duration" />.</returns>
		// Token: 0x06000694 RID: 1684 RVA: 0x0001E190 File Offset: 0x0001D590
		public Duration Subtract(Duration duration)
		{
			return this - duration;
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Duration" /> em uma representação <see cref="T:System.String" />.</summary>
		/// <returns>Uma representação <see cref="T:System.String" /> dessa instância de <see cref="T:System.Windows.Duration" />.</returns>
		// Token: 0x06000695 RID: 1685 RVA: 0x0001E1AC File Offset: 0x0001D5AC
		public override string ToString()
		{
			if (this.HasTimeSpan)
			{
				return TypeDescriptor.GetConverter(this._timeSpan).ConvertToString(this._timeSpan);
			}
			if (this._durationType == Duration.DurationType.Forever)
			{
				return "Forever";
			}
			return "Automatic";
		}

		// Token: 0x040005A0 RID: 1440
		private TimeSpan _timeSpan;

		// Token: 0x040005A1 RID: 1441
		private Duration.DurationType _durationType;

		// Token: 0x020007F5 RID: 2037
		private enum DurationType
		{
			// Token: 0x0400268A RID: 9866
			Automatic,
			// Token: 0x0400268B RID: 9867
			TimeSpan,
			// Token: 0x0400268C RID: 9868
			Forever
		}
	}
}
