using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Representa um arco elíptico entre dois pontos.</summary>
	// Token: 0x02000363 RID: 867
	public sealed class ArcSegment : PathSegment
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ArcSegment" />.</summary>
		// Token: 0x06001D7F RID: 7551 RVA: 0x00078C5C File Offset: 0x0007805C
		public ArcSegment()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ArcSegment" />.</summary>
		/// <param name="point">O ponto de destino do arco; o ponto inicial do arco é definido como o ponto atual do <see cref="T:System.Windows.Media.PathFigure" /> ao qual o <see cref="T:System.Windows.Media.ArcSegment" /> é adicionado.</param>
		/// <param name="size">Os raios x e y do arco. O raio x é especificado pela estrutura <see cref="T:System.Windows.Size" />, propriedade <see cref="P:System.Windows.Size.Width" />; o raio y é especificado pela estrutura <see cref="T:System.Windows.Size" />, propriedade <see cref="P:System.Windows.Size.Height" />.</param>
		/// <param name="rotationAngle">A rotação do eixo x da elipse.</param>
		/// <param name="isLargeArc">Se o arco deve ser maior que 180 graus.</param>
		/// <param name="sweepDirection">Defina como <see cref="F:System.Windows.Media.SweepDirection.Clockwise" /> para desenhar o arco em uma orientação de ângulo positivo; defina como <see cref="F:System.Windows.Media.SweepDirection.Counterclockwise" /> para desenhar o arco em uma orientação de ângulo negativo.</param>
		/// <param name="isStroked">Defina como true para traçar o arco quando um <see cref="T:System.Windows.Media.Pen" /> for usado para renderizar o segmento; caso contrário, false.</param>
		// Token: 0x06001D80 RID: 7552 RVA: 0x00078C70 File Offset: 0x00078070
		public ArcSegment(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection, bool isStroked)
		{
			this.Size = size;
			this.RotationAngle = rotationAngle;
			this.IsLargeArc = isLargeArc;
			this.SweepDirection = sweepDirection;
			this.Point = point;
			base.IsStroked = isStroked;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00078CB0 File Offset: 0x000780B0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void AddToFigure(Matrix matrix, PathFigure figure, ref Point current)
		{
			Point point = this.Point;
			if (matrix.IsIdentity)
			{
				figure.Segments.Add(this);
				return;
			}
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)12) * (UIntPtr)sizeof(Point))];
			Size size = this.Size;
			double rotationAngle = this.RotationAngle;
			MilMatrix3x2D milMatrix3x2D = CompositionResourceManager.MatrixToMilMatrix3x2D(ref matrix);
			int num;
			MilCoreApi.MilUtility_ArcToBezier(current, size, rotationAngle, this.IsLargeArc, this.SweepDirection, point, &milMatrix3x2D, ptr, out num);
			Invariant.Assert(num <= 4);
			num = Math.Min(num, 4);
			bool isStroked = base.IsStroked;
			bool isSmoothJoin = base.IsSmoothJoin;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					figure.Segments.Add(new BezierSegment(ptr[3 * i], ptr[3 * i + 1], ptr[3 * i + 2], isStroked, i < num - 1 || isSmoothJoin));
				}
			}
			else if (num == 0)
			{
				figure.Segments.Add(new LineSegment(*ptr, isStroked, isSmoothJoin));
			}
			current = point;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00078DD4 File Offset: 0x000781D4
		internal override void SerializeData(StreamGeometryContext ctx)
		{
			ctx.ArcTo(this.Point, this.Size, this.RotationAngle, this.IsLargeArc, this.SweepDirection, base.IsStroked, base.IsSmoothJoin);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00078E14 File Offset: 0x00078214
		internal override bool IsCurved()
		{
			return true;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00078E24 File Offset: 0x00078224
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"A{1:",
				format,
				"}{0}{2:",
				format,
				"}{0}{3}{0}{4}{0}{5:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this.Size,
				this.RotationAngle,
				this.IsLargeArc ? "1" : "0",
				(this.SweepDirection == SweepDirection.Clockwise) ? "1" : "0",
				this.Point
			});
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00078EE0 File Offset: 0x000782E0
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00078EF0 File Offset: 0x000782F0
		private static object CoerceSize(DependencyObject d, object value)
		{
			if (((Size)value).IsEmpty)
			{
				return new Size(0.0, 0.0);
			}
			return value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.ArcSegment" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06001D87 RID: 7559 RVA: 0x00078F2C File Offset: 0x0007832C
		public new ArcSegment Clone()
		{
			return (ArcSegment)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.ArcSegment" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06001D88 RID: 7560 RVA: 0x00078F44 File Offset: 0x00078344
		public new ArcSegment CloneCurrentValue()
		{
			return (ArcSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define o ponto de extremidade do arco elíptico.</summary>
		/// <returns>O ponto em que o arco é desenhado. O valor padrão é (0,0).</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00078F5C File Offset: 0x0007835C
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x00078F7C File Offset: 0x0007837C
		public Point Point
		{
			get
			{
				return (Point)base.GetValue(ArcSegment.PointProperty);
			}
			set
			{
				base.SetValueInternal(ArcSegment.PointProperty, value);
			}
		}

		/// <summary>Obtém ou define o raio x e y do arco como uma estrutura <see cref="T:System.Windows.Size" />.</summary>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Size" /> que descreve o raio x e y do arco elíptico. O <see cref="T:System.Windows.Size" /> da estrutura <see cref="P:System.Windows.Size.Width" /> propriedade especifica o raio x do arco; sua <see cref="P:System.Windows.Size.Height" /> propriedade especifica o raio y do arco. O valor padrão é 0,0.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00078F9C File Offset: 0x0007839C
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x00078FBC File Offset: 0x000783BC
		public Size Size
		{
			get
			{
				return (Size)base.GetValue(ArcSegment.SizeProperty);
			}
			set
			{
				base.SetValueInternal(ArcSegment.SizeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor (em graus) pelo qual a elipse é girada sobre o eixo x.</summary>
		/// <returns>O valor (em graus) pelo qual a elipse é girada sobre o eixo x. O valor padrão é 0.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00078FDC File Offset: 0x000783DC
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00078FFC File Offset: 0x000783FC
		public double RotationAngle
		{
			get
			{
				return (double)base.GetValue(ArcSegment.RotationAngleProperty);
			}
			set
			{
				base.SetValueInternal(ArcSegment.RotationAngleProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o arco deve ser maior que 180 graus.</summary>
		/// <returns>
		///   <see langword="true" /> Se o arco deve ser maior que 180 graus; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0007901C File Offset: 0x0007841C
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x0007903C File Offset: 0x0007843C
		public bool IsLargeArc
		{
			get
			{
				return (bool)base.GetValue(ArcSegment.IsLargeArcProperty);
			}
			set
			{
				base.SetValueInternal(ArcSegment.IsLargeArcProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define um valor que especifica se o arco é desenhado na direção <see cref="F:System.Windows.Media.SweepDirection.Clockwise" /> ou <see cref="F:System.Windows.Media.SweepDirection.Counterclockwise" />.</summary>
		/// <returns>Um valor que especifica a direção na qual o arco é desenhado. O valor padrão é <see cref="F:System.Windows.Media.SweepDirection.Counterclockwise" />.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0007905C File Offset: 0x0007845C
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0007907C File Offset: 0x0007847C
		public SweepDirection SweepDirection
		{
			get
			{
				return (SweepDirection)base.GetValue(ArcSegment.SweepDirectionProperty);
			}
			set
			{
				base.SetValueInternal(ArcSegment.SweepDirectionProperty, value);
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0007909C File Offset: 0x0007849C
		protected override Freezable CreateInstanceCore()
		{
			return new ArcSegment();
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000790B0 File Offset: 0x000784B0
		static ArcSegment()
		{
			Type typeFromHandle = typeof(ArcSegment);
			ArcSegment.PointProperty = Animatable.RegisterProperty("Point", typeof(Point), typeFromHandle, default(Point), null, null, false, null);
			ArcSegment.SizeProperty = Animatable.RegisterProperty("Size", typeof(Size), typeFromHandle, default(Size), null, null, false, new CoerceValueCallback(ArcSegment.CoerceSize));
			ArcSegment.RotationAngleProperty = Animatable.RegisterProperty("RotationAngle", typeof(double), typeFromHandle, 0.0, null, null, false, null);
			ArcSegment.IsLargeArcProperty = Animatable.RegisterProperty("IsLargeArc", typeof(bool), typeFromHandle, false, null, null, false, null);
			ArcSegment.SweepDirectionProperty = Animatable.RegisterProperty("SweepDirection", typeof(SweepDirection), typeFromHandle, SweepDirection.Counterclockwise, null, new ValidateValueCallback(ValidateEnums.IsSweepDirectionValid), false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ArcSegment.Point" />.</summary>
		// Token: 0x04000FDB RID: 4059
		public static readonly DependencyProperty PointProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ArcSegment.Size" />.</summary>
		// Token: 0x04000FDC RID: 4060
		public static readonly DependencyProperty SizeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ArcSegment.RotationAngle" />.</summary>
		// Token: 0x04000FDD RID: 4061
		public static readonly DependencyProperty RotationAngleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ArcSegment.IsLargeArc" />.</summary>
		// Token: 0x04000FDE RID: 4062
		public static readonly DependencyProperty IsLargeArcProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ArcSegment.SweepDirection" />.</summary>
		// Token: 0x04000FDF RID: 4063
		public static readonly DependencyProperty SweepDirectionProperty;

		// Token: 0x04000FE0 RID: 4064
		internal static Point s_Point;

		// Token: 0x04000FE1 RID: 4065
		internal static Size s_Size;

		// Token: 0x04000FE2 RID: 4066
		internal const double c_RotationAngle = 0.0;

		// Token: 0x04000FE3 RID: 4067
		internal const bool c_IsLargeArc = false;

		// Token: 0x04000FE4 RID: 4068
		internal const SweepDirection c_SweepDirection = SweepDirection.Counterclockwise;
	}
}
