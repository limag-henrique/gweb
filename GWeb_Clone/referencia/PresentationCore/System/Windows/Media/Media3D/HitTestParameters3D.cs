using System;

namespace System.Windows.Media.Media3D
{
	/// <summary>Classe abstrata que representa os parâmetros de um teste de clique 3D.</summary>
	// Token: 0x0200045F RID: 1119
	public abstract class HitTestParameters3D
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x000B9CCC File Offset: 0x000B90CC
		internal HitTestParameters3D()
		{
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000B9CF8 File Offset: 0x000B90F8
		internal void PushVisualTransform(Transform3D transform)
		{
			if (transform != null && transform != Transform3D.Identity)
			{
				this._visualTransformStack.Push(transform.Value);
			}
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B9D24 File Offset: 0x000B9124
		internal void PushModelTransform(Transform3D transform)
		{
			if (transform != null && transform != Transform3D.Identity)
			{
				this._modelTransformStack.Push(transform.Value);
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000B9D50 File Offset: 0x000B9150
		internal void PopTransform(Transform3D transform)
		{
			if (transform != null && transform != Transform3D.Identity)
			{
				if (this._modelTransformStack.Count > 0)
				{
					this._modelTransformStack.Pop();
					return;
				}
				this._visualTransformStack.Pop();
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000B9D90 File Offset: 0x000B9190
		internal bool HasWorldTransformMatrix
		{
			get
			{
				return this._visualTransformStack.Count > 0 || this._modelTransformStack.Count > 0;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000B9DBC File Offset: 0x000B91BC
		internal Matrix3D WorldTransformMatrix
		{
			get
			{
				if (this._modelTransformStack.IsEmpty)
				{
					return this._visualTransformStack.Top;
				}
				if (this._visualTransformStack.IsEmpty)
				{
					return this._modelTransformStack.Top;
				}
				return this._modelTransformStack.Top * this._visualTransformStack.Top;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x000B9E18 File Offset: 0x000B9218
		internal bool HasModelTransformMatrix
		{
			get
			{
				return this._modelTransformStack.Count > 0;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000B9E34 File Offset: 0x000B9234
		internal Matrix3D ModelTransformMatrix
		{
			get
			{
				return this._modelTransformStack.Top;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000B9E4C File Offset: 0x000B924C
		internal bool HasHitTestProjectionMatrix
		{
			get
			{
				return this._hitTestProjectionMatrix != null;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000B9E64 File Offset: 0x000B9264
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x000B9E7C File Offset: 0x000B927C
		internal Matrix3D HitTestProjectionMatrix
		{
			get
			{
				return this._hitTestProjectionMatrix.Value;
			}
			set
			{
				this._hitTestProjectionMatrix = new Matrix3D?(value);
			}
		}

		// Token: 0x04001503 RID: 5379
		internal Visual3D CurrentVisual;

		// Token: 0x04001504 RID: 5380
		internal Model3D CurrentModel;

		// Token: 0x04001505 RID: 5381
		internal GeometryModel3D CurrentGeometry;

		// Token: 0x04001506 RID: 5382
		private Matrix3D? _hitTestProjectionMatrix;

		// Token: 0x04001507 RID: 5383
		private Matrix3DStack _visualTransformStack = new Matrix3DStack();

		// Token: 0x04001508 RID: 5384
		private Matrix3DStack _modelTransformStack = new Matrix3DStack();
	}
}
