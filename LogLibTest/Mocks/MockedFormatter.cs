using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibTest.Mocks
{
	public class MockedFormatter : ILogFormatter
	{
		public string Format(DateTime Date, int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			return Message;
		}
	}
}
