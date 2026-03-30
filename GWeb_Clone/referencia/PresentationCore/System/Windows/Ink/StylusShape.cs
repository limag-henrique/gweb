using System;
using System.Windows.Media;
using MS.Internal;

namespace System.Windows.Ink
{
	/// <summary>Representa a ponta de uma caneta.</summary>
	// Token: 0x0200033E RID: 830
	public abstract class StylusShape
	{
		// Token: 0x06001C36 RID: 7222 RVA: 0x000730B4 File Offset: 0x000724B4
		internal StylusShape()
		{
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000730D4 File Offset: 0x000724D4
		internal StylusShape(StylusTip tip, double width, double height, double rotation)
		{
			if (double.IsNaN(width) || double.IsInfinity(width) || width < DrawingAttributes.MinWidth || width > DrawingAttributes.MaxWidth)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (double.IsNaN(height) || double.IsInfinity(height) || height < DrawingAttributes.MinHeight || height > DrawingAttributes.MaxHeight)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			if (double.IsNaN(rotation) || double.IsInfinity(rotation))
			{
				throw new ArgumentOutOfRangeException("rotation");
			}
			if (!StylusTipHelper.IsDefined(tip))
			{
				throw new ArgumentOutOfRangeException("tip");
			}
			this.m_width = width;
			this.m_height = height;
			this.m_rotation = ((rotation == 0.0) ? 0.0 : (rotation % 360.0));
			this.m_tip = tip;
			if (tip == StylusTip.Rectangle)
			{
				this.ComputeRectangleVertices();
			}
		}

		/// <summary>Obtém a largura da caneta.</summary>
		/// <returns>A largura da caneta.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000731C0 File Offset: 0x000725C0
		public double Width
		{
			get
			{
				return this.m_width;
			}
		}

		/// <summary>Obtém a altura da caneta.</summary>
		/// <returns>A altura da caneta.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x000731D4 File Offset: 0x000725D4
		public double Height
		{
			get
			{
				return this.m_height;
			}
		}

		/// <summary>Obtém o ângulo da caneta.</summary>
		/// <returns>O ângulo da caneta.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000731E8 File Offset: 0x000725E8
		public double Rotation
		{
			get
			{
				return this.m_rotation;
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000731FC File Offset: 0x000725FC
		internal Vector[] GetVerticesAsVectors()
		{
			Vector[] array;
			if (this.m_vertices != null)
			{
				array = new Vector[this.m_vertices.Length];
				if (this._transform.IsIdentity)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = (Vector)this.m_vertices[i];
					}
				}
				else
				{
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = this._transform.Transform((Vector)this.m_vertices[j]);
					}
					this.FixCounterClockwiseVertices(array);
				}
			}
			else
			{
				Point[] bezierControlPoints = this.GetBezierControlPoints();
				array = new Vector[bezierControlPoints.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = (Vector)bezierControlPoints[k];
				}
			}
			return array;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x000732C8 File Offset: 0x000726C8
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x000732DC File Offset: 0x000726DC
		internal Matrix Transform
		{
			get
			{
				return this._transform;
			}
			set
			{
				this._transform = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x000732F0 File Offset: 0x000726F0
		internal bool IsEllipse
		{
			get
			{
				return this.m_vertices == null;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x00073308 File Offset: 0x00072708
		internal bool IsPolygon
		{
			get
			{
				return this.m_vertices != null;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00073320 File Offset: 0x00072720
		internal Rect BoundingBox
		{
			get
			{
				Rect empty;
				if (this.IsPolygon)
				{
					empty = Rect.Empty;
					foreach (Point point in this.m_vertices)
					{
						empty.Union(point);
					}
				}
				else
				{
					empty = new Rect(-(this.m_width * 0.5), -(this.m_height * 0.5), this.m_width, this.m_height);
				}
				return empty;
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00073398 File Offset: 0x00072798
		private void ComputeRectangleVertices()
		{
			Point point = new Point(-(this.m_width * 0.5), -(this.m_height * 0.5));
			this.m_vertices = new Point[]
			{
				point,
				point + new Vector(this.m_width, 0.0),
				point + new Vector(this.m_width, this.m_height),
				point + new Vector(0.0, this.m_height)
			};
			if (!DoubleUtil.IsZero(this.m_rotation))
			{
				Matrix identity = Matrix.Identity;
				identity.Rotate(this.m_rotation);
				identity.Transform(this.m_vertices);
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00073470 File Offset: 0x00072870
		private void FixCounterClockwiseVertices(Vector[] vertices)
		{
			Point point = (Point)vertices[vertices.Length - 1];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				Point point2 = (Point)vertices[i];
				Vector vector = point2 - point;
				double num3 = Vector.Determinant(vector, (Point)vertices[(i + 1) % vertices.Length] - point2);
				if (0.0 > num3)
				{
					num++;
				}
				else if (0.0 < num3)
				{
					num2++;
				}
				point = point2;
			}
			if (num == vertices.Length)
			{
				int num4 = vertices.Length - 1;
				for (int j = 0; j < vertices.Length / 2; j++)
				{
					Vector vector2 = vertices[j];
					vertices[j] = vertices[num4 - j];
					vertices[num4 - j] = vector2;
				}
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00073548 File Offset: 0x00072948
		private Point[] GetBezierControlPoints()
		{
			double num = this.m_width / 2.0;
			double num2 = this.m_height / 2.0;
			double num3 = num * 0.55228474983079345;
			double num4 = num2 * 0.55228474983079345;
			Point[] array = new Point[]
			{
				new Point(-num, -num4),
				new Point(-num3, -num2),
				new Point(0.0, -num2),
				new Point(num3, -num2),
				new Point(num, -num4),
				new Point(num, 0.0),
				new Point(num, num4),
				new Point(num3, num2),
				new Point(0.0, num2),
				new Point(-num3, num2),
				new Point(-num, num4),
				new Point(-num, 0.0)
			};
			Matrix trans = Matrix.Identity;
			if (this.m_rotation != 0.0)
			{
				trans.Rotate(this.m_rotation);
			}
			if (!this._transform.IsIdentity)
			{
				trans *= this._transform;
			}
			if (!trans.IsIdentity)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = trans.Transform(array[i]);
				}
			}
			return array;
		}

		// Token: 0x04000F44 RID: 3908
		private double m_width;

		// Token: 0x04000F45 RID: 3909
		private double m_height;

		// Token: 0x04000F46 RID: 3910
		private double m_rotation;

		// Token: 0x04000F47 RID: 3911
		private Point[] m_vertices;

		// Token: 0x04000F48 RID: 3912
		private StylusTip m_tip;

		// Token: 0x04000F49 RID: 3913
		private Matrix _transform = Matrix.Identity;
	}
}
