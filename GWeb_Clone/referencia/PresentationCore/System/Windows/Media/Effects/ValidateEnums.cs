using System;

namespace System.Windows.Media.Effects
{
	// Token: 0x02000604 RID: 1540
	internal static class ValidateEnums
	{
		// Token: 0x06004690 RID: 18064 RVA: 0x001149EC File Offset: 0x00113DEC
		public static bool IsShaderRenderModeValid(object valueObject)
		{
			ShaderRenderMode shaderRenderMode = (ShaderRenderMode)valueObject;
			return shaderRenderMode == ShaderRenderMode.Auto || shaderRenderMode == ShaderRenderMode.SoftwareOnly || shaderRenderMode == ShaderRenderMode.HardwareOnly;
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00114A10 File Offset: 0x00113E10
		public static bool IsKernelTypeValid(object valueObject)
		{
			KernelType kernelType = (KernelType)valueObject;
			return kernelType == KernelType.Gaussian || kernelType == KernelType.Box;
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00114A30 File Offset: 0x00113E30
		public static bool IsEdgeProfileValid(object valueObject)
		{
			EdgeProfile edgeProfile = (EdgeProfile)valueObject;
			return edgeProfile == EdgeProfile.Linear || edgeProfile == EdgeProfile.CurvedIn || edgeProfile == EdgeProfile.CurvedOut || edgeProfile == EdgeProfile.BulgedUp;
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00114A58 File Offset: 0x00113E58
		public static bool IsRenderingBiasValid(object valueObject)
		{
			RenderingBias renderingBias = (RenderingBias)valueObject;
			return renderingBias == RenderingBias.Performance || renderingBias == RenderingBias.Quality;
		}
	}
}
