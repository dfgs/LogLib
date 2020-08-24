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
		T OrThrow<TException>(string Message)
			where TException : TryException;

		T OrThrow(ExceptionFactoryDelegate ExceptionFactory);

		bool OrAlert(out T Result,string Message);
		bool OrAlert(out T Result, Func<Exception, string> MessageFactory);

		bool OrWarn(out T Result, string Message);
		bool OrWarn(out T Result, Func<Exception, string> MessageFactory);

	}
}
