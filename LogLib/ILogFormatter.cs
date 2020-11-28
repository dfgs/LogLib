using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public interface ILogFormatter
	{
		string Format(Log Log);
	}
}
