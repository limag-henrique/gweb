using System;
using System.Security;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input.StylusPlugIns
{
	/// <summary>Fornece informações sobre a entrada de um <see cref="T:System.Windows.Input.StylusDevice" /> para um <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />.</summary>
	// Token: 0x020002F7 RID: 759
	public class RawStylusInput
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x00061300 File Offset: 0x00060700
		internal RawStylusInput(RawStylusInputReport report, GeneralTransform tabletToElementTransform, StylusPlugInCollection targetPlugInCollection)
		{
			if (report == null)
			{
				throw new ArgumentNullException("report");
			}
			if (tabletToElementTransform.Inverse == null)
			{
				throw new ArgumentException(SR.Get("Stylus_MatrixNotInvertable"), "tabletToElementTransform");
			}
			if (targetPlugInCollection == null)
			{
				throw new ArgumentNullException("targetPlugInCollection");
			}
			this._report = report;
			this._tabletToElementTransform = tabletToElementTransform;
			this._targetPlugInCollection = targetPlugInCollection;
		}

		/// <summary>Obtém o identificador do dispositivo de caneta atual.</summary>
		/// <returns>O identificador do atual <see cref="T:System.Windows.Input.StylusDevice" />.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00061364 File Offset: 0x00060764
		public int StylusDeviceId
		{
			get
			{
				return this._report.StylusDeviceId;
			}
		}

		/// <summary>Obtém o identificador do dispositivo de tablet atual.</summary>
		/// <returns>O identificador do atual <see cref="T:System.Windows.Input.TabletDevice" />.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0006137C File Offset: 0x0006077C
		public int TabletDeviceId
		{
			get
			{
				return this._report.TabletDeviceId;
			}
		}

		/// <summary>Obtém a hora em que o processo foi iniciado.</summary>
		/// <returns>A hora em que ocorreu a entrada.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00061394 File Offset: 0x00060794
		public int Timestamp
		{
			get
			{
				return this._report.Timestamp;
			}
		}

		/// <summary>Obtém os pontos de caneta que são coletados da caneta.</summary>
		/// <returns>Os pontos de caneta que são coletados da caneta.</returns>
		// Token: 0x0600182E RID: 6190 RVA: 0x000613AC File Offset: 0x000607AC
		public StylusPointCollection GetStylusPoints()
		{
			return this.GetStylusPoints(Transform.Identity);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000613C4 File Offset: 0x000607C4
		[SecuritySafeCritical]
		internal StylusPointCollection GetStylusPoints(GeneralTransform transform)
		{
			if (this._stylusPoints == null)
			{
				GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
				if (this.StylusDeviceId == 0)
				{
					generalTransformGroup.Children.Add(new MatrixTransform(this._report.InputSource.CompositionTarget.TransformFromDevice));
				}
				generalTransformGroup.Children.Add(this._tabletToElementTransform);
				generalTransformGroup.Children.Add(transform);
				return new StylusPointCollection(this._report.StylusPointDescription, this._report.GetRawPacketData(), generalTransformGroup, Matrix.Identity);
			}
			return this._stylusPoints.Clone(transform, this._stylusPoints.Description);
		}

		/// <summary>Define os pontos de caneta que são passados para o thread de aplicativo.</summary>
		/// <param name="stylusPoints">Os pontos de caneta a serem passados para o thread de aplicativo.</param>
		// Token: 0x06001830 RID: 6192 RVA: 0x00061464 File Offset: 0x00060864
		[SecuritySafeCritical]
		public void SetStylusPoints(StylusPointCollection stylusPoints)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (!StylusPointDescription.AreCompatible(stylusPoints.Description, this._report.StylusPointDescription))
			{
				throw new ArgumentException(SR.Get("IncompatibleStylusPointDescriptions"), "stylusPoints");
			}
			if (stylusPoints.Count == 0)
			{
				throw new ArgumentException(SR.Get("Stylus_StylusPointsCantBeEmpty"), "stylusPoints");
			}
			this._stylusPoints = stylusPoints.Clone();
		}

		/// <summary>Assina os métodos de caneta correspondentes do thread do aplicativo.</summary>
		/// <param name="callbackData">Os dados a serem passados para o thread de aplicativo.</param>
		// Token: 0x06001831 RID: 6193 RVA: 0x000614DC File Offset: 0x000608DC
		public void NotifyWhenProcessed(object callbackData)
		{
			if (this._currentNotifyPlugIn == null)
			{
				throw new InvalidOperationException(SR.Get("Stylus_CanOnlyCallForDownMoveOrUp"));
			}
			if (this._customData == null)
			{
				this._customData = new RawStylusInputCustomDataList();
			}
			this._customData.Add(new RawStylusInputCustomData(this._currentNotifyPlugIn, callbackData));
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0006152C File Offset: 0x0006092C
		internal bool StylusPointsModified
		{
			get
			{
				return this._stylusPoints != null;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00061544 File Offset: 0x00060944
		internal StylusPlugInCollection Target
		{
			get
			{
				return this._targetPlugInCollection;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00061558 File Offset: 0x00060958
		internal RawStylusInputReport Report
		{
			get
			{
				return this._report;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0006156C File Offset: 0x0006096C
		internal GeneralTransform ElementTransform
		{
			get
			{
				return this._tabletToElementTransform;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00061580 File Offset: 0x00060980
		internal RawStylusInputCustomDataList CustomDataList
		{
			get
			{
				if (this._customData == null)
				{
					this._customData = new RawStylusInputCustomDataList();
				}
				return this._customData;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000615A8 File Offset: 0x000609A8
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x000615BC File Offset: 0x000609BC
		internal StylusPlugIn CurrentNotifyPlugIn
		{
			get
			{
				return this._currentNotifyPlugIn;
			}
			set
			{
				this._currentNotifyPlugIn = value;
			}
		}

		// Token: 0x04000D2A RID: 3370
		private RawStylusInputReport _report;

		// Token: 0x04000D2B RID: 3371
		private GeneralTransform _tabletToElementTransform;

		// Token: 0x04000D2C RID: 3372
		private StylusPlugInCollection _targetPlugInCollection;

		// Token: 0x04000D2D RID: 3373
		private StylusPointCollection _stylusPoints;

		// Token: 0x04000D2E RID: 3374
		private StylusPlugIn _currentNotifyPlugIn;

		// Token: 0x04000D2F RID: 3375
		private RawStylusInputCustomDataList _customData;
	}
}
