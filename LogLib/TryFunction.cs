using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryFunction<T> : Try,ITryFunction<T>
	{
		private Func<T> function;

		public TryFunction(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Func<T> Function) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.function = Function;
		}


		public T OrThrow(string Message)
		{
			return OrThrow((Ex) => new Exception(Message, Ex));
		}

		public bool OrLog(out T Result, string Message)
		{
			return OrLog(out Result, (Ex) => $"An unexpected exception occured: {Ex.Message}");
		}


		public T OrThrow(Func<Exception, Exception> ExceptionFactory)
		{
			try
			{
				return function();
			}
			catch (Exception ex)
			{
				throw ExceptionFactory(ex);
			}
		}

		public bool OrLog(out T Result,Func<Exception, string> MessageFactory)
		{
			try
			{
				Result = function();
				return true;
			}
			catch (Exception ex)
			{
				Result = default(T);
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, MessageFactory(ex));
				return false;
			}
		}


	}
}
