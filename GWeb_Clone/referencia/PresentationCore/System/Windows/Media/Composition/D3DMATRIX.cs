using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000637 RID: 1591
	[StructLayout(LayoutKind.Explicit)]
	internal struct D3DMATRIX
	{
		// Token: 0x060047FC RID: 18428 RVA: 0x00119B6C File Offset: 0x00118F6C
		internal D3DMATRIX(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this._11 = m11;
			this._12 = m12;
			this._13 = m13;
			this._14 = m14;
			this._21 = m21;
			this._22 = m22;
			this._23 = m23;
			this._24 = m24;
			this._31 = m31;
			this._32 = m32;
			this._33 = m33;
			this._34 = m34;
			this._41 = m41;
			this._42 = m42;
			this._43 = m43;
			this._44 = m44;
		}

		// Token: 0x04001B85 RID: 7045
		[FieldOffset(0)]
		internal float _11;

		// Token: 0x04001B86 RID: 7046
		[FieldOffset(4)]
		internal float _12;

		// Token: 0x04001B87 RID: 7047
		[FieldOffset(8)]
		internal float _13;

		// Token: 0x04001B88 RID: 7048
		[FieldOffset(12)]
		internal float _14;

		// Token: 0x04001B89 RID: 7049
		[FieldOffset(16)]
		internal float _21;

		// Token: 0x04001B8A RID: 7050
		[FieldOffset(20)]
		internal float _22;

		// Token: 0x04001B8B RID: 7051
		[FieldOffset(24)]
		internal float _23;

		// Token: 0x04001B8C RID: 7052
		[FieldOffset(28)]
		internal float _24;

		// Token: 0x04001B8D RID: 7053
		[FieldOffset(32)]
		internal float _31;

		// Token: 0x04001B8E RID: 7054
		[FieldOffset(36)]
		internal float _32;

		// Token: 0x04001B8F RID: 7055
		[FieldOffset(40)]
		internal float _33;

		// Token: 0x04001B90 RID: 7056
		[FieldOffset(44)]
		internal float _34;

		// Token: 0x04001B91 RID: 7057
		[FieldOffset(48)]
		internal float _41;

		// Token: 0x04001B92 RID: 7058
		[FieldOffset(52)]
		internal float _42;

		// Token: 0x04001B93 RID: 7059
		[FieldOffset(56)]
		internal float _43;

		// Token: 0x04001B94 RID: 7060
		[FieldOffset(60)]
		internal float _44;
	}
}
