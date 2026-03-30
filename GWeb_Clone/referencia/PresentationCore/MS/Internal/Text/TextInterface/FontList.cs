using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000011 RID: 17
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal class FontList : IEnumerable<Font>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000BC24 File Offset: 0x0000B024
		protected NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontList> FontListObject
		{
			get
			{
				return this._fontList;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00010C70 File Offset: 0x00010070
		internal unsafe FontList(IDWriteFontList* fontList)
		{
			this._fontList = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontList>((IUnknown*)fontList);
		}

		// Token: 0x17000024 RID: 36
		internal unsafe Font this[uint A_0]
		{
			get
			{
				IDWriteFontList* value = this._fontList.Value;
				IDWriteFont* font;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFont**), value, A_0, ref font, *(*(long*)value + 40L));
				GC.KeepAlive(this._fontList);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new Font(font);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000FA9C File Offset: 0x0000EE9C
		internal unsafe uint Count
		{
			get
			{
				IDWriteFontList* value = this._fontList.Value;
				uint result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 32L));
				GC.KeepAlive(this._fontList);
				GC.KeepAlive(this);
				return result;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00010CD8 File Offset: 0x000100D8
		internal unsafe FontCollection FontsCollection
		{
			get
			{
				IDWriteFontList* value = this._fontList.Value;
				IDWriteFontCollection* fontCollection;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontCollection**), value, ref fontCollection, *(*(long*)value + 24L));
				GC.KeepAlive(this._fontList);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new FontCollection(fontCollection);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B83C File Offset: 0x0000AC3C
		public virtual IEnumerator<Font> GetEnumerator()
		{
			return new FontList.FontsEnumerator(this);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B83C File Offset: 0x0000AC3C
		public IEnumerator GetEnumerator2()
		{
			return new FontList.FontsEnumerator(this);
		}

		// Token: 0x04000324 RID: 804
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontList> _fontList;

		// Token: 0x02000012 RID: 18
		public class FontsEnumerator : IEnumerator<Font>
		{
			// Token: 0x060002B3 RID: 691 RVA: 0x0000B7F4 File Offset: 0x0000ABF4
			public FontsEnumerator(FontList fontList)
			{
				this._fontList = fontList;
				this._currentIndex = -1L;
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x00010538 File Offset: 0x0000F938
			[return: MarshalAs(UnmanagedType.U1)]
			public virtual bool MoveNext()
			{
				long num = (long)((ulong)this._fontList.Count);
				long currentIndex = this._currentIndex;
				if (currentIndex >= num)
				{
					return false;
				}
				this._currentIndex = currentIndex + 1L;
				long num2 = (long)((ulong)this._fontList.Count);
				return ((this._currentIndex < num2) ? 1 : 0) != 0;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x000113F8 File Offset: 0x000107F8
			public virtual Font Current
			{
				get
				{
					long num = (long)((ulong)this._fontList.Count);
					long currentIndex = this._currentIndex;
					if (currentIndex >= num)
					{
						throw new InvalidOperationException(LocalizedErrorMsgs.EnumeratorReachedEnd);
					}
					if (currentIndex == -1L)
					{
						throw new InvalidOperationException(LocalizedErrorMsgs.EnumeratorNotStarted);
					}
					return this._fontList[(uint)((int)currentIndex)];
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001144C File Offset: 0x0001084C
			public object Current2
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x0000B820 File Offset: 0x0000AC20
			public virtual void Reset()
			{
				this._currentIndex = -1L;
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000B790 File Offset: 0x0000AB90
			private void ~FontsEnumerator()
			{
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000B850 File Offset: 0x0000AC50
			protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
			{
				if (A_0)
				{
					this.~FontsEnumerator();
				}
				else
				{
					base.Finalize();
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000F474 File Offset: 0x0000E874
			public sealed void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000325 RID: 805
			public FontList _fontList;

			// Token: 0x04000326 RID: 806
			public long _currentIndex;
		}
	}
}
