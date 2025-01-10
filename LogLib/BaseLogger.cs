using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public abstract class BaseLogger : ILogger
	{

		

		public BaseLogger()
		{
		
		}
		public virtual void Dispose()
		{

		}

		public abstract void Log(Log Log);
		public void Log(int ComponentID, string ComponentName, string MethodName, Message Message)
		{
			Log log;
			log = new Log(DateTime.Now, ComponentID, ComponentName, MethodName,  Message);
			Log(log);
		}

		public void Log(int ComponentID, string ComponentName, string MethodName, Exception ex)
		{
			Log(ComponentID, ComponentName, MethodName, Message.Error(ex.Message));
		}

		public void LogEnter(int ComponentID, string ComponentName, string MethodName)
		{
			Log(ComponentID, ComponentName, MethodName, Message.Debug("Enter"));
		}

		public void LogLeave(int ComponentID, string ComponentName, string MethodName)
		{
			Log(ComponentID, ComponentName, MethodName, Message.Debug("Leave"));
		}

		
	}
}
