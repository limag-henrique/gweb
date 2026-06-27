using System;

namespace System.Windows.Media
{
	/// <summary>Fornece informações sobre a interseção entre as geometrias no <see cref="T:System.Windows.Media.GeometryHitTestParameters" /> e o visual que recebeu um clique.</summary>
	// Token: 0x0200041B RID: 1051
	public enum IntersectionDetail
	{
		/// <summary>O valor <see cref="T:System.Windows.Media.IntersectionDetail" /> não está calculado.</summary>
		// Token: 0x04001301 RID: 4865
		NotCalculated,
		/// <summary>O parâmetro de teste de clique <see cref="T:System.Windows.Media.Geometry" /> e o visual de destino ou a geometria não se interseccionam.</summary>
		// Token: 0x04001302 RID: 4866
		Empty,
		/// <summary>O visual de destino ou a geometria está totalmente dentro do parâmetro de teste de clique <see cref="T:System.Windows.Media.Geometry" />.</summary>
		// Token: 0x04001303 RID: 4867
		FullyInside,
		/// <summary>O parâmetro de teste de clique <see cref="T:System.Windows.Media.Geometry" /> está totalmente contido no limite do visual ou da geometria de destino.</summary>
		// Token: 0x04001304 RID: 4868
		FullyContains,
		/// <summary>O parâmetro de teste de clique <see cref="T:System.Windows.Media.Geometry" /> e o visual de destino ou a geometria, se interseccionam. Isso significa que os dois elementos se sobrepõem, mas nenhum elemento contém o outro.</summary>
		// Token: 0x04001305 RID: 4869
		Intersects
	}
}
