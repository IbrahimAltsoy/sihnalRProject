using Microsoft.AspNetCore.SignalR;
using System.Net.Mail;
using System.Net;

namespace sihnalRProject.Hubs
{
    public class MyHub:Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
            await SendEmailAsync(message);
        }
        public async Task SendEmailAsync(string message)
        {
            
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("altsoyibrahim5@gmail.com", "redugfbnvzmhyouq"),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress("i.altsoy@gmail.com"),//userId 
                Subject = "SignalR dan mesaj gelmiştir.",
                Body = message,
            };

            mailMessage.To.Add("i.altsoy@gmail.com");

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
