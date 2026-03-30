using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media
{
	// Token: 0x0200041E RID: 1054
	internal static class KnownColors
	{
		// Token: 0x06002A3B RID: 10811 RVA: 0x000A9630 File Offset: 0x000A8A30
		static KnownColors()
		{
			Array values = Enum.GetValues(typeof(KnownColor));
			foreach (object obj in values)
			{
				KnownColor knownColor = (KnownColor)obj;
				string key = string.Format("#{0,8:X8}", (uint)knownColor);
				KnownColors.s_knownArgbColors[key] = knownColor;
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000A96CC File Offset: 0x000A8ACC
		public static SolidColorBrush ColorStringToKnownBrush(string s)
		{
			if (s != null)
			{
				KnownColor knownColor = KnownColors.ColorStringToKnownColor(s);
				if (knownColor != KnownColor.UnknownColor)
				{
					return KnownColors.SolidColorBrushFromUint((uint)knownColor);
				}
			}
			return null;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000A96F0 File Offset: 0x000A8AF0
		public static bool IsKnownSolidColorBrush(SolidColorBrush scp)
		{
			Dictionary<uint, SolidColorBrush> obj = KnownColors.s_solidColorBrushCache;
			bool result;
			lock (obj)
			{
				result = KnownColors.s_solidColorBrushCache.ContainsValue(scp);
			}
			return result;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000A9744 File Offset: 0x000A8B44
		public static SolidColorBrush SolidColorBrushFromUint(uint argb)
		{
			SolidColorBrush solidColorBrush = null;
			Dictionary<uint, SolidColorBrush> obj = KnownColors.s_solidColorBrushCache;
			lock (obj)
			{
				if (!KnownColors.s_solidColorBrushCache.TryGetValue(argb, out solidColorBrush))
				{
					solidColorBrush = new SolidColorBrush(Color.FromUInt32(argb));
					solidColorBrush.Freeze();
					KnownColors.s_solidColorBrushCache[argb] = solidColorBrush;
				}
			}
			return solidColorBrush;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000A97BC File Offset: 0x000A8BBC
		internal static string MatchColor(string colorString, out bool isKnownColor, out bool isNumericColor, out bool isContextColor, out bool isScRgbColor)
		{
			string text = colorString.Trim();
			if ((text.Length == 4 || text.Length == 5 || text.Length == 7 || text.Length == 9) && text[0] == '#')
			{
				isNumericColor = true;
				isScRgbColor = false;
				isKnownColor = false;
				isContextColor = false;
				return text;
			}
			isNumericColor = false;
			if (text.StartsWith("sc#", StringComparison.Ordinal))
			{
				isNumericColor = false;
				isScRgbColor = true;
				isKnownColor = false;
				isContextColor = false;
			}
			else
			{
				isScRgbColor = false;
			}
			if (text.StartsWith("ContextColor ", StringComparison.OrdinalIgnoreCase))
			{
				isContextColor = true;
				isScRgbColor = false;
				isKnownColor = false;
				return text;
			}
			isContextColor = false;
			isKnownColor = true;
			return text;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000A9854 File Offset: 0x000A8C54
		internal static KnownColor ColorStringToKnownColor(string colorString)
		{
			if (colorString != null)
			{
				string text = colorString.ToUpper(CultureInfo.InvariantCulture);
				switch (text.Length)
				{
				case 3:
					if (text.Equals("RED"))
					{
						return (KnownColor)4294901760U;
					}
					if (text.Equals("TAN"))
					{
						return (KnownColor)4291998860U;
					}
					break;
				case 4:
				{
					char c = text[0];
					switch (c)
					{
					case 'A':
						if (text.Equals("AQUA"))
						{
							return (KnownColor)4278255615U;
						}
						break;
					case 'B':
						if (text.Equals("BLUE"))
						{
							return (KnownColor)4278190335U;
						}
						break;
					case 'C':
						if (text.Equals("CYAN"))
						{
							return (KnownColor)4278255615U;
						}
						break;
					case 'D':
					case 'E':
					case 'F':
						break;
					case 'G':
						if (text.Equals("GOLD"))
						{
							return (KnownColor)4294956800U;
						}
						if (text.Equals("GRAY"))
						{
							return (KnownColor)4286611584U;
						}
						break;
					default:
						switch (c)
						{
						case 'L':
							if (text.Equals("LIME"))
							{
								return (KnownColor)4278255360U;
							}
							break;
						case 'N':
							if (text.Equals("NAVY"))
							{
								return (KnownColor)4278190208U;
							}
							break;
						case 'P':
							if (text.Equals("PERU"))
							{
								return (KnownColor)4291659071U;
							}
							if (text.Equals("PINK"))
							{
								return (KnownColor)4294951115U;
							}
							if (text.Equals("PLUM"))
							{
								return (KnownColor)4292714717U;
							}
							break;
						case 'S':
							if (text.Equals("SNOW"))
							{
								return (KnownColor)4294966010U;
							}
							break;
						case 'T':
							if (text.Equals("TEAL"))
							{
								return (KnownColor)4278222976U;
							}
							break;
						}
						break;
					}
					break;
				}
				case 5:
				{
					char c2 = text[0];
					switch (c2)
					{
					case 'A':
						if (text.Equals("AZURE"))
						{
							return (KnownColor)4293984255U;
						}
						break;
					case 'B':
						if (text.Equals("BEIGE"))
						{
							return (KnownColor)4294309340U;
						}
						if (text.Equals("BLACK"))
						{
							return (KnownColor)4278190080U;
						}
						if (text.Equals("BROWN"))
						{
							return (KnownColor)4289014314U;
						}
						break;
					case 'C':
						if (text.Equals("CORAL"))
						{
							return (KnownColor)4294934352U;
						}
						break;
					case 'D':
					case 'E':
					case 'F':
					case 'H':
					case 'J':
					case 'M':
					case 'N':
						break;
					case 'G':
						if (text.Equals("GREEN"))
						{
							return (KnownColor)4278222848U;
						}
						break;
					case 'I':
						if (text.Equals("IVORY"))
						{
							return (KnownColor)4294967280U;
						}
						break;
					case 'K':
						if (text.Equals("KHAKI"))
						{
							return (KnownColor)4293977740U;
						}
						break;
					case 'L':
						if (text.Equals("LINEN"))
						{
							return (KnownColor)4294635750U;
						}
						break;
					case 'O':
						if (text.Equals("OLIVE"))
						{
							return (KnownColor)4286611456U;
						}
						break;
					default:
						if (c2 == 'W')
						{
							if (text.Equals("WHEAT"))
							{
								return (KnownColor)4294303411U;
							}
							if (text.Equals("WHITE"))
							{
								return (KnownColor)4294967295U;
							}
						}
						break;
					}
					break;
				}
				case 6:
				{
					char c3 = text[0];
					if (c3 != 'B')
					{
						if (c3 != 'I')
						{
							switch (c3)
							{
							case 'M':
								if (text.Equals("MAROON"))
								{
									return (KnownColor)4286578688U;
								}
								break;
							case 'O':
								if (text.Equals("ORANGE"))
								{
									return (KnownColor)4294944000U;
								}
								if (text.Equals("ORCHID"))
								{
									return (KnownColor)4292505814U;
								}
								break;
							case 'P':
								if (text.Equals("PURPLE"))
								{
									return (KnownColor)4286578816U;
								}
								break;
							case 'S':
								if (text.Equals("SALMON"))
								{
									return (KnownColor)4294606962U;
								}
								if (text.Equals("SIENNA"))
								{
									return (KnownColor)4288696877U;
								}
								if (text.Equals("SILVER"))
								{
									return (KnownColor)4290822336U;
								}
								break;
							case 'T':
								if (text.Equals("TOMATO"))
								{
									return (KnownColor)4294927175U;
								}
								break;
							case 'V':
								if (text.Equals("VIOLET"))
								{
									return (KnownColor)4293821166U;
								}
								break;
							case 'Y':
								if (text.Equals("YELLOW"))
								{
									return (KnownColor)4294967040U;
								}
								break;
							}
						}
						else if (text.Equals("INDIGO"))
						{
							return (KnownColor)4283105410U;
						}
					}
					else if (text.Equals("BISQUE"))
					{
						return (KnownColor)4294960324U;
					}
					break;
				}
				case 7:
				{
					char c4 = text[0];
					if (c4 <= 'M')
					{
						switch (c4)
						{
						case 'C':
							if (text.Equals("CRIMSON"))
							{
								return (KnownColor)4292613180U;
							}
							break;
						case 'D':
							if (text.Equals("DARKRED"))
							{
								return (KnownColor)4287299584U;
							}
							if (text.Equals("DIMGRAY"))
							{
								return (KnownColor)4285098345U;
							}
							break;
						case 'E':
						case 'G':
							break;
						case 'F':
							if (text.Equals("FUCHSIA"))
							{
								return (KnownColor)4294902015U;
							}
							break;
						case 'H':
							if (text.Equals("HOTPINK"))
							{
								return (KnownColor)4294928820U;
							}
							break;
						default:
							if (c4 == 'M')
							{
								if (text.Equals("MAGENTA"))
								{
									return (KnownColor)4294902015U;
								}
							}
							break;
						}
					}
					else if (c4 != 'O')
					{
						if (c4 != 'S')
						{
							if (c4 == 'T')
							{
								if (text.Equals("THISTLE"))
								{
									return (KnownColor)4292394968U;
								}
							}
						}
						else if (text.Equals("SKYBLUE"))
						{
							return (KnownColor)4287090411U;
						}
					}
					else if (text.Equals("OLDLACE"))
					{
						return (KnownColor)4294833638U;
					}
					break;
				}
				case 8:
				{
					char c5 = text[0];
					if (c5 <= 'H')
					{
						if (c5 != 'C')
						{
							if (c5 != 'D')
							{
								if (c5 == 'H')
								{
									if (text.Equals("HONEYDEW"))
									{
										return (KnownColor)4293984240U;
									}
								}
							}
							else
							{
								if (text.Equals("DARKBLUE"))
								{
									return (KnownColor)4278190219U;
								}
								if (text.Equals("DARKCYAN"))
								{
									return (KnownColor)4278225803U;
								}
								if (text.Equals("DARKGRAY"))
								{
									return (KnownColor)4289309097U;
								}
								if (text.Equals("DEEPPINK"))
								{
									return (KnownColor)4294907027U;
								}
							}
						}
						else if (text.Equals("CORNSILK"))
						{
							return (KnownColor)4294965468U;
						}
					}
					else if (c5 != 'L')
					{
						if (c5 != 'M')
						{
							if (c5 == 'S')
							{
								if (text.Equals("SEAGREEN"))
								{
									return (KnownColor)4281240407U;
								}
								if (text.Equals("SEASHELL"))
								{
									return (KnownColor)4294964718U;
								}
							}
						}
						else if (text.Equals("MOCCASIN"))
						{
							return (KnownColor)4294960309U;
						}
					}
					else if (text.Equals("LAVENDER"))
					{
						return (KnownColor)4293322490U;
					}
					break;
				}
				case 9:
					switch (text[0])
					{
					case 'A':
						if (text.Equals("ALICEBLUE"))
						{
							return (KnownColor)4293982463U;
						}
						break;
					case 'B':
						if (text.Equals("BURLYWOOD"))
						{
							return (KnownColor)4292786311U;
						}
						break;
					case 'C':
						if (text.Equals("CADETBLUE"))
						{
							return (KnownColor)4284456608U;
						}
						if (text.Equals("CHOCOLATE"))
						{
							return (KnownColor)4291979550U;
						}
						break;
					case 'D':
						if (text.Equals("DARKGREEN"))
						{
							return (KnownColor)4278215680U;
						}
						if (text.Equals("DARKKHAKI"))
						{
							return (KnownColor)4290623339U;
						}
						break;
					case 'F':
						if (text.Equals("FIREBRICK"))
						{
							return (KnownColor)4289864226U;
						}
						break;
					case 'G':
						if (text.Equals("GAINSBORO"))
						{
							return (KnownColor)4292664540U;
						}
						if (text.Equals("GOLDENROD"))
						{
							return (KnownColor)4292519200U;
						}
						break;
					case 'I':
						if (text.Equals("INDIANRED"))
						{
							return (KnownColor)4291648604U;
						}
						break;
					case 'L':
						if (text.Equals("LAWNGREEN"))
						{
							return (KnownColor)4286381056U;
						}
						if (text.Equals("LIGHTBLUE"))
						{
							return (KnownColor)4289583334U;
						}
						if (text.Equals("LIGHTCYAN"))
						{
							return (KnownColor)4292935679U;
						}
						if (text.Equals("LIGHTGRAY"))
						{
							return (KnownColor)4292072403U;
						}
						if (text.Equals("LIGHTPINK"))
						{
							return (KnownColor)4294948545U;
						}
						if (text.Equals("LIMEGREEN"))
						{
							return (KnownColor)4281519410U;
						}
						break;
					case 'M':
						if (text.Equals("MINTCREAM"))
						{
							return (KnownColor)4294311930U;
						}
						if (text.Equals("MISTYROSE"))
						{
							return (KnownColor)4294960353U;
						}
						break;
					case 'O':
						if (text.Equals("OLIVEDRAB"))
						{
							return (KnownColor)4285238819U;
						}
						if (text.Equals("ORANGERED"))
						{
							return (KnownColor)4294919424U;
						}
						break;
					case 'P':
						if (text.Equals("PALEGREEN"))
						{
							return (KnownColor)4288215960U;
						}
						if (text.Equals("PEACHPUFF"))
						{
							return (KnownColor)4294957753U;
						}
						break;
					case 'R':
						if (text.Equals("ROSYBROWN"))
						{
							return (KnownColor)4290547599U;
						}
						if (text.Equals("ROYALBLUE"))
						{
							return (KnownColor)4282477025U;
						}
						break;
					case 'S':
						if (text.Equals("SLATEBLUE"))
						{
							return (KnownColor)4285160141U;
						}
						if (text.Equals("SLATEGRAY"))
						{
							return (KnownColor)4285563024U;
						}
						if (text.Equals("STEELBLUE"))
						{
							return (KnownColor)4282811060U;
						}
						break;
					case 'T':
						if (text.Equals("TURQUOISE"))
						{
							return (KnownColor)4282441936U;
						}
						break;
					}
					break;
				case 10:
				{
					char c6 = text[0];
					if (c6 <= 'P')
					{
						switch (c6)
						{
						case 'A':
							if (text.Equals("AQUAMARINE"))
							{
								return (KnownColor)4286578644U;
							}
							break;
						case 'B':
							if (text.Equals("BLUEVIOLET"))
							{
								return (KnownColor)4287245282U;
							}
							break;
						case 'C':
							if (text.Equals("CHARTREUSE"))
							{
								return (KnownColor)4286578432U;
							}
							break;
						case 'D':
							if (text.Equals("DARKORANGE"))
							{
								return (KnownColor)4294937600U;
							}
							if (text.Equals("DARKORCHID"))
							{
								return (KnownColor)4288230092U;
							}
							if (text.Equals("DARKSALMON"))
							{
								return (KnownColor)4293498490U;
							}
							if (text.Equals("DARKVIOLET"))
							{
								return (KnownColor)4287889619U;
							}
							if (text.Equals("DODGERBLUE"))
							{
								return (KnownColor)4280193279U;
							}
							break;
						case 'E':
						case 'F':
						case 'H':
						case 'I':
						case 'J':
						case 'K':
							break;
						case 'G':
							if (text.Equals("GHOSTWHITE"))
							{
								return (KnownColor)4294506751U;
							}
							break;
						case 'L':
							if (text.Equals("LIGHTCORAL"))
							{
								return (KnownColor)4293951616U;
							}
							if (text.Equals("LIGHTGREEN"))
							{
								return (KnownColor)4287688336U;
							}
							break;
						case 'M':
							if (text.Equals("MEDIUMBLUE"))
							{
								return (KnownColor)4278190285U;
							}
							break;
						default:
							if (c6 == 'P')
							{
								if (text.Equals("PAPAYAWHIP"))
								{
									return (KnownColor)4294963157U;
								}
								if (text.Equals("POWDERBLUE"))
								{
									return (KnownColor)4289781990U;
								}
							}
							break;
						}
					}
					else if (c6 != 'S')
					{
						if (c6 == 'W')
						{
							if (text.Equals("WHITESMOKE"))
							{
								return (KnownColor)4294309365U;
							}
						}
					}
					else if (text.Equals("SANDYBROWN"))
					{
						return (KnownColor)4294222944U;
					}
					break;
				}
				case 11:
				{
					char c7 = text[0];
					if (c7 <= 'N')
					{
						switch (c7)
						{
						case 'D':
							if (text.Equals("DARKMAGENTA"))
							{
								return (KnownColor)4287299723U;
							}
							if (text.Equals("DEEPSKYBLUE"))
							{
								return (KnownColor)4278239231U;
							}
							break;
						case 'E':
							break;
						case 'F':
							if (text.Equals("FLORALWHITE"))
							{
								return (KnownColor)4294966000U;
							}
							if (text.Equals("FORESTGREEN"))
							{
								return (KnownColor)4280453922U;
							}
							break;
						case 'G':
							if (text.Equals("GREENYELLOW"))
							{
								return (KnownColor)4289593135U;
							}
							break;
						default:
							if (c7 != 'L')
							{
								if (c7 == 'N')
								{
									if (text.Equals("NAVAJOWHITE"))
									{
										return (KnownColor)4294958765U;
									}
								}
							}
							else
							{
								if (text.Equals("LIGHTSALMON"))
								{
									return (KnownColor)4294942842U;
								}
								if (text.Equals("LIGHTYELLOW"))
								{
									return (KnownColor)4294967264U;
								}
							}
							break;
						}
					}
					else if (c7 != 'S')
					{
						if (c7 != 'T')
						{
							if (c7 == 'Y')
							{
								if (text.Equals("YELLOWGREEN"))
								{
									return (KnownColor)4288335154U;
								}
							}
						}
						else if (text.Equals("TRANSPARENT"))
						{
							return KnownColor.Transparent;
						}
					}
					else
					{
						if (text.Equals("SADDLEBROWN"))
						{
							return (KnownColor)4287317267U;
						}
						if (text.Equals("SPRINGGREEN"))
						{
							return (KnownColor)4278255487U;
						}
					}
					break;
				}
				case 12:
				{
					char c8 = text[0];
					if (c8 <= 'D')
					{
						if (c8 != 'A')
						{
							if (c8 == 'D')
							{
								if (text.Equals("DARKSEAGREEN"))
								{
									return (KnownColor)4287609999U;
								}
							}
						}
						else if (text.Equals("ANTIQUEWHITE"))
						{
							return (KnownColor)4294634455U;
						}
					}
					else if (c8 != 'L')
					{
						if (c8 == 'M')
						{
							if (text.Equals("MEDIUMORCHID"))
							{
								return (KnownColor)4290401747U;
							}
							if (text.Equals("MEDIUMPURPLE"))
							{
								return (KnownColor)4287852763U;
							}
							if (text.Equals("MIDNIGHTBLUE"))
							{
								return (KnownColor)4279834992U;
							}
						}
					}
					else
					{
						if (text.Equals("LIGHTSKYBLUE"))
						{
							return (KnownColor)4287090426U;
						}
						if (text.Equals("LEMONCHIFFON"))
						{
							return (KnownColor)4294965965U;
						}
					}
					break;
				}
				case 13:
				{
					char c9 = text[0];
					if (c9 != 'D')
					{
						if (c9 != 'L')
						{
							if (c9 == 'P')
							{
								if (text.Equals("PALEGOLDENROD"))
								{
									return (KnownColor)4293847210U;
								}
								if (text.Equals("PALETURQUOISE"))
								{
									return (KnownColor)4289720046U;
								}
								if (text.Equals("PALEVIOLETRED"))
								{
									return (KnownColor)4292571283U;
								}
							}
						}
						else
						{
							if (text.Equals("LIGHTSEAGREEN"))
							{
								return (KnownColor)4280332970U;
							}
							if (text.Equals("LAVENDERBLUSH"))
							{
								return (KnownColor)4294963445U;
							}
						}
					}
					else
					{
						if (text.Equals("DARKSLATEBLUE"))
						{
							return (KnownColor)4282924427U;
						}
						if (text.Equals("DARKSLATEGRAY"))
						{
							return (KnownColor)4281290575U;
						}
						if (text.Equals("DARKGOLDENROD"))
						{
							return (KnownColor)4290283019U;
						}
						if (text.Equals("DARKTURQUOISE"))
						{
							return (KnownColor)4278243025U;
						}
					}
					break;
				}
				case 14:
				{
					char c10 = text[0];
					switch (c10)
					{
					case 'B':
						if (text.Equals("BLANCHEDALMOND"))
						{
							return (KnownColor)4294962125U;
						}
						break;
					case 'C':
						if (text.Equals("CORNFLOWERBLUE"))
						{
							return (KnownColor)4284782061U;
						}
						break;
					case 'D':
						if (text.Equals("DARKOLIVEGREEN"))
						{
							return (KnownColor)4283788079U;
						}
						break;
					default:
						if (c10 != 'L')
						{
							if (c10 == 'M')
							{
								if (text.Equals("MEDIUMSEAGREEN"))
								{
									return (KnownColor)4282168177U;
								}
							}
						}
						else
						{
							if (text.Equals("LIGHTSLATEGRAY"))
							{
								return (KnownColor)4286023833U;
							}
							if (text.Equals("LIGHTSTEELBLUE"))
							{
								return (KnownColor)4289774814U;
							}
						}
						break;
					}
					break;
				}
				case 15:
					if (text.Equals("MEDIUMSLATEBLUE"))
					{
						return (KnownColor)4286277870U;
					}
					if (text.Equals("MEDIUMTURQUOISE"))
					{
						return (KnownColor)4282962380U;
					}
					if (text.Equals("MEDIUMVIOLETRED"))
					{
						return (KnownColor)4291237253U;
					}
					break;
				case 16:
					if (text.Equals("MEDIUMAQUAMARINE"))
					{
						return (KnownColor)4284927402U;
					}
					break;
				case 17:
					if (text.Equals("MEDIUMSPRINGGREEN"))
					{
						return (KnownColor)4278254234U;
					}
					break;
				case 20:
					if (text.Equals("LIGHTGOLDENRODYELLOW"))
					{
						return (KnownColor)4294638290U;
					}
					break;
				}
			}
			return KnownColor.UnknownColor;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000AA7A8 File Offset: 0x000A9BA8
		internal static KnownColor ArgbStringToKnownColor(string argbString)
		{
			string key = argbString.Trim().ToUpper(CultureInfo.InvariantCulture);
			KnownColor result;
			if (KnownColors.s_knownArgbColors.TryGetValue(key, out result))
			{
				return result;
			}
			return KnownColor.UnknownColor;
		}

		// Token: 0x04001395 RID: 5013
		private static Dictionary<uint, SolidColorBrush> s_solidColorBrushCache = new Dictionary<uint, SolidColorBrush>();

		// Token: 0x04001396 RID: 5014
		private static Dictionary<string, KnownColor> s_knownArgbColors = new Dictionary<string, KnownColor>();
	}
}
