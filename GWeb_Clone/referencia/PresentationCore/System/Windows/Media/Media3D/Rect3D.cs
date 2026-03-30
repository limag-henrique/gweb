using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um retângulo 3D: por exemplo, um cubo.</summary>
	// Token: 0x0200047B RID: 1147
	[TypeConverter(typeof(Rect3DConverter))]
	[ValueSerializer(typeof(Rect3DValueSerializer))]
	[Serializable]
	public struct Rect3D : IFormattable
	{
		/// <summary>Inicializa uma nova instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="location">Local do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="size">Tamanho do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		// Token: 0x0600311C RID: 12572 RVA: 0x000C44D4 File Offset: 0x000C38D4
		public Rect3D(Point3D location, Size3D size)
		{
			if (size.IsEmpty)
			{
				this = Rect3D.s_empty;
				return;
			}
			this._x = location._x;
			this._y = location._y;
			this._z = location._z;
			this._sizeX = size._x;
			this._sizeY = size._y;
			this._sizeZ = size._z;
		}

		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="x">Coordenada do eixo X do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="y">Coordenada do eixo Y do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="z">Coordenada do eixo Z do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="sizeX">Tamanho do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão X.</param>
		/// <param name="sizeY">Tamanho do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão Y.</param>
		/// <param name="sizeZ">Tamanho do novo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão Z.</param>
		// Token: 0x0600311D RID: 12573 RVA: 0x000C4540 File Offset: 0x000C3940
		public Rect3D(double x, double y, double z, double sizeX, double sizeY, double sizeZ)
		{
			if (sizeX < 0.0 || sizeY < 0.0 || sizeZ < 0.0)
			{
				throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
			}
			this._x = x;
			this._y = y;
			this._z = z;
			this._sizeX = sizeX;
			this._sizeY = sizeY;
			this._sizeZ = sizeZ;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000C45B4 File Offset: 0x000C39B4
		internal Rect3D(Point3D point1, Point3D point2)
		{
			this._x = Math.Min(point1._x, point2._x);
			this._y = Math.Min(point1._y, point2._y);
			this._z = Math.Min(point1._z, point2._z);
			this._sizeX = Math.Max(point1._x, point2._x) - this._x;
			this._sizeY = Math.Max(point1._y, point2._y) - this._y;
			this._sizeZ = Math.Max(point1._z, point2._z) - this._z;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000C4660 File Offset: 0x000C3A60
		internal Rect3D(Point3D point, Vector3D vector)
		{
			this = new Rect3D(point, point + vector);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> vazio.</returns>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000C467C File Offset: 0x000C3A7C
		public static Rect3D Empty
		{
			get
			{
				return Rect3D.s_empty;
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é o <see cref="P:System.Windows.Media.Media3D.Rect3D.Empty" /><see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se este <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é o retângulo vazio; <see langword="false" /> caso contrário.</returns>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000C4690 File Offset: 0x000C3A90
		public bool IsEmpty
		{
			get
			{
				return this._sizeX < 0.0;
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Point3D" /> que representa a origem do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Point3D" /> que representa a origem do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, normalmente no canto inferior traseiro esquerdo.  O valor padrão é (0, 0,0).</returns>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000C46B0 File Offset: 0x000C3AB0
		// (set) Token: 0x06003123 RID: 12579 RVA: 0x000C46D4 File Offset: 0x000C3AD4
		public Point3D Location
		{
			get
			{
				return new Point3D(this._x, this._y, this._z);
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				this._x = value._x;
				this._y = value._y;
				this._z = value._z;
			}
		}

		/// <summary>Obtém ou define a área do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Size3D" /> que especifica a área do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000C4720 File Offset: 0x000C3B20
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x000C4754 File Offset: 0x000C3B54
		public Size3D Size
		{
			get
			{
				if (this.IsEmpty)
				{
					return Size3D.Empty;
				}
				return new Size3D(this._sizeX, this._sizeY, this._sizeZ);
			}
			set
			{
				if (value.IsEmpty)
				{
					this = Rect3D.s_empty;
					return;
				}
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				this._sizeX = value._x;
				this._sizeY = value._y;
				this._sizeZ = value._z;
			}
		}

		/// <summary>Obtém ou define o tamanho do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão X.</summary>
		/// <returns>Duplo que especifica o tamanho do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão X.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000C47B4 File Offset: 0x000C3BB4
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000C47C8 File Offset: 0x000C3BC8
		public double SizeX
		{
			get
			{
				return this._sizeX;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._sizeX = value;
			}
		}

		/// <summary>Obtém ou define o tamanho do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão Y.</summary>
		/// <returns>Duplo que especifica o tamanho do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão Y.</returns>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000C4810 File Offset: 0x000C3C10
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x000C4824 File Offset: 0x000C3C24
		public double SizeY
		{
			get
			{
				return this._sizeY;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._sizeY = value;
			}
		}

		/// <summary>Obtém ou define o tamanho do Rect3D na dimensão Z.</summary>
		/// <returns>Duplo que especifica o tamanho do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na dimensão Z.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000C486C File Offset: 0x000C3C6C
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x000C4880 File Offset: 0x000C3C80
		public double SizeZ
		{
			get
			{
				return this._sizeZ;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._sizeZ = value;
			}
		}

		/// <summary>Obtém ou define o valor da coordenada X do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>Valor da coordenada X do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000C48C8 File Offset: 0x000C3CC8
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x000C48DC File Offset: 0x000C3CDC
		public double X
		{
			get
			{
				return this._x;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				this._x = value;
			}
		}

		/// <summary>Obtém ou define o valor da coordenada Y do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>Valor da coordenada Y do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000C4908 File Offset: 0x000C3D08
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x000C491C File Offset: 0x000C3D1C
		public double Y
		{
			get
			{
				return this._y;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				this._y = value;
			}
		}

		/// <summary>Obtém ou define o valor da coordenada Z do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <returns>Valor da coordenada Z do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000C4948 File Offset: 0x000C3D48
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x000C495C File Offset: 0x000C3D5C
		public double Z
		{
			get
			{
				return this._z;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Rect3D_CannotModifyEmptyRect"));
				}
				this._z = value;
			}
		}

		/// <summary>Obtém um valor que indica se um <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas.</summary>
		/// <param name="point">Ponto a ser testado.</param>
		/// <returns>Verdadeiro se o ponto ou retângulo especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas; caso contrário, falso.</returns>
		// Token: 0x06003132 RID: 12594 RVA: 0x000C4988 File Offset: 0x000C3D88
		public bool Contains(Point3D point)
		{
			return this.Contains(point._x, point._y, point._z);
		}

		/// <summary>Obtém um valor que indica se um <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas.</summary>
		/// <param name="x">Coordenada do eixo X do <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser testada.</param>
		/// <param name="y">Coordenada do eixo Y do <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser testada.</param>
		/// <param name="z">Coordenada Z do <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser testada.</param>
		/// <returns>
		///   <see langword="true" /> se o ponto ou retângulo especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003133 RID: 12595 RVA: 0x000C49B0 File Offset: 0x000C3DB0
		public bool Contains(double x, double y, double z)
		{
			return !this.IsEmpty && this.ContainsInternal(x, y, z);
		}

		/// <summary>Obtém um valor que indica se um <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser testado.</param>
		/// <returns>Verdadeiro se o ponto ou retângulo especificado está dentro do <see cref="T:System.Windows.Media.Media3D.Rect3D" />, incluindo suas bordas; caso contrário, falso.</returns>
		// Token: 0x06003134 RID: 12596 RVA: 0x000C49D0 File Offset: 0x000C3DD0
		public bool Contains(Rect3D rect)
		{
			return !this.IsEmpty && !rect.IsEmpty && (this._x <= rect._x && this._y <= rect._y && this._z <= rect._z && this._x + this._sizeX >= rect._x + rect._sizeX && this._y + this._sizeY >= rect._y + rect._sizeY) && this._z + this._sizeZ >= rect._z + rect._sizeZ;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado intersecciona este <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="rect">Retângulo a ser avaliado.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado intersecciona este <see cref="T:System.Windows.Media.Media3D.Rect3D" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003135 RID: 12597 RVA: 0x000C4A78 File Offset: 0x000C3E78
		public bool IntersectsWith(Rect3D rect)
		{
			return !this.IsEmpty && !rect.IsEmpty && (rect._x <= this._x + this._sizeX && rect._x + rect._sizeX >= this._x && rect._y <= this._y + this._sizeY && rect._y + rect._sizeY >= this._y && rect._z <= this._z + this._sizeZ) && rect._z + rect._sizeZ >= this._z;
		}

		/// <summary>Localiza a interseção do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> atual e do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado e armazena o resultado como o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> atual.</summary>
		/// <param name="rect">O <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser interseccionado com o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> atual.</param>
		// Token: 0x06003136 RID: 12598 RVA: 0x000C4B1C File Offset: 0x000C3F1C
		public void Intersect(Rect3D rect)
		{
			if (this.IsEmpty || rect.IsEmpty || !this.IntersectsWith(rect))
			{
				this = Rect3D.Empty;
				return;
			}
			double num = Math.Max(this._x, rect._x);
			double num2 = Math.Max(this._y, rect._y);
			double num3 = Math.Max(this._z, rect._z);
			this._sizeX = Math.Min(this._x + this._sizeX, rect._x + rect._sizeX) - num;
			this._sizeY = Math.Min(this._y + this._sizeY, rect._y + rect._sizeY) - num2;
			this._sizeZ = Math.Min(this._z + this._sizeZ, rect._z + rect._sizeZ) - num3;
			this._x = num;
			this._y = num2;
			this._z = num3;
		}

		/// <summary>Retorna a interseção dos valores <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificados.</summary>
		/// <param name="rect1">Primeiro <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="rect2">Segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <returns>Resultado da interseção de <paramref name="rect1" /> e <paramref name="rect2" />.</returns>
		// Token: 0x06003137 RID: 12599 RVA: 0x000C4C10 File Offset: 0x000C4010
		public static Rect3D Intersect(Rect3D rect1, Rect3D rect2)
		{
			rect1.Intersect(rect2);
			return rect1;
		}

		/// <summary>Atualiza um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado para refletir a união desse <see cref="T:System.Windows.Media.Media3D.Rect3D" /> e um segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado.</summary>
		/// <param name="rect">O <see cref="T:System.Windows.Media.Media3D.Rect3D" /> cuja união com o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> atual deve ser avaliado.</param>
		// Token: 0x06003138 RID: 12600 RVA: 0x000C4C28 File Offset: 0x000C4028
		public void Union(Rect3D rect)
		{
			if (this.IsEmpty)
			{
				this = rect;
				return;
			}
			if (!rect.IsEmpty)
			{
				double num = Math.Min(this._x, rect._x);
				double num2 = Math.Min(this._y, rect._y);
				double num3 = Math.Min(this._z, rect._z);
				this._sizeX = Math.Max(this._x + this._sizeX, rect._x + rect._sizeX) - num;
				this._sizeY = Math.Max(this._y + this._sizeY, rect._y + rect._sizeY) - num2;
				this._sizeZ = Math.Max(this._z + this._sizeZ, rect._z + rect._sizeZ) - num3;
				this._x = num;
				this._y = num2;
				this._z = num3;
			}
		}

		/// <summary>Retorna uma nova instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que representa a união de dois objetos <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="rect1">Primeiro <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="rect2">Segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que representa o resultado da união de <paramref name="rect1" /> e <paramref name="rect2" />.</returns>
		// Token: 0x06003139 RID: 12601 RVA: 0x000C4D14 File Offset: 0x000C4114
		public static Rect3D Union(Rect3D rect1, Rect3D rect2)
		{
			rect1.Union(rect2);
			return rect1;
		}

		/// <summary>Atualiza um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado para refletir a união desse <see cref="T:System.Windows.Media.Media3D.Rect3D" /> e um <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="point">O <see cref="T:System.Windows.Media.Media3D.Point3D" /> cuja união com o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado deve ser avaliado.</param>
		// Token: 0x0600313A RID: 12602 RVA: 0x000C4D2C File Offset: 0x000C412C
		public void Union(Point3D point)
		{
			this.Union(new Rect3D(point, point));
		}

		/// <summary>Retorna um novo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que representa a união de um <see cref="T:System.Windows.Media.Media3D.Rect3D" />e um <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="rect">O <see cref="T:System.Windows.Media.Media3D.Rect3D" /> cuja união com o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> atual deve ser avaliado.</param>
		/// <param name="point">O <see cref="T:System.Windows.Media.Media3D.Point3D" /> cuja união com o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificado deve ser avaliado.</param>
		/// <returns>Resultado da união de <paramref name="rect" /> e <paramref name="point" />.</returns>
		// Token: 0x0600313B RID: 12603 RVA: 0x000C4D48 File Offset: 0x000C4148
		public static Rect3D Union(Rect3D rect, Point3D point)
		{
			rect.Union(new Rect3D(point, point));
			return rect;
		}

		/// <summary>Define a translação de deslocamento do <see cref="T:System.Windows.Media.Media3D.Rect3D" /> para o valor fornecido, especificado como um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="offsetVector">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a translação de deslocamento.</param>
		// Token: 0x0600313C RID: 12604 RVA: 0x000C4D64 File Offset: 0x000C4164
		public void Offset(Vector3D offsetVector)
		{
			this.Offset(offsetVector._x, offsetVector._y, offsetVector._z);
		}

		/// <summary>Obtém ou define um valor de deslocamento no qual o local de um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é convertido.</summary>
		/// <param name="offsetX">Deslocamento ao longo do eixo X.</param>
		/// <param name="offsetY">Deslocamento ao longo do eixo Y.</param>
		/// <param name="offsetZ">Deslocamento ao longo do eixo Z.</param>
		// Token: 0x0600313D RID: 12605 RVA: 0x000C4D8C File Offset: 0x000C418C
		public void Offset(double offsetX, double offsetY, double offsetZ)
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.Get("Rect3D_CannotCallMethod"));
			}
			this._x += offsetX;
			this._y += offsetY;
			this._z += offsetZ;
		}

		/// <summary>Obtém ou define um valor de deslocamento no qual o local de um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é convertido.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser movido.</param>
		/// <param name="offsetVector">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a translação de deslocamento.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que representa o resultado do deslocamento.</returns>
		// Token: 0x0600313E RID: 12606 RVA: 0x000C4DDC File Offset: 0x000C41DC
		public static Rect3D Offset(Rect3D rect, Vector3D offsetVector)
		{
			rect.Offset(offsetVector._x, offsetVector._y, offsetVector._z);
			return rect;
		}

		/// <summary>Obtém ou define um valor de deslocamento no qual o local de um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é convertido.</summary>
		/// <param name="rect">Rect3D a ser convertido.</param>
		/// <param name="offsetX">Deslocamento ao longo do eixo X.</param>
		/// <param name="offsetY">Deslocamento ao longo do eixo Y.</param>
		/// <param name="offsetZ">Deslocamento ao longo do eixo Z.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que representa o resultado do deslocamento.</returns>
		// Token: 0x0600313F RID: 12607 RVA: 0x000C4E04 File Offset: 0x000C4204
		public static Rect3D Offset(Rect3D rect, double offsetX, double offsetY, double offsetZ)
		{
			rect.Offset(offsetX, offsetY, offsetZ);
			return rect;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000C4E1C File Offset: 0x000C421C
		private bool ContainsInternal(double x, double y, double z)
		{
			return x >= this._x && x <= this._x + this._sizeX && y >= this._y && y <= this._y + this._sizeY && z >= this._z && z <= this._z + this._sizeZ;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000C4E7C File Offset: 0x000C427C
		private static Rect3D CreateEmptyRect3D()
		{
			return new Rect3D
			{
				_x = double.PositiveInfinity,
				_y = double.PositiveInfinity,
				_z = double.PositiveInfinity,
				_sizeX = double.NegativeInfinity,
				_sizeY = double.NegativeInfinity,
				_sizeZ = double.NegativeInfinity
			};
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000C4EF4 File Offset: 0x000C42F4
		private static Rect3D CreateInfiniteRect3D()
		{
			return new Rect3D
			{
				_x = -3.4028234663852886E+38,
				_y = -3.4028234663852886E+38,
				_z = -3.4028234663852886E+38,
				_sizeX = 6.8056469327705772E+38,
				_sizeY = 6.8056469327705772E+38,
				_sizeZ = 6.8056469327705772E+38
			};
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> quanto à igualdade exata.</summary>
		/// <param name="rect1">Primeiro <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser avaliado.</param>
		/// <param name="rect2">Segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser avaliado.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> são exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003143 RID: 12611 RVA: 0x000C4F6C File Offset: 0x000C436C
		public static bool operator ==(Rect3D rect1, Rect3D rect2)
		{
			return rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Z == rect2.Z && rect1.SizeX == rect2.SizeX && rect1.SizeY == rect2.SizeY && rect1.SizeZ == rect2.SizeZ;
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> quanto à desigualdade exata.</summary>
		/// <param name="rect1">Primeiro <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser comparado.</param>
		/// <param name="rect2">Segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> de comparação.</param>
		/// <returns>Verdadeiro se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> são diferentes; caso contrário, falso.</returns>
		// Token: 0x06003144 RID: 12612 RVA: 0x000C4FDC File Offset: 0x000C43DC
		public static bool operator !=(Rect3D rect1, Rect3D rect2)
		{
			return !(rect1 == rect2);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> quanto à igualdade.</summary>
		/// <param name="rect1">Primeiro <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser comparado.</param>
		/// <param name="rect2">Segundo <see cref="T:System.Windows.Media.Media3D.Rect3D" /> de comparação.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificadas forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003145 RID: 12613 RVA: 0x000C4FF4 File Offset: 0x000C43F4
		public static bool Equals(Rect3D rect1, Rect3D rect2)
		{
			if (rect1.IsEmpty)
			{
				return rect2.IsEmpty;
			}
			return rect1.X.Equals(rect2.X) && rect1.Y.Equals(rect2.Y) && rect1.Z.Equals(rect2.Z) && rect1.SizeX.Equals(rect2.SizeX) && rect1.SizeY.Equals(rect2.SizeY) && rect1.SizeZ.Equals(rect2.SizeZ);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> quanto à igualdade.</summary>
		/// <param name="o">O objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificadas forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003146 RID: 12614 RVA: 0x000C50A8 File Offset: 0x000C44A8
		public override bool Equals(object o)
		{
			if (o == null || !(o is Rect3D))
			{
				return false;
			}
			Rect3D rect = (Rect3D)o;
			return Rect3D.Equals(this, rect);
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> quanto à igualdade.</summary>
		/// <param name="value">A instância <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser comparada com a instância atual.</param>
		/// <returns>
		///   <see langword="true" /> se as duas instâncias <see cref="T:System.Windows.Media.Media3D.Rect3D" /> especificadas forem exatamente iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003147 RID: 12615 RVA: 0x000C50D8 File Offset: 0x000C44D8
		public bool Equals(Rect3D value)
		{
			return Rect3D.Equals(this, value);
		}

		/// <summary>Retorna o código hash para o <see cref="T:System.Windows.Media.Media3D.Rect3D" /></summary>
		/// <returns>Um código hash para este <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x06003148 RID: 12616 RVA: 0x000C50F4 File Offset: 0x000C44F4
		public override int GetHashCode()
		{
			if (this.IsEmpty)
			{
				return 0;
			}
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.SizeX.GetHashCode() ^ this.SizeY.GetHashCode() ^ this.SizeZ.GetHashCode();
		}

		/// <summary>Converte uma representação de cadeia de caracteres de um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> na estrutura <see cref="T:System.Windows.Media.Media3D.Rect3D" /> equivalente.</summary>
		/// <param name="source">Cadeia de caracteres que representa um <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x06003149 RID: 12617 RVA: 0x000C5164 File Offset: 0x000C4564
		public static Rect3D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string text = tokenizerHelper.NextTokenRequired();
			Rect3D empty;
			if (text == "Empty")
			{
				empty = Rect3D.Empty;
			}
			else
			{
				empty = new Rect3D(Convert.ToDouble(text, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			}
			tokenizerHelper.LastTokenRequired();
			return empty;
		}

		/// <summary>Cria uma representação de cadeia de caracteres do Rect3D.</summary>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x0600314A RID: 12618 RVA: 0x000C51EC File Offset: 0x000C45EC
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</returns>
		// Token: 0x0600314B RID: 12619 RVA: 0x000C5204 File Offset: 0x000C4604
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
		// Token: 0x0600314C RID: 12620 RVA: 0x000C521C File Offset: 0x000C461C
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000C5234 File Offset: 0x000C4634
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.IsEmpty)
			{
				return "Empty";
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
				"}{0}{5:",
				format,
				"}{0}{6:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this._x,
				this._y,
				this._z,
				this._sizeX,
				this._sizeY,
				this._sizeZ
			});
		}

		// Token: 0x0400157E RID: 5502
		internal static readonly Rect3D Infinite = Rect3D.CreateInfiniteRect3D();

		// Token: 0x0400157F RID: 5503
		private static readonly Rect3D s_empty = Rect3D.CreateEmptyRect3D();

		// Token: 0x04001580 RID: 5504
		internal double _x;

		// Token: 0x04001581 RID: 5505
		internal double _y;

		// Token: 0x04001582 RID: 5506
		internal double _z;

		// Token: 0x04001583 RID: 5507
		internal double _sizeX;

		// Token: 0x04001584 RID: 5508
		internal double _sizeY;

		// Token: 0x04001585 RID: 5509
		internal double _sizeZ;
	}
}
