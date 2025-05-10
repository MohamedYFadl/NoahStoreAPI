using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NoahStore.Core.Dto;
using NoahStore.Core.Services;

namespace NoahStore.Service
{
    public class MailService : IMailService
    {
        private readonly IOptions<EmailSettings> _options;

        public MailService(IOptions<EmailSettings> options)
        {
            _options = options;
        }
        public async Task SendEmail(EmailDto email)
        {
            #region Message Body
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.Subject,
            };
            message.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { 
                Text = $@"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Confirm Your Account</title>
    <meta charset=""UTF-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
</head>
<style type=""text/css"">
    html {{
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }}

    section {{
        text-align: center;
        padding: 30px;
        border: 1px solid #ddd;
        max-width: 450px;
        margin: auto;
        box-shadow: 1px 1px 1px #ddd;
    }}

    section h1 {{
        font-weight: bold;
        margin-bottom: 20px;
        border-bottom: 1px solid #ddd;
        padding-bottom: 12px;
    }}

    section p {{
        margin-bottom: 30px;

    }}

    section a {{
        text-decoration: none;
        color: white;
        padding: 12px 24px;
        background: #09c;
        border-radius: 20px;
    }}
</style>

<body>
    <section>
        <h1>{email.Subject}</h1>
        <p{email.Message}</p>
        <a href=""#"">Confirm</a>
    </section>
</body>

</html>"
            };
            #endregion

            #region Establish connection && Send Email
            using(var smtp = new SmtpClient())
            {
                try
                {
                   await smtp.ConnectAsync(_options.Value.Host, _options.Value.Port, SecureSocketOptions.StartTls);
                   await smtp.AuthenticateAsync(_options.Value.Email, _options.Value.Password);
                   await  smtp.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();

                }
            }
             
            
            #endregion


        }
    }
}
