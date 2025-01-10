using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class DefaultLogFormatter : ILogFormatter
	{
		public string Format(Log Log)
		{
			return $"{Log.DateTime} | {Log.Message.Level} | {Log.ComponentID} | {Log.ComponentName ??"Undefined"} | {Log.MethodName ?? "Undefined"} | {Log.Message.Content ?? "Undefined"}";
		}
	}
}
