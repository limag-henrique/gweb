using System;
using System.ComponentModel;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.Ink.InkSerializedFormat;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Especifica a aparência de um <see cref="T:System.Windows.Ink.Stroke" /></summary>
	// Token: 0x0200034F RID: 847
	public class DrawingAttributes : INotifyPropertyChanged
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
		// Token: 0x06001C71 RID: 7281 RVA: 0x00073ADC File Offset: 0x00072EDC
		public DrawingAttributes()
		{
			this._extendedProperties = new ExtendedPropertyCollection();
			this.Initialize();
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00073B0C File Offset: 0x00072F0C
		internal DrawingAttributes(ExtendedPropertyCollection extendedProperties)
		{
			this._extendedProperties = extendedProperties;
			this.Initialize();
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00073B38 File Offset: 0x00072F38
		private void Initialize()
		{
			this._extendedProperties.Changed += this.ExtendedPropertiesChanged_EventForwarder;
		}

		/// <summary>Obtém ou define a cor de um <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>A cor de um <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00073B5C File Offset: 0x00072F5C
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00073B94 File Offset: 0x00072F94
		public Color Color
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.Color))
				{
					return Colors.Black;
				}
				return (Color)this.GetExtendedPropertyBackedProperty(KnownIds.Color);
			}
			set
			{
				this.SetExtendedPropertyBackedProperty(KnownIds.Color, value);
			}
		}

		/// <summary>Obtém ou define a forma da caneta usada para desenhar o <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.Ink.StylusShape" />.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00073BB4 File Offset: 0x00072FB4
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x00073BD8 File Offset: 0x00072FD8
		public StylusTip StylusTip
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.StylusTip))
				{
					return StylusTip.Ellipse;
				}
				return StylusTip.Rectangle;
			}
			set
			{
				this.SetExtendedPropertyBackedProperty(KnownIds.StylusTip, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Matrix" /> que especifica a transformação a ser executada na ponta da caneta.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Matrix" /> que especifica a transformação para executar da caneta.</returns>
		/// <exception cref="T:System.ArgumentException">A matriz definida como <see cref="P:System.Windows.Ink.DrawingAttributes.StylusTipTransform" /> não é uma matriz que pode ser invertida.  
		///
		/// ou - 
		/// A propriedade <see cref="P:System.Windows.Media.Matrix.OffsetX" /> ou <see cref="P:System.Windows.Media.Matrix.OffsetY" /> da matriz não é zero.</exception>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00073BF8 File Offset: 0x00072FF8
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00073C30 File Offset: 0x00073030
		public Matrix StylusTipTransform
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.StylusTipTransform))
				{
					return Matrix.Identity;
				}
				return (Matrix)this.GetExtendedPropertyBackedProperty(KnownIds.StylusTipTransform);
			}
			set
			{
				Matrix matrix = value;
				if (matrix.OffsetX != 0.0 || matrix.OffsetY != 0.0)
				{
					throw new ArgumentException(SR.Get("InvalidSttValue"), "value");
				}
				this.SetExtendedPropertyBackedProperty(KnownIds.StylusTipTransform, value);
			}
		}

		/// <summary>Obtém ou define a altura da caneta usada para desenhar o <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>O valor que indica a altura da caneta usada para desenhar o <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Height" /> é menor que <see cref="F:System.Double.Epsilon" /> ou <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00073C8C File Offset: 0x0007308C
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x00073CC4 File Offset: 0x000730C4
		public double Height
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.StylusHeight))
				{
					return DrawingAttributes.DefaultHeight;
				}
				return (double)this.GetExtendedPropertyBackedProperty(KnownIds.StylusHeight);
			}
			set
			{
				if (double.IsNaN(value) || value < DrawingAttributes.MinHeight || value > DrawingAttributes.MaxHeight)
				{
					throw new ArgumentOutOfRangeException("Height", SR.Get("InvalidDrawingAttributesHeight"));
				}
				this.SetExtendedPropertyBackedProperty(KnownIds.StylusHeight, value);
			}
		}

		/// <summary>Obtém ou define a largura da caneta usada para desenhar o <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>A largura da caneta usada para desenhar o <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Width" /> é menor que <see cref="F:System.Double.Epsilon" /> ou <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00073D10 File Offset: 0x00073110
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00073D48 File Offset: 0x00073148
		public double Width
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.StylusWidth))
				{
					return DrawingAttributes.DefaultWidth;
				}
				return (double)this.GetExtendedPropertyBackedProperty(KnownIds.StylusWidth);
			}
			set
			{
				if (double.IsNaN(value) || value < DrawingAttributes.MinWidth || value > DrawingAttributes.MaxWidth)
				{
					throw new ArgumentOutOfRangeException("Width", SR.Get("InvalidDrawingAttributesWidth"));
				}
				this.SetExtendedPropertyBackedProperty(KnownIds.StylusWidth, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se a suavização de Bezier é usada para renderizar o <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>
		///   <see langword="true" /> Para usar para renderizar a suavização de Bezier a <see cref="T:System.Windows.Ink.Stroke" />; caso contrário <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00073D94 File Offset: 0x00073194
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x00073DB8 File Offset: 0x000731B8
		public bool FitToCurve
		{
			get
			{
				DrawingFlags drawingFlags = (DrawingFlags)this.GetExtendedPropertyBackedProperty(KnownIds.DrawingFlags);
				return (drawingFlags & DrawingFlags.FitToCurve) > DrawingFlags.Polyline;
			}
			set
			{
				DrawingFlags drawingFlags = (DrawingFlags)this.GetExtendedPropertyBackedProperty(KnownIds.DrawingFlags);
				if (value)
				{
					drawingFlags |= DrawingFlags.FitToCurve;
				}
				else
				{
					drawingFlags &= ~DrawingFlags.FitToCurve;
				}
				this.SetExtendedPropertyBackedProperty(KnownIds.DrawingFlags, drawingFlags);
			}
		}

		/// <summary>Obtém ou define um valor que indica se a espessura de um <see cref="T:System.Windows.Ink.Stroke" /> renderizado muda de acordo com a quantidade de pressão aplicada.</summary>
		/// <returns>
		///   <see langword="true" /> para indicar que a espessura do traço é uniforme; <see langword="false" /> para indicar que a espessura de um renderizado <see cref="T:System.Windows.Ink.Stroke" /> aumenta quando a pressão é aumentada. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00073DF8 File Offset: 0x000731F8
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x00073E1C File Offset: 0x0007321C
		public bool IgnorePressure
		{
			get
			{
				DrawingFlags drawingFlags = (DrawingFlags)this.GetExtendedPropertyBackedProperty(KnownIds.DrawingFlags);
				return (drawingFlags & DrawingFlags.IgnorePressure) > DrawingFlags.Polyline;
			}
			set
			{
				DrawingFlags drawingFlags = (DrawingFlags)this.GetExtendedPropertyBackedProperty(KnownIds.DrawingFlags);
				if (value)
				{
					drawingFlags |= DrawingFlags.IgnorePressure;
				}
				else
				{
					drawingFlags &= ~DrawingFlags.IgnorePressure;
				}
				this.SetExtendedPropertyBackedProperty(KnownIds.DrawingFlags, drawingFlags);
			}
		}

		/// <summary>Obtém ou define um valor que indica se <see cref="T:System.Windows.Ink.Stroke" /> é semelhante a um marca-texto.</summary>
		/// <returns>
		///   <see langword="true" /> para renderizar a <see cref="T:System.Windows.Ink.Stroke" /> como um marca-texto; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00073E5C File Offset: 0x0007325C
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x00073E80 File Offset: 0x00073280
		public bool IsHighlighter
		{
			get
			{
				return this._extendedProperties.Contains(KnownIds.IsHighlighter);
			}
			set
			{
				this.SetExtendedPropertyBackedProperty(KnownIds.IsHighlighter, value);
				if (value)
				{
					this._v1RasterOperation = DrawingAttributeSerializer.RasterOperationMaskPen;
					return;
				}
				this._v1RasterOperation = DrawingAttributeSerializer.RasterOperationDefaultV1;
			}
		}

		/// <summary>Adiciona uma propriedade personalizada ao objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> a ser associado à propriedade personalizada.</param>
		/// <param name="propertyData">O valor da propriedade personalizada. <paramref name="propertyData" /> deve ser do tipo <see cref="T:System.Char" />, <see cref="T:System.Byte" />, <see cref="T:System.Int16" />, <see cref="T:System.UInt16" />, <see cref="T:System.Int32" />, <see cref="T:System.UInt32" />, <see cref="T:System.Int64" />, <see cref="T:System.UInt64" />, <see cref="T:System.Single" />, <see cref="T:System.Double" />, <see cref="T:System.DateTime" />, <see cref="T:System.Boolean" />, <see cref="T:System.String" />, <see cref="T:System.Decimal" /> ou uma matriz desses tipos de dados, no entanto, ele não pode se ruma matriz do tipo <see cref="T:System.String" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyData" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> é um <see cref="T:System.Guid" /> vazio.  
		///
		/// ou - 
		/// <paramref name="propertyData" /> não é um dos tipos de dados permitidos listados na seção <see langword="Parameters" />.</exception>
		// Token: 0x06001C84 RID: 7300 RVA: 0x00073EB8 File Offset: 0x000732B8
		public void AddPropertyData(Guid propertyDataId, object propertyData)
		{
			DrawingAttributes.ValidateStylusTipTransform(propertyDataId, propertyData);
			this.SetExtendedPropertyBackedProperty(propertyDataId, propertyData);
		}

		/// <summary>Remove a propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> associado à propriedade personalizada a ser removida.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> não está associado a uma propriedade personalizada do objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</exception>
		// Token: 0x06001C85 RID: 7301 RVA: 0x00073ED4 File Offset: 0x000732D4
		public void RemovePropertyData(Guid propertyDataId)
		{
			this.ExtendedProperties.Remove(propertyDataId);
		}

		/// <summary>Obtém o valor da propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> associado à propriedade personalizada a ser obtida.</param>
		/// <returns>O valor da propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> não está associado a uma propriedade personalizada do objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</exception>
		// Token: 0x06001C86 RID: 7302 RVA: 0x00073EF0 File Offset: 0x000732F0
		public object GetPropertyData(Guid propertyDataId)
		{
			return this.GetExtendedPropertyBackedProperty(propertyDataId);
		}

		/// <summary>Retorna os GUIDs de todas as propriedades personalizadas associadas a <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <returns>Uma matriz do tipo <see cref="T:System.Guid" /> que representa os identificadores de dados da propriedade.</returns>
		// Token: 0x06001C87 RID: 7303 RVA: 0x00073F04 File Offset: 0x00073304
		public Guid[] GetPropertyDataIds()
		{
			return this.ExtendedProperties.GetGuidArray();
		}

		/// <summary>Retorna um valor que indica se o identificador de dados da propriedade especificado está no objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> a ser localizado no objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</param>
		/// <returns>
		///   <see langword="true" /> se o identificador de dados da propriedade especificado estiver no objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001C88 RID: 7304 RVA: 0x00073F1C File Offset: 0x0007331C
		public bool ContainsPropertyData(Guid propertyDataId)
		{
			return this.ExtendedProperties.Contains(propertyDataId);
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00073F38 File Offset: 0x00073338
		internal ExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00073F4C File Offset: 0x0007334C
		internal ExtendedPropertyCollection CopyPropertyData()
		{
			return this.ExtendedProperties.Clone();
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x00073F64 File Offset: 0x00073364
		internal StylusShape StylusShape
		{
			get
			{
				StylusShape stylusShape;
				if (this.StylusTip == StylusTip.Rectangle)
				{
					stylusShape = new RectangleStylusShape(this.Width, this.Height);
				}
				else
				{
					stylusShape = new EllipseStylusShape(this.Width, this.Height);
				}
				stylusShape.Transform = this.StylusTipTransform;
				return stylusShape;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00073FAC File Offset: 0x000733AC
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00073FE4 File Offset: 0x000733E4
		internal int FittingError
		{
			get
			{
				if (!this._extendedProperties.Contains(KnownIds.CurveFittingError))
				{
					return 0;
				}
				return (int)this._extendedProperties[KnownIds.CurveFittingError];
			}
			set
			{
				this._extendedProperties[KnownIds.CurveFittingError] = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00074008 File Offset: 0x00073408
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x00074028 File Offset: 0x00073428
		internal DrawingFlags DrawingFlags
		{
			get
			{
				return (DrawingFlags)this.GetExtendedPropertyBackedProperty(KnownIds.DrawingFlags);
			}
			set
			{
				this.SetExtendedPropertyBackedProperty(KnownIds.DrawingFlags, value);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00074048 File Offset: 0x00073448
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0007405C File Offset: 0x0007345C
		internal uint RasterOperation
		{
			get
			{
				return this._v1RasterOperation;
			}
			set
			{
				this._v1RasterOperation = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x00074070 File Offset: 0x00073470
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x00074084 File Offset: 0x00073484
		internal bool HeightChangedForCompatabity
		{
			get
			{
				return this._heightChangedForCompatabity;
			}
			set
			{
				this._heightChangedForCompatabity = value;
			}
		}

		/// <summary>Ocorre quando o valor de qualquer propriedade <see cref="T:System.Windows.Ink.DrawingAttributes" /> foi alterado.</summary>
		// Token: 0x1400017A RID: 378
		// (add) Token: 0x06001C94 RID: 7316 RVA: 0x00074098 File Offset: 0x00073498
		// (remove) Token: 0x06001C95 RID: 7317 RVA: 0x000740BC File Offset: 0x000734BC
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._propertyChanged, value);
			}
			remove
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._propertyChanged, value);
			}
		}

		/// <summary>Serve como uma função de hash para um tipo específico.</summary>
		/// <returns>Um código hash do <see cref="T:System.Object" /> atual.</returns>
		// Token: 0x06001C96 RID: 7318 RVA: 0x000740E0 File Offset: 0x000734E0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determina se o objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificado é igual ao objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> atual.</summary>
		/// <param name="o">O objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> a ser comparado com o objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001C97 RID: 7319 RVA: 0x000740F4 File Offset: 0x000734F4
		public override bool Equals(object o)
		{
			if (o == null || o.GetType() != base.GetType())
			{
				return false;
			}
			DrawingAttributes drawingAttributes = o as DrawingAttributes;
			return !(drawingAttributes == null) && this._extendedProperties == drawingAttributes._extendedProperties;
		}

		/// <summary>Determina se os objetos <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificados são iguais.</summary>
		/// <param name="first">O primeiro objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> a ser comparado.</param>
		/// <param name="second">O segundo objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001C98 RID: 7320 RVA: 0x0007413C File Offset: 0x0007353C
		public static bool operator ==(DrawingAttributes first, DrawingAttributes second)
		{
			return (first == null && second == null) || first == second || (first != null && second != null && first.Equals(second));
		}

		/// <summary>Determina se os objetos <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificados não são iguais.</summary>
		/// <param name="first">O primeiro objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> a ser comparado.</param>
		/// <param name="second">O segundo objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos não forem iguais; do contrário, <see langword="false" />.</returns>
		// Token: 0x06001C99 RID: 7321 RVA: 0x00074164 File Offset: 0x00073564
		public static bool operator !=(DrawingAttributes first, DrawingAttributes second)
		{
			return !(first == second);
		}

		/// <summary>Copia o objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
		/// <returns>Uma cópia do objeto <see cref="T:System.Windows.Ink.DrawingAttributes" />.</returns>
		// Token: 0x06001C9A RID: 7322 RVA: 0x0007417C File Offset: 0x0007357C
		public virtual DrawingAttributes Clone()
		{
			DrawingAttributes drawingAttributes = (DrawingAttributes)base.MemberwiseClone();
			drawingAttributes.AttributeChanged = null;
			drawingAttributes.PropertyDataChanged = null;
			drawingAttributes._extendedProperties = this._extendedProperties.Clone();
			drawingAttributes.Initialize();
			return drawingAttributes;
		}

		/// <summary>Ocorre quando uma propriedade no objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> é alterada.</summary>
		// Token: 0x1400017B RID: 379
		// (add) Token: 0x06001C9B RID: 7323 RVA: 0x000741BC File Offset: 0x000735BC
		// (remove) Token: 0x06001C9C RID: 7324 RVA: 0x000741F4 File Offset: 0x000735F4
		public event PropertyDataChangedEventHandler AttributeChanged;

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.DrawingAttributes.AttributeChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" /> que contém os dados do evento.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> é <see langword="null" />.</exception>
		// Token: 0x06001C9D RID: 7325 RVA: 0x0007422C File Offset: 0x0007362C
		protected virtual void OnAttributeChanged(PropertyDataChangedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			try
			{
				this.PrivateNotifyPropertyChanged(e);
			}
			finally
			{
				if (this.AttributeChanged != null)
				{
					this.AttributeChanged(this, e);
				}
			}
		}

		/// <summary>Ocorre quando os dados da propriedade são adicionados ou removidos da <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		// Token: 0x1400017C RID: 380
		// (add) Token: 0x06001C9E RID: 7326 RVA: 0x00074290 File Offset: 0x00073690
		// (remove) Token: 0x06001C9F RID: 7327 RVA: 0x000742C8 File Offset: 0x000736C8
		public event PropertyDataChangedEventHandler PropertyDataChanged;

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.DrawingAttributes.PropertyDataChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" /> que contém os dados do evento.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> é <see langword="null" />.</exception>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x00074300 File Offset: 0x00073700
		protected virtual void OnPropertyDataChanged(PropertyDataChangedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.PropertyDataChanged != null)
			{
				this.PropertyDataChanged(this, e);
			}
		}

		/// <summary>Ocorre quando qualquer propriedade <see cref="T:System.Windows.Ink.DrawingAttributes" /> é alterada.</summary>
		/// <param name="e">EventArgs</param>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x0007433C File Offset: 0x0007373C
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this._propertyChanged != null)
			{
				this._propertyChanged(this, e);
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00074360 File Offset: 0x00073760
		internal static object GetDefaultDrawingAttributeValue(Guid id)
		{
			if (KnownIds.Color == id)
			{
				return Colors.Black;
			}
			if (KnownIds.StylusWidth == id)
			{
				return DrawingAttributes.DefaultWidth;
			}
			if (KnownIds.StylusTip == id)
			{
				return StylusTip.Ellipse;
			}
			if (KnownIds.DrawingFlags == id)
			{
				return DrawingFlags.AntiAliased;
			}
			if (KnownIds.StylusHeight == id)
			{
				return DrawingAttributes.DefaultHeight;
			}
			if (KnownIds.StylusTipTransform == id)
			{
				return Matrix.Identity;
			}
			if (KnownIds.IsHighlighter == id)
			{
				return false;
			}
			return null;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007440C File Offset: 0x0007380C
		internal static void ValidateStylusTipTransform(Guid propertyDataId, object propertyData)
		{
			if (propertyData == null)
			{
				throw new ArgumentNullException("propertyData");
			}
			if (propertyDataId == KnownIds.StylusTipTransform)
			{
				Type type = propertyData.GetType();
				if (type == typeof(string))
				{
					throw new ArgumentException(SR.Get("InvalidValueType", new object[]
					{
						typeof(Matrix)
					}), "propertyData");
				}
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00074478 File Offset: 0x00073878
		internal static bool RemoveIdFromExtendedProperties(Guid id)
		{
			return KnownIds.Color == id || KnownIds.Transparency == id || KnownIds.StylusWidth == id || KnownIds.DrawingFlags == id || KnownIds.StylusHeight == id || KnownIds.CurveFittingError == id;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000744D8 File Offset: 0x000738D8
		internal static bool GeometricallyEqual(DrawingAttributes left, DrawingAttributes right)
		{
			return left == right || (left.StylusTip == right.StylusTip && left.StylusTipTransform == right.StylusTipTransform && DoubleUtil.AreClose(left.Width, right.Width) && DoubleUtil.AreClose(left.Height, right.Height) && left.DrawingFlags == right.DrawingFlags);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00074544 File Offset: 0x00073944
		internal static bool IsGeometricalDaGuid(Guid guid)
		{
			return guid == KnownIds.StylusHeight || guid == KnownIds.StylusWidth || guid == KnownIds.StylusTipTransform || guid == KnownIds.StylusTip || guid == KnownIds.DrawingFlags;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00074598 File Offset: 0x00073998
		private void ExtendedPropertiesChanged_EventForwarder(object sender, ExtendedPropertiesChangedEventArgs args)
		{
			if (!(args.NewProperty == null))
			{
				if (args.OldProperty == null)
				{
					object defaultDrawingAttributeValue = DrawingAttributes.GetDefaultDrawingAttributeValue(args.NewProperty.Id);
					if (defaultDrawingAttributeValue == null)
					{
						PropertyDataChangedEventArgs e = new PropertyDataChangedEventArgs(args.NewProperty.Id, args.NewProperty.Value, null);
						this.OnPropertyDataChanged(e);
						return;
					}
					if (!defaultDrawingAttributeValue.Equals(args.NewProperty.Value))
					{
						PropertyDataChangedEventArgs e2 = new PropertyDataChangedEventArgs(args.NewProperty.Id, args.NewProperty.Value, defaultDrawingAttributeValue);
						this.OnAttributeChanged(e2);
						return;
					}
				}
				else
				{
					object defaultDrawingAttributeValue2 = DrawingAttributes.GetDefaultDrawingAttributeValue(args.NewProperty.Id);
					if (defaultDrawingAttributeValue2 != null)
					{
						if (!args.NewProperty.Value.Equals(args.OldProperty.Value))
						{
							PropertyDataChangedEventArgs e3 = new PropertyDataChangedEventArgs(args.NewProperty.Id, args.NewProperty.Value, args.OldProperty.Value);
							this.OnAttributeChanged(e3);
							return;
						}
					}
					else if (!args.NewProperty.Value.Equals(args.OldProperty.Value))
					{
						PropertyDataChangedEventArgs e4 = new PropertyDataChangedEventArgs(args.NewProperty.Id, args.NewProperty.Value, args.OldProperty.Value);
						this.OnPropertyDataChanged(e4);
					}
				}
				return;
			}
			object defaultDrawingAttributeValue3 = DrawingAttributes.GetDefaultDrawingAttributeValue(args.OldProperty.Id);
			if (defaultDrawingAttributeValue3 != null)
			{
				ExtendedProperty extendedProperty = new ExtendedProperty(args.OldProperty.Id, defaultDrawingAttributeValue3);
				PropertyDataChangedEventArgs e5 = new PropertyDataChangedEventArgs(args.OldProperty.Id, extendedProperty.Value, args.OldProperty.Value);
				this.OnAttributeChanged(e5);
				return;
			}
			PropertyDataChangedEventArgs e6 = new PropertyDataChangedEventArgs(args.OldProperty.Id, null, args.OldProperty.Value);
			this.OnPropertyDataChanged(e6);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007476C File Offset: 0x00073B6C
		private void SetExtendedPropertyBackedProperty(Guid id, object value)
		{
			if (this._extendedProperties.Contains(id))
			{
				object defaultDrawingAttributeValue = DrawingAttributes.GetDefaultDrawingAttributeValue(id);
				if (defaultDrawingAttributeValue != null && defaultDrawingAttributeValue.Equals(value))
				{
					this._extendedProperties.Remove(id);
					return;
				}
				object extendedPropertyBackedProperty = this.GetExtendedPropertyBackedProperty(id);
				if (!extendedPropertyBackedProperty.Equals(value))
				{
					this._extendedProperties[id] = value;
					return;
				}
			}
			else
			{
				object defaultDrawingAttributeValue2 = DrawingAttributes.GetDefaultDrawingAttributeValue(id);
				if (defaultDrawingAttributeValue2 == null || !defaultDrawingAttributeValue2.Equals(value))
				{
					this._extendedProperties[id] = value;
				}
			}
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000747E8 File Offset: 0x00073BE8
		private object GetExtendedPropertyBackedProperty(Guid id)
		{
			if (this._extendedProperties.Contains(id))
			{
				return this._extendedProperties[id];
			}
			if (DrawingAttributes.GetDefaultDrawingAttributeValue(id) != null)
			{
				return DrawingAttributes.GetDefaultDrawingAttributeValue(id);
			}
			throw new ArgumentException(SR.Get("EPGuidNotFound"), "id");
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00074834 File Offset: 0x00073C34
		private void PrivateNotifyPropertyChanged(PropertyDataChangedEventArgs e)
		{
			if (e.PropertyGuid == KnownIds.Color)
			{
				this.OnPropertyChanged("Color");
				return;
			}
			if (e.PropertyGuid == KnownIds.StylusTip)
			{
				this.OnPropertyChanged("StylusTip");
				return;
			}
			if (e.PropertyGuid == KnownIds.StylusTipTransform)
			{
				this.OnPropertyChanged("StylusTipTransform");
				return;
			}
			if (e.PropertyGuid == KnownIds.StylusHeight)
			{
				this.OnPropertyChanged("Height");
				return;
			}
			if (e.PropertyGuid == KnownIds.StylusWidth)
			{
				this.OnPropertyChanged("Width");
				return;
			}
			if (e.PropertyGuid == KnownIds.IsHighlighter)
			{
				this.OnPropertyChanged("IsHighlighter");
				return;
			}
			if (e.PropertyGuid == KnownIds.DrawingFlags)
			{
				DrawingFlags drawingFlags = (DrawingFlags)e.PreviousValue ^ (DrawingFlags)e.NewValue;
				if ((drawingFlags & DrawingFlags.FitToCurve) != DrawingFlags.Polyline)
				{
					this.OnPropertyChanged("FitToCurve");
				}
				if ((drawingFlags & DrawingFlags.IgnorePressure) != DrawingFlags.Polyline)
				{
					this.OnPropertyChanged("IgnorePressure");
				}
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00074940 File Offset: 0x00073D40
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x04000F89 RID: 3977
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x04000F8A RID: 3978
		private ExtendedPropertyCollection _extendedProperties;

		// Token: 0x04000F8B RID: 3979
		private uint _v1RasterOperation = DrawingAttributeSerializer.RasterOperationDefaultV1;

		// Token: 0x04000F8C RID: 3980
		private bool _heightChangedForCompatabity;

		// Token: 0x04000F8D RID: 3981
		internal static readonly float StylusPrecision = 1000f;

		// Token: 0x04000F8E RID: 3982
		internal static readonly double DefaultWidth = 2.0031496062992127;

		// Token: 0x04000F8F RID: 3983
		internal static readonly double DefaultHeight = 2.0031496062992127;

		/// <summary>Especifica o menor valor permitido para a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Height" />.</summary>
		// Token: 0x04000F90 RID: 3984
		public static readonly double MinHeight = 3.77952755905512E-05;

		/// <summary>Especifica o menor valor permitido para a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Width" />.</summary>
		// Token: 0x04000F91 RID: 3985
		public static readonly double MinWidth = 3.77952755905512E-05;

		/// <summary>Especifica o maior valor permitido para a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Height" />.</summary>
		// Token: 0x04000F92 RID: 3986
		public static readonly double MaxHeight = 162329.461417323;

		/// <summary>Especifica o maior valor permitido para a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Width" />.</summary>
		// Token: 0x04000F93 RID: 3987
		public static readonly double MaxWidth = 162329.461417323;
	}
}
