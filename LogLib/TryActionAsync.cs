using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public class TryActionAsync : Try,ITryActionAsync
	{
		private Task action;

		public TryActionAsync(ILogger Logger, int ComponentID, string ComponentName, string MethodName, Task Action) : base(Logger,ComponentID,ComponentName,MethodName)
		{
			this.action = Action;
		}


		public async Task OrThrow(string Message)
		{
			await OrThrow((Ex,ComponentID,ComponentName,MethodName) => new TryException(Message, Ex,ComponentID,ComponentName,MethodName));
		}


		public async Task OrThrow(ExceptionFactoryDelegate ExceptionFactory)
		{
			try
			{
				await action;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw ExceptionFactory(ex,ComponentID,ComponentName,MethodName);
			}
		}
		public async Task OrThrow<TException>(string Message)
			where TException:TryException
		{
			try
			{
				await action;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw (TException)Activator.CreateInstance(typeof(TException), Message, ex, ComponentID, ComponentName, MethodName);
			}
		}

		public async Task<bool> OrAlert(string Message)
		{
			return await OrAlert((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}

		public async Task<bool> OrWarn(string Message)
		{
			return await OrWarn((Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}


		public async Task<bool> OrAlert(Func<Exception, string> MessageFactory)
		{
			try
			{
				await action;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, MessageFactory(ex));
				return false;
			}
		}
		public async Task<bool> OrWarn(Func<Exception, string> MessageFactory)
		{
			try
			{
				await action;
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
