using System.Net;
using System.Net.Mail;

namespace EMailSender
{
    static class EMailSend
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("denemee828@gmail.com", "Kale.1234"); // Gönderen bilgileri - SMTP Gmail hatası verdi. Kullanım dışına alınmış.
            smtp.Credentials = credential;

            MailAddress sender = new MailAddress("denemee828@gmail.com", "ExampleSender");
            MailAddress taker = new MailAddress(to);

            MailMessage mail = new MailMessage(sender, taker);
            mail.Subject = "Example";
            mail.Body = message;

            smtp.Send(mail);
        }
    }
}
