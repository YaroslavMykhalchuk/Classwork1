using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Client";
            Process.Start("Server.exe");
        }

        private void buttonReceive_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(textBoxIPAddress.Text);
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client_socket.Connect(endPoint);
                if(client_socket.Connected)
                {
                    string query = "GET\r\n\r\n";
                    client_socket.Send(Encoding.Default.GetBytes(query));
                    byte[] buffer = new byte[1024];
                    int len;
                    do
                    {
                        len = client_socket.Receive(buffer);
                        textBox1.Text += Encoding.Default.GetString(buffer, 0, len);
                    } while (client_socket.Available > 0);
                }
                else
                {
                    MessageBox.Show("Error connection!");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { 
                client_socket.Close();
            }
        }
    }
}