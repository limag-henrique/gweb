using System;

namespace System.Windows.Media
{
	/// <summary>Fornece dados para os eventos de <see cref="E:System.Windows.Controls.MediaElement.ScriptCommand" /> e de <see cref="E:System.Windows.Media.MediaPlayer.ScriptCommand" /> .</summary>
	// Token: 0x02000424 RID: 1060
	public sealed class MediaScriptCommandEventArgs : EventArgs
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x000ABD00 File Offset: 0x000AB100
		internal MediaScriptCommandEventArgs(string parameterType, string parameterValue)
		{
			if (parameterType == null)
			{
				throw new ArgumentNullException("parameterType");
			}
			if (parameterValue == null)
			{
				throw new ArgumentNullException("parameterValue");
			}
			this._parameterType = parameterType;
			this._parameterValue = parameterValue;
		}

		/// <summary>Obtém o tipo do comando de script que foi acionado.</summary>
		/// <returns>O tipo de comando de script.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000ABD40 File Offset: 0x000AB140
		public string ParameterType
		{
			get
			{
				return this._parameterType;
			}
		}

		/// <summary>Obtém os argumentos associados com o tipo de comando de script.</summary>
		/// <returns>Os argumentos associados com o tipo de comando de script.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000ABD54 File Offset: 0x000AB154
		public string ParameterValue
		{
			get
			{
				return this._parameterValue;
			}
		}

		// Token: 0x040013A7 RID: 5031
		private string _parameterType;

		// Token: 0x040013A8 RID: 5032
		private string _parameterValue;
	}
}
