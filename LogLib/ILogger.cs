using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ILogger
	{
		ILogFormatter Formatter
		{
			get;
		}

		void LogEnter(int ComponentID, string ComponentName,string MethodName);
		void LogLeave(int ComponentID, string ComponentName, string MethodName);
		void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message);
		void Log(int ComponentID, string ComponentName, string MethodName, Exception ex);
	}

}
