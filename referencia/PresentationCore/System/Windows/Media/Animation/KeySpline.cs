using System;
using System.ComponentModel;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Essa classe é usada por um quadro chave de spline para definir o andamento da animação.</summary>
	// Token: 0x02000571 RID: 1393
	[TypeConverter(typeof(KeySplineConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public class KeySpline : Freezable, IFormattable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		// Token: 0x06004082 RID: 16514 RVA: 0x000FCF64 File Offset: 0x000FC364
		public KeySpline()
		{
			this._controlPoint1 = new Point(0.0, 0.0);
			this._controlPoint2 = new Point(1.0, 1.0);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.KeySpline" /> com as coordenadas especificadas para os pontos de controle.</summary>
		/// <param name="x1">A coordenada x para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint1" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <param name="y1">A coordenada y para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint1" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <param name="x2">A coordenada x para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint2" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <param name="y2">A coordenada y para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint2" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		// Token: 0x06004083 RID: 16515 RVA: 0x000FCFB4 File Offset: 0x000FC3B4
		public KeySpline(double x1, double y1, double x2, double y2) : this(new Point(x1, y1), new Point(x2, y2))
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.KeySpline" /> com os pontos de controle especificados.</summary>
		/// <param name="controlPoint1">O ponto de controle para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint1" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <param name="controlPoint2">O ponto de controle para o <see cref="P:System.Windows.Media.Animation.KeySpline.ControlPoint2" /> do <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		// Token: 0x06004084 RID: 16516 RVA: 0x000FCFD8 File Offset: 0x000FC3D8
		public KeySpline(Point controlPoint1, Point controlPoint2)
		{
			if (!this.IsValidControlPoint(controlPoint1))
			{
				throw new ArgumentException(SR.Get("Animation_KeySpline_InvalidValue", new object[]
				{
					"controlPoint1",
					controlPoint1
				}));
			}
			if (!this.IsValidControlPoint(controlPoint2))
			{
				throw new ArgumentException(SR.Get("Animation_KeySpline_InvalidValue", new object[]
				{
					"controlPoint2",
					controlPoint2
				}));
			}
			this._controlPoint1 = controlPoint1;
			this._controlPoint2 = controlPoint2;
			this._isDirty = true;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.KeySpline" />.</returns>
		// Token: 0x06004085 RID: 16517 RVA: 0x000FD060 File Offset: 0x000FC460
		protected override Freezable CreateInstanceCore()
		{
			return new KeySpline();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.KeySpline" /> a ser clonado.</param>
		// Token: 0x06004086 RID: 16518 RVA: 0x000FD074 File Offset: 0x000FC474
		protected override void CloneCore(Freezable sourceFreezable)
		{
			KeySpline sourceKeySpline = (KeySpline)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CloneCommon(sourceKeySpline);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.KeySpline" /> a ser clonado.</param>
		// Token: 0x06004087 RID: 16519 RVA: 0x000FD098 File Offset: 0x000FC498
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			KeySpline sourceKeySpline = (KeySpline)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CloneCommon(sourceKeySpline);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.KeySpline" /> a ser clonado.</param>
		// Token: 0x06004088 RID: 16520 RVA: 0x000FD0BC File Offset: 0x000FC4BC
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			KeySpline sourceKeySpline = (KeySpline)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CloneCommon(sourceKeySpline);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.KeySpline" /> a ser copiado e congelado.</param>
		// Token: 0x06004089 RID: 16521 RVA: 0x000FD0E0 File Offset: 0x000FC4E0
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			KeySpline sourceKeySpline = (KeySpline)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CloneCommon(sourceKeySpline);
		}

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.KeySpline" /> atual é modificado.</summary>
		// Token: 0x0600408A RID: 16522 RVA: 0x000FD104 File Offset: 0x000FC504
		protected override void OnChanged()
		{
			this._isDirty = true;
			base.OnChanged();
		}

		/// <summary>O primeiro ponto de controle usado para definir uma curva de Bézier que descreve um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		/// <returns>A curva de Bézier primeiro ponto de controle. O ponto <see cref="P:System.Windows.Point.X" /> e <see cref="P:System.Windows.Point.Y" /> valores devem ser entre 0 e 1, inclusive. O valor padrão é (0,0).</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000FD120 File Offset: 0x000FC520
		// (set) Token: 0x0600408C RID: 16524 RVA: 0x000FD13C File Offset: 0x000FC53C
		public Point ControlPoint1
		{
			get
			{
				base.ReadPreamble();
				return this._controlPoint1;
			}
			set
			{
				base.WritePreamble();
				if (value != this._controlPoint1)
				{
					if (!this.IsValidControlPoint(value))
					{
						throw new ArgumentException(SR.Get("Animation_KeySpline_InvalidValue", new object[]
						{
							"ControlPoint1",
							value
						}));
					}
					this._controlPoint1 = value;
					base.WritePostscript();
				}
			}
		}

		/// <summary>O segundo ponto de controle usado para definir uma curva de Bézier que descreve um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		/// <returns>A curva de Bézier segundo ponto de controle. O ponto <see cref="P:System.Windows.Point.X" /> e <see cref="P:System.Windows.Point.Y" /> valores devem ser entre 0 e 1, inclusive. O valor padrão é (1,1).</returns>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000FD19C File Offset: 0x000FC59C
		// (set) Token: 0x0600408E RID: 16526 RVA: 0x000FD1B8 File Offset: 0x000FC5B8
		public Point ControlPoint2
		{
			get
			{
				base.ReadPreamble();
				return this._controlPoint2;
			}
			set
			{
				base.WritePreamble();
				if (value != this._controlPoint2)
				{
					if (!this.IsValidControlPoint(value))
					{
						throw new ArgumentException(SR.Get("Animation_KeySpline_InvalidValue", new object[]
						{
							"ControlPoint2",
							value
						}));
					}
					this._controlPoint2 = value;
					base.WritePostscript();
				}
			}
		}

		/// <summary>Calcula o progresso de spline de um progresso linear fornecido.</summary>
		/// <param name="linearProgress">O progresso linear a avaliar.</param>
		/// <returns>O progresso de spline calculado.</returns>
		// Token: 0x0600408F RID: 16527 RVA: 0x000FD218 File Offset: 0x000FC618
		public double GetSplineProgress(double linearProgress)
		{
			base.ReadPreamble();
			if (this._isDirty)
			{
				this.Build();
			}
			if (!this._isSpecified)
			{
				return linearProgress;
			}
			this.SetParameterFromX(linearProgress);
			return KeySpline.GetBezierValue(this._By, this._Cy, this._parameter);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x000FD264 File Offset: 0x000FC664
		private bool IsValidControlPoint(Point point)
		{
			return point.X >= 0.0 && point.X <= 1.0;
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000FD29C File Offset: 0x000FC69C
		private void Build()
		{
			if (this._controlPoint1 == new Point(0.0, 0.0) && this._controlPoint2 == new Point(1.0, 1.0))
			{
				this._isSpecified = false;
			}
			else
			{
				this._isSpecified = true;
				this._parameter = 0.0;
				this._Bx = 3.0 * this._controlPoint1.X;
				this._Cx = 3.0 * this._controlPoint2.X;
				this._Cx_Bx = 2.0 * (this._Cx - this._Bx);
				this._three_Cx = 3.0 - this._Cx;
				this._By = 3.0 * this._controlPoint1.Y;
				this._Cy = 3.0 * this._controlPoint2.Y;
			}
			this._isDirty = false;
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x000FD3BC File Offset: 0x000FC7BC
		private static double GetBezierValue(double b, double c, double t)
		{
			double num = 1.0 - t;
			double num2 = t * t;
			return b * t * num * num + c * num2 * num + num2 * t;
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000FD3EC File Offset: 0x000FC7EC
		private void GetXAndDx(double t, out double x, out double dx)
		{
			double num = 1.0 - t;
			double num2 = t * t;
			double num3 = num * num;
			x = this._Bx * t * num3 + this._Cx * num2 * num + num2 * t;
			dx = this._Bx * num3 + this._Cx_Bx * num * t + this._three_Cx * num2;
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000FD448 File Offset: 0x000FC848
		private void SetParameterFromX(double time)
		{
			double num = 0.0;
			double num2 = 1.0;
			if (time == 0.0)
			{
				this._parameter = 0.0;
				return;
			}
			if (time == 1.0)
			{
				this._parameter = 1.0;
				return;
			}
			while (num2 - num > 1E-06)
			{
				double num3;
				double num4;
				this.GetXAndDx(this._parameter, out num3, out num4);
				double num5 = Math.Abs(num4);
				if (num3 > time)
				{
					num2 = this._parameter;
				}
				else
				{
					num = this._parameter;
				}
				if (Math.Abs(num3 - time) < 0.001 * num5)
				{
					break;
				}
				if (num5 > 1E-06)
				{
					double num6 = this._parameter - (num3 - time) / num4;
					if (num6 >= num2)
					{
						this._parameter = (this._parameter + num2) / 2.0;
					}
					else if (num6 <= num)
					{
						this._parameter = (this._parameter + num) / 2.0;
					}
					else
					{
						this._parameter = num6;
					}
				}
				else
				{
					this._parameter = (num + num2) / 2.0;
				}
			}
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000FD570 File Offset: 0x000FC970
		private void CloneCommon(KeySpline sourceKeySpline)
		{
			this._controlPoint1 = sourceKeySpline._controlPoint1;
			this._controlPoint2 = sourceKeySpline._controlPoint2;
			this._isDirty = true;
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa instância de <see cref="T:System.Windows.Media.Animation.KeySpline" /> com base na cultura atual.</summary>
		/// <returns>Uma representação da cadeia de caracteres desse <see cref="T:System.Windows.Media.Animation.KeySpline" />.</returns>
		// Token: 0x06004096 RID: 16534 RVA: 0x000FD59C File Offset: 0x000FC99C
		public override string ToString()
		{
			base.ReadPreamble();
			return this.InternalConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres deste <see cref="T:System.Windows.Media.Animation.KeySpline" /> com base no <see cref="T:System.IFormatProvider" /> fornecido.</summary>
		/// <param name="formatProvider">O provedor de formato a ser usado. Se o provedor for <see langword="null" />, a cultura atual será usada.</param>
		/// <returns>Uma representação de cadeia de caracteres dessa instância de <see cref="T:System.Windows.Media.Animation.KeySpline" />.</returns>
		// Token: 0x06004097 RID: 16535 RVA: 0x000FD5B8 File Offset: 0x000FC9B8
		public string ToString(IFormatProvider formatProvider)
		{
			base.ReadPreamble();
			return this.InternalConvertToString(null, formatProvider);
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
		// Token: 0x06004098 RID: 16536 RVA: 0x000FD5D4 File Offset: 0x000FC9D4
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			base.ReadPreamble();
			return this.InternalConvertToString(format, formatProvider);
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000FD5F0 File Offset: 0x000FC9F0
		internal string InternalConvertToString(string format, IFormatProvider formatProvider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(formatProvider);
			return string.Format(formatProvider, "{1}{0}{2}", new object[]
			{
				numericListSeparator,
				this._controlPoint1,
				this._controlPoint2
			});
		}

		// Token: 0x04001794 RID: 6036
		private Point _controlPoint1;

		// Token: 0x04001795 RID: 6037
		private Point _controlPoint2;

		// Token: 0x04001796 RID: 6038
		private bool _isSpecified;

		// Token: 0x04001797 RID: 6039
		private bool _isDirty;

		// Token: 0x04001798 RID: 6040
		private double _parameter;

		// Token: 0x04001799 RID: 6041
		private double _Bx;

		// Token: 0x0400179A RID: 6042
		private double _Cx;

		// Token: 0x0400179B RID: 6043
		private double _Cx_Bx;

		// Token: 0x0400179C RID: 6044
		private double _three_Cx;

		// Token: 0x0400179D RID: 6045
		private double _By;

		// Token: 0x0400179E RID: 6046
		private double _Cy;

		// Token: 0x0400179F RID: 6047
		private const double accuracy = 0.001;

		// Token: 0x040017A0 RID: 6048
		private const double fuzz = 1E-06;
	}
}
