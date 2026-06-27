using System;
using System.Collections.Generic;
using MS.Internal;

namespace System.Windows
{
	// Token: 0x020001D3 RID: 467
	internal abstract class ReverseInheritProperty
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x0002F9E0 File Offset: 0x0002EDE0
		internal ReverseInheritProperty(DependencyPropertyKey flagKey, CoreFlags flagCache, CoreFlags flagChanged) : this(flagKey, flagCache, flagChanged, CoreFlags.None, CoreFlags.None)
		{
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002F9F8 File Offset: 0x0002EDF8
		internal ReverseInheritProperty(DependencyPropertyKey flagKey, CoreFlags flagCache, CoreFlags flagChanged, CoreFlags flagOldOriginCache, CoreFlags flagNewOriginCache)
		{
			this.FlagKey = flagKey;
			this.FlagCache = flagCache;
			this.FlagChanged = flagChanged;
			this.FlagOldOriginCache = flagOldOriginCache;
			this.FlagNewOriginCache = flagNewOriginCache;
		}

		// Token: 0x06000C79 RID: 3193
		internal abstract void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue);

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002FA30 File Offset: 0x0002EE30
		internal void OnOriginValueChanged(DependencyObject oldOrigin, DependencyObject newOrigin, ref DeferredElementTreeState oldTreeState)
		{
			this.OnOriginValueChanged(oldOrigin, newOrigin, null, ref oldTreeState, null);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002FA48 File Offset: 0x0002EE48
		internal void OnOriginValueChanged(DependencyObject oldOrigin, DependencyObject newOrigin, IList<DependencyObject> otherOrigins, ref DeferredElementTreeState oldTreeState, Action<DependencyObject, bool> originChangedAction)
		{
			DeferredElementTreeState deferredElementTreeState = oldTreeState;
			oldTreeState = null;
			bool setOriginCacheFlag = originChangedAction != null && this.FlagOldOriginCache != CoreFlags.None && this.FlagNewOriginCache > CoreFlags.None;
			if (oldOrigin != null)
			{
				this.SetCacheFlagInAncestry(oldOrigin, false, deferredElementTreeState, true, setOriginCacheFlag);
			}
			if (newOrigin != null)
			{
				this.SetCacheFlagInAncestry(newOrigin, true, null, true, setOriginCacheFlag);
			}
			int num = (otherOrigins != null) ? otherOrigins.Count : 0;
			for (int i = 0; i < num; i++)
			{
				this.SetCacheFlagInAncestry(otherOrigins[i], true, null, false, false);
			}
			if (oldOrigin != null)
			{
				this.FirePropertyChangeInAncestry(oldOrigin, true, deferredElementTreeState, originChangedAction);
			}
			if (newOrigin != null)
			{
				this.FirePropertyChangeInAncestry(newOrigin, false, null, originChangedAction);
			}
			if (oldTreeState == null && deferredElementTreeState != null)
			{
				deferredElementTreeState.Clear();
				oldTreeState = deferredElementTreeState;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002FAEC File Offset: 0x0002EEEC
		private void SetCacheFlagInAncestry(DependencyObject element, bool newValue, DeferredElementTreeState treeState, bool shortCircuit, bool setOriginCacheFlag)
		{
			UIElement uie;
			ContentElement ce;
			UIElement3D uie3D;
			ReverseInheritProperty.CastElement(element, out uie, out ce, out uie3D);
			bool flag = ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, this.FlagCache);
			bool flag2 = setOriginCacheFlag && ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, newValue ? this.FlagNewOriginCache : this.FlagOldOriginCache);
			if (newValue != flag || (setOriginCacheFlag && !flag2) || !shortCircuit)
			{
				if (newValue != flag)
				{
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, this.FlagCache, newValue);
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, this.FlagChanged, !ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, this.FlagChanged));
				}
				if (setOriginCacheFlag && !flag2)
				{
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, newValue ? this.FlagNewOriginCache : this.FlagOldOriginCache, true);
				}
				if (ReverseInheritProperty.BlockReverseInheritance(uie, ce, uie3D))
				{
					return;
				}
				DependencyObject inputElementParent = DeferredElementTreeState.GetInputElementParent(element, treeState);
				DependencyObject logicalParent = DeferredElementTreeState.GetLogicalParent(element, treeState);
				if (inputElementParent != null)
				{
					this.SetCacheFlagInAncestry(inputElementParent, newValue, treeState, shortCircuit, setOriginCacheFlag);
				}
				if (logicalParent != null && logicalParent != inputElementParent)
				{
					this.SetCacheFlagInAncestry(logicalParent, newValue, treeState, shortCircuit, setOriginCacheFlag);
				}
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002FBE4 File Offset: 0x0002EFE4
		private void FirePropertyChangeInAncestry(DependencyObject element, bool oldValue, DeferredElementTreeState treeState, Action<DependencyObject, bool> originChangedAction)
		{
			UIElement uie;
			ContentElement ce;
			UIElement3D uie3D;
			ReverseInheritProperty.CastElement(element, out uie, out ce, out uie3D);
			bool flag = ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, this.FlagChanged);
			bool flag2 = this.FlagOldOriginCache != CoreFlags.None && ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, this.FlagOldOriginCache);
			bool flag3 = this.FlagNewOriginCache != CoreFlags.None && ReverseInheritProperty.IsFlagSet(uie, ce, uie3D, this.FlagNewOriginCache);
			if (flag || flag2 || flag3)
			{
				if (flag)
				{
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, this.FlagChanged, false);
					if (oldValue)
					{
						element.ClearValue(this.FlagKey);
					}
					else
					{
						element.SetValue(this.FlagKey, true);
					}
					this.FireNotifications(uie, ce, uie3D, oldValue);
				}
				if (flag2 || flag3)
				{
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, this.FlagOldOriginCache, false);
					ReverseInheritProperty.SetFlag(uie, ce, uie3D, this.FlagNewOriginCache, false);
					if (flag2 != flag3)
					{
						originChangedAction(element, oldValue);
					}
				}
				if (ReverseInheritProperty.BlockReverseInheritance(uie, ce, uie3D))
				{
					return;
				}
				DependencyObject inputElementParent = DeferredElementTreeState.GetInputElementParent(element, treeState);
				DependencyObject logicalParent = DeferredElementTreeState.GetLogicalParent(element, treeState);
				if (inputElementParent != null)
				{
					this.FirePropertyChangeInAncestry(inputElementParent, oldValue, treeState, originChangedAction);
				}
				if (logicalParent != null && logicalParent != inputElementParent)
				{
					this.FirePropertyChangeInAncestry(logicalParent, oldValue, treeState, originChangedAction);
				}
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002FD00 File Offset: 0x0002F100
		private static bool BlockReverseInheritance(UIElement uie, ContentElement ce, UIElement3D uie3D)
		{
			if (uie != null)
			{
				return uie.BlockReverseInheritance();
			}
			if (ce != null)
			{
				return ce.BlockReverseInheritance();
			}
			return uie3D != null && uie3D.BlockReverseInheritance();
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002FD2C File Offset: 0x0002F12C
		private static void SetFlag(UIElement uie, ContentElement ce, UIElement3D uie3D, CoreFlags flag, bool value)
		{
			if (uie != null)
			{
				uie.WriteFlag(flag, value);
				return;
			}
			if (ce != null)
			{
				ce.WriteFlag(flag, value);
				return;
			}
			if (uie3D != null)
			{
				uie3D.WriteFlag(flag, value);
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002FD60 File Offset: 0x0002F160
		private static bool IsFlagSet(UIElement uie, ContentElement ce, UIElement3D uie3D, CoreFlags flag)
		{
			if (uie != null)
			{
				return uie.ReadFlag(flag);
			}
			if (ce != null)
			{
				return ce.ReadFlag(flag);
			}
			return uie3D != null && uie3D.ReadFlag(flag);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002FD90 File Offset: 0x0002F190
		private static void CastElement(DependencyObject o, out UIElement uie, out ContentElement ce, out UIElement3D uie3D)
		{
			uie = (o as UIElement);
			ce = ((uie != null) ? null : (o as ContentElement));
			uie3D = ((uie != null || ce != null) ? null : (o as UIElement3D));
		}

		// Token: 0x04000730 RID: 1840
		protected DependencyPropertyKey FlagKey;

		// Token: 0x04000731 RID: 1841
		protected CoreFlags FlagCache;

		// Token: 0x04000732 RID: 1842
		protected CoreFlags FlagChanged;

		// Token: 0x04000733 RID: 1843
		protected CoreFlags FlagOldOriginCache;

		// Token: 0x04000734 RID: 1844
		protected CoreFlags FlagNewOriginCache;
	}
}
