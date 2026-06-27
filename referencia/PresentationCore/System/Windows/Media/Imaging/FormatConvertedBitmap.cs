using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece funcionalidade de conversão de formato de pixel para um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
	// Token: 0x020005EB RID: 1515
	public sealed class FormatConvertedBitmap : BitmapSource, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</summary>
		// Token: 0x06004573 RID: 17779 RVA: 0x0010EEBC File Offset: 0x0010E2BC
		public FormatConvertedBitmap() : base(true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" /> que tem o <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.Source" />, <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationFormat" />, <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationPalette" /> e <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.AlphaThreshold" /> especificados.</summary>
		/// <param name="source">O <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.Source" /> da nova instância <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</param>
		/// <param name="destinationFormat">O <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationFormat" /> da nova instância <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</param>
		/// <param name="destinationPalette">O <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationPalette" /> da nova instância do <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" /> se <paramref name="destinationFormat" /> for um formato indexado.</param>
		/// <param name="alphaThreshold">O <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.AlphaThreshold" /> da nova instância <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</param>
		// Token: 0x06004574 RID: 17780 RVA: 0x0010EED0 File Offset: 0x0010E2D0
		public FormatConvertedBitmap(BitmapSource source, PixelFormat destinationFormat, BitmapPalette destinationPalette, double alphaThreshold) : base(true)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (alphaThreshold < 0.0 || alphaThreshold > 100.0)
			{
				throw new ArgumentException(SR.Get("Image_AlphaThresholdOutOfRange"));
			}
			this._bitmapInit.BeginInit();
			this.Source = source;
			this.DestinationFormat = destinationFormat;
			this.DestinationPalette = destinationPalette;
			this.AlphaThreshold = alphaThreshold;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		/// <summary>Sinaliza o início da inicialização <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" /> está sendo inicializado no momento. O <see cref="M:System.Windows.Media.Imaging.FormatConvertedBitmap.BeginInit" /> já foi chamado.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" /> já foi inicializado.</exception>
		// Token: 0x06004575 RID: 17781 RVA: 0x0010EF58 File Offset: 0x0010E358
		public void BeginInit()
		{
			base.WritePreamble();
			this._bitmapInit.BeginInit();
		}

		/// <summary>Sinaliza o término da inicialização do <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">A propriedade <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.Source" /> é <see langword="null" />.  
		///
		/// ou - 
		/// A propriedade <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationFormat" /> é um formato indexado e a propriedade <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationPalette" /> é <see langword="null" />.  
		///
		/// ou - 
		/// As cores de paleta não correspondem ao formato de destino.  
		///
		/// ou - 
		/// O método <see cref="M:System.Windows.Media.Imaging.FormatConvertedBitmap.EndInit" /> é chamado sem primeiro chamar <see cref="M:System.Windows.Media.Imaging.FormatConvertedBitmap.BeginInit" />.</exception>
		// Token: 0x06004576 RID: 17782 RVA: 0x0010EF78 File Offset: 0x0010E378
		[SecurityCritical]
		public void EndInit()
		{
			base.WritePreamble();
			this._bitmapInit.EndInit();
			this.IsValidForFinalizeCreation(true);
			this.FinalizeCreation();
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x0010EFA4 File Offset: 0x0010E3A4
		private void ClonePrequel(FormatConvertedBitmap otherFormatConvertedBitmap)
		{
			this.BeginInit();
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x0010EFB8 File Offset: 0x0010E3B8
		private void ClonePostscript(FormatConvertedBitmap otherFormatConvertedBitmap)
		{
			this.EndInit();
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x0010EFCC File Offset: 0x0010E3CC
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			this._bitmapInit.EnsureInitializedComplete();
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					IntPtr imagingFactoryPtr = factoryMaker.ImagingFactoryPtr;
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateFormatConverter(imagingFactoryPtr, out bitmapSourceSafeMILHandle));
					SafeMILHandle bitmapPalette;
					if (this.DestinationPalette != null)
					{
						bitmapPalette = this.DestinationPalette.InternalPalette;
					}
					else
					{
						bitmapPalette = new SafeMILHandle();
					}
					Guid guid = this.DestinationFormat.Guid;
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						HRESULT.Check(UnsafeNativeMethods.WICFormatConverter.Initialize(bitmapSourceSafeMILHandle, this.Source.WicSourceHandle, ref guid, DitherType.DitherTypeErrorDiffusion, bitmapPalette, this.AlphaThreshold, WICPaletteType.WICPaletteTypeOptimal));
					}
					base.WicSourceHandle = bitmapSourceSafeMILHandle;
					this._isSourceCached = false;
				}
				catch
				{
					this._bitmapInit.Reset();
					throw;
				}
				finally
				{
					if (bitmapSourceSafeMILHandle != null)
					{
						bitmapSourceSafeMILHandle.Close();
					}
				}
			}
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x0010F118 File Offset: 0x0010E518
		private void SourcePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				BitmapSource bitmapSource = e.NewValue as BitmapSource;
				this._source = bitmapSource;
				base.RegisterDownloadEventSource(this._source);
				this._syncObject = ((bitmapSource != null) ? bitmapSource.SyncObject : this._bitmapInit);
			}
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x0010F168 File Offset: 0x0010E568
		internal override bool IsValidForFinalizeCreation(bool throwIfInvalid)
		{
			if (this.Source != null)
			{
				if (this.DestinationFormat.Palettized)
				{
					if (this.DestinationPalette == null)
					{
						if (throwIfInvalid)
						{
							throw new InvalidOperationException(SR.Get("Image_IndexedPixelFormatRequiresPalette"));
						}
						return false;
					}
					else if (1 << this.DestinationFormat.BitsPerPixel < this.DestinationPalette.Colors.Count)
					{
						if (throwIfInvalid)
						{
							throw new InvalidOperationException(SR.Get("Image_PaletteColorsDoNotMatchFormat"));
						}
						return false;
					}
				}
				return true;
			}
			if (throwIfInvalid)
			{
				throw new InvalidOperationException(SR.Get("Image_NoArgument", new object[]
				{
					"Source"
				}));
			}
			return false;
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x0010F208 File Offset: 0x0010E608
		private void DestinationFormatPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._destinationFormat = (PixelFormat)e.NewValue;
			}
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x0010F230 File Offset: 0x0010E630
		private void DestinationPalettePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._destinationPalette = (e.NewValue as BitmapPalette);
			}
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x0010F258 File Offset: 0x0010E658
		private void AlphaThresholdPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._alphaThreshold = (double)e.NewValue;
			}
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x0010F280 File Offset: 0x0010E680
		private static object CoerceSource(DependencyObject d, object value)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			if (!formatConvertedBitmap._bitmapInit.IsInInit)
			{
				return formatConvertedBitmap._source;
			}
			return value;
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x0010F2AC File Offset: 0x0010E6AC
		private static object CoerceDestinationFormat(DependencyObject d, object value)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			if (!formatConvertedBitmap._bitmapInit.IsInInit)
			{
				return formatConvertedBitmap._destinationFormat;
			}
			if (((PixelFormat)value).Format != PixelFormatEnum.Default)
			{
				return value;
			}
			if (formatConvertedBitmap.Source != null)
			{
				return formatConvertedBitmap.Source.Format;
			}
			return formatConvertedBitmap._destinationFormat;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x0010F310 File Offset: 0x0010E710
		private static object CoerceDestinationPalette(DependencyObject d, object value)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			if (!formatConvertedBitmap._bitmapInit.IsInInit)
			{
				return formatConvertedBitmap._destinationPalette;
			}
			return value;
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x0010F33C File Offset: 0x0010E73C
		private static object CoerceAlphaThreshold(DependencyObject d, object value)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			if (!formatConvertedBitmap._bitmapInit.IsInInit)
			{
				return formatConvertedBitmap._alphaThreshold;
			}
			return value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004583 RID: 17795 RVA: 0x0010F36C File Offset: 0x0010E76C
		public new FormatConvertedBitmap Clone()
		{
			return (FormatConvertedBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004584 RID: 17796 RVA: 0x0010F384 File Offset: 0x0010E784
		public new FormatConvertedBitmap CloneCurrentValue()
		{
			return (FormatConvertedBitmap)base.CloneCurrentValue();
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x0010F39C File Offset: 0x0010E79C
		private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			formatConvertedBitmap.SourcePropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			formatConvertedBitmap.PropertyChanged(FormatConvertedBitmap.SourceProperty);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x0010F3DC File Offset: 0x0010E7DC
		private static void DestinationFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			formatConvertedBitmap.DestinationFormatPropertyChangedHook(e);
			formatConvertedBitmap.PropertyChanged(FormatConvertedBitmap.DestinationFormatProperty);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x0010F404 File Offset: 0x0010E804
		private static void DestinationPalettePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			formatConvertedBitmap.DestinationPalettePropertyChangedHook(e);
			formatConvertedBitmap.PropertyChanged(FormatConvertedBitmap.DestinationPaletteProperty);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x0010F42C File Offset: 0x0010E82C
		private static void AlphaThresholdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			FormatConvertedBitmap formatConvertedBitmap = (FormatConvertedBitmap)d;
			formatConvertedBitmap.AlphaThresholdPropertyChangedHook(e);
			formatConvertedBitmap.PropertyChanged(FormatConvertedBitmap.AlphaThresholdProperty);
		}

		/// <summary>Obtém ou define a origem do bitmap.</summary>
		/// <returns>A fonte de bitmap. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x0010F454 File Offset: 0x0010E854
		// (set) Token: 0x0600458A RID: 17802 RVA: 0x0010F474 File Offset: 0x0010E874
		public BitmapSource Source
		{
			get
			{
				return (BitmapSource)base.GetValue(FormatConvertedBitmap.SourceProperty);
			}
			set
			{
				base.SetValueInternal(FormatConvertedBitmap.SourceProperty, value);
			}
		}

		/// <summary>Obtém ou define o formato de pixel de conversão do bitmap.</summary>
		/// <returns>O formato de pixel a ser aplicado ao bitmap. O valor padrão é <see cref="P:System.Windows.Media.PixelFormats.Pbgra32" />.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x0010F490 File Offset: 0x0010E890
		// (set) Token: 0x0600458C RID: 17804 RVA: 0x0010F4B0 File Offset: 0x0010E8B0
		public PixelFormat DestinationFormat
		{
			get
			{
				return (PixelFormat)base.GetValue(FormatConvertedBitmap.DestinationFormatProperty);
			}
			set
			{
				base.SetValueInternal(FormatConvertedBitmap.DestinationFormatProperty, value);
			}
		}

		/// <summary>Obtém ou define a paleta a ser aplicada ao bitmap se o formato for indexado.</summary>
		/// <returns>A paleta de destino a ser aplicado ao bitmap. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x0010F4D0 File Offset: 0x0010E8D0
		// (set) Token: 0x0600458E RID: 17806 RVA: 0x0010F4F0 File Offset: 0x0010E8F0
		public BitmapPalette DestinationPalette
		{
			get
			{
				return (BitmapPalette)base.GetValue(FormatConvertedBitmap.DestinationPaletteProperty);
			}
			set
			{
				base.SetValueInternal(FormatConvertedBitmap.DestinationPaletteProperty, value);
			}
		}

		/// <summary>Obtém ou define o limite de canal alfa de um bitmap durante a conversão para formatos da paleta que reconhecem uma cor alfa.</summary>
		/// <returns>O limite de canal alfa para este <see cref="T:System.Windows.Media.Imaging.FormatConvertedBitmap" />. O valor padrão é 0,0.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x0010F50C File Offset: 0x0010E90C
		// (set) Token: 0x06004590 RID: 17808 RVA: 0x0010F52C File Offset: 0x0010E92C
		public double AlphaThreshold
		{
			get
			{
				return (double)base.GetValue(FormatConvertedBitmap.AlphaThresholdProperty);
			}
			set
			{
				base.SetValueInternal(FormatConvertedBitmap.AlphaThresholdProperty, value);
			}
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x0010F54C File Offset: 0x0010E94C
		protected override Freezable CreateInstanceCore()
		{
			return new FormatConvertedBitmap();
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x0010F560 File Offset: 0x0010E960
		protected override void CloneCore(Freezable source)
		{
			FormatConvertedBitmap otherFormatConvertedBitmap = (FormatConvertedBitmap)source;
			this.ClonePrequel(otherFormatConvertedBitmap);
			base.CloneCore(source);
			this.ClonePostscript(otherFormatConvertedBitmap);
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x0010F58C File Offset: 0x0010E98C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			FormatConvertedBitmap otherFormatConvertedBitmap = (FormatConvertedBitmap)source;
			this.ClonePrequel(otherFormatConvertedBitmap);
			base.CloneCurrentValueCore(source);
			this.ClonePostscript(otherFormatConvertedBitmap);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x0010F5B8 File Offset: 0x0010E9B8
		protected override void GetAsFrozenCore(Freezable source)
		{
			FormatConvertedBitmap otherFormatConvertedBitmap = (FormatConvertedBitmap)source;
			this.ClonePrequel(otherFormatConvertedBitmap);
			base.GetAsFrozenCore(source);
			this.ClonePostscript(otherFormatConvertedBitmap);
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0010F5E4 File Offset: 0x0010E9E4
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			FormatConvertedBitmap otherFormatConvertedBitmap = (FormatConvertedBitmap)source;
			this.ClonePrequel(otherFormatConvertedBitmap);
			base.GetCurrentValueAsFrozenCore(source);
			this.ClonePostscript(otherFormatConvertedBitmap);
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x0010F610 File Offset: 0x0010EA10
		static FormatConvertedBitmap()
		{
			Type typeFromHandle = typeof(FormatConvertedBitmap);
			FormatConvertedBitmap.SourceProperty = Animatable.RegisterProperty("Source", typeof(BitmapSource), typeFromHandle, null, new PropertyChangedCallback(FormatConvertedBitmap.SourcePropertyChanged), null, false, new CoerceValueCallback(FormatConvertedBitmap.CoerceSource));
			FormatConvertedBitmap.DestinationFormatProperty = Animatable.RegisterProperty("DestinationFormat", typeof(PixelFormat), typeFromHandle, PixelFormats.Pbgra32, new PropertyChangedCallback(FormatConvertedBitmap.DestinationFormatPropertyChanged), null, false, new CoerceValueCallback(FormatConvertedBitmap.CoerceDestinationFormat));
			FormatConvertedBitmap.DestinationPaletteProperty = Animatable.RegisterProperty("DestinationPalette", typeof(BitmapPalette), typeFromHandle, null, new PropertyChangedCallback(FormatConvertedBitmap.DestinationPalettePropertyChanged), null, false, new CoerceValueCallback(FormatConvertedBitmap.CoerceDestinationPalette));
			FormatConvertedBitmap.AlphaThresholdProperty = Animatable.RegisterProperty("AlphaThreshold", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(FormatConvertedBitmap.AlphaThresholdPropertyChanged), null, false, new CoerceValueCallback(FormatConvertedBitmap.CoerceAlphaThreshold));
		}

		// Token: 0x0400193A RID: 6458
		private BitmapSource _source;

		// Token: 0x0400193B RID: 6459
		private PixelFormat _destinationFormat;

		// Token: 0x0400193C RID: 6460
		private BitmapPalette _destinationPalette;

		// Token: 0x0400193D RID: 6461
		private double _alphaThreshold;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.Source" />.</summary>
		// Token: 0x0400193E RID: 6462
		public static readonly DependencyProperty SourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationFormat" />.</summary>
		// Token: 0x0400193F RID: 6463
		public static readonly DependencyProperty DestinationFormatProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.DestinationPalette" />.</summary>
		// Token: 0x04001940 RID: 6464
		public static readonly DependencyProperty DestinationPaletteProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.FormatConvertedBitmap.AlphaThreshold" />.</summary>
		// Token: 0x04001941 RID: 6465
		public static readonly DependencyProperty AlphaThresholdProperty;

		// Token: 0x04001942 RID: 6466
		internal static BitmapSource s_Source = null;

		// Token: 0x04001943 RID: 6467
		internal static PixelFormat s_DestinationFormat = PixelFormats.Pbgra32;

		// Token: 0x04001944 RID: 6468
		internal static BitmapPalette s_DestinationPalette = null;

		// Token: 0x04001945 RID: 6469
		internal const double c_AlphaThreshold = 0.0;
	}
}
