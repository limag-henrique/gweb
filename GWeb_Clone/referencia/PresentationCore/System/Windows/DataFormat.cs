using System;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Representa um formato de dados usando um nome de formato e uma ID numérica.</summary>
	// Token: 0x02000194 RID: 404
	public sealed class DataFormat
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.DataFormat" />.</summary>
		/// <param name="name">O nome do formado de dados.</param>
		/// <param name="id">A ID de inteiro do formato de dados.</param>
		// Token: 0x0600058F RID: 1423 RVA: 0x0001A544 File Offset: 0x00019944
		public DataFormat(string name, int id)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.Get("DataObject_EmptyFormatNotAllowed"));
			}
			this._name = name;
			this._id = id;
		}

		/// <summary>Obtém o nome do formato de dados.</summary>
		/// <returns>O nome do formato de dados.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001A590 File Offset: 0x00019990
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Obtém a ID numérica do formato de dados.</summary>
		/// <returns>A ID numérica do formato de dados.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001A5A4 File Offset: 0x000199A4
		public int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0400054F RID: 1359
		private readonly string _name;

		// Token: 0x04000550 RID: 1360
		private readonly int _id;
	}
}
