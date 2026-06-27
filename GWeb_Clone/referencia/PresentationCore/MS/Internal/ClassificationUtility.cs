using System;
using MS.Internal.Text.TextInterface;

namespace MS.Internal
{
	// Token: 0x02000682 RID: 1666
	internal class ClassificationUtility : IClassification
	{
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x0011E738 File Offset: 0x0011DB38
		internal static ClassificationUtility Instance
		{
			get
			{
				return ClassificationUtility._classificationUtilityInstance;
			}
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0011E74C File Offset: 0x0011DB4C
		public void GetCharAttribute(int unicodeScalar, out bool isCombining, out bool needsCaretInfo, out bool isIndic, out bool isDigit, out bool isLatin, out bool isStrong)
		{
			CharacterAttribute characterAttribute = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(unicodeScalar));
			byte itemClass = characterAttribute.ItemClass;
			isCombining = (itemClass == 7 || itemClass == 8 || Classification.IsIVS(unicodeScalar));
			isStrong = (itemClass == 5);
			int script = (int)characterAttribute.Script;
			needsCaretInfo = ClassificationUtility.ScriptCaretInfo[script];
			ScriptID scriptID = (ScriptID)script;
			isDigit = (scriptID == ScriptID.Digit);
			isLatin = (scriptID == ScriptID.Latin);
			if (isLatin)
			{
				isIndic = false;
				return;
			}
			isIndic = ClassificationUtility.IsScriptIndic(scriptID);
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0011E7BC File Offset: 0x0011DBBC
		private static bool IsScriptIndic(ScriptID scriptId)
		{
			return scriptId == ScriptID.Bengali || scriptId == ScriptID.Devanagari || scriptId == ScriptID.Gurmukhi || scriptId == ScriptID.Gujarati || scriptId == ScriptID.Kannada || scriptId == ScriptID.Malayalam || scriptId == ScriptID.Oriya || scriptId == ScriptID.Tamil || scriptId == ScriptID.Telugu;
		}

		// Token: 0x04001CDD RID: 7389
		internal static readonly bool[] ScriptCaretInfo = new bool[]
		{
			false,
			false,
			false,
			true,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			false,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false
		};

		// Token: 0x04001CDE RID: 7390
		private static ClassificationUtility _classificationUtilityInstance = new ClassificationUtility();
	}
}
