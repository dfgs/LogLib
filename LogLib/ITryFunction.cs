using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ITryFunction<T>:ITry
	{
		ITryFunction<T> Then(Action<T> Action);
		void OrThrow(string Message);
		void OrThrow<TException>(string Message)
			where TException : TryException;

		void OrThrow(ExceptionFactoryDelegate ExceptionFactory);

		bool OrAlert(string Message);
		bool OrAlert( Func<Exception, string> MessageFactory);

		bool OrWarn(string Message);
		bool OrWarn(Func<Exception, string> MessageFactory);

	}
}
