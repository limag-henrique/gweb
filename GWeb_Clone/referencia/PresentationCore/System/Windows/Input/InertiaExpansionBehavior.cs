using System;
using System.Windows.Input.Manipulations;

namespace System.Windows.Input
{
	/// <summary>Controla a desaceleração de uma manipulação de redimensionamento durante inércia.</summary>
	// Token: 0x0200023F RID: 575
	public class InertiaExpansionBehavior
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InertiaExpansionBehavior" />.</summary>
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003C368 File Offset: 0x0003B768
		public InertiaExpansionBehavior()
		{
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003C3D4 File Offset: 0x0003B7D4
		internal InertiaExpansionBehavior(Vector initialVelocity)
		{
			this._initialVelocity = initialVelocity;
		}

		/// <summary>Obtém ou define a taxa inicial que o elemento redimensiona no início de inércia.</summary>
		/// <returns>A taxa inicial que o elemento redimensiona no início de inércia.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0003C448 File Offset: 0x0003B848
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x0003C45C File Offset: 0x0003B85C
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

		/// <summary>Obtém ou define a taxa de desaceleração do redimensionamento em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundos quadrados.</summary>
		/// <returns>A taxa de desaceleração do redimensionamento em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundos quadrados. O padrão é <see cref="F:System.Double.NaN" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade é definida como infinito.  
		///
		/// ou - 
		/// A propriedade é definida como <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003C478 File Offset: 0x0003B878
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0003C48C File Offset: 0x0003B88C
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
				this._isDesiredExpansionSet = false;
				this._desiredExpansion = new Vector(double.NaN, double.NaN);
			}
		}

		/// <summary>Obtém ou define a quantidade de redimensionamento do elemento no final da inércia.</summary>
		/// <returns>A quantidade elemento redimensiona no final da inércia. O padrão é uma <see cref="T:System.Windows.Vector" /> que tem sua <see cref="P:System.Windows.Vector.X" /> e <see cref="P:System.Windows.Vector.Y" /> propriedades definidas como <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0003C4E8 File Offset: 0x0003B8E8
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x0003C4FC File Offset: 0x0003B8FC
		public Vector DesiredExpansion
		{
			get
			{
				return this._desiredExpansion;
			}
			set
			{
				this._isDesiredExpansionSet = true;
				this._desiredExpansion = value;
				this._isDesiredDecelerationSet = false;
				this._desiredDeceleration = double.NaN;
			}
		}

		/// <summary>Obtém ou define o raio médio inicial.</summary>
		/// <returns>O raio médio inicial.</returns>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0003C530 File Offset: 0x0003B930
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x0003C544 File Offset: 0x0003B944
		public double InitialRadius
		{
			get
			{
				return this._initialRadius;
			}
			set
			{
				this._isInitialRadiusSet = true;
				this._initialRadius = value;
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003C560 File Offset: 0x0003B960
		internal bool CanUseForInertia()
		{
			return this._isInitialVelocitySet || this._isInitialRadiusSet || this._isDesiredDecelerationSet || this._isDesiredExpansionSet;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003C590 File Offset: 0x0003B990
		internal static void ApplyParameters(InertiaExpansionBehavior behavior, InertiaProcessor2D processor, Vector initialVelocity)
		{
			if (behavior != null && behavior.CanUseForInertia())
			{
				InertiaExpansionBehavior2D inertiaExpansionBehavior2D = new InertiaExpansionBehavior2D();
				if (behavior._isInitialVelocitySet)
				{
					inertiaExpansionBehavior2D.InitialVelocityX = (float)behavior._initialVelocity.X;
					inertiaExpansionBehavior2D.InitialVelocityY = (float)behavior._initialVelocity.Y;
				}
				else
				{
					inertiaExpansionBehavior2D.InitialVelocityX = (float)initialVelocity.X;
					inertiaExpansionBehavior2D.InitialVelocityY = (float)initialVelocity.Y;
				}
				if (behavior._isDesiredDecelerationSet)
				{
					inertiaExpansionBehavior2D.DesiredDeceleration = (float)behavior._desiredDeceleration;
				}
				if (behavior._isDesiredExpansionSet)
				{
					inertiaExpansionBehavior2D.DesiredExpansionX = (float)behavior._desiredExpansion.X;
					inertiaExpansionBehavior2D.DesiredExpansionY = (float)behavior._desiredExpansion.Y;
				}
				if (behavior._isInitialRadiusSet)
				{
					inertiaExpansionBehavior2D.InitialRadius = (float)behavior._initialRadius;
				}
				processor.ExpansionBehavior = inertiaExpansionBehavior2D;
			}
		}

		// Token: 0x040008A2 RID: 2210
		private bool _isInitialVelocitySet;

		// Token: 0x040008A3 RID: 2211
		private Vector _initialVelocity = new Vector(double.NaN, double.NaN);

		// Token: 0x040008A4 RID: 2212
		private bool _isDesiredDecelerationSet;

		// Token: 0x040008A5 RID: 2213
		private double _desiredDeceleration = double.NaN;

		// Token: 0x040008A6 RID: 2214
		private bool _isDesiredExpansionSet;

		// Token: 0x040008A7 RID: 2215
		private Vector _desiredExpansion = new Vector(double.NaN, double.NaN);

		// Token: 0x040008A8 RID: 2216
		private bool _isInitialRadiusSet;

		// Token: 0x040008A9 RID: 2217
		private double _initialRadius = 1.0;
	}
}
