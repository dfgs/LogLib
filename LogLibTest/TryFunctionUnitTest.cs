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

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => 1234);
		  	t.Then(result=>Assert.AreEqual(1234,result)).OrThrow("Failure");
			Assert.AreEqual(0, logger.Logs.Count);
			
			Assert.AreEqual(true, t.Then(result=> Assert.AreEqual(1234, result)).OrAlert("Failure"));
			Assert.AreEqual(0, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldAlertOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.Then(result=>Assert.AreEqual(0,result)).OrAlert("Failure message"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: Failure message: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldAlertOnExceptionUsingCustomFormat()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.Then(result => Assert.AreEqual(0, result)).OrAlert((Ex)=>"Formatted Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: Formatted Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.Then(result => Assert.AreEqual(0, result)).OrWarn("Failure message"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: Failure message: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnExceptionUsingCustomFormat()
		{
			TryFunction<int> t;
			MockedLogger logger;
			int result=-1;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.Then(value=>result=value).OrWarn( (Ex) => "Formatted Failure"));
			Assert.AreEqual(-1, result);
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: Formatted Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldThrowOnException()
		{
			TryFunction<int> t;
			MockedLogger logger;
	
			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<TryException>(() =>t.OrThrow("Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldThrowOnExceptionAndContainsValidInformation()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			try
			{
				t.OrThrow("Failure");
				Assert.Fail();
			}
			catch (TryException ex)
			{
				Assert.IsNotNull(ex.InnerException);
				Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidCastException));
				Assert.AreEqual(1, ex.ComponentID);
				Assert.AreEqual("TestUnit", ex.ComponentName);
				Assert.AreEqual("TestMethod", ex.MethodName);
			}
			Assert.AreEqual(1, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldThrowOnExceptionUsingCustomException()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<InvalidOperationException>(() => t.OrThrow( (Ex, ComponentID, ComponentName, MethodName) =>new InvalidOperationException("Failure",Ex)));
			Assert.AreEqual(1, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldThrowOnExceptionUsingCustomGenericException()
		{
			TryFunction<int> t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<TestException>(() =>  t.OrThrow<TestException>("Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
		}


		[TestMethod]
		public void ShouldThrowOnExceptionUsingCustomGenericExceptionAndContainsValidInformation()
		{
			TryFunction<int> t;
			MockedLogger logger;
	
			logger = new MockedLogger();
			t = new TryFunction<int>(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			try
			{
				t.OrThrow<TestException>("Failure");
				Assert.Fail();
			}
			catch (TestException ex)
			{
				Assert.IsNotNull(ex.InnerException);
				Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidCastException));
				Assert.AreEqual(1, ex.ComponentID);
				Assert.AreEqual("TestUnit", ex.ComponentName);
				Assert.AreEqual("TestMethod", ex.MethodName);
			}
			Assert.AreEqual(1, logger.Logs.Count);
		}




	}
}
