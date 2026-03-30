using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Especifica as constantes que definem as ações executadas pelo mouse.</summary>
	// Token: 0x0200021A RID: 538
	[ValueSerializer(typeof(MouseActionValueSerializer))]
	[TypeConverter(typeof(MouseActionConverter))]
	public enum MouseAction : byte
	{
		/// <summary>Nenhuma ação.</summary>
		// Token: 0x04000845 RID: 2117
		None,
		/// <summary>Um clique no botão esquerdo do mouse.</summary>
		// Token: 0x04000846 RID: 2118
		LeftClick,
		/// <summary>Um clique no botão direito do mouse.</summary>
		// Token: 0x04000847 RID: 2119
		RightClick,
		/// <summary>Um clique no botão do meio do mouse.</summary>
		// Token: 0x04000848 RID: 2120
		MiddleClick,
		/// <summary>Uma rotação da roda do mouse.</summary>
		// Token: 0x04000849 RID: 2121
		WheelClick,
		/// <summary>Um clique duplo no botão esquerdo do mouse.</summary>
		// Token: 0x0400084A RID: 2122
		LeftDoubleClick,
		/// <summary>Um clique duplo no botão direito do mouse.</summary>
		// Token: 0x0400084B RID: 2123
		RightDoubleClick,
		/// <summary>Um clique duas vezes no botão do meio do mouse.</summary>
		// Token: 0x0400084C RID: 2124
		MiddleDoubleClick
	}
}
