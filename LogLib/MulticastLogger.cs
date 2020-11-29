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

		public MulticastLogger(IPAddress MulticastIPaddress, int Port) : base()
		{
			IPEndPoint localEndPoint;

			this.multicastIPaddress = MulticastIPaddress;
			localEndPoint = new IPEndPoint(IPAddress.Any, 0);
			remoteEndPoint = new IPEndPoint(MulticastIPaddress, Port);

			client = new UdpClient(AddressFamily.InterNetwork);
			
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			//client.Client.Bind(localEndPoint);
			client.JoinMulticastGroup(MulticastIPaddress, IPAddress.Any);

		}

		

		public override void Dispose()
		{
			client.DropMulticastGroup(multicastIPaddress);
			client.Close();
		}

		public override void Log(Log Log)
		{
			byte[] buffer;

			lock (locker)
			{
				buffer = Log.Serialize();
				client.Send(buffer,buffer.Length,remoteEndPoint);
			}
		}

	}
}
