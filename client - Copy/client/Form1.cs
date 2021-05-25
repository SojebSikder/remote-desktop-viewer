using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;

namespace client
{
    public partial class Form1 : Form
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;
        

        private static Image GrabDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(bounds.X,bounds.Y,0,0,bounds.Size, CopyPixelOperation.SourceCopy);
           
            return screenshot;
        }

        private static string text()
        {
            string text = "Hello android";

            return text;
        }

        private void SendDesktopImage()
        {
          
                //BinaryFormatter binFormatter = new BinaryFormatter();
                mainStream = client.GetStream();
                //binFormatter.Serialize(mainStream, text());
                string s = "hello";
                byte[] bt = Encoding.ASCII.GetBytes(s);

                //mainStream.Write(bt, 1, 2);
                mainStream.WriteByte(20);
         
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnShare.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            
            portNumber = int.Parse(txtPort.Text);
            try
            {
                client.Connect(txtIP.Text,portNumber);
                btnConnect.Text = "Connected";
               // btnConnect.Enabled = false;
               // btnConnect.ForeColor = Color.White;
                MessageBox.Show("Connected");
                btnConnect.Enabled = false;
                btnShare.Enabled = true;
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to connect..");
                btnConnect.Text = "Not Connected";
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            if(btnShare.Text.StartsWith("Share"))
            {
                timer1.Start();
                btnShare.Text = "Stop Sharing";
            }
            else{
                timer1.Stop();
                btnShare.Text = "Share My Screen";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDesktopImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This software created by sojeb sikder. (c)SojebSoft");
        }
       
    }
}
