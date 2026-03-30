using System;
using System.Windows.Media.Animation;

namespace System.Windows.Media
{
	/// <summary>Cria uma linha entre dois pontos em um <see cref="T:System.Windows.Media.PathFigure" />.</summary>
	// Token: 0x020003BD RID: 957
	public sealed class LineSegment : PathSegment
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.LineSegment" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002502 RID: 9474 RVA: 0x00094580 File Offset: 0x00093980
		public new LineSegment Clone()
		{
			return (LineSegment)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.LineSegment" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> será <see langword="false" /> mesmo que a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem seja <see langword="true" />.</returns>
		// Token: 0x06002503 RID: 9475 RVA: 0x00094598 File Offset: 0x00093998
		public new LineSegment CloneCurrentValue()
		{
			return (LineSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o ponto de extremidade do segmento de linha.</summary>
		/// <returns>O ponto de extremidade do segmento de linha.</returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000945B0 File Offset: 0x000939B0
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x000945D0 File Offset: 0x000939D0
		public Point Point
		{
			get
			{
				return (Point)base.GetValue(LineSegment.PointProperty);
			}
			set
			{
				base.SetValueInternal(LineSegment.PointProperty, value);
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000945F0 File Offset: 0x000939F0
		protected override Freezable CreateInstanceCore()
		{
			return new LineSegment();
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00094604 File Offset: 0x00093A04
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00094614 File Offset: 0x00093A14
		static LineSegment()
		{
			Type typeFromHandle = typeof(LineSegment);
			LineSegment.PointProperty = Animatable.RegisterProperty("Point", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LineSegment" />.</summary>
		// Token: 0x06002509 RID: 9481 RVA: 0x00094658 File Offset: 0x00093A58
		public LineSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LineSegment" /> que tem o <see cref="T:System.Windows.Point" /> final especificado e um booliano que determina se este <see cref="T:System.Windows.Media.LineSegment" /> é traçado.</summary>
		/// <param name="point">O ponto de extremidade deste <see cref="T:System.Windows.Media.LineSegment" />.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para traçar este <see cref="T:System.Windows.Media.LineSegment" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600250A RID: 9482 RVA: 0x0009466C File Offset: 0x00093A6C
		public LineSegment(Point point, bool isStroked)
		{
			this.Point = point;
			base.IsStroked = isStroked;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00094690 File Offset: 0x00093A90
		internal LineSegment(Point point, bool isStroked, bool isSmoothJoin)
		{
			this.Point = point;
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000946B8 File Offset: 0x00093AB8
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			current = this.Point;
			if (matrix.IsIdentity)
			{
				figure.Segments.Add(this);
				return;
			}
			Point point = current;
			point *= matrix;
			figure.Segments.Add(new LineSegment(point, base.IsStroked, base.IsSmoothJoin));
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00094714 File Offset: 0x00093B14
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.LineTo(this.Point, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0009473C File Offset: 0x00093B3C
		internal override bool IsCurved()
		{
			return false;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0009474C File Offset: 0x00093B4C
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			return "L" + ((IFormattable)this.Point).ToString(format, provider);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.LineSegment.Point" />.</summary>
		// Token: 0x04001185 RID: 4485
		public static readonly DependencyProperty PointProperty;

		// Token: 0x04001186 RID: 4486
		internal static Point s_Point;
	}
}
