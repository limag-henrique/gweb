using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MS.Internal
{
	// Token: 0x0200068B RID: 1675
	internal class DeferredElementTreeState
	{
		// Token: 0x060049B9 RID: 18873 RVA: 0x0011F2DC File Offset: 0x0011E6DC
		public void SetCoreParent(DependencyObject element, DependencyObject parent)
		{
			if (!this._oldCoreParents.ContainsKey(element))
			{
				this._oldCoreParents[element] = parent;
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0011F304 File Offset: 0x0011E704
		public static DependencyObject GetCoreParent(DependencyObject element, DeferredElementTreeState treeState)
		{
			DependencyObject result = null;
			if (treeState != null && treeState._oldCoreParents.ContainsKey(element))
			{
				result = treeState._oldCoreParents[element];
			}
			else
			{
				Visual visual = element as Visual;
				if (visual != null)
				{
					result = VisualTreeHelper.GetParent(visual);
				}
				else
				{
					ContentElement contentElement = element as ContentElement;
					if (contentElement != null)
					{
						result = ContentOperations.GetParent(contentElement);
					}
					else
					{
						Visual3D visual3D = element as Visual3D;
						if (visual3D != null)
						{
							result = VisualTreeHelper.GetParent(visual3D);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0011F36C File Offset: 0x0011E76C
		public static DependencyObject GetInputElementParent(DependencyObject element, DeferredElementTreeState treeState)
		{
			DependencyObject dependencyObject = element;
			do
			{
				dependencyObject = DeferredElementTreeState.GetCoreParent(dependencyObject, treeState);
			}
			while (dependencyObject != null && !InputElement.IsValid(dependencyObject));
			return dependencyObject;
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0011F390 File Offset: 0x0011E790
		public void SetLogicalParent(DependencyObject element, DependencyObject parent)
		{
			if (!this._oldLogicalParents.ContainsKey(element))
			{
				this._oldLogicalParents[element] = parent;
			}
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0011F3B8 File Offset: 0x0011E7B8
		public static DependencyObject GetLogicalParent(DependencyObject element, DeferredElementTreeState treeState)
		{
			DependencyObject result = null;
			if (treeState != null && treeState._oldLogicalParents.ContainsKey(element))
			{
				result = treeState._oldLogicalParents[element];
			}
			else
			{
				UIElement uielement = element as UIElement;
				if (uielement != null)
				{
					result = uielement.GetUIParentCore();
				}
				ContentElement contentElement = element as ContentElement;
				if (contentElement != null)
				{
					result = contentElement.GetUIParentCore();
				}
			}
			return result;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0011F40C File Offset: 0x0011E80C
		public void Clear()
		{
			this._oldCoreParents.Clear();
			this._oldLogicalParents.Clear();
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060049BF RID: 18879 RVA: 0x0011F430 File Offset: 0x0011E830
		public bool IsEmpty
		{
			get
			{
				return this._oldCoreParents.Count == 0 && this._oldLogicalParents.Count == 0;
			}
		}

		// Token: 0x04001D0E RID: 7438
		private Dictionary<DependencyObject, DependencyObject> _oldCoreParents = new Dictionary<DependencyObject, DependencyObject>();

		// Token: 0x04001D0F RID: 7439
		private Dictionary<DependencyObject, DependencyObject> _oldLogicalParents = new Dictionary<DependencyObject, DependencyObject>();
	}
}
