using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryFunction<T> : Try,ITryFunction<T>
	{
		private Func<T> first;
		private Action<T> then;

		public TryFunction(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Func<T> Function) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.first = Function;
		}
		public ITryFunction<T> Then(Action<T> Action)
		{
			this.then = Action;
			return this;
		}

		public void OrThrow(string Message)
		{
			OrThrow((Ex, ComponentID, ComponentName, MethodName) => new TryException(Message, Ex, ComponentID, ComponentName, MethodName));
		}



		public void OrThrow(ExceptionFactoryDelegate ExceptionFactory)
		{
			T result;
			try
			{
				result= first();
				if (then != null) then(result);
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw ExceptionFactory(ex, ComponentID, ComponentName, MethodName);
			}
		}

		public void OrThrow<TException>(string Message)
			where TException : TryException
		{
			T result;
			try
			{
				result = first();
				if (then != null) then(result);
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw (TException)Activator.CreateInstance(typeof(TException), Message, ex, ComponentID, ComponentName, MethodName);
			}
		}


		public bool OrAlert( string Message)
		{
			return OrAlert( (Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}
		public bool OrWarn( string Message)
		{
			return OrWarn((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}

		public bool OrAlert(Func<Exception, string> MessageFactory)
		{
			T result;
			try
			{
				result = first();
				if (then != null) then(result);
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
			T result;
			try
			{
				result = first();
				if (then != null) then(result);
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
