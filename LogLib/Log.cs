using System;
using System.Collections.Generic;
using System.Data;
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
		public Message Message
		{
			get;
			set;
		}

		public Log()
		{

		}

		public Log(DateTime DateTime,int ComponentID, string ComponentName, string MethodName, Message Message)
		{
			this.DateTime = DateTime; this.ComponentID = ComponentID;this.ComponentName = ComponentName;this.MethodName = MethodName;this.Message = Message;
		}

		public override string ToString()
		{
			return $"{DateTime} {Message.Level} {ComponentID} {ComponentName ?? "Undefined"} {MethodName ?? "Undefined"} {Message.Content ?? "Undefined"}";
		}

		public byte[] Serialize()
		{
			return Encoding.UTF8.GetBytes($"{DateTime.ToString("O")}|{Message.Level}|{ComponentID}|{ComponentName??""}|{MethodName??""}|{Message.Content ?? ""}");

		}
		public static Log Deserialize(byte[] Buffer)
		{
			string line;
			string[] parts;
			DateTime dateTime;
			LogLevels level; 
			string content;
			int componentID;
			string componentName;
			string methodName;

			line = Encoding.UTF8.GetString(Buffer);
			parts = line.Split('|');
			if (parts.Length != 6) throw new InvalidOperationException("Invalid buffer");

			
			try
			{
				dateTime = DateTime.Parse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
				level = (LogLevels)Enum.Parse(typeof(LogLevels), parts[1]);
				componentID = int.Parse(parts[2]);
				componentName = parts[3];
				methodName = parts[4];
				content = parts[5];

				if (componentName == "") componentName = null;
				if (methodName == "") methodName = null;
				if (content == "") content = null;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Invalid buffer",ex);
			}

			return new Log(dateTime,componentID,componentName,methodName,new Message(level,content));
		}


	}
}
