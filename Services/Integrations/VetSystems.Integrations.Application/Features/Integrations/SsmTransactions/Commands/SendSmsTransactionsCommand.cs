using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Integrations.Application.Service.Sms;
using VetSystems.Shared.Dtos;

namespace VetSystems.Integrations.Application.Features.Integrations.SsmTransactions.Commands
{
    public class SendSmsTransactionsCommand : IRequest<Response<bool>>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string[] SendPhone { get; set; } 
    }

    public class SendSmsTransactionsCommandHandler : IRequestHandler<SendSmsTransactionsCommand, Response<bool>>
    { 
        public SendSmsTransactionsCommandHandler()
        { 
        }

        public async Task<Response<bool>> Handle(SendSmsTransactionsCommand request, CancellationToken cancellationToken)
        { 
            var response = Response<bool>.Success(200);
            try
            {
                SmsService service = new SmsService(request.UserName, request.PassWord, "DOGANGUNES");
                //String[] numaralar = { "05398533010", "05530154968" };
                String[] numaralar = request.SendPhone;
                service.addSMS(request.Content, numaralar);
                String sonuc = service.gonder();
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }


}
