using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Durante o curso relativo de uma animação, uma instância <see cref="T:System.Windows.Media.Animation.KeyTime" /> especifica o momento preciso em que um determinado quadro chave deve ocorrer.</summary>
	// Token: 0x02000572 RID: 1394
	[TypeConverter(typeof(KeyTimeConverter))]
	public struct KeyTime : IEquatable<KeyTime>
	{
		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />, com a propriedade <see cref="T:System.Windows.Media.Animation.KeyTimeType" /> inicializada com o valor do parâmetro especificado.</summary>
		/// <param name="percent">O valor do novo <see cref="T:System.Windows.Media.Animation.KeyTime" />.</param>
		/// <returns>Uma nova instância <see cref="T:System.Windows.Media.Animation.KeyTime" />, inicializada para o valor de <paramref name="percent" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="percent" /> é menor que 0,0 ou maior que 1,0.</exception>
		// Token: 0x0600409A RID: 16538 RVA: 0x000FD63C File Offset: 0x000FCA3C
		public static KeyTime FromPercent(double percent)
		{
			if (percent < 0.0 || percent > 1.0)
			{
				throw new ArgumentOutOfRangeException("percent", SR.Get("Animation_KeyTime_InvalidPercentValue", new object[]
				{
					percent
				}));
			}
			return new KeyTime
			{
				_value = percent,
				_type = KeyTimeType.Percent
			};
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />, com a propriedade <see cref="T:System.Windows.Media.Animation.KeyTimeType" /> inicializada com o valor do parâmetro especificado.</summary>
		/// <param name="timeSpan">O valor do novo <see cref="T:System.Windows.Media.Animation.KeyTime" />.</param>
		/// <returns>Uma nova instância <see cref="T:System.Windows.Media.Animation.KeyTime" />, inicializada para o valor de <paramref name="timeSpan" />.</returns>
		// Token: 0x0600409B RID: 16539 RVA: 0x000FD6A4 File Offset: 0x000FCAA4
		public static KeyTime FromTimeSpan(TimeSpan timeSpan)
		{
			if (timeSpan < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("timeSpan", SR.Get("Animation_KeyTime_LessThanZero", new object[]
				{
					timeSpan
				}));
			}
			return new KeyTime
			{
				_value = timeSpan,
				_type = KeyTimeType.TimeSpan
			};
		}

		/// <summary>Obtém o valor de <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" /> que divide o tempo alocado da animação uniformemente entre os quadros chave.</summary>
		/// <returns>Um valor <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x000FD700 File Offset: 0x000FCB00
		public static KeyTime Uniform
		{
			get
			{
				return new KeyTime
				{
					_type = KeyTimeType.Uniform
				};
			}
		}

		/// <summary>Obtém o valor <see cref="P:System.Windows.Media.Animation.KeyTime.Paced" /> que cria o comportamento de temporização, resultando em uma animação que é interpolada a uma taxa constante.</summary>
		/// <returns>Um valor <see cref="P:System.Windows.Media.Animation.KeyTime.Paced" />.</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x000FD720 File Offset: 0x000FCB20
		public static KeyTime Paced
		{
			get
			{
				return new KeyTime
				{
					_type = KeyTimeType.Paced
				};
			}
		}

		/// <summary>Indica se as duas estruturas de <see cref="T:System.Windows.Media.Animation.KeyTime" /> especificadas são iguais.</summary>
		/// <param name="keyTime1">O primeiro valor a ser comparado.</param>
		/// <param name="keyTime2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os valores de <paramref name="keyTime1" /> e <paramref name="keyTime2" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600409E RID: 16542 RVA: 0x000FD740 File Offset: 0x000FCB40
		public static bool Equals(KeyTime keyTime1, KeyTime keyTime2)
		{
			if (keyTime1._type == keyTime2._type)
			{
				switch (keyTime1._type)
				{
				case KeyTimeType.Percent:
					if ((double)keyTime1._value != (double)keyTime2._value)
					{
						return false;
					}
					break;
				case KeyTimeType.TimeSpan:
					if ((TimeSpan)keyTime1._value != (TimeSpan)keyTime2._value)
					{
						return false;
					}
					break;
				}
				return true;
			}
			return false;
		}

		/// <summary>Operador sobrecarregado que compara duas estruturas <see cref="T:System.Windows.Media.Animation.KeyTime" /> quanto à igualdade.</summary>
		/// <param name="keyTime1">O primeiro valor a ser comparado.</param>
		/// <param name="keyTime2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="keyTime1" /> e <paramref name="keyTime2" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600409F RID: 16543 RVA: 0x000FD7B8 File Offset: 0x000FCBB8
		public static bool operator ==(KeyTime keyTime1, KeyTime keyTime2)
		{
			return KeyTime.Equals(keyTime1, keyTime2);
		}

		/// <summary>Operador sobrecarregado que compara duas estruturas <see cref="T:System.Windows.Media.Animation.KeyTime" /> quanto à desigualdade.</summary>
		/// <param name="keyTime1">O primeiro valor a ser comparado.</param>
		/// <param name="keyTime2">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="keyTime1" /> e <paramref name="keyTime2" /> não forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040A0 RID: 16544 RVA: 0x000FD7CC File Offset: 0x000FCBCC
		public static bool operator !=(KeyTime keyTime1, KeyTime keyTime2)
		{
			return !KeyTime.Equals(keyTime1, keyTime2);
		}

		/// <summary>Indica se essa instância é igual ao <see cref="T:System.Windows.Media.Animation.KeyTime" /> especificado.</summary>
		/// <param name="value">O objeto a ser comparado com essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for igual a essa instância; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040A1 RID: 16545 RVA: 0x000FD7E4 File Offset: 0x000FCBE4
		public bool Equals(KeyTime value)
		{
			return KeyTime.Equals(this, value);
		}

		/// <summary>Indica se essa instância é igual ao objeto especificado.</summary>
		/// <param name="value">O objeto a ser comparado com essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for um <see cref="T:System.Windows.Media.Animation.KeyTime" /> que representa o mesmo período de tempo que esta instância; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060040A2 RID: 16546 RVA: 0x000FD800 File Offset: 0x000FCC00
		public override bool Equals(object value)
		{
			return value != null && value is KeyTime && this == (KeyTime)value;
		}

		/// <summary>Retorna um código hash inteiro que representa esta instância.</summary>
		/// <returns>Um código hash inteiro.</returns>
		// Token: 0x060040A3 RID: 16547 RVA: 0x000FD82C File Offset: 0x000FCC2C
		public override int GetHashCode()
		{
			if (this._value != null)
			{
				return this._value.GetHashCode();
			}
			return this._type.GetHashCode();
		}

		/// <summary>Retorna uma cadeia de caracteres que representa esta instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>Uma representação de cadeia de caracteres dessa instância.</returns>
		// Token: 0x060040A4 RID: 16548 RVA: 0x000FD860 File Offset: 0x000FCC60
		public override string ToString()
		{
			KeyTimeConverter keyTimeConverter = new KeyTimeConverter();
			return keyTimeConverter.ConvertToString(this);
		}

		/// <summary>Operador sobrecarregado que converte implicitamente um <see cref="P:System.Windows.Media.Animation.KeyTime.TimeSpan" /> em um <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <param name="timeSpan">O valor <see cref="P:System.Windows.Media.Animation.KeyTime.TimeSpan" /> a ser convertido.</param>
		/// <returns>A nova instância <see cref="T:System.Windows.Media.Animation.KeyTime" />.</returns>
		// Token: 0x060040A5 RID: 16549 RVA: 0x000FD884 File Offset: 0x000FCC84
		public static implicit operator KeyTime(TimeSpan timeSpan)
		{
			return KeyTime.FromTimeSpan(timeSpan);
		}

		/// <summary>Obtém a hora em que o quadro chave termina, expressa como uma hora relativa ao início da animação.</summary>
		/// <returns>Um valor <see cref="P:System.Windows.Media.Animation.KeyTime.TimeSpan" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Se esta instância não for do tipo <see cref="P:System.Windows.Media.Animation.KeyTime.TimeSpan" />.</exception>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000FD898 File Offset: 0x000FCC98
		public TimeSpan TimeSpan
		{
			get
			{
				if (this._type == KeyTimeType.TimeSpan)
				{
					return (TimeSpan)this._value;
				}
				throw new InvalidOperationException();
			}
		}

		/// <summary>Obtém a hora em que o quadro chave termina, expressada como um percentual da duração total da animação.</summary>
		/// <returns>Um valor <see cref="P:System.Windows.Media.Animation.KeyTime.Percent" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Se esta instância não for do tipo <see cref="P:System.Windows.Media.Animation.KeyTime.Percent" />.</exception>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x000FD8C0 File Offset: 0x000FCCC0
		public double Percent
		{
			get
			{
				if (this._type == KeyTimeType.Percent)
				{
					return (double)this._value;
				}
				throw new InvalidOperationException();
			}
		}

		/// <summary>Obtém o valor <see cref="P:System.Windows.Media.Animation.KeyTime.Type" /> que essa instância representa.</summary>
		/// <returns>Um valor <see cref="P:System.Windows.Media.Animation.KeyTime.Type" />.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000FD8E8 File Offset: 0x000FCCE8
		public KeyTimeType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040017A1 RID: 6049
		private object _value;

		// Token: 0x040017A2 RID: 6050
		private KeyTimeType _type;
	}
}
