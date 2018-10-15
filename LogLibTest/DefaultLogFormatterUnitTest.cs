using System;
using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogLibTest
{
	[TestClass]
	public class DefaultLogFormatterUnitTest
	{
		[TestMethod]
		public void ShouldFormatWithValidInput()
		{
			ILogFormatter formatter;
			DateTime dateTime;

			
			formatter = new DefaultLogFormatter();
			dateTime = DateTime.Now;

			Assert.AreEqual($"{DateTime.Now} | Debug | 1 | Component | Method | Message", formatter.Format(dateTime, 1, "Component", "Method", LogLevels.Debug, "Message"));
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{
			ILogFormatter formatter;
			DateTime dateTime;


			formatter = new DefaultLogFormatter();
			dateTime =  DateTime.Now;

			Assert.AreEqual($"{DateTime.Now} | Debug | 1 | Undefined | Undefined | Undefined", formatter.Format(dateTime, 1, null, null, LogLevels.Debug, null));
		}

	}
}
