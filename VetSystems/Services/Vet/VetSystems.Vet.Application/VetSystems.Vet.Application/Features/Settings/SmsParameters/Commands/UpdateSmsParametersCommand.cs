using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Settings.SmsParameters.Commands
{
    public class UpdateSmsParametersCommand : IRequest<Response<bool>>
    {

    }

    public class UpdateSmsParametersCommandHandler : IRequestHandler<UpdateSmsParametersCommand, Response<bool>>
    {

        public Task<Response<bool>> Handle(UpdateSmsParametersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
