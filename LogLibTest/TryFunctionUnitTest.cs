using System;
using LogLib;
using LogLibTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogLibTest
{
	[TestClass]
	public class TryFunctionUnitTest
	{
		[TestMethod]
		public void ShouldSuccess()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => 1234);
		  	Assert.AreEqual(1234, t.OrThrow("Failure"));
			Assert.AreEqual(0, logger.Logs.Count);
			Assert.AreEqual(true, t.OrAlert(out result,"Failure"));
			Assert.AreEqual(1234, result);
			Assert.AreEqual(0, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldAlertOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.OrAlert(out result,"Failure"));
			Assert.AreEqual(0, result);
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: An unexpected exception occured: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldAlertOnExceptionUsingCustomFormat()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.OrAlert(out result, (Ex)=>"Formatted Failure"));
			Assert.AreEqual(0, result);
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: Formatted Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.OrWarn(out result, "Failure"));
			Assert.AreEqual(0, result);
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: An unexpected exception occured: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnExceptionUsingCustomFormat()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.OrWarn(out result, (Ex) => "Formatted Failure"));
			Assert.AreEqual(0, result);
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: Formatted Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldThrowOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<TryException>(() =>result= t.OrThrow("Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldThrowOnExceptionUsingCustomException()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<InvalidOperationException>(() => result=t.OrThrow( (Ex, ComponentID, ComponentName, MethodName) =>new InvalidOperationException("Failure",Ex)));
			Assert.AreEqual(1, logger.Logs.Count);
		}


	}
}
