using System;
using System.Runtime.InteropServices;

namespace MS.Internal
{
	// Token: 0x0200069D RID: 1693
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct CharacterAttribute
	{
		// Token: 0x04001F5B RID: 8027
		internal byte Script;

		// Token: 0x04001F5C RID: 8028
		internal byte ItemClass;

		// Token: 0x04001F5D RID: 8029
		internal ushort Flags;

		// Token: 0x04001F5E RID: 8030
		internal byte BreakType;

		// Token: 0x04001F5F RID: 8031
		internal DirectionClass BiDi;

		// Token: 0x04001F60 RID: 8032
		internal short LineBreak;
	}
}
