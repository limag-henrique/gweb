using System;
using System.Collections.Generic;
using System.Windows.Input;
using MS.Internal.Ink;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Executa dinamicamente o teste de clique em um <see cref="T:System.Windows.Ink.Stroke" />.</summary>
	// Token: 0x02000352 RID: 850
	public abstract class IncrementalHitTester
	{
		/// <summary>Adiciona um <see cref="T:System.Windows.Point" /> ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="point">O <see cref="T:System.Windows.Point" /> para adicionar ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</param>
		// Token: 0x06001CBB RID: 7355 RVA: 0x00074C14 File Offset: 0x00074014
		public void AddPoint(Point point)
		{
			this.AddPoints(new Point[]
			{
				point
			});
		}

		/// <summary>Adiciona pontos ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="points">Uma matriz do tipo <see cref="T:System.Windows.Point" /> a ser adicionada ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</param>
		// Token: 0x06001CBC RID: 7356 RVA: 0x00074C38 File Offset: 0x00074038
		public void AddPoints(IEnumerable<Point> points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			if (IEnumerablePointHelper.GetCount(points) == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArrayNotAllowedAsArgument"), "points");
			}
			if (!this._fValid)
			{
				throw new InvalidOperationException(SR.Get("EndHitTestingCalled"));
			}
			this.AddPointsCore(points);
		}

		/// <summary>Adiciona os objetos <see cref="T:System.Windows.Input.StylusPoint" /> especificados ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="stylusPoints">Uma coleção de objetos <see cref="T:System.Windows.Input.StylusPoint" /> a serem adicionados ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</param>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00074C90 File Offset: 0x00074090
		public void AddPoints(StylusPointCollection stylusPoints)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (stylusPoints.Count == 0)
			{
				throw new ArgumentException(SR.Get("EmptyArrayNotAllowedAsArgument"), "stylusPoints");
			}
			if (!this._fValid)
			{
				throw new InvalidOperationException(SR.Get("EndHitTestingCalled"));
			}
			Point[] array = new Point[stylusPoints.Count];
			for (int i = 0; i < stylusPoints.Count; i++)
			{
				array[i] = (Point)stylusPoints[i];
			}
			this.AddPointsCore(array);
		}

		/// <summary>Libera recursos usados pelo <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		// Token: 0x06001CBE RID: 7358 RVA: 0x00074D18 File Offset: 0x00074118
		public void EndHitTesting()
		{
			if (this._strokes != null)
			{
				this._strokes.StrokesChangedInternal -= this.OnStrokesChanged;
				this._strokes = null;
				int count = this._strokeInfos.Count;
				for (int i = 0; i < count; i++)
				{
					this._strokeInfos[i].Detach();
				}
				this._strokeInfos = null;
			}
			this._fValid = false;
		}

		/// <summary>Obtém se o <see cref="T:System.Windows.Ink.IncrementalHitTester" /> está realizando teste de clique.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Ink.IncrementalHitTester" /> for atingido teste; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00074D84 File Offset: 0x00074184
		public bool IsValid
		{
			get
			{
				return this._fValid;
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00074D98 File Offset: 0x00074198
		internal IncrementalHitTester(StrokeCollection strokes)
		{
			this._strokeInfos = new List<StrokeInfo>(strokes.Count);
			for (int i = 0; i < strokes.Count; i++)
			{
				Stroke stroke = strokes[i];
				this._strokeInfos.Add(new StrokeInfo(stroke));
			}
			this._strokes = strokes;
			this._strokes.StrokesChangedInternal += this.OnStrokesChanged;
		}

		/// <summary>Adiciona pontos ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="points">Os pontos a adicionar</param>
		// Token: 0x06001CC1 RID: 7361
		protected abstract void AddPointsCore(IEnumerable<Point> points);

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00074E0C File Offset: 0x0007420C
		internal List<StrokeInfo> StrokeInfos
		{
			get
			{
				return this._strokeInfos;
			}
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00074E20 File Offset: 0x00074220
		private void OnStrokesChanged(object sender, StrokeCollectionChangedEventArgs args)
		{
			StrokeCollection added = args.Added;
			StrokeCollection removed = args.Removed;
			if (added.Count > 0)
			{
				int num = this._strokes.IndexOf(added[0]);
				for (int i = 0; i < added.Count; i++)
				{
					this._strokeInfos.Insert(num, new StrokeInfo(added[i]));
					num++;
				}
			}
			if (removed.Count > 0)
			{
				StrokeCollection strokeCollection = new StrokeCollection(removed);
				int num2 = 0;
				while (num2 < this._strokeInfos.Count && strokeCollection.Count > 0)
				{
					bool flag = false;
					for (int j = 0; j < strokeCollection.Count; j++)
					{
						if (strokeCollection[j] == this._strokeInfos[num2].Stroke)
						{
							this._strokeInfos.RemoveAt(num2);
							strokeCollection.RemoveAt(j);
							flag = true;
						}
					}
					if (!flag)
					{
						num2++;
					}
				}
			}
			if (this._strokes.Count != this._strokeInfos.Count)
			{
				this.RebuildStrokeInfoCache();
				return;
			}
			for (int k = 0; k < this._strokeInfos.Count; k++)
			{
				if (this._strokeInfos[k].Stroke != this._strokes[k])
				{
					this.RebuildStrokeInfoCache();
					return;
				}
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00074F68 File Offset: 0x00074368
		private void RebuildStrokeInfoCache()
		{
			List<StrokeInfo> list = new List<StrokeInfo>(this._strokes.Count);
			foreach (Stroke stroke in this._strokes)
			{
				bool flag = false;
				for (int i = 0; i < this._strokeInfos.Count; i++)
				{
					StrokeInfo strokeInfo = this._strokeInfos[i];
					if (strokeInfo != null && stroke == strokeInfo.Stroke)
					{
						list.Add(strokeInfo);
						this._strokeInfos[i] = null;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(new StrokeInfo(stroke));
				}
			}
			for (int j = 0; j < this._strokeInfos.Count; j++)
			{
				StrokeInfo strokeInfo2 = this._strokeInfos[j];
				if (strokeInfo2 != null)
				{
					strokeInfo2.Detach();
				}
			}
			this._strokeInfos = list;
		}

		// Token: 0x04000F99 RID: 3993
		private StrokeCollection _strokes;

		// Token: 0x04000F9A RID: 3994
		private List<StrokeInfo> _strokeInfos;

		// Token: 0x04000F9B RID: 3995
		private bool _fValid = true;
	}
}
