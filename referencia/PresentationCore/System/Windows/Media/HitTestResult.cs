using System;

namespace System.Windows.Media
{
	/// <summary>Fornece a classe base para várias classes derivadas, que representa o valor retornado de um teste de clique.</summary>
	// Token: 0x0200040E RID: 1038
	public abstract class HitTestResult
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x000A8B34 File Offset: 0x000A7F34
		internal HitTestResult(DependencyObject visualHit)
		{
			this._visualHit = visualHit;
		}

		/// <summary>Obtém o objeto visual que foi atingido.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.DependencyObject" /> que representa o objeto visual que foi atingido.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000A8B50 File Offset: 0x000A7F50
		public DependencyObject VisualHit
		{
			get
			{
				return this._visualHit;
			}
		}

		// Token: 0x040012F9 RID: 4857
		private DependencyObject _visualHit;
	}
}
