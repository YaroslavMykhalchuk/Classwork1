using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress address = IPAddress.Parse("192.168.1.6");
//IPAddress address = IPAddress.Loopback;
//IPAddress address = Dns.GetHostAddresses(Dns.GetHostName());
int port = 1024;
IPEndPoint endPoint = new IPEndPoint(address, port);
Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
pass_socket.Bind(endPoint);
pass_socket.Listen(10);
Console.WriteLine($"Server was started work at port 1024 on address {address}");

Info();

IPAddress[] addresses = Dns.GetHostAddresses("microsoft.com");
string str = "";
foreach(var adress in addresses)
{
	Console.WriteLine(adress);
	str += adress + "\t";
}

try
{
	while(true)
	{
		Socket ns = pass_socket.Accept();
		Console.WriteLine($"Client #{ns.LocalEndPoint} connected!");
		Console.WriteLine($"Client #{ns.RemoteEndPoint} connected!");
		ns.Send(Encoding.Default.GetBytes($"Server {ns.LocalEndPoint} send answer {DateTime.Now}\n, adress microsoft {str}"));
		ns.Shutdown(SocketShutdown.Both);
		ns.Close();
	}
}
catch (SocketException ex)
{
	Console.WriteLine(ex.Message);
}

void Info()
{
    Console.WriteLine($"The IPEndPoint is: {address}:{port}\nThe AddressFamily is: {AddressFamily.InterNetwork.ToString()}\n" +
		$"The address is: {address}, and the port is:{port}");
}