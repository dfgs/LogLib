using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public abstract class Try:ITry
	{
		protected ILogger Logger
		{
			get;
			private set;
		}

		protected int ComponentID
		{
			get;
			private set;
		}

		protected string ComponentName
		{
			get;
			private set;
		}

		protected string MethodName
		{
			get;
			private set;
		}

		public Try(ILogger Logger,int ComponentID,string ComponentName,string MethodName)
		{
			this.Logger = Logger;this.ComponentID = ComponentID; this.ComponentName = ComponentName;this.MethodName = MethodName;
		}

	
	}
}
