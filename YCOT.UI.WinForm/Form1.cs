using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace YCOT.UI.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string respxml = "<?xml version='1.0' encoding='UTF-8'?><request><memberId>220.248.115.134</memberId><airline></airline><arrivalCity>SHA</arrivalCity><date>2013-10-15</date><departureCity>CSX</departureCity><queryFlag>1</queryFlag><clerk></clerk><websiteCode>1</websiteCode><version>1</version></request>";

            B2CTicketWebServices.B2CMemberWebServicePortTypeClient ws = new B2CTicketWebServices.B2CMemberWebServicePortTypeClient();
            string user = "061";
            string pswd = "123456";
            pswd = GetMD5(pswd, "gb2312");
            string md5respxml = GetMD5(respxml + pswd, "gb2312");

            
            var BB = ws.ticketOrdersSearch(user, md5respxml, respxml);


            richTextBox1.Text = BB.ToString();
        }



        public static string GetMD5(string s, string _input_charset)
        {

            /// <summary>
            /// 与ASP兼容的MD5加密算法
            /// </summary>

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
