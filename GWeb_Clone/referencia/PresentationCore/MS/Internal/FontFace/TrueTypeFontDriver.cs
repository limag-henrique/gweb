using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;

namespace MS.Internal.FontFace
{
	// Token: 0x02000767 RID: 1895
	internal class TrueTypeFontDriver
	{
		// Token: 0x06004FF0 RID: 20464 RVA: 0x0013FEE0 File Offset: 0x0013F2E0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe static ushort ReadOpenTypeUShort(CheckedPointer pointer)
		{
			byte* ptr = (byte*)pointer.Probe(0, 2);
			return (ushort)(((int)(*ptr) << 8) + (int)ptr[1]);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0013FF04 File Offset: 0x0013F304
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe static int ReadOpenTypeLong(CheckedPointer pointer)
		{
			byte* ptr = (byte*)pointer.Probe(0, 4);
			return ((((int)(*ptr) << 8) + (int)ptr[1] << 8) + (int)ptr[2] << 8) + (int)ptr[3];
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0013FF34 File Offset: 0x0013F334
		[SecurityCritical]
		internal TrueTypeFontDriver(UnmanagedMemoryStream unmanagedMemoryStream, Uri sourceUri)
		{
			this._sourceUri = sourceUri;
			this._unmanagedMemoryStream = unmanagedMemoryStream;
			this._fileStream = new CheckedPointer(unmanagedMemoryStream);
			try
			{
				CheckedPointer checkedPointer = this._fileStream;
				TrueTypeFontDriver.TrueTypeTags trueTypeTags = (TrueTypeFontDriver.TrueTypeTags)TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
				checkedPointer += 4;
				if (trueTypeTags == TrueTypeFontDriver.TrueTypeTags.TTC_TTCF)
				{
					this._technology = FontTechnology.TrueTypeCollection;
					checkedPointer += 4;
					this._numFaces = TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
				}
				else if (trueTypeTags == TrueTypeFontDriver.TrueTypeTags.OTTO)
				{
					this._technology = FontTechnology.PostscriptOpenType;
					this._numFaces = 1;
				}
				else
				{
					this._technology = FontTechnology.TrueType;
					this._numFaces = 1;
				}
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new FileFormatException(this.SourceUri, innerException);
			}
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0013FFF0 File Offset: 0x0013F3F0
		internal void SetFace(int faceIndex)
		{
			if (this._technology == FontTechnology.TrueTypeCollection)
			{
				if (faceIndex < 0 || faceIndex >= this._numFaces)
				{
					throw new ArgumentOutOfRangeException("faceIndex");
				}
			}
			else if (faceIndex != 0)
			{
				throw new ArgumentOutOfRangeException("faceIndex", SR.Get("FaceIndexValidOnlyForTTC"));
			}
			try
			{
				CheckedPointer checkedPointer = this._fileStream + 4;
				if (this._technology == FontTechnology.TrueTypeCollection)
				{
					checkedPointer += 8 + 4 * faceIndex;
					this._directoryOffset = TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
					checkedPointer = this._fileStream + (this._directoryOffset + 4);
				}
				this._faceIndex = faceIndex;
				int num = (int)TrueTypeFontDriver.ReadOpenTypeUShort(checkedPointer);
				checkedPointer += 2;
				long num2 = (long)(12 + num * 20);
				if ((long)this._fileStream.Size < num2)
				{
					throw new FileFormatException(this.SourceUri);
				}
				this._tableDirectory = new TrueTypeFontDriver.DirectoryEntry[num];
				checkedPointer += 6;
				for (int i = 0; i < this._tableDirectory.Length; i++)
				{
					this._tableDirectory[i].tag = (TrueTypeFontDriver.TrueTypeTags)TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
					checkedPointer += 8;
					int offset = TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
					checkedPointer += 4;
					int length = TrueTypeFontDriver.ReadOpenTypeLong(checkedPointer);
					checkedPointer += 4;
					this._tableDirectory[i].pointer = this._fileStream.CheckedProbe(offset, length);
				}
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new FileFormatException(this.SourceUri, innerException);
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x0014016C File Offset: 0x0013F56C
		internal int NumFaces
		{
			get
			{
				return this._numFaces;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x00140180 File Offset: 0x0013F580
		private Uri SourceUri
		{
			get
			{
				return this._sourceUri;
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x00140194 File Offset: 0x0013F594
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe byte[] ComputeFontSubset(ICollection<ushort> glyphs)
		{
			SecurityHelper.DemandUnmanagedCode();
			int size = this._fileStream.Size;
			void* ptr = this._fileStream.Probe(0, size);
			if (this._technology == FontTechnology.PostscriptOpenType)
			{
				byte[] array = new byte[size];
				Marshal.Copy((IntPtr)ptr, array, 0, size);
				return array;
			}
			ushort[] array2;
			if (glyphs == null || glyphs.Count == 0)
			{
				array2 = null;
			}
			else
			{
				array2 = new ushort[glyphs.Count];
				glyphs.CopyTo(array2, 0);
			}
			return TrueTypeSubsetter.ComputeSubset(ptr, size, this.SourceUri, this._directoryOffset, array2);
		}

		// Token: 0x04002464 RID: 9316
		private CheckedPointer _fileStream;

		// Token: 0x04002465 RID: 9317
		private UnmanagedMemoryStream _unmanagedMemoryStream;

		// Token: 0x04002466 RID: 9318
		private Uri _sourceUri;

		// Token: 0x04002467 RID: 9319
		private int _numFaces;

		// Token: 0x04002468 RID: 9320
		private FontTechnology _technology;

		// Token: 0x04002469 RID: 9321
		private int _faceIndex;

		// Token: 0x0400246A RID: 9322
		private int _directoryOffset;

		// Token: 0x0400246B RID: 9323
		private TrueTypeFontDriver.DirectoryEntry[] _tableDirectory;

		// Token: 0x020009E6 RID: 2534
		private struct DirectoryEntry
		{
			// Token: 0x04002ECA RID: 11978
			internal TrueTypeFontDriver.TrueTypeTags tag;

			// Token: 0x04002ECB RID: 11979
			internal CheckedPointer pointer;
		}

		// Token: 0x020009E7 RID: 2535
		private enum TrueTypeTags
		{
			// Token: 0x04002ECD RID: 11981
			CharToIndexMap = 1668112752,
			// Token: 0x04002ECE RID: 11982
			ControlValue = 1668707360,
			// Token: 0x04002ECF RID: 11983
			BitmapData = 1161970772,
			// Token: 0x04002ED0 RID: 11984
			BitmapLocation = 1161972803,
			// Token: 0x04002ED1 RID: 11985
			BitmapScale = 1161974595,
			// Token: 0x04002ED2 RID: 11986
			Editor0 = 1701082160,
			// Token: 0x04002ED3 RID: 11987
			Editor1,
			// Token: 0x04002ED4 RID: 11988
			Encryption = 1668446576,
			// Token: 0x04002ED5 RID: 11989
			FontHeader = 1751474532,
			// Token: 0x04002ED6 RID: 11990
			FontProgram = 1718642541,
			// Token: 0x04002ED7 RID: 11991
			GridfitAndScanProc = 1734439792,
			// Token: 0x04002ED8 RID: 11992
			GlyphDirectory = 1734633842,
			// Token: 0x04002ED9 RID: 11993
			GlyphData = 1735162214,
			// Token: 0x04002EDA RID: 11994
			HoriDeviceMetrics = 1751412088,
			// Token: 0x04002EDB RID: 11995
			HoriHeader = 1751672161,
			// Token: 0x04002EDC RID: 11996
			HorizontalMetrics = 1752003704,
			// Token: 0x04002EDD RID: 11997
			IndexToLoc = 1819239265,
			// Token: 0x04002EDE RID: 11998
			Kerning = 1801810542,
			// Token: 0x04002EDF RID: 11999
			LinearThreshold = 1280594760,
			// Token: 0x04002EE0 RID: 12000
			MaxProfile = 1835104368,
			// Token: 0x04002EE1 RID: 12001
			NamingTable = 1851878757,
			// Token: 0x04002EE2 RID: 12002
			OS_2 = 1330851634,
			// Token: 0x04002EE3 RID: 12003
			Postscript = 1886352244,
			// Token: 0x04002EE4 RID: 12004
			PreProgram = 1886545264,
			// Token: 0x04002EE5 RID: 12005
			VertDeviceMetrics = 1447316824,
			// Token: 0x04002EE6 RID: 12006
			VertHeader = 1986553185,
			// Token: 0x04002EE7 RID: 12007
			VerticalMetrics = 1986884728,
			// Token: 0x04002EE8 RID: 12008
			PCLT = 1346587732,
			// Token: 0x04002EE9 RID: 12009
			TTO_GSUB = 1196643650,
			// Token: 0x04002EEA RID: 12010
			TTO_GPOS = 1196445523,
			// Token: 0x04002EEB RID: 12011
			TTO_GDEF = 1195656518,
			// Token: 0x04002EEC RID: 12012
			TTO_BASE = 1111577413,
			// Token: 0x04002EED RID: 12013
			TTO_JSTF = 1246975046,
			// Token: 0x04002EEE RID: 12014
			OTTO = 1330926671,
			// Token: 0x04002EEF RID: 12015
			TTC_TTCF = 1953784678
		}
	}
}
