using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um ponto de coordenada x, y, z e w no espaço mundial usado na execução de transformações com matrizes 3D não afim.</summary>
	// Token: 0x02000470 RID: 1136
	[ValueSerializer(typeof(Point4DValueSerializer))]
	[TypeConverter(typeof(Point4DConverter))]
	[Serializable]
	public struct Point4D : IFormattable
	{
		/// <summary>Inicializa uma nova instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="x">A coordenada X da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</param>
		/// <param name="y">A coordenada y da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</param>
		/// <param name="z">A coordenada Z da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</param>
		/// <param name="w">A coordenada W da nova estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</param>
		// Token: 0x06003050 RID: 12368 RVA: 0x000C1084 File Offset: 0x000C0484
		public Point4D(double x, double y, double z, double w)
		{
			this._x = x;
			this._y = y;
			this._z = z;
			this._w = w;
		}

		/// <summary>Move a estrutura de <see cref="T:System.Windows.Media.Media3D.Point4D" /> de acordo com as quantidades especificadas.</summary>
		/// <param name="deltaX">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point4D.X" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point4D" /> deverá ser deslocada.</param>
		/// <param name="deltaY">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point4D.Y" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point4D" /> deverá ser deslocada.</param>
		/// <param name="deltaZ">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point4D" /> deverá ser deslocada.</param>
		/// <param name="deltaW">O quanto a coordenada <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> dessa estrutura de <see cref="T:System.Windows.Media.Media3D.Point4D" /> deverá ser deslocada.</param>
		// Token: 0x06003051 RID: 12369 RVA: 0x000C10B0 File Offset: 0x000C04B0
		public void Offset(double deltaX, double deltaY, double deltaZ, double deltaW)
		{
			this._x += deltaX;
			this._y += deltaY;
			this._z += deltaZ;
			this._w += deltaW;
		}

		/// <summary>Adiciona uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser adicionada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser adicionada.</param>
		/// <returns>Retorna a soma de <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06003052 RID: 12370 RVA: 0x000C10F8 File Offset: 0x000C04F8
		public static Point4D operator +(Point4D point1, Point4D point2)
		{
			return new Point4D(point1._x + point2._x, point1._y + point2._y, point1._z + point2._z, point1._w + point2._w);
		}

		/// <summary>Adiciona uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser adicionada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser adicionada.</param>
		/// <returns>Retorna a soma de <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06003053 RID: 12371 RVA: 0x000C1140 File Offset: 0x000C0540
		public static Point4D Add(Point4D point1, Point4D point2)
		{
			return new Point4D(point1._x + point2._x, point1._y + point2._y, point1._z + point2._z, point1._w + point2._w);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> e retorna o resultado como uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> da qual subtrair.</param>
		/// <param name="point2">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> para subtrair de <paramref name="point1" />.</param>
		/// <returns>Retorna a diferença entre <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06003054 RID: 12372 RVA: 0x000C1188 File Offset: 0x000C0588
		public static Point4D operator -(Point4D point1, Point4D point2)
		{
			return new Point4D(point1._x - point2._x, point1._y - point2._y, point1._z - point2._z, point1._w - point2._w);
		}

		/// <summary>Subtrai uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> da qual subtrair.</param>
		/// <param name="point2">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> para subtrair de <paramref name="point1" />.</param>
		/// <returns>Retorna a diferença entre <paramref name="point1" /> e <paramref name="point2" />.</returns>
		// Token: 0x06003055 RID: 12373 RVA: 0x000C11D0 File Offset: 0x000C05D0
		public static Point4D Subtract(Point4D point1, Point4D point2)
		{
			return new Point4D(point1._x - point2._x, point1._y - point2._y, point1._z - point2._z, point1._w - point2._w);
		}

		/// <summary>Transforma a estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> especificada pela estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser transformada.</param>
		/// <param name="matrix">A estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> de transformação.</param>
		/// <returns>Retorna o resultado da transformar <paramref name="point" /> e <paramref name="matrix" />.</returns>
		// Token: 0x06003056 RID: 12374 RVA: 0x000C1218 File Offset: 0x000C0618
		public static Point4D operator *(Point4D point, Matrix3D matrix)
		{
			return matrix.Transform(point);
		}

		/// <summary>Transforma a estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> especificada pela estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificada.</summary>
		/// <param name="point">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser transformada.</param>
		/// <param name="matrix">A estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> de transformação.</param>
		/// <returns>Retorna o resultado da transformar <paramref name="point" /> e <paramref name="matrix" />.</returns>
		// Token: 0x06003057 RID: 12375 RVA: 0x000C1230 File Offset: 0x000C0630
		public static Point4D Multiply(Point4D point, Matrix3D matrix)
		{
			return matrix.Transform(point);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as coordenadas <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> do <paramref name="point4D1" /> e do <paramref name="point4D2" /> forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003058 RID: 12376 RVA: 0x000C1248 File Offset: 0x000C0648
		public static bool operator ==(Point4D point1, Point4D point2)
		{
			return point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z && point1.W == point2.W;
		}

		/// <summary>Compara duas estruturas <see cref="T:System.Windows.Media.Media3D.Point4D" /> quanto à desigualdade.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as coordenadas <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> de <paramref name="point4D1" /> e <paramref name="point4D2" /> forem diferentes; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003059 RID: 12377 RVA: 0x000C1298 File Offset: 0x000C0698
		public static bool operator !=(Point4D point1, Point4D point2)
		{
			return !(point1 == point2);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="point1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <param name="point2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> de <paramref name="point3D1" /> e <paramref name="point3D2" /> forem iguais; <see langword="false" />, caso contrário.</returns>
		// Token: 0x0600305A RID: 12378 RVA: 0x000C12B0 File Offset: 0x000C06B0
		public static bool Equals(Point4D point1, Point4D point2)
		{
			return point1.X.Equals(point2.X) && point1.Y.Equals(point2.Y) && point1.Z.Equals(point2.Z) && point1.W.Equals(point2.W);
		}

		/// <summary>Determina se o <see cref="T:System.Object" /> especificado é uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> e se as propriedades <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> do <see cref="T:System.Object" /> especificado são iguais às propriedades <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> deste atributo <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="o">O objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.  
		///  <see langword="true" /> se <paramref name="o" /> (o <see cref="T:System.Object" /> passado) for uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> e for idêntica a esta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />; <see langword="false" />, caso contrário.</returns>
		// Token: 0x0600305B RID: 12379 RVA: 0x000C1320 File Offset: 0x000C0720
		public override bool Equals(object o)
		{
			if (o == null || !(o is Point4D))
			{
				return false;
			}
			Point4D point = (Point4D)o;
			return Point4D.Equals(this, point);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="value">A instância de Point4D a ser comparada a esta instância.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600305C RID: 12380 RVA: 0x000C1350 File Offset: 0x000C0750
		public bool Equals(Point4D value)
		{
			return Point4D.Equals(this, value);
		}

		/// <summary>Retorna o código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>Retorna o código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</returns>
		// Token: 0x0600305D RID: 12381 RVA: 0x000C136C File Offset: 0x000C076C
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}

		/// <summary>Converte uma representação <see cref="T:System.String" /> de uma estrutura point4D em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> equivalente.</summary>
		/// <param name="source">A representação <see cref="T:System.String" /> da estrutura point4D.</param>
		/// <returns>Retorna a estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> equivalente.</returns>
		// Token: 0x0600305E RID: 12382 RVA: 0x000C13B4 File Offset: 0x000C07B4
		public static Point4D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string value = tokenizerHelper.NextTokenRequired();
			Point4D result = new Point4D(Convert.ToDouble(value, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			tokenizerHelper.LastTokenRequired();
			return result;
		}

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Point4D.X" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Point4D.X" /> componente isso <see cref="T:System.Windows.Media.Media3D.Point4D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000C1410 File Offset: 0x000C0810
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000C1424 File Offset: 0x000C0824
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

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Point4D.Y" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Point4D.Y" /> componente isso <see cref="T:System.Windows.Media.Media3D.Point4D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000C1438 File Offset: 0x000C0838
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x000C144C File Offset: 0x000C084C
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

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> componente isso <see cref="T:System.Windows.Media.Media3D.Point4D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000C1460 File Offset: 0x000C0860
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x000C1474 File Offset: 0x000C0874
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

		/// <summary>Obtém ou define o componente <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> componente isso <see cref="T:System.Windows.Media.Media3D.Point4D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000C1488 File Offset: 0x000C0888
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x000C149C File Offset: 0x000C089C
		public double W
		{
			get
			{
				return this._w;
			}
			set
			{
				this._w = value;
			}
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</returns>
		// Token: 0x06003067 RID: 12391 RVA: 0x000C14B0 File Offset: 0x000C08B0
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Point4D.X" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Y" />, <see cref="P:System.Windows.Media.Media3D.Point4D.Z" /> e <see cref="P:System.Windows.Media.Media3D.Point4D.W" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</returns>
		// Token: 0x06003068 RID: 12392 RVA: 0x000C14C8 File Offset: 0x000C08C8
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
		// Token: 0x06003069 RID: 12393 RVA: 0x000C14E0 File Offset: 0x000C08E0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000C14F8 File Offset: 0x000C08F8
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

		// Token: 0x04001548 RID: 5448
		internal double _x;

		// Token: 0x04001549 RID: 5449
		internal double _y;

		// Token: 0x0400154A RID: 5450
		internal double _z;

		// Token: 0x0400154B RID: 5451
		internal double _w;
	}
}
