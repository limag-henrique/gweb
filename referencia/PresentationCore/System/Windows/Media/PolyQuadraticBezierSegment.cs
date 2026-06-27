using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa um conjunto de segmentos de Bézier quadráticos.</summary>
	// Token: 0x020003CD RID: 973
	public sealed class PolyQuadraticBezierSegment : PathSegment
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002674 RID: 9844 RVA: 0x0009943C File Offset: 0x0009883C
		public new PolyQuadraticBezierSegment Clone()
		{
			return (PolyQuadraticBezierSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002675 RID: 9845 RVA: 0x00099454 File Offset: 0x00098854
		public new PolyQuadraticBezierSegment CloneCurrentValue()
		{
			return (PolyQuadraticBezierSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.PointCollection" /> que define este objeto <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" />.</summary>
		/// <returns>Uma coleção que define a forma desse <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" /> objeto. O valor padrão é uma coleção vazia.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x0009946C File Offset: 0x0009886C
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x0009948C File Offset: 0x0009888C
		public PointCollection Points
		{
			get
			{
				return (PointCollection)base.GetValue(PolyQuadraticBezierSegment.PointsProperty);
			}
			set
			{
				base.SetValueInternal(PolyQuadraticBezierSegment.PointsProperty, value);
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000994A8 File Offset: 0x000988A8
		protected override Freezable CreateInstanceCore()
		{
			return new PolyQuadraticBezierSegment();
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000994BC File Offset: 0x000988BC
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000994CC File Offset: 0x000988CC
		static PolyQuadraticBezierSegment()
		{
			Type typeFromHandle = typeof(PolyQuadraticBezierSegment);
			PolyQuadraticBezierSegment.PointsProperty = Animatable.RegisterProperty("Points", typeof(PointCollection), typeFromHandle, new FreezableDefaultValueFactory(PointCollection.Empty), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" />.</summary>
		// Token: 0x0600267B RID: 9851 RVA: 0x00099518 File Offset: 0x00098918
		public PolyQuadraticBezierSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyQuadraticBezierSegment" /> com a coleção especificada de objetos <see cref="T:System.Windows.Point" /> e um valor que especifica se os segmentos são traçados.</summary>
		/// <param name="points">A coleção de pontos que especificam a geometria dos segmentos de curva de Bézier.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para traçar os segmentos; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600267C RID: 9852 RVA: 0x0009952C File Offset: 0x0009892C
		public PolyQuadraticBezierSegment(IEnumerable<Point> points, bool isStroked)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00099560 File Offset: 0x00098960
		internal PolyQuadraticBezierSegment(IEnumerable<Point> points, bool isStroked, bool isSmoothJoin)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0009959C File Offset: 0x0009899C
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			PointCollection points = this.Points;
			if (points != null && points.Count >= 2)
			{
				if (matrix.IsIdentity)
				{
					figure.Segments.Add(this);
				}
				else
				{
					PointCollection pointCollection = new PointCollection();
					Point point = default(Point);
					int count = points.Count;
					for (int i = 0; i < count; i++)
					{
						point = points.Internal_GetItem(i);
						point *= matrix;
						pointCollection.Add(point);
					}
					figure.Segments.Add(new PolyQuadraticBezierSegment(pointCollection, base.IsStroked, base.IsSmoothJoin));
				}
				current = points.Internal_GetItem(points.Count - 1);
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00099644 File Offset: 0x00098A44
		internal override bool IsEmpty()
		{
			return this.Points == null || this.Points.Count < 2;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0009966C File Offset: 0x00098A6C
		internal override bool IsCurved()
		{
			return !this.IsEmpty();
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00099684 File Offset: 0x00098A84
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.PolyQuadraticBezierTo(this.Points, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000996AC File Offset: 0x00098AAC
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.Points == null)
			{
				return "";
			}
			return "Q" + this.Points.ConvertToString(format, provider);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PolyQuadraticBezierSegment.Points" />.</summary>
		// Token: 0x040011C9 RID: 4553
		public static readonly DependencyProperty PointsProperty;

		// Token: 0x040011CA RID: 4554
		internal static PointCollection s_Points = PointCollection.Empty;
	}
}
