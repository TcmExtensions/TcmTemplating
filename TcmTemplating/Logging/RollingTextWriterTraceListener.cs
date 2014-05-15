#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RollingTextWriterTraceListener
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TcmTemplating.Logging
{
	/// <summary>
	/// <see cref="RollingTextWriterTraceListener" /> implements a <see cref="T:System.Diagnostics.TraceListeners" /> which automatically rolls over on a daily basis
	/// </summary>
	public class RollingTextWriterTraceListener : TraceListener
	{
		// Fields
		private String mFileName;
		private String mCurrentFile;
		private DateTime mCurrentDate;
		private TextWriter mWriter;

		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		/// <summary>
		/// Generates the rollover log filename
		/// </summary>
		/// <param name="fileName">basename of the filename to be generated</param>
		/// <returns></returns>
		private String generateFilename(String fileName)
		{
			// Initialize the new log file for today
			mCurrentDate = System.DateTime.Today;

			String directory = Path.GetDirectoryName(fileName);
			String filePrefix = Path.GetFileNameWithoutExtension(fileName);
			String fileExtension = Path.GetExtension(fileName);

			try
			{
				// Try and create the log directory
				Directory.CreateDirectory(directory);
			}
			catch
			{
			}

			// Return a filename in the form of "c:\temp\out1_20071101.log"
			return Path.Combine(directory, String.Format("{0}_{1}{2}", filePrefix, mCurrentDate.ToString("yyyyMMdd"), fileExtension));
		}

		private bool EnsureWriter()
		{
			bool flag = true;

			// If the date has rolled over to the next day, release the write so a new 
			// writer will be initialized
			//TODO: Implement logfile size rollover
			//if ((currentDate.CompareTo(System.DateTime.Today) != 0) || (File.Exists(currentFile) && (new FileInfo(currentFile).Length > DynConfig.ItemAsInt("System/LogFileSize", LOG_SIZE))))
			if (mCurrentDate.CompareTo(System.DateTime.Today) != 0)
				this.Close();

			// Close the stream if the file is no longer accessible
			try
			{
				if (!String.IsNullOrEmpty(mCurrentFile))
					File.SetAttributes(mCurrentFile, FileAttributes.Archive);
			}
			catch
			{
				this.Close();
			}

			// Initialize a new writer if none exists or if the current log file has been deleted
			if (mWriter == null)
			{
				flag = false;

				if (mFileName == null)
					return flag;

				Encoding encodingWithFallback = GetEncodingWithFallback(new UTF8Encoding(false));
				mCurrentFile = generateFilename(mFileName);

				// Try creating the file
				// 1. Create file name as given
				// 2. Create filename with a GUID appended in front
				// 3. File could not be created
				for (int i = 0; i < 2; i++)
				{
					try
					{
						FileStream fsOutput = new FileStream(mCurrentFile, FileMode.Append, FileAccess.Write, FileShare.Delete | FileShare.ReadWrite);
						mWriter = new StreamWriter(fsOutput, encodingWithFallback, 0x1000);
						flag = true;
						break;
					}
					catch (IOException)
					{
						String directoryName = Path.GetDirectoryName(mCurrentFile);
						String fileName = Path.GetFileName(mCurrentFile);

						fileName = Guid.NewGuid().ToString() + fileName;
						mCurrentFile = Path.Combine(directoryName, fileName);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}

				if (!flag)
					mFileName = null;
			}

			return flag;
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.IO.TextWriter" />
		/// </summary>
		/// <value>
		/// <see cref="T:System.IO.TextWriter" />
		/// </value>
		public TextWriter Writer
		{
			get
			{
				this.EnsureWriter();
				return mWriter;
			}
			set
			{
				mWriter = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingTextWriterTraceListener" /> class.
		/// </summary>
		public RollingTextWriterTraceListener()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingTextWriterTraceListener"/> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		public RollingTextWriterTraceListener(String fileName)
		{
			mFileName = fileName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingTextWriterTraceListener"/> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="name">Log name</param>
		public RollingTextWriterTraceListener(String fileName, String name) : base(name)
		{
			mFileName = fileName;
		}

		/// <summary>
		/// When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.
		/// </summary>
		public override void Close()
		{
			if (mWriter != null)
				mWriter.Close();

			mWriter = null;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(Boolean disposing)
		{
			if (disposing)
				Close();
		}

		/// <summary>
		/// When overridden in a derived class, flushes the output buffer.
		/// </summary>
		public override void Flush()
		{
			if (this.EnsureWriter())
				mWriter.Flush();
		}

		/// <summary>
		/// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
		/// </summary>
		/// <param name="message">A message to write.</param>
		public override void Write(String message)
		{
			if (this.EnsureWriter())
			{
				if (base.NeedIndent)
					this.WriteIndent();

				mWriter.Write(message);
			}
		}

		/// <summary>
		/// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
		/// </summary>
		/// <param name="message">A message to write.</param>
		public override void WriteLine(string message)
		{
			if (this.EnsureWriter())
			{
				if (base.NeedIndent)
					this.WriteIndent();

				mWriter.WriteLine(message);
				base.NeedIndent = true;
			}
		}
	}
}
