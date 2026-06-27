using System;
using System.Windows;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media3D
{
	// Token: 0x020006F2 RID: 1778
	internal class GeneralTransform3DTo2DTo3D : GeneralTransform3D
	{
		// Token: 0x06004CA2 RID: 19618 RVA: 0x0012D8B4 File Offset: 0x0012CCB4
		internal GeneralTransform3DTo2DTo3D()
		{
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0012D8C8 File Offset: 0x0012CCC8
		internal GeneralTransform3DTo2DTo3D(GeneralTransform3DTo2D transform3DTo2D, GeneralTransform2DTo3D transform2DTo3D)
		{
			this._transform3DTo2D = (GeneralTransform3DTo2D)transform3DTo2D.GetAsFrozen();
			this._transform2DTo3D = (GeneralTransform2DTo3D)transform2DTo3D.GetAsFrozen();
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0012D900 File Offset: 0x0012CD00
		public override bool TryTransform(Point3D inPoint, out Point3D result)
		{
			Point inPoint2 = default(Point);
			result = default(Point3D);
			return this._transform3DTo2D != null && this._transform3DTo2D.TryTransform(inPoint, out inPoint2) && this._transform2DTo3D != null && this._transform2DTo3D.TryTransform(inPoint2, out result);
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x0012D950 File Offset: 0x0012CD50
		public override GeneralTransform3D Inverse
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x0012D960 File Offset: 0x0012CD60
		internal override Transform3D AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				return null;
			}
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0012D970 File Offset: 0x0012CD70
		public override Rect3D TransformBounds(Rect3D rect)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0012D984 File Offset: 0x0012CD84
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform3DTo2DTo3D();
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0012D998 File Offset: 0x0012CD98
		protected override void CloneCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2DTo3D transform = (GeneralTransform3DTo2DTo3D)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0012D9BC File Offset: 0x0012CDBC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2DTo3D transform = (GeneralTransform3DTo2DTo3D)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0012D9E0 File Offset: 0x0012CDE0
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2DTo3D transform = (GeneralTransform3DTo2DTo3D)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0012DA04 File Offset: 0x0012CE04
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2DTo3D transform = (GeneralTransform3DTo2DTo3D)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0012DA28 File Offset: 0x0012CE28
		private void CopyCommon(GeneralTransform3DTo2DTo3D transform)
		{
			this._transform3DTo2D = transform._transform3DTo2D;
			this._transform2DTo3D = transform._transform2DTo3D;
		}

		// Token: 0x04002153 RID: 8531
		private GeneralTransform3DTo2D _transform3DTo2D;

		// Token: 0x04002154 RID: 8532
		private GeneralTransform2DTo3D _transform2DTo3D;
	}
}
