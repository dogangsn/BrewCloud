using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Integrations.Application.Features.Integrations.SsmTransactions.Commands
{
    public class SendSmsTransactions : IRequest<Response<bool>>
    {

    }

    public class SendSmsTransactionsHandler : IRequestHandler<SendSmsTransactions, Response<bool>>
    {

        public Task<Response<bool>> Handle(SendSmsTransactions request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
