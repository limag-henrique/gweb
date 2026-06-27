using System;

namespace System.Windows.Input
{
	/// <summary>Especifica o nome do escopo de entrada que modifica como a entrada de métodos de entrada alternativos é interpretada.</summary>
	// Token: 0x02000263 RID: 611
	public enum InputScopeNameValue
	{
		/// <summary>O tratamento padrão de comandos de entrada.</summary>
		// Token: 0x04000941 RID: 2369
		Default,
		/// <summary>O padrão de entrada de texto para uma URL (Uniform Resource Locator).</summary>
		// Token: 0x04000942 RID: 2370
		Url,
		/// <summary>O padrão de entrada de texto para o caminho completo de um arquivo.</summary>
		// Token: 0x04000943 RID: 2371
		FullFilePath,
		/// <summary>O padrão de entrada de texto para um nome de arquivo.</summary>
		// Token: 0x04000944 RID: 2372
		FileName,
		/// <summary>O padrão de entrada de texto para um nome de usuário de email.</summary>
		// Token: 0x04000945 RID: 2373
		EmailUserName,
		/// <summary>O padrão de entrada de texto para o endereço de email SMTP.</summary>
		// Token: 0x04000946 RID: 2374
		EmailSmtpAddress,
		/// <summary>O padrão de entrada de texto para um nome de logon.</summary>
		// Token: 0x04000947 RID: 2375
		LogOnName,
		/// <summary>O padrão de entrada de texto para o nome completo de uma pessoa.</summary>
		// Token: 0x04000948 RID: 2376
		PersonalFullName,
		/// <summary>O padrão de entrada de texto para o prefixo do nome de uma pessoa.</summary>
		// Token: 0x04000949 RID: 2377
		PersonalNamePrefix,
		/// <summary>O padrão de entrada de texto para o nome de uma pessoa.</summary>
		// Token: 0x0400094A RID: 2378
		PersonalGivenName,
		/// <summary>O padrão de entrada de texto para o nome do meio de uma pessoa.</summary>
		// Token: 0x0400094B RID: 2379
		PersonalMiddleName,
		/// <summary>O padrão de entrada de texto para o sobrenome de uma pessoa.</summary>
		// Token: 0x0400094C RID: 2380
		PersonalSurname,
		/// <summary>O padrão de entrada de texto para o sufixo do nome de uma pessoa.</summary>
		// Token: 0x0400094D RID: 2381
		PersonalNameSuffix,
		/// <summary>O padrão de entrada de texto para um endereço postal.</summary>
		// Token: 0x0400094E RID: 2382
		PostalAddress,
		/// <summary>O padrão de entrada de texto para um CEP.</summary>
		// Token: 0x0400094F RID: 2383
		PostalCode,
		/// <summary>O padrão de entrada de texto para um endereço.</summary>
		// Token: 0x04000950 RID: 2384
		AddressStreet,
		/// <summary>O padrão de entrada de texto para um estado ou província.</summary>
		// Token: 0x04000951 RID: 2385
		AddressStateOrProvince,
		/// <summary>O padrão de entrada de texto para uma cidade do endereço.</summary>
		// Token: 0x04000952 RID: 2386
		AddressCity,
		/// <summary>O padrão de entrada de texto para o nome de um país.</summary>
		// Token: 0x04000953 RID: 2387
		AddressCountryName,
		/// <summary>O padrão de entrada de texto para o nome abreviado de um país.</summary>
		// Token: 0x04000954 RID: 2388
		AddressCountryShortName,
		/// <summary>O padrão de entrada de texto para o valor e o símbolo de moeda.</summary>
		// Token: 0x04000955 RID: 2389
		CurrencyAmountAndSymbol,
		/// <summary>O padrão de entrada de texto para o valor de moeda.</summary>
		// Token: 0x04000956 RID: 2390
		CurrencyAmount,
		/// <summary>O padrão de entrada de texto para uma data do calendário.</summary>
		// Token: 0x04000957 RID: 2391
		Date,
		/// <summary>O padrão de entrada de texto para o mês numérico em uma data do calendário.</summary>
		// Token: 0x04000958 RID: 2392
		DateMonth,
		/// <summary>O padrão de entrada de texto para o dia numérico em uma data do calendário.</summary>
		// Token: 0x04000959 RID: 2393
		DateDay,
		/// <summary>O padrão de entrada de texto para o ano numérico em uma data do calendário.</summary>
		// Token: 0x0400095A RID: 2394
		DateYear,
		/// <summary>O padrão de entrada de texto para o nome do mês em uma data do calendário.</summary>
		// Token: 0x0400095B RID: 2395
		DateMonthName,
		/// <summary>O padrão de entrada de texto para o nome do dia em uma data do calendário.</summary>
		// Token: 0x0400095C RID: 2396
		DateDayName,
		/// <summary>O padrão de entrada de texto para dígitos.</summary>
		// Token: 0x0400095D RID: 2397
		Digits,
		/// <summary>O padrão de entrada de texto para um número.</summary>
		// Token: 0x0400095E RID: 2398
		Number,
		/// <summary>O padrão de entrada de texto para um caractere.</summary>
		// Token: 0x0400095F RID: 2399
		OneChar,
		/// <summary>O padrão de entrada de texto para uma senha.</summary>
		// Token: 0x04000960 RID: 2400
		Password,
		/// <summary>O padrão de entrada de texto para um número de telefone.</summary>
		// Token: 0x04000961 RID: 2401
		TelephoneNumber,
		/// <summary>O padrão de entrada de texto para um código de país do telefone.</summary>
		// Token: 0x04000962 RID: 2402
		TelephoneCountryCode,
		/// <summary>O padrão de entrada de texto para um código de área.</summary>
		// Token: 0x04000963 RID: 2403
		TelephoneAreaCode,
		/// <summary>O padrão de entrada de texto para um número de telefone local.</summary>
		// Token: 0x04000964 RID: 2404
		TelephoneLocalNumber,
		/// <summary>O padrão de entrada de texto para o horário.</summary>
		// Token: 0x04000965 RID: 2405
		Time,
		/// <summary>O padrão de entrada de texto para a hora do horário.</summary>
		// Token: 0x04000966 RID: 2406
		TimeHour,
		/// <summary>O padrão de entrada de texto para os minutos ou segundos do horário.</summary>
		// Token: 0x04000967 RID: 2407
		TimeMinorSec,
		/// <summary>O padrão de entrada de texto para um número de largura inteira.</summary>
		// Token: 0x04000968 RID: 2408
		NumberFullWidth,
		/// <summary>O padrão de entrada de texto para caracteres alfanuméricos de meia largura.</summary>
		// Token: 0x04000969 RID: 2409
		AlphanumericHalfWidth,
		/// <summary>O padrão de entrada de texto para caracteres alfanuméricos de largura inteira.</summary>
		// Token: 0x0400096A RID: 2410
		AlphanumericFullWidth,
		/// <summary>O padrão de entrada de texto para a moeda chinesa.</summary>
		// Token: 0x0400096B RID: 2411
		CurrencyChinese,
		/// <summary>O padrão de entrada de texto para o sistema de transcrição fonética do chinês mandarim Bopomofo.</summary>
		// Token: 0x0400096C RID: 2412
		Bopomofo,
		/// <summary>O padrão de entrada de texto para o sistema de escrita Hiragana.</summary>
		// Token: 0x0400096D RID: 2413
		Hiragana,
		/// <summary>O padrão de entrada de texto para caracteres Katakana de meia largura.</summary>
		// Token: 0x0400096E RID: 2414
		KatakanaHalfWidth,
		/// <summary>O padrão de entrada de texto para caracteres Katakana de largura inteira.</summary>
		// Token: 0x0400096F RID: 2415
		KatakanaFullWidth,
		/// <summary>O padrão de entrada de texto para caracteres hanja.</summary>
		// Token: 0x04000970 RID: 2416
		Hanja,
		/// <summary>O padrão de entrada de texto para uma lista de frases.</summary>
		// Token: 0x04000971 RID: 2417
		PhraseList = -1,
		/// <summary>O padrão de entrada de texto para uma expressão regular.</summary>
		// Token: 0x04000972 RID: 2418
		RegularExpression = -2,
		/// <summary>O padrão de entrada de texto para a SRGS (Speech Recognition Grammar Specification).</summary>
		// Token: 0x04000973 RID: 2419
		Srgs = -3,
		/// <summary>O padrão de entrada de texto para XML.</summary>
		// Token: 0x04000974 RID: 2420
		Xml = -4
	}
}
