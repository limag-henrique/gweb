using System;
using MS.Internal.Ink.InkSerializedFormat;

namespace System.Windows.Ink
{
	/// <summary>Contém um conjunto de GUIDs que identificam as propriedades na classe <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
	// Token: 0x02000335 RID: 821
	public static class DrawingAttributeIds
	{
		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Color" />.</summary>
		// Token: 0x04000EEA RID: 3818
		public static readonly Guid Color = KnownIdCache.OriginalISFIdTable[18];

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.StylusTip" />.</summary>
		// Token: 0x04000EEB RID: 3819
		public static readonly Guid StylusTip = new Guid(891733809U, 61049, 18824, 185, 62, 112, 217, 47, 137, 7, 237);

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.StylusTipTransform" />.</summary>
		// Token: 0x04000EEC RID: 3820
		public static readonly Guid StylusTipTransform = new Guid(1264827414, 31684, 20434, 149, 218, 172, byte.MaxValue, 71, 117, 115, 45);

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Height" />.</summary>
		// Token: 0x04000EED RID: 3821
		public static readonly Guid StylusHeight = KnownIdCache.OriginalISFIdTable[20];

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.Width" />.</summary>
		// Token: 0x04000EEE RID: 3822
		public static readonly Guid StylusWidth = KnownIdCache.OriginalISFIdTable[19];

		/// <summary>Identifica a propriedade <see langword="DrawingFlags" /> interna.</summary>
		// Token: 0x04000EEF RID: 3823
		public static readonly Guid DrawingFlags = KnownIdCache.OriginalISFIdTable[22];

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Ink.DrawingAttributes.IsHighlighter" />.</summary>
		// Token: 0x04000EF0 RID: 3824
		public static readonly Guid IsHighlighter = new Guid(3459276314U, 3592, 17891, 140, 220, 228, 11, 180, 80, 111, 33);
	}
}
