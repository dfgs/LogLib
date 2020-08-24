using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{

	public delegate Exception ExceptionFactoryDelegate(Exception InnerException, int ComponentID, string ComponentName, string MethodName);
	public class TryException:Exception
	{
		

		public int ComponentID
		{
			get;
			private set;
		}

		public string ComponentName
		{
			get;
			private set;
		}

		public string MethodName
		{
			get;
			private set;
		}

		public TryException(string Message,Exception InnerException,int ComponentID, string ComponentName, string MethodName):base(Message,InnerException)
		{
			this.ComponentID = ComponentID; this.ComponentName = ComponentName; this.MethodName = MethodName;
		}

		
	}
}
