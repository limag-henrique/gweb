using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma forma complexa que pode ser composta por arcos, curvas, elipses, linhas e retângulos.</summary>
	// Token: 0x020003C3 RID: 963
	[ContentProperty("Figures")]
	public sealed class PathGeometry : Geometry
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PathGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002591 RID: 9617 RVA: 0x00095FFC File Offset: 0x000953FC
		public new PathGeometry Clone()
		{
			return (PathGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PathGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002592 RID: 9618 RVA: 0x00096014 File Offset: 0x00095414
		public new PathGeometry CloneCurrentValue()
		{
			return (PathGeometry)base.CloneCurrentValue();
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0009602C File Offset: 0x0009542C
		private static void FillRulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PathGeometry pathGeometry = (PathGeometry)d;
			pathGeometry.PropertyChanged(PathGeometry.FillRuleProperty);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0009604C File Offset: 0x0009544C
		private static void FiguresPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PathGeometry pathGeometry = (PathGeometry)d;
			pathGeometry.FiguresPropertyChangedHook(e);
			pathGeometry.PropertyChanged(PathGeometry.FiguresProperty);
		}

		/// <summary>Obtém ou define um valor que determina como as áreas de interseção contidas neste <see cref="T:System.Windows.Media.PathGeometry" /> são combinadas.</summary>
		/// <returns>Indica como as áreas de interseção desse <see cref="T:System.Windows.Media.PathGeometry" /> são combinadas.  O valor padrão é EvenOdd.</returns>
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00096074 File Offset: 0x00095474
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x00096094 File Offset: 0x00095494
		public FillRule FillRule
		{
			get
			{
				return (FillRule)base.GetValue(PathGeometry.FillRuleProperty);
			}
			set
			{
				base.SetValueInternal(PathGeometry.FillRuleProperty, FillRuleBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.PathFigure" /> que descrevem o conteúdo do caminho.</summary>
		/// <returns>Uma coleção de objetos <see cref="T:System.Windows.Media.PathFigure" /> que descrevem o conteúdo do caminho. Cada <see cref="T:System.Windows.Media.PathFigure" /> individual descreve uma forma.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000960B4 File Offset: 0x000954B4
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x000960D4 File Offset: 0x000954D4
		public PathFigureCollection Figures
		{
			get
			{
				return (PathFigureCollection)base.GetValue(PathGeometry.FiguresProperty);
			}
			set
			{
				base.SetValueInternal(PathGeometry.FiguresProperty, value);
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000960F0 File Offset: 0x000954F0
		protected override Freezable CreateInstanceCore()
		{
			return new PathGeometry();
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00096104 File Offset: 0x00095504
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			this.ManualUpdateResource(channel, skipOnChannelCheck);
			base.UpdateResource(channel, skipOnChannelCheck);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00096124 File Offset: 0x00095524
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_PATHGEOMETRY))
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

		// Token: 0x0600259C RID: 9628 RVA: 0x00096170 File Offset: 0x00095570
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

		// Token: 0x0600259D RID: 9629 RVA: 0x000961A4 File Offset: 0x000955A4
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000961C0 File Offset: 0x000955C0
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000961D8 File Offset: 0x000955D8
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000961F4 File Offset: 0x000955F4
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00096204 File Offset: 0x00095604
		static PathGeometry()
		{
			Type typeFromHandle = typeof(PathGeometry);
			PathGeometry.FillRuleProperty = Animatable.RegisterProperty("FillRule", typeof(FillRule), typeFromHandle, FillRule.EvenOdd, new PropertyChangedCallback(PathGeometry.FillRulePropertyChanged), new ValidateValueCallback(ValidateEnums.IsFillRuleValid), false, null);
			PathGeometry.FiguresProperty = Animatable.RegisterProperty("Figures", typeof(PathFigureCollection), typeFromHandle, new FreezableDefaultValueFactory(PathFigureCollection.Empty), new PropertyChangedCallback(PathGeometry.FiguresPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathGeometry" />.</summary>
		// Token: 0x060025A2 RID: 9634 RVA: 0x00096294 File Offset: 0x00095694
		public PathGeometry()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathGeometry" /> com o <see cref="P:System.Windows.Media.PathGeometry.Figures" /> especificado.</summary>
		/// <param name="figures">O <see cref="P:System.Windows.Media.PathGeometry.Figures" /> do <see cref="T:System.Windows.Media.PathGeometry" /> que descreve o conteúdo do <see cref="T:System.Windows.Shapes.Path" />.</param>
		// Token: 0x060025A3 RID: 9635 RVA: 0x000962A8 File Offset: 0x000956A8
		public PathGeometry(IEnumerable<PathFigure> figures)
		{
			if (figures != null)
			{
				using (IEnumerator<PathFigure> enumerator = figures.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PathFigure value = enumerator.Current;
						this.Figures.Add(value);
					}
					goto IL_44;
				}
				goto IL_39;
				IL_44:
				this.SetDirty();
				return;
			}
			IL_39:
			throw new ArgumentNullException("figures");
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathGeometry" /> com o <see cref="P:System.Windows.Media.PathGeometry.Figures" />, <see cref="P:System.Windows.Media.PathGeometry.FillRule" /> e <see cref="P:System.Windows.Media.Geometry.Transform" /> especificados.</summary>
		/// <param name="figures">O <see cref="P:System.Windows.Media.PathGeometry.Figures" /> do <see cref="T:System.Windows.Media.PathGeometry" /> que descreve o conteúdo do <see cref="T:System.Windows.Shapes.Path" />.</param>
		/// <param name="fillRule">O <see cref="P:System.Windows.Media.PathGeometry.FillRule" /> do <see cref="T:System.Windows.Media.PathGeometry" />.</param>
		/// <param name="transform">O <see cref="P:System.Windows.Media.Geometry.Transform" /> que especifica a transformação aplicada.</param>
		// Token: 0x060025A4 RID: 9636 RVA: 0x0009631C File Offset: 0x0009571C
		public PathGeometry(IEnumerable<PathFigure> figures, FillRule fillRule, Transform transform)
		{
			base.Transform = transform;
			if (ValidateEnums.IsFillRuleValid(fillRule))
			{
				this.FillRule = fillRule;
				if (figures != null)
				{
					using (IEnumerator<PathFigure> enumerator = figures.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PathFigure value = enumerator.Current;
							this.Figures.Add(value);
						}
						goto IL_5F;
					}
					goto IL_54;
					IL_5F:
					this.SetDirty();
					return;
				}
				IL_54:
				throw new ArgumentNullException("figures");
			}
		}

		/// <summary>Cria uma versão <see cref="T:System.Windows.Media.PathGeometry" /> do <see cref="T:System.Windows.Media.Geometry" /> especificado.</summary>
		/// <param name="geometry">A geometria da qual criar um <see cref="T:System.Windows.Media.PathGeometry" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.PathGeometry" /> criado com base nos valores atuais do <see cref="T:System.Windows.Media.Geometry" /> especificado.</returns>
		// Token: 0x060025A5 RID: 9637 RVA: 0x000963AC File Offset: 0x000957AC
		public static PathGeometry CreateFromGeometry(Geometry geometry)
		{
			if (geometry == null)
			{
				return null;
			}
			return geometry.GetAsPathGeometry();
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000963C4 File Offset: 0x000957C4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe static void ParsePathGeometryData(Geometry.PathGeometryData pathData, CapacityStreamGeometryContext ctx)
		{
			if (pathData.IsEmpty())
			{
				return;
			}
			int num = 0;
			byte[] array;
			byte* ptr;
			if ((array = pathData.SerializedData) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Invariant.Assert(pathData.SerializedData.Length >= num + sizeof(MIL_PATHGEOMETRY));
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			num += sizeof(MIL_PATHGEOMETRY);
			if (ptr2->FigureCount > 0U)
			{
				ctx.SetFigureCount((int)ptr2->FigureCount);
				int num2 = 0;
				while ((long)num2 < (long)((ulong)ptr2->FigureCount))
				{
					MIL_PATHFIGURE* ptr3 = (MIL_PATHFIGURE*)(ptr + num);
					num += sizeof(MIL_PATHFIGURE);
					ctx.BeginFigure(ptr3->StartPoint, (ptr3->Flags & MilPathFigureFlags.IsFillable) > (MilPathFigureFlags)0, (ptr3->Flags & MilPathFigureFlags.IsClosed) > (MilPathFigureFlags)0);
					if (ptr3->Count > 0U)
					{
						ctx.SetSegmentCount((int)ptr3->Count);
						int num3 = 0;
						while ((long)num3 < (long)((ulong)ptr3->Count))
						{
							MIL_SEGMENT* ptr4 = (MIL_SEGMENT*)(ptr + num);
							switch (ptr4->Type)
							{
							case MIL_SEGMENT_TYPE.MilSegmentLine:
							{
								MIL_SEGMENT_LINE* ptr5 = (MIL_SEGMENT_LINE*)(ptr + num);
								ctx.LineTo(ptr5->Point, (ptr5->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr5->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
								num += sizeof(MIL_SEGMENT_LINE);
								break;
							}
							case MIL_SEGMENT_TYPE.MilSegmentBezier:
							{
								MIL_SEGMENT_BEZIER* ptr6 = (MIL_SEGMENT_BEZIER*)(ptr + num);
								ctx.BezierTo(ptr6->Point1, ptr6->Point2, ptr6->Point3, (ptr6->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr6->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
								num += sizeof(MIL_SEGMENT_BEZIER);
								break;
							}
							case MIL_SEGMENT_TYPE.MilSegmentQuadraticBezier:
							{
								MIL_SEGMENT_QUADRATICBEZIER* ptr7 = (MIL_SEGMENT_QUADRATICBEZIER*)(ptr + num);
								ctx.QuadraticBezierTo(ptr7->Point1, ptr7->Point2, (ptr7->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr7->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
								num += sizeof(MIL_SEGMENT_QUADRATICBEZIER);
								break;
							}
							case MIL_SEGMENT_TYPE.MilSegmentArc:
							{
								MIL_SEGMENT_ARC* ptr8 = (MIL_SEGMENT_ARC*)(ptr + num);
								ctx.ArcTo(ptr8->Point, ptr8->Size, ptr8->XRotation, ptr8->LargeArc > 0U, (ptr8->Sweep == 0U) ? SweepDirection.Counterclockwise : SweepDirection.Clockwise, (ptr8->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr8->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
								num += sizeof(MIL_SEGMENT_ARC);
								break;
							}
							case MIL_SEGMENT_TYPE.MilSegmentPolyLine:
							case MIL_SEGMENT_TYPE.MilSegmentPolyBezier:
							case MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier:
							{
								MIL_SEGMENT_POLY* ptr9 = (MIL_SEGMENT_POLY*)(ptr + num);
								if (ptr9->Count > 0U)
								{
									List<Point> list = new List<Point>((int)ptr9->Count);
									Point* ptr10 = (Point*)(ptr + num + sizeof(MIL_SEGMENT_POLY));
									for (uint num4 = 0U; num4 < ptr9->Count; num4 += 1U)
									{
										list.Add(*ptr10);
										ptr10++;
									}
									switch (ptr4->Type)
									{
									case MIL_SEGMENT_TYPE.MilSegmentPolyLine:
										ctx.PolyLineTo(list, (ptr9->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr9->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
										break;
									case MIL_SEGMENT_TYPE.MilSegmentPolyBezier:
										ctx.PolyBezierTo(list, (ptr9->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr9->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
										break;
									case MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier:
										ctx.PolyQuadraticBezierTo(list, (ptr9->Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0, (ptr9->Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0);
										break;
									}
								}
								num += sizeof(MIL_SEGMENT_POLY) + (int)(ptr9->Count * (uint)sizeof(Point));
								break;
							}
							}
							num3++;
						}
					}
					num2++;
				}
			}
			array = null;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00096700 File Offset: 0x00095B00
		protected override void OnChanged()
		{
			this.SetDirty();
			base.OnChanged();
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0009671C File Offset: 0x00095B1C
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			Matrix combinedMatrix = base.GetCombinedMatrix(transform);
			PathFigureCollection pathFigureCollection;
			if (combinedMatrix.IsIdentity)
			{
				pathFigureCollection = this.Figures;
				if (pathFigureCollection == null)
				{
					pathFigureCollection = new PathFigureCollection();
				}
			}
			else
			{
				pathFigureCollection = new PathFigureCollection();
				PathFigureCollection figures = this.Figures;
				int num = (figures != null) ? figures.Count : 0;
				for (int i = 0; i < num; i++)
				{
					PathFigure pathFigure = figures.Internal_GetItem(i);
					pathFigureCollection.Add(pathFigure.GetTransformedCopy(combinedMatrix));
				}
			}
			return pathFigureCollection;
		}

		/// <summary>Converte o <see cref="T:System.Windows.Media.Geometry" /> especificado em uma coleção de objetos <see cref="T:System.Windows.Media.PathFigure" /> e a adiciona ao caminho.   Observação: Se o <see cref="T:System.Windows.Media.Geometry" /> especificado for animado, a conversão de <see cref="T:System.Windows.Media.Geometry" /> para <see cref="T:System.Windows.Media.PathFigure" /> poderá resultar em perda de informações.</summary>
		/// <param name="geometry">A geometria a ser adicionada ao caminho.</param>
		// Token: 0x060025A9 RID: 9641 RVA: 0x0009678C File Offset: 0x00095B8C
		public void AddGeometry(Geometry geometry)
		{
			if (geometry == null)
			{
				throw new ArgumentNullException("geometry");
			}
			if (geometry.IsEmpty())
			{
				return;
			}
			PathFigureCollection pathFigureCollection = geometry.GetPathFigureCollection();
			PathFigureCollection pathFigureCollection2 = this.Figures;
			if (pathFigureCollection2 == null)
			{
				pathFigureCollection2 = (this.Figures = new PathFigureCollection());
			}
			for (int i = 0; i < pathFigureCollection.Count; i++)
			{
				pathFigureCollection2.Add(pathFigureCollection.Internal_GetItem(i));
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Point" /> e um vetor de tangente neste <see cref="T:System.Windows.Media.PathGeometry" /> na fração especificada de seu tamanho.</summary>
		/// <param name="progress">A fração do tamanho deste <see cref="T:System.Windows.Media.PathGeometry" />.</param>
		/// <param name="point">Quando esse método é retornado, contém o local neste <see cref="T:System.Windows.Media.PathGeometry" /> na fração especificada de seu tamanho. Este parâmetro é passado não inicializado.</param>
		/// <param name="tangent">Quando esse método é retornado, contém o vetor de tangente. Este parâmetro é passado não inicializado.</param>
		// Token: 0x060025AA RID: 9642 RVA: 0x000967F0 File Offset: 0x00095BF0
		[SecurityCritical]
		public unsafe void GetPointAtFractionLength(double progress, out Point point, out Point tangent)
		{
			if (this.IsEmpty())
			{
				point = default(Point);
				tangent = default(Point);
				return;
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			byte[] array;
			byte* pPathData;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array[0];
			}
			HRESULT.Check(MilCoreApi.MilUtility_GetPointAtLengthFraction(&pathGeometryData.Matrix, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, progress, out point, out tangent));
			array = null;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00096860 File Offset: 0x00095C60
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe static PathGeometry InternalCombine(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode, Transform transform, double tolerance, ToleranceType type)
		{
			MilMatrix3x2D milMatrix3x2D = CompositionResourceManager.TransformToMilMatrix3x2D(transform);
			Geometry.PathGeometryData pathGeometryData = geometry1.GetPathGeometryData();
			Geometry.PathGeometryData pathGeometryData2 = geometry2.GetPathGeometryData();
			byte[] array;
			byte* pPathData;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array[0];
			}
			byte[] array2;
			byte* pPathData2;
			if ((array2 = pathGeometryData2.SerializedData) == null || array2.Length == 0)
			{
				pPathData2 = null;
			}
			else
			{
				pPathData2 = &array2[0];
			}
			FillRule fillRule = FillRule.Nonzero;
			PathGeometry.FigureList figureList = new PathGeometry.FigureList();
			int num = UnsafeNativeMethods.MilCoreApi.MilUtility_PathGeometryCombine(&milMatrix3x2D, &pathGeometryData.Matrix, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, &pathGeometryData2.Matrix, pathGeometryData2.FillRule, pPathData2, pathGeometryData2.Size, tolerance, type == ToleranceType.Relative, new PathGeometry.AddFigureToListDelegate(figureList.AddFigureToList), mode, out fillRule);
			PathGeometry result;
			if (num == -2003304438)
			{
				result = new PathGeometry();
			}
			else
			{
				HRESULT.Check(num);
				result = new PathGeometry(figureList.Figures, fillRule, null);
			}
			array2 = null;
			array = null;
			return result;
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.PathFigure" /> deste <see cref="T:System.Windows.Media.PathGeometry" />.</summary>
		// Token: 0x060025AC RID: 9644 RVA: 0x0009694C File Offset: 0x00095D4C
		public void Clear()
		{
			PathFigureCollection figures = this.Figures;
			if (figures != null)
			{
				figures.Clear();
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora deste objeto <see cref="T:System.Windows.Media.PathGeometry" />.   Observação: Este método não leva em consideração nenhuma caneta.</summary>
		/// <returns>A caixa delimitadora deste <see cref="T:System.Windows.Media.PathGeometry" />.</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x0009696C File Offset: 0x00095D6C
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				if (this.IsEmpty())
				{
					return Rect.Empty;
				}
				if ((this._flags & PathGeometryInternalFlags.BoundsValid) == PathGeometryInternalFlags.None)
				{
					this._bounds = PathGeometry.GetPathBoundsAsRB(this.GetPathGeometryData(), null, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute, false);
					this._flags |= PathGeometryInternalFlags.BoundsValid;
				}
				return this._bounds.AsRect;
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000969D0 File Offset: 0x00095DD0
		internal static Rect GetPathBounds(Geometry.PathGeometryData pathData, Pen pen, Matrix worldMatrix, double tolerance, ToleranceType type, bool skipHollows)
		{
			if (pathData.IsEmpty())
			{
				return Rect.Empty;
			}
			return PathGeometry.GetPathBoundsAsRB(pathData, pen, worldMatrix, tolerance, type, skipHollows).AsRect;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00096A04 File Offset: 0x00095E04
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe static MilRectD GetPathBoundsAsRB(Geometry.PathGeometryData pathData, Pen pen, Matrix worldMatrix, double tolerance, ToleranceType type, bool skipHollows)
		{
			double[] array = null;
			MIL_PEN_DATA mil_PEN_DATA;
			if (pen != null)
			{
				pen.GetBasicPenData(&mil_PEN_DATA, out array);
			}
			MilMatrix3x2D milMatrix3x2D = CompositionResourceManager.MatrixToMilMatrix3x2D(ref worldMatrix);
			byte[] serializedData;
			byte* pPathData;
			if ((serializedData = pathData.SerializedData) == null || serializedData.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &serializedData[0];
			}
			double[] array2;
			double* pDashArray;
			if ((array2 = array) == null || array2.Length == 0)
			{
				pDashArray = null;
			}
			else
			{
				pDashArray = &array2[0];
			}
			MilRectD naN;
			int num = UnsafeNativeMethods.MilCoreApi.MilUtility_PathGeometryBounds((pen == null) ? null : (&mil_PEN_DATA), pDashArray, &milMatrix3x2D, pathData.FillRule, pPathData, pathData.Size, &pathData.Matrix, tolerance, type == ToleranceType.Relative, skipHollows, &naN);
			if (num == -2003304438)
			{
				naN = MilRectD.NaN;
			}
			else
			{
				HRESULT.Check(num);
			}
			array2 = null;
			return naN;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00096AB8 File Offset: 0x00095EB8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe static IntersectionDetail HitTestWithPathGeometry(Geometry geometry1, Geometry geometry2, double tolerance, ToleranceType type)
		{
			IntersectionDetail result = IntersectionDetail.NotCalculated;
			Geometry.PathGeometryData pathGeometryData = geometry1.GetPathGeometryData();
			Geometry.PathGeometryData pathGeometryData2 = geometry2.GetPathGeometryData();
			byte[] array;
			byte* pPathData;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array[0];
			}
			byte[] array2;
			byte* pPathData2;
			if ((array2 = pathGeometryData2.SerializedData) == null || array2.Length == 0)
			{
				pPathData2 = null;
			}
			else
			{
				pPathData2 = &array2[0];
			}
			int num = MilCoreApi.MilUtility_PathGeometryHitTestPathGeometry(&pathGeometryData.Matrix, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, &pathGeometryData2.Matrix, pathGeometryData2.FillRule, pPathData2, pathGeometryData2.Size, tolerance, type == ToleranceType.Relative, &result);
			if (num == -2003304438)
			{
				result = IntersectionDetail.Empty;
			}
			else
			{
				HRESULT.Check(num);
			}
			array2 = null;
			array = null;
			return result;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.PathGeometry" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.PathGeometry" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060025B1 RID: 9649 RVA: 0x00096B6C File Offset: 0x00095F6C
		public override bool IsEmpty()
		{
			PathFigureCollection figures = this.Figures;
			return figures == null || figures.Count <= 0;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.PathGeometry" /> pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> caso este objeto <see cref="T:System.Windows.Media.PathGeometry" /> possa ter segmentos de curva; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060025B2 RID: 9650 RVA: 0x00096B94 File Offset: 0x00095F94
		public override bool MayHaveCurves()
		{
			PathFigureCollection figures = this.Figures;
			int num = (figures != null) ? figures.Count : 0;
			for (int i = 0; i < num; i++)
			{
				if (figures.Internal_GetItem(i).MayHaveCurves())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00096BD4 File Offset: 0x00095FD4
		internal override PathGeometry GetAsPathGeometry()
		{
			return this.CloneCurrentValue();
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00096BE8 File Offset: 0x00095FE8
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			PathFigureCollection figures = this.Figures;
			FillRule fillRule = this.FillRule;
			string text = string.Empty;
			if (figures != null)
			{
				text = figures.ConvertToString(format, provider);
			}
			if (fillRule != FillRule.EvenOdd)
			{
				return "F1" + text;
			}
			return text;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00096C28 File Offset: 0x00096028
		internal void SetDirty()
		{
			this._flags = PathGeometryInternalFlags.Dirty;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00096C3C File Offset: 0x0009603C
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			Geometry.PathGeometryData result = default(Geometry.PathGeometryData);
			result.FillRule = this.FillRule;
			result.Matrix = CompositionResourceManager.TransformToMilMatrix3x2D(base.Transform);
			if (this.IsObviouslyEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			ByteStreamGeometryContext byteStreamGeometryContext = new ByteStreamGeometryContext();
			PathFigureCollection figures = this.Figures;
			int num = (figures == null) ? 0 : figures.Count;
			for (int i = 0; i < num; i++)
			{
				figures.Internal_GetItem(i).SerializeData(byteStreamGeometryContext);
			}
			byteStreamGeometryContext.Close();
			result.SerializedData = byteStreamGeometryContext.GetData();
			return result;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00096CC8 File Offset: 0x000960C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void ManualUpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
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
				DUCE.MILCMD_PATHGEOMETRY milcmd_PATHGEOMETRY;
				milcmd_PATHGEOMETRY.Type = MILCMD.MilCmdPathGeometry;
				milcmd_PATHGEOMETRY.Handle = this._duceResource.GetHandle(channel);
				milcmd_PATHGEOMETRY.hTransform = hTransform;
				milcmd_PATHGEOMETRY.FillRule = this.FillRule;
				Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
				milcmd_PATHGEOMETRY.FiguresSize = pathGeometryData.Size;
				channel.BeginCommand((byte*)(&milcmd_PATHGEOMETRY), sizeof(DUCE.MILCMD_PATHGEOMETRY), checked((int)milcmd_PATHGEOMETRY.FiguresSize));
				byte[] array;
				byte* pbCommandData;
				if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
				{
					pbCommandData = null;
				}
				else
				{
					pbCommandData = &array[0];
				}
				channel.AppendCommandData(pbCommandData, checked((int)milcmd_PATHGEOMETRY.FiguresSize));
				array = null;
				channel.EndCommand();
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00096DA0 File Offset: 0x000961A0
		internal override void TransformPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if ((this._flags & PathGeometryInternalFlags.BoundsValid) != PathGeometryInternalFlags.None)
			{
				this.SetDirty();
			}
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00096DC0 File Offset: 0x000961C0
		internal void FiguresPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			this.SetDirty();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathGeometry.FillRule" />.</summary>
		// Token: 0x0400119A RID: 4506
		public static readonly DependencyProperty FillRuleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathGeometry.Figures" />.</summary>
		// Token: 0x0400119B RID: 4507
		public static readonly DependencyProperty FiguresProperty;

		// Token: 0x0400119C RID: 4508
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400119D RID: 4509
		internal const FillRule c_FillRule = FillRule.EvenOdd;

		// Token: 0x0400119E RID: 4510
		internal static PathFigureCollection s_Figures = PathFigureCollection.Empty;

		// Token: 0x0400119F RID: 4511
		internal PathGeometryInternalFlags _flags;

		// Token: 0x040011A0 RID: 4512
		internal MilRectD _bounds;

		// Token: 0x02000878 RID: 2168
		internal class FigureList
		{
			// Token: 0x0600579B RID: 22427 RVA: 0x00166114 File Offset: 0x00165514
			internal FigureList()
			{
				this._figures = new PathFigureCollection();
			}

			// Token: 0x17001216 RID: 4630
			// (get) Token: 0x0600579C RID: 22428 RVA: 0x00166134 File Offset: 0x00165534
			internal PathFigureCollection Figures
			{
				get
				{
					return this._figures;
				}
			}

			// Token: 0x0600579D RID: 22429 RVA: 0x00166148 File Offset: 0x00165548
			[SecurityCritical]
			internal unsafe void AddFigureToList(bool isFilled, bool isClosed, MilPoint2F* pPoints, uint pointCount, byte* pSegTypes, uint segmentCount)
			{
				if (pointCount >= 1U && segmentCount >= 1U)
				{
					PathFigure pathFigure = new PathFigure();
					pathFigure.IsFilled = isFilled;
					pathFigure.StartPoint = new Point((double)pPoints->X, (double)pPoints->Y);
					int num = 1;
					int num2 = 0;
					while ((long)num2 < (long)((ulong)segmentCount))
					{
						byte b = pSegTypes[num2] & 3;
						int num3 = 1;
						while ((long)(num2 + num3) < (long)((ulong)segmentCount) && pSegTypes[num2] == pSegTypes[num2 + num3])
						{
							num3++;
						}
						bool isStroked = (pSegTypes[num2] & 4) == 0;
						bool isSmoothJoin = (pSegTypes[num2] & 8) > 0;
						if (b == 1)
						{
							if ((long)(num + num3) > (long)((ulong)pointCount))
							{
								throw new InvalidOperationException(SR.Get("PathGeometry_InternalReadBackError"));
							}
							if (num3 > 1)
							{
								PointCollection pointCollection = new PointCollection();
								for (int i = 0; i < num3; i++)
								{
									pointCollection.Add(new Point((double)pPoints[num + i].X, (double)pPoints[num + i].Y));
								}
								pointCollection.Freeze();
								PolyLineSegment polyLineSegment = new PolyLineSegment(pointCollection, isStroked, isSmoothJoin);
								polyLineSegment.Freeze();
								pathFigure.Segments.Add(polyLineSegment);
							}
							else
							{
								pathFigure.Segments.Add(new LineSegment(new Point((double)pPoints[num].X, (double)pPoints[num].Y), isStroked, isSmoothJoin));
							}
							num += num3;
						}
						else
						{
							if (b != 2)
							{
								throw new InvalidOperationException(SR.Get("PathGeometry_InternalReadBackError"));
							}
							int num4 = num3 * 3;
							if ((long)(num + num4) > (long)((ulong)pointCount))
							{
								throw new InvalidOperationException(SR.Get("PathGeometry_InternalReadBackError"));
							}
							if (num3 > 1)
							{
								PointCollection pointCollection2 = new PointCollection();
								for (int j = 0; j < num4; j++)
								{
									pointCollection2.Add(new Point((double)pPoints[num + j].X, (double)pPoints[num + j].Y));
								}
								pointCollection2.Freeze();
								PolyBezierSegment polyBezierSegment = new PolyBezierSegment(pointCollection2, isStroked, isSmoothJoin);
								polyBezierSegment.Freeze();
								pathFigure.Segments.Add(polyBezierSegment);
							}
							else
							{
								pathFigure.Segments.Add(new BezierSegment(new Point((double)pPoints[num].X, (double)pPoints[num].Y), new Point((double)pPoints[num + 1].X, (double)pPoints[num + 1].Y), new Point((double)pPoints[num + 2].X, (double)pPoints[num + 2].Y), isStroked, isSmoothJoin));
							}
							num += num4;
						}
						num2 += num3;
					}
					if (isClosed)
					{
						pathFigure.IsClosed = true;
					}
					pathFigure.Freeze();
					this.Figures.Add(pathFigure);
				}
			}

			// Token: 0x0400289D RID: 10397
			internal PathFigureCollection _figures;
		}

		// Token: 0x02000879 RID: 2169
		// (Invoke) Token: 0x0600579F RID: 22431
		[SecurityCritical]
		internal unsafe delegate void AddFigureToListDelegate(bool isFilled, bool isClosed, MilPoint2F* pPoints, uint pointCount, byte* pTypes, uint typeCount);
	}
}
