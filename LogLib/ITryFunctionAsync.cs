using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ITryFunctionAsync<T>:ITry
	{
		Task<T> OrThrow(string Message);
		Task<T> OrThrow<TException>(string Message)
			where TException : TryException;

		Task<T> OrThrow(ExceptionFactoryDelegate ExceptionFactory);

		// out not permitted with async funcs
		/*Task<bool> OrAlert(out T Result,string Message);
		Task<bool> OrAlert(out T Result, Func<Exception, string> MessageFactory);

		Task<bool> OrWarn(out T Result, string Message);
		Task<bool> OrWarn(out T Result, Func<Exception, string> MessageFactory);//*/

	}
}
