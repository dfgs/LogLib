using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ITryAction:ITry
	{
		void OrThrow(string Message);
		void OrThrow(Func<Exception, Exception> ExceptionFactory);

		bool OrLog(string Message);
		bool OrLog(Func<Exception, string> MessageFactory);


	}
}
