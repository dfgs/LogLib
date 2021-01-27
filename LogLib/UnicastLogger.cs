using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
	public sealed class UnicastLogger : BaseLogger,IDisposable
	{
		private UdpClient client;
		private readonly object locker = new object();
		private IPEndPoint remoteEndPoint ;

		public UnicastLogger(IPAddress RemoteIPaddress, int Port) : base()
		{
			IPEndPoint localEndPoint;

			localEndPoint = new IPEndPoint(IPAddress.Any, 0);
			remoteEndPoint = new IPEndPoint(RemoteIPaddress, Port);

			client = new UdpClient(AddressFamily.InterNetwork);
		}

		

		public override void Dispose()
		{
			client.Close();
		}

		public override void Log(Log Log)
		{
			byte[] buffer;

			lock (locker)
			{
				buffer = Log.Serialize();
				try { client.Send(buffer, buffer.Length, remoteEndPoint); } catch { }
			}
		}

	}
}
