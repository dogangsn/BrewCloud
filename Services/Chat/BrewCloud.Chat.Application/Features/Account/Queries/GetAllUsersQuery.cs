using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Chat.Application.GrpServices;
using BrewCloud.Chat.Application.Models;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Chat.Application.Features.Account.Queries
{
    public class GetAllUsersQuery : IRequest<Response<List<ChatDto>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<List<ChatDto>>>
    {
        private readonly IdentityGrpService _identityGrpService;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IdentityGrpService identityGrpService,  IIdentityRepository identity, IMapper mapper)
        {
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<List<ChatDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ChatDto>>
            {
                Data = new List<ChatDto>(),
                ResponseType = ResponseType.Ok
            };

            try
            {
                var enterpriseId = _identity.Account.EnterpriseId;
                var identityUsers = await _identityGrpService.GetCompanyUsersAsync(enterpriseId.ToString());
                if (!identityUsers.IsSuccess)
                {
                    return Response<List<ChatDto>>.Success(200);
                }
                var userdata = identityUsers.Data.ToList();
                var result = userdata.Select(r => new SignupDto
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    AppKey = r.AppKey,
                    UserAppKey = r.UserAppKey,
                }).ToList();

                foreach (var item in result)
                {
                    ChatDto _chatUsers = new();
                    _chatUsers.Id = Guid.Parse(item.Id);
                    _chatUsers.Contact.Id = Guid.Parse(item.Id);
                    _chatUsers.UnreadCount = 0;
                    _chatUsers.Contact.Name = item.FirstName;
                    response.Data.Add(_chatUsers);
                }
            }
            catch (Exception ex)
            {

                response.ResponseType = ResponseType.Error;
            }


            return response;
        }
    }
}
