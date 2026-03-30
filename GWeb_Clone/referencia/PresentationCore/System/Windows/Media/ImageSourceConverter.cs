using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte uma <see cref="T:System.Windows.Media.ImageSource" /> em/de outros tipos de dados.</summary>
	// Token: 0x02000418 RID: 1048
	public class ImageSourceConverter : TypeConverter
	{
		/// <summary>Determina se o conversor pode converter um objeto do tipo determinado em uma instância de <see cref="T:System.Windows.Media.ImageSource" />.</summary>
		/// <param name="context">Informações de contexto de tipo usadas para avaliar a conversão.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.Media.ImageSource" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002A2B RID: 10795 RVA: 0x000A918C File Offset: 0x000A858C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Stream) || sourceType == typeof(Uri) || sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.ImageSource" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Informações de contexto de tipo usadas para avaliar a conversão.</param>
		/// <param name="destinationType">O tipo desejado para o qual avaliar a conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter esta instância de <see cref="T:System.Windows.Media.ImageSource" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A instância <paramref name="context" /> não é um <see cref="T:System.Windows.Media.ImageSource" />.</exception>
		// Token: 0x06002A2C RID: 10796 RVA: 0x000A91EC File Offset: 0x000A85EC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.CanConvertTo(context, destinationType);
			}
			if (context == null || context.Instance == null)
			{
				return true;
			}
			if (!(context.Instance is ImageSource))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"ImageSource"
				}), "context");
			}
			ImageSource imageSource = (ImageSource)context.Instance;
			return imageSource.CanSerializeToString();
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.Media.ImageSource" />.</summary>
		/// <param name="context">Informações de contexto de tipo usadas para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.ImageSource" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou é um tipo inválido.</exception>
		// Token: 0x06002A2D RID: 10797 RVA: 0x000A9264 File Offset: 0x000A8664
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			object result;
			try
			{
				if (value == null)
				{
					throw base.GetConvertFromException(value);
				}
				if ((value is string && !string.IsNullOrEmpty((string)value)) || value is Uri)
				{
					UriHolder uriFromUriContext = TypeConverterHelper.GetUriFromUriContext(context, value);
					result = BitmapFrame.CreateFromUriOrStream(uriFromUriContext.BaseUri, uriFromUriContext.OriginalUri, null, BitmapCreateOptions.None, BitmapCacheOption.Default, null);
				}
				else
				{
					if (value is byte[])
					{
						byte[] array = (byte[])value;
						if (array != null)
						{
							Stream stream = this.GetBitmapStream(array);
							if (stream == null)
							{
								stream = new MemoryStream(array);
							}
							return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
						}
					}
					else if (value is Stream)
					{
						Stream bitmapStream = (Stream)value;
						return BitmapFrame.Create(bitmapStream, BitmapCreateOptions.None, BitmapCacheOption.Default);
					}
					result = base.ConvertFrom(context, culture, value);
				}
			}
			catch (Exception ex)
			{
				if (!CriticalExceptions.IsCriticalException(ex))
				{
					if (context == null && CoreAppContextSwitches.OverrideExceptionWithNullReferenceException)
					{
						throw new NullReferenceException();
					}
					IProvideValueTarget provideValueTarget = ((context != null) ? context.GetService(typeof(IProvideValueTarget)) : null) as IProvideValueTarget;
					if (provideValueTarget != null)
					{
						IProvidePropertyFallback providePropertyFallback = provideValueTarget.TargetObject as IProvidePropertyFallback;
						DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;
						if (providePropertyFallback != null && dependencyProperty != null && providePropertyFallback.CanProvidePropertyFallback(dependencyProperty.Name))
						{
							return providePropertyFallback.ProvidePropertyFallback(dependencyProperty.Name, ex);
						}
					}
				}
				throw;
			}
			return result;
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.ImageSource" /> em um tipo especificado.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">
		///   <see cref="T:System.Windows.Media.ImageSource" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>Uma nova instância do <paramref name="destinationType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido.  
		///
		/// ou - 
		/// A instância <paramref name="context" /> não pode ser serializada para uma cadeia de caracteres.</exception>
		// Token: 0x06002A2E RID: 10798 RVA: 0x000A93C4 File Offset: 0x000A87C4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is ImageSource)
			{
				ImageSource imageSource = (ImageSource)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !imageSource.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return imageSource.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000A9434 File Offset: 0x000A8834
		[SecurityCritical]
		private unsafe Stream GetBitmapStream(byte[] rawData)
		{
			fixed (byte[] array = rawData)
			{
				byte* ptr;
				if (rawData == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				IntPtr intPtr = (IntPtr)((void*)ptr);
				if (intPtr == IntPtr.Zero)
				{
					return null;
				}
				if (Marshal.ReadInt16(intPtr) != 7189)
				{
					return null;
				}
				ImageSourceConverter.OBJECTHEADER objectheader = (ImageSourceConverter.OBJECTHEADER)Marshal.PtrToStructure(intPtr, typeof(ImageSourceConverter.OBJECTHEADER));
				string @string = Encoding.ASCII.GetString(rawData, (int)(objectheader.headersize + 12), 6);
				if (@string != "PBrush")
				{
					return null;
				}
				byte[] bytes = Encoding.ASCII.GetBytes("BM");
				for (int i = (int)(objectheader.headersize + 18); i < (int)(objectheader.headersize + 510); i++)
				{
					if (bytes[0] == ptr[i] && bytes[1] == ptr[i + 1])
					{
						return new MemoryStream(rawData, i, rawData.Length - i);
					}
				}
			}
			return null;
		}

		// Token: 0x02000890 RID: 2192
		private struct OBJECTHEADER
		{
			// Token: 0x040028DF RID: 10463
			public short signature;

			// Token: 0x040028E0 RID: 10464
			public short headersize;

			// Token: 0x040028E1 RID: 10465
			public short objectType;

			// Token: 0x040028E2 RID: 10466
			public short nameLen;

			// Token: 0x040028E3 RID: 10467
			public short classLen;

			// Token: 0x040028E4 RID: 10468
			public short nameOffset;

			// Token: 0x040028E5 RID: 10469
			public short classOffset;

			// Token: 0x040028E6 RID: 10470
			public short width;

			// Token: 0x040028E7 RID: 10471
			public short height;

			// Token: 0x040028E8 RID: 10472
			public IntPtr pInfo;
		}
	}
}
