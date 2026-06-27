using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Windows.Input
{
	/// <summary>Contém uma coleção de objetos <see cref="T:System.Windows.Input.StylusButton" /> .</summary>
	// Token: 0x020002AD RID: 685
	public class StylusButtonCollection : ReadOnlyCollection<StylusButton>
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x0004B3B8 File Offset: 0x0004A7B8
		internal StylusButtonCollection(StylusButton[] buttons) : base(new List<StylusButton>(buttons))
		{
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004B3D4 File Offset: 0x0004A7D4
		internal StylusButtonCollection(List<StylusButton> buttons) : base(buttons)
		{
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusButton" /> que o GUID especificado identifica.</summary>
		/// <param name="guid">O <see cref="T:System.Guid" /> que especifica o <see cref="T:System.Windows.Input.StylusButton" /> desejado.</param>
		/// <returns>O <see cref="T:System.Windows.Input.StylusButton" /> do GUID especificado.</returns>
		// Token: 0x0600143C RID: 5180 RVA: 0x0004B3E8 File Offset: 0x0004A7E8
		public StylusButton GetStylusButtonByGuid(Guid guid)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Guid == guid)
				{
					return base[i];
				}
			}
			return null;
		}
	}
}
