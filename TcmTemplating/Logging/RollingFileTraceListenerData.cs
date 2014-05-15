#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RollingFileTraceListenerData
// ---------------------------------------------------------------------------------
//	Date Created	: May 15, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace TcmTemplating.Logging
{
	[Assembler(typeof(RollingFileTraceListenerAssembler))]
	public class RollingFileTraceListenerData : TraceListenerData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData" /> class.
		/// </summary>
		public RollingFileTraceListenerData()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData" /> class.
		/// </summary>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatterName">Log formatter name</param>
		public RollingFileTraceListenerData(String fileName, String formatterName): this("unnamed", fileName, formatterName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData"/> class.
		/// </summary>
		/// <param name="name">Log name.</param>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatterName">Log formatter name</param>
		public RollingFileTraceListenerData(String name, String fileName, String formatterName): this(name, typeof(RollingFileTraceListener), fileName, formatterName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData"/> class.
		/// </summary>
		/// <param name="name">Log name.</param>
		/// <param name="listenerType">Type of the listener.</param>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatterName">Log formatter name</param>
		public RollingFileTraceListenerData(String name, Type listenerType, String fileName, String formatterName): this(name, listenerType, fileName, formatterName, TraceOptions.None)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData"/> class.
		/// </summary>
		/// <param name="name">Log name.</param>
		/// <param name="fileName">Log filename</param>
		/// <param name="header">Log header.</param>
		/// <param name="footer">Log footer.</param>
		/// <param name="formatterName">Log formatter name</param>
		public RollingFileTraceListenerData(String name, String fileName, String header, String footer, String formatterName): this(name, fileName, header, footer, formatterName, TraceOptions.None)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData"/> class.
		/// </summary>
		/// <param name="name">Log name.</param>
		/// <param name="listenerType">Type of the listener.</param>
		/// <param name="fileName">Log filename</param>
		/// <param name="formatterName">Log formatter name</param>
		/// <param name="traceOutputOptions"><see cref="T:System.Diagnostics.TraceOptions" /></param>
		public RollingFileTraceListenerData(String name, Type listenerType, String fileName, String formatterName, TraceOptions traceOutputOptions): base(name, listenerType, traceOutputOptions)
		{
			this.FileName = fileName;
			this.Formatter = formatterName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RollingFileTraceListenerData"/> class.
		/// </summary>
		/// <param name="name">Log name.</param>
		/// <param name="fileName">Log filename</param>
		/// <param name="header">Log header.</param>
		/// <param name="footer">Log footer.</param>
		/// <param name="formatterName">Log formatter name</param>
		/// <param name="traceOutputOptions"><see cref="T:System.Diagnostics.TraceOptions" /></param>
		public RollingFileTraceListenerData(String name, String fileName, String header, String footer, String formatterName, TraceOptions traceOutputOptions): this(name, typeof(RollingFileTraceListener), fileName, formatterName, traceOutputOptions)
		{
			this.Header = header;
			this.Footer = footer;
		}

		/// <summary>
		/// Gets or sets the log filename
		/// </summary>
		/// <value>
		/// Log filename
		/// </value>
		[ConfigurationProperty("fileName", IsRequired = true)]
		public String FileName
		{
			get
			{
				return (String)base["fileName"];
			}
			set
			{
				base["fileName"] = value;
			}
		}

		/// <summary>
		/// Gets or sets log footer
		/// </summary>
		/// <value>
		/// Log footer.
		/// </value>
		[ConfigurationProperty("footer", IsRequired = false)]
		public String Footer
		{
			get
			{
				return (String)base["footer"];
			}
			set
			{
				base["footer"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the log formatter.
		/// </summary>
		/// <value>
		/// Log formatter.
		/// </value>
		[ConfigurationProperty("formatter", IsRequired = false)]
		public String Formatter
		{
			get
			{
				return (String)base["formatter"];
			}
			set
			{
				base["formatter"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the log header.
		/// </summary>
		/// <value>
		/// Log header.
		/// </value>
		[ConfigurationProperty("header", IsRequired = false)]
		public String Header
		{
			get
			{
				return (String)base["header"];
			}
			set
			{
				base["header"] = value;
			}
		}
	}
}
