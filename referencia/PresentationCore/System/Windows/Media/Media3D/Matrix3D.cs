using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma matriz 4x4 usada para transformações no espaço 3D.</summary>
	// Token: 0x02000463 RID: 1123
	[ValueSerializer(typeof(Matrix3DValueSerializer))]
	[TypeConverter(typeof(Matrix3DConverter))]
	[Serializable]
	public struct Matrix3D : IFormattable
	{
		/// <summary>Construtor que define os valores iniciais da matriz.</summary>
		/// <param name="m11">Valor do campo (1,1) da nova matriz.</param>
		/// <param name="m12">Valor do campo (1,2) da nova matriz.</param>
		/// <param name="m13">Valor do campo (1,3) da nova matriz.</param>
		/// <param name="m14">Valor do campo (1,4) da nova matriz.</param>
		/// <param name="m21">Valor do campo (2,1) da nova matriz.</param>
		/// <param name="m22">Valor do campo (2,2) da nova matriz.</param>
		/// <param name="m23">Valor do campo (2,3) da nova matriz.</param>
		/// <param name="m24">Valor do campo (2,4) da nova matriz.</param>
		/// <param name="m31">Valor do campo (3,1) da nova matriz.</param>
		/// <param name="m32">Valor do campo (3,2) da nova matriz.</param>
		/// <param name="m33">Valor do campo (3,3) da nova matriz.</param>
		/// <param name="m34">Valor do campo (3,4) da nova matriz.</param>
		/// <param name="offsetX">Valor do campo de deslocamento X da nova matriz.</param>
		/// <param name="offsetY">Valor do campo de deslocamento Y da nova matriz.</param>
		/// <param name="offsetZ">Valor do campo de deslocamento Z da nova matriz.</param>
		/// <param name="m44">Valor do campo (4,4) da nova matriz.</param>
		// Token: 0x06002ED7 RID: 11991 RVA: 0x000BA6C8 File Offset: 0x000B9AC8
		public Matrix3D(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double offsetX, double offsetY, double offsetZ, double m44)
		{
			this._m11 = m11;
			this._m12 = m12;
			this._m13 = m13;
			this._m14 = m14;
			this._m21 = m21;
			this._m22 = m22;
			this._m23 = m23;
			this._m24 = m24;
			this._m31 = m31;
			this._m32 = m32;
			this._m33 = m33;
			this._m34 = m34;
			this._offsetX = offsetX;
			this._offsetY = offsetY;
			this._offsetZ = offsetZ;
			this._m44 = m44;
			this._isNotKnownToBeIdentity = true;
		}

		/// <summary>Altera uma estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> em uma identidade <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>A Matrix3D de identidade.</returns>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000BA75C File Offset: 0x000B9B5C
		public static Matrix3D Identity
		{
			get
			{
				return Matrix3D.s_identity;
			}
		}

		/// <summary>Altera essa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> em uma matriz de identidade.</summary>
		// Token: 0x06002ED9 RID: 11993 RVA: 0x000BA770 File Offset: 0x000B9B70
		public void SetIdentity()
		{
			this = Matrix3D.s_identity;
		}

		/// <summary>Determina se esta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> é uma Matrix3D de identidade.</summary>
		/// <returns>
		///   <see langword="true" /> Se a estrutura Matrix3D é uma identidade Matrix3D; Caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000BA788 File Offset: 0x000B9B88
		public bool IsIdentity
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return true;
				}
				if (this._m11 == 1.0 && this._m12 == 0.0 && this._m13 == 0.0 && this._m14 == 0.0 && this._m21 == 0.0 && this._m22 == 1.0 && this._m23 == 0.0 && this._m24 == 0.0 && this._m31 == 0.0 && this._m32 == 0.0 && this._m33 == 1.0 && this._m34 == 0.0 && this._offsetX == 0.0 && this._offsetY == 0.0 && this._offsetZ == 0.0 && this._m44 == 1.0)
				{
					this.IsDistinguishedIdentity = true;
					return true;
				}
				return false;
			}
		}

		/// <summary>Precede uma matriz especificada na matriz atual.</summary>
		/// <param name="matrix">Matriz a ser precedida.</param>
		// Token: 0x06002EDB RID: 11995 RVA: 0x000BA8D4 File Offset: 0x000B9CD4
		public void Prepend(Matrix3D matrix)
		{
			this = matrix * this;
		}

		/// <summary>Acrescenta uma matriz especificada à matriz atual.</summary>
		/// <param name="matrix">Matriz a ser acrescentada.</param>
		// Token: 0x06002EDC RID: 11996 RVA: 0x000BA8F4 File Offset: 0x000B9CF4
		public void Append(Matrix3D matrix)
		{
			this *= matrix;
		}

		/// <summary>Acrescenta uma transformação de rotação ao <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> atual.</summary>
		/// <param name="quaternion">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a rotação.</param>
		// Token: 0x06002EDD RID: 11997 RVA: 0x000BA914 File Offset: 0x000B9D14
		public void Rotate(Quaternion quaternion)
		{
			Point3D point3D = default(Point3D);
			this *= Matrix3D.CreateRotationMatrix(ref quaternion, ref point3D);
		}

		/// <summary>Precede uma rotação especificada por um <see cref="T:System.Windows.Media.Media3D.Quaternion" /> nesta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="quaternion">Quaternion que representa a rotação.</param>
		// Token: 0x06002EDE RID: 11998 RVA: 0x000BA944 File Offset: 0x000B9D44
		public void RotatePrepend(Quaternion quaternion)
		{
			Point3D point3D = default(Point3D);
			this = Matrix3D.CreateRotationMatrix(ref quaternion, ref point3D) * this;
		}

		/// <summary>Gira este <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> sobre o <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="quaternion">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a rotação.</param>
		/// <param name="center">Centraliza <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre qual deve ser girado.</param>
		// Token: 0x06002EDF RID: 11999 RVA: 0x000BA974 File Offset: 0x000B9D74
		public void RotateAt(Quaternion quaternion, Point3D center)
		{
			this *= Matrix3D.CreateRotationMatrix(ref quaternion, ref center);
		}

		/// <summary>Precede uma rotação sobre um centro especificado <see cref="T:System.Windows.Media.Media3D.Point3D" /> nesta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="quaternion">
		///   <see cref="T:System.Windows.Media.Media3D.Quaternion" /> que representa a rotação.</param>
		/// <param name="center">Centraliza <see cref="T:System.Windows.Media.Media3D.Point3D" /> sobre qual deve ser girado.</param>
		// Token: 0x06002EE0 RID: 12000 RVA: 0x000BA99C File Offset: 0x000B9D9C
		public void RotateAtPrepend(Quaternion quaternion, Point3D center)
		{
			this = Matrix3D.CreateRotationMatrix(ref quaternion, ref center) * this;
		}

		/// <summary>Acrescenta a escala especificada <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a esta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="scale">Vector3D pelo qual dimensionar esta estrutura Matrix3D.</param>
		// Token: 0x06002EE1 RID: 12001 RVA: 0x000BA9C4 File Offset: 0x000B9DC4
		public void Scale(Vector3D scale)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetScaleMatrix(ref scale);
				return;
			}
			this._m11 *= scale.X;
			this._m12 *= scale.Y;
			this._m13 *= scale.Z;
			this._m21 *= scale.X;
			this._m22 *= scale.Y;
			this._m23 *= scale.Z;
			this._m31 *= scale.X;
			this._m32 *= scale.Y;
			this._m33 *= scale.Z;
			this._offsetX *= scale.X;
			this._offsetY *= scale.Y;
			this._offsetZ *= scale.Z;
		}

		/// <summary>Precede a escala <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificada na estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> atual.</summary>
		/// <param name="scale">Vector3D pelo qual dimensionar esta estrutura Matrix3D.</param>
		// Token: 0x06002EE2 RID: 12002 RVA: 0x000BAAD4 File Offset: 0x000B9ED4
		public void ScalePrepend(Vector3D scale)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetScaleMatrix(ref scale);
				return;
			}
			this._m11 *= scale.X;
			this._m12 *= scale.X;
			this._m13 *= scale.X;
			this._m14 *= scale.X;
			this._m21 *= scale.Y;
			this._m22 *= scale.Y;
			this._m23 *= scale.Y;
			this._m24 *= scale.Y;
			this._m31 *= scale.Z;
			this._m32 *= scale.Z;
			this._m33 *= scale.Z;
			this._m34 *= scale.Z;
		}

		/// <summary>Dimensiona essa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> pelo <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado sobre o <see cref="T:System.Windows.Media.Media3D.Point3D" /> indicado.</summary>
		/// <param name="scale">Vector3D pelo qual dimensionar esta estrutura Matrix3D.</param>
		/// <param name="center">Point3D sobre o qual escalonar.</param>
		// Token: 0x06002EE3 RID: 12003 RVA: 0x000BABE4 File Offset: 0x000B9FE4
		public void ScaleAt(Vector3D scale, Point3D center)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetScaleMatrix(ref scale, ref center);
				return;
			}
			double num = this._m14 * center.X;
			this._m11 = num + scale.X * (this._m11 - num);
			num = this._m14 * center.Y;
			this._m12 = num + scale.Y * (this._m12 - num);
			num = this._m14 * center.Z;
			this._m13 = num + scale.Z * (this._m13 - num);
			num = this._m24 * center.X;
			this._m21 = num + scale.X * (this._m21 - num);
			num = this._m24 * center.Y;
			this._m22 = num + scale.Y * (this._m22 - num);
			num = this._m24 * center.Z;
			this._m23 = num + scale.Z * (this._m23 - num);
			num = this._m34 * center.X;
			this._m31 = num + scale.X * (this._m31 - num);
			num = this._m34 * center.Y;
			this._m32 = num + scale.Y * (this._m32 - num);
			num = this._m34 * center.Z;
			this._m33 = num + scale.Z * (this._m33 - num);
			num = this._m44 * center.X;
			this._offsetX = num + scale.X * (this._offsetX - num);
			num = this._m44 * center.Y;
			this._offsetY = num + scale.Y * (this._offsetY - num);
			num = this._m44 * center.Z;
			this._offsetZ = num + scale.Z * (this._offsetZ - num);
		}

		/// <summary>Precede a transformação de escala especificada sobre o <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado nesta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="scale">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> pelo qual dimensionar esta estrutura Matrix3D.</param>
		/// <param name="center">Point3D sobre o qual escalonar.</param>
		// Token: 0x06002EE4 RID: 12004 RVA: 0x000BADD8 File Offset: 0x000BA1D8
		public void ScaleAtPrepend(Vector3D scale, Point3D center)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetScaleMatrix(ref scale, ref center);
				return;
			}
			double num = center.X - center.X * scale.X;
			double num2 = center.Y - center.Y * scale.Y;
			double num3 = center.Z - center.Z * scale.Z;
			this._offsetX += this._m11 * num + this._m21 * num2 + this._m31 * num3;
			this._offsetY += this._m12 * num + this._m22 * num2 + this._m32 * num3;
			this._offsetZ += this._m13 * num + this._m23 * num2 + this._m33 * num3;
			this._m44 += this._m14 * num + this._m24 * num2 + this._m34 * num3;
			this._m11 *= scale.X;
			this._m12 *= scale.X;
			this._m13 *= scale.X;
			this._m14 *= scale.X;
			this._m21 *= scale.Y;
			this._m22 *= scale.Y;
			this._m23 *= scale.Y;
			this._m24 *= scale.Y;
			this._m31 *= scale.Z;
			this._m32 *= scale.Z;
			this._m33 *= scale.Z;
			this._m34 *= scale.Z;
		}

		/// <summary>Acrescenta uma translação ao deslocamento especificado à estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> atual.</summary>
		/// <param name="offset">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica o deslocamento para a transformação.</param>
		// Token: 0x06002EE5 RID: 12005 RVA: 0x000BAFCC File Offset: 0x000BA3CC
		public void Translate(Vector3D offset)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetTranslationMatrix(ref offset);
				return;
			}
			this._m11 += this._m14 * offset.X;
			this._m12 += this._m14 * offset.Y;
			this._m13 += this._m14 * offset.Z;
			this._m21 += this._m24 * offset.X;
			this._m22 += this._m24 * offset.Y;
			this._m23 += this._m24 * offset.Z;
			this._m31 += this._m34 * offset.X;
			this._m32 += this._m34 * offset.Y;
			this._m33 += this._m34 * offset.Z;
			this._offsetX += this._m44 * offset.X;
			this._offsetY += this._m44 * offset.Y;
			this._offsetZ += this._m44 * offset.Z;
		}

		/// <summary>Precede uma translação do deslocamento especificado nesta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="offset">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica o deslocamento para a transformação.</param>
		// Token: 0x06002EE6 RID: 12006 RVA: 0x000BB130 File Offset: 0x000BA530
		public void TranslatePrepend(Vector3D offset)
		{
			if (this.IsDistinguishedIdentity)
			{
				this.SetTranslationMatrix(ref offset);
				return;
			}
			this._offsetX += this._m11 * offset.X + this._m21 * offset.Y + this._m31 * offset.Z;
			this._offsetY += this._m12 * offset.X + this._m22 * offset.Y + this._m32 * offset.Z;
			this._offsetZ += this._m13 * offset.X + this._m23 * offset.Y + this._m33 * offset.Z;
			this._m44 += this._m14 * offset.X + this._m24 * offset.Y + this._m34 * offset.Z;
		}

		/// <summary>Multiplica as matrizes especificadas.</summary>
		/// <param name="matrix1">Matriz a ser multiplicada.</param>
		/// <param name="matrix2">Matriz pelo qual a primeira matriz é multiplicada.</param>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que é o resultado da multiplicação.</returns>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000BB234 File Offset: 0x000BA634
		public static Matrix3D operator *(Matrix3D matrix1, Matrix3D matrix2)
		{
			if (matrix1.IsDistinguishedIdentity)
			{
				return matrix2;
			}
			if (matrix2.IsDistinguishedIdentity)
			{
				return matrix1;
			}
			Matrix3D result = new Matrix3D(matrix1._m11 * matrix2._m11 + matrix1._m12 * matrix2._m21 + matrix1._m13 * matrix2._m31 + matrix1._m14 * matrix2._offsetX, matrix1._m11 * matrix2._m12 + matrix1._m12 * matrix2._m22 + matrix1._m13 * matrix2._m32 + matrix1._m14 * matrix2._offsetY, matrix1._m11 * matrix2._m13 + matrix1._m12 * matrix2._m23 + matrix1._m13 * matrix2._m33 + matrix1._m14 * matrix2._offsetZ, matrix1._m11 * matrix2._m14 + matrix1._m12 * matrix2._m24 + matrix1._m13 * matrix2._m34 + matrix1._m14 * matrix2._m44, matrix1._m21 * matrix2._m11 + matrix1._m22 * matrix2._m21 + matrix1._m23 * matrix2._m31 + matrix1._m24 * matrix2._offsetX, matrix1._m21 * matrix2._m12 + matrix1._m22 * matrix2._m22 + matrix1._m23 * matrix2._m32 + matrix1._m24 * matrix2._offsetY, matrix1._m21 * matrix2._m13 + matrix1._m22 * matrix2._m23 + matrix1._m23 * matrix2._m33 + matrix1._m24 * matrix2._offsetZ, matrix1._m21 * matrix2._m14 + matrix1._m22 * matrix2._m24 + matrix1._m23 * matrix2._m34 + matrix1._m24 * matrix2._m44, matrix1._m31 * matrix2._m11 + matrix1._m32 * matrix2._m21 + matrix1._m33 * matrix2._m31 + matrix1._m34 * matrix2._offsetX, matrix1._m31 * matrix2._m12 + matrix1._m32 * matrix2._m22 + matrix1._m33 * matrix2._m32 + matrix1._m34 * matrix2._offsetY, matrix1._m31 * matrix2._m13 + matrix1._m32 * matrix2._m23 + matrix1._m33 * matrix2._m33 + matrix1._m34 * matrix2._offsetZ, matrix1._m31 * matrix2._m14 + matrix1._m32 * matrix2._m24 + matrix1._m33 * matrix2._m34 + matrix1._m34 * matrix2._m44, matrix1._offsetX * matrix2._m11 + matrix1._offsetY * matrix2._m21 + matrix1._offsetZ * matrix2._m31 + matrix1._m44 * matrix2._offsetX, matrix1._offsetX * matrix2._m12 + matrix1._offsetY * matrix2._m22 + matrix1._offsetZ * matrix2._m32 + matrix1._m44 * matrix2._offsetY, matrix1._offsetX * matrix2._m13 + matrix1._offsetY * matrix2._m23 + matrix1._offsetZ * matrix2._m33 + matrix1._m44 * matrix2._offsetZ, matrix1._offsetX * matrix2._m14 + matrix1._offsetY * matrix2._m24 + matrix1._offsetZ * matrix2._m34 + matrix1._m44 * matrix2._m44);
			return result;
		}

		/// <summary>Multiplica as matrizes especificadas.</summary>
		/// <param name="matrix1">Matriz a ser multiplicada.</param>
		/// <param name="matrix2">Matriz pelo qual a primeira matriz é multiplicada.</param>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que é o resultado da multiplicação.</returns>
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000BB5D0 File Offset: 0x000BA9D0
		public static Matrix3D Multiply(Matrix3D matrix1, Matrix3D matrix2)
		{
			return matrix1 * matrix2;
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado pelo <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> e retorna o resultado.</summary>
		/// <param name="point">O Point3D a ser transformado.</param>
		/// <returns>O resultado da transformação de <paramref name="point" /> por este Matrix3D.</returns>
		/// <exception cref="T:System.InvalidOperationException">Gera InvalidOperationException se a transformação não for afim.</exception>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000BB5E4 File Offset: 0x000BA9E4
		public Point3D Transform(Point3D point)
		{
			this.MultiplyPoint(ref point);
			return point;
		}

		/// <summary>Transforma os objetos <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificados na matriz pelo <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="points">Objetos Point3D a serem transformados. Os pontos originais na matriz são substituídos por seus valores transformados.</param>
		/// <exception cref="T:System.InvalidOperationException">Gera InvalidOperationException se a transformação não for afim.</exception>
		// Token: 0x06002EEA RID: 12010 RVA: 0x000BB5FC File Offset: 0x000BA9FC
		public void Transform(Point3D[] points)
		{
			if (points != null)
			{
				for (int i = 0; i < points.Length; i++)
				{
					this.MultiplyPoint(ref points[i]);
				}
			}
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Point4D" /> especificado pelo <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> e retorna o resultado.</summary>
		/// <param name="point">
		///   <see cref="T:System.Windows.Media.Media3D.Point4D" /> para transformar.</param>
		/// <returns>O resultado da transformação de <paramref name="point" /> por este Matrix3D.</returns>
		// Token: 0x06002EEB RID: 12011 RVA: 0x000BB628 File Offset: 0x000BAA28
		public Point4D Transform(Point4D point)
		{
			this.MultiplyPoint(ref point);
			return point;
		}

		/// <summary>Transforma os objetos <see cref="T:System.Windows.Media.Media3D.Point4D" /> especificados na matriz pelo <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> e retorna o resultado.</summary>
		/// <param name="points">Objetos <see cref="T:System.Windows.Media.Media3D.Point4D" /> a serem transformados. Os pontos originais na matriz são substituídos por seus valores transformados.</param>
		// Token: 0x06002EEC RID: 12012 RVA: 0x000BB640 File Offset: 0x000BAA40
		public void Transform(Point4D[] points)
		{
			if (points != null)
			{
				for (int i = 0; i < points.Length; i++)
				{
					this.MultiplyPoint(ref points[i]);
				}
			}
		}

		/// <summary>Transforma o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado por este <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="vector">
		///   <see cref="T:System.Windows.Media.Media3D.Vector3D" /> para transformar.</param>
		/// <returns>O resultado da transformação de <paramref name="vector" /> por este Matrix3D.</returns>
		// Token: 0x06002EED RID: 12013 RVA: 0x000BB66C File Offset: 0x000BAA6C
		public Vector3D Transform(Vector3D vector)
		{
			this.MultiplyVector(ref vector);
			return vector;
		}

		/// <summary>Transforma os objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificados na matriz por este <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="vectors">Objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a serem transformados. Os objetos Vector3D originais na matriz são substituídos por seus valores transformados.</param>
		// Token: 0x06002EEE RID: 12014 RVA: 0x000BB684 File Offset: 0x000BAA84
		public void Transform(Vector3D[] vectors)
		{
			if (vectors != null)
			{
				for (int i = 0; i < vectors.Length; i++)
				{
					this.MultiplyVector(ref vectors[i]);
				}
			}
		}

		/// <summary>Obtém um valor que indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> é afim.</summary>
		/// <returns>
		///   <see langword="true" /> Se a estrutura Matrix3D é afim; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000BB6B0 File Offset: 0x000BAAB0
		public bool IsAffine
		{
			get
			{
				return this.IsDistinguishedIdentity || (this._m14 == 0.0 && this._m24 == 0.0 && this._m34 == 0.0 && this._m44 == 1.0);
			}
		}

		/// <summary>Recupera o determinante dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O determinante dessa <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000BB710 File Offset: 0x000BAB10
		public double Determinant
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				if (this.IsAffine)
				{
					return this.GetNormalizedAffineDeterminant();
				}
				double num = this._m13 * this._m24 - this._m23 * this._m14;
				double num2 = this._m13 * this._m34 - this._m33 * this._m14;
				double num3 = this._m13 * this._m44 - this._offsetZ * this._m14;
				double num4 = this._m23 * this._m34 - this._m33 * this._m24;
				double num5 = this._m23 * this._m44 - this._offsetZ * this._m24;
				double num6 = this._m33 * this._m44 - this._offsetZ * this._m34;
				double num7 = this._m22 * num2 - this._m32 * num - this._m12 * num4;
				double num8 = this._m12 * num5 - this._m22 * num3 + this._offsetY * num;
				double num9 = this._m32 * num3 - this._offsetY * num2 - this._m12 * num6;
				double num10 = this._m22 * num6 - this._m32 * num5 + this._offsetY * num4;
				return this._offsetX * num7 + this._m31 * num8 + this._m21 * num9 + this._m11 * num10;
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> pode ser invertido.</summary>
		/// <returns>
		///   <see langword="true" /> Se a estrutura Matrix3D possui um inverso; Caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000BB884 File Offset: 0x000BAC84
		public bool HasInverse
		{
			get
			{
				return !DoubleUtil.IsZero(this.Determinant);
			}
		}

		/// <summary>Inverte essa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">Gera InvalidOperationException se a matriz não for invertível.</exception>
		// Token: 0x06002EF2 RID: 12018 RVA: 0x000BB8A0 File Offset: 0x000BACA0
		public void Invert()
		{
			if (!this.InvertCore())
			{
				throw new InvalidOperationException(SR.Get("Matrix3D_NotInvertible", null));
			}
		}

		/// <summary>Obtém ou define o valor da primeira linha e primeira coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da primeira linha e primeira coluna dessa <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000BB8C8 File Offset: 0x000BACC8
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000BB8F0 File Offset: 0x000BACF0
		public double M11
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				return this._m11;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m11 = value;
			}
		}

		/// <summary>Obtém ou define o valor da primeira linha e segunda coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da primeira linha e segunda coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000BB920 File Offset: 0x000BAD20
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000BB934 File Offset: 0x000BAD34
		public double M12
		{
			get
			{
				return this._m12;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m12 = value;
			}
		}

		/// <summary>Obtém ou define o valor da primeira linha e terceira coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da primeira linha e terceira coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000BB964 File Offset: 0x000BAD64
		// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x000BB978 File Offset: 0x000BAD78
		public double M13
		{
			get
			{
				return this._m13;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m13 = value;
			}
		}

		/// <summary>Obtém ou define o valor da primeira linha e quarta coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da primeira linha e quarta coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000BB9A8 File Offset: 0x000BADA8
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000BB9BC File Offset: 0x000BADBC
		public double M14
		{
			get
			{
				return this._m14;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m14 = value;
			}
		}

		/// <summary>Obtém ou define o valor da segunda linha e da primeira coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da segunda linha e primeira coluna dessa <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000BB9EC File Offset: 0x000BADEC
		// (set) Token: 0x06002EFC RID: 12028 RVA: 0x000BBA00 File Offset: 0x000BAE00
		public double M21
		{
			get
			{
				return this._m21;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m21 = value;
			}
		}

		/// <summary>Obtém ou define o valor da segunda linha e da segunda coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da segunda linha e na segunda coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000BBA30 File Offset: 0x000BAE30
		// (set) Token: 0x06002EFE RID: 12030 RVA: 0x000BBA58 File Offset: 0x000BAE58
		public double M22
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				return this._m22;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m22 = value;
			}
		}

		/// <summary>Obtém ou define o valor da segunda linha e terceira coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da segunda linha e terceira coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000BBA88 File Offset: 0x000BAE88
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x000BBA9C File Offset: 0x000BAE9C
		public double M23
		{
			get
			{
				return this._m23;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m23 = value;
			}
		}

		/// <summary>Obtém ou define o valor da segunda linha e quarta coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da segunda linha e quarta coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x000BBACC File Offset: 0x000BAECC
		// (set) Token: 0x06002F02 RID: 12034 RVA: 0x000BBAE0 File Offset: 0x000BAEE0
		public double M24
		{
			get
			{
				return this._m24;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m24 = value;
			}
		}

		/// <summary>Obtém ou define o valor da terceira linha e da primeira coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da terceira linha e da primeira coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</returns>
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000BBB10 File Offset: 0x000BAF10
		// (set) Token: 0x06002F04 RID: 12036 RVA: 0x000BBB24 File Offset: 0x000BAF24
		public double M31
		{
			get
			{
				return this._m31;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m31 = value;
			}
		}

		/// <summary>Obtém ou define o valor da terceira linha e da segunda coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da terceira linha e na segunda coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000BBB54 File Offset: 0x000BAF54
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000BBB68 File Offset: 0x000BAF68
		public double M32
		{
			get
			{
				return this._m32;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m32 = value;
			}
		}

		/// <summary>Obtém ou define o valor da terceira linha e terceira coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da terceira linha e terceira coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000BBB98 File Offset: 0x000BAF98
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000BBBC0 File Offset: 0x000BAFC0
		public double M33
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				return this._m33;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m33 = value;
			}
		}

		/// <summary>Obtém ou define o valor da terceira linha e quarta coluna desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da terceira linha e quarta coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000BBBF0 File Offset: 0x000BAFF0
		// (set) Token: 0x06002F0A RID: 12042 RVA: 0x000BBC04 File Offset: 0x000BB004
		public double M34
		{
			get
			{
				return this._m34;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m34 = value;
			}
		}

		/// <summary>Obtém ou define o valor da quarta linha e primeira coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da quarta linha e primeira coluna dessa <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000BBC34 File Offset: 0x000BB034
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000BBC48 File Offset: 0x000BB048
		public double OffsetX
		{
			get
			{
				return this._offsetX;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._offsetX = value;
			}
		}

		/// <summary>Obtém ou define o valor da quarta linha e segunda coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da quarta linha e na segunda coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000BBC78 File Offset: 0x000BB078
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000BBC8C File Offset: 0x000BB08C
		public double OffsetY
		{
			get
			{
				return this._offsetY;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._offsetY = value;
			}
		}

		/// <summary>Obtém ou define o valor da quarta linha e terceira coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da quarta linha e terceira coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000BBCBC File Offset: 0x000BB0BC
		// (set) Token: 0x06002F10 RID: 12048 RVA: 0x000BBCD0 File Offset: 0x000BB0D0
		public double OffsetZ
		{
			get
			{
				return this._offsetZ;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._offsetZ = value;
			}
		}

		/// <summary>Obtém ou define o valor da quarta linha e quarta coluna dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>O valor da quarta linha e quarta coluna deste <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> estrutura.</returns>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000BBD00 File Offset: 0x000BB100
		// (set) Token: 0x06002F12 RID: 12050 RVA: 0x000BBD28 File Offset: 0x000BB128
		public double M44
		{
			get
			{
				if (this.IsDistinguishedIdentity)
				{
					return 1.0;
				}
				return this._m44;
			}
			set
			{
				if (this.IsDistinguishedIdentity)
				{
					this = Matrix3D.s_identity;
					this.IsDistinguishedIdentity = false;
				}
				this._m44 = value;
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000BBD58 File Offset: 0x000BB158
		internal void SetScaleMatrix(ref Vector3D scale)
		{
			this._m11 = scale.X;
			this._m22 = scale.Y;
			this._m33 = scale.Z;
			this._m44 = 1.0;
			this.IsDistinguishedIdentity = false;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000BBDA0 File Offset: 0x000BB1A0
		internal void SetScaleMatrix(ref Vector3D scale, ref Point3D center)
		{
			this._m11 = scale.X;
			this._m22 = scale.Y;
			this._m33 = scale.Z;
			this._m44 = 1.0;
			this._offsetX = center.X - center.X * scale.X;
			this._offsetY = center.Y - center.Y * scale.Y;
			this._offsetZ = center.Z - center.Z * scale.Z;
			this.IsDistinguishedIdentity = false;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000BBE38 File Offset: 0x000BB238
		internal void SetTranslationMatrix(ref Vector3D offset)
		{
			this._m11 = (this._m22 = (this._m33 = (this._m44 = 1.0)));
			this._offsetX = offset.X;
			this._offsetY = offset.Y;
			this._offsetZ = offset.Z;
			this.IsDistinguishedIdentity = false;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000BBE9C File Offset: 0x000BB29C
		internal static Matrix3D CreateRotationMatrix(ref Quaternion quaternion, ref Point3D center)
		{
			Matrix3D matrix3D = Matrix3D.s_identity;
			matrix3D.IsDistinguishedIdentity = false;
			double num = quaternion.X + quaternion.X;
			double num2 = quaternion.Y + quaternion.Y;
			double num3 = quaternion.Z + quaternion.Z;
			double num4 = quaternion.X * num;
			double num5 = quaternion.X * num2;
			double num6 = quaternion.X * num3;
			double num7 = quaternion.Y * num2;
			double num8 = quaternion.Y * num3;
			double num9 = quaternion.Z * num3;
			double num10 = quaternion.W * num;
			double num11 = quaternion.W * num2;
			double num12 = quaternion.W * num3;
			matrix3D._m11 = 1.0 - (num7 + num9);
			matrix3D._m12 = num5 + num12;
			matrix3D._m13 = num6 - num11;
			matrix3D._m21 = num5 - num12;
			matrix3D._m22 = 1.0 - (num4 + num9);
			matrix3D._m23 = num8 + num10;
			matrix3D._m31 = num6 + num11;
			matrix3D._m32 = num8 - num10;
			matrix3D._m33 = 1.0 - (num4 + num7);
			if (center.X != 0.0 || center.Y != 0.0 || center.Z != 0.0)
			{
				matrix3D._offsetX = -center.X * matrix3D._m11 - center.Y * matrix3D._m21 - center.Z * matrix3D._m31 + center.X;
				matrix3D._offsetY = -center.X * matrix3D._m12 - center.Y * matrix3D._m22 - center.Z * matrix3D._m32 + center.Y;
				matrix3D._offsetZ = -center.X * matrix3D._m13 - center.Y * matrix3D._m23 - center.Z * matrix3D._m33 + center.Z;
			}
			return matrix3D;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000BC0A4 File Offset: 0x000BB4A4
		internal void MultiplyPoint(ref Point3D point)
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			double x = point.X;
			double y = point.Y;
			double z = point.Z;
			point.X = x * this._m11 + y * this._m21 + z * this._m31 + this._offsetX;
			point.Y = x * this._m12 + y * this._m22 + z * this._m32 + this._offsetY;
			point.Z = x * this._m13 + y * this._m23 + z * this._m33 + this._offsetZ;
			if (!this.IsAffine)
			{
				double num = x * this._m14 + y * this._m24 + z * this._m34 + this._m44;
				point.X /= num;
				point.Y /= num;
				point.Z /= num;
			}
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000BC198 File Offset: 0x000BB598
		internal void MultiplyPoint(ref Point4D point)
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			double x = point.X;
			double y = point.Y;
			double z = point.Z;
			double w = point.W;
			point.X = x * this._m11 + y * this._m21 + z * this._m31 + w * this._offsetX;
			point.Y = x * this._m12 + y * this._m22 + z * this._m32 + w * this._offsetY;
			point.Z = x * this._m13 + y * this._m23 + z * this._m33 + w * this._offsetZ;
			point.W = x * this._m14 + y * this._m24 + z * this._m34 + w * this._m44;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000BC270 File Offset: 0x000BB670
		internal void MultiplyVector(ref Vector3D vector)
		{
			if (this.IsDistinguishedIdentity)
			{
				return;
			}
			double x = vector.X;
			double y = vector.Y;
			double z = vector.Z;
			vector.X = x * this._m11 + y * this._m21 + z * this._m31;
			vector.Y = x * this._m12 + y * this._m22 + z * this._m32;
			vector.Z = x * this._m13 + y * this._m23 + z * this._m33;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000BC2FC File Offset: 0x000BB6FC
		internal double GetNormalizedAffineDeterminant()
		{
			double num = this._m12 * this._m23 - this._m22 * this._m13;
			double num2 = this._m32 * this._m13 - this._m12 * this._m33;
			double num3 = this._m22 * this._m33 - this._m32 * this._m23;
			return this._m31 * num + this._m21 * num2 + this._m11 * num3;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000BC378 File Offset: 0x000BB778
		internal bool NormalizedAffineInvert()
		{
			double num = this._m12 * this._m23 - this._m22 * this._m13;
			double num2 = this._m32 * this._m13 - this._m12 * this._m33;
			double num3 = this._m22 * this._m33 - this._m32 * this._m23;
			double num4 = this._m31 * num + this._m21 * num2 + this._m11 * num3;
			if (DoubleUtil.IsZero(num4))
			{
				return false;
			}
			double num5 = this._m21 * this._m13 - this._m11 * this._m23;
			double num6 = this._m11 * this._m33 - this._m31 * this._m13;
			double num7 = this._m31 * this._m23 - this._m21 * this._m33;
			double num8 = this._m11 * this._m22 - this._m21 * this._m12;
			double num9 = this._m11 * this._m32 - this._m31 * this._m12;
			double num10 = this._m11 * this._offsetY - this._offsetX * this._m12;
			double num11 = this._m21 * this._m32 - this._m31 * this._m22;
			double num12 = this._m21 * this._offsetY - this._offsetX * this._m22;
			double num13 = this._m31 * this._offsetY - this._offsetX * this._m32;
			double num14 = this._m23 * num10 - this._offsetZ * num8 - this._m13 * num12;
			double num15 = this._m13 * num13 - this._m33 * num10 + this._offsetZ * num9;
			double num16 = this._m33 * num12 - this._offsetZ * num11 - this._m23 * num13;
			double num17 = num8;
			double num18 = -num9;
			double num19 = num11;
			double num20 = 1.0 / num4;
			this._m11 = num3 * num20;
			this._m12 = num2 * num20;
			this._m13 = num * num20;
			this._m21 = num7 * num20;
			this._m22 = num6 * num20;
			this._m23 = num5 * num20;
			this._m31 = num19 * num20;
			this._m32 = num18 * num20;
			this._m33 = num17 * num20;
			this._offsetX = num16 * num20;
			this._offsetY = num15 * num20;
			this._offsetZ = num14 * num20;
			return true;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000BC5F4 File Offset: 0x000BB9F4
		internal bool InvertCore()
		{
			if (this.IsDistinguishedIdentity)
			{
				return true;
			}
			if (this.IsAffine)
			{
				return this.NormalizedAffineInvert();
			}
			double num = this._m13 * this._m24 - this._m23 * this._m14;
			double num2 = this._m13 * this._m34 - this._m33 * this._m14;
			double num3 = this._m13 * this._m44 - this._offsetZ * this._m14;
			double num4 = this._m23 * this._m34 - this._m33 * this._m24;
			double num5 = this._m23 * this._m44 - this._offsetZ * this._m24;
			double num6 = this._m33 * this._m44 - this._offsetZ * this._m34;
			double num7 = this._m22 * num2 - this._m32 * num - this._m12 * num4;
			double num8 = this._m12 * num5 - this._m22 * num3 + this._offsetY * num;
			double num9 = this._m32 * num3 - this._offsetY * num2 - this._m12 * num6;
			double num10 = this._m22 * num6 - this._m32 * num5 + this._offsetY * num4;
			double num11 = this._offsetX * num7 + this._m31 * num8 + this._m21 * num9 + this._m11 * num10;
			if (DoubleUtil.IsZero(num11))
			{
				return false;
			}
			double num12 = this._m11 * num4 - this._m21 * num2 + this._m31 * num;
			double num13 = this._m21 * num3 - this._offsetX * num - this._m11 * num5;
			double num14 = this._m11 * num6 - this._m31 * num3 + this._offsetX * num2;
			double num15 = this._m31 * num5 - this._offsetX * num4 - this._m21 * num6;
			num = this._m11 * this._m22 - this._m21 * this._m12;
			num2 = this._m11 * this._m32 - this._m31 * this._m12;
			num3 = this._m11 * this._offsetY - this._offsetX * this._m12;
			num4 = this._m21 * this._m32 - this._m31 * this._m22;
			num5 = this._m21 * this._offsetY - this._offsetX * this._m22;
			num6 = this._m31 * this._offsetY - this._offsetX * this._m32;
			double num16 = this._m13 * num4 - this._m23 * num2 + this._m33 * num;
			double num17 = this._m23 * num3 - this._offsetZ * num - this._m13 * num5;
			double num18 = this._m13 * num6 - this._m33 * num3 + this._offsetZ * num2;
			double num19 = this._m33 * num5 - this._offsetZ * num4 - this._m23 * num6;
			double num20 = this._m24 * num2 - this._m34 * num - this._m14 * num4;
			double num21 = this._m14 * num5 - this._m24 * num3 + this._m44 * num;
			double num22 = this._m34 * num3 - this._m44 * num2 - this._m14 * num6;
			double num23 = this._m24 * num6 - this._m34 * num5 + this._m44 * num4;
			double num24 = 1.0 / num11;
			this._m11 = num10 * num24;
			this._m12 = num9 * num24;
			this._m13 = num8 * num24;
			this._m14 = num7 * num24;
			this._m21 = num15 * num24;
			this._m22 = num14 * num24;
			this._m23 = num13 * num24;
			this._m24 = num12 * num24;
			this._m31 = num23 * num24;
			this._m32 = num22 * num24;
			this._m33 = num21 * num24;
			this._m34 = num20 * num24;
			this._offsetX = num19 * num24;
			this._offsetY = num18 * num24;
			this._offsetZ = num17 * num24;
			this._m44 = num16 * num24;
			return true;
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000BCA2C File Offset: 0x000BBE2C
		private static Matrix3D CreateIdentity()
		{
			return new Matrix3D(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0)
			{
				IsDistinguishedIdentity = true
			};
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000BCADC File Offset: 0x000BBEDC
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x000BCAF4 File Offset: 0x000BBEF4
		private bool IsDistinguishedIdentity
		{
			get
			{
				return !this._isNotKnownToBeIdentity;
			}
			set
			{
				this._isNotKnownToBeIdentity = !value;
			}
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> quanto à igualdade exata.</summary>
		/// <param name="matrix1">A primeira matriz a comparar.</param>
		/// <param name="matrix2">A segunda matriz a ser comparada.</param>
		/// <returns>
		///   <see cref="T:System.Boolean" /> que indica se duas instâncias de matrizes são iguais.</returns>
		// Token: 0x06002F20 RID: 12064 RVA: 0x000BCB0C File Offset: 0x000BBF0C
		public static bool operator ==(Matrix3D matrix1, Matrix3D matrix2)
		{
			if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
			{
				return matrix1.IsIdentity == matrix2.IsIdentity;
			}
			return matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 && matrix1.M14 == matrix2.M14 && matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22 && matrix1.M23 == matrix2.M23 && matrix1.M24 == matrix2.M24 && matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32 && matrix1.M33 == matrix2.M33 && matrix1.M34 == matrix2.M34 && matrix1.OffsetX == matrix2.OffsetX && matrix1.OffsetY == matrix2.OffsetY && matrix1.OffsetZ == matrix2.OffsetZ && matrix1.M44 == matrix2.M44;
		}

		/// <summary>Compara duas instâncias <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> quanto à desigualdade exata.</summary>
		/// <param name="matrix1">A primeira matriz a comparar.</param>
		/// <param name="matrix2">A segunda matriz a ser comparada.</param>
		/// <returns>
		///   <see cref="T:System.Boolean" /> que indica se duas instâncias de matrizes são diferentes.</returns>
		// Token: 0x06002F21 RID: 12065 RVA: 0x000BCC58 File Offset: 0x000BC058
		public static bool operator !=(Matrix3D matrix1, Matrix3D matrix2)
		{
			return !(matrix1 == matrix2);
		}

		/// <summary>Testa a igualdade entre duas matrizes.</summary>
		/// <param name="matrix1">O primeiro Matrix3D a ser comparado.</param>
		/// <param name="matrix2">O segundo Matrix3D a ser comparado.</param>
		/// <returns>
		///   <see cref="T:System.Boolean" /> que indica se as matrizes são iguais.</returns>
		// Token: 0x06002F22 RID: 12066 RVA: 0x000BCC70 File Offset: 0x000BC070
		public static bool Equals(Matrix3D matrix1, Matrix3D matrix2)
		{
			if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
			{
				return matrix1.IsIdentity == matrix2.IsIdentity;
			}
			return matrix1.M11.Equals(matrix2.M11) && matrix1.M12.Equals(matrix2.M12) && matrix1.M13.Equals(matrix2.M13) && matrix1.M14.Equals(matrix2.M14) && matrix1.M21.Equals(matrix2.M21) && matrix1.M22.Equals(matrix2.M22) && matrix1.M23.Equals(matrix2.M23) && matrix1.M24.Equals(matrix2.M24) && matrix1.M31.Equals(matrix2.M31) && matrix1.M32.Equals(matrix2.M32) && matrix1.M33.Equals(matrix2.M33) && matrix1.M34.Equals(matrix2.M34) && matrix1.OffsetX.Equals(matrix2.OffsetX) && matrix1.OffsetY.Equals(matrix2.OffsetY) && matrix1.OffsetZ.Equals(matrix2.OffsetZ) && matrix1.M44.Equals(matrix2.M44);
		}

		/// <summary>Testa a igualdade entre duas matrizes.</summary>
		/// <param name="o">Objeto a ser testado quanto à igualdade.</param>
		/// <returns>
		///   <see cref="T:System.Boolean" /> que indica se as matrizes são iguais.</returns>
		// Token: 0x06002F23 RID: 12067 RVA: 0x000BCE44 File Offset: 0x000BC244
		public override bool Equals(object o)
		{
			if (o == null || !(o is Matrix3D))
			{
				return false;
			}
			Matrix3D matrix = (Matrix3D)o;
			return Matrix3D.Equals(this, matrix);
		}

		/// <summary>Testa a igualdade entre duas matrizes.</summary>
		/// <param name="value">O Matrix3D com o qual comparar.</param>
		/// <returns>
		///   <see cref="T:System.Boolean" /> que indica se as matrizes são iguais.</returns>
		// Token: 0x06002F24 RID: 12068 RVA: 0x000BCE74 File Offset: 0x000BC274
		public bool Equals(Matrix3D value)
		{
			return Matrix3D.Equals(this, value);
		}

		/// <summary>Retorna o código hash desta matriz</summary>
		/// <returns>Inteiro que especifica o código hash para esta matriz.</returns>
		// Token: 0x06002F25 RID: 12069 RVA: 0x000BCE90 File Offset: 0x000BC290
		public override int GetHashCode()
		{
			if (this.IsDistinguishedIdentity)
			{
				return 0;
			}
			return this.M11.GetHashCode() ^ this.M12.GetHashCode() ^ this.M13.GetHashCode() ^ this.M14.GetHashCode() ^ this.M21.GetHashCode() ^ this.M22.GetHashCode() ^ this.M23.GetHashCode() ^ this.M24.GetHashCode() ^ this.M31.GetHashCode() ^ this.M32.GetHashCode() ^ this.M33.GetHashCode() ^ this.M34.GetHashCode() ^ this.OffsetX.GetHashCode() ^ this.OffsetY.GetHashCode() ^ this.OffsetZ.GetHashCode() ^ this.M44.GetHashCode();
		}

		/// <summary>Converte uma representação de cadeia de caracteres de uma estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> na estrutura Matrix3D equivalente.</summary>
		/// <param name="source">Representação de cadeia de caracteres do Matrix3D.</param>
		/// <returns>Estrutura Matrix3D representada pela cadeia de caracteres.</returns>
		// Token: 0x06002F26 RID: 12070 RVA: 0x000BCF98 File Offset: 0x000BC398
		public static Matrix3D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string text = tokenizerHelper.NextTokenRequired();
			Matrix3D identity;
			if (text == "Identity")
			{
				identity = Matrix3D.Identity;
			}
			else
			{
				identity = new Matrix3D(Convert.ToDouble(text, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			}
			tokenizerHelper.LastTokenRequired();
			return identity;
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <returns>Representação de cadeia de caracteres desta estrutura Matrix3D.</returns>
		// Token: 0x06002F27 RID: 12071 RVA: 0x000BD09C File Offset: 0x000BC49C
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres dessa estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Representação de cadeia de caracteres desta estrutura <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</returns>
		// Token: 0x06002F28 RID: 12072 RVA: 0x000BD0B4 File Offset: 0x000BC4B4
		public string ToString(IFormatProvider provider)
		{
			return this.ConvertToString(null, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002F29 RID: 12073 RVA: 0x000BD0CC File Offset: 0x000BC4CC
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000BD0E4 File Offset: 0x000BC4E4
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
				"}{0}{5:",
				format,
				"}{0}{6:",
				format,
				"}{0}{7:",
				format,
				"}{0}{8:",
				format,
				"}{0}{9:",
				format,
				"}{0}{10:",
				format,
				"}{0}{11:",
				format,
				"}{0}{12:",
				format,
				"}{0}{13:",
				format,
				"}{0}{14:",
				format,
				"}{0}{15:",
				format,
				"}{0}{16:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this._m11,
				this._m12,
				this._m13,
				this._m14,
				this._m21,
				this._m22,
				this._m23,
				this._m24,
				this._m31,
				this._m32,
				this._m33,
				this._m34,
				this._offsetX,
				this._offsetY,
				this._offsetZ,
				this._m44
			});
		}

		// Token: 0x0400150E RID: 5390
		private double _m11;

		// Token: 0x0400150F RID: 5391
		private double _m12;

		// Token: 0x04001510 RID: 5392
		private double _m13;

		// Token: 0x04001511 RID: 5393
		private double _m14;

		// Token: 0x04001512 RID: 5394
		private double _m21;

		// Token: 0x04001513 RID: 5395
		private double _m22;

		// Token: 0x04001514 RID: 5396
		private double _m23;

		// Token: 0x04001515 RID: 5397
		private double _m24;

		// Token: 0x04001516 RID: 5398
		private double _m31;

		// Token: 0x04001517 RID: 5399
		private double _m32;

		// Token: 0x04001518 RID: 5400
		private double _m33;

		// Token: 0x04001519 RID: 5401
		private double _m34;

		// Token: 0x0400151A RID: 5402
		private double _offsetX;

		// Token: 0x0400151B RID: 5403
		private double _offsetY;

		// Token: 0x0400151C RID: 5404
		private double _offsetZ;

		// Token: 0x0400151D RID: 5405
		private double _m44;

		// Token: 0x0400151E RID: 5406
		private bool _isNotKnownToBeIdentity;

		// Token: 0x0400151F RID: 5407
		private static readonly Matrix3D s_identity = Matrix3D.CreateIdentity();

		// Token: 0x04001520 RID: 5408
		private const int c_identityHashCode = 0;
	}
}
