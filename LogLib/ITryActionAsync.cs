using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ITryActionAsync:ITry
	{
		Task OrThrow(string Message);
		Task OrThrow<TException>(string Message)
			where TException:TryException;
		Task OrThrow(ExceptionFactoryDelegate ExceptionFactory);

		Task<bool> OrAlert(string Message);
		Task<bool> OrAlert(Func<Exception, string> MessageFactory);

		Task<bool> OrWarn(string Message);
		Task<bool> OrWarn(Func<Exception, string> MessageFactory);

	}
}
