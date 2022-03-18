using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailSend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> MailList = new List<string>();
            MailList.Add("Lucas.zhuge@chroma.com.tw");
            //MailList.Add("secrettime11@yahoo.com.tw");
            //MailList.Add("ccy840327@eipmail.nantou.gov.tw");
            for (int i = 0; i < 10; i++)
            {
                SendMailByGmail(MailList, "員工諸葛宏儒先生", $"先用100封警告，不允許封鎖本皇子。");
            }
            Console.WriteLine("done");
        }
        private void SendMailByGmail(List<string> MailList, string Subject, string Body)
        {
            MailMessage msg = new MailMessage();
            //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
            msg.To.Add(string.Join(",", MailList.ToArray()));
            msg.From = new MailAddress("secrettime11@gmail.com", "工務賠償單", System.Text.Encoding.UTF8);
            //郵件標題 
            msg.Subject = Subject;
            //郵件標題編碼  
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //郵件內容
            msg.Body = Body;
            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
            msg.Priority = MailPriority.Normal;//郵件優先級
            
            //要附加的檔案
            string filePath = @"C:\Users\user\Desktop\S__7790946.jpg";    
            Attachment attachment1 = new Attachment(filePath);
            attachment1.Name = System.IO.Path.GetFileName(filePath);
            attachment1.NameEncoding = Encoding.GetEncoding("utf-8");
            attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            attachment1.ContentDisposition.Inline = true;
            attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
            msg.Attachments.Add(attachment1);
            #region 其它 Host
            /*
             *  outlook.com smtp.live.com port:25
             *  yahoo smtp.mail.yahoo.com.tw port:465
            */
            #endregion
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("secrettime11@gmail.com", "xhqoljqvxgnhbzov");
            //Gmial 的 smtp 使用 SSL
            MySmtp.EnableSsl = true;
            MySmtp.Send(msg);
        }
    }
}
