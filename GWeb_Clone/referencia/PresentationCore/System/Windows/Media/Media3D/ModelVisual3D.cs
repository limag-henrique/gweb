using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece um <see cref="T:System.Windows.Media.Media3D.Visual3D" /> que renderiza objetos <see cref="T:System.Windows.Media.Media3D.Model3D" />.</summary>
	// Token: 0x0200046C RID: 1132
	[ContentProperty("Children")]
	public class ModelVisual3D : Visual3D, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.ModelVisual3D" />.</summary>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000BF570 File Offset: 0x000BE970
		public ModelVisual3D()
		{
			this._children = new Visual3DCollection(this);
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado na coleção pai.</summary>
		/// <param name="index">O índice do objeto visual 3D na coleção.</param>
		/// <returns>O filho na coleção no índice especificado.</returns>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000BF590 File Offset: 0x000BE990
		protected sealed override Visual3D GetVisual3DChild(int index)
		{
			return this._children[index];
		}

		/// <summary>Retorna o número de objetos filho.</summary>
		/// <returns>O número de objetos filho.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000BF5AC File Offset: 0x000BE9AC
		protected sealed override int Visual3DChildrenCount
		{
			get
			{
				return this._children.Count;
			}
		}

		/// <summary>Adiciona um objeto filho.</summary>
		/// <param name="value">O objeto filho a ser adicionado.</param>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000BF5C4 File Offset: 0x000BE9C4
		void IAddChild.AddChild(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Visual3D visual3D = value as Visual3D;
			if (visual3D == null)
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					typeof(Visual3D).Name
				}));
			}
			this.Children.Add(visual3D);
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="text">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000BF63C File Offset: 0x000BEA3C
		void IAddChild.AddText(string text)
		{
			foreach (char c in text)
			{
				if (!char.IsWhiteSpace(c))
				{
					throw new InvalidOperationException(SR.Get("AddText_Invalid", new object[]
					{
						base.GetType().Name
					}));
				}
			}
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" /> filho.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> que contém o filho <see cref="T:System.Windows.Media.Media3D.Visual3D" /> objetos.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000BF690 File Offset: 0x000BEA90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Visual3DCollection Children
		{
			get
			{
				base.VerifyAPIReadOnly();
				return this._children;
			}
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000BF6AC File Offset: 0x000BEAAC
		private static void ContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ModelVisual3D modelVisual3D = (ModelVisual3D)d;
			if (!e.IsASubPropertyChange)
			{
				modelVisual3D.Visual3DModel = (Model3D)e.NewValue;
			}
		}

		/// <summary>Obtém ou define o que inclui o conteúdo do <see cref="T:System.Windows.Media.Media3D.ModelVisual3D" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Media.Media3D.Model3D" /> que inclui o conteúdo do <see cref="T:System.Windows.Media.Media3D.ModelVisual3D" />.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000BF6DC File Offset: 0x000BEADC
		// (set) Token: 0x06002FCC RID: 12236 RVA: 0x000BF6FC File Offset: 0x000BEAFC
		public Model3D Content
		{
			get
			{
				return (Model3D)base.GetValue(ModelVisual3D.ContentProperty);
			}
			set
			{
				base.SetValue(ModelVisual3D.ContentProperty, value);
			}
		}

		/// <summary>Obtém ou define a transformação definida no <see cref="T:System.Windows.Media.Media3D.ModelVisual3D" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Transform3D" /> definido no <see cref="T:System.Windows.Media.Media3D.ModelVisual3D" />.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000BF718 File Offset: 0x000BEB18
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x000BF738 File Offset: 0x000BEB38
		public new Transform3D Transform
		{
			get
			{
				return (Transform3D)base.GetValue(ModelVisual3D.TransformProperty);
			}
			set
			{
				base.SetValue(ModelVisual3D.TransformProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ModelVisual3D.Content" />.</summary>
		// Token: 0x0400153C RID: 5436
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(Model3D), typeof(ModelVisual3D), new PropertyMetadata(new PropertyChangedCallback(ModelVisual3D.ContentPropertyChanged)), (object <p0>) => MediaContext.CurrentMediaContext.WriteAccessEnabled);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ModelVisual3D.Transform" />.</summary>
		// Token: 0x0400153D RID: 5437
		public new static readonly DependencyProperty TransformProperty = Visual3D.TransformProperty;

		// Token: 0x0400153E RID: 5438
		private readonly Visual3DCollection _children;
	}
}
