using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryAction : Try,ITryAction
	{
		private Action first;
		private Action then;

		public TryAction(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Action Action) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.first = Action;
		}

		public ITryAction Then(Action Action)
		{
			this.then = Action;
			return this;
		}

		public void OrThrow(string Message)
		{
			OrThrow((Ex,ComponentID,ComponentName,MethodName) => new TryException(Message, Ex,ComponentID,ComponentName,MethodName));
		}


		public void OrThrow(ExceptionFactoryDelegate ExceptionFactory)
		{
			try
			{
				first();
				if (then!=null) then();
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw ExceptionFactory(ex,ComponentID,ComponentName,MethodName);
			}
		}
		public void OrThrow<TException>(string Message)
			where TException:TryException
		{
			try
			{
				first();
				if (then != null) then();
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw (TException)Activator.CreateInstance(typeof(TException), Message, ex, ComponentID, ComponentName, MethodName);
			}
		}

		public bool OrAlert(string Message)
		{
			return OrAlert((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}

		public bool OrWarn(string Message)
		{
			return OrWarn((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}


		public bool OrAlert(Func<Exception, string> MessageFactory)
		{
			try
			{
				first();
				if (then != null) then();
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
				first();
				if (then != null) then();
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
