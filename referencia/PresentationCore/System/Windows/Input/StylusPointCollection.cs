using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using MS.Internal.Ink.InkSerializedFormat;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Contém uma coleção de objetos <see cref="T:System.Windows.Input.StylusPoint" /> .</summary>
	// Token: 0x020002BA RID: 698
	public class StylusPointCollection : Collection<StylusPoint>
	{
		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.StylusPointCollection" /> é alterado.</summary>
		// Token: 0x14000167 RID: 359
		// (add) Token: 0x060014C2 RID: 5314 RVA: 0x0004C414 File Offset: 0x0004B814
		// (remove) Token: 0x060014C3 RID: 5315 RVA: 0x0004C44C File Offset: 0x0004B84C
		public event EventHandler Changed;

		// Token: 0x14000168 RID: 360
		// (add) Token: 0x060014C4 RID: 5316 RVA: 0x0004C484 File Offset: 0x0004B884
		// (remove) Token: 0x060014C5 RID: 5317 RVA: 0x0004C4BC File Offset: 0x0004B8BC
		internal event CancelEventHandler CountGoingToZero;

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		// Token: 0x060014C6 RID: 5318 RVA: 0x0004C4F4 File Offset: 0x0004B8F4
		public StylusPointCollection()
		{
			this._stylusPointDescription = new StylusPointDescription();
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" /> que pode conter inicialmente o número especificado de objetos <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <param name="initialCapacity">O número de objetos <see cref="T:System.Windows.Input.StylusPoint" /> que a <see cref="T:System.Windows.Input.StylusPointCollection" /> pode conter inicialmente.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCapacity" /> é negativo.</exception>
		// Token: 0x060014C7 RID: 5319 RVA: 0x0004C514 File Offset: 0x0004B914
		public StylusPointCollection(int initialCapacity) : this()
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointConstructionZeroLengthCollection"), "initialCapacity");
			}
			((List<StylusPoint>)base.Items).Capacity = initialCapacity;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém as propriedades especificadas na <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		/// <param name="stylusPointDescription">Uma <see cref="T:System.Windows.Input.StylusPointDescription" /> que especifica as propriedades adicionais armazenadas em cada <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPointDescription" /> é <see langword="null" />.</exception>
		// Token: 0x060014C8 RID: 5320 RVA: 0x0004C554 File Offset: 0x0004B954
		public StylusPointCollection(StylusPointDescription stylusPointDescription)
		{
			if (stylusPointDescription == null)
			{
				throw new ArgumentNullException();
			}
			this._stylusPointDescription = stylusPointDescription;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" /> que é o tamanho especificado e contém as propriedades especificadas na <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		/// <param name="stylusPointDescription">Uma <see cref="T:System.Windows.Input.StylusPointDescription" /> que especifica as propriedades adicionais armazenadas em cada <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="initialCapacity">O número de objetos <see cref="T:System.Windows.Input.StylusPoint" /> que a <see cref="T:System.Windows.Input.StylusPointCollection" /> pode conter inicialmente.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCapacity" /> é negativo.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPointDescription" /> é <see langword="null" />.</exception>
		// Token: 0x060014C9 RID: 5321 RVA: 0x0004C578 File Offset: 0x0004B978
		public StylusPointCollection(StylusPointDescription stylusPointDescription, int initialCapacity) : this(stylusPointDescription)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointConstructionZeroLengthCollection"), "initialCapacity");
			}
			((List<StylusPoint>)base.Items).Capacity = initialCapacity;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" /> com os objetos <see cref="T:System.Windows.Input.StylusPoint" /> especificados.</summary>
		/// <param name="stylusPoints">Um IEnumerable genérico do tipo <see cref="T:System.Windows.Input.StylusPoint" /> a ser adicionado à <see cref="T:System.Windows.Input.StylusPointCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPoints" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O tamanho de <paramref name="points" /> é 0.  
		///
		/// ou - 
		/// Os objetos <see cref="T:System.Windows.Input.StylusPoint" /> nos <paramref name="stylusPoints" /> têm objetos <see cref="T:System.Windows.Input.StylusPointDescription" /> incompatíveis.</exception>
		// Token: 0x060014CA RID: 5322 RVA: 0x0004C5B8 File Offset: 0x0004B9B8
		public StylusPointCollection(IEnumerable<StylusPoint> stylusPoints)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			List<StylusPoint> list = new List<StylusPoint>(stylusPoints);
			if (list.Count == 0)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointConstructionZeroLengthCollection"), "stylusPoints");
			}
			this._stylusPointDescription = list[0].Description;
			((List<StylusPoint>)base.Items).Capacity = list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointCollection" /> com os pontos especificados.</summary>
		/// <param name="points">Um IEnumerable genérico do tipo <see cref="T:System.Windows.Point" /> que especifica os objetos <see cref="T:System.Windows.Input.StylusPoint" /> a serem adicionados à <see cref="T:System.Windows.Input.StylusPointCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="points" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O tamanho de <paramref name="points" /> é 0.</exception>
		// Token: 0x060014CB RID: 5323 RVA: 0x0004C648 File Offset: 0x0004BA48
		public StylusPointCollection(IEnumerable<Point> points) : this()
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			List<StylusPoint> list = new List<StylusPoint>();
			foreach (Point point in points)
			{
				list.Add(new StylusPoint(point.X, point.Y));
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointConstructionZeroLengthCollection"), "points");
			}
			((List<StylusPoint>)base.Items).Capacity = list.Count;
			((List<StylusPoint>)base.Items).AddRange(list);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004C70C File Offset: 0x0004BB0C
		internal StylusPointCollection(StylusPointDescription stylusPointDescription, int[] rawPacketData, GeneralTransform tabletToView, Matrix tabletToViewMatrix)
		{
			if (stylusPointDescription == null)
			{
				throw new ArgumentNullException("stylusPointDescription");
			}
			this._stylusPointDescription = stylusPointDescription;
			int inputArrayLengthPerPoint = stylusPointDescription.GetInputArrayLengthPerPoint();
			int num = rawPacketData.Length / inputArrayLengthPerPoint;
			((List<StylusPoint>)base.Items).Capacity = num;
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				Point point = new Point((double)rawPacketData[num2], (double)rawPacketData[num2 + 1]);
				if (tabletToView != null)
				{
					tabletToView.TryTransform(point, out point);
				}
				else
				{
					point = tabletToViewMatrix.Transform(point);
				}
				int num3 = 2;
				bool containsTruePressure = stylusPointDescription.ContainsTruePressure;
				if (containsTruePressure)
				{
					num3++;
				}
				int[] array = null;
				int num4 = inputArrayLengthPerPoint - num3;
				if (num4 > 0)
				{
					array = new int[num4];
					int j = 0;
					int num5 = num2 + num3;
					while (j < array.Length)
					{
						array[j] = rawPacketData[num5];
						j++;
						num5++;
					}
				}
				StylusPoint item = new StylusPoint(point.X, point.Y, StylusPoint.DefaultPressure, this._stylusPointDescription, array, false, false);
				if (containsTruePressure)
				{
					int value = rawPacketData[num2 + 2];
					item.SetPropertyValue(StylusPointProperties.NormalPressure, value);
				}
				((List<StylusPoint>)base.Items).Add(item);
				i++;
				num2 += inputArrayLengthPerPoint;
			}
		}

		/// <summary>Adiciona a <see cref="T:System.Windows.Input.StylusPointCollection" /> especificada à <see cref="T:System.Windows.Input.StylusPointCollection" /> atual.</summary>
		/// <param name="stylusPoints">A <see cref="T:System.Windows.Input.StylusPointCollection" /> a ser adicionada à <see cref="T:System.Windows.Input.StylusPointCollection" /> atual.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stylusPoints" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Windows.Input.StylusPointDescription" /> de <paramref name="stylusPoints" /> não é compatível com a propriedade <see cref="P:System.Windows.Input.StylusPointCollection.Description" />.</exception>
		// Token: 0x060014CD RID: 5325 RVA: 0x0004C838 File Offset: 0x0004BC38
		public void Add(StylusPointCollection stylusPoints)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (!StylusPointDescription.AreCompatible(stylusPoints.Description, this._stylusPointDescription))
			{
				throw new ArgumentException(SR.Get("IncompatibleStylusPointDescriptions"), "stylusPoints");
			}
			int count = stylusPoints.Count;
			for (int i = 0; i < count; i++)
			{
				StylusPoint item = stylusPoints[i];
				item.Description = this._stylusPointDescription;
				((List<StylusPoint>)base.Items).Add(item);
			}
			if (stylusPoints.Count > 0)
			{
				this.OnChanged(EventArgs.Empty);
			}
		}

		/// <summary>Obtém a <see cref="T:System.Windows.Input.StylusPointDescription" /> que está associada aos objetos <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointDescription" /> que está associado com o <see cref="T:System.Windows.Input.StylusPoint" /> objetos no <see cref="T:System.Windows.Input.StylusPointCollection" />.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0004C8C8 File Offset: 0x0004BCC8
		public StylusPointDescription Description
		{
			get
			{
				if (this._stylusPointDescription == null)
				{
					this._stylusPointDescription = new StylusPointDescription();
				}
				return this._stylusPointDescription;
			}
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Input.StylusPoint" /> de <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Input.StylusPointCollection" /> está conectada a um <see cref="T:System.Windows.Ink.Stroke" />.</exception>
		// Token: 0x060014CF RID: 5327 RVA: 0x0004C8F0 File Offset: 0x0004BCF0
		protected sealed override void ClearItems()
		{
			if (this.CanGoToZero())
			{
				base.ClearItems();
				this.OnChanged(EventArgs.Empty);
				return;
			}
			throw new InvalidOperationException(SR.Get("InvalidStylusPointCollectionZeroCount"));
		}

		/// <summary>Remove o <see cref="T:System.Windows.Input.StylusPoint" /> na posição especificada da <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		/// <param name="index">A posição na qual o <see cref="T:System.Windows.Input.StylusPoint" /> deve ser removido.</param>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Input.StylusPointCollection" /> está conectada a um <see cref="T:System.Windows.Ink.Stroke" /> e há apenas um <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" />.</exception>
		// Token: 0x060014D0 RID: 5328 RVA: 0x0004C928 File Offset: 0x0004BD28
		protected sealed override void RemoveItem(int index)
		{
			if (base.Count > 1 || this.CanGoToZero())
			{
				base.RemoveItem(index);
				this.OnChanged(EventArgs.Empty);
				return;
			}
			throw new InvalidOperationException(SR.Get("InvalidStylusPointCollectionZeroCount"));
		}

		/// <summary>Insere o <see cref="T:System.Windows.Input.StylusPoint" /> especificado na <see cref="T:System.Windows.Input.StylusPointCollection" /> na posição especificada.</summary>
		/// <param name="index">A posição na qual o <see cref="T:System.Windows.Input.StylusPoint" /> deve ser inserido.</param>
		/// <param name="stylusPoint">O <see cref="T:System.Windows.Input.StylusPoint" /> a ser inserido no <see cref="T:System.Windows.Input.StylusPointCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Windows.Input.StylusPointDescription" /> de <paramref name="stylusPoint" /> não é compatível com a propriedade <see cref="P:System.Windows.Input.StylusPointCollection.Description" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é maior ou igual ao número de objetos <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" />.</exception>
		// Token: 0x060014D1 RID: 5329 RVA: 0x0004C968 File Offset: 0x0004BD68
		protected sealed override void InsertItem(int index, StylusPoint stylusPoint)
		{
			if (!StylusPointDescription.AreCompatible(stylusPoint.Description, this._stylusPointDescription))
			{
				throw new ArgumentException(SR.Get("IncompatibleStylusPointDescriptions"), "stylusPoint");
			}
			stylusPoint.Description = this._stylusPointDescription;
			base.InsertItem(index, stylusPoint);
			this.OnChanged(EventArgs.Empty);
		}

		/// <summary>Define o <see cref="T:System.Windows.Input.StylusPoint" /> especificado na posição especificada.</summary>
		/// <param name="index">A posição na qual o <see cref="T:System.Windows.Input.StylusPoint" /> deve ser definido.</param>
		/// <param name="stylusPoint">O <see cref="T:System.Windows.Input.StylusPoint" /> a ser definido na posição especificada.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Windows.Input.StylusPointDescription" /> de <paramref name="stylusPoint" /> não é compatível com a propriedade <see cref="P:System.Windows.Input.StylusPointCollection.Description" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é maior que o número de objetos <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" />.</exception>
		// Token: 0x060014D2 RID: 5330 RVA: 0x0004C9C0 File Offset: 0x0004BDC0
		protected sealed override void SetItem(int index, StylusPoint stylusPoint)
		{
			if (!StylusPointDescription.AreCompatible(stylusPoint.Description, this._stylusPointDescription))
			{
				throw new ArgumentException(SR.Get("IncompatibleStylusPointDescriptions"), "stylusPoint");
			}
			stylusPoint.Description = this._stylusPointDescription;
			base.SetItem(index, stylusPoint);
			this.OnChanged(EventArgs.Empty);
		}

		/// <summary>Copia o <see cref="T:System.Windows.Input.StylusPointCollection" />.</summary>
		/// <returns>Uma nova <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém cópias dos objetos <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" /> atual.</returns>
		// Token: 0x060014D3 RID: 5331 RVA: 0x0004CA18 File Offset: 0x0004BE18
		public StylusPointCollection Clone()
		{
			return this.Clone(System.Windows.Media.Transform.Identity, this.Description, base.Count);
		}

		/// <summary>Converte uma <see cref="T:System.Windows.Input.StylusPointCollection" /> em uma matriz de pontos.</summary>
		/// <param name="stylusPoints">A coleção de pontos de caneta a ser convertida em uma matriz de pontos.</param>
		/// <returns>Uma matriz de pontos que contém os pontos que correspondem a cada <see cref="T:System.Windows.Input.StylusPoint" /> na <see cref="T:System.Windows.Input.StylusPointCollection" />.</returns>
		// Token: 0x060014D4 RID: 5332 RVA: 0x0004CA3C File Offset: 0x0004BE3C
		public static explicit operator Point[](StylusPointCollection stylusPoints)
		{
			if (stylusPoints == null)
			{
				return null;
			}
			Point[] array = new Point[stylusPoints.Count];
			for (int i = 0; i < stylusPoints.Count; i++)
			{
				array[i] = new Point(stylusPoints[i].X, stylusPoints[i].Y);
			}
			return array;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004CA98 File Offset: 0x0004BE98
		internal StylusPointCollection Clone(int count)
		{
			if (count > base.Count || count < 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return this.Clone(System.Windows.Media.Transform.Identity, this.Description, count);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004CAD0 File Offset: 0x0004BED0
		internal StylusPointCollection Clone(GeneralTransform transform, StylusPointDescription descriptionToUse)
		{
			return this.Clone(transform, descriptionToUse, base.Count);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0004CAEC File Offset: 0x0004BEEC
		private StylusPointCollection Clone(GeneralTransform transform, StylusPointDescription descriptionToUse, int count)
		{
			StylusPointCollection stylusPointCollection = new StylusPointCollection(descriptionToUse, count);
			bool flag = transform is Transform && ((Transform)transform).IsIdentity;
			for (int i = 0; i < count; i++)
			{
				if (flag)
				{
					((List<StylusPoint>)stylusPointCollection.Items).Add(base[i]);
				}
				else
				{
					Point inPoint = default(Point);
					StylusPoint item = base[i];
					inPoint.X = item.X;
					inPoint.Y = item.Y;
					transform.TryTransform(inPoint, out inPoint);
					item.X = inPoint.X;
					item.Y = inPoint.Y;
					((List<StylusPoint>)stylusPointCollection.Items).Add(item);
				}
			}
			return stylusPointCollection;
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Input.StylusPointCollection.Changed" />.</summary>
		/// <param name="e">Um <see cref="T:System.EventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060014D8 RID: 5336 RVA: 0x0004CBAC File Offset: 0x0004BFAC
		protected virtual void OnChanged(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (this.Changed != null)
			{
				this.Changed(this, e);
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0004CBDC File Offset: 0x0004BFDC
		internal void Transform(GeneralTransform transform)
		{
			Point inPoint = default(Point);
			for (int i = 0; i < base.Count; i++)
			{
				StylusPoint value = base[i];
				inPoint.X = value.X;
				inPoint.Y = value.Y;
				transform.TryTransform(inPoint, out inPoint);
				value.X = inPoint.X;
				value.Y = inPoint.Y;
				((List<StylusPoint>)base.Items)[i] = value;
			}
			if (base.Count > 0)
			{
				this.OnChanged(EventArgs.Empty);
			}
		}

		/// <summary>Localiza a interseção da <see cref="T:System.Windows.Input.StylusPointDescription" /> especificada e da propriedade <see cref="P:System.Windows.Input.StylusPointCollection.Description" />.</summary>
		/// <param name="subsetToReformatTo">Uma <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser interseccionada com a <see cref="T:System.Windows.Input.StylusPointDescription" /> da <see cref="T:System.Windows.Input.StylusPointCollection" /> atual.</param>
		/// <returns>Uma <see cref="T:System.Windows.Input.StylusPointCollection" /> que tem uma <see cref="T:System.Windows.Input.StylusPointDescription" /> que é um subconjunto da <see cref="T:System.Windows.Input.StylusPointDescription" /> especificada e a <see cref="T:System.Windows.Input.StylusPointDescription" /> que a <see cref="T:System.Windows.Input.StylusPointCollection" /> atual usa.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subsetToReformatTo" /> não é um subconjunto da propriedade <see cref="P:System.Windows.Input.StylusPointCollection.Description" />.</exception>
		// Token: 0x060014DA RID: 5338 RVA: 0x0004CC74 File Offset: 0x0004C074
		public StylusPointCollection Reformat(StylusPointDescription subsetToReformatTo)
		{
			return this.Reformat(subsetToReformatTo, System.Windows.Media.Transform.Identity);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004CC90 File Offset: 0x0004C090
		internal StylusPointCollection Reformat(StylusPointDescription subsetToReformatTo, GeneralTransform transform)
		{
			if (!subsetToReformatTo.IsSubsetOf(this.Description))
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointDescriptionSubset"), "subsetToReformatTo");
			}
			StylusPointDescription commonDescription = StylusPointDescription.GetCommonDescription(subsetToReformatTo, this.Description);
			if (StylusPointDescription.AreCompatible(this.Description, commonDescription) && transform is Transform && ((Transform)transform).IsIdentity)
			{
				return this.Clone(transform, commonDescription);
			}
			StylusPointCollection stylusPointCollection = new StylusPointCollection(commonDescription, base.Count);
			int expectedAdditionalDataCount = commonDescription.GetExpectedAdditionalDataCount();
			ReadOnlyCollection<StylusPointPropertyInfo> stylusPointProperties = commonDescription.GetStylusPointProperties();
			bool flag = transform is Transform && ((Transform)transform).IsIdentity;
			for (int i = 0; i < base.Count; i++)
			{
				StylusPoint stylusPoint = base[i];
				double x = stylusPoint.X;
				double y = stylusPoint.Y;
				float untruncatedPressureFactor = stylusPoint.GetUntruncatedPressureFactor();
				if (!flag)
				{
					Point inPoint = new Point(x, y);
					transform.TryTransform(inPoint, out inPoint);
					x = inPoint.X;
					y = inPoint.Y;
				}
				int[] additionalValues = null;
				if (expectedAdditionalDataCount > 0)
				{
					additionalValues = new int[expectedAdditionalDataCount];
				}
				StylusPoint item = new StylusPoint(x, y, untruncatedPressureFactor, commonDescription, additionalValues, false, false);
				for (int j = StylusPointDescription.RequiredCountOfProperties; j < stylusPointProperties.Count; j++)
				{
					int propertyValue = stylusPoint.GetPropertyValue(stylusPointProperties[j]);
					item.SetPropertyValue(stylusPointProperties[j], propertyValue, false);
				}
				((List<StylusPoint>)stylusPointCollection.Items).Add(item);
			}
			return stylusPointCollection;
		}

		/// <summary>Converte os valores de propriedade dos objetos <see cref="T:System.Windows.Input.StylusPoint" /> em uma matriz de inteiros com sinal de 32 bits.</summary>
		// Token: 0x060014DC RID: 5340 RVA: 0x0004CE04 File Offset: 0x0004C204
		public int[] ToHiMetricArray()
		{
			int outputArrayLengthPerPoint = this.Description.GetOutputArrayLengthPerPoint();
			int[] array = new int[outputArrayLengthPerPoint * base.Count];
			int i = 0;
			int num = 0;
			while (i < base.Count)
			{
				StylusPoint stylusPoint = base[i];
				array[num] = (int)Math.Round(stylusPoint.X * StrokeCollectionSerializer.AvalonToHimetricMultiplier);
				array[num + 1] = (int)Math.Round(stylusPoint.Y * StrokeCollectionSerializer.AvalonToHimetricMultiplier);
				array[num + 2] = stylusPoint.GetPropertyValue(StylusPointProperties.NormalPressure);
				if (outputArrayLengthPerPoint > StylusPointDescription.RequiredCountOfProperties)
				{
					int[] additionalData = stylusPoint.GetAdditionalData();
					int num2 = outputArrayLengthPerPoint - StylusPointDescription.RequiredCountOfProperties;
					for (int j = 0; j < num2; j++)
					{
						array[num + j + 3] = additionalData[j];
					}
				}
				i++;
				num += outputArrayLengthPerPoint;
			}
			return array;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004CEC8 File Offset: 0x0004C2C8
		internal void ToISFReadyArrays(out int[][] output, out bool shouldPersistPressure)
		{
			int num = this.Description.GetOutputArrayLengthPerPoint();
			if (this.Description.ButtonCount > 0)
			{
				num--;
			}
			output = new int[num][];
			for (int i = 0; i < num; i++)
			{
				output[i] = new int[base.Count];
			}
			StylusPointPropertyInfo propertyInfo = this.Description.GetPropertyInfo(StylusPointPropertyIds.NormalPressure);
			shouldPersistPressure = !StylusPointPropertyInfo.AreCompatible(propertyInfo, StylusPointPropertyInfoDefaults.NormalPressure);
			for (int j = 0; j < base.Count; j++)
			{
				StylusPoint stylusPoint = base[j];
				output[0][j] = (int)Math.Round(stylusPoint.X * StrokeCollectionSerializer.AvalonToHimetricMultiplier);
				output[1][j] = (int)Math.Round(stylusPoint.Y * StrokeCollectionSerializer.AvalonToHimetricMultiplier);
				output[2][j] = stylusPoint.GetPropertyValue(StylusPointProperties.NormalPressure);
				if (!shouldPersistPressure && !stylusPoint.HasDefaultPressure)
				{
					shouldPersistPressure = true;
				}
				if (num > StylusPointDescription.RequiredCountOfProperties)
				{
					int[] additionalData = stylusPoint.GetAdditionalData();
					int num2 = num - StylusPointDescription.RequiredCountOfProperties;
					for (int k = 0; k < num2; k++)
					{
						output[k + 3][j] = additionalData[k];
					}
				}
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004CFE0 File Offset: 0x0004C3E0
		private bool CanGoToZero()
		{
			if (this.CountGoingToZero == null)
			{
				return true;
			}
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			cancelEventArgs.Cancel = false;
			this.CountGoingToZero(this, cancelEventArgs);
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x04000B0D RID: 2829
		private StylusPointDescription _stylusPointDescription;
	}
}
