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
            // 把文本信息转成byte[] 流
            byte[] buffer = Encoding.UTF8.GetBytes(txtMsg.Text);

            // 把缓存数据的长度转成byte[] 数组发送出去 给接收端一个明确的长度信息
            byte[] bufferLengthInfo = BitConverter.GetBytes(buffer.Length);
            List<byte> list = new List<byte>();

            // 加上数据类型信息
            list.Add(0);

            //  加上数据长度信息
            list.AddRange(bufferLengthInfo);

            // 最后加上数据信息本身
            list.AddRange(buffer);

            //给客户端发消息
            socketSend.Send(list.ToArray());

            ShowMsg($"服务器：{txtMsg.Text}");

            txtMsg.Clear();
        }

        /// <summary>
        /// 选择要发送的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Title = "请选择要发送的图片",
                Filter = "所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK) txtPath.Text = ofd.FileName;
        }

        private void btnSendPic_Click(object sender, EventArgs e)
        {
            //获得发送图片的路径
            string path = txtPath.Text;
            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // 分配一个较大的byte[] 缓存数组
                byte[] buffer = new byte[1024 * 1024 * 5];

                // 把图片读到缓存里
                int bufferLength = fsRead.Read(buffer, 0, buffer.Length);

                // 新建一个动态List 添加各个数据段
                List<byte> list = new List<byte>();

                // 添加图片标识信息
                list.Add(1);

                // 添加数据长度信息
                list.AddRange(BitConverter.GetBytes(bufferLength));

                // 根据长度截取缓存 buffer对象 并添加到动态List
                list.AddRange(buffer.Take(bufferLength));
               
                // 最终动态List转成 byte[] 并发送
                socketSend.Send(list.ToArray());
            }
        }
    }
}
