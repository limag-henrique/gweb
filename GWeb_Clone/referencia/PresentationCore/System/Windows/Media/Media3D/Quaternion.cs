using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Estrutura que representa uma rotação em três dimensões.</summary>
	// Token: 0x02000476 RID: 1142
	[TypeConverter(typeof(QuaternionConverter))]
	[ValueSerializer(typeof(QuaternionValueSerializer))]
	[Serializable]
	public struct Quaternion : IFormattable
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
		/// <param name="x">Valor da coordenada X do novo <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</param>
		/// <param name="y">Valor da coordenada Y do novo <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</param>
		/// <param name="z">Valor da coordenada Z do novo <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</param>
		/// <param name="w">Valor da coordenada W do novo <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</param>
		// Token: 0x060030C3 RID: 12483 RVA: 0x000C2DA0 File Offset: 0x000C21A0
		public Quaternion(double x, double y, double z, double w)
		{
			this._x = x;
			this._y = y;
			this._z = z;
			this._w = w;
			this._isNotDistinguishedIdentity = true;
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
		/// <param name="axisOfRotation">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que representa o eixo de rotação.</param>
		/// <param name="angleInDegrees">Ângulo a ser girado em torno do eixo especificado, em graus.</param>
		// Token: 0x060030C4 RID: 12484 RVA: 0x000C2DD4 File Offset: 0x000C21D4
		public Quaternion(Vector3D axisOfRotation, double angleInDegrees)
		{
			angleInDegrees %= 360.0;
			double num = angleInDegrees * 0.017453292519943295;
			double length = axisOfRotation.Length;
			if (length == 0.0)
			{
				throw new InvalidOperationException(SR.Get("Quaternion_ZeroAxisSpecified"));
			}
			Vector3D vector3D = axisOfRotation / length * Math.Sin(0.5 * num);
			this._x = vector3D.X;
			this._y = vector3D.Y;
			this._z = vector3D.Z;
			this._w = Math.Cos(0.5 * num);
			this._isNotDistinguishedIdentity = true;
		}

		/// <summary>Obtém o quatérnion de Identidade</summary>
		/// <returns>O quatérnio de identidade.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x000C2E80 File Offset: 0x000C2280
		public static Quaternion Identity
		{
			get
			{
				return Quaternion.s_identity;
			}
		}

		/// <summary>Obtém o eixo do quatérnion.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que representa o eixo do quatérnion.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000C2E94 File Offset: 0x000C2294
		public Vector3D Axis
		{
			get
			{
				if (this.IsDistinguishedIdentity || (this._x == 0.0 && this._y == 0.0 && this._z == 0.0))
				{
					return new Vector3D(0.0, 1.0, 0.0);
				}
				Vector3D result = new Vector3D(this._x, this._y, this._z);
				result.Normalize();
				return result;
			}
		}

		/// <summary>Obtém o ângulo do quatérnion, em graus.</summary>
		/// <returns>Duplo que representa o ângulo do quatérnion, em graus.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000C2F20 File Offset: 0x000C2320
		public double Angle
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 0.0;
				}
				double num = Math.Sqrt(this._x * this._x + this._y * this._y + this._z * this._z);
				double x = this._w;
				if (num > 1.7976931348623157E+308)
				{
					double num2 = Math.Max(Math.Abs(this._x), Math.Max(Math.Abs(this._y), Math.Abs(this._z)));
					double num3 = this._x / num2;
					double num4 = this._y / num2;
					double num5 = this._z / num2;
					num = Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5);
					x = this._w / num2;
				}
				return Math.Atan2(num, x) * 114.59155902616465;
			}
		}

		/// <summary>Obtém um valor que indica se o quatérnion é normalizado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o quatérnion é normalizado, <see langword="false" /> caso contrário.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000C2FFC File Offset: 0x000C23FC
		public bool IsNormalized
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return true;
				}
				double value = this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
				return DoubleUtil.IsOne(value);
			}
		}

		/// <summary>Obtém um valor que indica se o quatérnion especificado é um quatérnion <see cref="P:System.Windows.Media.Media3D.Quaternion.Identity" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se o quatérnion é o <see cref="P:System.Windows.Media.Media3D.Quaternion.Identity" /> quaternion, <see langword="false" /> caso contrário.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000C3054 File Offset: 0x000C2454
		public bool IsIdentity
		{
			get
			{
				return this.IsDistinguishedIdentity || (this._x == 0.0 && this._y == 0.0 && this._z == 0.0 && this._w == 1.0);
			}
		}

		/// <summary>Substitui um quatérnion pelo seu conjugado.</summary>
		// Token: 0x060030CA RID: 12490 RVA: 0x000C30B4 File Offset: 0x000C24B4
		public void Conjugate()
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			this._x = -this._x;
			this._y = -this._y;
			this._z = -this._z;
		}

		/// <summary>Substitui o quatérnion especificado pelo seu inverso</summary>
		// Token: 0x060030CB RID: 12491 RVA: 0x000C30F4 File Offset: 0x000C24F4
		public void Invert()
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			this.Conjugate();
			double num = this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
			this._x /= num;
			this._y /= num;
			this._z /= num;
			this._w /= num;
		}

		/// <summary>Retorna um quatérnion normalizado.</summary>
		// Token: 0x060030CC RID: 12492 RVA: 0x000C3180 File Offset: 0x000C2580
		public void Normalize()
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			double num = this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
			if (num > 1.7976931348623157E+308)
			{
				double num2 = 1.0 / Quaternion.Max(Math.Abs(this._x), Math.Abs(this._y), Math.Abs(this._z), Math.Abs(this._w));
				this._x *= num2;
				this._y *= num2;
				this._z *= num2;
				this._w *= num2;
				num = this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
			}
			double num3 = 1.0 / Math.Sqrt(num);
			this._x *= num3;
			this._y *= num3;
			this._z *= num3;
			this._w *= num3;
		}

		/// <summary>Adiciona os valores de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificados.</summary>
		/// <param name="left">O primeiro quatérnion a adicionar.</param>
		/// <param name="right">Segundo quatérnion a adicionar.</param>
		/// <returns>Quatérnion que é a soma dos dois valores <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificados.</returns>
		// Token: 0x060030CD RID: 12493 RVA: 0x000C32D4 File Offset: 0x000C26D4
		public static Quaternion operator +(Quaternion left, Quaternion right)
		{
			if (right.IsDistinguishedIdentity)
			{
				if (left.IsDistinguishedIdentity)
				{
					return new Quaternion(0.0, 0.0, 0.0, 2.0);
				}
				left._w += 1.0;
				return left;
			}
			else
			{
				if (left.IsDistinguishedIdentity)
				{
					right._w += 1.0;
					return right;
				}
				return new Quaternion(left._x + right._x, left._y + right._y, left._z + right._z, left._w + right._w);
			}
		}

		/// <summary>Adiciona os quatérnions especificados.</summary>
		/// <param name="left">O primeiro quatérnion a adicionar.</param>
		/// <param name="right">Segundo quatérnion a adicionar.</param>
		/// <returns>Quatérnion que é o resultado da adição.</returns>
		// Token: 0x060030CE RID: 12494 RVA: 0x000C338C File Offset: 0x000C278C
		public static Quaternion Add(Quaternion left, Quaternion right)
		{
			return left + right;
		}

		/// <summary>Subtrai um quatérnion especificado de outro.</summary>
		/// <param name="left">Quatérnion do qual subtrair.</param>
		/// <param name="right">Quatérnion a subtrair do primeiro quatérnion.</param>
		/// <returns>Quatérnion que é o resultado da subtração.</returns>
		// Token: 0x060030CF RID: 12495 RVA: 0x000C33A0 File Offset: 0x000C27A0
		public static Quaternion operator -(Quaternion left, Quaternion right)
		{
			if (right.IsDistinguishedIdentity)
			{
				if (left.IsDistinguishedIdentity)
				{
					return new Quaternion(0.0, 0.0, 0.0, 0.0);
				}
				left._w -= 1.0;
				return left;
			}
			else
			{
				if (left.IsDistinguishedIdentity)
				{
					return new Quaternion(-right._x, -right._y, -right._z, 1.0 - right._w);
				}
				return new Quaternion(left._x - right._x, left._y - right._y, left._z - right._z, left._w - right._w);
			}
		}

		/// <summary>Subtrai um Quatérnion de outro.</summary>
		/// <param name="left">Quatérnion do qual subtrair.</param>
		/// <param name="right">Quatérnion a subtrair do primeiro quatérnion.</param>
		/// <returns>Quatérnion que é o resultado da subtração.</returns>
		// Token: 0x060030D0 RID: 12496 RVA: 0x000C346C File Offset: 0x000C286C
		public static Quaternion Subtract(Quaternion left, Quaternion right)
		{
			return left - right;
		}

		/// <summary>Multiplica o quatérnion especificado por outro.</summary>
		/// <param name="left">O primeiro quatérnion.</param>
		/// <param name="right">Segundo quatérnion.</param>
		/// <returns>Quatérnion que é o produto da multiplicação.</returns>
		// Token: 0x060030D1 RID: 12497 RVA: 0x000C3480 File Offset: 0x000C2880
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			if (left.IsDistinguishedIdentity)
			{
				return right;
			}
			if (right.IsDistinguishedIdentity)
			{
				return left;
			}
			double x = left._w * right._x + left._x * right._w + left._y * right._z - left._z * right._y;
			double y = left._w * right._y + left._y * right._w + left._z * right._x - left._x * right._z;
			double z = left._w * right._z + left._z * right._w + left._x * right._y - left._y * right._x;
			double w = left._w * right._w - left._x * right._x - left._y * right._y - left._z * right._z;
			Quaternion result = new Quaternion(x, y, z, w);
			return result;
		}

		/// <summary>Multiplica os valores de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificados.</summary>
		/// <param name="left">O primeiro quatérnion a multiplicar.</param>
		/// <param name="right">Segundo quatérnion a multiplicar.</param>
		/// <returns>Quatérnion que é o resultado da multiplicação.</returns>
		// Token: 0x060030D2 RID: 12498 RVA: 0x000C3594 File Offset: 0x000C2994
		public static Quaternion Multiply(Quaternion left, Quaternion right)
		{
			return left * right;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000C35A8 File Offset: 0x000C29A8
		private void Scale(double scale)
		{
			if (this.IsDistinguishedIdentity)
			{
				this._w = scale;
				this.IsDistinguishedIdentity = false;
				return;
			}
			this._x *= scale;
			this._y *= scale;
			this._z *= scale;
			this._w *= scale;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000C3604 File Offset: 0x000C2A04
		private double Length()
		{
			if (this.IsDistinguishedIdentity)
			{
				return 1.0;
			}
			double num = this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
			if (num > 1.7976931348623157E+308)
			{
				double num2 = Math.Max(Math.Max(Math.Abs(this._x), Math.Abs(this._y)), Math.Max(Math.Abs(this._z), Math.Abs(this._w)));
				double num3 = this._x / num2;
				double num4 = this._y / num2;
				double num5 = this._z / num2;
				double num6 = this._w / num2;
				double num7 = Math.Sqrt(num3 * num3 + num4 * num4 + num5 * num5 + num6 * num6);
				return num7 * num2;
			}
			return Math.Sqrt(num);
		}

		/// <summary>Interpola entre duas orientações usando interpolação linear esférica.</summary>
		/// <param name="from">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a orientação inicial.</param>
		/// <param name="to">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a orientação final.</param>
		/// <param name="t">Coeficiente de interpolação.</param>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" />, que representa a orientação resultante da interpolação.</returns>
		// Token: 0x060030D5 RID: 12501 RVA: 0x000C36F4 File Offset: 0x000C2AF4
		public static Quaternion Slerp(Quaternion from, Quaternion to, double t)
		{
			return Quaternion.Slerp(from, to, t, true);
		}

		/// <summary>Faz a interpolação entre orientações, representadas como estruturas <see cref="T:System.Windows.Media.Media3D.Quaternion" />, usando interpolação linear esférica.</summary>
		/// <param name="from">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a orientação inicial.</param>
		/// <param name="to">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a orientação final.</param>
		/// <param name="t">Coeficiente de interpolação.</param>
		/// <param name="useShortestPath">Booliano que indica se os quatérnions que constituem o arco mais curto possível em uma esfera de unidade quadridimensional devem ser computados.</param>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" />, que representa a orientação resultante da interpolação.</returns>
		// Token: 0x060030D6 RID: 12502 RVA: 0x000C370C File Offset: 0x000C2B0C
		public static Quaternion Slerp(Quaternion from, Quaternion to, double t, bool useShortestPath)
		{
			if (from.IsDistinguishedIdentity)
			{
				from._w = 1.0;
			}
			if (to.IsDistinguishedIdentity)
			{
				to._w = 1.0;
			}
			double num = from.Length();
			double num2 = to.Length();
			from.Scale(1.0 / num);
			to.Scale(1.0 / num2);
			double num3 = from._x * to._x + from._y * to._y + from._z * to._z + from._w * to._w;
			if (useShortestPath)
			{
				if (num3 < 0.0)
				{
					num3 = -num3;
					to._x = -to._x;
					to._y = -to._y;
					to._z = -to._z;
					to._w = -to._w;
				}
			}
			else if (num3 < -1.0)
			{
				num3 = -1.0;
			}
			if (num3 > 1.0)
			{
				num3 = 1.0;
			}
			double num4;
			double num5;
			if (num3 > 0.999999)
			{
				num4 = 1.0 - t;
				num5 = t;
			}
			else if (num3 < -0.9999999999)
			{
				to = new Quaternion(-from.Y, from.X, -from.W, from.Z);
				double num6 = t * 3.1415926535897931;
				num4 = Math.Cos(num6);
				num5 = Math.Sin(num6);
			}
			else
			{
				double num7 = Math.Acos(num3);
				double num8 = Math.Sqrt(1.0 - num3 * num3);
				num4 = Math.Sin((1.0 - t) * num7) / num8;
				num5 = Math.Sin(t * num7) / num8;
			}
			double num9 = num * Math.Pow(num2 / num, t);
			num4 *= num9;
			num5 *= num9;
			return new Quaternion(num4 * from._x + num5 * to._x, num4 * from._y + num5 * to._y, num4 * from._z + num5 * to._z, num4 * from._w + num5 * to._w);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000C3944 File Offset: 0x000C2D44
		private static double Max(double a, double b, double c, double d)
		{
			if (b > a)
			{
				a = b;
			}
			if (c > a)
			{
				a = c;
			}
			if (d > a)
			{
				a = d;
			}
			return a;
		}

		/// <summary>Obtém o componente X do quatérnion.</summary>
		/// <returns>O componente X do quatérnio.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000C3968 File Offset: 0x000C2D68
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x000C397C File Offset: 0x000C2D7C
		public double X
		{
			get
			{
				return this._x;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Quaternion.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._x = value;
			}
		}

		/// <summary>Obtém o componente Y do quatérnion.</summary>
		/// <returns>O componente Y do quatérnio.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000C39AC File Offset: 0x000C2DAC
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x000C39C0 File Offset: 0x000C2DC0
		public double Y
		{
			get
			{
				return this._y;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Quaternion.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._y = value;
			}
		}

		/// <summary>Obtém o componente Z do quatérnion.</summary>
		/// <returns>O componente Z do quatérnio.</returns>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000C39F0 File Offset: 0x000C2DF0
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x000C3A04 File Offset: 0x000C2E04
		public double Z
		{
			get
			{
				return this._z;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Quaternion.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._z = value;
			}
		}

		/// <summary>Obtém o componente W do quatérnion.</summary>
		/// <returns>O componente W do quatérnio.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x000C3A34 File Offset: 0x000C2E34
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x000C3A5C File Offset: 0x000C2E5C
		public double W
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				return this._w;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Quaternion.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._w = value;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000C3A8C File Offset: 0x000C2E8C
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x000C3AA4 File Offset: 0x000C2EA4
		private bool IsDistinguishedIdentity
		{
			get
			{
				return !this._isNotDistinguishedIdentity;
			}
			set
			{
				this._isNotDistinguishedIdentity = !value;
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000C3ABC File Offset: 0x000C2EBC
		private static int GetIdentityHashCode()
		{
			double num = 0.0;
			double num2 = 1.0;
			return num.GetHashCode() ^ num2.GetHashCode();
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000C3AEC File Offset: 0x000C2EEC
		private static Quaternion GetIdentity()
		{
			return new Quaternion(0.0, 0.0, 0.0, 1.0)
			{
				IsDistinguishedIdentity = true
			};
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> quanto à igualdade exata.</summary>
		/// <param name="quaternion1">O primeiro Quatérnion a ser comparado.</param>
		/// <param name="quaternion2">Segundo Quatérnion a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060030E4 RID: 12516 RVA: 0x000C3B30 File Offset: 0x000C2F30
		public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
		{
			if (quaternion1.IsDistinguishedIdentity || quaternion2.IsDistinguishedIdentity)
			{
				return quaternion1.IsIdentity == quaternion2.IsIdentity;
			}
			return quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z && quaternion1.W == quaternion2.W;
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> quanto à desigualdade exata.</summary>
		/// <param name="quaternion1">O primeiro quatérnion a ser comparado.</param>
		/// <param name="quaternion2">Segundo quatérnion a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060030E5 RID: 12517 RVA: 0x000C3BA4 File Offset: 0x000C2FA4
		public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
		{
			return !(quaternion1 == quaternion2);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> quanto à igualdade.</summary>
		/// <param name="quaternion1">Primeiro <see cref="T:System.Windows.Media.Media3D.Quaternion" /> a ser comparado.</param>
		/// <param name="quaternion2">Segundo <see cref="T:System.Windows.Media.Media3D.Quaternion" /> de comparação.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060030E6 RID: 12518 RVA: 0x000C3BBC File Offset: 0x000C2FBC
		public static bool Equals(Quaternion quaternion1, Quaternion quaternion2)
		{
			if (quaternion1.IsDistinguishedIdentity || quaternion2.IsDistinguishedIdentity)
			{
				return quaternion1.IsIdentity == quaternion2.IsIdentity;
			}
			return quaternion1.X.Equals(quaternion2.X) && quaternion1.Y.Equals(quaternion2.Y) && quaternion1.Z.Equals(quaternion2.Z) && quaternion1.W.Equals(quaternion2.W);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> quanto à igualdade.</summary>
		/// <param name="o">O objeto com o qual comparar.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060030E7 RID: 12519 RVA: 0x000C3C4C File Offset: 0x000C304C
		public override bool Equals(object o)
		{
			if (o == null || !(o is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)o;
			return Quaternion.Equals(this, quaternion);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> quanto à igualdade.</summary>
		/// <param name="value">Quatérnion ao qual comparar.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Quaternion" /> forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060030E8 RID: 12520 RVA: 0x000C3C7C File Offset: 0x000C307C
		public bool Equals(Quaternion value)
		{
			return Quaternion.Equals(this, value);
		}

		/// <summary>Retorna o código hash para o <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
		/// <returns>Um tipo de inteiro que representa o código hash para o <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</returns>
		// Token: 0x060030E9 RID: 12521 RVA: 0x000C3C98 File Offset: 0x000C3098
		public override int GetHashCode()
		{
			if (this.IsDistinguishedIdentity)
			{
				return Quaternion.c_identityHashCode;
			}
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}

		/// <summary>Converte uma representação de cadeia de caracteres de um <see cref="T:System.Windows.Media.Media3D.Quaternion" /> na estrutura <see cref="T:System.Windows.Media.Media3D.Quaternion" /> equivalente.</summary>
		/// <param name="source">Uma representação da cadeia de caracteres de <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</param>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> representado pela cadeia de caracteres.</returns>
		// Token: 0x060030EA RID: 12522 RVA: 0x000C3CF0 File Offset: 0x000C30F0
		public static Quaternion Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string text = tokenizerHelper.NextTokenRequired();
			Quaternion identity;
			if (text == "Identity")
			{
				identity = Quaternion.Identity;
			}
			else
			{
				identity = new Quaternion(Convert.ToDouble(text, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			}
			tokenizerHelper.LastTokenRequired();
			return identity;
		}

		/// <summary>Cria uma representação de cadeia de caracteres do objeto.</summary>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x060030EB RID: 12523 RVA: 0x000C3D60 File Offset: 0x000C3160
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres do objeto.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x060030EC RID: 12524 RVA: 0x000C3D78 File Offset: 0x000C3178
		public string ToString(IFormatProvider provider)
		{
			return this.ConvertToString(null, provider);
		}

		/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código. Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">A cadeia de caracteres que especifica o formato a ser usado.  
		///
		/// ou - 
		/// <see langword="null" /> para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O <see langword="IFormatProvider" /> a ser usado para formatar o valor.  
		///
		/// ou - 
		/// <see langword="null" /> para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>Uma cadeia de caracteres que contém o valor da instância atual no formato especificado.</returns>
		// Token: 0x060030ED RID: 12525 RVA: 0x000C3D90 File Offset: 0x000C3190
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000C3DA8 File Offset: 0x000C31A8
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.IsIdentity)
			{
				return "Identity";
			}
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"{1:",
				format,
				"}{0}{2:",
				format,
				"}{0}{3:",
				format,
				"}{0}{4:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this._x,
				this._y,
				this._z,
				this._w
			});
		}

		// Token: 0x04001567 RID: 5479
		internal double _x;

		// Token: 0x04001568 RID: 5480
		internal double _y;

		// Token: 0x04001569 RID: 5481
		internal double _z;

		// Token: 0x0400156A RID: 5482
		internal double _w;

		// Token: 0x0400156B RID: 5483
		private bool _isNotDistinguishedIdentity;

		// Token: 0x0400156C RID: 5484
		private static int c_identityHashCode = Quaternion.GetIdentityHashCode();

		// Token: 0x0400156D RID: 5485
		private static Quaternion s_identity = Quaternion.GetIdentity();
	}
}
