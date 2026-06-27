using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Representa a imagem usada para o ponteiro do mouse.</summary>
	// Token: 0x0200022E RID: 558
	[TypeConverter(typeof(CursorConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class Cursor : IDisposable
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x0003AF4C File Offset: 0x0003A34C
		internal Cursor(CursorType cursorType)
		{
			if (this.IsValidCursorType(cursorType))
			{
				this.LoadCursorHelper(cursorType);
				return;
			}
			throw new ArgumentException(SR.Get("InvalidCursorType", new object[]
			{
				cursorType
			}));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.Cursor" /> do arquivo .ani ou .cur especificado.</summary>
		/// <param name="cursorFile">O arquivo que contém o cursor.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cursorFile" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cursorFile" /> não é um nome de arquivo .ani ou .cur.</exception>
		// Token: 0x06000F73 RID: 3955 RVA: 0x0003AF9C File Offset: 0x0003A39C
		public Cursor(string cursorFile) : this(cursorFile, false)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.Cursor" />.</summary>
		/// <param name="cursorFile">O <see cref="T:System.IO.Stream" /> que contém o cursor.</param>
		/// <param name="scaleWithDpi">
		///   <see langword="true" /> se for ajustar a escala com o dpi; caso contrário, <see langword="false" />.</param>
		// Token: 0x06000F74 RID: 3956 RVA: 0x0003AFB4 File Offset: 0x0003A3B4
		public Cursor(string cursorFile, bool scaleWithDpi)
		{
			this._scaleWithDpi = scaleWithDpi;
			if (cursorFile == null)
			{
				throw new ArgumentNullException("cursorFile");
			}
			if (cursorFile != string.Empty && (cursorFile.EndsWith(".cur", StringComparison.OrdinalIgnoreCase) || cursorFile.EndsWith(".ani", StringComparison.OrdinalIgnoreCase)))
			{
				this.LoadFromFile(cursorFile);
				this._fileName = cursorFile;
				return;
			}
			throw new ArgumentException(SR.Get("Cursor_UnsupportedFormat", new object[]
			{
				cursorFile
			}));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.Cursor" /> do <see cref="T:System.IO.Stream" /> especificado.</summary>
		/// <param name="cursorStream">O <see cref="T:System.IO.Stream" /> que contém o cursor.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cursorStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">Este construtor não pôde criar um arquivo temporário.</exception>
		// Token: 0x06000F75 RID: 3957 RVA: 0x0003B038 File Offset: 0x0003A438
		public Cursor(Stream cursorStream) : this(cursorStream, false)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.Cursor" />.</summary>
		/// <param name="cursorStream">O <see cref="T:System.IO.Stream" /> que contém o cursor.</param>
		/// <param name="scaleWithDpi">
		///   <see langword="true" /> se for ajustar a escala com o dpi; caso contrário, <see langword="false" />.</param>
		// Token: 0x06000F76 RID: 3958 RVA: 0x0003B050 File Offset: 0x0003A450
		public Cursor(Stream cursorStream, bool scaleWithDpi)
		{
			this._scaleWithDpi = scaleWithDpi;
			if (cursorStream == null)
			{
				throw new ArgumentNullException("cursorStream");
			}
			this.LoadFromStream(cursorStream);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003B08C File Offset: 0x0003A48C
		[SecurityCritical]
		[FriendAccessAllowed]
		[SecurityTreatAsSafe]
		internal Cursor(SafeHandle cursorHandle)
		{
			if (!cursorHandle.IsInvalid)
			{
				this._cursorHandle = cursorHandle;
			}
		}

		/// <summary>Finaliza o objeto para liberar recursos e executar outras operações de limpeza.</summary>
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003B0BC File Offset: 0x0003A4BC
		~Cursor()
		{
			this.Dispose(false);
		}

		/// <summary>Libera os recursos usados pela classe <see cref="T:System.Windows.Input.Cursor" />.</summary>
		// Token: 0x06000F79 RID: 3961 RVA: 0x0003B0F8 File Offset: 0x0003A4F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003B114 File Offset: 0x0003A514
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void Dispose(bool disposing)
		{
			if (this._cursorHandle != null)
			{
				this._cursorHandle.Dispose();
				this._cursorHandle = null;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003B13C File Offset: 0x0003A53C
		internal CursorType CursorType
		{
			get
			{
				return this._cursorType;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003B150 File Offset: 0x0003A550
		internal SafeHandle Handle
		{
			[SecurityCritical]
			get
			{
				return this._cursorHandle ?? NativeMethods.CursorHandle.GetInvalidCursor();
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003B16C File Offset: 0x0003A56C
		internal string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003B180 File Offset: 0x0003A580
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void LoadFromFile(string fileName)
		{
			SecurityHelper.DemandFileIOReadPermission(fileName);
			this._cursorHandle = UnsafeNativeMethods.LoadImageCursor(IntPtr.Zero, fileName, 2, 0, 0, 16 | (this._scaleWithDpi ? 64 : 0));
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this._cursorHandle != null && !this._cursorHandle.IsInvalid)
			{
				return;
			}
			if (lastWin32Error == 0)
			{
				throw new ArgumentException(SR.Get("Cursor_LoadImageFailure", new object[]
				{
					fileName
				}));
			}
			if (lastWin32Error == 2 || lastWin32Error == 3)
			{
				throw new Win32Exception(lastWin32Error, SR.Get("Cursor_LoadImageFailure", new object[]
				{
					fileName
				}));
			}
			throw new Win32Exception(lastWin32Error);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003B21C File Offset: 0x0003A61C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void LegacyLoadFromStream(Stream cursorStream)
		{
			string tempFileName = Path.GetTempFileName();
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(cursorStream))
				{
					using (FileStream fileStream = new FileStream(tempFileName, FileMode.Open, FileAccess.Write, FileShare.None))
					{
						byte[] array = binaryReader.ReadBytes(4096);
						int i;
						for (i = array.Length; i >= 4096; i = binaryReader.Read(array, 0, 4096))
						{
							fileStream.Write(array, 0, 4096);
						}
						fileStream.Write(array, 0, i);
					}
				}
				this._cursorHandle = UnsafeNativeMethods.LoadImageCursor(IntPtr.Zero, tempFileName, 2, 0, 0, 16 | (this._scaleWithDpi ? 64 : 0));
				if (this._cursorHandle == null || this._cursorHandle.IsInvalid)
				{
					throw new ArgumentException(SR.Get("Cursor_InvalidStream"));
				}
			}
			finally
			{
				try
				{
					File.Delete(tempFileName);
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003B358 File Offset: 0x0003A758
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void LoadFromStream(Stream cursorStream)
		{
			if (CoreAppContextSwitches.AllowExternalProcessToBlockAccessToTemporaryFiles)
			{
				this.LegacyLoadFromStream(cursorStream);
				return;
			}
			string text = null;
			try
			{
				using (FileStream fileStream = FileHelper.CreateAndOpenTemporaryFile(out text, FileAccess.Write, FileOptions.None, null, "WPF"))
				{
					cursorStream.CopyTo(fileStream);
				}
				this._cursorHandle = UnsafeNativeMethods.LoadImageCursor(IntPtr.Zero, text, 2, 0, 0, 16 | (this._scaleWithDpi ? 64 : 0));
				if (this._cursorHandle == null || this._cursorHandle.IsInvalid)
				{
					throw new ArgumentException(SR.Get("Cursor_InvalidStream"));
				}
			}
			finally
			{
				FileHelper.DeleteTemporaryFile(text);
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003B420 File Offset: 0x0003A820
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void LoadCursorHelper(CursorType cursorType)
		{
			if (cursorType != CursorType.None)
			{
				this._cursorHandle = SafeNativeMethods.LoadCursor(new HandleRef(this, IntPtr.Zero), (IntPtr)Cursor.CursorTypes[(int)cursorType]);
			}
			this._cursorType = cursorType;
		}

		/// <summary>Retorna a representação de cadeia de caracteres do <see cref="T:System.Windows.Input.Cursor" />.</summary>
		/// <returns>O nome do cursor.</returns>
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003B45C File Offset: 0x0003A85C
		public override string ToString()
		{
			if (this._fileName != string.Empty)
			{
				return this._fileName;
			}
			return Enum.GetName(typeof(CursorType), this._cursorType);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003B49C File Offset: 0x0003A89C
		private bool IsValidCursorType(CursorType cursorType)
		{
			return cursorType >= CursorType.None && cursorType <= CursorType.ArrowCD;
		}

		// Token: 0x0400086C RID: 2156
		private const int BUFFERSIZE = 4096;

		// Token: 0x0400086D RID: 2157
		private string _fileName = string.Empty;

		// Token: 0x0400086E RID: 2158
		private CursorType _cursorType;

		// Token: 0x0400086F RID: 2159
		private bool _scaleWithDpi;

		// Token: 0x04000870 RID: 2160
		[SecurityCritical]
		private SafeHandle _cursorHandle;

		// Token: 0x04000871 RID: 2161
		private static readonly int[] CursorTypes = new int[]
		{
			0,
			32648,
			32512,
			32650,
			32515,
			32651,
			32513,
			32646,
			32643,
			32645,
			32642,
			32644,
			32516,
			32514,
			32649,
			32631,
			32652,
			32653,
			32654,
			32655,
			32656,
			32657,
			32658,
			32659,
			32660,
			32661,
			32662,
			32663
		};
	}
}
