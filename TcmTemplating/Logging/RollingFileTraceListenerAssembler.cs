#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: RollingFileTraceListenerAssembler
// ---------------------------------------------------------------------------------
//	Date Created	: May 15, 2014
//	Author			: Rob van Oostenrijk
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.ObjectBuilder;

namespace TcmTemplating.Logging
{
	/// <summary>
	/// <see cref="RollingFileTraceListenerAssembly" /> is a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.TraceListenerAssembler" /> which initializes a
	/// <see cref="T:TcmTemplating.Logging.RollingFileTraceListener" />
	/// </summary>
	public class RollingFileTraceListenerAssembler : TraceListenerAsssembler
	{
		// Methods
		public override TraceListener Assemble(IBuilderContext context, TraceListenerData objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
		{
			RollingFileTraceListenerData data = (RollingFileTraceListenerData)objectConfiguration;
			return new RollingFileTraceListener(data.FileName, data.Header, data.Footer, base.GetFormatter(context, data.Formatter, configurationSource, reflectionCache));
		}
	}
}
