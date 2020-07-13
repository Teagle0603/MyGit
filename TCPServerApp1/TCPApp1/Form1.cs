using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(IPtxt.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPoint.Text));

                socketWatch.Bind(point);
                ShowMsg("监听成功！");
                socketWatch.Listen(10);

                Thread th = new Thread(Listen);
                th.IsBackground = true;//后台执行
                th.Start(socketWatch);
            }
            catch 
            { }

       
        }
        /// <summary>
        /// 等待客户连接，并且创建通信的socket
        /// </summary>
        Socket socketSend;
        void Listen(object o)
        {
            Socket socketWatch = o as Socket;
            try
            {
                while (true)
                {
                    socketSend = socketWatch.Accept();
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功！");
                    //开启一个新线程，不停接受客户端发来的消息
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);

                }

            }
            catch
            { }

        }

        /// <summary>
        /// 服务器端不停接受客户端发来的消息
        /// </summary>
        /// <param name="o"></param>
        void Recive(object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                try
                {
                    //客户端连接成功后，服务器应该接受客户端发来的消息
                    byte[] buffer = new byte[1024 * 1024 * 10];
                    //实际接受到的有效字节数
                    int length = socketSend.Receive(buffer);
                    if (length == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, length);
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + str);
                }
                catch
                { }
                
            }
        }

        void ShowMsg(string str)
        {
            txtLog.AppendText(str + "\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 服务端给客户端发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            string str = txtMsg.Text;
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            List<byte> list = new List<byte>();
            list.Add(0);
            list.AddRange(buffer);
            //将泛型数组转换成数组
            byte[] NewBuffer = list.ToArray();
            //给客户端发消息
            socketSend.Send(NewBuffer);
            ShowMsg("服务器：" + str);
            txtMsg.Clear();
        }

        /// <summary>
        /// 选择要发送的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\Teagle\Desktop";//设置初始目录
            ofd.Title = "请选择要发送的图片";
            ofd.Filter = "所有文件|*.*";
            ofd.ShowDialog();

            txtPath.Text = ofd.FileName;
        }

        private void btnSendPic_Click(object sender, EventArgs e)
        {
            //获得发送图片的路径
            string path = txtPath.Text;
            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int Len = fsRead.Read(buffer, 0, buffer.Length);
                List<byte> list = new List<byte>();
                list.Add(1);
                list.AddRange(buffer);
                byte[] newBuffer = list.ToArray();
                
                socketSend.Send(newBuffer,0,Len,SocketFlags.None);
            }
        }
    }
}
