using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Chat.Application.Models;
using VetSystems.Shared.Dtos;

namespace VetSystems.Chat.Application.Features.Message.Commands
{
    public class CreateMessageCommand : IRequest<Response<ChatDto>>
    {
    }

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Response<ChatDto>>
    {
        public Task<Response<ChatDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
