using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using System.IO;
using System.Net;
using LogLibTest.Mocks;
using System.Threading;

namespace LogLibTest
{

	[TestClass]
	public class MulticastLoggerUnitTest
	{
		[TestMethod]
		public void ShouldFormatWithValidInputWhenNoReceiverIsRunning()
		{
			DateTime dateTime;
			MulticastLogger logger;
			
			logger = new MulticastLogger(new DefaultLogFormatter(),IPAddress.Parse("224.0.0.1"),2020);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message");

			logger.Dispose();
		}
		[TestMethod]
		public void ShouldFormatWithValidInput()
		{
			DateTime dateTime;
			MulticastLogger logger;
			MockedMulticastReceiver receiver;

			receiver = new MockedMulticastReceiver(IPAddress.Parse("224.0.0.1"), 2021);

			logger = new MulticastLogger(new DefaultLogFormatter(), IPAddress.Parse("224.0.0.1"), 2021);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message0");
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1,receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message0", receiver.Logs[0]);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message1");
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message1", receiver.Logs[1]);

			dateTime = DateTime.Now;
			logger.Log(new Log(dateTime,1, "Component", "Method", LogLevels.Debug, "Message2"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(3, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message2", receiver.Logs[2]);

			logger.Dispose();
			receiver.Dispose();
			
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{

			DateTime dateTime;
			MulticastLogger logger;
			MockedMulticastReceiver receiver;

			receiver = new MockedMulticastReceiver(IPAddress.Parse("224.0.0.1"), 2022);

			logger = new MulticastLogger(new DefaultLogFormatter(), IPAddress.Parse("224.0.0.1"), 2022);

			dateTime = DateTime.Now;
			logger.Log(1, null,null, LogLevels.Debug, null);
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Undefined | Undefined | Undefined", receiver.Logs[0]);

			dateTime = DateTime.Now;
			logger.Log(1, null, null, LogLevels.Debug, null);
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Undefined | Undefined | Undefined", receiver.Logs[1]);

			logger.Dispose();
			receiver.Dispose();

		}

		[TestMethod]
		public void ShouldFormatWithException()
		{
			DateTime dateTime;
			MulticastLogger logger;
			MockedMulticastReceiver receiver;

			receiver = new MockedMulticastReceiver(IPAddress.Parse("224.0.0.1"), 2023);

			logger = new MulticastLogger(new DefaultLogFormatter(), IPAddress.Parse("224.0.0.1"), 2023);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", new Exception("Message"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Error | 1 | Component | Method | Message", receiver.Logs[0]);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", new Exception("Message"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual($"{dateTime} | Error | 1 | Component | Method | Message", receiver.Logs[1]);


			logger.Dispose();
			receiver.Dispose();

		}
		//*/

	}
}
