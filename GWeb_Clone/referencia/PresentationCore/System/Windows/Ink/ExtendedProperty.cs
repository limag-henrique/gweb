using System;
using MS.Internal.Ink.InkSerializedFormat;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	// Token: 0x02000338 RID: 824
	internal sealed class ExtendedProperty
	{
		// Token: 0x06001BF0 RID: 7152 RVA: 0x00071AC0 File Offset: 0x00070EC0
		internal ExtendedProperty(Guid id, object value)
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentException(SR.Get("InvalidGuid"));
			}
			this._id = id;
			this.Value = value;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00071B00 File Offset: 0x00070F00
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00071B30 File Offset: 0x00070F30
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			ExtendedProperty extendedProperty = (ExtendedProperty)obj;
			if (extendedProperty.Id == this.Id)
			{
				Type type = this.Value.GetType();
				Type type2 = extendedProperty.Value.GetType();
				if (!type.IsArray || !type2.IsArray)
				{
					return extendedProperty.Value.Equals(this.Value);
				}
				Type elementType = type.GetElementType();
				Type elementType2 = type2.GetElementType();
				if (elementType == elementType2 && elementType.IsValueType && type.GetArrayRank() == 1 && elementType2.IsValueType && type2.GetArrayRank() == 1)
				{
					Array array = (Array)this.Value;
					Array array2 = (Array)extendedProperty.Value;
					if (array.Length == array2.Length)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (!array.GetValue(i).Equals(array2.GetValue(i)))
							{
								return false;
							}
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00071C54 File Offset: 0x00071054
		public static bool operator ==(ExtendedProperty first, ExtendedProperty second)
		{
			return (first == null && second == null) || (first != null && second != null && first.Equals(second));
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00071C78 File Offset: 0x00071078
		public static bool operator !=(ExtendedProperty first, ExtendedProperty second)
		{
			return !(first == second);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00071C90 File Offset: 0x00071090
		public override string ToString()
		{
			string str;
			if (this.Value == null)
			{
				str = "<undefined value>";
			}
			else if (this.Value is string)
			{
				str = "\"" + this.Value.ToString() + "\"";
			}
			else
			{
				str = this.Value.ToString();
			}
			return KnownIds.ConvertToString(this.Id) + "," + str;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00071CFC File Offset: 0x000710FC
		internal Guid Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00071D10 File Offset: 0x00071110
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00071D24 File Offset: 0x00071124
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ExtendedPropertySerializer.Validate(this._id, value);
				this._value = value;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00071D54 File Offset: 0x00071154
		internal ExtendedProperty Clone()
		{
			Guid id = this._id;
			Type type = this._value.GetType();
			if (type.IsValueType || type == typeof(string))
			{
				return new ExtendedProperty(id, this._value);
			}
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				if (elementType.IsValueType && type.GetArrayRank() == 1)
				{
					Array array = Array.CreateInstance(elementType, ((Array)this._value).Length);
					Array.Copy((Array)this._value, array, ((Array)this._value).Length);
					return new ExtendedProperty(id, array);
				}
			}
			throw new InvalidOperationException(SR.Get("InvalidDataTypeForExtendedProperty"));
		}

		// Token: 0x04000F2B RID: 3883
		private Guid _id;

		// Token: 0x04000F2C RID: 3884
		private object _value;
	}
}
