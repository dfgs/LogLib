﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class MemoryLogger : BaseLogger
	{
		private readonly object locker = new object();

		private List<string> logs;
		public IEnumerable<string> Logs
		{
			get { return logs; }
		}
		public int Count
		{
			get { return logs.Count; }
		}

		public MemoryLogger(ILogFormatter Formatter):base(Formatter)
		{
			logs = new List<string>();
		}

		public override void Log(Log Log)
		{
			lock(locker)
			{
				logs.Add( Formatter.Format(Log));
			}
		}

		

	}
}
