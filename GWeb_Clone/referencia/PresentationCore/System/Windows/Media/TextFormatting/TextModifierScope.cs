using System;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x020005AF RID: 1455
	internal sealed class TextModifierScope
	{
		// Token: 0x0600428C RID: 17036 RVA: 0x00103B78 File Offset: 0x00102F78
		internal TextModifierScope(TextModifierScope parentScope, TextModifier modifier, int cp)
		{
			this._parentScope = parentScope;
			this._modifier = modifier;
			this._cp = cp;
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x00103BA0 File Offset: 0x00102FA0
		public TextModifierScope ParentScope
		{
			get
			{
				return this._parentScope;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x00103BB4 File Offset: 0x00102FB4
		public TextModifier TextModifier
		{
			get
			{
				return this._modifier;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x00103BC8 File Offset: 0x00102FC8
		public int TextSourceCharacterIndex
		{
			get
			{
				return this._cp;
			}
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x00103BDC File Offset: 0x00102FDC
		internal TextRunProperties ModifyProperties(TextRunProperties properties)
		{
			for (TextModifierScope textModifierScope = this; textModifierScope != null; textModifierScope = textModifierScope._parentScope)
			{
				properties = textModifierScope._modifier.ModifyProperties(properties);
			}
			return properties;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x00103C08 File Offset: 0x00103008
		internal TextModifierScope CloneStack()
		{
			TextModifierScope textModifierScope = new TextModifierScope(null, this._modifier, this._cp);
			TextModifierScope textModifierScope2 = textModifierScope;
			for (TextModifierScope parentScope = this._parentScope; parentScope != null; parentScope = parentScope._parentScope)
			{
				textModifierScope2._parentScope = new TextModifierScope(null, parentScope._modifier, parentScope._cp);
				textModifierScope2 = textModifierScope2._parentScope;
			}
			return textModifierScope;
		}

		// Token: 0x04001837 RID: 6199
		private TextModifierScope _parentScope;

		// Token: 0x04001838 RID: 6200
		private TextModifier _modifier;

		// Token: 0x04001839 RID: 6201
		private int _cp;
	}
}
