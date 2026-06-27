using System;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa uma propriedade armazenada em um <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
	// Token: 0x020002BD RID: 701
	public class StylusPointProperty
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointProperty" /> usando o GUID especificado.</summary>
		/// <param name="identifier">O <see cref="T:System.Guid" /> que identifica exclusivamente o <see cref="T:System.Windows.Input.StylusPointProperty" />.</param>
		/// <param name="isButton">
		///   <see langword="true" /> para indicar que a propriedade representa um botão na caneta; caso contrário, <see langword="false" />.</param>
		// Token: 0x060014F6 RID: 5366 RVA: 0x0004D794 File Offset: 0x0004CB94
		public StylusPointProperty(Guid identifier, bool isButton)
		{
			this.Initialize(identifier, isButton);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPointProperty" /> copiando o <see cref="T:System.Windows.Input.StylusPointProperty" /> especificado.</summary>
		/// <param name="stylusPointProperty">O <see cref="T:System.Windows.Input.StylusPointProperty" /> para cópia.</param>
		// Token: 0x060014F7 RID: 5367 RVA: 0x0004D7B0 File Offset: 0x0004CBB0
		protected StylusPointProperty(StylusPointProperty stylusPointProperty)
		{
			if (stylusPointProperty == null)
			{
				throw new ArgumentNullException("stylusPointProperty");
			}
			this.Initialize(stylusPointProperty.Id, stylusPointProperty.IsButton);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0004D7E4 File Offset: 0x0004CBE4
		private void Initialize(Guid identifier, bool isButton)
		{
			if (StylusPointPropertyIds.IsKnownButton(identifier))
			{
				if (!isButton)
				{
					throw new ArgumentException(SR.Get("InvalidIsButtonForId"), "isButton");
				}
			}
			else if (StylusPointPropertyIds.IsKnownId(identifier) && isButton)
			{
				throw new ArgumentException(SR.Get("InvalidIsButtonForId2"), "isButton");
			}
			this._id = identifier;
			this._isButton = isButton;
		}

		/// <summary>Obtém o GUID para o <see cref="T:System.Windows.Input.StylusPointProperty" /> atual.</summary>
		/// <returns>O GUID para a atual <see cref="T:System.Windows.Input.StylusPointProperty" />.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0004D840 File Offset: 0x0004CC40
		public Guid Id
		{
			get
			{
				return this._id;
			}
		}

		/// <summary>Obtém se <see cref="T:System.Windows.Input.StylusPointProperty" /> representa um botão na caneta.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Input.StylusPointProperty" /> representa um botão na caneta; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0004D854 File Offset: 0x0004CC54
		public bool IsButton
		{
			get
			{
				return this._isButton;
			}
		}

		/// <summary>Retorna uma cadeia de caracteres que representa o objeto atual.</summary>
		/// <returns>Uma cadeia de caracteres que representa o objeto atual.</returns>
		// Token: 0x060014FB RID: 5371 RVA: 0x0004D868 File Offset: 0x0004CC68
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Id=",
				StylusPointPropertyIds.GetStringRepresentation(this._id),
				", IsButton=",
				this._isButton.ToString(CultureInfo.InvariantCulture),
				"}"
			});
		}

		// Token: 0x04000B2E RID: 2862
		private Guid _id;

		// Token: 0x04000B2F RID: 2863
		private bool _isButton;
	}
}
