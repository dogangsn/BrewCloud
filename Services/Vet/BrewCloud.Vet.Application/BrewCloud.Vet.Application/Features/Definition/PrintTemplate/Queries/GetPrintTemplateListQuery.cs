using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Application.Models.Definition.PrintTemplate;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BrewCloud.Vet.Application.Models.Definition.AppointmentTypes;

namespace BrewCloud.Vet.Application.Features.Definition.PrintTemplate.Queries
{
    public class GetPrintTemplateListQuery : IRequest<Response<List<PrintTemplateListDto>>>
    {

    }

    public class GetPrintTemplateListQueryHandler : IRequestHandler<GetPrintTemplateListQuery, Response<List<PrintTemplateListDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPrintTemplateListQueryHandler> _logger;
        private readonly IRepository<VetPrintTemplate> _vetPrintTemplateRepository;

        public GetPrintTemplateListQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetPrintTemplateListQueryHandler> logger, IRepository<VetPrintTemplate> vetPrintTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPrintTemplateRepository = vetPrintTemplateRepository;
        }

        public async Task<Response<List<PrintTemplateListDto>>> Handle(GetPrintTemplateListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PrintTemplateListDto>>();
            try
            {
                List<VetPrintTemplate> _list = (await _vetPrintTemplateRepository.GetAsync(x => x.Deleted == false)).OrderBy(x => x.Type).ToList();
                var result = _mapper.Map<List<PrintTemplateListDto>>(_list);
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<PrintTemplateListDto>>.Fail(ex.Message, 404);
            }
            return response;
        }
    }
}
