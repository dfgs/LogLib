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

		private List<Log> logs;

		public DebugLogger():base()
		{
			logs = new List<Log>();
		}

		public override void Log(Log Log)
		{
			lock(locker)
			{
				logs.Add(Log);
				switch(Log.Message.Level)
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

		public bool LogsContainKeyWords(LogLevels Level,params string[] KeyWords)
		{
			bool result;

			foreach(Log log in logs.Where(item=>item.Message.Level==Level))
			{
				result = true;
				foreach(string key in KeyWords)
				{
					if (!log.Message.Content.Contains(key))
					{
						result = false;
						break;
					}
				}
				if (result) return true;
			}
			return false;
		}
		

	}
}
