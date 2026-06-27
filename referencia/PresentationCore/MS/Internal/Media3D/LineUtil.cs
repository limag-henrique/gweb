using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MS.Internal.Media3D
{
	// Token: 0x020006F4 RID: 1780
	internal static class LineUtil
	{
		// Token: 0x06004CAE RID: 19630 RVA: 0x0012DA50 File Offset: 0x0012CE50
		public static void Transform(Matrix3D modelMatrix, ref Point3D origin, ref Vector3D direction, out bool isRay)
		{
			if (!modelMatrix.InvertCore())
			{
				LineUtil.TransformSingular(ref modelMatrix, ref origin, ref direction);
				isRay = false;
				return;
			}
			Point4D point4D = new Point4D(origin.X, origin.Y, origin.Z, 1.0);
			Point4D point4D2 = new Point4D(direction.X, direction.Y, direction.Z, 0.0);
			modelMatrix.MultiplyPoint(ref point4D);
			modelMatrix.MultiplyPoint(ref point4D2);
			if (point4D.W == 1.0 && point4D2.W == 0.0)
			{
				origin = new Point3D(point4D.X, point4D.Y, point4D.Z);
				direction = new Vector3D(point4D2.X, point4D2.Y, point4D2.Z);
				isRay = true;
				return;
			}
			double[,] array = new double[4, 2];
			array[0, 0] = point4D.X;
			array[0, 1] = point4D2.X;
			array[1, 0] = point4D.Y;
			array[1, 1] = point4D2.Y;
			array[2, 0] = point4D.Z;
			array[2, 1] = point4D2.Z;
			array[3, 0] = point4D.W;
			array[3, 1] = point4D2.W;
			double[,] matrix = array;
			LineUtil.ColumnsToAffinePointVector(matrix, 0, 1, out origin, out direction);
			isRay = false;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0012DBC4 File Offset: 0x0012CFC4
		private static void TransformSingular(ref Matrix3D modelMatrix, ref Point3D origin, ref Vector3D direction)
		{
			double[,] array = LineUtil.TransformedLineMatrix(ref modelMatrix, ref origin, ref direction);
			array = LineUtil.Square(array);
			double[,] array2 = new double[,]
			{
				{
					1.0,
					0.0,
					0.0,
					0.0
				},
				{
					0.0,
					1.0,
					0.0,
					0.0
				},
				{
					0.0,
					0.0,
					1.0,
					0.0
				},
				{
					0.0,
					0.0,
					0.0,
					1.0
				}
			};
			int num = 30;
			for (int i = 0; i < num; i++)
			{
				int num2 = i % 6;
				LineUtil.JacobiRotation jacobiRotation = new LineUtil.JacobiRotation(LineUtil.s_pairs[num2, 0], LineUtil.s_pairs[num2, 1], array);
				array = jacobiRotation.LeftRightMultiply(array);
				array2 = jacobiRotation.RightMultiply(array2);
			}
			int col;
			int col2;
			LineUtil.FindSmallestTwoDiagonal(array, out col, out col2);
			LineUtil.ColumnsToAffinePointVector(array2, col, col2, out origin, out direction);
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0012DC54 File Offset: 0x0012D054
		private static void ColumnsToAffinePointVector(double[,] matrix, int col1, int col2, out Point3D origin, out Vector3D direction)
		{
			if (matrix[3, col1] * matrix[3, col1] < matrix[3, col2] * matrix[3, col2])
			{
				int num = col1;
				col1 = col2;
				col2 = num;
			}
			double num2 = 1.0 / matrix[3, col1];
			origin = new Point3D(num2 * matrix[0, col1], num2 * matrix[1, col1], num2 * matrix[2, col1]);
			num2 = -matrix[3, col2];
			direction = new Vector3D(matrix[0, col2] + num2 * origin.X, matrix[1, col2] + num2 * origin.Y, matrix[2, col2] + num2 * origin.Z);
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0012DD14 File Offset: 0x0012D114
		private static void FindSmallestTwoDiagonal(double[,] matrix, out int evec1, out int evec2)
		{
			evec1 = 0;
			evec2 = 1;
			double num = matrix[0, 0] * matrix[0, 0];
			double num2 = matrix[1, 1] * matrix[1, 1];
			for (int i = 2; i < 4; i++)
			{
				double num3 = matrix[i, i] * matrix[i, i];
				if (num3 < num)
				{
					if (num < num2)
					{
						num2 = num3;
						evec2 = i;
					}
					else
					{
						num = num3;
						evec1 = i;
					}
				}
				else if (num3 < num2)
				{
					num2 = num3;
					evec2 = i;
				}
			}
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0012DD88 File Offset: 0x0012D188
		private static double[,] TransformedLineMatrix(ref Matrix3D modelMatrix, ref Point3D origin, ref Vector3D direction)
		{
			double x = origin.X;
			double y = origin.Y;
			double z = origin.Z;
			double x2 = direction.X;
			double y2 = direction.Y;
			double z2 = direction.Z;
			double num = y2 * z - y * z2;
			double num2 = x * z2 - x2 * z;
			double num3 = x2 * y - x * y2;
			Matrix3D matrix3D = modelMatrix * new Matrix3D(num, y2, z2, 0.0, num2, -x2, 0.0, z2, num3, 0.0, -x2, -y2, 0.0, num3, -num2, num);
			double[,] array = new double[4, 4];
			array[0, 0] = matrix3D.M11;
			array[0, 1] = matrix3D.M12;
			array[0, 2] = matrix3D.M13;
			array[0, 3] = matrix3D.M14;
			array[1, 0] = matrix3D.M21;
			array[1, 1] = matrix3D.M22;
			array[1, 2] = matrix3D.M23;
			array[1, 3] = matrix3D.M24;
			array[2, 0] = matrix3D.M31;
			array[2, 1] = matrix3D.M32;
			array[2, 2] = matrix3D.M33;
			array[2, 3] = matrix3D.M34;
			array[3, 0] = matrix3D.OffsetX;
			array[3, 1] = matrix3D.OffsetY;
			array[3, 2] = matrix3D.OffsetZ;
			array[3, 3] = matrix3D.M44;
			return array;
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0012DF28 File Offset: 0x0012D328
		private static double[,] Square(double[,] m)
		{
			double[,] array = new double[4, 4];
			double num = 0.0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					num = Math.Max(num, m[i, j] * m[i, j]);
				}
			}
			num = Math.Sqrt(num);
			for (int k = 0; k < 4; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					m[k, l] /= num;
				}
			}
			for (int n = 0; n < 4; n++)
			{
				for (int num2 = 0; num2 < 4; num2++)
				{
					double num3 = 0.0;
					for (int num4 = 0; num4 < 4; num4++)
					{
						num3 += m[n, num4] * m[num2, num4];
					}
					array[n, num2] = num3;
				}
			}
			return array;
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0012E00C File Offset: 0x0012D40C
		internal static bool ComputeLineTriangleIntersection(FaceType type, ref Point3D origin, ref Vector3D direction, ref Point3D v0, ref Point3D v1, ref Point3D v2, out Point hitCoord, out double dist)
		{
			Vector3D vector3D;
			Point3D.Subtract(ref v1, ref v0, out vector3D);
			Vector3D vector3D2;
			Point3D.Subtract(ref v2, ref v0, out vector3D2);
			Vector3D vector3D3;
			Vector3D.CrossProduct(ref direction, ref vector3D2, out vector3D3);
			double num = Vector3D.DotProduct(ref vector3D, ref vector3D3);
			Vector3D vector3D4;
			if (num > 0.0 && (type & FaceType.Front) != FaceType.None)
			{
				Point3D.Subtract(ref origin, ref v0, out vector3D4);
			}
			else
			{
				if (num >= 0.0 || (type & FaceType.Back) == FaceType.None)
				{
					hitCoord = default(Point);
					dist = 0.0;
					return false;
				}
				Point3D.Subtract(ref v0, ref origin, out vector3D4);
				num = -num;
			}
			double num2 = Vector3D.DotProduct(ref vector3D4, ref vector3D3);
			if (num2 < 0.0 || num < num2)
			{
				hitCoord = default(Point);
				dist = 0.0;
				return false;
			}
			Vector3D vector3D5;
			Vector3D.CrossProduct(ref vector3D4, ref vector3D, out vector3D5);
			double num3 = Vector3D.DotProduct(ref direction, ref vector3D5);
			if (num3 < 0.0 || num < num2 + num3)
			{
				hitCoord = default(Point);
				dist = 0.0;
				return false;
			}
			double num4 = Vector3D.DotProduct(ref vector3D2, ref vector3D5);
			double num5 = 1.0 / num;
			num4 *= num5;
			num2 *= num5;
			num3 *= num5;
			hitCoord = new Point(num2, num3);
			dist = num4;
			return true;
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0012E138 File Offset: 0x0012D538
		internal static bool ComputeLineBoxIntersection(ref Point3D origin, ref Vector3D direction, ref Rect3D box, bool isRay)
		{
			if (box.IsEmpty)
			{
				return false;
			}
			bool flag = true;
			bool[] array = new bool[3];
			double[] array2 = new double[3];
			double[] array3 = new double[]
			{
				box.X,
				box.Y,
				box.Z
			};
			double[] array4 = new double[]
			{
				box.X + box.SizeX,
				box.Y + box.SizeY,
				box.Z + box.SizeZ
			};
			double[] array5 = new double[]
			{
				origin.X,
				origin.Y,
				origin.Z
			};
			double[] array6 = new double[]
			{
				direction.X,
				direction.Y,
				direction.Z
			};
			for (int i = 0; i < 3; i++)
			{
				if (array5[i] < array3[i])
				{
					array[i] = false;
					array2[i] = array3[i];
					flag = false;
				}
				else if (array5[i] > array4[i])
				{
					array[i] = false;
					array2[i] = array4[i];
					flag = false;
				}
				else
				{
					array[i] = true;
				}
			}
			if (flag)
			{
				return true;
			}
			double num;
			if (isRay)
			{
				num = -1.0;
			}
			else
			{
				num = 0.0;
			}
			int num2 = 0;
			for (int i = 0; i < 3; i++)
			{
				if (!array[i] && array6[i] != 0.0)
				{
					double num3 = (array2[i] - array5[i]) / array6[i];
					if (isRay)
					{
						if (num3 > num)
						{
							num = num3;
							num2 = i;
						}
					}
					else if (num3 * num3 > num * num)
					{
						num = num3;
						num2 = i;
					}
				}
			}
			if (isRay && num < 0.0)
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (i != num2)
				{
					double num4 = array5[i] + num * array6[i];
					if (num4 < array3[i] || array4[i] < num4)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04002159 RID: 8537
		private static readonly int[,] s_pairs = new int[,]
		{
			{
				0,
				1
			},
			{
				0,
				2
			},
			{
				0,
				3
			},
			{
				1,
				2
			},
			{
				1,
				3
			},
			{
				2,
				3
			}
		};

		// Token: 0x0400215A RID: 8538
		private const int s_pairsCount = 6;

		// Token: 0x020009CD RID: 2509
		private struct JacobiRotation
		{
			// Token: 0x06005AFE RID: 23294 RVA: 0x0016CF6C File Offset: 0x0016C36C
			public JacobiRotation(int p, int q, double[,] a)
			{
				this._p = p;
				this._q = q;
				double num = (a[q, q] - a[p, p]) / (2.0 * a[p, q]);
				if (num < 1.7976931348623157E+308 && num > -1.7976931348623157E+308)
				{
					double num2 = Math.Sqrt(1.0 + num * num);
					double num3 = (-num < 0.0) ? (-num + num2) : (-num - num2);
					this._c = 1.0 / Math.Sqrt(1.0 + num3 * num3);
					this._s = num3 * this._c;
					return;
				}
				this._c = 1.0;
				this._s = 0.0;
			}

			// Token: 0x06005AFF RID: 23295 RVA: 0x0016D040 File Offset: 0x0016C440
			public double[,] LeftRightMultiply(double[,] a)
			{
				return this.RightMultiply(this.LeftMultiplyTranspose(a));
			}

			// Token: 0x06005B00 RID: 23296 RVA: 0x0016D05C File Offset: 0x0016C45C
			public double[,] RightMultiply(double[,] a)
			{
				for (int i = 0; i < 4; i++)
				{
					double num = a[i, this._p];
					double num2 = a[i, this._q];
					a[i, this._p] = this._c * num - this._s * num2;
					a[i, this._q] = this._s * num + this._c * num2;
				}
				return a;
			}

			// Token: 0x06005B01 RID: 23297 RVA: 0x0016D0D0 File Offset: 0x0016C4D0
			public double[,] LeftMultiplyTranspose(double[,] a)
			{
				for (int i = 0; i < 4; i++)
				{
					double num = a[this._p, i];
					double num2 = a[this._q, i];
					a[this._p, i] = this._c * num - this._s * num2;
					a[this._q, i] = this._s * num + this._c * num2;
				}
				return a;
			}

			// Token: 0x04002DFE RID: 11774
			private int _p;

			// Token: 0x04002DFF RID: 11775
			private int _q;

			// Token: 0x04002E00 RID: 11776
			private double _c;

			// Token: 0x04002E01 RID: 11777
			private double _s;
		}
	}
}
