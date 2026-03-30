using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Cache;
using System.Security;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Representa os dados de imagem retornados por um decodificador e aceitos pelo codificadores.</summary>
	// Token: 0x020005D9 RID: 1497
	public abstract class BitmapFrame : BitmapSource, IUriContext
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</summary>
		// Token: 0x060043BC RID: 17340 RVA: 0x00107DEC File Offset: 0x001071EC
		protected BitmapFrame()
		{
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00107E00 File Offset: 0x00107200
		internal BitmapFrame(bool useVirtuals) : base(useVirtuals)
		{
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00107E14 File Offset: 0x00107214
		internal static BitmapFrame CreateFromUriOrStream(Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, RequestCachePolicy uriCachePolicy)
		{
			if (uri != null)
			{
				BitmapDecoder bitmapDecoder = BitmapDecoder.CreateFromUriOrStream(baseUri, uri, null, createOptions, cacheOption, uriCachePolicy, true);
				if (bitmapDecoder.Frames.Count == 0)
				{
					throw new ArgumentException(SR.Get("Image_NoDecodeFrames"), "uri");
				}
				return bitmapDecoder.Frames[0];
			}
			else
			{
				BitmapDecoder bitmapDecoder2 = BitmapDecoder.Create(stream, createOptions, cacheOption);
				if (bitmapDecoder2.Frames.Count == 0)
				{
					throw new ArgumentException(SR.Get("Image_NoDecodeFrames"), "stream");
				}
				return bitmapDecoder2.Frames[0];
			}
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" />.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica a origem do <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" />.</returns>
		// Token: 0x060043BF RID: 17343 RVA: 0x00107EA0 File Offset: 0x001072A0
		public static BitmapFrame Create(Uri bitmapUri)
		{
			return BitmapFrame.Create(bitmapUri, null);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificado.</summary>
		/// <param name="bitmapUri">O local do bitmap do qual o <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> é criado.</param>
		/// <param name="uriCachePolicy">Os requisitos de cache para este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com o <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificado.</returns>
		// Token: 0x060043C0 RID: 17344 RVA: 0x00107EB4 File Offset: 0x001072B4
		public static BitmapFrame Create(Uri bitmapUri, RequestCachePolicy uriCachePolicy)
		{
			if (bitmapUri == null)
			{
				throw new ArgumentNullException("bitmapUri");
			}
			return BitmapFrame.CreateFromUriOrStream(null, bitmapUri, null, BitmapCreateOptions.None, BitmapCacheOption.Default, uriCachePolicy);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com os <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O local do bitmap do qual o <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> é criado.</param>
		/// <param name="createOptions">As opções usadas para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="cacheOption">As opção de cache usada para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> especificado e <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" />.</returns>
		// Token: 0x060043C1 RID: 17345 RVA: 0x00107EE0 File Offset: 0x001072E0
		public static BitmapFrame Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
		{
			return BitmapFrame.Create(bitmapUri, createOptions, cacheOption, null);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com os <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" />, <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> e <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificados.</summary>
		/// <param name="bitmapUri">O local do bitmap do qual o <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> é criado.</param>
		/// <param name="createOptions">As opções usadas para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="cacheOption">As opção de cache usada para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="uriCachePolicy">Os requisitos de cache para este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Uri" /> com os <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" />, <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> e <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificados.</returns>
		// Token: 0x060043C2 RID: 17346 RVA: 0x00107EF8 File Offset: 0x001072F8
		public static BitmapFrame Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, RequestCachePolicy uriCachePolicy)
		{
			if (bitmapUri == null)
			{
				throw new ArgumentNullException("bitmapUri");
			}
			return BitmapFrame.CreateFromUriOrStream(null, bitmapUri, null, createOptions, cacheOption, uriCachePolicy);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="bitmapStream">O <see cref="T:System.IO.Stream" /> usado para construir o <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.IO.Stream" />.</returns>
		// Token: 0x060043C3 RID: 17347 RVA: 0x00107F24 File Offset: 0x00107324
		public static BitmapFrame Create(Stream bitmapStream)
		{
			if (bitmapStream == null)
			{
				throw new ArgumentNullException("bitmapStream");
			}
			return BitmapFrame.CreateFromUriOrStream(null, null, bitmapStream, BitmapCreateOptions.None, BitmapCacheOption.Default, null);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.IO.Stream" /> com o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo do qual este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> é construído.</param>
		/// <param name="createOptions">As opções usadas para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="cacheOption">As opção de cache usada para criar este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.IO.Stream" /> com o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</returns>
		// Token: 0x060043C4 RID: 17348 RVA: 0x00107F4C File Offset: 0x0010734C
		public static BitmapFrame Create(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
		{
			if (bitmapStream == null)
			{
				throw new ArgumentNullException("bitmapStream");
			}
			return BitmapFrame.CreateFromUriOrStream(null, null, bitmapStream, createOptions, cacheOption, null);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> usado para construir este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</returns>
		// Token: 0x060043C5 RID: 17349 RVA: 0x00107F74 File Offset: 0x00107374
		public static BitmapFrame Create(BitmapSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			BitmapMetadata bitmapMetadata = null;
			try
			{
				bitmapMetadata = (source.Metadata as BitmapMetadata);
			}
			catch (NotSupportedException)
			{
			}
			if (bitmapMetadata != null)
			{
				bitmapMetadata = bitmapMetadata.Clone();
			}
			return new BitmapFrameEncode(source, null, bitmapMetadata, null);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com a miniatura especificada.</summary>
		/// <param name="source">A origem da qual o <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> é construído.</param>
		/// <param name="thumbnail">Uma imagem em miniatura do <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> resultante.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com a miniatura especificada.</returns>
		// Token: 0x060043C6 RID: 17350 RVA: 0x00107FD4 File Offset: 0x001073D4
		public static BitmapFrame Create(BitmapSource source, BitmapSource thumbnail)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			BitmapMetadata bitmapMetadata = null;
			try
			{
				bitmapMetadata = (source.Metadata as BitmapMetadata);
			}
			catch (NotSupportedException)
			{
			}
			if (bitmapMetadata != null)
			{
				bitmapMetadata = bitmapMetadata.Clone();
			}
			return BitmapFrame.Create(source, thumbnail, bitmapMetadata, null);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com a miniatura especificada, <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> e <see cref="T:System.Windows.Media.ColorContext" />.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> usado para construir este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="thumbnail">Uma imagem em miniatura do <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> resultante.</param>
		/// <param name="metadata">Os metadados a serem associados a esse <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <param name="colorContexts">Os objetos de <see cref="T:System.Windows.Media.ColorContext" /> que estão associados a esse <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> de um determinado <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com a miniatura especificada, <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> e <see cref="T:System.Windows.Media.ColorContext" />.</returns>
		// Token: 0x060043C7 RID: 17351 RVA: 0x00108034 File Offset: 0x00107434
		public static BitmapFrame Create(BitmapSource source, BitmapSource thumbnail, BitmapMetadata metadata, ReadOnlyCollection<ColorContext> colorContexts)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new BitmapFrameEncode(source, thumbnail, metadata, colorContexts);
		}

		/// <summary>Quando substituído em uma classe derivada, obtém ou define um valor que representa o <see cref="T:System.Uri" /> base do contexto atual.</summary>
		/// <returns>O <see cref="T:System.Uri" /> do contexto atual.</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060043C8 RID: 17352
		// (set) Token: 0x060043C9 RID: 17353
		public abstract Uri BaseUri { get; set; }

		/// <summary>Quando substituído em uma classe derivada, obtém a imagem de miniatura associada a esse <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa uma miniatura do atual <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060043CA RID: 17354
		public abstract BitmapSource Thumbnail { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém o decodificador associado a essa instância de <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> associado a este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060043CB RID: 17355
		public abstract BitmapDecoder Decoder { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém uma coleção de objetos de <see cref="T:System.Windows.Media.ColorContext" /> que são associados a esse <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</summary>
		/// <returns>Uma coleção somente leitura de <see cref="T:System.Windows.Media.ColorContext" /> objetos que estão associados com este <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</returns>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060043CC RID: 17356
		public abstract ReadOnlyCollection<ColorContext> ColorContexts { get; }

		/// <summary>Quando substituído em uma classe derivada, cria uma instância de <see cref="T:System.Windows.Media.Imaging.InPlaceBitmapMetadataWriter" />, que pode ser usada para associar metadados a um <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.InPlaceBitmapMetadataWriter" />.</returns>
		// Token: 0x060043CD RID: 17357
		public abstract InPlaceBitmapMetadataWriter CreateInPlaceBitmapMetadataWriter();

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x00108058 File Offset: 0x00107458
		// (set) Token: 0x060043CF RID: 17359 RVA: 0x00108068 File Offset: 0x00107468
		internal virtual BitmapMetadata InternalMetadata
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040018BD RID: 6333
		internal BitmapSource _thumbnail;

		// Token: 0x040018BE RID: 6334
		[SecurityCritical]
		internal BitmapMetadata _metadata;

		// Token: 0x040018BF RID: 6335
		internal ReadOnlyCollection<ColorContext> _readOnlycolorContexts;
	}
}
