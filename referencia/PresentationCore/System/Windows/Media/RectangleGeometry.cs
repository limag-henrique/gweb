using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Descreve um retângulo bidimensional.</summary>
	// Token: 0x020003D0 RID: 976
	public sealed class RectangleGeometry : Geometry
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.RectangleGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060026AD RID: 9901 RVA: 0x0009A080 File Offset: 0x00099480
		public new RectangleGeometry Clone()
		{
			return (RectangleGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.RectangleGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060026AE RID: 9902 RVA: 0x0009A098 File Offset: 0x00099498
		public new RectangleGeometry CloneCurrentValue()
		{
			return (RectangleGeometry)base.CloneCurrentValue();
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0009A0B0 File Offset: 0x000994B0
		private static void RadiusXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RectangleGeometry rectangleGeometry = (RectangleGeometry)d;
			rectangleGeometry.PropertyChanged(RectangleGeometry.RadiusXProperty);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0009A0D0 File Offset: 0x000994D0
		private static void RadiusYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RectangleGeometry rectangleGeometry = (RectangleGeometry)d;
			rectangleGeometry.PropertyChanged(RectangleGeometry.RadiusYProperty);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0009A0F0 File Offset: 0x000994F0
		private static void RectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RectangleGeometry rectangleGeometry = (RectangleGeometry)d;
			rectangleGeometry.PropertyChanged(RectangleGeometry.RectProperty);
		}

		/// <summary>Obtém ou define o raio de x da elipse usada para arredondar os cantos do retângulo.</summary>
		/// <returns>Um valor maior que ou igual a zero e menor ou igual à metade a largura do retângulo que descreve o raio x da elipse usar para arredondar os cantos do retângulo. Valores maiores do que meia a largura do retângulo são tratados como se igual à metade a largura do retângulo. Valores negativos são tratados como valores positivos. O padrão é 0,0.</returns>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0009A110 File Offset: 0x00099510
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x0009A130 File Offset: 0x00099530
		public double RadiusX
		{
			get
			{
				return (double)base.GetValue(RectangleGeometry.RadiusXProperty);
			}
			set
			{
				base.SetValueInternal(RectangleGeometry.RadiusXProperty, value);
			}
		}

		/// <summary>Obtém ou define o raio de y da elipse usada para arredondar os cantos do retângulo.</summary>
		/// <returns>Um valor maior que ou igual a zero e menor ou igual à metade a largura do retângulo que descreve o raio y da elipse usar para arredondar os cantos do retângulo. Valores maiores do que meia a largura do retângulo são tratados como se igual à metade a largura do retângulo. Valores negativos são tratados como valores positivos. O padrão é 0,0.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0009A150 File Offset: 0x00099550
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0009A170 File Offset: 0x00099570
		public double RadiusY
		{
			get
			{
				return (double)base.GetValue(RectangleGeometry.RadiusYProperty);
			}
			set
			{
				base.SetValueInternal(RectangleGeometry.RadiusYProperty, value);
			}
		}

		/// <summary>Obtém ou define as dimensões do retângulo.</summary>
		/// <returns>A posição e o tamanho do retângulo. O padrão é <see cref="P:System.Windows.Rect.Empty" />.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0009A190 File Offset: 0x00099590
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x0009A1B0 File Offset: 0x000995B0
		public Rect Rect
		{
			get
			{
				return (Rect)base.GetValue(RectangleGeometry.RectProperty);
			}
			set
			{
				base.SetValueInternal(RectangleGeometry.RectProperty, value);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0009A1D0 File Offset: 0x000995D0
		protected override Freezable CreateInstanceCore()
		{
			return new RectangleGeometry();
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0009A1E4 File Offset: 0x000995E4
		[SecurityCritical]
		[SecurityTreatAsSafe]
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
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(RectangleGeometry.RadiusXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(RectangleGeometry.RadiusYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(RectangleGeometry.RectProperty, channel);
				DUCE.MILCMD_RECTANGLEGEOMETRY milcmd_RECTANGLEGEOMETRY;
				milcmd_RECTANGLEGEOMETRY.Type = MILCMD.MilCmdRectangleGeometry;
				milcmd_RECTANGLEGEOMETRY.Handle = this._duceResource.GetHandle(channel);
				milcmd_RECTANGLEGEOMETRY.hTransform = hTransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_RECTANGLEGEOMETRY.RadiusX = this.RadiusX;
				}
				milcmd_RECTANGLEGEOMETRY.hRadiusXAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_RECTANGLEGEOMETRY.RadiusY = this.RadiusY;
				}
				milcmd_RECTANGLEGEOMETRY.hRadiusYAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_RECTANGLEGEOMETRY.Rect = this.Rect;
				}
				milcmd_RECTANGLEGEOMETRY.hRectAnimations = animationResourceHandle3;
				channel.SendCommand((byte*)(&milcmd_RECTANGLEGEOMETRY), sizeof(DUCE.MILCMD_RECTANGLEGEOMETRY));
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0009A2E8 File Offset: 0x000996E8
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_RECTANGLEGEOMETRY))
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

		// Token: 0x060026BB RID: 9915 RVA: 0x0009A334 File Offset: 0x00099734
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

		// Token: 0x060026BC RID: 9916 RVA: 0x0009A368 File Offset: 0x00099768
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0009A384 File Offset: 0x00099784
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0009A39C File Offset: 0x0009979C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0009A3B8 File Offset: 0x000997B8
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x0009A3C8 File Offset: 0x000997C8
		static RectangleGeometry()
		{
			Type typeFromHandle = typeof(RectangleGeometry);
			RectangleGeometry.RadiusXProperty = Animatable.RegisterProperty("RadiusX", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RectangleGeometry.RadiusXPropertyChanged), null, true, null);
			RectangleGeometry.RadiusYProperty = Animatable.RegisterProperty("RadiusY", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(RectangleGeometry.RadiusYPropertyChanged), null, true, null);
			RectangleGeometry.RectProperty = Animatable.RegisterProperty("Rect", typeof(Rect), typeFromHandle, Rect.Empty, new PropertyChangedCallback(RectangleGeometry.RectPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RectangleGeometry" /> e cria um retângulo com área zero.</summary>
		// Token: 0x060026C1 RID: 9921 RVA: 0x0009A504 File Offset: 0x00099904
		public RectangleGeometry()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RectangleGeometry" /> e especifica suas dimensões.</summary>
		/// <param name="rect">Uma estrutura <see cref="T:System.Windows.Rect" /> com as dimensões do retângulo.</param>
		// Token: 0x060026C2 RID: 9922 RVA: 0x0009A518 File Offset: 0x00099918
		public RectangleGeometry(Rect rect)
		{
			this.Rect = rect;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RectangleGeometry" />.</summary>
		/// <param name="rect">Uma estrutura <see cref="T:System.Windows.Rect" /> com as dimensões do retângulo.</param>
		/// <param name="radiusX">O raio do canto arredondado em que ele se conecta com as bordas superior e inferior do retângulo.</param>
		/// <param name="radiusY">O raio de canto arredondado, em que ele se conecta com as bordas esquerda e direita do retângulo.</param>
		// Token: 0x060026C3 RID: 9923 RVA: 0x0009A534 File Offset: 0x00099934
		public RectangleGeometry(Rect rect, double radiusX, double radiusY) : this(rect)
		{
			this.RadiusX = radiusX;
			this.RadiusY = radiusY;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RectangleGeometry" />.</summary>
		/// <param name="rect">Uma estrutura <see cref="T:System.Windows.Rect" /> com as dimensões do retângulo.</param>
		/// <param name="radiusX">O raio do canto arredondado em que ele se conecta com as bordas superior e inferior do retângulo.</param>
		/// <param name="radiusY">O raio de canto arredondado, em que ele se conecta com as bordas esquerda e direita do retângulo.</param>
		/// <param name="transform">A transformação a ser aplicada à geometria.</param>
		// Token: 0x060026C4 RID: 9924 RVA: 0x0009A558 File Offset: 0x00099958
		public RectangleGeometry(Rect rect, double radiusX, double radiusY, Transform transform) : this(rect, radiusX, radiusY)
		{
			base.Transform = transform;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora de um <see cref="T:System.Windows.Media.RectangleGeometry" />. Este método não leva em consideração nenhuma caneta.</summary>
		/// <returns>A caixa delimitadora do <see cref="T:System.Windows.Media.RectangleGeometry" />.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0009A578 File Offset: 0x00099978
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				Rect rect = this.Rect;
				Transform transform = base.Transform;
				Rect result;
				if (rect.IsEmpty)
				{
					result = Rect.Empty;
				}
				else if (transform == null || transform.IsIdentity)
				{
					result = rect;
				}
				else
				{
					double radiusX = this.RadiusX;
					double radiusY = this.RadiusY;
					if (radiusX == 0.0 && radiusY == 0.0)
					{
						result = rect;
						transform.TransformRect(ref result);
					}
					else
					{
						Matrix geometryMatrix;
						Transform.GetTransformValue(transform, out geometryMatrix);
						result = RectangleGeometry.GetBoundsHelper(null, Matrix.Identity, rect, radiusX, radiusY, geometryMatrix, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
					}
				}
				return result;
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0009A610 File Offset: 0x00099A10
		internal override bool AreClose(Geometry geometry)
		{
			RectangleGeometry rectangleGeometry = geometry as RectangleGeometry;
			if (rectangleGeometry != null)
			{
				Rect rect = this.Rect;
				Rect rect2 = rectangleGeometry.Rect;
				return DoubleUtil.AreClose(rect.X, rect2.X) && DoubleUtil.AreClose(rect.Y, rect2.Y) && DoubleUtil.AreClose(rect.Width, rect2.Width) && DoubleUtil.AreClose(rect.Height, rect2.Height) && DoubleUtil.AreClose(this.RadiusX, rectangleGeometry.RadiusX) && DoubleUtil.AreClose(this.RadiusY, rectangleGeometry.RadiusY) && this.Transform == rectangleGeometry.Transform && this.IsFrozen == rectangleGeometry.IsFrozen;
			}
			return base.AreClose(geometry);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0009A6E0 File Offset: 0x00099AE0
		internal override Rect GetBoundsInternal(Pen pen, Matrix worldMatrix, double tolerance, ToleranceType type)
		{
			Matrix geometryMatrix;
			Transform.GetTransformValue(base.Transform, out geometryMatrix);
			return RectangleGeometry.GetBoundsHelper(pen, worldMatrix, this.Rect, this.RadiusX, this.RadiusY, geometryMatrix, tolerance, type);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0009A718 File Offset: 0x00099B18
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe static Rect GetBoundsHelper(Pen pen, Matrix worldMatrix, Rect rect, double radiusX, double radiusY, Matrix geometryMatrix, double tolerance, ToleranceType type)
		{
			Rect result;
			if (rect.IsEmpty)
			{
				result = Rect.Empty;
			}
			else if ((pen == null || pen.DoesNotContainGaps) && geometryMatrix.IsIdentity && worldMatrix.IsIdentity)
			{
				result = rect;
				if (Pen.ContributesToBounds(pen))
				{
					double num = Math.Abs(pen.Thickness);
					result.X -= 0.5 * num;
					result.Y -= 0.5 * num;
					result.Width += num;
					result.Height += num;
				}
			}
			else
			{
				uint num2;
				uint segmentCount;
				RectangleGeometry.GetCounts(rect, radiusX, radiusY, out num2, out segmentCount);
				Invariant.Assert(num2 > 0U);
				Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)num2) * (UIntPtr)sizeof(Point))];
				RectangleGeometry.GetPointList(ptr, num2, rect, radiusX, radiusY);
				byte[] array;
				byte* pTypes;
				if ((array = RectangleGeometry.GetTypeList(rect, radiusX, radiusY)) == null || array.Length == 0)
				{
					pTypes = null;
				}
				else
				{
					pTypes = &array[0];
				}
				result = Geometry.GetBoundsHelper(pen, &worldMatrix, ptr, pTypes, num2, segmentCount, &geometryMatrix, tolerance, type, false);
				array = null;
			}
			return result;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0009A840 File Offset: 0x00099C40
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			if (this.IsEmpty())
			{
				return false;
			}
			double radiusX = this.RadiusX;
			double radiusY = this.RadiusY;
			Rect rect = this.Rect;
			uint pointCount = this.GetPointCount(rect, radiusX, radiusY);
			uint segmentCount = this.GetSegmentCount(rect, radiusX, radiusY);
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)pointCount) * (UIntPtr)sizeof(Point))];
			RectangleGeometry.GetPointList(ptr, pointCount, rect, radiusX, radiusY);
			byte[] typeList;
			byte* pTypes;
			if ((typeList = RectangleGeometry.GetTypeList(rect, radiusX, radiusY)) == null || typeList.Length == 0)
			{
				pTypes = null;
			}
			else
			{
				pTypes = &typeList[0];
			}
			return base.ContainsInternal(pen, hitPoint, tolerance, type, ptr, pointCount, pTypes, segmentCount);
		}

		/// <summary>Obtém a área da região preenchida deste objeto <see cref="T:System.Windows.Media.RectangleGeometry" />.</summary>
		/// <param name="tolerance">A tolerância computacional a erros.</param>
		/// <param name="type">Especifica como a tolerância a erros será interpretada.</param>
		/// <returns>A área da região preenchida deste objeto <see cref="T:System.Windows.Media.RectangleGeometry" />.</returns>
		// Token: 0x060026CA RID: 9930 RVA: 0x0009A8D0 File Offset: 0x00099CD0
		public override double GetArea(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (this.IsEmpty())
			{
				return 0.0;
			}
			double radiusX = this.RadiusX;
			double radiusY = this.RadiusY;
			Rect rect = this.Rect;
			double num = Math.Abs(rect.Width * rect.Height);
			num -= Math.Abs(radiusX * radiusY) * 0.85840734641020688;
			Transform transform = base.Transform;
			if (!transform.IsIdentity)
			{
				num *= Math.Abs(transform.Value.Determinant);
			}
			return num;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0009A95C File Offset: 0x00099D5C
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			if (this.IsEmpty())
			{
				return null;
			}
			Matrix combinedMatrix = base.GetCombinedMatrix(transform);
			double radiusX = this.RadiusX;
			double radiusY = this.RadiusY;
			Rect rect = this.Rect;
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				Point[] pointList = this.GetPointList(rect, radiusX, radiusY);
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
						new LineSegment(pointList[4], true, true),
						new BezierSegment(pointList[5], pointList[6], pointList[7], true, true),
						new LineSegment(pointList[8], true, true),
						new BezierSegment(pointList[9], pointList[10], pointList[11], true, true),
						new LineSegment(pointList[12], true, true),
						new BezierSegment(pointList[13], pointList[14], pointList[15], true, true)
					}, true)
				};
			}
			return new PathFigureCollection
			{
				new PathFigure(rect.TopLeft * combinedMatrix, new PathSegment[]
				{
					new PolyLineSegment(new Point[]
					{
						rect.TopRight * combinedMatrix,
						rect.BottomRight * combinedMatrix,
						rect.BottomLeft * combinedMatrix
					}, true)
				}, true)
			};
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0009AB28 File Offset: 0x00099F28
		internal static bool IsRounded(double radiusX, double radiusY)
		{
			return radiusX != 0.0 && radiusY != 0.0;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0009AB54 File Offset: 0x00099F54
		internal bool IsRounded()
		{
			return this.RadiusX != 0.0 && this.RadiusY != 0.0;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0009AB88 File Offset: 0x00099F88
		internal override PathGeometry GetAsPathGeometry()
		{
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext(FillRule.EvenOdd, base.Transform);
			PathGeometry.ParsePathGeometryData(this.GetPathGeometryData(), pathStreamGeometryContext);
			return pathStreamGeometryContext.GetPathGeometry();
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0009ABB4 File Offset: 0x00099FB4
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			if (this.IsObviouslyEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			Geometry.PathGeometryData result = default(Geometry.PathGeometryData);
			result.FillRule = FillRule.EvenOdd;
			result.Matrix = CompositionResourceManager.TransformToMilMatrix3x2D(base.Transform);
			double radiusX = this.RadiusX;
			double radiusY = this.RadiusY;
			Rect rect = this.Rect;
			ByteStreamGeometryContext byteStreamGeometryContext = new ByteStreamGeometryContext();
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				Point[] pointList = this.GetPointList(rect, radiusX, radiusY);
				byteStreamGeometryContext.BeginFigure(pointList[0], true, true);
				byteStreamGeometryContext.BezierTo(pointList[1], pointList[2], pointList[3], true, false);
				byteStreamGeometryContext.LineTo(pointList[4], true, false);
				byteStreamGeometryContext.BezierTo(pointList[5], pointList[6], pointList[7], true, false);
				byteStreamGeometryContext.LineTo(pointList[8], true, false);
				byteStreamGeometryContext.BezierTo(pointList[9], pointList[10], pointList[11], true, false);
				byteStreamGeometryContext.LineTo(pointList[12], true, false);
				byteStreamGeometryContext.BezierTo(pointList[13], pointList[14], pointList[15], true, false);
			}
			else
			{
				byteStreamGeometryContext.BeginFigure(rect.TopLeft, true, true);
				byteStreamGeometryContext.LineTo(this.Rect.TopRight, true, false);
				byteStreamGeometryContext.LineTo(this.Rect.BottomRight, true, false);
				byteStreamGeometryContext.LineTo(this.Rect.BottomLeft, true, false);
			}
			byteStreamGeometryContext.Close();
			result.SerializedData = byteStreamGeometryContext.GetData();
			return result;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0009AD48 File Offset: 0x0009A148
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe Point[] GetPointList(Rect rect, double radiusX, double radiusY)
		{
			uint pointCount = this.GetPointCount(rect, radiusX, radiusY);
			Point[] array = new Point[pointCount];
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
			RectangleGeometry.GetPointList(points, pointCount, rect, radiusX, radiusY);
			array2 = null;
			return array;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0009AD8C File Offset: 0x0009A18C
		[SecurityCritical]
		private unsafe static void GetPointList(Point* points, uint pointsCount, Rect rect, double radiusX, double radiusY)
		{
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				Invariant.Assert(pointsCount >= RectangleGeometry.c_roundedPointCount);
				radiusX = Math.Min(rect.Width * 0.5, Math.Abs(radiusX));
				radiusY = Math.Min(rect.Height * 0.5, Math.Abs(radiusY));
				double num = 0.44771525016920655 * radiusX;
				double num2 = 0.44771525016920655 * radiusY;
				points[1].X = (points->X = (points[15].X = (points[14].X = rect.X)));
				points[2].X = (points[13].X = rect.X + num);
				points[3].X = (points[12].X = rect.X + radiusX);
				points[4].X = (points[11].X = rect.Right - radiusX);
				points[5].X = (points[10].X = rect.Right - num);
				points[6].X = (points[7].X = (points[8].X = (points[9].X = rect.Right)));
				points[2].Y = (points[3].Y = (points[4].Y = (points[5].Y = rect.Y)));
				points[1].Y = (points[6].Y = rect.Y + num2);
				points->Y = (points[7].Y = rect.Y + radiusY);
				points[15].Y = (points[8].Y = rect.Bottom - radiusY);
				points[14].Y = (points[9].Y = rect.Bottom - num2);
				points[13].Y = (points[12].Y = (points[11].Y = (points[10].Y = rect.Bottom)));
				points[16] = *points;
				return;
			}
			Invariant.Assert(pointsCount >= 5U);
			points->X = (points[3].X = (points[4].X = rect.X));
			points[1].X = (points[2].X = rect.Right);
			points->Y = (points[1].Y = (points[4].Y = rect.Y));
			points[2].Y = (points[3].Y = rect.Bottom);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0009B180 File Offset: 0x0009A580
		private static byte[] GetTypeList(Rect rect, double radiusX, double radiusY)
		{
			if (rect.IsEmpty)
			{
				return null;
			}
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				return RectangleGeometry.s_roundedPathTypes;
			}
			return RectangleGeometry.s_squaredPathTypes;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0009B1AC File Offset: 0x0009A5AC
		private uint GetPointCount(Rect rect, double radiusX, double radiusY)
		{
			if (rect.IsEmpty)
			{
				return 0U;
			}
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				return RectangleGeometry.c_roundedPointCount;
			}
			return 5U;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0009B1D4 File Offset: 0x0009A5D4
		private uint GetSegmentCount(Rect rect, double radiusX, double radiusY)
		{
			if (rect.IsEmpty)
			{
				return 0U;
			}
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				return RectangleGeometry.c_roundedSegmentCount;
			}
			return 4U;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0009B1FC File Offset: 0x0009A5FC
		private static void GetCounts(Rect rect, double radiusX, double radiusY, out uint pointCount, out uint segmentCount)
		{
			if (rect.IsEmpty)
			{
				pointCount = 0U;
				segmentCount = 0U;
				return;
			}
			if (RectangleGeometry.IsRounded(radiusX, radiusY))
			{
				pointCount = RectangleGeometry.c_roundedPointCount;
				segmentCount = RectangleGeometry.c_roundedSegmentCount;
				return;
			}
			pointCount = 5U;
			segmentCount = 4U;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.RectangleGeometry" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.RectangleGeometry" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060026D6 RID: 9942 RVA: 0x0009B23C File Offset: 0x0009A63C
		public override bool IsEmpty()
		{
			return this.Rect.IsEmpty;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.RectangleGeometry" /> pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> caso este objeto <see cref="T:System.Windows.Media.RectangleGeometry" /> possa ter segmentos de curva; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060026D7 RID: 9943 RVA: 0x0009B258 File Offset: 0x0009A658
		public override bool MayHaveCurves()
		{
			return this.IsRounded();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RectangleGeometry.RadiusX" />.</summary>
		// Token: 0x040011D8 RID: 4568
		public static readonly DependencyProperty RadiusXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RectangleGeometry.RadiusY" />.</summary>
		// Token: 0x040011D9 RID: 4569
		public static readonly DependencyProperty RadiusYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RectangleGeometry.Rect" />.</summary>
		// Token: 0x040011DA RID: 4570
		public static readonly DependencyProperty RectProperty;

		// Token: 0x040011DB RID: 4571
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040011DC RID: 4572
		internal const double c_RadiusX = 0.0;

		// Token: 0x040011DD RID: 4573
		internal const double c_RadiusY = 0.0;

		// Token: 0x040011DE RID: 4574
		internal static Rect s_Rect = Rect.Empty;

		// Token: 0x040011DF RID: 4575
		private static uint c_roundedSegmentCount = 8U;

		// Token: 0x040011E0 RID: 4576
		private static uint c_roundedPointCount = 17U;

		// Token: 0x040011E1 RID: 4577
		private static byte smoothBezier = 42;

		// Token: 0x040011E2 RID: 4578
		private static byte smoothLine = 9;

		// Token: 0x040011E3 RID: 4579
		private static byte[] s_roundedPathTypes = new byte[]
		{
			58,
			RectangleGeometry.smoothLine,
			RectangleGeometry.smoothBezier,
			RectangleGeometry.smoothLine,
			RectangleGeometry.smoothBezier,
			RectangleGeometry.smoothLine,
			RectangleGeometry.smoothBezier,
			RectangleGeometry.smoothLine
		};

		// Token: 0x040011E4 RID: 4580
		private const uint c_squaredSegmentCount = 4U;

		// Token: 0x040011E5 RID: 4581
		private const uint c_squaredPointCount = 5U;

		// Token: 0x040011E6 RID: 4582
		private static readonly byte[] s_squaredPathTypes = new byte[]
		{
			17,
			1,
			1,
			1
		};
	}
}
