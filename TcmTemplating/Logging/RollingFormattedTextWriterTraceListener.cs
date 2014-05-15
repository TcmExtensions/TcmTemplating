#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RollingFormattedTextWriterTraceListener
// ---------------------------------------------------------------------------------
//	Date Created	: May 14, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Instrumentation;

namespace TcmTemplating.Logging
{
	/// <summary>
	/// <see cref="RollingFormattedTextWriterTraceListener" /> implements a <see cref="T:TcmTemplating.Logging.RollingTextWriterTraceListener" />
	/// </summary>
	public class RollingFormattedTextWriterTraceListener : RollingTextWriterTraceListener, IInstrumentationEventProvider
	{
		private ILogFormatter mFormatter;
		private LoggingInstrumentationProvider mInstrumentationProvider;

		/// <summary>
		/// Gets or sets the <see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" />
		/// </summary>
		/// <value>
		/// <see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" />
		/// </value>
		public ILogFormatter Formatter
		{
			get
			{
				return mFormatter;
			}
			set
			{
				mFormatter = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Instrumentation.LoggingInstrumentationProvider" />
		/// </summary>
		/// <value>
		/// <see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Instrumentation.LoggingInstrumentationProvider" />
		/// </value>
		protected LoggingInstrumentationProvider InstrumentationProvider
		{
			get
			{
				return mInstrumentationProvider;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFormattedTextWriterTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		public RollingFormattedTextWriterTraceListener(String fileName): base(RootFileNameAndEnsureTargetFolderExists(fileName))
		{
			mInstrumentationProvider = new LoggingInstrumentationProvider();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFormattedTextWriterTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="name">Log name.</param>
	    public RollingFormattedTextWriterTraceListener(String fileName, String name) : base(RootFileNameAndEnsureTargetFolderExists(fileName), name)
		{
			mInstrumentationProvider = new LoggingInstrumentationProvider();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFormattedTextWriterTraceListener"/> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatter"><see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" /></param>
		public RollingFormattedTextWriterTraceListener(String fileName, ILogFormatter formatter): this(fileName)
		{
			this.Formatter = formatter;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFormattedTextWriterTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="name">Log name.</param>
		/// <param name="formatter"><see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" /></param>
		public RollingFormattedTextWriterTraceListener(String fileName, String name, ILogFormatter formatter) : this(fileName, name)
		{
		}

		/// <summary>
		/// Gets the instrumentation event provider.
		/// </summary>
		/// <returns></returns>
		public Object GetInstrumentationEventProvider()
		{
			return mInstrumentationProvider;
		}

		/// <summary>
		/// Gets the supported attributes.
		/// </summary>
		/// <returns></returns>
		protected override String[] GetSupportedAttributes()
		{
			return new String[] { "formatter" };
		}

		/// <summary>
		/// Returns the root filename the file name and ensure target folder exists.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <returns>Log root filename</returns>
		private static String RootFileNameAndEnsureTargetFolderExists(String fileName)
		{
			String path = fileName;

			if (!Path.IsPathRooted(path))
				path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

			String directoryName = Path.GetDirectoryName(path);

			if (!(String.IsNullOrEmpty(directoryName) || Directory.Exists(directoryName)))
				Directory.CreateDirectory(directoryName);

			return path;
		}

		/// <summary>
		/// Writes trace data to the current <see cref="RollingFormattedTextWriterTraceListener" />
		/// </summary>
		/// <param name="eventCache"><see cref="T:System.Diagnostics.TraceEventCache" /></param>
		/// <param name="source">Event source.</param>
		/// <param name="eventType"><see cref="T:System.Diagnostics.TraceEventType" /></param>
		/// <param name="id">Event identifier</param>
		/// <param name="data">Event data</param>
		public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, int id, object data)
		{
			if (data is LogEntry)
			{
				if (this.Formatter != null)
					base.Write(this.Formatter.Format(data as LogEntry));
				else
					base.TraceData(eventCache, source, eventType, id, data);

				this.InstrumentationProvider.FireTraceListenerEntryWrittenEvent();
			}
			else
				base.TraceData(eventCache, source, eventType, id, data);
		}
	}
}
