using AutoMapper;
using BrewCloud.Gym.Application.Models.Member;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Gym.Application.Features.Member.Queries
{
    public class GetMembersListQuery : IRequest<Response<List<MemberListDto>>>
    {
    }

    public class GetMembersListQueryHandler : IRequestHandler<GetMembersListQuery, Response<List<MemberListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMembersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<MemberListDto>>> Handle(GetMembersListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<MemberListDto>>();
            try
            {
                string query = string.Empty;

                query = @"SELECT id, firstname, lastname  
                          FROM  gymmember where deleted = 0";
                var _data = _uow.Query<MemberListDto>(query).ToList();
                response.Data = _data;
            }
            catch (Exception ex)
            {
                return Response<List<MemberListDto>>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
