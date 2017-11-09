using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class ConsoleLogger : ILogger
	{
		private string locker = "locker";

		public ILogFormatter Formatter
		{
			get;
			private set;
		}

		public ConsoleLogger(ILogFormatter Formatter)
		{
			this.Formatter = Formatter;
		}

		public void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			lock(locker)
			{
				switch(Level)
				{
					case LogLevels.Debug:
						Console.ForegroundColor = ConsoleColor.Gray;
						break;
					case LogLevels.Information:
						Console.ForegroundColor = ConsoleColor.White;
						break;
					case LogLevels.Warning:
						Console.ForegroundColor = ConsoleColor.Yellow;
						break;
					case LogLevels.Error:
						Console.ForegroundColor = ConsoleColor.Red;
						break;
					case LogLevels.Fatal:
						Console.ForegroundColor = ConsoleColor.Magenta;
						break;
				}
				Console.WriteLine(Formatter.Format(DateTime.Now,ComponentID,ComponentName,MethodName,Level,Message));
			}
		}

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
