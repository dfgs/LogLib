using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class DefaultLogFormatter : ILogFormatter
	{
		public string Format(DateTime Date, int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			return $"{DateTime.Now} | {Level} | {ComponentID} | {ComponentName} | {MethodName} | {Message}";
		}
	}
}
