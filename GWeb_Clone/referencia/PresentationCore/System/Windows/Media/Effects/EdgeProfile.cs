using System;

namespace System.Windows.Media.Effects
{
	/// <summary>Especifica o tipo de curva a ser aplicada à borda de um bitmap.</summary>
	// Token: 0x02000618 RID: 1560
	public enum EdgeProfile
	{
		/// <summary>Uma borda é uma linha reta.</summary>
		// Token: 0x04001A2C RID: 6700
		Linear,
		/// <summary>Uma borda côncava que curva para dentro.</summary>
		// Token: 0x04001A2D RID: 6701
		CurvedIn,
		/// <summary>Uma borda convexa que curva para fora.</summary>
		// Token: 0x04001A2E RID: 6702
		CurvedOut,
		/// <summary>Uma borda que curva para cima e para baixo, como uma ondulação.</summary>
		// Token: 0x04001A2F RID: 6703
		BulgedUp
	}
}
