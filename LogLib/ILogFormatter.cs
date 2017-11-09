using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ILogFormatter
	{
		string Format(DateTime Date,int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message);
	}
}
