using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Estrutura de dados que descreve o tamanho de um objeto tridimensional.</summary>
	// Token: 0x0200047E RID: 1150
	[TypeConverter(typeof(Size3DConverter))]
	[ValueSerializer(typeof(Size3DValueSerializer))]
	[Serializable]
	public struct Size3D : IFormattable
	{
		/// <summary>Inicializa uma nova instância da estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="x">O novo valor <see cref="P:System.Windows.Media.Media3D.Size3D.X" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</param>
		/// <param name="y">O novo valor <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</param>
		/// <param name="z">O novo valor <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> da estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</param>
		// Token: 0x0600318E RID: 12686 RVA: 0x000C62E0 File Offset: 0x000C56E0
		public Size3D(double x, double y, double z)
		{
			if (x < 0.0 || y < 0.0 || z < 0.0)
			{
				throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
			}
			this._x = x;
			this._y = y;
			this._z = z;
		}

		/// <summary>Obtém um valor que representa uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> vazia.</summary>
		/// <returns>Uma instância vazia de um <see cref="T:System.Windows.Media.Media3D.Size3D" /> estrutura.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000C6338 File Offset: 0x000C5738
		public static Size3D Empty
		{
			get
			{
				return Size3D.s_empty;
			}
		}

		/// <summary>Obtém um valor que indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> está vazia.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Media.Media3D.Size3D" /> estrutura estiver vazia; caso contrário, <see langword="false" />.  O padrão é <see langword="false" />.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000C634C File Offset: 0x000C574C
		public bool IsEmpty
		{
			get
			{
				return this._x < 0.0;
			}
		}

		/// <summary>Obtém ou define o valor <see cref="P:System.Windows.Media.Media3D.Size3D.X" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Size3D.X" /> valor deste <see cref="T:System.Windows.Media.Media3D.Size3D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000C636C File Offset: 0x000C576C
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x000C6380 File Offset: 0x000C5780
		public double X
		{
			get
			{
				return this._x;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Size3D_CannotModifyEmptySize"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._x = value;
			}
		}

		/// <summary>Obtém ou define o valor <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> valor deste <see cref="T:System.Windows.Media.Media3D.Size3D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000C63C8 File Offset: 0x000C57C8
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x000C63DC File Offset: 0x000C57DC
		public double Y
		{
			get
			{
				return this._y;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Size3D_CannotModifyEmptySize"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._y = value;
			}
		}

		/// <summary>Obtém ou define o valor <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> valor deste <see cref="T:System.Windows.Media.Media3D.Size3D" /> estrutura.  O valor padrão é 0.</returns>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000C6424 File Offset: 0x000C5824
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x000C6438 File Offset: 0x000C5838
		public double Z
		{
			get
			{
				return this._z;
			}
			set
			{
				if (this.IsEmpty)
				{
					throw new InvalidOperationException(SR.Get("Size3D_CannotModifyEmptySize"));
				}
				if (value < 0.0)
				{
					throw new ArgumentException(SR.Get("Size3D_DimensionCannotBeNegative"));
				}
				this._z = value;
			}
		}

		/// <summary>Converte esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> em uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="size">O tamanho a converter.</param>
		/// <returns>O resultado da conversão de <paramref name="size" />.</returns>
		// Token: 0x06003197 RID: 12695 RVA: 0x000C6480 File Offset: 0x000C5880
		public static explicit operator Vector3D(Size3D size)
		{
			return new Vector3D(size._x, size._y, size._z);
		}

		/// <summary>Converte esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="size">O tamanho a converter.</param>
		/// <returns>O resultado da conversão de <paramref name="size" />.</returns>
		// Token: 0x06003198 RID: 12696 RVA: 0x000C64A4 File Offset: 0x000C58A4
		public static explicit operator Point3D(Size3D size)
		{
			return new Point3D(size._x, size._y, size._z);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000C64C8 File Offset: 0x000C58C8
		private static Size3D CreateEmptySize3D()
		{
			return new Size3D
			{
				_x = double.NegativeInfinity,
				_y = double.NegativeInfinity,
				_z = double.NegativeInfinity
			};
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" />.   Duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" /> são iguais se os valores de suas propriedades <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> são os mesmos.</summary>
		/// <param name="size1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <param name="size2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> de <paramref name="size1" /> e <paramref name="size2" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600319A RID: 12698 RVA: 0x000C6510 File Offset: 0x000C5910
		public static bool operator ==(Size3D size1, Size3D size2)
		{
			return size1.X == size2.X && size1.Y == size2.Y && size1.Z == size2.Z;
		}

		/// <summary>Compara duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" /> quanto à desigualdade.  Duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" /> não são iguais se os valores de suas propriedades <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> são diferentes.</summary>
		/// <param name="size1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <param name="size2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as coordenadas <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> de <paramref name="size1" /> e <paramref name="size2" /> forem diferentes; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600319B RID: 12699 RVA: 0x000C6550 File Offset: 0x000C5950
		public static bool operator !=(Size3D size1, Size3D size2)
		{
			return !(size1 == size2);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" />.   Duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" /> são iguais se os valores de suas propriedades <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> são os mesmos.</summary>
		/// <param name="size1">A primeira estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <param name="size2">A segunda estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.  
		///  <see langword="true" /> se os componentes <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> de <paramref name="size1" /> e <paramref name="size2" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600319C RID: 12700 RVA: 0x000C6568 File Offset: 0x000C5968
		public static bool Equals(Size3D size1, Size3D size2)
		{
			if (size1.IsEmpty)
			{
				return size2.IsEmpty;
			}
			return size1.X.Equals(size2.X) && size1.Y.Equals(size2.Y) && size1.Z.Equals(size2.Z);
		}

		/// <summary>Determina se o objeto especificado é uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> e se as propriedades <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> do <see cref="T:System.Object" /> especificado são iguais às propriedades <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="o">O <see cref="T:System.Object" /> de comparação.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.  
		///  <see langword="true" /> se <paramref name="o" /> for uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> e idêntico à esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600319D RID: 12701 RVA: 0x000C65D0 File Offset: 0x000C59D0
		public override bool Equals(object o)
		{
			if (o == null || !(o is Size3D))
			{
				return false;
			}
			Size3D size = (Size3D)o;
			return Size3D.Equals(this, size);
		}

		/// <summary>Compara a igualdade de duas estruturas <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="value">A instância de Size3D a ser comparada a esta instância.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias forem iguais, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600319E RID: 12702 RVA: 0x000C6600 File Offset: 0x000C5A00
		public bool Equals(Size3D value)
		{
			return Size3D.Equals(this, value);
		}

		/// <summary>Retorna o código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <returns>Um código hash desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</returns>
		// Token: 0x0600319F RID: 12703 RVA: 0x000C661C File Offset: 0x000C5A1C
		public override int GetHashCode()
		{
			if (this.IsEmpty)
			{
				return 0;
			}
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		/// <summary>Converte uma representação <see cref="T:System.String" /> de uma estrutura de tamanho tridimensional em uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> equivalente.</summary>
		/// <param name="source">A representação <see cref="T:System.String" /> da estrutura de tamanho tridimensional.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> equivalente.</returns>
		// Token: 0x060031A0 RID: 12704 RVA: 0x000C6660 File Offset: 0x000C5A60
		public static Size3D Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			string text = tokenizerHelper.NextTokenRequired();
			Size3D empty;
			if (text == "Empty")
			{
				empty = Size3D.Empty;
			}
			else
			{
				empty = new Size3D(Convert.ToDouble(text, invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
			}
			tokenizerHelper.LastTokenRequired();
			return empty;
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</returns>
		// Token: 0x060031A1 RID: 12705 RVA: 0x000C66C4 File Offset: 0x000C5AC4
		public override string ToString()
		{
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação <see cref="T:System.String" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Media.Media3D.Size3D.X" />, <see cref="P:System.Windows.Media.Media3D.Size3D.Y" /> e <see cref="P:System.Windows.Media.Media3D.Size3D.Z" /> desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</returns>
		// Token: 0x060031A2 RID: 12706 RVA: 0x000C66DC File Offset: 0x000C5ADC
		public string ToString(IFormatProvider provider)
		{
			return this.ConvertToString(null, provider);
		}

		/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código. Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">A cadeia de caracteres que especifica o formato a ser usado.  
		///
		/// ou - 
		/// <see langword="null" /> para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O <see langword="IFormatProvider" /> a ser usado para formatar o valor.  
		///
		/// ou - 
		/// <see langword="null" /> para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>Uma cadeia de caracteres que contém o valor da instância atual no formato especificado.</returns>
		// Token: 0x060031A3 RID: 12707 RVA: 0x000C66F4 File Offset: 0x000C5AF4
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000C670C File Offset: 0x000C5B0C
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.IsEmpty)
			{
				return "Empty";
			}
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, string.Concat(new string[]
			{
				"{1:",
				format,
				"}{0}{2:",
				format,
				"}{0}{3:",
				format,
				"}"
			}), new object[]
			{
				numericListSeparator,
				this._x,
				this._y,
				this._z
			});
		}

		// Token: 0x040015A6 RID: 5542
		private static readonly Size3D s_empty = Size3D.CreateEmptySize3D();

		// Token: 0x040015A7 RID: 5543
		internal double _x;

		// Token: 0x040015A8 RID: 5544
		internal double _y;

		// Token: 0x040015A9 RID: 5545
		internal double _z;
	}
}
