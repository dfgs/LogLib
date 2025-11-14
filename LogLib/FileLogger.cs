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
		private int numberOfFilesToKeep ;

		private StreamWriter writer;

		public FileLogger(ILogFormatter Formatter, string FileName, int NumberOfFilesToKeep) : this(Formatter, new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.Read))
		{
			this.fileName = FileName;
			this.numberOfFilesToKeep = NumberOfFilesToKeep;
		}
		private FileLogger(ILogFormatter Formatter, Stream Stream) : base(Formatter)
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
				System.IO.File.Move(fileName, $"{fileName}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{System.IO.Path.GetExtension(fileName)}");

				string[] files=System.IO.Directory.GetFiles(System.IO.Path.GetFullPath(fileName), $"{System.IO.Path.GetFileNameWithoutExtension(fileName)}*{System.IO.Path.GetExtension(fileName)}");
				foreach (string file in files.OrderByDescending(f => f).Skip(10))
				{
					System.IO.File.Delete(file);
				}

				writer = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read));
			}
		}
	}
}
