using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;
using MS.Internal;

namespace System.Windows.Media
{
	// Token: 0x02000429 RID: 1065
	internal static class MILUtilities
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000AE298 File Offset: 0x000AD698
		[SecurityCritical]
		internal unsafe static void ConvertToD3DMATRIX(Matrix* matrix, D3DMATRIX* d3dMatrix)
		{
			*d3dMatrix = MILUtilities.D3DMATRIXIdentity;
			*(float*)d3dMatrix = (float)(*(double*)matrix);
			*(float*)(d3dMatrix + 4 / sizeof(D3DMATRIX)) = (float)(*(double*)(matrix + 8 / sizeof(Matrix)));
			*(float*)(d3dMatrix + (IntPtr)4 * 4 / (IntPtr)sizeof(D3DMATRIX)) = (float)(*(double*)(matrix + (IntPtr)2 * 8 / (IntPtr)sizeof(Matrix)));
			*(float*)(d3dMatrix + (IntPtr)5 * 4 / (IntPtr)sizeof(D3DMATRIX)) = (float)(*(double*)(matrix + (IntPtr)3 * 8 / (IntPtr)sizeof(Matrix)));
			*(float*)(d3dMatrix + (IntPtr)12 * 4 / (IntPtr)sizeof(D3DMATRIX)) = (float)(*(double*)(matrix + (IntPtr)4 * 8 / (IntPtr)sizeof(Matrix)));
			*(float*)(d3dMatrix + (IntPtr)13 * 4 / (IntPtr)sizeof(D3DMATRIX)) = (float)(*(double*)(matrix + (IntPtr)5 * 8 / (IntPtr)sizeof(Matrix)));
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000AE300 File Offset: 0x000AD700
		[SecurityCritical]
		internal unsafe static void ConvertFromD3DMATRIX(D3DMATRIX* d3dMatrix, Matrix* matrix)
		{
			*(double*)matrix = (double)(*(float*)d3dMatrix);
			*(double*)(matrix + 8 / sizeof(Matrix)) = (double)(*(float*)(d3dMatrix + 4 / sizeof(D3DMATRIX)));
			*(double*)(matrix + (IntPtr)2 * 8 / (IntPtr)sizeof(Matrix)) = (double)(*(float*)(d3dMatrix + (IntPtr)4 * 4 / (IntPtr)sizeof(D3DMATRIX)));
			*(double*)(matrix + (IntPtr)3 * 8 / (IntPtr)sizeof(Matrix)) = (double)(*(float*)(d3dMatrix + (IntPtr)5 * 4 / (IntPtr)sizeof(D3DMATRIX)));
			*(double*)(matrix + (IntPtr)4 * 8 / (IntPtr)sizeof(Matrix)) = (double)(*(float*)(d3dMatrix + (IntPtr)12 * 4 / (IntPtr)sizeof(D3DMATRIX)));
			*(double*)(matrix + (IntPtr)5 * 8 / (IntPtr)sizeof(Matrix)) = (double)(*(float*)(d3dMatrix + (IntPtr)13 * 4 / (IntPtr)sizeof(D3DMATRIX)));
			*(int*)(matrix + (IntPtr)6 * 8 / (IntPtr)sizeof(Matrix)) = 4;
		}

		// Token: 0x06002BB4 RID: 11188
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll")]
		private static extern int MIL3DCalcProjected2DBounds(ref D3DMATRIX pFullTransform3D, ref MILUtilities.MILRect3D pboxBounds, out MILUtilities.MilRectF prcDestRect);

		// Token: 0x06002BB5 RID: 11189
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilUtility_CopyPixelBuffer", PreserveSig = false)]
		internal unsafe static extern void MILCopyPixelBuffer(byte* pOutputBuffer, uint outputBufferSize, uint outputBufferStride, uint outputBufferOffsetInBits, byte* pInputBuffer, uint inputBufferSize, uint inputBufferStride, uint inputBufferOffsetInBits, uint height, uint copyWidthInBits);

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000AE368 File Offset: 0x000AD768
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static Rect ProjectBounds(ref Matrix3D viewProjMatrix, ref Rect3D originalBox)
		{
			D3DMATRIX d3DMATRIX = CompositionResourceManager.Matrix3DToD3DMATRIX(viewProjMatrix);
			MILUtilities.MILRect3D milrect3D = new MILUtilities.MILRect3D(ref originalBox);
			MILUtilities.MilRectF milRectF = default(MILUtilities.MilRectF);
			HRESULT.Check(MILUtilities.MIL3DCalcProjected2DBounds(ref d3DMATRIX, ref milrect3D, out milRectF));
			if (milRectF.Left == milRectF.Right || milRectF.Top == milRectF.Bottom)
			{
				return Rect.Empty;
			}
			return new Rect((double)milRectF.Left, (double)milRectF.Top, (double)(milRectF.Right - milRectF.Left), (double)(milRectF.Bottom - milRectF.Top));
		}

		// Token: 0x040013E0 RID: 5088
		internal static readonly D3DMATRIX D3DMATRIXIdentity = new D3DMATRIX(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		// Token: 0x02000893 RID: 2195
		internal struct MILRect3D
		{
			// Token: 0x06005821 RID: 22561 RVA: 0x00167644 File Offset: 0x00166A44
			public MILRect3D(ref Rect3D rect)
			{
				this.X = (float)rect.X;
				this.Y = (float)rect.Y;
				this.Z = (float)rect.Z;
				this.LengthX = (float)rect.SizeX;
				this.LengthY = (float)rect.SizeY;
				this.LengthZ = (float)rect.SizeZ;
			}

			// Token: 0x040028EA RID: 10474
			public float X;

			// Token: 0x040028EB RID: 10475
			public float Y;

			// Token: 0x040028EC RID: 10476
			public float Z;

			// Token: 0x040028ED RID: 10477
			public float LengthX;

			// Token: 0x040028EE RID: 10478
			public float LengthY;

			// Token: 0x040028EF RID: 10479
			public float LengthZ;
		}

		// Token: 0x02000894 RID: 2196
		internal struct MilRectF
		{
			// Token: 0x040028F0 RID: 10480
			public float Left;

			// Token: 0x040028F1 RID: 10481
			public float Top;

			// Token: 0x040028F2 RID: 10482
			public float Right;

			// Token: 0x040028F3 RID: 10483
			public float Bottom;
		}
	}
}
