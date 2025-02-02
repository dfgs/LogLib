﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using System.IO;
using System.Linq;

namespace LogLibTest
{
	
	[TestClass]
	public class MemoryLoggerUnitTest
	{

		[TestMethod]
		public void ShouldLog()
		{
			MemoryLogger logger;

			logger = new MemoryLogger();
			logger.Log(1, "Component", "Method", Message.Debug("Message 1"));
			Assert.AreEqual(1, logger.Count);
			Assert.AreEqual("Message 1", logger.Logs.ElementAt(0).Message.Content);
			logger.Log(1, "Component", "Method", Message.Debug("Message 2"));
			Assert.AreEqual(2, logger.Count);
			Assert.AreEqual("Message 2", logger.Logs.ElementAt(1).Message.Content);
			logger.Log(new Log(DateTime.Now,1, "Component", "Method", Message.Debug("Message 3")));
			Assert.AreEqual(3, logger.Count);
			Assert.AreEqual("Message 3", logger.Logs.ElementAt(2).Message.Content);
		}


	}
}
