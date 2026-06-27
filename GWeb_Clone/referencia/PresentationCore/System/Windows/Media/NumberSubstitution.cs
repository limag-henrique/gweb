using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Especifica como os números em texto são exibidos em culturas diferentes.</summary>
	// Token: 0x02000443 RID: 1091
	public class NumberSubstitution
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.NumberSubstitution" />.</summary>
		// Token: 0x06002C78 RID: 11384 RVA: 0x000B1874 File Offset: 0x000B0C74
		public NumberSubstitution()
		{
			this._source = NumberCultureSource.Text;
			this._cultureOverride = null;
			this._substitution = NumberSubstitutionMethod.AsCulture;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.NumberSubstitution" /> com os valores da propriedade explícita.</summary>
		/// <param name="source">Identifica a origem do valor de cultura usado para determinar a substituição de número.</param>
		/// <param name="cultureOverride">Identifica qual cultura usar quando o valor da propriedade <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" /> é definido como <see cref="F:System.Windows.Media.NumberCultureSource.Override" />.</param>
		/// <param name="substitution">Identifica o método de substituição usado para determinar a substituição de número.</param>
		// Token: 0x06002C79 RID: 11385 RVA: 0x000B189C File Offset: 0x000B0C9C
		public NumberSubstitution(NumberCultureSource source, CultureInfo cultureOverride, NumberSubstitutionMethod substitution)
		{
			this._source = source;
			this._cultureOverride = NumberSubstitution.ThrowIfInvalidCultureOverride(cultureOverride);
			this._substitution = substitution;
		}

		/// <summary>Obtém ou define um valor que identifica a origem do valor de cultura usado para determinar a substituição de número.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.NumberCultureSource" />.</returns>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000B18CC File Offset: 0x000B0CCC
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000B18E0 File Offset: 0x000B0CE0
		public NumberCultureSource CultureSource
		{
			get
			{
				return this._source;
			}
			set
			{
				if (value > NumberCultureSource.Override)
				{
					throw new InvalidEnumArgumentException("CultureSource", (int)value, typeof(NumberCultureSource));
				}
				this._source = value;
			}
		}

		/// <summary>Obtém ou define um valor que identifica qual cultura usar quando o valor da propriedade <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" /> estiver definido como <see cref="F:System.Windows.Media.NumberCultureSource.Override" />.</summary>
		/// <returns>Um valor <see cref="T:System.Globalization.CultureInfo" /> que representa a cultura que é usada como uma substituição.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000B1910 File Offset: 0x000B0D10
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000B1924 File Offset: 0x000B0D24
		[TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
		public CultureInfo CultureOverride
		{
			get
			{
				return this._cultureOverride;
			}
			set
			{
				this._cultureOverride = NumberSubstitution.ThrowIfInvalidCultureOverride(value);
			}
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000B1940 File Offset: 0x000B0D40
		private static CultureInfo ThrowIfInvalidCultureOverride(CultureInfo culture)
		{
			if (!NumberSubstitution.IsValidCultureOverride(culture))
			{
				throw new ArgumentException(SR.Get("SpecificNumberCultureRequired"));
			}
			return culture;
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000B1968 File Offset: 0x000B0D68
		private static bool IsValidCultureOverride(CultureInfo culture)
		{
			return culture == null || (!culture.IsNeutralCulture && !culture.Equals(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000B1994 File Offset: 0x000B0D94
		private static bool IsValidCultureOverrideValue(object value)
		{
			return NumberSubstitution.IsValidCultureOverride((CultureInfo)value);
		}

		/// <summary>Obtém ou define um valor que identifica o método de substituição que é usado para determinar a substituição de número.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.NumberSubstitutionMethod" />.</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000B19AC File Offset: 0x000B0DAC
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x000B19C0 File Offset: 0x000B0DC0
		public NumberSubstitutionMethod Substitution
		{
			get
			{
				return this._substitution;
			}
			set
			{
				if (value > NumberSubstitutionMethod.Traditional)
				{
					throw new InvalidEnumArgumentException("Substitution", (int)value, typeof(NumberSubstitutionMethod));
				}
				this._substitution = value;
			}
		}

		/// <summary>Define o valor de <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" /> para um elemento fornecido.</summary>
		/// <param name="target">Elemento que está especificando uma substituição de cultura.</param>
		/// <param name="value">Um valor de origem da cultura do tipo <see cref="T:System.Windows.Media.NumberCultureSource" />.</param>
		// Token: 0x06002C83 RID: 11395 RVA: 0x000B19F0 File Offset: 0x000B0DF0
		public static void SetCultureSource(DependencyObject target, NumberCultureSource value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(NumberSubstitution.CultureSourceProperty, value);
		}

		/// <summary>Retorna o valor de <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" /> do elemento fornecido.</summary>
		/// <param name="target">O elemento para o qual retornar um valor <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" />.</param>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.NumberCultureSource" />.</returns>
		// Token: 0x06002C84 RID: 11396 RVA: 0x000B1A1C File Offset: 0x000B0E1C
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static NumberCultureSource GetCultureSource(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (NumberCultureSource)target.GetValue(NumberSubstitution.CultureSourceProperty);
		}

		/// <summary>Define o valor de <see cref="P:System.Windows.Media.NumberSubstitution.CultureOverride" /> para um elemento fornecido.</summary>
		/// <param name="target">Elemento que está especificando uma substituição de cultura.</param>
		/// <param name="value">Um valor de substituição de cultura do tipo <see cref="T:System.Globalization.CultureInfo" />.</param>
		// Token: 0x06002C85 RID: 11397 RVA: 0x000B1A48 File Offset: 0x000B0E48
		public static void SetCultureOverride(DependencyObject target, CultureInfo value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(NumberSubstitution.CultureOverrideProperty, value);
		}

		/// <summary>Retorna o valor de <see cref="P:System.Windows.Media.NumberSubstitution.CultureOverride" /> do elemento fornecido.</summary>
		/// <param name="target">O elemento para o qual retornar um valor <see cref="P:System.Windows.Media.NumberSubstitution.CultureOverride" />.</param>
		/// <returns>Um valor <see cref="T:System.Globalization.CultureInfo" /> que representa a cultura que é usada como uma substituição.</returns>
		// Token: 0x06002C86 RID: 11398 RVA: 0x000B1A70 File Offset: 0x000B0E70
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		[TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
		public static CultureInfo GetCultureOverride(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (CultureInfo)target.GetValue(NumberSubstitution.CultureOverrideProperty);
		}

		/// <summary>Define o valor de <see cref="P:System.Windows.Media.NumberSubstitution.Substitution" /> para um elemento fornecido.</summary>
		/// <param name="target">Elemento que está especificando um método de substituição.</param>
		/// <param name="value">Um valor do método de substituição do tipo <see cref="T:System.Windows.Media.NumberSubstitutionMethod" />.</param>
		// Token: 0x06002C87 RID: 11399 RVA: 0x000B1A9C File Offset: 0x000B0E9C
		public static void SetSubstitution(DependencyObject target, NumberSubstitutionMethod value)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(NumberSubstitution.SubstitutionProperty, value);
		}

		/// <summary>Retorna o valor de <see cref="P:System.Windows.Media.NumberSubstitution.Substitution" /> do elemento fornecido.</summary>
		/// <param name="target">O elemento para o qual retornar um valor <see cref="P:System.Windows.Media.NumberSubstitution.Substitution" />.</param>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.NumberSubstitutionMethod" />.</returns>
		// Token: 0x06002C88 RID: 11400 RVA: 0x000B1AC8 File Offset: 0x000B0EC8
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static NumberSubstitutionMethod GetSubstitution(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (NumberSubstitutionMethod)target.GetValue(NumberSubstitution.SubstitutionProperty);
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.NumberSubstitution" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x06002C89 RID: 11401 RVA: 0x000B1AF4 File Offset: 0x000B0EF4
		public override int GetHashCode()
		{
			int hash = (int)(HashFn.HashMultiply((int)this._source) + this._substitution);
			if (this._cultureOverride != null)
			{
				hash = HashFn.HashMultiply(hash) + this._cultureOverride.GetHashCode();
			}
			return HashFn.HashScramble(hash);
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.NumberSubstitution" /> atual.</summary>
		/// <param name="obj">O <see cref="T:System.Object" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.NumberSubstitution" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> for igual ao objeto <see cref="T:System.Windows.Media.NumberSubstitution" /> atual; caso contrário, <see langword="false" />. Se <paramref name="o" /> não for um objeto <see cref="T:System.Windows.Media.NumberSubstitution" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x06002C8A RID: 11402 RVA: 0x000B1B38 File Offset: 0x000B0F38
		public override bool Equals(object obj)
		{
			NumberSubstitution numberSubstitution = obj as NumberSubstitution;
			if (numberSubstitution == null || this._source != numberSubstitution._source || this._substitution != numberSubstitution._substitution)
			{
				return false;
			}
			if (this._cultureOverride != null)
			{
				return this._cultureOverride.Equals(numberSubstitution._cultureOverride);
			}
			return numberSubstitution._cultureOverride == null;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.NumberSubstitution.CultureSource" />.</summary>
		// Token: 0x04001452 RID: 5202
		public static readonly DependencyProperty CultureSourceProperty = DependencyProperty.RegisterAttached("CultureSource", typeof(NumberCultureSource), typeof(NumberSubstitution));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.NumberSubstitution.CultureOverride" />.</summary>
		// Token: 0x04001453 RID: 5203
		public static readonly DependencyProperty CultureOverrideProperty = DependencyProperty.RegisterAttached("CultureOverride", typeof(CultureInfo), typeof(NumberSubstitution), null, new ValidateValueCallback(NumberSubstitution.IsValidCultureOverrideValue));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.NumberSubstitution.Substitution" />.</summary>
		// Token: 0x04001454 RID: 5204
		public static readonly DependencyProperty SubstitutionProperty = DependencyProperty.RegisterAttached("Substitution", typeof(NumberSubstitutionMethod), typeof(NumberSubstitution));

		// Token: 0x04001455 RID: 5205
		private NumberCultureSource _source;

		// Token: 0x04001456 RID: 5206
		private CultureInfo _cultureOverride;

		// Token: 0x04001457 RID: 5207
		private NumberSubstitutionMethod _substitution;
	}
}
