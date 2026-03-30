using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Especifica as restrições de uma propriedade em um <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
	// Token: 0x020002BF RID: 703
	public class StylusPointPropertyInfo : StylusPointProperty
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointPropertyInfo" />.</summary>
		/// <param name="stylusPointProperty">O <see cref="T:System.Windows.Input.StylusPointProperty" /> no qual basear o novo <see cref="T:System.Windows.Input.StylusPointProperty" />.</param>
		// Token: 0x06001501 RID: 5377 RVA: 0x0004E1F0 File Offset: 0x0004D5F0
		public StylusPointPropertyInfo(StylusPointProperty stylusPointProperty) : base(stylusPointProperty)
		{
			StylusPointPropertyInfo stylusPointPropertyInfoDefault = StylusPointPropertyInfoDefaults.GetStylusPointPropertyInfoDefault(stylusPointProperty);
			this._min = stylusPointPropertyInfoDefault.Minimum;
			this._max = stylusPointPropertyInfoDefault.Maximum;
			this._resolution = stylusPointPropertyInfoDefault.Resolution;
			this._unit = stylusPointPropertyInfoDefault.Unit;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> usando os valores especificados.</summary>
		/// <param name="stylusPointProperty">O <see cref="T:System.Windows.Input.StylusPointProperty" /> no qual basear o novo <see cref="T:System.Windows.Input.StylusPointProperty" />.</param>
		/// <param name="minimum">O valor mínimo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="maximum">O valor máximo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</param>
		/// <param name="unit">Um dos valores de <see cref="T:System.Windows.Input.StylusPointPropertyUnit" />.</param>
		/// <param name="resolution">A escala que converte um valor da propriedade <see cref="T:System.Windows.Input.StylusPoint" /> em suas unidades.</param>
		// Token: 0x06001502 RID: 5378 RVA: 0x0004E23C File Offset: 0x0004D63C
		public StylusPointPropertyInfo(StylusPointProperty stylusPointProperty, int minimum, int maximum, StylusPointPropertyUnit unit, float resolution) : base(stylusPointProperty)
		{
			if (!StylusPointPropertyUnitHelper.IsDefined(unit))
			{
				throw new InvalidEnumArgumentException("unit", (int)unit, typeof(StylusPointPropertyUnit));
			}
			if (maximum < minimum)
			{
				throw new ArgumentException(SR.Get("Stylus_InvalidMax"), "maximum");
			}
			if (resolution < 0f)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointPropertyInfoResolution"), "resolution");
			}
			this._min = minimum;
			this._max = maximum;
			this._resolution = resolution;
			this._unit = unit;
		}

		/// <summary>Obtém o valor mínimo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>O valor mínimo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0004E2C8 File Offset: 0x0004D6C8
		public int Minimum
		{
			get
			{
				return this._min;
			}
		}

		/// <summary>Obtém o valor máximo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>O valor máximo aceito para a propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0004E2DC File Offset: 0x0004D6DC
		public int Maximum
		{
			get
			{
				return this._max;
			}
		}

		/// <summary>Obtém a escala que converte um valor da propriedade <see cref="T:System.Windows.Input.StylusPoint" /> em suas unidades.</summary>
		/// <returns>A escala que converte um <see cref="T:System.Windows.Input.StylusPoint" /> valor da propriedade em unidades.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0004E2F0 File Offset: 0x0004D6F0
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x0004E304 File Offset: 0x0004D704
		public float Resolution
		{
			get
			{
				return this._resolution;
			}
			internal set
			{
				this._resolution = value;
			}
		}

		/// <summary>Obtém o tipo de medida usado pela propriedade <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.Input.StylusPointPropertyUnit" />.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0004E318 File Offset: 0x0004D718
		public StylusPointPropertyUnit Unit
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004E32C File Offset: 0x0004D72C
		internal static bool AreCompatible(StylusPointPropertyInfo stylusPointPropertyInfo1, StylusPointPropertyInfo stylusPointPropertyInfo2)
		{
			if (stylusPointPropertyInfo1 == null || stylusPointPropertyInfo2 == null)
			{
				throw new ArgumentNullException("stylusPointPropertyInfo");
			}
			return stylusPointPropertyInfo1.Id == stylusPointPropertyInfo2.Id && stylusPointPropertyInfo1.IsButton == stylusPointPropertyInfo2.IsButton;
		}

		// Token: 0x04000B47 RID: 2887
		private int _min;

		// Token: 0x04000B48 RID: 2888
		private int _max;

		// Token: 0x04000B49 RID: 2889
		private float _resolution;

		// Token: 0x04000B4A RID: 2890
		private StylusPointPropertyUnit _unit;
	}
}
