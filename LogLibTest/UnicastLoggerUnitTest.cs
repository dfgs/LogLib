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
	public class UnicastLoggerUnitTest
	{
		[TestMethod]
		public void ShouldFormatWithValidInputWhenNoReceiverIsRunning()
		{
			DateTime dateTime;
			UnicastLogger logger;
			
			logger = new UnicastLogger(IPAddress.Parse("127.0.0.1"), 3020);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message");

			logger.Dispose();
		}
		[TestMethod]
		public void ShouldFormatWithValidInput()
		{
			DateTime dateTime;
			UnicastLogger logger;
			MockedUnicastReceiver receiver;

			receiver = new MockedUnicastReceiver( 3021);

			logger = new UnicastLogger(IPAddress.Parse("127.0.0.1"), 3021);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message0");
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1,receiver.Logs.Count);
			Assert.AreEqual("Message0", receiver.Logs[0].Message);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message1");
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual("Message1", receiver.Logs[1].Message);

			dateTime = DateTime.Now;
			logger.Log(new Log(dateTime,1, "Component", "Method", LogLevels.Debug, "Message2"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(3, receiver.Logs.Count);
			Assert.AreEqual("Message2", receiver.Logs[2].Message);

			logger.Dispose();
			receiver.Dispose();
			
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{

			DateTime dateTime;
			UnicastLogger logger;
			MockedUnicastReceiver receiver;

			receiver = new MockedUnicastReceiver( 3022);

			logger = new UnicastLogger( IPAddress.Parse("127.0.0.1"), 3022);

			dateTime = DateTime.Now;
			logger.Log(1, null,null, LogLevels.Debug, null);
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1, receiver.Logs.Count);
			Assert.AreEqual(null, receiver.Logs[0].Message);

			dateTime = DateTime.Now;
			logger.Log(1, null, null, LogLevels.Debug, null);
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual(null, receiver.Logs[1].Message);

			logger.Dispose();
			receiver.Dispose();

		}

		[TestMethod]
		public void ShouldFormatWithException()
		{
			DateTime dateTime;
			UnicastLogger logger;
			MockedUnicastReceiver receiver;

			receiver = new MockedUnicastReceiver( 3023);

			logger = new UnicastLogger( IPAddress.Parse("127.0.0.1"), 3023);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", new Exception("Message1"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(1, receiver.Logs.Count);
			Assert.AreEqual("Message1", receiver.Logs[0].Message);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", new Exception("Message2"));
			receiver.ReceivedEvent.WaitOne();
			Assert.AreEqual(2, receiver.Logs.Count);
			Assert.AreEqual("Message2", receiver.Logs[1].Message);


			logger.Dispose();
			receiver.Dispose();

		}
		//*/

	}
}
