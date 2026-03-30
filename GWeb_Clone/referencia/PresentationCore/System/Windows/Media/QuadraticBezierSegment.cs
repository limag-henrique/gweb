using System;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Cria uma curva quadrática de Bezier entre dois pontos em um <see cref="T:System.Windows.Media.PathFigure" />.</summary>
	// Token: 0x020003CE RID: 974
	public sealed class QuadraticBezierSegment : PathSegment
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.QuadraticBezierSegment" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06002683 RID: 9859 RVA: 0x000996E0 File Offset: 0x00098AE0
		public new QuadraticBezierSegment Clone()
		{
			return (QuadraticBezierSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.QuadraticBezierSegment" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06002684 RID: 9860 RVA: 0x000996F8 File Offset: 0x00098AF8
		public new QuadraticBezierSegment CloneCurrentValue()
		{
			return (QuadraticBezierSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o controle <see cref="T:System.Windows.Point" /> da curva.</summary>
		/// <returns>O ponto de controle deste <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00099710 File Offset: 0x00098B10
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x00099730 File Offset: 0x00098B30
		public Point Point1
		{
			get
			{
				return (Point)base.GetValue(QuadraticBezierSegment.Point1Property);
			}
			set
			{
				base.SetValueInternal(QuadraticBezierSegment.Point1Property, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Point" /> de extremidade desse <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</summary>
		/// <returns>O ponto de extremidade deste <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00099750 File Offset: 0x00098B50
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x00099770 File Offset: 0x00098B70
		public Point Point2
		{
			get
			{
				return (Point)base.GetValue(QuadraticBezierSegment.Point2Property);
			}
			set
			{
				base.SetValueInternal(QuadraticBezierSegment.Point2Property, value);
			}
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00099790 File Offset: 0x00098B90
		protected override Freezable CreateInstanceCore()
		{
			return new QuadraticBezierSegment();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000997A4 File Offset: 0x00098BA4
		static QuadraticBezierSegment()
		{
			Type typeFromHandle = typeof(QuadraticBezierSegment);
			QuadraticBezierSegment.Point1Property = Animatable.RegisterProperty("Point1", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
			QuadraticBezierSegment.Point2Property = Animatable.RegisterProperty("Point2", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</summary>
		// Token: 0x0600268B RID: 9867 RVA: 0x00099814 File Offset: 0x00098C14
		public QuadraticBezierSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.QuadraticBezierSegment" /> com os pontos de controle, ponto de extremidade e booliano indicando se esse <see cref="T:System.Windows.Media.QuadraticBezierSegment" /> deve ou não ser traçado.</summary>
		/// <param name="point1">O ponto de controle deste <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</param>
		/// <param name="point2">O ponto de extremidade deste <see cref="T:System.Windows.Media.QuadraticBezierSegment" />.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.QuadraticBezierSegment" /> deverá ser traçado; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600268C RID: 9868 RVA: 0x00099828 File Offset: 0x00098C28
		public QuadraticBezierSegment(Point point1, Point point2, bool isStroked)
		{
			this.Point1 = point1;
			this.Point2 = point2;
			base.IsStroked = isStroked;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x00099850 File Offset: 0x00098C50
		internal QuadraticBezierSegment(Point point1, Point point2, bool isStroked, bool isSmoothJoin)
		{
			this.Point1 = point1;
			this.Point2 = point2;
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00099880 File Offset: 0x00098C80
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			current = this.Point2;
			if (matrix.IsIdentity)
			{
				figure.Segments.Add(this);
				return;
			}
			Point point = this.Point1;
			point *= matrix;
			Point point2 = current;
			point2 *= matrix;
			figure.Segments.Add(new QuadraticBezierSegment(point, point2, base.IsStroked, base.IsSmoothJoin));
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000998EC File Offset: 0x00098CEC
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.QuadraticBezierTo(this.Point1, this.Point2, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00099918 File Offset: 0x00098D18
		internal override bool IsCurved()
		{
			return true;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00099928 File Offset: 0x00098D28
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"Q{1:",
				format,
				"}{0}{2:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this.Point1,
				this.Point2
			});
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.QuadraticBezierSegment.Point1" />.</summary>
		// Token: 0x040011CB RID: 4555
		public static readonly DependencyProperty Point1Property;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.QuadraticBezierSegment.Point2" />.</summary>
		// Token: 0x040011CC RID: 4556
		public static readonly DependencyProperty Point2Property;

		// Token: 0x040011CD RID: 4557
		internal static Point s_Point1;

		// Token: 0x040011CE RID: 4558
		internal static Point s_Point2;
	}
}
