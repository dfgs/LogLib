﻿using System;
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
			return OrThrow((Ex, ComponentID, ComponentName, MethodName) => new TryException(Message, Ex, ComponentID, ComponentName, MethodName));
		}



		public T OrThrow(ExceptionFactoryDelegate ExceptionFactory)
		{
			try
			{
				return function();
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw ExceptionFactory(ex, ComponentID, ComponentName, MethodName);
			}
		}

		public T OrThrow<TException>(string Message)
			where TException : TryException
		{
			try
			{
				return function();
			}
			catch (Exception ex)
			{
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Error, ExceptionFormatter.Format(ex));
				throw (TException)Activator.CreateInstance(typeof(TException), Message, ex, ComponentID, ComponentName, MethodName);
			}
		}


		public bool OrAlert(out T Result, string Message)
		{
			return OrAlert(out Result, (Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}
		public bool OrWarn(out T Result, string Message)
		{
			return OrWarn(out Result, (Ex) => $"{Message}: {ExceptionFormatter.Format(Ex)}");
		}

		public bool OrAlert(out T Result,Func<Exception, string> MessageFactory)
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
		public bool OrWarn(out T Result, Func<Exception, string> MessageFactory)
		{
			try
			{
				Result = function();
				return true;
			}
			catch (Exception ex)
			{
				Result = default(T);
				Logger.Log(ComponentID, ComponentName, MethodName, LogLevels.Warning, MessageFactory(ex));
				return false;
			}
		}

	}
}
