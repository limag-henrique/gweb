using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.Media3D;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Triângulo primitivo para criação de uma forma 3D.</summary>
	// Token: 0x02000468 RID: 1128
	public sealed class MeshGeometry3D : Geometry3D
	{
		/// <summary>Obtém o <see cref="T:System.Windows.Media.Media3D.Rect3D" /> delimitador para este <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
		/// <returns>Delimitação <see cref="T:System.Windows.Media.Media3D.Rect3D" /> para o <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</returns>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000BDE00 File Offset: 0x000BD200
		public override Rect3D Bounds
		{
			get
			{
				base.ReadPreamble();
				if (this._cachedBounds.IsEmpty)
				{
					this.UpdateCachedBounds();
				}
				return this._cachedBounds;
			}
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000BDE2C File Offset: 0x000BD22C
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			if (e.IsAValueChange || e.IsASubPropertyChange)
			{
				DependencyProperty property = e.Property;
				if (property == MeshGeometry3D.PositionsProperty)
				{
					this.SetCachedBoundsDirty();
				}
			}
			base.OnPropertyChanged(e);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000BDE68 File Offset: 0x000BD268
		internal Rect GetTextureCoordinateBounds()
		{
			PointCollection textureCoordinates = this.TextureCoordinates;
			int num = (textureCoordinates == null) ? 0 : textureCoordinates.Count;
			if (num > 0)
			{
				Point point = textureCoordinates[0];
				Point point2 = textureCoordinates[0];
				for (int i = 1; i < num; i++)
				{
					Point point3 = textureCoordinates.Internal_GetItem(i);
					double x = point3.X;
					if (point.X > x)
					{
						point.X = x;
					}
					else if (point2.X < x)
					{
						point2.X = x;
					}
					double y = point3.Y;
					if (point.Y > y)
					{
						point.Y = y;
					}
					else if (point2.Y < y)
					{
						point2.Y = y;
					}
				}
				return new Rect(point, point2);
			}
			return Rect.Empty;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000BDF28 File Offset: 0x000BD328
		internal override void RayHitTestCore(RayHitTestParameters rayParams, FaceType hitTestableFaces)
		{
			Point3DCollection positions = this.Positions;
			if (positions == null)
			{
				return;
			}
			Point3D point3D;
			Vector3D vector3D;
			rayParams.GetLocalLine(out point3D, out vector3D);
			Int32Collection triangleIndices = this.TriangleIndices;
			FaceType type;
			if (rayParams.IsRay)
			{
				type = hitTestableFaces;
			}
			else
			{
				type = (FaceType.Front | FaceType.Back);
			}
			if (triangleIndices == null || triangleIndices.Count == 0)
			{
				FrugalStructList<Point3D> collection = positions._collection;
				int num = collection.Count - collection.Count % 3;
				for (int i = num - 1; i >= 2; i -= 3)
				{
					int num2 = i - 2;
					int num3 = i - 1;
					int num4 = i;
					Point3D point3D2 = collection[num2];
					Point3D point3D3 = collection[num3];
					Point3D point3D4 = collection[num4];
					Point point;
					double hitTime;
					if (LineUtil.ComputeLineTriangleIntersection(type, ref point3D, ref vector3D, ref point3D2, ref point3D3, ref point3D4, out point, out hitTime))
					{
						if (rayParams.IsRay)
						{
							this.ValidateRayHit(rayParams, ref point3D, ref vector3D, hitTime, num2, num3, num4, ref point);
						}
						else
						{
							this.ValidateLineHit(rayParams, hitTestableFaces, num2, num3, num4, ref point3D2, ref point3D3, ref point3D4, ref point);
						}
					}
				}
				return;
			}
			FrugalStructList<Point3D> collection2 = positions._collection;
			FrugalStructList<int> collection3 = triangleIndices._collection;
			int count = collection3.Count;
			int count2 = collection2.Count;
			for (int j = 2; j < count; j += 3)
			{
				int num5 = collection3[j - 2];
				int num6 = collection3[j - 1];
				int num7 = collection3[j];
				if (0 > num5 || num5 >= count2 || 0 > num6 || num6 >= count2 || 0 > num7 || num7 >= count2)
				{
					break;
				}
				Point3D point3D5 = collection2[num5];
				Point3D point3D6 = collection2[num6];
				Point3D point3D7 = collection2[num7];
				Point point2;
				double hitTime2;
				if (LineUtil.ComputeLineTriangleIntersection(type, ref point3D, ref vector3D, ref point3D5, ref point3D6, ref point3D7, out point2, out hitTime2))
				{
					if (rayParams.IsRay)
					{
						this.ValidateRayHit(rayParams, ref point3D, ref vector3D, hitTime2, num5, num6, num7, ref point2);
					}
					else
					{
						this.ValidateLineHit(rayParams, hitTestableFaces, num5, num6, num7, ref point3D5, ref point3D6, ref point3D7, ref point2);
					}
				}
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000BE10C File Offset: 0x000BD50C
		private void ValidateRayHit(RayHitTestParameters rayParams, ref Point3D origin, ref Vector3D direction, double hitTime, int i0, int i1, int i2, ref Point barycentric)
		{
			if (hitTime > 0.0)
			{
				Matrix3D matrix3D = rayParams.HasWorldTransformMatrix ? rayParams.WorldTransformMatrix : Matrix3D.Identity;
				Point3D point3D = origin + hitTime * direction;
				Point3D point = point3D;
				matrix3D.MultiplyPoint(ref point);
				if (rayParams.HasHitTestProjectionMatrix)
				{
					Matrix3D hitTestProjectionMatrix = rayParams.HitTestProjectionMatrix;
					double num = point.X * hitTestProjectionMatrix.M13 + point.Y * hitTestProjectionMatrix.M23 + point.Z * hitTestProjectionMatrix.M33 + hitTestProjectionMatrix.OffsetZ;
					double num2 = point.X * hitTestProjectionMatrix.M14 + point.Y * hitTestProjectionMatrix.M24 + point.Z * hitTestProjectionMatrix.M34 + hitTestProjectionMatrix.M44;
					if (num / num2 > 1.0)
					{
						return;
					}
				}
				double length = (point - rayParams.Origin).Length;
				if (rayParams.HasModelTransformMatrix)
				{
					rayParams.ModelTransformMatrix.MultiplyPoint(ref point3D);
				}
				rayParams.ReportResult(this, point3D, length, i0, i1, i2, barycentric);
			}
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000BE240 File Offset: 0x000BD640
		private void ValidateLineHit(RayHitTestParameters rayParams, FaceType facesToHit, int i0, int i1, int i2, ref Point3D v0, ref Point3D v1, ref Point3D v2, ref Point barycentric)
		{
			Matrix3D matrix3D = rayParams.HasWorldTransformMatrix ? rayParams.WorldTransformMatrix : Matrix3D.Identity;
			Point3D point3D = M3DUtil.Interpolate(ref v0, ref v1, ref v2, ref barycentric);
			Point3D point = point3D;
			matrix3D.MultiplyPoint(ref point);
			Vector3D vector = point - rayParams.Origin;
			Vector3D direction = rayParams.Direction;
			double num = Vector3D.DotProduct(direction, vector);
			if (num > 0.0)
			{
				if (rayParams.HasHitTestProjectionMatrix)
				{
					Matrix3D hitTestProjectionMatrix = rayParams.HitTestProjectionMatrix;
					double num2 = point.X * hitTestProjectionMatrix.M13 + point.Y * hitTestProjectionMatrix.M23 + point.Z * hitTestProjectionMatrix.M33 + hitTestProjectionMatrix.OffsetZ;
					double num3 = point.X * hitTestProjectionMatrix.M14 + point.Y * hitTestProjectionMatrix.M24 + point.Z * hitTestProjectionMatrix.M34 + hitTestProjectionMatrix.M44;
					if (num2 / num3 > 1.0)
					{
						return;
					}
				}
				Point3D point2 = v0;
				Point3D point3 = v1;
				Point3D point4 = v2;
				matrix3D.MultiplyPoint(ref point2);
				matrix3D.MultiplyPoint(ref point3);
				matrix3D.MultiplyPoint(ref point4);
				Vector3D vector2 = Vector3D.CrossProduct(point3 - point2, point4 - point2);
				double num4 = -Vector3D.DotProduct(vector2, vector);
				double determinant = matrix3D.Determinant;
				bool flag = num4 > 0.0 == determinant >= 0.0;
				if (((facesToHit & FaceType.Front) == FaceType.Front && flag) || ((facesToHit & FaceType.Back) == FaceType.Back && !flag))
				{
					double length = vector.Length;
					if (rayParams.HasModelTransformMatrix)
					{
						rayParams.ModelTransformMatrix.MultiplyPoint(ref point3D);
					}
					rayParams.ReportResult(this, point3D, length, i0, i1, i2, barycentric);
				}
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000BE410 File Offset: 0x000BD810
		private void UpdateCachedBounds()
		{
			this._cachedBounds = M3DUtil.ComputeAxisAlignedBoundingBox(this.Positions);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000BE430 File Offset: 0x000BD830
		private void SetCachedBoundsDirty()
		{
			this._cachedBounds = Rect3D.Empty;
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000BE448 File Offset: 0x000BD848
		[Conditional("DEBUG")]
		private void Debug_VerifyCachedBounds()
		{
			Rect3D rect3D = M3DUtil.ComputeAxisAlignedBoundingBox(this.Positions);
			if (this._cachedBounds.X < rect3D.X || this._cachedBounds.X > rect3D.X || this._cachedBounds.Y < rect3D.Y || this._cachedBounds.Y > rect3D.Y || this._cachedBounds.Z < rect3D.Z || this._cachedBounds.Z > rect3D.Z || this._cachedBounds.SizeX < rect3D.SizeX || this._cachedBounds.SizeX > rect3D.SizeX || this._cachedBounds.SizeY < rect3D.SizeY || this._cachedBounds.SizeY > rect3D.SizeY || this._cachedBounds.SizeZ < rect3D.SizeZ || this._cachedBounds.SizeZ > rect3D.SizeZ)
			{
				this._cachedBounds == Rect3D.Empty;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002F79 RID: 12153 RVA: 0x000BE584 File Offset: 0x000BD984
		public new MeshGeometry3D Clone()
		{
			return (MeshGeometry3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F7A RID: 12154 RVA: 0x000BE59C File Offset: 0x000BD99C
		public new MeshGeometry3D CloneCurrentValue()
		{
			return (MeshGeometry3D)base.CloneCurrentValue();
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000BE5B4 File Offset: 0x000BD9B4
		private static void PositionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MeshGeometry3D meshGeometry3D = (MeshGeometry3D)d;
			meshGeometry3D.PropertyChanged(MeshGeometry3D.PositionsProperty);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000BE5D4 File Offset: 0x000BD9D4
		private static void NormalsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MeshGeometry3D meshGeometry3D = (MeshGeometry3D)d;
			meshGeometry3D.PropertyChanged(MeshGeometry3D.NormalsProperty);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000BE5F4 File Offset: 0x000BD9F4
		private static void TextureCoordinatesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MeshGeometry3D meshGeometry3D = (MeshGeometry3D)d;
			meshGeometry3D.PropertyChanged(MeshGeometry3D.TextureCoordinatesProperty);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000BE614 File Offset: 0x000BDA14
		private static void TriangleIndicesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MeshGeometry3D meshGeometry3D = (MeshGeometry3D)d;
			meshGeometry3D.PropertyChanged(MeshGeometry3D.TriangleIndicesProperty);
		}

		/// <summary>Obtém ou define uma coleção de posições de vértice para um <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> que contém as posições de vértice de MeshGeometry3D.</returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000BE634 File Offset: 0x000BDA34
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x000BE654 File Offset: 0x000BDA54
		public Point3DCollection Positions
		{
			get
			{
				return (Point3DCollection)base.GetValue(MeshGeometry3D.PositionsProperty);
			}
			set
			{
				base.SetValueInternal(MeshGeometry3D.PositionsProperty, value);
			}
		}

		/// <summary>Obtém ou define uma coleção de vetores normais para o <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> que contém os vetores normais para MeshGeometry3D.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000BE670 File Offset: 0x000BDA70
		// (set) Token: 0x06002F82 RID: 12162 RVA: 0x000BE690 File Offset: 0x000BDA90
		public Vector3DCollection Normals
		{
			get
			{
				return (Vector3DCollection)base.GetValue(MeshGeometry3D.NormalsProperty);
			}
			set
			{
				base.SetValueInternal(MeshGeometry3D.NormalsProperty, value);
			}
		}

		/// <summary>Obtém ou define um conjunto de coordenadas de textura para o <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.PointCollection" /> que contém as coordenadas de textura para o MeshGeometry3D.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000BE6AC File Offset: 0x000BDAAC
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000BE6CC File Offset: 0x000BDACC
		public PointCollection TextureCoordinates
		{
			get
			{
				return (PointCollection)base.GetValue(MeshGeometry3D.TextureCoordinatesProperty);
			}
			set
			{
				base.SetValueInternal(MeshGeometry3D.TextureCoordinatesProperty, value);
			}
		}

		/// <summary>Obtém ou define uma coleção de índices de triângulo para o <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
		/// <returns>Coleção que contém os índices de triângulo do MeshGeometry3D.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000BE6E8 File Offset: 0x000BDAE8
		// (set) Token: 0x06002F86 RID: 12166 RVA: 0x000BE708 File Offset: 0x000BDB08
		public Int32Collection TriangleIndices
		{
			get
			{
				return (Int32Collection)base.GetValue(MeshGeometry3D.TriangleIndicesProperty);
			}
			set
			{
				base.SetValueInternal(MeshGeometry3D.TriangleIndicesProperty, value);
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000BE724 File Offset: 0x000BDB24
		protected override Freezable CreateInstanceCore()
		{
			return new MeshGeometry3D();
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000BE738 File Offset: 0x000BDB38
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Point3DCollection positions = this.Positions;
				Vector3DCollection normals = this.Normals;
				PointCollection textureCoordinates = this.TextureCoordinates;
				Int32Collection triangleIndices = this.TriangleIndices;
				int num = (positions == null) ? 0 : positions.Count;
				int num2 = (normals == null) ? 0 : normals.Count;
				int num3 = (textureCoordinates == null) ? 0 : textureCoordinates.Count;
				int num4 = (triangleIndices == null) ? 0 : triangleIndices.Count;
				DUCE.MILCMD_MESHGEOMETRY3D milcmd_MESHGEOMETRY3D;
				milcmd_MESHGEOMETRY3D.Type = MILCMD.MilCmdMeshGeometry3D;
				milcmd_MESHGEOMETRY3D.Handle = this._duceResource.GetHandle(channel);
				milcmd_MESHGEOMETRY3D.PositionsSize = (uint)(sizeof(MilPoint3F) * num);
				milcmd_MESHGEOMETRY3D.NormalsSize = (uint)(sizeof(MilPoint3F) * num2);
				milcmd_MESHGEOMETRY3D.TextureCoordinatesSize = (uint)(sizeof(Point) * num3);
				milcmd_MESHGEOMETRY3D.TriangleIndicesSize = (uint)(4 * num4);
				channel.BeginCommand((byte*)(&milcmd_MESHGEOMETRY3D), sizeof(DUCE.MILCMD_MESHGEOMETRY3D), (int)(milcmd_MESHGEOMETRY3D.PositionsSize + milcmd_MESHGEOMETRY3D.NormalsSize + milcmd_MESHGEOMETRY3D.TextureCoordinatesSize + milcmd_MESHGEOMETRY3D.TriangleIndicesSize));
				for (int i = 0; i < num; i++)
				{
					MilPoint3F milPoint3F = CompositionResourceManager.Point3DToMilPoint3F(positions.Internal_GetItem(i));
					channel.AppendCommandData((byte*)(&milPoint3F), sizeof(MilPoint3F));
				}
				for (int j = 0; j < num2; j++)
				{
					MilPoint3F milPoint3F2 = CompositionResourceManager.Vector3DToMilPoint3F(normals.Internal_GetItem(j));
					channel.AppendCommandData((byte*)(&milPoint3F2), sizeof(MilPoint3F));
				}
				for (int k = 0; k < num3; k++)
				{
					Point point = textureCoordinates.Internal_GetItem(k);
					channel.AppendCommandData((byte*)(&point), sizeof(Point));
				}
				for (int l = 0; l < num4; l++)
				{
					int num5 = triangleIndices.Internal_GetItem(l);
					channel.AppendCommandData((byte*)(&num5), 4);
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000BE8EC File Offset: 0x000BDCEC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MESHGEOMETRY3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000BE928 File Offset: 0x000BDD28
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000BE94C File Offset: 0x000BDD4C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000BE968 File Offset: 0x000BDD68
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000BE980 File Offset: 0x000BDD80
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000BE99C File Offset: 0x000BDD9C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000BE9AC File Offset: 0x000BDDAC
		static MeshGeometry3D()
		{
			Type typeFromHandle = typeof(MeshGeometry3D);
			MeshGeometry3D.PositionsProperty = Animatable.RegisterProperty("Positions", typeof(Point3DCollection), typeFromHandle, new FreezableDefaultValueFactory(Point3DCollection.Empty), new PropertyChangedCallback(MeshGeometry3D.PositionsPropertyChanged), null, false, null);
			MeshGeometry3D.NormalsProperty = Animatable.RegisterProperty("Normals", typeof(Vector3DCollection), typeFromHandle, new FreezableDefaultValueFactory(Vector3DCollection.Empty), new PropertyChangedCallback(MeshGeometry3D.NormalsPropertyChanged), null, false, null);
			MeshGeometry3D.TextureCoordinatesProperty = Animatable.RegisterProperty("TextureCoordinates", typeof(PointCollection), typeFromHandle, new FreezableDefaultValueFactory(PointCollection.Empty), new PropertyChangedCallback(MeshGeometry3D.TextureCoordinatesPropertyChanged), null, false, null);
			MeshGeometry3D.TriangleIndicesProperty = Animatable.RegisterProperty("TriangleIndices", typeof(Int32Collection), typeFromHandle, new FreezableDefaultValueFactory(Int32Collection.Empty), new PropertyChangedCallback(MeshGeometry3D.TriangleIndicesPropertyChanged), null, false, null);
		}

		// Token: 0x0400152B RID: 5419
		private Rect3D _cachedBounds = Rect3D.Empty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MeshGeometry3D.Positions" />.</summary>
		// Token: 0x0400152C RID: 5420
		public static readonly DependencyProperty PositionsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MeshGeometry3D.Normals" />.</summary>
		// Token: 0x0400152D RID: 5421
		public static readonly DependencyProperty NormalsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MeshGeometry3D.TextureCoordinates" />.</summary>
		// Token: 0x0400152E RID: 5422
		public static readonly DependencyProperty TextureCoordinatesProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MeshGeometry3D.TriangleIndices" />.</summary>
		// Token: 0x0400152F RID: 5423
		public static readonly DependencyProperty TriangleIndicesProperty;

		// Token: 0x04001530 RID: 5424
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001531 RID: 5425
		internal static Point3DCollection s_Positions = Point3DCollection.Empty;

		// Token: 0x04001532 RID: 5426
		internal static Vector3DCollection s_Normals = Vector3DCollection.Empty;

		// Token: 0x04001533 RID: 5427
		internal static PointCollection s_TextureCoordinates = PointCollection.Empty;

		// Token: 0x04001534 RID: 5428
		internal static Int32Collection s_TriangleIndices = Int32Collection.Empty;
	}
}
