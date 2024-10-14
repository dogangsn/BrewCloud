using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.PetHotels.Rooms;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Settings.VetLog.Queries
{
    public class GetLogsQuery : IRequest<Response<List<LogDto>>>
    {
        public string MasterId { get; set; }
    }

    public class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, Response<List<LogDto>>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLogsQueryHandler> _logger;
        private readonly IRepository<VetLogs> _vetLogsRepository;

        public GetLogsQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetLogsQueryHandler> logger, IRepository<VetLogs> vetLogsRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetLogsRepository = vetLogsRepository;
        }

        public async Task<Response<List<LogDto>>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {

            var response = Response<List<LogDto>>.Success(200);
            try
            {
                List<VetLogs> _logs = (await _vetLogsRepository.GetAsync(x => x.Deleted == false && x.MasterId == request.MasterId)).ToList();
                var result = _mapper.Map<List<LogDto>>(_logs.OrderByDescending(e => e.CreateDate));
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<LogDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
