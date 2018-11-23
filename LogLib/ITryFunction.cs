using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ITryFunction<T>:ITry
	{
		T OrThrow(string Message);
		T OrThrow(Func<Exception, Exception> ExceptionFactory);

		bool OrLog(out T Result,string Message);
		bool OrLog(out T Result, Func<Exception, string> MessageFactory);


	}
}
