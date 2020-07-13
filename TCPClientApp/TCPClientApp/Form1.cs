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

namespace TCPClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket socketSend;

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //创建负责通信的socket
            socketSend=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(txt_IP.Text);
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txt_Point.Text));
            socketSend.Connect(point);

            ShowMsg("连接成功！");

            //开启不停的接受服务端发来的消息线程
            Thread th = new Thread(Recive);
            th.IsBackground = true;
            th.Start();

        }
        /// <summary>
        /// 不停的接受服务端发来的消息
        /// </summary>
        void Recive()
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 1024 * 10];
                int length = socketSend.Receive(buffer);
                if (length == 0)
                {
                    break;
                }
                //表示发送的是文字消息
                if (buffer[0]==0)
                {               
                    string s = Encoding.UTF8.GetString(buffer, 1, length);
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + s);
                }
                else if(buffer[0] == 1)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\Users\Teagle\Desktop";
                    sfd.Title = "请选择保存的图片";
                    sfd.Filter = "所以文件|*.*";
                    sfd.ShowDialog(this);//弹对话框

                    string path = sfd.FileName;
                    using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fsWrite.Write(buffer, 1, length);
                    }
                    MessageBox.Show("保持成功");
                }
                
            }
        }

        void ShowMsg(string str)
        {
            txtLog.AppendText(str + "\r\n");
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            string str = txtMsg.Text.Trim();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            socketSend.Send(buffer);
            ShowMsg("我：" + str);
            txtMsg.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
