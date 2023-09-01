using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace serwerr
{
    public partial class przenoszenie : Form
    {
        
        public string ip;
        public int liczba;
        public StreamReader reader;
        public StreamWriter writer;
        public string textsend;
        public String recive;
        public IPAddress adres;
        public TcpClient client;
        public TcpListener listener;
        public NetworkStream stream;
        public string wiadomosc;
        public int bytees;
        public List<TcpListener> klienci=new List<TcpListener> ();
        public static przenoszenie prze;
        public DoWorkEventHandler polaczenie;

        public przenoszenie()
        {
            InitializeComponent();
            stream = default(NetworkStream);
            TcpClient client = new TcpClient();
            liczba = Form1.ins.liczba;


            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ipp in host.AddressList)
            {
                if (ipp.AddressFamily == AddressFamily.InterNetwork)
                {
                    adres = ipp;
                    label2.Text = adres.ToString();
                }
            }
            ip = Form1.ins.ip;
            
            

        }

        private void przenoszenie_Load(object sender, EventArgs e)
        {

            if (liczba == 1)
            {
                listener = new TcpListener(adres, 80);
                
                klienci.Add(new TcpListener(adres, 80));
                listener.Start();
                textBox1.AppendText("Serwer dziala i oczekuje na klienta"+Environment.NewLine);

                var tasks = new List<Task>();


                
                Task.Run(async () =>
                {



                    

                    while (true)
                    {

                        client = listener.AcceptTcpClient();

                        IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                        string ipAddress = endPoint.Address.ToString();
                        textBox1.Invoke(new MethodInvoker(delegate
                        {
                            textBox1.AppendText("Ip klient to "+ipAddress+Environment.NewLine);
                        }));
                        var task = HandleClientAsync();
                        tasks.Add(task);
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork+= new DoWorkEventHandler (Polaczenie);
                        bw.RunWorkerAsync();
                        //bw.DoWork += new DoWorkEventHandler(bb1);

                        //backgroundWorker1.RunWorkerAsync();



                        /*int bytess = stream.Read(bytes, 0, bytes.Length);
                            stream.Read(bytes, 0, bytes.Length);
                            String wiadomosc = Encoding.ASCII.GetString(bytes);
                            wiadomosc = wiadomosc.Substring(0, wiadomosc.IndexOf(""));
                            textBox1.AppendText("Wiadomość przychodząca: " + wiadomosc);


                            string serverResponse = "Server response: " + wiadomosc;
                            Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            stream.Write(sendBytes, 0, sendBytes.Length);
                            stream.Flush();

                            Invoke((MethodInvoker)delegate
                            {
                                textBox1.AppendText(wiadomosc);
                            });

                            textBox1.AppendText(" >> " + serverResponse);
                        */


                    }
                });




                }
            else
            {
                client = new TcpClient(ip,80);
                stream = client.GetStream();
                //client.Connect(adres, 80);
            }
        }
        async Task HandleClientAsync()
        {
            while (true)
            {
                stream = client.GetStream();
                byte[] bytes = new byte[1024];

                bytees = stream.Read(bytes, 0, bytes.Length);
                string wiadom = Encoding.ASCII.GetString(bytes, 0, bytees);

                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new MethodInvoker(delegate
                    {

                        textBox1.AppendText("Klient: " + wiadom + Environment.NewLine);
                    }));
                }
                else
                {
                    textBox1.AppendText("Klient: " + wiadom + Environment.NewLine);
                }
            }
        }
        private void Wyslij_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.InvokeRequired)
                {
                    textBox2.BeginInvoke((MethodInvoker)delegate
                    {
                        wiadomosc = textBox2.Text;
                    });
                }
                else
                {
                wiadomosc = textBox2.Text;
                }
                byte[] bytes = Encoding.ASCII.GetBytes(wiadomosc);
                stream.Write(bytes, 0, bytes.Length);
                textBox2.Text = null;
                /*byte[] wiadomosc = Encoding.ASCII.GetBytes(textBox2.Text);
                stream.Write(wiadomosc, 0, wiadomosc.Length);
                stream.Flush();
                textBox2.Clear();*/

            }
            catch
            {
                textBox1.AppendText("Błąd wysyłania");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*while (true)
            {
                stream = client.GetStream();
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new MethodInvoker(delegate
                    {

                        textBox1.AppendText("Klient sie polaczyl" + Environment.NewLine);
                    }));
                }
                else
                {
                    textBox1.AppendText("Klient klient sie polaczyl" + Environment.NewLine);
                }
            }*/
            
        }
        void Polaczenie(object sender, DoWorkEventArgs e)
        {
            
        }
    }
    
}
