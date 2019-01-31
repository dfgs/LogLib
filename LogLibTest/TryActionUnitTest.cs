using System;
using LogLib;
using LogLibTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogLibTest
{
	[TestClass]
	public class TryActionUnitTest
	{
		[TestMethod]
		public void ShouldSuccess()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => { });
			t.OrThrow("Failure");
			Assert.AreEqual(0, logger.Logs.Count);
			Assert.AreEqual(true,t.OrAlert("Failure"));
			Assert.AreEqual(0, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldAlertOnException()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.OrAlert("Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: An unexpected exception occured: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldAlertOnExceptionUsingCustomFormat()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false,t.OrAlert( (Ex)=>"Formatted Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Error: Formatted Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnException()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.OrWarn("Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: An unexpected exception occured: ->Failure", logger.Logs[0]);
		}

		[TestMethod]
		public void ShouldWarnOnExceptionUsingCustomFormat()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.AreEqual(false, t.OrWarn((Ex) => "Formatted Failure"));
			Assert.AreEqual(1, logger.Logs.Count);
			Assert.AreEqual("Warning: Formatted Failure", logger.Logs[0]);
		}


		[TestMethod]
		public void ShouldThrowOnException()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<Exception>(() => t.OrThrow("Failure"));
			Assert.AreEqual(0, logger.Logs.Count);
		}

		[TestMethod]
		public void ShouldThrowOnExceptionUsingCustomException()
		{
			TryAction t;
			MockedLogger logger;

			logger = new MockedLogger();
			t = new TryAction(logger, 1, "TestUnit", "TestMethod", () => throw new InvalidCastException("Failure"));
			Assert.ThrowsException<InvalidOperationException>(() => t.OrThrow( (Ex)=>new InvalidOperationException("Failure",Ex)));
			Assert.AreEqual(0, logger.Logs.Count);
		}


	}
}
