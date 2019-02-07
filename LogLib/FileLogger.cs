using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public sealed class FileLogger : BaseLogger,IDisposable
	{
		private readonly object locker = new object();


		private readonly StreamWriter writer;

		public FileLogger(ILogFormatter Formatter, string FileName) : this(Formatter, new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
		{
		}
		public FileLogger(ILogFormatter Formatter, Stream Stream) : base(Formatter)
		{
			writer = new StreamWriter(Stream);
		}

		public void Dispose()
		{
			writer.Dispose();
		}

		public override void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			lock (locker)
			{
				writer.WriteLine(Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, Level, Message));
				writer.Flush();
			}
		}

		
	}
}
