using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000026 RID: 38
	internal sealed class ItemProps
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000FCCC File Offset: 0x0000F0CC
		public unsafe void* NumberSubstitutionNoAddRef
		{
			[SecurityCritical]
			get
			{
				return (void*)this._numberSubstitution.Value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000FCAC File Offset: 0x0000F0AC
		public unsafe void* ScriptAnalysis
		{
			[SecurityCritical]
			get
			{
				NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> scriptAnalysis = this._scriptAnalysis;
				if (scriptAnalysis != null)
				{
					return (void*)scriptAnalysis.Value;
				}
				return null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000C238 File Offset: 0x0000B638
		public CultureInfo DigitCulture
		{
			get
			{
				return this._digitCulture;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C24C File Offset: 0x0000B64C
		public bool HasExtendedCharacter
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this._hasExtendedCharacter;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000C260 File Offset: 0x0000B660
		public bool NeedsCaretInfo
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this._needsCaretInfo;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C288 File Offset: 0x0000B688
		public bool IsIndic
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this._isIndic;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C29C File Offset: 0x0000B69C
		public bool IsLatin
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this._isLatin;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C274 File Offset: 0x0000B674
		public bool HasCombiningMark
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this._hasCombiningMark;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000FCE4 File Offset: 0x0000F0E4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool CanShapeTogether(ItemProps other)
		{
			int num;
			if (this._numberSubstitution.Value == other._numberSubstitution.Value)
			{
				NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> scriptAnalysis = this._scriptAnalysis;
				NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> scriptAnalysis2 = other._scriptAnalysis;
				if (scriptAnalysis != scriptAnalysis2)
				{
					if (scriptAnalysis == null || scriptAnalysis2 == null)
					{
						goto IL_6F;
					}
					DWRITE_SCRIPT_ANALYSIS* value = scriptAnalysis2.Value;
					if (*(ushort*)this._scriptAnalysis.Value != *(ushort*)value)
					{
						goto IL_6F;
					}
					DWRITE_SCRIPT_ANALYSIS* ptr = other._scriptAnalysis.Value + 4L / (long)sizeof(DWRITE_SCRIPT_ANALYSIS);
					if (*(int*)(this._scriptAnalysis.Value + 4L / (long)sizeof(DWRITE_SCRIPT_ANALYSIS)) != *(int*)ptr)
					{
						goto IL_6F;
					}
				}
				num = 1;
				goto IL_71;
			}
			IL_6F:
			num = 0;
			IL_71:
			GC.KeepAlive(other);
			GC.KeepAlive(this);
			return (byte)num != 0;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000C2B0 File Offset: 0x0000B6B0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public ItemProps()
		{
			this._digitCulture = null;
			this._hasCombiningMark = false;
			this._hasExtendedCharacter = false;
			this._needsCaretInfo = false;
			this._isIndic = false;
			this._isLatin = false;
			this._numberSubstitution = null;
			this._scriptAnalysis = null;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011070 File Offset: 0x00010470
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe static ItemProps Create(void* scriptAnalysis, void* numberSubstitution, CultureInfo digitCulture, [MarshalAs(UnmanagedType.U1)] bool hasCombiningMark, [MarshalAs(UnmanagedType.U1)] bool needsCaretInfo, [MarshalAs(UnmanagedType.U1)] bool hasExtendedCharacter, [MarshalAs(UnmanagedType.U1)] bool isIndic, [MarshalAs(UnmanagedType.U1)] bool isLatin)
		{
			ItemProps itemProps = new ItemProps();
			itemProps._digitCulture = digitCulture;
			itemProps._hasCombiningMark = hasCombiningMark;
			itemProps._hasExtendedCharacter = hasExtendedCharacter;
			itemProps._needsCaretInfo = needsCaretInfo;
			itemProps._isIndic = isIndic;
			itemProps._isLatin = isLatin;
			if (scriptAnalysis != null)
			{
				DWRITE_SCRIPT_ANALYSIS* ptr = (DWRITE_SCRIPT_ANALYSIS*)<Module>.@new(8UL);
				DWRITE_SCRIPT_ANALYSIS* pNativePointer;
				if (ptr != null)
				{
					initblk(ptr, 0, 8L);
					pNativePointer = ptr;
				}
				else
				{
					pNativePointer = null;
				}
				NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> nativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> = new NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>(pNativePointer);
				itemProps._scriptAnalysis = nativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>;
				$VCls$0000000080 $VCls$;
				cpblk(ref $VCls$, scriptAnalysis, 8);
				cpblk(nativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>.Value, ref $VCls$, 8);
			}
			if (numberSubstitution != null)
			{
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), numberSubstitution, *(*(long*)numberSubstitution + 8L));
			}
			itemProps._numberSubstitution = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution>((IUnknown*)numberSubstitution);
			return itemProps;
		}

		// Token: 0x0400033C RID: 828
		private CultureInfo _digitCulture;

		// Token: 0x0400033D RID: 829
		private bool _hasCombiningMark;

		// Token: 0x0400033E RID: 830
		private bool _needsCaretInfo;

		// Token: 0x0400033F RID: 831
		private bool _hasExtendedCharacter;

		// Token: 0x04000340 RID: 832
		private bool _isIndic;

		// Token: 0x04000341 RID: 833
		private bool _isLatin;

		// Token: 0x04000342 RID: 834
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution> _numberSubstitution;

		// Token: 0x04000343 RID: 835
		private NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> _scriptAnalysis;
	}
}
