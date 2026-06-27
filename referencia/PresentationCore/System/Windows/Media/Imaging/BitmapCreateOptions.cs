using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Especifica as opções de inicialização para imagens bitmap.</summary>
	// Token: 0x020005D1 RID: 1489
	[Flags]
	public enum BitmapCreateOptions
	{
		/// <summary>Nenhum <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> é especificado. Este é o valor padrão.</summary>
		// Token: 0x0400186A RID: 6250
		None = 0,
		/// <summary>Garante que o <see cref="T:System.Windows.Media.PixelFormat" /> no qual um arquivo é armazenado seja o mesmo ao qual ele é carregado.</summary>
		// Token: 0x0400186B RID: 6251
		PreservePixelFormat = 1,
		/// <summary>Faz com que um objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> atrase a inicialização até que seja necessário. Isso é útil ao lidar com coleções de imagens.</summary>
		// Token: 0x0400186C RID: 6252
		DelayCreation = 2,
		/// <summary>Faz com que um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> ignore um perfil de cor incorporado.</summary>
		// Token: 0x0400186D RID: 6253
		IgnoreColorProfile = 4,
		/// <summary>Carrega imagens sem usar um cache de imagem existente. Esta opção deve ser selecionada apenas quando as imagens em um cache precisarem ser atualizadas.</summary>
		// Token: 0x0400186E RID: 6254
		IgnoreImageCache = 8
	}
}
