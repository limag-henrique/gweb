using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um deslocamento no espaço 3D.</summary>
	// Token: 0x02000484 RID: 1156
	[ValueSerializer(typeof(Vector3DValueSerializer))]
	[TypeConverter(typeof(Vector3DConverter))]
	[Serializable]
	public struct Vector3D : IFormattable
	{
		/// <summary>Inicializa uma nova instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="x">O novo valor <see cref="P:System.Windows.Media.Media3D.Vector3D.X" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		/// <param name="y">O novo valor <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		/// <param name="z">O novo valor <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		// Token: 0x06003218 RID: 12824 RVA: 0x000C7EF0 File Offset: 0x000C72F0
		public Vector3D(double x, double y, double z)
		{
			this._x = x;
			this._y = y;
			this._z = z;
		}

		/// <summary>Obtém o tamanho desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>O tamanho desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000C7F14 File Offset: 0x000C7314
		public double Length
		{
			get
			{
				return Math.Sqrt(this._x * this._x + this._y * this._y + this._z * this._z);
			}
		}

		/// <summary>Obtém o quadrado do comprimento dessa estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>O quadrado do comprimento desse <see cref="T:System.Windows.Media.Media3D.Vector3D" /> estrutura.</returns>
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000C7F50 File Offset: 0x000C7350
		public double LengthSquared
		{
			get
			{
				return this._x * this._x + this._y * this._y + this._z * this._z;
			}
		}

		/// <summary>Normaliza a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada.</summary>
		// Token: 0x0600321B RID: 12827 RVA: 0x000C7F88 File Offset: 0x000C7388
		public void Normalize()
		{
			double num = Math.Abs(this._x);
			double num2 = Math.Abs(this._y);
			double num3 = Math.Abs(this._z);
			if (num2 > num)
			{
				num = num2;
			}
			if (num3 > num)
			{
				num = num3;
			}
			this._x /= num;
			this._y /= num;
			this._z /= num;
			double scalar = Math.Sqrt(this._x * this._x + this._y * this._y + this._z * this._z);
			this /= scalar;
		}

		/// <summary>Recupera o ângulo necessário para girar a primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada na segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <returns>O ângulo em graus necessário para girar <paramref name="vector1" /> em <paramref name="vector2" />.</returns>
		// Token: 0x0600321C RID: 12828 RVA: 0x000C8030 File Offset: 0x000C7430
		public static double AngleBetween(Vector3D vector1, Vector3D vector2)
		{
			vector1.Normalize();
			vector2.Normalize();
			double num = Vector3D.DotProduct(vector1, vector2);
			double radians;
			if (num < 0.0)
			{
				radians = 3.1415926535897931 - 2.0 * Math.Asin((-vector1 - vector2).Length / 2.0);
			}
			else
			{
				radians = 2.0 * Math.Asin((vector1 - vector2).Length / 2.0);
			}
			return M3DUtil.RadiansToDegrees(radians);
		}

		/// <summary>Nega uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a negar.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> com valores de <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> opostos aos valores de <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> de <paramref name="vector" />.</returns>
		// Token: 0x0600321D RID: 12829 RVA: 0x000C80C8 File Offset: 0x000C74C8
		public static Vector3D operator -(Vector3D vector)
		{
			return new Vector3D(-vector._x, -vector._y, -vector._z);
		}

		/// <summary>Nega uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		// Token: 0x0600321E RID: 12830 RVA: 0x000C80F0 File Offset: 0x000C74F0
		public void Negate()
		{
			this._x = -this._x;
			this._y = -this._y;
			this._z = -this._z;
		}

		/// <summary>Adiciona duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" /> vetores e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser adicionada.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser adicionada.</param>
		/// <returns>A soma de <paramref name="vector1" /> e <paramref name="vector2" />.</returns>
		// Token: 0x0600321F RID: 12831 RVA: 0x000C8124 File Offset: 0x000C7524
		public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1._x + vector2._x, vector1._y + vector2._y, vector1._z + vector2._z);
		}

		/// <summary>Adiciona duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" /> vetores e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser adicionada.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser adicionada.</param>
		/// <returns>A soma de <paramref name="vector1" /> e <paramref name="vector2" />.</returns>
		// Token: 0x06003220 RID: 12832 RVA: 0x000C8160 File Offset: 0x000C7560
		public static Vector3D Add(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1._x + vector2._x, vector1._y + vector2._y, vector1._z + vector2._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> da qual subtrair.</param>
		/// <param name="vector2">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> para subtrair de <paramref name="vector1" />.</param>
		/// <returns>O resultado da subtração de <paramref name="vector2" /> de <paramref name="vector1" />.</returns>
		// Token: 0x06003221 RID: 12833 RVA: 0x000C819C File Offset: 0x000C759C
		public static Vector3D operator -(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1._x - vector2._x, vector1._y - vector2._y, vector1._z - vector2._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> da qual subtrair.</param>
		/// <param name="vector2">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> para subtrair de <paramref name="vector1" />.</param>
		/// <returns>O resultado da subtração de <paramref name="vector2" /> de <paramref name="vector1" />.</returns>
		// Token: 0x06003222 RID: 12834 RVA: 0x000C81D8 File Offset: 0x000C75D8
		public static Vector3D Subtract(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1._x - vector2._x, vector1._y - vector2._y, vector1._z - vector2._z);
		}

		/// <summary>Converte a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada segundo a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada e retorna o resultado como um estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> usada para converter a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada.</param>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser convertida.</param>
		/// <returns>O resultado de converter <paramref name="point" /> por <paramref name="vector" />.</returns>
		// Token: 0x06003223 RID: 12835 RVA: 0x000C8214 File Offset: 0x000C7614
		public static Point3D operator +(Vector3D vector, Point3D point)
		{
			return new Point3D(vector._x + point._x, vector._y + point._y, vector._z + point._z);
		}

		/// <summary>Converte a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada segundo a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada e retorna o resultado como um estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> usada para converter a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada.</param>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser convertida.</param>
		/// <returns>O resultado de converter <paramref name="point" /> por <paramref name="vector" />.</returns>
		// Token: 0x06003224 RID: 12836 RVA: 0x000C8250 File Offset: 0x000C7650
		public static Point3D Add(Vector3D vector, Point3D point)
		{
			return new Point3D(vector._x + point._x, vector._y + point._y, vector._z + point._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> da qual subtrair.</param>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para subtrair de <paramref name="vector" />.</param>
		/// <returns>O resultado da subtração de <paramref name="point" /> de <paramref name="vector" />.</returns>
		// Token: 0x06003225 RID: 12837 RVA: 0x000C828C File Offset: 0x000C768C
		public static Point3D operator -(Vector3D vector, Point3D point)
		{
			return new Point3D(vector._x - point._x, vector._y - point._y, vector._z - point._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> da qual subtrair.</param>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para subtrair de <paramref name="vector" />.</param>
		/// <returns>O resultado da subtração de <paramref name="point" /> de <paramref name="vector" />.</returns>
		// Token: 0x06003226 RID: 12838 RVA: 0x000C82C8 File Offset: 0x000C76C8
		public static Point3D Subtract(Vector3D vector, Point3D point)
		{
			return new Point3D(vector._x - point._x, vector._y - point._y, vector._z - point._z);
		}

		/// <summary>Multiplica a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada pelo escalar especificado e retorna o resultado como um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a multiplicar.</param>
		/// <param name="scalar">O escalar a se multiplicar.</param>
		/// <returns>O resultado da multiplicação de <paramref name="vector" /> por <paramref name="scalar" />.</returns>
		// Token: 0x06003227 RID: 12839 RVA: 0x000C8304 File Offset: 0x000C7704
		public static Vector3D operator *(Vector3D vector, double scalar)
		{
			return new Vector3D(vector._x * scalar, vector._y * scalar, vector._z * scalar);
		}

		/// <summary>Multiplica a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada pelo escalar especificado e retorna o resultado como um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a multiplicar.</param>
		/// <param name="scalar">O escalar a se multiplicar.</param>
		/// <returns>O resultado da multiplicação de <paramref name="vector" /> por <paramref name="scalar" />.</returns>
		// Token: 0x06003228 RID: 12840 RVA: 0x000C8330 File Offset: 0x000C7730
		public static Vector3D Multiply(Vector3D vector, double scalar)
		{
			return new Vector3D(vector._x * scalar, vector._y * scalar, vector._z * scalar);
		}

		/// <summary>Multiplica o escalar especificado pela estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada e retorna o resultado como <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="scalar">O escalar a se multiplicar.</param>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a multiplicar.</param>
		/// <returns>O resultado da multiplicação de <paramref name="scalar" /> por <paramref name="vector" />.</returns>
		// Token: 0x06003229 RID: 12841 RVA: 0x000C835C File Offset: 0x000C775C
		public static Vector3D operator *(double scalar, Vector3D vector)
		{
			return new Vector3D(vector._x * scalar, vector._y * scalar, vector._z * scalar);
		}

		/// <summary>Multiplica o escalar especificado pela estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada e retorna o resultado como <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="scalar">O escalar a se multiplicar.</param>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a multiplicar.</param>
		/// <returns>O resultado da multiplicação de <paramref name="scalar" /> por <paramref name="vector" />.</returns>
		// Token: 0x0600322A RID: 12842 RVA: 0x000C8388 File Offset: 0x000C7788
		public static Vector3D Multiply(double scalar, Vector3D vector)
		{
			return new Vector3D(vector._x * scalar, vector._y * scalar, vector._z * scalar);
		}

		/// <summary>Divide a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada pelo escalar especificado e retorna o resultado como um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser dividida.</param>
		/// <param name="scalar">O escalar pelo qual dividir <paramref name="vector" />.</param>
		/// <returns>O resultado da divisão de <paramref name="vector" /> por <paramref name="scalar" />.</returns>
		// Token: 0x0600322B RID: 12843 RVA: 0x000C83B4 File Offset: 0x000C77B4
		public static Vector3D operator /(Vector3D vector, double scalar)
		{
			return vector * (1.0 / scalar);
		}

		/// <summary>Divide a estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada pelo escalar especificado e retorna o resultado como um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser dividida.</param>
		/// <param name="scalar">O escalar pelo qual dividir <paramref name="vector" />.</param>
		/// <returns>O resultado da divisão de <paramref name="vector" /> por <paramref name="scalar" />.</returns>
		// Token: 0x0600322C RID: 12844 RVA: 0x000C83D4 File Offset: 0x000C77D4
		public static Vector3D Divide(Vector3D vector, double scalar)
		{
			return vector * (1.0 / scalar);
		}

		/// <summary>Transforma o espaço de coordenadas da estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada usando a estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser transformada.</param>
		/// <param name="matrix">A transformação a ser aplicada à estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		/// <returns>O resultado da transformar <paramref name="vector" /> em <paramref name="matrix" />.</returns>
		// Token: 0x0600322D RID: 12845 RVA: 0x000C83F4 File Offset: 0x000C77F4
		public static Vector3D operator *(Vector3D vector, Matrix3D matrix)
		{
			return matrix.Transform(vector);
		}

		/// <summary>Transforma o espaço de coordenadas da estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada usando a estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser transformada.</param>
		/// <param name="matrix">A transformação a ser aplicada à estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		/// <returns>Retorna o resultado da transformar <paramref name="vector" /> em <paramref name="matrix3D" />.</returns>
		// Token: 0x0600322E RID: 12846 RVA: 0x000C840C File Offset: 0x000C780C
		public static Vector3D Multiply(Vector3D vector, Matrix3D matrix)
		{
			return matrix.Transform(vector);
		}

		/// <summary>Calcula o produto escalar de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <returns>O produto escalar de <paramref name="vector1" /> e <paramref name="vector2" />.</returns>
		// Token: 0x0600322F RID: 12847 RVA: 0x000C8424 File Offset: 0x000C7824
		public static double DotProduct(Vector3D vector1, Vector3D vector2)
		{
			return Vector3D.DotProduct(ref vector1, ref vector2);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000C843C File Offset: 0x000C783C
		internal static double DotProduct(ref Vector3D vector1, ref Vector3D vector2)
		{
			return vector1._x * vector2._x + vector1._y * vector2._y + vector1._z * vector2._z;
		}

		/// <summary>Calcula o produto cruzado de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a se avaliar.</param>
		/// <returns>O produto cruzado de <paramref name="vector1" /> e <paramref name="vector2" />.</returns>
		// Token: 0x06003231 RID: 12849 RVA: 0x000C8474 File Offset: 0x000C7874
		public static Vector3D CrossProduct(Vector3D vector1, Vector3D vector2)
		{
			Vector3D result;
			Vector3D.CrossProduct(ref vector1, ref vector2, out result);
			return result;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000C8490 File Offset: 0x000C7890
		internal static void CrossProduct(ref Vector3D vector1, ref Vector3D vector2, out Vector3D result)
		{
			result._x = vector1._y * vector2._z - vector1._z * vector2._y;
			result._y = vector1._z * vector2._x - vector1._x * vector2._z;
			result._z = vector1._x * vector2._y - vector1._y * vector2._x;
		}

		/// <summary>Converte uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="vector">O vetor a ser convertido.</param>
		/// <returns>O resultado da conversão de <paramref name="vector" />.</returns>
		// Token: 0x06003233 RID: 12851 RVA: 0x000C8500 File Offset: 0x000C7900
		public static explicit operator Point3D(Vector3D vector)
		{
			return new Point3D(vector._x, vector._y, vector._z);
		}

		/// <summary>Converte um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> estrutura em um <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="vector">O vetor a ser convertido.</param>
		/// <returns>O resultado da conversão de <paramref name="vector" />.</returns>
		// Token: 0x06003234 RID: 12852 RVA: 0x000C8524 File Offset: 0x000C7924
		public static explicit operator Size3D(Vector3D vector)
		{
			return new Size3D(Math.Abs(vector._x), Math.Abs(vector._y), Math.Abs(vector._z));
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser comparada.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> de <paramref name="vector1" /> e <paramref name="vector2" /> forem iguais; <see langword="false" />, caso contrário.</returns>
		// Token: 0x06003235 RID: 12853 RVA: 0x000C8558 File Offset: 0x000C7958
		public static bool operator ==(Vector3D vector1, Vector3D vector2)
		{
			return vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
		}

		/// <summary>Compara a desigualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser comparada.</param>
		/// <param name="vector2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> de <paramref name="vector3D1" /> e de <paramref name="vector3D2" /> forem diferentes; <see langword="false" /> caso contrário.</returns>
		// Token: 0x06003236 RID: 12854 RVA: 0x000C8598 File Offset: 0x000C7998
		public static bool operator !=(Vector3D vector1, Vector3D vector2)
		{
			return !(vector1 == vector2);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="vector1">Primeiro <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser comparado.</param>
		/// <param name="vector2">Segundo <see cref="T:System.Windows.Media.Media3D.Vector3D" /> de comparação.</param>
		/// <returns>
		///   <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> de <paramref name="vector1" /> e <paramref name="vector2" /> forem iguais; <see langword="false" />, caso contrário.</returns>
		// Token: 0x06003237 RID: 12855 RVA: 0x000C85B0 File Offset: 0x000C79B0
		public static bool Equals(Vector3D vector1, Vector3D vector2)
		{
			return vector1.X.Equals(vector2.X) && vector1.Y.Equals(vector2.Y) && vector1.Z.Equals(vector2.Z);
		}

		/// <summary>Determina se o objeto especificado é uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e se as propriedades <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> do <see cref="T:System.Object" /> especificado são iguais às propriedades <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="o">O objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> for uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e idêntico à esta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003238 RID: 12856 RVA: 0x000C8608 File Offset: 0x000C7A08
		public override bool Equals(object o)
		{
			if (o == null || !(o is Vector3D))
			{
				return false;
			}
			Vector3D vector = (Vector3D)o;
			return Vector3D.Equals(this, vector);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="value">A instância do Vetor a comparar a esta instância.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003239 RID: 12857 RVA: 0x000C8638 File Offset: 0x000C7A38
		public bool Equals(Vector3D value)
		{
			return Vector3D.Equals(this, value);
		}

		/// <summary>Obtém o código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>Um código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x0600323A RID: 12858 RVA: 0x000C8654 File Offset: 0x000C7A54
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		/// <summary>Converte uma representação de <see cref="T:System.String" /> de um vetor 3D na estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> equivalente.</summary>
		/// <param name="source">A representação <see cref="T:System.String" /> do vetor 3D.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> equivalente.</returns>
		// Token: 0x0600323B RID: 12859 RVA: 0x000C8690 File Offset: 0x000C7A90
		public static Vector3D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string value = tokenizerHelper.NextTokenRequired();
			Vector3D result = new Vector3D(Convert.ToDouble(value, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			tokenizerHelper.LastTokenRequired();
			return result;
		}

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Vector3D.X" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Vector3D.X" /> componente isso <see cref="T:System.Windows.Media.Media3D.Vector3D" /> estrutura. O valor padrão é 0.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000C86E0 File Offset: 0x000C7AE0
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x000C86F4 File Offset: 0x000C7AF4
		public double X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> componente isso <see cref="T:System.Windows.Media.Media3D.Vector3D" /> estrutura. O valor padrão é 0.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000C8708 File Offset: 0x000C7B08
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x000C871C File Offset: 0x000C7B1C
		public double Y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> componente isso <see cref="T:System.Windows.Media.Media3D.Vector3D" /> estrutura. O valor padrão é 0.</returns>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000C8730 File Offset: 0x000C7B30
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x000C8744 File Offset: 0x000C7B44
		public double Z
		{
			get
			{
				return this._z;
			}
			set
			{
				this._z = value;
			}
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <returns>Uma cadeia de caracteres contendo os valores <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> dessa estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x06003242 RID: 12866 RVA: 0x000C8758 File Offset: 0x000C7B58
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Vector3D.X" />, <see cref="P:System.Windows.Media.Media3D.Vector3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Vector3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x06003243 RID: 12867 RVA: 0x000C8770 File Offset: 0x000C7B70
		public string ToString(IFormatProvider provider)
		{
			return this.ConvertToString(null, provider);
		}

		/// <summary>Esse membro faz parte da infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente pelo seu código. Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">A cadeia de caracteres que especifica o formato a ser usado.  
		///
		/// ou - 
		/// <see langword="null" /> para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O <see langword="IFormatProvider" /> a ser usado para formatar o valor.  
		///
		/// ou - 
		/// <see langword="null" /> para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>A representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06003244 RID: 12868 RVA: 0x000C8788 File Offset: 0x000C7B88
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000C87A0 File Offset: 0x000C7BA0
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"{1:",
				format,
				"}{0}{2:",
				format,
				"}{0}{3:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this._x,
				this._y,
				this._z
			});
		}

		// Token: 0x040015C6 RID: 5574
		internal double _x;

		// Token: 0x040015C7 RID: 5575
		internal double _y;

		// Token: 0x040015C8 RID: 5576
		internal double _z;
	}
}
