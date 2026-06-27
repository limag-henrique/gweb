using System;

namespace System.Windows.Input
{
	/// <summary>Define os valores que especificam os recursos de hardware de um dispositivo de tablet, incluindo mouses e digitalizadores de área de trabalho.</summary>
	// Token: 0x020002CF RID: 719
	[Flags]
	[Serializable]
	public enum TabletHardwareCapabilities
	{
		/// <summary>Indica que o dispositivo de tablet não pode fornecer essas informações.</summary>
		// Token: 0x04000BBD RID: 3005
		None = 0,
		/// <summary>Indica que o digitalizador é integrado à tela.</summary>
		// Token: 0x04000BBE RID: 3006
		Integrated = 1,
		/// <summary>Indica que a caneta deve estar em contato físico com o dispositivo de tablet para relatar sua posição.</summary>
		// Token: 0x04000BBF RID: 3007
		StylusMustTouch = 2,
		/// <summary>Indica que o dispositivo de tablet poderá gerar pacotes no ar quando a caneta estiver no intervalo de detecção física (proximidade) do dispositivo de tablet.</summary>
		// Token: 0x04000BC0 RID: 3008
		HardProximity = 4,
		/// <summary>Indica que o dispositivo de tablet pode identificar exclusivamente a caneta ativa.</summary>
		// Token: 0x04000BC1 RID: 3009
		StylusHasPhysicalIds = 8,
		/// <summary>Indica que o dispositivo de tablet pode detectar a quantidade de pressão que o usuário aplica ao usar a caneta.</summary>
		// Token: 0x04000BC2 RID: 3010
		SupportsPressure = 1073741824
	}
}
