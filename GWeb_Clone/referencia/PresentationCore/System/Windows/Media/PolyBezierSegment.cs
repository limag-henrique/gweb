using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa uma ou mais curvas de Bézier cúbicas.</summary>
	// Token: 0x020003CB RID: 971
	public sealed class PolyBezierSegment : PathSegment
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PolyBezierSegment" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002656 RID: 9814 RVA: 0x00098EFC File Offset: 0x000982FC
		public new PolyBezierSegment Clone()
		{
			return (PolyBezierSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PolyBezierSegment" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002657 RID: 9815 RVA: 0x00098F14 File Offset: 0x00098314
		public new PolyBezierSegment CloneCurrentValue()
		{
			return (PolyBezierSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define os <see cref="T:System.Windows.Media.PointCollection" /> que definem este objeto <see cref="T:System.Windows.Media.PolyBezierSegment" />.</summary>
		/// <returns>Coleção que definem este <see cref="T:System.Windows.Media.PolyBezierSegment" /> objeto.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00098F2C File Offset: 0x0009832C
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x00098F4C File Offset: 0x0009834C
		public PointCollection Points
		{
			get
			{
				return (PointCollection)base.GetValue(PolyBezierSegment.PointsProperty);
			}
			set
			{
				base.SetValueInternal(PolyBezierSegment.PointsProperty, value);
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00098F68 File Offset: 0x00098368
		protected override Freezable CreateInstanceCore()
		{
			return new PolyBezierSegment();
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00098F7C File Offset: 0x0009837C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00098F8C File Offset: 0x0009838C
		static PolyBezierSegment()
		{
			Type typeFromHandle = typeof(PolyBezierSegment);
			PolyBezierSegment.PointsProperty = Animatable.RegisterProperty("Points", typeof(PointCollection), typeFromHandle, new FreezableDefaultValueFactory(PointCollection.Empty), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyBezierSegment" />.</summary>
		// Token: 0x0600265D RID: 9821 RVA: 0x00098FD8 File Offset: 0x000983D8
		public PolyBezierSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyBezierSegment" /> com a coleção especificada de objetos <see cref="T:System.Windows.Point" /> e um valor que especifica se os segmentos são traçados.</summary>
		/// <param name="points">A coleção de pontos que especificam a geometria dos segmentos de curva de Bézier.</param>
		/// <param name="isStroked">Valor que especifica se os segmentos são traçados.</param>
		// Token: 0x0600265E RID: 9822 RVA: 0x00098FEC File Offset: 0x000983EC
		public PolyBezierSegment(IEnumerable<Point> points, bool isStroked)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00099020 File Offset: 0x00098420
		internal PolyBezierSegment(IEnumerable<Point> points, bool isStroked, bool isSmoothJoin)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0009905C File Offset: 0x0009845C
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			PointCollection points = this.Points;
			if (points != null && points.Count >= 3)
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
					figure.Segments.Add(new PolyBezierSegment(pointCollection, base.IsStroked, base.IsSmoothJoin));
				}
				current = points.Internal_GetItem(points.Count - 1);
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00099104 File Offset: 0x00098504
		internal override bool IsEmpty()
		{
			return this.Points == null || this.Points.Count < 3;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0009912C File Offset: 0x0009852C
		internal override bool IsCurved()
		{
			return !this.IsEmpty();
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00099144 File Offset: 0x00098544
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.PolyBezierTo(this.Points, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0009916C File Offset: 0x0009856C
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.Points == null)
			{
				return "";
			}
			return "C" + this.Points.ConvertToString(format, provider);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PolyBezierSegment.Points" />.</summary>
		// Token: 0x040011C5 RID: 4549
		public static readonly DependencyProperty PointsProperty;

		// Token: 0x040011C6 RID: 4550
		internal static PointCollection s_Points = PointCollection.Empty;
	}
}
