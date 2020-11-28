using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class Log
	{
		public DateTime DateTime
		{
			get;
			set;
		}
		public int ComponentID
		{
			get;
			set;
		}
		public string ComponentName
		{
			get;
			set;
		}
		public string MethodName
		{
			get;
			set;
		}
		public LogLevels Level
		{
			get;
			set;
		}
		public string Message
		{
			get;
			set;
		}

		public Log()
		{

		}

		public Log(DateTime DateTime,int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			this.DateTime = DateTime; this.ComponentID = ComponentID;this.ComponentName = ComponentName;this.MethodName = MethodName;this.Level = Level;this.Message = Message;
		}

		public byte[] Serialize()
		{
			return Encoding.UTF8.GetBytes($"{DateTime.ToString("O")}|{Level}|{ComponentID}|{ComponentName??""}|{MethodName??""}|{Message ?? ""}");

		}
		public static Log Deserialize(byte[] Buffer)
		{
			string line;
			string[] parts;
			Log log;

			line = Encoding.UTF8.GetString(Buffer);
			parts = line.Split('|');
			if (parts.Length != 6) throw new InvalidOperationException("Invalid buffer");

			log = new Log();
			try
			{
				log.DateTime = DateTime.Parse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
				log.Level = (LogLevels)Enum.Parse(typeof(LogLevels), parts[1]);
				log.ComponentID = int.Parse(parts[2]);
				log.ComponentName = parts[3];
				log.MethodName = parts[4];
				log.Message = parts[5];

				if (log.ComponentName == "") log.ComponentName = null;
				if (log.MethodName == "") log.MethodName = null;
				if (log.Message == "") log.Message = null;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Invalid buffer",ex);
			}

			return log;
		}


	}
}
