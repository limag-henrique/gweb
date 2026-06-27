using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa um conjunto de segmentos de linha definidos por um <see cref="T:System.Windows.Media.PointCollection" />, com cada <see cref="T:System.Windows.Point" /> especificando o ponto de extremidade de um segmento de linha.</summary>
	// Token: 0x020003CC RID: 972
	public sealed class PolyLineSegment : PathSegment
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PolyLineSegment" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002665 RID: 9829 RVA: 0x000991A0 File Offset: 0x000985A0
		public new PolyLineSegment Clone()
		{
			return (PolyLineSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PolyLineSegment" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002666 RID: 9830 RVA: 0x000991B8 File Offset: 0x000985B8
		public new PolyLineSegment CloneCurrentValue()
		{
			return (PolyLineSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a coleção de estruturas <see cref="T:System.Windows.Point" /> que define este objeto <see cref="T:System.Windows.Media.PolyLineSegment" />.</summary>
		/// <returns>A forma desse <see cref="T:System.Windows.Media.PolyLineSegment" /> objeto.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000991D0 File Offset: 0x000985D0
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x000991F0 File Offset: 0x000985F0
		public PointCollection Points
		{
			get
			{
				return (PointCollection)base.GetValue(PolyLineSegment.PointsProperty);
			}
			set
			{
				base.SetValueInternal(PolyLineSegment.PointsProperty, value);
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0009920C File Offset: 0x0009860C
		protected override Freezable CreateInstanceCore()
		{
			return new PolyLineSegment();
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x00099220 File Offset: 0x00098620
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00099230 File Offset: 0x00098630
		static PolyLineSegment()
		{
			Type typeFromHandle = typeof(PolyLineSegment);
			PolyLineSegment.PointsProperty = Animatable.RegisterProperty("Points", typeof(PointCollection), typeFromHandle, new FreezableDefaultValueFactory(PointCollection.Empty), null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyLineSegment" />.</summary>
		// Token: 0x0600266C RID: 9836 RVA: 0x0009927C File Offset: 0x0009867C
		public PolyLineSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PolyLineSegment" /> com a lista especificada de pontos que determinam os segmentos de linha e um valor que indica se os segmentos são traçados.</summary>
		/// <param name="points">Uma coleção de pontos que determinam os segmentos de linha do <see cref="T:System.Windows.Media.PolyLineSegment" />.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para tornar o segmento traçado; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600266D RID: 9837 RVA: 0x00099290 File Offset: 0x00098690
		public PolyLineSegment(IEnumerable<Point> points, bool isStroked)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000992C4 File Offset: 0x000986C4
		internal PolyLineSegment(IEnumerable<Point> points, bool isStroked, bool isSmoothJoin)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.Points = new PointCollection(points);
			base.IsStroked = isStroked;
			base.IsSmoothJoin = isSmoothJoin;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00099300 File Offset: 0x00098700
		internal override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			PointCollection points = this.Points;
			if (points != null && points.Count >= 1)
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
					figure.Segments.Add(new PolyLineSegment(pointCollection, base.IsStroked, base.IsSmoothJoin));
				}
				current = points.Internal_GetItem(points.Count - 1);
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000993A8 File Offset: 0x000987A8
		internal override bool IsEmpty()
		{
			return this.Points == null || this.Points.Count < 1;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000993D0 File Offset: 0x000987D0
		internal override bool IsCurved()
		{
			return false;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000993E0 File Offset: 0x000987E0
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.PolyLineTo(this.Points, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00099408 File Offset: 0x00098808
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.IsEmpty())
			{
				return "";
			}
			return "L" + this.Points.ConvertToString(format, provider);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PolyLineSegment.Points" />.</summary>
		// Token: 0x040011C7 RID: 4551
		public static readonly DependencyProperty PointsProperty;

		// Token: 0x040011C8 RID: 4552
		internal static PointCollection s_Points = PointCollection.Empty;
	}
}
