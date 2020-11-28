using System;
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

			logger = new MemoryLogger(new DefaultLogFormatter());
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message 1");
			Assert.AreEqual(1, logger.Count);
			Assert.AreEqual(true, logger.Logs.ElementAt(0).Contains("Message 1"));
			logger.Log(1, "Component", "Method", LogLevels.Debug, "Message 2");
			Assert.AreEqual(2, logger.Count);
			Assert.AreEqual(true, logger.Logs.ElementAt(1).Contains("Message 2"));
			logger.Log(new Log(DateTime.Now,1, "Component", "Method", LogLevels.Debug, "Message 3"));
			Assert.AreEqual(3, logger.Count);
			Assert.AreEqual(true, logger.Logs.ElementAt(2).Contains("Message 3"));
		}


	}
}
