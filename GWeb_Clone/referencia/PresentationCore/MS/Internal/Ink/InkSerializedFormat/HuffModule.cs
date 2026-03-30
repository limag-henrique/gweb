using System;
using System.Collections.Generic;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007DF RID: 2015
	internal class HuffModule
	{
		// Token: 0x060054AC RID: 21676 RVA: 0x0015CC2C File Offset: 0x0015C02C
		internal HuffModule()
		{
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0015CC5C File Offset: 0x0015C05C
		internal HuffCodec GetDefCodec(uint index)
		{
			if ((uint)AlgoModule.DefaultBAACount > index)
			{
				HuffCodec huffCodec = this._defaultHuffCodecs[(int)index];
				if (huffCodec == null)
				{
					huffCodec = new HuffCodec(index);
					this._defaultHuffCodecs[(int)index] = huffCodec;
				}
				return huffCodec;
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0015CCA0 File Offset: 0x0015C0A0
		internal HuffCodec FindCodec(byte algoData)
		{
			byte b = algoData & 31;
			if (b < AlgoModule.DefaultBAACount)
			{
				return this.GetDefCodec((uint)b);
			}
			if ((int)b >= this._huffCodecs.Count + (int)AlgoModule.DefaultBAACount)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("invalid codec computed"));
			}
			return this._huffCodecs[(int)(b - AlgoModule.DefaultBAACount)];
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0015CCFC File Offset: 0x0015C0FC
		internal DataXform FindDtXf(byte algoData)
		{
			return this.DefaultDeltaDelta;
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x0015CD10 File Offset: 0x0015C110
		private DeltaDelta DefaultDeltaDelta
		{
			get
			{
				if (this._defaultDtxf == null)
				{
					this._defaultDtxf = new DeltaDelta();
				}
				return this._defaultDtxf;
			}
		}

		// Token: 0x04002630 RID: 9776
		private DeltaDelta _defaultDtxf;

		// Token: 0x04002631 RID: 9777
		private List<HuffCodec> _huffCodecs = new List<HuffCodec>();

		// Token: 0x04002632 RID: 9778
		private HuffCodec[] _defaultHuffCodecs = new HuffCodec[(int)AlgoModule.DefaultBAACount];
	}
}
