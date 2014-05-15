#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RollingFileTraceListener
// ---------------------------------------------------------------------------------
//	Date Created	: May 15, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace TcmTemplating.Logging
{
	/// <summary>
	/// <see cref="RollingFileTraceListener" /> imeplements a rolling logfile listener
	/// </summary>
	[ConfigurationElementType(typeof(RollingFileTraceListenerData))]
	public class RollingFileTraceListener : RollingFormattedTextWriterTraceListener
	{
		// Fields
		private String mFooter;
		private String mHeader;

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		public RollingFileTraceListener(String fileName): base(fileName)
		{
			mHeader = String.Empty;
			mFooter = String.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatter"><see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" /></param>
		public RollingFileTraceListener(String fileName, ILogFormatter formatter): base(fileName, formatter)
		{
			mHeader = String.Empty;
			mFooter = String.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener"/> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="name">Log name</param>
		public RollingFileTraceListener(String fileName, String name) : base(fileName, name)
		{
			mHeader = String.Empty;
			mFooter = String.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener"/> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="name">Log name</param>
		/// <param name="formatter"><see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" /></param>
		public RollingFileTraceListener(String fileName, String name, ILogFormatter formatter): base(fileName, name, formatter)
		{
			mHeader = String.Empty;
			mFooter = String.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="header">Log header.</param>
		/// <param name="footer">Log footer.</param>
		public RollingFileTraceListener(String fileName, String header, String footer): base(fileName)
		{
			mHeader = header;
			mFooter = footer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListener" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="header">Log header.</param>
		/// <param name="footer">Log footer.</param>
		/// <param name="formatter"><see cref="I:Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.ILogFormatter" /></param>
		public RollingFileTraceListener(String fileName, String header, String footer, ILogFormatter formatter): base(fileName, formatter)
		{
			mHeader = header;
			mFooter = footer;
		}

		/// <summary>
		/// Gets the supported attributes for this <see cref="RollingFileTraceListener" />
		/// </summary>
		/// <returns></returns>
		protected override String[] GetSupportedAttributes()
		{
			return new String[] { "formatter", "fileName", "header", "footer" };
		}

		/// <summary>
		/// Writes trace data to the current <see cref="RollingFileTraceListener" />
		/// </summary>
		/// <param name="eventCache"><see cref="T:System.Diagnostics.TraceEventCache" /></param>
		/// <param name="source">Event source.</param>
		/// <param name="eventType"><see cref="T:System.Diagnostics.TraceEventType" /></param>
		/// <param name="id">Event identifier</param>
		/// <param name="data">Event data</param>
		public override void TraceData(TraceEventCache eventCache, String source, TraceEventType eventType, int id, Object data)
		{
			if (!String.IsNullOrEmpty(mHeader))
				this.WriteLine(mHeader);

			if (data is LogEntry)
			{
				if (base.Formatter != null)
					base.WriteLine(base.Formatter.Format(data as LogEntry));
				else
					base.TraceData(eventCache, source, eventType, id, data);

				base.InstrumentationProvider.FireTraceListenerEntryWrittenEvent();
			}
			else
				base.TraceData(eventCache, source, eventType, id, data);

			if (!String.IsNullOrEmpty(mFooter))
				this.WriteLine(mFooter);
		}
	}
}
