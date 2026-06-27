using System;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Representa um botão em uma caneta.</summary>
	// Token: 0x020002AC RID: 684
	public class StylusButton
	{
		// Token: 0x06001431 RID: 5169 RVA: 0x0004B294 File Offset: 0x0004A694
		internal StylusButton(string name, Guid id)
		{
			this._name = name;
			this._guid = id;
		}

		/// <summary>Obtém o <see cref="T:System.Guid" /> que representa o botão da caneta.</summary>
		/// <returns>O <see cref="T:System.Guid" /> propriedade que representa o botão da caneta.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0004B2B8 File Offset: 0x0004A6B8
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		/// <summary>Obtém o estado do botão da caneta.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.Input.StylusButtonState" />.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0004B2CC File Offset: 0x0004A6CC
		public StylusButtonState StylusButtonState
		{
			get
			{
				StylusPointCollection stylusPoints = this._stylusDevice.GetStylusPoints(null);
				if (stylusPoints == null || stylusPoints.Count == 0)
				{
					return this.CachedButtonState;
				}
				return (StylusButtonState)stylusPoints[stylusPoints.Count - 1].GetPropertyValue(new StylusPointProperty(this.Guid, true));
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0004B31C File Offset: 0x0004A71C
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0004B330 File Offset: 0x0004A730
		internal StylusButtonState CachedButtonState
		{
			get
			{
				return this._cachedButtonState;
			}
			set
			{
				this._cachedButtonState = value;
			}
		}

		/// <summary>Obtém o nome do botão da caneta.</summary>
		/// <returns>O nome do botão da caneta.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0004B344 File Offset: 0x0004A744
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Obtém a caneta à qual este botão pertence.</summary>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusDevice" /> que representa a caneta atual <see cref="T:System.Windows.Input.StylusButton" />.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004B358 File Offset: 0x0004A758
		public StylusDevice StylusDevice
		{
			get
			{
				return this._stylusDevice.StylusDevice;
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004B370 File Offset: 0x0004A770
		internal void SetOwner(StylusDeviceBase stylusDevice)
		{
			this._stylusDevice = stylusDevice;
		}

		/// <summary>Cria uma representação de cadeia de caracteres do <see cref="T:System.Windows.Input.StylusButton" />.</summary>
		// Token: 0x06001439 RID: 5177 RVA: 0x0004B384 File Offset: 0x0004A784
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				base.ToString(),
				this.Name
			});
		}

		// Token: 0x04000AF5 RID: 2805
		private StylusDeviceBase _stylusDevice;

		// Token: 0x04000AF6 RID: 2806
		private string _name;

		// Token: 0x04000AF7 RID: 2807
		private Guid _guid;

		// Token: 0x04000AF8 RID: 2808
		private StylusButtonState _cachedButtonState;
	}
}
