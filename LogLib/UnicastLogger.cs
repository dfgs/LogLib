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
		private IPAddress remoteIPaddress;
		private IPEndPoint remoteEndPoint ;

		public UnicastLogger(IPAddress RemoteIPaddress, int Port) : base()
		{
			IPEndPoint localEndPoint;

			this.remoteIPaddress = RemoteIPaddress;
			localEndPoint = new IPEndPoint(IPAddress.Any, 0);
			remoteEndPoint = new IPEndPoint(RemoteIPaddress, Port);

			client = new UdpClient(AddressFamily.InterNetwork);
			
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			//client.Client.Bind(localEndPoint);
			//client.JoinMulticastGroup(RemoteIPaddress, IPAddress.Any);

		}

		

		public override void Dispose()
		{
			//client.DropMulticastGroup(remoteIPaddress);
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
