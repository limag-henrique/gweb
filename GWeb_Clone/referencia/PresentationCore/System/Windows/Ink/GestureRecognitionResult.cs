using System;

namespace System.Windows.Ink
{
	/// <summary>Contém informações sobre um gesto de tinta.</summary>
	// Token: 0x02000350 RID: 848
	public class GestureRecognitionResult
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x000749C8 File Offset: 0x00073DC8
		internal GestureRecognitionResult(RecognitionConfidence confidence, ApplicationGesture gesture)
		{
			this._confidence = confidence;
			this._gesture = gesture;
		}

		/// <summary>Obtém o nível de confiança que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> tem no reconhecimento do gesto.</summary>
		/// <returns>Um do <see cref="T:System.Windows.Ink.RecognitionConfidence" /> níveis.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000749EC File Offset: 0x00073DEC
		public RecognitionConfidence RecognitionConfidence
		{
			get
			{
				return this._confidence;
			}
		}

		/// <summary>Obtém o gesto de tinta reconhecido.</summary>
		/// <returns>O gesto de tinta reconhecido.</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00074A00 File Offset: 0x00073E00
		public ApplicationGesture ApplicationGesture
		{
			get
			{
				return this._gesture;
			}
		}

		// Token: 0x04000F94 RID: 3988
		private RecognitionConfidence _confidence;

		// Token: 0x04000F95 RID: 3989
		private ApplicationGesture _gesture;
	}
}
