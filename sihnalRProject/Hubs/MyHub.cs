using Microsoft.AspNetCore.SignalR;
using System.Net.Mail;
using System.Net;

namespace sihnalRProject.Hubs
{
    public class MyHub:Hub
    {
        static List<string> clients = new List<string>();
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
            await SendEmailAsync(message);
        }
        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            //await Clients.All.SendAsync("Kullanıcı giriş sağladı");// bütün kullanıcılara mesaj atar
            await Clients.Client(Context.ConnectionId).SendAsync("clientJoin", Context.ConnectionId);// giriş yapan kullanıcıya mesaj gönderir
            await Clients.All.SendAsync("clientJoin", Context.ConnectionId);
        }
        public  override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("clientLeft", Context.ConnectionId);
           
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
