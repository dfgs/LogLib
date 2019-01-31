using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryAction : Try,ITryAction
	{
		private Action action;

		public TryAction(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Action Action) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.action = Action;
		}


		public void OrThrow(string Message)
		{
			OrThrow((Ex) => new Exception(Message, Ex));
		}


		public void OrThrow(Func<Exception, Exception> ExceptionFactory)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				throw ExceptionFactory(ex);
			}
		}
		public bool OrAlert(string Message)
		{
			return OrAlert((Ex) => $"An unexpected exception occured: {ExceptionFormatter.Format(Ex)}");
		}

		public bool OrWarn(string Message)
		{
			return OrWarn((Ex) => $"An unexpected exception occured: {ExceptionFormatter.Format(Ex)}");
		}


		public bool OrAlert(Func<Exception, string> MessageFactory)
		{
			try
			{
				action();
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, MessageFactory(ex));
				return false;
			}
		}
		public bool OrWarn(Func<Exception, string> MessageFactory)
		{
			try
			{
				action();
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Warning, MessageFactory(ex));
				return false;
			}
		}

	}
}
