using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.Interop;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200061D RID: 1565
	internal class DUCE
	{
		// Token: 0x060047F5 RID: 18421 RVA: 0x001199F0 File Offset: 0x00118DF0
		[SecurityCritical]
		internal unsafe static void CopyBytes(byte* pbTo, byte* pbFrom, int cbData)
		{
			for (int i = 0; i < cbData / 4; i++)
			{
				*(int*)(pbTo + (IntPtr)i * 4) = *(int*)(pbFrom + (IntPtr)i * 4);
			}
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x00119A20 File Offset: 0x00118E20
		[SecuritySafeCritical]
		internal unsafe static void NotifyPolicyChangeForNonInteractiveMode(bool forceRender, DUCE.Channel channel)
		{
			DUCE.MILCMD_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE milcmd_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE = new DUCE.MILCMD_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE
			{
				Type = MILCMD.MilCmdPartitionNotifyPolicyChangeForNonInteractiveMode,
				ShouldRenderEvenWhenNoDisplayDevicesAreAvailable = (forceRender ? 1U : 0U)
			};
			channel.SendCommand((byte*)(&milcmd_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE), sizeof(DUCE.MILCMD_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE), false);
		}

		// Token: 0x04001A3B RID: 6715
		internal const uint waitInfinite = 4294967295U;

		// Token: 0x020008E1 RID: 2273
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private static class UnsafeNativeMethods
		{
			// Token: 0x06005919 RID: 22809
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_CreateOrAddRefOnChannel(IntPtr pChannel, DUCE.ResourceType resourceType, ref DUCE.ResourceHandle hResource);

			// Token: 0x0600591A RID: 22810
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_DuplicateHandle(IntPtr pSourceChannel, DUCE.ResourceHandle original, IntPtr pTargetChannel, ref DUCE.ResourceHandle duplicate);

			// Token: 0x0600591B RID: 22811
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilConnection_CreateChannel(IntPtr pTransport, IntPtr hChannel, out IntPtr channelHandle);

			// Token: 0x0600591C RID: 22812
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilConnection_DestroyChannel(IntPtr channelHandle);

			// Token: 0x0600591D RID: 22813
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilChannel_CloseBatch")]
			internal static extern int MilConnection_CloseBatch(IntPtr channelHandle);

			// Token: 0x0600591E RID: 22814
			[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilChannel_CommitChannel")]
			internal static extern int MilConnection_CommitChannel(IntPtr channelHandle);

			// Token: 0x0600591F RID: 22815
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int WgxConnection_SameThreadPresent(IntPtr pConnection);

			// Token: 0x06005920 RID: 22816
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilChannel_GetMarshalType(IntPtr channelHandle, out ChannelMarshalType marshalType);

			// Token: 0x06005921 RID: 22817
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilResource_SendCommand(byte* pbData, uint cbSize, bool sendInSeparateBatch, IntPtr pChannel);

			// Token: 0x06005922 RID: 22818
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilChannel_BeginCommand(IntPtr pChannel, byte* pbData, uint cbSize, uint cbExtra);

			// Token: 0x06005923 RID: 22819
			[DllImport("wpfgfx_v0400.dll")]
			internal unsafe static extern int MilChannel_AppendCommandData(IntPtr pChannel, byte* pbData, uint cbSize);

			// Token: 0x06005924 RID: 22820
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilChannel_EndCommand(IntPtr pChannel);

			// Token: 0x06005925 RID: 22821
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_SendCommandMedia(DUCE.ResourceHandle handle, SafeMediaHandle pMedia, IntPtr pChannel, bool notifyUceDirect);

			// Token: 0x06005926 RID: 22822
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_SendCommandBitmapSource(DUCE.ResourceHandle handle, BitmapSourceSafeMILHandle pBitmapSource, IntPtr pChannel);

			// Token: 0x06005927 RID: 22823
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_ReleaseOnChannel(IntPtr pChannel, DUCE.ResourceHandle hResource, out int deleted);

			// Token: 0x06005928 RID: 22824
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilChannel_SetNotificationWindow(IntPtr pChannel, IntPtr hwnd, WindowMessage message);

			// Token: 0x06005929 RID: 22825
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilComposition_WaitForNextMessage(IntPtr pChannel, int nCount, IntPtr[] handles, int bWaitAll, uint waitTimeout, out int waitReturn);

			// Token: 0x0600592A RID: 22826
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilComposition_PeekNextMessage(IntPtr pChannel, out DUCE.MilMessage.Message message, IntPtr messageSize, out int messageRetrieved);

			// Token: 0x0600592B RID: 22827
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilResource_GetRefCountOnChannel(IntPtr pChannel, DUCE.ResourceHandle hResource, out uint refCount);
		}

		// Token: 0x020008E2 RID: 2274
		internal static class MilMessage
		{
			// Token: 0x02000A26 RID: 2598
			internal enum Type
			{
				// Token: 0x04002F9F RID: 12191
				Invalid,
				// Token: 0x04002FA0 RID: 12192
				SyncFlushReply,
				// Token: 0x04002FA1 RID: 12193
				Caps = 4,
				// Token: 0x04002FA2 RID: 12194
				PartitionIsZombie = 6,
				// Token: 0x04002FA3 RID: 12195
				SyncModeStatus = 9,
				// Token: 0x04002FA4 RID: 12196
				Presented,
				// Token: 0x04002FA5 RID: 12197
				BadPixelShader = 16,
				// Token: 0x04002FA6 RID: 12198
				ForceDWORD = -1
			}

			// Token: 0x02000A27 RID: 2599
			[StructLayout(LayoutKind.Explicit, Pack = 1)]
			internal struct CapsData
			{
				// Token: 0x04002FA7 RID: 12199
				[FieldOffset(0)]
				internal int CommonMinimumCaps;

				// Token: 0x04002FA8 RID: 12200
				[FieldOffset(4)]
				internal uint DisplayUniqueness;

				// Token: 0x04002FA9 RID: 12201
				[FieldOffset(8)]
				internal MilGraphicsAccelerationCaps Caps;
			}

			// Token: 0x02000A28 RID: 2600
			[StructLayout(LayoutKind.Explicit, Pack = 1)]
			internal struct PartitionIsZombieStatus
			{
				// Token: 0x04002FAA RID: 12202
				[FieldOffset(0)]
				internal int HRESULTFailureCode;
			}

			// Token: 0x02000A29 RID: 2601
			[StructLayout(LayoutKind.Explicit, Pack = 1)]
			internal struct SyncModeStatus
			{
				// Token: 0x04002FAB RID: 12203
				[FieldOffset(0)]
				internal int Enabled;
			}

			// Token: 0x02000A2A RID: 2602
			[StructLayout(LayoutKind.Explicit, Pack = 1)]
			internal struct Presented
			{
				// Token: 0x04002FAC RID: 12204
				[FieldOffset(0)]
				internal MIL_PRESENTATION_RESULTS PresentationResults;

				// Token: 0x04002FAD RID: 12205
				[FieldOffset(4)]
				internal int RefreshRate;

				// Token: 0x04002FAE RID: 12206
				[FieldOffset(8)]
				internal long PresentationTime;
			}

			// Token: 0x02000A2B RID: 2603
			[StructLayout(LayoutKind.Explicit, Pack = 1)]
			internal struct Message
			{
				// Token: 0x04002FAF RID: 12207
				[FieldOffset(0)]
				internal DUCE.MilMessage.Type Type;

				// Token: 0x04002FB0 RID: 12208
				[FieldOffset(4)]
				internal int Reserved;

				// Token: 0x04002FB1 RID: 12209
				[FieldOffset(8)]
				internal DUCE.MilMessage.CapsData Caps;

				// Token: 0x04002FB2 RID: 12210
				[FieldOffset(8)]
				internal DUCE.MilMessage.PartitionIsZombieStatus HRESULTFailure;

				// Token: 0x04002FB3 RID: 12211
				[FieldOffset(8)]
				internal DUCE.MilMessage.Presented Presented;

				// Token: 0x04002FB4 RID: 12212
				[FieldOffset(8)]
				internal DUCE.MilMessage.SyncModeStatus SyncModeStatus;
			}
		}

		// Token: 0x020008E3 RID: 2275
		internal struct ChannelSet
		{
			// Token: 0x040029A5 RID: 10661
			internal DUCE.Channel Channel;

			// Token: 0x040029A6 RID: 10662
			internal DUCE.Channel OutOfBandChannel;
		}

		// Token: 0x020008E4 RID: 2276
		internal sealed class Channel
		{
			// Token: 0x0600592C RID: 22828 RVA: 0x00169894 File Offset: 0x00168C94
			[SecurityCritical]
			public Channel(DUCE.Channel referenceChannel, bool isOutOfBandChannel, IntPtr pConnection, bool isSynchronous)
			{
				IntPtr hChannel = IntPtr.Zero;
				this._referenceChannel = referenceChannel;
				this._pConnection = pConnection;
				this._isOutOfBandChannel = isOutOfBandChannel;
				this._isSynchronous = isSynchronous;
				if (referenceChannel != null)
				{
					hChannel = referenceChannel._hChannel;
				}
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_CreateChannel(this._pConnection, hChannel, out this._hChannel));
			}

			// Token: 0x0600592D RID: 22829 RVA: 0x001698EC File Offset: 0x00168CEC
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void Commit()
			{
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_CommitChannel(this._hChannel));
			}

			// Token: 0x0600592E RID: 22830 RVA: 0x0016991C File Offset: 0x00168D1C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void CloseBatch()
			{
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_CloseBatch(this._hChannel));
			}

			// Token: 0x0600592F RID: 22831 RVA: 0x0016994C File Offset: 0x00168D4C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal void SyncFlush()
			{
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				HRESULT.Check(MilCoreApi.MilComposition_SyncFlush(this._hChannel));
			}

			// Token: 0x06005930 RID: 22832 RVA: 0x0016997C File Offset: 0x00168D7C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void Close()
			{
				if (this._hChannel != IntPtr.Zero)
				{
					HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_CloseBatch(this._hChannel));
					HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_CommitChannel(this._hChannel));
				}
				this._referenceChannel = null;
				if (this._hChannel != IntPtr.Zero)
				{
					HRESULT.Check(DUCE.UnsafeNativeMethods.MilConnection_DestroyChannel(this._hChannel));
					this._hChannel = IntPtr.Zero;
				}
			}

			// Token: 0x06005931 RID: 22833 RVA: 0x001699F0 File Offset: 0x00168DF0
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void Present()
			{
				HRESULT.Check(DUCE.UnsafeNativeMethods.WgxConnection_SameThreadPresent(this._pConnection));
			}

			// Token: 0x06005932 RID: 22834 RVA: 0x00169A10 File Offset: 0x00168E10
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal bool CreateOrAddRefOnChannel(object instance, ref DUCE.ResourceHandle handle, DUCE.ResourceType resourceType)
			{
				bool isNull = handle.IsNull;
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_CreateOrAddRefOnChannel(this._hChannel, resourceType, ref handle));
				if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.PERF_LOW))
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.CreateOrAddResourceOnChannel, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.PERF_LOW, new object[]
					{
						PerfService.GetPerfElementID(instance),
						this._hChannel,
						(uint)handle,
						(uint)resourceType
					});
				}
				return isNull;
			}

			// Token: 0x06005933 RID: 22835 RVA: 0x00169AB0 File Offset: 0x00168EB0
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal DUCE.ResourceHandle DuplicateHandle(DUCE.ResourceHandle original, DUCE.Channel targetChannel)
			{
				DUCE.ResourceHandle @null = DUCE.ResourceHandle.Null;
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_DuplicateHandle(this._hChannel, original, targetChannel._hChannel, ref @null));
				return @null;
			}

			// Token: 0x06005934 RID: 22836 RVA: 0x00169AE0 File Offset: 0x00168EE0
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal bool ReleaseOnChannel(DUCE.ResourceHandle handle)
			{
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				int num;
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_ReleaseOnChannel(this._hChannel, handle, out num));
				if (num != 0 && EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.PERF_LOW))
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.ReleaseOnChannel, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.PERF_LOW, new object[]
					{
						this._hChannel,
						(uint)handle
					});
				}
				return num != 0;
			}

			// Token: 0x06005935 RID: 22837 RVA: 0x00169B64 File Offset: 0x00168F64
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal uint GetRefCount(DUCE.ResourceHandle handle)
			{
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				uint result;
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_GetRefCountOnChannel(this._hChannel, handle, out result));
				return result;
			}

			// Token: 0x17001261 RID: 4705
			// (get) Token: 0x06005936 RID: 22838 RVA: 0x00169B9C File Offset: 0x00168F9C
			internal bool IsConnected
			{
				[SecurityCritical]
				[SecurityTreatAsSafe]
				get
				{
					return MediaContext.CurrentMediaContext.IsConnected;
				}
			}

			// Token: 0x17001262 RID: 4706
			// (get) Token: 0x06005937 RID: 22839 RVA: 0x00169BB4 File Offset: 0x00168FB4
			internal ChannelMarshalType MarshalType
			{
				[SecurityCritical]
				[SecurityTreatAsSafe]
				get
				{
					Invariant.Assert(this._hChannel != IntPtr.Zero);
					ChannelMarshalType result;
					HRESULT.Check(DUCE.UnsafeNativeMethods.MilChannel_GetMarshalType(this._hChannel, out result));
					return result;
				}
			}

			// Token: 0x17001263 RID: 4707
			// (get) Token: 0x06005938 RID: 22840 RVA: 0x00169BEC File Offset: 0x00168FEC
			internal bool IsSynchronous
			{
				get
				{
					return this._isSynchronous;
				}
			}

			// Token: 0x17001264 RID: 4708
			// (get) Token: 0x06005939 RID: 22841 RVA: 0x00169C00 File Offset: 0x00169000
			internal bool IsOutOfBandChannel
			{
				get
				{
					return this._isOutOfBandChannel;
				}
			}

			// Token: 0x0600593A RID: 22842 RVA: 0x00169C14 File Offset: 0x00169014
			[SecurityCritical]
			internal unsafe void SendCommand(byte* pCommandData, int cSize)
			{
				this.SendCommand(pCommandData, cSize, false);
			}

			// Token: 0x0600593B RID: 22843 RVA: 0x00169C2C File Offset: 0x0016902C
			[SecurityCritical]
			internal unsafe void SendCommand(byte* pCommandData, int cSize, bool sendInSeparateBatch)
			{
				Invariant.Assert(pCommandData != null && cSize > 0);
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				int hr = DUCE.UnsafeNativeMethods.MilResource_SendCommand(pCommandData, checked((uint)cSize), sendInSeparateBatch, this._hChannel);
				HRESULT.Check(hr);
			}

			// Token: 0x0600593C RID: 22844 RVA: 0x00169C78 File Offset: 0x00169078
			[SecurityCritical]
			internal unsafe void BeginCommand(byte* pbCommandData, int cbSize, int cbExtra)
			{
				Invariant.Assert(cbSize > 0);
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				int hr = checked(DUCE.UnsafeNativeMethods.MilChannel_BeginCommand(this._hChannel, pbCommandData, (uint)cbSize, (uint)cbExtra));
				HRESULT.Check(hr);
			}

			// Token: 0x0600593D RID: 22845 RVA: 0x00169CBC File Offset: 0x001690BC
			[SecurityCritical]
			internal unsafe void AppendCommandData(byte* pbCommandData, int cbSize)
			{
				Invariant.Assert(pbCommandData != null && cbSize > 0);
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				int hr = DUCE.UnsafeNativeMethods.MilChannel_AppendCommandData(this._hChannel, pbCommandData, checked((uint)cbSize));
				HRESULT.Check(hr);
			}

			// Token: 0x0600593E RID: 22846 RVA: 0x00169D04 File Offset: 0x00169104
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void EndCommand()
			{
				if (this._hChannel == IntPtr.Zero)
				{
					return;
				}
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilChannel_EndCommand(this._hChannel));
			}

			// Token: 0x0600593F RID: 22847 RVA: 0x00169D34 File Offset: 0x00169134
			[SecurityCritical]
			internal void SendCommandBitmapSource(DUCE.ResourceHandle imageHandle, BitmapSourceSafeMILHandle pBitmapSource)
			{
				Invariant.Assert(pBitmapSource != null && !pBitmapSource.IsInvalid);
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_SendCommandBitmapSource(imageHandle, pBitmapSource, this._hChannel));
			}

			// Token: 0x06005940 RID: 22848 RVA: 0x00169D7C File Offset: 0x0016917C
			[SecurityCritical]
			internal void SendCommandMedia(DUCE.ResourceHandle mediaHandle, SafeMediaHandle pMedia, bool notifyUceDirect)
			{
				Invariant.Assert(pMedia != null && !pMedia.IsInvalid);
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilResource_SendCommandMedia(mediaHandle, pMedia, this._hChannel, notifyUceDirect));
			}

			// Token: 0x06005941 RID: 22849 RVA: 0x00169DC8 File Offset: 0x001691C8
			[SecurityCritical]
			internal void SetNotificationWindow(IntPtr hwnd, WindowMessage message)
			{
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilChannel_SetNotificationWindow(this._hChannel, hwnd, message));
			}

			// Token: 0x06005942 RID: 22850 RVA: 0x00169DFC File Offset: 0x001691FC
			[SecurityCritical]
			internal void WaitForNextMessage()
			{
				int num;
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilComposition_WaitForNextMessage(this._hChannel, 0, null, 1, uint.MaxValue, out num));
			}

			// Token: 0x06005943 RID: 22851 RVA: 0x00169E20 File Offset: 0x00169220
			[SecurityCritical]
			internal bool PeekNextMessage(out DUCE.MilMessage.Message message)
			{
				Invariant.Assert(this._hChannel != IntPtr.Zero);
				int num;
				HRESULT.Check(DUCE.UnsafeNativeMethods.MilComposition_PeekNextMessage(this._hChannel, out message, (IntPtr)sizeof(DUCE.MilMessage.Message), out num));
				return num != 0;
			}

			// Token: 0x040029A7 RID: 10663
			[SecurityCritical]
			private IntPtr _hChannel;

			// Token: 0x040029A8 RID: 10664
			private DUCE.Channel _referenceChannel;

			// Token: 0x040029A9 RID: 10665
			private bool _isSynchronous;

			// Token: 0x040029AA RID: 10666
			private bool _isOutOfBandChannel;

			// Token: 0x040029AB RID: 10667
			private IntPtr _pConnection;
		}

		// Token: 0x020008E5 RID: 2277
		internal struct Resource
		{
			// Token: 0x06005944 RID: 22852 RVA: 0x00169E64 File Offset: 0x00169264
			public Resource(DUCE.ResourceHandle h)
			{
				this._handle = h;
			}

			// Token: 0x06005945 RID: 22853 RVA: 0x00169E78 File Offset: 0x00169278
			public bool CreateOrAddRefOnChannel(object instance, DUCE.Channel channel, DUCE.ResourceType type)
			{
				return channel.CreateOrAddRefOnChannel(instance, ref this._handle, type);
			}

			// Token: 0x06005946 RID: 22854 RVA: 0x00169E94 File Offset: 0x00169294
			public bool ReleaseOnChannel(DUCE.Channel channel)
			{
				if (channel.ReleaseOnChannel(this._handle))
				{
					this._handle = DUCE.ResourceHandle.Null;
					return true;
				}
				return false;
			}

			// Token: 0x06005947 RID: 22855 RVA: 0x00169EC0 File Offset: 0x001692C0
			public bool IsOnChannel(DUCE.Channel channel)
			{
				return !this._handle.IsNull;
			}

			// Token: 0x17001265 RID: 4709
			// (get) Token: 0x06005948 RID: 22856 RVA: 0x00169EDC File Offset: 0x001692DC
			public DUCE.ResourceHandle Handle
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x040029AC RID: 10668
			public static readonly DUCE.Resource Null = new DUCE.Resource(DUCE.ResourceHandle.Null);

			// Token: 0x040029AD RID: 10669
			private DUCE.ResourceHandle _handle;
		}

		// Token: 0x020008E6 RID: 2278
		[StructLayout(LayoutKind.Explicit)]
		internal struct ResourceHandle
		{
			// Token: 0x0600594A RID: 22858 RVA: 0x00169F0C File Offset: 0x0016930C
			public static explicit operator uint(DUCE.ResourceHandle r)
			{
				return r._handle;
			}

			// Token: 0x0600594B RID: 22859 RVA: 0x00169F20 File Offset: 0x00169320
			public ResourceHandle(uint handle)
			{
				this._handle = handle;
			}

			// Token: 0x17001266 RID: 4710
			// (get) Token: 0x0600594C RID: 22860 RVA: 0x00169F34 File Offset: 0x00169334
			public bool IsNull
			{
				get
				{
					return this._handle == 0U;
				}
			}

			// Token: 0x040029AE RID: 10670
			public static readonly DUCE.ResourceHandle Null = new DUCE.ResourceHandle(0U);

			// Token: 0x040029AF RID: 10671
			[FieldOffset(0)]
			private uint _handle;
		}

		// Token: 0x020008E7 RID: 2279
		internal struct Map<ValueType>
		{
			// Token: 0x0600594E RID: 22862 RVA: 0x00169F64 File Offset: 0x00169364
			public bool IsEmpty()
			{
				return this._first._key == null && this._others == null;
			}

			// Token: 0x0600594F RID: 22863 RVA: 0x00169F8C File Offset: 0x0016938C
			private int Find(object key)
			{
				int result = -2;
				if (this._first._key != null)
				{
					if (this._first._key == key)
					{
						result = -1;
					}
					else if (this._others != null)
					{
						for (int i = 0; i < this._others.Count; i++)
						{
							if (this._others[i]._key == key)
							{
								result = i;
								break;
							}
						}
					}
				}
				return result;
			}

			// Token: 0x06005950 RID: 22864 RVA: 0x00169FF4 File Offset: 0x001693F4
			public void Set(object key, ValueType value)
			{
				int num = this.Find(key);
				if (num == -1)
				{
					this._first._value = value;
					return;
				}
				if (num != -2)
				{
					this._others[num] = new DUCE.Map<ValueType>.Entry(key, value);
					return;
				}
				if (this._first._key == null)
				{
					this._first = new DUCE.Map<ValueType>.Entry(key, value);
					return;
				}
				if (this._others == null)
				{
					this._others = new List<DUCE.Map<ValueType>.Entry>(2);
				}
				this._others.Add(new DUCE.Map<ValueType>.Entry(key, value));
			}

			// Token: 0x06005951 RID: 22865 RVA: 0x0016A074 File Offset: 0x00169474
			public bool Remove(object key)
			{
				int num = this.Find(key);
				if (num == -1)
				{
					if (this._others != null)
					{
						int num2 = this._others.Count - 1;
						this._first = this._others[num2];
						if (num2 == 0)
						{
							this._others = null;
						}
						else
						{
							this._others.RemoveAt(num2);
						}
					}
					else
					{
						this._first = default(DUCE.Map<ValueType>.Entry);
					}
					return true;
				}
				if (num >= 0)
				{
					if (this._others.Count == 1)
					{
						this._others = null;
					}
					else
					{
						this._others.RemoveAt(num);
					}
					return true;
				}
				return false;
			}

			// Token: 0x06005952 RID: 22866 RVA: 0x0016A108 File Offset: 0x00169508
			public bool Get(object key, out ValueType value)
			{
				int num = this.Find(key);
				value = default(ValueType);
				if (num == -1)
				{
					value = this._first._value;
					return true;
				}
				if (num >= 0)
				{
					value = this._others[num]._value;
					return true;
				}
				return false;
			}

			// Token: 0x06005953 RID: 22867 RVA: 0x0016A15C File Offset: 0x0016955C
			public int Count()
			{
				if (this._first._key == null)
				{
					return 0;
				}
				if (this._others == null)
				{
					return 1;
				}
				return this._others.Count + 1;
			}

			// Token: 0x06005954 RID: 22868 RVA: 0x0016A190 File Offset: 0x00169590
			public object Get(int index)
			{
				if (index >= this.Count())
				{
					return null;
				}
				if (index == 0)
				{
					return this._first._key;
				}
				return this._others[index - 1]._key;
			}

			// Token: 0x040029B0 RID: 10672
			private const int FOUND_IN_INLINE_STORAGE = -1;

			// Token: 0x040029B1 RID: 10673
			private const int NOT_FOUND = -2;

			// Token: 0x040029B2 RID: 10674
			private DUCE.Map<ValueType>.Entry _first;

			// Token: 0x040029B3 RID: 10675
			private List<DUCE.Map<ValueType>.Entry> _others;

			// Token: 0x02000A2C RID: 2604
			private struct Entry
			{
				// Token: 0x06005C35 RID: 23605 RVA: 0x00172518 File Offset: 0x00171918
				public Entry(object k, ValueType v)
				{
					this._key = k;
					this._value = v;
				}

				// Token: 0x04002FB5 RID: 12213
				public object _key;

				// Token: 0x04002FB6 RID: 12214
				public ValueType _value;
			}
		}

		// Token: 0x020008E8 RID: 2280
		internal struct Map
		{
			// Token: 0x06005955 RID: 22869 RVA: 0x0016A1CC File Offset: 0x001695CC
			public bool IsEmpty()
			{
				return this._head._key == null;
			}

			// Token: 0x06005956 RID: 22870 RVA: 0x0016A1E8 File Offset: 0x001695E8
			private int Find(object key)
			{
				int result = -2;
				if (this._head._key != null)
				{
					if (this._head._key == key)
					{
						result = -1;
					}
					else if (this._head._value.IsNull)
					{
						List<DUCE.Map.Entry> list = (List<DUCE.Map.Entry>)this._head._key;
						for (int i = 0; i < list.Count; i++)
						{
							if (list[i]._key == key)
							{
								result = i;
								break;
							}
						}
					}
				}
				return result;
			}

			// Token: 0x06005957 RID: 22871 RVA: 0x0016A260 File Offset: 0x00169660
			public void Set(object key, DUCE.ResourceHandle value)
			{
				int num = this.Find(key);
				if (num == -1)
				{
					this._head._value = value;
					return;
				}
				if (num != -2)
				{
					((List<DUCE.Map.Entry>)this._head._key)[num] = new DUCE.Map.Entry(key, value);
					return;
				}
				if (this._head._key == null)
				{
					this._head = new DUCE.Map.Entry(key, value);
					return;
				}
				if (!this._head._value.IsNull)
				{
					this._head._key = new List<DUCE.Map.Entry>(2)
					{
						this._head,
						new DUCE.Map.Entry(key, value)
					};
					this._head._value = DUCE.ResourceHandle.Null;
					return;
				}
				((List<DUCE.Map.Entry>)this._head._key).Add(new DUCE.Map.Entry(key, value));
			}

			// Token: 0x06005958 RID: 22872 RVA: 0x0016A334 File Offset: 0x00169734
			public bool Remove(object key)
			{
				int num = this.Find(key);
				if (num == -1)
				{
					this._head = default(DUCE.Map.Entry);
					return true;
				}
				if (num >= 0)
				{
					List<DUCE.Map.Entry> list = (List<DUCE.Map.Entry>)this._head._key;
					if (this.Count() == 2)
					{
						this._head = list[1 - num];
					}
					else
					{
						list.RemoveAt(num);
					}
					return true;
				}
				return false;
			}

			// Token: 0x06005959 RID: 22873 RVA: 0x0016A394 File Offset: 0x00169794
			public bool Get(object key, out DUCE.ResourceHandle value)
			{
				int num = this.Find(key);
				value = DUCE.ResourceHandle.Null;
				if (num == -1)
				{
					value = this._head._value;
					return true;
				}
				if (num >= 0)
				{
					value = ((List<DUCE.Map.Entry>)this._head._key)[num]._value;
					return true;
				}
				return false;
			}

			// Token: 0x0600595A RID: 22874 RVA: 0x0016A3F4 File Offset: 0x001697F4
			public int Count()
			{
				if (this._head._key == null)
				{
					return 0;
				}
				if (!this._head._value.IsNull)
				{
					return 1;
				}
				return ((List<DUCE.Map.Entry>)this._head._key).Count;
			}

			// Token: 0x0600595B RID: 22875 RVA: 0x0016A43C File Offset: 0x0016983C
			public object Get(int index)
			{
				if (index >= this.Count() || index < 0)
				{
					return null;
				}
				if (this.Count() == 1)
				{
					return this._head._key;
				}
				return ((List<DUCE.Map.Entry>)this._head._key)[index]._key;
			}

			// Token: 0x040029B4 RID: 10676
			private const int FOUND_IN_INLINE_STORAGE = -1;

			// Token: 0x040029B5 RID: 10677
			private const int NOT_FOUND = -2;

			// Token: 0x040029B6 RID: 10678
			private DUCE.Map.Entry _head;

			// Token: 0x02000A2D RID: 2605
			private struct Entry
			{
				// Token: 0x06005C36 RID: 23606 RVA: 0x00172534 File Offset: 0x00171934
				public Entry(object k, DUCE.ResourceHandle v)
				{
					this._key = k;
					this._value = v;
				}

				// Token: 0x04002FB7 RID: 12215
				public object _key;

				// Token: 0x04002FB8 RID: 12216
				public DUCE.ResourceHandle _value;
			}
		}

		// Token: 0x020008E9 RID: 2281
		internal class ShareableDUCEMultiChannelResource
		{
			// Token: 0x040029B7 RID: 10679
			public DUCE.MultiChannelResource _duceResource;
		}

		// Token: 0x020008EA RID: 2282
		internal struct MultiChannelResource
		{
			// Token: 0x0600595D RID: 22877 RVA: 0x0016A49C File Offset: 0x0016989C
			public bool CreateOrAddRefOnChannel(object instance, DUCE.Channel channel, DUCE.ResourceType type)
			{
				DUCE.ResourceHandle value;
				bool flag = this._map.Get(channel, out value);
				bool result = channel.CreateOrAddRefOnChannel(instance, ref value, type);
				if (!flag)
				{
					this._map.Set(channel, value);
				}
				return result;
			}

			// Token: 0x0600595E RID: 22878 RVA: 0x0016A4D4 File Offset: 0x001698D4
			public DUCE.ResourceHandle DuplicateHandle(DUCE.Channel sourceChannel, DUCE.Channel targetChannel)
			{
				DUCE.ResourceHandle resourceHandle = DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle @null = DUCE.ResourceHandle.Null;
				bool flag = this._map.Get(sourceChannel, out @null);
				resourceHandle = sourceChannel.DuplicateHandle(@null, targetChannel);
				if (!resourceHandle.IsNull)
				{
					this._map.Set(targetChannel, resourceHandle);
				}
				return resourceHandle;
			}

			// Token: 0x0600595F RID: 22879 RVA: 0x0016A51C File Offset: 0x0016991C
			public bool ReleaseOnChannel(DUCE.Channel channel)
			{
				DUCE.ResourceHandle handle;
				bool flag = this._map.Get(channel, out handle);
				if (channel.ReleaseOnChannel(handle))
				{
					this._map.Remove(channel);
					return true;
				}
				return false;
			}

			// Token: 0x06005960 RID: 22880 RVA: 0x0016A554 File Offset: 0x00169954
			public DUCE.ResourceHandle GetHandle(DUCE.Channel channel)
			{
				DUCE.ResourceHandle @null;
				if (channel != null)
				{
					this._map.Get(channel, out @null);
				}
				else
				{
					@null = DUCE.ResourceHandle.Null;
				}
				return @null;
			}

			// Token: 0x06005961 RID: 22881 RVA: 0x0016A57C File Offset: 0x0016997C
			public bool IsOnChannel(DUCE.Channel channel)
			{
				return !this.GetHandle(channel).IsNull;
			}

			// Token: 0x17001267 RID: 4711
			// (get) Token: 0x06005962 RID: 22882 RVA: 0x0016A59C File Offset: 0x0016999C
			public bool IsOnAnyChannel
			{
				get
				{
					return !this._map.IsEmpty();
				}
			}

			// Token: 0x06005963 RID: 22883 RVA: 0x0016A5B8 File Offset: 0x001699B8
			public int GetChannelCount()
			{
				return this._map.Count();
			}

			// Token: 0x06005964 RID: 22884 RVA: 0x0016A5D0 File Offset: 0x001699D0
			public DUCE.Channel GetChannel(int index)
			{
				return this._map.Get(index) as DUCE.Channel;
			}

			// Token: 0x06005965 RID: 22885 RVA: 0x0016A5F0 File Offset: 0x001699F0
			public uint GetRefCountOnChannel(DUCE.Channel channel)
			{
				DUCE.ResourceHandle handle;
				bool flag = this._map.Get(channel, out handle);
				return channel.GetRefCount(handle);
			}

			// Token: 0x040029B8 RID: 10680
			private DUCE.Map _map;
		}

		// Token: 0x020008EB RID: 2283
		internal static class CompositionNode
		{
			// Token: 0x06005966 RID: 22886 RVA: 0x0016A614 File Offset: 0x00169A14
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetTransform(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hTransform, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETTRANSFORM milcmd_VISUAL_SETTRANSFORM;
				milcmd_VISUAL_SETTRANSFORM.Type = MILCMD.MilCmdVisualSetTransform;
				milcmd_VISUAL_SETTRANSFORM.Handle = hCompositionNode;
				milcmd_VISUAL_SETTRANSFORM.hTransform = hTransform;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETTRANSFORM), sizeof(DUCE.MILCMD_VISUAL_SETTRANSFORM));
			}

			// Token: 0x06005967 RID: 22887 RVA: 0x0016A64C File Offset: 0x00169A4C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetEffect(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hEffect, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETEFFECT milcmd_VISUAL_SETEFFECT;
				milcmd_VISUAL_SETEFFECT.Type = MILCMD.MilCmdVisualSetEffect;
				milcmd_VISUAL_SETEFFECT.Handle = hCompositionNode;
				milcmd_VISUAL_SETEFFECT.hEffect = hEffect;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETEFFECT), sizeof(DUCE.MILCMD_VISUAL_SETEFFECT));
			}

			// Token: 0x06005968 RID: 22888 RVA: 0x0016A684 File Offset: 0x00169A84
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetCacheMode(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hCacheMode, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETCACHEMODE milcmd_VISUAL_SETCACHEMODE;
				milcmd_VISUAL_SETCACHEMODE.Type = MILCMD.MilCmdVisualSetCacheMode;
				milcmd_VISUAL_SETCACHEMODE.Handle = hCompositionNode;
				milcmd_VISUAL_SETCACHEMODE.hCacheMode = hCacheMode;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETCACHEMODE), sizeof(DUCE.MILCMD_VISUAL_SETCACHEMODE));
			}

			// Token: 0x06005969 RID: 22889 RVA: 0x0016A6BC File Offset: 0x00169ABC
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetOffset(DUCE.ResourceHandle hCompositionNode, double offsetX, double offsetY, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETOFFSET milcmd_VISUAL_SETOFFSET;
				milcmd_VISUAL_SETOFFSET.Type = MILCMD.MilCmdVisualSetOffset;
				milcmd_VISUAL_SETOFFSET.Handle = hCompositionNode;
				milcmd_VISUAL_SETOFFSET.offsetX = offsetX;
				milcmd_VISUAL_SETOFFSET.offsetY = offsetY;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETOFFSET), sizeof(DUCE.MILCMD_VISUAL_SETOFFSET));
			}

			// Token: 0x0600596A RID: 22890 RVA: 0x0016A6FC File Offset: 0x00169AFC
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetContent(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hContent, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETCONTENT milcmd_VISUAL_SETCONTENT;
				milcmd_VISUAL_SETCONTENT.Type = MILCMD.MilCmdVisualSetContent;
				milcmd_VISUAL_SETCONTENT.Handle = hCompositionNode;
				milcmd_VISUAL_SETCONTENT.hContent = hContent;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETCONTENT), sizeof(DUCE.MILCMD_VISUAL_SETCONTENT));
			}

			// Token: 0x0600596B RID: 22891 RVA: 0x0016A734 File Offset: 0x00169B34
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetAlpha(DUCE.ResourceHandle hCompositionNode, double alpha, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETALPHA milcmd_VISUAL_SETALPHA;
				milcmd_VISUAL_SETALPHA.Type = MILCMD.MilCmdVisualSetAlpha;
				milcmd_VISUAL_SETALPHA.Handle = hCompositionNode;
				milcmd_VISUAL_SETALPHA.alpha = alpha;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETALPHA), sizeof(DUCE.MILCMD_VISUAL_SETALPHA));
			}

			// Token: 0x0600596C RID: 22892 RVA: 0x0016A76C File Offset: 0x00169B6C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetAlphaMask(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hAlphaMaskBrush, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETALPHAMASK milcmd_VISUAL_SETALPHAMASK;
				milcmd_VISUAL_SETALPHAMASK.Type = MILCMD.MilCmdVisualSetAlphaMask;
				milcmd_VISUAL_SETALPHAMASK.Handle = hCompositionNode;
				milcmd_VISUAL_SETALPHAMASK.hAlphaMask = hAlphaMaskBrush;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETALPHAMASK), sizeof(DUCE.MILCMD_VISUAL_SETALPHAMASK));
			}

			// Token: 0x0600596D RID: 22893 RVA: 0x0016A7A4 File Offset: 0x00169BA4
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetScrollableAreaClip(DUCE.ResourceHandle hCompositionNode, Rect? clip, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETSCROLLABLEAREACLIP milcmd_VISUAL_SETSCROLLABLEAREACLIP;
				milcmd_VISUAL_SETSCROLLABLEAREACLIP.Type = MILCMD.MilCmdVisualSetScrollableAreaClip;
				milcmd_VISUAL_SETSCROLLABLEAREACLIP.Handle = hCompositionNode;
				milcmd_VISUAL_SETSCROLLABLEAREACLIP.IsEnabled = ((clip != null) ? 1U : 0U);
				if (clip != null)
				{
					milcmd_VISUAL_SETSCROLLABLEAREACLIP.Clip = clip.Value;
				}
				else
				{
					milcmd_VISUAL_SETSCROLLABLEAREACLIP.Clip = Rect.Empty;
				}
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETSCROLLABLEAREACLIP), sizeof(DUCE.MILCMD_VISUAL_SETSCROLLABLEAREACLIP));
			}

			// Token: 0x0600596E RID: 22894 RVA: 0x0016A80C File Offset: 0x00169C0C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetClip(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hClip, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETCLIP milcmd_VISUAL_SETCLIP;
				milcmd_VISUAL_SETCLIP.Type = MILCMD.MilCmdVisualSetClip;
				milcmd_VISUAL_SETCLIP.Handle = hCompositionNode;
				milcmd_VISUAL_SETCLIP.hClip = hClip;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETCLIP), sizeof(DUCE.MILCMD_VISUAL_SETCLIP));
			}

			// Token: 0x0600596F RID: 22895 RVA: 0x0016A844 File Offset: 0x00169C44
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetRenderOptions(DUCE.ResourceHandle hCompositionNode, MilRenderOptions renderOptions, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_SETRENDEROPTIONS milcmd_VISUAL_SETRENDEROPTIONS;
				milcmd_VISUAL_SETRENDEROPTIONS.Type = MILCMD.MilCmdVisualSetRenderOptions;
				milcmd_VISUAL_SETRENDEROPTIONS.Handle = hCompositionNode;
				milcmd_VISUAL_SETRENDEROPTIONS.renderOptions = renderOptions;
				channel.SendCommand((byte*)(&milcmd_VISUAL_SETRENDEROPTIONS), sizeof(DUCE.MILCMD_VISUAL_SETRENDEROPTIONS));
			}

			// Token: 0x06005970 RID: 22896 RVA: 0x0016A87C File Offset: 0x00169C7C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void RemoveChild(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hChild, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_REMOVECHILD milcmd_VISUAL_REMOVECHILD;
				milcmd_VISUAL_REMOVECHILD.Type = MILCMD.MilCmdVisualRemoveChild;
				milcmd_VISUAL_REMOVECHILD.Handle = hCompositionNode;
				milcmd_VISUAL_REMOVECHILD.hChild = hChild;
				channel.SendCommand((byte*)(&milcmd_VISUAL_REMOVECHILD), sizeof(DUCE.MILCMD_VISUAL_REMOVECHILD));
			}

			// Token: 0x06005971 RID: 22897 RVA: 0x0016A8B4 File Offset: 0x00169CB4
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void RemoveAllChildren(DUCE.ResourceHandle hCompositionNode, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_REMOVEALLCHILDREN milcmd_VISUAL_REMOVEALLCHILDREN;
				milcmd_VISUAL_REMOVEALLCHILDREN.Type = MILCMD.MilCmdVisualRemoveAllChildren;
				milcmd_VISUAL_REMOVEALLCHILDREN.Handle = hCompositionNode;
				channel.SendCommand((byte*)(&milcmd_VISUAL_REMOVEALLCHILDREN), sizeof(DUCE.MILCMD_VISUAL_REMOVEALLCHILDREN));
			}

			// Token: 0x06005972 RID: 22898 RVA: 0x0016A8E4 File Offset: 0x00169CE4
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void InsertChildAt(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hChild, uint iPosition, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL_INSERTCHILDAT milcmd_VISUAL_INSERTCHILDAT;
				milcmd_VISUAL_INSERTCHILDAT.Type = MILCMD.MilCmdVisualInsertChildAt;
				milcmd_VISUAL_INSERTCHILDAT.Handle = hCompositionNode;
				milcmd_VISUAL_INSERTCHILDAT.hChild = hChild;
				milcmd_VISUAL_INSERTCHILDAT.index = iPosition;
				channel.SendCommand((byte*)(&milcmd_VISUAL_INSERTCHILDAT), sizeof(DUCE.MILCMD_VISUAL_INSERTCHILDAT));
			}

			// Token: 0x06005973 RID: 22899 RVA: 0x0016A924 File Offset: 0x00169D24
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetGuidelineCollection(DUCE.ResourceHandle hCompositionNode, DoubleCollection guidelinesX, DoubleCollection guidelinesY, DUCE.Channel channel)
			{
				int num = (guidelinesX == null) ? 0 : guidelinesX.Count;
				int num2 = (guidelinesY == null) ? 0 : guidelinesY.Count;
				checked
				{
					int num3 = num + num2;
					DUCE.MILCMD_VISUAL_SETGUIDELINECOLLECTION milcmd_VISUAL_SETGUIDELINECOLLECTION;
					milcmd_VISUAL_SETGUIDELINECOLLECTION.Type = MILCMD.MilCmdVisualSetGuidelineCollection;
					milcmd_VISUAL_SETGUIDELINECOLLECTION.Handle = hCompositionNode;
					milcmd_VISUAL_SETGUIDELINECOLLECTION.countX = (ushort)num;
					milcmd_VISUAL_SETGUIDELINECOLLECTION.countY = (ushort)num2;
					if (num == 0 && num2 == 0)
					{
						channel.SendCommand(unchecked((byte*)(&milcmd_VISUAL_SETGUIDELINECOLLECTION)), sizeof(DUCE.MILCMD_VISUAL_SETGUIDELINECOLLECTION));
						return;
					}
					double[] array = new double[num3];
					if (num != 0)
					{
						guidelinesX.CopyTo(array, 0);
						Array.Sort<double>(array, 0, num);
					}
					if (num2 != 0)
					{
						guidelinesY.CopyTo(array, num);
						Array.Sort<double>(array, num, num2);
					}
					float[] array2 = new float[num3];
					for (int i = 0; i < num3; i++)
					{
						array2[i] = (float)array[i];
					}
					channel.BeginCommand(unchecked((byte*)(&milcmd_VISUAL_SETGUIDELINECOLLECTION)), sizeof(DUCE.MILCMD_VISUAL_SETGUIDELINECOLLECTION), 4 * num3);
					float[] array3;
					float* pbCommandData;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						pbCommandData = null;
					}
					else
					{
						pbCommandData = &array3[0];
					}
					channel.AppendCommandData((byte*)pbCommandData, 4 * num3);
					array3 = null;
					channel.EndCommand();
				}
			}
		}

		// Token: 0x020008EC RID: 2284
		internal static class Viewport3DVisualNode
		{
			// Token: 0x06005974 RID: 22900 RVA: 0x0016AA1C File Offset: 0x00169E1C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetCamera(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hCamera, DUCE.Channel channel)
			{
				DUCE.MILCMD_VIEWPORT3DVISUAL_SETCAMERA milcmd_VIEWPORT3DVISUAL_SETCAMERA;
				milcmd_VIEWPORT3DVISUAL_SETCAMERA.Type = MILCMD.MilCmdViewport3DVisualSetCamera;
				milcmd_VIEWPORT3DVISUAL_SETCAMERA.Handle = hCompositionNode;
				milcmd_VIEWPORT3DVISUAL_SETCAMERA.hCamera = hCamera;
				channel.SendCommand((byte*)(&milcmd_VIEWPORT3DVISUAL_SETCAMERA), sizeof(DUCE.MILCMD_VIEWPORT3DVISUAL_SETCAMERA));
			}

			// Token: 0x06005975 RID: 22901 RVA: 0x0016AA54 File Offset: 0x00169E54
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetViewport(DUCE.ResourceHandle hCompositionNode, Rect viewport, DUCE.Channel channel)
			{
				DUCE.MILCMD_VIEWPORT3DVISUAL_SETVIEWPORT milcmd_VIEWPORT3DVISUAL_SETVIEWPORT;
				milcmd_VIEWPORT3DVISUAL_SETVIEWPORT.Type = MILCMD.MilCmdViewport3DVisualSetViewport;
				milcmd_VIEWPORT3DVISUAL_SETVIEWPORT.Handle = hCompositionNode;
				milcmd_VIEWPORT3DVISUAL_SETVIEWPORT.Viewport = viewport;
				channel.SendCommand((byte*)(&milcmd_VIEWPORT3DVISUAL_SETVIEWPORT), sizeof(DUCE.MILCMD_VIEWPORT3DVISUAL_SETVIEWPORT));
			}

			// Token: 0x06005976 RID: 22902 RVA: 0x0016AA8C File Offset: 0x00169E8C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void Set3DChild(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hVisual3D, DUCE.Channel channel)
			{
				DUCE.MILCMD_VIEWPORT3DVISUAL_SET3DCHILD milcmd_VIEWPORT3DVISUAL_SET3DCHILD;
				milcmd_VIEWPORT3DVISUAL_SET3DCHILD.Type = MILCMD.MilCmdViewport3DVisualSet3DChild;
				milcmd_VIEWPORT3DVISUAL_SET3DCHILD.Handle = hCompositionNode;
				milcmd_VIEWPORT3DVISUAL_SET3DCHILD.hChild = hVisual3D;
				channel.SendCommand((byte*)(&milcmd_VIEWPORT3DVISUAL_SET3DCHILD), sizeof(DUCE.MILCMD_VIEWPORT3DVISUAL_SET3DCHILD));
			}
		}

		// Token: 0x020008ED RID: 2285
		internal static class Visual3DNode
		{
			// Token: 0x06005977 RID: 22903 RVA: 0x0016AAC4 File Offset: 0x00169EC4
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void RemoveChild(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hChild, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL3D_REMOVECHILD milcmd_VISUAL3D_REMOVECHILD;
				milcmd_VISUAL3D_REMOVECHILD.Type = MILCMD.MilCmdVisual3DRemoveChild;
				milcmd_VISUAL3D_REMOVECHILD.Handle = hCompositionNode;
				milcmd_VISUAL3D_REMOVECHILD.hChild = hChild;
				channel.SendCommand((byte*)(&milcmd_VISUAL3D_REMOVECHILD), sizeof(DUCE.MILCMD_VISUAL3D_REMOVECHILD));
			}

			// Token: 0x06005978 RID: 22904 RVA: 0x0016AAFC File Offset: 0x00169EFC
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void RemoveAllChildren(DUCE.ResourceHandle hCompositionNode, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL3D_REMOVEALLCHILDREN milcmd_VISUAL3D_REMOVEALLCHILDREN;
				milcmd_VISUAL3D_REMOVEALLCHILDREN.Type = MILCMD.MilCmdVisual3DRemoveAllChildren;
				milcmd_VISUAL3D_REMOVEALLCHILDREN.Handle = hCompositionNode;
				channel.SendCommand((byte*)(&milcmd_VISUAL3D_REMOVEALLCHILDREN), sizeof(DUCE.MILCMD_VISUAL3D_REMOVEALLCHILDREN));
			}

			// Token: 0x06005979 RID: 22905 RVA: 0x0016AB2C File Offset: 0x00169F2C
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void InsertChildAt(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hChild, uint iPosition, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL3D_INSERTCHILDAT milcmd_VISUAL3D_INSERTCHILDAT;
				milcmd_VISUAL3D_INSERTCHILDAT.Type = MILCMD.MilCmdVisual3DInsertChildAt;
				milcmd_VISUAL3D_INSERTCHILDAT.Handle = hCompositionNode;
				milcmd_VISUAL3D_INSERTCHILDAT.hChild = hChild;
				milcmd_VISUAL3D_INSERTCHILDAT.index = iPosition;
				channel.SendCommand((byte*)(&milcmd_VISUAL3D_INSERTCHILDAT), sizeof(DUCE.MILCMD_VISUAL3D_INSERTCHILDAT));
			}

			// Token: 0x0600597A RID: 22906 RVA: 0x0016AB6C File Offset: 0x00169F6C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetContent(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hContent, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL3D_SETCONTENT milcmd_VISUAL3D_SETCONTENT;
				milcmd_VISUAL3D_SETCONTENT.Type = MILCMD.MilCmdVisual3DSetContent;
				milcmd_VISUAL3D_SETCONTENT.Handle = hCompositionNode;
				milcmd_VISUAL3D_SETCONTENT.hContent = hContent;
				channel.SendCommand((byte*)(&milcmd_VISUAL3D_SETCONTENT), sizeof(DUCE.MILCMD_VISUAL3D_SETCONTENT));
			}

			// Token: 0x0600597B RID: 22907 RVA: 0x0016ABA4 File Offset: 0x00169FA4
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetTransform(DUCE.ResourceHandle hCompositionNode, DUCE.ResourceHandle hTransform, DUCE.Channel channel)
			{
				DUCE.MILCMD_VISUAL3D_SETTRANSFORM milcmd_VISUAL3D_SETTRANSFORM;
				milcmd_VISUAL3D_SETTRANSFORM.Type = MILCMD.MilCmdVisual3DSetTransform;
				milcmd_VISUAL3D_SETTRANSFORM.Handle = hCompositionNode;
				milcmd_VISUAL3D_SETTRANSFORM.hTransform = hTransform;
				channel.SendCommand((byte*)(&milcmd_VISUAL3D_SETTRANSFORM), sizeof(DUCE.MILCMD_VISUAL3D_SETTRANSFORM));
			}
		}

		// Token: 0x020008EE RID: 2286
		internal static class CompositionTarget
		{
			// Token: 0x0600597C RID: 22908 RVA: 0x0016ABDC File Offset: 0x00169FDC
			[SecurityCritical]
			internal unsafe static void HwndInitialize(DUCE.ResourceHandle hCompositionTarget, IntPtr hWnd, int nWidth, int nHeight, bool softwareOnly, int dpiAwarenessContext, DpiScale dpiScale, DUCE.Channel channel)
			{
				DUCE.MILCMD_HWNDTARGET_CREATE milcmd_HWNDTARGET_CREATE;
				milcmd_HWNDTARGET_CREATE.Type = MILCMD.MilCmdHwndTargetCreate;
				milcmd_HWNDTARGET_CREATE.Handle = hCompositionTarget;
				UIntPtr value = new UIntPtr(hWnd.ToPointer());
				milcmd_HWNDTARGET_CREATE.hwnd = (ulong)value;
				milcmd_HWNDTARGET_CREATE.width = (uint)nWidth;
				milcmd_HWNDTARGET_CREATE.height = (uint)nHeight;
				milcmd_HWNDTARGET_CREATE.clearColor.b = 0f;
				milcmd_HWNDTARGET_CREATE.clearColor.r = 0f;
				milcmd_HWNDTARGET_CREATE.clearColor.g = 0f;
				milcmd_HWNDTARGET_CREATE.clearColor.a = 1f;
				milcmd_HWNDTARGET_CREATE.flags = 12U;
				if (softwareOnly)
				{
					milcmd_HWNDTARGET_CREATE.flags |= 1U;
				}
				bool? enableMultiMonitorDisplayClipping = CoreCompatibilityPreferences.EnableMultiMonitorDisplayClipping;
				if (enableMultiMonitorDisplayClipping != null)
				{
					milcmd_HWNDTARGET_CREATE.flags |= 32768U;
					if (!enableMultiMonitorDisplayClipping.Value)
					{
						milcmd_HWNDTARGET_CREATE.flags |= 16384U;
					}
				}
				if (CoreAppContextSwitches.DisableDirtyRectangles)
				{
					milcmd_HWNDTARGET_CREATE.flags |= 65536U;
				}
				milcmd_HWNDTARGET_CREATE.hBitmap = DUCE.ResourceHandle.Null;
				milcmd_HWNDTARGET_CREATE.stride = 0U;
				milcmd_HWNDTARGET_CREATE.ePixelFormat = 0U;
				milcmd_HWNDTARGET_CREATE.hSection = 0UL;
				milcmd_HWNDTARGET_CREATE.masterDevice = 0UL;
				milcmd_HWNDTARGET_CREATE.DpiAwarenessContext = dpiAwarenessContext;
				milcmd_HWNDTARGET_CREATE.DpiX = dpiScale.DpiScaleX;
				milcmd_HWNDTARGET_CREATE.DpiY = dpiScale.DpiScaleY;
				channel.SendCommand((byte*)(&milcmd_HWNDTARGET_CREATE), sizeof(DUCE.MILCMD_HWNDTARGET_CREATE), false);
			}

			// Token: 0x0600597D RID: 22909 RVA: 0x0016AD38 File Offset: 0x0016A138
			[SecuritySafeCritical]
			internal unsafe static void ProcessDpiChanged(DUCE.ResourceHandle hCompositionTarget, DpiScale dpiScale, bool afterParent, DUCE.Channel channel)
			{
				DUCE.MILCMD_HWNDTARGET_DPICHANGED milcmd_HWNDTARGET_DPICHANGED;
				milcmd_HWNDTARGET_DPICHANGED.Type = MILCMD.MilCmdHwndTargetDpiChanged;
				milcmd_HWNDTARGET_DPICHANGED.Handle = hCompositionTarget;
				milcmd_HWNDTARGET_DPICHANGED.DpiX = dpiScale.DpiScaleX;
				milcmd_HWNDTARGET_DPICHANGED.DpiY = dpiScale.DpiScaleY;
				milcmd_HWNDTARGET_DPICHANGED.AfterParent = (afterParent ? 1U : 0U);
				channel.SendCommand((byte*)(&milcmd_HWNDTARGET_DPICHANGED), sizeof(DUCE.MILCMD_HWNDTARGET_DPICHANGED), false);
			}

			// Token: 0x0600597E RID: 22910 RVA: 0x0016AD90 File Offset: 0x0016A190
			[SecurityCritical]
			internal unsafe static void PrintInitialize(DUCE.ResourceHandle hCompositionTarget, IntPtr pRenderTarget, int nWidth, int nHeight, DUCE.Channel channel)
			{
				DUCE.MILCMD_GENERICTARGET_CREATE milcmd_GENERICTARGET_CREATE;
				milcmd_GENERICTARGET_CREATE.Type = MILCMD.MilCmdGenericTargetCreate;
				milcmd_GENERICTARGET_CREATE.Handle = hCompositionTarget;
				milcmd_GENERICTARGET_CREATE.hwnd = 0UL;
				milcmd_GENERICTARGET_CREATE.pRenderTarget = (ulong)((long)pRenderTarget);
				milcmd_GENERICTARGET_CREATE.width = (uint)nWidth;
				milcmd_GENERICTARGET_CREATE.height = (uint)nHeight;
				milcmd_GENERICTARGET_CREATE.dummy = 0U;
				channel.SendCommand((byte*)(&milcmd_GENERICTARGET_CREATE), sizeof(DUCE.MILCMD_GENERICTARGET_CREATE), false);
			}

			// Token: 0x0600597F RID: 22911 RVA: 0x0016ADF0 File Offset: 0x0016A1F0
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetClearColor(DUCE.ResourceHandle hCompositionTarget, Color color, DUCE.Channel channel)
			{
				DUCE.MILCMD_TARGET_SETCLEARCOLOR milcmd_TARGET_SETCLEARCOLOR;
				milcmd_TARGET_SETCLEARCOLOR.Type = MILCMD.MilCmdTargetSetClearColor;
				milcmd_TARGET_SETCLEARCOLOR.Handle = hCompositionTarget;
				milcmd_TARGET_SETCLEARCOLOR.clearColor.b = color.ScB;
				milcmd_TARGET_SETCLEARCOLOR.clearColor.r = color.ScR;
				milcmd_TARGET_SETCLEARCOLOR.clearColor.g = color.ScG;
				milcmd_TARGET_SETCLEARCOLOR.clearColor.a = color.ScA;
				channel.SendCommand((byte*)(&milcmd_TARGET_SETCLEARCOLOR), sizeof(DUCE.MILCMD_TARGET_SETCLEARCOLOR));
			}

			// Token: 0x06005980 RID: 22912 RVA: 0x0016AE6C File Offset: 0x0016A26C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void SetRenderingMode(DUCE.ResourceHandle hCompositionTarget, MILRTInitializationFlags nRenderingMode, DUCE.Channel channel)
			{
				DUCE.MILCMD_TARGET_SETFLAGS milcmd_TARGET_SETFLAGS;
				milcmd_TARGET_SETFLAGS.Type = MILCMD.MilCmdTargetSetFlags;
				milcmd_TARGET_SETFLAGS.Handle = hCompositionTarget;
				milcmd_TARGET_SETFLAGS.flags = (uint)nRenderingMode;
				channel.SendCommand((byte*)(&milcmd_TARGET_SETFLAGS), sizeof(DUCE.MILCMD_TARGET_SETFLAGS));
			}

			// Token: 0x06005981 RID: 22913 RVA: 0x0016AEA4 File Offset: 0x0016A2A4
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void SetRoot(DUCE.ResourceHandle hCompositionTarget, DUCE.ResourceHandle hRoot, DUCE.Channel channel)
			{
				DUCE.MILCMD_TARGET_SETROOT milcmd_TARGET_SETROOT;
				milcmd_TARGET_SETROOT.Type = MILCMD.MilCmdTargetSetRoot;
				milcmd_TARGET_SETROOT.Handle = hCompositionTarget;
				milcmd_TARGET_SETROOT.hRoot = hRoot;
				channel.SendCommand((byte*)(&milcmd_TARGET_SETROOT), sizeof(DUCE.MILCMD_TARGET_SETROOT));
			}

			// Token: 0x06005982 RID: 22914 RVA: 0x0016AEDC File Offset: 0x0016A2DC
			[SecurityCritical]
			internal unsafe static void UpdateWindowSettings(DUCE.ResourceHandle hCompositionTarget, NativeMethods.RECT windowRect, Color colorKey, float constantAlpha, MILWindowLayerType windowLayerType, MILTransparencyFlags transparencyMode, bool isChild, bool isRTL, bool renderingEnabled, int disableCookie, DUCE.Channel channel)
			{
				DUCE.MILCMD_TARGET_UPDATEWINDOWSETTINGS milcmd_TARGET_UPDATEWINDOWSETTINGS;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.Type = MILCMD.MilCmdTargetUpdateWindowSettings;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.Handle = hCompositionTarget;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.renderingEnabled = (renderingEnabled ? 1U : 0U);
				milcmd_TARGET_UPDATEWINDOWSETTINGS.disableCookie = (uint)disableCookie;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.windowRect = windowRect;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.colorKey.b = colorKey.ScB;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.colorKey.r = colorKey.ScR;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.colorKey.g = colorKey.ScG;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.colorKey.a = colorKey.ScA;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.constantAlpha = constantAlpha;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.transparencyMode = transparencyMode;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.windowLayerType = windowLayerType;
				milcmd_TARGET_UPDATEWINDOWSETTINGS.isChild = (isChild ? 1U : 0U);
				milcmd_TARGET_UPDATEWINDOWSETTINGS.isRTL = (isRTL ? 1U : 0U);
				channel.SendCommand((byte*)(&milcmd_TARGET_UPDATEWINDOWSETTINGS), sizeof(DUCE.MILCMD_TARGET_UPDATEWINDOWSETTINGS));
			}

			// Token: 0x06005983 RID: 22915 RVA: 0x0016AFB0 File Offset: 0x0016A3B0
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal unsafe static void Invalidate(DUCE.ResourceHandle hCompositionTarget, ref NativeMethods.RECT pRect, DUCE.Channel channel)
			{
				DUCE.MILCMD_TARGET_INVALIDATE milcmd_TARGET_INVALIDATE;
				milcmd_TARGET_INVALIDATE.Type = MILCMD.MilCmdTargetInvalidate;
				milcmd_TARGET_INVALIDATE.Handle = hCompositionTarget;
				milcmd_TARGET_INVALIDATE.rc = pRect;
				channel.SendCommand((byte*)(&milcmd_TARGET_INVALIDATE), sizeof(DUCE.MILCMD_TARGET_INVALIDATE), false);
				channel.CloseBatch();
				channel.Commit();
			}
		}

		// Token: 0x020008EF RID: 2287
		internal static class ETWEvent
		{
			// Token: 0x06005984 RID: 22916 RVA: 0x0016AFF8 File Offset: 0x0016A3F8
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe static void RaiseEvent(DUCE.ResourceHandle hEtwEvent, uint id, DUCE.Channel channel)
			{
				DUCE.MILCMD_ETWEVENTRESOURCE milcmd_ETWEVENTRESOURCE;
				milcmd_ETWEVENTRESOURCE.Type = MILCMD.MilCmdEtwEventResource;
				milcmd_ETWEVENTRESOURCE.Handle = hEtwEvent;
				milcmd_ETWEVENTRESOURCE.id = id;
				channel.SendCommand((byte*)(&milcmd_ETWEVENTRESOURCE), sizeof(DUCE.MILCMD_ETWEVENTRESOURCE));
			}
		}

		// Token: 0x020008F0 RID: 2288
		internal interface IResource
		{
			// Token: 0x06005985 RID: 22917
			DUCE.ResourceHandle AddRefOnChannel(DUCE.Channel channel);

			// Token: 0x06005986 RID: 22918
			int GetChannelCount();

			// Token: 0x06005987 RID: 22919
			DUCE.Channel GetChannel(int index);

			// Token: 0x06005988 RID: 22920
			void ReleaseOnChannel(DUCE.Channel channel);

			// Token: 0x06005989 RID: 22921
			DUCE.ResourceHandle GetHandle(DUCE.Channel channel);

			// Token: 0x0600598A RID: 22922
			DUCE.ResourceHandle Get3DHandle(DUCE.Channel channel);

			// Token: 0x0600598B RID: 22923
			void RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel);
		}

		// Token: 0x020008F1 RID: 2289
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PARTITION_REGISTERFORNOTIFICATIONS
		{
			// Token: 0x040029B9 RID: 10681
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029BA RID: 10682
			[FieldOffset(4)]
			internal uint Enable;
		}

		// Token: 0x020008F2 RID: 2290
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_CHANNEL_REQUESTTIER
		{
			// Token: 0x040029BB RID: 10683
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029BC RID: 10684
			[FieldOffset(4)]
			internal uint ReturnCommonMinimum;
		}

		// Token: 0x020008F3 RID: 2291
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PARTITION_SETVBLANKSYNCMODE
		{
			// Token: 0x040029BD RID: 10685
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029BE RID: 10686
			[FieldOffset(4)]
			internal uint Enable;
		}

		// Token: 0x020008F4 RID: 2292
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PARTITION_NOTIFYPRESENT
		{
			// Token: 0x040029BF RID: 10687
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029C0 RID: 10688
			[FieldOffset(4)]
			internal ulong FrameTime;
		}

		// Token: 0x020008F5 RID: 2293
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_D3DIMAGE
		{
			// Token: 0x040029C1 RID: 10689
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029C2 RID: 10690
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029C3 RID: 10691
			[FieldOffset(8)]
			internal ulong pInteropDeviceBitmap;

			// Token: 0x040029C4 RID: 10692
			[FieldOffset(16)]
			internal ulong pSoftwareBitmap;
		}

		// Token: 0x020008F6 RID: 2294
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_D3DIMAGE_PRESENT
		{
			// Token: 0x040029C5 RID: 10693
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029C6 RID: 10694
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029C7 RID: 10695
			[FieldOffset(8)]
			internal ulong hEvent;
		}

		// Token: 0x020008F7 RID: 2295
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_BITMAP_INVALIDATE
		{
			// Token: 0x040029C8 RID: 10696
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029C9 RID: 10697
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029CA RID: 10698
			[FieldOffset(8)]
			internal uint UseDirtyRect;

			// Token: 0x040029CB RID: 10699
			[FieldOffset(12)]
			internal NativeMethods.RECT DirtyRect;
		}

		// Token: 0x020008F8 RID: 2296
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DOUBLERESOURCE
		{
			// Token: 0x040029CC RID: 10700
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029CD RID: 10701
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029CE RID: 10702
			[FieldOffset(8)]
			internal double Value;
		}

		// Token: 0x020008F9 RID: 2297
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_COLORRESOURCE
		{
			// Token: 0x040029CF RID: 10703
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029D0 RID: 10704
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029D1 RID: 10705
			[FieldOffset(8)]
			internal MilColorF Value;
		}

		// Token: 0x020008FA RID: 2298
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_POINTRESOURCE
		{
			// Token: 0x040029D2 RID: 10706
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029D3 RID: 10707
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029D4 RID: 10708
			[FieldOffset(8)]
			internal Point Value;
		}

		// Token: 0x020008FB RID: 2299
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_RECTRESOURCE
		{
			// Token: 0x040029D5 RID: 10709
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029D6 RID: 10710
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029D7 RID: 10711
			[FieldOffset(8)]
			internal Rect Value;
		}

		// Token: 0x020008FC RID: 2300
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SIZERESOURCE
		{
			// Token: 0x040029D8 RID: 10712
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029D9 RID: 10713
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029DA RID: 10714
			[FieldOffset(8)]
			internal Size Value;
		}

		// Token: 0x020008FD RID: 2301
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MATRIXRESOURCE
		{
			// Token: 0x040029DB RID: 10715
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029DC RID: 10716
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029DD RID: 10717
			[FieldOffset(8)]
			internal MilMatrix3x2D Value;
		}

		// Token: 0x020008FE RID: 2302
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_POINT3DRESOURCE
		{
			// Token: 0x040029DE RID: 10718
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029DF RID: 10719
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029E0 RID: 10720
			[FieldOffset(8)]
			internal MilPoint3F Value;
		}

		// Token: 0x020008FF RID: 2303
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VECTOR3DRESOURCE
		{
			// Token: 0x040029E1 RID: 10721
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029E2 RID: 10722
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029E3 RID: 10723
			[FieldOffset(8)]
			internal MilPoint3F Value;
		}

		// Token: 0x02000900 RID: 2304
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_QUATERNIONRESOURCE
		{
			// Token: 0x040029E4 RID: 10724
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029E5 RID: 10725
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029E6 RID: 10726
			[FieldOffset(8)]
			internal MilQuaternionF Value;
		}

		// Token: 0x02000901 RID: 2305
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_RENDERDATA
		{
			// Token: 0x040029E7 RID: 10727
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029E8 RID: 10728
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029E9 RID: 10729
			[FieldOffset(8)]
			internal uint cbData;
		}

		// Token: 0x02000902 RID: 2306
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_ETWEVENTRESOURCE
		{
			// Token: 0x040029EA RID: 10730
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029EB RID: 10731
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029EC RID: 10732
			[FieldOffset(8)]
			internal uint id;
		}

		// Token: 0x02000903 RID: 2307
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETOFFSET
		{
			// Token: 0x040029ED RID: 10733
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029EE RID: 10734
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029EF RID: 10735
			[FieldOffset(8)]
			internal double offsetX;

			// Token: 0x040029F0 RID: 10736
			[FieldOffset(16)]
			internal double offsetY;
		}

		// Token: 0x02000904 RID: 2308
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETTRANSFORM
		{
			// Token: 0x040029F1 RID: 10737
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029F2 RID: 10738
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029F3 RID: 10739
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hTransform;
		}

		// Token: 0x02000905 RID: 2309
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETEFFECT
		{
			// Token: 0x040029F4 RID: 10740
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029F5 RID: 10741
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029F6 RID: 10742
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hEffect;
		}

		// Token: 0x02000906 RID: 2310
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETCACHEMODE
		{
			// Token: 0x040029F7 RID: 10743
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029F8 RID: 10744
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029F9 RID: 10745
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hCacheMode;
		}

		// Token: 0x02000907 RID: 2311
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETCLIP
		{
			// Token: 0x040029FA RID: 10746
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029FB RID: 10747
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029FC RID: 10748
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hClip;
		}

		// Token: 0x02000908 RID: 2312
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETALPHA
		{
			// Token: 0x040029FD RID: 10749
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x040029FE RID: 10750
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x040029FF RID: 10751
			[FieldOffset(8)]
			internal double alpha;
		}

		// Token: 0x02000909 RID: 2313
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETRENDEROPTIONS
		{
			// Token: 0x04002A00 RID: 10752
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A01 RID: 10753
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A02 RID: 10754
			[FieldOffset(8)]
			internal MilRenderOptions renderOptions;
		}

		// Token: 0x0200090A RID: 2314
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETCONTENT
		{
			// Token: 0x04002A03 RID: 10755
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A04 RID: 10756
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A05 RID: 10757
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hContent;
		}

		// Token: 0x0200090B RID: 2315
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETALPHAMASK
		{
			// Token: 0x04002A06 RID: 10758
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A07 RID: 10759
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A08 RID: 10760
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hAlphaMask;
		}

		// Token: 0x0200090C RID: 2316
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_REMOVEALLCHILDREN
		{
			// Token: 0x04002A09 RID: 10761
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A0A RID: 10762
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;
		}

		// Token: 0x0200090D RID: 2317
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_REMOVECHILD
		{
			// Token: 0x04002A0B RID: 10763
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A0C RID: 10764
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A0D RID: 10765
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hChild;
		}

		// Token: 0x0200090E RID: 2318
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_INSERTCHILDAT
		{
			// Token: 0x04002A0E RID: 10766
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A0F RID: 10767
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A10 RID: 10768
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hChild;

			// Token: 0x04002A11 RID: 10769
			[FieldOffset(12)]
			internal uint index;
		}

		// Token: 0x0200090F RID: 2319
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETGUIDELINECOLLECTION
		{
			// Token: 0x04002A12 RID: 10770
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A13 RID: 10771
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A14 RID: 10772
			[FieldOffset(8)]
			internal ushort countX;

			// Token: 0x04002A15 RID: 10773
			[FieldOffset(12)]
			internal ushort countY;

			// Token: 0x04002A16 RID: 10774
			[FieldOffset(15)]
			private byte BYTEPacking0;
		}

		// Token: 0x02000910 RID: 2320
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL_SETSCROLLABLEAREACLIP
		{
			// Token: 0x04002A17 RID: 10775
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A18 RID: 10776
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A19 RID: 10777
			[FieldOffset(8)]
			internal Rect Clip;

			// Token: 0x04002A1A RID: 10778
			[FieldOffset(40)]
			internal uint IsEnabled;
		}

		// Token: 0x02000911 RID: 2321
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VIEWPORT3DVISUAL_SETCAMERA
		{
			// Token: 0x04002A1B RID: 10779
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A1C RID: 10780
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A1D RID: 10781
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hCamera;
		}

		// Token: 0x02000912 RID: 2322
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VIEWPORT3DVISUAL_SETVIEWPORT
		{
			// Token: 0x04002A1E RID: 10782
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A1F RID: 10783
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A20 RID: 10784
			[FieldOffset(8)]
			internal Rect Viewport;
		}

		// Token: 0x02000913 RID: 2323
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VIEWPORT3DVISUAL_SET3DCHILD
		{
			// Token: 0x04002A21 RID: 10785
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A22 RID: 10786
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A23 RID: 10787
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hChild;
		}

		// Token: 0x02000914 RID: 2324
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL3D_SETCONTENT
		{
			// Token: 0x04002A24 RID: 10788
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A25 RID: 10789
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A26 RID: 10790
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hContent;
		}

		// Token: 0x02000915 RID: 2325
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL3D_SETTRANSFORM
		{
			// Token: 0x04002A27 RID: 10791
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A28 RID: 10792
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A29 RID: 10793
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hTransform;
		}

		// Token: 0x02000916 RID: 2326
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL3D_REMOVEALLCHILDREN
		{
			// Token: 0x04002A2A RID: 10794
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A2B RID: 10795
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;
		}

		// Token: 0x02000917 RID: 2327
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL3D_REMOVECHILD
		{
			// Token: 0x04002A2C RID: 10796
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A2D RID: 10797
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A2E RID: 10798
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hChild;
		}

		// Token: 0x02000918 RID: 2328
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUAL3D_INSERTCHILDAT
		{
			// Token: 0x04002A2F RID: 10799
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A30 RID: 10800
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A31 RID: 10801
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hChild;

			// Token: 0x04002A32 RID: 10802
			[FieldOffset(12)]
			internal uint index;
		}

		// Token: 0x02000919 RID: 2329
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_HWNDTARGET_CREATE
		{
			// Token: 0x04002A33 RID: 10803
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A34 RID: 10804
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A35 RID: 10805
			[FieldOffset(8)]
			internal ulong hwnd;

			// Token: 0x04002A36 RID: 10806
			[FieldOffset(16)]
			internal ulong hSection;

			// Token: 0x04002A37 RID: 10807
			[FieldOffset(24)]
			internal ulong masterDevice;

			// Token: 0x04002A38 RID: 10808
			[FieldOffset(32)]
			internal uint width;

			// Token: 0x04002A39 RID: 10809
			[FieldOffset(36)]
			internal uint height;

			// Token: 0x04002A3A RID: 10810
			[FieldOffset(40)]
			internal MilColorF clearColor;

			// Token: 0x04002A3B RID: 10811
			[FieldOffset(56)]
			internal uint flags;

			// Token: 0x04002A3C RID: 10812
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hBitmap;

			// Token: 0x04002A3D RID: 10813
			[FieldOffset(64)]
			internal uint stride;

			// Token: 0x04002A3E RID: 10814
			[FieldOffset(68)]
			internal uint ePixelFormat;

			// Token: 0x04002A3F RID: 10815
			[FieldOffset(72)]
			internal int DpiAwarenessContext;

			// Token: 0x04002A40 RID: 10816
			[FieldOffset(76)]
			internal double DpiX;

			// Token: 0x04002A41 RID: 10817
			[FieldOffset(84)]
			internal double DpiY;
		}

		// Token: 0x0200091A RID: 2330
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TARGET_UPDATEWINDOWSETTINGS
		{
			// Token: 0x04002A42 RID: 10818
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A43 RID: 10819
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A44 RID: 10820
			[FieldOffset(8)]
			internal NativeMethods.RECT windowRect;

			// Token: 0x04002A45 RID: 10821
			[FieldOffset(24)]
			internal MILWindowLayerType windowLayerType;

			// Token: 0x04002A46 RID: 10822
			[FieldOffset(28)]
			internal MILTransparencyFlags transparencyMode;

			// Token: 0x04002A47 RID: 10823
			[FieldOffset(32)]
			internal float constantAlpha;

			// Token: 0x04002A48 RID: 10824
			[FieldOffset(36)]
			internal uint isChild;

			// Token: 0x04002A49 RID: 10825
			[FieldOffset(40)]
			internal uint isRTL;

			// Token: 0x04002A4A RID: 10826
			[FieldOffset(44)]
			internal uint renderingEnabled;

			// Token: 0x04002A4B RID: 10827
			[FieldOffset(48)]
			internal MilColorF colorKey;

			// Token: 0x04002A4C RID: 10828
			[FieldOffset(64)]
			internal uint disableCookie;

			// Token: 0x04002A4D RID: 10829
			[FieldOffset(68)]
			internal uint gdiBlt;
		}

		// Token: 0x0200091B RID: 2331
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GENERICTARGET_CREATE
		{
			// Token: 0x04002A4E RID: 10830
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A4F RID: 10831
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A50 RID: 10832
			[FieldOffset(8)]
			internal ulong hwnd;

			// Token: 0x04002A51 RID: 10833
			[FieldOffset(16)]
			internal ulong pRenderTarget;

			// Token: 0x04002A52 RID: 10834
			[FieldOffset(24)]
			internal uint width;

			// Token: 0x04002A53 RID: 10835
			[FieldOffset(28)]
			internal uint height;

			// Token: 0x04002A54 RID: 10836
			[FieldOffset(32)]
			internal uint dummy;
		}

		// Token: 0x0200091C RID: 2332
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TARGET_SETROOT
		{
			// Token: 0x04002A55 RID: 10837
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A56 RID: 10838
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A57 RID: 10839
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hRoot;
		}

		// Token: 0x0200091D RID: 2333
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TARGET_SETCLEARCOLOR
		{
			// Token: 0x04002A58 RID: 10840
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A59 RID: 10841
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A5A RID: 10842
			[FieldOffset(8)]
			internal MilColorF clearColor;
		}

		// Token: 0x0200091E RID: 2334
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TARGET_INVALIDATE
		{
			// Token: 0x04002A5B RID: 10843
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A5C RID: 10844
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A5D RID: 10845
			[FieldOffset(8)]
			internal NativeMethods.RECT rc;
		}

		// Token: 0x0200091F RID: 2335
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TARGET_SETFLAGS
		{
			// Token: 0x04002A5E RID: 10846
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A5F RID: 10847
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A60 RID: 10848
			[FieldOffset(8)]
			internal uint flags;
		}

		// Token: 0x02000920 RID: 2336
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_HWNDTARGET_DPICHANGED
		{
			// Token: 0x04002A61 RID: 10849
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A62 RID: 10850
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A63 RID: 10851
			[FieldOffset(8)]
			internal double DpiX;

			// Token: 0x04002A64 RID: 10852
			[FieldOffset(16)]
			internal double DpiY;

			// Token: 0x04002A65 RID: 10853
			[FieldOffset(24)]
			internal uint AfterParent;
		}

		// Token: 0x02000921 RID: 2337
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GLYPHRUN_CREATE
		{
			// Token: 0x04002A66 RID: 10854
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A67 RID: 10855
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A68 RID: 10856
			[FieldOffset(8)]
			internal ulong pIDWriteFont;

			// Token: 0x04002A69 RID: 10857
			[FieldOffset(16)]
			internal ushort GlyphRunFlags;

			// Token: 0x04002A6A RID: 10858
			[FieldOffset(20)]
			internal MilPoint2F Origin;

			// Token: 0x04002A6B RID: 10859
			[FieldOffset(28)]
			internal float MuSize;

			// Token: 0x04002A6C RID: 10860
			[FieldOffset(32)]
			internal Rect ManagedBounds;

			// Token: 0x04002A6D RID: 10861
			[FieldOffset(64)]
			internal ushort GlyphCount;

			// Token: 0x04002A6E RID: 10862
			[FieldOffset(68)]
			internal ushort BidiLevel;

			// Token: 0x04002A6F RID: 10863
			[FieldOffset(72)]
			internal ushort DWriteTextMeasuringMethod;

			// Token: 0x04002A70 RID: 10864
			[FieldOffset(75)]
			private byte BYTEPacking0;
		}

		// Token: 0x02000922 RID: 2338
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DOUBLEBUFFEREDBITMAP
		{
			// Token: 0x04002A71 RID: 10865
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A72 RID: 10866
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A73 RID: 10867
			[FieldOffset(8)]
			internal ulong SwDoubleBufferedBitmap;

			// Token: 0x04002A74 RID: 10868
			[FieldOffset(16)]
			internal uint UseBackBuffer;
		}

		// Token: 0x02000923 RID: 2339
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DOUBLEBUFFEREDBITMAP_COPYFORWARD
		{
			// Token: 0x04002A75 RID: 10869
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A76 RID: 10870
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A77 RID: 10871
			[FieldOffset(8)]
			internal ulong CopyCompletedEvent;
		}

		// Token: 0x02000924 RID: 2340
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PARTITION_NOTIFYPOLICYCHANGEFORNONINTERACTIVEMODE
		{
			// Token: 0x04002A78 RID: 10872
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A79 RID: 10873
			[FieldOffset(4)]
			internal uint ShouldRenderEvenWhenNoDisplayDevicesAreAvailable;
		}

		// Token: 0x02000925 RID: 2341
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_AXISANGLEROTATION3D
		{
			// Token: 0x04002A7A RID: 10874
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A7B RID: 10875
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A7C RID: 10876
			[FieldOffset(8)]
			internal double angle;

			// Token: 0x04002A7D RID: 10877
			[FieldOffset(16)]
			internal MilPoint3F axis;

			// Token: 0x04002A7E RID: 10878
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hAxisAnimations;

			// Token: 0x04002A7F RID: 10879
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hAngleAnimations;
		}

		// Token: 0x02000926 RID: 2342
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_QUATERNIONROTATION3D
		{
			// Token: 0x04002A80 RID: 10880
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A81 RID: 10881
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A82 RID: 10882
			[FieldOffset(8)]
			internal MilQuaternionF quaternion;

			// Token: 0x04002A83 RID: 10883
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hQuaternionAnimations;
		}

		// Token: 0x02000927 RID: 2343
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PERSPECTIVECAMERA
		{
			// Token: 0x04002A84 RID: 10884
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A85 RID: 10885
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A86 RID: 10886
			[FieldOffset(8)]
			internal double nearPlaneDistance;

			// Token: 0x04002A87 RID: 10887
			[FieldOffset(16)]
			internal double farPlaneDistance;

			// Token: 0x04002A88 RID: 10888
			[FieldOffset(24)]
			internal double fieldOfView;

			// Token: 0x04002A89 RID: 10889
			[FieldOffset(32)]
			internal MilPoint3F position;

			// Token: 0x04002A8A RID: 10890
			[FieldOffset(44)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002A8B RID: 10891
			[FieldOffset(48)]
			internal MilPoint3F lookDirection;

			// Token: 0x04002A8C RID: 10892
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hNearPlaneDistanceAnimations;

			// Token: 0x04002A8D RID: 10893
			[FieldOffset(64)]
			internal MilPoint3F upDirection;

			// Token: 0x04002A8E RID: 10894
			[FieldOffset(76)]
			internal DUCE.ResourceHandle hFarPlaneDistanceAnimations;

			// Token: 0x04002A8F RID: 10895
			[FieldOffset(80)]
			internal DUCE.ResourceHandle hPositionAnimations;

			// Token: 0x04002A90 RID: 10896
			[FieldOffset(84)]
			internal DUCE.ResourceHandle hLookDirectionAnimations;

			// Token: 0x04002A91 RID: 10897
			[FieldOffset(88)]
			internal DUCE.ResourceHandle hUpDirectionAnimations;

			// Token: 0x04002A92 RID: 10898
			[FieldOffset(92)]
			internal DUCE.ResourceHandle hFieldOfViewAnimations;
		}

		// Token: 0x02000928 RID: 2344
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_ORTHOGRAPHICCAMERA
		{
			// Token: 0x04002A93 RID: 10899
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002A94 RID: 10900
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002A95 RID: 10901
			[FieldOffset(8)]
			internal double nearPlaneDistance;

			// Token: 0x04002A96 RID: 10902
			[FieldOffset(16)]
			internal double farPlaneDistance;

			// Token: 0x04002A97 RID: 10903
			[FieldOffset(24)]
			internal double width;

			// Token: 0x04002A98 RID: 10904
			[FieldOffset(32)]
			internal MilPoint3F position;

			// Token: 0x04002A99 RID: 10905
			[FieldOffset(44)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002A9A RID: 10906
			[FieldOffset(48)]
			internal MilPoint3F lookDirection;

			// Token: 0x04002A9B RID: 10907
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hNearPlaneDistanceAnimations;

			// Token: 0x04002A9C RID: 10908
			[FieldOffset(64)]
			internal MilPoint3F upDirection;

			// Token: 0x04002A9D RID: 10909
			[FieldOffset(76)]
			internal DUCE.ResourceHandle hFarPlaneDistanceAnimations;

			// Token: 0x04002A9E RID: 10910
			[FieldOffset(80)]
			internal DUCE.ResourceHandle hPositionAnimations;

			// Token: 0x04002A9F RID: 10911
			[FieldOffset(84)]
			internal DUCE.ResourceHandle hLookDirectionAnimations;

			// Token: 0x04002AA0 RID: 10912
			[FieldOffset(88)]
			internal DUCE.ResourceHandle hUpDirectionAnimations;

			// Token: 0x04002AA1 RID: 10913
			[FieldOffset(92)]
			internal DUCE.ResourceHandle hWidthAnimations;
		}

		// Token: 0x02000929 RID: 2345
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MATRIXCAMERA
		{
			// Token: 0x04002AA2 RID: 10914
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AA3 RID: 10915
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AA4 RID: 10916
			[FieldOffset(8)]
			internal D3DMATRIX viewMatrix;

			// Token: 0x04002AA5 RID: 10917
			[FieldOffset(72)]
			internal D3DMATRIX projectionMatrix;

			// Token: 0x04002AA6 RID: 10918
			[FieldOffset(136)]
			internal DUCE.ResourceHandle htransform;
		}

		// Token: 0x0200092A RID: 2346
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MODEL3DGROUP
		{
			// Token: 0x04002AA7 RID: 10919
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AA8 RID: 10920
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AA9 RID: 10921
			[FieldOffset(8)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002AAA RID: 10922
			[FieldOffset(12)]
			internal uint ChildrenSize;
		}

		// Token: 0x0200092B RID: 2347
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_AMBIENTLIGHT
		{
			// Token: 0x04002AAB RID: 10923
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AAC RID: 10924
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AAD RID: 10925
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AAE RID: 10926
			[FieldOffset(24)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002AAF RID: 10927
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hColorAnimations;
		}

		// Token: 0x0200092C RID: 2348
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DIRECTIONALLIGHT
		{
			// Token: 0x04002AB0 RID: 10928
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AB1 RID: 10929
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AB2 RID: 10930
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AB3 RID: 10931
			[FieldOffset(24)]
			internal MilPoint3F direction;

			// Token: 0x04002AB4 RID: 10932
			[FieldOffset(36)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002AB5 RID: 10933
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hColorAnimations;

			// Token: 0x04002AB6 RID: 10934
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hDirectionAnimations;
		}

		// Token: 0x0200092D RID: 2349
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_POINTLIGHT
		{
			// Token: 0x04002AB7 RID: 10935
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AB8 RID: 10936
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AB9 RID: 10937
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002ABA RID: 10938
			[FieldOffset(24)]
			internal double range;

			// Token: 0x04002ABB RID: 10939
			[FieldOffset(32)]
			internal double constantAttenuation;

			// Token: 0x04002ABC RID: 10940
			[FieldOffset(40)]
			internal double linearAttenuation;

			// Token: 0x04002ABD RID: 10941
			[FieldOffset(48)]
			internal double quadraticAttenuation;

			// Token: 0x04002ABE RID: 10942
			[FieldOffset(56)]
			internal MilPoint3F position;

			// Token: 0x04002ABF RID: 10943
			[FieldOffset(68)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002AC0 RID: 10944
			[FieldOffset(72)]
			internal DUCE.ResourceHandle hColorAnimations;

			// Token: 0x04002AC1 RID: 10945
			[FieldOffset(76)]
			internal DUCE.ResourceHandle hPositionAnimations;

			// Token: 0x04002AC2 RID: 10946
			[FieldOffset(80)]
			internal DUCE.ResourceHandle hRangeAnimations;

			// Token: 0x04002AC3 RID: 10947
			[FieldOffset(84)]
			internal DUCE.ResourceHandle hConstantAttenuationAnimations;

			// Token: 0x04002AC4 RID: 10948
			[FieldOffset(88)]
			internal DUCE.ResourceHandle hLinearAttenuationAnimations;

			// Token: 0x04002AC5 RID: 10949
			[FieldOffset(92)]
			internal DUCE.ResourceHandle hQuadraticAttenuationAnimations;
		}

		// Token: 0x0200092E RID: 2350
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SPOTLIGHT
		{
			// Token: 0x04002AC6 RID: 10950
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AC7 RID: 10951
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AC8 RID: 10952
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AC9 RID: 10953
			[FieldOffset(24)]
			internal double range;

			// Token: 0x04002ACA RID: 10954
			[FieldOffset(32)]
			internal double constantAttenuation;

			// Token: 0x04002ACB RID: 10955
			[FieldOffset(40)]
			internal double linearAttenuation;

			// Token: 0x04002ACC RID: 10956
			[FieldOffset(48)]
			internal double quadraticAttenuation;

			// Token: 0x04002ACD RID: 10957
			[FieldOffset(56)]
			internal double outerConeAngle;

			// Token: 0x04002ACE RID: 10958
			[FieldOffset(64)]
			internal double innerConeAngle;

			// Token: 0x04002ACF RID: 10959
			[FieldOffset(72)]
			internal MilPoint3F position;

			// Token: 0x04002AD0 RID: 10960
			[FieldOffset(84)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002AD1 RID: 10961
			[FieldOffset(88)]
			internal MilPoint3F direction;

			// Token: 0x04002AD2 RID: 10962
			[FieldOffset(100)]
			internal DUCE.ResourceHandle hColorAnimations;

			// Token: 0x04002AD3 RID: 10963
			[FieldOffset(104)]
			internal DUCE.ResourceHandle hPositionAnimations;

			// Token: 0x04002AD4 RID: 10964
			[FieldOffset(108)]
			internal DUCE.ResourceHandle hRangeAnimations;

			// Token: 0x04002AD5 RID: 10965
			[FieldOffset(112)]
			internal DUCE.ResourceHandle hConstantAttenuationAnimations;

			// Token: 0x04002AD6 RID: 10966
			[FieldOffset(116)]
			internal DUCE.ResourceHandle hLinearAttenuationAnimations;

			// Token: 0x04002AD7 RID: 10967
			[FieldOffset(120)]
			internal DUCE.ResourceHandle hQuadraticAttenuationAnimations;

			// Token: 0x04002AD8 RID: 10968
			[FieldOffset(124)]
			internal DUCE.ResourceHandle hDirectionAnimations;

			// Token: 0x04002AD9 RID: 10969
			[FieldOffset(128)]
			internal DUCE.ResourceHandle hOuterConeAngleAnimations;

			// Token: 0x04002ADA RID: 10970
			[FieldOffset(132)]
			internal DUCE.ResourceHandle hInnerConeAngleAnimations;
		}

		// Token: 0x0200092F RID: 2351
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GEOMETRYMODEL3D
		{
			// Token: 0x04002ADB RID: 10971
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002ADC RID: 10972
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002ADD RID: 10973
			[FieldOffset(8)]
			internal DUCE.ResourceHandle htransform;

			// Token: 0x04002ADE RID: 10974
			[FieldOffset(12)]
			internal DUCE.ResourceHandle hgeometry;

			// Token: 0x04002ADF RID: 10975
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hmaterial;

			// Token: 0x04002AE0 RID: 10976
			[FieldOffset(20)]
			internal DUCE.ResourceHandle hbackMaterial;
		}

		// Token: 0x02000930 RID: 2352
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MESHGEOMETRY3D
		{
			// Token: 0x04002AE1 RID: 10977
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AE2 RID: 10978
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AE3 RID: 10979
			[FieldOffset(8)]
			internal uint PositionsSize;

			// Token: 0x04002AE4 RID: 10980
			[FieldOffset(12)]
			internal uint NormalsSize;

			// Token: 0x04002AE5 RID: 10981
			[FieldOffset(16)]
			internal uint TextureCoordinatesSize;

			// Token: 0x04002AE6 RID: 10982
			[FieldOffset(20)]
			internal uint TriangleIndicesSize;
		}

		// Token: 0x02000931 RID: 2353
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MATERIALGROUP
		{
			// Token: 0x04002AE7 RID: 10983
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AE8 RID: 10984
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AE9 RID: 10985
			[FieldOffset(8)]
			internal uint ChildrenSize;
		}

		// Token: 0x02000932 RID: 2354
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DIFFUSEMATERIAL
		{
			// Token: 0x04002AEA RID: 10986
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AEB RID: 10987
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AEC RID: 10988
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AED RID: 10989
			[FieldOffset(24)]
			internal MilColorF ambientColor;

			// Token: 0x04002AEE RID: 10990
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hbrush;
		}

		// Token: 0x02000933 RID: 2355
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SPECULARMATERIAL
		{
			// Token: 0x04002AEF RID: 10991
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AF0 RID: 10992
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AF1 RID: 10993
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AF2 RID: 10994
			[FieldOffset(24)]
			internal double specularPower;

			// Token: 0x04002AF3 RID: 10995
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hbrush;
		}

		// Token: 0x02000934 RID: 2356
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_EMISSIVEMATERIAL
		{
			// Token: 0x04002AF4 RID: 10996
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AF5 RID: 10997
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AF6 RID: 10998
			[FieldOffset(8)]
			internal MilColorF color;

			// Token: 0x04002AF7 RID: 10999
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hbrush;
		}

		// Token: 0x02000935 RID: 2357
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TRANSFORM3DGROUP
		{
			// Token: 0x04002AF8 RID: 11000
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AF9 RID: 11001
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AFA RID: 11002
			[FieldOffset(8)]
			internal uint ChildrenSize;
		}

		// Token: 0x02000936 RID: 2358
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TRANSLATETRANSFORM3D
		{
			// Token: 0x04002AFB RID: 11003
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002AFC RID: 11004
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002AFD RID: 11005
			[FieldOffset(8)]
			internal double offsetX;

			// Token: 0x04002AFE RID: 11006
			[FieldOffset(16)]
			internal double offsetY;

			// Token: 0x04002AFF RID: 11007
			[FieldOffset(24)]
			internal double offsetZ;

			// Token: 0x04002B00 RID: 11008
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hOffsetXAnimations;

			// Token: 0x04002B01 RID: 11009
			[FieldOffset(36)]
			internal DUCE.ResourceHandle hOffsetYAnimations;

			// Token: 0x04002B02 RID: 11010
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hOffsetZAnimations;
		}

		// Token: 0x02000937 RID: 2359
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SCALETRANSFORM3D
		{
			// Token: 0x04002B03 RID: 11011
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B04 RID: 11012
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B05 RID: 11013
			[FieldOffset(8)]
			internal double scaleX;

			// Token: 0x04002B06 RID: 11014
			[FieldOffset(16)]
			internal double scaleY;

			// Token: 0x04002B07 RID: 11015
			[FieldOffset(24)]
			internal double scaleZ;

			// Token: 0x04002B08 RID: 11016
			[FieldOffset(32)]
			internal double centerX;

			// Token: 0x04002B09 RID: 11017
			[FieldOffset(40)]
			internal double centerY;

			// Token: 0x04002B0A RID: 11018
			[FieldOffset(48)]
			internal double centerZ;

			// Token: 0x04002B0B RID: 11019
			[FieldOffset(56)]
			internal DUCE.ResourceHandle hScaleXAnimations;

			// Token: 0x04002B0C RID: 11020
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hScaleYAnimations;

			// Token: 0x04002B0D RID: 11021
			[FieldOffset(64)]
			internal DUCE.ResourceHandle hScaleZAnimations;

			// Token: 0x04002B0E RID: 11022
			[FieldOffset(68)]
			internal DUCE.ResourceHandle hCenterXAnimations;

			// Token: 0x04002B0F RID: 11023
			[FieldOffset(72)]
			internal DUCE.ResourceHandle hCenterYAnimations;

			// Token: 0x04002B10 RID: 11024
			[FieldOffset(76)]
			internal DUCE.ResourceHandle hCenterZAnimations;
		}

		// Token: 0x02000938 RID: 2360
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_ROTATETRANSFORM3D
		{
			// Token: 0x04002B11 RID: 11025
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B12 RID: 11026
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B13 RID: 11027
			[FieldOffset(8)]
			internal double centerX;

			// Token: 0x04002B14 RID: 11028
			[FieldOffset(16)]
			internal double centerY;

			// Token: 0x04002B15 RID: 11029
			[FieldOffset(24)]
			internal double centerZ;

			// Token: 0x04002B16 RID: 11030
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hCenterXAnimations;

			// Token: 0x04002B17 RID: 11031
			[FieldOffset(36)]
			internal DUCE.ResourceHandle hCenterYAnimations;

			// Token: 0x04002B18 RID: 11032
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hCenterZAnimations;

			// Token: 0x04002B19 RID: 11033
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hrotation;
		}

		// Token: 0x02000939 RID: 2361
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MATRIXTRANSFORM3D
		{
			// Token: 0x04002B1A RID: 11034
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B1B RID: 11035
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B1C RID: 11036
			[FieldOffset(8)]
			internal D3DMATRIX matrix;
		}

		// Token: 0x0200093A RID: 2362
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PIXELSHADER
		{
			// Token: 0x04002B1D RID: 11037
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B1E RID: 11038
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B1F RID: 11039
			[FieldOffset(8)]
			internal ShaderRenderMode ShaderRenderMode;

			// Token: 0x04002B20 RID: 11040
			[FieldOffset(12)]
			internal uint PixelShaderBytecodeSize;

			// Token: 0x04002B21 RID: 11041
			[FieldOffset(16)]
			internal uint CompileSoftwareShader;
		}

		// Token: 0x0200093B RID: 2363
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_IMPLICITINPUTBRUSH
		{
			// Token: 0x04002B22 RID: 11042
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B23 RID: 11043
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B24 RID: 11044
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002B25 RID: 11045
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002B26 RID: 11046
			[FieldOffset(20)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B27 RID: 11047
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hRelativeTransform;
		}

		// Token: 0x0200093C RID: 2364
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_BLUREFFECT
		{
			// Token: 0x04002B28 RID: 11048
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B29 RID: 11049
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B2A RID: 11050
			[FieldOffset(8)]
			internal double Radius;

			// Token: 0x04002B2B RID: 11051
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hRadiusAnimations;

			// Token: 0x04002B2C RID: 11052
			[FieldOffset(20)]
			internal KernelType KernelType;

			// Token: 0x04002B2D RID: 11053
			[FieldOffset(24)]
			internal RenderingBias RenderingBias;
		}

		// Token: 0x0200093D RID: 2365
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DROPSHADOWEFFECT
		{
			// Token: 0x04002B2E RID: 11054
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B2F RID: 11055
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B30 RID: 11056
			[FieldOffset(8)]
			internal double ShadowDepth;

			// Token: 0x04002B31 RID: 11057
			[FieldOffset(16)]
			internal MilColorF Color;

			// Token: 0x04002B32 RID: 11058
			[FieldOffset(32)]
			internal double Direction;

			// Token: 0x04002B33 RID: 11059
			[FieldOffset(40)]
			internal double Opacity;

			// Token: 0x04002B34 RID: 11060
			[FieldOffset(48)]
			internal double BlurRadius;

			// Token: 0x04002B35 RID: 11061
			[FieldOffset(56)]
			internal DUCE.ResourceHandle hShadowDepthAnimations;

			// Token: 0x04002B36 RID: 11062
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hColorAnimations;

			// Token: 0x04002B37 RID: 11063
			[FieldOffset(64)]
			internal DUCE.ResourceHandle hDirectionAnimations;

			// Token: 0x04002B38 RID: 11064
			[FieldOffset(68)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002B39 RID: 11065
			[FieldOffset(72)]
			internal DUCE.ResourceHandle hBlurRadiusAnimations;

			// Token: 0x04002B3A RID: 11066
			[FieldOffset(76)]
			internal RenderingBias RenderingBias;
		}

		// Token: 0x0200093E RID: 2366
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SHADEREFFECT
		{
			// Token: 0x04002B3B RID: 11067
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B3C RID: 11068
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B3D RID: 11069
			[FieldOffset(8)]
			internal double TopPadding;

			// Token: 0x04002B3E RID: 11070
			[FieldOffset(16)]
			internal double BottomPadding;

			// Token: 0x04002B3F RID: 11071
			[FieldOffset(24)]
			internal double LeftPadding;

			// Token: 0x04002B40 RID: 11072
			[FieldOffset(32)]
			internal double RightPadding;

			// Token: 0x04002B41 RID: 11073
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hPixelShader;

			// Token: 0x04002B42 RID: 11074
			[FieldOffset(44)]
			internal int DdxUvDdyUvRegisterIndex;

			// Token: 0x04002B43 RID: 11075
			[FieldOffset(48)]
			internal uint ShaderConstantFloatRegistersSize;

			// Token: 0x04002B44 RID: 11076
			[FieldOffset(52)]
			internal uint DependencyPropertyFloatValuesSize;

			// Token: 0x04002B45 RID: 11077
			[FieldOffset(56)]
			internal uint ShaderConstantIntRegistersSize;

			// Token: 0x04002B46 RID: 11078
			[FieldOffset(60)]
			internal uint DependencyPropertyIntValuesSize;

			// Token: 0x04002B47 RID: 11079
			[FieldOffset(64)]
			internal uint ShaderConstantBoolRegistersSize;

			// Token: 0x04002B48 RID: 11080
			[FieldOffset(68)]
			internal uint DependencyPropertyBoolValuesSize;

			// Token: 0x04002B49 RID: 11081
			[FieldOffset(72)]
			internal uint ShaderSamplerRegistrationInfoSize;

			// Token: 0x04002B4A RID: 11082
			[FieldOffset(76)]
			internal uint DependencyPropertySamplerValuesSize;
		}

		// Token: 0x0200093F RID: 2367
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DRAWINGIMAGE
		{
			// Token: 0x04002B4B RID: 11083
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B4C RID: 11084
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B4D RID: 11085
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hDrawing;
		}

		// Token: 0x02000940 RID: 2368
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TRANSFORMGROUP
		{
			// Token: 0x04002B4E RID: 11086
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B4F RID: 11087
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B50 RID: 11088
			[FieldOffset(8)]
			internal uint ChildrenSize;
		}

		// Token: 0x02000941 RID: 2369
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_TRANSLATETRANSFORM
		{
			// Token: 0x04002B51 RID: 11089
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B52 RID: 11090
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B53 RID: 11091
			[FieldOffset(8)]
			internal double X;

			// Token: 0x04002B54 RID: 11092
			[FieldOffset(16)]
			internal double Y;

			// Token: 0x04002B55 RID: 11093
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hXAnimations;

			// Token: 0x04002B56 RID: 11094
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hYAnimations;
		}

		// Token: 0x02000942 RID: 2370
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SCALETRANSFORM
		{
			// Token: 0x04002B57 RID: 11095
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B58 RID: 11096
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B59 RID: 11097
			[FieldOffset(8)]
			internal double ScaleX;

			// Token: 0x04002B5A RID: 11098
			[FieldOffset(16)]
			internal double ScaleY;

			// Token: 0x04002B5B RID: 11099
			[FieldOffset(24)]
			internal double CenterX;

			// Token: 0x04002B5C RID: 11100
			[FieldOffset(32)]
			internal double CenterY;

			// Token: 0x04002B5D RID: 11101
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hScaleXAnimations;

			// Token: 0x04002B5E RID: 11102
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hScaleYAnimations;

			// Token: 0x04002B5F RID: 11103
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hCenterXAnimations;

			// Token: 0x04002B60 RID: 11104
			[FieldOffset(52)]
			internal DUCE.ResourceHandle hCenterYAnimations;
		}

		// Token: 0x02000943 RID: 2371
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SKEWTRANSFORM
		{
			// Token: 0x04002B61 RID: 11105
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B62 RID: 11106
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B63 RID: 11107
			[FieldOffset(8)]
			internal double AngleX;

			// Token: 0x04002B64 RID: 11108
			[FieldOffset(16)]
			internal double AngleY;

			// Token: 0x04002B65 RID: 11109
			[FieldOffset(24)]
			internal double CenterX;

			// Token: 0x04002B66 RID: 11110
			[FieldOffset(32)]
			internal double CenterY;

			// Token: 0x04002B67 RID: 11111
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hAngleXAnimations;

			// Token: 0x04002B68 RID: 11112
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hAngleYAnimations;

			// Token: 0x04002B69 RID: 11113
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hCenterXAnimations;

			// Token: 0x04002B6A RID: 11114
			[FieldOffset(52)]
			internal DUCE.ResourceHandle hCenterYAnimations;
		}

		// Token: 0x02000944 RID: 2372
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_ROTATETRANSFORM
		{
			// Token: 0x04002B6B RID: 11115
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B6C RID: 11116
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B6D RID: 11117
			[FieldOffset(8)]
			internal double Angle;

			// Token: 0x04002B6E RID: 11118
			[FieldOffset(16)]
			internal double CenterX;

			// Token: 0x04002B6F RID: 11119
			[FieldOffset(24)]
			internal double CenterY;

			// Token: 0x04002B70 RID: 11120
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hAngleAnimations;

			// Token: 0x04002B71 RID: 11121
			[FieldOffset(36)]
			internal DUCE.ResourceHandle hCenterXAnimations;

			// Token: 0x04002B72 RID: 11122
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hCenterYAnimations;
		}

		// Token: 0x02000945 RID: 2373
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_MATRIXTRANSFORM
		{
			// Token: 0x04002B73 RID: 11123
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B74 RID: 11124
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B75 RID: 11125
			[FieldOffset(8)]
			internal MilMatrix3x2D Matrix;

			// Token: 0x04002B76 RID: 11126
			[FieldOffset(56)]
			internal DUCE.ResourceHandle hMatrixAnimations;
		}

		// Token: 0x02000946 RID: 2374
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_LINEGEOMETRY
		{
			// Token: 0x04002B77 RID: 11127
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B78 RID: 11128
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B79 RID: 11129
			[FieldOffset(8)]
			internal Point StartPoint;

			// Token: 0x04002B7A RID: 11130
			[FieldOffset(24)]
			internal Point EndPoint;

			// Token: 0x04002B7B RID: 11131
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B7C RID: 11132
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hStartPointAnimations;

			// Token: 0x04002B7D RID: 11133
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hEndPointAnimations;
		}

		// Token: 0x02000947 RID: 2375
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_RECTANGLEGEOMETRY
		{
			// Token: 0x04002B7E RID: 11134
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B7F RID: 11135
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B80 RID: 11136
			[FieldOffset(8)]
			internal double RadiusX;

			// Token: 0x04002B81 RID: 11137
			[FieldOffset(16)]
			internal double RadiusY;

			// Token: 0x04002B82 RID: 11138
			[FieldOffset(24)]
			internal Rect Rect;

			// Token: 0x04002B83 RID: 11139
			[FieldOffset(56)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B84 RID: 11140
			[FieldOffset(60)]
			internal DUCE.ResourceHandle hRadiusXAnimations;

			// Token: 0x04002B85 RID: 11141
			[FieldOffset(64)]
			internal DUCE.ResourceHandle hRadiusYAnimations;

			// Token: 0x04002B86 RID: 11142
			[FieldOffset(68)]
			internal DUCE.ResourceHandle hRectAnimations;
		}

		// Token: 0x02000948 RID: 2376
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_ELLIPSEGEOMETRY
		{
			// Token: 0x04002B87 RID: 11143
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B88 RID: 11144
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B89 RID: 11145
			[FieldOffset(8)]
			internal double RadiusX;

			// Token: 0x04002B8A RID: 11146
			[FieldOffset(16)]
			internal double RadiusY;

			// Token: 0x04002B8B RID: 11147
			[FieldOffset(24)]
			internal Point Center;

			// Token: 0x04002B8C RID: 11148
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B8D RID: 11149
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hRadiusXAnimations;

			// Token: 0x04002B8E RID: 11150
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hRadiusYAnimations;

			// Token: 0x04002B8F RID: 11151
			[FieldOffset(52)]
			internal DUCE.ResourceHandle hCenterAnimations;
		}

		// Token: 0x02000949 RID: 2377
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GEOMETRYGROUP
		{
			// Token: 0x04002B90 RID: 11152
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B91 RID: 11153
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B92 RID: 11154
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B93 RID: 11155
			[FieldOffset(12)]
			internal FillRule FillRule;

			// Token: 0x04002B94 RID: 11156
			[FieldOffset(16)]
			internal uint ChildrenSize;
		}

		// Token: 0x0200094A RID: 2378
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_COMBINEDGEOMETRY
		{
			// Token: 0x04002B95 RID: 11157
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B96 RID: 11158
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B97 RID: 11159
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B98 RID: 11160
			[FieldOffset(12)]
			internal GeometryCombineMode GeometryCombineMode;

			// Token: 0x04002B99 RID: 11161
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hGeometry1;

			// Token: 0x04002B9A RID: 11162
			[FieldOffset(20)]
			internal DUCE.ResourceHandle hGeometry2;
		}

		// Token: 0x0200094B RID: 2379
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PATHGEOMETRY
		{
			// Token: 0x04002B9B RID: 11163
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002B9C RID: 11164
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002B9D RID: 11165
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002B9E RID: 11166
			[FieldOffset(12)]
			internal FillRule FillRule;

			// Token: 0x04002B9F RID: 11167
			[FieldOffset(16)]
			internal uint FiguresSize;
		}

		// Token: 0x0200094C RID: 2380
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_SOLIDCOLORBRUSH
		{
			// Token: 0x04002BA0 RID: 11168
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BA1 RID: 11169
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BA2 RID: 11170
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BA3 RID: 11171
			[FieldOffset(16)]
			internal MilColorF Color;

			// Token: 0x04002BA4 RID: 11172
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BA5 RID: 11173
			[FieldOffset(36)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BA6 RID: 11174
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BA7 RID: 11175
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hColorAnimations;
		}

		// Token: 0x0200094D RID: 2381
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_LINEARGRADIENTBRUSH
		{
			// Token: 0x04002BA8 RID: 11176
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BA9 RID: 11177
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BAA RID: 11178
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BAB RID: 11179
			[FieldOffset(16)]
			internal Point StartPoint;

			// Token: 0x04002BAC RID: 11180
			[FieldOffset(32)]
			internal Point EndPoint;

			// Token: 0x04002BAD RID: 11181
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BAE RID: 11182
			[FieldOffset(52)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BAF RID: 11183
			[FieldOffset(56)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BB0 RID: 11184
			[FieldOffset(60)]
			internal ColorInterpolationMode ColorInterpolationMode;

			// Token: 0x04002BB1 RID: 11185
			[FieldOffset(64)]
			internal BrushMappingMode MappingMode;

			// Token: 0x04002BB2 RID: 11186
			[FieldOffset(68)]
			internal GradientSpreadMethod SpreadMethod;

			// Token: 0x04002BB3 RID: 11187
			[FieldOffset(72)]
			internal uint GradientStopsSize;

			// Token: 0x04002BB4 RID: 11188
			[FieldOffset(76)]
			internal DUCE.ResourceHandle hStartPointAnimations;

			// Token: 0x04002BB5 RID: 11189
			[FieldOffset(80)]
			internal DUCE.ResourceHandle hEndPointAnimations;
		}

		// Token: 0x0200094E RID: 2382
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_RADIALGRADIENTBRUSH
		{
			// Token: 0x04002BB6 RID: 11190
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BB7 RID: 11191
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BB8 RID: 11192
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BB9 RID: 11193
			[FieldOffset(16)]
			internal Point Center;

			// Token: 0x04002BBA RID: 11194
			[FieldOffset(32)]
			internal double RadiusX;

			// Token: 0x04002BBB RID: 11195
			[FieldOffset(40)]
			internal double RadiusY;

			// Token: 0x04002BBC RID: 11196
			[FieldOffset(48)]
			internal Point GradientOrigin;

			// Token: 0x04002BBD RID: 11197
			[FieldOffset(64)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BBE RID: 11198
			[FieldOffset(68)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BBF RID: 11199
			[FieldOffset(72)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BC0 RID: 11200
			[FieldOffset(76)]
			internal ColorInterpolationMode ColorInterpolationMode;

			// Token: 0x04002BC1 RID: 11201
			[FieldOffset(80)]
			internal BrushMappingMode MappingMode;

			// Token: 0x04002BC2 RID: 11202
			[FieldOffset(84)]
			internal GradientSpreadMethod SpreadMethod;

			// Token: 0x04002BC3 RID: 11203
			[FieldOffset(88)]
			internal uint GradientStopsSize;

			// Token: 0x04002BC4 RID: 11204
			[FieldOffset(92)]
			internal DUCE.ResourceHandle hCenterAnimations;

			// Token: 0x04002BC5 RID: 11205
			[FieldOffset(96)]
			internal DUCE.ResourceHandle hRadiusXAnimations;

			// Token: 0x04002BC6 RID: 11206
			[FieldOffset(100)]
			internal DUCE.ResourceHandle hRadiusYAnimations;

			// Token: 0x04002BC7 RID: 11207
			[FieldOffset(104)]
			internal DUCE.ResourceHandle hGradientOriginAnimations;
		}

		// Token: 0x0200094F RID: 2383
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_IMAGEBRUSH
		{
			// Token: 0x04002BC8 RID: 11208
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BC9 RID: 11209
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BCA RID: 11210
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BCB RID: 11211
			[FieldOffset(16)]
			internal Rect Viewport;

			// Token: 0x04002BCC RID: 11212
			[FieldOffset(48)]
			internal Rect Viewbox;

			// Token: 0x04002BCD RID: 11213
			[FieldOffset(80)]
			internal double CacheInvalidationThresholdMinimum;

			// Token: 0x04002BCE RID: 11214
			[FieldOffset(88)]
			internal double CacheInvalidationThresholdMaximum;

			// Token: 0x04002BCF RID: 11215
			[FieldOffset(96)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BD0 RID: 11216
			[FieldOffset(100)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BD1 RID: 11217
			[FieldOffset(104)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BD2 RID: 11218
			[FieldOffset(108)]
			internal BrushMappingMode ViewportUnits;

			// Token: 0x04002BD3 RID: 11219
			[FieldOffset(112)]
			internal BrushMappingMode ViewboxUnits;

			// Token: 0x04002BD4 RID: 11220
			[FieldOffset(116)]
			internal DUCE.ResourceHandle hViewportAnimations;

			// Token: 0x04002BD5 RID: 11221
			[FieldOffset(120)]
			internal DUCE.ResourceHandle hViewboxAnimations;

			// Token: 0x04002BD6 RID: 11222
			[FieldOffset(124)]
			internal Stretch Stretch;

			// Token: 0x04002BD7 RID: 11223
			[FieldOffset(128)]
			internal TileMode TileMode;

			// Token: 0x04002BD8 RID: 11224
			[FieldOffset(132)]
			internal AlignmentX AlignmentX;

			// Token: 0x04002BD9 RID: 11225
			[FieldOffset(136)]
			internal AlignmentY AlignmentY;

			// Token: 0x04002BDA RID: 11226
			[FieldOffset(140)]
			internal CachingHint CachingHint;

			// Token: 0x04002BDB RID: 11227
			[FieldOffset(144)]
			internal DUCE.ResourceHandle hImageSource;
		}

		// Token: 0x02000950 RID: 2384
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DRAWINGBRUSH
		{
			// Token: 0x04002BDC RID: 11228
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BDD RID: 11229
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BDE RID: 11230
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BDF RID: 11231
			[FieldOffset(16)]
			internal Rect Viewport;

			// Token: 0x04002BE0 RID: 11232
			[FieldOffset(48)]
			internal Rect Viewbox;

			// Token: 0x04002BE1 RID: 11233
			[FieldOffset(80)]
			internal double CacheInvalidationThresholdMinimum;

			// Token: 0x04002BE2 RID: 11234
			[FieldOffset(88)]
			internal double CacheInvalidationThresholdMaximum;

			// Token: 0x04002BE3 RID: 11235
			[FieldOffset(96)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BE4 RID: 11236
			[FieldOffset(100)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BE5 RID: 11237
			[FieldOffset(104)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BE6 RID: 11238
			[FieldOffset(108)]
			internal BrushMappingMode ViewportUnits;

			// Token: 0x04002BE7 RID: 11239
			[FieldOffset(112)]
			internal BrushMappingMode ViewboxUnits;

			// Token: 0x04002BE8 RID: 11240
			[FieldOffset(116)]
			internal DUCE.ResourceHandle hViewportAnimations;

			// Token: 0x04002BE9 RID: 11241
			[FieldOffset(120)]
			internal DUCE.ResourceHandle hViewboxAnimations;

			// Token: 0x04002BEA RID: 11242
			[FieldOffset(124)]
			internal Stretch Stretch;

			// Token: 0x04002BEB RID: 11243
			[FieldOffset(128)]
			internal TileMode TileMode;

			// Token: 0x04002BEC RID: 11244
			[FieldOffset(132)]
			internal AlignmentX AlignmentX;

			// Token: 0x04002BED RID: 11245
			[FieldOffset(136)]
			internal AlignmentY AlignmentY;

			// Token: 0x04002BEE RID: 11246
			[FieldOffset(140)]
			internal CachingHint CachingHint;

			// Token: 0x04002BEF RID: 11247
			[FieldOffset(144)]
			internal DUCE.ResourceHandle hDrawing;
		}

		// Token: 0x02000951 RID: 2385
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VISUALBRUSH
		{
			// Token: 0x04002BF0 RID: 11248
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002BF1 RID: 11249
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002BF2 RID: 11250
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002BF3 RID: 11251
			[FieldOffset(16)]
			internal Rect Viewport;

			// Token: 0x04002BF4 RID: 11252
			[FieldOffset(48)]
			internal Rect Viewbox;

			// Token: 0x04002BF5 RID: 11253
			[FieldOffset(80)]
			internal double CacheInvalidationThresholdMinimum;

			// Token: 0x04002BF6 RID: 11254
			[FieldOffset(88)]
			internal double CacheInvalidationThresholdMaximum;

			// Token: 0x04002BF7 RID: 11255
			[FieldOffset(96)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002BF8 RID: 11256
			[FieldOffset(100)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002BF9 RID: 11257
			[FieldOffset(104)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002BFA RID: 11258
			[FieldOffset(108)]
			internal BrushMappingMode ViewportUnits;

			// Token: 0x04002BFB RID: 11259
			[FieldOffset(112)]
			internal BrushMappingMode ViewboxUnits;

			// Token: 0x04002BFC RID: 11260
			[FieldOffset(116)]
			internal DUCE.ResourceHandle hViewportAnimations;

			// Token: 0x04002BFD RID: 11261
			[FieldOffset(120)]
			internal DUCE.ResourceHandle hViewboxAnimations;

			// Token: 0x04002BFE RID: 11262
			[FieldOffset(124)]
			internal Stretch Stretch;

			// Token: 0x04002BFF RID: 11263
			[FieldOffset(128)]
			internal TileMode TileMode;

			// Token: 0x04002C00 RID: 11264
			[FieldOffset(132)]
			internal AlignmentX AlignmentX;

			// Token: 0x04002C01 RID: 11265
			[FieldOffset(136)]
			internal AlignmentY AlignmentY;

			// Token: 0x04002C02 RID: 11266
			[FieldOffset(140)]
			internal CachingHint CachingHint;

			// Token: 0x04002C03 RID: 11267
			[FieldOffset(144)]
			internal DUCE.ResourceHandle hVisual;
		}

		// Token: 0x02000952 RID: 2386
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_BITMAPCACHEBRUSH
		{
			// Token: 0x04002C04 RID: 11268
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C05 RID: 11269
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C06 RID: 11270
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002C07 RID: 11271
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002C08 RID: 11272
			[FieldOffset(20)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002C09 RID: 11273
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hRelativeTransform;

			// Token: 0x04002C0A RID: 11274
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hBitmapCache;

			// Token: 0x04002C0B RID: 11275
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hInternalTarget;
		}

		// Token: 0x02000953 RID: 2387
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DASHSTYLE
		{
			// Token: 0x04002C0C RID: 11276
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C0D RID: 11277
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C0E RID: 11278
			[FieldOffset(8)]
			internal double Offset;

			// Token: 0x04002C0F RID: 11279
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hOffsetAnimations;

			// Token: 0x04002C10 RID: 11280
			[FieldOffset(20)]
			internal uint DashesSize;
		}

		// Token: 0x02000954 RID: 2388
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_PEN
		{
			// Token: 0x04002C11 RID: 11281
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C12 RID: 11282
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C13 RID: 11283
			[FieldOffset(8)]
			internal double Thickness;

			// Token: 0x04002C14 RID: 11284
			[FieldOffset(16)]
			internal double MiterLimit;

			// Token: 0x04002C15 RID: 11285
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hBrush;

			// Token: 0x04002C16 RID: 11286
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hThicknessAnimations;

			// Token: 0x04002C17 RID: 11287
			[FieldOffset(32)]
			internal PenLineCap StartLineCap;

			// Token: 0x04002C18 RID: 11288
			[FieldOffset(36)]
			internal PenLineCap EndLineCap;

			// Token: 0x04002C19 RID: 11289
			[FieldOffset(40)]
			internal PenLineCap DashCap;

			// Token: 0x04002C1A RID: 11290
			[FieldOffset(44)]
			internal PenLineJoin LineJoin;

			// Token: 0x04002C1B RID: 11291
			[FieldOffset(48)]
			internal DUCE.ResourceHandle hDashStyle;
		}

		// Token: 0x02000955 RID: 2389
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GEOMETRYDRAWING
		{
			// Token: 0x04002C1C RID: 11292
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C1D RID: 11293
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C1E RID: 11294
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hBrush;

			// Token: 0x04002C1F RID: 11295
			[FieldOffset(12)]
			internal DUCE.ResourceHandle hPen;

			// Token: 0x04002C20 RID: 11296
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hGeometry;
		}

		// Token: 0x02000956 RID: 2390
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GLYPHRUNDRAWING
		{
			// Token: 0x04002C21 RID: 11297
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C22 RID: 11298
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C23 RID: 11299
			[FieldOffset(8)]
			internal DUCE.ResourceHandle hGlyphRun;

			// Token: 0x04002C24 RID: 11300
			[FieldOffset(12)]
			internal DUCE.ResourceHandle hForegroundBrush;
		}

		// Token: 0x02000957 RID: 2391
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_IMAGEDRAWING
		{
			// Token: 0x04002C25 RID: 11301
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C26 RID: 11302
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C27 RID: 11303
			[FieldOffset(8)]
			internal Rect Rect;

			// Token: 0x04002C28 RID: 11304
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hImageSource;

			// Token: 0x04002C29 RID: 11305
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hRectAnimations;
		}

		// Token: 0x02000958 RID: 2392
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_VIDEODRAWING
		{
			// Token: 0x04002C2A RID: 11306
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C2B RID: 11307
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C2C RID: 11308
			[FieldOffset(8)]
			internal Rect Rect;

			// Token: 0x04002C2D RID: 11309
			[FieldOffset(40)]
			internal DUCE.ResourceHandle hPlayer;

			// Token: 0x04002C2E RID: 11310
			[FieldOffset(44)]
			internal DUCE.ResourceHandle hRectAnimations;
		}

		// Token: 0x02000959 RID: 2393
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_DRAWINGGROUP
		{
			// Token: 0x04002C2F RID: 11311
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C30 RID: 11312
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C31 RID: 11313
			[FieldOffset(8)]
			internal double Opacity;

			// Token: 0x04002C32 RID: 11314
			[FieldOffset(16)]
			internal uint ChildrenSize;

			// Token: 0x04002C33 RID: 11315
			[FieldOffset(20)]
			internal DUCE.ResourceHandle hClipGeometry;

			// Token: 0x04002C34 RID: 11316
			[FieldOffset(24)]
			internal DUCE.ResourceHandle hOpacityAnimations;

			// Token: 0x04002C35 RID: 11317
			[FieldOffset(28)]
			internal DUCE.ResourceHandle hOpacityMask;

			// Token: 0x04002C36 RID: 11318
			[FieldOffset(32)]
			internal DUCE.ResourceHandle hTransform;

			// Token: 0x04002C37 RID: 11319
			[FieldOffset(36)]
			internal DUCE.ResourceHandle hGuidelineSet;

			// Token: 0x04002C38 RID: 11320
			[FieldOffset(40)]
			internal EdgeMode EdgeMode;

			// Token: 0x04002C39 RID: 11321
			[FieldOffset(44)]
			internal BitmapScalingMode bitmapScalingMode;

			// Token: 0x04002C3A RID: 11322
			[FieldOffset(48)]
			internal ClearTypeHint ClearTypeHint;
		}

		// Token: 0x0200095A RID: 2394
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_GUIDELINESET
		{
			// Token: 0x04002C3B RID: 11323
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C3C RID: 11324
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C3D RID: 11325
			[FieldOffset(8)]
			internal uint GuidelinesXSize;

			// Token: 0x04002C3E RID: 11326
			[FieldOffset(12)]
			internal uint GuidelinesYSize;

			// Token: 0x04002C3F RID: 11327
			[FieldOffset(16)]
			internal uint IsDynamic;
		}

		// Token: 0x0200095B RID: 2395
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MILCMD_BITMAPCACHE
		{
			// Token: 0x04002C40 RID: 11328
			[FieldOffset(0)]
			internal MILCMD Type;

			// Token: 0x04002C41 RID: 11329
			[FieldOffset(4)]
			internal DUCE.ResourceHandle Handle;

			// Token: 0x04002C42 RID: 11330
			[FieldOffset(8)]
			internal double RenderAtScale;

			// Token: 0x04002C43 RID: 11331
			[FieldOffset(16)]
			internal DUCE.ResourceHandle hRenderAtScaleAnimations;

			// Token: 0x04002C44 RID: 11332
			[FieldOffset(20)]
			internal uint SnapsToDevicePixels;

			// Token: 0x04002C45 RID: 11333
			[FieldOffset(24)]
			internal uint EnableClearType;
		}

		// Token: 0x0200095C RID: 2396
		internal enum ResourceType
		{
			// Token: 0x04002C47 RID: 11335
			TYPE_NULL,
			// Token: 0x04002C48 RID: 11336
			TYPE_MEDIAPLAYER,
			// Token: 0x04002C49 RID: 11337
			TYPE_ROTATION3D,
			// Token: 0x04002C4A RID: 11338
			TYPE_AXISANGLEROTATION3D,
			// Token: 0x04002C4B RID: 11339
			TYPE_QUATERNIONROTATION3D,
			// Token: 0x04002C4C RID: 11340
			TYPE_CAMERA,
			// Token: 0x04002C4D RID: 11341
			TYPE_PROJECTIONCAMERA,
			// Token: 0x04002C4E RID: 11342
			TYPE_PERSPECTIVECAMERA,
			// Token: 0x04002C4F RID: 11343
			TYPE_ORTHOGRAPHICCAMERA,
			// Token: 0x04002C50 RID: 11344
			TYPE_MATRIXCAMERA,
			// Token: 0x04002C51 RID: 11345
			TYPE_MODEL3D,
			// Token: 0x04002C52 RID: 11346
			TYPE_MODEL3DGROUP,
			// Token: 0x04002C53 RID: 11347
			TYPE_LIGHT,
			// Token: 0x04002C54 RID: 11348
			TYPE_AMBIENTLIGHT,
			// Token: 0x04002C55 RID: 11349
			TYPE_DIRECTIONALLIGHT,
			// Token: 0x04002C56 RID: 11350
			TYPE_POINTLIGHTBASE,
			// Token: 0x04002C57 RID: 11351
			TYPE_POINTLIGHT,
			// Token: 0x04002C58 RID: 11352
			TYPE_SPOTLIGHT,
			// Token: 0x04002C59 RID: 11353
			TYPE_GEOMETRYMODEL3D,
			// Token: 0x04002C5A RID: 11354
			TYPE_GEOMETRY3D,
			// Token: 0x04002C5B RID: 11355
			TYPE_MESHGEOMETRY3D,
			// Token: 0x04002C5C RID: 11356
			TYPE_MATERIAL,
			// Token: 0x04002C5D RID: 11357
			TYPE_MATERIALGROUP,
			// Token: 0x04002C5E RID: 11358
			TYPE_DIFFUSEMATERIAL,
			// Token: 0x04002C5F RID: 11359
			TYPE_SPECULARMATERIAL,
			// Token: 0x04002C60 RID: 11360
			TYPE_EMISSIVEMATERIAL,
			// Token: 0x04002C61 RID: 11361
			TYPE_TRANSFORM3D,
			// Token: 0x04002C62 RID: 11362
			TYPE_TRANSFORM3DGROUP,
			// Token: 0x04002C63 RID: 11363
			TYPE_AFFINETRANSFORM3D,
			// Token: 0x04002C64 RID: 11364
			TYPE_TRANSLATETRANSFORM3D,
			// Token: 0x04002C65 RID: 11365
			TYPE_SCALETRANSFORM3D,
			// Token: 0x04002C66 RID: 11366
			TYPE_ROTATETRANSFORM3D,
			// Token: 0x04002C67 RID: 11367
			TYPE_MATRIXTRANSFORM3D,
			// Token: 0x04002C68 RID: 11368
			TYPE_PIXELSHADER,
			// Token: 0x04002C69 RID: 11369
			TYPE_IMPLICITINPUTBRUSH,
			// Token: 0x04002C6A RID: 11370
			TYPE_EFFECT,
			// Token: 0x04002C6B RID: 11371
			TYPE_BLUREFFECT,
			// Token: 0x04002C6C RID: 11372
			TYPE_DROPSHADOWEFFECT,
			// Token: 0x04002C6D RID: 11373
			TYPE_SHADEREFFECT,
			// Token: 0x04002C6E RID: 11374
			TYPE_VISUAL,
			// Token: 0x04002C6F RID: 11375
			TYPE_VIEWPORT3DVISUAL,
			// Token: 0x04002C70 RID: 11376
			TYPE_VISUAL3D,
			// Token: 0x04002C71 RID: 11377
			TYPE_GLYPHRUN,
			// Token: 0x04002C72 RID: 11378
			TYPE_RENDERDATA,
			// Token: 0x04002C73 RID: 11379
			TYPE_DRAWINGCONTEXT,
			// Token: 0x04002C74 RID: 11380
			TYPE_RENDERTARGET,
			// Token: 0x04002C75 RID: 11381
			TYPE_HWNDRENDERTARGET,
			// Token: 0x04002C76 RID: 11382
			TYPE_GENERICRENDERTARGET,
			// Token: 0x04002C77 RID: 11383
			TYPE_ETWEVENTRESOURCE,
			// Token: 0x04002C78 RID: 11384
			TYPE_DOUBLERESOURCE,
			// Token: 0x04002C79 RID: 11385
			TYPE_COLORRESOURCE,
			// Token: 0x04002C7A RID: 11386
			TYPE_POINTRESOURCE,
			// Token: 0x04002C7B RID: 11387
			TYPE_RECTRESOURCE,
			// Token: 0x04002C7C RID: 11388
			TYPE_SIZERESOURCE,
			// Token: 0x04002C7D RID: 11389
			TYPE_MATRIXRESOURCE,
			// Token: 0x04002C7E RID: 11390
			TYPE_POINT3DRESOURCE,
			// Token: 0x04002C7F RID: 11391
			TYPE_VECTOR3DRESOURCE,
			// Token: 0x04002C80 RID: 11392
			TYPE_QUATERNIONRESOURCE,
			// Token: 0x04002C81 RID: 11393
			TYPE_IMAGESOURCE,
			// Token: 0x04002C82 RID: 11394
			TYPE_DRAWINGIMAGE,
			// Token: 0x04002C83 RID: 11395
			TYPE_TRANSFORM,
			// Token: 0x04002C84 RID: 11396
			TYPE_TRANSFORMGROUP,
			// Token: 0x04002C85 RID: 11397
			TYPE_TRANSLATETRANSFORM,
			// Token: 0x04002C86 RID: 11398
			TYPE_SCALETRANSFORM,
			// Token: 0x04002C87 RID: 11399
			TYPE_SKEWTRANSFORM,
			// Token: 0x04002C88 RID: 11400
			TYPE_ROTATETRANSFORM,
			// Token: 0x04002C89 RID: 11401
			TYPE_MATRIXTRANSFORM,
			// Token: 0x04002C8A RID: 11402
			TYPE_GEOMETRY,
			// Token: 0x04002C8B RID: 11403
			TYPE_LINEGEOMETRY,
			// Token: 0x04002C8C RID: 11404
			TYPE_RECTANGLEGEOMETRY,
			// Token: 0x04002C8D RID: 11405
			TYPE_ELLIPSEGEOMETRY,
			// Token: 0x04002C8E RID: 11406
			TYPE_GEOMETRYGROUP,
			// Token: 0x04002C8F RID: 11407
			TYPE_COMBINEDGEOMETRY,
			// Token: 0x04002C90 RID: 11408
			TYPE_PATHGEOMETRY,
			// Token: 0x04002C91 RID: 11409
			TYPE_BRUSH,
			// Token: 0x04002C92 RID: 11410
			TYPE_SOLIDCOLORBRUSH,
			// Token: 0x04002C93 RID: 11411
			TYPE_GRADIENTBRUSH,
			// Token: 0x04002C94 RID: 11412
			TYPE_LINEARGRADIENTBRUSH,
			// Token: 0x04002C95 RID: 11413
			TYPE_RADIALGRADIENTBRUSH,
			// Token: 0x04002C96 RID: 11414
			TYPE_TILEBRUSH,
			// Token: 0x04002C97 RID: 11415
			TYPE_IMAGEBRUSH,
			// Token: 0x04002C98 RID: 11416
			TYPE_DRAWINGBRUSH,
			// Token: 0x04002C99 RID: 11417
			TYPE_VISUALBRUSH,
			// Token: 0x04002C9A RID: 11418
			TYPE_BITMAPCACHEBRUSH,
			// Token: 0x04002C9B RID: 11419
			TYPE_DASHSTYLE,
			// Token: 0x04002C9C RID: 11420
			TYPE_PEN,
			// Token: 0x04002C9D RID: 11421
			TYPE_DRAWING,
			// Token: 0x04002C9E RID: 11422
			TYPE_GEOMETRYDRAWING,
			// Token: 0x04002C9F RID: 11423
			TYPE_GLYPHRUNDRAWING,
			// Token: 0x04002CA0 RID: 11424
			TYPE_IMAGEDRAWING,
			// Token: 0x04002CA1 RID: 11425
			TYPE_VIDEODRAWING,
			// Token: 0x04002CA2 RID: 11426
			TYPE_DRAWINGGROUP,
			// Token: 0x04002CA3 RID: 11427
			TYPE_GUIDELINESET,
			// Token: 0x04002CA4 RID: 11428
			TYPE_CACHEMODE,
			// Token: 0x04002CA5 RID: 11429
			TYPE_BITMAPCACHE,
			// Token: 0x04002CA6 RID: 11430
			TYPE_BITMAPSOURCE,
			// Token: 0x04002CA7 RID: 11431
			TYPE_DOUBLEBUFFEREDBITMAP,
			// Token: 0x04002CA8 RID: 11432
			TYPE_D3DIMAGE
		}

		// Token: 0x0200095D RID: 2397
		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		internal struct MIL_GRADIENTSTOP
		{
			// Token: 0x04002CA9 RID: 11433
			[FieldOffset(0)]
			internal double Position;

			// Token: 0x04002CAA RID: 11434
			[FieldOffset(8)]
			internal MilColorF Color;
		}
	}
}
