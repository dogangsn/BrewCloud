using Azure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Application.Models.Settings.SmsParameters;

namespace VetSystems.Vet.Application.Features.Settings.SmsParameters.Queries
{
    public class GetSmsParametersListQuery : IRequest<Response<List<SmsParametersDto>>>
    {
    }

    public class GetSmsParametersListQueryHandler : IRequestHandler<GetSmsParametersListQuery, Response<List<SmsParametersDto>>>
    {
        public Task<Response<List<SmsParametersDto>>> Handle(GetSmsParametersListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
