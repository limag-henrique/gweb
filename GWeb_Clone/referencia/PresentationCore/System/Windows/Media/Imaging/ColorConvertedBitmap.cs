using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Altera o espaço de cores de um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
	// Token: 0x020005E8 RID: 1512
	public sealed class ColorConvertedBitmap : BitmapSource, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" />.</summary>
		// Token: 0x06004533 RID: 17715 RVA: 0x0010E158 File Offset: 0x0010D558
		public ColorConvertedBitmap() : base(true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" /> usando os valores especificados.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é convertido.</param>
		/// <param name="sourceColorContext">O <see cref="T:System.Windows.Media.ColorContext" /> do bitmap de origem.</param>
		/// <param name="destinationColorContext">O <see cref="T:System.Windows.Media.ColorContext" /> do bitmap convertido.</param>
		/// <param name="format">O <see cref="T:System.Windows.Media.PixelFormat" /> do bitmap convertido.</param>
		// Token: 0x06004534 RID: 17716 RVA: 0x0010E16C File Offset: 0x0010D56C
		public ColorConvertedBitmap(BitmapSource source, ColorContext sourceColorContext, ColorContext destinationColorContext, PixelFormat format) : base(true)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (sourceColorContext == null)
			{
				throw new ArgumentNullException("sourceColorContext");
			}
			if (destinationColorContext == null)
			{
				throw new ArgumentNullException("destinationColorContext");
			}
			this._bitmapInit.BeginInit();
			this.Source = source;
			this.SourceColorContext = sourceColorContext;
			this.DestinationColorContext = destinationColorContext;
			this.DestinationFormat = format;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		/// <summary>Sinaliza o início da inicialização <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" /> está sendo inicializado no momento. O <see cref="M:System.Windows.Media.Imaging.ColorConvertedBitmap.BeginInit" /> já foi chamado.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" /> já foi inicializado.</exception>
		// Token: 0x06004535 RID: 17717 RVA: 0x0010E1F0 File Offset: 0x0010D5F0
		public void BeginInit()
		{
			base.WritePreamble();
			this._bitmapInit.BeginInit();
		}

		/// <summary>Sinaliza o término da inicialização do <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">A propriedade <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.Source" />, <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.SourceColorContext" /> ou <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.DestinationColorContext" /> é <see langword="null" />.  
		///
		/// ou - 
		/// O método <see cref="M:System.Windows.Media.Imaging.ColorConvertedBitmap.EndInit" /> é chamado sem primeiro chamar <see cref="M:System.Windows.Media.Imaging.ColorConvertedBitmap.BeginInit" />.</exception>
		// Token: 0x06004536 RID: 17718 RVA: 0x0010E210 File Offset: 0x0010D610
		[SecurityCritical]
		public void EndInit()
		{
			base.WritePreamble();
			this._bitmapInit.EndInit();
			this.IsValidForFinalizeCreation(true);
			this.FinalizeCreation();
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x0010E23C File Offset: 0x0010D63C
		private void ClonePrequel(ColorConvertedBitmap otherColorConvertedBitmap)
		{
			this.BeginInit();
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x0010E250 File Offset: 0x0010D650
		private void ClonePostscript(ColorConvertedBitmap otherColorConvertedBitmap)
		{
			this.EndInit();
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x0010E264 File Offset: 0x0010D664
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			this._bitmapInit.EnsureInitializedComplete();
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			HRESULT.Check(UnsafeNativeMethods.WICCodec.CreateColorTransform(out bitmapSourceSafeMILHandle));
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				Guid guid = this.DestinationFormat.Guid;
				HRESULT.Check(UnsafeNativeMethods.WICColorTransform.Initialize(bitmapSourceSafeMILHandle, this.Source.WicSourceHandle, this.SourceColorContext.ColorContextHandle, this.DestinationColorContext.ColorContextHandle, ref guid));
			}
			base.WicSourceHandle = bitmapSourceSafeMILHandle;
			this._isSourceCached = this.Source.IsSourceCached;
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x0010E328 File Offset: 0x0010D728
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

		// Token: 0x0600453B RID: 17723 RVA: 0x0010E378 File Offset: 0x0010D778
		internal override bool IsValidForFinalizeCreation(bool throwIfInvalid)
		{
			if (this.Source == null)
			{
				if (throwIfInvalid)
				{
					throw new InvalidOperationException(SR.Get("Image_NoArgument", new object[]
					{
						"Source"
					}));
				}
				return false;
			}
			else if (this.SourceColorContext == null)
			{
				if (throwIfInvalid)
				{
					throw new InvalidOperationException(SR.Get("Color_NullColorContext"));
				}
				return false;
			}
			else
			{
				if (!(this.DestinationColorContext == null))
				{
					return true;
				}
				if (throwIfInvalid)
				{
					throw new InvalidOperationException(SR.Get("Image_NoArgument", new object[]
					{
						"DestinationColorContext"
					}));
				}
				return false;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x0010E408 File Offset: 0x0010D808
		private void SourceColorContextPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._sourceColorContext = (e.NewValue as ColorContext);
			}
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x0010E430 File Offset: 0x0010D830
		private void DestinationColorContextPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._destinationColorContext = (e.NewValue as ColorContext);
			}
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x0010E458 File Offset: 0x0010D858
		private void DestinationFormatPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._destinationFormat = (PixelFormat)e.NewValue;
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x0010E480 File Offset: 0x0010D880
		private static object CoerceSource(DependencyObject d, object value)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			if (!colorConvertedBitmap._bitmapInit.IsInInit)
			{
				return colorConvertedBitmap._source;
			}
			return value;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x0010E4AC File Offset: 0x0010D8AC
		private static object CoerceSourceColorContext(DependencyObject d, object value)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			if (!colorConvertedBitmap._bitmapInit.IsInInit)
			{
				return colorConvertedBitmap._sourceColorContext;
			}
			return value;
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x0010E4D8 File Offset: 0x0010D8D8
		private static object CoerceDestinationColorContext(DependencyObject d, object value)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			if (!colorConvertedBitmap._bitmapInit.IsInInit)
			{
				return colorConvertedBitmap._destinationColorContext;
			}
			return value;
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0010E504 File Offset: 0x0010D904
		private static object CoerceDestinationFormat(DependencyObject d, object value)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			if (!colorConvertedBitmap._bitmapInit.IsInInit)
			{
				return colorConvertedBitmap._destinationFormat;
			}
			return value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004543 RID: 17731 RVA: 0x0010E534 File Offset: 0x0010D934
		public new ColorConvertedBitmap Clone()
		{
			return (ColorConvertedBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.ColorConvertedBitmap" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004544 RID: 17732 RVA: 0x0010E54C File Offset: 0x0010D94C
		public new ColorConvertedBitmap CloneCurrentValue()
		{
			return (ColorConvertedBitmap)base.CloneCurrentValue();
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x0010E564 File Offset: 0x0010D964
		private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			colorConvertedBitmap.SourcePropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			colorConvertedBitmap.PropertyChanged(ColorConvertedBitmap.SourceProperty);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0010E5A4 File Offset: 0x0010D9A4
		private static void SourceColorContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			colorConvertedBitmap.SourceColorContextPropertyChangedHook(e);
			colorConvertedBitmap.PropertyChanged(ColorConvertedBitmap.SourceColorContextProperty);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x0010E5CC File Offset: 0x0010D9CC
		private static void DestinationColorContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			colorConvertedBitmap.DestinationColorContextPropertyChangedHook(e);
			colorConvertedBitmap.PropertyChanged(ColorConvertedBitmap.DestinationColorContextProperty);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x0010E5F4 File Offset: 0x0010D9F4
		private static void DestinationFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorConvertedBitmap colorConvertedBitmap = (ColorConvertedBitmap)d;
			colorConvertedBitmap.DestinationFormatPropertyChangedHook(e);
			colorConvertedBitmap.PropertyChanged(ColorConvertedBitmap.DestinationFormatProperty);
		}

		/// <summary>Obtém ou define um valor que identifica o bitmap de origem que é convertido.</summary>
		/// <returns>Um valor que identifica o bitmap de origem que é convertido.</returns>
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x0010E61C File Offset: 0x0010DA1C
		// (set) Token: 0x0600454A RID: 17738 RVA: 0x0010E63C File Offset: 0x0010DA3C
		public BitmapSource Source
		{
			get
			{
				return (BitmapSource)base.GetValue(ColorConvertedBitmap.SourceProperty);
			}
			set
			{
				base.SetValueInternal(ColorConvertedBitmap.SourceProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o perfil de cor do bitmap de origem.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.ColorContext" />.</returns>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x0010E658 File Offset: 0x0010DA58
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x0010E678 File Offset: 0x0010DA78
		public ColorContext SourceColorContext
		{
			get
			{
				return (ColorContext)base.GetValue(ColorConvertedBitmap.SourceColorContextProperty);
			}
			set
			{
				base.SetValueInternal(ColorConvertedBitmap.SourceColorContextProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o perfil de cor, conforme definido pela classe <see cref="T:System.Windows.Media.ColorContext" />, do bitmap convertido.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.ColorContext" />.</returns>
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x0010E694 File Offset: 0x0010DA94
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x0010E6B4 File Offset: 0x0010DAB4
		public ColorContext DestinationColorContext
		{
			get
			{
				return (ColorContext)base.GetValue(ColorConvertedBitmap.DestinationColorContextProperty);
			}
			set
			{
				base.SetValueInternal(ColorConvertedBitmap.DestinationColorContextProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que representa o <see cref="T:System.Windows.Media.PixelFormat" /> do bitmap convertido.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.PixelFormat" />.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x0010E6D0 File Offset: 0x0010DAD0
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x0010E6F0 File Offset: 0x0010DAF0
		public PixelFormat DestinationFormat
		{
			get
			{
				return (PixelFormat)base.GetValue(ColorConvertedBitmap.DestinationFormatProperty);
			}
			set
			{
				base.SetValueInternal(ColorConvertedBitmap.DestinationFormatProperty, value);
			}
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x0010E710 File Offset: 0x0010DB10
		protected override Freezable CreateInstanceCore()
		{
			return new ColorConvertedBitmap();
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x0010E724 File Offset: 0x0010DB24
		protected override void CloneCore(Freezable source)
		{
			ColorConvertedBitmap otherColorConvertedBitmap = (ColorConvertedBitmap)source;
			this.ClonePrequel(otherColorConvertedBitmap);
			base.CloneCore(source);
			this.ClonePostscript(otherColorConvertedBitmap);
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x0010E750 File Offset: 0x0010DB50
		protected override void CloneCurrentValueCore(Freezable source)
		{
			ColorConvertedBitmap otherColorConvertedBitmap = (ColorConvertedBitmap)source;
			this.ClonePrequel(otherColorConvertedBitmap);
			base.CloneCurrentValueCore(source);
			this.ClonePostscript(otherColorConvertedBitmap);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x0010E77C File Offset: 0x0010DB7C
		protected override void GetAsFrozenCore(Freezable source)
		{
			ColorConvertedBitmap otherColorConvertedBitmap = (ColorConvertedBitmap)source;
			this.ClonePrequel(otherColorConvertedBitmap);
			base.GetAsFrozenCore(source);
			this.ClonePostscript(otherColorConvertedBitmap);
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x0010E7A8 File Offset: 0x0010DBA8
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			ColorConvertedBitmap otherColorConvertedBitmap = (ColorConvertedBitmap)source;
			this.ClonePrequel(otherColorConvertedBitmap);
			base.GetCurrentValueAsFrozenCore(source);
			this.ClonePostscript(otherColorConvertedBitmap);
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x0010E7D4 File Offset: 0x0010DBD4
		static ColorConvertedBitmap()
		{
			Type typeFromHandle = typeof(ColorConvertedBitmap);
			ColorConvertedBitmap.SourceProperty = Animatable.RegisterProperty("Source", typeof(BitmapSource), typeFromHandle, null, new PropertyChangedCallback(ColorConvertedBitmap.SourcePropertyChanged), null, false, new CoerceValueCallback(ColorConvertedBitmap.CoerceSource));
			ColorConvertedBitmap.SourceColorContextProperty = Animatable.RegisterProperty("SourceColorContext", typeof(ColorContext), typeFromHandle, null, new PropertyChangedCallback(ColorConvertedBitmap.SourceColorContextPropertyChanged), null, false, new CoerceValueCallback(ColorConvertedBitmap.CoerceSourceColorContext));
			ColorConvertedBitmap.DestinationColorContextProperty = Animatable.RegisterProperty("DestinationColorContext", typeof(ColorContext), typeFromHandle, null, new PropertyChangedCallback(ColorConvertedBitmap.DestinationColorContextPropertyChanged), null, false, new CoerceValueCallback(ColorConvertedBitmap.CoerceDestinationColorContext));
			ColorConvertedBitmap.DestinationFormatProperty = Animatable.RegisterProperty("DestinationFormat", typeof(PixelFormat), typeFromHandle, PixelFormats.Pbgra32, new PropertyChangedCallback(ColorConvertedBitmap.DestinationFormatPropertyChanged), null, false, new CoerceValueCallback(ColorConvertedBitmap.CoerceDestinationFormat));
		}

		// Token: 0x04001927 RID: 6439
		private BitmapSource _source;

		// Token: 0x04001928 RID: 6440
		private ColorContext _sourceColorContext;

		// Token: 0x04001929 RID: 6441
		private ColorContext _destinationColorContext;

		// Token: 0x0400192A RID: 6442
		private PixelFormat _destinationFormat;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.Source" />.</summary>
		// Token: 0x0400192B RID: 6443
		public static readonly DependencyProperty SourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.SourceColorContext" />.</summary>
		// Token: 0x0400192C RID: 6444
		public static readonly DependencyProperty SourceColorContextProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.DestinationColorContext" />.</summary>
		// Token: 0x0400192D RID: 6445
		public static readonly DependencyProperty DestinationColorContextProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.ColorConvertedBitmap.DestinationFormat" />.</summary>
		// Token: 0x0400192E RID: 6446
		public static readonly DependencyProperty DestinationFormatProperty;

		// Token: 0x0400192F RID: 6447
		internal static BitmapSource s_Source = null;

		// Token: 0x04001930 RID: 6448
		internal static ColorContext s_SourceColorContext = null;

		// Token: 0x04001931 RID: 6449
		internal static ColorContext s_DestinationColorContext = null;

		// Token: 0x04001932 RID: 6450
		internal static PixelFormat s_DestinationFormat = PixelFormats.Pbgra32;
	}
}
