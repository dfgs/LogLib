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

			Assert.AreEqual($"{dateTime} | Debug | 1 | Component | Method | Message", formatter.Format(new Log(dateTime, 1, "Component", "Method", Message.Debug("Message"))));
		}
		[TestMethod]
		public void ShouldFormatWithNullInput()
		{
			ILogFormatter formatter;
			DateTime dateTime;


			formatter = new DefaultLogFormatter();
			dateTime =  DateTime.Now;

			Assert.AreEqual($"{dateTime} | Debug | 1 | Undefined | Undefined | Undefined", formatter.Format(new Log(dateTime, 1, null, null, Message.Debug(null))));
		}

	}
}
