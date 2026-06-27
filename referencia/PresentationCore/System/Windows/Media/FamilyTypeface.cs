using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Windows.Markup;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Especifica os detalhes de uma única face de tipos com suporte por um <see cref="T:System.Windows.Media.FontFamily" />.</summary>
	// Token: 0x02000392 RID: 914
	public class FamilyTypeface : IDeviceFont, ITypefaceMetrics
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FamilyTypeface" />.</summary>
		// Token: 0x060021FF RID: 8703 RVA: 0x00089854 File Offset: 0x00088C54
		public FamilyTypeface()
		{
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00089868 File Offset: 0x00088C68
		internal FamilyTypeface(Typeface face)
		{
			this._style = face.Style;
			this._weight = face.Weight;
			this._stretch = face.Stretch;
			this._underlinePosition = face.UnderlinePosition;
			this._underlineThickness = face.UnderlineThickness;
			this._strikeThroughPosition = face.StrikethroughPosition;
			this._strikeThroughThickness = face.StrikethroughThickness;
			this._capsHeight = face.CapsHeight;
			this._xHeight = face.XHeight;
			this._readOnly = true;
		}

		/// <summary>Obtém ou define o estilo do design da face de tipos da família de fontes.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.FontStyle" />.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000898F0 File Offset: 0x00088CF0
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x00089904 File Offset: 0x00088D04
		public FontStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.VerifyChangeable();
				this._style = value;
			}
		}

		/// <summary>Obtém ou define o peso projetado desta face de tipos da família de fonte.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.FontWeight" />.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00089920 File Offset: 0x00088D20
		// (set) Token: 0x06002204 RID: 8708 RVA: 0x00089934 File Offset: 0x00088D34
		public FontWeight Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this.VerifyChangeable();
				this._weight = value;
			}
		}

		/// <summary>Obtém ou define a ampliação projetada de face de tipos da família de fonte.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.FontStretch" />.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00089950 File Offset: 0x00088D50
		// (set) Token: 0x06002206 RID: 8710 RVA: 0x00089964 File Offset: 0x00088D64
		public FontStretch Stretch
		{
			get
			{
				return this._stretch;
			}
			set
			{
				this.VerifyChangeable();
				this._stretch = value;
			}
		}

		/// <summary>Obtém ou define a posição do valor sublinhado em relação à linha de base. O valor também é relativo ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00089980 File Offset: 0x00088D80
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x00089994 File Offset: 0x00088D94
		public double UnderlinePosition
		{
			get
			{
				return this._underlinePosition;
			}
			set
			{
				CompositeFontParser.VerifyMultiplierOfEm("UnderlinePosition", ref value);
				this.VerifyChangeable();
				this._underlinePosition = value;
			}
		}

		/// <summary>Obtém ou define a espessura do sublinhado em relação ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x000899BC File Offset: 0x00088DBC
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x000899D0 File Offset: 0x00088DD0
		public double UnderlineThickness
		{
			get
			{
				return this._underlineThickness;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("UnderlineThickness", ref value);
				this.VerifyChangeable();
				this._underlineThickness = value;
			}
		}

		/// <summary>Obtém ou define a posição do valor tachado em relação à linha de base. O valor também é relativo ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000899F8 File Offset: 0x00088DF8
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x00089A0C File Offset: 0x00088E0C
		public double StrikethroughPosition
		{
			get
			{
				return this._strikeThroughPosition;
			}
			set
			{
				CompositeFontParser.VerifyMultiplierOfEm("StrikethroughPosition", ref value);
				this.VerifyChangeable();
				this._strikeThroughPosition = value;
			}
		}

		/// <summary>Obtém ou define a espessura do tachado em relação ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00089A34 File Offset: 0x00088E34
		// (set) Token: 0x0600220E RID: 8718 RVA: 0x00089A48 File Offset: 0x00088E48
		public double StrikethroughThickness
		{
			get
			{
				return this._strikeThroughThickness;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("StrikethroughThickness", ref value);
				this.VerifyChangeable();
				this._strikeThroughThickness = value;
			}
		}

		/// <summary>Obtém ou define a distância da linha de base até a parte superior de uma maiúscula em inglês, com relação ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00089A70 File Offset: 0x00088E70
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x00089A84 File Offset: 0x00088E84
		public double CapsHeight
		{
			get
			{
				return this._capsHeight;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("CapsHeight", ref value);
				this.VerifyChangeable();
				this._capsHeight = value;
			}
		}

		/// <summary>Obtém ou define a altura x Ocidental em relação ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00089AAC File Offset: 0x00088EAC
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x00089AC0 File Offset: 0x00088EC0
		public double XHeight
		{
			get
			{
				return this._xHeight;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("XHeight", ref value);
				this.VerifyChangeable();
				this._xHeight = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00089AE8 File Offset: 0x00088EE8
		bool ITypefaceMetrics.Symbol
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00089AF8 File Offset: 0x00088EF8
		StyleSimulations ITypefaceMetrics.StyleSimulations
		{
			get
			{
				return StyleSimulations.None;
			}
		}

		/// <summary>Obtém uma coleção de nomes de face localizados ajustados para o diferenciador de fonte.</summary>
		/// <returns>Uma matriz do tipo <see cref="T:System.Collections.Generic.IDictionary`2" /> que representam os nomes de face de tipos localizados.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00089B08 File Offset: 0x00088F08
		public IDictionary<XmlLanguage, string> AdjustedFaceNames
		{
			get
			{
				return FontDifferentiator.ConstructFaceNamesByStyleWeightStretch(this._style, this._weight, this._stretch);
			}
		}

		/// <summary>Compara a igualdade de dois tipos de faces de família de fonte.</summary>
		/// <param name="typeface">O valor <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="typeface" /> não é nulo e tem as mesmas propriedades que essa face de tipos da família de fontes; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002216 RID: 8726 RVA: 0x00089B2C File Offset: 0x00088F2C
		public bool Equals(FamilyTypeface typeface)
		{
			return typeface != null && (this.Style == typeface.Style && this.Weight == typeface.Weight) && this.Stretch == typeface.Stretch;
		}

		/// <summary>Obtém ou define o nome ou identificador exclusivo para uma face de tipos de família de fontes do dispositivo.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.String" /> que representa o nome da fonte de dispositivo.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00089B78 File Offset: 0x00088F78
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x00089B8C File Offset: 0x00088F8C
		public string DeviceFontName
		{
			get
			{
				return this._deviceFontName;
			}
			set
			{
				this.VerifyChangeable();
				this._deviceFontName = value;
			}
		}

		/// <summary>Obtém a coleção de métricas de caractere para uma face de tipo de família de fontes do dispositivo.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00089BA8 File Offset: 0x00088FA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CharacterMetricsDictionary DeviceFontCharacterMetrics
		{
			get
			{
				if (this._characterMetrics == null)
				{
					this._characterMetrics = new CharacterMetricsDictionary();
				}
				return this._characterMetrics;
			}
		}

		/// <summary>Compara a igualdade de dois tipos de faces de família de fonte.</summary>
		/// <param name="o">O valor <see cref="T:System.Object" /> que representa a face de tipos a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="typeface" /> não é nulo e tem as mesmas propriedades que essa face de tipos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600221A RID: 8730 RVA: 0x00089BD0 File Offset: 0x00088FD0
		public override bool Equals(object o)
		{
			return this.Equals(o as FamilyTypeface);
		}

		/// <summary>Serve como uma função de hash para um objeto <see cref="T:System.Windows.Media.FamilyTypeface" />. O método <see cref="M:System.Windows.Media.FamilyTypeface.GetHashCode" /> é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Int32" />.</returns>
		// Token: 0x0600221B RID: 8731 RVA: 0x00089BEC File Offset: 0x00088FEC
		public override int GetHashCode()
		{
			return this._style.GetHashCode() ^ this._weight.GetHashCode() ^ this._stretch.GetHashCode();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00089C30 File Offset: 0x00089030
		private void VerifyChangeable()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.Get("General_ObjectIsReadOnly"));
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00089C58 File Offset: 0x00089058
		string IDeviceFont.Name
		{
			get
			{
				return this._deviceFontName;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00089C6C File Offset: 0x0008906C
		bool IDeviceFont.ContainsCharacter(int unicodeScalar)
		{
			return this._characterMetrics != null && this._characterMetrics.GetValue(unicodeScalar) != null;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00089C94 File Offset: 0x00089094
		[SecurityCritical]
		unsafe void IDeviceFont.GetAdvanceWidths(char* characterString, int characterLength, double emSize, int* pAdvances)
		{
			for (int i = 0; i < characterLength; i++)
			{
				CharacterMetrics value = this._characterMetrics.GetValue((int)characterString[i]);
				if (value != null)
				{
					pAdvances[i] = Math.Max(0, (int)((value.BlackBoxWidth + value.LeftSideBearing + value.RightSideBearing) * emSize));
				}
				else
				{
					pAdvances[i] = 0;
				}
			}
		}

		// Token: 0x040010E1 RID: 4321
		private bool _readOnly;

		// Token: 0x040010E2 RID: 4322
		private FontStyle _style;

		// Token: 0x040010E3 RID: 4323
		private FontWeight _weight;

		// Token: 0x040010E4 RID: 4324
		private FontStretch _stretch;

		// Token: 0x040010E5 RID: 4325
		private double _underlinePosition;

		// Token: 0x040010E6 RID: 4326
		private double _underlineThickness;

		// Token: 0x040010E7 RID: 4327
		private double _strikeThroughPosition;

		// Token: 0x040010E8 RID: 4328
		private double _strikeThroughThickness;

		// Token: 0x040010E9 RID: 4329
		private double _capsHeight;

		// Token: 0x040010EA RID: 4330
		private double _xHeight;

		// Token: 0x040010EB RID: 4331
		private string _deviceFontName;

		// Token: 0x040010EC RID: 4332
		private CharacterMetricsDictionary _characterMetrics;
	}
}
