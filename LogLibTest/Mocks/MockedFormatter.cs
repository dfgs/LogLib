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
		public string Format(Log Log)
		{
			return Log.Level.ToString() +": "+ Log.Message;
		}
	}
}
