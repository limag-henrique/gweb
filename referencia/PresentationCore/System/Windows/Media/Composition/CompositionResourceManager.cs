using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000640 RID: 1600
	internal static class CompositionResourceManager
	{
		// Token: 0x060047FD RID: 18429 RVA: 0x00119BF8 File Offset: 0x00118FF8
		internal static MilColorF ColorToMilColorF(Color c)
		{
			MilColorF result;
			result.r = c.ScR;
			result.g = c.ScG;
			result.b = c.ScB;
			result.a = c.ScA;
			return result;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00119C40 File Offset: 0x00119040
		internal static D3DMATRIX Matrix3DToD3DMATRIX(Matrix3D m)
		{
			D3DMATRIX result;
			result._11 = (float)m.M11;
			result._12 = (float)m.M12;
			result._13 = (float)m.M13;
			result._14 = (float)m.M14;
			result._21 = (float)m.M21;
			result._22 = (float)m.M22;
			result._23 = (float)m.M23;
			result._24 = (float)m.M24;
			result._31 = (float)m.M31;
			result._32 = (float)m.M32;
			result._33 = (float)m.M33;
			result._34 = (float)m.M34;
			result._41 = (float)m.OffsetX;
			result._42 = (float)m.OffsetY;
			result._43 = (float)m.OffsetZ;
			result._44 = (float)m.M44;
			return result;
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00119D40 File Offset: 0x00119140
		internal static MilPoint3F Point3DToMilPoint3F(Point3D p)
		{
			MilPoint3F result;
			result.X = (float)p.X;
			result.Y = (float)p.Y;
			result.Z = (float)p.Z;
			return result;
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x00119D7C File Offset: 0x0011917C
		internal static MilPoint3F Vector3DToMilPoint3F(Vector3D v)
		{
			MilPoint3F result;
			result.X = (float)v.X;
			result.Y = (float)v.Y;
			result.Z = (float)v.Z;
			return result;
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00119DB8 File Offset: 0x001191B8
		internal static MilQuaternionF QuaternionToMilQuaternionF(Quaternion q)
		{
			MilQuaternionF result;
			result.X = (float)q.X;
			result.Y = (float)q.Y;
			result.Z = (float)q.Z;
			result.W = (float)q.W;
			return result;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00119E04 File Offset: 0x00119204
		internal static MilMatrix4x4D MatrixToMilMatrix4x4D(Matrix m)
		{
			MilMatrix4x4D result;
			if (m.IsIdentity)
			{
				result.M_11 = 1.0;
				result.M_12 = 0.0;
				result.M_13 = 0.0;
				result.M_14 = 0.0;
				result.M_21 = 0.0;
				result.M_22 = 1.0;
				result.M_23 = 0.0;
				result.M_24 = 0.0;
				result.M_31 = 0.0;
				result.M_32 = 0.0;
				result.M_33 = 1.0;
				result.M_34 = 0.0;
				result.M_41 = 0.0;
				result.M_42 = 0.0;
				result.M_43 = 0.0;
				result.M_44 = 1.0;
			}
			else
			{
				result.M_11 = m.M11;
				result.M_12 = m.M12;
				result.M_13 = 0.0;
				result.M_14 = 0.0;
				result.M_21 = m.M21;
				result.M_22 = m.M22;
				result.M_23 = 0.0;
				result.M_24 = 0.0;
				result.M_31 = 0.0;
				result.M_32 = 0.0;
				result.M_33 = 1.0;
				result.M_34 = 0.0;
				result.M_41 = m.OffsetX;
				result.M_42 = m.OffsetY;
				result.M_43 = 0.0;
				result.M_44 = 1.0;
			}
			return result;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0011A018 File Offset: 0x00119418
		internal static MilMatrix3x2D TransformToMilMatrix3x2D(Transform t)
		{
			MilMatrix3x2D result;
			if (t == null || t.IsIdentity)
			{
				result.S_11 = 1.0;
				result.S_12 = 0.0;
				result.S_21 = 0.0;
				result.S_22 = 1.0;
				result.DX = 0.0;
				result.DY = 0.0;
			}
			else
			{
				Matrix value = t.Value;
				result.S_11 = value.M11;
				result.S_12 = value.M12;
				result.S_21 = value.M21;
				result.S_22 = value.M22;
				result.DX = value.OffsetX;
				result.DY = value.OffsetY;
			}
			return result;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0011A0F0 File Offset: 0x001194F0
		internal static MilMatrix3x2D MatrixToMilMatrix3x2D(Matrix m)
		{
			return CompositionResourceManager.MatrixToMilMatrix3x2D(ref m);
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0011A104 File Offset: 0x00119504
		internal static MilMatrix3x2D MatrixToMilMatrix3x2D(ref Matrix m)
		{
			MilMatrix3x2D result;
			if (m.IsIdentity)
			{
				result.S_11 = 1.0;
				result.S_12 = 0.0;
				result.S_21 = 0.0;
				result.S_22 = 1.0;
				result.DX = 0.0;
				result.DY = 0.0;
			}
			else
			{
				result.S_11 = m.M11;
				result.S_12 = m.M12;
				result.S_21 = m.M21;
				result.S_22 = m.M22;
				result.DX = m.OffsetX;
				result.DY = m.OffsetY;
			}
			return result;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0011A1CC File Offset: 0x001195CC
		internal static Matrix MilMatrix3x2DToMatrix(ref MilMatrix3x2D m)
		{
			return new Matrix(m.S_11, m.S_12, m.S_21, m.S_22, m.DX, m.DY);
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0011A204 File Offset: 0x00119604
		internal static uint BooleanToUInt32(bool v)
		{
			if (!v)
			{
				return 0U;
			}
			return uint.MaxValue;
		}

		// Token: 0x04001BC3 RID: 7107
		public const int InvalidResourceHandle = 0;
	}
}
