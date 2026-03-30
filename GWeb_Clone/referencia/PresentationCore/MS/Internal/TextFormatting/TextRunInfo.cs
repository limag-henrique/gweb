using System;
using System.Globalization;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075D RID: 1885
	internal sealed class TextRunInfo
	{
		// Token: 0x06004F46 RID: 20294 RVA: 0x0013B694 File Offset: 0x0013AA94
		internal TextRunInfo(CharacterBufferRange charBufferRange, int textRunLength, int offsetToFirstCp, TextRun textRun, Plsrun lsRunType, ushort charFlags, CultureInfo digitCulture, bool contextualSubstitution, bool symbolTypeface, TextModifierScope modifierScope)
		{
			this._charBufferRange = charBufferRange;
			this._textRunLength = textRunLength;
			this._offsetToFirstCp = offsetToFirstCp;
			this._textRun = textRun;
			this._plsrun = lsRunType;
			this._charFlags = charFlags;
			this._digitCulture = digitCulture;
			this._runFlags = 0;
			this._modifierScope = modifierScope;
			if (contextualSubstitution)
			{
				this._runFlags |= 1;
			}
			if (symbolTypeface)
			{
				this._runFlags |= 2;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x0013B714 File Offset: 0x0013AB14
		internal TextRun TextRun
		{
			get
			{
				return this._textRun;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x0013B728 File Offset: 0x0013AB28
		internal TextRunProperties Properties
		{
			get
			{
				if (this._properties == null)
				{
					if (this._modifierScope != null)
					{
						this._properties = this._modifierScope.ModifyProperties(this._textRun.Properties);
					}
					else
					{
						this._properties = this._textRun.Properties;
					}
				}
				return this._properties;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x0013B77C File Offset: 0x0013AB7C
		internal CharacterBuffer CharacterBuffer
		{
			get
			{
				return this._charBufferRange.CharacterBuffer;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x0013B794 File Offset: 0x0013AB94
		internal int OffsetToFirstChar
		{
			get
			{
				return this._charBufferRange.OffsetToFirstChar;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x0013B7AC File Offset: 0x0013ABAC
		internal int OffsetToFirstCp
		{
			get
			{
				return this._offsetToFirstCp;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x0013B7C0 File Offset: 0x0013ABC0
		// (set) Token: 0x06004F4D RID: 20301 RVA: 0x0013B7D8 File Offset: 0x0013ABD8
		internal int StringLength
		{
			get
			{
				return this._charBufferRange.Length;
			}
			set
			{
				this._charBufferRange = new CharacterBufferRange(this._charBufferRange.CharacterBufferReference, value);
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06004F4E RID: 20302 RVA: 0x0013B7FC File Offset: 0x0013ABFC
		// (set) Token: 0x06004F4F RID: 20303 RVA: 0x0013B810 File Offset: 0x0013AC10
		internal int Length
		{
			get
			{
				return this._textRunLength;
			}
			set
			{
				this._textRunLength = value;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x0013B824 File Offset: 0x0013AC24
		// (set) Token: 0x06004F51 RID: 20305 RVA: 0x0013B838 File Offset: 0x0013AC38
		internal ushort CharacterAttributeFlags
		{
			get
			{
				return this._charFlags;
			}
			set
			{
				this._charFlags = value;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x0013B84C File Offset: 0x0013AC4C
		internal CultureInfo DigitCulture
		{
			get
			{
				return this._digitCulture;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x0013B860 File Offset: 0x0013AC60
		internal bool ContextualSubstitution
		{
			get
			{
				return (this._runFlags & 1) > 0;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x0013B878 File Offset: 0x0013AC78
		internal bool IsSymbol
		{
			get
			{
				return (this._runFlags & 2) > 0;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x0013B890 File Offset: 0x0013AC90
		internal Plsrun Plsrun
		{
			get
			{
				return this._plsrun;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x0013B8A4 File Offset: 0x0013ACA4
		internal bool IsEndOfLine
		{
			get
			{
				return this._textRun is TextEndOfLine;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x0013B8C0 File Offset: 0x0013ACC0
		internal TextModifierScope TextModifierScope
		{
			get
			{
				return this._modifierScope;
			}
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x0013B8D4 File Offset: 0x0013ACD4
		internal int GetRoughWidth(double realToIdeal)
		{
			TextRunProperties properties = this._textRun.Properties;
			if (properties != null)
			{
				return (int)Math.Round(properties.FontRenderingEmSize * 0.75 * (double)this._textRunLength * realToIdeal);
			}
			return 0;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0013B914 File Offset: 0x0013AD14
		internal static Plsrun GetRunType(TextRun textRun)
		{
			if (textRun is ITextSymbols || textRun is TextShapeableSymbols)
			{
				return Plsrun.Text;
			}
			if (textRun is TextEmbeddedObject)
			{
				return Plsrun.InlineObject;
			}
			if (textRun is TextEndOfParagraph)
			{
				return Plsrun.ParaBreak;
			}
			if (textRun is TextEndOfLine)
			{
				return Plsrun.LineBreak;
			}
			return Plsrun.Hidden;
		}

		// Token: 0x040023F2 RID: 9202
		private CharacterBufferRange _charBufferRange;

		// Token: 0x040023F3 RID: 9203
		private int _textRunLength;

		// Token: 0x040023F4 RID: 9204
		private int _offsetToFirstCp;

		// Token: 0x040023F5 RID: 9205
		private TextRun _textRun;

		// Token: 0x040023F6 RID: 9206
		private Plsrun _plsrun;

		// Token: 0x040023F7 RID: 9207
		private CultureInfo _digitCulture;

		// Token: 0x040023F8 RID: 9208
		private ushort _charFlags;

		// Token: 0x040023F9 RID: 9209
		private ushort _runFlags;

		// Token: 0x040023FA RID: 9210
		private TextModifierScope _modifierScope;

		// Token: 0x040023FB RID: 9211
		private TextRunProperties _properties;
	}
}
