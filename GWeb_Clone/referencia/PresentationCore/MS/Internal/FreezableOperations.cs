using System;
using System.Windows;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000678 RID: 1656
	[FriendAccessAllowed]
	internal static class FreezableOperations
	{
		// Token: 0x0600491B RID: 18715 RVA: 0x0011D4B0 File Offset: 0x0011C8B0
		internal static Freezable Clone(Freezable freezable)
		{
			if (freezable == null)
			{
				return null;
			}
			return freezable.Clone();
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x0011D4C8 File Offset: 0x0011C8C8
		public static Freezable GetAsFrozen(Freezable freezable)
		{
			if (freezable == null)
			{
				return null;
			}
			return freezable.GetAsFrozen();
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x0011D4E0 File Offset: 0x0011C8E0
		internal static Freezable GetAsFrozenIfPossible(Freezable freezable)
		{
			if (freezable == null)
			{
				return null;
			}
			if (freezable.CanFreeze)
			{
				freezable = freezable.GetAsFrozen();
			}
			return freezable;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x0011D504 File Offset: 0x0011C904
		internal static void PropagateChangedHandlers(Freezable oldValue, Freezable newValue, EventHandler changedHandler)
		{
			if (newValue != null && !newValue.IsFrozen)
			{
				newValue.Changed += changedHandler;
			}
			if (oldValue != null && !oldValue.IsFrozen)
			{
				oldValue.Changed -= changedHandler;
			}
		}
	}
}
