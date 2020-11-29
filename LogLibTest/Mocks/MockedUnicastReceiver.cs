using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogLibTest.Mocks
{
	public class MockedUnicastReceiver:IDisposable
	{
		private UdpClient client;

		public AutoResetEvent ReceivedEvent;

		public List<Log> Logs
		{
			get;
			set;
		}

		public MockedUnicastReceiver( int Port)
		{
			IPEndPoint localEndPoint;

			ReceivedEvent = new AutoResetEvent(false);

			Logs = new List<Log>();

			localEndPoint = new IPEndPoint(IPAddress.Any, Port);

			client = new UdpClient(AddressFamily.InterNetwork);

			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			client.Client.Bind(localEndPoint);

			client.BeginReceive(ReceivedCallback,null);
		}

		public void Dispose()
		{
			client.Close();
		}

		private void ReceivedCallback(IAsyncResult ar)
		{
			IPEndPoint sender = new IPEndPoint(0, 0);
			Byte[] buffer = client.EndReceive(ar, ref sender);

			Logs.Add(Log.Deserialize(buffer)); 

			client.BeginReceive(new AsyncCallback(ReceivedCallback), null);
			
			ReceivedEvent.Set();

		}


	}
}
