using System;
using System.ComponentModel;
using System.Windows.Automation.Peers;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um contêiner para objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
	// Token: 0x02000455 RID: 1109
	[ContentProperty("Children")]
	public sealed class ContainerUIElement3D : UIElement3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ContainerUIElement3D" />.</summary>
		// Token: 0x06002E0E RID: 11790 RVA: 0x000B8160 File Offset: 0x000B7560
		public ContainerUIElement3D()
		{
			this._children = new Visual3DCollection(this);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000B8180 File Offset: 0x000B7580
		protected override Visual3D GetVisual3DChild(int index)
		{
			return this._children[index];
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000B819C File Offset: 0x000B759C
		protected override int Visual3DChildrenCount
		{
			get
			{
				return this._children.Count;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> de elementos filho deste <see cref="T:System.Windows.Media.Media3D.ContainerUIElement3D" /> objeto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> de elementos filho. O padrão é uma coleção vazia.</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000B81B4 File Offset: 0x000B75B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Visual3DCollection Children
		{
			get
			{
				base.VerifyAPIReadOnly();
				return this._children;
			}
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000B81D0 File Offset: 0x000B75D0
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new UIElement3DAutomationPeer(this);
		}

		// Token: 0x040014E5 RID: 5349
		private readonly Visual3DCollection _children;
	}
}
