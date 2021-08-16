using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class DebugLogger : BaseLogger
	{
		private readonly object locker = new object();

		public int DebugCount
		{
			get;
			private set;
		}
		public int InformationCount
		{
			get;
			private set;
		}
		public int WarningCount
		{
			get;
			private set;
		}
		public int ErrorCount
		{
			get;
			private set;
		}
		public int FatalCount
		{
			get;
			private set;
		}

		

		public DebugLogger():base()
		{
		}

		public override void Log(Log Log)
		{
			lock(locker)
			{
				switch(Log.Level)
				{
					case LogLevels.Debug:
						DebugCount++;
						break;
					case LogLevels.Information:
						InformationCount++;
						break;
					case LogLevels.Warning:
						WarningCount++;
						break;
					case LogLevels.Error:
						ErrorCount++;
						break;
					case LogLevels.Fatal:
						FatalCount++;
						break;

				}
			}
		}

		

	}
}
