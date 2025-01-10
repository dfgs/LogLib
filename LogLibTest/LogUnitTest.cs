using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using System.IO;

namespace LogLibTest
{
	
	[TestClass]
	public class LogUnitTest
	{

		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			DateTime dateTime;
			Log log;

			dateTime = DateTime.Now;
			log = new Log(dateTime,1, "ComponentName", "MethodName", Message.Error("Message"));

			Assert.AreEqual(dateTime, log.DateTime);
			Assert.AreEqual(1, log.ComponentID);
			Assert.AreEqual("ComponentName", log.ComponentName);
			Assert.AreEqual("MethodName", log.MethodName);
			Assert.AreEqual(LogLevels.Error, log.Message.Level);
			Assert.AreEqual("Message", log.Message.Content);

		}
		[TestMethod]
		public void ShouldSerializeAndDeserialize()
		{
			DateTime dateTime;
			Log log,result;
			byte[] buffer;

			dateTime = DateTime.Now;
			log = new Log(dateTime, 1, "ComponentName", "MethodName", Message.Error("Message"));
			buffer = log.Serialize();
			result = Log.Deserialize(buffer);
			Assert.AreEqual(log.DateTime, result.DateTime);
			Assert.AreEqual(log.ComponentID, result.ComponentID);
			Assert.AreEqual(log.ComponentName, result.ComponentName);
			Assert.AreEqual(log.Message.Level, result.Message.Level);
			Assert.AreEqual(log.MethodName, result.MethodName);
			Assert.AreEqual(log.Message.Content, result.Message.Content);

			log = new Log(dateTime, 1, null, null, Message.Error(null));
			buffer = log.Serialize();
			result = Log.Deserialize(buffer);
			Assert.AreEqual(log.DateTime, result.DateTime);
			Assert.AreEqual(log.ComponentID, result.ComponentID);
			Assert.AreEqual(log.ComponentName, result.ComponentName);
			Assert.AreEqual(log.Message.Level, result.Message.Level);
			Assert.AreEqual(log.MethodName, result.MethodName);
			Assert.AreEqual(log.Message.Content, result.Message.Content);
		}
		[TestMethod]
		public void ShouldNotDeserialize()
		{
			DateTime dateTime;
			byte[] buffer;

			dateTime = DateTime.Now;
			buffer = Encoding.UTF8.GetBytes("abc|abc");
			Assert.ThrowsException<InvalidOperationException>(() => Log.Deserialize(buffer));
			buffer = Encoding.UTF8.GetBytes("abc|abc|abc|abc|abc|abc");
			Assert.ThrowsException<InvalidOperationException>(() => Log.Deserialize(buffer));
		}
		//*/

	}
}
