using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryFunctionAsync<T> : Try, ITryFunctionAsync<T>
	{
		private Task<T> first;
		private Action<T> then;

		public TryFunctionAsync(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Task<T> Function) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.first = Function;
		}

		public ITryFunctionAsync<T> Then(Action<T> Action)
		{
			this.then = Action;
			return this;
		}

		public async Task OrThrow(string Message)
		{
			await OrThrow((Ex, ComponentID, ComponentName, MethodName) => new TryException(Message, Ex, ComponentID, ComponentName, MethodName));
		}



		public async Task OrThrow(ExceptionFactoryDelegate ExceptionFactory)
		{
			T result;
			try
			{
				result = await first;
				if (then != null) then(result);
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw ExceptionFactory(ex, ComponentID, ComponentName, MethodName);
			}
		}

		public async Task OrThrow<TException>(string Message)
			where TException : TryException
		{
			T result;
			try
			{
				result = await first;
				if (then != null) then(result);
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw (TException)Activator.CreateInstance(typeof(TException), Message, ex, ComponentID, ComponentName, MethodName);
			}
		}

		

		public async Task<bool> OrAlert( string Message)
		{
			return await OrAlert((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}
		public async Task<bool> OrWarn(string Message)
		{
			return await OrWarn((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}


		public async Task<bool> OrAlert( Func<Exception, string> MessageFactory)
		{
			T result;
			try
			{
				result = await first;
				if (then != null) then(result);
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, MessageFactory(ex));
				return false;
			}
		}


		public async Task<bool> OrWarn( Func<Exception, string> MessageFactory)
		{
			T result;
			try
			{
				result = await first;
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
