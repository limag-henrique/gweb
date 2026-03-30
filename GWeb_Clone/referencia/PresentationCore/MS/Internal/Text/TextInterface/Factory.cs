using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Threading;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Interfaces;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200002D RID: 45
	internal sealed class Factory : CriticalHandle
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0001059C File Offset: 0x0000F99C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		static unsafe Factory()
		{
			Guid guid = new Guid("b859ee5a-d838-4b5b-a2e8-1adc7d93db48");
			Guid guid2 = guid;
			_GUID* ptr = (_GUID*)<Module>.@new(16UL);
			_GUID* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 16L);
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			_GUID guid3 = Util.ToGUID(ref guid2);
			cpblk(ptr2, ref guid3, 16);
			Factory._guidForIDWriteFactory = new NativePointerWrapper<_GUID>(ptr2);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011460 File Offset: 0x00010860
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private unsafe Factory(FactoryType factoryType, IFontSourceCollectionFactory fontSourceCollectionFactory, IFontSourceFactory fontSourceFactory) : base(IntPtr.Zero)
		{
			try
			{
				this.Initialize(factoryType);
				FontFileLoader fontFileLoader = new FontFileLoader(fontSourceFactory);
				this._wpfFontFileLoader = fontFileLoader;
				this._wpfFontCollectionLoader = new FontCollectionLoader(fontSourceCollectionFactory, fontFileLoader);
				this._fontSourceFactory = fontSourceFactory;
				IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(this._wpfFontFileLoader, typeof(IDWriteFontFileLoaderMirror));
				IDWriteFactory* pFactory = this._pFactory;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFileLoader*), pFactory, comInterfaceForObject.ToPointer(), *(*(long*)pFactory + 104L));
				Marshal.Release(comInterfaceForObject);
				Util.ConvertHresultToException(hr);
				IntPtr comInterfaceForObject2 = Marshal.GetComInterfaceForObject(this._wpfFontCollectionLoader, typeof(IDWriteFontCollectionLoaderMirror));
				IDWriteFactory* pFactory2 = this._pFactory;
				int hr2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontCollectionLoader*), pFactory2, comInterfaceForObject2.ToPointer(), *(*(long*)pFactory2 + 40L));
				Marshal.Release(comInterfaceForObject2);
				Util.ConvertHresultToException(hr2);
				GC.KeepAlive(this);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000105F4 File Offset: 0x0000F9F4
		[SecuritySafeCritical]
		private unsafe void Initialize(FactoryType factoryType)
		{
			method dwriteCreateFactoryFunctionPointer = <Module>.GetDWriteCreateFactoryFunctionPointer();
			IUnknown* pFactory;
			Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(MS.Internal.Text.TextInterface.Native.DWRITE_FACTORY_TYPE,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),MS.Internal.Text.TextInterface.Native.IUnknown**), DWriteTypeConverter.Convert(factoryType), Factory._guidForIDWriteFactory.Value, ref pFactory, dwriteCreateFactoryFunctionPointer));
			this._pFactory = (IDWriteFactory*)pFactory;
			GC.KeepAlive(this);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BAC8 File Offset: 0x0000AEC8
		private static void CleanupTimeStampCache()
		{
			Factory._timeStampCacheCleanupOp = null;
			Factory._timeStampCache.Clear();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BA00 File Offset: 0x0000AE00
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe sealed override bool ReleaseHandle()
		{
			if (this._wpfFontCollectionLoader != null)
			{
				IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(this._wpfFontCollectionLoader, typeof(IDWriteFontCollectionLoaderMirror));
				IDWriteFactory* pFactory = this._pFactory;
				object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontCollectionLoader*), pFactory, comInterfaceForObject.ToPointer(), *(*(long*)pFactory + 48L));
				Marshal.Release(comInterfaceForObject);
				this._wpfFontCollectionLoader = null;
			}
			if (this._wpfFontFileLoader != null)
			{
				IntPtr comInterfaceForObject2 = Marshal.GetComInterfaceForObject(this._wpfFontFileLoader, typeof(IDWriteFontFileLoaderMirror));
				IDWriteFactory* pFactory2 = this._pFactory;
				object obj2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFileLoader*), pFactory2, comInterfaceForObject2.ToPointer(), *(*(long*)pFactory2 + 112L));
				Marshal.Release(comInterfaceForObject2);
				this._wpfFontFileLoader = null;
			}
			IDWriteFactory* pFactory3 = this._pFactory;
			if (pFactory3 != null)
			{
				IDWriteFactory* ptr = pFactory3;
				object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this._pFactory = null;
			}
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B958 File Offset: 0x0000AD58
		internal unsafe IDWriteFactory* DWriteFactoryAddRef
		{
			[SecurityCritical]
			get
			{
				IDWriteFactory* pFactory = this._pFactory;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pFactory, *(*(long*)pFactory + 8L));
				GC.KeepAlive(this);
				return this._pFactory;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011E70 File Offset: 0x00011270
		[SecurityCritical]
		internal static Factory Create(FactoryType factoryType, IFontSourceCollectionFactory fontSourceCollectionFactory, IFontSourceFactory fontSourceFactory)
		{
			return new Factory(factoryType, fontSourceCollectionFactory, fontSourceFactory);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F4B0 File Offset: 0x0000E8B0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe static int CreateFontFile(IDWriteFactory* factory, FontFileLoader fontFileLoader, Uri filePathUri, IDWriteFontFile** dwriteFontFile)
		{
			int result;
			if (Factory.IsLocalUri(filePathUri))
			{
				ref ushort uint16_u0020modopt(IsConst)& = ref <Module>.CriticalPtrToStringChars(filePathUri.LocalPath);
				_FILETIME* ptr = null;
				System.Runtime.InteropServices.ComTypes.FILETIME value = default(System.Runtime.InteropServices.ComTypes.FILETIME);
				Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
				if (dispatcher != null)
				{
					if (Factory._timeStampCache == null)
					{
						Factory._timeStampCache = new Dictionary<Uri, System.Runtime.InteropServices.ComTypes.FILETIME>();
					}
					if (Factory._timeStampCache.TryGetValue(filePathUri, out value))
					{
						_FILETIME dwLowDateTime = value.dwLowDateTime;
						*(ref dwLowDateTime + 4) = value.dwHighDateTime;
						ptr = &dwLowDateTime;
					}
					else
					{
						void* ptr2 = <Module>.CreateFileW(ref uint16_u0020modopt(IsConst)&, int.MinValue, 7, null, 3, 268435456, null);
						if (ptr2 != -1L)
						{
							_BY_HANDLE_FILE_INFORMATION by_HANDLE_FILE_INFORMATION;
							if (<Module>.GetFileInformationByHandle(ptr2, &by_HANDLE_FILE_INFORMATION) != null)
							{
								value.dwLowDateTime = *(ref by_HANDLE_FILE_INFORMATION + 20);
								value.dwHighDateTime = *(ref by_HANDLE_FILE_INFORMATION + 24);
								Factory._timeStampCache.Add(filePathUri, value);
								if (Factory._timeStampCacheCleanupOp == null)
								{
									object[] args = new object[0];
									Factory._timeStampCacheCleanupOp = dispatcher.BeginInvoke(new Action(Factory.CleanupTimeStampCache), args);
								}
								_FILETIME dwLowDateTime;
								cpblk(ref dwLowDateTime, ref by_HANDLE_FILE_INFORMATION + 20, 8);
								ptr = &dwLowDateTime;
							}
							<Module>.CloseHandle(ptr2);
						}
					}
				}
				result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,_FILETIME modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.IDWriteFontFile**), factory, ref uint16_u0020modopt(IsConst)&, ptr, dwriteFontFile, *(*(long*)factory + 56L));
			}
			else
			{
				string absoluteUri = filePathUri.AbsoluteUri;
				ref ushort uint16_u0020modopt(IsConst)&2 = ref <Module>.CriticalPtrToStringChars(absoluteUri);
				IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(fontFileLoader, typeof(IDWriteFontFileLoaderMirror));
				result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFileLoader*,MS.Internal.Text.TextInterface.Native.IDWriteFontFile**), factory, ref uint16_u0020modopt(IsConst)&2, (uint)((long)(absoluteUri.Length + 1) * 2L), comInterfaceForObject.ToPointer(), dwriteFontFile, *(*(long*)factory + 64L));
				Marshal.Release(comInterfaceForObject);
			}
			return result;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001154C File Offset: 0x0001094C
		[SecurityCritical]
		internal unsafe FontFile CreateFontFile(Uri filePathUri)
		{
			IDWriteFontFile* fontFile = null;
			int num = Factory.CreateFontFile(this._pFactory, this._wpfFontFileLoader, filePathUri, &fontFile);
			if (num < 0)
			{
				this._fontSourceFactory.Create(filePathUri.AbsoluteUri).TestFileOpenable();
			}
			GC.KeepAlive(this);
			Util.ConvertHresultToException(num);
			return new FontFile(fontFile);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00011E88 File Offset: 0x00011288
		[SecurityCritical]
		internal FontFace CreateFontFace(Uri filePathUri, uint faceIndex)
		{
			return this.CreateFontFace(filePathUri, faceIndex, FontSimulations.None);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000115A0 File Offset: 0x000109A0
		[SecurityCritical]
		internal unsafe FontFace CreateFontFace(Uri filePathUri, uint faceIndex, FontSimulations fontSimulationFlags)
		{
			FontFile fontFile = this.CreateFontFile(filePathUri);
			uint num = 0U;
			DWRITE_FONT_FILE_TYPE dwrite_FONT_FILE_TYPE;
			DWRITE_FONT_FACE_TYPE dwrite_FONT_FACE_TYPE;
			int num2;
			if (!fontFile.Analyze(out dwrite_FONT_FILE_TYPE, out dwrite_FONT_FACE_TYPE, out num, ref num2))
			{
				if (num2 == -2003283968)
				{
					this._fontSourceFactory.Create(filePathUri.AbsoluteUri).TestFileOpenable();
				}
				Util.ConvertHresultToException(num2);
				GC.KeepAlive(this);
				return null;
			}
			if (faceIndex >= num)
			{
				throw new ArgumentOutOfRangeException("faceIndex");
			}
			byte b = DWriteTypeConverter.Convert(fontSimulationFlags);
			IDWriteFontFace* fontFace = null;
			IDWriteFontFile* dwriteFontFileNoAddRef = fontFile.DWriteFontFileNoAddRef;
			IDWriteFactory* pFactory = this._pFactory;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_FACE_TYPE,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFile* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_SIMULATIONS,MS.Internal.Text.TextInterface.Native.IDWriteFontFace**), pFactory, dwrite_FONT_FACE_TYPE, 1, ref dwriteFontFileNoAddRef, faceIndex, b, ref fontFace, *(*(long*)pFactory + 72L));
			GC.KeepAlive(fontFile);
			GC.KeepAlive(this);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new FontFace(fontFace);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011654 File Offset: 0x00010A54
		[SecuritySafeCritical]
		internal unsafe FontCollection GetSystemFontCollection([MarshalAs(UnmanagedType.U1)] bool checkForUpdates)
		{
			IDWriteFontCollection* fontCollection = null;
			IDWriteFactory* pFactory = this._pFactory;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontCollection**,System.Int32), pFactory, ref fontCollection, checkForUpdates, *(*(long*)pFactory + 24L));
			GC.KeepAlive(this);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new FontCollection(fontCollection);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011EA0 File Offset: 0x000112A0
		internal FontCollection GetSystemFontCollection()
		{
			return this.GetSystemFontCollection(false);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011694 File Offset: 0x00010A94
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe FontCollection GetFontCollection(Uri uri)
		{
			string absoluteUri = uri.AbsoluteUri;
			IDWriteFontCollection* fontCollection = null;
			ref ushort uint16_u0020modopt(IsConst)& = ref <Module>.CriticalPtrToStringChars(absoluteUri);
			IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(this._wpfFontCollectionLoader, typeof(IDWriteFontCollectionLoaderMirror));
			IDWriteFontCollectionLoader* ptr = (IDWriteFontCollectionLoader*)comInterfaceForObject.ToPointer();
			IDWriteFactory* pFactory = this._pFactory;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontCollectionLoader*,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontCollection**), pFactory, ptr, ref uint16_u0020modopt(IsConst)&, (uint)((long)(absoluteUri.Length + 1) * 2L), ref fontCollection, *(*(long*)pFactory + 32L));
			Marshal.Release(comInterfaceForObject);
			GC.KeepAlive(this);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new FontCollection(fontCollection);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B984 File Offset: 0x0000AD84
		[return: MarshalAs(UnmanagedType.U1)]
		internal static bool IsLocalUri(Uri uri)
		{
			int num;
			if (uri.IsFile && uri.IsLoopback && !uri.IsUnc)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return (byte)num != 0;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B9B4 File Offset: 0x0000ADB4
		[SecuritySafeCritical]
		internal unsafe static DWRITE_MATRIX GetIdentityTransform()
		{
			DWRITE_MATRIX result = 1f;
			*(ref result + 4) = 0f;
			*(ref result + 12) = 1f;
			*(ref result + 8) = 0f;
			*(ref result + 16) = 0f;
			*(ref result + 20) = 0f;
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011718 File Offset: 0x00010B18
		[SecuritySafeCritical]
		internal unsafe TextAnalyzer CreateTextAnalyzer()
		{
			IDWriteTextAnalyzer* textAnalyzer = null;
			IDWriteFactory* pFactory = this._pFactory;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalyzer**), pFactory, ref textAnalyzer, *(*(long*)pFactory + 168L));
			GC.KeepAlive(this);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new TextAnalyzer(textAnalyzer);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000BAE8 File Offset: 0x0000AEE8
		public override bool IsInvalid
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return ((this._pFactory == null) ? 1 : 0) != 0;
			}
		}

		// Token: 0x04000346 RID: 838
		[SecurityCritical]
		private static NativePointerWrapper<_GUID> _guidForIDWriteFactory;

		// Token: 0x04000347 RID: 839
		[SecurityCritical]
		private unsafe IDWriteFactory* _pFactory;

		// Token: 0x04000348 RID: 840
		[SecurityCritical]
		private FontCollectionLoader _wpfFontCollectionLoader;

		// Token: 0x04000349 RID: 841
		[SecurityCritical]
		private FontFileLoader _wpfFontFileLoader;

		// Token: 0x0400034A RID: 842
		private IFontSourceFactory _fontSourceFactory;

		// Token: 0x0400034B RID: 843
		[ThreadStatic]
		private static Dictionary<Uri, System.Runtime.InteropServices.ComTypes.FILETIME> _timeStampCache;

		// Token: 0x0400034C RID: 844
		[ThreadStatic]
		private static DispatcherOperation _timeStampCacheCleanupOp;
	}
}
