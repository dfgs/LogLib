using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public static class ExceptionFormatter
	{
		public static string Format(Exception ex)
		{
			StringBuilder sb;
			sb = new StringBuilder();
			while (ex != null)
			{
				sb.Append("->");
				sb.Append(ex.ToString());
				ex=ex.InnerException;
			}
			return sb.ToString();
		}
	}
}
