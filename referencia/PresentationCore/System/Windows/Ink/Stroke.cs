using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.Ink;
using MS.Internal.Ink.InkSerializedFormat;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Representa um único traço de tinta.</summary>
	// Token: 0x0200035A RID: 858
	public class Stroke : INotifyPropertyChanged
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <param name="stylusPoints">Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que representa o <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPoints" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stylusPoints" /> está vazio.</exception>
		// Token: 0x06001CDD RID: 7389 RVA: 0x0007562C File Offset: 0x00074A2C
		public Stroke(StylusPointCollection stylusPoints) : this(stylusPoints, new DrawingAttributes(), null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <param name="stylusPoints">Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que representa o <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <param name="drawingAttributes">Um objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> que especifica a aparência do <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPoints" /> é <see langword="null" />.  
		///
		/// ou - 
		/// <paramref name="drawingAtrributes" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stylusPoints" /> está vazio.</exception>
		// Token: 0x06001CDE RID: 7390 RVA: 0x00075648 File Offset: 0x00074A48
		public Stroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes) : this(stylusPoints, drawingAttributes, null)
		{
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00075660 File Offset: 0x00074A60
		internal Stroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes, ExtendedPropertyCollection extendedProperties)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (stylusPoints.Count == 0)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointCollectionZeroCount"), "stylusPoints");
			}
			if (drawingAttributes == null)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			this._drawingAttributes = drawingAttributes;
			this._stylusPoints = stylusPoints;
			this._extendedProperties = extendedProperties;
			this.Initialize();
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000756F0 File Offset: 0x00074AF0
		private void Initialize()
		{
			this._drawingAttributes.AttributeChanged += this.DrawingAttributes_Changed;
			this._stylusPoints.Changed += this.StylusPoints_Changed;
			this._stylusPoints.CountGoingToZero += this.StylusPoints_CountGoingToZero;
		}

		/// <summary>Retorna uma cópia profunda do objeto <see cref="T:System.Windows.Ink.Stroke" /> existente.</summary>
		/// <returns>O novo objeto <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x00075744 File Offset: 0x00074B44
		public virtual Stroke Clone()
		{
			Stroke stroke = (Stroke)base.MemberwiseClone();
			stroke.DrawingAttributesChanged = null;
			stroke.DrawingAttributesReplaced = null;
			stroke.StylusPointsReplaced = null;
			stroke.StylusPointsChanged = null;
			stroke.PropertyDataChanged = null;
			stroke.Invalidated = null;
			stroke._propertyChanged = null;
			if (this._cloneStylusPoints)
			{
				stroke._stylusPoints = this._stylusPoints.Clone();
			}
			stroke._drawingAttributes = this._drawingAttributes.Clone();
			if (this._extendedProperties != null)
			{
				stroke._extendedProperties = this._extendedProperties.Clone();
			}
			stroke.Initialize();
			stroke._cloneStylusPoints = true;
			return stroke;
		}

		/// <summary>Executa uma transformação com base no objeto <see cref="T:System.Windows.Media.Matrix" /> especificado.</summary>
		/// <param name="transformMatrix">O objeto <see cref="T:System.Windows.Media.Matrix" /> que define a transformação.</param>
		/// <param name="applyToStylusTip">
		///   <see langword="true" /> para aplicar a transformação à ponta da caneta; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x000757E8 File Offset: 0x00074BE8
		public virtual void Transform(Matrix transformMatrix, bool applyToStylusTip)
		{
			if (transformMatrix.IsIdentity)
			{
				return;
			}
			if (!transformMatrix.HasInverse)
			{
				throw new ArgumentException(SR.Get("MatrixNotInvertible"), "transformMatrix");
			}
			if (MatrixHelper.ContainsNaN(transformMatrix))
			{
				throw new ArgumentException(SR.Get("InvalidMatrixContainsNaN"), "transformMatrix");
			}
			if (MatrixHelper.ContainsInfinity(transformMatrix))
			{
				throw new ArgumentException(SR.Get("InvalidMatrixContainsInfinity"), "transformMatrix");
			}
			this._cachedGeometry = null;
			this._cachedBounds = Rect.Empty;
			if (applyToStylusTip)
			{
				this._delayRaiseInvalidated = true;
			}
			try
			{
				this._stylusPoints.Transform(new MatrixTransform(transformMatrix));
				if (applyToStylusTip)
				{
					Matrix matrix = this._drawingAttributes.StylusTipTransform;
					transformMatrix.OffsetX = 0.0;
					transformMatrix.OffsetY = 0.0;
					matrix *= transformMatrix;
					if (matrix.HasInverse)
					{
						this._drawingAttributes.StylusTipTransform = matrix;
					}
				}
				if (this._delayRaiseInvalidated)
				{
					this.OnInvalidated(EventArgs.Empty);
				}
			}
			finally
			{
				this._delayRaiseInvalidated = false;
			}
		}

		/// <summary>Retorna os pontos de caneta que o <see cref="T:System.Windows.Ink.Stroke" /> usa quando <see cref="P:System.Windows.Ink.DrawingAttributes.FitToCurve" /> é <see langword="true" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém os pontos de caneta pela lombada de um <see cref="T:System.Windows.Ink.Stroke" /> quando <see cref="P:System.Windows.Ink.DrawingAttributes.FitToCurve" /> é <see langword="true" /></returns>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x0007590C File Offset: 0x00074D0C
		public StylusPointCollection GetBezierStylusPoints()
		{
			if (this._stylusPoints.Count < 2)
			{
				return this._stylusPoints;
			}
			Bezier bezier = new Bezier();
			if (!bezier.ConstructBezierState(this._stylusPoints, (double)this.DrawingAttributes.FittingError))
			{
				return this._stylusPoints.Clone();
			}
			double num = 0.5;
			StylusShape stylusShape = this.DrawingAttributes.StylusShape;
			if (stylusShape != null)
			{
				Rect boundingBox = stylusShape.BoundingBox;
				double num2 = Math.Min(boundingBox.Width, boundingBox.Height);
				num = Math.Log10(num2 + num2);
				num *= StrokeCollectionSerializer.AvalonToHimetricMultiplier / 2.0;
				if (num < 0.5)
				{
					num = 0.5;
				}
			}
			List<Point> bezierPoints = bezier.Flatten(num);
			return this.GetInterpolatedStylusPoints(bezierPoints);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000759D4 File Offset: 0x00074DD4
		private StylusPointCollection GetInterpolatedStylusPoints(List<Point> bezierPoints)
		{
			StylusPointCollection stylusPointCollection = new StylusPointCollection(this._stylusPoints.Description, bezierPoints.Count);
			this.AddInterpolatedBezierPoint(stylusPointCollection, bezierPoints[0], this._stylusPoints[0].GetAdditionalData(), this._stylusPoints[0].PressureFactor);
			if (bezierPoints.Count == 1)
			{
				return stylusPointCollection;
			}
			double num = 0.0;
			double num2 = 0.0;
			double num3 = this.GetDistanceBetweenPoints((Point)this._stylusPoints[0], (Point)this._stylusPoints[1]);
			int num4 = 1;
			int i = this._stylusPoints.Count;
			for (int j = 1; j < bezierPoints.Count - 1; j++)
			{
				num += this.GetDistanceBetweenPoints(bezierPoints[j - 1], bezierPoints[j]);
				while (i > num4)
				{
					if (num >= num2 && num < num3)
					{
						StylusPoint stylusPoint = this._stylusPoints[num4 - 1];
						float num5 = ((float)num - (float)num2) / ((float)num3 - (float)num2);
						float pressureFactor = stylusPoint.PressureFactor;
						float num6 = this._stylusPoints[num4].PressureFactor - pressureFactor;
						float pressure = num5 * num6 + pressureFactor;
						this.AddInterpolatedBezierPoint(stylusPointCollection, bezierPoints[j], stylusPoint.GetAdditionalData(), pressure);
						break;
					}
					num4++;
					if (i > num4)
					{
						num2 = num3;
						num3 += this.GetDistanceBetweenPoints((Point)this._stylusPoints[num4 - 1], (Point)this._stylusPoints[num4]);
					}
				}
			}
			this.AddInterpolatedBezierPoint(stylusPointCollection, bezierPoints[bezierPoints.Count - 1], this._stylusPoints[i - 1].GetAdditionalData(), this._stylusPoints[i - 1].PressureFactor);
			return stylusPointCollection;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00075BBC File Offset: 0x00074FBC
		private double GetDistanceBetweenPoints(Point p1, Point p2)
		{
			return Math.Sqrt((p2 - p1).LengthSquared);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00075BE0 File Offset: 0x00074FE0
		private void AddInterpolatedBezierPoint(StylusPointCollection bezierStylusPoints, Point bezierPoint, int[] additionalData, float pressure)
		{
			double x = (bezierPoint.X > StylusPoint.MaxXY) ? StylusPoint.MaxXY : ((bezierPoint.X < StylusPoint.MinXY) ? StylusPoint.MinXY : bezierPoint.X);
			double y = (bezierPoint.Y > StylusPoint.MaxXY) ? StylusPoint.MaxXY : ((bezierPoint.Y < StylusPoint.MinXY) ? StylusPoint.MinXY : bezierPoint.Y);
			StylusPoint item = new StylusPoint(x, y, pressure, bezierStylusPoints.Description, additionalData, false, false);
			bezierStylusPoints.Add(item);
		}

		/// <summary>Adiciona uma propriedade personalizada ao objeto <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <param name="propertyDataId">O identificador exclusivo para a propriedade.</param>
		/// <param name="propertyData">O valor da propriedade personalizada. <paramref name="propertyData" /> deve ser do tipo <see cref="T:System.Char" />, <see cref="T:System.Byte" />, <see cref="T:System.Int16" />, <see cref="T:System.UInt16" />, <see cref="T:System.Int32" />, <see cref="T:System.UInt32" />, <see cref="T:System.Int64" />, <see cref="T:System.UInt64" />, <see cref="T:System.Single" />, <see cref="T:System.Double" />, <see cref="T:System.DateTime" />, <see cref="T:System.Boolean" />, <see cref="T:System.String" />, <see cref="T:System.Decimal" /> ou uma matriz desses tipos de dados, exceto <see cref="T:System.String" />, que não é permitido.</param>
		/// <exception cref="T:System.ArgumentException">O argumento <paramref name="propertyData" /> não é um dos tipos de dados permitidos listados na seção <see langword="Parameters" />.</exception>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x00075C6C File Offset: 0x0007506C
		public void AddPropertyData(Guid propertyDataId, object propertyData)
		{
			DrawingAttributes.ValidateStylusTipTransform(propertyDataId, propertyData);
			object previousValue = null;
			if (this.ContainsPropertyData(propertyDataId))
			{
				previousValue = this.GetPropertyData(propertyDataId);
				this.ExtendedProperties[propertyDataId] = propertyData;
			}
			else
			{
				this.ExtendedProperties.Add(propertyDataId, propertyData);
			}
			this.OnPropertyDataChanged(new PropertyDataChangedEventArgs(propertyDataId, propertyData, previousValue));
		}

		/// <summary>Exclui uma propriedade personalizada do objeto <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <param name="propertyDataId">O identificador exclusivo para a propriedade.</param>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x00075CC0 File Offset: 0x000750C0
		public void RemovePropertyData(Guid propertyDataId)
		{
			object propertyData = this.GetPropertyData(propertyDataId);
			this.ExtendedProperties.Remove(propertyDataId);
			this.OnPropertyDataChanged(new PropertyDataChangedEventArgs(propertyDataId, null, propertyData));
		}

		/// <summary>Recupera os dados de propriedade do GUID especificado.</summary>
		/// <param name="propertyDataId">O identificador exclusivo para a propriedade.</param>
		/// <returns>Um <see langword="object" /> que contém os dados de propriedade.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> não está associado a uma propriedade personalizada do <see cref="T:System.Windows.Ink.Stroke" />.</exception>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x00075CF0 File Offset: 0x000750F0
		public object GetPropertyData(Guid propertyDataId)
		{
			return this.ExtendedProperties[propertyDataId];
		}

		/// <summary>Recupera os GUIDs de todas as propriedades personalizadas associadas ao objeto <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>Uma matriz de objetos de <see cref="T:System.Guid" />.</returns>
		// Token: 0x06001CEA RID: 7402 RVA: 0x00075D0C File Offset: 0x0007510C
		public Guid[] GetPropertyDataIds()
		{
			return this.ExtendedProperties.GetGuidArray();
		}

		/// <summary>Retorna um valor que indica se o objeto <see cref="T:System.Windows.Ink.Stroke" /> contém a propriedade personalizada especificada.</summary>
		/// <param name="propertyDataId">O identificador exclusivo para a propriedade.</param>
		/// <returns>Retorna <see langword="true" /> se a propriedade personalizada existir, caso contrário, retorna <see langword="false" />.</returns>
		// Token: 0x06001CEB RID: 7403 RVA: 0x00075D24 File Offset: 0x00075124
		public bool ContainsPropertyData(Guid propertyDataId)
		{
			return this.ExtendedProperties.Contains(propertyDataId);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Ink.DrawingAttributes" /> para o objeto <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <exception cref="T:System.ArgumentNullException">O valor definido é <see langword="null" />.</exception>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00075D40 File Offset: 0x00075140
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00075D54 File Offset: 0x00075154
		public DrawingAttributes DrawingAttributes
		{
			get
			{
				return this._drawingAttributes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._drawingAttributes.AttributeChanged -= this.DrawingAttributes_Changed;
				DrawingAttributesReplacedEventArgs e = new DrawingAttributesReplacedEventArgs(value, this._drawingAttributes);
				DrawingAttributes drawingAttributes = this._drawingAttributes;
				this._drawingAttributes = value;
				if (!DrawingAttributes.GeometricallyEqual(drawingAttributes, this._drawingAttributes))
				{
					this._cachedGeometry = null;
					this._cachedBounds = Rect.Empty;
				}
				this._drawingAttributes.AttributeChanged += this.DrawingAttributes_Changed;
				this.OnDrawingAttributesReplaced(e);
				this.OnInvalidated(EventArgs.Empty);
				this.OnPropertyChanged("DrawingAttributes");
			}
		}

		/// <summary>Retorna os pontos da caneta do <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém os pontos de caneta que representam atual <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00075DFC File Offset: 0x000751FC
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x00075E10 File Offset: 0x00075210
		public StylusPointCollection StylusPoints
		{
			get
			{
				return this._stylusPoints;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Count == 0)
				{
					throw new ArgumentException(SR.Get("InvalidStylusPointCollectionZeroCount"));
				}
				this._cachedGeometry = null;
				this._cachedBounds = Rect.Empty;
				StylusPointsReplacedEventArgs e = new StylusPointsReplacedEventArgs(value, this._stylusPoints);
				this._stylusPoints.Changed -= this.StylusPoints_Changed;
				this._stylusPoints.CountGoingToZero -= this.StylusPoints_CountGoingToZero;
				this._stylusPoints = value;
				this._stylusPoints.Changed += this.StylusPoints_Changed;
				this._stylusPoints.CountGoingToZero += this.StylusPoints_CountGoingToZero;
				this.OnStylusPointsReplaced(e);
				this.OnInvalidated(EventArgs.Empty);
				this.OnPropertyChanged("StylusPoints");
			}
		}

		/// <summary>Ocorre quando o <see cref="P:System.Windows.Ink.Stroke.DrawingAttributes" /> associado ao objeto <see cref="T:System.Windows.Ink.Stroke" /> muda.</summary>
		// Token: 0x1400017F RID: 383
		// (add) Token: 0x06001CF0 RID: 7408 RVA: 0x00075EE4 File Offset: 0x000752E4
		// (remove) Token: 0x06001CF1 RID: 7409 RVA: 0x00075F1C File Offset: 0x0007531C
		public event PropertyDataChangedEventHandler DrawingAttributesChanged;

		/// <summary>Ocorre quando os atributos de desenho de um objeto <see cref="T:System.Windows.Ink.Stroke" /> são substituídos.</summary>
		// Token: 0x14000180 RID: 384
		// (add) Token: 0x06001CF2 RID: 7410 RVA: 0x00075F54 File Offset: 0x00075354
		// (remove) Token: 0x06001CF3 RID: 7411 RVA: 0x00075F8C File Offset: 0x0007538C
		public event DrawingAttributesReplacedEventHandler DrawingAttributesReplaced;

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Ink.Stroke.StylusPoints" /> recebe uma nova <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		// Token: 0x14000181 RID: 385
		// (add) Token: 0x06001CF4 RID: 7412 RVA: 0x00075FC4 File Offset: 0x000753C4
		// (remove) Token: 0x06001CF5 RID: 7413 RVA: 0x00075FFC File Offset: 0x000753FC
		public event StylusPointsReplacedEventHandler StylusPointsReplaced;

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Ink.Stroke.StylusPoints" /> muda.</summary>
		// Token: 0x14000182 RID: 386
		// (add) Token: 0x06001CF6 RID: 7414 RVA: 0x00076034 File Offset: 0x00075434
		// (remove) Token: 0x06001CF7 RID: 7415 RVA: 0x0007606C File Offset: 0x0007546C
		public event EventHandler StylusPointsChanged;

		/// <summary>Ocorre quando as propriedades personalizadas em um objeto <see cref="T:System.Windows.Ink.Stroke" /> são alteradas.</summary>
		// Token: 0x14000183 RID: 387
		// (add) Token: 0x06001CF8 RID: 7416 RVA: 0x000760A4 File Offset: 0x000754A4
		// (remove) Token: 0x06001CF9 RID: 7417 RVA: 0x000760DC File Offset: 0x000754DC
		public event PropertyDataChangedEventHandler PropertyDataChanged;

		/// <summary>Ocorre quando a aparência do <see cref="T:System.Windows.Ink.Stroke" /> é alterada.</summary>
		// Token: 0x14000184 RID: 388
		// (add) Token: 0x06001CFA RID: 7418 RVA: 0x00076114 File Offset: 0x00075514
		// (remove) Token: 0x06001CFB RID: 7419 RVA: 0x0007614C File Offset: 0x0007554C
		public event EventHandler Invalidated;

		/// <summary>Ocorre quando o valor de qualquer propriedade <see cref="T:System.Windows.Ink.Stroke" /> foi alterado.</summary>
		// Token: 0x14000185 RID: 389
		// (add) Token: 0x06001CFC RID: 7420 RVA: 0x00076184 File Offset: 0x00075584
		// (remove) Token: 0x06001CFD RID: 7421 RVA: 0x000761A8 File Offset: 0x000755A8
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

		/// <summary>Permite que classes derivadas modifiquem o comportamento padrão do evento <see cref="E:System.Windows.Ink.Stroke.DrawingAttributesChanged" />.</summary>
		/// <param name="e">O objeto <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001CFE RID: 7422 RVA: 0x000761CC File Offset: 0x000755CC
		protected virtual void OnDrawingAttributesChanged(PropertyDataChangedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.DrawingAttributesChanged != null)
			{
				this.DrawingAttributesChanged(this, e);
			}
		}

		/// <summary>Permite que classes derivadas modifiquem o comportamento padrão do evento <see cref="E:System.Windows.Ink.Stroke.DrawingAttributesReplaced" />.</summary>
		/// <param name="e">O objeto <see cref="T:System.Windows.Ink.DrawingAttributesReplacedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001CFF RID: 7423 RVA: 0x00076208 File Offset: 0x00075608
		protected virtual void OnDrawingAttributesReplaced(DrawingAttributesReplacedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (this.DrawingAttributesReplaced != null)
			{
				this.DrawingAttributesReplaced(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.Stroke.StylusPointsReplaced" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Ink.StylusPointsReplacedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D00 RID: 7424 RVA: 0x00076238 File Offset: 0x00075638
		protected virtual void OnStylusPointsReplaced(StylusPointsReplacedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.StylusPointsReplaced != null)
			{
				this.StylusPointsReplaced(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.Stroke.StylusPointsChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.EventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D01 RID: 7425 RVA: 0x00076274 File Offset: 0x00075674
		protected virtual void OnStylusPointsChanged(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.StylusPointsChanged != null)
			{
				this.StylusPointsChanged(this, e);
			}
		}

		/// <summary>Permite que classes derivadas modifiquem o comportamento padrão do evento <see cref="E:System.Windows.Ink.Stroke.PropertyDataChanged" />.</summary>
		/// <param name="e">O objeto <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D02 RID: 7426 RVA: 0x000762B0 File Offset: 0x000756B0
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

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.Stroke.Invalidated" />.</summary>
		/// <param name="e">Um <see cref="T:System.EventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D03 RID: 7427 RVA: 0x000762EC File Offset: 0x000756EC
		protected virtual void OnInvalidated(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.Invalidated != null)
			{
				this.Invalidated(this, e);
			}
		}

		/// <summary>Ocorre quando qualquer propriedade <see cref="T:System.Windows.Ink.Stroke" /> é alterada.</summary>
		/// <param name="e">Os dados de evento que descrevem a propriedade alterada, bem como valores novos e antigos.</param>
		// Token: 0x06001D04 RID: 7428 RVA: 0x00076328 File Offset: 0x00075728
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this._propertyChanged != null)
			{
				this._propertyChanged(this, e);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0007634C File Offset: 0x0007574C
		internal ExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				if (this._extendedProperties == null)
				{
					this._extendedProperties = new ExtendedPropertyCollection();
				}
				return this._extendedProperties;
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00076378 File Offset: 0x00075778
		private StrokeCollection Clip(StrokeFIndices[] cutAt)
		{
			StrokeCollection strokeCollection = new StrokeCollection();
			if (cutAt.Length == 0)
			{
				return strokeCollection;
			}
			if (cutAt.Length == 1 && cutAt[0].IsFull)
			{
				strokeCollection.Add(this.Clone());
				return strokeCollection;
			}
			StylusPointCollection sourceStylusPoints = this.StylusPoints;
			if (this.DrawingAttributes.FitToCurve)
			{
				sourceStylusPoints = this.GetBezierStylusPoints();
			}
			foreach (StrokeFIndices strokeFIndices in cutAt)
			{
				if (!DoubleUtil.GreaterThanOrClose(strokeFIndices.BeginFIndex, strokeFIndices.EndFIndex))
				{
					Stroke item = this.Copy(sourceStylusPoints, strokeFIndices.BeginFIndex, strokeFIndices.EndFIndex);
					strokeCollection.Add(item);
				}
			}
			return strokeCollection;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00076418 File Offset: 0x00075818
		private StrokeCollection Erase(StrokeFIndices[] cutAt)
		{
			StrokeCollection strokeCollection = new StrokeCollection();
			if (cutAt.Length == 0 || (cutAt.Length == 1 && cutAt[0].IsFull))
			{
				return strokeCollection;
			}
			StylusPointCollection sourceStylusPoints = this.StylusPoints;
			if (this.DrawingAttributes.FitToCurve)
			{
				sourceStylusPoints = this.GetBezierStylusPoints();
			}
			int i = 0;
			double num = StrokeFIndices.BeforeFirst;
			if (cutAt[0].BeginFIndex == StrokeFIndices.BeforeFirst)
			{
				num = cutAt[0].EndFIndex;
				i++;
			}
			while (i < cutAt.Length)
			{
				StrokeFIndices strokeFIndices = cutAt[i];
				if (!DoubleUtil.GreaterThanOrClose(num, strokeFIndices.BeginFIndex))
				{
					Stroke item = this.Copy(sourceStylusPoints, num, strokeFIndices.BeginFIndex);
					strokeCollection.Add(item);
					num = strokeFIndices.EndFIndex;
				}
				i++;
			}
			if (num != StrokeFIndices.AfterLast)
			{
				Stroke item2 = this.Copy(sourceStylusPoints, num, StrokeFIndices.AfterLast);
				strokeCollection.Add(item2);
			}
			return strokeCollection;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000764F4 File Offset: 0x000758F4
		private Stroke Copy(StylusPointCollection sourceStylusPoints, double beginFIndex, double endFIndex)
		{
			int num = DoubleUtil.AreClose(StrokeFIndices.BeforeFirst, beginFIndex) ? 0 : ((int)Math.Floor(beginFIndex));
			int num2 = DoubleUtil.AreClose(StrokeFIndices.AfterLast, endFIndex) ? (sourceStylusPoints.Count - 1) : ((int)Math.Ceiling(endFIndex));
			int num3 = num2 - num + 1;
			StylusPointCollection stylusPointCollection = new StylusPointCollection(this.StylusPoints.Description, num3);
			for (int i = 0; i < num3; i++)
			{
				StylusPoint item = sourceStylusPoints[i + num];
				stylusPointCollection.Add(item);
			}
			if (!DoubleUtil.AreClose(beginFIndex, StrokeFIndices.BeforeFirst))
			{
				beginFIndex -= (double)num;
			}
			if (!DoubleUtil.AreClose(endFIndex, StrokeFIndices.AfterLast))
			{
				endFIndex -= (double)num;
			}
			if (stylusPointCollection.Count > 1)
			{
				Point point = (Point)stylusPointCollection[0];
				Point point2 = (Point)stylusPointCollection[stylusPointCollection.Count - 1];
				if (!DoubleUtil.AreClose(endFIndex, StrokeFIndices.AfterLast) && !DoubleUtil.AreClose((double)num2, endFIndex))
				{
					double num4 = Math.Ceiling(endFIndex);
					double findex = num4 - endFIndex;
					point2 = this.GetIntermediatePoint(stylusPointCollection[stylusPointCollection.Count - 1], stylusPointCollection[stylusPointCollection.Count - 2], findex);
				}
				if (!DoubleUtil.AreClose(beginFIndex, StrokeFIndices.BeforeFirst) && !DoubleUtil.AreClose((double)num, beginFIndex))
				{
					point = this.GetIntermediatePoint(stylusPointCollection[0], stylusPointCollection[1], beginFIndex);
				}
				StylusPoint value = stylusPointCollection[stylusPointCollection.Count - 1];
				value.X = point2.X;
				value.Y = point2.Y;
				stylusPointCollection[stylusPointCollection.Count - 1] = value;
				StylusPoint value2 = stylusPointCollection[0];
				value2.X = point.X;
				value2.Y = point.Y;
				stylusPointCollection[0] = value2;
			}
			Stroke stroke = null;
			try
			{
				this._cloneStylusPoints = false;
				stroke = this.Clone();
				if (stroke.DrawingAttributes.FitToCurve)
				{
					stroke.DrawingAttributes.FitToCurve = false;
				}
				stroke.StylusPoints = stylusPointCollection;
			}
			finally
			{
				this._cloneStylusPoints = true;
			}
			return stroke;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00076704 File Offset: 0x00075B04
		private Point GetIntermediatePoint(StylusPoint p1, StylusPoint p2, double findex)
		{
			double num = p2.X - p1.X;
			double num2 = p2.Y - p1.Y;
			double num3 = num * findex;
			double num4 = num2 * findex;
			return new Point(p1.X + num3, p1.Y + num4);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00076750 File Offset: 0x00075B50
		private void DrawingAttributes_Changed(object sender, PropertyDataChangedEventArgs e)
		{
			if (DrawingAttributes.IsGeometricalDaGuid(e.PropertyGuid))
			{
				this._cachedGeometry = null;
				this._cachedBounds = Rect.Empty;
			}
			this.OnDrawingAttributesChanged(e);
			if (!this._delayRaiseInvalidated)
			{
				this.OnInvalidated(EventArgs.Empty);
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00076798 File Offset: 0x00075B98
		private void StylusPoints_Changed(object sender, EventArgs e)
		{
			this._cachedGeometry = null;
			this._cachedBounds = Rect.Empty;
			this.OnStylusPointsChanged(EventArgs.Empty);
			if (!this._delayRaiseInvalidated)
			{
				this.OnInvalidated(EventArgs.Empty);
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000767D8 File Offset: 0x00075BD8
		private void StylusPoints_CountGoingToZero(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000767EC File Offset: 0x00075BEC
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>Recupera a caixa delimitadora do objeto <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>Uma estrutura <see cref="T:System.Windows.Rect" /> que define a caixa delimitadora para um objeto <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x06001D0E RID: 7438 RVA: 0x00076808 File Offset: 0x00075C08
		public virtual Rect GetBounds()
		{
			if (this._cachedBounds.IsEmpty)
			{
				StrokeNodeIterator iterator = StrokeNodeIterator.GetIterator(this, this.DrawingAttributes);
				for (int i = 0; i < iterator.Count; i++)
				{
					this._cachedBounds.Union(iterator[i].GetBounds());
				}
			}
			return this._cachedBounds;
		}

		/// <summary>Renderiza o objeto <see cref="T:System.Windows.Ink.Stroke" /> com base no <see cref="T:System.Windows.Media.DrawingContext" /> especificado.</summary>
		/// <param name="context">O objeto <see cref="T:System.Windows.Media.DrawingContext" /> no qual o traço será renderizado.</param>
		// Token: 0x06001D0F RID: 7439 RVA: 0x00076860 File Offset: 0x00075C60
		public void Draw(DrawingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Draw(context, this.DrawingAttributes);
		}

		/// <summary>Renderiza o objeto <see cref="T:System.Windows.Ink.Stroke" /> com base no <see cref="T:System.Windows.Media.DrawingContext" /> e <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificados.</summary>
		/// <param name="drawingContext">O objeto <see cref="T:System.Windows.Media.DrawingContext" /> no qual o traço será renderizado.</param>
		/// <param name="drawingAttributes">O objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> que define os atributos do traço que é desenhado.</param>
		// Token: 0x06001D10 RID: 7440 RVA: 0x00076888 File Offset: 0x00075C88
		public void Draw(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
		{
			if (drawingContext == null)
			{
				throw new ArgumentNullException("context");
			}
			if (null == drawingAttributes)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			if (drawingAttributes.IsHighlighter)
			{
				drawingContext.PushOpacity(StrokeRenderer.HighlighterOpacity);
				try
				{
					this.DrawInternal(drawingContext, StrokeRenderer.GetHighlighterAttributes(this, this.DrawingAttributes), false);
					return;
				}
				finally
				{
					drawingContext.Pop();
				}
			}
			this.DrawInternal(drawingContext, drawingAttributes, false);
		}

		/// <summary>Retorna os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão dentro do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que especifica a área a ser recortada.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém cópias dos segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão dentro dos limites de <paramref name="bounds" />.</returns>
		// Token: 0x06001D11 RID: 7441 RVA: 0x0007690C File Offset: 0x00075D0C
		public StrokeCollection GetClipResult(Rect bounds)
		{
			return this.GetClipResult(new Point[]
			{
				bounds.TopLeft,
				bounds.TopRight,
				bounds.BottomRight,
				bounds.BottomLeft
			});
		}

		/// <summary>Retorna os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão dentro dos limites especificados.</summary>
		/// <param name="lassoPoints">Os pontos que especificam a linha que define onde recortar.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém cópias dos segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão dentro dos limites especificados.</returns>
		// Token: 0x06001D12 RID: 7442 RVA: 0x00076960 File Offset: 0x00075D60
		public StrokeCollection GetClipResult(IEnumerable<Point> lassoPoints)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			if (IEnumerablePointHelper.GetCount(lassoPoints) == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArray"));
			}
			Lasso lasso = new SingleLoopLasso();
			lasso.AddPoints(lassoPoints);
			return this.Clip(this.HitTest(lasso));
		}

		/// <summary>Retorna os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão fora do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que especifica a área a ser apagada.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão fora dos limites do <see cref="T:System.Windows.Rect" /> especificado.</returns>
		// Token: 0x06001D13 RID: 7443 RVA: 0x000769B0 File Offset: 0x00075DB0
		public StrokeCollection GetEraseResult(Rect bounds)
		{
			return this.GetEraseResult(new Point[]
			{
				bounds.TopLeft,
				bounds.TopRight,
				bounds.BottomRight,
				bounds.BottomLeft
			});
		}

		/// <summary>Retorna os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão fora dos limites especificados.</summary>
		/// <param name="lassoPoints">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que especifica a área a ser apagada.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual que estão fora dos limites especificados.</returns>
		// Token: 0x06001D14 RID: 7444 RVA: 0x00076A04 File Offset: 0x00075E04
		public StrokeCollection GetEraseResult(IEnumerable<Point> lassoPoints)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			if (IEnumerablePointHelper.GetCount(lassoPoints) == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArray"));
			}
			Lasso lasso = new SingleLoopLasso();
			lasso.AddPoints(lassoPoints);
			return this.Erase(this.HitTest(lasso));
		}

		/// <summary>Retorna os segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual após ele ser apresentado pelo caminho designado usando o <see cref="T:System.Windows.Ink.StylusShape" /> especificado.</summary>
		/// <param name="eraserPath">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que especifica o caminho que apresenta o <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <param name="eraserShape">Um <see cref="T:System.Windows.Ink.StylusShape" /> que especifica a forma da borracha.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém cópias dos segmentos do <see cref="T:System.Windows.Ink.Stroke" /> atual após ele ser apresentado pelo caminho especificado.</returns>
		// Token: 0x06001D15 RID: 7445 RVA: 0x00076A54 File Offset: 0x00075E54
		public StrokeCollection GetEraseResult(IEnumerable<Point> eraserPath, StylusShape eraserShape)
		{
			if (eraserShape == null)
			{
				throw new ArgumentNullException("eraserShape");
			}
			if (eraserPath == null)
			{
				throw new ArgumentNullException("eraserPath");
			}
			return this.Erase(this.EraseTest(eraserPath, eraserShape));
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Ink.Stroke" /> atual intersecciona o ponto especificado.</summary>
		/// <param name="point">O <see cref="T:System.Windows.Point" /> ao qual será aplicado o teste de clique.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="point" /> intersecciona o traço atual, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D16 RID: 7446 RVA: 0x00076A8C File Offset: 0x00075E8C
		public bool HitTest(Point point)
		{
			return this.HitTest(new Point[]
			{
				point
			}, new EllipseStylusShape(this.TapHitPointSize, this.TapHitPointSize, this.TapHitRotation));
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Ink.Stroke" /> atual intersecciona a área especificada.</summary>
		/// <param name="point">O <see cref="T:System.Windows.Point" /> que define o centro da área que passará pelo teste de clique.</param>
		/// <param name="diameter">O diâmetro da área que passará pelo teste de clique.</param>
		/// <returns>
		///   <see langword="true" /> se a área especificada intersecciona o traço atual, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D17 RID: 7447 RVA: 0x00076AC4 File Offset: 0x00075EC4
		public bool HitTest(Point point, double diameter)
		{
			if (double.IsNaN(diameter) || diameter < DrawingAttributes.MinWidth || diameter > DrawingAttributes.MaxWidth)
			{
				throw new ArgumentOutOfRangeException("diameter", SR.Get("InvalidDiameter"));
			}
			return this.HitTest(new Point[]
			{
				point
			}, new EllipseStylusShape(diameter, diameter, this.TapHitRotation));
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Ink.Stroke" /> está dentro dos limites do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que representa os limites da área que passará por teste de clique.</param>
		/// <param name="percentageWithinBounds">O percentual do tamanho do <see cref="T:System.Windows.Ink.Stroke" />, que deve estar em <paramref name="percentageWithinBounds" /> para o <see cref="T:System.Windows.Ink.Stroke" /> ser considerado um clique.</param>
		/// <returns>
		///   <see langword="true" /> se o traço atual estiver dentro dos limites de <paramref name="bounds" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D18 RID: 7448 RVA: 0x00076B20 File Offset: 0x00075F20
		public bool HitTest(Rect bounds, int percentageWithinBounds)
		{
			if (percentageWithinBounds < 0 || percentageWithinBounds > 100)
			{
				throw new ArgumentOutOfRangeException("percentageWithinBounds");
			}
			if (percentageWithinBounds == 0)
			{
				return true;
			}
			StrokeInfo strokeInfo = null;
			bool result;
			try
			{
				strokeInfo = new StrokeInfo(this);
				StylusPointCollection stylusPoints = strokeInfo.StylusPoints;
				double num = strokeInfo.TotalWeight * (double)percentageWithinBounds / 100.0 - Stroke.PercentageTolerance;
				for (int i = 0; i < stylusPoints.Count; i++)
				{
					if (bounds.Contains((Point)stylusPoints[i]))
					{
						num -= strokeInfo.GetPointWeight(i);
						if (DoubleUtil.LessThanOrClose(num, 0.0))
						{
							return true;
						}
					}
				}
				result = false;
			}
			finally
			{
				if (strokeInfo != null)
				{
					strokeInfo.Detach();
				}
			}
			return result;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Ink.Stroke" /> atual está dentro dos limites especificados.</summary>
		/// <param name="lassoPoints">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que representa os limites da área que passará por teste de clique.</param>
		/// <param name="percentageWithinLasso">O percentual do tamanho do <see cref="T:System.Windows.Ink.Stroke" />, que deve estar em <paramref name="lassoPoints" /> para o <see cref="T:System.Windows.Ink.Stroke" /> ser considerado um clique.</param>
		/// <returns>
		///   <see langword="true" /> se o traço atual estiver dentro dos limites especificados, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D19 RID: 7449 RVA: 0x00076BE4 File Offset: 0x00075FE4
		public bool HitTest(IEnumerable<Point> lassoPoints, int percentageWithinLasso)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			if (percentageWithinLasso < 0 || percentageWithinLasso > 100)
			{
				throw new ArgumentOutOfRangeException("percentageWithinLasso");
			}
			if (percentageWithinLasso == 0)
			{
				return true;
			}
			StrokeInfo strokeInfo = null;
			bool result;
			try
			{
				strokeInfo = new StrokeInfo(this);
				StylusPointCollection stylusPoints = strokeInfo.StylusPoints;
				double num = strokeInfo.TotalWeight * (double)percentageWithinLasso / 100.0 - Stroke.PercentageTolerance;
				Lasso lasso = new SingleLoopLasso();
				lasso.AddPoints(lassoPoints);
				for (int i = 0; i < stylusPoints.Count; i++)
				{
					if (lasso.Contains((Point)stylusPoints[i]))
					{
						num -= strokeInfo.GetPointWeight(i);
						if (DoubleUtil.LessThan(num, 0.0))
						{
							return true;
						}
					}
				}
				result = false;
			}
			finally
			{
				if (strokeInfo != null)
				{
					strokeInfo.Detach();
				}
			}
			return result;
		}

		/// <summary>Retorna se o caminho especificado intersecciona o <see cref="T:System.Windows.Ink.Stroke" /> usando o <see cref="T:System.Windows.Ink.StylusShape" /> especificado.</summary>
		/// <param name="path">O caminho que <paramref name="stylusShape" /> segue para teste de clique</param>
		/// <param name="stylusShape">A forma do <paramref name="path" /> com a qual realizar o teste de clique.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="stylusShape" /> intersecciona o traço atual, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D1A RID: 7450 RVA: 0x00076CC4 File Offset: 0x000760C4
		public bool HitTest(IEnumerable<Point> path, StylusShape stylusShape)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (stylusShape == null)
			{
				throw new ArgumentNullException("stylusShape");
			}
			if (IEnumerablePointHelper.GetCount(path) == 0)
			{
				return false;
			}
			ErasingStroke erasingStroke = new ErasingStroke(stylusShape);
			erasingStroke.MoveTo(path);
			Rect bounds = erasingStroke.Bounds;
			return !bounds.IsEmpty && bounds.IntersectsWith(this.GetBounds()) && erasingStroke.HitTest(StrokeNodeIterator.GetIterator(this, this.DrawingAttributes));
		}

		/// <summary>Renderiza o <see cref="T:System.Windows.Ink.Stroke" /> no <see cref="T:System.Windows.Media.DrawingContext" /> especificado usando o <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificado.</summary>
		/// <param name="drawingContext">O objeto <see cref="T:System.Windows.Media.DrawingContext" /> no qual o traço será renderizado.</param>
		/// <param name="drawingAttributes">O objeto <see cref="T:System.Windows.Ink.DrawingAttributes" /> que define os atributos do traço que é desenhado.</param>
		// Token: 0x06001D1B RID: 7451 RVA: 0x00076D3C File Offset: 0x0007613C
		protected virtual void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
		{
			if (drawingContext == null)
			{
				throw new ArgumentNullException("drawingContext");
			}
			if (null == drawingAttributes)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			if (this._drawAsHollow)
			{
				DrawingAttributes drawingAttributes2 = drawingAttributes.Clone();
				drawingAttributes2.Height = Math.Max(drawingAttributes2.Height, DrawingAttributes.DefaultHeight);
				drawingAttributes2.Width = Math.Max(drawingAttributes2.Width, DrawingAttributes.DefaultWidth);
				Matrix stylusTipTransform;
				Matrix stylusTipTransform2;
				Stroke.CalcHollowTransforms(drawingAttributes2, out stylusTipTransform, out stylusTipTransform2);
				drawingAttributes2.StylusTipTransform = stylusTipTransform2;
				SolidColorBrush solidColorBrush = new SolidColorBrush(drawingAttributes.Color);
				solidColorBrush.Freeze();
				drawingContext.DrawGeometry(solidColorBrush, null, this.GetGeometry(drawingAttributes2));
				drawingAttributes2.StylusTipTransform = stylusTipTransform;
				drawingContext.DrawGeometry(Brushes.White, null, this.GetGeometry(drawingAttributes2));
				return;
			}
			SolidColorBrush solidColorBrush2 = new SolidColorBrush(drawingAttributes.Color);
			solidColorBrush2.Freeze();
			drawingContext.DrawGeometry(solidColorBrush2, null, this.GetGeometry(drawingAttributes));
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Geometry" /> da <see cref="T:System.Windows.Ink.Stroke" /> atual.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Geometry" /> que representa o <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x06001D1C RID: 7452 RVA: 0x00076E18 File Offset: 0x00076218
		public Geometry GetGeometry()
		{
			return this.GetGeometry(this.DrawingAttributes);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Geometry" /> do <see cref="T:System.Windows.Ink.Stroke" /> atual usando os <see cref="T:System.Windows.Ink.DrawingAttributes" /> especificados.</summary>
		/// <param name="drawingAttributes">O <see cref="T:System.Windows.Ink.DrawingAttributes" /> que determina o <see cref="T:System.Windows.Media.Geometry" /> do <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Geometry" /> que representa o <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x06001D1D RID: 7453 RVA: 0x00076E34 File Offset: 0x00076234
		public Geometry GetGeometry(DrawingAttributes drawingAttributes)
		{
			if (drawingAttributes == null)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			bool flag = DrawingAttributes.GeometricallyEqual(drawingAttributes, this.DrawingAttributes);
			if (flag && (!flag || this._cachedGeometry != null))
			{
				return this._cachedGeometry;
			}
			StrokeNodeIterator iterator = StrokeNodeIterator.GetIterator(this, drawingAttributes);
			Geometry geometry;
			Rect bounds;
			StrokeRenderer.CalcGeometryAndBounds(iterator, drawingAttributes, true, out geometry, out bounds);
			if (!flag)
			{
				return geometry;
			}
			this.SetGeometry(geometry);
			this.SetBounds(bounds);
			return geometry;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00076EA0 File Offset: 0x000762A0
		[FriendAccessAllowed]
		internal void DrawInternal(DrawingContext dc, DrawingAttributes DrawingAttributes, bool drawAsHollow)
		{
			if (drawAsHollow)
			{
				try
				{
					this._drawAsHollow = true;
					this.DrawCore(dc, DrawingAttributes);
					return;
				}
				finally
				{
					this._drawAsHollow = false;
				}
			}
			this.DrawCore(dc, DrawingAttributes);
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00076EF0 File Offset: 0x000762F0
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00076F04 File Offset: 0x00076304
		[FriendAccessAllowed]
		internal bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					this.OnInvalidated(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00076F2C File Offset: 0x0007632C
		internal void SetGeometry(Geometry geometry)
		{
			this._cachedGeometry = geometry;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00076F40 File Offset: 0x00076340
		internal void SetBounds(Rect newBounds)
		{
			this._cachedBounds = newBounds;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00076F54 File Offset: 0x00076354
		internal StrokeIntersection[] EraseTest(IEnumerable<Point> path, StylusShape shape)
		{
			if (IEnumerablePointHelper.GetCount(path) == 0)
			{
				return new StrokeIntersection[0];
			}
			ErasingStroke erasingStroke = new ErasingStroke(shape, path);
			List<StrokeIntersection> list = new List<StrokeIntersection>();
			erasingStroke.EraseTest(StrokeNodeIterator.GetIterator(this, this.DrawingAttributes), list);
			return list.ToArray();
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00076F98 File Offset: 0x00076398
		internal StrokeIntersection[] HitTest(Lasso lasso)
		{
			if (lasso.IsEmpty)
			{
				return new StrokeIntersection[0];
			}
			if (!lasso.Bounds.IntersectsWith(this.GetBounds()))
			{
				return new StrokeIntersection[0];
			}
			return lasso.HitTest(StrokeNodeIterator.GetIterator(this, this.DrawingAttributes));
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00076FE4 File Offset: 0x000763E4
		internal StrokeCollection Erase(StrokeIntersection[] cutAt)
		{
			if (cutAt.Length == 0)
			{
				return new StrokeCollection
				{
					this.Clone()
				};
			}
			StrokeFIndices[] hitSegments = StrokeIntersection.GetHitSegments(cutAt);
			return this.Erase(hitSegments);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00077018 File Offset: 0x00076418
		internal StrokeCollection Clip(StrokeIntersection[] cutAt)
		{
			if (cutAt.Length == 0)
			{
				return new StrokeCollection();
			}
			StrokeFIndices[] inSegments = StrokeIntersection.GetInSegments(cutAt);
			if (inSegments.Length == 0)
			{
				return new StrokeCollection();
			}
			return this.Clip(inSegments);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00077048 File Offset: 0x00076448
		private static void CalcHollowTransforms(DrawingAttributes originalDa, out Matrix innerTransform, out Matrix outerTransform)
		{
			innerTransform = (outerTransform = Matrix.Identity);
			Point point = originalDa.StylusTipTransform.Transform(new Point(originalDa.Width, 0.0));
			Point point2 = originalDa.StylusTipTransform.Transform(new Point(0.0, originalDa.Height));
			double num = Math.Sqrt(point.X * point.X + point.Y * point.Y);
			double num2 = Math.Sqrt(point2.X * point2.X + point2.Y * point2.Y);
			double scaleX = DoubleUtil.GreaterThan(num, Stroke.HollowLineSize) ? ((num - Stroke.HollowLineSize) / num) : 1.0;
			double scaleY = DoubleUtil.GreaterThan(num2, Stroke.HollowLineSize) ? ((num2 - Stroke.HollowLineSize) / num2) : 1.0;
			innerTransform.Scale(scaleX, scaleY);
			innerTransform *= originalDa.StylusTipTransform;
			outerTransform.Scale((num + Stroke.HollowLineSize) / num, (num2 + Stroke.HollowLineSize) / num2);
			outerTransform *= originalDa.StylusTipTransform;
		}

		// Token: 0x04000FAF RID: 4015
		private ExtendedPropertyCollection _extendedProperties;

		// Token: 0x04000FB0 RID: 4016
		private DrawingAttributes _drawingAttributes;

		// Token: 0x04000FB1 RID: 4017
		private StylusPointCollection _stylusPoints;

		// Token: 0x04000FB2 RID: 4018
		internal double TapHitPointSize = 1.0;

		// Token: 0x04000FB3 RID: 4019
		internal double TapHitRotation;

		// Token: 0x04000FB4 RID: 4020
		private Geometry _cachedGeometry;

		// Token: 0x04000FB5 RID: 4021
		private bool _isSelected;

		// Token: 0x04000FB6 RID: 4022
		private bool _drawAsHollow;

		// Token: 0x04000FB7 RID: 4023
		private bool _cloneStylusPoints = true;

		// Token: 0x04000FB8 RID: 4024
		private bool _delayRaiseInvalidated;

		// Token: 0x04000FB9 RID: 4025
		private static readonly double HollowLineSize = 1.0;

		// Token: 0x04000FBA RID: 4026
		private Rect _cachedBounds = Rect.Empty;

		// Token: 0x04000FBB RID: 4027
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x04000FBC RID: 4028
		private const string DrawingAttributesName = "DrawingAttributes";

		// Token: 0x04000FBD RID: 4029
		private const string StylusPointsName = "StylusPoints";

		// Token: 0x04000FBE RID: 4030
		internal static readonly double PercentageTolerance = 0.0001;
	}
}
