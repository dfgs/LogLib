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

		public bool OrLog(string Message)
		{
			return OrLog((Ex) => $"An unexpected exception occured: {Ex.Message}");
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

		public bool OrLog(Func<Exception, string> MessageFactory)
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


	}
}
