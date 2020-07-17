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
        private bool hasRecived;

        // 建立一个全局的临时缓冲用来接收每次接收的数据
        private byte[] buffer;

        // 新建一个枚举对象用来描述 接收的数据是文字类型还是图片类型
        private MessageType messageType = MessageType.Word;

        public Form1()
        {
            InitializeComponent();
        }
        Socket socketSend;

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //创建负责通信的socket
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
            MessageType type = default;
            this.hasRecived = false;
            // 一开始先接收5个字节的信息  1个数据类型 4个数据长度
            this.buffer = new byte[5];
            while (true)
            {
                // 这里每次接收数据都  new 一个新的对象 对内存开销很大 
                // 所以用一个全局的buffer迭代接收数据 
                int length = socketSend.Receive(buffer);
                if (length == 0) break;
                else
                {
                    // 一开始hasRecived == false 表示没有进入接收完成阶段
                    if (this.hasRecived == false)
                    {
                        // 解析数据包基本信息
                        // 拿到第一个字节 转成枚举
                        type = (MessageType)buffer[0];

                        // 拿到数据包长度
                        var recivedLength = BitConverter.ToInt32(buffer.Skip(1).ToArray(), 0);

                        // 重新安装新的长度分配这个 buffer缓存
                        this.buffer = new byte[recivedLength];

                        // 置标志 可以接收正文数据了
                        this.hasRecived = true;
                    }
                    // 标志是 true 那么可以开始解析正文信息了
                    else
                    {
                        // 根据之前的type标志 来确认接下来如何处理
                        switch (type)
                        {
                            case MessageType.Word:
                            {
                                string s = Encoding.UTF8.GetString(this.buffer, 0, this.buffer.Length);
                                ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + s);
                                break;
                            }
                            case MessageType.Image:
                            {
                                MemoryStream ms = new MemoryStream(); //新建内存bai流du
                                ms.Write(buffer, 0, buffer.Length); //附值
                                pictureBox1.Image = Image.FromStream(ms); //读取zhi流中内容dao
                                break;
                            }
                            default: break;
                        }
                        this.buffer = new byte[5];
                        this.hasRecived = false;
                    }
                }


                //// 这里是结束的地方 因为如果是结束符8  说明数据传输完毕
                //if(buffer[0]==32)
                //{
                //    // 直接取得第一个字符 转成枚举对象便于识别 （时间久了  谁知道 0和1 代表啥）
                //    this.messageType  = (MessageType)this.cache[0];

                //    // 首字符是识别字符 不是数据 所以扔掉  尾字符也是
                //    this.cache.RemoveAt(0);
                //    this.cache.Remove(this.cache.Last());

                //    // 这里开始处理数据
                //    switch (this.messageType)
                //    {
                //        case MessageType.Word:
                //        {
                //            string s = Encoding.UTF8.GetString(this.cache.ToArray(), 0, this.cache.Count);
                //            ShowMsg(socketSend.RemoteEndPoint.ToString() + ":" + s);
                //            break;
                //        }
                //        case MessageType.Image:
                //        {
                //            MemoryStream ms = new MemoryStream(); //新建内存bai流du
                //            ms.Write(buffer, 0, buffer.Length); //附值
                //            pictureBox1.Image = Image.FromStream(ms); //读取zhi流中内容dao
                //            break;
                //        }
                //    }
                //}
                ////  如果没有读到结束符 则往缓存里放数据
                //else
                //{
                //    this.cache.Add(this.buffer[0]);
                //}
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

    public enum MessageType
    {
        Word = 0,
        Image = 1
    }


}
