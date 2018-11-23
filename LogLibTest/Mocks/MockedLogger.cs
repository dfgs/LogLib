using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibTest.Mocks
{
	public class MockedLogger : ILogger
	{
		private static ILogFormatter logFormatter= new MockedFormatter();
		public ILogFormatter Formatter => logFormatter;

		public List<string> Logs
		{
			get;
			private set;
		}

		public MockedLogger()
		{
			Logs = new List<string>();
		}
		public void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			Logs.Add(Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, Level, Message));
		}

		public void Log(int ComponentID, string ComponentName, string MethodName, Exception ex)
		{
			Logs.Add(Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, LogLevels.Error, ex.Message));
		}

		public void LogEnter(int ComponentID, string ComponentName, string MethodName)
		{
			Logs.Add(Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, LogLevels.Debug, "Enter"));
		}

		public void LogLeave(int ComponentID, string ComponentName, string MethodName)
		{
			Logs.Add(Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, LogLevels.Debug, "Leave"));
		}


	}
}
