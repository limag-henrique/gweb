using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Input
{
	/// <summary>Encapsula um evento de entrada quando ele está sendo processado pelo gerenciador de entrada.</summary>
	// Token: 0x02000299 RID: 665
	public class StagingAreaInputItem
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x00048418 File Offset: 0x00047818
		internal StagingAreaInputItem(bool isMarker)
		{
			this._isMarker = isMarker;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00048434 File Offset: 0x00047834
		internal void Reset(InputEventArgs input, StagingAreaInputItem promote)
		{
			this._input = input;
			if (promote != null && promote._dictionary != null)
			{
				this._dictionary = (Hashtable)promote._dictionary.Clone();
				return;
			}
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			this._dictionary = new Hashtable();
		}

		/// <summary>Obtém os dados de evento de entrada associados a este objeto <see cref="T:System.Windows.Input.StagingAreaInputItem" /></summary>
		/// <returns>O evento.</returns>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0004848C File Offset: 0x0004788C
		public InputEventArgs Input
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this._input;
			}
		}

		/// <summary>Obtém os dados de entrada associados à chave especificada.</summary>
		/// <param name="key">Uma chave arbitrária para os dados. Esse não pode ser <see langword="null" />.</param>
		/// <returns>Os dados para essa chave ou <see langword="null" />.</returns>
		// Token: 0x06001359 RID: 4953 RVA: 0x000484A0 File Offset: 0x000478A0
		public object GetData(object key)
		{
			return this._dictionary[key];
		}

		/// <summary>Cria uma entrada de dicionário usando a chave e os dados especificados.</summary>
		/// <param name="key">Uma chave arbitrária para os dados. Esse não pode ser <see langword="null" />.</param>
		/// <param name="value">Os dados a serem definidos para esta chave. Ele pode ser <see langword="null" />.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x000484BC File Offset: 0x000478BC
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void SetData(object key, object value)
		{
			this._dictionary[key] = value;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000484D8 File Offset: 0x000478D8
		internal bool IsMarker
		{
			get
			{
				return this._isMarker;
			}
		}

		// Token: 0x04000A8B RID: 2699
		private bool _isMarker;

		// Token: 0x04000A8C RID: 2700
		private InputEventArgs _input;

		// Token: 0x04000A8D RID: 2701
		private Hashtable _dictionary;
	}
}
