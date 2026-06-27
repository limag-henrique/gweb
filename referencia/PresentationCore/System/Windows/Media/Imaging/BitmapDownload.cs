using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Win32.SafeHandles;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005D4 RID: 1492
	internal static class BitmapDownload
	{
		// Token: 0x06004391 RID: 17297 RVA: 0x00106860 File Offset: 0x00105C60
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static BitmapDownload()
		{
			BitmapDownload._waitEvent = new AutoResetEvent(false);
			BitmapDownload._workQueue = Queue.Synchronized(new Queue());
			BitmapDownload._uriTable = Hashtable.Synchronized(new Hashtable());
			BitmapDownload._readCallback = new AsyncCallback(BitmapDownload.ReadCallback);
			BitmapDownload._responseCallback = new AsyncCallback(BitmapDownload.ResponseCallback);
			BitmapDownload._thread = new Thread(new ThreadStart(BitmapDownload.DownloadThreadProc));
			BitmapDownload._syncLock = new object();
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001068E4 File Offset: 0x00105CE4
		[SecurityCritical]
		internal static void BeginDownload(BitmapDecoder decoder, Uri uri, RequestCachePolicy uriCachePolicy, Stream stream)
		{
			object syncLock = BitmapDownload._syncLock;
			lock (syncLock)
			{
				if (!BitmapDownload._thread.IsAlive)
				{
					BitmapDownload._thread.IsBackground = true;
					BitmapDownload._thread.Start();
				}
			}
			QueueEntry queueEntry;
			if (uri != null)
			{
				object syncLock2 = BitmapDownload._syncLock;
				lock (syncLock2)
				{
					if (BitmapDownload._uriTable[uri] != null)
					{
						queueEntry = (QueueEntry)BitmapDownload._uriTable[uri];
						queueEntry.decoders.Add(new WeakReference(decoder));
						return;
					}
				}
			}
			queueEntry = new QueueEntry();
			queueEntry.decoders = new List<WeakReference>();
			object syncLock3 = BitmapDownload._syncLock;
			lock (syncLock3)
			{
				queueEntry.decoders.Add(new WeakReference(decoder));
			}
			queueEntry.inputUri = uri;
			queueEntry.inputStream = stream;
			string localPath = WinInet.InternetCacheFolder.LocalPath;
			bool flag4 = false;
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			try
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				UnsafeNativeMethods.GetTempFileName(localPath, "WPF", 0U, stringBuilder);
				try
				{
					string text = stringBuilder.ToString();
					SafeFileHandle safeFileHandle = UnsafeNativeMethods.CreateFile(text, 3221225472U, 0U, null, 2, 67109120, IntPtr.Zero);
					if (safeFileHandle.IsInvalid)
					{
						throw new Win32Exception();
					}
					queueEntry.outputStream = new FileStream(safeFileHandle, FileAccess.ReadWrite);
					queueEntry.streamPath = text;
					flag4 = true;
				}
				catch (Exception ex)
				{
					if (CriticalExceptions.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (!flag4)
			{
				throw new IOException(SR.Get("Image_CannotCreateTempFile"));
			}
			queueEntry.readBuffer = new byte[1024];
			queueEntry.contentLength = -1L;
			queueEntry.contentType = string.Empty;
			queueEntry.lastPercent = 0;
			if (uri != null)
			{
				object syncLock4 = BitmapDownload._syncLock;
				lock (syncLock4)
				{
					BitmapDownload._uriTable[uri] = queueEntry;
				}
			}
			if (stream == null)
			{
				bool flag6 = false;
				if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
				{
					SecurityHelper.BlockCrossDomainForHttpsApps(uri);
					if (SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.SafeImage))
					{
						flag6 = true;
					}
				}
				if (flag6)
				{
					new WebPermission(NetworkAccess.Connect, BindUriHelper.UriToString(uri)).Assert();
				}
				try
				{
					queueEntry.webRequest = WpfWebRequestHelper.CreateRequest(uri);
					if (uriCachePolicy != null)
					{
						queueEntry.webRequest.CachePolicy = uriCachePolicy;
					}
				}
				finally
				{
					if (flag6)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				queueEntry.webRequest.BeginGetResponse(BitmapDownload._responseCallback, queueEntry);
				return;
			}
			BitmapDownload._workQueue.Enqueue(queueEntry);
			BitmapDownload._waitEvent.Set();
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00106C3C File Offset: 0x0010603C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void DownloadThreadProc()
		{
			Queue workQueue = BitmapDownload._workQueue;
			for (;;)
			{
				BitmapDownload._waitEvent.WaitOne();
				while (workQueue.Count != 0)
				{
					QueueEntry queueEntry = (QueueEntry)workQueue.Dequeue();
					try
					{
						queueEntry.inputStream.BeginRead(queueEntry.readBuffer, 0, 1024, BitmapDownload._readCallback, queueEntry);
					}
					catch (Exception e)
					{
						BitmapDownload.MarshalException(queueEntry, e);
					}
					finally
					{
						queueEntry = null;
					}
				}
			}
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00106CD4 File Offset: 0x001060D4
		[SecurityCritical]
		private static void ResponseCallback(IAsyncResult result)
		{
			QueueEntry queueEntry = (QueueEntry)result.AsyncState;
			try
			{
				WebResponse webResponse = WpfWebRequestHelper.EndGetResponse(queueEntry.webRequest, result);
				queueEntry.inputStream = webResponse.GetResponseStream();
				queueEntry.contentLength = webResponse.ContentLength;
				queueEntry.contentType = webResponse.ContentType;
				queueEntry.webRequest = null;
				BitmapDownload._workQueue.Enqueue(queueEntry);
				BitmapDownload._waitEvent.Set();
			}
			catch (Exception e)
			{
				BitmapDownload.MarshalException(queueEntry, e);
			}
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00106D64 File Offset: 0x00106164
		[SecurityCritical]
		private static void ReadCallback(IAsyncResult result)
		{
			QueueEntry queueEntry = (QueueEntry)result.AsyncState;
			int num = 0;
			try
			{
				num = queueEntry.inputStream.EndRead(result);
			}
			catch (Exception e)
			{
				BitmapDownload.MarshalException(queueEntry, e);
			}
			if (num == 0)
			{
				queueEntry.inputStream.Close();
				queueEntry.inputStream = null;
				queueEntry.outputStream.Flush();
				queueEntry.outputStream.Seek(0L, SeekOrigin.Begin);
				object syncLock = BitmapDownload._syncLock;
				lock (syncLock)
				{
					foreach (WeakReference weakReference in queueEntry.decoders)
					{
						LateBoundBitmapDecoder lateBoundBitmapDecoder = weakReference.Target as LateBoundBitmapDecoder;
						if (lateBoundBitmapDecoder != null)
						{
							BitmapDownload.MarshalEvents(lateBoundBitmapDecoder, new DispatcherOperationCallback(lateBoundBitmapDecoder.ProgressCallback), 100);
							BitmapDownload.MarshalEvents(lateBoundBitmapDecoder, new DispatcherOperationCallback(lateBoundBitmapDecoder.DownloadCallback), queueEntry.outputStream);
						}
					}
				}
				if (!(queueEntry.inputUri != null))
				{
					return;
				}
				object syncLock2 = BitmapDownload._syncLock;
				lock (syncLock2)
				{
					BitmapDownload._uriTable[queueEntry.inputUri] = null;
					return;
				}
			}
			queueEntry.outputStream.Write(queueEntry.readBuffer, 0, num);
			if (queueEntry.contentLength > 0L)
			{
				int num2 = (int)Math.Floor(100.0 * (double)queueEntry.outputStream.Length / (double)queueEntry.contentLength);
				if (num2 != queueEntry.lastPercent)
				{
					queueEntry.lastPercent = num2;
					object syncLock3 = BitmapDownload._syncLock;
					lock (syncLock3)
					{
						foreach (WeakReference weakReference2 in queueEntry.decoders)
						{
							LateBoundBitmapDecoder lateBoundBitmapDecoder2 = weakReference2.Target as LateBoundBitmapDecoder;
							if (lateBoundBitmapDecoder2 != null)
							{
								BitmapDownload.MarshalEvents(lateBoundBitmapDecoder2, new DispatcherOperationCallback(lateBoundBitmapDecoder2.ProgressCallback), num2);
							}
						}
					}
				}
			}
			BitmapDownload._workQueue.Enqueue(queueEntry);
			BitmapDownload._waitEvent.Set();
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00107028 File Offset: 0x00106428
		private static void MarshalEvents(LateBoundBitmapDecoder decoder, DispatcherOperationCallback doc, object arg)
		{
			Dispatcher dispatcher = decoder.Dispatcher;
			if (dispatcher != null)
			{
				dispatcher.BeginInvoke(DispatcherPriority.Normal, doc, arg);
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0010704C File Offset: 0x0010644C
		private static void MarshalException(QueueEntry entry, Exception e)
		{
			object syncLock = BitmapDownload._syncLock;
			lock (syncLock)
			{
				foreach (WeakReference weakReference in entry.decoders)
				{
					LateBoundBitmapDecoder lateBoundBitmapDecoder = weakReference.Target as LateBoundBitmapDecoder;
					if (lateBoundBitmapDecoder != null)
					{
						BitmapDownload.MarshalEvents(lateBoundBitmapDecoder, new DispatcherOperationCallback(lateBoundBitmapDecoder.ExceptionCallback), e);
					}
				}
				if (entry.inputUri != null)
				{
					object syncLock2 = BitmapDownload._syncLock;
					lock (syncLock2)
					{
						BitmapDownload._uriTable[entry.inputUri] = null;
					}
				}
			}
		}

		// Token: 0x04001899 RID: 6297
		internal static AutoResetEvent _waitEvent = new AutoResetEvent(false);

		// Token: 0x0400189A RID: 6298
		[SecurityCritical]
		internal static Queue _workQueue;

		// Token: 0x0400189B RID: 6299
		internal static Hashtable _uriTable;

		// Token: 0x0400189C RID: 6300
		internal static AsyncCallback _readCallback;

		// Token: 0x0400189D RID: 6301
		internal static AsyncCallback _responseCallback;

		// Token: 0x0400189E RID: 6302
		private static Thread _thread;

		// Token: 0x0400189F RID: 6303
		private static object _syncLock;

		// Token: 0x040018A0 RID: 6304
		private const int READ_SIZE = 1024;
	}
}
