using AutoMapper;
using MassTransit.Futures.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Queries
{
    public class GetTitleDefinationQuery : IRequest<Response<List<TitleDefinationDto>>>
    {
    }

    public class GetTitleDefinationQueryHandler : IRequestHandler<GetTitleDefinationQuery, Response<List<TitleDefinationDto>>>
    {

        private readonly IRepository<TitleDefinitions> _titleDefinationRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public GetTitleDefinationQueryHandler(IRepository<TitleDefinitions> titleDefinationRepository, IIdentityRepository identityRepository, IMapper mapper)
        {
            _titleDefinationRepository = titleDefinationRepository ?? throw new ArgumentNullException(nameof(titleDefinationRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<List<TitleDefinationDto>>> Handle(GetTitleDefinationQuery request, CancellationToken cancellationToken)
        {
            List<TitleDefinitions> titlelist = (await _titleDefinationRepository.GetAsync(x=>x.Deleted == false)).ToList();
            var result = _mapper.Map<List<TitleDefinationDto>>(titlelist.OrderByDescending(e => e.CreateDate));
            return Response<List<TitleDefinationDto>>.Success(result, 200);
        }
    }
}
