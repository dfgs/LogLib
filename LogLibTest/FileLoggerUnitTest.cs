using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using System.IO;

namespace LogLibTest
{

	[TestClass]
	public class FileLoggerUnitTest
	{

		[TestMethod]
		public void ShouldFormatWithValidInput()
		{
			DateTime dateTime;
			ILogger logger;
			TextReader reader;

			using (FileStream stream = new FileStream("log.txt", FileMode.Create))
			{
				logger = new FileLogger(new DefaultLogFormatter(),stream);

				dateTime = DateTime.Now;
				logger.Log(1, "Component", "Method", LogLevels.Debug, "Message");
				logger.Log(new Log(dateTime,1, "Component", "Method", LogLevels.Debug, "Message"));
			}
			using (FileStream stream = new FileStream("log.txt", FileMode.Open))
			{
				reader = new StreamReader(stream);
				Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message", reader.ReadLine());
				Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message", reader.ReadLine());
			}
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{
			DateTime dateTime;
			ILogger logger;
			TextReader reader;

			using (FileStream stream = new FileStream("log.txt", FileMode.Create))
			{
				logger = new FileLogger(new DefaultLogFormatter(), stream);

				dateTime = DateTime.Now;
				logger.Log(1, null, null, LogLevels.Debug, null);
			}
			using (FileStream stream = new FileStream("log.txt", FileMode.Open))
			{
				reader = new StreamReader(stream);
				Assert.AreEqual($"{dateTime} | Debug | 1 | Undefined | Undefined | Undefined", reader.ReadLine());
			}
		}
		[TestMethod]
		public void ShouldFormatWithException()
		{
			DateTime dateTime;
			ILogger logger;
			TextReader reader;

			using (FileStream stream = new FileStream("log.txt", FileMode.Create))
			{
				logger = new FileLogger(new DefaultLogFormatter(), stream);

				dateTime = DateTime.Now;
				logger.Log(1, "Component", "Method", new Exception("Message"));
			}
			using (FileStream stream = new FileStream("log.txt", FileMode.Open))
			{
				reader = new StreamReader(stream);
				Assert.AreEqual($"{dateTime} | Error | 1 | Component | Method | Message", reader.ReadLine());
			}
		}
		//*/

	}
}
