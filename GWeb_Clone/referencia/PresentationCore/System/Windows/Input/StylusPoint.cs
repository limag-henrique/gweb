using System;
using System.Collections.ObjectModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa um único ponto de dados coletado do digitalizador e da caneta.</summary>
	// Token: 0x020002B9 RID: 697
	public struct StylusPoint : IEquatable<StylusPoint>
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPoint" /> usando as coordenadas especificadas (x, y).</summary>
		/// <param name="x">A coordenada x do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="y">A coordenada y do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		// Token: 0x060014A3 RID: 5283 RVA: 0x0004BB28 File Offset: 0x0004AF28
		public StylusPoint(double x, double y)
		{
			this = new StylusPoint(x, y, StylusPoint.DefaultPressure, null, null, false, false);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPoint" /> usando as coordenadas (x, y) e a pressão especificadas.</summary>
		/// <param name="x">A coordenada x do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="y">A coordenada y do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="pressureFactor">A quantidade de pressão aplicada no <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pressureFactor" /> é menor que 0 ou maior que 1.</exception>
		// Token: 0x060014A4 RID: 5284 RVA: 0x0004BB48 File Offset: 0x0004AF48
		public StylusPoint(double x, double y, float pressureFactor)
		{
			this = new StylusPoint(x, y, pressureFactor, null, null, false, true);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPoint" /> usando as coordenadas especificadas (x, y), um <paramref name="pressureFactor" /> e parâmetros adicionais especificados na <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		/// <param name="x">A coordenada x do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="y">A coordenada y do <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="pressureFactor">A quantidade de pressão aplicada no <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="stylusPointDescription">Uma <see cref="T:System.Windows.Input.StylusPointDescription" /> que especifica as propriedades adicionais armazenadas no <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="additionalValues">Uma matriz de inteiros com sinal de 32 bits que contém os valores das propriedades definidas em <paramref name="stylusPointDescription" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pressureFactor" /> é menor que 0 ou maior que 1.  
		///
		/// ou - 
		/// Os valores em <paramref name="additionalValues" /> que correspondem às propriedades do botão não são 0 nem 1.</exception>
		/// <exception cref="T:System.ArgumentException">O número de valores em <paramref name="additionalValues" /> não corresponde ao número de propriedades em <paramref name="stylusPointDescription" /> menos 3.</exception>
		// Token: 0x060014A5 RID: 5285 RVA: 0x0004BB64 File Offset: 0x0004AF64
		public StylusPoint(double x, double y, float pressureFactor, StylusPointDescription stylusPointDescription, int[] additionalValues)
		{
			this = new StylusPoint(x, y, pressureFactor, stylusPointDescription, additionalValues, true, true);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0004BB80 File Offset: 0x0004AF80
		internal StylusPoint(double x, double y, float pressureFactor, StylusPointDescription stylusPointDescription, int[] additionalValues, bool validateAdditionalData, bool validatePressureFactor)
		{
			if (double.IsNaN(x))
			{
				throw new ArgumentOutOfRangeException("x", SR.Get("InvalidStylusPointXYNaN"));
			}
			if (double.IsNaN(y))
			{
				throw new ArgumentOutOfRangeException("y", SR.Get("InvalidStylusPointXYNaN"));
			}
			if (validatePressureFactor && (pressureFactor == float.NaN || pressureFactor < 0f || pressureFactor > 1f))
			{
				throw new ArgumentOutOfRangeException("pressureFactor", SR.Get("InvalidPressureValue"));
			}
			this._x = StylusPoint.GetClampedXYValue(x);
			this._y = StylusPoint.GetClampedXYValue(y);
			this._stylusPointDescription = stylusPointDescription;
			this._additionalValues = additionalValues;
			this._pressureFactor = pressureFactor;
			if (validateAdditionalData)
			{
				if (stylusPointDescription == null)
				{
					throw new ArgumentNullException("stylusPointDescription");
				}
				if (stylusPointDescription.PropertyCount > StylusPointDescription.RequiredCountOfProperties && additionalValues == null)
				{
					throw new ArgumentNullException("additionalValues");
				}
				if (additionalValues != null)
				{
					ReadOnlyCollection<StylusPointPropertyInfo> stylusPointProperties = stylusPointDescription.GetStylusPointProperties();
					int num = stylusPointProperties.Count - StylusPointDescription.RequiredCountOfProperties;
					if (additionalValues.Length != num)
					{
						throw new ArgumentException(SR.Get("InvalidAdditionalDataForStylusPoint"), "additionalValues");
					}
					int[] additionalValues2 = new int[stylusPointDescription.GetExpectedAdditionalDataCount()];
					this._additionalValues = additionalValues2;
					int i = StylusPointDescription.RequiredCountOfProperties;
					int num2 = 0;
					while (i < stylusPointProperties.Count)
					{
						this.SetPropertyValue(stylusPointProperties[i], additionalValues[num2], false);
						i++;
						num2++;
					}
				}
			}
		}

		/// <summary>Obtém ou define o valor da coordenada x do <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>A coordenada x do <see cref="T:System.Windows.Input.StylusPoint" />.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0004BCD8 File Offset: 0x0004B0D8
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0004BCEC File Offset: 0x0004B0EC
		public double X
		{
			get
			{
				return this._x;
			}
			set
			{
				if (double.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("X", SR.Get("InvalidStylusPointXYNaN"));
				}
				this._x = StylusPoint.GetClampedXYValue(value);
			}
		}

		/// <summary>Obtém ou define a coordenada y do <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>A coordenada y do <see cref="T:System.Windows.Input.StylusPoint" />.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004BD24 File Offset: 0x0004B124
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0004BD38 File Offset: 0x0004B138
		public double Y
		{
			get
			{
				return this._y;
			}
			set
			{
				if (double.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("Y", SR.Get("InvalidStylusPointXYNaN"));
				}
				this._y = StylusPoint.GetClampedXYValue(value);
			}
		}

		/// <summary>Obtém ou define um valor entre 0 e 1 que reflete a quantidade de pressão que a caneta aplica à superfície do digitalizador quando o <see cref="T:System.Windows.Input.StylusPoint" /> é criado.</summary>
		/// <returns>Valor entre 0 e 1 que indica a quantidade de pressão que a caneta aplica-se a superfície do digitalizador como o <see cref="T:System.Windows.Input.StylusPoint" /> é criado.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade <see cref="P:System.Windows.Input.StylusPoint.PressureFactor" /> é definida como um valor menor que 0 ou maior que 1.</exception>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0004BD70 File Offset: 0x0004B170
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0004BDAC File Offset: 0x0004B1AC
		public float PressureFactor
		{
			get
			{
				if (this._pressureFactor > 1f)
				{
					return 1f;
				}
				if (this._pressureFactor < 0f)
				{
					return 0f;
				}
				return this._pressureFactor;
			}
			set
			{
				if (value < 0f || value > 1f)
				{
					throw new ArgumentOutOfRangeException("PressureFactor", SR.Get("InvalidPressureValue"));
				}
				this._pressureFactor = value;
			}
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Input.StylusPointDescription" /> que especifica as propriedades armazenadas no <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointDescription" /> Especifica as propriedades armazenadas do <see cref="T:System.Windows.Input.StylusPoint" />.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004BDE8 File Offset: 0x0004B1E8
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x0004BE10 File Offset: 0x0004B210
		public StylusPointDescription Description
		{
			get
			{
				if (this._stylusPointDescription == null)
				{
					this._stylusPointDescription = new StylusPointDescription();
				}
				return this._stylusPointDescription;
			}
			internal set
			{
				this._stylusPointDescription = value;
			}
		}

		/// <summary>Retorna se o <see cref="T:System.Windows.Input.StylusPoint" /> atual contém a propriedade especificada.</summary>
		/// <param name="stylusPointProperty">A <see cref="T:System.Windows.Input.StylusPointProperty" /> a ser verificada no <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se a <see cref="T:System.Windows.Input.StylusPointProperty" /> especificada estiver no <see cref="T:System.Windows.Input.StylusPoint" /> atual, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014AF RID: 5295 RVA: 0x0004BE24 File Offset: 0x0004B224
		public bool HasProperty(StylusPointProperty stylusPointProperty)
		{
			return this.Description.HasProperty(stylusPointProperty);
		}

		/// <summary>Retorna o valor da propriedade especificada.</summary>
		/// <param name="stylusPointProperty">A <see cref="T:System.Windows.Input.StylusPointProperty" /> que especifica qual valor da propriedade deve ser obtido.</param>
		/// <returns>O valor da <see cref="T:System.Windows.Input.StylusPointProperty" /> especificada.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stylusPointProperty" /> não é uma das propriedades na <see cref="P:System.Windows.Input.StylusPoint.Description" />.</exception>
		// Token: 0x060014B0 RID: 5296 RVA: 0x0004BE40 File Offset: 0x0004B240
		public int GetPropertyValue(StylusPointProperty stylusPointProperty)
		{
			if (stylusPointProperty == null)
			{
				throw new ArgumentNullException("stylusPointProperty");
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.X)
			{
				return (int)this._x;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Y)
			{
				return (int)this._y;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.NormalPressure)
			{
				StylusPointPropertyInfo propertyInfo = this.Description.GetPropertyInfo(StylusPointProperties.NormalPressure);
				int maximum = propertyInfo.Maximum;
				return (int)(this._pressureFactor * (float)maximum);
			}
			int propertyIndex = this.Description.GetPropertyIndex(stylusPointProperty.Id);
			if (-1 == propertyIndex)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointProperty"), "stylusPointProperty");
			}
			if (!stylusPointProperty.IsButton)
			{
				return this._additionalValues[propertyIndex - 3];
			}
			int num = this._additionalValues[this._additionalValues.Length - 1];
			int buttonBitPosition = this.Description.GetButtonBitPosition(stylusPointProperty);
			int num2 = 1 << buttonBitPosition;
			if ((num & num2) != 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Define a propriedade especificada com o valor especificado.</summary>
		/// <param name="stylusPointProperty">A <see cref="T:System.Windows.Input.StylusPointProperty" /> que especifica qual valor da propriedade deve ser definido.</param>
		/// <param name="value">O valor da propriedade.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stylusPointProperty" /> não é uma das propriedades na <see cref="P:System.Windows.Input.StylusPoint.Description" />.</exception>
		// Token: 0x060014B1 RID: 5297 RVA: 0x0004BF34 File Offset: 0x0004B334
		public void SetPropertyValue(StylusPointProperty stylusPointProperty, int value)
		{
			this.SetPropertyValue(stylusPointProperty, value, true);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0004BF4C File Offset: 0x0004B34C
		internal void SetPropertyValue(StylusPointProperty stylusPointProperty, int value, bool copyBeforeWrite)
		{
			if (stylusPointProperty == null)
			{
				throw new ArgumentNullException("stylusPointProperty");
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.X)
			{
				double xyValue = (double)value;
				this._x = StylusPoint.GetClampedXYValue(xyValue);
				return;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Y)
			{
				double xyValue2 = (double)value;
				this._y = StylusPoint.GetClampedXYValue(xyValue2);
				return;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.NormalPressure)
			{
				StylusPointPropertyInfo propertyInfo = this.Description.GetPropertyInfo(StylusPointProperties.NormalPressure);
				int minimum = propertyInfo.Minimum;
				int maximum = propertyInfo.Maximum;
				if (maximum == 0)
				{
					this._pressureFactor = 0f;
					return;
				}
				this._pressureFactor = Convert.ToSingle(minimum + value) / Convert.ToSingle(maximum);
				return;
			}
			else
			{
				int propertyIndex = this.Description.GetPropertyIndex(stylusPointProperty.Id);
				if (-1 == propertyIndex)
				{
					throw new ArgumentException(SR.Get("InvalidStylusPointProperty"), "propertyId");
				}
				if (!stylusPointProperty.IsButton)
				{
					if (copyBeforeWrite)
					{
						this.CopyAdditionalData();
					}
					this._additionalValues[propertyIndex - 3] = value;
					return;
				}
				if (value < 0 || value > 1)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("InvalidMinMaxForButton"));
				}
				if (copyBeforeWrite)
				{
					this.CopyAdditionalData();
				}
				int num = this._additionalValues[this._additionalValues.Length - 1];
				int buttonBitPosition = this.Description.GetButtonBitPosition(stylusPointProperty);
				int num2 = 1 << buttonBitPosition;
				if (value == 0)
				{
					num &= ~num2;
				}
				else
				{
					num |= num2;
				}
				this._additionalValues[this._additionalValues.Length - 1] = num;
				return;
			}
		}

		/// <summary>Converte o <see cref="T:System.Windows.Input.StylusPoint" /> especificado em um <see cref="T:System.Windows.Point" />.</summary>
		/// <param name="stylusPoint">O <see cref="T:System.Windows.Input.StylusPoint" /> a ser convertido em um <see cref="T:System.Windows.Point" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Point" /> que contém as mesmas coordenadas (x, y) que <paramref name="stylusPoint" />.</returns>
		// Token: 0x060014B3 RID: 5299 RVA: 0x0004C0C4 File Offset: 0x0004B4C4
		public static explicit operator Point(StylusPoint stylusPoint)
		{
			return new Point(stylusPoint.X, stylusPoint.Y);
		}

		/// <summary>Converte uma <see cref="T:System.Windows.Input.StylusPoint" /> em uma <see cref="T:System.Windows.Point" />.</summary>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Point" />.</returns>
		// Token: 0x060014B4 RID: 5300 RVA: 0x0004C0E4 File Offset: 0x0004B4E4
		public Point ToPoint()
		{
			return new Point(this.X, this.Y);
		}

		/// <summary>Compara dois objetos <see cref="T:System.Windows.Input.StylusPoint" /> especificados e determina se eles são iguais.</summary>
		/// <param name="stylusPoint1">O primeiro <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <param name="stylusPoint2">O segundo <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos <see cref="T:System.Windows.Input.StylusPoint" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014B5 RID: 5301 RVA: 0x0004C104 File Offset: 0x0004B504
		public static bool operator ==(StylusPoint stylusPoint1, StylusPoint stylusPoint2)
		{
			return StylusPoint.Equals(stylusPoint1, stylusPoint2);
		}

		/// <summary>Retorna um valor booliano que indica se os objetos <see cref="T:System.Windows.Input.StylusPoint" /> especificados são diferentes.</summary>
		/// <param name="stylusPoint1">O primeiro <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <param name="stylusPoint2">O segundo <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos especificados <see cref="T:System.Windows.Input.StylusPoint" /> forem diferentes, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014B6 RID: 5302 RVA: 0x0004C118 File Offset: 0x0004B518
		public static bool operator !=(StylusPoint stylusPoint1, StylusPoint stylusPoint2)
		{
			return !StylusPoint.Equals(stylusPoint1, stylusPoint2);
		}

		/// <summary>Retorna um valor booliano que indica se os dois objetos <see cref="T:System.Windows.Input.StylusPoint" /> especificados são iguais.</summary>
		/// <param name="stylusPoint1">O primeiro <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <param name="stylusPoint2">O segundo <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos <see cref="T:System.Windows.Input.StylusPoint" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014B7 RID: 5303 RVA: 0x0004C130 File Offset: 0x0004B530
		public static bool Equals(StylusPoint stylusPoint1, StylusPoint stylusPoint2)
		{
			if (stylusPoint1._x != stylusPoint2._x || stylusPoint1._y != stylusPoint2._y || stylusPoint1._pressureFactor != stylusPoint2._pressureFactor)
			{
				return false;
			}
			if (stylusPoint1._additionalValues == null && stylusPoint2._additionalValues == null)
			{
				return true;
			}
			if (stylusPoint1.Description == stylusPoint2.Description || StylusPointDescription.AreCompatible(stylusPoint1.Description, stylusPoint2.Description))
			{
				for (int i = 0; i < stylusPoint1._additionalValues.Length; i++)
				{
					if (stylusPoint1._additionalValues[i] != stylusPoint2._additionalValues[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Retorna um valor que indica se o objeto especificado é igual ao <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <param name="o">O <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado com o <see cref="T:System.Windows.Input.StylusPoint" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014B8 RID: 5304 RVA: 0x0004C1D4 File Offset: 0x0004B5D4
		public override bool Equals(object o)
		{
			if (o == null || !(o is StylusPoint))
			{
				return false;
			}
			StylusPoint stylusPoint = (StylusPoint)o;
			return StylusPoint.Equals(this, stylusPoint);
		}

		/// <summary>Retorna um valor booliano que indica se o <see cref="T:System.Windows.Input.StylusPoint" /> especificado é igual ao <see cref="T:System.Windows.Input.StylusPoint" /> atual.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Input.StylusPoint" /> a ser comparado com o <see cref="T:System.Windows.Input.StylusPoint" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos <see cref="T:System.Windows.Input.StylusPoint" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014B9 RID: 5305 RVA: 0x0004C204 File Offset: 0x0004B604
		public bool Equals(StylusPoint value)
		{
			return StylusPoint.Equals(this, value);
		}

		/// <summary>Retorna o código hash para essa instância.</summary>
		/// <returns>Um inteiro com sinal de 32 bits que é o código hash para esta instância.</returns>
		// Token: 0x060014BA RID: 5306 RVA: 0x0004C220 File Offset: 0x0004B620
		public override int GetHashCode()
		{
			int num = this._x.GetHashCode() ^ this._y.GetHashCode() ^ this._pressureFactor.GetHashCode();
			if (this._stylusPointDescription != null)
			{
				num ^= this._stylusPointDescription.GetHashCode();
			}
			if (this._additionalValues != null)
			{
				for (int i = 0; i < this._additionalValues.Length; i++)
				{
					num ^= this._additionalValues[i];
				}
			}
			return num;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0004C290 File Offset: 0x0004B690
		internal int[] GetAdditionalData()
		{
			return this._additionalValues;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0004C2A4 File Offset: 0x0004B6A4
		internal float GetUntruncatedPressureFactor()
		{
			return this._pressureFactor;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004C2B8 File Offset: 0x0004B6B8
		internal int[] GetPacketData()
		{
			int num = 2;
			if (this._additionalValues != null)
			{
				num += this._additionalValues.Length;
			}
			if (this.Description.ContainsTruePressure)
			{
				num++;
			}
			int[] array = new int[num];
			array[0] = (int)this._x;
			array[1] = (int)this._y;
			int num2 = 2;
			if (this.Description.ContainsTruePressure)
			{
				num2 = 3;
				array[2] = this.GetPropertyValue(StylusPointProperties.NormalPressure);
			}
			if (this._additionalValues != null)
			{
				for (int i = 0; i < this._additionalValues.Length; i++)
				{
					array[i + num2] = this._additionalValues[i];
				}
			}
			return array;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0004C350 File Offset: 0x0004B750
		internal bool HasDefaultPressure
		{
			get
			{
				return this._pressureFactor == StylusPoint.DefaultPressure;
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004C36C File Offset: 0x0004B76C
		private void CopyAdditionalData()
		{
			if (this._additionalValues != null)
			{
				int[] array = new int[this._additionalValues.Length];
				for (int i = 0; i < this._additionalValues.Length; i++)
				{
					array[i] = this._additionalValues[i];
				}
				this._additionalValues = array;
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004C3B4 File Offset: 0x0004B7B4
		private static double GetClampedXYValue(double xyValue)
		{
			if (xyValue > StylusPoint.MaxXY)
			{
				return StylusPoint.MaxXY;
			}
			if (xyValue < StylusPoint.MinXY)
			{
				return StylusPoint.MinXY;
			}
			return xyValue;
		}

		// Token: 0x04000B05 RID: 2821
		internal static readonly float DefaultPressure = 0.5f;

		// Token: 0x04000B06 RID: 2822
		private double _x;

		// Token: 0x04000B07 RID: 2823
		private double _y;

		// Token: 0x04000B08 RID: 2824
		private float _pressureFactor;

		// Token: 0x04000B09 RID: 2825
		private int[] _additionalValues;

		// Token: 0x04000B0A RID: 2826
		private StylusPointDescription _stylusPointDescription;

		/// <summary>Especifica o maior valor válido para um par de coordenadas (x, y).</summary>
		// Token: 0x04000B0B RID: 2827
		public static readonly double MaxXY = 81164736.2834643;

		/// <summary>Especifica o menor valor válido para um par de coordenadas (x, y).</summary>
		// Token: 0x04000B0C RID: 2828
		public static readonly double MinXY = -81164736.3212596;
	}
}
