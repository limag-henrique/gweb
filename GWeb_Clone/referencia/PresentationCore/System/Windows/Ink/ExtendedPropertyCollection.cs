using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	// Token: 0x02000339 RID: 825
	internal sealed class ExtendedPropertyCollection
	{
		// Token: 0x06001BFA RID: 7162 RVA: 0x00071E0C File Offset: 0x0007120C
		internal ExtendedPropertyCollection()
		{
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00071E34 File Offset: 0x00071234
		public override bool Equals(object o)
		{
			if (o == null || o.GetType() != base.GetType())
			{
				return false;
			}
			ExtendedPropertyCollection extendedPropertyCollection = (ExtendedPropertyCollection)o;
			if (extendedPropertyCollection.Count != this.Count)
			{
				return false;
			}
			for (int i = 0; i < extendedPropertyCollection.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < this._extendedProperties.Count; j++)
				{
					if (this._extendedProperties[j].Equals(extendedPropertyCollection[i]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00071EC0 File Offset: 0x000712C0
		public static bool operator ==(ExtendedPropertyCollection first, ExtendedPropertyCollection second)
		{
			return (first == null && second == null) || first == second || (first != null && second != null && first.Equals(second));
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00071EE8 File Offset: 0x000712E8
		public static bool operator !=(ExtendedPropertyCollection first, ExtendedPropertyCollection second)
		{
			return !(first == second);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00071F00 File Offset: 0x00071300
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00071F14 File Offset: 0x00071314
		internal bool Contains(Guid attributeId)
		{
			for (int i = 0; i < this._extendedProperties.Count; i++)
			{
				if (this._extendedProperties[i].Id == attributeId)
				{
					this._optimisticIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00071F5C File Offset: 0x0007135C
		internal ExtendedPropertyCollection Clone()
		{
			ExtendedPropertyCollection extendedPropertyCollection = new ExtendedPropertyCollection();
			for (int i = 0; i < this._extendedProperties.Count; i++)
			{
				extendedPropertyCollection.Add(this._extendedProperties[i].Clone());
			}
			return extendedPropertyCollection;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00071FA0 File Offset: 0x000713A0
		internal void Add(Guid id, object value)
		{
			if (this.Contains(id))
			{
				throw new ArgumentException(SR.Get("EPExists"), "id");
			}
			ExtendedProperty extendedProperty = new ExtendedProperty(id, value);
			this.Add(extendedProperty);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00071FDC File Offset: 0x000713DC
		internal void Remove(Guid id)
		{
			if (!this.Contains(id))
			{
				throw new ArgumentException(SR.Get("EPGuidNotFound"), "id");
			}
			ExtendedProperty extendedPropertyById = this.GetExtendedPropertyById(id);
			this._extendedProperties.Remove(extendedPropertyById);
			this._optimisticIndex = -1;
			if (this.Changed != null)
			{
				ExtendedPropertiesChangedEventArgs e = new ExtendedPropertiesChangedEventArgs(extendedPropertyById, null);
				this.Changed(this, e);
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00072040 File Offset: 0x00071440
		internal Guid[] GetGuidArray()
		{
			if (this._extendedProperties.Count > 0)
			{
				Guid[] array = new Guid[this._extendedProperties.Count];
				for (int i = 0; i < this._extendedProperties.Count; i++)
				{
					array[i] = this[i].Id;
				}
				return array;
			}
			return new Guid[0];
		}

		// Token: 0x17000539 RID: 1337
		internal object this[Guid attributeId]
		{
			get
			{
				ExtendedProperty extendedPropertyById = this.GetExtendedPropertyById(attributeId);
				if (extendedPropertyById == null)
				{
					throw new ArgumentException(SR.Get("EPNotFound"), "attributeId");
				}
				return extendedPropertyById.Value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < this._extendedProperties.Count; i++)
				{
					ExtendedProperty extendedProperty = this._extendedProperties[i];
					if (extendedProperty.Id == attributeId)
					{
						object value2 = extendedProperty.Value;
						extendedProperty.Value = value;
						if (this.Changed != null)
						{
							ExtendedPropertiesChangedEventArgs e = new ExtendedPropertiesChangedEventArgs(new ExtendedProperty(extendedProperty.Id, value2), extendedProperty);
							this.Changed(this, e);
						}
						return;
					}
				}
				ExtendedProperty extendedProperty2 = new ExtendedProperty(attributeId, value);
				this.Add(extendedProperty2);
			}
		}

		// Token: 0x1700053A RID: 1338
		internal ExtendedProperty this[int index]
		{
			get
			{
				return this._extendedProperties[index];
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0007218C File Offset: 0x0007158C
		internal int Count
		{
			get
			{
				return this._extendedProperties.Count;
			}
		}

		// Token: 0x14000179 RID: 377
		// (add) Token: 0x06001C08 RID: 7176 RVA: 0x000721A4 File Offset: 0x000715A4
		// (remove) Token: 0x06001C09 RID: 7177 RVA: 0x000721DC File Offset: 0x000715DC
		internal event ExtendedPropertiesChangedEventHandler Changed;

		// Token: 0x06001C0A RID: 7178 RVA: 0x00072214 File Offset: 0x00071614
		private void Add(ExtendedProperty extendedProperty)
		{
			this._extendedProperties.Add(extendedProperty);
			if (this.Changed != null)
			{
				ExtendedPropertiesChangedEventArgs e = new ExtendedPropertiesChangedEventArgs(null, extendedProperty);
				this.Changed(this, e);
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0007224C File Offset: 0x0007164C
		private ExtendedProperty GetExtendedPropertyById(Guid id)
		{
			if (this._optimisticIndex != -1 && this._optimisticIndex < this._extendedProperties.Count && this._extendedProperties[this._optimisticIndex].Id == id)
			{
				return this._extendedProperties[this._optimisticIndex];
			}
			for (int i = 0; i < this._extendedProperties.Count; i++)
			{
				if (this._extendedProperties[i].Id == id)
				{
					return this._extendedProperties[i];
				}
			}
			return null;
		}

		// Token: 0x04000F2E RID: 3886
		private List<ExtendedProperty> _extendedProperties = new List<ExtendedProperty>();

		// Token: 0x04000F2F RID: 3887
		private int _optimisticIndex = -1;
	}
}
