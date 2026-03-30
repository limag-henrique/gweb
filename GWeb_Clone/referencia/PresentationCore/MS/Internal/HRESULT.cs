using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000667 RID: 1639
	internal struct HRESULT
	{
		// Token: 0x0600488F RID: 18575 RVA: 0x0011AEF4 File Offset: 0x0011A2F4
		internal static bool IsWindowsCodecError(int hr)
		{
			return (hr & 2147475456) == 144187392;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0011AF10 File Offset: 0x0011A310
		[SecuritySafeCritical]
		internal static Exception ConvertHRToException(int hr)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(hr, (IntPtr)(-1));
			if ((hr & 268435456) != 268435456)
			{
				if (hr <= -2003292404)
				{
					if (hr <= -2147022885)
					{
						if (hr == -2147024809)
						{
							return new ArgumentException(SR.Get("Media_InvalidArgument", null), exceptionForHR);
						}
						if (hr != -2147024362)
						{
							if (hr != -2147022885)
							{
								return exceptionForHR;
							}
							return new FileFormatException(null, SR.Get("Image_InvalidColorContext"), exceptionForHR);
						}
					}
					else if (hr <= -2003303155)
					{
						switch (hr)
						{
						case -2003304442:
							return new InvalidOperationException(SR.Get("Image_DisplayStateInvalid"), exceptionForHR);
						case -2003304441:
							return new ArithmeticException(SR.Get("Image_SingularMatrix"), exceptionForHR);
						case -2003304440:
						case -2003304439:
							return exceptionForHR;
						case -2003304438:
							return new ArithmeticException(SR.Get("Geometry_BadNumber"), exceptionForHR);
						default:
							switch (hr)
							{
							case -2003303161:
								return new InvalidWmpVersionException(SR.Get("Media_InvalidWmpVersion", null), exceptionForHR);
							case -2003303160:
								return new NotSupportedException(SR.Get("Media_InsufficientVideoResources", null), exceptionForHR);
							case -2003303159:
								return new NotSupportedException(SR.Get("Media_HardwareVideoAccelerationNotAvailable", null), exceptionForHR);
							case -2003303158:
							case -2003303157:
							case -2003303156:
								return exceptionForHR;
							case -2003303155:
								return new NotSupportedException(SR.Get("Media_PlayerIsClosed", null), exceptionForHR);
							default:
								return exceptionForHR;
							}
							break;
						}
					}
					else
					{
						switch (hr)
						{
						case -2003302400:
							return new ArgumentException(SR.Get("D3DImage_InvalidUsage"), exceptionForHR);
						case -2003302399:
							return new ArgumentException(SR.Get("D3DImage_SurfaceTooBig"), exceptionForHR);
						case -2003302398:
							return new ArgumentException(SR.Get("D3DImage_InvalidPool"), exceptionForHR);
						case -2003302397:
							return new ArgumentException(SR.Get("D3DImage_InvalidDevice"), exceptionForHR);
						case -2003302396:
							return new ArgumentException(SR.Get("D3DImage_AARequires9Ex"), exceptionForHR);
						default:
							switch (hr)
							{
							case -2003292412:
								return new InvalidOperationException(SR.Get("Image_WrongState"), exceptionForHR);
							case -2003292411:
								break;
							case -2003292410:
							case -2003292408:
							case -2003292407:
							case -2003292406:
								return exceptionForHR;
							case -2003292409:
								return new FileFormatException(null, SR.Get("Image_UnknownFormat"), exceptionForHR);
							case -2003292405:
								return new FileLoadException(SR.Get("MilErr_UnsupportedVersion"), exceptionForHR);
							case -2003292404:
								return new InvalidOperationException(SR.Get("WIC_NotInitialized"), exceptionForHR);
							default:
								return exceptionForHR;
							}
							break;
						}
					}
					return new OverflowException(SR.Get("Image_Overflow"), exceptionForHR);
				}
				if (hr <= -2003292301)
				{
					switch (hr)
					{
					case -2003292352:
						return new ArgumentException(SR.Get("Image_PropertyNotFound"), exceptionForHR);
					case -2003292351:
						return new NotSupportedException(SR.Get("Image_PropertyNotSupported"), exceptionForHR);
					case -2003292350:
						return new ArgumentException(SR.Get("Image_PropertySize"), exceptionForHR);
					case -2003292349:
						return new InvalidOperationException(SR.Get("Image_CodecPresent"), exceptionForHR);
					case -2003292348:
						return new NotSupportedException(SR.Get("Image_NoThumbnail"), exceptionForHR);
					case -2003292347:
						return new InvalidOperationException(SR.Get("Image_NoPalette"), exceptionForHR);
					case -2003292346:
						return new ArgumentException(SR.Get("Image_TooManyScanlines"), exceptionForHR);
					case -2003292345:
					case -2003292342:
					case -2003292341:
					case -2003292340:
					case -2003292339:
					case -2003292338:
					case -2003292337:
						return exceptionForHR;
					case -2003292344:
						return new InvalidOperationException(SR.Get("Image_InternalError"), exceptionForHR);
					case -2003292343:
						return new ArgumentException(SR.Get("Image_BadDimensions"), exceptionForHR);
					case -2003292336:
						break;
					case -2003292335:
						return new ArgumentException(SR.Get("Image_SizeOutOfRange"), exceptionForHR);
					case -2003292334:
						return new FileFormatException(null, SR.Get("Image_TooMuchMetadata"), exceptionForHR);
					default:
						switch (hr)
						{
						case -2003292320:
							goto IL_369;
						case -2003292319:
							return new FileFormatException(null, SR.Get("Image_HeaderError"), exceptionForHR);
						case -2003292318:
							return new ArgumentException(SR.Get("Image_FrameMissing"), exceptionForHR);
						case -2003292317:
							return new ArgumentException(SR.Get("Image_BadMetadataHeader"), exceptionForHR);
						default:
							switch (hr)
							{
							case -2003292304:
								return new ArgumentException(SR.Get("Image_BadStreamData"), exceptionForHR);
							case -2003292303:
								return new InvalidOperationException(SR.Get("Image_StreamWrite"), exceptionForHR);
							case -2003292302:
								return new IOException(SR.Get("Image_StreamRead"), exceptionForHR);
							case -2003292301:
								return new NotSupportedException(SR.Get("Image_StreamNotAvailable"), exceptionForHR);
							default:
								return exceptionForHR;
							}
							break;
						}
						break;
					}
				}
				else if (hr <= -1073741801)
				{
					switch (hr)
					{
					case -2003292288:
						return new NotSupportedException(SR.Get("Image_UnsupportedPixelFormat"), exceptionForHR);
					case -2003292287:
						return new NotSupportedException(SR.Get("Image_UnsupportedOperation"), exceptionForHR);
					case -2003292286:
					case -2003292285:
					case -2003292284:
					case -2003292283:
					case -2003292282:
					case -2003292281:
					case -2003292280:
					case -2003292279:
					case -2003292278:
						return exceptionForHR;
					case -2003292277:
						break;
					case -2003292276:
						return new ArgumentException(SR.Get("Image_InsufficientBuffer"), exceptionForHR);
					case -2003292275:
						return new FileFormatException(null, SR.Get("Image_DuplicateMetadataPresent"), exceptionForHR);
					case -2003292274:
						return new FileFormatException(null, SR.Get("Image_PropertyUnexpectedType"), exceptionForHR);
					case -2003292273:
						goto IL_369;
					case -2003292272:
						return new IOException(SR.Get("Image_InvalidQueryRequest"), exceptionForHR);
					case -2003292271:
						return new FileFormatException(null, SR.Get("Image_UnexpectedMetadataType"), exceptionForHR);
					case -2003292270:
						return new FileFormatException(null, SR.Get("Image_RequestOnlyValidAtMetadataRoot"), exceptionForHR);
					case -2003292269:
						return new IOException(SR.Get("Image_InvalidQueryCharacter"), exceptionForHR);
					default:
						if (hr != -1073741801)
						{
							return exceptionForHR;
						}
						return new OutOfMemoryException();
					}
				}
				else
				{
					if (hr == -1072885782)
					{
						return new FileNotFoundException(SR.Get("Media_DownloadFailed", null), exceptionForHR);
					}
					switch (hr)
					{
					case -1072885354:
						return new SecurityException(SR.Get("Media_LogonFailure"), exceptionForHR);
					case -1072885353:
						return new FileNotFoundException(SR.Get("Media_FileNotFound"), exceptionForHR);
					case -1072885352:
					case -1072885349:
					case -1072885348:
						return exceptionForHR;
					case -1072885351:
					case -1072885350:
						return new FileFormatException(SR.Get("Media_FileFormatNotSupported"), exceptionForHR);
					case -1072885347:
						return new FileFormatException(SR.Get("Media_PlaylistFormatNotSupported"), exceptionForHR);
					default:
						return exceptionForHR;
					}
				}
				return new NotSupportedException(SR.Get("Image_ComponentNotFound"), exceptionForHR);
				IL_369:
				return new FileFormatException(null, SR.Get("Image_DecoderError"), exceptionForHR);
			}
			int num = hr & -268435457;
			if (num == -1073741801)
			{
				return new OutOfMemoryException();
			}
			return exceptionForHR;
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0011B51C File Offset: 0x0011A91C
		public static void Check(int hr)
		{
			if (hr >= 0)
			{
				return;
			}
			throw HRESULT.ConvertHRToException(hr);
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x0011B534 File Offset: 0x0011A934
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0011B548 File Offset: 0x0011A948
		public static bool Failed(int hr)
		{
			return !HRESULT.Succeeded(hr);
		}

		// Token: 0x04001C54 RID: 7252
		internal const int FACILITY_NT_BIT = 268435456;

		// Token: 0x04001C55 RID: 7253
		internal const int FACILITY_MASK = 2147418112;

		// Token: 0x04001C56 RID: 7254
		internal const int FACILITY_WINCODEC_ERROR = 144179200;

		// Token: 0x04001C57 RID: 7255
		internal const int COMPONENT_MASK = 57344;

		// Token: 0x04001C58 RID: 7256
		internal const int COMPONENT_WINCODEC_ERROR = 8192;

		// Token: 0x04001C59 RID: 7257
		internal const int S_OK = 0;

		// Token: 0x04001C5A RID: 7258
		internal const int E_FAIL = -2147467259;

		// Token: 0x04001C5B RID: 7259
		internal const int E_OUTOFMEMORY = -2147024882;

		// Token: 0x04001C5C RID: 7260
		internal const int D3DERR_OUTOFVIDEOMEMORY = -2005532292;
	}
}
