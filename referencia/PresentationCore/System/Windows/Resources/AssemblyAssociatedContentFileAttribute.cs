using System;

namespace System.Windows.Resources
{
	/// <summary>Esse atributo é interpretado durante o processo de compilação da linguagem XAML para associar o conteúdo flexível a um aplicativo WPF (Windows Presentation Foundation).</summary>
	// Token: 0x020001FD RID: 509
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class AssemblyAssociatedContentFileAttribute : Attribute
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Resources.AssemblyAssociatedContentFileAttribute" />.</summary>
		/// <param name="relativeContentFilePath">O caminho para o conteúdo associado.</param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x00032420 File Offset: 0x00031820
		public AssemblyAssociatedContentFileAttribute(string relativeContentFilePath)
		{
			this._path = relativeContentFilePath;
		}

		/// <summary>Obtém o caminho para o conteúdo associado.</summary>
		/// <returns>O caminho, conforme declarado no atributo.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0003243C File Offset: 0x0003183C
		public string RelativeContentFilePath
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x040007FB RID: 2043
		private string _path;
	}
}
