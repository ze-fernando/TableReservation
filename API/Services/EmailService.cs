using API.Config;
using API.Models;
using System.Net;
using System.Net.Mail;

namespace API.Services;

public class EmailService
{

    public async Task SendEmail(string to, string subject, string body)
    {

        string senderMail = Settings.SenderMail;
        string senderPass = Settings.SenderPass;

        using var smtpClient = new SmtpClient(Settings.Host, Settings.Port)
        {
            Credentials = new NetworkCredential(senderMail, senderPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(senderMail, to, subject, body)
        {
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task MakeConfirmEmail(Reservation resv)
    {
        var body = @$"
             <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <p>Obrigado por reservar uma mesa em nosso restaurante. Por favor, confirme a sua reserva clicando no botão abaixo:</p>
                <div>{resv}</div>
                <a href='localhost:5241/api/redis/{resv.Id}'style='display: inline-block; padding: 10px 20px; font-size: 16px; color: #fff; background-color: #007BFF; text-decoration: none; border-radius: 5px;'>
                    Clique aqui para confirmar o email
                </a>
                <p>Se o botão acima não funcionar, copie e cole o link abaixo no seu navegador:</p>
                <p><a href='localhost:5241/api/redis/{resv.Id}'>localhost:5241/api/redis/{resv.Id}</a>
                </p>

                <p>Atenciosamente,<br>Restaurante tal e tal</p>
            </body>
            ";

        string subject = "Confirmação de reserva";

        await SendEmail(resv.EmailOfClient, subject, body);

    }
}
