using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa a geometria de um círculo ou elipse.</summary>
	// Token: 0x0200038B RID: 907
	public sealed class EllipseGeometry : Geometry
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.EllipseGeometry" />.</summary>
		// Token: 0x06002193 RID: 8595 RVA: 0x00087C4C File Offset: 0x0008704C
		public EllipseGeometry()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.EllipseGeometry" /> que tem um diâmetro horizontal igual à largura do <see cref="T:System.Windows.Rect" /> passado, um diâmetro vertical igual ao comprimento do <see cref="T:System.Windows.Rect" /> passado e um local de ponto central igual ao centro do <see cref="T:System.Windows.Rect" /> passado.</summary>
		/// <param name="rect">O retângulo que descreve as dimensões da elipse.</param>
		// Token: 0x06002194 RID: 8596 RVA: 0x00087C60 File Offset: 0x00087060
		public EllipseGeometry(Rect rect)
		{
			if (rect.IsEmpty)
			{
				throw new ArgumentException(SR.Get("Rect_Empty", new object[]
				{
					"rect"
				}));
			}
			this.RadiusX = (rect.Right - rect.X) * 0.5;
			this.RadiusY = (rect.Bottom - rect.Y) * 0.5;
			this.Center = new Point(rect.X + this.RadiusX, rect.Y + this.RadiusY);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.EllipseGeometry" /> como uma elipse que tem o local central, o raio x e o raio y especificados.</summary>
		/// <param name="center">A localização do centro da elipse.</param>
		/// <param name="radiusX">O raio horizontal da elipse.</param>
		/// <param name="radiusY">O raio vertical da elipse.</param>
		// Token: 0x06002195 RID: 8597 RVA: 0x00087D00 File Offset: 0x00087100
		public EllipseGeometry(Point center, double radiusX, double radiusY)
		{
			this.Center = center;
			this.RadiusX = radiusX;
			this.RadiusY = radiusY;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.EllipseGeometry" /> que tem a posição, o tamanho e a transformação especificados.</summary>
		/// <param name="center">A localização do centro da elipse.</param>
		/// <param name="radiusX">O raio horizontal da elipse.</param>
		/// <param name="radiusY">O raio vertical da elipse.</param>
		/// <param name="transform">A transformação a ser aplicada à elipse.</param>
		// Token: 0x06002196 RID: 8598 RVA: 0x00087D28 File Offset: 0x00087128
		public EllipseGeometry(Point center, double radiusX, double radiusY, Transform transform) : this(center, radiusX, radiusY)
		{
			base.Transform = transform;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> que representa a caixa delimitadora desse <see cref="T:System.Windows.Media.EllipseGeometry" />. Esse método não considera a área extra potencialmente adicionada por um traço.</summary>
		/// <returns>A caixa delimitadora do <see cref="T:System.Windows.Media.EllipseGeometry" />.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00087D48 File Offset: 0x00087148
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				Transform transform = base.Transform;
				Rect boundsHelper;
				if (transform == null || transform.IsIdentity)
				{
					Point center = this.Center;
					double radiusX = this.RadiusX;
					double radiusY = this.RadiusY;
					boundsHelper = new Rect(center.X - Math.Abs(radiusX), center.Y - Math.Abs(radiusY), 2.0 * Math.Abs(radiusX), 2.0 * Math.Abs(radiusY));
				}
				else
				{
					Matrix geometryMatrix;
					Transform.GetTransformValue(transform, out geometryMatrix);
					boundsHelper = EllipseGeometry.GetBoundsHelper(null, Matrix.Identity, this.Center, this.RadiusX, this.RadiusY, geometryMatrix, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
				}
				return boundsHelper;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00087DF8 File Offset: 0x000871F8
		internal override Rect GetBoundsInternal(Pen pen, Matrix matrix, double tolerance, ToleranceType type)
		{
			Matrix geometryMatrix;
			Transform.GetTransformValue(base.Transform, out geometryMatrix);
			return EllipseGeometry.GetBoundsHelper(pen, matrix, this.Center, this.RadiusX, this.RadiusY, geometryMatrix, tolerance, type);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00087E30 File Offset: 0x00087230
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe static Rect GetBoundsHelper(Pen pen, Matrix worldMatrix, Point center, double radiusX, double radiusY, Matrix geometryMatrix, double tolerance, ToleranceType type)
		{
			Rect boundsHelper;
			if ((pen == null || pen.DoesNotContainGaps) && worldMatrix.IsIdentity && geometryMatrix.IsIdentity)
			{
				double num = 0.0;
				if (Pen.ContributesToBounds(pen))
				{
					num = Math.Abs(pen.Thickness);
				}
				boundsHelper = new Rect(center.X - Math.Abs(radiusX) - 0.5 * num, center.Y - Math.Abs(radiusY) - 0.5 * num, 2.0 * Math.Abs(radiusX) + num, 2.0 * Math.Abs(radiusY) + num);
			}
			else
			{
				Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)13) * (UIntPtr)sizeof(Point))];
				EllipseGeometry.GetPointList(ptr, 13U, center, radiusX, radiusY);
				byte[] array;
				byte* pTypes;
				if ((array = EllipseGeometry.s_roundedPathTypes) == null || array.Length == 0)
				{
					pTypes = null;
				}
				else
				{
					pTypes = &array[0];
				}
				boundsHelper = Geometry.GetBoundsHelper(pen, &worldMatrix, ptr, pTypes, 13U, 4U, &geometryMatrix, tolerance, type, false);
				array = null;
			}
			return boundsHelper;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00087F38 File Offset: 0x00087338
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)this.GetPointCount()) * (UIntPtr)sizeof(Point))];
			EllipseGeometry.GetPointList(ptr, this.GetPointCount(), this.Center, this.RadiusX, this.RadiusY);
			byte[] typeList;
			byte* pTypes;
			if ((typeList = this.GetTypeList()) == null || typeList.Length == 0)
			{
				pTypes = null;
			}
			else
			{
				pTypes = &typeList[0];
			}
			return base.ContainsInternal(pen, hitPoint, tolerance, type, ptr, this.GetPointCount(), pTypes, this.GetSegmentCount());
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.EllipseGeometry" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.EllipseGeometry" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600219B RID: 8603 RVA: 0x00087FAC File Offset: 0x000873AC
		public override bool IsEmpty()
		{
			return false;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.EllipseGeometry" /> pode ter em segmentos de curva.</summary>
		/// <returns>
		///   <see langword="true" /> se esse objeto <see cref="T:System.Windows.Media.EllipseGeometry" /> pode ter segmentos de curva, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600219C RID: 8604 RVA: 0x00087FBC File Offset: 0x000873BC
		public override bool MayHaveCurves()
		{
			return true;
		}

		/// <summary>Obtém a área dessa <see cref="T:System.Windows.Media.EllipseGeometry" />.</summary>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores de enumeração, <see cref="F:System.Windows.Media.ToleranceType.Absolute" /> ou <see cref="F:System.Windows.Media.ToleranceType.Relative" />, que especifica se o fator de tolerância é um valor absoluto ou relativo à área dessa geometria.</param>
		/// <returns>A área da região preenchida da elipse.</returns>
		// Token: 0x0600219D RID: 8605 RVA: 0x00087FCC File Offset: 0x000873CC
		public override double GetArea(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			double num = Math.Abs(this.RadiusX * this.RadiusY) * 3.1415926535897931;
			Transform transform = base.Transform;
			if (transform != null && !transform.IsIdentity)
			{
				num *= Math.Abs(transform.Value.Determinant);
			}
			return num;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00088028 File Offset: 0x00087428
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			Point[] pointList = this.GetPointList();
			Matrix combinedMatrix = base.GetCombinedMatrix(transform);
			if (!combinedMatrix.IsIdentity)
			{
				for (int i = 0; i < pointList.Length; i++)
				{
					pointList[i] *= combinedMatrix;
				}
			}
			return new PathFigureCollection
			{
				new PathFigure(pointList[0], new PathSegment[]
				{
					new BezierSegment(pointList[1], pointList[2], pointList[3], true, true),
					new BezierSegment(pointList[4], pointList[5], pointList[6], true, true),
					new BezierSegment(pointList[7], pointList[8], pointList[9], true, true),
					new BezierSegment(pointList[10], pointList[11], pointList[12], true, true)
				}, true)
			};
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00088114 File Offset: 0x00087514
		internal override PathGeometry GetAsPathGeometry()
		{
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext(FillRule.EvenOdd, base.Transform);
			PathGeometry.ParsePathGeometryData(this.GetPathGeometryData(), pathStreamGeometryContext);
			return pathStreamGeometryContext.GetPathGeometry();
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00088140 File Offset: 0x00087540
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			if (this.IsObviouslyEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			Geometry.PathGeometryData result = default(Geometry.PathGeometryData);
			result.FillRule = FillRule.EvenOdd;
			result.Matrix = CompositionResourceManager.TransformToMilMatrix3x2D(base.Transform);
			Point[] pointList = this.GetPointList();
			ByteStreamGeometryContext byteStreamGeometryContext = new ByteStreamGeometryContext();
			byteStreamGeometryContext.BeginFigure(pointList[0], true, true);
			for (int i = 0; i < 12; i += 3)
			{
				byteStreamGeometryContext.BezierTo(pointList[i + 1], pointList[i + 2], pointList[i + 3], true, true);
			}
			byteStreamGeometryContext.Close();
			result.SerializedData = byteStreamGeometryContext.GetData();
			return result;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000881E0 File Offset: 0x000875E0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe Point[] GetPointList()
		{
			Point[] array = new Point[this.GetPointCount()];
			Point[] array2;
			Point* points;
			if ((array2 = array) == null || array2.Length == 0)
			{
				points = null;
			}
			else
			{
				points = &array2[0];
			}
			EllipseGeometry.GetPointList(points, this.GetPointCount(), this.Center, this.RadiusX, this.RadiusY);
			array2 = null;
			return array;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00088234 File Offset: 0x00087634
		[SecurityCritical]
		private unsafe static void GetPointList(Point* points, uint pointsCount, Point center, double radiusX, double radiusY)
		{
			Invariant.Assert(pointsCount >= 13U);
			radiusX = Math.Abs(radiusX);
			radiusY = Math.Abs(radiusY);
			double num = radiusX * 0.55228474983079345;
			points->X = (points[1].X = (points[11].X = (points[12].X = center.X + radiusX)));
			points[2].X = (points[10].X = center.X + num);
			points[3].X = (points[9].X = center.X);
			points[4].X = (points[8].X = center.X - num);
			points[5].X = (points[6].X = (points[7].X = center.X - radiusX));
			num = radiusY * 0.55228474983079345;
			points[2].Y = (points[3].Y = (points[4].Y = center.Y + radiusY));
			points[1].Y = (points[5].Y = center.Y + num);
			points->Y = (points[6].Y = (points[12].Y = center.Y));
			points[7].Y = (points[11].Y = center.Y - num);
			points[8].Y = (points[9].Y = (points[10].Y = center.Y - radiusY));
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00088490 File Offset: 0x00087890
		private byte[] GetTypeList()
		{
			return EllipseGeometry.s_roundedPathTypes;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000884A4 File Offset: 0x000878A4
		private uint GetPointCount()
		{
			return 13U;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000884B4 File Offset: 0x000878B4
		private uint GetSegmentCount()
		{
			return 4U;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.EllipseGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060021A6 RID: 8614 RVA: 0x000884C4 File Offset: 0x000878C4
		public new EllipseGeometry Clone()
		{
			return (EllipseGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.EllipseGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060021A7 RID: 8615 RVA: 0x000884DC File Offset: 0x000878DC
		public new EllipseGeometry CloneCurrentValue()
		{
			return (EllipseGeometry)base.CloneCurrentValue();
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000884F4 File Offset: 0x000878F4
		private static void RadiusXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EllipseGeometry ellipseGeometry = (EllipseGeometry)d;
			ellipseGeometry.PropertyChanged(EllipseGeometry.RadiusXProperty);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00088514 File Offset: 0x00087914
		private static void RadiusYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EllipseGeometry ellipseGeometry = (EllipseGeometry)d;
			ellipseGeometry.PropertyChanged(EllipseGeometry.RadiusYProperty);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00088534 File Offset: 0x00087934
		private static void CenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EllipseGeometry ellipseGeometry = (EllipseGeometry)d;
			ellipseGeometry.PropertyChanged(EllipseGeometry.CenterProperty);
		}

		/// <summary>Obtém ou define o valor de raio x do <see cref="T:System.Windows.Media.EllipseGeometry" />.</summary>
		/// <returns>O valor de raio de x a <see cref="T:System.Windows.Media.EllipseGeometry" />.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00088554 File Offset: 0x00087954
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x00088574 File Offset: 0x00087974
		public double RadiusX
		{
			get
			{
				return (double)base.GetValue(EllipseGeometry.RadiusXProperty);
			}
			set
			{
				base.SetValueInternal(EllipseGeometry.RadiusXProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor de raio y do <see cref="T:System.Windows.Media.EllipseGeometry" />.</summary>
		/// <returns>O valor de raio y a <see cref="T:System.Windows.Media.EllipseGeometry" />.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00088594 File Offset: 0x00087994
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x000885B4 File Offset: 0x000879B4
		public double RadiusY
		{
			get
			{
				return (double)base.GetValue(EllipseGeometry.RadiusYProperty);
			}
			set
			{
				base.SetValueInternal(EllipseGeometry.RadiusYProperty, value);
			}
		}

		/// <summary>Obtém ou define o ponto central do <see cref="T:System.Windows.Media.EllipseGeometry" />.</summary>
		/// <returns>O ponto central do <see cref="T:System.Windows.Media.EllipseGeometry" />.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000885D4 File Offset: 0x000879D4
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x000885F4 File Offset: 0x000879F4
		public Point Center
		{
			get
			{
				return (Point)base.GetValue(EllipseGeometry.CenterProperty);
			}
			set
			{
				base.SetValueInternal(EllipseGeometry.CenterProperty, value);
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00088614 File Offset: 0x00087A14
		protected override Freezable CreateInstanceCore()
		{
			return new EllipseGeometry();
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00088628 File Offset: 0x00087A28
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(EllipseGeometry.RadiusXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(EllipseGeometry.RadiusYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(EllipseGeometry.CenterProperty, channel);
				DUCE.MILCMD_ELLIPSEGEOMETRY milcmd_ELLIPSEGEOMETRY;
				milcmd_ELLIPSEGEOMETRY.Type = MILCMD.MilCmdEllipseGeometry;
				milcmd_ELLIPSEGEOMETRY.Handle = this._duceResource.GetHandle(channel);
				milcmd_ELLIPSEGEOMETRY.hTransform = hTransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_ELLIPSEGEOMETRY.RadiusX = this.RadiusX;
				}
				milcmd_ELLIPSEGEOMETRY.hRadiusXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_ELLIPSEGEOMETRY.RadiusY = this.RadiusY;
				}
				milcmd_ELLIPSEGEOMETRY.hRadiusYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_ELLIPSEGEOMETRY.Center = this.Center;
				}
				milcmd_ELLIPSEGEOMETRY.hCenterAnimations = animationResourceHandle3;
				channel.SendCommand((byte*)(&milcmd_ELLIPSEGEOMETRY), sizeof(DUCE.MILCMD_ELLIPSEGEOMETRY));
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0008872C File Offset: 0x00087B2C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_ELLIPSEGEOMETRY))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00088778 File Offset: 0x00087B78
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000887AC File Offset: 0x00087BAC
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000887C8 File Offset: 0x00087BC8
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000887E0 File Offset: 0x00087BE0
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000887FC File Offset: 0x00087BFC
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0008880C File Offset: 0x00087C0C
		static EllipseGeometry()
		{
			Type typeFromHandle = typeof(EllipseGeometry);
			EllipseGeometry.RadiusXProperty = Animatable.RegisterProperty("RadiusX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(EllipseGeometry.RadiusXPropertyChanged), null, true, null);
			EllipseGeometry.RadiusYProperty = Animatable.RegisterProperty("RadiusY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(EllipseGeometry.RadiusYPropertyChanged), null, true, null);
			EllipseGeometry.CenterProperty = Animatable.RegisterProperty("Center", typeof(Point), typeFromHandle, default(Point), new PropertyChangedCallback(EllipseGeometry.CenterPropertyChanged), null, true, null);
		}

		// Token: 0x040010BE RID: 4286
		internal const double c_arcAsBezier = 0.55228474983079345;

		// Token: 0x040010BF RID: 4287
		private const uint c_segmentCount = 4U;

		// Token: 0x040010C0 RID: 4288
		private const uint c_pointCount = 13U;

		// Token: 0x040010C1 RID: 4289
		private const byte c_smoothBezier = 42;

		// Token: 0x040010C2 RID: 4290
		private static readonly byte[] s_roundedPathTypes = new byte[]
		{
			58,
			42,
			42,
			42
		};

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.EllipseGeometry.RadiusX" />.</summary>
		// Token: 0x040010C3 RID: 4291
		public static readonly DependencyProperty RadiusXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.EllipseGeometry.RadiusY" />.</summary>
		// Token: 0x040010C4 RID: 4292
		public static readonly DependencyProperty RadiusYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.EllipseGeometry.Center" />.</summary>
		// Token: 0x040010C5 RID: 4293
		public static readonly DependencyProperty CenterProperty;

		// Token: 0x040010C6 RID: 4294
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040010C7 RID: 4295
		internal const double c_RadiusX = 0.0;

		// Token: 0x040010C8 RID: 4296
		internal const double c_RadiusY = 0.0;

		// Token: 0x040010C9 RID: 4297
		internal static Point s_Center = default(Point);
	}
}
