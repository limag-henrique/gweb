using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Representa a geometria de uma linha.</summary>
	// Token: 0x020003BC RID: 956
	public sealed class LineGeometry : Geometry
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.LineGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060024E2 RID: 9442 RVA: 0x00093ED8 File Offset: 0x000932D8
		public new LineGeometry Clone()
		{
			return (LineGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.LineGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060024E3 RID: 9443 RVA: 0x00093EF0 File Offset: 0x000932F0
		public new LineGeometry CloneCurrentValue()
		{
			return (LineGeometry)base.CloneCurrentValue();
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00093F08 File Offset: 0x00093308
		private static void StartPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LineGeometry lineGeometry = (LineGeometry)d;
			lineGeometry.PropertyChanged(LineGeometry.StartPointProperty);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00093F28 File Offset: 0x00093328
		private static void EndPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LineGeometry lineGeometry = (LineGeometry)d;
			lineGeometry.PropertyChanged(LineGeometry.EndPointProperty);
		}

		/// <summary>Obtém ou define o ponto inicial da linha.</summary>
		/// <returns>O ponto inicial da linha. O padrão é (0,0).</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00093F48 File Offset: 0x00093348
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x00093F68 File Offset: 0x00093368
		public Point StartPoint
		{
			get
			{
				return (Point)base.GetValue(LineGeometry.StartPointProperty);
			}
			set
			{
				base.SetValueInternal(LineGeometry.StartPointProperty, value);
			}
		}

		/// <summary>Obtém ou define o ponto final da linha.</summary>
		/// <returns>O ponto de extremidade da linha. O padrão é (0,0).</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00093F88 File Offset: 0x00093388
		// (set) Token: 0x060024E9 RID: 9449 RVA: 0x00093FA8 File Offset: 0x000933A8
		public Point EndPoint
		{
			get
			{
				return (Point)base.GetValue(LineGeometry.EndPointProperty);
			}
			set
			{
				base.SetValueInternal(LineGeometry.EndPointProperty, value);
			}
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00093FC8 File Offset: 0x000933C8
		protected override Freezable CreateInstanceCore()
		{
			return new LineGeometry();
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00093FDC File Offset: 0x000933DC
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
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(LineGeometry.StartPointProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(LineGeometry.EndPointProperty, channel);
				DUCE.MILCMD_LINEGEOMETRY milcmd_LINEGEOMETRY;
				milcmd_LINEGEOMETRY.Type = MILCMD.MilCmdLineGeometry;
				milcmd_LINEGEOMETRY.Handle = this._duceResource.GetHandle(channel);
				milcmd_LINEGEOMETRY.hTransform = hTransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_LINEGEOMETRY.StartPoint = this.StartPoint;
				}
				milcmd_LINEGEOMETRY.hStartPointAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_LINEGEOMETRY.EndPoint = this.EndPoint;
				}
				milcmd_LINEGEOMETRY.hEndPointAnimations = animationResourceHandle2;
				channel.SendCommand((byte*)(&milcmd_LINEGEOMETRY), sizeof(DUCE.MILCMD_LINEGEOMETRY));
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000940B0 File Offset: 0x000934B0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_LINEGEOMETRY))
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

		// Token: 0x060024ED RID: 9453 RVA: 0x000940FC File Offset: 0x000934FC
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

		// Token: 0x060024EE RID: 9454 RVA: 0x00094130 File Offset: 0x00093530
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0009414C File Offset: 0x0009354C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00094164 File Offset: 0x00093564
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00094180 File Offset: 0x00093580
		static LineGeometry()
		{
			Type typeFromHandle = typeof(LineGeometry);
			LineGeometry.StartPointProperty = Animatable.RegisterProperty("StartPoint", typeof(Point), typeFromHandle, default(Point), new PropertyChangedCallback(LineGeometry.StartPointPropertyChanged), null, true, null);
			LineGeometry.EndPointProperty = Animatable.RegisterProperty("EndPoint", typeof(Point), typeFromHandle, default(Point), new PropertyChangedCallback(LineGeometry.EndPointPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LineGeometry" /> que não tem comprimento.</summary>
		// Token: 0x060024F2 RID: 9458 RVA: 0x0009422C File Offset: 0x0009362C
		public LineGeometry()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LineGeometry" /> que tem os pontos inicial e final especificados.</summary>
		/// <param name="startPoint">O ponto inicial da linha.</param>
		/// <param name="endPoint">O ponto de extremidade da linha.</param>
		// Token: 0x060024F3 RID: 9459 RVA: 0x00094240 File Offset: 0x00093640
		public LineGeometry(Point startPoint, Point endPoint)
		{
			this.StartPoint = startPoint;
			this.EndPoint = endPoint;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LineGeometry" />.</summary>
		/// <param name="startPoint">O ponto de início.</param>
		/// <param name="endPoint">O ponto de término.</param>
		/// <param name="transform">A transformação a ser aplicada à linha.</param>
		// Token: 0x060024F4 RID: 9460 RVA: 0x00094264 File Offset: 0x00093664
		public LineGeometry(Point startPoint, Point endPoint, Transform transform) : this(startPoint, endPoint)
		{
			base.Transform = transform;
		}

		/// <summary>Obtém a caixa delimitadora alinhada ao eixo deste <see cref="T:System.Windows.Media.LineGeometry" />.</summary>
		/// <returns>A caixa delimitadora alinhada por eixo desse <see cref="T:System.Windows.Media.LineGeometry" />.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00094280 File Offset: 0x00093680
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				Rect result = new Rect(this.StartPoint, this.EndPoint);
				Transform transform = base.Transform;
				if (transform != null && !transform.IsIdentity)
				{
					transform.TransformRect(ref result);
				}
				return result;
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000942C4 File Offset: 0x000936C4
		internal override Rect GetBoundsInternal(Pen pen, Matrix worldMatrix, double tolerance, ToleranceType type)
		{
			Matrix geometryMatrix;
			Transform.GetTransformValue(base.Transform, out geometryMatrix);
			return LineGeometry.GetBoundsHelper(pen, worldMatrix, this.StartPoint, this.EndPoint, geometryMatrix, tolerance, type);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000942F8 File Offset: 0x000936F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe static Rect GetBoundsHelper(Pen pen, Matrix worldMatrix, Point pt1, Point pt2, Matrix geometryMatrix, double tolerance, ToleranceType type)
		{
			if (pen == null && worldMatrix.IsIdentity && geometryMatrix.IsIdentity)
			{
				return new Rect(pt1, pt2);
			}
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(Point))];
			*ptr = pt1;
			ptr[1] = pt2;
			byte[] array;
			byte* pTypes;
			if ((array = LineGeometry.s_lineTypes) == null || array.Length == 0)
			{
				pTypes = null;
			}
			else
			{
				pTypes = &array[0];
			}
			return Geometry.GetBoundsHelper(pen, &worldMatrix, ptr, pTypes, 2U, 1U, &geometryMatrix, tolerance, type, false);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00094374 File Offset: 0x00093774
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(Point))];
			*ptr = this.StartPoint;
			ptr[1] = this.EndPoint;
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

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.LineGeometry" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.LineGeometry" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060024F9 RID: 9465 RVA: 0x000943E4 File Offset: 0x000937E4
		public override bool IsEmpty()
		{
			return false;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.LineGeometry" /> pode ter em segmentos de curva.</summary>
		/// <returns>
		///   <see langword="true" /> se esse objeto <see cref="T:System.Windows.Media.LineGeometry" /> pode ter segmentos de curva, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060024FA RID: 9466 RVA: 0x000943F4 File Offset: 0x000937F4
		public override bool MayHaveCurves()
		{
			return false;
		}

		/// <summary>Obtém a área da região preenchida deste objeto <see cref="T:System.Windows.Media.LineGeometry" />.</summary>
		/// <param name="tolerance">A tolerância computacional a erros.</param>
		/// <param name="type">O tipo especificado para interpretar a tolerância de erro.</param>
		/// <returns>A área da região preenchida deste objeto <see cref="T:System.Windows.Media.LineGeometry" />, que é sempre 0 porque uma linha não contém nenhuma área.</returns>
		// Token: 0x060024FB RID: 9467 RVA: 0x00094404 File Offset: 0x00093804
		public override double GetArea(double tolerance, ToleranceType type)
		{
			return 0.0;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0009441C File Offset: 0x0009381C
		private byte[] GetTypeList()
		{
			return LineGeometry.s_lineTypes;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00094430 File Offset: 0x00093830
		private uint GetPointCount()
		{
			return 2U;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00094440 File Offset: 0x00093840
		private uint GetSegmentCount()
		{
			return 1U;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00094450 File Offset: 0x00093850
		internal override PathGeometry GetAsPathGeometry()
		{
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext(FillRule.EvenOdd, base.Transform);
			PathGeometry.ParsePathGeometryData(this.GetPathGeometryData(), pathStreamGeometryContext);
			return pathStreamGeometryContext.GetPathGeometry();
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0009447C File Offset: 0x0009387C
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			Point point = this.StartPoint;
			Point point2 = this.EndPoint;
			Transform transform2 = base.Transform;
			if (transform2 != null && !transform2.IsIdentity)
			{
				Matrix value = transform2.Value;
				point *= value;
				point2 *= value;
			}
			if (transform != null && !transform.IsIdentity)
			{
				Matrix value2 = transform.Value;
				point *= value2;
				point2 *= value2;
			}
			return new PathFigureCollection
			{
				new PathFigure(point, new PathSegment[]
				{
					new LineSegment(point2, true)
				}, false)
			};
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0009450C File Offset: 0x0009390C
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			if (this.IsObviouslyEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			Geometry.PathGeometryData result = default(Geometry.PathGeometryData);
			result.FillRule = FillRule.EvenOdd;
			result.Matrix = CompositionResourceManager.TransformToMilMatrix3x2D(base.Transform);
			ByteStreamGeometryContext byteStreamGeometryContext = new ByteStreamGeometryContext();
			byteStreamGeometryContext.BeginFigure(this.StartPoint, true, false);
			byteStreamGeometryContext.LineTo(this.EndPoint, true, false);
			byteStreamGeometryContext.Close();
			result.SerializedData = byteStreamGeometryContext.GetData();
			return result;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.LineGeometry.StartPoint" />.</summary>
		// Token: 0x0400117D RID: 4477
		public static readonly DependencyProperty StartPointProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.LineGeometry.EndPoint" />.</summary>
		// Token: 0x0400117E RID: 4478
		public static readonly DependencyProperty EndPointProperty;

		// Token: 0x0400117F RID: 4479
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001180 RID: 4480
		internal static Point s_StartPoint = default(Point);

		// Token: 0x04001181 RID: 4481
		internal static Point s_EndPoint = default(Point);

		// Token: 0x04001182 RID: 4482
		private static byte[] s_lineTypes = new byte[]
		{
			1
		};

		// Token: 0x04001183 RID: 4483
		private const uint c_segmentCount = 1U;

		// Token: 0x04001184 RID: 4484
		private const uint c_pointCount = 2U;
	}
}
