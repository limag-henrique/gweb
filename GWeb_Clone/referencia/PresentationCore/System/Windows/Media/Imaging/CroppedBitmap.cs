using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Corta um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
	// Token: 0x020005E9 RID: 1513
	public sealed class CroppedBitmap : BitmapSource, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />.</summary>
		// Token: 0x06004557 RID: 17751 RVA: 0x0010E8E8 File Offset: 0x0010DCE8
		public CroppedBitmap() : base(true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" /> que tem o <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.Source" /> e o <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.SourceRect" /> especificados.</summary>
		/// <param name="source">O <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.Source" /> da nova instância <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />.</param>
		/// <param name="sourceRect">O <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.SourceRect" /> da nova instância <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceRect" /> está fora dos limites de <paramref name="source" />.</exception>
		// Token: 0x06004558 RID: 17752 RVA: 0x0010E8FC File Offset: 0x0010DCFC
		public CroppedBitmap(BitmapSource source, Int32Rect sourceRect) : base(true)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this._bitmapInit.BeginInit();
			this.Source = source;
			this.SourceRect = sourceRect;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		/// <summary>Sinaliza o início da inicialização <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" /> está sendo inicializado no momento. O <see cref="M:System.Windows.Media.Imaging.CroppedBitmap.BeginInit" /> já foi chamado.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" /> já foi inicializado.</exception>
		// Token: 0x06004559 RID: 17753 RVA: 0x0010E948 File Offset: 0x0010DD48
		public void BeginInit()
		{
			base.WritePreamble();
			this._bitmapInit.BeginInit();
		}

		/// <summary>Sinaliza o término da inicialização do <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">A propriedade <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.Source" /> é <see langword="null" />.  
		///
		/// ou - 
		/// O método <see cref="M:System.Windows.Media.Imaging.CroppedBitmap.EndInit" /> é chamado sem primeiro chamar <see cref="M:System.Windows.Media.Imaging.CroppedBitmap.BeginInit" />.</exception>
		// Token: 0x0600455A RID: 17754 RVA: 0x0010E968 File Offset: 0x0010DD68
		[SecurityCritical]
		public void EndInit()
		{
			base.WritePreamble();
			this._bitmapInit.EndInit();
			this.IsValidForFinalizeCreation(true);
			this.FinalizeCreation();
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x0010E994 File Offset: 0x0010DD94
		private void ClonePrequel(CroppedBitmap otherCroppedBitmap)
		{
			this.BeginInit();
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x0010E9A8 File Offset: 0x0010DDA8
		private void ClonePostscript(CroppedBitmap otherCroppedBitmap)
		{
			this.EndInit();
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x0010E9BC File Offset: 0x0010DDBC
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			this._bitmapInit.EnsureInitializedComplete();
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			Int32Rect sourceRect = this.SourceRect;
			BitmapSource source = this.Source;
			if (sourceRect.IsEmpty)
			{
				sourceRect.Width = source.PixelWidth;
				sourceRect.Height = source.PixelHeight;
			}
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					IntPtr imagingFactoryPtr = factoryMaker.ImagingFactoryPtr;
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapClipper(imagingFactoryPtr, out bitmapSourceSafeMILHandle));
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						HRESULT.Check(UnsafeNativeMethods.WICBitmapClipper.Initialize(bitmapSourceSafeMILHandle, source.WicSourceHandle, ref sourceRect));
					}
					base.WicSourceHandle = bitmapSourceSafeMILHandle;
					this._isSourceCached = source.IsSourceCached;
					bitmapSourceSafeMILHandle = null;
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

		// Token: 0x0600455E RID: 17758 RVA: 0x0010EB08 File Offset: 0x0010DF08
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

		// Token: 0x0600455F RID: 17759 RVA: 0x0010EB58 File Offset: 0x0010DF58
		internal override bool IsValidForFinalizeCreation(bool throwIfInvalid)
		{
			if (this.Source != null)
			{
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

		// Token: 0x06004560 RID: 17760 RVA: 0x0010EB94 File Offset: 0x0010DF94
		private void SourceRectPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._sourceRect = (Int32Rect)e.NewValue;
			}
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x0010EBBC File Offset: 0x0010DFBC
		private static object CoerceSource(DependencyObject d, object value)
		{
			CroppedBitmap croppedBitmap = (CroppedBitmap)d;
			if (!croppedBitmap._bitmapInit.IsInInit)
			{
				return croppedBitmap._source;
			}
			return value;
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x0010EBE8 File Offset: 0x0010DFE8
		private static object CoerceSourceRect(DependencyObject d, object value)
		{
			CroppedBitmap croppedBitmap = (CroppedBitmap)d;
			if (!croppedBitmap._bitmapInit.IsInInit)
			{
				return croppedBitmap._sourceRect;
			}
			return value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004563 RID: 17763 RVA: 0x0010EC18 File Offset: 0x0010E018
		public new CroppedBitmap Clone()
		{
			return (CroppedBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.CroppedBitmap" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004564 RID: 17764 RVA: 0x0010EC30 File Offset: 0x0010E030
		public new CroppedBitmap CloneCurrentValue()
		{
			return (CroppedBitmap)base.CloneCurrentValue();
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x0010EC48 File Offset: 0x0010E048
		private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CroppedBitmap croppedBitmap = (CroppedBitmap)d;
			croppedBitmap.SourcePropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			croppedBitmap.PropertyChanged(CroppedBitmap.SourceProperty);
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x0010EC88 File Offset: 0x0010E088
		private static void SourceRectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CroppedBitmap croppedBitmap = (CroppedBitmap)d;
			croppedBitmap.SourceRectPropertyChangedHook(e);
			croppedBitmap.PropertyChanged(CroppedBitmap.SourceRectProperty);
		}

		/// <summary>Obtém ou define a origem do bitmap.</summary>
		/// <returns>A fonte de bitmap. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x0010ECB0 File Offset: 0x0010E0B0
		// (set) Token: 0x06004568 RID: 17768 RVA: 0x0010ECD0 File Offset: 0x0010E0D0
		public BitmapSource Source
		{
			get
			{
				return (BitmapSource)base.GetValue(CroppedBitmap.SourceProperty);
			}
			set
			{
				base.SetValueInternal(CroppedBitmap.SourceProperty, value);
			}
		}

		/// <summary>Obtém ou define a área retangular para a qual o bitmap será recortado.</summary>
		/// <returns>A área retangular para a qual o bitmap será recortado. O padrão é <see cref="P:System.Windows.Int32Rect.Empty" />.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0010ECEC File Offset: 0x0010E0EC
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x0010ED0C File Offset: 0x0010E10C
		public Int32Rect SourceRect
		{
			get
			{
				return (Int32Rect)base.GetValue(CroppedBitmap.SourceRectProperty);
			}
			set
			{
				base.SetValueInternal(CroppedBitmap.SourceRectProperty, value);
			}
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x0010ED2C File Offset: 0x0010E12C
		protected override Freezable CreateInstanceCore()
		{
			return new CroppedBitmap();
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x0010ED40 File Offset: 0x0010E140
		protected override void CloneCore(Freezable source)
		{
			CroppedBitmap otherCroppedBitmap = (CroppedBitmap)source;
			this.ClonePrequel(otherCroppedBitmap);
			base.CloneCore(source);
			this.ClonePostscript(otherCroppedBitmap);
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x0010ED6C File Offset: 0x0010E16C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			CroppedBitmap otherCroppedBitmap = (CroppedBitmap)source;
			this.ClonePrequel(otherCroppedBitmap);
			base.CloneCurrentValueCore(source);
			this.ClonePostscript(otherCroppedBitmap);
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x0010ED98 File Offset: 0x0010E198
		protected override void GetAsFrozenCore(Freezable source)
		{
			CroppedBitmap otherCroppedBitmap = (CroppedBitmap)source;
			this.ClonePrequel(otherCroppedBitmap);
			base.GetAsFrozenCore(source);
			this.ClonePostscript(otherCroppedBitmap);
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x0010EDC4 File Offset: 0x0010E1C4
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			CroppedBitmap otherCroppedBitmap = (CroppedBitmap)source;
			this.ClonePrequel(otherCroppedBitmap);
			base.GetCurrentValueAsFrozenCore(source);
			this.ClonePostscript(otherCroppedBitmap);
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x0010EDF0 File Offset: 0x0010E1F0
		static CroppedBitmap()
		{
			Type typeFromHandle = typeof(CroppedBitmap);
			CroppedBitmap.SourceProperty = Animatable.RegisterProperty("Source", typeof(BitmapSource), typeFromHandle, null, new PropertyChangedCallback(CroppedBitmap.SourcePropertyChanged), null, false, new CoerceValueCallback(CroppedBitmap.CoerceSource));
			CroppedBitmap.SourceRectProperty = Animatable.RegisterProperty("SourceRect", typeof(Int32Rect), typeFromHandle, Int32Rect.Empty, new PropertyChangedCallback(CroppedBitmap.SourceRectPropertyChanged), null, false, new CoerceValueCallback(CroppedBitmap.CoerceSourceRect));
		}

		// Token: 0x04001933 RID: 6451
		private BitmapSource _source;

		// Token: 0x04001934 RID: 6452
		private Int32Rect _sourceRect;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.Source" />.</summary>
		// Token: 0x04001935 RID: 6453
		public static readonly DependencyProperty SourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.CroppedBitmap.SourceRect" />.</summary>
		// Token: 0x04001936 RID: 6454
		public static readonly DependencyProperty SourceRectProperty;

		// Token: 0x04001937 RID: 6455
		internal static BitmapSource s_Source = null;

		// Token: 0x04001938 RID: 6456
		internal static Int32Rect s_SourceRect = Int32Rect.Empty;
	}
}
