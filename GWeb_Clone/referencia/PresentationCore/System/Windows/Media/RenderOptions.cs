using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Interop;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece opções para controlar o comportamento de renderização de objetos.</summary>
	// Token: 0x02000437 RID: 1079
	public static class RenderOptions
	{
		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência do qual o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> será recuperado.</param>
		/// <returns>O valor atual da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> no objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C23 RID: 11299 RVA: 0x000B0508 File Offset: 0x000AF908
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static EdgeMode GetEdgeMode(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (EdgeMode)target.GetValue(RenderOptions.EdgeModeProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> em um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> é definido</param>
		/// <param name="edgeMode">O novo valor a ser definido para a propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C24 RID: 11300 RVA: 0x000B0534 File Offset: 0x000AF934
		public static void SetEdgeMode(DependencyObject target, EdgeMode edgeMode)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.EdgeModeProperty, edgeMode);
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência do qual o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> será recuperado.</param>
		/// <returns>O valor atual da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> no objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C25 RID: 11301 RVA: 0x000B0560 File Offset: 0x000AF960
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static BitmapScalingMode GetBitmapScalingMode(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (BitmapScalingMode)target.GetValue(RenderOptions.BitmapScalingModeProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> em um objeto de dependência especificado.</summary>
		/// <param name="target">O descendente <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.Media.DrawingGroup" /> no qual o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> será definido.</param>
		/// <param name="bitmapScalingMode">O novo valor a ser definido para a propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C26 RID: 11302 RVA: 0x000B058C File Offset: 0x000AF98C
		public static void SetBitmapScalingMode(DependencyObject target, BitmapScalingMode bitmapScalingMode)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.BitmapScalingModeProperty, bitmapScalingMode);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" /> do elemento especificado.</summary>
		/// <param name="target">O <see cref="T:System.Windows.DependencyObject" /> para o qual recuperar a propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" />.</param>
		/// <returns>O valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" /> para <paramref name="target" />.</returns>
		// Token: 0x06002C27 RID: 11303 RVA: 0x000B05B8 File Offset: 0x000AF9B8
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static ClearTypeHint GetClearTypeHint(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (ClearTypeHint)target.GetValue(RenderOptions.ClearTypeHintProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" /> do elemento especificado.</summary>
		/// <param name="target">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" />.</param>
		/// <param name="clearTypeHint">O novo valor <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" />.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C28 RID: 11304 RVA: 0x000B05E4 File Offset: 0x000AF9E4
		public static void SetClearTypeHint(DependencyObject target, ClearTypeHint clearTypeHint)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.ClearTypeHintProperty, clearTypeHint);
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CachingHint" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência do qual o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CachingHint" /> será recuperado.</param>
		/// <returns>O valor atual da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CachingHint" /> no objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C29 RID: 11305 RVA: 0x000B0610 File Offset: 0x000AFA10
		[AttachedPropertyBrowsableForType(typeof(TileBrush))]
		public static CachingHint GetCachingHint(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (CachingHint)target.GetValue(RenderOptions.CachingHintProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CachingHint" /> em um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual definir o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CachingHint" />.</param>
		/// <param name="cachingHint">O novo valor a ser definido para a propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C2A RID: 11306 RVA: 0x000B063C File Offset: 0x000AFA3C
		public static void SetCachingHint(DependencyObject target, CachingHint cachingHint)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.CachingHintProperty, cachingHint);
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência do qual o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> será recuperado.</param>
		/// <returns>O valor atual da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> no objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C2B RID: 11307 RVA: 0x000B0668 File Offset: 0x000AFA68
		[AttachedPropertyBrowsableForType(typeof(TileBrush))]
		public static double GetCacheInvalidationThresholdMinimum(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (double)target.GetValue(RenderOptions.CacheInvalidationThresholdMinimumProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> em um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> é definido</param>
		/// <param name="cacheInvalidationThresholdMinimum">O novo valor a ser definido para a propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C2C RID: 11308 RVA: 0x000B0694 File Offset: 0x000AFA94
		public static void SetCacheInvalidationThresholdMinimum(DependencyObject target, double cacheInvalidationThresholdMinimum)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.CacheInvalidationThresholdMinimumProperty, cacheInvalidationThresholdMinimum);
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência do qual o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> será recuperado.</param>
		/// <returns>O valor atual da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> no objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C2D RID: 11309 RVA: 0x000B06C0 File Offset: 0x000AFAC0
		[AttachedPropertyBrowsableForType(typeof(TileBrush))]
		public static double GetCacheInvalidationThresholdMaximum(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (double)target.GetValue(RenderOptions.CacheInvalidationThresholdMaximumProperty);
		}

		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> em um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual o valor da propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> é definido</param>
		/// <param name="cacheInvalidationThresholdMaximum">O novo valor a ser definido para a propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="target" /> especificado é <see langword="null" />.</exception>
		// Token: 0x06002C2E RID: 11310 RVA: 0x000B06EC File Offset: 0x000AFAEC
		public static void SetCacheInvalidationThresholdMaximum(DependencyObject target, double cacheInvalidationThresholdMaximum)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(RenderOptions.CacheInvalidationThresholdMaximumProperty, cacheInvalidationThresholdMaximum);
		}

		/// <summary>Especifica a preferência de modo de renderização para o processo atual.</summary>
		/// <returns>A preferência <see cref="T:System.Windows.Interop.RenderMode" /> para o processo atual.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000B0718 File Offset: 0x000AFB18
		// (set) Token: 0x06002C30 RID: 11312 RVA: 0x000B0730 File Offset: 0x000AFB30
		public static RenderMode ProcessRenderMode
		{
			[SecurityCritical]
			get
			{
				if (!UnsafeNativeMethods.MilCoreApi.RenderOptions_IsSoftwareRenderingForcedForProcess())
				{
					return RenderMode.Default;
				}
				return RenderMode.SoftwareOnly;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (value != RenderMode.Default && value != RenderMode.SoftwareOnly)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(RenderMode));
				}
				UnsafeNativeMethods.MilCoreApi.RenderOptions_ForceSoftwareRenderingModeForProcess(value == RenderMode.SoftwareOnly);
			}
		}

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.EdgeMode" /> anexada.</summary>
		// Token: 0x04001420 RID: 5152
		public static readonly DependencyProperty EdgeModeProperty = DependencyProperty.RegisterAttached("EdgeMode", typeof(EdgeMode), typeof(RenderOptions), new UIPropertyMetadata(EdgeMode.Unspecified), new ValidateValueCallback(ValidateEnums.IsEdgeModeValid));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.BitmapScalingMode" /> anexada.</summary>
		// Token: 0x04001421 RID: 5153
		public static readonly DependencyProperty BitmapScalingModeProperty = DependencyProperty.RegisterAttached("BitmapScalingMode", typeof(BitmapScalingMode), typeof(RenderOptions), new UIPropertyMetadata(BitmapScalingMode.Unspecified), new ValidateValueCallback(ValidateEnums.IsBitmapScalingModeValid));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.ClearTypeHint" /> anexada.</summary>
		// Token: 0x04001422 RID: 5154
		public static readonly DependencyProperty ClearTypeHintProperty = DependencyProperty.RegisterAttached("ClearTypeHint", typeof(ClearTypeHint), typeof(RenderOptions), new UIPropertyMetadata(ClearTypeHint.Auto), new ValidateValueCallback(ValidateEnums.IsClearTypeHintValid));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.CachingHint" /> anexada.</summary>
		// Token: 0x04001423 RID: 5155
		public static readonly DependencyProperty CachingHintProperty = DependencyProperty.RegisterAttached("CachingHint", typeof(CachingHint), typeof(RenderOptions), new UIPropertyMetadata(CachingHint.Unspecified), new ValidateValueCallback(ValidateEnums.IsCachingHintValid));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMinimum" /> anexada.</summary>
		// Token: 0x04001424 RID: 5156
		public static readonly DependencyProperty CacheInvalidationThresholdMinimumProperty = DependencyProperty.RegisterAttached("CacheInvalidationThresholdMinimum", typeof(double), typeof(RenderOptions), new UIPropertyMetadata(0.707), null);

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Media.RenderOptions.CacheInvalidationThresholdMaximum" /> anexada.</summary>
		// Token: 0x04001425 RID: 5157
		public static readonly DependencyProperty CacheInvalidationThresholdMaximumProperty = DependencyProperty.RegisterAttached("CacheInvalidationThresholdMaximum", typeof(double), typeof(RenderOptions), new UIPropertyMetadata(1.414), null);
	}
}
