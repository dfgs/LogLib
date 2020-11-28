using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class MemoryLogger : BaseLogger
	{
		private readonly object locker = new object();

		private List<Log> logs;
		public IEnumerable<Log> Logs
		{
			get { return logs; }
		}
		public int Count
		{
			get { return logs.Count; }
		}

		public MemoryLogger():base()
		{
			logs = new List<Log>();
		}

		public override void Log(Log Log)
		{
			lock(locker)
			{
				logs.Add( Log);
			}
		}

		

	}
}
