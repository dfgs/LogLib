using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public sealed class FileLogger : TextLogger,IDisposable
	{
		private readonly object locker = new object();
		private string fileName;
		
		private StreamWriter writer;

		public FileLogger(ILogFormatter Formatter, string FileName) : this(Formatter, new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
		{
		}
		public FileLogger(ILogFormatter Formatter, Stream Stream) : base(Formatter)
		{
			writer = new StreamWriter(Stream);
		}

		public override void Dispose()
		{
			writer.Dispose();
		}

		public override void Log(Log Log)
		{
			lock (locker)
			{
				writer.WriteLine(Formatter.Format(Log));
				writer.Flush();
			}
		}

		public override void Rotate()
		{
			lock (locker)
			{
				writer.BaseStream.Close();
				writer.Close();
				System.IO.File.Move(fileName, $"{fileName}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}");
				writer = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read));
			}
		}
	}
}
