using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Dtos.MailKit;
using VetSystems.Shared.Events;

namespace VetSystems.Shared.Service.MailKit
{
    public class SendMailService
    {
        private MailSenderDto _sender;

        public SendMailService(MailSenderDto sender)
        {
            _sender = sender;
        }

        public Response<string> SendMail(MailDetailDto Mails)
        {
            Response<string> response = Response<string>.Success(200);
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_sender.DisplayName, _sender.EmailId);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(Mails.EmailToName, Mails.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = Mails.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = Mails.EmailBody;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_sender.Host, _sender.Port, _sender.UseSSL);
                emailClient.Authenticate(_sender.EmailId, _sender.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
            }
            catch (Exception exc)
            {
                response = Response<string>.Fail(exc.Message, 500);
            }
            return response;
        }

    }
}
