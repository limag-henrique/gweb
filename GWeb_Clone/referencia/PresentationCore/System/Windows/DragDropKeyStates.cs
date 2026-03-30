using System;

namespace System.Windows
{
	/// <summary>Especifica o estado atual das teclas modificadoras (SHIFT, CTRL e ALT), bem como o estado dos botões do mouse.</summary>
	// Token: 0x020001C6 RID: 454
	[Flags]
	public enum DragDropKeyStates
	{
		/// <summary>Nenhuma tecla modificadora ou botão do mouse é pressionado.</summary>
		// Token: 0x040006E2 RID: 1762
		None = 0,
		/// <summary>O botão esquerdo do mouse foi pressionado.</summary>
		// Token: 0x040006E3 RID: 1763
		LeftMouseButton = 1,
		/// <summary>O botão direito do mouse é pressionado.</summary>
		// Token: 0x040006E4 RID: 1764
		RightMouseButton = 2,
		/// <summary>A tecla shift (SHIFT) está pressionada.</summary>
		// Token: 0x040006E5 RID: 1765
		ShiftKey = 4,
		/// <summary>A tecla CTRL é pressionada.</summary>
		// Token: 0x040006E6 RID: 1766
		ControlKey = 8,
		/// <summary>O botão do meio do mouse é pressionado.</summary>
		// Token: 0x040006E7 RID: 1767
		MiddleMouseButton = 16,
		/// <summary>A tecla ALT está pressionada.</summary>
		// Token: 0x040006E8 RID: 1768
		AltKey = 32
	}
}
