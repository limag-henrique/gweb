using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.Ink;
using MS.Internal.Ink.InkSerializedFormat;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Ink.Stroke" /> .</summary>
	// Token: 0x0200035D RID: 861
	[TypeConverter(typeof(StrokeCollectionConverter))]
	public class StrokeCollection : Collection<Stroke>, INotifyPropertyChanged, INotifyCollectionChanged
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		// Token: 0x06001D2D RID: 7469 RVA: 0x00077360 File Offset: 0x00076760
		public StrokeCollection()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços especificados.</summary>
		/// <param name="strokes">Os traços a serem adicionados ao <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		// Token: 0x06001D2E RID: 7470 RVA: 0x00077374 File Offset: 0x00076774
		public StrokeCollection(IEnumerable<Stroke> strokes)
		{
			if (strokes == null)
			{
				throw new ArgumentNullException("strokes");
			}
			List<Stroke> list = (List<Stroke>)base.Items;
			foreach (Stroke item in strokes)
			{
				if (list.Contains(item))
				{
					list.Clear();
					throw new ArgumentException(SR.Get("StrokeIsDuplicated"), "strokes");
				}
				list.Add(item);
			}
		}

		/// <summary>Inicializa um <see cref="T:System.Windows.Ink.StrokeCollection" /> do <see cref="T:System.IO.Stream" /> especificado do ISF (Formato ISF).</summary>
		/// <param name="stream">O fluxo que contém os dados de tinta.</param>
		// Token: 0x06001D2F RID: 7471 RVA: 0x0007740C File Offset: 0x0007680C
		public StrokeCollection(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(SR.Get("Image_StreamRead"), "stream");
			}
			Stream seekableStream = this.GetSeekableStream(stream);
			if (seekableStream == null)
			{
				throw new ArgumentException(SR.Get("Invalid_isfData_Length"), "stream");
			}
			StrokeCollectionSerializer strokeCollectionSerializer = new StrokeCollectionSerializer(this);
			strokeCollectionSerializer.DecodeISF(seekableStream);
		}

		/// <summary>Salva o <see cref="T:System.Windows.Ink.StrokeCollection" /> no <see cref="T:System.IO.Stream" /> especificado e o compacta, quando indicado.</summary>
		/// <param name="stream">O <see cref="T:System.IO.Stream" /> no qual salvar o <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		/// <param name="compress">
		///   <see langword="true" /> para compactar o <see cref="T:System.Windows.Ink.StrokeCollection" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001D30 RID: 7472 RVA: 0x00077478 File Offset: 0x00076878
		public virtual void Save(Stream stream, bool compress)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(SR.Get("Image_StreamWrite"), "stream");
			}
			this.SaveIsf(stream, compress);
		}

		/// <summary>Salva o <see cref="T:System.Windows.Ink.StrokeCollection" /> no <see cref="T:System.IO.Stream" /> especificado.</summary>
		/// <param name="stream">O <see cref="T:System.IO.Stream" /> no qual salvar o <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		// Token: 0x06001D31 RID: 7473 RVA: 0x000774B8 File Offset: 0x000768B8
		public void Save(Stream stream)
		{
			this.Save(stream, true);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000774D0 File Offset: 0x000768D0
		internal void SaveIsf(Stream stream, bool compress)
		{
			new StrokeCollectionSerializer(this)
			{
				CurrentCompressionMode = (compress ? CompressionMode.Compressed : CompressionMode.NoCompression)
			}.EncodeISF(stream);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000774F8 File Offset: 0x000768F8
		private Stream GetSeekableStream(Stream stream)
		{
			if (!stream.CanSeek)
			{
				MemoryStream memoryStream = new MemoryStream();
				int num = 4096;
				byte[] buffer = new byte[num];
				int num2 = num;
				while (num2 == num)
				{
					num2 = stream.Read(buffer, 0, 4096);
					memoryStream.Write(buffer, 0, num2);
				}
				memoryStream.Position = 0L;
				return memoryStream;
			}
			int num3 = (int)(stream.Length - stream.Position);
			if (num3 < 1)
			{
				return null;
			}
			return stream;
		}

		/// <summary>Adiciona uma propriedade personalizada ao <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> a ser associado à propriedade personalizada.</param>
		/// <param name="propertyData">O valor da propriedade personalizada. <paramref name="propertyData" /> deve ser do tipo <see cref="T:System.Char" />, <see cref="T:System.Byte" />, <see cref="T:System.Int16" />, <see cref="T:System.UInt16" />, <see cref="T:System.Int32" />, <see cref="T:System.UInt32" />, <see cref="T:System.Int64" />, <see cref="T:System.UInt64" />, <see cref="T:System.Single" />, <see cref="T:System.Double" />, <see cref="T:System.DateTime" />, <see cref="T:System.Boolean" />, <see cref="T:System.String" />, <see cref="T:System.Decimal" /> ou uma matriz desses tipos de dados, exceto <see cref="T:System.String" />, que não é permitido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> é um <see cref="T:System.Guid" /> vazio.  
		///
		/// ou - 
		/// <paramref name="propertyData" /> não é um dos tipos de dados permitidos listados na seção <see langword="Parameters" />.</exception>
		// Token: 0x06001D34 RID: 7476 RVA: 0x00077564 File Offset: 0x00076964
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

		/// <summary>Remove a propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> associado à propriedade personalizada a ser removida.</param>
		// Token: 0x06001D35 RID: 7477 RVA: 0x000775B8 File Offset: 0x000769B8
		public void RemovePropertyData(Guid propertyDataId)
		{
			object propertyData = this.GetPropertyData(propertyDataId);
			this.ExtendedProperties.Remove(propertyDataId);
			this.OnPropertyDataChanged(new PropertyDataChangedEventArgs(propertyDataId, null, propertyData));
		}

		/// <summary>Retorna o valor da propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> associado à propriedade personalizada a ser obtida.</param>
		/// <returns>O valor da propriedade personalizada associada ao <see cref="T:System.Guid" /> especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyDataId" /> não está associado a uma propriedade personalizada do <see cref="T:System.Windows.Ink.StrokeCollection" />.</exception>
		// Token: 0x06001D36 RID: 7478 RVA: 0x000775E8 File Offset: 0x000769E8
		public object GetPropertyData(Guid propertyDataId)
		{
			if (propertyDataId == Guid.Empty)
			{
				throw new ArgumentException(SR.Get("InvalidGuid"), "propertyDataId");
			}
			return this.ExtendedProperties[propertyDataId];
		}

		/// <summary>Retorna os GUIDs de todas as propriedades personalizadas associadas a <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <returns>Uma matriz do tipo <see cref="T:System.Guid" /> que representa os identificadores de propriedade personalizada.</returns>
		// Token: 0x06001D37 RID: 7479 RVA: 0x00077624 File Offset: 0x00076A24
		public Guid[] GetPropertyDataIds()
		{
			return this.ExtendedProperties.GetGuidArray();
		}

		/// <summary>Retorna se o identificador de propriedade personalizada especificada está no <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="propertyDataId">O <see cref="T:System.Guid" /> a ser localizado no <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o identificador da propriedade personalizada especificada estiver no objeto <see cref="T:System.Windows.Ink.StrokeCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001D38 RID: 7480 RVA: 0x0007763C File Offset: 0x00076A3C
		public bool ContainsPropertyData(Guid propertyDataId)
		{
			return this.ExtendedProperties.Contains(propertyDataId);
		}

		/// <summary>Modifica cada um dos <see cref="P:System.Windows.Ink.Stroke.StylusPoints" /> e, opcionalmente, o <see cref="P:System.Windows.Ink.DrawingAttributes.StylusTipTransform" /> para cada traço no <see cref="T:System.Windows.Ink.StrokeCollection" /> conforme o <see cref="T:System.Windows.Media.Matrix" /> especificado.</summary>
		/// <param name="transformMatrix">Um <see cref="T:System.Windows.Media.Matrix" /> que especifica a transformação a ser executada no <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		/// <param name="applyToStylusTip">
		///   <see langword="true" /> para aplicar a transformação à ponta da caneta; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001D39 RID: 7481 RVA: 0x00077658 File Offset: 0x00076A58
		public void Transform(Matrix transformMatrix, bool applyToStylusTip)
		{
			if (!transformMatrix.HasInverse)
			{
				throw new ArgumentException(SR.Get("MatrixNotInvertible"), "transformMatrix");
			}
			if (transformMatrix.IsIdentity || base.Count == 0)
			{
				return;
			}
			foreach (Stroke stroke in this)
			{
				stroke.Transform(transformMatrix, applyToStylusTip);
			}
		}

		/// <summary>Copia o <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <returns>Uma cópia do <see cref="T:System.Windows.Ink.StrokeCollection" />.</returns>
		// Token: 0x06001D3A RID: 7482 RVA: 0x000776E0 File Offset: 0x00076AE0
		public virtual StrokeCollection Clone()
		{
			StrokeCollection strokeCollection = new StrokeCollection();
			foreach (Stroke stroke in this)
			{
				strokeCollection.Add(stroke.Clone());
			}
			if (this._extendedProperties != null)
			{
				strokeCollection._extendedProperties = this._extendedProperties.Clone();
			}
			return strokeCollection;
		}

		/// <summary>Limpa todos os traços do <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		// Token: 0x06001D3B RID: 7483 RVA: 0x00077760 File Offset: 0x00076B60
		protected sealed override void ClearItems()
		{
			if (base.Count > 0)
			{
				StrokeCollection strokeCollection = new StrokeCollection();
				for (int i = 0; i < base.Count; i++)
				{
					((List<Stroke>)strokeCollection.Items).Add(base[i]);
				}
				base.ClearItems();
				this.RaiseStrokesChanged(null, strokeCollection, -1);
			}
		}

		/// <summary>Remove o traço no índice especificado do <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="index">O índice especificado.</param>
		// Token: 0x06001D3C RID: 7484 RVA: 0x000777B4 File Offset: 0x00076BB4
		protected sealed override void RemoveItem(int index)
		{
			Stroke item = base[index];
			base.RemoveItem(index);
			StrokeCollection strokeCollection = new StrokeCollection();
			((List<Stroke>)strokeCollection.Items).Add(item);
			this.RaiseStrokesChanged(null, strokeCollection, index);
		}

		/// <summary>Insere um traço no <see cref="T:System.Windows.Ink.StrokeCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice especificado.</param>
		/// <param name="stroke">O traço especificado</param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x000777F0 File Offset: 0x00076BF0
		protected sealed override void InsertItem(int index, Stroke stroke)
		{
			if (stroke == null)
			{
				throw new ArgumentNullException("stroke");
			}
			if (this.IndexOf(stroke) != -1)
			{
				throw new ArgumentException(SR.Get("StrokeIsDuplicated"), "stroke");
			}
			base.InsertItem(index, stroke);
			StrokeCollection strokeCollection = new StrokeCollection();
			((List<Stroke>)strokeCollection.Items).Add(stroke);
			this.RaiseStrokesChanged(strokeCollection, null, index);
		}

		/// <summary>Substitui o traço no índice especificado.</summary>
		/// <param name="index">A posição na qual o traçado será substituído.</param>
		/// <param name="stroke">O traço para substituir o traço atual.</param>
		// Token: 0x06001D3E RID: 7486 RVA: 0x00077854 File Offset: 0x00076C54
		protected sealed override void SetItem(int index, Stroke stroke)
		{
			if (stroke == null)
			{
				throw new ArgumentNullException("stroke");
			}
			if (this.IndexOf(stroke) != -1)
			{
				throw new ArgumentException(SR.Get("StrokeIsDuplicated"), "stroke");
			}
			Stroke item = base[index];
			base.SetItem(index, stroke);
			StrokeCollection strokeCollection = new StrokeCollection();
			((List<Stroke>)strokeCollection.Items).Add(item);
			StrokeCollection strokeCollection2 = new StrokeCollection();
			((List<Stroke>)strokeCollection2.Items).Add(stroke);
			this.RaiseStrokesChanged(strokeCollection2, strokeCollection, index);
		}

		/// <summary>Retorna o índice do <see cref="T:System.Windows.Ink.Stroke" /> especificado no <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="stroke">O traço cuja posição é necessária.</param>
		/// <returns>O índice do traço.</returns>
		// Token: 0x06001D3F RID: 7487 RVA: 0x000778D8 File Offset: 0x00076CD8
		public new int IndexOf(Stroke stroke)
		{
			if (stroke == null)
			{
				return -1;
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i] == stroke)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Remove os traços especificados da coleção.</summary>
		/// <param name="strokes">O <see cref="T:System.Windows.Ink.StrokeCollection" /> a ser removido da coleção.</param>
		// Token: 0x06001D40 RID: 7488 RVA: 0x00077908 File Offset: 0x00076D08
		public void Remove(StrokeCollection strokes)
		{
			if (strokes == null)
			{
				throw new ArgumentNullException("strokes");
			}
			if (strokes.Count == 0)
			{
				return;
			}
			int[] strokeIndexes = this.GetStrokeIndexes(strokes);
			if (strokeIndexes == null)
			{
				throw new ArgumentException(SR.Get("InvalidRemovedStroke"), "strokes")
				{
					Data = 
					{
						{
							"System.Windows.Ink.StrokeCollection",
							""
						}
					}
				};
			}
			for (int i = strokeIndexes.Length - 1; i >= 0; i--)
			{
				((List<Stroke>)base.Items).RemoveAt(strokeIndexes[i]);
			}
			this.RaiseStrokesChanged(null, strokes, strokeIndexes[0]);
		}

		/// <summary>Adiciona o traço especificado ao <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="strokes">O <see cref="T:System.Windows.Ink.StrokeCollection" /> a adicionar à coleção.</param>
		/// <exception cref="T:System.ArgumentException">Um <see cref="T:System.Windows.Ink.Stroke" /> em <paramref name="strokes" /> já é membro do <see cref="T:System.Windows.Ink.StrokeCollection" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="strokes" /> é <see langword="null" />.</exception>
		// Token: 0x06001D41 RID: 7489 RVA: 0x00077994 File Offset: 0x00076D94
		public void Add(StrokeCollection strokes)
		{
			if (strokes == null)
			{
				throw new ArgumentNullException("strokes");
			}
			if (strokes.Count == 0)
			{
				return;
			}
			int count = base.Count;
			for (int i = 0; i < strokes.Count; i++)
			{
				Stroke stroke = strokes[i];
				if (this.IndexOf(stroke) != -1)
				{
					throw new ArgumentException(SR.Get("StrokeIsDuplicated"), "strokes");
				}
			}
			((List<Stroke>)base.Items).AddRange(strokes);
			this.RaiseStrokesChanged(strokes, null, count);
		}

		/// <summary>Substitui o <see cref="T:System.Windows.Ink.Stroke" /> especificado pelo <see cref="T:System.Windows.Ink.StrokeCollection" /> indicado.</summary>
		/// <param name="strokeToReplace">O <see cref="T:System.Windows.Ink.Stroke" /> a ser substituído.</param>
		/// <param name="strokesToReplaceWith">A fonte de <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="strokesToReplace" /> está vazio.  
		///
		/// ou - 
		/// <paramref name="strokesToReplaceWith" /> está vazio.  
		///
		/// ou - 
		/// Um <see cref="T:System.Windows.Ink.Stroke" /> em <paramref name="strokesToReplaceWith" /> já está em <paramref name="strokesToReplace" />.</exception>
		// Token: 0x06001D42 RID: 7490 RVA: 0x00077A14 File Offset: 0x00076E14
		public void Replace(Stroke strokeToReplace, StrokeCollection strokesToReplaceWith)
		{
			if (strokeToReplace == null)
			{
				throw new ArgumentNullException(SR.Get("EmptyScToReplace"));
			}
			this.Replace(new StrokeCollection
			{
				strokeToReplace
			}, strokesToReplaceWith);
		}

		/// <summary>Substitui o <see cref="T:System.Windows.Ink.StrokeCollection" /> especificado por um novo <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="strokesToReplace">O <see cref="T:System.Windows.Ink.StrokeCollection" /> de destino.</param>
		/// <param name="strokesToReplaceWith">A fonte de <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="strokesToReplace" /> está vazio.  
		///
		/// ou - 
		/// <paramref name="strokesToReplaceWith" /> está vazio.  
		///
		/// ou - 
		/// Um <see cref="T:System.Windows.Ink.Stroke" /> em <paramref name="strokesToReplaceWith" /> já está em <paramref name="strokesToReplace." />  
		///
		/// ou - 
		/// Os traços no <paramref name="strokesToReplaceWith" /> não são contínuos.</exception>
		// Token: 0x06001D43 RID: 7491 RVA: 0x00077A4C File Offset: 0x00076E4C
		public void Replace(StrokeCollection strokesToReplace, StrokeCollection strokesToReplaceWith)
		{
			if (strokesToReplace == null)
			{
				throw new ArgumentNullException(SR.Get("EmptyScToReplace"));
			}
			if (strokesToReplaceWith == null)
			{
				throw new ArgumentNullException(SR.Get("EmptyScToReplaceWith"));
			}
			if (strokesToReplace.Count == 0)
			{
				throw new ArgumentException(SR.Get("EmptyScToReplace"), "strokesToReplace")
				{
					Data = 
					{
						{
							"System.Windows.Ink.StrokeCollection",
							""
						}
					}
				};
			}
			int[] strokeIndexes = this.GetStrokeIndexes(strokesToReplace);
			if (strokeIndexes == null)
			{
				throw new ArgumentException(SR.Get("InvalidRemovedStroke"), "strokesToReplace")
				{
					Data = 
					{
						{
							"System.Windows.Ink.StrokeCollection",
							""
						}
					}
				};
			}
			for (int i = 0; i < strokesToReplaceWith.Count; i++)
			{
				Stroke stroke = strokesToReplaceWith[i];
				if (this.IndexOf(stroke) != -1)
				{
					throw new ArgumentException(SR.Get("StrokeIsDuplicated"), "strokesToReplaceWith");
				}
			}
			for (int j = strokeIndexes.Length - 1; j >= 0; j--)
			{
				((List<Stroke>)base.Items).RemoveAt(strokeIndexes[j]);
			}
			if (strokesToReplaceWith.Count > 0)
			{
				((List<Stroke>)base.Items).InsertRange(strokeIndexes[0], strokesToReplaceWith);
			}
			this.RaiseStrokesChanged(strokesToReplaceWith, strokesToReplace, strokeIndexes[0]);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00077B78 File Offset: 0x00076F78
		internal void AddWithoutEvent(Stroke stroke)
		{
			((List<Stroke>)base.Items).Add(stroke);
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00077B98 File Offset: 0x00076F98
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x00077BC4 File Offset: 0x00076FC4
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
			private set
			{
				if (value != null)
				{
					this._extendedProperties = value;
				}
			}
		}

		/// <summary>Ocorre quando um <see cref="T:System.Windows.Ink.Stroke" /> nesta coleção é alterada.</summary>
		// Token: 0x14000186 RID: 390
		// (add) Token: 0x06001D47 RID: 7495 RVA: 0x00077BE4 File Offset: 0x00076FE4
		// (remove) Token: 0x06001D48 RID: 7496 RVA: 0x00077C1C File Offset: 0x0007701C
		public event StrokeCollectionChangedEventHandler StrokesChanged;

		// Token: 0x14000187 RID: 391
		// (add) Token: 0x06001D49 RID: 7497 RVA: 0x00077C54 File Offset: 0x00077054
		// (remove) Token: 0x06001D4A RID: 7498 RVA: 0x00077C8C File Offset: 0x0007708C
		internal event StrokeCollectionChangedEventHandler StrokesChangedInternal;

		/// <summary>Ocorre quando a propriedade personalizada é adicionada ou removida do <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		// Token: 0x14000188 RID: 392
		// (add) Token: 0x06001D4B RID: 7499 RVA: 0x00077CC4 File Offset: 0x000770C4
		// (remove) Token: 0x06001D4C RID: 7500 RVA: 0x00077CFC File Offset: 0x000770FC
		public event PropertyDataChangedEventHandler PropertyDataChanged;

		/// <summary>Ocorre quando o valor de qualquer propriedade <see cref="T:System.Windows.Ink.StrokeCollection" /> foi alterado.</summary>
		// Token: 0x14000189 RID: 393
		// (add) Token: 0x06001D4D RID: 7501 RVA: 0x00077D34 File Offset: 0x00077134
		// (remove) Token: 0x06001D4E RID: 7502 RVA: 0x00077D58 File Offset: 0x00077158
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

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Ink.StrokeCollection" /> é alterado.</summary>
		// Token: 0x1400018A RID: 394
		// (add) Token: 0x06001D4F RID: 7503 RVA: 0x00077D7C File Offset: 0x0007717C
		// (remove) Token: 0x06001D50 RID: 7504 RVA: 0x00077DA0 File Offset: 0x000771A0
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.StrokeCollection.StrokesChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Ink.StrokeCollectionChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D51 RID: 7505 RVA: 0x00077DC4 File Offset: 0x000771C4
		protected virtual void OnStrokesChanged(StrokeCollectionChangedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e", SR.Get("EventArgIsNull"));
			}
			if (this.StrokesChangedInternal != null)
			{
				this.StrokesChangedInternal(this, e);
			}
			if (this.StrokesChanged != null)
			{
				this.StrokesChanged(this, e);
			}
			if (this._collectionChanged != null)
			{
				NotifyCollectionChangedEventArgs e2;
				if (base.Count == 0)
				{
					e2 = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
				}
				else if (e.Added.Count == 0)
				{
					e2 = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.Removed, e.Index);
				}
				else if (e.Removed.Count == 0)
				{
					e2 = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.Added, e.Index);
				}
				else
				{
					e2 = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, e.Added, e.Removed, e.Index);
				}
				this._collectionChanged(this, e2);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.StrokeCollection.PropertyDataChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Ink.PropertyDataChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001D52 RID: 7506 RVA: 0x00077E9C File Offset: 0x0007729C
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

		/// <summary>Ocorre quando qualquer propriedade <see cref="T:System.Windows.Ink.StrokeCollection" /> é alterada.</summary>
		/// <param name="e">Dados do evento.</param>
		// Token: 0x06001D53 RID: 7507 RVA: 0x00077ED8 File Offset: 0x000772D8
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this._propertyChanged != null)
			{
				this._propertyChanged(this, e);
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00077EFC File Offset: 0x000772FC
		private int OptimisticIndexOf(int startingIndex, Stroke stroke)
		{
			for (int i = startingIndex; i < base.Count; i++)
			{
				if (base[i] == stroke)
				{
					return i;
				}
			}
			for (int j = 0; j < startingIndex; j++)
			{
				if (base[j] == stroke)
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00077F40 File Offset: 0x00077340
		private int[] GetStrokeIndexes(StrokeCollection strokes)
		{
			int[] array = new int[strokes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = int.MaxValue;
			}
			int num = 0;
			int num2 = -1;
			int num3 = 0;
			for (int j = 0; j < strokes.Count; j++)
			{
				num = this.OptimisticIndexOf(num, strokes[j]);
				if (num == -1)
				{
					return null;
				}
				if (num > num2)
				{
					array[num3++] = num;
					num2 = num;
				}
				else
				{
					int k = 0;
					while (k < array.Length)
					{
						if (num < array[k])
						{
							if (array[k] != 2147483647)
							{
								for (int l = array.Length - 1; l > k; l--)
								{
									array[l] = array[l - 1];
								}
							}
							array[k] = num;
							num3++;
							if (num > num2)
							{
								num2 = num;
								break;
							}
							break;
						}
						else
						{
							k++;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0007800C File Offset: 0x0007740C
		private void RaiseStrokesChanged(StrokeCollection addedStrokes, StrokeCollection removedStrokes, int index)
		{
			StrokeCollectionChangedEventArgs e = new StrokeCollectionChangedEventArgs(addedStrokes, removedStrokes, index);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnStrokesChanged(e);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00078040 File Offset: 0x00077440
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>Retorna os limites dos traços na coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que contém os limites dos traços no <see cref="T:System.Windows.Ink.StrokeCollection" />.</returns>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0007805C File Offset: 0x0007745C
		public Rect GetBounds()
		{
			Rect empty = Rect.Empty;
			foreach (Stroke stroke in this)
			{
				empty.Union(stroke.GetBounds());
			}
			return empty;
		}

		/// <summary>Retorna uma coleção de traços que se interseccionam no ponto especificado.</summary>
		/// <param name="point">O ponto para fazer o teste de clique.</param>
		/// <returns>Uma coleção de objetos <see cref="T:System.Windows.Ink.Stroke" /> que interseccionam o ponto especificado.</returns>
		// Token: 0x06001D59 RID: 7513 RVA: 0x000780C0 File Offset: 0x000774C0
		public StrokeCollection HitTest(Point point)
		{
			return this.PointHitTest(point, new RectangleStylusShape(1.0, 1.0));
		}

		/// <summary>Retorna uma coleção de traços que se interseccionam na área especificada.</summary>
		/// <param name="point">O <see cref="T:System.Windows.Point" /> ao qual será aplicado o teste de clique.</param>
		/// <param name="diameter">O tamanho da área em torno do <see cref="T:System.Windows.Point" /> que passará pelo teste de clique.</param>
		/// <returns>Uma coleção de objetos <see cref="T:System.Windows.Ink.Stroke" /> que interseccionam o ponto especificado.</returns>
		// Token: 0x06001D5A RID: 7514 RVA: 0x000780EC File Offset: 0x000774EC
		public StrokeCollection HitTest(Point point, double diameter)
		{
			if (double.IsNaN(diameter) || diameter < DrawingAttributes.MinWidth || diameter > DrawingAttributes.MaxWidth)
			{
				throw new ArgumentOutOfRangeException("diameter", SR.Get("InvalidDiameter"));
			}
			return this.PointHitTest(point, new EllipseStylusShape(diameter, diameter));
		}

		/// <summary>Retorna uma coleção de traços que contém pelo menos o percentual especificado do tamanho dentro da área especificada.</summary>
		/// <param name="lassoPoints">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que representa os limites da área que passará por teste de clique.</param>
		/// <param name="percentageWithinLasso">O comprimento aceitável do <see cref="T:System.Windows.Ink.Stroke" />, como um percentual, que <paramref name="lassoPoints" /> deverá conter.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que tem traços com pelo menos o percentual especificado dentro da matriz <see cref="T:System.Windows.Point" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="lassoPoints" /> é <see langword="null" />.  
		///
		/// ou - 
		/// <paramref name="percentageWithinLasso" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lassoPoints" /> contém uma matriz vazia.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="percentageWithinLasso" /> é menor que 0 ou maior que 100.</exception>
		// Token: 0x06001D5B RID: 7515 RVA: 0x00078134 File Offset: 0x00077534
		public StrokeCollection HitTest(IEnumerable<Point> lassoPoints, int percentageWithinLasso)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			if (percentageWithinLasso < 0 || percentageWithinLasso > 100)
			{
				throw new ArgumentOutOfRangeException("percentageWithinLasso");
			}
			if (IEnumerablePointHelper.GetCount(lassoPoints) < 3)
			{
				return new StrokeCollection();
			}
			Lasso lasso = new SingleLoopLasso();
			lasso.AddPoints(lassoPoints);
			StrokeCollection strokeCollection = new StrokeCollection();
			foreach (Stroke stroke in this)
			{
				if (percentageWithinLasso == 0)
				{
					strokeCollection.Add(stroke);
				}
				else
				{
					StrokeInfo strokeInfo = null;
					try
					{
						strokeInfo = new StrokeInfo(stroke);
						StylusPointCollection stylusPoints = strokeInfo.StylusPoints;
						double num = strokeInfo.TotalWeight * (double)percentageWithinLasso / 100.0 - Stroke.PercentageTolerance;
						for (int i = 0; i < stylusPoints.Count; i++)
						{
							if (lasso.Contains((Point)stylusPoints[i]))
							{
								num -= strokeInfo.GetPointWeight(i);
								if (DoubleUtil.LessThanOrClose(num, 0.0))
								{
									strokeCollection.Add(stroke);
									break;
								}
							}
						}
					}
					finally
					{
						if (strokeInfo != null)
						{
							strokeInfo.Detach();
						}
					}
				}
			}
			return strokeCollection;
		}

		/// <summary>Retorna uma coleção de traços que contém pelo menos o percentual especificado do tamanho dentro do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que especifica os limites do teste de clique.</param>
		/// <param name="percentageWithinBounds">O tamanho mínimo necessário de um Traço que deve existir dentro dos limites para ele ser considerado uma ocorrência.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que tem traços com pelo menos o percentual especificado dentro do <see cref="T:System.Windows.Rect" />.</returns>
		// Token: 0x06001D5C RID: 7516 RVA: 0x00078280 File Offset: 0x00077680
		public StrokeCollection HitTest(Rect bounds, int percentageWithinBounds)
		{
			if (percentageWithinBounds < 0 || percentageWithinBounds > 100)
			{
				throw new ArgumentOutOfRangeException("percentageWithinBounds");
			}
			if (bounds.IsEmpty)
			{
				return new StrokeCollection();
			}
			StrokeCollection strokeCollection = new StrokeCollection();
			foreach (Stroke stroke in this)
			{
				if (stroke.HitTest(bounds, percentageWithinBounds))
				{
					strokeCollection.Add(stroke);
				}
			}
			return strokeCollection;
		}

		/// <summary>Retorna uma coleção de traços que se interseccionam com o caminho especificado.</summary>
		/// <param name="path">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que representa o caminho do teste de clique.</param>
		/// <param name="stylusShape">O <see cref="T:System.Windows.Ink.StylusShape" /> que especifica a forma de <paramref name="eraserPath" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> de traços que se interseccionam com <paramref name="path" />.</returns>
		// Token: 0x06001D5D RID: 7517 RVA: 0x00078308 File Offset: 0x00077708
		public StrokeCollection HitTest(IEnumerable<Point> path, StylusShape stylusShape)
		{
			if (stylusShape == null)
			{
				throw new ArgumentNullException("stylusShape");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (IEnumerablePointHelper.GetCount(path) == 0)
			{
				return new StrokeCollection();
			}
			ErasingStroke erasingStroke = new ErasingStroke(stylusShape, path);
			Rect bounds = erasingStroke.Bounds;
			if (bounds.IsEmpty)
			{
				return new StrokeCollection();
			}
			StrokeCollection strokeCollection = new StrokeCollection();
			foreach (Stroke stroke in this)
			{
				if (bounds.IntersectsWith(stroke.GetBounds()) && erasingStroke.HitTest(StrokeNodeIterator.GetIterator(stroke, stroke.DrawingAttributes)))
				{
					strokeCollection.Add(stroke);
				}
			}
			return strokeCollection;
		}

		/// <summary>Remove todos os traços no <see cref="T:System.Windows.Ink.StrokeCollection" /> que estão fora dos limites da matriz <see cref="T:System.Drawing.Point" /> especificada.</summary>
		/// <param name="lassoPoints">Uma matriz do tipo <see cref="T:System.Drawing.Point" /> que especifica a área a ser recortada.</param>
		// Token: 0x06001D5E RID: 7518 RVA: 0x000783D0 File Offset: 0x000777D0
		public void Clip(IEnumerable<Point> lassoPoints)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			int count = IEnumerablePointHelper.GetCount(lassoPoints);
			if (count == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArray"));
			}
			if (count < 3)
			{
				base.Clear();
				return;
			}
			Lasso lasso = new SingleLoopLasso();
			lasso.AddPoints(lassoPoints);
			for (int i = 0; i < base.Count; i++)
			{
				Stroke stroke = base[i];
				StrokeCollection toReplace = stroke.Clip(stroke.HitTest(lasso));
				this.UpdateStrokeCollection(stroke, toReplace, ref i);
			}
		}

		/// <summary>Substitui todos os traços que estão recortados pelo retângulo especificado por novos traços que não saem dos limites do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que especifica a área a ser recortada.</param>
		// Token: 0x06001D5F RID: 7519 RVA: 0x00078450 File Offset: 0x00077850
		public void Clip(Rect bounds)
		{
			if (!bounds.IsEmpty)
			{
				this.Clip(new Point[]
				{
					bounds.TopLeft,
					bounds.TopRight,
					bounds.BottomRight,
					bounds.BottomLeft
				});
			}
		}

		/// <summary>Remove a tinta que está dentro dos limites da área especificada.</summary>
		/// <param name="lassoPoints">Uma matriz do tipo <see cref="T:System.Drawing.Point" /> que especifica a área a ser apagada.</param>
		// Token: 0x06001D60 RID: 7520 RVA: 0x000784AC File Offset: 0x000778AC
		public void Erase(IEnumerable<Point> lassoPoints)
		{
			if (lassoPoints == null)
			{
				throw new ArgumentNullException("lassoPoints");
			}
			int count = IEnumerablePointHelper.GetCount(lassoPoints);
			if (count == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArray"));
			}
			if (count < 3)
			{
				return;
			}
			Lasso lasso = new SingleLoopLasso();
			lasso.AddPoints(lassoPoints);
			for (int i = 0; i < base.Count; i++)
			{
				Stroke stroke = base[i];
				StrokeCollection toReplace = stroke.Erase(stroke.HitTest(lasso));
				this.UpdateStrokeCollection(stroke, toReplace, ref i);
			}
		}

		/// <summary>Substitui todos os traços que estão recortados pelo retângulo especificado por novos traçados que não adentram os limites do retângulo especificado.</summary>
		/// <param name="bounds">Um <see cref="T:System.Windows.Rect" /> que especifica a área a ser apagada.</param>
		// Token: 0x06001D61 RID: 7521 RVA: 0x00078528 File Offset: 0x00077928
		public void Erase(Rect bounds)
		{
			if (!bounds.IsEmpty)
			{
				this.Erase(new Point[]
				{
					bounds.TopLeft,
					bounds.TopRight,
					bounds.BottomRight,
					bounds.BottomLeft
				});
			}
		}

		/// <summary>Substitui todos os traços que estão recortados pela região criada pelo <see cref="T:System.Windows.Ink.StylusShape" /> especificado junto do caminho especificado com novos Traços que não são recortados por região.</summary>
		/// <param name="eraserPath">Uma matriz do tipo <see cref="T:System.Windows.Point" /> que especifica o caminho a ser apagado.</param>
		/// <param name="eraserShape">Um <see cref="T:System.Windows.Ink.StylusShape" /> que especifica a forma da borracha.</param>
		// Token: 0x06001D62 RID: 7522 RVA: 0x00078584 File Offset: 0x00077984
		public void Erase(IEnumerable<Point> eraserPath, StylusShape eraserShape)
		{
			if (eraserShape == null)
			{
				throw new ArgumentNullException(SR.Get("SCEraseShape"));
			}
			if (eraserPath == null)
			{
				throw new ArgumentNullException(SR.Get("SCErasePath"));
			}
			if (IEnumerablePointHelper.GetCount(eraserPath) == 0)
			{
				return;
			}
			ErasingStroke erasingStroke = new ErasingStroke(eraserShape, eraserPath);
			for (int i = 0; i < base.Count; i++)
			{
				Stroke stroke = base[i];
				List<StrokeIntersection> list = new List<StrokeIntersection>();
				erasingStroke.EraseTest(StrokeNodeIterator.GetIterator(stroke, stroke.DrawingAttributes), list);
				StrokeCollection toReplace = stroke.Erase(list.ToArray());
				this.UpdateStrokeCollection(stroke, toReplace, ref i);
			}
		}

		/// <summary>Desenha os traços no <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <param name="context">A <see cref="T:System.Windows.Media.DrawingContext" /> na qual desenhar o <see cref="T:System.Windows.Ink.StrokeCollection" />.</param>
		// Token: 0x06001D63 RID: 7523 RVA: 0x00078614 File Offset: 0x00077A14
		public void Draw(DrawingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			List<Stroke> list = new List<Stroke>();
			Dictionary<Color, List<Stroke>> dictionary = new Dictionary<Color, List<Stroke>>();
			for (int i = 0; i < base.Count; i++)
			{
				Stroke stroke = base[i];
				if (stroke.DrawingAttributes.IsHighlighter)
				{
					Color highlighterColor = StrokeRenderer.GetHighlighterColor(stroke.DrawingAttributes.Color);
					List<Stroke> list2;
					if (!dictionary.TryGetValue(highlighterColor, out list2))
					{
						list2 = new List<Stroke>();
						dictionary.Add(highlighterColor, list2);
					}
					list2.Add(stroke);
				}
				else
				{
					list.Add(stroke);
				}
			}
			foreach (List<Stroke> list3 in dictionary.Values)
			{
				context.PushOpacity(StrokeRenderer.HighlighterOpacity);
				try
				{
					foreach (Stroke stroke2 in list3)
					{
						stroke2.DrawInternal(context, StrokeRenderer.GetHighlighterAttributes(stroke2, stroke2.DrawingAttributes), false);
					}
				}
				finally
				{
					context.Pop();
				}
			}
			foreach (Stroke stroke3 in list)
			{
				stroke3.DrawInternal(context, stroke3.DrawingAttributes, false);
			}
		}

		/// <summary>Cria um <see cref="T:System.Windows.Ink.IncrementalStrokeHitTester" /> que testa os cliques do <see cref="T:System.Windows.Ink.StrokeCollection" /> com um caminho de apagamento.</summary>
		/// <param name="eraserShape">Um <see cref="T:System.Windows.Ink.StylusShape" /> que especifica a ponta da caneta.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.IncrementalStrokeHitTester" /> que testa os clique de <see cref="T:System.Windows.Ink.StrokeCollection" />.</returns>
		// Token: 0x06001D64 RID: 7524 RVA: 0x000787CC File Offset: 0x00077BCC
		public IncrementalStrokeHitTester GetIncrementalStrokeHitTester(StylusShape eraserShape)
		{
			if (eraserShape == null)
			{
				throw new ArgumentNullException("eraserShape");
			}
			return new IncrementalStrokeHitTester(this, eraserShape);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Ink.IncrementalLassoHitTester" /> que testa os cliques do <see cref="T:System.Windows.Ink.StrokeCollection" /> com um caminho de laço (à mão livre).</summary>
		/// <param name="percentageWithinLasso">O percentual mínimo de cada <see cref="T:System.Windows.Ink.Stroke" /> que deve estar contido no laço para ser considerado uma ocorrência.</param>
		/// <returns>Um <see cref="T:System.Windows.Ink.IncrementalLassoHitTester" /> que testa os clique de <see cref="T:System.Windows.Ink.StrokeCollection" />.</returns>
		// Token: 0x06001D65 RID: 7525 RVA: 0x000787F0 File Offset: 0x00077BF0
		public IncrementalLassoHitTester GetIncrementalLassoHitTester(int percentageWithinLasso)
		{
			if (percentageWithinLasso < 0 || percentageWithinLasso > 100)
			{
				throw new ArgumentOutOfRangeException("percentageWithinLasso");
			}
			return new IncrementalLassoHitTester(this, percentageWithinLasso);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00078818 File Offset: 0x00077C18
		private StrokeCollection PointHitTest(Point point, StylusShape shape)
		{
			StrokeCollection strokeCollection = new StrokeCollection();
			for (int i = 0; i < base.Count; i++)
			{
				Stroke stroke = base[i];
				if (stroke.HitTest(new Point[]
				{
					point
				}, shape))
				{
					strokeCollection.Add(stroke);
				}
			}
			return strokeCollection;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00078864 File Offset: 0x00077C64
		private void UpdateStrokeCollection(Stroke original, StrokeCollection toReplace, ref int index)
		{
			if (toReplace.Count == 0)
			{
				base.Remove(original);
				index--;
				return;
			}
			if (toReplace.Count != 1 || toReplace[0] != original)
			{
				this.Replace(original, toReplace);
				index += toReplace.Count - 1;
			}
		}

		/// <summary>Representa o formato de persistência nativo para dados de tinta.</summary>
		// Token: 0x04000FBF RID: 4031
		public static readonly string InkSerializedFormat = "Ink Serialized Format";

		// Token: 0x04000FC3 RID: 4035
		private ExtendedPropertyCollection _extendedProperties;

		// Token: 0x04000FC4 RID: 4036
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x04000FC5 RID: 4037
		private NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x04000FC6 RID: 4038
		private const string IndexerName = "Item[]";

		// Token: 0x04000FC7 RID: 4039
		private const string CountName = "Count";

		// Token: 0x02000854 RID: 2132
		internal class ReadOnlyStrokeCollection : StrokeCollection, ICollection<Stroke>, IEnumerable<Stroke>, IEnumerable, IList, ICollection
		{
			// Token: 0x06005708 RID: 22280 RVA: 0x001644A0 File Offset: 0x001638A0
			internal ReadOnlyStrokeCollection(StrokeCollection strokeCollection)
			{
				if (strokeCollection != null)
				{
					((List<Stroke>)base.Items).AddRange(strokeCollection);
				}
			}

			// Token: 0x06005709 RID: 22281 RVA: 0x001644C8 File Offset: 0x001638C8
			protected override void OnStrokesChanged(StrokeCollectionChangedEventArgs e)
			{
				throw new NotSupportedException(SR.Get("StrokeCollectionIsReadOnly"));
			}

			// Token: 0x170011E9 RID: 4585
			// (get) Token: 0x0600570A RID: 22282 RVA: 0x001644E4 File Offset: 0x001638E4
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170011EA RID: 4586
			// (get) Token: 0x0600570B RID: 22283 RVA: 0x001644F4 File Offset: 0x001638F4
			bool ICollection<Stroke>.IsReadOnly
			{
				get
				{
					return true;
				}
			}
		}
	}
}
