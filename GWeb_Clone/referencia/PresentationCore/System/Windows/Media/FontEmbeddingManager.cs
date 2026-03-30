using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece funcionalidade para inserção de fonte física e de composição.</summary>
	// Token: 0x02000394 RID: 916
	public class FontEmbeddingManager
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FontEmbeddingManager" />.</summary>
		// Token: 0x06002246 RID: 8774 RVA: 0x0008A414 File Offset: 0x00089814
		public FontEmbeddingManager()
		{
			this._collectedGlyphTypefaces = new Dictionary<Uri, Dictionary<ushort, bool>>(FontEmbeddingManager._uriComparer);
		}

		/// <summary>Inicia a coleta de informações de uso sobre a face de tipos de glifo e os índices usados por um <see cref="T:System.Windows.Media.GlyphRun" /> especificado.</summary>
		/// <param name="glyphRun">O <see cref="T:System.Windows.Media.GlyphRun" /> cujas informações de uso são coletadas.</param>
		// Token: 0x06002247 RID: 8775 RVA: 0x0008A438 File Offset: 0x00089838
		public void RecordUsage(GlyphRun glyphRun)
		{
			if (glyphRun == null)
			{
				throw new ArgumentNullException("glyphRun");
			}
			Uri fontUri = glyphRun.GlyphTypeface.FontUri;
			Dictionary<ushort, bool> dictionary;
			if (this._collectedGlyphTypefaces.ContainsKey(fontUri))
			{
				dictionary = this._collectedGlyphTypefaces[fontUri];
			}
			else
			{
				dictionary = (this._collectedGlyphTypefaces[fontUri] = new Dictionary<ushort, bool>());
			}
			foreach (ushort key in glyphRun.GlyphIndices)
			{
				dictionary[key] = true;
			}
		}

		/// <summary>Retorna a coleção de faces de tipos de glifo usada pelo <see cref="T:System.Windows.Media.GlyphRun" /> especificado no método <see cref="M:System.Windows.Media.FontEmbeddingManager.RecordUsage(System.Windows.Media.GlyphRun)" />.</summary>
		/// <returns>Uma coleção de <see cref="T:System.Uri" /> valores que representam os tipos de glifo.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0008A4E0 File Offset: 0x000898E0
		[CLSCompliant(false)]
		public ICollection<Uri> GlyphTypefaceUris
		{
			get
			{
				return this._collectedGlyphTypefaces.Keys;
			}
		}

		/// <summary>Recupera a lista de glifos usada pela face de tipos de glifo.</summary>
		/// <param name="glyphTypeface">Um valor <see cref="T:System.Uri" /> que representa o local da face de tipos de glifo que contêm os glifos.</param>
		/// <returns>Uma coleção de valores <see cref="T:System.UInt16" /> que representam os glifos.</returns>
		/// <exception cref="T:System.ArgumentException">O valor <paramref name="glyphTypeface" /> não faz referência a uma face de tipos de glifo gravada anteriormente.</exception>
		// Token: 0x06002249 RID: 8777 RVA: 0x0008A4F8 File Offset: 0x000898F8
		[CLSCompliant(false)]
		public ICollection<ushort> GetUsedGlyphs(Uri glyphTypeface)
		{
			Dictionary<ushort, bool> dictionary = this._collectedGlyphTypefaces[glyphTypeface];
			if (dictionary == null)
			{
				throw new ArgumentException(SR.Get("GlyphTypefaceNotRecorded"), "glyphTypeface");
			}
			return dictionary.Keys;
		}

		// Token: 0x040010F1 RID: 4337
		private Dictionary<Uri, Dictionary<ushort, bool>> _collectedGlyphTypefaces;

		// Token: 0x040010F2 RID: 4338
		private static FontEmbeddingManager.UriComparer _uriComparer = new FontEmbeddingManager.UriComparer();

		// Token: 0x0200086C RID: 2156
		private class UriComparer : IEqualityComparer<Uri>
		{
			// Token: 0x06005758 RID: 22360 RVA: 0x00164EC8 File Offset: 0x001642C8
			public bool Equals(Uri x, Uri y)
			{
				return string.Equals(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06005759 RID: 22361 RVA: 0x00164EE8 File Offset: 0x001642E8
			public int GetHashCode(Uri obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
