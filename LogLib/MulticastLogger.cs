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
	public sealed class MulticastLogger : BaseLogger,IDisposable
	{
		private UdpClient client;
		private readonly object locker = new object();
		private IPAddress multicastIPaddress;
		private IPEndPoint remoteEndPoint ;

		public MulticastLogger(ILogFormatter Formatter, IPAddress MulticastIPaddress, int Port) : base(Formatter)
		{
			IPEndPoint localEndPoint;

			this.multicastIPaddress = MulticastIPaddress;
			localEndPoint = new IPEndPoint(IPAddress.Any, Port);
			remoteEndPoint = new IPEndPoint(MulticastIPaddress, Port);

			client = new UdpClient(AddressFamily.InterNetwork);
			
			client.ExclusiveAddressUse = false;
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			client.Client.Bind(localEndPoint);
			client.JoinMulticastGroup(MulticastIPaddress, IPAddress.Any);

		}

		

		public void Dispose()
		{
			client.DropMulticastGroup(multicastIPaddress);
			client.Close();
		}

		public override void Log(int ComponentID, string ComponentName, string MethodName, LogLevels Level, string Message)
		{
			string log;
			byte[] buffer;

			lock (locker)
			{
				log = Formatter.Format(DateTime.Now, ComponentID, ComponentName, MethodName, Level, Message);
				buffer = Encoding.Default.GetBytes(log);
				client.Send(buffer,buffer.Length,remoteEndPoint);
			}
		}

	}
}
