using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public struct Message
	{
		public LogLevels Level
		{
			get;
			private set;
		}
		public string Content
		{
			get;
			private set;
		}

		public Message(LogLevels Level, string Content)
		{
			this.Level = Level;
			this.Content = Content;
		}


		public static Message Debug(string Content)
		{
			return new Message(LogLevels.Debug, Content);
		}
		public static Message Information(string Content)
		{
			return new Message(LogLevels.Information, Content);
		}
		public static Message Warning(string Content)
		{
			return new Message(LogLevels.Warning, Content);
		}
		public static Message Error(string Content)
		{
			return new Message(LogLevels.Error, Content);
		}
		public static Message Fatal(string Content)
		{
			return new Message(LogLevels.Fatal, Content);
		}

	}
}
