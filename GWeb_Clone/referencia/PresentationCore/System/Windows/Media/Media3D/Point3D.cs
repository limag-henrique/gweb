using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um ponto da coordenada x, y e z em 3D no espaço.</summary>
	// Token: 0x0200046D RID: 1133
	[ValueSerializer(typeof(Point3DValueSerializer))]
	[TypeConverter(typeof(Point3DConverter))]
	[Serializable]
	public struct Point3D : IFormattable
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="x">O valor <see cref="P:System.Windows.Media.Media3D.Point3D.X" /> da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</param>
		/// <param name="y">O valor <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</param>
		/// <param name="z">O valor <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</param>
		// Token: 0x06002FD0 RID: 12240 RVA: 0x000BF7B0 File Offset: 0x000BEBB0
		public Point3D(double x, double y, double z)
		{
			this._x = x;
			this._y = y;
			this._z = z;
		}

		/// <summary>Move a estrutura de <see cref="T:System.Windows.Media.Media3D.Point3D" /> de acordo com as quantidades especificadas.</summary>
		/// <param name="offsetX">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point3D.X" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point3D" /> deverá ser alterada.</param>
		/// <param name="offsetY">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point3D" /> deverá ser alterada.</param>
		/// <param name="offsetZ">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point3D" /> deverá ser alterada.</param>
		// Token: 0x06002FD1 RID: 12241 RVA: 0x000BF7D4 File Offset: 0x000BEBD4
		public void Offset(double offsetX, double offsetY, double offsetZ)
		{
			this._x += offsetX;
			this._y += offsetY;
			this._z += offsetZ;
		}

		/// <summary>Adiciona uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point">O ponto a ser adicionado.</param>
		/// <param name="vector">O vetor a ser adicionado.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> que é a soma de <paramref name="point" /> e <paramref name="vector" />.</returns>
		// Token: 0x06002FD2 RID: 12242 RVA: 0x000BF80C File Offset: 0x000BEC0C
		public static Point3D operator +(Point3D point, Vector3D vector)
		{
			return new Point3D(point._x + vector._x, point._y + vector._y, point._z + vector._z);
		}

		/// <summary>Adiciona uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser adicionada.</param>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser adicionada.</param>
		/// <returns>A soma de <paramref name="point" /> e <paramref name="vector" />.</returns>
		// Token: 0x06002FD3 RID: 12243 RVA: 0x000BF848 File Offset: 0x000BEC48
		public static Point3D Add(Point3D point, Vector3D vector)
		{
			return new Point3D(point._x + vector._x, point._y + vector._y, point._z + vector._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> do qual o vetor deve ser subtraído.</param>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser subtraída do ponto.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> alterada, o resultado da subtração de <paramref name="vector" /> de <paramref name="point" />.</returns>
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000BF884 File Offset: 0x000BEC84
		public static Point3D operator -(Point3D point, Vector3D vector)
		{
			return new Point3D(point._x - vector._x, point._y - vector._y, point._z - vector._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> da qual subtrair <paramref name="vector" />.</param>
		/// <param name="vector">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> para subtrair de <paramref name="point" />.</param>
		/// <returns>A diferença entre <paramref name="point" /> e <paramref name="vector" />.</returns>
		// Token: 0x06002FD5 RID: 12245 RVA: 0x000BF8C0 File Offset: 0x000BECC0
		public static Point3D Subtract(Point3D point, Vector3D vector)
		{
			return new Point3D(point._x - vector._x, point._y - vector._y, point._z - vector._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="point1">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> na qual a subtração será executada.</param>
		/// <param name="point2">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para subtrair de <paramref name="point1" />.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que representa a diferença entre <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06002FD6 RID: 12246 RVA: 0x000BF8FC File Offset: 0x000BECFC
		public static Vector3D operator -(Point3D point1, Point3D point2)
		{
			return new Vector3D(point1._x - point2._x, point1._y - point2._y, point1._z - point2._z);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="point1">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> da qual subtrair.</param>
		/// <param name="point2">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para subtrair de <paramref name="point1" />.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que representa a diferença entre <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000BF938 File Offset: 0x000BED38
		public static Vector3D Subtract(Point3D point1, Point3D point2)
		{
			Vector3D result = default(Vector3D);
			Point3D.Subtract(ref point1, ref point2, out result);
			return result;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000BF95C File Offset: 0x000BED5C
		internal static void Subtract(ref Point3D p1, ref Point3D p2, out Vector3D result)
		{
			result._x = p1._x - p2._x;
			result._y = p1._y - p2._y;
			result._z = p1._z - p2._z;
		}

		/// <summary>Transforma a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada pela estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="point">O ponto a ser transformado.</param>
		/// <param name="matrix">A matriz que é usada para transformar o <paramref name="point" />.</param>
		/// <returns>O resultado da transformação do <paramref name="point" /> usando <paramref name="matrix" />.</returns>
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000BF9A4 File Offset: 0x000BEDA4
		public static Point3D operator *(Point3D point, Matrix3D matrix)
		{
			return matrix.Transform(point);
		}

		/// <summary>Transforma a estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificada pela estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser transformada.</param>
		/// <param name="matrix">A estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> a ser usada para a transformação.</param>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> transformada, o resultado da transformação de <paramref name="point" /> por <paramref name="matrix" />.</returns>
		// Token: 0x06002FDA RID: 12250 RVA: 0x000BF9BC File Offset: 0x000BEDBC
		public static Point3D Multiply(Point3D point, Matrix3D matrix)
		{
			return matrix.Transform(point);
		}

		/// <summary>Converte uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> em uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="point">O ponto para converter.</param>
		/// <returns>O resultado da conversão de <paramref name="point" />.</returns>
		// Token: 0x06002FDB RID: 12251 RVA: 0x000BF9D4 File Offset: 0x000BEDD4
		public static explicit operator Vector3D(Point3D point)
		{
			return new Vector3D(point._x, point._y, point._z);
		}

		/// <summary>Converte uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point">O ponto para converter.</param>
		/// <returns>O resultado da conversão de <paramref name="point" />.</returns>
		// Token: 0x06002FDC RID: 12252 RVA: 0x000BF9F8 File Offset: 0x000BEDF8
		public static explicit operator Point4D(Point3D point)
		{
			return new Point4D(point._x, point._y, point._z, 1.0);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as coordenadas <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> do <paramref name="point1" /> e do <paramref name="point2" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FDD RID: 12253 RVA: 0x000BFA28 File Offset: 0x000BEE28
		public static bool operator ==(Point3D point1, Point3D point2)
		{
			return point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z;
		}

		/// <summary>Compara a desigualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as coordenadas <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> do <paramref name="point1" /> e do <paramref name="point2" /> forem diferentes, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FDE RID: 12254 RVA: 0x000BFA68 File Offset: 0x000BEE68
		public static bool operator !=(Point3D point1, Point3D point2)
		{
			return !(point1 == point2);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os valores de <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> para <paramref name="point1" /> e <paramref name="point2" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FDF RID: 12255 RVA: 0x000BFA80 File Offset: 0x000BEE80
		public static bool Equals(Point3D point1, Point3D point2)
		{
			return point1.X.Equals(point2.X) && point1.Y.Equals(point2.Y) && point1.Z.Equals(point2.Z);
		}

		/// <summary>Determina se o objeto especificado é uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e, se sim, determina se as propriedades <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> do <see cref="T:System.Object" /> especificado são iguais às propriedades <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="o">O objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais; caso contrário, <see langword="false" />.  
		///  <see langword="true" /> se <paramref name="o" /> for uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> e se ele também for idêntico a esta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FE0 RID: 12256 RVA: 0x000BFAD8 File Offset: 0x000BEED8
		public override bool Equals(object o)
		{
			if (o == null || !(o is Point3D))
			{
				return false;
			}
			Point3D point = (Point3D)o;
			return Point3D.Equals(this, point);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="value">A instância <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser comparada a essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FE1 RID: 12257 RVA: 0x000BFB08 File Offset: 0x000BEF08
		public bool Equals(Point3D value)
		{
			return Point3D.Equals(this, value);
		}

		/// <summary>Retorna o código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <returns>Um código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
		// Token: 0x06002FE2 RID: 12258 RVA: 0x000BFB24 File Offset: 0x000BEF24
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		/// <summary>Converte uma representação de <see cref="T:System.String" /> de um ponto 3D na estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> equivalente.</summary>
		/// <param name="source">A representação de <see cref="T:System.String" /> do ponto 3D.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> equivalente.</returns>
		// Token: 0x06002FE3 RID: 12259 RVA: 0x000BFB60 File Offset: 0x000BEF60
		public static Point3D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string value = tokenizerHelper.NextTokenRequired();
			Point3D result = new Point3D(Convert.ToDouble(value, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			tokenizerHelper.LastTokenRequired();
			return result;
		}

		/// <summary>Obtém ou define a coordenada x dessa estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <returns>A coordenada x deste <see cref="T:System.Windows.Media.Media3D.Point3D" /> estrutura.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000BFBB0 File Offset: 0x000BEFB0
		// (set) Token: 0x06002FE5 RID: 12261 RVA: 0x000BFBC4 File Offset: 0x000BEFC4
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

		/// <summary>Obtém ou define a coordenada y dessa estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <returns>A coordenada y deste <see cref="T:System.Windows.Media.Media3D.Point3D" /> estrutura.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000BFBD8 File Offset: 0x000BEFD8
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000BFBEC File Offset: 0x000BEFEC
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

		/// <summary>Obtém ou define a coordenada z desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <returns>A coordenada z desta <see cref="T:System.Windows.Media.Media3D.Point3D" /> estrutura.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000BFC00 File Offset: 0x000BF000
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x000BFC14 File Offset: 0x000BF014
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

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <returns>Um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
		// Token: 0x06002FEA RID: 12266 RVA: 0x000BFC28 File Offset: 0x000BF028
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="provider">As informações de formatação específicas da cultura.</param>
		/// <returns>Um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Point3D.X" />, <see cref="P:System.Windows.Media.Media3D.Point3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
		// Token: 0x06002FEB RID: 12267 RVA: 0x000BFC40 File Offset: 0x000BF040
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
		// Token: 0x06002FEC RID: 12268 RVA: 0x000BFC58 File Offset: 0x000BF058
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000BFC70 File Offset: 0x000BF070
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

		// Token: 0x0400153F RID: 5439
		internal double _x;

		// Token: 0x04001540 RID: 5440
		internal double _y;

		// Token: 0x04001541 RID: 5441
		internal double _z;
	}
}
