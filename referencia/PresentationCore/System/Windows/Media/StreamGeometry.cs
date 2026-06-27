using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Define uma forma geométrica, descrita usando um <see cref="T:System.Windows.Media.StreamGeometryContext" />. Esta geometria é a alternativa mais leve à <see cref="T:System.Windows.Media.PathGeometry" />: ela não dá suporte à vinculação, animação ou modificação de dados.</summary>
	// Token: 0x020003F0 RID: 1008
	[TypeConverter(typeof(GeometryConverter))]
	public sealed class StreamGeometry : Geometry
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.StreamGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600279A RID: 10138 RVA: 0x0009F6F0 File Offset: 0x0009EAF0
		public new StreamGeometry Clone()
		{
			return (StreamGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.StreamGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600279B RID: 10139 RVA: 0x0009F708 File Offset: 0x0009EB08
		public new StreamGeometry CloneCurrentValue()
		{
			return (StreamGeometry)base.CloneCurrentValue();
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0009F720 File Offset: 0x0009EB20
		private static void FillRulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			StreamGeometry streamGeometry = (StreamGeometry)d;
			streamGeometry.PropertyChanged(StreamGeometry.FillRuleProperty);
		}

		/// <summary>Obtém ou define um valor que determina como as áreas de interseção contidas neste <see cref="T:System.Windows.Media.StreamGeometry" /> são combinadas.</summary>
		/// <returns>Indica como as áreas de interseção desse <see cref="T:System.Windows.Media.StreamGeometry" /> são combinadas.  O valor padrão é EvenOdd.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x0009F740 File Offset: 0x0009EB40
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x0009F760 File Offset: 0x0009EB60
		public FillRule FillRule
		{
			get
			{
				return (FillRule)base.GetValue(StreamGeometry.FillRuleProperty);
			}
			set
			{
				base.SetValueInternal(StreamGeometry.FillRuleProperty, FillRuleBoxes.Box(value));
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0009F780 File Offset: 0x0009EB80
		protected override Freezable CreateInstanceCore()
		{
			return new StreamGeometry();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0009F794 File Offset: 0x0009EB94
		static StreamGeometry()
		{
			Type typeFromHandle = typeof(StreamGeometry);
			StreamGeometry.FillRuleProperty = Animatable.RegisterProperty("FillRule", typeof(FillRule), typeFromHandle, FillRule.EvenOdd, new PropertyChangedCallback(StreamGeometry.FillRulePropertyChanged), new ValidateValueCallback(ValidateEnums.IsFillRuleValid), false, null);
		}

		/// <summary>Abre um <see cref="T:System.Windows.Media.StreamGeometryContext" /> que pode ser usado para descrever o conteúdo deste objeto <see cref="T:System.Windows.Media.StreamGeometry" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.StreamGeometryContext" /> que pode ser usado para descrever o conteúdo deste objeto <see cref="T:System.Windows.Media.StreamGeometry" />.</returns>
		// Token: 0x060027A2 RID: 10146 RVA: 0x0009F7FC File Offset: 0x0009EBFC
		public StreamGeometryContext Open()
		{
			base.WritePreamble();
			return new StreamGeometryCallbackContext(this);
		}

		/// <summary>Remove todas as informações geométricas deste <see cref="T:System.Windows.Media.StreamGeometry" />.</summary>
		// Token: 0x060027A3 RID: 10147 RVA: 0x0009F818 File Offset: 0x0009EC18
		public void Clear()
		{
			base.WritePreamble();
			this._data = null;
			this.SetDirty();
			base.RegisterForAsyncUpdateResource();
		}

		/// <summary>Determina se este <see cref="T:System.Windows.Media.StreamGeometry" /> descreve uma forma geométrica.</summary>
		/// <returns>
		///   <see langword="true" /> se esta <see cref="T:System.Windows.Media.StreamGeometry" /> descrever uma forma geométrica; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060027A4 RID: 10148 RVA: 0x0009F840 File Offset: 0x0009EC40
		[SecurityCritical]
		public unsafe override bool IsEmpty()
		{
			base.ReadPreamble();
			if (this._data == null || this._data.Length == 0)
			{
				return true;
			}
			Invariant.Assert(this._data != null && this._data.Length >= sizeof(MIL_PATHGEOMETRY));
			byte[] data;
			byte* ptr;
			if ((data = this._data) == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			return ptr2->FigureCount <= 0U;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0009F8B4 File Offset: 0x0009ECB4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe bool AreBoundsValid(ref MilRectD bounds)
		{
			if (this.IsEmpty())
			{
				return false;
			}
			byte[] data;
			byte* ptr;
			if ((data = this._data) == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			bool flag = (ptr2->Flags & MilPathGeometryFlags.BoundsValid) > (MilPathGeometryFlags)0;
			if (flag)
			{
				bounds = ptr2->Bounds;
			}
			return flag;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0009F908 File Offset: 0x0009ED08
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void CacheBounds(ref MilRectD bounds)
		{
			byte[] array;
			byte* ptr;
			if ((array = this._data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			ptr2->Flags |= MilPathGeometryFlags.BoundsValid;
			ptr2->Bounds = bounds;
			array = null;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0009F950 File Offset: 0x0009ED50
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void SetDirty()
		{
			if (!this.IsEmpty())
			{
				byte[] array;
				byte* ptr;
				if ((array = this._data) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
				ptr2->Flags &= ~MilPathGeometryFlags.BoundsValid;
				array = null;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> exatamente grande o suficiente para conter este <see cref="T:System.Windows.Media.StreamGeometry" />.</summary>
		/// <returns>A caixa delimitadora deste <see cref="T:System.Windows.Media.StreamGeometry" />.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0009F994 File Offset: 0x0009ED94
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				if (this.IsEmpty())
				{
					return Rect.Empty;
				}
				MilRectD milRectD = default(MilRectD);
				if (!this.AreBoundsValid(ref milRectD))
				{
					milRectD = PathGeometry.GetPathBoundsAsRB(this.GetPathGeometryData(), null, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute, false);
					this.CacheBounds(ref milRectD);
				}
				return milRectD.AsRect;
			}
		}

		/// <summary>Determina se este <see cref="T:System.Windows.Media.StreamGeometry" /> contém um segmento curvado.</summary>
		/// <returns>
		///   <see langword="true" /> se este objeto <see cref="T:System.Windows.Media.StreamGeometry" /> tiver um segmento curvado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060027A9 RID: 10153 RVA: 0x0009F9F0 File Offset: 0x0009EDF0
		[SecurityCritical]
		public unsafe override bool MayHaveCurves()
		{
			if (this.IsEmpty())
			{
				return false;
			}
			Invariant.Assert(this._data != null && this._data.Length >= sizeof(MIL_PATHGEOMETRY));
			byte[] data;
			byte* ptr;
			if ((data = this._data) == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			return (ptr2->Flags & MilPathGeometryFlags.HasCurves) > (MilPathGeometryFlags)0;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0009FA54 File Offset: 0x0009EE54
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe bool HasHollows()
		{
			if (this.IsEmpty())
			{
				return false;
			}
			Invariant.Assert(this._data != null && this._data.Length >= sizeof(MIL_PATHGEOMETRY));
			byte[] data;
			byte* ptr;
			if ((data = this._data) == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			return (ptr2->Flags & MilPathGeometryFlags.HasHollows) > (MilPathGeometryFlags)0;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0009FAB8 File Offset: 0x0009EEB8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe bool HasGaps()
		{
			if (this.IsEmpty())
			{
				return false;
			}
			Invariant.Assert(this._data != null && this._data.Length >= sizeof(MIL_PATHGEOMETRY));
			byte[] data;
			byte* ptr;
			if ((data = this._data) == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			return (ptr2->Flags & MilPathGeometryFlags.HasGaps) > (MilPathGeometryFlags)0;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0009FB1C File Offset: 0x0009EF1C
		internal void Close(byte[] _buffer)
		{
			this.SetDirty();
			this._data = _buffer;
			base.RegisterForAsyncUpdateResource();
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0009FB3C File Offset: 0x0009EF3C
		protected override void OnChanged()
		{
			this.SetDirty();
			base.OnChanged();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0009FB58 File Offset: 0x0009EF58
		internal override PathGeometry GetAsPathGeometry()
		{
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext(this.FillRule, base.Transform);
			PathGeometry.ParsePathGeometryData(this.GetPathGeometryData(), pathStreamGeometryContext);
			return pathStreamGeometryContext.GetPathGeometry();
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0009FB8C File Offset: 0x0009EF8C
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			PathGeometry asPathGeometry = this.GetAsPathGeometry();
			if (asPathGeometry != null)
			{
				return asPathGeometry.GetTransformedFigureCollection(transform);
			}
			return null;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x0009FBAC File Offset: 0x0009EFAC
		internal override bool CanSerializeToString()
		{
			Transform transform = base.Transform;
			return (transform == null || transform.IsIdentity) && !this.HasHollows() && !this.HasGaps();
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0009FBE0 File Offset: 0x0009EFE0
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			return this.GetAsPathGeometry().ConvertToString(format, provider);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0009FBFC File Offset: 0x0009EFFC
		private void InvalidateResourceFigures(object sender, EventArgs args)
		{
			this.SetDirty();
			base.RegisterForAsyncUpdateResource();
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0009FC18 File Offset: 0x0009F018
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			if (this.IsEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			return new Geometry.PathGeometryData
			{
				FillRule = this.FillRule,
				Matrix = CompositionResourceManager.TransformToMilMatrix3x2D(base.Transform),
				SerializedData = this._data
			};
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x0009FC68 File Offset: 0x0009F068
		internal override void TransformPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			this.SetDirty();
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0009FC7C File Offset: 0x0009F07C
		[SecurityCritical]
		private unsafe int GetFigureSize(byte* pbPathData)
		{
			if (pbPathData != null)
			{
				return (int)((MIL_PATHGEOMETRY*)pbPathData)->Size;
			}
			return 0;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0009FC98 File Offset: 0x0009F098
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			checked
			{
				if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
				{
					Transform transform = base.Transform;
					DUCE.ResourceHandle hTransform;
					if (transform == null || transform == Transform.Identity)
					{
						hTransform = DUCE.ResourceHandle.Null;
					}
					else
					{
						hTransform = ((DUCE.IResource)transform).GetHandle(channel);
					}
					DUCE.MILCMD_PATHGEOMETRY milcmd_PATHGEOMETRY;
					milcmd_PATHGEOMETRY.Type = MILCMD.MilCmdPathGeometry;
					milcmd_PATHGEOMETRY.Handle = this._duceResource.GetHandle(channel);
					milcmd_PATHGEOMETRY.hTransform = hTransform;
					milcmd_PATHGEOMETRY.FillRule = this.FillRule;
					byte[] array = (this._data == null) ? Geometry.GetEmptyPathGeometryData().SerializedData : this._data;
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					milcmd_PATHGEOMETRY.FiguresSize = (uint)this.GetFigureSize(ptr);
					channel.BeginCommand(unchecked((byte*)(&milcmd_PATHGEOMETRY)), sizeof(DUCE.MILCMD_PATHGEOMETRY), (int)milcmd_PATHGEOMETRY.FiguresSize);
					channel.AppendCommandData(ptr, (int)milcmd_PATHGEOMETRY.FiguresSize);
					array2 = null;
					channel.EndCommand();
				}
				base.UpdateResource(channel, skipOnChannelCheck);
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0009FD88 File Offset: 0x0009F188
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_PATHGEOMETRY))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0009FDCC File Offset: 0x0009F1CC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0009FDF8 File Offset: 0x0009F1F8
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0009FE14 File Offset: 0x0009F214
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0009FE2C File Offset: 0x0009F22C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0009FE48 File Offset: 0x0009F248
		protected override void CloneCore(Freezable source)
		{
			base.CloneCore(source);
			StreamGeometry streamGeometry = (StreamGeometry)source;
			if (streamGeometry._data != null && streamGeometry._data.Length != 0)
			{
				this._data = new byte[streamGeometry._data.Length];
				streamGeometry._data.CopyTo(this._data, 0);
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0009FE9C File Offset: 0x0009F29C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			base.CloneCurrentValueCore(source);
			StreamGeometry streamGeometry = (StreamGeometry)source;
			if (streamGeometry._data != null && streamGeometry._data.Length != 0)
			{
				this._data = new byte[streamGeometry._data.Length];
				streamGeometry._data.CopyTo(this._data, 0);
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0009FEF0 File Offset: 0x0009F2F0
		protected override void GetAsFrozenCore(Freezable source)
		{
			base.GetAsFrozenCore(source);
			StreamGeometry streamGeometry = (StreamGeometry)source;
			if (streamGeometry._data != null && streamGeometry._data.Length != 0)
			{
				this._data = new byte[streamGeometry._data.Length];
				streamGeometry._data.CopyTo(this._data, 0);
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0009FF44 File Offset: 0x0009F344
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			base.GetCurrentValueAsFrozenCore(source);
			StreamGeometry streamGeometry = (StreamGeometry)source;
			if (streamGeometry._data != null && streamGeometry._data.Length != 0)
			{
				this._data = new byte[streamGeometry._data.Length];
				streamGeometry._data.CopyTo(this._data, 0);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.StreamGeometry.FillRule" />.</summary>
		// Token: 0x04001260 RID: 4704
		public static readonly DependencyProperty FillRuleProperty;

		// Token: 0x04001261 RID: 4705
		internal const FillRule c_FillRule = FillRule.EvenOdd;

		// Token: 0x04001262 RID: 4706
		private byte[] _data;

		// Token: 0x04001263 RID: 4707
		internal DUCE.MultiChannelResource _duceResource;
	}
}
