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

		bool OrAlert(string Message);
		bool OrAlert(Func<Exception, string> MessageFactory);

		bool OrWarn(string Message);
		bool OrWarn(Func<Exception, string> MessageFactory);

	}
}
