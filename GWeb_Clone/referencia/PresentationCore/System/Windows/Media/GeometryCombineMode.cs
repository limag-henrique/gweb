using System;

namespace System.Windows.Media
{
	/// <summary>Especifica os métodos diferentes pelos quais duas geometrias podem ser combinadas.</summary>
	// Token: 0x020003A4 RID: 932
	public enum GeometryCombineMode
	{
		/// <summary>As duas regiões são combinadas usando a união de ambas.  A geometria resultante é geometria <paramref name="A" /> + geometria <paramref name="B" />.</summary>
		// Token: 0x0400112C RID: 4396
		Union,
		/// <summary>As duas regiões são combinadas usando a interseção entre elas.  A nova área consiste na região sobreposta entre as duas geometrias.</summary>
		// Token: 0x0400112D RID: 4397
		Intersect,
		/// <summary>As duas regiões são combinadas usando a área que existe na primeira região, mas não na segunda e a área que existe na segunda região, mas não na primeira.  A nova região consiste em <paramref name="(A-B)" /> + <paramref name="(B-A)" />, em que <paramref name="A" /> e <paramref name="B" /> são geometrias.</summary>
		// Token: 0x0400112E RID: 4398
		Xor,
		/// <summary>A segunda região é excluída da primeira.  Considerando duas geometrias, <paramref name="A" /> e <paramref name="B" />, a área de geometria <paramref name="B" /> é removida da área de geometria <paramref name="A" />, produzindo uma região <paramref name="A-B" />.</summary>
		// Token: 0x0400112F RID: 4399
		Exclude
	}
}
