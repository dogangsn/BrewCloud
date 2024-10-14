using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Chat.Application.Models;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Chat.Application.Features.Message.Commands
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
