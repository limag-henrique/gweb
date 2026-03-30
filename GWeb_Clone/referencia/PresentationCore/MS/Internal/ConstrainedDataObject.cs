using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;

namespace MS.Internal
{
	// Token: 0x0200068C RID: 1676
	internal sealed class ConstrainedDataObject : IDataObject
	{
		// Token: 0x060049C1 RID: 18881 RVA: 0x0011F488 File Offset: 0x0011E888
		[SecurityCritical]
		internal ConstrainedDataObject(IDataObject data)
		{
			Invariant.Assert(data != null);
			this._innerData = data;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0011F4AC File Offset: 0x0011E8AC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public object GetData(string format, bool autoConvert)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (this.IsCriticalFormat(format))
			{
				return null;
			}
			return this._innerData.GetData(format, autoConvert);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0011F4E0 File Offset: 0x0011E8E0
		public object GetData(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetData(format, true);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0011F504 File Offset: 0x0011E904
		public object GetData(Type format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetData(format.FullName);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0011F534 File Offset: 0x0011E934
		public bool GetDataPresent(Type format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetDataPresent(format.FullName);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0011F564 File Offset: 0x0011E964
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public bool GetDataPresent(string format, bool autoConvert)
		{
			bool result = false;
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (!this.IsCriticalFormat(format))
			{
				result = this._innerData.GetDataPresent(format, autoConvert);
			}
			return result;
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0011F59C File Offset: 0x0011E99C
		public bool GetDataPresent(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return this.GetDataPresent(format, true);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0011F5C0 File Offset: 0x0011E9C0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public string[] GetFormats(bool autoConvert)
		{
			string[] formats = this._innerData.GetFormats(autoConvert);
			if (formats != null)
			{
				this.StripCriticalFormats(formats);
			}
			return formats;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0011F5E8 File Offset: 0x0011E9E8
		public string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0011F5FC File Offset: 0x0011E9FC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void SetData(object data)
		{
			this._innerData.SetData(data);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0011F618 File Offset: 0x0011EA18
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void SetData(string format, object data)
		{
			this._innerData.SetData(format, data);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0011F634 File Offset: 0x0011EA34
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void SetData(Type format, object data)
		{
			this._innerData.SetData(format, data);
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0011F650 File Offset: 0x0011EA50
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void SetData(string format, object data, bool autoConvert)
		{
			this._innerData.SetData(format, data, autoConvert);
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0011F66C File Offset: 0x0011EA6C
		private static bool IsFormatEqual(string format1, string format2)
		{
			return string.CompareOrdinal(format1, format2) == 0;
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x0011F684 File Offset: 0x0011EA84
		private string[] StripCriticalFormats(string[] formats)
		{
			List<string> list = new List<string>();
			uint num = 0U;
			while ((ulong)num < (ulong)((long)formats.Length))
			{
				if (!this.IsCriticalFormat(formats[(int)num]))
				{
					list.Add(formats[(int)num]);
				}
				num += 1U;
			}
			return list.ToArray();
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0011F6C4 File Offset: 0x0011EAC4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool IsCriticalFormat(string format)
		{
			return ConstrainedDataObject.IsFormatEqual(format, DataFormats.Xaml) || ConstrainedDataObject.IsFormatEqual(format, DataFormats.ApplicationTrust);
		}

		// Token: 0x04001D10 RID: 7440
		[SecurityCritical]
		private IDataObject _innerData;
	}
}
