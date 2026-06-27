using System;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma subseção de uma geometria, uma única série conectada de segmentos geométricos bidimensionais.</summary>
	// Token: 0x020003C0 RID: 960
	[ContentProperty("Segments")]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class PathFigure : Animatable, IFormattable
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PathFigure" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002540 RID: 9536 RVA: 0x00094E24 File Offset: 0x00094224
		public new PathFigure Clone()
		{
			return (PathFigure)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PathFigure" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002541 RID: 9537 RVA: 0x00094E3C File Offset: 0x0009423C
		public new PathFigure CloneCurrentValue()
		{
			return (PathFigure)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Point" /> em que o <see cref="T:System.Windows.Media.PathFigure" /> é iniciado.</summary>
		/// <returns>O <see cref="T:System.Windows.Point" /> no qual o <see cref="T:System.Windows.Media.PathFigure" /> é iniciado. O valor padrão é 0,0.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00094E54 File Offset: 0x00094254
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x00094E74 File Offset: 0x00094274
		public Point StartPoint
		{
			get
			{
				return (Point)base.GetValue(PathFigure.StartPointProperty);
			}
			set
			{
				base.SetValueInternal(PathFigure.StartPointProperty, value);
			}
		}

		/// <summary>Obtém ou define se a área independente deste <see cref="T:System.Windows.Media.PathFigure" /> deve ser usada para testes de clique, renderização e recorte.</summary>
		/// <returns>Determina se a área independente deste <see cref="T:System.Windows.Media.PathFigure" /> deve ser usada para teste de clique, renderização e recorte.  O valor padrão é <see langword="true" />.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x00094E94 File Offset: 0x00094294
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x00094EB4 File Offset: 0x000942B4
		public bool IsFilled
		{
			get
			{
				return (bool)base.GetValue(PathFigure.IsFilledProperty);
			}
			set
			{
				base.SetValueInternal(PathFigure.IsFilledProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define a coleção de segmentos que definem a forma desse objeto <see cref="T:System.Windows.Media.PathFigure" />.</summary>
		/// <returns>A coleção de segmentos que definem a forma desse objeto <see cref="T:System.Windows.Media.PathFigure" />. O valor padrão é uma coleção vazia.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x00094ED4 File Offset: 0x000942D4
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x00094EF4 File Offset: 0x000942F4
		public PathSegmentCollection Segments
		{
			get
			{
				return (PathSegmentCollection)base.GetValue(PathFigure.SegmentsProperty);
			}
			set
			{
				base.SetValueInternal(PathFigure.SegmentsProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica se essas figuras do primeiro e último segmentos estão conectadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se esta figura do primeiro e último segmentos estão conectadas; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x00094F10 File Offset: 0x00094310
		// (set) Token: 0x06002549 RID: 9545 RVA: 0x00094F30 File Offset: 0x00094330
		public bool IsClosed
		{
			get
			{
				return (bool)base.GetValue(PathFigure.IsClosedProperty);
			}
			set
			{
				base.SetValueInternal(PathFigure.IsClosedProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00094F50 File Offset: 0x00094350
		protected override Freezable CreateInstanceCore()
		{
			return new PathFigure();
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x00094F64 File Offset: 0x00094364
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00094F74 File Offset: 0x00094374
		static PathFigure()
		{
			Type typeFromHandle = typeof(PathFigure);
			PathFigure.StartPointProperty = Animatable.RegisterProperty("StartPoint", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
			PathFigure.IsFilledProperty = Animatable.RegisterProperty("IsFilled", typeof(bool), typeFromHandle, true, null, null, false, null);
			PathFigure.SegmentsProperty = Animatable.RegisterProperty("Segments", typeof(PathSegmentCollection), typeFromHandle, new FreezableDefaultValueFactory(PathSegmentCollection.Empty), null, null, false, null);
			PathFigure.IsClosedProperty = Animatable.RegisterProperty("IsClosed", typeof(bool), typeFromHandle, false, null, null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathFigure" />.</summary>
		// Token: 0x0600254D RID: 9549 RVA: 0x00095040 File Offset: 0x00094440
		public PathFigure()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathFigure" /> com os valores <see cref="P:System.Windows.Media.PathFigure.StartPoint" />, <see cref="P:System.Windows.Media.PathFigure.Segments" /> e <see cref="P:System.Windows.Media.PathFigure.IsClosed" /> especificados.</summary>
		/// <param name="start">O <see cref="P:System.Windows.Media.PathFigure.StartPoint" /> para o <see cref="T:System.Windows.Media.PathFigure" />.</param>
		/// <param name="segments">O <see cref="P:System.Windows.Media.PathFigure.Segments" /> para o <see cref="T:System.Windows.Media.PathFigure" />.</param>
		/// <param name="closed">O <see cref="P:System.Windows.Media.PathFigure.IsClosed" /> para o <see cref="T:System.Windows.Media.PathFigure" />.</param>
		// Token: 0x0600254E RID: 9550 RVA: 0x00095054 File Offset: 0x00094454
		public PathFigure(Point start, IEnumerable<PathSegment> segments, bool closed)
		{
			this.StartPoint = start;
			PathSegmentCollection segments2 = this.Segments;
			if (segments != null)
			{
				using (IEnumerator<PathSegment> enumerator = segments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PathSegment value = enumerator.Current;
						segments2.Add(value);
					}
					goto IL_4D;
				}
				goto IL_42;
				IL_4D:
				this.IsClosed = closed;
				return;
			}
			IL_42:
			throw new ArgumentNullException("segments");
		}

		/// <summary>Obtém um objeto <see cref="T:System.Windows.Media.PathFigure" />, dentro do erro de tolerância especificado, que é uma aproximação poligonal deste objeto <see cref="T:System.Windows.Media.PathFigure" />.</summary>
		/// <param name="tolerance">A tolerância computacional a erros.</param>
		/// <param name="type">Especifica como o erro de tolerância é interpretado.</param>
		/// <returns>A aproximação poligonal deste objeto <see cref="T:System.Windows.Media.PathFigure" />.</returns>
		// Token: 0x0600254F RID: 9551 RVA: 0x000950D4 File Offset: 0x000944D4
		public PathFigure GetFlattenedPathFigure(double tolerance, ToleranceType type)
		{
			PathGeometry flattenedPathGeometry = new PathGeometry
			{
				Figures = 
				{
					this
				}
			}.GetFlattenedPathGeometry(tolerance, type);
			int count = flattenedPathGeometry.Figures.Count;
			if (count == 0)
			{
				return new PathFigure();
			}
			if (count == 1)
			{
				return flattenedPathGeometry.Figures[0];
			}
			throw new InvalidOperationException(SR.Get("PathGeometry_InternalReadBackError"));
		}

		/// <summary>Obtém um objeto <see cref="T:System.Windows.Media.PathFigure" /> que é uma aproximação poligonal do objeto <see cref="T:System.Windows.Media.PathFigure" />.</summary>
		/// <returns>A aproximação poligonal deste objeto <see cref="T:System.Windows.Media.PathFigure" />.</returns>
		// Token: 0x06002550 RID: 9552 RVA: 0x00095134 File Offset: 0x00094534
		public PathFigure GetFlattenedPathFigure()
		{
			return this.GetFlattenedPathFigure(Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.PathFigure" /> pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> caso este objeto <see cref="T:System.Windows.Media.PathFigure" /> possa ter segmentos de curva; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002551 RID: 9553 RVA: 0x00095150 File Offset: 0x00094550
		public bool MayHaveCurves()
		{
			PathSegmentCollection segments = this.Segments;
			if (segments == null)
			{
				return false;
			}
			int count = segments.Count;
			for (int i = 0; i < count; i++)
			{
				if (segments.Internal_GetItem(i).IsCurved())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00095190 File Offset: 0x00094590
		internal PathFigure GetTransformedCopy(Matrix matrix)
		{
			PathSegmentCollection segments = this.Segments;
			PathFigure pathFigure = new PathFigure();
			Point startPoint = this.StartPoint;
			pathFigure.StartPoint = startPoint * matrix;
			if (segments != null)
			{
				int count = segments.Count;
				for (int i = 0; i < count; i++)
				{
					segments.Internal_GetItem(i).AddToFigure(matrix, pathFigure, ref startPoint);
				}
			}
			pathFigure.IsClosed = this.IsClosed;
			pathFigure.IsFilled = this.IsFilled;
			return pathFigure;
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse objeto.</summary>
		/// <returns>Uma representação da cadeia de caracteres desse <see cref="T:System.Windows.Media.PathFigure" />.</returns>
		// Token: 0x06002553 RID: 9555 RVA: 0x00095200 File Offset: 0x00094600
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação da cadeia de caracteres deste objeto usando a formatação específica da cultura especificada.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura; caso contrário, <see langword="null" /> para usar as configurações de formatação padrão e da cultura atuais.</param>
		/// <returns>Uma representação de cadeia de caracteres formatada deste <see cref="T:System.Windows.Media.PathFigure" />.</returns>
		// Token: 0x06002554 RID: 9556 RVA: 0x0009521C File Offset: 0x0009461C
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002555 RID: 9557 RVA: 0x00095238 File Offset: 0x00094638
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00095254 File Offset: 0x00094654
		internal bool CanSerializeToString()
		{
			PathSegmentCollection segments = this.Segments;
			return this.IsFilled && (segments == null || segments.CanSerializeToString());
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00095280 File Offset: 0x00094680
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			PathSegmentCollection segments = this.Segments;
			return "M" + ((IFormattable)this.StartPoint).ToString(format, provider) + ((segments != null) ? segments.ConvertToString(format, provider) : "") + (this.IsClosed ? "z" : "");
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000952D8 File Offset: 0x000946D8
		internal void SerializeData(StreamGeometryContext ctx)
		{
			ctx.BeginFigure(this.StartPoint, this.IsFilled, this.IsClosed);
			PathSegmentCollection segments = this.Segments;
			int num = (segments == null) ? 0 : segments.Count;
			for (int i = 0; i < num; i++)
			{
				segments.Internal_GetItem(i).SerializeData(ctx);
			}
		}

		/// <summary>O identificador da propriedade de dependência <see cref="P:System.Windows.Media.PathFigure.StartPoint" />.</summary>
		// Token: 0x0400118F RID: 4495
		public static readonly DependencyProperty StartPointProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathFigure.IsFilled" />.</summary>
		// Token: 0x04001190 RID: 4496
		public static readonly DependencyProperty IsFilledProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathFigure.Segments" />.</summary>
		// Token: 0x04001191 RID: 4497
		public static readonly DependencyProperty SegmentsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathFigure.IsClosed" />.</summary>
		// Token: 0x04001192 RID: 4498
		public static readonly DependencyProperty IsClosedProperty;

		// Token: 0x04001193 RID: 4499
		internal static Point s_StartPoint = default(Point);

		// Token: 0x04001194 RID: 4500
		internal const bool c_IsFilled = true;

		// Token: 0x04001195 RID: 4501
		internal static PathSegmentCollection s_Segments = PathSegmentCollection.Empty;

		// Token: 0x04001196 RID: 4502
		internal const bool c_IsClosed = false;
	}
}
