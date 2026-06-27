using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000760 RID: 1888
	internal sealed class BidiState : Bidi.State
	{
		// Token: 0x06004F8C RID: 20364 RVA: 0x0013DE40 File Offset: 0x0013D240
		public BidiState(FormatSettings settings, int cpFirst) : this(settings, cpFirst, null)
		{
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0013DE58 File Offset: 0x0013D258
		public BidiState(FormatSettings settings, int cpFirst, TextModifierScope modifierScope) : base(settings.Pap.RightToLeft)
		{
			this._settings = settings;
			this._cpFirst = cpFirst;
			this.NumberClass = DirectionClass.ClassInvalid;
			this.StrongCharClass = DirectionClass.ClassInvalid;
			while (modifierScope != null && !modifierScope.TextModifier.HasDirectionalEmbedding)
			{
				modifierScope = modifierScope.ParentScope;
			}
			if (modifierScope != null)
			{
				this._cpFirstScope = modifierScope.TextSourceCharacterIndex;
				Bidi.BidiStack bidiStack = new Bidi.BidiStack();
				bidiStack.Init(base.LevelStack);
				ushort overflow = 0;
				BidiState.InitLevelStackFromModifierScope(bidiStack, modifierScope, ref overflow);
				base.LevelStack = bidiStack.GetData();
				base.Overflow = overflow;
			}
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0013DEF0 File Offset: 0x0013D2F0
		internal void SetLastDirectionClassesAtLevelChange()
		{
			if ((this.CurrentLevel & 1) == 0)
			{
				this.LastStrongClass = DirectionClass.Left;
				this.LastNumberClass = DirectionClass.Left;
				return;
			}
			this.LastStrongClass = DirectionClass.ArabicLetter;
			this.LastNumberClass = DirectionClass.ArabicNumber;
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x0013DF24 File Offset: 0x0013D324
		internal byte CurrentLevel
		{
			get
			{
				return Bidi.BidiStack.GetMaximumLevel(base.LevelStack);
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x0013DF3C File Offset: 0x0013D33C
		// (set) Token: 0x06004F91 RID: 20369 RVA: 0x0013DF60 File Offset: 0x0013D360
		public override DirectionClass LastNumberClass
		{
			get
			{
				if (this.NumberClass == DirectionClass.ClassInvalid)
				{
					this.GetLastDirectionClasses();
				}
				return this.NumberClass;
			}
			set
			{
				this.NumberClass = value;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x0013DF74 File Offset: 0x0013D374
		// (set) Token: 0x06004F93 RID: 20371 RVA: 0x0013DF98 File Offset: 0x0013D398
		public override DirectionClass LastStrongClass
		{
			get
			{
				if (this.StrongCharClass == DirectionClass.ClassInvalid)
				{
					this.GetLastDirectionClasses();
				}
				return this.StrongCharClass;
			}
			set
			{
				this.StrongCharClass = value;
				this.NumberClass = value;
			}
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0013DFB4 File Offset: 0x0013D3B4
		private void GetLastDirectionClasses()
		{
			DirectionClass directionClass = DirectionClass.ClassInvalid;
			DirectionClass directionClass2 = DirectionClass.ClassInvalid;
			bool flag = true;
			while (flag && this._cpFirst > this._cpFirstScope)
			{
				TextSpan<CultureSpecificCharacterBufferRange> precedingText = this._settings.GetPrecedingText(this._cpFirst);
				CultureSpecificCharacterBufferRange value = precedingText.Value;
				if (precedingText.Length <= 0)
				{
					break;
				}
				if (!value.CharacterBufferRange.IsEmpty)
				{
					flag = Bidi.GetLastStongAndNumberClass(value.CharacterBufferRange, ref directionClass, ref directionClass2);
					if (directionClass != DirectionClass.ClassInvalid)
					{
						this.StrongCharClass = directionClass;
						if (this.NumberClass == DirectionClass.ClassInvalid)
						{
							if (directionClass2 == DirectionClass.EuropeanNumber)
							{
								directionClass2 = this.GetEuropeanNumberClassOverride(CultureMapper.GetSpecificCulture(value.CultureInfo));
							}
							this.NumberClass = directionClass2;
							break;
						}
						break;
					}
				}
				this._cpFirst -= precedingText.Length;
			}
			if (directionClass == DirectionClass.ClassInvalid)
			{
				this.StrongCharClass = (((this.CurrentLevel & 1) == 0) ? DirectionClass.Left : DirectionClass.ArabicLetter);
			}
			if (directionClass2 == DirectionClass.ClassInvalid)
			{
				this.NumberClass = (((this.CurrentLevel & 1) == 0) ? DirectionClass.Left : DirectionClass.ArabicNumber);
			}
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0013E0A8 File Offset: 0x0013D4A8
		private static void InitLevelStackFromModifierScope(Bidi.BidiStack stack, TextModifierScope scope, ref ushort overflowLevels)
		{
			Stack<TextModifier> stack2 = new Stack<TextModifier>(32);
			for (TextModifierScope textModifierScope = scope; textModifierScope != null; textModifierScope = textModifierScope.ParentScope)
			{
				if (textModifierScope.TextModifier.HasDirectionalEmbedding)
				{
					stack2.Push(textModifierScope.TextModifier);
				}
			}
			while (stack2.Count > 0)
			{
				TextModifier textModifier = stack2.Pop();
				if (overflowLevels > 0)
				{
					overflowLevels += 1;
				}
				else if (!stack.Push(textModifier.FlowDirection == FlowDirection.LeftToRight))
				{
					overflowLevels = 1;
				}
			}
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0013E118 File Offset: 0x0013D518
		internal DirectionClass GetEuropeanNumberClassOverride(CultureInfo cultureInfo)
		{
			if (cultureInfo != null && ((cultureInfo.LCID & 255) == 1 || (cultureInfo.LCID & 255) == 41) && (this.CurrentLevel & 1) != 0)
			{
				return DirectionClass.ArabicNumber;
			}
			return DirectionClass.EuropeanNumber;
		}

		// Token: 0x0400241F RID: 9247
		private FormatSettings _settings;

		// Token: 0x04002420 RID: 9248
		private int _cpFirst;

		// Token: 0x04002421 RID: 9249
		private int _cpFirstScope;
	}
}
