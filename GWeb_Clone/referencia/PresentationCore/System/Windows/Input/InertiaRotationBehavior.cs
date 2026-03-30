using System;
using System.Windows.Input.Manipulations;

namespace System.Windows.Input
{
	/// <summary>Controla a desaceleração de uma manipulação de rotação durante inércia.</summary>
	// Token: 0x02000240 RID: 576
	public class InertiaRotationBehavior
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InertiaRotationBehavior" />.</summary>
		// Token: 0x06000FFC RID: 4092 RVA: 0x0003C65C File Offset: 0x0003BA5C
		public InertiaRotationBehavior()
		{
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003C69C File Offset: 0x0003BA9C
		internal InertiaRotationBehavior(double initialVelocity)
		{
			this._initialVelocity = initialVelocity;
		}

		/// <summary>Obtém ou define a taxa inicial de rotação no início da fase de inércia.</summary>
		/// <returns>A taxa inicial de rotação no início da fase de inércia.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003C6E4 File Offset: 0x0003BAE4
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003C6F8 File Offset: 0x0003BAF8
		public double InitialVelocity
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

		/// <summary>Obtém ou define a taxa de desaceleração da rotação em graus por milissegundo quadrado.</summary>
		/// <returns>A taxa de desaceleração da rotação em graus por milissegundo ao quadrado. O padrão é <see cref="F:System.Double.NaN" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade é definida como infinito.  
		///
		/// ou - 
		/// A propriedade é definida como <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0003C714 File Offset: 0x0003BB14
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x0003C728 File Offset: 0x0003BB28
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
				this._isDesiredRotationSet = false;
				this._desiredRotation = double.NaN;
			}
		}

		/// <summary>Obtém ou define a rotação, em graus, no final do movimento inércia.</summary>
		/// <returns>A rotação em graus, no final do movimento inércia. O padrão é <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0003C774 File Offset: 0x0003BB74
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x0003C788 File Offset: 0x0003BB88
		public double DesiredRotation
		{
			get
			{
				return this._desiredRotation;
			}
			set
			{
				if (double.IsInfinity(value) || double.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._isDesiredRotationSet = true;
				this._desiredRotation = value;
				this._isDesiredDecelerationSet = false;
				this._desiredDeceleration = double.NaN;
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003C7D4 File Offset: 0x0003BBD4
		internal bool CanUseForInertia()
		{
			return this._isInitialVelocitySet || this._isDesiredDecelerationSet || this._isDesiredRotationSet;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003C7FC File Offset: 0x0003BBFC
		internal static void ApplyParameters(InertiaRotationBehavior behavior, InertiaProcessor2D processor, double initialVelocity)
		{
			if (behavior != null && behavior.CanUseForInertia())
			{
				InertiaRotationBehavior2D inertiaRotationBehavior2D = new InertiaRotationBehavior2D();
				if (behavior._isInitialVelocitySet)
				{
					inertiaRotationBehavior2D.InitialVelocity = (float)AngleUtil.DegreesToRadians(behavior._initialVelocity);
				}
				else
				{
					inertiaRotationBehavior2D.InitialVelocity = (float)AngleUtil.DegreesToRadians(initialVelocity);
				}
				if (behavior._isDesiredDecelerationSet)
				{
					inertiaRotationBehavior2D.DesiredDeceleration = (float)AngleUtil.DegreesToRadians(behavior._desiredDeceleration);
				}
				if (behavior._isDesiredRotationSet)
				{
					inertiaRotationBehavior2D.DesiredRotation = (float)AngleUtil.DegreesToRadians(behavior._desiredRotation);
				}
				processor.RotationBehavior = inertiaRotationBehavior2D;
			}
		}

		// Token: 0x040008AA RID: 2218
		private bool _isInitialVelocitySet;

		// Token: 0x040008AB RID: 2219
		private double _initialVelocity = double.NaN;

		// Token: 0x040008AC RID: 2220
		private bool _isDesiredDecelerationSet;

		// Token: 0x040008AD RID: 2221
		private double _desiredDeceleration = double.NaN;

		// Token: 0x040008AE RID: 2222
		private bool _isDesiredRotationSet;

		// Token: 0x040008AF RID: 2223
		private double _desiredRotation = double.NaN;
	}
}
