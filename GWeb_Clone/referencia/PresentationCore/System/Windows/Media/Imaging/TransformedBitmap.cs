using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Dimensiona e gira um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
	// Token: 0x020005FF RID: 1535
	public sealed class TransformedBitmap : BitmapSource, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />.</summary>
		// Token: 0x0600463F RID: 17983 RVA: 0x00112C9C File Offset: 0x0011209C
		public TransformedBitmap() : base(true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" /> que tem o <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Source" /> e o <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Transform" /> especificados.</summary>
		/// <param name="source">O <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Source" /> da nova instância <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />.</param>
		/// <param name="newTransform">O <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Transform" /> da nova instância <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />.</param>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="source" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">O parâmetro <paramref name="newTransform" /> é <see langword="null" />.  
		///
		/// ou - 
		/// A transformação não é uma transformação ortogonal.</exception>
		// Token: 0x06004640 RID: 17984 RVA: 0x00112CB0 File Offset: 0x001120B0
		public TransformedBitmap(BitmapSource source, Transform newTransform) : base(true)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (newTransform == null)
			{
				throw new InvalidOperationException(SR.Get("Image_NoArgument", new object[]
				{
					"Transform"
				}));
			}
			if (!this.CheckTransform(newTransform))
			{
				throw new InvalidOperationException(SR.Get("Image_OnlyOrthogonal"));
			}
			this._bitmapInit.BeginInit();
			this.Source = source;
			this.Transform = newTransform;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		/// <summary>Sinaliza o início da inicialização <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" /> está sendo inicializado no momento. O <see cref="M:System.Windows.Media.Imaging.TransformedBitmap.BeginInit" /> já foi chamado.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" /> já foi inicializado.</exception>
		// Token: 0x06004641 RID: 17985 RVA: 0x00112D38 File Offset: 0x00112138
		public void BeginInit()
		{
			base.WritePreamble();
			this._bitmapInit.BeginInit();
		}

		/// <summary>Sinaliza o término da inicialização do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">As propriedades <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Source" /> ou <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Transform" /> são <see langword="null" />.  
		///
		/// ou - 
		/// A transformação não é uma transformação ortogonal.  
		///
		/// ou - 
		/// O método <see cref="M:System.Windows.Media.Imaging.TransformedBitmap.EndInit" /> é chamado sem primeiro chamar <see cref="M:System.Windows.Media.Imaging.TransformedBitmap.BeginInit" />.</exception>
		// Token: 0x06004642 RID: 17986 RVA: 0x00112D58 File Offset: 0x00112158
		[SecurityCritical]
		public void EndInit()
		{
			base.WritePreamble();
			this._bitmapInit.EndInit();
			this.IsValidForFinalizeCreation(true);
			this.FinalizeCreation();
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x00112D84 File Offset: 0x00112184
		private void ClonePrequel(TransformedBitmap otherTransformedBitmap)
		{
			this.BeginInit();
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x00112D98 File Offset: 0x00112198
		private void ClonePostscript(TransformedBitmap otherTransformedBitmap)
		{
			this.EndInit();
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x00112DAC File Offset: 0x001121AC
		internal bool CheckTransform(Transform newTransform)
		{
			Matrix value = newTransform.Value;
			bool result = false;
			if ((DoubleUtil.IsZero(value.M11) && DoubleUtil.IsZero(value.M22)) || (DoubleUtil.IsZero(value.M12) && DoubleUtil.IsZero(value.M21)))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x00112E00 File Offset: 0x00112200
		internal void GetParamsFromTransform(Transform newTransform, out double scaleX, out double scaleY, out WICBitmapTransformOptions options)
		{
			Matrix value = newTransform.Value;
			if (DoubleUtil.IsZero(value.M12) && DoubleUtil.IsZero(value.M21))
			{
				scaleX = Math.Abs(value.M11);
				scaleY = Math.Abs(value.M22);
				options = WICBitmapTransformOptions.WICBitmapTransformRotate0;
				if (value.M11 < 0.0)
				{
					options |= WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal;
				}
				if (value.M22 < 0.0)
				{
					options |= WICBitmapTransformOptions.WICBitmapTransformFlipVertical;
					return;
				}
			}
			else
			{
				scaleX = Math.Abs(value.M12);
				scaleY = Math.Abs(value.M21);
				options = WICBitmapTransformOptions.WICBitmapTransformRotate90;
				if (value.M12 < 0.0)
				{
					options |= WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal;
				}
				if (value.M21 >= 0.0)
				{
					options |= WICBitmapTransformOptions.WICBitmapTransformFlipVertical;
				}
			}
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00112EDC File Offset: 0x001122DC
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			this._bitmapInit.EnsureInitializedComplete();
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			double num;
			double num2;
			WICBitmapTransformOptions wicbitmapTransformOptions;
			this.GetParamsFromTransform(this.Transform, out num, out num2, out wicbitmapTransformOptions);
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					IntPtr imagingFactoryPtr = factoryMaker.ImagingFactoryPtr;
					bitmapSourceSafeMILHandle = this._source.WicSourceHandle;
					if (!DoubleUtil.IsOne(num) || !DoubleUtil.IsOne(num2))
					{
						uint width = Math.Max(1U, (uint)(num * (double)this._source.PixelWidth + 0.5));
						uint height = Math.Max(1U, (uint)(num2 * (double)this._source.PixelHeight + 0.5));
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapScaler(imagingFactoryPtr, out bitmapSourceSafeMILHandle));
						object syncObject = this._syncObject;
						lock (syncObject)
						{
							HRESULT.Check(UnsafeNativeMethods.WICBitmapScaler.Initialize(bitmapSourceSafeMILHandle, this._source.WicSourceHandle, width, height, WICInterpolationMode.Fant));
						}
					}
					if (wicbitmapTransformOptions != WICBitmapTransformOptions.WICBitmapTransformRotate0)
					{
						bitmapSourceSafeMILHandle = BitmapSource.CreateCachedBitmap(null, bitmapSourceSafeMILHandle, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default, this._source.Palette);
						BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle2 = null;
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFlipRotator(imagingFactoryPtr, out bitmapSourceSafeMILHandle2));
						object syncObject2 = this._syncObject;
						lock (syncObject2)
						{
							HRESULT.Check(UnsafeNativeMethods.WICBitmapFlipRotator.Initialize(bitmapSourceSafeMILHandle2, bitmapSourceSafeMILHandle, wicbitmapTransformOptions));
						}
						bitmapSourceSafeMILHandle = bitmapSourceSafeMILHandle2;
					}
					if (wicbitmapTransformOptions == WICBitmapTransformOptions.WICBitmapTransformRotate0 && DoubleUtil.IsOne(num) && DoubleUtil.IsOne(num2))
					{
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFlipRotator(imagingFactoryPtr, out bitmapSourceSafeMILHandle));
						object syncObject3 = this._syncObject;
						lock (syncObject3)
						{
							HRESULT.Check(UnsafeNativeMethods.WICBitmapFlipRotator.Initialize(bitmapSourceSafeMILHandle, this._source.WicSourceHandle, WICBitmapTransformOptions.WICBitmapTransformRotate0));
						}
					}
					base.WicSourceHandle = bitmapSourceSafeMILHandle;
					this._isSourceCached = this._source.IsSourceCached;
				}
				catch
				{
					this._bitmapInit.Reset();
					throw;
				}
			}
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0011313C File Offset: 0x0011253C
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

		// Token: 0x06004649 RID: 17993 RVA: 0x0011318C File Offset: 0x0011258C
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
			else
			{
				Transform transform = this.Transform;
				if (transform == null)
				{
					if (throwIfInvalid)
					{
						throw new InvalidOperationException(SR.Get("Image_NoArgument", new object[]
						{
							"Transform"
						}));
					}
					return false;
				}
				else
				{
					if (this.CheckTransform(transform))
					{
						return true;
					}
					if (throwIfInvalid)
					{
						throw new InvalidOperationException(SR.Get("Image_OnlyOrthogonal"));
					}
					return false;
				}
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x00113210 File Offset: 0x00112610
		private void TransformPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._transform = (e.NewValue as Transform);
			}
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x00113238 File Offset: 0x00112638
		private static object CoerceSource(DependencyObject d, object value)
		{
			TransformedBitmap transformedBitmap = (TransformedBitmap)d;
			if (!transformedBitmap._bitmapInit.IsInInit)
			{
				return transformedBitmap._source;
			}
			return value;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00113264 File Offset: 0x00112664
		private static object CoerceTransform(DependencyObject d, object value)
		{
			TransformedBitmap transformedBitmap = (TransformedBitmap)d;
			if (!transformedBitmap._bitmapInit.IsInInit)
			{
				return transformedBitmap._transform;
			}
			return value;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600464D RID: 17997 RVA: 0x00113290 File Offset: 0x00112690
		public new TransformedBitmap Clone()
		{
			return (TransformedBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.TransformedBitmap" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600464E RID: 17998 RVA: 0x001132A8 File Offset: 0x001126A8
		public new TransformedBitmap CloneCurrentValue()
		{
			return (TransformedBitmap)base.CloneCurrentValue();
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x001132C0 File Offset: 0x001126C0
		private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TransformedBitmap transformedBitmap = (TransformedBitmap)d;
			transformedBitmap.SourcePropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			transformedBitmap.PropertyChanged(TransformedBitmap.SourceProperty);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00113300 File Offset: 0x00112700
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TransformedBitmap transformedBitmap = (TransformedBitmap)d;
			transformedBitmap.TransformPropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			transformedBitmap.PropertyChanged(TransformedBitmap.TransformProperty);
		}

		/// <summary>Obtém ou define a origem do bitmap.</summary>
		/// <returns>A fonte de bitmap. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x00113340 File Offset: 0x00112740
		// (set) Token: 0x06004652 RID: 18002 RVA: 0x00113360 File Offset: 0x00112760
		public BitmapSource Source
		{
			get
			{
				return (BitmapSource)base.GetValue(TransformedBitmap.SourceProperty);
			}
			set
			{
				base.SetValueInternal(TransformedBitmap.SourceProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Transform" />, que especifica a escala ou a rotação do bitmap.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Transform" />, que especifica a escala ou a rotação do bitmap. O valor padrão é <see cref="P:System.Windows.Media.Transform.Identity" />.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x0011337C File Offset: 0x0011277C
		// (set) Token: 0x06004654 RID: 18004 RVA: 0x0011339C File Offset: 0x0011279C
		public Transform Transform
		{
			get
			{
				return (Transform)base.GetValue(TransformedBitmap.TransformProperty);
			}
			set
			{
				base.SetValueInternal(TransformedBitmap.TransformProperty, value);
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001133B8 File Offset: 0x001127B8
		protected override Freezable CreateInstanceCore()
		{
			return new TransformedBitmap();
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x001133CC File Offset: 0x001127CC
		protected override void CloneCore(Freezable source)
		{
			TransformedBitmap otherTransformedBitmap = (TransformedBitmap)source;
			this.ClonePrequel(otherTransformedBitmap);
			base.CloneCore(source);
			this.ClonePostscript(otherTransformedBitmap);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x001133F8 File Offset: 0x001127F8
		protected override void CloneCurrentValueCore(Freezable source)
		{
			TransformedBitmap otherTransformedBitmap = (TransformedBitmap)source;
			this.ClonePrequel(otherTransformedBitmap);
			base.CloneCurrentValueCore(source);
			this.ClonePostscript(otherTransformedBitmap);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x00113424 File Offset: 0x00112824
		protected override void GetAsFrozenCore(Freezable source)
		{
			TransformedBitmap otherTransformedBitmap = (TransformedBitmap)source;
			this.ClonePrequel(otherTransformedBitmap);
			base.GetAsFrozenCore(source);
			this.ClonePostscript(otherTransformedBitmap);
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x00113450 File Offset: 0x00112850
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			TransformedBitmap otherTransformedBitmap = (TransformedBitmap)source;
			this.ClonePrequel(otherTransformedBitmap);
			base.GetCurrentValueAsFrozenCore(source);
			this.ClonePostscript(otherTransformedBitmap);
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0011347C File Offset: 0x0011287C
		static TransformedBitmap()
		{
			Type typeFromHandle = typeof(TransformedBitmap);
			TransformedBitmap.SourceProperty = Animatable.RegisterProperty("Source", typeof(BitmapSource), typeFromHandle, null, new PropertyChangedCallback(TransformedBitmap.SourcePropertyChanged), null, false, new CoerceValueCallback(TransformedBitmap.CoerceSource));
			TransformedBitmap.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform), typeFromHandle, Transform.Identity, new PropertyChangedCallback(TransformedBitmap.TransformPropertyChanged), null, false, new CoerceValueCallback(TransformedBitmap.CoerceTransform));
		}

		// Token: 0x0400199F RID: 6559
		private BitmapSource _source;

		// Token: 0x040019A0 RID: 6560
		private Transform _transform;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Source" />.</summary>
		// Token: 0x040019A1 RID: 6561
		public static readonly DependencyProperty SourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.TransformedBitmap.Transform" />.</summary>
		// Token: 0x040019A2 RID: 6562
		public static readonly DependencyProperty TransformProperty;

		// Token: 0x040019A3 RID: 6563
		internal static BitmapSource s_Source = null;

		// Token: 0x040019A4 RID: 6564
		internal static Transform s_Transform = Transform.Identity;
	}
}
