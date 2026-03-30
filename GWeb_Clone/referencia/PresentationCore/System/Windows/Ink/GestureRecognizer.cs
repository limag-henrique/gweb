using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using MS.Internal;
using MS.Internal.Ink.GestureRecognition;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Reconhece gestos de tinta.</summary>
	// Token: 0x02000351 RID: 849
	public sealed class GestureRecognizer : DependencyObject, IDisposable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.GestureRecognizer" />.</summary>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x00074A14 File Offset: 0x00073E14
		public GestureRecognizer() : this(new ApplicationGesture[1])
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.GestureRecognizer" />.</summary>
		/// <param name="enabledApplicationGestures">Uma matriz do tipo <see cref="T:System.Windows.Ink.ApplicationGesture" /> que especifica os gestos de aplicativo que serão reconhecidos pelo <see cref="T:System.Windows.Ink.GestureRecognizer" />.</param>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00074A30 File Offset: 0x00073E30
		public GestureRecognizer(IEnumerable<ApplicationGesture> enabledApplicationGestures)
		{
			this._nativeRecognizer = NativeRecognizer.CreateInstance();
			if (this._nativeRecognizer == null)
			{
				NativeRecognizer.GetApplicationGestureArrayAndVerify(enabledApplicationGestures);
				return;
			}
			this.SetEnabledGestures(enabledApplicationGestures);
		}

		/// <summary>Define os gestos do aplicativo que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> reconhece.</summary>
		/// <param name="applicationGestures">Uma matriz do tipo <see cref="T:System.Windows.Ink.ApplicationGesture" /> que especifica os gestos de aplicativo que você deseja que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> reconheça.</param>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x00074A68 File Offset: 0x00073E68
		public void SetEnabledGestures(IEnumerable<ApplicationGesture> applicationGestures)
		{
			base.VerifyAccess();
			this.VerifyDisposed();
			this.VerifyRecognizerAvailable();
			ApplicationGesture[] enabledGestures = this._nativeRecognizer.SetEnabledGestures(applicationGestures);
			this._enabledGestures = enabledGestures;
		}

		/// <summary>Obtém os gestos do que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> reconhece.</summary>
		/// <returns>Uma matriz do tipo <see cref="T:System.Windows.Ink.ApplicationGesture" /> que contém gestos que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> está configurado para reconhecer.</returns>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00074A9C File Offset: 0x00073E9C
		public ReadOnlyCollection<ApplicationGesture> GetEnabledGestures()
		{
			base.VerifyAccess();
			this.VerifyDisposed();
			this.VerifyRecognizerAvailable();
			if (this._enabledGestures == null)
			{
				this._enabledGestures = new ApplicationGesture[0];
			}
			return new ReadOnlyCollection<ApplicationGesture>(this._enabledGestures);
		}

		/// <summary>Procura gestos no <see cref="T:System.Windows.Ink.StrokeCollection" /> especificado.</summary>
		/// <param name="strokes">O <see cref="T:System.Windows.Ink.StrokeCollection" /> no qual pesquisar por gestos.</param>
		/// <returns>Uma matriz do tipo <see cref="T:System.Windows.Ink.GestureRecognitionResult" /> que contém gestos de aplicativo que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> reconheceu.</returns>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x00074ADC File Offset: 0x00073EDC
		[SecurityCritical]
		public ReadOnlyCollection<GestureRecognitionResult> Recognize(StrokeCollection strokes)
		{
			SecurityHelper.DemandUnmanagedCode();
			return this.RecognizeImpl(strokes);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00074AF8 File Offset: 0x00073EF8
		[SecurityCritical]
		internal ReadOnlyCollection<GestureRecognitionResult> CriticalRecognize(StrokeCollection strokes)
		{
			return this.RecognizeImpl(strokes);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00074B0C File Offset: 0x00073F0C
		[SecurityCritical]
		private ReadOnlyCollection<GestureRecognitionResult> RecognizeImpl(StrokeCollection strokes)
		{
			if (strokes == null)
			{
				throw new ArgumentNullException("strokes");
			}
			if (strokes.Count > 2)
			{
				throw new ArgumentException(SR.Get("StrokeCollectionCountTooBig"), "strokes");
			}
			base.VerifyAccess();
			this.VerifyDisposed();
			this.VerifyRecognizerAvailable();
			return new ReadOnlyCollection<GestureRecognitionResult>(this._nativeRecognizer.Recognize(strokes));
		}

		/// <summary>Obtém um booliano que indica se o reconhecedor de gestos está disponível no sistema do usuário.</summary>
		/// <returns>
		///   <see langword="true" /> Se o componente de reconhecimento está disponível. Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00074B68 File Offset: 0x00073F68
		public bool IsRecognizerAvailable
		{
			get
			{
				base.VerifyAccess();
				this.VerifyDisposed();
				return this._nativeRecognizer != null;
			}
		}

		/// <summary>Libera todos os recursos usados pelo <see cref="T:System.Windows.Ink.GestureRecognizer" />.</summary>
		// Token: 0x06001CB8 RID: 7352 RVA: 0x00074B8C File Offset: 0x00073F8C
		public void Dispose()
		{
			base.VerifyAccess();
			if (this._disposed)
			{
				return;
			}
			if (this._nativeRecognizer != null)
			{
				this._nativeRecognizer.Dispose();
				this._nativeRecognizer = null;
			}
			this._disposed = true;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00074BCC File Offset: 0x00073FCC
		private void VerifyRecognizerAvailable()
		{
			if (this._nativeRecognizer == null)
			{
				throw new InvalidOperationException(SR.Get("GestureRecognizerNotAvailable"));
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00074BF4 File Offset: 0x00073FF4
		private void VerifyDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GestureRecognizer");
			}
		}

		// Token: 0x04000F96 RID: 3990
		private ApplicationGesture[] _enabledGestures;

		// Token: 0x04000F97 RID: 3991
		private NativeRecognizer _nativeRecognizer;

		// Token: 0x04000F98 RID: 3992
		private bool _disposed;
	}
}
