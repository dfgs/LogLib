using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public abstract class TextLogger : BaseLogger
	{

		protected ILogFormatter Formatter
		{
			get;
			private set;
		}

		public TextLogger(ILogFormatter Formatter)
		{
			this.Formatter = Formatter;
		}

		

		
	}
}
