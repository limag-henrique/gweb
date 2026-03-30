using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Define um nome para padrões de entrada de texto.</summary>
	// Token: 0x02000260 RID: 608
	[TypeConverter("System.Windows.Input.InputScopeNameConverter, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[ContentProperty("NameValue")]
	public class InputScopeName : IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputScopeName" />.</summary>
		// Token: 0x0600111A RID: 4378 RVA: 0x00040634 File Offset: 0x0003FA34
		public InputScopeName()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputScopeName" /> com o <see cref="P:System.Windows.Input.InputScopeName.NameValue" /> especificado.</summary>
		/// <param name="nameValue">O nome do escopo de entrada que modifica como a entrada de métodos alternativos de entrada é interpretada.</param>
		// Token: 0x0600111B RID: 4379 RVA: 0x00040648 File Offset: 0x0003FA48
		public InputScopeName(InputScopeNameValue nameValue)
		{
			this._nameValue = nameValue;
		}

		/// <summary>Adiciona um objeto filho a este <see cref="T:System.Windows.Input.InputScopeName" />.</summary>
		/// <param name="value">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Input.InputScopeName" />.</param>
		// Token: 0x0600111C RID: 4380 RVA: 0x00040664 File Offset: 0x0003FA64
		public void AddChild(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Input.InputScopeName" />.</summary>
		/// <param name="name">O texto adicionado ao <see cref="T:System.Windows.Input.InputScopeName" />.</param>
		// Token: 0x0600111D RID: 4381 RVA: 0x00040678 File Offset: 0x0003FA78
		public void AddText(string name)
		{
		}

		/// <summary>Obtém ou define o valor do nome do escopo de entrada que modifica como a entrada de métodos de entrada alternativos é interpretada.</summary>
		/// <returns>O valor de nome de escopo de entrada que modifica como a entrada de métodos de entrada alternativos é interpretada.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00040688 File Offset: 0x0003FA88
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x0004069C File Offset: 0x0003FA9C
		public InputScopeNameValue NameValue
		{
			get
			{
				return this._nameValue;
			}
			set
			{
				if (!this.IsValidInputScopeNameValue(value))
				{
					throw new ArgumentException(SR.Get("InputScope_InvalidInputScopeName", new object[]
					{
						"value"
					}));
				}
				this._nameValue = value;
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000406D8 File Offset: 0x0003FAD8
		private bool IsValidInputScopeNameValue(InputScopeNameValue name)
		{
			switch (name)
			{
			case InputScopeNameValue.Xml:
			case InputScopeNameValue.Srgs:
			case InputScopeNameValue.RegularExpression:
			case InputScopeNameValue.PhraseList:
			case InputScopeNameValue.Default:
			case InputScopeNameValue.Url:
			case InputScopeNameValue.FullFilePath:
			case InputScopeNameValue.FileName:
			case InputScopeNameValue.EmailUserName:
			case InputScopeNameValue.EmailSmtpAddress:
			case InputScopeNameValue.LogOnName:
			case InputScopeNameValue.PersonalFullName:
			case InputScopeNameValue.PersonalNamePrefix:
			case InputScopeNameValue.PersonalGivenName:
			case InputScopeNameValue.PersonalMiddleName:
			case InputScopeNameValue.PersonalSurname:
			case InputScopeNameValue.PersonalNameSuffix:
			case InputScopeNameValue.PostalAddress:
			case InputScopeNameValue.PostalCode:
			case InputScopeNameValue.AddressStreet:
			case InputScopeNameValue.AddressStateOrProvince:
			case InputScopeNameValue.AddressCity:
			case InputScopeNameValue.AddressCountryName:
			case InputScopeNameValue.AddressCountryShortName:
			case InputScopeNameValue.CurrencyAmountAndSymbol:
			case InputScopeNameValue.CurrencyAmount:
			case InputScopeNameValue.Date:
			case InputScopeNameValue.DateMonth:
			case InputScopeNameValue.DateDay:
			case InputScopeNameValue.DateYear:
			case InputScopeNameValue.DateMonthName:
			case InputScopeNameValue.DateDayName:
			case InputScopeNameValue.Digits:
			case InputScopeNameValue.Number:
			case InputScopeNameValue.OneChar:
			case InputScopeNameValue.Password:
			case InputScopeNameValue.TelephoneNumber:
			case InputScopeNameValue.TelephoneCountryCode:
			case InputScopeNameValue.TelephoneAreaCode:
			case InputScopeNameValue.TelephoneLocalNumber:
			case InputScopeNameValue.Time:
			case InputScopeNameValue.TimeHour:
			case InputScopeNameValue.TimeMinorSec:
			case InputScopeNameValue.NumberFullWidth:
			case InputScopeNameValue.AlphanumericHalfWidth:
			case InputScopeNameValue.AlphanumericFullWidth:
			case InputScopeNameValue.CurrencyChinese:
			case InputScopeNameValue.Bopomofo:
			case InputScopeNameValue.Hiragana:
			case InputScopeNameValue.KatakanaHalfWidth:
			case InputScopeNameValue.KatakanaFullWidth:
			case InputScopeNameValue.Hanja:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0400093E RID: 2366
		private InputScopeNameValue _nameValue;
	}
}
