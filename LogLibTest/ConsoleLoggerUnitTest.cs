﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using System.IO;

namespace LogLibTest
{
	
	[TestClass]
	public class ConsoleLoggerUnitTest
	{

		[TestMethod]
		public void ShouldFormatWithValidInput()
		{
			DateTime dateTime;
			ILogger logger;
			Stream stream;
			TextWriter writer;
			TextReader reader;

			logger = new ConsoleLogger(new DefaultLogFormatter());

			stream = new MemoryStream();
			writer = new StreamWriter(stream,Encoding.UTF8);
			reader = new StreamReader(stream, Encoding.UTF8);
			Console.SetOut(writer);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method", Message.Debug("Message"));

			logger.Log(new Log(dateTime,1, "Component", "Method", Message.Debug("Message")));

			writer.Flush();
			stream.Seek(0, SeekOrigin.Begin);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message", reader.ReadLine());
			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message", reader.ReadLine());
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{
			DateTime dateTime;
			ILogger logger;
			Stream stream;
			TextWriter writer;
			TextReader reader;

			logger = new ConsoleLogger(new DefaultLogFormatter());

			stream = new MemoryStream();
			writer = new StreamWriter(stream, Encoding.UTF8);
			reader = new StreamReader(stream, Encoding.UTF8);
			Console.SetOut(writer);

			dateTime = DateTime.Now;
			logger.Log(1, null, null, Message.Debug(null));
			writer.Flush();
			stream.Seek(0, SeekOrigin.Begin);
			Assert.AreEqual($"{dateTime} | Debug | 1 | Undefined | Undefined | Undefined", reader.ReadLine());
		}
		[TestMethod]
		public void ShouldFormatWithException()
		{
			DateTime dateTime;
			ILogger logger;
			Stream stream;
			TextWriter writer;
			TextReader reader;

			logger = new ConsoleLogger(new DefaultLogFormatter());

			stream = new MemoryStream();
			writer = new StreamWriter(stream, Encoding.UTF8);
			reader = new StreamReader(stream, Encoding.UTF8);
			Console.SetOut(writer);

			dateTime = DateTime.Now;
			logger.Log(1, "Component", "Method",new Exception("Message"));
			writer.Flush();
			stream.Seek(0, SeekOrigin.Begin);
			Assert.AreEqual($"{dateTime} | Error | 1 | Component | Method | Message", reader.ReadLine());
		}
		//*/

	}
}
