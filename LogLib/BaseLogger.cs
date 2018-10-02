using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public abstract class BaseLogger : ILogger
	{

		public ILogFormatter Formatter
		{
			get;
			private set;
		}

		public BaseLogger(ILogFormatter Formatter)
		{
			this.Formatter = Formatter;
		}

		public abstract void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message);

		public void Log(int ComponentID, string ComponentName, string MethodName, Exception ex)
		{
			Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ex.Message);
		}

		public void LogEnter(int ComponentID, string ComponentName, string MethodName)
		{
			Log(ComponentID, ComponentName, MethodName, LogLevels.Debug, "Enter");
		}

		public void LogLeave(int ComponentID, string ComponentName, string MethodName)
		{
			Log(ComponentID, ComponentName, MethodName, LogLevels.Debug, "Leave");
		}

		
	}
}
