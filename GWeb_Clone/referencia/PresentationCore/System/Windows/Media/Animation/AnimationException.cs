using System;
using System.Runtime.Serialization;

namespace System.Windows.Media.Animation
{
	/// <summary>A exceção lançada quando ocorre um erro ao animar uma propriedade.</summary>
	// Token: 0x020004A2 RID: 1186
	[Serializable]
	public sealed class AnimationException : SystemException
	{
		// Token: 0x06003497 RID: 13463 RVA: 0x000D04A0 File Offset: 0x000CF8A0
		internal AnimationException(AnimationClock clock, DependencyProperty property, IAnimatable target, string message, Exception innerException) : base(message, innerException)
		{
			this._clock = clock;
			this._property = property;
			this._targetElement = target;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000D04CC File Offset: 0x000CF8CC
		private AnimationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Obtém o relógio que gera os valores animados.</summary>
		/// <returns>O relógio que gera os valores animados.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000D04E4 File Offset: 0x000CF8E4
		public AnimationClock Clock
		{
			get
			{
				return this._clock;
			}
		}

		/// <summary>Obtém a propriedade de dependência animada.</summary>
		/// <returns>A propriedade de dependência animada.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000D04F8 File Offset: 0x000CF8F8
		public DependencyProperty Property
		{
			get
			{
				return this._property;
			}
		}

		/// <summary>Obtém o objeto animado.</summary>
		/// <returns>O objeto animado.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000D050C File Offset: 0x000CF90C
		public IAnimatable Target
		{
			get
			{
				return this._targetElement;
			}
		}

		// Token: 0x04001604 RID: 5636
		[NonSerialized]
		private AnimationClock _clock;

		// Token: 0x04001605 RID: 5637
		[NonSerialized]
		private DependencyProperty _property;

		// Token: 0x04001606 RID: 5638
		[NonSerialized]
		private IAnimatable _targetElement;
	}
}
