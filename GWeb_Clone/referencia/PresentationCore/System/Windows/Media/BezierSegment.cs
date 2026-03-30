using System;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa uma curva de Bézier cúbica desenhada entre dois pontos.</summary>
	// Token: 0x02000364 RID: 868
	public sealed class BezierSegment : PathSegment
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BezierSegment" />.</summary>
		// Token: 0x06001D95 RID: 7573 RVA: 0x000791AC File Offset: 0x000785AC
		public BezierSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BezierSegment" /> com os pontos de controle, ponto final e opção de traço especificados.</summary>
		/// <param name="point1">O primeiro ponto de controle, que determina a parte inicial da curva.</param>
		/// <param name="point2">O segundo ponto de controle, que determina a parte final da curva.</param>
		/// <param name="point3">O ponto em que a curva é desenhada.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para traçar a curva quando um <see cref="T:System.Windows.Media.Pen" /> é usado para renderizar o segmento; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001D96 RID: 7574 RVA: 0x000791C0 File Offset: 0x000785C0
		public BezierSegment(Point point1, Point point2, Point point3, bool isStroked)
		{
			this.Point1 = point1;
			this.Point2 = point2;
			this.Point3 = point3;
			base.IsStroked = isStroked;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000791F0 File Offset: 0x000785F0
		internal BezierSegment(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin)
		{
			this.Point1 = point1;
			this.Point2 = point2;
			this.Point3 = point3;
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00079228 File Offset: 0x00078628
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			current = this.Point3;
			if (matrix.IsIdentity)
			{
				figure.Segments.Add(this);
				return;
			}
			Point point = this.Point1;
			point *= matrix;
			Point point2 = this.Point2;
			point2 *= matrix;
			Point point3 = current;
			point3 *= matrix;
			figure.Segments.Add(new BezierSegment(point, point2, point3, base.IsStroked, base.IsSmoothJoin));
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000792A4 File Offset: 0x000786A4
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.BezierTo(this.Point1, this.Point2, this.Point3, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000792D8 File Offset: 0x000786D8
		internal override bool IsCurved()
		{
			return true;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000792E8 File Offset: 0x000786E8
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"C{1:",
				format,
				"}{0}{2:",
				format,
				"}{0}{3:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this.Point1,
				this.Point2,
				this.Point3
			});
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.BezierSegment" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06001D9C RID: 7580 RVA: 0x00079374 File Offset: 0x00078774
		public new BezierSegment Clone()
		{
			return (BezierSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.BezierSegment" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06001D9D RID: 7581 RVA: 0x0007938C File Offset: 0x0007878C
		public new BezierSegment CloneCurrentValue()
		{
			return (BezierSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o primeiro ponto de controle da curva.</summary>
		/// <returns>O primeiro ponto de controle da curva.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x000793A4 File Offset: 0x000787A4
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x000793C4 File Offset: 0x000787C4
		public Point Point1
		{
			get
			{
				return (Point)base.GetValue(BezierSegment.Point1Property);
			}
			set
			{
				base.SetValueInternal(BezierSegment.Point1Property, value);
			}
		}

		/// <summary>Obtém ou define o segundo ponto de controle da curva.</summary>
		/// <returns>O segundo ponto de controle da curva.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000793E4 File Offset: 0x000787E4
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x00079404 File Offset: 0x00078804
		public Point Point2
		{
			get
			{
				return (Point)base.GetValue(BezierSegment.Point2Property);
			}
			set
			{
				base.SetValueInternal(BezierSegment.Point2Property, value);
			}
		}

		/// <summary>Obtém ou define o ponto final da curva.</summary>
		/// <returns>O ponto de extremidade da curva.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x00079424 File Offset: 0x00078824
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x00079444 File Offset: 0x00078844
		public Point Point3
		{
			get
			{
				return (Point)base.GetValue(BezierSegment.Point3Property);
			}
			set
			{
				base.SetValueInternal(BezierSegment.Point3Property, value);
			}
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00079464 File Offset: 0x00078864
		protected override Freezable CreateInstanceCore()
		{
			return new BezierSegment();
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00079478 File Offset: 0x00078878
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00079488 File Offset: 0x00078888
		static BezierSegment()
		{
			Type typeFromHandle = typeof(BezierSegment);
			BezierSegment.Point1Property = Animatable.RegisterProperty("Point1", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
			BezierSegment.Point2Property = Animatable.RegisterProperty("Point2", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
			BezierSegment.Point3Property = Animatable.RegisterProperty("Point3", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BezierSegment.Point1" />.</summary>
		// Token: 0x04000FE5 RID: 4069
		public static readonly DependencyProperty Point1Property;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BezierSegment.Point2" />.</summary>
		// Token: 0x04000FE6 RID: 4070
		public static readonly DependencyProperty Point2Property;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BezierSegment.Point3" />.</summary>
		// Token: 0x04000FE7 RID: 4071
		public static readonly DependencyProperty Point3Property;

		// Token: 0x04000FE8 RID: 4072
		internal static Point s_Point1;

		// Token: 0x04000FE9 RID: 4073
		internal static Point s_Point2;

		// Token: 0x04000FEA RID: 4074
		internal static Point s_Point3;
	}
}
