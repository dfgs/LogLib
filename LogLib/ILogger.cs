using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ILogger:IDisposable
	{
		/*ILogFormatter Formatter
		{
			get;
		}*/

		void LogEnter(int ComponentID, string ComponentName,string MethodName);
		void LogLeave(int ComponentID, string ComponentName, string MethodName);
		void Log(Log Log);
		void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message);
		void Log(int ComponentID, string ComponentName, string MethodName, Exception ex);
	}

}
