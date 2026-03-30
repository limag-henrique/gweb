using System;
using System.Collections.Generic;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Renderiza os filhos 2D dentro dos limites do visor 3D especificado.</summary>
	// Token: 0x02000485 RID: 1157
	[ContentProperty("Visual")]
	public sealed class Viewport2DVisual3D : Visual3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Viewport2DVisual3D" />.</summary>
		// Token: 0x06003246 RID: 12870 RVA: 0x000C882C File Offset: 0x000C7C2C
		public Viewport2DVisual3D()
		{
			this._visualBrush = this.CreateVisualBrush();
			this._bitmapCacheBrush = this.CreateBitmapCacheBrush();
			base.Visual3DModel = new GeometryModel3D
			{
				CanBeInheritanceContext = false
			};
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000C886C File Offset: 0x000C7C6C
		internal static bool Get3DPointFor2DCoordinate(Point point, out Point3D point3D, Point3DCollection positions, PointCollection textureCoords, Int32Collection triIndices)
		{
			point3D = default(Point3D);
			Point3D[] array = new Point3D[3];
			Point[] array2 = new Point[3];
			if (positions != null && textureCoords != null)
			{
				if (triIndices == null || triIndices.Count == 0)
				{
					int count = textureCoords.Count;
					int num = positions.Count;
					num -= num % 3;
					for (int i = 0; i < num; i += 3)
					{
						for (int j = 0; j < 3; j++)
						{
							array[j] = positions[i + j];
							if (i + j < count)
							{
								array2[j] = textureCoords[i + j];
							}
							else
							{
								array2[j] = new Point(0.0, 0.0);
							}
						}
						if (M3DUtil.IsPointInTriangle(point, array2, array, out point3D))
						{
							return true;
						}
					}
				}
				else
				{
					int count2 = positions.Count;
					int count3 = textureCoords.Count;
					int k = 2;
					int count4 = triIndices.Count;
					while (k < count4)
					{
						bool flag = true;
						for (int l = 0; l < 3; l++)
						{
							int num2 = triIndices[k - 2 + l];
							if (num2 < 0 || num2 >= count2)
							{
								return false;
							}
							if (num2 < 0 || num2 >= count3)
							{
								flag = false;
								break;
							}
							array[l] = positions[num2];
							array2[l] = textureCoords[num2];
						}
						if (flag && M3DUtil.IsPointInTriangle(point, array2, array, out point3D))
						{
							return true;
						}
						k += 3;
					}
				}
			}
			return false;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000C89D0 File Offset: 0x000C7DD0
		internal static Point TextureCoordsToVisualCoords(Point uv, Visual visual)
		{
			return Viewport2DVisual3D.TextureCoordsToVisualCoords(uv, visual.CalculateSubgraphRenderBoundsOuterSpace());
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000C89EC File Offset: 0x000C7DEC
		internal static Point TextureCoordsToVisualCoords(Point uv, Rect descBounds)
		{
			return new Point(uv.X * descBounds.Width + descBounds.Left, uv.Y * descBounds.Height + descBounds.Top);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000C8A2C File Offset: 0x000C7E2C
		internal static bool GetIntersectionInfo(RayHitTestResult rayHitResult, out Point outputPoint)
		{
			bool result = false;
			outputPoint = default(Point);
			RayMeshGeometry3DHitTestResult rayMeshGeometry3DHitTestResult = rayHitResult as RayMeshGeometry3DHitTestResult;
			if (rayMeshGeometry3DHitTestResult != null)
			{
				MeshGeometry3D meshHit = rayMeshGeometry3DHitTestResult.MeshHit;
				double vertexWeight = rayMeshGeometry3DHitTestResult.VertexWeight1;
				double vertexWeight2 = rayMeshGeometry3DHitTestResult.VertexWeight2;
				double vertexWeight3 = rayMeshGeometry3DHitTestResult.VertexWeight3;
				int vertexIndex = rayMeshGeometry3DHitTestResult.VertexIndex1;
				int vertexIndex2 = rayMeshGeometry3DHitTestResult.VertexIndex2;
				int vertexIndex3 = rayMeshGeometry3DHitTestResult.VertexIndex3;
				PointCollection textureCoordinates = meshHit.TextureCoordinates;
				if (textureCoordinates != null && vertexIndex < textureCoordinates.Count && vertexIndex2 < textureCoordinates.Count && vertexIndex3 < textureCoordinates.Count)
				{
					Point point = textureCoordinates[vertexIndex];
					Point point2 = textureCoordinates[vertexIndex2];
					Point point3 = textureCoordinates[vertexIndex3];
					outputPoint = new Point(point.X * vertexWeight + point2.X * vertexWeight2 + point3.X * vertexWeight3, point.Y * vertexWeight + point2.Y * vertexWeight2 + point3.Y * vertexWeight3);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000C8B20 File Offset: 0x000C7F20
		internal static Point VisualCoordsToTextureCoords(Point pt, Visual visual)
		{
			return Viewport2DVisual3D.VisualCoordsToTextureCoords(pt, visual.CalculateSubgraphRenderBoundsOuterSpace());
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000C8B3C File Offset: 0x000C7F3C
		internal static Point VisualCoordsToTextureCoords(Point pt, Rect descBounds)
		{
			return new Point((pt.X - descBounds.Left) / (descBounds.Right - descBounds.Left), (pt.Y - descBounds.Top) / (descBounds.Bottom - descBounds.Top));
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000C8B8C File Offset: 0x000C7F8C
		private void GenerateMaterial()
		{
			Material material = this.Material;
			if (material != null)
			{
				material = material.CloneCurrentValue();
			}
			((GeometryModel3D)base.Visual3DModel).Material = material;
			if (material != null)
			{
				this.SwapInCyclicBrush(material);
			}
		}

		/// <summary>Obtém ou define o visual 2D a ser colocado no objeto 3D.</summary>
		/// <returns>O visual a ser colocado no objeto 3D.</returns>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000C8BC8 File Offset: 0x000C7FC8
		// (set) Token: 0x0600324F RID: 12879 RVA: 0x000C8BE8 File Offset: 0x000C7FE8
		public Visual Visual
		{
			get
			{
				return (Visual)base.GetValue(Viewport2DVisual3D.VisualProperty);
			}
			set
			{
				base.SetValue(Viewport2DVisual3D.VisualProperty, value);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000C8C04 File Offset: 0x000C8004
		// (set) Token: 0x06003251 RID: 12881 RVA: 0x000C8C18 File Offset: 0x000C8018
		private VisualBrush InternalVisualBrush
		{
			get
			{
				return this._visualBrush;
			}
			set
			{
				this._visualBrush = value;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000C8C2C File Offset: 0x000C802C
		// (set) Token: 0x06003253 RID: 12883 RVA: 0x000C8C40 File Offset: 0x000C8040
		private BitmapCacheBrush InternalBitmapCacheBrush
		{
			get
			{
				return this._bitmapCacheBrush;
			}
			set
			{
				this._bitmapCacheBrush = value;
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000C8C54 File Offset: 0x000C8054
		internal static void OnVisualChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)sender;
			Visual visual = (Visual)e.OldValue;
			Visual visual2 = (Visual)e.NewValue;
			if (visual != visual2)
			{
				if (viewport2DVisual3D.CacheMode is BitmapCache)
				{
					viewport2DVisual3D.InternalBitmapCacheBrush.Target = visual2;
					return;
				}
				viewport2DVisual3D.RemoveVisualChild(visual);
				viewport2DVisual3D.AddVisualChild(visual2);
				viewport2DVisual3D.InternalVisualBrush.Visual = visual2;
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000C8CBC File Offset: 0x000C80BC
		private void AddVisualChild(Visual child)
		{
			if (child == null)
			{
				return;
			}
			if (child._parent != null)
			{
				throw new ArgumentException(SR.Get("Visual_HasParent"));
			}
			child._parent = this;
			this.OnVisualChildrenChanged(child, null);
			child.FireOnVisualParentChanged(null);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000C8CFC File Offset: 0x000C80FC
		private void RemoveVisualChild(Visual child)
		{
			if (child == null || child._parent == null)
			{
				return;
			}
			if (child._parent != this)
			{
				throw new ArgumentException(SR.Get("Visual_NotChild"));
			}
			child._parent = null;
			child.FireOnVisualParentChanged(this);
			this.OnVisualChildrenChanged(null, child);
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000C8D44 File Offset: 0x000C8144
		private VisualBrush CreateVisualBrush()
		{
			VisualBrush visualBrush = new VisualBrush();
			visualBrush.CanBeInheritanceContext = false;
			visualBrush.ViewportUnits = BrushMappingMode.Absolute;
			visualBrush.TileMode = TileMode.None;
			RenderOptions.SetCachingHint(visualBrush, (CachingHint)base.GetValue(Viewport2DVisual3D.CachingHintProperty));
			RenderOptions.SetCacheInvalidationThresholdMinimum(visualBrush, (double)base.GetValue(Viewport2DVisual3D.CacheInvalidationThresholdMinimumProperty));
			RenderOptions.SetCacheInvalidationThresholdMaximum(visualBrush, (double)base.GetValue(Viewport2DVisual3D.CacheInvalidationThresholdMaximumProperty));
			return visualBrush;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000C8DB0 File Offset: 0x000C81B0
		private BitmapCacheBrush CreateBitmapCacheBrush()
		{
			return new BitmapCacheBrush
			{
				CanBeInheritanceContext = false,
				AutoWrapTarget = true,
				BitmapCache = (this.CacheMode as BitmapCache)
			};
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000C8DE4 File Offset: 0x000C81E4
		private void SwapInCyclicBrush(Material material)
		{
			int num = 0;
			Stack<Material> stack = new Stack<Material>();
			stack.Push(material);
			Brush brush = (this.CacheMode is BitmapCache) ? this.InternalBitmapCacheBrush : this.InternalVisualBrush;
			while (stack.Count > 0)
			{
				Material material2 = stack.Pop();
				if (material2 is DiffuseMaterial)
				{
					DiffuseMaterial diffuseMaterial = (DiffuseMaterial)material2;
					if ((bool)diffuseMaterial.GetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty))
					{
						diffuseMaterial.Brush = brush;
						num++;
					}
				}
				else if (material2 is EmissiveMaterial)
				{
					EmissiveMaterial emissiveMaterial = (EmissiveMaterial)material2;
					if ((bool)emissiveMaterial.GetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty))
					{
						emissiveMaterial.Brush = brush;
						num++;
					}
				}
				else if (material2 is SpecularMaterial)
				{
					SpecularMaterial specularMaterial = (SpecularMaterial)material2;
					if ((bool)specularMaterial.GetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty))
					{
						specularMaterial.Brush = brush;
						num++;
					}
				}
				else if (material2 is MaterialGroup)
				{
					MaterialGroup materialGroup = (MaterialGroup)material2;
					if ((bool)materialGroup.GetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty))
					{
						throw new ArgumentException(SR.Get("Viewport2DVisual3D_MaterialGroupIsInteractiveMaterial"), "material");
					}
					MaterialCollection children = materialGroup.Children;
					if (children != null)
					{
						int i = 0;
						int count = children.Count;
						while (i < count)
						{
							Material item = children[i];
							stack.Push(item);
							i++;
						}
					}
				}
				else
				{
					Invariant.Assert(true, "Unexpected Material type encountered.  V2DV3D handles DiffuseMaterial, EmissiveMaterial, SpecularMaterial, and MaterialGroup.");
				}
			}
			if (num > 1)
			{
				throw new ArgumentException(SR.Get("Viewport2DVisual3D_MultipleInteractiveMaterials"), "material");
			}
		}

		/// <summary>Obtém ou define a geometria 3D deste <see cref="T:System.Windows.Media.Media3D.Viewport2DVisual3D" />.</summary>
		/// <returns>A geometria 3D para este <see cref="T:System.Windows.Media.Media3D.Viewport2DVisual3D" />.</returns>
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000C8F70 File Offset: 0x000C8370
		// (set) Token: 0x0600325B RID: 12891 RVA: 0x000C8F90 File Offset: 0x000C8390
		public Geometry3D Geometry
		{
			get
			{
				return (Geometry3D)base.GetValue(Viewport2DVisual3D.GeometryProperty);
			}
			set
			{
				base.SetValue(Viewport2DVisual3D.GeometryProperty, value);
			}
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000C8FAC File Offset: 0x000C83AC
		internal static void OnGeometryChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)sender;
			viewport2DVisual3D.InvalidateAllCachedValues();
			if (!e.IsASubPropertyChange)
			{
				((GeometryModel3D)viewport2DVisual3D.Visual3DModel).Geometry = viewport2DVisual3D.Geometry;
			}
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000C8FE8 File Offset: 0x000C83E8
		private void InvalidateAllCachedValues()
		{
			this.InternalPositionsCache = null;
			this.InternalTextureCoordinatesCache = null;
			this.InternalTriangleIndicesCache = null;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000C900C File Offset: 0x000C840C
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x000C9060 File Offset: 0x000C8460
		internal Point3DCollection InternalPositionsCache
		{
			get
			{
				if (this._positionsCache == null)
				{
					MeshGeometry3D meshGeometry3D = this.Geometry as MeshGeometry3D;
					if (meshGeometry3D != null)
					{
						this._positionsCache = meshGeometry3D.Positions;
						if (this._positionsCache != null)
						{
							this._positionsCache = (Point3DCollection)this._positionsCache.GetCurrentValueAsFrozen();
						}
					}
				}
				return this._positionsCache;
			}
			set
			{
				this._positionsCache = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000C9074 File Offset: 0x000C8474
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000C90C8 File Offset: 0x000C84C8
		internal PointCollection InternalTextureCoordinatesCache
		{
			get
			{
				if (this._textureCoordinatesCache == null)
				{
					MeshGeometry3D meshGeometry3D = this.Geometry as MeshGeometry3D;
					if (meshGeometry3D != null)
					{
						this._textureCoordinatesCache = meshGeometry3D.TextureCoordinates;
						if (this._textureCoordinatesCache != null)
						{
							this._textureCoordinatesCache = (PointCollection)this._textureCoordinatesCache.GetCurrentValueAsFrozen();
						}
					}
				}
				return this._textureCoordinatesCache;
			}
			set
			{
				this._textureCoordinatesCache = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000C90DC File Offset: 0x000C84DC
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000C9130 File Offset: 0x000C8530
		internal Int32Collection InternalTriangleIndicesCache
		{
			get
			{
				if (this._triangleIndicesCache == null)
				{
					MeshGeometry3D meshGeometry3D = this.Geometry as MeshGeometry3D;
					if (meshGeometry3D != null)
					{
						this._triangleIndicesCache = meshGeometry3D.TriangleIndices;
						if (this._triangleIndicesCache != null)
						{
							this._triangleIndicesCache = (Int32Collection)this._triangleIndicesCache.GetCurrentValueAsFrozen();
						}
					}
				}
				return this._triangleIndicesCache;
			}
			set
			{
				this._triangleIndicesCache = value;
			}
		}

		/// <summary>Obtém ou define o material que descreve a aparência do objeto 3D.</summary>
		/// <returns>O material do objeto 3D.</returns>
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000C9144 File Offset: 0x000C8544
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000C9164 File Offset: 0x000C8564
		public Material Material
		{
			get
			{
				return (Material)base.GetValue(Viewport2DVisual3D.MaterialProperty);
			}
			set
			{
				base.SetValue(Viewport2DVisual3D.MaterialProperty, value);
			}
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000C9180 File Offset: 0x000C8580
		internal static void OnMaterialPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)sender;
			viewport2DVisual3D.GenerateMaterial();
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.IsVisualHostMaterial" /> para um elemento especificado.</summary>
		/// <param name="element">O elemento no qual a propriedade anexada é gravada.</param>
		/// <param name="value">O valor necessário de <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.IsVisualHostMaterial" />.</param>
		// Token: 0x06003267 RID: 12903 RVA: 0x000C919C File Offset: 0x000C859C
		public static void SetIsVisualHostMaterial(Material element, bool value)
		{
			element.SetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty, BooleanBoxes.Box(value));
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.IsVisualHostMaterial" /> para um <see cref="T:System.Windows.UIElement" /> especificado.</summary>
		/// <param name="element">O elemento do qual o valor da propriedade é lido.</param>
		/// <returns>O valor da propriedade <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.IsVisualHostMaterial" /> para o elemento.</returns>
		// Token: 0x06003268 RID: 12904 RVA: 0x000C91BC File Offset: 0x000C85BC
		public static bool GetIsVisualHostMaterial(Material element)
		{
			return (bool)element.GetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty);
		}

		/// <summary>Obtém ou define uma representação armazenada em cache do <see cref="T:System.Windows.Media.Media3D.Viewport2DVisual3D" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.CacheMode" /> que contém uma representação armazenada em cache do <see cref="T:System.Windows.Media.Media3D.Viewport2DVisual3D" />.</returns>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000C91DC File Offset: 0x000C85DC
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x000C91FC File Offset: 0x000C85FC
		public CacheMode CacheMode
		{
			get
			{
				return (CacheMode)base.GetValue(Viewport2DVisual3D.CacheModeProperty);
			}
			set
			{
				base.SetValue(Viewport2DVisual3D.CacheModeProperty, value);
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000C9218 File Offset: 0x000C8618
		internal static void OnCacheModeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)sender;
			BitmapCache bitmapCache = ((CacheMode)e.OldValue) as BitmapCache;
			BitmapCache bitmapCache2 = ((CacheMode)e.NewValue) as BitmapCache;
			if (bitmapCache != bitmapCache2)
			{
				viewport2DVisual3D.InternalBitmapCacheBrush.BitmapCache = bitmapCache2;
				if (bitmapCache == null)
				{
					viewport2DVisual3D.RemoveVisualChild(viewport2DVisual3D.Visual);
					viewport2DVisual3D.AddVisualChild(viewport2DVisual3D.InternalBitmapCacheBrush.InternalTarget);
					viewport2DVisual3D.InternalVisualBrush.Visual = null;
					viewport2DVisual3D.InternalBitmapCacheBrush.Target = viewport2DVisual3D.Visual;
				}
				if (bitmapCache2 == null)
				{
					viewport2DVisual3D.InternalBitmapCacheBrush.Target = null;
					viewport2DVisual3D.InternalVisualBrush.Visual = viewport2DVisual3D.Visual;
					viewport2DVisual3D.RemoveVisualChild(viewport2DVisual3D.InternalBitmapCacheBrush.InternalTarget);
					viewport2DVisual3D.AddVisualChild(viewport2DVisual3D.Visual);
				}
				if (bitmapCache == null || bitmapCache2 == null)
				{
					viewport2DVisual3D.GenerateMaterial();
				}
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000C92EC File Offset: 0x000C86EC
		protected override int Visual3DChildrenCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000C92FC File Offset: 0x000C86FC
		protected override Visual3D GetVisual3DChild(int index)
		{
			throw new ArgumentOutOfRangeException("index", index, SR.Get("Visual_ArgumentOutOfRange"));
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000C9324 File Offset: 0x000C8724
		internal override int InternalVisual2DOr3DChildrenCount
		{
			get
			{
				if (this.Visual == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000C933C File Offset: 0x000C873C
		internal override DependencyObject InternalGet2DOr3DVisualChild(int index)
		{
			Visual visual = this.Visual;
			if (index != 0 || visual == null)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Get("Visual_ArgumentOutOfRange"));
			}
			return visual;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000C9374 File Offset: 0x000C8774
		private static void OnCachingHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)d;
			RenderOptions.SetCachingHint(viewport2DVisual3D._visualBrush, (CachingHint)e.NewValue);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000C93A0 File Offset: 0x000C87A0
		private static void OnCacheInvalidationThresholdMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)d;
			RenderOptions.SetCacheInvalidationThresholdMinimum(viewport2DVisual3D._visualBrush, (double)e.NewValue);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000C93CC File Offset: 0x000C87CC
		private static void OnCacheInvalidationThresholdMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Viewport2DVisual3D viewport2DVisual3D = (Viewport2DVisual3D)d;
			RenderOptions.SetCacheInvalidationThresholdMaximum(viewport2DVisual3D._visualBrush, (double)e.NewValue);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.Visual" />.</summary>
		// Token: 0x040015C9 RID: 5577
		public static readonly DependencyProperty VisualProperty = VisualBrush.VisualProperty.AddOwner(typeof(Viewport2DVisual3D), new PropertyMetadata(null, new PropertyChangedCallback(Viewport2DVisual3D.OnVisualChanged)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.Geometry" />.</summary>
		// Token: 0x040015CA RID: 5578
		public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register("Geometry", typeof(Geometry3D), typeof(Viewport2DVisual3D), new PropertyMetadata(null, new PropertyChangedCallback(Viewport2DVisual3D.OnGeometryChanged)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.Material" />.</summary>
		// Token: 0x040015CB RID: 5579
		public static readonly DependencyProperty MaterialProperty = DependencyProperty.Register("Material", typeof(Material), typeof(Viewport2DVisual3D), new PropertyMetadata(null, new PropertyChangedCallback(Viewport2DVisual3D.OnMaterialPropertyChanged)));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.IsVisualHostMaterial" /> anexada.</summary>
		// Token: 0x040015CC RID: 5580
		public static readonly DependencyProperty IsVisualHostMaterialProperty = DependencyProperty.RegisterAttached("IsVisualHostMaterial", typeof(bool), typeof(Viewport2DVisual3D), new PropertyMetadata(BooleanBoxes.FalseBox));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport2DVisual3D.CacheMode" />.</summary>
		// Token: 0x040015CD RID: 5581
		public static readonly DependencyProperty CacheModeProperty = DependencyProperty.Register("CacheMode", typeof(CacheMode), typeof(Viewport2DVisual3D), new PropertyMetadata(null, new PropertyChangedCallback(Viewport2DVisual3D.OnCacheModeChanged)));

		// Token: 0x040015CE RID: 5582
		private static readonly DependencyProperty CachingHintProperty = RenderOptions.CachingHintProperty.AddOwner(typeof(Viewport2DVisual3D), new UIPropertyMetadata(new PropertyChangedCallback(Viewport2DVisual3D.OnCachingHintChanged)));

		// Token: 0x040015CF RID: 5583
		private static readonly DependencyProperty CacheInvalidationThresholdMinimumProperty = RenderOptions.CacheInvalidationThresholdMinimumProperty.AddOwner(typeof(Viewport2DVisual3D), new UIPropertyMetadata(new PropertyChangedCallback(Viewport2DVisual3D.OnCacheInvalidationThresholdMinimumChanged)));

		// Token: 0x040015D0 RID: 5584
		private static readonly DependencyProperty CacheInvalidationThresholdMaximumProperty = RenderOptions.CacheInvalidationThresholdMaximumProperty.AddOwner(typeof(Viewport2DVisual3D), new UIPropertyMetadata(new PropertyChangedCallback(Viewport2DVisual3D.OnCacheInvalidationThresholdMaximumChanged)));

		// Token: 0x040015D1 RID: 5585
		private VisualBrush _visualBrush;

		// Token: 0x040015D2 RID: 5586
		private BitmapCacheBrush _bitmapCacheBrush;

		// Token: 0x040015D3 RID: 5587
		private Point3DCollection _positionsCache;

		// Token: 0x040015D4 RID: 5588
		private PointCollection _textureCoordinatesCache;

		// Token: 0x040015D5 RID: 5589
		private Int32Collection _triangleIndicesCache;
	}
}
