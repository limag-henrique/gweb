using System;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007C6 RID: 1990
	internal class Compressor
	{
		// Token: 0x06005402 RID: 21506 RVA: 0x0015467C File Offset: 0x00153A7C
		internal static void DecompressPacketData(byte[] compressedInput, ref uint size, int[] decompressedPackets)
		{
			if (compressedInput == null || (ulong)size > (ulong)((long)compressedInput.Length) || decompressedPackets == null)
			{
				throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage(SR.Get("DecompressPacketDataFailed")));
			}
			size = Compressor.AlgoModule.DecompressPacketData(compressedInput, decompressedPackets);
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x001546BC File Offset: 0x00153ABC
		internal static byte[] DecompressPropertyData(byte[] input)
		{
			if (input == null)
			{
				throw new InvalidOperationException(StrokeCollectionSerializer.ISFDebugMessage(SR.Get("DecompressPropertyFailed")));
			}
			return Compressor.AlgoModule.DecompressPropertyData(input);
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x001546F0 File Offset: 0x00153AF0
		internal static byte[] CompressPropertyData(byte[] data, byte algorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return Compressor.AlgoModule.CompressPropertyData(data, algorithm);
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x00154718 File Offset: 0x00153B18
		internal static byte[] CompressPacketData(int[] input, ref byte algorithm)
		{
			if (input == null)
			{
				throw new InvalidOperationException(SR.Get("IsfOperationFailed"));
			}
			return Compressor.AlgoModule.CompressPacketData(input, algorithm);
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x00154748 File Offset: 0x00153B48
		private static AlgoModule AlgoModule
		{
			get
			{
				if (Compressor._algoModule == null)
				{
					Compressor._algoModule = new AlgoModule();
				}
				return Compressor._algoModule;
			}
		}

		// Token: 0x040025DB RID: 9691
		[ThreadStatic]
		private static AlgoModule _algoModule;
	}
}
