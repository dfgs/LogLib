using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibTest.Mocks
{
	public class TestException : TryException
	{
		public TestException(string Message, Exception InnerException, int ComponentID, string ComponentName, string MethodName) : base(Message, InnerException, ComponentID, ComponentName, MethodName)
		{
		}
	}
}
