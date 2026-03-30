using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000449 RID: 1097
	internal class MapClass
	{
		// Token: 0x06002CBA RID: 11450 RVA: 0x000B286C File Offset: 0x000B1C6C
		internal MapClass()
		{
			this._map_ofBrushes = default(DUCE.Map<bool>);
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000B288C File Offset: 0x000B1C8C
		internal bool IsEmpty
		{
			get
			{
				return this._map_ofBrushes.IsEmpty();
			}
		}

		// Token: 0x0400146A RID: 5226
		public DUCE.Map<bool> _map_ofBrushes;
	}
}
