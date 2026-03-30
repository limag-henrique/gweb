using System;
using System.Collections.Generic;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x020006FE RID: 1790
	internal static class Bidi
	{
		// Token: 0x06004D12 RID: 19730 RVA: 0x0012FE6C File Offset: 0x0012F26C
		static Bidi()
		{
			Bidi.Action = new Bidi.StateMachineAction[,]
			{
				{
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.EN_L,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.NSM_ST,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.EN_AL,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.NSM_ST,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.SEP_ST,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.NSM_ST,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.NUM_NUM,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ES_AN,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.NSM_ST,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.NUM_NUM,
					Bidi.StateMachineAction.ST_ST,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.CS_NUM,
					Bidi.StateMachineAction.ET_EN,
					Bidi.StateMachineAction.NSM_ST,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_ET,
					Bidi.StateMachineAction.ST_ET,
					Bidi.StateMachineAction.ST_ET,
					Bidi.StateMachineAction.EN_ET,
					Bidi.StateMachineAction.ST_ET,
					Bidi.StateMachineAction.SEP_ET,
					Bidi.StateMachineAction.SEP_ET,
					Bidi.StateMachineAction.ET_ET,
					Bidi.StateMachineAction.NSM_ET,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ET
				},
				{
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.NUM_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.ET_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.NUM_NUMSEP,
					Bidi.StateMachineAction.ST_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.ET_NUMSEP,
					Bidi.StateMachineAction.SEP_NUMSEP,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ST
				},
				{
					Bidi.StateMachineAction.ST_N,
					Bidi.StateMachineAction.ST_N,
					Bidi.StateMachineAction.ST_N,
					Bidi.StateMachineAction.EN_N,
					Bidi.StateMachineAction.ST_N,
					Bidi.StateMachineAction.SEP_N,
					Bidi.StateMachineAction.SEP_N,
					Bidi.StateMachineAction.ET_N,
					Bidi.StateMachineAction.NSM_ET,
					Bidi.StateMachineAction.BN_ST,
					Bidi.StateMachineAction.N_ET
				}
			};
			Bidi.NextState = new Bidi.StateMachineState[,]
			{
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ANfCS,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_ENfCS,
					Bidi.StateMachineState.S_ENfCS,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ANfCS,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ENfCS,
					Bidi.StateMachineState.S_N
				},
				{
					Bidi.StateMachineState.S_L,
					Bidi.StateMachineState.S_R,
					Bidi.StateMachineState.S_AN,
					Bidi.StateMachineState.S_EN,
					Bidi.StateMachineState.S_AL,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_ET,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N,
					Bidi.StateMachineState.S_N
				}
			};
			Bidi.ImplictPush = new byte[,]
			{
				{
					0,
					1,
					2,
					2
				},
				{
					1,
					0,
					1,
					1
				}
			};
			Bidi.CharProperty = new byte[,]
			{
				{
					1,
					1,
					0,
					0,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					1,
					1,
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					1,
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					0,
					0,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				}
			};
			Bidi.ClassToState = new Bidi.StateMachineState[]
			{
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_R,
				Bidi.StateMachineState.S_AN,
				Bidi.StateMachineState.S_EN,
				Bidi.StateMachineState.S_AL,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L,
				Bidi.StateMachineState.S_L
			};
			Bidi.FastPathClass = new byte[]
			{
				2,
				3,
				0,
				0,
				3,
				1,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1
			};
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0012FF14 File Offset: 0x0012F314
		internal static bool GetLastStongAndNumberClass(CharacterBufferRange charString, ref DirectionClass strongClass, ref DirectionClass numberClass)
		{
			int num;
			for (int i = charString.Length - 1; i >= 0; i -= num)
			{
				int unicodeScalar = (int)charString[i];
				num = 1;
				if ((charString[i] & 'ﰀ') == '\udc00' && i > 0 && (charString[i - 1] & 'ﰀ') == '\ud800')
				{
					unicodeScalar = (int)((int)(charString[i - 1] & 'Ͽ') << 10 | (charString[i] & 'Ͽ')) + 65536;
					num = 2;
				}
				DirectionClass biDi = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(unicodeScalar)).BiDi;
				if (biDi == DirectionClass.ParagraphSeparator)
				{
					return false;
				}
				if (Bidi.CharProperty[1, (int)biDi] == 1)
				{
					if (numberClass == DirectionClass.ClassInvalid)
					{
						numberClass = biDi;
					}
					if (biDi != DirectionClass.EuropeanNumber)
					{
						strongClass = biDi;
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x0012FFDC File Offset: 0x0012F3DC
		private static bool GetFirstStrongCharacter(CharacterBuffer charBuffer, int ichText, int cchText, ref DirectionClass strongClass)
		{
			DirectionClass directionClass = DirectionClass.ClassInvalid;
			int num2;
			for (int i = 0; i < cchText; i += num2)
			{
				int num = (int)charBuffer[ichText + i];
				num2 = 1;
				if ((num & 64512) == 55296)
				{
					num = DoubleWideChar.GetChar(charBuffer, ichText, cchText, i, out num2);
				}
				directionClass = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num)).BiDi;
				if (Bidi.CharProperty[0, (int)directionClass] == 1 || directionClass == DirectionClass.ParagraphSeparator)
				{
					break;
				}
			}
			if (Bidi.CharProperty[0, (int)directionClass] == 1)
			{
				strongClass = directionClass;
				return true;
			}
			return false;
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x00130058 File Offset: 0x0012F458
		private static void ResolveNeutrals(IList<DirectionClass> characterClass, int classIndex, int count, DirectionClass startClass, DirectionClass endClass, byte runLevel)
		{
			if (characterClass == null || count == 0)
			{
				return;
			}
			DirectionClass directionClass = (startClass == DirectionClass.EuropeanNumber || startClass == DirectionClass.ArabicNumber || startClass == DirectionClass.ArabicLetter) ? DirectionClass.Right : startClass;
			DirectionClass directionClass2 = (endClass == DirectionClass.EuropeanNumber || endClass == DirectionClass.ArabicNumber || endClass == DirectionClass.ArabicLetter) ? DirectionClass.Right : endClass;
			DirectionClass value;
			if (directionClass == directionClass2)
			{
				value = directionClass;
			}
			else
			{
				value = (Bidi.Helper.IsOdd(runLevel) ? DirectionClass.Right : DirectionClass.Left);
			}
			for (int i = 0; i < count; i++)
			{
				characterClass[i + classIndex] = value;
			}
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x001300C0 File Offset: 0x0012F4C0
		private static void ChangeType(IList<DirectionClass> characterClass, int classIndex, int count, DirectionClass newClass)
		{
			if (characterClass == null || count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				characterClass[i + classIndex] = newClass;
			}
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x001300EC File Offset: 0x0012F4EC
		private static int ResolveNeutralAndWeak(IList<DirectionClass> characterClass, int classIndex, int runLength, DirectionClass sor, DirectionClass eor, byte runLevel, Bidi.State stateIn, Bidi.State stateOut, bool previousStrongIsArabic, Bidi.Flags flags)
		{
			int num = -1;
			int num2 = -1;
			DirectionClass lastNumberClass = DirectionClass.ClassInvalid;
			bool flag = false;
			bool flag2 = false;
			int num3 = 0;
			if (runLength == 0)
			{
				return 0;
			}
			DirectionClass directionClass;
			DirectionClass directionClass3;
			DirectionClass directionClass2;
			if (stateIn != null)
			{
				directionClass = stateIn.LastStrongClass;
				if (stateIn.LastNumberClass != DirectionClass.ClassInvalid)
				{
					directionClass2 = (lastNumberClass = (directionClass3 = stateIn.LastNumberClass));
				}
				else
				{
					directionClass3 = (directionClass2 = directionClass);
				}
			}
			else if (previousStrongIsArabic)
			{
				directionClass2 = DirectionClass.ArabicLetter;
				directionClass = sor;
				directionClass3 = sor;
				flag = true;
			}
			else
			{
				directionClass = sor;
				directionClass3 = sor;
				directionClass2 = sor;
			}
			Bidi.StateMachineState stateMachineState = Bidi.ClassToState[(int)directionClass2];
			int i;
			for (i = 0; i < runLength; i++)
			{
				DirectionClass directionClass4 = characterClass[i + classIndex];
				if (Bidi.CharProperty[5, (int)directionClass4] == 0)
				{
					return num3;
				}
				Bidi.StateMachineAction stateMachineAction = Bidi.Action[(int)stateMachineState, (int)directionClass4];
				if (Bidi.CharProperty[4, (int)directionClass4] == 1)
				{
					lastNumberClass = directionClass4;
				}
				if (Bidi.CharProperty[0, (int)directionClass4] == 1)
				{
					flag = false;
				}
				switch (stateMachineAction)
				{
				case Bidi.StateMachineAction.ST_ST:
					if (directionClass4 == DirectionClass.ArabicLetter)
					{
						characterClass[i + classIndex] = DirectionClass.Right;
					}
					if (num2 != -1)
					{
						num = num2;
						Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						num2 = (num = -1);
					}
					if (directionClass4 != DirectionClass.ArabicNumber || (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Right))
					{
						directionClass = directionClass4;
					}
					flag2 = (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Left);
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.ST_ET:
					if (num == -1)
					{
						num = num2;
					}
					if (directionClass4 == DirectionClass.ArabicLetter)
					{
						characterClass[i + classIndex] = DirectionClass.Right;
					}
					Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
					num2 = (num = -1);
					if (directionClass4 != DirectionClass.ArabicNumber || (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Right))
					{
						directionClass = directionClass4;
					}
					flag2 = (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Left);
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.ST_NUMSEP:
				{
					bool flag3 = false;
					if (directionClass4 == DirectionClass.ArabicLetter)
					{
						characterClass[i + classIndex] = DirectionClass.Right;
					}
					if ((directionClass == DirectionClass.ArabicLetter || flag) && ((directionClass4 == DirectionClass.EuropeanNumber && (flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight) || directionClass4 == DirectionClass.ArabicNumber))
					{
						characterClass[i + classIndex] = DirectionClass.ArabicNumber;
						bool flag4 = true;
						int num4 = 0;
						for (int j = num2; j < i; j++)
						{
							if (characterClass[j + classIndex] != DirectionClass.CommonSeparator && characterClass[j + classIndex] != DirectionClass.BoundaryNeutral)
							{
								flag4 = false;
								break;
							}
							if (characterClass[j + classIndex] == DirectionClass.CommonSeparator)
							{
								num4++;
							}
						}
						if (flag4 && num4 == 1)
						{
							Bidi.ChangeType(characterClass, classIndex + num2, i - num2, characterClass[i + classIndex]);
							flag3 = true;
						}
					}
					else if (directionClass == DirectionClass.Left && directionClass4 == DirectionClass.EuropeanNumber)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
					}
					if (!flag3)
					{
						num = num2;
						Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
					}
					num2 = (num = -1);
					if ((directionClass4 != DirectionClass.ArabicNumber || (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Right)) && ((directionClass != DirectionClass.Left && directionClass != DirectionClass.ArabicLetter) || directionClass4 != DirectionClass.EuropeanNumber))
					{
						directionClass = directionClass4;
					}
					flag2 = (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Left);
					directionClass3 = directionClass4;
					if (characterClass[i + classIndex] == DirectionClass.ArabicNumber)
					{
						directionClass4 = DirectionClass.ArabicNumber;
					}
					break;
				}
				case Bidi.StateMachineAction.ST_N:
					if (directionClass4 == DirectionClass.ArabicLetter)
					{
						characterClass[i + classIndex] = DirectionClass.Right;
					}
					Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
					num2 = (num = -1);
					if (directionClass4 != DirectionClass.ArabicNumber || (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Right))
					{
						directionClass = directionClass4;
					}
					flag2 = (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Left);
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.SEP_ST:
					if (num2 != -1)
					{
						num = num2;
						num2 = -1;
					}
					else
					{
						num = i;
					}
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.CS_NUM:
					if (num2 == -1)
					{
						num2 = i;
					}
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.SEP_ET:
					if (num == -1)
					{
						num = num2;
					}
					num2 = -1;
					directionClass3 = DirectionClass.GenericNeutral;
					break;
				case Bidi.StateMachineAction.SEP_NUMSEP:
					num = num2;
					num2 = -1;
					directionClass3 = DirectionClass.GenericNeutral;
					break;
				case Bidi.StateMachineAction.SEP_N:
					num2 = -1;
					break;
				case Bidi.StateMachineAction.ES_AN:
					if (num2 != -1)
					{
						num = num2;
						num2 = -1;
					}
					else
					{
						num = i;
					}
					directionClass3 = DirectionClass.GenericNeutral;
					break;
				case Bidi.StateMachineAction.ET_NUMSEP:
					num = num2;
					num2 = i;
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.ET_EN:
					if (num2 == -1)
					{
						num2 = i;
					}
					if (directionClass != DirectionClass.ArabicLetter && !flag)
					{
						if (directionClass == DirectionClass.Left)
						{
							characterClass[i + classIndex] = DirectionClass.Left;
						}
						else
						{
							characterClass[i + classIndex] = DirectionClass.EuropeanNumber;
						}
						Bidi.ChangeType(characterClass, classIndex + num2, i - num2, characterClass[i + classIndex]);
						num2 = -1;
					}
					directionClass3 = DirectionClass.EuropeanNumber;
					if (i < runLength - 1 && (characterClass[i + 1 + classIndex] == DirectionClass.EuropeanSeparator || characterClass[i + 1 + classIndex] == DirectionClass.CommonSeparator))
					{
						characterClass[i + 1 + classIndex] = DirectionClass.GenericNeutral;
					}
					break;
				case Bidi.StateMachineAction.ET_N:
					if (num2 == -1)
					{
						num2 = i;
					}
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.NUM_NUMSEP:
					if (directionClass == DirectionClass.ArabicLetter || flag || flag2)
					{
						if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight)
						{
							characterClass[i + classIndex] = DirectionClass.ArabicNumber;
						}
					}
					else if (directionClass == DirectionClass.Left)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
					}
					else
					{
						directionClass = directionClass4;
					}
					Bidi.ChangeType(characterClass, classIndex + num2, i - num2, characterClass[i + classIndex]);
					num2 = -1;
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.NUM_NUM:
					if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight && (directionClass == DirectionClass.ArabicLetter || flag))
					{
						characterClass[i + classIndex] = DirectionClass.ArabicNumber;
						directionClass4 = DirectionClass.ArabicNumber;
					}
					else if (directionClass == DirectionClass.Left)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
					}
					if (num2 != -1)
					{
						num = num2;
						Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						num2 = (num = -1);
					}
					flag2 = (directionClass4 == DirectionClass.ArabicNumber && directionClass == DirectionClass.Left);
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.EN_L:
					if (directionClass == DirectionClass.Left)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
					}
					if (num2 != -1)
					{
						num = num2;
						Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						num2 = (num = -1);
					}
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.EN_AL:
					if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight)
					{
						characterClass[i + classIndex] = DirectionClass.ArabicNumber;
					}
					else
					{
						stateMachineState = Bidi.StateMachineState.S_L;
					}
					if (num2 != -1)
					{
						num = num2;
						Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						num2 = (num = -1);
					}
					directionClass3 = characterClass[i + classIndex];
					break;
				case Bidi.StateMachineAction.EN_ET:
					if (directionClass == DirectionClass.ArabicLetter || flag)
					{
						if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight)
						{
							characterClass[i + classIndex] = DirectionClass.ArabicNumber;
							directionClass4 = DirectionClass.ArabicNumber;
						}
						if (num == -1)
						{
							Bidi.ResolveNeutrals(characterClass, classIndex + num2, i - num2, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						}
						else
						{
							Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						}
					}
					else if (directionClass == DirectionClass.Left)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
						Bidi.ChangeType(characterClass, classIndex + num2, i - num2, characterClass[i + classIndex]);
						if (num != -1)
						{
							Bidi.ResolveNeutrals(characterClass, classIndex + num, num2 - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
						}
						flag2 = false;
					}
					else
					{
						Bidi.ChangeType(characterClass, classIndex + num2, i - num2, DirectionClass.EuropeanNumber);
						if (num != -1)
						{
							Bidi.ResolveNeutrals(characterClass, classIndex + num, num2 - num, flag2 ? DirectionClass.ArabicNumber : directionClass, directionClass4, runLevel);
						}
					}
					num2 = (num = -1);
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.EN_N:
					if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight && (directionClass == DirectionClass.ArabicLetter || flag))
					{
						characterClass[i + classIndex] = DirectionClass.ArabicNumber;
						directionClass4 = DirectionClass.ArabicNumber;
					}
					else if (directionClass == DirectionClass.Left)
					{
						characterClass[i + classIndex] = DirectionClass.Left;
					}
					Bidi.ResolveNeutrals(characterClass, classIndex + num, i - num, flag2 ? DirectionClass.ArabicNumber : directionClass, characterClass[i + classIndex], runLevel);
					num2 = (num = -1);
					flag2 = false;
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.BN_ST:
					if (num2 == -1)
					{
						num2 = i;
					}
					break;
				case Bidi.StateMachineAction.NSM_ST:
					if (directionClass == DirectionClass.ArabicLetter)
					{
						if (directionClass3 == DirectionClass.EuropeanNumber)
						{
							if ((flags & Bidi.Flags.OverrideEuropeanNumberResolution) == Bidi.Flags.DirectionLeftToRight)
							{
								characterClass[i + classIndex] = DirectionClass.ArabicNumber;
							}
							else
							{
								characterClass[i + classIndex] = DirectionClass.EuropeanNumber;
							}
						}
						else if (directionClass3 != DirectionClass.ArabicNumber)
						{
							characterClass[i + classIndex] = DirectionClass.Right;
						}
						else
						{
							characterClass[i + classIndex] = DirectionClass.ArabicNumber;
						}
					}
					else
					{
						characterClass[i + classIndex] = ((flag2 || directionClass3 == DirectionClass.ArabicNumber) ? DirectionClass.ArabicNumber : ((directionClass3 == DirectionClass.EuropeanNumber && directionClass != DirectionClass.Left) ? DirectionClass.EuropeanNumber : directionClass));
					}
					if (num2 != -1)
					{
						Bidi.ChangeType(characterClass, classIndex + num2, i - num2, characterClass[i + classIndex]);
						num2 = -1;
					}
					break;
				case Bidi.StateMachineAction.NSM_ET:
					characterClass[i + classIndex] = directionClass3;
					break;
				case Bidi.StateMachineAction.N_ST:
					if (num2 != -1)
					{
						num = num2;
						num2 = -1;
					}
					else
					{
						num = i;
					}
					directionClass3 = directionClass4;
					break;
				case Bidi.StateMachineAction.N_ET:
					if (num == -1 && num2 != -1)
					{
						num = num2;
					}
					num2 = -1;
					directionClass3 = directionClass4;
					break;
				}
				stateMachineState = Bidi.NextState[(int)stateMachineState, (int)directionClass4];
				num3 = ((Math.Max(num, num2) == -1) ? (i + 1) : ((Math.Min(num, num2) == -1) ? Math.Max(num, num2) : Math.Min(num, num2)));
			}
			if (stateOut != null)
			{
				stateOut.LastStrongClass = directionClass;
				stateOut.LastNumberClass = lastNumberClass;
				return num3;
			}
			if (num3 != i)
			{
				Bidi.ResolveNeutrals(characterClass, classIndex + num3, i - num3, flag2 ? DirectionClass.ArabicNumber : directionClass, eor, runLevel);
			}
			return i;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x00130960 File Offset: 0x0012FD60
		private static void ResolveImplictLevels(IList<DirectionClass> characterClass, CharacterBuffer charBuffer, int ichText, int runLength, IList<byte> levels, int index, byte paragraphEmbeddingLevel)
		{
			if (runLength == 0)
			{
				return;
			}
			for (int i = runLength - 1; i >= 0; i--)
			{
				Invariant.Assert(Bidi.CharProperty[3, (int)characterClass[i + index]] == 1, "Cannot have unresolved classes during implict levels resolution");
				int num = (int)charBuffer[ichText + index + i];
				int num2 = 1;
				if ((num & 64512) == 56320 && i > 0 && (charBuffer[ichText + index + i - 1] & 'ﰀ') == '\ud800')
				{
					num = (int)((int)(charBuffer[ichText + index + i - 1] & 'Ͽ') << 10 | (charBuffer[ichText + index + i] & 'Ͽ')) + 65536;
					num2 = 2;
				}
				DirectionClass biDi = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num)).BiDi;
				if (biDi == DirectionClass.ParagraphSeparator || biDi == DirectionClass.SegmentSeparator)
				{
					levels[i + index] = paragraphEmbeddingLevel;
				}
				else
				{
					levels[i + index] = Bidi.ImplictPush[Bidi.Helper.IsOdd(levels[i + index]) ? 1 : 0, (int)characterClass[index + i]] + levels[i + index];
				}
				if (num2 > 1)
				{
					levels[i + index - 1] = levels[i + index];
					i--;
				}
			}
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x00130AA4 File Offset: 0x0012FEA4
		public static bool Analyze(char[] chars, int cchText, int cchTextMaxHint, Bidi.Flags flags, Bidi.State state, out byte[] levels, out int cchResolved)
		{
			DirectionClass[] array = new DirectionClass[cchText];
			levels = new byte[cchText];
			return Bidi.BidiAnalyzeInternal(new CharArrayCharacterBuffer(chars), 0, cchText, cchTextMaxHint, flags, state, levels, new PartialArray<DirectionClass>(array), out cchResolved);
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x00130AE4 File Offset: 0x0012FEE4
		internal static bool BidiAnalyzeInternal(CharacterBuffer charBuffer, int ichText, int cchText, int cchTextMaxHint, Bidi.Flags flags, Bidi.State state, IList<byte> levels, IList<DirectionClass> characterClass, out int cchResolved)
		{
			Bidi.State state2 = null;
			Bidi.State state3 = null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			Invariant.Assert(levels != null && levels.Count >= cchText);
			Invariant.Assert(characterClass != null && characterClass.Count >= cchText);
			cchResolved = 0;
			if (charBuffer == null || cchText <= 0 || charBuffer.Count < cchText || (((flags & Bidi.Flags.ContinueAnalysis) != Bidi.Flags.DirectionLeftToRight || (flags & Bidi.Flags.IncompleteText) != Bidi.Flags.DirectionLeftToRight) && state == null))
			{
				return false;
			}
			int num5;
			if ((flags & Bidi.Flags.MaximumHint) != Bidi.Flags.DirectionLeftToRight && cchTextMaxHint > 0 && cchTextMaxHint < cchText)
			{
				if (cchTextMaxHint > 1 && (charBuffer[ichText + cchTextMaxHint - 2] & 'ﰀ') == '\ud800')
				{
					cchTextMaxHint--;
				}
				int i = cchTextMaxHint - 1;
				int num4 = (int)charBuffer[ichText + i];
				num5 = 1;
				if ((num4 & 64512) == 55296)
				{
					num4 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, i, out num5);
				}
				DirectionClass directionClass = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num4)).BiDi;
				i += num5;
				if (Bidi.CharProperty[1, (int)directionClass] == 1)
				{
					while (i < cchText)
					{
						if (i - cchTextMaxHint >= 20)
						{
							break;
						}
						num4 = (int)charBuffer[ichText + i];
						num5 = 1;
						if ((num4 & 64512) == 55296)
						{
							num4 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, i, out num5);
						}
						if (directionClass != Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num4)).BiDi)
						{
							break;
						}
						i += num5;
					}
				}
				else
				{
					while (i < cchText)
					{
						num4 = (int)charBuffer[ichText + i];
						num5 = 1;
						if ((num4 & 64512) == 55296)
						{
							num4 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, i, out num5);
						}
						if (Bidi.CharProperty[1, (int)Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num4)).BiDi] == 1)
						{
							break;
						}
						i += num5;
					}
					i++;
				}
				cchText = Math.Min(cchText, i);
			}
			Bidi.BidiStack bidiStack = new Bidi.BidiStack();
			if ((flags & Bidi.Flags.IncompleteText) != Bidi.Flags.DirectionLeftToRight)
			{
				int num6 = (int)charBuffer[ichText + cchText - 1];
				if (cchText > 1 && (charBuffer[ichText + cchText - 2] & 'ﰀ') == '\ud800' && (charBuffer[ichText + cchText - 1] & 'ﰀ') == '\udc00')
				{
					num6 = 65536 + (int)((int)(charBuffer[ichText + cchText - 2] & 'Ͽ') << 10 | (charBuffer[ichText + cchText - 1] & 'Ͽ'));
				}
				if (DirectionClass.ParagraphSeparator != Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num6)).BiDi)
				{
					state3 = state;
				}
			}
			if ((flags & Bidi.Flags.ContinueAnalysis) != Bidi.Flags.DirectionLeftToRight)
			{
				int num6 = (int)charBuffer[ichText];
				if (cchText > 1 && (charBuffer[ichText] & 'ﰀ') == '\ud800' && (charBuffer[ichText + 1] & 'ﰀ') == '\udc00')
				{
					num6 = 65536 + (int)((int)(charBuffer[ichText] & 'Ͽ') << 10 | (charBuffer[ichText + 1] & 'Ͽ'));
				}
				DirectionClass directionClass = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num6)).BiDi;
				state2 = state;
				switch (directionClass)
				{
				case DirectionClass.Left:
				case DirectionClass.Right:
				case DirectionClass.ArabicNumber:
				case DirectionClass.ArabicLetter:
					state2.LastNumberClass = directionClass;
					state2.LastStrongClass = directionClass;
					break;
				case DirectionClass.EuropeanNumber:
					state2.LastNumberClass = directionClass;
					break;
				}
			}
			byte b;
			ushort num7;
			ulong num8;
			Bidi.OverrideClass overrideClass;
			if (state2 != null)
			{
				if (!bidiStack.Init(state2.LevelStack))
				{
					cchResolved = 0;
					return false;
				}
				b = bidiStack.GetCurrentLevel();
				num7 = state2.Overflow;
				num8 = state2.OverrideLevels;
				overrideClass = (Bidi.Helper.IsBitSet(num8, b) ? (Bidi.Helper.IsOdd(b) ? Bidi.OverrideClass.OverrideClassRight : Bidi.OverrideClass.OverrideClassLeft) : Bidi.OverrideClass.OverrideClassNeutral);
			}
			else
			{
				b = 0;
				if ((flags & Bidi.Flags.FirstStrongAsBaseDirection) != Bidi.Flags.DirectionLeftToRight)
				{
					DirectionClass directionClass2 = DirectionClass.ClassInvalid;
					if (Bidi.GetFirstStrongCharacter(charBuffer, ichText, cchText, ref directionClass2) && directionClass2 != DirectionClass.Left)
					{
						b = 1;
					}
				}
				else if ((flags & Bidi.Flags.DirectionRightToLeft) != Bidi.Flags.DirectionLeftToRight)
				{
					b = 1;
				}
				bidiStack.Init((ulong)b + 1UL);
				num7 = 0;
				num8 = 0UL;
				overrideClass = Bidi.OverrideClass.OverrideClassNeutral;
			}
			byte stackBottom = bidiStack.GetStackBottom();
			int j = -1;
			byte b2;
			byte b3;
			byte b5;
			byte b4;
			DirectionClass directionClass3;
			if (Bidi.Helper.IsOdd(b))
			{
				b2 = b;
				b3 = b + 1;
				b4 = (b5 = 3);
				if (state2 != null)
				{
					directionClass3 = state2.LastStrongClass;
				}
				else
				{
					directionClass3 = DirectionClass.Right;
				}
			}
			else
			{
				b3 = b;
				b2 = b + 1;
				b4 = (b5 = 2);
				if (state2 != null)
				{
					directionClass3 = state2.LastStrongClass;
				}
				else
				{
					directionClass3 = DirectionClass.Left;
				}
			}
			if (state2 != null && (Bidi.FastPathClass[(int)directionClass3] & 2) == 2)
			{
				b5 = Bidi.FastPathClass[(int)directionClass3];
			}
			DirectionClass directionClass4 = DirectionClass.GenericNeutral;
			int k = 0;
			num5 = 1;
			int l = k;
			while (l < cchText)
			{
				int num9 = (int)charBuffer[ichText + l];
				if ((num9 & 64512) == 55296)
				{
					num9 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, k, out num5);
				}
				if (num9 != (int)Bidi.CharHidden)
				{
					directionClass4 = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num9)).BiDi;
					if (directionClass4 == DirectionClass.EuropeanNumber && (flags & Bidi.Flags.OverrideEuropeanNumberResolution) != Bidi.Flags.DirectionLeftToRight)
					{
						directionClass4 = characterClass[l];
					}
					IL_57A:
					while (k < cchText)
					{
						num5 = 1;
						int num6 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, k, out num5);
						DirectionClass directionClass = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num6)).BiDi;
						if (num6 == (int)Bidi.CharHidden)
						{
							directionClass = directionClass4;
						}
						if (Bidi.FastPathClass[(int)directionClass] == 0)
						{
							break;
						}
						characterClass[k] = directionClass;
						directionClass4 = directionClass;
						if (Bidi.FastPathClass[(int)directionClass] == 1)
						{
							if (directionClass != DirectionClass.EuropeanSeparator && directionClass != DirectionClass.CommonSeparator)
							{
								characterClass[k] = DirectionClass.GenericNeutral;
							}
							if (j == -1)
							{
								j = k;
							}
						}
						else
						{
							if (j != -1)
							{
								byte value;
								if (b5 != Bidi.FastPathClass[(int)directionClass])
								{
									value = b;
								}
								else
								{
									value = ((b5 == 2) ? b3 : b2);
								}
								while (j < k)
								{
									levels[j] = value;
									j++;
								}
								j = -1;
							}
							b5 = Bidi.FastPathClass[(int)directionClass];
							levels[k] = ((b5 == 2) ? b3 : b2);
							if (num5 == 2)
							{
								levels[k + 1] = levels[k];
							}
							directionClass3 = directionClass;
						}
						k += num5;
					}
					if (k < cchText)
					{
						for (int m = 0; m < k; m++)
						{
							levels[m] = b;
						}
						byte value2 = b;
						int[] array = new int[cchText];
						while (k < cchText)
						{
							int num10 = (int)charBuffer[ichText + k];
							num5 = 1;
							if ((num10 & 64512) == 55296)
							{
								num10 = DoubleWideChar.GetChar(charBuffer, ichText, cchText, k, out num5);
							}
							DirectionClass directionClass5 = Classification.CharAttributeOf((int)Classification.GetUnicodeClass(num10)).BiDi;
							levels[k] = bidiStack.GetCurrentLevel();
							if (num10 == (int)Bidi.CharHidden)
							{
								directionClass5 = directionClass4;
							}
							switch (directionClass5)
							{
							case DirectionClass.ParagraphSeparator:
								levels[k] = byte.MaxValue;
								array[num3] = k;
								if (k != cchText - 1)
								{
									num3++;
								}
								bidiStack.Init((ulong)b + 1UL);
								num8 = 0UL;
								overrideClass = Bidi.OverrideClass.OverrideClassNeutral;
								num7 = 0;
								goto IL_6C9;
							case DirectionClass.LeftToRightEmbedding:
							case DirectionClass.RightToLeftEmbedding:
								characterClass[k] = DirectionClass.BoundaryNeutral;
								if ((flags & Bidi.Flags.IgnoreDirectionalControls) == Bidi.Flags.DirectionLeftToRight)
								{
									if (!bidiStack.Push(directionClass5 == DirectionClass.LeftToRightEmbedding))
									{
										num7 += 1;
									}
									else
									{
										array[num3] = k;
										if (k != cchText - 1)
										{
											num3++;
										}
										num2++;
									}
									overrideClass = Bidi.OverrideClass.OverrideClassNeutral;
									levels[k] = value2;
								}
								break;
							case DirectionClass.LeftToRightOverride:
							case DirectionClass.RightToLeftOverride:
								characterClass[k] = DirectionClass.BoundaryNeutral;
								if ((flags & Bidi.Flags.IgnoreDirectionalControls) == Bidi.Flags.DirectionLeftToRight)
								{
									if (!bidiStack.Push(directionClass5 == DirectionClass.LeftToRightOverride))
									{
										num7 += 1;
									}
									else
									{
										Bidi.Helper.ResetBit(ref num8, (int)bidiStack.GetCurrentLevel());
										overrideClass = ((directionClass5 == DirectionClass.LeftToRightOverride) ? Bidi.OverrideClass.OverrideClassLeft : Bidi.OverrideClass.OverrideClassRight);
										array[num3] = k;
										if (k != cchText - 1)
										{
											num3++;
										}
										num2++;
									}
									levels[k] = value2;
								}
								break;
							case DirectionClass.PopDirectionalFormat:
								characterClass[k] = DirectionClass.BoundaryNeutral;
								if ((flags & Bidi.Flags.IgnoreDirectionalControls) == Bidi.Flags.DirectionLeftToRight)
								{
									if (num7 != 0)
									{
										num7 -= 1;
									}
									else if (bidiStack.Pop())
									{
										int currentLevel = (int)bidiStack.GetCurrentLevel();
										overrideClass = (Bidi.Helper.IsBitSet(num8, currentLevel) ? (Bidi.Helper.IsOdd(currentLevel) ? Bidi.OverrideClass.OverrideClassRight : Bidi.OverrideClass.OverrideClassLeft) : Bidi.OverrideClass.OverrideClassNeutral);
										if (num2 > 0)
										{
											num3--;
											num2--;
										}
										else
										{
											array[num3] = k;
											if (k != cchText - 1)
											{
												num3++;
											}
										}
									}
									levels[k] = value2;
								}
								break;
							case DirectionClass.SegmentSeparator:
							case DirectionClass.WhiteSpace:
							case DirectionClass.OtherNeutral:
								goto IL_6C9;
							default:
								num2 = 0;
								if (directionClass5 == DirectionClass.EuropeanNumber && (flags & Bidi.Flags.OverrideEuropeanNumberResolution) != Bidi.Flags.DirectionLeftToRight)
								{
									Invariant.Assert(characterClass[k] == DirectionClass.ArabicNumber || characterClass[k] == DirectionClass.EuropeanNumber);
								}
								else
								{
									characterClass[k] = directionClass5;
								}
								if (overrideClass != Bidi.OverrideClass.OverrideClassNeutral)
								{
									characterClass[k] = ((overrideClass == Bidi.OverrideClass.OverrideClassLeft) ? DirectionClass.Left : DirectionClass.Right);
								}
								if (k > 0 && characterClass[k - 1] == DirectionClass.BoundaryNeutral && levels[k - 1] < levels[k])
								{
									levels[k - 1] = levels[k];
								}
								break;
							}
							IL_8EF:
							value2 = levels[k];
							if (num5 > 1)
							{
								levels[k + 1] = levels[k];
								characterClass[k + 1] = characterClass[k];
							}
							directionClass4 = characterClass[k];
							k += num5;
							continue;
							IL_6C9:
							characterClass[k] = DirectionClass.GenericNeutral;
							if (k > 0 && characterClass[k - 1] == DirectionClass.BoundaryNeutral && levels[k - 1] < levels[k] && levels[k] != 255)
							{
								levels[k - 1] = levels[k];
							}
							num2 = 0;
							goto IL_8EF;
						}
						num3++;
						if (state3 != null)
						{
							state3.LevelStack = bidiStack.GetData();
							state3.OverrideLevels = num8;
							state3.Overflow = num7;
						}
						byte val = b;
						bool flag = false;
						for (k = 0; k < num3; k++)
						{
							bool flag2 = levels[array[k]] == byte.MaxValue;
							if (flag2)
							{
								levels[array[k]] = b;
							}
							int num11 = (k == 0) ? 0 : (array[k - 1] + 1);
							int num12 = (k != num3 - 1) ? (flag2 ? 1 : 0) : 0;
							int num13 = (k == num3 - 1) ? (cchText - num11 - num12) : (array[k] - num11 + 1 - num12);
							bool flag3 = num3 - 1 == k && (flags & Bidi.Flags.IncompleteText) != Bidi.Flags.DirectionLeftToRight && state3 != null;
							bool flag4 = k == 0 && state2 != null;
							DirectionClass sor;
							if (k == 0 || flag)
							{
								sor = (Bidi.Helper.IsOdd(Math.Max(b, levels[num11])) ? DirectionClass.Right : DirectionClass.Left);
							}
							else
							{
								sor = (Bidi.Helper.IsOdd(Math.Max(val, levels[num11])) ? DirectionClass.Right : DirectionClass.Left);
							}
							val = levels[num11];
							DirectionClass eor;
							if (num3 - 1 == k || flag2)
							{
								eor = (Bidi.Helper.IsOdd(Math.Max(levels[num11], b)) ? DirectionClass.Right : DirectionClass.Left);
							}
							else
							{
								int num14 = k + 1;
								while (num14 < num3 - 1 && array[num14] - array[num14 - 1] == 1 && characterClass[array[num14]] == DirectionClass.BoundaryNeutral)
								{
									num14++;
								}
								eor = (Bidi.Helper.IsOdd(Math.Max(levels[num11], levels[array[num14 - 1] + 1])) ? DirectionClass.Right : DirectionClass.Left);
							}
							int num15 = Bidi.ResolveNeutralAndWeak(characterClass, num11, num13, sor, eor, levels[num11], flag4 ? state2 : null, flag3 ? state3 : null, k == 0 && state2 == null && (flags & Bidi.Flags.PreviousStrongIsArabic) > Bidi.Flags.DirectionLeftToRight, flags);
							if (flag3)
							{
								num = num13 - num15;
							}
							Bidi.ResolveImplictLevels(characterClass, charBuffer, ichText, num13 - num, levels, num11, stackBottom);
							flag = flag2;
						}
						cchResolved = cchText - num;
						if ((flags & Bidi.Flags.IncompleteText) != Bidi.Flags.DirectionLeftToRight && state3 == null)
						{
							state.OverrideLevels = 0UL;
							state.Overflow = 0;
							if ((stackBottom & 1) != 0)
							{
								state.LastStrongClass = DirectionClass.Right;
								state.LastNumberClass = DirectionClass.Right;
								state.LevelStack = 2UL;
							}
							else
							{
								state.LastStrongClass = DirectionClass.Left;
								state.LastNumberClass = DirectionClass.Left;
								state.LevelStack = 1UL;
							}
						}
						return true;
					}
					cchResolved = cchText;
					if (state != null)
					{
						state.LastStrongClass = directionClass3;
					}
					if (j != -1)
					{
						if ((flags & Bidi.Flags.IncompleteText) == Bidi.Flags.DirectionLeftToRight)
						{
							byte value;
							if (b5 != b4)
							{
								value = b;
							}
							else
							{
								value = ((b5 == 2) ? b3 : b2);
							}
							while (j < cchText)
							{
								levels[j] = value;
								j++;
							}
						}
						else
						{
							cchResolved = j;
						}
					}
					return true;
				}
				else
				{
					l += num5;
				}
			}
			goto IL_57A;
		}

		// Token: 0x04002171 RID: 8561
		private static readonly Bidi.StateMachineAction[,] Action;

		// Token: 0x04002172 RID: 8562
		private static readonly Bidi.StateMachineState[,] NextState;

		// Token: 0x04002173 RID: 8563
		private static readonly byte[,] ImplictPush;

		// Token: 0x04002174 RID: 8564
		private static readonly byte[,] CharProperty;

		// Token: 0x04002175 RID: 8565
		private static readonly Bidi.StateMachineState[] ClassToState;

		// Token: 0x04002176 RID: 8566
		private static readonly byte[] FastPathClass;

		// Token: 0x04002177 RID: 8567
		private static char CharHidden = char.MaxValue;

		// Token: 0x04002178 RID: 8568
		private const byte ParagraphTerminatorLevel = 255;

		// Token: 0x04002179 RID: 8569
		private const int PositionInvalid = -1;

		// Token: 0x0400217A RID: 8570
		private const byte BaseLevelLeft = 0;

		// Token: 0x0400217B RID: 8571
		private const byte BaseLevelRight = 1;

		// Token: 0x0400217C RID: 8572
		private const uint EmptyStack = 0U;

		// Token: 0x0400217D RID: 8573
		private const uint StackLtr = 1U;

		// Token: 0x0400217E RID: 8574
		private const uint StackRtl = 2U;

		// Token: 0x0400217F RID: 8575
		private const int MaxLevel = 63;

		// Token: 0x020009D1 RID: 2513
		private static class Helper
		{
			// Token: 0x06005B04 RID: 23300 RVA: 0x0016D184 File Offset: 0x0016C584
			public static ulong LeftShift(ulong x, byte y)
			{
				return x << (int)y;
			}

			// Token: 0x06005B05 RID: 23301 RVA: 0x0016D198 File Offset: 0x0016C598
			public static ulong LeftShift(ulong x, int y)
			{
				return x << y;
			}

			// Token: 0x06005B06 RID: 23302 RVA: 0x0016D1AC File Offset: 0x0016C5AC
			public static void SetBit(ref ulong x, byte y)
			{
				x |= Bidi.Helper.LeftShift(1UL, y);
			}

			// Token: 0x06005B07 RID: 23303 RVA: 0x0016D1C8 File Offset: 0x0016C5C8
			public static void ResetBit(ref ulong x, int y)
			{
				x &= ~Bidi.Helper.LeftShift(1UL, y);
			}

			// Token: 0x06005B08 RID: 23304 RVA: 0x0016D1E4 File Offset: 0x0016C5E4
			public static bool IsBitSet(ulong x, byte y)
			{
				return (x & Bidi.Helper.LeftShift(1UL, y)) > 0UL;
			}

			// Token: 0x06005B09 RID: 23305 RVA: 0x0016D200 File Offset: 0x0016C600
			public static bool IsBitSet(ulong x, int y)
			{
				return (x & Bidi.Helper.LeftShift(1UL, y)) > 0UL;
			}

			// Token: 0x06005B0A RID: 23306 RVA: 0x0016D21C File Offset: 0x0016C61C
			public static bool IsOdd(byte x)
			{
				return (x & 1) > 0;
			}

			// Token: 0x06005B0B RID: 23307 RVA: 0x0016D230 File Offset: 0x0016C630
			public static bool IsOdd(int x)
			{
				return (x & 1) != 0;
			}
		}

		// Token: 0x020009D2 RID: 2514
		internal class BidiStack
		{
			// Token: 0x06005B0C RID: 23308 RVA: 0x0016D244 File Offset: 0x0016C644
			public BidiStack()
			{
				this.currentStackLevel = 0;
			}

			// Token: 0x06005B0D RID: 23309 RVA: 0x0016D260 File Offset: 0x0016C660
			public bool Init(ulong initialStack)
			{
				byte maximumLevel = Bidi.BidiStack.GetMaximumLevel(initialStack);
				byte minimumLevel = Bidi.BidiStack.GetMinimumLevel(initialStack);
				if (maximumLevel >= 62 || minimumLevel < 0)
				{
					return false;
				}
				this.stack = initialStack;
				this.currentStackLevel = maximumLevel;
				return true;
			}

			// Token: 0x06005B0E RID: 23310 RVA: 0x0016D298 File Offset: 0x0016C698
			public bool Push(bool pushToGreaterEven)
			{
				byte b;
				if (!Bidi.BidiStack.PushCore(ref this.stack, pushToGreaterEven, this.currentStackLevel, out b))
				{
					return false;
				}
				this.currentStackLevel = b;
				return true;
			}

			// Token: 0x06005B0F RID: 23311 RVA: 0x0016D2C8 File Offset: 0x0016C6C8
			public bool Pop()
			{
				byte b;
				if (!Bidi.BidiStack.PopCore(ref this.stack, this.currentStackLevel, out b))
				{
					return false;
				}
				this.currentStackLevel = b;
				return true;
			}

			// Token: 0x06005B10 RID: 23312 RVA: 0x0016D2F4 File Offset: 0x0016C6F4
			public byte GetStackBottom()
			{
				return Bidi.BidiStack.GetMinimumLevel(this.stack);
			}

			// Token: 0x06005B11 RID: 23313 RVA: 0x0016D30C File Offset: 0x0016C70C
			public byte GetCurrentLevel()
			{
				return this.currentStackLevel;
			}

			// Token: 0x06005B12 RID: 23314 RVA: 0x0016D320 File Offset: 0x0016C720
			public ulong GetData()
			{
				return this.stack;
			}

			// Token: 0x06005B13 RID: 23315 RVA: 0x0016D334 File Offset: 0x0016C734
			internal static bool Push(ref ulong stack, bool pushToGreaterEven, out byte topLevel)
			{
				byte maximumLevel = Bidi.BidiStack.GetMaximumLevel(stack);
				return Bidi.BidiStack.PushCore(ref stack, pushToGreaterEven, maximumLevel, out topLevel);
			}

			// Token: 0x06005B14 RID: 23316 RVA: 0x0016D354 File Offset: 0x0016C754
			internal static bool Pop(ref ulong stack, out byte topLevel)
			{
				byte maximumLevel = Bidi.BidiStack.GetMaximumLevel(stack);
				return Bidi.BidiStack.PopCore(ref stack, maximumLevel, out topLevel);
			}

			// Token: 0x06005B15 RID: 23317 RVA: 0x0016D374 File Offset: 0x0016C774
			internal static byte GetMaximumLevel(ulong inputStack)
			{
				byte result = 0;
				for (int i = 63; i >= 0; i--)
				{
					if (Bidi.Helper.IsBitSet(inputStack, i))
					{
						result = (byte)i;
						break;
					}
				}
				return result;
			}

			// Token: 0x06005B16 RID: 23318 RVA: 0x0016D3A0 File Offset: 0x0016C7A0
			private static bool PushCore(ref ulong stack, bool pushToGreaterEven, byte currentStackLevel, out byte newMaximumLevel)
			{
				newMaximumLevel = (pushToGreaterEven ? Bidi.BidiStack.GreaterEven(currentStackLevel) : Bidi.BidiStack.GreaterOdd(currentStackLevel));
				if (newMaximumLevel >= 62)
				{
					newMaximumLevel = currentStackLevel;
					return false;
				}
				Bidi.Helper.SetBit(ref stack, newMaximumLevel);
				return true;
			}

			// Token: 0x06005B17 RID: 23319 RVA: 0x0016D3D4 File Offset: 0x0016C7D4
			private static bool PopCore(ref ulong stack, byte currentStackLevel, out byte newMaximumLevel)
			{
				newMaximumLevel = currentStackLevel;
				if (currentStackLevel == 0 || (currentStackLevel == 1 && (stack & 1UL) == 0UL))
				{
					return false;
				}
				newMaximumLevel = (Bidi.Helper.IsBitSet(stack, (int)(currentStackLevel - 1)) ? (currentStackLevel - 1) : (currentStackLevel - 2));
				Bidi.Helper.ResetBit(ref stack, (int)currentStackLevel);
				return true;
			}

			// Token: 0x06005B18 RID: 23320 RVA: 0x0016D414 File Offset: 0x0016C814
			private static byte GetMinimumLevel(ulong inputStack)
			{
				byte result = byte.MaxValue;
				for (byte b = 0; b <= 63; b += 1)
				{
					if (Bidi.Helper.IsBitSet(inputStack, b))
					{
						result = b;
						break;
					}
				}
				return result;
			}

			// Token: 0x06005B19 RID: 23321 RVA: 0x0016D444 File Offset: 0x0016C844
			private static byte GreaterEven(byte level)
			{
				if (!Bidi.Helper.IsOdd(level))
				{
					return level + 2;
				}
				return level + 1;
			}

			// Token: 0x06005B1A RID: 23322 RVA: 0x0016D464 File Offset: 0x0016C864
			private static byte GreaterOdd(byte level)
			{
				if (!Bidi.Helper.IsOdd(level))
				{
					return level + 1;
				}
				return level + 2;
			}

			// Token: 0x04002E17 RID: 11799
			private const byte EmbeddingLevelInvalid = 62;

			// Token: 0x04002E18 RID: 11800
			private ulong stack;

			// Token: 0x04002E19 RID: 11801
			private byte currentStackLevel;
		}

		// Token: 0x020009D3 RID: 2515
		public enum Flags : uint
		{
			// Token: 0x04002E1B RID: 11803
			DirectionLeftToRight,
			// Token: 0x04002E1C RID: 11804
			DirectionRightToLeft,
			// Token: 0x04002E1D RID: 11805
			FirstStrongAsBaseDirection,
			// Token: 0x04002E1E RID: 11806
			PreviousStrongIsArabic = 4U,
			// Token: 0x04002E1F RID: 11807
			ContinueAnalysis = 8U,
			// Token: 0x04002E20 RID: 11808
			IncompleteText = 16U,
			// Token: 0x04002E21 RID: 11809
			MaximumHint = 32U,
			// Token: 0x04002E22 RID: 11810
			IgnoreDirectionalControls = 64U,
			// Token: 0x04002E23 RID: 11811
			OverrideEuropeanNumberResolution = 128U
		}

		// Token: 0x020009D4 RID: 2516
		private enum OverrideClass
		{
			// Token: 0x04002E25 RID: 11813
			OverrideClassNeutral,
			// Token: 0x04002E26 RID: 11814
			OverrideClassLeft,
			// Token: 0x04002E27 RID: 11815
			OverrideClassRight
		}

		// Token: 0x020009D5 RID: 2517
		private enum StateMachineState
		{
			// Token: 0x04002E29 RID: 11817
			S_L,
			// Token: 0x04002E2A RID: 11818
			S_AL,
			// Token: 0x04002E2B RID: 11819
			S_R,
			// Token: 0x04002E2C RID: 11820
			S_AN,
			// Token: 0x04002E2D RID: 11821
			S_EN,
			// Token: 0x04002E2E RID: 11822
			S_ET,
			// Token: 0x04002E2F RID: 11823
			S_ANfCS,
			// Token: 0x04002E30 RID: 11824
			S_ENfCS,
			// Token: 0x04002E31 RID: 11825
			S_N
		}

		// Token: 0x020009D6 RID: 2518
		private enum StateMachineAction
		{
			// Token: 0x04002E33 RID: 11827
			ST_ST,
			// Token: 0x04002E34 RID: 11828
			ST_ET,
			// Token: 0x04002E35 RID: 11829
			ST_NUMSEP,
			// Token: 0x04002E36 RID: 11830
			ST_N,
			// Token: 0x04002E37 RID: 11831
			SEP_ST,
			// Token: 0x04002E38 RID: 11832
			CS_NUM,
			// Token: 0x04002E39 RID: 11833
			SEP_ET,
			// Token: 0x04002E3A RID: 11834
			SEP_NUMSEP,
			// Token: 0x04002E3B RID: 11835
			SEP_N,
			// Token: 0x04002E3C RID: 11836
			ES_AN,
			// Token: 0x04002E3D RID: 11837
			ET_ET,
			// Token: 0x04002E3E RID: 11838
			ET_NUMSEP,
			// Token: 0x04002E3F RID: 11839
			ET_EN,
			// Token: 0x04002E40 RID: 11840
			ET_N,
			// Token: 0x04002E41 RID: 11841
			NUM_NUMSEP,
			// Token: 0x04002E42 RID: 11842
			NUM_NUM,
			// Token: 0x04002E43 RID: 11843
			EN_L,
			// Token: 0x04002E44 RID: 11844
			EN_AL,
			// Token: 0x04002E45 RID: 11845
			EN_ET,
			// Token: 0x04002E46 RID: 11846
			EN_N,
			// Token: 0x04002E47 RID: 11847
			BN_ST,
			// Token: 0x04002E48 RID: 11848
			NSM_ST,
			// Token: 0x04002E49 RID: 11849
			NSM_ET,
			// Token: 0x04002E4A RID: 11850
			N_ST,
			// Token: 0x04002E4B RID: 11851
			N_ET
		}

		// Token: 0x020009D7 RID: 2519
		internal class State
		{
			// Token: 0x06005B1B RID: 23323 RVA: 0x0016D484 File Offset: 0x0016C884
			public State(bool isRightToLeft)
			{
				this.OverrideLevels = 0UL;
				this.Overflow = 0;
				this.NumberClass = DirectionClass.Left;
				this.StrongCharClass = DirectionClass.Left;
				this.LevelStack = (isRightToLeft ? 2UL : 1UL);
			}

			// Token: 0x17001289 RID: 4745
			// (get) Token: 0x06005B1C RID: 23324 RVA: 0x0016D4C4 File Offset: 0x0016C8C4
			// (set) Token: 0x06005B1D RID: 23325 RVA: 0x0016D4D8 File Offset: 0x0016C8D8
			public virtual DirectionClass LastStrongClass
			{
				get
				{
					return this.StrongCharClass;
				}
				set
				{
					this.StrongCharClass = value;
				}
			}

			// Token: 0x1700128A RID: 4746
			// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0016D4EC File Offset: 0x0016C8EC
			// (set) Token: 0x06005B1F RID: 23327 RVA: 0x0016D500 File Offset: 0x0016C900
			public virtual DirectionClass LastNumberClass
			{
				get
				{
					return this.NumberClass;
				}
				set
				{
					this.NumberClass = value;
				}
			}

			// Token: 0x1700128B RID: 4747
			// (get) Token: 0x06005B20 RID: 23328 RVA: 0x0016D514 File Offset: 0x0016C914
			// (set) Token: 0x06005B21 RID: 23329 RVA: 0x0016D528 File Offset: 0x0016C928
			public ulong LevelStack
			{
				get
				{
					return this.m_levelStack;
				}
				set
				{
					this.m_levelStack = value;
				}
			}

			// Token: 0x1700128C RID: 4748
			// (get) Token: 0x06005B22 RID: 23330 RVA: 0x0016D53C File Offset: 0x0016C93C
			// (set) Token: 0x06005B23 RID: 23331 RVA: 0x0016D550 File Offset: 0x0016C950
			public ulong OverrideLevels
			{
				get
				{
					return this.m_overrideLevels;
				}
				set
				{
					this.m_overrideLevels = value;
				}
			}

			// Token: 0x1700128D RID: 4749
			// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0016D564 File Offset: 0x0016C964
			// (set) Token: 0x06005B25 RID: 23333 RVA: 0x0016D578 File Offset: 0x0016C978
			public ushort Overflow
			{
				get
				{
					return this.m_overflow;
				}
				set
				{
					this.m_overflow = value;
				}
			}

			// Token: 0x04002E4C RID: 11852
			private ulong m_levelStack;

			// Token: 0x04002E4D RID: 11853
			private ulong m_overrideLevels;

			// Token: 0x04002E4E RID: 11854
			protected DirectionClass NumberClass;

			// Token: 0x04002E4F RID: 11855
			protected DirectionClass StrongCharClass;

			// Token: 0x04002E50 RID: 11856
			private ushort m_overflow;
		}
	}
}
