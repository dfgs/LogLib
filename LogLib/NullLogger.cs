using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public sealed class NullLogger : ILogger
	{
		

		private static NullLogger instance=new NullLogger();
		public static NullLogger Instance
		{
			get { return instance; }
		}
		
		private NullLogger()
		{

		}
		public void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
		}

		public void Log(int ComponentID, string ComponentName, string MethodName, Exception ex)
		{
		}

		public void LogEnter(int ComponentID, string ComponentName, string MethodName)
		{
		}

		public void LogLeave(int ComponentID, string ComponentName, string MethodName)
		{
		}
	}
}
