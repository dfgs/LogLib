using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class ConsoleLogger : BaseLogger
	{
		private readonly object locker = new object();

		

		public ConsoleLogger(ILogFormatter Formatter):base(Formatter)
		{
		}

		public override void Log(Log Log)
		{
			ConsoleColor colorBackup;

			lock(locker)
			{
				colorBackup = Console.ForegroundColor;
				switch(Log.Level)
				{
					case LogLevels.Debug:
						Console.ForegroundColor = ConsoleColor.Gray;
						break;
					case LogLevels.Information:
						Console.ForegroundColor = ConsoleColor.White;
						break;
					case LogLevels.Warning:
						Console.ForegroundColor = ConsoleColor.Yellow;
						break;
					case LogLevels.Error:
						Console.ForegroundColor = ConsoleColor.Red;
						break;
					case LogLevels.Fatal:
						Console.ForegroundColor = ConsoleColor.Magenta;
						break;
				}
				Console.WriteLine(Formatter.Format(Log));
				Console.ForegroundColor = colorBackup;
			}
		}

		

	}
}
