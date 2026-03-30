using System;
using System.Windows.Input.Manipulations;

namespace System.Windows.Input
{
	/// <summary>Controla a desaceleração em uma manipulação de conversão durante inércia.</summary>
	// Token: 0x02000241 RID: 577
	public class InertiaTranslationBehavior
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InertiaTranslationBehavior" />.</summary>
		// Token: 0x06001006 RID: 4102 RVA: 0x0003C880 File Offset: 0x0003BC80
		public InertiaTranslationBehavior()
		{
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003C8D0 File Offset: 0x0003BCD0
		internal InertiaTranslationBehavior(Vector initialVelocity)
		{
			this._initialVelocity = initialVelocity;
		}

		/// <summary>Obtém ou define a taxa inicial de movimento linear no início da fase de inércia.</summary>
		/// <returns>A taxa inicial de movimento linear no início da fase de inércia.</returns>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0003C928 File Offset: 0x0003BD28
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0003C93C File Offset: 0x0003BD3C
		public Vector InitialVelocity
		{
			get
			{
				return this._initialVelocity;
			}
			set
			{
				this._isInitialVelocitySet = true;
				this._initialVelocity = value;
			}
		}

		/// <summary>Obtém ou define a taxa de desaceleração do movimento linear em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo quadrado.</summary>
		/// <returns>A taxa de desaceleração do movimento linear em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo quadrado.  O padrão é <see cref="F:System.Double.NaN" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade é definida como infinito.  
		///
		/// ou - 
		/// A propriedade é definida como <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003C958 File Offset: 0x0003BD58
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x0003C96C File Offset: 0x0003BD6C
		public double DesiredDeceleration
		{
			get
			{
				return this._desiredDeceleration;
			}
			set
			{
				if (double.IsInfinity(value) || double.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._isDesiredDecelerationSet = true;
				this._desiredDeceleration = value;
				this._isDesiredDisplacementSet = false;
				this._desiredDisplacement = double.NaN;
			}
		}

		/// <summary>Obtém ou define o movimento linear da manipulação no final da inércia.</summary>
		/// <returns>O movimento linear da manipulação no final da inércia. O padrão é <see cref="F:System.Double.NaN" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade é definida como infinito.  
		///
		/// ou - 
		/// A propriedade é definida como <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0003C9B8 File Offset: 0x0003BDB8
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x0003C9CC File Offset: 0x0003BDCC
		public double DesiredDisplacement
		{
			get
			{
				return this._desiredDisplacement;
			}
			set
			{
				if (double.IsInfinity(value) || double.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._isDesiredDisplacementSet = true;
				this._desiredDisplacement = value;
				this._isDesiredDecelerationSet = false;
				this._desiredDeceleration = double.NaN;
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003CA18 File Offset: 0x0003BE18
		internal bool CanUseForInertia()
		{
			return this._isInitialVelocitySet || this._isDesiredDecelerationSet || this._isDesiredDisplacementSet;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0003CA40 File Offset: 0x0003BE40
		internal static void ApplyParameters(InertiaTranslationBehavior behavior, InertiaProcessor2D processor, Vector initialVelocity)
		{
			if (behavior != null && behavior.CanUseForInertia())
			{
				InertiaTranslationBehavior2D inertiaTranslationBehavior2D = new InertiaTranslationBehavior2D();
				if (behavior._isInitialVelocitySet)
				{
					inertiaTranslationBehavior2D.InitialVelocityX = (float)behavior._initialVelocity.X;
					inertiaTranslationBehavior2D.InitialVelocityY = (float)behavior._initialVelocity.Y;
				}
				else
				{
					inertiaTranslationBehavior2D.InitialVelocityX = (float)initialVelocity.X;
					inertiaTranslationBehavior2D.InitialVelocityY = (float)initialVelocity.Y;
				}
				if (behavior._isDesiredDecelerationSet)
				{
					inertiaTranslationBehavior2D.DesiredDeceleration = (float)behavior._desiredDeceleration;
				}
				if (behavior._isDesiredDisplacementSet)
				{
					inertiaTranslationBehavior2D.DesiredDisplacement = (float)behavior._desiredDisplacement;
				}
				processor.TranslationBehavior = inertiaTranslationBehavior2D;
			}
		}

		// Token: 0x040008B0 RID: 2224
		private bool _isInitialVelocitySet;

		// Token: 0x040008B1 RID: 2225
		private Vector _initialVelocity = new Vector(double.NaN, double.NaN);

		// Token: 0x040008B2 RID: 2226
		private bool _isDesiredDecelerationSet;

		// Token: 0x040008B3 RID: 2227
		private double _desiredDeceleration = double.NaN;

		// Token: 0x040008B4 RID: 2228
		private bool _isDesiredDisplacementSet;

		// Token: 0x040008B5 RID: 2229
		private double _desiredDisplacement = double.NaN;
	}
}
