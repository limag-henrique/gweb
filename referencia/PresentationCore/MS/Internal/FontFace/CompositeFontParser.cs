using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using MS.Internal.PresentationCore;

namespace MS.Internal.FontFace
{
	// Token: 0x02000764 RID: 1892
	internal class CompositeFontParser
	{
		// Token: 0x06004FCC RID: 20428 RVA: 0x0013ED14 File Offset: 0x0013E114
		internal static void VerifyMultiplierOfEm(string propertyName, ref double value)
		{
			if (DoubleUtil.IsNaN(value))
			{
				throw new ArgumentException(SR.Get("PropertyValueCannotBeNaN", new object[]
				{
					propertyName
				}));
			}
			if (value > 100.0)
			{
				value = 100.0;
				return;
			}
			if (value < -100.0)
			{
				value = -100.0;
			}
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0013ED78 File Offset: 0x0013E178
		internal static void VerifyPositiveMultiplierOfEm(string propertyName, ref double value)
		{
			if (DoubleUtil.IsNaN(value))
			{
				throw new ArgumentException(SR.Get("PropertyValueCannotBeNaN", new object[]
				{
					propertyName
				}));
			}
			if (value > 100.0)
			{
				value = 100.0;
				return;
			}
			if (value <= 0.0)
			{
				throw new ArgumentException(SR.Get("PropertyMustBeGreaterThanZero", new object[]
				{
					propertyName
				}));
			}
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0013EDE8 File Offset: 0x0013E1E8
		internal static void VerifyNonNegativeMultiplierOfEm(string propertyName, ref double value)
		{
			if (DoubleUtil.IsNaN(value))
			{
				throw new ArgumentException(SR.Get("PropertyValueCannotBeNaN", new object[]
				{
					propertyName
				}));
			}
			if (value > 100.0)
			{
				value = 100.0;
				return;
			}
			if (value < 0.0)
			{
				throw new ArgumentException(SR.Get("PropertyCannotBeNegative", new object[]
				{
					propertyName
				}));
			}
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0013EE58 File Offset: 0x0013E258
		private double GetAttributeAsDouble()
		{
			object obj = null;
			try
			{
				obj = this._doubleTypeConverter.ConvertFromString(null, TypeConverterHelper.InvariantEnglishUS, this.GetAttributeValue());
			}
			catch (NotSupportedException)
			{
				this.FailAttributeValue();
			}
			if (obj == null)
			{
				this.FailAttributeValue();
			}
			return (double)obj;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0013EEB8 File Offset: 0x0013E2B8
		private XmlLanguage GetAttributeAsXmlLanguage()
		{
			object obj = null;
			try
			{
				obj = this._xmlLanguageTypeConverter.ConvertFromString(null, TypeConverterHelper.InvariantEnglishUS, this.GetAttributeValue());
			}
			catch (NotSupportedException)
			{
				this.FailAttributeValue();
			}
			if (obj == null)
			{
				this.FailAttributeValue();
			}
			return (XmlLanguage)obj;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0013EF18 File Offset: 0x0013E318
		private string GetAttributeValue()
		{
			string text = this._reader.Value;
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			if (text[0] == '{')
			{
				if (text.Length > 1 && text[1] == '}')
				{
					text = text.Substring(2);
				}
				else
				{
					this.FailAttributeValue();
				}
			}
			return text;
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0013EF70 File Offset: 0x0013E370
		internal static CompositeFontInfo LoadXml(Stream fileStream)
		{
			CompositeFontParser compositeFontParser = new CompositeFontParser(fileStream);
			return compositeFontParser._compositeFontInfo;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0013EF8C File Offset: 0x0013E38C
		private CompositeFontParser(Stream fileStream)
		{
			this._compositeFontInfo = new CompositeFontInfo();
			this._namespaceMap = new Hashtable();
			this._doubleTypeConverter = TypeDescriptor.GetConverter(typeof(double));
			this._xmlLanguageTypeConverter = new XmlLanguageConverter();
			this._reader = this.CreateXmlReader(fileStream);
			try
			{
				if (this.IsStartElement("FontFamily", "http://schemas.microsoft.com/winfx/2006/xaml/composite-font"))
				{
					this.ParseFontFamilyElement();
				}
				else if (this.IsStartElement("FontFamilyCollection", "http://schemas.microsoft.com/winfx/2006/xaml/composite-font"))
				{
					this.ParseFontFamilyCollectionElement();
				}
				else
				{
					this.FailUnknownElement();
				}
			}
			catch (XmlException x)
			{
				this.FailNotWellFormed(x);
			}
			catch (XmlSyntaxException x2)
			{
				this.FailNotWellFormed(x2);
			}
			catch (FormatException ex)
			{
				if (this._reader.NodeType == XmlNodeType.Attribute)
				{
					this.FailAttributeValue(ex);
				}
				else
				{
					this.Fail(ex.Message, ex);
				}
			}
			catch (ArgumentException ex2)
			{
				if (this._reader.NodeType == XmlNodeType.Attribute)
				{
					this.FailAttributeValue(ex2);
				}
				else
				{
					this.Fail(ex2.Message, ex2);
				}
			}
			finally
			{
				this._reader.Close();
				this._reader = null;
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0013F114 File Offset: 0x0013E514
		private XmlReader CreateXmlReader(Stream fileStream)
		{
			XmlReader baseReader = XmlReader.Create(fileStream, new XmlReaderSettings
			{
				CloseInput = true,
				IgnoreComments = true,
				IgnoreWhitespace = false,
				ProhibitDtd = true
			});
			return new XmlCompatibilityReader(baseReader, new IsXmlNamespaceSupportedCallback(this.IsXmlNamespaceSupported));
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0013F160 File Offset: 0x0013E560
		private bool IsXmlNamespaceSupported(string xmlNamespace, out string newXmlNamespace)
		{
			newXmlNamespace = null;
			return xmlNamespace == "http://schemas.microsoft.com/winfx/2006/xaml/composite-font" || xmlNamespace == "http://schemas.microsoft.com/winfx/2006/xaml" || this.IsMappedNamespace(xmlNamespace);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0013F194 File Offset: 0x0013E594
		private bool IsStartElement(string localName, string namespaceURI)
		{
			this.MoveToContent();
			return this._reader.IsStartElement(localName, namespaceURI);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0013F1B8 File Offset: 0x0013E5B8
		private XmlNodeType MoveToContent()
		{
			bool flag = false;
			do
			{
				XmlNodeType nodeType = this._reader.NodeType;
				if (nodeType == XmlNodeType.Element || nodeType - XmlNodeType.CDATA <= 1 || nodeType - XmlNodeType.EndElement <= 1)
				{
					flag = true;
				}
			}
			while (!flag && this._reader.Read());
			return this._reader.NodeType;
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0013F204 File Offset: 0x0013E604
		private bool IsMappedNamespace(string xmlNamespace)
		{
			return this._namespaceMap.ContainsKey(xmlNamespace);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0013F220 File Offset: 0x0013E620
		private bool IsSystemNamespace(string xmlNamespace)
		{
			return xmlNamespace == "clr-namespace:System;assembly=mscorlib";
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0013F238 File Offset: 0x0013E638
		private void ParseFontFamilyCollectionElement()
		{
			bool flag = false;
			while (this._reader.Read())
			{
				OperatingSystemVersion osVer;
				if (Enum.TryParse<OperatingSystemVersion>(this._reader.GetAttribute("OS"), out osVer) && OSVersionHelper.IsOsVersionOrGreater(osVer))
				{
					this.ParseFontFamilyElement();
					return;
				}
			}
			if (!flag)
			{
				this.Fail(string.Format("No FontFamily element found in FontFamilyCollection that matches current OS or greater: {0}", OSVersionHelper.GetOsVersion().ToString()));
			}
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0013F2A8 File Offset: 0x0013E6A8
		private void ParseFontFamilyElement()
		{
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.IsCompositeFontAttribute())
					{
						string localName = this._reader.LocalName;
						if (localName == "Baseline")
						{
							this._compositeFontInfo.Baseline = this.GetAttributeAsDouble();
						}
						else if (localName == "LineSpacing")
						{
							this._compositeFontInfo.LineSpacing = this.GetAttributeAsDouble();
						}
						else if (localName != "OS")
						{
							this.FailUnknownAttribute();
						}
					}
					else if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
			if (this._reader.IsEmptyElement)
			{
				this.VerifyCompositeFontInfo();
				this._reader.Read();
				return;
			}
			this._reader.Read();
			while (this.MoveToContent() != XmlNodeType.EndElement)
			{
				if (this._reader.NodeType == XmlNodeType.Element && this._reader.NamespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml/composite-font")
				{
					bool isEmptyElement = this._reader.IsEmptyElement;
					string localName2 = this._reader.LocalName;
					if (!(localName2 == "FontFamily.FamilyNames"))
					{
						if (!(localName2 == "FontFamily.FamilyTypefaces"))
						{
							if (!(localName2 == "FontFamily.FamilyMaps"))
							{
								this.FailUnknownElement();
							}
							else
							{
								this.VerifyNoAttributes();
								this._reader.Read();
								if (!isEmptyElement)
								{
									while (this.IsStartElement("FontFamilyMap", "http://schemas.microsoft.com/winfx/2006/xaml/composite-font"))
									{
										this.ParseFamilyMapElement();
									}
									this._reader.ReadEndElement();
								}
							}
						}
						else
						{
							this.VerifyNoAttributes();
							this._reader.Read();
							if (!isEmptyElement)
							{
								while (this.IsStartElement("FamilyTypeface", "http://schemas.microsoft.com/winfx/2006/xaml/composite-font"))
								{
									this.ParseFamilyTypefaceElement();
								}
								this._reader.ReadEndElement();
							}
						}
					}
					else
					{
						this.VerifyNoAttributes();
						this._reader.Read();
						if (!isEmptyElement)
						{
							while (this.MoveToContent() == XmlNodeType.Element)
							{
								if (this._reader.LocalName == "String" && this.IsSystemNamespace(this._reader.NamespaceURI))
								{
									this.ParseFamilyNameElement();
								}
								else
								{
									this.FailUnknownElement();
								}
							}
							this._reader.ReadEndElement();
						}
					}
				}
				else
				{
					this._reader.Skip();
				}
			}
			this.VerifyCompositeFontInfo();
			this._reader.ReadEndElement();
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0013F518 File Offset: 0x0013E918
		private void VerifyNoAttributes()
		{
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0013F55C File Offset: 0x0013E95C
		private void ParseFamilyNameElement()
		{
			XmlLanguage xmlLanguage = null;
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (this._reader.NamespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml" && this._reader.LocalName == "Key")
					{
						xmlLanguage = this.GetAttributeAsXmlLanguage();
					}
					else if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
			if (xmlLanguage == null)
			{
				this.FailMissingAttribute("Language");
			}
			string value = this._reader.ReadElementString();
			if (string.IsNullOrEmpty(value))
			{
				this.FailMissingAttribute("Name");
			}
			this._compositeFontInfo.FamilyNames.Add(xmlLanguage, value);
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0013F618 File Offset: 0x0013EA18
		private void ParseFamilyTypefaceElement()
		{
			FamilyTypeface familyTypeface = new FamilyTypeface();
			this.ParseFamilyTypefaceAttributes(familyTypeface);
			if (this._reader.IsEmptyElement)
			{
				this._reader.Read();
			}
			else
			{
				this._reader.Read();
				while (this.MoveToContent() != XmlNodeType.EndElement)
				{
					if (this._reader.NodeType == XmlNodeType.Element && this._reader.NamespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml/composite-font")
					{
						if (this._reader.LocalName == "FamilyTypeface.DeviceFontCharacterMetrics")
						{
							this.VerifyNoAttributes();
							if (this._reader.IsEmptyElement)
							{
								this._reader.Read();
							}
							else
							{
								this._reader.Read();
								while (this.MoveToContent() == XmlNodeType.Element)
								{
									if (this._reader.LocalName == "CharacterMetrics")
									{
										this.ParseCharacterMetricsElement(familyTypeface);
									}
									else
									{
										this.FailUnknownElement();
									}
								}
								this._reader.ReadEndElement();
							}
						}
						else
						{
							this.FailUnknownElement();
						}
					}
					else
					{
						this._reader.Skip();
					}
				}
				this._reader.ReadEndElement();
			}
			this._compositeFontInfo.GetFamilyTypefaceList().Add(familyTypeface);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0013F74C File Offset: 0x0013EB4C
		private void ParseFamilyTypefaceAttributes(FamilyTypeface face)
		{
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.IsCompositeFontAttribute())
					{
						string localName = this._reader.LocalName;
						if (localName == "Style")
						{
							FontStyle style = default(FontStyle);
							if (!FontStyles.FontStyleStringToKnownStyle(this.GetAttributeValue(), CultureInfo.InvariantCulture, ref style))
							{
								this.FailAttributeValue();
							}
							face.Style = style;
						}
						else if (localName == "Weight")
						{
							FontWeight weight = default(FontWeight);
							if (!FontWeights.FontWeightStringToKnownWeight(this.GetAttributeValue(), CultureInfo.InvariantCulture, ref weight))
							{
								this.FailAttributeValue();
							}
							face.Weight = weight;
						}
						else if (localName == "Stretch")
						{
							FontStretch stretch = default(FontStretch);
							if (!FontStretches.FontStretchStringToKnownStretch(this.GetAttributeValue(), CultureInfo.InvariantCulture, ref stretch))
							{
								this.FailAttributeValue();
							}
							face.Stretch = stretch;
						}
						else if (localName == "UnderlinePosition")
						{
							face.UnderlinePosition = this.GetAttributeAsDouble();
						}
						else if (localName == "UnderlineThickness")
						{
							face.UnderlineThickness = this.GetAttributeAsDouble();
						}
						else if (localName == "StrikethroughPosition")
						{
							face.StrikethroughPosition = this.GetAttributeAsDouble();
						}
						else if (localName == "StrikethroughThickness")
						{
							face.StrikethroughThickness = this.GetAttributeAsDouble();
						}
						else if (localName == "CapsHeight")
						{
							face.CapsHeight = this.GetAttributeAsDouble();
						}
						else if (localName == "XHeight")
						{
							face.XHeight = this.GetAttributeAsDouble();
						}
						else if (localName == "DeviceFontName")
						{
							face.DeviceFontName = this.GetAttributeValue();
						}
						else
						{
							this.FailUnknownAttribute();
						}
					}
					else if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0013F92C File Offset: 0x0013ED2C
		private void ParseCharacterMetricsElement(FamilyTypeface face)
		{
			string text = null;
			string text2 = null;
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (this._reader.NamespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml" && this._reader.LocalName == "Key")
					{
						text = this.GetAttributeValue();
					}
					else if (this.IsCompositeFontAttribute() && this._reader.LocalName == "Metrics")
					{
						text2 = this.GetAttributeValue();
					}
					else if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
			if (text == null)
			{
				this.FailMissingAttribute("Key");
			}
			if (text2 == null)
			{
				this.FailMissingAttribute("Metrics");
			}
			face.DeviceFontCharacterMetrics.Add(CharacterMetricsDictionary.ConvertKey(text), new CharacterMetrics(text2));
			this.ParseEmptyElement();
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0013FA0C File Offset: 0x0013EE0C
		private void ParseFamilyMapElement()
		{
			FontFamilyMap fontFamilyMap = new FontFamilyMap();
			if (this._reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.IsCompositeFontAttribute())
					{
						string localName = this._reader.LocalName;
						if (localName == "Unicode")
						{
							fontFamilyMap.Unicode = this.GetAttributeValue();
						}
						else if (localName == "Target")
						{
							fontFamilyMap.Target = this.GetAttributeValue();
						}
						else if (localName == "Scale")
						{
							fontFamilyMap.Scale = this.GetAttributeAsDouble();
						}
						else if (localName == "Language")
						{
							fontFamilyMap.Language = this.GetAttributeAsXmlLanguage();
						}
						else
						{
							this.FailUnknownAttribute();
						}
					}
					else if (!this.IsIgnorableAttribute())
					{
						this.FailUnknownAttribute();
					}
				}
				while (this._reader.MoveToNextAttribute());
				this._reader.MoveToElement();
			}
			this._compositeFontInfo.FamilyMaps.Add(fontFamilyMap);
			this.ParseEmptyElement();
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0013FAFC File Offset: 0x0013EEFC
		private void ParseEmptyElement()
		{
			if (this._reader.IsEmptyElement)
			{
				this._reader.Read();
				return;
			}
			this._reader.Read();
			while (this.MoveToContent() != XmlNodeType.EndElement)
			{
				if (this._reader.NodeType == XmlNodeType.Element && this._reader.NamespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml/composite-font")
				{
					this.FailUnknownElement();
				}
				else
				{
					this._reader.Skip();
				}
			}
			this._reader.ReadEndElement();
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0013FB80 File Offset: 0x0013EF80
		private bool IsCompositeFontAttribute()
		{
			string namespaceURI = this._reader.NamespaceURI;
			return string.IsNullOrEmpty(namespaceURI) || namespaceURI == "http://schemas.microsoft.com/winfx/2006/xaml/composite-font";
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0013FBB0 File Offset: 0x0013EFB0
		private bool IsIgnorableAttribute()
		{
			string namespaceURI = this._reader.NamespaceURI;
			return namespaceURI == "http://www.w3.org/XML/1998/namespace" || namespaceURI == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0013FBE4 File Offset: 0x0013EFE4
		private void VerifyCompositeFontInfo()
		{
			if (this._compositeFontInfo.FamilyMaps.Count == 0)
			{
				this.Fail(SR.Get("CompositeFontMissingElement", new object[]
				{
					"FontFamilyMap"
				}));
			}
			if (this._compositeFontInfo.FamilyNames.Count == 0)
			{
				this.Fail(SR.Get("CompositeFontMissingElement", new object[]
				{
					"String"
				}));
			}
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0013FC54 File Offset: 0x0013F054
		private void FailNotWellFormed(Exception x)
		{
			throw new FileFormatException(new Uri(this._reader.BaseURI, UriKind.RelativeOrAbsolute), x);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0013FC78 File Offset: 0x0013F078
		private void FailAttributeValue()
		{
			this.Fail(SR.Get("CompositeFontAttributeValue1", new object[]
			{
				this._reader.LocalName
			}));
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0013FCAC File Offset: 0x0013F0AC
		private void FailAttributeValue(Exception x)
		{
			this.Fail(SR.Get("CompositeFontAttributeValue2", new object[]
			{
				this._reader.LocalName,
				x.Message
			}), x);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0013FCE8 File Offset: 0x0013F0E8
		private void FailUnknownElement()
		{
			this.Fail(SR.Get("CompositeFontUnknownElement", new object[]
			{
				this._reader.LocalName,
				this._reader.NamespaceURI
			}));
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0013FD28 File Offset: 0x0013F128
		private void FailUnknownAttribute()
		{
			this.Fail(SR.Get("CompositeFontUnknownAttribute", new object[]
			{
				this._reader.LocalName,
				this._reader.NamespaceURI
			}));
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0013FD68 File Offset: 0x0013F168
		private void FailMissingAttribute(string name)
		{
			this.Fail(SR.Get("CompositeFontMissingAttribute", new object[]
			{
				name
			}));
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0013FD90 File Offset: 0x0013F190
		private void Fail(string message)
		{
			this.Fail(message, null);
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0013FDA8 File Offset: 0x0013F1A8
		private void Fail(string message, Exception innerException)
		{
			string baseURI = this._reader.BaseURI;
			throw new FileFormatException(new Uri(baseURI, UriKind.RelativeOrAbsolute), message, innerException);
		}

		// Token: 0x04002436 RID: 9270
		private const NumberStyles UnsignedDecimalPointStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint;

		// Token: 0x04002437 RID: 9271
		private const NumberStyles SignedDecimalPointStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

		// Token: 0x04002438 RID: 9272
		private CompositeFontInfo _compositeFontInfo;

		// Token: 0x04002439 RID: 9273
		private XmlReader _reader;

		// Token: 0x0400243A RID: 9274
		private Hashtable _namespaceMap;

		// Token: 0x0400243B RID: 9275
		private TypeConverter _doubleTypeConverter;

		// Token: 0x0400243C RID: 9276
		private TypeConverter _xmlLanguageTypeConverter;

		// Token: 0x0400243D RID: 9277
		private const string SystemClrNamespace = "System";

		// Token: 0x0400243E RID: 9278
		private const string CompositeFontNamespace = "http://schemas.microsoft.com/winfx/2006/xaml/composite-font";

		// Token: 0x0400243F RID: 9279
		private const string XamlNamespace = "http://schemas.microsoft.com/winfx/2006/xaml";

		// Token: 0x04002440 RID: 9280
		private const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04002441 RID: 9281
		private const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04002442 RID: 9282
		private const string FontFamilyCollectionElement = "FontFamilyCollection";

		// Token: 0x04002443 RID: 9283
		private const string FontFamilyElement = "FontFamily";

		// Token: 0x04002444 RID: 9284
		private const string BaselineAttribute = "Baseline";

		// Token: 0x04002445 RID: 9285
		private const string LineSpacingAttribute = "LineSpacing";

		// Token: 0x04002446 RID: 9286
		private const string FamilyNamesPropertyElement = "FontFamily.FamilyNames";

		// Token: 0x04002447 RID: 9287
		private const string StringElement = "String";

		// Token: 0x04002448 RID: 9288
		private const string FamilyTypefacesPropertyElement = "FontFamily.FamilyTypefaces";

		// Token: 0x04002449 RID: 9289
		private const string FamilyTypefaceElement = "FamilyTypeface";

		// Token: 0x0400244A RID: 9290
		private const string FamilyMapsPropertyElement = "FontFamily.FamilyMaps";

		// Token: 0x0400244B RID: 9291
		private const string FamilyMapElement = "FontFamilyMap";

		// Token: 0x0400244C RID: 9292
		private const string KeyAttribute = "Key";

		// Token: 0x0400244D RID: 9293
		private const string LanguageAttribute = "Language";

		// Token: 0x0400244E RID: 9294
		private const string NameAttribute = "Name";

		// Token: 0x0400244F RID: 9295
		private const string StyleAttribute = "Style";

		// Token: 0x04002450 RID: 9296
		private const string WeightAttribute = "Weight";

		// Token: 0x04002451 RID: 9297
		private const string StretchAttribute = "Stretch";

		// Token: 0x04002452 RID: 9298
		private const string UnderlinePositionAttribute = "UnderlinePosition";

		// Token: 0x04002453 RID: 9299
		private const string UnderlineThicknessAttribute = "UnderlineThickness";

		// Token: 0x04002454 RID: 9300
		private const string StrikethroughPositionAttribute = "StrikethroughPosition";

		// Token: 0x04002455 RID: 9301
		private const string StrikethroughThicknessAttribute = "StrikethroughThickness";

		// Token: 0x04002456 RID: 9302
		private const string CapsHeightAttribute = "CapsHeight";

		// Token: 0x04002457 RID: 9303
		private const string XHeightAttribute = "XHeight";

		// Token: 0x04002458 RID: 9304
		private const string UnicodeAttribute = "Unicode";

		// Token: 0x04002459 RID: 9305
		private const string TargetAttribute = "Target";

		// Token: 0x0400245A RID: 9306
		private const string ScaleAttribute = "Scale";

		// Token: 0x0400245B RID: 9307
		private const string DeviceFontNameAttribute = "DeviceFontName";

		// Token: 0x0400245C RID: 9308
		private const string DeviceFontCharacterMetricsPropertyElement = "FamilyTypeface.DeviceFontCharacterMetrics";

		// Token: 0x0400245D RID: 9309
		private const string CharacterMetricsElement = "CharacterMetrics";

		// Token: 0x0400245E RID: 9310
		private const string MetricsAttribute = "Metrics";

		// Token: 0x0400245F RID: 9311
		private const string OsAttribute = "OS";
	}
}
