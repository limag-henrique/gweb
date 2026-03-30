using System;
using System.Windows.Automation.Peers;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D
{
	/// <summary>Renderiza um modelo 3D que dá suporte a entrada, foco e eventos.</summary>
	// Token: 0x0200046B RID: 1131
	[ContentProperty("Model")]
	public sealed class ModelUIElement3D : UIElement3D
	{
		// Token: 0x06002FBF RID: 12223 RVA: 0x000BF49C File Offset: 0x000BE89C
		private static void ModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ModelUIElement3D modelUIElement3D = (ModelUIElement3D)d;
			if (!e.IsASubPropertyChange)
			{
				modelUIElement3D.Visual3DModel = (Model3D)e.NewValue;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Model3D" /> a ser renderizado.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Media3D.Model3D" /> a ser renderizado.</returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000BF4CC File Offset: 0x000BE8CC
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x000BF4EC File Offset: 0x000BE8EC
		public Model3D Model
		{
			get
			{
				return (Model3D)base.GetValue(ModelUIElement3D.ModelProperty);
			}
			set
			{
				base.SetValue(ModelUIElement3D.ModelProperty, value);
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000BF508 File Offset: 0x000BE908
		protected override AutomationPeer OnCreateAutomationPeer()
		{
			return new UIElement3DAutomationPeer(this);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ModelUIElement3D.Model" />.</summary>
		// Token: 0x0400153B RID: 5435
		public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(Model3D), typeof(ModelUIElement3D), new PropertyMetadata(new PropertyChangedCallback(ModelUIElement3D.ModelPropertyChanged)), (object <p0>) => MediaContext.CurrentMediaContext.WriteAccessEnabled);
	}
}
