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
	public class MockedMulticastReceiver:IDisposable
	{
		private UdpClient client;
		private IPAddress multicastIPaddress;
		private IPEndPoint remoteEndPoint;

		public AutoResetEvent ReceivedEvent;

		public List<string> Logs
		{
			get;
			set;
		}

		public MockedMulticastReceiver(IPAddress MulticastIPaddress, int Port)
		{
			IPEndPoint localEndPoint;

			ReceivedEvent = new AutoResetEvent(false);

			Logs = new List<string>();

			this.multicastIPaddress = MulticastIPaddress;
			localEndPoint = new IPEndPoint(IPAddress.Any, Port);
			remoteEndPoint = new IPEndPoint(MulticastIPaddress, Port);

			client = new UdpClient(AddressFamily.InterNetwork);

			client.ExclusiveAddressUse = false;
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			client.Client.Bind(localEndPoint);
			client.JoinMulticastGroup(MulticastIPaddress, IPAddress.Any);

			client.BeginReceive(ReceivedCallback,null);
		}

		public void Dispose()
		{

			client.DropMulticastGroup(multicastIPaddress);
			client.Close();
		}

		private void ReceivedCallback(IAsyncResult ar)
		{
			IPEndPoint sender = new IPEndPoint(0, 0);
			Byte[] buffer = client.EndReceive(ar, ref sender);

			Logs.Add(Encoding.Default.GetString(buffer)); 

			client.BeginReceive(new AsyncCallback(ReceivedCallback), null);
			
			ReceivedEvent.Set();

		}


	}
}
