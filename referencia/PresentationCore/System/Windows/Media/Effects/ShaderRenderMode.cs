using System;

namespace System.Windows.Media.Effects
{
	/// <summary>Indica a política para renderização de um <see cref="T:System.Windows.Media.Effects.ShaderEffect" /> em software.</summary>
	// Token: 0x02000619 RID: 1561
	public enum ShaderRenderMode
	{
		/// <summary>Permitir renderização de hardware e de software.</summary>
		// Token: 0x04001A31 RID: 6705
		Auto,
		/// <summary>Force a renderização de software.</summary>
		// Token: 0x04001A32 RID: 6706
		SoftwareOnly,
		/// <summary>Requer renderização de hardware, ignore se não estiver disponível.</summary>
		// Token: 0x04001A33 RID: 6707
		HardwareOnly
	}
}
