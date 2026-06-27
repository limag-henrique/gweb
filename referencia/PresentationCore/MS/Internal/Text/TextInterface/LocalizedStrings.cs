using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200000D RID: 13
	internal sealed class LocalizedStrings : IDictionary<CultureInfo, string>
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00010DB4 File Offset: 0x000101B4
		[SecuritySafeCritical]
		private unsafe uint GetLocaleNameLength(uint index)
		{
			NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings> localizedStrings = this._localizedStrings;
			if (localizedStrings == null)
			{
				return 0U;
			}
			uint result = 0U;
			IDWriteLocalizedStrings* value = localizedStrings.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.UInt32*), value, index, ref result, *(*(long*)value + 40L));
			GC.KeepAlive(this._localizedStrings);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010EA4 File Offset: 0x000102A4
		[SecuritySafeCritical]
		private unsafe uint GetStringLength(uint index)
		{
			NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings> localizedStrings = this._localizedStrings;
			if (localizedStrings == null)
			{
				return 0U;
			}
			uint result = 0U;
			IDWriteLocalizedStrings* value = localizedStrings.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.UInt32*), value, index, ref result, *(*(long*)value + 56L));
			GC.KeepAlive(this._localizedStrings);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00011AEC File Offset: 0x00010EEC
		private CultureInfo[] KeysArray
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new CultureInfo[this.StringsCount];
					uint num = 0U;
					if (0U < this.StringsCount)
					{
						do
						{
							this._keys[(int)num] = new CultureInfo(this.GetLocaleName(num));
							num += 1U;
						}
						while (num < this.StringsCount);
					}
				}
				return this._keys;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00011B44 File Offset: 0x00010F44
		private string[] ValuesArray
		{
			get
			{
				if (this._values == null)
				{
					this._values = new string[this.StringsCount];
					uint num = 0U;
					if (0U < this.StringsCount)
					{
						do
						{
							this._values[(int)num] = this.GetString(num);
							num += 1U;
						}
						while (num < this.StringsCount);
					}
				}
				return this._values;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BC38 File Offset: 0x0000B038
		[SecuritySafeCritical]
		internal LocalizedStrings()
		{
			this._localizedStrings = null;
			this._keys = null;
			this._values = null;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010D1C File Offset: 0x0001011C
		[SecurityCritical]
		internal unsafe LocalizedStrings(IDWriteLocalizedStrings* localizedStrings)
		{
			this._localizedStrings = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings>((IUnknown*)localizedStrings);
			this._keys = null;
			this._values = null;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000FAD4 File Offset: 0x0000EED4
		internal unsafe uint StringsCount
		{
			[SecuritySafeCritical]
			get
			{
				NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings> localizedStrings = this._localizedStrings;
				uint result;
				if (localizedStrings != null)
				{
					IDWriteLocalizedStrings* value = localizedStrings.Value;
					result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 24L));
				}
				else
				{
					result = 0U;
				}
				GC.KeepAlive(this._localizedStrings);
				GC.KeepAlive(this);
				return result;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010D4C File Offset: 0x0001014C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool FindLocaleName(string localeName, out uint index)
		{
			if (this._localizedStrings == null)
			{
				index = 0U;
				return false;
			}
			ref ushort uint16_u0020modopt(IsConst)& = ref <Module>.CriticalPtrToStringChars(localeName);
			int num = 0;
			uint num2 = 0U;
			IDWriteLocalizedStrings* value = this._localizedStrings.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32*,System.Int32*), value, ref uint16_u0020modopt(IsConst)&, ref num2, ref num, *(*(long*)value + 32L));
			GC.KeepAlive(this._localizedStrings);
			Util.ConvertHresultToException(hr);
			index = num2;
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010E00 File Offset: 0x00010200
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe string GetLocaleName(uint index)
		{
			if (this._localizedStrings == null)
			{
				return string.Empty;
			}
			uint localeNameLength = this.GetLocaleNameLength(index);
			byte condition = (localeNameLength < uint.MaxValue) ? 1 : 0;
			Invariant.Assert(condition != 0);
			ushort* ptr = null;
			string result;
			try
			{
				ushort* ptr2 = (ushort*)<Module>.@new((ulong)(localeNameLength + 1U) * 2UL);
				ptr = ptr2;
				IDWriteLocalizedStrings* value = this._localizedStrings.Value;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.UInt16*,System.UInt32), value, index, ptr2, localeNameLength + 1U, *(*(long*)value + 48L));
				GC.KeepAlive(this._localizedStrings);
				Util.ConvertHresultToException(hr);
				result = new string((char*)ptr2);
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.delete((void*)ptr);
				}
			}
			return result;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00010EF0 File Offset: 0x000102F0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe string GetString(uint index)
		{
			if (this._localizedStrings == null)
			{
				return string.Empty;
			}
			uint stringLength = this.GetStringLength(index);
			byte condition = (stringLength < uint.MaxValue) ? 1 : 0;
			Invariant.Assert(condition != 0);
			ushort* ptr = null;
			string result;
			try
			{
				ushort* ptr2 = (ushort*)<Module>.@new((ulong)(stringLength + 1U) * 2UL);
				ptr = ptr2;
				IDWriteLocalizedStrings* value = this._localizedStrings.Value;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.UInt16*,System.UInt32), value, index, ptr2, stringLength + 1U, *(*(long*)value + 64L));
				GC.KeepAlive(this._localizedStrings);
				Util.ConvertHresultToException(hr);
				result = new string((char*)ptr2);
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.delete((void*)ptr);
				}
			}
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B6FC File Offset: 0x0000AAFC
		public void Add(KeyValuePair<CultureInfo, string> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B6D4 File Offset: 0x0000AAD4
		public void Add(CultureInfo key, string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001137C File Offset: 0x0001077C
		[return: MarshalAs(UnmanagedType.U1)]
		public bool ContainsKey(CultureInfo key)
		{
			uint num = 0U;
			return this.FindLocaleName(key.Name, out num);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000120A8 File Offset: 0x000114A8
		public ICollection<CultureInfo> Keys
		{
			get
			{
				return (ICollection<CultureInfo>)this.KeysArray;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B6E8 File Offset: 0x0000AAE8
		[return: MarshalAs(UnmanagedType.U1)]
		public bool Remove(KeyValuePair<CultureInfo, string> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B6E8 File Offset: 0x0000AAE8
		[return: MarshalAs(UnmanagedType.U1)]
		public bool Remove(CultureInfo key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001139C File Offset: 0x0001079C
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		public bool TryGetValue(CultureInfo key, out string value)
		{
			byte condition = (key != null) ? 1 : 0;
			Invariant.Assert(condition != 0);
			uint index = 0U;
			if (this.FindLocaleName(key.Name, out index))
			{
				value = this.GetString(index);
				return true;
			}
			return false;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000120C0 File Offset: 0x000114C0
		public ICollection<string> Values
		{
			get
			{
				return (ICollection<string>)this.ValuesArray;
			}
		}

		// Token: 0x17000010 RID: 16
		public string this[CultureInfo key]
		{
			get
			{
				string result = null;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B710 File Offset: 0x0000AB10
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B724 File Offset: 0x0000AB24
		[return: MarshalAs(UnmanagedType.U1)]
		public bool Contains(KeyValuePair<CultureInfo, string> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F3F4 File Offset: 0x0000E7F4
		public void CopyTo(KeyValuePair<CultureInfo, string>[] arrayObj, int arrayIndex)
		{
			int num = arrayIndex;
			foreach (KeyValuePair<CultureInfo, string> keyValuePair in this)
			{
				arrayObj[num] = keyValuePair;
				num++;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000FB14 File Offset: 0x0000EF14
		public int Count
		{
			get
			{
				uint stringsCount = this.StringsCount;
				if (stringsCount > 2147483647U)
				{
					throw new OverflowException("The number of elements is greater than System.Int32.MaxValue");
				}
				return (int)stringsCount;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B738 File Offset: 0x0000AB38
		public bool IsReadOnly
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return true;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B7A0 File Offset: 0x0000ABA0
		public IEnumerator<KeyValuePair<CultureInfo, string>> GetEnumerator()
		{
			return new LocalizedStrings.LocalizedStringsEnumerator(this);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B7B4 File Offset: 0x0000ABB4
		public IEnumerator GetEnumerator2()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000315 RID: 789
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings> _localizedStrings;

		// Token: 0x04000316 RID: 790
		private CultureInfo[] _keys;

		// Token: 0x04000317 RID: 791
		private string[] _values;

		// Token: 0x0200000E RID: 14
		public class LocalizedStringsEnumerator : IEnumerator<KeyValuePair<CultureInfo, string>>
		{
			// Token: 0x06000290 RID: 656 RVA: 0x0000B748 File Offset: 0x0000AB48
			public LocalizedStringsEnumerator(LocalizedStrings localizedStrings)
			{
				this._localizedStrings = localizedStrings;
				this._currentIndex = -1L;
			}

			// Token: 0x06000291 RID: 657 RVA: 0x000104F0 File Offset: 0x0000F8F0
			[return: MarshalAs(UnmanagedType.U1)]
			public virtual bool MoveNext()
			{
				long num = (long)((ulong)this._localizedStrings.StringsCount);
				long currentIndex = this._currentIndex;
				if (currentIndex >= num)
				{
					return false;
				}
				this._currentIndex = currentIndex + 1L;
				long num2 = (long)((ulong)this._localizedStrings.StringsCount);
				return ((this._currentIndex < num2) ? 1 : 0) != 0;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000292 RID: 658 RVA: 0x00011B98 File Offset: 0x00010F98
			public virtual KeyValuePair<CultureInfo, string> Current
			{
				[SecuritySafeCritical]
				get
				{
					long num = (long)((ulong)this._localizedStrings.StringsCount);
					long currentIndex = this._currentIndex;
					if (currentIndex >= num)
					{
						throw new InvalidOperationException(LocalizedErrorMsgs.EnumeratorReachedEnd);
					}
					if (currentIndex == -1L)
					{
						throw new InvalidOperationException(LocalizedErrorMsgs.EnumeratorNotStarted);
					}
					CultureInfo[] keysArray = this._localizedStrings.KeysArray;
					string[] valuesArray = this._localizedStrings.ValuesArray;
					int num2 = (int)this._currentIndex;
					KeyValuePair<CultureInfo, string> result = new KeyValuePair<CultureInfo, string>(keysArray[num2], valuesArray[num2]);
					return result;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00011C14 File Offset: 0x00011014
			public object Current2
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000B774 File Offset: 0x0000AB74
			public virtual void Reset()
			{
				this._currentIndex = -1L;
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000B790 File Offset: 0x0000AB90
			private void ~LocalizedStringsEnumerator()
			{
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000B7C8 File Offset: 0x0000ABC8
			protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
			{
				if (!A_0)
				{
					base.Finalize();
				}
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000F458 File Offset: 0x0000E858
			public sealed void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000318 RID: 792
			public LocalizedStrings _localizedStrings;

			// Token: 0x04000319 RID: 793
			public long _currentIndex;
		}
	}
}
