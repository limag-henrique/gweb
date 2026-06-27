using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Especifica as propriedades que estão em um <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
	// Token: 0x020002BB RID: 699
	public class StylusPointDescription
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		// Token: 0x060014DF RID: 5343 RVA: 0x0004D01C File Offset: 0x0004C41C
		public StylusPointDescription()
		{
			this._stylusPointPropertyInfos = new StylusPointPropertyInfo[]
			{
				StylusPointPropertyInfoDefaults.X,
				StylusPointPropertyInfoDefaults.Y,
				StylusPointPropertyInfoDefaults.NormalPressure
			};
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointDescription" /> com os objetos <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> especificados.</summary>
		/// <param name="stylusPointPropertyInfos">Um IEnumerable genérico do tipo <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> que especifica as propriedades no <see cref="T:System.Windows.Input.StylusPointDescription" />.</param>
		// Token: 0x060014E0 RID: 5344 RVA: 0x0004D060 File Offset: 0x0004C460
		public StylusPointDescription(IEnumerable<StylusPointPropertyInfo> stylusPointPropertyInfos)
		{
			if (stylusPointPropertyInfos == null)
			{
				throw new ArgumentNullException("stylusPointPropertyInfos");
			}
			List<StylusPointPropertyInfo> list = new List<StylusPointPropertyInfo>(stylusPointPropertyInfos);
			if (list.Count < StylusPointDescription.RequiredCountOfProperties || list[StylusPointDescription.RequiredXIndex].Id != StylusPointPropertyIds.X || list[StylusPointDescription.RequiredYIndex].Id != StylusPointPropertyIds.Y || list[StylusPointDescription.RequiredPressureIndex].Id != StylusPointPropertyIds.NormalPressure)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointDescription"), "stylusPointPropertyInfos");
			}
			List<Guid> list2 = new List<Guid>();
			list2.Add(StylusPointPropertyIds.X);
			list2.Add(StylusPointPropertyIds.Y);
			list2.Add(StylusPointPropertyIds.NormalPressure);
			int num = 0;
			for (int i = StylusPointDescription.RequiredCountOfProperties; i < list.Count; i++)
			{
				if (list2.Contains(list[i].Id))
				{
					throw new ArgumentException(SR.Get("InvalidStylusPointDescriptionDuplicatesFound"), "stylusPointPropertyInfos");
				}
				if (list[i].IsButton)
				{
					num++;
				}
				else if (num > 0)
				{
					throw new ArgumentException(SR.Get("InvalidStylusPointDescriptionButtonsMustBeLast"), "stylusPointPropertyInfos");
				}
				list2.Add(list[i].Id);
			}
			if (num > StylusPointDescription.MaximumButtonCount)
			{
				throw new ArgumentException(SR.Get("InvalidStylusPointDescriptionTooManyButtons"), "stylusPointPropertyInfos");
			}
			this._buttonCount = num;
			this._stylusPointPropertyInfos = list.ToArray();
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004D1E0 File Offset: 0x0004C5E0
		internal StylusPointDescription(IEnumerable<StylusPointPropertyInfo> stylusPointPropertyInfos, int originalPressureIndex) : this(stylusPointPropertyInfos)
		{
			this._originalPressureIndex = originalPressureIndex;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Input.StylusPointDescription" /> atual tem a propriedade especificada.</summary>
		/// <param name="stylusPointProperty">O <see cref="T:System.Windows.Input.StylusPointProperty" /> a ser verificado no <see cref="T:System.Windows.Input.StylusPointDescription" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Input.StylusPointDescription" /> tiver o <see cref="T:System.Windows.Input.StylusPointProperty" /> especificado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014E2 RID: 5346 RVA: 0x0004D1FC File Offset: 0x0004C5FC
		public bool HasProperty(StylusPointProperty stylusPointProperty)
		{
			if (stylusPointProperty == null)
			{
				throw new ArgumentNullException("stylusPointProperty");
			}
			int num = this.IndexOf(stylusPointProperty.Id);
			return -1 != num;
		}

		/// <summary>Obtém o número de propriedades no <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		/// <returns>O número de propriedades no <see cref="T:System.Windows.Input.StylusPointDescription" />.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0004D22C File Offset: 0x0004C62C
		public int PropertyCount
		{
			get
			{
				return this._stylusPointPropertyInfos.Length;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> da propriedade especificada.</summary>
		/// <param name="stylusPointProperty">O <see cref="T:System.Windows.Input.StylusPointProperty" /> que especifica a propriedade do <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> desejado.</param>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> para o <see cref="T:System.Windows.Input.StylusPointProperty" /> especificado.</returns>
		// Token: 0x060014E4 RID: 5348 RVA: 0x0004D244 File Offset: 0x0004C644
		public StylusPointPropertyInfo GetPropertyInfo(StylusPointProperty stylusPointProperty)
		{
			if (stylusPointProperty == null)
			{
				throw new ArgumentNullException("stylusPointProperty");
			}
			return this.GetPropertyInfo(stylusPointProperty.Id);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004D26C File Offset: 0x0004C66C
		internal StylusPointPropertyInfo GetPropertyInfo(Guid guid)
		{
			int num = this.IndexOf(guid);
			if (-1 == num)
			{
				throw new ArgumentException("stylusPointProperty");
			}
			return this._stylusPointPropertyInfos[num];
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004D298 File Offset: 0x0004C698
		internal int GetPropertyIndex(Guid guid)
		{
			return this.IndexOf(guid);
		}

		/// <summary>Obtém todas as propriedades do <see cref="T:System.Windows.Input.StylusPointDescription" />.</summary>
		/// <returns>Uma coleção que contém todos os objetos <see cref="T:System.Windows.Input.StylusPointPropertyInfo" /> no <see cref="T:System.Windows.Input.StylusPointDescription" />.</returns>
		// Token: 0x060014E7 RID: 5351 RVA: 0x0004D2AC File Offset: 0x0004C6AC
		public ReadOnlyCollection<StylusPointPropertyInfo> GetStylusPointProperties()
		{
			return new ReadOnlyCollection<StylusPointPropertyInfo>(this._stylusPointPropertyInfos);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004D2C4 File Offset: 0x0004C6C4
		internal Guid[] GetStylusPointPropertyIds()
		{
			Guid[] array = new Guid[this._stylusPointPropertyInfos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._stylusPointPropertyInfos[i].Id;
			}
			return array;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004D304 File Offset: 0x0004C704
		internal int GetInputArrayLengthPerPoint()
		{
			int num = (this._buttonCount > 0) ? 1 : 0;
			int num2 = this._stylusPointPropertyInfos.Length - this._buttonCount + num;
			if (!this.ContainsTruePressure)
			{
				num2--;
			}
			return num2;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004D340 File Offset: 0x0004C740
		internal int GetExpectedAdditionalDataCount()
		{
			int num = (this._buttonCount > 0) ? 1 : 0;
			return this._stylusPointPropertyInfos.Length - this._buttonCount + num - 3;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004D370 File Offset: 0x0004C770
		internal int GetOutputArrayLengthPerPoint()
		{
			int num = this.GetInputArrayLengthPerPoint();
			if (!this.ContainsTruePressure)
			{
				num++;
			}
			return num;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0004D394 File Offset: 0x0004C794
		internal int ButtonCount
		{
			get
			{
				return this._buttonCount;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004D3A8 File Offset: 0x0004C7A8
		internal int GetButtonBitPosition(StylusPointProperty buttonProperty)
		{
			if (!buttonProperty.IsButton)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			for (int i = this._stylusPointPropertyInfos.Length - this._buttonCount; i < this._stylusPointPropertyInfos.Length; i++)
			{
				if (this._stylusPointPropertyInfos[i].Id == buttonProperty.Id)
				{
					return num;
				}
				if (this._stylusPointPropertyInfos[i].IsButton)
				{
					num++;
				}
			}
			return -1;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0004D418 File Offset: 0x0004C818
		internal bool ContainsTruePressure
		{
			get
			{
				return this._originalPressureIndex != -1;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0004D434 File Offset: 0x0004C834
		internal int OriginalPressureIndex
		{
			get
			{
				return this._originalPressureIndex;
			}
		}

		/// <summary>Retorna um valor que indica se os objetos <see cref="T:System.Windows.Input.StylusPointDescription" /> especificados são idênticos.</summary>
		/// <param name="stylusPointDescription1">O primeiro <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser verificado.</param>
		/// <param name="stylusPointDescription2">O segundo <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser verificado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos <see cref="T:System.Windows.Input.StylusPointDescription" /> forem idênticos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014F0 RID: 5360 RVA: 0x0004D448 File Offset: 0x0004C848
		public static bool AreCompatible(StylusPointDescription stylusPointDescription1, StylusPointDescription stylusPointDescription2)
		{
			if (stylusPointDescription1 == null || stylusPointDescription2 == null)
			{
				throw new ArgumentNullException("stylusPointDescription");
			}
			if (stylusPointDescription1._stylusPointPropertyInfos.Length != stylusPointDescription2._stylusPointPropertyInfos.Length)
			{
				return false;
			}
			for (int i = StylusPointDescription.RequiredCountOfProperties; i < stylusPointDescription1._stylusPointPropertyInfos.Length; i++)
			{
				if (!StylusPointPropertyInfo.AreCompatible(stylusPointDescription1._stylusPointPropertyInfos[i], stylusPointDescription2._stylusPointPropertyInfos[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Retorna a interseção dos objetos <see cref="T:System.Windows.Input.StylusPointDescription" /> especificados.</summary>
		/// <param name="stylusPointDescription">O primeiro <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser interseccionado.</param>
		/// <param name="stylusPointDescriptionPreserveInfo">O segundo <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser interseccionado.</param>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointDescription" /> que contém as propriedades presentes se ambos os objetos <see cref="T:System.Windows.Input.StylusPointDescription" /> especificados estiverem presentes.</returns>
		// Token: 0x060014F1 RID: 5361 RVA: 0x0004D4AC File Offset: 0x0004C8AC
		public static StylusPointDescription GetCommonDescription(StylusPointDescription stylusPointDescription, StylusPointDescription stylusPointDescriptionPreserveInfo)
		{
			if (stylusPointDescription == null)
			{
				throw new ArgumentNullException("stylusPointDescription");
			}
			if (stylusPointDescriptionPreserveInfo == null)
			{
				throw new ArgumentNullException("stylusPointDescriptionPreserveInfo");
			}
			List<StylusPointPropertyInfo> list = new List<StylusPointPropertyInfo>();
			list.Add(stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos[0]);
			list.Add(stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos[1]);
			list.Add(stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos[2]);
			for (int i = StylusPointDescription.RequiredCountOfProperties; i < stylusPointDescription._stylusPointPropertyInfos.Length; i++)
			{
				for (int j = StylusPointDescription.RequiredCountOfProperties; j < stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos.Length; j++)
				{
					if (StylusPointPropertyInfo.AreCompatible(stylusPointDescription._stylusPointPropertyInfos[i], stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos[j]))
					{
						list.Add(stylusPointDescriptionPreserveInfo._stylusPointPropertyInfos[j]);
					}
				}
			}
			return new StylusPointDescription(list);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.Input.StylusPointDescription" /> atual é um subconjunto do <see cref="T:System.Windows.Input.StylusPointDescription" /> especificado.</summary>
		/// <param name="stylusPointDescriptionSuperset">O <see cref="T:System.Windows.Input.StylusPointDescription" /> com relação ao qual verificar se o <see cref="T:System.Windows.Input.StylusPointDescription" /> atual é um subconjunto.</param>
		/// <returns>
		///   <see langword="true" /> se a <see cref="T:System.Windows.Input.StylusPointDescription" /> atual for um subconjunto da <see cref="T:System.Windows.Input.StylusPointDescription" /> especificada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060014F2 RID: 5362 RVA: 0x0004D560 File Offset: 0x0004C960
		public bool IsSubsetOf(StylusPointDescription stylusPointDescriptionSuperset)
		{
			if (stylusPointDescriptionSuperset == null)
			{
				throw new ArgumentNullException("stylusPointDescriptionSuperset");
			}
			if (stylusPointDescriptionSuperset._stylusPointPropertyInfos.Length < this._stylusPointPropertyInfos.Length)
			{
				return false;
			}
			for (int i = 0; i < this._stylusPointPropertyInfos.Length; i++)
			{
				Guid id = this._stylusPointPropertyInfos[i].Id;
				if (-1 == stylusPointDescriptionSuperset.IndexOf(id))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004D5C0 File Offset: 0x0004C9C0
		private int IndexOf(Guid propertyId)
		{
			for (int i = 0; i < this._stylusPointPropertyInfos.Length; i++)
			{
				if (this._stylusPointPropertyInfos[i].Id == propertyId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000B10 RID: 2832
		internal static readonly int RequiredCountOfProperties = 3;

		// Token: 0x04000B11 RID: 2833
		internal static readonly int RequiredXIndex = 0;

		// Token: 0x04000B12 RID: 2834
		internal static readonly int RequiredYIndex = 1;

		// Token: 0x04000B13 RID: 2835
		internal static readonly int RequiredPressureIndex = 2;

		// Token: 0x04000B14 RID: 2836
		internal static readonly int MaximumButtonCount = 31;

		// Token: 0x04000B15 RID: 2837
		private int _buttonCount;

		// Token: 0x04000B16 RID: 2838
		private int _originalPressureIndex = StylusPointDescription.RequiredPressureIndex;

		// Token: 0x04000B17 RID: 2839
		private StylusPointPropertyInfo[] _stylusPointPropertyInfos;
	}
}
