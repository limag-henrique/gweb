using System;
using System.Net;
using System.Net.Cache;
using System.Security;
using System.Windows.Navigation;
using MS.Internal;
using MS.Internal.IO.Packaging;
using MS.Internal.PresentationCore;

namespace System.IO.Packaging
{
	/// <summary>Faz uma solicitação para um <see cref="T:System.IO.Packaging.PackagePart" /> inteiro ou um <see cref="T:System.IO.Packaging.PackagePart" /> em um pacote, identificado por um URI de pacote.</summary>
	// Token: 0x02000185 RID: 389
	public sealed class PackWebRequest : WebRequest
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00014A74 File Offset: 0x00013E74
		internal PackWebRequest(Uri uri, Uri packageUri, Uri partUri) : this(uri, packageUri, partUri, null, false, false)
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014A90 File Offset: 0x00013E90
		internal PackWebRequest(Uri uri, Uri packageUri, Uri partUri, Package cacheEntry, bool respectCachePolicy, bool cachedPackageIsThreadSafe)
		{
			this._uri = uri;
			this._innerUri = packageUri;
			this._partName = partUri;
			this._cacheEntry = cacheEntry;
			this._respectCachePolicy = respectCachePolicy;
			this._cachedPackageIsThreadSafe = cachedPackageIsThreadSafe;
			this._cachePolicy = PackWebRequest._defaultCachePolicy;
		}

		/// <summary>Não usar – <see cref="M:System.IO.Packaging.PackWebRequest.GetRequestStream" /> não é compatível com <see cref="T:System.IO.Packaging.PackWebRequest" />.</summary>
		/// <returns>Se <see cref="M:System.IO.Packaging.PackWebRequest.GetRequestStream" /> for chamado, um <see cref="T:System.NotSupportedException" /> será gerado.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorre em qualquer chamada para <see cref="M:System.IO.Packaging.PackWebRequest.GetRequestStream" />.  O protocolo URI do pacote não é compatível com gravação.</exception>
		// Token: 0x0600038E RID: 910 RVA: 0x00014ADC File Offset: 0x00013EDC
		public override Stream GetRequestStream()
		{
			throw new NotSupportedException();
		}

		/// <summary>Retorna o fluxo de resposta para a solicitação.</summary>
		/// <returns>O fluxo de resposta para a solicitação.</returns>
		// Token: 0x0600038F RID: 911 RVA: 0x00014AF0 File Offset: 0x00013EF0
		[SecurityCritical]
		public override WebResponse GetResponse()
		{
			bool flag = this.IsCachedPackage;
			if (!flag || (flag && this._respectCachePolicy))
			{
				RequestCacheLevel level = this._cachePolicy.Level;
				if (level == RequestCacheLevel.Default)
				{
					level = PackWebRequest._defaultCachePolicy.Level;
				}
				switch (level)
				{
				case RequestCacheLevel.BypassCache:
					flag = false;
					break;
				case RequestCacheLevel.CacheOnly:
					if (!flag)
					{
						throw new WebException(SR.Get("ResourceNotFoundUnderCacheOnlyPolicy"));
					}
					break;
				case RequestCacheLevel.CacheIfAvailable:
					break;
				default:
					throw new WebException(SR.Get("PackWebRequestCachePolicyIllegal"));
				}
			}
			if (flag)
			{
				return new PackWebResponse(this._uri, this._innerUri, this._partName, this._cacheEntry, this._cachedPackageIsThreadSafe);
			}
			WebRequest request = this.GetRequest(false);
			if (this._webRequest == null || this._webRequest is PseudoWebRequest)
			{
				throw new InvalidOperationException(SR.Get("SchemaInvalidForTransport"));
			}
			return new PackWebResponse(this._uri, this._innerUri, this._partName, request);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Net.Cache.RequestCachePolicy" />.</summary>
		/// <returns>O <see cref="T:System.Net.Cache.RequestCachePolicy" /> para usar com a solicitação do pack URI da web.</returns>
		/// <exception cref="T:System.Net.WebException">O <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificado a definir não é válido.</exception>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00014BD8 File Offset: 0x00013FD8
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00014BEC File Offset: 0x00013FEC
		public override RequestCachePolicy CachePolicy
		{
			get
			{
				return this._cachePolicy;
			}
			set
			{
				if (value == null)
				{
					this._cachePolicy = PackWebRequest._defaultCachePolicy;
					return;
				}
				switch (value.Level)
				{
				case RequestCacheLevel.BypassCache:
				case RequestCacheLevel.CacheOnly:
				case RequestCacheLevel.CacheIfAvailable:
					this._cachePolicy = value;
					return;
				default:
					throw new WebException(SR.Get("PackWebRequestCachePolicyIllegal"));
				}
			}
		}

		/// <summary>Obtém ou define o nome do grupo de conexão.</summary>
		/// <returns>O nome do grupo de conexão.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00014C3C File Offset: 0x0001403C
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00014C54 File Offset: 0x00014054
		public override string ConnectionGroupName
		{
			get
			{
				return this.GetRequest().ConnectionGroupName;
			}
			set
			{
				this.GetRequest().ConnectionGroupName = value;
			}
		}

		/// <summary>Obtém ou define o cabeçalho HTTP Content-length.</summary>
		/// <returns>O comprimento de conteúdo, em bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">Não há suporte para definir, <see cref="T:System.IO.Packaging.PackWebRequest" /> é somente leitura.</exception>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00014C70 File Offset: 0x00014070
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00014C88 File Offset: 0x00014088
		public override long ContentLength
		{
			get
			{
				return this.GetRequest().ContentLength;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Obtém ou define o cabeçalho HTTP Content-type.</summary>
		/// <returns>O conteúdo do cabeçalho.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00014C9C File Offset: 0x0001409C
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00014CC8 File Offset: 0x000140C8
		public override string ContentType
		{
			get
			{
				string contentType = this.GetRequest().ContentType;
				if (contentType == null)
				{
					return contentType;
				}
				return new ContentType(contentType).ToString();
			}
			set
			{
				this.GetRequest().ContentType = value;
			}
		}

		/// <summary>Obtém ou define as credenciais de autenticação.</summary>
		/// <returns>As credenciais de autenticação para usar com a solicitação.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00014CE4 File Offset: 0x000140E4
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00014CFC File Offset: 0x000140FC
		public override ICredentials Credentials
		{
			get
			{
				return this.GetRequest().Credentials;
			}
			set
			{
				this.GetRequest().Credentials = value;
			}
		}

		/// <summary>Obtém ou define a coleção de pares nome-valor do cabeçalho associados à solicitação.</summary>
		/// <returns>Um objeto de coleção de cabeçalho.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00014D18 File Offset: 0x00014118
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00014D30 File Offset: 0x00014130
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.GetRequest().Headers;
			}
			set
			{
				this.GetRequest().Headers = value;
			}
		}

		/// <summary>Obtém ou define o método de protocolo a ser usado com a solicitação do URI de pacote.</summary>
		/// <returns>O nome do método de protocolo que executa esta solicitação.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00014D4C File Offset: 0x0001414C
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00014D64 File Offset: 0x00014164
		public override string Method
		{
			get
			{
				return this.GetRequest().Method;
			}
			set
			{
				this.GetRequest().Method = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se a solicitação deve ser pré-autenticada.</summary>
		/// <returns>
		///   <see langword="true" /> para enviar uma WWW-authenticate cabeçalho HTTP com a solicitação inicial; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00014D80 File Offset: 0x00014180
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00014D98 File Offset: 0x00014198
		public override bool PreAuthenticate
		{
			get
			{
				return this.GetRequest().PreAuthenticate;
			}
			set
			{
				this.GetRequest().PreAuthenticate = value;
			}
		}

		/// <summary>Obtém ou define o proxy de rede para acesso à Internet.</summary>
		/// <returns>O <see cref="T:System.Net.WebProxy" /> a ser usado para acesso à Internet.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00014DB4 File Offset: 0x000141B4
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00014DCC File Offset: 0x000141CC
		public override IWebProxy Proxy
		{
			[SecurityCritical]
			get
			{
				return this.GetRequest().Proxy;
			}
			[SecurityCritical]
			set
			{
				this.GetRequest().Proxy = value;
			}
		}

		/// <summary>Obtém o URI do recurso associado à solicitação.</summary>
		/// <returns>O URI (Uniform Resource Identifier) do recurso associado à solicitação.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00014DE8 File Offset: 0x000141E8
		public override Uri RequestUri
		{
			get
			{
				return this._uri;
			}
		}

		/// <summary>Obtém ou define o período de tempo antes que o tempo limite da solicitação seja atingido.</summary>
		/// <returns>O número de milissegundos de espera antes que a solicitação atinja o tempo limite.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00014DFC File Offset: 0x000141FC
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00014E14 File Offset: 0x00014214
		public override int Timeout
		{
			get
			{
				return this.GetRequest().Timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.GetRequest().Timeout = value;
			}
		}

		/// <summary>Obtém ou define as credenciais de autenticação padrão.</summary>
		/// <returns>As credenciais de autenticação padrão para usar com a solicitação URI de pacote.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00014E40 File Offset: 0x00014240
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00014E58 File Offset: 0x00014258
		public override bool UseDefaultCredentials
		{
			get
			{
				return this.GetRequest().UseDefaultCredentials;
			}
			set
			{
				this.GetRequest().UseDefaultCredentials = value;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Net.WebRequest" /> interno.</summary>
		/// <returns>Um <see cref="T:System.Net.WebRequest" /> criado do URI interno se a solicitação for resolvida para um do protocolo de transporte válido, como http ou ftp; ou um <see cref="T:System.Net.WebRequest" /> criado com um URI nulo se a solicitação for resolvida do cache <see cref="T:System.IO.Packaging.PackageStore" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O URI interno não resolve um protocolo de transporte válido, como http para ftp e a solicitação não ser resolvida do <see cref="T:System.IO.Packaging.PackageStore" />.</exception>
		// Token: 0x060003A7 RID: 935 RVA: 0x00014E74 File Offset: 0x00014274
		public WebRequest GetInnerRequest()
		{
			WebRequest request = this.GetRequest(false);
			if (request == null || request is PseudoWebRequest)
			{
				return null;
			}
			return request;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014E98 File Offset: 0x00014298
		private WebRequest GetRequest()
		{
			return this.GetRequest(true);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014EAC File Offset: 0x000142AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private WebRequest GetRequest(bool allowPseudoRequest)
		{
			if (this._webRequest == null)
			{
				if (!this.IsPreloadedPackage)
				{
					try
					{
						this._webRequest = WpfWebRequestHelper.CreateRequest(this._innerUri);
						FtpWebRequest ftpWebRequest = this._webRequest as FtpWebRequest;
						if (ftpWebRequest != null)
						{
							ftpWebRequest.UsePassive = false;
						}
					}
					catch (NotSupportedException)
					{
						if (!this.IsCachedPackage)
						{
							throw;
						}
					}
				}
				if (this._webRequest == null && allowPseudoRequest)
				{
					this._webRequest = new PseudoWebRequest(this._uri, this._innerUri, this._partName, this._cacheEntry);
				}
			}
			return this._webRequest;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00014F54 File Offset: 0x00014354
		private bool IsCachedPackage
		{
			get
			{
				return this._cacheEntry != null;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00014F6C File Offset: 0x0001436C
		private bool IsPreloadedPackage
		{
			get
			{
				return this._cacheEntry != null && !this._respectCachePolicy;
			}
		}

		// Token: 0x0400049E RID: 1182
		private Uri _uri;

		// Token: 0x0400049F RID: 1183
		private Uri _innerUri;

		// Token: 0x040004A0 RID: 1184
		private Uri _partName;

		// Token: 0x040004A1 RID: 1185
		[SecurityCritical]
		private WebRequest _webRequest;

		// Token: 0x040004A2 RID: 1186
		private Package _cacheEntry;

		// Token: 0x040004A3 RID: 1187
		private bool _respectCachePolicy;

		// Token: 0x040004A4 RID: 1188
		private bool _cachedPackageIsThreadSafe;

		// Token: 0x040004A5 RID: 1189
		private RequestCachePolicy _cachePolicy;

		// Token: 0x040004A6 RID: 1190
		private static RequestCachePolicy _defaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);

		// Token: 0x040004A7 RID: 1191
		private static Uri _siteOfOriginUri = PackUriHelper.GetPackageUri(BaseUriHelper.SiteOfOriginBaseUri);

		// Token: 0x040004A8 RID: 1192
		private static Uri _appBaseUri = PackUriHelper.GetPackageUri(BaseUriHelper.PackAppBaseUri);
	}
}
